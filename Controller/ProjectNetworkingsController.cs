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

namespace DeploymentTool.Model
{
    public class ProjectNetworkingsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectNetworkings
        public IQueryable<tblProjectNetworking> Get(Dictionary<string, string> searchFields)
        {
            return db.tblProjectNetworkings;
        }

        // GET: api/ProjectNetworkings/5
        [ResponseType(typeof(tblProjectNetworking))]
        public async Task<IHttpActionResult> GetProjectNetworking(int id)
        {
            tblProjectNetworking tblProjectNetworking = await db.tblProjectNetworkings.FindAsync(id);
            if (tblProjectNetworking == null)
            {
                return NotFound();
            }

            return Ok(tblProjectNetworking);
        }

        // PUT: api/ProjectNetworkings/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> update(tblProjectNetworking tblProjectNetworking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            db.Entry(tblProjectNetworking).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectNetworkingExists(tblProjectNetworking.aProjectNetworkingID))
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

        // POST: api/ProjectNetworkings
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProjectNetworking tblProjectNetworking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblProjectNetworkings.Add(tblProjectNetworking);
            await db.SaveChangesAsync();

            return Json(tblProjectNetworking);
        }

        // DELETE: api/ProjectNetworkings/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectNetworking tblProjectNetworking = await db.tblProjectNetworkings.FindAsync(id);
            if (tblProjectNetworking == null)
            {
                return NotFound();
            }

            db.tblProjectNetworkings.Remove(tblProjectNetworking);
            await db.SaveChangesAsync();

            return Ok(tblProjectNetworking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectNetworkingExists(int id)
        {
            return db.tblProjectNetworkings.Count(e => e.aProjectNetworkingID == id) > 0;
        }
    }
}