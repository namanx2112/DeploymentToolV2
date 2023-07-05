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
using DeploymentTool.Model;
using System.Data.SqlClient;
using System.Net.Http.Formatting;

namespace DeploymentTool.Controller
{
    public class UserController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        // GET: api/tblUser
        [Authorize]
        [HttpPost]
        public IQueryable<UserModel> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<UserModel> items = db.Database.SqlQuery<UserModel>("exec sproc_getUserModel").AsQueryable();
            if (searchFields == null)
            {
                return items;
            }
            else
            {
                StringBuilder sBuilder = new StringBuilder();
                foreach (KeyValuePair<string, string> keyVal in searchFields)
                {
                    if (sBuilder.Length > 0)
                        sBuilder.Append(" and ");
                    sBuilder.AppendFormat("x.{0}.ToLower().Contains(\"{1}\".ToLower())", keyVal.Key, keyVal.Value);
                }
                return items.Where("x=>" + sBuilder.ToString());
            }
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
        public async Task<IHttpActionResult> Update(UserModel userRequest)
        {

            db.Entry(userRequest.GetTblUser()).State = EntityState.Modified;
            // Update into tblUserVendorRelation
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExists(userRequest.aUserID))
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
        public async Task<IHttpActionResult> Create(UserModel userRequest)
        {
            var tmpUser = userRequest.GetTblUser();
            db.tblUser.Add(tmpUser);
            await db.SaveChangesAsync();

            userRequest.aUserID = tmpUser.aUserID;
            // Add into tblUserVendorRelation

            return Json(userRequest);
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