using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DeploymentTool.Model;
using System.Dynamic;

namespace DeploymentTool.Controller
{
    public class TableController : ApiController
    {
        
        // POST api/<controller>
        [HttpPost]
        public List<dynamic> GetTableData ([FromBody] dynamic value)
        {
            string jsonString = JsonConvert.SerializeObject(value);
            Parameters pram = new Parameters("tSerchJson",jsonString);           
            List<Parameters> lsPram = new List<Parameters>();
            lsPram.Add(pram);
            DataTable dt = DBHelper.ExecuteStoredProcedure("sproc_GetStore", lsPram);            
            var dynamicList = dt.AsEnumerable()
                            .Select(row =>
                            {
                                var json = row.Field<string>("tJsonObject");
                                var obj = JsonConvert.DeserializeObject<dynamic>(json);
                                obj.nStoreID = row.Field<int>("nStoreID");
                                return obj;
                            })
                            .ToList();
            return dynamicList;
        }       

       
    }
}