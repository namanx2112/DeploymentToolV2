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
            int nProjectID = searchFields["nProjectID"] != null ? Convert.ToInt32(searchFields["nProjectID"]) : 0;

            return db.tblProjectPOS.Where(p => p.nProjectID == nProjectID &&  p.ProjectActiveStatus == 1 ).AsQueryable();

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
            tblProjectPOS.ProjectActiveStatus = 1;
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
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectPOS set projectActiveStatus=0 where nProjectId =@nProjectId", new SqlParameter("@nProjectId", tblProjectPOS.nProjectID));
            tblProjectPOS.ProjectActiveStatus = 1;
            tblProjectPOS.aProjectPOSID = 0;
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