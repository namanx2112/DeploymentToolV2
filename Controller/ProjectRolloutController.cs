using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;
using DeploymentTool.Misc;
using Newtonsoft.Json;

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

                int nBrandID = searchFields!=null && searchFields.ContainsKey("nBrandID") ? Convert.ToInt32(searchFields["nBrandID"]) : 0;
                //int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;
               

                if (nBrandID != 0)
                {
                    items = db.tblProjectsRollouts.Where(p => p.nBrandID == nBrandID).AsQueryable();
                }
                else
                    items = db.tblProjectsRollouts.AsQueryable();

            }
            catch (Exception ex)
            {
            }
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
        public async Task<HttpResponseMessage> Create(ProjectsRolloutModel request)
        {
            string strReturn = "";
            tblProjectsRollout tblRollout = request.GetTblProjectsRollout();
            try
            {
                
                if (request.aProjectsRolloutID>0)
                {
                    db.Entry(tblRollout).State = EntityState.Modified;
                }
                else
                {
                    tblRollout.aProjectsRolloutID = 0;
                    // Misc.Utilities.SetActiveProjectId(Misc.ProjectType.AudioInstallation, tblProjectsRollout.nStoreId, tblProjectsRollout);
                    db.tblProjectsRollouts.Add(tblRollout);
                }
                
                await db.SaveChangesAsync();
                int aProjectsRolloutID = tblRollout.aProjectsRolloutID;
                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                Nullable<int> lUserId = securityContext.nUserID;
                int? nBrandID = request.nBrandID;
                int nCreateOrUpdate = 1;
                foreach (RolloutItem item in request.uploadingRows)
                {

                    if (item.type == ProjectType.OrderAccuracy)
                    {
                        strReturn += Misc.Utilities.CreateNewStoresForOrderAccurcy(item.items, nBrandID, aProjectsRolloutID);
                    }
                    else if (item.type == ProjectType.OrderStatusBoard)
                    {
                        strReturn += Misc.Utilities.CreateStoreFromExcelForOrderStatusBoard(item.items, nBrandID, aProjectsRolloutID);
                    }
                    else if (item.type == ProjectType.ArbysHPRollout)
                    {
                        strReturn += Misc.Utilities.CreateStoreFromExcelForHPRollout(item.items, nBrandID, aProjectsRolloutID);
                    }
                    else if (item.type == ProjectType.ServerHandheld)
                    {
                       // await CreateStoreFromExcelForServerHandheld(item.items, nBrandID,  aProjectsRolloutID);
                       strReturn+= Misc.Utilities.CreateStoreFromExcelForServerHandheld(item.items, nBrandID, aProjectsRolloutID);

                    }
                }
                ////----Vendor Audit field-----------
                //if (tblProjectsRollout.nVendor != null)
                //    Misc.Utilities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 1, "tblProjectsRollout", "nVendor", tblProjectsRollout.nVendor.ToString(), "", lUserId, nCreateOrUpdate);
                ////----nStatus Audit field-----------
                //if (tblProjectsRollout.nStatus != null)
                //    Misc.Util
                //    ities.AddToAudit(tblProjectsRollout.nStoreId, tblProjectsRollout.nProjectID, 1, "tblProjectsRollout", "nStatus", tblProjectsRollout.nStatus.ToString(), "", lUserId, nCreateOrUpdate);
                if (strReturn != "")
                    strReturn =" Rollout created successfully and failed to import following stores:"+ strReturn;
                else
                    strReturn = "Save successfully!";
            }
            catch (Exception ex)
            {

            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<string>(strReturn, new JsonMediaTypeFormatter())
            };
        }      
        [Authorize]
        [HttpGet]
        [Route("api/Store/GetMyProjects")]
        public HttpResponseMessage GetMyProjects(int nProjectsRolloutID)
        {
            var reportTable = new DataTable();
            DbConnection connection = db.Database.Connection;
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
            using (var cmd = dbFactory.CreateCommand())
            {
                string strName = string.Empty;
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sproc_GetMyProjectsForRollout";
                var reportIdParam = new SqlParameter("@nProjectsRolloutID", nProjectsRolloutID);
                cmd.Parameters.Add(reportIdParam);
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(reportTable);
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<DataTable>(reportTable, new JsonMediaTypeFormatter())
            };
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


