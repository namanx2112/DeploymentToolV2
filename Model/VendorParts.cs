using DeploymentTool.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class VendorParts: IModelParent
    {
        public int aPartID { get; set; }
        public string tPartDesc { get; set; }
        public string tPartNumber { get; set; }
        public Nullable<decimal> cPrice { get; set; }

        public int nVendorId { get; set; }

        public tblPart GetTblParts()
        {
            return new tblPart()
            {
                aPartID = this.aPartID,
                tPartDesc = this.tPartDesc,
                tPartNumber= this.tPartNumber,
                cPrice = this.cPrice
            };
        }
        public tblVendorPartRel GetTblVendorPartRel(VendorParts partsRequest)
        {
            return new tblVendorPartRel()
            {
                aVendorPartRelID = 0,
                nVendorID = partsRequest.nVendorId,
                nPartID = partsRequest.aPartID
            };
        }
    }
}