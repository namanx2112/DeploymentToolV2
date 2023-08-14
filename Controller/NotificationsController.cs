using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            List<Notifications> items = new List<Notifications>();
            try
            {
                var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                //securityContext.nUserID
                //  var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
                //Nullable<int> lUserId = securityContext.nUserID;
                int nUserID = 2;
                items = db.Database.SqlQuery<Notifications>("exec sproc_getAllNotificationSummary @nUserID", new SqlParameter("@nUserID", nUserID)).ToList();

                //new Notifications()
                //    {
                //        aNotificationId = 1,
                //        dCreatedOn = DateTime.Now.AddDays(-110),
                //        isNew = false,
                //        nNotificationType  = NotificationType.QuoteRequestNotification,
                //        nUserId = 1,
                //        tNotification = "2 Qutote request sent",
                //        relatedInstances = new List<string>()
                //        {
                //            "32313", "21312"
                //        }
                //    }
                //};
            }
            catch (Exception ex)
            { 
            }
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
