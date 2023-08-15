using DeploymentTool.Misc;
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
                items = db.Database.SqlQuery<Notifications>("exec sproc_getAllNotificationSummary @nUserID", new SqlParameter("@nUserID", securityContext.nUserID)).ToList();
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("NotificationsController", HttpContext.Current, ex);
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
            try
            {
                db.Database.ExecuteSqlCommand("exec sproc_updateNotificationReadStatus @nNotificationId", new SqlParameter("@nNotificationId", notificationId));
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("NotificationsController", HttpContext.Current, ex);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("Some exception occured", new JsonMediaTypeFormatter())
                };
            }            
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<string>(string.Empty, new JsonMediaTypeFormatter())
            };
        }
    }
}
