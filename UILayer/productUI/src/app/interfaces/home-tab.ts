import { ValidatorFn } from "@angular/forms"

export interface HomeTab {
    tab_name: string,
    tab_header: string,
    tab_unique_name: string,
    tab_type: TabType,
    search_fields: Fields[],
    fields: Fields[],
    childTabs: HomeTab[],
    instanceType: TabInstanceType
}

export enum TabInstanceType {
    Single, Table
}

export enum TabType {
    Users, Brands, Franchise, Store, TechComponent, Vendor, TechComponentTools, Analytics,
    StoreContact, StoreConfiguration, StoreStackHolder, StoreNetworking, StorePOS, StoreAudio, StoreExteriorMenus, StorePaymetSystem, 
    StoreInteriorMenus, StoreSonicRadio, StoreInstallation
}


export interface Fields {
    field_name: string,
    fieldUniqeName: string,
    icon?: string,
    field_type: FieldType,
    readOnly: boolean,
    field_placeholder: string | null,
    invalid: boolean | false,
    validator: ValidatorFn[],
    mandatory: boolean,
    defaultVal: string,
    conditional_mandatory?: ConditionalOption,
    options?: OptionType[],
    hidden: boolean
}

export enum FieldType {
    text, email, date, time, dropdown, number
}

export interface OptionType {
    optionDisplayName: string,
    optionIndex: string,
    optionOrder: number
}

export interface ConditionalOption {
    field_name: string,
    value: string
}
