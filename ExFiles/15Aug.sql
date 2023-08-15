create proc sproc_updateNotificationReadStatus
@nNotificationId int
as
BEGIN
	update tblUserNotification set nReadStatus = 1 where aUserNotificationID = @nNotificationId
END

GO

Alter procedure sproc_getAllNotificationSummary  
@nUserID int=0    
as    
BEGIN  
DECLARE @ListOfNotification TABLE(aID INT IDENTITY(1,1) ,nUserID int,ncount int,nNotificationTypeID int,tInstanceID nvarchar(max),nReadStatus int,dtCreatedOn date,tNotification nvarchar(max))  
   
INSERT INTO @ListOfNotification (nNotificationTypeID,ncount)  
select nNotificationTypeID, COUNT(*) as ncount from tblUserNotification with(nolock) where nUserID =@nUserID and nReadStatus=0  group by nNotificationTypeID   
  
  
 declare @tmpnNotificationTypeID int,@temptInstanceID nvarchar(max),@tmptNotification nvarchar(max),@tmpncount int,@tmpnUserID int,@tmpnReadStatus int,@tmpdtCreatedOn date  
    
 DECLARE db_cursor CURSOR FOR     
 SELECT nNotificationTypeID,ncount from @ListOfNotification    
    
 OPEN db_cursor      
 FETCH NEXT FROM db_cursor INTO @tmpnNotificationTypeID,@tmpncount    
    
 WHILE @@FETCH_STATUS = 0      
 BEGIN      
 set @temptInstanceID=''  
 set @tmptNotification=''  
 select @tmpnUserID=nUserID,@temptInstanceID=@temptInstanceID+Cast(nInstanceID as nvarchar(50))+',',@tmpnReadStatus=nReadStatus,@tmpdtCreatedOn=dtCreatedOn,@tmptNotification=cast(@tmpncount as nvarchar(50))+' ' + tName from tblUserNotification A with(nolock)  
 inner join tblNotificationType B with(nolock) on A.nNotificationTypeID=B.aNotificationTypeID  
 where nUserID =@nUserID and nNotificationTypeID=@tmpnNotificationTypeID
   
 update @ListOfNotification set nUserID=@tmpnUserID,tInstanceID=@temptInstanceID,nReadStatus=@tmpnReadStatus,dtCreatedOn=@tmpdtCreatedOn,tNotification=@tmptNotification where nNotificationTypeID=@tmpnNotificationTypeID  
  
  FETCH NEXT FROM db_cursor INTO @tmpnNotificationTypeID,@tmpncount  
 END     
     
     
 CLOSE db_cursor      
 DEALLOCATE db_cursor    
    
select aID,nUserID as nUserId,ncount,nNotificationTypeID as aNotificationId,nNotificationTypeID as nNotificationType,tInstanceID as relatedInstances,dtCreatedOn as dCreatedOn,tNotification,nReadStatus from @ListOfNotification  
  
  
END  

GO


sp_helptext sproc_getAllNotificationSummary
select * from tblProjectInstallation where nStoreId = 2
update tblProjectInstallation set nProjectId = 9 where aProjectInstallationID = 9

sp_depends tblUserNotification
select * from dbo.tblUserNotification

