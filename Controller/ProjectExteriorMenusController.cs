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
            int nStoreId = searchFields["nStoreId"] != null ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

            return db.tblProjectExteriorMenus.Where(p => p.nStoreId == nStoreId).AsQueryable();

          
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
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.MenuInstallation, tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu);
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
                //var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectExteriorMenus set projectActiveStatus=0 where nProjectId =@nProjectId", new SqlParameter("@nProjectId", tblProjectExteriorMenu.nProjectID));
                //tblProjectExteriorMenu.ProjectActiveStatus = 1;Santosh

                tblProjectExteriorMenu.aProjectExteriorMenuID = 0;
                Misc.Utilities.SetActiveProjectId(Misc.ProjectType.MenuInstallation, tblProjectExteriorMenu.nStoreId, tblProjectExteriorMenu);
                db.tblProjectExteriorMenus.Add(tblProjectExteriorMenu);
                await db.SaveChangesAsync();
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