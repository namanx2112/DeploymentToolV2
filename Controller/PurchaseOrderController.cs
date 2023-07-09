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
using System.Net.Http.Formatting;
using System.Web.Helpers;
using Org.BouncyCastle.Utilities.Net;

namespace DeploymentTool.Controller
{
    public class PurchaseOrderController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpGet]
        [ActionName("GetAllTemplate")]
        public HttpResponseMessage GetAllTemplate(int nBrandId)
        {
            List<PurchaseOrderTemplateTemp> tList = new List<PurchaseOrderTemplateTemp> {
                new PurchaseOrderTemplateTemp()
                {
                    aPurchaseOrderTemplateID= 1,
                    nBrandId= 1,
                    nCreatedBy= 1,
                    nUpdateBy= 1,
                    tTemplateName = "First PO"
                },
                new PurchaseOrderTemplateTemp()
                {
                    aPurchaseOrderTemplateID= 2,
                    nBrandId= 1,
                    nCreatedBy= 1,
                    nUpdateBy= 1,
                    tTemplateName = "Second PO"
                },
                new PurchaseOrderTemplateTemp()
                {
                    aPurchaseOrderTemplateID= 3,
                    nBrandId= 1,
                    nCreatedBy= 1,
                    nUpdateBy= 1,
                    tTemplateName = "Third PO"
                }
            };
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<PurchaseOrderTemplateTemp>>(tList, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetTemplate(int nTemplateId)
        {
            PurchaseOrderTeamplate tItem = new PurchaseOrderTeamplate()
            {
                aPurchaseOrderTemplateID = nTemplateId,
                nBrandId = 1,
                nCreatedBy = 1,
                tCompName = "Installation",
                nUpdateBy = 1,
                nVendorId = 2,
                tTemplateName = "First PO",
                purchaseOrderParts = new List<PurchaseOrderParts>()
                    {
                        new PurchaseOrderParts()
                        {
                            aPurchaseOrderTemplatePartsID = 1,
                            nPartID= 1,
                            tPartDesc= "Yellow Paint Marker",
                            tPartNumber = "SONIC_MARKER_YEL",
                            cPrice = 88.56M,
                            tTableName = "tblProjectConfig",
                            tTechCompField = "nStallCount"
                        },new PurchaseOrderParts()
                        {
                            aPurchaseOrderTemplatePartsID = 2,
                            nPartID= 2,
                            tPartDesc= "Black Paint Marker",
                            tPartNumber = "SONIC_MARKER_BLK",
                            cPrice = 17.95M,
                            tTableName = "tblProjectExteriorMenus",
                            tTechCompField = "nStalls"
                        },new PurchaseOrderParts()
                        {
                            aPurchaseOrderTemplatePartsID = 3,
                            nPartID= 3,
                            tPartDesc= "New Parts Marker",
                            tPartNumber = "776",
                            cPrice = 1500M,
                            tTableName = "tblProjectExteriorMenus",
                            tTechCompField = "nPatio"
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
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<PurchaseOrderTeamplate>(tItem, new JsonMediaTypeFormatter())
            };
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

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetMergedPO(int nTemplateId, int nProjectId)
        {
            PurchaseOrderPreviewTeamplate poRequest = new PurchaseOrderPreviewTeamplate()
            {
                nProjectId = nProjectId,
                aPurchaseOrderPreviewTeamplateID = 10,
                nVendorId = 1,
                tStore = "111",
                tStoreNumber = "10101",
                tNotes = "Heloo",
                tName = "Name",
                tPhone = "33982823498423",
                tEmail = "heell@ggmail.com",
                tAddress = "1st streat, scond block",
                tCity = "Atlaanta",
                tStoreState = "NewYork",
                tStoreZip = "45449",
                tBillToCompany = "Test",
                tBillToEmail = "bill@gmal.com",
                tBillToAddress = "second streat, 1st main",
                tBillToCity = "Atlantta",
                tBillToState = "Newjurcy",
                cTotal = 1000.55M,
                tPurchaseOrderNumber = "888",
                dDeliver = DateTime.Now,
                nOutgoingEmailID = 1,
                purchaseOrderParts = new List<PurchaseOrderParts>()
                {
                     new PurchaseOrderParts()
                        {
                            aPurchaseOrderTemplatePartsID = 1,
                            nPartID= 1,
                            tPartDesc= "Yellow Paint Marker",
                            tPartNumber = "SONIC_MARKER_YEL",
                            cPrice = 88.56M,
                            tTableName = "tblProjectConfig",
                            tTechCompField = "nStallCount",
                            cTotal = 100,
                            nQuantity = 10
                        },new PurchaseOrderParts()
                        {
                            aPurchaseOrderTemplatePartsID = 2,
                            nPartID= 2,
                            tPartDesc= "Black Paint Marker",
                            tPartNumber = "SONIC_MARKER_BLK",
                            cPrice = 17.95M,
                            tTableName = "tblProjectExteriorMenus",
                            tTechCompField = "nStalls",
                            cTotal = 210,
                            nQuantity = 50
                        },new PurchaseOrderParts()
                        {
                            aPurchaseOrderTemplatePartsID = 3,
                            nPartID= 3,
                            tPartDesc= "New Parts Marker",
                            tPartNumber = "776",
                            cPrice = 1500.5M,
                            tTableName = "tblProjectExteriorMenus",
                            tTechCompField = "nPatio",
                            cTotal = 500.50M,
                            nQuantity = 5
                        }
                }
            };
            //tblPart tblPart = await db.tblParts.FindAsync(id);
            //if (tblPart == null)
            //{
            //    return NotFound();
            //}

            //db.tblParts.Remove(tblPart);
            //await db.SaveChangesAsync();

            return Ok(poRequest);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult SenMergedPO(PurchaseOrderPreviewTeamplate request)
        {
            PurchaseOrderMailMessage message = new PurchaseOrderMailMessage()
            {
                nProjectId = request.nProjectId,
                tTo = "abcd@gmail.com",
                tCC = "ccabcd@gmail.com",
                tContent = "<div style='background-color:gray'>All Febcon attachment with content for this PO</div>" +
                "<div><span>PO#:</span>" + request.tPurchaseOrderNumber + "<br/>" +
                "<span>Revision/Filename:</span>PurchaseOrde1212.pdf<br/>" +
                "<span>Type:</span>HME<br/>" +
                "<span>Store:</span>" + request.tStoreNumber + "<br/>" +
                "<span>Delivery:</span>" + request.dDeliver.ToShortDateString() + "<br/>" +
                "<span>Project Manager:</span>Santosh PP<br/>" +
                "</div>",
                tFileName = "PurchaseOrde1212.pdf",
                tSubject = "Hello HME"
            };

            return Ok(message);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult SendPO(PurchaseOrderMailMessage request)
        {
            // Send PO
            return Ok(1);
        }


        private bool tblUserExists(int id)
        {
            return db.tblParts.Count(e => e.aPartID == id) > 0;
        }
    }
}
