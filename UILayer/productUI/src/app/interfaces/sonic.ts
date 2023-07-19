export interface Sonic {
}


export interface StoreContact {
    aStoreId: number,
    tStoreName: string,
    tStoreAddressLine1: string,
    tStoreAddressLine2: string,
    tCity: string,
    nStoreState: string
    tStoreZip: string,
    tStoreManager: string,
    tPOC: string,
    tPOCPhone: string,
    tPOCEmail: string,
    tGC: string,
    tGCPhone: string,
    tGCEMail: string,
    tBillToCompany: string,
    tBillToAddress: string,
    tBillToCity: string,
    nBillToState: number,
    tBillToZip: string,
    tBillToEmail: string
}
export interface StoreConfiguration {
    aProjectConfigID: number,
    nProjectID: number,
    nStallCount: number,
    nDriveThru: number,
    nInsideDining: number,
    nGroundBreak: number,
    nKitchenInstall: Date,
    cProjectCost: number
}
export interface StoreStackholders {
    aProjectStakeHolderID: number,
    nProjectID: number,
    nFranchisee: number,
    tRVP: string,
    tFBC: string,
    nCD: number,
    nITPM: number,
    tRED: string,
    tCM: string,
    tAandE: string,
    tPrincipalPartner: string
}
export interface StoreNetworkings {
    aProjectNetworkingID: number,
    nProjectID: number,
    nVendor: number,
    nPrimaryStatus: number,
    dPrimaryDate: Date,
    nPrimaryType: number,
    nBackupStatus: number,
    dBackupDate: Date,
    nBackupType: number,
    nTempStatus: number,
    dTempDate: Date,
    nTempType: number
}
export interface StorePOS {
    aProjectPOSID: number,
    nProjectID: number,
    nVendor: number,
    dDeliveryDate: Date,
    dConfigDate: Date,
    dSupportDate: Date,
    nStatus: number
    nPaperworkStatus: number,
    cCost: number
}
export interface StoreAudio {
    aProjectAudioID: number,
    nProjectID: number,
    nVendor: number,
    nStatus: number,
    nConfiguration: number,
    dDeliveryDate: Date
    nLoopStatus: number,
    nLoopType: number,
    dLoopDeliveryDate: Date,
    cCost: number
}
export interface StoreExteriorMenus {
    aProjectExteriorMenuID: number,
    nProjectID: number,
    nVendor: number,
    nStalls: number,
    nPatio: number,
    nFlat: number,
    nDTPops: number,
    nDTMenu: number,
    nStatus: number
    dDeliveryDate: Date,
    cFabConCost: number,
    cIDTechCost: number,
    cTotalCost: number
}
export interface StorePaymentSystem {
    aProjectPaymentSystemID: number,
    nProjectID: number,
    nVendor: number,
    nBuyPassID: number,
    nServerEPS: number,
    nStatus: number,
    dDeliveryDate: Date
    nPAYSUnits: number,
    n45Enclosures: number,
    n90Enclosures: number,
    nDTEnclosures: number,
    n15SunShields: number,
    nUPS: number
    nShelf: number,
    cCost: number,
    nType: number,
}
export interface StoreInteriorMenus {
    aProjectInteriorMenuID: number,
    nProjectID: number,
    nVendor: number,
    nDMBQuantity: number,
    nStatus: number,
    dDeliveryDate: Date,
    cCost: number
}
export interface StoreSonicRadio {
    aProjectSonicRadioID:number,
    nProjectID: number,
    nVendor: number,
    nOutdoorSpeakers: number,
    nColors: number
    nIndoorSpeakers: number,
    nZones: number,
    nServerRacks: number,
    nStatus: number
    dDeliveryDate: Date,
    cCost: number
}
export interface StoreInstallation {
    aProjectInstallationID: number,
    nProjectID: number,
    nVendor: number,
    tLeadTech: string,
    dInstallDate: Date,
    dInstallEnd: Date,
    nStatus: number,
    nSignoffs: number,
    nTestTransactions: number,
    nProjectStatus: number,
    cCost: number
}

export interface HistoricalProjects {
    nProjectId: number,
    nStoreNo: number,
    dProjectGoliveDate: Date,
    tProjectType: string,
    tProjManager: string,
    tVendor: string
}

export interface ActiveProject{
    nProjectId: number,
    nStoreNo: number,
    dProjectGoliveDate: Date,
    dProjEndDate: Date,
    tProjectType: string,
    tStatus: string,
    tPrevProjManager: string,
    tProjManager: string,
    tOldVendor: string,
    tNewVendor: string
}

export interface SonicNotes {
    aNotesId: number,
    dNotesDate: Date,
    tType: string,
    tSource: string,
    tNote: string
}

export interface SonicNotification {
    aNotification: number,
    tHeader: string
}

export interface SonicProjectExcel {
    tProjectType: string,
    tStoreNumber: string,
    tAddress: string,
    tCity: string,
    tState: string,
    nDMAID: number,
    tDMA: string,
    tRED: string,
    tCM: string,
    tANE: string,
    tRVP: string,
    tPrincipalPartner: string,
    dStatus: Date,
    dOpenStore: Date,
    tProjectStatus: string,
    nStoreExistStatus: number
}

export interface StoreSearchModel {
    nProjectId: number,
    tStoreName: string,
    tProjectName: string,
    tStoreNumber: string,
    tProjectType: string,
    dGoLiveDate: Date
}

export interface NewProjectStore{
    nProjectType:number,
    tStoreNumber: string,
    tAddress: string,
    tCity: string,
    tState: string,
    nDMAID: number,
    tDMA: string,
    tRED: string,
    tCM: string,
    tANE: string,
    tRVP: string,
    tPrincipalPartner: string,
    dStatus: Date,
    dOpenStore: Date,
    nProjectStatus: string,
    aStoreId: number,
    tStoreName: string,
    tStoreAddressLine1: string,
    tStoreAddressLine2: string,
    nStoreState: string
    tStoreZip: string,
    tStoreManager: string,
    tPOC: string,
    tPOCPhone: string,
    tPOCEmail: string,
    tGC: string,
    tGCPhone: string,
    tGCEMail: string
}