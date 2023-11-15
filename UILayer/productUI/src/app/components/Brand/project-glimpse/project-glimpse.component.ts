import { Component, Input } from '@angular/core';
import { BrandModel } from 'src/app/interfaces/models';
import { ProjectGlimpse, ProjectInfo } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';
import { ExStoreService } from 'src/app/services/ex-store.service';

@Component({
  selector: 'app-project-glimpse',
  templateUrl: './project-glimpse.component.html',
  styleUrls: ['./project-glimpse.component.css']
})
export class ProjectGlimpseComponent {
  @Input()
  set request(val: any) {
    this.curProject = val.curProject;
    this.curBrandId = val.curBrandId;
    this.getProjectGlimpse();
  }
  curProject: ProjectInfo;
  curBrandId: number;
  curGlimpse: ProjectGlimpse;
  isRollout: boolean = false;

  constructor(private exStoreService: ExStoreService, private commonService: CommonService) {

  }

  getProjectStatusString(nStatus: number) {
    if (nStatus == null)
      return "";
    let ddOptions = this.commonService.GetDropdownOptions(this.curBrandId, "InstallationProjectStatus");
    let tOp = ddOptions.find(x => x.aDropdownId == nStatus.toString());
    if (tOp)
      return tOp.tDropdownText;
    else
      return "";
  }

  getFormatedDate(dtString: any) {
    return CommonService.getFormatedDateString(dtString);
  }

  getProjectGlimpse() {
    this.exStoreService.GetProjectGlimpse(this.curProject.nProjectId).subscribe(x => {
      this.curGlimpse = x;
      this.isRollout = (x.aProjectsRolloutID > 0);
    })
  }
}
