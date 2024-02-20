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
    public class FnSamAgroHBAPIConn
    {
        string msSQL,lsinstitution_gid,lscontact_gid,lsemployee_gid,lsuser_gid,lsrelationshipmanager_gid,lserp_status;
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
        string employee_externalid,hbemployee_externalid, hbentity_name, hbcreated_by, hbcreated_date, hbemployee_erpid;

        //Actual API

        public MdlHBAPIConnDAResponse PostEmployeeHBAPI(string employee_externalid)
        {
            string type = "Employee";
            MdlHBAPIConnDAResponse objemployeeresponse = new MdlHBAPIConnDAResponse();
            try
            {
                LogForAuditHBAPI("Logging started for Employee with Employee external ID -" + employee_externalid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

               
                MdlEmployeeHBAPI objMdlEmployeeHBAPI = new MdlEmployeeHBAPI();

                msSQL = "select employeereporting_to from hrm_mst_temployee where employee_externalid = '" + employee_externalid + "'";
                string lsemployeereporting_to = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select erp_status from hrm_mst_temployee where employee_gid = '" + lsemployeereporting_to + "'";
                string lsreportingto_erpstatus = objdbconn.GetExecuteScalar(msSQL);

                string lsreportingto_erpid;

                if (lsreportingto_erpstatus == "No")
                {
                    objemployeeresponse.status = false;
                    objemployeeresponse.message = "'Reporting To' of Employee couldn't be sent since that person hasn't been posted to ERP yet. Hence, posting employee to ERP failed.";
                    return objemployeeresponse;
                }
                else
                {
                    msSQL = "select erp_id from hrm_mst_temployee where employee_externalid = '" + lsemployeereporting_to + "'";
                    lsreportingto_erpid = objdbconn.GetExecuteScalar(msSQL);
                }

                msSQL = "select a.user_gid,a.employee_externalid,b.entity_name,c.baselocation_name, a.employee_emailid,a.employee_mobileno " +
                        "from hrm_mst_temployee a " +
                        "left join adm_mst_tentity b on a.entity_gid = b.entity_gid " +
                        "left join sys_mst_tbaselocation c on c.baselocation_gid = a.baselocation_gid " +
                        "where employee_externalid = '" + employee_externalid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsuser_gid = objODBCDatareader["user_gid"].ToString();
                        objMdlEmployeeHBAPI.employee_externalid = objODBCDatareader["employee_externalid"].ToString();
                        objMdlEmployeeHBAPI.entity_name = objODBCDatareader["entity_name"].ToString();
                        objMdlEmployeeHBAPI.employee_emailid = objODBCDatareader["employee_emailid"].ToString();
                        objMdlEmployeeHBAPI.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                        objMdlEmployeeHBAPI.baselocation = objODBCDatareader["baselocation_name"].ToString();
                        objMdlEmployeeHBAPI.employeereporting_to = lsreportingto_erpid;
                    }
                    objODBCDatareader.Close();

                    msSQL = " select user_firstname, user_lastname,user_code from adm_mst_tuser" +
                            " where user_gid = '" + lsuser_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objMdlEmployeeHBAPI.employee_firstname = objODBCDatareader["user_firstname"].ToString();
                        objMdlEmployeeHBAPI.employee_lastname = objODBCDatareader["user_lastname"].ToString();
                        objMdlEmployeeHBAPI.employee_code = objODBCDatareader["user_code"].ToString();
                    }
                    objODBCDatareader.Close();

                objMdlEmployeeHBAPI.entity = objHBAPICmnFunctions.GetEntityLMSCode(employee_externalid);
                objMdlEmployeeHBAPI.department = objHBAPICmnFunctions.GetDepartmentLMSCode(employee_externalid);




                LogForAuditHBAPI("Employee data obtained from Database", type);

                    string EmployeeHBAPIJSON = JsonConvert.SerializeObject(objMdlEmployeeHBAPI);

                    LogForAuditHBAPI("JSON generated from Employee data", type);
                    LogForAuditHBAPI(EmployeeHBAPIJSON, type);
                    LogForAuditHBAPI("End of Employee JSON", type);

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    HBAPIEmployeeResponse objHBAPIEmployeeResponse = new HBAPIEmployeeResponse();

                    var clientEmployee = new RestClient(ConfigurationManager.AppSettings["HBAPIPostURL"].ToString() + HBPostAPINameRepo.NetSuiteAPIDTrnPostEmployee);
                    var requestEmployee = new RestRequest(Method.POST);
                    requestEmployee.AddHeader("content-type", "application/x-www-form-urlencoded");
                    requestEmployee.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    requestEmployee.AddParameter("fromsamagroJSON", EmployeeHBAPIJSON);
                    IRestResponse responseEmployee = clientEmployee.Execute(requestEmployee);
                    objHBAPIEmployeeResponse = JsonConvert.DeserializeObject<HBAPIEmployeeResponse>(responseEmployee.Content);

                LogForAuditHBAPI("Response obtained from HyperbridgeAPI", type);
                LogForAuditHBAPI(responseEmployee.Content, type);
                LogForAuditHBAPI(responseEmployee.ErrorMessage, type);
                LogForAuditHBAPI("End of Response", type);

                if (objHBAPIEmployeeResponse.employeeerp_id != null && objHBAPIEmployeeResponse.employeeerp_id != "")
                    {

                         msSQL = " update hrm_mst_temployee set " +
                                 " erp_status='" + "Yes" + "'," +
                                 " erp_id='" + objHBAPIEmployeeResponse.employeeerp_id + "'" +
                                 " where employee_externalid='" + employee_externalid + "' ";
                         mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objemployeeresponse.status = true;
                    objemployeeresponse.message = "Employee Posted to NetSuite Successfully.";
                    LogForAuditHBAPI("Employee created in Netsuite successfully", type);
                    LogForAuditHBAPI("Logging ended for Employee with Employee GID -" + employee_externalid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                }
                else
                {
                    objemployeeresponse.status = false;
                    objemployeeresponse.message = objHBAPIEmployeeResponse.message;
                    objemployeeresponse.error_response = objHBAPIEmployeeResponse.error_response;
                    LogForAuditHBAPI("Error occured while posting employee", type);
                    LogForAuditHBAPI("Logging ended for Employee with Employee GID -" + employee_externalid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                }

            }
            catch(Exception ex)
            {
                LogForAuditHBAPI("Exception occured while posting Employee", type);
                LogForAuditHBAPI(ex.ToString(), type);
                LogForAuditHBAPI("End of Exception", type);
                LogForAuditHBAPI("Logging ended for Employee with Application GID -" + employee_externalid, type);
                objemployeeresponse.status = false;
                objemployeeresponse.message = "Exception occured while posting employee to NetSuite";
            }
            return objemployeeresponse;

        }
        
        public bool UpdateEmployeeHBAPI(UpdateHBAPIEmployeeRequest values)
        {
            
            try
            {
                HBAPIEmployeeUpdateResponse objHBAPIEmployeeUpdateResponse = new HBAPIEmployeeUpdateResponse();

                msSQL = "select employee_externalid from hrm_mst_temployee where employee_gid = '" + values.employee_gid + "'";
                values.employee_gid = objdbconn.GetExecuteScalar(msSQL);

                values.department_gid = objHBAPICmnFunctions.GetDepartmentLMSCode(values.employee_gid);

                msSQL = " select erp_id from hrm_mst_temployee" +
                        " where employee_externalid = '" + values.employee_gid + "'";
                    values.erp_id = objdbconn.GetExecuteScalar(msSQL);

                    string UpdateEmployeeHBAPIJSON = JsonConvert.SerializeObject(values);

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    var client = new RestClient(ConfigurationManager.AppSettings["HBAPIPostURL"].ToString() + HBPostAPINameRepo.NetSuiteAPIDTrnUpdateEmployee);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                    request.AddParameter("fromsamagroJSON", UpdateEmployeeHBAPIJSON);
                    IRestResponse response = client.Execute(request);
                    objHBAPIEmployeeUpdateResponse = JsonConvert.DeserializeObject<HBAPIEmployeeUpdateResponse>(response.Content);
                

                if (objHBAPIEmployeeUpdateResponse.status == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public MdlHBAPIConnDAResponse PostSupplierInstitutionHBAPI(string application_gid, string lsapprovalperson_name)
        {
            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();
            string type = "Supplier";
            try
            {
                LogForAuditHBAPI("Logging started for Supplier with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                msSQL = " select institution_gid from agr_mst_tsupronboard2institution" +
                    " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditHBAPI("Applicant GID obtained -" + lsinstitution_gid, type);

                MdlBuyerHBAPI objMdlBuyerHBAPI = new MdlBuyerHBAPI(); //Buyer Model has been reused for Supplier


                objMdlBuyerHBAPI.gstDetails = new GSTDetails();
                objMdlBuyerHBAPI.addressDetails = new AddressDetails();

                objMdlBuyerHBAPI.gstDetails.gstlist = new GSTData[] { };
                objMdlBuyerHBAPI.addressDetails.addresslist = new AddressData[] { };
                objMdlBuyerHBAPI.contactPersonDetailsGeneral = new ContactPersonDetailsGeneral();
                objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[] { };
                objMdlBuyerHBAPI.contactPersonDetailsCompany = new ContactPersonDetailsCompany();
                objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[] { };


                msSQL = " select customerref_name,vertical_name, constitution_name, application_no from agr_mst_tsupronboard" +
                        " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.company_name = objODBCDatareader["customerref_name"].ToString();
                    objMdlBuyerHBAPI.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    objMdlBuyerHBAPI.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    objMdlBuyerHBAPI.ExternalID = objODBCDatareader["application_no"].ToString();
                }
                objODBCDatareader.Close();


                msSQL = " select officialemail_address,official_telephoneno,company_name,companypan_no,companytype_gid,tan_number,incometax_returnsstatus," +
                        " lastyear_turnover" +
                        " from agr_mst_tsupronboard2institution where institution_gid='" + lsinstitution_gid + "' and stakeholder_type='Applicant'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    //objMdlBuyerHBAPI.company_name = objODBCDatareader["company_name"].ToString();
                    objMdlBuyerHBAPI.officialemail_address = objODBCDatareader["officialemail_address"].ToString();
                    objMdlBuyerHBAPI.official_telephoneno = objODBCDatareader["official_telephoneno"].ToString();
                    objMdlBuyerHBAPI.companypan_no = objODBCDatareader["companypan_no"].ToString();
                    objMdlBuyerHBAPI.companytype_name = fetchCompanyTypeName(objODBCDatareader["companytype_gid"].ToString());
                    objMdlBuyerHBAPI.tan_number = objODBCDatareader["tan_number"].ToString();
                    objMdlBuyerHBAPI.incometax_returnsstatus = objODBCDatareader["incometax_returnsstatus"].ToString();
                    objMdlBuyerHBAPI.lastyear_turnover = objODBCDatareader["lastyear_turnover"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select licensetype_name,license_no" +
                        " from agr_mst_tsupronboardinstitution2licensedtl where institution_gid='" + lsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (dr_datarow["licensetype_name"].ToString() == "FSSAI")
                            objMdlBuyerHBAPI.fssai_licenseno = dr_datarow["license_no"].ToString();
                        else if (dr_datarow["licensetype_name"].ToString() == "APMC")
                            objMdlBuyerHBAPI.apmc_licenseno = dr_datarow["license_no"].ToString();
                        else if (dr_datarow["licensetype_name"].ToString() == "IEC")
                            objMdlBuyerHBAPI.iec_licenseno = dr_datarow["license_no"].ToString();
                    }
                }
                dt_datatable.Dispose();

                populateSupplierInstitutionContactTable(application_gid); //Function to populate contacts in separate table

                msSQL = " select supronboardcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                        " from agr_mst_tsupronboardcontactdetails where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist.Length; i++)
                {
                    objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["supronboardcontactdetails_gid"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select supronboardinstitutioncontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                       " from agr_mst_tsupronboardinstitutioncontactdetails where institution_gid='" + lsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist.Length; i++)
                {
                    objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["supronboardinstitutioncontactdetails_gid"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();



                msSQL = " select institution2branch_gid,gst_no,gst_state" +
                        " from agr_mst_tsupronboardinstitution2branch where institution_gid='" + lsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                objMdlBuyerHBAPI.gstDetails.gstlist = new GSTData[dt_datatable.Rows.Count];
                for (int i = 0; i < objMdlBuyerHBAPI.gstDetails.gstlist.Length; i++)
                {
                    objMdlBuyerHBAPI.gstDetails.gstlist[i] = new GSTData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int gstind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.gstDetails.gstlist[gstind].gst_no = dr_datarow["gst_no"].ToString();
                        objMdlBuyerHBAPI.gstDetails.gstlist[gstind].externalid = dr_datarow["institution2branch_gid"].ToString();
                        objMdlBuyerHBAPI.gstDetails.gstlist[gstind].gst_state = fetchNsGSTStateSode(dr_datarow["gst_state"].ToString());
                        gstind++;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select institution2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude" +
                        " from agr_mst_tsupronboardinstitution2address where institution_gid='" + lsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                objMdlBuyerHBAPI.addressDetails.addresslist = new AddressData[dt_datatable.Rows.Count];
                for (int i = 0; i < objMdlBuyerHBAPI.addressDetails.addresslist.Length; i++)
                {
                    objMdlBuyerHBAPI.addressDetails.addresslist[i] = new AddressData();

                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int addressind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addresstype_name = dr_datarow["addresstype_name"].ToString(); ;
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addressline1 = dr_datarow["addressline1"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addressline2 = dr_datarow["addressline2"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].city = dr_datarow["city"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["institution2address_gid"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].state = fetchNsAddrStateCode(dr_datarow["state"].ToString());
                        addressind++;
                    }
                }
                dt_datatable.Dispose();


                msSQL = " select bankaccount_number,bankaccount_name,ifsc_code,bank_name,micr_code,bank_address,branch_name" +
                        " from agr_mst_tsupronboardinstitution2bankdtl where institution_gid='" + lsinstitution_gid + "' and primary_status = 'Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    objMdlBuyerHBAPI.accountholder_name = objODBCDatareader["bankaccount_name"].ToString();
                    objMdlBuyerHBAPI.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objMdlBuyerHBAPI.bank_name = objODBCDatareader["bank_name"].ToString();
                    objMdlBuyerHBAPI.micr_code = objODBCDatareader["micr_code"].ToString();
                    objMdlBuyerHBAPI.bank_address = objODBCDatareader["bank_address"].ToString();
                    objMdlBuyerHBAPI.branch_name = objODBCDatareader["branch_name"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select gst_no from agr_mst_tsupronboardinstitution2branch where headoffice_status = 'Yes' and institution_gid = '" + lsinstitution_gid + "'";
                objMdlBuyerHBAPI.primary_gst = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditHBAPI("Supplier data obtained from Database", type);

                string BuyerHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerHBAPI);

                LogForAuditHBAPI("JSON generated from Supplier data", type);
                LogForAuditHBAPI(BuyerHBAPIJSON, type);
                LogForAuditHBAPI("End of Supplier JSON", type);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                HBAPICustomerResponse objHBAPICustomerResponse = new HBAPICustomerResponse();
               
                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIPostURL"].ToString() + HBPostAPINameRepo.NetSuiteAPIDTrnPostSupplier);
                
                var request = new RestRequest(Method.POST);
                
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
               
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                
                request.AddParameter("fromsamagroJSON", BuyerHBAPIJSON);
                
                request.AddParameter("approvalPersonName", lsapprovalperson_name);
                
                IRestResponse response = client.Execute(request);
               
                objHBAPICustomerResponse = JsonConvert.DeserializeObject<HBAPICustomerResponse>(response.Content);
          
                LogForAuditHBAPI("Response obtained from HyperbridgeAPI", type);
                LogForAuditHBAPI(response.Content, type);
                LogForAuditHBAPI("End of Response", type);

                if (objHBAPICustomerResponse.customererp_id != null && objHBAPICustomerResponse.customererp_id != "")
                {
                    msSQL = " update agr_mst_tsupronboard2institution set " +
                            " urn_status='" + "Yes" + "'," +
                            " urn='" + objHBAPICustomerResponse.customererp_id + "'" +
                            " where institution_gid='" + lsinstitution_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.addresslist)
                    {
                        msSQL = " update agr_mst_tsupronboardinstitution2address set " +
                      " erp_id='" + entry.Value + "'" +
                      " where institution2address_gid='" + entry.Key + "' ";

                        mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (objHBAPICustomerResponse.contactlist_generaldetails != null)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.contactlist_generaldetails)
                        {
                            msSQL = " update agr_mst_tsupronboardcontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where supronboardcontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    if (objHBAPICustomerResponse.contactlist_company != null)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.contactlist_company)
                        {
                            msSQL = " update agr_mst_tsupronboardinstitutioncontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where supronboardinstitutioncontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    LogForAuditHBAPI("Logging ended for Supplier with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                    objMdlHBAPIConnDAResponse.status = true;
                    objMdlHBAPIConnDAResponse.message = "Supplier posted to ERP successfully..!";
                }
                else
                {
                    msSQL = "delete from agr_mst_tsupronboardcontactdetails where application_gid='" + application_gid + "'"; //delete contacts added in contact table in case posting supplier to Netsuite ERP failed
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "delete from agr_mst_tsupronboardinstitutioncontactdetails where institution_gid='" + lsinstitution_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    LogForAuditHBAPI("Logging ended for Supplier with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);


                    objMdlHBAPIConnDAResponse.status = false;
                    objMdlHBAPIConnDAResponse.message = "Posting Supplier to ERP failed";
                    objMdlHBAPIConnDAResponse.error_response = objHBAPICustomerResponse.error_response;
                }

                
            }
            catch (Exception ex)
            {
                msSQL = "delete from agr_mst_tsupronboardcontactdetails where application_gid='" + application_gid + "'"; //delete contacts added in contact table in case posting supplier to Netsuite ERP failed
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from agr_mst_tsupronboardinstitutioncontactdetails where institution_gid='" + lsinstitution_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                LogForAuditHBAPI("Exception occured while posting Supplier", type);
                LogForAuditHBAPI(ex.ToString(), type);
                LogForAuditHBAPI("End of Exception", type);
                LogForAuditHBAPI("Logging ended for Supplier with Application GID -" + application_gid, type);

                objMdlHBAPIConnDAResponse.status = false;
                objMdlHBAPIConnDAResponse.message = "Exception occurred in Posting Supplier to ERP..!";
            }
            return objMdlHBAPIConnDAResponse;
        }

        public MdlHBAPIConnDAResponse PostSupplierContactHBAPI(string application_gid, string lsapprovalperson_name)
        {
            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();
            string type = "Supplier";
            try
            {
                LogForAuditHBAPI("Logging started for Supplier with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                msSQL = " select contact_gid from agr_mst_tsupronboardcontact" +
                    " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditHBAPI("Applicant GID obtained -" + lscontact_gid, type);

                MdlIndividualBuyerHBAPI objMdlBuyerHBAPI = new MdlIndividualBuyerHBAPI();

                objMdlBuyerHBAPI.addressDetails = new AddressDetails();
                objMdlBuyerHBAPI.addressDetails.addresslist = new AddressData[] { };
                objMdlBuyerHBAPI.contactPersonDetailsGeneral = new ContactPersonDetailsGeneral();
                objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[] { };
                objMdlBuyerHBAPI.contactPersonDetailsIndividual = new ContactPersonDetailsIndividual();
                objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist = new ContactPersonData[] { };


                msSQL = " select vertical_name, constitution_name,application_no from agr_mst_tsupronboard" +
                        " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    objMdlBuyerHBAPI.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    objMdlBuyerHBAPI.ExternalID = objODBCDatareader["application_no"].ToString();
                }
                objODBCDatareader.Close();


                msSQL = " select first_name,middle_name,last_name,aadhar_no" +
                        " from agr_mst_tsupronboardcontact where contact_gid='" + lscontact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.first_name = objODBCDatareader["first_name"].ToString();
                    objMdlBuyerHBAPI.middle_name = objODBCDatareader["middle_name"].ToString();
                    objMdlBuyerHBAPI.last_name = objODBCDatareader["last_name"].ToString();
                    objMdlBuyerHBAPI.aadhar_no = objODBCDatareader["aadhar_no"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tsupronboardcontact2email where " +
                         " contact_gid='" + lscontact_gid + "' and primary_status = 'Yes'";
                objMdlBuyerHBAPI.officialemail_address = objdbconn.GetExecuteScalar(msSQL);



                msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsupronboardcontact2mobileno where " +
                        " contact_gid='" + lscontact_gid + "' and primary_status = 'Yes'";
                objMdlBuyerHBAPI.official_telephoneno = objdbconn.GetExecuteScalar(msSQL);

                populateSupplierIndividualContactTable(application_gid); //Function to populate contacts in separate table

                msSQL = " select supronboardcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                        " from agr_mst_tsupronboardcontactdetails where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist.Length; i++)
                {
                    objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardcontactdetails_gid"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select supronboardindividualcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                       " from agr_mst_tsupronboardindividualcontactdetails where contact_gid='" + lscontact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist.Length; i++)
                {
                    objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardindividualcontactdetails_gid"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();



                msSQL = " select contact2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude" +
                        " from agr_mst_tsupronboardcontact2address where contact_gid='" + lscontact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.addressDetails.addresslist = new AddressData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.addressDetails.addresslist.Length; i++)
                {
                    objMdlBuyerHBAPI.addressDetails.addresslist[i] = new AddressData();

                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int addressind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addresstype_name = dr_datarow["addresstype_name"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addressline1 = dr_datarow["addressline1"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addressline2 = dr_datarow["addressline2"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].city = dr_datarow["city"].ToString();
                        //objMdlBuyerHBAPI.addressDetails.addresslist[addressind].state = dr_datarow["state"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["contact2address_gid"].ToString();
                        msSQL = "select ns_state_code from agr_mst_tnsstatemst where addr_state_name = '" + dr_datarow["state"].ToString() + "'";
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].state = objdbconn.GetExecuteScalar(msSQL);
                        addressind++;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select bankaccount_number,bankaccount_name,ifsc_code,bank_name,micr_code,bank_address,branch_name" +
                        " from agr_mst_tsupronboardcontact2bankdtl where contact_gid='" + lscontact_gid + "' and primary_status = 'Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    objMdlBuyerHBAPI.accountholder_name = objODBCDatareader["bankaccount_name"].ToString();
                    objMdlBuyerHBAPI.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objMdlBuyerHBAPI.bank_name = objODBCDatareader["bank_name"].ToString();
                    objMdlBuyerHBAPI.micr_code = objODBCDatareader["micr_code"].ToString();
                    objMdlBuyerHBAPI.bank_address = objODBCDatareader["bank_address"].ToString();
                    objMdlBuyerHBAPI.branch_name = objODBCDatareader["branch_name"].ToString();
                }
                objODBCDatareader.Close();

                LogForAuditHBAPI("Buyer data obtained from Database", type);

                string BuyerHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerHBAPI);

                LogForAuditHBAPI("JSON generated from Supplier data", type);
                LogForAuditHBAPI(BuyerHBAPIJSON, type);
                LogForAuditHBAPI("End of Supplier JSON", type);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls; //SSL Protocol Version Update

                HBAPICustomerResponse objHBAPICustomerResponse = new HBAPICustomerResponse();

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIPostURL"].ToString() + HBPostAPINameRepo.NetSuiteAPIDTrnPostIndividualSupplier);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", BuyerHBAPIJSON);
                request.AddParameter("approvalPersonName", lsapprovalperson_name);

                IRestResponse response = client.Execute(request);

                objHBAPICustomerResponse = JsonConvert.DeserializeObject<HBAPICustomerResponse>(response.Content);

                LogForAuditHBAPI("Response obtained from HyperbridgeAPI", type);
                LogForAuditHBAPI2(response, type);
                LogForAuditHBAPI(response.Content, type);
                LogForAuditHBAPI("End of Response", type);

                if (objHBAPICustomerResponse.customererp_id != null && objHBAPICustomerResponse.customererp_id != "")
                {
                    msSQL = " update agr_mst_tsupronboardcontact set " +
                             " urn_status='" + "Yes" + "'," +
                             " urn='" + objHBAPICustomerResponse.customererp_id + "'" +
                             " where contact_gid='" + lscontact_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.addresslist)
                    {
                        msSQL = " update agr_mst_tsupronboardcontact2address set " +
                      " erp_id='" + entry.Value + "'" +
                      " where contact2address_gid='" + entry.Key + "' ";

                        mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (objHBAPICustomerResponse.contactlist_generaldetails != null)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.contactlist_generaldetails)
                        {
                            msSQL = " update agr_mst_tsupronboardcontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where byronboardcontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    if (objHBAPICustomerResponse.contactlist_company != null)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.contactlist_company)
                        {
                            msSQL = " update agr_mst_tsupronboardindividualcontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where byronboardindividualcontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    LogForAuditHBAPI("Logging ended for Supplier with Application GID -" + application_gid, type);

                    objMdlHBAPIConnDAResponse.status = true;
                    objMdlHBAPIConnDAResponse.message = "Supplier posted to ERP successfully..!";
                }
                else
                {
                    msSQL = "delete from agr_mst_tsupronboardcontactdetails where application_gid='" + application_gid + "'"; //delete contacts added in contact table in case posting supplier to Netsuite ERP failed
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "delete from agr_mst_tsupronboardindividualcontactdetails where contact_gid='" + lscontact_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    LogForAuditHBAPI("Logging ended for Supplier with Application GID -" + application_gid, type);

                    objMdlHBAPIConnDAResponse.status = false;
                    objMdlHBAPIConnDAResponse.message = "Posting Supplier to ERP failed";
                    objMdlHBAPIConnDAResponse.error_response = objHBAPICustomerResponse.error_response;
                }

            }
            catch (Exception ex)
            {
                msSQL = "delete from agr_mst_tsupronboardcontactdetails where application_gid='" + application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from agr_mst_tsupronboardindividualcontactdetails where contact_gid='" + lscontact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                LogForAuditHBAPI("Exception occured while posting Supplier", type);
                LogForAuditHBAPI(ex.ToString(), type);
                LogForAuditHBAPI("End of Exception", type);
                LogForAuditHBAPI("Logging ended for Supplier with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                objMdlHBAPIConnDAResponse.status = false;
                objMdlHBAPIConnDAResponse.message = "Exception occurred in Posting Supplier to ERP..!";
            }
            return objMdlHBAPIConnDAResponse;
        }

        public MdlHBAPIConnDAResponse PostBuyerInstitutionHBAPI(string application_gid, string lsapprovalperson_name)
        {
            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();
            string type = "Buyer";
            try
            {
                LogForAuditHBAPI("Logging started for Buyer with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                msSQL = " select institution_gid from agr_mst_tbyronboard2institution" +
                    " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditHBAPI("Applicant GID obtained -" + lsinstitution_gid, type);

                MdlBuyerHBAPI objMdlBuyerHBAPI = new MdlBuyerHBAPI();

                objMdlBuyerHBAPI.gstDetails = new GSTDetails();
                objMdlBuyerHBAPI.addressDetails = new AddressDetails();

                objMdlBuyerHBAPI.gstDetails.gstlist = new GSTData[] { };
                objMdlBuyerHBAPI.addressDetails.addresslist = new AddressData[] { };
                objMdlBuyerHBAPI.contactPersonDetailsGeneral = new ContactPersonDetailsGeneral();
                objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[] { };
                objMdlBuyerHBAPI.contactPersonDetailsCompany = new ContactPersonDetailsCompany();
                objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[] { };


                msSQL = " select customerref_name,vertical_name, constitution_name,application_no,virtualaccount_number,customerbank_name,branch_name,ifsc_code,lgltag_status from agr_mst_tbyronboard" +
                        " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.company_name = objODBCDatareader["customerref_name"].ToString();
                    objMdlBuyerHBAPI.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    objMdlBuyerHBAPI.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    objMdlBuyerHBAPI.ExternalID = objODBCDatareader["application_no"].ToString();
                    objMdlBuyerHBAPI.virtualaccount_number = objODBCDatareader["virtualaccount_number"].ToString();
                    objMdlBuyerHBAPI.virtualcustomerbank_name = objODBCDatareader["customerbank_name"].ToString();
                    objMdlBuyerHBAPI.virtualbranch_name = objODBCDatareader["branch_name"].ToString();
                    objMdlBuyerHBAPI.virtualifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objMdlBuyerHBAPI.lgltag_status = objODBCDatareader["lgltag_status"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select created_by from agr_mst_tbyronboard where application_gid = '" + application_gid + "'";
                lsrelationshipmanager_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select erp_status from hrm_mst_temployee where employee_gid = '" + lsrelationshipmanager_gid + "'";
                lserp_status = objdbconn.GetExecuteScalar(msSQL);

                if (lserp_status == "No")
                {
                    objMdlHBAPIConnDAResponse.status = false;
                    objMdlHBAPIConnDAResponse.message = "RM of this application hasn't been posted to ERP yet. Since he can't be assigned as Sales Rep in ERP, posting Buyer to ERP failed.";
                    return objMdlHBAPIConnDAResponse;
                } 
                else
                {
                    msSQL = " select erp_id from hrm_mst_temployee where employee_gid = '" + lsrelationshipmanager_gid + "'";
                    objMdlBuyerHBAPI.rm_erpid = objdbconn.GetExecuteScalar(msSQL);
                }


                msSQL = " select officialemail_address,official_telephoneno,company_name,companypan_no,companytype_gid,tan_number,incometax_returnsstatus," +
                        " lastyear_turnover,contactperson_firstname,contactperson_middlename,contactperson_lastname,designation" +
                        " from agr_mst_tbyronboard2institution where institution_gid='" + lsinstitution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.officialemail_address = objODBCDatareader["officialemail_address"].ToString();
                    objMdlBuyerHBAPI.official_telephoneno = objODBCDatareader["official_telephoneno"].ToString();
                    objMdlBuyerHBAPI.companypan_no = objODBCDatareader["companypan_no"].ToString();
                    objMdlBuyerHBAPI.companytype_name = fetchCompanyTypeName(objODBCDatareader["companytype_gid"].ToString());
                    objMdlBuyerHBAPI.tan_number = objODBCDatareader["tan_number"].ToString();
                    objMdlBuyerHBAPI.incometax_returnsstatus = objODBCDatareader["incometax_returnsstatus"].ToString();
                    objMdlBuyerHBAPI.lastyear_turnover = objODBCDatareader["lastyear_turnover"].ToString();
                    
                }
                objODBCDatareader.Close();

                msSQL = " select licensetype_name,license_no" +
                        " from agr_mst_tbyronboardinstitution2licensedtl where institution_gid='" + lsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (dr_datarow["licensetype_name"].ToString() == "FSSAI")
                            objMdlBuyerHBAPI.fssai_licenseno = dr_datarow["license_no"].ToString();
                        else if (dr_datarow["licensetype_name"].ToString() == "APMC")
                            objMdlBuyerHBAPI.apmc_licenseno = dr_datarow["license_no"].ToString();
                        else if (dr_datarow["licensetype_name"].ToString() == "IEC")
                            objMdlBuyerHBAPI.iec_licenseno = dr_datarow["license_no"].ToString();
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select gst_no from agr_mst_tbyronboardinstitution2branch where headoffice_status = 'Yes' and institution_gid = '" + lsinstitution_gid + "'";
                objMdlBuyerHBAPI.primary_gst = objdbconn.GetExecuteScalar(msSQL);

                populateBuyerInstitutionContactTable(application_gid); //Function to populate contacts in separate table

                msSQL = " select byronboardcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                        " from agr_mst_tbyronboardcontactdetails where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist.Length; i++)
                {
                    objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardcontactdetails_gid"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select byronboardinstitutioncontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                       " from agr_mst_tbyronboardinstitutioncontactdetails where institution_gid='" + lsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist.Length; i++)
                {
                    objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardinstitutioncontactdetails_gid"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsCompany.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();



                msSQL = " select institution2branch_gid,gst_no,gst_state" +
                        " from agr_mst_tbyronboardinstitution2branch where institution_gid='" + lsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                objMdlBuyerHBAPI.gstDetails.gstlist = new GSTData[dt_datatable.Rows.Count];
                for (int i = 0; i < objMdlBuyerHBAPI.gstDetails.gstlist.Length; i++)
                {
                    objMdlBuyerHBAPI.gstDetails.gstlist[i] = new GSTData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int gstind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.gstDetails.gstlist[gstind].gst_no = dr_datarow["gst_no"].ToString();
                        objMdlBuyerHBAPI.gstDetails.gstlist[gstind].externalid = dr_datarow["institution2branch_gid"].ToString();
                        objMdlBuyerHBAPI.gstDetails.gstlist[gstind].gst_state = fetchNsGSTStateSode(dr_datarow["gst_state"].ToString());
                        gstind++;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select institution2address_gid,addresstype_name,addressline1,addressline2,city,state,country,postal_code,latitude,longitude" +
                        " from agr_mst_tbyronboardinstitution2address where institution_gid='" + lsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.addressDetails.addresslist = new AddressData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.addressDetails.addresslist.Length; i++)
                {
                    objMdlBuyerHBAPI.addressDetails.addresslist[i] = new AddressData();

                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int addressind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addresstype_name = dr_datarow["addresstype_name"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addressline1 = dr_datarow["addressline1"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addressline2 = dr_datarow["addressline2"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].city = dr_datarow["city"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["institution2address_gid"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].state = fetchNsAddrStateCode(dr_datarow["state"].ToString());
                        addressind++;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select bankaccount_number,bankaccount_name,ifsc_code,bank_name,micr_code,bank_address,branch_name" +
                        " from agr_mst_tbyronboardinstitution2bankdtl where institution_gid='" + lsinstitution_gid + "' and primary_status = 'Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    objMdlBuyerHBAPI.accountholder_name = objODBCDatareader["bankaccount_name"].ToString();
                    objMdlBuyerHBAPI.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objMdlBuyerHBAPI.bank_name = objODBCDatareader["bank_name"].ToString();
                    objMdlBuyerHBAPI.micr_code = objODBCDatareader["micr_code"].ToString();
                    objMdlBuyerHBAPI.bank_address = objODBCDatareader["bank_address"].ToString();
                    objMdlBuyerHBAPI.branch_name = objODBCDatareader["branch_name"].ToString();
                }
                objODBCDatareader.Close();

                LogForAuditHBAPI("Buyer data obtained from Database", type);

                string BuyerHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerHBAPI);

                LogForAuditHBAPI("JSON generated from Buyer data", type);
                LogForAuditHBAPI(BuyerHBAPIJSON, type);
                LogForAuditHBAPI("End of Buyer JSON", type);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls; //SSL Protocol Version Update

                HBAPICustomerResponse objHBAPICustomerResponse = new HBAPICustomerResponse();

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIPostURL"].ToString() + HBPostAPINameRepo.NetSuiteAPIDTrnPostCustomer);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", BuyerHBAPIJSON);
                request.AddParameter("approvalPersonName", lsapprovalperson_name);

                IRestResponse response = client.Execute(request);

                objHBAPICustomerResponse = JsonConvert.DeserializeObject<HBAPICustomerResponse>(response.Content);

                LogForAuditHBAPI("Response obtained from HyperbridgeAPI", type);
                LogForAuditHBAPI2(response, type);
                LogForAuditHBAPI(response.Content, type);
                LogForAuditHBAPI("End of Response", type);

                if (objHBAPICustomerResponse.customererp_id != null && objHBAPICustomerResponse.customererp_id != "")
                {
                    msSQL = " update agr_mst_tbyronboard2institution set " +
                            " urn_status='" + "Yes" + "'," +
                            " urn='" + objHBAPICustomerResponse.customererp_id + "'" +
                            " where institution_gid='" + lsinstitution_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.addresslist)
                    {
                        msSQL = " update agr_mst_tbyronboardinstitution2address set " +
                      " erp_id='" + entry.Value + "'" +
                      " where institution2address_gid='" + entry.Key + "' ";

                        mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (objHBAPICustomerResponse.contactlist_generaldetails != null)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.contactlist_generaldetails)
                        {
                            msSQL = " update agr_mst_tbyronboardcontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where byronboardcontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    if (objHBAPICustomerResponse.contactlist_company != null)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.contactlist_company)
                        {
                            msSQL = " update agr_mst_tbyronboardinstitutioncontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where byronboardinstitutioncontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    LogForAuditHBAPI("Logging ended for Buyer with Application GID -" + application_gid, type);

                    objMdlHBAPIConnDAResponse.status = true;
                    objMdlHBAPIConnDAResponse.message = "Buyer posted to ERP successfully..!";
                }
                else
                {
                    msSQL = "delete from agr_mst_tbyronboardcontactdetails where application_gid='" + application_gid + "'"; //delete contacts added in contact table in case posting buyer to Netsuite ERP failed
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "delete from agr_mst_tbyronboardinstitutioncontactdetails where institution_gid='" + lsinstitution_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    LogForAuditHBAPI("Logging ended for Buyer with Application GID -" + application_gid, type);
                    objMdlHBAPIConnDAResponse.status = false;
                    objMdlHBAPIConnDAResponse.message = "Posting Buyer to ERP failed";
                    objMdlHBAPIConnDAResponse.error_response = objHBAPICustomerResponse.error_response;

                }

                
            }
            catch (Exception ex)
            {
                msSQL = "delete from agr_mst_tbyronboardcontactdetails where application_gid='" + application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from agr_mst_tbyronboardinstitutioncontactdetails where institution_gid='" + lsinstitution_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                LogForAuditHBAPI("Exception occured while posting Buyer", type);
                LogForAuditHBAPI(ex.ToString(), type);
                LogForAuditHBAPI("End of Exception", type);
                LogForAuditHBAPI("Logging ended for Buyer with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                objMdlHBAPIConnDAResponse.status = false;
                objMdlHBAPIConnDAResponse.message = "Exception occurred in Posting Buyer to ERP..!";
            }
            return objMdlHBAPIConnDAResponse;
        }

        public MdlHBAPIConnDAResponse PostBuyerContactHBAPI(string application_gid, string lsapprovalperson_name)
        {
            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();
            string type = "Buyer";
            try
            {
                LogForAuditHBAPI("Logging started for Buyer with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                msSQL = " select contact_gid from agr_mst_tbyronboardcontact" +
                    " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                LogForAuditHBAPI("Applicant GID obtained -" + lscontact_gid, type);

                MdlIndividualBuyerHBAPI objMdlBuyerHBAPI = new MdlIndividualBuyerHBAPI();

                objMdlBuyerHBAPI.addressDetails = new AddressDetails();
                objMdlBuyerHBAPI.addressDetails.addresslist = new AddressData[] { };
                objMdlBuyerHBAPI.contactPersonDetailsGeneral = new ContactPersonDetailsGeneral();
                objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[] { };
                objMdlBuyerHBAPI.contactPersonDetailsIndividual = new ContactPersonDetailsIndividual();
                objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist = new ContactPersonData[] { };


                msSQL = " select vertical_name, constitution_name,application_no,virtualaccount_number,customerbank_name,branch_name,ifsc_code from agr_mst_tbyronboard" +
                        " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    objMdlBuyerHBAPI.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    objMdlBuyerHBAPI.ExternalID = objODBCDatareader["application_no"].ToString();
                    objMdlBuyerHBAPI.virtualaccount_number = objODBCDatareader["virtualaccount_number"].ToString();
                    objMdlBuyerHBAPI.virtualcustomerbank_name = objODBCDatareader["customerbank_name"].ToString();
                    objMdlBuyerHBAPI.virtualbranch_name = objODBCDatareader["branch_name"].ToString();
                    objMdlBuyerHBAPI.virtualifsc_code = objODBCDatareader["ifsc_code"].ToString();
                }
                objODBCDatareader.Close();


                msSQL = " select first_name,middle_name,last_name,aadhar_no" +
                        " from agr_mst_tbyronboardcontact where contact_gid='" + lscontact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.first_name = objODBCDatareader["first_name"].ToString();
                    objMdlBuyerHBAPI.middle_name = objODBCDatareader["middle_name"].ToString();
                    objMdlBuyerHBAPI.last_name = objODBCDatareader["last_name"].ToString();
                    objMdlBuyerHBAPI.aadhar_no = objODBCDatareader["aadhar_no"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tbyronboardcontact2mobileno where " +
                       " contact_gid='" + lscontact_gid + "' and primary_status = 'Yes'";
                objMdlBuyerHBAPI.official_telephoneno = objdbconn.GetExecuteScalar(msSQL);



                msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tbyronboardcontact2email where " +
                          " contact_gid='" + lscontact_gid + "' and primary_status = 'Yes'";
                objMdlBuyerHBAPI.officialemail_address = objdbconn.GetExecuteScalar(msSQL);

                populateBuyerIndividualContactTable(application_gid); //Function to populate contacts in separate table

                msSQL = " select byronboardcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                        " from agr_mst_tbyronboardcontactdetails where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist.Length; i++)
                {
                    objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardcontactdetails_gid"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsGeneral.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select byronboardindividualcontactdetails_gid,first_name,middle_name,last_name,designation,mobileno,email" +
                       " from agr_mst_tbyronboardindividualcontactdetails where contact_gid='" + lscontact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist = new ContactPersonData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist.Length; i++)
                {
                    objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist[i] = new ContactPersonData();
                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int contactpersonind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactdetails_gid = dr_datarow["byronboardindividualcontactdetails_gid"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_firstname = dr_datarow["first_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_middlename = dr_datarow["middle_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].contactperson_lastname = dr_datarow["last_name"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].designation = dr_datarow["designation"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].mobile_no = dr_datarow["mobileno"].ToString();
                        objMdlBuyerHBAPI.contactPersonDetailsIndividual.contactpersonlist[contactpersonind].email_address = dr_datarow["email"].ToString();

                        contactpersonind++;
                    }
                }
                dt_datatable.Dispose();



                msSQL = " select contact2address_gid,addresstype_name,addressline1,addressline2,city,state,postal_code,latitude,longitude" +
                        " from agr_mst_tbyronboardcontact2address where contact_gid='" + lscontact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                objMdlBuyerHBAPI.addressDetails.addresslist = new AddressData[dt_datatable.Rows.Count];

                for (int i = 0; i < objMdlBuyerHBAPI.addressDetails.addresslist.Length; i++)
                {
                    objMdlBuyerHBAPI.addressDetails.addresslist[i] = new AddressData();

                }
                if (dt_datatable.Rows.Count != 0)
                {
                    int addressind = 0;
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addresstype_name = dr_datarow["addresstype_name"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addressline1 = dr_datarow["addressline1"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].addressline2 = dr_datarow["addressline2"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].city = dr_datarow["city"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].postal_code = dr_datarow["postal_code"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].latitude = dr_datarow["latitude"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].longitude = dr_datarow["longitude"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].externalid = dr_datarow["contact2address_gid"].ToString();
                        objMdlBuyerHBAPI.addressDetails.addresslist[addressind].state = fetchNsAddrStateCode(dr_datarow["state"].ToString());
                        addressind++;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select bankaccount_number,bankaccount_name,ifsc_code,bank_name,micr_code,bank_address,branch_name" +
                        " from agr_mst_tbyronboardcontact2bankdtl where contact_gid='" + lscontact_gid + "' and primary_status = 'Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objMdlBuyerHBAPI.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    objMdlBuyerHBAPI.accountholder_name = objODBCDatareader["bankaccount_name"].ToString();
                    objMdlBuyerHBAPI.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    objMdlBuyerHBAPI.bank_name = objODBCDatareader["bank_name"].ToString();
                    objMdlBuyerHBAPI.micr_code = objODBCDatareader["micr_code"].ToString();
                    objMdlBuyerHBAPI.bank_address = objODBCDatareader["bank_address"].ToString();
                    objMdlBuyerHBAPI.branch_name = objODBCDatareader["branch_name"].ToString();
                }
                objODBCDatareader.Close();

                LogForAuditHBAPI("Buyer data obtained from Database", type);

                string BuyerHBAPIJSON = JsonConvert.SerializeObject(objMdlBuyerHBAPI);

                LogForAuditHBAPI("JSON generated from Buyer data", type);
                LogForAuditHBAPI(BuyerHBAPIJSON, type);
                LogForAuditHBAPI("End of Buyer JSON", type);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls; //SSL Protocol Version Update

                HBAPICustomerResponse objHBAPICustomerResponse = new HBAPICustomerResponse();

                var client = new RestClient(ConfigurationManager.AppSettings["HBAPIPostURL"].ToString() + HBPostAPINameRepo.NetSuiteAPIDTrnPostIndividualCustomer);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("HyperBridge_APIkey", ConfigurationManager.AppSettings["HBAPIKey"].ToString());
                request.AddParameter("fromsamagroJSON", BuyerHBAPIJSON);
                request.AddParameter("approvalPersonName", lsapprovalperson_name);

                IRestResponse response = client.Execute(request);

                objHBAPICustomerResponse = JsonConvert.DeserializeObject<HBAPICustomerResponse>(response.Content);

                LogForAuditHBAPI("Response obtained from HyperbridgeAPI", type);
                LogForAuditHBAPI2(response, type);
                LogForAuditHBAPI(response.Content, type);
                LogForAuditHBAPI("End of Response", type);

                if (objHBAPICustomerResponse.customererp_id != null && objHBAPICustomerResponse.customererp_id != "")
                {
                    msSQL = " update agr_mst_tbyronboardcontact set " +
                             " urn_status='" + "Yes" + "'," +
                             " urn='" + objHBAPICustomerResponse.customererp_id + "'" +
                             " where contact_gid='" + lscontact_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.addresslist)
                    {
                        msSQL = " update agr_mst_tbyronboardcontact2address set " +
                      " erp_id='" + entry.Value + "'" +
                      " where contact2address_gid='" + entry.Key + "' ";

                        mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (objHBAPICustomerResponse.contactlist_generaldetails != null)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.contactlist_generaldetails)
                        {
                            msSQL = " update agr_mst_tbyronboardcontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where byronboardcontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    if (objHBAPICustomerResponse.contactlist_company != null)
                    {
                        foreach (KeyValuePair<string, string> entry in objHBAPICustomerResponse.contactlist_company)
                        {
                            msSQL = " update agr_mst_tbyronboardindividualcontactdetails set " +
                          " erp_id='" + entry.Value + "'" +
                          " where byronboardindividualcontactdetails_gid='" + entry.Key + "' ";

                            mnResultContact = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    LogForAuditHBAPI("Logging ended for Buyer with Application GID -" + application_gid, type);

                    objMdlHBAPIConnDAResponse.status = true;
                    objMdlHBAPIConnDAResponse.message = "Buyer posted to ERP successfully..!";
                }
                else
                {
                    msSQL = "delete from agr_mst_tbyronboardcontactdetails where application_gid='" + application_gid + "'"; //delete contacts added in contact table in case posting buyer to Netsuite ERP failed
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "delete from agr_mst_tbyronboardindividualcontactdetails where contact_gid='" + lscontact_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    LogForAuditHBAPI("Logging ended for Buyer with Application GID -" + application_gid, type);

                    objMdlHBAPIConnDAResponse.status = false;
                    objMdlHBAPIConnDAResponse.message = "Posting Buyer to ERP failed";
                    objMdlHBAPIConnDAResponse.error_response = objHBAPICustomerResponse.error_response;
                }

            }
            catch (Exception ex)
            {
                msSQL = "delete from agr_mst_tbyronboardcontactdetails where application_gid='" + application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from agr_mst_tbyronboardindividualcontactdetails where contact_gid='" + lscontact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                LogForAuditHBAPI("Exception occured while posting Buyer", type);
                LogForAuditHBAPI(ex.ToString(), type);
                LogForAuditHBAPI("End of Exception", type);
                LogForAuditHBAPI("Logging ended for Buyer with Application GID -" + application_gid + "at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                objMdlHBAPIConnDAResponse.status = false;
                objMdlHBAPIConnDAResponse.message = "Exception occurred in Posting Buyer to ERP..!";
            }
            return objMdlHBAPIConnDAResponse;
        }

        public void nsEmpEntityUpdate(string user_gid, string updated_by, string entity_gid, string user_code)
        {
            msSQL = "select employee_externalid, created_by, created_date, erp_id from hrm_mst_temployee where user_gid = '" + user_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                hbemployee_externalid = objODBCDatareader["employee_externalid"].ToString();
                hbcreated_by = objODBCDatareader["created_by"].ToString();
                hbcreated_date = objODBCDatareader["created_date"].ToString();
                hbemployee_erpid = objODBCDatareader["erp_id"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = "select entity_name from adm_mst_tentity where entity_gid = '" + entity_gid + "'";
            hbentity_name = objdbconn.GetExecuteScalar(msSQL);

            employee_externalid = objcmnfunctions.GetMasterGID("EECL");
            msSQL = "insert into `sys_trn_temployeeentitychangelog` " +
                        "(`employeeentitychangelog_gid`, " +
                        "`employee_externalid`, " +
                        "`employee_erpid`, " +
                        "`entity_gid`, " +
                        "`entity_name`, " +
                        "`created_by`, " +
                        "`user_code`, " +
                        //"`created_date`, " +
                        "`updated_by`, " +
                        "`updated_date`) " +
                        " VALUES( " +
                        "'" + employee_externalid + "'," +
                        "'" + hbemployee_externalid + "'," +
                        "'" + hbemployee_erpid + "'," +
                        "'" + entity_gid + "'," +
                        "'" + hbentity_name + "'," +
                        "'" + hbcreated_by + "'," +
                        "'" + user_code + "'," +
                        //"'" + hbcreated_date + "'," +
                        "'" + updated_by + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            
            msSQL = " update hrm_mst_temployee set employee_externalid ='" + employee_externalid + "'" +
                    " where user_gid='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            HBAPIEmployeeResponse objHBAPIEmployeeResponse = new HBAPIEmployeeResponse();
            MdlHBAPIConnDAResponse objNsEmpEntityUpdateresponse = PostEmployeeHBAPI(employee_externalid);
            string postEmpResponse = JsonConvert.SerializeObject(objNsEmpEntityUpdateresponse);
            objHBAPIEmployeeResponse = JsonConvert.DeserializeObject<HBAPIEmployeeResponse>(postEmpResponse);

            if(objHBAPIEmployeeResponse.status == true)
            {
                LogForAuditHBAPI("Entity Update Successful", "Employee");
            }
            else
            {
                msSQL = " update hrm_mst_temployee set employee_entitychange_flag ='N'" +
                        " where employee_externalid='" + employee_externalid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                LogForAuditHBAPI("Entity Update Failed due to :" + objHBAPIEmployeeResponse.error_response, "Employee");
            }
        }

        //Auxillary Flow Functions

        public void populateSupplierInstitutionContactTable(string application_gid)
        {
            //Application Contact
            msSQL = " select application_gid,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_type" +
                  " from agr_mst_tsupronboard where application_gid='" + application_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lscontactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                lscontactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                lscontactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                lsdesignation = objODBCDatareader["designation_type"].ToString();
            }
            objODBCDatareader.Close();



            msSQL = " select primary_mobileno,mobile_no" +
                     " from agr_mst_tsupronboard2contactno where application_gid='" + application_gid + "'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            msSQL = " select email_address,primary_emailaddress" +
                    " from agr_mst_tsupronboard2email where application_gid='" + application_gid + "'";
            dt_email = objdbconn.GetDataTable(msSQL);


            arrContactDetails = dt_email.Rows.Count > dt_mobileno.Rows.Count ? new ContactDetails[dt_email.Rows.Count] : new ContactDetails[dt_mobileno.Rows.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }


            msSQL = " select application2contact_gid,mobile_no" +
                 " from agr_mst_tsupronboard2contactno where application_gid='" + application_gid + "' and primary_mobileno = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].mobileno_gid = objODBCDatareader["application2contact_gid"].ToString();
                arrContactDetails[0].mobileno = objODBCDatareader["mobile_no"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select application2email_gid,email_address" +
                " from agr_mst_tsupronboard2email where application_gid='" + application_gid + "' and primary_emailaddress = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            arrContactDetails[0].email = objdbconn.GetExecuteScalar(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].email_gid = objODBCDatareader["application2email_gid"].ToString();
                arrContactDetails[0].email = objODBCDatareader["email_address"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails[0].primary_status = "Yes";


            msSQL = " select application2contact_gid,primary_mobileno,mobile_no" +
               " from agr_mst_tsupronboard2contactno where application_gid='" + application_gid + "' and primary_mobileno = 'No'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            int mobind = 1;
            foreach (DataRow dr_mobileno in dt_mobileno.Rows)
            {
                arrContactDetails[mobind].mobileno_gid = dr_mobileno["application2contact_gid"].ToString();
                arrContactDetails[mobind].mobileno = dr_mobileno["mobile_no"].ToString();
                arrContactDetails[mobind].primary_status = dr_mobileno["primary_mobileno"].ToString();

                mobind++;
            }

            msSQL = " select application2email_gid,email_address,primary_emailaddress" +
                   " from agr_mst_tsupronboard2email where application_gid='" + application_gid + "'  and primary_emailaddress = 'No'";
            dt_email = objdbconn.GetDataTable(msSQL);

            int emailind = 1;
            foreach (DataRow dr_email in dt_email.Rows)
            {
                arrContactDetails[emailind].email_gid = dr_email["application2email_gid"].ToString();
                arrContactDetails[emailind].email = dr_email["email_address"].ToString();
                arrContactDetails[emailind].primary_status = dr_email["primary_emailaddress"].ToString();

                emailind++;
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

            //Institution Contact 

            msSQL = " select institution_gid" +
                  " from agr_mst_tsupronboard2institution where application_gid='" + application_gid + "' and stakeholder_type='Applicant'";
            lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

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

            msSQL = " select primary_status,mobile_no" +
                     " from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + lsinstitution_gid + "'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            msSQL = " select primary_status,email_address" +
                    " from agr_mst_tsupronboardinstitution2email where institution_gid='" + lsinstitution_gid + "'";
            dt_email = objdbconn.GetDataTable(msSQL);

            arrContactDetails = dt_email.Rows.Count > dt_mobileno.Rows.Count ? new ContactDetails[dt_email.Rows.Count] : new ContactDetails[dt_mobileno.Rows.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }

            msSQL = " select institution2mobileno_gid,mobile_no" +
                     " from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + lsinstitution_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
                arrContactDetails[0].mobileno = objODBCDatareader["mobile_no"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select institution2email_gid,email_address" +
                " from agr_mst_tsupronboardinstitution2email where institution_gid='" + lsinstitution_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            arrContactDetails[0].email = objdbconn.GetExecuteScalar(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].email_gid = objODBCDatareader["institution2email_gid"].ToString();
                arrContactDetails[0].email = objODBCDatareader["email_address"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails[0].primary_status = "Yes";

            msSQL = " select institution2mobileno_gid,primary_status,mobile_no" +
                   " from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + lsinstitution_gid + "' and primary_status = 'No'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            int mobindI = 1;
            foreach (DataRow dr_mobileno in dt_mobileno.Rows)
            {
                arrContactDetails[mobindI].mobileno_gid = dr_mobileno["institution2mobileno_gid"].ToString();
                arrContactDetails[mobindI].mobileno = dr_mobileno["mobile_no"].ToString();
                arrContactDetails[mobindI].primary_status = dr_mobileno["primary_status"].ToString();

                mobindI++;
            }

            msSQL = " select institution2email_gid,email_address,primary_status" +
                    " from agr_mst_tsupronboardinstitution2email where institution_gid='" + lsinstitution_gid + "'  and primary_status = 'No'";
            dt_email = objdbconn.GetDataTable(msSQL);

            int emailindI = 1;
            foreach (DataRow dr_email in dt_email.Rows)
            {
                arrContactDetails[emailindI].email_gid = dr_email["institution2email_gid"].ToString();
                arrContactDetails[emailindI].email = dr_email["email_address"].ToString();
                arrContactDetails[emailindI].primary_status = dr_email["primary_status"].ToString();

                emailindI++;
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

        public void populateSupplierIndividualContactTable(string application_gid)
        {
            //Application Contact
            msSQL = " select application_gid,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_type" +
                  " from agr_mst_tsupronboard where application_gid='" + application_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lscontactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                lscontactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                lscontactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                lsdesignation = objODBCDatareader["designation_type"].ToString();
            }
            objODBCDatareader.Close();



            msSQL = " select primary_mobileno,mobile_no" +
                     " from agr_mst_tsupronboard2contactno where application_gid='" + application_gid + "'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            msSQL = " select email_address,primary_emailaddress" +
                    " from agr_mst_tsupronboard2email where application_gid='" + application_gid + "'";
            dt_email = objdbconn.GetDataTable(msSQL);


            arrContactDetails = dt_email.Rows.Count > dt_mobileno.Rows.Count ? new ContactDetails[dt_email.Rows.Count] : new ContactDetails[dt_mobileno.Rows.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }


            msSQL = " select application2contact_gid,mobile_no" +
                 " from agr_mst_tsupronboard2contactno where application_gid='" + application_gid + "' and primary_mobileno = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].mobileno_gid = objODBCDatareader["application2contact_gid"].ToString();
                arrContactDetails[0].mobileno = objODBCDatareader["mobile_no"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select application2email_gid,email_address" +
                " from agr_mst_tsupronboard2email where application_gid='" + application_gid + "' and primary_emailaddress = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            arrContactDetails[0].email = objdbconn.GetExecuteScalar(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].email_gid = objODBCDatareader["application2email_gid"].ToString();
                arrContactDetails[0].email = objODBCDatareader["email_address"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails[0].primary_status = "Yes";


            msSQL = " select application2contact_gid,primary_mobileno,mobile_no" +
               " from agr_mst_tsupronboard2contactno where application_gid='" + application_gid + "' and primary_mobileno = 'No'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            int mobind = 1;
            foreach (DataRow dr_mobileno in dt_mobileno.Rows)
            {
                arrContactDetails[mobind].mobileno_gid = dr_mobileno["application2contact_gid"].ToString();
                arrContactDetails[mobind].mobileno = dr_mobileno["mobile_no"].ToString();
                arrContactDetails[mobind].primary_status = dr_mobileno["primary_mobileno"].ToString();

                mobind++;
            }

            msSQL = " select application2email_gid,email_address,primary_emailaddress" +
                   " from agr_mst_tsupronboard2email where application_gid='" + application_gid + "'  and primary_emailaddress = 'No'";
            dt_email = objdbconn.GetDataTable(msSQL);

            int emailind = 1;
            foreach (DataRow dr_email in dt_email.Rows)
            {
                arrContactDetails[emailind].email_gid = dr_email["application2email_gid"].ToString();
                arrContactDetails[emailind].email = dr_email["email_address"].ToString();
                arrContactDetails[emailind].primary_status = dr_email["primary_emailaddress"].ToString();

                emailind++;
            }



            foreach (var item in arrContactDetails)
            {
                mscontactdetailsGID = objcmnfunctions.GetMasterGID("BRCT");
                msSQL = " insert into agr_mst_tsupronboardcontactdetails(" +
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

            //Individual Contact 

            msSQL = " select contact_gid" +
                  " from agr_mst_tsupronboardcontact where application_gid='" + application_gid + "' and stakeholder_type='Applicant'";
            lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

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

            msSQL = " select primary_status,mobile_no" +
                     " from agr_mst_tsupronboardcontact2mobileno where contact_gid='" + lscontact_gid + "'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            msSQL = " select primary_status,email_address" +
                    " from agr_mst_tsupronboardcontact2email where contact_gid='" + lscontact_gid + "'";
            dt_email = objdbconn.GetDataTable(msSQL);

            arrContactDetails = dt_email.Rows.Count > dt_mobileno.Rows.Count ? new ContactDetails[dt_email.Rows.Count] : new ContactDetails[dt_mobileno.Rows.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }

            msSQL = " select contact2mobileno_gid,mobile_no" +
                     " from agr_mst_tsupronboardcontact2mobileno where contact_gid='" + lscontact_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].mobileno_gid = objODBCDatareader["contact2mobileno_gid"].ToString();
                arrContactDetails[0].mobileno = objODBCDatareader["mobile_no"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select contact2email_gid,email_address" +
                " from agr_mst_tsupronboardcontact2email where contact_gid='" + lscontact_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            arrContactDetails[0].email = objdbconn.GetExecuteScalar(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].email_gid = objODBCDatareader["contact2email_gid"].ToString();
                arrContactDetails[0].email = objODBCDatareader["email_address"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails[0].primary_status = "Yes";

            msSQL = " select contact2mobileno_gid,primary_status,mobile_no" +
                   " from agr_mst_tsupronboardcontact2mobileno where contact_gid='" + lscontact_gid + "' and primary_status = 'No'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            int mobindI = 1;
            foreach (DataRow dr_mobileno in dt_mobileno.Rows)
            {
                arrContactDetails[mobindI].mobileno_gid = dr_mobileno["contact2mobileno_gid"].ToString();
                arrContactDetails[mobindI].mobileno = dr_mobileno["mobile_no"].ToString();
                arrContactDetails[mobindI].primary_status = dr_mobileno["primary_status"].ToString();

                mobindI++;
            }

            msSQL = " select contact2email_gid,email_address,primary_status" +
                    " from agr_mst_tsupronboardcontact2email where contact_gid='" + lscontact_gid + "'  and primary_status = 'No'";
            dt_email = objdbconn.GetDataTable(msSQL);

            int emailindI = 1;
            foreach (DataRow dr_email in dt_email.Rows)
            {
                arrContactDetails[emailindI].email_gid = dr_email["contact2email_gid"].ToString();
                arrContactDetails[emailindI].email = dr_email["email_address"].ToString();
                arrContactDetails[emailindI].primary_status = dr_email["primary_status"].ToString();

                emailindI++;
            }

            foreach (var item in arrContactDetails)
            {
                mscontactdetailsGID = objcmnfunctions.GetMasterGID("BPCT");
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

        public void populateBuyerInstitutionContactTable(string application_gid)
        {
            //Application Contact
            msSQL = " select application_gid,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_type" +
                  " from agr_mst_tbyronboard where application_gid='" + application_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lscontactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                lscontactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                lscontactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                lsdesignation = objODBCDatareader["designation_type"].ToString();
            }
            objODBCDatareader.Close();



            msSQL = " select primary_mobileno,mobile_no" +
                     " from agr_mst_tbyronboard2contactno where application_gid='" + application_gid + "'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            msSQL = " select email_address,primary_emailaddress" +
                    " from agr_mst_tbyronboard2email where application_gid='" + application_gid + "'";
            dt_email = objdbconn.GetDataTable(msSQL);


            arrContactDetails = dt_email.Rows.Count > dt_mobileno.Rows.Count ? new ContactDetails[dt_email.Rows.Count] : new ContactDetails[dt_mobileno.Rows.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }


            msSQL = " select application2contact_gid,mobile_no" +
                 " from agr_mst_tbyronboard2contactno where application_gid='" + application_gid + "' and primary_mobileno = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].mobileno_gid = objODBCDatareader["application2contact_gid"].ToString();
                arrContactDetails[0].mobileno = objODBCDatareader["mobile_no"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select application2email_gid,email_address" +
                " from agr_mst_tbyronboard2email where application_gid='" + application_gid + "' and primary_emailaddress = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            arrContactDetails[0].email = objdbconn.GetExecuteScalar(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].email_gid = objODBCDatareader["application2email_gid"].ToString();
                arrContactDetails[0].email = objODBCDatareader["email_address"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails[0].primary_status = "Yes";


            msSQL = " select application2contact_gid,primary_mobileno,mobile_no" +
               " from agr_mst_tbyronboard2contactno where application_gid='" + application_gid + "' and primary_mobileno = 'No'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            int mobind = 1;
            foreach (DataRow dr_mobileno in dt_mobileno.Rows)
            {
                arrContactDetails[mobind].mobileno_gid = dr_mobileno["application2contact_gid"].ToString();
                arrContactDetails[mobind].mobileno = dr_mobileno["mobile_no"].ToString();
                arrContactDetails[mobind].primary_status = dr_mobileno["primary_mobileno"].ToString();

                mobind++;
            }

            msSQL = " select application2email_gid,email_address,primary_emailaddress" +
                   " from agr_mst_tbyronboard2email where application_gid='" + application_gid + "'  and primary_emailaddress = 'No'";
            dt_email = objdbconn.GetDataTable(msSQL);

            int emailind = 1;
            foreach (DataRow dr_email in dt_email.Rows)
            {
                arrContactDetails[emailind].email_gid = dr_email["application2email_gid"].ToString();
                arrContactDetails[emailind].email = dr_email["email_address"].ToString();
                arrContactDetails[emailind].primary_status = dr_email["primary_emailaddress"].ToString();

                emailind++;
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

            //Institution Contact 

            msSQL = " select institution_gid" +
                  " from agr_mst_tbyronboard2institution where application_gid='" + application_gid + "' and stakeholder_type='Applicant'";
            lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

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

            msSQL = " select primary_status,mobile_no" +
                     " from agr_mst_tbyronboardinstitution2mobileno where institution_gid='" + lsinstitution_gid + "'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            msSQL = " select primary_status,email_address" +
                    " from agr_mst_tbyronboardinstitution2email where institution_gid='" + lsinstitution_gid + "'";
            dt_email = objdbconn.GetDataTable(msSQL);

            arrContactDetails = dt_email.Rows.Count > dt_mobileno.Rows.Count ? new ContactDetails[dt_email.Rows.Count] : new ContactDetails[dt_mobileno.Rows.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }

            msSQL = " select institution2mobileno_gid,mobile_no" +
                     " from agr_mst_tbyronboardinstitution2mobileno where institution_gid='" + lsinstitution_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
                arrContactDetails[0].mobileno = objODBCDatareader["mobile_no"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select institution2email_gid,email_address" +
                " from agr_mst_tbyronboardinstitution2email where institution_gid='" + lsinstitution_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            arrContactDetails[0].email = objdbconn.GetExecuteScalar(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].email_gid = objODBCDatareader["institution2email_gid"].ToString();
                arrContactDetails[0].email = objODBCDatareader["email_address"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails[0].primary_status = "Yes";

            msSQL = " select institution2mobileno_gid,primary_status,mobile_no" +
                   " from agr_mst_tbyronboardinstitution2mobileno where institution_gid='" + lsinstitution_gid + "' and primary_status = 'No'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            int mobindI = 1;
            foreach (DataRow dr_mobileno in dt_mobileno.Rows)
            {
                arrContactDetails[mobindI].mobileno_gid = dr_mobileno["institution2mobileno_gid"].ToString();
                arrContactDetails[mobindI].mobileno = dr_mobileno["mobile_no"].ToString();
                arrContactDetails[mobindI].primary_status = dr_mobileno["primary_status"].ToString();

                mobindI++;
            }

            msSQL = " select institution2email_gid,email_address,primary_status" +
                    " from agr_mst_tbyronboardinstitution2email where institution_gid='" + lsinstitution_gid + "'  and primary_status = 'No'";
            dt_email = objdbconn.GetDataTable(msSQL);

            int emailindI = 1;
            foreach (DataRow dr_email in dt_email.Rows)
            {
                arrContactDetails[emailindI].email_gid = dr_email["institution2email_gid"].ToString();
                arrContactDetails[emailindI].email = dr_email["email_address"].ToString();
                arrContactDetails[emailindI].primary_status = dr_email["primary_status"].ToString();

                emailindI++;
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

        public void populateBuyerIndividualContactTable(string application_gid)
        {
            //Application Contact
            msSQL = " select application_gid,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_type" +
                  " from agr_mst_tbyronboard where application_gid='" + application_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lscontactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                lscontactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                lscontactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                lsdesignation = objODBCDatareader["designation_type"].ToString();
            }
            objODBCDatareader.Close();



            msSQL = " select primary_mobileno,mobile_no" +
                     " from agr_mst_tbyronboard2contactno where application_gid='" + application_gid + "'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            msSQL = " select email_address,primary_emailaddress" +
                    " from agr_mst_tbyronboard2email where application_gid='" + application_gid + "'";
            dt_email = objdbconn.GetDataTable(msSQL);


            arrContactDetails = dt_email.Rows.Count > dt_mobileno.Rows.Count ? new ContactDetails[dt_email.Rows.Count] : new ContactDetails[dt_mobileno.Rows.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }


            msSQL = " select application2contact_gid,mobile_no" +
                 " from agr_mst_tbyronboard2contactno where application_gid='" + application_gid + "' and primary_mobileno = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].mobileno_gid = objODBCDatareader["application2contact_gid"].ToString();
                arrContactDetails[0].mobileno = objODBCDatareader["mobile_no"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select application2email_gid,email_address" +
                " from agr_mst_tbyronboard2email where application_gid='" + application_gid + "' and primary_emailaddress = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            arrContactDetails[0].email = objdbconn.GetExecuteScalar(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].email_gid = objODBCDatareader["application2email_gid"].ToString();
                arrContactDetails[0].email = objODBCDatareader["email_address"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails[0].primary_status = "Yes";


            msSQL = " select application2contact_gid,primary_mobileno,mobile_no" +
               " from agr_mst_tbyronboard2contactno where application_gid='" + application_gid + "' and primary_mobileno = 'No'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            int mobind = 1;
            foreach (DataRow dr_mobileno in dt_mobileno.Rows)
            {
                arrContactDetails[mobind].mobileno_gid = dr_mobileno["application2contact_gid"].ToString();
                arrContactDetails[mobind].mobileno = dr_mobileno["mobile_no"].ToString();
                arrContactDetails[mobind].primary_status = dr_mobileno["primary_mobileno"].ToString();

                mobind++;
            }

            msSQL = " select application2email_gid,email_address,primary_emailaddress" +
                   " from agr_mst_tbyronboard2email where application_gid='" + application_gid + "'  and primary_emailaddress = 'No'";
            dt_email = objdbconn.GetDataTable(msSQL);

            int emailind = 1;
            foreach (DataRow dr_email in dt_email.Rows)
            {
                arrContactDetails[emailind].email_gid = dr_email["application2email_gid"].ToString();
                arrContactDetails[emailind].email = dr_email["email_address"].ToString();
                arrContactDetails[emailind].primary_status = dr_email["primary_emailaddress"].ToString();

                emailind++;
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

            //Individual Contact 

            msSQL = " select contact_gid" +
                  " from agr_mst_tbyronboardcontact where application_gid='" + application_gid + "' and stakeholder_type='Applicant'";
            lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

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

            msSQL = " select primary_status,mobile_no" +
                     " from agr_mst_tbyronboardcontact2mobileno where contact_gid='" + lscontact_gid + "'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            msSQL = " select primary_status,email_address" +
                    " from agr_mst_tbyronboardcontact2email where contact_gid='" + lscontact_gid + "'";
            dt_email = objdbconn.GetDataTable(msSQL);

            arrContactDetails = dt_email.Rows.Count > dt_mobileno.Rows.Count ? new ContactDetails[dt_email.Rows.Count] : new ContactDetails[dt_mobileno.Rows.Count];

            for (int i = 0; i < arrContactDetails.Length; i++)
            {
                arrContactDetails[i] = new ContactDetails();
            }

            msSQL = " select contact2mobileno_gid,mobile_no" +
                     " from agr_mst_tbyronboardcontact2mobileno where contact_gid='" + lscontact_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].mobileno_gid = objODBCDatareader["contact2mobileno_gid"].ToString();
                arrContactDetails[0].mobileno = objODBCDatareader["mobile_no"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select contact2email_gid,email_address" +
                " from agr_mst_tbyronboardcontact2email where contact_gid='" + lscontact_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            arrContactDetails[0].email = objdbconn.GetExecuteScalar(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                arrContactDetails[0].email_gid = objODBCDatareader["contact2email_gid"].ToString();
                arrContactDetails[0].email = objODBCDatareader["email_address"].ToString();
            }
            objODBCDatareader.Close();

            arrContactDetails[0].primary_status = "Yes";

            msSQL = " select contact2mobileno_gid,primary_status,mobile_no" +
                   " from agr_mst_tbyronboardcontact2mobileno where contact_gid='" + lscontact_gid + "' and primary_status = 'No'";
            dt_mobileno = objdbconn.GetDataTable(msSQL);

            int mobindI = 1;
            foreach (DataRow dr_mobileno in dt_mobileno.Rows)
            {
                arrContactDetails[mobindI].mobileno_gid = dr_mobileno["contact2mobileno_gid"].ToString();
                arrContactDetails[mobindI].mobileno = dr_mobileno["mobile_no"].ToString();
                arrContactDetails[mobindI].primary_status = dr_mobileno["primary_status"].ToString();

                mobindI++;
            }

            msSQL = " select contact2email_gid,email_address,primary_status" +
                    " from agr_mst_tbyronboardcontact2email where contact_gid='" + lscontact_gid + "'  and primary_status = 'No'";
            dt_email = objdbconn.GetDataTable(msSQL);

            int emailindI = 1;
            foreach (DataRow dr_email in dt_email.Rows)
            {
                arrContactDetails[emailindI].email_gid = dr_email["contact2email_gid"].ToString();
                arrContactDetails[emailindI].email = dr_email["email_address"].ToString();
                arrContactDetails[emailindI].primary_status = dr_email["primary_status"].ToString();

                emailindI++;
            }

            foreach (var item in arrContactDetails)
            {
                mscontactdetailsGID = objcmnfunctions.GetMasterGID("BPCT");
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


        //Auxillary Functions
        public void LogForAuditHBAPI(string strVal, string type)
        {
            try
            {

                if (type == "Buyer")
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
                else if (type == "Supplier")
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
        
        public void LogForAuditHBAPI2(IRestResponse strVal, string type)
        {
            try
            {

                if (type == "Buyer")
                {

                    loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/HyperbridgeAPI/NetsuiteERPLog2/" + type + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    if ((!System.IO.Directory.Exists(loglspath)))
                        System.IO.Directory.CreateDirectory(loglspath);
                    if (logFileName == "")

                    {
                        logFileName = "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
                    }
                    loglspath = loglspath + logFileName;
                }
                else if (type == "Supplier")
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

        public string fetchNsGSTStateSode(string value)
        {

            msSQL = "select ns_state_code from agr_mst_tnsstatemst where gst_state_name = '" + value + "'";
            string ns_state_code = objdbconn.GetExecuteScalar(msSQL);
            return ns_state_code;

        }

        public string fetchNsAddrStateCode(string value)
        {
            msSQL = "select ns_addr_code from agr_mst_tnsaddrstatemst where addr_state_name = '" + value + "'";
            string ns_state_code = objdbconn.GetExecuteScalar(msSQL);
            return ns_state_code;
        }

        public string fetchCompanyTypeName(string companytype_gid)
        {
            msSQL = "select companytype_name from ocs_mst_tcompanytype where companytype_gid = '" + companytype_gid + "'";
            string companytype_name = objdbconn.GetExecuteScalar(msSQL);
            return companytype_name;
        }

    }
}