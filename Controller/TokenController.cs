﻿using DeploymentTool.Auth;
using DeploymentTool.Model;
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
            var user = CheckUser(request.UserName, request.Password);

            if (user.nUserID != -1)
            {
                user.auth = JwtManager.GenerateToken(user);
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
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

        User CheckUser(string username, string password)
        {
            User objUser = new User();
            objUser.userName = username;
            objUser.password = password;
            DBHelper.login(ref objUser);
            return objUser;

        }

    }
}