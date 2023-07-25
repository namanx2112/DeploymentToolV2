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
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;

namespace DeploymentTool.Controller
{
    public class ProjectPaymentSystemsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        // GET: api/ProjectPaymentSystems
        public IQueryable<tblProjectPaymentSystem> Get(Dictionary<string, string> searchFields)
        {
            IQueryable<tblProjectPaymentSystem> items = null;
            try
            {

                int nProjectID = searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
                int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

                if (nProjectID != 0)
                {
                    return db.tblProjectPaymentSystems.Where(p => p.nProjectID == nProjectID).AsQueryable();
                }
                else
                {
                    SqlParameter tModuleNameParam = new SqlParameter("@nStoreId", nStoreId);
                    SqlParameter tModuleTech = new SqlParameter("@tTechnologyTableName", "tblProjectPaymentSystem");
                    items = db.Database.SqlQuery<tblProjectPaymentSystem>("exec sproc_getTechnologyData @nStoreId,@tTechnologyTableName", tModuleNameParam, tModuleTech).AsQueryable();
                    
                }
            }
            catch (Exception ex) { }
            return items;
        }
        [Authorize]
        [HttpPost]
        // GET: api/ProjectPaymentSystems/5
        [ResponseType(typeof(tblProjectPaymentSystem))]
        public async Task<IHttpActionResult> GettblProjectPaymentSystem(int id)
        {
            tblProjectPaymentSystem tblProjectPaymentSystem = await db.tblProjectPaymentSystems.FindAsync(id);
            if (tblProjectPaymentSystem == null)
            {
                return NotFound();
            }

            return Ok(tblProjectPaymentSystem);
        }
        [Authorize]
        [HttpPost]
        // PUT: api/ProjectPaymentSystems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(tblProjectPaymentSystem tblProjectPaymentSystem)
        {
            //tblProjectPaymentSystem.ProjectActiveStatus = 1;Santosh
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.PaymentTerminalInstallation, tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem);
            db.Entry(tblProjectPaymentSystem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblProjectPaymentSystemExists(tblProjectPaymentSystem.aProjectPaymentSystemID))
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
        // POST: api/ProjectPaymentSystems
        [ResponseType(typeof(tblProjectPaymentSystem))]
        public async Task<IHttpActionResult> Create(tblProjectPaymentSystem tblProjectPaymentSystem)
        {
            //var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblProjectPaymentSystem set projectActiveStatus=0 where nProjectId =@nProjectId", new SqlParameter("@nProjectId", tblProjectPaymentSystem.nProjectID));
            //tblProjectPaymentSystem.ProjectActiveStatus = 1;Santosh
            tblProjectPaymentSystem.aProjectPaymentSystemID = 0;
            Misc.Utilities.SetActiveProjectId(Misc.ProjectType.PaymentTerminalInstallation, tblProjectPaymentSystem.nStoreId, tblProjectPaymentSystem);
            db.tblProjectPaymentSystems.Add(tblProjectPaymentSystem);
            await db.SaveChangesAsync();

            return Json(tblProjectPaymentSystem);
        }
        [Authorize]
        [HttpPost]
        // DELETE: api/ProjectPaymentSystems/5
        [ResponseType(typeof(tblProjectPaymentSystem))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectPaymentSystem tblProjectPaymentSystem = await db.tblProjectPaymentSystems.FindAsync(id);
            if (tblProjectPaymentSystem == null)
            {
                return NotFound();
            }

            db.tblProjectPaymentSystems.Remove(tblProjectPaymentSystem);
            await db.SaveChangesAsync();

            return Ok(tblProjectPaymentSystem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblProjectPaymentSystemExists(int id)
        {
            return db.tblProjectPaymentSystems.Count(e => e.aProjectPaymentSystemID == id) > 0;
        }
    }
}