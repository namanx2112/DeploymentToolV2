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

namespace DeploymentTool.Controller
{
    public class UserController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        // GET: api/tblUser
        [Authorize]
        [HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> Get(Dictionary<string, string> searchFields)
        {
            try
            {
                int nVendorId = (searchFields != null && searchFields.ContainsKey("nVendorId")) ? Convert.ToInt32(searchFields["nVendorId"]) : 0;
                SqlParameter tModuleNameParam = new SqlParameter("@nVendorId", nVendorId);
                List<UserModel> items = db.Database.SqlQuery<UserModel>("exec sproc_getUserModel @nVendorId", tModuleNameParam).ToList();
                //await db.SaveChangesAsync();
                foreach (var UserModel in items)
                {
                    UserModel.userAndUsertypeRel = db.Database.SqlQuery<UserTypeByUser>("exec sproc_GetUserTypeByUserID @aUserID", new SqlParameter("@aUserID", UserModel.aUserID)).ToList();

                    UserModel.userAndParmissionRel = db.Database.SqlQuery<UserPermission>("exec sproc_GetPermissionsByUser @aUserID", new SqlParameter("@aUserID", UserModel.aUserID)).ToList();

                    UserModel.rBrandID = db.Database.SqlQuery<int>("exec sproc_GetBrandByUser @aUserID", new SqlParameter("@aUserID", UserModel.aUserID)).ToList();

                }
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<UserModel>>(items, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            // return items;
        }

        // GET: api/tblUser/5
        [ResponseType(typeof(tblUser))]
        public async Task<IHttpActionResult> GettblUser(int id)
        {
            tblUser tblUser = await db.tblUser.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            return Ok(tblUser);
        }

        // PUT: api/tblUser/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(UserModel userRequest)
        {

            db.Entry(userRequest.GetTblUser()).State = EntityState.Modified;
            // Update into tblUserVendorRelation
            try
            {
                await db.SaveChangesAsync();

                //  var nUserVendor = db.Database.ExecuteSqlCommand("delete from tblUserVendorRel where nUserID =@nUserID ", new SqlParameter("@nUserID", userRequest.aUserID));
                var nUserType = db.Database.ExecuteSqlCommand("delete from tblUserAndUserTypeRel where nUserID =@nUserID ", new SqlParameter("@nUserID", userRequest.aUserID));
                var nUserPermission = db.Database.ExecuteSqlCommand("delete from tblUserPermissionRel where nUserID =@nUserID ", new SqlParameter("@nUserID", userRequest.aUserID));
                var nUserPBrand = db.Database.ExecuteSqlCommand("delete from tblUserBrandRel where nUserID =@nUserID ", new SqlParameter("@nUserID", userRequest.aUserID));
                // Add into tblUserVendorRelation

                //db.tblUserVendorRels.Add(userRequest.GetTblUserVendorRel(userRequest));
                //await db.SaveChangesAsync();


                if (userRequest.userAndUsertypeRel != null)
                {
                    List<tblUserAndUserTypeRel> objUserUserTypeRel = new List<tblUserAndUserTypeRel>();
                    foreach (var UserTypeByUser in userRequest.userAndUsertypeRel)
                    {
                        tblUserAndUserTypeRel objRel = new tblUserAndUserTypeRel();

                        objRel.aUserAndUserTypeRelID = 0;
                        objRel.nUserID = userRequest.aUserID;
                        //objRel.nPermissionID = UserTypeByUser.aUserTypeID;
                        objUserUserTypeRel.Add(objRel);

                    }
                    db.tblUserAndUserTypeRels.AddRange(objUserUserTypeRel);

                    await db.SaveChangesAsync();
                }


                if (userRequest.userAndParmissionRel != null)
                {
                    List<tblUserPermissionRel> objUserPermissionRel = new List<tblUserPermissionRel>();
                    foreach (var UserTypeByUser in userRequest.userAndParmissionRel)
                    {
                        tblUserPermissionRel objPermRel = new tblUserPermissionRel();

                        objPermRel.aUserPermissionRelID = 0;
                        objPermRel.nUserID = userRequest.aUserID;
                        objPermRel.nPermissionID = UserTypeByUser.aPermissionlID;
                        objUserPermissionRel.Add(objPermRel);

                    }
                    db.tblUserPermissionRels.AddRange(objUserPermissionRel);

                    await db.SaveChangesAsync();
                }

                if (userRequest.rBrandID != null)
                {
                    List<tblUserBrandRel> lstBrandUser = new List<tblUserBrandRel>();
                    foreach (int nbrandId in userRequest.rBrandID)
                    {
                        lstBrandUser.Add(new tblUserBrandRel()
                        {
                            nBrandID = nbrandId,
                            nUserID = userRequest.aUserID
                        });
                    }
                    db.tblUserBrandRels.AddRange(lstBrandUser);
                }

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExists(userRequest.aUserID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tblUser
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(UserModel userRequest)
        {
            var tmpUser = userRequest.GetTblUser();
            db.tblUser.Add(tmpUser);
            await db.SaveChangesAsync();

            userRequest.aUserID = tmpUser.aUserID;
            // Add into tblUserVendorRelation

            db.tblUserVendorRels.Add(userRequest.GetTblUserVendorRel(userRequest));
            await db.SaveChangesAsync();

            if (userRequest.userAndUsertypeRel != null)
            {
                List<tblUserAndUserTypeRel> objUserUserTypeRel = new List<tblUserAndUserTypeRel>();

                foreach (var UserTypeByUser in userRequest.userAndUsertypeRel)
                {
                    tblUserAndUserTypeRel objRel = new tblUserAndUserTypeRel();

                    objRel.aUserAndUserTypeRelID = 0;
                    objRel.nUserID = userRequest.aUserID;
                    //objRel.nUserTypeID = UserTypeByUser.aUserTypeID;
                    objUserUserTypeRel.Add(objRel);

                }
                db.tblUserAndUserTypeRels.AddRange(objUserUserTypeRel);

                await db.SaveChangesAsync();
            }

            if (userRequest.userAndParmissionRel != null)
            {
                List<tblUserPermissionRel> objUserPermissionRel = new List<tblUserPermissionRel>();

                foreach (var UserTypeByUser in userRequest.userAndParmissionRel)
                {
                    tblUserPermissionRel objPermRel = new tblUserPermissionRel();

                    objPermRel.aUserPermissionRelID = 0;
                    objPermRel.nUserID = userRequest.aUserID;
                    //objPermRel.nPermissionID = UserTypeByUser.aPermissionlID;
                    objUserPermissionRel.Add(objPermRel);

                }
                db.tblUserPermissionRels.AddRange(objUserPermissionRel);

                await db.SaveChangesAsync();
            }

            if(userRequest.rBrandID != null)
            {
                List<tblUserBrandRel> lstBrandUser = new List<tblUserBrandRel>();
                foreach(int nbrandId in userRequest.rBrandID)
                {
                    lstBrandUser.Add(new tblUserBrandRel()
                    {
                        nBrandID = nbrandId,
                        nUserID = tmpUser.aUserID
                    });
                }
                db.tblUserBrandRels.AddRange(lstBrandUser);
            }
            return Json(userRequest);
        }

        // DELETE: api/tblUser/5
        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblUser tblUser = await db.tblUser.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }
            tblUser.bDeleted = true;
            db.Entry(tblUser).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(tblUser);
        }

        // GET: api/tblUserType
        [Authorize]
        [HttpPost]
        public IQueryable<tblUserType> GetUserType()
        {

            IQueryable<tblUserType> items = db.tblUserTypes;
            return items;
        }
        [Authorize]
        [HttpPost]
        public IQueryable<UserTypeByUser> GetUserTypeByUserID(int aUserID)
        {
            //int aUserID = (searchFields != null && searchFields.ContainsKey("aUserID")) ? Convert.ToInt32(searchFields["aUserID"]) : 0;
            SqlParameter tModuleNameParam = new SqlParameter("@aUserID", aUserID);
            IQueryable<UserTypeByUser> items = db.Database.SqlQuery<UserTypeByUser>("exec sproc_GetUserTypeByUserID @aUserID", tModuleNameParam).AsQueryable();
            return items;
        }        
        [Authorize]
        [HttpPost]
        public IQueryable<UserPermission> GetUserPermissionByUser(int aUserID)
        {
            //int aUserID = (searchFields != null && searchFields.ContainsKey("aUserID")) ? Convert.ToInt32(searchFields["aUserID"]) : 0;
            SqlParameter tModuleNameParam = new SqlParameter("@aUserID", aUserID);
            IQueryable<UserPermission> items = db.Database.SqlQuery<UserPermission>("exec sproc_GetPermissionsByUser @aUserID", tModuleNameParam).AsQueryable();
            return items;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserExists(int id)
        {
            return db.tblUser.Count(e => e.aUserID == id) > 0;
        }
    }
}