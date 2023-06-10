using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.ModelBinding;
using DeploymentTool;
using DeploymentTool.Misc;
using DeploymentTool.Model;

namespace DeploymentTool.Controller
{
    public class DropdownController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        // GET: api/Dropdown
        [Authorize]
        [HttpGet]
        [ActionName("Get")]
        public HttpResponseMessage Get(string tModuleName)
        {
            if (tModuleName == null)
                tModuleName = "";
            SqlParameter tModuleNameParam = new SqlParameter("@tModuleName", tModuleName);
            List<Dropdown> items= db.Database.SqlQuery<Dropdown>("exec sproc_GetDropdown @tModuleName", tModuleNameParam).ToList();            
            if (items == null || items.Count == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<Dropdown>>(items, new JsonMediaTypeFormatter())
            };
        }

        // GET: api/Dropdown/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> GettblDropdown(int id)
        {
            tblDropdown tblDropdown = await db.tblDropdowns.FindAsync(id);
            tblDropdownMain tblMain = await db.tblDropdownMains.FindAsync(id);
            if (tblDropdown == null)
            {
                return NotFound();
            }

            return Ok(tblDropdown);
        }

        // PUT: api/Dropdown/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(Dropdown request)
        {
            tblDropdown ddObj = new tblDropdown()
            {
                tDropdownText = request.tDropdownText,
                bDeleted = request.bDeleted,
                aDropdownId = request.aDropdownId
            };

            Utilities.SetHousekeepingFields(false, HttpContext.Current, ddObj);
            db.tblDropdowns.Add(ddObj);

            db.Entry(ddObj).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblDropdownExists(request.aDropdownId))
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

        // POST: api/Dropdown
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(Dropdown request)
        {

            tblDropdown ddObj = new tblDropdown()
            {
                tDropdownText = request.tDropdownText,
            };

            Utilities.SetHousekeepingFields(true, HttpContext.Current, ddObj);
            db.tblDropdowns.Add(ddObj);
            await db.SaveChangesAsync();

            tblDropdownMain mainObj = new tblDropdownMain()
            {
                nBrandId = request.nBrandId,
                nDropdownId = ddObj.aDropdownId,
                tModuleName = request.tModuleName,
            };

            Utilities.SetHousekeepingFields(true, HttpContext.Current, mainObj);
            db.tblDropdownMains.Add(mainObj);
            await db.SaveChangesAsync();

            return Json(ddObj.aDropdownId);
        }

        // DELETE: api/Dropdown/5
        [Authorize]
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblDropdown ddObj = new tblDropdown()
            {
                bDeleted = true,
                aDropdownId = id
            };

            db.tblDropdowns.Attach(ddObj).bDeleted = true;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblDropdownExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblDropdownExists(int id)
        {
            return db.tblDropdowns.Count(e => e.aDropdownId == id) > 0;
        }
    }
}