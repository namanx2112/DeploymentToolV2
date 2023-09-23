using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ReportRequest
    {
        public int reportId { get; set; }
        public string tParam { get; set; }
    }
    public class ReportModel
    {
        public string tReportName { get; set; }
        public DataTable reportTable { get; set; }
    }

    public class DahboardTile
    {
        public int reportId { get; set; }
        public string title { get; set; }
        public int count { get; set; }
        public Nullable<int> compareWith { get; set; }

        string _compareWithText;

        public string compareWithText
        {
            get
            {
                return _compareWithText;
            }
            set
            {
                _compareWithText = value;
                if (string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(chartType))
                        type = DashboardTileType.Text;
                    else
                        type = DashboardTileType.Chart;
                }
                else
                    type = DashboardTileType.TextWithCompare;
            }
        }

        public DashboardTileType type { get; set; }
        public string chartType { get; set; }
        public int[] chartValues { get; set; }
        public string[] chartLabels { get; set; }
    }

    public enum DashboardTileType
    {
        Text, TextWithCompare, Chart
    }
}