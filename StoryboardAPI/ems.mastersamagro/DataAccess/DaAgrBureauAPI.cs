using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Xml;
using RestSharp;
using System.Net;
using Spire.Pdf;
using Spire.Doc;
using System.Drawing;
//using Spire.Pdf.HtmlConverter;
using System.IO;
using System.Threading;
using System.Configuration;
using Spire.Pdf.Graphics;
using Spire.Doc.Documents;
using System.Globalization;
using Spire.Doc.Fields;
using Spire.Pdf.HtmlConverter;
using System.Text;
using System.Xml.Xsl;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using ems.mastersamagro.TransUnionService;
using ems.storage.Functions;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access for various Single and Mutliple events (Highmark,
    /// TransUnionCommercial, TransUnionConsumer -  Credit , Institution) in Bureau API - Buyer.
    /// </summary>
    /// <remarks>Written by Praveen </remarks>
    public class DaAgrBureauAPI
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult, mnResultHRA;
        string lscompany_code, path, lspath;
        string name, pan, dob, address, city, statetemp, state, pin, phone_number;
        string templatePath, templatePathDoc, fileName;       

        string finalData = string.Empty;
        PdfDocument pdf = new PdfDocument();

        string gender, id_type, telephone_no,telephone_type, residence_type,state_code;
        string addressline1, addressline2, address_type, postal_code;
        string lsreportpdfpath;

        string application_id, document_id;

        string gender_name, gender_code;

        string temp_bureauresponse ="";

        string company_name, companypan_no, mobile_no;
        string report_id;
        string templateIssuePath;
        string finalResponseHtmlData;
        string validation_message;
        string bureau_type;
        string failedStatus = "Failed", successStatus = "Success";
        string loglspath, logreportFileName = "";

        string noofphones_rep3months, noofaddresses_rep3months, noofdistphones_rep3months, noofdistddresses_rep3months,
               noofdistids_rep3months, noofdistpincodes_rep3months, enqdifflenders_30days, newloansopened_30days,
               distunsecuredenq_3months, ranksegment_hml;


        public MdlHighmarkResponse DaGetHighmarkCreditInfo(string employee_gid, string contact_gid)
        {
            MdlHighmarkResponse ObjMdlHighmarkResponse = new MdlHighmarkResponse();
            bureau_type = "Consumer";
            BureauLogResponse bureauLogResponse = new BureauLogResponse();
            try
            {
                LogForAuditHighmark("Logging started for Highmark Consumer with Contact GID - " + contact_gid + " at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), bureau_type, contact_gid);
                HighmarkConsumer highmarkConsumer = new HighmarkConsumer();

                msSQL = " select first_name,middle_name,last_name,pan_no,individual_dob from agr_mst_tcontact where " +
                       " contact_gid='" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    highmarkConsumer.name = objODBCDatareader["first_name"].ToString() + " " + objODBCDatareader["middle_name"].ToString() + " " + objODBCDatareader["last_name"].ToString();
                    highmarkConsumer.pan = objODBCDatareader["pan_no"].ToString();
                    highmarkConsumer.dob = objODBCDatareader["individual_dob"].ToString().Replace('-', '/');
                }

                msSQL = " select addressline1,addressline2,city,state,postal_code from agr_mst_tcontact2address where " +
                           " contact_gid='" + contact_gid + "' and primary_status='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    highmarkConsumer.address = objODBCDatareader["addressline1"].ToString() + objODBCDatareader["addressline2"].ToString();
                    highmarkConsumer.city = objODBCDatareader["city"].ToString();
                    highmarkConsumer.statetemp = objODBCDatareader["state"].ToString();
                    highmarkConsumer.pin = objODBCDatareader["postal_code"].ToString();
                }
                msSQL = " select state_code from agr_mst_thighmarkstatecode where " +
                          " state='" + highmarkConsumer.statetemp + "'";
                highmarkConsumer.state = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select mobile_no from agr_mst_tcontact2mobileno where " +
                           " contact_gid='" + contact_gid + "' and primary_status='Yes'";
                highmarkConsumer.phone_number = objdbconn.GetExecuteScalar(msSQL);

                if (HighmarkConsumerValidator(highmarkConsumer) == false)
                {
                    ObjMdlHighmarkResponse.status = false;
                    ObjMdlHighmarkResponse.message = validation_message;
                    return ObjMdlHighmarkResponse;
                }
                LogForAuditHighmark("Input fields validated..!", bureau_type, contact_gid);

                templatePath = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateRequestHighMark.xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(templatePath);

                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/APPLICANT-SEGMENT/APPLICANT-NAME/NAME1").InnerText = name.ToUpper();
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/APPLICANT-SEGMENT/DOB/DOB-DATE").InnerText = dob;
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/APPLICANT-SEGMENT/IDS/ID/VALUE").InnerText = pan;
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/APPLICANT-SEGMENT/PHONES/PHONE/TELE-NO").InnerText = phone_number;

                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/ADDRESS-SEGMENT/ADDRESS/ADDRESS-1").InnerText = address.ToUpper();
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/ADDRESS-SEGMENT/ADDRESS/CITY").InnerText = city.ToUpper();
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/ADDRESS-SEGMENT/ADDRESS/STATE").InnerText = state;
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/ADDRESS-SEGMENT/ADDRESS/PIN").InnerText = pin;

                string xmlreqContents = xmlDoc.InnerXml;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                string requestUri = ConfigurationManager.AppSettings["highmarkurl"].ToString();
                string xmlResponse = string.Empty;
               
                    var client = new RestClient(requestUri);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("requestXML", xmlreqContents);
                    request.AddHeader("userId", ConfigurationManager.AppSettings["highmarkuserId"].ToString());
                    request.AddHeader("password", ConfigurationManager.AppSettings["highmarkpassword"].ToString());
                    request.AddHeader("mbrid", ConfigurationManager.AppSettings["highmarkmbrid"].ToString());
                    request.AddHeader("productType", "INDV");
                    request.AddHeader("productVersion", "1.0");
                    request.AddHeader("reqVolType", "INDV");

                    IRestResponse response = client.Execute(request);
                    xmlResponse = response.Content;

                LogForAuditHighmark("Response obtained for the Bureau Request..!", bureau_type, contact_gid);
                LogForAuditHighmark(xmlResponse, bureau_type, contact_gid);
                LogForAuditHighmark("End of Response..!", bureau_type, contact_gid);

                XmlDocument xmlResponseDoc = new XmlDocument();
                    xmlResponseDoc.LoadXml(xmlResponse);

                if (xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-VALUE") == null)
                {
                    ObjMdlHighmarkResponse.status = false;
                    ObjMdlHighmarkResponse.message = "Bureau Score was not obtained in the Response..!";
                    bureauLogResponse.failure_reason = ObjMdlHighmarkResponse.message;
                    LogHighmarkConsumerResponse(contact_gid, employee_gid, bureauLogResponse, failedStatus);
                    LogForAuditHighmark("Bureau Score Not Obtained in Response..!", bureau_type, contact_gid);
                    LogForAuditHighmark("Logging ended for Highmark Consumer..!", bureau_type, contact_gid);
                    return ObjMdlHighmarkResponse;
                }
                else
                {

                    ObjMdlHighmarkResponse.bureau_score = xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-VALUE").InnerText;
                    LogForAuditHighmark("Bureau Score Obtained..!", bureau_type, contact_gid);
                }

                if (xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-COMMENTS") == null && xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-FACTORS") == null)
                {
                    ObjMdlHighmarkResponse.status = false;
                    ObjMdlHighmarkResponse.message = "Bureau Comments was not obtained in the Response..!";
                    bureauLogResponse.failure_reason = ObjMdlHighmarkResponse.message;
                    LogHighmarkConsumerResponse(contact_gid, employee_gid, bureauLogResponse, failedStatus);
                    LogForAuditHighmark("Bureau Comments Not Obtained in Response..!", bureau_type, contact_gid);
                    LogForAuditHighmark("Logging ended for Highmark Consumer..!", bureau_type, contact_gid);
                    return ObjMdlHighmarkResponse;
                }

                if (!(xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-COMMENTS") == null))
                {
                    temp_bureauresponse += xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-COMMENTS").InnerText;
                    LogForAuditHighmark("Bureau Comments Obtained..!", bureau_type, contact_gid);
                }
                if (!(xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-FACTORS") == null))
                {
                    if(temp_bureauresponse != "")
                    {
                        temp_bureauresponse = temp_bureauresponse + "; " + xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-FACTORS").InnerText;
                    } else
                    {
                        temp_bureauresponse += xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-FACTORS").InnerText;
                    }
                    LogForAuditHighmark("Bureau Comments with Score Factors Obtained..!", bureau_type, contact_gid);
                }
                ObjMdlHighmarkResponse.bureau_response = temp_bureauresponse;


                    fileName = xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/PRINTABLE-REPORT/FILE-NAME").InnerText;

                    //fileName = fileName.Replace(".html", ".pdf");

                    XmlCDataSection cDataNode = (XmlCDataSection)(xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/PRINTABLE-REPORT/CONTENT").ChildNodes[0]);

                    finalData = cDataNode.Data;

                bureauLogResponse.bureau_score = ObjMdlHighmarkResponse.bureau_score;
                bureauLogResponse.bureau_response = ObjMdlHighmarkResponse.bureau_response;
                bureauLogResponse.document_content = finalData;
                LogHighmarkConsumerResponse(contact_gid, employee_gid, bureauLogResponse, successStatus);

                //PDFConversionCodeStart

                Spire.Pdf.PdfDocument pdf = new PdfDocument();

                PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();

                htmlLayoutFormat.IsWaiting = false;

                PdfPageSettings setting = new PdfPageSettings();

                setting.Size = Spire.Pdf.PdfPageSize.A3;

                Thread thread = new Thread(() =>

                {
                    pdf.LoadFromHTML(finalData, false, setting, htmlLayoutFormat);
                });

                thread.SetApartmentState(ApartmentState.STA);

                thread.Start();

                thread.Join();

                MemoryStream ms = new MemoryStream();
                pdf.SaveToStream(ms);



                //OriginalCode
                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                        lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                        string lsreportabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        {
                            if ((!System.IO.Directory.Exists(lsreportabspath)))
                                System.IO.Directory.CreateDirectory(lsreportabspath);
                        }
                        
                        lsreportabspath = lsreportabspath + fileName;

                string lsreportrelpath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                //lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CallProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //bool status;
                //status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                //ms.Close();
                //lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                string lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", fetchCompanyCode() + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + fileName, ms);
                ms.Close();
                lspath = "erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                LogForAuditHighmark("Bureau Report File Contents Converted to PDF file..!", bureau_type, contact_gid);


                msGetGid = objcmnfunctions.GetMasterGID("IDCU");
                msSQL = " insert into agr_mst_tindividual2cicdocumentupload( " +
                            " individual2cicdocumentupload_gid, " +
                            " contact2bureau_gid," +
                            " contact_gid," +
                            " cicdocument_name ," +
                            " cicdocument_path," +
                            " document_content," +
                            " created_by," +
                            " created_date" +
                            " )values(" +
                            "'" + msGetGid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + fileName + "'," +
                            "'" + lsreportrelpath + fileName + "'," +
                            "'" + finalData.Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                ObjMdlHighmarkResponse.status = true;
                
            }
            catch (Exception ex)
            {
                ObjMdlHighmarkResponse.status = false;
                ObjMdlHighmarkResponse.message = ex.ToString();

                msSQL = " insert into agr_trn_texceptionlog( " +
                                  " exception_message_name ," +
                                  " created_by," +
                                  " created_date" +
                                  " )values(" +
                                  "'" + ex.ToString().Replace("'","") + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            } 
            return ObjMdlHighmarkResponse;
        }

        public void DaGetHighmarkHTMLContent(string tmpcicdocument_gid, MdlHighmarkResponse values)
        {
            try
            {
                msSQL = "select document_content from agr_mst_tindividual2cicdocumentupload" +
                    " where individual2cicdocumentupload_gid='" + tmpcicdocument_gid + "'";
                values.html_content = objdbconn.GetExecuteScalar(msSQL);

                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public MdlTransUnionResponse DaGetTransUnionConsumerCreditInfo(string employee_gid, string contact_gid)
        {
            MdlTransUnionResponse ObjMdlTransUnionResponse = new MdlTransUnionResponse();
            try
            {
                TransUnionConsumer transUnionConsumer = new TransUnionConsumer();
                bureau_type = "Consumer";
                LogForAuditSATransUnion("Logging started for TransUnion Consumer with Contact GID - " + contact_gid + "at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), bureau_type, contact_gid);
                msSQL = " select first_name,middle_name,last_name,pan_no,individual_dob,gender_name from agr_mst_tcontact where " +
                         " contact_gid='" + contact_gid + "'";
                  objODBCDatareader = objdbconn.GetDataReader(msSQL);
                  if (objODBCDatareader.HasRows == true)
                  {
                    transUnionConsumer.name = objODBCDatareader["first_name"].ToString() + " " + objODBCDatareader["middle_name"].ToString() + " " + objODBCDatareader["last_name"].ToString();
                    transUnionConsumer.pan = objODBCDatareader["pan_no"].ToString();
                    transUnionConsumer.dob = objODBCDatareader["individual_dob"].ToString().Replace("-", string.Empty);
                    transUnionConsumer.gender_name = objODBCDatareader["gender_name"].ToString().Replace("-", string.Empty);
                  }

                  msSQL = " select mobile_no from agr_mst_tcontact2mobileno where " +
                         " contact_gid='" + contact_gid + "' and primary_Status='Yes'";
                transUnionConsumer.telephone_no = objdbconn.GetExecuteScalar(msSQL);

                  msSQL = " select addressline1,addressline2,city,postal_code,state from agr_mst_tcontact2address where " +
                         " contact_gid='" + contact_gid + "' and primary_status = 'Yes'";
                  objODBCDatareader = objdbconn.GetDataReader(msSQL);

                  if (objODBCDatareader.HasRows == true)
                  {
                    transUnionConsumer.addressline1 = objODBCDatareader["addressline1"].ToString();
                    transUnionConsumer.addressline2 = objODBCDatareader["addressline2"].ToString().Replace("-", string.Empty);
                    transUnionConsumer.city = objODBCDatareader["city"].ToString();
                    transUnionConsumer.postal_code = objODBCDatareader["postal_code"].ToString().Replace("-", string.Empty);
                    transUnionConsumer.state = objODBCDatareader["state"].ToString().Replace("-", string.Empty);
                  }

                  msSQL = " select state_code from agr_mst_ttransunionstatecode where " +
                         " state='" + transUnionConsumer.state + "'";
                transUnionConsumer.state_code = objdbconn.GetExecuteScalar(msSQL);

                  msSQL = " select gender_code from agr_mst_ttransuniongendercode where " +
                        " gender='" + transUnionConsumer.gender_name + "'";
                transUnionConsumer.gender_code = objdbconn.GetExecuteScalar(msSQL);

                if (TransUnionConsumerValidator(transUnionConsumer) == false)
                {
                    ObjMdlTransUnionResponse.status = false;
                    ObjMdlTransUnionResponse.message = validation_message;
                    LogForAuditSATransUnion("Input fields not validated - " + validation_message, bureau_type, contact_gid);
                    return ObjMdlTransUnionResponse;
                }

                LogForAuditSATransUnion("Input Fields validated..!", bureau_type, contact_gid);

                id_type = "01";
                  telephone_type = "01";
                  residence_type = "01";
                  address_type = "01";


                  templatePath = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateRequestTransUnionConsumer.xml";



                  XmlDocument xmlDoc = new XmlDocument();
                  xmlDoc.Load(templatePath); 
                 

                  string xmlreqContents = xmlDoc.InnerXml;

                  xmlreqContents = xmlreqContents.Replace("<ApplicantFirstName></ApplicantFirstName>", "<ApplicantFirstName>" + transUnionConsumer.name + "</ApplicantFirstName>");
                  xmlreqContents = xmlreqContents.Replace("<DateOfBirth></DateOfBirth>", "<DateOfBirth>" + transUnionConsumer.dob + "</DateOfBirth>");
                  xmlreqContents = xmlreqContents.Replace("<Gender></Gender>", "<Gender>" + transUnionConsumer.gender_code + "</Gender>");

                  xmlreqContents = xmlreqContents.Replace("<IdNumber></IdNumber>", "<IdNumber>" + transUnionConsumer.pan + "</IdNumber>");
                  xmlreqContents = xmlreqContents.Replace("<IdType></IdType>", "<IdType>" + id_type + "</IdType>");

                  xmlreqContents = xmlreqContents.Replace("<TelephoneNumber></TelephoneNumber>", "<TelephoneNumber>" + transUnionConsumer.telephone_no + "</TelephoneNumber>");
                  xmlreqContents = xmlreqContents.Replace("<TelephoneType></TelephoneType>", "<TelephoneType>" + telephone_type + "</TelephoneType>");

                  xmlreqContents = xmlreqContents.Replace("<AddressLine1></AddressLine1>", "<AddressLine1>" + transUnionConsumer.addressline1 + "</AddressLine1>");
                  xmlreqContents = xmlreqContents.Replace("<AddressLine2></AddressLine2>", "<AddressLine2>" + transUnionConsumer.addressline2 + "</AddressLine2>");
                  xmlreqContents = xmlreqContents.Replace("<AddressType></AddressType>", "<AddressType>" + address_type + "</AddressType>");

                  xmlreqContents = xmlreqContents.Replace("<City></City>", "<City>" + transUnionConsumer.city + "</City>");
                  xmlreqContents = xmlreqContents.Replace("<PinCode></PinCode>", "<PinCode>" + transUnionConsumer.postal_code + "</PinCode>");
                  xmlreqContents = xmlreqContents.Replace("<ResidenceType></ResidenceType>", "<ResidenceType>" + residence_type + "</ResidenceType>");
                  xmlreqContents = xmlreqContents.Replace("<StateCode></StateCode>", "<StateCode>" + transUnionConsumer.state_code + "</StateCode>");

                LogForAuditSATransUnion("XML Request Contents obtained - " + xmlreqContents + " - End of XML Request Contents", bureau_type, contact_gid);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


                  TransUnionService.ExternalSolutionExecutionService client = new TransUnionService.ExternalSolutionExecutionService();


                                 string xmlResponse = client.ExecuteXMLString(xmlreqContents);

                LogForAuditSATransUnion("Response obtained for the Bureau Request..!", bureau_type, contact_gid);
                LogForAuditSATransUnion(xmlResponse, bureau_type, contact_gid);
                LogForAuditSATransUnion("End of Response..!", bureau_type, contact_gid);

                string pattern = @"Score&amp;gt;[-0-9]{5}";

                                Match m = Regex.Match(xmlResponse, pattern, RegexOptions.IgnoreCase);
                                if (m.Success)
                                {
                                    ObjMdlTransUnionResponse.bureau_score = m.Value.Substring(m.Value.Length - 3);
                                    LogForAuditSATransUnion("Bureau Score obtained- " + ObjMdlTransUnionResponse.bureau_score , bureau_type, contact_gid);
                                    if (ObjMdlTransUnionResponse.bureau_score == "0-1")
                                    { 
                                        ObjMdlTransUnionResponse.bureau_score = "-1";
                                        ObjMdlTransUnionResponse.status = false;
                                        ObjMdlTransUnionResponse.message = "Consumer not in CIBIL Database or history older than 36 months!";
                                        LogForAuditSATransUnion("Consumer not in CIBIL Database or history older than 36 months!", bureau_type, contact_gid);
                                    }
                                }
                                else
                                {
                                    ObjMdlTransUnionResponse.status = false;
                                    ObjMdlTransUnionResponse.message = "Bureau score was not found in the response..!";
                                    LogForAuditSATransUnion("Bureau Score not found in the Response..!", bureau_type, contact_gid);
                                }



                                string patternappid = @"ApplicationId&gt;[0-9]{8}&lt;/ApplicationId";

                                 Match mappid = Regex.Match(xmlResponse, patternappid, RegexOptions.IgnoreCase);
                                 if (mappid.Success)
                                 {
                                     application_id = mappid.Value.Substring(17,8);
                                 }

                                 string patterndocid = @"&lt;Id&gt;[0-9]{7}&lt;/Id&gt;";

                                 Match mdocid = Regex.Match(xmlResponse, patterndocid, RegexOptions.IgnoreCase);
                                 if (mdocid.Success)
                                 {
                                     document_id = mdocid.Value.Substring(10, 7);
                                 }

                                if (mappid.Success == false || mdocid.Success == false)
                                {
                                    ObjMdlTransUnionResponse.status = false;
                                    ObjMdlTransUnionResponse.message = "Application ID/Document ID was not found in the response..!";
                                    LogForAuditSATransUnion("Application ID/Document ID not found in the Response..!", bureau_type, contact_gid);
                                    return ObjMdlTransUnionResponse;
                                }
                                else
                                {
                                    LogForAuditSATransUnion("Application ID and Document ID were found in the Response..!", bureau_type, contact_gid);
                                }

                                templatePathDoc = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateRequestDocTransUnionConsumer.xml";

                                 XmlDocument xmlDoc2 = new XmlDocument();
                                 xmlDoc2.Load(templatePathDoc);

                                 string xmlreqDocContents = xmlDoc2.InnerXml;

                                 xmlreqDocContents = xmlreqDocContents.Replace("<ApplicationId></ApplicationId>", "<ApplicationId>" + application_id + "</ApplicationId>");
                                 xmlreqDocContents = xmlreqDocContents.Replace("<DocumentId></DocumentId>", "<DocumentId>" + document_id + "</DocumentId>");
                                

                                 DownloadDocumentResponseContract docContent = client.DownloadDocument(xmlreqDocContents);

                                 MemoryStream ms = new MemoryStream(docContent.FileContent);

                                if (docContent.FileContent != null)
                                {
                                    LogForAuditSATransUnion("Report File Content obtained in Response..!", bureau_type, contact_gid);
                                }
                                else
                                {
                                    ObjMdlTransUnionResponse.status = false;
                                    ObjMdlTransUnionResponse.message = "Report File Content was not obtained in the Response..!";
                                    LogForAuditSATransUnion("Report File Content was not obtained in the Response..!", bureau_type, contact_gid);
                                    return ObjMdlTransUnionResponse;
                                }

                                 msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                                 lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                                 string lsreportabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/TransUnionReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                                 {
                                     if ((!System.IO.Directory.Exists(lsreportabspath)))
                                         System.IO.Directory.CreateDirectory(lsreportabspath);
                                 }
                                 fileName = "TransUnionReport"  + application_id + ".xml";
                                 lsreportabspath = lsreportabspath + fileName;

                                 FileStream file = new FileStream(lsreportabspath, FileMode.Create, FileAccess.Write);
                                 ms.WriteTo(file);
                                 file.Close();

                                string lsreportrelpath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/TransUnionReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                                XmlDocument xmlResponseDoc = new XmlDocument();

                                 using (XmlReader reader = XmlReader.Create(new StreamReader(ms, Encoding.UTF8)))
                                 {
                                     xmlResponseDoc.Load(reader);
                                 }



                                 string xmlresponseContents = xmlResponseDoc.InnerXml;

                                 ms.Close();

                                 msGetGid = objcmnfunctions.GetMasterGID("IDCU");
                                 msSQL = " insert into agr_mst_tindividual2cicdocumentupload( " +
                                             " individual2cicdocumentupload_gid, " +
                                             " contact2bureau_gid," +
                                             " contact_gid," +
                                             " cicdocument_name ," +
                                             " cicdocument_path," +
                                             " document_content," +
                                             " created_by," +
                                             " created_date" +
                                             " )values(" +
                                             "'" + msGetGid + "'," +
                                             "'" + employee_gid + "'," +
                                             "'" + employee_gid + "'," +
                                             "'" + fileName + "'," +
                                             "'" + lsreportrelpath + fileName + "'," +
                                             "'" + xmlresponseContents.Replace("'", "") + "'," +
                                             "'" + employee_gid + "'," +
                                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                 mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
       

                

                 noofphones_rep3months = Regex.Match(xmlResponse, "&lt;Id&gt;SPD020P001DM201S&lt;/Id&gt;\r\n&lt;Value&gt;([0-9]+)&lt;/Value&gt;\r\n").Groups[1].Value;
                 noofaddresses_rep3months = Regex.Match(xmlResponse, "&lt;Id&gt;SPD020P001DM211S&lt;/Id&gt;\r\n&lt;Value&gt;([0-9]+)&lt;/Value&gt;\r\n").Groups[1].Value;
                 noofdistphones_rep3months = Regex.Match(xmlResponse, "&lt;Id&gt;SPD020P001DM207E&lt;/Id&gt;\r\n&lt;Value&gt;([0-9]+)&lt;/Value&gt;\r\n").Groups[1].Value;
                 noofdistddresses_rep3months = Regex.Match(xmlResponse, "&lt;Id&gt;SPD020P001DM217E&lt;/Id&gt;\r\n&lt;Value&gt;([0-9]+)&lt;/Value&gt;\r\n").Groups[1].Value;
                 noofdistids_rep3months = Regex.Match(xmlResponse, "&lt;Id&gt;SPD020P001DM247S&lt;/Id&gt;\r\n&lt;Value&gt;([0-9]+)&lt;/Value&gt;\r\n").Groups[1].Value;
                 noofdistpincodes_rep3months = Regex.Match(xmlResponse, "&lt;Id&gt;SPD020P001DM257S&lt;/Id&gt;\r\n&lt;Value&gt;([0-9]+)&lt;/Value&gt;\r\n").Groups[1].Value;
                 enqdifflenders_30days = Regex.Match(xmlResponse, "&lt;Id&gt;SPD020P001INQ_VEL_6&lt;/Id&gt;\r\n&lt;Value&gt;([0-9]+[.][0-9]{2})&lt;/Value&gt;\r\n").Groups[1].Value;
                 newloansopened_30days = Regex.Match(xmlResponse, "&lt;Id&gt;SPD020P001TRD_VEL_1&lt;/Id&gt;\r\n&lt;Value&gt;([0-9]+[.][0-9]{2})&lt;/Value&gt;\r\n").Groups[1].Value;
                 distunsecuredenq_3months = Regex.Match(xmlResponse, "&lt;Id&gt;SPD020P002A169&lt;/Id&gt;\r\n&lt;Value&gt;([0-9]+)&lt;/Value&gt;\r\n").Groups[1].Value;
                 ranksegment_hml = Regex.Match(xmlResponse, "&lt;HRARuleAttributes&gt;\r\n&lt;Attribute&gt;\r\n&lt;Value&gt;([A-Za-z]+)&lt;/Value&gt;\r\n&lt;Id&gt;RankSegment&lt;/Id&gt;").Groups[1].Value;

                


                msGetGid = objcmnfunctions.GetMasterGID("C2HA");
                msSQL = " insert into agr_mst_tcontact2tuhighriskalert( " +
                            " contact2tuhighriskalert_gid, " +
                            " contact2bureau_gid," +
                            " contact_gid," +
                            " noofph_rep3mon ," +
                            " noofad_rep3mon," +
                            " noofdistph_rep3mon ," +
                            " noofdistad_rep3mon," +
                            " noofdistid_rep3mon ," +
                            " noofdistpin_3mon," +
                            " enqdifflend_30days ," +
                            " newloanopened_30days," +
                            " distunsecenq_3mon ," +
                            " ranksegment_hml," +
                            " created_by," +
                            " created_date" +
                            " )values(" +
                            "'" + msGetGid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + noofphones_rep3months + "'," +
                            "'" + noofaddresses_rep3months + "'," +
                            "'" + noofdistphones_rep3months + "'," +
                            "'" + noofdistddresses_rep3months + "'," +
                            "'" + noofdistids_rep3months + "'," +
                            "'" + noofdistpincodes_rep3months + "'," +
                            "'" + enqdifflenders_30days + "'," +
                            "'" + newloansopened_30days + "'," +
                            "'" + distunsecuredenq_3months + "'," +
                            "'" + ranksegment_hml + "'," +                        
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResultHRA = objdbconn.ExecuteNonQuerySQL(msSQL);




                ObjMdlTransUnionResponse.status = true;
                ObjMdlTransUnionResponse.message = "TransUnion details generation was successful";
                LogForAuditSATransUnion("Logging ended for TransUnion Consumer..!", bureau_type, contact_gid);
                LogForAuditSATransUnion("", bureau_type, contact_gid);

            }
            catch (Exception ex)
            {
                ObjMdlTransUnionResponse.status = false;
                ObjMdlTransUnionResponse.message = "Transunion details generation failed";
                LogForAuditSATransUnion("Exception occured while enerating details - " + ex.ToString(), bureau_type, contact_gid);
                LogForAuditSATransUnion("Logging ended for TransUnion Consumer..!", bureau_type, contact_gid);
                LogForAuditSATransUnion("", bureau_type, contact_gid);
                ObjMdlTransUnionResponse.message = ObjMdlTransUnionResponse.message.Replace("'", "");

                msSQL = " insert into agr_trn_texceptionlog( " +
                            " exception_message_name," +
                            " created_by," +
                            " created_date" +
                            " )values(" +
                            "'" + ex.ToString().Replace("'","\\'") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            return ObjMdlTransUnionResponse;
        }

        public void DaGetTransUnionXMLContent(string tmpcicdocument_gid, MdlTransUnionResponse values)
        {
            try
            {
                msSQL = "select document_content from agr_mst_tindividual2cicdocumentupload" +
                    " where individual2cicdocumentupload_gid='" + tmpcicdocument_gid + "'";
                values.xml_content = objdbconn.GetExecuteScalar(msSQL);

                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public MdlHighmarkResponse DaGetHighmarkInstitutionCreditInfo(string employee_gid, string institution_gid)
        {
            MdlHighmarkResponse ObjMdlHighmarkResponse = new MdlHighmarkResponse();
            bureau_type = "Commercial";
            BureauLogResponse bureauLogResponse = new BureauLogResponse();
            try
            {
                HighmarkCommercial highmarkCommercial = new HighmarkCommercial();

                LogForAuditHighmark("Logging started for Highmark Commercial with Institution GID - " + institution_gid + "at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), bureau_type, institution_gid);


                msSQL = " select company_name,companypan_no from agr_mst_tinstitution where " +
                       " institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    highmarkCommercial.company_name = objODBCDatareader["company_name"].ToString();
                    highmarkCommercial.companypan_no = objODBCDatareader["companypan_no"].ToString();
                }

                msSQL = " select mobile_no from agr_mst_tinstitution2mobileno where " +
                         " institution_gid='" + institution_gid + "' and primary_status='Yes'";
                highmarkCommercial.mobile_no = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select addressline1,addressline2,city,state,postal_code from agr_mst_tinstitution2address where " +
                           " institution_gid='" + institution_gid + "' and primary_status='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    highmarkCommercial.address = objODBCDatareader["addressline1"].ToString() + objODBCDatareader["addressline2"].ToString();
                    highmarkCommercial.city = objODBCDatareader["city"].ToString();
                    highmarkCommercial.statetemp = objODBCDatareader["state"].ToString();
                    highmarkCommercial.pin = objODBCDatareader["postal_code"].ToString();
                }
                msSQL = " select state_code from agr_mst_thighmarkstatecode where " +
                          " state='" + highmarkCommercial.statetemp + "'";
                highmarkCommercial.state = objdbconn.GetExecuteScalar(msSQL);

                if (HighmarkCommercialValidator(highmarkCommercial) == false)
                {
                    ObjMdlHighmarkResponse.status = false;
                    ObjMdlHighmarkResponse.message = validation_message;
                    bureauLogResponse.failure_reason = ObjMdlHighmarkResponse.message;
                    LogHighmarkCommercialResponse(institution_gid, employee_gid, bureauLogResponse, failedStatus);
                    LogForAuditHighmark("Input fields not validated - " + validation_message, bureau_type, institution_gid);
                    return ObjMdlHighmarkResponse;
                }

                LogForAuditHighmark("Input fields validated - " + highmarkCommercial.ToString() + " - End of input fields", bureau_type, institution_gid);

                templatePath = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateRequestHighMarkInstitution.xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(templatePath);

                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-APPLICANT-SEGMENT/BORROWER-NAME").InnerText = highmarkCommercial.company_name.ToUpper();
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-APPLICANT-SEGMENT/IDS/ID/VALUE").InnerText = highmarkCommercial.companypan_no;

                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-APPLICANT-SEGMENT/PHONES/PHONE/TELE-NO").InnerText = highmarkCommercial.mobile_no;

                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-ADDRESS-SEGMENT/ADDRESS/ADDRESS-LINE").InnerText = highmarkCommercial.address;
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-ADDRESS-SEGMENT/ADDRESS/CITY").InnerText = highmarkCommercial.city;
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-ADDRESS-SEGMENT/ADDRESS/STATE").InnerText = highmarkCommercial.state;
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-ADDRESS-SEGMENT/ADDRESS/PIN").InnerText = highmarkCommercial.pin;



                string xmlreqContents = xmlDoc.InnerXml;

                LogForAuditHighmark("XML Req Contents obtained - " + xmlreqContents + " - End of XML Req Contents", bureau_type, institution_gid);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                string requestUri = ConfigurationManager.AppSettings["highmarkinstitutionurl"].ToString();
                string xmlAckResponse = string.Empty;

                var client = new RestClient(requestUri);
                var request = new RestRequest(Method.POST);
                request.AddHeader("inquiryXML", xmlreqContents);
                request.AddHeader("userId", ConfigurationManager.AppSettings["highmarkinstitutionuserId"].ToString());
                request.AddHeader("password", ConfigurationManager.AppSettings["highmarkinstitutionpassword"].ToString());


                IRestResponse response = client.Execute(request);
                Thread.Sleep(5000);

                xmlAckResponse = response.Content;

                LogForAuditHighmark("XML Acknowledgement Response obtained - " + xmlAckResponse + " - End of XML Acknowledgement Response", bureau_type, institution_gid);

                XmlDocument xmlAckResponseDoc = new XmlDocument();
                xmlAckResponseDoc.LoadXml(xmlAckResponse);

                if (xmlAckResponseDoc.SelectSingleNode("/REPORT-FILE/INQUIRY-STATUS/INQUIRY/RESPONSE-TYPE").InnerText == "ERROR")
                {
                    ObjMdlHighmarkResponse.status = false;
                    ObjMdlHighmarkResponse.message = "Error in Acknowledgement of HighMark Request since '" + xmlAckResponseDoc.SelectSingleNode("/REPORT-FILE/INQUIRY-STATUS/INQUIRY/ERRORS/ERROR/DESCRIPTION").InnerText + "'";
                    bureauLogResponse.failure_reason = ObjMdlHighmarkResponse.message;
                    LogHighmarkCommercialResponse(institution_gid, employee_gid, bureauLogResponse, failedStatus);
                    LogForAuditHighmark("Error in XML Acknowledgement message - " + xmlAckResponseDoc.SelectSingleNode("/REPORT-FILE/INQUIRY-STATUS/INQUIRY/ERRORS/ERROR/DESCRIPTION").InnerText + " - End of XML Acknowledgement Error", bureau_type, institution_gid);
                    return ObjMdlHighmarkResponse;
                }
                else
                {
                    LogForAuditHighmark("XML Acknowledgement received successfully", bureau_type, institution_gid);
                    report_id = xmlAckResponseDoc.SelectSingleNode("/REPORT-FILE/INQUIRY-STATUS/INQUIRY/REPORT-ID").InnerText;
                    templateIssuePath = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateIssueHighmarkInstitution.xml";


                    XmlDocument xmlIssueDoc = new XmlDocument();
                    xmlIssueDoc.Load(templateIssuePath);

                    xmlIssueDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/REPORT-ID").InnerText = report_id;

                    string xmlIssueContents = xmlIssueDoc.InnerXml;

                    LogForAuditHighmark("XML Issue Contents obtained - " + xmlIssueContents + " - End of XML Issue Contents", bureau_type, institution_gid);


                    string xmlFinalResponse = string.Empty;

                    var clientIssue = new RestClient(requestUri);
                    var requestIssue = new RestRequest(Method.POST);
                    requestIssue.AddHeader("inquiryXML", xmlIssueContents);
                    requestIssue.AddHeader("userId", ConfigurationManager.AppSettings["highmarkinstitutionuserId"].ToString());
                    requestIssue.AddHeader("password", ConfigurationManager.AppSettings["highmarkinstitutionpassword"].ToString());


                    IRestResponse responseIssue = clientIssue.Execute(requestIssue);

                    Thread.Sleep(5000);

                    xmlFinalResponse = responseIssue.Content;

                    LogForAuditHighmark("XML Final Response obtained - " + xmlFinalResponse + " - End of XML Final Response", bureau_type, institution_gid);



                    XmlDocument xmlFinalResponseDoc = new XmlDocument();
                    xmlFinalResponseDoc.LoadXml(xmlFinalResponse);

                    int inprocessCount = 0;

                    if (xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/INQUIRY-STATUS/INQUIRY/RESPONSE-TYPE") != null)
                    {
                        if (xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/INQUIRY-STATUS/INQUIRY/RESPONSE-TYPE").InnerText == "INPROCESS")
                        {

                            do
                            {
                                Thread.Sleep(10000);

                                string xmlFinalResponseInProcess = string.Empty;

                                var clientIssueInProcess = new RestClient(requestUri);
                                var requestIssueInProcess = new RestRequest(Method.POST);
                                requestIssueInProcess.AddHeader("inquiryXML", xmlIssueContents);
                                requestIssueInProcess.AddHeader("userId", ConfigurationManager.AppSettings["highmarkinstitutionuserId"].ToString());
                                requestIssueInProcess.AddHeader("password", ConfigurationManager.AppSettings["highmarkinstitutionpassword"].ToString());


                                IRestResponse responseIssueInProcess = clientIssueInProcess.Execute(requestIssueInProcess);

                                Thread.Sleep(5000);

                                xmlFinalResponseInProcess = responseIssueInProcess.Content;

                                LogForAuditHighmark("XML Final Response for In Process count " + inprocessCount.ToString() + " obtained - " + xmlFinalResponseInProcess + " - End of XML Final Response", bureau_type, institution_gid);

                                xmlFinalResponseDoc.LoadXml(xmlFinalResponseInProcess);

                                if (xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/INQUIRY-STATUS/INQUIRY/RESPONSE-TYPE") != null)
                                {
                                    if (xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/INQUIRY-STATUS/INQUIRY/RESPONSE-TYPE").InnerText == "INPROCESS")
                                    {
                                        inprocessCount++;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            } while (inprocessCount < 3);

                        }
                    }

                    if (inprocessCount == 3)
                    {
                        ObjMdlHighmarkResponse.status = false;
                        ObjMdlHighmarkResponse.message = "Bureau Score was not obtained after retrying multiple times." + Environment.NewLine + "Response from Highmark: " + xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/INQUIRY-STATUS/INQUIRY/DESCRIPTION").InnerText;
                        LogForAuditHighmark("Bureau Score was not obtained after retrying multiple times." + Environment.NewLine + "Response from Highmark: " + xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/INQUIRY-STATUS/INQUIRY/DESCRIPTION").InnerText, bureau_type, institution_gid);
                        return ObjMdlHighmarkResponse;
                    }


                    if (xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/COMMERCIAL-REPORT/SCORES/SCORE/SCORE-VALUE") == null)
                    {
                        ObjMdlHighmarkResponse.status = false;
                        ObjMdlHighmarkResponse.message = "Bureau Score was not obtained in the Response..!";
                        bureauLogResponse.failure_reason = ObjMdlHighmarkResponse.message;
                        LogHighmarkCommercialResponse(institution_gid, employee_gid, bureauLogResponse, failedStatus);
                        LogForAuditHighmark("Bureau Score Not Obtained in Response..!", bureau_type, institution_gid);
                        return ObjMdlHighmarkResponse;
                    }
                    else
                    {
                        ObjMdlHighmarkResponse.bureau_score = xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/COMMERCIAL-REPORT/SCORES/SCORE/SCORE-VALUE").InnerText;
                        LogForAuditHighmark("Bureau Score Obtained in Response..!", bureau_type, institution_gid);
                    }

                    if (xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/COMMERCIAL-REPORT/SCORES/SCORE/SCORE-COMMENTS") == null && xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/COMMERCIAL-REPORT/SCORES/SCORE/SCORE-FACTORS") == null)
                    {
                        ObjMdlHighmarkResponse.status = false;
                        ObjMdlHighmarkResponse.message = "Bureau Comments was not obtained in the Response..!";
                        bureauLogResponse.failure_reason = ObjMdlHighmarkResponse.message;
                        LogHighmarkCommercialResponse(institution_gid, employee_gid, bureauLogResponse, failedStatus);
                        LogForAuditHighmark("Bureau Comments Not Obtained in the Response..!", bureau_type, institution_gid);
                        return ObjMdlHighmarkResponse;
                    }

                    if (!(xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/COMMERCIAL-REPORT/SCORES/SCORE/SCORE-COMMENTS") == null))
                    {
                        temp_bureauresponse += xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/COMMERCIAL-REPORT/SCORES/SCORE/SCORE-COMMENTS").InnerText;
                    }
                    if (!(xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/COMMERCIAL-REPORT/SCORES/SCORE/SCORE-FACTORS") == null))
                    {
                        if (temp_bureauresponse != "")
                        {
                            temp_bureauresponse = temp_bureauresponse + "; " + xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/COMMERCIAL-REPORT/SCORES/SCORE/SCORE-FACTORS").InnerText;
                        }
                        else
                        {
                            temp_bureauresponse += xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/COMMERCIAL-REPORT/SCORES/SCORE/SCORE-FACTORS").InnerText;
                        }
                    }
                    ObjMdlHighmarkResponse.bureau_response = temp_bureauresponse;

                    LogForAuditHighmark("Bureau Comments with Score Factors Obtained in Response..!", bureau_type, institution_gid);

                    if (xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/PRINTABLE-REPORT/CONTENT") == null)
                    {
                        ObjMdlHighmarkResponse.status = false;
                        ObjMdlHighmarkResponse.message = "Bureau Report File was not obtained in the Response..!";
                        bureauLogResponse.failure_reason = ObjMdlHighmarkResponse.message;
                        LogHighmarkCommercialResponse(institution_gid, employee_gid, bureauLogResponse, failedStatus);
                        LogForAuditHighmark("Bureau Report File Not Obtained in Response..!", bureau_type, institution_gid);
                        return ObjMdlHighmarkResponse;
                    }
                    else
                    {
                        XmlCDataSection cDataNode = (XmlCDataSection)(xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/PRINTABLE-REPORT/CONTENT").ChildNodes[0]);
                        finalResponseHtmlData = cDataNode.Data;
                        LogForAuditHighmark("Final Response HTML Data Obtained - " + finalResponseHtmlData + "- End of Final Response", bureau_type, institution_gid);
                        fileName = xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/PRINTABLE-REPORT/FILE-NAME").InnerText;
                        fileName = fileName.Replace(".html", ".pdf");
                        LogForAuditHighmark("Bureau Report File Obtained in Response..!", bureau_type, institution_gid);
                    }

                    bureauLogResponse.bureau_score = ObjMdlHighmarkResponse.bureau_score;
                    bureauLogResponse.bureau_response = ObjMdlHighmarkResponse.bureau_response;
                    bureauLogResponse.document_content = finalResponseHtmlData;
                    LogHighmarkCommercialResponse(institution_gid, employee_gid, bureauLogResponse, successStatus);

                    //PDFConversionCodeStart

                    Spire.Pdf.PdfDocument pdf = new PdfDocument();

                    PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();

                    htmlLayoutFormat.IsWaiting = false;

                    PdfPageSettings setting = new PdfPageSettings();

                    setting.Size = Spire.Pdf.PdfPageSize.A3;

                    Thread thread = new Thread(() =>

                    {
                        pdf.LoadFromHTML(finalResponseHtmlData, false, setting, htmlLayoutFormat);
                    });

                    thread.SetApartmentState(ApartmentState.STA);

                    thread.Start();

                    thread.Join();

                    MemoryStream ms = new MemoryStream();
                    pdf.SaveToStream(ms);

                    //PDFConversionCodeEnd

                    string lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                    if ((!System.IO.Directory.Exists(lspath)))
                        System.IO.Directory.CreateDirectory(lspath);

                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", fetchCompanyCode() + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + fileName, ms);
                    ms.Close();
                    lspath = "erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                    msGetGid = objcmnfunctions.GetMasterGID("INCU");
                    msSQL = " insert into agr_mst_tinstitution2cicdocumentupload( " +
                                " institution2cicdocumentupload_gid, " +
                                " institution2bureau_gid," +
                                " institution_gid," +
                                " cicdocument_name ," +
                                " cicdocument_path," +
                                " document_content," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + fileName.Replace("'", "") + "'," +
                                "'" + lspath + fileName.Replace("'", "") + "'," +
                                "'" + finalResponseHtmlData.Replace("'", "") + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    if (mnResult == 1)
                    {
                        LogForAuditHighmark("Bureau Report File Contents entered in DB Successfully..!", bureau_type, institution_gid);
                        ObjMdlHighmarkResponse.status = true;
                        ObjMdlHighmarkResponse.message = "Highmark Details generated successfully..!";
                    }
                    else
                    {
                        ObjMdlHighmarkResponse.status = false;
                        ObjMdlHighmarkResponse.message = "Error occured in inserting Bureau details into DB..!";
                        LogForAuditHighmark("Error occured in inserting Bureau details into DB..!", bureau_type, institution_gid);
                    }


                }
            }
            catch (Exception ex)
            {

                ObjMdlHighmarkResponse.status = false;
                ObjMdlHighmarkResponse.message = ex.Message;
                bureauLogResponse.failure_reason = ObjMdlHighmarkResponse.message;
                LogHighmarkCommercialResponse(institution_gid, employee_gid, bureauLogResponse, failedStatus);
                LogForAuditHighmark("Exception occured - ExString - " + ex.ToString() + " - End of Exception String", bureau_type, institution_gid);
                LogForAuditHighmark("Exception occured - ExStackTrace - " + ex.StackTrace + " - End of Exception StackTrace", bureau_type, institution_gid);
                LogForAuditHighmark("Logging ended for Highmark Commercial..!", bureau_type, institution_gid);
            }
            LogForAuditHighmark("Logging ended for Highmark Commercial..!", bureau_type, institution_gid);
            return ObjMdlHighmarkResponse;
        }

        public void DaGetHighmarkInstitutionHTMLContent(string tmpcicdocument_gid, MdlHighmarkResponse values)
        {
            try
            {
                msSQL = "select document_content from agr_mst_tinstitution2cicdocumentupload" +
                    " where institution2cicdocumentupload_gid='" + tmpcicdocument_gid + "'";
                values.html_content = objdbconn.GetExecuteScalar(msSQL);

                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetHighRiskAlertDetails(string contact2bureau_gid, string employee_gid, MdlHighRiskAlertDetails values)
        {
            try
            {
                msSQL = " select noofph_rep3mon,noofad_rep3mon,noofdistph_rep3mon,noofdistad_rep3mon,noofdistid_rep3mon,noofdistpin_3mon,enqdifflend_30days,newloanopened_30days," +
                        " distunsecenq_3mon,ranksegment_hml" +
                        " from agr_mst_tcontact2tuhighriskalert a" +
                        " where contact2bureau_gid='" + contact2bureau_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values.noofph_rep3mon = objODBCDatareader["noofph_rep3mon"].ToString();
                    values.noofad_rep3mon = objODBCDatareader["noofad_rep3mon"].ToString();
                    values.noofdistph_rep3mon = objODBCDatareader["noofdistph_rep3mon"].ToString();
                    values.noofdistad_rep3mon = objODBCDatareader["noofdistad_rep3mon"].ToString();
                    values.noofdistid_rep3mon = objODBCDatareader["noofdistid_rep3mon"].ToString();
                    values.noofdistpin_3mon = objODBCDatareader["noofdistpin_3mon"].ToString();
                    values.enqdifflend_30days = objODBCDatareader["enqdifflend_30days"].ToString();
                    values.newloanopened_30days = objODBCDatareader["newloanopened_30days"].ToString();
                    values.distunsecenq_3mon = objODBCDatareader["distunsecenq_3mon"].ToString();
                    values.ranksegment_hml = objODBCDatareader["ranksegment_hml"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch
            {
                values.status = false;
                values.message = "failure";
            }


        }


        //Auxillary Functions
        public bool HighmarkCommercialValidator(HighmarkCommercial highmarkCommercial)
        {
            if (String.IsNullOrEmpty(highmarkCommercial.company_name))
            {
                validation_message = "Company Name field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkCommercial.companypan_no))
            {
                validation_message = "Company PAN field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkCommercial.mobile_no))
            {
                validation_message = "Mobile number field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkCommercial.address))
            {
                validation_message = "Address field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkCommercial.city))
            {
                validation_message = "City field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkCommercial.statetemp))
            {
                validation_message = "State Field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkCommercial.state))
            {
                validation_message = "State Code Field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkCommercial.pin))
            {
                validation_message = "PIN field is empty..!";
                return false;
            }
            else
            {
                return true;
            }
        }
        public void LogForAudit(string strVal)
        {

            try
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erpdocument/SAMUNNATI/SamAgro/HighmarkAuditLog/";
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);

                lspath = lspath + "HighmarkInstitutionAuditLog.txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
                sw.WriteLine(strVal);
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void LogForAuditHighmark(string strVal, string type, string function_gid)
        {
            try
            {

                if (type == "Consumer")
                {

                    loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/HighmarkAuditLog/Consumer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + function_gid + "/";
                    if ((!System.IO.Directory.Exists(loglspath)))
                        System.IO.Directory.CreateDirectory(loglspath);
                    if (logreportFileName == "")
                    {
                        logreportFileName = function_gid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    }
                    loglspath = loglspath + logreportFileName;
                }
                else
                {
                    loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/HighmarkAuditLog/Commercial/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + function_gid + "/";
                    if ((!System.IO.Directory.Exists(loglspath)))
                        System.IO.Directory.CreateDirectory(loglspath);
                    if (logreportFileName == "")
                    {
                        logreportFileName = function_gid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    }
                    loglspath = loglspath + logreportFileName;
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(loglspath, true);
                sw.WriteLine(strVal);
                sw.Close();

            }
            catch (Exception ex)
            {
            }
        }

        public void LogHighmarkCommercialResponse(string institution_gid, string employee_gid, BureauLogResponse bureauLogResponse, string status)
        {
            try
            {
                if (status == "Failed")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AHCM");
                    msSQL = " insert into agr_trn_thighmarkcommerciallog( " +
                                " highmarkcommerciallog_gid, " +
                                " institution_gid," +
                                " request_status," +
                                " bureau_score ," +
                                " bureau_response," +
                                " document_content," +
                                " failure_reason," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + institution_gid + "'," +
                                "'" + "Failed" + "'," +
                                "'" + "-" + "'," +
                                "'" + "-" + "'," +
                                "'" + "-" + "'," +
                                "'" + bureauLogResponse.failure_reason + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AHCM");
                    msSQL = " insert into agr_trn_thighmarkcommerciallog( " +
                                " highmarkcommerciallog_gid, " +
                                " institution_gid," +
                                " request_status," +
                                " bureau_score ," +
                                " bureau_response," +
                                " document_content," +
                                " failure_reason," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + institution_gid + "'," +
                                "'" + "Success" + "'," +
                                "'" + bureauLogResponse.bureau_score + "'," +
                                "'" + bureauLogResponse.bureau_response + "'," +
                                "'" + bureauLogResponse.document_content.Replace("'", "") + "'," +
                                "'" + "-" + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            catch (Exception ex)
            {
                LogForAuditHighmark("Highmark DB Logging Exception - String - " + ex.ToString() + " - End of Exception", bureau_type, institution_gid);
            }
        }
        public string fetchCompanyCode()
        {
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            string lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            return lscompany_code;
        }

        public void LogForAuditSATransUnion(string strVal, string type, string function_gid)
        {
            try
            {
                if (type == "Consumer")
                {
                    loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "MasterSamAgro/TransUnionAuditLog/Consumer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + function_gid + "/";
                    if ((!System.IO.Directory.Exists(loglspath)))
                        System.IO.Directory.CreateDirectory(loglspath);
                    if (logreportFileName == "")
                    {
                        logreportFileName = function_gid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    }
                    loglspath = loglspath + logreportFileName;
                }
                else
                {
                    loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "MasterSamAgro/TransUnionAuditLog/Commercial/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + function_gid + "/";
                    if ((!System.IO.Directory.Exists(loglspath)))
                        System.IO.Directory.CreateDirectory(loglspath);
                    if (logreportFileName == "")
                    {
                        logreportFileName = function_gid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    }
                    loglspath = loglspath + logreportFileName;
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(loglspath, true);
                sw.WriteLine(strVal);
                sw.Close();

            }
            catch (Exception ex)
            {
            }
        }

        public bool TransUnionConsumerValidator(TransUnionConsumer transUnionConsumer)
        {
            if (String.IsNullOrEmpty(transUnionConsumer.name))
            {
                validation_message = "Name field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionConsumer.pan))
            {
                validation_message = "PAN field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionConsumer.dob))
            {
                validation_message = "Date of Birth field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionConsumer.gender_name))
            {
                validation_message = "Gender field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionConsumer.gender_code))
            {
                validation_message = "Gender Code field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionConsumer.telephone_no))
            {
                validation_message = "Mobile number field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionConsumer.addressline1))
            {
                validation_message = "Address Line1 Field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionConsumer.city))
            {
                validation_message = "City field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionConsumer.postal_code))
            {
                validation_message = "Postal Code field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionConsumer.state))
            {
                validation_message = "State field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionConsumer.state_code))
            {
                validation_message = "State Code field is empty..!";
                return false;
            }
            else
            {
                return true;
            }
        }

        //TransUnion Commercial

        public MdlTransUnionResponse DaGetTransUnionCommercialCreditInfo(string employee_gid, string institution_gid)
        {
            TransUnionCommercial transUnionCommercial = new TransUnionCommercial();
            MdlTransUnionResponse ObjMdlTransUnionResponse = new MdlTransUnionResponse();
            bureau_type = "Commercial";
            TransUnionCommService.ExternalSolutionExecutionService clientTransUnionComm = new TransUnionCommService.ExternalSolutionExecutionService();
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                LogForAuditSATransUnion("Logging started for TransUnion Commercial with GID - " + institution_gid + "at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), bureau_type, institution_gid);
                msSQL = " select company_name,companypan_no from agr_mst_tinstitution where " +
                       " institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    transUnionCommercial.company_name = objODBCDatareader["company_name"].ToString();
                    transUnionCommercial.companypan_no = objODBCDatareader["companypan_no"].ToString();
                }

                msSQL = " select addressline1,addressline2,city,postal_code,state from agr_mst_tinstitution2address where " +
                       " institution_gid='" + institution_gid + "' and primary_status = 'Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    transUnionCommercial.addressline1 = objODBCDatareader["addressline1"].ToString();
                    transUnionCommercial.addressline2 = objODBCDatareader["addressline2"].ToString().Replace("-", string.Empty);
                    transUnionCommercial.city = objODBCDatareader["city"].ToString();
                    transUnionCommercial.postal_code = objODBCDatareader["postal_code"].ToString().Replace("-", string.Empty);
                    transUnionCommercial.state = objODBCDatareader["state"].ToString().Replace("-", string.Empty);
                }

                msSQL = " select state_code from ocs_mst_ttransunionstatecode where " +
                       " state='" + transUnionCommercial.state + "'";
                transUnionCommercial.state_code = objdbconn.GetExecuteScalar(msSQL);

                if (TransUnionCommercialValidator(transUnionCommercial) == false)
                {
                    ObjMdlTransUnionResponse.status = false;
                    ObjMdlTransUnionResponse.message = validation_message;
                    LogForAuditSATransUnion("Input fields not validated - " + validation_message, bureau_type, institution_gid);
                    return ObjMdlTransUnionResponse;
                }
                LogForAuditSATransUnion("Input fields validated", bureau_type, institution_gid);
                templatePath = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateRequestTransUnionCommercial.xml";
                xmlDoc.Load(templatePath);
                string xmlreqContents = xmlDoc.InnerXml;
                LogForAuditSATransUnion("Template loaded successfully", bureau_type, institution_gid);
                xmlreqContents = xmlreqContents.Replace("&lt;CompanyName&gt;&lt;/CompanyName&gt;", "&lt;CompanyName&gt;" + transUnionCommercial.company_name + "&lt;/CompanyName&gt;");
                xmlreqContents = xmlreqContents.Replace("&lt;PAN&gt;&lt;/PAN&gt;", "&lt;PAN&gt;" + transUnionCommercial.companypan_no + "&lt;/PAN&gt;");

                xmlreqContents = xmlreqContents.Replace("&lt;AddressLine1&gt;&lt;/AddressLine1&gt;", "&lt;AddressLine1&gt;" + transUnionCommercial.addressline1 + "&lt;/AddressLine1&gt;");
                xmlreqContents = xmlreqContents.Replace("&lt;AddressLine2&gt;&lt;/AddressLine2&gt;", "&lt;AddressLine2&gt;" + transUnionCommercial.addressline2 + "&lt;/AddressLine2&gt;");
                xmlreqContents = xmlreqContents.Replace("&lt;AddressCity&gt;&lt;/AddressCity&gt;", "&lt;AddressCity&gt;" + transUnionCommercial.city + "&lt;/AddressCity&gt;");
                xmlreqContents = xmlreqContents.Replace("&lt;AddressStateCode&gt;&lt;/AddressStateCode&gt;", "&lt;AddressStateCode&gt;" + transUnionCommercial.state_code + "&lt;/AddressStateCode&gt;");
                xmlreqContents = xmlreqContents.Replace("&lt;AddressPincode&gt;&lt;/AddressPincode&gt;", "&lt;AddressPincode&gt;" + transUnionCommercial.postal_code + "&lt;/AddressPincode&gt;");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


                string xmlResponse = clientTransUnionComm.ExecuteXMLString(xmlreqContents);

                LogForAuditSATransUnion("Bureau Request: " + xmlreqContents, bureau_type, institution_gid);
                LogForAuditSATransUnion("Response obtained for the Bureau Request..!", bureau_type, institution_gid);
                LogForAuditSATransUnion(xmlResponse, bureau_type, institution_gid);
                LogForAuditSATransUnion("End of Response..!", bureau_type, institution_gid);

                string pattern = @"&amp;lt;rankValue&amp;gt;[a-zA-Z]{3}-[0-9]{1,2}";

                Match m = Regex.Match(xmlResponse, pattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    ObjMdlTransUnionResponse.bureau_score = m.Value.Substring(m.Value.Length - 6);
                    ObjMdlTransUnionResponse.bureau_score = ObjMdlTransUnionResponse.bureau_score.Replace(";", "");
                }
                else
                {
                    ObjMdlTransUnionResponse.bureau_score = "NA";
                }


                string patternappid = @"<ApplicationId>[0-9]{8}</ApplicationId>";

                Match mappid = Regex.Match(xmlResponse, patternappid, RegexOptions.IgnoreCase);
                if (mappid.Success)
                {
                    application_id = mappid.Value.Substring(15, 8);
                }
                else
                {
                    ObjMdlTransUnionResponse.status = false;
                    ObjMdlTransUnionResponse.message = "Error occured in generating TransUnion Commercial details since application ID could not be found in the response!";
                    return ObjMdlTransUnionResponse;
                }

                templatePath = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateDocMetaDataTransUnionCommercial.xml";
                xmlDoc.Load(templatePath);
                string xmlDocMetaDataContents = xmlDoc.InnerXml;
                LogForAuditSATransUnion("Document metadata Template loaded successfully", bureau_type, institution_gid);
                xmlDocMetaDataContents = xmlDocMetaDataContents.Replace("<ApplicationId></ApplicationId>", "<ApplicationId>" + application_id + "</ApplicationId>");

                string xmlMetaDataResponse = clientTransUnionComm.RetrieveDocumentMetaDataXMLString(xmlDocMetaDataContents);

                LogForAuditSATransUnion("Document metadata Request: " + xmlDocMetaDataContents, bureau_type, institution_gid);
                LogForAuditSATransUnion("Response obtained for the document meta data Request..!", bureau_type, institution_gid);
                LogForAuditSATransUnion(xmlMetaDataResponse, bureau_type, institution_gid);
                LogForAuditSATransUnion("End of Response..!", bureau_type, institution_gid);

                string patterndocid = @"<DocumentId>[0-9]{8}</DocumentId>";

                Match mdocid = Regex.Match(xmlMetaDataResponse, patterndocid, RegexOptions.IgnoreCase);
                if (mdocid.Success)
                {
                    document_id = mdocid.Value.Substring(12, 8);
                }
                else
                {
                    ObjMdlTransUnionResponse.status = false;
                    ObjMdlTransUnionResponse.message = "Error occured in generating TransUnion Commercial details since document ID could not be found in the response!";
                    return ObjMdlTransUnionResponse;
                }

                templatePath = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateDownloadDocTransUnionCommercial.xml";
                xmlDoc.Load(templatePath);
                string xmlDownloadDocContents = xmlDoc.InnerXml;
                LogForAuditSATransUnion("Document download Template loaded successfully", bureau_type, institution_gid);

                xmlDownloadDocContents = xmlDownloadDocContents.Replace("<ApplicationId></ApplicationId>", "<ApplicationId>" + application_id + "</ApplicationId>");
                xmlDownloadDocContents = xmlDownloadDocContents.Replace("<DocumentId></DocumentId>", "<DocumentId>" + document_id + "</DocumentId>");
                LogForAuditSATransUnion("Download Document Request: " + xmlDownloadDocContents, bureau_type, institution_gid);

                TransUnionCommService.DownloadDocumentResponseContract docContent = clientTransUnionComm.DownloadDocument(xmlDownloadDocContents);

                MemoryStream ms = new MemoryStream(docContent.FileContent);

                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                string lsreportabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/TransUnionCommercialReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                {
                    if ((!System.IO.Directory.Exists(lsreportabspath)))
                        System.IO.Directory.CreateDirectory(lsreportabspath);
                }
                fileName = "TransUnionCommReport" + document_id + ".xml";
                lsreportabspath = lsreportabspath + fileName;

                FileStream file = new FileStream(lsreportabspath, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();

                string lsreportrelpath = "../../erpdocument" + "/" + lscompany_code + "/" + "Master/TransUnionReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                XmlDocument xmlResponseDoc = new XmlDocument();

                using (XmlReader reader = XmlReader.Create(new StreamReader(ms, Encoding.UTF8)))
                {
                    xmlResponseDoc.Load(reader);
                }

                string xmlresponseContents = xmlResponseDoc.InnerXml;

                ms.Close();

                msGetGid = objcmnfunctions.GetMasterGID("IDCU");
                msSQL = " insert into agr_mst_tinstitution2cicdocumentupload( " +
                            " institution2cicdocumentupload_gid, " +
                            " institution2bureau_gid," +
                            " institution_gid," +
                            " cicdocument_name ," +
                            " cicdocument_path," +
                            " document_content," +
                            " created_by," +
                            " created_date" +
                            " )values(" +
                            "'" + msGetGid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + fileName.Replace("'", "") + "'," +
                            "'" + lsreportrelpath + fileName.Replace("'", "") + "'," +
                            "'" + xmlresponseContents.Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    ObjMdlTransUnionResponse.status = true;
                    ObjMdlTransUnionResponse.message = "TransUnion Commercial details generation was successful";
                    LogForAuditSATransUnion(ObjMdlTransUnionResponse.message, bureau_type, institution_gid);
                    LogForAuditSATransUnion("Logging ended", bureau_type, institution_gid);
                    return ObjMdlTransUnionResponse;
                }
                else
                {
                    ObjMdlTransUnionResponse.status = false;
                    ObjMdlTransUnionResponse.message = "Error occured in generating TransUnion Commercial details";
                    LogForAuditSATransUnion(ObjMdlTransUnionResponse.message, bureau_type, institution_gid);
                    LogForAuditSATransUnion("Logging ended", bureau_type, institution_gid);
                    return ObjMdlTransUnionResponse;
                }

            }
            catch (Exception ex)
            {
                ObjMdlTransUnionResponse.status = false;
                ObjMdlTransUnionResponse.message = "Transunion Commercial details generation failed";
                LogForAuditSATransUnion("Exception occured while generating TransUnion details: " + ex.ToString(), bureau_type, institution_gid);
                ObjMdlTransUnionResponse.message = ObjMdlTransUnionResponse.message.Replace("'", "");
                msSQL = " insert into agr_trn_texceptionlog( " +
                            " exception_message_name," +
                            " created_by," +
                            " created_date" +
                            " )values(" +
                            "'" + ex.ToString().Replace("'", "\\'") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                return ObjMdlTransUnionResponse;
            }

        }

        public bool TransUnionCommercialValidator(TransUnionCommercial transUnionCommercial)
        {
            if (String.IsNullOrEmpty(transUnionCommercial.company_name))
            {
                validation_message = "Company Name field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionCommercial.companypan_no))
            {
                validation_message = "Company PAN field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionCommercial.addressline1))
            {
                validation_message = "Address Line1 Field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionCommercial.city))
            {
                validation_message = "City field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionCommercial.postal_code))
            {
                validation_message = "Postal Code field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionCommercial.state))
            {
                validation_message = "State field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(transUnionCommercial.state_code))
            {
                validation_message = "State Code field is empty..!";
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DaGetTransunionInstitutionXMLContent(string tmpcicdocument_gid, MdlTransUnionResponse values)
        {
            try
            {
                msSQL = "select document_content from agr_mst_tinstitution2cicdocumentupload" +
                    " where institution2cicdocumentupload_gid='" + tmpcicdocument_gid + "'";
                values.xml_content = objdbconn.GetExecuteScalar(msSQL);

                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }


        //public void LogForAuditHighmark(string strVal, string type, string function_gid)
        //{
        //    try
        //    {

        //        if (type == "Consumer")
        //        {

        //            loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "Samagro/HighmarkAuditLog/Consumer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + function_gid + "/";
        //            if ((!System.IO.Directory.Exists(loglspath)))
        //                System.IO.Directory.CreateDirectory(loglspath);
        //            if (logreportFileName == "")
        //            {
        //                logreportFileName = function_gid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        //            }
        //            loglspath = loglspath + logreportFileName;
        //        }
        //        else
        //        {
        //            loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "Samagro/HighmarkAuditLog/Commercial/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + function_gid + "/";
        //            if ((!System.IO.Directory.Exists(loglspath)))
        //                System.IO.Directory.CreateDirectory(loglspath);
        //            if (logreportFileName == "")
        //            {
        //                logreportFileName = function_gid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        //            }
        //            loglspath = loglspath + logreportFileName;
        //        }

        //        System.IO.StreamWriter sw = new System.IO.StreamWriter(loglspath, true);
        //        sw.WriteLine(strVal);
        //        sw.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //Auxillary Functions

        public bool HighmarkConsumerValidator(HighmarkConsumer highmarkConsumer)
        {
            if (String.IsNullOrEmpty(highmarkConsumer.name))
            {
                validation_message = "Kindly fill Name of the Individual";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkConsumer.pan))
            {
                validation_message = "Kindly add PAN number";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkConsumer.dob))
            {
                validation_message = "Kindly enter Date of Birth";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkConsumer.address))
            {
                validation_message = "Kindly add Primary Address";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkConsumer.city))
            {
                validation_message = "Kindly enter City in Primary Address";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkConsumer.statetemp))
            {
                validation_message = "Kindly enter State in Primary Address";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkConsumer.state))
            {
                validation_message = "State Code field is empty..!";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkConsumer.pin))
            {
                validation_message = "Kindly enter PIN for Primary Address";
                return false;
            }
            else if (String.IsNullOrEmpty(highmarkConsumer.phone_number))
            {
                validation_message = "Kindly add Primary Mobile number";
                return false;
            }
            else
            {
                return true;
            }
        }

        public void LogHighmarkConsumerResponse(string contact_gid, string employee_gid, BureauLogResponse bureauLogResponse, string status)
        {
            try
            {
                if (status == "Failed")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HCNL");
                    msSQL = " insert into agr_trn_thighmarkconsumerlog( " +
                                " highmarkconsumerlog_gid, " +
                                " contact_gid," +
                                " request_status," +
                                " bureau_score ," +
                                " bureau_response," +
                                " document_content," +
                                " failure_reason," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + contact_gid + "'," +
                                "'" + "Failed" + "'," +
                                "'" + "-" + "'," +
                                "'" + "-" + "'," +
                                "'" + "-" + "'," +
                                "'" + bureauLogResponse.failure_reason + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HCNL");
                    msSQL = " insert into agr_trn_thighmarkconsumerlog( " +
                                " highmarkconsumerlog_gid, " +
                                " contact_gid," +
                                " request_status," +
                                " bureau_score ," +
                                " bureau_response," +
                                " document_content," +
                                " failure_reason," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + contact_gid + "'," +
                                "'" + "Success" + "'," +
                                "'" + bureauLogResponse.bureau_score + "'," +
                                "'" + bureauLogResponse.bureau_response + "'," +
                                "'" + bureauLogResponse.document_content.Replace("'", "") + "'," +
                                "'" + "-" + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            catch (Exception ex)
            {
                LogForAuditHighmark("Highmark DB Logging Exception - String - " + ex.ToString() + " - End of Exception", bureau_type, contact_gid);
            }
        }


    }
}