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
        public bool canEdit { get; set; }
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
    public class ReportDetails
    {
        public int aReportId { get; set; }
        public int nFolderId { get; set; }
        public string tReportName { get; set; }
        public string tReportDescription { get; set; }
        public int nBrandId { get; set; }
        public string tBrandID { get; set; }
        public List<tblFilterCondition> conditions { get; set; }
        public List<tblDisplayColumn> spClmn { get; set; }
        public List<tblSortColumn> srtClmn { get; set; }

        public DateTime dCreatedOn { get; set; }
        public string tCreatedBy { get; set; }

        public tblReport GetTblReport()
        {
            return new tblReport()
            {
                aReportID = this.aReportId,
                // tBrandID =","+ this.nBrandId.t+",",
                tName = this.tReportName,
                tReportDescription = this.tReportDescription,
                nReportFolderID = this.nFolderId
            };
        }



    }
    //public class ReportConditions
    //{
    //    public int nConditionID { get; set; }
    //    public int nRelatedID { get; set; }
    //    public int nRelatedType { get; set; }
        
    //    public int nFieldID { get; set; }
    //    public int nFieldTypeID { get; set; }
    //    public int nAndOr { get; set; }

    //    public int nOperatorID { get; set; }
    //    public int nValue { get; set; }
        
    //    public string tValue { get; set; }

    //    public DateTime dValue { get; set; }

    //    public Decimal cValue { get; set; }

    //}
    
    public class ReportShareModel
    {
        public List<int> reportIds { get; set; }
        public List<int> brands { get; set; }
        public List<int> roles { get; set; }
    }

public class ReportInfo
    {
        public int aReportId { get; set; }
        public int nFolderId { get; set; }
        public string tReportName { get; set; }
        public string tReportDescription { get; set; }
        
        public List<ReportFields> conditions { get; set; }
        public DateTime dCreatedOn { get; set; }
        public string tCreatedBy { get; set; }
        public bool canEdit { get; set; }
    }
    public class ReportFieldAndOperatorType
    {
        public int aOperatorID { get; set; }
        public int nFieldTypeID { get; set; }
        public string tName { get; set; }
        public string tOperator { get; set; }
    }
    public class ReportFields
    {
        public int aFieldID { get; set; }
        public string tGroupName { get; set; }
        public int nFieldTypeID { get; set; }
        public string tFieldName { get; set; }

        public int nAvailableFlag { get; set; }
        public string tConstraint { get; set; }

        public string tTableName { get; set; }

        public string tColumnName { get; set; }

        public string tPrimaryColumn { get; set; }
        public string tRelColumn { get; set; }
        public Nullable<int> nBrandID { get; set; }
    }
}