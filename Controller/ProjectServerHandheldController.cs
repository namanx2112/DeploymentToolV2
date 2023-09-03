using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class ProjectServerHandheldController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectAudios
        public IQueryable<tblProjectServerHandheld> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectServerHandheld> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;


                if (nProjectID != 0)
                {
                    return db.tblProjectServerHandhelds.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectServerHandheld");
                    items = db.Database.SqlQuery<tblProjectServerHandheld>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
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
            tblProjectServerHandheld tblProjectServerHandheld = await db.tblProjectServerHandhelds.FindAsync(id);
            if (tblProjectServerHandheld == null)
            {
                return NotFound();
            }

            return Ok(tblProjectServerHandheld);
        }

        // PUT: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectServerHandheld tblProjectServerHandheld)
        {

            //tblProjectServerHandheld.ProjectActiveStatus = 1;//SantoshPP\
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.ServerHandheld, tblProjectServerHandheld.nStoreId, tblProjectServerHandheld);
            db.Entry(tblProjectServerHandheld).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectAudioExists(tblProjectServerHandheld.aServerHandheldId))
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
        public async Task<IHttpActionResult> Create(tblProjectServerHandheld tblProjectServerHandheld)
        {
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectServerHandheld set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectServerHandheld.nStoreId));
            //tblProjectServerHandheld.ProjectActiveStatus = 1; SantoshPP
            tblProjectServerHandheld.aServerHandheldId = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.ServerHandheld, tblProjectServerHandheld.nStoreId, tblProjectServerHandheld);
            db.tblProjectServerHandhelds.Add(tblProjectServerHandheld);
            await db.SaveChangesAsync();

            return Json(tblProjectServerHandheld);
        }

        // DELETE: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectServerHandheld tblProjectServerHandheld = await db.tblProjectServerHandhelds.FindAsync(id);
            if (tblProjectServerHandheld == null)
            {
                return NotFound();
            }

            db.tblProjectServerHandhelds.Remove(tblProjectServerHandheld);
            await db.SaveChangesAsync();

            return Ok(tblProjectServerHandheld);
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
            return db.tblProjectServerHandhelds.Count(e => e.aServerHandheldId == id) > 0;
        }
    }
}
