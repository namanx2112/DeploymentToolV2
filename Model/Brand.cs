using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class Brand
    {
        public int? aBrandId { get; set; }
        public string tBrandName { get; set; }
        public string tBrandDescription { get; set; }
        public string tBrandWebsite { get; set; }
        public string tBrandCountry { get; set; }
        public DateTime tBrandEstablished { get; set; }
        public string tBrandCategory { get; set; }
        public string tBrandContact { get; set; }
        public int? nBrandLogoAttachmentID { get; set; }
        public int? nCreatedBy { get; set; }
        public int? nUpdateBy { get; set; }
        public DateTime dtCreatedOn { get; set; }
        public DateTime dtUpdatedOn { get; set; }
        public bool bDeleted { get; set; }
        public int? nPageSize { get; set; }
        public int? nPageNumber { get; set; }
        public int? nTotalCount { get; set; }


    }

    public enum Brands
    {
        Sonic = 1, Buffalo, Arby, Dunkin, Rusty, Jimmy
    }

    public class BrandModel
    {
        public int aBrandId { get; set; }

        string _tBrandName { get; set; }
        public string tBrandName
        {
            get
            {
                return _tBrandName;
            }
            set
            {
                _tBrandName = value;
                this.SetBrandURL();
            }
        }
        public string tBrandDomain { get; set; }
        public string tBrandAddressLine1 { get; set; }
        public string tBrandAddressLine2 { get; set; }
        public string tBrandCity { get; set; }
        public Nullable<int> nBrandState { get; set; }
        public Nullable<int> nBrandCountry { get; set; }
        public string tBrandZipCode { get; set; }
        public Nullable<int> nBrandLogoAttachmentID { get; set; }
        public Nullable<int> nCreatedBy { get; set; }
        public Nullable<int> nUpdateBy { get; set; }
        public Nullable<System.DateTime> dtCreatedOn { get; set; }
        public Nullable<System.DateTime> dtUpdatedOn { get; set; }
        public Nullable<bool> bDeleted { get; set; }
        public byte[] BrandFile { get; set; }
        public string tIconURL { get; set; }
        public int nEnabled { get; set; }
        public Nullable<int> nBrandType { get; set; }

        public string tMyClass { get; set; }

        public void SetBrandURL()
        {
            string tURL = string.Empty;
            if (this.tBrandName.ToLower().IndexOf("sonic") > -1)
            {
                this.tMyClass = "sonic_theme";
                tURL = "./BrandIcons/sonic.png";
            }
            else if (this.tBrandName.ToLower().IndexOf("dunkin") > -1)
            {
                this.tMyClass = "dunkin_theme";
                tURL = "./BrandIcons/dunkin.png";
            }
            else if (this.tBrandName.ToLower().IndexOf("baskin") > -1)
            {
                this.tMyClass = "baskin_theme";
                tURL = "./BrandIcons/baskin.png";
            }
            else if (this.tBrandName.ToLower().IndexOf("buffa") > -1)
            {
                this.tMyClass = "buffalo_theme";
                tURL = "./BrandIcons/buffalo.png";
            }
            else if (this.tBrandName.ToLower().IndexOf("arby") > -1)
            {
                this.tMyClass = "arby_theme";
                tURL = "./BrandIcons/arby.png";
            }
            else if (this.tBrandName.ToLower().IndexOf("jim") > -1)
            {
                this.tMyClass = "jj_theme";
                tURL = "./BrandIcons/jj.png";
            }
            else
            {
                tURL = "./BrandIcons/sonic.png";
            }
            this.tIconURL = tURL;
        }
    }
}