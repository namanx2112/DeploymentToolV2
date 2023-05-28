﻿using System;
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
    public class VendorController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        // GET: api/tblVendor
        [Authorize]
        [HttpPost]
        public IQueryable<tblVendor> Get(Dictionary<string, string> searchFields)
        {
            return db.tblVendor;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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