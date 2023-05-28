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
    public class BrandController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        // GET: api/tblBrand
        [Authorize]
        [HttpPost]
        public IQueryable<tblBrand> Get(Dictionary<string, string> searchFields)
        {
            return db.tblBrand;
        }

        // GET: api/tblBrand/5
        [ResponseType(typeof(tblBrand))]
        public async Task<IHttpActionResult> GettblBrand(int id)
        {
            tblBrand tblBrand = await db.tblBrand.FindAsync(id);
            if (tblBrand == null)
            {
                return NotFound();
            }

            return Ok(tblBrand);
        }

        // PUT: api/tblBrand/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblBrand tblBrand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(tblBrand).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblBrandExists(tblBrand.aBrandId))
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

        // POST: api/tblBrand
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblBrand tblBrand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblBrand.Add(tblBrand);
            await db.SaveChangesAsync();

            return Json(tblBrand);
        }

        // DELETE: api/tblBrand/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblBrand tblBrand = await db.tblBrand.FindAsync(id);
            if (tblBrand == null)
            {
                return NotFound();
            }

            db.tblBrand.Remove(tblBrand);
            await db.SaveChangesAsync();

            return Ok(tblBrand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblBrandExists(int id)
        {
            return db.tblBrand.Count(e => e.aBrandId == id) > 0;
        }
    }
}