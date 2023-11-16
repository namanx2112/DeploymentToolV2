using DeploymentTool.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int StoreID { get; set; }
        public string ProjectType { get; set; }
        public int ProjectManager { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public DateTime ProjectExpectedEndDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Deleted { get; set; }
    }

    public partial class ProjectsRolloutModel : Misc.IModelParent
    {
        public int aProjectsRolloutID { get; set; }
        public string tProjectsRolloutName { get; set; }
        public string tProjectsRolloutDescription { get; set; }
        public Nullable<int> nBrandID { get; set; }
        public Nullable<int> nNumberOfStore { get; set; }
        public Nullable<int> nMyActiveStatus { get; set; }
        public Nullable<int> nStatus { get; set; }
        public Nullable<System.DateTime> dtStartDate { get; set; }
        public Nullable<System.DateTime> dtEndDate { get; set; }
        public string tEstimateInstallTImePerStore { get; set; }
        public Nullable<int> nCreatedBy { get; set; }
        public Nullable<int> nUpdateBy { get; set; }
        public Nullable<System.DateTime> dtCreatedOn { get; set; }
        public Nullable<System.DateTime> dtUpdatedOn { get; set; }
        public Nullable<bool> bDeleted { get; set; }
        public Nullable<System.DateTime> dDateFor_nStatus { get; set; }
        public Nullable<decimal> cHardwareCost { get; set; }
        public Nullable<decimal> cDeploymentCost { get; set; }

        public List<RolloutItem> uploadingRows { get; set; }

        public tblProjectsRollout GetTblProjectsRollout()
        {
            return new tblProjectsRollout()
            {
                aProjectsRolloutID = this.aProjectsRolloutID,
                tProjectsRolloutName = this.tProjectsRolloutName,
                tProjectsRolloutDescription = this.tProjectsRolloutDescription,
                cHardwareCost = this.cHardwareCost,
                cDeploymentCost = this.cDeploymentCost,
                nBrandID = this.nBrandID,
                nNumberOfStore = this.nNumberOfStore,
                nMyActiveStatus = this.nMyActiveStatus,
                nStatus = this.nStatus,
                dtStartDate = this.dtStartDate,
                dtEndDate = this.dtEndDate,
                tEstimateInstallTImePerStore = this.tEstimateInstallTImePerStore,
                nCreatedBy = this.nCreatedBy,
                nUpdateBy = this.nUpdateBy,
                dtCreatedOn = this.dtCreatedOn,
                dtUpdatedOn = this.dtUpdatedOn,
                bDeleted = this.bDeleted,
                dDateFor_nStatus = this.dDateFor_nStatus
            };
        }
    }

    public class RolloutItem
    {
        public List<dynamic> items { get; set; }
        public string name { get; set; }

        public ProjectType type { get; set; }
    }

    public class ProjectGlimpse
    {
        public int aProjectsRolloutID { get; set; }
        public string tProjectsRolloutName { get; set; }

        public Nullable<int> nProjectStatus { get; set; }
    }

}