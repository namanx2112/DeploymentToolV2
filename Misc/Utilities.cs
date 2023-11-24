using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Net.Mail;
using DeploymentTool.Model.Templates;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.DynamicData;
using System.Xml.Linq;
using System.Collections;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Data.Entity;
using System.Security.Cryptography;
using ExcelDataReader;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentTool.Misc
{
    public class Utilities
    {
        public static void SetHousekeepingFields(bool create, HttpContext context, IModelParent objRef)
        {
            try
            {
                var securityContext = (User)context.Items["SecurityContext"];
                Nullable<long> lUserId = securityContext.nUserID;
                PropertyInfo prop;
                if (create)
                {
                    prop = objRef.GetType().GetProperty("nCreatedBy", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, (int)lUserId, null);
                    }

                    prop = objRef.GetType().GetProperty("dtCreatedOn", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, (DateTime)DateTime.Now, null);
                    }
                }
                else
                {
                    prop = objRef.GetType().GetProperty("nUpdateBy", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, (int)lUserId, null);
                    }

                    prop = objRef.GetType().GetProperty("dtUpdatedOn", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, (DateTime)DateTime.Now, null);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void SetActiveProjectId(ProjectType pType, int? nStoreId, IModelParent objRef)
        {
            try
            {
                dtDBEntities db = new dtDBEntities();
                IQueryable<tblProject> query;
                int nProjectId = 0;
                switch (pType)
                {
                    case ProjectType.AudioInstallation:
                    case ProjectType.POSInstallation:
                    case ProjectType.InteriorMenuInstallation:
                    case ProjectType.ExteriorMenuInstallation:
                    case ProjectType.PaymentTerminalInstallation:
                    case ProjectType.ServerHandheld:
                        nProjectId = db.Database.SqlQuery<int>($"select nProjectId from dbo.fn_GetProjectIdForThisTechOrAnyProjectType({nStoreId},{(int)pType},1)").FirstOrDefault();
                        //query = db.tblProjects.Where(x => x.nStoreID == nStoreId && x.nProjectActiveStatus == 1 && (x.nProjectType == (int)pType || x.aProjectID > 0));
                        break;
                    default:
                        nProjectId = db.Database.SqlQuery<int>($"select nProjectId from dbo.fn_GetProjectIdForThisTechOrAnyProjectType({nStoreId},{(int)pType},1)").FirstOrDefault();
                        //query = db.tblProjects.Where(x => x.nStoreID == nStoreId && x.nProjectActiveStatus == 1);
                        break;
                }
                //tblProject tProject = query.FirstOrDefault();
                if (nProjectId > 0)
                {

                    PropertyInfo prop;
                    prop = objRef.GetType().GetProperty("nProjectID", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, nProjectId, null);
                    }
                    prop = objRef.GetType().GetProperty("nMyActiveStatus", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, (int)1, null);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void AddToAudit(int? nStoreId, int? nProjectID, int nDataType, string tTable, string tField, string tChangeValue, string tChangeNote, int? lUserId, int nCreateOrUpdate)
        {
            try
            {
                int? nBrandID = 0;//
                dtDBEntities db = new dtDBEntities();
                List<SqlParameter> tp1 = new List<SqlParameter>();
                //tp1.Add(new SqlParameter("@nBrandID", nBrandID));
                tp1.Add(new SqlParameter("@nStoreID", nStoreId));
                tp1.Add(new SqlParameter("@nProjectID", nProjectID));
                tp1.Add(new SqlParameter("@nDataType", nDataType));
                tp1.Add(new SqlParameter("@tTable", tTable));
                tp1.Add(new SqlParameter("@tField", tField));
                tp1.Add(new SqlParameter("@tChangeValue", tChangeValue));
                tp1.Add(new SqlParameter("@tChangeNote", tChangeNote));
                tp1.Add(new SqlParameter("@nCreatedBy", lUserId));
                tp1.Add(new SqlParameter("@nCreateOrUpdate", nCreateOrUpdate));
                //db.Database.ExecuteSqlCommand("exec sproc_AddAudit @nBrandID,@nStoreID,@nProjectID,@nDataType,@tTable,@tField,@tChangeValue,@tChangeNote,@nCreatedBy ,@nCreateOrUpdate", tp1[0], tp1[1], tp1[2], tp1[3], tp1[4], tp1[5], tp1[6], tp1[7], tp1[8], tp1[9]);
                db.Database.ExecuteSqlCommand("exec sproc_AddAudit @nStoreID,@nProjectID,@nDataType,@tTable,@tField,@tChangeValue,@tChangeNote,@nCreatedBy ,@nCreateOrUpdate", tp1[0], tp1[1], tp1[2], tp1[3], tp1[4], tp1[5], tp1[6], tp1[7], tp1[8]);

            }
            catch (Exception ex)
            {

            }
        }
        public static String WriteHTMLToPDF(String strBody, String fileName)
        {
            string strFilePath = "";
            try
            {
                string URL = HttpRuntime.AppDomainAppPath + "Attachments\\" + Guid.NewGuid();

                bool exists = System.IO.Directory.Exists(URL);

                if (!exists)
                    System.IO.Directory.CreateDirectory(URL);
                strFilePath = URL + "\\" + fileName;

                StringReader sr = new StringReader(strBody);
                Document pdfDoc = new Document();// PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                // using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(strFilePath, FileMode.Create));
                    pdfDoc.Open();

                    htmlparser.Style.LoadTagStyle(HtmlTags.DIV, "size", "10px");
                    htmlparser.Style.LoadTagStyle(HtmlTags.HEADERCELL, HtmlTags.BACKGROUNDCOLOR, "gray");
                    htmlparser.Style.LoadTagStyle(HtmlTags.HEADERCELL, HtmlTags.COLOR, "#fff");
                    htmlparser.Style.LoadTagStyle(HtmlTags.TABLE, HtmlTags.WIDTH, "100%");
                    htmlparser.Style.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDERWIDTH, "1");
                    htmlparser.Style.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDERCOLOR, "lightgray");
                    htmlparser.Style.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BACKGROUNDCOLOR, "#c2c2c2");
                    htmlparser.Parse(sr);
                    pdfDoc.Close();


                }


            }
            catch (Exception ex) { }
            return strFilePath;
        }


        public static string CreateNewStoresForOrderAccurcy(List<dynamic> requestArr, int? nBrandId, int aProjectsRolloutID)
        {

            string strReturn = "";


            try
            {
                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                dtDBEntities db = new dtDBEntities();
                foreach (var tItem in requestArr)
                {
                    ProjectExcelFieldsOrderAccurcy request = JsonConvert.DeserializeObject<ProjectExcelFieldsOrderAccurcy>(JsonConvert.SerializeObject(tItem));

                    try
                    {

                        List<SqlParameter> tPramList = new List<SqlParameter>();
                        tPramList.Add(new SqlParameter("@tStoreName", request.tProjectType));
                        tPramList.Add(new SqlParameter("@tProjectType", 12));//12->Order Accuracy Installation
                        tPramList.Add(new SqlParameter("@tStoreNumber", request.tStoreNumber));
                        tPramList.Add(new SqlParameter("@tAddress", request.tAddress));
                        tPramList.Add(new SqlParameter("@tCity", request.tCity));
                        tPramList.Add(new SqlParameter("@tState", request.tState));
                        tPramList.Add(new SqlParameter("@nDMAID", request.nDMAID));
                        tPramList.Add(new SqlParameter("@tDMA", request.tDMA));
                        tPramList.Add(new SqlParameter("@tRED", request.tRED));
                        tPramList.Add(new SqlParameter("@tCM", request.tCM));
                        tPramList.Add(new SqlParameter("@tANE", request.tANE));
                        tPramList.Add(new SqlParameter("@tRVP", request.tRVP));
                        tPramList.Add(new SqlParameter("@tPrincipalPartner", request.tPrincipalPartner));
                        tPramList.Add(new SqlParameter("@dStatus", request.dStatus ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@dOpenStore", request.dOpenStore ?? (object)DBNull.Value));

                        tPramList.Add(new SqlParameter("@tProjectStatus", request.tProjectStatus));
                        tPramList.Add(new SqlParameter("@nCreatedBy", securityContext.nUserID));
                        tPramList.Add(new SqlParameter("@nBrandId", nBrandId));
                        //
                        tPramList.Add(new SqlParameter("@tOrderAccuracyVendor", request.tOrderAccuracyVendor));
                        tPramList.Add(new SqlParameter("@tOrderAccuracyStatus", request.tOrderAccuracyStatus));
                        tPramList.Add(new SqlParameter("@nBakeryPrinter", request.nBakeryPrinter));
                        tPramList.Add(new SqlParameter("@nDualCupLabel", request.nDualCupLabel));
                        tPramList.Add(new SqlParameter("@nDTExpo", request.nDTExpo));
                        tPramList.Add(new SqlParameter("@nFCExpo", request.nFCExpo));
                        tPramList.Add(new SqlParameter("@dShipDate", request.dShipDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tShippingCarrier", request.tShippingCarrier));
                        tPramList.Add(new SqlParameter("@tTrackingNumber", request.tTrackingNumber));
                        tPramList.Add(new SqlParameter("@dDeliveryDate", request.dDeliveryDate ?? (object)DBNull.Value));
                        //
                        tPramList.Add(new SqlParameter("@tInstallationVendor", request.tInstallationVendor));
                        tPramList.Add(new SqlParameter("@tInstallStatus", request.tInstallStatus));
                        tPramList.Add(new SqlParameter("@dInstallDate", request.dInstallDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tInstallTime", request.tInstallTime));
                        tPramList.Add(new SqlParameter("@tInstallTechNumber", request.tInstallTechNumber));
                        tPramList.Add(new SqlParameter("@tManagerName", request.tManagerName));
                        tPramList.Add(new SqlParameter("@tManagerNumber", request.tManagerNumber));
                        tPramList.Add(new SqlParameter("@tManagerCheckout", request.tManagerCheckout));
                        tPramList.Add(new SqlParameter("@tPhotoDeliverables", request.tPhotoDeliverables));
                        tPramList.Add(new SqlParameter("@tLeadTech", request.tLeadTech));
                        tPramList.Add(new SqlParameter("@dInstallEnd", request.dInstallEnd ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tSignoffs", request.tSignoffs));
                        tPramList.Add(new SqlParameter("@tTestTransactions", request.tTestTransactions));
                        tPramList.Add(new SqlParameter("@tInstallProjectStatus", request.tInstallProjectStatus));
                        tPramList.Add(new SqlParameter("@dRevisitDate", request.dRevisitDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tCost", request.tCost));
                        tPramList.Add(new SqlParameter("@tInstallNotes", request.tInstallNotes));
                        tPramList.Add(new SqlParameter("@tInstallType", request.tInstallType));
                        tPramList.Add(new SqlParameter("@nProjectsRolloutID", aProjectsRolloutID));
                        tPramList.Add(new SqlParameter("@nProjectId", request.nProjectId));
                        tPramList.Add(new SqlParameter("@nStoreId", request.nStoreId));
                        tPramList.Add(new SqlParameter("@nExistFlag", request.nStoreExistStatus));
                        db.Database.ExecuteSqlCommand("exec sproc_CreateStoreFromExcelForOrderAccuracy @nProjectId,@nStoreId,@nExistFlag, @tStoreName,@tProjectType," +
                            "@tStoreNumber,@tAddress,@tCity,@tState,@nDMAID,@tDMA,@tRED,@tCM," +
                            "@tANE,@tRVP,@tPrincipalPartner,@dStatus,@dOpenStore,@tProjectStatus,@nCreatedBy,@nBrandId," +
                            "@tOrderAccuracyVendor,@tOrderAccuracyStatus,@nBakeryPrinter,@nDualCupLabel,@nDTExpo,@nFCExpo,@dShipDate,@tShippingCarrier,@tTrackingNumber,@dDeliveryDate," +
                            "@tInstallationVendor,@tInstallStatus,@dInstallDate,@tInstallTime,@tInstallTechNumber,@tManagerName,@tManagerNumber," +
                            "@tManagerCheckout,@tPhotoDeliverables,@tLeadTech,@dInstallEnd,@tSignoffs,@tTestTransactions,@tInstallProjectStatus," +
                            "@dRevisitDate,@tCost,@tInstallNotes,@tInstallType,@nProjectsRolloutID  "
                            , tPramList[47], tPramList[48], tPramList[49], tPramList[0], tPramList[1], tPramList[2], tPramList[3], tPramList[4], tPramList[5],
                            tPramList[6], tPramList[7], tPramList[8], tPramList[9], tPramList[10], tPramList[11], tPramList[12], tPramList[13],
                            tPramList[14], tPramList[15], tPramList[16], tPramList[17], tPramList[18], tPramList[19], tPramList[20], tPramList[21],
                            tPramList[22], tPramList[23], tPramList[24], tPramList[25], tPramList[26], tPramList[27], tPramList[28], tPramList[29],
                            tPramList[30], tPramList[31], tPramList[32], tPramList[33], tPramList[34], tPramList[35], tPramList[36], tPramList[37],
                            tPramList[38], tPramList[39], tPramList[40], tPramList[41], tPramList[42], tPramList[43], tPramList[44], tPramList[45], tPramList[46]).ToString();


                    }
                    catch (Exception)
                    {
                        strReturn += request.tStoreNumber + ",";
                    }

                }

            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("OrderAccuracy.CreateNewStores", HttpContext.Current, ex);
                return "failed to import, please contact administrator";
            }
            return strReturn;

        }

        public static string CreateStoreFromExcelForOrderStatusBoard(List<dynamic> requestArr, int? nBrandId, int aProjectsRolloutID)
        {


            string strReturn = "";


            try
            {
                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                dtDBEntities db = new dtDBEntities();
                foreach (var tItem in requestArr)
                {
                    ProjectExcelFieldsOrderStatusBoard request = JsonConvert.DeserializeObject<ProjectExcelFieldsOrderStatusBoard>(JsonConvert.SerializeObject(tItem));

                    try
                    {

                        List<SqlParameter> tPramList = new List<SqlParameter>();
                        tPramList.Add(new SqlParameter("@tStoreName", request.tProjectType));
                        tPramList.Add(new SqlParameter("@tProjectType", 13));//13->Order Status Board Installation
                        tPramList.Add(new SqlParameter("@tStoreNumber", request.tStoreNumber));
                        tPramList.Add(new SqlParameter("@tAddress", request.tAddress));
                        tPramList.Add(new SqlParameter("@tCity", request.tCity));
                        tPramList.Add(new SqlParameter("@tState", request.tState));
                        tPramList.Add(new SqlParameter("@nDMAID", request.nDMAID));
                        tPramList.Add(new SqlParameter("@tDMA", request.tDMA));
                        tPramList.Add(new SqlParameter("@tRED", request.tRED));
                        tPramList.Add(new SqlParameter("@tCM", request.tCM));
                        tPramList.Add(new SqlParameter("@tANE", request.tANE));
                        tPramList.Add(new SqlParameter("@tRVP", request.tRVP));
                        tPramList.Add(new SqlParameter("@tPrincipalPartner", request.tPrincipalPartner));
                        tPramList.Add(new SqlParameter("@dStatus", request.dStatus ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@dOpenStore", request.dOpenStore ?? (object)DBNull.Value));

                        tPramList.Add(new SqlParameter("@tProjectStatus", request.tProjectStatus));
                        tPramList.Add(new SqlParameter("@nCreatedBy", securityContext.nUserID));
                        tPramList.Add(new SqlParameter("@nBrandId", nBrandId));
                        //
                        //
                        tPramList.Add(new SqlParameter("@tOrderStatusBoardVendor", request.tOrderStatusBoardVendor));
                        tPramList.Add(new SqlParameter("@tOrderStatusBoardStatus", request.tOrderStatusBoardStatus));
                        tPramList.Add(new SqlParameter("@nOSB", request.nOSB));
                        tPramList.Add(new SqlParameter("@dShipDate", request.dShipDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tShippingCarrier", request.tShippingCarrier));
                        tPramList.Add(new SqlParameter("@tTrackingNumber", request.tTrackingNumber));
                        tPramList.Add(new SqlParameter("@dDeliveryDate", request.dDeliveryDate ?? (object)DBNull.Value));
                        //
                        tPramList.Add(new SqlParameter("@tInstallationVendor", request.tInstallationVendor));
                        tPramList.Add(new SqlParameter("@tInstallStatus", request.tInstallStatus));
                        tPramList.Add(new SqlParameter("@dInstallDate", request.dInstallDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tInstallTime", request.tInstallTime));
                        tPramList.Add(new SqlParameter("@tInstallTechNumber", request.tInstallTechNumber));
                        tPramList.Add(new SqlParameter("@tManagerName", request.tManagerName));
                        tPramList.Add(new SqlParameter("@tManagerNumber", request.tManagerNumber));
                        tPramList.Add(new SqlParameter("@tManagerCheckout", request.tManagerCheckout));
                        tPramList.Add(new SqlParameter("@tPhotoDeliverables", request.tPhotoDeliverables));
                        tPramList.Add(new SqlParameter("@tLeadTech", request.tLeadTech));
                        tPramList.Add(new SqlParameter("@dInstallEnd", request.dInstallEnd ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tSignoffs", request.tSignoffs));
                        tPramList.Add(new SqlParameter("@tTestTransactions", request.tTestTransactions));
                        tPramList.Add(new SqlParameter("@tInstallProjectStatus", request.tInstallProjectStatus));
                        tPramList.Add(new SqlParameter("@dRevisitDate", request.dRevisitDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tCost", request.tCost));
                        tPramList.Add(new SqlParameter("@tInstallNotes", request.tInstallNotes));
                        tPramList.Add(new SqlParameter("@tInstallType", request.tInstallType));
                        tPramList.Add(new SqlParameter("@nProjectsRolloutID", aProjectsRolloutID));
                        tPramList.Add(new SqlParameter("@nProjectId", request.nProjectId));
                        tPramList.Add(new SqlParameter("@nStoreId", request.nStoreId));
                        tPramList.Add(new SqlParameter("@nExistFlag", request.nStoreExistStatus));
                        db.Database.ExecuteSqlCommand("exec sproc_CreateStoreFromExcelForOrderStatusBoard @nProjectId,@nStoreId,@nExistFlag, @tStoreName,@tProjectType," +
                            "@tStoreNumber,@tAddress,@tCity,@tState,@nDMAID,@tDMA,@tRED,@tCM," +
                            "@tANE,@tRVP,@tPrincipalPartner,@dStatus,@dOpenStore,@tProjectStatus,@nCreatedBy,@nBrandId," +
                            "@tOrderStatusBoardVendor,@tOrderStatusBoardStatus,@nOSB,@dShipDate,@tShippingCarrier,@tTrackingNumber,@dDeliveryDate," +
                            "@tInstallationVendor,@tInstallStatus,@dInstallDate,@tInstallTime,@tInstallTechNumber,@tManagerName,@tManagerNumber," +
                            "@tManagerCheckout,@tPhotoDeliverables,@tLeadTech,@dInstallEnd,@tSignoffs,@tTestTransactions,@tInstallProjectStatus," +
                            "@dRevisitDate,@tCost,@tInstallNotes,@tInstallType,@nProjectsRolloutID  "
                            , tPramList[44], tPramList[45], tPramList[46],tPramList[0], tPramList[1], tPramList[2], tPramList[3], tPramList[4], tPramList[5],
                            tPramList[6], tPramList[7], tPramList[8], tPramList[9], tPramList[10], tPramList[11], tPramList[12], tPramList[13],
                            tPramList[14], tPramList[15], tPramList[16], tPramList[17], tPramList[18], tPramList[19], tPramList[20], tPramList[21],
                            tPramList[22], tPramList[23], tPramList[24], tPramList[25], tPramList[26], tPramList[27], tPramList[28], tPramList[29],
                            tPramList[30], tPramList[31], tPramList[32], tPramList[33], tPramList[34], tPramList[35], tPramList[36], tPramList[37],
                            tPramList[38], tPramList[39], tPramList[40], tPramList[41], tPramList[42], tPramList[43]).ToString();

                    }
                    catch (Exception)
                    {
                        strReturn += request.tStoreNumber + ",";
                    }

                }

            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("OrderStatusBoard.CreateNewStores", HttpContext.Current, ex);
                return "failed to import, please contact administrator";
            }
            return strReturn;

        }
        public static string CreateStoreFromExcelForServerHandheld(List<dynamic> requestArr, int? nBrandId, int aProjectsRolloutID)
        {
            string strReturn = "";


            try
            {
                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                dtDBEntities db = new dtDBEntities();
                foreach (var tItem in requestArr)
                {
                    ProjectExcelFieldsServerHandheld request = JsonConvert.DeserializeObject<ProjectExcelFieldsServerHandheld>(JsonConvert.SerializeObject(tItem));

                    try
                    {

                        List<SqlParameter> tPramList = new List<SqlParameter>();
                        tPramList.Add(new SqlParameter("@tStoreName", request.tProjectType));
                        tPramList.Add(new SqlParameter("@tProjectType", 10));//10->Server Handheld
                        tPramList.Add(new SqlParameter("@tStoreNumber", request.tStoreNumber));
                        tPramList.Add(new SqlParameter("@tAddress", request.tAddress));
                        tPramList.Add(new SqlParameter("@tCity", request.tCity));
                        tPramList.Add(new SqlParameter("@tState", request.tState));
                        tPramList.Add(new SqlParameter("@nDMAID", request.nDMAID));
                        tPramList.Add(new SqlParameter("@tDMA", request.tDMA));
                        tPramList.Add(new SqlParameter("@tRED", request.tRED));
                        tPramList.Add(new SqlParameter("@tCM", request.tCM));
                        tPramList.Add(new SqlParameter("@tANE", request.tANE));
                        tPramList.Add(new SqlParameter("@tRVP", request.tRVP));
                        tPramList.Add(new SqlParameter("@tPrincipalPartner", request.tPrincipalPartner));
                        tPramList.Add(new SqlParameter("@dStatus", request.dStatus ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@dOpenStore", request.dOpenStore ?? (object)DBNull.Value));

                        tPramList.Add(new SqlParameter("@tProjectStatus", request.tProjectStatus));
                        tPramList.Add(new SqlParameter("@nCreatedBy", securityContext.nUserID));
                        tPramList.Add(new SqlParameter("@nBrandId", nBrandId));
                        //
                        //
                        tPramList.Add(new SqlParameter("@tServerHandheldVendor", request.tServerHandheldVendor));
                        tPramList.Add(new SqlParameter("@tServerHandheldStatus", request.tServerHandheldStatus ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@dShipDate", request.dShipDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tShippingCarrier", request.tShippingCarrier));
                        tPramList.Add(new SqlParameter("@tTrackingNumber", request.tTrackingNumber));
                        tPramList.Add(new SqlParameter("@nTablets", request.nTablets));
                        tPramList.Add(new SqlParameter("@nFiveBayharger", request.nFiveBayharger));

                        tPramList.Add(new SqlParameter("@nShoulderStrap", request.nShoulderStrap));
                        tPramList.Add(new SqlParameter("@nProtectiveCase", request.nProtectiveCase));
                        tPramList.Add(new SqlParameter("@dDeliveryDate", request.dDeliveryDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tServerHandheldCost", request.tServerHandheldCost ?? (object)DBNull.Value));
                        //
                        tPramList.Add(new SqlParameter("@tInstallationVendor", request.tInstallationVendor));
                        tPramList.Add(new SqlParameter("@tInstallStatus", request.tInstallStatus));
                        tPramList.Add(new SqlParameter("@dInstallDate", request.dInstallDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tInstallTime", request.tInstallTime));
                        tPramList.Add(new SqlParameter("@tInstallTechNumber", request.tInstallTechNumber));
                        tPramList.Add(new SqlParameter("@tManagerName", request.tManagerName));
                        tPramList.Add(new SqlParameter("@tManagerNumber", request.tManagerNumber));
                        tPramList.Add(new SqlParameter("@tManagerCheckout", request.tManagerCheckout));
                        tPramList.Add(new SqlParameter("@tPhotoDeliverables", request.tPhotoDeliverables));
                        tPramList.Add(new SqlParameter("@tLeadTech", request.tLeadTech));
                        tPramList.Add(new SqlParameter("@dInstallEnd", request.dInstallEnd ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tSignoffs", request.tSignoffs));
                        tPramList.Add(new SqlParameter("@tTestTransactions", request.tTestTransactions));
                        tPramList.Add(new SqlParameter("@tInstallProjectStatus", request.tInstallProjectStatus));
                        tPramList.Add(new SqlParameter("@dRevisitDate", request.dRevisitDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tCost", request.tCost ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tInstallNotes", request.tInstallNotes));
                        tPramList.Add(new SqlParameter("@tInstallType", request.tInstallType));
                        tPramList.Add(new SqlParameter("@nProjectsRolloutID", aProjectsRolloutID));
                        tPramList.Add(new SqlParameter("@nProjectId", request.nProjectId));
                        tPramList.Add(new SqlParameter("@nStoreId", request.nStoreId));
                        tPramList.Add(new SqlParameter("@nExistFlag", request.nStoreExistStatus));
                        db.Database.ExecuteSqlCommand(" exec sproc_CreateStoreFromExcelForServerHandheld @nProjectId,@nStoreId,@nExistFlag, @tStoreName,@tProjectType," +
                            "@tStoreNumber,@tAddress,@tCity,@tState,@nDMAID,@tDMA,@tRED,@tCM," +
                            "@tANE,@tRVP,@tPrincipalPartner,@dStatus,@dOpenStore,@tProjectStatus,@nCreatedBy,@nBrandId," +
                            "@tServerHandheldVendor,@tServerHandheldStatus,@dShipDate,@tShippingCarrier,@tTrackingNumber,@nTablets,@nFiveBayharger," +
                            "@nShoulderStrap, @nProtectiveCase, @dDeliveryDate, @tServerHandheldCost, " +
                            "@tInstallationVendor,@tInstallStatus,@dInstallDate,@tInstallTime,@tInstallTechNumber,@tManagerName,@tManagerNumber," +
                            "@tManagerCheckout,@tPhotoDeliverables,@tLeadTech,@dInstallEnd,@tSignoffs,@tTestTransactions,@tInstallProjectStatus," +
                            "@dRevisitDate,@tCost,@tInstallNotes,@tInstallType,@nProjectsRolloutID "
                            , tPramList[48], tPramList[49], tPramList[50], tPramList[0], tPramList[1], tPramList[2], tPramList[3], tPramList[4], tPramList[5],
                            tPramList[6], tPramList[7], tPramList[8], tPramList[9], tPramList[10], tPramList[11], tPramList[12], tPramList[13],
                            tPramList[14], tPramList[15], tPramList[16], tPramList[17], tPramList[18], tPramList[19], tPramList[20], tPramList[21],
                            tPramList[22], tPramList[23], tPramList[24], tPramList[25], tPramList[26], tPramList[27], tPramList[28], tPramList[29],
                            tPramList[30], tPramList[31], tPramList[32], tPramList[33], tPramList[34], tPramList[35], tPramList[36], tPramList[37],
                            tPramList[38], tPramList[39], tPramList[40], tPramList[41], tPramList[42],
                            tPramList[43], tPramList[44], tPramList[45], tPramList[46], tPramList[47]).ToString();

                    }
                    catch (Exception)
                    {
                        strReturn += request.tStoreNumber + ",";
                    }

                }

            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ServerHandheld.CreateNewStores", HttpContext.Current, ex);
                return "failed to import, please contact administrator";
            }
            return strReturn;
        }

        public static string CreateStoreFromExcelForHPRollout(List<dynamic> requestArr, int? nBrandId, int aProjectsRolloutID)
        {

            string strReturn = "";


            try
            {
                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                dtDBEntities db = new dtDBEntities();
                foreach (var tItem in requestArr)
                {
                    ProjectExcelFieldsHPRollout request = JsonConvert.DeserializeObject<ProjectExcelFieldsHPRollout>(JsonConvert.SerializeObject(tItem));

                    try
                    {


                        List<SqlParameter> tPramList = new List<SqlParameter>();
                        tPramList.Add(new SqlParameter("@tStoreName", request.nStoreExistStatus));
                        tPramList.Add(new SqlParameter("@tProjectType", 14));//14->Arbys HP Rollout Installation
                        tPramList.Add(new SqlParameter("@tStoreNumber", request.tStoreNumber));
                        tPramList.Add(new SqlParameter("@tAddress", request.tAddress));
                        tPramList.Add(new SqlParameter("@tCity", request.tCity));
                        tPramList.Add(new SqlParameter("@tState", request.tState));
                        tPramList.Add(new SqlParameter("@nDMAID", request.nDMAID));
                        tPramList.Add(new SqlParameter("@tDMA", request.tDMA));
                        tPramList.Add(new SqlParameter("@tRED", request.tRED));
                        tPramList.Add(new SqlParameter("@tCM", request.tCM));
                        tPramList.Add(new SqlParameter("@tANE", request.tANE));
                        tPramList.Add(new SqlParameter("@tRVP", request.tRVP));
                        tPramList.Add(new SqlParameter("@tPrincipalPartner", request.tPrincipalPartner));
                        tPramList.Add(new SqlParameter("@dStatus", request.dStatus ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@dOpenStore", request.dOpenStore ?? (object)DBNull.Value));

                        tPramList.Add(new SqlParameter("@tProjectStatus", request.tProjectStatus));
                        tPramList.Add(new SqlParameter("@nCreatedBy", securityContext.nUserID));
                        tPramList.Add(new SqlParameter("@nBrandId", nBrandId));
                        //
                        //
                        tPramList.Add(new SqlParameter("@tNetworkSwitchVendor", request.tNetworkSwitchVendor));
                        tPramList.Add(new SqlParameter("@tNetworkSwitchStatus", request.tNetworkSwitchStatus));
                        tPramList.Add(new SqlParameter("@tShipmenttoVendor", request.tShipmenttoVendor));
                        tPramList.Add(new SqlParameter("@tSetupStatus", request.tSetupStatus));
                        tPramList.Add(new SqlParameter("@tNewSerialNumber", request.tNewSerialNumber));
                        tPramList.Add(new SqlParameter("@tOldSerialNumber", request.tOldSerialNumber));
                        tPramList.Add(new SqlParameter("@tOldSwitchReturnStatus", request.tOldSwitchReturnStatus));
                        tPramList.Add(new SqlParameter("@tOldSwitchTracking", request.tOldSwitchTracking));
                        //
                        tPramList.Add(new SqlParameter("@tImageMemoryVendor", request.tImageMemoryVendor));
                        tPramList.Add(new SqlParameter("@tImageMemoryStatus", request.tImageMemoryStatus));
                        tPramList.Add(new SqlParameter("@tShipmentTracking", request.tShipmentTracking));
                        tPramList.Add(new SqlParameter("@tReturnShipment", request.tReturnShipment));
                        tPramList.Add(new SqlParameter("@tReturnShipmentTracking", request.tReturnShipmentTracking));
                        //
                        tPramList.Add(new SqlParameter("@tInstallationVendor", request.tInstallationVendor));
                        tPramList.Add(new SqlParameter("@tInstallStatus", request.tInstallStatus));
                        tPramList.Add(new SqlParameter("@dInstallDate", request.dInstallDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tInstallTime", request.tInstallTime));
                        tPramList.Add(new SqlParameter("@tInstallTechNumber", request.tInstallTechNumber));
                        tPramList.Add(new SqlParameter("@tManagerName", request.tManagerName));
                        tPramList.Add(new SqlParameter("@tManagerNumber", request.tManagerNumber));
                        tPramList.Add(new SqlParameter("@tManagerCheckout", request.tManagerCheckout));
                        tPramList.Add(new SqlParameter("@tPhotoDeliverables", request.tPhotoDeliverables));
                        tPramList.Add(new SqlParameter("@tLeadTech", request.tLeadTech));
                        tPramList.Add(new SqlParameter("@dInstallEnd", request.dInstallEnd ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tSignoffs", request.tSignoffs));
                        tPramList.Add(new SqlParameter("@tTestTransactions", request.tTestTransactions));
                        tPramList.Add(new SqlParameter("@tInstallProjectStatus", request.tInstallProjectStatus));
                        tPramList.Add(new SqlParameter("@dRevisitDate", request.dRevisitDate ?? (object)DBNull.Value));
                        tPramList.Add(new SqlParameter("@tCost", request.tCost));
                        tPramList.Add(new SqlParameter("@tInstallNotes", request.tInstallNotes));
                        tPramList.Add(new SqlParameter("@tInstallType", request.tInstallType));
                        tPramList.Add(new SqlParameter("@nProjectsRolloutID", aProjectsRolloutID));
                        tPramList.Add(new SqlParameter("@nProjectId", request.nProjectId));
                        tPramList.Add(new SqlParameter("@nStoreId", request.nStoreId));
                        tPramList.Add(new SqlParameter("@nExistFlag", request.nStoreExistStatus));
                        db.Database.ExecuteSqlCommand(" exec sproc_CreateStoreFromExcelForHPRollout @nProjectId,@nStoreId,@nExistFlag, @tStoreName,@tProjectType," +
                            "@tStoreNumber,@tAddress,@tCity,@tState,@nDMAID,@tDMA,@tRED,@tCM," +
                            "@tANE,@tRVP,@tPrincipalPartner,@dStatus,@dOpenStore,@tProjectStatus,@nCreatedBy,@nBrandId," +
                            "@tNetworkSwitchVendor,@tNetworkSwitchStatus,@tShipmenttoVendor,@tSetupStatus,@tNewSerialNumber,@tOldSerialNumber,@tOldSwitchReturnStatus,@tOldSwitchTracking," +
                             "@tImageMemoryVendor,@tImageMemoryStatus,@tShipmentTracking,@tReturnShipment,@tReturnShipmentTracking," +
                             "@tInstallationVendor,@tInstallStatus,@dInstallDate,@tInstallTime,@tInstallTechNumber,@tManagerName,@tManagerNumber," +
                            "@tManagerCheckout,@tPhotoDeliverables,@tLeadTech,@dInstallEnd,@tSignoffs,@tTestTransactions,@tInstallProjectStatus," +
                            "@dRevisitDate,@tCost,@tInstallNotes,@tInstallType,@nProjectsRolloutID "
                            , tPramList[50], tPramList[51], tPramList[52], tPramList[0], tPramList[1], tPramList[2], tPramList[3], tPramList[4], tPramList[5],
                            tPramList[6], tPramList[7], tPramList[8], tPramList[9], tPramList[10], tPramList[11], tPramList[12], tPramList[13],
                            tPramList[14], tPramList[15], tPramList[16], tPramList[17], tPramList[18], tPramList[19], tPramList[20], tPramList[21],
                            tPramList[22], tPramList[23], tPramList[24], tPramList[25], tPramList[26], tPramList[27], tPramList[28], tPramList[29],
                            tPramList[30], tPramList[31], tPramList[32], tPramList[33], tPramList[34], tPramList[35], tPramList[36], tPramList[37],
                            tPramList[38], tPramList[39], tPramList[40], tPramList[41], tPramList[42],
                            tPramList[43], tPramList[44], tPramList[45], tPramList[46], tPramList[47], tPramList[48], tPramList[49]).ToString();

                    }
                    catch (Exception)
                    {
                        strReturn += request.tStoreNumber + ",";
                    }

                }

            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("HPRollout.CreateNewStores", HttpContext.Current, ex);
                return "failed to import, please contact administrator";
            }
            return strReturn;
        }
        public static void SendMail(EMailRequest request)
        {

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            using (SmtpClient smtpClient = new SmtpClient())
            {

                if (request.tTo != null && request.tTo.Length > 0)
                {
                    foreach (string toAddress in request.tTo.Split(';'))
                        if (!String.IsNullOrEmpty(toAddress))
                            mailMessage.To.Add(toAddress.Trim());
                }
                if (request.tCC != null && request.tCC.Length > 0)
                {
                    foreach (string toAddress in request.tCC.Split(';'))
                        if (!String.IsNullOrEmpty(toAddress))
                            mailMessage.CC.Add(toAddress.Trim());
                }
                mailMessage.Subject = request.tSubject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = request.tContent;

                //if(request.FileAttachments!=null)
                //foreach (var RequestFile in request.FileAttachments)
                //{
                //    System.Net.Mail.Attachment objAtt = new System.Net.Mail.Attachment(RequestFile.tFileName);
                //    objAtt.ContentId = RequestFile.tContentID;
                //    mailMessage.Attachments.Add(objAtt);
                //}
                if (!string.IsNullOrEmpty(request.tFilePath))
                {
                    System.Net.Mail.Attachment objAtt = new System.Net.Mail.Attachment(request.tFilePath);
                    //objAtt.ContentId = RequestFile.tContentID;
                    mailMessage.Attachments.Add(objAtt);
                }
                // Send the email
                //SendMail();
                smtpClient.Send(mailMessage);



            }


        }

        public static void SendPasswordToEmail(string tName, string tUserName, string tEmail, string sPassword, bool forReset)
        {
            string tContent = "<div>Dear " + tName + ",<br/></div>";
            EMailRequest MailObj = new EMailRequest();
            if (forReset)
            {
                tContent += "<div>We have reset your password for accessing the Inspire Brands's Restaurant Technology Deployment tool!.<br/></div>";
                tContent += "<div>Please find the below credentials to Login:<br/></div>";
                tContent += $"<div>URL: {System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri} <br/></div>";
                tContent += "<div>User Name:" + tUserName + " <br/></div>";
                tContent += "<div>Password:  " + sPassword + " <br/><br/></div>";
                tContent += "<div>Thanks & Regards<br/></div>";
                tContent += "<div>Restaurant Technology Deployment Team</div>";
                MailObj.tSubject = "Password Reset:Inspire Brands";
            }
            else
            {
                tContent += "<div>We have created your user account for accessing the Inspire Brands's Restaurant Technology Deployment tool!.<br/></div>";
                tContent += "<div>Please find the below credentials to Login:<br/></div>";
                tContent += $"<div>URL: {System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri} <br/></div>";
                tContent += "<div>User Name:" + tUserName + " <br/></div>";
                tContent += "<div>Password:  " + sPassword + " <br/><br/></div>";
                tContent += "<div>Thanks & Regards<br/></div>";
                tContent += "<div>Restaurant Technology Deployment Team</div>";
                MailObj.tSubject = "Welcome to Inspire Brands";
            }

            MailObj.tTo = tEmail;
            MailObj.tContent = tContent;
            DeploymentTool.Misc.Utilities.SendMail(MailObj);
        }
        
        public static string EncodeString(string str)
        {
            string sResp = string.Empty;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
            sResp = System.Convert.ToBase64String(plainTextBytes);
            return sResp;
        }

        public static string DecodeString(string str)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        internal static string CreatePassword(string sUserName, int length, out string sPassword)
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string number = "1234567890";
            const string special = "!@#$%^&*";

            var middle = length / 2;
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                if (middle == length)
                {
                    res.Append(number[rnd.Next(number.Length)]);
                }
                else if (middle - 1 == length)
                {
                    res.Append(special[rnd.Next(special.Length)]);
                }
                else
                {
                    if (length % 2 == 0)
                    {
                        res.Append(lower[rnd.Next(lower.Length)]);
                    }
                    else
                    {
                        res.Append(upper[rnd.Next(upper.Length)]);
                    }
                }
            }
            sPassword = res.ToString();
            string sEncoded = Hashing.GenerateHash(sPassword);
            return sEncoded.ToString();
        }

        public static string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }

        public static List<IImportModel> ConvertExcelReaderToImportableModel(string strFilePath, IImportModel tModel, int instanceId)
        {
            //  
            List<IImportModel> items = new List<IImportModel>();
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                try
                {
                    using (var stream = File.Open(strFilePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            do
                            {
                                reader.Read();
                                while (reader.Read())
                                {
                                    items.Add(tModel.GetFromExcel(reader, instanceId));
                                }
                            } while (reader.NextResult());
                        }
                    }

                }
                catch (Exception ex)
                {
                    TraceUtility.ForceWriteException("ImportExceltoDatabase", HttpContext.Current, ex);
                    //result = false;
                }
                finally
                {
                    //oledbConn.Close();
                }
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ImportExceltoDatabase2", HttpContext.Current, ex);
            }
            // return ip;
            // return result;
            return items;
        }
    }
    public class Hashing
    {
        static string salt = "deplution";
        public static string GenerateHash(string password)
        {
            string _finalHash = string.Empty;
            try
            {
                byte[] keyByte = new ASCIIEncoding().GetBytes(salt);
                byte[] messageBytes = new ASCIIEncoding().GetBytes(password);
                byte[] hashmessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
                _finalHash = String.Concat(Array.ConvertAll(hashmessage, x => x.ToString("x2")));
            }
            catch (Exception ex)
            {
                _finalHash = string.Empty;
            }
            return _finalHash;
        }
    }

    public static class ExtensionMethod
    {
        public static DateTime? ConvertMeToDateTime(this IExcelDataReader reader, DataTable dtable, string columnName)
        {
            DateTime? dt = null;
            if (reader != null && dtable.Columns.IndexOf(columnName) > -1 && reader.GetValue(dtable.Columns.IndexOf(columnName)) != null && reader.GetValue(dtable.Columns.IndexOf(columnName)).ToString() != "")
            {
                DateTime newDT;
                if (DateTime.TryParse(reader.GetValue(dtable.Columns.IndexOf(columnName)).ToString(), out newDT))
                    dt = newDT;
            }
            return dt;
        }
    }

}