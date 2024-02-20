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
using System.Web.Http.Results;
using System.Web.UI.WebControls;

namespace ems.hbapiconn.Functions
{
    public class FnSamFinEncoreCustomer
    {
        string lsresponsecontentcustomercreation, lsresponseStatusCodecustomercreation, lsresponsecontentfindcustomer, lsresponseStatusCodefindcustomer;
        string msSQL, lsinstitution_gid, lscontact_gid, msGetGidCustCrtResp,msGetGidCustCrtReq,lsbankaccount_number, lsconstitution_name, msGetGidFnCustReq;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();

        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        int mnResult, mnResultContact, mnResultAddress;

        string lsstate,lscity,lsdistrict,lssector, lsinsti_id, lscont_id, lsencore_sector,
            lsencore_constitution,lsencore_statecode,lsencore_districtcode,lsencore_citycode, lsapplication_gid,
            lsvertical_gid, lsconstitution_gid, lsvertical_name, lsaadhar_no;

        int lsinsti_idcount, lscont_idcount,farmer_count, total_farmer;

        string loglspath = "", logFileName = "", lspan, lsfarmer;

        //Create Customer Encore
        public MdlCreateCustomerResponse DaPostCreateCustomerEncore(MdlcustomercreationLMSAPI values, string employee_gid)
        {
            string type = "CreateCustomer";
            MdlCreateCustomerResponse ObjMdlCreateCustomerResponse = new MdlCreateCustomerResponse();
            try
            {
                
                MdlCreateCustomerRequest ObjMdlCreateCustomerRequest = new MdlCreateCustomerRequest();

                if (!(String.IsNullOrEmpty(values.farmercontact_gid)))
                {
                    LogForAuditEncoreIntegration("Customer Creation Request Initiated for Individual Farmer . Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                    msSQL = " select  a.application_gid,a.application_no,a.first_name,a.last_name,a.middle_name," +
                      " b.addressline1,b.addressline2,b.postal_code,c.email_address, " +
                      " a.pan_no,a.aadhar_no,a.gender_name,b.state,b.city,b.district,SUBSTRING(a.individual_dob,7) AS Year,SUBSTRING(a.individual_dob,4,2) AS Month,SUBSTRING(a.individual_dob,1,2) AS Day," +
                      " a.father_firstname,a.father_middlename,a.father_lastname,a.mother_firstname,a.mother_middlename,a.mother_lastname," +
                      " a.bankaccount_number,a.bank_name,a.branch_name,a.branch_address,a.ifsc_code,a.micr_code,a.account_type,a.accountholder_name," +                      
                      " f.mobile_no from ocs_trn_tfarmercontact a left join ocs_trn_tfarmercontact2email c on a.farmercontact_gid=c.farmercontact_gid " +
                      " left join ocs_trn_tfarmercontact2mobileno f on a.farmercontact_gid=f.farmercontact_gid " +                      
                      " left join ocs_trn_tfarmercontact2address b on a.farmercontact_gid=b.farmercontact_gid " +
                      " where f.primary_status='Yes' and a.farmercontact_gid='" + values.farmercontact_gid + "'";
                 
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ObjMdlCreateCustomerRequest.firstName = objODBCDatareader["first_name"].ToString();
                        ObjMdlCreateCustomerRequest.lastName = objODBCDatareader["last_name"].ToString();
                        ObjMdlCreateCustomerRequest.middleName = objODBCDatareader["middle_name"].ToString();
                        ObjMdlCreateCustomerRequest.address1 = objODBCDatareader["addressline1"].ToString();
                        ObjMdlCreateCustomerRequest.address2 = objODBCDatareader["addressline2"].ToString();
                        ObjMdlCreateCustomerRequest.branchCode = "HO";
                        ObjMdlCreateCustomerRequest.countryCode = "IN";
                        ObjMdlCreateCustomerRequest.dateOfBirth = objODBCDatareader["Year"].ToString() +"-"+objODBCDatareader["Month"].ToString() +"-"+ objODBCDatareader["Day"].ToString();

                        ObjMdlCreateCustomerRequest.pinCode = objODBCDatareader["postal_code"].ToString();
                        ObjMdlCreateCustomerRequest.phone1 = objODBCDatareader["mobile_no"].ToString();
                        ObjMdlCreateCustomerRequest.email = objODBCDatareader["email_address"].ToString();
                        ObjMdlCreateCustomerRequest.fatherOrSpouseFirstName = objODBCDatareader["father_firstname"].ToString();
                        ObjMdlCreateCustomerRequest.fatherOrSpouseMiddleName = objODBCDatareader["father_middlename"].ToString();
                        ObjMdlCreateCustomerRequest.fatherOrSpouseLastName = objODBCDatareader["father_lastname"].ToString();

                        ObjMdlCreateCustomerRequest.motherFirstName = objODBCDatareader["mother_firstname"].ToString();
                        ObjMdlCreateCustomerRequest.motherMiddleName = objODBCDatareader["mother_middlename"].ToString();
                        ObjMdlCreateCustomerRequest.motherLastName = objODBCDatareader["mother_firstname"].ToString();
                        ObjMdlCreateCustomerRequest.pan = objODBCDatareader["pan_no"].ToString();
                        ObjMdlCreateCustomerRequest.uidNum = objODBCDatareader["aadhar_no"].ToString();
                        ObjMdlCreateCustomerRequest.gender = objODBCDatareader["gender_name"].ToString();
                        ObjMdlCreateCustomerRequest.nationality = "INDIAN";
                        ObjMdlCreateCustomerRequest.addressType = "Communication";

                        lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                        lsstate = objODBCDatareader["state"].ToString();
                        lsdistrict = objODBCDatareader["district"].ToString();
                        lscity = objODBCDatareader["city"].ToString();
                            
                        ObjMdlCreateCustomerRequest.accountNumber1 = objODBCDatareader["bankaccount_number"].ToString();
                        ObjMdlCreateCustomerRequest.bankName1 = objODBCDatareader["bank_name"].ToString();
                        ObjMdlCreateCustomerRequest.branchName1 = objODBCDatareader["branch_name"].ToString();
                        ObjMdlCreateCustomerRequest.branchAddress1 = objODBCDatareader["branch_address"].ToString();
                        ObjMdlCreateCustomerRequest.ifscCode1 = objODBCDatareader["ifsc_code"].ToString();
                        ObjMdlCreateCustomerRequest.micrCode1 = objODBCDatareader["micr_code"].ToString();

                        ObjMdlCreateCustomerRequest.accountType1 = objODBCDatareader["account_type"].ToString();
                        ObjMdlCreateCustomerRequest.accountHolderName1 = objODBCDatareader["accountholder_name"].ToString();
                    }                
                    objODBCDatareader.Close();
                    
                    msSQL = " select vertical_gid,constitution_gid " +
                      " from ocs_trn_tcadapplication " +
                      " where application_gid='" + lsapplication_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                        lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                    }
                    objODBCDatareader.Close();                    
                }
                else
                {
                    LogForAuditEncoreIntegration("Customer Creation Request Initiated for Applicant . Applicant ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                    msSQL = " select stakeholder_type,application_gid from ocs_trn_tcadinstitution where  application_gid='" + values.application_gid + "'" +
                    " and stakeholder_type= 'Applicant' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select  a.application_gid,a.application_no,a.customer_name, " +
                            " b.addressline1,b.addressline2,date_format(c.date_incorporation,'%Y-%m-%d') as date_incorporation,b.postal_code,f.mobile_no,c.email_address,c.companypan_no," +
                            " a.vertical_gid,b.state,b.city,b.district,b.country,b.institution_gid,a.constitution_gid,a.relationshipmanager_name as 'RMName',e.user_code as 'UserID',d.employee_emailid as 'RMmailID'" +
                            " from ocs_trn_tcadapplication a left join ocs_trn_tcadinstitution c on c.application_gid=a.application_gid " +
                            " left join hrm_mst_temployee d on d.employee_gid=a.relationshipmanager_gid left join adm_mst_tuser e on e.user_gid=d.user_gid left join ocs_trn_tcadinstitution2mobileno f on f.institution_gid=c.institution_gid " +
                            " left join ocs_trn_tcadinstitution2email g on g.institution_gid=c.institution_gid left join ocs_trn_tcadinstitution2address b on b.institution_gid=c.institution_gid " +
                            " where f.primary_status='Yes' and a.application_gid='" + values.application_gid + "'" +
                           " group by b.institution_gid ,f.institution_gid,g.institution_gid ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ObjMdlCreateCustomerRequest.firstName = objODBCDatareader["customer_name"].ToString();
                            ObjMdlCreateCustomerRequest.address1 = objODBCDatareader["addressline1"].ToString();
                            ObjMdlCreateCustomerRequest.address2 = objODBCDatareader["addressline2"].ToString();
                            ObjMdlCreateCustomerRequest.branchCode = "HO";
                            ObjMdlCreateCustomerRequest.countryCode = "IN";
                            ObjMdlCreateCustomerRequest.dateOfBirth = objODBCDatareader["date_incorporation"].ToString();
                            ObjMdlCreateCustomerRequest.pinCode = objODBCDatareader["postal_code"].ToString();

                            ObjMdlCreateCustomerRequest.phone1 = objODBCDatareader["mobile_no"].ToString();
                            ObjMdlCreateCustomerRequest.email = objODBCDatareader["email_address"].ToString();
                            ObjMdlCreateCustomerRequest.pan = objODBCDatareader["companypan_no"].ToString();
                            ObjMdlCreateCustomerRequest.addressType = "Communication";

                            lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                            lsstate = objODBCDatareader["state"].ToString();
                            lsdistrict = objODBCDatareader["district"].ToString();
                            lscity = objODBCDatareader["city"].ToString();
                            lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                           
                        }
                        objODBCDatareader.Close();
                    }

                    msSQL = " select stakeholder_type,application_gid from ocs_trn_tcadcontact where  application_gid='" + values.application_gid + "'" +
                   " and stakeholder_type= 'Applicant' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select  a.application_gid,a.application_no,c.first_name,c.last_name,c.middle_name," +
                       " b.addressline1,b.addressline2,SUBSTRING(c.individual_dob,7) AS Year,SUBSTRING(c.individual_dob,4,2) AS Month,SUBSTRING(c.individual_dob,1,2) AS Day,b.postal_code,c.email_address, " +
                       " c.pan_no,c.aadhar_no,c.gender_name,b.state,b.city,b.district,a.vertical_gid,a.constitution_gid," +
                       " c.father_firstname,c.father_middlename,c.father_lastname,c.mother_firstname,c.mother_middlename,c.mother_lastname," +
                       " f.mobile_no from ocs_trn_tcadapplication a left join ocs_trn_tcadcontact c on c.application_gid=a.application_gid " +
                       " left join hrm_mst_temployee d on d.employee_gid=a.relationshipmanager_gid left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                       " left join ocs_trn_tcadcontact2mobileno f on f.contact_gid=c.contact_gid " +
                       " left join ocs_trn_tcadcontact2idproof h on h.contact_gid=c.contact_gid" +
                       " left join ocs_trn_tcadcontact2email g on g.contact_gid=c.contact_gid left join ocs_trn_tcadcontact2address b on b.contact_gid=c.contact_gid " +
                       " where f.primary_status='Yes' and a.application_gid='" + values.application_gid + "'" +
                       " group by b.contact_gid ,f.contact_gid,g.contact_gid ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ObjMdlCreateCustomerRequest.firstName = objODBCDatareader["first_name"].ToString();
                            ObjMdlCreateCustomerRequest.lastName = objODBCDatareader["last_name"].ToString();
                            ObjMdlCreateCustomerRequest.middleName = objODBCDatareader["middle_name"].ToString();
                            ObjMdlCreateCustomerRequest.address1 = objODBCDatareader["addressline1"].ToString();
                            ObjMdlCreateCustomerRequest.address2 = objODBCDatareader["addressline2"].ToString();
                            ObjMdlCreateCustomerRequest.branchCode = "HO";
                            ObjMdlCreateCustomerRequest.countryCode = "IN";
                            ObjMdlCreateCustomerRequest.dateOfBirth = objODBCDatareader["Year"].ToString() + "-" + objODBCDatareader["Month"].ToString() + "-" + objODBCDatareader["Day"].ToString();

                            ObjMdlCreateCustomerRequest.pinCode = objODBCDatareader["postal_code"].ToString();
                            ObjMdlCreateCustomerRequest.phone1 = objODBCDatareader["mobile_no"].ToString();
                            ObjMdlCreateCustomerRequest.email = objODBCDatareader["email_address"].ToString();
                            ObjMdlCreateCustomerRequest.fatherOrSpouseFirstName = objODBCDatareader["father_firstname"].ToString();
                            ObjMdlCreateCustomerRequest.fatherOrSpouseMiddleName = objODBCDatareader["father_middlename"].ToString();
                            ObjMdlCreateCustomerRequest.fatherOrSpouseLastName = objODBCDatareader["father_lastname"].ToString();

                            ObjMdlCreateCustomerRequest.motherFirstName = objODBCDatareader["mother_firstname"].ToString();
                            ObjMdlCreateCustomerRequest.motherMiddleName = objODBCDatareader["mother_middlename"].ToString();
                            ObjMdlCreateCustomerRequest.motherLastName = objODBCDatareader["mother_firstname"].ToString();
                            ObjMdlCreateCustomerRequest.pan = objODBCDatareader["pan_no"].ToString();
                            ObjMdlCreateCustomerRequest.uidNum = objODBCDatareader["aadhar_no"].ToString();
                            ObjMdlCreateCustomerRequest.gender = objODBCDatareader["gender_name"].ToString();
                            ObjMdlCreateCustomerRequest.nationality = "INDIAN";
                            ObjMdlCreateCustomerRequest.addressType = "Communication";

                            lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                            lsstate = objODBCDatareader["state"].ToString();
                            lsdistrict = objODBCDatareader["district"].ToString();
                            lscity = objODBCDatareader["city"].ToString();
                            lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                        }
                    }
                    objODBCDatareader.Close();



                    msSQL = " select bankaccount_number,gst_no from ocs_trn_tcustomercreationlms where application_gid='" + values.application_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ObjMdlCreateCustomerRequest.gstin = objODBCDatareader["gst_no"].ToString();
                        ObjMdlCreateCustomerRequest.accountNumber1 = objODBCDatareader["bankaccount_number"].ToString();
                        lsbankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select bank_name,branch_name,bank_address,ifsc_code,micr_code,bankaccounttype_name,accountholder_name from ocs_trn_tcadcreditbankdtl where bankaccount_number='" + lsbankaccount_number + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ObjMdlCreateCustomerRequest.bankName1 = objODBCDatareader["bank_name"].ToString();
                        ObjMdlCreateCustomerRequest.branchName1 = objODBCDatareader["branch_name"].ToString();
                        ObjMdlCreateCustomerRequest.branchAddress1 = objODBCDatareader["bank_address"].ToString();
                        ObjMdlCreateCustomerRequest.ifscCode1 = objODBCDatareader["ifsc_code"].ToString();
                        ObjMdlCreateCustomerRequest.micrCode1 = objODBCDatareader["micr_code"].ToString();
                        ObjMdlCreateCustomerRequest.accountType1 = objODBCDatareader["bankaccounttype_name"].ToString();
                        ObjMdlCreateCustomerRequest.accountHolderName1 = objODBCDatareader["accountholder_name"].ToString();
                    }
                    objODBCDatareader.Close();
                }

                if ((String.IsNullOrEmpty(lsconstitution_gid)))
                {
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Constitution Name is Empty";
                    return ObjMdlCreateCustomerResponse;
                }

                if ((String.IsNullOrEmpty(lsstate)))
                {
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "State Field is Empty";
                    return ObjMdlCreateCustomerResponse;
                }


                if ((String.IsNullOrEmpty(lsdistrict)))
                {
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "District Field is Empty";
                    return ObjMdlCreateCustomerResponse;
                }

                if ((String.IsNullOrEmpty(lscity)))
                {
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "City Field is Empty";
                    return ObjMdlCreateCustomerResponse;
                }


                if ((String.IsNullOrEmpty(lsvertical_gid)))
                {
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Sector is Empty";
                    return ObjMdlCreateCustomerResponse;
                }

                msSQL = " select encore_constitution,constitution_name from ocs_mst_tconstitution where constitution_gid='" + lsconstitution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ObjMdlCreateCustomerRequest.customerType = objODBCDatareader["encore_constitution"].ToString();
                    lsencore_constitution = objODBCDatareader["encore_constitution"].ToString();
                    lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                    objODBCDatareader.Close();
                }               
                else
                {
                    objODBCDatareader.Close();
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Encore Customer Type Constitution Mapping is Unavailable for '" + lsconstitution_name + "'";
                    return ObjMdlCreateCustomerResponse;
                }

                if ((String.IsNullOrEmpty(lsencore_constitution)))
                {
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Encore Customer Type Constitution Mapping is Unavailable for '" + lsconstitution_name + "'";
                    return ObjMdlCreateCustomerResponse;
                }

                msSQL = " select encore_statecode from ocs_mst_tstatecodemapping where postalcode_statefield='" + lsstate + "'";
                lsencore_statecode = objdbconn.GetExecuteScalar(msSQL);
                if (!(String.IsNullOrEmpty(lsencore_statecode)))
                {
                    ObjMdlCreateCustomerRequest.stateCode = lsencore_statecode;
                    objODBCDatareader.Close();
                }
                else
                {
                    objODBCDatareader.Close();
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Encore State Mapping is Unavailable for '" + lsstate + "'";
                    return ObjMdlCreateCustomerResponse;
                }

                msSQL = " select encore_districtcode from ocs_mst_tdistrictcodemapping where postalcode_districtfield='" + lsdistrict + "'";
                lsencore_districtcode = objdbconn.GetExecuteScalar(msSQL);
                if (!(String.IsNullOrEmpty(lsencore_districtcode)))
                {
                    ObjMdlCreateCustomerRequest.districtCode = lsencore_districtcode;
                    objODBCDatareader.Close();
                }

                else
                {
                    objODBCDatareader.Close();
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Encore District Mapping is Unavailable for '" + lsdistrict + "'";
                    return ObjMdlCreateCustomerResponse;
                }

                msSQL = " select encore_citycode from ocs_mst_tcitycodemapping where postalcode_cityfield='" + lscity + "'";
                lsencore_citycode = objdbconn.GetExecuteScalar(msSQL);
                if (!(String.IsNullOrEmpty(lsencore_citycode)))
                {
                    ObjMdlCreateCustomerRequest.cityCode = lsencore_citycode;
                    objODBCDatareader.Close();
                }
                else
                {
                    objODBCDatareader.Close();
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Encore City Mapping is Unavailable for '" + lscity + "'";
                    return ObjMdlCreateCustomerResponse;
                }

                msSQL = " select encore_sector,vertical_name from ocs_mst_tvertical where vertical_gid='" + lsvertical_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ObjMdlCreateCustomerRequest.sector = objODBCDatareader["encore_sector"].ToString();
                    lsencore_sector = objODBCDatareader["encore_sector"].ToString();
                    lsvertical_name = objODBCDatareader["vertical_name"].ToString();
                    objODBCDatareader.Close();
                }
                else
                {
                    objODBCDatareader.Close();
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Encore Sector Mapping is Unavailable  for '" + lsvertical_name + "'";
                    return ObjMdlCreateCustomerResponse;
                }

                if ((String.IsNullOrEmpty(lsencore_sector)))
                {
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Encore Sector Mapping is Unavailable  for '" + lsvertical_name + "'";
                    return ObjMdlCreateCustomerResponse;
                }
                

                string lscreatecustomerreqst_json = Newtonsoft.Json.JsonConvert.SerializeObject(ObjMdlCreateCustomerRequest);

                msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("ECRQ");
                msSQL = " insert into ocs_trn_tencorecreatecustomerrequest(" +
                        " encorecreatecustomerrequest_gid," +
                        " application_gid," +
                        " farmercontact_gid," +                        
                        " request_time," +
                        " requested_by," +
                        " customerId," +
                        " branchCode," +
                        " firstName," +
                        " middleName," +
                        " lastName," +
                        " dateOfBirth," +
                        " address1," +
                        " address2," +
                        " address3," +
                        " countryCode," +
                        " stateCode," +
                        " cityCode," +
                        " districtCode," +
                        " pinCode," +
                        " phone1," +
                        " phone2," +
                        " email," +
                        " customerType," +
                        " fatherOrSpouseFirstName," +
                        " fatherOrSpouseMiddleName," +
                        " fatherOrSpouseLastName," +
                        " motherFirstName," +
                        " motherMiddleName," +
                        " motherLastName," +
                        " salutation," +
                        " gender," +
                        " nationality," +
                        " caste," +
                        " religion," +
                        " domicileStatus," +
                        " guardianFirstName," +
                        " guardianMiddleName," +
                        " guardianLastName," +
                        " guardianAddress1," +
                        " guardianAddress2," +
                        " guardianAddress3," +
                        " guardianCountryCode," +
                        " guardianStateCode," +
                        " guardianCityCode," +
                        " guardianDistrictCode," +
                        " guardianPinCode," +
                        " guardianRelationshipWithCustomer," +
                        " guardianDateOfBirth," +
                        " guardianSalutation," +
                        " guardianGender," +
                        " guardianCustomerId," +
                        " pan," +
                        " uidNum," +
                        " gstin," +
                        " registrationNo," +
                        " cin," +
                        " legalEntityIdentifier," +
                        " tag," +
                        " documentType," +
                        " proofType," +
                        " documentNumber," +
                        " dateOfIssue," +
                        " dateOfExpiry," +
                        " filePath," +
                        " occupation," +
                        " employment," +
                        " sector," +
                        " residenceType," +
                        " language," +
                        " languageProficiency," +
                        " education," +
                        " annualIncome," +
                        " segment," +
                        " addressType," +
                        " organizationName," +
                        " alt1Address1," +
                        " alt1Address2," +
                        " alt1Address3," +
                        " alt1CityCode," +
                        " alt1DistrictCode," +
                        " alt1StateCode," +
                        " alt1CountryCode," +
                        " alt1PinCode," +
                        " altAddressType1," +
                        " alt1Phone1," +
                        " alt1Phone2," +
                        " alt1Email," +
                        " alt2Address1," +
                        " alt2Address2," +
                        " alt2Address3," +
                        " alt2CityCode," +
                        " alt2DistrictCode," +
                        " alt2StateCode," +
                        " alt2CountryCode," +
                        " alt2PinCode," +
                        " altAddressType2," +
                        " alt2Phone1," +
                        " alt2Phone2," +
                        " alt2Email," +
                        " centreName," +
                        " centreId," +
                        " nomineeCustomerId," +
                        " nomineeRelationshipType," +
                        " maritalStatus," +
                        " bankName1," +
                        " branchName1," +
                        " branchAddress1," +
                        " ifscCode1," +
                        " micrCode1," +
                        " accountType1," +
                        " accountNumber1," +
                        " accountHolderName1," +
                        " bankName2," +
                        " branchName2," +
                        " branchAddress2," +
                        " ifscCode2," +
                        " micrCode2," +
                        " accountType2," +
                        " accountNumber2," +
                        " accountHolderName2," +
                        " bankName3," +
                        " branchName3," +
                        " branchAddress3," +
                        " ifscCode3," +
                        " micrCode3," +
                        " accountType3," +
                        " accountNumber3," +
                        " accountHolderName3," +
                        " referenceCustomerId1," +
                        " referenceCustomerId2," +
                        " json_string) " +
                        " values(" +
                        "'" + msGetGidCustCrtReq + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.farmercontact_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + ObjMdlCreateCustomerRequest.customerId + "'," +
                        "'" + ObjMdlCreateCustomerRequest.branchCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.firstName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.middleName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.lastName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.dateOfBirth + "'," +
                        "'" + ObjMdlCreateCustomerRequest.address1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.address2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.address3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.countryCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.stateCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.cityCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.districtCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.pinCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.phone1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.phone2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.email + "'," +
                        "'" + ObjMdlCreateCustomerRequest.customerType + "'," +
                        "'" + ObjMdlCreateCustomerRequest.fatherOrSpouseFirstName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.fatherOrSpouseMiddleName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.fatherOrSpouseLastName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.motherFirstName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.motherMiddleName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.motherLastName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.salutation + "'," +
                        "'" + ObjMdlCreateCustomerRequest.gender + "'," +
                        "'" + ObjMdlCreateCustomerRequest.nationality + "'," +
                        "'" + ObjMdlCreateCustomerRequest.caste + "'," +
                        "'" + ObjMdlCreateCustomerRequest.religion + "'," +
                        "'" + ObjMdlCreateCustomerRequest.domicileStatus + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianFirstName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianMiddleName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianLastName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianAddress1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianAddress2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianAddress3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianCountryCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianStateCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianCityCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianDistrictCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianPinCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianRelationshipWithCustomer + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianDateOfBirth + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianSalutation + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianGender + "'," +
                        "'" + ObjMdlCreateCustomerRequest.guardianCustomerId + "'," +
                        "'" + ObjMdlCreateCustomerRequest.pan + "'," +
                        "'" + ObjMdlCreateCustomerRequest.uidNum + "'," +
                        "'" + ObjMdlCreateCustomerRequest.gstin + "'," +
                        "'" + ObjMdlCreateCustomerRequest.registrationNo + "'," +
                        "'" + ObjMdlCreateCustomerRequest.cin + "'," +
                        "'" + ObjMdlCreateCustomerRequest.legalEntityIdentifier + "'," +
                        "'" + ObjMdlCreateCustomerRequest.tag + "'," +
                        "'" + ObjMdlCreateCustomerRequest.documentType + "'," +
                        "'" + ObjMdlCreateCustomerRequest.proofType + "'," +
                        "'" + ObjMdlCreateCustomerRequest.documentNumber + "'," +
                        "'" + ObjMdlCreateCustomerRequest.dateOfIssue + "'," +
                        "'" + ObjMdlCreateCustomerRequest.dateOfExpiry + "'," +
                        "'" + ObjMdlCreateCustomerRequest.filePath + "'," +
                        "'" + ObjMdlCreateCustomerRequest.occupation + "'," +
                        "'" + ObjMdlCreateCustomerRequest.employment + "'," +
                        "'" + ObjMdlCreateCustomerRequest.sector + "'," +
                        "'" + ObjMdlCreateCustomerRequest.residenceType + "'," +
                        "'" + ObjMdlCreateCustomerRequest.language + "'," +
                        "'" + ObjMdlCreateCustomerRequest.languageProficiency + "'," +
                        "'" + ObjMdlCreateCustomerRequest.education + "'," +
                        "'" + ObjMdlCreateCustomerRequest.annualIncome + "'," +
                        "'" + ObjMdlCreateCustomerRequest.segment + "'," +
                        "'" + ObjMdlCreateCustomerRequest.addressType + "'," +
                        "'" + ObjMdlCreateCustomerRequest.organizationName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1Address1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1Address2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1Address3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1CityCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1DistrictCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1StateCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1CountryCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1PinCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.altAddressType1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1Phone1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1Phone2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt1Email + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2Address1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2Address2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2Address3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2CityCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2DistrictCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2StateCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2CountryCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2PinCode + "'," +
                        "'" + ObjMdlCreateCustomerRequest.altAddressType2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2Phone1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2Phone2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.alt2Email + "'," +
                        "'" + ObjMdlCreateCustomerRequest.centreName + "'," +
                        "'" + ObjMdlCreateCustomerRequest.centreId + "'," +
                        "'" + ObjMdlCreateCustomerRequest.nomineeCustomerId + "'," +
                        "'" + ObjMdlCreateCustomerRequest.nomineeRelationshipType + "'," +
                        "'" + ObjMdlCreateCustomerRequest.maritalStatus + "'," +
                        "'" + ObjMdlCreateCustomerRequest.bankName1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.branchName1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.branchAddress1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.ifscCode1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.micrCode1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.accountType1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.accountNumber1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.accountHolderName1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.bankName2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.branchName2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.branchAddress2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.ifscCode2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.micrCode2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.accountType2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.accountNumber2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.accountHolderName2 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.bankName3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.branchName3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.branchAddress3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.ifscCode3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.micrCode3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.accountType3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.accountNumber3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.accountHolderName3 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.referenceCustomerId1 + "'," +
                        "'" + ObjMdlCreateCustomerRequest.referenceCustomerId2 + "'," +
                        "'" + lscreatecustomerreqst_json + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 1)
                {
                    LogForAuditEncoreIntegration("Error occurred while storing customer creation request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Error occurred while posting customer request to Encore";
                    return ObjMdlCreateCustomerResponse;
                }
                try
                {
                    var client = new RestClient(ConfigurationManager.AppSettings["encore_customercreationurl"].ToString());
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                    string val = System.Convert.ToBase64String(plainTextBytes);
                    request.AddHeader("Authorization", "Basic " + val);

                    var body = lscreatecustomerreqst_json;

                    request.AddParameter("application/json", body, ParameterType.RequestBody);

                    IRestResponse response = client.Execute(request);

                    lsresponsecontentcustomercreation = response.Content;    
                    lsresponseStatusCodecustomercreation = response.StatusCode.ToString();

                }
                catch (Exception ex)
                {                                                
                    LogForAuditEncoreIntegration("Error occurred while hitting customer creation URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Error occurred posting customer creation request to Encore";
                    return ObjMdlCreateCustomerResponse;
                }
                if (lsresponseStatusCodecustomercreation == "OK")
                {
                    ObjMdlCreateCustomerResponse = JsonConvert.DeserializeObject<MdlCreateCustomerResponse>(lsresponsecontentcustomercreation);

                    msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                    msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                            " encorecreatecustomerresponse_gid," +
                            " encorecreatecustomerrequest_gid," +
                            " response_time," +
                            " customerId," +
                            " branchCode," +
                            " firstName," +
                            " middleName," +
                            " lastName," +
                            " dateOfBirth," +
                            " address1," +
                            " address2," +
                            " address3," +
                            " countryCode," +
                            " stateCode," +
                            " cityCode," +
                            " districtCode," +
                            " pinCode," +
                            " phone1," +
                            " phone2," +
                            " email," +
                            " customerType," +
                            " fatherOrSpouseFirstName," +
                            " fatherOrSpouseMiddleName," +
                            " fatherOrSpouseLastName," +
                            " motherFirstName," +
                            " motherMiddleName," +
                            " motherLastName," +
                            " salutation," +
                            " gender," +
                            " nationality," +
                            " caste," +
                            " religion," +
                            " domicileStatus," +
                            " guardianFirstName," +
                            " guardianMiddleName," +
                            " guardianLastName," +
                            " guardianAddress1," +
                            " guardianAddress2," +
                            " guardianAddress3," +
                            " guardianCountryCode," +
                            " guardianStateCode," +
                            " guardianCityCode," +
                            " guardianDistrictCode," +
                            " guardianPinCode," +
                            " guardianRelationshipWithCustomer," +
                            " guardianDateOfBirth," +
                            " guardianSalutation," +
                            " guardianGender," +
                            " guardianCustomerId," +
                            " pan," +
                            " uidNum," +
                            " gstin," +
                            " registrationNo," +
                            " cin," +
                            " legalEntityIdentifier," +
                            " tag," +
                            " documentType," +
                            " proofType," +
                            " documentNumber," +
                            " dateOfIssue," +
                            " dateOfExpiry," +
                            " filePath," +
                            " occupation," +
                            " employment," +
                            " sector," +
                            " residenceType," +
                            " language," +
                            " languageProficiency," +
                            " education," +
                            " annualIncome," +
                            " segment," +
                            " addressType," +
                            " organizationName," +
                            " alt1Address1," +
                            " alt1Address2," +
                            " alt1Address3," +
                            " alt1CityCode," +
                            " alt1DistrictCode," +
                            " alt1StateCode," +
                            " alt1CountryCode," +
                            " alt1PinCode," +
                            " altAddressType1," +
                            " alt1Phone1," +
                            " alt1Phone2," +
                            " alt1Email," +
                            " alt2Address1," +
                            " alt2Address2," +
                            " alt2Address3," +
                            " alt2CityCode," +
                            " alt2DistrictCode," +
                            " alt2StateCode," +
                            " alt2CountryCode," +
                            " alt2PinCode," +
                            " altAddressType2," +
                            " alt2Phone1," +
                            " alt2Phone2," +
                            " alt2Email," +
                            " centreName," +
                            " centreId," +
                            " nomineeCustomerId," +
                            " nomineeRelationshipType," +
                            " maritalStatus," +
                            " bankName1," +
                            " branchName1," +
                            " branchAddress1," +
                            " ifscCode1," +
                            " micrCode1," +
                            " accountType1," +
                            " accountNumber1," +
                            " accountHolderName1," +
                            " bankName2," +
                            " branchName2," +
                            " branchAddress2," +
                            " ifscCode2," +
                            " micrCode2," +
                            " accountType2," +
                            " accountNumber2," +
                            " accountHolderName2," +
                            " bankName3," +
                            " branchName3," +
                            " branchAddress3," +
                            " ifscCode3," +
                            " micrCode3," +
                            " accountType3," +
                            " accountNumber3," +
                            " accountHolderName3," +
                            " referenceCustomerId1," +
                            " referenceCustomerId2," +
                            " json_string) " +
                            " values(" +
                            "'" + msGetGidCustCrtResp + "'," +
                            "'" + msGetGidCustCrtReq + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + ObjMdlCreateCustomerResponse.customerId + "'," +
                            "'" + ObjMdlCreateCustomerResponse.branchCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.firstName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.middleName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.lastName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.dateOfBirth + "'," +
                            "'" + ObjMdlCreateCustomerResponse.address1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.address2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.address3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.countryCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.stateCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.cityCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.districtCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.pinCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.phone1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.phone2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.email + "'," +
                            "'" + ObjMdlCreateCustomerResponse.customerType + "'," +
                            "'" + ObjMdlCreateCustomerResponse.fatherOrSpouseFirstName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.fatherOrSpouseMiddleName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.fatherOrSpouseLastName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.motherFirstName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.motherMiddleName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.motherLastName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.salutation + "'," +
                            "'" + ObjMdlCreateCustomerResponse.gender + "'," +
                            "'" + ObjMdlCreateCustomerResponse.nationality + "'," +
                            "'" + ObjMdlCreateCustomerResponse.caste + "'," +
                            "'" + ObjMdlCreateCustomerResponse.religion + "'," +
                            "'" + ObjMdlCreateCustomerResponse.domicileStatus + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianFirstName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianMiddleName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianLastName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianAddress1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianAddress2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianAddress3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianCountryCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianStateCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianCityCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianDistrictCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianPinCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianRelationshipWithCustomer + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianDateOfBirth + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianSalutation + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianGender + "'," +
                            "'" + ObjMdlCreateCustomerResponse.guardianCustomerId + "'," +
                            "'" + ObjMdlCreateCustomerResponse.pan + "'," +
                            "'" + ObjMdlCreateCustomerResponse.uidNum + "'," +
                            "'" + ObjMdlCreateCustomerResponse.gstin + "'," +
                            "'" + ObjMdlCreateCustomerResponse.registrationNo + "'," +
                            "'" + ObjMdlCreateCustomerResponse.cin + "'," +
                            "'" + ObjMdlCreateCustomerResponse.legalEntityIdentifier + "'," +
                            "'" + ObjMdlCreateCustomerResponse.tag + "'," +
                            "'" + ObjMdlCreateCustomerResponse.documentType + "'," +
                            "'" + ObjMdlCreateCustomerResponse.proofType + "'," +
                            "'" + ObjMdlCreateCustomerResponse.documentNumber + "'," +
                            "'" + ObjMdlCreateCustomerResponse.dateOfIssue + "'," +
                            "'" + ObjMdlCreateCustomerResponse.dateOfExpiry + "'," +
                            "'" + ObjMdlCreateCustomerResponse.filePath + "'," +
                            "'" + ObjMdlCreateCustomerResponse.occupation + "'," +
                            "'" + ObjMdlCreateCustomerResponse.employment + "'," +
                            "'" + ObjMdlCreateCustomerResponse.sector + "'," +
                            "'" + ObjMdlCreateCustomerResponse.residenceType + "'," +
                            "'" + ObjMdlCreateCustomerResponse.language + "'," +
                            "'" + ObjMdlCreateCustomerResponse.languageProficiency + "'," +
                            "'" + ObjMdlCreateCustomerResponse.education + "'," +
                            "'" + ObjMdlCreateCustomerResponse.annualIncome + "'," +
                            "'" + ObjMdlCreateCustomerResponse.segment + "'," +
                            "'" + ObjMdlCreateCustomerResponse.addressType + "'," +
                            "'" + ObjMdlCreateCustomerResponse.organizationName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1Address1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1Address2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1Address3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1CityCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1DistrictCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1StateCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1CountryCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1PinCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.altAddressType1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1Phone1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1Phone2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt1Email + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2Address1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2Address2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2Address3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2CityCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2DistrictCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2StateCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2CountryCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2PinCode + "'," +
                            "'" + ObjMdlCreateCustomerResponse.altAddressType2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2Phone1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2Phone2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.alt2Email + "'," +
                            "'" + ObjMdlCreateCustomerResponse.centreName + "'," +
                            "'" + ObjMdlCreateCustomerResponse.centreId + "'," +
                            "'" + ObjMdlCreateCustomerResponse.nomineeCustomerId + "'," +
                            "'" + ObjMdlCreateCustomerResponse.nomineeRelationshipType + "'," +
                            "'" + ObjMdlCreateCustomerResponse.maritalStatus + "'," +
                            "'" + ObjMdlCreateCustomerResponse.bankName1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.branchName1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.branchAddress1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.ifscCode1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.micrCode1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.accountType1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.accountNumber1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.accountHolderName1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.bankName2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.branchName2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.branchAddress2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.ifscCode2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.micrCode2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.accountType2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.accountNumber2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.accountHolderName2 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.bankName3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.branchName3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.branchAddress3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.ifscCode3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.micrCode3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.accountType3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.accountNumber3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.accountHolderName3 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.referenceCustomerId1 + "'," +
                            "'" + ObjMdlCreateCustomerResponse.referenceCustomerId2 + "'," +
                            "'" + lsresponsecontentcustomercreation + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlCreateCustomerResponse.customerId))))
                    {

                        if (!(String.IsNullOrEmpty(values.farmercontact_gid)))
                        {
                            msSQL = "update ocs_trn_tfarmercontact set urn='" + ObjMdlCreateCustomerResponse.customerId + "',urn_status='Yes' where farmercontact_gid='" + values.farmercontact_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            LogForAuditEncoreIntegration("Customer Creation Request Ended for Individual Farmer . Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            ObjMdlCreateCustomerResponse.status = true;
                            ObjMdlCreateCustomerResponse.message = "Customer Creation Response received successfully from Encore";
                            return ObjMdlCreateCustomerResponse;
                        }
                        else
                        {
                            msSQL = "update ocs_trn_tcustomercreationlms set customer_urn='" + ObjMdlCreateCustomerResponse.customerId + "',lms_status='Businessops',update_flag='Y' where application_gid='" + values.application_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 0)
                            {
                                msSQL = "select count(institution_gid) from ocs_trn_tcadinstitution where application_gid='" + values.application_gid + "' and stakeholder_type='Applicant'";
                                lsinsti_idcount = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));
                                if (lsinsti_idcount != 0)
                                {
                                    msSQL = "select institution_gid from ocs_trn_tcadinstitution where application_gid='" + values.application_gid + "' and stakeholder_type='Applicant'";
                                    lsinsti_id = objdbconn.GetExecuteScalar(msSQL);
                                    if (lsinsti_id != "" || lsinsti_id != null)
                                    {
                                        msSQL = "update ocs_trn_tcadinstitution set urn='" + ObjMdlCreateCustomerResponse.customerId + "',urn_status ='Yes' where institution_gid='" + lsinsti_id + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        msSQL = "update ocs_trn_tcadapplication set customer_urn='" + ObjMdlCreateCustomerResponse.customerId + "' where application_gid='" + values.application_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    }
                                }
                                msSQL = "select count(contact_gid) from ocs_trn_tcadcontact where application_gid='" + values.application_gid + "'and stakeholder_type='Applicant'";
                                lscont_idcount = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));
                                if (lscont_idcount != 0)
                                {
                                    msSQL = "select contact_gid from ocs_trn_tcadcontact where application_gid='" + values.application_gid + "'and stakeholder_type='Applicant'";
                                    lscont_id = objdbconn.GetExecuteScalar(msSQL);
                                    if (lscont_id != "" || lscont_id != null)
                                    {
                                        msSQL = "update ocs_trn_tcadcontact set urn='" + ObjMdlCreateCustomerResponse.customerId + "',urn_status ='Yes' where contact_gid='" + lscont_id + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        msSQL = "update ocs_trn_tcadapplication set customer_urn='" + ObjMdlCreateCustomerResponse.customerId + "' where application_gid='" + values.application_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }

                                LogForAuditEncoreIntegration("Customer Creation Request Ended for Applicant . Applicant ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlCreateCustomerResponse.status = true;
                                ObjMdlCreateCustomerResponse.message = "Customer Creation Response received successfully from Encore";
                                return ObjMdlCreateCustomerResponse;
                            }
                            else
                            {
                                ObjMdlCreateCustomerResponse.status = false;
                                ObjMdlCreateCustomerResponse.message = "Error occurred while storing response from Encore";
                                return ObjMdlCreateCustomerResponse;
                            }

                        }

                    }
                    else
                    {
                        LogForAuditEncoreIntegration("Error occurred while storing customer creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        ObjMdlCreateCustomerResponse.status = false;
                        ObjMdlCreateCustomerResponse.message = "Error occurred while storing response from Encore";
                        return ObjMdlCreateCustomerResponse;
                    }

                }
                else
                {
                    MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                    objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentcustomercreation);
                    
                    LogForAuditEncoreIntegration("Failed to receive 200 Response . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlCreateCustomerResponse.status = false;
                    ObjMdlCreateCustomerResponse.message = "Customer Creation in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.message;
                    return ObjMdlCreateCustomerResponse;
                }



               
            }
            catch (Exception ex)
            {
                LogForAuditEncoreIntegration("Error occurred posting customer creation request  to Encore . Exception - " + ex + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                ObjMdlCreateCustomerResponse.status = false;
                ObjMdlCreateCustomerResponse.message = "Error occurred posting customer creation request to Encore";
                return ObjMdlCreateCustomerResponse;
            }
           
        }

        //Find Customer in Encore Farmer
        public MdlFindCustomerResponse DaPostFindCustomerEncoreFarmer(MdlcustomercreationLMSAPI values, string employee_gid)
        {
            string type = "FindCustomer";
            MdlFindCustomerResponse ObjMdlFindCustomerResponse = new MdlFindCustomerResponse();
            try
            {

                if (!(String.IsNullOrEmpty(values.farmercontact_gid)))
                {
                    LogForAuditEncoreIntegration("Find Customer Request Initiated for Individual Farmer . Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                    msSQL = " select application_gid,application_no,pan_no " +
                            " from ocs_trn_tfarmercontact " +
                            " where farmercontact_gid='" + values.farmercontact_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lspan = objODBCDatareader["pan_no"].ToString();
                    }
                    objODBCDatareader.Close();

                    if ((String.IsNullOrEmpty(lspan)))
                    {

                            if (!(String.IsNullOrEmpty(values.farmercontact_gid)))
                            {
                                //LogForAuditEncoreIntegration("Find Customer Request Initiated for Individual Farmer . Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                msSQL = " select application_gid,application_no,aadhar_no " +
                                        " from ocs_trn_tfarmercontact " +
                                        " where farmercontact_gid='" + values.farmercontact_gid + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaadhar_no = objODBCDatareader["aadhar_no"].ToString();
                                }
                                objODBCDatareader.Close();

                                if ((String.IsNullOrEmpty(lsaadhar_no)))
                                {
                                    msSQL = "update ocs_trn_tfarmercontact set encorefindcust_status='Y' where farmercontact_gid='" + values.farmercontact_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "PAN and Aadhar Number is Empty - Proceed with Create Customer";
                                    return ObjMdlFindCustomerResponse;
                                }

                            }
                           
                            msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("EFCR");
                            msSQL = " insert into ocs_trn_tencorefindcustomerrequest(" +
                                    " encorefindcustomerrequest_gid," +
                                    " application_gid," +
                                    " farmercontact_gid," +
                                    " request_time," +
                                    " requested_by," +
                                    " aadhar_no) " +
                                    " values(" +
                                    "'" + msGetGidCustCrtReq + "'," +
                                    "'" + values.application_gid + "'," +
                                    "'" + values.farmercontact_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsaadhar_no + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 1)
                            {
                                LogForAuditEncoreIntegration("Error occurred while storing find customer request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Error occurred while posting find customer request to Encore";
                                return ObjMdlFindCustomerResponse;
                            }
                            try
                            {

                                var param = "uid=" + lsaadhar_no + "&offSet=0";

                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                                string requestAddressURL = ConfigurationManager.AppSettings["encore_findcustomerurl"].ToString() + "?" + param;
                                var clientAddress = new RestClient(requestAddressURL);
                                var requestAddress = new RestRequest(Method.GET);

                                requestAddress.AddHeader("Content-Type", "application/json");
                                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                                string val = System.Convert.ToBase64String(plainTextBytes);
                                requestAddress.AddHeader("Authorization", "Basic " + val);

                                IRestResponse responseAddress = clientAddress.Execute(requestAddress);

                                lsresponsecontentfindcustomer = responseAddress.Content;
                                lsresponseStatusCodefindcustomer = responseAddress.StatusCode.ToString();

                            }
                            catch (Exception ex)
                            {
                                LogForAuditEncoreIntegration("Error occurred while hitting Find customer URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Error occurred posting customer creation request to Encore";
                                return ObjMdlFindCustomerResponse;
                            }
                            if (lsresponseStatusCodefindcustomer == "OK")
                            {

                                ObjMdlFindCustomerResponse = JsonConvert.DeserializeObject<MdlFindCustomerResponse>(lsresponsecontentfindcustomer);

                                if (ObjMdlFindCustomerResponse.totalResults == "0")
                                {
                                    LogForAuditEncoreIntegration("Find Customer Request Ended for Individual Farmer. Reason - No Customer Found in Encore. Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                    if (!(String.IsNullOrEmpty(values.farmercontact_gid)))
                                    {
                                        msSQL = "update ocs_trn_tfarmercontact set encorefindcust_status='Y' where farmercontact_gid='" + values.farmercontact_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    
                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "No Customer Found in Encore";
                                    return ObjMdlFindCustomerResponse;
                                }

                                msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                                msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                                        " encorecreatecustomerresponse_gid," +
                                        " encorecreatecustomerrequest_gid," +
                                        " response_time," +
                                        " customerId," +
                                        " branchCode," +
                                        " firstName," +
                                        " middleName," +
                                        " lastName," +
                                        " dateOfBirth," +
                                        " address1," +
                                        " address2," +
                                        " address3," +
                                        " countryCode," +
                                        " stateCode," +
                                        " cityCode," +
                                        " districtCode," +
                                        " pinCode," +
                                        " phone1," +
                                        " phone2," +
                                        " email," +
                                        " customerType," +
                                        " fatherOrSpouseFirstName," +
                                        " fatherOrSpouseMiddleName," +
                                        " fatherOrSpouseLastName," +
                                        " motherFirstName," +
                                        " motherMiddleName," +
                                        " motherLastName," +
                                        " salutation," +
                                        " gender," +
                                        " nationality," +
                                        " caste," +
                                        " religion," +
                                        " domicileStatus," +
                                        " guardianFirstName," +
                                        " guardianMiddleName," +
                                        " guardianLastName," +
                                        " guardianAddress1," +
                                        " guardianAddress2," +
                                        " guardianAddress3," +
                                        " guardianCountryCode," +
                                        " guardianStateCode," +
                                        " guardianCityCode," +
                                        " guardianDistrictCode," +
                                        " guardianPinCode," +
                                        " guardianRelationshipWithCustomer," +
                                        " guardianDateOfBirth," +
                                        " guardianSalutation," +
                                        " guardianGender," +
                                        " guardianCustomerId," +
                                        " pan," +
                                        " uidNum," +
                                        " gstin," +
                                        " registrationNo," +
                                        " cin," +
                                        " legalEntityIdentifier," +
                                        " tag," +
                                        " documentType," +
                                        " proofType," +
                                        " documentNumber," +
                                        " dateOfIssue," +
                                        " dateOfExpiry," +
                                        " filePath," +
                                        " occupation," +
                                        " employment," +
                                        " sector," +
                                        " residenceType," +
                                        " language," +
                                        " languageProficiency," +
                                        " education," +
                                        " annualIncome," +
                                        " segment," +
                                        " addressType," +
                                        " organizationName," +
                                        " alt1Address1," +
                                        " alt1Address2," +
                                        " alt1Address3," +
                                        " alt1CityCode," +
                                        " alt1DistrictCode," +
                                        " alt1StateCode," +
                                        " alt1CountryCode," +
                                        " alt1PinCode," +
                                        " altAddressType1," +
                                        " alt1Phone1," +
                                        " alt1Phone2," +
                                        " alt1Email," +
                                        " alt2Address1," +
                                        " alt2Address2," +
                                        " alt2Address3," +
                                        " alt2CityCode," +
                                        " alt2DistrictCode," +
                                        " alt2StateCode," +
                                        " alt2CountryCode," +
                                        " alt2PinCode," +
                                        " altAddressType2," +
                                        " alt2Phone1," +
                                        " alt2Phone2," +
                                        " alt2Email," +
                                        " centreName," +
                                        " centreId," +
                                        " nomineeCustomerId," +
                                        " nomineeRelationshipType," +
                                        " maritalStatus," +
                                        " bankName1," +
                                        " branchName1," +
                                        " branchAddress1," +
                                        " ifscCode1," +
                                        " micrCode1," +
                                        " accountType1," +
                                        " accountNumber1," +
                                        " accountHolderName1," +
                                        " bankName2," +
                                        " branchName2," +
                                        " branchAddress2," +
                                        " ifscCode2," +
                                        " micrCode2," +
                                        " accountType2," +
                                        " accountNumber2," +
                                        " accountHolderName2," +
                                        " bankName3," +
                                        " branchName3," +
                                        " branchAddress3," +
                                        " ifscCode3," +
                                        " micrCode3," +
                                        " accountType3," +
                                        " accountNumber3," +
                                        " accountHolderName3," +
                                        " referenceCustomerId1," +
                                        " referenceCustomerId2," +
                                        " json_string) " +
                                        " values(" +
                                        "'" + msGetGidCustCrtResp + "'," +
                                        "'" + msGetGidCustCrtReq + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].firstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].middleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].lastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].countryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].stateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].districtCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].salutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nationality + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].caste + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].religion + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].domicileStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianStateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianPinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianRelationshipWithCustomer + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianSalutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianGender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pan + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].uidNum + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gstin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].registrationNo + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].legalEntityIdentifier + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].tag + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].proofType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentNumber + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfIssue + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfExpiry + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].filePath + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].occupation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].employment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].sector + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].residenceType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].language + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].languageProficiency + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].education + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].annualIncome + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].segment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].addressType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].organizationName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeRelationshipType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].maritalStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId2 + "'," +
                                        "'" + lsresponsecontentfindcustomer + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlFindCustomerResponse.customers[0].customerId))))
                                {

                                    if (!(String.IsNullOrEmpty(values.farmercontact_gid)))
                                    {
                                        msSQL = "update ocs_trn_tfarmercontact set urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',urn_status='Yes',encorefindcust_status='Y' where farmercontact_gid='" + values.farmercontact_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        LogForAuditEncoreIntegration("Find Customer Request Ended for Individual Farmer . Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                        ObjMdlFindCustomerResponse.status = true;
                                        ObjMdlFindCustomerResponse.message = "Find Customer Response received successfully from Encore";
                                        return ObjMdlFindCustomerResponse;
                                    }

                                }
                                else
                                {
                                    LogForAuditEncoreIntegration("Error occurred while storing customer creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "Error occurred while storing response from Encore";
                                    return ObjMdlFindCustomerResponse;
                                }

                            }
                            else
                            {
                                MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                                objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentcustomercreation);

                                LogForAuditEncoreIntegration("Failed to receive 200 Response . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Find Customer in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.localizedMessage;
                                return ObjMdlFindCustomerResponse;
                            }                        
                    }

                }
               

                msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("EFCR");
                msSQL = " insert into ocs_trn_tencorefindcustomerrequest(" +
                        " encorefindcustomerrequest_gid," +
                        " application_gid," +
                        " farmercontact_gid," +
                        " request_time," +
                        " requested_by," +
                        " pan_no) " +
                        " values(" +
                        "'" + msGetGidCustCrtReq + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.farmercontact_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + lspan + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 1)
                {
                    LogForAuditEncoreIntegration("Error occurred while storing find customer request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlFindCustomerResponse.status = false;
                    ObjMdlFindCustomerResponse.message = "Error occurred while posting find customer request to Encore";
                    return ObjMdlFindCustomerResponse;
                }
                try
                {
                    
                    var param = "pan=" + lspan + "&offSet=0";

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                    string requestAddressURL = ConfigurationManager.AppSettings["encore_findcustomerurl"].ToString() + "?" + param;
                    var clientAddress = new RestClient(requestAddressURL);
                    var requestAddress = new RestRequest(Method.GET);

                    requestAddress.AddHeader("Content-Type", "application/json");
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                    string val = System.Convert.ToBase64String(plainTextBytes);
                    requestAddress.AddHeader("Authorization", "Basic " + val);

                    IRestResponse responseAddress = clientAddress.Execute(requestAddress);
                
                    lsresponsecontentfindcustomer = responseAddress.Content;
                    lsresponseStatusCodefindcustomer = responseAddress.StatusCode.ToString();

                }
                catch (Exception ex)
                {
                    LogForAuditEncoreIntegration("Error occurred while hitting Find customer URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlFindCustomerResponse.status = false;
                    ObjMdlFindCustomerResponse.message = "Error occurred posting customer creation request to Encore";
                    return ObjMdlFindCustomerResponse;
                }
                if (lsresponseStatusCodefindcustomer == "OK")
                {
                    
                    ObjMdlFindCustomerResponse = JsonConvert.DeserializeObject<MdlFindCustomerResponse>(lsresponsecontentfindcustomer);

                    if (ObjMdlFindCustomerResponse.totalResults == "0")
                    {
                        //LogForAuditEncoreIntegration("Find Customer Request Ended for Individual Farmer. Reason - No Customer Found in Encore. Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                            if (!(String.IsNullOrEmpty(values.farmercontact_gid)))
                            {
                                LogForAuditEncoreIntegration("Find Customer Request Reinitiated for Individual Farmer - Aadhar based Validation . Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                msSQL = " select application_gid,application_no,aadhar_no " +
                                        " from ocs_trn_tfarmercontact " +
                                        " where farmercontact_gid='" + values.farmercontact_gid + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaadhar_no = objODBCDatareader["aadhar_no"].ToString();
                                }
                                objODBCDatareader.Close();

                                if ((String.IsNullOrEmpty(lsaadhar_no)))
                                {
                                    msSQL = "update ocs_trn_tfarmercontact set encorefindcust_status='Y' where farmercontact_gid='" + values.farmercontact_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "Aadhar Number is Empty - Proceed with Create Customer";
                                    return ObjMdlFindCustomerResponse;
                                }

                            }
                           
                            msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("EFCR");
                            msSQL = " insert into ocs_trn_tencorefindcustomerrequest(" +
                                    " encorefindcustomerrequest_gid," +
                                    " application_gid," +
                                    " farmercontact_gid," +
                                    " request_time," +
                                    " requested_by," +
                                    " aadhar_no) " +
                                    " values(" +
                                    "'" + msGetGidCustCrtReq + "'," +
                                    "'" + values.application_gid + "'," +
                                    "'" + values.farmercontact_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsaadhar_no + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 1)
                            {
                                LogForAuditEncoreIntegration("Error occurred while storing find customer request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Error occurred while posting find customer request to Encore";
                                return ObjMdlFindCustomerResponse;
                            }
                            try
                            {

                                var param = "uid=" + lsaadhar_no + "&offSet=0";

                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                                string requestAddressURL = ConfigurationManager.AppSettings["encore_findcustomerurl"].ToString() + "?" + param;
                                var clientAddress = new RestClient(requestAddressURL);
                                var requestAddress = new RestRequest(Method.GET);

                                requestAddress.AddHeader("Content-Type", "application/json");
                                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                                string val = System.Convert.ToBase64String(plainTextBytes);
                                requestAddress.AddHeader("Authorization", "Basic " + val);

                                IRestResponse responseAddress = clientAddress.Execute(requestAddress);

                                lsresponsecontentfindcustomer = responseAddress.Content;
                                lsresponseStatusCodefindcustomer = responseAddress.StatusCode.ToString();

                            }
                            catch (Exception ex)
                            {
                                LogForAuditEncoreIntegration("Error occurred while hitting Find customer URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Error occurred posting customer creation request to Encore";
                                return ObjMdlFindCustomerResponse;
                            }
                            if (lsresponseStatusCodefindcustomer == "OK")
                            {

                                ObjMdlFindCustomerResponse = JsonConvert.DeserializeObject<MdlFindCustomerResponse>(lsresponsecontentfindcustomer);

                                if (ObjMdlFindCustomerResponse.totalResults == "0")
                                {
                                    LogForAuditEncoreIntegration("Find Customer Request Ended for Individual Farmer. Reason - No Customer Found in Encore. Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                    if (!(String.IsNullOrEmpty(values.farmercontact_gid)))
                                    {
                                        msSQL = "update ocs_trn_tfarmercontact set encorefindcust_status='Y' where farmercontact_gid='" + values.farmercontact_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                   
                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "No Customer Found in Encore";
                                    return ObjMdlFindCustomerResponse;
                                }

                                msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                                msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                                        " encorecreatecustomerresponse_gid," +
                                        " encorecreatecustomerrequest_gid," +
                                        " response_time," +
                                        " customerId," +
                                        " branchCode," +
                                        " firstName," +
                                        " middleName," +
                                        " lastName," +
                                        " dateOfBirth," +
                                        " address1," +
                                        " address2," +
                                        " address3," +
                                        " countryCode," +
                                        " stateCode," +
                                        " cityCode," +
                                        " districtCode," +
                                        " pinCode," +
                                        " phone1," +
                                        " phone2," +
                                        " email," +
                                        " customerType," +
                                        " fatherOrSpouseFirstName," +
                                        " fatherOrSpouseMiddleName," +
                                        " fatherOrSpouseLastName," +
                                        " motherFirstName," +
                                        " motherMiddleName," +
                                        " motherLastName," +
                                        " salutation," +
                                        " gender," +
                                        " nationality," +
                                        " caste," +
                                        " religion," +
                                        " domicileStatus," +
                                        " guardianFirstName," +
                                        " guardianMiddleName," +
                                        " guardianLastName," +
                                        " guardianAddress1," +
                                        " guardianAddress2," +
                                        " guardianAddress3," +
                                        " guardianCountryCode," +
                                        " guardianStateCode," +
                                        " guardianCityCode," +
                                        " guardianDistrictCode," +
                                        " guardianPinCode," +
                                        " guardianRelationshipWithCustomer," +
                                        " guardianDateOfBirth," +
                                        " guardianSalutation," +
                                        " guardianGender," +
                                        " guardianCustomerId," +
                                        " pan," +
                                        " uidNum," +
                                        " gstin," +
                                        " registrationNo," +
                                        " cin," +
                                        " legalEntityIdentifier," +
                                        " tag," +
                                        " documentType," +
                                        " proofType," +
                                        " documentNumber," +
                                        " dateOfIssue," +
                                        " dateOfExpiry," +
                                        " filePath," +
                                        " occupation," +
                                        " employment," +
                                        " sector," +
                                        " residenceType," +
                                        " language," +
                                        " languageProficiency," +
                                        " education," +
                                        " annualIncome," +
                                        " segment," +
                                        " addressType," +
                                        " organizationName," +
                                        " alt1Address1," +
                                        " alt1Address2," +
                                        " alt1Address3," +
                                        " alt1CityCode," +
                                        " alt1DistrictCode," +
                                        " alt1StateCode," +
                                        " alt1CountryCode," +
                                        " alt1PinCode," +
                                        " altAddressType1," +
                                        " alt1Phone1," +
                                        " alt1Phone2," +
                                        " alt1Email," +
                                        " alt2Address1," +
                                        " alt2Address2," +
                                        " alt2Address3," +
                                        " alt2CityCode," +
                                        " alt2DistrictCode," +
                                        " alt2StateCode," +
                                        " alt2CountryCode," +
                                        " alt2PinCode," +
                                        " altAddressType2," +
                                        " alt2Phone1," +
                                        " alt2Phone2," +
                                        " alt2Email," +
                                        " centreName," +
                                        " centreId," +
                                        " nomineeCustomerId," +
                                        " nomineeRelationshipType," +
                                        " maritalStatus," +
                                        " bankName1," +
                                        " branchName1," +
                                        " branchAddress1," +
                                        " ifscCode1," +
                                        " micrCode1," +
                                        " accountType1," +
                                        " accountNumber1," +
                                        " accountHolderName1," +
                                        " bankName2," +
                                        " branchName2," +
                                        " branchAddress2," +
                                        " ifscCode2," +
                                        " micrCode2," +
                                        " accountType2," +
                                        " accountNumber2," +
                                        " accountHolderName2," +
                                        " bankName3," +
                                        " branchName3," +
                                        " branchAddress3," +
                                        " ifscCode3," +
                                        " micrCode3," +
                                        " accountType3," +
                                        " accountNumber3," +
                                        " accountHolderName3," +
                                        " referenceCustomerId1," +
                                        " referenceCustomerId2," +
                                        " json_string) " +
                                        " values(" +
                                        "'" + msGetGidCustCrtResp + "'," +
                                        "'" + msGetGidCustCrtReq + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].firstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].middleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].lastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].countryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].stateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].districtCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].salutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nationality + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].caste + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].religion + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].domicileStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianStateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianPinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianRelationshipWithCustomer + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianSalutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianGender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pan + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].uidNum + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gstin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].registrationNo + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].legalEntityIdentifier + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].tag + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].proofType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentNumber + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfIssue + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfExpiry + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].filePath + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].occupation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].employment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].sector + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].residenceType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].language + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].languageProficiency + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].education + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].annualIncome + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].segment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].addressType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].organizationName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeRelationshipType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].maritalStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId2 + "'," +
                                        "'" + lsresponsecontentfindcustomer + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlFindCustomerResponse.customers[0].customerId))))
                                {

                                    if (!(String.IsNullOrEmpty(values.farmercontact_gid)))
                                    {
                                        msSQL = "update ocs_trn_tfarmercontact set urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',urn_status='Yes',encorefindcust_status='Y' where farmercontact_gid='" + values.farmercontact_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        LogForAuditEncoreIntegration("Find Customer Request Ended for Individual Farmer . Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                        ObjMdlFindCustomerResponse.status = true;
                                        ObjMdlFindCustomerResponse.message = "Find Customer Response received successfully from Encore";
                                        return ObjMdlFindCustomerResponse;
                                    }
                                   
                                }
                                else
                                {
                                    LogForAuditEncoreIntegration("Error occurred while storing customer creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "Error occurred while storing response from Encore";
                                    return ObjMdlFindCustomerResponse;
                                }

                            }
                            else
                            {
                                MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                                objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentcustomercreation);

                                LogForAuditEncoreIntegration("Failed to receive 200 Response . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Find Customer in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.localizedMessage;
                                return ObjMdlFindCustomerResponse;
                            }
                        
                    }

                    msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                    msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                            " encorecreatecustomerresponse_gid," +
                            " encorecreatecustomerrequest_gid," +
                            " response_time," +
                            " customerId," +
                            " branchCode," +
                            " firstName," +
                            " middleName," +
                            " lastName," +
                            " dateOfBirth," +
                            " address1," +
                            " address2," +
                            " address3," +
                            " countryCode," +
                            " stateCode," +
                            " cityCode," +
                            " districtCode," +
                            " pinCode," +
                            " phone1," +
                            " phone2," +
                            " email," +
                            " customerType," +
                            " fatherOrSpouseFirstName," +
                            " fatherOrSpouseMiddleName," +
                            " fatherOrSpouseLastName," +
                            " motherFirstName," +
                            " motherMiddleName," +
                            " motherLastName," +
                            " salutation," +
                            " gender," +
                            " nationality," +
                            " caste," +
                            " religion," +
                            " domicileStatus," +
                            " guardianFirstName," +
                            " guardianMiddleName," +
                            " guardianLastName," +
                            " guardianAddress1," +
                            " guardianAddress2," +
                            " guardianAddress3," +
                            " guardianCountryCode," +
                            " guardianStateCode," +
                            " guardianCityCode," +
                            " guardianDistrictCode," +
                            " guardianPinCode," +
                            " guardianRelationshipWithCustomer," +
                            " guardianDateOfBirth," +
                            " guardianSalutation," +
                            " guardianGender," +
                            " guardianCustomerId," +
                            " pan," +
                            " uidNum," +
                            " gstin," +
                            " registrationNo," +
                            " cin," +
                            " legalEntityIdentifier," +
                            " tag," +
                            " documentType," +
                            " proofType," +
                            " documentNumber," +
                            " dateOfIssue," +
                            " dateOfExpiry," +
                            " filePath," +
                            " occupation," +
                            " employment," +
                            " sector," +
                            " residenceType," +
                            " language," +
                            " languageProficiency," +
                            " education," +
                            " annualIncome," +
                            " segment," +
                            " addressType," +
                            " organizationName," +
                            " alt1Address1," +
                            " alt1Address2," +
                            " alt1Address3," +
                            " alt1CityCode," +
                            " alt1DistrictCode," +
                            " alt1StateCode," +
                            " alt1CountryCode," +
                            " alt1PinCode," +
                            " altAddressType1," +
                            " alt1Phone1," +
                            " alt1Phone2," +
                            " alt1Email," +
                            " alt2Address1," +
                            " alt2Address2," +
                            " alt2Address3," +
                            " alt2CityCode," +
                            " alt2DistrictCode," +
                            " alt2StateCode," +
                            " alt2CountryCode," +
                            " alt2PinCode," +
                            " altAddressType2," +
                            " alt2Phone1," +
                            " alt2Phone2," +
                            " alt2Email," +
                            " centreName," +
                            " centreId," +
                            " nomineeCustomerId," +
                            " nomineeRelationshipType," +
                            " maritalStatus," +
                            " bankName1," +
                            " branchName1," +
                            " branchAddress1," +
                            " ifscCode1," +
                            " micrCode1," +
                            " accountType1," +
                            " accountNumber1," +
                            " accountHolderName1," +
                            " bankName2," +
                            " branchName2," +
                            " branchAddress2," +
                            " ifscCode2," +
                            " micrCode2," +
                            " accountType2," +
                            " accountNumber2," +
                            " accountHolderName2," +
                            " bankName3," +
                            " branchName3," +
                            " branchAddress3," +
                            " ifscCode3," +
                            " micrCode3," +
                            " accountType3," +
                            " accountNumber3," +
                            " accountHolderName3," +
                            " referenceCustomerId1," +
                            " referenceCustomerId2," +
                            " json_string) " +
                            " values(" +
                            "'" + msGetGidCustCrtResp + "'," +
                            "'" + msGetGidCustCrtReq + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].customerId + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].firstName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].middleName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].lastName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].dateOfBirth + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].address1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].address2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].address3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].countryCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].stateCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].cityCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].districtCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].pinCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].phone1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].phone2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].email + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].customerType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseFirstName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseMiddleName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseLastName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].motherFirstName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].motherMiddleName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].motherLastName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].salutation + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].gender + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].nationality + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].caste + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].religion + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].domicileStatus + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianFirstName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianMiddleName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianLastName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianCountryCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianStateCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianCityCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianDistrictCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianPinCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianRelationshipWithCustomer + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianDateOfBirth + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianSalutation + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianGender + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianCustomerId + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].pan + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].uidNum + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].gstin + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].registrationNo + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].cin + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].legalEntityIdentifier + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].tag + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].documentType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].proofType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].documentNumber + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].dateOfIssue + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].dateOfExpiry + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].filePath + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].occupation + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].employment + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].sector + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].residenceType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].language + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].languageProficiency + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].education + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].annualIncome + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].segment + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].addressType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].organizationName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1CityCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1DistrictCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1StateCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1CountryCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1PinCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Email + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2CityCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2DistrictCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2StateCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2CountryCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2PinCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Email + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].centreName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].centreId + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].nomineeCustomerId + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].nomineeRelationshipType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].maritalStatus + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].bankName1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchName1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].micrCode1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountType1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].bankName2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchName2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].micrCode2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountType2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].bankName3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchName3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].micrCode3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountType3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId2 + "'," +
                            "'" + lsresponsecontentfindcustomer + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlFindCustomerResponse.customers[0].customerId))))
                    {

                            msSQL = "update ocs_trn_tfarmercontact set urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',urn_status='Yes',encorefindcust_status='Y' where farmercontact_gid='" + values.farmercontact_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            LogForAuditEncoreIntegration("Find Customer Request Ended for Individual Farmer . Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            ObjMdlFindCustomerResponse.status = true;
                            ObjMdlFindCustomerResponse.message = "Find Customer Response received successfully from Encore";
                            return ObjMdlFindCustomerResponse;
                                               
                    }
                    else
                    {
                        LogForAuditEncoreIntegration("Error occurred while storing customer creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        ObjMdlFindCustomerResponse.status = false;
                        ObjMdlFindCustomerResponse.message = "Error occurred while storing response from Encore";
                        return ObjMdlFindCustomerResponse;
                    }

                }
                else
                {
                    MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                    objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentcustomercreation);

                    LogForAuditEncoreIntegration("Failed to receive 200 Response . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlFindCustomerResponse.status = false;
                    ObjMdlFindCustomerResponse.message = "Find Customer in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.localizedMessage;
                    return ObjMdlFindCustomerResponse;
                }




            }
            catch (Exception ex)
            {
                LogForAuditEncoreIntegration("Error occurred posting customer creation request  to Encore . Exception - " + ex + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                ObjMdlFindCustomerResponse.status = false;
                ObjMdlFindCustomerResponse.message = "Error occurred posting customer creation request to Encore";
                return ObjMdlFindCustomerResponse;
            }

        }

        //Find Customer in Encore Applicant
        public MdlFindCustomerResponse DaPostFindCustomerEncoreApplicant(MdlcustomercreationLMSAPI values, string employee_gid)
        {
            string type = "FindCustomer";
            MdlFindCustomerResponse ObjMdlFindCustomerResponse = new MdlFindCustomerResponse();
            try
            {


                    LogForAuditEncoreIntegration("Find Customer Request Initiated for Applicant . Applicant ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                    msSQL = " select stakeholder_type,application_gid from ocs_trn_tcadinstitution where  application_gid='" + values.application_gid + "'" +
                    " and stakeholder_type= 'Applicant' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select application_gid,application_no,companypan_no " +
                                " from ocs_trn_tcadinstitution " +
                                " where application_gid='" + values.application_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lspan = objODBCDatareader["companypan_no"].ToString();
                        }
                        objODBCDatareader.Close();

                        if ((String.IsNullOrEmpty(lspan)))
                        {
                            msSQL = "update ocs_trn_tcustomercreationlms set encorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            ObjMdlFindCustomerResponse.status = false;
                            ObjMdlFindCustomerResponse.message = "Pan Number is Empty - Proceed with Create Customer";
                            return ObjMdlFindCustomerResponse;
                        }
                    }

                    msSQL = " select stakeholder_type,application_gid from ocs_trn_tcadcontact where  application_gid='" + values.application_gid + "'" +
                   " and stakeholder_type= 'Applicant' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " select application_gid,application_no,pan_no " +
                                " from ocs_trn_tcadcontact " +
                                " where application_gid='" + values.application_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lspan = objODBCDatareader["pan_no"].ToString();
                        }
                        objODBCDatareader.Close();

                        if ((String.IsNullOrEmpty(lspan)))
                        {

                                LogForAuditEncoreIntegration("Find Customer Request Reinitiated for Applicant - Aadhar based Validation . Applicant ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                msSQL = " select stakeholder_type,application_gid from ocs_trn_tcadcontact where  application_gid='" + values.application_gid + "'" +
                               " and stakeholder_type= 'Applicant' ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                                if (objODBCDatareader.HasRows == true)
                                {
                                    msSQL = " select application_gid,application_no,aadhar_no " +
                                            " from ocs_trn_tcadcontact " +
                                            " where application_gid='" + values.application_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsaadhar_no = objODBCDatareader["aadhar_no"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    if ((String.IsNullOrEmpty(lsaadhar_no)))
                                    {
                                        msSQL = "update ocs_trn_tcustomercreationlms set encorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        ObjMdlFindCustomerResponse.status = false;
                                        ObjMdlFindCustomerResponse.message = "Aadhar Number is Empty - Proceed with Create Customer";
                                        return ObjMdlFindCustomerResponse;
                                    }
                                }

                            
                            msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("EFCR");
                            msSQL = " insert into ocs_trn_tencorefindcustomerrequest(" +
                                    " encorefindcustomerrequest_gid," +
                                    " application_gid," +
                                    " farmercontact_gid," +
                                    " request_time," +
                                    " requested_by," +
                                    " aadhar_no) " +
                                    " values(" +
                                    "'" + msGetGidCustCrtReq + "'," +
                                    "'" + values.application_gid + "'," +
                                    "'" + values.farmercontact_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsaadhar_no + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 1)
                            {
                                LogForAuditEncoreIntegration("Error occurred while storing find customer request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Error occurred while posting find customer request to Encore";
                                return ObjMdlFindCustomerResponse;
                            }
                            try
                            {

                                var param = "uid=" + lsaadhar_no + "&offSet=0";

                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                                string requestAddressURL = ConfigurationManager.AppSettings["encore_findcustomerurl"].ToString() + "?" + param;
                                var clientAddress = new RestClient(requestAddressURL);
                                var requestAddress = new RestRequest(Method.GET);

                                requestAddress.AddHeader("Content-Type", "application/json");
                                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                                string val = System.Convert.ToBase64String(plainTextBytes);
                                requestAddress.AddHeader("Authorization", "Basic " + val);

                                IRestResponse responseAddress = clientAddress.Execute(requestAddress);

                                lsresponsecontentfindcustomer = responseAddress.Content;
                                lsresponseStatusCodefindcustomer = responseAddress.StatusCode.ToString();

                            }
                            catch (Exception ex)
                            {
                                LogForAuditEncoreIntegration("Error occurred while hitting Find customer URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Error occurred posting customer creation request to Encore";
                                return ObjMdlFindCustomerResponse;
                            }
                            if (lsresponseStatusCodefindcustomer == "OK")
                            {

                                ObjMdlFindCustomerResponse = JsonConvert.DeserializeObject<MdlFindCustomerResponse>(lsresponsecontentfindcustomer);

                                if (ObjMdlFindCustomerResponse.totalResults == "0")
                                {
                                    LogForAuditEncoreIntegration("Find Customer Request Ended for Individual Applicant. Reason - No Customer Found in Encore. Farmer ID - " + values.farmercontact_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                    
                                    if (!(String.IsNullOrEmpty(values.application_gid)))
                                    {
                                        msSQL = "update ocs_trn_tcustomercreationlms set encorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }

                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "No Customer Found in Encore";
                                    return ObjMdlFindCustomerResponse;
                                }

                                msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                                msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                                        " encorecreatecustomerresponse_gid," +
                                        " encorecreatecustomerrequest_gid," +
                                        " response_time," +
                                        " customerId," +
                                        " branchCode," +
                                        " firstName," +
                                        " middleName," +
                                        " lastName," +
                                        " dateOfBirth," +
                                        " address1," +
                                        " address2," +
                                        " address3," +
                                        " countryCode," +
                                        " stateCode," +
                                        " cityCode," +
                                        " districtCode," +
                                        " pinCode," +
                                        " phone1," +
                                        " phone2," +
                                        " email," +
                                        " customerType," +
                                        " fatherOrSpouseFirstName," +
                                        " fatherOrSpouseMiddleName," +
                                        " fatherOrSpouseLastName," +
                                        " motherFirstName," +
                                        " motherMiddleName," +
                                        " motherLastName," +
                                        " salutation," +
                                        " gender," +
                                        " nationality," +
                                        " caste," +
                                        " religion," +
                                        " domicileStatus," +
                                        " guardianFirstName," +
                                        " guardianMiddleName," +
                                        " guardianLastName," +
                                        " guardianAddress1," +
                                        " guardianAddress2," +
                                        " guardianAddress3," +
                                        " guardianCountryCode," +
                                        " guardianStateCode," +
                                        " guardianCityCode," +
                                        " guardianDistrictCode," +
                                        " guardianPinCode," +
                                        " guardianRelationshipWithCustomer," +
                                        " guardianDateOfBirth," +
                                        " guardianSalutation," +
                                        " guardianGender," +
                                        " guardianCustomerId," +
                                        " pan," +
                                        " uidNum," +
                                        " gstin," +
                                        " registrationNo," +
                                        " cin," +
                                        " legalEntityIdentifier," +
                                        " tag," +
                                        " documentType," +
                                        " proofType," +
                                        " documentNumber," +
                                        " dateOfIssue," +
                                        " dateOfExpiry," +
                                        " filePath," +
                                        " occupation," +
                                        " employment," +
                                        " sector," +
                                        " residenceType," +
                                        " language," +
                                        " languageProficiency," +
                                        " education," +
                                        " annualIncome," +
                                        " segment," +
                                        " addressType," +
                                        " organizationName," +
                                        " alt1Address1," +
                                        " alt1Address2," +
                                        " alt1Address3," +
                                        " alt1CityCode," +
                                        " alt1DistrictCode," +
                                        " alt1StateCode," +
                                        " alt1CountryCode," +
                                        " alt1PinCode," +
                                        " altAddressType1," +
                                        " alt1Phone1," +
                                        " alt1Phone2," +
                                        " alt1Email," +
                                        " alt2Address1," +
                                        " alt2Address2," +
                                        " alt2Address3," +
                                        " alt2CityCode," +
                                        " alt2DistrictCode," +
                                        " alt2StateCode," +
                                        " alt2CountryCode," +
                                        " alt2PinCode," +
                                        " altAddressType2," +
                                        " alt2Phone1," +
                                        " alt2Phone2," +
                                        " alt2Email," +
                                        " centreName," +
                                        " centreId," +
                                        " nomineeCustomerId," +
                                        " nomineeRelationshipType," +
                                        " maritalStatus," +
                                        " bankName1," +
                                        " branchName1," +
                                        " branchAddress1," +
                                        " ifscCode1," +
                                        " micrCode1," +
                                        " accountType1," +
                                        " accountNumber1," +
                                        " accountHolderName1," +
                                        " bankName2," +
                                        " branchName2," +
                                        " branchAddress2," +
                                        " ifscCode2," +
                                        " micrCode2," +
                                        " accountType2," +
                                        " accountNumber2," +
                                        " accountHolderName2," +
                                        " bankName3," +
                                        " branchName3," +
                                        " branchAddress3," +
                                        " ifscCode3," +
                                        " micrCode3," +
                                        " accountType3," +
                                        " accountNumber3," +
                                        " accountHolderName3," +
                                        " referenceCustomerId1," +
                                        " referenceCustomerId2," +
                                        " json_string) " +
                                        " values(" +
                                        "'" + msGetGidCustCrtResp + "'," +
                                        "'" + msGetGidCustCrtReq + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].firstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].middleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].lastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].countryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].stateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].districtCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].salutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nationality + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].caste + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].religion + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].domicileStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianStateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianPinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianRelationshipWithCustomer + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianSalutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianGender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pan + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].uidNum + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gstin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].registrationNo + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].legalEntityIdentifier + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].tag + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].proofType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentNumber + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfIssue + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfExpiry + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].filePath + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].occupation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].employment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].sector + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].residenceType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].language + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].languageProficiency + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].education + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].annualIncome + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].segment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].addressType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].organizationName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeRelationshipType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].maritalStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId2 + "'," +
                                        "'" + lsresponsecontentfindcustomer + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlFindCustomerResponse.customers[0].customerId))))
                                {


                                        msSQL = "update ocs_trn_tcustomercreationlms set customer_urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',lms_status='Businessops',update_flag='Y',encorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        if (mnResult != 0)
                                        {
                                            
                                            msSQL = "select count(contact_gid) from ocs_trn_tcadcontact where application_gid='" + values.application_gid + "'and stakeholder_type='Applicant'";
                                            lscont_idcount = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));
                                            if (lscont_idcount != 0)
                                            {
                                                msSQL = "select contact_gid from ocs_trn_tcadcontact where application_gid='" + values.application_gid + "'and stakeholder_type='Applicant'";
                                                lscont_id = objdbconn.GetExecuteScalar(msSQL);
                                                if (lscont_id != "" || lscont_id != null)
                                                {
                                                    msSQL = "update ocs_trn_tcadcontact set urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',urn_status ='Yes' where contact_gid='" + lscont_id + "'";
                                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                    msSQL = "update ocs_trn_tcadapplication set customer_urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "' where application_gid='" + values.application_gid + "'";
                                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                }
                                            }

                                            LogForAuditEncoreIntegration("Find Customer Request Ended for Applicant . Applicant ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                            ObjMdlFindCustomerResponse.status = true;
                                            ObjMdlFindCustomerResponse.message = "Find Customer Response received successfully from Encore";
                                            return ObjMdlFindCustomerResponse;
                                        }
                                        else
                                        {
                                            ObjMdlFindCustomerResponse.status = false;
                                            ObjMdlFindCustomerResponse.message = "Error occurred while storing response from Encore";
                                            return ObjMdlFindCustomerResponse;
                                        }

                                    //}

                                }
                                else
                                {
                                    LogForAuditEncoreIntegration("Error occurred while storing customer creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "Error occurred while storing response from Encore";
                                    return ObjMdlFindCustomerResponse;
                                }

                            }
                            else
                            {
                                MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                                objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentcustomercreation);

                                LogForAuditEncoreIntegration("Failed to receive 200 Response . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Find Customer in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.localizedMessage;
                                return ObjMdlFindCustomerResponse;
                            }
                        
                    }
                    }

                

                msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("EFCR");
                msSQL = " insert into ocs_trn_tencorefindcustomerrequest(" +
                        " encorefindcustomerrequest_gid," +
                        " application_gid," +
                        " farmercontact_gid," +
                        " request_time," +
                        " requested_by," +
                        " pan_no) " +
                        " values(" +
                        "'" + msGetGidCustCrtReq + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.farmercontact_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + lspan + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 1)
                {
                    LogForAuditEncoreIntegration("Error occurred while storing find customer request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlFindCustomerResponse.status = false;
                    ObjMdlFindCustomerResponse.message = "Error occurred while posting find customer request to Encore";
                    return ObjMdlFindCustomerResponse;
                }
                try
                {

                    var param = "pan=" + lspan + "&offSet=0";

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                    string requestAddressURL = ConfigurationManager.AppSettings["encore_findcustomerurl"].ToString() + "?" + param;
                    var clientAddress = new RestClient(requestAddressURL);
                    var requestAddress = new RestRequest(Method.GET);

                    requestAddress.AddHeader("Content-Type", "application/json");
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                    string val = System.Convert.ToBase64String(plainTextBytes);
                    requestAddress.AddHeader("Authorization", "Basic " + val);

                    IRestResponse responseAddress = clientAddress.Execute(requestAddress);

                    lsresponsecontentfindcustomer = responseAddress.Content;
                    lsresponseStatusCodefindcustomer = responseAddress.StatusCode.ToString();

                }
                catch (Exception ex)
                {
                    LogForAuditEncoreIntegration("Error occurred while hitting Find customer URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlFindCustomerResponse.status = false;
                    ObjMdlFindCustomerResponse.message = "Error occurred posting customer creation request to Encore";
                    return ObjMdlFindCustomerResponse;
                }
                if (lsresponseStatusCodefindcustomer == "OK")
                {

                    ObjMdlFindCustomerResponse = JsonConvert.DeserializeObject<MdlFindCustomerResponse>(lsresponsecontentfindcustomer);

                    if (ObjMdlFindCustomerResponse.totalResults == "0")
                    {
                       
                            msSQL = " select stakeholder_type,application_gid from ocs_trn_tcadinstitution where  application_gid='" + values.application_gid + "'" +
                                    " and stakeholder_type= 'Applicant' ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);

                            if (objODBCDatareader.HasRows == true)
                            {
                                LogForAuditEncoreIntegration("Find Customer Request Ended for Applicant . Applicant ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                msSQL = "update ocs_trn_tcustomercreationlms set encorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "No Customer Found in Encore"; ;
                                return ObjMdlFindCustomerResponse;
                            }
                           
                            LogForAuditEncoreIntegration("Find Customer Request Initiated for Applicant - Aadhar based Validation . Applicant ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                            msSQL = " select stakeholder_type,application_gid from ocs_trn_tcadcontact where  application_gid='" + values.application_gid + "'" +
                           " and stakeholder_type= 'Applicant' ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);

                            if (objODBCDatareader.HasRows == true)
                            {
                                msSQL = " select application_gid,application_no,aadhar_no " +
                                        " from ocs_trn_tcadcontact " +
                                        " where application_gid='" + values.application_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaadhar_no = objODBCDatareader["aadhar_no"].ToString();
                                }
                                objODBCDatareader.Close();

                                if ((String.IsNullOrEmpty(lsaadhar_no)))
                                {
                                    msSQL = "update ocs_trn_tcustomercreationlms set encorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "Aadhar Number is Empty - Proceed with Create Customer";
                                    return ObjMdlFindCustomerResponse;
                                }
                            }
                        
                            
                            msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("EFCR");
                            msSQL = " insert into ocs_trn_tencorefindcustomerrequest(" +
                                    " encorefindcustomerrequest_gid," +
                                    " application_gid," +
                                    " farmercontact_gid," +
                                    " request_time," +
                                    " requested_by," +
                                    " aadhar_no) " +
                                    " values(" +
                                    "'" + msGetGidCustCrtReq + "'," +
                                    "'" + values.application_gid + "'," +
                                    "'" + values.farmercontact_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsaadhar_no + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 1)
                            {
                                LogForAuditEncoreIntegration("Error occurred while storing find customer request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Error occurred while posting find customer request to Encore";
                                return ObjMdlFindCustomerResponse;
                            }
                            try
                            {

                                var param = "uid=" + lsaadhar_no + "&offSet=0";

                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                                string requestAddressURL = ConfigurationManager.AppSettings["encore_findcustomerurl"].ToString() + "?" + param;
                                var clientAddress = new RestClient(requestAddressURL);
                                var requestAddress = new RestRequest(Method.GET);

                                requestAddress.AddHeader("Content-Type", "application/json");
                                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                                string val = System.Convert.ToBase64String(plainTextBytes);
                                requestAddress.AddHeader("Authorization", "Basic " + val);

                                IRestResponse responseAddress = clientAddress.Execute(requestAddress);

                                lsresponsecontentfindcustomer = responseAddress.Content;
                                lsresponseStatusCodefindcustomer = responseAddress.StatusCode.ToString();

                            }
                            catch (Exception ex)
                            {
                                LogForAuditEncoreIntegration("Error occurred while hitting Find customer URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Error occurred posting customer creation request to Encore";
                                return ObjMdlFindCustomerResponse;
                            }
                            if (lsresponseStatusCodefindcustomer == "OK")
                            {

                                ObjMdlFindCustomerResponse = JsonConvert.DeserializeObject<MdlFindCustomerResponse>(lsresponsecontentfindcustomer);

                                if (ObjMdlFindCustomerResponse.totalResults == "0")
                                {
                                    LogForAuditEncoreIntegration("Find Customer Request Ended for Individual Applicant. Reason - No Customer Found in Encore. Farmer ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                    
                                    if (!(String.IsNullOrEmpty(values.application_gid)))
                                    {
                                        msSQL = "update ocs_trn_tcustomercreationlms set encorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }

                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "No Customer Found in Encore";
                                    return ObjMdlFindCustomerResponse;
                                }

                                msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                                msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                                        " encorecreatecustomerresponse_gid," +
                                        " encorecreatecustomerrequest_gid," +
                                        " response_time," +
                                        " customerId," +
                                        " branchCode," +
                                        " firstName," +
                                        " middleName," +
                                        " lastName," +
                                        " dateOfBirth," +
                                        " address1," +
                                        " address2," +
                                        " address3," +
                                        " countryCode," +
                                        " stateCode," +
                                        " cityCode," +
                                        " districtCode," +
                                        " pinCode," +
                                        " phone1," +
                                        " phone2," +
                                        " email," +
                                        " customerType," +
                                        " fatherOrSpouseFirstName," +
                                        " fatherOrSpouseMiddleName," +
                                        " fatherOrSpouseLastName," +
                                        " motherFirstName," +
                                        " motherMiddleName," +
                                        " motherLastName," +
                                        " salutation," +
                                        " gender," +
                                        " nationality," +
                                        " caste," +
                                        " religion," +
                                        " domicileStatus," +
                                        " guardianFirstName," +
                                        " guardianMiddleName," +
                                        " guardianLastName," +
                                        " guardianAddress1," +
                                        " guardianAddress2," +
                                        " guardianAddress3," +
                                        " guardianCountryCode," +
                                        " guardianStateCode," +
                                        " guardianCityCode," +
                                        " guardianDistrictCode," +
                                        " guardianPinCode," +
                                        " guardianRelationshipWithCustomer," +
                                        " guardianDateOfBirth," +
                                        " guardianSalutation," +
                                        " guardianGender," +
                                        " guardianCustomerId," +
                                        " pan," +
                                        " uidNum," +
                                        " gstin," +
                                        " registrationNo," +
                                        " cin," +
                                        " legalEntityIdentifier," +
                                        " tag," +
                                        " documentType," +
                                        " proofType," +
                                        " documentNumber," +
                                        " dateOfIssue," +
                                        " dateOfExpiry," +
                                        " filePath," +
                                        " occupation," +
                                        " employment," +
                                        " sector," +
                                        " residenceType," +
                                        " language," +
                                        " languageProficiency," +
                                        " education," +
                                        " annualIncome," +
                                        " segment," +
                                        " addressType," +
                                        " organizationName," +
                                        " alt1Address1," +
                                        " alt1Address2," +
                                        " alt1Address3," +
                                        " alt1CityCode," +
                                        " alt1DistrictCode," +
                                        " alt1StateCode," +
                                        " alt1CountryCode," +
                                        " alt1PinCode," +
                                        " altAddressType1," +
                                        " alt1Phone1," +
                                        " alt1Phone2," +
                                        " alt1Email," +
                                        " alt2Address1," +
                                        " alt2Address2," +
                                        " alt2Address3," +
                                        " alt2CityCode," +
                                        " alt2DistrictCode," +
                                        " alt2StateCode," +
                                        " alt2CountryCode," +
                                        " alt2PinCode," +
                                        " altAddressType2," +
                                        " alt2Phone1," +
                                        " alt2Phone2," +
                                        " alt2Email," +
                                        " centreName," +
                                        " centreId," +
                                        " nomineeCustomerId," +
                                        " nomineeRelationshipType," +
                                        " maritalStatus," +
                                        " bankName1," +
                                        " branchName1," +
                                        " branchAddress1," +
                                        " ifscCode1," +
                                        " micrCode1," +
                                        " accountType1," +
                                        " accountNumber1," +
                                        " accountHolderName1," +
                                        " bankName2," +
                                        " branchName2," +
                                        " branchAddress2," +
                                        " ifscCode2," +
                                        " micrCode2," +
                                        " accountType2," +
                                        " accountNumber2," +
                                        " accountHolderName2," +
                                        " bankName3," +
                                        " branchName3," +
                                        " branchAddress3," +
                                        " ifscCode3," +
                                        " micrCode3," +
                                        " accountType3," +
                                        " accountNumber3," +
                                        " accountHolderName3," +
                                        " referenceCustomerId1," +
                                        " referenceCustomerId2," +
                                        " json_string) " +
                                        " values(" +
                                        "'" + msGetGidCustCrtResp + "'," +
                                        "'" + msGetGidCustCrtReq + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].firstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].middleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].lastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].countryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].stateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].districtCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].salutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nationality + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].caste + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].religion + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].domicileStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianStateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianPinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianRelationshipWithCustomer + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianSalutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianGender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pan + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].uidNum + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gstin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].registrationNo + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].legalEntityIdentifier + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].tag + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].proofType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentNumber + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfIssue + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfExpiry + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].filePath + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].occupation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].employment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].sector + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].residenceType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].language + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].languageProficiency + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].education + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].annualIncome + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].segment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].addressType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].organizationName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeRelationshipType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].maritalStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId2 + "'," +
                                        "'" + lsresponsecontentfindcustomer + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlFindCustomerResponse.customers[0].customerId))))
                                {


                                        msSQL = "update ocs_trn_tcustomercreationlms set customer_urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',lms_status='Businessops',update_flag='Y',encorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        if (mnResult != 0)
                                        {

                                            msSQL = "select count(contact_gid) from ocs_trn_tcadcontact where application_gid='" + values.application_gid + "'and stakeholder_type='Applicant'";
                                            lscont_idcount = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));
                                            if (lscont_idcount != 0)
                                            {
                                                msSQL = "select contact_gid from ocs_trn_tcadcontact where application_gid='" + values.application_gid + "'and stakeholder_type='Applicant'";
                                                lscont_id = objdbconn.GetExecuteScalar(msSQL);
                                                if (lscont_id != "" || lscont_id != null)
                                                {
                                                    msSQL = "update ocs_trn_tcadcontact set urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',urn_status ='Yes' where contact_gid='" + lscont_id + "'";
                                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                    msSQL = "update ocs_trn_tcadapplication set customer_urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "' where application_gid='" + values.application_gid + "'";
                                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                }
                                            }

                                            LogForAuditEncoreIntegration("Find Customer Request Ended for Applicant . Applicant ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                            ObjMdlFindCustomerResponse.status = true;
                                            ObjMdlFindCustomerResponse.message = "Find Customer Response received successfully from Encore";
                                            return ObjMdlFindCustomerResponse;
                                        }
                                        else
                                        {
                                            ObjMdlFindCustomerResponse.status = false;
                                            ObjMdlFindCustomerResponse.message = "Error occurred while storing response from Encore";
                                            return ObjMdlFindCustomerResponse;
                                        }

                                    

                                }
                                else
                                {
                                    LogForAuditEncoreIntegration("Error occurred while storing customer creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                    ObjMdlFindCustomerResponse.status = false;
                                    ObjMdlFindCustomerResponse.message = "Error occurred while storing response from Encore";
                                    return ObjMdlFindCustomerResponse;
                                }

                            }
                            else
                            {
                                MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                                objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentcustomercreation);

                                LogForAuditEncoreIntegration("Failed to receive 200 Response . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Find Customer in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.localizedMessage;
                                return ObjMdlFindCustomerResponse;
                            }

                    }

                    msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                    msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                            " encorecreatecustomerresponse_gid," +
                            " encorecreatecustomerrequest_gid," +
                            " response_time," +
                            " customerId," +
                            " branchCode," +
                            " firstName," +
                            " middleName," +
                            " lastName," +
                            " dateOfBirth," +
                            " address1," +
                            " address2," +
                            " address3," +
                            " countryCode," +
                            " stateCode," +
                            " cityCode," +
                            " districtCode," +
                            " pinCode," +
                            " phone1," +
                            " phone2," +
                            " email," +
                            " customerType," +
                            " fatherOrSpouseFirstName," +
                            " fatherOrSpouseMiddleName," +
                            " fatherOrSpouseLastName," +
                            " motherFirstName," +
                            " motherMiddleName," +
                            " motherLastName," +
                            " salutation," +
                            " gender," +
                            " nationality," +
                            " caste," +
                            " religion," +
                            " domicileStatus," +
                            " guardianFirstName," +
                            " guardianMiddleName," +
                            " guardianLastName," +
                            " guardianAddress1," +
                            " guardianAddress2," +
                            " guardianAddress3," +
                            " guardianCountryCode," +
                            " guardianStateCode," +
                            " guardianCityCode," +
                            " guardianDistrictCode," +
                            " guardianPinCode," +
                            " guardianRelationshipWithCustomer," +
                            " guardianDateOfBirth," +
                            " guardianSalutation," +
                            " guardianGender," +
                            " guardianCustomerId," +
                            " pan," +
                            " uidNum," +
                            " gstin," +
                            " registrationNo," +
                            " cin," +
                            " legalEntityIdentifier," +
                            " tag," +
                            " documentType," +
                            " proofType," +
                            " documentNumber," +
                            " dateOfIssue," +
                            " dateOfExpiry," +
                            " filePath," +
                            " occupation," +
                            " employment," +
                            " sector," +
                            " residenceType," +
                            " language," +
                            " languageProficiency," +
                            " education," +
                            " annualIncome," +
                            " segment," +
                            " addressType," +
                            " organizationName," +
                            " alt1Address1," +
                            " alt1Address2," +
                            " alt1Address3," +
                            " alt1CityCode," +
                            " alt1DistrictCode," +
                            " alt1StateCode," +
                            " alt1CountryCode," +
                            " alt1PinCode," +
                            " altAddressType1," +
                            " alt1Phone1," +
                            " alt1Phone2," +
                            " alt1Email," +
                            " alt2Address1," +
                            " alt2Address2," +
                            " alt2Address3," +
                            " alt2CityCode," +
                            " alt2DistrictCode," +
                            " alt2StateCode," +
                            " alt2CountryCode," +
                            " alt2PinCode," +
                            " altAddressType2," +
                            " alt2Phone1," +
                            " alt2Phone2," +
                            " alt2Email," +
                            " centreName," +
                            " centreId," +
                            " nomineeCustomerId," +
                            " nomineeRelationshipType," +
                            " maritalStatus," +
                            " bankName1," +
                            " branchName1," +
                            " branchAddress1," +
                            " ifscCode1," +
                            " micrCode1," +
                            " accountType1," +
                            " accountNumber1," +
                            " accountHolderName1," +
                            " bankName2," +
                            " branchName2," +
                            " branchAddress2," +
                            " ifscCode2," +
                            " micrCode2," +
                            " accountType2," +
                            " accountNumber2," +
                            " accountHolderName2," +
                            " bankName3," +
                            " branchName3," +
                            " branchAddress3," +
                            " ifscCode3," +
                            " micrCode3," +
                            " accountType3," +
                            " accountNumber3," +
                            " accountHolderName3," +
                            " referenceCustomerId1," +
                            " referenceCustomerId2," +
                            " json_string) " +
                            " values(" +
                            "'" + msGetGidCustCrtResp + "'," +
                            "'" + msGetGidCustCrtReq + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].customerId + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].firstName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].middleName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].lastName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].dateOfBirth + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].address1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].address2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].address3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].countryCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].stateCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].cityCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].districtCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].pinCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].phone1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].phone2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].email + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].customerType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseFirstName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseMiddleName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseLastName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].motherFirstName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].motherMiddleName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].motherLastName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].salutation + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].gender + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].nationality + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].caste + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].religion + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].domicileStatus + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianFirstName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianMiddleName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianLastName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianCountryCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianStateCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianCityCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianDistrictCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianPinCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianRelationshipWithCustomer + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianDateOfBirth + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianSalutation + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianGender + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].guardianCustomerId + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].pan + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].uidNum + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].gstin + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].registrationNo + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].cin + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].legalEntityIdentifier + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].tag + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].documentType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].proofType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].documentNumber + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].dateOfIssue + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].dateOfExpiry + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].filePath + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].occupation + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].employment + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].sector + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].residenceType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].language + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].languageProficiency + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].education + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].annualIncome + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].segment + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].addressType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].organizationName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1CityCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1DistrictCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1StateCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1CountryCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1PinCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt1Email + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2CityCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2DistrictCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2StateCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2CountryCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2PinCode + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].alt2Email + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].centreName + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].centreId + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].nomineeCustomerId + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].nomineeRelationshipType + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].maritalStatus + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].bankName1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchName1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].micrCode1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountType1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].bankName2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchName2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].micrCode2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountType2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName2 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].bankName3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchName3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].micrCode3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountType3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName3 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId1 + "'," +
                            "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId2 + "'," +
                            "'" + lsresponsecontentfindcustomer + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlFindCustomerResponse.customers[0].customerId))))
                    {

                            msSQL = "update ocs_trn_tcustomercreationlms set customer_urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',lms_status='Businessops',update_flag='Y',encorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 0)
                            {
                                msSQL = "select count(institution_gid) from ocs_trn_tcadinstitution where application_gid='" + values.application_gid + "' and stakeholder_type='Applicant'";
                                lsinsti_idcount = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));
                                if (lsinsti_idcount != 0)
                                {
                                    msSQL = "select institution_gid from ocs_trn_tcadinstitution where application_gid='" + values.application_gid + "' and stakeholder_type='Applicant'";
                                    lsinsti_id = objdbconn.GetExecuteScalar(msSQL);
                                    if (lsinsti_id != "" || lsinsti_id != null)
                                    {
                                        msSQL = "update ocs_trn_tcadinstitution set urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',urn_status ='Yes' where institution_gid='" + lsinsti_id + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        msSQL = "update ocs_trn_tcadapplication set customer_urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "' where application_gid='" + values.application_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    }
                                }
                                msSQL = "select count(contact_gid) from ocs_trn_tcadcontact where application_gid='" + values.application_gid + "'and stakeholder_type='Applicant'";
                                lscont_idcount = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));
                                if (lscont_idcount != 0)
                                {
                                    msSQL = "select contact_gid from ocs_trn_tcadcontact where application_gid='" + values.application_gid + "'and stakeholder_type='Applicant'";
                                    lscont_id = objdbconn.GetExecuteScalar(msSQL);
                                    if (lscont_id != "" || lscont_id != null)
                                    {
                                        msSQL = "update ocs_trn_tcadcontact set urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',urn_status ='Yes' where contact_gid='" + lscont_id + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        msSQL = "update ocs_trn_tcadapplication set customer_urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "' where application_gid='" + values.application_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }

                                LogForAuditEncoreIntegration("Find Customer Request Ended for Applicant . Applicant ID - " + values.application_gid + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                                ObjMdlFindCustomerResponse.status = true;
                                ObjMdlFindCustomerResponse.message = "Find Customer Response received successfully from Encore";
                                return ObjMdlFindCustomerResponse;
                            }
                            else
                            {
                                ObjMdlFindCustomerResponse.status = false;
                                ObjMdlFindCustomerResponse.message = "Error occurred while storing response from Encore";
                                return ObjMdlFindCustomerResponse;
                            }

                        

                    }
                    else
                    {
                        LogForAuditEncoreIntegration("Error occurred while storing customer creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        ObjMdlFindCustomerResponse.status = false;
                        ObjMdlFindCustomerResponse.message = "Error occurred while storing response from Encore";
                        return ObjMdlFindCustomerResponse;
                    }

                }
                else
                {
                    MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                    objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentcustomercreation);

                    LogForAuditEncoreIntegration("Failed to receive 200 Response . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                    ObjMdlFindCustomerResponse.status = false;
                    ObjMdlFindCustomerResponse.message = "Find Customer in Encore failed\nEncore Response: " + objMdlEncoreErrorMessageResponse.localizedMessage;
                    return ObjMdlFindCustomerResponse;
                }




            }
            catch (Exception ex)
            {
                LogForAuditEncoreIntegration("Error occurred posting customer creation request  to Encore . Exception - " + ex + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                ObjMdlFindCustomerResponse.status = false;
                ObjMdlFindCustomerResponse.message = "Error occurred posting customer creation request to Encore";
                return ObjMdlFindCustomerResponse;
            }

        }

        //Batch Customer Creation for Farmer(B2B2C)
        public void DaBatchCustomerCreationforFarmer(MdlCreateCustomer values, string employee_gid)
        {
            string type = "Batch_CreateCustomer";
            MdlCreateCustomerResponse ObjMdlCreateCustomerResponse = new MdlCreateCustomerResponse();
            try
            {

                List<string> farmer_list = new List<string>();

                msSQL = " select farmercontact_gid " +                       
                       " from ocs_trn_tfarmercontact " +                       
                       " where urn_status='No' and (urn='' or urn is null) and rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                farmer_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("farmercontact_gid")).ToList();
                farmer_count = 0;
                total_farmer = farmer_list.Count();
                if(total_farmer == 0)
                {
                    values.status = false;
                    values.message =" Customer Creation Completed for Farmer list in current Disbursement";
                    return;
                }
                foreach (var farmer in farmer_list)
                {                    
                    MdlCreateCustomerRequest ObjMdlCreateCustomerRequest = new MdlCreateCustomerRequest();

                    lsfarmer = farmer;

                    if (!(String.IsNullOrEmpty(farmer)))
                    {
                        LogForAuditEncoreIntegration("Batch - Customer Creation Request Initiated for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                        msSQL = " select  a.application_gid,a.application_no,a.first_name,a.last_name,a.middle_name," +
                          " b.addressline1,b.addressline2,b.postal_code,c.email_address, " +
                          " a.pan_no,a.aadhar_no,a.gender_name,b.state,b.city,b.district,SUBSTRING(a.individual_dob,7) AS Year,SUBSTRING(a.individual_dob,4,2) AS Month,SUBSTRING(a.individual_dob,1,2) AS Day," +
                          " a.father_firstname,a.father_middlename,a.father_lastname,a.mother_firstname,a.mother_middlename,a.mother_lastname," +
                          " a.bankaccount_number,a.bank_name,a.branch_name,a.branch_address,a.ifsc_code,a.micr_code,a.account_type,a.accountholder_name," +
                          " f.mobile_no from ocs_trn_tfarmercontact a left join ocs_trn_tfarmercontact2email c on a.farmercontact_gid=c.farmercontact_gid " +
                          " left join ocs_trn_tfarmercontact2mobileno f on a.farmercontact_gid=f.farmercontact_gid " +
                          " left join ocs_trn_tfarmercontact2address b on a.farmercontact_gid=b.farmercontact_gid " +
                          " where f.primary_status='Yes' and a.farmercontact_gid='" + farmer + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ObjMdlCreateCustomerRequest.firstName = objODBCDatareader["first_name"].ToString();
                            ObjMdlCreateCustomerRequest.lastName = objODBCDatareader["last_name"].ToString();
                            ObjMdlCreateCustomerRequest.middleName = objODBCDatareader["middle_name"].ToString();
                            ObjMdlCreateCustomerRequest.address1 = objODBCDatareader["addressline1"].ToString();
                            ObjMdlCreateCustomerRequest.address2 = objODBCDatareader["addressline2"].ToString();
                            ObjMdlCreateCustomerRequest.branchCode = "HO";
                            ObjMdlCreateCustomerRequest.countryCode = "IN";
                            ObjMdlCreateCustomerRequest.dateOfBirth = objODBCDatareader["Year"].ToString() + "-" + objODBCDatareader["Month"].ToString() + "-" + objODBCDatareader["Day"].ToString();

                            ObjMdlCreateCustomerRequest.pinCode = objODBCDatareader["postal_code"].ToString();
                            ObjMdlCreateCustomerRequest.phone1 = objODBCDatareader["mobile_no"].ToString();
                            ObjMdlCreateCustomerRequest.email = objODBCDatareader["email_address"].ToString();
                            ObjMdlCreateCustomerRequest.fatherOrSpouseFirstName = objODBCDatareader["father_firstname"].ToString();
                            ObjMdlCreateCustomerRequest.fatherOrSpouseMiddleName = objODBCDatareader["father_middlename"].ToString();
                            ObjMdlCreateCustomerRequest.fatherOrSpouseLastName = objODBCDatareader["father_lastname"].ToString();

                            ObjMdlCreateCustomerRequest.motherFirstName = objODBCDatareader["mother_firstname"].ToString();
                            ObjMdlCreateCustomerRequest.motherMiddleName = objODBCDatareader["mother_middlename"].ToString();
                            ObjMdlCreateCustomerRequest.motherLastName = objODBCDatareader["mother_firstname"].ToString();
                            ObjMdlCreateCustomerRequest.pan = objODBCDatareader["pan_no"].ToString();
                            ObjMdlCreateCustomerRequest.uidNum = objODBCDatareader["aadhar_no"].ToString();
                            ObjMdlCreateCustomerRequest.gender = objODBCDatareader["gender_name"].ToString();
                            ObjMdlCreateCustomerRequest.nationality = "INDIAN";
                            ObjMdlCreateCustomerRequest.addressType = "Communication";

                            lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                            lsstate = objODBCDatareader["state"].ToString();
                            lsdistrict = objODBCDatareader["district"].ToString();
                            lscity = objODBCDatareader["city"].ToString();

                            ObjMdlCreateCustomerRequest.accountNumber1 = objODBCDatareader["bankaccount_number"].ToString();
                            ObjMdlCreateCustomerRequest.bankName1 = objODBCDatareader["bank_name"].ToString();
                            ObjMdlCreateCustomerRequest.branchName1 = objODBCDatareader["branch_name"].ToString();
                            ObjMdlCreateCustomerRequest.branchAddress1 = objODBCDatareader["branch_address"].ToString();
                            ObjMdlCreateCustomerRequest.ifscCode1 = objODBCDatareader["ifsc_code"].ToString();
                            ObjMdlCreateCustomerRequest.micrCode1 = objODBCDatareader["micr_code"].ToString();

                            ObjMdlCreateCustomerRequest.accountType1 = objODBCDatareader["account_type"].ToString();
                            ObjMdlCreateCustomerRequest.accountHolderName1 = objODBCDatareader["accountholder_name"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select vertical_gid,constitution_gid " +
                          " from ocs_trn_tcadapplication " +
                          " where application_gid='" + lsapplication_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                            lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                        }
                        objODBCDatareader.Close();
                    }


                    if ((String.IsNullOrEmpty(lsconstitution_gid)))
                    {
                        continue;
                    }

                    if ((String.IsNullOrEmpty(lsstate)))
                    {
                        continue;
                    }


                    if ((String.IsNullOrEmpty(lsdistrict)))
                    {
                        continue;
                    }

                    if ((String.IsNullOrEmpty(lscity)))
                    {
                        continue;
                    }


                    if ((String.IsNullOrEmpty(lsvertical_gid)))
                    {
                        continue;
                    }

                    msSQL = " select encore_constitution,constitution_name from ocs_mst_tconstitution where constitution_gid='" + lsconstitution_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ObjMdlCreateCustomerRequest.customerType = objODBCDatareader["encore_constitution"].ToString();
                        lsencore_constitution = objODBCDatareader["encore_constitution"].ToString();
                        lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                        objODBCDatareader.Close();
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        continue;
                    }

                    if ((String.IsNullOrEmpty(lsencore_constitution)))
                    {
                        continue;
                    }

                    msSQL = " select encore_statecode from ocs_mst_tstatecodemapping where postalcode_statefield='" + lsstate + "'";
                    lsencore_statecode = objdbconn.GetExecuteScalar(msSQL);
                    if (!(String.IsNullOrEmpty(lsencore_statecode)))
                    {
                        ObjMdlCreateCustomerRequest.stateCode = lsencore_statecode;
                        objODBCDatareader.Close();
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        continue;
                    }

                    msSQL = " select encore_districtcode from ocs_mst_tdistrictcodemapping where postalcode_districtfield='" + lsdistrict + "'";
                    lsencore_districtcode = objdbconn.GetExecuteScalar(msSQL);
                    if (!(String.IsNullOrEmpty(lsencore_districtcode)))
                    {
                        ObjMdlCreateCustomerRequest.districtCode = lsencore_districtcode;
                        objODBCDatareader.Close();
                    }

                    else
                    {
                        objODBCDatareader.Close();
                        continue;
                    }

                    msSQL = " select encore_citycode from ocs_mst_tcitycodemapping where postalcode_cityfield='" + lscity + "'";
                    lsencore_citycode = objdbconn.GetExecuteScalar(msSQL);
                    if (!(String.IsNullOrEmpty(lsencore_citycode)))
                    {
                        ObjMdlCreateCustomerRequest.cityCode = lsencore_citycode;
                        objODBCDatareader.Close();
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        continue;
                    }

                    msSQL = " select encore_sector,vertical_name from ocs_mst_tvertical where vertical_gid='" + lsvertical_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ObjMdlCreateCustomerRequest.sector = objODBCDatareader["encore_sector"].ToString();
                        lsencore_sector = objODBCDatareader["encore_sector"].ToString();
                        lsvertical_name = objODBCDatareader["vertical_name"].ToString();
                        objODBCDatareader.Close();
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        continue;
                    }

                    if ((String.IsNullOrEmpty(lsencore_sector)))
                    {
                        continue;
                    }


                    string lscreatecustomerreqst_json = Newtonsoft.Json.JsonConvert.SerializeObject(ObjMdlCreateCustomerRequest);

                    msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("ECRQ");
                    msSQL = " insert into ocs_trn_tencorecreatecustomerrequest(" +
                            " encorecreatecustomerrequest_gid," +                           
                            " farmercontact_gid," +
                            " request_time," +
                            " requested_by," +
                            " customerId," +
                            " branchCode," +
                            " firstName," +
                            " middleName," +
                            " lastName," +
                            " dateOfBirth," +
                            " address1," +
                            " address2," +
                            " address3," +
                            " countryCode," +
                            " stateCode," +
                            " cityCode," +
                            " districtCode," +
                            " pinCode," +
                            " phone1," +
                            " phone2," +
                            " email," +
                            " customerType," +
                            " fatherOrSpouseFirstName," +
                            " fatherOrSpouseMiddleName," +
                            " fatherOrSpouseLastName," +
                            " motherFirstName," +
                            " motherMiddleName," +
                            " motherLastName," +
                            " salutation," +
                            " gender," +
                            " nationality," +
                            " caste," +
                            " religion," +
                            " domicileStatus," +
                            " guardianFirstName," +
                            " guardianMiddleName," +
                            " guardianLastName," +
                            " guardianAddress1," +
                            " guardianAddress2," +
                            " guardianAddress3," +
                            " guardianCountryCode," +
                            " guardianStateCode," +
                            " guardianCityCode," +
                            " guardianDistrictCode," +
                            " guardianPinCode," +
                            " guardianRelationshipWithCustomer," +
                            " guardianDateOfBirth," +
                            " guardianSalutation," +
                            " guardianGender," +
                            " guardianCustomerId," +
                            " pan," +
                            " uidNum," +
                            " gstin," +
                            " registrationNo," +
                            " cin," +
                            " legalEntityIdentifier," +
                            " tag," +
                            " documentType," +
                            " proofType," +
                            " documentNumber," +
                            " dateOfIssue," +
                            " dateOfExpiry," +
                            " filePath," +
                            " occupation," +
                            " employment," +
                            " sector," +
                            " residenceType," +
                            " language," +
                            " languageProficiency," +
                            " education," +
                            " annualIncome," +
                            " segment," +
                            " addressType," +
                            " organizationName," +
                            " alt1Address1," +
                            " alt1Address2," +
                            " alt1Address3," +
                            " alt1CityCode," +
                            " alt1DistrictCode," +
                            " alt1StateCode," +
                            " alt1CountryCode," +
                            " alt1PinCode," +
                            " altAddressType1," +
                            " alt1Phone1," +
                            " alt1Phone2," +
                            " alt1Email," +
                            " alt2Address1," +
                            " alt2Address2," +
                            " alt2Address3," +
                            " alt2CityCode," +
                            " alt2DistrictCode," +
                            " alt2StateCode," +
                            " alt2CountryCode," +
                            " alt2PinCode," +
                            " altAddressType2," +
                            " alt2Phone1," +
                            " alt2Phone2," +
                            " alt2Email," +
                            " centreName," +
                            " centreId," +
                            " nomineeCustomerId," +
                            " nomineeRelationshipType," +
                            " maritalStatus," +
                            " bankName1," +
                            " branchName1," +
                            " branchAddress1," +
                            " ifscCode1," +
                            " micrCode1," +
                            " accountType1," +
                            " accountNumber1," +
                            " accountHolderName1," +
                            " bankName2," +
                            " branchName2," +
                            " branchAddress2," +
                            " ifscCode2," +
                            " micrCode2," +
                            " accountType2," +
                            " accountNumber2," +
                            " accountHolderName2," +
                            " bankName3," +
                            " branchName3," +
                            " branchAddress3," +
                            " ifscCode3," +
                            " micrCode3," +
                            " accountType3," +
                            " accountNumber3," +
                            " accountHolderName3," +
                            " referenceCustomerId1," +
                            " referenceCustomerId2," +
                            " json_string) " +
                            " values(" +
                            "'" + msGetGidCustCrtReq + "'," +                            
                            "'" + farmer + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + ObjMdlCreateCustomerRequest.customerId + "'," +
                            "'" + ObjMdlCreateCustomerRequest.branchCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.firstName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.middleName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.lastName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.dateOfBirth + "'," +
                            "'" + ObjMdlCreateCustomerRequest.address1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.address2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.address3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.countryCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.stateCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.cityCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.districtCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.pinCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.phone1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.phone2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.email + "'," +
                            "'" + ObjMdlCreateCustomerRequest.customerType + "'," +
                            "'" + ObjMdlCreateCustomerRequest.fatherOrSpouseFirstName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.fatherOrSpouseMiddleName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.fatherOrSpouseLastName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.motherFirstName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.motherMiddleName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.motherLastName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.salutation + "'," +
                            "'" + ObjMdlCreateCustomerRequest.gender + "'," +
                            "'" + ObjMdlCreateCustomerRequest.nationality + "'," +
                            "'" + ObjMdlCreateCustomerRequest.caste + "'," +
                            "'" + ObjMdlCreateCustomerRequest.religion + "'," +
                            "'" + ObjMdlCreateCustomerRequest.domicileStatus + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianFirstName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianMiddleName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianLastName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianAddress1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianAddress2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianAddress3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianCountryCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianStateCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianCityCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianDistrictCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianPinCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianRelationshipWithCustomer + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianDateOfBirth + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianSalutation + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianGender + "'," +
                            "'" + ObjMdlCreateCustomerRequest.guardianCustomerId + "'," +
                            "'" + ObjMdlCreateCustomerRequest.pan + "'," +
                            "'" + ObjMdlCreateCustomerRequest.uidNum + "'," +
                            "'" + ObjMdlCreateCustomerRequest.gstin + "'," +
                            "'" + ObjMdlCreateCustomerRequest.registrationNo + "'," +
                            "'" + ObjMdlCreateCustomerRequest.cin + "'," +
                            "'" + ObjMdlCreateCustomerRequest.legalEntityIdentifier + "'," +
                            "'" + ObjMdlCreateCustomerRequest.tag + "'," +
                            "'" + ObjMdlCreateCustomerRequest.documentType + "'," +
                            "'" + ObjMdlCreateCustomerRequest.proofType + "'," +
                            "'" + ObjMdlCreateCustomerRequest.documentNumber + "'," +
                            "'" + ObjMdlCreateCustomerRequest.dateOfIssue + "'," +
                            "'" + ObjMdlCreateCustomerRequest.dateOfExpiry + "'," +
                            "'" + ObjMdlCreateCustomerRequest.filePath + "'," +
                            "'" + ObjMdlCreateCustomerRequest.occupation + "'," +
                            "'" + ObjMdlCreateCustomerRequest.employment + "'," +
                            "'" + ObjMdlCreateCustomerRequest.sector + "'," +
                            "'" + ObjMdlCreateCustomerRequest.residenceType + "'," +
                            "'" + ObjMdlCreateCustomerRequest.language + "'," +
                            "'" + ObjMdlCreateCustomerRequest.languageProficiency + "'," +
                            "'" + ObjMdlCreateCustomerRequest.education + "'," +
                            "'" + ObjMdlCreateCustomerRequest.annualIncome + "'," +
                            "'" + ObjMdlCreateCustomerRequest.segment + "'," +
                            "'" + ObjMdlCreateCustomerRequest.addressType + "'," +
                            "'" + ObjMdlCreateCustomerRequest.organizationName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1Address1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1Address2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1Address3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1CityCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1DistrictCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1StateCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1CountryCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1PinCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.altAddressType1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1Phone1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1Phone2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt1Email + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2Address1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2Address2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2Address3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2CityCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2DistrictCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2StateCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2CountryCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2PinCode + "'," +
                            "'" + ObjMdlCreateCustomerRequest.altAddressType2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2Phone1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2Phone2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.alt2Email + "'," +
                            "'" + ObjMdlCreateCustomerRequest.centreName + "'," +
                            "'" + ObjMdlCreateCustomerRequest.centreId + "'," +
                            "'" + ObjMdlCreateCustomerRequest.nomineeCustomerId + "'," +
                            "'" + ObjMdlCreateCustomerRequest.nomineeRelationshipType + "'," +
                            "'" + ObjMdlCreateCustomerRequest.maritalStatus + "'," +
                            "'" + ObjMdlCreateCustomerRequest.bankName1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.branchName1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.branchAddress1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.ifscCode1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.micrCode1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.accountType1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.accountNumber1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.accountHolderName1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.bankName2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.branchName2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.branchAddress2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.ifscCode2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.micrCode2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.accountType2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.accountNumber2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.accountHolderName2 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.bankName3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.branchName3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.branchAddress3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.ifscCode3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.micrCode3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.accountType3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.accountNumber3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.accountHolderName3 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.referenceCustomerId1 + "'," +
                            "'" + ObjMdlCreateCustomerRequest.referenceCustomerId2 + "'," +
                            "'" + lscreatecustomerreqst_json + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 1)
                    {
                        LogForAuditEncoreIntegration("Error occurred while storing customer creation request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        continue;
                    }
                    try
                    {
                        var client = new RestClient(ConfigurationManager.AppSettings["encore_customercreationurl"].ToString());
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                        string val = System.Convert.ToBase64String(plainTextBytes);
                        request.AddHeader("Authorization", "Basic " + val);

                        var body = lscreatecustomerreqst_json;

                        request.AddParameter("application/json", body, ParameterType.RequestBody);

                        IRestResponse response = client.Execute(request);

                        lsresponsecontentcustomercreation = response.Content;
                        lsresponseStatusCodecustomercreation = response.StatusCode.ToString();

                    }
                    catch (Exception ex)
                    {
                        LogForAuditEncoreIntegration("Error occurred while hitting customer creation URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        continue;
                    }
                    if (lsresponseStatusCodecustomercreation == "OK")
                    {
                        ObjMdlCreateCustomerResponse = JsonConvert.DeserializeObject<MdlCreateCustomerResponse>(lsresponsecontentcustomercreation);

                        msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                        msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                                " encorecreatecustomerresponse_gid," +
                                " encorecreatecustomerrequest_gid," +
                                " response_time," +
                                " customerId," +
                                " branchCode," +
                                " firstName," +
                                " middleName," +
                                " lastName," +
                                " dateOfBirth," +
                                " address1," +
                                " address2," +
                                " address3," +
                                " countryCode," +
                                " stateCode," +
                                " cityCode," +
                                " districtCode," +
                                " pinCode," +
                                " phone1," +
                                " phone2," +
                                " email," +
                                " customerType," +
                                " fatherOrSpouseFirstName," +
                                " fatherOrSpouseMiddleName," +
                                " fatherOrSpouseLastName," +
                                " motherFirstName," +
                                " motherMiddleName," +
                                " motherLastName," +
                                " salutation," +
                                " gender," +
                                " nationality," +
                                " caste," +
                                " religion," +
                                " domicileStatus," +
                                " guardianFirstName," +
                                " guardianMiddleName," +
                                " guardianLastName," +
                                " guardianAddress1," +
                                " guardianAddress2," +
                                " guardianAddress3," +
                                " guardianCountryCode," +
                                " guardianStateCode," +
                                " guardianCityCode," +
                                " guardianDistrictCode," +
                                " guardianPinCode," +
                                " guardianRelationshipWithCustomer," +
                                " guardianDateOfBirth," +
                                " guardianSalutation," +
                                " guardianGender," +
                                " guardianCustomerId," +
                                " pan," +
                                " uidNum," +
                                " gstin," +
                                " registrationNo," +
                                " cin," +
                                " legalEntityIdentifier," +
                                " tag," +
                                " documentType," +
                                " proofType," +
                                " documentNumber," +
                                " dateOfIssue," +
                                " dateOfExpiry," +
                                " filePath," +
                                " occupation," +
                                " employment," +
                                " sector," +
                                " residenceType," +
                                " language," +
                                " languageProficiency," +
                                " education," +
                                " annualIncome," +
                                " segment," +
                                " addressType," +
                                " organizationName," +
                                " alt1Address1," +
                                " alt1Address2," +
                                " alt1Address3," +
                                " alt1CityCode," +
                                " alt1DistrictCode," +
                                " alt1StateCode," +
                                " alt1CountryCode," +
                                " alt1PinCode," +
                                " altAddressType1," +
                                " alt1Phone1," +
                                " alt1Phone2," +
                                " alt1Email," +
                                " alt2Address1," +
                                " alt2Address2," +
                                " alt2Address3," +
                                " alt2CityCode," +
                                " alt2DistrictCode," +
                                " alt2StateCode," +
                                " alt2CountryCode," +
                                " alt2PinCode," +
                                " altAddressType2," +
                                " alt2Phone1," +
                                " alt2Phone2," +
                                " alt2Email," +
                                " centreName," +
                                " centreId," +
                                " nomineeCustomerId," +
                                " nomineeRelationshipType," +
                                " maritalStatus," +
                                " bankName1," +
                                " branchName1," +
                                " branchAddress1," +
                                " ifscCode1," +
                                " micrCode1," +
                                " accountType1," +
                                " accountNumber1," +
                                " accountHolderName1," +
                                " bankName2," +
                                " branchName2," +
                                " branchAddress2," +
                                " ifscCode2," +
                                " micrCode2," +
                                " accountType2," +
                                " accountNumber2," +
                                " accountHolderName2," +
                                " bankName3," +
                                " branchName3," +
                                " branchAddress3," +
                                " ifscCode3," +
                                " micrCode3," +
                                " accountType3," +
                                " accountNumber3," +
                                " accountHolderName3," +
                                " referenceCustomerId1," +
                                " referenceCustomerId2," +
                                " json_string) " +
                                " values(" +
                                "'" + msGetGidCustCrtResp + "'," +
                                "'" + msGetGidCustCrtReq + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + ObjMdlCreateCustomerResponse.customerId + "'," +
                                "'" + ObjMdlCreateCustomerResponse.branchCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.firstName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.middleName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.lastName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.dateOfBirth + "'," +
                                "'" + ObjMdlCreateCustomerResponse.address1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.address2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.address3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.countryCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.stateCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.cityCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.districtCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.pinCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.phone1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.phone2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.email + "'," +
                                "'" + ObjMdlCreateCustomerResponse.customerType + "'," +
                                "'" + ObjMdlCreateCustomerResponse.fatherOrSpouseFirstName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.fatherOrSpouseMiddleName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.fatherOrSpouseLastName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.motherFirstName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.motherMiddleName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.motherLastName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.salutation + "'," +
                                "'" + ObjMdlCreateCustomerResponse.gender + "'," +
                                "'" + ObjMdlCreateCustomerResponse.nationality + "'," +
                                "'" + ObjMdlCreateCustomerResponse.caste + "'," +
                                "'" + ObjMdlCreateCustomerResponse.religion + "'," +
                                "'" + ObjMdlCreateCustomerResponse.domicileStatus + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianFirstName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianMiddleName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianLastName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianAddress1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianAddress2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianAddress3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianCountryCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianStateCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianCityCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianDistrictCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianPinCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianRelationshipWithCustomer + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianDateOfBirth + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianSalutation + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianGender + "'," +
                                "'" + ObjMdlCreateCustomerResponse.guardianCustomerId + "'," +
                                "'" + ObjMdlCreateCustomerResponse.pan + "'," +
                                "'" + ObjMdlCreateCustomerResponse.uidNum + "'," +
                                "'" + ObjMdlCreateCustomerResponse.gstin + "'," +
                                "'" + ObjMdlCreateCustomerResponse.registrationNo + "'," +
                                "'" + ObjMdlCreateCustomerResponse.cin + "'," +
                                "'" + ObjMdlCreateCustomerResponse.legalEntityIdentifier + "'," +
                                "'" + ObjMdlCreateCustomerResponse.tag + "'," +
                                "'" + ObjMdlCreateCustomerResponse.documentType + "'," +
                                "'" + ObjMdlCreateCustomerResponse.proofType + "'," +
                                "'" + ObjMdlCreateCustomerResponse.documentNumber + "'," +
                                "'" + ObjMdlCreateCustomerResponse.dateOfIssue + "'," +
                                "'" + ObjMdlCreateCustomerResponse.dateOfExpiry + "'," +
                                "'" + ObjMdlCreateCustomerResponse.filePath + "'," +
                                "'" + ObjMdlCreateCustomerResponse.occupation + "'," +
                                "'" + ObjMdlCreateCustomerResponse.employment + "'," +
                                "'" + ObjMdlCreateCustomerResponse.sector + "'," +
                                "'" + ObjMdlCreateCustomerResponse.residenceType + "'," +
                                "'" + ObjMdlCreateCustomerResponse.language + "'," +
                                "'" + ObjMdlCreateCustomerResponse.languageProficiency + "'," +
                                "'" + ObjMdlCreateCustomerResponse.education + "'," +
                                "'" + ObjMdlCreateCustomerResponse.annualIncome + "'," +
                                "'" + ObjMdlCreateCustomerResponse.segment + "'," +
                                "'" + ObjMdlCreateCustomerResponse.addressType + "'," +
                                "'" + ObjMdlCreateCustomerResponse.organizationName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1Address1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1Address2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1Address3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1CityCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1DistrictCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1StateCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1CountryCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1PinCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.altAddressType1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1Phone1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1Phone2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt1Email + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2Address1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2Address2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2Address3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2CityCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2DistrictCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2StateCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2CountryCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2PinCode + "'," +
                                "'" + ObjMdlCreateCustomerResponse.altAddressType2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2Phone1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2Phone2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.alt2Email + "'," +
                                "'" + ObjMdlCreateCustomerResponse.centreName + "'," +
                                "'" + ObjMdlCreateCustomerResponse.centreId + "'," +
                                "'" + ObjMdlCreateCustomerResponse.nomineeCustomerId + "'," +
                                "'" + ObjMdlCreateCustomerResponse.nomineeRelationshipType + "'," +
                                "'" + ObjMdlCreateCustomerResponse.maritalStatus + "'," +
                                "'" + ObjMdlCreateCustomerResponse.bankName1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.branchName1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.branchAddress1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.ifscCode1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.micrCode1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.accountType1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.accountNumber1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.accountHolderName1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.bankName2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.branchName2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.branchAddress2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.ifscCode2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.micrCode2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.accountType2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.accountNumber2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.accountHolderName2 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.bankName3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.branchName3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.branchAddress3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.ifscCode3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.micrCode3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.accountType3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.accountNumber3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.accountHolderName3 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.referenceCustomerId1 + "'," +
                                "'" + ObjMdlCreateCustomerResponse.referenceCustomerId2 + "'," +
                                "'" + lsresponsecontentcustomercreation + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlCreateCustomerResponse.customerId))))
                        {
                            if (!(String.IsNullOrEmpty(farmer)))
                            {                                
                                msSQL = "update ocs_trn_tfarmercontact set urn='" + ObjMdlCreateCustomerResponse.customerId + "',urn_status='Yes' where farmercontact_gid='" + farmer + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                LogForAuditEncoreIntegration("Batch - Customer Creation Request Ended for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                farmer_count = farmer_count + 1;
                            }
                        }
                        else
                        {
                            LogForAuditEncoreIntegration("Error occurred while storing customer creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }

                    }
                    else if (lsresponseStatusCodecustomercreation == "InternalServerError")
                    {

                        LogForAuditEncoreIntegration("Internal Server Error . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        continue;

                    }
                    else
                    {
      
                        LogForAuditEncoreIntegration("Failed to receive 200 or 500 Response  . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        values.status = false;
                        values.message = "" + lsresponseStatusCodecustomercreation + " Response from Encore. Totally " +
                                         "" + farmer_count.ToString() + " Of " + total_farmer.ToString() + " Successful Customer Creation Response received from Encore";
                        return;
                    }
                }


                msSQL = "update ocs_trn_tfarmercontact set batchencorecreatecust_status='Y' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = farmer_count.ToString() + " Of " + total_farmer.ToString() + " Successful Customer Creation Response received from Encore ";

           
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = " Error occurred while posting batch customer creation request to Encore ";
                LogForAuditEncoreIntegration("Error occurred while posting batch customer creation request to Encore .Farmer ID - " + lsfarmer + ". Exception - " + ex + " .Response Content(If Exists) - " + lsresponsecontentcustomercreation + " at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);                
            }

        }

        //Batch Find Customer for Farmer(B2B2C)
        public void DaBatchFindCustomerforFarmer(MdlCreateCustomer values, string employee_gid)
        {
            string type = "Batch_FindCustomer";
            MdlFindCustomerResponse ObjMdlFindCustomerResponse = new MdlFindCustomerResponse();
            try
            {
                List<string> farmer_list = new List<string>();

                msSQL = " select farmercontact_gid " +
                       " from ocs_trn_tfarmercontact " +
                       " where encorefindcust_status='N' and rmdisbursementrequest_gid='" + values.rmdisbursementrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                farmer_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("farmercontact_gid")).ToList();
                farmer_count = 0;
                total_farmer = farmer_list.Count();
                if (total_farmer == 0)
                {
                    msSQL = "update ocs_trn_tfarmercontact set batchencorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = false;
                    values.message = "Finding Customer Process Completed for Farmer list in current Disbursement";
                    return;
                }
                foreach (var farmer in farmer_list)
                {
                    MdlCreateCustomerRequest ObjMdlCreateCustomerRequest = new MdlCreateCustomerRequest();

                    lsfarmer = farmer;

                    if (!(String.IsNullOrEmpty(farmer)))
                    {
                        LogForAuditEncoreIntegration("Batch - Find Customer Request Initiated for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                        msSQL = " select application_gid,application_no,pan_no " +
                                " from ocs_trn_tfarmercontact " +
                                " where farmercontact_gid='" + farmer + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lspan = objODBCDatareader["pan_no"].ToString();
                        }
                        objODBCDatareader.Close();

                        if ((String.IsNullOrEmpty(lspan)))
                        {

                            if (!(String.IsNullOrEmpty(farmer)))
                            {
                                //LogForAuditEncoreIntegration("Find Customer Request Initiated for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);


                                msSQL = " select application_gid,application_no,aadhar_no " +
                                        " from ocs_trn_tfarmercontact " +
                                        " where farmercontact_gid='" + farmer + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaadhar_no = objODBCDatareader["aadhar_no"].ToString();
                                }
                                objODBCDatareader.Close();

                                if ((String.IsNullOrEmpty(lsaadhar_no)))
                                {
                                    msSQL = "update ocs_trn_tfarmercontact set encorefindcust_status='Y' where farmercontact_gid='" + farmer + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    LogForAuditEncoreIntegration("Batch - Find Customer Request Ended for Individual Farmer (PAN and Aadhar Number is Empty) . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                    continue;
                                }

                            }

                            msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("EFCR");
                            msSQL = " insert into ocs_trn_tencorefindcustomerrequest(" +
                                    " encorefindcustomerrequest_gid," +
                                    " application_gid," +
                                    " farmercontact_gid," +
                                    " request_time," +
                                    " requested_by," +
                                    " aadhar_no) " +
                                    " values(" +
                                    "'" + msGetGidCustCrtReq + "'," +
                                    "'" + values.application_gid + "'," +
                                    "'" + farmer + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsaadhar_no + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 1)
                            {
                                LogForAuditEncoreIntegration("Error occurred while storing find customer request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                continue;
                            }
                            try
                            {

                                var param = "uid=" + lsaadhar_no + "&offSet=0";

                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                                string requestAddressURL = ConfigurationManager.AppSettings["encore_findcustomerurl"].ToString() + "?" + param;
                                var clientAddress = new RestClient(requestAddressURL);
                                var requestAddress = new RestRequest(Method.GET);

                                requestAddress.AddHeader("Content-Type", "application/json");
                                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                                string val = System.Convert.ToBase64String(plainTextBytes);
                                requestAddress.AddHeader("Authorization", "Basic " + val);

                                IRestResponse responseAddress = clientAddress.Execute(requestAddress);

                                lsresponsecontentfindcustomer = responseAddress.Content;
                                lsresponseStatusCodefindcustomer = responseAddress.StatusCode.ToString();

                            }
                            catch (Exception ex)
                            {
                                LogForAuditEncoreIntegration("Error occurred while hitting Find customer URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                continue;
                            }
                            if (lsresponseStatusCodefindcustomer == "OK")
                            {

                                ObjMdlFindCustomerResponse = JsonConvert.DeserializeObject<MdlFindCustomerResponse>(lsresponsecontentfindcustomer);

                                if (ObjMdlFindCustomerResponse.totalResults == "0")
                                {
                                    LogForAuditEncoreIntegration("Batch - Find Customer Request Ended for Individual Farmer. Reason - No Customer Found in Encore. Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                    if (!(String.IsNullOrEmpty(farmer)))
                                    {
                                        msSQL = "update ocs_trn_tfarmercontact set encorefindcust_status='Y' where farmercontact_gid='" + farmer + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }

                                    continue;
                                }

                                msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                                msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                                        " encorecreatecustomerresponse_gid," +
                                        " encorecreatecustomerrequest_gid," +
                                        " response_time," +
                                        " customerId," +
                                        " branchCode," +
                                        " firstName," +
                                        " middleName," +
                                        " lastName," +
                                        " dateOfBirth," +
                                        " address1," +
                                        " address2," +
                                        " address3," +
                                        " countryCode," +
                                        " stateCode," +
                                        " cityCode," +
                                        " districtCode," +
                                        " pinCode," +
                                        " phone1," +
                                        " phone2," +
                                        " email," +
                                        " customerType," +
                                        " fatherOrSpouseFirstName," +
                                        " fatherOrSpouseMiddleName," +
                                        " fatherOrSpouseLastName," +
                                        " motherFirstName," +
                                        " motherMiddleName," +
                                        " motherLastName," +
                                        " salutation," +
                                        " gender," +
                                        " nationality," +
                                        " caste," +
                                        " religion," +
                                        " domicileStatus," +
                                        " guardianFirstName," +
                                        " guardianMiddleName," +
                                        " guardianLastName," +
                                        " guardianAddress1," +
                                        " guardianAddress2," +
                                        " guardianAddress3," +
                                        " guardianCountryCode," +
                                        " guardianStateCode," +
                                        " guardianCityCode," +
                                        " guardianDistrictCode," +
                                        " guardianPinCode," +
                                        " guardianRelationshipWithCustomer," +
                                        " guardianDateOfBirth," +
                                        " guardianSalutation," +
                                        " guardianGender," +
                                        " guardianCustomerId," +
                                        " pan," +
                                        " uidNum," +
                                        " gstin," +
                                        " registrationNo," +
                                        " cin," +
                                        " legalEntityIdentifier," +
                                        " tag," +
                                        " documentType," +
                                        " proofType," +
                                        " documentNumber," +
                                        " dateOfIssue," +
                                        " dateOfExpiry," +
                                        " filePath," +
                                        " occupation," +
                                        " employment," +
                                        " sector," +
                                        " residenceType," +
                                        " language," +
                                        " languageProficiency," +
                                        " education," +
                                        " annualIncome," +
                                        " segment," +
                                        " addressType," +
                                        " organizationName," +
                                        " alt1Address1," +
                                        " alt1Address2," +
                                        " alt1Address3," +
                                        " alt1CityCode," +
                                        " alt1DistrictCode," +
                                        " alt1StateCode," +
                                        " alt1CountryCode," +
                                        " alt1PinCode," +
                                        " altAddressType1," +
                                        " alt1Phone1," +
                                        " alt1Phone2," +
                                        " alt1Email," +
                                        " alt2Address1," +
                                        " alt2Address2," +
                                        " alt2Address3," +
                                        " alt2CityCode," +
                                        " alt2DistrictCode," +
                                        " alt2StateCode," +
                                        " alt2CountryCode," +
                                        " alt2PinCode," +
                                        " altAddressType2," +
                                        " alt2Phone1," +
                                        " alt2Phone2," +
                                        " alt2Email," +
                                        " centreName," +
                                        " centreId," +
                                        " nomineeCustomerId," +
                                        " nomineeRelationshipType," +
                                        " maritalStatus," +
                                        " bankName1," +
                                        " branchName1," +
                                        " branchAddress1," +
                                        " ifscCode1," +
                                        " micrCode1," +
                                        " accountType1," +
                                        " accountNumber1," +
                                        " accountHolderName1," +
                                        " bankName2," +
                                        " branchName2," +
                                        " branchAddress2," +
                                        " ifscCode2," +
                                        " micrCode2," +
                                        " accountType2," +
                                        " accountNumber2," +
                                        " accountHolderName2," +
                                        " bankName3," +
                                        " branchName3," +
                                        " branchAddress3," +
                                        " ifscCode3," +
                                        " micrCode3," +
                                        " accountType3," +
                                        " accountNumber3," +
                                        " accountHolderName3," +
                                        " referenceCustomerId1," +
                                        " referenceCustomerId2," +
                                        " json_string) " +
                                        " values(" +
                                        "'" + msGetGidCustCrtResp + "'," +
                                        "'" + msGetGidCustCrtReq + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].firstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].middleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].lastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].countryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].stateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].districtCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].salutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nationality + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].caste + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].religion + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].domicileStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianStateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianPinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianRelationshipWithCustomer + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianSalutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianGender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pan + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].uidNum + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gstin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].registrationNo + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].legalEntityIdentifier + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].tag + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].proofType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentNumber + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfIssue + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfExpiry + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].filePath + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].occupation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].employment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].sector + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].residenceType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].language + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].languageProficiency + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].education + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].annualIncome + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].segment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].addressType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].organizationName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeRelationshipType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].maritalStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId2 + "'," +
                                        "'" + lsresponsecontentfindcustomer + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlFindCustomerResponse.customers[0].customerId))))
                                {

                                    if (!(String.IsNullOrEmpty(farmer)))
                                    {
                                        msSQL = "update ocs_trn_tfarmercontact set urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',urn_status='Yes',encorefindcust_status='Y' where farmercontact_gid='" + farmer + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        farmer_count = farmer_count + 1;

                                        LogForAuditEncoreIntegration("Batch - Find Customer Request Ended for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
 
                                        continue;
                                    }

                                }
                                else
                                {
                                    LogForAuditEncoreIntegration("Error occurred while storing customer creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
    
                                    continue;
                                }

                            }
                            else
                            {
                                MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                                objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentcustomercreation);

                                LogForAuditEncoreIntegration("Failed to receive 200 Response. Farmer ID - " + farmer + " . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                continue;
                            }
                            

                            continue;
                        }
                    }

                    msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("EFCR");
                    msSQL = " insert into ocs_trn_tencorefindcustomerrequest(" +
                            " encorefindcustomerrequest_gid," +
                            " farmercontact_gid," +
                            " request_time," +
                            " requested_by," +
                            " pan_no) " +
                            " values(" +
                            "'" + msGetGidCustCrtReq + "'," +
                            "'" + farmer + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + lspan + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 1)
                    {
                        LogForAuditEncoreIntegration("Error occurred while storing find customer request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                        continue;
                    }
                    try
                    {

                        var param = "pan=" + lspan + "&offSet=0";

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                        string requestAddressURL = ConfigurationManager.AppSettings["encore_findcustomerurl"].ToString() + "?" + param;
                        var clientAddress = new RestClient(requestAddressURL);
                        var requestAddress = new RestRequest(Method.GET);

                        requestAddress.AddHeader("Content-Type", "application/json");
                        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                        string val = System.Convert.ToBase64String(plainTextBytes);
                        requestAddress.AddHeader("Authorization", "Basic " + val);

                        IRestResponse responseAddress = clientAddress.Execute(requestAddress);

                        lsresponsecontentfindcustomer = responseAddress.Content;
                        lsresponseStatusCodefindcustomer = responseAddress.StatusCode.ToString();

                    }
                    catch (Exception ex)
                    {
                        LogForAuditEncoreIntegration("Error occurred while hitting Find customer URL of Encore .  Farmer ID - " + farmer + " . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        continue;
                    }
                    if (lsresponseStatusCodefindcustomer == "OK")
                    {

                        ObjMdlFindCustomerResponse = JsonConvert.DeserializeObject<MdlFindCustomerResponse>(lsresponsecontentfindcustomer);

                        if (ObjMdlFindCustomerResponse.totalResults == "0")
                        {
                            LogForAuditEncoreIntegration("Batch - Find Customer Request Reinitiated for Individual Farmer. Reason - Aadhar based validation. Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);


                            if (!(String.IsNullOrEmpty(farmer)))
                            {
                                //LogForAuditEncoreIntegration("Find Customer Request Initiated for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);


                                msSQL = " select application_gid,application_no,aadhar_no " +
                                        " from ocs_trn_tfarmercontact " +
                                        " where farmercontact_gid='" + farmer + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaadhar_no = objODBCDatareader["aadhar_no"].ToString();
                                }
                                objODBCDatareader.Close();

                                if ((String.IsNullOrEmpty(lsaadhar_no)))
                                {
                                    msSQL = "update ocs_trn_tfarmercontact set encorefindcust_status='Y' where farmercontact_gid='" + farmer + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    LogForAuditEncoreIntegration("Batch - Find Customer Request Ended for Individual Farmer (PAN and Aadhar Number is Empty) . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                    continue;
                                }

                            }

                            msGetGidCustCrtReq = objcmnfunctions.GetMasterGID("EFCR");
                            msSQL = " insert into ocs_trn_tencorefindcustomerrequest(" +
                                    " encorefindcustomerrequest_gid," +
                                    " application_gid," +
                                    " farmercontact_gid," +
                                    " request_time," +
                                    " requested_by," +
                                    " aadhar_no) " +
                                    " values(" +
                                    "'" + msGetGidCustCrtReq + "'," +
                                    "'" + values.application_gid + "'," +
                                    "'" + farmer + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsaadhar_no + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 1)
                            {
                                LogForAuditEncoreIntegration("Error occurred while storing find customer request . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                continue;
                            }
                            try
                            {

                                var param = "uid=" + lsaadhar_no + "&offSet=0";

                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                                string requestAddressURL = ConfigurationManager.AppSettings["encore_findcustomerurl"].ToString() + "?" + param;
                                var clientAddress = new RestClient(requestAddressURL);
                                var requestAddress = new RestRequest(Method.GET);

                                requestAddress.AddHeader("Content-Type", "application/json");
                                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["encore_basicauthusername"].ToString() + ":" + ConfigurationManager.AppSettings["encore_basicauthuserpassword"].ToString());
                                string val = System.Convert.ToBase64String(plainTextBytes);
                                requestAddress.AddHeader("Authorization", "Basic " + val);

                                IRestResponse responseAddress = clientAddress.Execute(requestAddress);

                                lsresponsecontentfindcustomer = responseAddress.Content;
                                lsresponseStatusCodefindcustomer = responseAddress.StatusCode.ToString();

                            }
                            catch (Exception ex)
                            {
                                LogForAuditEncoreIntegration("Error occurred while hitting Find customer URL of Encore . Exception - " + ex + " . Response Content is - " + lsresponsecontentcustomercreation + " at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                continue;
                            }
                            if (lsresponseStatusCodefindcustomer == "OK")
                            {

                                ObjMdlFindCustomerResponse = JsonConvert.DeserializeObject<MdlFindCustomerResponse>(lsresponsecontentfindcustomer);

                                if (ObjMdlFindCustomerResponse.totalResults == "0")
                                {
                                    LogForAuditEncoreIntegration("Batch - Find Customer Request Ended for Individual Farmer. Reason - No Customer Found in Encore. Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                    if (!(String.IsNullOrEmpty(farmer)))
                                    {
                                        msSQL = "update ocs_trn_tfarmercontact set encorefindcust_status='Y' where farmercontact_gid='" + farmer + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }


                                    continue;
                                }

                                msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                                msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                                        " encorecreatecustomerresponse_gid," +
                                        " encorecreatecustomerrequest_gid," +
                                        " response_time," +
                                        " customerId," +
                                        " branchCode," +
                                        " firstName," +
                                        " middleName," +
                                        " lastName," +
                                        " dateOfBirth," +
                                        " address1," +
                                        " address2," +
                                        " address3," +
                                        " countryCode," +
                                        " stateCode," +
                                        " cityCode," +
                                        " districtCode," +
                                        " pinCode," +
                                        " phone1," +
                                        " phone2," +
                                        " email," +
                                        " customerType," +
                                        " fatherOrSpouseFirstName," +
                                        " fatherOrSpouseMiddleName," +
                                        " fatherOrSpouseLastName," +
                                        " motherFirstName," +
                                        " motherMiddleName," +
                                        " motherLastName," +
                                        " salutation," +
                                        " gender," +
                                        " nationality," +
                                        " caste," +
                                        " religion," +
                                        " domicileStatus," +
                                        " guardianFirstName," +
                                        " guardianMiddleName," +
                                        " guardianLastName," +
                                        " guardianAddress1," +
                                        " guardianAddress2," +
                                        " guardianAddress3," +
                                        " guardianCountryCode," +
                                        " guardianStateCode," +
                                        " guardianCityCode," +
                                        " guardianDistrictCode," +
                                        " guardianPinCode," +
                                        " guardianRelationshipWithCustomer," +
                                        " guardianDateOfBirth," +
                                        " guardianSalutation," +
                                        " guardianGender," +
                                        " guardianCustomerId," +
                                        " pan," +
                                        " uidNum," +
                                        " gstin," +
                                        " registrationNo," +
                                        " cin," +
                                        " legalEntityIdentifier," +
                                        " tag," +
                                        " documentType," +
                                        " proofType," +
                                        " documentNumber," +
                                        " dateOfIssue," +
                                        " dateOfExpiry," +
                                        " filePath," +
                                        " occupation," +
                                        " employment," +
                                        " sector," +
                                        " residenceType," +
                                        " language," +
                                        " languageProficiency," +
                                        " education," +
                                        " annualIncome," +
                                        " segment," +
                                        " addressType," +
                                        " organizationName," +
                                        " alt1Address1," +
                                        " alt1Address2," +
                                        " alt1Address3," +
                                        " alt1CityCode," +
                                        " alt1DistrictCode," +
                                        " alt1StateCode," +
                                        " alt1CountryCode," +
                                        " alt1PinCode," +
                                        " altAddressType1," +
                                        " alt1Phone1," +
                                        " alt1Phone2," +
                                        " alt1Email," +
                                        " alt2Address1," +
                                        " alt2Address2," +
                                        " alt2Address3," +
                                        " alt2CityCode," +
                                        " alt2DistrictCode," +
                                        " alt2StateCode," +
                                        " alt2CountryCode," +
                                        " alt2PinCode," +
                                        " altAddressType2," +
                                        " alt2Phone1," +
                                        " alt2Phone2," +
                                        " alt2Email," +
                                        " centreName," +
                                        " centreId," +
                                        " nomineeCustomerId," +
                                        " nomineeRelationshipType," +
                                        " maritalStatus," +
                                        " bankName1," +
                                        " branchName1," +
                                        " branchAddress1," +
                                        " ifscCode1," +
                                        " micrCode1," +
                                        " accountType1," +
                                        " accountNumber1," +
                                        " accountHolderName1," +
                                        " bankName2," +
                                        " branchName2," +
                                        " branchAddress2," +
                                        " ifscCode2," +
                                        " micrCode2," +
                                        " accountType2," +
                                        " accountNumber2," +
                                        " accountHolderName2," +
                                        " bankName3," +
                                        " branchName3," +
                                        " branchAddress3," +
                                        " ifscCode3," +
                                        " micrCode3," +
                                        " accountType3," +
                                        " accountNumber3," +
                                        " accountHolderName3," +
                                        " referenceCustomerId1," +
                                        " referenceCustomerId2," +
                                        " json_string) " +
                                        " values(" +
                                        "'" + msGetGidCustCrtResp + "'," +
                                        "'" + msGetGidCustCrtReq + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].firstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].middleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].lastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].countryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].stateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].districtCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].customerType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].motherLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].salutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nationality + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].caste + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].religion + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].domicileStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianFirstName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianMiddleName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianLastName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianStateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianPinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianRelationshipWithCustomer + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianDateOfBirth + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianSalutation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianGender + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].guardianCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].pan + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].uidNum + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].gstin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].registrationNo + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].cin + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].legalEntityIdentifier + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].tag + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].proofType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].documentNumber + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfIssue + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].dateOfExpiry + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].filePath + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].occupation + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].employment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].sector + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].residenceType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].language + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].languageProficiency + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].education + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].annualIncome + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].segment + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].addressType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].organizationName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt1Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CityCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2DistrictCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2StateCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2CountryCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2PinCode + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].alt2Email + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreName + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].centreId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeCustomerId + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].nomineeRelationshipType + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].maritalStatus + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName2 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].bankName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].micrCode3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountType3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName3 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId1 + "'," +
                                        "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId2 + "'," +
                                        "'" + lsresponsecontentfindcustomer + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlFindCustomerResponse.customers[0].customerId))))
                                {

                                    if (!(String.IsNullOrEmpty(farmer)))
                                    {
                                        msSQL = "update ocs_trn_tfarmercontact set urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',urn_status='Yes',encorefindcust_status='Y' where farmercontact_gid='" + farmer + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        farmer_count = farmer_count + 1;

                                        LogForAuditEncoreIntegration("Batch - Find Customer Request Ended for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                        continue;
                                    }

                                }
                                else
                                {
                                    LogForAuditEncoreIntegration("Error occurred while storing customer creation response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                    continue;
                                }

                            }
                            else
                            {
                                MdlEncoreErrorMessageResponse objMdlEncoreErrorMessageResponse = new MdlEncoreErrorMessageResponse();

                                objMdlEncoreErrorMessageResponse = JsonConvert.DeserializeObject<MdlEncoreErrorMessageResponse>(lsresponsecontentcustomercreation);

                                LogForAuditEncoreIntegration("Failed to receive 200 Response . Response Content - " + lsresponsecontentcustomercreation + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                continue;
                            }
                            

                            continue;

                          
                        }

                        msGetGidCustCrtResp = objcmnfunctions.GetMasterGID("ECCR");
                        msSQL = " insert into ocs_trn_tencorecreatecustomerresponse(" +
                                " encorecreatecustomerresponse_gid," +
                                " encorecreatecustomerrequest_gid," +
                                " response_time," +
                                " customerId," +
                                " branchCode," +
                                " firstName," +
                                " middleName," +
                                " lastName," +
                                " dateOfBirth," +
                                " address1," +
                                " address2," +
                                " address3," +
                                " countryCode," +
                                " stateCode," +
                                " cityCode," +
                                " districtCode," +
                                " pinCode," +
                                " phone1," +
                                " phone2," +
                                " email," +
                                " customerType," +
                                " fatherOrSpouseFirstName," +
                                " fatherOrSpouseMiddleName," +
                                " fatherOrSpouseLastName," +
                                " motherFirstName," +
                                " motherMiddleName," +
                                " motherLastName," +
                                " salutation," +
                                " gender," +
                                " nationality," +
                                " caste," +
                                " religion," +
                                " domicileStatus," +
                                " guardianFirstName," +
                                " guardianMiddleName," +
                                " guardianLastName," +
                                " guardianAddress1," +
                                " guardianAddress2," +
                                " guardianAddress3," +
                                " guardianCountryCode," +
                                " guardianStateCode," +
                                " guardianCityCode," +
                                " guardianDistrictCode," +
                                " guardianPinCode," +
                                " guardianRelationshipWithCustomer," +
                                " guardianDateOfBirth," +
                                " guardianSalutation," +
                                " guardianGender," +
                                " guardianCustomerId," +
                                " pan," +
                                " uidNum," +
                                " gstin," +
                                " registrationNo," +
                                " cin," +
                                " legalEntityIdentifier," +
                                " tag," +
                                " documentType," +
                                " proofType," +
                                " documentNumber," +
                                " dateOfIssue," +
                                " dateOfExpiry," +
                                " filePath," +
                                " occupation," +
                                " employment," +
                                " sector," +
                                " residenceType," +
                                " language," +
                                " languageProficiency," +
                                " education," +
                                " annualIncome," +
                                " segment," +
                                " addressType," +
                                " organizationName," +
                                " alt1Address1," +
                                " alt1Address2," +
                                " alt1Address3," +
                                " alt1CityCode," +
                                " alt1DistrictCode," +
                                " alt1StateCode," +
                                " alt1CountryCode," +
                                " alt1PinCode," +
                                " altAddressType1," +
                                " alt1Phone1," +
                                " alt1Phone2," +
                                " alt1Email," +
                                " alt2Address1," +
                                " alt2Address2," +
                                " alt2Address3," +
                                " alt2CityCode," +
                                " alt2DistrictCode," +
                                " alt2StateCode," +
                                " alt2CountryCode," +
                                " alt2PinCode," +
                                " altAddressType2," +
                                " alt2Phone1," +
                                " alt2Phone2," +
                                " alt2Email," +
                                " centreName," +
                                " centreId," +
                                " nomineeCustomerId," +
                                " nomineeRelationshipType," +
                                " maritalStatus," +
                                " bankName1," +
                                " branchName1," +
                                " branchAddress1," +
                                " ifscCode1," +
                                " micrCode1," +
                                " accountType1," +
                                " accountNumber1," +
                                " accountHolderName1," +
                                " bankName2," +
                                " branchName2," +
                                " branchAddress2," +
                                " ifscCode2," +
                                " micrCode2," +
                                " accountType2," +
                                " accountNumber2," +
                                " accountHolderName2," +
                                " bankName3," +
                                " branchName3," +
                                " branchAddress3," +
                                " ifscCode3," +
                                " micrCode3," +
                                " accountType3," +
                                " accountNumber3," +
                                " accountHolderName3," +
                                " referenceCustomerId1," +
                                " referenceCustomerId2," +
                                " json_string) " +
                                " values(" +
                                "'" + msGetGidCustCrtResp + "'," +
                                "'" + msGetGidCustCrtReq + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].customerId + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].branchCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].firstName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].middleName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].lastName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].dateOfBirth + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].address1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].address2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].address3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].countryCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].stateCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].cityCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].districtCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].pinCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].phone1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].phone2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].email + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].customerType + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseFirstName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseMiddleName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].fatherOrSpouseLastName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].motherFirstName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].motherMiddleName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].motherLastName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].salutation + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].gender + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].nationality + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].caste + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].religion + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].domicileStatus + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianFirstName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianMiddleName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianLastName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianAddress3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianCountryCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianStateCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianCityCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianDistrictCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianPinCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianRelationshipWithCustomer + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianDateOfBirth + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianSalutation + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianGender + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].guardianCustomerId + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].pan + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].uidNum + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].gstin + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].registrationNo + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].cin + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].legalEntityIdentifier + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].tag + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].documentType + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].proofType + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].documentNumber + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].dateOfIssue + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].dateOfExpiry + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].filePath + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].occupation + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].employment + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].sector + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].residenceType + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].language + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].languageProficiency + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].education + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].annualIncome + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].segment + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].addressType + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].organizationName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1Address3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1CityCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1DistrictCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1StateCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1CountryCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1PinCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1Phone2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt1Email + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2Address3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2CityCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2DistrictCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2StateCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2CountryCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2PinCode + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].altAddressType2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2Phone2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].alt2Email + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].centreName + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].centreId + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].nomineeCustomerId + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].nomineeRelationshipType + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].maritalStatus + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].bankName1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].branchName1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].micrCode1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].accountType1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].bankName2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].branchName2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].micrCode2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].accountType2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName2 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].bankName3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].branchName3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].branchAddress3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].ifscCode3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].micrCode3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].accountType3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].accountNumber3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].accountHolderName3 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId1 + "'," +
                                "'" + ObjMdlFindCustomerResponse.customers[0].referenceCustomerId2 + "'," +
                                "'" + lsresponsecontentfindcustomer + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                        if (mnResult != 0 && (!(String.IsNullOrEmpty(ObjMdlFindCustomerResponse.customers[0].customerId))))
                        {
                            if (!(String.IsNullOrEmpty(farmer)))
                            {
                                msSQL = "update ocs_trn_tfarmercontact set urn='" + ObjMdlFindCustomerResponse.customers[0].customerId + "',urn_status='Yes',encorefindcust_status='Y' where farmercontact_gid='" + farmer + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                LogForAuditEncoreIntegration("Batch - Find Customer Request Ended for Individual Farmer . Farmer ID - " + farmer + " . at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);

                                farmer_count = farmer_count + 1;
                            }
                        }
                        else
                        {
                            LogForAuditEncoreIntegration("Error occurred while storing find customer response . msSQL - " + msSQL + " . at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                            continue;
                        }

                    }
                    else if (lsresponseStatusCodefindcustomer == "InternalServerError")
                    {
                        LogForAuditEncoreIntegration("Internal Server Error . Response Content - " + lsresponsecontentfindcustomer + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        continue;
                    }
                    else
                    {

                        LogForAuditEncoreIntegration("Failed to receive 200 or 500 Response  . Response Content - " + lsresponsecontentfindcustomer + " . Response Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
                        values.status = false;
                        values.message = "" + lsresponseStatusCodefindcustomer + " Response from Encore. Totally " +
                                         "" + farmer_count.ToString() + " Of " + total_farmer.ToString() + " Successful Find Customer Response received from Encore";
                        return;
                    }
                }

                msSQL = "update ocs_trn_tfarmercontact set batchencorefindcust_status='Y' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = farmer_count.ToString() + " Of " + total_farmer.ToString() + " Successful Find Customer Response received from Encore ";


            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = " Error occurred while posting batch find customer request to Encore ";
                LogForAuditEncoreIntegration("Error occurred while posting batch find customer request to Encore .Farmer ID - " + lsfarmer + ". Exception - " + ex + " .  Response Content(If Exists) - " + lsresponsecontentfindcustomer + " at  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), type);
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