using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;

using System.Configuration;

using System.Globalization;

using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.IO;
using ems.hbapiconn.Models;
using ems.hbapiconn.Functions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Helpers;
using System.Dynamic;
using System.Threading.Tasks;
using System.Threading;


namespace ems.hbapiconn.Functions
{
    /// <summary>
    /// Data Access class contains the related API calls to HyperbridgeAPI for posting Contract(SamAgro) to External ERP 
    /// </summary>
    /// <remarks>Written by Praveen Raj</remarks>
    public class FnSamAgroHBAPIContract
    {
        string msSQL, lsinstitution_gid, lscontact_gid, lsemployee_gid, lsuser_gid, lsapplication2loan_gid, lsrelationshipmanager_gid, lserp_status, lscount_deferraldoc;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        int mnResult, mnResultContact, mnResultAddress;
        string loglspath = "", logFileName = "";
        bool postResult;
        string urn;
        string lsapplication_gid, lscontactpersonfirst_name, lscontactpersonmiddle_name, lscontactpersonlast_name, lsdesignation;
        DataTable dt_mobileno, dt_email;
        string iterator_table;
        string mscontactdetailsGID;
        ContactDetails[] arrContactDetails;
        HBAPICmnFunctions objHBAPICmnFunctions = new HBAPICmnFunctions();
        string lsloanproduct_gid, lsloansubproduct_gid;
        string lsonboardinstitution_gid;
        string lsproducttype_gid, lsproductsubtype_gid;
        string contract_erpid, contract_id;
        string lserp_id;
        string lsbuyeronboard_gid;
        string lsprogram_erpid;
        string lsvariety_gid,lsvariety_name, lsuom_gid;
        string commodity_errormessage;
        string lscontract_externalid;
        string lssupplier_gid, lsurn_status, lsurn, lsvendor_externalid, lssupplier_name, lssupplier_id;
        string lsdeferral_status;
        string customer_erpid, address_erpid;
        FnSamAgroHBAPIConn objFnSamAgroHBAPIConn = new FnSamAgroHBAPIConn();
        string lsiec_licenseno;




        public MdlHBAPIConnDAResponse PostContractHBAPI(string application_gid, string buyeronboard_gid)
        {

            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();

            try
            {
                LogForAuditContractHBAPI("Logging started in API - " + ContractAPIMetaList.PostContractHBAPI);
                LogForAuditContractHBAPI("Application GID - " + application_gid);
                LogForAuditContractHBAPI("Buyer Onboard GID - " + buyeronboard_gid);

                MdlContractHBAPI objMdlContractHBAPI = new MdlContractHBAPI();

                objMdlContractHBAPI = populateBuyerInstitutionForContract(application_gid, buyeronboard_gid); // Populates Buyer Details for Contract

                LogForAuditContractHBAPI("Institution details of Contract populated");

                msSQL = " select contract_id,sa_name,relationshipmanager_gid from agr_mst_tapplication" +
                    " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractHBAPI.ExternalID = objODBCDatareader["contract_id"].ToString();
                    objMdlContractHBAPI.broker_name = objODBCDatareader["sa_name"].ToString();
                    lsrelationshipmanager_gid = objODBCDatareader["relationshipmanager_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select relationshipmanager_gid from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                lsrelationshipmanager_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select erp_status from hrm_mst_temployee where employee_gid = '" + lsrelationshipmanager_gid + "'";
                lserp_status = objdbconn.GetExecuteScalar(msSQL);

                if (lserp_status == "No")
                {
                    objMdlHBAPIConnDAResponse.status = false;
                    objMdlHBAPIConnDAResponse.message = "RM of this application hasn't been posted to ERP yet. Since he can't be assigned as Sales Rep in ERP, posting Contract to ERP failed.";
                    return objMdlHBAPIConnDAResponse;
                }
                {
                    msSQL = " select erp_id from hrm_mst_temployee where employee_gid = '" + lsrelationshipmanager_gid + "'";
                    objMdlContractHBAPI.rm_erpid = objdbconn.GetExecuteScalar(msSQL);
                }

                msSQL = " select supplier_gid from agr_mst_tapploan2supplierdtl where application_gid = '" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select urn_status from agr_mst_tsupronboard2institution where application_gid = '" + dt["supplier_gid"] + "' and stakeholder_type = 'Applicant'";
                    lsurn_status = objdbconn.GetExecuteScalar(msSQL);

                    if (lsurn_status != "Yes")
                    {
                        objMdlHBAPIConnDAResponse.status = false;
                        objMdlHBAPIConnDAResponse.message = "All the suppliers of this contract haven't been posted to ERP yet. Hence, posting Contract to ERP failed.";
                        return objMdlHBAPIConnDAResponse;
                    }
                }


                msSQL = " select application2loan_gid from agr_mst_tapplication2loan" +
                    " where application_gid = '" + application_gid + "' order by created_date limit 1";
                lsapplication2loan_gid = objdbconn.GetExecuteScalar(msSQL);

                if(objHBAPICmnFunctions.validateCommodityERPMapping(lsapplication2loan_gid) == false)
                {
                    objMdlHBAPIConnDAResponse.status = false;
                    objMdlHBAPIConnDAResponse.message = "All the commodities of this contract haven't been mapped with ERP yet. Hence, posting Contract to ERP failed.";
                    return objMdlHBAPIConnDAResponse;
                }

                msSQL = " select date_format(programlimit_validdfrom,'%Y-%m-%d') as programlimit_validdfrom,date_format(programlimit_validdto,'%Y-%m-%d') as programlimit_validdto,rate_interest,penal_interest,trade_orginatedby,loanfacility_amount,SA_Brokerage,loan_type,productsub_type,facility_mode from agr_mst_tapplication2loan" +
                       " where application2loan_gid = '" + lsapplication2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractHBAPI.programlimit_validdfrom = objODBCDatareader["programlimit_validdfrom"].ToString();
                    objMdlContractHBAPI.programlimit_validdto = objODBCDatareader["programlimit_validdto"].ToString();
                    objMdlContractHBAPI.rate_interest = objODBCDatareader["rate_interest"].ToString();
                    objMdlContractHBAPI.penal_interest = objODBCDatareader["penal_interest"].ToString();
                    objMdlContractHBAPI.trade_orginatedby = objODBCDatareader["trade_orginatedby"].ToString();
                    objMdlContractHBAPI.loanfacility_amount = objODBCDatareader["loanfacility_amount"].ToString();
                    objMdlContractHBAPI.SA_Brokerage = objODBCDatareader["SA_Brokerage"].ToString();
                    objMdlContractHBAPI.loan_type = objODBCDatareader["loan_type"].ToString();
                    objMdlContractHBAPI.productsub_type = objODBCDatareader["productsub_type"].ToString();
                    objMdlContractHBAPI.facility_mode = objODBCDatareader["facility_mode"].ToString();
                }
                objODBCDatareader.Close();

                if(DateTime.Parse(objMdlContractHBAPI.programlimit_validdto) < DateTime.Now)
                {
                    objMdlHBAPIConnDAResponse.status = false;
                    objMdlHBAPIConnDAResponse.message = "Ending Date of Contract has expired. Since such contracts become inactive by default in NetSuite, this contract cannot be posted.";
                    return objMdlHBAPIConnDAResponse;
                }
               

                msSQL = " select producttype_gid,productsubtype_gid from agr_mst_tapplication2loan" +
                      " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsproducttype_gid = objODBCDatareader["producttype_gid"].ToString();
                    lsproductsubtype_gid = objODBCDatareader["productsubtype_gid"].ToString();
                }
                objODBCDatareader.Close();

                objMdlContractHBAPI.program_erpid = objHBAPICmnFunctions.getProgramERPID(lsproductsubtype_gid, lsproducttype_gid);

                msSQL = " select insurance_applicability, insurance_availability from agr_mst_tapplication2product where application2loan_gid = '" + lsapplication2loan_gid + "' order by created_date limit 1";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractHBAPI.insurance_applicability = objODBCDatareader["insurance_applicability"].ToString();
                    objMdlContractHBAPI.insurance_limit = objODBCDatareader["insurance_availability"].ToString();
                }

                msSQL = " select completed_flag from agr_trn_tprocesstypeassign where menu_gid = '" + HBAPICADProcessMenuGID.SoftcopyVetting + "' and application_gid = '" + application_gid + "'";
                lsdeferral_status = objdbconn.GetExecuteScalar(msSQL);

                objMdlContractHBAPI.deferral_status = lsdeferral_status == "Y" ? "No" : "Yes";

                msSQL = " select processing_collectiontype,processing_fee,doccharge_collectiontype,doc_charges from agr_mst_tapplicationservicecharge" +
                       " where application2loan_gid = '" + lsapplication2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractHBAPI.processing_collectiontype = objODBCDatareader["processing_collectiontype"].ToString();
                    objMdlContractHBAPI.processing_fee = objODBCDatareader["processing_fee"].ToString();
                    objMdlContractHBAPI.doccharge_collectiontype = objODBCDatareader["doccharge_collectiontype"].ToString();
                    objMdlContractHBAPI.doc_charges = objODBCDatareader["doc_charges"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select scopeof_transport,scopeof_loading,scopeof_unloading,scopeof_qualityandquantity,scopeof_moisturegainloss,scopeof_insurance from agr_mst_tapplication2trade" +
                       " where application2loan_gid = '" + lsapplication2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractHBAPI.scopeof_transport = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_transport"].ToString());
                    objMdlContractHBAPI.scopeof_loading = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_loading"].ToString());
                    objMdlContractHBAPI.scopeof_unloading = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_unloading"].ToString());
                    objMdlContractHBAPI.scopeof_qualityandquantity = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_qualityandquantity"].ToString());
                    objMdlContractHBAPI.scopeof_moisturegainloss = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_moisturegainloss"].ToString());
                    objMdlContractHBAPI.scopeof_insurance = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_insurance"].ToString());

                }

                LogForAuditContractHBAPI("Contract details fetched from DB");

                string ContractHBAPIJSON = JsonConvert.SerializeObject(objMdlContractHBAPI);

                LogForAuditContractHBAPI("Contract Json for sending to Hyperbridge obtained");
                LogForAuditContractHBAPI(ContractHBAPIJSON);
                LogForAuditContractHBAPI("End of Contract JSON");



                HBAPIContractResponse objHBAPIContractResponse = new HBAPIContractResponse();

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.PostContract);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", ContractHBAPIJSON);
                IRestResponse response = client.Execute(request);
                objHBAPIContractResponse = JsonConvert.DeserializeObject<HBAPIContractResponse>(response.Content);

                LogForAuditContractHBAPI("Response from Hyperbridge");
                LogForAuditContractHBAPI(response.StatusCode.ToString());
                LogForAuditContractHBAPI(response.Content);
                LogForAuditContractHBAPI("End of Response");

                if (!String.IsNullOrEmpty(objHBAPIContractResponse.contract_erpid))
                {
                    msSQL = " update agr_mst_tapplication set " +
                            " erp_status='" + "Yes" + "'," +
                            " erp_id='" + objHBAPIContractResponse.contract_erpid + "'" +
                            " where application_gid='" + application_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    foreach (KeyValuePair<string, string> entry in objHBAPIContractResponse.addresslist)
                    {
                        msSQL = " update agr_mst_tinstitution2address set " +
                      " erp_id='" + entry.Value + "'" +
                      " where institution2address_gid='" + entry.Key + "' ";

                        mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    MdlHBAPICommodityResponse objMdlHBAPICommodityResponse = new MdlHBAPICommodityResponse();

                    objMdlHBAPICommodityResponse = PostCommodityHBAPI(lsapplication2loan_gid);

                    MdlHBAPISupplierResponse objMdlHBAPISupplierResponse = new MdlHBAPISupplierResponse();

                    objMdlHBAPISupplierResponse = AddSupplierToContractHBAPI(lsapplication2loan_gid);


                    objMdlHBAPIConnDAResponse.status = objHBAPIContractResponse.status;
                    objMdlHBAPIConnDAResponse.message = objHBAPIContractResponse.message;

                    if (objMdlHBAPICommodityResponse.status == false)
                        objMdlHBAPIConnDAResponse.message += "\r\n" + objMdlHBAPICommodityResponse.message;

                    if (objMdlHBAPISupplierResponse.status == false)
                        objMdlHBAPIConnDAResponse.message += "\r\n" + objMdlHBAPISupplierResponse.message;


                }
                else
                {
                    objMdlHBAPIConnDAResponse.status = objHBAPIContractResponse.status;
                    objMdlHBAPIConnDAResponse.message = objHBAPIContractResponse.message;
                    if (objMdlHBAPIConnDAResponse.error_response != null)
                        objMdlHBAPIConnDAResponse.message = objMdlHBAPIConnDAResponse.message + "\r\nNetsuite Response: " + objMdlHBAPIConnDAResponse.error_response;
                }

            }
            catch (Exception ex)
            {
                LogForAuditContractHBAPI("Exception occurred  - " + ex.ToString() + "\r\nException Message - " + ex.Message);
                objMdlHBAPIConnDAResponse.status = false;
                objMdlHBAPIConnDAResponse.message = "Exception occurred in sending Contract details to Hyperbridge..!";
            }
            LogForAuditContractHBAPI("Logging ended\r\n");
            return objMdlHBAPIConnDAResponse;

        }
        public void UpdateContractBasicDetailsHBAPI(string application_gid)
        {
            HBAPIContractUpdateResponse objHBAPIContractUpdateResponse = new HBAPIContractUpdateResponse();
            try
            {
                LogForAuditContractHBAPI("Logging started in API - " + ContractAPIMetaList.UpdateContractBasicDetailsHBAPI);
                LogForAuditContractHBAPI("Application GID - " + application_gid);

                MdlContractBasicDetailsUpdateHBAPI objMdlContractBasicDetailsUpdateHBAPI = new MdlContractBasicDetailsUpdateHBAPI();

                msSQL = " select erp_id from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                lserp_id = objdbconn.GetExecuteScalar(msSQL);

                if (String.IsNullOrEmpty(lserp_id))
                {
                    LogForAuditContractHBAPI("Contract can't be updated since it doesn't have ERP ID");
                    LogForAuditContractHBAPI("Logging ended");
                    return;
                }

                msSQL = " select customerref_name,sa_name" +
                       " from agr_mst_tapplication" +
                       " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractBasicDetailsUpdateHBAPI.company_name = objODBCDatareader["customerref_name"].ToString();
                    objMdlContractBasicDetailsUpdateHBAPI.sa_name = objODBCDatareader["sa_name"].ToString();
                }
                objODBCDatareader.Close();


                string ContractBasicDetailsUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlContractBasicDetailsUpdateHBAPI);

                LogForAuditContractHBAPI("Contract Json for sending to Hyperbridge obtained");
                LogForAuditContractHBAPI(ContractBasicDetailsUpdateHBAPIJSON);
                LogForAuditContractHBAPI("End of Contract JSON");

                msSQL = " select contract_id,erp_id from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    contract_id = objODBCDatareader["contract_id"].ToString();
                    contract_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();



                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.UpdateContractBasicDetails);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", ContractBasicDetailsUpdateHBAPIJSON);
                request.AddParameter("contract_erpid", contract_erpid);
                request.AddParameter("contract_id", contract_id);


                IRestResponse response = client.Execute(request);

                LogForAuditContractHBAPI("Response from Hyperbridge");
                LogForAuditContractHBAPI("Status Code - " + response.StatusCode.ToString());
                LogForAuditContractHBAPI("Content - " + response.Content);
                LogForAuditContractHBAPI("End of Response");

                objHBAPIContractUpdateResponse = JsonConvert.DeserializeObject<HBAPIContractUpdateResponse>(response.Content);

                if (objHBAPIContractUpdateResponse.status == true)
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }
                else
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }



            }
            catch (Exception ex)
            {
                LogForAuditContractHBAPI("Exception occurred  - " + ex.ToString() + "\r\nException Message - " + ex.Message);
            }
            LogForAuditContractHBAPI("Logging Ended");
        }

        public void UpdateContractDeferralStatusHBAPI(string application_gid)
        {
            HBAPIContractUpdateResponse objHBAPIContractUpdateResponse = new HBAPIContractUpdateResponse();
            try
            {
                LogForAuditContractHBAPI("Logging started in API - " + ContractAPIMetaList.UpdateContractDeferralStatusHBAPI);
                LogForAuditContractHBAPI("Application GID - " + application_gid);

                MdlContractDeferralStatusUpdateHBAPI objMdlContractDeferralStatusUpdateHBAPI = new MdlContractDeferralStatusUpdateHBAPI();

                msSQL = " select erp_id from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                lserp_id = objdbconn.GetExecuteScalar(msSQL);

                if (String.IsNullOrEmpty(lserp_id))
                {
                    LogForAuditContractHBAPI("Contract can't be updated since it doesn't have ERP ID");
                    LogForAuditContractHBAPI("Logging ended");
                    return;
                }

                msSQL = " select completed_flag from agr_trn_tprocesstypeassign where menu_gid = '" + HBAPICADProcessMenuGID.SoftcopyVetting + "' and application_gid = '" + application_gid + "'";
                lsdeferral_status = objdbconn.GetExecuteScalar(msSQL);

                objMdlContractDeferralStatusUpdateHBAPI.deferral_status = lsdeferral_status == "Y" ? "No" : "Yes";


                string ContractDeferralStatusUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlContractDeferralStatusUpdateHBAPI);

                LogForAuditContractHBAPI("Contract Json for sending to Hyperbridge obtained");
                LogForAuditContractHBAPI(ContractDeferralStatusUpdateHBAPIJSON);
                LogForAuditContractHBAPI("End of Contract JSON");

                msSQL = " select contract_id,erp_id from agr_mst_tapplication where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    contract_id = objODBCDatareader["contract_id"].ToString();
                    contract_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();



                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.UpdateContractDeferralStatus);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", ContractDeferralStatusUpdateHBAPIJSON);
                request.AddParameter("contract_erpid", contract_erpid);
                request.AddParameter("contract_id", contract_id);


                IRestResponse response = client.Execute(request);

                LogForAuditContractHBAPI("Response from Hyperbridge");
                LogForAuditContractHBAPI("Status Code - " + response.StatusCode.ToString());
                LogForAuditContractHBAPI("Content - " + response.Content);
                LogForAuditContractHBAPI("End of Response");

                objHBAPIContractUpdateResponse = JsonConvert.DeserializeObject<HBAPIContractUpdateResponse>(response.Content);

                if (objHBAPIContractUpdateResponse.status == true)
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }
                else
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }



            }
            catch (Exception ex)
            {
                LogForAuditContractHBAPI("Exception occurred  - " + ex.ToString() + "\r\nException Message - " + ex.Message);
            }
            LogForAuditContractHBAPI("Logging Ended");
        }
        public void UpdateContractInstitutionHBAPI(string institution_gid)
        {
            HBAPIContractUpdateResponse objHBAPIContractUpdateResponse = new HBAPIContractUpdateResponse();
            try
            {
                LogForAuditContractHBAPI("Logging started in API - " + ContractAPIMetaList.UpdateContractInstitutionHBAPI);
                LogForAuditContractHBAPI("Institution GID - " + institution_gid);

                MdlContractInstitutionUpdateHBAPI objMdlContractInstitutionUpdateHBAPI = new MdlContractInstitutionUpdateHBAPI();

                msSQL = " select application_gid from agr_mst_tinstitution where institution_gid = '" + institution_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select erp_id from agr_mst_tapplication where application_gid = '" + lsapplication_gid + "'";
                lserp_id = objdbconn.GetExecuteScalar(msSQL);

                if(String.IsNullOrEmpty(lserp_id)) {
                    LogForAuditContractHBAPI("Contract can't be updated since it doesn't have ERP ID");
                    LogForAuditContractHBAPI("Logging ended");
                    return;
                }

                msSQL = " select officialemail_address,official_telephoneno,company_name,companypan_no,companytype_gid,tan_number,incometax_returnsstatus," +
                        " lastyear_turnover from agr_mst_tinstitution" +
                        " where institution_gid = '" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                { 
                    objMdlContractInstitutionUpdateHBAPI.officialemail_address = objODBCDatareader["officialemail_address"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.official_telephoneno = objODBCDatareader["official_telephoneno"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.companypan_no = objODBCDatareader["companypan_no"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.companytype_name = objHBAPICmnFunctions.fetchCompanyTypeName(objODBCDatareader["companytype_gid"].ToString());
                    objMdlContractInstitutionUpdateHBAPI.tan_number = objODBCDatareader["tan_number"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.incometax_returnsstatus = objODBCDatareader["incometax_returnsstatus"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.lastyear_turnover = objODBCDatareader["lastyear_turnover"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select iec_no" + " from agr_trn_tiecdtl where function_gid='" + institution_gid + "' order by created_date limit 1";
                lsiec_licenseno = objdbconn.GetExecuteScalar(msSQL);

                if (!String.IsNullOrEmpty(lsiec_licenseno))
                    objMdlContractInstitutionUpdateHBAPI.iec_licenseno = lsiec_licenseno;

                msSQL = " select bankaccount_number,bankaccount_name,ifsc_code,bank_name,micr_code,bank_address,branch_name" +
                       " from agr_mst_tinstitution2bankdtl where institution_gid='" + institution_gid + "' and primary_status = 'Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractInstitutionUpdateHBAPI.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.accountholder_name = objODBCDatareader["bankaccount_name"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.bank_name = objODBCDatareader["bank_name"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.micr_code = objODBCDatareader["micr_code"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.bank_address = objODBCDatareader["bank_address"].ToString();
                    objMdlContractInstitutionUpdateHBAPI.branch_name = objODBCDatareader["branch_name"].ToString();
                }
                objODBCDatareader.Close();

                LogForAuditContractHBAPI("Contract details fetched from DB");

                string ContractInstitutionUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlContractInstitutionUpdateHBAPI);

                LogForAuditContractHBAPI("Contract Json for sending to Hyperbridge obtained");
                LogForAuditContractHBAPI(ContractInstitutionUpdateHBAPIJSON);
                LogForAuditContractHBAPI("End of Contract JSON");

                msSQL = " select application_gid from agr_mst_tinstitution where institution_gid = '" + institution_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contract_id,erp_id from agr_mst_tapplication where application_gid = '" + lsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    contract_id = objODBCDatareader["contract_id"].ToString();
                    contract_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();



                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.UpdateContractInstitution);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", ContractInstitutionUpdateHBAPIJSON);
                request.AddParameter("contract_erpid", contract_erpid);
                request.AddParameter("contract_id", contract_id);


                IRestResponse response = client.Execute(request);

                LogForAuditContractHBAPI("Response from Hyperbridge");
                LogForAuditContractHBAPI("Status Code - " + response.StatusCode.ToString());
                LogForAuditContractHBAPI("Content - " + response.Content);
                LogForAuditContractHBAPI("End of Response");

                objHBAPIContractUpdateResponse = JsonConvert.DeserializeObject<HBAPIContractUpdateResponse>(response.Content);

                if (objHBAPIContractUpdateResponse.status == true)
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }
                else
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }

            }
            catch (Exception ex)
            {
                LogForAuditContractHBAPI("Exception occurred  - " + ex.ToString() + "\r\nException Message - " + ex.Message);
            }
            LogForAuditContractHBAPI("Logging Ended");
        }

        public void UpdateContractProductHBAPI(string application2loan_gid)
        {
            HBAPIContractUpdateResponse objHBAPIContractUpdateResponse = new HBAPIContractUpdateResponse();
            try
            {
                LogForAuditContractHBAPI("Logging started in API - " + ContractAPIMetaList.UpdateContractProductHBAPI);
                LogForAuditContractHBAPI("Application2Loan GID - " + application2loan_gid);

                msSQL = " select producttype_gid,productsubtype_gid from agr_mst_tapplication2loan" +
                      " where application2loan_gid = '" + application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsproducttype_gid = objODBCDatareader["producttype_gid"].ToString();
                    lsproductsubtype_gid = objODBCDatareader["productsubtype_gid"].ToString();
                }
                objODBCDatareader.Close();

                lsprogram_erpid = objHBAPICmnFunctions.getProgramERPID(lsproductsubtype_gid, lsproducttype_gid);

                if (String.IsNullOrEmpty(lsprogram_erpid))
                {
                    LogForAuditContractHBAPI("Product updates not posted to ERP since program is not mapped to ERP");
                    LogForAuditContractHBAPI("Logging Ended");
                    return;
                }

                MdlContractProductUpdateHBAPI objMdlContractProductUpdateHBAPI = new MdlContractProductUpdateHBAPI();

                msSQL = " select date_format(programlimit_validdfrom,'%Y-%m-%d') as programlimit_validdfrom,date_format(programlimit_validdto,'%Y-%m-%d') as programlimit_validdto,rate_interest,penal_interest,trade_orginatedby,loanfacility_amount,SA_Brokerage,loan_type,productsub_type,facility_mode" +
                        " from agr_mst_tapplication2loan" +
                        " where application2loan_gid = '" + application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractProductUpdateHBAPI.programlimit_validdfrom = objODBCDatareader["programlimit_validdfrom"].ToString();
                    objMdlContractProductUpdateHBAPI.programlimit_validdto = objODBCDatareader["programlimit_validdto"].ToString();
                    objMdlContractProductUpdateHBAPI.rate_interest = objODBCDatareader["rate_interest"].ToString();
                    objMdlContractProductUpdateHBAPI.penal_interest = objODBCDatareader["penal_interest"].ToString();
                    objMdlContractProductUpdateHBAPI.trade_orginatedby = objODBCDatareader["trade_orginatedby"].ToString();
                    objMdlContractProductUpdateHBAPI.loanfacility_amount = objODBCDatareader["loanfacility_amount"].ToString();
                    objMdlContractProductUpdateHBAPI.SA_Brokerage = objODBCDatareader["SA_Brokerage"].ToString();
                    objMdlContractProductUpdateHBAPI.loan_type = objODBCDatareader["loan_type"].ToString();
                    objMdlContractProductUpdateHBAPI.productsub_type = objODBCDatareader["productsub_type"].ToString();
                    objMdlContractProductUpdateHBAPI.facility_mode = objODBCDatareader["facility_mode"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select insurance_applicability, insurance_availability from agr_mst_tapplication2product where application2loan_gid = '" + application2loan_gid + "' order by created_date limit 1";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractProductUpdateHBAPI.insurance_applicability = objODBCDatareader["insurance_applicability"].ToString();
                    objMdlContractProductUpdateHBAPI.insurance_limit = objODBCDatareader["insurance_availability"].ToString();
                }

                objMdlContractProductUpdateHBAPI.program_erpid = objHBAPICmnFunctions.getProgramERPID(lsproductsubtype_gid, lsproducttype_gid);

                LogForAuditContractHBAPI("Contract details fetched from DB");

                string ContractChargesUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlContractProductUpdateHBAPI);

                LogForAuditContractHBAPI("Contract Json for sending to Hyperbridge obtained");
                LogForAuditContractHBAPI(ContractChargesUpdateHBAPIJSON);
                LogForAuditContractHBAPI("End of Contract JSON");

                msSQL = " select application_gid from agr_mst_tapplication2loan where application2loan_gid = '" + application2loan_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contract_id,erp_id from agr_mst_tapplication where application_gid = '" + lsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    contract_id = objODBCDatareader["contract_id"].ToString();
                    contract_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();

               

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.UpdateContractProduct);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", ContractChargesUpdateHBAPIJSON);
                request.AddParameter("contract_erpid", contract_erpid);
                request.AddParameter("contract_id", contract_id);


                IRestResponse response = client.Execute(request);

                LogForAuditContractHBAPI("Response from Hyperbridge");
                LogForAuditContractHBAPI("Status Code - " + response.StatusCode.ToString());
                LogForAuditContractHBAPI("Content - " + response.Content);
                LogForAuditContractHBAPI("End of Response");

                objHBAPIContractUpdateResponse = JsonConvert.DeserializeObject<HBAPIContractUpdateResponse>(response.Content);

                if (objHBAPIContractUpdateResponse.status == true)
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }
                else
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }

            }
            catch (Exception ex)
            {
                LogForAuditContractHBAPI("Exception occurred  - " + ex.ToString() + "\r\nException Message - " + ex.Message);
            }
            LogForAuditContractHBAPI("Logging Ended");
        }
        public void UpdateContractChargesHBAPI(string application2servicecharge_gid)
        {
            HBAPIContractUpdateResponse objHBAPIContractUpdateResponse = new HBAPIContractUpdateResponse();
            try
            {
                LogForAuditContractHBAPI("Logging started in API - " + ContractAPIMetaList.UpdateContractChargesHBAPI);
                LogForAuditContractHBAPI("Application2Servicecharges GID - " + application2servicecharge_gid);

                msSQL = " select application2loan_gid from agr_mst_tapplicationservicecharge where application2servicecharge_gid = '" + application2servicecharge_gid + "'";
                lsapplication2loan_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select producttype_gid,productsubtype_gid from agr_mst_tapplication2loan" +
                      " where application2loan_gid = '" + lsapplication2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsproducttype_gid = objODBCDatareader["producttype_gid"].ToString();
                    lsproductsubtype_gid = objODBCDatareader["productsubtype_gid"].ToString();
                }
                objODBCDatareader.Close();

                lsprogram_erpid = objHBAPICmnFunctions.getProgramERPID(lsproductsubtype_gid, lsproducttype_gid);

                if(String.IsNullOrEmpty(lsprogram_erpid))
                {
                    LogForAuditContractHBAPI("Service charge updates not posted to ERP since the corresponding program is not mapped to ERP");
                    LogForAuditContractHBAPI("Logging Ended");
                    return;
                }

                MdlContractChargesUpdateHBAPI objMdlContractChargesUpdateHBAPI = new MdlContractChargesUpdateHBAPI();

                msSQL = " select processing_collectiontype,processing_fee,doccharge_collectiontype,doc_charges" +
                        " from agr_mst_tapplicationservicecharge" +
                        " where application2servicecharge_gid = '" + application2servicecharge_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractChargesUpdateHBAPI.processing_collectiontype = objODBCDatareader["processing_collectiontype"].ToString();
                    objMdlContractChargesUpdateHBAPI.processing_fee = objODBCDatareader["processing_fee"].ToString();
                    objMdlContractChargesUpdateHBAPI.doccharge_collectiontype = objODBCDatareader["doccharge_collectiontype"].ToString();
                    objMdlContractChargesUpdateHBAPI.doc_charges = objODBCDatareader["doc_charges"].ToString();
                }
                objODBCDatareader.Close();

                LogForAuditContractHBAPI("Contract details fetched from DB");

                string ContractChargesUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlContractChargesUpdateHBAPI);

                LogForAuditContractHBAPI("Contract Json for sending to Hyperbridge obtained");
                LogForAuditContractHBAPI(ContractChargesUpdateHBAPIJSON);
                LogForAuditContractHBAPI("End of Contract JSON");

                msSQL = " select application_gid from agr_mst_tapplicationservicecharge where application2servicecharge_gid = '" + application2servicecharge_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contract_id,erp_id from agr_mst_tapplication where application_gid = '" + lsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    contract_id = objODBCDatareader["contract_id"].ToString();
                    contract_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.UpdateContractCharges);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", ContractChargesUpdateHBAPIJSON);
                request.AddParameter("contract_erpid", contract_erpid);
                request.AddParameter("contract_id", contract_id);


                IRestResponse response = client.Execute(request);

                LogForAuditContractHBAPI("Response from Hyperbridge");
                LogForAuditContractHBAPI("Status Code - " + response.StatusCode.ToString());
                LogForAuditContractHBAPI("Content - " + response.Content);
                LogForAuditContractHBAPI("End of Response");

                objHBAPIContractUpdateResponse = JsonConvert.DeserializeObject<HBAPIContractUpdateResponse>(response.Content);

                if (objHBAPIContractUpdateResponse.status == true)
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }
                else
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }

            }
            catch (Exception ex)
            {
                LogForAuditContractHBAPI("Exception occurred  - " + ex.ToString() + "\r\nException Message - " + ex.Message);
            }
            LogForAuditContractHBAPI("Logging Ended");
        }

        public void UpdateContractTradeHBAPI(string application2trade_gid)
        {
            HBAPIContractUpdateResponse objHBAPIContractUpdateResponse = new HBAPIContractUpdateResponse();
            try
            {
                LogForAuditContractHBAPI("Logging started in API - " + ContractAPIMetaList.UpdateContractTradeHBAPI);
                LogForAuditContractHBAPI("Application2Trade GID - " + application2trade_gid);

                msSQL = " select application2loan_gid from agr_mst_tapplication2trade where application2trade_gid = '" + application2trade_gid + "'";
                lsapplication2loan_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select producttype_gid,productsubtype_gid from agr_mst_tapplication2loan" +
                        " where application2loan_gid = '" + lsapplication2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsproducttype_gid = objODBCDatareader["producttype_gid"].ToString();
                    lsproductsubtype_gid = objODBCDatareader["productsubtype_gid"].ToString();
                }
                objODBCDatareader.Close();

                lsprogram_erpid = objHBAPICmnFunctions.getProgramERPID(lsproductsubtype_gid, lsproducttype_gid);

                if (String.IsNullOrEmpty(lsprogram_erpid))
                {
                    LogForAuditContractHBAPI("Trade updates not posted to ERP since the corresponding program is not mapped to ERP");
                    LogForAuditContractHBAPI("Logging Ended");
                    return;
                }

                MdlContractTradeUpdateHBAPI objMdlContractTradeUpdateHBAPI = new MdlContractTradeUpdateHBAPI();

                msSQL = " select scopeof_transport,scopeof_loading,scopeof_unloading,scopeof_qualityandquantity,scopeof_moisturegainloss,scopeof_insurance" +
                        " from agr_mst_tapplication2trade" +
                        " where application2trade_gid = '" + application2trade_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractTradeUpdateHBAPI.scopeof_transport = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_transport"].ToString());
                    objMdlContractTradeUpdateHBAPI.scopeof_loading = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_loading"].ToString());
                    objMdlContractTradeUpdateHBAPI.scopeof_unloading = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_unloading"].ToString());
                    objMdlContractTradeUpdateHBAPI.scopeof_qualityandquantity = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_qualityandquantity"].ToString());
                    objMdlContractTradeUpdateHBAPI.scopeof_moisturegainloss = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_moisturegainloss"].ToString());
                    objMdlContractTradeUpdateHBAPI.scopeof_insurance = objHBAPICmnFunctions.getScopeERPID(objODBCDatareader["scopeof_insurance"].ToString());

                }
                objODBCDatareader.Close();

                LogForAuditContractHBAPI("Contract details fetched from DB");

                string ContractTradeUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlContractTradeUpdateHBAPI);

                LogForAuditContractHBAPI("Contract Json for sending to Hyperbridge obtained");
                LogForAuditContractHBAPI(ContractTradeUpdateHBAPIJSON);
                LogForAuditContractHBAPI("End of Contract JSON");

                msSQL = " select application_gid from agr_mst_tapplication2trade where application2trade_gid = '" + application2trade_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contract_id,erp_id from agr_mst_tapplication where application_gid = '" + lsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    contract_id = objODBCDatareader["contract_id"].ToString();
                    contract_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.UpdateContractTrade);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", ContractTradeUpdateHBAPIJSON);
                request.AddParameter("contract_erpid", contract_erpid);
                request.AddParameter("contract_id", contract_id);


                IRestResponse response = client.Execute(request);

                LogForAuditContractHBAPI("Response from Hyperbridge");
                LogForAuditContractHBAPI("Status Code - " + response.StatusCode.ToString());
                LogForAuditContractHBAPI("Content - " + response.Content);
                LogForAuditContractHBAPI("End of Response");

                objHBAPIContractUpdateResponse = JsonConvert.DeserializeObject<HBAPIContractUpdateResponse>(response.Content);

                if (objHBAPIContractUpdateResponse.status == true)
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }
                else
                {
                    LogForAuditContractHBAPI(objHBAPIContractUpdateResponse.message);
                }

            }
            catch (Exception ex)
            {
                LogForAuditContractHBAPI("Exception occurred  - " + ex.ToString() + "\r\nException Message - " + ex.Message);
            }
            LogForAuditContractHBAPI("Logging Ended");
        }

        public MdlHBAPICommodityResponse PostCommodityHBAPI(string application2loan_gid)
        {
            MdlHBAPICommodityResponse objMdlHBAPICommodityResponse = new MdlHBAPICommodityResponse();

            var failedcommodity_list = new List<string>();

            int commoditySuccessCount = 0;

            msSQL = " select application_gid from agr_mst_tapplication2loan where application2loan_gid = '" + application2loan_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select contract_id from agr_mst_tapplication where application_gid = '" + lsapplication_gid + "'";
            lscontract_externalid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " select application2product_gid from agr_mst_tapplication2product where application2loan_gid = '" + application2loan_gid + "' and erp_status = 'No'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    MdlCommodityHBAPI objMdlCommodityHBAPI = new MdlCommodityHBAPI();

                    objMdlCommodityHBAPI.contract_externalid = lscontract_externalid;

                    msSQL = " select variety_gid,variety_name,quantity,uom_gid,commodity_margin,creditperiod_days from agr_mst_tapplication2product where application2product_gid = '" + dt["application2product_gid"] + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        lsvariety_gid = objODBCDatareader["variety_gid"].ToString();
                        lsvariety_name = objODBCDatareader["variety_name"].ToString();
                        objMdlCommodityHBAPI.quantity = objODBCDatareader["quantity"].ToString();
                        lsuom_gid = objODBCDatareader["uom_gid"].ToString();
                        objMdlCommodityHBAPI.margin = objODBCDatareader["commodity_margin"].ToString();
                        objMdlCommodityHBAPI.creditperiod_days = objODBCDatareader["creditperiod_days"].ToString();

                    }
                    objODBCDatareader.Close();


                        msSQL = " select erp_id from ocs_mst_tvariety where variety_gid = '" + lsvariety_gid + "'";
                    objMdlCommodityHBAPI.commodity_erpid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select lms_code from agr_mst_tuom where uom_gid = '" + lsuom_gid + "'";
                    objMdlCommodityHBAPI.uom = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select paymenttypecustomer_gid from agr_mst_tapploan2paymenttypecustomer where application2loan_gid = '" + application2loan_gid + "' and customerpaymenttype_name ='Advance'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    objMdlCommodityHBAPI.customer_advance = objODBCDatareader.HasRows == true ? true : false;

                    msSQL = "select paymenttypecustomer_gid from agr_mst_tapploan2paymenttypecustomer where application2loan_gid = '" + application2loan_gid + "' and customerpaymenttype_name ='Milestone'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    objMdlCommodityHBAPI.customer_milestone = objODBCDatareader.HasRows == true ? true : false;

                    msSQL = "select paymenttypecustomer_gid from agr_mst_tapploan2paymenttypecustomer where application2loan_gid = '" + application2loan_gid + "' and customerpaymenttype_name ='Retention'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    objMdlCommodityHBAPI.customer_retention = objODBCDatareader.HasRows == true ? true : false;

                    msSQL = "select apploan2supplierpayment_gid,maxpercent_paymentterm from agr_mst_tapploan2supplierpayment where application2loan_gid = '" + application2loan_gid + "' and supplierpayment_type ='Advance' order by created_date limit 1";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    objMdlCommodityHBAPI.supplier_advance = objODBCDatareader.HasRows == true ? true : false;
                    objMdlCommodityHBAPI.supplier_advance_maxpercent = objODBCDatareader.HasRows == true ? objODBCDatareader["maxpercent_paymentterm"].ToString() : "0";

                    msSQL = "select apploan2supplierpayment_gid,maxpercent_paymentterm from agr_mst_tapploan2supplierpayment where application2loan_gid = '" + application2loan_gid + "' and supplierpayment_type ='Milestone' order by created_date limit 1";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    objMdlCommodityHBAPI.supplier_milestone = objODBCDatareader.HasRows == true ? true : false;
                    objMdlCommodityHBAPI.supplier_milestone_maxpercent = objODBCDatareader.HasRows == true ? objODBCDatareader["maxpercent_paymentterm"].ToString() : "0";

                    msSQL = "select apploan2supplierpayment_gid,maxpercent_paymentterm from agr_mst_tapploan2supplierpayment where application2loan_gid = '" + application2loan_gid + "' and supplierpayment_type ='Retention' order by created_date limit 1";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    objMdlCommodityHBAPI.supplier_retention = objODBCDatareader.HasRows == true ? true : false;
                    objMdlCommodityHBAPI.supplier_retention_maxpercent = objODBCDatareader.HasRows == true ? objODBCDatareader["maxpercent_paymentterm"].ToString() : "0";

                    LogForAuditContractHBAPI("Commodity details fetched from DB");

                    string CommodityHBAPIJSON = JsonConvert.SerializeObject(objMdlCommodityHBAPI);

                    LogForAuditContractHBAPI("Commodity Json for sending to Hyperbridge obtained");
                    LogForAuditContractHBAPI(CommodityHBAPIJSON);
                    LogForAuditContractHBAPI("End of Commodity JSON");

                    HBAPIContractResponse objHBAPIContractResponse = new HBAPIContractResponse();

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    var clientCommodity = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.PostCommodity);
                    var requestCommodity = new RestRequest(Method.POST);
                    requestCommodity.AddHeader("content-type", "application/x-www-form-urlencoded");
                    requestCommodity.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    requestCommodity.AddParameter("fromsamagroJSON", CommodityHBAPIJSON);
                    IRestResponse responseCommodity = clientCommodity.Execute(requestCommodity);
                    objHBAPIContractResponse = JsonConvert.DeserializeObject<HBAPIContractResponse>(responseCommodity.Content);

                    if(objHBAPIContractResponse.status == true)
                    {
                        commoditySuccessCount++;

                        msSQL = " update agr_mst_tapplication2product set " +
                        " erp_status='Yes'" +
                        " where application2product_gid='" + dt["application2product_gid"] + "' ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        failedcommodity_list.Add(lsvariety_name);
                    }

                }
            }


            if(commoditySuccessCount == dt_datatable.Rows.Count)
            {
                objMdlHBAPICommodityResponse.status = true;
            }
            else
            {
                objMdlHBAPICommodityResponse.status = false;
                objMdlHBAPICommodityResponse.message = "Commodities " + string.Join(",", failedcommodity_list) + " failed in posting to ERP" ;
            }

            return objMdlHBAPICommodityResponse;

        }

        public MdlHBAPISupplierResponse AddSupplierToContractHBAPI(string application2loan_gid)
        {
            MdlHBAPISupplierResponse objMdlHBAPISupplierResponse = new MdlHBAPISupplierResponse();

            var failedsupplier_list = new List<string>();

            int supplierSuccessCount = 0;

            msSQL = " select application_gid from agr_mst_tapplication2loan where application2loan_gid = '" + application2loan_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select contract_id from agr_mst_tapplication where application_gid = '" + lsapplication_gid + "'";
            lscontract_externalid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select apploan2supplierdtl_gid from agr_mst_tapploan2supplierdtl where application2loan_gid = '" + application2loan_gid + "' and erp_status = 'No'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select supplier_gid from agr_mst_tapploan2supplierdtl where apploan2supplierdtl_gid = '" + dt["apploan2supplierdtl_gid"] + "'";
                    lssupplier_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select supplier_name from agr_mst_tapploan2supplierdtl where apploan2supplierdtl_gid = '" + dt["apploan2supplierdtl_gid"] + "'";
                    lssupplier_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select application_no from agr_mst_tsupronboard where application_gid = '" + lssupplier_gid + "'";
                    lssupplier_id = objdbconn.GetExecuteScalar(msSQL);

                    lsvendor_externalid = lssupplier_id;

                    MdlSupplierAddContractHBAPI objMdlSupplierAddContractHBAPI = new MdlSupplierAddContractHBAPI();

                    objMdlSupplierAddContractHBAPI.contract_externalid = lscontract_externalid;
                    objMdlSupplierAddContractHBAPI.vendor_externalid = lsvendor_externalid;

                    LogForAuditContractHBAPI("Commodity details fetched from DB");

                    string SupplierAddContractHBAPIJSON = JsonConvert.SerializeObject(objMdlSupplierAddContractHBAPI);

                    LogForAuditContractHBAPI("Commodity Json for sending to Hyperbridge obtained");
                    LogForAuditContractHBAPI(SupplierAddContractHBAPIJSON);
                    LogForAuditContractHBAPI("End of Commodity JSON");

                    HBAPIContractResponse objHBAPIContractResponse = new HBAPIContractResponse();

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.AddSupplierToContract);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", SupplierAddContractHBAPIJSON);
                    IRestResponse response = client.Execute(request);
                    objHBAPIContractResponse = JsonConvert.DeserializeObject<HBAPIContractResponse>(response.Content);

                    if (objHBAPIContractResponse.status == true)
                    {
                        supplierSuccessCount++;

                        msSQL = " update agr_mst_tapploan2supplierdtl set " +
                        " erp_status='Yes'" +
                        " where apploan2supplierdtl_gid='" + dt["apploan2supplierdtl_gid"] + "' ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        failedsupplier_list.Add(lssupplier_name);
                    }

                }
            }

            if (supplierSuccessCount == dt_datatable.Rows.Count)
            {
                objMdlHBAPISupplierResponse.status = true;
            }
            else
            {
                objMdlHBAPISupplierResponse.status = false;
                objMdlHBAPISupplierResponse.message = "Suppliers " + string.Join(",", failedsupplier_list) + " failed in posting to ERP";
            }

            return objMdlHBAPISupplierResponse;

        }

        public void UpdateContractInstitutionAddressHBAPI(string institution2address_gid)
        {
            HBAPIContractUpdateResponse objHBAPIContractUpdateResponse = new HBAPIContractUpdateResponse();
            try
            {
                LogForAuditContractHBAPI("Logging started in API - " + ContractAPIMetaList.UpdateContractInstitutionAddressHBAPI + " for Contract with Address GID -" + institution2address_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));

                MdlContractAddressUpdateHBAPI objMdlContractAddressUpdateHBAPI = new MdlContractAddressUpdateHBAPI();

                msSQL = " select institution_gid from agr_mst_tinstitution2address" +
                        " where institution2address_gid = '" + institution2address_gid + "'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select application_gid from agr_mst_tinstitution" +
                        " where institution_gid = '" + lsinstitution_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select erp_id,contract_id from agr_mst_tapplication" +
                        " where application_gid = '" + lsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    customer_erpid = objODBCDatareader["erp_id"].ToString();
                    contract_id = objODBCDatareader["contract_id"].ToString();
                }
                objODBCDatareader.Close();

                LogForAuditContractHBAPI("Contract ERP ID obtained - " + customer_erpid);

                msSQL = " select institution2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude,erp_id" +
                        " from agr_mst_tinstitution2address where institution2address_gid='" + institution2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlContractAddressUpdateHBAPI.externalid = objODBCDatareader["institution2address_gid"].ToString();
                    objMdlContractAddressUpdateHBAPI.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    objMdlContractAddressUpdateHBAPI.addressline1 = objODBCDatareader["addressline1"].ToString();
                    objMdlContractAddressUpdateHBAPI.addressline2 = objODBCDatareader["addressline2"].ToString();
                    objMdlContractAddressUpdateHBAPI.city = objODBCDatareader["city"].ToString();
                    objMdlContractAddressUpdateHBAPI.state = objFnSamAgroHBAPIConn.fetchNsAddrStateCode(objODBCDatareader["state"].ToString());
                    objMdlContractAddressUpdateHBAPI.postal_code = objODBCDatareader["postal_code"].ToString();
                    objMdlContractAddressUpdateHBAPI.latitude = objODBCDatareader["latitude"].ToString();
                    objMdlContractAddressUpdateHBAPI.longitude = objODBCDatareader["longitude"].ToString();
                    address_erpid = objODBCDatareader["erp_id"].ToString();

                }
                objODBCDatareader.Close();

                LogForAuditContractHBAPI("Address ERP ID obtained - " + address_erpid);

                string MdlInstitutionAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlContractAddressUpdateHBAPI);

                HBAPIContractResponse objHBAPIContractResponse = new HBAPIContractResponse();
                LogForAuditContractHBAPI("JSON generated from Contract Address Data");
                LogForAuditContractHBAPI(MdlInstitutionAddressUpdateHBAPI);
                LogForAuditContractHBAPI("End of Contract address JSON");

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.UpdateContractExistingAddress);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlInstitutionAddressUpdateHBAPI);
                request.AddParameter("address_erpid", address_erpid);
                request.AddParameter("customer_erpid", customer_erpid);
                request.AddParameter("contract_id", contract_id);
                IRestResponse response = client.Execute(request);
                objHBAPIContractResponse = JsonConvert.DeserializeObject<HBAPIContractResponse>(response.Content);

                LogForAuditContractHBAPI("Response obtained from HyperbridgeAPI");
                LogForAuditContractHBAPI(response.StatusCode.ToString());
                LogForAuditContractHBAPI(response.Content);
                LogForAuditContractHBAPI("End of Response");

                objHBAPIContractUpdateResponse = JsonConvert.DeserializeObject<HBAPIContractUpdateResponse>(response.Content);

                if (objHBAPIContractUpdateResponse.status == true)
                {
                    LogForAuditContractHBAPI("Update was successful");
                }
                else
                {
                    LogForAuditContractHBAPI("Update was not successful");
                }

            }
            catch (Exception ex)
            {
                LogForAuditContractHBAPI("Exception occured in Update - " + ex.ToString());
            }

        }

        public void UpdateContractInstitutionAddressAddHBAPI(string institution_gid)
        {
            HBAPIContractUpdateResponse objHBAPIContractUpdateResponse = new HBAPIContractUpdateResponse();
            try
            {
                LogForAuditContractHBAPI("Logging started in API - " + ContractAPIMetaList.UpdateContractInstitutionAddressAddHBAPI + " for Contract with Institution GID -" + institution_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));

                MdlInstitutionAddressUpdateAddHBAPI objMdlInstitutionAddressUpdateAddHBAPI = new MdlInstitutionAddressUpdateAddHBAPI();

                msSQL = " select application_gid from agr_mst_tinstitution" +
                        " where institution_gid = '" + institution_gid + "'";
                string application_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select erp_id from agr_mst_tapplication" +
                        " where application_gid = '" + application_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contract_id from agr_mst_tapplication" +
                        " where application_gid = '" + application_gid + "'";
                contract_id = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditContractHBAPI("Contract ERP ID obtained - " + customer_erpid);


                msSQL = " select institution2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude" +
                        " from agr_mst_tinstitution2address where (erp_id is null or erp_id = '') and institution_gid = '" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count > 0)
                {
                    objMdlInstitutionAddressUpdateAddHBAPI.addressDetails = new AddressDetails();

                    objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist = new AddressData[dt_datatable.Rows.Count];

                    for (int i = 0; i < objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist.Length; i++)
                    {
                        objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[i] = new AddressData();

                    }
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int addressind = 0;
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addresstype_name = dr_datarow["addresstype_name"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addressline1 = dr_datarow["addressline1"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addressline2 = dr_datarow["addressline2"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].city = dr_datarow["city"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].state = objFnSamAgroHBAPIConn.fetchNsAddrStateCode(dr_datarow["state"].ToString());
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["institution2address_gid"].ToString();
                            addressind++;
                        }
                    }
                    dt_datatable.Dispose();

                    string MdlInstitutionAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlInstitutionAddressUpdateAddHBAPI);

                    LogForAuditContractHBAPI("JSON generated from Buyer Data");
                    LogForAuditContractHBAPI(MdlInstitutionAddressUpdateHBAPI);
                    LogForAuditContractHBAPI("End of Buyer JSON");

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIContractURL"].ToString() + HBAPINameRepoContract.UpdateContractAddressAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlInstitutionAddressUpdateHBAPI);
                    request.AddParameter("customer_erpid", customer_erpid);
                    request.AddParameter("contract_id", contract_id);


                    IRestResponse response = client.Execute(request);

                    LogForAuditContractHBAPI("Response obtained from HyperbridgeAPI");
                    LogForAuditContractHBAPI(response.StatusCode.ToString());
                    LogForAuditContractHBAPI(response.Content);
                    LogForAuditContractHBAPI("End of Response");

                    objHBAPIContractUpdateResponse = JsonConvert.DeserializeObject<HBAPIContractUpdateResponse>(response.Content);

                    if (objHBAPIContractUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPIContractUpdateResponse.addresslist)
                        {
                            msSQL = " update agr_mst_tinstitution2address set " +
                          " erp_id='" + entry.Value + "'" +
                          " where institution2address_gid='" + entry.Key + "' ";

                            mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditContractHBAPI("Update was successful");

                    }
                    else
                    {
                        LogForAuditContractHBAPI("Update was not successful");
                    }
                }

            }
            catch (Exception ex)
            {
                LogForAuditContractHBAPI("Exception Occured in Update - " + ex.ToString());
            }

        }

        public result1234 fnNsLimitAPI(string buyer_gid)
        {
            limit objlimit = new limit();
            result1234 objresult1234 = new result1234();
            msSQL = "select contract_id from agr_mst_tapplication where buyeronboard_gid = '" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            objlimit.LIMITDETAILS = new LIMITDETAILS();
            objlimit.LIMITDETAILS.limitlist = new LIMITData[] { };
            objlimit.LIMITDETAILS.limitlist = new LIMITData[dt_datatable.Rows.Count];
            for (int i = 0; i < objlimit.LIMITDETAILS.limitlist.Length; i++)
            {
                objlimit.LIMITDETAILS.limitlist[i] = new LIMITData();
            }
            if (dt_datatable.Rows.Count != 0)
            {
                int limitind = 0;
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objlimit.LIMITDETAILS.limitlist[limitind].contract_id = dr_datarow["contract_id"].ToString();
                    limitind++;
                }
                string limitHBAPIJSON = JsonConvert.SerializeObject(objlimit);
                MdlLimitResponse objmdlnslimitresponse = new MdlLimitResponse();
                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIPostURL"].ToString() + HBPostAPINameRepo.NSLimitAPI);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", limitHBAPIJSON);
                IRestResponse response = client.Execute(request);
                objmdlnslimitresponse = JsonConvert.DeserializeObject<MdlLimitResponse>(response.Content);
                if (objmdlnslimitresponse.status == true)
                {
                    if (objmdlnslimitresponse.limitResponselist.limitresponsedata.Length != 0)
                    {
                        foreach (var item in objmdlnslimitresponse.limitResponselist.limitresponsedata)
                        {
                            msSQL = "select a.application_gid from agr_mst_tapplication2loan a left join agr_mst_tapplication b on a.application_gid = b.application_gid where b.buyeronboard_gid = '" + buyer_gid + "' and contract_id = '" + item.contract_id + "' ";
                            string application_gid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "update agr_mst_tapplication2loan set overdue_balance = '" + item.overdue_balance + "',available_limit = '" + item.credit_limit + "' where application_gid = '" + application_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
                else
                {
                    objresult1234.message = objmdlnslimitresponse.message;
                }
            }
            dt_datatable.Dispose();

            return objresult1234;
        }

        //Auxillary Functions

        public MdlContractHBAPI populateBuyerInstitutionForContract(string application_gid, string buyeronboard_gid)
        {

            MdlContractHBAPI objMdlContractHBAPI = new MdlContractHBAPI();

            objMdlContractHBAPI.gstDetails = new GSTDetails();
            objMdlContractHBAPI.addressDetails = new AddressDetails();

            objMdlContractHBAPI.gstDetails.gstlist = new GSTData[] { };
            objMdlContractHBAPI.addressDetails.addresslist = new AddressData[] { };

            msSQL = " select institution_gid from agr_mst_tinstitution" +
                    " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
            lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select institution_gid from agr_mst_tbyronboard2institution" +
                    " where application_gid = '" + buyeronboard_gid + "' and stakeholder_type='Applicant'";
            lsonboardinstitution_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " select customerref_name,sa_name from agr_mst_tapplication" +
                    " where application_gid = '" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objMdlContractHBAPI.company_name = objODBCDatareader["customerref_name"].ToString();
                objMdlContractHBAPI.sa_name = objODBCDatareader["sa_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select application_no,virtualaccount_number,customerbank_name,branch_name,ifsc_code from agr_mst_tbyronboard" +
                      " where application_gid = '" + buyeronboard_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objMdlContractHBAPI.virtualaccount_number = objODBCDatareader["virtualaccount_number"].ToString();
                objMdlContractHBAPI.virtualcustomerbank_name = objODBCDatareader["customerbank_name"].ToString();
                objMdlContractHBAPI.virtualbranch_name = objODBCDatareader["branch_name"].ToString();
                objMdlContractHBAPI.virtualifsc_code = objODBCDatareader["ifsc_code"].ToString();
                objMdlContractHBAPI.customer_id = objODBCDatareader["application_no"].ToString();
            }
            objODBCDatareader.Close();



            msSQL = " select officialemail_address,official_telephoneno,company_name,companypan_no,companytype_gid,tan_number,incometax_returnsstatus," +
                    " lastyear_turnover,urn" +
                    " from agr_mst_tinstitution where institution_gid='" + lsinstitution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objMdlContractHBAPI.officialemail_address = objODBCDatareader["officialemail_address"].ToString();
                objMdlContractHBAPI.official_telephoneno = objODBCDatareader["official_telephoneno"].ToString();
                objMdlContractHBAPI.companypan_no = objODBCDatareader["companypan_no"].ToString();
                objMdlContractHBAPI.companytype_name = objHBAPICmnFunctions.getCompanyTypeName(objODBCDatareader["companytype_gid"].ToString());
                objMdlContractHBAPI.tan_number = objODBCDatareader["tan_number"].ToString();
                objMdlContractHBAPI.incometax_returnsstatus = objODBCDatareader["incometax_returnsstatus"].ToString();
                objMdlContractHBAPI.lastyear_turnover = objODBCDatareader["lastyear_turnover"].ToString();
                
            }
            objODBCDatareader.Close();

            msSQL = " select gst_no from agr_mst_tbyronboardinstitution2branch where headoffice_status = 'Yes' and institution_gid = '" + lsinstitution_gid + "'";
            objMdlContractHBAPI.primary_gst = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select urn" +
                    " from agr_mst_tbyronboard2institution where institution_gid='" + lsonboardinstitution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objMdlContractHBAPI.customer_erpid = objODBCDatareader["urn"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select iec_no" + " from agr_mst_tinstitution2licensedtl where institution_gid='" + lsinstitution_gid + "' order by created_date limit 1";
            objMdlContractHBAPI.iec_licenseno = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " select institution2branch_gid,gst_no,gst_state" +
                        " from agr_mst_tinstitution2branch where institution_gid='" + lsinstitution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            objMdlContractHBAPI.gstDetails.gstlist = new GSTData[dt_datatable.Rows.Count];
            for (int i = 0; i < objMdlContractHBAPI.gstDetails.gstlist.Length; i++)
            {
                objMdlContractHBAPI.gstDetails.gstlist[i] = new GSTData();
            }
            if (dt_datatable.Rows.Count != 0)
            {
                int gstind = 0;
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objMdlContractHBAPI.gstDetails.gstlist[gstind].gst_no = dr_datarow["gst_no"].ToString();
                    objMdlContractHBAPI.gstDetails.gstlist[gstind].externalid = dr_datarow["institution2branch_gid"].ToString();
                    objMdlContractHBAPI.gstDetails.gstlist[gstind].gst_state = objHBAPICmnFunctions.getNsGSTStateSode(dr_datarow["gst_state"].ToString());
                    gstind++;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select institution2address_gid,addresstype_name,addressline1,addressline2,city,state,country,postal_code,latitude,longitude" +
                    " from agr_mst_tinstitution2address where institution_gid='" + lsinstitution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            objMdlContractHBAPI.addressDetails.addresslist = new AddressData[dt_datatable.Rows.Count];

            for (int i = 0; i < objMdlContractHBAPI.addressDetails.addresslist.Length; i++)
            {
                objMdlContractHBAPI.addressDetails.addresslist[i] = new AddressData();

            }
            if (dt_datatable.Rows.Count != 0)
            {
                int addressind = 0;
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objMdlContractHBAPI.addressDetails.addresslist[addressind].addresstype_name = dr_datarow["addresstype_name"].ToString();
                    objMdlContractHBAPI.addressDetails.addresslist[addressind].addressline1 = dr_datarow["addressline1"].ToString();
                    objMdlContractHBAPI.addressDetails.addresslist[addressind].addressline2 = dr_datarow["addressline2"].ToString();
                    objMdlContractHBAPI.addressDetails.addresslist[addressind].city = dr_datarow["city"].ToString();
                    objMdlContractHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                    objMdlContractHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                    objMdlContractHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                    objMdlContractHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["institution2address_gid"].ToString();
                    objMdlContractHBAPI.addressDetails.addresslist[addressind].state = objHBAPICmnFunctions.getNsAddrStateCode(dr_datarow["state"].ToString());
                    addressind++;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select bankaccount_number,bankaccount_name,ifsc_code,bank_name,micr_code,bank_address,branch_name" +
                    " from agr_mst_tinstitution2bankdtl where institution_gid='" + lsinstitution_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objMdlContractHBAPI.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                objMdlContractHBAPI.accountholder_name = objODBCDatareader["bankaccount_name"].ToString();
                objMdlContractHBAPI.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                objMdlContractHBAPI.bank_name = objODBCDatareader["bank_name"].ToString();
                objMdlContractHBAPI.micr_code = objODBCDatareader["micr_code"].ToString();
                objMdlContractHBAPI.bank_address = objODBCDatareader["bank_address"].ToString();
                objMdlContractHBAPI.branch_name = objODBCDatareader["branch_name"].ToString();
            }
            objODBCDatareader.Close();

            return objMdlContractHBAPI;

        }
        //Logging Functions

        public void LogForAuditContractHBAPI(string strVal)
        {
            try
            {
                    loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + objHBAPICmnFunctions.fetchCompanyCode() + "/" + "SamAgro/HyperbridgeAPI/NetsuiteERPLogContract/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    
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