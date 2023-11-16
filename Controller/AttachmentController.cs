using DeploymentTool.Helpers;
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
        [Route("api/Attachment/ImportItems")]
        public async Task<HttpResponseMessage> ImportItems()
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
                    IImportables tObject = null;
                    List<IImportModel> tList = null;
                    string ItemName = HttpContext.Current.Request.QueryString["objectName"];
                    switch (ItemName)
                    {
                        case "VendorParts":
                            tObject = new VendorPartsImportable();
                            break;
                    }
                    int nBrandId = 0;
                    int nInstanceId = 0;
                    var filesReadToProvider = await request.Content.ReadAsMultipartAsync();
                    byte[] fileBytes = null;
                    string FileName = string.Empty;
                    foreach (var stream in filesReadToProvider.Contents)
                    {
                        string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                        if (fRequest.Length >= 2)
                        {
                            FileName = fRequest[0];
                            nBrandId = Convert.ToInt32(fRequest[1]);
                            if (fRequest.Length == 3)
                                nInstanceId = Convert.ToInt32(fRequest[2]);
                            fileBytes = await stream.ReadAsByteArrayAsync();
                            break;
                        }
                    }
                    string URL = HttpRuntime.AppDomainAppPath;
                    string strFilePath = URL + @"Attachments\" + FileName;

                    TraceUtility.WriteTrace("AttachmentController", "UploadStore:strFilePath:" + strFilePath);
                    using (System.IO.BinaryWriter bw = new BinaryWriter(File.Open(strFilePath, FileMode.Create)))
                    {
                        bw.Write(fileBytes);
                        bw.Close();
                    }
                    TraceUtility.WriteTrace("AttachmentController", "UploadStore:Written:" + strFilePath);
                    tList = tObject.Import(strFilePath, nBrandId, nInstanceId);
                    TraceUtility.WriteTrace("AttachmentController", "UploadStore:Returing");
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ObjectContent<List<IImportModel>>(tList, new JsonMediaTypeFormatter())
                    };
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
                    int nProjectType = 0;
                    foreach (var stream in filesReadToProvider.Contents)
                    {
                        string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                        if (fRequest.Length > 2)
                        {
                            string FileName = fRequest[0];
                            nBrandIdTemp = Convert.ToInt32(fRequest[1]);
                            nProjectType = Convert.ToInt32(fRequest[2]);
                            break;
                        }
                    }
                    var output = db.Database.SqlQuery<string>("select top 1 tBrandName from tblBrand with (nolock) where aBrandId=@aBrandId ", new SqlParameter("@aBrandId", nBrandIdTemp)).FirstOrDefault();

                    //if (output.Contains("Buffalo Wild Wings"))
                    //{
                    //    List<ProjectExcelFields> fields = new List<ProjectExcelFields>();


                    //    foreach (var stream in filesReadToProvider.Contents)
                    //    {
                    //        string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                    //        if (fRequest.Length > 2)
                    //        {
                    //            string FileName = fRequest[0];
                    //            int nBrandId = Convert.ToInt32(fRequest[1]);                                
                    //            var fileBytes = await stream.ReadAsByteArrayAsync();
                    //            string URL = HttpRuntime.AppDomainAppPath;

                    //            string strFilePath = URL + @"Attachments\" + FileName;

                    //            TraceUtility.WriteTrace("AttachmentController", "UploadStore:strFilePath:" + strFilePath);
                    //            using (System.IO.BinaryWriter bw = new BinaryWriter(File.Open(strFilePath, FileMode.Create)))
                    //            {
                    //                bw.Write(fileBytes);
                    //                bw.Close();
                    //            }

                    //            TraceUtility.WriteTrace("AttachmentController", "UploadStore:Written:" + strFilePath);
                    //            ImportExceltoDatabase(fields, strFilePath, nBrandId);
                    //            /* fields.Add(new ProjectExcelFields() {
                    //                 tProjectType = "RELOCATION",
                    //                 tStoreNumber = "6937",
                    //                 tAddress = "461 Columbia Ave",
                    //                 tCity = "Lexington",
                    //                 tState = "SC",
                    //                 nDMAID = 546,
                    //                 tDMA = "COLUMBIA SC",
                    //                 tRED = "Michael Landru",
                    //                 tCM = "Kevin Dalpiaz",
                    //                 tANE = "",
                    //                 tRVP = "Linda Wiseley",
                    //                 tPrincipalPartner = "MICHAEL IRONS",
                    //                 dStatus = DateTime.Now,
                    //                 dOpenStore = DateTime.Now,
                    //                 tProjectStatus = "Under Construction"
                    //             });*/

                    //        }
                    //    }
                    //    TraceUtility.WriteTrace("AttachmentController", "UploadStore:Returing");
                    //    return new HttpResponseMessage(HttpStatusCode.OK)
                    //    {
                    //        Content = new ObjectContent<List<ProjectExcelFields>>(fields, new JsonMediaTypeFormatter())
                    //    };
                    //}
                    //else
                    {

                        if (nProjectType == 12)//12,'Order Accuracy Installation'
                        {
                            List<ProjectExcelFieldsOrderAccurcy> fields = new List<ProjectExcelFieldsOrderAccurcy>();


                            foreach (var stream in filesReadToProvider.Contents)
                            {
                                string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                                if (fRequest.Length > 2)
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
                                    ImportExceltoDatabaseOrderAccuracy(fields, strFilePath, nBrandId);                                    

                                }
                            }
                            TraceUtility.WriteTrace("AttachmentController", "UploadStore:Returing");
                            return new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new ObjectContent<List<ProjectExcelFieldsOrderAccurcy>>(fields, new JsonMediaTypeFormatter())
                            };
                        }
                        else if (nProjectType == 13)//(13,'Order Status Board Installation'
                        {
                            List<ProjectExcelFieldsOrderStatusBoard> fields = new List<ProjectExcelFieldsOrderStatusBoard>();
                            foreach (var stream in filesReadToProvider.Contents)
                            {
                                string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                                if (fRequest.Length > 2)
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
                                    ImportExceltoDatabaseOrderStatusBoard(fields, strFilePath, nBrandId);                                   

                                }
                            }
                            TraceUtility.WriteTrace("AttachmentController", "UploadStore:Returing");
                            return new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new ObjectContent<List<ProjectExcelFieldsOrderStatusBoard>>(fields, new JsonMediaTypeFormatter())
                            };
                        }
                        else if (nProjectType == 14)//14,'Arbys HP Rollout Installation'
                        {
                            List<ProjectExcelFieldsHPRollout> fields = new List<ProjectExcelFieldsHPRollout>();


                            foreach (var stream in filesReadToProvider.Contents)
                            {
                                string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                                if (fRequest.Length > 2)
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
                                    ImportExceltoDatabaseHPRollout(fields, strFilePath, nBrandId);

                                }
                            }
                            TraceUtility.WriteTrace("AttachmentController", "UploadStore:Returing");
                            return new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new ObjectContent<List<ProjectExcelFieldsHPRollout>>(fields, new JsonMediaTypeFormatter())
                            };
                        }
                        else if (nProjectType == 10)//10,'Server Handheld'
                        {
                            List<ProjectExcelFieldsServerHandheld> fields = new List<ProjectExcelFieldsServerHandheld>();


                            foreach (var stream in filesReadToProvider.Contents)
                            {
                                string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                                if (fRequest.Length > 2)
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
                                    ImportExceltoDatabaseServerHandheld(fields, strFilePath, nBrandId);

                                }
                            }
                            TraceUtility.WriteTrace("AttachmentController", "UploadStore:Returing");
                            return new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new ObjectContent<List<ProjectExcelFieldsServerHandheld>>(fields, new JsonMediaTypeFormatter())
                            };
                        }
                        else 
                        {
                            List<ProjectExcelFieldsSonic> fields = new List<ProjectExcelFieldsSonic>();

                            foreach (var stream in filesReadToProvider.Contents)
                            {
                                string[] fRequest = stream.Headers.ContentDisposition.FileName.Replace("\"", "").Split((char)1000);
                                if (fRequest.Length > 2)
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
                                    

                                }
                            }
                            TraceUtility.WriteTrace("AttachmentController", "UploadStore:Returing");
                            return new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new ObjectContent<List<ProjectExcelFieldsSonic>>(fields, new JsonMediaTypeFormatter())
                            };
                        }
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
        void ImportExceltoDatabaseOrderAccuracy(List<ProjectExcelFieldsOrderAccurcy> fields, string strFilePath, int nBrandId)
        {
            //  

            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                ProjectExcelFieldsOrderAccurcy objProjectExcel = new ProjectExcelFieldsOrderAccurcy();
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
                                         objProjectExcel = new ProjectExcelFieldsOrderAccurcy();

                                        var output = db.Database.SqlQuery<int>("(select count(*) from tblProject with (nolock) where nstoreid in(Select aStoreID from tblstore with (nolock) where tstoreNumber= @tStoreNumber  and nBrandID=@nBrandId))  ", new SqlParameter("@tStoreNumber", storeNumber), new SqlParameter("@nBrandId", nBrandId)).FirstOrDefault();

                                        if (output == 0)
                                            objProjectExcel.nStoreExistStatus = 0;
                                        else if (output == 1)
                                            objProjectExcel.nStoreExistStatus = 1;
                                        else
                                            objProjectExcel.nStoreExistStatus = 2;

                                        if (objProjectExcel.nStoreExistStatus != 2)
                                        {

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

                                            objProjectExcel.tOrderAccuracyVendor = reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Vendor"))) : "";
                                            objProjectExcel.tOrderAccuracyStatus = reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Status"))) : "";
                                            objProjectExcel.nBakeryPrinter = reader.GetValue(dtNew.Columns.IndexOf("Bakery Printer")) != null && reader.GetValue(dtNew.Columns.IndexOf("Bakery Printer")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Bakery Printer"))) : 0;
                                            objProjectExcel.nDualCupLabel = reader.GetValue(dtNew.Columns.IndexOf("Dual Cup Label")) != null && reader.GetValue(dtNew.Columns.IndexOf("Dual Cup Label")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Dual Cup Label"))) : 0;
                                            objProjectExcel.nDTExpo = reader.GetValue(dtNew.Columns.IndexOf("DT Expo")) != null && reader.GetValue(dtNew.Columns.IndexOf("DT Expo")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DT Expo"))) : 0;
                                            objProjectExcel.nFCExpo = reader.GetValue(dtNew.Columns.IndexOf("FC Expo")) != null && reader.GetValue(dtNew.Columns.IndexOf("FC Expo")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("FC Expo"))) : 0;


                                            if (reader.GetValue(dtNew.Columns.IndexOf("Ship Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Ship Date")).ToString() != "")
                                                objProjectExcel.dShipDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Ship Date")));

                                            objProjectExcel.tShippingCarrier = reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier"))) : "";
                                            objProjectExcel.tTrackingNumber = reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Tracking Number"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")).ToString() != "")
                                                objProjectExcel.dDeliveryDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")));




                                            objProjectExcel.tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                                            objProjectExcel.tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Install Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Date")).ToString() != "")
                                                objProjectExcel.dInstallDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install Date")));

                                            objProjectExcel.tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                                            objProjectExcel.tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                                            objProjectExcel.tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                                            objProjectExcel.tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                                            objProjectExcel.tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                                            objProjectExcel.tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                                            objProjectExcel.tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                                            if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                                                objProjectExcel.dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                                            objProjectExcel.tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                                            objProjectExcel.tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                                            objProjectExcel.tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                                                objProjectExcel.dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                                            objProjectExcel.tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                                            objProjectExcel.tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                                            objProjectExcel.tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";

                                            fields.Add(objProjectExcel);
                                        }
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
        void ImportExceltoDatabaseOrderStatusBoard(List<ProjectExcelFieldsOrderStatusBoard> fields, string strFilePath, int nBrandId)
        {
            //  

            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                ProjectExcelFieldsOrderStatusBoard objProjectExcel = new ProjectExcelFieldsOrderStatusBoard();
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
                                        objProjectExcel = new ProjectExcelFieldsOrderStatusBoard();

                                        var output = db.Database.SqlQuery<int>("(select count(*) from tblProject with (nolock) where nstoreid in(Select aStoreID from tblstore with (nolock) where tstoreNumber= @tStoreNumber  and nBrandID=@nBrandId))  ", new SqlParameter("@tStoreNumber", storeNumber), new SqlParameter("@nBrandId", nBrandId)).FirstOrDefault();

                                        if (output == 0)
                                            objProjectExcel.nStoreExistStatus = 0;
                                        else if (output == 1)
                                            objProjectExcel.nStoreExistStatus = 1;
                                        else
                                            objProjectExcel.nStoreExistStatus = 2;

                                        if (objProjectExcel.nStoreExistStatus != 2)
                                        {

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


                                            objProjectExcel.tOrderStatusBoardVendor = reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Vendor"))) : "";
                                            objProjectExcel.tOrderStatusBoardStatus = reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Status"))) : "";
                                            objProjectExcel.nOSB = reader.GetValue(dtNew.Columns.IndexOf("OSB")) != null && reader.GetValue(dtNew.Columns.IndexOf("OSB")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("OSB"))) : 0;

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Ship Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Ship Date")).ToString() != "")
                                                objProjectExcel.dShipDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Ship Date")));

                                            objProjectExcel.tShippingCarrier = reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier"))) : "";
                                            objProjectExcel.tTrackingNumber = reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Tracking Number"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")).ToString() != "")
                                                objProjectExcel.dDeliveryDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")));




                                            objProjectExcel.tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                                            objProjectExcel.tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Install Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Date")).ToString() != "")
                                                objProjectExcel.dInstallDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install Date")));

                                            objProjectExcel.tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                                            objProjectExcel.tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                                            objProjectExcel.tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                                            objProjectExcel.tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                                            objProjectExcel.tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                                            objProjectExcel.tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                                            objProjectExcel.tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                                            if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                                                objProjectExcel.dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                                            objProjectExcel.tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                                            objProjectExcel.tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                                            objProjectExcel.tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                                                objProjectExcel.dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                                            objProjectExcel.tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                                            objProjectExcel.tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                                            objProjectExcel.tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";


                                            fields.Add(objProjectExcel);
                                        }
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

        void ImportExceltoDatabaseHPRollout(List<ProjectExcelFieldsHPRollout> fields, string strFilePath, int nBrandId)
        {
            //  

            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                ProjectExcelFieldsHPRollout objProjectExcel = new ProjectExcelFieldsHPRollout();
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
                                        objProjectExcel = new ProjectExcelFieldsHPRollout();
                                        var output = db.Database.SqlQuery<int>("(select count(*) from tblProject with (nolock) where nstoreid in(Select aStoreID from tblstore with (nolock) where tstoreNumber= @tStoreNumber  and nBrandID=@nBrandId))  ", new SqlParameter("@tStoreNumber", storeNumber), new SqlParameter("@nBrandId", nBrandId)).FirstOrDefault();
                                      
                                        if (output == 0)
                                            objProjectExcel.nStoreExistStatus = 0;
                                        else if (output == 1)
                                            objProjectExcel.nStoreExistStatus = 1;
                                        else
                                            objProjectExcel.nStoreExistStatus = 2;

                                        if (objProjectExcel.nStoreExistStatus != 2)
                                        {

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


                                            objProjectExcel.tNetworkSwitchVendor = reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor"))) : "";
                                            objProjectExcel.tNetworkSwitchStatus = reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status"))) : "";
                                            objProjectExcel.tShipmenttoVendor = reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor"))) : "";
                                            objProjectExcel.tSetupStatus = reader.GetValue(dtNew.Columns.IndexOf("Setup Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Setup Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Setup Status"))) : "";
                                            objProjectExcel.tNewSerialNumber = reader.GetValue(dtNew.Columns.IndexOf("New Serial Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("New Serial Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("New Serial Number"))) : "";
                                            objProjectExcel.tOldSerialNumber = reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number"))) : "";
                                            objProjectExcel.tOldSwitchReturnStatus = reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status"))) : "";
                                            objProjectExcel.tOldSwitchTracking = reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking"))) : "";

                                            /////////////
                                            objProjectExcel.tImageMemoryVendor = reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor"))) : "";
                                            objProjectExcel.tImageMemoryStatus = reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status"))) : "";
                                            objProjectExcel.tShipmentTracking = reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking"))) : "";
                                            objProjectExcel.tReturnShipment = reader.GetValue(dtNew.Columns.IndexOf("Return Shipment")) != null && reader.GetValue(dtNew.Columns.IndexOf("Return Shipment")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Return Shipment"))) : "";
                                            objProjectExcel.tReturnShipmentTracking = reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking"))) : "";



                                            objProjectExcel.tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                                            objProjectExcel.tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Install Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Date")).ToString() != "")
                                                objProjectExcel.dInstallDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install Date")));

                                            objProjectExcel.tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                                            objProjectExcel.tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                                            objProjectExcel.tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                                            objProjectExcel.tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                                            objProjectExcel.tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                                            objProjectExcel.tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                                            objProjectExcel.tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                                            if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                                                objProjectExcel.dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                                            objProjectExcel.tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                                            objProjectExcel.tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                                            objProjectExcel.tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                                                objProjectExcel.dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                                            objProjectExcel.tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                                            objProjectExcel.tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                                            objProjectExcel.tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";


                                            fields.Add(objProjectExcel);
                                        }
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

        void ImportExceltoDatabaseServerHandheld(List<ProjectExcelFieldsServerHandheld> fields, string strFilePath, int nBrandId)
        {
            //  

            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                ProjectExcelFieldsServerHandheld objProjectExcel = new ProjectExcelFieldsServerHandheld();
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
                                        var output = db.Database.SqlQuery<int>("(select count(*) from tblProject with (nolock) where nstoreid in(Select aStoreID from tblstore with (nolock) where tstoreNumber= @tStoreNumber  and nBrandID=@nBrandId))  ", new SqlParameter("@tStoreNumber", storeNumber), new SqlParameter("@nBrandId", nBrandId)).FirstOrDefault();
                                        objProjectExcel = new ProjectExcelFieldsServerHandheld();

                                        if (output==0)
                                            objProjectExcel.nStoreExistStatus = 0;
                                        else if (output == 1)
                                            objProjectExcel.nStoreExistStatus = 1;
                                        else
                                            objProjectExcel.nStoreExistStatus = 2;

                                        if (objProjectExcel.nStoreExistStatus != 2)
                                        {
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


                                            objProjectExcel.tServerHandheldVendor = reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Vendor"))) : "";
                                            objProjectExcel.tServerHandheldStatus = reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Status"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Ship Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Ship Date")).ToString() != "")
                                                objProjectExcel.dShipDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Ship Date")));

                                            objProjectExcel.tShippingCarrier = reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier"))) : "";
                                            objProjectExcel.tTrackingNumber = reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Tracking Number"))) : "";

                                            objProjectExcel.nTablets = reader.GetValue(dtNew.Columns.IndexOf("Tablets")) != null && reader.GetValue(dtNew.Columns.IndexOf("Tablets")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Tablets"))) : 0;
                                            objProjectExcel.nFiveBayharger = reader.GetValue(dtNew.Columns.IndexOf("5 Bay Charger")) != null && reader.GetValue(dtNew.Columns.IndexOf("5 Bay Charger")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("5 Bay Charger"))) : 0;
                                            objProjectExcel.nShoulderStrap = reader.GetValue(dtNew.Columns.IndexOf("Shoulder Strap")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shoulder Strap")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Shoulder Strap"))) : 0;
                                            objProjectExcel.nProtectiveCase = reader.GetValue(dtNew.Columns.IndexOf("Protective Case")) != null && reader.GetValue(dtNew.Columns.IndexOf("Protective Case")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Protective Case"))) : 0;

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")).ToString() != "")
                                                objProjectExcel.dDeliveryDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")));

                                            objProjectExcel.tServerHandheldCost = reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Cost"))) : "";



                                            objProjectExcel.tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                                            objProjectExcel.tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Install Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Date")).ToString() != "")
                                                objProjectExcel.dInstallDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install Date")));

                                            objProjectExcel.tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                                            objProjectExcel.tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                                            objProjectExcel.tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                                            objProjectExcel.tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                                            objProjectExcel.tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                                            objProjectExcel.tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                                            objProjectExcel.tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                                            if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                                                objProjectExcel.dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                                            objProjectExcel.tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                                            objProjectExcel.tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                                            objProjectExcel.tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                            if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                                                objProjectExcel.dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                                            objProjectExcel.tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                                            objProjectExcel.tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                                            objProjectExcel.tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";


                                            fields.Add(objProjectExcel);
                                        }
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
