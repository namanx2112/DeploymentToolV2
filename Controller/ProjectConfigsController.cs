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
    public class ProjectConfigsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectConfigs
        public IQueryable<tblProjectConfig> Get(Dictionary<string, string> searchFields)
        {
            return db.tblProjectConfigs;
        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectConfigs/5
        [ResponseType(typeof(tblProjectConfig))]
        public async Task<IHttpActionResult> GettblProjectConfig(int id)
        {
            tblProjectConfig tblProjectConfig = await db.tblProjectConfigs.FindAsync(id);
            if (tblProjectConfig == null)
            {
                return NotFound();
            }

            return Ok(tblProjectConfig);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectConfigs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(tblProjectConfig tblProjectConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            db.Entry(tblProjectConfig).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectConfigExists(tblProjectConfig.aProjectConfigID))
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
        // POST: api/ProjectConfigs
        [ResponseType(typeof(tblProjectConfig))]
        public async Task<IHttpActionResult> Cretae(tblProjectConfig tblProjectConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblProjectConfigs.Add(tblProjectConfig);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblProjectConfig.aProjectConfigID }, tblProjectConfig);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectConfigs/5
        [ResponseType(typeof(tblProjectConfig))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectConfig tblProjectConfig = await db.tblProjectConfigs.FindAsync(id);
            if (tblProjectConfig == null)
            {
                return NotFound();
            }

            db.tblProjectConfigs.Remove(tblProjectConfig);
            await db.SaveChangesAsync();

            return Ok(tblProjectConfig);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectConfigExists(int id)
        {
            return db.tblProjectConfigs.Count(e => e.aProjectConfigID == id) > 0;
        }
    }
}