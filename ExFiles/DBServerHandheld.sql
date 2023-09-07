
create table tblProjectServerHandheld(aServerHandheldId int  NOT NULL    IDENTITY    PRIMARY KEY,nProjectID int, nVendor int, dDeliveryDate date, nStatus int, nNumberOfTabletsPerStore int, dRevisitDate date, cCost decimal,nMyActiveStatus int ,nStoreId int
,dDateFor_nStatus date)

GO
insert into tblProjectTypeConfig values(10, 'tblProjectServerHandheld', 1)
insert into tblProjectTypeConfig values(0, 'tblProjectServerHandheld', 0)
insert into tblProjectTypeConfig values(1, 'tblProjectServerHandheld', 0)
insert into tblProjectTypeConfig values(2, 'tblProjectServerHandheld', 0)
insert into tblProjectTypeConfig values(3, 'tblProjectServerHandheld', 0)
insert into tblProjectTypeConfig values(4, 'tblProjectServerHandheld', 0)
insert into tblProjectTypeConfig values(9, 'tblProjectServerHandheld', 0)

GO

insert into tblDropdownModule(tModuleName,tModuleDisplayName,tModuleGroup,editable) values('SeverHandheldStatus', 'Status', 'Sever Handheld', 1)
select * from tblDropdownModule order by 1 desc
select * from tblBrand
insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) values(2,38)

GO


  alter procedure sproc_GetPortfolioData    
@nPojectID as int=0,  
@nStoreID int=0  
AS      
BEGIN      
  
DECLARE @ListOfDeliveryStatus TABLE(aID INT,tTechComponent VARCHAR(100) ,tVendor VARCHAR(500), dDeliveryDate date  ,dInstallDate date ,dConfigDate date, tStatus VARCHAR(500),dSupportDate date, tLoopType VARCHAR(500), tLoopStatus VARCHAR(500), tBuyPassID VARCHAR(500), tServerEPS VARCHAR(500),dInstallEndDate date, tSignoffs VARCHAR(500), tTestTransactions VARCHAR(500))  
   
INSERT INTO @ListOfDeliveryStatus  
VALUES  
(1,'Installation','',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),  
(2,'Exterior Menus','',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),  
(3,'Interior Menus','',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),  
(4,'Payment Systems','',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),  
(5,'Sonic Radio','',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),  
(6,'Audio','',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),  
(7,'POS','',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),  
(8,'Networking','',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),  
(9,'Server Handheld','',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)  
  
Declare @nProjectType int
Declare @tTechName nVARCHAR(100)  
Declare @dDate VARCHAR(100)  
Declare @tStatus VARCHAR(500)  
Declare @tVendor VARCHAR(500)  
Declare @dInstallDate VARCHAR(100)  
Declare @dConfigDate VARCHAR(100)  
Declare @dSupportDate VARCHAR(100)  

Declare @tLoopType VARCHAR(500), @tLoopStatus VARCHAR(500), @tBuyPassID VARCHAR(500), @tServerEPS VARCHAR(500),@dInstallEndDate VARCHAR(500), @tSignoffs VARCHAR(500), @tTestTransactions VARCHAR(500)

select @nProjectType=nProjectType from tblProject with (nolock) where aProjectID=@nPojectID

Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor), @dDate=dInstallDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus), 
@dInstallEndDate=dInstallEnd,@tSignoffs=dbo.fn_getDropdownText(nSignoffs),@tTestTransactions=dbo.fn_getDropdownText(nTestTransactions)
from tblProjectInstallation  with (nolock) where nProjectID =@nPojectID  
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,dInstallEndDate=@dInstallEndDate,tSignoffs=@tSignoffs,tTestTransactions=@tTestTransactions where aID=1  
set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
set @dSupportDate=null

set @tLoopType=null
set @tLoopStatus=null
set @tBuyPassID=null
set @tServerEPS=null
set @dInstallEndDate=null
set @tSignoffs=null
set @tTestTransactions=null

--(0,'New'),(1,'Rebuild'),(2,'Remodel'),(3,'Relocation'),(4,'Acquisition'),
--(5,'POS Installation'),(6,'Audio Installation'),(7,'Menu Installation'),(8,'Payment Terminal Installation'),
--(9,'Parts Replacement')    

if(@nProjectType=0 OR @nProjectType=1 OR @nProjectType=2 OR @nProjectType=3 OR @nProjectType=4 OR @nProjectType=9 OR @nProjectType=7 )
Begin
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
from tblProjectExteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1   
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor where aID=2  
set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
set @dSupportDate=null

set @tLoopType=null
set @tLoopStatus=null
set @tBuyPassID=null
set @tServerEPS=null
set @dInstallEndDate=null
set @tSignoffs=null
set @tTestTransactions=null
End
if(@nProjectType=0 OR @nProjectType=1 OR @nProjectType=2 OR @nProjectType=3 OR @nProjectType=4 OR @nProjectType=9 OR @nProjectType=7 )
Begin
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor), @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
from tblProjectInteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1   
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor where aID=3  

set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
set @dSupportDate=null

set @tLoopType=null
set @tLoopStatus=null
set @tBuyPassID=null
set @tServerEPS=null
set @dInstallEndDate=null
set @tSignoffs=null
set @tTestTransactions=null
End
if(@nProjectType=0 OR @nProjectType=1 OR @nProjectType=2 OR @nProjectType=3 OR @nProjectType=4 OR @nProjectType=9 OR @nProjectType=8 )
Begin
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus),

@tBuyPassID=dbo.fn_getDropdownText(nBuyPassID),@tServerEPS=dbo.fn_getDropdownText(nServerEPS)
from tblProjectPaymentSystem with (nolock)  where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1  
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,tBuyPassID=@tBuyPassID,tServerEPS=@tServerEPS where aID=4  

set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
set @dSupportDate=null

set @tLoopType=null
set @tLoopStatus=null
set @tBuyPassID=null
set @tServerEPS=null
set @dInstallEndDate=null
set @tSignoffs=null
set @tTestTransactions=null
End
if(@nProjectType=0 OR @nProjectType=1 OR @nProjectType=2 OR @nProjectType=3 OR @nProjectType=4 OR @nProjectType=9  )
Begin
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
from tblProjectSonicRadio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor where aID=5  

set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
set @dSupportDate=null

set @tLoopType=null
set @tLoopStatus=null
set @tBuyPassID=null
set @tServerEPS=null
set @dInstallEndDate=null
set @tSignoffs=null
set @tTestTransactions=null
End
if(@nProjectType=0 OR @nProjectType=1 OR @nProjectType=2 OR @nProjectType=3 OR @nProjectType=4 OR @nProjectType=9 OR @nProjectType=6 )
Begin
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus),

@tLoopStatus=dbo.fn_getDropdownText(nLoopStatus),@tLoopType=dbo.fn_getDropdownText(nLoopType)
from tblProjectAudio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1   
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,tLoopStatus=@tLoopStatus,tLoopType=@tLoopType where aID=6  

set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
set @dSupportDate=null

set @tLoopType=null
set @tLoopStatus=null
set @tBuyPassID=null
set @tServerEPS=null
set @dInstallEndDate=null
set @tSignoffs=null
set @tTestTransactions=null
End
if(@nProjectType=0 OR @nProjectType=1 OR @nProjectType=2 OR @nProjectType=3 OR @nProjectType=4 OR @nProjectType=9 OR @nProjectType=5 )
Begin
	Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @dSupportDate=dSupportDate, @dInstallDate=dSupportDate, @dConfigDate=dConfigDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
	from tblProjectPOS with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1  
	update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,dConfigDate=@dConfigDate,tStatus=@tStatus,tVendor=@tVendor,dSupportDate=@dSupportDate where aID=7  

	set @tVendor=''
	set @dDate=null
	set  @tStatus=''
	set @dInstallDate=null
	set @dConfigDate=null
	set @dSupportDate=null

	set @tLoopType=null
	set @tLoopStatus=null
	set @tBuyPassID=null
	set @tServerEPS=null
	set @dInstallEndDate=null
	set @tSignoffs=null
	set @tTestTransactions=null
END
if(@nProjectType=0 OR @nProjectType=1 OR @nProjectType=2 OR @nProjectType=3 OR @nProjectType=4 OR @nProjectType=9 OR @nProjectType=10 )
Begin
	Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate,  @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
	from tblProjectServerHandheld with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1  
	update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,dConfigDate=@dConfigDate,tStatus=@tStatus,tVendor=@tVendor,dSupportDate=@dSupportDate where aID=9  

	set @tVendor=''
	set @dDate=null
	set  @tStatus=''
	set @dInstallDate=null
	set @dConfigDate=null
	set @dSupportDate=null

	set @tLoopType=null
	set @tLoopStatus=null
	set @tBuyPassID=null
	set @tServerEPS=null
	set @dInstallEndDate=null
	set @tSignoffs=null
	set @tTestTransactions=null
END
if(@nProjectType=0 OR @nProjectType=1 OR @nProjectType=2 OR @nProjectType=3 OR @nProjectType=4 OR @nProjectType=9  )
Begin
	Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dPrimaryDate,@dInstallDate=dBackupDate, @tStatus=dbo.geDropDownStatusTextByID(nPrimaryStatus,dDateFor_nPrimaryStatus)  

	from tblProjectNetworking with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1     
	update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,tStatus=@tStatus,tVendor=@tVendor where aID=8  
End
select aID, tTechComponent as tComponent,tVendor,dDeliveryDate as dDeliveryDate,dInstallDate,dConfigDate, tStatus,dSupportDate,tLoopType,tLoopStatus,tBuyPassID,tServerEPS,dInstallEndDate,tSignoffs,tTestTransactions
from @ListOfDeliveryStatus  
END    

go


alter procedure sproc_getActivePortFolioProjects      
@nBrandId int=0,  
@nStoreId int=0
as      
BEGIN     
  create table #tmpTable(nStoreId int,nProjectId int,nProjectType int,tProjectType varchar(100), tStoreNumber varchar(100), tStoreDetails VARCHAR(500),dProjectGoliveDate date,  dProjEndDate date, tProjManager varchar(500), tStatus varchar(100), tNewVendor
  
 varchar(500),tTableName VARCHAR(100),tFranchise VARCHAR(100),cCost decimal,dInstallDate date)      
    
      
  
 if(@nStoreId>0)
 begin
 Insert into #tmpTable(nStoreId,nProjectId,nProjectType,tProjectType,tStatus,dProjectGoliveDate,dProjEndDate,tTableName)-- , tStatus, tPrevProjManager, tProjManager, dProjectGoliveDate, dProjEndDate, tOldVendor, tNewVendor)      

 select nStoreId,aProjectId,nProjectType, dbo.fn_getProjectType(nProjectType) tProjectType, dbo.fn_getDropdownText(nProjectStatus) tStatus,dGoLiveDate as dProjectGoliveDate,dProjEndDate ,tTableName    
 from tblProject with(nolock)      
 left join tblProjectTypeConfig with(nolock) on tblProjectTypeConfig.aTypeId = case when (tblProject.nProjectType < 5 OR  tblProject.nProjectType = 9)    
 then -1 else tblProject.nProjectType end      
 where    nStoreId=@nStoreId and
 nProjectActiveStatus = 1  --and dGoLiveDate >=getdate()   
 end
 else
 begin
 Insert into #tmpTable(nStoreId,nProjectId,nProjectType,tProjectType,tStatus,dProjectGoliveDate,dProjEndDate,tTableName)-- , tStatus, tPrevProjManager, tProjManager, dProjectGoliveDate, dProjEndDate, tOldVendor, tNewVendor)      

  select nStoreId,aProjectId,nProjectType, dbo.fn_getProjectType(nProjectType) tProjectType, dbo.fn_getDropdownText(nProjectStatus) tStatus,dGoLiveDate as dProjectGoliveDate,dProjEndDate ,tTableName    
 from tblProject with(nolock)      
 left join tblProjectTypeConfig with(nolock) on tblProjectTypeConfig.aTypeId = case when (tblProject.nProjectType < 5 OR  tblProject.nProjectType = 9)    
 then -1 else tblProject.nProjectType end      
 where nBrandId = @nBrandId and    
 nProjectActiveStatus = 1  and dGoLiveDate >=getdate()  
 End
 declare @tQuery NVARCHAR(MAX), @tmpProjectId int, @tmpTableName VARCHAR(100),@TempnStoreId int    
      
 DECLARE db_cursor CURSOR FOR       
 SELECT nStoreId,nProjectId,tTableName from #tmpTable      
      
 OPEN db_cursor        
 FETCH NEXT FROM db_cursor INTO @TempnStoreId,@tmpProjectId, @tmpTableName      
      
 WHILE @@FETCH_STATUS = 0        
 BEGIN        
  Set @tQuery  = N'update #tmpTable set tNewVendor=dbo.fn_getVendorName(nVendor) from [dbo].['+ @tmpTableName +'] Where #tmpTable.nProjectId = [dbo].['+ @tmpTableName +'].nProjectId and nMyActiveStatus = 1 and #tmpTable.nProjectId = ' + CAST(@tmpProjectId as VARCHAR) -- update NewVendor      
 print @tQuery
 EXEC sp_executesql @tQuery     
 print @TempnStoreId    
  update #tmpTable set tStoreNumber = tblStore.tStoreNumber,tStoreDetails=tblStore.tCity +' '+dbo.fn_getDropdownText(tblStore.nStoreState) from tblStore with(nolock) where aStoreID = @TempnStoreId    and nStoreId=@TempnStoreId    
   
    update #tmpTable set tProjManager = tITPM, tFranchise=(select tFranchiseName from tblFranchise with (nolock) where aFranchiseId=nFranchisee) from tblProjectStakeHolders with(nolock) 
  where tblProjectStakeHolders.nStoreId = @TempnStoreId and  tblProjectStakeHolders.nProjectId =@tmpProjectId  and tITPM is not null    and #tmpTable.nProjectId=@tmpProjectId  
   update #tmpTable set cCost = tblProjectConfig.cProjectCost from tblProjectConfig with(nolock) where tblProjectConfig.nStoreId = @TempnStoreId  and  tblProjectConfig.nProjectId =@tmpProjectId and nMyActiveStatus = 1 and tblProjectConfig.cProjectCost is not null    and #tmpTable.nProjectId=@tmpProjectId 
  
     update #tmpTable set #tmpTable.dInstallDate =tblProjectInstallation.dInstallDate  from tblProjectInstallation with(nolock) where tblProjectInstallation.nStoreId = @TempnStoreId and  tblProjectInstallation.nProjectId =@tmpProjectId    and #tmpTable.nProjectId=@tmpProjectId       
	

     
  FETCH NEXT FROM db_cursor INTO @TempnStoreId,@tmpProjectId, @tmpTableName      
 END       
       
       
 CLOSE db_cursor        
 DEALLOCATE db_cursor      
      
 select * from #tmpTable   order by dProjectGoliveDate     
      
  drop table #tmpTable    
    
END    
  

  Go
  
  
alter procedure [dbo].[sproc_GetPurchaseOrderTemplate]           
@aPurchaseOrderTemplateID int ,        
@nStoreId int=0        
AS            
BEGIN            
 Select aPurchaseOrderTemplateID,tTemplateName,tTechnologyComponent,tTechnologyComponent as tCompName,nBrandId,nVendorID,isnull(nCreatedBy,0) as nCreatedBy,isnull(nUpdateBy,0) as nUpdateBy,dtCreatedOn,dtUpdatedOn,        
 isnull(bDeleted,0) as bDeleted,          
  isnull(CASE  WHEN tTechnologyComponent = 'Installation' THEN         
  (Select top 1  dInstallDate from tblProjectInstallation  with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1)        
        WHEN tTechnologyComponent = 'Exterior Menus' THEN        
        (select top 1 dDeliveryDate from tblProjectExteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1)        
  WHEN tTechnologyComponent = 'Interior Menus' THEN        
  (select  top 1 dDeliveryDate from tblProjectInteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1)        
  WHEN tTechnologyComponent = 'Payment Systems' THEN        
  (select top 1 dDeliveryDate from tblProjectPaymentSystem with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1)        
  WHEN tTechnologyComponent = 'Sonic Radio' THEN        
  (select top 1 dDeliveryDate from tblProjectSonicRadio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1)        
  WHEN tTechnologyComponent = 'Audio' THEN        
  (select top 1 dDeliveryDate from tblProjectAudio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1)        
  WHEN tTechnologyComponent = 'POS' THEN        
  (select top 1 dDeliveryDate from tblProjectPOS with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1)         
  WHEN tTechnologyComponent = 'Server Handheld' THEN        
  (select top 1 dDeliveryDate from tblProjectServerHandheld with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1)         
  WHEN tTechnologyComponent = 'Networking' THEN        
  (select top 1 dPrimaryDate from tblProjectNetworking with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1)        
  ELSE CAST(null as nvarchar(50))        
  END, CAST(null as nvarchar(50))) AS dDeliveryDate,      
  ( CASE  WHEN tTechnologyComponent = 'Exterior Menus' THEN        
        (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=7  order by 1 desc),'')='' THEN (select 
top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 and nProjectType<>10 order by 1 desc  )    
else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=7 order by 1 desc ) end)))        
  WHEN tTechnologyComponent = 'Interior Menus' THEN        
 (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=7  order by 1 desc),'')='' THEN
 (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 and nProjectType<>10 order by 1 desc  )    
else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=7 order by 1 desc ) end)))        
  WHEN tTechnologyComponent = 'Payment Systems' THEN        
 (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=8  order by 1 desc),'')='' THEN (select top 1 
aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 and nProjectType<>10 order by 1 desc  )    
else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=8 order by 1 desc ) end)))         
    WHEN tTechnologyComponent = 'Server Handheld' THEN        
 (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=10  order by 1 desc),'')='' THEN
 (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 and nProjectType<>10 order by 1 desc  )    
else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=10 order by 1 desc ) end)))        

   WHEN tTechnologyComponent = 'Audio' THEN        
  (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=6  order by 1 desc),'')='' THEN (select top 1 
aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 and nProjectType<>10 order by 1 desc  )    
else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=6 order by 1 desc ) end)))          
  WHEN tTechnologyComponent = 'POS' THEN        
  (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=5 order by 1 desc ),'')='' THEN (select top 1 
aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 and nProjectType<>10 order by 1 desc  )    
 else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=7 order by 1 desc ) end)))    
 else     
 (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 and nProjectType<>10 order by 1 
desc  ))    
    
 End) AS tProjectManager,      
  (select top 1 tVendorName  from tblvendor with (nolock) where aVendorId=nVendorID) as tVendorName,        
  dbo.gettToByVendor(nVendorID) as tTo,        
  --'' as tTo,        
  --getCCByVendor(nVendorID) as tCC        
  '' as tCC        
        
 from tblPurchaseOrderTemplate with (nolock) where aPurchaseOrderTemplateID = @aPurchaseOrderTemplateID              
END 
Go
alter function fn_getProjectType    
(    
 @nProjectType int    
)    
returns varchar(100)    
as     
BEGIN    
 DECLARE @pTypeTable TABLE (id INT, name varchar(100))    
 Declare @tProjectType varchar(100)    
 insert into @pTypeTable values(0,'New'),(1,'Rebuild'),(2,'Remodel'),(3,'Relocation'),(4,'Acquisition'),(5,'POS Installation'),(6,'Audio Installation'),(7,'Menu Installation'),(8,'Payment Terminal Installation'),(9,'Parts Replacement'),(10,'Server Handheld') 
 select @tProjectType = name from @pTypeTable where id = @nProjectType    
 return @tProjectType    
END

Go




alter procedure sproc_GetAllTechData      
@nStoreID int=0    
AS        
BEGIN        
    
DECLARE @ListOfDeliveryStatus TABLE(aID INT,tTechComponent VARCHAR(100) ,tVendor VARCHAR(500), dDeliveryDate date  ,dInstallDate date ,dConfigDate date, tStatus VARCHAR(500),nVendorID int)    
DECLARE @Brand nvarchar(max)
 select top 1 @Brand=tBrandName from tblBrand with (nolock) where aBrandId =(select top 1 nBrandID from tblstore with (nolock) where aStoreID=@nStoreID)

INSERT INTO @ListOfDeliveryStatus    
VALUES   

(8,'Networking','',NULL,NULL,NULL,'',null),    
(7,'POS','',NULL,NULL,NULL,'',null),    
(6,'Audio','',NULL,NULL,NULL,'',null),    
(2,'Exterior Menus','',NULL,NULL,NULL,'',null),    
(4,'Payment Systems','',NULL,NULL,NULL,'',null),    
(3,'Interior Menus','',NULL,NULL,NULL,'',null),    
(5,'Sonic Radio','',NULL,NULL,NULL,'',null),    
(1,'Installation','',NULL,NULL,NULL,'',null),    
(9,'Server Handheld','',NULL,NULL,NULL,'',null)     

if(@Brand='Buffalo Wild Wings')
 delete from @ListOfDeliveryStatus where aID=5
 else if(@Brand like 'Sonic%')
 delete from @ListOfDeliveryStatus where aID=9

    
    
    
Declare @tTechName nVARCHAR(100)    
Declare @dDate VARCHAR(100)    
Declare @tStatus VARCHAR(500)    
Declare @tVendor VARCHAR(500)    
Declare @dInstallDate VARCHAR(100)    
Declare @dConfigDate VARCHAR(100)    
Declare @nVendorID int   
  
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor, @dDate=dInstallDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus)
from tblProjectInstallation  with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1     
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=1    
set @nVendorID=null  
set @tVendor=''  
set @dDate=null  
set  @tStatus=''  
set @dInstallDate=null  
set @dConfigDate=null  
  
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
from tblProjectExteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1        
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=2    
set @nVendorID=null  
set @tVendor=''  
set @dDate=null  
set  @tStatus=''  
set @dInstallDate=null  
set @dConfigDate=null  
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor, @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
from tblProjectInteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1        
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=3    
set @nVendorID=null  
set @tVendor=''  
set @dDate=null  
set  @tStatus=''  
set @dInstallDate=null  
set @dConfigDate=null  
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
from tblProjectPaymentSystem with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1      
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=4   
if(@Brand like 'Sonic%')
Begin
set @nVendorID=null  
set @tVendor=''  
set @dDate=null  
set  @tStatus=''  
set @dInstallDate=null  
set @dConfigDate=null  
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
from tblProjectSonicRadio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1      
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=5    
End
set @nVendorID=null  
set @tVendor=''  
set @dDate=null  
set  @tStatus=''  
set @dInstallDate=null  
set @dConfigDate=null  
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
from tblProjectAudio with (nolock) where nProjectID in
(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=6    
set @nVendorID=null  
set @tVendor=''  
set @dDate=null  
set  @tStatus=''  
set @dInstallDate=null  
set @dConfigDate=null  
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @dInstallDate=dSupportDate, @dConfigDate=dConfigDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) 
from tblProjectPOS with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1        
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,dConfigDate=@dConfigDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=7    
set @nVendorID=null  
set @tVendor=''  
set @dDate=null  
set  @tStatus=''  
set @dInstallDate=null  
set @dConfigDate=null  
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dPrimaryDate,@dInstallDate=dBackupDate, @tStatus=dbo.geDropDownStatusTextByID(nPrimaryStatus,dDateFor_nPrimaryStatus) 
from tblProjectNetworking with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1     
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=8    
if(@Brand='Buffalo Wild Wings')
Begin
set @nVendorID=null  
set @tVendor=''  
set @dDate=null  
set  @tStatus=''  
set @dInstallDate=null  
set @dConfigDate=null  
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus)
from tblProjectServerHandheld with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1     
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=9    
End
select tTechComponent as tComponent,tVendor,dDeliveryDate as dDeliveryDate,dInstallDate,dConfigDate, tStatus,nVendorID from @ListOfDeliveryStatus    
END  

Go


alter Procedure sproc_CopyTechnologyToCurrentProject_new             
@nStoreId int,              
@nProjectType int,            
@nProjectID int,          
@nFromProjectId int
as                            
BEGIN 
	--(0,'New'),(1,'Rebuild'),(2,'Remodel'),(3,'Relocation'),(4,'Acquisition'),(5,'POSInstallation'),(6,'AudioInstallation'),(7,'MenuInstallation'),(8,'PaymentTerminalInstallation'),(9,'PartsReplacement')              
	if(Not Exists(select top 1 1 from tblProjectTypeConfig with(nolock) where isTechProjectType = 0 and aTypeId = @nProjectType group by aTypeId))-- If  new project is not of Tech project      
	BEGIN  
		 
		--
		update tblProject set nProjectActiveStatus = 0 where nStoreID = @nStoreId and nProjectActiveStatus = 1 and 
		nProjectType in (select aTypeId from tblProjectTypeConfig with(nolock) where isTechProjectType = 0 group by aTypeId)-- Move all to History since previous project was not specific project 
	
		update tblProject set nProjectActiveStatus = 0 where aProjectID = (select  top 1 aProjectID 
		from tblProject with(nolock)  where nStoreID = @nStoreId and nProjectType = @nProjectType and nProjectActiveStatus = 1 and  aProjectID<>@nProjectID ) --Move only that type of Project        

		--tblProjectNetworking 

		update tblProjectNetworking set nMyActiveStatus=0 where nStoreId =@nStoreId        
		insert into tblProjectNetworking (nProjectID,nVendor,nPrimaryStatus,dPrimaryDate,nPrimaryType,nBackupStatus,dBackupDate,nBackupType,nTempStatus,dTempDate,nTempType,nStoreId,nMyActiveStatus,dDateFor_nPrimaryStatus,dDateFor_nBackupStatus,dDateFor_nTempStatus)             
		(select top 1 @nProjectID,nVendor,nPrimaryStatus,dPrimaryDate,nPrimaryType,nBackupStatus,dBackupDate,nBackupType,nTempStatus,dTempDate,nTempType,@nStoreId,1,dDateFor_nPrimaryStatus,dDateFor_nBackupStatus,dDateFor_nTempStatus 
		from tblProjectNetworking with (nolock)  where nProjectID=@nFromProjectId)     
	
		--tblProjectSonicRadio

		update tblProjectSonicRadio set nMyActiveStatus=0 where nStoreId =@nStoreId            
		insert into tblProjectSonicRadio (nProjectID,nVendor,nOutdoorSpeakers,nColors,nIndoorSpeakers,nZones,nServerRacks,nStatus,dDeliveryDate,cCost,nStoreId,nMyActiveStatus,dDateFor_nStatus)            
		select top 1 @nProjectID,nVendor,nOutdoorSpeakers,nColors,nIndoorSpeakers,nZones,nServerRacks,nStatus,dDeliveryDate,cCost,@nStoreId,1 ,dDateFor_nStatus
		from tblProjectSonicRadio with (nolock) where nProjectID=@nFromProjectId            
		
		--tblProjectAudio

		update tblProjectAudio set nMyActiveStatus=0 where nStoreId =@nStoreId            
		insert into tblProjectAudio (nProjectID,nVendor,nStatus,nConfiguration,dDeliveryDate,nLoopStatus,nLoopType,dLoopDeliveryDate,cCost,nStoreId,nMyActiveStatus,dDateFor_nStatus,dDateFor_nLoopStatus)            
		select top 1 @nProjectID,nVendor,nStatus,nConfiguration,dDeliveryDate,nLoopStatus,nLoopType,dLoopDeliveryDate,cCost,@nStoreId,1,dDateFor_nStatus,dDateFor_nLoopStatus
		from tblProjectAudio with (nolock) where nProjectID=@nFromProjectId    
		--tblProjectExteriorMenus

		update tblProjectExteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId            
		insert into tblProjectExteriorMenus (nProjectID,nVendor,nStalls,nPatio,nFlat,nDTPops,nDTMenu,nStatus,dDeliveryDate,cFabConCost,cIDTechCost,cTotalCost,nStoreId,nMyActiveStatus,dDateFor_nStatus)            
		select top 1 @nProjectID,nVendor,nStalls,nPatio,nFlat,nDTPops,nDTMenu,nStatus,dDeliveryDate,cFabConCost,cIDTechCost,cTotalCost,@nStoreId,1,dDateFor_nStatus 
		from tblProjectExteriorMenus with (nolock) where nProjectID=@nFromProjectId            
		--tblProjectInteriorMenus

		update tblProjectInteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId            
		insert into tblProjectInteriorMenus (nProjectID,nVendor,nDMBQuantity,nStatus,dDeliveryDate,cCost,nStoreId,nMyActiveStatus,dDateFor_nStatus)            
		select top 1 @nProjectID,nVendor,nDMBQuantity,nStatus,dDeliveryDate,cCost,@nStoreId,1,dDateFor_nStatus
		from tblProjectInteriorMenus with (nolock) where nProjectID=@nFromProjectId     
		--tblProjectPaymentSystem
		update tblProjectPaymentSystem set nMyActiveStatus=0 where nStoreId =@nStoreId            
		insert into tblProjectPaymentSystem (nProjectID,nVendor,nBuyPassID,nServerEPS,nStatus,dDeliveryDate,nPAYSUnits,n45Enclosures,n90Enclosures,nDTEnclosures,n15SunShields,nUPS,nShelf,cCost,nType,nStoreId,nMyActiveStatus,dDateFor_nBuyPassID,dDateFor_nServerEPS,dDateFor_nStatus)            
		select top 1 @nProjectID,nVendor,nBuyPassID,nServerEPS,nStatus,dDeliveryDate,nPAYSUnits,n45Enclosures,n90Enclosures,nDTEnclosures,n15SunShields,nUPS,nShelf,cCost,nType,@nStoreId,1,dDateFor_nBuyPassID,dDateFor_nServerEPS,dDateFor_nStatus 
		from tblProjectPaymentSystem with (nolock) where nProjectID=@nFromProjectId            
		--tblProjectPOS
		update tblProjectPOS set nMyActiveStatus=0 where nStoreId =@nStoreId            
		insert into tblProjectPOS (nProjectID,nVendor,dDeliveryDate,dConfigDate,dSupportDate,nStatus,nPaperworkStatus,cCost,nStoreId,nMyActiveStatus,dDateFor_nStatus,dDateFor_nPaperworkStatus)            
		select top 1 @nProjectID,nVendor,dDeliveryDate,dConfigDate,dSupportDate,nStatus,nPaperworkStatus,cCost,@nStoreId,1 ,dDateFor_nStatus,dDateFor_nPaperworkStatus
		from tblProjectPOS with (nolock) where nProjectID=@nFromProjectId          
		
		--tblProjectServerHandheld
		update tblProjectServerHandheld set nMyActiveStatus=0 where nStoreId =@nStoreId            
		insert into tblProjectServerHandheld (nProjectID,nVendor,dDeliveryDate,nStatus,nNumberOfTabletsPerStore,dRevisitDate,cCost,nMyActiveStatus,nStoreId,dDateFor_nStatus)            
		select top 1 @nProjectID,nVendor,dDeliveryDate,nStatus,nNumberOfTabletsPerStore,dRevisitDate,cCost,1,@nStoreId,dDateFor_nStatus
		from tblProjectServerHandheld with (nolock) where nProjectID=@nFromProjectId          
		
		--tblProjectinstallation
		update tblProjectinstallation set nMyActiveStatus=0 where nStoreId =@nStoreId            
		insert into tblProjectinstallation (nProjectID,nVendor,tLeadTech,dInstallDate,dInstallEnd,nStatus,nSignoffs,nTestTransactions,nProjectStatus,cCost,nStoreId,nMyActiveStatus,dDateFor_nStatus,dDateFor_nProjectStatus)            
		select top 1 @nProjectID,nVendor,tLeadTech,dInstallDate,dInstallEnd,nStatus,nSignoffs,nTestTransactions,nProjectStatus,cCost,@nStoreId,1,dDateFor_nStatus,dDateFor_nProjectStatus
		from tblProjectinstallation with (nolock) where nProjectID=@nFromProjectId           
		--tblProjectConfig
		update tblProjectConfig set nMyActiveStatus=0 where nStoreId =@nStoreId  
		insert into tblProjectConfig(nStallCount,nDriveThru,nInsideDining,cProjectCost,dGroundBreak,dKitchenInstall,nStoreId,nMyActiveStatus,nProjectID)  
		select top 1 nStallCount,nDriveThru,nInsideDining,cProjectCost,dGroundBreak,dKitchenInstall,nStoreId,1,@nProjectId from tblProjectConfig with(nolock)  where nProjectID=@nFromProjectId--nStoreId = @nStoreId order by nProjectId desc  

		-- tblProjectStakeHolders
		update tblProjectStakeHolders set nMyActiveStatus=0 where nStoreId =@nStoreId  
		insert into tblProjectStakeHolders (nProjectID,nFranchisee,tRVP,tFBC,tRED,tCM,tAandE,tPrincipalPartner,tCD,tITPM,nMyActiveStatus,nStoreId)            
		select top 1 @nProjectID,nFranchisee,tRVP,tFBC,tRED,tCM,tAandE,tPrincipalPartner,tCD,tITPM,1,@nStoreId
		from tblProjectStakeHolders with (nolock) where nProjectID=@nFromProjectId      

	END
	Else
	Begin
		update tblProject set nProjectActiveStatus = 0 where nStoreID = @nStoreId   and  aProjectID<>@nProjectID  
		update tblProjectConfig set nMyActiveStatus=0 where nStoreId =@nStoreId	 
		update tblProjectStakeHolders set nMyActiveStatus=0 where nStoreId =@nStoreId  
		update tblProjectPaymentSystem set nMyActiveStatus=0 where nStoreId =@nStoreId   
		update tblProjectInteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId     
		update tblProjectExteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId  
		update tblProjectAudio set nMyActiveStatus=0 where nStoreId =@nStoreId 
		update tblProjectSonicRadio set nMyActiveStatus=0 where nStoreId =@nStoreId  
		update tblProjectNetworking set nMyActiveStatus=0 where nStoreId =@nStoreId   
		update tblProjectPOS set nMyActiveStatus=0 where nStoreId =@nStoreId    
		update tblProjectServerHandheld set nMyActiveStatus=0 where nStoreId =@nStoreId            
		update tblProjectinstallation set nMyActiveStatus=0 where nStoreId =@nStoreId
		if(@nProjectType<>0)
		Begin
			 
				--tblProjectConfig
		
			insert into tblProjectConfig(nStallCount,nDriveThru,nInsideDining,cProjectCost,dGroundBreak,dKitchenInstall,nStoreId,nMyActiveStatus,nProjectID)  
			select top 1 nStallCount,nDriveThru,nInsideDining,cProjectCost,dGroundBreak,dKitchenInstall,nStoreId,1,@nProjectId from tblProjectConfig with(nolock)  where nProjectID=@nFromProjectId--nStoreId = @nStoreId order by nProjectId desc  

			-- tblProjectStakeHolders
		
			insert into tblProjectStakeHolders (nProjectID,nFranchisee,tRVP,tFBC,tRED,tCM,tAandE,tPrincipalPartner,tCD,tITPM,nMyActiveStatus,nStoreId)            
			select top 1 @nProjectID,nFranchisee,tRVP,tFBC,tRED,tCM,tAandE,tPrincipalPartner,tCD,tITPM,1,@nStoreId
			from tblProjectStakeHolders with (nolock) where nProjectID=@nFromProjectId 
			

		End
	End
END
Go
