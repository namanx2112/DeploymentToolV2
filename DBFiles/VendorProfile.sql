
create table tblRoleFieldRestrictedAccess(aAccessId int identity primary key, nRoleId int, tTechCompName varchar(500), tFieldName varchar(500), nAccessVal int default 2)

-------------Sample Date for different Tech Components
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'POS','nVendor', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'POS','dDeliveryDate', 2)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'POS','dConfigDate', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'POS','dSupportDate', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'POS','nStatus', 2)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'POS','nPaperworkStatus', 2)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'POS','cCost', 2)

insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'Networking','nVendor', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'Audio','nVendor', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'Exterior Menus','nVendor', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'Payment System','nVendor', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'Interior Menus','nVendor', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'Radio','nVendor', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'Sonic Radio','nVendor', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'Server Handheld','nVendor', 1)
insert into tblRoleFieldRestrictedAccess(nRoleId, tTechCompName, tFieldName, nAccessVal) values(6, 'Installation','nVendor', 1)

GO
drop proc sproc_GetMyRestrictedAccess
go
create proc sproc_GetMyRestrictedAccess
@nUserId int
as
BEGIN
	declare @nEquipmentRoleId int, @nInstallationRoleId int, @nVendorId int, @nInstallation int, @nEquipment int
	Select top 1 @nVendorId = nVendorID from tblUserVendorRel with(nolock) where nUserID = @nUserId
	if(@nVendorId is not null)
	BEGIN
		select @nEquipmentRoleId = aRoleId from tblRole with(nolock) where tRoleName = 'Equipment Vendor'
		select @nInstallationRoleId = aRoleId from tblRole with(nolock) where tRoleName = 'Installation Vendor'
		Select @nInstallation = nInstallation, @nEquipment = nEquipment from tblVendor with(nolock) where aVendorId = @nVendorId
		--if(@nInstallation != 0)
		if(@nEquipment != 0)
		BEGIN
			select tTechCompName,tFieldName,nAccessVal from tblRoleFieldRestrictedAccess with(nolock) where nRoleId = @nEquipmentRoleId
		END
	END
END

GO
drop proc sproc_getUserMeta
GO
create proc sproc_getUserMeta
@nUserId int
as
BEGIN
	declare @nEquipmentRoleId int, @nInstallationRoleId int, @nVendorId int, @nInstallation int, @nEquipment int, @nUserType int
	Select top 1 @nVendorId = nVendorID from tblUserVendorRel with(nolock) where nUserID = @nUserId
	set @nUserType = 0
	if(@nVendorId is not null)
	BEGIN
		Select @nInstallation = nInstallation, @nEquipment = nEquipment from tblVendor with(nolock) where aVendorId = @nVendorId		
		if(@nInstallation = 1 and @nEquipment = 1)
			set @nUserType = 4
		else if(@nInstallation = 1)
			set @nUserType = 3
		else if(@nEquipment = 1)
			set @nUserType = 2
	END
	ELSE
		SET @nVendorId = 0
	
	select @nVendorId nOriginatorId, @nUserType userType
END

GO

drop proc sproc_GetDropdown
GO
CREATE Procedure [dbo].[sproc_GetDropdown]              
@tModuleName as VARCHAR(500),    
@nUserId int          
as              
BEGIN              
 IF(@tModuleName is null OR @tModuleName = '')              
 BEGIN              
  Select nBrandId, tModuleName, aDropDownId, tDropdownText, tblDropdowns.bDeleted, nOrder, nFunction from tblDropdowns  with(nolock) join tblDropdownModule with(nolock) on   
  nModuleId = aModuleId UNION              
 Select 0, 'Vendor', aVendorId, tVendorName, bDeleted, 1, 0 from tblVendor with(nolock)  UNION          
 Select 0, 'Franchise', aFranchiseId, tFranchiseName,bDeleted,1, 0 from tblFranchise  with(nolock) UNION      
 select 0, 'UserRole', aRoleID, tRoleName, CONVERT(bit, 0),1, 0 from tblRole with(nolock) UNION      
 select 0, 'Brand', aBrandId, tBrandName, bDeleted,1, 0 from tblBrand with(nolock) join tblUserBrandRel with(nolock) on nBrandID = aBrandID where nUserID = @nUserID
 UNION select 0, 'User', aUserId, tUserName, bDeleted, 1, 0 from tblUser with(nolock)
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
 Select nBrandId, tModuleName, aDropDownId, tDropdownText, tblDropdowns.bDeleted, nOrder, nFunction from tblDropdowns  with(nolock) join tblDropdownModule with(nolock) on   
  nModuleId = aModuleId  
  where tModuleName = @tModuleName and (tblDropdowns.bDeleted is null or tblDropdowns.bDeleted = 0)  order by nOrder           
  END            
 END              
END   

GO

--TO Show notes for not read only user template
update tblRolePermissionRel set nPermVal = 2 where nPermissionID in (select aPermissionlID from tbPermission where tPermissionName = 'home.sonic.project.notes')
and nRoleID in (select aRoleID from tblRole where tRoleName != 'Read Only')

--TO Show notes for not read only user
update tblUserPermissionRel set nPermVal = 2 where nUserID in(select aUserID from tblUser where nRole in(select aRoleId from tblRole where tRoleName != 'Read Only'))
and nPermissionID in(select aPermissionlID from tbPermission where tPermissionName = 'home.sonic.project.notes')