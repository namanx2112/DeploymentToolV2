using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ProjectPortfolio: ITableActualResponse
    {        
        public int nStoreId {  get; set; }
        public int nProjectType { get; set; }
        public string tProjectType { get; set; }
        public int nProjectId {  get; set; }
        public ProjectPortfolioStore store { get; set; }
        public ProjectPortfolioItems networking { get; set; }
        public ProjectPortfolioItems pos { get; set; }
        public ProjectPortfolioItems audio { get; set; }
        public ProjectPortfolioItems exteriormenu { get; set; }
        public ProjectPortfolioItems paymentsystem { get; set; }
        public ProjectPortfolioItems interiormenu { get; set; }
        public ProjectPortfolioItems sonicradio { get; set; }
        public ProjectPortfolioItems serverhandheld { get; set; }

        public ProjectPortfolioItems orderaccuracy { get; set; }
        public ProjectPortfolioItems orderstatusboard { get; set; }
        public ProjectPortfolioItems networkswitch { get; set; }

        public ProjectPortfolioItems imagememory { get; set; }
        public ProjectPortfolioItems installation { get; set; }
        public List<ProjectPorfolioNotes> notes { get; set; }
    }

    public class ProjectPortfolioStore
    {
        public string tStoreNumber { get; set; }
        public string tStoreDetails { get; set; }
        public Nullable<DateTime> dtGoliveDate { get; set; }
        public string tProjectManager { get; set; }
        public string tProjectType { get; set; }
        public string tFranchise { get; set; }
        public decimal cCost { get; set; }

        public Nullable<DateTime> dInstallDate { get; set; }
    }

    public class ProjectPortfolioItems
    {
        public string tVendor { get; set; }
        public Nullable<DateTime> dtDate { get; set; }
        public string tStatus { get; set; }

        public Nullable<DateTime> dSupportDate { get; set; }

        public string tLoopType { get; set; }

        public string tLoopStatus { get; set; }
        public string tBuyPassID { get; set; }

        public string tServerEPS { get; set; }
        public Nullable<DateTime> dInstallEndDate { get; set; }
        public string tSignoffs { get; set; }
        public string tTestTransactions { get; set; }
    }

    public class ProjectPorfolioNotes
    {
        
        public Nullable<int> aNoteID { get; set; }
        public int nProjectId { get; set; }
        public string tNotesOwner { get; set; }
        public string tNotesDesc { get; set; }
      
    }
    public class VendorList
    {

        public int aVendorId { get; set; }
        public string tVendorName { get; set; }

    }
    public class iDropDownList
    {

        public int aDropdownId { get; set; }
        public string tDropdownText { get; set; }

        public Nullable<int> nBrandId { get; set; }

    }

    public class ActivePortFolioProjectsModel
    {
        

        public int nProjectId { get; set; }

        public int nStoreId { get; set; }
        public int nProjectType { get; set; }
        
        public string tProjectType { get; set; }

        public string tStoreNumber { get; set; }
        public string tStoreDetails { get; set; }
        public Nullable<DateTime> dProjectGoliveDate { get; set; }
        public Nullable<DateTime> dProjEndDate { get; set; }
        public string tProjManager { get; set; }
        public string tStatus { get; set; }
        public string tNewVendor { get; set; }

        public Nullable<int> nVendor { get; set; }


        public string tFranchise { get; set; }
        public Nullable<decimal> cCost { get; set; }
        //public decimal cCost { get; set; }
        public Nullable<DateTime> dInstallDate { get; set; }

        public int nTotalRows { get; set; }

    }
    public class ActivePortFolioProjectsAllModel
    {


        public int nStoreID        { get; set; }

        public int nBrandID { get; set; }

        public string tStoreNumber { get; set; }
        public string tStoreDetails { get; set; }
        
        public int nProjectType { get; set; }

        public string tProjectType { get; set; }
        
        public int nProjectActiveStatus { get; set; }
        public int aProjectID { get; set; }
        public string tProjectManager { get; set; }
        public string tFranchise { get; set; }

        public Nullable<DateTime> dGoliveDate { get; set; }
        public Nullable<DateTime> dProjEndDate { get; set; }

        public Nullable<decimal> cCost { get; set; }
        public Nullable<int> nNetworkingVendorID { get; set; }
        public Nullable<DateTime> dNetworkDeliveryDate { get; set; }
        public Nullable<int> nPrimaryStatus { get; set; }
        public Nullable<DateTime> dDateFor_nPrimaryStatus { get; set; }


        public Nullable<int> nPOSVendorID { get; set; }
        public Nullable<DateTime> dPOSDeliveryDate { get; set; }
        public Nullable<DateTime> dPOSSupportDate { get; set; }
        
        public Nullable<int> nPOSnStatusID { get; set; }
        public Nullable<DateTime> dPOSDateFor_nStatus { get; set; }

        public Nullable<int> nAudioVendorID { get; set; }
        public Nullable<int> nLoopStatus { get; set; }
        public Nullable<DateTime> dDateFor_nLoopStatus { get; set; }
        public Nullable<DateTime> dAudioDeliveryDate { get; set; }
        public Nullable<int> nLoopType { get; set; }

        public Nullable<int> nAudionStatusID { get; set; }
        public Nullable<DateTime> dAudioDateFor_nStatus { get; set; }


        public Nullable<int> nExteriorMenusVendorID { get; set; }
        public Nullable<DateTime> dExteriorMenusDeliveryDate { get; set; }

        public Nullable<int> nExteriorMenusStatusID { get; set; }
        public Nullable<DateTime> dExteriorMenusDateFor_nStatus { get; set; }

        public Nullable<int> nPaymentSystemVendorID { get; set; }
        public Nullable<DateTime> dPaymentSystemDeliveryDate { get; set; }

        public Nullable<int> nPaymentSystemStatusID { get; set; }
        public Nullable<DateTime> dPaymentSystemDateFor_nStatus { get; set; }
        public Nullable<int> nBuyPassID { get; set; }
        public Nullable<int> nServerEPS { get; set; }

        public Nullable<int> nInteriorMenusVendorID { get; set; }
        public Nullable<DateTime> dInteriorMenusDeliveryDate { get; set; }

        public Nullable<int> nInteriorMenusStatusID { get; set; }
        public Nullable<DateTime> dInteriorMenusDateFor_nStatus { get; set; }

        public Nullable<int> nNetworkSwitchVendorID { get; set; }

        public Nullable<int> nNetworkSwitchStatusID { get; set; }
        public Nullable<DateTime> dNetworkSwitchDateFor_nStatusID { get; set; }

        public Nullable<int> nImageOrMemoryVendorID { get; set; }

        public Nullable<int> nImageOrMemoryStatusID { get; set; }
        public Nullable<DateTime> dImageOrMemoryDateFor_nStatus { get; set; }

        public Nullable<int> nOrderAccuracyVendorID { get; set; }
        public Nullable<DateTime> dOrderAccuracyDeliveryDate { get; set; }

        public Nullable<int> nOrderAccuracyStatusID { get; set; }
        public Nullable<DateTime> dOrderAccuracyDateFor_nStatus { get; set; }

        public Nullable<int> nOrderStatusBoardVendorID { get; set; }
        public Nullable<DateTime> dOrderStatusBoardDeliveryDate { get; set; }

        public Nullable<int> nOrderStatusBoardStatusID { get; set; }
        public Nullable<DateTime> dOrderStatusBoardDateFor_nStatus { get; set; }

        public Nullable<int> nServerHandheldVendorID { get; set; }
        public Nullable<DateTime> dServerHandheldDeliveryDate { get; set; }

        public Nullable<int> nServerHandheldStatusID { get; set; }
        public Nullable<DateTime> dServerHandheldDateFor_nStatus { get; set; }

        public Nullable<int> nSonicRadioVendorID { get; set; }
        public Nullable<DateTime> dSonicRadioDeliveryDate { get; set; }

        public Nullable<int> nSonicRadioStatusID { get; set; }
        public Nullable<DateTime> dSonicRadioDateFor_nStatus { get; set; }

        public Nullable<int> nInstallationVendorID { get; set; }
        public Nullable<DateTime> dInstallationDate { get; set; }
        public Nullable<DateTime> dInstallationEndDate { get; set; }
        public Nullable<int> nSignoffs { get; set; }
        public Nullable<int> nInstallationStatusID { get; set; }
        public Nullable<DateTime> dInstallationDateFor_nStatus { get; set; }

        public Nullable<int> nTestTransactions { get; set; }
        
        public string tNoteDesc { get; set; }

        public Nullable<int> aNoteID { get; set; }
        public string tNotesOwner { get; set; }
       

        public int nTotalRows { get; set; }

    }
    public class ActiveProjectsTypeGoliveDate
    {


        public int nProjectId { get; set; }

        public int nStoreId { get; set; }
        public int nProjectType { get; set; }

        public string tProjectType { get; set; }

        public string tStoreNumber { get; set; }
        public string tStoreDetails { get; set; }
        public Nullable<DateTime> dProjectGoliveDate { get; set; }
        public Nullable<DateTime> dProjEndDate { get; set; }
        public string tProjManager { get; set; }
        public string tStatus { get; set; }
        public string tNewVendor { get; set; }


        public string tFranchise { get; set; }
        public Nullable<decimal> cCost { get; set; }
        //public decimal cCost { get; set; }
        public Nullable<DateTime> dInstallDate { get; set; }

        public int nTotalRows { get; set; }

    }
}