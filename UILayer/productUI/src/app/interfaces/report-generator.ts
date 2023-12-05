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
    aFieldID: number,
    tGroupName: string,
    nFieldTypeID: number,
    tFieldName: string,
    nAvailableFlag: number,
    tTableName: string,
    tColumnName: string,
    tPrimaryColumn: string,
    tRelColumn: string,
    nBrandID: number,
    tConstraint: string
}

export interface ReportFieldAndOperatorType {
    aOperatorID: number,
    nFieldTypeID: number,
    tName: string,
    tOperator: string
}


export interface ReportEditorModel {
    aReportId: number,
    tReportName: string,
    tReportDescription: string,
    conditions: ReportCondtion[],
    isValid: boolean
}

export interface ReportCondtion {
    nConditionID:number,
    nRelatedID: number,
    
    nAndOr: number,
    nFieldID: number,
    nOperatorID: number,
    nValue: number,
    nArrValues: any[]
    tValue: string,
    dValue?: Date,
    cValue: number
}