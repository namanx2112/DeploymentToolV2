using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ReportModel
    {
        public string tReportName {  get; set; }
        public List<string> titles { get; set; }
        public List<string> headers { get; set; }
        public List<Dictionary<string, string>> data { get; set; }
    }
}