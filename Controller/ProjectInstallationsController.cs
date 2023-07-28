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

            IQueryable<tblProjectInstallation> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

                if (nProjectID != 0)
                {
                    return db.tblProjectInstallations.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectInstallation");
                    items = db.Database.SqlQuery<tblProjectInstallation>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    //return items;
                }
            }
            catch (Exception ex) { }
            return items;

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

            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectInstallation set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectInstallation.nStoreId));

            //tblProjectInstallation.ProjectActiveStatus = 1;Santosh

            tblProjectInstallation.aProjectInstallationID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectInstallation.nStoreId, tblProjectInstallation);
            db.tblProjectInstallations.Add(tblProjectInstallation);
            await db.SaveChangesAsync();

            if (tblProjectInstallation.dInstallEnd != null)
                db.Database.ExecuteSqlCommand("update tblProject set dGoLiveDate=@dInstallationDate where aProjectID =@aProjectID", new SqlParameter("@dInstallationDate", tblProjectInstallation.dInstallEnd), new SqlParameter("@aProjectID", tblProjectInstallation.nProjectID)); // Update goLive date as installation end date
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