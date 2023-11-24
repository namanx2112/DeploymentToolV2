using DeploymentTool.Misc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;

namespace DeploymentTool.Model
{
    public class ProjectFieldProcessor
    {
        private dtDBEntities db = new dtDBEntities();
        public ProjectFieldProcessor() { }

        public Dictionary<string, ProjectExcelFieldsHPRollout> ImportFields(string strFilePath, out string strStoreNumbers)
        {
            strStoreNumbers = string.Empty;
            //  
            Dictionary<string, ProjectExcelFieldsHPRollout> fields = new Dictionary<string, ProjectExcelFieldsHPRollout>();
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                ProjectExcelFieldsHPRollout objProjectExcel;
                DataTable dt = new DataTable();
                try
                {
                    DataTable dtNew = new DataTable();
                    ProjectType nProjectType;
                    // oledbConn.Open();
                    using (var stream = File.Open(strFilePath, FileMode.Open, FileAccess.Read))
                    {
                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<string> lstStoreNumber = new List<string>();

                            // 1. Use the reader methods
                            do
                            {
                                reader.Read();
                                int ColumnCount = reader.FieldCount;
                                for (int i = 0; i < ColumnCount; i++)
                                {
                                    string ColumnName = reader.GetValue(i).ToString();
                                    if (!dtNew.Columns.Contains(ColumnName))
                                    { dtNew.Columns.Add(ColumnName); }
                                }
                                while (reader.Read())
                                {
                                    if (reader.FieldCount == 0)
                                        break;
                                    // reader.GetDouble(0);
                                    string storeNumber = reader.GetValue(dtNew.Columns.IndexOf("Store Number")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Store Number")).ToString() : "";
                                    string projectType = reader.GetValue(dtNew.Columns.IndexOf("Project Type")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Type")).ToString() : "";
                                    if (projectType != "" && storeNumber != "" && Enum.TryParse(projectType.Replace(" ", ""), true, out nProjectType))
                                    {
                                        lstStoreNumber.Add(storeNumber);
                                        objProjectExcel = new ProjectExcelFieldsHPRollout();
                                        objProjectExcel.tProjectType = projectType;
                                        objProjectExcel.tStoreNumber = storeNumber;
                                        objProjectExcel.tAddress = reader.GetValue(dtNew.Columns.IndexOf("Address")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Address")).ToString() : "";
                                        objProjectExcel.tCity = reader.GetValue(dtNew.Columns.IndexOf("City")) != null ? reader.GetValue(dtNew.Columns.IndexOf("City")).ToString() : "";
                                        objProjectExcel.tState = reader.GetValue(dtNew.Columns.IndexOf("State")) != null ? reader.GetValue(dtNew.Columns.IndexOf("State")).ToString() : "";
                                        objProjectExcel.nDMAID = reader.GetValue(dtNew.Columns.IndexOf("DMA ID")) != null && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DMA ID"))) : 0;
                                        objProjectExcel.tDMA = reader.GetValue(dtNew.Columns.IndexOf("DMA")) != null ? reader.GetValue(dtNew.Columns.IndexOf("DMA")).ToString() : "";
                                        objProjectExcel.tRED = reader.GetValue(dtNew.Columns.IndexOf("RED")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RED")).ToString() : "";
                                        objProjectExcel.tCM = reader.GetValue(dtNew.Columns.IndexOf("CM")) != null ? reader.GetValue(dtNew.Columns.IndexOf("CM")).ToString() : "";
                                        objProjectExcel.tANE = reader.GetValue(dtNew.Columns.IndexOf("A&E")) != null ? reader.GetValue(dtNew.Columns.IndexOf("A&E")).ToString() : "";
                                        objProjectExcel.tRVP = reader.GetValue(dtNew.Columns.IndexOf("RVP")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RVP")).ToString() : "";
                                        objProjectExcel.tPrincipalPartner = reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")).ToString() : "";
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Status")).ToString() != "")
                                            objProjectExcel.dStatus = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Status")));
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Open Store")) != null && reader.GetValue(dtNew.Columns.IndexOf("Open Store")).ToString() != "")
                                            objProjectExcel.dOpenStore = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Open Store")));//default value

                                        objProjectExcel.tProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Project Status")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Status")).ToString() : "";


                                        objProjectExcel.tNetworkSwitchVendor = reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor"))) : "";
                                        objProjectExcel.tNetworkSwitchStatus = reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status"))) : "";
                                        objProjectExcel.tShipmenttoVendor = reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor"))) : "";
                                        objProjectExcel.tSetupStatus = reader.GetValue(dtNew.Columns.IndexOf("Setup Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Setup Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Setup Status"))) : "";
                                        objProjectExcel.tNewSerialNumber = reader.GetValue(dtNew.Columns.IndexOf("New Serial Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("New Serial Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("New Serial Number"))) : "";
                                        objProjectExcel.tOldSerialNumber = reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number"))) : "";
                                        objProjectExcel.tOldSwitchReturnStatus = reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status"))) : "";
                                        objProjectExcel.tOldSwitchTracking = reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking"))) : "";

                                        /////////////
                                        objProjectExcel.tImageMemoryVendor = reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor"))) : "";
                                        objProjectExcel.tImageMemoryStatus = reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status"))) : "";
                                        objProjectExcel.tShipmentTracking = reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking"))) : "";
                                        objProjectExcel.tReturnShipment = reader.GetValue(dtNew.Columns.IndexOf("Return Shipment")) != null && reader.GetValue(dtNew.Columns.IndexOf("Return Shipment")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Return Shipment"))) : "";
                                        objProjectExcel.tReturnShipmentTracking = reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking"))) : "";



                                        objProjectExcel.tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                                        objProjectExcel.tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                        objProjectExcel.dInstallDate = reader.ConvertMeToDateTime(dtNew, "Install Date");

                                        objProjectExcel.tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                                        objProjectExcel.tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                                        objProjectExcel.tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                                        objProjectExcel.tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                                        objProjectExcel.tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                                        objProjectExcel.tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                                        objProjectExcel.tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                                            objProjectExcel.dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                                        objProjectExcel.tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                                        objProjectExcel.tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                                        objProjectExcel.tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                        if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                                            objProjectExcel.dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                                        objProjectExcel.tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                                        objProjectExcel.tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                                        objProjectExcel.tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";

                                        if (!fields.ContainsKey(objProjectExcel.tStoreNumber))
                                            fields.Add(objProjectExcel.tStoreNumber, objProjectExcel);
                                        else
                                            fields[objProjectExcel.tStoreNumber] = objProjectExcel;
                                    }
                                    else
                                        break;
                                }
                            } while (reader.NextResult());
                            strStoreNumbers = string.Join(",", lstStoreNumber.ToArray());
                            // 2. Use the AsDataSet extension method
                            //  var result = reader.AsDataSet();

                            // The result of each spreadsheet is in result.Tables
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
            return fields;
        }

        public Dictionary<string, ProjectExcelFieldsHPRollout> ImportExceltoDatabaseHPRollout(string strFilePath, out string strStoreNumbers)
        {
            strStoreNumbers = string.Empty;
            //  
            Dictionary<string, ProjectExcelFieldsHPRollout> fields = new Dictionary<string, ProjectExcelFieldsHPRollout>();
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                ProjectExcelFieldsHPRollout objProjectExcel;
                DataTable dt = new DataTable();
                try
                {
                    DataTable dtNew = new DataTable();
                    ProjectType nProjectType;
                    // oledbConn.Open();
                    using (var stream = File.Open(strFilePath, FileMode.Open, FileAccess.Read))
                    {
                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<string> lstStoreNumber = new List<string>();

                            // 1. Use the reader methods
                            do
                            {
                                reader.Read();
                                int ColumnCount = reader.FieldCount;
                                for (int i = 0; i < ColumnCount; i++)
                                {
                                    string ColumnName = reader.GetValue(i).ToString();
                                    if (!dtNew.Columns.Contains(ColumnName))
                                    { dtNew.Columns.Add(ColumnName); }
                                }
                                while (reader.Read())
                                {
                                    if (reader.FieldCount == 0)
                                        break;
                                    // reader.GetDouble(0);
                                    string storeNumber = reader.GetValue(dtNew.Columns.IndexOf("Store Number")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Store Number")).ToString() : "";
                                    string projectType = reader.GetValue(dtNew.Columns.IndexOf("Project Type")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Type")).ToString() : "";
                                    if (projectType != "" && storeNumber != "" && Enum.TryParse(projectType.Replace(" ", ""), true, out nProjectType))
                                    {
                                        lstStoreNumber.Add(storeNumber);
                                        objProjectExcel = new ProjectExcelFieldsHPRollout();
                                        objProjectExcel.tProjectType = projectType;
                                        objProjectExcel.tStoreNumber = storeNumber;
                                        objProjectExcel.tAddress = reader.GetValue(dtNew.Columns.IndexOf("Address")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Address")).ToString() : "";
                                        objProjectExcel.tCity = reader.GetValue(dtNew.Columns.IndexOf("City")) != null ? reader.GetValue(dtNew.Columns.IndexOf("City")).ToString() : "";
                                        objProjectExcel.tState = reader.GetValue(dtNew.Columns.IndexOf("State")) != null ? reader.GetValue(dtNew.Columns.IndexOf("State")).ToString() : "";
                                        objProjectExcel.nDMAID = reader.GetValue(dtNew.Columns.IndexOf("DMA ID")) != null && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DMA ID"))) : 0;
                                        objProjectExcel.tDMA = reader.GetValue(dtNew.Columns.IndexOf("DMA")) != null ? reader.GetValue(dtNew.Columns.IndexOf("DMA")).ToString() : "";
                                        objProjectExcel.tRED = reader.GetValue(dtNew.Columns.IndexOf("RED")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RED")).ToString() : "";
                                        objProjectExcel.tCM = reader.GetValue(dtNew.Columns.IndexOf("CM")) != null ? reader.GetValue(dtNew.Columns.IndexOf("CM")).ToString() : "";
                                        objProjectExcel.tANE = reader.GetValue(dtNew.Columns.IndexOf("A&E")) != null ? reader.GetValue(dtNew.Columns.IndexOf("A&E")).ToString() : "";
                                        objProjectExcel.tRVP = reader.GetValue(dtNew.Columns.IndexOf("RVP")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RVP")).ToString() : "";
                                        objProjectExcel.tPrincipalPartner = reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")).ToString() : "";
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Status")).ToString() != "")
                                            objProjectExcel.dStatus = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Status")));
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Open Store")) != null && reader.GetValue(dtNew.Columns.IndexOf("Open Store")).ToString() != "")
                                            objProjectExcel.dOpenStore = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Open Store")));//default value

                                        objProjectExcel.tProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Project Status")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Status")).ToString() : "";


                                        objProjectExcel.tNetworkSwitchVendor = reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor"))) : "";
                                        objProjectExcel.tNetworkSwitchStatus = reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status"))) : "";
                                        objProjectExcel.tShipmenttoVendor = reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor"))) : "";
                                        objProjectExcel.tSetupStatus = reader.GetValue(dtNew.Columns.IndexOf("Setup Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Setup Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Setup Status"))) : "";
                                        objProjectExcel.tNewSerialNumber = reader.GetValue(dtNew.Columns.IndexOf("New Serial Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("New Serial Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("New Serial Number"))) : "";
                                        objProjectExcel.tOldSerialNumber = reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number"))) : "";
                                        objProjectExcel.tOldSwitchReturnStatus = reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status"))) : "";
                                        objProjectExcel.tOldSwitchTracking = reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking"))) : "";

                                        /////////////
                                        objProjectExcel.tImageMemoryVendor = reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor"))) : "";
                                        objProjectExcel.tImageMemoryStatus = reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status"))) : "";
                                        objProjectExcel.tShipmentTracking = reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking"))) : "";
                                        objProjectExcel.tReturnShipment = reader.GetValue(dtNew.Columns.IndexOf("Return Shipment")) != null && reader.GetValue(dtNew.Columns.IndexOf("Return Shipment")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Return Shipment"))) : "";
                                        objProjectExcel.tReturnShipmentTracking = reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking"))) : "";



                                        objProjectExcel.tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                                        objProjectExcel.tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                        objProjectExcel.dInstallDate = reader.ConvertMeToDateTime(dtNew, "Install Date");

                                        objProjectExcel.tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                                        objProjectExcel.tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                                        objProjectExcel.tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                                        objProjectExcel.tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                                        objProjectExcel.tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                                        objProjectExcel.tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                                        objProjectExcel.tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                                            objProjectExcel.dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                                        objProjectExcel.tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                                        objProjectExcel.tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                                        objProjectExcel.tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                        if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                                            objProjectExcel.dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                                        objProjectExcel.tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                                        objProjectExcel.tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                                        objProjectExcel.tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";

                                        if (!fields.ContainsKey(objProjectExcel.tStoreNumber))
                                            fields.Add(objProjectExcel.tStoreNumber, objProjectExcel);
                                        else
                                            fields[objProjectExcel.tStoreNumber] = objProjectExcel;
                                    }
                                    else
                                        break;
                                }
                            } while (reader.NextResult());
                            strStoreNumbers = string.Join(",", lstStoreNumber.ToArray());
                            // 2. Use the AsDataSet extension method
                            //  var result = reader.AsDataSet();

                            // The result of each spreadsheet is in result.Tables
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
            return fields;
        }

        public Dictionary<string, ProjectExcelFieldsHPRollout> ImportExceltoDatabaseServerHandheld(string strFilePath, out string strStoreNumbers)
        {
            //  
            strStoreNumbers = string.Empty;
            //  
            Dictionary<string, ProjectExcelFieldsHPRollout> fields = new Dictionary<string, ProjectExcelFieldsHPRollout>();
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                ProjectExcelFieldsServerHandheld objProjectExcel = new ProjectExcelFieldsServerHandheld();
                DataTable dt = new DataTable();
                try
                {
                    DataTable dtNew = new DataTable();
                    // oledbConn.Open();
                    using (var stream = File.Open(strFilePath, FileMode.Open, FileAccess.Read))
                    {
                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Choose one of either 1 or 2:
                            List<string> lstStoreNumber = new List<string>();
                            // 1. Use the reader methods
                            do
                            {
                                reader.Read();
                                int ColumnCount = reader.FieldCount;
                                for (int i = 0; i < ColumnCount; i++)
                                {
                                    string ColumnName = reader.GetValue(i).ToString();
                                    if (!dtNew.Columns.Contains(ColumnName))
                                    { dtNew.Columns.Add(ColumnName); }
                                }
                                while (reader.Read())
                                {

                                    // reader.GetDouble(0);
                                    string storeNumber = reader.GetValue(dtNew.Columns.IndexOf("Store Number")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Store Number")).ToString() : "";
                                    string projectType = reader.GetValue(dtNew.Columns.IndexOf("Project Type")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Type")).ToString() : "";

                                    if (projectType != "" && storeNumber != "")
                                    {
                                        objProjectExcel.tProjectType = projectType;
                                        objProjectExcel.tStoreNumber = storeNumber;
                                        objProjectExcel.tAddress = reader.GetValue(dtNew.Columns.IndexOf("Address")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Address")).ToString() : "";
                                        objProjectExcel.tCity = reader.GetValue(dtNew.Columns.IndexOf("City")) != null ? reader.GetValue(dtNew.Columns.IndexOf("City")).ToString() : "";
                                        objProjectExcel.tState = reader.GetValue(dtNew.Columns.IndexOf("State")) != null ? reader.GetValue(dtNew.Columns.IndexOf("State")).ToString() : "";
                                        objProjectExcel.nDMAID = reader.GetValue(dtNew.Columns.IndexOf("DMA ID")) != null && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DMA ID"))) : 0;
                                        objProjectExcel.tDMA = reader.GetValue(dtNew.Columns.IndexOf("DMA")) != null ? reader.GetValue(dtNew.Columns.IndexOf("DMA")).ToString() : "";
                                        objProjectExcel.tRED = reader.GetValue(dtNew.Columns.IndexOf("RED")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RED")).ToString() : "";
                                        objProjectExcel.tCM = reader.GetValue(dtNew.Columns.IndexOf("CM")) != null ? reader.GetValue(dtNew.Columns.IndexOf("CM")).ToString() : "";
                                        objProjectExcel.tANE = reader.GetValue(dtNew.Columns.IndexOf("A&E")) != null ? reader.GetValue(dtNew.Columns.IndexOf("A&E")).ToString() : "";
                                        objProjectExcel.tRVP = reader.GetValue(dtNew.Columns.IndexOf("RVP")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RVP")).ToString() : "";
                                        objProjectExcel.tPrincipalPartner = reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")).ToString() : "";
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Status")).ToString() != "")
                                            objProjectExcel.dStatus = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Status")));
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Open Store")) != null && reader.GetValue(dtNew.Columns.IndexOf("Open Store")).ToString() != "")
                                            objProjectExcel.dOpenStore = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Open Store")));//default value

                                        objProjectExcel.tProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Project Status")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Status")).ToString() : "";


                                        objProjectExcel.tServerHandheldVendor = reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Vendor"))) : "";
                                        objProjectExcel.tServerHandheldStatus = reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Status"))) : "";

                                        if (reader.GetValue(dtNew.Columns.IndexOf("Ship Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Ship Date")).ToString() != "")
                                            objProjectExcel.dShipDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Ship Date")));

                                        objProjectExcel.tShippingCarrier = reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier"))) : "";
                                        objProjectExcel.tTrackingNumber = reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Tracking Number"))) : "";

                                        objProjectExcel.nTablets = reader.GetValue(dtNew.Columns.IndexOf("Tablets")) != null && reader.GetValue(dtNew.Columns.IndexOf("Tablets")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Tablets"))) : 0;
                                        objProjectExcel.nFiveBayharger = reader.GetValue(dtNew.Columns.IndexOf("5 Bay Charger")) != null && reader.GetValue(dtNew.Columns.IndexOf("5 Bay Charger")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("5 Bay Charger"))) : 0;
                                        objProjectExcel.nShoulderStrap = reader.GetValue(dtNew.Columns.IndexOf("Shoulder Strap")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shoulder Strap")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Shoulder Strap"))) : 0;
                                        objProjectExcel.nProtectiveCase = reader.GetValue(dtNew.Columns.IndexOf("Protective Case")) != null && reader.GetValue(dtNew.Columns.IndexOf("Protective Case")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Protective Case"))) : 0;

                                        if (reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")).ToString() != "")
                                            objProjectExcel.dDeliveryDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")));

                                        objProjectExcel.tServerHandheldCost = reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Cost"))) : "";



                                        objProjectExcel.tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                                        objProjectExcel.tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                        if (reader.GetValue(dtNew.Columns.IndexOf("Install Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Date")).ToString() != "")
                                            objProjectExcel.dInstallDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install Date")));

                                        objProjectExcel.tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                                        objProjectExcel.tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                                        objProjectExcel.tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                                        objProjectExcel.tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                                        objProjectExcel.tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                                        objProjectExcel.tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                                        objProjectExcel.tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                                        if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                                            objProjectExcel.dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                                        objProjectExcel.tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                                        objProjectExcel.tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                                        objProjectExcel.tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                                        if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                                            objProjectExcel.dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                                        objProjectExcel.tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                                        objProjectExcel.tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                                        objProjectExcel.tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";


                                        fields.Add(objProjectExcel);
                                    }
                                }
                            }
                            } while (reader.NextResult()) ;

                        // 2. Use the AsDataSet extension method
                        //  var result = reader.AsDataSet();

                        // The result of each spreadsheet is in result.Tables
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
            return fields;
        }

public List<ProjectExcelFieldsHPRollout> UpdateStoreExistFields(Dictionary<string, ProjectExcelFieldsHPRollout> itemsByStoreNumber, int nProjectType, string storeNumbers, int nBrandId, int nRolloutId)
{
    SqlParameter tModuleNameParam1 = new SqlParameter("@nBrandId", nBrandId);
    SqlParameter tModuleNameParam2 = new SqlParameter("@tStoreNumber", storeNumbers);
    SqlParameter tModuleNameParam3 = new SqlParameter("@nRolloutId", nRolloutId);
    SqlParameter tModuleNameParam4 = new SqlParameter("@nProjectType", nProjectType);
    var output = db.Database.SqlQuery<StoreExistModel>("exec sproc_StoreExistCheckForRollout @nBrandId, @tStoreNumber, @nRolloutId, @nProjectType",
        tModuleNameParam1, tModuleNameParam2, tModuleNameParam3, tModuleNameParam4).ToList();

    foreach (var tItem in output)
    {
        if (itemsByStoreNumber.ContainsKey(tItem.StoreNumber))
        {
            itemsByStoreNumber[tItem.StoreNumber].nStoreExistStatus = tItem.ExistFlag;
            itemsByStoreNumber[tItem.StoreNumber].nStoreId = tItem.StoreId;
            itemsByStoreNumber[tItem.StoreNumber].nProjectId = tItem.ProjectId;
        }
    }

    return itemsByStoreNumber.Values.ToList();
}
    }

    public class StoreExistModel
{
    public string StoreNumber { get; set; }
    public Nullable<int> StoreId { get; set; }
    public Nullable<int> ProjectId { get; set; }
    public int ExistFlag { get; set; }
}
}