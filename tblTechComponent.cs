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
    
    public partial class tblTechComponent
    {
        public int aTechComponentId { get; set; }
        public string tTechComponentName { get; set; }
        public Nullable<int> nBrandID { get; set; }
        public string tTechComponentDescription { get; set; }
        public string tComponentType { get; set; }
        public Nullable<int> nCreatedBy { get; set; }
        public Nullable<int> nUpdateBy { get; set; }
        public Nullable<System.DateTime> dtCreatedOn { get; set; }
        public Nullable<System.DateTime> dtUpdatedOn { get; set; }
        public Nullable<bool> bDeleted { get; set; }
    }
}
