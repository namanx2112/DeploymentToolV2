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
        public async Task<IHttpActionResult> Import(List<VendorPartsImportModel> partsRequest)
        {
            try
            {
                foreach (VendorPartsImportModel tObject in partsRequest)
                {
                    if (tObject.isExist)
                    {
                        VendorParts tRequest = (VendorParts)tObject.GetMyTblModel();
                        //update
                    }
                    else
                    {
                        //Create
                    }
                }
            }
            catch(Exception ex)
            {

            }

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
            List<IImportModel> items = Utilities.ConvertExcelReaderToImportableModel(strPath, new VendorPartsImportModel(), nInstanceId);
            return items;
        }
    }

    public class VendorPartsImportModel : IImportModel
    {
        private dtDBEntities db = new dtDBEntities();
        private List<string> excelColumns = new List<string>()
        {
            "Parts Description", "Parts Number", "Parts Price"
        };

        public int nVendorId { get; set; }
        public int aPartID { get; set; }
        public string tPartDesc { get; set; }
        public string tPartNumber { get; set; }
        public Nullable<decimal> cPrice { get; set; }

        public bool isExist { get; set; }

        public IImportModel GetFromExcel(IExcelDataReader reader, int instanceId)
        {
            tPartDesc = excelColumns.IndexOf("Parts Description") > -1 ? reader.GetValue(excelColumns.IndexOf("Parts Description")).ToString() : "";
            tPartNumber = excelColumns.IndexOf("Parts Number") > -1 ? reader.GetValue(excelColumns.IndexOf("Parts Number")).ToString() : "";
            cPrice = excelColumns.IndexOf("Parts Price") > -1 ? Convert.ToDecimal(reader.GetValue(excelColumns.IndexOf("Parts Price")).ToString()) : 0;
            var output = db.Database.SqlQuery<string>("select nPartID from tblVendorPartRel with (nolock) where nPartID in (select aPartID from tblparts with (nolock) where tPartNumber=@tPartNumber) and nVendorID=@nVendorID", new SqlParameter("@tPartNumber", tPartNumber), new SqlParameter("@nVendorID", nVendorId)).FirstOrDefault();

            nVendorId = instanceId;
            isExist = false;
            aPartID = 0;
            if (output != null && output != "" && Convert.ToInt32(output) > 0)
            {
                isExist = true;
                aPartID = Convert.ToInt32(output);// Get Part Id from DB
            }
            
            return this;
        }

        public IModelParent GetMyTblModel()
        {
            return new VendorParts()
            {
                aPartID = this.aPartID,
                tPartDesc = this.tPartDesc,
                tPartNumber = this.tPartNumber,
                cPrice = this.cPrice,
                nVendorId = this.nVendorId
            };
        }
    }
}
