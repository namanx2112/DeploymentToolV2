using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{

    public interface IProjectExcelFields
    {

    }
    public class ProjectExcelFields: IProjectExcelFields
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

    }

    public class ProjectExcelFieldsOrderAccurcy: IProjectExcelFields
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

    }

    public class ProjectExcelFieldsOrderStatusBoard: IProjectExcelFields
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