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
    
    public partial class tblSupportTicket: Misc.ModelParent
    {
        public int aTicketId { get; set; }
        public Nullable<int> nPriority { get; set; }
        public string tContent { get; set; }
        public Nullable<int> nFileSie { get; set; }
        public string fBase64 { get; set; }
        public Nullable<int> nCreatedBy { get; set; }
        public Nullable<System.DateTime> dtCreatedOn { get; set; }
    }
}
