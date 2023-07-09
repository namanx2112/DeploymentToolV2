using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class SupportController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [HttpPost]
        public async Task<HttpResponseMessage> Log(tblSupportTicket request)
        {
            Misc.Utilities.SetHousekeepingFields(true, HttpContext.Current, request);
            db.tblSupportTickets.Add(request);
            await db.SaveChangesAsync();
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<int>(0, new JsonMediaTypeFormatter())
            };
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get(int nTicketId)
        {
            var request = db.tblSupportTickets.Where(p => p.aTicketId == nTicketId).FirstOrDefault();
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<tblSupportTicket>(request, new JsonMediaTypeFormatter())
            };
        }
    }
}
