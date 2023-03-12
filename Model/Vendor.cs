using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int TechComponentID { get; set; }
        public string VendorDescription { get; set; }
        public string VendorEmail { get; set; }
        public string VendorAddress { get; set; }
        public string VendorPhone { get; set; }
        public string VendorContactPerson { get; set; }
        public string VendorWebsite { get; set; }
        public string VendorCountry { get; set; }
        public DateTime? VendorEstablished { get; set; }
        public string VendorCategory { get; set; }
        public string VendorContact { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Deleted { get; set; }
    }

}