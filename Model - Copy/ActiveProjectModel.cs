﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ActiveProjectModel
    {
        public int nProjectId { get; set; }

        public string tStoreNumber { get; set; }
        public string tProjectType { get; set; }
        public string tStatus { get; set; }
        public string tPrevProjManager { get; set; }
        public string tProjManager { get; set; }
        public Nullable<DateTime> dProjectGoliveDate { get; set; }
        public Nullable<DateTime> dProjEndDate { get; set; }
        public string tOldVendor { get; set; }
        public string tNewVendor { get; set; }
    }
}