Alter procedure sproc_GetBrandByUser
@aUserID as int=0  
as            
BEGIN    
if(@aUserID>0)  
begin  
	select nBrandID from tblUserBrandRel with(nolock) where nUserID = @aUserID
end  
else  
begin  
	select nBrandID from tblUserBrandRel with(nolock) 
end  
END

GO

Alter table tblUserBrandRel ADD PRIMARY KEY (aUserBrandRelID)

GO

sproc_getDropdown ''