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

        public string tComponent { get; set; }

        
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

        public List<FileAttachment> FileAttachments { get; set; }
    }

    public class EMailRequest
    {
        public string tContent { get; set; }
        public string tTo { get; set; }
        public string tCC { get; set; }
        public string tSubject { get; set; }
        public string tFilePath { get; set; }
        public List<FileAttachment> FileAttachments { get; set; }

        public tblOutgoingEmail GettblOutgoingEmail()
        {
            return new tblOutgoingEmail()
            {
                tTo = this.tTo,
                tCC = this.tCC,
                tSubject = this.tSubject,
                tHTMLContent = this.tContent
            };
        }
        //public tblOutgoingEmailAttachment GettblOutgoingEmailAttachment(FileAttachment att)
        //{
        //    return new tblOutgoingEmailAttachment()
        //    {
        //        aOutgoingEmailAttachmentID = att.aOutgoingEmailAttachmentID,
        //        nOutgoingEmailID = att.nOutgoingEmailID,
        //        tFileName = att.tFileName,
        //        tContentID = att.tContentID,
        //        ifile = File.ReadAllBytes(att.tFilePath)

        //};
        //}

    }
    public class FileAttachment
    {
        public int aOutgoingEmailAttachmentID { get; set; }
        public int nOutgoingEmailID { get; set; }
        public string tFileName { get; set; }

        public string tFilePath { get; set; }
        public string tContentID { get; set; }
        public byte[] ifile { get; set; }
    }
}