using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class StoreSearchModel
    {
        public int nProjectId { get; set; }
        public string tStoreName { get; set; }
        public string tProjectName { get; set; }
        public string tStoreNumber { get; set; }
        public string tProjectType { get; set; }
        public DateTime dGoLiveDate { get; set; }
    }
}