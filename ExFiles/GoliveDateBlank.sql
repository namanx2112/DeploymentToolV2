  
ALTER procedure sproc_SearchStore                
@tText as VARCHAR(500),      
@nBrandID int=0      
AS                
BEGIN        
        
Select nStoreId, tStoreName,tStoreNumber,tProjectsInfo, @nBrandID nBrandId , case when(tAddress is null) then '' else tAddress end tAddress  
from(        
 select aStoreId nStoreId, tStoreName,tStoreNumber, tProjectsInfo , (tStoreAddressLine1 + ',' + tCity + ' ' + tStoreZip) tAddress  
 from tblStore with(nolock)  Join        
 (select nStoreId, STRING_AGG(CAST(aProjectId as varchar) + '_' + CAST(nProjectType as varchar) + '_' + 
 case when(dGoLiveDate is not null) then CAST(dGoLiveDate as varchar) else '' end
 , ', ') tProjectsInfo from tblProject with(nolock) where  nBrandID=@nBrandID and nProjectActiveStatus = 1        
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

  
CREATE  Procedure sproc_GetMySavedReports                          
@nBrandID int=0,  
@nUserID int=0   
as       
BEGIN  
	Select top 3 aReportId, tName from tblReport with(nolock)
End  