using DeploymentTool.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;
using System.Web.Http.ModelBinding;

namespace DeploymentTool.Model
{
    public class NewProjectStore : ModelParent
    {
        public int nProjectType { get; set; }
        public tblProjectStakeHolder tStakeHolder { get; set; }

        public string tStoreNumber { get; set; }
        public string tAddress { get; set; }
        public int nStoreState { get; set; }
        public int nDMAID { get; set; }
        public string tDMA { get; set; }
        public string tRED { get; set; }
        public string tCM { get; set; }
        public string tANE { get; set; }
        public string tRVP { get; set; }
        public string tPrincipalPartner { get; set; }
        public DateTime dStatus { get; set; }
        public DateTime dOpenStore { get; set; }
        public int nProjectStatus { get; set; }
        [JsonPropertyName("aStoreId")]
        public int aStoreId { get; set; }
        public Nullable<int> nProjectID { get; set; }
        public string tStoreName { get; set; }
        public string tStoreAddressLine1 { get; set; }
        public string tStoreAddressLine2 { get; set; }
        public string tCity { get; set; }
        public string tStoreZip { get; set; }
        public string tStoreManager { get; set; }
        public string tPOC { get; set; }
        public string tPOCPhone { get; set; }
        public string tPOCEmail { get; set; }
        public string tGC { get; set; }
        public string tGCPhone { get; set; }
        public string tGCEMail { get; set; }
        public Nullable<int> nCreatedBy { get; set; }
        public Nullable<int> nUpdateBy { get; set; }
        public Nullable<System.DateTime> dtCreatedOn { get; set; }
        public Nullable<System.DateTime> dtUpdatedOn { get; set; }
        public Nullable<bool> bDeleted { get; set; }
        public int nBrandID { get; set; }
        public string tBillToCompany { get; set; }
        public string tBillToAddress { get; set; }
        public string tBillToCity { get; set; }
        public Nullable<int> nBillToState { get; set; }
        public string tBillToZip { get; set; }
        public string tBillToEmail { get; set; }

        public void SetValues(tblProject tProj, tblStore tStore)
        {
            this.nProjectID = tProj.aProjectID;
            this.tStoreName = tProj.tProjectName;
            this.aStoreId = (int)tStore.aStoreID;
            this.dOpenStore = (DateTime)tProj.dGoLiveDate;
            this.nProjectType = (int)tProj.nProjectType;
            if (tProj.nProjectStatus != null)
                this.nProjectStatus = (int)tProj.nProjectStatus;
            if (tProj.nDMAID != null)
                this.nDMAID = (int)tProj.nDMAID;
            this.tDMA = tProj.tDMA;
            if (tProj.nBrandID != null)
                this.nBrandID = (int)tProj.nBrandID;

            this.tStoreNumber = tStore.tStoreNumber;

            this.tStoreName = tStore.tStoreName;
            this.tStoreAddressLine1 = tStore.tStoreAddressLine1;
            this.tStoreAddressLine2 = tStore.tStoreAddressLine2;
            if (tStore.tCity != null)
                this.tCity = tStore.tCity;
            if (tStore.nStoreState != null)
                this.nStoreState = (int)tStore.nStoreState;
            this.tStoreZip = tStore.tStoreZip;
            this.tStoreManager = tStore.tStoreManager;
            this.tPOC = tStore.tPOC;
            this.tPOCPhone = tStore.tPOCPhone;
            this.tPOCEmail = tStore.tPOCEmail;
            this.tGC = tStore.tGC;
            this.tGCPhone = tStore.tGCPhone;
            this.tGCEMail = tStore.tGCEmail;
            this.tBillToCompany = tStore.tBillToCompany;
            this.tBillToAddress = tStore.tBillToAddress;
            this.tBillToCity = tStore.tBillToCity;
            this.nBillToState = tStore.nBillToState;
            this.tBillToZip = tStore.tBillToZip;
            this.tBillToEmail = tStore.tBillToEmail;
        }

        public tblProject GettblProject()
        {
            tblProject tObj = new tblProject()
            {
                aProjectID = (int)this.nProjectID,
                tProjectName = this.tStoreName,
                nStoreID = this.aStoreId,
                dGoLiveDate = this.dOpenStore,
                nProjectType = this.nProjectType,
                nProjectStatus = this.nProjectStatus,
                nDMAID = this.nDMAID,
                tDMA = this.tDMA,
                nBrandID = this.nBrandID,
                nCreatedBy = this.nCreatedBy,
                nUpdateBy = this.nUpdateBy,
                dtCreatedOn = this.dtCreatedOn,
                dtUpdatedOn = this.dtUpdatedOn,
                bDeleted = this.bDeleted
            };
            return tObj;
        }
        public tblStore GettblStores()
        {
            tblStore tObj = new tblStore()
            {
                tStoreNumber = this.tStoreNumber,
                aStoreID = this.aStoreId,
                tStoreName = this.tStoreName,
                tStoreAddressLine1 = this.tStoreAddressLine1,
                tStoreAddressLine2 = this.tStoreAddressLine2,
                tCity = this.tCity,
                nStoreState = this.nStoreState,
                tStoreZip = this.tStoreZip,
                tStoreManager = this.tStoreManager,
                tPOC = this.tPOC,
                tPOCPhone = this.tPOCPhone,
                tPOCEmail = this.tPOCEmail,
                tGC = this.tGC,
                tGCPhone = this.tGCPhone,
                tGCEmail = this.tGCEMail,
                nCreatedBy = this.nCreatedBy,
                nUpdateBy = this.nUpdateBy,
                dtCreatedOn = this.dtCreatedOn,
                dtUpdatedOn = this.dtUpdatedOn,
                bDeleted = this.bDeleted,
                tBillToCompany = this.tBillToCompany,
                tBillToAddress = this.tBillToAddress,
                tBillToCity = this.tBillToCity,
                nBillToState = this.nBillToState,
                tBillToZip = this.tBillToZip,
                tBillToEmail = this.tBillToEmail
            };
            return tObj;
        }
    }
}