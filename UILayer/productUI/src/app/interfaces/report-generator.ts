export interface ReportGenerator {
}

export enum ConditionOperatorIds {
    Equals = 1, NotEquals = 2, Greater = 3, Less = 4, GreaterEquals = 5, LessEquals = 6, In = 7, NotIn = 8, Contains = 9, NotContains = 10,
    StartsWith = 11, EndsWith = 12, IsEmpty = 13, IsNotEmpty = 14
}

export interface ReportFolder {
    aFolderId: number,
    nBrandId: number,
    tFolderName: string,
    tFolderDescription: string,
    nFolderType: number,
    dCreatedOn: Date,
    tCreatedBy: string,
    visible: number | -1,
    canEdit: boolean
}

export interface ReportInfo {
    aReportId: number,
    nBrandId: number,
    nFolderId: number,
    tReportName: string,
    dCreatedOn: Date,
    tCreatedBy: string,
    visible: number | 1,
    canEdit: boolean
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

export interface GroupByFields {
    tGroupName: string,
    items: ReportField[]
}

export interface DisplayColumn {
    aDisplayColumnsID: number,
    nRelatedID: number,
    nRelatedType: number,
    nFieldID: number,
    tFieldName: string,
    nOrder: number
}

export interface SortColumns {
    aSortColumnsID: number,
    nRelatedID: number,
    nRelatedType: number,
    nFieldID: number,
    tFieldName: string,
    nOrder: number
}

export interface ReportFieldAndOperatorType {
    aOperatorID: number,
    nFieldTypeID: number,
    tName: string,
    tOperator: string
}


export interface ReportEditorModel {
    aReportId: number,
    nFolderId: number,
    tBrandID: number,
    tReportName: string,
    tReportDescription: string,
    conditions: ReportCondtion[],
    spClmn: DisplayColumn[],
    srtClmn: SortColumns[]
}

export interface ReportCondtion {
    nConditionID: number,
    nRelatedID: number,
    nAndOr: number,
    nFieldID: number,
    field: any,
    nFieldTypeID: number,
    nOperatorID: number,
    nValue: number,
    nArrValues: any[],
    operators: any[],
    ddOptions: any[],
    tValue: string,
    dValue?: Date,
    cValue: number
}