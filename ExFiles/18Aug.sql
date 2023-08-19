alter table tblSupportTicket add tTicketStatus varchar(100), tFixComment NVARCHAR(MAX), dtUpdatedOn datetime, nUpdateBy int


Alter proc sproc_getSupportTicket
AS
BEGIN
	select aTicketId,nPriority,tContent,nFileSie,fBase64,tName as tCreatedBy,tblSupportTicket.nCreatedBy, tblSupportTicket.dtCreatedOn,tTicketStatus,tFixComment from  tblSupportTicket with(nolock) join tblUser with(nolock) on tblSupportTicket.nCreatedBy = aUserID
	order by aTicketId desc
END

