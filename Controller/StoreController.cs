using DeploymentTool.Helpers;
using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

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
            //    },
            //    new ActiveProjectModel()
            //    {
            //        nProjectId = 16,
            //        tStoreNo = "1030",
            //        dProjectGoliveDate = DateTime.Now,
            //        dProjEndDate = DateTime.Now,
            //        tNewVendor = "OOPP Vendor",
            //        tOldVendor = "GGG Vendor",
            //        tPrevProjManager = "Harry Gartner",
            //        tProjectType = "Audio",
            //        tProjManager = "Garry Gram",
            //        tStatus = "On Track"
            //    },
            //    new ActiveProjectModel()
            //    {
            //        nProjectId = 62,
            //        tStoreNo = "8500",
            //        dProjectGoliveDate = DateTime.Now,
            //        dProjEndDate = DateTime.Now,
            //        tNewVendor = "SDSD Vendor",
            //        tOldVendor = "XDSZ Vendor",
            //        tPrevProjManager = "Harry Gartner",
            //        tProjectType = "Audio",
            //        tProjManager = "Garry Gram",
            //        tStatus = "Delay Track"
            //    },
            //    new ActiveProjectModel()
            //    {
            //        nProjectId = 12,
            //        tStoreNo = "5000",
            //        dProjectGoliveDate = DateTime.Now,
            //        dProjEndDate = DateTime.Now,
            //        tNewVendor = "A54dfg Vendor",
            //        tOldVendor = "FDSDS Vendor",
            //        tPrevProjManager = "DSSDDD Gartner",
            //        tProjectType = "Audio",
            //        tProjManager = "GaDD Gram",
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
            //        },
            //        new HistoricalProjectModel()
            //        {
            //            nProjectId = 12,
            //            tStoreNo = "1030",
            //            dProjectGoliveDate = DateTime.Now,
            //            dProjEndDate = DateTime.Now,
            //            tVendor = "OOPP Vendor",
            //            tProjectType = "Audio",
            //            tProjManager = "Garry Gram"
            //        },
            //        new HistoricalProjectModel()
            //        {
            //            nProjectId = 52,
            //            tStoreNo = "8500",
            //            dProjectGoliveDate = DateTime.Now,
            //            dProjEndDate = DateTime.Now,
            //            tVendor = "XDSZ Vendor",
            //            tProjectType = "Audio",
            //            tProjManager = "Garry Gram"
            //        },
            //        new HistoricalProjectModel()
            //        {
            //            nProjectId = 72,
            //            tStoreNo = "5000",
            //            dProjectGoliveDate = DateTime.Now,
            //            dProjEndDate = DateTime.Now,
            //            tVendor = "A54dfg Vendor",
            //            tProjectType = "Audio",
            //            tProjManager = "GaDD Gram"
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

            List<DeliveryStatus> items = db.Database.SqlQuery<DeliveryStatus>("exec sproc_GetDeliveryStatus @nStoreID", new SqlParameter("@nStoreID", nStoreId)).ToList();

            //{
            //    new DeliveryStatus()
            //    {
            //        tStatus = "Completed",
            //        dDeliveryDate = DateTime.Now,
            //        tTechComponent = "Networking"
            //    },
            //    new DeliveryStatus()
            //    {
            //        tStatus = "Completed",
            //        dDeliveryDate = DateTime.Now.AddDays(10),
            //        tTechComponent = "Networking"
            //    },
            //    new DeliveryStatus()
            //    {
            //        tStatus = "Completed",
            //        dDeliveryDate = DateTime.Now.AddDays(-10),
            //        tTechComponent = "Networking"
            //    },
            //    new DeliveryStatus()
            //    {
            //        tStatus = "Completed",
            //        dDeliveryDate = DateTime.Now.AddDays(30),
            //        tTechComponent = "Networking"
            //    },
            //    new DeliveryStatus()
            //    {
            //        tStatus = "Completed",
            //        dDeliveryDate = DateTime.Now.AddDays(44),
            //        tTechComponent = "Networking"
            //    },
            //    new DeliveryStatus()
            //    {
            //        tStatus = "Completed",
            //        dDeliveryDate = DateTime.Now.AddDays(-12),
            //        tTechComponent = "Networking"
            //    },
            //    new DeliveryStatus()
            //    {
            //        tStatus = "Completed",
            //        dDeliveryDate = DateTime.Now.AddDays(34),
            //        tTechComponent = "Networking"
            //    }
            //};
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<DeliveryStatus>>(items, new JsonMediaTypeFormatter())
            };

        }

        [Authorize]
        [HttpGet]
        [Route("api/Store/GetDateChange")]
        public HttpResponseMessage GetDateChangeTable(int nStoreId)
        {
            List<DateChangeNotitication> items = new List<DateChangeNotitication>()
            {
                new DateChangeNotitication()
                {
                    isSelected = false,
                    tComponent = "Netowkring",
                    tVendor = "Comecast"
                },
                new DateChangeNotitication()
                {
                    isSelected = false,
                    tComponent = "Audio",
                    tVendor = "HME"
                },
                new DateChangeNotitication()
                {
                    isSelected = false,
                    tComponent = "POS",
                    tVendor = "Infor"
                },
                new DateChangeNotitication()
                {
                    isSelected = false,
                    tComponent = "Exterior Menus",
                    tVendor = "Fabcon"
                },
                new DateChangeNotitication()
                {
                    isSelected = false,
                    tComponent = "Payment Systems",
                    tVendor = "Fiserv"
                },
                new DateChangeNotitication()
                {
                    isSelected = false,
                    tComponent = "Interior Menus",
                    tVendor = "TDS"
                },
                new DateChangeNotitication()
                {
                    isSelected = false,
                    tComponent = "Sonic Radio",
                    tVendor = "PAM"
                },
                new DateChangeNotitication()
                {
                    isSelected = false,
                    tComponent = "Installation",
                    tVendor = "MIRA"
                }
            };
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<DateChangeNotitication>>(items, new JsonMediaTypeFormatter())
            };

        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/GetDateChangeBody")]
        public HttpResponseMessage GetDateChangeBody(DateChangeBody request)
        {
            DateChangeNotificationBody reeponse = new DateChangeNotificationBody()
            {
                tTo = "",
                tCC = "",
                nStoreId = request.nStoreId,
                tContent = "<h3>Test Body</h3>",
                tSubject = "Test, OK #10005 - Update Install & delivery status"
            };


            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<DateChangeNotificationBody>(reeponse, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/SendDateChangeNotification")]
        public HttpResponseMessage SendDateChangeNotification(DateChangeNotificationBody request)
        {

            List<DateChangePOOption> response = new List<DateChangePOOption>() {
            new DateChangePOOption()
            {
                nStoreId = 1,
                nPOId = 1,
                tPONumber = "Exterior Menu PO #1010"
            },
            new DateChangePOOption()
            {
                nStoreId = 2,
                nPOId = 2,
                tPONumber = "Interior Menu PO #8010",
            },
            new DateChangePOOption()
            {
                nStoreId = 3,
                nPOId = 3,
                tPONumber = "Interior Menu PO #9898",
            }
            };

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<DateChangePOOption>>(response, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/SendDateChangeRevisedPO")]
        public HttpResponseMessage SendDateChangeRevisedPO(List<DateChangePOOption> request)
        {

            string resp = "";

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<string>(resp, new JsonMediaTypeFormatter())
            };
        }


        [Authorize]
        [HttpGet]
        [Route("api/Store/GetDateChangeBody")]
        public HttpResponseMessage GetDocumentationTab(int nStoreId)
        {
            List<DocumentationTable> response = new List<DocumentationTable>()
            {
                new DocumentationTable()
                {
                    nPOId = 1,
                    nProjectId = 1,
                    nStoreId = nStoreId,
                    tFileName = "Test.pdf",
                    tSentBy = "Roshan",
                    tStoreNumber = "32423",
                    dtCreatedOn = DateTime.Now,
                },
                new DocumentationTable()
                {
                    nPOId = 1,
                    nProjectId = 1,
                    nStoreId = nStoreId,
                    tFileName = "Testdsads.pdf",
                    tSentBy = "Santosh",
                    tStoreNumber = "4324",
                    dtCreatedOn = DateTime.Now.AddDays(10)
                },
                new DocumentationTable()
                {
                    nPOId = 1,
                    nProjectId = 1,
                    nStoreId = nStoreId,
                    tFileName = "Tdsadasd.pdf",
                    tSentBy = "Roshan",
                    tStoreNumber = "423",
                    dtCreatedOn = DateTime.Now.AddDays(120)
                },
                new DocumentationTable()
                {
                    nPOId = 1,
                    nProjectId = 1,
                    nStoreId = nStoreId,
                    tFileName = "Tedr43443st.pdf",
                    tSentBy = "Santosh",
                    tStoreNumber = "4324",
                    dtCreatedOn = DateTime.Now.AddDays(-10)
                },
                new DocumentationTable()
                {
                    nPOId = 1,
                    nProjectId = 1,
                    nStoreId = nStoreId,
                    tFileName = "dasdasd.pdf",
                    tSentBy = "Roshan",
                    tStoreNumber = "4324234",
                    dtCreatedOn = DateTime.Now.AddDays(101)
                }
            };


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
            string strFilePath = "C:\\ProjectCode\\DeploymentToolV2\\Attachments\\294232e5-8a08-4d2f-83d6-5bdfecd29385\\PurchaseOrder.pdf";

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(strFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = request.tFileName;
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

            return response;
        }
    }
}