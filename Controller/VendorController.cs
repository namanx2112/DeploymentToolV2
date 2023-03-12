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
    public class VendorController : ApiController
    {
        // GET api/<controller>
        [Authorize]
        [HttpPost]
        [ActionName("GetVendors")]
        public HttpResponseMessage GetVendors([FromBody] Vendor inputvendor)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            var dal = new VendorDAL();
            var result = dal.GetVendors(inputvendor, (int)securityContext.nUserID);

            if (result == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<Vendor>>(result, new JsonMediaTypeFormatter())
                };
            }
        }
        [Authorize]
        [HttpPost]
        [ActionName("CreateVendor")]
        // POST api/<controller>
        public HttpResponseMessage CreateVendor([FromBody] Vendor vendor)
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
            var vendorDAL = new VendorDAL();
            int vendorId = vendorDAL.CreateVendor(vendor, securityContext.nUserID);
            vendor.aVendorId = vendorId;

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Vendor>(vendor, new JsonMediaTypeFormatter())
            };
        }
        [Authorize]
        [HttpPost]
        [Route("api/Vendor/update")]
        // PUT api/<controller>/5
        public HttpResponseMessage Update([FromBody] Vendor vendor)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");
            var vendorDAL = new VendorDAL();

            vendor.nUpdatedBy = (int)securityContext.nUserID;

            vendorDAL.Update(vendor);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Vendor>(vendor, new JsonMediaTypeFormatter())
            };

        }
        // DELETE api/<controller>/5

        [Authorize]
        [HttpPost]
        [Route("api/Vendor/delete")]
        public HttpResponseMessage Delete(Vendor vendor)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");
            int nUserid = (int)securityContext.nUserID;
            var vendorDAL = new VendorDAL();
            vendorDAL.Delete(vendor, nUserid);

            return new HttpResponseMessage(HttpStatusCode.OK);


        }

    }
}