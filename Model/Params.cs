using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Parameters
    {
        public string parameterName { get; set; }
        public object parameterValue { get; set; }

        public Parameters(string paramName,object paramValue)
        {
            parameterName = paramName;
            parameterValue = paramValue;
        }
    }
    
}