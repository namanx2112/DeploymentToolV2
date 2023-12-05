﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeploymentTool
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class dtDBEntities : DbContext
    {
        public dtDBEntities()
            : base("name=dtDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblBrand> tblBrand { get; set; }
        public virtual DbSet<tblUser> tblUser { get; set; }
        public virtual DbSet<tblVendor> tblVendor { get; set; }
        public virtual DbSet<tblAttachment> tblAttachments { get; set; }
        public virtual DbSet<tblProject> tblProjects { get; set; }
        public virtual DbSet<tblProjectAudio> tblProjectAudios { get; set; }
        public virtual DbSet<tblProjectConfig> tblProjectConfigs { get; set; }
        public virtual DbSet<tblProjectExteriorMenu> tblProjectExteriorMenus { get; set; }
        public virtual DbSet<tblProjectInstallation> tblProjectInstallations { get; set; }
        public virtual DbSet<tblProjectInteriorMenu> tblProjectInteriorMenus { get; set; }
        public virtual DbSet<tblProjectNetworking> tblProjectNetworkings { get; set; }
        public virtual DbSet<tblProjectNote> tblProjectNotes { get; set; }
        public virtual DbSet<tblProjectPaymentSystem> tblProjectPaymentSystems { get; set; }
        public virtual DbSet<tblProjectPOS> tblProjectPOS { get; set; }
        public virtual DbSet<tblProjectSonicRadio> tblProjectSonicRadios { get; set; }
        public virtual DbSet<tblProjectStakeHolder> tblProjectStakeHolders { get; set; }
        public virtual DbSet<tblStore> tblStores { get; set; }
        public virtual DbSet<tblUser1> tblUsers1 { get; set; }
        public virtual DbSet<tblBrandBKP> tblBrandBKPs { get; set; }
        public virtual DbSet<tblCountry> tblCountries { get; set; }
        public virtual DbSet<tblEnum> tblEnums { get; set; }
        public virtual DbSet<tblEnumTableField> tblEnumTableFields { get; set; }
        public virtual DbSet<tblFranchise> tblFranchises { get; set; }
        public virtual DbSet<tblFunction> tblFunctions { get; set; }
        public virtual DbSet<tblState> tblStates { get; set; }
        public virtual DbSet<tblTechComponent> tblTechComponents { get; set; }
        public virtual DbSet<tblUserBrandRel> tblUserBrandRels { get; set; }
        public virtual DbSet<tblUserFunctionRel> tblUserFunctionRels { get; set; }
        public virtual DbSet<tblDropdown> tblDropdowns { get; set; }
        public virtual DbSet<tblDocument> tblDocuments { get; set; }
        public virtual DbSet<tblApplicationTrace> tblApplicationTraces { get; set; }
        public virtual DbSet<tblQuoteRequestMain> tblQuoteRequestMains { get; set; }
        public virtual DbSet<tblQuoteRequestTechComp> tblQuoteRequestTechComps { get; set; }
        public virtual DbSet<tblQuoteRequestTechCompField> tblQuoteRequestTechCompFields { get; set; }
        public virtual DbSet<tblTrace> tblTraces { get; set; }
        public virtual DbSet<tblPurchaseOrderTemplate> tblPurchaseOrderTemplates { get; set; }
        public virtual DbSet<tblOutgoingEmail> tblOutgoingEmails { get; set; }
        public virtual DbSet<tblOutgoingEmailAttachment> tblOutgoingEmailAttachments { get; set; }
        public virtual DbSet<tblPart> tblParts { get; set; }
        public virtual DbSet<tblPurchaseOrder> tblPurchaseOrders { get; set; }
        public virtual DbSet<tblPurchaseOrderPart> tblPurchaseOrderParts { get; set; }
        public virtual DbSet<tblPurchaseOrderTemplatePart> tblPurchaseOrderTemplateParts { get; set; }
        public virtual DbSet<tblVendorPartRel> tblVendorPartRels { get; set; }
        public virtual DbSet<tblUserVendorRel> tblUserVendorRels { get; set; }
        public virtual DbSet<tblSupportTicket> tblSupportTickets { get; set; }
        public virtual DbSet<tblRole> tblRoles { get; set; }
        public virtual DbSet<tblRolePermissionRel> tblRolePermissionRels { get; set; }
        public virtual DbSet<tblUserAndUserTypeRel> tblUserAndUserTypeRels { get; set; }
        public virtual DbSet<tblUserPermissionRel> tblUserPermissionRels { get; set; }
        public virtual DbSet<tblUserRoleRel> tblUserRoleRels { get; set; }
        public virtual DbSet<tblUserType> tblUserTypes { get; set; }
        public virtual DbSet<tbPermission> tbPermissions { get; set; }
        public virtual DbSet<tblDropdownModule> tblDropdownModules { get; set; }
        public virtual DbSet<tblUserFranchiseRel> tblUserFranchiseRels { get; set; }
        public virtual DbSet<tblNoteType> tblNoteTypes { get; set; }
        public virtual DbSet<tblProjectNote1> tblProjectNote1 { get; set; }
        public virtual DbSet<tblProjectServerHandheld> tblProjectServerHandhelds { get; set; }
        public virtual DbSet<tblProjectImageOrMemory> tblProjectImageOrMemories { get; set; }
        public virtual DbSet<tblProjectNetworkSwitch> tblProjectNetworkSwitches { get; set; }
        public virtual DbSet<tblProjectOrderAccuracy> tblProjectOrderAccuracies { get; set; }
        public virtual DbSet<tblProjectOrderStatusBoard> tblProjectOrderStatusBoards { get; set; }
        public virtual DbSet<tblProjectsRollout> tblProjectsRollouts { get; set; }
        public virtual DbSet<tblProjectsRolloutStoreRel> tblProjectsRolloutStoreRels { get; set; }
        public virtual DbSet<tblProjectsRolloutTechRel> tblProjectsRolloutTechRels { get; set; }
        public virtual DbSet<tblReportFolder> tblReportFolders { get; set; }
        public virtual DbSet<tblDisplayColumn> tblDisplayColumns { get; set; }
        public virtual DbSet<tblField> tblFields { get; set; }
        public virtual DbSet<tblFieldGroup> tblFieldGroups { get; set; }
        public virtual DbSet<tblFieldTypeOperatorRel> tblFieldTypeOperatorRels { get; set; }
        public virtual DbSet<tblFieldTypeOperatorValueRel> tblFieldTypeOperatorValueRels { get; set; }
        public virtual DbSet<tblFieldType> tblFieldTypes { get; set; }
        public virtual DbSet<tblOperator> tblOperators { get; set; }
        public virtual DbSet<tblReport> tblReports { get; set; }
        public virtual DbSet<tblReportColumn> tblReportColumns { get; set; }
        public virtual DbSet<tblSortColumn> tblSortColumns { get; set; }
        public virtual DbSet<tblFilterCondition> tblFilterConditions { get; set; }
    
        public virtual int sproc_CreateStoreFromExcel(string tStoreName, Nullable<int> nProjectType, string tStoreNumber, string tAddress, string tCity, string tState, Nullable<int> nDMAID, string tDMA, string tRED, string tCM, string tANE, string tRVP, string tPrincipalPartner, Nullable<System.DateTime> dStatus, Nullable<System.DateTime> dOpenStore, string tProjectStatus, Nullable<int> nCreatedBy, Nullable<int> nBrandId, Nullable<int> nNumberOfTabletsPerStore, string tEquipmentVendor, Nullable<System.DateTime> dShipDate, Nullable<System.DateTime> dRevisitDate, Nullable<System.DateTime> dInstallDate, string tInstallationVendor, string tInstallStatus)
        {
            var tStoreNameParameter = tStoreName != null ?
                new ObjectParameter("tStoreName", tStoreName) :
                new ObjectParameter("tStoreName", typeof(string));
    
            var nProjectTypeParameter = nProjectType.HasValue ?
                new ObjectParameter("nProjectType", nProjectType) :
                new ObjectParameter("nProjectType", typeof(int));
    
            var tStoreNumberParameter = tStoreNumber != null ?
                new ObjectParameter("tStoreNumber", tStoreNumber) :
                new ObjectParameter("tStoreNumber", typeof(string));
    
            var tAddressParameter = tAddress != null ?
                new ObjectParameter("tAddress", tAddress) :
                new ObjectParameter("tAddress", typeof(string));
    
            var tCityParameter = tCity != null ?
                new ObjectParameter("tCity", tCity) :
                new ObjectParameter("tCity", typeof(string));
    
            var tStateParameter = tState != null ?
                new ObjectParameter("tState", tState) :
                new ObjectParameter("tState", typeof(string));
    
            var nDMAIDParameter = nDMAID.HasValue ?
                new ObjectParameter("nDMAID", nDMAID) :
                new ObjectParameter("nDMAID", typeof(int));
    
            var tDMAParameter = tDMA != null ?
                new ObjectParameter("tDMA", tDMA) :
                new ObjectParameter("tDMA", typeof(string));
    
            var tREDParameter = tRED != null ?
                new ObjectParameter("tRED", tRED) :
                new ObjectParameter("tRED", typeof(string));
    
            var tCMParameter = tCM != null ?
                new ObjectParameter("tCM", tCM) :
                new ObjectParameter("tCM", typeof(string));
    
            var tANEParameter = tANE != null ?
                new ObjectParameter("tANE", tANE) :
                new ObjectParameter("tANE", typeof(string));
    
            var tRVPParameter = tRVP != null ?
                new ObjectParameter("tRVP", tRVP) :
                new ObjectParameter("tRVP", typeof(string));
    
            var tPrincipalPartnerParameter = tPrincipalPartner != null ?
                new ObjectParameter("tPrincipalPartner", tPrincipalPartner) :
                new ObjectParameter("tPrincipalPartner", typeof(string));
    
            var dStatusParameter = dStatus.HasValue ?
                new ObjectParameter("dStatus", dStatus) :
                new ObjectParameter("dStatus", typeof(System.DateTime));
    
            var dOpenStoreParameter = dOpenStore.HasValue ?
                new ObjectParameter("dOpenStore", dOpenStore) :
                new ObjectParameter("dOpenStore", typeof(System.DateTime));
    
            var tProjectStatusParameter = tProjectStatus != null ?
                new ObjectParameter("tProjectStatus", tProjectStatus) :
                new ObjectParameter("tProjectStatus", typeof(string));
    
            var nCreatedByParameter = nCreatedBy.HasValue ?
                new ObjectParameter("nCreatedBy", nCreatedBy) :
                new ObjectParameter("nCreatedBy", typeof(int));
    
            var nBrandIdParameter = nBrandId.HasValue ?
                new ObjectParameter("nBrandId", nBrandId) :
                new ObjectParameter("nBrandId", typeof(int));
    
            var nNumberOfTabletsPerStoreParameter = nNumberOfTabletsPerStore.HasValue ?
                new ObjectParameter("nNumberOfTabletsPerStore", nNumberOfTabletsPerStore) :
                new ObjectParameter("nNumberOfTabletsPerStore", typeof(int));
    
            var tEquipmentVendorParameter = tEquipmentVendor != null ?
                new ObjectParameter("tEquipmentVendor", tEquipmentVendor) :
                new ObjectParameter("tEquipmentVendor", typeof(string));
    
            var dShipDateParameter = dShipDate.HasValue ?
                new ObjectParameter("dShipDate", dShipDate) :
                new ObjectParameter("dShipDate", typeof(System.DateTime));
    
            var dRevisitDateParameter = dRevisitDate.HasValue ?
                new ObjectParameter("dRevisitDate", dRevisitDate) :
                new ObjectParameter("dRevisitDate", typeof(System.DateTime));
    
            var dInstallDateParameter = dInstallDate.HasValue ?
                new ObjectParameter("dInstallDate", dInstallDate) :
                new ObjectParameter("dInstallDate", typeof(System.DateTime));
    
            var tInstallationVendorParameter = tInstallationVendor != null ?
                new ObjectParameter("tInstallationVendor", tInstallationVendor) :
                new ObjectParameter("tInstallationVendor", typeof(string));
    
            var tInstallStatusParameter = tInstallStatus != null ?
                new ObjectParameter("tInstallStatus", tInstallStatus) :
                new ObjectParameter("tInstallStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sproc_CreateStoreFromExcel", tStoreNameParameter, nProjectTypeParameter, tStoreNumberParameter, tAddressParameter, tCityParameter, tStateParameter, nDMAIDParameter, tDMAParameter, tREDParameter, tCMParameter, tANEParameter, tRVPParameter, tPrincipalPartnerParameter, dStatusParameter, dOpenStoreParameter, tProjectStatusParameter, nCreatedByParameter, nBrandIdParameter, nNumberOfTabletsPerStoreParameter, tEquipmentVendorParameter, dShipDateParameter, dRevisitDateParameter, dInstallDateParameter, tInstallationVendorParameter, tInstallStatusParameter);
        }
    
        public virtual ObjectResult<sproc_SearchStore_Result> sproc_SearchStore(string tText, Nullable<int> nBrandID)
        {
            var tTextParameter = tText != null ?
                new ObjectParameter("tText", tText) :
                new ObjectParameter("tText", typeof(string));
    
            var nBrandIDParameter = nBrandID.HasValue ?
                new ObjectParameter("nBrandID", nBrandID) :
                new ObjectParameter("nBrandID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sproc_SearchStore_Result>("sproc_SearchStore", tTextParameter, nBrandIDParameter);
        }
    }
}
