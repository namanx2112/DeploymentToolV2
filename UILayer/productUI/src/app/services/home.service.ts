import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { FieldType, HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { ValidatorFn, Validators } from "@angular/forms"

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  configUrl: any;
  constructor(private http: HttpClient, private authService: AuthService) {
    this.configUrl = authService.getConfigUrl();
   }

   public loginGet = () => {
    return this.http.get<string>(this.configUrl + "login/get?username=cuong&password=1", { headers: this.authService.getHttpHeaders() });
  }

  GetAllTabs():HomeTab[]{
    return [
      {
        tab_name: "Users",
        tab_header: "Users search",
        tab_type: TabType.Users,
        tab_unique_name: "",
        search_fields: [{
          field_name: "User Name",
          fieldUniqeName: "ABCD",
          defaultVal: "",
          readOnly: false,
          invalid: false,
          field_type: FieldType.text,
          field_placeholder: "Enter USer Name",
          validator: [],
          mandatory: false,
          hidden: false
        }],
        fields: [
          {
            field_name: "Name",
            fieldUniqeName: "Name",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.text,
            field_placeholder: "Enter Name",
            validator: [],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "Mobile",
            fieldUniqeName: "Mobile",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.text,
            field_placeholder: "Enter Name",
            validator: [],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "Email",
            fieldUniqeName: "Email",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.email,
            field_placeholder: "Enter mail",
            validator: [],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "User DOB",
            fieldUniqeName: "DOB",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.date,
            field_placeholder: "Enter DOB",
            validator: [],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "Created Time",
            fieldUniqeName: "Time",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.time,
            field_placeholder: "Enter Created Time",
            validator: [],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "User Time",
            fieldUniqeName: "uTime",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.dropdown,
            field_placeholder: "Enter User Roles",
            validator: [],
            mandatory: false,
            hidden: false,
            options: [
              {
                optionDisplayName: "Admin",
                optionIndex: 0,
                optionOrder: 1
              },
              {
                optionDisplayName: "User",
                optionIndex: 1,
                optionOrder: 2
              },
              {
                optionDisplayName: "Vendor",
                optionIndex: 2,
                optionOrder: 3
              }
            ]
          }
        ]
      },
      {
        tab_name: "Brands",
        tab_header: "Brands search",
        tab_type: TabType.Brands,
        tab_unique_name: "",
        search_fields: [
          {
          field_name: "Brands Name",
          fieldUniqeName: "tBrandName",
          defaultVal: "",
          readOnly: false,
          invalid: false,
          field_type: FieldType.text,
          field_placeholder: "Enter Brands Name",
          validator: [],
          mandatory: false,
          hidden: false
        }],
        fields: [
          {
            field_name: "Brand Id",
            fieldUniqeName: "aBrandId",
            defaultVal: "",
            readOnly: true,
            invalid: false,
            field_type: FieldType.number,
            field_placeholder: "Enter Brands Id",
            validator: [],
            mandatory: false,
            hidden: true
          },
          {
            field_name: "Brands Name",
            fieldUniqeName: "tBrandName",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.text,
            field_placeholder: "Enter Brands Name",
            validator: [Validators.required],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "Brands Description",
            fieldUniqeName: "tBrandDescription",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.text,
            field_placeholder: "Enter Brands Description",
            validator: [],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "Brands Website",
            fieldUniqeName: "tBrandWebsite",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.text,
            field_placeholder: "Enter Brands Website",
            validator: [],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "Brands Country",
            fieldUniqeName: "tBrandCountry",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.dropdown,
            field_placeholder: "Enter Brands Country",
            validator: [Validators.required],
            mandatory: false,
            hidden: false,
            options: [
              {
                optionDisplayName: "India",
                optionIndex: 0,
                optionOrder: 1
              },
              {
                optionDisplayName: "USA",
                optionIndex: 1,
                optionOrder: 2
              },
              {
                optionDisplayName: "UAE",
                optionIndex: 2,
                optionOrder: 3
              }
            ]
          },
          {
            field_name: "Brands Established",
            fieldUniqeName: "tBrandEstablished",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.date,
            field_placeholder: "Enter Brands Establishment date",
            validator: [],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "Brands Category",
            fieldUniqeName: "tBrandCategory",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.text,
            field_placeholder: "Enter Brands Category",
            validator: [Validators.required],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "Brands Contact",
            fieldUniqeName: "tBrandContact",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.text,
            field_placeholder: "Enter Brands Contact",
            validator: [],
            mandatory: false,
            hidden: false
          },
          {
            field_name: "Brands Attachment Id",
            fieldUniqeName: "nBrandLogoAttachmentID",
            defaultVal: "0",
            readOnly: false,
            invalid: false,
            field_type: FieldType.number,
            field_placeholder: "Enter Brands Attachment",
            validator: [],
            mandatory: false,
            hidden: true
          },
          {
            field_name: "Brands Created By",
            fieldUniqeName: "nCreatedBy",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.number,
            field_placeholder: "Enter Brands Created By",
            validator: [],
            mandatory: false,
            hidden: true
          },
          {
            field_name: "Brands Updated by",
            fieldUniqeName: "nUpdateBy",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.number,
            field_placeholder: "Enter Brands Updated by",
            validator: [],
            mandatory: false,
            hidden: true
          },
          {
            field_name: "Brands Created on",
            fieldUniqeName: "dtCreatedOn",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.date,
            field_placeholder: "Enter Brands Created on",
            validator: [],
            mandatory: false,
            hidden: true
          },
          {
            field_name: "Brands Updated on",
            fieldUniqeName: "dtUpdatedOn",
            defaultVal: "",
            readOnly: false,
            invalid: false,
            field_type: FieldType.date,
            field_placeholder: "Enter Brands Updated on",
            validator: [],
            mandatory: false,
            hidden: true
          }
        ]
      },
      {
        tab_name: "Vendors",
        tab_header: "Vendors search",
        tab_type: TabType.Vendor,
        tab_unique_name: "",
        search_fields: [{
          field_name: "Vendor Name",
          fieldUniqeName: "tVendorName",
          defaultVal: "",
          readOnly: false,
          invalid: false,
          field_type: FieldType.text,
          field_placeholder: "Enter Vendor Name",
          validator: [Validators.required],
          mandatory: false,
          hidden: false
        }],
        fields: [{
          field_name: "Vendor Name",
          fieldUniqeName: "tVendorName",
          defaultVal: "",
          readOnly: false,
          invalid: false,
          field_type: FieldType.text,
          field_placeholder: "Enter Vendor Name",
          validator: [Validators.required],
          mandatory: false,
          hidden: false
        }]
      },
      {
        tab_name: "Tech Components",
        tab_header: "Tech Components search",
        tab_type: TabType.TechComponent,
        tab_unique_name: "",
        search_fields: [{
          field_name: "Tech Comp Name",
          fieldUniqeName: "tTechCompName",
          defaultVal: "",
          readOnly: false,
          invalid: false,
          field_type: FieldType.text,
          field_placeholder: "Enter Tech Comp Name",
          validator: [Validators.required],
          mandatory: false,
          hidden: false
        }],
        fields: [{
          field_name: "Tech Comp Name",
          fieldUniqeName: "tTechCompName",
          defaultVal: "",
          readOnly: false,
          invalid: false,
          field_type: FieldType.text,
          field_placeholder: "Enter Tech Comp Name",
          validator: [Validators.required],
          mandatory: false,
          hidden: false
        }]
      },
      {
        tab_name: "Franchises",
        tab_header: "Franchises search",
        tab_type: TabType.Franchise,
        tab_unique_name: "",
        search_fields: [{
          field_name: "Frenchise Name",
          fieldUniqeName: "tFrenchiseName",
          defaultVal: "",
          readOnly: false,
          invalid: false,
          field_type: FieldType.text,
          field_placeholder: "Enter Frenchise Name",
          validator: [Validators.required],
          mandatory: false,
          hidden: false
        }],
        fields: [{
          field_name: "Frenchise Name",
          fieldUniqeName: "tFrenchiseName",
          defaultVal: "",
          readOnly: false,
          invalid: false,
          field_type: FieldType.text,
          field_placeholder: "Enter Frenchise Name",
          validator: [Validators.required],
          mandatory: false,
          hidden: false
        }]
      },
      {
        tab_name: "Stores",
        tab_header: "Stores search",
        tab_type: TabType.Store,
        tab_unique_name: "",
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
      },
      {
        tab_name: "Tech Component Tools",
        tab_header: "Tech Component Tools search",
        tab_type: TabType.TechComponentTools,
        tab_unique_name: "",
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
      },
      {
        tab_name: "Analytics",
        tab_header: "Analytics search",
        tab_type: TabType.Analytics,
        tab_unique_name: "",
        search_fields: [],
        fields: []
      }
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