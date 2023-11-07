using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;

namespace DeploymentTool.Model
{
    public class ProjectNetworkingsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectNetworkings
        public IQueryable<tblProjectNetworking> Get(Dictionary<string, string> searchFields)
        {

            IQueryable<tblProjectNetworking> items = null;
            try
            {
                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

                if (nProjectID != 0)
                {
                    return db.tblProjectNetworkings.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectNetworking");
                   items = db.Database.SqlQuery<tblProjectNetworking>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                  //  return items;
                }
            }
            catch (Exception ex) { }

            return items;

        }

        // GET: api/ProjectNetworkings/5
        [ResponseType(typeof(tblProjectNetworking))]
        public async Task<IHttpActionResult> GetProjectNetworking(int id)
        {
            tblProjectNetworking tblProjectNetworking = await db.tblProjectNetworkings.FindAsync(id);
            if (tblProjectNetworking == null)
            {
                return NotFound();
            }

            return Ok(tblProjectNetworking);
        }

        // PUT: api/ProjectNetworkings/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> update(tblProjectNetworking tblProjectNetworking)
        {
            //tblProjectNetworking.ProjectActiveStatus = 1;Santosh
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectNetworking.nStoreId, tblProjectNetworking);
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 2;
            ////----Vendor Audit field-----------
            //if (tblProjectNetworking.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nVendor", tblProjectNetworking.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nPrimaryStatus Audit field-----------
            if (tblProjectNetworking.nPrimaryStatus != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nPrimaryStatus", tblProjectNetworking.nPrimaryStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----nPrimaryType Audit field-----------
            if (tblProjectNetworking.nPrimaryType != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nPrimaryType", tblProjectNetworking.nPrimaryType.ToString(), "", lUserId, nCreateOrUpdate);
            //----nBackupStatus Audit field-----------
            if (tblProjectNetworking.nBackupStatus != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nBackupStatus", tblProjectNetworking.nBackupStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----nBackupType Audit field-----------
            if (tblProjectNetworking.nBackupType != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nBackupType", tblProjectNetworking.nBackupType.ToString(), "", lUserId, nCreateOrUpdate);
            //----nTempStatus Audit field-----------
            if (tblProjectNetworking.nTempStatus != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nTempStatus", tblProjectNetworking.nTempStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----nTempType Audit field-----------
            if (tblProjectNetworking.nTempType != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nTempType", tblProjectNetworking.nTempType.ToString(), "", lUserId, nCreateOrUpdate);
            //----dPrimaryDate Audit field-----------
            if (tblProjectNetworking.dPrimaryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectNetworking.dPrimaryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 4, "tblProjectNetworking", "dPrimaryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            //----dBackupDate Audit field-----------
            if (tblProjectNetworking.dBackupDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectNetworking.dBackupDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 4, "tblProjectNetworking", "dBackupDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            //----dTempDate Audit field-----------
            if (tblProjectNetworking.dTempDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectNetworking.dTempDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 4, "tblProjectNetworking", "dTempDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
          
            db.Entry(tblProjectNetworking).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectNetworkingExists(tblProjectNetworking.aProjectNetworkingID))
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

        // POST: api/ProjectNetworkings
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProjectNetworking tblProjectNetworking)
        {
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectNetworking set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectNetworking.nStoreId));

            //tblProjectNetworking.ProjectActiveStatus = 1;Santosh
            tblProjectNetworking.aProjectNetworkingID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectNetworking.nStoreId, tblProjectNetworking);
            db.tblProjectNetworkings.Add(tblProjectNetworking);
            await db.SaveChangesAsync();
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 1;
            ////----Vendor Audit field-----------
            //if (tblProjectNetworking.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nVendor", tblProjectNetworking.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nPrimaryStatus Audit field-----------
            if (tblProjectNetworking.nPrimaryStatus != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nPrimaryStatus", tblProjectNetworking.nPrimaryStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----nPrimaryType Audit field-----------
            if (tblProjectNetworking.nPrimaryType != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nPrimaryType", tblProjectNetworking.nPrimaryType.ToString(), "", lUserId, nCreateOrUpdate);
            //----nBackupStatus Audit field-----------
            if (tblProjectNetworking.nBackupStatus != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nBackupStatus", tblProjectNetworking.nBackupStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----nBackupType Audit field-----------
            if (tblProjectNetworking.nBackupType != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nBackupType", tblProjectNetworking.nBackupType.ToString(), "", lUserId, nCreateOrUpdate);
            //----nTempStatus Audit field-----------
            if (tblProjectNetworking.nTempStatus != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nTempStatus", tblProjectNetworking.nTempStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----nTempType Audit field-----------
            if (tblProjectNetworking.nTempType != null)
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 1, "tblProjectNetworking", "nTempType", tblProjectNetworking.nTempType.ToString(), "", lUserId, nCreateOrUpdate);
            //----dPrimaryDate Audit field-----------
            if (tblProjectNetworking.dPrimaryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectNetworking.dPrimaryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 4, "tblProjectNetworking", "dPrimaryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            //----dBackupDate Audit field-----------
            if (tblProjectNetworking.dBackupDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectNetworking.dBackupDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 4, "tblProjectNetworking", "dBackupDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            //----dTempDate Audit field-----------
            if (tblProjectNetworking.dTempDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectNetworking.dTempDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectNetworking.nStoreId, tblProjectNetworking.nProjectID, 4, "tblProjectNetworking", "dTempDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }

            return Json(tblProjectNetworking);
        }

        // DELETE: api/ProjectNetworkings/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectNetworking tblProjectNetworking = await db.tblProjectNetworkings.FindAsync(id);
            if (tblProjectNetworking == null)
            {
                return NotFound();
            }

            db.tblProjectNetworkings.Remove(tblProjectNetworking);
            await db.SaveChangesAsync();

            return Ok(tblProjectNetworking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectNetworkingExists(int id)
        {
            return db.tblProjectNetworkings.Count(e => e.aProjectNetworkingID == id) > 0;
        }
    }
}