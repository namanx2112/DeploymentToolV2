using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class HistoricalProjectModel
    {
        public int nProjectId { get; set; }
        public int nProjectType { get; set; }
        public string tStoreNumber { get; set; }
        [Column(TypeName = "date")]
        public Nullable<DateTime> dProjectGoliveDate { get; set; }
        public string tProjectType { get; set; }
        [Column(TypeName = "date")]
        public Nullable<DateTime> dProjEndDate { get; set; }
        public string tProjManager { get; set; }        
        public string tVendor { get; set; }
    }
}