Alter procedure sproc_GetBrandByUser
@aUserID as int=0  
as            
BEGIN    
if(@aUserID>0)  
begin  
	select nBrandID from tblUserBrandRel with(nolock) where nUserID = @aUserID
end  
else  
begin  
	select nBrandID from tblUserBrandRel with(nolock) 
end  
END

GO

Alter table tblUserBrandRel ADD PRIMARY KEY (aUserBrandRelID)

GO
Alter table tbPermission ADD UNIQUE(tPermissionName)
GO
truncate table tbPermission
-- Insert Permissions
insert into tbPermission (tPermissionName,tPermissionDiplayName) values('home.configuration', 'home.configuration'),('home.configuration.dashboard', 'home.configuration.dashboard'),('home.configuration.Users','home.configuration.Users'),
('home.configuration.Brands','home.configuration.Brands'), ('home.configuration.Vendors','home.configuration.Vendors'), ('home.configuration.managedropdown','home.configuration.managedropdown'),
('home.configuration.Tech Components','home.configuration.Tech Components'), ('home.configuration.Franchises','home.configuration.Franchises'), ('home.configuration.quoterequest', 'home.configuration.quoterequest'),
('home.configuration.po', 'home.configuration.po'),('home.configuration.Items','home.configuration.ItemsPart'),
('home.configuration.Stores','home.configuration.Stores'),('home.configuration.Tech Component Tools','home.configuration.Tech Component Tools'),('home.configuration.Analytics','home.configuration.Analytics'),
('home.configuration.Store Contact','home.configuration.Store Contact'),('home.configuration.Store Configuration','home.configuration.Store Configuration'),('home.configuration.Stake Holders','home.configuration.Stake Holders'),
('home.configuration.Networking','home.configuration.Networking'),('home.configuration.POS','home.configuration.POS'),('home.configuration.Audio','home.configuration.Audio'),
('home.configuration.Exterior Menus','home.configuration.Exterior Menus'),('home.configuration.Payment System','home.configuration.Payment System'),('home.configuration.Interior Menus','home.configuration.Interior Menus'),
('home.configuration.Sonic Radio','home.configuration.Sonic Radio'),('home.configuration.Installation','home.configuration.Installation'),('home.configuration.Projects','home.configuration.Projects'),
('home.configuration.Historical Projects','home.configuration.Historical Projects'),('home.configuration.Notes','home.configuration.Notes'),
('home.dashboard.user', 'home.dashboard.user'), ('home.dashboard.brand', 'home.dashboard.brand'), ('home.dashboard.vendor', 'home.dashboard.vendor'),
('home.dashboard.managedropdown', 'home.dashboard.managedropdown'),('home.dashboard.report', 'home.dashboard.report'), ('home.dashboard.techarea', 'home.dashboard.techarea'),
('home.dashboard.setting', 'home.dashboard.setting'), ('home.dashboard.franchise', 'home.dashboard.franchise'),('home.dashboard.quoterequest', 'home.dashboard.quoterequest'),
('home.dashboard.po', 'home.dashboard.po'), ('home.dashboard', 'home.dashboard'), ('home.notification', 'home.notification'),
('home.mail', 'home.mail'), ('home.find', 'home.find'), ('home.list', 'home.list'), ('home.setting', 'home.setting'), ('home.dashboard.priority', 'home.dashboard.priority'),
('home.dashboard.goal', 'home.dashboard.goal'), ('home.dashboard.notification', 'home.dashboard.notification'), ('home.dashboard.request', 'home.dashboard.request'),
('home.sonic.notificationtemplate.render', 'home.sonic.notificationtemplate.render'), ('home.sonic.quoterequest.render', 'home.sonic.quoterequest.render'),('home.sonic.po.render', 'home.sonic.po.render'),
('home.sonic.viewstore', 'home.sonic.viewstore'), ('home.sonic.storehighlight', 'home.sonic.storehighlight'), ('home.sonic.newproject', 'home.sonic.newproject'),('home.sonic.project.workflows', 'home.sonic.project.workflows'),
('home.sonic.searchproject', 'home.sonic.searchproject'), ('home.sonic.importproject', 'home.sonic.importproject'), ('home.sonic.projectportfolio', 'home.sonic.projectportfolio'),
('home.sonic.reportgenerator', 'home.sonic.reportgenerator'), ('home.sonic.report', 'home.sonic.report'), ('home.sonic.project.golive', 'home.sonic.project.golive'),
('home.sonic.project.task', 'home.sonic.project.task'), ('home.sonic.project.notes', 'home.sonic.project.notes'), ('home.sonic.project.activeproject', 'home.sonic.project.activeproject'),
('home.sonic.project.historicproject', 'home.sonic.project.historicproject'), 
('home.sonic.project.Items','home.sonic.project.ItemsPart'),('home.sonic.project.Users','home.sonic.project.Users'),('home.sonic.project.Brands','home.sonic.project.Brands'),
('home.sonic.project.Vendors','home.sonic.project.Vendors'),('home.sonic.project.Tech Components','home.sonic.project.Tech Components'),('home.sonic.project.Franchises','home.sonic.project.Franchises'),
('home.sonic.project.Stores','home.sonic.project.Stores'),('home.sonic.project.Tech Component Tools','home.sonic.project.Tech Component Tools'),('home.sonic.project.Analytics','home.sonic.project.Analytics'),
('home.sonic.project.Store Contact','home.sonic.project.Store Contact'),('home.sonic.project.Store Configuration','home.sonic.project.Store Configuration'),('home.sonic.project.Stake Holders','home.sonic.project.Stake Holders'),
('home.sonic.project.Networking','home.sonic.project.Networking'),('home.sonic.project.POS','home.sonic.project.POS'),('home.sonic.project.Audio','home.sonic.project.Audio'),
('home.sonic.project.Exterior Menus','home.sonic.project.Exterior Menus'),('home.sonic.project.Payment System','home.sonic.project.Payment System'),('home.sonic.project.Interior Menus','home.sonic.project.Interior Menus'),
('home.sonic.project.Sonic Radio','home.sonic.project.Sonic Radio'),('home.sonic.project.Installation','home.sonic.project.Installation'),
('home.sonic.project.Projects','home.sonic.project.Projects'),('home.sonic.project.Historical Projects','home.sonic.project.Historical Projects')

Go

Alter table tblUserPermissionRel add nPermVal int not null
alter table tblRolePermissionRel add nPermVal int not null default 1

GO
if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Read Only'))
	Insert into tblRole(tRoleName) values('Read Only')
if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Franchise'))
	Insert into tblRole(tRoleName) values('Franchise')
if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Installation Vendor'))
	Insert into tblRole(tRoleName) values('Installation Vendor')
if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Equipment Vendor'))
	Insert into tblRole(tRoleName) values('Equipment Vendor')

-- Insert Role Permissions
declare @nRoleID int
Select @nRoleID = select aRoleID from tblRole where tRoleName  = 'Super Admin'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = select aRoleID from tblRole where tRoleName  = 'Admin'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = select aRoleID from tblRole where tRoleName  = 'Project Manage'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = select aRoleID from tblRole where tRoleName  = 'Franchise'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = select aRoleID from tblRole where tRoleName  = 'Installation Vendor'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = select aRoleID from tblRole where tRoleName  = 'Equipment Vendor'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = select aRoleID from tblRole where tRoleName  = 'Read Only'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 1 from tbPermission

update tblRolePermissionRel set nPermVal = 1 where nRoleID in (select aRoleID from tblRole where tRoleName in('Admin', 'Project Manager')) and nPermissionID in(Select aPermissionID from tblPermission where tPermissionName in ('home.configuration.Users', 'home.configuration.Brands'))
update tblRolePermissionRel set nPermVal = 0 where nRoleID in (select aRoleID from tblRole where tRoleName in('Franchise', 'Installation Vendor', 'Equipment Vendor')) and 
nPermissionID in(Select aPermissionID from tblPermission where tPermissionName in ('home.configuration', 'home.dashboard.report', 'home.sonic.reportgenerator', 'home.sonic.importproject', 'home.sonic.projectportfolio', 'home.sonic.reportgenerator',
'home.sonic.project.workflows')


GO

create procedure sproc_ChangeUserPermissionFromRole
@nUserId int,
@nRoleId int
as
BEGIN
	delete from tblUserPermissionRel where nUserID = @nUserId
	Insert into tblUserPermissionRel(nUserID, nPermissionID, nPermVal) Select @nUserId, nPermissionID, nPermVal from tblRolePermissionRel with(nolock) where nRoleID = @nRoleId
END

Go

Create procedure sproc_getMyAccess
@nuserId int
as
BEGIN
	Select tPermissionName, nPermVal from tblUserPermissionRel with(nolock) join tbPermission with(nolock) on aPermissionlID = nPermissionId where nUserId = @nUserId
END

GO

Create procedure sproc_getAccesibleBrand
@nuserId int
as
BEGIN
	select * from tblBrand with(nolock) join tblUserBrandRel with(nolock) on aBrandId = nBrandID where nUserID = @nUserId
END

Go

Alter  PROC sproc_UserLogin               
(              
@tUserName NVARCHAR(255),              
@tPassword NVARCHAR(255)             
)              
AS              
BEGIN              
   select tName, tEmail, nRole, aUserID nUserID from tblUser with(nolock) where UPPER(tUserName) = UPPER(@tUserName) and tPassword = @tPassword  
END 


GO

Alter table tblFranchise drop column tFranchiseDescription, tFranchiseLocation, dFranchiseEstablished, tFranchiseContact, tFranchiseOwner, nFranchiseEmployeeCount, nFranchiseRevenue
Alter table tblFranchise add tFranchiseAddress2 VARCHAR(5000)
Alter table tblFranchise add tFranchiseCity VARCHAR(100)
Alter table tblFranchise add nFranchiseState int
Alter table tblFranchise add tFranchiseZip VARCHAR(100)

Go

alter table tblVendor drop column nTechComponentID, tVendorDescription, tVendorContactPerson, tVendorCountry, tVendorEstablished

Go

Alter procedure sproc_getUserModel        
@nVendorId as int = 0,
@nFranchiseId as int = 0 
as         
BEGIN        
       
if(@nVendorId=0 and @nFranchiseId = 0)      
Select *, 1 nVendorId from tblUser A with(nolock) where bDeleted is null or bDeleted = 0  
 --inner join tblUserVendorRel B with(nolock) on  A.bDeleted=B.nPartID      
 --where nVendorId=@nVendorId      
 else if(@nVendorId <> 0)
 Select *, nVendorId from tblUser A with(nolock)       
 inner join tblUserVendorRel B with(nolock) on  A.aUserID=B.nUserID      
 where nVendorId=@nVendorId and bDeleted is null or bDeleted = 0  
else if(@nFranchiseId <> 0)
	Select *, 1 nVendorId from tblUser A with(nolock) where bDeleted is null or bDeleted = 0  
 --Select *, nVendorId from tblUser A with(nolock)       
 --inner join tblUserFranchiseRel B with(nolock) on  A.aUserID=B.nUserID      
 --where nVendorId=@nVendorId and bDeleted is null or bDeleted = 0   
END

