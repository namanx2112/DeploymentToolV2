export interface BrandModel {
    aBrandId: number,
    tBrandName: string,
    tBrandDescription: string,
    tBrandWebsite: string,
    tBrandCountry: string,
    tBrandEstablished: Date,
    tBrandCategory: string,
    tBrandContact: string,
    nBrandLogoAttachmentID: number,
    nCreatedBy: number,
    nUpdateBy: number,
    dtCreatedOn: Date,
    dtUpdatedOn: Date,
    tIconURL: string
}


export interface VendorModel {
    aVendorId: number,
    tVendorName: string,
    nTechComponentID: number,
    nBrandID: number,
    tVendorDescription: string,
    tVendorEmail: string,
    tVendorAddress: string,
    tVendorPhone: string,
    tVendorContactPerson: string,
    tVendorWebsite: string,
    tVendorCountry: string,
    tVendorEstablished: Date,
    tVendorCategory: string,
    tVendorContact: string,
    nCreatedBy: number,
    nUpdatedBy: number,
    dtCreatedOn: Date,
    dtUpdatedOn: Date,
    bDeleted: boolean,
}

export interface TechComponentModel {
    aTechComponentId: number,
    tTechComponentName: string,
    nBrandID: number,
    tTechComponentDescription: string,
    tComponentType: string,
    nCreatedBy: number,
    nUpdateBy: number,
    dtCreatedOn: Date,
    dtUpdatedOn: Date,
    bDeleted: boolean
}

export interface FranchiseModel {
    aFranchiseId: number,
    tFranchiseName: string,
    nBrandId: number,
    tFranchiseDescription: string,
    tFranchiseLocation: string,
    dFranchiseEstablished: Date,
    tFranchiseContact: string,
    tFranchiseOwner: string,
    tFranchiseEmail: string,
    tFranchisePhone: string,
    tFranchiseAddress: string,
    nFranchiseEmployeeCount: number,
    nFranchiseRevenue: number,
    nCreatedBy: number,
    nUpdateBy: number,
    nUserID: number,
    dtCreatedOn: Date,
    dtUpdatedOn: Date,
    bDeleted: boolean
}

export interface PartsModel {
    aPartsId: number,
    tPartsName: string,
    nPartsNumber: number,
    nPartsPrice: number
}

export interface UserModel {
    aUserId: number,
    tUserName: string,
    tUserEmail: string,
    tContactNumber: string,
    tRole: string,
    nBrandId: number
}