using DeploymentTool.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Dropdown : IModelParent
    {
        public int nBrandId { get; set; }
        public int nModuleId { get; set; }
        public int aDropdownId { get; set; }

        public string tModuleName { get; set; }
        public string tDropdownText { get; set; }
        public Nullable<int> nOrder { get; set; }
        public Nullable<int> nFunction { get; set; }
        public Nullable<bool> bDeleted { get; set; }
    }

    public class DropdownModule
    {
        public int aModuleId { get; set; }
        public int nBrandId { get; set; }
        public string tModuleName { get; set; }
        public string tModuleDisplayName { get; set; }
        public string tModuleGroup { get; set; }
        public bool editable { get; set; }
    }
}