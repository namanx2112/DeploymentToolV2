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

        string[] _insArray;

        public string relatedInstances
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _insArray = value.Split(',');
            }
        }
        public string[] arrInstances
        {
            get
            {
                return _insArray;
            }
        }// Instance Id key and data to show will be value

        public int nReadStatus { get; set; }
    }

    public enum NotificationType
    {
        StoreAssignedToPM, VendorMadeChangesInDate, SignOffDailyUpdatesSubmitted, SendStoreRequestErollmentSurvey, NewStoreAdded, DateChangeNotification, NewProjectAdded, TrackingDeliveryNotification, CompletedTask,
        PONotification, QuoteRequestNotification
    }
}