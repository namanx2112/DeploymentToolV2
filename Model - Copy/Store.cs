using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Store
    {
        public int aStoreId { get; set; }
        public string tStoreName { get; set; }
        public int? nFranchiseID { get; set; }
        public string tStoreDescription { get; set; }
        public string tStoreLocation { get; set; }
        public DateTime? tStoreEstablished { get; set; }
        public string tStoreContact { get; set; }
        public string tStoreManager { get; set; }
        public string tStoreEmail { get; set; }
        public string tStorePhone { get; set; }
        public string tStoreAddress { get; set; }
        public string tStoreHours { get; set; }
        public string tStoreWebsite { get; set; }
        public string tStoreServices { get; set; }
        public int? tStoreSize { get; set; }
        public string tStoreParking { get; set; }
        public string tStoreAmenities { get; set; }
        public string tStoreTax_id { get; set; }
        public string tStoreSales_tax { get; set; }
        public string tStorePayment_methods { get; set; }
        public string tStoreDelivery { get; set; }
        public string tLatitude { get; set; }
        public string tLongitude { get; set; }
        public int? nCreatedBy { get; set; }
        public int? nUpdateBy { get; set; }
        public DateTime? dtCreatedOn { get; set; }
        public DateTime? dtUpdatedOn { get; set; }
        public bool? bDeleted { get; set; }
    }

}