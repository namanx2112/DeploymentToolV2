using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace DeploymentTool.Model
{
    public class MyApiContext
    {
        public string UserId { get; set; }
        public IEnumerable<Claim> ToClaims()
        {
            yield return new Claim(ClaimTypes.NameIdentifier, UserId);
           
        }
    }
}