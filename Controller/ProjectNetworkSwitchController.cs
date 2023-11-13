using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class ProjectNetworkSwitchController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectAudios
        public IQueryable<tblProjectNetworkSwitch> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectNetworkSwitch> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;


                if (nProjectID != 0)
                {
                    return db.tblProjectNetworkSwitches.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectNetworkSwitch");
                    items = db.Database.SqlQuery<tblProjectNetworkSwitch>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
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
            tblProjectNetworkSwitch tblProjectNetworkSwitch = await db.tblProjectNetworkSwitches.FindAsync(id);
            if (tblProjectNetworkSwitch == null)
            {
                return NotFound();
            }

            return Ok(tblProjectNetworkSwitch);
        }

        // PUT: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectNetworkSwitch tblProjectNetworkSwitch)
        {

            //tblProjectNetworkSwitch.ProjectActiveStatus = 1;//SantoshPP\
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch);

            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 2;
            ////----Vendor Audit field-----------
            // if (tblProjectNetworkSwitch.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 1, "tblProjectNetworkSwitch", "nVendor", tblProjectNetworkSwitch.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectNetworkSwitch.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 1, "tblProjectNetworkSwitch", "nStatus", tblProjectNetworkSwitch.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nNumberOfTabletsPerStore Audit field-----------
            //if (tblProjectNetworkSwitch.nNumberOfTabletsPerStore != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 1, "tblProjectNetworkSwitch", "nNumberOfTabletsPerStore", tblProjectNetworkSwitch.nNumberOfTabletsPerStore.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            //if (tblProjectNetworkSwitch.dDeliveryDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectNetworkSwitch.dDeliveryDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 4, "tblProjectNetworkSwitch", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //} //----dShipDate Audit field-----------
            //if (tblProjectNetworkSwitch.dShipDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectNetworkSwitch.dShipDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 4, "tblProjectNetworkSwitch", "dShipDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            //////----Cost Audit field-----------
            ////if (tblProjectNetworkSwitch.cCost != null)
            ////    Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 1, "tblProjectNetworkSwitch", "cCost", tblProjectNetworkSwitch.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            db.Entry(tblProjectNetworkSwitch).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectAudioExists(tblProjectNetworkSwitch.aProjectNetworkSwtichID))
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
        public async Task<IHttpActionResult> Create(tblProjectNetworkSwitch tblProjectNetworkSwitch)
        {
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectNetworkSwitch set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectNetworkSwitch.nStoreId));
            //tblProjectNetworkSwitch.ProjectActiveStatus = 1; SantoshPP
            tblProjectNetworkSwitch.aProjectNetworkSwtichID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch);
            db.tblProjectNetworkSwitches.Add(tblProjectNetworkSwitch);
            await db.SaveChangesAsync();

            //tblProjectNetworkSwitches. = tblProjectNetworkSwitch.aProjectNetworkSwtichID;
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 1;
            ////----Vendor Audit field-----------
            //if (tblProjectNetworkSwitch.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 1, "tblProjectNetworkSwitch", "nVendor", tblProjectNetworkSwitch.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectNetworkSwitch.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 1, "tblProjectNetworkSwitch", "nStatus", tblProjectNetworkSwitch.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nNumberOfTabletsPerStore Audit field-----------
            //if (tblProjectNetworkSwitch.nNumberOfTabletsPerStore != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 1, "tblProjectNetworkSwitch", "nNumberOfTabletsPerStore", tblProjectNetworkSwitch.nNumberOfTabletsPerStore.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            //if (tblProjectNetworkSwitch.dDeliveryDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectNetworkSwitch.dDeliveryDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 4, "tblProjectNetworkSwitch", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //} //----dShipDate Audit field-----------
            //if (tblProjectNetworkSwitch.dShipDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectNetworkSwitch.dShipDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 4, "tblProjectNetworkSwitch", "dShipDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            ////----Cost Audit field-----------
            //if (tblProjectNetworkSwitch.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwitch.nStoreId, tblProjectNetworkSwitch.nProjectID, 1, "tblProjectNetworkSwitch", "cCost", tblProjectNetworkSwitch.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            return Json(tblProjectNetworkSwitch);
        }

        // DELETE: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectNetworkSwitch tblProjectNetworkSwitch = await db.tblProjectNetworkSwitches.FindAsync(id);
            if (tblProjectNetworkSwitch == null)
            {
                return NotFound();
            }

            db.tblProjectNetworkSwitches.Remove(tblProjectNetworkSwitch);
            await db.SaveChangesAsync();

            return Ok(tblProjectNetworkSwitch);
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
            return db.tblProjectNetworkSwitches.Count(e => e.aProjectNetworkSwtichID == id) > 0;
        }
    }
}
