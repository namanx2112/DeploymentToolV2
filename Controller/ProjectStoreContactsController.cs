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
    public class ProjectStoreContactsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        // GET: api/ProjectStoreContacts
        [Authorize]
        [HttpPost]
        public IQueryable<tblProjectStore> Get(Dictionary<string, string> searchFields)
        {

            int nProjectID = searchFields["nProjectID"] != null ? Convert.ToInt32(searchFields["nProjectID"]) : 0;

            return db.tblProjectStores.Where(p => p.nProjectID == nProjectID).AsQueryable(); 
        }

        // GET: api/ProjectStoreContacts/5
        [ResponseType(typeof(tblProjectStore))]
        public async Task<IHttpActionResult> GettblProjectStore(int id)
        {
            tblProjectStore tblProjectStore = await db.tblProjectStores.FindAsync(id);
            if (tblProjectStore == null)
            {
                return NotFound();
            }

            return Ok(tblProjectStore);
        }

        // PUT: api/ProjectStoreContacts/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectStore tblProjectStore)
        {

            db.Entry(tblProjectStore).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectStoreExists(tblProjectStore.aProjectStoreID))
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

        // POST: api/ProjectStoreContacts
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProjectStore tblProjectStore)
        {
            tblProjectStore.aProjectStoreID = 0;

            db.tblProjectStores.Add(tblProjectStore);
            await db.SaveChangesAsync();

            return Json(tblProjectStore);
        }

        // DELETE: api/ProjectStoreContacts/5
        [ResponseType(typeof(tblProjectStore))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectStore tblProjectStore = await db.tblProjectStores.FindAsync(id);
            if (tblProjectStore == null)
            {
                return NotFound();
            }

            db.tblProjectStores.Remove(tblProjectStore);
            await db.SaveChangesAsync();

            return Ok(tblProjectStore);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectStoreExists(int id)
        {
            return db.tblProjectStores.Count(e => e.aProjectStoreID == id) > 0;
        }
    }
}