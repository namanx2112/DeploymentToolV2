using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class PurchaseOrderController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpPost]
        public IQueryable<PurchaseOrderTeamplate> Get(Dictionary<string, string> searchFields)
        {
            List<PurchaseOrderTeamplate> tList = new List<PurchaseOrderTeamplate> {
                new PurchaseOrderTeamplate()
                {
                    aPurchaseOrderTemplateID= 1,
                    nBrandId= 1,
                    nCreatedBy= 1,
                    nUpdateBy= 1,
                    nVenderID= 1,
                    tTemplateName = "First PO",
                    purchaseOrderParts = new List<PurchaseOrderParts>()
                    {
                        new PurchaseOrderParts()
                        {
                            aPartID= 1,
                            nVendorId= 1,
                            tPartDesc= "sss",
                            tPartNumber = "11",
                            cPrice = 200,
                            tTableName = "tblProjectAudio",
                            tTechCompField = ""
                        }
                    }
                }
            };
            //IQueryable<VendorParts> items = db.Database.SqlQuery<VendorParts>("exec sproc_getVendorPartsModel").AsQueryable();
            //if (searchFields == null)
            //{
            //    return items;
            //}
            //else
            //{
            //    StringBuilder sBuilder = new StringBuilder();
            //    foreach (KeyValuePair<string, string> keyVal in searchFields)
            //    {
            //        if (sBuilder.Length > 0)
            //            sBuilder.Append(" and ");
            //        sBuilder.AppendFormat("x.{0}.ToLower().Contains(\"{1}\".ToLower())", keyVal.Key, keyVal.Value);
            //    }
            //    return items.Where("x=>" + sBuilder.ToString());
            //}
            return tList.AsQueryable();
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(PurchaseOrderTeamplate poRequest)
        {

            //db.Entry(partsRequest.GetTblParts()).State = EntityState.Modified;
            //// Update into tblVendorPartRel
            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!tblUserExists(partsRequest.aPartID))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tblUser
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(PurchaseOrderTeamplate poRequest)
        {
            //db.tblParts.Add(partsRequest.GetTblParts());
            //await db.SaveChangesAsync();

            // Add into tblVendorPartRel
            poRequest.aPurchaseOrderTemplateID = 10;
            return Json(poRequest);
        }

        // DELETE: api/tblUser/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            PurchaseOrderTeamplate poRequest = new PurchaseOrderTeamplate();
            //tblPart tblPart = await db.tblParts.FindAsync(id);
            //if (tblPart == null)
            //{
            //    return NotFound();
            //}

            //db.tblParts.Remove(tblPart);
            //await db.SaveChangesAsync();

            return Ok(poRequest);
        }


        private bool tblUserExists(int id)
        {
            return db.tblParts.Count(e => e.aPartID == id) > 0;
        }
    }
}
