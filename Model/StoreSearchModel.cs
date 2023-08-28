using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class StoreSearchModel
    {
        public int nStoreId { get; set; }
        public int nBrandId { get; set; }
        //public int nProjectId { get; set; }
        public string tStoreName { get; set; }

        public string tAddress { get; set; }
        //public string tProjectName { get; set; }
        public string tStoreNumber { get; set; }
        //public string tProjectType { get; set; }
        //public DateTime dGoLiveDate { get; set; }

        public string tProjectsInfo
        {
            set
            {
                if (value != null)
                {
                    this._nProjectIds = new List<ProjectInfo>();
                    string[] pArr = value.Split(',');
                    foreach (string p in pArr)
                    {
                        string[] tArr = p.Split('_');
                        if (tArr.Length > 1)
                        {
                            int tProjId;
                            int tProjType;
                            DateTime dGoLiveDate;
                            if (int.TryParse(tArr[0], out tProjId) && int.TryParse(tArr[1], out tProjType) && DateTime.TryParse(tArr[2], out dGoLiveDate))
                            {
                                _nProjectIds.Add(new ProjectInfo()
                                {
                                    nProjectId = tProjId,
                                    nProjectType = tProjType,
                                    dGoLiveDate = dGoLiveDate
                                });
                            }
                        }
                    }
                }
            }
        }

        List<ProjectInfo> _nProjectIds;

        public List<ProjectInfo> lstProjectsInfo
        {
            get
            {
                return _nProjectIds;
            }
        }
    }

    public class ProjectInfo
    {
        public int nProjectId { get; set; }
        public int nProjectType { get; set; }
        public DateTime dGoLiveDate { get; set; }
    }
}