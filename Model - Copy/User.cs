using DeploymentTool.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class User
    {
        public int nUserID { get; set; }
        public int nRole { get; set; }
        public string tName { get; set; }
        public string tEmail { get; set; }
        public AuthResponse auth { get; set; }

    }

    public class UserModel
    {
        public int aUserID { get; set; }
        public string tName { get; set; }
        public string tUserName { get; set; }
        public string tPassword { get; set; }
        public string tEmail { get; set; }
        public Nullable<int> nDepartment { get; set; }
        public Nullable<int> nRole { get; set; }
        public List<int> rBrandID { get; set; }
        public string tEmpID { get; set; }
        public string tMobile { get; set; }
        public int nVendorId { get; set; }
        public int nFranchiseId { get; set; }

        public List<UserTypeByUser> userAndUsertypeRel { get; set; }


        public List<UserPermission> userAndParmissionRel { get; set; }

        public tblUser GetTblUser()
        {
            return new tblUser()
            {
                aUserID = this.aUserID,
                tName = this.tName,
                tUserName = this.tUserName,
                tPassword = this.tPassword,
                tEmail = this.tEmail,
                nDepartment = this.nDepartment,
                nRole = this.nRole,
                tEmpID = this.tEmpID,
                tMobile = this.tMobile
            };
        }
        public tblUserVendorRel GetTblUserVendorRel(UserModel userRequest)
        {
            return new tblUserVendorRel()
            {

                aUserVendorRelID = 0,
                nVendorID = userRequest.nVendorId,
                nUserID = userRequest.aUserID
            };
        }
        public tblUserFranchiseRel GetTblUserFranchiseRel(UserModel userRequest)
        {
            return new tblUserFranchiseRel()
            {

                aUserFranchiseRelID = 0,
                nFranchiseID = userRequest.nFranchiseId,
                nUserID = userRequest.aUserID
            };
        }

    }

    public class UserPermission
    {
        public int nUserID { get; set; }
        public int aPermissionlID { get; set; }
        public string tPermissionName { get; set; }
        public string tPermissionDiplayName { get; set; }
        public int nCreatedBy { get; set; }


    }
    public class UserTypeByUser
    {
        public int nUserID { get; set; }
        public int aUserTypeID { get; set; }
        public string tUserTypeName { get; set; }
        public string tVisibleFor { get; set; }


    }
}