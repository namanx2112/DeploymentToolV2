using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using DeploymentTool.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Web;
using DeploymentTool.Helpers;

namespace DeploymentTool.Controller
{
    public class BrandController : ApiController
    {

        
        // GET api/<controller>
        [Authorize]
        [HttpPost]
        [ActionName("GetBrands")]
        public HttpResponseMessage GetBrands([FromBody] Brand inputbrand)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            var dal = new BrandDAL();
            var result = dal.GetBrands(inputbrand,(int)securityContext.nUserID);

            if (result == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<Brand>>(result, new JsonMediaTypeFormatter())
                };
            }
        }


       
        [Authorize]
        [HttpPost]
        [ActionName("CreateBrand")]
        // POST api/<controller>
        public HttpResponseMessage CreateBrand([FromBody] Brand brand)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            //if (!ModelState.IsValid)
            //{
            //    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            //}
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context"); 

            int nuserid = 1;
            var brandDAL = new BrandDAL();
            brandDAL.Update(brand);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Brand>(brand, new JsonMediaTypeFormatter())
            };
        }


        [Authorize]
        [HttpPost]
        [Route("api/Brand/update")]
        // PUT api/<controller>/5
        public HttpResponseMessage Update([FromBody] Brand brand)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");
            var brandDAL = new BrandDAL();
            
            brand.nUpdateBy = (int)securityContext.nUserID;
            brandDAL.Update(brand);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Brand>(brand, new JsonMediaTypeFormatter())
            };

        }

        // DELETE api/<controller>/5

        [Authorize]
        [HttpPost]
        [Route("api/Brand/delete")]
        public HttpResponseMessage Delete(Brand brand)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");
            int nUserid = (int)securityContext.nUserID;
            var brandDAL = new BrandDAL();
            brandDAL.Delete(brand, nUserid);

            return new HttpResponseMessage(HttpStatusCode.OK);


        }
    }
}