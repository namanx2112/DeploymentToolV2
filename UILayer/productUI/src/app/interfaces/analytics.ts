export interface Analytics {
}

export interface ProjectPortfolio{
    nProjectId: number,
    nStoreId: number,
    tProjectType: string,
    nProjectType: number,
    store: ProjectPortfolioStore,
    networking: ProjectPortfolioItems,
    pos: ProjectPortfolioItems,
    audio: ProjectPortfolioItems,
    exteriormenu: ProjectPortfolioItems,
    paymentsystem: ProjectPortfolioItems,
    interiormenu: ProjectPortfolioItems,
    sonicradio: ProjectPortfolioItems,
    installation: ProjectPortfolioItems,
    notes: ProjectPorfolioNotes
}

export interface ProjectPortfolioStore{
    tStoreNumber: string,
    tStoreDetails: string,
    dtGoliveDate: Date,
    tProjectManager: string,
    tProjectType: string,
    cCost: number,
    tFranchise: string
}

export interface ProjectPortfolioItems{
    tVendor: string,
    dtDate: Date,
    tStatus: string
}

export interface ProjectPorfolioNotes{
    tNotesOwner: string,
    tNotesDesc: string
}

export interface ReportModel{
    titles: string[]
    headers: string[],
    data: any
}