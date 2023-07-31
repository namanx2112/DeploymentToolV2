using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DeploymentTool;
using System.Linq.Dynamic.Core;
using DeploymentTool.Model;
using System.Data.SqlClient;
using System.Net.Http.Formatting;
using iTextSharp.text;

namespace DeploymentTool.Controller
{
    public class RoleController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        #region Role 
        [Authorize]
        [HttpPost]
        public IQueryable<Role> Get(Dictionary<string, string> searchFields)
        {
            int aRoleID = (searchFields != null && searchFields.ContainsKey("aRoleID")) ? Convert.ToInt32(searchFields["aRoleID"]) : 0;
            SqlParameter tModuleNameParam = new SqlParameter("@aRoleID", aRoleID);
            IQueryable<Role> items = db.Database.SqlQuery<Role>("exec sproc_GetRole @aRoleID", tModuleNameParam).AsQueryable();
            return items;
        }
        [Authorize]
        [HttpPost]
        public IQueryable<RolePermission> GetRoleWithPermission(int aRoleID)
        {
            //int aRoleID = (searchFields != null && searchFields.ContainsKey("aRoleID")) ? Convert.ToInt32(searchFields["aRoleID"]) : 0;
            SqlParameter tModuleNameParam = new SqlParameter("@aRoleID", aRoleID);
            IQueryable<RolePermission> items = db.Database.SqlQuery<RolePermission>("exec sproc_GetPermissionsByRole @aRoleID", tModuleNameParam).AsQueryable();
            return items;
        }
        [Authorize]
        [HttpPost]
        public HttpResponseMessage GetAllRoleWithPermission(int aRoleID)
        {
            try
            {
               // List<Role> items = new List<Role>();
                //int aRoleID = 0;
                SqlParameter tModuleNameParam = new SqlParameter("@aRoleID", aRoleID);
                List<Role> Ritems = db.Database.SqlQuery<Role>("exec sproc_GetRole @aRoleID", tModuleNameParam).ToList();
                foreach (var Role in Ritems)
                {
                    SqlParameter tModuleNameParams = new SqlParameter("@aRoleID", Role.aRoleID);
                    List<RolePermission> itemFields = db.Database.SqlQuery<RolePermission>("exec sproc_GetPermissionsByRole @aRoleID", tModuleNameParams).ToList();
                    Role.rolePermission = itemFields;
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<Role>>(Ritems, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
}
        #endregion
    }
}