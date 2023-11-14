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
    public class ProjectRolloutController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectAudios
        public IQueryable<tblProjectsRollout> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectsRollout> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;


                if (nProjectID != 0)
                {
                    return db.tblProjectsRollouts.Where(p => p.aProjectsRolloutID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectsRollout");
                    items = db.Database.SqlQuery<tblProjectsRollout>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    //return items;
                }
            }
            catch (Exception ex) { }
            return items;

        }

        // GET: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> GettblProjectsRollout(int id)
        {
            tblProjectsRollout tblProjectsRollout = await db.tblProjectsRollouts.FindAsync(id);
            if (tblProjectsRollout == null)
            {
                return NotFound();
            }

            return Ok(tblProjectsRollout);
        }

        // PUT: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectsRollout tblProjectsRollout)
        {

            //tblProjectsRollout.ProjectActiveStatus = 1;//SantoshPP\
           // Misc.Utilities.SetActiveProjectId(Misc.ProjectType.AudioInstallation, tblProjectsRollout.nStoreId, tblProjectsRollout);
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            //int nBrandID = 6;
            int nCreateOrUpdate = 2;
            ////----Vendor Audit field-----------
            //if (tblProjectsRollout.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 1, "tblProjectsRollout", "nVendor", tblProjectsRollout.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
           
            ////----Cost Audit field-----------
            //if (tblProjectsRollout.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 3, "tblProjectsRollout", "cCost", tblProjectsRollout.cCost.ToString(), "", lUserId, nCreateOrUpdate);

            ////----LoopStatus Audit field-----------
            //if (tblProjectsRollout.nLoopStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 1, "tblProjectsRollout", "nLoopStatus", tblProjectsRollout.nLoopStatus.ToString(), "", lUserId, nCreateOrUpdate);

            db.Entry(tblProjectsRollout).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectsRolloutExists(tblProjectsRollout.aProjectsRolloutID))
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
        public async Task<IHttpActionResult> Create(tblProjectsRollout tblProjectsRollout)
        {
           // var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectsRollout set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectsRollout.nStoreId));
            //tblProjectsRollout.ProjectActiveStatus = 1; SantoshPP
            tblProjectsRollout.aProjectsRolloutID = 0;
           // Misc.Utilities.SetActiveProjectId(Misc.ProjectType.AudioInstallation, tblProjectsRollout.nStoreId, tblProjectsRollout);
            db.tblProjectsRollouts.Add(tblProjectsRollout);
            await db.SaveChangesAsync();

            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 1;
            ////----Vendor Audit field-----------
            //if (tblProjectsRollout.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 1, "tblProjectsRollout", "nVendor", tblProjectsRollout.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nStatus Audit field-----------
            //if (tblProjectsRollout.nStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 1, "tblProjectsRollout", "nStatus", tblProjectsRollout.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
       
            return Json(tblProjectsRollout);
        }

        // DELETE: api/ProjectAudios/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectsRollout tblProjectsRollout = await db.tblProjectsRollouts.FindAsync(id);
            if (tblProjectsRollout == null)
            {
                return NotFound();
            }

            db.tblProjectsRollouts.Remove(tblProjectsRollout);
            await db.SaveChangesAsync();

            return Ok(tblProjectsRollout);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectsRolloutExists(int id)
        {
            return db.tblProjectsRollouts.Count(e => e.aProjectsRolloutID == id) > 0;
        }
    }
}

    
