using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ProjectPortfolio
    {        
        public int nProjectType { get; set; }
        public int nProjectId {  get; set; }
        public ProjectPortfolioStore store { get; set; }
        public ProjectPortfolioItems networking { get; set; }
        public ProjectPortfolioItems pos { get; set; }
        public ProjectPortfolioItems audio { get; set; }
        public ProjectPortfolioItems exteriormenu { get; set; }
        public ProjectPortfolioItems paymentsystem { get; set; }
        public ProjectPortfolioItems interiormenu { get; set; }
        public ProjectPortfolioItems sonicradio { get; set; }
        public ProjectPortfolioItems installation { get; set; }
        public List<ProjectPorfolioNotes> notes { get; set; }
    }

    public class ProjectPortfolioStore
    {
        public string tStoreDetails { get; set; }
        public DateTime dtGoliveDate { get; set; }
        public string tProjectManager { get; set; }
        public string tProjectType { get; set; }
        public string tFranchise { get; set; }
        public decimal cCost { get; set; }
    }

    public class ProjectPortfolioItems
    {
        public string tVendor { get; set; }
        public DateTime dtDate { get; set; }
        public string tStatus { get; set; } 
    }

    public class ProjectPorfolioNotes
    {
        public string tNotesOwner { get; set; }
        public string tNotesDesc { get; set; }
    }
}