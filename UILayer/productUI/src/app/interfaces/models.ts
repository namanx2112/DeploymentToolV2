import { HomeTab } from "./home-tab"

export interface BrandModel {
    aBrandId: number,
    tBrandIdentifier: string,
    tBrandName: string,
    tBrandDomain: string,
    tBrandAddressLine1: string,
    tBrandAddressLine2: string,
    tBrandCity: string,
    nBrandState: number,
    nBrandCountry: number,
    tBrandZipCode: string,
    nBrandLogoAttachmentID: number,
    nCreatedBy: number,
    nUpdateBy: number,
    dtCreatedOn: Date,
    dtUpdatedOn: Date,
    bDeleted: boolean,
    tIconURL: string,
    access: boolean | true
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
    aPartID: number,
    nVendorId: number,
    tPartDesc: string,
    tPartNumber: string,
    cPrice: number,
    show: boolean | true
}

export interface POMailMessage {
    nProjectId: number,
    aPurchaseOrderID: number,
    tTo: string,
    tCC: string,
    tSubject: string,
    tContent: string,
    tFileName: string,
    tMyFolderId: string
}

export interface UserModel {
    aUserID: number,
    tName: string,
    tUserName: string,
    tEmail: string,
    nDepartment: number,
    nRole: number,
    tEmpID: string,
    tMobile: string,
    nBrandId: number,
    nVendorId: number
}

export interface QuoteRequestTemplate {
    aQuoteRequestTemplateId: number,
    tTemplateName: string,
    nBrandId: number,
    quoteRequestTechComps: QuoteRequestTechAreas[],
    nCreatedBy?: number,
    nUpdateBy?: number,
    dtCreatedOn?: Date
    dtUpdatedOn?: Date,
    bDeleted?: boolean
}

export interface QuoteRequestTechAreas {
    nQuoteRequestTemplateId: number,
    tTechCompName: string,
    tTableName: string,
    fields: QuoteRequestFields[],
    part: HomeTab,
    nCreatedBy?: number,
    nUpdateBy?: number,
    dtCreatedOn?: Date
    dtUpdatedOn?: Date,
    bDeleted?: boolean
}

export interface QuoteRequestFields {
    nQuoteRequestTemplateId: number,
    tTechCompField: string,
    tTechCompFieldName: string
}



export interface MergedQuoteRequest {
    tContent: string,
    tTo: string,
    tCC: string,
    tSubject: string
}

export interface POConfigTemplateTemp {
    aPurchaseOrderTemplateID: number,
    tTemplateName: string;
    nBrandId: number,
    nVenderId: number,
    dtCreatedOn: Date
}

export interface POConfigTemplate {
    aPurchaseOrderTemplateID: number,
    tTemplateName: string;
    nBrandID: number,
    nVendorID: number,
    tCompName: string,
    purchaseOrderParts: POConfigPart[],
    nCreatedBy?: number,
    nUpdateBy?: number,
    dtCreatedOn?: Date
    dtUpdatedOn?: Date,
    bDeleted?: boolean
}

export interface POConfigPart {
    aPurchaseOrderTemplatePartsID: number,
    nPartID: number,
    tPartDesc: string,
    tPartNumber: string,
    cPrice: number,
    tTableName: string,
    tTechCompField: string,
    nQuantity: number,
    cTotal: number,
    selected: boolean
}

export interface MergedPO {
    aPurchaseOrderPreviewTeamplateID: number,
    nProjectId: number,
    nTemplateId: number,
    nVendorId: number,
    tStore: string,
    tStoreNumber: string,
    tNotes: string,
    tName: string,
    tPhone: string,
    tEmail: string,
    tAddress: string,
    tCity: string,
    tStoreState: string,
    tStoreZip: string,
    tBillToCompany: string,
    tBillToEmail: string,
    tBillToAddress: string,
    tBillToCity: string,
    tBillToState: string,
    cTotal: number,
    dDeliver: Date,
    tTemplateName: string,
    nOutgoingEmailID: number
    purchaseOrderParts: POConfigPart[]
}

export interface DropwDown {
    aDropdownId: number,
    nBrandId: number,
    tModuleName: string,
    tDropdownText: string,
    bDeleted: boolean
}

export interface ProjectTemplates {
    nTemplateId: number,
    tTemplateName: string,
    nTemplateType: ProjectTemplateType
}

export enum ProjectTemplateType {
    Notification, QuoteRequest, PurchaseOrder
}

export enum SuportPriorities{
    High, Medium, Low
}

export interface SupportContent {
    nPriority: number,
    tContent: string,
    nFileSie: number,
    fBase64: string
}