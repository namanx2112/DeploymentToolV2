using DeploymentTool.Misc;
using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class ReportGeneratorController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        public ReportGeneratorController() { }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetMyFolder(int nBrandId)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            SqlParameter tparam1 = new SqlParameter("@nBrandId", nBrandId);
            SqlParameter tparam2 = new SqlParameter("@nUserID", lUserId);
            List<ReportFolder> items = db.Database.SqlQuery<ReportFolder>("exec sproc_getMyfolderForReport @nBrandId,@nUserID ", tparam1, tparam2).ToList();


            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<ReportFolder>>(items, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetReportsForFolder(int nFolderId)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            SqlParameter tparam1 = new SqlParameter("@nFolderId", nFolderId);
            SqlParameter tparam2 = new SqlParameter("@nUserID", lUserId);
            List<ReportInfo> items = db.Database.SqlQuery<ReportInfo>("exec sproc_getReportsForFolder @nFolderId,@nUserID ", tparam1, tparam2).ToList();

          

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<ReportInfo>>(items, new JsonMediaTypeFormatter())
            };
        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetReportDetails(int nReportId)
        {
           // IQueryable<tblFilterCondition> itm = null;
            ReportDetails request =new ReportDetails();
            List<tblReport> tblreport= db.tblReports.Where(p => p.aReportID == nReportId).ToList();
            request.aReportId=nReportId;
            request.nFolderId = (int)tblreport[0].nReportFolderID;
            request.tReportName = tblreport[0].tName;
            request.tReportDescription = tblreport[0].tReportDescription;
            // itm = db.tblFilterConditions.Where(p => p.nRelatedID == nReportId && p.nRelatedType ==1 ).ToList();
            request.conditions = db.tblFilterConditions.Where(p => p.nRelatedID == nReportId && p.nRelatedType == 1).ToList(); 
            request.spClmn = db.tblDisplayColumns.Where(p => p.nRelatedID == nReportId && p.nRelatedType == 1).ToList();
            request.srtClmn = db.tblSortColumns.Where(p => p.nRelatedID == nReportId && p.nRelatedType == 1).ToList();
            //SqlParameter tparam1 = new SqlParameter("@nReportId", nReportId);
            //List<ReportConditions> items = db.Database.SqlQuery<ReportConditions>("exec sproc_getReportDetails  @nReportId", tparam1).ToList();



            //var items = new ReportInfo()
            //{
            //    aReportId = 1,
            //    nFolderId = 1,
            //    tReportName = "ABCD Report",
            //    dCreatedOn = DateTime.Now.AddDays(-111),
            //    tCreatedBy = "ASDASd"
            //};

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ReportDetails>(request, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetFieldOperatorType(int nBrandID)
        {
            //var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //Nullable<int> lUserId = securityContext.nUserID;
            //SqlParameter tparam1 = new SqlParameter("@nBrandId", nBrandId);
            //SqlParameter tparam2 = new SqlParameter("@nUserID", lUserId);
            List<ReportFieldAndOperatorType> items = db.Database.SqlQuery<ReportFieldAndOperatorType>("exec sproc_getFieldOperatorType  ").ToList();


            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<ReportFieldAndOperatorType>>(items, new JsonMediaTypeFormatter())
            };
        }
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetReportFields(int nBrandID)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            Nullable<int> lUserId = securityContext.nUserID;
            SqlParameter tparam1 = new SqlParameter("@nBrandID", nBrandID);
            SqlParameter tparam2 = new SqlParameter("@nUserID", lUserId);
            List<ReportFields> items = db.Database.SqlQuery<ReportFields>("exec sproc_getFields @nBrandID,@nUserID ", tparam1, tparam2).ToList();


            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<ReportFields>>(items, new JsonMediaTypeFormatter())
            };
        }


        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> EditFolder(ReportFolder request)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
               
                Nullable<int> lUserId = securityContext.nUserID;
                tblReportFolder tblReportFldr = request.GetTblReportFolder();
                
                if (request.aFolderId > 0)
                {
                    tblReportFldr.nUpdateBy = lUserId;
                    tblReportFldr.dtUpdatedOn = DateTime.Now;
                    db.Entry(tblReportFldr).State = EntityState.Unchanged;
                    db.Entry(tblReportFldr).Property(x => x.nUpdateBy).IsModified = true;
                    db.Entry(tblReportFldr).Property(x => x.dtUpdatedOn).IsModified = true;
                    db.Entry(tblReportFldr).Property(x => x.nFolderType).IsModified = true;
                    db.Entry(tblReportFldr).Property(x => x.tFolderName).IsModified = true;
                    db.Entry(tblReportFldr).Property(x => x.tFolderDescription).IsModified = true;

                }
                else
                {
                    tblReportFldr.nCreatedBy = lUserId;
                    tblReportFldr.dtCreatedOn= DateTime.Now;
                    tblReportFldr.aReportFolderID = 0;
                    db.tblReportFolders.Add(tblReportFldr);
                }

                await db.SaveChangesAsync();
               // int aFolderId = tblReportFldr.aReportFolderID;
                request.aFolderId = tblReportFldr.aReportFolderID;
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("Success", new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ReportGenerator.CreateFolder", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage DeleteFolder(ReportFolder request)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                string strReturn = "";
                if (request.aFolderId > 0)
                {
                    var nTempVendor = db.Database.SqlQuery<int>("select count(*) from tblReport with (nolock) where nReportFolderID=@nReportFolderID  ", new SqlParameter("@nReportFolderID", request.aFolderId)).ToList();
                    if (nTempVendor.Count > 0)
                        strReturn = "Can not delete this folder, it contains one or more reports. Please delete or move the reports and then delete this folder.";
                    else
                    {
                        var nFilterCndn = db.Database.ExecuteSqlCommand("delete from tblReportFolder where aReportFolderID =@aFolderId  ", new SqlParameter("@aFolderId", request.aFolderId));
                    }
                }
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>(strReturn, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ReportGenerator.DeleteFolder", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [Authorize]
        [HttpPost]
        public HttpResponseMessage MoveReport(int aReportID, int aFolderId)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {

                var nTempVendor = db.Database.SqlQuery<int>("update tblreport set nReportFolderID=@nReportFolderID  where aReportID=@aReportID ", new SqlParameter("@nReportFolderID", aFolderId), new SqlParameter("@aReportID", aReportID)).ToList();


                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("SUCCESS", new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ReportGenerator.MoveReport", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [Authorize]
        [HttpPost]
        public HttpResponseMessage DeleteReport(ReportInfo request)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                if (request.aReportId > 0)
                {
                    var nFilterCndn = db.Database.ExecuteSqlCommand("delete from tblFilterCondition where nRelatedID =@nReportID and nRelatedType=1 ", new SqlParameter("@nReportID", request.aReportId));
                    var nDsplClmn = db.Database.ExecuteSqlCommand("delete from tblDisplayColumns where nRelatedID =@nReportID and nRelatedType=1 ", new SqlParameter("@nReportID", request.aReportId));
                    var nSrtClmn = db.Database.ExecuteSqlCommand("delete from tblSortColumns where nRelatedID =@nReportID and nRelatedType=1 ", new SqlParameter("@nReportID", request.aReportId));
                    var nreport = db.Database.ExecuteSqlCommand("delete from tblreport where aReportID=@nReportID  ", new SqlParameter("@nReportID", request.aReportId));

                }
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("", new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ReportGenerator.CreateFolder", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetShareDetails(int nReportId)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {

                ReportShareModel response = new ReportShareModel()
                {
                    reportIds = new List<int>() { nReportId },
                    brands = new List<int>() { 1, 2 },
                    roles = new List<int>() { 5, 4, 2 }
                };
                //await db.SaveChangesAsync();
                //}
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<ReportShareModel>(response, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ReportGenerator.CreateFolder", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage ShareReport(ReportShareModel request)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {

                Nullable<int> lUserId = securityContext.nUserID;
                string strBrnd = "";
                foreach (var itrt in request.brands)
                {
                    if (itrt != 0)
                        strBrnd = "," + itrt.ToString() + ",";
                }
                foreach (var itrt in request.reportIds)
                {
                    foreach (var itr in request.roles)
                    {
                        var nUpdateReport = db.Database.ExecuteSqlCommand("exec sproc_shareReportsByRole @nReportID,@nRoleID  ", new SqlParameter("@nReportID", itrt), new SqlParameter("@nRoleID", itr));
                    }
                    if(strBrnd!="")
                     db.Database.ExecuteSqlCommand("update tblReport set tBrandID=@tBrandID where aReportID= @nReportID  ", new SqlParameter("@tBrandID", strBrnd), new SqlParameter("@nReportID", itrt));

                }
                //await db.SaveChangesAsync();
                //}
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("Success", new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ReportGenerator.CreateFolder", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> EditReport(ReportDetails request)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                Nullable<int> lUserId = securityContext.nUserID;
                tblReport tblreport = request.GetTblReport();
                if (request.nBrandId != null && request.nBrandId != 0)
                    tblreport.tBrandID = "," + request.nBrandId.ToString() + ",";
                if (request.aReportId > 0)
                {
                    var nFilterCndn = db.Database.ExecuteSqlCommand("delete from tblFilterCondition where nRelatedID =@nReportID and nRelatedType=1 ", new SqlParameter("@nReportID", request.aReportId));
                    var nDsplClmn = db.Database.ExecuteSqlCommand("delete from tblDisplayColumns where nRelatedID =@nReportID and nRelatedType=1 ", new SqlParameter("@nReportID", request.aReportId));
                    var nSrtClmn = db.Database.ExecuteSqlCommand("delete from tblSortColumns where nRelatedID =@nReportID and nRelatedType=1 ", new SqlParameter("@nReportID", request.aReportId));


                    tblreport.nUpdateBy = lUserId;
                    tblreport.dtUpdatedOn = DateTime.Now;
                    db.Entry(tblreport).State = EntityState.Unchanged;
                    db.Entry(tblreport).Property(x => x.nUpdateBy).IsModified = true;
                    db.Entry(tblreport).Property(x => x.dtUpdatedOn).IsModified = true;
                    db.Entry(tblreport).Property(x => x.tName).IsModified = true;
                    db.Entry(tblreport).Property(x => x.tReportDescription).IsModified = true;
                    db.Entry(tblreport).Property(x => x.nReportFolderID).IsModified = true;
                    db.Entry(tblreport).Property(x => x.tBrandID).IsModified = true;
                }
                else
                {
                  
                    tblreport.nCreatedBy = lUserId;
                    tblreport.dtCreatedOn = DateTime.Now;
                    tblreport.aReportID = 0;
                    db.tblReports.Add(tblreport);
                }

                await db.SaveChangesAsync();
                int aReportID = tblreport.aReportID;
                request.aReportId = tblreport.aReportID;
                if (request.conditions != null)
                    foreach (var itrt in request.conditions)
                    {
                        tblFilterCondition tblFiltercndn = new tblFilterCondition();
                        tblFiltercndn.nRelatedID = aReportID;
                        tblFiltercndn.nRelatedType = 1;
                        tblFiltercndn.nFieldID = itrt.nFieldID;
                        tblFiltercndn.nFieldTypeID = itrt.nFieldTypeID;
                        tblFiltercndn.nAndOr = itrt.nAndOr;
                        tblFiltercndn.nOperatorID = itrt.nOperatorID;
                        tblFiltercndn.nValue = itrt.nValue;
                        tblFiltercndn.tValue = itrt.tValue;
                        tblFiltercndn.dValue = itrt.dValue;
                        tblFiltercndn.cValue = itrt.cValue;
                        db.tblFilterConditions.Add(tblFiltercndn);
                    }
                if (request.spClmn != null)
                    foreach (var itrt in request.spClmn)
                    {
                        tblDisplayColumn tblDspClmn = new tblDisplayColumn();
                        tblDspClmn.nRelatedID = aReportID;
                        tblDspClmn.nRelatedType = 1;
                        tblDspClmn.nFieldID = itrt.nFieldID;
                        tblDspClmn.nOrder = itrt.nOrder;
                        db.tblDisplayColumns.Add(tblDspClmn);
                    }
                if (request.srtClmn != null)
                    foreach (var itrt in request.srtClmn)
                    {
                        tblSortColumn tblsrtClmn = new tblSortColumn();
                        tblsrtClmn.nRelatedID = aReportID;
                        tblsrtClmn.nRelatedType = 1;
                        tblsrtClmn.nFieldID = itrt.nFieldID;
                        tblsrtClmn.nOrder = itrt.nOrder;
                        db.tblSortColumns.Add(tblsrtClmn);
                    }
                await db.SaveChangesAsync();

                var nupdateReport = db.Database.ExecuteSqlCommand("exec sproc_generateSqlQuery @nRelatedID =@nReportID, @nRelatedType=1 ", new SqlParameter("@nReportID", request.aReportId));

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("Success", new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ReportGenerator.CreateFolder", HttpContext.Current, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
