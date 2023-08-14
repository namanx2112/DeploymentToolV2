using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ProjectPortfolio
    {        
        public int nStoreId {  get; set; }
        public int nProjectType { get; set; }
        public string tProjectType { get; set; }
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
        public Nullable<DateTime> dtDate { get; set; }
        public string tStatus { get; set; } 
    }

    public class ProjectPorfolioNotes
    {
        
        public int aNoteID { get; set; }
        public int nProjectId { get; set; }
        public string tNotesOwner { get; set; }
        public string tNotesDesc { get; set; }
    }

    public class ActivePortFolioProjectsModel
    {
        

        public int nProjectId { get; set; }

        public int nStoreId { get; set; }
        public int nProjectType { get; set; }
        
        public string tProjectType { get; set; }

        public string tStoreNumber { get; set; }
        public string tStoreDetails { get; set; }
        public Nullable<DateTime> dProjectGoliveDate { get; set; }
        public Nullable<DateTime> dProjEndDate { get; set; }
        public string tProjManager { get; set; }
        public string tStatus { get; set; }
        public string tNewVendor { get; set; }


        public string tFranchise { get; set; }
        public Nullable<decimal> cCost { get; set; }
        //public decimal cCost { get; set; }

    }
}