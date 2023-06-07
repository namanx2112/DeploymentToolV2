using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            return db.tblProjectPOS;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != tblProjectPOS.aProjectPOSID)
            //{
            //    return BadRequest();
            //}

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblProjectPOS.Add(tblProjectPOS);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblProjectPOS.aProjectPOSID }, tblProjectPOS);
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