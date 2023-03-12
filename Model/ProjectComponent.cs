using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ProjectComponent
    {
        public int ProjectComponentId { get; set; }
        public int? ProjectId { get; set; }
        public int? TechComponentId { get; set; }
        public string VendorId { get; set; }
        public int? Status { get; set; }
        public string StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EndExpectedDate { get; set; }
        public DateTime? SignOffStatus { get; set; }
        public string Comments { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Deleted { get; set; }
    }

}