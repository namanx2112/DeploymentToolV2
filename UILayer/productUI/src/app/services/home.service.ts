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

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService, private brandService: BrandServiceService,
    private vendorService: VendorService, private techComponentService: TechComponenttService, private userSerice: UserService, private franchiseSerice: FranchiseService
    , private partsService: PartsService) {
    this.configUrl = authService.getConfigUrl();
   }

   public loginGet = () => {
    return this.http.get<string>(this.configUrl + "login/get?username=cuong&password=1", { headers: this.authService.getHttpHeaders() });
  }

  GetPartsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "PartsService",
      tab_header: "Parts search",
      tab_type: TabType.Users,
      tab_unique_name: "",
      search_fields: this.partsService.GetPartsSearchFields(),
      fields: this.partsService.GetPartsFields(),
      instanceType: instType,
      childTabs: []
    };
  }

  GetUsersTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Users",
      tab_header: "Users search",
      tab_type: TabType.Users,
      tab_unique_name: "",
      search_fields: this.userSerice.GetUserSearchFields(),
      fields: this.userSerice.GetUserFields(),
      instanceType: instType,
      childTabs: []
    };
  }
  
  GetBrandsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Brands",
      tab_header: "Brands search",
      tab_type: TabType.Brands,
      tab_unique_name: "",
      search_fields: this.brandService.GetBrandSearchFields(),
      fields: this.brandService.GetBrandFields(),
      instanceType: instType,
      childTabs: []
    };
  }
  
  GetVendorsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Vendors",
      tab_header: "Vendors search",
      tab_type: TabType.Vendor,
      tab_unique_name: "",
      search_fields: this.vendorService.GetVendorSearchFields(),
      fields: this.vendorService.GetVendorFields(),
      instanceType: instType,
      childTabs: [
this.GetUsersTab(TabInstanceType.Table),
this.GetPartsTab(TabInstanceType.Table)
      ]
    };
  }
  
  GetTechComponentsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Tech Components",
      tab_header: "Tech Components search",
      tab_type: TabType.TechComponent,
      tab_unique_name: "",
      search_fields: this.techComponentService.GetTechComponentSearchFields(),
      fields: this.techComponentService.GetTechComponentFields(),
      instanceType: instType,
      childTabs: []
    };
  }
  
  GetFrenchiseTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Franchises",
      tab_header: "Franchises search",
      tab_type: TabType.Franchise,
      tab_unique_name: "",
      search_fields: this.franchiseSerice.GetFranchiseSearchFields(),
      fields: this.franchiseSerice.GetFranchiseFields(),
      instanceType: instType,
      childTabs: []
    };
  }
  
  GetStoreTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Stores",
      tab_header: "Stores search",
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
      }]
    };
  }
  
  GetTechToolsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Tech Component Tools",
      tab_header: "Tech Component Tools search",
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
      }]
    };
  }
  
  GetAnalyticsTab(instType: TabInstanceType):HomeTab{
    return {
      tab_name: "Analytics",
      tab_header: "Analytics search",
      tab_type: TabType.Analytics,
      tab_unique_name: "",
      instanceType: instType,
      search_fields: [],
      fields: [],
      childTabs: []
    };
  }

  GetAllTabs():HomeTab[]{
    return [
      this.GetUsersTab(TabInstanceType.Single),
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