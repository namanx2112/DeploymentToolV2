using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Role
    {
        public int aRoleID { get; set; }
        public string tRoleName { get; set; }
        public int nCreatedBy { get; set; }
        public List<RolePermission> rolePermission { get; set; }
    }
    public class RolePermission
    {
        public int aRoleID { get; set; }
        public int aPermissionlID { get; set; }
        public string tPermissionName { get; set; }
        public string tPermissionDiplayName { get; set; }
        public int nCreatedBy { get; set; }


    }
}