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
using Org.BouncyCastle.Asn1.Ocsp;

namespace DeploymentTool.Controller
{
    public class ProjectStakeHoldersController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectStakeHolders
        public IQueryable<tblProjectStakeHolder> Get(Dictionary<string, string> searchFields)
        {
            int nStoreId = searchFields["nStoreId"] != null ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

            return db.tblProjectStakeHolders.Where(p => p.nStoreId == nStoreId).AsQueryable();

        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectStakeHolders/5
        [ResponseType(typeof(tblProjectStakeHolder))]
        public async Task<IHttpActionResult> GettblProjectStakeHolder(int id)
        {
            tblProjectStakeHolder tblProjectStakeHolder = await db.tblProjectStakeHolders.FindAsync(id);
            if (tblProjectStakeHolder == null)
            {
                return NotFound();
            }

            return Ok(tblProjectStakeHolder);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectStakeHolders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> update(tblProjectStakeHolder tblProjectStakeHolder)
        {
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectStakeHolder.nStoreId, tblProjectStakeHolder);
            db.Entry(tblProjectStakeHolder).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectStakeHolderExists(tblProjectStakeHolder.aProjectStakeHolderID))
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
        // POST: api/ProjectStakeHolders
        [ResponseType(typeof(tblProjectStakeHolder))]
        public async Task<IHttpActionResult> Create(tblProjectStakeHolder request)
        {
            request.aProjectStakeHolderID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, request.nStoreId, request);
            db.tblProjectStakeHolders.Add(request);
            await db.SaveChangesAsync();
            return Json(request);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectStakeHolders/5
        [ResponseType(typeof(tblProjectStakeHolder))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectStakeHolder tblProjectStakeHolder = await db.tblProjectStakeHolders.FindAsync(id);
            if (tblProjectStakeHolder == null)
            {
                return NotFound();
            }

            db.tblProjectStakeHolders.Remove(tblProjectStakeHolder);
            await db.SaveChangesAsync();

            return Ok(tblProjectStakeHolder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectStakeHolderExists(int id)
        {
            return db.tblProjectStakeHolders.Count(e => e.aProjectStakeHolderID == id) > 0;
        }
    }
}