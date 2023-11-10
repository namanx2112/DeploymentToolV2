using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class ProjectImageMemoryController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectAudios
        public IQueryable<tblProjectImageOrMemory> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectImageOrMemory> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;


                if (nProjectID != 0)
                {
                    return db.tblProjectImageOrMemories.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectImageOrMemory");
                    items = db.Database.SqlQuery<tblProjectImageOrMemory>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    //return items;
                }
            }
            catch (Exception ex) { }
            return items;

        }

        // GET: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> GettblProjectAudio(int id)
        {
            tblProjectImageOrMemory tblProjectImageOrMemory = await db.tblProjectImageOrMemories.FindAsync(id);
            if (tblProjectImageOrMemory == null)
            {
                return NotFound();
            }

            return Ok(tblProjectImageOrMemory);
        }

        // PUT: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectImageOrMemory tblProjectImageOrMemory)
        {

            ////tblProjectImageOrMemory.ProjectActiveStatus = 1;//SantoshPP\
            //Misc.Utilities.SetActiveProjectId(Misc.ProjectType.ServerHandheld, tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory);

            //var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            //int nCreateOrUpdate = 2;
            //////----Vendor Audit field-----------
            //// if (tblProjectImageOrMemory.nVendor != null)
            ////    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 1, "tblProjectImageOrMemory", "nVendor", tblProjectImageOrMemory.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nStatus Audit field-----------
            //if (tblProjectImageOrMemory.nStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 1, "tblProjectImageOrMemory", "nStatus", tblProjectImageOrMemory.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //////----nNumberOfTabletsPerStore Audit field-----------
            ////if (tblProjectImageOrMemory.nNumberOfTabletsPerStore != null)
            ////    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 1, "tblProjectImageOrMemory", "nNumberOfTabletsPerStore", tblProjectImageOrMemory.nNumberOfTabletsPerStore.ToString(), "", lUserId, nCreateOrUpdate);
            ////----DeliveryDate Audit field-----------
            //if (tblProjectImageOrMemory.dDeliveryDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectImageOrMemory.dDeliveryDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 4, "tblProjectImageOrMemory", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //} //----dShipDate Audit field-----------
            //if (tblProjectImageOrMemory.dShipDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectImageOrMemory.dShipDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 4, "tblProjectImageOrMemory", "dShipDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            //////----Cost Audit field-----------
            ////if (tblProjectImageOrMemory.cCost != null)
            ////    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 1, "tblProjectImageOrMemory", "cCost", tblProjectImageOrMemory.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            //db.Entry(tblProjectImageOrMemory).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!tblProjectAudioExists(tblProjectImageOrMemory.aServerHandheldId))
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

        // POST: api/ProjectAudios
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProjectImageOrMemory tblProjectImageOrMemory)
        {
            //var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectImageOrMemory set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectImageOrMemory.nStoreId));
            ////tblProjectImageOrMemory.ProjectActiveStatus = 1; SantoshPP
            //tblProjectImageOrMemory.aServerHandheldId = 0;
            //Misc.Utilities.SetActiveProjectId(Misc.ProjectType.ServerHandheld, tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory);
            //db.tblProjectImageOrMemories.Add(tblProjectImageOrMemory);
            //await db.SaveChangesAsync();

            //var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            //int nCreateOrUpdate = 1;
            //////----Vendor Audit field-----------
            ////if (tblProjectImageOrMemory.nVendor != null)
            ////    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 1, "tblProjectImageOrMemory", "nVendor", tblProjectImageOrMemory.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nStatus Audit field-----------
            //if (tblProjectImageOrMemory.nStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 1, "tblProjectImageOrMemory", "nStatus", tblProjectImageOrMemory.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //////----nNumberOfTabletsPerStore Audit field-----------
            ////if (tblProjectImageOrMemory.nNumberOfTabletsPerStore != null)
            ////    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 1, "tblProjectImageOrMemory", "nNumberOfTabletsPerStore", tblProjectImageOrMemory.nNumberOfTabletsPerStore.ToString(), "", lUserId, nCreateOrUpdate);
            ////----DeliveryDate Audit field-----------
            //if (tblProjectImageOrMemory.dDeliveryDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectImageOrMemory.dDeliveryDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 4, "tblProjectImageOrMemory", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //} //----dShipDate Audit field-----------
            //if (tblProjectImageOrMemory.dShipDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectImageOrMemory.dShipDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 4, "tblProjectImageOrMemory", "dShipDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            //////----Cost Audit field-----------
            ////if (tblProjectImageOrMemory.cCost != null)
            ////    Misc.Utilities.AddToAudit(tblProjectImageOrMemory.nStoreId, tblProjectImageOrMemory.nProjectID, 1, "tblProjectImageOrMemory", "cCost", tblProjectImageOrMemory.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            return Json(tblProjectImageOrMemory);
        }

        // DELETE: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectImageOrMemory tblProjectImageOrMemory = await db.tblProjectImageOrMemories.FindAsync(id);
            if (tblProjectImageOrMemory == null)
            {
                return NotFound();
            }

            db.tblProjectImageOrMemories.Remove(tblProjectImageOrMemory);
            await db.SaveChangesAsync();

            return Ok(tblProjectImageOrMemory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectAudioExists(int id)
        {
            return db.tblProjectImageOrMemories.Count(e => e.aProjectImageOrMemoryID == id) > 0;
        }
    }
}
