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
    
    public partial class tblProjectOrderAccuracy
    {
        public int aProjectOrderAccuracyID { get; set; }
        public Nullable<int> nProjectID { get; set; }
        public Nullable<int> nVendor { get; set; }
        public Nullable<int> nStatus { get; set; }
        public Nullable<int> nBakeryPrinter { get; set; }
        public Nullable<int> nDualCupLabel { get; set; }
        public Nullable<int> nDTExpo { get; set; }
        public Nullable<int> nFCExpo { get; set; }
        public Nullable<System.DateTime> dShipDate { get; set; }
        public string tShippingCarrier { get; set; }
        public string tTrackingNumber { get; set; }
        public Nullable<System.DateTime> dDeliveryDate { get; set; }
        public Nullable<int> nStoreId { get; set; }
        public Nullable<int> nMyActiveStatus { get; set; }
        public Nullable<System.DateTime> dDateFor_nStatus { get; set; }
    }
}
