using ems.hbapiconn.Models;
using ems.utilities.Functions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Web;

namespace ems.hbapiconn.Functions
{
    public class FnSamAgroHAPIOtherCreditor
    {
        string msSQL, lsinstitution_gid, lscontact_gid, lsemployee_gid, lsuser_gid, lscount_gstno;
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
        string lserp_id, address_erpid, customer_erpid, othercreditor_erpid, contact2address_gid, lscreditor_gid, OtherCreditor_erpid, creditor_erpid;
        FnSamAgroHBAPIConn obj = new FnSamAgroHBAPIConn();
        
        //Other Creditor Add
        public MdlHBAPIConnDAResponse DaPostOtherCreditorAddHAPI(string creditor_gid, string employee_gid)
        {
            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();
            string type = "OtherCreditor";
            MdlHAPIOtherCreditorResponse objMdlHAPIOtherCreditorEntityResponse = new MdlHAPIOtherCreditorResponse();

            objMdlHAPIOtherCreditorEntityResponse.gstDetails = new GSTDetails();
            objMdlHAPIOtherCreditorEntityResponse.addressDetails = new AddressDetails();

            objMdlHAPIOtherCreditorEntityResponse.gstDetails.gstlist = new GSTData[] { };
            objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist = new AddressData[] { };
            try
            {
                LogForAuditHBAPI("Logging started in API DaPostOtherCreditorAddHAPI for Other Creditor at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                msSQL = "select creditorref_no,Applicant_type,Applicant_category,contactperson_name,email_id,contact_no,aadhar_no, " +
                        "Applicant_name,pan_no from agr_mst_tcreditor " +
                        "where creditor_gid = '" + creditor_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlHAPIOtherCreditorEntityResponse.ExternalID = objODBCDatareader["creditorref_no"].ToString();
                    //objMdlHAPIOtherCreditorEntityResponse.creditorref_no = objODBCDatareader["creditorref_no"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.Applicant_type = objODBCDatareader["Applicant_type"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.Applicant_category = objODBCDatareader["Applicant_category"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.contactperson_name = objODBCDatareader["contactperson_name"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.email_id = objODBCDatareader["email_id"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.contact_no = objODBCDatareader["contact_no"].ToString();

                    objMdlHAPIOtherCreditorEntityResponse.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.Applicant_name = objODBCDatareader["Applicant_name"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.pan_no = objODBCDatareader["pan_no"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select count(*) from agr_mst_tcreditor2branch where creditor_gid = '" + creditor_gid + "'";
                lscount_gstno = objdbconn.GetExecuteScalar(msSQL);

                objMdlHAPIOtherCreditorEntityResponse.company_type = Int32.Parse(lscount_gstno) > 0 ? "Registered" : "Unregistered";

                msSQL = " select account_number,accountholder_name,ifsc_code,branch_address,branch_name, bank_name,micr " +
                       " from agr_mst_tcreditor2cheque " +
                       " where creditor_gid = '" + creditor_gid + "'" +
                       " order by created_date limit 1";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objMdlHAPIOtherCreditorEntityResponse.account_number = objODBCDatareader["account_number"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.branch_address = objODBCDatareader["branch_address"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.bank_name = objODBCDatareader["bank_name"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.branch_name = objODBCDatareader["branch_name"].ToString();


                    objMdlHAPIOtherCreditorEntityResponse.micr = objODBCDatareader["micr"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select agreementinvolvement_type,creditor2agreement_no,date_format(execution_date, '%Y-%m-%d') as execution_date,date_format(expiry_date, '%Y-%m-%d') as expiry_date" +
                        " from agr_mst_tcreditor2agreement " +
                        " where creditor_gid = '" + creditor_gid + "'" +
                        " order by created_date limit 1";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objMdlHAPIOtherCreditorEntityResponse.agreementinvolvement_type = objODBCDatareader["agreementinvolvement_type"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.creditor2agreement_no = objODBCDatareader["creditor2agreement_no"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.execution_date = objODBCDatareader["execution_date"].ToString();
                    objMdlHAPIOtherCreditorEntityResponse.expiry_date = objODBCDatareader["expiry_date"].ToString();
                }
                objODBCDatareader.Close();


                msSQL = " select creditor2branch_gid,gst_no,gst_state" +
                        " from agr_mst_tcreditor2branch where creditor_gid='" + creditor_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                objMdlHAPIOtherCreditorEntityResponse.gstDetails.gstlist = new GSTData[dt_datatable.Rows.Count];
                for (int i = 0; i < objMdlHAPIOtherCreditorEntityResponse.gstDetails.gstlist.Length; i++)
                {
                    objMdlHAPIOtherCreditorEntityResponse.gstDetails.gstlist[i] = new GSTData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int gstind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlHAPIOtherCreditorEntityResponse.gstDetails.gstlist[gstind].gst_no = dr_datarow["gst_no"].ToString();
                        objMdlHAPIOtherCreditorEntityResponse.gstDetails.gstlist[gstind].externalid = dr_datarow["creditor2branch_gid"].ToString();
                        objMdlHAPIOtherCreditorEntityResponse.gstDetails.gstlist[gstind].gst_state = fetchNsGSTStateSode(dr_datarow["gst_state"].ToString());
                        gstind++;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select gst_no from agr_mst_tcreditor2branch where headoffice_status = 'Yes' and creditor_gid = '" + creditor_gid + "'"; 
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objMdlHAPIOtherCreditorEntityResponse.primary_gst = objODBCDatareader["gst_no"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select creditor2address_gid,addresstype_name,addressline1,addressline2,city,state,country,postal_code,latitude,longitude" +
                        " from agr_mst_tcreditor2address where creditor_gid='" + creditor_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist = new AddressData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist.Length; i++)
                {
                    objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist[i] = new AddressData();

                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int addressind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist[addressind].addresstype_name = dr_datarow["addresstype_name"].ToString();
                        objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist[addressind].addressline1 = dr_datarow["addressline1"].ToString();
                        objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist[addressind].addressline2 = dr_datarow["addressline2"].ToString();
                        objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist[addressind].city = dr_datarow["city"].ToString();
                        objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                        objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                        objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                        objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist[addressind].externalid = dr_datarow["creditor2address_gid"].ToString();
                        objMdlHAPIOtherCreditorEntityResponse.addressDetails.addresslist[addressind].state = fetchNsAddrStateCode(dr_datarow["state"].ToString());
                        addressind++;
                    }
                }
                dt_datatable.Dispose();

                string HAPIOtherCreditorEntityResponse = JsonConvert.SerializeObject(objMdlHAPIOtherCreditorEntityResponse);

                //post to hyperbridge
                LogForAuditHBAPI("JSON generated from Other Creditor Approve Add", type);
                LogForAuditHBAPI(HAPIOtherCreditorEntityResponse, type);
                LogForAuditHBAPI("End of Other Creditor Approve Add JSON", type);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls; //SSL Protocol Version Update

                HBAPIOtherCreditorResponse objHBAPIOtherCreditorResponse = new HBAPIOtherCreditorResponse();

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIOtherCreditorURL"].ToString() + HBPostAPINameRepo_OtherCreditor.PostOtherCreditor);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", HAPIOtherCreditorEntityResponse);
                request.AddParameter("approvalPersonName", employee_gid);

                IRestResponse response = client.Execute(request);

                objHBAPIOtherCreditorResponse = JsonConvert.DeserializeObject<HBAPIOtherCreditorResponse>(response.Content);

                LogForAuditHBAPI("Response obtained from HyperbridgeAPI", type);
                LogForAuditHBAPI(response.Content, type);
                LogForAuditHBAPI("End of Response", type);

                if (objHBAPIOtherCreditorResponse.othercreditor_erpid != null && objHBAPIOtherCreditorResponse.othercreditor_erpid != "")
                {
                    msSQL = " update agr_mst_tcreditor set " +
                             " erp_id='" + objHBAPIOtherCreditorResponse.othercreditor_erpid + "'" +
                             " where creditor_gid='" + creditor_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    foreach (KeyValuePair<string, string> entry in objHBAPIOtherCreditorResponse.addresslist)
                    {
                        msSQL = " update agr_mst_tcreditor2address set " +
                      " erp_id='" + entry.Value + "'" +
                      " where creditor2address_gid='" + entry.Key + "' ";

                        mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    LogForAuditHBAPI("Logging ended for Other Creditor with Creditor gid -" + creditor_gid, type);

                    objMdlHBAPIConnDAResponse.status = true;
                    objMdlHBAPIConnDAResponse.message = "Other Creditor posted to ERP successfully..!";
                }
                else
                {

                    objMdlHBAPIConnDAResponse.status = false;
                    objMdlHBAPIConnDAResponse.message = "Posting Other Creditor to ERP failed";
                    objMdlHBAPIConnDAResponse.error_response = objHBAPIOtherCreditorResponse.error_response;
                }
                //post to hyperbridge

            }
            catch (Exception ex)
            {

                objMdlHBAPIConnDAResponse.status = false;
                objMdlHBAPIConnDAResponse.message = "Exception occurred in Posting Other Creditor to ERP..!";

            }
            return objMdlHBAPIConnDAResponse;

        }

        //Other Creditor Update
        public void UpdateOtherCreditorHBAPI(string creditor_gid, string employee_gid)
        {
            HBAPIOtherCreditorUpdateResponse objHBAPIOtherCreditorUpdateResponse = new HBAPIOtherCreditorUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList_OtherCreditor.UpdateOtherCreditorHBAPI + " for Other Creditor with Creditor Gid -" + creditor_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                MdlHAPIOtherCreditorResponseUpdate objMdlHAPIOtherCreditorResponseUpdate = new MdlHAPIOtherCreditorResponseUpdate();

                msSQL = "select creditor_gid,creditorref_no,Applicant_type,Applicant_category,contactperson_name,email_id,contact_no,aadhar_no, " +
                                        "Applicant_name,pan_no,erp_id from agr_mst_tcreditor " +
                                        "where creditor_gid = '" + creditor_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlHAPIOtherCreditorResponseUpdate.ExternalID = objODBCDatareader["creditorref_no"].ToString();
                    //objMdlHAPIOtherCreditorEntityResponse.creditorref_no = objODBCDatareader["creditorref_no"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.Applicant_type = objODBCDatareader["Applicant_type"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.Applicant_category = objODBCDatareader["Applicant_category"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.contactperson_name = objODBCDatareader["contactperson_name"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.email_id = objODBCDatareader["email_id"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.contact_no = objODBCDatareader["contact_no"].ToString();

                    objMdlHAPIOtherCreditorResponseUpdate.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.Applicant_name = objODBCDatareader["Applicant_name"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.pan_no = objODBCDatareader["pan_no"].ToString();
                    lserp_id = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select account_number,accountholder_name,ifsc_code,branch_address, bank_name,branch_name,micr " +
                       "from agr_mst_tcreditor2cheque " +
                       "where creditor_gid = '" + creditor_gid + "' order by created_date asc limit 1 ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objMdlHAPIOtherCreditorResponseUpdate.account_number = objODBCDatareader["account_number"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.branch_address = objODBCDatareader["branch_address"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.branch_name = objODBCDatareader["branch_name"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.bank_name = objODBCDatareader["bank_name"].ToString();

                    objMdlHAPIOtherCreditorResponseUpdate.micr = objODBCDatareader["micr"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select agreementinvolvement_type,creditor2agreement_no,date_format(execution_date, '%Y-%m-%d') as execution_date,date_format(expiry_date, '%Y-%m-%d') as expiry_date" +
                       " from agr_mst_tcreditor2agreement " +
                       " where creditor_gid = '" + creditor_gid + "'" +
                       " order by created_date limit 1";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objMdlHAPIOtherCreditorResponseUpdate.agreementinvolvement_type = objODBCDatareader["agreementinvolvement_type"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.creditor2agreement_no = objODBCDatareader["creditor2agreement_no"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.execution_date = objODBCDatareader["execution_date"].ToString();
                    objMdlHAPIOtherCreditorResponseUpdate.expiry_date = objODBCDatareader["expiry_date"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select gst_no from agr_mst_tcreditor2branch where headoffice_status = 'Yes' and creditor_gid = '" + creditor_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objMdlHAPIOtherCreditorResponseUpdate.primary_gst = objODBCDatareader["gst_no"].ToString();
                }
                objODBCDatareader.Close();

                string OtherCreditorUpdateHBAPI = JsonConvert.SerializeObject(objMdlHAPIOtherCreditorResponseUpdate);

                LogForAuditUpdateHBAPI("JSON generated from Other Creditor Update", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                LogForAuditUpdateHBAPI(OtherCreditorUpdateHBAPI, LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                LogForAuditUpdateHBAPI("End of Other Creditor Update JSON", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + lserp_id, LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIOtherCreditorURL"].ToString() + HBUpdateAPINameRepo_OtherCreditor.UpdateOtherCreditorGeneral);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", OtherCreditorUpdateHBAPI);
                request.AddParameter("creditor_erpid", lserp_id);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                objHBAPIOtherCreditorUpdateResponse = JsonConvert.DeserializeObject<HBAPIOtherCreditorUpdateResponse>(response.Content);

                if (objHBAPIOtherCreditorUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
            }

        }

        //Other Creditor Address Update
        public void UpdateOtherCreditorAddressHBAPI(string creditor2address_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList_OtherCreditor.UpdateOtherCreditorAddressHBAPI + " for Other Creditor with Address GID -" + creditor2address_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                MdlOtherCreditorAddressUpdateHBAPI objMdlOtherCreditorAddressUpdateHBAPI = new MdlOtherCreditorAddressUpdateHBAPI();

                msSQL = " select creditor_gid from agr_mst_tcreditor2address" +
               " where creditor2address_gid = '" + creditor2address_gid + "'";
                lscreditor_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select erp_id from agr_mst_tcreditor" +
               " where creditor_gid = '" + lscreditor_gid + "'";
                creditor_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Other Creditor ERP ID obtained - " + othercreditor_erpid, LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                msSQL = " select creditor2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude,erp_id" +
                        " from agr_mst_tcreditor2address where creditor2address_gid='" + creditor2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlOtherCreditorAddressUpdateHBAPI.externalid = objODBCDatareader["creditor2address_gid"].ToString();
                    objMdlOtherCreditorAddressUpdateHBAPI.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    objMdlOtherCreditorAddressUpdateHBAPI.addressline1 = objODBCDatareader["addressline1"].ToString();
                    objMdlOtherCreditorAddressUpdateHBAPI.addressline2 = objODBCDatareader["addressline2"].ToString();
                    objMdlOtherCreditorAddressUpdateHBAPI.city = objODBCDatareader["city"].ToString();
                    objMdlOtherCreditorAddressUpdateHBAPI.state = obj.fetchNsAddrStateCode(objODBCDatareader["state"].ToString());
                    objMdlOtherCreditorAddressUpdateHBAPI.postal_code = objODBCDatareader["postal_code"].ToString();
                    objMdlOtherCreditorAddressUpdateHBAPI.latitude = objODBCDatareader["latitude"].ToString();
                    objMdlOtherCreditorAddressUpdateHBAPI.longitude = objODBCDatareader["longitude"].ToString();
                    address_erpid = objODBCDatareader["erp_id"].ToString();

                }
                objODBCDatareader.Close();

                LogForAuditUpdateHBAPI("Address ERP ID obtained - " + address_erpid, LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                string MdlOtherCreditorAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlOtherCreditorAddressUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Other Creditor Address", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                LogForAuditUpdateHBAPI(MdlOtherCreditorAddressUpdateHBAPI, LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIOtherCreditorURL"].ToString() + HBUpdateAPINameRepo_OtherCreditor.UpdateOtherCreditorAddress);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlOtherCreditorAddressUpdateHBAPI);
                request.AddParameter("address_erpid", address_erpid);
                request.AddParameter("creditor_erpid", creditor_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
            }

        }

        //Other Creditor Address Add
        public void UpdateOtherCreditorAddressAddHBAPI(string creditor_gid)
        {
            HBAPIOtherCreditorUpdateResponse objHBAPIOtherCreditorUpdateResponse = new HBAPIOtherCreditorUpdateResponse();
            try
            {
                  
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList_OtherCreditor.UpdateOtherCreditorAddressAddHBAPI + " for Other Creditor with Creditor GID -" + creditor_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                MdlOtherCreditorAddressUpdateAddHBAPI objMdlOtherCreditorAddressUpdateAddHBAPI = new MdlOtherCreditorAddressUpdateAddHBAPI();

                msSQL = " select erp_id from agr_mst_tcreditor" +
                " where creditor_gid = '" + creditor_gid + "'";
                othercreditor_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Other Creditor ERP ID obtained - " + othercreditor_erpid, LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);


                msSQL = " select creditor2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude" +
                        " from agr_mst_tcreditor2address where (erp_id is null or erp_id = '') and creditor_gid = '" + creditor_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count > 0)
                {
                    objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails = new AddressDetails();

                    objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist = new AddressData[dt_datatable.Rows.Count];

                    for (int i = 0; i < objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist.Length; i++)
                    {
                        objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist[i] = new AddressData();

                    }
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int addressind = 0;
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addresstype_name = dr_datarow["addresstype_name"].ToString();
                            objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addressline1 = dr_datarow["addressline1"].ToString();
                            objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addressline2 = dr_datarow["addressline2"].ToString();
                            objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].city = dr_datarow["city"].ToString();
                            objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].state = obj.fetchNsAddrStateCode(dr_datarow["state"].ToString());
                            objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                            objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                            objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                            objMdlOtherCreditorAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["creditor2address_gid"].ToString();
                            addressind++;
                        }
                    }
                    dt_datatable.Dispose();

                    string MdlOtherCreditorAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlOtherCreditorAddressUpdateAddHBAPI);

                    LogForAuditUpdateHBAPI("JSON generated from Other Creditor Data", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                    LogForAuditUpdateHBAPI(MdlOtherCreditorAddressUpdateHBAPI, LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                    LogForAuditUpdateHBAPI("End of Other Creditor JSON", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIOtherCreditorURL"].ToString() + HBUpdateAPINameRepo_OtherCreditor.UpdateOtherCreditorAddressAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlOtherCreditorAddressUpdateHBAPI);
                    request.AddParameter("creditor_erpid", othercreditor_erpid);


                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                    objHBAPIOtherCreditorUpdateResponse = JsonConvert.DeserializeObject<HBAPIOtherCreditorUpdateResponse>(response.Content);

                    if (objHBAPIOtherCreditorUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPIOtherCreditorUpdateResponse.addresslist)
                        {
                            msSQL = " update agr_mst_tcreditor2address set " +
                          " erp_id='" + entry.Value + "'" +
                          " where creditor2address_gid='" + entry.Key + "' ";

                            mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);

                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
                    }
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception Occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor);
            }

        }
        //Auxillary Functions
        public void LogForAuditHBAPI(string strVal, string type)
        {
            try
            {

                if (type == "OtherCreditor")
                {

                    loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/HyperbridgeAPI/NetsuiteERPLog/" + type + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    if ((!System.IO.Directory.Exists(loglspath)))
                        System.IO.Directory.CreateDirectory(loglspath);
                    if (logFileName == "")

                    {
                        logFileName = "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
                    }
                    loglspath = loglspath + logFileName;
                }

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

        //Auxillary Functions
        public void LogForAuditUpdateHBAPI(string strVal, string type)
        {
            try
            {

                if (type == LoggingTypeHBAPIUpdate_OtherCreditor.OtherCreditor)
                {

                    loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/HyperbridgeAPI/NetsuiteERPLogUpdate/" + type + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    if ((!System.IO.Directory.Exists(loglspath)))
                        System.IO.Directory.CreateDirectory(loglspath);
                    if (logFileName == "")

                    {
                        logFileName = "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
                    }
                    loglspath = loglspath + logFileName;
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(loglspath, true);
                sw.WriteLine(strVal);
                sw.Close();

            }
            catch (Exception ex)
            {
            }
        }

        public string fetchNsAddrStateCode(string value)
        {
            msSQL = "select ns_addr_code from agr_mst_tnsaddrstatemst where addr_state_name = '" + value + "'";
            string ns_state_code = objdbconn.GetExecuteScalar(msSQL);
            return ns_state_code;
        }

        public string fetchNsGSTStateSode(string value)
        {

            msSQL = "select ns_state_code from agr_mst_tnsstatemst where gst_state_name = '" + value + "'";
            string ns_state_code = objdbconn.GetExecuteScalar(msSQL);
            return ns_state_code;

        }

       
    }
}