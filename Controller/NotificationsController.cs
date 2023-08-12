using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class NotificationsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpGet]
        [Route("api/Store/GetMyNotification")]
        public HttpResponseMessage GetMyNotification()
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //securityContext.nUserID

            List<Notifications> items = new List<Notifications>()
            {
                new Notifications()
                {
                    aNotificationId = 1,
                    dCreatedOn = DateTime.Now.AddHours(-5),
                    isNew = true,
                    nNotificationType  = NotificationType.NewStoreAdded,
                    nUserId = 1,
                    tNotification = "5 New Stores added from constructions database",
                    relatedInstances = new List<string>()
                    {
                        "10001", "20035", "21312", "213213", "23123"
                    }
                },
                new Notifications()
                {
                    aNotificationId = 2,
                    dCreatedOn = DateTime.Now.AddDays(-10),
                    isNew = true,
                    nNotificationType  = NotificationType.StoreAssignedToPM,
                    nUserId = 1,
                    tNotification = "2 Stores Assigned to PM",
                    relatedInstances = new List<string>()
                    {
                       "10001", "32313"
                    }
                },
                new Notifications()
                {
                    aNotificationId = 1,
                    dCreatedOn = DateTime.Now.AddDays(-20),
                    isNew = true,
                    nNotificationType  = NotificationType.NewProjectAdded,
                    nUserId = 1,
                    tNotification = "1 New Project added",
                    relatedInstances =new List<string>()
                    {
                       "23123"
                    }
                },
                new Notifications()
                {
                    aNotificationId = 1,
                    dCreatedOn = DateTime.Now.AddDays(-14),
                    isNew = false,
                    nNotificationType  = NotificationType.PONotification,
                    nUserId = 1,
                    tNotification = "1 PO sent",
                    relatedInstances = new List<string>()
                    {
                        "21312"
                    }
                },new Notifications()
                {
                    aNotificationId = 1,
                    dCreatedOn = DateTime.Now.AddDays(-110),
                    isNew = false,
                    nNotificationType  = NotificationType.QuoteRequestNotification,
                    nUserId = 1,
                    tNotification = "2 Qutote request sent",
                    relatedInstances = new List<string>()
                    {
                        "32313", "21312"
                    }
                }
            };
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<Notifications>>(items, new JsonMediaTypeFormatter())
            };
        }


        [Authorize]
        [HttpGet]
        [Route("api/Store/ReadNotification")]
        public HttpResponseMessage ReadNotification(int notificationId)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<string>(string.Empty, new JsonMediaTypeFormatter())
            };
        }
    }
}
