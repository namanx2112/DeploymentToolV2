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

namespace DeploymentTool.Model
{
    public class ProjectExteriorMenusController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectExteriorMenus
        public IQueryable<tblProjectExteriorMenu> Get(Dictionary<string, string> searchFields)
        {
            return db.tblProjectExteriorMenus;
        }

        // GET: api/ProjectExteriorMenus/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> GettblProjectExteriorMenu(int id)
        {
            tblProjectExteriorMenu tblProjectExteriorMenu = await db.tblProjectExteriorMenus.FindAsync(id);
            if (tblProjectExteriorMenu == null)
            {
                return NotFound();
            }

            return Ok(tblProjectExteriorMenu);
        }

        // PUT: api/ProjectExteriorMenus/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectExteriorMenu tblProjectExteriorMenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          

            db.Entry(tblProjectExteriorMenu).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectExteriorMenuExists(tblProjectExteriorMenu.aProjectExteriorMenuID))
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

        // POST: api/ProjectExteriorMenus
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProjectExteriorMenu tblProjectExteriorMenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblProjectExteriorMenus.Add(tblProjectExteriorMenu);
            await db.SaveChangesAsync();

            return Json(tblProjectExteriorMenu);
        }

        // DELETE: api/ProjectExteriorMenus/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectExteriorMenu tblProjectExteriorMenu = await db.tblProjectExteriorMenus.FindAsync(id);
            if (tblProjectExteriorMenu == null)
            {
                return NotFound();
            }

            db.tblProjectExteriorMenus.Remove(tblProjectExteriorMenu);
            await db.SaveChangesAsync();

            return Ok(tblProjectExteriorMenu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectExteriorMenuExists(int id)
        {
            return db.tblProjectExteriorMenus.Count(e => e.aProjectExteriorMenuID == id) > 0;
        }
    }
}