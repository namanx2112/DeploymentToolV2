using DeploymentTool.Helpers;
using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

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
        // PUT api/<controller>/5
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
    }
}