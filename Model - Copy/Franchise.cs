using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Franchise
    {
        public int aFranchiseId { get; set; }
        public string tFranchiseName { get; set; }
        public int nBrandId { get; set; }
        public string tFranchiseDescription { get; set; }
        public string tFranchiseLocation { get; set; }
        public DateTime dFranchiseEstablished { get; set; }
        public string tFranchiseContact { get; set; }
        public string tFranchiseOwner { get; set; }
        public string tFranchiseEmail { get; set; }
        public string tFranchisePhone { get; set; }
        public string tFranchiseAddress { get; set; }
        public int nFranchiseEmployeeCount { get; set; }
        public int nFranchiseRevenue { get; set; }
        public int nCreatedBy { get; set; }
        public int nUpdateBy { get; set; }
        public int nUserID { get; set; }
        public DateTime dtCreatedOn { get; set; }
        public DateTime dtUpdatedOn { get; set; }
        public bool bDeleted { get; set; }
        public int nPageSize { get; set; }
        public int nPageNumber { get; set; }
        public int nTotalCount { get; set; }
    }
}
