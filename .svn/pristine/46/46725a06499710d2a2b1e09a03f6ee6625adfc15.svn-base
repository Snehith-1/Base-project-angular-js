using ems.hbapiconn.Models;
using ems.utilities.Functions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;

namespace ems.hbapiconn.Functions
{
    public class FnSamFinEncoreLoanAccount
    {
        string lsresponsecontentopenloanaccount;
        string msSQL, lsinstitution_gid, lscontact_gid,msGetGidOpLnResp, msGetGidOpLnReq;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();

        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        int mnResult, mnResultContact, mnResultAddress;
        string loglspath = "", logFileName = "", lsloanTenure_unit,lsprogram_tenure, lsfarmer, lsresponseStatusCodeopenloanaccount;
        int farmer_count, total_farmer, supplier_count, total_supplier;
        string lsfacilityvalidity_days, lsfacilityvalidity_month, lsfacilityvalidity_year;

        public MdlCreateLoanResponse CreateLoanAccount(MdlCreateLoanRequest values, string employee_gid)
        {
            string type = "CreateLoanAccount";
            MdlCreateLoanResponse objMdlCreateLoanResponse = new MdlCreateLoanResponse();
            try
            {
                LogForAuditEncoreIntegration("Logging Started for Loan Account Creation - Applicant; Params - " + values.application_gid + " " + values.application2sanction_gid + " " + values.application2loan_gid + " " + values.customer_urn + " " + values.rmdisbursementrequest_gid, type);
                string lsapplicant_type = "";                

                MdlOpenLoanAcccountRequest objMdlOpenLoanAcccountRequest = new MdlOpenLoanAcccountRequest();

                lsapplicant_type = FetchApplicantType(values.application_gid);

                if (lsapplicant_type == ApplicantType.Institution)
                    objMdlOpenLoanAcccountRequest = populateOpenLoanAccountRequestInstitution(values);
                else
                    objMdlOpenLoanAcccountRequest = populateOpenLoanAccountRequestIndividual(values);

                LogForAuditEncoreIntegration("Institution/Individual details populated", type);

                ProductCodeValidationResponse objProductCodeValidationResponse = new ProductCodeValidationResponse();

                objProductCodeValidationResponse = validateProductCodeTenureUnitbased(values.application2loan_gid);

                if (objProductCodeValidationResponse.status == false && objProductCodeValidationResponse.loanTenure_unit == "InValid")
                {                   
                    objMdlCreateLoanResponse.status = false;
                    objMdlCreateLoanResponse.message = "Invalid Tenure Units. Hence Loan account cannot be created.";
                    return objMdlCreateLoanResponse;
                }

                if (objProductCodeValidationResponse.status == false)
                {
                    objMdlCreateLoanResponse.status = false;
                    objMdlCreateLoanResponse.message = "Product Terms doesn't match with those available in Encore. Hence Loan account cannot be created.";
                    return objMdlCreateLoanResponse;
                }

                objMdlOpenLoanAcccountRequest.productCode = objProductCodeValidationResponse.product_code;

                objMdlOpenLoanAcccountRequest.tenureUnit = objProductCodeValidationResponse.loanTenure_unit;

                lsloanTenure_unit = objProductCodeValidationResponse.loanTenure_unit;

                LogForAuditEncoreIntegration("Product code validated", type);

                msSQL = " select checkerdisbursement_amount from ocs_trn_tdisbursementamount " +
                            " where application_gid = '" + values.application_gid + "' and rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                

                objMdlOpenLoanAcccountRequest.amountMagnitude = objdbconn.GetExecuteScalar(msSQL);
                objMdlOpenLoanAcccountRequest.amountMagnitude = objMdlOpenLoanAcccountRequest.amountMagnitude.Replace(",", "");

                objMdlOpenLoanAcccountRequest.branchCode = "HO";
                objMdlOpenLoanAcccountRequest.openedOnDate = "2022-12-01";


                objMdlOpenLoanAcccountRequest.tenureMagnitude = fetchProgramTenureUnitBased(values.application2loan_gid,lsloanTenure_unit);
               

                msSQL = "select rate_interest,penal_interest from ocs_trn_tcadapplication2loan where application2loan_gid = '" + values.application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objMdlOpenLoanAcccountRequest.normalInterestRate = objODBCDatareader["rate_interest"].ToString();
                    objMdlOpenLoanAcccountRequest.penalInterestRate = objODBCDatareader["penal_interest"].ToString();
                }

                msSQL = "select relationshipmanager_gid,relationshipmanager_name,customer_urn,constitution_name from ocs_trn_tcadapplication where application_gid = '" + values.application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objMdlOpenLoanAcccountRequest.openUserId = objODBCDatareader["relationshipmanager_gid"].ToString();
                    objMdlOpenLoanAcccountRequest.roName = objODBCDatareader["relationshipmanager_name"].ToString();
                    objMdlOpenLoanAcccountRequest.customerId1 = objODBCDatareader["customer_urn"].ToString();
                    objMdlOpenLoanAcccountRequest.customerType = objODBCDatareader["constitution_name"].ToString();
                }

                LogForAuditEncoreIntegration("Amount, Tenure, Interest and RM details obtained", type);

                objMdlOpenLoanAcccountRequest.currencyCode = "INR";

                objMdlOpenLoanAcccountRequest.udfText1 = "abc";
                objMdlOpenLoanAcccountRequest.udfText2 = "def";
                objMdlOpenLoanAcccountRequest.udfText3 = "ghi";
                objMdlOpenLoanAcccountRequest.udfText4 = "jkl";
                objMdlOpenLoanAcccountRequest.udfText5 = "mno";
                objMdlOpenLoanAcccountRequest.udfText6 = "pqr";
                objMdlOpenLoanAcccountRequest.udfText7 = "stu";
                objMdlOpenLoanAcccountRequest.udfText8 = "vwx";
                objMdlOpenLoanAcccountRequest.udfText9 = "yza";
                objMdlOpenLoanAcccountRequest.udfText10 = "bab";
                objMdlOpenLoanAcccountRequest.udfText11 = "dss";
                objMdlOpenLoanAcccountRequest.udfText12 = "kre";
                objMdlOpenLoanAcccountRequest.udfText13 = "swa";
                objMdlOpenLoanAcccountRequest.udfText14 = "nas";
                objMdlOpenLoanAcccountRequest.udfText15 = "clu";


                string MdlOpenLoanAcccountRequestJSON = JsonConvert.SerializeObject(objMdlOpenLoanAcccountRequest);

                LogForAuditEncoreIntegration("Request JSON formed", type);
                LogForAuditEncoreIntegration(MdlOpenLoanAcccountRequestJSON, type);
                LogForAuditEncoreIntegration("End of Request JSON", type);

                msGetGidOpLnReq = objcmnfunctions.GetMasterGID("OLAR");
                msSQL = " insert into ocs_trn_tencorecreateloanaccountrequest(" +
                          " encorecreateloanaccountrequest_gid," +
                          " application_gid," +
                          " application2sanction_gid, " +
                          " application2loan_gid, " +
                          " customer_urn," +
                          " rmdisbursementrequest_gid, " +
                          " customer1FirstName, " +
                          " customer1MiddleName," +
                          " customer1LastName, " +
                          " amountMagnitude, " +
                          " openedOnDate," +
                          " branchCode, " +
                          " productCode, " +
                          " tenureMagnitude," +
                          " tenureUnit, " +
                          " normalInterestRate, " +
                          " penalInterestRate," +
                          " openUserId, " +
                          " roName, " +
                          " customerId1, " +
                          " customerType, " +
                          " udfText1," +
                          " udfText2, " +
                          " udfText3, " +
                          " udfText4," +
                          " udfText5, " +
                          " udfText6, " +
                          " udfText7, " +
                          " udfText8, " +
                          " udfText9," +
                          " udfText10, " +
                          " udfText11, " +
                          " udfText12," +
                          " udfText13, " +
                          " udfText14, " +
                          " udfText15, " +
                          " guarantorCustomerId1, " +
                          " json_string, " +
                          " requested_by, " +
                          " request_time) " +
                          " values(" +
                          "'" + msGetGidOpLnReq + "'," +
                          "'" + values.application_gid + "'," +
                          "'" + values.application2sanction_gid + "'," +
                          "'" + values.application2loan_gid + "'," +
                          "'" + values.customer_urn + "'," +
                          "'" + values.rmdisbursementrequest_gid + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customer1FirstName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customer1MiddleName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customer1LastName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.amountMagnitude + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.openedOnDate + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.branchCode + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.productCode + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.tenureMagnitude + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.tenureUnit + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.normalInterestRate + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.penalInterestRate + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.openUserId + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.roName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customerId1 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customerType + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText1 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText2 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText3 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText4 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText5 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText6 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText7 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText8 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText9 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText10 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText11 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText12 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText13 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText14 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText15 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.guarantorCustomerId1 + "'," +
                          "'" + MdlOpenLoanAcccountRequestJSON + "'," +
                          "'" + employee_gid + "'," +                      
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                MdlOpenLoanAccountResponse objMdlOpenLoanAccountResponse = new MdlOpenLoanAccountResponse();

                var client = new RestClient(ConfigurationManager.AppSettings["encore_openloanaccounturl"].ToString());
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                string val = System.Convert.ToBase64String(plainTextBytes);
                request.AddHeader("Authorization", "Basic " + val);


                request.AddParameter("application/json", MdlOpenLoanAcccountRequestJSON, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                lsresponsecontentopenloanaccount = response.Content;

                LogForAuditEncoreIntegration("Response obtained", type);
                LogForAuditEncoreIntegration(response.Content, type);
                LogForAuditEncoreIntegration("End of Response", type);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    objMdlOpenLoanAccountResponse = JsonConvert.DeserializeObject<MdlOpenLoanAccountResponse>(lsresponsecontentopenloanaccount);

                    msGetGidOpLnResp = objcmnfunctions.GetMasterGID("OLAR");
                    msSQL = " insert into ocs_trn_tencoreopenloanaccountresponse(" +
                              " encoreopenloanaccountresponse_gid," +
                              " encoreopenloanaccountrequest_gid," +
                              " response_time, " +
                              " transactionId, " +
                              " valueDate," +
                              " valueDateStr, " +
                              " transactionDate, " +
                              " transactionDateStr," +
                              " systemDateAndTime, " +
                              " systemDateAndTimeStr, " +
                              " accountId," +
                              " entityId, " +
                              " sequenceNum, " +
                              " transactionName," +
                              " part1, " +
                              " part2, " +
                              " part3," +
                              " part4, " +
                              " part5, " +
                              " part6, " +
                              " part7, " +
                              " part8," +
                              " amount1, " +
                              " amount2, " +
                              " amount3," +
                              " description, " +
                              " userId, " +
                              " customerName, " +
                              " status, " +
                              " responseCode," +
                              " response, " +
                              " payeeAccountId, " +
                              " param1," +
                              " param2, " +
                              " param2Str, " +
                              " param3, " +
                              " param4, " +
                              " param5," +
                              " instrument, " +
                              " reference, " +
                              " transactionLotId," +
                              " currencyCode, " +
                              " originalTransactionDate, " +
                              " originalTransactionDateStr, " +
                              " originalTransactionId, " +
                              " originalUserId) " +
                              " values(" +
                              "'" + msGetGidOpLnResp + "'," +
                              "'" + msGetGidOpLnReq + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.valueDate + "'," +
                              "'" + objMdlOpenLoanAccountResponse.valueDateStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionDate + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionDateStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.systemDateAndTime + "'," +
                              "'" + objMdlOpenLoanAccountResponse.systemDateAndTimeStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.accountId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.entityId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.sequenceNum + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionName + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part1 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part2 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part3 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part4 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part5 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part6 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part7 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part8 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.amount1 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.amount2 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.amount3 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.description + "'," +
                              "'" + objMdlOpenLoanAccountResponse.userId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.customerName + "'," +
                              "'" + objMdlOpenLoanAccountResponse.status + "'," +
                              "'" + objMdlOpenLoanAccountResponse.responseCode + "'," +
                              "'" + objMdlOpenLoanAccountResponse.response + "'," +
                              "'" + objMdlOpenLoanAccountResponse.payeeAccountId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param1 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param2 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param2Str + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param3 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param4 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param5 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.instrument + "'," +
                              "'" + objMdlOpenLoanAccountResponse.reference + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionLotId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.currencyCode + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalTransactionDate + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalTransactionDateStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalTransactionId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalUserId + "')";
                    
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_trmdisbursementrequest set " + 
                            " encoreintegration_status = 'Y'," +
                            " encore_accountid='" + objMdlOpenLoanAccountResponse.accountId + "'" +
                            " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    LogForAuditEncoreIntegration("Loan Account Created in Encore Successfully", type);

                    objMdlCreateLoanResponse.status = true;
                    objMdlCreateLoanResponse.message = "Loan Account Created in Encore Successfully..!";

                }
                else
                {
                    MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                    objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(response.Content);

                    LogForAuditEncoreIntegration("Loan Account Creation in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.message, type);

                    objMdlCreateLoanResponse.status = false;
                    objMdlCreateLoanResponse.message = "Loan Account Creation in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.message;
                }

            }
            catch (Exception ex) 
            {
                LogForAuditEncoreIntegration("Exception occurred in Loan Account Creation\nException Message: " + ex.ToString(), type);

                objMdlCreateLoanResponse.status = false; ;
                objMdlCreateLoanResponse.message = "Exception occurred in Loan Account Creation\nException Message: " + ex.ToString();
            }

            LogForAuditEncoreIntegration("End of logging\r\n", type);
            return objMdlCreateLoanResponse;

        }

        public MdlCreateLoanResponse CreateLoanAccountFarmer(MdlCreateLoanRequest values, string employee_gid)
        {
            string type = "CreateLoanAccount";
            MdlCreateLoanResponse objMdlCreateLoanResponse = new MdlCreateLoanResponse();
            try
            {
                LogForAuditEncoreIntegration("Logging Started for Loan Account Creation - Farmer; Params - " + values.application_gid + " " + values.application2sanction_gid + " " + values.application2loan_gid + " " + values.customer_urn + " " + values.rmdisbursementrequest_gid + " " + values.farmercontact_gid, type);

                MdlOpenLoanAcccountRequest objMdlOpenLoanAcccountRequest = new MdlOpenLoanAcccountRequest();
               
                objMdlOpenLoanAcccountRequest = populateOpenLoanAccountRequestFarmer(values);

                LogForAuditEncoreIntegration("Farmer details populated",type);

                ProductCodeValidationResponse objProductCodeValidationResponse = new ProductCodeValidationResponse();

                objProductCodeValidationResponse = validateProductCodeTenureUnitbased(values.application2loan_gid);

                if (objProductCodeValidationResponse.status == false && objProductCodeValidationResponse.loanTenure_unit == "InValid")
                {
                    objMdlCreateLoanResponse.status = false;
                    objMdlCreateLoanResponse.message = "Invalid Tenure Units. Hence Loan account cannot be created.";
                    return objMdlCreateLoanResponse;
                }


                if (objProductCodeValidationResponse.status == false)
                {
                    objMdlCreateLoanResponse.status = false;
                    objMdlCreateLoanResponse.message = "Product Terms doesn't match with those available in Encore. Hence Loan account cannot be created.";
                    return objMdlCreateLoanResponse;
                }

                objMdlOpenLoanAcccountRequest.productCode = objProductCodeValidationResponse.product_code;

                objMdlOpenLoanAcccountRequest.tenureUnit = objProductCodeValidationResponse.loanTenure_unit;

                lsloanTenure_unit = objProductCodeValidationResponse.loanTenure_unit;

                LogForAuditEncoreIntegration("Product code validated", type);

                msSQL = " select disbursement_amount,creditopsdisbursement_amount,creditopscheckerdisbursement_amount from ocs_trn_tfarmercontact a " +
                        " where farmercontact_gid='" + values.farmercontact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    if (!String.IsNullOrEmpty(objODBCDatareader["creditopscheckerdisbursement_amount"].ToString()))
                    {
                        objMdlOpenLoanAcccountRequest.amountMagnitude = objODBCDatareader["creditopscheckerdisbursement_amount"].ToString().Replace(",","");
                    }
                    else if(!String.IsNullOrEmpty(objODBCDatareader["creditopsdisbursement_amount"].ToString()))
                    {
                        objMdlOpenLoanAcccountRequest.amountMagnitude = objODBCDatareader["creditopsdisbursement_amount"].ToString().Replace(",", "");
                    }
                    else
                    {
                        objMdlOpenLoanAcccountRequest.amountMagnitude = objODBCDatareader["disbursement_amount"].ToString();
                    }
                }


                objMdlOpenLoanAcccountRequest.branchCode = "HO";
                objMdlOpenLoanAcccountRequest.openedOnDate = "2022-12-01";


                objMdlOpenLoanAcccountRequest.tenureMagnitude = fetchProgramTenureUnitBased(values.application2loan_gid,lsloanTenure_unit);


                msSQL = "select rate_interest,penal_interest from ocs_trn_tcadapplication2loan where application2loan_gid = '" + values.application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objMdlOpenLoanAcccountRequest.normalInterestRate = objODBCDatareader["rate_interest"].ToString();
                    objMdlOpenLoanAcccountRequest.penalInterestRate = objODBCDatareader["penal_interest"].ToString();
                }

                msSQL = " select a.created_by as relationshipmanager_gid,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as relationshipmanager_name," + 
                        " urn from ocs_trn_tfarmercontact a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where farmercontact_gid = '" + values.farmercontact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objMdlOpenLoanAcccountRequest.openUserId = objODBCDatareader["relationshipmanager_gid"].ToString();
                    objMdlOpenLoanAcccountRequest.roName = objODBCDatareader["relationshipmanager_name"].ToString();
                    objMdlOpenLoanAcccountRequest.customerId1 = objODBCDatareader["urn"].ToString();
                }

                LogForAuditEncoreIntegration("Amount, Tenure, Interest and RM details obtained", type);

                objMdlOpenLoanAcccountRequest.currencyCode = "INR";

                objMdlOpenLoanAcccountRequest.udfText1 = "abc";
                objMdlOpenLoanAcccountRequest.udfText2 = "def";
                objMdlOpenLoanAcccountRequest.udfText3 = "ghi";
                objMdlOpenLoanAcccountRequest.udfText4 = "jkl";
                objMdlOpenLoanAcccountRequest.udfText5 = "mno";
                objMdlOpenLoanAcccountRequest.udfText6 = "pqr";
                objMdlOpenLoanAcccountRequest.udfText7 = "stu";
                objMdlOpenLoanAcccountRequest.udfText8 = "vwx";
                objMdlOpenLoanAcccountRequest.udfText9 = "yza";
                objMdlOpenLoanAcccountRequest.udfText10 = "bab";
                objMdlOpenLoanAcccountRequest.udfText11 = "dss";
                objMdlOpenLoanAcccountRequest.udfText12 = "kre";
                objMdlOpenLoanAcccountRequest.udfText13 = "swa";
                objMdlOpenLoanAcccountRequest.udfText14 = "nas";
                objMdlOpenLoanAcccountRequest.udfText15 = "clu";

                msSQL = " select customer_urn from ocs_trn_tcadapplication where application_gid = '" + values.application_gid + "'";
                objMdlOpenLoanAcccountRequest.guarantorCustomerId1 = objdbconn.GetExecuteScalar(msSQL);

                string MdlOpenLoanAcccountRequestJSON = JsonConvert.SerializeObject(objMdlOpenLoanAcccountRequest);

                LogForAuditEncoreIntegration("Request JSON formed", type);

                msGetGidOpLnReq = objcmnfunctions.GetMasterGID("OLAR");
                msSQL = " insert into ocs_trn_tencorecreateloanaccountrequest(" +
                          " encorecreateloanaccountrequest_gid," +
                          " application_gid," +
                          " application2sanction_gid, " +
                          " application2loan_gid, " +
                          " customer_urn," +
                          " rmdisbursementrequest_gid, " +
                          " farmercontact_gid, " +
                          " customer1FirstName, " +
                          " customer1MiddleName," +
                          " customer1LastName, " +
                          " amountMagnitude, " +
                          " openedOnDate," +
                          " branchCode, " +
                          " productCode, " +
                          " tenureMagnitude," +
                          " tenureUnit, " +
                          " normalInterestRate, " +
                          " penalInterestRate," +
                          " openUserId, " +
                          " roName, " +
                          " customerId1, " +
                          " customerType, " +
                          " udfText1," +
                          " udfText2, " +
                          " udfText3, " +
                          " udfText4," +
                          " udfText5, " +
                          " udfText6, " +
                          " udfText7, " +
                          " udfText8, " +
                          " udfText9," +
                          " udfText10, " +
                          " udfText11, " +
                          " udfText12," +
                          " udfText13, " +
                          " udfText14, " +
                          " udfText15, " +
                          " guarantorCustomerId1, " +
                          " json_string, " +
                          " requested_by, " +
                          " request_time) " +
                          " values(" +
                          "'" + msGetGidOpLnReq + "'," +
                          "'" + values.application_gid + "'," +
                          "'" + values.application2sanction_gid + "'," +
                          "'" + values.application2loan_gid + "'," +
                          "'" + values.customer_urn + "'," +
                          "'" + values.rmdisbursementrequest_gid + "'," +
                          "'" + values.farmercontact_gid + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customer1FirstName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customer1MiddleName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customer1LastName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.amountMagnitude + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.openedOnDate + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.branchCode + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.productCode + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.tenureMagnitude + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.tenureUnit + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.normalInterestRate + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.penalInterestRate + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.openUserId + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.roName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customerId1 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customerType + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText1 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText2 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText3 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText4 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText5 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText6 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText7 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText8 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText9 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText10 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText11 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText12 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText13 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText14 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText15 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.guarantorCustomerId1 + "'," +
                          "'" + MdlOpenLoanAcccountRequestJSON + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                MdlOpenLoanAccountResponse objMdlOpenLoanAccountResponse = new MdlOpenLoanAccountResponse();

                var client = new RestClient(ConfigurationManager.AppSettings["encore_openloanaccounturl"].ToString());
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                string val = System.Convert.ToBase64String(plainTextBytes);
                request.AddHeader("Authorization", "Basic " + val);


                request.AddParameter("application/json", MdlOpenLoanAcccountRequestJSON, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                lsresponsecontentopenloanaccount = response.Content;
                
                LogForAuditEncoreIntegration("Response obtained", type);
                LogForAuditEncoreIntegration(response.Content, type);
                LogForAuditEncoreIntegration("End of Response", type);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    objMdlOpenLoanAccountResponse = JsonConvert.DeserializeObject<MdlOpenLoanAccountResponse>(lsresponsecontentopenloanaccount);

                    msGetGidOpLnResp = objcmnfunctions.GetMasterGID("OLAR");
                    msSQL = " insert into ocs_trn_tencoreopenloanaccountresponse(" +
                              " encoreopenloanaccountresponse_gid," +
                              " encoreopenloanaccountrequest_gid," +
                              " response_time, " +
                              " transactionId, " +
                              " valueDate," +
                              " valueDateStr, " +
                              " transactionDate, " +
                              " transactionDateStr," +
                              " systemDateAndTime, " +
                              " systemDateAndTimeStr, " +
                              " accountId," +
                              " entityId, " +
                              " sequenceNum, " +
                              " transactionName," +
                              " part1, " +
                              " part2, " +
                              " part3," +
                              " part4, " +
                              " part5, " +
                              " part6, " +
                              " part7, " +
                              " part8," +
                              " amount1, " +
                              " amount2, " +
                              " amount3," +
                              " description, " +
                              " userId, " +
                              " customerName, " +
                              " status, " +
                              " responseCode," +
                              " response, " +
                              " payeeAccountId, " +
                              " param1," +
                              " param2, " +
                              " param2Str, " +
                              " param3, " +
                              " param4, " +
                              " param5," +
                              " instrument, " +
                              " reference, " +
                              " transactionLotId," +
                              " currencyCode, " +
                              " originalTransactionDate, " +
                              " originalTransactionDateStr, " +
                              " originalTransactionId, " +
                              " originalUserId) " +
                              " values(" +
                              "'" + msGetGidOpLnResp + "'," +
                              "'" + msGetGidOpLnReq + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.valueDate + "'," +
                              "'" + objMdlOpenLoanAccountResponse.valueDateStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionDate + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionDateStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.systemDateAndTime + "'," +
                              "'" + objMdlOpenLoanAccountResponse.systemDateAndTimeStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.accountId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.entityId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.sequenceNum + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionName + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part1 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part2 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part3 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part4 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part5 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part6 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part7 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part8 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.amount1 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.amount2 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.amount3 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.description + "'," +
                              "'" + objMdlOpenLoanAccountResponse.userId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.customerName + "'," +
                              "'" + objMdlOpenLoanAccountResponse.status + "'," +
                              "'" + objMdlOpenLoanAccountResponse.responseCode + "'," +
                              "'" + objMdlOpenLoanAccountResponse.response + "'," +
                              "'" + objMdlOpenLoanAccountResponse.payeeAccountId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param1 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param2 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param2Str + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param3 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param4 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param5 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.instrument + "'," +
                              "'" + objMdlOpenLoanAccountResponse.reference + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionLotId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.currencyCode + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalTransactionDate + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalTransactionDateStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalTransactionId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalUserId + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tfarmercontact set " +
                            " encoreaccintegration_status = 'Y'," +
                            " encore_accountid='" + objMdlOpenLoanAccountResponse.accountId + "'" +
                            " where farmercontact_gid='" + values.farmercontact_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    LogForAuditEncoreIntegration("Loan Account Created in Encore Successfully", type);

                    objMdlCreateLoanResponse.status = true;
                    objMdlCreateLoanResponse.message = "Loan Account Created in Encore Successfully..!";

                }
                else
                {
                    MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                    objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(response.Content);

                    LogForAuditEncoreIntegration("Loan Account Creation in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.message, type);

                    objMdlCreateLoanResponse.status = false;
                    objMdlCreateLoanResponse.message = "Loan Account Creation in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.message;
                }

            }
            catch (Exception ex)
            {
                LogForAuditEncoreIntegration("Exception occurred in Loan Account Creation\nException Message: " + ex.ToString(), type);

                objMdlCreateLoanResponse.status = false; ;
                objMdlCreateLoanResponse.message = "Exception occurred in Loan Account Creation\nException Message: " + ex.ToString();
            }
            LogForAuditEncoreIntegration("End of logging\r\n", type);
            return objMdlCreateLoanResponse;

        }

        public MdlCreateLoanResponse CreateLoanAccountSupplier(MdlCreateLoanRequest values, string employee_gid)
        {
            string type = "CreateLoanAccount";
            MdlCreateLoanResponse objMdlCreateLoanResponse = new MdlCreateLoanResponse();
            try
            {
                LogForAuditEncoreIntegration("Logging Started for Loan Account Creation - Applicant(Supplier); Params - " + values.application_gid + " " + values.application2sanction_gid + " " + values.application2loan_gid + " " + values.customer_urn + " " + values.rmdisbursementrequest_gid, type);

                string lsapplicant_type = "";

                MdlOpenLoanAcccountRequest objMdlOpenLoanAcccountRequest = new MdlOpenLoanAcccountRequest();


                lsapplicant_type = FetchApplicantType(values.application_gid);

                if (lsapplicant_type == ApplicantType.Institution)
                    objMdlOpenLoanAcccountRequest = populateOpenLoanAccountRequestInstitution(values);
                else
                    objMdlOpenLoanAcccountRequest = populateOpenLoanAccountRequestIndividual(values);

                LogForAuditEncoreIntegration("Institution/Individual details populated", type);

                ProductCodeValidationResponse objProductCodeValidationResponse = new ProductCodeValidationResponse();

                objProductCodeValidationResponse = validateProductCodeTenureUnitbased(values.application2loan_gid);

                if (objProductCodeValidationResponse.status == false && objProductCodeValidationResponse.loanTenure_unit == "InValid")
                {
                    objMdlCreateLoanResponse.status = false;
                    objMdlCreateLoanResponse.message = "Invalid Tenure Units. Hence Loan account cannot be created.";
                    return objMdlCreateLoanResponse;
                }

                if (objProductCodeValidationResponse.status == false)
                {
                    objMdlCreateLoanResponse.status = false;
                    objMdlCreateLoanResponse.message = "Product Terms doesn't match with those available in Encore. Hence Loan account cannot be created.";
                    return objMdlCreateLoanResponse;
                }

                objMdlOpenLoanAcccountRequest.productCode = objProductCodeValidationResponse.product_code;

                objMdlOpenLoanAcccountRequest.tenureUnit = objProductCodeValidationResponse.loanTenure_unit;

                lsloanTenure_unit = objProductCodeValidationResponse.loanTenure_unit;

                LogForAuditEncoreIntegration("Product code validated", type);

                long amountTotal = 0;

                msSQL = " select creditopscheckerdisbursement_amount from ocs_trn_tdisbursementsupplier " +
                        " where application_gid = '" + values.application_gid + "' and rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        amountTotal += long.Parse(dt["creditopscheckerdisbursement_amount"].ToString().Replace(",",""));
                    }
                }

                objMdlOpenLoanAcccountRequest.amountMagnitude = amountTotal.ToString();
;


                objMdlOpenLoanAcccountRequest.branchCode = "HO";
                objMdlOpenLoanAcccountRequest.openedOnDate = "2022-12-01";


                objMdlOpenLoanAcccountRequest.tenureMagnitude = fetchProgramTenureUnitBased(values.application2loan_gid, lsloanTenure_unit);


                msSQL = "select rate_interest,penal_interest from ocs_trn_tcadapplication2loan where application2loan_gid = '" + values.application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objMdlOpenLoanAcccountRequest.normalInterestRate = objODBCDatareader["rate_interest"].ToString();
                    objMdlOpenLoanAcccountRequest.penalInterestRate = objODBCDatareader["penal_interest"].ToString();
                }

                msSQL = "select relationshipmanager_gid,relationshipmanager_name,customer_urn,constitution_name from ocs_trn_tcadapplication where application_gid = '" + values.application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objMdlOpenLoanAcccountRequest.openUserId = objODBCDatareader["relationshipmanager_gid"].ToString();
                    objMdlOpenLoanAcccountRequest.roName = objODBCDatareader["relationshipmanager_name"].ToString();
                    objMdlOpenLoanAcccountRequest.customerId1 = objODBCDatareader["customer_urn"].ToString();
                    objMdlOpenLoanAcccountRequest.customerType = objODBCDatareader["constitution_name"].ToString();
                }

                LogForAuditEncoreIntegration("Amount, Tenure, Interest and RM details obtained", type);

                objMdlOpenLoanAcccountRequest.currencyCode = "INR";

                objMdlOpenLoanAcccountRequest.udfText1 = "abc";
                objMdlOpenLoanAcccountRequest.udfText2 = "def";
                objMdlOpenLoanAcccountRequest.udfText3 = "ghi";
                objMdlOpenLoanAcccountRequest.udfText4 = "jkl";
                objMdlOpenLoanAcccountRequest.udfText5 = "mno";
                objMdlOpenLoanAcccountRequest.udfText6 = "pqr";
                objMdlOpenLoanAcccountRequest.udfText7 = "stu";
                objMdlOpenLoanAcccountRequest.udfText8 = "vwx";
                objMdlOpenLoanAcccountRequest.udfText9 = "yza";
                objMdlOpenLoanAcccountRequest.udfText10 = "bab";
                objMdlOpenLoanAcccountRequest.udfText11 = "dss";
                objMdlOpenLoanAcccountRequest.udfText12 = "kre";
                objMdlOpenLoanAcccountRequest.udfText13 = "swa";
                objMdlOpenLoanAcccountRequest.udfText14 = "nas";
                objMdlOpenLoanAcccountRequest.udfText15 = "clu";


                string MdlOpenLoanAcccountRequestJSON = JsonConvert.SerializeObject(objMdlOpenLoanAcccountRequest);

                LogForAuditEncoreIntegration("Request JSON formed", type);
                LogForAuditEncoreIntegration(MdlOpenLoanAcccountRequestJSON, type);
                LogForAuditEncoreIntegration("End of Request JSON", type);

                msGetGidOpLnReq = objcmnfunctions.GetMasterGID("OLAR");
                msSQL = " insert into ocs_trn_tencorecreateloanaccountrequest(" +
                          " encorecreateloanaccountrequest_gid," +
                          " application_gid," +
                          " application2sanction_gid, " +
                          " application2loan_gid, " +
                          " customer_urn," +
                          " rmdisbursementrequest_gid, " +
                          " customer1FirstName, " +
                          " customer1MiddleName," +
                          " customer1LastName, " +
                          " amountMagnitude, " +
                          " openedOnDate," +
                          " branchCode, " +
                          " productCode, " +
                          " tenureMagnitude," +
                          " tenureUnit, " +
                          " normalInterestRate, " +
                          " penalInterestRate," +
                          " openUserId, " +
                          " roName, " +
                          " customerId1, " +
                          " customerType, " +
                          " udfText1," +
                          " udfText2, " +
                          " udfText3, " +
                          " udfText4," +
                          " udfText5, " +
                          " udfText6, " +
                          " udfText7, " +
                          " udfText8, " +
                          " udfText9," +
                          " udfText10, " +
                          " udfText11, " +
                          " udfText12," +
                          " udfText13, " +
                          " udfText14, " +
                          " udfText15, " +
                          " guarantorCustomerId1, " +
                          " json_string, " +
                          " requested_by, " +
                          " request_time) " +
                          " values(" +
                          "'" + msGetGidOpLnReq + "'," +
                          "'" + values.application_gid + "'," +
                          "'" + values.application2sanction_gid + "'," +
                          "'" + values.application2loan_gid + "'," +
                          "'" + values.customer_urn + "'," +
                          "'" + values.rmdisbursementrequest_gid + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customer1FirstName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customer1MiddleName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customer1LastName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.amountMagnitude + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.openedOnDate + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.branchCode + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.productCode + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.tenureMagnitude + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.tenureUnit + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.normalInterestRate + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.penalInterestRate + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.openUserId + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.roName + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customerId1 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.customerType + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText1 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText2 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText3 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText4 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText5 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText6 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText7 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText8 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText9 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText10 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText11 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText12 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText13 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText14 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.udfText15 + "'," +
                          "'" + objMdlOpenLoanAcccountRequest.guarantorCustomerId1 + "'," +
                          "'" + MdlOpenLoanAcccountRequestJSON + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                MdlOpenLoanAccountResponse objMdlOpenLoanAccountResponse = new MdlOpenLoanAccountResponse();

                var client = new RestClient(ConfigurationManager.AppSettings["encore_openloanaccounturl"].ToString());
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                string val = System.Convert.ToBase64String(plainTextBytes);
                request.AddHeader("Authorization", "Basic " + val);


                request.AddParameter("application/json", MdlOpenLoanAcccountRequestJSON, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                lsresponsecontentopenloanaccount = response.Content;

                LogForAuditEncoreIntegration("Response obtained", type);
                LogForAuditEncoreIntegration(response.Content, type);
                LogForAuditEncoreIntegration("End of Response", type);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    objMdlOpenLoanAccountResponse = JsonConvert.DeserializeObject<MdlOpenLoanAccountResponse>(lsresponsecontentopenloanaccount);

                    msGetGidOpLnResp = objcmnfunctions.GetMasterGID("OLAR");
                    msSQL = " insert into ocs_trn_tencoreopenloanaccountresponse(" +
                              " encoreopenloanaccountresponse_gid," +
                              " encoreopenloanaccountrequest_gid," +
                              " response_time, " +
                              " transactionId, " +
                              " valueDate," +
                              " valueDateStr, " +
                              " transactionDate, " +
                              " transactionDateStr," +
                              " systemDateAndTime, " +
                              " systemDateAndTimeStr, " +
                              " accountId," +
                              " entityId, " +
                              " sequenceNum, " +
                              " transactionName," +
                              " part1, " +
                              " part2, " +
                              " part3," +
                              " part4, " +
                              " part5, " +
                              " part6, " +
                              " part7, " +
                              " part8," +
                              " amount1, " +
                              " amount2, " +
                              " amount3," +
                              " description, " +
                              " userId, " +
                              " customerName, " +
                              " status, " +
                              " responseCode," +
                              " response, " +
                              " payeeAccountId, " +
                              " param1," +
                              " param2, " +
                              " param2Str, " +
                              " param3, " +
                              " param4, " +
                              " param5," +
                              " instrument, " +
                              " reference, " +
                              " transactionLotId," +
                              " currencyCode, " +
                              " originalTransactionDate, " +
                              " originalTransactionDateStr, " +
                              " originalTransactionId, " +
                              " originalUserId) " +
                              " values(" +
                              "'" + msGetGidOpLnResp + "'," +
                              "'" + msGetGidOpLnReq + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.valueDate + "'," +
                              "'" + objMdlOpenLoanAccountResponse.valueDateStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionDate + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionDateStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.systemDateAndTime + "'," +
                              "'" + objMdlOpenLoanAccountResponse.systemDateAndTimeStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.accountId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.entityId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.sequenceNum + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionName + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part1 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part2 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part3 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part4 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part5 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part6 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part7 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.part8 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.amount1 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.amount2 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.amount3 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.description + "'," +
                              "'" + objMdlOpenLoanAccountResponse.userId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.customerName + "'," +
                              "'" + objMdlOpenLoanAccountResponse.status + "'," +
                              "'" + objMdlOpenLoanAccountResponse.responseCode + "'," +
                              "'" + objMdlOpenLoanAccountResponse.response + "'," +
                              "'" + objMdlOpenLoanAccountResponse.payeeAccountId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param1 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param2 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param2Str + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param3 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param4 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.param5 + "'," +
                              "'" + objMdlOpenLoanAccountResponse.instrument + "'," +
                              "'" + objMdlOpenLoanAccountResponse.reference + "'," +
                              "'" + objMdlOpenLoanAccountResponse.transactionLotId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.currencyCode + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalTransactionDate + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalTransactionDateStr + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalTransactionId + "'," +
                              "'" + objMdlOpenLoanAccountResponse.originalUserId + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_trmdisbursementrequest set " +
                            " encoreintegration_status = 'Y'," +
                            " encore_accountid='" + objMdlOpenLoanAccountResponse.accountId + "'" +
                            " where rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    LogForAuditEncoreIntegration("Loan Account Created in Encore Successfully", type);

                    objMdlCreateLoanResponse.status = true;
                    objMdlCreateLoanResponse.message = "Loan Account Created in Encore Successfully..!";

                }
                else
                {
                    MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                    objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(response.Content);

                    LogForAuditEncoreIntegration("Loan Account Creation in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.message, type);

                    objMdlCreateLoanResponse.status = false;
                    objMdlCreateLoanResponse.message = "Loan Account Creation in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.message;
                }

            }
            catch (Exception ex)
            {
                LogForAuditEncoreIntegration("Exception occurred in Loan Account Creation\nException Message: " + ex.ToString(), type);

                objMdlCreateLoanResponse.status = false; ;
                objMdlCreateLoanResponse.message = "Exception occurred in Loan Account Creation\nException Message: " + ex.ToString();
            }
            LogForAuditEncoreIntegration("End of logging\r\n", type);
            return objMdlCreateLoanResponse;

        }

        //Batch Loan Account Creation in Encore for Farmer
        public void DaBatchCreateLoanAccountFarmer(MdlCreateLoanRequest values, string employee_gid)
        {
            string type = "Batch_CreateLoanAccount";
           
            MdlOpenLoanAccountResponse objMdlOpenLoanAccountResponse = new MdlOpenLoanAccountResponse();
            try
            {
                List<string> farmer_list = new List<string>();

                msSQL = " select farmercontact_gid " +
                       " from ocs_trn_tfarmercontact " +
                       " where urn_status='Yes' and encoreaccintegration_status='N' and rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                farmer_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("farmercontact_gid")).ToList();
                farmer_count = 0;
                total_farmer = farmer_list.Count();
                if (total_farmer == 0)
                {
                    values.status = false;
                    values.message = " Loan Account Creation Completed for current Farmer list";
                    return;
                }
                foreach (var farmer in farmer_list)
                {
                    MdlOpenLoanAcccountRequest objMdlOpenLoanAcccountRequest = new MdlOpenLoanAcccountRequest();

                    lsfarmer = farmer;

                    if (!(String.IsNullOrEmpty(farmer)))
                    {
                        LogForAuditEncoreIntegration("Batch - Logging Started for Loan Account Creation - Farmer; Params - " + values.application_gid + " " + values.application2sanction_gid + " " + values.application2loan_gid + " " + values.customer_urn + " " + values.rmdisbursementrequest_gid + " " + farmer , type);
                        
                        objMdlOpenLoanAcccountRequest = populateOpenLoanAccountRequestFarmer(farmer);

                        LogForAuditEncoreIntegration("Farmer details populated", type);

                        ProductCodeValidationResponse objProductCodeValidationResponse = new ProductCodeValidationResponse();

                        objProductCodeValidationResponse = validateProductCodeTenureUnitbased(values.application2loan_gid);

                        if (objProductCodeValidationResponse.status == false && objProductCodeValidationResponse.loanTenure_unit == "InValid")
                        {

                            LogForAuditEncoreIntegration("Loan Account Creation Ended for Individual Farmer since Invalid Tenure Units. Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;

                        }

                        if (objProductCodeValidationResponse.status == false)
                        {

                            LogForAuditEncoreIntegration("Loan Account Creation Ended for Individual Farmer since Product Terms doesn't match with those available in Encore . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;

                        }

                        objMdlOpenLoanAcccountRequest.productCode = objProductCodeValidationResponse.product_code;

                        objMdlOpenLoanAcccountRequest.tenureUnit = objProductCodeValidationResponse.loanTenure_unit;

                        lsloanTenure_unit = objProductCodeValidationResponse.loanTenure_unit;

                        LogForAuditEncoreIntegration("Product code validated", type);

                        msSQL = " select disbursement_amount,creditopsdisbursement_amount,creditopscheckerdisbursement_amount from ocs_trn_tfarmercontact a " +
                                " where farmercontact_gid='" + farmer + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);

                        if (objODBCDatareader.HasRows == true)
                        {
                            if (!String.IsNullOrEmpty(objODBCDatareader["creditopscheckerdisbursement_amount"].ToString()))
                            {
                                objMdlOpenLoanAcccountRequest.amountMagnitude = objODBCDatareader["creditopscheckerdisbursement_amount"].ToString().Replace(",", "");
                            }
                            else if (!String.IsNullOrEmpty(objODBCDatareader["creditopsdisbursement_amount"].ToString()))
                            {
                                objMdlOpenLoanAcccountRequest.amountMagnitude = objODBCDatareader["creditopsdisbursement_amount"].ToString().Replace(",", "");
                            }
                            else
                            {
                                objMdlOpenLoanAcccountRequest.amountMagnitude = objODBCDatareader["disbursement_amount"].ToString();
                            }
                        }


                        objMdlOpenLoanAcccountRequest.branchCode = "HO";
                        objMdlOpenLoanAcccountRequest.openedOnDate = "2022-12-01";


                        objMdlOpenLoanAcccountRequest.tenureMagnitude = fetchProgramTenureUnitBased(values.application2loan_gid, lsloanTenure_unit);


                        msSQL = "select rate_interest,penal_interest from ocs_trn_tcadapplication2loan where application2loan_gid = '" + values.application2loan_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);

                        if (objODBCDatareader.HasRows == true)
                        {
                            objMdlOpenLoanAcccountRequest.normalInterestRate = objODBCDatareader["rate_interest"].ToString();
                            objMdlOpenLoanAcccountRequest.penalInterestRate = objODBCDatareader["penal_interest"].ToString();
                        }

                        msSQL = " select a.created_by as relationshipmanager_gid,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as relationshipmanager_name," +
                                " urn from ocs_trn_tfarmercontact a" +
                                " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                                " where farmercontact_gid = '" + farmer + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);

                        if (objODBCDatareader.HasRows == true)
                        {
                            objMdlOpenLoanAcccountRequest.openUserId = objODBCDatareader["relationshipmanager_gid"].ToString();
                            objMdlOpenLoanAcccountRequest.roName = objODBCDatareader["relationshipmanager_name"].ToString();
                            objMdlOpenLoanAcccountRequest.customerId1 = objODBCDatareader["urn"].ToString();
                        }

                        LogForAuditEncoreIntegration("Amount, Tenure, Interest and RM details obtained", type);

                        objMdlOpenLoanAcccountRequest.currencyCode = "INR";

                        objMdlOpenLoanAcccountRequest.udfText1 = "abc";
                        objMdlOpenLoanAcccountRequest.udfText2 = "def";
                        objMdlOpenLoanAcccountRequest.udfText3 = "ghi";
                        objMdlOpenLoanAcccountRequest.udfText4 = "jkl";
                        objMdlOpenLoanAcccountRequest.udfText5 = "mno";
                        objMdlOpenLoanAcccountRequest.udfText6 = "pqr";
                        objMdlOpenLoanAcccountRequest.udfText7 = "stu";
                        objMdlOpenLoanAcccountRequest.udfText8 = "vwx";
                        objMdlOpenLoanAcccountRequest.udfText9 = "yza";
                        objMdlOpenLoanAcccountRequest.udfText10 = "bab";
                        objMdlOpenLoanAcccountRequest.udfText11 = "dss";
                        objMdlOpenLoanAcccountRequest.udfText12 = "kre";
                        objMdlOpenLoanAcccountRequest.udfText13 = "swa";
                        objMdlOpenLoanAcccountRequest.udfText14 = "nas";
                        objMdlOpenLoanAcccountRequest.udfText15 = "clu";

                        msSQL = " select customer_urn from ocs_trn_tcadapplication where application_gid = '" + values.application_gid + "'";
                        objMdlOpenLoanAcccountRequest.guarantorCustomerId1 = objdbconn.GetExecuteScalar(msSQL);

                        string MdlOpenLoanAcccountRequestJSON = JsonConvert.SerializeObject(objMdlOpenLoanAcccountRequest);

                        LogForAuditEncoreIntegration("Request JSON formed", type);

                        msGetGidOpLnReq = objcmnfunctions.GetMasterGID("OLAR");
                        msSQL = " insert into ocs_trn_tencorecreateloanaccountrequest(" +
                                  " encorecreateloanaccountrequest_gid," +
                                  " application_gid," +
                                  " application2sanction_gid, " +
                                  " application2loan_gid, " +
                                  " customer_urn," +
                                  " rmdisbursementrequest_gid, " +
                                  " farmercontact_gid, " +
                                  " customer1FirstName, " +
                                  " customer1MiddleName," +
                                  " customer1LastName, " +
                                  " amountMagnitude, " +
                                  " openedOnDate," +
                                  " branchCode, " +
                                  " productCode, " +
                                  " tenureMagnitude," +
                                  " tenureUnit, " +
                                  " normalInterestRate, " +
                                  " penalInterestRate," +
                                  " openUserId, " +
                                  " roName, " +
                                  " customerId1, " +
                                  " customerType, " +
                                  " udfText1," +
                                  " udfText2, " +
                                  " udfText3, " +
                                  " udfText4," +
                                  " udfText5, " +
                                  " udfText6, " +
                                  " udfText7, " +
                                  " udfText8, " +
                                  " udfText9," +
                                  " udfText10, " +
                                  " udfText11, " +
                                  " udfText12," +
                                  " udfText13, " +
                                  " udfText14, " +
                                  " udfText15, " +
                                  " guarantorCustomerId1, " +
                                  " json_string, " +
                                  " requested_by, " +
                                  " request_time) " +
                                  " values(" +
                                  "'" + msGetGidOpLnReq + "'," +
                                  "'" + values.application_gid + "'," +
                                  "'" + values.application2sanction_gid + "'," +
                                  "'" + values.application2loan_gid + "'," +
                                  "'" + values.customer_urn + "'," +
                                  "'" + values.rmdisbursementrequest_gid + "'," +
                                  "'" + farmer + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.customer1FirstName + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.customer1MiddleName + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.customer1LastName + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.amountMagnitude + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.openedOnDate + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.branchCode + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.productCode + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.tenureMagnitude + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.tenureUnit + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.normalInterestRate + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.penalInterestRate + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.openUserId + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.roName + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.customerId1 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.customerType + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText1 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText2 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText3 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText4 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText5 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText6 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText7 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText8 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText9 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText10 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText11 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText12 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText13 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText14 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.udfText15 + "'," +
                                  "'" + objMdlOpenLoanAcccountRequest.guarantorCustomerId1 + "'," +
                                  "'" + MdlOpenLoanAcccountRequestJSON + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 1)
                        {
                            LogForAuditEncoreIntegration("Error occurred while storing loan account creation request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }
                        try
                        {
                            

                            var client = new RestClient(ConfigurationManager.AppSettings["encore_openloanaccounturl"].ToString());
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                            var request = new RestRequest(Method.POST);

                            request.AddHeader("Content-Type", "application/json");
                            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                            string val = System.Convert.ToBase64String(plainTextBytes);
                            request.AddHeader("Authorization", "Basic " + val);


                            request.AddParameter("application/json", MdlOpenLoanAcccountRequestJSON, ParameterType.RequestBody);

                            IRestResponse response = client.Execute(request);
                            
                            LogForAuditEncoreIntegration("Response obtained", type);
                            LogForAuditEncoreIntegration(response.Content, type);
                            LogForAuditEncoreIntegration("End of Response", type);
                            
                            lsresponsecontentopenloanaccount = response.Content;
                            lsresponseStatusCodeopenloanaccount = response.StatusCode.ToString();

                        }
                        catch (Exception ex)
                        {
                            LogForAuditEncoreIntegration("Error occurred while hitting loan account creation URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentopenloanaccount + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }
                        if (lsresponseStatusCodeopenloanaccount == "OK")
                        {
                            objMdlOpenLoanAccountResponse = JsonConvert.DeserializeObject<MdlOpenLoanAccountResponse>(lsresponsecontentopenloanaccount);

                            msGetGidOpLnResp = objcmnfunctions.GetMasterGID("OLAR");
                            msSQL = " insert into ocs_trn_tencoreopenloanaccountresponse(" +
                                      " encoreopenloanaccountresponse_gid," +
                                      " encoreopenloanaccountrequest_gid," +
                                      " response_time, " +
                                      " transactionId, " +
                                      " valueDate," +
                                      " valueDateStr, " +
                                      " transactionDate, " +
                                      " transactionDateStr," +
                                      " systemDateAndTime, " +
                                      " systemDateAndTimeStr, " +
                                      " accountId," +
                                      " entityId, " +
                                      " sequenceNum, " +
                                      " transactionName," +
                                      " part1, " +
                                      " part2, " +
                                      " part3," +
                                      " part4, " +
                                      " part5, " +
                                      " part6, " +
                                      " part7, " +
                                      " part8," +
                                      " amount1, " +
                                      " amount2, " +
                                      " amount3," +
                                      " description, " +
                                      " userId, " +
                                      " customerName, " +
                                      " status, " +
                                      " responseCode," +
                                      " response, " +
                                      " payeeAccountId, " +
                                      " param1," +
                                      " param2, " +
                                      " param2Str, " +
                                      " param3, " +
                                      " param4, " +
                                      " param5," +
                                      " instrument, " +
                                      " reference, " +
                                      " transactionLotId," +
                                      " currencyCode, " +
                                      " originalTransactionDate, " +
                                      " originalTransactionDateStr, " +
                                      " originalTransactionId, " +
                                      " originalUserId) " +
                                      " values(" +
                                      "'" + msGetGidOpLnResp + "'," +
                                      "'" + msGetGidOpLnReq + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.transactionId + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.valueDate + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.valueDateStr + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.transactionDate + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.transactionDateStr + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.systemDateAndTime + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.systemDateAndTimeStr + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.accountId + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.entityId + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.sequenceNum + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.transactionName + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.part1 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.part2 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.part3 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.part4 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.part5 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.part6 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.part7 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.part8 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.amount1 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.amount2 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.amount3 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.description + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.userId + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.customerName + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.status + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.responseCode + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.response + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.payeeAccountId + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.param1 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.param2 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.param2Str + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.param3 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.param4 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.param5 + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.instrument + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.reference + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.transactionLotId + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.currencyCode + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.originalTransactionDate + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.originalTransactionDateStr + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.originalTransactionId + "'," +
                                      "'" + objMdlOpenLoanAccountResponse.originalUserId + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            if (mnResult != 0)
                            {
                                msSQL = " update ocs_trn_tfarmercontact set " +
                                        " encoreaccintegration_status = 'Y'," +
                                        " encore_accountid='" + objMdlOpenLoanAccountResponse.accountId + "'" +
                                        " where farmercontact_gid='" + farmer + "' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                               
                                LogForAuditEncoreIntegration("Batch - Loan Account Creation Ended for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                farmer_count = farmer_count + 1;
                            }
                            else
                            {
                                LogForAuditEncoreIntegration("Error occurred while storing loan account creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                continue;
                            }

                        }
                        else if ((lsresponseStatusCodeopenloanaccount == "InternalServerError"))
                        {
                            LogForAuditEncoreIntegration("Internal Server Error . Response Content - " + lsresponsecontentopenloanaccount + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }
                        else
                        {
                            LogForAuditEncoreIntegration("Failed to receive 200 or 500 Response  . Response Content - " + lsresponsecontentopenloanaccount + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            values.status = false;
                            values.message = "" + lsresponseStatusCodeopenloanaccount + " Response from Encore. Totally " +
                                             "" + farmer_count.ToString() + " Of " + total_farmer.ToString() + " Successful Loan Account Creation Response received from Encore";                            
                            return;
                        }
                    }
                }

                values.status = true;
                values.message = farmer_count.ToString() + " Of " + total_farmer.ToString() + " Successful Loan Account Creation Response received from Encore ";


            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = " Error occurred while posting batch Loan Account Creation request to Encore ";
                LogForAuditEncoreIntegration("Error occurred while posting batch Loan Account Creation request to Encore .Farmer ID - " + lsfarmer + ". Exception - " + ex + " .Response Content(If Exists) - " + lsresponsecontentopenloanaccount + " at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
            }

        }

        //Auxillary Functions
        public string FetchApplicantType(string application_gid)
        {
            string type = "CreateLoanAccount";
            LogForAuditEncoreIntegration("Entering FetchApplicantType function",type);

            string lsapplicant_gid = "";
            msSQL = " select institution_gid from ocs_trn_tcadinstitution" +
                    " where application_gid = '" + application_gid + "' and stakeholder_type = 'Applicant'";

            lsapplicant_gid = objdbconn.GetExecuteScalar(msSQL);

            LogForAuditEncoreIntegration("Exiting FetchApplicantType function", type);

            if (lsapplicant_gid != "")
                return ApplicantType.Institution;
            else
                return ApplicantType.Individual;

            
        }


        public string fetchProgramTenure(string application2loan_gid)
        {
            string type = "CreateLoanAccount";

            LogForAuditEncoreIntegration("Entering fetchProgramTenure function",type);

            long program_tenure = 0, validity_year = 0, validity_month = 0, validity_day = 0;

          msSQL = "select facilityvalidity_year,facilityvalidity_month,facilityvalidity_days from ocs_trn_tcadapplication2loan where application2loan_gid = '" + application2loan_gid + "'";
          objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                validity_year = long.Parse(objODBCDatareader["facilityvalidity_year"].ToString());
                validity_month = long.Parse(objODBCDatareader["facilityvalidity_month"].ToString());
                validity_day = long.Parse(objODBCDatareader["facilityvalidity_days"].ToString());

               program_tenure = (validity_year * 365) + (validity_month * 12) + validity_day;
                
            }
            else
            {

            }
            LogForAuditEncoreIntegration("Exiting fetchProgramTenure function",type);
            return program_tenure.ToString();
        }

        //Fetch Program Tenure Unit Based
        public string fetchProgramTenureUnitBased(string application2loan_gid,string lsloanTenure_unit)
        {
            string type = "CreateLoanAccount";
            LogForAuditEncoreIntegration("Entering fetchProgramTenure function Unit Based",type);
            
            if (lsloanTenure_unit == "Day")
            {
                msSQL = "select facilityvalidity_days from ocs_trn_tcadapplication2loan where application2loan_gid = '" + application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lsprogram_tenure = objODBCDatareader["facilityvalidity_days"].ToString();
                }
                objODBCDatareader.Close();
                if (String.IsNullOrEmpty(lsprogram_tenure))
                {
                    lsprogram_tenure = "0";
                }
            }

            if (lsloanTenure_unit == "Month")
            {
                msSQL = "select facilityvalidity_month from ocs_trn_tcadapplication2loan where application2loan_gid = '" + application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lsprogram_tenure = objODBCDatareader["facilityvalidity_month"].ToString();
                }
                objODBCDatareader.Close();
                if (String.IsNullOrEmpty(lsprogram_tenure))
                {
                    lsprogram_tenure = "0";
                }
            }

            if (lsloanTenure_unit == "Annual")
            {
                msSQL = "select facilityvalidity_year from ocs_trn_tcadapplication2loan where application2loan_gid = '" + application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lsprogram_tenure = objODBCDatareader["facilityvalidity_year"].ToString();
                }
                objODBCDatareader.Close();
                if (String.IsNullOrEmpty(lsprogram_tenure))
                {
                    lsprogram_tenure = "0";
                }
            }

            LogForAuditEncoreIntegration("Exiting fetchProgramTenure function",type);
            return lsprogram_tenure.ToString();
        }

        public MdlOpenLoanAcccountRequest populateOpenLoanAccountRequestInstitution(MdlCreateLoanRequest values)
        {
            string type = "CreateLoanAccount";
            LogForAuditEncoreIntegration("Entering populateOpenLoanAccountRequestInstitution function",type);
            MdlOpenLoanAcccountRequest objMdlOpenLoanAcccountRequest = new MdlOpenLoanAcccountRequest();

            msSQL = "select company_name from ocs_trn_tcadinstitution where application_gid = '" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                objMdlOpenLoanAcccountRequest.customer1FirstName = objODBCDatareader["company_name"].ToString();

            }


            LogForAuditEncoreIntegration("Exiting populateOpenLoanAccountRequestInstitution function",type);
            return objMdlOpenLoanAcccountRequest;

        }

        public MdlOpenLoanAcccountRequest populateOpenLoanAccountRequestIndividual(MdlCreateLoanRequest values)
        {
            string type = "CreateLoanAccount";
            LogForAuditEncoreIntegration("Entering populateOpenLoanAccountRequestIndividual function",type);
            MdlOpenLoanAcccountRequest objMdlOpenLoanAcccountRequest = new MdlOpenLoanAcccountRequest();

            msSQL = "select first_name,middle_name,last_name from ocs_trn_tcadcontact where application_gid = '" + values.application_gid + "' and stakeholder_type = 'Applicant'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                objMdlOpenLoanAcccountRequest.customer1FirstName = objODBCDatareader["first_name"].ToString();
                objMdlOpenLoanAcccountRequest.customer1MiddleName = objODBCDatareader["middle_name"].ToString();
                objMdlOpenLoanAcccountRequest.customer1LastName = objODBCDatareader["last_name"].ToString();

            }

            LogForAuditEncoreIntegration("Exiting populateOpenLoanAccountRequestIndividual function", type);
            return objMdlOpenLoanAcccountRequest;

        }

        public MdlOpenLoanAcccountRequest populateOpenLoanAccountRequestFarmer(MdlCreateLoanRequest values)
        {
            string type = "CreateLoanAccount";
            LogForAuditEncoreIntegration("Entering populateOpenLoanAccountRequestFarmer function", type);
            MdlOpenLoanAcccountRequest objMdlOpenLoanAcccountRequest = new MdlOpenLoanAcccountRequest();

            msSQL = "select first_name,middle_name,last_name from ocs_trn_tfarmercontact where farmercontact_gid = '" + values.farmercontact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                objMdlOpenLoanAcccountRequest.customer1FirstName = objODBCDatareader["first_name"].ToString();
                objMdlOpenLoanAcccountRequest.customer1MiddleName = objODBCDatareader["middle_name"].ToString();
                objMdlOpenLoanAcccountRequest.customer1LastName = objODBCDatareader["last_name"].ToString();

            }
            LogForAuditEncoreIntegration("Exiting populateOpenLoanAccountRequestFarmer function", type);
            return objMdlOpenLoanAcccountRequest;

        }

        public MdlOpenLoanAcccountRequest populateOpenLoanAccountRequestFarmer(string farmer)
        {
            string type = "CreateLoanAccount";
            LogForAuditEncoreIntegration("Entering populateOpenLoanAccountRequestFarmer_Batch function", type);
            MdlOpenLoanAcccountRequest objMdlOpenLoanAcccountRequest = new MdlOpenLoanAcccountRequest();

            msSQL = "select first_name,middle_name,last_name from ocs_trn_tfarmercontact where farmercontact_gid = '" + farmer + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                objMdlOpenLoanAcccountRequest.customer1FirstName = objODBCDatareader["first_name"].ToString();
                objMdlOpenLoanAcccountRequest.customer1MiddleName = objODBCDatareader["middle_name"].ToString();
                objMdlOpenLoanAcccountRequest.customer1LastName = objODBCDatareader["last_name"].ToString();

            }
            LogForAuditEncoreIntegration("Exiting populateOpenLoanAccountRequestFarmer_Batch function", type);
            return objMdlOpenLoanAcccountRequest;

        }

        public ProductCodeValidationResponse validateProductCode(string application2loan_gid)
        {
            string type = "CreateLoanAccount";
            LogForAuditEncoreIntegration("Entering validateProductCode function",type);
            ProductCodeValidationResponse objProductCodeValidationResponse = new ProductCodeValidationResponse();

            MdlEncoreLoanAccountProduct objMasterProduct = new MdlEncoreLoanAccountProduct();
            MdlEncoreLoanAccountProduct objTransactionProduct = new MdlEncoreLoanAccountProduct();

            msSQL = "select producttype_gid,productsubtype_gid,principalfrequency_gid,interestfrequency_gid,interest_status,moratorium_status,moratorium_type from ocs_trn_tcadapplication2loan where application2loan_gid = '" + application2loan_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                objTransactionProduct.product = objODBCDatareader["producttype_gid"].ToString();
                objTransactionProduct.sub_product = objODBCDatareader["productsubtype_gid"].ToString();
                objTransactionProduct.principal_frequency = objODBCDatareader["principalfrequency_gid"].ToString();
                objTransactionProduct.interest_frequency = objODBCDatareader["interestfrequency_gid"].ToString();
                objTransactionProduct.interestdeduction_upfront = objODBCDatareader["interest_status"].ToString();
                objTransactionProduct.moratorium_status = objODBCDatareader["moratorium_status"].ToString();
                objTransactionProduct.moratorium_type = objODBCDatareader["moratorium_type"].ToString();
            }

            msSQL = "select Products_gid,SubProduct_gid,PrincipalTennure_gid,InterestTennure_gid,Intdeductstatus,Moratoriamstatus,MoratoriumType,lms_code from ocs_mst_tencoreproduct where status_log = 'Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objMasterProduct.product = dr_datarow["Products_gid"].ToString();
                    objMasterProduct.sub_product = dr_datarow["SubProduct_gid"].ToString();
                    objMasterProduct.principal_frequency = dr_datarow["PrincipalTennure_gid"].ToString();
                    objMasterProduct.interest_frequency = dr_datarow["InterestTennure_gid"].ToString();
                    objMasterProduct.interestdeduction_upfront = dr_datarow["Intdeductstatus"].ToString() == "Y"? "Yes" : "No";
                    objMasterProduct.moratorium_status = dr_datarow["Moratoriamstatus"].ToString() == "Y"? "Yes": "No";
                    objMasterProduct.moratorium_type = dr_datarow["MoratoriumType"].ToString();

                    string masterSerialized = JsonConvert.SerializeObject(objMasterProduct);
                    string transactionSerialized = JsonConvert.SerializeObject(objTransactionProduct);

                    if (masterSerialized == transactionSerialized)
                    {
                        objProductCodeValidationResponse.status = true;
                        objProductCodeValidationResponse.product_code = dr_datarow["lms_code"].ToString();
                        return objProductCodeValidationResponse;
                    }
                        

                }                
            }
            dt_datatable.Dispose();

            objProductCodeValidationResponse.status = false;

            LogForAuditEncoreIntegration("Exiting validateProductCode function",type);
            return objProductCodeValidationResponse;

        }

        //Validate Product Code Tenure Unit based
        public ProductCodeValidationResponse validateProductCodeTenureUnitbased(string application2loan_gid)
        {
            string type = "CreateLoanAccount";
            LogForAuditEncoreIntegration("Entering validateProductCode function",type);
            ProductCodeValidationResponse objProductCodeValidationResponse = new ProductCodeValidationResponse();

            MdlEncoreLoanAccountProduct objMasterProduct = new MdlEncoreLoanAccountProduct();
            MdlEncoreLoanAccountProduct objTransactionProduct = new MdlEncoreLoanAccountProduct();

            ProductSpecificTenureUnitValidationResponse objProductSpecificTenureUnitValidationResponse = new ProductSpecificTenureUnitValidationResponse();

            objProductSpecificTenureUnitValidationResponse = ProductSpecificTenureUnitValidation(application2loan_gid);

            msSQL = "select producttype_gid,productsubtype_gid,principalfrequency_gid,interestfrequency_gid,interest_status,moratorium_status,moratorium_type from ocs_trn_tcadapplication2loan where application2loan_gid = '" + application2loan_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                objTransactionProduct.product = objODBCDatareader["producttype_gid"].ToString();
                objTransactionProduct.sub_product = objODBCDatareader["productsubtype_gid"].ToString();
                objTransactionProduct.principal_frequency = objODBCDatareader["principalfrequency_gid"].ToString();
                objTransactionProduct.interest_frequency = objODBCDatareader["interestfrequency_gid"].ToString();
                objTransactionProduct.interestdeduction_upfront = objODBCDatareader["interest_status"].ToString();
                objTransactionProduct.moratorium_status = objODBCDatareader["moratorium_status"].ToString();
                objTransactionProduct.moratorium_type = objODBCDatareader["moratorium_type"].ToString();
            }

            msSQL = "select Products_gid,SubProduct_gid,PrincipalTennure_gid,InterestTennure_gid,Intdeductstatus,Moratoriamstatus,MoratoriumType,lms_code,LoanTennure from ocs_mst_tencoreproduct where status_log = 'Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objMasterProduct.product = dr_datarow["Products_gid"].ToString();
                    objMasterProduct.sub_product = dr_datarow["SubProduct_gid"].ToString();
                    objMasterProduct.principal_frequency = dr_datarow["PrincipalTennure_gid"].ToString();
                    objMasterProduct.interest_frequency = dr_datarow["InterestTennure_gid"].ToString();
                    objMasterProduct.interestdeduction_upfront = dr_datarow["Intdeductstatus"].ToString() == "Y" ? "Yes" : "No";
                    objMasterProduct.moratorium_status = dr_datarow["Moratoriamstatus"].ToString() == "Y" ? "Yes" : "No";
                    objMasterProduct.moratorium_type = dr_datarow["MoratoriumType"].ToString();
                    

                    string masterSerialized = JsonConvert.SerializeObject(objMasterProduct);
                    string transactionSerialized = JsonConvert.SerializeObject(objTransactionProduct);
                    
                    if (masterSerialized == transactionSerialized && objProductSpecificTenureUnitValidationResponse.message == "Valid")
                    {

                        objProductCodeValidationResponse.status = true;
                        objProductCodeValidationResponse.product_code = dr_datarow["lms_code"].ToString(); 
                        objProductCodeValidationResponse.loanTenure_unit = dr_datarow["LoanTennure"].ToString();
                        return objProductCodeValidationResponse;
                    }
                    else if(masterSerialized == transactionSerialized && objProductSpecificTenureUnitValidationResponse.message == "InValid")
                    {
                        objProductCodeValidationResponse.status = false;
                        objProductCodeValidationResponse.product_code = dr_datarow["lms_code"].ToString();
                        objProductCodeValidationResponse.loanTenure_unit = "InValid";
                        return objProductCodeValidationResponse;
                    }
                }
            }
            dt_datatable.Dispose();

            objProductCodeValidationResponse.status = false;

            LogForAuditEncoreIntegration("Exiting validateProductCode function", type);
            return objProductCodeValidationResponse;

        }

        //Product Specific Tenure Unit Validation
        public ProductSpecificTenureUnitValidationResponse ProductSpecificTenureUnitValidation(string application2loan_gid)
        {
            
            ProductSpecificTenureUnitValidationResponse objProductSpecificTenureUnitValidationResponse = new ProductSpecificTenureUnitValidationResponse();

            msSQL = "select facilityvalidity_days,facilityvalidity_month,facilityvalidity_year from ocs_trn_tcadapplication2loan where application2loan_gid = '" + application2loan_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsfacilityvalidity_days = objODBCDatareader["facilityvalidity_days"].ToString();
                lsfacilityvalidity_month = objODBCDatareader["facilityvalidity_month"].ToString();
                lsfacilityvalidity_year = objODBCDatareader["facilityvalidity_year"].ToString();
            }
            objODBCDatareader.Close();

            List<string> facilityvalidityunits = new List<string>();

            facilityvalidityunits.Add(lsfacilityvalidity_days);
            facilityvalidityunits.Add(lsfacilityvalidity_month);
            facilityvalidityunits.Add(lsfacilityvalidity_year);

            int count = 0;
            foreach (var units in facilityvalidityunits)
            {
                if(!((string.IsNullOrEmpty(units)) || units=="0"))
                {
                    count = count + 1;
                }
            }

            if (count == 1)
            {
                objProductSpecificTenureUnitValidationResponse.status = true;
                objProductSpecificTenureUnitValidationResponse.message = "Valid";
                return objProductSpecificTenureUnitValidationResponse;
            }
            else
            {
                objProductSpecificTenureUnitValidationResponse.status = false;
                objProductSpecificTenureUnitValidationResponse.message = "InValid";
                return objProductSpecificTenureUnitValidationResponse;
            }
        }

        //Product Specific Tenure Unit Validation - Product Loan Details With Encore Master Validation
        public ProductSpecificTenureUnitValidationResponse ProductSpecificTenureUnitValidation(string facilityvalidity_days,string facilityvalidity_month,string facilityvalidity_year)
        {

            ProductSpecificTenureUnitValidationResponse objProductSpecificTenureUnitValidationResponse = new ProductSpecificTenureUnitValidationResponse();

            lsfacilityvalidity_days = facilityvalidity_days;
            lsfacilityvalidity_month = facilityvalidity_month;
            lsfacilityvalidity_year = facilityvalidity_year;    


            List<string> facilityvalidityunits = new List<string>();

            facilityvalidityunits.Add(lsfacilityvalidity_days);
            facilityvalidityunits.Add(lsfacilityvalidity_month);
            facilityvalidityunits.Add(lsfacilityvalidity_year);

            int count = 0;
            foreach (var units in facilityvalidityunits)
            {
                if (!((string.IsNullOrEmpty(units)) || units == "0"))
                {
                    count = count + 1;
                }
            }

            if (count == 1)
            {
                objProductSpecificTenureUnitValidationResponse.status = true;
                objProductSpecificTenureUnitValidationResponse.message = "Valid";
                return objProductSpecificTenureUnitValidationResponse;
            }
            else
            {
                objProductSpecificTenureUnitValidationResponse.status = false;
                objProductSpecificTenureUnitValidationResponse.message = "InValid";
                return objProductSpecificTenureUnitValidationResponse;
            }
        }

        public void LogForAuditEncoreIntegration(string strVal,string type)
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

        public string fetchCompanyCode()
        {
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            string lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            return lscompany_code;
        }

        //ProductLoanDetailsWithEncoreMasterValidation
        public ProductLoanDetailsWithEncoreMasterValidationResponse ProductLoanDetailsWithEncoreMasterValidation(MdlProductLoanDetails values)
        {
            string type = "ProductLoanDetailsValidation";
            LogForAuditEncoreIntegration("Entering ProductLoanDetailsWithEncoreMasterValidation function", type);

            ProductSpecificTenureUnitValidationResponse objProductSpecificTenureUnitValidationResponse = new ProductSpecificTenureUnitValidationResponse();

            objProductSpecificTenureUnitValidationResponse = ProductSpecificTenureUnitValidation(values.facilityvalidity_days,values.facilityvalidity_month,values.facilityvalidity_year);

            ProductLoanDetailsWithEncoreMasterValidationResponse objProductLoanDetailsWithEncoreMasterValidationResponse = new ProductLoanDetailsWithEncoreMasterValidationResponse();

            MdlProductLoanDetails objMdlProductLoanDetails = new MdlProductLoanDetails();

            MdlEncoreLoanAccountProduct objMasterProduct = new MdlEncoreLoanAccountProduct();

            MdlEncoreLoanAccountProduct objTransactionProduct = new MdlEncoreLoanAccountProduct();


            objTransactionProduct.product = values.product?.ToString() ?? "";
            objTransactionProduct.sub_product = values.sub_product?.ToString() ?? "";
            objTransactionProduct.principal_frequency = values.principal_frequency?.ToString() ?? "";
            objTransactionProduct.interest_frequency = values.interest_frequency?.ToString() ?? "";
            objTransactionProduct.interestdeduction_upfront = values.interestdeduction_upfront?.ToString() ?? "";
            objTransactionProduct.moratorium_status = values.moratorium_status?.ToString() ?? "";
            objTransactionProduct.moratorium_type = values.moratorium_type?.ToString() ?? "";

            msSQL = "select Products_gid,SubProduct_gid,PrincipalTennure_gid,InterestTennure_gid,Intdeductstatus,Moratoriamstatus,MoratoriumType,lms_code,LoanTennure from ocs_mst_tencoreproduct where status_log = 'Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objMasterProduct.product = dr_datarow["Products_gid"].ToString();
                    objMasterProduct.sub_product = dr_datarow["SubProduct_gid"].ToString();
                    objMasterProduct.principal_frequency = dr_datarow["PrincipalTennure_gid"].ToString();
                    objMasterProduct.interest_frequency = dr_datarow["InterestTennure_gid"].ToString();
                    objMasterProduct.interestdeduction_upfront = dr_datarow["Intdeductstatus"].ToString() == "Y" ? "Yes" : "No";
                    objMasterProduct.moratorium_status = dr_datarow["Moratoriamstatus"].ToString() == "Y" ? "Yes" : "No";
                    objMasterProduct.moratorium_type = dr_datarow["MoratoriumType"].ToString();


                    string masterSerialized = JsonConvert.SerializeObject(objMasterProduct);
                    string transactionSerialized = JsonConvert.SerializeObject(objTransactionProduct);

                    if (masterSerialized == transactionSerialized && objProductSpecificTenureUnitValidationResponse.message == "Valid")
                    {

                        objProductLoanDetailsWithEncoreMasterValidationResponse.status = true;
                        
                        return objProductLoanDetailsWithEncoreMasterValidationResponse;
                    }
                    else if (masterSerialized == transactionSerialized && objProductSpecificTenureUnitValidationResponse.message == "InValid")
                    {
                        objProductLoanDetailsWithEncoreMasterValidationResponse.status = false;
                        objProductLoanDetailsWithEncoreMasterValidationResponse.message = "InValid";

                        return objProductLoanDetailsWithEncoreMasterValidationResponse;
                    }
                }
            }
            dt_datatable.Dispose();

            objProductLoanDetailsWithEncoreMasterValidationResponse.status = false;
            
            LogForAuditEncoreIntegration("Exiting ProductLoanDetailsWithEncoreMasterValidation function", type);

            return objProductLoanDetailsWithEncoreMasterValidationResponse;

        }

        //Log For Audit Product Loan Details - SamFin
        public void LogForAuditProductLoanDetails(string strVal)
        {
            try
            {
                loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamFin/ProductLoanDetailsValidation/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
    }
}