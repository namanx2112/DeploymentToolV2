using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
     //{
     //   "tab_name": "Store Information",
     //   "tab_unique_name": "store_storeInformation",
     //   "tab_type": "fields",
     //   "fields": [
     //       {
     //           "field_name": "field1",
     //           "fieldUniqeName": "store_storeInformation_field1",
     //           "field_type": "text",
     //           "regex_validation": "^[a-zA-Z0-9]+$",
     //           "mandatory": true,
     //           "conditional_mandatory": {
     //               "field_name": "field2",
     //               "value": "some_value"
    public class Tab
    {
        public string TabName { get; set; }
        public string tab_unique_name { get; set; }
        public string TabType { get; set; }
        public List<Field> Fields { get; set; }
    }

    public class Field
    {
        public string fieldName { get; set; }
        public object fieldValue { get; set; }
        public string fieldType { get; set; }
        public string fieldUniqeName { get; set; }
        public string regexValidation { get; set; }
        public string hint { get; set; }
        public bool isMandatory { get; set; }
        public bool isConditionalMandatory { get; set; }
        public ConditionalMandatory conditionalMandatory { get; set; }

        public List<ddOptions> lsDDOoption { get; set; }
    }
    public class ConditionalMandatory
    {
        public string value { get; set; }
        public string fieldName { get; set; }

    }

    public class ddOptions
    {
        public int index { get; set; }
        public int orderID { get; set; }
        public string dropDownText { get; set; }

    }

    
}