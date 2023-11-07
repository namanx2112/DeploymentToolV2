using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ProjectRolloutModel
    {
        public int aRolloutProjectId { get; set; }
        public string tProjectName { get; set; }
        public int nBrandId { get; set; }
        public int nTechComponent { get; set; }
        public int nEquipmentVendor { get; set; }
        public int nInstallationVendor { get; set; }
        public DateTime dStartDate { get; set; }
        public DateTime dEndDate { get; set; }
    }
}