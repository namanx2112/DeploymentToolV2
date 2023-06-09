﻿using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Data.SqlClient;
using System.Data;
using DeploymentTool.Model.Templates;
using System.Net.Mail;
using iTextSharp.text;
using System.Web;
using System.Web.Helpers;
using System.IO;
using Org.BouncyCastle.Utilities.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace DeploymentTool.Controller
{
    public class PurchaseOrderController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpGet]
        [ActionName("GetAllTemplate")]
        public HttpResponseMessage GetAllTemplate(int nBrandId)
        {
           // nBrandId = 1;
            SqlParameter tModuleNameParam = new SqlParameter("@nBrandId", nBrandId);
            List<PurchaseOrderTemplateTemp> tList = db.Database.SqlQuery<PurchaseOrderTemplateTemp>("exec sproc_GetAllPurchaseOrderTemplate @nBrandId", tModuleNameParam).ToList();
            
          /*  List<PurchaseOrderTemplateTemp> tList = new List<PurchaseOrderTemplateTemp> {
                new PurchaseOrderTemplateTemp()
                {
                    aPurchaseOrderTemplateID= 1,
                    nBrandId= 1,
                    nCreatedBy= 1,
                    nUpdateBy= 1,
                    tTemplateName = "First PO"
                },
                new PurchaseOrderTemplateTemp()
                {
                    aPurchaseOrderTemplateID= 2,
                    nBrandId= 1,
                    nCreatedBy= 1,
                    nUpdateBy= 1,
                    tTemplateName = "Second PO"
                },
                new PurchaseOrderTemplateTemp()
                {
                    aPurchaseOrderTemplateID= 3,
                    nBrandId= 1,
                    nCreatedBy= 1,
                    nUpdateBy= 1,
                    tTemplateName = "Third PO"
                }
            };
			*/
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<PurchaseOrderTemplateTemp>>(tList, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetTemplate(int nTemplateId)
        {
          /*  PurchaseOrderTeamplate tItem = new PurchaseOrderTeamplate()
            {
                aPurchaseOrderTemplateID = nTemplateId,
                nBrandId = 1,
                nCreatedBy = 1,
                tCompName = "Installation",
                nUpdateBy = 1,
                nVendorId = 2,
                tTemplateName = "First PO",
                purchaseOrderParts = new List<PurchaseOrderParts>()
                    {
                        new PurchaseOrderParts()
                        {
                            aPurchaseOrderTemplatePartsID = 1,
                            nPartID= 1,
                            tPartDesc= "Yellow Paint Marker",
                            tPartNumber = "SONIC_MARKER_YEL",
                            cPrice = 88.56M,
                            tTableName = "tblProjectConfig",
                            tTechCompField = "nStallCount"
                        }
                    }
            };
			*/
			PurchaseOrderTeamplate tItem = new PurchaseOrderTeamplate();
            SqlParameter tModuleNameParam = new SqlParameter("@aPurchaseOrderTemplateID", nTemplateId);
            List<PurchaseOrderTeamplate> items = db.Database.SqlQuery<PurchaseOrderTeamplate>("exec sproc_GetPurchaseOrderTemplate @aPurchaseOrderTemplateID", tModuleNameParam).ToList();

            tItem.aPurchaseOrderTemplateID = items[0].aPurchaseOrderTemplateID;
            tItem.tTemplateName = items[0].tTemplateName;
            tItem.tCompName =  items[0].tCompName;            
            tItem.nBrandId = items[0].nBrandId;
            tItem.nVendorId = items[0].nVendorId;
            tItem.nCreatedBy = items[0].nCreatedBy;
            tItem.nUpdateBy = items[0].nUpdateBy;

          
            SqlParameter tModuleparmParts = new SqlParameter("@aPurchaseOrderTemplateID", nTemplateId);

            List<PurchaseOrderParts> itemParts = db.Database.SqlQuery<PurchaseOrderParts>("exec sproc_GetPurchaseOrderPartsDetails @aPurchaseOrderTemplateID", tModuleparmParts).ToList();
            List<PurchaseOrderParts> obj = new List<PurchaseOrderParts>();
            foreach (var RequestTechComp in itemParts)
            {

                obj.Add(new PurchaseOrderParts()
                {
                    
                    aPurchaseOrderTemplatePartsID = RequestTechComp.aPurchaseOrderTemplatePartsID,
                    aPurchaseOrderTemplateID = RequestTechComp.aPurchaseOrderTemplateID,
                    nPartID = RequestTechComp.nPartID,
                    //nVendorId= RequestTechComp.nVendorId,
                    tTechCompField = RequestTechComp.tTechCompField,
                    tPartDesc = RequestTechComp.tPartDesc,
                    tPartNumber = RequestTechComp.tPartNumber,
                    cPrice = RequestTechComp.cPrice,
                    tTableName=RequestTechComp.tTableName
                    //nQuantity = RequestTechComp.nQuantity,
                    //cTotal = RequestTechComp.cTotal


                });

            }
            tItem.purchaseOrderParts = obj;
            //IQueryable<VendorParts> items = db.Database.SqlQuery<VendorParts>("exec sproc_getVendorPartsModel").AsQueryable();
            //if (searchFields == null)
            //{
            //    return items;
            //}
            //else
            //{
            //    StringBuilder sBuilder = new StringBuilder();
            //    foreach (KeyValuePair<string, string> keyVal in searchFields)
            //    {
            //        if (sBuilder.Length > 0)
            //            sBuilder.Append(" and ");
            //        sBuilder.AppendFormat("x.{0}.ToLower().Contains(\"{1}\".ToLower())", keyVal.Key, keyVal.Value);
            //    }
            //    return items.Where("x=>" + sBuilder.ToString());
            //}
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<PurchaseOrderTeamplate>(tItem, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUpdateTemplate(PurchaseOrderTeamplate poRequest)
        {           
            try
            {
                //GetMergedPORequest(poRequest.aPurchaseOrderTemplateID);
                ////Start 
                tblPurchaseOrderTemplate tPurchaseOrder = poRequest.GetTblPurchaseOrder();

                // Add into tblVendorPartRel
                //tPurchaseOrder.aPurchaseOrderTemplateID = poRequest.aPurchaseOrderTemplateID;

                if (poRequest.aPurchaseOrderTemplateID > 0)
                {
                    var npoRequest = db.Database.ExecuteSqlCommand("delete from tblPurchaseOrderTemplateParts where nPurchaseOrderTemplateID =@nPurchaseOrderTemplateID ", new SqlParameter("@nPurchaseOrderTemplateID", poRequest.aPurchaseOrderTemplateID));
                    db.Entry(tPurchaseOrder).State = EntityState.Modified;
                }
                else
                    db.tblPurchaseOrderTemplates.Add(tPurchaseOrder);

                await db.SaveChangesAsync();
                // Add into tblVendorPartRel
                poRequest.aPurchaseOrderTemplateID = tPurchaseOrder.aPurchaseOrderTemplateID;

                List<tblPurchaseOrderTemplatePart> obj = new List<tblPurchaseOrderTemplatePart>();
                foreach (var RequestPOParts in poRequest.purchaseOrderParts)
                {
                    tblPurchaseOrderTemplatePart objPOPart = new tblPurchaseOrderTemplatePart();

                    //if (RequestPOParts.aPartID == 0)
                    //{
                    //    tblPart objtblPart = new tblPart();
                    //    objtblPart.tPartDesc = RequestPOParts.tPartDesc;
                    //    objtblPart.tPartNumber = RequestPOParts.tPartNumber;
                    //    objtblPart.cPrice = RequestPOParts.cPrice;
                    //    db.tblParts.Add(objtblPart);
                    //    await db.SaveChangesAsync();
                    //    // Add into tblVendorPartRel
                    //    objPOPart.nPartID = objtblPart.aPartID;
                    //    RequestPOParts.aPartID = objtblPart.aPartID;
                    //    tblVendorPartRel objtblVendorPart = new tblVendorPartRel();
                    //    // aVendorPartRelID = 0,
                    //    objtblVendorPart.nVendorID = poRequest.nVendorID;
                    //    objtblVendorPart.nPartID = objtblPart.aPartID;
                    //    db.tblVendorPartRels.Add(objtblVendorPart);
                    //    await db.SaveChangesAsync();
                    //}
                    if (RequestPOParts.nPartID > 0)
                    {
                        objPOPart.aPurchaseOrderTemplatePartsID = RequestPOParts.aPurchaseOrderTemplatePartsID;
                        objPOPart.nPurchaseOrderTemplateID = poRequest.aPurchaseOrderTemplateID;
                        objPOPart.nPartID = RequestPOParts.nPartID;
                        objPOPart.tTechCompField = RequestPOParts.tTechCompField;
                        objPOPart.tTableName = RequestPOParts.tTableName;
                        obj.Add(objPOPart);
                    }

                }

                db.tblPurchaseOrderTemplateParts.AddRange(obj);
                await db.SaveChangesAsync();

                //END
            }
            catch (Exception ex)
            {
                if (poRequest.aPurchaseOrderTemplateID == 0)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tblUser
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Create(PurchaseOrderTeamplate poRequest)
        {
            tblPurchaseOrderTemplate tPurchaseOrder = poRequest.GetTblPurchaseOrder();
            db.tblPurchaseOrderTemplates.Add(tPurchaseOrder);
            await db.SaveChangesAsync();
            // Add into tblVendorPartRel
            tPurchaseOrder.aPurchaseOrderTemplateID = poRequest.aPurchaseOrderTemplateID;

            List<tblPurchaseOrderTemplatePart> obj = new List<tblPurchaseOrderTemplatePart>();
            foreach (var RequestPOParts in poRequest.purchaseOrderParts)
            {
                tblPurchaseOrderTemplatePart objPOPart = new tblPurchaseOrderTemplatePart();

                //if (RequestPOParts.aPartID == 0)
                //{
                //    tblPart objtblPart = new tblPart();
                //    objtblPart.tPartDesc = RequestPOParts.tPartDesc;
                //    objtblPart.tPartNumber = RequestPOParts.tPartNumber;
                //    objtblPart.cPrice = RequestPOParts.cPrice;
                //    db.tblParts.Add(objtblPart);
                //    await db.SaveChangesAsync();
                //    // Add into tblVendorPartRel
                //    objPOPart.nPartID = objtblPart.aPartID;
                //    RequestPOParts.aPartID = objtblPart.aPartID;
                //    tblVendorPartRel objtblVendorPart = new tblVendorPartRel();
                //    // aVendorPartRelID = 0,
                //    objtblVendorPart.nVendorID = poRequest.nVendorID;
                //    objtblVendorPart.nPartID = objtblPart.aPartID;
                //    db.tblVendorPartRels.Add(objtblVendorPart);
                //    await db.SaveChangesAsync();

                if (RequestPOParts.nPartID > 0)
                {
                    objPOPart.aPurchaseOrderTemplatePartsID = RequestPOParts.aPurchaseOrderTemplatePartsID;
                    objPOPart.nPurchaseOrderTemplateID = poRequest.aPurchaseOrderTemplateID;
                    objPOPart.nPartID = RequestPOParts.nPartID;
                    objPOPart.tTechCompField = RequestPOParts.tTechCompField;
                    objPOPart.tTableName = RequestPOParts.tTableName;
                    obj.Add(objPOPart);
                }

            }

            db.tblPurchaseOrderTemplateParts.AddRange(obj);
            await db.SaveChangesAsync();

            // db.tblPurchaseOrderTemplateParts.AddRange(poRequest.GettblPurchaseOrderTemplateParts(poRequest));
            // await db.SaveChangesAsync();

            //  poRequest.aPurchaseOrderTemplateID = 10;
            return Json(poRequest);
        }

        // DELETE: api/tblUser/5
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var npoRequest = db.Database.ExecuteSqlCommand("delete from tblPurchaseOrderTemplateParts where nPurchaseOrderTemplateID =@nPurchaseOrderTemplateID ", new SqlParameter("@nPurchaseOrderTemplateID", id));
                var npo = db.Database.ExecuteSqlCommand("delete from tblPurchaseOrderTemplate where aPurchaseOrderTemplateID =@nPurchaseOrderTemplateID ", new SqlParameter("@nPurchaseOrderTemplateID", id));

            }
            PurchaseOrderTeamplate poRequest = new PurchaseOrderTeamplate();


            return Ok(poRequest);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetMergedPO(int nTemplateId, int nProjectId)
        {
            PurchaseOrderPreviewTeamplate poRequest = new PurchaseOrderPreviewTeamplate();
            

            try
            {
                //PO Start
               // int aPurchaseOrderTemplateID = 4;
                SqlParameter tModuleparmAdress = new SqlParameter("@nProjectID", nProjectId);

                List<PurchaseOrderPreviewTeamplate> itemPOStore= db.Database.SqlQuery<PurchaseOrderPreviewTeamplate>("exec sproc_GetPurchaseOrdeStorerDetails @nProjectID", tModuleparmAdress).ToList();
                SqlParameter tModuleparm = new SqlParameter("@aPurchaseOrderTemplateID", nTemplateId);
                List<tblPurchaseOrderTemplate> itemPOTemplate = db.Database.SqlQuery<tblPurchaseOrderTemplate>("exec sproc_GetPurchaseOrderTemplate @aPurchaseOrderTemplateID", tModuleparm).ToList();

                SqlParameter tModuleparmTempID = new SqlParameter("@nPurchaseOrderTemplateID", nTemplateId);
                SqlParameter tModuleparmParts = new SqlParameter("@nProjectID", nProjectId);

                List<PurchaseOrderPartDetails> itemPOParts = db.Database.SqlQuery<PurchaseOrderPartDetails>("exec sproc_GetPurchaseOrderPartsDetails @nPurchaseOrderTemplateID,@nProjectID", tModuleparmTempID, tModuleparmParts).ToList();

                poRequest.nVendorId = (int)itemPOTemplate[0].nVendorID;// 1;
                poRequest.tStore = itemPOStore[0].tStore;// "111";
                poRequest.tStoreNumber = itemPOStore[0].tStoreNumber;// "10101";
                poRequest.tNotes = "";// "Hello";
                poRequest.tName = itemPOStore[0].tName;// "Name";
                poRequest.tPhone = itemPOStore[0].tPhone;// "33982823498423";
                poRequest.tEmail = itemPOStore[0].tEmail;// "heell@ggmail.com";
                poRequest.tAddress = itemPOStore[0].tAddress;// "1st streat, scond block";
                poRequest.tCity = itemPOStore[0].tCity;// "Atlaanta";
                poRequest.tStoreState = itemPOStore[0].tStoreState;// "NewYork";
                poRequest.tStoreZip = itemPOStore[0].tStoreZip;// "45449";
                poRequest.tBillToCompany = itemPOStore[0].tBillToCompany;// "Test";
                poRequest.tBillToEmail = itemPOStore[0].tBillToEmail;// "bill@gmal.com";
                poRequest.tBillToAddress = itemPOStore[0].tBillToAddress;// "second streat, 1st main";
                poRequest.tBillToCity = itemPOStore[0].tBillToCity;// "Atlantta";
                poRequest.tBillToState = itemPOStore[0].tBillToState;// "Newjurcy";
                poRequest.tTemplateName= itemPOTemplate[0].tTemplateName;
               // poRequest.cTotal = 1000.55M;
                poRequest.tPurchaseOrderNumber = nTemplateId.ToString();
                poRequest.dDeliver = DateTime.Now;
                poRequest.nOutgoingEmailID = 0;
                decimal cTotal = 0;
                List<PurchaseOrderParts> obj =new List<PurchaseOrderParts>();
                foreach (var parts in itemPOParts)
                {
                    PurchaseOrderParts objParts = new PurchaseOrderParts();

                    objParts.aPurchaseOrderTemplatePartsID = parts.nPurchaseOrderPartDetailsID;
                    objParts.nPartID = parts.nPartID;// 1;
                    objParts.tPartDesc = parts.tPartDesc;// "Yellow Paint Marker";
                    objParts.tPartNumber = parts.tPartNumber;// "SONIC_MARKER_YEL";
                    objParts.cPrice = parts.cPrice;// 88.56M;
                    objParts.tTableName = parts.tTableName;// "tblProjectConfig";
                    objParts.tTechCompField = parts.tTechCompField;// "nStallCount";                  
                    objParts.nQuantity = parts.nQuantity;// 10;                    
                    parts.cTotal = parts.cPrice * parts.nQuantity;
                    cTotal = cTotal + parts.cTotal;
                    obj.Add(objParts);
                }
                poRequest.cTotal = cTotal;
                poRequest.purchaseOrderParts = obj;
               



            //PO End



        }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

           
            //tblPart tblPart = await db.tblParts.FindAsync(id);
            //if (tblPart == null)
            //{
            //    return NotFound();
            //}

            //db.tblParts.Remove(tblPart);
            //await db.SaveChangesAsync();

            return Ok(poRequest);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult SenMergedPO(PurchaseOrderPreviewTeamplate request)
        {
            string strBody = "";
            string strSubject = "";
            //var strNumber = db.Database.SqlQuery<string>("Select tstoreNumber from tblstore with (nolock) where aStoreID= (Select nStoreID from tblProject  with (nolock) where aProjectID= @nProjectID)", new SqlParameter("@nProjectID", nProjectID)).FirstOrDefault();
            var tSubject = "Store #" + request.tStoreNumber + " - " + request.tTemplateName + " Purchase Order";

            strBody += "<div><h1> " + request.tTemplateName + " </h1></div>";

           // strBody += "<div><b> " + request.tTemplateName + " </b></div><div>\r\n    &nbsp;\r\n</div>";
            strBody += "<div><b> Store No </b> " + request.tStoreNumber + "</div>";
            //strBody += "<figure class='table' style='width:100%;'>";
            strBody += " <table><tbody><tr>";
            bool btemp = true;
            //foreach (var PurchaseOrderAdress in itemAdress)
            {

                //if (PurchaseOrderAdress.nPurchaseOrderAddressType == PurchaseOrderAddressType.Billing)
                //if (btemp)
                // {
                strBody += "<td></br><div><b> Billing </b></div></br>";

                strBody += "<div><b> Name </b> " + request.tName + "</div>";

                strBody += "<div><b> Phone </b> " + request.tPhone + "</div>";

                strBody += "<div><b> Email </b> " + request.tEmail + "</div>";

                strBody += "<div><b> Address </b> " + request.tAddress + "</div></br>";
                strBody += "<div> " + request.tCity + " " + request.tStoreState + " " + request.tStoreZip + "</div></td>";

                //btemp = false;

                //}
                //else if (PurchaseOrderAdress.nPurchaseOrderAddressType == PurchaseOrderAddressType.Shipping)

                strBody += "<td></br><div><b> Shipping </b></div></br>";

                strBody += "<div><b> Store </b> " + request.tStore + "</div>";

                strBody += "<div><b> Name </b> " + request.tName + "</div>";

                //strBody += "<div><b> Email </b> " + itemAdress[0].tEmail + "</div>";

                strBody += "<div><b> Address </b> " + request.tBillToAddress + "</div>";
                strBody += "<div> " + request.tBillToCity + " " + request.tBillToState + " " + request.tStoreZip + "</div></td>";


            }
            strBody += "</tr></tbody></table></br><div><b> Notes </b> &nbsp;</div>";
            strBody += "<div><table><thead><tr><th style='width:35%;'>Description</th><th style='width:35%;'>Parts Number</th><th style='width:10%;'>Price</th><th style='width:10%;'> Quantity</th><th style='width:10%;'>Total</th></tr></thead>";

           
            strBody += "<tbody>";
            foreach (var parts in request.purchaseOrderParts)
            {
               // parts.cTotal = parts.cPrice * parts.nQuantity;
                strBody += "<tr><td>" + parts.tPartDesc + "</td><td>" + parts.tPartNumber + "</td><td>" + parts.cPrice + "</td><td>" + parts.nQuantity + "</td><td>" + parts.cTotal + "</td></tr>";
                //cTotal = cTotal + parts.cTotal;
            }
            strBody += "</tbody>";
            strBody += "</table></div>";
            strBody += "<div style='text-align:right;'><b> Total:</b> " + request.cTotal.ToString() + "</div>";
            strBody += "<div style='text-align:right;'><b> PO#: </b> " + request.aPurchaseOrderPreviewTeamplateID.ToString() + "</div>";
            strBody += "<div style='text-align:right;'><b> Deliver#: </b> " + request.dDeliver.ToShortDateString() + "</div>";
            String strFilePath= DeploymentTool.Misc.Utilities.WriteHTMLToPDF(strBody);

            PurchaseOrderMailMessage message = new PurchaseOrderMailMessage()
            {
                nProjectId = request.nProjectId,
                tTo = "abcd@gmail.com",
                tCC = "ccabcd@gmail.com",
                tContent = "<div style='background-color:gray'>All Febcon attachment with content for this PO</div>" +
                "<div><span>PO#:</span>" + request.tPurchaseOrderNumber + "<br/>" +
                "<span>Revision/Filename:</span>"+ strFilePath + "<br/>" +
                "<span>Type:</span>HME<br/>" +
                "<span>Store:</span>" + request.tStoreNumber + "<br/>" +
                "<span>Delivery:</span>" + request.dDeliver.ToShortDateString() + "<br/>" +
                "<span>Project Manager:</span>Santosh PP<br/>" +
                "</div>",
                tFileName = strFilePath,
                tSubject = "Hello HME"
            };
            
            
            return Ok(message);
        }

        [Authorize]
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult>  SendPO(PurchaseOrderMailMessage request)
        {
            EMailRequest MailObj = new EMailRequest();
            MailObj.tSubject = request.tSubject;
            MailObj.tTo = request.tTo;
            MailObj.tCC = request.tCC;
            MailObj.tContent = request.tContent;
            MailObj.FileAttachments = request.FileAttachments;
            DeploymentTool.Misc.Utilities.SendMail(MailObj);
            tblOutgoingEmail tblQuoteEmail = MailObj.GettblOutgoingEmail();
            db.tblOutgoingEmails.Add(tblQuoteEmail);
            await db.SaveChangesAsync();
            var aOutgoingEmailID = tblQuoteEmail.aOutgoingEmailID;

            foreach (var RequestFile in request.FileAttachments)
            {

                tblOutgoingEmailAttachment tblQuoteEmailAtt = MailObj.GettblOutgoingEmailAttachment(RequestFile);
                db.tblOutgoingEmailAttachments.Add(tblQuoteEmailAtt);
                await db.SaveChangesAsync();
            }            
            // request.
            // Send PO
            return Ok(1);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage downloadPO(string tFileName, int nProjectId)
        {
            string fileName = "PurachaaseOrder.pdf";

            string URL = HttpRuntime.AppDomainAppPath;
            string strFilePath = URL + @"Attachments\" + fileName;

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(strFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

            return response;
        }


        private bool tblUserExists(int id)
        {
            return db.tblParts.Count(e => e.aPartID == id) > 0;
        }
    }
}
