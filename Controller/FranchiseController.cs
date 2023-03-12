using DeploymentTool.Helpers;
using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class FranchiseController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("GetFranchises")]
        public HttpResponseMessage GetFranchises([FromBody] Franchise inputFranchise)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            var dal = new FranchiseDAL();
            var result = dal.GetFranchises(inputFranchise, inputFranchise.nUserID);

            if (result == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<Franchise>>(result, new JsonMediaTypeFormatter())
                };
            }

        }

        // DELETE api/<controller>/5

        [Authorize]
        [HttpPost]
        [Route("api/Franchise/delete")]
        public HttpResponseMessage Delete(Franchise franchise)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");
            int nUserid = (int)securityContext.nUserID;
            var franchiseDAL = new FranchiseDAL();
            franchiseDAL.Delete(franchise.aFranchiseId, nUserid);

            return new HttpResponseMessage(HttpStatusCode.OK);


        }

        [Authorize]
        [HttpPost]
        [Route("api/Franchise/update")]
        // PUT api/<controller>/5
        public HttpResponseMessage Update([FromBody] Franchise franchise)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");
            var franchiseDAL = new FranchiseDAL();

            franchise.nUpdateBy = (int)securityContext.nUserID;
            
            franchiseDAL.Update(franchise,franchise.nUserID);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Franchise>(franchise, new JsonMediaTypeFormatter())
            };

        }

        [Authorize]
        [HttpPost]
        [Route("api/Franchise/CreateFranchise")]
        // POST api/<controller>
        public HttpResponseMessage CreateFranchise([FromBody] Franchise franchise)
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


            var franchiseDAL = new FranchiseDAL();
            int franchiseID = franchiseDAL.CreateFranchise(franchise, franchise.nUserID);
            franchise.aFranchiseId= franchiseID;

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Franchise>(franchise, new JsonMediaTypeFormatter())
            };
        }
    }
}
