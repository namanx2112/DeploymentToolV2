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
    
    public partial class tblProjectInstallation : Misc.ModelParent
    {
        public int aProjectInstallationID { get; set; }
        public Nullable<int> nProjectID { get; set; }
        public Nullable<int> nVendor { get; set; }
        public string tLeadTech { get; set; }
        public Nullable<System.DateTime> dInstallDate { get; set; }
        public Nullable<System.DateTime> dInstallEnd { get; set; }
        public Nullable<int> nStatus { get; set; }
        public Nullable<int> nSignoffs { get; set; }
        public Nullable<int> nTestTransactions { get; set; }
        public Nullable<int> nProjectStatus { get; set; }
        public Nullable<decimal> cCost { get; set; }
        public Nullable<int> nStoreId { get; set; }
        public Nullable<int> nMyActiveStatus { get; set; }
    }
}
