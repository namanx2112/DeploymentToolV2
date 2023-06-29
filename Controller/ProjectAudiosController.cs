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
    public class ProjectAudiosController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectAudios
        public IQueryable<tblProjectAudio> Get(Dictionary<string, string> searchFields)
        {
            int nProjectID = searchFields["nProjectID"] != null ? Convert.ToInt32(searchFields["nProjectID"]) : 0;

            return db.tblProjectAudios.Where(p => p.nProjectID == nProjectID && p.ProjectActiveStatus == 1).AsQueryable();
           
        }

        // GET: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> GettblProjectAudio(int id)
        {
            tblProjectAudio tblProjectAudio = await db.tblProjectAudios.FindAsync(id);
            if (tblProjectAudio == null)
            {
                return NotFound();
            }

            return Ok(tblProjectAudio);
        }

        // PUT: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectAudio tblProjectAudio)
        {
          

            db.Entry(tblProjectAudio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectAudioExists(tblProjectAudio.aProjectAudioID))
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
        public async Task<IHttpActionResult> Create(tblProjectAudio tblProjectAudio)
        {
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectAudio set projectActiveStatus=0 where nProjectId =@nProjectId", new SqlParameter("@nProjectId", tblProjectAudio.nProjectID));
            tblProjectAudio.ProjectActiveStatus = 1;
            tblProjectAudio.aProjectAudioID = 0;

            db.tblProjectAudios.Add(tblProjectAudio);
            await db.SaveChangesAsync();

            return Json(tblProjectAudio);
        }

        // DELETE: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectAudio tblProjectAudio = await db.tblProjectAudios.FindAsync(id);
            if (tblProjectAudio == null)
            {
                return NotFound();
            }

            db.tblProjectAudios.Remove(tblProjectAudio);
            await db.SaveChangesAsync();

            return Ok(tblProjectAudio);
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
            return db.tblProjectAudios.Count(e => e.aProjectAudioID == id) > 0;
        }
    }
}