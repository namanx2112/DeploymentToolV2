export interface Commons {
}

export interface Dictionary<T> {
    [Key: string]: T;
}

export class ControlsErrorMessages {
    public static Requird = "Field is required";
    public static Email = "Email format is not correct";
    public static Range = "Range is not proper";
}

export enum FormsModes {
    None, New, Edit
}

export interface SonicProjectHighlights {
    title: string,
    count: number
}

export interface checkboxItems {
    name: string,
    displayName: string,
    checked: boolean
}
