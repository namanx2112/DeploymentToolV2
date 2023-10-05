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
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Data.Entity;
using System.Security.Cryptography;

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
                    case ProjectType.ServerHandheld:
                        nProjectId = db.Database.SqlQuery<int>($"select nProjectId from dbo.fn_GetProjectIdForThisTechOrAnyProjectType({nStoreId},{(int)pType},1)").FirstOrDefault();
                        //query = db.tblProjects.Where(x => x.nStoreID == nStoreId && x.nProjectActiveStatus == 1 && (x.nProjectType == (int)pType || x.aProjectID > 0));
                        break;
                    default:
                        nProjectId = db.Database.SqlQuery<int>($"select nProjectId from dbo.fn_GetProjectIdForThisTechOrAnyProjectType({nStoreId},{(int)pType},1)").FirstOrDefault();
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
                        if (!String.IsNullOrEmpty(toAddress))
                            mailMessage.To.Add(toAddress.Trim());
                }
                if (request.tCC != null && request.tCC.Length > 0)
                {
                    foreach (string toAddress in request.tCC.Split(';'))
                        if (!String.IsNullOrEmpty(toAddress))
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

        public static void SendPasswordToEmail(string tName, string tUserName, string tEmail, string sPassword, bool forReset)
        {
            string tContent = "<div>Dear " + tName + ",<br/></div>";
            EMailRequest MailObj = new EMailRequest();
            if (forReset)
            {
                tContent += "<div>We have reset your password for accessing the Inspire Brands's Restaurant Technology Deployment tool!.<br/></div>";
                tContent += "<div>Please find the below credentials to Login:<br/></div>";
                tContent += $"<div>URL: {System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri} <br/></div>";
                tContent += "<div>User Name:" + tUserName + " <br/></div>";
                tContent += "<div>Password:  " + sPassword + " <br/><br/></div>";
                tContent += "<div>Thanks & Regards<br/></div>";
                tContent += "<div>Restaurant Technology Deployment Team</div>";
                MailObj.tSubject = "Password Reset:Inspire Brands";
            }
            else
            {
                tContent += "<div>We have created your user account for accessing the Inspire Brands's Restaurant Technology Deployment tool!.<br/></div>";
                tContent += "<div>Please find the below credentials to Login:<br/></div>";
                tContent += $"<div>URL: {System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri} <br/></div>";
                tContent += "<div>User Name:" + tUserName + " <br/></div>";
                tContent += "<div>Password:  " + sPassword + " <br/><br/></div>";
                tContent += "<div>Thanks & Regards<br/></div>";
                tContent += "<div>Restaurant Technology Deployment Team</div>";
                MailObj.tSubject = "Welcome to Inspire Brands";
            }

            MailObj.tTo = tEmail;
            MailObj.tContent = tContent;
            DeploymentTool.Misc.Utilities.SendMail(MailObj);
        }
        public static string EncodeString(string str)
        {
            string sResp = string.Empty;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
            sResp = System.Convert.ToBase64String(plainTextBytes);
            return sResp;
        }

        public static string DecodeString(string str)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        internal static string CreatePassword(string sUserName, int length, out string sPassword)
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string number = "1234567890";
            const string special = "!@#$%^&*";

            var middle = length / 2;
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                if (middle == length)
                {
                    res.Append(number[rnd.Next(number.Length)]);
                }
                else if (middle - 1 == length)
                {
                    res.Append(special[rnd.Next(special.Length)]);
                }
                else
                {
                    if (length % 2 == 0)
                    {
                        res.Append(lower[rnd.Next(lower.Length)]);
                    }
                    else
                    {
                        res.Append(upper[rnd.Next(upper.Length)]);
                    }
                }
            }
            sPassword = res.ToString();
            string sEncoded = Hashing.GenerateHash(sPassword);
            return sEncoded.ToString();
        }

        public static string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
    }
    public class Hashing
    {
        static string salt = "deplution";
        public static string GenerateHash(string password)
        {
            string _finalHash = string.Empty;
            try
            {
                byte[] keyByte = new ASCIIEncoding().GetBytes(salt);
                byte[] messageBytes = new ASCIIEncoding().GetBytes(password);
                byte[] hashmessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
                _finalHash = String.Concat(Array.ConvertAll(hashmessage, x => x.ToString("x2")));
            }
            catch (Exception ex)
            {
                _finalHash = string.Empty;
            }
            return _finalHash;
        }
    }

}