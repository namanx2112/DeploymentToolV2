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
    public class ExStoreController : ApiController
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
                            request.tRED, request.tCM, request.tANE, request.tRVP, request.tPrincipalPartner, request.dStatus, request.dOpenStore, request.tProjectStatus, securityContext.nUserID, 
                            request.nBrandId, request.nNumberOfTabletsPerStore, request.tEquipmentVendor, request.dShipDate, request.dRevisitDate, request.dInstallDate, request.tInstallationVendor, request.tInstallStatus
                            );

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
        public HttpResponseMessage GetStoreList(string searchText, int nBrandId)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                if (searchText == null)
                    searchText = string.Empty;
                SqlParameter tModuleNameParam = new SqlParameter("@tText", searchText);
                SqlParameter nBrand = new SqlParameter("@nBrandID", nBrandId);
                List<StoreAndId> items = db.Database.SqlQuery<StoreAndId>("exec sproc_SearchStore @tText,@nBrandID", tModuleNameParam, nBrand).ToList();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<StoreAndId>>(items, new JsonMediaTypeFormatter())
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
        public HttpResponseMessage GetStoreInfo(string searchText, int nBrandId)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                if (searchText == null)
                    searchText = string.Empty;
                SqlParameter tModuleNameParam = new SqlParameter("@tText", searchText);
                SqlParameter nBrand = new SqlParameter("@nBrandID", nBrandId);
                List<StoreSearchModel> items = db.Database.SqlQuery<StoreSearchModel>("exec sproc_GetStoreInfo @tText,@nBrandID", tModuleNameParam, nBrand).ToList();
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
        public HttpResponseMessage GetProjectGlimpse(int nProjectId)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                SqlParameter pProjectId = new SqlParameter("@nProjectId", nProjectId);
                ProjectGlimpse items = db.Database.SqlQuery<ProjectGlimpse>("exec sproc_getProjectGlimpse @nProjectId", pProjectId).FirstOrDefault();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<ProjectGlimpse>(items, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("Sonic.GetProjectGlimpse", HttpContext.Current, ex);
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
        public async Task<HttpResponseMessage> NewProject(NewProjectModel newStore)
        {

            try
            {
                ProjectType pType = (ProjectType)newStore.nProjectType;
                //Utilities.SetHousekeepingFields(true, HttpContext.Current, newStore.);
                int nMovedProjectId = 0;
                nMovedProjectId = db.Database.SqlQuery<int>("select top 1  aProjectID  from tblProject with(nolock) where nStoreID = @nStoreId order by aProjectID desc ", new SqlParameter("@nStoreId", newStore.nStoreId)).FirstOrDefault();


                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                Nullable<int> lUserId = securityContext.nUserID;
                tblProject tProjectModel = new tblProject();
                tProjectModel.nProjectActiveStatus = 1;
                tProjectModel.nProjectType = newStore.nProjectType;
                if(newStore.tblProjectInstallation!=null && newStore.tblProjectInstallation.dInstallEnd != null)
                tProjectModel.dGoLiveDate = newStore.tblProjectInstallation.dInstallEnd;
                else
                    tProjectModel.dGoLiveDate = DateTime.Now;  //to load store item view date is must hence adding today date
                tProjectModel.nStoreID = newStore.nStoreId;
                tProjectModel.nBrandID = newStore.nBrandID;
                tProjectModel.nCreatedBy = lUserId;
                tProjectModel.dtCreatedOn = DateTime.Now;
                //tProjectModel.nProjectStatus = nProjectStatus;// Need to do this pending
                //tProjectModel.nDMAID = nDMAID;
                //tProjectModel.tDMA = tDMA;
               // tProjectModel.dProjEndDate = dProjEndDate;
                db.tblProjects.Add(tProjectModel);
                await db.SaveChangesAsync();
                int aProjectID=tProjectModel.aProjectID;
                if (nMovedProjectId > 0)
                {
                    var CopyTechIfRequired = db.Database.ExecuteSqlCommand("exec sproc_CopyTechnologyToCurrentProject_new @nStoreId, @nProjectType, @nProjectID, @nFromProjectId", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectType", (int)pType), new SqlParameter("@nProjectID", aProjectID), new SqlParameter("@nFromProjectId", nMovedProjectId));
                }

                // Update Config Data
                //db.Database.ExecuteSqlCommand("exec sproc_copyProjectsConfig @nStoreId, @nProjectId", new SqlParameter("@nProjectId", aProjectID), new SqlParameter("@nStoreId", newStore.nStoreId));
                if (newStore.tblProjectConfig != null)// Add tblProjectConfig data to get it copied
                {
                    var aProjectConfigID = db.Database.SqlQuery<int>("select top 1  aProjectConfigID  from tblProjectConfig with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();

                    // db.Database.ExecuteSqlCommand("update tblProjectStakeHolders set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", newStore.nStoreId));
                    newStore.tblProjectConfig.nProjectID = aProjectID;
                    newStore.tblProjectConfig.nMyActiveStatus = 1;
                    newStore.tblProjectConfig.aProjectConfigID = aProjectConfigID;
                    if (aProjectConfigID > 0)
                        db.Entry(newStore.tblProjectConfig).State = EntityState.Modified;
                    else
                        db.tblProjectConfigs.Add(newStore.tblProjectConfig);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectStakeHolders != null)// Add stakeholder data to get it copied
                {
                    var aProjectStakeHolderID = db.Database.SqlQuery<int>("select top 1  aProjectStakeHolderID  from tblProjectStakeHolders with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectStakeHolders.nProjectID = aProjectID;
                    newStore.tblProjectStakeHolders.nMyActiveStatus = 1;
                    newStore.tblProjectStakeHolders.aProjectStakeHolderID = aProjectStakeHolderID;
                    if (aProjectStakeHolderID > 0)
                        db.Entry(newStore.tblProjectStakeHolders).State = EntityState.Modified;
                    else
                        db.tblProjectStakeHolders.Add(newStore.tblProjectStakeHolders);
                    
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectExteriorMenus != null)
                {
                    var aProjectExteriorMenuID = db.Database.SqlQuery<int>("select top 1  aProjectExteriorMenuID  from tblProjectExteriorMenus with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();

                    newStore.tblProjectExteriorMenus.nProjectID = aProjectID;
                    newStore.tblProjectExteriorMenus.nMyActiveStatus = 1;
                    newStore.tblProjectExteriorMenus.aProjectExteriorMenuID = aProjectExteriorMenuID;
                   
                    if (aProjectExteriorMenuID > 0)
                        db.Entry(newStore.tblProjectExteriorMenus).State = EntityState.Modified;
                    else
                        db.tblProjectExteriorMenus.Add(newStore.tblProjectExteriorMenus);

                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectInteriorMenus != null)
                {
                    var aProjectInteriorMenuID = db.Database.SqlQuery<int>("select top 1  aProjectInteriorMenuID  from tblProjectInteriorMenus with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectInteriorMenus.nProjectID = aProjectID;
                    newStore.tblProjectInteriorMenus.nMyActiveStatus = 1;
                    newStore.tblProjectInteriorMenus.aProjectInteriorMenuID = aProjectInteriorMenuID;

                    if (aProjectInteriorMenuID > 0)
                        db.Entry(newStore.tblProjectInteriorMenus).State = EntityState.Modified;
                    else
                        db.tblProjectInteriorMenus.Add(newStore.tblProjectInteriorMenus);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectPaymentSystem != null)
                {
                    var aProjectPaymentSystemID = db.Database.SqlQuery<int>("select top 1  aProjectPaymentSystemID  from tblProjectPaymentSystem with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectPaymentSystem.nProjectID = aProjectID;
                    newStore.tblProjectPaymentSystem.nMyActiveStatus = 1;
                    newStore.tblProjectPaymentSystem.aProjectPaymentSystemID = aProjectPaymentSystemID;
                    if (aProjectPaymentSystemID > 0)
                        db.Entry(newStore.tblProjectPaymentSystem).State = EntityState.Modified;
                    else
                        db.tblProjectPaymentSystems.Add(newStore.tblProjectPaymentSystem);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectAudio != null)
                {
                    var aProjectAudioID = db.Database.SqlQuery<int>("select top 1  aProjectAudioID  from tblProjectAudio with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectAudio.nProjectID = aProjectID;
                    newStore.tblProjectAudio.nMyActiveStatus = 1;
                    newStore.tblProjectAudio.aProjectAudioID = aProjectAudioID;
                    if (aProjectAudioID > 0)
                        db.Entry(newStore.tblProjectAudio).State = EntityState.Modified;
                    else
                        db.tblProjectAudios.Add(newStore.tblProjectAudio);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectPOS != null)
                {
                    var aProjectPOSID = db.Database.SqlQuery<int>("select top 1  aProjectPOSID  from tblProjectPOS with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectPOS.nProjectID = aProjectID;
                    newStore.tblProjectPOS.nMyActiveStatus = 1;
                    newStore.tblProjectPOS.aProjectPOSID = aProjectPOSID;
                    if (aProjectPOSID > 0)
                        db.Entry(newStore.tblProjectPOS).State = EntityState.Modified;
                    else
                        db.tblProjectPOS.Add(newStore.tblProjectPOS);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectNetworking != null)
                {
                    var aProjectNetworkingID = db.Database.SqlQuery<int>("select top 1  aProjectNetworkingID  from tblProjectNetworking with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectNetworking.nProjectID = aProjectID;
                    newStore.tblProjectNetworking.nMyActiveStatus = 1;
                    newStore.tblProjectNetworking.aProjectNetworkingID = aProjectNetworkingID;
                    if (aProjectNetworkingID > 0)
                        db.Entry(newStore.tblProjectNetworking).State = EntityState.Modified;
                    else
                        db.tblProjectNetworkings.Add(newStore.tblProjectNetworking);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectSonicRadio != null)
                {
                    var aProjectSonicRadioID = db.Database.SqlQuery<int>("select top 1  aProjectSonicRadioID  from tblProjectSonicRadio with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectSonicRadio.nProjectID = aProjectID;
                    newStore.tblProjectSonicRadio.nMyActiveStatus = 1;
                    newStore.tblProjectSonicRadio.aProjectSonicRadioID = aProjectSonicRadioID;
                    if (aProjectSonicRadioID > 0)
                        db.Entry(newStore.tblProjectSonicRadio).State = EntityState.Modified;
                    else
                        db.tblProjectSonicRadios.Add(newStore.tblProjectSonicRadio);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectServerHandheld != null)
                {
                    var aServerHandheldId = db.Database.SqlQuery<int>("select top 1  aServerHandheldId  from tblProjectServerHandheld with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectServerHandheld.nProjectID = aProjectID;
                    newStore.tblProjectServerHandheld.nMyActiveStatus = 1;
                    newStore.tblProjectServerHandheld.aServerHandheldId = aServerHandheldId;
                    if (aServerHandheldId > 0)
                        db.Entry(newStore.tblProjectServerHandheld).State = EntityState.Modified;
                    else
                        db.tblProjectServerHandhelds.Add(newStore.tblProjectServerHandheld);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectNetworkSwitch != null)
                {
                    var aProjectNetworkSwtichID = db.Database.SqlQuery<int>("select top 1  aProjectNetworkSwtichID  from tblProjectNetworkSwitch with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectNetworkSwitch.nProjectID = aProjectID;
                    newStore.tblProjectNetworkSwitch.nMyActiveStatus = 1;
                    newStore.tblProjectNetworkSwitch.aProjectNetworkSwtichID = aProjectNetworkSwtichID;
                    if (aProjectNetworkSwtichID > 0)
                        db.Entry(newStore.tblProjectNetworkSwitch).State = EntityState.Modified;
                    else
                        db.tblProjectNetworkSwitches.Add(newStore.tblProjectNetworkSwitch);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectImageOrMemory != null)
                {
                    var aProjectImageOrMemoryID = db.Database.SqlQuery<int>("select top 1  aProjectImageOrMemoryID  from tblProjectImageOrMemory with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectImageOrMemory.nProjectID = aProjectID;
                    newStore.tblProjectImageOrMemory.nMyActiveStatus = 1;
                    newStore.tblProjectImageOrMemory.aProjectImageOrMemoryID = aProjectImageOrMemoryID;
                    if (aProjectImageOrMemoryID > 0)
                        db.Entry(newStore.tblProjectImageOrMemory).State = EntityState.Modified;
                    else
                        db.tblProjectImageOrMemories.Add(newStore.tblProjectImageOrMemory);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectOrderAccuracy != null)
                {
                    var aProjectOrderAccuracyID = db.Database.SqlQuery<int>("select top 1  aProjectOrderAccuracyID  from tblProjectOrderAccuracy with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectOrderAccuracy.nProjectID = aProjectID;
                    newStore.tblProjectOrderAccuracy.nMyActiveStatus = 1;
                    newStore.tblProjectOrderAccuracy.aProjectOrderAccuracyID = aProjectOrderAccuracyID;
                    if (aProjectOrderAccuracyID > 0)
                        db.Entry(newStore.tblProjectOrderAccuracy).State = EntityState.Modified;
                    else
                        db.tblProjectOrderAccuracies.Add(newStore.tblProjectOrderAccuracy);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectOrderStatusBoard != null)
                {
                    var aProjectOrderStatusBoardID = db.Database.SqlQuery<int>("select top 1  aProjectOrderStatusBoardID  from tblProjectOrderStatusBoard with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectOrderStatusBoard.nProjectID = aProjectID;
                    newStore.tblProjectOrderStatusBoard.nMyActiveStatus = 1;
                    newStore.tblProjectOrderStatusBoard.aProjectOrderStatusBoardID = aProjectOrderStatusBoardID;
                    if (aProjectOrderStatusBoardID > 0)
                        db.Entry(newStore.tblProjectOrderStatusBoard).State = EntityState.Modified;
                    else
                        db.tblProjectOrderStatusBoards.Add(newStore.tblProjectOrderStatusBoard);
                    await db.SaveChangesAsync();
                }
                if (newStore.tblProjectInstallation != null)
                {
                    var aProjectInstallationID = db.Database.SqlQuery<int>("select top 1  aProjectInstallationID  from tblProjectInstallation with(nolock) where nStoreID = @nStoreId and nProjectID=@nProjectID  ", new SqlParameter("@nStoreId", newStore.nStoreId), new SqlParameter("@nProjectID", aProjectID)).FirstOrDefault();
                    newStore.tblProjectInstallation.nProjectID = aProjectID;
                    newStore.tblProjectInstallation.nMyActiveStatus = 1;
                    newStore.tblProjectInstallation.aProjectInstallationID = aProjectInstallationID;
                    if (aProjectInstallationID > 0)
                        db.Entry(newStore.tblProjectInstallation).State = EntityState.Modified;
                    else
                        db.tblProjectInstallations.Add(newStore.tblProjectInstallation);
                    await db.SaveChangesAsync();
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<NewProjectModel>(newStore, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("Sonic.NewStore", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            //return new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new ObjectContent<NewProjectModel>(newStore, new JsonMediaTypeFormatter())
            //};
        }

            [Authorize]
        [HttpPost]
        public HttpResponseMessage NewStore(NewProjectStore newStore)
        {
            try
            {
                var tId = db.Database.SqlQuery<int>("select top 1 1 from tblStore with(nolock) where tStoreNumber='" + newStore.tStoreNumber + "' and nBrandId=" + newStore.nBrandID).FirstOrDefault();
                if (tId == 0)
                {
                    ProjectType pType = (ProjectType)newStore.nProjectType;
                    Utilities.SetHousekeepingFields(true, HttpContext.Current, newStore);
                    
                    tblStore ttboStore = newStore.GettblStores();
                    if (ttboStore.aStoreID > 0)
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
                   
                    tblProject tProjectModel = newStore.GettblProject();
                    tProjectModel.nProjectActiveStatus = 1;
                    tProjectModel.nStoreID = ttboStore.aStoreID;
                    newStore.aStoreId = ttboStore.aStoreID;
                    db.tblProjects.Add(tProjectModel);
                    db.SaveChanges();
                    newStore.nProjectID = tProjectModel.aProjectID;
                    
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ObjectContent<NewProjectStore>(newStore, new JsonMediaTypeFormatter())
                    };
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ObjectContent<int>(-1, new JsonMediaTypeFormatter())
                    };
                }
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
