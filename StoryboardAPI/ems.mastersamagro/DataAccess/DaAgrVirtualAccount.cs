using ems.mastersamagro.Models;
using ems.utilities.Functions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.Web;
using System.Net.Mail;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will create virtual account for the onboarded customers
    /// </summary>
    /// <remarks>Written by Premchandar.K </remarks>

    public class DaAgrVirtualAccount
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;
        string lsmaster_value;
        string ls_server, ls_username, ls_password;
        int ls_port;

        public MdlVirtualAccountResponse DaCreateVirtualAccount(string employee_gid, MdlVirtualAccount values)
        {
            MdlVirtualAccountResponse ObjMdlVirtualAccountResponse = new MdlVirtualAccountResponse();
            
            try
            {
                MdlVACreationRequest objMdlVACreationRequest = new MdlVACreationRequest();
                MdlVACreationResponse ObjMdlVACreationResponse = new MdlVACreationResponse();

                objMdlVACreationRequest.buyer_id = values.application_no;
                objMdlVACreationRequest.virtualaccount_no = values.virtualaccount_number;
                objMdlVACreationRequest.created_by = employee_gid;

                string VACreationRequestJSON = JsonConvert.SerializeObject(objMdlVACreationRequest);
                
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var client = new RestClient(ConfigurationManager.AppSettings["SamBTrnURL"].ToString());
        
                var request = new RestRequest(Method.POST);
                request.AddHeader("SamBTrn-ApiKey", ConfigurationManager.AppSettings["SamBTrnAPIKey"].ToString());
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("virtualaccount_json", VACreationRequestJSON);

                IRestResponse response = client.Execute(request);             

                ObjMdlVACreationResponse = JsonConvert.DeserializeObject<MdlVACreationResponse>(response.Content);
                
                if(ObjMdlVACreationResponse.status == true)
                {
                    msSQL = " update agr_mst_tbyronboard set " +
                      " vacreation_status='" + VACreationStatus.Success + "'" +
                      " where application_gid='" + values.application_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if(mnResult == 1)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("VACL");
                        msSQL = " insert into agr_trn_tvacreationlog(" +
                                " vacreationlog_gid," +
                                " application_gid," +
                                " buyer_id," +
                                " virtualaccount_no," +
                                " vacreation_status," +
                                " vacreation_message," +
                                " vacreation_requestid," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + values.application_gid + "'," +
                                "'" + values.application_no + "'," +
                                "'" + values.virtualaccount_number + "'," +
                                "'" + VACreationStatus.Success+ "'," +
                                "'" + ObjMdlVACreationResponse.message + "'," +
                                "'" + ObjMdlVACreationResponse.request_id + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    
                    ObjMdlVirtualAccountResponse.status = true;
                    ObjMdlVirtualAccountResponse.message = "Virtual Account Created Successfully";

                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("VACL");
                    msSQL = " insert into agr_trn_tvacreationlog(" +
                            " vacreationlog_gid," +
                            " application_gid," +
                            " buyer_id," +
                            " virtualaccount_no," +
                            " vacreation_status," +
                            " vacreation_message," +
                            " vacreation_requestid," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.application_no + "'," +
                            "'" + values.virtualaccount_number + "'," +
                            "'" + VACreationStatus.Failure + "'," +
                            "'" + ObjMdlVACreationResponse.message + "'," +
                            "'" + ObjMdlVACreationResponse.request_id + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    string responseMessage = ObjMdlVACreationResponse.message;
                    bool isDuplicate = (responseMessage.IndexOf("Duplicate", StringComparison.OrdinalIgnoreCase) >= 0);
                    if (isDuplicate)
                    {
                        msSQL = " update agr_mst_tbyronboard set " +
                      " vacreation_status='" + VACreationStatus.Success + "'" +
                      " where application_gid='" + values.application_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    ObjMdlVirtualAccountResponse.status = false;
                    ObjMdlVirtualAccountResponse.message = "Virtual Account Creation Failed - " + ObjMdlVACreationResponse.message;

                }


            }
            catch (Exception ex)
            {
                ObjMdlVirtualAccountResponse.status = false;
                ObjMdlVirtualAccountResponse.message = "Exception Occurred in VA Creation - " + ex.ToString();
            }
            return ObjMdlVirtualAccountResponse;
        }

        public VACreationConfirmationMailResponse VACreationConfirmationMail(string application_gid, string virtualaccount_no)
        {
            VACreationConfirmationMailResponse objVACreationConfirmationMailResponse = new VACreationConfirmationMailResponse();
            try
            {
                VACreationMailCustomer objVACreationMailCustomer = new VACreationMailCustomer();
                msSQL = " select a.customerref_name,b.employee_emailid," +
                        " concat(c.user_firstname, ' ', c.user_lastname) as relationshipmanager_name" +
                        " from agr_mst_tbyronboard a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where application_gid = '" + application_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objVACreationMailCustomer.customer_name = objODBCDatareader["customerref_name"].ToString();
                    objVACreationMailCustomer.relationshipmanager_name = objODBCDatareader["relationshipmanager_name"].ToString();
                    objVACreationMailCustomer.relationshipmanager_email = objODBCDatareader["employee_emailid"].ToString();
                }

                string lsinstitution_gid, lscontact_gid;

                msSQL = " select institution_gid from agr_mst_tbyronboard2institution" +
                       " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contact_gid from agr_mst_tbyronboardcontact" +
                   " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!String.IsNullOrEmpty(lsinstitution_gid))
                {
                    msSQL = " select email_address from agr_mst_tbyronboardinstitution2email" +
                      " where institution_gid = '" + lsinstitution_gid + "'";
                    objVACreationMailCustomer.customer_email = objdbconn.GetExecuteScalar(msSQL);
                }
                else if (!String.IsNullOrEmpty(lscontact_gid))
                {
                    msSQL = " select email_address from agr_mst_tbyronboardcontact2email" +
                      " where contact_gid = '" + lscontact_gid + "'";
                    objVACreationMailCustomer.customer_email = objdbconn.GetExecuteScalar(msSQL);
                }

                string bccmail_values = ConfigurationManager.AppSettings["VACreationMailBCCList"].ToString();

                string ccmail = objVACreationMailCustomer.relationshipmanager_email;

                string tomail = objVACreationMailCustomer.customer_email;

                string sub, body;
                SendMailResponse objSendMailResponse = new SendMailResponse();

                sub = virtualaccount_no + " " + HttpUtility.HtmlEncode(objVACreationMailCustomer.customer_name);
                body = "Dear " + HttpUtility.HtmlEncode(objVACreationMailCustomer.customer_name)+ ",<br><br>";
                body = body + "This is to inform you that your Samunnati Virtual Account Number - " + virtualaccount_no + " is successfully created. Requesting you to remit your future payments to Samunnati’s Virtual Account Number.<br><br>";
                body = body + "Please reach out to Mr/Ms. " + HttpUtility.HtmlEncode(objVACreationMailCustomer.relationshipmanager_name)+ " (" + HttpUtility.HtmlEncode(objVACreationMailCustomer.relationshipmanager_email) + ") for any queries relating to Virtual Account.<br><br>";
                body = body + "Thanks!<br>";
                body = body + "Team Samunnati.<br><br>";
                body = body + "<i>Disclaimer: This is an automatically system generated email – please do not reply to it. The content of this email is confidential and intended for the recipient specified in the message only. Please do not share any part of this email with any third party, without a written consent of Samunnati. If you are not the intended recipient of this email, please follow with its deletion and reach out to us on [tradecommunications@samunnati.com], so that you do not receive such emails in the future. Further, this information is confidential to the intended recipient and any misuse of any such information/ data will be considered as breach and subject to legal action under applicable laws.</i>";


                objSendMailResponse = DaSendMail(sub, body, tomail, ccmail, bccmail_values);
                if (objSendMailResponse.result == true)
                {
                    objVACreationConfirmationMailResponse.result = true;
                    objVACreationConfirmationMailResponse.message = "Mail triggered successfully";
                }
                else
                {
                    objVACreationConfirmationMailResponse.result = false;
                    objVACreationConfirmationMailResponse.message = "Exception occurred in send mail function - " + objSendMailResponse.message;
                }
            }
            catch(Exception ex)
            {
                objVACreationConfirmationMailResponse.result = false;
                objVACreationConfirmationMailResponse.message = "Exception occurred in VA Creation Confirmation mail function - " + ex.ToString();
            }
            return objVACreationConfirmationMailResponse;
        }

        public SendMailResponse DaSendMail(string sub, string body, string tomail, string ccmail, string bccmail)
        {
            SendMailResponse objSendMailResponse = new SendMailResponse();
            try
            {
                      
                msSQL = " SELECT pop_server, pop_port, pop_username, pop_password  FROM adm_mst_tcompany";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(tomail));


                if (!String.IsNullOrEmpty(bccmail))
                {
                    string[] bccmail_list = bccmail.Split(',');
                   
                        foreach (string BCCEmail in bccmail_list)
                        {
                            message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                        }

                }

               

                message.Subject = sub;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = ls_port;
                smtp.Host = ls_server; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                try
                {
                    smtp.Send(message);
                    objSendMailResponse.result = true;
                    objSendMailResponse.message = "Sendmail was successful";
                    

                }
                catch (Exception ex)
                {
                    objSendMailResponse.result = false;
                    objSendMailResponse.message = ex.ToString();
                }
               
            }
            catch (Exception ex)
            {
                objSendMailResponse.result = false;
                objSendMailResponse.message = ex.ToString();
            }

            return objSendMailResponse;


        }

    }
}