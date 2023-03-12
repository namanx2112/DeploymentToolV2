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

namespace DeploymentTool.Controller
{


    public class AttachmentController : ApiController
    {
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
