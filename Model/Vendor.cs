using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Vendor
    {
        public int aVendorId { get; set; }
        public string tVendorName { get; set; }
        public int nTechComponentID { get; set; }

        public int nBrandID { get; set; }
        public string tVendorDescription { get; set; }
        public string tVendorEmail { get; set; }
        public string tVendorAddress { get; set; }
        public string tVendorPhone { get; set; }
        public string tVendorContactPerson { get; set; }
        public string tVendorWebsite { get; set; }
        public string tVendorCountry { get; set; }
        public DateTime? tVendorEstablished { get; set; }
        public string tVendorCategory { get; set; }
        public string tVendorContact { get; set; }
        public int? nCreatedBy { get; set; }
        public int? nUpdatedBy { get; set; }
        public DateTime? dtCreatedOn { get; set; }
        public DateTime? dtUpdatedOn { get; set; }
        public bool? bDeleted { get; set; }
        public int? nPageSize { get; set; }
        public int? nPageNumber { get; set; }
    }

}