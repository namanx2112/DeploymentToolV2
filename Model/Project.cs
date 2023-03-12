using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int StoreID { get; set; }
        public string ProjectType { get; set; }
        public int ProjectManager { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public DateTime ProjectExpectedEndDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Deleted { get; set; }
    }

}