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
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;
using DeploymentTool.Misc;
using System.Linq.Dynamic.Core;

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
            if (searchFields == null)
                return db.tblBrand;
            else
            {
                StringBuilder sBuilder = new StringBuilder();
                foreach (KeyValuePair<string, string> keyVal in searchFields)
                {
                    if (sBuilder.Length > 0)
                        sBuilder.Append(" and ");
                    sBuilder.AppendFormat("x.{0}.ToLower().Contains(\"{1}\".ToLower())", keyVal.Key, keyVal.Value);
                }
                return db.tblBrand.Where("x=>" + sBuilder.ToString());
            }
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
            Utilities.SetHousekeepingFields(false, HttpContext.Current, tblBrand);
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

            Utilities.SetHousekeepingFields(true, HttpContext.Current, tblBrand);
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