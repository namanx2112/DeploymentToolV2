using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ReportGenerator
    {
    }

    public class ReportFolder
    {
        public int aFolderId { get; set; }
        public string tFolderName { get; set; } 
        public string tFolderDescription { get; set; }
        public DateTime dCreatedOn { get; set; }
        public string tCreatedBy { get; set; }

    }

    public class ReportInfo
    {
        public int aReportId { get; set; }
        public int nFolderId { get; set; }
        public string tReportName { get; set; }
        public DateTime dCreatedOn { get; set; }
        public string tCreatedBy { get; set; }
    }
}