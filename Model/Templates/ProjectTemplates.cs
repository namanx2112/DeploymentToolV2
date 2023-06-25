using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model.Templates
{
    public class ProjectTemplates
    {
        public int nTemplateId { get; set; }
        public string tTemplateName { get; set; }
        public ProjectTemplateType nTemplateType { get; set; }
    }

    public enum ProjectTemplateType
    {
        Notification, QuoteRequest, PurchaseOrder
    }

    public class MergedQuoteRequest
    {
        public string tContent;
        public string tTo;
        public string tCC;
        public string tSubject;
    }
}