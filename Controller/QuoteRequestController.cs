using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class QuoteRequestController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpGet]
        [ActionName("GetAllTemplate")]
        public HttpResponseMessage GetAllTemplate(int nBrandId)
        {
            Dictionary<int, string> lstQuoteRequest = new Dictionary<int, string>();
            lstQuoteRequest.Add(1, "Audio");
            lstQuoteRequest.Add(2, "Networking");
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Dictionary<int, string>>(lstQuoteRequest, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetTemplate(int nTemplateId)
        {
            try
            {
                QuoteRequestTemplate templateObj = new QuoteRequestTemplate();
                templateObj.aQuoteRequestTemplateId = nTemplateId;
                templateObj.tTemplateName = "New";
                templateObj.nBrandId = 1;
                templateObj.quoteRequestTechComps = new List<QuoteRequestTechComp>()
                {
                    new QuoteRequestTechComp()
                    {
                        tTechCompName = "POS",
                        fields = new List<QuoteRequestTechCompField>()
                        {
                            new QuoteRequestTechCompField()
                            {
                                nQuoteRequestTemplateId= nTemplateId,
                                tTechCompField = "nStatus",
                                tTechCompFieldName = "nStatus"
                            }
                        }
                    }
                };
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<QuoteRequestTemplate>(templateObj, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage CreateUpdateTemplate(QuoteRequestTemplate quoteRequest)
        {
            try
            {
                QuoteRequestTemplate templateObj = new QuoteRequestTemplate();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<QuoteRequestTemplate>(templateObj, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpGet]
        public string Delete(int nTemplateId)
        {            
            return "";
        }
    }
}
