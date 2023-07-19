using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class HistoricalProjectModel
    {
        public int nProjectId { get; set; }
        public string tStoreNo { get; set; }
        public DateTime dProjectGoliveDate { get; set; }
        public string tProjectType { get; set; }
        public DateTime dProjEndDate { get; set; }
        public string tProjManager { get; set; }        
        public string tVendor { get; set; }
    }
}