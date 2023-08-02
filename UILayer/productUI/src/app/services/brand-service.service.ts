import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Validators } from '@angular/forms';
import { Fields, FieldType } from '../interfaces/home-tab';
import { BrandModel } from '../interfaces/models';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { Observable } from 'rxjs';
import { Dictionary } from '../interfaces/commons';

@Injectable({
  providedIn: 'root'
})
export class BrandServiceService {

  constructor(private http: HttpClient, private authService: AuthService, private commonService: CommonService) {
  }

  //Create
  //Update
  //Get
  //GetSearchFields
  //GetFields
  //GetTableVisibleColumns

  Create(request: any) {
    return this.http.post<BrandModel>(CommonService.ConfigUrl + "Brand/Create", request, { headers: this.authService.getHttpHeaders() });
  }

  Update(request: any) {
    return this.http.post<BrandModel>(CommonService.ConfigUrl + "Brand/Update", request, { headers: this.authService.getHttpHeaders() });
  }

  Delete(request: BrandModel) {
    return this.http.get<BrandModel>(CommonService.ConfigUrl + "Brand/Delete?id=" + request.aBrandId, { headers: this.authService.getHttpHeaders() });
  }

  Get(searchFields: Dictionary<string> | null) {
    // return new Observable<BrandModel[]>((obj) => {
    //   let items: BrandModel[] = [
    //     {
    //       aBrandId: 1,
    //       tBrandName: "Sonic",
    //       tBrandDomain: "www.sonic.com",
    //       tBrandAddressLine1: "",
    //       tBrandAddressLine2: "",
    //       nBrandCountry: 1,
    //       tBrandCity: "Test",
    //       nBrandState: 1,
    //       tBrandZipCode: "22",
    //       nBrandLogoAttachmentID: 1,
    //       nCreatedBy: 1,
    //       nUpdateBy: 1,
    //       dtCreatedOn: new Date(),
    //       dtUpdatedOn: new Date(),
    //       bDeleted: false,
    //       tIconURL:"https://upload.wikimedia.org/wikipedia/commons/f/ff/SONIC_New_Logo_2020.svg"
    //     }
    //   ];
    //   obj.next(items);
    // });
    return this.http.post<BrandModel[]>(CommonService.ConfigUrl + "Brand/Get", searchFields, { headers: this.authService.getHttpHeaders() });
  }

  GetTableVisibleColumns() {
    return [
      "tBrandName",
      "tBrandWebsite",
      "tBrandCountry",
      "tBrandContact"
    ];
  }

  GetSearchFields(): Fields[] {
    let fields = [{
      field_name: "Brands Name",
      fieldUniqeName: "tBrandName",
      defaultVal: "",
      readOnly: false,
      invalid: false,
      field_type: FieldType.text,
      field_placeholder: "Enter Brands Name",
      validator: [],
      mandatory: false,
      hidden: false,
      icon: "search"
    }]
    return fields;
  }

  GetFields(): Fields[] {
    let fields = [
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
        mandatory: true,
        hidden: false
      },
      {
        field_name: "Brands Domain",
        fieldUniqeName: "tBrandDomain",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Brands Domain",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Brands Address",
        fieldUniqeName: "tBrandAddressLine1",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Brands Address",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Brands Address2",
        fieldUniqeName: "tBrandAddressLine2",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Brands Address",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Brands City",
        fieldUniqeName: "tBrandCity",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Brands City",
        validator: [Validators.required],
        mandatory: false,
        hidden: false,
        options: this.commonService.GetDropdown("City")
      },
      {
        field_name: "Brands State",
        fieldUniqeName: "nBrandState",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Brands State",
        validator: [Validators.required],
        mandatory: false,
        hidden: false,
        options: this.commonService.GetDropdown("State")
      },
      {
        field_name: "Brands Country",
        fieldUniqeName: "nBrandCountry",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.dropdown,
        field_placeholder: "Enter Brands Country",
        validator: [Validators.required],
        mandatory: false,
        hidden: false,
        options: this.commonService.GetDropdown("Country")
      },
      {
        field_name: "Brands Zip",
        fieldUniqeName: "tBrandZipCode",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.text,
        field_placeholder: "Enter Brands Zip",
        validator: [],
        mandatory: false,
        hidden: false
      },
      {
        field_name: "Brands nBrandLogoAttachmentID",
        fieldUniqeName: "nBrandLogoAttachmentID",
        defaultVal: "",
        readOnly: false,
        invalid: false,
        field_type: FieldType.number,
        field_placeholder: "Enter Brands nBrandLogoAttachmentID",
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
    ];
    return fields;
  }

  // GetAllBrands(): BrandModel[] {
  //   let brands: BrandModel[] = [
  //     {
  //       aBrandId: 0,
  //       tBrandName: "Dunkin",
  //       tBrandDescription: "Dunkin",
  //       tBrandWebsite: "Dunkin",
  //       tBrandCountry: "Dunkin",
  //       tBrandEstablished: new Date(),
  //       tBrandCategory: "Dunkin",
  //       tBrandContact: "Dunkin",
  //       nBrandLogoAttachmentID: 1,
  //       nCreatedBy: 1,
  //       nUpdateBy: 1,
  //       dtCreatedOn: new Date(),
  //       dtUpdatedOn: new Date(),
  //       tIconURL: "https://s3-ap-southeast-1.amazonaws.com/assets.limetray.com/assets/user_images/logos/original/1602742091_DUNKINLogo.png"
  //     },
  //     {
  //       aBrandId: 0,
  //       tBrandName: "Baskin Robins",
  //       tBrandDescription: "Baskin Robins",
  //       tBrandWebsite: "Baskin Robins",
  //       tBrandCountry: "Baskin Robins",
  //       tBrandEstablished: new Date(),
  //       tBrandCategory: "Baskin Robins",
  //       tBrandContact: "Baskin Robins",
  //       nBrandLogoAttachmentID: 1,
  //       nCreatedBy: 1,
  //       nUpdateBy: 1,
  //       dtCreatedOn: new Date(),
  //       dtUpdatedOn: new Date(),
  //       tIconURL: "https://1000logos.net/wp-content/uploads/2016/10/Baskin-Robbins-Logo-2020.png"
  //     },
  //     {
  //       aBrandId: 0,
  //       tBrandName: "Buffalo Wild Wings",
  //       tBrandDescription: "Buffalo Wild Wings",
  //       tBrandWebsite: "Buffalo Wild Wings",
  //       tBrandCountry: "Buffalo Wild Wings",
  //       tBrandEstablished: new Date(),
  //       tBrandCategory: "Buffalo Wild Wings",
  //       tBrandContact: "Buffalo Wild Wings",
  //       nBrandLogoAttachmentID: 1,
  //       nCreatedBy: 1,
  //       nUpdateBy: 1,
  //       dtCreatedOn: new Date(),
  //       dtUpdatedOn: new Date(),
  //       tIconURL: "https://logos-world.net/wp-content/uploads/2022/01/Buffalo-Wild-Wings-Logo.png"
  //     },
  //     {
  //       aBrandId: 0,
  //       tBrandName: "Arbys",
  //       tBrandDescription: "Arbys",
  //       tBrandWebsite: "Arbys",
  //       tBrandCountry: "Arbys",
  //       tBrandEstablished: new Date(),
  //       tBrandCategory: "Arbys",
  //       tBrandContact: "Arbys",
  //       nBrandLogoAttachmentID: 1,
  //       nCreatedBy: 1,
  //       nUpdateBy: 1,
  //       dtCreatedOn: new Date(),
  //       dtUpdatedOn: new Date(),
  //       tIconURL: "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Arby%27s_logo.svg/2394px-Arby%27s_logo.svg.png"
  //     },
  //     {
  //       aBrandId: 0,
  //       tBrandName: "Jimmy Johns",
  //       tBrandDescription: "Jimmy Johns",
  //       tBrandWebsite: "Jimmy Johns",
  //       tBrandCountry: "Jimmy Johns",
  //       tBrandEstablished: new Date(),
  //       tBrandCategory: "Jimmy Johns",
  //       tBrandContact: "Jimmy Johns",
  //       nBrandLogoAttachmentID: 1,
  //       nCreatedBy: 1,
  //       nUpdateBy: 1,
  //       dtCreatedOn: new Date(),
  //       dtUpdatedOn: new Date(),
  //       tIconURL: "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Jimmy_Johns_logo.svg/1200px-Jimmy_Johns_logo.svg.png"
  //     },
  //     {
  //       aBrandId: 0,
  //       tBrandName: "Sonic",
  //       tBrandDescription: "Sonic",
  //       tBrandWebsite: "Sonic",
  //       tBrandCountry: "Sonic",
  //       tBrandEstablished: new Date(),
  //       tBrandCategory: "Sonic",
  //       tBrandContact: "Sonic",
  //       nBrandLogoAttachmentID: 1,
  //       nCreatedBy: 1,
  //       nUpdateBy: 1,
  //       dtCreatedOn: new Date(),
  //       dtUpdatedOn: new Date(),
  //       tIconURL: "https://upload.wikimedia.org/wikipedia/commons/f/ff/SONIC_New_Logo_2020.svg"
  //     }
  //   ];
  //   return brands;
  // }
}
