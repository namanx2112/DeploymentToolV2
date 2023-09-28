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