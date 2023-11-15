using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;
using DeploymentTool.Misc;
using Newtonsoft.Json;

namespace DeploymentTool.Model
{
    public class ProjectRolloutController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectAudios
        public IQueryable<tblProjectsRollout> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectsRollout> items = null;
            try
            {

                //int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                //int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;


                // if (nProjectID != 0)
                {
                    items = db.tblProjectsRollouts.AsQueryable();
                }
                //else
                //{
                //    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                //    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectsRollout");
                //    items = db.Database.SqlQuery<tblProjectsRollout>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                //    //return items;
                //}
            }
            catch (Exception ex)
            {
            }
            return items;

        }

        // GET: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> GettblProjectsRollout(int id)
        {
            tblProjectsRollout tblProjectsRollout = await db.tblProjectsRollouts.FindAsync(id);
            if (tblProjectsRollout == null)
            {
                return NotFound();
            }

            return Ok(tblProjectsRollout);
        }

        // PUT: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectsRollout tblProjectsRollout)
        {

            //tblProjectsRollout.ProjectActiveStatus = 1;//SantoshPP\
            // Misc.Utilities.SetActiveProjectId(Misc.ProjectType.AudioInstallation, tblProjectsRollout.nStoreId, tblProjectsRollout);
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            int nCreateOrUpdate = 2;
            ////----Vendor Audit field-----------
            //if (tblProjectsRollout.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 1, "tblProjectsRollout", "nVendor", tblProjectsRollout.nVendor.ToString(), "", lUserId, nCreateOrUpdate);

            ////----Cost Audit field-----------
            //if (tblProjectsRollout.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 3, "tblProjectsRollout", "cCost", tblProjectsRollout.cCost.ToString(), "", lUserId, nCreateOrUpdate);

            ////----LoopStatus Audit field-----------
            //if (tblProjectsRollout.nLoopStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 1, "tblProjectsRollout", "nLoopStatus", tblProjectsRollout.nLoopStatus.ToString(), "", lUserId, nCreateOrUpdate);

            db.Entry(tblProjectsRollout).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectsRolloutExists(tblProjectsRollout.aProjectsRolloutID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProjectAudios
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(ProjectsRolloutModel request)
        {
            tblProjectsRollout tblRollout = request.GetTblProjectsRollout();
            try
            {
                // var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectsRollout set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectsRollout.nStoreId));
                //tblProjectsRollout.ProjectActiveStatus = 1; SantoshPP
                tblRollout.aProjectsRolloutID = 0;
                // Misc.Utilities.SetActiveProjectId(Misc.ProjectType.AudioInstallation, tblProjectsRollout.nStoreId, tblProjectsRollout);
                db.tblProjectsRollouts.Add(tblRollout);
                await db.SaveChangesAsync();

                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                Nullable<int> lUserId = securityContext.nUserID;
                int nBrandID = 6;
                int nCreateOrUpdate = 1;
                foreach (RolloutItem item in request.uploadingRows)
                {

                    if (item.type == ProjectType.OrderAccuracyInstallation)
                    {
                        await CreateNewStoresForOrderAccurcy(item.items);
                    }
                    else if (item.type == ProjectType.OrderStatusBoardInstallation)
                    {
                        await CreateStoreFromExcelForOrderStatusBoard(item.items);
                    }
                }
                ////----Vendor Audit field-----------
                //if (tblProjectsRollout.nVendor != null)
                //    Misc.Utilities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 1, "tblProjectsRollout", "nVendor", tblProjectsRollout.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
                ////----nStatus Audit field-----------
                //if (tblProjectsRollout.nStatus != null)
                //    Misc.Util
                //    ities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 1, "tblProjectsRollout", "nStatus", tblProjectsRollout.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            }
            catch (Exception ex)
            {

            }
            return Json(tblRollout);
        }

        public async Task<HttpResponseMessage> CreateNewStoresForOrderAccurcy(List<dynamic> requestArr)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                foreach (var tItem in requestArr)
                {
                    int nBrandId = 6;
                    ProjectType pType;
                    ProjectExcelFieldsOrderAccurcy request = JsonConvert.DeserializeObject<ProjectExcelFieldsOrderAccurcy>(JsonConvert.SerializeObject(tItem));
                    List<SqlParameter> tPramList = new List<SqlParameter>();
                    tPramList.Add(new SqlParameter("@tStoreName", "Dont Know"));
                    tPramList.Add(new SqlParameter("@tProjectType", request.tProjectType));
                    tPramList.Add(new SqlParameter("@tStoreNumber", request.tStoreNumber));
                    tPramList.Add(new SqlParameter("@tAddress", request.tAddress));
                    tPramList.Add(new SqlParameter("@tCity", request.tCity));
                    tPramList.Add(new SqlParameter("@tState", request.tState));
                    tPramList.Add(new SqlParameter("@nDMAID", request.nDMAID));
                    tPramList.Add(new SqlParameter("@tDMA", request.tDMA));
                    tPramList.Add(new SqlParameter("@tRED", request.tRED));
                    tPramList.Add(new SqlParameter("@tCM", request.tCM));
                    tPramList.Add(new SqlParameter("@tANE", request.tANE));
                    tPramList.Add(new SqlParameter("@tRVP", request.tRVP));
                    tPramList.Add(new SqlParameter("@tPrincipalPartner", request.tPrincipalPartner));
                    tPramList.Add(new SqlParameter("@dStatus", request.tState));
                    tPramList.Add(new SqlParameter("@dOpenStore", request.dOpenStore));
                    tPramList.Add(new SqlParameter("@tProjectStatus", request.tProjectStatus));
                    tPramList.Add(new SqlParameter("@nCreatedBy", securityContext.nUserID));
                    tPramList.Add(new SqlParameter("@nBrandId", nBrandId));
                    //
                    tPramList.Add(new SqlParameter("@tOrderAccuracyVendor", request.tOrderAccuracyVendor));
                    tPramList.Add(new SqlParameter("@tOrderAccuracyStatus", request.tOrderAccuracyStatus));
                    tPramList.Add(new SqlParameter("@nBakeryPrinter", request.nBakeryPrinter));
                    tPramList.Add(new SqlParameter("@nDualCupLabel", request.nDualCupLabel));
                    tPramList.Add(new SqlParameter("@nDTExpo", request.nDTExpo));
                    tPramList.Add(new SqlParameter("@nFCExpo", request.nFCExpo));
                    tPramList.Add(new SqlParameter("@dShipDate", request.dShipDate));
                    tPramList.Add(new SqlParameter("@tShippingCarrier", request.tShippingCarrier));
                    tPramList.Add(new SqlParameter("@tTrackingNumber", request.tTrackingNumber));
                    tPramList.Add(new SqlParameter("@dDeliveryDate", request.dDeliveryDate));
                    //
                    tPramList.Add(new SqlParameter("@tInstallationVendor", request.tInstallationVendor));
                    tPramList.Add(new SqlParameter("@tInstallStatus", request.tInstallStatus));
                    tPramList.Add(new SqlParameter("@dInstallDate", request.dInstallDate));
                    tPramList.Add(new SqlParameter("@tInstallTime", request.tInstallTime));
                    tPramList.Add(new SqlParameter("@tInstallTechNumber", request.tInstallTechNumber));
                    tPramList.Add(new SqlParameter("@tManagerName", request.tManagerName));
                    tPramList.Add(new SqlParameter("@tManagerNumber", request.tManagerNumber));
                    tPramList.Add(new SqlParameter("@tManagerCheckout", request.tManagerCheckout));
                    tPramList.Add(new SqlParameter("@tPhotoDeliverables", request.tPhotoDeliverables));
                    tPramList.Add(new SqlParameter("@tLeadTech", request.tLeadTech));
                    tPramList.Add(new SqlParameter("@dInstallEnd", request.dInstallEnd));
                    tPramList.Add(new SqlParameter("@tSignoffs", request.tSignoffs));
                    tPramList.Add(new SqlParameter("@tTestTransactions", request.tTestTransactions));
                    tPramList.Add(new SqlParameter("@tInstallProjectStatus", request.tInstallProjectStatus));
                    tPramList.Add(new SqlParameter("@dRevisitDate", request.dRevisitDate));
                    tPramList.Add(new SqlParameter("@tCost", request.tCost));
                    tPramList.Add(new SqlParameter("@tInstallNotes", request.tInstallNotes));
                    tPramList.Add(new SqlParameter("@tInstallType", request.tInstallType));
                    string output = db.Database.SqlQuery<Dropdown>("exec sproc_CreateStoreFromExcelForOrderAccuracy @tStoreName,@tProjectType," +
                        "@tStoreNumber,@tAddress,@tCity,@tState,@nDMAID,@tDMA,@tRED,@tCM," +
                        "@tANE,@tRVP,@tPrincipalPartner,@dStatus,@dOpenStore,@tProjectStatus,@nCreatedBy,@nBrandId," +
                        "@tOrderAccuracyVendor,@tOrderAccuracyStatus,@nBakeryPrinter,@nDualCupLabel,@nDTExpo,@nFCExpo,@dShipDate,@tShippingCarrier,@tTrackingNumber,@dDeliveryDate," +
                        "@tInstallationVendor,@tInstallStatus,@dInstallDate,@tInstallTime,@tInstallTechNumber,@tManagerName,@tManagerNumber," +
                        "@tManagerCheckout,@tPhotoDeliverables,@tLeadTech,@dInstallEnd,@tSignoffs,@tTestTransactions,@tInstallProjectStatus," +
                        "@dRevisitDate,@tCost,@tInstallNotes,@tInstallType "
                        , tPramList[0], tPramList[1], tPramList[2], tPramList[3], tPramList[4], tPramList[5],
                        tPramList[6], tPramList[7], tPramList[8], tPramList[9], tPramList[10], tPramList[11], tPramList[12], tPramList[13],
                        tPramList[14], tPramList[15], tPramList[16], tPramList[17], tPramList[18], tPramList[19], tPramList[20], tPramList[21],
                        tPramList[22], tPramList[23], tPramList[24], tPramList[25], tPramList[26], tPramList[27], tPramList[28], tPramList[29],
                        tPramList[30], tPramList[31], tPramList[32], tPramList[33], tPramList[34], tPramList[35], tPramList[36], tPramList[37],
                        tPramList[38], tPramList[39], tPramList[40], tPramList[41], tPramList[42], tPramList[43], tPramList[44], tPramList[45]).ToString();
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
        [HttpPost]
        public async Task<HttpResponseMessage> CreateStoreFromExcelForOrderStatusBoard(List<dynamic> requestArr)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                foreach (var tItem in requestArr)
                {
                    int nBrandId = 6;
                    ProjectType pType;
                    ProjectExcelFieldsOrderStatusBoard request = JsonConvert.DeserializeObject<ProjectExcelFieldsOrderStatusBoard>(JsonConvert.SerializeObject(tItem));
                    List<SqlParameter> tPramList = new List<SqlParameter>();
                    tPramList.Add(new SqlParameter("@tStoreName", "Dont Know"));
                    tPramList.Add(new SqlParameter("@tProjectType", request.tProjectType));
                    tPramList.Add(new SqlParameter("@tStoreNumber", request.tStoreNumber));
                    tPramList.Add(new SqlParameter("@tAddress", request.tAddress));
                    tPramList.Add(new SqlParameter("@tCity", request.tCity));
                    tPramList.Add(new SqlParameter("@tState", request.tState));
                    tPramList.Add(new SqlParameter("@nDMAID", request.nDMAID));
                    tPramList.Add(new SqlParameter("@tDMA", request.tDMA));
                    tPramList.Add(new SqlParameter("@tRED", request.tRED));
                    tPramList.Add(new SqlParameter("@tCM", request.tCM));
                    tPramList.Add(new SqlParameter("@tANE", request.tANE));
                    tPramList.Add(new SqlParameter("@tRVP", request.tRVP));
                    tPramList.Add(new SqlParameter("@tPrincipalPartner", request.tPrincipalPartner));
                    tPramList.Add(new SqlParameter("@dStatus", request.tState));
                    tPramList.Add(new SqlParameter("@dOpenStore", request.dOpenStore));
                    tPramList.Add(new SqlParameter("@tProjectStatus", request.tProjectStatus));
                    tPramList.Add(new SqlParameter("@nCreatedBy", securityContext.nUserID));
                    tPramList.Add(new SqlParameter("@nBrandId", nBrandId));
                    //
                    tPramList.Add(new SqlParameter("@tOrderStatusBoardVendor", request.tOrderStatusBoardVendor));
                    tPramList.Add(new SqlParameter("@tOrderStatusBoardStatus", request.tOrderStatusBoardStatus));
                    tPramList.Add(new SqlParameter("@nOSB", request.nOSB));
                    tPramList.Add(new SqlParameter("@dShipDate", request.dShipDate));
                    tPramList.Add(new SqlParameter("@tShippingCarrier", request.tShippingCarrier));
                    tPramList.Add(new SqlParameter("@tTrackingNumber", request.tTrackingNumber));
                    tPramList.Add(new SqlParameter("@dDeliveryDate", request.dDeliveryDate));
                    //
                    tPramList.Add(new SqlParameter("@tInstallationVendor", request.tInstallationVendor));
                    tPramList.Add(new SqlParameter("@tInstallStatus", request.tInstallStatus));
                    tPramList.Add(new SqlParameter("@dInstallDate", request.dInstallDate));
                    tPramList.Add(new SqlParameter("@tInstallTime", request.tInstallTime));
                    tPramList.Add(new SqlParameter("@tInstallTechNumber", request.tInstallTechNumber));
                    tPramList.Add(new SqlParameter("@tManagerName", request.tManagerName));
                    tPramList.Add(new SqlParameter("@tManagerNumber", request.tManagerNumber));
                    tPramList.Add(new SqlParameter("@tManagerCheckout", request.tManagerCheckout));
                    tPramList.Add(new SqlParameter("@tPhotoDeliverables", request.tPhotoDeliverables));
                    tPramList.Add(new SqlParameter("@tLeadTech", request.tLeadTech));
                    tPramList.Add(new SqlParameter("@dInstallEnd", request.dInstallEnd));
                    tPramList.Add(new SqlParameter("@tSignoffs", request.tSignoffs));
                    tPramList.Add(new SqlParameter("@tTestTransactions", request.tTestTransactions));
                    tPramList.Add(new SqlParameter("@tInstallProjectStatus", request.tInstallProjectStatus));
                    tPramList.Add(new SqlParameter("@dRevisitDate", request.dRevisitDate));
                    tPramList.Add(new SqlParameter("@tCost", request.tCost));
                    tPramList.Add(new SqlParameter("@tInstallNotes", request.tInstallNotes));
                    tPramList.Add(new SqlParameter("@tInstallType", request.tInstallType));
                    string output = db.Database.SqlQuery<Dropdown>("exec sproc_CreateStoreFromExcelForOrderStatusBoard @tStoreName,@tProjectType," +
                        "@tStoreNumber,@tAddress,@tCity,@tState,@nDMAID,@tDMA,@tRED,@tCM," +
                        "@tANE,@tRVP,@tPrincipalPartner,@dStatus,@dOpenStore,@tProjectStatus,@nCreatedBy,@nBrandId," +
                        "@tOrderStatusBoardVendor,@tOrderStatusBoardStatus,@nOSB,@dShipDate,@tShippingCarrier,@tTrackingNumber,@dDeliveryDate," +
                        "@tInstallationVendor,@tInstallStatus,@dInstallDate,@tInstallTime,@tInstallTechNumber,@tManagerName,@tManagerNumber," +
                        "@tManagerCheckout,@tPhotoDeliverables,@tLeadTech,@dInstallEnd,@tSignoffs,@tTestTransactions,@tInstallProjectStatus," +
                        "@dRevisitDate,@tCost,@tInstallNotes,@tInstallType "
                        , tPramList[0], tPramList[1], tPramList[2], tPramList[3], tPramList[4], tPramList[5],
                        tPramList[6], tPramList[7], tPramList[8], tPramList[9], tPramList[10], tPramList[11], tPramList[12], tPramList[13],
                        tPramList[14], tPramList[15], tPramList[16], tPramList[17], tPramList[18], tPramList[19], tPramList[20], tPramList[21],
                        tPramList[22], tPramList[23], tPramList[24], tPramList[25], tPramList[26], tPramList[27], tPramList[28], tPramList[29],
                        tPramList[30], tPramList[31], tPramList[32], tPramList[33], tPramList[34], tPramList[35], tPramList[36], tPramList[37],
                        tPramList[38], tPramList[39], tPramList[40], tPramList[41], tPramList[42]).ToString();
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
        // DELETE: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectsRollout tblProjectsRollout = await db.tblProjectsRollouts.FindAsync(id);
            if (tblProjectsRollout == null)
            {
                return NotFound();
            }

            db.tblProjectsRollouts.Remove(tblProjectsRollout);
            await db.SaveChangesAsync();

            return Ok(tblProjectsRollout);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectsRolloutExists(int id)
        {
            return db.tblProjectsRollouts.Count(e => e.aProjectsRolloutID == id) > 0;
        }
    }
}


