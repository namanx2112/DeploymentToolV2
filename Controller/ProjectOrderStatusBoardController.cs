using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class ProjectOrderStatusBoardController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectAudios
        public IQueryable<tblProjectOrderStatusBoard> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectOrderStatusBoard> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;


                if (nProjectID != 0)
                {
                    return db.tblProjectOrderStatusBoards.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectOrderStatusBoard");
                    items = db.Database.SqlQuery<tblProjectOrderStatusBoard>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
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
            tblProjectOrderStatusBoard tblProjectOrderStatusBoard = await db.tblProjectOrderStatusBoards.FindAsync(id);
            if (tblProjectOrderStatusBoard == null)
            {
                return NotFound();
            }

            return Ok(tblProjectOrderStatusBoard);
        }

        // PUT: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectOrderStatusBoard tblProjectOrderStatusBoard)
        {

            ////tblProjectOrderStatusBoard.ProjectActiveStatus = 1;//SantoshPP\
            //Misc.Utilities.SetActiveProjectId(Misc.ProjectType.ServerHandheld, tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard);

            //var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            //int nCreateOrUpdate = 2;
            //////----Vendor Audit field-----------
            //// if (tblProjectOrderStatusBoard.nVendor != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 1, "tblProjectOrderStatusBoard", "nVendor", tblProjectOrderStatusBoard.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nStatus Audit field-----------
            //if (tblProjectOrderStatusBoard.nStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 1, "tblProjectOrderStatusBoard", "nStatus", tblProjectOrderStatusBoard.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //////----nNumberOfTabletsPerStore Audit field-----------
            ////if (tblProjectOrderStatusBoard.nNumberOfTabletsPerStore != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 1, "tblProjectOrderStatusBoard", "nNumberOfTabletsPerStore", tblProjectOrderStatusBoard.nNumberOfTabletsPerStore.ToString(), "", lUserId, nCreateOrUpdate);
            ////----DeliveryDate Audit field-----------
            //if (tblProjectOrderStatusBoard.dDeliveryDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectOrderStatusBoard.dDeliveryDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 4, "tblProjectOrderStatusBoard", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //} //----dShipDate Audit field-----------
            //if (tblProjectOrderStatusBoard.dShipDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectOrderStatusBoard.dShipDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 4, "tblProjectOrderStatusBoard", "dShipDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            //////----Cost Audit field-----------
            ////if (tblProjectOrderStatusBoard.cCost != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 1, "tblProjectOrderStatusBoard", "cCost", tblProjectOrderStatusBoard.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            //db.Entry(tblProjectOrderStatusBoard).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!tblProjectAudioExists(tblProjectOrderStatusBoard.aServerHandheldId))
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
        public async Task<IHttpActionResult> Create(tblProjectOrderStatusBoard tblProjectOrderStatusBoard)
        {
            //var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectOrderStatusBoard set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectOrderStatusBoard.nStoreId));
            ////tblProjectOrderStatusBoard.ProjectActiveStatus = 1; SantoshPP
            //tblProjectOrderStatusBoard.aServerHandheldId = 0;
            //Misc.Utilities.SetActiveProjectId(Misc.ProjectType.ServerHandheld, tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard);
            //db.tblProjectOrderStatusBoards.Add(tblProjectOrderStatusBoard);
            //await db.SaveChangesAsync();

            //var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            //int nCreateOrUpdate = 1;
            //////----Vendor Audit field-----------
            ////if (tblProjectOrderStatusBoard.nVendor != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 1, "tblProjectOrderStatusBoard", "nVendor", tblProjectOrderStatusBoard.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nStatus Audit field-----------
            //if (tblProjectOrderStatusBoard.nStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 1, "tblProjectOrderStatusBoard", "nStatus", tblProjectOrderStatusBoard.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //////----nNumberOfTabletsPerStore Audit field-----------
            ////if (tblProjectOrderStatusBoard.nNumberOfTabletsPerStore != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 1, "tblProjectOrderStatusBoard", "nNumberOfTabletsPerStore", tblProjectOrderStatusBoard.nNumberOfTabletsPerStore.ToString(), "", lUserId, nCreateOrUpdate);
            ////----DeliveryDate Audit field-----------
            //if (tblProjectOrderStatusBoard.dDeliveryDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectOrderStatusBoard.dDeliveryDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 4, "tblProjectOrderStatusBoard", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //} //----dShipDate Audit field-----------
            //if (tblProjectOrderStatusBoard.dShipDate != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectOrderStatusBoard.dShipDate.ToString());
            //    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 4, "tblProjectOrderStatusBoard", "dShipDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            //////----Cost Audit field-----------
            ////if (tblProjectOrderStatusBoard.cCost != null)
            ////    Misc.Utilities.AddToAudit(tblProjectOrderStatusBoard.nStoreId, tblProjectOrderStatusBoard.nProjectID, 1, "tblProjectOrderStatusBoard", "cCost", tblProjectOrderStatusBoard.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            return Json(tblProjectOrderStatusBoard);
        }

        // DELETE: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectOrderStatusBoard tblProjectOrderStatusBoard = await db.tblProjectOrderStatusBoards.FindAsync(id);
            if (tblProjectOrderStatusBoard == null)
            {
                return NotFound();
            }

            db.tblProjectOrderStatusBoards.Remove(tblProjectOrderStatusBoard);
            await db.SaveChangesAsync();

            return Ok(tblProjectOrderStatusBoard);
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
            return db.tblProjectOrderStatusBoards.Count(e => e.aProjectOrderStatusBoardID == id) > 0;
        }
    }
}
