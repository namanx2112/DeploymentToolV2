using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;
using DeploymentTool.Misc;
using DeploymentTool.Model;

namespace DeploymentTool.Controller
{
    public class StoreContactsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        // GET: api/ProjectStoreContacts
        [Authorize]
        [HttpPost]
        public IQueryable<tblStore> Get(Dictionary<string, string> searchFields)
        {

            int nStoreId = searchFields["nStoreId"] != null ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

            return db.tblStores.Where(p => p.aStoreID == nStoreId).AsQueryable(); 
        }

        // GET: api/ProjectStoreContacts/5
        [ResponseType(typeof(tblStore))]
        public async Task<IHttpActionResult> GettblProjectStore(int id)
        {
            tblStore tblStore = await db.tblStores.FindAsync(id);
            if (tblStore == null)
            {
                return NotFound();
            }

            return Ok(tblStore);
        }

        // PUT: api/ProjectStoreContacts/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblStore tblStore)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            db.Entry(tblStore).State = EntityState.Modified;

            db.Entry(tblStore).Property(p => p.nCreatedBy).IsModified = false;
            db.Entry(tblStore).Property(p => p.dtCreatedOn).IsModified = false;
            Utilities.SetHousekeepingFields(false, HttpContext.Current, tblStore);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectStoreExists(tblStore.aStoreID))
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
        public async Task<IHttpActionResult> Create(tblStore tblStore)
        {
            tblStore.aStoreID = 0;

            db.tblStores.Add(tblStore);
            await db.SaveChangesAsync();

            return Json(tblStore);
        }

        // DELETE: api/ProjectStoreContacts/5
        [ResponseType(typeof(tblStore))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblStore tblStore = await db.tblStores.FindAsync(id);
            if (tblStore == null)
            {
                return NotFound();
            }

            db.tblStores.Remove(tblStore);
            await db.SaveChangesAsync();

            return Ok(tblStore);
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
            return db.tblStores.Count(e => e.aStoreID == id) > 0;
        }
    }
}