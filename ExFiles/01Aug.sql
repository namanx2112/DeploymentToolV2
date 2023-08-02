--sproc_GetDropdown ''
Alter Procedure [dbo].[sproc_GetDropdown]        
@tModuleName as VARCHAR(500)        
as        
BEGIN        
 IF(@tModuleName is null OR @tModuleName = '')        
 BEGIN        
  Select nBrandId, tModuleName, aDropDownId, tDropdownText, tblDropdowns.bDeleted from tblDropdownMain  with(nolock) join tblDropdowns with(nolock) on        
  aDropdownId = nDropdownId  UNION        
 Select 1, 'Vendor', aVendorId, tVendorName, bDeleted from tblVendor with(nolock)  UNION    
 Select 1, 'Franchise', aFranchiseId, tFranchiseName,bDeleted from tblFranchise  with(nolock) UNION
 select 1, 'UserRole', aRoleID, tRoleName, bDeleted from tblRole with(nolock) UNION
 select 1, 'Brand', aBrandId, tBrandName, bDeleted from tblBrand with(nolock)
     
 END        
 ELSE         
 BEGIN        
 IF(@tModuleName = 'Vendor')      
 BEGIN      
 Select nBrand, @tModuleName, aVendorId, tVendorName, bDeleted from tblVendor      
 END      
 ELSE      
 BEGIN      
 Select 1, tModuleName, aDropDownId, tDropdownText, tblDropdowns.bDeleted from tblDropdownMain join tblDropdowns on        
  aDropdownId = nDropdownId where tModuleName = @tModuleName         
  END      
 END        
END 