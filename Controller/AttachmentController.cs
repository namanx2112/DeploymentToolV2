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
                    List<ProjectExcelFields> fields = new List<ProjectExcelFields>();
                    var filesReadToProvider = await request.Content.ReadAsMultipartAsync();

                    foreach (var stream in filesReadToProvider.Contents)
                    {
                        string FileName = stream.Headers.ContentDisposition.FileName.Replace("\"","");
                        var fileBytes = await stream.ReadAsByteArrayAsync();
                        string URL = HttpRuntime.AppDomainAppPath;
                      
                        string strFilePath = URL+@"Attachments\" + FileName;
                        //string strFilePath = "C:\\Code\\namanx2112\\new\\Attachments\\store.xlsx";
                        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                        TraceUtility.WriteTrace("AttachmentController", "UploadStore:strFilePath:" + strFilePath);
                        //using (System.IO.BinaryWriter bw = new BinaryWriter(File.Open(@"../Attachments/" + FileName, FileMode.Create)))
                        using (System.IO.BinaryWriter bw = new BinaryWriter(File.Open(strFilePath, FileMode.Create)))
                        {
                            bw.Write(fileBytes);
                            bw.Close();
                        }

                        TraceUtility.WriteTrace("AttachmentController", "UploadStore:Written:" + strFilePath);
                        ImportExceltoDatabase(fields,strFilePath, connString);
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
                         });
                         fields.Add(new ProjectExcelFields()
                         {
                             tProjectType = "New",
                             tStoreNumber = "6937",
                             tAddress = "108 N Lincoln Dr (temp)",
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
                         });
                         fields.Add(new ProjectExcelFields()
                         {
                             tProjectType = "RELOCATION",
                             tStoreNumber = "5345",
                             tAddress = "524 TRIMBLE PLAZA SOUTHEAST",
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
                         });
                         fields.Add(new ProjectExcelFields()
                         {
                             tProjectType = "RELOCATION",
                             tStoreNumber = "54353",
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
                         });
                         fields.Add(new ProjectExcelFields()
                         {
                             tProjectType = "REBUILD",
                             tStoreNumber = "3424",
                             tAddress = "401 S. POPLAR STREET",
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
                         });
                         fields.Add(new ProjectExcelFields()
                         {
                             tProjectType = "RELOCATION",
                             tStoreNumber = "34534",
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
                         }); */
                    }
                    TraceUtility.WriteTrace("AttachmentController", "UploadStore:Returing");
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ObjectContent<List<ProjectExcelFields>>(fields, new JsonMediaTypeFormatter())
                    };
                }
                catch (System.Exception ex)
                {
                    TraceUtility.WriteTrace("AttachmentController", "UploadStore:Exception:" + ex.Message);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        void ImportExceltoDatabase(List<ProjectExcelFields> fields, string strFilePath, string connString)
        {
          //  

            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                ProjectExcelFields objProjectExcel = new ProjectExcelFields();

                OleDbConnection oledbConn = new OleDbConnection(connString);
                DataTable dt = new DataTable();
                try
                {
                    oledbConn.Open();
                    TraceUtility.WriteTrace("AttachmentController", "ImportExceltoDatabase:Opened");
                    using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                    {
                        OleDbDataAdapter oleda = new OleDbDataAdapter();
                        oleda.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        oleda.Fill(ds);
                        dt = ds.Tables[0];
                        TraceUtility.WriteTrace("AttachmentController", "ImportExceltoDatabase:dt.Rows.Count:" + dt.Rows.Count);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                string Name = row["Store Number"] != null ? row["Store Number"].ToString() : "";
                                string ProjectType =row["Project Type"] != null ? row["Project Type"].ToString() : "";

                                SqlParameter tModuleNameParam = new SqlParameter("@tStoreNumber", Name);
                                var output = db.Database.SqlQuery<string>("Select tstoreNumber from tblstore with (nolock) where tstoreNumber= @tStoreNumber", new SqlParameter("@tStoreNumber", Name)).FirstOrDefault();

                                if (ProjectType != "")
                                {
                                    objProjectExcel = new ProjectExcelFields();

                                    if (Name != output)
                                        objProjectExcel.nStoreExistStatus = 0;
                                    else
                                        objProjectExcel.nStoreExistStatus = 1;

                                    objProjectExcel.tProjectType = ProjectType;
                                    objProjectExcel.tStoreNumber = Name;
                                    objProjectExcel.tAddress = row["Address"] != null ? row["Address"].ToString() : "";
                                    objProjectExcel.tCity = row["City"] != null ? row["City"].ToString() : "";
                                    objProjectExcel.tState = row["State"] != null ? row["State"].ToString() : "";
                                    objProjectExcel.nDMAID = row["DMA ID"] != null && row["DMA ID"].ToString() != "" ? Convert.ToInt32(row["DMA ID"]) : 0;
                                    objProjectExcel.tDMA = row["DMA"] != null ? row["DMA"].ToString() : "";
                                    objProjectExcel.tRED = row["RED"] != null ? row["RED"].ToString() : "";
                                    objProjectExcel.tCM = row["CM"] != null ? row["CM"].ToString() : "";
                                    objProjectExcel.tANE = row["A&E"] != null ? row["A&E"].ToString() : "";
                                    objProjectExcel.tRVP = row["RVP"] != null ? row["RVP"].ToString() : "";
                                    objProjectExcel.tPrincipalPartner = row["Principal Partner"] != null ? row["Principal Partner"].ToString() : "";
                                    objProjectExcel.dStatus = row["Status"] != null && row["Status"].ToString() != "" ? Convert.ToDateTime(row["Status"]) : new DateTime(2001, 1, 1); //default value
                                    objProjectExcel.dOpenStore = row["Open Store"] != null && row["Open Store"].ToString() != "" ? Convert.ToDateTime(row["Open Store"]) : new DateTime(2001, 1, 1);//default value

                                    objProjectExcel.tProjectStatus = row["Project Status"] != null ? row["Project Status"].ToString() : "";


                                    fields.Add(objProjectExcel);
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TraceUtility.WriteTrace("AttachmentController", "ImportExceltoDatabase:Exception:"+ ex.Message);
                    //result = false;
                }
                finally
                {
                    oledbConn.Close();
                }
            }
            catch (Exception ex)
            {

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
                    AttachmentId = Convert.ToInt32(httpRequest.Form["AttachmentId"]) ,
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
