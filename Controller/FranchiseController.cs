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
using System.Linq.Dynamic.Core;
using System.Text;

namespace DeploymentTool.Controller
{
    public class FranchiseController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        // GET: api/Franchise
        [Authorize]
        [HttpPost]
        public IQueryable<tblFranchise> Get(Dictionary<string, string> searchFields)
        {
            if (searchFields == null)
                return db.tblFranchises;
            else
            {
                StringBuilder sBuilder = new StringBuilder();
                foreach (KeyValuePair<string, string> keyVal in searchFields)
                {
                    if (sBuilder.Length > 0)
                        sBuilder.Append(" and ");
                    sBuilder.AppendFormat("x.{0}.ToLower().Contains(\"{1}\".ToLower())", keyVal.Key, keyVal.Value);
                }
                return db.tblFranchises.Where("x=>" + sBuilder.ToString());
            }
        }

        // GET: api/Franchise/5
        [ResponseType(typeof(tblFranchise))]
        public async Task<IHttpActionResult> GettblFranchise(int id)
        {
            tblFranchise tblFranchise = await db.tblFranchises.FindAsync(id);
            if (tblFranchise == null)
            {
                return NotFound();
            }

            return Ok(tblFranchise);
        }

        // PUT: api/Franchise/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblFranchise tblFranchise)
        {

            db.Entry(tblFranchise).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblFranchiseExists(tblFranchise.aFranchiseId))
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

        // POST: api/Franchise
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblFranchise tblFranchise)
        {

            db.tblFranchises.Add(tblFranchise);
            await db.SaveChangesAsync();

            return Json(tblFranchise);
        }

        // DELETE: api/Franchise/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblFranchise tblFranchise = await db.tblFranchises.FindAsync(id);
            if (tblFranchise == null)
            {
                return NotFound();
            }

            db.tblFranchises.Remove(tblFranchise);
            await db.SaveChangesAsync();

            return Ok(tblFranchise);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblFranchiseExists(int id)
        {
            return db.tblFranchises.Count(e => e.aFranchiseId == id) > 0;
        }
    }
}