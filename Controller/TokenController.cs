using DeploymentTool.Auth;
using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace DeploymentTool.Controller
{
    public class TokenController : ApiController
    {
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

        public User CheckUser(string username, string password)
        {
            User objUser = new User();
            objUser.userName = username;
            objUser.password = password;
            DBHelper.login(ref objUser);          
            return objUser;           

        }

    }
}