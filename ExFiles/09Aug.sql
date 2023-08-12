insert into tbPermission (tPermissionName,tPermissionDiplayName) values('home.sonic.project.documentstab', 'home.sonic.project.documentstab')
insert into tbPermission (tPermissionName,tPermissionDiplayName) values('home.sonic.project.deliverystatus', 'home.sonic.project.deliverystatus')
insert into tbPermission (tPermissionName,tPermissionDiplayName) values('home.sonic.projectportfolio', 'home.sonic.projectportfolio')

declare @nRoleID int
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Super Admin'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission where tPermissionName in ('home.sonic.projectportfolio','home.sonic.project.documentstab','home.sonic.project.deliverystatus')
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Admin'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission where tPermissionName in ('home.sonic.projectportfolio','home.sonic.project.documentstab','home.sonic.project.deliverystatus')
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Project Manage'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission where tPermissionName in ('home.sonic.projectportfolio','home.sonic.project.documentstab','home.sonic.project.deliverystatus')

select * from tbPermission order by 1 desc

declare @nRoleID int
Select @nRoleID = aRoleID from tblRole where tRoleName  = 'Project Manager'
insert into tblRolePermissionRel (nRoleID, nPermissionID, nPermVal) select @nRoleID, aPermissionlID, 2 from tbPermission


select * from tblRolePermissionRel order by 1 desc

select * from tblUser where nRole = 1
sproc_ChangeUserPermissionFromRole 2,1,0,0
select * from tblRole

---------------------------------------------Santosh
alter FUNCTION dbo.geDropDownStatusTextByID (@aDropDownId int,@dDtate nvarchar(100))  
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
Go
alter table tblProjectNotes ADD  DEFAULT (getdate()) FOR [dtCreatedOn]

go
alter procedure [dbo].[sproc_GetNotes]
@nStoreId int,
@nPojectID as int=0
as          
BEGIN

if(@nStoreId>0 )
	if(@nPojectID>0)
		select aNoteID,nProjectID,nNoteType,nStoreID,tSource,tNoteDesc,dtCreatedOn,nCreatedBy,nUpdateBy,dtCreatedOn,dtUpdatedOn,bDeleted
		from tblProjectNotes A with (nolock)
		where  nStoreID=@nStoreId And nProjectID=@nPojectID order by aNoteID desc
	else 
		select aNoteID,nProjectID,nNoteType,nStoreID,tSource,tNoteDesc,dtCreatedOn,nCreatedBy,nUpdateBy,dtCreatedOn,dtUpdatedOn,bDeleted
		from tblProjectNotes A with (nolock)
		where  nStoreId=@nStoreId order by aNoteID desc
else
select aNoteID,nProjectID,nNoteType,nStoreID,tSource,tNoteDesc,dtCreatedOn,nCreatedBy,nUpdateBy,dtCreatedOn,dtUpdatedOn,bDeleted
from tblProjectNotes A with (nolock) order by aNoteID desc

END
Go
--sproc_GetDocumentation 2
ALter procedure [dbo].[sproc_GetDocumentation]
@nStoreId int=0
as          
BEGIN

if(@nStoreId>0 )
	
	select aPurchaseOrderID as nPOId,tPurchaseOrderNumber,nStoreId,tTemplateName +' PO#'+cast(aPurchaseOrderID as nvarchar(20)) as tFileName,(select tName from tbluser with (nolock) where aUserID=A.nCreatedBy) as tSentBy,
	(select tStoreNumber from tblstore with (nolock) where aStoreID=nStoreId) as tStoreNumber,A.dtCreatedOn from tblpurchaseOrder A with (nolock)
	inner join tblOutgoingEmail B with (nolock) on A.nOutgoingEmailID=B.aOutgoingEmailID
	inner join tblOutgoingEmailAttachment C with (nolock)on  C.nOutgoingEmailID=B.aOutgoingEmailID
	
	inner join tblPurchaseOrderTemplate D with (nolock) on  D.aPurchaseOrderTemplateID=A.nTemplateID
	where nStoreId=@nStoreId order by A.dtCreatedOn desc
	
else

	select aPurchaseOrderID as nPOId,tPurchaseOrderNumber,nStoreId,tTemplateName +' PO#'+cast(aPurchaseOrderID as nvarchar(20)) as tFileName,(select tName from tbluser with (nolock) where aUserID=A.nCreatedBy) as tSentBy, 
	(select tStoreNumber from tblstore with (nolock) where aStoreID=nStoreId) as tStoreNumber,A.dtCreatedOn from tblpurchaseOrder A with (nolock)
	inner join tblOutgoingEmail B with (nolock) on A.nOutgoingEmailID=B.aOutgoingEmailID
	inner join tblOutgoingEmailAttachment C with (nolock)on  C.nOutgoingEmailID=B.aOutgoingEmailID
	
	inner join tblPurchaseOrderTemplate D with (nolock) on  D.aPurchaseOrderTemplateID=A.nTemplateID
	order by A.dtCreatedOn desc

END
Go
  alter procedure sproc_getAttachemntByPOID     
@aPurchaseOrderID as int
AS    
Begin
select  tFileName as [FileName],ifile as AttachmentBlob from tblpurchaseOrder A with (nolock)
inner join tblOutgoingEmail B with (nolock) on A.nOutgoingEmailID=B.aOutgoingEmailID 
inner join tblOutgoingEmailAttachment C with (nolock) on C.nOutgoingEmailID=B.aOutgoingEmailID 
where aPurchaseOrderID=@aPurchaseOrderID
ENd
--sproc_GetDateChangeNotification 2
--Create procedure sproc_GetDateChangeNotification   
--@nStoreID int=0
--AS    
--BEGIN    

--DECLARE @ListOfNotificationStatus TABLE(aID INT,isSelected bit, tComponent VARCHAR(100) , tVendor VARCHAR(500))
 
--INSERT INTO @ListOfNotificationStatus
--VALUES 
--(1,0,'Installation',''),
--(2,0,'Exterior Menus',''),
--(3,0,'Interior Menus',''),
--(4,0,'Payment Systems',''),
--(5,0,'Sonic Radio',''),
--(6,0,'Audio',''),
--(7,0,'POS',''),	
--(8,0,'Networking','')	


--Declare @tTechName nVARCHAR(100)
--Declare @dDate VARCHAR(100)
--Declare @tVendor VARCHAR(500)

--Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor) from tblProjectInstallation  with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1 
--update @ListOfNotificationStatus set tVendor=@tVendor where aID=1
--Select top 1  @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor) from tblProjectExteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
--update @ListOfNotificationStatus set tVendor=@tVendor where aID=2
--Select top 1  @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor) from tblProjectInteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
--update @ListOfNotificationStatus set tVendor=@tVendor where aID=3
--Select top 1  @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor) from tblProjectPaymentSystem with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1  
--update @ListOfNotificationStatus set tVendor=@tVendor where aID=4
--Select top 1  @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor) from tblProjectSonicRadio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1  
--update @ListOfNotificationStatus set tVendor=@tVendor where aID=5
--Select top 1  @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor) from tblProjectAudio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1
--update @ListOfNotificationStatus set tVendor=@tVendor where aID=6
--Select top 1  @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor) from tblProjectPOS with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
--update @ListOfNotificationStatus set tVendor=@tVendor where aID=7
--Select top 1  @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor)  from tblProjectNetworking with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1   
--update @ListOfNotificationStatus set tVendor=@tVendor where aID=8
--select tComponent,tVendor from @ListOfNotificationStatus
--END  
--GO

alter procedure sproc_GetAllTechData  
@nStoreID int=0
AS    
BEGIN    

DECLARE @ListOfDeliveryStatus TABLE(aID INT,tTechComponent VARCHAR(100) ,tVendor VARCHAR(500), dDeliveryDate date  ,dInstallDate date ,dConfigDate date, tStatus VARCHAR(500))
 
INSERT INTO @ListOfDeliveryStatus
VALUES 
(1,'Installation','',NULL,NULL,NULL,''),
(2,'Exterior Menus','',NULL,NULL,NULL,''),
(3,'Interior Menus','',NULL,NULL,NULL,''),
(4,'Payment Systems','',NULL,NULL,NULL,''),
(5,'Sonic Radio','',NULL,NULL,NULL,''),
(6,'Audio','',NULL,NULL,NULL,''),
(7,'POS','',NULL,NULL,NULL,''),
(8,'Networking','',NULL,NULL,NULL,'')


Declare @tTechName nVARCHAR(100)
Declare @dDate VARCHAR(100)
Declare @tStatus VARCHAR(500)
Declare @tVendor VARCHAR(500)
Declare @dInstallDate VARCHAR(100)
Declare @dConfigDate VARCHAR(100)

Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor), @dDate=dInstallDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectInstallation  with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1 
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor where aID=1
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectExteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor where aID=2
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor), @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectInteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor where aID=3
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectPaymentSystem with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1  
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor where aID=4
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectSonicRadio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1  
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor where aID=5
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectAudio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor where aID=6
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @dInstallDate=dSupportDate, @dConfigDate=dConfigDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectPOS with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,dConfigDate=@dConfigDate,tStatus=@tStatus,tVendor=@tVendor where aID=7
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dPrimaryDate,@dInstallDate=dBackupDate, @tStatus=dbo.geDropDownStatusTextByID(nPrimaryStatus,dDateFor_nPrimaryStatus)  from tblProjectNetworking with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1   

update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,tStatus=@tStatus,tVendor=@tVendor where aID=8
select tTechComponent as tComponent,tVendor,dDeliveryDate as dDeliveryDate,dInstallDate,dConfigDate, tStatus from @ListOfDeliveryStatus
END  
GO

Alter table tblpurchaseOrder add tPDFData VARCHAR(max)
Alter table tblpurchaseOrder add tSentHtml VARCHAR(max)

Go

alter procedure [dbo].[sproc_getPOID]
@nStoreId int=0,
@nTemplateId int=0,
@nUserID int=0,
@aPurchaseOrderID int=0 out
as          
BEGIN

declare @nOutgoingEmailID int
set @nOutgoingEmailID=0
if(@nTemplateId>0)
select top 1 @aPurchaseOrderID=aPurchaseOrderID from tblpurchaseOrder with (nolock) where nCreatedBy=@nUserID and nTemplateId=@nTemplateId   order by 1 desc 
else
select top 1 @aPurchaseOrderID=aPurchaseOrderID from tblpurchaseOrder with (nolock) where nCreatedBy=@nUserID  order by 1 desc 

print @aPurchaseOrderID
select top 1 @nOutgoingEmailID=nOutgoingEmailID from tblpurchaseOrder with (nolock) where aPurchaseOrderID=@aPurchaseOrderID 
print @nOutgoingEmailID
if(@nOutgoingEmailID>0)
begin
insert into tblpurchaseOrder(nStoreID,nTemplateID,nCreatedBy,dtCreatedOn)values(@nStoreId,@nTemplateId,@nUserID,getdate())
set @aPurchaseOrderID=@@identity
print @aPurchaseOrderID
return @aPurchaseOrderID
end

END
Go
ALter procedure [dbo].[sproc_GetPurchaseOrdeStorerDetails]       
@nStoreId int    
AS        
BEGIN        
 Select  top 1 tStoreName as tStore,tstoreNumber,    
 tPOC as tName,tPOCPhone as tPhone,tPOCEmail as tEmail,tStoreAddressLine1  as tAddress, tCity,    
  (Select top 1 tDropdownText from tblDropdowns  with (nolock) WHERE aDropdownId = nStoreState) As [tstoreState],    
  tStoreZip,tBillToEmail,tBillToAddress,tBillToCity,    
  (Select top 1 tDropdownText from tblDropdowns  with (nolock) WHERE aDropdownId = nBillToState) As [tBillToState],    
  tBillToZip,tITPM as tProjectManager    
 from tblStore A with (nolock)    
 --inner join   tblProject B  with (nolock) on B.aProjectID=A.nProjectID    
 --inner join tblstore C with (nolock) on C.aStoreID=B.nStoreID    
 inner join tblProjectStakeHolders D with (nolock) on A.aStoreId=D.nStoreId    
  where A.aStoreId = @nStoreId    and nMyActiveStatus=1
  END
  GO


 Alter procedure [dbo].[sproc_getPreviousPODetails]
@nStoreId int=0,
@nUserID int=0,
@nTemplateId int=0
as          
BEGIN
declare @aPurchaseOrderID int
select top 1 @aPurchaseOrderID=aPurchaseOrderID from tblPurchaseOrder where  nCreatedBy=@nUserID and ntemplateId=@nTemplateId  and nOutgoingEmailID>0 order by aPurchaseOrderID desc

select (select top 1 tstoreNumber from tblstore with (Nolock) where aStoreID=@nStoreId) as tStoreNumber, *-- aPurchaseOrderID,tPurchaseOrderNumber,nStoreID,tBillingName,tBillingPhone,tBillingEmail,tBillingAddress,tShippingName,tShippingPhone,tShippingEmail,tShippingAddress,tNotes,dDeliver,cTotal,nOutgoingEmailID,nCreatedBy,nUpdateBy,dtCreatedOn,dtUpdatedOn,bDeleted,nTemplateId,tPDFData,tSentHtml
from tblpurchaseOrder A with (nolock)
inner join tblOutgoingEmail B with (nolock) on A.nOutgoingEmailID=B.aOutgoingEmailID 
where aPurchaseOrderID=@aPurchaseOrderID

END
Go

--select * from tblstore

--sproc_getPreviousPODetails 2,2,1
--sproc_GetPurchaseOrderTemplate 2,1

--sproc_GetPurchaseOrdeStorerDetails 2
--select *-- aPurchaseOrderID,tPurchaseOrderNumber,nStoreID,tBillingName,tBillingPhone,tBillingEmail,tBillingAddress,tShippingName,tShippingPhone,tShippingEmail,tShippingAddress,tNotes,dDeliver,cTotal,nOutgoingEmailID,nCreatedBy,nUpdateBy,dtCreatedOn,dtUpdatedOn,bDeleted,nTemplateId,tPDFData,tSentHtml
--from tblpurchaseOrder A with (nolock)
--inner join tblOutgoingEmail B with (nolock) on A.nOutgoingEmailID=B.aOutgoingEmailID 
--where aPurchaseOrderID=110

--declare @aPurchaseOrderID int
--declare @nOutgoingEmailID int
--declare @nUserID int
--set @nUserID=3
--select @aPurchaseOrderID=aPurchaseOrderID from tblpurchaseOrder with (nolock) where aPurchaseOrderID=97-- nCreatedBy=@nUserID  order by 1 desc 
--print @aPurchaseOrderID
--select top 1 @nOutgoingEmailID=nOutgoingEmailID from tblpurchaseOrder with (nolock) where aPurchaseOrderID=@aPurchaseOrderID 
--print @nOutgoingEmailID
--if(@nOutgoingEmailID>0)
--print 'if'
--else
--print 'else'

--sproc_getPOID 2,7,2

--select top 1 tPDFData, * from tblPurchaseOrder where  nCreatedBy=2 and ntemplateId=1  and nOutgoingEmailID>0 order by aPurchaseOrderID desc
--sp_helptext sproc_GetPurchaseOrderTemplate
----update tblpurchaseOrder set tSentHtml=N'<style>td{ border: 0px none!important;} table{ width: 60%!important;border: 1px solid lightgray!important;border-radius: 5px!important;}</style><div>All,</br></br>See attached purchase order, Please verify that you can meet the delivery date listed.</div>Thanks!<table><tr><td>PO#:</td><td>@@InspirePOID@@</td></tr><tr><td>Revision/Filename:</td><td>PurchaseOrder.pdf</td></tr><tr><td>Type:</td><td>@@InspiretVendorName@@</td></tr><tr><td>Store:</td><td>@@InspiretStoreNumber@@</td></tr><tr><td>Delivery:</td><td>@@InspiredDeliver@@ </td></tr><tr><td>Project Manager:</td><td>@@InspiretProjectManager@@</td></tr><table>@@Splitter@@108@@Splitter@@Vendor New@@Splitter@@200336@@Splitter@@24-08-2023@@Splitter@@' where aPurchaseOrderID=108
--select * from tblpurchaseOrder with (nolock) where nCreatedBy=2  order by 1 desc 
--select nOutgoingEmailID from tblpurchaseOrder with (nolock) where aPurchaseOrderID=98 
--select top 1 aPurchaseOrderID from tblpurchaseOrder with (nolock) where nCreatedBy=2  order by 1 desc

--select * from tblpurchaseOrder with (nolock)  order by 1 desc --nTemplateId
--select * from tblOutgoingEmail

--select * from tblProjectInstallation
--select * from tblOutgoingEmailAttachment

--sp_tables '%outgoing%'
----update tblProjectNotes set dtCreatedOn=getdate()

--nInstallation
--nEquipment
--insert into tblUserVendorRels(nUserID,nVendorID)values(36,1012)

--Alter  PROC sproc_UpdateUserAndVendorRel               
--(              
--@nVendorID int,
--@nUserId int 
--)              
--AS              
--BEGIN              
----6	Installation Vendor
----7	Equipment Vendor
--   Select nUserID,6, * from tblVendor  with (nolock) where aVendorID=@nVendorID and 
--END 

--select * from  table tblUserVendorRels
--select * from tblUserPermissionRel with (nolock) where nuserid=36

--select * from tblUserBrandRel with (nolock) where nuserid=36

--select * from tblUserVendorRel with (nolock) where nuserid=36

--select *   from tblUserAndUserTypeRel where aUserAndUserTypeRelID=18
--select * from tblUserType
--public int nUserID { get; set; }
--        public int aUserTypeID { get; set; }
--        public string tUserTypeName { get; set; }
--        public string tVisibleFor { get; set; }

--		sproc_GetUsertypeByVendorID 1012,36 

GO
alter procedure sproc_copyProjectsConfig
@nStoreId int,
@nProjectId int
as
BEGIN
	insert into tblProjectConfig(nStallCount,nDriveThru,nInsideDining,cProjectCost,dGroundBreak,dKitchenInstall,nStoreId,nMyActiveStatus,nProjectID)
	select top 1 nStallCount,nDriveThru,nInsideDining,cProjectCost,dGroundBreak,dKitchenInstall,nStoreId,nMyActiveStatus,@nProjectId from tblProjectConfig with(nolock)  where nStoreId = @nStoreId order by nProjectId desc
END

GO

select * from tblProjectNotes
update tblProjectNotes set nNoteType = 108
select * from tblDropdownMain where tModuleName like '%note%'

select * from tblProjectConfig where nStoreID = 1
select * from tblProjectStakeHolders where nStoreID = 1

update tblProjectConfig set nMyActiveStatus =1 where aProjectConfigID = 11

select * from tblProjectStakeHolders
delete from tblProjectStakeHolders where aProjectStakeholderId = 10
update tblProjectStakeHolders set nProjectID = 8 where  aProjectStakeholderId = 9