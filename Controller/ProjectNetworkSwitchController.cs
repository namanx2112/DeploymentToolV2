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
        public IQueryable<tblProjectNetworkSwtich> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectNetworkSwtich> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;


                if (nProjectID != 0)
                {
                    return db.tblProjectNetworkSwtiches.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectNetworkSwtich");
                    items = db.Database.SqlQuery<tblProjectNetworkSwtich>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
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
            tblProjectNetworkSwtich tblProjectNetworkSwtich = await db.tblProjectNetworkSwtiches.FindAsync(id);
            if (tblProjectNetworkSwtich == null)
            {
                return NotFound();
            }

            return Ok(tblProjectNetworkSwtich);
        }

        // PUT: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectNetworkSwtich tblProjectNetworkSwtich)
        {

            //tblProjectNetworkSwtich.ProjectActiveStatus = 1;//SantoshPP\
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich);

            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 2;
            ////----Vendor Audit field-----------
            // if (tblProjectNetworkSwtich.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 1, "tblProjectNetworkSwtich", "nVendor", tblProjectNetworkSwtich.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectNetworkSwtich.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 1, "tblProjectNetworkSwtich", "nStatus", tblProjectNetworkSwtich.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nNumberOfTabletsPerStore Audit field-----------
            //if (tblProjectNetworkSwtich.nNumberOfTabletsPerStore != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 1, "tblProjectNetworkSwtich", "nNumberOfTabletsPerStore", tblProjectNetworkSwtich.nNumberOfTabletsPerStore.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            //if (tblProjectNetworkSwtich.dDeliveryDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectNetworkSwtich.dDeliveryDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 4, "tblProjectNetworkSwtich", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //} //----dShipDate Audit field-----------
            //if (tblProjectNetworkSwtich.dShipDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectNetworkSwtich.dShipDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 4, "tblProjectNetworkSwtich", "dShipDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            //////----Cost Audit field-----------
            ////if (tblProjectNetworkSwtich.cCost != null)
            ////    Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 1, "tblProjectNetworkSwtich", "cCost", tblProjectNetworkSwtich.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            db.Entry(tblProjectNetworkSwtich).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectAudioExists(tblProjectNetworkSwtich.aProjectNetworkSwtichID))
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
        public async Task<IHttpActionResult> Create(tblProjectNetworkSwtich tblProjectNetworkSwtich)
        {
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectNetworkSwtich set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectNetworkSwtich.nStoreId));
            //tblProjectNetworkSwtich.ProjectActiveStatus = 1; SantoshPP
            tblProjectNetworkSwtich.aProjectNetworkSwtichID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich);
            db.tblProjectNetworkSwtiches.Add(tblProjectNetworkSwtich);
            await db.SaveChangesAsync();

            //tblProjectNetworkSwtiches. = tblProjectNetworkSwtich.aProjectNetworkSwtichID;
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 1;
            ////----Vendor Audit field-----------
            //if (tblProjectNetworkSwtich.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 1, "tblProjectNetworkSwtich", "nVendor", tblProjectNetworkSwtich.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectNetworkSwtich.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 1, "tblProjectNetworkSwtich", "nStatus", tblProjectNetworkSwtich.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nNumberOfTabletsPerStore Audit field-----------
            //if (tblProjectNetworkSwtich.nNumberOfTabletsPerStore != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 1, "tblProjectNetworkSwtich", "nNumberOfTabletsPerStore", tblProjectNetworkSwtich.nNumberOfTabletsPerStore.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            //if (tblProjectNetworkSwtich.dDeliveryDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectNetworkSwtich.dDeliveryDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 4, "tblProjectNetworkSwtich", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //} //----dShipDate Audit field-----------
            //if (tblProjectNetworkSwtich.dShipDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectNetworkSwtich.dShipDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 4, "tblProjectNetworkSwtich", "dShipDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            ////----Cost Audit field-----------
            //if (tblProjectNetworkSwtich.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworkSwtich.nStoreId, tblProjectNetworkSwtich.nProjectID, 1, "tblProjectNetworkSwtich", "cCost", tblProjectNetworkSwtich.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            return Json(tblProjectNetworkSwtich);
        }

        // DELETE: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectNetworkSwtich tblProjectNetworkSwtich = await db.tblProjectNetworkSwtiches.FindAsync(id);
            if (tblProjectNetworkSwtich == null)
            {
                return NotFound();
            }

            db.tblProjectNetworkSwtiches.Remove(tblProjectNetworkSwtich);
            await db.SaveChangesAsync();

            return Ok(tblProjectNetworkSwtich);
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
            return db.tblProjectNetworkSwtiches.Count(e => e.aProjectNetworkSwtichID == id) > 0;
        }
    }
}
