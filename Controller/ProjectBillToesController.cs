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
    public class ProjectBillToesController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectBillToes
        public IQueryable<tblProjectBillTo> Get(Dictionary<string, string> searchFields)
        {
            return db.tblProjectBillToes;
        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectBillToes/5
        [ResponseType(typeof(tblProjectBillTo))]
        public async Task<IHttpActionResult> GettblProjectBillTo(int id)
        {
            tblProjectBillTo tblProjectBillTo = await db.tblProjectBillToes.FindAsync(id);
            if (tblProjectBillTo == null)
            {
                return NotFound();
            }

            return Ok(tblProjectBillTo);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectBillToes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update( tblProjectBillTo tblProjectBillTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          

            db.Entry(tblProjectBillTo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectBillToExists(tblProjectBillTo.aProjectBillToID))
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
        // POST: api/ProjectBillToes
        [ResponseType(typeof(tblProjectBillTo))]
        public async Task<IHttpActionResult> Create(tblProjectBillTo tblProjectBillTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblProjectBillToes.Add(tblProjectBillTo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblProjectBillTo.aProjectBillToID }, tblProjectBillTo);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectBillToes/5
        [ResponseType(typeof(tblProjectBillTo))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectBillTo tblProjectBillTo = await db.tblProjectBillToes.FindAsync(id);
            if (tblProjectBillTo == null)
            {
                return NotFound();
            }

            db.tblProjectBillToes.Remove(tblProjectBillTo);
            await db.SaveChangesAsync();

            return Ok(tblProjectBillTo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectBillToExists(int id)
        {
            return db.tblProjectBillToes.Count(e => e.aProjectBillToID == id) > 0;
        }
    }
}