using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class HistoricalProjectModel
    {
        public int nProjectId { get; set; }
        public int nProjectType { get; set; }
        public string tStoreNumber { get; set; }
        public Nullable<DateTime> dProjectGoliveDate { get; set; }
        public string tProjectType { get; set; }
        public Nullable<DateTime> dProjEndDate { get; set; }
        public string tProjManager { get; set; }        
        public string tVendor { get; set; }
    }
}