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
    public class ProjectSonicRadiosController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectSonicRadios
        public IQueryable<tblProjectSonicRadio> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectSonicRadio> items=null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

                if (nProjectID != 0)
                {
                    return db.tblProjectSonicRadios.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectSonicRadio");
                   items = db.Database.SqlQuery<tblProjectSonicRadio>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                   // return items;
                }
            }catch (Exception ex) { }
            return items;

        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectSonicRadios/5
        [ResponseType(typeof(tblProjectSonicRadio))]
        public async Task<IHttpActionResult> GettblProjectSonicRadio(int id)
        {
            tblProjectSonicRadio tblProjectSonicRadio = await db.tblProjectSonicRadios.FindAsync(id);
            if (tblProjectSonicRadio == null)
            {
                return NotFound();
            }

            return Ok(tblProjectSonicRadio);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectSonicRadios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(tblProjectSonicRadio tblProjectSonicRadio)
        {
            //tblProjectSonicRadio.ProjectActiveStatus = 1;Santosh
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectSonicRadio.nStoreId, tblProjectSonicRadio);


            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 2;
            ////----Vendor Audit field-----------
            //if (tblProjectSonicRadio.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nVendor", tblProjectSonicRadio.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectSonicRadio.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nStatus", tblProjectSonicRadio.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nOutdoorSpeakers Audit field-----------
            //if (tblProjectSonicRadio.nOutdoorSpeakers != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nOutdoorSpeakers", tblProjectSonicRadio.nOutdoorSpeakers.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nColors Audit field-----------
            //if (tblProjectSonicRadio.nColors != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nColors", tblProjectSonicRadio.nColors.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nIndoorSpeakers Audit field-----------
            //if (tblProjectSonicRadio.nIndoorSpeakers != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nIndoorSpeakers", tblProjectSonicRadio.nIndoorSpeakers.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nZones Audit field-----------
            //if (tblProjectSonicRadio.nZones != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nZones", tblProjectSonicRadio.nZones.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nServerRacks Audit field-----------
            //if (tblProjectSonicRadio.nServerRacks != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nServerRacks", tblProjectSonicRadio.nServerRacks.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            if (tblProjectSonicRadio.dDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectSonicRadio.dDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 4, "tblProjectSonicRadio", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            } 
            ////----Cost Audit field-----------
            //if (tblProjectSonicRadio.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 3, "tblProjectSonicRadio", "cCost", tblProjectSonicRadio.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            db.Entry(tblProjectSonicRadio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectSonicRadioExists(tblProjectSonicRadio.aProjectSonicRadioID))
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
        // POST: api/ProjectSonicRadios
        [ResponseType(typeof(tblProjectSonicRadio))]
        public async Task<IHttpActionResult> Create(tblProjectSonicRadio tblProjectSonicRadio)
        {
            var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectSonicRadio set nMyActiveStatus=0 where nStoreId =@nStoreId", new SqlParameter("@nStoreId", tblProjectSonicRadio.nStoreId));

            //tblProjectSonicRadio.ProjectActiveStatus = 1;Santosh
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.New, tblProjectSonicRadio.nStoreId, tblProjectSonicRadio);
            tblProjectSonicRadio.aProjectSonicRadioID = 0;

            db.tblProjectSonicRadios.Add(tblProjectSonicRadio);
            await db.SaveChangesAsync();

            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            int nBrandID = 6;
            int nCreateOrUpdate = 1;
            ////----Vendor Audit field-----------
            //if (tblProjectSonicRadio.nVendor != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nVendor", tblProjectSonicRadio.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
            //----nStatus Audit field-----------
            if (tblProjectSonicRadio.nStatus != null)
                Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nStatus", tblProjectSonicRadio.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nOutdoorSpeakers Audit field-----------
            //if (tblProjectSonicRadio.nOutdoorSpeakers != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nOutdoorSpeakers", tblProjectSonicRadio.nOutdoorSpeakers.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nColors Audit field-----------
            //if (tblProjectSonicRadio.nColors != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nColors", tblProjectSonicRadio.nColors.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nIndoorSpeakers Audit field-----------
            //if (tblProjectSonicRadio.nIndoorSpeakers != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nIndoorSpeakers", tblProjectSonicRadio.nIndoorSpeakers.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nZones Audit field-----------
            //if (tblProjectSonicRadio.nZones != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nZones", tblProjectSonicRadio.nZones.ToString(), "", lUserId, nCreateOrUpdate);
            ////----nServerRacks Audit field-----------
            //if (tblProjectSonicRadio.nServerRacks != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 1, "tblProjectSonicRadio", "nServerRacks", tblProjectSonicRadio.nServerRacks.ToString(), "", lUserId, nCreateOrUpdate);
            //----DeliveryDate Audit field-----------
            if (tblProjectSonicRadio.dDeliveryDate != null)
            {
                DateTime dt = DateTime.Parse(tblProjectSonicRadio.dDeliveryDate.ToString());
                Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 4, "tblProjectSonicRadio", "dDeliveryDate", dt.ToString("yyyy-MM-dd"), "", lUserId, nCreateOrUpdate);
            }
            ////----Cost Audit field-----------
            //if (tblProjectSonicRadio.cCost != null)
            //    Misc.Utilities.AddToAudit(tblProjectSonicRadio.nStoreId, tblProjectSonicRadio.nProjectID, 3, "tblProjectSonicRadio", "cCost", tblProjectSonicRadio.cCost.ToString(), "", lUserId, nCreateOrUpdate);


            return Json(tblProjectSonicRadio);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectSonicRadios/5
        [ResponseType(typeof(tblProjectSonicRadio))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectSonicRadio tblProjectSonicRadio = await db.tblProjectSonicRadios.FindAsync(id);
            if (tblProjectSonicRadio == null)
            {
                return NotFound();
            }

            db.tblProjectSonicRadios.Remove(tblProjectSonicRadio);
            await db.SaveChangesAsync();

            return Ok(tblProjectSonicRadio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectSonicRadioExists(int id)
        {
            return db.tblProjectSonicRadios.Count(e => e.aProjectSonicRadioID == id) > 0;
        }
    }
}