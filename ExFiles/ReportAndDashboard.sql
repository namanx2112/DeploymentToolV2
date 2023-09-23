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