using ems.hbapiconn.Models;
using ems.utilities.Functions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ems.hbapiconn.Functions
{
    public class FnSamFinEncoreDisbursement
    {
        string lsresponsecontentdisbursement, lsresponseStatusCodedisbursement, lsdissuplrapplication_gid, lsdissuplrrmdisbursementrequest_gid;
        string msSQL,msGetGidDisResp,msGetGidDisReq;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();

        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        int mnResult;
        string loglspath = "", logFileName = "", lsfarmer, lsencoreintegration_status, lssupplier;
        int farmer_count, total_farmer, supplier_count, total_supplier;

        //Disbursement booking in Encore for Farmer
        public MdlEncoreDisbursementResponse DaPostEncoreDisbursementFarmer(MdlEncoreDisbursement values, string employee_gid)
        {
            string type = "DisbursementFarmer";
            MdlEncoreDisbursementResponse ObjMdlEncoreDisbursementResponse = new MdlEncoreDisbursementResponse();
            try
            {
                MdlEncoreDisbursementRequest ObjMdlEncoreDisbursementRequest = new MdlEncoreDisbursementRequest();

                    LogForAuditEncoreIntegration("Disbursement Booking Request Initiated for Individual Farmer . Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                   
                    msSQL = " select farmercontact_gid,encore_accountid,urn,creditopscheckerdisbursement_amount,bankaccount_number, " +                      
                            " accountholder_name, " +
                            " concat('accountCode:','','|bankName:',bank_name,'|bankBranchDetails:',branch_address,'|bankDisburse:','true') as reference, " +
                            " ifsc_code from ocs_trn_tfarmercontact " +                                        
                            " where farmercontact_gid='" + values.farmercontact_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {

                    ObjMdlEncoreDisbursementRequest.transaction.accountId = objODBCDatareader["encore_accountid"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.customerId = objODBCDatareader["urn"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.amount1 = objODBCDatareader["creditopscheckerdisbursement_amount"].ToString().Replace(",", "");
                    ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId = objODBCDatareader["bankaccount_number"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.param5 = objODBCDatareader["accountholder_name"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.reference = objODBCDatareader["reference"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.param4 = objODBCDatareader["ifsc_code"].ToString();
                   
                    }
                    objODBCDatareader.Close();

                    msSQL = " SELECT encoredisbursementrequestmaster_gid,transactionName,userId,instrument,remarks,description,part1,part2,part3,part4,part5,part6,part7,part8, " +
                            " safeMode,runReversedTransactions FROM ocs_mst_tencoredisbursementrequestmaster where encoredisbursementrequestmaster_gid='" + ConfigurationManager.AppSettings["disbursementrequestmastertemplateid"].ToString() + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {
                        ObjMdlEncoreDisbursementRequest.transaction.transactionName = objODBCDatareader["transactionName"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.userId = objODBCDatareader["userId"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.instrument = values.instrument;
                        ObjMdlEncoreDisbursementRequest.transaction.remarks = objODBCDatareader["remarks"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.description = objODBCDatareader["description"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.part1 = objODBCDatareader["part1"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.part2 = objODBCDatareader["part2"].ToString();
                        
                        ObjMdlEncoreDisbursementRequest.transaction.part3 = objODBCDatareader["part3"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.part4 = objODBCDatareader["part4"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.part5 = objODBCDatareader["part5"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.part6 = objODBCDatareader["part6"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.part7 = objODBCDatareader["part7"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.part8 = objODBCDatareader["part8"].ToString();
                        ObjMdlEncoreDisbursementRequest.safeMode = objODBCDatareader["safeMode"].ToString();

                        ObjMdlEncoreDisbursementRequest.runReversedTransactions = objODBCDatareader["runReversedTransactions"].ToString();

                        ObjMdlEncoreDisbursementRequest.transaction.valueDateStr = "2022-12-01";
                    }
                    objODBCDatareader.Close();
                               
                    string lsdisbursementreqst_json = Newtonsoft.Json.JsonConvert.SerializeObject(ObjMdlEncoreDisbursementRequest);

                    msGetGidDisReq = objcmnfunctions.GetMasterGID("ENDR");
                    msSQL = " insert into ocs_trn_tencoredisbursementrequest(" +
                            " encoredisbursementrequest_gid," +                      
                            " farmercontact_gid," +
                            " request_time," +
                            " requested_by," +
                            " accountId," +
                            " customerId," +
                            " amount1," +
                            " transactionName," +
                            " param2Str," +
                            " userId," +
                            " valueDateStr," +
                            " instrument," +
                            " remarks," +
                            " description," +
                            " transactionDateStr," +
                            " part1," +
                            " part2," +
                            " part3," +
                            " part4," +
                            " part5," +
                            " part6," +
                            " part7," +
                            " part8," +
                            " payeeAccountId," +
                            " param5," +
                            " reference," +
                            " param4," +
                            " safeMode," +
                            " runReversedTransactions," +                        
                            " json_string) " +
                            " values(" +
                            "'" + msGetGidDisReq + "'," +
                            "'" + values.farmercontact_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.accountId + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.customerId + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.amount1 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.transactionName + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.param2Str + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.userId + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.valueDateStr + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.instrument + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.remarks + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.description + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.transactionDateStr + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.part1 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.part2 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.part3 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.part4 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.part5 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.part6 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.part7 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.part8 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.param5 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.reference + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.transaction.param4 + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.safeMode + "'," +
                            "'" + ObjMdlEncoreDisbursementRequest.runReversedTransactions + "'," +
                            "'" + lsdisbursementreqst_json + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 1)
                    {
                        LogForAuditEncoreIntegration("Error occurred while storing disbursement booking request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        ObjMdlEncoreDisbursementResponse.status = false;
                        ObjMdlEncoreDisbursementResponse.message = "Error occurred while posting disbursement booking request to Encore";
                        return ObjMdlEncoreDisbursementResponse;
                    }
                    try
                    {
                        var client = new RestClient(ConfigurationManager.AppSettings["encore_disbursementurl"].ToString());
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                        string val = System.Convert.ToBase64String(plainTextBytes);
                        request.AddHeader("Authorization", "Basic " + val);

                        var body = lsdisbursementreqst_json;

                        request.AddParameter("application/json", body, ParameterType.RequestBody);

                        IRestResponse response = client.Execute(request);

                        lsresponsecontentdisbursement = response.Content;
                        lsresponseStatusCodedisbursement = response.StatusCode.ToString();

                    }
                    catch (Exception ex)
                    {
                        LogForAuditEncoreIntegration("Error occurred while hitting disbursement booking URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentdisbursement + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        ObjMdlEncoreDisbursementResponse.status = false;
                        ObjMdlEncoreDisbursementResponse.message = "Error occurred posting disbursement booking request to Encore";
                        return ObjMdlEncoreDisbursementResponse;
                    }
                    if (lsresponseStatusCodedisbursement == "OK")
                    {
                    

                        msGetGidDisResp = objcmnfunctions.GetMasterGID("EDRS");
                        msSQL = " insert into ocs_trn_tencoredisbursementresponse(" +
                                " encoredisbursementresponse_gid," +
                                " encoredisbursementrequest_gid," +
                                " response_time," +
                                " json_string) " +
                                " values(" +
                                "'" + msGetGidDisResp + "'," +
                                "'" + msGetGidDisReq + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +                                                                                             
                                "'" + lsresponsecontentdisbursement + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        if (mnResult != 0)
                        {
                        msSQL = "update ocs_trn_tfarmercontact set disbursementbookingencore_status='Y' where farmercontact_gid='" + values.farmercontact_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        LogForAuditEncoreIntegration("Disbursement Booking Request Ended for Individual Farmer . Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            ObjMdlEncoreDisbursementResponse.status = true;
                            ObjMdlEncoreDisbursementResponse.message = "Disbursement Booking Response received successfully from Encore";
                            return ObjMdlEncoreDisbursementResponse;
                        }
                        else
                        {
                            LogForAuditEncoreIntegration("Error occurred while storing disbursement response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            ObjMdlEncoreDisbursementResponse.status = false;
                            ObjMdlEncoreDisbursementResponse.message = "Error occurred posting disbursement booking request to Encore";
                            return ObjMdlEncoreDisbursementResponse;
                        }

                }
                else if ((lsresponseStatusCodedisbursement == "InternalServerError"))
                {
                    MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                    objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentdisbursement);

                    LogForAuditEncoreIntegration("Internal Server Error . Response Content - " + lsresponsecontentdisbursement + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlEncoreDisbursementResponse.status = false;
                    ObjMdlEncoreDisbursementResponse.message = "Disbursement Booking Request in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.message;
                    return ObjMdlEncoreDisbursementResponse;
                }
                else
                {
                    LogForAuditEncoreIntegration("Failed to receive 200 or 500 Response  . Response Content - " + lsresponsecontentdisbursement + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlEncoreDisbursementResponse.status = false;
                    ObjMdlEncoreDisbursementResponse.message = "Disbursement Booking Request in Encore failed\nEncore Response: " + lsresponseStatusCodedisbursement + "";
                    return ObjMdlEncoreDisbursementResponse;
                }
            }
            catch (Exception ex)
            {
                LogForAuditEncoreIntegration("Error occurred posting disbursement booking request to Encore . Exception - " + ex + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                ObjMdlEncoreDisbursementResponse.status = false;
                ObjMdlEncoreDisbursementResponse.message = "Error occurred posting disbursement booking request to Encore";
                return ObjMdlEncoreDisbursementResponse;
            }

        }

        //Disbursement booking in Encore for Applicant(B2B)
        public MdlEncoreDisbursementResponse DaPostEncoreDisbursementB2B(MdlEncoreDisbursement values, string employee_gid)
        {
            string type = "DisbursementApplicant(B2B)";
            MdlEncoreDisbursementResponse ObjMdlEncoreDisbursementResponse = new MdlEncoreDisbursementResponse();
            try
            {
                MdlEncoreDisbursementRequest ObjMdlEncoreDisbursementRequest = new MdlEncoreDisbursementRequest();

                LogForAuditEncoreIntegration("Disbursement Booking Request Initiated for Applicant. Application ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                
                msSQL = " select encore_accountid " +
                        " from ocs_trn_trmdisbursementrequest " +
                        " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ObjMdlEncoreDisbursementRequest.transaction.accountId = objODBCDatareader["encore_accountid"].ToString();                    
                }
                objODBCDatareader.Close();
                
                msSQL = " select customer_urn " +
                        " from ocs_trn_tcadapplication " +
                        " where application_gid='" + values.application_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ObjMdlEncoreDisbursementRequest.transaction.customerId = objODBCDatareader["customer_urn"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select count(disbapplicantbankdtl_gid) from ocs_trn_tdisbapplicantbankdtl where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' and disbursementaccount_status='Yes'";

                string count_disbapplicantbankdtl = objdbconn.GetExecuteScalar(msSQL);
                if (!(count_disbapplicantbankdtl == "0"))
                {
                    msSQL = " select disbapplicantbankdtl_gid,creditopscheckerdisbursement_amount, " +
                       " bankaccount_number,accountholder_name," +
                       " concat('accountCode:','','|bankName:',bank_name,'|bankBranchDetails:',branch_name,' ',branch_address,'|bankDisburse:','true') as reference, " +
                       " ifsc_code from ocs_trn_tdisbapplicantbankdtl " +
                       " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' and disbursementaccount_status='Yes'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId = objODBCDatareader["bankaccount_number"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.param5 = objODBCDatareader["accountholder_name"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.reference = objODBCDatareader["reference"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.param4 = objODBCDatareader["ifsc_code"].ToString();
                    }
                    objODBCDatareader.Close();
                }

                msSQL = "select count(creditbankdtl_gid) from ocs_trn_tcadcreditbankdtl where application_gid='" + values.application_gid + "' and disbursementaccount_status='Yes'";

                string count_cadcreditbankdtl = objdbconn.GetExecuteScalar(msSQL);
                if (!(count_cadcreditbankdtl == "0"))
                {
                    msSQL = " select creditbankdtl_gid,credit_gid, " +
                       " bankaccount_number,bankaccount_name," +
                       " concat('accountCode:','','|bankName:',bank_name,'|bankBranchDetails:',branch_name,' ',bank_address,'|bankDisburse:','true') as reference, " +
                       " ifsc_code from ocs_trn_tcadcreditbankdtl " +
                       " where application_gid='" + values.application_gid + "' and disbursementaccount_status='Yes'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId = objODBCDatareader["bankaccount_number"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.param5 = objODBCDatareader["bankaccount_name"].ToString();                        
                        ObjMdlEncoreDisbursementRequest.transaction.reference = objODBCDatareader["reference"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.param4 = objODBCDatareader["ifsc_code"].ToString();
                    }
                    objODBCDatareader.Close();
                }

                msSQL = "select count(lsabankaccdtl_gid) from ocs_trn_tlsabankaccountdtl where application_gid='" + values.application_gid + "' and disbursementaccount_status='Yes'";

                string count_lsabankaccountdtl = objdbconn.GetExecuteScalar(msSQL);
                if (!(count_lsabankaccountdtl == "0"))
                {
                    msSQL = " select lsabankaccdtl_gid, " +
                       " bankaccount_number,accountholder_name," +
                       " concat('accountCode:','','|bankName:',bank_name,'|bankBranchDetails:',branch_name,' ',branch_address,'|bankDisburse:','true') as reference, " +
                       " ifsc_code from ocs_trn_tlsabankaccountdtl " +
                       " where application_gid='" + values.application_gid + "' and disbursementaccount_status='Yes'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId = objODBCDatareader["bankaccount_number"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.param5 = objODBCDatareader["accountholder_name"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.reference = objODBCDatareader["reference"].ToString();
                        ObjMdlEncoreDisbursementRequest.transaction.param4 = objODBCDatareader["ifsc_code"].ToString();
                    }
                    objODBCDatareader.Close();
                }



                msSQL = " select checkerdisbursement_amount " +                        
                        " from ocs_trn_tdisbursementamount " +
                        " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ObjMdlEncoreDisbursementRequest.transaction.amount1 = objODBCDatareader["checkerdisbursement_amount"].ToString().Replace(",", "");                   
                }
                objODBCDatareader.Close();

                msSQL = " SELECT encoredisbursementrequestmaster_gid,transactionName,userId,instrument,remarks,description,part1,part2,part3,part4,part5,part6,part7,part8, " +
                        " safeMode,runReversedTransactions FROM ocs_mst_tencoredisbursementrequestmaster where encoredisbursementrequestmaster_gid='" + ConfigurationManager.AppSettings["disbursementrequestmastertemplateid"].ToString() + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    ObjMdlEncoreDisbursementRequest.transaction.transactionName = objODBCDatareader["transactionName"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.userId = objODBCDatareader["userId"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.instrument = values.instrument;
                    ObjMdlEncoreDisbursementRequest.transaction.remarks = objODBCDatareader["remarks"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.description = objODBCDatareader["description"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part1 = objODBCDatareader["part1"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part2 = objODBCDatareader["part2"].ToString();

                    ObjMdlEncoreDisbursementRequest.transaction.part3 = objODBCDatareader["part3"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part4 = objODBCDatareader["part4"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part5 = objODBCDatareader["part5"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part6 = objODBCDatareader["part6"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part7 = objODBCDatareader["part7"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part8 = objODBCDatareader["part8"].ToString();
                    ObjMdlEncoreDisbursementRequest.safeMode = objODBCDatareader["safeMode"].ToString();

                    ObjMdlEncoreDisbursementRequest.runReversedTransactions = objODBCDatareader["runReversedTransactions"].ToString();

                    ObjMdlEncoreDisbursementRequest.transaction.valueDateStr = "2022-12-01";
                }
                objODBCDatareader.Close();

                string lsdisbursementreqst_json = Newtonsoft.Json.JsonConvert.SerializeObject(ObjMdlEncoreDisbursementRequest);

                msGetGidDisReq = objcmnfunctions.GetMasterGID("ENDR");
                msSQL = " insert into ocs_trn_tencoredisbursementrequest(" +
                        " encoredisbursementrequest_gid," +
                        " application_gid," +
                        " request_time," +
                        " requested_by," +
                        " accountId," +
                        " customerId," +
                        " amount1," +
                        " transactionName," +
                        " param2Str," +
                        " userId," +
                        " valueDateStr," +
                        " instrument," +
                        " remarks," +
                        " description," +
                        " transactionDateStr," +
                        " part1," +
                        " part2," +
                        " part3," +
                        " part4," +
                        " part5," +
                        " part6," +
                        " part7," +
                        " part8," +
                        " payeeAccountId," +
                        " param5," +
                        " reference," +
                        " param4," +
                        " safeMode," +
                        " runReversedTransactions," +
                        " json_string) " +
                        " values(" +
                        "'" + msGetGidDisReq + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.accountId + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.customerId + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.amount1 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.transactionName + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.param2Str + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.userId + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.valueDateStr + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.instrument + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.remarks + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.description + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.transactionDateStr + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part1 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part2 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part3 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part4 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part5 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part6 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part7 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part8 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.param5 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.reference + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.param4 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.safeMode + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.runReversedTransactions + "'," +
                        "'" + lsdisbursementreqst_json + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 1)
                {
                    LogForAuditEncoreIntegration("Error occurred while storing disbursement booking request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlEncoreDisbursementResponse.status = false;
                    ObjMdlEncoreDisbursementResponse.message = "Error occurred while posting disbursement booking request to Encore";
                    return ObjMdlEncoreDisbursementResponse;
                }
                try
                {
                    var client = new RestClient(ConfigurationManager.AppSettings["encore_disbursementurl"].ToString());
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                    string val = System.Convert.ToBase64String(plainTextBytes);
                    request.AddHeader("Authorization", "Basic " + val);

                    var body = lsdisbursementreqst_json;

                    request.AddParameter("application/json", body, ParameterType.RequestBody);

                    IRestResponse response = client.Execute(request);

                    lsresponsecontentdisbursement = response.Content;
                    lsresponseStatusCodedisbursement = response.StatusCode.ToString();

                }
                catch (Exception ex)
                {
                    LogForAuditEncoreIntegration("Error occurred while hitting disbursement booking URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentdisbursement + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlEncoreDisbursementResponse.status = false;
                    ObjMdlEncoreDisbursementResponse.message = "Error occurred posting disbursement booking request to Encore";
                    return ObjMdlEncoreDisbursementResponse;
                }
                if (lsresponseStatusCodedisbursement == "OK")
                {
                    

                    msGetGidDisResp = objcmnfunctions.GetMasterGID("EDRS");
                    msSQL = " insert into ocs_trn_tencoredisbursementresponse(" +
                            " encoredisbursementresponse_gid," +
                            " encoredisbursementrequest_gid," +
                            " response_time," +
                            " json_string) " +
                            " values(" +
                            "'" + msGetGidDisResp + "'," +
                            "'" + msGetGidDisReq + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + lsresponsecontentdisbursement + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    if (mnResult != 0)
                    {
                        msSQL = "update ocs_trn_trmdisbursementrequest set disbursementbookingencore_status='Y' where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        LogForAuditEncoreIntegration("Disbursement Booking Request Ended for Applicant . Applicant ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        ObjMdlEncoreDisbursementResponse.status = true;
                        ObjMdlEncoreDisbursementResponse.message = "Disbursement Booking Response received successfully from Encore";
                        return ObjMdlEncoreDisbursementResponse;
                    }
                    else
                    {
                        LogForAuditEncoreIntegration("Error occurred while storing disbursement response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        ObjMdlEncoreDisbursementResponse.status = false;
                        ObjMdlEncoreDisbursementResponse.message = "Error occurred posting disbursement booking request to Encore";
                        return ObjMdlEncoreDisbursementResponse;
                    }

                }
                else if ((lsresponseStatusCodedisbursement == "InternalServerError"))
                {
                    MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                    objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentdisbursement);

                    LogForAuditEncoreIntegration("Internal Server Error . Response Content - " + lsresponsecontentdisbursement + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlEncoreDisbursementResponse.status = false;
                    ObjMdlEncoreDisbursementResponse.message = "Disbursement Booking Request in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.message;
                    return ObjMdlEncoreDisbursementResponse;
                }
                else
                {
                    LogForAuditEncoreIntegration("Failed to receive 200 or 500 Response  . Response Content - " + lsresponsecontentdisbursement + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlEncoreDisbursementResponse.status = false;
                    ObjMdlEncoreDisbursementResponse.message = "Disbursement Booking Request in Encore failed\nEncore Response: " + lsresponseStatusCodedisbursement + "";
                    return ObjMdlEncoreDisbursementResponse;
                }
            }
            catch (Exception ex)
            {
                LogForAuditEncoreIntegration("Error occurred posting disbursement booking request  to Encore . Exception - " + ex + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                ObjMdlEncoreDisbursementResponse.status = false;
                ObjMdlEncoreDisbursementResponse.message = "Error occurred posting disbursement booking request to Encore";
                return ObjMdlEncoreDisbursementResponse;
            }

        }

        //Disbursement booking in Encore for Supplier
        public MdlEncoreDisbursementResponse DaPostEncoreDisbursementSupplier(MdlEncoreDisbursement values, string employee_gid)
        {
            string type = "DisbursementSupplier";
            MdlEncoreDisbursementResponse ObjMdlEncoreDisbursementResponse = new MdlEncoreDisbursementResponse();
            try
            {
                MdlEncoreDisbursementRequest ObjMdlEncoreDisbursementRequest = new MdlEncoreDisbursementRequest();

                LogForAuditEncoreIntegration("Disbursement Booking Request Initiated for Supplier. Supplier ID - " + values.disbursementsupplier_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                msSQL = " select application_gid,rmdisbursementrequest_gid,disbursementsupplier_gid,creditopscheckerdisbursement_amount, " +
                        " bankaccount_number,accountholder_name," +
                        " concat('accountCode:','','|bankName:',bank_name,'|bankBranchDetails:',branch_name,' ',branch_address,'|bankDisburse:','true') as reference, " +
                        " ifsc_code from ocs_trn_tdisbursementsupplier " +
                        " where disbursementsupplier_gid='" + values.disbursementsupplier_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdissuplrapplication_gid = objODBCDatareader["application_gid"].ToString();
                    lsdissuplrrmdisbursementrequest_gid = objODBCDatareader["rmdisbursementrequest_gid"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.amount1 = objODBCDatareader["creditopscheckerdisbursement_amount"].ToString().Replace(",", "");                   
                    ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId = objODBCDatareader["bankaccount_number"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.param5 = objODBCDatareader["accountholder_name"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.reference = objODBCDatareader["reference"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.param4 = objODBCDatareader["ifsc_code"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = " select encore_accountid " +
                        " from ocs_trn_trmdisbursementrequest " +
                        " where rmdisbursementrequest_gid='" + lsdissuplrrmdisbursementrequest_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ObjMdlEncoreDisbursementRequest.transaction.accountId = objODBCDatareader["encore_accountid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select customer_urn " +
                        " from ocs_trn_tcadapplication " +
                        " where application_gid='" + lsdissuplrapplication_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ObjMdlEncoreDisbursementRequest.transaction.customerId = objODBCDatareader["customer_urn"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT encoredisbursementrequestmaster_gid,transactionName,userId,instrument,remarks,description,part1,part2,part3,part4,part5,part6,part7,part8, " +
                            " safeMode,runReversedTransactions FROM ocs_mst_tencoredisbursementrequestmaster where encoredisbursementrequestmaster_gid='" + ConfigurationManager.AppSettings["disbursementrequestmastertemplateid"].ToString() + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    ObjMdlEncoreDisbursementRequest.transaction.transactionName = objODBCDatareader["transactionName"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.userId = objODBCDatareader["userId"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.instrument = values.instrument;
                    ObjMdlEncoreDisbursementRequest.transaction.remarks = objODBCDatareader["remarks"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.description = objODBCDatareader["description"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part1 = objODBCDatareader["part1"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part2 = objODBCDatareader["part2"].ToString();

                    ObjMdlEncoreDisbursementRequest.transaction.part3 = objODBCDatareader["part3"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part4 = objODBCDatareader["part4"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part5 = objODBCDatareader["part5"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part6 = objODBCDatareader["part6"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part7 = objODBCDatareader["part7"].ToString();
                    ObjMdlEncoreDisbursementRequest.transaction.part8 = objODBCDatareader["part8"].ToString();
                    ObjMdlEncoreDisbursementRequest.safeMode = objODBCDatareader["safeMode"].ToString();

                    ObjMdlEncoreDisbursementRequest.runReversedTransactions = objODBCDatareader["runReversedTransactions"].ToString();

                    ObjMdlEncoreDisbursementRequest.transaction.valueDateStr = "2022-12-01";
                }
                objODBCDatareader.Close();

                string lsdisbursementreqst_json = Newtonsoft.Json.JsonConvert.SerializeObject(ObjMdlEncoreDisbursementRequest);

                msGetGidDisReq = objcmnfunctions.GetMasterGID("ENDR");
                msSQL = " insert into ocs_trn_tencoredisbursementrequest(" +
                        " encoredisbursementrequest_gid," +      
                        " disbursementsupplier_gid," +
                        " request_time," +
                        " requested_by," +
                        " accountId," +
                        " customerId," +
                        " amount1," +
                        " transactionName," +
                        " param2Str," +
                        " userId," +
                        " valueDateStr," +
                        " instrument," +
                        " remarks," +
                        " description," +
                        " transactionDateStr," +
                        " part1," +
                        " part2," +
                        " part3," +
                        " part4," +
                        " part5," +
                        " part6," +
                        " part7," +
                        " part8," +
                        " payeeAccountId," +
                        " param5," +
                        " reference," +
                        " param4," +
                        " safeMode," +
                        " runReversedTransactions," +
                        " json_string) " +
                        " values(" +
                        "'" + msGetGidDisReq + "'," +
                        "'" + values.disbursementsupplier_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.accountId + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.customerId + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.amount1 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.transactionName + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.param2Str + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.userId + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.valueDateStr + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.instrument + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.remarks + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.description + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.transactionDateStr + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part1 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part2 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part3 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part4 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part5 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part6 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part7 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.part8 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.param5 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.reference + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.transaction.param4 + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.safeMode + "'," +
                        "'" + ObjMdlEncoreDisbursementRequest.runReversedTransactions + "'," +
                        "'" + lsdisbursementreqst_json + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 1)
                {
                    LogForAuditEncoreIntegration("Error occurred while storing disbursement booking request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlEncoreDisbursementResponse.status = false;
                    ObjMdlEncoreDisbursementResponse.message = "Error occurred while posting disbursement booking request to Encore";
                    return ObjMdlEncoreDisbursementResponse;
                }
                try
                {
                    var client = new RestClient(ConfigurationManager.AppSettings["encore_disbursementurl"].ToString());
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                    string val = System.Convert.ToBase64String(plainTextBytes);
                    request.AddHeader("Authorization", "Basic " + val);

                    var body = lsdisbursementreqst_json;

                    request.AddParameter("application/json", body, ParameterType.RequestBody);

                    IRestResponse response = client.Execute(request);

                    lsresponsecontentdisbursement = response.Content;
                    lsresponseStatusCodedisbursement = response.StatusCode.ToString();

                }
                catch (Exception ex)
                {
                    LogForAuditEncoreIntegration("Error occurred while hitting disbursement booking URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentdisbursement + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlEncoreDisbursementResponse.status = false;
                    ObjMdlEncoreDisbursementResponse.message = "Error occurred posting disbursement booking request to Encore";
                    return ObjMdlEncoreDisbursementResponse;
                }
                if (lsresponseStatusCodedisbursement == "OK")
                {
                    

                    msGetGidDisResp = objcmnfunctions.GetMasterGID("EDRS");
                    msSQL = " insert into ocs_trn_tencoredisbursementresponse(" +
                            " encoredisbursementresponse_gid," +
                            " encoredisbursementrequest_gid," +
                            " response_time," +
                            " json_string) " +
                            " values(" +
                            "'" + msGetGidDisResp + "'," +
                            "'" + msGetGidDisReq + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + lsresponsecontentdisbursement + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    if (mnResult != 0)
                    {
                        msSQL = "update ocs_trn_tdisbursementsupplier set disbursementbookingencore_status='Y' where disbursementsupplier_gid='" + values.disbursementsupplier_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        LogForAuditEncoreIntegration("Disbursement Booking Request Ended for Supplier . Supplier ID - " + values.disbursementsupplier_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        ObjMdlEncoreDisbursementResponse.status = true;
                        ObjMdlEncoreDisbursementResponse.message = "Disbursement Response received successfully from Encore";
                        return ObjMdlEncoreDisbursementResponse;
                    }
                    else
                    {
                        LogForAuditEncoreIntegration("Error occurred while storing disbursement response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        ObjMdlEncoreDisbursementResponse.status = false;
                        ObjMdlEncoreDisbursementResponse.message = "Error occurred posting disbursement request to Encore";
                        return ObjMdlEncoreDisbursementResponse;
                    }

                }
                else if ((lsresponseStatusCodedisbursement == "InternalServerError"))
                {
                    MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                    objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentdisbursement);

                    LogForAuditEncoreIntegration("Internal Server Error . Response Content - " + lsresponsecontentdisbursement + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlEncoreDisbursementResponse.status = false;
                    ObjMdlEncoreDisbursementResponse.message = "Disbursement Booking Request in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.message;
                    return ObjMdlEncoreDisbursementResponse;
                }
                else
                {
                    LogForAuditEncoreIntegration("Failed to receive 200 or 500 Response . Response Content - " + lsresponsecontentdisbursement + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlEncoreDisbursementResponse.status = false;
                    ObjMdlEncoreDisbursementResponse.message = "Disbursement Booking Request in Encore failed\nEncore Response: " + lsresponseStatusCodedisbursement + "";
                    return ObjMdlEncoreDisbursementResponse;
                }
            }
            catch (Exception ex)
            {
                LogForAuditEncoreIntegration("Error occurred posting disbursement booking request  to Encore . Exception - " + ex + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                ObjMdlEncoreDisbursementResponse.status = false;
                ObjMdlEncoreDisbursementResponse.message = "Error occurred posting disbursement booking request to Encore";
                return ObjMdlEncoreDisbursementResponse;
            }

        }

        //Batch Disbursement booking in Encore for Farmer
        public void DaBatchEncoreDisbursementFarmer(MdlEncoreDisbursement values, string employee_gid)
        {

            string type = "Batch_DisbursementFarmer";           
            MdlEncoreDisbursementResponse ObjMdlEncoreDisbursementResponse = new MdlEncoreDisbursementResponse();
            try
            {
                List<string> farmer_list = new List<string>();

                List<string> disbursementbookingencore_status_list = new List<string>();

                msSQL = " select farmercontact_gid,disbursementbookingencore_status " +
                       " from ocs_trn_tfarmercontact " +
                       " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                disbursementbookingencore_status_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("disbursementbookingencore_status")).ToList();
                if (disbursementbookingencore_status_list.Contains("N") == false)
                {
                    values.status = false;
                    values.message = " Disbursement Booking Request Completed for current Farmer list";
                    return;
                }

                msSQL = " select farmercontact_gid " +
                        " from ocs_trn_tfarmercontact " +
                        " where urn_status='Yes' and encoreaccintegration_status='Y' and disbursementbookingencore_status='N' and rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                farmer_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("farmercontact_gid")).ToList();
                farmer_count = 0;
                total_farmer = farmer_list.Count();
                if (total_farmer == 0)
                {
                    values.status = false;
                    values.message = " No Eligible record for Disbursement Booking . Kindly Complete both Customer Creation and Account Creation in current farmer list";
                    return;
                }
                foreach (var farmer in farmer_list)
                {
                    MdlEncoreDisbursementRequest ObjMdlEncoreDisbursementRequest = new MdlEncoreDisbursementRequest();

                    lsfarmer = farmer;

                    if (!(String.IsNullOrEmpty(farmer)))
                    {
                        LogForAuditEncoreIntegration("Batch - Disbursement Booking Request Initiated for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                        msSQL = " select farmercontact_gid,encore_accountid,urn,creditopscheckerdisbursement_amount,bankaccount_number, " +
                             " accountholder_name, " +
                             " concat('accountCode:','','|bankName:',bank_name,'|bankBranchDetails:',branch_address,'|bankDisburse:','true') as reference, " +
                             " ifsc_code from ocs_trn_tfarmercontact " +
                             " where farmercontact_gid='" + farmer + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            ObjMdlEncoreDisbursementRequest.transaction.accountId = objODBCDatareader["encore_accountid"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.customerId = objODBCDatareader["urn"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.amount1 = objODBCDatareader["creditopscheckerdisbursement_amount"].ToString().Replace(",", "");
                            ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId = objODBCDatareader["bankaccount_number"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.param5 = objODBCDatareader["accountholder_name"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.reference = objODBCDatareader["reference"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.param4 = objODBCDatareader["ifsc_code"].ToString();

                        }
                        objODBCDatareader.Close();

                        msSQL = " SELECT encoredisbursementrequestmaster_gid,transactionName,userId,instrument,remarks,description,part1,part2,part3,part4,part5,part6,part7,part8, " +
                                " safeMode,runReversedTransactions FROM ocs_mst_tencoredisbursementrequestmaster where encoredisbursementrequestmaster_gid='" + ConfigurationManager.AppSettings["disbursementrequestmastertemplateid"].ToString() + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows)
                        {
                            ObjMdlEncoreDisbursementRequest.transaction.transactionName = objODBCDatareader["transactionName"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.userId = objODBCDatareader["userId"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.instrument = values.instrument;
                            ObjMdlEncoreDisbursementRequest.transaction.remarks = objODBCDatareader["remarks"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.description = objODBCDatareader["description"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part1 = objODBCDatareader["part1"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part2 = objODBCDatareader["part2"].ToString();

                            ObjMdlEncoreDisbursementRequest.transaction.part3 = objODBCDatareader["part3"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part4 = objODBCDatareader["part4"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part5 = objODBCDatareader["part5"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part6 = objODBCDatareader["part6"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part7 = objODBCDatareader["part7"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part8 = objODBCDatareader["part8"].ToString();
                            ObjMdlEncoreDisbursementRequest.safeMode = objODBCDatareader["safeMode"].ToString();

                            ObjMdlEncoreDisbursementRequest.runReversedTransactions = objODBCDatareader["runReversedTransactions"].ToString();

                            ObjMdlEncoreDisbursementRequest.transaction.valueDateStr = "2022-12-01";
                        }
                        objODBCDatareader.Close();

                        string lsdisbursementreqst_json = Newtonsoft.Json.JsonConvert.SerializeObject(ObjMdlEncoreDisbursementRequest);

                        msGetGidDisReq = objcmnfunctions.GetMasterGID("ENDR");
                        msSQL = " insert into ocs_trn_tencoredisbursementrequest(" +
                                " encoredisbursementrequest_gid," +
                                " farmercontact_gid," +
                                " request_time," +
                                " requested_by," +
                                " accountId," +
                                " customerId," +
                                " amount1," +
                                " transactionName," +
                                " param2Str," +
                                " userId," +
                                " valueDateStr," +
                                " instrument," +
                                " remarks," +
                                " description," +
                                " transactionDateStr," +
                                " part1," +
                                " part2," +
                                " part3," +
                                " part4," +
                                " part5," +
                                " part6," +
                                " part7," +
                                " part8," +
                                " payeeAccountId," +
                                " param5," +
                                " reference," +
                                " param4," +
                                " safeMode," +
                                " runReversedTransactions," +
                                " json_string) " +
                                " values(" +
                                "'" + msGetGidDisReq + "'," +
                                "'" + farmer + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + employee_gid + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.accountId + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.customerId + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.amount1 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.transactionName + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.param2Str + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.userId + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.valueDateStr + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.instrument + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.remarks + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.description + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.transactionDateStr + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part1 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part2 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part3 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part4 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part5 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part6 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part7 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part8 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.param5 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.reference + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.param4 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.safeMode + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.runReversedTransactions + "'," +
                                "'" + lsdisbursementreqst_json + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 1)
                        {
                            LogForAuditEncoreIntegration("Error occurred while storing disbursement booking request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }
                        try
                        {
                            var client = new RestClient(ConfigurationManager.AppSettings["encore_disbursementurl"].ToString());
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                            string val = System.Convert.ToBase64String(plainTextBytes);
                            request.AddHeader("Authorization", "Basic " + val);

                            var body = lsdisbursementreqst_json;

                            request.AddParameter("application/json", body, ParameterType.RequestBody);

                            IRestResponse response = client.Execute(request);

                            
                            lsresponsecontentdisbursement = response.Content;
                            lsresponseStatusCodedisbursement = response.StatusCode.ToString();

                        }
                        catch (Exception ex)
                        {
                            LogForAuditEncoreIntegration("Error occurred while hitting disbursement booking URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentdisbursement + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }
                        if (lsresponseStatusCodedisbursement == "OK")
                        {
                            

                            msGetGidDisResp = objcmnfunctions.GetMasterGID("EDRS");
                            msSQL = " insert into ocs_trn_tencoredisbursementresponse(" +
                                    " encoredisbursementresponse_gid," +
                                    " encoredisbursementrequest_gid," +
                                    " response_time," +
                                    " json_string) " +
                                    " values(" +
                                    "'" + msGetGidDisResp + "'," +
                                    "'" + msGetGidDisReq + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + lsresponsecontentdisbursement + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            if (mnResult != 0)
                            {
                                msSQL = "update ocs_trn_tfarmercontact set disbursementbookingencore_status='Y' where farmercontact_gid='" + farmer + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                LogForAuditEncoreIntegration("Disbursement Booking Request Ended for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                farmer_count = farmer_count + 1;
                            }
                            else
                            {
                                LogForAuditEncoreIntegration("Error occurred while storing disbursement response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                continue;
                            }

                        }
                        else if ((lsresponseStatusCodedisbursement == "InternalServerError"))
                        {                            
                            LogForAuditEncoreIntegration("Internal Server Error . Response Content - " + lsresponsecontentdisbursement + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }
                        else
                        {
                            LogForAuditEncoreIntegration("Failed to receive 200 or 500 Response  . Response Content - " + lsresponsecontentdisbursement + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            values.status = false;
                            values.message = "" + lsresponseStatusCodedisbursement + " Response from Encore. Totally " +
                                             "" + farmer_count.ToString() + " Of " + total_farmer.ToString() + " Successful Disbursement Booking Response received from Encore";                            
                            return;
                        }
                    }
                }
                
                values.status = true;
                values.message = farmer_count.ToString() + " Of " + total_farmer.ToString() + " Successful Disbursement Booking Response received from Encore ";


            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = " Error occurred while posting batch disbursement booking request to Encore ";
                LogForAuditEncoreIntegration("Error occurred while posting batch disbursement booking request to Encore .Farmer ID - " + lsfarmer + ". Exception - " + ex + " .Response Content(If Exists) - " + lsresponsecontentdisbursement + " at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
            }

        }

        //Batch Disbursement booking in Encore for Supplier
        public void DaBatchEncoreDisbursementSupplier(MdlEncoreDisbursement values, string employee_gid)
        {

            string type = "Batch_DisbursementSupplier";
            MdlEncoreDisbursementResponse ObjMdlEncoreDisbursementResponse = new MdlEncoreDisbursementResponse();
            try
            {
                List<string> supplier_list = new List<string>();

                msSQL = " select encoreintegration_status from ocs_trn_trmdisbursementrequest where rmdisbursementrequest_gid ='" + values.rmdisbursementrequest_gid + "'";
                lsencoreintegration_status = objdbconn.GetExecuteScalar(msSQL);
                if (lsencoreintegration_status == "N")
                {
                    values.status = false;
                    values.message = "Loan Account Creation Pending for Guaranter";
                    return;
                }

                msSQL = " select disbursementsupplier_gid " +
                       " from ocs_trn_tdisbursementsupplier " +
                       " where disbursementbookingencore_status='N' and rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                supplier_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("disbursementsupplier_gid")).ToList();
                supplier_count = 0;
                total_supplier = supplier_list.Count();
                if (total_supplier == 0)
                {
                    values.status = false;
                    values.message = " Disbursement Booking Request Completed for current Supplier list";
                    return;
                }
                foreach (var supplier in supplier_list)
                {
                    MdlEncoreDisbursementRequest ObjMdlEncoreDisbursementRequest = new MdlEncoreDisbursementRequest();

                    lssupplier = supplier;

                    if (!(String.IsNullOrEmpty(supplier)))
                    {
                        LogForAuditEncoreIntegration("Batch - Disbursement Booking Request Initiated for Supplier. Application ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                        msSQL = " select application_gid,rmdisbursementrequest_gid,disbursementsupplier_gid,creditopscheckerdisbursement_amount, " +
                                " bankaccount_number,accountholder_name," +
                                " concat('accountCode:','','|bankName:',bank_name,'|bankBranchDetails:',branch_name,' ',branch_address,'|bankDisburse:','true') as reference, " +
                                " ifsc_code from ocs_trn_tdisbursementsupplier " +
                                " where disbursementsupplier_gid='" + supplier + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsdissuplrapplication_gid = objODBCDatareader["application_gid"].ToString();
                            lsdissuplrrmdisbursementrequest_gid = objODBCDatareader["rmdisbursementrequest_gid"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.amount1 = objODBCDatareader["creditopscheckerdisbursement_amount"].ToString().Replace(",", "");
                            ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId = objODBCDatareader["bankaccount_number"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.param5 = objODBCDatareader["accountholder_name"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.reference = objODBCDatareader["reference"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.param4 = objODBCDatareader["ifsc_code"].ToString();

                        }
                        objODBCDatareader.Close();

                        msSQL = " select encore_accountid " +
                                " from ocs_trn_trmdisbursementrequest " +
                                " where rmdisbursementrequest_gid='" + lsdissuplrrmdisbursementrequest_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ObjMdlEncoreDisbursementRequest.transaction.accountId = objODBCDatareader["encore_accountid"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select customer_urn " +
                                " from ocs_trn_tcadapplication " +
                                " where application_gid='" + lsdissuplrapplication_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ObjMdlEncoreDisbursementRequest.transaction.customerId = objODBCDatareader["customer_urn"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " SELECT encoredisbursementrequestmaster_gid,transactionName,userId,instrument,remarks,description,part1,part2,part3,part4,part5,part6,part7,part8, " +
                                    " safeMode,runReversedTransactions FROM ocs_mst_tencoredisbursementrequestmaster where encoredisbursementrequestmaster_gid='" + ConfigurationManager.AppSettings["disbursementrequestmastertemplateid"].ToString() + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows)
                        {
                            ObjMdlEncoreDisbursementRequest.transaction.transactionName = objODBCDatareader["transactionName"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.userId = objODBCDatareader["userId"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.instrument = values.instrument;
                            ObjMdlEncoreDisbursementRequest.transaction.remarks = objODBCDatareader["remarks"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.description = objODBCDatareader["description"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part1 = objODBCDatareader["part1"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part2 = objODBCDatareader["part2"].ToString();

                            ObjMdlEncoreDisbursementRequest.transaction.part3 = objODBCDatareader["part3"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part4 = objODBCDatareader["part4"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part5 = objODBCDatareader["part5"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part6 = objODBCDatareader["part6"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part7 = objODBCDatareader["part7"].ToString();
                            ObjMdlEncoreDisbursementRequest.transaction.part8 = objODBCDatareader["part8"].ToString();
                            ObjMdlEncoreDisbursementRequest.safeMode = objODBCDatareader["safeMode"].ToString();

                            ObjMdlEncoreDisbursementRequest.runReversedTransactions = objODBCDatareader["runReversedTransactions"].ToString();

                            ObjMdlEncoreDisbursementRequest.transaction.valueDateStr = "2022-12-01";
                        }
                        objODBCDatareader.Close();

                        string lsdisbursementreqst_json = Newtonsoft.Json.JsonConvert.SerializeObject(ObjMdlEncoreDisbursementRequest);

                        msGetGidDisReq = objcmnfunctions.GetMasterGID("ENDR");
                        msSQL = " insert into ocs_trn_tencoredisbursementrequest(" +
                                " encoredisbursementrequest_gid," +
                                " disbursementsupplier_gid," +
                                " request_time," +
                                " requested_by," +
                                " accountId," +
                                " customerId," +
                                " amount1," +
                                " transactionName," +
                                " param2Str," +
                                " userId," +
                                " valueDateStr," +
                                " instrument," +
                                " remarks," +
                                " description," +
                                " transactionDateStr," +
                                " part1," +
                                " part2," +
                                " part3," +
                                " part4," +
                                " part5," +
                                " part6," +
                                " part7," +
                                " part8," +
                                " payeeAccountId," +
                                " param5," +
                                " reference," +
                                " param4," +
                                " safeMode," +
                                " runReversedTransactions," +
                                " json_string) " +
                                " values(" +
                                "'" + msGetGidDisReq + "'," +
                                "'" + supplier + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + employee_gid + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.accountId + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.customerId + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.amount1 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.transactionName + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.param2Str + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.userId + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.valueDateStr + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.instrument + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.remarks + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.description + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.transactionDateStr + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part1 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part2 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part3 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part4 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part5 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part6 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part7 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.part8 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.payeeAccountId + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.param5 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.reference + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.transaction.param4 + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.safeMode + "'," +
                                "'" + ObjMdlEncoreDisbursementRequest.runReversedTransactions + "'," +
                                "'" + lsdisbursementreqst_json + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 1)
                        {
                            LogForAuditEncoreIntegration("Error occurred while storing disbursement booking request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }
                        try
                        {
                            var client = new RestClient(ConfigurationManager.AppSettings["encore_disbursementurl"].ToString());
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                            string val = System.Convert.ToBase64String(plainTextBytes);
                            request.AddHeader("Authorization", "Basic " + val);

                            var body = lsdisbursementreqst_json;

                            request.AddParameter("application/json", body, ParameterType.RequestBody);

                            IRestResponse response = client.Execute(request);

                            lsresponsecontentdisbursement = response.Content;
                            lsresponseStatusCodedisbursement = response.StatusCode.ToString();

                        }
                        catch (Exception ex)
                        {
                            LogForAuditEncoreIntegration("Error occurred while hitting disbursement booking URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentdisbursement + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }
                        if (lsresponseStatusCodedisbursement == "OK")
                        {
                            

                            msGetGidDisResp = objcmnfunctions.GetMasterGID("EDRS");
                            msSQL = " insert into ocs_trn_tencoredisbursementresponse(" +
                                    " encoredisbursementresponse_gid," +
                                    " encoredisbursementrequest_gid," +
                                    " response_time," +
                                    " json_string) " +
                                    " values(" +
                                    "'" + msGetGidDisResp + "'," +
                                    "'" + msGetGidDisReq + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + lsresponsecontentdisbursement + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            if (mnResult != 0)
                            {
                                msSQL = "update ocs_trn_tdisbursementsupplier set disbursementbookingencore_status='Y' where disbursementsupplier_gid='" + supplier + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                LogForAuditEncoreIntegration("Batch - Disbursement Booking Request Ended for Individual Supplier . Supplier ID - " + supplier + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                supplier_count = supplier_count + 1;
                            }
                            else
                            {
                                LogForAuditEncoreIntegration("Error occurred while storing disbursement booking response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                continue;
                            }

                        }
                        else if ((lsresponseStatusCodedisbursement == "InternalServerError"))
                        {
                            LogForAuditEncoreIntegration("Internal Server Error . Response Content - " + lsresponsecontentdisbursement + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }
                        else
                        {
                            LogForAuditEncoreIntegration("Failed to receive 200 or 500 Response  . Response Content - " + lsresponsecontentdisbursement + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            values.status = false;
                            values.message = "" + lsresponseStatusCodedisbursement + " Response from Encore. Totally " +
                                             "" + supplier_count.ToString() + " Of " + total_supplier.ToString() + " Successful Disbursement Booking Response received from Encore";                        
                            return;
                        }
                    }
                }

                values.status = true;
                values.message = supplier_count.ToString() + " Of " + total_supplier.ToString() + " Successful Disbursement Booking Response received from Encore ";


            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = " Error occurred while posting batch disbursement booking request to Encore ";
                LogForAuditEncoreIntegration("Error occurred while posting batch disbursement booking request to Encore .Farmer ID - " + lsfarmer + ". Exception - " + ex + " .Response Content(If Exists) - " + lsresponsecontentdisbursement + " at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
            }

        }

        //Auxillary Functions
        public void LogForAuditEncoreIntegration(string strVal, string type)
        {
            try
            {
        
                    loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamFin/EncoreIntegration/" + type + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    if ((!System.IO.Directory.Exists(loglspath)))
                        System.IO.Directory.CreateDirectory(loglspath);
                    if (logFileName == "")

                    {
                        logFileName = "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
                    }
                    loglspath = loglspath + logFileName;
              

                System.IO.StreamWriter sw = new System.IO.StreamWriter(loglspath, true);
                sw.WriteLine(strVal);
                sw.Close();

            }
            catch (Exception ex)
            {
            }
        }

        //Auxillary Functions
        public string fetchCompanyCode()
        {
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            string lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            return lscompany_code;
        }
    }
}