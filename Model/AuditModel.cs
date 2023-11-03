using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class AuditFields
    {
        public int nUserId { get; set; }
        public string tFieldName { get; set; }
        public string tPreviousValue { get; set; }
        public string tNewValue { get; set; }

        public string tChangeNote { get; set; }
        public DateTime dDate { get; set; }
    }

    public class AuditModel
    {
        public string tComponentName { get; set; }
        public int nTotalCount { get; set; }

        public List<AuditFields> lItems { get; set; }
    }
}