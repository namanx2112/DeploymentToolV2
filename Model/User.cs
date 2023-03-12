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
}