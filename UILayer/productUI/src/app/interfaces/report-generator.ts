export interface ReportGenerator {
}

export interface ReportFolder {
    aFolderId: number,
    nBrandId: number,
    tFolderName: string,
    tFolderDescription: string,
    nFolderType: number,
    dCreatedOn: Date,
    tCreatedBy: string,
    visible: number | -1
}

export interface ReportInfo {
    aReportId: number,
    nBrandId: number,
    nFolderId: number,
    tReportName: string,
    dCreatedOn: Date,
    tCreatedBy: string,
    visible: number | 1
}

export interface ReportField {
    aReportFieldID: number,
    nReportFieldGroupID: number,
    tGroupName: string,
    nFieldTypeID: number,
    tReportFieldName: string,
    bAvailableForFilter: boolean,
    bAvailableForColumn: boolean,
    bAvailableForSort: boolean,
    tTableName: string,
    tColumnName: string,
    tPrimaryColumn: string,
    tRelColumn: string,
    nBrandID: number
}

export interface ReportFieldAndOperatorType {
    aFieldTypeOperatorRelID: number,
    nFieldTypeID: number,
    tName: string,
    tOperator: string
}


export interface ReportEditorModel {
    aReportId: number,
    tReportName: string,
    tReportDescription: string
}