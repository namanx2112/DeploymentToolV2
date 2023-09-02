using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Security;

namespace DeploymentTool.Controller
{
    public class AnalyticsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpPost]
        [Route("api/Store/GetProjectPortfolio")]
        public HttpResponseMessage GetProjectPortfolio(Dictionary<string, string> searchFields)
        {
            int nStoreId = 0;// (searchFields == null || searchFields["nStoreId"] == null) ? 0 : Convert.ToInt32(searchFields["nStoreId"]);
            int nBrandId =  (searchFields == null || searchFields["nBrandId"] == null) ? 0 : Convert.ToInt32(searchFields["nBrandId"]);

            List<ProjectPortfolio> items = new List<ProjectPortfolio>();
            try
            {
                List<ActivePortFolioProjectsModel> activeProj = db.Database.SqlQuery<ActivePortFolioProjectsModel>("exec sproc_getActivePortFolioProjects @nBrandId", new SqlParameter("@nBrandId", nBrandId)).ToList();

                foreach (var parts in activeProj)
                {
                    int nProjectId = parts.nProjectId;
                    nProjectId = 0;//ignore project and get store notes
                    List<ProjectPorfolioNotes> portnotes = db.Database.SqlQuery<ProjectPorfolioNotes>("exec sproc_getProjectPortfolioNotes @nStoreId,@nProjectId", new SqlParameter("@nStoreId", parts.nStoreId), new SqlParameter("@nProjectId", nProjectId)).ToList();

                    ProjectPortfolio obj = new ProjectPortfolio();
                    obj.nProjectType = parts.nProjectType;
                    obj.tProjectType = parts.tProjectType;
                    obj.nProjectId = parts.nProjectId;
                    obj.notes = portnotes;
                    obj.nStoreId = parts.nStoreId;
                    List<TechData> techData = db.Database.SqlQuery<TechData>("exec sproc_GetPortfolioData @nProjectId, @nStoreId", new SqlParameter("@nProjectId", parts.nProjectId), new SqlParameter("@nStoreId", parts.nStoreId)).ToList();

                    obj.store = new ProjectPortfolioStore()
                    {
                        tStoreNumber = parts.tStoreNumber,
                        tStoreDetails = parts.tStoreDetails,
                        dtGoliveDate = (DateTime)parts.dProjectGoliveDate,
                        tProjectManager = parts.tProjManager,
                        tProjectType = parts.tProjectType,
                        tFranchise = parts.tFranchise,
                        cCost = parts.cCost != null ? (decimal)parts.cCost : 0,
                        dInstallDate = parts.dInstallDate?.Date,
                    };
                    foreach (var techparts in techData)
                    {
                        if (techparts.tComponent == "Networking")
                        {
                            obj.networking = new ProjectPortfolioItems()
                            {
                                dtDate = techparts.dDeliveryDate?.Date,
                                tStatus = techparts.tStatus,
                                tVendor = techparts.tVendor
                            };
                        }
                        else if (techparts.tComponent == "POS")
                        {
                            obj.pos = new ProjectPortfolioItems()
                            {
                                dtDate = techparts.dDeliveryDate?.Date,
                                tStatus = techparts.tStatus,
                                tVendor = techparts.tVendor,
                                dSupportDate = techparts.dSupportDate?.Date
                            };
                        }
                        else if (techparts.tComponent == "Audio")
                        {
                            obj.audio = new ProjectPortfolioItems()
                            {
                                dtDate = techparts.dDeliveryDate?.Date,
                                tStatus = techparts.tStatus,
                                tVendor = techparts.tVendor,
                                tLoopType=techparts.tLoopType,
                                tLoopStatus=techparts.tLoopStatus
                            };
                        }
                        else if (techparts.tComponent == "Sonic Radio")
                        {
                            obj.sonicradio = new ProjectPortfolioItems()
                            {
                                dtDate = techparts.dDeliveryDate?.Date,
                                tStatus = techparts.tStatus,
                                tVendor = techparts.tVendor
                            };
                        }
                        else if (techparts.tComponent == "Payment Systems")
                        {
                            obj.paymentsystem = new ProjectPortfolioItems()
                            {
                                dtDate = techparts.dDeliveryDate?.Date,
                                tStatus = techparts.tStatus,
                                tVendor = techparts.tVendor,
                                tBuyPassID = techparts.tBuyPassID,
                                tServerEPS = techparts.tServerEPS
                            };
                        }
                        else if (techparts.tComponent == "Interior Menus")
                        {
                            obj.interiormenu = new ProjectPortfolioItems()
                            {
                                dtDate = techparts.dDeliveryDate?.Date,
                                tStatus = techparts.tStatus,
                                tVendor = techparts.tVendor
                            };
                        }
                        else if (techparts.tComponent == "Exterior Menus")
                        {
                            obj.exteriormenu = new ProjectPortfolioItems()
                            {
                                dtDate = techparts.dDeliveryDate?.Date,// == null ? DateTime.Now : (DateTime)techparts.dDeliveryDate,
                                tStatus = techparts.tStatus,
                                tVendor = techparts.tVendor
                            };
                        }
                        else if (techparts.tComponent == "Installation")
                        {
                            obj.installation = new ProjectPortfolioItems()
                            {
                                dtDate = techparts.dDeliveryDate?.Date,
                                tStatus = techparts.tStatus,
                                tVendor = techparts.tVendor,
                                dInstallEndDate = techparts.dInstallEndDate?.Date,
                                tSignoffs = techparts.tSignoffs,
                                tTestTransactions = techparts.tTestTransactions
                            };
                        }
                    }
                    items.Add(obj);

                }
            }
            catch (Exception ex)
            {

            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<ProjectPortfolio>>(items, new JsonMediaTypeFormatter())
            };

        }

        [Authorize]
        [HttpGet]
        [Route("api/Store/GetReport")]
        public HttpResponseMessage GetReport(int reportId)
        {
            ReportModel reportModel = new ReportModel()
            {
                tReportName = "Monthly report",
                titles = new List<string>() {
                    "Store Number", "Status","Franchise", "Delivery Date"
                },
                headers = new List<string>()
                {
                    "tStoreNumber",
                    "tStatus",
                    "tFranchise",
                    "dDeliveryDate"
                },
                data = new List<Dictionary<string, string>>()
                {
                    new Dictionary<string, string>(){
                    { "tStoreNumber" , "111" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "222" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "2321" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "434" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                },new Dictionary<string, string>(){
                    { "tStoreNumber" , "423432" },
                    {"tStatus", "Completed" },
                    {"tFranchise", "Bruce Banner" },
                    {"dDeliveryDate", DateTime.Now.ToString() }
                }
                }
            };
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ReportModel>(reportModel, new JsonMediaTypeFormatter())
            };
        }
    }

}
