//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeploymentTool
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblBrand : Misc.ModelParent
    {
        public int aBrandId { get; set; }
        public string tBrandName { get; set; }
        public string tBrandDomain { get; set; }
        public string tBrandAddressLine1 { get; set; }
        public string tBrandAddressLine2 { get; set; }
        public string tBrandCity { get; set; }
        public Nullable<int> nBrandState { get; set; }
        public Nullable<int> nBrandCountry { get; set; }
        public string tBrandZipCode { get; set; }
        public Nullable<int> nBrandLogoAttachmentID { get; set; }
        public Nullable<int> nCreatedBy { get; set; }
        public Nullable<int> nUpdateBy { get; set; }
        public Nullable<System.DateTime> dtCreatedOn { get; set; }
        public Nullable<System.DateTime> dtUpdatedOn { get; set; }
        public Nullable<bool> bDeleted { get; set; }
        public byte[] BrandFile { get; set; }
    }
}
