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

    public class DahboardTile
    {
        public int reportId { get; set; }
        public string title { get; set; }
        public int count { get; set; }
        public Nullable<int> compareWith { get; set; }

        public string compareWithText { get; set; }

        public DashboardTileType type { get; set; }
        public string chartType { get; set; }
        public int[] chartValues { get; set; }
        public string[] chartLabels { get; set; }
    }

    public enum DashboardTileType
    {
        Text, Chart
    }
}