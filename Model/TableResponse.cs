using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class TableResponse
    {
        public int nTotalRows { get; set; }
        public dynamic response { get; set; }
    }

    public interface ITableActualResponse
    {

    }
}