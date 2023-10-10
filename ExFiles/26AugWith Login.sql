alter table tblSupportTicket add tTicketStatus varchar(100), tFixComment NVARCHAR(MAX), dtUpdatedOn datetime, nUpdateBy int


Create proc sproc_getSupportTicket
AS
BEGIN
	select aTicketId,nPriority,tContent,nFileSie,fBase64,tName as tCreatedBy,tblSupportTicket.nCreatedBy, tblSupportTicket.dtCreatedOn,tTicketStatus,tFixComment from  tblSupportTicket with(nolock) join tblUser with(nolock) on tblSupportTicket.nCreatedBy = aUserID
	order by aTicketId desc
END

GO

--update tblDropdownModule set nBrandID = 0 where tModuleGroup in('User','Vendor')

select * from tblDropdownModule

delete from tblDropDownModule where tModuleName = 'StackHolderCD'--- this is not required since CD is text Property

create table tblDropdownModuleBrandRel(aRelId int identity primary key, nBrandId int not null, nModuleId int not null)

alter table tblDropdownModule drop column nBrandId

Insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) select 0, aModuleId from tblDropdownModule where tModuleGroup in('User','Vendor')

select * from tblDropdownModuleBrandRel
select * from tblBrand-- Get Brand Id's

Insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) select 1, aModuleId from tblDropdownModule where tModuleGroup not in('User','Vendor')
Insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) select 2, aModuleId from tblDropdownModule where tModuleGroup not in('User','Vendor')
Insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) select 3, aModuleId from tblDropdownModule where tModuleGroup not in('User','Vendor')
Insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) select 4, aModuleId from tblDropdownModule where tModuleGroup not in('User','Vendor')
Insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) select 5, aModuleId from tblDropdownModule where tModuleGroup not in('User','Vendor')
Insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) select 6, aModuleId from tblDropdownModule where tModuleGroup not in('User','Vendor')
select * from tblDropdowns
select * from tblDropdownModuleBrandRel

update tblDropdowns set nBrandId = 1 from tblDropdownModuleBrandRel where tblDropdownModuleBrandRel.nModuleId = tblDropdowns.nModuleId

select * from tblDropdownModuleBrandRel
alter table tblDropdowns add nModuleId int
alter table tblDropdowns add nBrandId int
update tblDropdowns set nBrandId =1 where nbrandId = 6

select * from tblDropdownMain

insert into tblDropdowns(tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId)
select tDropdownText,getDate(),0,0,nFunction,nModuleId,2 from tblDropdowns where nbrandId = 1
insert into tblDropdowns(tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId)
select tDropdownText,getDate(),0,0,nFunction,nModuleId,3from tblDropdowns where nbrandId = 1
insert into tblDropdowns(tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId)
select tDropdownText,getDate(),0,0,nFunction,nModuleId,4 from tblDropdowns where nbrandId = 1
insert into tblDropdowns(tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId)
select tDropdownText,getDate(),0,0,nFunction,nModuleId,5 from tblDropdowns where nbrandId = 1
insert into tblDropdowns(tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId)
select tDropdownText,getDate(),0,0,nFunction,nModuleId,6 from tblDropdowns where nbrandId = 1

--update tblDropdownModuleBrandRel set nBrandId = 6 where nModuleId in (select aModuleId from tblDropdownModule where tModuleGroup in('All'))

GO

Alter procedure sproc_getDropdownModules  
@nBrandId int  
AS  
BEGIN  
 select aModuleId,nBrandId,tModuleName,tModuleDisplayName,tModuleGroup, editable from tblDropdownModule with(nolock) join tblDropdownModuleBrandRel with(nolock) on aModuleId = nModuleId
END

GO
Alter table tblStore add nBrandID int
GO

  
Alter procedure sproc_SearchStore              
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
Insert into tblDropdowns (tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId) values('On Track',getdate(),0,0,0,29,1)
Insert into tblDropdowns (tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId) values('Action Required',getdate(),0,0,0,29,1)
Insert into tblDropdowns (tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId) values('Complete',getdate(),0,0,0,29,1)
Insert into tblDropdowns (tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId) values('At Risk',getdate(),0,0,0,29,1)
update tblDropdowns set nBrandId =1, nModuleId = 29 where aDropdownId = 216

select * from tblDropdowns where nBrandId = 4
select * from tblBrand

insert into tblDropdowns(tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId)
select tDropdownText,getDate(),0,0,nFunction,nModuleId,6 from tblDropdowns where nbrandId = 1 


and nModuleId =29
insert into tblDropdowns(tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId)
select tDropdownText,getDate(),0,0,nFunction,nModuleId,3from tblDropdowns where nbrandId = 1 and nModuleId =29
insert into tblDropdowns(tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId)
select tDropdownText,getDate(),0,0,nFunction,nModuleId,4 from tblDropdowns where nbrandId = 1 and nModuleId =29
insert into tblDropdowns(tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId)
select tDropdownText,getDate(),0,0,nFunction,nModuleId,5 from tblDropdowns where nbrandId = 1 and nModuleId =29
insert into tblDropdowns(tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId)
select tDropdownText,getDate(),0,0,nFunction,nModuleId,6 from tblDropdowns where nbrandId = 1 and nModuleId =29

--Insert into tblDropdowns (tDropdownText,dtCreatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId) values('Under Construction',getdate(),0,0,0,29,1)


GO
select * from tblDropdowns where nModuleId is null
select * from tblDropdownModule where aModuleId not in (select nModuleId from tblDropdowns where nModuleId is not null)
alter table tblDropDowns add nModuleId int, nBrandId int
update tblDropdowns set nModuleId = aModuleId from tblDropdowns join tblDropDownMain on aDropdownId = nDropdownId  join tblDropdownModule on tblDropdownModule.tModuleName = tblDropDownMain.tModuleName
drop table tblDropdownMain
--drop table tblDropdownModuleBrandRel
--update tblDropdowns set tblDropdowns.nBrandId = tblDropdownModuleBrandRel.nBrandId from tblDropdownModuleBrandRel where tblDropdownModuleBrandRel.nModuleId = tblDropdowns.nModuleId


select * from tblDropdownModuleBrandRel with(nolock) join tblDropdownModule with(nolock) on aModuleId = nModuleId join tblDropdownMain with(nolock) on tblDropdownMain.tModuleName = tblDropdownModule.tModuleName
where tblDropdownModule.tModuleName = 'ExteriorMenuStatus'
and nBrandId = 6
select * from tblDropdownModule
select * from tblDropdownMain
select * from tblDropDowns where tDropdownText like '%under%'
select * from tblProject
select * from tblDropdownModuleBrandRel
GO
GO

-------------------------------Multiple Brands-------------------------------



--update tblProject set nBrandID=2 where nstoreid in (5,10,11)
Go
Alter procedure sproc_SearchStore          
@tText as VARCHAR(500),
@nBrandID int=0
AS          
BEGIN  
  
Select nStoreId, tStoreName,tStoreNumber,tProjectsInfo  
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
                
 select @nStoreId=aStoreID from tblStore with(nolock) where tStoreNumber=@tStoreNumber  and nBrandID=@nBrandId                    
 if (@nStoreId>0)                
 begin                
  update tblProject set tProjectName=@tStoreName,dGoLiveDate=@dOpenStore,nProjectType=@nProjectType,nProjectStatus=@nProjectStatus,      
  nDMAID=@nDMAID,tDMA=@tDMA,nBrandID=@nBrandId,dtUpdatedOn=GETDATE(),@nProjectId=aProjectID      
  where nStoreID=@nStoreId     
       
  update tblStore set tStoreName=@tStoreName,tStoreAddressLine1=@tAddress,tCity=@tCity,nStoreState=@nStoreState,dtUpdatedOn=GETDATE(),nBrandID=@nBrandId      
  where aStoreID=@nStoreId    
      
  update tblProjectStakeHolders set tRVP=@tRVP,tRED=@tRED,tCM=@tCM,tAandE=@tANE,tPrincipalPartner=@tPrincipalPartner      
  where nStoreId=@nStoreId    
 end                
 else                 
 begin                
  INSERT INTO tblStore(tStoreNumber, nCreatedBy, dtCreatedOn, tStoreName, tStoreAddressLine1, tCity, nStoreState,nBrandID) VALUES(@tStoreNumber, @nCreatedBy, GETDATE(), @tStoreName, @tAddress, @tCity,@nStoreState,@nBrandID)                  
  SET @nStoreId = @@IDENTITY      
                
  INSERT INTO tblProject(tProjectName,nStoreID,dGoLiveDate,nProjectType,nProjectStatus,nDMAID,tDMA,nBrandID,nCreatedBy,dtCreatedOn, nProjectActiveStatus)                  
  VALUES(@tStoreName, @nStoreId, @dOpenStore,@nProjectType,  @nProjectStatus, @nDMAID, @tDMA, @nBrandId, @nCreatedBy,GETDATE(),1)                  
                  
  SET @nProjectId = @@IDENTITY                     
         
  insert into tblProjectStakeHolders(nProjectID,tRVP,tRED,tCM,tAandE,tPrincipalPartner, nStoreId)        
  values(@nProjectId,@tRVP,@tRED,@tCM,@tANE,@tPrincipalPartner, @nStoreId)          
                
 end                    
        
END


GO

alter procedure sproc_getActivePortFolioProjects    
@nBrandId int=0    
as    
BEGIN   
  create table #tmpTable(nStoreId int,nProjectId int,nProjectType int,tProjectType varchar(100), tStoreNumber varchar(100), tStoreDetails VARCHAR(500),dProjectGoliveDate date,  dProjEndDate date, tProjManager varchar(500), tStatus varchar(100), tNewVendor
 varchar(500),tTableName VARCHAR(100),tFranchise VARCHAR(100),cCost decimal)    
  
    
 Insert into #tmpTable(nStoreId,nProjectId,nProjectType,tProjectType,tStatus,dProjectGoliveDate,dProjEndDate,tTableName)-- , tStatus, tPrevProjManager, tProjManager, dProjectGoliveDate, dProjEndDate, tOldVendor, tNewVendor)    
 select nStoreId,aProjectId,nProjectType, dbo.fn_getProjectType(nProjectType) tProjectType, dbo.fn_getDropdownText(nProjectStatus) tStatus,dGoLiveDate as dProjectGoliveDate,dProjEndDate ,tTableName  
 from tblProject with(nolock)    
 left join tblProjectTypeConfig with(nolock) on tblProjectTypeConfig.aTypeId = case when (tblProject.nProjectType < 5 OR  tblProject.nProjectType = 9)  
 then -1 else tblProject.nProjectType end    
 where nBrandId = @nBrandId and  
 nProjectActiveStatus = 1  and dGoLiveDate >=getdate()   
   
    
 declare @tQuery NVARCHAR(MAX), @tmpProjectId int, @tmpTableName VARCHAR(100),@TempnStoreId int  
    
 DECLARE db_cursor CURSOR FOR     
 SELECT nStoreId,nProjectId,tTableName from #tmpTable    
    
 OPEN db_cursor      
 FETCH NEXT FROM db_cursor INTO @TempnStoreId,@tmpProjectId, @tmpTableName    
    
 WHILE @@FETCH_STATUS = 0      
 BEGIN      
  Set @tQuery  = N'update #tmpTable set tNewVendor=dbo.fn_getVendorName(nVendor) from [dbo].['+ @tmpTableName +'] Where #tmpTable.nProjectId = [dbo].['+ @tmpTableName +'].nProjectId and nMyActiveStatus = 1 and #tmpTable.nProjectId = ' + CAST(@tmpProjectId
 as VARCHAR) -- update NewVendor    
  EXEC sp_executesql @tQuery    
   
 print @TempnStoreId  
  update #tmpTable set tStoreNumber = tblStore.tStoreNumber,tStoreDetails=tblStore.tStoreAddressLine1 from tblStore with(nolock) where aStoreID = @TempnStoreId    and nStoreId=@TempnStoreId  
    
  update #tmpTable set tProjManager = tITPM, tFranchise=(select tFranchiseName from tblFranchise with (nolock) where aFranchiseId=nFranchisee) from tblProjectStakeHolders with(nolock) where tblProjectStakeHolders.nStoreId = @TempnStoreId and nMyActiveStatus = 1 and tITPM is not null    and #tmpTable.nStoreId=@TempnStoreId  
   update #tmpTable set cCost = tblProjectConfig.cProjectCost from tblProjectConfig with(nolock) where tblProjectConfig.nStoreId = @TempnStoreId and nMyActiveStatus = 1 and tblProjectConfig.cProjectCost is not null    and #tmpTable.nStoreId=@TempnStoreId 
 
   
  FETCH NEXT FROM db_cursor INTO @TempnStoreId,@tmpProjectId, @tmpTableName    
 END     
     
     
 CLOSE db_cursor      
 DEALLOCATE db_cursor    
    
 select * from #tmpTable   order by dProjectGoliveDate   
    
  drop table #tmpTable  
  
END  

GO


  
ALTER Procedure [dbo].[sproc_GetDropdown]              
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

create table tblLoginSessions(aSessionId int identity primary key, nUserId int not null, tSessionId VARCHAR(1000), nStatus int, tDevice VARCHAR(MAX), dtLoginTime dateTime, dtLogoutTime datetime)

GO

ALTER PROC sproc_UserLogin                 
(                
@tUserName NVARCHAR(255),                
@tPassword NVARCHAR(255),
@tDevice VARCHAR(MAX)
)                
AS                
BEGIN
	declare @aUserId int
	select @aUserId = aUserId from tblUser with(nolock) where UPPER(tUserName) = UPPER(@tUserName) and tPassword = @tPassword       
   Insert into tblLoginSessions(nUserId, tSessionId, nStatus, tDevice, dtLoginTime) values(@aUserId, '', 1, @tDevice, getdate())
   select tName, tUserName, tEmail, case when(nRole is null) then 0 else nRole end, aUserID nUserID, isFirstTime from tblUser with(nolock) where aUserID = @aUserId
END 

GO

CREATE PROC sproc_UserLogout
@nUserId int
AS
BEGIN
	Update tblLoginSessions set dtLogoutTime = getDate(), nStatus = 0 where nUserId = @nUserId and nStatus =1
END

GO

Select * from tblStore
update tblStore set nBrandId = 1
select * from tblbrand
--select * from tblLoginSessions

--select @aUserId from tblUser with(nolock) where UPPER(tUserName) = UPPER(@tUserName) and tPassword = @tPassword   

--sproc_UserLogin 'admin', 'YWRtaW4=', 'IP:::1, Browser:Chrome'
--GO

--sp_tables '%modul%'

--Select * from tblDropdownModule

-- Select tblDropdownModuleBrandRel.nBrandId, tblDropdownMain.tModuleName, aDropDownId, tDropdownText, tblDropdowns.bDeleted, nOrder, nFunction from tblDropdownMain  with(nolock) join tblDropdowns with(nolock) on            
--  aDropdownId = nDropdownId join tblDropdownModule with(nolock) on tblDropdownMain.tModuleName = tblDropdownModule.tModuleName join tblDropdownModuleBrandRel with(nolock) on aModuleId = nModuleId

--select * from tblDropdownMain where tModuleName = 'VendorType'

--sp_helptext sproc_GetDropdown

--sproc_GetDropdown 'NetworkingTempStatus',2

--select * from tblBrand

--sproc_getDropdownModules 2

--select * from tblDropdownModule where tModuleName like '%state%'

--select * from tblDropdownMain where tModuleName = 'UserRole'
--select * from tblDropdowns where 

--sproc_SearchStore '', 6

--select * from tblDropdowns order by 1 desc
--delete from tblDropdowns where aDropdownId >= 130 tModuleName

select * from tbPermission where tPermissionName = 'home.configuration.users'
select * from tblUserPermissionRel where nPermissionId = 3 and nUserId = 2
update tblUserPermissionRel set nPermVal = 2 where nPermissionId = 3 and nUserId = 2
sp_tables '%perm%'

select * from tblDropdowns


sproc_SearchStore '', 1


      
--Select nStoreId, tStoreName,tStoreNumber,tProjectsInfo    
--from(      
-- select aStoreId nStoreId, tStoreName,tStoreNumber, tProjectsInfo  
-- from tblStore with(nolock)  Join      
-- (select nStoreId, STRING_AGG(CAST(aProjectId as varchar) + '_' + CAST(nProjectType as varchar) + '_' + CAST(dGoLiveDate as varchar) , ', ') tProjectsInfo from tblProject with(nolock) where  nBrandID=1 and nProjectActiveStatus = 1      
-- --where ProjectActiveStatus = 1       
-- group by nStoreID) tblProj on aStoreID = nStoreID       
-- where   nBrandID=1 and    
-- tStoreNumber like '' OR      
-- tStoreName like '' 
-- ) as tmpTable order by tStoreNumber   

 select * from tblProject

 select * from tbluser
 update tbluser set 