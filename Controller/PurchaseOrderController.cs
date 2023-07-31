using DeploymentTool.Model;
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
using System.IO;
using System.Web;

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
            /*  PurchaseOrderTemplate tItem = new PurchaseOrderTemplate()
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
            PurchaseOrderTemplate tItem = new PurchaseOrderTemplate();
            SqlParameter tModuleNameParam = new SqlParameter("@aPurchaseOrderTemplateID", nTemplateId);
            List<PurchaseOrderTemplate> items = db.Database.SqlQuery<PurchaseOrderTemplate>("exec sproc_GetPurchaseOrderTemplate @aPurchaseOrderTemplateID", tModuleNameParam).ToList();

            tItem.aPurchaseOrderTemplateID = items[0].aPurchaseOrderTemplateID;
            tItem.tTemplateName = items[0].tTemplateName;
            tItem.tCompName = items[0].tCompName;
            tItem.nBrandID = items[0].nBrandID;
            tItem.nVendorID = items[0].nVendorID;
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
                    tTableName = RequestTechComp.tTableName
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
                Content = new ObjectContent<PurchaseOrderTemplate>(tItem, new JsonMediaTypeFormatter())
            };
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUpdateTemplate(PurchaseOrderTemplate poRequest)
        {
            try
            {
                //GetMergedPORequest(poRequest.aPurchaseOrderTemplateID);
                ////Start 
                tblPurchaseOrderTemplate tPurchaseOrder = poRequest.GetTblPurchaseOrder(HttpContext.Current);

                // Add into tblVendorPartRel
                //tPurchaseOrder.aPurchaseOrderTemplateID = poRequest.aPurchaseOrderTemplateID;

                if (poRequest.aPurchaseOrderTemplateID > 0)
                {
                    var npoRequest = db.Database.ExecuteSqlCommand("delete from tblPurchaseOrderTemplateParts where nPurchaseOrderTemplateID =@nPurchaseOrderTemplateID ", new SqlParameter("@nPurchaseOrderTemplateID", poRequest.aPurchaseOrderTemplateID));
                    db.Entry(tPurchaseOrder).State = EntityState.Modified;
                }
                else
                {

                    db.tblPurchaseOrderTemplates.Add(tPurchaseOrder);
                }

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
        public async Task<IHttpActionResult> Create(PurchaseOrderTemplate poRequest)
        {
            tblPurchaseOrderTemplate tPurchaseOrder = poRequest.GetTblPurchaseOrder(HttpContext.Current);
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
        [HttpGet]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var npoRequest = db.Database.ExecuteSqlCommand("delete from tblPurchaseOrderTemplateParts where nPurchaseOrderTemplateID =@nPurchaseOrderTemplateID ", new SqlParameter("@nPurchaseOrderTemplateID", id));
                var npo = db.Database.ExecuteSqlCommand("delete from tblPurchaseOrderTemplate where aPurchaseOrderTemplateID =@nPurchaseOrderTemplateID ", new SqlParameter("@nPurchaseOrderTemplateID", id));

            }
            PurchaseOrderTemplate poRequest = new PurchaseOrderTemplate();


            return Ok(poRequest);
        }

        [Authorize]
        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetMergedPO(int nTemplateId, int nStoreId)//SantoshPP 
        {
            PurchaseOrderPreviewTemplate poRequest = new PurchaseOrderPreviewTemplate();


            try
            {
               // int nProjectId = nStoreId;//SantoshPP getActive Projects for This Store and then process
                //PO Start
                // int aPurchaseOrderTemplateID = 4;
                SqlParameter tModuleparmAdress = new SqlParameter("@nStoreId", nStoreId);

                List<PurchaseOrderPreviewTemplate> itemPOStore = db.Database.SqlQuery<PurchaseOrderPreviewTemplate>("exec sproc_GetPurchaseOrdeStorerDetails @nStoreId", tModuleparmAdress).ToList();
                SqlParameter tModuleparm = new SqlParameter("@aPurchaseOrderTemplateID", nTemplateId);
                List<PurchaseOrderTemplate> itemPOTemplate = db.Database.SqlQuery<PurchaseOrderTemplate>("exec sproc_GetPurchaseOrderTemplate @aPurchaseOrderTemplateID,@nStoreId", tModuleparm, new SqlParameter("@nStoreId", nStoreId)).ToList();

                SqlParameter tModuleparmTempID = new SqlParameter("@nPurchaseOrderTemplateID", nTemplateId);
                SqlParameter tModuleparmParts = new SqlParameter("@nStoreId", nStoreId);

                List<PurchaseOrderPartDetails> itemPOParts = db.Database.SqlQuery<PurchaseOrderPartDetails>("exec sproc_GetPurchaseOrderPartsDetails @nPurchaseOrderTemplateID,@nStoreId", tModuleparmTempID, tModuleparmParts).ToList();
                poRequest.nProjectId = nStoreId;
                poRequest.nVendorId = (int)itemPOTemplate[0].nVendorID;// 1;
                poRequest.tVendorName = itemPOTemplate[0].tVendorName;
                poRequest.tStore = itemPOStore[0].tStore + " #" + itemPOStore[0].tStoreNumber;
                poRequest.tStoreNumber = itemPOStore[0].tStoreNumber;// "10101";
                poRequest.tNotes = "";// "Hello";
                poRequest.tName = itemPOStore[0].tName;// "Name";
                poRequest.tPhone = itemPOStore[0].tPhone;// "33982823498423";
                poRequest.tEmail = itemPOStore[0].tEmail;// "heell@ggmail.com";
                poRequest.tAddress = itemPOStore[0].tAddress + ", " + itemPOStore[0].tAddress2 + ", " + itemPOStore[0].tCity + ", " + itemPOStore[0].tStoreState + ", " + itemPOStore[0].tStoreZip;// "1st streat, scond block";
                poRequest.tCity = itemPOStore[0].tCity;// "Atlaanta";
                poRequest.tStoreState = itemPOStore[0].tStoreState;// "NewYork";
                poRequest.tStoreZip = itemPOStore[0].tStoreZip;// "45449";
                poRequest.tBillToCompany = itemPOStore[0].tBillToCompany;// "Test";
                poRequest.tBillToEmail = itemPOStore[0].tBillToEmail;// "bill@gmal.com";
                poRequest.tBillToAddress = itemPOStore[0].tBillToAddress + " " + itemPOStore[0].tBillToCity + ", " + itemPOStore[0].tBillToState + " " + itemPOStore[0].tBillToZip;// "1st streat, scond block";
                poRequest.tBillToCity = itemPOStore[0].tBillToCity;// "Atlantta";
                poRequest.tBillToState = itemPOStore[0].tBillToState;// "Newjurcy";
                poRequest.tBillToZip = itemPOStore[0].tBillToZip;// "Newjurcy";
                poRequest.tTemplateName = itemPOTemplate[0].tTemplateName;
                poRequest.nTemplateId = nTemplateId;
                poRequest.dDeliver = itemPOTemplate[0].dDeliveryDate != null ? Convert.ToDateTime(itemPOTemplate[0].dDeliveryDate) : DateTime.Now;
                poRequest.tTo = itemPOTemplate[0].tTo;
                poRequest.tCC = itemPOTemplate[0].tCC;
                poRequest.tProjectManager = itemPOStore[0].tProjectManager;

                poRequest.nOutgoingEmailID = 0;
                decimal cTotal = 0;
                List<PurchaseOrderParts> obj = new List<PurchaseOrderParts>();
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

                tblPurchaseOrder tblPO = new tblPurchaseOrder()
                {
                    nTemplateId = poRequest.nTemplateId,
                    nStoreID = Convert.ToInt32(poRequest.tStoreNumber),
                    tBillingName = poRequest.tName,
                    tBillingPhone = poRequest.tPhone,
                    tBillingEmail = poRequest.tBillToEmail,
                    tBillingAddress = poRequest.tBillToAddress,
                    tShippingName = poRequest.tName,
                    tShippingPhone = poRequest.tPhone,
                    tShippingEmail = poRequest.tEmail,
                    tShippingAddress = poRequest.tAddress,
                    tNotes = poRequest.tNotes,
                    dDeliver = poRequest.dDeliver,
                    cTotal = poRequest.cTotal

                };

                db.tblPurchaseOrders.Add(tblPO);
                await db.SaveChangesAsync();
                var aPurchaseOrderID = tblPO.aPurchaseOrderID;
                poRequest.aPurchaseOrderPreviewTeamplateID = aPurchaseOrderID;



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
        public async System.Threading.Tasks.Task<IHttpActionResult> SenMergedPO(PurchaseOrderPreviewTemplate request)
        {
            string strBody = "";
            strBody = "<htm><head></head><body><div  style='text-align:center;'><h1> " + request.tTemplateName + " Purchase Order </h1></div>";
            strBody += "<div style='width:100%;line-height:25px;float:left' ><b> Store No </b> " + request.tStoreNumber + "</div>";
            strBody += "</br><div style='line-height:25px;float:left' ><p> </p> </div>";
            strBody += "<div> <table  style='border: 1px solid black;'><tbody><tr>";
            strBody += "<td> </br><div><b> Billing </b></div></br>";
            strBody += "<div><b> Name </b> " + request.tName + "</div>";
            strBody += "<div><b>Phone </b> " + request.tPhone + "</div>";
            strBody += "<div><b>Email </b> " + request.tBillToEmail + "</div>";
            strBody += "<div><b>Address </b> " + request.tBillToAddress + "</div></br></td>";
            //strBody += "<div> " + request.tCity + " " + request.tStoreState + " " + request.tStoreZip + "</div></td>";
            strBody += "<td></br><div><b>Shipping </b></div></br>";
            strBody += "<div><b>Store </b> " + request.tStore + "</div>";
            strBody += "<div><b>Name </b> " + request.tName + "</div>";
            // strBody += "<div  style='text-align:left;'><b> Email </b> " + request.tEmail + "</div>";
            strBody += "<div><b>Address </b> " + request.tAddress + "</div>";
            strBody += "<div> </div></td>";
            //  strBody += "<div> " + request.tBillToCity + " " + request.tBillToState + " " + request.tBillToZip + "</div></td>";

            strBody += "</tr></tbody></table></br>";
            strBody += "<div style='width:100%;line-height:25px;float:left'><b> Notes </b> " + request.tNotes + " </div>";
            strBody += "<br></br><div style='line-height:25px;float:left'><p></p></div>";
            strBody += "</br></br><div><table style='border: 1px solid black;'><thead style='border: 1px solid black;'><tr><b><th style='width:35%;'>Description</th><th style='width:35%;'>Parts Number</th><th style='width:10%;'>Price</th><th style='width:10%;'> Quantity</th><th style='width:10%;'>Total</th></b></tr></thead>";
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
            strBody += "</body></html>";
            string fileName = "PurchaseOrder.pdf";
            String strFilePath = DeploymentTool.Misc.Utilities.WriteHTMLToPDF(strBody, fileName);
            string strStyle = "<style>td{ border: 0px none!important;} " +
                    "table{ width: 60%!important;border: 1px solid lightgray!important;border-radius: 5px!important;}</style>";
            PurchaseOrderMailMessage message = new PurchaseOrderMailMessage()
            {
                nProjectId = request.nProjectId,
                tTo = request.tTo,
                tCC = request.tCC,
                tContent = strStyle + "<div>All,</br></br>See attached purchase order, Please verify that you can meet the delivery date listed.</div>Thanks!" +
                "<table>" +
                "<tr><td>PO#:</td><td>" + request.aPurchaseOrderPreviewTeamplateID + "</td></tr>" +
                "<tr><td>Revision/Filename:</td><td>" + fileName + "</td></tr>" +
                "<tr><td>Type:</td><td>" + request.tVendorName + "</td></tr>" +
                "<tr><td>Store:</td><td>" + request.tStoreNumber + "</td></tr>" +
                "<tr><td>Delivery:</td><td>" + request.dDeliver.ToShortDateString() + "</td></tr>" +
                "<tr><td>Project Manager:</td><td>" + request.tProjectManager + "</td></tr>" +
                "<table>",
                tFileName = fileName,
                tSubject = request.tCity + ", " + request.tStoreState + " #" + request.tStoreNumber + " - " + request.tVendorName + " " + request.tTemplateName + " Purchase Order",
                tMyFolderId = strFilePath,
                aPurchaseOrderID = request.aPurchaseOrderPreviewTeamplateID,

            };

            tblPurchaseOrder tblPO = new tblPurchaseOrder()
            {
                aPurchaseOrderID = request.aPurchaseOrderPreviewTeamplateID,
                nTemplateId = request.nTemplateId,
                nStoreID = Convert.ToInt32(request.tStoreNumber),
                tBillingName = request.tName,
                tBillingPhone = request.tPhone,
                tBillingEmail = request.tBillToEmail,
                tBillingAddress = request.tBillToAddress,
                tShippingName = request.tName,
                tShippingPhone = request.tPhone,
                tShippingEmail = request.tEmail,
                tShippingAddress = request.tAddress,
                tNotes = request.tNotes,
                dDeliver = request.dDeliver,
                cTotal = request.cTotal

            };

            //db.tblPurchaseOrders.Add(tblPO);
            db.Entry(tblPO).State = EntityState.Modified;
            // Update into tblVendorPartRel


            await db.SaveChangesAsync();
            var aPurchaseOrderID = tblPO.aPurchaseOrderID;

            return Ok(message);
        }

        [Authorize]
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> SendPO(PurchaseOrderMailMessage request)
        {
            try
            {
                //string fileName = "PurachaaseOrder.pdf";

                //string URL = HttpRuntime.AppDomainAppPath;
                //string strFilePath = URL + @"Attachments\" + fileName;

                EMailRequest MailObj = new EMailRequest();
                MailObj.tSubject = request.tSubject;
                MailObj.tTo = request.tTo;
                MailObj.tCC = request.tCC;
                MailObj.tContent = request.tContent;
                MailObj.tFilePath = request.tMyFolderId;// strFilePath;
                                                        //FileAttachment ifile = new FileAttachment();
                                                        //ifile.tFileName= strFilePath;
                                                        //List< FileAttachment> ifiles = new List<FileAttachment>();
                                                        //ifiles.Add(ifile);
                                                        //MailObj.FileAttachments= ifiles;
                DeploymentTool.Misc.Utilities.SendMail(MailObj);
                tblOutgoingEmail tblQuoteEmail = MailObj.GettblOutgoingEmail();
                db.tblOutgoingEmails.Add(tblQuoteEmail);
                await db.SaveChangesAsync();
                var aOutgoingEmailID = tblQuoteEmail.aOutgoingEmailID;
                var noOfRowUpdated = db.Database.ExecuteSqlCommand("update tblPurchaseOrder set nOutgoingEmailID=@nOutgoingEmailID  where aPurchaseOrderID =@aPurchaseOrderID", new SqlParameter("@nOutgoingEmailID", aOutgoingEmailID), new SqlParameter("@aPurchaseOrderID", request.aPurchaseOrderID));

                //if (MailObj.FileAttachments != null)
                //{
                //    foreach (var RequestFile in MailObj.FileAttachments)
                //    {

                //        tblOutgoingEmailAttachment tblQuoteEmailAtt = MailObj.GettblOutgoingEmailAttachment(RequestFile);
                //        db.tblOutgoingEmailAttachments.Add(tblQuoteEmailAtt);
                //        await db.SaveChangesAsync();
                //    }
                //}
                tblOutgoingEmailAttachment tblQuoteEmailAtt = new tblOutgoingEmailAttachment();
                tblQuoteEmailAtt.nOutgoingEmailID = aOutgoingEmailID;
                tblQuoteEmailAtt.tFileName = request.tFileName;
                tblQuoteEmailAtt.ifile = File.ReadAllBytes(request.tMyFolderId);
                db.tblOutgoingEmailAttachments.Add(tblQuoteEmailAtt);
                await db.SaveChangesAsync();

                if (File.Exists(request.tMyFolderId))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(request.tMyFolderId);
                    bool exists = System.IO.Directory.Exists(request.tMyFolderId.Replace("\\"+ request.tFileName,""));

                    if (exists)
                        System.IO.Directory.Delete(request.tMyFolderId.Replace("\\" + request.tFileName, ""));
                }
            }
            catch (Exception ex)
            { 
            }
            // request.
            // Send PO
            return Ok(1);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage downloadPO(PurchaseOrderFile request)// SantoshPP
        {
            //string fileName = "PurachaaseOrder.pdf";

           // string URL = HttpRuntime.AppDomainAppPath;
            string strFilePath = request.tMyFolderId;

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(strFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = request.tFileName;
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

            return response;
        }


        private bool tblUserExists(int id)
        {
            return db.tblParts.Count(e => e.aPartID == id) > 0;
        }

    }
}
