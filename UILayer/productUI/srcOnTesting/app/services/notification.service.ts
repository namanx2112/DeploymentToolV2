import { Injectable } from '@angular/core';
import { CommonService } from './common.service';
import { HttpClient } from '@angular/common/http';
import { CacheService } from './cache.service';
import { NotificationModel } from '../interfaces/notification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private commonService: CommonService, private http: HttpClient, private cacheService: CacheService) { }

  Get() {
    return this.http.get<NotificationModel[]>(CommonService.ConfigUrl + "Notifications/GetMyNotification", { headers: this.cacheService.getHttpHeaders() });
  }

  ReadNotification(notificationId: number) {
    return this.http.get<NotificationModel[]>(CommonService.ConfigUrl + "Notifications/ReadNotification?notificationId=" + notificationId, { headers: this.cacheService.getHttpHeaders() });
  }
}
