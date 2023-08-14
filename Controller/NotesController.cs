using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using DeploymentTool.Misc;

namespace DeploymentTool.Controller
{
    public class NotesController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();
        [Authorize]
        [HttpPost]
        public List<tblProjectNote> Get(Dictionary<string, string> searchFields)
        {
            int nStoreId =  searchFields != null && searchFields["nStoreId"] != null? Convert.ToInt32(searchFields["nStoreId"]):0;

            int nProjectID = searchFields!=null && searchFields.ContainsKey("nProjectID") ? Convert.ToInt32(searchFields["nProjectID"]) : 0;
            //int nStoreId = searchFields.ContainsKey("nStoreId") ? Convert.ToInt32(searchFields["nStoreId"]) : 0;

            List<tblProjectNote> notes = db.Database.SqlQuery<tblProjectNote>("exec sproc_GetNotes @nStoreId,@nProjectID", new SqlParameter("@nStoreId", nStoreId), new SqlParameter("@nProjectID", nProjectID)).ToList();

            //List<tblProjectNote> notes = new List<tblProjectNote>()
            //{
            //    new tblProjectNote()
            //    {
            //        aNoteID = 1,
            //        nProjectID = 1,
            //        nNoteType = 1,
            //        nStoreID = 1,
            //        tNoteDesc = "Test Notes",
            //        tSource = "Fron dummy",
            //        dtCreatedOn = DateTime.Now,
            //        nCreatedBy = 1
            //    },
            //    new tblProjectNote()
            //    {
            //        aNoteID = 1,
            //        nProjectID = 1,
            //        nNoteType = 1,
            //        nStoreID = 1,
            //        tNoteDesc = "Test Notes",
            //        tSource = "Fron dummy",
            //        dtCreatedOn = DateTime.Now,
            //        nCreatedBy = 1
            //    },
            //    new tblProjectNote()
            //    {
            //        aNoteID = 1,
            //        nProjectID = 1,
            //        nNoteType = 1,
            //        nStoreID = 1,
            //        tNoteDesc = "Test Notes",
            //        tSource = "Fron dummy",
            //        dtCreatedOn = DateTime.Now,
            //        nCreatedBy = 1
            //    },
            //    new tblProjectNote()
            //    {
            //        aNoteID = 1,
            //        nProjectID = 1,
            //        nNoteType = 1,
            //        nStoreID = 1,
            //        tNoteDesc = "Test Notes",
            //        tSource = "Fron dummy",
            //        dtCreatedOn = DateTime.Now,
            //        nCreatedBy = 1
            //    }
            //};
            return notes;
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(tblProjectNote noteRequest)
        {
            db.Entry(noteRequest).State = EntityState.Modified;
            noteRequest.dtUpdatedOn = DateTime.Now;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!db.tblProjectNotes.con(noteRequest.aNoteID))
                //{
                //    return NotFound();
                //}
                //else
                //{
                    throw;
                //}
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(tblProjectNote noteRequest)
        {
            noteRequest.aNoteID = 0;
            Utilities.SetHousekeepingFields(true, HttpContext.Current, noteRequest);
            db.tblProjectNotes.Add(noteRequest);
            await db.SaveChangesAsync();
            return Json(noteRequest);
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblProjectNote tblNote = await db.tblProjectNotes.FindAsync(id);
            if (tblNote == null)
            {
                return NotFound();
            }

            db.tblProjectNotes.Remove(tblNote);
            await db.SaveChangesAsync();

            return Ok(tblNote);
            
        }
    }
}
