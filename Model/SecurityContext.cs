using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class SecurityContext
    {
        public string Username { get; set; }
        public int UserId { get; set; }
        public string RoleType { get; set; }
    }
}