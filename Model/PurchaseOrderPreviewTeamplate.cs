﻿using DeploymentTool.Model.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace DeploymentTool.Model
{
    public class PurchaseOrderPreviewTeamplate: Misc.ModelParent
    {
        public int nProjectId { get; set; }
        public int aPurchaseOrderPreviewTeamplateID { get; set; }
        public int nVendorId { get; set; }
        public string tStore { get; set; }
        public string tStoreNumber { get; set; }
        public string tNotes { get; set; }
        public string tName { get; set; }
        public string tPhone { get; set; }
        public string tEmail { get; set; }
        public string tAddress { get; set; }
        public string tCity { get; set; }
        public string tStoreState { get; set; }
        public string tBillToCompany { get; set; }
        public string tStoreZip { get; set; }
        public string tBillToEmail { get; set; }
        public string tBillToAddress { get; set; }
        public string tBillToCity { get; set; }
        public string tBillToState { get; set; }
        public List<PurchaseOrderParts> purchaseOrderParts { get; set; }
        public decimal cTotal { get; set; }
        public string tPurchaseOrderNumber { get; set; }
        public DateTime dDeliver { get; set; }
        public int nOutgoingEmailID { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public string tTemplateName { get; set; }
    }

    public class PurchaseOrderTemplateTemp
    {
        public int aPurchaseOrderTemplateID { get; set; }
        public string tTemplateName { get; set; }
        public int nBrandId { get; set; }
        public int nVendorId { get; set; }
        public int nCreatedBy { get; set; }
        public int nUpdateBy { get; set; }
    }
    public class PurchaseOrderTeamplate
    {
        public int aPurchaseOrderTemplateID { get; set; }
        public string tTemplateName { get; set; }
        public int nBrandId { get; set; }
        public int nVendorId { get; set; }
        public string tCompName { get; set; }
        public int nCreatedBy { get; set; }
        public int nUpdateBy { get; set; }
        public List<PurchaseOrderParts> purchaseOrderParts { get; set; }
        public tblPurchaseOrderTemplate GetTblPurchaseOrder()
        {
            return new tblPurchaseOrderTemplate()
            {
                aPurchaseOrderTemplateID = this.aPurchaseOrderTemplateID,
                tTemplateName = this.tTemplateName,
                tTechnologyComponent = this.tCompName,
                nBrandID = this.nBrandId,
                nVendorID=this.nVendorId,
                nCreatedBy = this.nCreatedBy
            };
        }
    }
    public class PurchaseOrderParts
    {
        public int aPurchaseOrderTemplatePartsID { get; set; }
        public int aPurchaseOrderTemplateID { get; set; }
        
        public int nPartID { get; set; }
        public string tPartDesc { get; set; }
        public string tPartNumber { get; set; }
        public decimal cPrice { get; set; }
        public string tTableName { get; set; }
        public string tTechCompField { get; set; }
        public int nQuantity { get; set; }
        public decimal cTotal { get; set; }
    }

    public class PurchaseOrderMailMessage
    {
        public int nProjectId { get; set; }
        public string tTo { get; set; }
        public string tCC { get; set; }
        public string tSubject { get; set; }
        public string tContent { get; set; }
        public string tFileName { get; set; }

        public List<FileAttachment> FileAttachments { get; set; }
    }
	 public class PurchaseOrderPartDetails
    {
        public int aPurchaseOrderTemplateID { get; set; }
        public int nPartID { get; set; }

        public int nVenderId { get; set; }
        public int nPurchaseOrderPartDetailsID { get; set; }

        public string tTechCompField { get; set; }

        public string tTableName { get; set; }
        public string tPartDesc { get; set; }
        public string tPartNumber { get; set; }
        public decimal cPrice { get; set; }
        public int nQuantity { get; set; }
        public decimal cTotal { get; set; }
    }

}