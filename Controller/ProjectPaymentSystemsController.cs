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
    public class ProjectPaymentSystemsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectPaymentSystems
        public IQueryable<tblProjectPaymentSystem> Get(Dictionary<string, string> searchFields)
        {
            return db.tblProjectPaymentSystems;
        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectPaymentSystems/5
        [ResponseType(typeof(tblProjectPaymentSystem))]
        public async Task<IHttpActionResult> GettblProjectPaymentSystem(int id)
        {
            tblProjectPaymentSystem tblProjectPaymentSystem = await db.tblProjectPaymentSystems.FindAsync(id);
            if (tblProjectPaymentSystem == null)
            {
                return NotFound();
            }

            return Ok(tblProjectPaymentSystem);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectPaymentSystems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(tblProjectPaymentSystem tblProjectPaymentSystem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          

            db.Entry(tblProjectPaymentSystem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectPaymentSystemExists(tblProjectPaymentSystem.aProjectPaymentSystemID))
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
        // POST: api/ProjectPaymentSystems
        [ResponseType(typeof(tblProjectPaymentSystem))]
        public async Task<IHttpActionResult> Create(tblProjectPaymentSystem tblProjectPaymentSystem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblProjectPaymentSystems.Add(tblProjectPaymentSystem);
            await db.SaveChangesAsync();

            return Json(tblProjectPaymentSystem);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectPaymentSystems/5
        [ResponseType(typeof(tblProjectPaymentSystem))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectPaymentSystem tblProjectPaymentSystem = await db.tblProjectPaymentSystems.FindAsync(id);
            if (tblProjectPaymentSystem == null)
            {
                return NotFound();
            }

            db.tblProjectPaymentSystems.Remove(tblProjectPaymentSystem);
            await db.SaveChangesAsync();

            return Ok(tblProjectPaymentSystem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectPaymentSystemExists(int id)
        {
            return db.tblProjectPaymentSystems.Count(e => e.aProjectPaymentSystemID == id) > 0;
        }
    }
}