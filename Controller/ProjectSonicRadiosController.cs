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
    public class ProjectSonicRadiosController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectSonicRadios
        public IQueryable<tblProjectSonicRadio> Get(Dictionary<string, string> searchFields)
        {
            int nProjectID = searchFields["nProjectID"] != null ? Convert.ToInt32(searchFields["nProjectID"]) : 0;

            return db.tblProjectSonicRadios.Where(p => p.nProjectID == nProjectID && p.ProjectActiveStatus == 1).AsQueryable();

        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectSonicRadios/5
        [ResponseType(typeof(tblProjectSonicRadio))]
        public async Task<IHttpActionResult> GettblProjectSonicRadio(int id)
        {
            tblProjectSonicRadio tblProjectSonicRadio = await db.tblProjectSonicRadios.FindAsync(id);
            if (tblProjectSonicRadio == null)
            {
                return NotFound();
            }

            return Ok(tblProjectSonicRadio);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectSonicRadios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(tblProjectSonicRadio tblProjectSonicRadio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(tblProjectSonicRadio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectSonicRadioExists(tblProjectSonicRadio.aProjectSonicRadioID))
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
        // POST: api/ProjectSonicRadios
        [ResponseType(typeof(tblProjectSonicRadio))]
        public async Task<IHttpActionResult> Create(tblProjectSonicRadio tblProjectSonicRadio)
        {
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectSonicRadio set projectActiveStatus=0 where nProjectId =@nProjectId", new SqlParameter("@nProjectId", tblProjectSonicRadio.nProjectID));
            tblProjectSonicRadio.ProjectActiveStatus = 1;

            tblProjectSonicRadio.aProjectSonicRadioID = 0;

            db.tblProjectSonicRadios.Add(tblProjectSonicRadio);
            await db.SaveChangesAsync();

            return Json(tblProjectSonicRadio);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectSonicRadios/5
        [ResponseType(typeof(tblProjectSonicRadio))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectSonicRadio tblProjectSonicRadio = await db.tblProjectSonicRadios.FindAsync(id);
            if (tblProjectSonicRadio == null)
            {
                return NotFound();
            }

            db.tblProjectSonicRadios.Remove(tblProjectSonicRadio);
            await db.SaveChangesAsync();

            return Ok(tblProjectSonicRadio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectSonicRadioExists(int id)
        {
            return db.tblProjectSonicRadios.Count(e => e.aProjectSonicRadioID == id) > 0;
        }
    }
}