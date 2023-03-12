using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Brand
    {
        public int? aBrandId { get; set; }
        public string tBrandName { get; set; }
        public string tBrandDescription { get; set; }
        public string tBrandWebsite { get; set; }
        public string tBrandCountry { get; set; }
        public DateTime tBrandEstablished { get; set; }
        public string tBrandCategory { get; set; }
        public string tBrandContact { get; set; }
        public int? nBrandLogoAttachmentID { get; set; }
        public int? nCreatedBy { get; set; }
        public int? nUpdateBy { get; set; }
        public DateTime dtCreatedOn { get; set; }
        public DateTime dtUpdatedOn { get; set; }
        public bool bDeleted { get; set; }
        public int? nPageSize { get; set; }
        public int? nPageNumber { get; set; }
        public int? nTotalCount { get; set; }       


    }
}