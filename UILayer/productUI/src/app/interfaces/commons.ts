import { ChartType } from "chart.js";

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

export interface DahboardTile {
    reportId: number,
    title: string,
    count: number,
    compareWith?: number,
    compareWithText?: string,
    type: DashboardTileType,
    chartType?: ChartType,
    chartValues?: number[],
    chartLabels?: string[]
}

export enum DashboardTileType {
    Text, Chart
}

export interface checkboxItems {
    name: string,
    displayName: string,
    checked: boolean
}
