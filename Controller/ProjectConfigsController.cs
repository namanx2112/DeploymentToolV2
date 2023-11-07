using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;
using DeploymentTool.Model;

namespace DeploymentTool.Controller
{
    public class ProjectConfigsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectConfigs
        public IQueryable<tblProjectConfig> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectConfig> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

                if (nProjectID != 0)
                {
                    return db.tblProjectConfigs.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectConfig");
                    items = db.Database.SqlQuery<tblProjectConfig>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    // return items;
                }
            }
            catch (Exception ex) { }
            return items;

        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectConfigs/5
        [ResponseType(typeof(tblProjectConfig))]
        public async Task<IHttpActionResult> GettblProjectConfig(int id)
        {
            tblProjectConfig tblProjectConfig = await db.tblProjectConfigs.FindAsync(id);
            if (tblProjectConfig == null)
            {
                return NotFound();
            }

            return Ok(tblProjectConfig);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectConfigs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(tblProjectConfig tblProjectConfig)
        {

            db.Entry(tblProjectConfig).State = EntityState.Modified;
            if (tblProjectConfig.nProjectID == null || tblProjectConfig.nProjectID == 0)// Special handling since it can be for individual project
            {
                Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectConfig.nStoreId, tblProjectConfig);
            }
            //var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            //int nCreateOrUpdate = 2;
            ////----nStallCount Audit field-----------
            //if (tblProjectConfig.nStallCount != null)
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 1, "tblProjectConfigs", "nStallCount", tblProjectConfig.nStallCount.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nDriveThru Audit field-----------
            //if (tblProjectConfig.nDriveThru != null)
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 1, "tblProjectConfig", "nDriveThru", tblProjectConfig.nDriveThru.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nInsideDining Audit field-----------
            //if (tblProjectConfig.nInsideDining != null)
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 1, "tblProjectConfig", "nInsideDining", tblProjectConfig.nInsideDining.ToString(), "", lUserId, nCreateOrUpdate);
            ////----dGroundBreak Audit field-----------
            //if (tblProjectConfig.dGroundBreak != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectConfig.dGroundBreak.ToString());
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 4, "tblProjectConfig", "dGroundBreak", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            ////----dKitchenInstall Audit field-----------
            //if (tblProjectConfig.dKitchenInstall != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectConfig.dKitchenInstall.ToString());
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 4, "tblProjectConfig", "dKitchenInstall", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            ////----cProjectCost Audit field-----------
            //if (tblProjectConfig.cProjectCost != null)
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 3, "tblProjectConfig", "cProjectCost", tblProjectConfig.cProjectCost.ToString(), "", lUserId, nCreateOrUpdate);

            try
            {
                await db.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectConfigExists(tblProjectConfig.aProjectConfigID))
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
        [Authorize]
        [HttpPost]
        // POST: api/ProjectConfigs
        [ResponseType(typeof(tblProjectConfig))]
        public async Task<IHttpActionResult> Create(tblProjectConfig tblProjectConfig)
        {
            if (tblProjectConfig.nProjectID == null || tblProjectConfig.nProjectID == 0)// Special handling since it can be for individual project
            {
                var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectConfig set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectConfig.nStoreId));
                Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectConfig.nStoreId, tblProjectConfig);
            }
            //tblProjectConfig.ProjectActiveStatus = 1;SantoshPP
            tblProjectConfig.aProjectConfigID = 0;            
            db.tblProjectConfigs.Add(tblProjectConfig);
            await db.SaveChangesAsync();

            //var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            //int nCreateOrUpdate = 1;
            ////----nStallCount Audit field-----------
            //if (tblProjectConfig.nStallCount != null)
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 1, "tblProjectConfigs", "nStallCount", tblProjectConfig.nStallCount.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nDriveThru Audit field-----------
            //if (tblProjectConfig.nDriveThru != null)
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 1, "tblProjectConfig", "nDriveThru", tblProjectConfig.nDriveThru.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nInsideDining Audit field-----------
            //if (tblProjectConfig.nInsideDining != null)
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 1, "tblProjectConfig", "nInsideDining", tblProjectConfig.nInsideDining.ToString(), "", lUserId, nCreateOrUpdate);
            ////----dGroundBreak Audit field-----------
            //if (tblProjectConfig.dGroundBreak != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectConfig.dGroundBreak.ToString());
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 4, "tblProjectConfig", "dGroundBreak", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            ////----dKitchenInstall Audit field-----------
            //if (tblProjectConfig.dKitchenInstall != null)
            //{
            //    DateTime dt = DateTime.Parse(tblProjectConfig.dKitchenInstall.ToString());
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 4, "tblProjectConfig", "dKitchenInstall", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            //}
            ////----cProjectCost Audit field-----------
            //if (tblProjectConfig.cProjectCost != null)
            //    Misc.Utilities.AddToAudit( tblProjectConfig.nStoreId, tblProjectConfig.nProjectID, 3, "tblProjectConfig", "cProjectCost", tblProjectConfig.cProjectCost.ToString(), "", lUserId, nCreateOrUpdate);

            return Json(tblProjectConfig);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectConfigs/5
        [ResponseType(typeof(tblProjectConfig))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectConfig tblProjectConfig = await db.tblProjectConfigs.FindAsync(id);
            if (tblProjectConfig == null)
            {
                return NotFound();
            }

            db.tblProjectConfigs.Remove(tblProjectConfig);
            await db.SaveChangesAsync();

            return Ok(tblProjectConfig);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectConfigExists(int id)
        {
            return db.tblProjectConfigs.Count(e => e.aProjectConfigID == id) > 0;
        }
    }
}