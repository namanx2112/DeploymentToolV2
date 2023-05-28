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
    public class UserController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        // GET: api/tblUser
        [Authorize]
        [HttpPost]
        public IQueryable<tblUser> Get()
        {
            return db.tblUser;
        }

        // GET: api/tblUser/5
        [ResponseType(typeof(tblUser))]
        public async Task<IHttpActionResult> GettblUser(int id)
        {
            tblUser tblUser = await db.tblUser.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            return Ok(tblUser);
        }

        // PUT: api/tblUser/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Entry(tblUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExists(tblUser.aUserID))
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

        // POST: api/tblUser
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblUser.Add(tblUser);
            await db.SaveChangesAsync();

            return Json(tblUser);
        }

        // DELETE: api/tblUser/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblUser tblUser = await db.tblUser.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            db.tblUser.Remove(tblUser);
            await db.SaveChangesAsync();

            return Ok(tblUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserExists(int id)
        {
            return db.tblUser.Count(e => e.aUserID == id) > 0;
        }
    }
}