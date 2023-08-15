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
  allNotifications: NotificationModel[];
  news: NotificationModel[];
  olds: NotificationModel[];
  firstFew: NotificationModel[];
  filter: string;
  constructor(private service: NotificationService, private sonicService: SonicService) {
    this.filter = "All";
    this.getNotifcation();
  }

  getNotifcation() {
    this.service.Get().subscribe((x: NotificationModel[]) => {
      for (var tItem in x) {
        x[tItem].tIcon = "check_circle_outline";
      }
      this.allNotifications = x;
      this.getFilteredItems(0);
    })
  }

  filterChange(ev: any) {
    if (this.filter == "All")
      this.getFilteredItems(0);
    else if (this.filter == "Unread")
      this.getFilteredItems(1);
    else
      this.getFilteredItems(2);
  }

  getFilteredItems(readUnread: number) {
    let items = this.allNotifications;
    if (readUnread == 1) {
      items = items.filter(x => x.nReadStatus == 0);
    }
    else if (readUnread == 2) {
      items = items.filter(x => x.nReadStatus == 1);
    }
    var last7DayStart = moment().startOf('day').subtract(1, 'week');
    this.news = items.filter(x => moment(x.dCreatedOn).isAfter(last7DayStart));
    this.firstFew = this.news.slice(0, 5);
    this.olds = items.filter(x => moment(x.dCreatedOn).isBefore(last7DayStart));
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
