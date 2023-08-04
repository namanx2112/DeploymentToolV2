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
using System.Net.Http.Headers;
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
            List<Dropdown> items = db.Database.SqlQuery<Dropdown>("exec sproc_GetDropdown @tModuleName", tModuleNameParam).OrderBy(x => x.nOrder).ToList();
            if (items == null || items.Count == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<Dropdown>>(items, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpGet]
        [ActionName("GetModules")]
        public HttpResponseMessage GetModules(int nBrandId)
        {
            SqlParameter tModuleNameParam = new SqlParameter("@nBrandId", nBrandId);
            List<DropdownModule> items = db.Database.SqlQuery<DropdownModule>("exec sproc_getDropdownModules @nBrandId", tModuleNameParam).ToList();
            if (items == null || items.Count == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<DropdownModule>>(items, new JsonMediaTypeFormatter())
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

        [Authorize]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Dropdown[] request)
        {

            foreach (var item in request)
            {
                tblDropdown ddObj = new tblDropdown()
                {
                    aDropdownId = item.aDropdownId,
                    nOrder = item.nOrder
                };
                Utilities.SetHousekeepingFields(false, HttpContext.Current, ddObj);
                db.tblDropdowns.Attach(ddObj);
                db.Entry(ddObj).Property(x => x.nOrder).IsModified = true;
                db.Entry(ddObj).Property(x => x.dtUpdatedOn).IsModified = true;
                db.Entry(ddObj).Property(x => x.nUpdateBy).IsModified = true;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT: api/Dropdown/5
        [Authorize]
        [HttpPost]
        public HttpResponseMessage Update(Dropdown request)
        {
            var alredyExist = db.Database.SqlQuery<int>("select top 1 1 from tblDropDownMain with(nolock) where tModuleName = '" + request.tModuleName + "' and nDropdownID in(select aDropdownId from tblDropDowns with(nolock) where UPPER(tDropDownText) = '" + request.tDropdownText.ToUpper() + "' and aDropdownId<>" + request.aDropdownId + ")").FirstOrDefault();
            if (alredyExist == null || alredyExist == 0)
            {
                tblDropdown ddObj = new tblDropdown()
                {
                    tDropdownText = request.tDropdownText,
                    bDeleted = request.bDeleted,
                    aDropdownId = request.aDropdownId,
                    nFunction = request.nFunction
                };

                Utilities.SetHousekeepingFields(false, HttpContext.Current, ddObj);
                db.tblDropdowns.Add(ddObj);

                db.Entry(ddObj).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblDropdownExists(request.aDropdownId))
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }
                    else
                    {
                        throw;
                    }
                }
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<tblDropdown>(ddObj, new JsonMediaTypeFormatter())
                };
            }
            else
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>($"The item {request.tDropdownText} already exist!", new JsonMediaTypeFormatter())
                };
        }

        // POST: api/Dropdown
        [Authorize]
        [HttpPost]
        public HttpResponseMessage Create(Dropdown request)
        {
            var alredyExist = db.Database.SqlQuery<int>("select top 1 1 from tblDropDownMain with(nolock) where tModuleName = '" + request.tModuleName + "' and nDropdownID in(select aDropdownId from tblDropDowns with(nolock) where UPPER(tDropDownText)='" + request.tDropdownText.ToUpper() + "')").FirstOrDefault();
            if (alredyExist == null || alredyExist == 0)
            {
                tblDropdown ddObj = new tblDropdown()
                {
                    nFunction  = request.nFunction,
                    tDropdownText = request.tDropdownText,
                };

                Utilities.SetHousekeepingFields(true, HttpContext.Current, ddObj);
                db.tblDropdowns.Add(ddObj);
                db.SaveChanges();

                tblDropdownMain mainObj = new tblDropdownMain()
                {
                    nBrandId = request.nBrandId,
                    nDropdownId = ddObj.aDropdownId,
                    tModuleName = request.tModuleName,
                };

                Utilities.SetHousekeepingFields(true, HttpContext.Current, mainObj);
                db.tblDropdownMains.Add(mainObj);
                db.SaveChanges();

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<tblDropdown>(ddObj, new JsonMediaTypeFormatter())
                };
            }
            else
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>($"The item {request.tDropdownText} already exist!", new JsonMediaTypeFormatter())
                };
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

            db.tblDropdowns.Attach(ddObj);
            db.Entry(ddObj).Property(x => x.bDeleted).IsModified = true;

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