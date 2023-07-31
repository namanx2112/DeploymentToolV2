select * from tblUser
update tbluser set tPassword = 'YWRtaW4=', tEmail = 'admin@test.com', tUserName='admin' where aUserID = 2
Alter procedure sproc_ChangePassword
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
Alter  PROC sproc_UserLogin             
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
--SET @nUserID = -1         
--set @nRoleType = -1        
--set @tEmail= ''        
--set @tName = ''        
--if(LOWER(@tUserName) = 'admin' AND LOWER(@tPassword) = 'admin')  
-- SELECT  @tEmail = 'test@test.com' ,@tName= 'admin', @nUserID = 2,@nRoleType = 'admin'            
--else  
-- SELECT  @tEmail = '' ,@tName= '', @nUserID = -1,@nRoleType = ''            
   select @tName = tName, @tEmail = tEmail, @nRoleType = nRole, @nUserID = aUserID from tblUser with(nolock) where tUserName = @tUserName and tPassword = @tPassword
END 

