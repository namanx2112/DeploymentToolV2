import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { FieldType, HomeTab, TabInstanceType, TabType } from 'src/app/interfaces/home-tab';
import { ValidatorFn, Validators } from "@angular/forms"
import { BrandServiceService } from './brand-service.service';
import { VendorService } from './vendor.service';
import { TechComponenttService } from './tech-component.service';
import { UserService } from './user.service';
import { FranchiseService } from './frenchise.service';
import { PartsService } from './parts.service';
import { SupportContent } from '../interfaces/models';
import { CommonService } from './common.service';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient, private cacheService: CacheService, private brandService: BrandServiceService,
    private vendorService: VendorService, private techComponentService: TechComponenttService, private userSerice: UserService, private franchiseSerice: FranchiseService
    , private partsService: PartsService) {
   }

   public loginGet = () => {
    return this.http.get<string>(CommonService.ConfigUrl + "login/get?username=cuong&password=1", { headers: this.cacheService.getHttpHeaders() });
  }


  GetPartsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Items",
      tab_header: "Parts",
      tTableName: "tblParts",
      tab_type: TabType.VendorParts,
      tab_unique_name: "",
      search_fields: this.partsService.GetSearchFields(),
      fields: this.partsService.GetFields(),
      instanceType: instType,
      childTabs: [],
      my_service: this.partsService,
      needImport: true
    };
  }

  GetUsersTab(instType: TabInstanceType, needAccess: boolean):HomeTab{
    return {
      tab_name: "Users",
      tab_header: "Users",
      tTableName:"tblUser",
      tab_type: TabType.Users,
      tab_unique_name: "",
      search_fields: this.userSerice.GetSearchFields(),
      fields: this.userSerice.GetFields(needAccess),
      instanceType: instType,
      childTabs: [],
      my_service: this.userSerice,
      needImport: false
    };
  }
  
  GetBrandsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Brands",
      tab_header: "Brands",
      tTableName: "tblBrand",
      tab_type: TabType.Brands,
      tab_unique_name: "",
      search_fields: this.brandService.GetSearchFields(),
      fields: this.brandService.GetFields(),
      instanceType: instType,
      childTabs: [],
      my_service: this.brandService,
      needImport: false
    };
  }
  
  GetVendorsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Vendors",
      tab_header: "Vendors",
      tTableName: "tblVendor",
      tab_type: TabType.Vendor,
      tab_unique_name: "",
      search_fields: this.vendorService.GetSearchFields(),
      fields: this.vendorService.GetFields(),
      instanceType: instType,
      childTabs: [
this.GetUsersTab(TabInstanceType.Table, true),
this.GetPartsTab(TabInstanceType.Table)
      ],
      my_service: this.vendorService,
      needImport: false
    };
  }
  
  GetTechComponentsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Tech Components",
      tab_header: "Tech Components",
      tTableName: "tblTechComp",
      tab_type: TabType.TechComponent,
      tab_unique_name: "",
      search_fields: this.techComponentService.GetSearchFields(),
      fields: this.techComponentService.GetFields(),
      instanceType: instType,
      childTabs: [],
      my_service: this.techComponentService,
      needImport: false
    };
  }
  
  GetFrenchiseTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Franchises",
      tab_header: "Franchises",
      tTableName: "tblFranchise",
      tab_type: TabType.Franchise,
      tab_unique_name: "",
      search_fields: this.franchiseSerice.GetSearchFields(),
      fields: this.franchiseSerice.GetFields(),
      instanceType: instType,
      childTabs: [this.GetUsersTab(TabInstanceType.Table, true)],
      my_service: this.franchiseSerice,
      needImport: true
    };
  }
  
  GetStoreTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Stores",
      tab_header: "Stores",
      tTableName: "tblStore",
      tab_type: TabType.Store,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Stores Name",
        fieldUniqeName: "tStoreName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Stores Name",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "Stores Name",
        fieldUniqeName: "tStoreName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Stores Name",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      my_service: "",
      needImport: true
    };
  }
  
  GetTechToolsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Tech Component Tools",
      tab_header: "Tech Component Tools",
      tTableName: "tblTechTool",
      tab_type: TabType.TechComponentTools,
      tab_unique_name: "",
      instanceType: instType,
      childTabs: [],
      search_fields: [{
        field_name: "Tech Component Tools Name",
        fieldUniqeName: "tTechComponentToolsName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Tech Component Tools Name",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      fields: [{
        field_name: "Tech Component Tools Name",
        fieldUniqeName: "tTechComponentToolsName",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Tech Component Tools Name",
        validator: [Validators.required],
        mandatory: false,
        hidden: false
      }],
      my_service: "",
      needImport: false
    };
  }
  
  GetAnalyticsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Analytics",
      tab_header: "Analytics search",
      tTableName: "tblAnalytics",
      tab_type: TabType.Analytics,
      tab_unique_name: "",
      instanceType: instType,
      search_fields: [],
      fields: [],
      childTabs: [],
      my_service: "",
      needImport: false
    };
  }

  GetAllTabs():HomeTab[]{
    return [
      this.GetUsersTab(TabInstanceType.Single, false),
      this.GetBrandsTab(TabInstanceType.Single),
      this.GetVendorsTab(TabInstanceType.Single),
      this.GetTechComponentsTab(TabInstanceType.Single),
      this.GetFrenchiseTab(TabInstanceType.Single),
      this.GetStoreTab(TabInstanceType.Single),
      this.GetTechToolsTab(TabInstanceType.Single),
      this.GetAnalyticsTab(TabInstanceType.Single)
    ];
  }
}


//Table Json
// [
//   {
//     "id": "uniquefield3",
//     "sort": true,
//     "order": 1,
//     "width": 100
//   },
//   {
//     "id": "uniquefield7",
//     "sort": false,
//     "order": 2,
//     "width": 100
//   }
// ]

//Search Json
//[  {    "id": "uniquefield3",    "order": 1  },  {    "id": "uniquefield7",    "order": 2  }]

//Search record
// [
 
//   {
//   "id" : "uniquefield3" 
//   value : "9742010450"
//   Operator : "and"
//   }
  
//     ,
     
//   {
//   "id" : "uniquefield4" 
//   value : "9742010450"
//   Operator : "and"
//   }
  
  
    
//   ]

//Search fielld
// [
 
//   {
//   "id" : "uniquefield3" 
  
//     Order : 1
  
//   }
//   ,
//     {
//   "id" : "uniquefield7", 
    
//       Order : 2 
    
//   }
    
//   ]

//Fields
// [
 
//   {
//   "id" : "uniquefield3" 
//     Sort : True;
//     Order : 1
//     width : 100 
//   }
//   ,
//     {
//   "id" : "uniquefield7"  
//       Sort : False
//       Order : 2 
//       width : 100
//   }
    
//   ]