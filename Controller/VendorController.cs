using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;
using System.Linq.Dynamic.Core;

namespace DeploymentTool.Controller
{
    public class VendorController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        // GET: api/tblVendor
        [Authorize]
        [HttpPost]
        public IQueryable<tblVendor> Get(Dictionary<string, string> searchFields)
        {
            if (searchFields == null)
                return db.tblVendor;
            else
            {
                StringBuilder sBuilder = new StringBuilder();
                foreach (KeyValuePair<string, string> keyVal in searchFields)
                {
                    if (sBuilder.Length > 0)
                        sBuilder.Append(" and ");
                    sBuilder.AppendFormat("x.{0}.ToLower().Contains(\"{1}\".ToLower())", keyVal.Key, keyVal.Value);
                }
                return db.tblVendor.Where("x=>" + sBuilder.ToString());
            }
        }

        // GET: api/tblVendor/5
        [ResponseType(typeof(tblVendor))]
        public async Task<IHttpActionResult> GettblVendor(int id)
        {
            tblVendor tblVendor = await db.tblVendor.FindAsync(id);
            if (tblVendor == null)
            {
                return NotFound();
            }

            return Ok(tblVendor);
        }

        // PUT: api/tblVendor/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblVendor tblVendor)
        {

            db.Entry(tblVendor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblVendorExists(tblVendor.aVendorId))
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

        // POST: api/tblVendor
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblVendor tblVendor)
        {

            db.tblVendor.Add(tblVendor);
            await db.SaveChangesAsync();

            return Json(tblVendor);
        }

        // DELETE: api/tblVendor/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblVendor tblVendor = await db.tblVendor.FindAsync(id);
            if (tblVendor == null)
            {
                return NotFound();
            }

            db.tblVendor.Remove(tblVendor);
            await db.SaveChangesAsync();

            return Ok(tblVendor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblVendorExists(int id)
        {
            return db.tblVendor.Count(e => e.aVendorId == id) > 0;
        }
    }
}