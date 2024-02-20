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
    /// This DataAccess provide access for various events (Highmark,
    /// TransUnionCommercial, TransUnionConsumer -  Credit , Institution) in Bureau API - Supplier.
    /// </summary>
    /// <remarks>Written by Praveen </remarks>

    public class DaAgrSuprBureauAPI
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

        string gender, id_type, telephone_no, telephone_type, residence_type, state_code;
        string addressline1, addressline2, address_type, postal_code;
        string lsreportpdfpath;

        string application_id, document_id;

        string gender_name, gender_code;

        string temp_bureauresponse = "";

        string company_name, companypan_no, mobile_no;
        string report_id;
        string templateIssuePath;
        string finalResponseHtmlData;

        string noofphones_rep3months, noofaddresses_rep3months, noofdistphones_rep3months, noofdistddresses_rep3months,
               noofdistids_rep3months, noofdistpincodes_rep3months, enqdifflenders_30days, newloansopened_30days,
               distunsecuredenq_3months, ranksegment_hml;


        public MdlHighmarkResponse DaGetHighmarkCreditInfo(string employee_gid, string contact_gid)
        {
            MdlHighmarkResponse ObjMdlHighmarkResponse = new MdlHighmarkResponse();
            try
            {

                msSQL = " select first_name,middle_name,last_name,pan_no,individual_dob from agr_mst_tsuprcontact where " +
                       " contact_gid='" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    name = objODBCDatareader["first_name"].ToString() + " " + objODBCDatareader["middle_name"].ToString() + " " + objODBCDatareader["last_name"].ToString();
                    pan = objODBCDatareader["pan_no"].ToString();
                    dob = objODBCDatareader["individual_dob"].ToString().Replace('-', '/');
                }

                msSQL = " select addressline1,addressline2,city,state,postal_code from agr_mst_tsuprcontact2address where " +
                           " contact_gid='" + contact_gid + "' and primary_status='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    address = objODBCDatareader["addressline1"].ToString() + objODBCDatareader["addressline2"].ToString();
                    city = objODBCDatareader["city"].ToString();
                    statetemp = objODBCDatareader["state"].ToString();
                    pin = objODBCDatareader["postal_code"].ToString();
                }
                msSQL = " select state_code from agr_mst_thighmarkstatecode where " +
                          " state='" + statetemp + "'";
                state = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select mobile_no from agr_mst_tsuprcontact2mobileno where " +
                          " contact_gid='" + contact_gid + "' and primary_status='Yes'";
                phone_number = objdbconn.GetExecuteScalar(msSQL);

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




                XmlDocument xmlResponseDoc = new XmlDocument();
                xmlResponseDoc.LoadXml(xmlResponse);


                ObjMdlHighmarkResponse.bureau_score = xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-VALUE").InnerText;

                if (!(xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-COMMENTS") == null))
                {
                    temp_bureauresponse += xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-COMMENTS").InnerText;
                }
                if (!(xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-FACTORS") == null))
                {
                    if (temp_bureauresponse != "")
                    {
                        temp_bureauresponse = temp_bureauresponse + "; " + xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-FACTORS").InnerText;
                    }
                    else
                    {
                        temp_bureauresponse += xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/SCORES/SCORE/SCORE-FACTORS").InnerText;
                    }
                }
                ObjMdlHighmarkResponse.bureau_response = temp_bureauresponse;


                fileName = xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/PRINTABLE-REPORT/FILE-NAME").InnerText;

                //fileName = fileName.Replace(".html", ".pdf");

                XmlCDataSection cDataNode = (XmlCDataSection)(xmlResponseDoc.SelectSingleNode("/INDV-REPORT-FILE/INDV-REPORTS/INDV-REPORT/PRINTABLE-REPORT/CONTENT").ChildNodes[0]);

                finalData = cDataNode.Data;





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
                lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                msGetGid = objcmnfunctions.GetMasterGID("IDCU");
                msSQL = " insert into agr_mst_tsuprindividual2cicdocumentupload( " +
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
                                  "'" + ex.ToString().Replace("'", "") + "'," +
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
                msSQL = "select document_content from agr_mst_tsuprindividual2cicdocumentupload" +
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
                msSQL = " select first_name,middle_name,last_name,pan_no,individual_dob,gender_name from agr_mst_tsuprcontact where " +
                       " contact_gid='" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    name = objODBCDatareader["first_name"].ToString() + " " + objODBCDatareader["middle_name"].ToString() + " " + objODBCDatareader["last_name"].ToString();
                    pan = objODBCDatareader["pan_no"].ToString();
                    dob = objODBCDatareader["individual_dob"].ToString().Replace("-", string.Empty);
                    gender_name = objODBCDatareader["gender_name"].ToString().Replace("-", string.Empty);
                }

                msSQL = " select mobile_no from agr_mst_tsuprcontact2mobileno where " +
                       " contact_gid='" + contact_gid + "' and primary_Status='Yes'";
                telephone_no = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select addressline1,addressline2,city,postal_code,state from agr_mst_tsuprcontact2address where " +
                       " contact_gid='" + contact_gid + "' and primary_status = 'Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    addressline1 = objODBCDatareader["addressline1"].ToString();
                    addressline2 = objODBCDatareader["addressline2"].ToString().Replace("-", string.Empty);
                    city = objODBCDatareader["city"].ToString();
                    postal_code = objODBCDatareader["postal_code"].ToString().Replace("-", string.Empty);
                    state = objODBCDatareader["state"].ToString().Replace("-", string.Empty);
                }

                msSQL = " select state_code from agr_mst_ttransunionstatecode where " +
                       " state='" + state + "'";
                state_code = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select gender_code from agr_mst_ttransuniongendercode where " +
                      " gender='" + gender_name + "'";
                gender_code = objdbconn.GetExecuteScalar(msSQL);


                id_type = "01";
                telephone_type = "01";
                residence_type = "01";
                address_type = "01";


                templatePath = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateRequestTransUnionConsumer.xml";



                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(templatePath);


                string xmlreqContents = xmlDoc.InnerXml;

                xmlreqContents = xmlreqContents.Replace("<ApplicantFirstName></ApplicantFirstName>", "<ApplicantFirstName>" + name + "</ApplicantFirstName>");
                xmlreqContents = xmlreqContents.Replace("<DateOfBirth></DateOfBirth>", "<DateOfBirth>" + dob + "</DateOfBirth>");
                xmlreqContents = xmlreqContents.Replace("<Gender></Gender>", "<Gender>" + gender_code + "</Gender>");

                xmlreqContents = xmlreqContents.Replace("<IdNumber></IdNumber>", "<IdNumber>" + pan + "</IdNumber>");
                xmlreqContents = xmlreqContents.Replace("<IdType></IdType>", "<IdType>" + id_type + "</IdType>");

                xmlreqContents = xmlreqContents.Replace("<TelephoneNumber></TelephoneNumber>", "<TelephoneNumber>" + telephone_no + "</TelephoneNumber>");
                xmlreqContents = xmlreqContents.Replace("<TelephoneType></TelephoneType>", "<TelephoneType>" + telephone_type + "</TelephoneType>");

                xmlreqContents = xmlreqContents.Replace("<AddressLine1></AddressLine1>", "<AddressLine1>" + addressline1 + "</AddressLine1>");
                xmlreqContents = xmlreqContents.Replace("<AddressLine2></AddressLine2>", "<AddressLine2>" + addressline2 + "</AddressLine2>");
                xmlreqContents = xmlreqContents.Replace("<AddressType></AddressType>", "<AddressType>" + address_type + "</AddressType>");

                xmlreqContents = xmlreqContents.Replace("<City></City>", "<City>" + city + "</City>");
                xmlreqContents = xmlreqContents.Replace("<PinCode></PinCode>", "<PinCode>" + postal_code + "</PinCode>");
                xmlreqContents = xmlreqContents.Replace("<ResidenceType></ResidenceType>", "<ResidenceType>" + residence_type + "</ResidenceType>");
                xmlreqContents = xmlreqContents.Replace("<StateCode></StateCode>", "<StateCode>" + state_code + "</StateCode>");



                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


                TransUnionService.ExternalSolutionExecutionService client = new TransUnionService.ExternalSolutionExecutionService();


                string xmlResponse = client.ExecuteXMLString(xmlreqContents);

                string pattern = @"Score&amp;gt;[0-9]{5}";

                Match m = Regex.Match(xmlResponse, pattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    ObjMdlTransUnionResponse.bureau_score = m.Value.Substring(m.Value.Length - 3);
                }



                string patternappid = @"ApplicationId&gt;[0-9]{8}&lt;/ApplicationId";

                Match mappid = Regex.Match(xmlResponse, patternappid, RegexOptions.IgnoreCase);
                if (mappid.Success)
                {
                    application_id = mappid.Value.Substring(17, 8);
                }

                string patterndocid = @"&lt;Id&gt;[0-9]{7}&lt;/Id&gt;";

                Match mdocid = Regex.Match(xmlResponse, patterndocid, RegexOptions.IgnoreCase);
                if (mdocid.Success)
                {
                    document_id = mdocid.Value.Substring(10, 7);
                }

                templatePathDoc = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateRequestDocTransUnionConsumer.xml";

                XmlDocument xmlDoc2 = new XmlDocument();
                xmlDoc2.Load(templatePathDoc);

                string xmlreqDocContents = xmlDoc2.InnerXml;

                xmlreqDocContents = xmlreqDocContents.Replace("<ApplicationId></ApplicationId>", "<ApplicationId>" + application_id + "</ApplicationId>");
                xmlreqDocContents = xmlreqDocContents.Replace("<DocumentId></DocumentId>", "<DocumentId>" + document_id + "</DocumentId>");


                DownloadDocumentResponseContract docContent = client.DownloadDocument(xmlreqDocContents);

                MemoryStream ms = new MemoryStream(docContent.FileContent);

                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                string lsreportabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/TransUnionReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                {
                    if ((!System.IO.Directory.Exists(lsreportabspath)))
                        System.IO.Directory.CreateDirectory(lsreportabspath);
                }
                fileName = pan + "TransUnionReport.xml";
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
                msSQL = " insert into agr_mst_tsuprindividual2cicdocumentupload( " +
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
                msSQL = " insert into agr_mst_tsuprcontact2tuhighriskalert( " +
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

            }
            catch (Exception ex)
            {
                ObjMdlTransUnionResponse.status = false;
                ObjMdlTransUnionResponse.message = "Transunion details generation failed";

                ObjMdlTransUnionResponse.message = ObjMdlTransUnionResponse.message.Replace("'", "");

                msSQL = " insert into agr_trn_texceptionlog( " +
                            " exception_message_name," +
                            " created_by," +
                            " created_date" +
                            " )values(" +
                            "'" + ObjMdlTransUnionResponse.message + "'," +
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
                msSQL = "select document_content from agr_mst_tsuprindividual2cicdocumentupload" +
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
            try
            {

                msSQL = " select company_name,companypan_no from agr_mst_tsuprinstitution where " +
                       " institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    company_name = objODBCDatareader["company_name"].ToString();
                    companypan_no = objODBCDatareader["companypan_no"].ToString();
                }

                msSQL = " select mobile_no from agr_mst_tsuprinstitution2mobileno where " +
                         " institution_gid='" + institution_gid + "' and primary_status='Yes'";
                mobile_no = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select addressline1,addressline2,city,state,postal_code from agr_mst_tsuprinstitution2address where " +
                           " institution_gid='" + institution_gid + "' and primary_status='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    address = objODBCDatareader["addressline1"].ToString() + objODBCDatareader["addressline2"].ToString();
                    city = objODBCDatareader["city"].ToString();
                    statetemp = objODBCDatareader["state"].ToString();
                    pin = objODBCDatareader["postal_code"].ToString();
                }
                msSQL = " select state_code from agr_mst_thighmarkstatecode where " +
                          " state='" + statetemp + "'";
                state = objdbconn.GetExecuteScalar(msSQL);

                LogForAudit("Institution Basic Details Obtained => Company Name - " + company_name + " Company PAN No - " + companypan_no + " Mobile No - " + mobile_no);
                LogForAudit("Institution Address Details Obtained => Address - " + address + " City - " + city + " State - " + state + " PIN - " + pin);




                templatePath = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateRequestHighMarkInstitution.xml";



                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(templatePath);

                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-APPLICANT-SEGMENT/BORROWER-NAME").InnerText = company_name.ToUpper();
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-APPLICANT-SEGMENT/IDS/ID/VALUE").InnerText = companypan_no;

                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-APPLICANT-SEGMENT/PHONES/PHONE/TELE-NO").InnerText = mobile_no;

                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-ADDRESS-SEGMENT/ADDRESS/ADDRESS-LINE").InnerText = address;
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-ADDRESS-SEGMENT/ADDRESS/CITY").InnerText = city;
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-ADDRESS-SEGMENT/ADDRESS/STATE").InnerText = state;
                xmlDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/COMM-ADDRESS-SEGMENT/ADDRESS/PIN").InnerText = pin;



                string xmlreqContents = xmlDoc.InnerXml;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                string requestUri = ConfigurationManager.AppSettings["highmarkinstitutionurl"].ToString();
                string xmlAckResponse = string.Empty;

                var client = new RestClient(requestUri);
                var request = new RestRequest(Method.POST);
                request.AddHeader("inquiryXML", xmlreqContents);
                request.AddHeader("userId", ConfigurationManager.AppSettings["highmarkinstitutionuserId"].ToString());
                request.AddHeader("password", ConfigurationManager.AppSettings["highmarkinstitutionpassword"].ToString());


                IRestResponse response = client.Execute(request);
                Thread.Sleep(2000);

                xmlAckResponse = response.Content;

                LogForAudit("Request Acknowledgement Response Obtained => Response - " + xmlAckResponse);



                XmlDocument xmlAckResponseDoc = new XmlDocument();
                xmlAckResponseDoc.LoadXml(xmlAckResponse);

                if (xmlAckResponseDoc.SelectSingleNode("/REPORT-FILE/INQUIRY-STATUS/INQUIRY/RESPONSE-TYPE").InnerText == "ERROR")
                {
                    ObjMdlHighmarkResponse.status = false;
                    ObjMdlHighmarkResponse.message = "Error in Acknowledgement of HighMark Request since '" + xmlAckResponseDoc.SelectSingleNode("/REPORT-FILE/INQUIRY-STATUS/INQUIRY/ERRORS/ERROR/DESCRIPTION").InnerText + "'";
                    LogForAudit("Acknowledgement Error Message => Message - " + ObjMdlHighmarkResponse.message);

                }
                else
                {
                    LogForAudit("Acknowledgement Received Successfully");
                    report_id = xmlAckResponseDoc.SelectSingleNode("/REPORT-FILE/INQUIRY-STATUS/INQUIRY/REPORT-ID").InnerText;
                    templateIssuePath = ConfigurationManager.AppSettings["file_path"] + "/templates/TemplateIssueHighmarkInstitution.xml";

                    XmlDocument xmlIssueDoc = new XmlDocument();
                    xmlIssueDoc.Load(templateIssuePath);

                    xmlIssueDoc.SelectSingleNode("/REQUEST-REQUEST-FILE/INQUIRY/REPORT-ID").InnerText = report_id;

                    string xmlIssueContents = xmlIssueDoc.InnerXml;
                    string xmlFinalResponse = string.Empty;

                    var clientIssue = new RestClient(requestUri);
                    var requestIssue = new RestRequest(Method.POST);
                    requestIssue.AddHeader("inquiryXML", xmlIssueContents);
                    requestIssue.AddHeader("userId", ConfigurationManager.AppSettings["highmarkinstitutionuserId"].ToString());
                    requestIssue.AddHeader("password", ConfigurationManager.AppSettings["highmarkinstitutionpassword"].ToString());


                    IRestResponse responseIssue = clientIssue.Execute(requestIssue);

                    Thread.Sleep(20000);

                    xmlFinalResponse = responseIssue.Content;

                    LogForAudit("Request Final Response Obtained => Response - " + xmlFinalResponse);


                    XmlDocument xmlFinalResponseDoc = new XmlDocument();
                    xmlFinalResponseDoc.LoadXml(xmlFinalResponse);

                    ObjMdlHighmarkResponse.bureau_score = xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/COMMERCIAL-REPORT/SCORES/SCORE/SCORE-VALUE").InnerText;

                    LogForAudit("Bureau Score Obtained => Bureau Score - " + ObjMdlHighmarkResponse.bureau_score);

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

                    LogForAudit("Bureau Response Obtained => Bureau Response - " + ObjMdlHighmarkResponse.bureau_response);


                    XmlCDataSection cDataNode = (XmlCDataSection)(xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/PRINTABLE-REPORT/CONTENT").ChildNodes[0]);

                    finalResponseHtmlData = cDataNode.Data;

                    LogForAudit("Final Response Html Data Obtained => Final Response - " + finalResponseHtmlData);


                    fileName = xmlFinalResponseDoc.SelectSingleNode("/COMMERCIAL-REPORT-FILE/COMMERCIAL-ACE-REPORTS/PRINTABLE-REPORT/FILE-NAME").InnerText;

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



                    msGetGid = objcmnfunctions.GetMasterGID("INCU");
                    msSQL = " insert into agr_mst_tsuprinstitution2cicdocumentupload( " +
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
                                "'" + fileName + "'," +
                                "'" + lsreportrelpath + fileName + "'," +
                                "'" + finalResponseHtmlData.Replace("'", "") + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        LogForAudit("Highmark Institution Details entered into DB successfully!");

                        ObjMdlHighmarkResponse.status = true;
                        ObjMdlHighmarkResponse.message = "HighMark Details Generated Successfully..!";
                    }
                    else
                    {
                        ObjMdlHighmarkResponse.status = false;
                        ObjMdlHighmarkResponse.message = "Error occured in generating Highmark Details";
                    }

                }
            }
            catch (Exception ex)
            {
                LogForAudit("Exception occured in Highmark Institution Report => Exception Message - " + ex.ToString());
                LogForAudit("Exception occured in Highmark Institution Report => Exception StackTrace- " + ex.StackTrace);


                ObjMdlHighmarkResponse.status = false;
                ObjMdlHighmarkResponse.message = ex.ToString();

                msSQL = " insert into agr_trn_texceptionlog( " +
                                  " exception_message_name ," +
                                  " created_by," +
                                  " created_date" +
                                  " )values(" +
                                  "'" + ex.ToString().Replace("'", "") + " XXXSeparatorXXX " + ex.StackTrace.Replace("'", "") + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            return ObjMdlHighmarkResponse;
        }

        public void DaGetHighmarkInstitutionHTMLContent(string tmpcicdocument_gid, MdlHighmarkResponse values)
        {
            try
            {
                msSQL = "select document_content from agr_mst_tsuprinstitution2cicdocumentupload" +
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
                        " from agr_mst_tsuprcontact2tuhighriskalert a" +
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

    } 
}