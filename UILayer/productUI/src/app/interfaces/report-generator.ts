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


export interface ReportEditorModel {
    aReportId: number,
    tReportName: string,
    tReportDescription: string
}