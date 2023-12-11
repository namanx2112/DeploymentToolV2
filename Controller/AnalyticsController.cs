using DeploymentTool.Misc;
using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
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
            int ncurrentPage = (searchFields == null || searchFields["currentPage"] == null) ? 0 : Convert.ToInt32(searchFields["currentPage"]);
            int npageSize = (searchFields == null || searchFields["pageSize"] == null) ? 0 : Convert.ToInt32(searchFields["pageSize"]);

            var tStoreNumber =  searchFields.ContainsKey("tStoreNumber") && (searchFields["tStoreNumber"] != null) ? Convert.ToString(searchFields["tStoreNumber"]) : "";
            //var tStoreNumber = "55003";//
            // var tFranchise = (searchFields["tFranchise"] == null) ? "" : Convert.ToString(searchFields["tFranchise"]);
            var tFranchise = searchFields.ContainsKey("tFranchise") && (searchFields["tFranchise"] != null) ? Convert.ToString(searchFields["tFranchise"]) : "";

            //var tCity = (searchFields["tCity"] == null) ? null : Convert.ToString(searchFields["tCity"]);
            var tProjTypes = searchFields.ContainsKey("tProjTypes") && (searchFields["tProjTypes"] != null) ? Convert.ToString(searchFields["tProjTypes"]) : "";
            var tITPM = searchFields.ContainsKey("tITPM") && (searchFields["tITPM"] != null) ? Convert.ToString(searchFields["tITPM"]) : "";

            TableResponse tableResponse = new TableResponse();
            List<ProjectPortfolio> items = new List<ProjectPortfolio>();
            try
            {
                Dictionary<int?, string> objVendor =Misc.Utilities.GetVendorList();
                Dictionary<int?, string> objDrop = Misc.Utilities.GetDropDownList();
                Dictionary<int?, string> objProj = Misc.Utilities.GetProjectType();

                ncurrentPage++;
              //  npageSize = 10000;
                db.Database.CommandTimeout= 1000;
                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                Nullable<int> lUserId = securityContext.nUserID;
                //var nTechComp = db.Database.ExecuteSqlCommand("select top 1 nVendorID from tblUserVendorRel where nuserid=@nUserID ",new SqlParameter("@nUserID", lUserId));
                var nTempVendor = db.Database.SqlQuery<int>("select top 1 nVendorID from tblUserVendorRel where nuserid=@nUserID  ", new SqlParameter("@nUserID", lUserId)).ToList();


                List<ActivePortFolioProjectsAllModel> activeProj = db.Database.SqlQuery<ActivePortFolioProjectsAllModel>(" exec sproc_getActivePortFolioProjects_new @nBrandId,@CurrentPage,@PageSize,@nUserID, @tStoreNumber, @tFranchise,@tITPM,@tProjectTypes "
                    , new SqlParameter("@nBrandId", nBrandId), new SqlParameter("@CurrentPage", ncurrentPage), 
                    new SqlParameter("@PageSize", npageSize), new SqlParameter("@nUserID", lUserId),
                    new SqlParameter("@tStoreNumber", tStoreNumber), new SqlParameter("@tFranchise", tFranchise), 
                    new SqlParameter("@tITPM", tITPM), new SqlParameter("@tProjectTypes", tProjTypes)
                    
                    ).ToList();

                //List<ActivePortFolioProjectsModel> activeProj = db.Database.SqlQuery<ActivePortFolioProjectsModel>("exec sproc_getActivePortFolioProjects @nBrandId,@CurrentPage,@PageSize,@nUserID "
                //   , new SqlParameter("@nBrandId", nBrandId), new SqlParameter("@CurrentPage", ncurrentPage), new SqlParameter("@PageSize", npageSize), new SqlParameter("@nUserID", lUserId)).ToList();

                foreach (var parts in activeProj)
                {
                    int nProjectId = parts.aProjectID;
                    if (nTempVendor.Count > 0 && Misc.Utilities.isProjectTypeWithSameVendor(nTempVendor[0],parts))
                        continue;
                        tableResponse.nTotalRows = parts.nTotalRows;
                    nProjectId = 0;//ignore project and get store notes
                                   //List<ProjectPorfolioNotes> portnotes = new List<ProjectPorfolioNotes>() ;// db.Database.SqlQuery<ProjectPorfolioNotes>("exec sproc_getProjectPortfolioNotes @nStoreId,@nProjectId", new SqlParameter("@nStoreId", parts.nStoreId), new SqlParameter("@nProjectId", nProjectId)).ToList();
                                   //ProjectPorfolioNotes objNot = new ProjectPorfolioNotes();
                                   //objNot.aNoteID = parts.aNoteID;
                                   //objNot.tNotesDesc = parts.tNoteDesc;
                                   //objNot.tNotesOwner = parts.tNotesOwner;

                    //portnotes.Add(objNot);
                    List<ProjectPorfolioNotes> portnotes = new List<ProjectPorfolioNotes>();
                    if (parts.aNoteID > 0)
                    {
                        portnotes.Add(new ProjectPorfolioNotes()
                        {

                            tNotesDesc = parts.tNoteDesc,
                            nProjectId = parts.aProjectID,
                            tNotesOwner = parts.tNotesOwner,
                            aNoteID = parts.aNoteID

                        });
                    }
                    ProjectPortfolio obj = new ProjectPortfolio();
                    obj.nProjectType = parts.nProjectType;
                    obj.tProjectType = objProj[parts.nProjectType]; // FIX1 ((ProjectType)parts.nProjectType).ToString(); // FIX1
                    obj.nProjectId = parts.aProjectID;
                    obj.notes = portnotes;
                    obj.nStoreId = parts.nStoreID;
                   // List<TechData> techData = db.Database.SqlQuery<TechData>("exec sproc_GetPortfolioData @nProjectId, @nStoreId", new SqlParameter("@nProjectId", parts.nProjectId), new SqlParameter("@nStoreId", parts.nStoreId)).ToList();

                    obj.store = new ProjectPortfolioStore()
                    {
                        tStoreNumber = parts.tStoreNumber,
                        tStoreDetails = parts.tStoreDetails,
                       // dtGoliveDate = (DateTime)parts.dProjectGoliveDate,
                       
                        dtGoliveDate = parts.dGoliveDate?.Date,
                        tProjectManager = parts.tProjectManager,
                        tProjectType = objProj[parts.nProjectType], //FIX2
                        tFranchise = parts.tFranchise,
                        cCost = parts.cCost != null ? (decimal)parts.cCost : 0,
                        dInstallDate = parts.dInstallationDate?.Date,
                    };
                   // foreach (var techparts in techData)
                    {
                       // if (nTempVendor.Count > 0 && nTempVendor[0] != parts.nNetworkingVendorID)  
                                                                                                   //    continue;
                                                                                                   //string tTempStatus = null;
                                                                                                   //if (parts.nPrimaryStatus != null && Misc.Utilities.dropDownList.ContainsKey(parts.nPrimaryStatus) && parts.dDateFor_nPrimaryStatus != null)
                                                                                                   //    tTempStatus = Misc.Utilities.dropDownList.ContainsKey(parts.nPrimaryStatus) + Misc.Utilities.dropDownList[parts.nPrimaryStatus].ToString().Replace("[Day/", "[" + Convert.ToDateTime(parts.dDateFor_nPrimaryStatus).ToString("dd") + "//").Replace("/Month]", "" + Convert.ToDateTime(parts.dDateFor_nPrimaryStatus).ToString("MMM") + "]");
                                                                                                   //else if (parts.nPrimaryStatus != null && Misc.Utilities.dropDownList.ContainsKey(parts.nPrimaryStatus))
                                                                                                   //    tTempStatus = Misc.Utilities.dropDownList[parts.nPrimaryStatus].ToString();
                        if ((nTempVendor.Count < 1 && Misc.Utilities.GetProjectTypeWithTech(parts) )|| (nTempVendor.Count > 0 && nTempVendor[0] == parts.nNetworkingVendorID) )
                        {
                            obj.networking = new ProjectPortfolioItems()
                            {
                                dtDate = parts.dNetworkDeliveryDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nPrimaryStatus, parts.dDateFor_nPrimaryStatus),
                                tVendor = parts.nNetworkingVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nNetworkingVendorID) ? Misc.Utilities.vendorListData[parts.nNetworkingVendorID].ToString() : null
                            };
                        }

                        if (nTempVendor.Count < 1 && (Misc.Utilities.GetProjectTypeWithTech(parts) || (ProjectType)parts.nProjectType == ProjectType.POSInstallation) || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nPOSVendorID))
                        {
                            obj.pos = new ProjectPortfolioItems()
                            {                                
                                dtDate = parts.dPOSDeliveryDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nPOSnStatusID, parts.dPOSDateFor_nStatus),
                                tVendor = parts.nPOSVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nPOSVendorID) ? Misc.Utilities.vendorListData[parts.nPOSVendorID].ToString() : null,
                                dSupportDate = parts.dPOSSupportDate?.Date

                            };
                        }
                        if (nTempVendor.Count < 1 && (Misc.Utilities.GetProjectTypeWithTech(parts) || (ProjectType)parts.nProjectType == ProjectType.AudioInstallation) || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nAudioVendorID))
                        {
                            obj.audio = new ProjectPortfolioItems()
                            {
                                

                                dtDate = parts.dAudioDeliveryDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nAudionStatusID, parts.dAudioDateFor_nStatus),
                                tVendor = parts.nAudioVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nAudioVendorID) ? Misc.Utilities.vendorListData[parts.nAudioVendorID].ToString() : null,
                                tLoopType = Misc.Utilities.geDropDownStatusTextByID(parts.nLoopType, null),
                                tLoopStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nLoopStatus, parts.dDateFor_nLoopStatus)
                              
                            };
                        }
                        if ((nTempVendor.Count < 1 && Misc.Utilities.GetProjectTypeWithTech(parts))|| (nTempVendor.Count > 0 && nTempVendor[0] == parts.nSonicRadioVendorID))
                        {
                            obj.sonicradio = new ProjectPortfolioItems()
                            {
                                dtDate = parts.dSonicRadioDeliveryDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nSonicRadioStatusID, parts.dSonicRadioDateFor_nStatus),
                                tVendor = parts.nSonicRadioVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nSonicRadioVendorID) ? Misc.Utilities.vendorListData[parts.nSonicRadioVendorID].ToString() : null

                            };
                        }
                        if (nTempVendor.Count < 1 && (Misc.Utilities.GetProjectTypeWithTech(parts) || (ProjectType)parts.nProjectType == ProjectType.PaymentTerminalInstallation) || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nPaymentSystemVendorID))
                        {
                            obj.paymentsystem = new ProjectPortfolioItems()
                            {                                
                                dtDate = parts.dPaymentSystemDeliveryDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nPaymentSystemStatusID, parts.dPaymentSystemDateFor_nStatus),
                                tVendor = parts.nPaymentSystemVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nPaymentSystemVendorID) ? Misc.Utilities.vendorListData[parts.nPaymentSystemVendorID].ToString() : null,
                                tBuyPassID = Misc.Utilities.geDropDownStatusTextByID(parts.nBuyPassID, null),
                                tServerEPS = Misc.Utilities.geDropDownStatusTextByID(parts.nServerEPS, null)
                            };
                        }
                        if (nTempVendor.Count < 1 && (Misc.Utilities.GetProjectTypeWithTech(parts) || (ProjectType)parts.nProjectType == ProjectType.InteriorMenuInstallation) || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nInteriorMenusVendorID))
                        {
                            obj.interiormenu = new ProjectPortfolioItems()
                            {
                                dtDate = parts.dInteriorMenusDeliveryDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nInteriorMenusStatusID, parts.dInteriorMenusDateFor_nStatus),
                                tVendor = parts.nInteriorMenusVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nInteriorMenusVendorID) ? Misc.Utilities.vendorListData[parts.nInteriorMenusVendorID].ToString() : null

                            };
                        }
                        if (nTempVendor.Count < 1 && (Misc.Utilities.GetProjectTypeWithTech(parts) || (ProjectType)parts.nProjectType == ProjectType.ExteriorMenuInstallation) || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nExteriorMenusVendorID))
                        {
                            obj.exteriormenu = new ProjectPortfolioItems()
                            {
                                dtDate = parts.dExteriorMenusDeliveryDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nExteriorMenusStatusID, parts.dExteriorMenusDateFor_nStatus),
                                tVendor = parts.nExteriorMenusVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nExteriorMenusVendorID) ? Misc.Utilities.vendorListData[parts.nExteriorMenusVendorID].ToString() : null

                            };
                        }
                        if (nTempVendor.Count < 1 || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nInstallationVendorID))
                        {
                            obj.installation = new ProjectPortfolioItems()
                            {
                               

                                dtDate = parts.dInstallationDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nInstallationStatusID, parts.dInstallationDateFor_nStatus),
                                tVendor = parts.nInstallationVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nInstallationVendorID) ? Misc.Utilities.vendorListData[parts.nInstallationVendorID].ToString() : null,
                                dInstallEndDate= parts.dInstallationEndDate?.Date,
                                tSignoffs = Misc.Utilities.geDropDownStatusTextByID(parts.nSignoffs, null),
                                tTestTransactions = Misc.Utilities.geDropDownStatusTextByID(parts.nTestTransactions, null) 

                            };
                        }
                        if (nTempVendor.Count < 1 && (Misc.Utilities.GetProjectTypeWithTech(parts) || (ProjectType)parts.nProjectType == ProjectType.ServerHandheld) || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nServerHandheldVendorID))
                        {
                            obj.serverhandheld = new ProjectPortfolioItems()
                            {                                
                                dtDate = parts.dServerHandheldDeliveryDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nServerHandheldStatusID, parts.dServerHandheldDateFor_nStatus), 
                                tVendor = parts.nServerHandheldVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nServerHandheldVendorID) ? Misc.Utilities.vendorListData[parts.nServerHandheldVendorID].ToString() : null

                            };
                        }
                        if (nTempVendor.Count < 1 && (Misc.Utilities.GetProjectTypeWithTech(parts) || (ProjectType)parts.nProjectType == ProjectType.OrderAccuracy) || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nOrderAccuracyVendorID))
                        {
                            obj.orderaccuracy = new ProjectPortfolioItems()
                            {
                                dtDate = parts.dOrderAccuracyDeliveryDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nOrderAccuracyStatusID, parts.dOrderAccuracyDateFor_nStatus),
                                tVendor = parts.nOrderAccuracyVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nOrderAccuracyVendorID) ? Misc.Utilities.vendorListData[parts.nOrderAccuracyVendorID].ToString() : null

                            };
                        }
                        if (nTempVendor.Count < 1 && (Misc.Utilities.GetProjectTypeWithTech(parts) || (ProjectType)parts.nProjectType == ProjectType.OrderStatusBoard) || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nOrderStatusBoardVendorID))
                        {
                            obj.orderstatusboard = new ProjectPortfolioItems()
                            {
                                dtDate = parts.dOrderStatusBoardDeliveryDate?.Date,
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nOrderStatusBoardStatusID, parts.dOrderStatusBoardDateFor_nStatus),
                                tVendor = parts.nOrderStatusBoardVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nOrderStatusBoardVendorID) ? Misc.Utilities.vendorListData[parts.nOrderStatusBoardVendorID].ToString() : null

                            };
                        }
                        if (nTempVendor.Count < 1 && (Misc.Utilities.GetProjectTypeWithTech(parts) || (ProjectType)parts.nProjectType == ProjectType.ArbysHPRollout) || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nNetworkSwitchVendorID))
                        {
                            obj.networkswitch = new ProjectPortfolioItems()
                            {                                
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nNetworkSwitchStatusID, parts.dNetworkSwitchDateFor_nStatusID),
                                tVendor = parts.nNetworkSwitchVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nNetworkSwitchVendorID) ? Misc.Utilities.vendorListData[parts.nNetworkSwitchVendorID].ToString() : null

                            };
                        }
                        if (nTempVendor.Count < 1 && (Misc.Utilities.GetProjectTypeWithTech(parts) || (ProjectType)parts.nProjectType == ProjectType.ArbysHPRollout) || (nTempVendor.Count > 0 && nTempVendor[0] == parts.nImageOrMemoryVendorID))
                        {
                            obj.imagememory = new ProjectPortfolioItems()
                            {
                                
                                tStatus = Misc.Utilities.geDropDownStatusTextByID(parts.nImageOrMemoryStatusID, parts.dImageOrMemoryDateFor_nStatus),
                                tVendor = parts.nImageOrMemoryVendorID != null && Misc.Utilities.vendorListData.ContainsKey(parts.nImageOrMemoryVendorID) ? Misc.Utilities.vendorListData[parts.nImageOrMemoryVendorID].ToString() : null

                            };
                        }
                    }
                    items.Add(obj);

                }
            }
            catch (Exception ex)
            {

            }
            tableResponse.response = items;
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<TableResponse>(tableResponse, new JsonMediaTypeFormatter())
            };

        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/DownloadReport")]
        public HttpResponseMessage DownloadReport(ReportRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            Stream stream = new MemoryStream(null);
            var fileContent = new StreamContent(stream);
            response.Content = fileContent;
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "changeMe.xlsx";
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.ms-excel");

            return response;
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
            var nTotalRows = 0;
            try
            {
                DbConnection connection = db.Database.Connection;
                DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);

                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                Nullable<int> lUserId = securityContext.nUserID;

                using (var cmd = dbFactory.CreateCommand())
                {
                    int ncurrentPage = request.nCurrentPage != null ? Convert.ToInt32(request.nCurrentPage) : 0;
                    int npageSize = request.nPageSize != null ? Convert.ToInt32(request.nPageSize) : 0;
                    ncurrentPage++;
                    //npageSize = 11000;
                    string strName = string.Empty;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sproc_getReportData";
                    var reportIdParam = new SqlParameter("@nReportId", request.reportId);
                    var reportNameParam = new SqlParameter("@tReportName", strName);
                    //var reportParameters1 = new SqlParameter("@pm1", request.tParam1.Trim(','));
                     var reportParameters1 = new SqlParameter("@pm1", request.tParam1!=null?request.tParam1.Trim(','):null);
               
					var reportParameters2 = new SqlParameter("@pm2", request.tParam2);
                    var reportParameters3 = new SqlParameter("@pm3", ncurrentPage);
                    var reportParameters4 = new SqlParameter("@pm4", npageSize);
                    var reportParameters5 = new SqlParameter("@pm5", lUserId);
                    // var reportParameters6 = new SqlParameter("@CurrentPage", ncurrentPage);
                    // var reportParameters7 = new SqlParameter("@PageSize", npageSize);
                    cmd.CommandTimeout = 100000;
                    reportNameParam.Direction = ParameterDirection.Output;
                    reportNameParam.Size = 500;
                    cmd.Parameters.Add(reportIdParam);
                    cmd.Parameters.Add(reportNameParam);
                    cmd.Parameters.Add(reportParameters1);
                    cmd.Parameters.Add(reportParameters2);
                    cmd.Parameters.Add(reportParameters3);
                    cmd.Parameters.Add(reportParameters4);
                    cmd.Parameters.Add(reportParameters5);
                    //cmd.Parameters.Add(reportParameters6);
                    //cmd.Parameters.Add(reportParameters7);
                    using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        Misc.Utilities.UpadateROPValue(dt);
                       
                        reportModel.reportTable = dt;
                        reportModel.tReportName = reportNameParam.Value.ToString();
                    }
                }

                //  if (reportModel.reportTable.Columns["nTotalRows"] != null)
                if (reportModel.reportTable.Columns["nTotalRows"] != null && reportModel.reportTable.Rows.Count > 0)
                {
                    nTotalRows = reportModel.reportTable.Rows[0].Field<int>("nTotalRows");
                    if (reportModel.reportTable.Columns.Contains("nTotalRows"))
                        reportModel.reportTable.Columns.Remove("nTotalRows");
                }
                // reportModel.reportTable.Columns.RemoveAt(columnIndex);
            }
            catch (Exception ex)
            {

            }
            TableResponse resp = new TableResponse()
            {
                response = reportModel,
                nTotalRows = nTotalRows
            };
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<TableResponse>(resp, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/DownloadStoreTable")]
        public HttpResponseMessage DownloadStoreTable(Dictionary<string, string> searchFields)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            Stream stream = new MemoryStream(null);
            var fileContent = new StreamContent(stream);
            response.Content = fileContent;
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "changeMe.xlsx";
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.ms-excel");

            return response;
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
               // int nBrandId = (searchFields["nBrandId"] == null) ? 0 : Convert.ToInt32(searchFields["nBrandId"]);
                var nBrandId = searchFields.ContainsKey("nBrandId") && (searchFields["nBrandId"] != null) ? Convert.ToInt32(searchFields["nBrandId"]) : 0;

                //  var tProjTypes = (searchFields["tProjTypes"] == null) ? "" : Convert.ToString(searchFields["tProjTypes"]);
                var tProjTypes = searchFields.ContainsKey("tProjTypes") && (searchFields["tProjTypes"] != null) ? Convert.ToString(searchFields["tProjTypes"]) : "";

                //var dtStart = (searchFields["dtStart"] == null) ? null : Convert.ToString(searchFields["dtStart"]);
                var dtStart = searchFields.ContainsKey("dtStart") && (searchFields["dtStart"] != null) ? Convert.ToString(searchFields["dtStart"]) : null;

                //DateTime dtEnd = (searchFields["dtEnd"] == null) ? null : Convert.ToDateTime(searchFields["dtEnd"]);
                // var dtEnd = (searchFields["dtEnd"] == null) ? null : Convert.ToString(searchFields["dtEnd"]);
                var dtEnd = searchFields.ContainsKey("dtEnd") && (searchFields["dtEnd"] != null) ? Convert.ToString(searchFields["dtEnd"]) : null;

                // var tVendor = (searchFields["tVendor"] == null) ? "" : Convert.ToString(searchFields["tVendor"]);
                var tVendor = searchFields.ContainsKey("tVendor") && (searchFields["tVendor"] != null) ? Convert.ToString(searchFields["tVendor"]) : "";

                // var tFranchise = (searchFields["tFranchise"] == null) ? "" : Convert.ToString(searchFields["tFranchise"]);
                var tFranchise = searchFields.ContainsKey("tFranchise") && (searchFields["tFranchise"] != null) ? Convert.ToString(searchFields["tFranchise"]) : "";

                //var tCity = (searchFields["tCity"] == null) ? null : Convert.ToString(searchFields["tCity"]);
                var tCity = searchFields.ContainsKey("tCity") && (searchFields["tCity"] != null) ? Convert.ToString(searchFields["tCity"]) : null;
                var tITPM = searchFields.ContainsKey("tITPM") && (searchFields["tITPM"] != null) ?  Convert.ToString(searchFields["tITPM"]):null;
                var tInstaller = searchFields.ContainsKey("tInstaller") && (searchFields["tInstaller"] != null) ? Convert.ToString(searchFields["tInstaller"]) : null;
                var tState = searchFields.ContainsKey("tState") && (searchFields["tState"] != null) ? Convert.ToString(searchFields["tState"]) : null;
                //"[defaultCondition, nVendor=6]"
                var defaultCondition =searchFields.ContainsKey("defaultCondition") && (searchFields["defaultCondition"] != null) ?  Convert.ToString(searchFields["defaultCondition"]):"";
                if (defaultCondition != "")
                {
                    //if(defaultCondition.Contains("nEquipVendor"))
                    //    tVendor = defaultCondition.Replace("nEquipVendor=", "");
                    //else if (defaultCondition.Contains("nInstallVendor"))
                    //    tInstaller = defaultCondition.Replace("nInstallVendor=", "");
                    defaultCondition = defaultCondition.Replace("nVendor=", "");
                }
                int ncurrentPage = searchFields.ContainsKey("currentPage") && (searchFields["currentPage"] != null) ? Convert.ToInt32(searchFields["currentPage"]) : 0;
                int npageSize = searchFields.ContainsKey("pageSize") && (searchFields["pageSize"] != null) ? Convert.ToInt32(searchFields["pageSize"]) : 0;
                ncurrentPage++;
               // npageSize = 5;
                string strName = string.Empty;
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sproc_getAdvanceSearchData";
                cmd.CommandTimeout = 100000;
                var reportIdParam = new SqlParameter("@nBrandId", nBrandId);
                var reportParameters1 = new SqlParameter("@tProjectTypes", tProjTypes);
                var reportParameters2 = new SqlParameter("@dtStart", dtStart);
                var reportParameters3 = new SqlParameter("@dtEnd", dtEnd);
                var reportParameters4 = new SqlParameter("@tVendor", tVendor);
                var reportParameters5 = new SqlParameter("@tFranchise", tFranchise);
                var reportParameters6 = new SqlParameter("@tCity", tCity);
                var reportParameters7 = new SqlParameter("@tITPM", tITPM);
                var reportParameters8 = new SqlParameter("@tInstaller", tInstaller);
                var reportParameters9 = new SqlParameter("@tState", tState);
                var reportParameters10 = new SqlParameter("@defaultCondition", defaultCondition);
                var reportParameters11 = new SqlParameter("@CurrentPage", ncurrentPage);
                var reportParameters12 = new SqlParameter("@PageSize", npageSize);
                cmd.Parameters.Add(reportIdParam);
                cmd.Parameters.Add(reportParameters1);
                cmd.Parameters.Add(reportParameters2);
                cmd.Parameters.Add(reportParameters3);
                cmd.Parameters.Add(reportParameters4);
                cmd.Parameters.Add(reportParameters5);
                cmd.Parameters.Add(reportParameters6);
                cmd.Parameters.Add(reportParameters7);
                cmd.Parameters.Add(reportParameters8);
                cmd.Parameters.Add(reportParameters9);
                cmd.Parameters.Add(reportParameters10);
                cmd.Parameters.Add(reportParameters11);
                cmd.Parameters.Add(reportParameters12);
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(reportModel.reportTable);
                    reportModel.tReportName = "Advance Search Result";
                }
            }
            var nTotalRows = 0;
            if (reportModel.reportTable.Columns["nTotalRows"] != null && reportModel.reportTable.Rows.Count>0)
            {
                nTotalRows = reportModel.reportTable.Rows[0].Field<int>("nTotalRows");

                reportModel.reportTable.Columns.Remove("nTotalRows");
            }
           // reportModel.reportTable.Columns.RemoveAt(columnIndex);
            TableResponse resp = new TableResponse()
            {
                response = reportModel,
                nTotalRows = nTotalRows
            };
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<TableResponse>(resp, new JsonMediaTypeFormatter())
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
                var dtStart = request.dStart != null ? Convert.ToString(request.dStart) : "";
                var dtEnd = request.dEnd != null? Convert.ToString(request.dEnd) : "";

                items = db.Database.SqlQuery<DahboardTile>("exec sproc_GetDashboardReports @nBrandID, @tProjectTypes,@nUserID,@dStarDate,@dEndDate",
                    new SqlParameter("@nBrandID", request.nBrandId),
                    new SqlParameter("@tProjectTypes", tProjectTypesTemp), 
                    new SqlParameter("@nUserID", securityContext.nUserID),
                    new SqlParameter("@dStarDate", dtStart), new SqlParameter("@dEndDate", dtEnd)).ToList();
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
