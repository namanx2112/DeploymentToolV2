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
    public class ProjectInstallationsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectInstallations
        public IQueryable<tblProjectInstallation> Get(Dictionary<string, string> searchFields)
        {
            int nStoreId = searchFields["nStoreId"] != null ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

            return db.tblProjectInstallations.Where(p => p.nStoreId == nStoreId).AsQueryable();
            
        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectInstallations/5
        [ResponseType(typeof(tblProjectInstallation))]
        public async Task<IHttpActionResult> GettblProjectInstallation(int id)
        {
            tblProjectInstallation tblProjectInstallation = await db.tblProjectInstallations.FindAsync(id);
            if (tblProjectInstallation == null)
            {
                return NotFound();
            }

            return Ok(tblProjectInstallation);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectInstallations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(tblProjectInstallation tblProjectInstallation)
        {
            //tblProjectInstallation.ProjectActiveStatus = 1;Santosh
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectInstallation.nStoreId, tblProjectInstallation);
            db.Entry(tblProjectInstallation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectInstallationExists(tblProjectInstallation.aProjectInstallationID))
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
        // POST: api/ProjectInstallations
        [ResponseType(typeof(tblProjectInstallation))]
        public async Task<IHttpActionResult> Create(tblProjectInstallation tblProjectInstallation)
        {
            //var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectInstallation set projectActiveStatus=0 where nProjectId =@nProjectId", new SqlParameter("@nProjectId", tblProjectInstallation.nProjectID));
            //tblProjectInstallation.ProjectActiveStatus = 1;Santosh

            tblProjectInstallation.aProjectInstallationID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectInstallation.nStoreId, tblProjectInstallation);
            db.tblProjectInstallations.Add(tblProjectInstallation);
            await db.SaveChangesAsync();

            return Json(tblProjectInstallation);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectInstallations/5
        [ResponseType(typeof(tblProjectInstallation))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectInstallation tblProjectInstallation = await db.tblProjectInstallations.FindAsync(id);
            if (tblProjectInstallation == null)
            {
                return NotFound();
            }

            db.tblProjectInstallations.Remove(tblProjectInstallation);
            await db.SaveChangesAsync();

            return Ok(tblProjectInstallation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectInstallationExists(int id)
        {
            return db.tblProjectInstallations.Count(e => e.aProjectInstallationID == id) > 0;
        }
    }
}