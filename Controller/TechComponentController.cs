using DeploymentTool.Helpers;
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
    public class TechComponentController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("CreateTechComponent")]
        // POST api/<controller>
        public HttpResponseMessage CreateTechComponent([FromBody] TechComponent techcomponent)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context"); if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            int nuserid = 1;
            var techcomponentDAL = new TechComponentDAL();
            int techcomponentId = techcomponentDAL.CreateTechComponent(techcomponent, securityContext.nUserID);
            techcomponent.aTechComponentId = techcomponentId;

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<TechComponent>(techcomponent, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        [Route("api/TechComponent/update")]
        // PUT api/<controller>/5
        public HttpResponseMessage Update([FromBody] TechComponent techcomponent)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");
            var techcomponentDAL = new TechComponentDAL();

            techcomponent.nUpdateBy = (int)securityContext.nUserID;
            techcomponentDAL.Update(techcomponent);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<TechComponent>(techcomponent, new JsonMediaTypeFormatter())
            };

        }

        // GET api/<controller>
        [Authorize]
        [HttpPost]
        [ActionName("GetTechComponents")]
        public HttpResponseMessage GetTechComponents([FromBody] TechComponent inputtechcomponent)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            var dal = new TechComponentDAL();
            var result = dal.GetTechComponents(inputtechcomponent, (int)securityContext.nUserID);

            if (result == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<TechComponent>>(result, new JsonMediaTypeFormatter())
                };
            }
        }

        // DELETE api/<controller>/5

        [Authorize]
        [HttpPost]
        [Route("api/TechComponent/delete")]
        public HttpResponseMessage Delete(TechComponent techcomponent)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");
            int nUserid = (int)securityContext.nUserID;
            var techcomponentDAL = new TechComponentDAL();
            techcomponentDAL.Delete(techcomponent, nUserid);

            return new HttpResponseMessage(HttpStatusCode.OK);


        }
    }
}