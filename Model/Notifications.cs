using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Notifications
    {
        public int aNotificationId { get; set; }
        public int nUserId { get; set; }
        public int tFromUserId { get; set; }
        public NotificationType nNotificationType { get; set; }
        public DateTime dCreatedOn { get; set; }
        public bool isNew { get; set; }
        public string tNotification { get; set; }
        public List<string> relatedInstances { get; set; }// Instance Id key and data to show will be value
    }

    public enum NotificationType
    {
        StoreAssignedToPM, VendorMadeChangesInDate, SignOffDailyUpdatesSubmitted, SendStoreRequestErollmentSurvey, NewStoreAdded, DateChangeNotification, NewProjectAdded, TrackingDeliveryNotification, CompletedTask,
        PONotification, QuoteRequestNotification
    }
}