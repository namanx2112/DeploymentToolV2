import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BrandModel } from 'src/app/interfaces/models';
import { ReportFolder, ReportInfo } from 'src/app/interfaces/report-generator';
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
    editItem = new EventEmitter<any>();

    curBrand: BrandModel;

    allFolders: ReportFolder[] = [];
    curReports: ReportInfo[] = [];
    constructor(private rgService: ReportGeneratorService) {

    }

    getFormatedDate(date: any) {
        return CommonService.getFormatedDateTimeString(date);
    }

    getMyFolders() {
        this.rgService.GetMyFolders(this.curBrand.aBrandId).subscribe(x => {
            this.allFolders = x;
        });
    }

    getMyReports(folder: ReportFolder) {
        this.rgService.GetReportsForFolder(folder.aFolderId).subscribe(x => {
            this.curReports = x;
        });
    }

    editMe(item: any, report: boolean) {
        this.editItem.emit({ item: item, report: report });
    }

    deleteMe(item: any, report: boolean) {
        if (!report) {
            this.rgService.DeleteFolder(item).subscribe(x => {
                if (x.aFolderId > 0)
                    this.getMyFolders();
            });
        }
        else {
            // this.rgService.DeleteFolder(item).subscribe(x => {
            //     if (x.aFolderId > 0)
            //         this.getMyFolders();
            // });
        }
    }

    Closed() { }

}
