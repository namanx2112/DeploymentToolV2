﻿using System;
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
using DeploymentTool.Misc;
using System.Web;

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
                return db.tblVendor.Where(x => x.bDeleted != true);
            else
            {
                StringBuilder sBuilder = new StringBuilder();
                sBuilder.AppendFormat("x.bDeleted != true");
                foreach (KeyValuePair<string, string> keyVal in searchFields)
                {
                    sBuilder.Append(" and ");
                    if (keyVal.Key.StartsWith("t"))
                        sBuilder.AppendFormat("x.{0}.ToLower().Contains(\"{1}\".ToLower())", keyVal.Key, keyVal.Value);
                    else
                        sBuilder.AppendFormat("x.{0}={1}", keyVal.Key, keyVal.Value);
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
            if (tblVendor.nBrand == null)
                tblVendor.nBrand = 1;

            db.Entry(tblVendor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                TraceUtility.ForceWriteException("Vendor.Update", HttpContext.Current, ex);
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
            if (tblVendor.nBrand == null)
                tblVendor.nBrand = 1;
            db.tblVendor.Add(tblVendor);
            await db.SaveChangesAsync();

            return Json(tblVendor);
        }

        // DELETE: api/tblVendor/5
        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblVendor tblVendor = await db.tblVendor.FindAsync(id);
            if (tblVendor == null)
            {
                return NotFound();
            }

            tblVendor.bDeleted = true;
            db.Entry(tblVendor).State = EntityState.Modified;
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