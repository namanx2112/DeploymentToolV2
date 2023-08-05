using DeploymentTool.Misc;
using DeploymentTool.Model;
using DeploymentTool.Model.Templates;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static System.Data.Entity.Infrastructure.Design.Executor;

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
                    ProjectType pType;
                    if (Enum.TryParse(request.tProjectType.Replace(" ", ""), true, out pType))
                    {
                        int nProjectType = (int)pType;
                        db.sproc_CreateStoreFromExcel(string.Empty, nProjectType, request.tStoreNumber, request.tAddress, request.tCity, request.tState, request.nDMAID, request.tDMA,
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
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("Success", new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("Sonic.CreateNewStores", HttpContext.Current, ex);
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
                TraceUtility.ForceWriteException("Sonic.SearchStore", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [Authorize]
        [HttpGet]
        public HttpResponseMessage getStoreDetails(int nStoreId)
        {
            NewProjectStore tmpStore = new NewProjectStore();
            try
            {
                //  int nProjectId = nStoreId;

                // SqlParameter tModuleNameParam = new SqlParameter("@aProjectid", nProjectId);                


                //tblProjectStore tProjStore = db.tblProjectStores.Where(p => p.nProjectID == nProjectId).FirstOrDefault();
                tblProject tProj = db.tblProjects.Where(p => p.nStoreID == nStoreId && p.nProjectActiveStatus == 1).FirstOrDefault();
                tblStore tStore = db.tblStores.Where(p => p.aStoreID == nStoreId).FirstOrDefault();
                tmpStore.tStakeHolder = db.tblProjectStakeHolders.Where(p => p.nStoreId == nStoreId && p.nMyActiveStatus == 1).FirstOrDefault();
                // var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProject set projectActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tStore.aStoreID));
                //tProj.ProjectActiveStatus = 1;
                //Utilities.SetHousekeepingFields(true, HttpContext.Current, tProj);
                //db.tblProjects.Add(tProj); db.SaveChanges();
                //tProjStore.nProjectID = tProj.aProjectID;
                //Utilities.SetHousekeepingFields(true, HttpContext.Current, tProjStore);
                //db.tblProjectStores.Add(tProjStore); db.SaveChanges();

                //tProjStore.nProjectID = tProj.aProjectID;

                tmpStore.SetValues(tProj, tStore);

            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("Sonic.getStoreDetails", HttpContext.Current, ex);
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
                ProjectType pType = (ProjectType)newStore.nProjectType;
                Utilities.SetHousekeepingFields(true, HttpContext.Current, newStore);
                int nMovedProjectId = 0;
                var paramProjectId = new SqlParameter("@movedProjectId", nMovedProjectId);
                paramProjectId.Direction = ParameterDirection.Output;
                var noOfRowUpdated = db.Database.ExecuteSqlCommand("exec sproc_MoveProjectToHistory @nStoreId, @nProjectType,@movedProjectId OUTPUT", new SqlParameter("@nStoreId", newStore.aStoreId), new SqlParameter("@nProjectType", (int)pType), paramProjectId);
                if (noOfRowUpdated != 0)
                    nMovedProjectId = (paramProjectId.Value == null) ? 0 : Convert.ToInt32(paramProjectId.Value);
                tblStore ttboStore = newStore.GettblStores();
                if(ttboStore.aStoreID > 0)
                {
                    db.Entry(ttboStore).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    newStore.nProjectID = 0;
                    db.tblStores.Add(ttboStore);
                    db.SaveChanges();
                }
                //switch (pType)
                //{
                //    case ProjectType.AudioInstallation:
                //    case ProjectType.POSInstallation:
                //    case ProjectType.MenuInstallation:
                //    case ProjectType.PaymentTerminalInstallation:
                //        db.Entry(ttboStore).State = EntityState.Modified;
                //        db.SaveChanges();
                //        break;
                //    default:
                //        newStore.nProjectID = 0;
                //        db.tblStores.Add(ttboStore);
                //        db.SaveChanges();
                //        break;
                //}
                tblProject tProjectModel = newStore.GettblProject();
                tProjectModel.nProjectActiveStatus = 1;
                tProjectModel.nStoreID = ttboStore.aStoreID;
                newStore.aStoreId = ttboStore.aStoreID;
                db.tblProjects.Add(tProjectModel);
                db.SaveChanges();
                newStore.nProjectID = tProjectModel.aProjectID;
                if (nMovedProjectId > 0)
                {
                    var CopyTechIfRequired = db.Database.ExecuteSqlCommand("exec sproc_CopyTechnologyToCurrentProject @nStoreId, @nProjectType, @nProjectID, @nFromProjectId", new SqlParameter("@nStoreId", newStore.aStoreId), new SqlParameter("@nProjectType", (int)pType), new SqlParameter("@nProjectID", newStore.nProjectID), new SqlParameter("@nFromProjectId", nMovedProjectId));
                }

                // Update Config Data
                db.Database.ExecuteSqlCommand("update tblProjectConfig set nProjectID=@nProjectId where nStoreId =@nStoreId", new SqlParameter("@nProjectId", tProjectModel.aProjectID), new SqlParameter("@nStoreId", newStore.aStoreId));
                if (newStore.tStakeHolder != null)// Add stakeholder data to get it copied
                {
                    db.Database.ExecuteSqlCommand("update tblProjectStakeHolders set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", newStore.aStoreId));
                    newStore.tStakeHolder.nProjectID = tProjectModel.aProjectID;
                    newStore.tStakeHolder.nMyActiveStatus = 1;
                    newStore.tStakeHolder.aProjectStakeHolderID = 0;
                    db.tblProjectStakeHolders.Add(newStore.tStakeHolder);
                    db.SaveChangesAsync();
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<NewProjectStore>(newStore, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("Sonic.NewStore", HttpContext.Current, ex);
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
                tblStore ttboStore = newStore.GettblStores();
                db.Entry(ttboStore).State = EntityState.Modified;
                db.SaveChanges();
                tblProject tProjectModel = newStore.GettblProject();
                db.Entry(tProjectModel).State = EntityState.Modified;
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<NewProjectStore>(newStore, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("Sonic.UpdateStore", HttpContext.Current, ex);
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
                TraceUtility.ForceWriteException("Sonic.GetProjectTemplates", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion
    }
}
