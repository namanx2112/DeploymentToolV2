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
    
    public partial class tblProjectNetworkSwitch : Misc.IModelParent
    {
        public int aProjectNetworkSwtichID { get; set; }
        public Nullable<int> nProjectID { get; set; }
        public Nullable<int> nVendor { get; set; }
        public Nullable<int> nStatus { get; set; }
        public Nullable<int> nShipmentToVendor { get; set; }
        public Nullable<int> nSetupStatus { get; set; }
        public string tNewSerialNumber { get; set; }
        public string tOldSerialNumber { get; set; }
        public Nullable<int> nStoreId { get; set; }
        public Nullable<int> nMyActiveStatus { get; set; }
        public Nullable<System.DateTime> dDateFor_nStatus { get; set; }
    }
}
