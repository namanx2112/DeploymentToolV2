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
    tGCEMail: string

}
export interface StoreConfiguration {
    aCongigId: number,
    nStallCount: number,
    nDriveThru: number,
    nInsideDining: number,
    dGroundBreak: number,
    dKitchenInstall: Date,
    cProjectCost: number
}
export interface StoreStackholders {
    aStackHolderId: number,
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
    nVendor: number,
    dDeliveryDate: Date,
    dConfigDate: Date,
    dSupportDate: Date,
    nStatus: number
    nPaperworkStatus: number,
    cCost: number
}
export interface StoreAudio {
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
    cCost: number
}
export interface StoreInteriorMenus {
    nVendor: number,
    nDMBQuantity: number,
    nStatus: number,
    dDeliveryDate: Date,
    cCost: number
}
export interface StoreSonicRadio {
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

export interface StoreProjects {
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

export interface SonicNotification{
    aNotification: number,
    tHeader: string
}