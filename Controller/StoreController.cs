using DeploymentTool.Helpers;
using DeploymentTool.Misc;
using DeploymentTool.Model;
using DeploymentTool.Model.Templates;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;
using System.Xml.Linq;

namespace DeploymentTool.Controller
{
    public class StoreController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        #region Active/Historical Project
        [Authorize]
        [HttpPost]
        public IQueryable<ActiveProjectModel> GetActiveProjects(Dictionary<string, string> searchFields)
        {
            int nStoreId = (searchFields["nStoreId"] == null) ? 0 : Convert.ToInt32(searchFields["nStoreId"]);
            IQueryable<ActiveProjectModel> items = db.Database.SqlQuery<ActiveProjectModel>("exec sproc_getActiveProjects @nStoreId", new SqlParameter("@nStoreId", nStoreId)).AsQueryable();
            //    new List<ActiveProjectModel>() {
            //    new ActiveProjectModel()
            //    {
            //        nProjectId = 72,
            //        tStoreNo = "1000",
            //        dProjectGoliveDate = DateTime.Now,
            //        dProjEndDate = DateTime.Now,
            //        tNewVendor = "ABCD Vendor",
            //        tOldVendor = "XYZ Vendor",
            //        tPrevProjManager = "Harry Gartner",
            //        tProjectType = "Audio",
            //        tProjManager = "Garry Gram",
            //        tStatus = "On Track"
            //    }           
            //};
            return items;
        }

        [Authorize]
        [HttpPost]
        public IQueryable<HistoricalProjectModel> GetHistoricalProjects(Dictionary<string, string> searchFields)
        {
            int nStoreId = (searchFields["nStoreId"] == null) ? 0 : Convert.ToInt32(searchFields["nStoreId"]);
            IQueryable<HistoricalProjectModel> items = db.Database.SqlQuery<HistoricalProjectModel>("exec sproc_getHistoricalProjects @nStoreId", new SqlParameter("@nStoreId", nStoreId)).AsQueryable();
            //try
            //{
            //    List<HistoricalProjectModel> items = new List<HistoricalProjectModel>() {
            //        new HistoricalProjectModel()
            //        {
            //            nProjectId = 11,
            //            tStoreNo = "1000",
            //            dProjectGoliveDate = DateTime.Now,
            //            dProjEndDate = DateTime.Now,
            //            tVendor = "ABCD Vendor",
            //            tProjectType = "Audio",
            //            tProjManager = "Garry Gram"
            //        }
            //    };
            //    return new HttpResponseMessage(HttpStatusCode.OK)
            //    {
            //        Content = new ObjectContent<List<HistoricalProjectModel>>(items, new JsonMediaTypeFormatter())
            //    };
            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            //}
            return items;
        }
        #endregion

        // GET api/<controller>
        [Authorize]
        [HttpPost]
        [ActionName("GetStores")]
        public HttpResponseMessage GetStores([FromBody] Store inputstore)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            var dal = new StoreDAL();
            var result = dal.GetStores(inputstore, (int)securityContext.nUserID);

            if (result == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<Store>>(result, new JsonMediaTypeFormatter())
                };
            }
        }

        [Authorize]
        [HttpPost]
        [ActionName("CreateStore")]
        // POST api/<controller>
        public HttpResponseMessage CreateStore([FromBody] Store store)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context"); if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            int nuserid = 1;
            var storeDAL = new StoreDAL();
            int storeId = storeDAL.CreateStore(store, securityContext.nUserID);
            store.aStoreId = storeId;

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Store>(store, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/update")]
        // PUT api/<controller>/5
        public HttpResponseMessage Update([FromBody] Store store)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");
            var storeDAL = new StoreDAL();

            store.nUpdateBy = (int)securityContext.nUserID;

            storeDAL.Update(store);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Store>(store, new JsonMediaTypeFormatter())
            };

        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/UpdateGoliveDate")]
        public HttpResponseMessage UpdateGoliveDate(ProjectInfo projInfo)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");

            db.Database.ExecuteSqlCommand("update tblProject set dGoLiveDate=@dGoLiveDate where aProjectID=@aProjectID", new SqlParameter("@dGoLiveDate", projInfo.dGoLiveDate), new SqlParameter("@aProjectID", projInfo.nProjectId));

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ProjectInfo>(projInfo, new JsonMediaTypeFormatter())
            };

        }

        [Authorize]
        [HttpGet]
        [Route("api/Store/GetDeliveryStatus")]
        public HttpResponseMessage GetDeliveryStatus(int nStoreId)
        {

            List<TechData> items = db.Database.SqlQuery<TechData>("exec sproc_GetAllTechData @nStoreID", new SqlParameter("@nStoreID", nStoreId)).ToList();


            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<TechData>>(items, new JsonMediaTypeFormatter())
            };
            //List<DeliveryStatus> items = db.Database.SqlQuery<DeliveryStatus>("exec sproc_GetDeliveryStatus @nStoreID", new SqlParameter("@nStoreID", nStoreId)).ToList();


            //return new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new ObjectContent<List<DeliveryStatus>>(items, new JsonMediaTypeFormatter())
            //};

        }

        [Authorize]
        [HttpGet]
        [Route("api/Store/GetDateChange")]
        public HttpResponseMessage GetDateChangeTable(int nStoreId)
        {
            List<TechData> items = db.Database.SqlQuery<TechData>("exec sproc_GetAllTechData @nStoreID", new SqlParameter("@nStoreID", nStoreId)).ToList();

            
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<TechData>>(items, new JsonMediaTypeFormatter())
            };

        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/GetDateChangeBody")]
        public HttpResponseMessage GetDateChangeBody(DateChangeBody request)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            string tUserName = securityContext.tName;
            SqlParameter tModuleparmAdress = new SqlParameter("@nStoreId", request.nStoreId);

            List<PurchaseOrderPreviewTemplate> itemPOStore = db.Database.SqlQuery<PurchaseOrderPreviewTemplate>("exec sproc_GetPurchaseOrdeStorerDetails @nStoreId", tModuleparmAdress).ToList();
            List<TechData> notification = db.Database.SqlQuery<TechData>("exec sproc_GetAllTechData @nStoreId", new SqlParameter("@nStoreId", request.nStoreId)).ToList();
            string strStyle = "<style>td{ border: 0px none!important;} " +
                    "table{ width: 100%!important;border: 1px solid lightgray!important;border-radius: 5px!important; text-align:left;}</style>";
            string tContent = strStyle + "<div>All,</div>";
            tContent += "<div>There has been a change in target dates for this project. please update your schedule to reflect </div>";
            tContent += "<div>the following responding to this email chain with confirmation of the change or any issues. </div>";
            tContent += "<div></br>Schedule</div>";
            //tContent += "<div><table style='border: 1px solid black;'><thead style='border: 1px solid black;'><tr><b><th>Components</th><th>Delivery Date</th><th>Install/Support Date</th><th>Config Date</th><th>Status</th></b></tr></thead>";
            tContent += "<div><table><thead><tr><b><th>Components</th><th>Delivery Date</th><th>Config Date</th><th>Status</th></b></tr></thead>";
            tContent += "<tbody>";
            string tTo = "";
            //int nVendorID = 0;
            foreach (var parts in notification)
            {
                foreach (var reqTech in request.lstItems)
                    if (reqTech.isSelected && reqTech.tComponent == parts.tComponent)
                    {
                        string dDeliver = parts.dDeliveryDate!=null?Convert.ToDateTime(parts.dDeliveryDate).ToString("MM/dd/yyyy").Replace('-', '/'):"";
                        string dCongDate = parts.dConfigDate != null ? Convert.ToDateTime(parts.dConfigDate).ToString("MM/dd/yyyy").Replace('-', '/') : "";
                        //tContent += "<tr><td>" + parts.tComponent + " - " + parts.tVendor + "</td><td>" + parts.dDeliveryDate + "</td><td>" + parts.dInstallDate + "</td><td>" + parts.dConfigDate + "</td><td>" + parts.tStatus + "</td></tr>";
                        if (parts.nVendorID != null && parts.nVendorID > 0)
                        {
                            var output = db.Database.SqlQuery<string>("select  dbo.gettToByVendor(@nVendorID) ", new SqlParameter("@nVendorID", parts.nVendorID)).FirstOrDefault();
                            if (!String.IsNullOrEmpty(output))
                                tTo = tTo + output;// + ";";
                        }
                        tContent += "<tr><td>" + parts.tComponent + " - " + parts.tVendor + "</td><td>" + dDeliver + "</td><td>" + dCongDate + "</td><td>" + parts.tStatus + "</td></tr>";
                    }
            }
            tContent += "</tbody>";
            tContent += "</table></div></br>";
            int nBrandId = 0;
            List<ActivePortFolioProjectsModel> activeProj = db.Database.SqlQuery<ActivePortFolioProjectsModel>("exec sproc_getActivePortFolioProjects @nBrandId,@nStoreID", new SqlParameter("@nBrandId", nBrandId),  new SqlParameter("@nStoreID", request.nStoreId)).ToList();
            tContent += "<div><table><thead><tr><b><th>Project type</th><th>Go-live Date</th></b></tr></thead>";
            tContent += "<tbody>";
            foreach (var part in activeProj)
            {
                string GoliveDate = part.dProjectGoliveDate != null ? Convert.ToDateTime(part.dProjectGoliveDate).ToString("MM/dd/yyyy").Replace('-', '/') : "";

                tContent += "<tr><td>" + part.tProjectType + "</td><td>" + GoliveDate + "</td></tr>";

                ;
            }
            tContent += "</tbody>";
            tContent += "</table></div></br>";

            tContent += "<div>Respectfully,</div>";
            tContent += "<div>"+tUserName+"</div>";
            tContent += "<div>New Store Team</div>";
            DateChangeNotificationBody reeponse = new DateChangeNotificationBody()
            {
                tTo = tTo,
                tCC = "",
                nStoreId = request.nStoreId,
                tContent = tContent,
                tSubject = itemPOStore[0].tCity + ", " + itemPOStore[0].tStoreState + " #" + itemPOStore[0].tStoreNumber + " - Updated Install & Delivery Dates",

            };


            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<DateChangeNotificationBody>(reeponse, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/SendDateChangeNotification")]
        public async System.Threading.Tasks.Task<IHttpActionResult> SendDateChangeNotification(DateChangeNotificationBody request)
        {
            List<DateChangePOOption> response = new List<DateChangePOOption>();

            try
            {

                EMailRequest MailObj = new EMailRequest();
                MailObj.tSubject = request.tSubject;
                if (request.tTo != null && request.tTo.Length > 0)
                {
                    MailObj.tTo = request.tTo;
                    if (request.tCC != null && request.tCC.Length > 0)
                        MailObj.tCC = request.tCC;
                    MailObj.tContent = request.tContent;
                    //MailObj.FileAttachments = request.FileAttachments;
                    DeploymentTool.Misc.Utilities.SendMail(MailObj);
                    tblOutgoingEmail tblQuoteEmail = MailObj.GettblOutgoingEmail();
                    db.tblOutgoingEmails.Add(tblQuoteEmail);
                    await db.SaveChangesAsync();
                    var aOutgoingEmailID = tblQuoteEmail.aOutgoingEmailID;
                }
                int nBrandId = 1;// Sonic
                SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", request.nStoreId);
                List<DateChangePOOption> tList = db.Database.SqlQuery<DateChangePOOption>("exec sproc_getPrevPOIDByStore @nStoreId", tModuleNameParam).ToList();

                //var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                //Nullable<int> lUserId = securityContext.nUserID;

                 foreach (var item in tList)
                {
                    int ret = 0;
                    //List<SqlParameter> tPramList = new List<SqlParameter>();
                    //tPramList.Add(new SqlParameter("@nStoreId", request.nStoreId));
                    //tPramList.Add(new SqlParameter("@nTemplateId", item.aPurchaseOrderTemplateID));
                    //tPramList.Add(new SqlParameter("@nUserID", lUserId));
                    //tPramList.Add(new SqlParameter("@ret", ret) { Direction = ParameterDirection.InputOutput });
                    //db.Database.ExecuteSqlCommand("exec sproc_getPOID @nStoreId,@nTemplateId,@nUserID,@ret out", tPramList[0], tPramList[1], tPramList[2], tPramList[3]);
                    //var aPurchaseOrderID = ((SqlParameter)tPramList[3]).Value == DBNull.Value ? 0 : Convert.ToInt32(((SqlParameter)tPramList[3]).Value);


                    DateChangePOOption obj = new DateChangePOOption();
                    obj.aPurchaseOrderTemplateID = item.aPurchaseOrderTemplateID;
                    obj.nStoreId = request.nStoreId;
                    obj.nPOId = 0;
                    obj.tPONumber = item.tTemplateName + " PO #" + item.nPOId.ToString();
                    response.Add(obj);
                }
            }
            catch (Exception ex)
            {


            }

            //{
            //new DateChangePOOption()
            //{
            //    nStoreId = 1,
            //    nPOId = 1,
            //    tPONumber = "Exterior Menu PO #1010"
            //},
            //new DateChangePOOption()
            //{
            //    nStoreId = 2,
            //    nPOId = 2,
            //    tPONumber = "Interior Menu PO #8010",
            //},
            //new DateChangePOOption()
            //{
            //    nStoreId = 3,
            //    nPOId = 3,
            //    tPONumber = "Interior Menu PO #9898",
            //}
            //};
            return Json(response);

        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/SendDateChangeRevisedPO")]
        public async System.Threading.Tasks.Task<IHttpActionResult> SendDateChangeRevisedPO(List<DateChangePOOption> request)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;

            try
            {

                string resp = "";
                string spliter = "@@Splitter@@";
                string[] separators = { "@@Splitter@@" };
                foreach (var item in request)
                {
                    if (item.isSelected)
                    {
                        if (item.nPOId <= 0)
                        {
                            int ret=0;
                            List<SqlParameter> tPramList = new List<SqlParameter>();
                            tPramList.Add(new SqlParameter("@nStoreId", item.nStoreId));
                            tPramList.Add(new SqlParameter("@nTemplateId", item.aPurchaseOrderTemplateID));
                            tPramList.Add(new SqlParameter("@nUserID", lUserId));
                            tPramList.Add(new SqlParameter("@ret", ret) { Direction = ParameterDirection.InputOutput });
                            db.Database.ExecuteSqlCommand("exec sproc_getPOID @nStoreId,@nTemplateId,@nUserID,@ret out", tPramList[0], tPramList[1], tPramList[2], tPramList[3]);
                            var aPOID = ((SqlParameter)tPramList[3]).Value == DBNull.Value ? 0 : Convert.ToInt32(((SqlParameter)tPramList[3]).Value);
                            item.nPOId = aPOID;
                        }

                        List<tblPurchaseOrderNotification> itemPOStore = db.Database.SqlQuery<tblPurchaseOrderNotification>("exec sproc_getPreviousPODetails @nStoreId,@nUserID,@nTemplateId", new SqlParameter("@nStoreId", item.nStoreId), new SqlParameter("@nUserID", lUserId), new SqlParameter("@nTemplateId", item.aPurchaseOrderTemplateID)).ToList();
                        List<PurchaseOrderTemplate> itemPOTemplate = db.Database.SqlQuery<PurchaseOrderTemplate>("exec sproc_GetPurchaseOrderTemplate @aPurchaseOrderTemplateID,@nStoreId", new SqlParameter("@aPurchaseOrderTemplateID", item.aPurchaseOrderTemplateID), new SqlParameter("@nStoreId", item.nStoreId)).ToList();
                        // DateTime dDeliverTime = itemPOTemplate[0].dDeliveryDate != null ? Convert.ToDateTime(itemPOTemplate[0].dDeliveryDate) : DateTime.Now;
                        // string dDeliver = dDeliverTime.ToShortDateString();
                        string dDeliver = itemPOTemplate[0].dDeliveryDate != null ? Convert.ToDateTime(itemPOTemplate[0].dDeliveryDate).ToString("MM/dd/yyyy").Replace('-', '/') : "";

                        string strBody = itemPOStore[0].tPDFData;
                        strBody = strBody.Split(separators, StringSplitOptions.None)[0];
                        string sPDFData = strBody + "@@Splitter@@" + item.nPOId.ToString() + "@@Splitter@@" + dDeliver;
                        strBody = strBody.Replace("@@InspirePOID@@", item.nPOId.ToString()).Replace("@@InspiredDeliver@@", dDeliver);

                        string fileName = "PurchaseOrder.pdf";
                        String strFilePath = DeploymentTool.Misc.Utilities.WriteHTMLToPDF(strBody, fileName);

                        string tContent = itemPOStore[0].tSentHtml.Split(separators, StringSplitOptions.None)[0] ;
                        string tSubject = itemPOStore[0].tSubject;
                        string tTo = itemPOTemplate[0].tTo; //itemPOStore[0].tTo;//
                        string tCC = itemPOTemplate[0].tCC;// itemPOStore[0].tCC;//
                        string tVendorName = itemPOTemplate[0].tVendorName;
                        string tStoreNumber = itemPOStore[0].tStoreNumber;
                        string tProjectManager = itemPOTemplate[0].tProjectManager;
                       // var tProjectManager = db.Database.SqlQuery<string>("select top 1 tITPM as tProjectManager from tblProjectStakeHolders with (nolock) where nMyActiveStatus=1  and nStoreId=@nStoreId", new SqlParameter("@nStoreId", item.nStoreId)).FirstOrDefault();

                        string sSentHtml = tContent + "@@Splitter@@" + item.nPOId.ToString() + "@@Splitter@@" + tVendorName + "@@Splitter@@" + tStoreNumber + "@@Splitter@@" + dDeliver + "@@Splitter@@" + tProjectManager;
                        tContent = tContent.Replace("@@InspirePOID@@", item.nPOId.ToString()).Replace("@@InspiretVendorName@@", tVendorName).Replace("@@InspiretStoreNumber@@", tStoreNumber).Replace("@@InspiredDeliver@@", dDeliver).Replace("@@InspiretProjectManager@@", tProjectManager);
                        PurchaseOrderMailMessage message = new PurchaseOrderMailMessage()
                        {
                            nProjectId = item.nStoreId,
                            tTo = tTo,
                            tCC = tCC,
                            tContent = tContent,
                            tFileName = fileName,
                            tSubject = tSubject,
                            tMyFolderId = strFilePath,
                            aPurchaseOrderID = item.nPOId,

                        };



                        tblPurchaseOrder tblPO = new tblPurchaseOrder()
                        {
                            aPurchaseOrderID = item.nPOId,
                            nTemplateId = item.aPurchaseOrderTemplateID,
                            nStoreID = item.nStoreId,
                            // tPurchaseOrderNumber = request.tStoreNumber,
                            tBillingName = itemPOStore[0].tBillingName,
                            tBillingPhone = itemPOStore[0].tBillingPhone,
                            tBillingEmail = itemPOStore[0].tBillingEmail,
                            tBillingAddress = itemPOStore[0].tBillingAddress,
                            tShippingName = itemPOStore[0].tShippingName,
                            tShippingPhone = itemPOStore[0].tShippingPhone,
                            tShippingEmail = itemPOStore[0].tShippingEmail,
                            tShippingAddress = itemPOStore[0].tShippingAddress,
                            tNotes = itemPOStore[0].tNotes,
                            dDeliver = itemPOTemplate[0].dDeliveryDate,
                            cTotal = itemPOStore[0].cTotal,
                            tPDFData = sPDFData,
                            tSentHtml = sSentHtml,
                            nCreatedBy = lUserId,
                            dtCreatedOn = DateTime.Now

                        };

                        //db.tblPurchaseOrders.Add(tblPO);
                        db.Entry(tblPO).State = EntityState.Modified;
                        // Update into tblVendorPartRel


                        await db.SaveChangesAsync();
                        var aPurchaseOrderID = tblPO.aPurchaseOrderID;
                        PurchaseOrderController poCntrl = new PurchaseOrderController();

                        try
                        {
                            //string fileName = "PurachaaseOrder.pdf";

                            //string URL = HttpRuntime.AppDomainAppPath;
                            //string strFilePath = URL + @"Attachments\" + fileName;

                            EMailRequest MailObj = new EMailRequest();
                            MailObj.tSubject = message.tSubject;
                            MailObj.tTo = message.tTo;
                            MailObj.tCC = message.tCC;
                            MailObj.tContent = message.tContent;
                            MailObj.tFilePath = message.tMyFolderId;// strFilePath;
                                                                    //FileAttachment ifile = new FileAttachment();
                                                                    //ifile.tFileName= strFilePath;
                                                                    //List< FileAttachment> ifiles = new List<FileAttachment>();
                                                                    //ifiles.Add(ifile);
                                                                    //MailObj.FileAttachments= ifiles;
                            DeploymentTool.Misc.Utilities.SendMail(MailObj);
                            tblOutgoingEmail tblQuoteEmail = MailObj.GettblOutgoingEmail();
                            db.tblOutgoingEmails.Add(tblQuoteEmail);
                            await db.SaveChangesAsync();
                            var aOutgoingEmailID = tblQuoteEmail.aOutgoingEmailID;
                            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblPurchaseOrder set nOutgoingEmailID=@nOutgoingEmailID  where aPurchaseOrderID =@aPurchaseOrderID", new SqlParameter("@nOutgoingEmailID", aOutgoingEmailID), new SqlParameter("@aPurchaseOrderID", message.aPurchaseOrderID));

                            //if (MailObj.FileAttachments != null)
                            //{
                            //    foreach (var RequestFile in MailObj.FileAttachments)
                            //    {

                            //        tblOutgoingEmailAttachment tblQuoteEmailAtt = MailObj.GettblOutgoingEmailAttachment(RequestFile);
                            //        db.tblOutgoingEmailAttachments.Add(tblQuoteEmailAtt);
                            //        await db.SaveChangesAsync();
                            //    }
                            //}
                            tblOutgoingEmailAttachment tblQuoteEmailAtt = new tblOutgoingEmailAttachment();
                            tblQuoteEmailAtt.nOutgoingEmailID = aOutgoingEmailID;
                            tblQuoteEmailAtt.tFileName = message.tFileName;
                            tblQuoteEmailAtt.ifile = File.ReadAllBytes(message.tMyFolderId);
                            db.tblOutgoingEmailAttachments.Add(tblQuoteEmailAtt);
                            await db.SaveChangesAsync();

                            if (File.Exists(message.tMyFolderId))
                            {
                                System.GC.Collect();
                                System.GC.WaitForPendingFinalizers();
                                File.Delete(message.tMyFolderId);
                                bool exists = System.IO.Directory.Exists(message.tMyFolderId.Replace("\\" + message.tFileName, ""));

                                if (exists)
                                    System.IO.Directory.Delete(message.tMyFolderId.Replace("\\" + message.tFileName, ""));
                            }
                        }
                        catch (Exception ex)
                        {
                            TraceUtility.ForceWriteException("PurchseOrder.SendPO", HttpContext.Current, ex);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }

            return Ok("");
        }


        [Authorize]
        [HttpGet]
        [Route("api/Store/GetDateChangeBody")]
        public HttpResponseMessage GetDocumentationTab(int nStoreId)
        {
            

            List<DocumentationTable> response = db.Database.SqlQuery<DocumentationTable>("exec sproc_GetDocumentation @nStoreId", new SqlParameter("@nStoreId", nStoreId)).ToList();

            //List<DocumentationTable> response = new List<DocumentationTable>()
            //{
            //    new DocumentationTable()
            //    {
            //        nPOId = 1,
            //        nProjectId = 1,
            //        nStoreId = nStoreId,
            //        tFileName = "Test.pdf",
            //        tSentBy = "Roshan",
            //        tStoreNumber = "32423",
            //        dtCreatedOn = DateTime.Now,
            //    },
            //    new DocumentationTable()
            //    {
            //        nPOId = 1,
            //        nProjectId = 1,
            //        nStoreId = nStoreId,
            //        tFileName = "Testdsads.pdf",
            //        tSentBy = "Santosh",
            //        tStoreNumber = "4324",
            //        dtCreatedOn = DateTime.Now.AddDays(10)
            //    },
            //    new DocumentationTable()
            //    {
            //        nPOId = 1,
            //        nProjectId = 1,
            //        nStoreId = nStoreId,
            //        tFileName = "Tdsadasd.pdf",
            //        tSentBy = "Roshan",
            //        tStoreNumber = "423",
            //        dtCreatedOn = DateTime.Now.AddDays(120)
            //    },
            //    new DocumentationTable()
            //    {
            //        nPOId = 1,
            //        nProjectId = 1,
            //        nStoreId = nStoreId,
            //        tFileName = "Tedr43443st.pdf",
            //        tSentBy = "Santosh",
            //        tStoreNumber = "4324",
            //        dtCreatedOn = DateTime.Now.AddDays(-10)
            //    },
            //    new DocumentationTable()
            //    {
            //        nPOId = 1,
            //        nProjectId = 1,
            //        nStoreId = nStoreId,
            //        tFileName = "dasdasd.pdf",
            //        tSentBy = "Roshan",
            //        tStoreNumber = "4324234",
            //        dtCreatedOn = DateTime.Now.AddDays(101)
            //    }
            //};


            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<DocumentationTable>>(response, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage downloadPO(DocumentationTable request)// SantoshPP
        {
            //string fileName = "PurachaaseOrder.pdf";

            // string URL = HttpRuntime.AppDomainAppPath;
            //string strFilePath = "C:\\ProjectCode\\DeploymentToolV2\\Attachments\\294232e5-8a08-4d2f-83d6-5bdfecd29385\\PurchaseOrder.pdf";
            List<Attachment> Attachment = db.Database.SqlQuery<Attachment>("exec sproc_getAttachemntByPOID @aPurchaseOrderID", new SqlParameter("@aPurchaseOrderID", request.nPOId)).ToList();


            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            // response.Content = new StreamContent(new FileStream(strFilePath, FileMode.Open, FileAccess.Read));
            //Stream stream = ConvertToStream(Attachment[0].AttachmentBlob);
            Stream stream = new MemoryStream(Attachment[0].AttachmentBlob);
            var fileContent = new StreamContent(stream);
            response.Content = fileContent;
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = request.tFileName;
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

            return response;
        }
    }
}