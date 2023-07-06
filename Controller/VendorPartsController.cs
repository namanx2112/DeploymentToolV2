using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.SqlClient;

namespace DeploymentTool.Controller
{
    public class VendorPartsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpPost]
        public IQueryable<VendorParts> Get(Dictionary<string, string> searchFields)
        {
            int nVendorId = (searchFields != null && searchFields.ContainsKey("nVendorId")) ? Convert.ToInt32(searchFields["nVendorId"]) : 0;
            SqlParameter tModuleNameParam = new SqlParameter("@nVendorId", nVendorId);
            IQueryable<VendorParts> items = db.Database.SqlQuery<VendorParts>("exec sproc_getVendorPartsModel @nVendorId", tModuleNameParam).AsQueryable();
            return items;
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(VendorParts partsRequest)
        {

            db.Entry(partsRequest.GetTblParts()).State = EntityState.Modified;
            // Update into tblVendorPartRel
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExists(partsRequest.aPartID))
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
        public async Task<IHttpActionResult> Create(VendorParts partsRequest)
        {
            db.tblParts.Add(partsRequest.GetTblParts());
            await db.SaveChangesAsync();

            // Add into tblVendorPartRel

            return Json(partsRequest);
        }

        // DELETE: api/tblUser/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblPart tblPart = await db.tblParts.FindAsync(id);
            if (tblPart == null)
            {
                return NotFound();
            }

            db.tblParts.Remove(tblPart);
            await db.SaveChangesAsync();

            return Ok(tblPart);
        }


        private bool tblUserExists(int id)
        {
            return db.tblParts.Count(e => e.aPartID == id) > 0;
        }
    }
}
