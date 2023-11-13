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
using DeploymentTool.Model;

namespace DeploymentTool.Controller
{
    public class ProjectInstallationsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectInstallations
        public IQueryable<tblProjectInstallation> Get(Dictionary<string, string> searchFields)
        {

            IQueryable<tblProjectInstallation> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

                if (nProjectID != 0)
                {
                    return db.tblProjectInstallations.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectInstallation");
                    items = db.Database.SqlQuery<tblProjectInstallation>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    //return items;
                }
            }
            catch (Exception ex) { }
            return items;

        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectInstallations/5
        [ResponseType(typeof(tblProjectInstallation))]
        public async Task<IHttpActionResult> GettblProjectInstallation(int id)
        {
            tblProjectInstallation tblProjectInstallation = await db.tblProjectInstallations.FindAsync(id);
            if (tblProjectInstallation == null)
            {
                return NotFound();
            }

            return Ok(tblProjectInstallation);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectInstallations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(tblProjectInstallation tblProjectInstallation)
        {
            db.Entry(tblProjectInstallation).State = EntityState.Modified;
            //tblProjectInstallation.ProjectActiveStatus = 1;Santosh
            if (tblProjectInstallation.nProjectID == null || tblProjectInstallation.nProjectID == 0)// Special handling since it can be for individual project
            {
                Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectInstallation.nStoreId, tblProjectInstallation);
            }
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 2;
            ////----Vendor Audit field-----------
            //if (tblProjectInstallation.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 1, "tblProjectInstallation", "nVendor", tblProjectInstallation.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectInstallation.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 1, "tblProjectInstallation", "nStatus", tblProjectInstallation.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----nSignoffs Audit field-----------
            if (tblProjectInstallation.nSignoffs != null)
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 1, "tblProjectInstallation", "nSignoffs", tblProjectInstallation.nSignoffs.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nTestTransactions Audit field-----------
            //if (tblProjectInstallation.nTestTransactions != null)
            //    Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 1, "tblProjectInstallation", "nTestTransactions", tblProjectInstallation.nTestTransactions.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nProjectStatus Audit field-----------
            //if (tblProjectInstallation.nProjectStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 1, "tblProjectInstallation", "nProjectStatus", tblProjectInstallation.nProjectStatus.ToString(), "", lUserId, nCreateOrUpdate);

            //----tLeadTech Audit field-----------
            if (tblProjectInstallation.tLeadTech != null)
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 2, "tblProjectInstallation", "tLeadTech", tblProjectInstallation.tLeadTech.ToString(), "", lUserId, nCreateOrUpdate);
            //----dInstallDate Audit field-----------
            if (tblProjectInstallation.dInstallDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectInstallation.dInstallDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 4, "tblProjectInstallation", "dInstallDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            //----dInstallEnd Audit field-----------
            if (tblProjectInstallation.dInstallEnd != null)
            {
                DateTime dt = DateTime.Parse(tblProjectInstallation.dInstallEnd.ToString());
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 4, "tblProjectInstallation", "dInstallEnd", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            //----dRevisitDate Audit field-----------
            if (tblProjectInstallation.dRevisitDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectInstallation.dRevisitDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 4, "tblProjectInstallation", "dRevisitDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }


            //----tInstallNotes Audit field-----------
            if (tblProjectInstallation.tInstallNotes != null)
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 2, "tblProjectInstallation", "tInstallNotes", tblProjectInstallation.tInstallNotes.ToString(), "", lUserId, nCreateOrUpdate);

            ////----cCost Audit field-----------
            //if (tblProjectInstallation.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 3, "tblProjectInstallation", "cCost", tblProjectInstallation.cCost.ToString(), "", lUserId, nCreateOrUpdate);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectInstallationExists(tblProjectInstallation.aProjectInstallationID))
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
        // POST: api/ProjectInstallations
        [ResponseType(typeof(tblProjectInstallation))]
        public async Task<IHttpActionResult> Create(tblProjectInstallation tblProjectInstallation)
        {

            if (tblProjectInstallation.nProjectID == null || tblProjectInstallation.nProjectID == 0)// Special handling since it can be for individual project
            {
                var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectInstallation set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectInstallation.nStoreId));
                Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectInstallation.nStoreId, tblProjectInstallation);
            }

            //tblProjectInstallation.ProjectActiveStatus = 1;Santosh

            tblProjectInstallation.aProjectInstallationID = 0;
            db.tblProjectInstallations.Add(tblProjectInstallation);
            await db.SaveChangesAsync();

            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 1;
            ////----Vendor Audit field-----------
            //if (tblProjectInstallation.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 1, "tblProjectInstallation", "nVendor", tblProjectInstallation.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectInstallation.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 1, "tblProjectInstallation", "nStatus", tblProjectInstallation.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            //----nSignoffs Audit field-----------
            if (tblProjectInstallation.nSignoffs != null)
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 1, "tblProjectInstallation", "nSignoffs", tblProjectInstallation.nSignoffs.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nTestTransactions Audit field-----------
            //if (tblProjectInstallation.nTestTransactions != null)
            //    Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 1, "tblProjectInstallation", "nTestTransactions", tblProjectInstallation.nTestTransactions.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nProjectStatus Audit field-----------
            //if (tblProjectInstallation.nProjectStatus != null)
            //    Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 1, "tblProjectInstallation", "nProjectStatus", tblProjectInstallation.nProjectStatus.ToString(), "", lUserId, nCreateOrUpdate);

            //----tLeadTech Audit field-----------
            if (tblProjectInstallation.tLeadTech != null)
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 2, "tblProjectInstallation", "tLeadTech", tblProjectInstallation.tLeadTech.ToString(), "", lUserId, nCreateOrUpdate);
            //----dInstallDate Audit field-----------
            if (tblProjectInstallation.dInstallDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectInstallation.dInstallDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 4, "tblProjectInstallation", "dInstallDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            //----dInstallEnd Audit field-----------
            if (tblProjectInstallation.dInstallEnd != null)
            {
                DateTime dt = DateTime.Parse(tblProjectInstallation.dInstallEnd.ToString());
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 4, "tblProjectInstallation", "dInstallEnd", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            //----dRevisitDate Audit field-----------
            if (tblProjectInstallation.dRevisitDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectInstallation.dRevisitDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 4, "tblProjectInstallation", "dRevisitDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }

            ////----cCost Audit field-----------
            //if (tblProjectInstallation.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectInstallation.nStoreId, tblProjectInstallation.nProjectID, 3, "tblProjectInstallation", "cCost", tblProjectInstallation.cCost.ToString(), "", lUserId, nCreateOrUpdate);

            if (tblProjectInstallation.dInstallEnd != null)
                db.Database.ExecuteSqlCommand("update tblProject set dGoLiveDate=@dInstallationDate where aProjectID =@aProjectID", new SqlParameter("@dInstallationDate", tblProjectInstallation.dInstallEnd), new SqlParameter("@aProjectID", tblProjectInstallation.nProjectID)); // Update goLive date as installation end date
            return Json(tblProjectInstallation);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectInstallations/5
        [ResponseType(typeof(tblProjectInstallation))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectInstallation tblProjectInstallation = await db.tblProjectInstallations.FindAsync(id);
            if (tblProjectInstallation == null)
            {
                return NotFound();
            }

            db.tblProjectInstallations.Remove(tblProjectInstallation);
            await db.SaveChangesAsync();

            return Ok(tblProjectInstallation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectInstallationExists(int id)
        {
            return db.tblProjectInstallations.Count(e => e.aProjectInstallationID == id) > 0;
        }
    }
}