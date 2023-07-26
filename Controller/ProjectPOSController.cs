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

namespace DeploymentTool.Controller
{
    public class ProjectPOSController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectPOS
        public IQueryable<tblProjectPOS> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectPOS> items = null;
            try
            {


                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

                if (nProjectID != 0)
                {
                    return db.tblProjectPOS.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectPOS");
                    items = db.Database.SqlQuery<tblProjectPOS>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    
                }
            }
            catch(Exception ex) { }
            return items;

        }

        // GET: api/ProjectPOS/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> GetProjectPOS(int id)
        {
            tblProjectPOS tblProjectPOS = await db.tblProjectPOS.FindAsync(id);
            if (tblProjectPOS == null)
            {
                return NotFound();
            }

            return Ok(tblProjectPOS);
        }

        // PUT: api/ProjectPOS/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update( tblProjectPOS tblProjectPOS)
        {
            //tblProjectPOS.ProjectActiveStatus = 1;Santosh
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.POSInstallation, tblProjectPOS.nStoreId, tblProjectPOS);
            db.Entry(tblProjectPOS).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectPOSExists(tblProjectPOS.aProjectPOSID))
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

        // POST: api/ProjectPOS
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProjectPOS tblProjectPOS)
        {
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectPOS set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectPOS.nStoreId));

            //tblProjectPOS.ProjectActiveStatus = 1; Santosh
            tblProjectPOS.aProjectPOSID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.POSInstallation, tblProjectPOS.nStoreId, tblProjectPOS);
            db.tblProjectPOS.Add(tblProjectPOS);
            await db.SaveChangesAsync();

            return Json(tblProjectPOS);
        }

        // DELETE: api/ProjectPOS/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectPOS tblProjectPOS = await db.tblProjectPOS.FindAsync(id);
            if (tblProjectPOS == null)
            {
                return NotFound();
            }

            db.tblProjectPOS.Remove(tblProjectPOS);
            await db.SaveChangesAsync();

            return Ok(tblProjectPOS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectPOSExists(int id)
        {
            return db.tblProjectPOS.Count(e => e.aProjectPOSID == id) > 0;
        }
    }
}