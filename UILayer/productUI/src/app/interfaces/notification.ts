export interface NotificationModel {
    aNotificationId: number,
    nUserId: number,
    nNotificationType: NotificationType,
    tFromUserId: string,
    dCreatedOn: Date,
    isNew: boolean,
    tNotification: string,
    relatedInstances: any,
    tIcon:string
}

export enum NotificationType{
    StoreAssignedToPM, VendorMadeChangesInDate, SignOffDailyUpdatesSubmitted, SendStoreRequestErollmentSurvey, NewStoreAdded, DateChangeNotification, NewProjectAdded, TrackingDeliveryNotification, CompletedTask,
        PONotification, QuoteRequestNotification
}
