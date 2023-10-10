Alter proc sproc_getReportData
@nReportId int,
@tParameters VARCHAR(MAX),
@tReportName VARCHAR(500) output
as
BEGIN
	declare @tQuery VARCHAR(5000)
	select @tReportName = tName, @tQuery = tQuery from tblReport with(nolock) where aReportID = @nReportId
	Exec @tQuery
END
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
	if(@aUserId is not null)
	BEGIN	
	   Insert into tblLoginSessions(nUserId, tSessionId, nStatus, tDevice, dtLoginTime) values(@aUserId, '', 1, @tDevice, getdate())  
	   select tName, tUserName, tEmail, case when(nRole is null) then 0 else nRole end, aUserID nUserID, isFirstTime from tblUser with(nolock) where aUserID = @aUserId  
   END
END 
GO

update tblUser set tPassword = '260d23baeb73c2451f00f4a30ea4eb9ccad81b7f0c7dc23f58de70fd1426ab9d' where aUserId = 2

select * from tblUser


--drop table tblReport
CREATE TABLE [dbo].[tblReport](
	[aReportID] [int] IDENTITY(1,1) NOT NULL,
	[tName] varchar(1000) NULL,
	[nReportType] [int] NULL,
	[tQuery] varchar(max) NULL,
	[bSproc] [bit] NULL,
	[nChartType]  [int] NULL,
	[nCreatedBy] [int] NULL,
	[nUpdateBy] [int] NULL,
	[dtCreatedOn] [datetime] NULL,
	[dtUpdatedOn] [datetime] NULL,
	[bDeleted] [bit] NULL

	
PRIMARY KEY CLUSTERED 
(
	[aReportID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[tblReportColumns](
	[aReportColumns] [int] IDENTITY(1,1) NOT NULL,
	[nReportID] [int] NULL,
	[tTitles] varchar(1000) NULL,
	[tHeaders] varchar(1000) NULL,
	[tSelectColumns] varchar(1000) NULL

	
PRIMARY KEY CLUSTERED 
(
	[aReportColumns] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--ALTER TABLE tblReport ADD nChartType int; 


--select * from tblReport
insert into tblReport(tName,nReportType,tQuery,bSproc,nCreatedBy,dtCreatedOn)
values('InstallerReport ',1,'sproc_GetInstallerReport',1,2,GETDATE())
go
insert into tblReport(tName,nReportType,tQuery,bSproc,nCreatedBy,dtCreatedOn)
values('InforPOSReport ',1,'sproc_GetInforPOSReport',1,2,GETDATE())
Go
insert into tblReport(tName,nReportType,tQuery,bSproc,nCreatedBy,dtCreatedOn)
values('OraclePOSReport ',1,'sproc_GetOraclePOSReport',1,2,GETDATE())
Go
insert into tblReport(tName,nReportType,tQuery,bSproc,nCreatedBy,dtCreatedOn)
values('Dashboard Report ',1,'sproc_getDashboardData @pm1,@pm2',1,2,GETDATE())
Go
insert into  tblReportColumns(nReportID,tTitles,tHeaders,tSelectColumns)
values (1,'Type','nProjectType','dbo.fn_getProjectType(nProjectType)'),
 (1,'StoreNo','tStoreNumber',''),
 (1,'City','tCity',''),
 (1,'State','nStoreState','dbo.geDropDownTextByID(nStoreState)'),
 (1,'POS','tblProjectInstallation.nVendor','dbo.fn_getVendorName(tblProjectInstallation.nVendor) as [POS]'),
 (1,'[Quote Status]','tblProjectInstallation.nStatus','dbo.geDropDownTextByID(tblProjectInstallation.nStatus) as [Quote Status]'),
 (1,'[Signoffs]','tblProjectInstallation.nSignoffs','dbo.geDropDownTextByID(tblProjectInstallation.nSignoffs) as	[Signoffs]'),
 (1,'[Test Transactions]','tblProjectInstallation.nTestTransactions','dbo.geDropDownTextByID(tblProjectInstallation.nTestTransactions) as	[Test Transactions]'),
 (1,'[Project Status]','tblProjectInstallation.nProjectStatus','dbo.geDropDownTextByID(tblProjectInstallation.nProjectStatus) as [Project Status]'),
 (1,'[Install Time]','dInstallDate','dInstallDate as	[Install Time]'),
 (1,'[Install Date]','dInstallDate','dInstallDate as [Install Date]'),
 (1,'[Go-Live]','dGoliveDate','dGoliveDate as	[Go-Live]'),
 (1,'[Lead Tech]','tLeadTech','tLeadTech as	[Lead Tech]'),
 (1,'[Installer]','tblProjectInstallation.nVendor','dbo.fn_getVendorName(tblProjectInstallation.nVendor) as	[Installer]'),
 (1,'[IT PM]','tITPM','tITPM as	[IT PM]')
--values (1,'Type','nProjectType','Type')
GO
Create Procedure sproc_GetInstallerReport          
 
as                          
BEGIN 
select dbo.fn_getProjectType(nProjectType) as [Type],tStoreNumber as [StoreNo],tCity as	[City],dbo.geDropDownTextByID(nStoreState) as [State],dbo.fn_getVendorName(D.nVendor) as [POS],	
dbo.geDropDownTextByID(D.nStatus) as 'Quote Status',dbo.geDropDownTextByID(D.nSignoffs) as	[Signoffs],dbo.geDropDownTextByID(D.nTestTransactions) as	[Test Transactions],
dbo.geDropDownTextByID(D.nProjectStatus) as [Project Status],dInstallDate as	[Install Time],	dInstallDate as [Install Date],dGoliveDate as	[Go-Live],tLeadTech as	[Lead Tech],
dbo.fn_getVendorName(D.nVendor) as	[Installer],tITPM as	[IT PM]

from tblstore A with (nolock) inner join 
tblProject B  with (nolock) on A.aStoreID=B.nStoreID and nProjectActiveStatus=1  inner join 
tblProjectPOS C  with (nolock) on A.aStoreID=C.nStoreID and   nMyActiveStatus=1  inner join 
tblProjectInstallation D  with (nolock) on A.aStoreID=D.nStoreID and   D.nMyActiveStatus=1 inner join 
tblProjectStakeHolders E  with (nolock) on A.aStoreID=E.nStoreID  and   E.nMyActiveStatus=1 

END
Go
Create Procedure sproc_GetInforPOSReport          
 
as                          
BEGIN   


select dbo.fn_getProjectType(nProjectType) as [Type],tStoreNumber as	[StoreNo],tStoreAddressLine1 as [Location],--	[Questionnaire Status],
dbo.geDropDownTextByID(C.nStatus) as [POS Status],dDeliveryDate as	[Delivery Date],
dInstallDate as [InstallDate],dSupportDate as	[POS Support Date],dGoliveDate as	[Go-Live],tLeadTech as	[POS Tech],dbo.fn_getVendorName(D.nVendor) as	[Installer],tLeadTech as	[Install Tech],
(select tFranchiseName from tblFranchise with (nolock) where aFranchiseId=nFranchisee) as	[Franchisee],tITPM as	[IT PM]

from tblstore A with (nolock) inner join 
tblProject B  with (nolock) on A.aStoreID=B.nStoreID and nProjectActiveStatus=1  inner join 
tblProjectPOS C  with (nolock) on A.aStoreID=C.nStoreID and   nMyActiveStatus=1  inner join 
tblProjectInstallation D  with (nolock) on A.aStoreID=D.nStoreID and   D.nMyActiveStatus=1 inner join 
tblProjectStakeHolders E  with (nolock) on A.aStoreID=E.nStoreID  and   E.nMyActiveStatus=1 -- where nProjectType=5

END

GO
Create Procedure sproc_GetOraclePOSReport          
 
as                          
BEGIN 
select dbo.fn_getProjectType(nProjectType) as [Type],tStoreNumber as	[Details],tStoreAddressLine1 as	[Location],dbo.geDropDownTextByID(F.nConfiguration) as	[Audio Type],	
dbo.geDropDownTextByID(F.nStatus) as[Audio Status],dbo.geDropDownTextByID(C.nStatus) as	[POS Status],F.dDeliveryDate as	[Audio Delivery],C.dDeliveryDate as	[POS Delivery],D.dInstallDate as	[Install Date],
dConfigDate as [POS CALDate],dSupportDate as	[POS Support Date],dGoliveDate as	[Go-Live],tLeadTech as	[POS Tech],dbo.fn_getVendorName(D.nVendor) as	[Installer],tLeadTech as	[Install Tech],
(select tFranchiseName from tblFranchise with (nolock) where aFranchiseId=nFranchisee) as [Franchisee],tITPM as	[IT PM]

from tblstore A with (nolock) inner join 
tblProject B  with (nolock) on A.aStoreID=B.nStoreID and nProjectActiveStatus=1  inner join 
tblProjectPOS C  with (nolock) on A.aStoreID=C.nStoreID and   nMyActiveStatus=1  inner join 
tblProjectInstallation D  with (nolock) on A.aStoreID=D.nStoreID and   D.nMyActiveStatus=1 inner join 
tblProjectStakeHolders E  with (nolock) on A.aStoreID=E.nStoreID  and   E.nMyActiveStatus=1 inner join 
tblProjectAudio F  with (nolock) on A.aStoreID=F.nStoreID  and   F.nMyActiveStatus=1

End
---------------------

--select * from tblReport
--drop table tblReportWhereClause
CREATE TABLE [dbo].[tblReportWhereClause](
	[aReportWhereClauseID] [int] IDENTITY(1,1) NOT NULL,	
	[tRopertFilterName] nvarchar(1000) NULL,
	[tReportWherClause] nvarchar(max) NULL

	
PRIMARY KEY CLUSTERED 
(
	[aReportWhereClauseID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

insert into tblReportWhereClause (tRopertFilterName,tReportWherClause)
values('Number of Project','')
insert into tblReportWhereClause (tRopertFilterName,tReportWherClause)
values('Completed Project',' dGoliveDate <getdate() ')
insert into tblReportWhereClause (tRopertFilterName,tReportWherClause)
values('Project going live in 10 days',' dGoliveDate between  getdate() and  DATEADD(DAY,10, getdate()) ')
insert into tblReportWhereClause (tRopertFilterName,tReportWherClause)
values('Project going live in 30 days',' dGoliveDate between  getdate() and  DATEADD(DAY,30, getdate())')
insert into tblReportWhereClause (tRopertFilterName,tReportWherClause)
values('Project going live after 30 days ',' dGoliveDate >=DATEADD(DAY,30, getdate()) ')
insert into tblReportWhereClause (tRopertFilterName,tReportWherClause)
values('Intallation Revisit data change','  inner join tblProjectInstallation  with (nolock) on #ListOfAllProject.nProjectID=tblProjectInstallation.nProjectID
	 where dRevisitDate is not null  ')
insert into tblReportWhereClause (tRopertFilterName,tReportWherClause)
values('Number of project completed vs Not Completed','')



--update tblReportWhereClause set tReportWherClause='  inner join tblProjectInstallation  with (nolock) on tblProject.aProjectID=tblProjectInstallation.nProjectID
--	 where nBrandID=  @nBrandId and dRevisitDate is not null and nProjectActiveStatus=1 ' where aReportWhereClauseID=6
--drop table tblDashboard
CREATE TABLE [dbo].[tblDashboard](
	[aDashboardID] [int] IDENTITY(1,1) NOT NULL,
	[tName] varchar(1000) NULL,
	[nDashboardTileType] [int] NULL,
	[nchartType] [int] NULL,
	[nReportWhereClauseID] [int] NULL,
	[bCompareWith] [bit] NULL,	
	[nSize] [int] NULL,
	[tCompareWithText] varchar(1000) NULL,
	[tCompareWithWhereClause] varchar(max) NULL,
	[nCreatedBy] [int] NULL,
	[nUpdateBy] [int] NULL,
	[dtCreatedOn] [datetime] NULL,
	[dtUpdatedOn] [datetime] NULL,
	[bDeleted] [bit] NULL

	
PRIMARY KEY CLUSTERED 
(
	[aDashboardID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--ALTER TABLE tblDashboard ADD nSize int;
insert into tblDashboard(tName,nDashboardTileType,nReportWhereClauseID,nCreatedBy,dtCreatedOn,nSize)
values('Number of Project',0,1,2,GETDATE(),1)
go
insert into tblDashboard(tName,nDashboardTileType,nReportWhereClauseID,nCreatedBy,dtCreatedOn,nSize)
values('Completed Project',0,2,2,GETDATE(),1)
go
insert into tblDashboard(tName,nDashboardTileType,nReportWhereClauseID,tcompareWithText,tCompareWithWhereClause,nCreatedBy,dtCreatedOn,nSize)
values('Project going live in 10 days',1,3,'vs in last 10 days',N' dGoliveDate between  DATEADD(DAY,-10, getdate()) and  getdate()',2,GETDATE(),1)
go
insert into tblDashboard(tName,nDashboardTileType,nReportWhereClauseID,nCreatedBy,dtCreatedOn,nSize)
values('Project going live in next 30 days',0,4,2,GETDATE(),1)
go

insert into tblDashboard(tName,nDashboardTileType,nReportWhereClauseID,nchartType,nCreatedBy,dtCreatedOn,nSize)
values('Project going live after 30 days ',0,5,1,2,GETDATE(),1)

insert into tblDashboard(tName,nDashboardTileType,nReportWhereClauseID,nCreatedBy,dtCreatedOn,nSize)
values('Intallation Revisit data change',0,6,2,GETDATE(),1)
go
insert into tblDashboard(tName,nDashboardTileType,nReportWhereClauseID,nCreatedBy,dtCreatedOn,nSize)
values('Number of project completed vs Not completed',2,7,2,GETDATE(),2)
go

insert into tblDashboard(tName,nDashboardTileType,nReportWhereClauseID,nCreatedBy,dtCreatedOn,nSize)
values('Forecast',2,7,2,GETDATE(),2)
go
insert into tblDashboard(tName,nDashboardTileType,nReportWhereClauseID,nCreatedBy,dtCreatedOn,nSize)
values('Number of projects completed in month wise',2,7,2,GETDATE(),4)
go
--sproc_GetDashboardReports 6,null,2,'9/11/2022','9/11/2024'
select * from tblDashboard --update tblDashboard set nSize=4 where aDashboardID=8

alter  Procedure sproc_GetDashboardReports                        
@nBrandID int=0,
@tProjectTypes nvarchar(2000)=Null,
@nUserID int=0,
@dStarDate date=null,
@dEndDate date=null


as     
BEGIN
--{
--set @dStarDate =DATEADD(MONTH,-1, getdate())
--set @dEndDate =DATEADD(MONTH,1, getdate())

	IF OBJECT_ID('tempdb..#ListOfAllProject') IS NOT NULL
				DROP TABLE #ListOfAllProject

Create table  #ListOfAllProject (aID INT IDENTITY(1,1),nProjectID int,nProjectType int,dGoliveDate date,tProjectIDs nVARCHAR(max))
--INSERT INTO #ListOfAllProject (nProjectID,nProjectType,dGoliveDate) 
--select  aProjectID, nProjectType,dGoliveDate from  tblProject with (nolock) where dGoliveDate >@dStarDate and dGoliveDate <@dEndDate and nBrandID=@nBrandID

DECLARE @ListOfProjectHighlights TABLE(aID INT IDENTITY(1,1),DashboardID int,title VARCHAR(500),[count] int,compareWith int,
compareWithText VARCHAR(500), [type] int  ,chartType nvarchar(max) ,chartValuesTemp nVARCHAR(max), chartLabelsTemp nVARCHAR(max),tProjectIDs nVARCHAR(max),Size int)
   
INSERT INTO @ListOfProjectHighlights (DashboardID,title,[type],Size) 
select  aDashboardID, tName,nDashboardTileType,nSize from  tblDashboard with (nolock) --where  aDashboardID in(8,9,10,11,12,13,14)

declare @tSelect nvarchar(Max)
declare @tWhere nvarchar(Max)
declare @tCompareWithText nvarchar(Max)
declare @tCompareWithValue nvarchar(Max)
declare @tProjectIDs nvarchar(Max)
declare @tQuery nvarchar(Max)
declare @tCompareWithWhereClause nvarchar(max)
declare @chartValues nvarchar(max)
declare @chartLabels nvarchar(max)
declare @nCount int
Declare @TempDashboardID int

set @tWhere=' where dGoliveDate >'''+cast(@dStarDate as nvarchar(100))+''' and dGoliveDate <'''+cast(@dEndDate as nvarchar(100))+''' and nBrandID='+cast(@nBrandID as nvarchar(100))
if(@tProjectTypes is not null and @tProjectTypes <>'')
			set @tWhere=@tWhere+ ' and nProjectType in ('+@tProjectTypes+')'
set @tQuery='INSERT INTO #ListOfAllProject (nProjectID,nProjectType,dGoliveDate) 
select  aProjectID, nProjectType,dGoliveDate from  tblProject with (nolock)'
set @tQuery=@tQuery+@tWhere
print @tQuery
		EXEC sp_executesql @tQuery

 set @tQuery=''
	set @tWhere=''

	DECLARE db_cursor CURSOR FOR       
	SELECT DashboardID from @ListOfProjectHighlights      
      
	OPEN db_cursor        
	FETCH NEXT FROM db_cursor INTO @TempDashboardID  
      
	WHILE @@FETCH_STATUS = 0        
	BEGIN        
	--{
		set @nCount=0
		set @tProjectIDs=''
		set @tCompareWithWhereClause=''
		set @tCompareWithText=''
		set @tWhere=''
		set @chartValues =''
		set @chartLabels =''
		set @tSelect=' select @tProjectIDs=@tProjectIDs+ cast(#ListOfAllProject.nProjectID as nvarchar(50))+'','', @nTempCount=@nTempCount+1  from #ListOfAllProject with (nolock) '
		select @tWhere=tReportWherClause,@tCompareWithText=tCompareWithText,@tCompareWithWhereClause=tCompareWithWhereClause 
		from tblDashboard with (nolock)  inner join tblReportWhereClause  with (nolock) on tblDashboard.nReportWhereClauseID=tblReportWhereClause.aReportWhereClauseID 
		where aDashboardID=@TempDashboardID

		--select * from tblProject
		if(@tWhere<>'' AND CHARINDEX('where ', @tWhere) <= 0)
		begin
			set @tWhere=' Where '+@tWhere			
		end
		
		
		set @tQuery=@tSelect +@tWhere 

		print @tQuery

		EXEC sp_executesql @tQuery,N' @nTempCount INT OUTPUT, @tProjectIDs nvarchar(max) output', @nTempCount=@nCount OUTPUT ,@tProjectIDs=@tProjectIDs output
 
		 print @nCount
		 print @TempDashboardID
		update @ListOfProjectHighlights set [count]=@nCount,tProjectIDs=@tProjectIDs where DashboardID=@TempDashboardID
		set @nCount=0
		if((select [type] from @ListOfProjectHighlights where DashboardID=@TempDashboardID) =1)
		Begin
		--{
			set @nCount=0
			--print 'CompareWith true'
			set @tWhere=''
			if(@tCompareWithWhereClause<>'' AND CHARINDEX('where ', @tWhere) <= 0)
			begin
				set @tWhere=' Where '+@tCompareWithWhereClause
				
			end
			
			set @tQuery=@tSelect +@tWhere 
			print @tQuery
			print @TempDashboardID
			EXEC sp_executesql @tQuery,N' @nTempCount INT OUTPUT, @tProjectIDs nvarchar(max) output', @nTempCount=@nCount OUTPUT ,@tProjectIDs=@tProjectIDs output
			update @ListOfProjectHighlights set  compareWith=Cast(@nCount as nvarchar(50)),compareWithText=@tCompareWithText where DashboardID=@TempDashboardID
		--}		
		end
		if((select title from @ListOfProjectHighlights where DashboardID=@TempDashboardID)='Number of project completed vs Not Completed')
		begin
		set @nCount=0
			set @tWhere=' Where  dGoliveDate <getdate() '	
		
			set @tQuery=@tSelect +@tWhere 
			print @tQuery
		
			EXEC sp_executesql @tQuery,N' @nTempCount INT OUTPUT, @tProjectIDs nvarchar(max) output', @nTempCount=@nCount OUTPUT ,@tProjectIDs=@tProjectIDs output
		
			set @chartValues=cast(@nCount as nvarchar(100))
			set @nCount=0
			set @tWhere=' Where  dGoliveDate >getdate() '		
			
			set @tQuery=@tSelect +@tWhere 

			print @tQuery
			
			EXEC sp_executesql @tQuery,N'@nBrandId int, @nTempCount INT OUTPUT, @tProjectIDs nvarchar(max) output', @nBrandId, @nTempCount=@nCount OUTPUT ,@tProjectIDs=@tProjectIDs output
		
		
			set @chartValues=@chartValues+','+ cast(@nCount as nvarchar(100))
			print @TempDashboardID
			update @ListOfProjectHighlights set chartType='doughnut',chartValuesTemp=@chartValues,chartLabelsTemp='Completed project,Not Completed project ' where DashboardID=@TempDashboardID
		End
     if((select title from @ListOfProjectHighlights where DashboardID=@TempDashboardID)='Forecast')
		begin
		set @nCount=0
			

	
				IF OBJECT_ID('tempdb..#ListOfAllProjectType') IS NOT NULL
					DROP TABLE #ListOfAllProjectType
			Create table  #ListOfAllProjectType (aID INT IDENTITY(1,1),nProjectType int,[count] int)
			INSERT INTO #ListOfAllProjectType (nProjectType,[count])
			select   nProjectType,count(*) from  #ListOfAllProject where dGoLiveDate>GETDATE() group by nProjectType

			set @chartLabels=''
			set @chartValues=''
			select @chartLabels=@chartLabels+ dbo.fn_getProjectType(#ListOfAllProjectType.nProjectType)+',', @chartValues=@chartValues+ cast(#ListOfAllProjectType.[count] as nvarchar(50))+','
			from #ListOfAllProjectType
	

			print @TempDashboardID
			update @ListOfProjectHighlights set chartType='bar',chartValuesTemp=@chartValues,chartLabelsTemp=@chartLabels where DashboardID=@TempDashboardID
		End
		if((select title from @ListOfProjectHighlights where DashboardID=@TempDashboardID)='Number of projects completed in month wise')
		begin
		set @nCount=0
			
			
				IF OBJECT_ID('tempdb..#ListOfAllProjectByMonthTemp') IS NOT NULL
				DROP TABLE #ListOfAllProjectByMonthTemp

				Create table  #ListOfAllProjectByMonthTemp (aID INT IDENTITY(1,1),dMonth int,[count] int)
				INSERT INTO #ListOfAllProjectByMonthTemp values(1,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(2,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(3,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(4,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(5,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(6,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(7,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(8,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(9,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(10,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(11,0)
				INSERT INTO #ListOfAllProjectByMonthTemp values(12,0)
				

				IF OBJECT_ID('tempdb..#ListOfAllProjectByMonth') IS NOT NULL
					DROP TABLE #ListOfAllProjectByMonth
				Create table  #ListOfAllProjectByMonth (aID INT IDENTITY(1,1),dMonth int,[count] int)
				INSERT INTO #ListOfAllProjectByMonth (dMonth,[count])
				select   DATEPART(MONTH, dGoLiveDate),count(*) from  #ListOfAllProject where dGoLiveDate<getdate() group by DATEPART(MONTH, dGoLiveDate) 



				UPDATE   #ListOfAllProjectByMonthTemp
				SET #ListOfAllProjectByMonthTemp.[count] = #ListOfAllProjectByMonth.[count]

				FROM #ListOfAllProjectByMonthTemp JOIN #ListOfAllProjectByMonth 
				   ON #ListOfAllProjectByMonthTemp.dMonth = #ListOfAllProjectByMonth.dMonth

				
				set @chartLabels=''
				set @chartValues=''
				select @chartLabels=@chartLabels+ FORMAT(DATEFROMPARTS(1900, #ListOfAllProjectByMonthTemp.dMonth, 1), 'MMMM', 'en-US')+',', @chartValues=@chartValues+ cast(#ListOfAllProjectByMonthTemp.[count] as nvarchar(50))+','
				from #ListOfAllProjectByMonthTemp
			print @TempDashboardID
			update @ListOfProjectHighlights set chartType='bar',chartValuesTemp=@chartValues,chartLabelsTemp=@chartLabels where DashboardID=@TempDashboardID
		End
	FETCH NEXT FROM db_cursor INTO @TempDashboardID      
	--}
	END      
       
	CLOSE db_cursor        
	DEALLOCATE db_cursor      
      
select 4 as reportId,* from @ListOfProjectHighlights   
--}
End
Go
Go
--sproc_getDashboardData ''
Create proc sproc_getDashboardData
@tProjectIDs nVARCHAR(max)=null,
@tProjectTypes  nVARCHAR(max)=null
as
Begin

if(@tProjectIDs is  null OR @tProjectIDs ='' )	
Begin
select  'Store Number','Store Adreess', 'Project Type', 'Project Manager','Golive Date',
'Equip Vendor Name','Delivery Date','Installer Name','Installer Date'
return
End
declare @tQuery nVARCHAR(max)
set @tQuery=''
set @tQuery ='select   tStoreNumber as ''Store Number'',tStoreAddressLine1 as ''Store Adreess'', dbo.fn_getProjectType(nProjectType) as ''Project Type'',  
(select top 1 tITPM from tblProjectStakeHolders with (nolock) where nProjectID=aProjectID) as ''Project Manager'', dGoLiveDate as ''Golive Date'',
isnull(CASE  WHEN nProjectType = 5 THEN    
	(select  top 1 dbo.fn_getVendorName(nVendor) from tblProjectPOS with (nolock) where nProjectID=aProjectID)
	WHEN nProjectType = 6 THEN    
	(select  top 1 dbo.fn_getVendorName(nVendor) from tblProjectAudio with (nolock) where nProjectID=aProjectID)	
	WHEN nProjectType = 7 THEN    
	(select  top 1 dbo.fn_getVendorName(nVendor) from tblProjectExteriorMenus with (nolock) where nProjectID=aProjectID)	
	WHEN nProjectType = 8 THEN    
	(select  top 1 dbo.fn_getVendorName(nVendor) from tblProjectPaymentSystem with (nolock) where nProjectID=aProjectID)
	WHEN nProjectType = 10 THEN    
	(select  top 1 dbo.fn_getVendorName(nVendor) from tblProjectServerHandheld with (nolock) where nProjectID=aProjectID)	
	else ''''
	end,'''') as ''Equip Vendor Name'',
	CASE  WHEN nProjectType = 5 THEN    
	(select  top 1 dDeliveryDate from tblProjectPOS with (nolock) where nProjectID=aProjectID)
	WHEN nProjectType = 6 THEN    
	(select  top 1 dDeliveryDate from tblProjectAudio with (nolock) where nProjectID=aProjectID)	
	WHEN nProjectType = 7 THEN    
	(select  top 1 dDeliveryDate from tblProjectExteriorMenus with (nolock) where nProjectID=aProjectID)	
	WHEN nProjectType = 8 THEN    
	(select  top 1 dDeliveryDate from tblProjectPaymentSystem with (nolock) where nProjectID=aProjectID)
	WHEN nProjectType = 10 THEN    
	(select  top 1 dDeliveryDate from tblProjectServerHandheld with (nolock) where nProjectID=aProjectID)	
	else Null
	end as ''Delivery Date'',
	(select top 1 dbo.fn_getVendorName(nVendor) from tblProjectInstallation with (nolock) where nProjectID=aProjectID) as ''Installer Name'',
(select top 1 dInstallDate from tblProjectInstallation with (nolock) where nProjectID=aProjectID) as ''Installer Date''
from tblProject with (nolock)  
inner join   tblStore with (nolock) on tblProject.nStoreID=tblStore.aStoreID' 
	if(@tProjectIDs is not null and @tProjectIDs <>''  AND @tProjectTypes is not null and @tProjectTypes <>'')	
		set @tQuery=@tQuery+' where aProjectID in ('+@tProjectIDs+') and nProjectType in ('+@tProjectTypes+')' 
	else if(@tProjectIDs is not null and @tProjectIDs <>'' )	
		set @tQuery=@tQuery+' where aProjectID in ('+@tProjectIDs+')' 
	else if(@tProjectTypes is not null and @tProjectTypes <>'')	
		set @tQuery=@tQuery+' where  nProjectType in ('+@tProjectTypes+')' 

	
	print @tQuery
	EXEC sp_executesql @tQuery
END
GO
Create proc sproc_getReportData
@nReportId int,    
@tReportName NVARCHAR(500) output,
@pm1 NVARCHAR(MAX)='',
@pm2 NVARCHAR(MAX)='',
@pm3 NVARCHAR(MAX)='',
@pm4 NVARCHAR(MAX)='',
@pm5 NVARCHAR(MAX)=''
as  
BEGIN  
declare @tQuery NVARCHAR(max)  
select @tReportName = tName, @tQuery = tQuery from tblReport with(nolock) where aReportID = @nReportId  

print @tQuery
EXEC sp_executesql @tQuery, N'@pm1 NVARCHAR(max), @pm2 NVARCHAR(max),@pm3 NVARCHAR(max), @pm4 NVARCHAR(max), @pm5 NVARCHAR(max)',@pm1=@pm1, @pm2=@pm2,@pm3=@pm3,@pm4=@pm4,@pm5=@pm5


 END

 --update tblReport set tQuery=N'sproc_getDashboardData @pm1,@pm2' where aReportID = 4  

 GO

---------------Arbys
select * from tblBrand 
alter table tblBrand add nBrandType int
update tblBrand set nBrandType = 1 where tBrandName like '%Sonic%'
update tblBrand set nBrandType = 2 where tBrandName like '%Buffa%'
update tblBrand set nBrandType = 3 where tBrandName like '%Arby%'
update tblBrand set nBrandType = 4 where tBrandName like '%Dunkin%'
update tblBrand set nBrandType = 5 where tBrandName like '%Rusty%'
update tblBrand set nBrandType = 6 where tBrandName like '%Jimmy%'
update tblBrand set nEnabled = 1 where tBrandName like '%Arby%'

update tblDropdownModule set tModuleDisplayName = 'Drive Thru' where tModuleDisplayName like '%Drive%'

GO
select * from tblBrand
If(NOT EXISTS(select top 1 1 from tblDropdownModuleBrandRel where nBrandId = 3))
Insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) select 1,nModuleId from tblDropdownModuleBrandRel where nBrandId =2 

If(NOT EXISTS(select top 1 1 from tblDropdowns where nBrandId = 3))
Insert into tblDropdowns (tDropdownText,nUpdateBy,dtCreatedOn,dtUpdatedOn,bDeleted,nOrder,nFunction,nModuleId,nBrandId) select  tDropdownText,nUpdateBy,dtCreatedOn,dtUpdatedOn,bDeleted,nOrder,nFunction,nModuleId,1 from tblDropdowns where nBrandId = 2

select * from tblDropdowns where nBrandId = 3
select * from tblDropdownModuleBrandRel where nBrandId = 3

GO

ALTER procedure sproc_GetAllTechData        
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
 else if(@Brand like 'Sonic%' or @Brand like 'Arby''s')  
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
if(@Brand like 'Sonic%' or @Brand like 'Arby''s')   
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
--if(@tTechComp='Select top ')
--set @tTechComp='Select top 1 * ' 
--print @tTechComp    

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


  