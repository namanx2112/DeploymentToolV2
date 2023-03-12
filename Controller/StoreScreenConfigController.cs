using DeploymentTool.Jwt.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace DeploymentTool.Controller
{
    public class StoreScreenConfigController : ApiController
    {

        // GET api/<controller>/5
        public Dictionary<string, object> Get()
        {
            Dictionary<string, dynamic> dictResponse = new Dictionary<string, dynamic>();
            var repsone = DeploymentTool.DBHelper.CallProcedureAndConvertToJson("sproc_get_StoreSearchConfig");
            foreach(var dic in repsone)
            {
                dictResponse.Add(dic.Key, JsonConvert.DeserializeObject<dynamic>(dic.Value.ToString()));

            }
            return dictResponse;
        }
       
    }
}