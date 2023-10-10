using DeploymentTool.Model;
using DeploymentTool.Model.Templates;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Mail;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Web;
using System.Runtime.Remoting.Messaging;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;
using DeploymentTool.Misc;

namespace DeploymentTool.Controller
{
    public class QuoteRequestController : ApiController
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpGet]
        [ActionName("GetAllTemplate")]
        public HttpResponseMessage GetAllTemplate(int nBrandId)
        {
            SqlParameter tModuleNameParam = new SqlParameter("@nBrandId", nBrandId);
            List<QuoteRequestTemplateTemp> items = db.Database.SqlQuery<QuoteRequestTemplateTemp>("exec sproc_GetAllQuoteRequestTemplate @nBrandId", tModuleNameParam).ToList();           
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<QuoteRequestTemplateTemp>>(items, new JsonMediaTypeFormatter())
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
                    
                    //PurchaseOrderTemplate templatePOobj = new PurchaseOrderTemplate();
                    //SqlParameter tModuleNameParam = new SqlParameter("@aPurchaseOrderTemplateID", nTemplateId);
                    //List<tblPurchaseOrderTemplateTemp> items = db.Database.SqlQuery<tblPurchaseOrderTemplateTemp>("exec sproc_GetPurchaseOrderTemplate @aPurchaseOrderTemplateID", tModuleNameParam).ToList();

                    //templatePOobj.aPurchaseOrderTemplateID = items[0].aPurchaseOrderTemplateID;
                    //templatePOobj.tTemplateName = items[0].tTemplateName;
                    //templatePOobj.nBrandId = items[0].nBrandId;
                    //templatePOobj.nCreatedBy = items[0].nCreatedBy;
                    //templatePOobj.nUpdateBy = items[0].nUpdateBy;
                    //SqlParameter tModuleparmParts = new SqlParameter("@aPurchaseOrderTemplateID", nTemplateId);

                    //List<PurchaseOrderPartDetails> itemParts = db.Database.SqlQuery<PurchaseOrderPartDetails>("exec sproc_GetPurchaseOrderPartsDetails @aPurchaseOrderTemplateID", tModuleparmParts).ToList();
                    //List<PurchaseOrderParts> obj=new List<PurchaseOrderParts>();
                    //foreach (var RequestTechComp in itemParts)
                    //{

                    //    obj.Add(new PurchaseOrderParts()
                    //    {
                    //        aPurchaseOrderTemplatePartsID = RequestTechComp.nPurchaseOrderPartDetailsID,
                    //        aPurchaseOrderTemplateID = RequestTechComp.aPurchaseOrderTemplateID,
                    //        nPartID = RequestTechComp.nPartID,
                    //        nConfigProjectFieldID = RequestTechComp.nQuantity,
                    //        tPartDesc = RequestTechComp.tPartDesc,
                    //        tPartNumber = RequestTechComp.tPartNumber,
                    //        cPrice = RequestTechComp.cPrice,
                    //        nQuantity = RequestTechComp.nQuantity,
                    //        cTotal = RequestTechComp.cTotal


                    //    });

                    //}
                    //templatePOobj.purchaseOrderParts = obj;
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<QuoteRequestTemplate>(templateObj, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("QuoteRequerst.GetTemplate", HttpContext.Current, ex);
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
                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                int lUserId = securityContext.nUserID;
                List<SqlParameter> tPramList = new List<SqlParameter>();
                tPramList.Add(new SqlParameter("@tTemplateName", quoteRequest.tTemplateName));
                tPramList.Add(new SqlParameter("@nBrandId", quoteRequest.nBrandId));
                tPramList.Add(new SqlParameter("@nCreatedBy", lUserId));
                tPramList.Add(new SqlParameter("@nUpdateBy", lUserId));
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
                TraceUtility.ForceWriteException("QuoteRequerst.CreateUpdateTemplate", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetMergedQuoteRequest(int nStoreId, int nTemplateId)// SantoshPP
        {
            try
            {
             //   int nProjectId = nStoreId;// Need to get ProjectId from store Id
                
                string strBody = "";
             //   string strSubject = "";
               // var strNumber = db.Database.SqlQuery<string>("Select tstoreNumber from tblstore with (nolock) where aStoreID= (Select nStoreID from tblProject  with (nolock) where aProjectID= @nProjectID)", new SqlParameter("@nProjectID", nProjectID)).FirstOrDefault();
                //strSubject = "Store #" + strNumber + " Quote Request";

                SqlParameter tModuleNameP = new SqlParameter("@nQuoteRequestTemplateId", nTemplateId);
                List<QuoteRequestTechCompTemp> item = db.Database.SqlQuery<QuoteRequestTechCompTemp>("exec sproc_GetQuoteRequestTechComp @nQuoteRequestTemplateId", tModuleNameP).ToList();
                string strStyle = "<style>td{ border: 0px none!important;} " +
                    "table{ width: 60%!important;border: 1px solid lightgray!important;border-radius: 5px!important;}</style>";
                foreach (var RequestTechComp in item)
                {
                    strBody += "<h2> " + RequestTechComp.tTechCompName + " </h2>";
                    // strBody += "</br> " + RequestTechComp.tTechCompName + ":</br>";
                    string strData = "";
                    using (var conn = new SqlConnection(_connectionString))
                    {

                        using (var cmd = new SqlCommand("sproc_getDynamicDataFromCompID", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@nQuoteRequestTechCompId", RequestTechComp.aQuoteRequestTechCompId);
                            cmd.Parameters.AddWithValue("@nStoreId", nStoreId);

                            conn.Open();

                            try
                            {
                                using (var reader = cmd.ExecuteReader())
                                {
                                    bool RowExist = false;
                                    do
                                    {
                                        while (reader.Read())
                                        {
                                            for (int i = 0; i < reader.FieldCount; i++)
                                            {
                                                strData += "<tr>";
                                                //strData += reader.GetName(i).ToString() + ":" + reader.GetValue(i).ToString() + "</br>";
                                                strData += "<td><b> " + reader.GetName(i).ToString() + " </b></td><td>" + reader.GetValue(i).ToString() + "</td>";
                                                RowExist = true;
                                                strData += "</tr>";
                                            }
                                        }
                                        if (!RowExist)
                                        {
                                            for (int i = 0; i < reader.FieldCount; i++)
                                            {
                                                strData += "<tr>";
                                                strData += "<td><b> " + reader.GetName(i).ToString() + " </b></td><td></td>";
                                                strData += "</tr>";
                                            }
                                        }
                                    } while (reader.NextResult());
                                }
                            }
                            catch (Exception e)
                            {

                                TraceUtility.ForceWriteException("QuoteRequerst.GetMergedQuoteRequest sproc_getDynamicDataFromCompID aQuoteRequestTechCompId="+ RequestTechComp.aQuoteRequestTechCompId.ToString() + " nStoreId="+ nStoreId.ToString(), HttpContext.Current, e);

                            }
                        }
                    }
                    strBody += "<div><table>" + strData + "</table></div>";
                }
                SqlParameter tModuleparmAdress = new SqlParameter("@nStoreId", nStoreId);

                List<PurchaseOrderPreviewTemplate> itemPOStore = db.Database.SqlQuery<PurchaseOrderPreviewTemplate>("exec sproc_GetPurchaseOrdeStorerDetails @nStoreId", tModuleparmAdress).ToList();

                string tContentdata = "<div>Please provide a quote for this store based on the information below. Please be sure to reply to all so our entire team receives it. Thanks!</br></div>";
                tContentdata += strBody;// "<div><h1>Audio</h1></div><div><b>Address</b>C333 IUO Naaf, USA</div><div><h1>Audio</h1></div><div><b>Address</b>C333 IUO Naaf, USA</div>",
                SqlParameter tModuleNameParam = new SqlParameter("@aQuoteRequestTemplateId", nTemplateId);
                List<QuoteRequestTemplateTemp> items = db.Database.SqlQuery<QuoteRequestTemplateTemp>("exec sproc_GetQuoteRequestTemplate @aQuoteRequestTemplateId", tModuleNameParam).ToList();

                

                var itemMerge = new MergedQuoteRequest()
                {
                    tContent = strStyle + tContentdata,
                    tSubject = "Store #" + itemPOStore[0].tStoreNumber + "  - "+ itemPOStore[0].tCity+" "+ itemPOStore[0].tStoreState +" -"+ items[0].tTemplateName,//" Quote Request",
                    tTo = ""

                };

                // Quote End
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<MergedQuoteRequest>(itemMerge, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("QuoteRequerst.GetMergedQuoteRequest", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> SendQuoteRequest(MergedQuoteRequest request)
        {
            try
            {
                EMailRequest MailObj=new EMailRequest();
                MailObj.tSubject = request.tSubject;
                MailObj.tTo = request.tTo;
                MailObj.tCC = request.tCC;
                MailObj.tContent = request.tContent;
                MailObj.FileAttachments = request.FileAttachments;
                DeploymentTool.Misc.Utilities.SendMail(MailObj);
                tblOutgoingEmail tblQuoteEmail = MailObj.GettblOutgoingEmail();
                db.tblOutgoingEmails.Add(tblQuoteEmail);
                await db.SaveChangesAsync();
                var aOutgoingEmailID = tblQuoteEmail.aOutgoingEmailID;
                //if(request.FileAttachments !=null)
                //foreach (var RequestFile in request.FileAttachments)
                //{

                //    tblOutgoingEmailAttachment tblQuoteEmailAtt = MailObj.GettblOutgoingEmailAttachment(RequestFile);
                //    db.tblOutgoingEmailAttachments.Add(tblQuoteEmailAtt);
                //    await db.SaveChangesAsync();
                //}
                //start END
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("Done", new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("QuoteRequerst.SendQuoteRequest", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblQuoteRequestMain tblQuoteRequest = await db.tblQuoteRequestMains.FindAsync(id);
            if (tblQuoteRequest == null)
            {
                return NotFound();
            }

            tblQuoteRequest.bDeleted = true;
            db.Entry(tblQuoteRequest).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(tblQuoteRequest);
        }
    }
}
