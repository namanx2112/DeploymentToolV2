using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class NotesController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        public List<tblProjectNote> Get(Dictionary<string, string> searchFields)
        {
            int nPojectId = (searchFields != null && searchFields.ContainsKey("nPojectId")) ? Convert.ToInt32(searchFields["nPojectId"]) : 0;
            List<tblProjectNote> notes = new List<tblProjectNote>()
            {
                new tblProjectNote()
                {
                    aNoteID = 1,
                    nProjectID = 1,
                    nNoteType = 109,
                    nStoreID = 1,
                    tNoteDesc = "Test Notes",
                    tSource = "Fron dummy",
                    dtCreatedOn = DateTime.Now,
                    nCreatedBy = 1
                },
                new tblProjectNote()
                {
                    aNoteID = 1,
                    nProjectID = 1,
                    nNoteType = 109,
                    nStoreID = 1,
                    tNoteDesc = "Test Notes",
                    tSource = "Fron dummy",
                    dtCreatedOn = DateTime.Now,
                    nCreatedBy = 1
                },
                new tblProjectNote()
                {
                    aNoteID = 1,
                    nProjectID = 1,
                    nNoteType = 108,
                    nStoreID = 1,
                    tNoteDesc = "Test Notes",
                    tSource = "Fron dummy",
                    dtCreatedOn = DateTime.Now,
                    nCreatedBy = 1
                },
                new tblProjectNote()
                {
                    aNoteID = 1,
                    nProjectID = 1,
                    nNoteType = 109,
                    nStoreID = 1,
                    tNoteDesc = "Test Notes",
                    tSource = "Fron dummy",
                    dtCreatedOn = DateTime.Now,
                    nCreatedBy = 1
                }
            };
            return notes;
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProject userRequest)
        {
            return Json(userRequest);
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProject userRequest)
        {
            return Json(userRequest);
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Delete(int id)
        {
            return Json(new tblProjectNote());
        }
    }
}
