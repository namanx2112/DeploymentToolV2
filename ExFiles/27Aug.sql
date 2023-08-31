alter table tblBrand add nEnabled int

update tblBrand set nEnabled =0
update tblBrand set nEnabled =1 where tBrandName like '%Sonic%'
update tblBrand set nEnabled =1 where tBrandName like '%Buffalo%'
select * from tblBrand
GO
  
Alter procedure sproc_SearchStore              
@tText as VARCHAR(500),    
@nBrandID int=0    
AS              
BEGIN      
      
Select nStoreId, tStoreName,tStoreNumber,tProjectsInfo, @nBrandID nBrandId , case when(tAddress is null) then '' else tAddress end tAddress
from(      
 select aStoreId nStoreId, tStoreName,tStoreNumber, tProjectsInfo , (tStoreAddressLine1 + ',' + tCity + ' ' + tStoreZip) tAddress
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


--sproc_GetAllTechData 17
alter procedure sproc_GetAllTechData    
@nStoreID int=0  
AS      
BEGIN      
  
DECLARE @ListOfDeliveryStatus TABLE(aID INT,tTechComponent VARCHAR(100) ,tVendor VARCHAR(500), dDeliveryDate date  ,dInstallDate date ,dConfigDate date, tStatus VARCHAR(500),nVendorID int)  
   
INSERT INTO @ListOfDeliveryStatus  
VALUES   
(8,'Networking','',NULL,NULL,NULL,'',null),  
(7,'POS','',NULL,NULL,NULL,'',null),  
(6,'Audio','',NULL,NULL,NULL,'',null),  
(2,'Exterior Menus','',NULL,NULL,NULL,'',null),  
(4,'Payment Systems','',NULL,NULL,NULL,'',null),  
(3,'Interior Menus','',NULL,NULL,NULL,'',null),  
(5,'Sonic Radio','',NULL,NULL,NULL,'',null),  
(1,'Installation','',NULL,NULL,NULL,'',null)  
  
  
  
  
  
Declare @tTechName nVARCHAR(100)  
Declare @dDate VARCHAR(100)  
Declare @tStatus VARCHAR(500)  
Declare @tVendor VARCHAR(500)  
Declare @dInstallDate VARCHAR(100)  
Declare @dConfigDate VARCHAR(100)  
Declare @nVendorID int 

Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor, @dDate=dInstallDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectInstallation  with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1   
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=1  
set @nVendorID=null
set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null

Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectExteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1      
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=2  
set @nVendorID=null
set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor, @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectInteriorMenus with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1      
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=3  
set @nVendorID=null
set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectPaymentSystem with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=4  
set @nVendorID=null
set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectSonicRadio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=5  
set @nVendorID=null
set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectAudio with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1  
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=6  
set @nVendorID=null
set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dDeliveryDate, @dInstallDate=dSupportDate, @dConfigDate=dConfigDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectPOS with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1      
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,dConfigDate=@dConfigDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=7  
set @nVendorID=null
set @tVendor=''
set @dDate=null
set  @tStatus=''
set @dInstallDate=null
set @dConfigDate=null
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@nVendorID=nVendor,@dDate=dPrimaryDate,@dInstallDate=dBackupDate, @tStatus=dbo.geDropDownStatusTextByID(nPrimaryStatus,dDateFor_nPrimaryStatus)  from tblProjectNetworking with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1     
  
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,tStatus=@tStatus,tVendor=@tVendor,nVendorID=@nVendorID where aID=8  
select tTechComponent as tComponent,tVendor,dDeliveryDate as dDeliveryDate,dInstallDate,dConfigDate, tStatus,nVendorID from @ListOfDeliveryStatus  
END    


Go



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
(8,'Networking','',NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)  
  
  
Declare @tTechName nVARCHAR(100)  
Declare @dDate VARCHAR(100)  
Declare @tStatus VARCHAR(500)  
Declare @tVendor VARCHAR(500)  
Declare @dInstallDate VARCHAR(100)  
Declare @dConfigDate VARCHAR(100)  
Declare @dSupportDate VARCHAR(100)  

Declare @tLoopType VARCHAR(500), @tLoopStatus VARCHAR(500), @tBuyPassID VARCHAR(500), @tServerEPS VARCHAR(500),@dInstallEndDate VARCHAR(500), @tSignoffs VARCHAR(500), @tTestTransactions VARCHAR(500)

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

Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectExteriorMenus with (nolock) where nProjectID =@nPojectID 
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
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor), @dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectInteriorMenus with (nolock) where nProjectID =@nPojectID 
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

Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus),

@tBuyPassID=dbo.fn_getDropdownText(nBuyPassID),@tServerEPS=dbo.fn_getDropdownText(nServerEPS)
from tblProjectPaymentSystem with (nolock) where nProjectID =@nPojectID  
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
Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectSonicRadio with (nolock) where nProjectID =@nPojectID   
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

Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus),

@tLoopStatus=dbo.fn_getDropdownText(nLoopStatus),@tLoopType=dbo.fn_getDropdownText(nLoopType)
from tblProjectAudio with (nolock) where nProjectID =@nPojectID  
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

Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dDeliveryDate, @dSupportDate=dSupportDate, @dInstallDate=dSupportDate, @dConfigDate=dConfigDate, @tStatus=dbo.geDropDownStatusTextByID(nStatus,dDateFor_nStatus) from tblProjectPOS 
with (nolock) where nProjectID =@nPojectID   
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

Select top 1 @tVendor=(select tVendorName from tblVendor with (nolock) where aVendorId=nVendor),@dDate=dPrimaryDate,@dInstallDate=dBackupDate, @tStatus=dbo.geDropDownStatusTextByID(nPrimaryStatus,dDateFor_nPrimaryStatus)  from tblProjectNetworking with (nolock) where nProjectID =@nPojectID    
update @ListOfDeliveryStatus set dDeliveryDate= @dDate,dInstallDate=@dInstallDate,tStatus=@tStatus,tVendor=@tVendor where aID=8  
  
select aID, tTechComponent as tComponent,tVendor,dDeliveryDate as dDeliveryDate,dInstallDate,dConfigDate, tStatus,dSupportDate,tLoopType,tLoopStatus,tBuyPassID,tServerEPS,dInstallEndDate,tSignoffs,tTestTransactions
from @ListOfDeliveryStatus  
END    

go
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
 --print @tQuery
 EXEC sp_executesql @tQuery      
     
 print @TempnStoreId    
  update #tmpTable set tStoreNumber = tblStore.tStoreNumber,tStoreDetails=tblStore.tStoreAddressLine1 from tblStore with(nolock) where aStoreID = @TempnStoreId    and nStoreId=@TempnStoreId    
      
  update #tmpTable set tProjManager = tITPM, tFranchise=(select tFranchiseName from tblFranchise with (nolock) where aFranchiseId=nFranchisee) from tblProjectStakeHolders with(nolock) where tblProjectStakeHolders.nStoreId = @TempnStoreId and nMyActiveStatus = 1 and tITPM is not null    and #tmpTable.nStoreId=@TempnStoreId    
   update #tmpTable set cCost = tblProjectConfig.cProjectCost from tblProjectConfig with(nolock) where tblProjectConfig.nStoreId = @TempnStoreId and nMyActiveStatus = 1 and tblProjectConfig.cProjectCost is not null    and #tmpTable.nStoreId=@TempnStoreId 
  
     update #tmpTable set #tmpTable.dInstallDate =tblProjectInstallation.dInstallDate  from tblProjectInstallation with(nolock) where tblProjectInstallation.nStoreId = @TempnStoreId and nMyActiveStatus = 1    and #tmpTable.nStoreId=@TempnStoreId    
	

     
  FETCH NEXT FROM db_cursor INTO @TempnStoreId,@tmpProjectId, @tmpTableName      
 END       
       
       
 CLOSE db_cursor        
 DEALLOCATE db_cursor      
      
 select * from #tmpTable   order by dProjectGoliveDate     
      
  drop table #tmpTable    
    
END    
  

  Go


    
Alter Procedure sproc_CreateStoreFromExcel                    
@tStoreName as VARCHAR(500),                    
@nProjectType as int,                    
@tStoreNumber as VARCHAR(20),                    
@tAddress as VARCHAR(4000),                    
@tCity as VARCHAR(500),                    
@tState as VARCHAR(500),                    
@nDMAID as INT,                    
@tDMA as VARCHAR(500),                    
@tRED as VARCHAR(500),                    
@tCM as VARCHAR(500),                    
@tANE as VARCHAR(500),                    
@tRVP as VARCHAR(500),                    
@tPrincipalPartner  as VARCHAR(500),                    
@dStatus as datetime,                    
@dOpenStore as dateTIME,                    
@tProjectStatus as VARCHAR(100),                    
@nCreatedBy int=0,                    
@nBrandId int=0                    
as                    
BEGIN                    
                 
 --insert into tbltrace (StoreName,ProjectType,StoreNumber,[Address],City,[State],DMAID,DMA,RED,CM,ANE,RVP,PrincipalPartner,dtCreatedOn,dtUpdatedOn,ProjectStatus,CreatedBy,BrandId)                  
 --values (@tStoreName,@nProjectType,@tStoreNumber,@tAddress,@tCity,@tState,@nDMAID,@tDMA,@tRED,@tCM,@tANE,@tRVP,@tPrincipalPartner,@dStatus,@dOpenStore, @tProjectStatus, @nCreatedBy,@nBrandId )                  

 declare @nStoreId int, @nProjectStatus INT, @nProjectId INT, @nStoreState INT                          
 Select @nProjectStatus = aDropDownId from tblDropdowns WHERE tDropdownText = @tProjectStatus                    
 Select @nStoreState = aDropDownId from tblDropdowns WHERE tDropdownText = @tState               
 declare @aFranchiseId int
 select top 1 @aFranchiseId=aFranchiseId  from tblfranchise with (nolock)  where tFranchiseName=@tPrincipalPartner 
 
 select @nStoreId=aStoreID from tblStore with(nolock) where tStoreNumber=@tStoreNumber  and nBrandID=@nBrandId                      
 if (@nStoreId>0)                  
 begin                  
  update tblProject set tProjectName=@tStoreName,dGoLiveDate=@dOpenStore,nProjectType=@nProjectType,nProjectStatus=@nProjectStatus,        
  nDMAID=@nDMAID,tDMA=@tDMA,nBrandID=@nBrandId,dtUpdatedOn=GETDATE(),@nProjectId=aProjectID        
  where nStoreID=@nStoreId       
         
  update tblStore set tStoreName=@tStoreName,tStoreAddressLine1=@tAddress,tCity=@tCity,nStoreState=@nStoreState,dtUpdatedOn=GETDATE(),nBrandID=@nBrandId        
  where aStoreID=@nStoreId      
        
  update tblProjectStakeHolders set tRVP=@tRVP,tRED=@tRED,tCM=@tCM,tAandE=@tANE,tPrincipalPartner=@tPrincipalPartner,nFranchisee=@aFranchiseId       
  where nStoreId=@nStoreId      
 end                  
 else                   
 begin                  
  INSERT INTO tblStore(tStoreNumber, nCreatedBy, dtCreatedOn, tStoreName, tStoreAddressLine1, tCity, nStoreState,nBrandID) VALUES(@tStoreNumber, @nCreatedBy, GETDATE(), @tStoreName, @tAddress, @tCity,@nStoreState,@nBrandID)                    
  SET @nStoreId = @@IDENTITY        
                  
  INSERT INTO tblProject(tProjectName,nStoreID,dGoLiveDate,nProjectType,nProjectStatus,nDMAID,tDMA,nBrandID,nCreatedBy,dtCreatedOn, nProjectActiveStatus)                    
  VALUES(@tStoreName, @nStoreId, @dOpenStore,@nProjectType,  @nProjectStatus, @nDMAID, @tDMA, @nBrandId, @nCreatedBy,GETDATE(),1)                    
                    
  SET @nProjectId = @@IDENTITY
   
  insert into tblProjectStakeHolders(nProjectID,tRVP,tRED,tCM,tAandE,tPrincipalPartner, nStoreId, nFranchisee)          
  values(@nProjectId,@tRVP,@tRED,@tCM,@tANE,@tPrincipalPartner, @nStoreId,@aFranchiseId)            
    
	
	
 end                      
          
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
  WHEN tTechnologyComponent = 'Networking' THEN      
  (select top 1 dPrimaryDate from tblProjectNetworking with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectActiveStatus=1) and nMyActiveStatus=1)      
  ELSE CAST('' as nvarchar(50))      
  END, CAST('' as nvarchar(50))) AS dDeliveryDate,    
  ( CASE  WHEN tTechnologyComponent = 'Exterior Menus' THEN      
        (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=7  order by 1 desc),'')='' THEN (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 order by 1 desc  )  
else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=7 order by 1 desc ) end)))      
  WHEN tTechnologyComponent = 'Interior Menus' THEN      
 (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=7  order by 1 desc),'')='' THEN (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 order by 1 desc  )  
else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=7 order by 1 desc ) end)))        
  WHEN tTechnologyComponent = 'Payment Systems' THEN      
 (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=8  order by 1 desc),'')='' THEN (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 order by 1 desc  )  
else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=8 order by 1 desc ) end)))       
   WHEN tTechnologyComponent = 'Audio' THEN      
  (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=6  order by 1 desc),'')='' THEN (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 order by 1 desc  )  
else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=6 order by 1 desc ) end)))        
  WHEN tTechnologyComponent = 'POS' THEN      
  (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in(select (CASE  WHEN  isnull((select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=5 order by 1 desc ),'')='' THEN (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 order by 1 desc  )  
 else (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType=7 order by 1 desc ) end)))  
 else   
 (Select top 1 tITPM from tblProjectStakeHolders  with (nolock) where nProjectID in (select top 1 aProjectID from tblproject with (nolock) where nStoreID=@nStoreId and nProjectType<>5 and nProjectType<>6 and nProjectType<>7 and nProjectType<>8 order by 1 desc  ))  
  
 End) AS tProjectManager,    
  (select top 1 tVendorName  from tblvendor with (nolock) where aVendorId=nVendorID) as tVendorName,      
  dbo.gettToByVendor(nVendorID) as tTo,      
  --'' as tTo,      
  --dbo.getCCByVendor(nVendorID) as tCC      
  '' as tCC      
      
 from tblPurchaseOrderTemplate with (nolock) where aPurchaseOrderTemplateID = @aPurchaseOrderTemplateID            
END    

GO

alter FUNCTION gettToByVendor (@VendorID int)  
RETURNS nvarchar(max)  
AS  
Begin 
declare @EmailId nvarchar(max)
set @EmailId=''
select @EmailId=isnull(tVendorEmail,'') from  tblvendor with (nolock) where aVendorId=@VendorID
if(@EmailId<>'')
set @EmailId=@EmailId+';'
select @EmailId=@EmailId+tEmail+';' from tbluser with (nolock) where aUserId in (select nUserID from tblUserVendorRel with (nolock) where nVendorId=@VendorID)
Return @EmailId 
END
Go



 