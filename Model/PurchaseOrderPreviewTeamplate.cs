using DeploymentTool.Model.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class PurchaseOrderPreviewTeamplate
    {
        public int aPurchaseOrderPreviewTeamplateID { get; set; }
        public int nStore { get; set; }
        public PurchaseOrderBillingAndShippingDetails PurchaseOrderBillingAndShippingDetails { get; set; }
        public string tNotes { get; set; }
        public List<PurchaseOrderPartDetails> PurchaseOrderPartDetails { get; set; }
        public decimal cTotal { get; set; }
        public string tPurchaseOrderNumber { get; set; }
        public DateTime dDeliver { get; set; }
        public int nOutgoingEmailID { get; set; }
    }
    public class PurchaseOrderBillingAndShippingDetails
    {
        public string tName { get; set; }
        public string tPhone { get; set; }
        public string tEmail { get; set; }
        public string tAddress { get; set; }
        public PurchaseOrderAddressType nPurchaseOrderAddressType { get; set; }
    }
    public enum PurchaseOrderAddressType
    {
        Billing, Shipping
    }

    public class PurchaseOrderPartDetails
    {
        public int aPurchaseOrderTemplateID { get; set; }
        public int nPartID { get; set; }
        public int nPurchaseOrderPartDetailsID { get; set; }
        public string tPartDesc { get; set; }
        public string tPartNumber { get; set; }
        public decimal cPrice { get; set; }
        public int nQuantity { get; set; }
        public decimal cTotal { get; set; }
    }

    public class tblPurchaseOrderTemplateTemp
    {
        public int aPurchaseOrderTemplateID { get; set; }
        public string tTemplateName { get; set; }
        public int nBrandId { get; set; }
        public int nVenderID { get; set; }
        public int nCreatedBy { get; set; }
        public int nUpdateBy { get; set; }
    }
    public class PurchaseOrderTeamplate
    {
        public int aPurchaseOrderTemplateID { get; set; }
        public string tTemplateName { get; set; }
        public int nBrandId { get; set; }
        public int nVenderID { get; set; }
        public int nCreatedBy { get; set; }
        public int nUpdateBy { get; set; }
        public List<PurchaseOrderParts> purchaseOrderParts { get; set; }
    }
    public class PurchaseOrderParts
    {
        public int aPartID { get; set; }
        public int nVendorId { get; set; }
        public string tPartDesc { get; set; }
        public string tPartNumber { get; set; }
        public int cPrice { get; set; }
        public string tTableName { get; set; }
        public string tTechCompField { get; set; }
    }

}