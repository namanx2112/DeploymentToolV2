import { Component, Input } from '@angular/core';
import { DocumentsTabTable, StoreSearchModel } from 'src/app/interfaces/store';
import { CommonService } from 'src/app/services/common.service';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-documents-tab',
  templateUrl: './documents-tab.component.html',
  styleUrls: ['./documents-tab.component.css']
})
export class DocumentsTabComponent {
  _curStore: StoreSearchModel;
  @Input()
  set curStore(val: StoreSearchModel) {
    this._curStore = val;
    this.loadTab();
  }
  allStatus: DocumentsTabTable[]
  constructor(private storeService: StoreService) {

  }

  getFormateDate(str: any) {
    return CommonService.getFormatedDateString(str);
  }

  loadTab() {
    this.storeService.GetDocumentationTab(this._curStore.nStoreId).subscribe(x => {
      this.allStatus = x;
    })
  }

  DownloadItem(item: DocumentsTabTable){
    this.storeService.downloadPO(item).subscribe(tdata => {
      var newBlob = new Blob([tdata], { type: "application/pdf" });

      // For other browsers: 
      // Create a link pointing to the ObjectURL containing the blob.
      const data = window.URL.createObjectURL(newBlob);

      var link = document.createElement('a');
      link.href = data;
      link.download = item.tFileName;
      // this is necessary as link.click() does not work on the latest firefox
      link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));

      setTimeout(function () {
        // For Firefox it is necessary to delay revoking the ObjectURL
        window.URL.revokeObjectURL(data);
        link.remove();
      }, 100);
    }, async (error) => {
      console.log("Error occured:" + error);
    });
  }
}
