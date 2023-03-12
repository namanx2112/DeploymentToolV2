using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class TechComponent
    {
        public int aTechComponentId { get; set; }
        public string tTechComponentName { get; set; }
        public int? nBrandID { get; set; }
        public string tTechComponentDescription { get; set; }
        public string tComponentType { get; set; }
        public int? nCreatedBy { get; set; }
        public int? nUpdateBy { get; set; }
        public DateTime? dtCreatedOn { get; set; }
        public DateTime? dtUpdatedOn { get; set; }
        public bool? bDeleted { get; set; }
    }

}