import { ValidatorFn } from "@angular/forms"

export interface HomeTab {
    tab_name: string,
    tab_header: string,
    tTableName: string,
    tab_unique_name: string,
    tab_type: TabType,
    search_fields: Fields[],
    fields: Fields[],
    childTabs: HomeTab[],
    instanceType: TabInstanceType,
    done?:boolean,
    my_service: any,
    needImport: boolean,
    isTechComponent: boolean
}

export enum TabInstanceType {
    Single, Table, TechComponent
}

export enum TabType {
    Users, Brands, Franchise, Store, TechComponent, Vendor, TechComponentTools, Analytics,
    StoreContact, StoreConfiguration, StoreStackHolder, StoreNetworking, StorePOS, StoreAudio, StoreExteriorMenus, StorePaymetSystem,
    StoreInteriorMenus, StoreSonicRadio, StoreInstallation, StoreProjects, StoreNotes, NewStore, SearchStore, VendorParts, HistoricalProjects,
    StoreProjectServerHandheld, AuditChanges, RolloutProjects, StoreNetworkSwitch, StoreImageMemory,
    StoreOrderAccuracy, StoreOrderStatusBoard
}


export interface Fields {
    field_name: string,
    field_group?: string,
    fieldUniqeName: string,
    icon?: string,
    field_type: FieldType,
    readOnly: boolean,
    field_placeholder: string | null,
    invalid: boolean | false,
    validator: ValidatorFn[],
    mandatory: boolean,
    defaultVal: string,
    conditionals?: any,
    options?: string,
    dropDownOptions?: OptionType[],
    hidden: boolean,
    forImport?: boolean,
    relations?: HomeTab[]
}

export enum FieldType {
    text, email, date, time, dropdown, number, textarea, multiTab, currency, password, multidropdown, checkbox
}

export interface OptionType {
    tDropdownText: string,
    aDropdownId: string,
    optionOrder: number,
    nFunction: number,
    bDeleted: boolean
}
