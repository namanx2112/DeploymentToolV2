using DeploymentTool.Misc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{

    public interface IProjectExcelFields
    {
        IProjectExcelFields getModelFromColumns(IExcelDataReader reader, DataTable dtNew);

        string getStoreNumber();

        void setValues(int nExist, int? nProjectId, int? nStoreId);
    }
    public class ProjectExcelFields : IProjectExcelFields
    {
        public int nBrandId { get; set; }
        private Nullable<int> _nProjectId;
        private Nullable<int> _nStoreId;
        public Nullable<int> nProjectId
        {
            get
            {
                return _nProjectId == null ? 0 : _nProjectId;
            }
            set
            {
                _nProjectId = value;
            }
        }
        public Nullable<int> nStoreId
        {
            get
            {
                return _nStoreId == null ? 0 : _nStoreId;
            }
            set
            {
                _nStoreId = value;
            }
        }
        public string tProjectType { get; set; }
        public string tStoreNumber { get; set; }
        public string tAddress { get; set; }
        public string tCity { get; set; }
        public string tState { get; set; }
        public int nDMAID { get; set; }
        public string tDMA { get; set; }
        public string tRED { get; set; }
        public string tCM { get; set; }
        public string tANE { get; set; }
        public string tRVP { get; set; }
        public string tPrincipalPartner { get; set; }
        public Nullable<DateTime> dStatus { get; set; }
        public Nullable<DateTime> dOpenStore { get; set; }
        public string tProjectStatus { get; set; }
        public int nStoreExistStatus { get; set; }

        public int nNumberOfTabletsPerStore { get; set; }
        public string tEquipmentVendor { get; set; }

        public Nullable<DateTime> dShipDate { get; set; }

        public Nullable<DateTime> dRevisitDate { get; set; }

        public Nullable<DateTime> dInstallDate { get; set; }

        public string tInstallationVendor { get; set; }

        public string tInstallStatus { get; set; }

        public IProjectExcelFields getModelFromColumns(IExcelDataReader reader, DataTable dtNew)
        {
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                DataTable dt = new DataTable();
                try
                {
                    ProjectType nProjectType;
                    string storeNumber = reader.GetValue(dtNew.Columns.IndexOf("Store Number")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Store Number")).ToString() : "";
                    string projectType = reader.GetValue(dtNew.Columns.IndexOf("Project Type")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Type")).ToString() : "";
                    if (projectType != "" && storeNumber != "" && Enum.TryParse(projectType.Replace(" ", ""), true, out nProjectType))
                    {
                        tProjectType = projectType;
                        tStoreNumber = storeNumber;
                        tProjectType = projectType;
                        tStoreNumber = storeNumber;
                        tAddress = dtNew.Columns.IndexOf("Address") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Address")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Address")).ToString() : "";
                        tCity = dtNew.Columns.IndexOf("City") > 0 && reader.GetValue(dtNew.Columns.IndexOf("City")) != null ? reader.GetValue(dtNew.Columns.IndexOf("City")).ToString() : "";
                        tState = dtNew.Columns.IndexOf("State") > 0 && reader.GetValue(dtNew.Columns.IndexOf("State")) != null ? reader.GetValue(dtNew.Columns.IndexOf("State")).ToString() : "";
                        nDMAID = dtNew.Columns.IndexOf("DMA ID") > 0 && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")) != null && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DMA ID"))) : 0;
                        tDMA = dtNew.Columns.IndexOf("DMA") > 0 && reader.GetValue(dtNew.Columns.IndexOf("DMA")) != null ? reader.GetValue(dtNew.Columns.IndexOf("DMA")).ToString() : "";
                        tRED = dtNew.Columns.IndexOf("RED") > 0 && reader.GetValue(dtNew.Columns.IndexOf("RED")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RED")).ToString() : "";
                        tCM = dtNew.Columns.IndexOf("CM") > 0 && reader.GetValue(dtNew.Columns.IndexOf("CM")) != null ? reader.GetValue(dtNew.Columns.IndexOf("CM")).ToString() : "";
                        tANE = dtNew.Columns.IndexOf("A&E") > 0 && dtNew.Columns.IndexOf("A&E") > 0 &&  reader.GetValue(dtNew.Columns.IndexOf("A&E")) != null ? reader.GetValue(dtNew.Columns.IndexOf("A&E")).ToString() : "";
                        tRVP = dtNew.Columns.IndexOf("RVP") > 0 && reader.GetValue(dtNew.Columns.IndexOf("RVP")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RVP")).ToString() : "";
                        tPrincipalPartner = dtNew.Columns.IndexOf("Principal Partner") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")).ToString() : "";
                        if (dtNew.Columns.IndexOf("Status") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Status")).ToString() != "")
                            dStatus = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Status")));
                        if (dtNew.Columns.IndexOf("Open Store") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Open Store")) != null && reader.GetValue(dtNew.Columns.IndexOf("Open Store")).ToString() != "")
                            dOpenStore = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Open Store")));//default value

                        tProjectStatus = dtNew.Columns.IndexOf("Project Status") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Project Status")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Status")).ToString() : "";

                        nNumberOfTabletsPerStore = dtNew.Columns.IndexOf("# of tablets per store") > 0 && reader.GetValue(dtNew.Columns.IndexOf("# of tablets per store")) != null && reader.GetValue(dtNew.Columns.IndexOf("# of tablets per store")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("# of tablets per store"))) :0;
                        tEquipmentVendor = dtNew.Columns.IndexOf("\"Equipment Vendor") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Equipment Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Equipment Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Equipment Vendor"))) : "";
                        if (dtNew.Columns.IndexOf("Ship Date") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Ship Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Ship Date")).ToString() != "")
                            dShipDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Ship Date")));
                        if (dtNew.Columns.IndexOf("Revisit Date") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                            dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                        if (dtNew.Columns.IndexOf("Scheduled Install Date") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Scheduled Install Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Scheduled Install Date")).ToString() != "")
                            dInstallDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Scheduled Install Date")));
                        tInstallationVendor = dtNew.Columns.IndexOf("Installation Vendor") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                        tInstallStatus = dtNew.Columns.IndexOf("Install Status") > 0 && reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
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
            return this;
        }

        public string getStoreNumber()
        {
            return this.tStoreNumber;
        }

        public void setValues(int nExist, int? nProjectId, int? nStoreId)
        {
            this.nStoreId = nStoreId;
            this.nStoreExistStatus = nExist;
            this.nProjectId = nProjectId;
        }
    }

    public class ProjectExcelFieldsOrderAccurcy : IProjectExcelFields
    {
        public int nBrandId { get; set; }
        private Nullable<int> _nProjectId;
        private Nullable<int> _nStoreId;
        public Nullable<int> nProjectId
        {
            get
            {
                return _nProjectId == null ? 0 : _nProjectId;
            }
            set
            {
                _nProjectId = value;
            }
        }
        public Nullable<int> nStoreId
        {
            get
            {
                return _nStoreId == null ? 0 : _nStoreId;
            }
            set
            {
                _nStoreId = value;
            }
        }
        public string tProjectType { get; set; }
        public string tStoreNumber { get; set; }
        public string tAddress { get; set; }
        public string tCity { get; set; }
        public string tState { get; set; }
        public int nDMAID { get; set; }
        public string tDMA { get; set; }
        public string tRED { get; set; }
        public string tCM { get; set; }
        public string tANE { get; set; }
        public string tRVP { get; set; }
        public string tPrincipalPartner { get; set; }
        public Nullable<DateTime> dStatus { get; set; }
        public Nullable<DateTime> dOpenStore { get; set; }
        public string tProjectStatus { get; set; }
        public int nStoreExistStatus { get; set; }
        /// <summary>
        /// Order Accuracy
        /// </summary>
        public string tOrderAccuracyVendor { get; set; }
        public string tOrderAccuracyStatus { get; set; }
        public int nBakeryPrinter { get; set; }
        public int nDualCupLabel { get; set; }
        public int nDTExpo { get; set; }
        public int nFCExpo { get; set; }

        public Nullable<DateTime> dShipDate { get; set; }

        public string tShippingCarrier { get; set; }
        public string tTrackingNumber { get; set; }


        public Nullable<DateTime> dDeliveryDate { get; set; }
        /// <summary>
        /// Installation
        /// </summary>
        public string tInstallationVendor { get; set; }

        public string tInstallStatus { get; set; }
        public Nullable<DateTime> dInstallDate { get; set; }
        public string tInstallTime { get; set; }
        public string tInstallTechNumber { get; set; }
        public string tManagerName { get; set; }
        public string tManagerNumber { get; set; }
        public string tManagerCheckout { get; set; }
        public string tPhotoDeliverables { get; set; }
        public string tLeadTech { get; set; }
        public Nullable<DateTime> dInstallEnd { get; set; }
        public string tSignoffs { get; set; }
        public string tTestTransactions { get; set; }
        public string tInstallProjectStatus { get; set; }
        public Nullable<DateTime> dRevisitDate { get; set; }
        public string tCost { get; set; }
        public string tInstallNotes { get; set; }
        public string tInstallType { get; set; }

        public IProjectExcelFields getModelFromColumns(IExcelDataReader reader, DataTable dtNew)
        {
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                DataTable dt = new DataTable();
                try
                {
                    ProjectType nProjectType;
                    string storeNumber = reader.GetValue(dtNew.Columns.IndexOf("Store Number")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Store Number")).ToString() : "";
                    string projectType = reader.GetValue(dtNew.Columns.IndexOf("Project Type")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Type")).ToString() : "";
                    if (projectType != "" && storeNumber != "" && Enum.TryParse(projectType.Replace(" ", ""), true, out nProjectType))
                    {
                        tProjectType = projectType;
                        tStoreNumber = storeNumber;
                        tAddress = reader.GetValue(dtNew.Columns.IndexOf("Address")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Address")).ToString() : "";
                        tCity = reader.GetValue(dtNew.Columns.IndexOf("City")) != null ? reader.GetValue(dtNew.Columns.IndexOf("City")).ToString() : "";
                        tState = reader.GetValue(dtNew.Columns.IndexOf("State")) != null ? reader.GetValue(dtNew.Columns.IndexOf("State")).ToString() : "";
                        nDMAID = reader.GetValue(dtNew.Columns.IndexOf("DMA ID")) != null && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DMA ID"))) : 0;
                        tDMA = reader.GetValue(dtNew.Columns.IndexOf("DMA")) != null ? reader.GetValue(dtNew.Columns.IndexOf("DMA")).ToString() : "";
                        tRED = reader.GetValue(dtNew.Columns.IndexOf("RED")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RED")).ToString() : "";
                        tCM = reader.GetValue(dtNew.Columns.IndexOf("CM")) != null ? reader.GetValue(dtNew.Columns.IndexOf("CM")).ToString() : "";
                        tANE = reader.GetValue(dtNew.Columns.IndexOf("A&E")) != null ? reader.GetValue(dtNew.Columns.IndexOf("A&E")).ToString() : "";
                        tRVP = reader.GetValue(dtNew.Columns.IndexOf("RVP")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RVP")).ToString() : "";
                        tPrincipalPartner = reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")).ToString() : "";
                        if (reader.GetValue(dtNew.Columns.IndexOf("Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Status")).ToString() != "")
                            dStatus = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Status")));
                        if (reader.GetValue(dtNew.Columns.IndexOf("Open Store")) != null && reader.GetValue(dtNew.Columns.IndexOf("Open Store")).ToString() != "")
                            dOpenStore = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Open Store")));//default value

                        tProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Project Status")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Status")).ToString() : "";

                        tOrderAccuracyVendor = reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Vendor"))) : "";
                        tOrderAccuracyStatus = reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Order Accuracy Status"))) : "";
                        nBakeryPrinter = reader.GetValue(dtNew.Columns.IndexOf("Bakery Printer")) != null && reader.GetValue(dtNew.Columns.IndexOf("Bakery Printer")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Bakery Printer"))) : 0;
                        nDualCupLabel = reader.GetValue(dtNew.Columns.IndexOf("Dual Cup Label")) != null && reader.GetValue(dtNew.Columns.IndexOf("Dual Cup Label")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Dual Cup Label"))) : 0;
                        nDTExpo = reader.GetValue(dtNew.Columns.IndexOf("DT Expo")) != null && reader.GetValue(dtNew.Columns.IndexOf("DT Expo")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DT Expo"))) : 0;
                        nFCExpo = reader.GetValue(dtNew.Columns.IndexOf("FC Expo")) != null && reader.GetValue(dtNew.Columns.IndexOf("FC Expo")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("FC Expo"))) : 0;


                        if (reader.GetValue(dtNew.Columns.IndexOf("Ship Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Ship Date")).ToString() != "")
                            dShipDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Ship Date")));

                        tShippingCarrier = reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier"))) : "";
                        tTrackingNumber = reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Tracking Number"))) : "";

                        if (reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")).ToString() != "")
                            dDeliveryDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")));




                        tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                        tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                        if (reader.GetValue(dtNew.Columns.IndexOf("Install Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Date")).ToString() != "")
                            dInstallDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install Date")));

                        tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                        tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                        tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                        tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                        tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                        tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                        tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                        if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                            dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                        tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                        tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                        tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                        if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                            dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                        tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                        tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                        tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";
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
            return this;
        }

        public string getStoreNumber()
        {
            return this.tStoreNumber;
        }
        public void setValues(int nExist, int? nProjectId, int? nStoreId)
        {
            this.nStoreId = nStoreId;
            this.nStoreExistStatus = nExist;
            this.nProjectId = nProjectId;
        }
    }

    public class ProjectExcelFieldsOrderStatusBoard : IProjectExcelFields
    {
        public int nBrandId { get; set; }
        private Nullable<int> _nProjectId;
        private Nullable<int> _nStoreId;
        public Nullable<int> nProjectId
        {
            get
            {
                return _nProjectId == null ? 0 : _nProjectId;
            }
            set
            {
                _nProjectId = value;
            }
        }
        public Nullable<int> nStoreId
        {
            get
            {
                return _nStoreId == null ? 0 : _nStoreId;
            }
            set
            {
                _nStoreId = value;
            }
        }
        public string tProjectType { get; set; }
        public string tStoreNumber { get; set; }
        public string tAddress { get; set; }
        public string tCity { get; set; }
        public string tState { get; set; }
        public int nDMAID { get; set; }
        public string tDMA { get; set; }
        public string tRED { get; set; }
        public string tCM { get; set; }
        public string tANE { get; set; }
        public string tRVP { get; set; }
        public string tPrincipalPartner { get; set; }
        public Nullable<DateTime> dStatus { get; set; }
        public Nullable<DateTime> dOpenStore { get; set; }
        public string tProjectStatus { get; set; }
        public int nStoreExistStatus { get; set; }
        /// <summary>
        /// Order Status Board
        /// </summary>
        public string tOrderStatusBoardVendor { get; set; }
        public string tOrderStatusBoardStatus { get; set; }
        public int nOSB { get; set; }
        public Nullable<DateTime> dShipDate { get; set; }
        public string tShippingCarrier { get; set; }
        public string tTrackingNumber { get; set; }
        public Nullable<DateTime> dDeliveryDate { get; set; }
        /// <summary>
        /// Insatllation
        /// </summary>
        public string tInstallationVendor { get; set; }

        public string tInstallStatus { get; set; }

        public Nullable<DateTime> dInstallDate { get; set; }
        public string tInstallTime { get; set; }

        public string tInstallTechNumber { get; set; }
        public string tManagerName { get; set; }
        public string tManagerNumber { get; set; }
        public string tManagerCheckout { get; set; }
        public string tPhotoDeliverables { get; set; }
        public string tLeadTech { get; set; }
        public Nullable<DateTime> dInstallEnd { get; set; }
        public string tSignoffs { get; set; }
        public string tTestTransactions { get; set; }
        public string tInstallProjectStatus { get; set; }
        public Nullable<DateTime> dRevisitDate { get; set; }
        public string tCost { get; set; }
        public string tInstallNotes { get; set; }
        public string tInstallType { get; set; }

        public IProjectExcelFields getModelFromColumns(IExcelDataReader reader, DataTable dtNew)
        {
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                DataTable dt = new DataTable();
                try
                {
                    ProjectType nProjectType;
                    string storeNumber = reader.GetValue(dtNew.Columns.IndexOf("Store Number")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Store Number")).ToString() : "";
                    string projectType = reader.GetValue(dtNew.Columns.IndexOf("Project Type")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Type")).ToString() : "";
                    if (projectType != "" && storeNumber != "" && Enum.TryParse(projectType.Replace(" ", ""), true, out nProjectType))
                    {
                        tProjectType = projectType;
                        tStoreNumber = storeNumber;
                        tAddress = reader.GetValue(dtNew.Columns.IndexOf("Address")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Address")).ToString() : "";
                        tCity = reader.GetValue(dtNew.Columns.IndexOf("City")) != null ? reader.GetValue(dtNew.Columns.IndexOf("City")).ToString() : "";
                        tState = reader.GetValue(dtNew.Columns.IndexOf("State")) != null ? reader.GetValue(dtNew.Columns.IndexOf("State")).ToString() : "";
                        nDMAID = reader.GetValue(dtNew.Columns.IndexOf("DMA ID")) != null && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DMA ID"))) : 0;
                        tDMA = reader.GetValue(dtNew.Columns.IndexOf("DMA")) != null ? reader.GetValue(dtNew.Columns.IndexOf("DMA")).ToString() : "";
                        tRED = reader.GetValue(dtNew.Columns.IndexOf("RED")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RED")).ToString() : "";
                        tCM = reader.GetValue(dtNew.Columns.IndexOf("CM")) != null ? reader.GetValue(dtNew.Columns.IndexOf("CM")).ToString() : "";
                        tANE = reader.GetValue(dtNew.Columns.IndexOf("A&E")) != null ? reader.GetValue(dtNew.Columns.IndexOf("A&E")).ToString() : "";
                        tRVP = reader.GetValue(dtNew.Columns.IndexOf("RVP")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RVP")).ToString() : "";
                        tPrincipalPartner = reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")).ToString() : "";
                        if (reader.GetValue(dtNew.Columns.IndexOf("Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Status")).ToString() != "")
                            dStatus = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Status")));
                        if (reader.GetValue(dtNew.Columns.IndexOf("Open Store")) != null && reader.GetValue(dtNew.Columns.IndexOf("Open Store")).ToString() != "")
                            dOpenStore = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Open Store")));//default value

                        tProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Project Status")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Status")).ToString() : "";


                        tOrderStatusBoardVendor = reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Vendor"))) : "";
                        tOrderStatusBoardStatus = reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Order Status Board Status"))) : "";
                        nOSB = reader.GetValue(dtNew.Columns.IndexOf("OSB")) != null && reader.GetValue(dtNew.Columns.IndexOf("OSB")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("OSB"))) : 0;

                        if (reader.GetValue(dtNew.Columns.IndexOf("Ship Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Ship Date")).ToString() != "")
                            dShipDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Ship Date")));

                        tShippingCarrier = reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier"))) : "";
                        tTrackingNumber = reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Tracking Number"))) : "";

                        if (reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")).ToString() != "")
                            dDeliveryDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")));




                        tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                        tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                        if (reader.GetValue(dtNew.Columns.IndexOf("Install Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Date")).ToString() != "")
                            dInstallDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install Date")));

                        tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                        tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                        tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                        tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                        tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                        tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                        tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                        if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                            dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                        tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                        tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                        tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                        if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                            dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                        tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                        tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                        tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";
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
            return this;
        }

        public string getStoreNumber()
        {
            return this.tStoreNumber;
        }
        public void setValues(int nExist, int? nProjectId, int? nStoreId)
        {
            this.nStoreId = nStoreId;
            this.nStoreExistStatus = nExist;
            this.nProjectId = nProjectId;
        }
    }
    public class ProjectExcelFieldsHPRollout : IProjectExcelFields
    {
        public int nBrandId { get; set; }
        private Nullable<int> _nProjectId;
        private Nullable<int> _nStoreId;
        public Nullable<int> nProjectId
        {
            get
            {
                return _nProjectId == null ? 0 : _nProjectId;
            }
            set
            {
                _nProjectId = value;
            }
        }
        public Nullable<int> nStoreId
        {
            get
            {
                return _nStoreId == null ? 0 : _nStoreId;
            }
            set
            {
                _nStoreId = value;
            }
        }
        public string tProjectType { get; set; }
        public string tStoreNumber { get; set; }
        public string tAddress { get; set; }
        public string tCity { get; set; }
        public string tState { get; set; }
        public int nDMAID { get; set; }
        public string tDMA { get; set; }
        public string tRED { get; set; }
        public string tCM { get; set; }
        public string tANE { get; set; }
        public string tRVP { get; set; }
        public string tPrincipalPartner { get; set; }
        public Nullable<DateTime> dStatus { get; set; }
        public Nullable<DateTime> dOpenStore { get; set; }
        public string tProjectStatus { get; set; }
        public int nStoreExistStatus { get; set; }
        /// <summary>
        /// Network Switch 
        /// </summary>
        public string tNetworkSwitchVendor { get; set; }
        public string tNetworkSwitchStatus { get; set; }
        public string tShipmenttoVendor { get; set; }
        public string tSetupStatus { get; set; }
        public string tNewSerialNumber { get; set; }

        public string tOldSerialNumber { get; set; }
        public string tOldSwitchReturnStatus { get; set; }
        public string tOldSwitchTracking { get; set; }
        /// <summary>
        /// Image/Memory
        /// </summary>
        public string tImageMemoryVendor { get; set; }
        public string tImageMemoryStatus { get; set; }
        public string tShipmentTracking { get; set; }
        public string tReturnShipment { get; set; }
        public string tReturnShipmentTracking { get; set; }

        /// <summary>
        /// Installation
        /// </summary>
        public string tInstallationVendor { get; set; }

        public string tInstallStatus { get; set; }
        public Nullable<DateTime> dInstallDate { get; set; }
        public string tInstallTime { get; set; }
        public string tInstallTechNumber { get; set; }
        public string tManagerName { get; set; }
        public string tManagerNumber { get; set; }
        public string tManagerCheckout { get; set; }
        public string tPhotoDeliverables { get; set; }
        public string tLeadTech { get; set; }
        public Nullable<DateTime> dInstallEnd { get; set; }
        public string tSignoffs { get; set; }
        public string tTestTransactions { get; set; }
        public string tInstallProjectStatus { get; set; }
        public Nullable<DateTime> dRevisitDate { get; set; }
        public string tCost { get; set; }
        public string tInstallNotes { get; set; }
        public string tInstallType { get; set; }

        public IProjectExcelFields getModelFromColumns(IExcelDataReader reader, DataTable dtNew)
        {
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                DataTable dt = new DataTable();
                try
                {
                    ProjectType nProjectType;
                    string storeNumber = reader.GetValue(dtNew.Columns.IndexOf("Store Number")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Store Number")).ToString() : "";
                    string projectType = reader.GetValue(dtNew.Columns.IndexOf("Project Type")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Type")).ToString() : "";
                    if (projectType != "" && storeNumber != "" && Enum.TryParse(projectType.Replace(" ", ""), true, out nProjectType))
                    {
                        tProjectType = projectType;
                        tStoreNumber = storeNumber;
                        tAddress = reader.GetValue(dtNew.Columns.IndexOf("Address")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Address")).ToString() : "";
                        tCity = reader.GetValue(dtNew.Columns.IndexOf("City")) != null ? reader.GetValue(dtNew.Columns.IndexOf("City")).ToString() : "";
                        tState = reader.GetValue(dtNew.Columns.IndexOf("State")) != null ? reader.GetValue(dtNew.Columns.IndexOf("State")).ToString() : "";
                        nDMAID = reader.GetValue(dtNew.Columns.IndexOf("DMA ID")) != null && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DMA ID"))) : 0;
                        tDMA = reader.GetValue(dtNew.Columns.IndexOf("DMA")) != null ? reader.GetValue(dtNew.Columns.IndexOf("DMA")).ToString() : "";
                        tRED = reader.GetValue(dtNew.Columns.IndexOf("RED")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RED")).ToString() : "";
                        tCM = reader.GetValue(dtNew.Columns.IndexOf("CM")) != null ? reader.GetValue(dtNew.Columns.IndexOf("CM")).ToString() : "";
                        tANE = reader.GetValue(dtNew.Columns.IndexOf("A&E")) != null ? reader.GetValue(dtNew.Columns.IndexOf("A&E")).ToString() : "";
                        tRVP = reader.GetValue(dtNew.Columns.IndexOf("RVP")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RVP")).ToString() : "";
                        tPrincipalPartner = reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")).ToString() : "";
                        if (reader.GetValue(dtNew.Columns.IndexOf("Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Status")).ToString() != "")
                            dStatus = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Status")));
                        if (reader.GetValue(dtNew.Columns.IndexOf("Open Store")) != null && reader.GetValue(dtNew.Columns.IndexOf("Open Store")).ToString() != "")
                            dOpenStore = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Open Store")));//default value

                        tProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Project Status")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Status")).ToString() : "";


                        tNetworkSwitchVendor = reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Network Switch Vendor"))) : "";
                        tNetworkSwitchStatus = reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Network Switch Status"))) : "";
                        tShipmenttoVendor = reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipment to Vendor"))) : "";
                        tSetupStatus = reader.GetValue(dtNew.Columns.IndexOf("Setup Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Setup Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Setup Status"))) : "";
                        tNewSerialNumber = reader.GetValue(dtNew.Columns.IndexOf("New Serial Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("New Serial Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("New Serial Number"))) : "";
                        tOldSerialNumber = reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Serial Number"))) : "";
                        tOldSwitchReturnStatus = dtNew.Columns.IndexOf("Old Switch Return Status") > -1 && reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Switch Return Status"))) : "";
                        tOldSwitchTracking = reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Old Switch Tracking"))) : "";

                        /////////////
                        tImageMemoryVendor = reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Image Memory Vendor"))) : "";
                        tImageMemoryStatus = reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Image Memory Status"))) : "";
                        tShipmentTracking = reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipment Tracking"))) : "";
                        tReturnShipment = reader.GetValue(dtNew.Columns.IndexOf("Return Shipment")) != null && reader.GetValue(dtNew.Columns.IndexOf("Return Shipment")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Return Shipment"))) : "";
                        tReturnShipmentTracking = reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking")) != null && reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Return Shipment Tracking"))) : "";



                        tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                        tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                        dInstallDate = reader.ConvertMeToDateTime(dtNew, "Install Date");

                        tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                        tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                        tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                        tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                        tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                        tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                        tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                        if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                            dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                        tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                        tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                        tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                        if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                            dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                        tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                        tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                        tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";
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
            return this;
        }

        public string getStoreNumber()
        {
            return this.tStoreNumber;
        }

        public void setValues(int nExist, int? nProjectId, int? nStoreId)
        {
            this.nStoreId = nStoreId;
            this.nStoreExistStatus = nExist;
            this.nProjectId = nProjectId;
        }
    }

    public class ProjectExcelFieldsServerHandheld : IProjectExcelFields
    {
        public int nBrandId { get; set; }

        private Nullable<int> _nProjectId;
        private Nullable<int> _nStoreId;
        public Nullable<int> nProjectId
        {
            get
            {
                return _nProjectId == null ? 0 : _nProjectId;
            }
            set
            {
                _nProjectId = value;
            }
        }
        public Nullable<int> nStoreId
        {
            get
            {
                return _nStoreId == null ? 0 : _nStoreId;
            }
            set
            {
                _nStoreId = value;
            }
        }
        public string tProjectType { get; set; }
        public string tStoreNumber { get; set; }
        public string tAddress { get; set; }
        public string tCity { get; set; }
        public string tState { get; set; }
        public int nDMAID { get; set; }
        public string tDMA { get; set; }
        public string tRED { get; set; }
        public string tCM { get; set; }
        public string tANE { get; set; }
        public string tRVP { get; set; }
        public string tPrincipalPartner { get; set; }
        public Nullable<DateTime> dStatus { get; set; }
        public Nullable<DateTime> dOpenStore { get; set; }
        public string tProjectStatus { get; set; }
        public int nStoreExistStatus { get; set; }
        /// <summary>
        /// ServerHandheld 
        /// </summary>
        public string tServerHandheldVendor { get; set; }
        public string tServerHandheldStatus { get; set; }
        public Nullable<DateTime> dShipDate { get; set; }
        public string tShippingCarrier { get; set; }
        public string tTrackingNumber { get; set; }
        public int nTablets { get; set; }
        public int nFiveBayharger { get; set; }
        public int nShoulderStrap { get; set; }
        public int nProtectiveCase { get; set; }

        public Nullable<DateTime> dDeliveryDate { get; set; }
        public string tServerHandheldCost { get; set; }


        /// <summary>
        /// Installation
        /// </summary>
        public string tInstallationVendor { get; set; }

        public string tInstallStatus { get; set; }
        public Nullable<DateTime> dInstallDate { get; set; }
        public string tInstallTime { get; set; }
        public string tInstallTechNumber { get; set; }
        public string tManagerName { get; set; }
        public string tManagerNumber { get; set; }
        public string tManagerCheckout { get; set; }
        public string tPhotoDeliverables { get; set; }
        public string tLeadTech { get; set; }
        public Nullable<DateTime> dInstallEnd { get; set; }
        public string tSignoffs { get; set; }
        public string tTestTransactions { get; set; }
        public string tInstallProjectStatus { get; set; }
        public Nullable<DateTime> dRevisitDate { get; set; }
        public string tCost { get; set; }
        public string tInstallNotes { get; set; }
        public string tInstallType { get; set; }

        public IProjectExcelFields getModelFromColumns(IExcelDataReader reader, DataTable dtNew)
        {
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                DataTable dt = new DataTable();
                try
                {
                    ProjectType nProjectType;
                    string storeNumber = reader.GetValue(dtNew.Columns.IndexOf("Store Number")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Store Number")).ToString() : "";
                    string projectType = reader.GetValue(dtNew.Columns.IndexOf("Project Type")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Type")).ToString() : "";
                    if (projectType != "" && storeNumber != "" && Enum.TryParse(projectType.Replace(" ", ""), true, out nProjectType))
                    {
                        tProjectType = projectType;
                        tStoreNumber = storeNumber;
                        tAddress = reader.GetValue(dtNew.Columns.IndexOf("Address")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Address")).ToString() : "";
                        tCity = reader.GetValue(dtNew.Columns.IndexOf("City")) != null ? reader.GetValue(dtNew.Columns.IndexOf("City")).ToString() : "";
                        tState = reader.GetValue(dtNew.Columns.IndexOf("State")) != null ? reader.GetValue(dtNew.Columns.IndexOf("State")).ToString() : "";
                        nDMAID = reader.GetValue(dtNew.Columns.IndexOf("DMA ID")) != null && reader.GetValue(dtNew.Columns.IndexOf("DMA ID")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("DMA ID"))) : 0;
                        tDMA = reader.GetValue(dtNew.Columns.IndexOf("DMA")) != null ? reader.GetValue(dtNew.Columns.IndexOf("DMA")).ToString() : "";
                        tRED = reader.GetValue(dtNew.Columns.IndexOf("RED")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RED")).ToString() : "";
                        tCM = reader.GetValue(dtNew.Columns.IndexOf("CM")) != null ? reader.GetValue(dtNew.Columns.IndexOf("CM")).ToString() : "";
                        tANE = reader.GetValue(dtNew.Columns.IndexOf("A&E")) != null ? reader.GetValue(dtNew.Columns.IndexOf("A&E")).ToString() : "";
                        tRVP = reader.GetValue(dtNew.Columns.IndexOf("RVP")) != null ? reader.GetValue(dtNew.Columns.IndexOf("RVP")).ToString() : "";
                        tPrincipalPartner = reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Principal Partner")).ToString() : "";
                        if (reader.GetValue(dtNew.Columns.IndexOf("Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Status")).ToString() != "")
                            dStatus = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Status")));
                        if (reader.GetValue(dtNew.Columns.IndexOf("Open Store")) != null && reader.GetValue(dtNew.Columns.IndexOf("Open Store")).ToString() != "")
                            dOpenStore = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Open Store")));//default value

                        tProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Project Status")) != null ? reader.GetValue(dtNew.Columns.IndexOf("Project Status")).ToString() : "";


                        tServerHandheldVendor = reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Vendor"))) : "";
                        tServerHandheldStatus = reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Status"))) : "";

                        if (reader.GetValue(dtNew.Columns.IndexOf("Ship Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Ship Date")).ToString() != "")
                            dShipDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Ship Date")));

                        tShippingCarrier = reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Shipping Carrier"))) : "";
                        tTrackingNumber = reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Tracking Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Tracking Number"))) : "";

                        nTablets = reader.GetValue(dtNew.Columns.IndexOf("Tablets")) != null && reader.GetValue(dtNew.Columns.IndexOf("Tablets")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Tablets"))) : 0;
                        nFiveBayharger = reader.GetValue(dtNew.Columns.IndexOf("5 Bay Charger")) != null && reader.GetValue(dtNew.Columns.IndexOf("5 Bay Charger")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("5 Bay Charger"))) : 0;
                        nShoulderStrap = reader.GetValue(dtNew.Columns.IndexOf("Shoulder Strap")) != null && reader.GetValue(dtNew.Columns.IndexOf("Shoulder Strap")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Shoulder Strap"))) : 0;
                        nProtectiveCase = reader.GetValue(dtNew.Columns.IndexOf("Protective Case")) != null && reader.GetValue(dtNew.Columns.IndexOf("Protective Case")).ToString() != "" ? Convert.ToInt32(reader.GetValue(dtNew.Columns.IndexOf("Protective Case"))) : 0;

                        if (reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")).ToString() != "")
                            dDeliveryDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Delivery Date")));

                        tServerHandheldCost = reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Server Handheld Cost"))) : "";



                        tInstallationVendor = reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")) != null && reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Installation Vendor"))) : "";
                        tInstallStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                        if (reader.GetValue(dtNew.Columns.IndexOf("Install Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Date")).ToString() != "")
                            dInstallDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install Date")));

                        tInstallTime = reader.GetValue(dtNew.Columns.IndexOf("Install Time")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Time")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Time"))) : "";
                        tInstallTechNumber = reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Tech Number"))) : "";

                        tManagerName = reader.GetValue(dtNew.Columns.IndexOf("Manager Name")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Name")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Name"))) : "";
                        tManagerNumber = reader.GetValue(dtNew.Columns.IndexOf("Manager Number")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Number")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Number"))) : "";

                        tManagerCheckout = reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")) != null && reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Manager Checkout"))) : "";
                        tPhotoDeliverables = reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")) != null && reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Photo Deliverables"))) : "";

                        tLeadTech = reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")) != null && reader.GetValue(dtNew.Columns.IndexOf("Lead Tech")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Lead Tech"))) : "";
                        if (reader.GetValue(dtNew.Columns.IndexOf("Install End")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install End")).ToString() != "")
                            dInstallEnd = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Install End")));
                        tSignoffs = reader.GetValue(dtNew.Columns.IndexOf("Signoffs")) != null && reader.GetValue(dtNew.Columns.IndexOf("Signoffs")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Signoffs"))) : "";
                        tTestTransactions = reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")) != null && reader.GetValue(dtNew.Columns.IndexOf("Test Transactions")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";
                        tInstallProjectStatus = reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Project Status")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Status"))) : "";

                        if (reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")) != null && reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")).ToString() != "")
                            dRevisitDate = Convert.ToDateTime(reader.GetValue(dtNew.Columns.IndexOf("Revisit Date")));

                        tCost = reader.GetValue(dtNew.Columns.IndexOf("Cost")) != null && reader.GetValue(dtNew.Columns.IndexOf("Cost")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Cost"))) : "";
                        tInstallNotes = reader.GetValue(dtNew.Columns.IndexOf("Install Notes")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Notes")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Notes"))) : "";
                        tInstallType = reader.GetValue(dtNew.Columns.IndexOf("Install Type")) != null && reader.GetValue(dtNew.Columns.IndexOf("Install Type")).ToString() != "" ? Convert.ToString(reader.GetValue(dtNew.Columns.IndexOf("Install Type"))) : "";
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
            return this;
        }

        public string getStoreNumber()
        {
            return this.tStoreNumber;
        }
        public void setValues(int nExist, int? nProjectId, int? nStoreId)
        {
            this.nStoreId = nStoreId;
            this.nStoreExistStatus = nExist;
            this.nProjectId = nProjectId;
        }
    }

    public class ProjectExcelFieldsSonic
    {
        public int nBrandId { get; set; }
        public string tProjectType { get; set; }
        public string tStoreNumber { get; set; }
        public string tAddress { get; set; }
        public string tCity { get; set; }
        public string tState { get; set; }
        public int nDMAID { get; set; }
        public string tDMA { get; set; }
        public string tRED { get; set; }
        public string tCM { get; set; }
        public string tANE { get; set; }
        public string tRVP { get; set; }
        public string tPrincipalPartner { get; set; }
        public DateTime dStatus { get; set; }
        public DateTime dOpenStore { get; set; }
        public string tProjectStatus { get; set; }
        public int nStoreExistStatus { get; set; }

    }
}