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
using DeploymentTool.Model.Templates;
using DeploymentTool.Misc;
using System.Web;

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
                int nFranchiseId = (searchFields != null && searchFields.ContainsKey("nFranchiseId")) ? Convert.ToInt32(searchFields["nFranchiseId"]) : 0;
                SqlParameter tModuleNameParam = new SqlParameter("@nVendorId", nVendorId);
                SqlParameter tModuleNameParam2 = new SqlParameter("@nFranchiseId", nFranchiseId);
                List<UserModel> items = db.Database.SqlQuery<UserModel>("exec sproc_getUserModel @nVendorId, @nFranchiseId", tModuleNameParam, tModuleNameParam2).ToList();
                //await db.SaveChangesAsync();
                foreach (var UserModel in items)
                {
                    //    UserModel.userAndUsertypeRel = db.Database.SqlQuery<UserTypeByUser>("exec sproc_GetUserTypeByUserID @aUserID", new SqlParameter("@aUserID", UserModel.aUserID)).ToList();

                    //    UserModel.userAndParmissionRel = db.Database.SqlQuery<UserPermission>("exec sproc_GetPermissionsByUser @aUserID", new SqlParameter("@aUserID", UserModel.aUserID)).ToList();

                    UserModel.rBrandID = db.Database.SqlQuery<int>("exec sproc_GetBrandByUser @aUserID", new SqlParameter("@aUserID", UserModel.aUserID)).ToList();

                }
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<UserModel>>(items, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("User.Get", HttpContext.Current, ex);
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
            var exist = db.Database.SqlQuery<int>("select top 1 1 from tblUser with(nolock) where UPPER(tUserName)='" + userRequest.tUserName.ToUpper() + "'  and aUserID <> " + userRequest.aUserID.ToString()).FirstOrDefault();
            if (exist == null || exist == 0)
            {
                var tblUser = userRequest.GetTblUser();
                db.tblUser.Attach(tblUser);
                db.Entry(tblUser).Property(x => x.nDepartment).IsModified = true;
                db.Entry(tblUser).Property(x => x.tUserName).IsModified = true;
                db.Entry(tblUser).Property(x => x.tName).IsModified = true;
                db.Entry(tblUser).Property(x => x.nRole).IsModified = true;
                db.Entry(tblUser).Property(x => x.nStatus).IsModified = true;
                db.Entry(tblUser).Property(x => x.tEmpID).IsModified = true;
                db.Entry(tblUser).Property(x => x.tMobile).IsModified = true;
                db.Entry(tblUser).Property(x => x.tEmpID).IsModified = true;
                db.Entry(tblUser).Property(x => x.dtUpdatedOn).IsModified = true;
                db.Entry(tblUser).Property(x => x.nUpdateBy).IsModified = true;
                db.Entry(tblUser).Property(x => x.nAccess).IsModified = true;
                // Update into tblUserVendorRelation
                try
                {
                    db.Database.BeginTransaction();
                    await db.SaveChangesAsync();

                    var nUserType = db.Database.ExecuteSqlCommand("delete from tblUserAndUserTypeRel where nUserID =@nUserID ", new SqlParameter("@nUserID", userRequest.aUserID));
                    var nUserPermission = db.Database.ExecuteSqlCommand("delete from tblUserPermissionRel where nUserID =@nUserID ", new SqlParameter("@nUserID", userRequest.aUserID));
                    var nUserPBrand = db.Database.ExecuteSqlCommand("delete from tblUserBrandRel where nUserID =@nUserID ", new SqlParameter("@nUserID", userRequest.aUserID));
                    // Add into tblUserVendorRelation



                    if (userRequest.nVendorId > 0)
                    {
                        var nUserVendor = db.Database.ExecuteSqlCommand("delete from tblUserVendorRel where nUserID =@nUserID ", new SqlParameter("@nUserID", userRequest.aUserID));

                        db.tblUserVendorRels.Add(userRequest.GetTblUserVendorRel(userRequest));
                        await db.SaveChangesAsync();
                        if (userRequest.userAndUsertypeRel == null)
                        {
                            List<UserTypeByUser> itemParts = db.Database.SqlQuery<UserTypeByUser>("exec sproc_GetUsertypeByVendorID @nVendorID, @nUserID", new SqlParameter("@nVendorID", userRequest.nVendorId), new SqlParameter("@nUserID", userRequest.aUserID)).ToList();
                            userRequest.userAndUsertypeRel = itemParts;
                        }
                    }
                    if (userRequest.nFranchiseId > 0)
                    {
                        var nUserVendor = db.Database.ExecuteSqlCommand("delete from tblUserFranchiseRel where nUserID =@nUserID ", new SqlParameter("@nUserID", userRequest.aUserID));

                        db.tblUserFranchiseRel.Add(userRequest.GetTblUserFranchiseRel(userRequest));//Need to check 
                        await db.SaveChangesAsync();
                        if (userRequest.userAndUsertypeRel == null)
                        {
                            userRequest.userAndUsertypeRel = new List<UserTypeByUser>();
                            UserTypeByUser objuser = new UserTypeByUser();
                            objuser.nUserID = userRequest.aUserID;
                            objuser.aUserTypeID = 5;
                            userRequest.userAndUsertypeRel.Add(objuser);
                        }

                    }
                    if (userRequest.userAndUsertypeRel != null)
                    {
                        List<tblUserAndUserTypeRel> objUserUserTypeRel = new List<tblUserAndUserTypeRel>();
                        foreach (var UserTypeByUser in userRequest.userAndUsertypeRel)
                        {
                            tblUserAndUserTypeRel objRel = new tblUserAndUserTypeRel();

                            objRel.aUserAndUserTypeRelID = 0;
                            objRel.nUserID = userRequest.aUserID;
                            objRel.nUserTypeID = UserTypeByUser.aUserTypeID;
                            objUserUserTypeRel.Add(objRel);

                        }
                        db.tblUserAndUserTypeRels.AddRange(objUserUserTypeRel);

                        await db.SaveChangesAsync();
                    }


                    // Commenting for now as this is for future release
                    //if (userRequest.userAndParmissionRel != null)
                    //{
                    //    List<tblUserPermissionRel> objUserPermissionRel = new List<tblUserPermissionRel>();
                    //    foreach (var UserTypeByUser in userRequest.userAndParmissionRel)
                    //    {
                    //        tblUserPermissionRel objPermRel = new tblUserPermissionRel();

                    //        objPermRel.aUserPermissionRelID = 0;
                    //        objPermRel.nUserID = userRequest.aUserID;
                    //        objPermRel.nPermissionID = UserTypeByUser.aPermissionlID;
                    //        objUserPermissionRel.Add(objPermRel);

                    //    }
                    //    db.tblUserPermissionRels.AddRange(objUserPermissionRel);

                    //    await db.SaveChangesAsync();
                    //}
                    if (userRequest.nRole == null)
                        userRequest.nRole = -1;
                    var resole = db.Database.ExecuteSqlCommand("Exec sproc_ChangeUserPermissionFromRole @nUserId, @nRoleId,@nVendorId,@nFranchiseId", new SqlParameter("@nUserId", userRequest.aUserID), new SqlParameter("@nRoleId", userRequest.nRole), new SqlParameter("@nVendorId", userRequest.nVendorId), new SqlParameter("@nFranchiseId", userRequest.nFranchiseId));

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
                    db.Database.CurrentTransaction.Commit();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    TraceUtility.ForceWriteException("User.Update", HttpContext.Current, ex);
                    db.Database.CurrentTransaction.Rollback();
                    if (!tblUserExists(userRequest.aUserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    TraceUtility.ForceWriteException("User.Update", HttpContext.Current, ex);
                    db.Database.BeginTransaction();
                    throw ex;
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            else
                return Ok("User name \"" + userRequest.tUserName + "\" already taken, please choose a different user name!");
        }

        // POST: api/tblUser
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(UserModel userRequest)
        {
            var exist = db.Database.SqlQuery<int>("select top 1 1 from tblUser with(nolock) where UPPER(tUserName)='" + userRequest.tUserName.ToUpper() + "'").FirstOrDefault();
            if (exist == null || exist == 0)
            {
                try
                {
                    db.Database.BeginTransaction();
                    string strPwd;
                    userRequest.tPassword = DeploymentTool.Misc.Utilities.CreatePassword(userRequest.tUserName, 8, out strPwd);
                    var tmpUser = userRequest.GetTblUser();
                    tmpUser.isFirstTime = 1;
                    db.tblUser.Add(tmpUser);
                    await db.SaveChangesAsync();

                    userRequest.aUserID = tmpUser.aUserID;
                    // Add into tblUserVendorRelation
                    // var reso = db.Database.ExecuteSqlCommand("Exec sproc_UpdateUserAndVendorRel @nVendorID,@nUserId", new SqlParameter("@nVendorID", userRequest.nVendorId), new SqlParameter("@nUserId", userRequest.aUserID));
                    if (userRequest.nVendorId > 0)
                    {
                        db.tblUserVendorRels.Add(userRequest.GetTblUserVendorRel(userRequest));
                        await db.SaveChangesAsync();
                        if (userRequest.userAndUsertypeRel == null)
                        {
                            List<UserTypeByUser> itemParts = db.Database.SqlQuery<UserTypeByUser>("exec sproc_GetUsertypeByVendorID @nVendorID, @nUserID", new SqlParameter("@nVendorID", userRequest.nVendorId), new SqlParameter("@nUserID", userRequest.aUserID)).ToList();
                            userRequest.userAndUsertypeRel = itemParts;
                        }
                    }
                    if (userRequest.nFranchiseId > 0)
                    {
                        db.tblUserFranchiseRel.Add(userRequest.GetTblUserFranchiseRel(userRequest));//Need to check 
                        await db.SaveChangesAsync();
                        if (userRequest.userAndUsertypeRel == null)
                        {
                            userRequest.userAndUsertypeRel = new List<UserTypeByUser>();
                            UserTypeByUser objuser = new UserTypeByUser();
                            objuser.nUserID = userRequest.aUserID;
                            objuser.aUserTypeID = 5;
                            userRequest.userAndUsertypeRel.Add(objuser);

                        }
                    }
                    if (userRequest.userAndUsertypeRel != null)
                    {
                        List<tblUserAndUserTypeRel> objUserUserTypeRel = new List<tblUserAndUserTypeRel>();

                        foreach (var UserTypeByUser in userRequest.userAndUsertypeRel)
                        {
                            tblUserAndUserTypeRel objRel = new tblUserAndUserTypeRel();

                            objRel.aUserAndUserTypeRelID = 0;
                            objRel.nUserID = userRequest.aUserID;
                            objRel.nUserTypeID = UserTypeByUser.aUserTypeID;
                            objUserUserTypeRel.Add(objRel);

                        }
                        db.tblUserAndUserTypeRels.AddRange(objUserUserTypeRel);
                        await db.SaveChangesAsync();
                    }

                    //if (userRequest.userAndParmissionRel != null)
                    //{
                    //    List<tblUserPermissionRel> objUserPermissionRel = new List<tblUserPermissionRel>();

                    //    foreach (var UserTypeByUser in userRequest.userAndParmissionRel)
                    //    {
                    //        tblUserPermissionRel objPermRel = new tblUserPermissionRel();

                    //        objPermRel.aUserPermissionRelID = 0;
                    //        objPermRel.nUserID = userRequest.aUserID;
                    //        //objPermRel.nPermissionID = UserTypeByUser.aPermissionlID;
                    //        objUserPermissionRel.Add(objPermRel);

                    //    }
                    //    db.tblUserPermissionRels.AddRange(objUserPermissionRel);

                    //}
                    if (userRequest.nRole == null)
                        userRequest.nRole = -1;
                    var resole = db.Database.ExecuteSqlCommand("Exec sproc_ChangeUserPermissionFromRole @nUserId, @nRoleId,@nVendorId,@nFranchiseId", new SqlParameter("@nUserId", userRequest.aUserID), new SqlParameter("@nRoleId", userRequest.nRole), new SqlParameter("@nVendorId", userRequest.nVendorId), new SqlParameter("@nFranchiseId", userRequest.nFranchiseId));

                    if (userRequest.rBrandID != null)
                    {
                        List<tblUserBrandRel> lstBrandUser = new List<tblUserBrandRel>();
                        foreach (int nbrandId in userRequest.rBrandID)
                        {
                            lstBrandUser.Add(new tblUserBrandRel()
                            {
                                nBrandID = nbrandId,
                                nUserID = tmpUser.aUserID
                            });
                        }
                        db.tblUserBrandRels.AddRange(lstBrandUser);
                    }
                    await db.SaveChangesAsync();

                    if (userRequest.nAccess !=1)
                    Utilities.SendPasswordToEmail(userRequest.tName, userRequest.tUserName, userRequest.tEmail, strPwd, false);

                    db.Database.CurrentTransaction.Commit();
                }
                catch (Exception ex)
                {
                    TraceUtility.ForceWriteException("User.Create", HttpContext.Current, ex);
                    db.Database.CurrentTransaction.Rollback();
                }
                return Json(userRequest);
            }
            else
                return Ok("User name \"" + userRequest.tUserName + "\" already taken, please choose a different user name!");
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