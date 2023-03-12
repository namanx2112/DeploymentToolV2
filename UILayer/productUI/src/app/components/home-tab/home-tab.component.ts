import { Component } from '@angular/core';
import { FieldType, HomeTab, TabType } from 'src/app/interfaces/home-tab';
import { ValidatorFn, Validators } from "@angular/forms"

@Component({
  selector: 'app-home-tab',
  templateUrl: './home-tab.component.html',
  styleUrls: ['./home-tab.component.css']
})
export class HomeTabComponent {

  tabsArray: HomeTab[];
  constructor(){
      this.tabsArray = [
        {
          tab_name: "Users",
          tab_header: "Users search",
          tab_type: TabType.Users,
          tab_unique_name: "",
          search_fields: [{
            field_name: "User Name",
            fieldUniqeName: "ABCD",
            icon: "search",
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
          search_fields: [{
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
              hidden: false
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
              defaultVal: "",
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
          tab_name: "Franchises",
          tab_header: "Franchises search",
          tab_type: TabType.Franchise,
          tab_unique_name: "",
          search_fields: [],
          fields: []
        },
        {
          tab_name: "Stores",
          tab_header: "Stores search",
          tab_type: TabType.Store,
          tab_unique_name: "",
          search_fields: [],
          fields: []
        },
        {
          tab_name: "Tech Components",
          tab_header: "Tech Components search",
          tab_type: TabType.TechComponent,
          tab_unique_name: "",
          search_fields: [],
          fields: []
        },
        {
          tab_name: "Vendors",
          tab_header: "Vendors search",
          tab_type: TabType.Vendor,
          tab_unique_name: "",
          search_fields: [],
          fields: []
        },
        {
          tab_name: "Tech Component Tools",
          tab_header: "Tech Component Tools search",
          tab_type: TabType.TechComponentTools,
          tab_unique_name: "",
          search_fields: [],
          fields: []
        },
        {
          tab_name: "Analytics",
          tab_header: "Analytics search",
          tab_type: TabType.Analytics,
          tab_unique_name: "",
          search_fields: [],
          fields: []
        }
      ]
  }
}
