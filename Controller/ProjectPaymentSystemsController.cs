using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;
using DeploymentTool.Model;

namespace DeploymentTool.Controller
{
    public class ProjectPaymentSystemsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectPaymentSystems
        public IQueryable<tblProjectPaymentSystem> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectPaymentSystem> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

                if (nProjectID != 0)
                {
                    return db.tblProjectPaymentSystems.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectPaymentSystem");
                    items = db.Database.SqlQuery<tblProjectPaymentSystem>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    
                }
            }
            catch (Exception ex) { }
            return items;
        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectPaymentSystems/5
        [ResponseType(typeof(tblProjectPaymentSystem))]
        public async Task<IHttpActionResult> GettblProjectPaymentSystem(int id)
        {
            tblProjectPaymentSystem tblProjectPaymentSystem = await db.tblProjectPaymentSystems.FindAsync(id);
            if (tblProjectPaymentSystem == null)
            {
                return NotFound();
            }

            return Ok(tblProjectPaymentSystem);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectPaymentSystems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(tblProjectPaymentSystem tblProjectPaymentSystem)
        {
            //tblProjectPaymentSystem.ProjectActiveStatus = 1;Santosh
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.PaymentTerminalInstallation, tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem);

            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 2;
            ////----Vendor Audit field-----------
            //if (tblProjectPaymentSystem.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nVendor", tblProjectPaymentSystem.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectPaymentSystem.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nStatus", tblProjectPaymentSystem.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            if (tblProjectPaymentSystem.dDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectPaymentSystem.dDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 4, "tblProjectPaymentSystem", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            } 
            ////----cCost Audit field-----------
            //if (tblProjectPaymentSystem.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 3, "tblProjectPaymentSystem", "cCost", tblProjectPaymentSystem.cCost.ToString(), "", lUserId, nCreateOrUpdate);

            ////----nBuyPassID Audit field-----------
            //if (tblProjectPaymentSystem.nBuyPassID != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nBuyPassID", tblProjectPaymentSystem.nBuyPassID.ToString(), "", lUserId, nCreateOrUpdate);


            ////----nServerEPS Audit field-----------
            //if (tblProjectPaymentSystem.nServerEPS != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nServerEPS", tblProjectPaymentSystem.nServerEPS.ToString(), "", lUserId, nCreateOrUpdate);

            ////----nPAYSUnits Audit field-----------
            //if (tblProjectPaymentSystem.nPAYSUnits != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nPAYSUnits", tblProjectPaymentSystem.nPAYSUnits.ToString(), "", lUserId, nCreateOrUpdate);

            ////----n45Enclosures Audit field-----------
            //if (tblProjectPaymentSystem.n45Enclosures != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "n45Enclosures", tblProjectPaymentSystem.n45Enclosures.ToString(), "", lUserId, nCreateOrUpdate);
            ////----n90Enclosures Audit field-----------
            //if (tblProjectPaymentSystem.n90Enclosures != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "n90Enclosures", tblProjectPaymentSystem.n90Enclosures.ToString(), "", lUserId, nCreateOrUpdate);


            ////----nDTEnclosures Audit field-----------
            //if (tblProjectPaymentSystem.nDTEnclosures != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nDTEnclosures", tblProjectPaymentSystem.nDTEnclosures.ToString(), "", lUserId, nCreateOrUpdate);


            ////----n15SunShields Audit field-----------
            //if (tblProjectPaymentSystem.n15SunShields != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "n15SunShields", tblProjectPaymentSystem.n15SunShields.ToString(), "", lUserId, nCreateOrUpdate);

            ////----nUPS Audit field-----------
            //if (tblProjectPaymentSystem.nUPS != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nUPS", tblProjectPaymentSystem.nUPS.ToString(), "", lUserId, nCreateOrUpdate);

            ////----nShelf Audit field-----------
            //if (tblProjectPaymentSystem.nShelf != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nShelf", tblProjectPaymentSystem.nShelf.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nType Audit field-----------
            //if (tblProjectPaymentSystem.nType != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nType", tblProjectPaymentSystem.nType.ToString(), "", lUserId, nCreateOrUpdate);

            db.Entry(tblProjectPaymentSystem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectPaymentSystemExists(tblProjectPaymentSystem.aProjectPaymentSystemID))
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
        [Authorize]
        [HttpPost]
        // POST: api/ProjectPaymentSystems
        [ResponseType(typeof(tblProjectPaymentSystem))]
        public async Task<IHttpActionResult> Create(tblProjectPaymentSystem tblProjectPaymentSystem)
        {
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectPaymentSystem set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectPaymentSystem.nStoreId));

            //tblProjectPaymentSystem.ProjectActiveStatus = 1;Santosh
            tblProjectPaymentSystem.aProjectPaymentSystemID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.PaymentTerminalInstallation, tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem);
            db.tblProjectPaymentSystems.Add(tblProjectPaymentSystem);
            await db.SaveChangesAsync();

            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 1;
            ////----Vendor Audit field-----------
            //if (tblProjectPaymentSystem.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nVendor", tblProjectPaymentSystem.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectPaymentSystem.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nStatus", tblProjectPaymentSystem.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            if (tblProjectPaymentSystem.dDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectPaymentSystem.dDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 4, "tblProjectPaymentSystem", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            } //----Configuration Audit field-----------

            ////----cCost Audit field-----------
            //if (tblProjectPaymentSystem.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 3, "tblProjectPaymentSystem", "cCost", tblProjectPaymentSystem.cCost.ToString(), "", lUserId, nCreateOrUpdate);

            ////----nBuyPassID Audit field-----------
            //if (tblProjectPaymentSystem.nBuyPassID != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nBuyPassID", tblProjectPaymentSystem.nBuyPassID.ToString(), "", lUserId, nCreateOrUpdate);


            ////----nServerEPS Audit field-----------
            //if (tblProjectPaymentSystem.nServerEPS != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nServerEPS", tblProjectPaymentSystem.nServerEPS.ToString(), "", lUserId, nCreateOrUpdate);

            ////----nPAYSUnits Audit field-----------
            //if (tblProjectPaymentSystem.nPAYSUnits != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nPAYSUnits", tblProjectPaymentSystem.nPAYSUnits.ToString(), "", lUserId, nCreateOrUpdate);

            ////----n45Enclosures Audit field-----------
            //if (tblProjectPaymentSystem.n45Enclosures != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "n45Enclosures", tblProjectPaymentSystem.n45Enclosures.ToString(), "", lUserId, nCreateOrUpdate);
            ////----n90Enclosures Audit field-----------
            //if (tblProjectPaymentSystem.n90Enclosures != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "n90Enclosures", tblProjectPaymentSystem.n90Enclosures.ToString(), "", lUserId, nCreateOrUpdate);


            ////----nDTEnclosures Audit field-----------
            //if (tblProjectPaymentSystem.nDTEnclosures != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nDTEnclosures", tblProjectPaymentSystem.nDTEnclosures.ToString(), "", lUserId, nCreateOrUpdate);


            ////----n15SunShields Audit field-----------
            //if (tblProjectPaymentSystem.n15SunShields != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "n15SunShields", tblProjectPaymentSystem.n15SunShields.ToString(), "", lUserId, nCreateOrUpdate);

            ////----nUPS Audit field-----------
            //if (tblProjectPaymentSystem.nUPS != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nUPS", tblProjectPaymentSystem.nUPS.ToString(), "", lUserId, nCreateOrUpdate);

            ////----nShelf Audit field-----------
            //if (tblProjectPaymentSystem.nShelf != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nShelf", tblProjectPaymentSystem.nShelf.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nType Audit field-----------
            //if (tblProjectPaymentSystem.nType != null)
            //    Misc.Utilities.AddToAudit(tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem.nProjectID, 1, "tblProjectPaymentSystem", "nType", tblProjectPaymentSystem.nType.ToString(), "", lUserId, nCreateOrUpdate);

            return Json(tblProjectPaymentSystem);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectPaymentSystems/5
        [ResponseType(typeof(tblProjectPaymentSystem))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectPaymentSystem tblProjectPaymentSystem = await db.tblProjectPaymentSystems.FindAsync(id);
            if (tblProjectPaymentSystem == null)
            {
                return NotFound();
            }

            db.tblProjectPaymentSystems.Remove(tblProjectPaymentSystem);
            await db.SaveChangesAsync();

            return Ok(tblProjectPaymentSystem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectPaymentSystemExists(int id)
        {
            return db.tblProjectPaymentSystems.Count(e => e.aProjectPaymentSystemID == id) > 0;
        }
    }
}