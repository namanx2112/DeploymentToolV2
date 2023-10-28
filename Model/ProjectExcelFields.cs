using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ProjectExcelFields
    {
        public int nBrandId {  get; set; }
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