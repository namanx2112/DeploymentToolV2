using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Model
{

   



    public class Attachment
    {
        public int AttachmentId { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string FileType { get; set; }
        public string AttachmentType { get; set; }
        public string AttachmentComments { get; set; }
        public byte[] AttachmentBlob { get; set; }
        public string AttachmentUrl { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int nPageSize { get; set; }
        public int nPageNumber { get; set; }
        public int nTotalCount { get; set; }


    }
}