using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{
    public class QuoteRequestTemplate
    {
        public int aQuoteRequestTemplateId { get; set; }
        public string tTemplateName { get; set; }
        public List<QuoteRequestTechComp> quoteRequestTechComps { get; set; }
        public int nBrandId { get; set; }
        public int nCreatedBy { get; set; }
        public int nUpdateBy { get; set; }
        public int nUserID { get; set; }
        public DateTime dtCreatedOn { get; set; }
        public DateTime dtUpdatedOn { get; set; }
        public bool bDeleted { get; set; }
    }

    public class QuoteRequestTechComp
    {
        public int nQuoteRequestTemplateId { get; set; }

        public int aQuoteRequestTechCompId { get; set; }
        
        public string tTechCompName { get; set; }
        public string tTableName { get; set; }
        public List<QuoteRequestTechCompField> fields { get; set; }
        public int nCreatedBy { get; set; }
        public int nUpdateBy { get; set; }
        public int nUserID { get; set; }
        public DateTime dtCreatedOn { get; set; }
        public DateTime dtUpdatedOn { get; set; }
        public bool bDeleted { get; set; }
    }

    public class QuoteRequestTechCompField
    {
        public int nQuoteRequestTemplateId { get; set; }

        public int nQuoteRequestTechCompId { get; set; }
        public string tTechCompField { get; set; }
        public string tTechCompFieldName { get; set; }
    }

    public class QuoteRequestTemplateTemp
    {
        public int aQuoteRequestTemplateId { get; set; }
        public string tTemplateName { get; set; }
        public int nBrandId { get; set; }
        public int nCreatedBy { get; set; }
        public int nUpdateBy { get; set; }
        
    }
    public class QuoteRequestTechCompTemp
    {
        public int nQuoteRequestTemplateId { get; set; }
        public int aQuoteRequestTechCompId { get; set; }
        public string tTechCompName { get; set; }
        public string tTableName { get; set; }       
        public int nCreatedBy { get; set; }
        public int nUpdateBy { get; set; }

    }

}