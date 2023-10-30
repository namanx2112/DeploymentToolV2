﻿using DeploymentTool.Misc;
using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
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
            int nBrandId = (searchFields == null || searchFields["nBrandId"] == null) ? 0 : Convert.ToInt32(searchFields["nBrandId"]);

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
                                tLoopType = techparts.tLoopType,
                                tLoopStatus = techparts.tLoopStatus
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
                        else if (techparts.tComponent == "Server Handheld")
                        {
                            obj.serverhandheld = new ProjectPortfolioItems()
                            {
                                dtDate = techparts.dDeliveryDate?.Date,// == null ? DateTime.Now : (DateTime)techparts.dDeliveryDate,
                                tStatus = techparts.tStatus,
                                tVendor = techparts.tVendor
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
        [HttpPost]
        [Route("api/Store/GetReport")]
        public HttpResponseMessage GetReport(ReportRequest request)
        {
            ReportModel reportModel = new ReportModel()
            {
                reportTable = new DataTable(),
                tReportName = string.Empty
            };
            DbConnection connection = db.Database.Connection;
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
            using (var cmd = dbFactory.CreateCommand())
            {
                string strName = string.Empty;
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sproc_getReportData";
                var reportIdParam = new SqlParameter("@nReportId", request.reportId);                
                var reportNameParam = new SqlParameter("@tReportName", strName);
                var reportParameters1 = new SqlParameter("@pm1", request.tParam1.Trim(','));
                var reportParameters2= new SqlParameter("@pm2", request.tParam2);
                var reportParameters3 = new SqlParameter("@pm3", request.tParam3);
                var reportParameters4 = new SqlParameter("@pm4", request.tParam4);
                var reportParameters5 = new SqlParameter("@pm5", request.tParam5);
                reportNameParam.Direction = ParameterDirection.Output;
                reportNameParam.Size = 500;
                cmd.Parameters.Add(reportIdParam);                
                cmd.Parameters.Add(reportNameParam);
                cmd.Parameters.Add(reportParameters1);
                cmd.Parameters.Add(reportParameters2);
                cmd.Parameters.Add(reportParameters3);
                cmd.Parameters.Add(reportParameters4);
                cmd.Parameters.Add(reportParameters5);
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(reportModel.reportTable);
                    reportModel.tReportName = reportNameParam.Value.ToString();
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ReportModel>(reportModel, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/GetStoreTable")]
        public HttpResponseMessage GetStoreTable(Dictionary<string, string> searchFields)
        {
            //ReportRequest request = new ReportRequest()
            //{
            //    reportId = 4,
            //    tParam1 = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,17,22,33,34,35,36,51,57,72,73,74,85,86,87,88,89,90,91,93,94,95,96,97,98,99,100,101,106,107,108,109,110,111,112,113,114,115,116,117,155,156,157,158,159,160,161,",

            //};
            ReportModel reportModel = new ReportModel()
            {
                reportTable = new DataTable(),
                tReportName = string.Empty
            };
            DbConnection connection = db.Database.Connection;
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
            using (var cmd = dbFactory.CreateCommand())
            {
                int nBrandId = (searchFields["nBrandId"] == null) ? 0 : Convert.ToInt32(searchFields["nBrandId"]);
                var tProjTypes = (searchFields["tProjTypes"] == null) ? "" : Convert.ToString(searchFields["tProjTypes"]);

                var dtStart = (searchFields["dtStart"] == null) ? null : Convert.ToString(searchFields["dtStart"]);
                //DateTime dtEnd = (searchFields["dtEnd"] == null) ? null : Convert.ToDateTime(searchFields["dtEnd"]);
                var dtEnd = (searchFields["dtEnd"] == null) ? null : Convert.ToString(searchFields["dtEnd"]);
                var tVendor = (searchFields["tVendor"] == null) ? "" : Convert.ToString(searchFields["tVendor"]);
                var tFranchise = (searchFields["tFranchise"] == null) ? "" : Convert.ToString(searchFields["tFranchise"]);
                var tCity = (searchFields["tCity"] == null) ? null : Convert.ToString(searchFields["tCity"]);

                string strName = string.Empty;
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sproc_getAdvanceSearchData";
                var reportIdParam = new SqlParameter("@nBrandId", nBrandId);
                var reportParameters1 = new SqlParameter("@tProjectTypes", tProjTypes);
                var reportParameters2 = new SqlParameter("@dtStart", dtStart);
                var reportParameters3 = new SqlParameter("@dtEnd", dtEnd);
                var reportParameters4 = new SqlParameter("@tVendor", tVendor);
                var reportParameters5 = new SqlParameter("@tFranchise", tFranchise);
                var reportParameters6 = new SqlParameter("@tCity", tCity);
                cmd.Parameters.Add(reportIdParam);
                cmd.Parameters.Add(reportParameters1);
                cmd.Parameters.Add(reportParameters2);
                cmd.Parameters.Add(reportParameters3);
                cmd.Parameters.Add(reportParameters4);
                cmd.Parameters.Add(reportParameters5);
                cmd.Parameters.Add(reportParameters6);
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(reportModel.reportTable);
                    reportModel.tReportName = "Advance Search Result";
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ReportModel>(reportModel, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/GetDashboards")]
        public HttpResponseMessage GetDashboards(DashboardRequest request)
        {
          
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            List<DahboardTile> items = new List<DahboardTile>();
            try
            {
                // var nBrandID = 2;
                var tProjectTypesTemp = request.tProjectTypes == null ? "" : request.tProjectTypes;

                items = db.Database.SqlQuery<DahboardTile>("exec sproc_GetDashboardReports @nBrandID, @tProjectTypes,@nUserID,@dStarDate,@dEndDate", new SqlParameter("@nBrandID", request.nBrandId),
                    new SqlParameter("@tProjectTypes", tProjectTypesTemp), new SqlParameter("@nUserID", securityContext.nUserID),
                    new SqlParameter("@dStarDate", request.dStart), new SqlParameter("@dEndDate", request.dEnd)).ToList();
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("GetDashboards", HttpContext.Current, ex);
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<DahboardTile>>(items, new JsonMediaTypeFormatter())
            };
            //securityContext.nUserID
            //    List<DahboardTile> tils = new List<DahboardTile>()
            //    {
            //        new DahboardTile()
            //        {
            //             title = "Number of prjetcs live",
            //            count = 10,
            //            reportId = 1,
            //            type = DashboardTileType.Text
            //        },
            //        new DahboardTile()
            //        {
            //            title = "Project going live in 10 days",
            //            count = 44,
            //            reportId = 2,
            //            type = DashboardTileType.Text,
            //            compareWith = 30,
            //            compareWithText = "vs in last 10 days"
            //        },
            //        new DahboardTile()
            //        {
            //            title = "Project going live in next 30 days",
            //            count = 9,
            //            reportId = 3,
            //            type = DashboardTileType.Text
            //        },
            //        new DahboardTile()
            //{
            //            title = "Projects already deployed",
            //            count = 12,
            //            reportId = 4,
            //            type = DashboardTileType.Text
            //        },
            //        new DahboardTile()
            //        {
            //            title = "Revist date changed",
            //            count = 48,
            //            reportId = 5,
            //            type = DashboardTileType.Text
            //        },
            //        new DahboardTile()
            //        {
            //            title = "Project opened last month",
            //            count = 50,
            //            reportId = 7,
            //            type = DashboardTileType.Text,
            //            compareWith = 90,
            //            compareWithText = "vs previous year"
            //        },
            //        new DahboardTile()
            //        {
            //            title = "Number of projects cpmpleted vs Not Completed",
            //            count = 7,
            //            reportId = 6,
            //            type = DashboardTileType.Chart,
            //            chartType = "doughnut",
            //            chartValues =  new int[] {10, 50 },
            //            chartLabels = new string[] {"POS", "Audio" }
            //        }
        //};
            //return new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new ObjectContent<List<DahboardTile>>(tils, new JsonMediaTypeFormatter())
            //};
        }


        [Authorize]
        [HttpGet]
        [Route("api/Store/GetSavedReportsForMe")]
        public HttpResponseMessage GetSavedReportsForMe(int nBrandId)
        {

            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            List<MyReport> items = new List<MyReport>();
            try
            {

                items = db.Database.SqlQuery<MyReport>("exec sproc_GetMySavedReports @nBrandID, @nUserID", new SqlParameter("@nBrandID", nBrandId),
                    new SqlParameter("@nUserID", securityContext.nUserID)).ToList();
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("GetDashboards", HttpContext.Current, ex);
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<MyReport>>(items, new JsonMediaTypeFormatter())
            };            
        }

    }

}
