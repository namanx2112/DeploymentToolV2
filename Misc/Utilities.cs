using DeploymentTool.Model;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Mail;
using DeploymentTool.Model.Templates;

namespace DeploymentTool.Misc
{
    public class Utilities
    {
        public static void SetHousekeepingFields(bool create, HttpContext context, ModelParent objRef)
        {
            try
            {
                var securityContext = (User)context.Items["SecurityContext"];
                Nullable<long> lUserId = securityContext.nUserID;
                PropertyInfo prop;
                if (create)
                {
                    prop = objRef.GetType().GetProperty("nCreatedBy", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, (int)lUserId, null);
                    }

                    prop = objRef.GetType().GetProperty("dtCreatedOn", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, (DateTime)DateTime.Now, null);
                    }
                }
                else
                {
                    prop = objRef.GetType().GetProperty("nUpdateBy", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, (int)lUserId, null);
                    }

                    prop = objRef.GetType().GetProperty("dtUpdatedOn", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, (DateTime)DateTime.Now, null);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public static String WriteHTMLToPDF(String strBody, String fileName)
        {
            string URL = HttpRuntime.AppDomainAppPath+ "Attachments\\" + Guid.NewGuid();

            bool exists = System.IO.Directory.Exists(URL);

            if (!exists)
                System.IO.Directory.CreateDirectory(URL);
            string strFilePath = URL + "\\"+fileName;
            //// Create a new PDF writer
            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(strFilePath, FileMode.Create));

            //// Open the PDF document
            //document.Open();
            //request.tContent = "Please provide a quote for this store based on the information below. Please be sure to reply to all so our entire team receives it. Thanks!\r\nPOS\r\nnStatus\r\nVendor 4\r\nDelivery Date\r\nConfig Date\r\nSupport Date\r\nStore Information\r\nStore Name 1223\r\nAddress1 1108 W. JACKSON\r\nStore City OZARK\r\nStore Name 1223\r\nAddress1 1108 W. JACKSON\r\nStore City OZARK\r\nStore Zip\r\nGeneral Contractor Phone\r\nGeneral ContractorEmail\r\nStore Name 1223\r\nAddress1 1108 W. JACKSON\r\nStore Address2\r\nStore City OZARK\r\nStore Configuration\r\nAudio\r\nVendor 2\r\nConfiguration\r\nCost 452.00\r\nVendor 2\r\nStatus 1\r\nConfiguration\r\nDelivery Date\r\nVendor 2\r\nStatus 1\r\nConfiguration\r\nDelivery Date";
            //// Add content to the PDF document
            //document.Add(new Paragraph(request.tContent));
            //document.Close();
            StringReader sr = new StringReader(strBody);
            Document pdfDoc = new Document();// PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            // using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(strFilePath, FileMode.Create));
                pdfDoc.Open();

                htmlparser.Parse(sr);
                pdfDoc.Close();

                //byte[] bytes = memoryStream.ToArray();
                //memoryStream.Close();
            }
            return strFilePath;
        }

        public static void SendMail(EMailRequest request)
        {
           
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            using (SmtpClient smtpClient = new SmtpClient())
            {
                // Create a MailMessage object

                mailMessage.To.Add(request.tTo);
                if (request.tCC !=null && request.tCC.Length > 0)
                    mailMessage.CC.Add(request.tCC);
                mailMessage.Subject = request.tSubject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = request.tContent;

                //if(request.FileAttachments!=null)
                //foreach (var RequestFile in request.FileAttachments)
                //{
                //    System.Net.Mail.Attachment objAtt = new System.Net.Mail.Attachment(RequestFile.tFileName);
                //    objAtt.ContentId = RequestFile.tContentID;
                //    mailMessage.Attachments.Add(objAtt);
                //}
                if (!string.IsNullOrEmpty(request.tFilePath))
                {
                    System.Net.Mail.Attachment objAtt = new System.Net.Mail.Attachment(request.tFilePath);
                    //objAtt.ContentId = RequestFile.tContentID;
                    mailMessage.Attachments.Add(objAtt);
                }
                // Send the email
                //SendMail();
                smtpClient.Send(mailMessage);



            }
           
            
        }


    }
}