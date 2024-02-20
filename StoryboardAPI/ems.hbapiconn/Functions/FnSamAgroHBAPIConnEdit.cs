using System.Data.Odbc;
using ems.utilities.Functions;
using System.Configuration;
using System.Net;
using ems.hbapiconn.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Data;
using System;
using System.Collections.Generic;

namespace ems.hbapiconn.Functions
{
    public class FnSamAgroHBAPIConnEdit
    {
        string msSQL, lsinstitution_gid, lscontact_gid;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        FnSamAgroHBAPIConn obj = new FnSamAgroHBAPIConn();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        int mnResult, mnResultContact, mnResultAddress;
        string loglspath = "", logFileName = "";
        string customer_erpid, supplier_erpid, address_erpid, contact_erpid;
        string lsapplication_gid, lscontactpersonfirst_name, lscontactpersonmiddle_name, lscontactpersonlast_name, lsdesignation;
        string mscontactdetailsGID;
        ContactDetails[] arrContactDetails;
        string lsmobile_no, lsemail;
        string lsrelationshipmanager_gid, lserp_status;

        //Buyer Functions
        //General
        public void UpdateBuyerGeneralHBAPI(string application_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
          try
            {

                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerGeneralHBAPI + " for Buyer with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                MdlBuyerGeneralUpdateHBAPI objMdlBuyerGeneralUpdateHBAPI = new MdlBuyerGeneralUpdateHBAPI();

                msSQL = " select customerref_name, vertical_name, constitution_name from agr_mst_tbyronboard" +
                          " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerGeneralUpdateHBAPI.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    objMdlBuyerGeneralUpdateHBAPI.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    objMdlBuyerGeneralUpdateHBAPI.company_name = objODBCDatareader["customerref_name"].ToString();
                }
                objODBCDatareader.Close();

                string BuyerGeneralUpdateHBAPI = JsonConvert.SerializeObject(objMdlBuyerGeneralUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(BuyerGeneralUpdateHBAPI, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select institution_gid, urn from agr_mst_tbyronboard2institution" +
                            " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contact_gid from agr_mst_tbyronboardcontact" +
                " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!string.IsNullOrEmpty(lsinstitution_gid))
                {
                    msSQL = " select urn from agr_mst_tbyronboard2institution" +
                            " where institution_gid = '" + lsinstitution_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                }
                else if (!string.IsNullOrEmpty(lscontact_gid))
                {
                    msSQL = " select urn from agr_mst_tbyronboard2contact" +
                           " where contact_gid = '" + lscontact_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                }

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerGeneral);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", BuyerGeneralUpdateHBAPI);
                request.AddParameter("customer_erpid", customer_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch(Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerGeneralContactHBAPI(string function_gid, string edit_from)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerGeneralContactHBAPI + " for Buyer with Application GID -" + function_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                MdlGeneralContactUpdateHBAPI objMdlGeneralContactUpdateHBAPI = new MdlGeneralContactUpdateHBAPI();

                if (edit_from == UpdateContactHBAPIFrom.Email)
                {
                    msSQL = " select email_address" +
                    " from agr_mst_tbyronboard2email where application2email_gid='" + function_gid + "'";
                    lsemail = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " update agr_mst_tbyronboardcontactdetails set" +
                    " email='" + lsemail + "'" +
                    " where email_gid='" + function_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select byronboardcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                            " from agr_mst_tbyronboardcontactdetails where email_gid='" + function_gid + "'";
                }
                else if (edit_from == UpdateContactHBAPIFrom.MobileNo)
                {
                    msSQL = " select mobile_no" +
                   " from agr_mst_tbyronboard2contactno where application2contact_gid='" + function_gid + "'";
                    lsmobile_no = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " update agr_mst_tbyronboardcontactdetails set" +
                    " mobileno='" + lsmobile_no + "'" +
                    " where mobileno_gid='" + function_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select byronboardcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                            " from agr_mst_tbyronboardcontactdetails where mobileno_gid='" + function_gid + "'";
                }

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlGeneralContactUpdateHBAPI.contactdetails_gid = objODBCDatareader["byronboardcontactdetails_gid"].ToString();
                    objMdlGeneralContactUpdateHBAPI.contactperson_firstname = objODBCDatareader["first_name"].ToString();
                    objMdlGeneralContactUpdateHBAPI.contactperson_middlename = objODBCDatareader["middle_name"].ToString();
                    objMdlGeneralContactUpdateHBAPI.contactperson_lastname = objODBCDatareader["last_name"].ToString();
                    objMdlGeneralContactUpdateHBAPI.designation = objODBCDatareader["designation"].ToString();
                    objMdlGeneralContactUpdateHBAPI.email_address = objODBCDatareader["email"].ToString();
                    objMdlGeneralContactUpdateHBAPI.mobile_no = objODBCDatareader["mobileno"].ToString();
                    contact_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();



                string MdlGeneralContactUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlGeneralContactUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(MdlGeneralContactUpdateHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerGenContact);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlGeneralContactUpdateHBAPIJSON);
                request.AddParameter("contact_erpid", contact_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerGeneralContactAddHBAPI(List<string> mobilenogid_list, List<string> emailgid_list, string application_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerGeneralContactAddHBAPI + " for Buyer with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                lsapplication_gid = application_gid;

                MdlBuyerGeneralContactUpdateAddHBAPI objMdlBuyerGeneralContactUpdateAddHBAPI = new MdlBuyerGeneralContactUpdateAddHBAPI();

                objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral = new ContactPersonDetailsGeneral();
                objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[] { };

                if (mobilenogid_list.Count > 0 || emailgid_list.Count > 0)
                {
                    populateBuyerGeneralContactTableUpdate(mobilenogid_list, emailgid_list, application_gid); //Update details in Contact table
                }

                msSQL = " select institution_gid from agr_mst_tbyronboard2institution" +
                " where application_gid = '" + application_gid + "' and stakeholder_type = 'Applicant'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contact_gid from agr_mst_tbyronboardcontact" +
                " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!string.IsNullOrEmpty(lsinstitution_gid))
                {
                    msSQL = " select urn from agr_mst_tbyronboard2institution" +
                            " where institution_gid = '" + lsinstitution_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                }
                else if (!string.IsNullOrEmpty(lscontact_gid))
                {
                    msSQL = " select urn from agr_mst_tbyronboard2contact" +
                           " where contact_gid = '" + lscontact_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                }

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select byronboardcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                      " from agr_mst_tbyronboardcontactdetails where application_gid='" + lsapplication_gid + "' and (erp_id is null or erp_id = '')";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if(dt_datatable.Rows.Count > 0)
                {
                    objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                    for (int i = 0; i < objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist.Length; i++)
                    {
                        objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[i] = new ContactPersonData();
                    }
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int contactpersonind = 0;
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardcontactdetails_gid"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                            contactpersonind++;
                        }
                    }
                    dt_datatable.Dispose();


                    string MdlBuyerInstitutionContactUpdateAddHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerGeneralContactUpdateAddHBAPI);

                    LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(MdlBuyerInstitutionContactUpdateAddHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerGenContactAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlBuyerInstitutionContactUpdateAddHBAPIJSON);
                    request.AddParameter("customer_erpid", customer_erpid);


                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                    objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                    if (objHBAPICustomerUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerUpdateResponse.contactlist_generaldetails)
                        {
                            msSQL = " update agr_mst_tbyronboardcontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where byronboardcontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                    }
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not needed", LoggingTypeHBAPIUpdate.Buyer);
                }
                
            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerGeneralContactBasicHBAPI(string application_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerGeneralContactBasicHBAPI + " for Buyer with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select institution_gid from agr_mst_tbyronboard2institution" +
               " where application_gid = '" + application_gid + "' and stakeholder_type = 'Applicant'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contact_gid from agr_mst_tbyronboardcontact" +
                " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!string.IsNullOrEmpty(lsinstitution_gid))
                {
                    msSQL = " select urn from agr_mst_tbyronboard2institution" +
                            " where institution_gid = '" + lsinstitution_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                }
                else if (!string.IsNullOrEmpty(lscontact_gid))
                {
                    msSQL = " select urn from agr_mst_tbyronboard2contact" +
                           " where contact_gid = '" + lscontact_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                }


                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);


                msSQL = " select contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_type" +
                        " from agr_mst_tbyronboard where application_gid='" + application_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscontactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                    lscontactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                    lscontactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                    lsdesignation = objODBCDatareader["designation_type"].ToString();     
                }
                objODBCDatareader.Close();

                msSQL = " update agr_mst_tbyronboardcontactdetails set" +
                    " first_name='" + lscontactpersonfirst_name + "'," +
                    " middle_name='" + lscontactpersonmiddle_name + "'," +
                    " last_name='" + lscontactpersonlast_name + "'," +
                    " designation='" + lsdesignation + "'" +
                    " where application_gid='" + application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if(mnResult == 1)
                    LogForAuditUpdateHBAPI("Basic Details Updated in Buyer General Contacts table", LoggingTypeHBAPIUpdate.Buyer);



                MdlBuyerGeneralContactUpdateBasicHBAPI objMdlBuyerGeneralContactUpdateBasicHBAPI = new MdlBuyerGeneralContactUpdateBasicHBAPI();

                objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral = new ContactPersonDetailsGeneral();
                objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[] { };

                msSQL = " select byronboardcontactdetails_gid,first_name,middle_name,last_name,designation" +
                      " from agr_mst_tbyronboardcontactdetails where application_gid='" + application_gid + "' and erp_id is not null";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist.Length; i++)
                {
                    objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardcontactdetails_gid"].ToString();
                        objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();



                string MdlBuyerGeneralContactUpdateBasicHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerGeneralContactUpdateBasicHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(MdlBuyerGeneralContactUpdateBasicHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerGenContactBasic);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlBuyerGeneralContactUpdateBasicHBAPIJSON);
                request.AddParameter("customer_erpid", customer_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerTagStatusHBAPI(string application_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {

                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerTagStatusHBAPI + " for Buyer with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                MdlBuyerTagStatusUpdateHBAPI objMdlBuyerTagStatusUpdateHBAPI = new MdlBuyerTagStatusUpdateHBAPI();

                msSQL = " select lgltag_status from agr_mst_tbyronboard" +
                          " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerTagStatusUpdateHBAPI.lgltag_status = objODBCDatareader["lgltag_status"].ToString();
                }
                objODBCDatareader.Close();

                string BuyerTagStatusUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerTagStatusUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(BuyerTagStatusUpdateHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select institution_gid, urn from agr_mst_tbyronboard2institution" +
                            " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contact_gid from agr_mst_tbyronboardcontact" +
                " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!string.IsNullOrEmpty(lsinstitution_gid))
                {
                    msSQL = " select urn from agr_mst_tbyronboard2institution" +
                            " where institution_gid = '" + lsinstitution_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                }
                else if (!string.IsNullOrEmpty(lscontact_gid))
                {
                    msSQL = " select urn from agr_mst_tbyronboard2contact" +
                           " where contact_gid = '" + lscontact_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                }

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerStatus);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", BuyerTagStatusUpdateHBAPIJSON);
                request.AddParameter("customer_erpid", customer_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerRMHBAPI(string application_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {

                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerRMHBAPI + " for Buyer with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                MdlBuyerRMUpdateHBAPI objMdlBuyerRMUpdateHBAPI = new MdlBuyerRMUpdateHBAPI();

                msSQL = " select created_by from agr_mst_tbyronboard where application_gid = '" + application_gid + "'";
                lsrelationshipmanager_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select erp_status from hrm_mst_temployee where employee_gid = '" + lsrelationshipmanager_gid + "'";
                lserp_status = objdbconn.GetExecuteScalar(msSQL);

                if (lserp_status == "No")
                {
                    LogForAuditUpdateHBAPI("RM to whom this application is to be transferred hasn't been posted to ERP yet. Since he can't be assigned as Sales Rep in ERP, updating RM transfer in ERP failed.", LoggingTypeHBAPIUpdate.Buyer);
                    return;
                }
               

                msSQL = " select application_no,created_by from agr_mst_tbyronboard where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerRMUpdateHBAPI.rm_custopediaid = objODBCDatareader["created_by"].ToString();
                    objMdlBuyerRMUpdateHBAPI.application_no = objODBCDatareader["application_no"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select erp_id from hrm_mst_temployee where employee_gid = '" + objMdlBuyerRMUpdateHBAPI.rm_custopediaid + "'";
                objMdlBuyerRMUpdateHBAPI.rm_erpid = objdbconn.GetExecuteScalar(msSQL);

                string BuyerRMUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerRMUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(BuyerRMUpdateHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);
               

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerSalesRep);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", BuyerRMUpdateHBAPIJSON);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                    
                    if(!String.IsNullOrEmpty(objHBAPICustomerUpdateResponse.message))
                        LogForAuditUpdateHBAPI("Error message - " + objHBAPICustomerUpdateResponse.message, LoggingTypeHBAPIUpdate.Buyer);

                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        //Institution
        public void UpdateBuyerInstitutionHBAPI(string institution_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerInstitutionHBAPI + " for Buyer with Institution GID -" + institution_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                MdlBuyerInstitutionUpdateHBAPI objMdlBuyerInstitutionUpdateHBAPI = new MdlBuyerInstitutionUpdateHBAPI();

                msSQL = " select officialemail_address,official_telephoneno,company_name,companypan_no,companytype_gid,tan_number,incometax_returnsstatus," +
                        " lastyear_turnover from agr_mst_tbyronboard2institution" +
                        " where institution_gid = '" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerInstitutionUpdateHBAPI.officialemail_address = objODBCDatareader["officialemail_address"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.official_telephoneno = objODBCDatareader["official_telephoneno"].ToString();
                    //objMdlBuyerInstitutionUpdateHBAPI.company_name = objODBCDatareader["company_name"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.companypan_no = objODBCDatareader["companypan_no"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.companytype_name = obj.fetchCompanyTypeName(objODBCDatareader["companytype_gid"].ToString());
                    objMdlBuyerInstitutionUpdateHBAPI.tan_number = objODBCDatareader["tan_number"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.incometax_returnsstatus = objODBCDatareader["incometax_returnsstatus"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.lastyear_turnover = objODBCDatareader["lastyear_turnover"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select bankaccount_number,bankaccount_name,ifsc_code,bank_name,micr_code,bank_address,branch_name" +
                       " from agr_mst_tbyronboardinstitution2bankdtl where institution_gid='" + institution_gid + "' and primary_status = 'Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerInstitutionUpdateHBAPI.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.accountholder_name = objODBCDatareader["bankaccount_name"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.bank_name = objODBCDatareader["bank_name"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.micr_code = objODBCDatareader["micr_code"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.bank_address = objODBCDatareader["bank_address"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.branch_name = objODBCDatareader["branch_name"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select gst_no from agr_mst_tbyronboardinstitution2branch where headoffice_status = 'Yes' and institution_gid = '" + institution_gid + "'";
                objMdlBuyerInstitutionUpdateHBAPI.primary_gst = objdbconn.GetExecuteScalar(msSQL);

                string BuyerInstitutionUpdateHBAPI = JsonConvert.SerializeObject(objMdlBuyerInstitutionUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(BuyerInstitutionUpdateHBAPI, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select urn from agr_mst_tbyronboard2institution" +
                            " where institution_gid = '" + institution_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerInstitution);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", BuyerInstitutionUpdateHBAPI);
                request.AddParameter("customer_erpid", customer_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerInstitutionAddressHBAPI(string institution2address_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerInstitutionAddressHBAPI + " for Buyer with Address GID -" + institution2address_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                MdlInstitutionAddressUpdateHBAPI objMdlInstitutionAddressUpdateHBAPI = new MdlInstitutionAddressUpdateHBAPI();

                msSQL = " select institution_gid from agr_mst_tbyronboardinstitution2address" +
               " where institution2address_gid = '" + institution2address_gid + "'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select urn from agr_mst_tbyronboard2institution" +
               " where institution_gid = '" + lsinstitution_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select institution2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude,erp_id" +
                        " from agr_mst_tbyronboardinstitution2address where institution2address_gid='" + institution2address_gid + "'";  
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlInstitutionAddressUpdateHBAPI.externalid = objODBCDatareader["institution2address_gid"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.addressline1 = objODBCDatareader["addressline1"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.addressline2 = objODBCDatareader["addressline2"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.city = objODBCDatareader["city"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.state = obj.fetchNsAddrStateCode(objODBCDatareader["state"].ToString());
                    objMdlInstitutionAddressUpdateHBAPI.postal_code = objODBCDatareader["postal_code"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.latitude = objODBCDatareader["latitude"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.longitude = objODBCDatareader["longitude"].ToString();
                    address_erpid = objODBCDatareader["erp_id"].ToString();

                }
                objODBCDatareader.Close();

                LogForAuditUpdateHBAPI("Address ERP ID obtained - " + address_erpid, LoggingTypeHBAPIUpdate.Buyer);

                string MdlInstitutionAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlInstitutionAddressUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(MdlInstitutionAddressUpdateHBAPI, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerAddress);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlInstitutionAddressUpdateHBAPI);
                request.AddParameter("address_erpid", address_erpid);
                request.AddParameter("customer_erpid", customer_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerInstitutionAddressAddHBAPI(string institution_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerInstitutionAddressHBAPI + " for Buyer with Institution GID -" + institution_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                MdlInstitutionAddressUpdateAddHBAPI objMdlInstitutionAddressUpdateAddHBAPI = new MdlInstitutionAddressUpdateAddHBAPI();

                msSQL = " select urn from agr_mst_tbyronboard2institution" +
                " where institution_gid = '" + institution_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);


                msSQL = " select institution2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude" +
                        " from agr_mst_tbyronboardinstitution2address where (erp_id is null or erp_id = '') and institution_gid = '" + institution_gid + "'";
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
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].state = obj.fetchNsAddrStateCode(dr_datarow["state"].ToString());
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["institution2address_gid"].ToString();
                            addressind++;
                        }
                    }
                    dt_datatable.Dispose();

                    string MdlInstitutionAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlInstitutionAddressUpdateAddHBAPI);

                    LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(MdlInstitutionAddressUpdateHBAPI, LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerAddressAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlInstitutionAddressUpdateHBAPI);
                    request.AddParameter("customer_erpid", customer_erpid);


                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                    objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                    if (objHBAPICustomerUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerUpdateResponse.addresslist)
                        {
                            msSQL = " update agr_mst_tbyronboardinstitution2address set " +
                          " erp_id='" + entry.Value + "'" +
                          " where institution2address_gid='" + entry.Key + "' ";

                            mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);

                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                    }
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception Occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }


        public void UpdateBuyerInstitutionContactHBAPI(string function_gid, string edit_from)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerInstitutionContactHBAPI + " for Buyer with Function GID -" + function_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                MdlInstitutionContactUpdateHBAPI objMdlInstitutionContactUpdateHBAPI = new MdlInstitutionContactUpdateHBAPI();

                if(edit_from == UpdateContactHBAPIFrom.Email)
                {
                    msSQL = " select institution_gid from agr_mst_tbyronboardinstitution2email" +
                        " where institution2email_gid = '" + function_gid + "'";
                }
                else if(edit_from == UpdateContactHBAPIFrom.MobileNo)
                {
                    msSQL = " select institution_gid from agr_mst_tbyronboardinstitution2mobileno" +
                        " where institution2mobileno_gid = '" + function_gid + "'";
                }
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Institution GID obtained - " + lsinstitution_gid, LoggingTypeHBAPIUpdate.Buyer);

                if (edit_from == UpdateContactHBAPIFrom.Email)
                {
                    msSQL = " select email_address" +
                   " from agr_mst_tbyronboardinstitution2email where institution2email_gid='" + function_gid + "'";
                    lsemail = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " update agr_mst_tbyronboardinstitutioncontactdetails set" +
                    " email='" + lsemail + "'" +
                    " where email_gid='" + function_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select byronboardinstitutioncontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                      " from agr_mst_tbyronboardinstitutioncontactdetails where email_gid='" + function_gid + "'";
                }
                else if (edit_from == UpdateContactHBAPIFrom.MobileNo)
                {
                    msSQL = " select mobile_no" +
                   " from agr_mst_tbyronboardinstitution2mobileno where institution2mobileno_gid='" + function_gid + "'";
                    lsmobile_no = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " update agr_mst_tbyronboardinstitutioncontactdetails set" +
                    " mobileno='" + lsmobile_no + "'" +
                    " where mobileno_gid='" + function_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select byronboardinstitutioncontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                       " from agr_mst_tbyronboardinstitutioncontactdetails where mobileno_gid='" + function_gid + "'";
                }

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlInstitutionContactUpdateHBAPI.contactdetails_gid = objODBCDatareader["byronboardinstitutioncontactdetails_gid"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.contactperson_firstname = objODBCDatareader["first_name"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.contactperson_middlename = objODBCDatareader["middle_name"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.contactperson_lastname = objODBCDatareader["last_name"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.designation = objODBCDatareader["designation"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.email_address = objODBCDatareader["email"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.mobile_no = objODBCDatareader["mobileno"].ToString();
                    contact_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();



                string MdlInstitutionContactUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlInstitutionContactUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(MdlInstitutionContactUpdateHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerContact);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlInstitutionContactUpdateHBAPIJSON);
                request.AddParameter("contact_erpid", contact_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was Successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not Successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception Occurred in Update- " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerInstitutionContactAddHBAPI(List<string> mobilenogid_list, List<string> emailgid_list, string institution_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerInstitutionContactAddHBAPI + " for Buyer with Institution GID -" + institution_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                MdlBuyerInstitutionContactUpdateAddHBAPI objMdlBuyerInstitutionContactUpdateAddHBAPI = new MdlBuyerInstitutionContactUpdateAddHBAPI();

                objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany = new ContactPersonDetailsCompany();
                objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[] { };

                if(mobilenogid_list.Count > 0 || emailgid_list.Count > 0)
                {
                    populateBuyerInstitutionContactTableUpdate(mobilenogid_list, emailgid_list, institution_gid); //Update details in Contact table
                }
                

                msSQL = " select urn from agr_mst_tbyronboard2institution" +
                " where institution_gid = '" + institution_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select byronboardinstitutioncontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                      " from agr_mst_tbyronboardinstitutioncontactdetails where institution_gid='" + institution_gid + "' and (erp_id is null or erp_id = '')";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if(dt_datatable.Rows.Count > 0)
                {
                    objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                    for (int i = 0; i < objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist.Length; i++)
                    {
                        objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[i] = new ContactPersonData();
                    }
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int contactpersonind = 0;
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardinstitutioncontactdetails_gid"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                            contactpersonind++;
                        }
                    }
                    dt_datatable.Dispose();


                    string MdlBuyerInstitutionContactUpdateAddHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerInstitutionContactUpdateAddHBAPI);

                    LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(MdlBuyerInstitutionContactUpdateAddHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerContactAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlBuyerInstitutionContactUpdateAddHBAPIJSON);
                    request.AddParameter("customer_erpid", customer_erpid);


                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                    objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                    if (objHBAPICustomerUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerUpdateResponse.contactlist_company)
                        {
                            msSQL = " update agr_mst_tbyronboardinstitutioncontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where byronboardinstitutioncontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was Successful", LoggingTypeHBAPIUpdate.Buyer);
                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not Successful", LoggingTypeHBAPIUpdate.Buyer);
                    }
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not needed", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occurred in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerInstitutionContactBasicHBAPI(string institution_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerInstitutionContactBasicHBAPI + " for Buyer with Institution GID -" + institution_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select urn from agr_mst_tbyronboard2institution" +
               " where institution_gid = '" + institution_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);


                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);


                msSQL = " select contactperson_firstname,contactperson_middlename,contactperson_lastname,designation" +
                        " from agr_mst_tbyronboard2institution where institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lscontactpersonfirst_name = objODBCDatareader["contactperson_firstname"].ToString();
                    lscontactpersonmiddle_name = objODBCDatareader["contactperson_middlename"].ToString();
                    lscontactpersonlast_name = objODBCDatareader["contactperson_lastname"].ToString();
                    lsdesignation = objODBCDatareader["designation"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " update agr_mst_tbyronboardinstitutioncontactdetails set" +
                    " first_name='" + lscontactpersonfirst_name + "'," +
                    " middle_name='" + lscontactpersonmiddle_name + "'," +
                    " last_name='" + lscontactpersonlast_name + "'," +
                    " designation='" + lsdesignation + "'" +
                    " where institution_gid='" + institution_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                    LogForAuditUpdateHBAPI("Basic Details Updated in Buyer Institution Contacts table", LoggingTypeHBAPIUpdate.Buyer);



                MdlBuyerInstitutionContactUpdateBasicHBAPI objMdlBuyerInstitutionContactUpdateBasicHBAPI = new MdlBuyerInstitutionContactUpdateBasicHBAPI();

                objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany = new ContactPersonDetailsCompany();
                objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[] { };

                msSQL = " select byronboardinstitutioncontactdetails_gid,first_name,middle_name,last_name,designation" +
                      " from agr_mst_tbyronboardinstitutioncontactdetails where institution_gid='" + institution_gid + "' and erp_id is not null";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist.Length; i++)
                {
                    objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardinstitutioncontactdetails_gid"].ToString();
                        objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();



                string MdlBuyerInstitutionContactUpdateBasicHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerInstitutionContactUpdateBasicHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(MdlBuyerInstitutionContactUpdateBasicHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerContactBasic);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlBuyerInstitutionContactUpdateBasicHBAPIJSON);
                request.AddParameter("customer_erpid", customer_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        //Individual
        public void UpdateBuyerIndividualHBAPI(string contact_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerIndividualHBAPI + " for Buyer with Contact GID -" + contact_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                MdlBuyerUpdateIndividualHBAPI objMdlBuyerIndividualUpdateHBAPI = new MdlBuyerUpdateIndividualHBAPI();

                msSQL = " select first_name,middle_name,last_name,aadhar_no" +
                        " from agr_mst_tbyronboardcontact where contact_gid='" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerIndividualUpdateHBAPI.first_name = objODBCDatareader["first_name"].ToString();
                    objMdlBuyerIndividualUpdateHBAPI.middle_name = objODBCDatareader["middle_name"].ToString();
                    objMdlBuyerIndividualUpdateHBAPI.last_name = objODBCDatareader["last_name"].ToString();
                    objMdlBuyerIndividualUpdateHBAPI.aadhar_no = objODBCDatareader["aadhar_no"].ToString();

                }
                objODBCDatareader.Close();

                string BuyerInstitutionUpdateHBAPI = JsonConvert.SerializeObject(objMdlBuyerIndividualUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Individual Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(BuyerInstitutionUpdateHBAPI, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select urn from agr_mst_tbyronboardcontact" +
                " where contact_gid = '" + contact_gid + "' and stakeholder_type='Applicant'";

                customer_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);


                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerIndividual);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", BuyerInstitutionUpdateHBAPI);
                request.AddParameter("customer_erpid", customer_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerIndividualAddressHBAPI(string contact2address_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerIndividualAddressHBAPI + " for Buyer with Address GID -" + contact2address_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);
                MdlInstitutionAddressUpdateHBAPI objMdlIndividualAddressUpdateHBAPI = new MdlInstitutionAddressUpdateHBAPI();

                msSQL = " select contact_gid from agr_mst_tbyronboardcontact2address" +
               " where contact2address_gid = '" + contact2address_gid + "'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select urn from agr_mst_tbyronboardcontact" +
               " where contact_gid = '" + lscontact_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select contact2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude,erp_id" +
                        " from agr_mst_tbyronboardcontact2address where institution2address_gid='" + contact2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlIndividualAddressUpdateHBAPI.externalid = objODBCDatareader["contact2address_gid"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.addressline1 = objODBCDatareader["addressline1"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.addressline2 = objODBCDatareader["addressline2"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.city = objODBCDatareader["city"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.state = obj.fetchNsAddrStateCode(objODBCDatareader["state"].ToString());
                    objMdlIndividualAddressUpdateHBAPI.postal_code = objODBCDatareader["postal_code"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.latitude = objODBCDatareader["latitude"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.longitude = objODBCDatareader["longitude"].ToString();
                    address_erpid = objODBCDatareader["erp_id"].ToString();

                }
                objODBCDatareader.Close();

                LogForAuditUpdateHBAPI("Address ERP ID obtained - " + address_erpid, LoggingTypeHBAPIUpdate.Buyer);

                string MdlIndividualAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlIndividualAddressUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(MdlIndividualAddressUpdateHBAPI, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerAddress);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlIndividualAddressUpdateHBAPI);
                request.AddParameter("address_erpid", address_erpid);
                request.AddParameter("customer_erpid", customer_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerIndividualAddressAddHBAPI(string contact_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerIndividualAddressAddHBAPI + " for Buyer with Institution GID -" + contact_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);
                MdlInstitutionAddressUpdateAddHBAPI objMdlIndividualAddressUpdateAddHBAPI = new MdlInstitutionAddressUpdateAddHBAPI();

                msSQL = " select urn from agr_mst_tbyronboardcontact" +
                " where contact_gid = '" + contact_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);


                msSQL = " select contact2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude" +
                        " from agr_mst_tbyronboardcontact2address where (erp_id is null or erp_id = '') and contact_gid = '" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count > 0)
                {
                    objMdlIndividualAddressUpdateAddHBAPI.addressDetails = new AddressDetails();

                    objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist = new AddressData[dt_datatable.Rows.Count];

                    for (int i = 0; i < objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist.Length; i++)
                    {
                        objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[i] = new AddressData();

                    }
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int addressind = 0;
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addresstype_name = dr_datarow["addresstype_name"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addressline1 = dr_datarow["addressline1"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addressline2 = dr_datarow["addressline2"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].city = dr_datarow["city"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].state = obj.fetchNsAddrStateCode(dr_datarow["state"].ToString());
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["contact2address_gid"].ToString();
                            addressind++;
                        }
                    }
                    dt_datatable.Dispose();




                    string MdlIndividualAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlIndividualAddressUpdateAddHBAPI);

                    LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(MdlIndividualAddressUpdateHBAPI, LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerAddressAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlIndividualAddressUpdateHBAPI);
                    request.AddParameter("customer_erpid", customer_erpid);


                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                    objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                    if (objHBAPICustomerUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerUpdateResponse.addresslist)
                        {
                            msSQL = " update agr_mst_tbyronboardcontact2address set " +
                          " erp_id='" + entry.Value + "'" +
                          " where contact2address_gid='" + entry.Key + "' ";

                            mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);

                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                    }
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception Occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerIndividualContactHBAPI(string function_gid, string edit_from)
        {
            HBAPIIndividualCustomerUpdateResponse objHBAPIIndividualCustomerUpdateResponse = new HBAPIIndividualCustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerIndividualContactHBAPI + " for Buyer with Function GID -" + function_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);
                MdlIndividualContactUpdateHBAPI objMdlIndividualContactUpdateHBAPI = new MdlIndividualContactUpdateHBAPI();

                if (edit_from == UpdateContactHBAPIFrom.Email)
                {
                    msSQL = " select contact_gid from agr_mst_tbyronboardcontact2email" +
                        " where contact2email_gid = '" + function_gid + "'";
                }
                else if (edit_from == UpdateContactHBAPIFrom.MobileNo)
                {
                    msSQL = " select contact_gid from agr_mst_tbyronboardcontact2mobileno" +
                        " where contact2mobileno_gid = '" + function_gid + "'";
                }
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);
                LogForAuditUpdateHBAPI("Contact GID obtained - " + lscontact_gid, LoggingTypeHBAPIUpdate.Buyer);

                if (edit_from == UpdateContactHBAPIFrom.Email)
                {
                    msSQL = " select email_address" +
                  " from agr_mst_tbyronboardcontact2email where contact2email_gid='" + function_gid + "'";
                    lsemail = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " update agr_mst_tbyronboardcontactdetails set" +
                    " email='" + lsemail + "'" +
                    " where email_gid='" + function_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select byronboardindividualcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                      " from agr_mst_tbyronboardindividualcontactdetails where email_gid='" + function_gid + "'";
                }
                else if (edit_from == UpdateContactHBAPIFrom.MobileNo)
                {
                    msSQL = " select mobile_no" +
                   " from agr_mst_tbyronboardcontact2mobileno where contact2mobileno_gid='" + function_gid + "'";
                    lsmobile_no = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " update agr_mst_tbyronboardcontactdetails set" +
                    " mobileno='" + lsmobile_no + "'" +
                    " where mobileno_gid='" + function_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select byronboardindividualcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                       " from agr_mst_tbyronboardindividualcontactdetails where mobileno_gid='" + function_gid + "'";
                }

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlIndividualContactUpdateHBAPI.contactdetails_gid = objODBCDatareader["byronboardindividualcontactdetails_gid"].ToString();
                    objMdlIndividualContactUpdateHBAPI.contactperson_firstname = objODBCDatareader["first_name"].ToString();
                    objMdlIndividualContactUpdateHBAPI.contactperson_middlename = objODBCDatareader["middle_name"].ToString();
                    objMdlIndividualContactUpdateHBAPI.contactperson_lastname = objODBCDatareader["last_name"].ToString();
                    objMdlIndividualContactUpdateHBAPI.designation = objODBCDatareader["designation"].ToString();
                    objMdlIndividualContactUpdateHBAPI.email_address = objODBCDatareader["email"].ToString();
                    objMdlIndividualContactUpdateHBAPI.mobile_no = objODBCDatareader["mobileno"].ToString();
                    contact_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();



                string MdlIndividualContactUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlIndividualContactUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(MdlIndividualContactUpdateHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateCustomerContact);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlIndividualContactUpdateHBAPIJSON);
                request.AddParameter("contact_erpid", contact_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPIIndividualCustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPIIndividualCustomerUpdateResponse>(response.Content);

                if (objHBAPIIndividualCustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was Successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not Successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception Occurred in Update- " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerIndividualContactAddHBAPI(List<string> mobilenogid_list, List<string> emailgid_list, string contact_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerIndividualContactAddHBAPI + " for Buyer with Contact GID -" + contact_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);
                MdlBuyerIndividualContactUpdateAddHBAPI objMdlBuyerIndividualContactUpdateAddHBAPI = new MdlBuyerIndividualContactUpdateAddHBAPI();

                objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual = new ContactPersonDetailsIndividual();
                objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist = new ContactPersonData[] { };

                if(mobilenogid_list.Count > 0 || emailgid_list.Count > 0)
                {
                    populateBuyerIndividualContactTableUpdate(mobilenogid_list, emailgid_list, contact_gid); //Update details in Contact table
                }

                msSQL = " select urn from agr_mst_tbyronboardcontact" +
                " where contact_gid = '" + contact_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select byronboardindividualcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                      " from agr_mst_tbyronboardindividualcontactdetails where contact_gid='" + lscontact_gid + "' and (erp_id is null or erp_id = '')";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if(dt_datatable.Rows.Count > 0)
                {
                    objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                    for (int i = 0; i < objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist.Length; i++)
                    {
                        objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist[i] = new ContactPersonData();
                    }
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int contactpersonind = 0;
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardinstitutioncontactdetails_gid"].ToString();
                            objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                            objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                            objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                            objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                            objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                            objMdlBuyerIndividualContactUpdateAddHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                            contactpersonind++;
                        }
                    }
                    dt_datatable.Dispose();


                    string MdlBuyerInstitutionContactUpdateAddHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerIndividualContactUpdateAddHBAPI);

                    LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(MdlBuyerInstitutionContactUpdateAddHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateIndividualCustomerContactAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlBuyerInstitutionContactUpdateAddHBAPIJSON);
                    request.AddParameter("customer_erpid", customer_erpid);

                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                    objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                    if (objHBAPICustomerUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerUpdateResponse.contactlist_individual)
                        {
                            msSQL = " update agr_mst_tbyronboardindividualcontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where byronboardindividualcontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was Successful", LoggingTypeHBAPIUpdate.Buyer);
                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not Successful", LoggingTypeHBAPIUpdate.Buyer);
                    }
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not needed", LoggingTypeHBAPIUpdate.Buyer);
                }  

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occurred in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        public void UpdateBuyerIndividualContactBasicHBAPI(string contact_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateBuyerInstitutionContactBasicHBAPI + " for Buyer with Contact GID -" + contact_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Buyer);

                msSQL = " select urn from agr_mst_tbyronboardcontact" +
               " where contact_gid = '" + contact_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);


                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Buyer);


                msSQL = " select first_name,middle_name,last_name,designation_name" +
                        " from agr_mst_tbyronboardcontact where contact_gid='" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lscontactpersonfirst_name = objODBCDatareader["contactperson_firstname"].ToString();
                    lscontactpersonmiddle_name = objODBCDatareader["contactperson_middlename"].ToString();
                    lscontactpersonlast_name = objODBCDatareader["contactperson_lastname"].ToString();
                    lsdesignation = objODBCDatareader["designation"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " update agr_mst_tbyronboardindividualcontactdetails set" +
                    " first_name='" + lscontactpersonfirst_name + "'," +
                    " middle_name='" + lscontactpersonmiddle_name + "'," +
                    " last_name='" + lscontactpersonlast_name + "'," +
                    " designation='" + lsdesignation + "'" +
                    " where contact_gid='" + contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                    LogForAuditUpdateHBAPI("Basic Details Updated in Buyer Institution Contacts table", LoggingTypeHBAPIUpdate.Buyer);



                MdlBuyerIndividualContactUpdateBasicHBAPI objMdlBuyerIndividualContactUpdateBasicHBAPI = new MdlBuyerIndividualContactUpdateBasicHBAPI();

                objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual = new ContactPersonDetailsIndividual();
                objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist = new ContactPersonData[] { };

                msSQL = " select byronboardindividualcontactdetails_gid,first_name,middle_name,last_name,designation" +
                      " from agr_mst_tbyronboardindividualcontactdetails where contact_gid='" + contact_gid + "' and erp_id is not null";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist.Length; i++)
                {
                    objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardindividualcontactdetails_gid"].ToString();
                        objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();



                string MdlBuyerIndividualContactUpdateBasicHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerIndividualContactUpdateBasicHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(MdlBuyerIndividualContactUpdateBasicHBAPIJSON, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Buyer);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateIndividualCustomerContactBasic);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlBuyerIndividualContactUpdateBasicHBAPIJSON);
                request.AddParameter("customer_erpid", customer_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Buyer);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Buyer);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Buyer);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Buyer);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Buyer);
            }

        }

        //Supplier Functions
        //General
        public void UpdateSupplierGeneralHBAPI(string application_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierGeneralHBAPI + " for Supplier with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                MdlBuyerGeneralUpdateHBAPI objMdlSuprGeneralUpdateHBAPI = new MdlBuyerGeneralUpdateHBAPI();

                msSQL = " select customerref_name, vertical_name, constitution_name from agr_mst_tsupronboard" +
                          " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlSuprGeneralUpdateHBAPI.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    objMdlSuprGeneralUpdateHBAPI.company_name = objODBCDatareader["customerref_name"].ToString();
                    objMdlSuprGeneralUpdateHBAPI.constitution_name = objODBCDatareader["constitution_name"].ToString();
                }
                objODBCDatareader.Close();

                string SupplierGeneralUpdateHBAPI = JsonConvert.SerializeObject(objMdlSuprGeneralUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(SupplierGeneralUpdateHBAPI, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select institution_gid, urn from agr_mst_tsupronboard2institution" +
                            " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contact_gid from agr_mst_tsupronboardcontact" +
                " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!string.IsNullOrEmpty(lsinstitution_gid))
                {
                    msSQL = " select urn from agr_mst_tsupronboard2institution" +
                            " where institution_gid = '" + lsinstitution_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                }
                else if (!string.IsNullOrEmpty(lscontact_gid))
                {
                    msSQL = " select urn from agr_mst_tsupronboardcontact" +
                           " where contact_gid = '" + lscontact_gid + "'";
                    customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                }
                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Supplier);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierGeneral);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", SupplierGeneralUpdateHBAPI);
                request.AddParameter("customer_erpid", customer_erpid);

                IRestResponse response = client.Execute(request);
                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);



                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }

        public void UpdateSupplierGeneralContactHBAPI(string function_gid, string edit_from)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                MdlGeneralContactUpdateHBAPI objMdlGeneralContactUpdateHBAPI = new MdlGeneralContactUpdateHBAPI();
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierGeneralContactHBAPI + " for Supplier with Application GID -" + function_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);

                if (edit_from == UpdateContactHBAPIFrom.Email)
                {
                    msSQL = " select email_address from agr_mst_tsupronboard2email" +
                        " where application2email_gid = '" + function_gid + "'";
                    lsemail = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " update agr_mst_tsupronboardcontactdetails set" +
                    " email='" + lsemail + "'" +
                    " where email_gid='" + function_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select supronboardcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                            " from agr_mst_tsupronboardcontactdetails where email_gid='" + function_gid + "'";

                }
                else if (edit_from == UpdateContactHBAPIFrom.MobileNo)
                {
                    msSQL = " select mobile_no from agr_mst_tsupronboard2contactno" +
                        " where application2contact_gid = '" + function_gid + "'";
                    lsmobile_no = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " update agr_mst_tsupronboardcontactdetails set" +
                    " mobileno='" + lsmobile_no + "'" +
                    " where mobileno_gid='" + function_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select supronboardcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                            " from agr_mst_tsupronboardcontactdetails where mobileno_gid='" + function_gid + "'";

                }
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlGeneralContactUpdateHBAPI.contactdetails_gid = objODBCDatareader["supronboardcontactdetails_gid"].ToString();
                    objMdlGeneralContactUpdateHBAPI.contactperson_firstname = objODBCDatareader["first_name"].ToString();
                    objMdlGeneralContactUpdateHBAPI.contactperson_middlename = objODBCDatareader["middle_name"].ToString();
                    objMdlGeneralContactUpdateHBAPI.contactperson_lastname = objODBCDatareader["last_name"].ToString();
                    objMdlGeneralContactUpdateHBAPI.designation = objODBCDatareader["designation"].ToString();
                    objMdlGeneralContactUpdateHBAPI.email_address = objODBCDatareader["email"].ToString();
                    objMdlGeneralContactUpdateHBAPI.mobile_no = objODBCDatareader["mobileno"].ToString();
                    contact_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();



                string MdlGeneralContactUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlGeneralContactUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(MdlGeneralContactUpdateHBAPIJSON, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);


                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierGenContact);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlGeneralContactUpdateHBAPIJSON);
                request.AddParameter("contact_erpid", contact_erpid);


                IRestResponse response = client.Execute(request);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }

        public void UpdateSupplierGeneralContactAddHBAPI(List<string> mobilenogid_list, List<string> emailgid_list, string application_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                lsapplication_gid = application_gid;
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierGeneralContactAddHBAPI + " for Supplier with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                MdlBuyerGeneralContactUpdateAddHBAPI objMdlBuyerGeneralContactUpdateAddHBAPI = new MdlBuyerGeneralContactUpdateAddHBAPI();

                objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral = new ContactPersonDetailsGeneral();
                objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[] { };

                if(mobilenogid_list.Count > 0 || emailgid_list.Count > 0)
                {
                    populateSupplierGeneralContactTableUpdate(mobilenogid_list, emailgid_list, application_gid); //Update details in Contact table
                }
                

                msSQL = " select institution_gid from agr_mst_tsupronboard2institution" +
                        " where application_gid = '" + application_gid + "' and stakeholder_type = 'Applicant'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contact_gid from agr_mst_tsupronboardcontact" +
               " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!string.IsNullOrEmpty(lsinstitution_gid))
                {
                    msSQL = " select urn from agr_mst_tsupronboard2institution" +
                            " where institution_gid = '" + lsinstitution_gid + "'";
                    supplier_erpid = objdbconn.GetExecuteScalar(msSQL);
                }
                else if (!string.IsNullOrEmpty(lscontact_gid))
                {
                    msSQL = " select urn from agr_mst_tsupronboard2contact" +
                           " where contact_gid = '" + lscontact_gid + "'";
                    supplier_erpid = objdbconn.GetExecuteScalar(msSQL);
                }

                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + supplier_erpid, LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select supronboardcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                      " from agr_mst_tsupronboardcontactdetails where application_gid='" + lsapplication_gid + "' and (erp_id is null or erp_id = '')";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if(dt_datatable.Rows.Count > 0)
                {
                    objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                    for (int i = 0; i < objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist.Length; i++)
                    {
                        objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[i] = new ContactPersonData();
                    }
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int contactpersonind = 0;
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["supronboardcontactdetails_gid"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                            objMdlBuyerGeneralContactUpdateAddHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                            contactpersonind++;
                        }
                    }
                    dt_datatable.Dispose();


                    string MdlBuyerInstitutionContactUpdateAddHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerGeneralContactUpdateAddHBAPI);
                    LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(MdlBuyerInstitutionContactUpdateAddHBAPIJSON, LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);



                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierGenContactadd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlBuyerInstitutionContactUpdateAddHBAPIJSON);
                    request.AddParameter("customer_erpid", supplier_erpid);


                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                    objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                    if (objHBAPICustomerUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerUpdateResponse.contactlist_generaldetails)
                        {
                            msSQL = " update agr_mst_tsupronboardcontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where supronboardcontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                    }
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not needed", LoggingTypeHBAPIUpdate.Supplier);
                }
                

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }

        public void UpdateSupplierGeneralContactBasicHBAPI(string application_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierGeneralContactBasicHBAPI + " for Supplier with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select institution_gid from agr_mst_tsupronboard2institution" +
               " where application_gid = '" + application_gid + "' and stakeholder_type = 'Applicant'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contact_gid from agr_mst_tsupronboardcontact" +
                " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!string.IsNullOrEmpty(lsinstitution_gid))
                {
                    msSQL = " select urn from agr_mst_tsupronboard2institution" +
                            " where institution_gid = '" + lsinstitution_gid + "'";
                    supplier_erpid = objdbconn.GetExecuteScalar(msSQL);
                }
                else if (!string.IsNullOrEmpty(lscontact_gid))
                {
                    msSQL = " select urn from agr_mst_tsupronboardcontact" +
                           " where contact_gid = '" + lscontact_gid + "'";
                    supplier_erpid = objdbconn.GetExecuteScalar(msSQL);
                }


                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + supplier_erpid, LoggingTypeHBAPIUpdate.Supplier);


                msSQL = " select contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_type" +
                        " from agr_mst_tsupronboard where application_gid='" + application_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscontactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                    lscontactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                    lscontactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                    lsdesignation = objODBCDatareader["designation_type"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " update agr_mst_tsupronboardcontactdetails set" +
                    " first_name='" + lscontactpersonfirst_name + "'," +
                    " middle_name='" + lscontactpersonmiddle_name + "'," +
                    " last_name='" + lscontactpersonlast_name + "'," +
                    " designation='" + lsdesignation + "'" +
                    " where application_gid='" + application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                    LogForAuditUpdateHBAPI("Basic Details Updated in Supplier General Contacts table", LoggingTypeHBAPIUpdate.Supplier);



                MdlSupplierGeneralContactUpdateBasicHBAPI objMdlBuyerGeneralContactUpdateBasicHBAPI = new MdlSupplierGeneralContactUpdateBasicHBAPI();

                objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral = new ContactPersonDetailsGeneral();
                objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[] { };

                msSQL = " select supronboardcontactdetails_gid,first_name,middle_name,last_name,designation" +
                      " from agr_mst_tsupronboardcontactdetails where application_gid='" + application_gid + "' and erp_id is not null";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist.Length; i++)
                {
                    objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["supronboardcontactdetails_gid"].ToString();
                        objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerGeneralContactUpdateBasicHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();



                string MdlBuyerGeneralContactUpdateBasicHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerGeneralContactUpdateBasicHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(MdlBuyerGeneralContactUpdateBasicHBAPIJSON, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierGenContactBasic);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlBuyerGeneralContactUpdateBasicHBAPIJSON);
                request.AddParameter("customer_erpid", supplier_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }

        //Institution
        public void UpdateSupplierInstitutionHBAPI(string institution_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierInstitutionHBAPI + " for Supplier with Institution GID -" + institution_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                MdlBuyerInstitutionUpdateHBAPI objMdlBuyerInstitutionUpdateHBAPI = new MdlBuyerInstitutionUpdateHBAPI();

                msSQL = " select officialemail_address,official_telephoneno,company_name,companypan_no,companytype_gid,tan_number,incometax_returnsstatus," +
                        " lastyear_turnover from agr_mst_tsupronboard2institution" +
                        " where institution_gid = '" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerInstitutionUpdateHBAPI.officialemail_address = objODBCDatareader["officialemail_address"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.official_telephoneno = objODBCDatareader["official_telephoneno"].ToString();
                    //objMdlBuyerInstitutionUpdateHBAPI.company_name = objODBCDatareader["company_name"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.companypan_no = objODBCDatareader["companypan_no"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.companytype_name = obj.fetchCompanyTypeName(objODBCDatareader["companytype_gid"].ToString());
                    objMdlBuyerInstitutionUpdateHBAPI.tan_number = objODBCDatareader["tan_number"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.incometax_returnsstatus = objODBCDatareader["incometax_returnsstatus"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.lastyear_turnover = objODBCDatareader["lastyear_turnover"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select bankaccount_number,bankaccount_name,ifsc_code,bank_name,micr_code,bank_address,branch_name" +
                       " from agr_mst_tsupronboardinstitution2bankdtl where institution_gid='" + institution_gid + "' and primary_status = 'Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerInstitutionUpdateHBAPI.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.accountholder_name = objODBCDatareader["bankaccount_name"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.bank_name = objODBCDatareader["bank_name"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.micr_code = objODBCDatareader["micr_code"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.bank_address = objODBCDatareader["bank_address"].ToString();
                    objMdlBuyerInstitutionUpdateHBAPI.branch_name = objODBCDatareader["branch_name"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select gst_no from agr_mst_tsupronboardinstitution2branch where headoffice_status = 'Yes' and institution_gid = '" + institution_gid + "'";
                objMdlBuyerInstitutionUpdateHBAPI.primary_gst = objdbconn.GetExecuteScalar(msSQL);

                string SupplierInstitutionUpdateHBAPI = JsonConvert.SerializeObject(objMdlBuyerInstitutionUpdateHBAPI);
                LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(SupplierInstitutionUpdateHBAPI, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select urn from agr_mst_tsupronboard2institution" +
                        " where institution_gid = '" + institution_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);
                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Supplier);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierInstitution);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", SupplierInstitutionUpdateHBAPI);
                request.AddParameter("customer_erpid", customer_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);



                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {

                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        } 

        public void UpdateSupplierInstitutionAddressHBAPI(string institution2address_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierInstitutionAddressHBAPI + " for Supplier with Institution2address GID -" + institution2address_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                MdlInstitutionAddressUpdateHBAPI objMdlInstitutionAddressUpdateHBAPI = new MdlInstitutionAddressUpdateHBAPI();

                msSQL = " select institution_gid from agr_mst_tsupronboardinstitution2address" +
               " where institution2address_gid = '" + institution2address_gid + "'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select urn from agr_mst_tsupronboard2institution" +
               " where institution_gid = '" + lsinstitution_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select institution2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude,erp_id" +
                        " from agr_mst_tsupronboardinstitution2address where institution2address_gid='" + institution2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlInstitutionAddressUpdateHBAPI.externalid = objODBCDatareader["institution2address_gid"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.addressline1 = objODBCDatareader["addressline1"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.addressline2 = objODBCDatareader["addressline2"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.city = objODBCDatareader["city"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.state = obj.fetchNsAddrStateCode(objODBCDatareader["state"].ToString());
                    objMdlInstitutionAddressUpdateHBAPI.postal_code = objODBCDatareader["postal_code"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.latitude = objODBCDatareader["latitude"].ToString();
                    objMdlInstitutionAddressUpdateHBAPI.longitude = objODBCDatareader["longitude"].ToString();
                    address_erpid = objODBCDatareader["erp_id"].ToString();

                }
                objODBCDatareader.Close();

                LogForAuditUpdateHBAPI("Address ERP ID obtained - " + address_erpid, LoggingTypeHBAPIUpdate.Supplier);

                string MdlInstitutionAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlInstitutionAddressUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(MdlInstitutionAddressUpdateHBAPI, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);



                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierAddress);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlInstitutionAddressUpdateHBAPI);
                request.AddParameter("address_erpid", address_erpid);
                request.AddParameter("customer_erpid", customer_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);

            }

        }

        public void UpdateSupplierInstitutionAddressAddHBAPI(string institution_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierInstitutionAddressAddHBAPI + " for Supplier with Institution GID -" + institution_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                MdlInstitutionAddressUpdateAddHBAPI objMdlInstitutionAddressUpdateAddHBAPI = new MdlInstitutionAddressUpdateAddHBAPI();

                msSQL = " select urn from agr_mst_tsupronboard2institution" +
                " where institution_gid = '" + institution_gid + "'";
                supplier_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + supplier_erpid, LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select institution2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude" +
                        " from agr_mst_tsupronboardinstitution2address where (erp_id is null or erp_id = '')  and institution_gid ='" + institution_gid + "'";
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
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].state = obj.fetchNsAddrStateCode(dr_datarow["state"].ToString());
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                            objMdlInstitutionAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["institution2address_gid"].ToString();
                            addressind++;
                        }
                    }
                    dt_datatable.Dispose();

                    string MdlInstitutionAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlInstitutionAddressUpdateAddHBAPI);


                    LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(MdlInstitutionAddressUpdateHBAPI, LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierAddressAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlInstitutionAddressUpdateHBAPI);
                    request.AddParameter("customer_erpid", supplier_erpid);


                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);


                    objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                    if (objHBAPICustomerUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerUpdateResponse.addresslist)
                        {
                            msSQL = " update agr_mst_tsupronboardinstitution2address set " +
                          " erp_id='" + entry.Value + "'" +
                          " where institution2address_gid='" + entry.Key + "' ";

                            mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);

                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                    }
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }


        public void UpdateSupplierInstitutionContactHBAPI(string function_gid, string edit_from)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                MdlInstitutionContactUpdateHBAPI objMdlInstitutionContactUpdateHBAPI = new MdlInstitutionContactUpdateHBAPI();
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierInstitutionContactHBAPI + " for Supplier with Function GID -" + function_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                if (edit_from == UpdateContactHBAPIFrom.Email)
                {
                    msSQL = " select institution_gid from agr_mst_tsupronboardinstitution2email" +
                        " where institution2email_gid = '" + function_gid + "'";
                }
                else if (edit_from == UpdateContactHBAPIFrom.MobileNo)
                {
                    msSQL = " select institution_gid from agr_mst_tsupronboardinstitution2mobileno" +
                        " where institution2mobileno_gid = '" + function_gid + "'";
                }
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);


                LogForAuditUpdateHBAPI("Institution GID obtained - " + lsinstitution_gid, LoggingTypeHBAPIUpdate.Supplier);


                if (edit_from == UpdateContactHBAPIFrom.Email)
                {
                    msSQL = " select email_address" +
                   " from agr_mst_tsupronboardinstitution2email where institution2email_gid='" + function_gid + "'";
                    lsemail = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " update agr_mst_tsupronboardinstitutioncontactdetails set" +
                    " email='" + lsemail + "'" +
                    " where email_gid='" + function_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select supronboardinstitutioncontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                      " from agr_mst_tsupronboardinstitutioncontactdetails where email_gid='" + function_gid + "'";
                }
                else if (edit_from == UpdateContactHBAPIFrom.MobileNo)
                {
                    msSQL = " select mobile_no" +
                  " from agr_mst_tsupronboardinstitution2mobileno where institution2mobileno_gid='" + function_gid + "'";
                    lsmobile_no = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " update agr_mst_tsupronboardinstitutioncontactdetails set" +
                    " mobileno='" + lsmobile_no + "'" +
                    " where mobileno_gid='" + function_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select supronboardinstitutioncontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                       " from agr_mst_tsupronboardinstitutioncontactdetails where mobileno_gid='" + function_gid + "'";
                }

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlInstitutionContactUpdateHBAPI.contactdetails_gid = objODBCDatareader["supronboardinstitutioncontactdetails_gid"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.contactperson_firstname = objODBCDatareader["first_name"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.contactperson_middlename = objODBCDatareader["middle_name"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.contactperson_lastname = objODBCDatareader["last_name"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.designation = objODBCDatareader["designation"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.email_address = objODBCDatareader["email"].ToString();
                    objMdlInstitutionContactUpdateHBAPI.mobile_no = objODBCDatareader["mobileno"].ToString();
                    contact_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();



                string MdlInstitutionContactUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlInstitutionContactUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(MdlInstitutionContactUpdateHBAPIJSON, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierContact);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlInstitutionContactUpdateHBAPIJSON);
                request.AddParameter("contact_erpid", contact_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);


                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);

            }

        }

        public void UpdateSupplierInstitutionContactAddHBAPI(List<string> mobilenogid_list, List<string> emailgid_list, string institution_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierInstitutionContactAddHBAPI + " for Supplier with institution GID -" + institution_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                MdlBuyerInstitutionContactUpdateAddHBAPI objMdlBuyerInstitutionContactUpdateAddHBAPI = new MdlBuyerInstitutionContactUpdateAddHBAPI();

                objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany = new ContactPersonDetailsCompany();
                objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[] { };

                if(mobilenogid_list.Count > 0 || emailgid_list.Count > 0)
                {
                    populateSupplierInstitutionContactTableUpdate(mobilenogid_list, emailgid_list, institution_gid); //Update details in Contact table
                }           

                msSQL = " select urn from agr_mst_tsupronboard2institution" +
                " where institution_gid = '" + institution_gid + "'";
                supplier_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + supplier_erpid, LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select supronboardinstitutioncontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                      " from agr_mst_tsupronboardinstitutioncontactdetails where institution_gid='" + lsinstitution_gid + "' and (erp_id is null or erp_id = '')";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if(dt_datatable.Rows.Count > 0)
                {
                    objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                    for (int i = 0; i < objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist.Length; i++)
                    {
                        objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[i] = new ContactPersonData();
                    }
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int contactpersonind = 0;
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["supronboardinstitutioncontactdetails_gid"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                            objMdlBuyerInstitutionContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                            contactpersonind++;
                        }
                    }
                    dt_datatable.Dispose();


                    string MdlSupplierInstitutionContactUpdateAddHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerInstitutionContactUpdateAddHBAPI);

                    LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(MdlSupplierInstitutionContactUpdateAddHBAPIJSON, LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);


                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierContactAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlSupplierInstitutionContactUpdateAddHBAPIJSON);
                    request.AddParameter("customer_erpid", supplier_erpid);


                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                    objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                    if (objHBAPICustomerUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerUpdateResponse.contactlist_company)
                        {
                            msSQL = " update agr_mst_tsupronboardinstitutioncontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where supronboardinstitutioncontactdetails_gid='" + entry.Key + "'";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                    }
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not needed", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }

        public void UpdateSupplierInstitutionContactBasicHBAPI(string institution_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierInstitutionContactBasicHBAPI + " for Supplier with Institution GID -" + institution_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select urn from agr_mst_tsupronboard2institution" +
               " where institution_gid = '" + institution_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);


                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Supplier);


                msSQL = " select contactperson_firstname,contactperson_middlename,contactperson_lastname,designation" +
                        " from agr_mst_tsupronboard2institution where institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lscontactpersonfirst_name = objODBCDatareader["contactperson_firstname"].ToString();
                    lscontactpersonmiddle_name = objODBCDatareader["contactperson_middlename"].ToString();
                    lscontactpersonlast_name = objODBCDatareader["contactperson_lastname"].ToString();
                    lsdesignation = objODBCDatareader["designation"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " update agr_mst_tsupronboardinstitutioncontactdetails set" +
                    " first_name='" + lscontactpersonfirst_name + "'," +
                    " middle_name='" + lscontactpersonmiddle_name + "'," +
                    " last_name='" + lscontactpersonlast_name + "'," +
                    " designation='" + lsdesignation + "'" +
                    " where institution_gid='" + institution_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                    LogForAuditUpdateHBAPI("Basic Details Updated in Supplier Institution Contacts table", LoggingTypeHBAPIUpdate.Supplier);



                MdlBuyerInstitutionContactUpdateBasicHBAPI objMdlBuyerInstitutionContactUpdateBasicHBAPI = new MdlBuyerInstitutionContactUpdateBasicHBAPI();

                objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany = new ContactPersonDetailsCompany();
                objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[] { };

                msSQL = " select supronboardinstitutioncontactdetails_gid,first_name,middle_name,last_name,designation" +
                      " from agr_mst_tsupronboardinstitutioncontactdetails where institution_gid='" + institution_gid + "' and erp_id is not null";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist.Length; i++)
                {
                    objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["supronboardinstitutioncontactdetails_gid"].ToString();
                        objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerInstitutionContactUpdateBasicHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();



                string MdlBuyerInstitutionContactUpdateBasicHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerInstitutionContactUpdateBasicHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(MdlBuyerInstitutionContactUpdateBasicHBAPIJSON, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierContactBasic);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlBuyerInstitutionContactUpdateBasicHBAPIJSON);
                request.AddParameter("customer_erpid", customer_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }


        //Individual
        public void UpdateSupplierIndividualHBAPI(string contact_gid)
        {
            HBAPISupplierUpdateResponse objHBAPISupplierUpdateResponse = new HBAPISupplierUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierIndividualHBAPI + " for Individual Supplier with Contact GID -" + contact_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);

                MdlSupplierUpdateIndividualHBAPI objMdlSupplierIndividualUpdateHBAPI = new MdlSupplierUpdateIndividualHBAPI();

                msSQL = " select first_name,middle_name,last_name,aadhar_no" +
                        " from agr_mst_tsupronboardcontact where contact_gid='" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objMdlSupplierIndividualUpdateHBAPI.first_name = objODBCDatareader["first_name"].ToString();
                    objMdlSupplierIndividualUpdateHBAPI.middle_name = objODBCDatareader["middle_name"].ToString();
                    objMdlSupplierIndividualUpdateHBAPI.last_name = objODBCDatareader["last_name"].ToString();
                    objMdlSupplierIndividualUpdateHBAPI.aadhar_no = objODBCDatareader["aadhar_no"].ToString();

                }
                objODBCDatareader.Close();

                string SupplierIndividualUpdateHBAPI = JsonConvert.SerializeObject(objMdlSupplierIndividualUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Individual Buyer Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(SupplierIndividualUpdateHBAPI, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select urn from agr_mst_tsupronboardcontact" +
                " where contact_gid = '" + contact_gid + "' and stakeholder_type='Applicant'";

                supplier_erpid = objdbconn.GetExecuteScalar(msSQL);


                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierIndividual);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", SupplierIndividualUpdateHBAPI);
                request.AddParameter("supplier_erpid", supplier_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                objHBAPISupplierUpdateResponse = JsonConvert.DeserializeObject<HBAPISupplierUpdateResponse>(response.Content);

                if (objHBAPISupplierUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }

        public void UpdateSupplierIndividualAddressHBAPI(string contact2address_gid)
        {
            HBAPISupplierUpdateResponse objHBAPISupplierUpdateResponse = new HBAPISupplierUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierIndividualAddressHBAPI + " for Supplier with Address GID -" + contact2address_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                MdlIndividualAddressUpdateHBAPI objMdlIndividualAddressUpdateHBAPI = new MdlIndividualAddressUpdateHBAPI();

                msSQL = " select contact_gid from agr_mst_tsupronboardcontact2address" +
               " where contact2address_gid = '" + contact2address_gid + "'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select urn from agr_mst_tsupronboardcontact" +
               " where contact_gid = '" + lscontact_gid + "'";
                supplier_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + supplier_erpid, LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select contact2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude,erp_id" +
                        " from agr_mst_tsupronboardcontact2address where contact2address_gid='" + contact2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlIndividualAddressUpdateHBAPI.externalid = objODBCDatareader["contact2address_gid"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.addressline1 = objODBCDatareader["addressline1"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.addressline2 = objODBCDatareader["addressline2"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.city = objODBCDatareader["city"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.state = obj.fetchNsAddrStateCode(objODBCDatareader["state"].ToString());
                    objMdlIndividualAddressUpdateHBAPI.postal_code = objODBCDatareader["postal_code"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.latitude = objODBCDatareader["latitude"].ToString();
                    objMdlIndividualAddressUpdateHBAPI.longitude = objODBCDatareader["longitude"].ToString();
                    address_erpid = objODBCDatareader["erp_id"].ToString();

                }
                objODBCDatareader.Close();

                LogForAuditUpdateHBAPI("Address ERP ID obtained - " + address_erpid, LoggingTypeHBAPIUpdate.Supplier);

                string MdlIndividualAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlIndividualAddressUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Buyer Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(MdlIndividualAddressUpdateHBAPI, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Buyer JSON", LoggingTypeHBAPIUpdate.Supplier);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierAddress);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlIndividualAddressUpdateHBAPI);
                request.AddParameter("address_erpid", address_erpid);
                request.AddParameter("supplier_erpid", supplier_erpid);

                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                objHBAPISupplierUpdateResponse = JsonConvert.DeserializeObject<HBAPISupplierUpdateResponse>(response.Content);

                if (objHBAPISupplierUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }

        public void UpdateSupplierIndividualAddressAddHBAPI(string contact_gid)
        {
            HBAPISupplierUpdateResponse objHBAPISupplierUpdateResponse = new HBAPISupplierUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierIndividualAddressAddHBAPI + " for Individual Supplier with Contact GID -" + contact_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                MdlIndividualAddressUpdateAddHBAPI objMdlIndividualAddressUpdateAddHBAPI = new MdlIndividualAddressUpdateAddHBAPI();

                msSQL = " select urn from agr_mst_tsupronboardcontact" +
                " where contact_gid = '" + contact_gid + "'";
                supplier_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + supplier_erpid, LoggingTypeHBAPIUpdate.Supplier);



                msSQL = " select contact2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude" +
                        " from agr_mst_tsupronboardcontact2address where (erp_id is null or erp_id = '') and contact_gid = '" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count > 0)
                {
                    objMdlIndividualAddressUpdateAddHBAPI.addressDetails = new AddressDetails();

                    objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist = new AddressData[dt_datatable.Rows.Count];

                    for (int i = 0; i < objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist.Length; i++)
                    {
                        objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[i] = new AddressData();

                    }
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int addressind = 0;
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addresstype_name = dr_datarow["addresstype_name"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addressline1 = dr_datarow["addressline1"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].addressline2 = dr_datarow["addressline2"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].city = dr_datarow["city"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].state = obj.fetchNsAddrStateCode(dr_datarow["state"].ToString());
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                            objMdlIndividualAddressUpdateAddHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["contact2address_gid"].ToString();
                            addressind++;
                        }
                    }
                    dt_datatable.Dispose();




                    string MdlIndividualAddressUpdateHBAPI = JsonConvert.SerializeObject(objMdlIndividualAddressUpdateAddHBAPI);

                    LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(MdlIndividualAddressUpdateHBAPI, LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierAddressAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlIndividualAddressUpdateHBAPI);
                    request.AddParameter("supplier_erpid", supplier_erpid);


                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                    objHBAPISupplierUpdateResponse = JsonConvert.DeserializeObject<HBAPISupplierUpdateResponse>(response.Content);

                    if (objHBAPISupplierUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPISupplierUpdateResponse.addresslist)
                        {
                            msSQL = " update agr_mst_tsupronboardcontact2address set " +
                          " erp_id='" + entry.Value + "'" +
                          " where contact2address_gid='" + entry.Key + "' ";

                            mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);

                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                    }
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception Occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }


        public void UpdateSupplierIndividualContactHBAPI(string function_gid, string edit_from)
        {
            HBAPIIndividualSupplierUpdateResponse objHBAPIIndividualSupplierUpdateResponse = new HBAPIIndividualSupplierUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierIndividualContactHBAPI + " for Supplier with Function GID -" + function_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                MdlIndividualContactUpdateHBAPI objMdlIndividualContactUpdateHBAPI = new MdlIndividualContactUpdateHBAPI();

                if (edit_from == "email")
                {
                    msSQL = " select contact_gid from agr_mst_tsupronboardcontact2email" +
                        " where contact2email_gid = '" + function_gid + "'";
                }
                else if (edit_from == "mobileno")
                {
                    msSQL = " select contact_gid from agr_mst_tsupronboardcontact2mobileno" +
                        " where contact2mobileno_gid = '" + function_gid + "'";
                }
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);
                LogForAuditUpdateHBAPI("Contact GID obtained - " + lscontact_gid, LoggingTypeHBAPIUpdate.Supplier);

                if (edit_from == "email")
                {
                    msSQL = " select supronboardindividualcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                      " from agr_mst_tsupronboardindividualcontactdetails where email_gid='" + function_gid + "'";
                }
                else if (edit_from == "mobileno")
                {
                    msSQL = " select supronboardindividualcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email,erp_id" +
                       " from agr_mst_tsupronboardindividualcontactdetails where mobileno_gid='" + function_gid + "'";
                }

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlIndividualContactUpdateHBAPI.contactdetails_gid = objODBCDatareader["supronboardindividualcontactdetails_gid"].ToString();
                    objMdlIndividualContactUpdateHBAPI.contactperson_firstname = objODBCDatareader["first_name"].ToString();
                    objMdlIndividualContactUpdateHBAPI.contactperson_middlename = objODBCDatareader["middle_name"].ToString();
                    objMdlIndividualContactUpdateHBAPI.contactperson_lastname = objODBCDatareader["last_name"].ToString();
                    objMdlIndividualContactUpdateHBAPI.designation = objODBCDatareader["designation"].ToString();
                    objMdlIndividualContactUpdateHBAPI.email_address = objODBCDatareader["email"].ToString();
                    objMdlIndividualContactUpdateHBAPI.mobile_no = objODBCDatareader["mobileno"].ToString();
                    contact_erpid = objODBCDatareader["erp_id"].ToString();
                }
                objODBCDatareader.Close();



                string MdlIndividualContactUpdateHBAPIJSON = JsonConvert.SerializeObject(objMdlIndividualContactUpdateHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(MdlIndividualContactUpdateHBAPIJSON, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateSupplierContact);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlIndividualContactUpdateHBAPIJSON);
                request.AddParameter("contact_erpid", contact_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                objHBAPIIndividualSupplierUpdateResponse = JsonConvert.DeserializeObject<HBAPIIndividualSupplierUpdateResponse>(response.Content);

                if (objHBAPIIndividualSupplierUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was Successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not Successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception Occurred in Update- " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }

        public void UpdateSupplierIndividualContactAddHBAPI(List<string> mobilenogid_list, List<string> emailgid_list, string contact_gid)
        {
            HBAPISupplierUpdateResponse objHBAPISupplierUpdateResponse = new HBAPISupplierUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierIndividualContactAddHBAPI + " for Supplier with Contact GID -" + contact_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);
                MdlSupplierIndividualContactUpdateAddHBAPI objMdlSupplierIndividualContactUpdateAddHBAPI = new MdlSupplierIndividualContactUpdateAddHBAPI();

                objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany = new ContactPersonDetailsCompany();
                objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[] { };

                if(mobilenogid_list.Count > 0 || emailgid_list.Count > 0)
                {
                    populateSupplierIndividualContactTableUpdate(mobilenogid_list, emailgid_list, contact_gid); //Update details in Contact table
                }        

                msSQL = " select urn from agr_mst_tsupronboardcontact" +
                " where contact_gid = '" + contact_gid + "'";
                supplier_erpid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditUpdateHBAPI("Customer ERP ID obtained - " + supplier_erpid, LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select supronboardindividualcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                      " from agr_mst_tsupronboardindividualcontactdetails where contact_gid='" + lscontact_gid + "' and (erp_id is null or erp_id = '')";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if(dt_datatable.Rows.Count > 0)
                {
                    objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                    for (int i = 0; i < objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist.Length; i++)
                    {
                        objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[i] = new ContactPersonData();
                    }
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int contactpersonind = 0;
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["supronboardindividualcontactdetails_gid"].ToString();
                            objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                            objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                            objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                            objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                            objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                            objMdlSupplierIndividualContactUpdateAddHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                            contactpersonind++;
                        }
                    }
                    dt_datatable.Dispose();


                    string MdlSupplierIndividualContactUpdateAddHBAPIJSON = JsonConvert.SerializeObject(objMdlSupplierIndividualContactUpdateAddHBAPI);

                    LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(MdlSupplierIndividualContactUpdateAddHBAPIJSON, LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateIndividualSupplierContactAdd);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", MdlSupplierIndividualContactUpdateAddHBAPIJSON);
                    request.AddParameter("supplier_erpid", supplier_erpid);


                    IRestResponse response = client.Execute(request);

                    LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                    LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                    objHBAPISupplierUpdateResponse = JsonConvert.DeserializeObject<HBAPISupplierUpdateResponse>(response.Content);

                    if (objHBAPISupplierUpdateResponse.status == true)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPISupplierUpdateResponse.contactlist_company)
                        {
                            msSQL = " update agr_mst_tsupronboardindividualcontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where supronboardindividualcontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        LogForAuditUpdateHBAPI("Update was Successful", LoggingTypeHBAPIUpdate.Supplier);
                    }
                    else
                    {
                        LogForAuditUpdateHBAPI("Update was not Successful", LoggingTypeHBAPIUpdate.Supplier);
                    }
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not needed", LoggingTypeHBAPIUpdate.Supplier);
                }
                

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occurred in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }

        public void UpdateSupplierIndividualContactBasicHBAPI(string contact_gid)
        {
            HBAPICustomerUpdateResponse objHBAPICustomerUpdateResponse = new HBAPICustomerUpdateResponse();
            try
            {
                LogForAuditUpdateHBAPI("Logging started in API - " + UpdateAPIMetaList.UpdateSupplierIndividualContactBasicHBAPI + " for Supplier with Contact GID -" + contact_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIUpdate.Supplier);

                msSQL = " select urn from agr_mst_tsupronboardcontact" +
               " where contact_gid = '" + contact_gid + "'";
                customer_erpid = objdbconn.GetExecuteScalar(msSQL);


                LogForAuditUpdateHBAPI("Supplier ERP ID obtained - " + customer_erpid, LoggingTypeHBAPIUpdate.Supplier);


                msSQL = " select first_name,middle_name,last_name,designation_name" +
                        " from agr_mst_tsupronboardcontact where contact_gid='" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lscontactpersonfirst_name = objODBCDatareader["contactperson_firstname"].ToString();
                    lscontactpersonmiddle_name = objODBCDatareader["contactperson_middlename"].ToString();
                    lscontactpersonlast_name = objODBCDatareader["contactperson_lastname"].ToString();
                    lsdesignation = objODBCDatareader["designation"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " update agr_mst_tsupronboardindividualcontactdetails set" +
                    " first_name='" + lscontactpersonfirst_name + "'," +
                    " middle_name='" + lscontactpersonmiddle_name + "'," +
                    " last_name='" + lscontactpersonlast_name + "'," +
                    " designation='" + lsdesignation + "'" +
                    " where contact_gid='" + contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                    LogForAuditUpdateHBAPI("Basic Details Updated in Supplier Individual Contacts table", LoggingTypeHBAPIUpdate.Supplier);



                MdlBuyerIndividualContactUpdateBasicHBAPI objMdlBuyerIndividualContactUpdateBasicHBAPI = new MdlBuyerIndividualContactUpdateBasicHBAPI();

                objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual = new ContactPersonDetailsIndividual();
                objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist = new ContactPersonData[] { };

                msSQL = " select supronboardindividualcontactdetails_gid,first_name,middle_name,last_name,designation" +
                      " from agr_mst_tsupronboardindividualcontactdetails where contact_gid='" + contact_gid + "' and erp_id is not null";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist.Length; i++)
                {
                    objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["supronboardindividualcontactdetails_gid"].ToString();
                        objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerIndividualContactUpdateBasicHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();



                string MdlBuyerIndividualContactUpdateBasicHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerIndividualContactUpdateBasicHBAPI);

                LogForAuditUpdateHBAPI("JSON generated from Supplier Data", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(MdlBuyerIndividualContactUpdateBasicHBAPIJSON, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Supplier JSON", LoggingTypeHBAPIUpdate.Supplier);

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIUpdateURL"].ToString() + HBUpdateAPINameRepo.NetSuiteAPIUpdateIndividualSupplierContactBasic);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", MdlBuyerIndividualContactUpdateBasicHBAPIJSON);
                request.AddParameter("customer_erpid", customer_erpid);


                IRestResponse response = client.Execute(request);

                LogForAuditUpdateHBAPI("Response obtained from HyperbridgeAPI", LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.StatusCode.ToString(), LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI(response.Content, LoggingTypeHBAPIUpdate.Supplier);
                LogForAuditUpdateHBAPI("End of Response", LoggingTypeHBAPIUpdate.Supplier);

                objHBAPICustomerUpdateResponse = JsonConvert.DeserializeObject<HBAPICustomerUpdateResponse>(response.Content);

                if (objHBAPICustomerUpdateResponse.status == true)
                {
                    LogForAuditUpdateHBAPI("Update was successful", LoggingTypeHBAPIUpdate.Supplier);
                }
                else
                {
                    LogForAuditUpdateHBAPI("Update was not successful", LoggingTypeHBAPIUpdate.Supplier);
                }

            }
            catch (Exception ex)
            {
                LogForAuditUpdateHBAPI("Exception occured in Update - " + ex.ToString(), LoggingTypeHBAPIUpdate.Supplier);
            }

        }



        //Buyer - Auxillary Flow Functions
        public void populateBuyerInstitutionContactTableUpdate(List<string> mobilenogid_list, List<string> emailgid_list, string institution_gid)
        {

            //Institution Contact 

            lsinstitution_gid = institution_gid;

            msSQL = " select contactperson_firstname,contactperson_middlename,contactperson_lastname,designation" +
                  " from agr_mst_tbyronboard2institution where institution_gid='" + lsinstitution_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscontactpersonfirst_name = objODBCDatareader["contactperson_firstname"].ToString();
                lscontactpersonmiddle_name = objODBCDatareader["contactperson_middlename"].ToString();
                lscontactpersonlast_name = objODBCDatareader["contactperson_lastname"].ToString();
                lsdesignation = objODBCDatareader["designation"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails = emailgid_list.Count > mobilenogid_list.Count ? new ContactDetails[emailgid_list.Count] : new ContactDetails[mobilenogid_list.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                msSQL = " select institution2mobileno_gid,primary_status,mobile_no" +
                " from agr_mst_tbyronboardinstitution2mobileno where institution2mobileno_gid='" + mobilenogid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
                    arrContactDetails[i].mobileno = objODBCDatareader["mobile_no"].ToString();
                    arrContactDetails[i].primary_status = objODBCDatareader["primary_status"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select institution2email_gid,email_address" +
               " from agr_mst_tbyronboardinstitution2email where institution2email_gid='" + emailgid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                arrContactDetails[i].email = objdbconn.GetExecuteScalar(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].email_gid = objODBCDatareader["institution2email_gid"].ToString();
                    arrContactDetails[i].email = objODBCDatareader["email_address"].ToString();
                }
                objODBCDatareader.Close();

            }

            foreach (var item in arrContactDetails)
            {
                mscontactdetailsGID = objcmnfunctions.GetMasterGID("BICT");
                msSQL = " insert into agr_mst_tbyronboardinstitutioncontactdetails(" +
                   " byronboardinstitutioncontactdetails_gid," +
                   " institution_gid," +
                   " mobileno_gid," +
                   " email_gid," +
                   " primary_status," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " designation," +
                   " mobileno," +
                   " email)" +
                   " values(" +
                   "'" + mscontactdetailsGID + "'," +
                   "'" + lsinstitution_gid + "'," +
                   "'" + item.mobileno_gid + "'," +
                   "'" + item.email_gid + "'," +
                   "'" + item.primary_status + "'," +
                   "'" + lscontactpersonfirst_name + "'," +
                   "'" + lscontactpersonmiddle_name + "'," +
                   "'" + lscontactpersonlast_name + "'," +
                   "'" + lsdesignation + "'," +
                   "'" + item.mobileno + "'," +
                   "'" + item.email + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }



        }

        public void populateBuyerGeneralContactTableUpdate(List<string> mobilenogid_list, List<string> emailgid_list, string application_gid)
        {

            //Institution Contact 

            lsapplication_gid = application_gid;

            msSQL = " select contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_type" +
                  " from agr_mst_tbyronboard where application_gid='" + lsapplication_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscontactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                lscontactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                lscontactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                lsdesignation = objODBCDatareader["designation_type"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails = emailgid_list.Count > mobilenogid_list.Count ? new ContactDetails[emailgid_list.Count] : new ContactDetails[mobilenogid_list.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                msSQL = " select application2contact_gid,primary_mobileno,mobile_no" +
                " from agr_mst_tbyronboard2contactno where application2contact_gid='" + mobilenogid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].mobileno_gid = objODBCDatareader["application2contact_gid"].ToString();
                    arrContactDetails[i].mobileno = objODBCDatareader["mobile_no"].ToString();
                    arrContactDetails[i].primary_status = objODBCDatareader["primary_mobileno"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select application2email_gid,email_address" +
               " from agr_mst_tbyronboard2email where application2email_gid='" + emailgid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                arrContactDetails[i].email = objdbconn.GetExecuteScalar(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].email_gid = objODBCDatareader["application2email_gid"].ToString();
                    arrContactDetails[i].email = objODBCDatareader["email_address"].ToString();
                }
                objODBCDatareader.Close();

            }

            foreach (var item in arrContactDetails)
            {
                mscontactdetailsGID = objcmnfunctions.GetMasterGID("BRCT");
                msSQL = " insert into agr_mst_tbyronboardcontactdetails(" +
                   " byronboardcontactdetails_gid," +
                   " application_gid," +
                   " mobileno_gid," +
                   " email_gid," +
                   " primary_status," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " designation," +
                   " mobileno," +
                   " email)" +
                   " values(" +
                   "'" + mscontactdetailsGID + "'," +
                   "'" + lsapplication_gid + "'," +
                   "'" + item.mobileno_gid + "'," +
                   "'" + item.email_gid + "'," +
                   "'" + item.primary_status + "'," +
                   "'" + lscontactpersonfirst_name + "'," +
                   "'" + lscontactpersonmiddle_name + "'," +
                   "'" + lscontactpersonlast_name + "'," +
                   "'" + lsdesignation + "'," +
                   "'" + item.mobileno + "'," +
                   "'" + item.email + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

        }

        public void populateBuyerIndividualContactTableUpdate(List<string> mobilenogid_list, List<string> emailgid_list, string contact_gid)
        {

            //Individual Contact 

            lscontact_gid = contact_gid;

            msSQL = " select first_name,middle_name,last_name,designation_name" +
                  " from agr_mst_tbyronboardcontact where contact_gid='" + lscontact_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscontactpersonfirst_name = objODBCDatareader["first_name"].ToString();
                lscontactpersonmiddle_name = objODBCDatareader["middle_name"].ToString();
                lscontactpersonlast_name = objODBCDatareader["last_name"].ToString();
                lsdesignation = objODBCDatareader["designation_name"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails = emailgid_list.Count > mobilenogid_list.Count ? new ContactDetails[emailgid_list.Count] : new ContactDetails[mobilenogid_list.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                msSQL = " select contact2mobileno_gid,primary_status,mobile_no" +
                " from agr_mst_tbyronboardcontact2mobileno where contact2mobileno_gid='" + mobilenogid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].mobileno_gid = objODBCDatareader["contact2mobileno_gid"].ToString();
                    arrContactDetails[i].mobileno = objODBCDatareader["mobile_no"].ToString();
                    arrContactDetails[i].primary_status = objODBCDatareader["primary_status"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select contact2email_gid,email_address" +
               " from agr_mst_tbyronboardcontact2email where contact2email_gid='" + emailgid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                arrContactDetails[i].email = objdbconn.GetExecuteScalar(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].email_gid = objODBCDatareader["contact2email_gid"].ToString();
                    arrContactDetails[i].email = objODBCDatareader["email_address"].ToString();
                }
                objODBCDatareader.Close();

            }

            foreach (var item in arrContactDetails)
            {
                mscontactdetailsGID = objcmnfunctions.GetMasterGID("BICT");
                msSQL = " insert into agr_mst_tbyronboardindividualcontactdetails(" +
                   " byronboardindividualcontactdetails_gid," +
                   " contact_gid," +
                   " mobileno_gid," +
                   " email_gid," +
                   " primary_status," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " designation," +
                   " mobileno," +
                   " email)" +
                   " values(" +
                   "'" + mscontactdetailsGID + "'," +
                   "'" + lscontact_gid + "'," +
                   "'" + item.mobileno_gid + "'," +
                   "'" + item.email_gid + "'," +
                   "'" + item.primary_status + "'," +
                   "'" + lscontactpersonfirst_name + "'," +
                   "'" + lscontactpersonmiddle_name + "'," +
                   "'" + lscontactpersonlast_name + "'," +
                   "'" + lsdesignation + "'," +
                   "'" + item.mobileno + "'," +
                   "'" + item.email + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }



        }

        //Supplier - Auxillary Flow Functions
        public void populateSupplierInstitutionContactTableUpdate(List<string> mobilenogid_list, List<string> emailgid_list, string institution_gid)
        {

            //Institution Contact 

            lsinstitution_gid = institution_gid;

            msSQL = " select contactperson_firstname,contactperson_middlename,contactperson_lastname,designation" +
                  " from agr_mst_tsupronboard2institution where institution_gid='" + lsinstitution_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscontactpersonfirst_name = objODBCDatareader["contactperson_firstname"].ToString();
                lscontactpersonmiddle_name = objODBCDatareader["contactperson_middlename"].ToString();
                lscontactpersonlast_name = objODBCDatareader["contactperson_lastname"].ToString();
                lsdesignation = objODBCDatareader["designation"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails = emailgid_list.Count > mobilenogid_list.Count ? new ContactDetails[emailgid_list.Count] : new ContactDetails[mobilenogid_list.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                msSQL = " select institution2mobileno_gid,primary_status,mobile_no" +
                " from agr_mst_tsupronboardinstitution2mobileno where institution2mobileno_gid='" + mobilenogid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
                    arrContactDetails[i].mobileno = objODBCDatareader["mobile_no"].ToString();
                    arrContactDetails[i].primary_status = objODBCDatareader["primary_status"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select institution2email_gid,email_address" +
               " from agr_mst_tsupronboardinstitution2email where institution2email_gid='" + emailgid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                arrContactDetails[i].email = objdbconn.GetExecuteScalar(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].email_gid = objODBCDatareader["institution2email_gid"].ToString();
                    arrContactDetails[i].email = objODBCDatareader["email_address"].ToString();
                }
                objODBCDatareader.Close();

            }

            foreach (var item in arrContactDetails)
            {
                mscontactdetailsGID = objcmnfunctions.GetMasterGID("SICT");
                msSQL = " insert into agr_mst_tsupronboardinstitutioncontactdetails(" +
                   " supronboardinstitutioncontactdetails_gid," +
                   " institution_gid," +
                   " mobileno_gid," +
                   " email_gid," +
                   " primary_status," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " designation," +
                   " mobileno," +
                   " email)" +
                   " values(" +
                   "'" + mscontactdetailsGID + "'," +
                   "'" + lsinstitution_gid + "'," +
                   "'" + item.mobileno_gid + "'," +
                   "'" + item.email_gid + "'," +
                   "'" + item.primary_status + "'," +
                   "'" + lscontactpersonfirst_name + "'," +
                   "'" + lscontactpersonmiddle_name + "'," +
                   "'" + lscontactpersonlast_name + "'," +
                   "'" + lsdesignation + "'," +
                   "'" + item.mobileno + "'," +
                   "'" + item.email + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }



        }

        public void populateSupplierGeneralContactTableUpdate(List<string> mobilenogid_list, List<string> emailgid_list, string application_gid)
        {

            // General Contact 

            lsapplication_gid = application_gid;

            msSQL = " select contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_type from agr_mst_tsupronboard where application_gid='" + lsapplication_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscontactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                lscontactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                lscontactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                lsdesignation = objODBCDatareader["designation_type"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails = emailgid_list.Count > mobilenogid_list.Count ? new ContactDetails[emailgid_list.Count] : new ContactDetails[mobilenogid_list.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                msSQL = "select application2contact_gid, mobile_no, primary_mobileno from agr_mst_tsupronboard2contactno  where application2contact_gid = '" + mobilenogid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].mobileno_gid = objODBCDatareader["application2contact_gid"].ToString();
                    arrContactDetails[i].mobileno = objODBCDatareader["mobile_no"].ToString();
                    arrContactDetails[i].primary_status = objODBCDatareader["primary_mobileno"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select application2email_gid,email_address from agr_mst_tsupronboard2email where application2email_gid='" + emailgid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                arrContactDetails[i].email = objdbconn.GetExecuteScalar(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].email_gid = objODBCDatareader["application2email_gid"].ToString();
                    arrContactDetails[i].email = objODBCDatareader["email_address"].ToString();
                }
                objODBCDatareader.Close();

            }

            foreach (var item in arrContactDetails)
            {
                mscontactdetailsGID = objcmnfunctions.GetMasterGID("SRCT");
                msSQL = " insert into agr_mst_tsupronboardcontactdetails(" +
                   " supronboardcontactdetails_gid," +
                   " application_gid," +
                   " mobileno_gid," +
                   " email_gid," +
                   " primary_status," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " designation," +
                   " mobileno," +
                   " email)" +
                   " values(" +
                   "'" + mscontactdetailsGID + "'," +
                   "'" + lsapplication_gid + "'," +
                   "'" + item.mobileno_gid + "'," +
                   "'" + item.email_gid + "'," +
                   "'" + item.primary_status + "'," +
                   "'" + lscontactpersonfirst_name + "'," +
                   "'" + lscontactpersonmiddle_name + "'," +
                   "'" + lscontactpersonlast_name + "'," +
                   "'" + lsdesignation + "'," +
                   "'" + item.mobileno + "'," +
                   "'" + item.email + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }



        }

        public void populateSupplierIndividualContactTableUpdate(List<string> mobilenogid_list, List<string> emailgid_list, string contact_gid)
        {

            //Individual Contact 

            lscontact_gid = contact_gid;

            msSQL = " select first_name,middle_name,last_name,designation_name" +
                  " from agr_mst_tsupronboardcontact where contact_gid='" + lscontact_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscontactpersonfirst_name = objODBCDatareader["first_name"].ToString();
                lscontactpersonmiddle_name = objODBCDatareader["middle_name"].ToString();
                lscontactpersonlast_name = objODBCDatareader["last_name"].ToString();
                lsdesignation = objODBCDatareader["designation_name"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails = emailgid_list.Count > mobilenogid_list.Count ? new ContactDetails[emailgid_list.Count] : new ContactDetails[mobilenogid_list.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                msSQL = " select contact2mobileno_gid,primary_status,mobile_no" +
                " from agr_mst_tsupronboardcontact2mobileno where contact2mobileno_gid='" + mobilenogid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].mobileno_gid = objODBCDatareader["contact2mobileno_gid"].ToString();
                    arrContactDetails[i].mobileno = objODBCDatareader["mobile_no"].ToString();
                    arrContactDetails[i].primary_status = objODBCDatareader["primary_status"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select contact2email_gid,email_address" +
               " from agr_mst_tsupronboardcontact2email where contact2email_gid='" + emailgid_list[i] + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                arrContactDetails[i].email = objdbconn.GetExecuteScalar(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    arrContactDetails[i].email_gid = objODBCDatareader["contact2email_gid"].ToString();
                    arrContactDetails[i].email = objODBCDatareader["email_address"].ToString();
                }
                objODBCDatareader.Close();

            }

            foreach (var item in arrContactDetails)
            {
                mscontactdetailsGID = objcmnfunctions.GetMasterGID("SICT");
                msSQL = " insert into agr_mst_tsupronboardindividualcontactdetails(" +
                   " supronboardindividualcontactdetails_gid," +
                   " contact_gid," +
                   " mobileno_gid," +
                   " email_gid," +
                   " primary_status," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " designation," +
                   " mobileno," +
                   " email)" +
                   " values(" +
                   "'" + mscontactdetailsGID + "'," +
                   "'" + lscontact_gid + "'," +
                   "'" + item.mobileno_gid + "'," +
                   "'" + item.email_gid + "'," +
                   "'" + item.primary_status + "'," +
                   "'" + lscontactpersonfirst_name + "'," +
                   "'" + lscontactpersonmiddle_name + "'," +
                   "'" + lscontactpersonlast_name + "'," +
                   "'" + lsdesignation + "'," +
                   "'" + item.mobileno + "'," +
                   "'" + item.email + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }



        }     

        //Auxillary Functions
        public void LogForAuditUpdateHBAPI(string strVal, string type)
        {
            try
            {

                if (type == LoggingTypeHBAPIUpdate.Buyer)
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
                else if (type == LoggingTypeHBAPIUpdate.Supplier)
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
                else if (type == "Employee")
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

    }
}