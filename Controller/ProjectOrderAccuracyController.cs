using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class ProjectOrderAccuracyController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectAudios
        public IQueryable<tblProjectOrderAccuracy> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectOrderAccuracy> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;


                if (nProjectID != 0)
                {
                    return db.tblProjectOrderAccuracies.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectOrderAccuracy");
                    items = db.Database.SqlQuery<tblProjectOrderAccuracy>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    //return items;
                }
            }
            catch (Exception ex) { }
            return items;

        }

        // GET: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> GettblProjectAudio(int id)
        {
            tblProjectOrderAccuracy tblProjectOrderAccuracy = await db.tblProjectOrderAccuracies.FindAsync(id);
            if (tblProjectOrderAccuracy == null)
            {
                return NotFound();
            }

            return Ok(tblProjectOrderAccuracy);
        }

        // PUT: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectOrderAccuracy tblProjectOrderAccuracy)
        {

            ////tblProjectOrderAccuracy.ProjectActiveStatus = 1;//SantoshPP\
            //Misc.Utilities.SetActiveProjectId(Misc.ProjectType.ServerHandheld, tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy);

            //var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            //int nCreateOrUpdate = 2;
            //////----Vendor Audit field-----------
            //// if (tblProjectOrderAccuracy.nVendor != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 1, "tblProjectOrderAccuracy", "nVendor", tblProjectOrderAccuracy.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nStatus Audit field-----------
            //if (tblProjectOrderAccuracy.nStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 1, "tblProjectOrderAccuracy", "nStatus", tblProjectOrderAccuracy.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //////----nNumberOfTabletsPerStore Audit field-----------
            ////if (tblProjectOrderAccuracy.nNumberOfTabletsPerStore != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 1, "tblProjectOrderAccuracy", "nNumberOfTabletsPerStore", tblProjectOrderAccuracy.nNumberOfTabletsPerStore.ToString(), "", lUserId, nCreateOrUpdate);
            ////----DeliveryDate Audit field-----------
            //if (tblProjectOrderAccuracy.dDeliveryDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectOrderAccuracy.dDeliveryDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 4, "tblProjectOrderAccuracy", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //} //----dShipDate Audit field-----------
            //if (tblProjectOrderAccuracy.dShipDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectOrderAccuracy.dShipDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 4, "tblProjectOrderAccuracy", "dShipDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            //////----Cost Audit field-----------
            ////if (tblProjectOrderAccuracy.cCost != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 1, "tblProjectOrderAccuracy", "cCost", tblProjectOrderAccuracy.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            //db.Entry(tblProjectOrderAccuracy).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!tblProjectAudioExists(tblProjectOrderAccuracy.aServerHandheldId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProjectAudios
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProjectOrderAccuracy tblProjectOrderAccuracy)
        {
            //var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectOrderAccuracy set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectOrderAccuracy.nStoreId));
            ////tblProjectOrderAccuracy.ProjectActiveStatus = 1; SantoshPP
            //tblProjectOrderAccuracy.aServerHandheldId = 0;
            //Misc.Utilities.SetActiveProjectId(Misc.ProjectType.ServerHandheld, tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy);
            //db.tblProjectOrderAccuracys.Add(tblProjectOrderAccuracy);
            //await db.SaveChangesAsync();

            //var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            //int nCreateOrUpdate = 1;
            //////----Vendor Audit field-----------
            ////if (tblProjectOrderAccuracy.nVendor != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 1, "tblProjectOrderAccuracy", "nVendor", tblProjectOrderAccuracy.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nStatus Audit field-----------
            //if (tblProjectOrderAccuracy.nStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 1, "tblProjectOrderAccuracy", "nStatus", tblProjectOrderAccuracy.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //////----nNumberOfTabletsPerStore Audit field-----------
            ////if (tblProjectOrderAccuracy.nNumberOfTabletsPerStore != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 1, "tblProjectOrderAccuracy", "nNumberOfTabletsPerStore", tblProjectOrderAccuracy.nNumberOfTabletsPerStore.ToString(), "", lUserId, nCreateOrUpdate);
            ////----DeliveryDate Audit field-----------
            //if (tblProjectOrderAccuracy.dDeliveryDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectOrderAccuracy.dDeliveryDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 4, "tblProjectOrderAccuracy", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //} //----dShipDate Audit field-----------
            //if (tblProjectOrderAccuracy.dShipDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectOrderAccuracy.dShipDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 4, "tblProjectOrderAccuracy", "dShipDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            //////----Cost Audit field-----------
            ////if (tblProjectOrderAccuracy.cCost != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderAccuracy.nStoreId, tblProjectOrderAccuracy.nProjectID, 1, "tblProjectOrderAccuracy", "cCost", tblProjectOrderAccuracy.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            return Json(tblProjectOrderAccuracy);
        }

        // DELETE: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectOrderAccuracy tblProjectOrderAccuracy = await db.tblProjectOrderAccuracies.FindAsync(id);
            if (tblProjectOrderAccuracy == null)
            {
                return NotFound();
            }

            db.tblProjectOrderAccuracies.Remove(tblProjectOrderAccuracy);
            await db.SaveChangesAsync();

            return Ok(tblProjectOrderAccuracy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectAudioExists(int id)
        {
            return db.tblProjectOrderAccuracies.Count(e => e.aProjectOrderAccuracyID == id) > 0;
        }
    }
}
