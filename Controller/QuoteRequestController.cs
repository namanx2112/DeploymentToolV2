using DeploymentTool.Model;
using DeploymentTool.Model.Templates;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Mail;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Web;
using iTextSharp.text.html.simpleparser;
using System.Runtime.Remoting.Messaging;

namespace DeploymentTool.Controller
{
    public class QuoteRequestController : ApiController
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpGet]
        [ActionName("GetAllTemplate")]
        public HttpResponseMessage GetAllTemplate(int nBrandId)
        {
            Dictionary<int, string> lstQuoteRequest = new Dictionary<int, string>();
            SqlParameter tModuleNameParam = new SqlParameter("@nBrandId", nBrandId);
            List<QuoteRequestTemplateTemp> items = db.Database.SqlQuery<QuoteRequestTemplateTemp>("exec sproc_GetAllQuoteRequestTemplate @nBrandId", tModuleNameParam).ToList();
            foreach (var RequestTechComps in items)
            {

                lstQuoteRequest.Add(RequestTechComps.aQuoteRequestTemplateId, RequestTechComps.tTemplateName);
                //lstQuoteRequest.Add(2, "Networking");
            }
            //lstQuoteRequest.Add(1, "Audio");
           // lstQuoteRequest.Add(2, "Networking");
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<Dictionary<int, string>>(lstQuoteRequest, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetTemplate(int nTemplateId)
        {
            try
            {
                QuoteRequestTemplate templateObj = new QuoteRequestTemplate();
                if (nTemplateId == 0)
                {
                    templateObj.aQuoteRequestTemplateId = nTemplateId;
                    templateObj.tTemplateName = "New";
                    templateObj.nBrandId = 1;
                    templateObj.quoteRequestTechComps = new List<QuoteRequestTechComp>()
                    {
                        new QuoteRequestTechComp()
                        {
                            tTechCompName = "POS",
                            tTableName="tblProjectPOS",
                            fields = new List<QuoteRequestTechCompField>()
                            {
                                new QuoteRequestTechCompField()
                                {
                                    nQuoteRequestTemplateId= nTemplateId,
                                    tTechCompField = "nStatus",
                                    tTechCompFieldName = "nStatus"
                                }
                            }
                        }
                    };
                }
                else
                {
                    /*
                    SqlParameter tModuleNameParam = new SqlParameter("@aQuoteRequestTemplateId", nTemplateId);
                    List<QuoteRequestTemplateTemp> items = db.Database.SqlQuery<QuoteRequestTemplateTemp>("exec sproc_GetQuoteRequestTemplate @aQuoteRequestTemplateId", tModuleNameParam).ToList();

                    templateObj.aQuoteRequestTemplateId = items[0].aQuoteRequestTemplateId;
                    templateObj.tTemplateName = items[0].tTemplateName;
                    templateObj.nBrandId = items[0].nBrandId;
                    templateObj.nCreatedBy = items[0].nCreatedBy;
                    templateObj.nUpdateBy = items[0].nUpdateBy;
                    SqlParameter tModuleNameP = new SqlParameter("@nQuoteRequestTemplateId", nTemplateId);

                    List<QuoteRequestTechCompTemp> item = db.Database.SqlQuery<QuoteRequestTechCompTemp>("exec sproc_GetQuoteRequestTechComp @nQuoteRequestTemplateId", tModuleNameP).ToList();
                    List<QuoteRequestTechComp> quoteRequestTechComps = new List<QuoteRequestTechComp>();
                    foreach (var RequestTechComp in item)
                    {
                        SqlParameter tModuleparm = new SqlParameter("@nQuoteRequestTechCompId", RequestTechComp.aQuoteRequestTechCompId);

                        List<QuoteRequestTechCompField> itemFields = db.Database.SqlQuery<QuoteRequestTechCompField>("exec sproc_GetQuoteRequestTechCompField @nQuoteRequestTechCompId", tModuleparm).ToList();

                        quoteRequestTechComps.Add(new QuoteRequestTechComp()
                        {

                            nQuoteRequestTemplateId = RequestTechComp.nQuoteRequestTemplateId,
                            aQuoteRequestTechCompId = RequestTechComp.aQuoteRequestTechCompId,
                            tTechCompName = RequestTechComp.tTechCompName,
                            tTableName = RequestTechComp.tTableName,
                            nCreatedBy = RequestTechComp.nCreatedBy,
                            nUpdateBy = RequestTechComp.nUpdateBy,
                            fields = itemFields

                        });

                    }
                    templateObj.quoteRequestTechComps = quoteRequestTechComps;
                    */
                    //PurchaseOrderTeamplate templatePOobj = new PurchaseOrderTeamplate();
                    //SqlParameter tModuleNameParam = new SqlParameter("@aPurchaseOrderTemplateID", nTemplateId);
                    //List<tblPurchaseOrderTemplateTemp> items = db.Database.SqlQuery<tblPurchaseOrderTemplateTemp>("exec sproc_GetPurchaseOrderTemplate @aPurchaseOrderTemplateID", tModuleNameParam).ToList();

                    //templatePOobj.aPurchaseOrderTemplateID = items[0].aPurchaseOrderTemplateID;
                    //templatePOobj.tTemplateName = items[0].tTemplateName;
                    //templatePOobj.nBrandId = items[0].nBrandId;
                    //templatePOobj.nCreatedBy = items[0].nCreatedBy;
                    //templatePOobj.nUpdateBy = items[0].nUpdateBy;
                    //SqlParameter tModuleparmParts = new SqlParameter("@aPurchaseOrderTemplateID", nTemplateId);

                    //List<PurchaseOrderPartDetails> itemParts = db.Database.SqlQuery<PurchaseOrderPartDetails>("exec sproc_GetPurchaseOrderPartsDetails @aPurchaseOrderTemplateID", tModuleparmParts).ToList();
                    //List<PurchaseOrderParts> obj=new List<PurchaseOrderParts>();
                    //foreach (var RequestTechComp in itemParts)
                    //{

                    //    obj.Add(new PurchaseOrderParts()
                    //    {
                    //        aPurchaseOrderTemplatePartsID = RequestTechComp.nPurchaseOrderPartDetailsID,
                    //        aPurchaseOrderTemplateID = RequestTechComp.aPurchaseOrderTemplateID,
                    //        nPartID = RequestTechComp.nPartID,
                    //        nConfigProjectFieldID = RequestTechComp.nQuantity,
                    //        tPartDesc = RequestTechComp.tPartDesc,
                    //        tPartNumber = RequestTechComp.tPartNumber,
                    //        cPrice = RequestTechComp.cPrice,
                    //        nQuantity = RequestTechComp.nQuantity,
                    //        cTotal = RequestTechComp.cTotal


                    //    });

                    //}
                    //templatePOobj.purchaseOrderParts = obj;
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<QuoteRequestTemplate>(templateObj, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage CreateUpdateTemplate(QuoteRequestTemplate quoteRequest)
        {
            try
            {
                if (quoteRequest.aQuoteRequestTemplateId > 0)
                {
                    var nTechComp = db.Database.ExecuteSqlCommand("delete from tblQuoteRequestTechComp where nQuoteRequestTemplateId =@nQuoteRequestTemplateId ", new SqlParameter("@nQuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId));
                    var nTechCompFields = db.Database.ExecuteSqlCommand("delete from tblQuoteRequestTechCompFields where nQuoteRequestTemplateId =@nQuoteRequestTemplateId ", new SqlParameter("@nQuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId));

                }
                List<SqlParameter> tPramList = new List<SqlParameter>();
                tPramList.Add(new SqlParameter("@tTemplateName", quoteRequest.tTemplateName));
                tPramList.Add(new SqlParameter("@nBrandId", quoteRequest.nBrandId));
                tPramList.Add(new SqlParameter("@nCreatedBy", quoteRequest.nCreatedBy));
                tPramList.Add(new SqlParameter("@nUpdateBy", quoteRequest.nUpdateBy));
                tPramList.Add(new SqlParameter("@QuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId) {Direction = ParameterDirection.InputOutput});                
                db.Database.ExecuteSqlCommand("exec sproc_CreateAndUpdateQouteRequestMain @tTemplateName,@nBrandId,@nCreatedBy,@nUpdateBy,@QuoteRequestTemplateId out", tPramList[0], tPramList[1], tPramList[2], tPramList[3], tPramList[4]);
                quoteRequest.aQuoteRequestTemplateId = ((SqlParameter)tPramList[4]).Value == DBNull.Value ? 0 : Convert.ToInt32(((SqlParameter)tPramList[4]).Value);

                foreach (var RequestTechComps in quoteRequest.quoteRequestTechComps)
                {
                    List<SqlParameter> tPramListTechComps = new List<SqlParameter>();
                    tPramListTechComps.Add(new SqlParameter("@nQuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId));
                    tPramListTechComps.Add(new SqlParameter("@tTechCompName", RequestTechComps.tTechCompName));
                    tPramListTechComps.Add(new SqlParameter("@tTableName", RequestTechComps.tTableName==null? "": RequestTechComps.tTableName));                    
                    tPramListTechComps.Add(new SqlParameter("@nCreatedBy", RequestTechComps.nCreatedBy));
                    tPramListTechComps.Add(new SqlParameter("@nUpdateBy", RequestTechComps.nUpdateBy));
                    tPramListTechComps.Add(new SqlParameter("@aQuoteRequestTechCompId", RequestTechComps.aQuoteRequestTechCompId) { Direction = ParameterDirection.InputOutput });

                    db.Database.ExecuteSqlCommand("exec sproc_CreateAndUpdateQouteRequestTechComp @nQuoteRequestTemplateId,@tTechCompName,@tTableName,@nCreatedBy,@nUpdateBy,@aQuoteRequestTechCompId out", tPramListTechComps[0], tPramListTechComps[1], tPramListTechComps[2], tPramListTechComps[3], tPramListTechComps[4], tPramListTechComps[5]);
                    RequestTechComps.aQuoteRequestTechCompId = ((SqlParameter)tPramListTechComps[5]).Value == DBNull.Value ? 0 : Convert.ToInt32(((SqlParameter)tPramListTechComps[5]).Value);
                    //var noOfRowUpdated = db.Database.ExecuteSqlCommand("delete from tblQuoteRequestTechCompFields where nQuoteRequestTemplateId =nQuoteRequestTechCompId and nQuoteRequestTechCompId=@nQuoteRequestTechCompId", new SqlParameter("@nQuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId), new SqlParameter("@nQuoteRequestTechCompId", RequestTechComps.aQuoteRequestTechCompId));

                    foreach (var RequestTechCompsField in RequestTechComps.fields)
                    {
                        List<SqlParameter> tPramListTechCompsFields = new List<SqlParameter>();
                        tPramListTechCompsFields.Add(new SqlParameter("@nQuoteRequestTemplateId", quoteRequest.aQuoteRequestTemplateId));
                        tPramListTechCompsFields.Add(new SqlParameter("@nQuoteRequestTechCompId", RequestTechComps.aQuoteRequestTechCompId));
                        tPramListTechCompsFields.Add(new SqlParameter("@tTechCompField", RequestTechCompsField.tTechCompField));
                        tPramListTechCompsFields.Add(new SqlParameter("@tTechCompName", RequestTechCompsField.tTechCompFieldName));

                        tPramListTechCompsFields.Add(new SqlParameter("@aQuoteRequestTechCompFieldId", SqlDbType.Int) { Direction = ParameterDirection.Output });
                        db.Database.ExecuteSqlCommand("exec @aQuoteRequestTechCompFieldId=sproc_CreateQouteRequestTechCompFields @nQuoteRequestTemplateId,@nQuoteRequestTechCompId,@tTechCompField,@tTechCompName ", tPramListTechCompsFields[4], tPramListTechCompsFields[0], tPramListTechCompsFields[1], tPramListTechCompsFields[2], tPramListTechCompsFields[3]);
                      //  var aQuoteRequestTechCompFieldId = ((SqlParameter)tPramListTechCompsFields[4]).Value == DBNull.Value ? 0 : Convert.ToInt32(((SqlParameter)tPramListTechCompsFields[4]).Value);

                    }
                }
                QuoteRequestTemplate templateObj = new QuoteRequestTemplate();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<QuoteRequestTemplate>(templateObj, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetMergedQuoteRequest(int nProjectID)
        {
            try
            {
                //PO Start
                //int aPurchaseOrderTemplateID = 1;//nProjectID;
               
                //string strBody = "";
                //string strSubject = "";
                //var strNumber = db.Database.SqlQuery<string>("Select tstoreNumber from tblstore with (nolock) where aStoreID= (Select nStoreID from tblProject  with (nolock) where aProjectID= @nProjectID)", new SqlParameter("@nProjectID", nProjectID)).FirstOrDefault();
                //var tSubject = "Store #" + strNumber + "- FabCon Purchase Order";

                //SqlParameter tModuleparm = new SqlParameter("@aPurchaseOrderTemplateID", aPurchaseOrderTemplateID);

                //List<tblPurchaseOrderTemplateTemp> itemFields = db.Database.SqlQuery<tblPurchaseOrderTemplateTemp>("exec sproc_GetPurchaseOrderTemplate @aPurchaseOrderTemplateID", tModuleparm).ToList();

                //strBody +="<div><h1> " + itemFields[0].tTemplateName + " </h1></div>";

                //nProjectID = 7;
                //SqlParameter tModuleparmAdress = new SqlParameter("@nProjectID", nProjectID);

                //List<PurchaseOrderBillingAndShippingDetails> itemAdress = db.Database.SqlQuery<PurchaseOrderBillingAndShippingDetails>("exec sproc_GetPurchaseOrderBillingAndShippingDetails @nProjectID", tModuleparmAdress).ToList();
                //strBody += "<div><b> " + itemFields[0].tTemplateName + " </b></div><div>\r\n    &nbsp;\r\n</div>";
                //strBody += "<div><b> Store No </b> " + strNumber + "</div>";
                ////strBody += "<figure class='table' style='width:100%;'>";
                //strBody += " <table><tbody><tr>";
                //bool btemp = true;              
                //foreach (var PurchaseOrderAdress in itemAdress)
                //{
                
                //    //if (PurchaseOrderAdress.nPurchaseOrderAddressType == PurchaseOrderAddressType.Billing)
                //    if (btemp)
                //    {
                //        strBody += "<td></br><div><b> Billing </b></div></br>";

                //        strBody += "<div><b> Name </b> " + PurchaseOrderAdress.tName + "</div>";

                //        strBody += "<div><b> Phone </b> " + PurchaseOrderAdress.tPhone + "</div>";

                //        strBody += "<div><b> Email </b> " + PurchaseOrderAdress.tEmail + "</div>";

                //        strBody += "<div><b> Address </b> " + PurchaseOrderAdress.tAddress + "</div></td>";
                //        btemp = false;

                //    }
                //    //else if (PurchaseOrderAdress.nPurchaseOrderAddressType == PurchaseOrderAddressType.Shipping)
                //    else
                //    {
                //        strBody += "<td></br><div><b> Shipping </b></div></br>";

                //        strBody += "<div><b> Name </b> " + PurchaseOrderAdress.tName + "</div>";

                //        strBody += "<div><b> Phone </b> " + PurchaseOrderAdress.tPhone + "</div>";

                //        strBody += "<div><b> Email </b> " + PurchaseOrderAdress.tEmail + "</div>";

                //        strBody += "<div><b> Address </b> " + PurchaseOrderAdress.tAddress + "</div></td>";
                //    }
                    
                //}
                //strBody += "</tr></tbody></table></br><div><b> Notes </b> &nbsp;</div>";
                //strBody += "<div><table><thead><tr><th style='width:35%;'>Description</th><th style='width:35%;'>Parts Number</th><th style='width:10%;'>Price</th><th style='width:10%;'> Quantity</th><th style='width:10%;'>Total</th></tr></thead>";

                //nProjectID = 1;
                //SqlParameter tModuleparmParts = new SqlParameter("@nProjectID", nProjectID);

                //List<PurchaseOrderPartDetails> itemParts = db.Database.SqlQuery<PurchaseOrderPartDetails>("exec sproc_GetPurchaseOrderPartsDetails @nProjectID", tModuleparmParts).ToList();
                //decimal cTotal = 0;
                //strBody += "<tbody>";
                //foreach (var parts in itemParts)
                //{
                //    parts.cTotal = parts.cPrice * parts.nQuantity;
                //    strBody += "<tr><td>" + parts.tPartDesc + "</td><td>" + parts.tPartNumber + "</td><td>" + parts.cPrice + "</td><td>" + parts.nQuantity + "</td><td>" + parts.cTotal + "</td></tr>";
                //    cTotal = cTotal+ parts.cTotal;
                //}
                //strBody += "</tbody>";
                //strBody += "</table></div>";
                //strBody += "<div style='text-align:right;'><b> Total:</b> " + cTotal.ToString() + "</div>";
                //strBody += "<div style='text-align:right;'><b> PO#: </b> " + aPurchaseOrderTemplateID.ToString() + "</div>";
                //strBody += "<div style='text-align:right;'><b> Deliver#: </b> " + DateTime.Now.ToString() + "</div>";

                //string URL = HttpRuntime.AppDomainAppPath;
                //string strFilePath = URL + @"Attachments\PurachaaseOrder.pdf";
                ////// Create a new PDF writer
                ////PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(strFilePath, FileMode.Create));

                ////// Open the PDF document
                ////document.Open();
                ////request.tContent = "Please provide a quote for this store based on the information below. Please be sure to reply to all so our entire team receives it. Thanks!\r\nPOS\r\nnStatus\r\nVendor 4\r\nDelivery Date\r\nConfig Date\r\nSupport Date\r\nStore Information\r\nStore Name 1223\r\nAddress1 1108 W. JACKSON\r\nStore City OZARK\r\nStore Name 1223\r\nAddress1 1108 W. JACKSON\r\nStore City OZARK\r\nStore Zip\r\nGeneral Contractor Phone\r\nGeneral ContractorEmail\r\nStore Name 1223\r\nAddress1 1108 W. JACKSON\r\nStore Address2\r\nStore City OZARK\r\nStore Configuration\r\nAudio\r\nVendor 2\r\nConfiguration\r\nCost 452.00\r\nVendor 2\r\nStatus 1\r\nConfiguration\r\nDelivery Date\r\nVendor 2\r\nStatus 1\r\nConfiguration\r\nDelivery Date";
                ////// Add content to the PDF document
                ////document.Add(new Paragraph(request.tContent));
                ////document.Close();
                //StringReader sr = new StringReader(strBody);
                //Document pdfDoc = new Document();// PageSize.A4, 10f, 10f, 10f, 0f);
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //// using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(strFilePath, FileMode.Create));
                //    pdfDoc.Open();

                //    htmlparser.Parse(sr);
                //    pdfDoc.Close();

                //    //byte[] bytes = memoryStream.ToArray();
                //    //memoryStream.Close();
                //}
                ////Start
                //var itemMerge = new MergedQuoteRequest()
                //{
                //    tContent = strBody,
                //    tSubject = "Store #" + strNumber + "- HME Quote Request",
                //    tTo = "abcd@gmail.com"

                //};
                //                         
                //PO End

                // //Quote Start
                // //int nProjectID = 7;
                // int nTemplateId = 3;
                // string strBody = "";
                // string strSubject = "";
                // var strNumber = db.Database.SqlQuery<string>("Select tstoreNumber from tblstore with (nolock) where aStoreID= (Select nStoreID from tblProject  with (nolock) where aProjectID= @nProjectID)", new SqlParameter("@nProjectID", nProjectID)).FirstOrDefault();
                // //strSubject = "Store #" + strNumber + " Quote Request";

                // SqlParameter tModuleNameP = new SqlParameter("@nQuoteRequestTemplateId", nTemplateId);
                // List<QuoteRequestTechCompTemp> item = db.Database.SqlQuery<QuoteRequestTechCompTemp>("exec sproc_GetQuoteRequestTechComp @nQuoteRequestTemplateId", tModuleNameP).ToList();
                // foreach (var RequestTechComp in item)
                // {
                //     strBody += "<div><h1> " + RequestTechComp.tTechCompName + " </h1></div>";
                //    // strBody += "</br> " + RequestTechComp.tTechCompName + ":</br>";
                //     string strData = "";
                //     using (var conn = new SqlConnection(_connectionString))
                //     {

                //         using (var cmd = new SqlCommand("sproc_getDynamicDataFromCompID", conn))
                //         {
                //             cmd.CommandType = CommandType.StoredProcedure;

                //             cmd.Parameters.AddWithValue("@nQuoteRequestTechCompId", RequestTechComp.aQuoteRequestTechCompId);
                //             cmd.Parameters.AddWithValue("@nProjectID", nProjectID);

                //             conn.Open();

                //             using (var reader = cmd.ExecuteReader())
                //             {
                //                 bool RowExist = false;
                //                 do
                //                 {
                //                     while (reader.Read())
                //                     {
                //                         for (int i = 0; i < reader.FieldCount; i++)
                //                         {
                //                             //strData += reader.GetName(i).ToString() + ":" + reader.GetValue(i).ToString() + "</br>";
                //                             strData += "<div><b> "+ reader.GetName(i).ToString() + " </b> "+ reader.GetValue(i).ToString() + "</div>";
                //                             RowExist = true;
                //                         }

                //                     }
                //                     if (!RowExist)
                //                     {
                //                         for (int i = 0; i < reader.FieldCount; i++)
                //                             strData += "<div><b> " + reader.GetName(i).ToString() + " </b></div> ";
                //                     }
                //                 } while (reader.NextResult());
                //             }
                //         }
                //     }
                //     strBody += strData ;
                // }

                //string tContentdata = "<div>Please provide a quote for this store based on the information below. Please be sure to reply to all so our entire team receives it. Thanks!</br></div>";
                // tContentdata += strBody;// "<div><h1>Audio</h1></div><div><b>Address</b>C333 IUO Naaf, USA</div><div><h1>Audio</h1></div><div><b>Address</b>C333 IUO Naaf, USA</div>",

                // var itemMerge = new MergedQuoteRequest()
                // {
                //     tContent = tContentdata,
                //     tSubject = "Store #" + strNumber + "- HME Quote Request",
                //     tTo = "abcd@gmail.com"

                // };

                // // Quote End
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<MergedQuoteRequest>(new MergedQuoteRequest(), new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage SendQuoteRequest(MergedQuoteRequest request)
        {
            try
            {
                //Document document = new Document();
                // string URL = HttpRuntime.AppDomainAppPath;
                // string strFilePath = URL + @"Attachments\PurachaaseOrder.pdf";
                // //// Create a new PDF writer
                // //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(strFilePath, FileMode.Create));

                // //// Open the PDF document
                // //document.Open();
                // //request.tContent = "Please provide a quote for this store based on the information below. Please be sure to reply to all so our entire team receives it. Thanks!\r\nPOS\r\nnStatus\r\nVendor 4\r\nDelivery Date\r\nConfig Date\r\nSupport Date\r\nStore Information\r\nStore Name 1223\r\nAddress1 1108 W. JACKSON\r\nStore City OZARK\r\nStore Name 1223\r\nAddress1 1108 W. JACKSON\r\nStore City OZARK\r\nStore Zip\r\nGeneral Contractor Phone\r\nGeneral ContractorEmail\r\nStore Name 1223\r\nAddress1 1108 W. JACKSON\r\nStore Address2\r\nStore City OZARK\r\nStore Configuration\r\nAudio\r\nVendor 2\r\nConfiguration\r\nCost 452.00\r\nVendor 2\r\nStatus 1\r\nConfiguration\r\nDelivery Date\r\nVendor 2\r\nStatus 1\r\nConfiguration\r\nDelivery Date";
                // //// Add content to the PDF document
                // //document.Add(new Paragraph(request.tContent));
                // //document.Close();
                // StringReader sr = new StringReader(request.tContent);
                // Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                // HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //// using (MemoryStream memoryStream = new MemoryStream())
                // {
                //     PdfWriter writer = PdfWriter.GetInstance(pdfDoc,  new FileStream(strFilePath, FileMode.Create));
                //     pdfDoc.Open();

                //     htmlparser.Parse(sr);
                //     pdfDoc.Close();

                //     //byte[] bytes = memoryStream.ToArray();
                //     //memoryStream.Close();
                // }
                ////Start
                //  string smtpServer = "smtp.sendgrid.net"; // Replace with your SMTP server
                string smtpServer = "smtp.office365.com"; // Replace with your SMTP server
                int smtpPort = 587; // Replace with your SMTP port
                string smtpUsername = "santoshpp@santoshpp.onmicrosoft.com"; // Replace with your SMTP username
                string smtpPassword = "Tali$ma123"; // Replace with your SMTP password

                string fromAddress = "santoshpp@santoshpp.onmicrosoft.com"; // Replace with the sender's email address
                string toAddress = request.tTo; // Replace with the recipient's email address
                string subject = request.tSubject;
                string body = request.tContent;

                // Create an instance of the SmtpClient class
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    // Set the SMTP credentials
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true; // Enable SSL encryption, if required by your SMTP server

                    try
                    {
                        // Create a MailMessage object
                        using (MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body))
                        {
                            // Send the email
                            smtpClient.Send(mailMessage);

                        }
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("An error occurred while sending the email: " + ex.Message);
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while sending the email: " + ex);
                    }
                }
                //start END
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<string>("Done", new JsonMediaTypeFormatter())
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize]
        [HttpGet]
        public string Delete(int nTemplateId)
        {            
            return "";
        }
    }
}
