using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;

namespace DeploymentTool.Model
{
    public class ProjectAudiosController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectAudios
        public IQueryable<tblProjectAudio> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectAudio> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;


                if (nProjectID != 0)
                {
                    return db.tblProjectAudios.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectAudio");
                  items = db.Database.SqlQuery<tblProjectAudio>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
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
            tblProjectAudio tblProjectAudio = await db.tblProjectAudios.FindAsync(id);
            if (tblProjectAudio == null)
            {
                return NotFound();
            }

            return Ok(tblProjectAudio);
        }

        // PUT: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectAudio tblProjectAudio)
        {

            //tblProjectAudio.ProjectActiveStatus = 1;//SantoshPP\
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.AudioInstallation, tblProjectAudio.nStoreId, tblProjectAudio);
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            int nCreateOrUpdate = 2;
            ////----Vendor Audit field-----------
            //if (tblProjectAudio.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 1, "tblProjectAudio", "nVendor", tblProjectAudio.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectAudio.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 1, "tblProjectAudio", "nStatus", tblProjectAudio.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            if (tblProjectAudio.dDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectAudio.dDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 4, "tblProjectAudio", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            ////----Configuration Audit field-----------
            //if (tblProjectAudio.nConfiguration != null)
            //{
            //    Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 1, "tblProjectAudio", "nConfiguration", tblProjectAudio.nConfiguration.ToString(), "", lUserId, nCreateOrUpdate);
            //}
            //----LoopDeliveryDate Audit field-----------
            if (tblProjectAudio.dLoopDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectAudio.dLoopDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 4, "tblProjectAudio", "dLoopDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            ////----Cost Audit field-----------
            //if (tblProjectAudio.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 3, "tblProjectAudio", "cCost", tblProjectAudio.cCost.ToString(), "", lUserId, nCreateOrUpdate);

            ////----LoopStatus Audit field-----------
            //if (tblProjectAudio.nLoopStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 1, "tblProjectAudio", "nLoopStatus", tblProjectAudio.nLoopStatus.ToString(), "", lUserId, nCreateOrUpdate);

            db.Entry(tblProjectAudio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectAudioExists(tblProjectAudio.aProjectAudioID))
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

        // POST: api/ProjectAudios
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProjectAudio tblProjectAudio)
        {
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectAudio set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectAudio.nStoreId));
            //tblProjectAudio.ProjectActiveStatus = 1; SantoshPP
            tblProjectAudio.aProjectAudioID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.AudioInstallation, tblProjectAudio.nStoreId, tblProjectAudio);
            db.tblProjectAudios.Add(tblProjectAudio);
            await db.SaveChangesAsync();

            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 1;
            ////----Vendor Audit field-----------
            //if (tblProjectAudio.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 1, "tblProjectAudio", "nVendor", tblProjectAudio.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nStatus Audit field-----------
            if (tblProjectAudio.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 1, "tblProjectAudio", "nStatus", tblProjectAudio.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            if (tblProjectAudio.dDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectAudio.dDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 4, "tblProjectAudio", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            ////----Configuration Audit field-----------
            //if (tblProjectAudio.nConfiguration != null)
            //{
            //    Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 1, "tblProjectAudio", "nConfiguration", tblProjectAudio.nConfiguration.ToString(), "", lUserId, nCreateOrUpdate);
            //}
            //----LoopDeliveryDate Audit field-----------
            if (tblProjectAudio.dLoopDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectAudio.dLoopDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 4, "tblProjectAudio", "dLoopDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            ////----Cost Audit field-----------
            //if (tblProjectAudio.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 3, "tblProjectAudio", "cCost", tblProjectAudio.cCost.ToString(), "", lUserId, nCreateOrUpdate);

            ////----LoopStatus Audit field-----------
            //if (tblProjectAudio.nLoopStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectAudio.nStoreId, tblProjectAudio.nProjectID, 1, "tblProjectAudio", "nLoopStatus", tblProjectAudio.nLoopStatus.ToString(), "", lUserId, nCreateOrUpdate);

            return Json(tblProjectAudio);
        }

        // DELETE: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectAudio tblProjectAudio = await db.tblProjectAudios.FindAsync(id);
            if (tblProjectAudio == null)
            {
                return NotFound();
            }

            db.tblProjectAudios.Remove(tblProjectAudio);
            await db.SaveChangesAsync();

            return Ok(tblProjectAudio);
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
            return db.tblProjectAudios.Count(e => e.aProjectAudioID == id) > 0;
        }
    }
}