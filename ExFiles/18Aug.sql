alter table tblSupportTicket add tTicketStatus varchar(100), tFixComment NVARCHAR(MAX), dtUpdatedOn datetime, nUpdateBy int


Alter proc sproc_getSupportTicket
AS
BEGIN
	select aTicketId,nPriority,tContent,nFileSie,fBase64,tName as tCreatedBy,tblSupportTicket.nCreatedBy, tblSupportTicket.dtCreatedOn,tTicketStatus,tFixComment from  tblSupportTicket with(nolock) join tblUser with(nolock) on tblSupportTicket.nCreatedBy = aUserID
	order by aTicketId desc
END

GO

update tblDropdownModule set nBrandID = 0 where tModuleGroup in('User','Vendor')

select * from tblDropdownModule

create table tblDropdownModuleBrandRel(aRelId int identity primary key, nBrandId int not null, nModuleId int not null)

alter table tblDropdownModule drop column nBrandId

Insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) select 0, aModuleId from tblDropdownModule where tModuleGroup in('User','Vendor')

select * from tblBrand

Insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) select 2, aModuleId from tblDropdownModule where tModuleGroup not in('User','Vendor','All')

update tblDropdownModuleBrandRel set nBrandId = 6 where nModuleId in (select aModuleId from tblDropdownModule where tModuleGroup in('All'))

GO

Alter procedure sproc_getDropdownModules  
@nBrandId int  
AS  
BEGIN  
 select aModuleId,nBrandId,tModuleName,tModuleDisplayName,tModuleGroup, editable from tblDropdownModule with(nolock) join tblDropdownModuleBrandRel with(nolock) on aModuleId = nModuleId
END

GO

ALTER procedure sproc_SearchStore            
@tText as VARCHAR(500),  
@nBrandID int=0  
AS            
BEGIN    
    
Select nStoreId, tStoreName,tStoreNumber,tProjectsInfo, @nBrandID nBrandId  
from(    
 select aStoreId nStoreId, tStoreName,tStoreNumber, tProjectsInfo
 from tblStore with(nolock)  Join    
 (select nStoreId, STRING_AGG(CAST(aProjectId as varchar) + '_' + CAST(nProjectType as varchar) + '_' + CAST(dGoLiveDate as varchar) , ', ') tProjectsInfo from tblProject with(nolock) where  nBrandID=@nBrandID and nProjectActiveStatus = 1    
 --where ProjectActiveStatus = 1     
 group by nStoreID) tblProj on aStoreID = nStoreID     
 where   nBrandID=@nBrandID and  
 tStoreNumber like ('%' + @tText + '%') OR    
 tStoreName like ('%' + @tText + '%')    
 ) as tmpTable order by tStoreNumber    
 --Select nProjectId, tStoreName,tProjectName,tStoreNumber, tDropdownText tProjectType, dGoLiveDate       
 --from tblProjectStore projstore with(nolock) join tblProject proj with(nolock) on           
 --proj.aProjectId = projstore.nProjectId join  tblStore store with(nolock) on       
 --store.aStoreID = proj.nStoreID FULL OUTER JOIN          
 --tblDropdowns pType with(nolock) on pType.aDropDownId =          
 --(case when nProjectType is null then 4          
 --else nProjectType END)           
 --where proj.projectActiveStatus=1 and (tStoreName like ('%' + @tText + '%') OR proj.tProjectName like ('%' + @tText + '%') OR            
 --store.tStoreNumber like ('%' + @tText + '%'))) tProject where          
 --tProjectType like ('%' + @tText + '%')          
        
 --order by nProjectID desc        
END 

GO

Alter Procedure [dbo].[sproc_GetDropdown]            
@tModuleName as VARCHAR(500),  
@nUserId int        
as            
BEGIN            
 IF(@tModuleName is null OR @tModuleName = '')            
 BEGIN            
  Select tblDropdownModuleBrandRel.nBrandId, tblDropdownMain.tModuleName, aDropDownId, tDropdownText, tblDropdowns.bDeleted, nOrder, nFunction from tblDropdownMain  with(nolock) join tblDropdowns with(nolock) on            
  aDropdownId = nDropdownId join tblDropdownModule with(nolock) on tblDropdownMain.tModuleName = tblDropdownModule.tModuleName join tblDropdownModuleBrandRel with(nolock) on aModuleId = nModuleId  UNION            
 Select 0, 'Vendor', aVendorId, tVendorName, bDeleted, 1, 0 from tblVendor with(nolock)  UNION        
 Select 0, 'Franchise', aFranchiseId, tFranchiseName,bDeleted,1, 0 from tblFranchise  with(nolock) UNION    
 select 0, 'UserRole', aRoleID, tRoleName, CONVERT(bit, 0),1, 0 from tblRole with(nolock) UNION    
 select 0, 'Brand', aBrandId, tBrandName, bDeleted,1, 0 from tblBrand with(nolock) join tblUserBrandRel with(nolock) on nBrandID = aBrandID where nUserID = @nUserID          
 END            
 ELSE             
 BEGIN            
 IF(@tModuleName = 'Vendor')          
 BEGIN          
 Select nBrand, @tModuleName, aVendorId, tVendorName, bDeleted,1, 0 from tblVendor with(nolock) order by tVendorName     
 END          
 ELSE IF(@tModuleName = 'UserRole')          
 BEGIN          
 select 0, 'UserRole' tModuleName, aRoleID aDropdownId, tRoleName tDropdownText, CONVERT(bit, 0),1 nFunction, 0 nFunction from tblRole with(nolock)
 END          
 ELSE          
 BEGIN          
 Select tblDropdownModuleBrandRel.nBrandId, tblDropdownMain.tModuleName, aDropDownId, tDropdownText, tblDropdowns.bDeleted, nOrder, nFunction from tblDropdownMain  with(nolock) join tblDropdowns with(nolock) on            
  aDropdownId = nDropdownId join tblDropdownModule with(nolock) on tblDropdownMain.tModuleName = tblDropdownModule.tModuleName join tblDropdownModuleBrandRel with(nolock) on aModuleId = nModuleId
  where tblDropdownMain.tModuleName = @tModuleName and (tblDropdowns.bDeleted is null or tblDropdowns.bDeleted = 0)  order by nOrder         
  END          
 END            
END 

GO

sp_tables '%modul%'

Select * from tblDropdownModule

 Select tblDropdownModuleBrandRel.nBrandId, tblDropdownMain.tModuleName, aDropDownId, tDropdownText, tblDropdowns.bDeleted, nOrder, nFunction from tblDropdownMain  with(nolock) join tblDropdowns with(nolock) on            
  aDropdownId = nDropdownId join tblDropdownModule with(nolock) on tblDropdownMain.tModuleName = tblDropdownModule.tModuleName join tblDropdownModuleBrandRel with(nolock) on aModuleId = nModuleId

select * from tblDropdownMain where tModuleName = 'VendorType'

sp_helptext sproc_GetDropdown

sproc_GetDropdown '',2

select * from tblBrand

sproc_getDropdownModules 2

select * from tblDropdownModule where tModuleName like '%state%'

select * from tblDropdownMain where tModuleName = 'UserRole'
select * from tblDropdowns where 

sproc_SearchStore '', 6