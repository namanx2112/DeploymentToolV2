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
    
    public partial class tblProjectConfig : Misc.ModelParent
    {
        public int aProjectConfigID { get; set; }
        public Nullable<int> nProjectID { get; set; }
        public Nullable<int> nStallCount { get; set; }
        public Nullable<int> nDriveThru { get; set; }
        public Nullable<int> nInsideDining { get; set; }
        public Nullable<decimal> cProjectCost { get; set; }
        public Nullable<System.DateTime> dGroundBreak { get; set; }
        public Nullable<System.DateTime> dKitchenInstall { get; set; }
        public Nullable<int> nStoreId { get; set; }
        public Nullable<int> nMyActiveStatus { get; set; }
    }
}
