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

namespace DeploymentTool.Model
{
    public class ProjectExteriorMenusController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectExteriorMenus
        public IQueryable<tblProjectExteriorMenu> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectExteriorMenu> items = null;
            try
            {
                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

                if (nProjectID != 0)
                {
                    return db.tblProjectExteriorMenus.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectExteriorMenus");
                    items = db.Database.SqlQuery<tblProjectExteriorMenu>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    //return items;
                }
            }
            catch (Exception ex) { }
            return items;


        }

        // GET: api/ProjectExteriorMenus/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> GettblProjectExteriorMenu(int id)
        {
            tblProjectExteriorMenu tblProjectExteriorMenu = await db.tblProjectExteriorMenus.FindAsync(id);
            if (tblProjectExteriorMenu == null)
            {
                return NotFound();
            }

            return Ok(tblProjectExteriorMenu);
        }

        // PUT: api/ProjectExteriorMenus/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectExteriorMenu tblProjectExteriorMenu)
        {
            // tblProjectExteriorMenu.ProjectActiveStatus = 1;//Santosh
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.ExteriorMenuInstallation, tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu);

            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 2;
            //----Vendor Audit field-----------
            //if (tblProjectExteriorMenu.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nVendor", tblProjectExteriorMenu.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectExteriorMenu.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nStatus", tblProjectExteriorMenu.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            if (tblProjectExteriorMenu.dDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectExteriorMenu.dDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 4, "tblProjectExteriorMenus", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            ////----FabConCost Audit field-----------
            //if (tblProjectExteriorMenu.cFabConCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 3, "tblProjectExteriorMenus", "cFabConCost", tblProjectExteriorMenu.cFabConCost.ToString(), "", lUserId, nCreateOrUpdate);

            ////----IDTechCost Audit field-----------
            //if (tblProjectExteriorMenu.cIDTechCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 3, "tblProjectExteriorMenus", "cIDTechCost", tblProjectExteriorMenu.cIDTechCost.ToString(), "", lUserId, nCreateOrUpdate);

            ////----FabConCost Audit field-----------
            //if (tblProjectExteriorMenu.cTotalCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 3, "tblProjectExteriorMenus", "cTotalCost", tblProjectExteriorMenu.cTotalCost.ToString(), "", lUserId, nCreateOrUpdate);

            ////----Stalls Audit field-----------
            //if (tblProjectExteriorMenu.nStalls != null)
            //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nStalls", tblProjectExteriorMenu.nStalls.ToString(), "", lUserId, nCreateOrUpdate);

           
            ////----nPatio Audit field-----------
            //if (tblProjectExteriorMenu.nPatio != null)
            //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nPatio", tblProjectExteriorMenu.nPatio.ToString(), "", lUserId, nCreateOrUpdate);

            ////----nFlat Audit field-----------
            //if (tblProjectExteriorMenu.nFlat != null)
            //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nFlat", tblProjectExteriorMenu.nFlat.ToString(), "", lUserId, nCreateOrUpdate);

            ////----nDTPops Audit field-----------
            //if (tblProjectExteriorMenu.nDTPops != null)
            //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nDTPops", tblProjectExteriorMenu.nDTPops.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nDTMenu Audit field-----------
            //if (tblProjectExteriorMenu.nDTMenu != null)
            //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nDTMenu", tblProjectExteriorMenu.nDTMenu.ToString(), "", lUserId, nCreateOrUpdate);

            db.Entry(tblProjectExteriorMenu).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectExteriorMenuExists(tblProjectExteriorMenu.aProjectExteriorMenuID))
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

        // POST: api/ProjectExteriorMenus
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProjectExteriorMenu tblProjectExteriorMenu)
        {
            try
            {
                var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectExteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectExteriorMenu.nStoreId));
                //tblProjectExteriorMenu.ProjectActiveStatus = 1;Santosh

                tblProjectExteriorMenu.aProjectExteriorMenuID = 0;
                Misc.Utilities.SetActiveProjectId(Misc.ProjectType.ExteriorMenuInstallation, tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu);
                db.tblProjectExteriorMenus.Add(tblProjectExteriorMenu);
                await db.SaveChangesAsync();

                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                Nullable<int> lUserId = securityContext.nUserID;
                int nBrandID = 6;
                int nCreateOrUpdate = 1;
                ////----Vendor Audit field-----------
                //if (tblProjectExteriorMenu.nVendor != null)
                //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nVendor", tblProjectExteriorMenu.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
                //----nStatus Audit field-----------
                if (tblProjectExteriorMenu.nStatus != null)
                    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nStatus", tblProjectExteriorMenu.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
                //----DeliveryDate Audit field-----------
                if (tblProjectExteriorMenu.dDeliveryDate != null)
                {
                    DateTime dt = DateTime.Parse(tblProjectExteriorMenu.dDeliveryDate.ToString());
                    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 4, "tblProjectExteriorMenus", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
                } 
                ////----FabConCost Audit field-----------
                //if (tblProjectExteriorMenu.cFabConCost != null)
                //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 3, "tblProjectExteriorMenus", "cFabConCost", tblProjectExteriorMenu.cFabConCost.ToString(), "", lUserId, nCreateOrUpdate);

                ////----IDTechCost Audit field-----------
                //if (tblProjectExteriorMenu.cIDTechCost != null)
                //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 3, "tblProjectExteriorMenus", "cIDTechCost", tblProjectExteriorMenu.cIDTechCost.ToString(), "", lUserId, nCreateOrUpdate);

                ////----FabConCost Audit field-----------
                //if (tblProjectExteriorMenu.cTotalCost != null)
                //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 3, "tblProjectExteriorMenus", "cTotalCost", tblProjectExteriorMenu.cTotalCost.ToString(), "", lUserId, nCreateOrUpdate);

                ////----Stalls Audit field-----------
                //if (tblProjectExteriorMenu.nStalls != null)
                //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nStalls", tblProjectExteriorMenu.nStalls.ToString(), "", lUserId, nCreateOrUpdate);

             
                ////----nPatio Audit field-----------
                //if (tblProjectExteriorMenu.nPatio != null)
                //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nPatio", tblProjectExteriorMenu.nPatio.ToString(), "", lUserId, nCreateOrUpdate);

                ////----nFlat Audit field-----------
                //if (tblProjectExteriorMenu.nFlat != null)
                //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nFlat", tblProjectExteriorMenu.nFlat.ToString(), "", lUserId, nCreateOrUpdate);

                ////----nDTPops Audit field-----------
                //if (tblProjectExteriorMenu.nDTPops != null)
                //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nDTPops", tblProjectExteriorMenu.nDTPops.ToString(), "", lUserId, nCreateOrUpdate);
                ////----nDTMenu Audit field-----------
                //if (tblProjectExteriorMenu.nDTMenu != null)
                //    Misc.Utilities.AddToAudit(tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu.nProjectID, 1, "tblProjectExteriorMenus", "nDTMenu", tblProjectExteriorMenu.nDTMenu.ToString(), "", lUserId, nCreateOrUpdate);

            }
            catch (Exception ex)
            { }

            return Json(tblProjectExteriorMenu);
        }

        // DELETE: api/ProjectExteriorMenus/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectExteriorMenu tblProjectExteriorMenu = await db.tblProjectExteriorMenus.FindAsync(id);
            if (tblProjectExteriorMenu == null)
            {
                return NotFound();
            }

            db.tblProjectExteriorMenus.Remove(tblProjectExteriorMenu);
            await db.SaveChangesAsync();

            return Ok(tblProjectExteriorMenu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectExteriorMenuExists(int id)
        {
            return db.tblProjectExteriorMenus.Count(e => e.aProjectExteriorMenuID == id) > 0;
        }
    }
}