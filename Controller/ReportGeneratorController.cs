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

            //List<ReportFolder> items = new List<ReportFolder>()
            //{
            //    new ReportFolder()
            //    {
            //        aFolderId = 1,
            //        dCreatedOn = DateTime.Now,
            //        tCreatedBy = "Admin",
            //        tFolderName = "Home",
            //        tFolderDescription = "Folder for Home Page"
            //    },
            //    new ReportFolder()
            //    {
            //        aFolderId = 2,
            //        dCreatedOn = DateTime.Now.AddDays(-112),
            //        tCreatedBy = "Admin",
            //        tFolderName = "Vendor",
            //        tFolderDescription = "Folder for Vendors"
            //    },
            //    new ReportFolder()
            //    {
            //        aFolderId = 3,
            //        dCreatedOn = DateTime.Now.AddDays(-111),
            //        tCreatedBy = "Admin",
            //        tFolderName = "Franchise",
            //        tFolderDescription = "Folder for Franchise"
            //    },
            //    new ReportFolder()
            //    {
            //        aFolderId = 4,
            //        dCreatedOn = DateTime.Now.AddDays(-12),
            //        tCreatedBy = "Admin",
            //        tFolderName = "Admin",
            //        tFolderDescription = "Folder for Admins"
            //    },
            //    new ReportFolder()
            //    {
            //        aFolderId = 5,
            //        dCreatedOn = DateTime.Now.AddDays(-102),
            //        tCreatedBy = "Admin",
            //        tFolderName = "Project Managers",
            //        tFolderDescription = "Folder for Project Managers"
            //    },
            //    new ReportFolder()
            //    {
            //        aFolderId = 6,
            //        dCreatedOn = DateTime.Now.AddDays(-212),
            //        tCreatedBy = "Admin",
            //        tFolderName = "Sonic",
            //        tFolderDescription = "Folder for Sonic"
            //    }
            //};

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

            //List<ReportInfo> items = new List<ReportInfo>()
            //{
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "ABCD Report",
            //       dCreatedOn = DateTime.Now.AddDays(-111),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "XYZ Report",
            //       dCreatedOn = DateTime.Now.AddDays(-20),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "DASCD Report",
            //       dCreatedOn = DateTime.Now.AddDays(-1211),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "afwetv Report",
            //       dCreatedOn = DateTime.Now.AddDays(-1111),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "ggsdfv  sd Report",
            //       dCreatedOn = DateTime.Now.AddDays(-11),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "dsfv  sdfsdf Report",
            //       dCreatedOn = DateTime.Now.AddDays(-1),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "dfsdfsd Report",
            //       dCreatedOn = DateTime.Now.AddDays(-10),
            //       tCreatedBy = "ASDASd"
            //    },

            //};

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<ReportInfo>>(items, new JsonMediaTypeFormatter())
            };
        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetReportDetails(int nReportId)
        {

            //List<ReportInfo> items = new List<ReportInfo>()
            //{
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "ABCD Report",
            //       dCreatedOn = DateTime.Now.AddDays(-111),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "XYZ Report",
            //       dCreatedOn = DateTime.Now.AddDays(-20),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "DASCD Report",
            //       dCreatedOn = DateTime.Now.AddDays(-1211),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "afwetv Report",
            //       dCreatedOn = DateTime.Now.AddDays(-1111),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "ggsdfv  sd Report",
            //       dCreatedOn = DateTime.Now.AddDays(-11),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "dsfv  sdfsdf Report",
            //       dCreatedOn = DateTime.Now.AddDays(-1),
            //       tCreatedBy = "ASDASd"
            //    },
            //    new ReportInfo()
            //    {
            //       aReportId = 1,
            //       nFolderId = 1,
            //       tReportName = "dfsdfsd Report",
            //       dCreatedOn = DateTime.Now.AddDays(-10),
            //       tCreatedBy = "ASDASd"
            //    },

            //};

            var items = new ReportInfo()
            {
                aReportId = 1,
                nFolderId = 1,
                tReportName = "ABCD Report",
                dCreatedOn = DateTime.Now.AddDays(-111),
                tCreatedBy = "ASDASd"
            };

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ReportInfo>(items, new JsonMediaTypeFormatter())
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
                    db.Entry(tblReportFldr).State = EntityState.Modified;
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
                request.aFolderId = 99;
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
        public async Task<HttpResponseMessage> EditReport(ReportInfo request)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            try
            {
                Nullable<int> lUserId = securityContext.nUserID;
                tblReport tblreport = request.GetTblReport();
                //request.tBrandID != null && request.tBrandID != "" &&
                //        tblreport.tBrandID = "," + request.tBrandID + ",";
                if (request.aReportId > 0)
                {
                    var nFilterCndn = db.Database.ExecuteSqlCommand("delete from tblFilterCondition where nRelatedID =@nReportID and nRelatedType=1 ", new SqlParameter("@nReportID", request.aReportId));
                    var nDsplClmn = db.Database.ExecuteSqlCommand("delete from tblDisplayColumns where nRelatedID =@nReportID and nRelatedType=1 ", new SqlParameter("@nReportID", request.aReportId));
                    var nSrtClmn = db.Database.ExecuteSqlCommand("delete from tblSortColumns where nRelatedID =@nReportID and nRelatedType=1 ", new SqlParameter("@nReportID", request.aReportId));


                    tblreport.nUpdateBy = lUserId;
                    tblreport.dtUpdatedOn = DateTime.Now;
                    //db.Entry(tblreport).State = EntityState.Modified;
                    db.Entry(tblreport).Property(x => x.nUpdateBy).IsModified = true;
                    db.Entry(tblreport).Property(x => x.dtUpdatedOn).IsModified = true;
                    db.Entry(tblreport).Property(x => x.tName).IsModified = true;
                    //db.Entry(tblreport).Property(x => x.tDesc).IsModified = true;
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

                foreach (var itrt in request.conditions)
                {
                    tblFilterCondition tblFiltercndn = new tblFilterCondition();
                    tblFiltercndn.nRelatedID = aReportID;
                    tblFiltercndn.nRelatedType = 1;
                    tblFiltercndn.nFieldID = itrt.aFieldID;
                    tblFiltercndn.nFieldTypeID = itrt.nFieldTypeID;
                    tblFiltercndn.nAndOr = itrt.nAndOr;
                    tblFiltercndn.nOperatorID = itrt.nOperatorID;
                    tblFiltercndn.nValue = itrt.nValue;
                    tblFiltercndn.tValue = itrt.tValue;
                    tblFiltercndn.dValue = itrt.dValue;
                    tblFiltercndn.cValue = itrt.cValue;
                    db.tblFilterConditions.Add(tblFiltercndn);
                }
              
                foreach (var itrt in request.spClmn)
                {
                    tblDisplayColumn tblDspClmn = new tblDisplayColumn();
                    tblDspClmn.nRelatedID = aReportID;
                    tblDspClmn.nRelatedType = 1;
                    tblDspClmn.nFieldID = itrt.nFieldID;
                    tblDspClmn.nOrder = itrt.nOrder;
                    db.tblDisplayColumns.Add(tblDspClmn);
                }
                foreach (var itrt in request.srtClmn)
                {
                    tblSortColumn tblsrtClmn = new tblSortColumn();
                    tblsrtClmn.nRelatedID = aReportID;
                    tblsrtClmn.nRelatedType = 1;
                    tblsrtClmn.nFieldID = itrt.nFieldID;
                    tblsrtClmn.nOrder = itrt.nOrder;
                    db.tblSortColumns.Add(tblsrtClmn);
                }
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
