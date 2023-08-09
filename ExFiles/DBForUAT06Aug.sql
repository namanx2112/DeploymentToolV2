Create procedure sproc_GetBrandByUser
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
/****** Object:  Table [dbo].[tbPermission]    Script Date: 8/6/2023 8:20:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbPermission](
	[aPermissionlID] [int] IDENTITY(1,1) NOT NULL,
	[tPermissionName] [nvarchar](1000) NULL,
	[tPermissionDiplayName] [nvarchar](1000) NULL,
	[nCreatedBy] [int] NULL,
	[nUpdateBy] [int] NULL,
	[dtCreatedOn] [datetime] NULL,
	[dtUpdatedOn] [datetime] NULL,
	[bDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[aPermissionlID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[tPermissionName] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[tPermissionName] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


GO
truncate table tbPermission
-- Insert Permissions
insert into tbPermission (tPermissionName,tPermissionDiplayName) values('home.configuration', 'home.configuration'),('home.configuration.dashboard', 'home.configuration.dashboard'),('home.configuration.Users','home.configuration.Users'),
('home.configuration.Brands','home.configuration.Brands'), ('home.configuration.Vendors','home.configuration.Vendors'), ('home.configuration.managedropdown','home.configuration.managedropdown'),('home.configuration.report', 'home.configuration.report'),
('home.configuration.Tech Components','home.configuration.Tech Components'), ('home.configuration.Franchises','home.configuration.Franchises'), ('home.configuration.quoterequest', 'home.configuration.quoterequest'),('home.configuration.techarea', 'home.configuration.techarea'),
('home.configuration.po', 'home.configuration.po'),('home.configuration.Items','home.configuration.ItemsPart'),('home.configuration.setting', 'home.configuration.setting'),
('home.configuration.Stores','home.configuration.Stores'),('home.configuration.Tech Component Tools','home.configuration.Tech Component Tools'),('home.configuration.Analytics','home.configuration.Analytics'),
('home.configuration.Store Contact','home.configuration.Store Contact'),('home.configuration.Store Configuration','home.configuration.Store Configuration'),('home.configuration.Stake Holders','home.configuration.Stake Holders'),
('home.configuration.Networking','home.configuration.Networking'),('home.configuration.POS','home.configuration.POS'),('home.configuration.Audio','home.configuration.Audio'),
('home.configuration.Exterior Menus','home.configuration.Exterior Menus'),('home.configuration.Payment System','home.configuration.Payment System'),('home.configuration.Interior Menus','home.configuration.Interior Menus'),
('home.configuration.Sonic Radio','home.configuration.Sonic Radio'),('home.configuration.Installation','home.configuration.Installation'),('home.configuration.Projects','home.configuration.Projects'),
('home.configuration.Historical Projects','home.configuration.Historical Projects'),('home.configuration.Notes','home.configuration.Notes'),
 ('home.dashboard', 'home.dashboard'), ('home.notification', 'home.notification'),
('home.mail', 'home.mail'), ('home.find', 'home.find'), ('home.list', 'home.list'), ('home.setting', 'home.setting'), ('home.dashboard.priority', 'home.dashboard.priority'),
('home.dashboard.goal', 'home.dashboard.goal'), ('home.dashboard.notification', 'home.dashboard.notification'), ('home.dashboard.request', 'home.dashboard.request'),
('home.sonic.notificationtemplate.render', 'home.sonic.notificationtemplate.render'), ('home.sonic.quoterequest.render', 'home.sonic.quoterequest.render'),('home.sonic.po.render', 'home.sonic.po.render'),
('home.sonic.viewstore', 'home.sonic.viewstore'), ('home.sonic.storehighlight', 'home.sonic.storehighlight'), ('home.sonic.newproject', 'home.sonic.newproject'),
('home.sonic.searchproject', 'home.sonic.searchproject'), ('home.sonic.importproject', 'home.sonic.importproject'), ('home.sonic.projectportfolio', 'home.sonic.projectportfolio'),
('home.sonic.reportgenerator', 'home.sonic.reportgenerator'), ('home.sonic.report', 'home.sonic.report'), ('home.sonic.project.golive', 'home.sonic.project.golive'),('home.sonic.project.workflows', 'home.sonic.project.workflows'),
('home.sonic.project.task', 'home.sonic.project.task'), ('home.sonic.project.notes', 'home.sonic.project.notes'), ('home.sonic.project.activeproject', 'home.sonic.project.activeproject'),
('home.sonic.project.historicproject', 'home.sonic.project.historicproject'), 
('home.sonic.project.Items','home.sonic.project.ItemsPart'),('home.sonic.project.Users','home.sonic.project.Users'),('home.sonic.project.Brands','home.sonic.project.Brands'),
('home.sonic.project.Vendors','home.sonic.project.Vendors'),('home.sonic.project.Tech Components','home.sonic.project.Tech Components'),('home.sonic.project.Franchises','home.sonic.project.Franchises'),
('home.sonic.project.Stores','home.sonic.project.Stores'),('home.sonic.project.Tech Component Tools','home.sonic.project.Tech Component Tools'),('home.sonic.project.Analytics','home.sonic.project.Analytics'),
('home.sonic.project.Store Contact','home.sonic.project.Store Contact'),('home.sonic.project.Store Configuration','home.sonic.project.Store Configuration'),('home.sonic.project.Stake Holders','home.sonic.project.Stake Holders'),
('home.sonic.project.Networking','home.sonic.project.Networking'),('home.sonic.project.POS','home.sonic.project.POS'),('home.sonic.project.Audio','home.sonic.project.Audio'),
('home.sonic.project.Exterior Menus','home.sonic.project.Exterior Menus'),('home.sonic.project.Payment System','home.sonic.project.Payment System'),('home.sonic.project.Interior Menus','home.sonic.project.Interior Menus'),
('home.sonic.project.Sonic Radio','home.sonic.project.Sonic Radio'),('home.sonic.project.Installation','home.sonic.project.Installation'), ('home.sonic.project.dailyupdate','home.sonic.project.dailyupdate'),
('home.sonic.project.Projects','home.sonic.project.Projects'),('home.sonic.project.Historical Projects','home.sonic.project.Historical Projects'), ('home.sonic.project.signoff','home.sonic.project.signoff')

Go

/****** Object:  Table [dbo].[tblUserPermissionRel]    Script Date: 8/6/2023 8:22:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblUserPermissionRel](
	[aUserPermissionRelID] [int] IDENTITY(1,1) NOT NULL,
	[nUserID] [int] NULL,
	[nPermissionID] [int] NULL,
	[nPermVal] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[aUserPermissionRelID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[tblRole]    Script Date: 8/6/2023 8:23:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblRole](
	[aRoleID] [int] IDENTITY(1,1) NOT NULL,
	[tRoleName] [varchar](500) NULL,
	[nCreatedBy] [int] NULL,
	[nUpdateBy] [int] NULL,
	[dtCreatedOn] [datetime] NULL,
	[dtUpdatedOn] [datetime] NULL,
	[bDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[aRoleID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Super Admin'))
	Insert into tblRole(tRoleName) values('Super Admin')
if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Admin'))
	Insert into tblRole(tRoleName) values('Admin')
if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Project Manager'))
	Insert into tblRole(tRoleName) values('Project Manager')
if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Read Only'))
	Insert into tblRole(tRoleName) values('Read Only')
if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Franchise'))
	Insert into tblRole(tRoleName) values('Franchise')
if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Installation Vendor'))
	Insert into tblRole(tRoleName) values('Installation Vendor')
if(not Exists(Select top 1 1 from tblRole where tRoleName = 'Equipment Vendor'))
	Insert into tblRole(tRoleName) values('Equipment Vendor')

GO

/****** Object:  Table [dbo].[tblRolePermissionRel]    Script Date: 8/6/2023 8:28:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblRolePermissionRel](
	[aRolePermissionRelID] [int] IDENTITY(1,1) NOT NULL,
	[nRoleID] [int] NULL,
	[nPermissionID] [int] NULL,
	[nPermVal] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[aRolePermissionRelID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblRolePermissionRel] ADD  DEFAULT ((1)) FOR [nPermVal]
GO


-- Insert Role Permissions
declare @nRoleID int
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Super Admin'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Admin'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Project Manager'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Franchise'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Installation Vendor'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Equipment Vendor'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Read Only'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 1 from tbPermission

update tblRolePermissionRel set nPermVal = 0 where nRoleID in (select aRoleID from tblRole where tRoleName in('Read Only')) and 
nPermissionID in(Select aPermissionlID from tbPermission where tPermissionName in ('home.configuration', 'home.sonic.project.workflows', 'home.sonic.project.dailyupdate', 'home.sonic.project.signoff'))

update tblRolePermissionRel set nPermVal = 0 where nRoleID in (select aRoleID from tblRole where tRoleName in('Admin', 'Project Manager')) and nPermissionID 
in(Select aPermissionlID from tbPermission where tPermissionName in ('home.configuration.Users', 'home.configuration.Brands', 'home.sonic.project.dailyupdate', 'home.sonic.project.signoff'))


update tblRolePermissionRel set nPermVal = 1 where nRoleID in (select aRoleID from tblRole where tRoleName in('Franchise', 'Installation Vendor', 'Equipment Vendor','Read Only')) and 
nPermissionID in(Select aPermissionlID from tbPermission where tPermissionName in ('home.sonic.project.golive', 'home.sonic.project.task', 'home.sonic.project.notes', 'home.sonic.project.Store Contact',
'home.sonic.project.Store Configuration', 'home.sonic.project.Stake Holders', 'home.sonic.project.Networking', 'home.sonic.project.POS', 'home.sonic.project.Audio','home.sonic.project.Exterior Menus','home.sonic.project.Payment System',
'home.sonic.project.Interior Menus','home.sonic.project.Sonic Radio', 'home.sonic.project.Installation'))

update tblRolePermissionRel set nPermVal = 0 where nRoleID in (select aRoleID from tblRole where tRoleName in('Franchise', 'Installation Vendor', 'Equipment Vendor')) and 
nPermissionID in(Select aPermissionlID from tbPermission where tPermissionName in ('home.configuration', 'home.dashboard.report', 'home.sonic.reportgenerator', 'home.sonic.importproject',
'home.sonic.projectportfolio', 'home.sonic.reportgenerator', 'home.sonic.project.workflows', 'home.sonic.project.dailyupdate', 'home.sonic.project.signoff','home.sonic.newproject'))

update tblRolePermissionRel set nPermVal = 2 where nRoleID in (select aRoleID from tblRole where tRoleName in('Installation Vendor')) and 
nPermissionID in(Select aPermissionlID from tbPermission where tPermissionName in ('home.sonic.project.dailyupdate', 'home.sonic.project.signoff'))

update tblRolePermissionRel set nPermVal = 2 where nRoleID in (select aRoleID from tblRole where tRoleName in('Installation Vendor','Equipment Vendor')) and 
nPermissionID in(Select aPermissionlID from tbPermission where tPermissionName in ('home.sonic.project.deliverystatus'))

--select * from tblUser where tUserName like '%fr%'
--select * from tblUserBrandRel where nUserId = 45
--select * from tblBrand
--update tblUserBrandRel set nBrandID = 6 where nUserId = 45

--update tblUser set tpassword = 'YWRtaW4=' where aUserId = 58

--select * from tblUserPermissionRel where nUserId = 59 and nPermissionID in(3,4)

GO
CREATE TABLE [dbo].[tblUserFranchiseRel](
	[aUserFranchiseRelID] [int] IDENTITY(1,1) NOT NULL,
	[nUserID] [int] NULL,
	[nFranchiseID] [int] NULL
	
PRIMARY KEY CLUSTERED 
(
	[aUserFranchiseRelID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
Create  PROC sproc_GetUsertypeByVendorID               
(              
@nVendorID int ,
@nUserID int
)              
AS              
BEGIN              
--6	Installation Vendor
--7	Equipment Vendor
--declare @nVendorID int ,@nUserID int    
--set @nVendorID=1012 set @nUserID=36
   Select nUserID,6 as aUserTypeID from  tblVendor A with (nolock) inner join tblUserVendorRel B with (nolock) on A.aVendorID=B.NvendorID  where aVendorID=@nVendorID and nInstallation=1 and nUserID=@nUserID
   union all
    Select nUserID,7 as aUserTypeID from  tblVendor A with (nolock) inner join tblUserVendorRel B with (nolock) on A.aVendorID=B.NvendorID where aVendorID=@nVendorID and nEquipment=1 and nUserID=@nUserID


END 
GO
create procedure sproc_ChangeUserPermissionFromRole  
@nUserId int,  
@nRoleId int,
@nVendorId int,
@nFranchiseId int
as  
BEGIN  
	if(@nRoleId = -1) -- get Role for Vendor or Franchise
	BEGIN	
		Declare @tRole int
		delete from tblUserPermissionRel where nUserID = @nUserId  
		if(@nVendorId <> 0)
		BEGIN
			Declare @equp int, @inst int
			Select @equp = nEquipment, @inst = nInstallation from tblVendor with(nolock) where aVendorId = @nVendorId
			if(@equp is not null)
			BEGIN
				select @tRole = aRoleId from tblrole with(nolock) where tRoleName = 'Equipment Vendor'
				Insert into tblUserPermissionRel(nUserID, nPermissionID, nPermVal) Select @nUserId, nPermissionID, nPermVal from tblRolePermissionRel with(nolock) where nRoleID = @tRole  
			END
			if(@inst is not null)
			BEGIN
				select @tRole = aRoleId from tblrole with(nolock) where tRoleName = 'Installation Vendor'
				Insert into tblUserPermissionRel(nUserID, nPermissionID, nPermVal) Select @nUserId, nPermissionID, nPermVal from tblRolePermissionRel with(nolock) where nRoleID = @tRole  
			END
		END
		else if(@nFranchiseId <> 0)
		BEGIN
			select @tRole = aRoleId from tblrole with(nolock) where tRoleName = 'Franchise'
			Insert into tblUserPermissionRel(nUserID, nPermissionID, nPermVal) Select @nUserId, nPermissionID, nPermVal from tblRolePermissionRel with(nolock) where nRoleID = @tRole
		END
		
	END
	ELSE
	BEGIN
		 delete from tblUserPermissionRel where nUserID = @nUserId  
		 Insert into tblUserPermissionRel(nUserID, nPermissionID, nPermVal) Select @nUserId, nPermissionID, nPermVal from tblRolePermissionRel with(nolock) where nRoleID = @nRoleId  
	END
END  

Go

Create procedure sproc_getMyAccess
@nuserId int
as
BEGIN
	Select lower(tPermissionName) tPermissionName, nPermVal from tblUserPermissionRel with(nolock) join tbPermission with(nolock) on aPermissionlID = nPermissionId where nUserId = @nUserId
END

GO

Create procedure sproc_getAccesibleBrand
@nuserId int
as
BEGIN
	select * from tblBrand with(nolock) join tblUserBrandRel with(nolock) on aBrandId = nBrandID where nUserID = @nUserId
END

Go


Alter table tblUser add isFirstTime int default 0

GO

Alter table tblFranchise drop column tFranchiseDescription, tFranchiseLocation, dFranchiseEstablished, tFranchiseContact, tFranchiseOwner, nFranchiseEmployeeCount, nFranchiseRevenue
Alter table tblFranchise add tFranchiseAddress2 VARCHAR(5000)
Alter table tblFranchise add tFranchiseCity VARCHAR(100)
Alter table tblFranchise add nFranchiseState int
Alter table tblFranchise add tFranchiseZip VARCHAR(100)

Go

alter table tblVendor drop column nTechComponentID, tVendorDescription, tVendorContactPerson, tVendorCountry, tVendorEstablished

Go


create table tblDropdownModule(aModuleId int identity primary key, nBrandId int, tModuleName VARCHAR(500) unique, tModuleDisplayName VARCHAR(1000), tModuleGroup VARCHAR(500), editable bit default 1)
GO
INSERT into tblDropdownModule(nBrandId, tModuleName, tModuleDisplayName, tModuleGroup, editable) values(1, 'ConfigurationDriveThrou', 'DriveThrou', 'Configuration',1),(1, 'ConfigurationInsideDining', 'Inside Dining', 'Configuration',1),(1, 'StackHolderCD', 'CD', 'Stack Holder',1)
,(1, 'NetworkingStatus', 'Status', 'Networking',1),(1, 'NetworkingPrimaryType', 'Primary Type', 'Networking',1),(1, 'NetworkingBackupStatus', 'Backup Status', 'Networking',1),(1, 'NetworkingBackupType', 'Backup Type', 'Networking',1)
,(1, 'NetworkingTempStatus', 'TempStatus', 'Networking',1),(1, 'NetworkingTempType', 'TempType', 'Networking',1),(1, 'POSStatus', 'Status', 'POS',1),(1, 'AudioLoopType', 'LoopType', 'Audio',1),(1, 'POSPaperworkStatus', 'PaperworkStatus', 'POS',1)
,(1, 'AudioStatus', 'Status', 'Audio',1),(1, 'AudioConfiguration', 'Configuration', 'Audio',1),(1, 'AudioLoopStatus', 'LoopStatus', 'Audio',1),(1, 'PaymentSystemType', 'System Type', 'Payment',1),(1, 'ExteriorMenuStatus', 'Status', 'Exterior Menu',1)
,(1, 'PaymentSystemBuyPassID', 'BuyPass ID', 'Payment System',1),(1, 'PaymentSystemServerEPS', 'Server EPS', 'Payment System',1),(1, 'PaymentSystemStatus', 'Status', 'Payment System',1),(1, 'InteriorMenuStatus', 'Status', 'Interior Menu',1),(1, 'InstallationProjectStatus', 'Project Status', 'Installation',1)
,(1, 'SonicRaidoColors', 'Raido Colors', 'Sonic',1),(1, 'SonicRadioStatus', 'Radio Status', 'Sonic',1),(1, 'InstallationStatus', 'Status', 'Installation',1),(1, 'InstallationSignOffs', 'SignOffs', 'Installation',1),(1, 'InstallationTestTransactions', 'Test Transactions', 'Installation',1)
,(1, 'SonicNoteType', 'NoteType', 'Sonic',1),(1, 'ProjectStatus', 'Status', 'ProjectStatus',0),(1, 'VendorType', 'Type', 'Vendor',1),(1, 'VendorStatus', 'Status', 'Vendor',1)
,(1, 'UserDepartment', 'Department', 'User',1),(1, 'UserRole', 'Role', 'User',0),(1, 'State', 'State', 'All',1),(1, 'Country', 'Country', 'All',1)--(1, 'City', 'City', 'All',1)
GO
create procedure sproc_getDropdownModules
@nBrandId int
AS
BEGIN
	select * from tblDropdownModule with(nolock)
END

alter table tblDropDowns add nOrder int
alter table tblDropDowns add nFunction int

GO

alter table tblProjectNetworking add dDateFor_nPrimaryStatus date
alter table tblProjectNetworking add dDateFor_nBackupStatus date
alter table tblProjectNetworking add dDateFor_nTempStatus date

alter table tblProjectPOS add dDateFor_nStatus date
alter table tblProjectPOS add dDateFor_nPaperworkStatus date

alter table tblProjectAudio add dDateFor_nStatus date
alter table tblProjectAudio add dDateFor_nLoopStatus date

alter table tblProjectExteriorMenus add dDateFor_nStatus date

alter table tblProjectInteriorMenus add dDateFor_nStatus date

alter table tblProjectPaymentSystem add dDateFor_nBuyPassID date
alter table tblProjectPaymentSystem add dDateFor_nServerEPS date
alter table tblProjectPaymentSystem add dDateFor_nStatus date

alter table tblProjectSonicRadio add dDateFor_nStatus date

alter table tblProjectInstallation add dDateFor_nStatus date
alter table tblProjectInstallation add dDateFor_nProjectStatus date

update tblDropDowns set nFunction = 1 where tDropDownText like '%]%'

GO


Create FUNCTION dbo.geDropDownStatusTextByID (@aDropDownId int,@dDtate nvarchar(100))  
RETURNS nvarchar(max)  
AS  
Begin  
Declare @Temp nvarchar(max)
set @Temp=''
declare @day nvarchar(5)
declare @maonth nvarchar(5)
set @Temp=(Select  tDropdownText from tblDropdowns with (nolock) WHERE aDropDownId = @aDropDownId) 
if exists(Select top 1 1 from tblDropdowns with (nolock) WHERE aDropDownId = @aDropDownId and nFunction=1)
begin

	set @dDtate=isnull(@dDtate,'')
	if(@dDtate <>'')
	begin
		
		select @day =datename(day,@dDtate),@maonth=month(@dDtate)
		
		set @Temp=(replace(@Temp,'[Day/','['+@day+'/'))
		set @Temp=(replace(@Temp,'/Month]','/'+@maonth+']'))
		
	End	
end


return @Temp
END  

GO

Create procedure sproc_GetDeliveryStatus   
@nStoreID int=0
AS    
BEGIN    

DECLARE @ListOfDeliveryStatus TABLE(aID INT,tTechComponent VARCHAR(100) , dDeliveryDate VARCHAR(100), tStatus VARCHAR(500))
 
INSERT INTO @ListOfDeliveryStatus
VALUES 
(1,'Installation','','')  ,
(2,'Exterior Menus','','') ,
(3,'Interior Menus','','') ,
(4,'Payment Systems','',''),
(5,'Sonic Radio','',''),
(6,'Audio','',''),
(7,'POS','',''),	
(8,'Networking','','')	


Declare @tTechName nVARCHAR(100)
Declare @dDate VARCHAR(100)
Declare @tStatus VARCHAR(500)

Select top 1 @dDate=dInstallDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectInstallation  with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1 
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus where aID=1
Select top 1 @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectExteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus where aID=2
Select top 1 @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectInteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus where aID=3
Select top 1 @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectPaymentSystem with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1  
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus where aID=4
Select top 1 @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectSonicRadio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1  
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus where aID=5
Select top 1 @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectAudio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus where aID=6
Select top 1 @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectPOS with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus where aID=7
Select top 1 @dDate=dPrimaryDate, @tStatus=dbo.geDropDownStatusTextByID(nPrimaryStatus,dDateFor_nPrimaryStatus)  from tblProjectNetworking with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1   
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus where aID=8
select tTechComponent,isnull(dDeliveryDate,'') as dDeliveryDate,tStatus from @ListOfDeliveryStatus
END  

GO

alter table tblProjectNotes ADD  DEFAULT (getdate()) FOR [dtCreatedOn]
GO
alter procedure [dbo].[sproc_GetNotes]
@nPojectId as int=0
as          
BEGIN  
if(@nPojectId>0)
select aNoteID,nProjectID,nNoteType,nStoreID,tSource,tNoteDesc,dtCreatedOn,nCreatedBy,nUpdateBy,dtCreatedOn,dtUpdatedOn,bDeleted
from tblProjectNotes A with (nolock)
--inner join  tblNoteType B with (nolock) on A.nNoteType=B.aNoteTypeID
where  nProjectID=@nPojectId
else
select aNoteID,nProjectID,nNoteType,nStoreID,tSource,tNoteDesc,dtCreatedOn,nCreatedBy,nUpdateBy,dtCreatedOn,dtUpdatedOn,bDeleted
from tblProjectNotes A with (nolock)

END
GO
alter table tblUser add nUserTypeID int
alter table tblUser add nStatus int
GO
delete from tblUser where aUserId <>2
GO
sproc_ChangeUserPermissionFromRole 2,1,0,0

go
Select * from tblBrand

insert into tblUserBrandRel(nUserID, nBrandID) values(2,1),(2,2),(2,3),(2,4),(2,5),(2,6)

GO

/****** Object:  Table [dbo].[tblUserAndUserTypeRel]    Script Date: 8/6/2023 8:56:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblUserAndUserTypeRel](
	[aUserAndUserTypeRelID] [int] IDENTITY(1,1) NOT NULL,
	[nUserID] [int] NULL,
	[nUserTypeID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[aUserAndUserTypeRelID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



GO

  
Alter procedure sproc_getUserModel            
@nVendorId as int = 0,    
@nFranchiseId as int = 0     
as             
BEGIN            
           
if(@nVendorId=0 and @nFranchiseId = 0)          
Select *, 0 nVendorId from tblUser A with(nolock) where (bDeleted is null or bDeleted = 0) and aUserId not in(select nuserID from tblUserVendorRel with(nolock) union Select nUserID from tblUserFranchiseRel with(nolock)) order by tName   
 --inner join tblUserVendorRel B with(nolock) on  A.bDeleted=B.nPartID          
 --where nVendorId=@nVendorId          
 else if(@nVendorId <> 0)    
 Select *, nVendorId from tblUser A with(nolock)           
 inner join tblUserVendorRel B with(nolock) on  A.aUserID=B.nUserID          
 where nVendorId=@nVendorId and bDeleted is null or bDeleted = 0      
else if(@nFranchiseId <> 0)    
 Select *, nFranchiseID from tblUser A with(nolock)           
 inner join tblUserFranchiseRel B with(nolock) on  A.aUserID=B.nUserID          
 where nFranchiseID=@nFranchiseId and bDeleted is null or bDeleted = 0       
END 

GO
truncate table tblUserVendorRel
GO

CREATE procedure sproc_ChangePassword  
@nUserId int,  
@tCurPassword varchar(100),  
@tNewPassword varchar(100)  
as  
BEGIN  
 if(EXISTS(select top 1 1 from tblUser with(nolock) where aUserID = @nUserId and tPassword = @tCurPassword))  
 BEGIN  
  update tblUser set tPassword = @tNewPassword where aUserID = @nUserId  
  return 0  
 END  
 ELSE  
  return -1  
END

GO
--sproc_GetDropdown ''  

Alter Procedure [dbo].[sproc_GetDropdown]          
@tModuleName as VARCHAR(500),
@nUserId int      
as          
BEGIN          
 IF(@tModuleName is null OR @tModuleName = '')          
 BEGIN          
  Select nBrandId, tModuleName, aDropDownId, tDropdownText, tblDropdowns.bDeleted, nOrder, nFunction from tblDropdownMain  with(nolock) join tblDropdowns with(nolock) on          
  aDropdownId = nDropdownId  UNION          
 Select 1, 'Vendor', aVendorId, tVendorName, bDeleted, 1, 0 from tblVendor with(nolock)  UNION      
 Select 1, 'Franchise', aFranchiseId, tFranchiseName,bDeleted,1, 0 from tblFranchise  with(nolock) UNION  
 select 1, 'UserRole', aRoleID, tRoleName, bDeleted,1, 0 from tblRole with(nolock) UNION  
 select 1, 'Brand', aBrandId, tBrandName, bDeleted,1, 0 from tblBrand with(nolock) join tblUserBrandRel with(nolock) on nBrandID = aBrandID where nUserID = @nUserID        
 END          
 ELSE           
 BEGIN          
 IF(@tModuleName = 'Vendor')        
 BEGIN        
 Select nBrand, @tModuleName, aVendorId, tVendorName, bDeleted,1, 0 from tblVendor with(nolock) order by tVendorName   
 END        
 ELSE        
 BEGIN        
 Select 1, tModuleName, aDropDownId, tDropdownText, tblDropdowns.bDeleted, nOrder, nFunction from tblDropdownMain join tblDropdowns on          
  aDropdownId = nDropdownId where tModuleName = @tModuleName and (tblDropdowns.bDeleted is null or tblDropdowns.bDeleted = 0)  order by nOrder       
  END        
 END          
END 

GO
Alter table tblUser add isFirstTime int default 0

GO

Alter  PROC sproc_UserLogin               
(              
@tUserName NVARCHAR(255),              
@tPassword NVARCHAR(255)             
)              
AS              
BEGIN   
   select tName, tUserName, tEmail, case when(nRole is null) then 0 else nRole end, aUserID nUserID, isFirstTime from tblUser with(nolock) where UPPER(tUserName) = UPPER(@tUserName) and tPassword = @tPassword     
END 
