using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Net.Mail;
using DeploymentTool.Model.Templates;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.DynamicData;
using System.Xml.Linq;
using System.Collections;

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
            catch (Exception ex)
            {

            }
        }

        public static void SetActiveProjectId(ProjectType pType, int? nStoreId, ModelParent objRef)
        {
            try
            {
                dtDBEntities db = new dtDBEntities();
                IQueryable<tblProject> query;
                int nProjectId = 0;
                switch (pType)
                {
                    case ProjectType.AudioInstallation:
                    case ProjectType.POSInstallation:
                    case ProjectType.MenuInstallation:
                    case ProjectType.PaymentTerminalInstallation:
                        nProjectId = db.Database.SqlQuery<int>($"select aProjectID from tblProject with(nolock) where nProjectActiveStatus=1 and nStoreID={nStoreId} and nProjectType={(int)pType}").FirstOrDefault();
                        //query = db.tblProjects.Where(x => x.nStoreID == nStoreId && x.nProjectActiveStatus == 1 && (x.nProjectType == (int)pType || x.aProjectID > 0));
                        break;
                    default:
                        nProjectId = db.Database.SqlQuery<int>($"select aProjectID from tblProject with(nolock) where nProjectActiveStatus=1 and nStoreID={nStoreId}").FirstOrDefault();
                        //query = db.tblProjects.Where(x => x.nStoreID == nStoreId && x.nProjectActiveStatus == 1);
                        break;
                }
                //tblProject tProject = query.FirstOrDefault();
                if (nProjectId > 0)
                {

                    PropertyInfo prop;
                    prop = objRef.GetType().GetProperty("nProjectID", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, nProjectId, null);
                    }
                    prop = objRef.GetType().GetProperty("nMyActiveStatus", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(objRef, (int)1, null);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static String WriteHTMLToPDF(String strBody, String fileName)
        {
            string strFilePath = "";
            try
            {
                string URL = HttpRuntime.AppDomainAppPath + "Attachments\\" + Guid.NewGuid();

                bool exists = System.IO.Directory.Exists(URL);

                if (!exists)
                    System.IO.Directory.CreateDirectory(URL);
                strFilePath = URL + "\\" + fileName;

                StringReader sr = new StringReader(strBody);
                Document pdfDoc = new Document();// PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                // using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(strFilePath, FileMode.Create));
                    pdfDoc.Open();

                    htmlparser.Style.LoadTagStyle(HtmlTags.DIV, "size", "10px");
                    htmlparser.Style.LoadTagStyle(HtmlTags.HEADERCELL, HtmlTags.BACKGROUNDCOLOR, "gray");
                    htmlparser.Style.LoadTagStyle(HtmlTags.HEADERCELL, HtmlTags.COLOR, "#fff");
                    htmlparser.Style.LoadTagStyle(HtmlTags.TABLE, HtmlTags.WIDTH, "100%");
                    htmlparser.Style.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDERWIDTH, "1");
                    htmlparser.Style.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDERCOLOR, "lightgray");
                    htmlparser.Style.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BACKGROUNDCOLOR, "#c2c2c2");
                    htmlparser.Parse(sr);
                    pdfDoc.Close();


                }


            }
            catch (Exception ex) { }
            return strFilePath;
        }

        public static void SendMail(EMailRequest request)
        {

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            using (SmtpClient smtpClient = new SmtpClient())
            {

                if (request.tTo != null && request.tTo.Length > 0)
                {
                    foreach (string toAddress in request.tTo.Split(';'))
                        mailMessage.To.Add(toAddress.Trim());
                }
                if (request.tCC != null && request.tCC.Length > 0)
                {
                    foreach (string toAddress in request.tCC.Split(';'))
                        mailMessage.CC.Add(toAddress.Trim());
                }
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