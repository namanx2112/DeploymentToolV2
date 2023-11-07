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
    public class ProjectInteriorMenusController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectInteriorMenus
        public IQueryable<tblProjectInteriorMenu> Get(Dictionary<string, string> searchFields)
        {

            IQueryable<tblProjectInteriorMenu> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

                if (nProjectID != 0)
                {
                    return db.tblProjectInteriorMenus.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectInteriorMenus");
                    items = db.Database.SqlQuery<tblProjectInteriorMenu>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    
                }
            }catch (Exception ex) { }
            return items;

        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectInteriorMenus/5
        [ResponseType(typeof(tblProjectInteriorMenu))]
        public async Task<IHttpActionResult> GettblProjectInteriorMenu(int id)
        {
            tblProjectInteriorMenu tblProjectInteriorMenu = await db.tblProjectInteriorMenus.FindAsync(id);
            if (tblProjectInteriorMenu == null)
            {
                return NotFound();
            }

            return Ok(tblProjectInteriorMenu);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectInteriorMenus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(tblProjectInteriorMenu tblProjectInteriorMenu)
        {
            //tblProjectInteriorMenu.ProjectActiveStatus = 1;Santosh
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.InteriorMenuInstallation, tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu);
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 2;
            ////----Vendor Audit field-----------
            //if (tblProjectInteriorMenu.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu.nProjectID, 1, "tblProjectInteriorMenus", "nVendor", tblProjectInteriorMenu.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectInteriorMenu.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu.nProjectID, 1, "tblProjectInteriorMenus", "nStatus", tblProjectInteriorMenu.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nDMBQuantity Audit field-----------
            //if (tblProjectInteriorMenu.nDMBQuantity != null)
            //    Misc.Utilities.AddToAudit(tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu.nProjectID, 1, "tblProjectInteriorMenus", "nDMBQuantity", tblProjectInteriorMenu.nDMBQuantity.ToString(), "", lUserId, nCreateOrUpdate);
            //----dDeliveryDate Audit field-----------
            if (tblProjectInteriorMenu.dDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectInteriorMenu.dDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu.nProjectID, 4, "tblProjectInteriorMenus", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }           
            ////----cCost Audit field-----------
            //if (tblProjectInteriorMenu.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu.nProjectID, 3, "tblProjectInteriorMenus", "cCost", tblProjectInteriorMenu.cCost.ToString(), "", lUserId, nCreateOrUpdate);

            db.Entry(tblProjectInteriorMenu).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectInteriorMenuExists(tblProjectInteriorMenu.aProjectInteriorMenuID))
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
        // POST: api/ProjectInteriorMenus
        [ResponseType(typeof(tblProjectInteriorMenu))]
        public async Task<IHttpActionResult> Create(tblProjectInteriorMenu tblProjectInteriorMenu)
        {

            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectInteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectInteriorMenu.nStoreId));

            //tblProjectInteriorMenu.ProjectActiveStatus = 1;Santosh
            tblProjectInteriorMenu.aProjectInteriorMenuID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.InteriorMenuInstallation, tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu);
            db.tblProjectInteriorMenus.Add(tblProjectInteriorMenu);
            await db.SaveChangesAsync();
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 1;
            ////----Vendor Audit field-----------
            //if (tblProjectInteriorMenu.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu.nProjectID, 1, "tblProjectInteriorMenus", "nVendor", tblProjectInteriorMenu.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectInteriorMenu.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu.nProjectID, 1, "tblProjectInteriorMenus", "nStatus", tblProjectInteriorMenu.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nDMBQuantity Audit field-----------
            //if (tblProjectInteriorMenu.nDMBQuantity != null)
            //    Misc.Utilities.AddToAudit(tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu.nProjectID, 1, "tblProjectInteriorMenus", "nDMBQuantity", tblProjectInteriorMenu.nDMBQuantity.ToString(), "", lUserId, nCreateOrUpdate);
            //----dDeliveryDate Audit field-----------
            if (tblProjectInteriorMenu.dDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectInteriorMenu.dDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu.nProjectID, 4, "tblProjectInteriorMenus", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            ////----cCost Audit field-----------
            //if (tblProjectInteriorMenu.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectInteriorMenu.nStoreId, tblProjectInteriorMenu.nProjectID, 3, "tblProjectInteriorMenus", "cCost", tblProjectInteriorMenu.cCost.ToString(), "", lUserId, nCreateOrUpdate);

            return Json(tblProjectInteriorMenu);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectInteriorMenus/5
        [ResponseType(typeof(tblProjectInteriorMenu))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectInteriorMenu tblProjectInteriorMenu = await db.tblProjectInteriorMenus.FindAsync(id);
            if (tblProjectInteriorMenu == null)
            {
                return NotFound();
            }

            db.tblProjectInteriorMenus.Remove(tblProjectInteriorMenu);
            await db.SaveChangesAsync();

            return Ok(tblProjectInteriorMenu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectInteriorMenuExists(int id)
        {
            return db.tblProjectInteriorMenus.Count(e => e.aProjectInteriorMenuID == id) > 0;
        }
    }
}