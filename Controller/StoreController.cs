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
    public class StoreController : ApiController
    {
        // GET api/<controller>
        [Authorize]
        [HttpPost]
        [ActionName("GetStores")]
        public HttpResponseMessage GetStores([FromBody] Store inputstore)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            var dal = new StoreDAL();
            var result = dal.GetStores(inputstore, (int)securityContext.nUserID);

            if (result == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<Store>>(result, new JsonMediaTypeFormatter())
                };
            }
        }

        [Authorize]
        [HttpPost]
        [ActionName("CreateStore")]
        // POST api/<controller>
        public HttpResponseMessage CreateStore([FromBody] Store store)
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
            var storeDAL = new StoreDAL();
            int storeId = storeDAL.CreateStore(store, securityContext.nUserID);
            store.aStoreId = storeId;

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Store>(store, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        [Route("api/Store/update")]
        // PUT api/<controller>/5
        public HttpResponseMessage Update([FromBody] Store store)
        {
            var securityContext = (User)HttpContext.Current.Items["SecurityContext"];
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (securityContext == null)
                throw new HttpRequestValidationException("Exception while creating Security Context");
            var storeDAL = new StoreDAL();

            store.nUpdateBy = (int)securityContext.nUserID;

            storeDAL.Update(store);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Store>(store, new JsonMediaTypeFormatter())
            };

        }
    }
}