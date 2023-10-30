using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.SqlClient;
using DeploymentTool.Misc;
using System.Web;
using ExcelDataReader;

namespace DeploymentTool.Controller
{
    public class VendorPartsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpPost]
        public IQueryable<VendorParts> Get(Dictionary<string, string> searchFields)
        {
            int nVendorId = (searchFields != null && searchFields.ContainsKey("nVendorId")) ? Convert.ToInt32(searchFields["nVendorId"]) : 0;
            SqlParameter tModuleNameParam = new SqlParameter("@nVendorId", nVendorId);
            IQueryable<VendorParts> items = db.Database.SqlQuery<VendorParts>("exec sproc_getVendorPartsModel @nVendorId", tModuleNameParam).AsQueryable();
            return items;
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Update(VendorParts partsRequest)
        {

            db.Entry(partsRequest.GetTblParts()).State = EntityState.Modified;
            // Update into tblVendorPartRel
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                TraceUtility.ForceWriteException("VendorParts.Update", HttpContext.Current, ex);
                if (!tblUserExists(partsRequest.aPartID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tblUser
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(VendorParts partsRequest)
        {
            tblPart tParts = partsRequest.GetTblParts();
            db.tblParts.Add(tParts);
            await db.SaveChangesAsync();
            // Add into tblVendorPartRel
            partsRequest.aPartID = tParts.aPartID;
            db.tblVendorPartRels.Add(partsRequest.GetTblVendorPartRel(partsRequest));
            await db.SaveChangesAsync();


            return Json(partsRequest);
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Import(VendorParts partsRequest)
        {
            tblPart tParts = partsRequest.GetTblParts();
            db.tblParts.Add(tParts);
            await db.SaveChangesAsync();
            // Add into tblVendorPartRel
            partsRequest.aPartID = tParts.aPartID;
            db.tblVendorPartRels.Add(partsRequest.GetTblVendorPartRel(partsRequest));
            await db.SaveChangesAsync();


            return Json(partsRequest);
        }

        // DELETE: api/tblUser/5
        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Delete(int id)
        {
            tblPart tblPart = await db.tblParts.FindAsync(id);
            if (tblPart == null)
            {
                return NotFound();
            }

            tblPart.bDeleted = true;
            db.Entry(tblPart).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(tblPart);
        }


        private bool tblUserExists(int id)
        {
            return db.tblParts.Count(e => e.aPartID == id) > 0;
        }
    }


    public class VendorPartsImportable : IImportables
    {
        public List<IImportModel> Import(string strPath, int nBrandId, int nInstanceId)
        {
            List<IImportModel> items = Utilities.ConvertExcelReaderToImportableModel(strPath, new VendorPartsImportModel());
            return items;
        }
    }

    public class VendorPartsImportModel : IImportModel
    {
        private List<string> excelColumns = new List<string>()
        {
            "Parts Description", "Parts Number", "Parts Price"
        };
        public string tPartDesc { get; set; }
        public string tPartNumber { get; set; }
        public Nullable<decimal> cPrice { get; set; }

        public bool isExist { get; set; }

        public IImportModel GetFromExcel(IExcelDataReader reader)
        {
            tPartDesc = excelColumns.IndexOf("Parts Description") > -1 ? reader.GetValue(excelColumns.IndexOf("Parts Description")).ToString() : "";
            tPartNumber = excelColumns.IndexOf("Parts Number") > -1 ? reader.GetValue(excelColumns.IndexOf("Parts Number")).ToString() : "";
            cPrice = excelColumns.IndexOf("Parts Price") > -1 ? Convert.ToDecimal(reader.GetValue(excelColumns.IndexOf("Parts Price")).ToString()) : 0;
            isExist = false;
            return this;
        }
    }
}
