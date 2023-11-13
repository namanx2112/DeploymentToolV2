export interface FieldAccess {
    tTechCompName: string,
    tFieldName: string,
    nAccessVal: number
}

export interface RolloutProjects{
    aProjectsRolloutID: number,
    tProjectsRolloutName: string,
    tProjectsRolloutDescription: string,
    nNumberOfStore: string,
    nBrandID: number,
    nMyActiveStatus: number,
    nStatus: number,
    tEstimateInstallTImePerStore: string,
    dtStartDate: Date,
    dtEndDate: Date,
    dDateFor_nStatus: Date
}