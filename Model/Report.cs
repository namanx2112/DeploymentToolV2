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
        public string tParam1 { get; set; }
        public string tParam2 { get; set; }
        public string tParam3 { get; set; }
        public string tParam4 { get; set; }
        public string tParam5 { get; set; }
    }
    public class ReportModel
    {
        public string tReportName { get; set; }
        public DataTable reportTable { get; set; }
    }

    public class DashboardRequest
    {
        public string tProjectTypes { get; set; }
        public int nBrandId { get; set; }
        public Nullable<DateTime> dStart { get; set; }
        public Nullable<DateTime> dEnd { get; set; }
    }

    public class DahboardTile
    {
        public int reportId { get; set; }

        Random tRandom = new Random(4);

        int _size;
        public int size
        {
            get
            {
                return tRandom.Next(1, 4);
            }
            set
            {
                _size = value;
            }
        }
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

        string[] _insArray;
        char[] separators = new char[] { ',' };
        int[] _inschartArray;
        public int[] chartValues
        {
            get
            {
                return _inschartArray;
            }
        }
        public string chartValuesTemp
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _inschartArray = value.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray(); ;
            }
        }
        public string chartLabelsTemp
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _insArray = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public string[] chartLabels
        {
            get
            {
                return _insArray;
            }
        }

        public string tProjectIDs { get; set; }
    }

    public enum DashboardTileType
    {
        Text, TextWithCompare, Chart
    }
}