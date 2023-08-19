using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            if(request.aTicketId > 0)
            {
                Misc.Utilities.SetHousekeepingFields(false, HttpContext.Current, request);
                db.Entry(request).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            else
            {
                Misc.Utilities.SetHousekeepingFields(true, HttpContext.Current, request);
                db.tblSupportTickets.Add(request);
                await db.SaveChangesAsync();
            }
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<int>(0, new JsonMediaTypeFormatter())
            };
        }

        [HttpGet]
        public IQueryable<SupportTicketModel> GetAll()
        {
            return db.Database.SqlQuery<SupportTicketModel>("exec sproc_getSupportTicket").AsQueryable();
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

    public class SupportTicketModel
    {
        public int aTicketId { get; set; }
        public Nullable<int> nPriority { get; set; }
        public string tContent { get; set; }
        public Nullable<int> nFileSie { get; set; }
        public string fBase64 { get; set; }
        public string tTicketStatus { get; set; }
        public string tFixComment { get; set; }
        public string tCreatedBy { get; set; }
        public Nullable<int> nCreatedBy { get; set; }
        public Nullable<System.DateTime> dtCreatedOn { get; set; }

    }
}
