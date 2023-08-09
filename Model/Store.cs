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

    public class DeliveryStatus
    {
        public string tComponent { get; set; }
        public Nullable<DateTime> dDeliveryDate { get; set; }
        public string tStatus { get; set; }
    }

    public class DateChangeNotitication
    {
        public string tComponent { get; set; }
        public string tVendor { get; set; }
        public bool isSelected { get; set; }
    }

    public class DateChangeBody
    {
        public int nStoreId { get; set; }
        public List<DateChangeNotitication> lstItems { get; set; }
    }

    public class DateChangeNotificationBody
    {
        public string tTo { get; set; }
        public string tCC { get; set; }
        public string tSubject { get; set; }
        public string tContent { get; set; }
        public int nStoreId { get; set; }
        public int? nCreatedBy { get; set; }
        public DateTime? dtCreatedOn { get; set; }
    }

    public class DateChangePOOption
    {
        public int nStoreId { get; set; }
        public int nPOId { get; set; }
        public string tPONumber { get; set; }
        public bool isSelected { get; set; }
    }

    public class DocumentationTable
    {
        public int nStoreId { get; set; }
        public int nProjectId { get; set; }
        public int nPOId { get; set; }
        public string tFileName { get; set; }
        public string tStoreNumber { get; set; }
        public string tSentBy { get; set; }
        public DateTime dtCreatedOn { get; set; }
    }

}