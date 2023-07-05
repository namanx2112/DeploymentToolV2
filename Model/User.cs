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
        public string userName { get; set; }
        public string password { get; set; }
        public string tName { get; set; }
        public string tEmail { get; set; }
        public string nRoleType { get; set; }

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
        public string tEmpID { get; set; }
        public string tMobile { get; set; }
        public int nVendorId { get; set; }

        public tblUser GetTblUser()
        {
            return new tblUser(){
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
    }
}