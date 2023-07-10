using DeploymentTool.Misc;
using DeploymentTool.Model;
using DeploymentTool.Model.Templates;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class SonicController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateNewStores(ProjectExcelFields[] requestArr)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                foreach (var request in requestArr)
                {
                    db.sproc_CreateStoreFromExcel(string.Empty, request.tProjectType, request.tStoreNumber, request.tAddress, request.tCity, request.tState, request.nDMAID, request.tDMA,
                        request.tRED, request.tCM, request.tANE, request.tRVP, request.tPrincipalPartner, request.dStatus, request.dOpenStore, request.tProjectStatus, securityContext.nUserID, 6);

                    //List<SqlParameter> tPramList = new List<SqlParameter>();
                    //tPramList.Add(new SqlParameter("@tStoreName", "Dont Know"));
                    //tPramList.Add(new SqlParameter("@tProjectType", request.tProjectType));
                    //tPramList.Add(new SqlParameter("@tStoreNumber", request.tStoreNumber));
                    //tPramList.Add(new SqlParameter("@tAddress", request.tAddress));
                    //tPramList.Add(new SqlParameter("@tCity", request.tCity));
                    //tPramList.Add(new SqlParameter("@tState", request.tState));
                    //tPramList.Add(new SqlParameter("@nDMAID", request.nDMAID));
                    //tPramList.Add(new SqlParameter("@tDMA", request.tDMA));
                    //tPramList.Add(new SqlParameter("@tRED", request.tRED));
                    //tPramList.Add(new SqlParameter("@tCM", request.tCM));
                    //tPramList.Add(new SqlParameter("@tANE", request.tANE));
                    //tPramList.Add(new SqlParameter("@tRVP", request.tRVP));
                    //tPramList.Add(new SqlParameter("@tPrincipalPartner", request.tPrincipalPartner));
                    //tPramList.Add(new SqlParameter("@dStatus", request.tState));
                    //tPramList.Add(new SqlParameter("@dOpenStore", request.dOpenStore));
                    //tPramList.Add(new SqlParameter("@tProjectStatus", request.tProjectStatus));
                    //tPramList.Add(new SqlParameter("@nCreatedBy", securityContext.nUserID));
                    //tPramList.Add(new SqlParameter("@nBrandId", 6));
                    //string output = db.Database.SqlQuery<Dropdown>("exec sproc_CreateStoreFromExcel @tStoreName,@tProjectType,@tStoreNumber,@tAddress,@tCity,@tState,@nDMAID,@tDMA,@tRED,@tCM," +
                    //    "@tANE,@tRVP,@tPrincipalPartner,@dStatus,@dOpenStore,@tProjectStatus,@nCreatedBy,@nBrandId ", tPramList[0], tPramList[1], tPramList[2], tPramList[3], tPramList[4], tPramList[5],
                    //    tPramList[6], tPramList[7], tPramList[8], tPramList[9], tPramList[10], tPramList[11], tPramList[12], tPramList[13], tPramList[14], tPramList[15], tPramList[16], tPramList[17]).ToString();
                    await db.SaveChangesAsync();
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("Success", new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage SearchStore(string searchText)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                if (searchText == null)
                    searchText = string.Empty;
                SqlParameter tModuleNameParam = new SqlParameter("@tText", searchText);
                List<StoreSearchModel> items = db.Database.SqlQuery<StoreSearchModel>("exec sproc_SearchStore @tText", tModuleNameParam).ToList();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<StoreSearchModel>>(items, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [Authorize]
        [HttpGet]
        public HttpResponseMessage CreateAndGetProjectStoreDetails(int nProjectId)
        {
            NewProjectStore tmpStore = new NewProjectStore();
            try
            {

                // SqlParameter tModuleNameParam = new SqlParameter("@aProjectid", nProjectId);                


                tblProjectStore tProjStore = db.tblProjectStores.Where(p => p.nProjectID == nProjectId).FirstOrDefault();
                tblProject tProj = db.tblProjects.Where(p => p.aProjectID == nProjectId).FirstOrDefault();
                tblStore tStore = db.tblStores.Where(p => p.aStoreID == tProj.nStoreID).FirstOrDefault();
                tmpStore.tStakeHolder = db.tblProjectStakeHolders.Where(p => p.nProjectID == nProjectId).FirstOrDefault();
               // var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProject set projectActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tStore.aStoreID));
                //tProj.ProjectActiveStatus = 1;
                //Utilities.SetHousekeepingFields(true, HttpContext.Current, tProj);
                //db.tblProjects.Add(tProj); db.SaveChanges();
                //tProjStore.nProjectID = tProj.aProjectID;
                //Utilities.SetHousekeepingFields(true, HttpContext.Current, tProjStore);
                //db.tblProjectStores.Add(tProjStore); db.SaveChanges();

                //tProjStore.nProjectID = tProj.aProjectID;

                tmpStore.SetValues(tProj, tProjStore, tStore);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<NewProjectStore>(tmpStore, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage NewStore(NewProjectStore newStore)
        {
            try
            {
                newStore.nProjectID = 0;
                Utilities.SetHousekeepingFields(true, HttpContext.Current, newStore);
                tblStore ttboStore = newStore.GettblStore();
                db.tblStores.Add(ttboStore);
                db.SaveChanges();
                tblProject tProjectModel = newStore.GettblProject();
                tProjectModel.nStoreID = ttboStore.aStoreID;
                newStore.nStoreId = ttboStore.aStoreID;
                db.tblProjects.Add(tProjectModel);
                db.SaveChanges();
                newStore.nProjectID = tProjectModel.aProjectID;
                tblProjectStore tblProjectStoreModel = newStore.GettblProjectStores();
                db.tblProjectStores.Add(tblProjectStoreModel);
                db.SaveChanges();
                newStore.aProjectStoreID = tblProjectStoreModel.aProjectStoreID;
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<NewProjectStore>(newStore, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage UpdateStore(NewProjectStore newStore)
        {
            try
            {
                Utilities.SetHousekeepingFields(false, HttpContext.Current, newStore);
                tblStore ttboStore = newStore.GettblStore();
                db.Entry(ttboStore).State = EntityState.Modified;
                db.SaveChanges();
                tblProject tProjectModel = newStore.GettblProject();
                db.Entry(tProjectModel).State = EntityState.Modified;
                db.SaveChanges();
                tblProjectStore tblProjectStoreModel = newStore.GettblProjectStores();
                db.Entry(tblProjectStoreModel).State = EntityState.Modified;
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<NewProjectStore>(newStore, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #region Template
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetProjectTemplates(int nBrandId)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
               // List<ProjectTemplates> items = new List<ProjectTemplates>();
                SqlParameter tModuleNameParam = new SqlParameter("@nBrandId", nBrandId);
                List<ProjectTemplates> items = db.Database.SqlQuery<ProjectTemplates>("exec sproc_GetAllTemplate @nBrandId", tModuleNameParam).ToList();

                //{
                //    items.Add(
                //    new ProjectTemplates()
                //    {
                //        nTemplateId = 1,
                //        nTemplateType = ProjectTemplateType.Notification,
                //        tTemplateName = "Notification"
                //    });
                   
                //    //items.Add(
                //    //new ProjectTemplates()
                //    //{
                //    //    nTemplateId = 3,
                //    //    nTemplateType = ProjectTemplateType.QuoteRequest,
                //    //    tTemplateName = "Networking"
                //    //});
                //    //items.Add(
                //    //new ProjectTemplates()
                //    //{
                //    //    nTemplateId = 4,
                //    //    nTemplateType = ProjectTemplateType.PurchaseOrder,
                //    //    tTemplateName = "Exterior Menu-Fabcon"
                //    //});
                //    //items.Add(
                //    //new ProjectTemplates()
                //    //{
                //    //    nTemplateId = 6,
                //    //    nTemplateType = ProjectTemplateType.PurchaseOrder,
                //    //    tTemplateName = "Interior Menus-TDS"
                //    //});
                //};
                //Dictionary<int, string> lstQuoteRequest = new Dictionary<int, string>();
                //SqlParameter tModuleNameParam = new SqlParameter("@nBrandId", nBrandId);
                //List<QuoteRequestTemplateTemp> itemsQoute = db.Database.SqlQuery<QuoteRequestTemplateTemp>("exec sproc_GetAllQuoteRequestTemplate @nBrandId", tModuleNameParam).ToList();
                
                //foreach (var RequestTechComps in itemsQoute)
                //{

                //    lstQuoteRequest.Add(RequestTechComps.aQuoteRequestTemplateId, RequestTechComps.tTemplateName);
                //    //lstQuoteRequest.Add(2, "Networking");
                //    items.Add(
                //   new ProjectTemplates()
                //   {
                //       nTemplateId = RequestTechComps.aQuoteRequestTemplateId,
                //       nTemplateType = ProjectTemplateType.QuoteRequest,
                //       tTemplateName = RequestTechComps.tTemplateName
                //   });
                //}
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<ProjectTemplates>>(items, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion
    }
}
