﻿using DeploymentTool.Helpers;
using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Web.Http.Results;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Http.Formatting;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using DeploymentTool.Misc;
using ExcelDataReader;
using System.Reflection;

namespace DeploymentTool.Controller
{


    public class AttachmentController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        private readonly AttachmentDAL _attachmentRepository = new AttachmentDAL();

        [HttpPost]
        [Route("api/Attachment/CreateAttachment")]
        public IHttpActionResult CreateAttachment()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                var attachment = new Attachment
                {
                    FileName = httpRequest.Form["tfileName"],
                    FileExt = httpRequest.Form["tFileExt"],
                    FileType = httpRequest.Form["tfileType"],
                    AttachmentType = httpRequest.Form["tAttachmentType"],
                    AttachmentComments = httpRequest.Form["tAttachmentComments"],
                    AttachmentUrl = httpRequest.Form["tAttachmentURL"],
                    CreatedBy = Convert.ToInt32(httpRequest.Form["nCreatedBy"])
                };

                using (var stream = new MemoryStream())
                {
                    httpRequest.Files[0].InputStream.CopyTo(stream);
                    attachment.AttachmentBlob = stream.ToArray();
                }

                var attachmentId = _attachmentRepository.CreateAttachment(attachment);

                return Ok(new { AttachmentId = attachmentId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Attachment/UploadStore")]
        public async Task<HttpResponseMessage> UploadStore()
        {
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "UploadStore:Start:");
                HttpRequestMessage request = this.Request;
                if (!request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }


                TraceUtility.WriteTrace("AttachmentController", "UploadStore:Multipart:");
                try
                {

                    int nBrandIdTemp = 0;
                    var filesReadToProvider = await request.Content.ReadAsMultipartAsync();

                    foreach (var stream in filesReadToProvider.Contents)
                    {
                        string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                        if (fRequest.Length == 2)
                        {
                            string FileName = fRequest[0];
                            nBrandIdTemp = Convert.ToInt32(fRequest[1]);
                            break;
                        }
                    }
                    var output = db.Database.SqlQuery<string>("select top 1 tBrandName from tblBrand with (nolock) where aBrandId=@aBrandId ",new SqlParameter("@aBrandId", nBrandIdTemp)).FirstOrDefault();

                    if (output.Contains("Buffalo Wild Wings"))
                    {
                        List<ProjectExcelFields> fields = new List<ProjectExcelFields>();


                        foreach (var stream in filesReadToProvider.Contents)
                        {
                            string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                            if (fRequest.Length == 2)
                            {
                                string FileName = fRequest[0];
                                int nBrandId = Convert.ToInt32(fRequest[1]);
                                var fileBytes = await stream.ReadAsByteArrayAsync();
                                string URL = HttpRuntime.AppDomainAppPath;

                                string strFilePath = URL + @"Attachments\" + FileName;

                                TraceUtility.WriteTrace("AttachmentController", "UploadStore:strFilePath:" + strFilePath);
                                using (System.IO.BinaryWriter bw = new BinaryWriter(File.Open(strFilePath, FileMode.Create)))
                                {
                                    bw.Write(fileBytes);
                                    bw.Close();
                                }

                                TraceUtility.WriteTrace("AttachmentController", "UploadStore:Written:" + strFilePath);
                                ImportExceltoDatabase(fields, strFilePath, nBrandId);
                                /* fields.Add(new ProjectExcelFields() {
                                     tProjectType = "RELOCATION",
                                     tStoreNumber = "6937",
                                     tAddress = "461 Columbia Ave",
                                     tCity = "Lexington",
                                     tState = "SC",
                                     nDMAID = 546,
                                     tDMA = "COLUMBIA SC",
                                     tRED = "Michael Landru",
                                     tCM = "Kevin Dalpiaz",
                                     tANE = "",
                                     tRVP = "Linda Wiseley",
                                     tPrincipalPartner = "MICHAEL IRONS",
                                     dStatus = DateTime.Now,
                                     dOpenStore = DateTime.Now,
                                     tProjectStatus = "Under Construction"
                                 });*/

                            }
                        }
                        TraceUtility.WriteTrace("AttachmentController", "UploadStore:Returing");
                        return new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ObjectContent<List<ProjectExcelFields>>(fields, new JsonMediaTypeFormatter())
                        };
                    }
                    else
                    {
                        List<ProjectExcelFieldsSonic> fields = new List<ProjectExcelFieldsSonic>();

                        foreach (var stream in filesReadToProvider.Contents)
                        {
                            string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                            if (fRequest.Length == 2)
                            {
                                string FileName = fRequest[0];
                                int nBrandId = Convert.ToInt32(fRequest[1]);
                                var fileBytes = await stream.ReadAsByteArrayAsync();
                                string URL = HttpRuntime.AppDomainAppPath;

                                string strFilePath = URL + @"Attachments\" + FileName;

                                TraceUtility.WriteTrace("AttachmentController", "UploadStore:strFilePath:" + strFilePath);
                                using (System.IO.BinaryWriter bw = new BinaryWriter(File.Open(strFilePath, FileMode.Create)))
                                {
                                    bw.Write(fileBytes);
                                    bw.Close();
                                }

                                TraceUtility.WriteTrace("AttachmentController", "UploadStore:Written:" + strFilePath);
                                ImportExceltoDatabaseSonic(fields, strFilePath, nBrandId);
                                /* fields.Add(new ProjectExcelFields() {
                                     tProjectType = "RELOCATION",
                                     tStoreNumber = "6937",
                                     tAddress = "461 Columbia Ave",
                                     tCity = "Lexington",
                                     tState = "SC",
                                     nDMAID = 546,
                                     tDMA = "COLUMBIA SC",
                                     tRED = "Michael Landru",
                                     tCM = "Kevin Dalpiaz",
                                     tANE = "",
                                     tRVP = "Linda Wiseley",
                                     tPrincipalPartner = "MICHAEL IRONS",
                                     dStatus = DateTime.Now,
                                     dOpenStore = DateTime.Now,
                                     tProjectStatus = "Under Construction"
                                 });*/

                            }
                        }
                        TraceUtility.WriteTrace("AttachmentController", "UploadStore:Returing");
                        return new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ObjectContent<List<ProjectExcelFieldsSonic>>(fields, new JsonMediaTypeFormatter())
                        };
                    }
                }
                catch (System.Exception ex)
                {
                    TraceUtility.ForceWriteException("AttachmentController", HttpContext.Current, ex);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("AttachmentController2", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        void ImportExceltoDatabase(List<ProjectExcelFields> fields, string strFilePath, int nBrandId)
        {
            //  

            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                ProjectExcelFields objProjectExcel = new ProjectExcelFields();
                DataTable dt = new DataTable();
                try
                {
                    DataTable dtNew = new DataTable();
                    // oledbConn.Open();
                    using (var stream = File.Open(strFilePath, FileMode.Open, FileAccess.Read))
                    {
                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Choose one of either 1 or 2:

                            // 1. Use the reader methods
                            do
                            {
                                reader.Read();
                                int ColumnCount = reader.FieldCount;
                                for (int i = 0; i < ColumnCount; i++)
                                {
                                    string ColumnName = reader.GetValue(i).ToString();
                                    if (!dtNew.Columns.Contains(ColumnName))
                                    { dtNew.Columns.Add(ColumnName); }
                                }
                                while (reader.Read())
                                {

                                    // reader.GetDouble(0);
                                    string storeNumber = reader.GetValue(dtNew.Columns.IndexOf("Store Number")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Store Number")).ToString() : "";
                                    string projectType = reader.GetValue(dtNew.Columns.IndexOf("Project Type")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Type")).ToString() : "";

                                    if (projectType != "" && storeNumber != "")
                                    {
                                        SqlParameter tModuleNameParam = new SqlParameter("@tStoreNumber", storeNumber);
                                        var output = db.Database.SqlQuery<string>("Select tstoreNumber from tblstore with (nolock) where tstoreNumber= @tStoreNumber  and nBrandID=@nBrandId ", new SqlParameter("@tStoreNumber", storeNumber), new SqlParameter("@nBrandId", nBrandId)).FirstOrDefault();
                                        objProjectExcel = new ProjectExcelFields();

                                        if (storeNumber != output)
                                            objProjectExcel.nStoreExistStatus = 0;
                                        else
                                            objProjectExcel.nStoreExistStatus = 1;

                                        objProjectExcel.tProjectType = projectType;
                                        objProjectExcel.tStoreNumber = storeNumber;
                                        objProjectExcel.tAddress = reader.GetValue(dtNew.Columns.IndexOf("Address")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Address")).ToString() : "";
                                        objProjectExcel.tCity = reader.GetValue(dtNew.Columns.IndexOf("City")) != null ? reader.GetValue(dtNew.Columns.IndexOf("City")).ToString() : "";
                                        objProjectExcel.tState = reader.GetValue(dtNew.Columns.IndexOf("State")) != null ? reader.GetValue(dtNew.Columns.IndexOf("State")).ToString() : "";
                                        objProjectExcel.nDMAID = reader.GetValue(dtNew.Columns.IndexOf("DMA ID")) != null && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DMA ID"))) : 0;
                                        objProjectExcel.tDMA = reader.GetValue(dtNew.Columns.IndexOf("DMA")) != null ? reader.GetValue(dtNew.Columns.IndexOf("DMA")).ToString() : "";
                                        objProjectExcel.tRED = reader.GetValue(dtNew.Columns.IndexOf("RED")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RED")).ToString() : "";
                                        objProjectExcel.tCM = reader.GetValue(dtNew.Columns.IndexOf("CM")) != null ? reader.GetValue(dtNew.Columns.IndexOf("CM")).ToString() : "";
                                        objProjectExcel.tANE = reader.GetValue(dtNew.Columns.IndexOf("A&E")) != null ? reader.GetValue(dtNew.Columns.IndexOf("A&E")).ToString() : "";
                                        objProjectExcel.tRVP = reader.GetValue(dtNew.Columns.IndexOf("RVP")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RVP")).ToString() : "";
                                        objProjectExcel.tPrincipalPartner = reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")).ToString() : "";
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Status")).ToString() != "")
                                            objProjectExcel.dStatus = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Status")));
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Open Store")) != null && reader.GetValue(dtNew.Columns.IndexOf("Open Store")).ToString() != "")
                                        objProjectExcel.dOpenStore = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Open Store")));//default value

                                        objProjectExcel.tProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Project Status")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Status")).ToString() : "";

                                        objProjectExcel.nNumberOfTabletsPerStore = reader.GetValue(dtNew.Columns.IndexOf("# of tablets per store")) != null && reader.GetValue(dtNew.Columns.IndexOf("# of tablets per store")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("# of tablets per store"))) : 0;
                                        objProjectExcel.tEquipmentVendor = reader.GetValue(dtNew.Columns.IndexOf("Equipment Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Equipment Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Equipment Vendor"))) : "";
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Ship Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Ship Date")).ToString() != "")
                                            objProjectExcel.dShipDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Ship Date")));
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                                            objProjectExcel.dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                                        if (reader.GetValue(dtNew.Columns.IndexOf("Scheduled Install Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Scheduled Install Date")).ToString() != "")
                                            objProjectExcel.dInstallDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Scheduled Install Date")));
                                        objProjectExcel.tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                                        objProjectExcel.tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                                        

                                        fields.Add(objProjectExcel);
                                    }
                                }
                            } while (reader.NextResult());

                            // 2. Use the AsDataSet extension method
                            //  var result = reader.AsDataSet();

                            // The result of each spreadsheet is in result.Tables
                        }
                    }

                }
                catch (Exception ex)
                {
                    TraceUtility.ForceWriteException("ImportExceltoDatabase", HttpContext.Current, ex);
                    //result = false;
                }
                finally
                {
                    //oledbConn.Close();
                }
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ImportExceltoDatabase2", HttpContext.Current, ex);
            }
            // return ip;
            // return result;
        }
        void ImportExceltoDatabaseSonic(List<ProjectExcelFieldsSonic> fields, string strFilePath, int nBrandId)
        {
            //  

            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                ProjectExcelFieldsSonic objProjectExcel = new ProjectExcelFieldsSonic();
                DataTable dt = new DataTable();
                try
                {
                    DataTable dtNew = new DataTable();
                    // oledbConn.Open();
                    using (var stream = File.Open(strFilePath, FileMode.Open, FileAccess.Read))
                    {
                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Choose one of either 1 or 2:

                            // 1. Use the reader methods
                            do
                            {
                                reader.Read();
                                int ColumnCount = reader.FieldCount;
                                for (int i = 0; i < ColumnCount; i++)
                                {
                                    string ColumnName = reader.GetValue(i).ToString();
                                    if (!dtNew.Columns.Contains(ColumnName))
                                    { dtNew.Columns.Add(ColumnName); }
                                }
                                while (reader.Read())
                                {

                                    // reader.GetDouble(0);
                                    string storeNumber = reader.GetValue(dtNew.Columns.IndexOf("Store Number")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Store Number")).ToString() : "";
                                    string projectType = reader.GetValue(dtNew.Columns.IndexOf("Project Type")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Type")).ToString() : "";

                                    if (projectType != "" && storeNumber != "")
                                    {
                                        SqlParameter tModuleNameParam = new SqlParameter("@tStoreNumber", storeNumber);
                                        var output = db.Database.SqlQuery<string>("Select tstoreNumber from tblstore with (nolock) where tstoreNumber= @tStoreNumber  and nBrandID=@nBrandId ", new SqlParameter("@tStoreNumber", storeNumber), new SqlParameter("@nBrandId", nBrandId)).FirstOrDefault();
                                        objProjectExcel = new ProjectExcelFieldsSonic();

                                        if (storeNumber != output)
                                            objProjectExcel.nStoreExistStatus = 0;
                                        else
                                            objProjectExcel.nStoreExistStatus = 1;

                                        objProjectExcel.tProjectType = projectType;
                                        objProjectExcel.tStoreNumber = storeNumber;
                                        objProjectExcel.tAddress = reader.GetValue(dtNew.Columns.IndexOf("Address")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Address")).ToString() : "";
                                        objProjectExcel.tCity = reader.GetValue(dtNew.Columns.IndexOf("City")) != null ? reader.GetValue(dtNew.Columns.IndexOf("City")).ToString() : "";
                                        objProjectExcel.tState = reader.GetValue(dtNew.Columns.IndexOf("State")) != null ? reader.GetValue(dtNew.Columns.IndexOf("State")).ToString() : "";
                                        objProjectExcel.nDMAID = reader.GetValue(dtNew.Columns.IndexOf("DMA ID")) != null && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DMA ID"))) : 0;
                                        objProjectExcel.tDMA = reader.GetValue(dtNew.Columns.IndexOf("DMA")) != null ? reader.GetValue(dtNew.Columns.IndexOf("DMA")).ToString() : "";
                                        objProjectExcel.tRED = reader.GetValue(dtNew.Columns.IndexOf("RED")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RED")).ToString() : "";
                                        objProjectExcel.tCM = reader.GetValue(dtNew.Columns.IndexOf("CM")) != null ? reader.GetValue(dtNew.Columns.IndexOf("CM")).ToString() : "";
                                        objProjectExcel.tANE = reader.GetValue(dtNew.Columns.IndexOf("A&E")) != null ? reader.GetValue(dtNew.Columns.IndexOf("A&E")).ToString() : "";
                                        objProjectExcel.tRVP = reader.GetValue(dtNew.Columns.IndexOf("RVP")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RVP")).ToString() : "";
                                        objProjectExcel.tPrincipalPartner = reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")).ToString() : "";
                                        objProjectExcel.dStatus = reader.GetValue(dtNew.Columns.IndexOf("Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Status")).ToString() != "" ? Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Status"))) : new DateTime(2001, 1, 1); //default value
                                        objProjectExcel.dOpenStore = reader.GetValue(dtNew.Columns.IndexOf("Open Store")) != null && reader.GetValue(dtNew.Columns.IndexOf("Open Store")).ToString() != "" ? Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Open Store"))) : new DateTime(2001, 1, 1);//default value

                                        objProjectExcel.tProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Project Status")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Status")).ToString() : "";


                                        fields.Add(objProjectExcel);
                                    }
                                }
                            } while (reader.NextResult());

                            // 2. Use the AsDataSet extension method
                            //  var result = reader.AsDataSet();

                            // The result of each spreadsheet is in result.Tables
                        }
                    }

                }
                catch (Exception ex)
                {
                    TraceUtility.ForceWriteException("ImportExceltoDatabase", HttpContext.Current, ex);
                    //result = false;
                }
                finally
                {
                    //oledbConn.Close();
                }
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ImportExceltoDatabase2", HttpContext.Current, ex);
            }
            // return ip;
            // return result;
        }
        [HttpPost]
        [Route("api/Attachment/UpdateAttachment")]
        public IHttpActionResult UpdateAttachment()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                var attachment = new Attachment
                {
                    AttachmentId = Convert.ToInt32(httpRequest.Form["AttachmentId"]),
                    FileName = httpRequest.Form["tfileName"],
                    FileExt = httpRequest.Form["tFileExt"],
                    FileType = httpRequest.Form["tfileType"],
                    AttachmentType = httpRequest.Form["tAttachmentType"],
                    AttachmentComments = httpRequest.Form["tAttachmentComments"],
                    AttachmentUrl = httpRequest.Form["tAttachmentURL"],
                    UpdateBy = Convert.ToInt32(httpRequest.Form["UpdateBy"])
                };

                using (var stream = new MemoryStream())
                {
                    httpRequest.Files[0].InputStream.CopyTo(stream);
                    attachment.AttachmentBlob = stream.ToArray();
                }

                _attachmentRepository.UpdateAttachment(attachment);

                return Ok(new { Message = "Attachment updated successfully." });
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("UpdateAttachment", HttpContext.Current, ex);
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("api/Attachment/DeleteAttachment/{attachmentId}")]
        public IHttpActionResult DeleteAttachment([FromBody] dynamic attachmentId)
        {
            try
            {
                int nattachmentid = attachmentId.ID;
                var userId = 1; // replace with actual user ID

                _attachmentRepository.DeleteAttachment(nattachmentid, userId);

                return Ok(new { Message = "Attachment deleted successfully." });
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("DeleteAttachment", HttpContext.Current, ex);
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("api/Attachment/GetAttachment")]
        public IHttpActionResult GetAttachment([FromBody] Attachment attachment)
        {
            try
            {
                var attachments = _attachmentRepository.GetAttachments(attachment.AttachmentId, 1, attachment.FileName, attachment.nPageSize, attachment.nPageNumber);
                return Ok(attachments);
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("GetAttachment", HttpContext.Current, ex);
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("api/Attachment/GetAttachmentBlob/{attachmentId}/{userId}")]
        public IHttpActionResult GetAttachmentBlob([FromBody] dynamic attachmentId)
        {
            try
            {
                int nattachmentid = attachmentId.ID;
                int userId = 1;
                Attachment attachment = _attachmentRepository.GetAttachmentBlob(nattachmentid, userId);

                if (attachment == null)
                {
                    return NotFound();
                }

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(attachment.AttachmentBlob);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = attachment.FileName;
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(attachment.FileType);

                return new ResponseMessageResult(response);
            }
            catch (SqlException ex)
            {
                TraceUtility.ForceWriteException("GetAttachmentBlob", HttpContext.Current, ex);
                // log error message
                return InternalServerError(ex);
            }
            catch (Exception ex)
            {
                // log error message
                return InternalServerError(ex);
            }
        }
    }

}
