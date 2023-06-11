using DeploymentTool.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;
using System.Web.Http.ModelBinding;

namespace DeploymentTool.Model
{
    public class NewProjectStore: ModelParent
    {
        public int nProjectType { get; set; }

        public int nStoreId { get; set; }
        public string tStoreNumber { get; set; }
        public string tAddress { get; set; }
        public int nState { get; set; }
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
        public int aProjectStoreID { get; set; }
        public Nullable<int> nProjectID { get; set; }
        public string tStoreName { get; set; }
        public string tStoreAddressLine1 { get; set; }
        public string tStoreAddressLine2 { get; set; }
        public int nCity { get; set; }
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

        public tblProject GettblProject()
        {
            tblProject tObj = new tblProject()
            {
                aProjectID = (int)this.nProjectID,
                tProjectName = this.tStoreName,
                nStoreID = this.nStoreId,
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

        public tblStore GettblStore()
        {
            tblStore tObj = new tblStore() {
                aStoreID = this.nStoreId,
                tStoreNumber = this.tStoreNumber,
                dtCreatedOn= this.dtCreatedOn,
                nCreatedBy = this.nCreatedBy
            };
            return tObj;
        }

        public tblProjectStore GettblProjectStores()
        {
            tblProjectStore tObj = new tblProjectStore()
            {
                aProjectStoreID = this.aProjectStoreID,
                nProjectID = this.nProjectID,
                tStoreName = this.tStoreName,
                tStoreAddressLine1 = this.tStoreAddressLine1,
                tStoreAddressLine2 = this.tStoreAddressLine2,
                nCity = this.nCity,
                nStoreState = this.nState,
                tStoreZip = this.tStoreZip,
                tStoreManager = this.tStoreManager,
                tPOC = this.tPOC,
                tPOCPhone = this.tPOCPhone,
                tPOCEmail = this.tPOCEmail,
                tGC = this.tGC,
                tGCPhone = this.tGCPhone,
                tGCEMail = this.tGCEMail,
                nCreatedBy = this.nCreatedBy,
                nUpdateBy = this.nUpdateBy,
                dtCreatedOn = this.dtCreatedOn,
                dtUpdatedOn = this.dtUpdatedOn,
                bDeleted = this.bDeleted
            };
            return tObj;
        }
    }
}