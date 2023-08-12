import { Component, EventEmitter, Input, Output } from '@angular/core';
import moment from 'moment';
import { NotificationModel } from 'src/app/interfaces/notification';
import { StoreSearchModel } from 'src/app/interfaces/sonic';
import { NotificationService } from 'src/app/services/notification.service';
import { SonicService } from 'src/app/services/sonic.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent {
  @Input()
  fullView: boolean;
  @Output() openStore = new EventEmitter<StoreSearchModel>();
  news: NotificationModel[];
  olds: NotificationModel[];
  firstFew: NotificationModel[];
  constructor(private service: NotificationService, private sonicService: SonicService) {
    this.getNotifcation();
  }

  getNotifcation() {
    this.service.Get().subscribe((x: NotificationModel[]) => {
      for (var tItem in x) {
        x[tItem].tIcon = "check_circle_outline";
      }
      this.firstFew = x.slice(0, 5);
      this.news = x.filter(x => x.isNew);
      this.olds = x.filter(x => !x.isNew);
    })
  }

  openItem(item: any) {
    this.sonicService.SearchStore(item).subscribe((x: StoreSearchModel[]) => {
      this.openStore.emit(x[0]);
    });
  }

  readMe(notification: NotificationModel) {
    notification.tIcon = "check_circle";
    this.service.ReadNotification(notification.aNotificationId).subscribe(x => {
      this.getNotifcation();
    });
  }

  getTime(dString: any) {
    let momentVal = (typeof dString == 'string') ? ((dString.indexOf("-") > -1) ? moment(dString, 'YYYY-MM-DD') : moment(dString, 'DD/MM/YYYY')) : moment(dString);
    //let tDate = momentVal.format('MM/DD/YYYY');
    //let diff = moment(new Date()).subtract(momentVal.date()).hours();
    var diff = moment.duration(moment(new Date()).diff(momentVal)).asHours();
    let tVal = Math.round(diff);
    let tValue = "";
    if (tVal < 24)
      tValue = tVal + " hours ago";
    else
      tValue = Math.round(tVal / 24) + " days ago";
    return tValue;
  }
}
