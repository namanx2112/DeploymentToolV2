﻿using DeploymentTool.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Dropdown : ModelParent
    {
        public int nBrandId { get; set; }
        public string tModuleName { get; set; }
        public int aDropdownId { get; set; }
        public string tDropdownText { get; set; }
        public Nullable<bool> bDeleted { get; set; }
    }
}