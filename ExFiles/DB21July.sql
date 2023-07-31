alter table tblStore add tStoreName VARCHAR(1000)
go
alter table tblStore add tStoreAddressLine1 varchar(1000)
go
alter table tblStore add tStoreAddressLine2 varchar(1000)
go
alter table tblStore add nStoreState int
go
alter table tblStore add tCity varchar(500)
go
alter table tblStore add tStoreZip varchar(100)
go
alter table tblStore add tStoreManager varchar(500)
go
alter table tblStore add tPOC varchar(500)
go
alter table tblStore add tPOCPhone varchar(20)
go
alter table tblStore add tPOCEmail varchar(500)
go
alter table tblStore add tGC varchar(500)
go
alter table tblStore add tGCPhone varchar(20)
go
alter table tblStore add tGCEmail varchar(500)
go
alter table tblStore add tBillToCompany varchar(500)
go
alter table tblStore add tBillToAddress varchar(500)
go
alter table tblStore add tBillToCity varchar(500)
go
alter table tblStore add nBillToState int
go
alter table tblStore add tBillToZip varchar(100)
go
alter table tblStore add tBillToEmail varchar(500)

go
drop table tblProjectStore
drop table tblProjectComponent
drop table tblProjectBillTo
drop table tblProjectComponentUpload
alter table tblProjectAudio add nStoreId int
alter table tblProjectConfig add nStoreId int
alter table tblProjectExteriorMenus add nStoreId int
alter table tblProjectInstallation add nStoreId int
alter table tblProjectInteriorMenus add nStoreId int
alter table tblProjectNetworking add nStoreId int
alter table tblProjectNotes add nStoreId int
alter table tblProjectPaymentSystem add nStoreId int
alter table tblProjectPOS add nStoreId int
alter table tblProjectSonicRadio add nStoreId int
alter table tblProjectStakeHolders add nStoreId int

alter table tblProjectAudio drop column ProjectActiveStatus 
alter table tblProjectConfig drop column ProjectActiveStatus 
alter table tblProjectExteriorMenus drop column ProjectActiveStatus 
alter table tblProjectInstallation drop column ProjectActiveStatus 
alter table tblProjectInteriorMenus drop column ProjectActiveStatus 
alter table tblProjectNetworking drop column ProjectActiveStatus 
alter table tblProjectPaymentSystem drop column ProjectActiveStatus 
alter table tblProjectPOS drop column ProjectActiveStatus 
alter table tblProjectSonicRadio drop column ProjectActiveStatus 
alter table tblProjectStakeHolders drop column ProjectActiveStatus 
alter table tblProjectPaymentSystem drop column ProjectActiveStatus 

alter table tblProject drop column ProjectActiveStatus 
alter table tblProject add nProjectActiveStatus int
alter table tblProject add dProjEndDate date

go 
alter table tblProjectAudio add nMyActiveStatus int DEFAULT 1
alter table tblProjectConfig add nMyActiveStatus int DEFAULT 1
alter table tblProjectExteriorMenus add nMyActiveStatus int DEFAULT 1
alter table tblProjectInstallation add nMyActiveStatus int DEFAULT 1
alter table tblProjectInteriorMenus add nMyActiveStatus int DEFAULT 1
alter table tblProjectNetworking add nMyActiveStatus int DEFAULT 1
alter table tblProjectPaymentSystem add nMyActiveStatus int DEFAULT 1
alter table tblProjectPOS add nMyActiveStatus int DEFAULT 1
alter table tblProjectSonicRadio add nMyActiveStatus int DEFAULT 1
alter table tblProjectStakeHolders add nMyActiveStatus int DEFAULT 1



truncate table tblProjectAudio
truncate table tblProjectConfig
truncate table tblProjectExteriorMenus
truncate table tblProjectInstallation
truncate table tblProjectInteriorMenus
truncate table tblProjectNetworking
truncate table tblProjectNotes
truncate table tblProjectPaymentSystem
truncate table tblProjectPOS
truncate table tblProjectSonicRadio
truncate table tblProjectStakeHolders
truncate table tblProject
truncate table tblStore

go
--select * from tblProjectTypeConfig
create table tblProjectTypeConfig(aTypeId int, tTableName varchar(100),isTechProjectType int)
Insert into tblProjectTypeConfig values(0, 'tblProjectAudio',0),(0, 'tblProjectExteriorMenus',0),(0, 'tblProjectInstallation',0),(0, 'tblProjectInteriorMenus',0),(0, 'tblProjectNetworking',0),(0, 'tblProjectPaymentSystem',0),(0, 'tblProjectPOS',0),(0, 'tblProjectSonicRadio',0)
Insert into tblProjectTypeConfig values(1, 'tblProjectAudio',0),(1, 'tblProjectExteriorMenus',0),(1, 'tblProjectInstallation',0),(1, 'tblProjectInteriorMenus',0),(1, 'tblProjectNetworking',0),(1, 'tblProjectPaymentSystem',0),(1, 'tblProjectPOS',0),(1, 'tblProjectSonicRadio',0)
Insert into tblProjectTypeConfig values(2, 'tblProjectAudio',0),(2, 'tblProjectExteriorMenus',0),(2, 'tblProjectInstallation',0),(2, 'tblProjectInteriorMenus',0),(2, 'tblProjectNetworking',0),(2, 'tblProjectPaymentSystem',0),(2, 'tblProjectPOS',0),(2, 'tblProjectSonicRadio',0)
Insert into tblProjectTypeConfig values(3, 'tblProjectAudio',0),(3, 'tblProjectExteriorMenus',0),(3, 'tblProjectInstallation',0),(3, 'tblProjectInteriorMenus',0),(3, 'tblProjectNetworking',0),(3, 'tblProjectPaymentSystem',0),(3, 'tblProjectPOS',0),(3, 'tblProjectSonicRadio',0)
Insert into tblProjectTypeConfig values(4, 'tblProjectAudio',0),(4, 'tblProjectExteriorMenus',0),(4, 'tblProjectInstallation',0),(4, 'tblProjectInteriorMenus',0),(4, 'tblProjectNetworking',0),(4, 'tblProjectPaymentSystem',0),(4, 'tblProjectPOS',0),(4, 'tblProjectSonicRadio',0)
Insert into tblProjectTypeConfig values(5, 'tblProjectPOS',1)
Insert into tblProjectTypeConfig values(6, 'tblProjectAudio',1)
Insert into tblProjectTypeConfig values(7, 'tblProjectInteriorMenus',1),(7, 'tblProjectExteriorMenus',1)
Insert into tblProjectTypeConfig values(8, 'tblProjectPaymentSystem',1)
Insert into tblProjectTypeConfig values(9, 'tblProjectAudio',0),(9, 'tblProjectExteriorMenus',0),(9, 'tblProjectInstallation',0),(9, 'tblProjectInteriorMenus',0),(9, 'tblProjectNetworking',0),(9, 'tblProjectPaymentSystem',0),(9, 'tblProjectPOS',0),(9, 'tblProjectSonicRadio',0)

GO

create function fn_GetProjectIdForThisTechOrAnyProjectType
(@nStoreId int, @nProjectType int, @nActiveStatus int)
returns @retContactInformation  table(
nProjectId int,
nProjectType int)
as
BEGIN
	if(EXISTS( select top 1 1 from tblProject where nStoreID = @nStoreId and nProjectActiveStatus = @nActiveStatus and nProjectType = @nProjectType))
		insert @retContactInformation select aProjectID, nProjectType from tblProject where nStoreID = @nStoreId and nProjectActiveStatus = @nActiveStatus and nProjectType = @nProjectType
	else
		insert @retContactInformation select top 1 aProjectID, nProjectType from tblProject where nStoreID = @nStoreId and nProjectActiveStatus = @nActiveStatus order by aProjectID desc
	return
END
go
Alter procedure sproc_SearchStore        
@tText as VARCHAR(500)        
AS        
BEGIN

Select nStoreId, tStoreName,tStoreNumber,tProjectsInfo
from(
 select aStoreId nStoreId, tStoreName,tStoreNumber, tProjectsInfo  from tblStore with(nolock) Join
 (select nStoreId, STRING_AGG(CAST(aProjectId as varchar) + '_' + CAST(nProjectType as varchar) + '_' + CAST(dGoLiveDate as varchar) , ', ') tProjectsInfo from tblProject with(nolock) where nProjectActiveStatus = 1
 --where ProjectActiveStatus = 1 
 group by nStoreID) tblProj on aStoreID = nStoreID 
 where 
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

Alter Procedure sproc_CreateStoreFromExcel              
@tStoreName as VARCHAR(500),              
@tProjectType as VARCHAR(500),              
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
 --values (@tStoreName,@tProjectType,@tStoreNumber,@tAddress,@tCity,@tState,@nDMAID,@tDMA,@tRED,@tCM,@tANE,@tRVP,@tPrincipalPartner,@dStatus,@dOpenStore, @tProjectStatus, @nCreatedBy,@nBrandId )            
 declare @nStoreId int, @nProjectType INT,  @nProjectStatus INT, @nProjectId INT, @nStoreState INT           
 Select @nProjectType = aDropDownId from tblDropdowns WHERE tDropdownText = @tProjectType              
 Select @nProjectStatus = aDropDownId from tblDropdowns WHERE tDropdownText = @tProjectStatus              
 Select @nStoreState = aDropDownId from tblDropdowns WHERE tDropdownText = @tState         
            
 select @nStoreId=aStoreID from tblStore with(nolock) where tStoreNumber=@tStoreNumber             
 if (@nStoreId>0)            
 begin            
  update tblProject set tProjectName=@tStoreName,dGoLiveDate=@dOpenStore,nProjectType=@nProjectType,nProjectStatus=@nProjectStatus,  
  nDMAID=@nDMAID,tDMA=@tDMA,nBrandID=@nBrandId,dtUpdatedOn=GETDATE(),@nProjectId=aProjectID  
  where nStoreID=@nStoreId 
   
  update tblStore set tStoreName=@tStoreName,tStoreAddressLine1=@tAddress,tCity=@tCity,nStoreState=@nStoreState,dtUpdatedOn=GETDATE()   
  where aStoreID=@nStoreId
  
  update tblProjectStakeHolders set tRVP=@tRVP,tRED=@tRED,tCM=@tCM,tAandE=@tANE,tPrincipalPartner=@tPrincipalPartner  
  where nStoreId=@nStoreId
 end            
 else             
 begin            
  INSERT INTO tblStore(tStoreNumber, nCreatedBy, dtCreatedOn, tStoreName, tStoreAddressLine1, tCity, nStoreState) VALUES(@tStoreNumber, @nCreatedBy, GETDATE(), @tStoreName, @tAddress, @tCity,@nStoreState)              
  SET @nStoreId = @@IDENTITY  
            
  INSERT INTO tblProject(tProjectName,nStoreID,dGoLiveDate,nProjectType,nProjectStatus,nDMAID,tDMA,nBrandID,nCreatedBy,dtCreatedOn, nProjectActiveStatus)              
  VALUES(@tStoreName, @nStoreId, @dOpenStore,@nProjectType,  @nProjectStatus, @nDMAID, @tDMA, @nBrandId, @nCreatedBy,GETDATE(),1)              
              
  SET @nProjectId = @@IDENTITY                 
     
  insert into tblProjectStakeHolders(nProjectID,tRVP,tRED,tCM,tAandE,tPrincipalPartner, nStoreId)    
  values(@nProjectId,@tRVP,@tRED,@tCM,@tANE,@tPrincipalPartner, @nStoreId)      
            
 end                
    
END
GO
Create Procedure sproc_MoveProjectToHistory    
@nStoreId int,    
@nProjectType int,
@movedProjectId int output
as                  
BEGIN     
 if(Exists(select top 1 1 from tblProjectTypeConfig with(nolock) where isTechProjectType = 0 and aTypeId = @nProjectType group by aTypeId))-- If new project is not of Tech project
 BEGIN
	SET @movedProjectId = 0
	 update tblProject set nProjectActiveStatus = 0 where nStoreID = @nStoreId
 END
 ELSE
 BEGIN
	 select @movedProjectId = aProjectID  from tblProject with(nolock) where nStoreID = @nStoreId and nProjectType = @nProjectType and nProjectActiveStatus = 1
	 if(@movedProjectId is not null)    
	 BEGIN    
	  update tblProject set nProjectActiveStatus = 0 where aProjectID = @movedProjectId --Move only that type of Project    
	 END    
	 ELSE    
	 BEGIN   
	  select top 1 @movedProjectId = aProjectID  from tblProject with(nolock) where nStoreID = @nStoreId and nProjectActiveStatus = 1 and nProjectType in (select aTypeId from tblProjectTypeConfig with(nolock) where isTechProjectType = 0 group by aTypeId) order by aProjectID desc
	  update tblProject set nProjectActiveStatus = 0 where nStoreID = @nStoreId and nProjectActiveStatus = 1 and nProjectType in (select aTypeId from tblProjectTypeConfig with(nolock) where isTechProjectType = 0 group by aTypeId)-- Move all to History since previous project was not specific project   
	 END  
 END
end 

GO

create function fn_getProjectType
(
	@nProjectType int
)
returns varchar(100)
as 
BEGIN
	DECLARE @pTypeTable TABLE (id INT, name varchar(100))
	Declare @tProjectType varchar(100)
	insert into @pTypeTable values(0,'New'),(1,'Rebuild'),(2,'Remodel'),(3,'Relocation'),(4,'Acquisition'),(5,'POSInstallation'),(6,'AudioInstallation'),(7,'MenuInstallation'),(8,'PaymentTerminalInstallation'),(9,'PartsReplacement')
	select @tProjectType = name from @pTypeTable where id = @nProjectType
	return @tProjectType
END

GO
Create  procedure sproc_getActiveProjects
@nStoreId int
as
BEGIN	
	create table #pTypeTable(tProjectId int, nPType int, nVendorId INT, tVendorName varchar(500), tTableName varchar(100), nActiveStatus int, tProjManager Varchar(500))

	insert into #pTypeTable (tProjectId, tTableName,nActiveStatus,nPType) select aProjectId, tTableName,nActiveStatus, nPType from(
	select aProjectId, tTableName, nProjectActiveStatus nActiveStatus, row_number() OVER (PARTITION BY nProjectType ORDER BY aProjectID desc) AS rNumber, nProjectType nPType from tblProject
	left join tblProjectTypeConfig on tblProjectTypeConfig.aTypeId = case when (tblProject.nProjectType < 5 OR  tblProject.nProjectType = 9) then -1 else tblProject.nProjectType end
	where nStoreId = @nStoreId
	) tTable where rNumber <= 2

	

	declare @tQuery NVARCHAR(MAX), @tmpProjectId int, @tmpTableName VARCHAR(100)
	DECLARE db_cursor CURSOR FOR 
	SELECT tProjectId,tTableName from #pTypeTable where nActiveStatus = 1

	OPEN db_cursor  
	FETCH NEXT FROM db_cursor INTO @tmpProjectId, @tmpTableName

	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		Set @tQuery  = N'update #pTypeTable set nVendorId = nVendor from [dbo].['+ @tmpTableName +'] Where nProjectId = tProjectId and tProjectId = ' + CAST(@tmpProjectId as VARCHAR) + ' and nMyActiveStatus = 1'		
		EXEC sp_executesql @tQuery
		FETCH NEXT FROM db_cursor INTO @tmpProjectId, @tmpTableName
	END 
	--Update Previous Vendor Name
	--update #pTypeTable set tOldVendorName = tOldVendorName from #pTypeTable inActive join  #pTypeTable active on inActive.nPType = active.nPType where inActive.nActiveStatus = 0 and active.nActiveStatus = 1 
	CLOSE db_cursor  
	DEALLOCATE db_cursor 	
	DECLARE db_cursor CURSOR FOR 
	SELECT tProjectId,tTableName from #pTypeTable where nActiveStatus = 0

	OPEN db_cursor  
	FETCH NEXT FROM db_cursor INTO @tmpProjectId, @tmpTableName

	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		-- Old Project
		Set @tQuery = N'update #pTypeTable set nVendorId = nVendor from [dbo].['+ @tmpTableName +'] Where tProjectId =  nProjectId and tProjectId = ' + CAST(@tmpProjectId as VARCHAR) + ' and (nMyActiveStatus = 0 OR nMyActiveStatus is null)'		
		EXEC sp_executesql @tQuery
		--Previous Project Manager
		Set @tQuery = N'Update #pTypeTable set tProjManager = tITPM from tblProjectStakeHolders with(nolock) where tProjectId = tblProjectStakeHolders.nProjectID and tProjectId = ' + CAST(@tmpProjectId as VARCHAR)
		EXEC sp_executesql @tQuery
		FETCH NEXT FROM db_cursor INTO @tmpProjectId, @tmpTableName
	END 
	--Update Previous Vendor Name
	--update #pTypeTable set tOldVendorName = tOldVendorName from #pTypeTable inActive join  #pTypeTable active on inActive.nPType = active.nPType where inActive.nActiveStatus = 0 and active.nActiveStatus = 1 
	
	CLOSE db_cursor  
	DEALLOCATE db_cursor 	
	
	Set @tQuery = N'Update #pTypeTable set tVendorName = tblVendor.tVendorName from tblVendor with(nolock) where nVendorId = tblVendor.aVendorId'  
	EXEC sp_executesql @tQuery

	create table #resutTable(tProjectId int, nPType int, tOldVendorName varchar(500), tNewVendorName varchar(500), tTableName varchar(100), nActiveStatus int, tPrevProjManager Varchar(500))

	insert into #resutTable(tProjectId,nPType,tNewVendorName) select tProjectId, nPType, tVendorName from #pTypeTable where nActiveStatus = 1

	update #resutTable set tOldVendorName = tVendorName, tPrevProjManager = tProjManager from #pTypeTable where #resutTable.nPType = #pTypeTable.nPType and #pTypeTable.nActiveStatus =0
	--select * from #resutTable
	drop table #pTypeTable

	select aProjectId, tStoreNumber, dbo.fn_getProjectType(nProjectType) tProjectType,nProjectType,  dDown.tDropdownText tStatus,tPrevProjManager tPrevProjManager, sHolder.tITPM tProjManager, tblProject.dGoLiveDate dProjectGoliveDate,
	tblProject.dProjEndDate dProjEndDate, tOldVendorName tOldVendor, tNewVendorName tNewVendor 
	from tblProject with(nolock) 
	join tblStore with(nolock) on aStoreId = tblProject.nStoreId
	left join tblProjectStakeHolders sHolder with(nolock) on sHolder.nProjectID = aProjectID
	join (select aDropDownId, tDropdownText from tblDropDowns with(nolock) where aDropDownId in(Select nDropdownId from tblDropDownMain with(nolock) where tModuleName = 'ProjectStatus')) dDown on tblProject.nProjectStatus = dDown.aDropDownId
	Join #resutTable on tProjectId = aProjectId
	where nProjectActiveStatus = 1 and aStoreId = @nStoreId

	drop table #resutTable
END

GO

Create procedure sproc_UpdateProjectEndDateForHistoricProjects
@nStoreId int
as
BEGIN
	Declare @curProjectId int, @curProjectType int
	Declare @curGoLiveDate date
	DECLARE db_cursor CURSOR FOR 
	SELECT aProjectID,nProjectType from tblProject  with(nolock) where nProjectActiveStatus = 0 and nStoreID = @nStoreId

	OPEN db_cursor  
	FETCH NEXT FROM db_cursor INTO @curProjectId, @curProjectType

	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		if(EXISTS(select top 1 1 from tblProject  with(nolock) where nStoreID = @nStoreId and nProjectActiveStatus = 1 and aProjectID > @curProjectId and nProjectType = @curProjectType))
			select top 1 @curGoLiveDate = dGoliveDate from tblProject with(nolock) where nStoreID = @nStoreId and aProjectID > @curProjectId and nProjectType = @curProjectType order by aProjectID asc
		else
			select top 1 @curGoLiveDate = dGoliveDate from tblProject  with(nolock) where nStoreID = @nStoreId and aProjectID > @curProjectId order by aProjectID asc
		if(@curGoLiveDate is not null)
		BEGIN
			update tblProject set dProjEndDate = DATEADD(day,-1, @curGoLiveDate) where aProjectID = @curProjectId
		END
		FETCH NEXT FROM db_cursor INTO @curProjectId, @curProjectType
	END 
		CLOSE db_cursor  
	DEALLOCATE db_cursor 	
END

GO

Create   procedure sproc_getHistoricalProjects
@nStoreId int
as
BEGIN	
	exec sproc_UpdateProjectEndDateForHistoricProjects @nStoreId
	create table #pTypeTable(tProjectId int, nPType int, nVendorId INT,nProjectStatus int, tProjectStatus varchar(100), tVendorName varchar(500), tTableName varchar(100), tProjManager Varchar(500))

	insert into #pTypeTable (tProjectId, tTableName,nPType,nProjectStatus) select aProjectId, tTableName, nPType,nProjectStatus from(
	select aProjectId, tTableName, nProjectType nPType,nProjectStatus from (select max(aProjectId) aProjectId, nProjectType, nProjectStatus from tblProject where nStoreID = @nStoreId and (nProjectActiveStatus = 0 or nProjectActiveStatus is null)
group by nProjectType,nProjectStatus) tmpTable left join 
	tblProjectTypeConfig on tblProjectTypeConfig.aTypeId = case when (tmpTable.nProjectType < 5 OR  tmpTable.nProjectType = 9) then -1 else tmpTable.nProjectType end
	) tTable 
	declare @tQuery NVARCHAR(MAX), @tmpProjectId int, @tmpTableName VARCHAR(100)
	DECLARE db_cursor CURSOR FOR 
	SELECT tProjectId,tTableName from #pTypeTable

	OPEN db_cursor  
	FETCH NEXT FROM db_cursor INTO @tmpProjectId, @tmpTableName

	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		Set @tQuery = N'update #pTypeTable set nVendorId = nVendor from [dbo].['+ @tmpTableName +'] Where tProjectId =  nProjectId and tProjectId = ' + CAST(@tmpProjectId as VARCHAR)		
		EXEC sp_executesql @tQuery
		FETCH NEXT FROM db_cursor INTO @tmpProjectId, @tmpTableName
	END 
	
	Set @tQuery = N'Update #pTypeTable set tVendorName = tblVendor.tVendorName from tblVendor with(nolock) where nVendorId = tblVendor.aVendorId'  
	EXEC sp_executesql @tQuery

	Set @tQuery = N'Update #pTypeTable set tProjectStatus = tDropdownText from tblDropDowns with(nolock) where aDropDownId = nProjectStatus'  
	EXEC sp_executesql @tQuery
	
	update #pTypeTable set tProjManager = tITPM from tblProjectStakeHolders with(nolock) where nProjectID = #pTypeTable.tProjectId
	
	--Update Previous Vendor Name
	--update #pTypeTable set tOldVendorName = tOldVendorName from #pTypeTable inActive join  #pTypeTable active on inActive.nPType = active.nPType where inActive.nActiveStatus = 0 and active.nActiveStatus = 1 
	
	CLOSE db_cursor  
	DEALLOCATE db_cursor 	
	--select * from #pTypeTable

	select aProjectId nProjectId, tStoreNumber, dbo.fn_getProjectType(nProjectType) tProjectType,nProjectType, tProjectStatus tStatus, tProjManager tProjManager, tblProject.dGoLiveDate dProjectGoliveDate,
	tblProject.dProjEndDate dProjEndDate, tVendorName tVendor, dProjEndDate 
	from tblProject with(nolock)
	join tblStore with(nolock) on aStoreId = tblProject.nStoreId 
	Join #pTypeTable on tProjectId = aProjectId

	drop table #pTypeTable
END

Go
Create procedure sproc_getTechnologyData
@nStoreId as int,
@tTechnologyTableName as nvarchar(100)=''
as
Begin
--declare @nProjectId as int
--set @nProjectId=0		
declare @tTech nvarchar(Max)   
set @tTech='Select top 1 * '  
set @tTech=@tTech +' From '+@tTechnologyTableName +' with (nolock) Where  nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID='+cast(@nStoreId as nvarchar(20))+' and nProjectActiveStatus=1) and nMyActiveStatus=1'
print  @tTech 
EXEC (@tTech)  
End
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
 --values (@tStoreName,@tProjectType,@tStoreNumber,@tAddress,@tCity,@tState,@nDMAID,@tDMA,@tRED,@tCM,@tANE,@tRVP,@tPrincipalPartner,@dStatus,@dOpenStore, @tProjectStatus, @nCreatedBy,@nBrandId )              
 declare @nStoreId int, @nProjectStatus INT, @nProjectId INT, @nStoreState INT                      
 Select @nProjectStatus = aDropDownId from tblDropdowns WHERE tDropdownText = @tProjectStatus                
 Select @nStoreState = aDropDownId from tblDropdowns WHERE tDropdownText = @tState           
              
 select @nStoreId=aStoreID from tblStore with(nolock) where tStoreNumber=@tStoreNumber               
 if (@nStoreId>0)              
 begin              
  update tblProject set tProjectName=@tStoreName,dGoLiveDate=@dOpenStore,nProjectType=@nProjectType,nProjectStatus=@nProjectStatus,    
  nDMAID=@nDMAID,tDMA=@tDMA,nBrandID=@nBrandId,dtUpdatedOn=GETDATE(),@nProjectId=aProjectID    
  where nStoreID=@nStoreId   
     
  update tblStore set tStoreName=@tStoreName,tStoreAddressLine1=@tAddress,tCity=@tCity,nStoreState=@nStoreState,dtUpdatedOn=GETDATE()     
  where aStoreID=@nStoreId  
    
  update tblProjectStakeHolders set tRVP=@tRVP,tRED=@tRED,tCM=@tCM,tAandE=@tANE,tPrincipalPartner=@tPrincipalPartner    
  where nStoreId=@nStoreId  
 end              
 else               
 begin              
  INSERT INTO tblStore(tStoreNumber, nCreatedBy, dtCreatedOn, tStoreName, tStoreAddressLine1, tCity, nStoreState) VALUES(@tStoreNumber, @nCreatedBy, GETDATE(), @tStoreName, @tAddress, @tCity,@nStoreState)                
  SET @nStoreId = @@IDENTITY    
              
  INSERT INTO tblProject(tProjectName,nStoreID,dGoLiveDate,nProjectType,nProjectStatus,nDMAID,tDMA,nBrandID,nCreatedBy,dtCreatedOn, nProjectActiveStatus)                
  VALUES(@tStoreName, @nStoreId, @dOpenStore,@nProjectType,  @nProjectStatus, @nDMAID, @tDMA, @nBrandId, @nCreatedBy,GETDATE(),1)                
                
  SET @nProjectId = @@IDENTITY                   
       
  insert into tblProjectStakeHolders(nProjectID,tRVP,tRED,tCM,tAandE,tPrincipalPartner, nStoreId)      
  values(@nProjectId,@tRVP,@tRED,@tCM,@tANE,@tPrincipalPartner, @nStoreId)        
              
 end                  
      
END

GO

Create Procedure sproc_CopyTechnologyToCurrentProject    
@nStoreId int,    
@nProjectType int,  
@nProjectID int,
@nFromProjectId int
as                  
BEGIN          
 declare @nPrevProjectType int  
 set @nPrevProjectType=0  
 select @nPrevProjectType=@nProjectType from tblProject with(nolock) where aProjectID = @nFromProjectId
  --(0,'New'),(1,'Rebuild'),(2,'Remodel'),(3,'Relocation'),(4,'Acquisition'),(5,'POSInstallation'),(6,'AudioInstallation'),(7,'MenuInstallation'),(8,'PaymentTerminalInstallation'),(9,'PartsReplacement')    
  
 if(@nPrevProjectType<> 5 or  @nPrevProjectType<> 6 or @nPrevProjectType<> 7 or @nPrevProjectType<> 8)  
 begin  
 update tblProjectNetworking set nMyActiveStatus=0 where nStoreId =@nStoreId  
 insert into tblProjectNetworking (nProjectID,nVendor,nPrimaryStatus,dPrimaryDate,nPrimaryType,nBackupStatus,dBackupDate,nBackupType,nTempStatus,dTempDate,nTempType,nStoreId,nMyActiveStatus)   
 (select top 1 @nProjectID,nVendor,nPrimaryStatus,dPrimaryDate,nPrimaryType,nBackupStatus,dBackupDate,nBackupType,nTempStatus,dTempDate,nTempType,@nStoreId,1 from tblProjectNetworking with (nolock)  where nProjectID=@nFromProjectId)  
  
 update tblProjectSonicRadio set nMyActiveStatus=0 where nStoreId =@nStoreId  
 insert into tblProjectSonicRadio (nProjectID,nVendor,nOutdoorSpeakers,nColors,nIndoorSpeakers,nZones,nServerRacks,nStatus,dDeliveryDate,cCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,nOutdoorSpeakers,nColors,nIndoorSpeakers,nZones,nServerRacks,nStatus,dDeliveryDate,cCost,@nStoreId,1 from tblProjectSonicRadio with (nolock) where nProjectID=@nFromProjectId  
  
  update tblProjectStakeHolders set nMyActiveStatus=0 where nStoreId =@nStoreId    -- no need to copy Stakeholder as it will be created through newStore API

 if(@nPrevProjectType=5)--5,'POSInstallation'  
 Begin  
 update tblProjectAudio set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectAudio (nProjectID,nVendor,nStatus,nConfiguration,dDeliveryDate,nLoopStatus,nLoopType,dLoopDeliveryDate,cCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,nStatus,nConfiguration,dDeliveryDate,nLoopStatus,nLoopType,dLoopDeliveryDate,cCost,@nStoreId,1 from tblProjectAudio with (nolock) where nProjectID=@nFromProjectId  
  
  update tblProjectExteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectExteriorMenus (nProjectID,nVendor,nStalls,nPatio,nFlat,nDTPops,nDTMenu,nStatus,dDeliveryDate,cFabConCost,cIDTechCost,cTotalCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,nStalls,nPatio,nFlat,nDTPops,nDTMenu,nStatus,dDeliveryDate,cFabConCost,cIDTechCost,cTotalCost,@nStoreId,1 from tblProjectExteriorMenus with (nolock) where nProjectID=@nFromProjectId  
  
 update tblProjectInteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectInteriorMenus (nProjectID,nVendor,nDMBQuantity,nStatus,dDeliveryDate,cCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,nDMBQuantity,nStatus,dDeliveryDate,cCost,@nStoreId,1 from tblProjectInteriorMenus with (nolock) where nProjectID=@nFromProjectId  
  
  update tblProjectPaymentSystem set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectPaymentSystem (nProjectID,nVendor,nBuyPassID,nServerEPS,nStatus,dDeliveryDate,nPAYSUnits,n45Enclosures,n90Enclosures,nDTEnclosures,n15SunShields,nUPS,nShelf,cCost,nType,nStoreId,nMyActiveStatus)  
  select top 1 @nProjectID,nVendor,nBuyPassID,nServerEPS,nStatus,dDeliveryDate,nPAYSUnits,n45Enclosures,n90Enclosures,nDTEnclosures,n15SunShields,nUPS,nShelf,cCost,nType,@nStoreId,1 from tblProjectPaymentSystem with (nolock) where nProjectID=@nFromProjectId  
  
 End  
 else if(@nPrevProjectType=6)--6,'AudioInstallation'  
 Begin  
 update tblProjectPOS set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectPOS (nProjectID,nVendor,dDeliveryDate,dConfigDate,dSupportDate,nStatus,nPaperworkStatus,cCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,dDeliveryDate,dConfigDate,dSupportDate,nStatus,nPaperworkStatus,cCost,@nStoreId,1 from tblProjectPOS with (nolock) where nProjectID=@nFromProjectId  
  
  update tblProjectExteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectExteriorMenus (nProjectID,nVendor,nStalls,nPatio,nFlat,nDTPops,nDTMenu,nStatus,dDeliveryDate,cFabConCost,cIDTechCost,cTotalCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,nStalls,nPatio,nFlat,nDTPops,nDTMenu,nStatus,dDeliveryDate,cFabConCost,cIDTechCost,cTotalCost,@nStoreId,1 from tblProjectExteriorMenus with (nolock) where nProjectID=@nFromProjectId  
  
 update tblProjectInteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectInteriorMenus (nProjectID,nVendor,nDMBQuantity,nStatus,dDeliveryDate,cCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,nDMBQuantity,nStatus,dDeliveryDate,cCost,@nStoreId,1 from tblProjectInteriorMenus with (nolock) where nProjectID=@nFromProjectId  
  
  update tblProjectPaymentSystem set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectPaymentSystem (nProjectID,nVendor,nBuyPassID,nServerEPS,nStatus,dDeliveryDate,nPAYSUnits,n45Enclosures,n90Enclosures,nDTEnclosures,n15SunShields,nUPS,nShelf,cCost,nType,nStoreId,nMyActiveStatus)  
  select top 1 @nProjectID,nVendor,nBuyPassID,nServerEPS,nStatus,dDeliveryDate,nPAYSUnits,n45Enclosures,n90Enclosures,nDTEnclosures,n15SunShields,nUPS,nShelf,cCost,nType,@nStoreId,1 from tblProjectPaymentSystem with (nolock) where nProjectID=@nFromProjectId  
  
 End  
 else if(@nPrevProjectType=7)--7,'MenuInstallation'  
 Begin  
 update tblProjectAudio set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectAudio (nProjectID,nVendor,nStatus,nConfiguration,dDeliveryDate,nLoopStatus,nLoopType,dLoopDeliveryDate,cCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,nStatus,nConfiguration,dDeliveryDate,nLoopStatus,nLoopType,dLoopDeliveryDate,cCost,@nStoreId,1 from tblProjectAudio with (nolock) where nProjectID=@nFromProjectId  
  
 update tblProjectPOS set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectPOS (nProjectID,nVendor,dDeliveryDate,dConfigDate,dSupportDate,nStatus,nPaperworkStatus,cCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,dDeliveryDate,dConfigDate,dSupportDate,nStatus,nPaperworkStatus,cCost,@nStoreId,1 from tblProjectPOS with (nolock) where nProjectID=@nFromProjectId  
  
   update tblProjectPaymentSystem set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectPaymentSystem (nProjectID,nVendor,nBuyPassID,nServerEPS,nStatus,dDeliveryDate,nPAYSUnits,n45Enclosures,n90Enclosures,nDTEnclosures,n15SunShields,nUPS,nShelf,cCost,nType,nStoreId,nMyActiveStatus)  
  select top 1 @nProjectID,nVendor,nBuyPassID,nServerEPS,nStatus,dDeliveryDate,nPAYSUnits,n45Enclosures,n90Enclosures,nDTEnclosures,n15SunShields,nUPS,nShelf,cCost,nType,@nStoreId,1 from tblProjectPaymentSystem with (nolock) where nProjectID=@nFromProjectId  
  
 End  
  else if(@nPrevProjectType=8)--8,'PaymentTerminalInstallation'  
 Begin  
 update tblProjectAudio set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectAudio (nProjectID,nVendor,nStatus,nConfiguration,dDeliveryDate,nLoopStatus,nLoopType,dLoopDeliveryDate,cCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,nStatus,nConfiguration,dDeliveryDate,nLoopStatus,nLoopType,dLoopDeliveryDate,cCost,@nStoreId,1 from tblProjectAudio with (nolock) where nProjectID=@nFromProjectId  
  
 update tblProjectPOS set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectPOS (nProjectID,nVendor,dDeliveryDate,dConfigDate,dSupportDate,nStatus,nPaperworkStatus,cCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,dDeliveryDate,dConfigDate,dSupportDate,nStatus,nPaperworkStatus,cCost,@nStoreId,1 from tblProjectPOS with (nolock) where nProjectID=@nFromProjectId  
  
  update tblProjectExteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectExteriorMenus (nProjectID,nVendor,nStalls,nPatio,nFlat,nDTPops,nDTMenu,nStatus,dDeliveryDate,cFabConCost,cIDTechCost,cTotalCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,nStalls,nPatio,nFlat,nDTPops,nDTMenu,nStatus,dDeliveryDate,cFabConCost,cIDTechCost,cTotalCost,@nStoreId,1 from tblProjectExteriorMenus with (nolock) where nProjectID=@nFromProjectId  
  
 update tblProjectInteriorMenus set nMyActiveStatus=0 where nStoreId =@nStoreId  
  insert into tblProjectInteriorMenus (nProjectID,nVendor,nDMBQuantity,nStatus,dDeliveryDate,cCost,nStoreId,nMyActiveStatus)  
 select top 1 @nProjectID,nVendor,nDMBQuantity,nStatus,dDeliveryDate,cCost,@nStoreId,1 from tblProjectInteriorMenus with (nolock) where nProjectID=@nFromProjectId  
  
 End  
 End
 END
 GO
 alter procedure sproc_getDynamicDataFromCompID    
@nQuoteRequestTechCompid as int,    
--@nTemplateId as int,    
@nStoreId as int    
AS    
Begin    
--declare @nQuoteRequestTechCompid int    
--set @nQuoteRequestTechCompid=11    
declare @tTechComp nvarchar(Max)    
declare @tableName nvarchar(Max)    
declare @ColumnID nvarchar(Max)    
set @tTechComp='Select top 1 '    
select @tTechComp=@tTechComp+ dbo.getColumnDataType(tTechCompField)+' as ['+tTechCompFieldName+'], ' from tblQuoteRequestTechCompFields  with (nolock) where nQuoteRequestTechCompid=@nQuoteRequestTechCompid    
set @tTechComp=LEFT(@tTechComp, LEN(@tTechComp) - 1)    
print @tTechComp    
select @tableName=tTableName from  tblQuoteRequestTechComp  with (nolock) where aQuoteRequestTechCompId=@nQuoteRequestTechCompid    
print @tableName    
--if(@tableName='tblProjectStore')    
----select @ColumnID=COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMNPROPERTY(object_id(@tableName), COLUMN_NAME, 'IsIdentity') = 1    
--set @ColumnID='aProjectStoreID'    
--else    
set @ColumnID='nProjectID'    
--print @ColumnID    
--set @tTechComp=@tTechComp +' From '+@tableName +' with (nolock) Where ' +@ColumnID +'='+cast(@nTemplateId as nvarchar(20))    
print @tTechComp  
 if(@tableName='tblProjectStore')
 set @tableName='tblstore'
if (@tableName='tblstore')  
set @tTechComp=@tTechComp +' From '+@tableName +' with (nolock) Where  aStoreID='+cast(@nStoreId as nvarchar(20)) 
else if (@tableName='tblProjectConfig')  
set @tTechComp=@tTechComp +' From '+@tableName +' with (nolock) Where   nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID='+cast(@nStoreId as nvarchar(20))+' and nProjectActiveStatus=1)' 
else  
set @tTechComp=@tTechComp +' From '+@tableName +' with (nolock) Where    nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID='+cast(@nStoreId as nvarchar(20))+' and nProjectActiveStatus=1) and nMyActiveStatus=1'  
print @tTechComp    
 EXEC (@tTechComp)    
    
 End
 go 
 alter procedure [dbo].[sproc_GetPurchaseOrdeStorerDetails]     
@nStoreId int  
AS      
BEGIN      
 Select  top 1 tStoreName as tStore,tstoreNumber,  
 tPOC as tName,tPOCPhone as tPhone,tPOCEmail as tEmail,tStoreAddressLine1  as tAddress,tStoreAddressLine2 tAddress2,  tCity,  
  (Select top 1 tDropdownText from tblDropdowns  with (nolock) WHERE aDropdownId = nStoreState) As [tstoreState],  
  tStoreZip,tBillToEmail,tBillToAddress,tBillToCity,  
  (Select top 1 tDropdownText from tblDropdowns  with (nolock) WHERE aDropdownId = nBillToState) As [tBillToState],  
  tBillToZip,tITPM as tProjectManager  
 from tblStore A with (nolock)  
 --inner join   tblProject B  with (nolock) on B.aProjectID=A.nProjectID  
 --inner join tblstore C with (nolock) on C.aStoreID=B.nStoreID  
 inner join tblProjectStakeHolders D with (nolock) on A.aStoreId=D.nStoreId  
  where A.aStoreId = @nStoreId  
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
  ELSE CAST(Getdate() as nvarchar(50))  
  END, CAST(Getdate() as nvarchar(50))) AS dDeliveryDate,  
  (select top 1 tVendorName  from tblvendor with (nolock) where aVendorId=nVendorID) as tVendorName,  
  --gettToByVendor(nVendorID) as tTo,  
  '' as tTo,  
  --getCCByVendor(nVendorID) as tCC  
  '' as tCC  
  
 from tblPurchaseOrderTemplate with (nolock) where aPurchaseOrderTemplateID = @aPurchaseOrderTemplateID        
END    
Go
alter procedure sproc_GetPurchaseOrderPartsDetails     
@nPurchaseOrderTemplateID int,  
@nStoreId int=0  
AS      
BEGIN      
  
if(@nStoreId>0)  
Begin  
  
  
  
  
DECLARE @A_Table TABLE(aPurchaseOrderTemplatePartsID int,sqlcommand nvarchar(1000), nQuantity int)  
insert into @A_Table  
 Select aPurchaseOrderTemplatePartsID,  (N'select @nQuantity= CAST ('+ tTechCompField + '  as int) from ' +tTableName + ' with (nolock) where nProjectID in(select aProjectID from tblproject with (nolock) where nStoreID='+cast(@nStoreId as nvarchar(20))+' and nProjectActiveStatus=1) and nMyActiveStatus=1' ) as query,0  
 from tblPurchaseOrderTemplateParts  A with (nolock) where nPurchaseOrderTemplateID = @nPurchaseOrderTemplateID  
  
   
DECLARE @aPurchaseOrderTemplatePartsID int  
DECLARE @sqlcommand nvarchar(1000)  
DECLARE  @Result AS INT   
DECLARE db_cursor CURSOR FOR   
SELECT aPurchaseOrderTemplatePartsID,sqlcommand FROM @A_Table ORDER BY 1  
  
OPEN db_cursor    
FETCH NEXT FROM db_cursor INTO @aPurchaseOrderTemplatePartsID,@sqlcommand    
  
WHILE @@FETCH_STATUS = 0    
BEGIN    
  PRINT @sqlcommand  
     
  Set @Result=0   
  EXEC sp_executesql @sqlcommand , N'@nStoreId INT,@nQuantity INT OUTPUT' , @nStoreId=@nStoreId ,@nQuantity=@Result OUTPUT  
  
  --print @Result  
  update @A_Table  set nQuantity =@Result  where aPurchaseOrderTemplatePartsID=@aPurchaseOrderTemplatePartsID  
  
       FETCH NEXT FROM db_cursor INTO @aPurchaseOrderTemplatePartsID,@sqlcommand    
        
END    
--select * from @A_Table  
CLOSE db_cursor    
DEALLOCATE db_cursor  
  
 Select A.aPurchaseOrderTemplatePartsID,nPurchaseOrderTemplateID,nPartID,tPartDesc,tPartNumber,cPrice,tTechCompField,tTableName,isnull(nQuantity,0) as nQuantity  
 from tblPurchaseOrderTemplateParts  A with (nolock)    
 inner join tblParts B  with (nolock) on A.nPartID=B.aPartID  
 inner join @A_Table C  on C.aPurchaseOrderTemplatePartsID=A.aPurchaseOrderTemplatePartsID    
 where nPurchaseOrderTemplateID = @nPurchaseOrderTemplateID  
 End  
 else  
 Begin  
  
 Select aPurchaseOrderTemplatePartsID,nPurchaseOrderTemplateID,nPartID,tPartDesc,tPartNumber,cPrice,tTechCompField,tTableName , 0 nQuantity  
 from tblPurchaseOrderTemplateParts  A with (nolock)    
 inner join tblParts B  with (nolock) on A.nPartID=B.aPartID  
 where nPurchaseOrderTemplateID = @nPurchaseOrderTemplateID  
 End  
   
END   

GO
ALTER  PROC sproc_UserLogin               
(              
@tUserName NVARCHAR(255),              
@tPassword NVARCHAR(255),              
@tName NVARCHAR(255) OUT,              
@tEmail NVARCHAR(255) OUT,              
@nRoleType NVARCHAR(255) OUT,             
@nUserID INT OUT               
)              
AS              
BEGIN              
   select @tName = tName, @tEmail = tEmail, @nRoleType = nRole, @nUserID = aUserID from tblUser with(nolock) where tUserName = @tUserName and tPassword = @tPassword  
END 
GO
update tblUser set tUserName = 'admin', tPassword = 'YWRtaW4=' where aUserID = 2
GO
