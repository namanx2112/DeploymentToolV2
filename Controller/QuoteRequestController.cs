using DeploymentTool.Model;
using DeploymentTool.Model.Templates;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;

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
            SqlParameter tModuleNameParam = new SqlParameter("@nBrandId", nBrandId);
            List<QuoteRequestTemplateTemp> items = db.Database.SqlQuery<QuoteRequestTemplateTemp>("exec sproc_GetAllQuoteRequestTemplate @nBrandId", tModuleNameParam).ToList();
            foreach (var RequestTechComps in items)
            {

                lstQuoteRequest.Add(RequestTechComps.aQuoteRequestTemplateId, RequestTechComps.tTemplateName);
                //lstQuoteRequest.Add(2, "Networking");
            }
            //lstQuoteRequest.Add(1, "Audio");
           // lstQuoteRequest.Add(2, "Networking");
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
                if (nTemplateId == 0)
                {
                    templateObj.aQuoteRequestTemplateId = nTemplateId;
                    templateObj.tTemplateName = "New";
                    templateObj.nBrandId = 1;
                    templateObj.quoteRequestTechComps = new List<QuoteRequestTechComp>()
                    {
                        new QuoteRequestTechComp()
                        {
                            tTechCompName = "POS",
                            tTableName="tblProjectPOS",
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
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@aQuoteRequestTemplateId", nTemplateId);
                    List<QuoteRequestTemplateTemp> items = db.Database.SqlQuery<QuoteRequestTemplateTemp>("exec sproc_GetQuoteRequestTemplate @aQuoteRequestTemplateId", tModuleNameParam).ToList();

                    templateObj.aQuoteRequestTemplateId = items[0].aQuoteRequestTemplateId;
                    templateObj.tTemplateName = items[0].tTemplateName;
                    templateObj.nBrandId = items[0].nBrandId;
                    templateObj.nCreatedBy = items[0].nCreatedBy;
                    templateObj.nUpdateBy = items[0].nUpdateBy;
                    SqlParameter tModuleNameP = new SqlParameter("@nQuoteRequestTemplateId", nTemplateId);

                    List<QuoteRequestTechCompTemp> item = db.Database.SqlQuery<QuoteRequestTechCompTemp>("exec sproc_GetQuoteRequestTechComp @nQuoteRequestTemplateId", tModuleNameP).ToList();
                    List<QuoteRequestTechComp> quoteRequestTechComps = new List<QuoteRequestTechComp>();
                    foreach (var RequestTechComp in item)
                    {
                        SqlParameter tModuleparm = new SqlParameter("@nQuoteRequestTechCompId", RequestTechComp.aQuoteRequestTechCompId);

                        List<QuoteRequestTechCompField> itemFields = db.Database.SqlQuery<QuoteRequestTechCompField>("exec sproc_GetQuoteRequestTechCompField @nQuoteRequestTechCompId", tModuleparm).ToList();

                        quoteRequestTechComps.Add(new QuoteRequestTechComp()
                        {

                            nQuoteRequestTemplateId = RequestTechComp.nQuoteRequestTemplateId,
                            aQuoteRequestTechCompId = RequestTechComp.aQuoteRequestTechCompId,
                            tTechCompName = RequestTechComp.tTechCompName,
                            tTableName = RequestTechComp.tTableName,
                            nCreatedBy = RequestTechComp.nCreatedBy,
                            nUpdateBy = RequestTechComp.nUpdateBy,
                            fields = itemFields

                        });

                    }
                    templateObj.quoteRequestTechComps = quoteRequestTechComps;
                }
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
                if (quoteRequest.aQuoteRequestTemplateId > 0)
                {
                    var nTechComp = db.Database.ExecuteSqlCommand("delete from tblQuoteRequestTechComp where nQuoteRequestTemplateId =@nQuoteRequestTemplateId ", new SqlParameter("@nQuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId));
                    var nTechCompFields = db.Database.ExecuteSqlCommand("delete from tblQuoteRequestTechCompFields where nQuoteRequestTemplateId =@nQuoteRequestTemplateId ", new SqlParameter("@nQuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId));

                }
                List<SqlParameter> tPramList = new List<SqlParameter>();
                tPramList.Add(new SqlParameter("@tTemplateName", quoteRequest.tTemplateName));
                tPramList.Add(new SqlParameter("@nBrandId", quoteRequest.nBrandId));
                tPramList.Add(new SqlParameter("@nCreatedBy", quoteRequest.nCreatedBy));
                tPramList.Add(new SqlParameter("@nUpdateBy", quoteRequest.nUpdateBy));
                tPramList.Add(new SqlParameter("@QuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId) {Direction = ParameterDirection.InputOutput});                
                db.Database.ExecuteSqlCommand("exec sproc_CreateAndUpdateQouteRequestMain @tTemplateName,@nBrandId,@nCreatedBy,@nUpdateBy,@QuoteRequestTemplateId out", tPramList[0], tPramList[1], tPramList[2], tPramList[3], tPramList[4]);
                quoteRequest.aQuoteRequestTemplateId = ((SqlParameter)tPramList[4]).Value == DBNull.Value ? 0 : Convert.ToInt32(((SqlParameter)tPramList[4]).Value);

                foreach (var RequestTechComps in quoteRequest.quoteRequestTechComps)
                {
                    List<SqlParameter> tPramListTechComps = new List<SqlParameter>();
                    tPramListTechComps.Add(new SqlParameter("@nQuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId));
                    tPramListTechComps.Add(new SqlParameter("@tTechCompName", RequestTechComps.tTechCompName));
                    tPramListTechComps.Add(new SqlParameter("@tTableName", RequestTechComps.tTableName==null? "": RequestTechComps.tTableName));                    
                    tPramListTechComps.Add(new SqlParameter("@nCreatedBy", RequestTechComps.nCreatedBy));
                    tPramListTechComps.Add(new SqlParameter("@nUpdateBy", RequestTechComps.nUpdateBy));
                    tPramListTechComps.Add(new SqlParameter("@aQuoteRequestTechCompId", RequestTechComps.aQuoteRequestTechCompId) { Direction = ParameterDirection.InputOutput });

                    db.Database.ExecuteSqlCommand("exec sproc_CreateAndUpdateQouteRequestTechComp @nQuoteRequestTemplateId,@tTechCompName,@tTableName,@nCreatedBy,@nUpdateBy,@aQuoteRequestTechCompId out", tPramListTechComps[0], tPramListTechComps[1], tPramListTechComps[2], tPramListTechComps[3], tPramListTechComps[4], tPramListTechComps[5]);
                    RequestTechComps.aQuoteRequestTechCompId = ((SqlParameter)tPramListTechComps[5]).Value == DBNull.Value ? 0 : Convert.ToInt32(((SqlParameter)tPramListTechComps[5]).Value);
                    //var noOfRowUpdated = db.Database.ExecuteSqlCommand("delete from tblQuoteRequestTechCompFields where nQuoteRequestTemplateId =nQuoteRequestTechCompId and nQuoteRequestTechCompId=@nQuoteRequestTechCompId", new SqlParameter("@nQuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId), new SqlParameter("@nQuoteRequestTechCompId", RequestTechComps.aQuoteRequestTechCompId));

                    foreach (var RequestTechCompsField in RequestTechComps.fields)
                    {
                        List<SqlParameter> tPramListTechCompsFields = new List<SqlParameter>();
                        tPramListTechCompsFields.Add(new SqlParameter("@nQuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId));
                        tPramListTechCompsFields.Add(new SqlParameter("@nQuoteRequestTechCompId", RequestTechComps.aQuoteRequestTechCompId));
                        tPramListTechCompsFields.Add(new SqlParameter("@tTechCompField", RequestTechCompsField.tTechCompField));
                        tPramListTechCompsFields.Add(new SqlParameter("@tTechCompName", RequestTechCompsField.tTechCompFieldName));

                        tPramListTechCompsFields.Add(new SqlParameter("@aQuoteRequestTechCompFieldId", SqlDbType.Int) { Direction = ParameterDirection.Output });
                        db.Database.ExecuteSqlCommand("exec @aQuoteRequestTechCompFieldId=sproc_CreateQouteRequestTechCompFields @nQuoteRequestTemplateId,@nQuoteRequestTechCompId,@tTechCompField,@tTechCompName ", tPramListTechCompsFields[4], tPramListTechCompsFields[0], tPramListTechCompsFields[1], tPramListTechCompsFields[2], tPramListTechCompsFields[3]);
                      //  var aQuoteRequestTechCompFieldId = ((SqlParameter)tPramListTechCompsFields[4]).Value == DBNull.Value ? 0 : Convert.ToInt32(((SqlParameter)tPramListTechCompsFields[4]).Value);

                    }
                }
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
        public HttpResponseMessage GetMergedQuoteRequest(int nProjectId)
        {
            try
            {
                var item = new MergedQuoteRequest()
                {
                    tContent = "<div><h1>Audio</h1></div><div><b>Address</b>C333 IUO Naaf, USA</div><div><h1>Audio</h1></div><div><b>Address</b>C333 IUO Naaf, USA</div><div><h1>Audio</h1></div><div><b>Address</b>C333 IUO Naaf, USA</div>",
                    tSubject = "Store #" + nProjectId + "HME Quote Request",
                    tTo = "abcd@gmail.com"
                };
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<MergedQuoteRequest>(item, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage SendQuoteRequest(MergedQuoteRequest request)
        {
            try
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("Done", new JsonMediaTypeFormatter())
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
