import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BrandModel } from 'src/app/interfaces/models';
import { ReportFolder, ReportInfo } from 'src/app/interfaces/report-generator';
import { AccessService } from 'src/app/services/access.service';
import { CommonService } from 'src/app/services/common.service';
import { ReportGeneratorService } from 'src/app/services/report-generator.service';

@Component({
    selector: 'app-report-folders',
    templateUrl: './report-folders.component.html',
    styleUrls: ['./report-folders.component.css']
})
export class ReportFoldersComponent {
    @Input()
    set request(val: any) {
        this.curBrand = val.curBrand;
        this.getMyFolders();
    }

    @Output()
    folderListAction = new EventEmitter<any>();

    curBrand: BrandModel;

    allFolders: ReportFolder[] = [];
    curReports: ReportInfo[] = [];
    selectedReports: number[] = [];
    constructor(private rgService: ReportGeneratorService, public access: AccessService) {

    }

    getFormatedDate(date: any) {
        return CommonService.getFormatedDateString(date);
    }

    getMyFolders() {
        this.rgService.GetMyFolders(this.curBrand.aBrandId).subscribe(x => {
            this.folderListAction.emit({ action: "folderFetched", noFolder: (x.length == 0) });
            this.allFolders = x;
        });
    }

    filterVisible(item: any) {
        return item.visible != 0;
    }

    getMyReports(folder: ReportFolder) {
        this.curReports = [];
        this.rgService.GetReportsForFolder(folder.aFolderId).subscribe(x => {
            this.curReports = x;
        });
    }

    applyFilter(event: Event) {
        let filterValue: any;
        filterValue = (event.target as HTMLInputElement).value;
        if (this.allFolders) {
            filterValue = filterValue.toLowerCase();
            for (let indx in this.allFolders) {
                let tItem = this.allFolders[indx];
                if (tItem.tFolderName.trim().toLowerCase().indexOf(filterValue) > -1)
                    tItem.visible = 1;
                else
                    tItem.visible = 0;
            }
            // if (this.curReports.length > 0) {
            //     for (let indx in this.curReports) {
            //         let tItem = this.curReports[indx];
            //         if (tItem.tReportName.trim().toLowerCase().indexOf(filterValue) > -1)
            //             tItem.visible = 1;
            //         else
            //             tItem.visible = 0;
            //     }
            // }
        }
    }

    changeReportSelection(ev: any, item: ReportInfo) {
        let checked = ev.checked;
        let indx = this.selectedReports.findIndex(x => x == item.aReportId);
        if (checked) {
            if (indx == -1)
                this.selectedReports.push(item.aReportId);
        }
        else {
            this.selectedReports.splice(indx, 1)
        }
        this.folderListAction.emit({ action: "reportselect", selectedReports: this.selectedReports });
        //ev.stopPropagation();
    }

    editMe(item: any, report: boolean) {
        this.folderListAction.emit({ action: "editreport", item: { item: item, report: report } });
    }

    openReport(item: ReportInfo) {
        this.folderListAction.emit({ action: "openreport", item: item });
    }

    deleteMe(item: any, report: boolean) {
        if (!report) {
            if (confirm("Are you sure you want to delete this folder?")) {
                this.rgService.DeleteFolder(item).subscribe(x => {
                    if (typeof x == 'string' && x == "")
                        this.getMyFolders();
                    else
                        alert(x);
                });
            }
        }
        else {
            if (confirm("Are you sure you want to delete this report?")) {
                this.rgService.DeleteReport(item).subscribe(x => {
                    if (typeof x == 'string' && x == "")
                        this.getMyFolders();
                    else
                        alert(x);
                });
            }
        }
    }

    Closed() { }

}
