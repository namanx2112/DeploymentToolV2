using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class ReportGenerator
    {
    }

    public class ReportFolder
    {
        public int aFolderId { get; set; }
        public int nBrandId { get; set; }
        public string tFolderName { get; set; } 
        public string tFolderDescription { get; set; }
        public Nullable<int> nFolderType { get; set; }
        public DateTime dCreatedOn { get; set; }
        public string tCreatedBy { get; set; }
        public tblReportFolder GetTblReportFolder()
        {
            return new tblReportFolder()
            {
                aReportFolderID = this.aFolderId,
                nBrandId = this.nBrandId,
                tFolderName = this.tFolderName,
                tFolderDescription = this.tFolderDescription,
                nFolderType = this.nFolderType
            };
        }

    }

    public class ReportInfo
    {
        public int aReportId { get; set; }
        public int nFolderId { get; set; }
        public string tReportName { get; set; }
        public DateTime dCreatedOn { get; set; }
        public string tCreatedBy { get; set; }
    }
    public class ReportFieldAndOperatorType
    {
        public int aFieldTypeOperatorRelID { get; set; }
        public int nFieldTypeID { get; set; }
        public string tName { get; set; }
        public string tOperator { get; set; }
    }
    public class ReportFields
    {
        public int aReportFieldID { get; set; }
        public int nReportFieldGroupID { get; set; }
        public string tGroupName { get; set; }
        public int nFieldTypeID { get; set; }
        public string tReportFieldName { get; set; }

        public Nullable<bool> bAvailableForFilter { get; set; }
        public Nullable<bool> bAvailableForColumn { get; set; }
        public Nullable<bool> bAvailableForSort { get; set; }

        public string tTableName { get; set; }

        public string tColumnName { get; set; }

        public string tPrimaryColumn { get; set; }
        public string tRelColumn { get; set; }        
        public Nullable<int> nBrandID { get; set; }
    }
}