using DeploymentTool.Auth;
using DeploymentTool.Misc;
using DeploymentTool.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http;
using System.Web.Services.Description;


namespace DeploymentTool.Controller
{
    public class TokenController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Token/Get")]
        public HttpResponseMessage Get(UserForAuthentication request)
        {
            var user = CheckUser(request.UserName, request.Password, HttpContext.Current, Request);
            if (user == null)
                return Request.CreateResponse(HttpStatusCode.OK, "Wrong password or user name. Try again!");
            else
            {
                if (user.nUserID > 0)
                {
                    user.auth = JwtManager.GenerateToken(user);
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage Logout()
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (securityContext != null)
                LogoutMe(securityContext);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage ChangePassword(ChangePasswordModel request)
        {
            string resp = "";
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            var returnVal = db.Database.ExecuteSqlCommand("exec sproc_ChangePassword @nUserId, @tCurPassword, @tNewPassword", new SqlParameter("@nUserId", securityContext.nUserID), new SqlParameter("@tCurPassword", request.tCurrentPassword), new SqlParameter("@tNewPassword", request.tNewPassword));
            if (returnVal == -1)
                resp = "You have entred a wrong password!";
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<string>(resp, new JsonMediaTypeFormatter())
            };
        }

        [HttpPost]
        public HttpResponseMessage ForgotPassword(ForgotPasswordModel tUserNameOrEmail)
        {
            string resp = "";
            //var userObject = db.Database.SqlQuery<User>("select tName, tUserName, tEmail, case when(nRole is null) then 0 else nRole end, aUserID nUserID from tblUser with(nolock) where UPPER(tUserName)='" + tUserNameOrEmail.tContent.ToUpper() + "' or UPPER(tEmail) = '" + tUserNameOrEmail.tContent.ToUpper() + "'").FirstOrDefault();
            var userObject = db.Database.SqlQuery<User>("select tName, tUserName, tEmail, case when(nRole is null) then 0 else nRole end, aUserID nUserID from tblUser with(nolock) where (nAccess=0  Or nAccess=2)  and UPPER(tUserName)='" + tUserNameOrEmail.tContent.ToUpper() + "'").FirstOrDefault();
            if (userObject != null)
            {
                string sPassword;
                string encodedPassword = DeploymentTool.Misc.Utilities.CreatePassword(userObject.tUserName, 8, out sPassword);
                var returnVal = db.Database.ExecuteSqlCommand("update tblUser set tPassword='" + encodedPassword + "', isFirstTime=1 where aUserID =" + userObject.nUserID);
                Utilities.SendPasswordToEmail(userObject.tName, userObject.tUserName, userObject.tEmail, sPassword, true);
                resp = "We have reset your password and sent you the details to login, please check your email!";
            }
            else
                resp = "There’s no Account with the info that you provided.!";
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<string>(resp, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetAccess()
        {
            UserAccessResponse tObj = new UserAccessResponse();
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            var returnList = db.Database.SqlQuery<UserAccessModel>("exec sproc_getMyAccess @nUserId", new SqlParameter("@nUserId", securityContext.nUserID)).ToList();
            if (returnList != null && returnList.Count > 0)
            {
                string sObject = JsonConvert.SerializeObject(returnList);
                tObj.tData = Utilities.EncodeString(sObject);
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<UserAccessResponse>(tObj, new JsonMediaTypeFormatter())
            };
        }

        void LogoutMe(User user)
        {
            db.Database.ExecuteSqlCommand("Exec sproc_UserLogout @nUserId", new SqlParameter("@nUserId", user.nUserID));
        }

        User CheckUser(string username, string password, HttpContext context, HttpRequestMessage request)
        {
            string device = string.Format("IP:{0}, Browser:{1}", Utilities.GetClientIp(request), context.Request.Browser.Browser);
            User objUser = db.Database.SqlQuery<User>("Exec sproc_UserLogin @tUserName, @tPassword, @tDevice", new SqlParameter("@tUserName", username), new SqlParameter("@tPassword", password), new SqlParameter("@tDevice", device)).FirstOrDefault();
            if (objUser != null && objUser.isFirstTime == 1)
            {
                db.Database.ExecuteSqlCommand("update tblUser set isFirstTime = 0 where aUserID=" + objUser.nUserID);
            }
            return objUser;
        }

    }
}