export interface ReportGenerator {
}

export interface ReportFolder {
    aFolderId: number,
    tFolderName: string,
    tFolderDescription: string,
    dCreatedOn: Date,
    tCreatedBy: string
}

export interface ReportInfo {
    aReportId: number,
    nFolderId: number,
    tReportName: string,
    dCreatedOn: Date,
    tCreatedBy: string
}


export interface ReportEditorModel {
    aReportId: number,
    tReportName: string,
    tReportDescription: string
}