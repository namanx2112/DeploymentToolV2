
create table tblProjectServerHandheld(aServerHandheldId int  NOT NULL    IDENTITY    PRIMARY KEY,nProjectID int, nVendor int, dDeliveryDate date, nStatus int, nNumberOfTabletsPerStore int, dRevisitDate date, cCost decimal,nMyActiveStatus int ,nStoreId int
,dDateFor_nStatus date)

GO
insert into tblProjectTypeConfig values(10, 'tblProjectServerHandheld', 1)
insert into tblProjectTypeConfig values(0, 'tblProjectServerHandheld', 0)
insert into tblProjectTypeConfig values(1, 'tblProjectServerHandheld', 0)
insert into tblProjectTypeConfig values(2, 'tblProjectServerHandheld', 0)
insert into tblProjectTypeConfig values(3, 'tblProjectServerHandheld', 0)
insert into tblProjectTypeConfig values(4, 'tblProjectServerHandheld', 0)
insert into tblProjectTypeConfig values(9, 'tblProjectServerHandheld', 0)

GO

insert into tblDropdownModule(tModuleName,tModuleDisplayName,tModuleGroup,editable) values('SeverHandheldStatus', 'Status', 'Sever Handheld', 1)
select * from tblDropdownModule order by 1 desc
select * from tblBrand
insert into tblDropdownModuleBrandRel(nBrandId,nModuleId) values(2,38)

GO

select * from tblDropdowns


select * from tblProjectTypeConfig
sp_depends tblProjectTypeConfig
