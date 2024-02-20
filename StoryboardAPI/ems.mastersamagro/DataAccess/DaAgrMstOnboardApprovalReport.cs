using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Configuration;
using System.IO;
using ems.storage.Functions;
using ems.mastersamagro.Models;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to export buyer and supplier approved record details.
    /// </summary>
    /// <remarks>Written by Kalaiarasan</remarks>

    public class DaAgrMstOnboardApprovalReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_child, dt_datatable1, dt_datatable2, dt_datatable3;
        OdbcDataReader objODBCDatareader;
        OdbcDataReader objODBCDataReader, objODBCDataReader1;
        string msSQL, msGetGid;
        int mnResult;
        string lsmaster_value;


        //Buyer Onboard Report Export Excel --- STARTING
        public void DaGetExportBuyerOnboardApproved(MdlAgrMstOnboardApprovalReport objAgrMstOnboardApprovalReport)
        {
            msSQL = " select  application_no as 'Buyer Id', virtualaccount_number as 'VAN', " +
                    " customerref_name as 'Customer/SupplierName',constitution_name as 'Constitution', " +
                    " sa_status as 'Application From SA',sa_name as 'SAMAssociate ID/Name', " +
                    " vertical_name as 'Vertical',vernacular_language as 'Vernacular Language', buyersuppliertype_name as 'Buyer Type',  " +
                    " (select group_concat(product_name SEPARATOR ' || ')  from agr_mst_tbyronboard2product where application_gid = a.application_gid) as 'Product', " +
                    " (select group_concat(sector_name SEPARATOR ' || ')  from agr_mst_tbyronboard2product where application_gid = a.application_gid) as 'Sector/Strategic BusinessUnit', " +
                    " (select group_concat(category_name SEPARATOR ' || ')  from agr_mst_tbyronboard2product where application_gid = a.application_gid) as 'Category', " +
                    " (select group_concat(variety_name SEPARATOR ' || ')  from agr_mst_tbyronboard2product where application_gid = a.application_gid) as 'Commodity', " +
                    " (select group_concat(botanical_name SEPARATOR ' || ')  from agr_mst_tbyronboard2product where application_gid = a.application_gid) as 'Botanical Name', " +
                    " (select group_concat(alternative_name SEPARATOR ' || ')  from agr_mst_tbyronboard2product where application_gid = a.application_gid) as 'Alternative Names', " +
                    " (select group_concat(hsn_code SEPARATOR ' || ')  from agr_mst_tbyronboard2product where application_gid = a.application_gid) as 'HSN Code', " +
                    " contactpersonfirst_name as 'First Name',contactpersonmiddle_name as 'Middle Name', contactpersonlast_name as 'Last Name', " +
                    " designation_type as 'Designation',landline_no as 'Landline Number', " +
                    " (select group_concat(mobile_no SEPARATOR ' || ')  from agr_mst_tbyronboard2contactno where application_gid = a.application_gid) as 'Mobile Number', " +
                    " (select group_concat(primary_mobileno SEPARATOR ' || ')  from agr_mst_tbyronboard2contactno where application_gid = a.application_gid) as 'Mobile Primary Status(Yes/No)', " +
                    " (select group_concat(whatsapp_mobileno SEPARATOR ' || ')  from agr_mst_tbyronboard2contactno where application_gid = a.application_gid) as 'Whatsapp Number(Yes/No)', " +
                    " (select group_concat(email_address SEPARATOR ' || ')  from agr_mst_tbyronboard2email where application_gid = a.application_gid) as 'Email Address', " +
                    " (select group_concat(primary_emailaddress SEPARATOR ' || ')  from agr_mst_tbyronboard2email where application_gid = a.application_gid) as 'Email Primary Status(Yes/No)', " +
                    " (select group_concat(geneticcode_name SEPARATOR ' || ')  from agr_mst_tbyronboard2geneticcode where application_gid = a.application_gid) as 'Genetic Code', " +
                    " (select group_concat(genetic_status SEPARATOR ' || ')  from agr_mst_tbyronboard2geneticcode where application_gid = a.application_gid) as 'Genetic Status(Yes/No)', " +
                    " (select group_concat(genetic_remarks SEPARATOR ' || ')  from agr_mst_tbyronboard2geneticcode where application_gid = a.application_gid) as 'Observation(s)', lgltag_status as 'Tag Status', " +
                    " a.onboard_approvedbyname as 'Approved By', date_format(a.onboard_approveddate,'%d-%m-%Y %h:%i %p') as 'Approved Date', concat(i.user_firstname, ' ', i.user_lastname, ' / ', i.user_code) as RM_name " +
                    " from agr_mst_tbyronboard a " +
                    " left join hrm_mst_temployee h on a.created_by = h.employee_gid " +
                    " left join adm_mst_tuser i on i.user_gid = h.user_gid " +
                    " where onboard_approvalstatus= 'Y'"+
                    " group by a.application_gid ";

            dt_datatable1 = objdbconn.GetDataTable(msSQL);

            msSQL = " select l.application_no as 'Buyer ID', urn as 'ERP Id', company_name as 'Legal/TradeName'," +
                    " companypan_no as 'PAN Value*', " +
                    " (select group_concat(gst_registered SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2branch where institution_gid = a.institution_gid) as 'GST Registered(Yes/No)'," +
                    " (select group_concat(gst_no SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2branch where institution_gid = a.institution_gid) as 'GST Number'," +
                    " (select group_concat(gst_state SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2branch where institution_gid = a.institution_gid) as 'GST State'," +
                    " date_format(date_incorporation, '%d-%m-%Y') as 'Certificate of Incorporation*'," +
                    " date_format(businessstart_date, '%d-%m-%Y') as 'Business StartDate*' ," +
                    " cin_no as 'Corporate Identification Number(CIN)', tan_number as 'TAN', i.companytype_name as 'Company Type*'," +
                     " official_telephoneno as 'Official ContactNumber*', officialemail_address as 'Official MailID*', stakeholder_type as 'Stakeholder Type*'," +
                    " year_business as 'Years in Business', month_business as 'Months in Business'," +
                    " (select group_concat(amlcategory_name SEPARATOR ' || ')  from ocs_mst_tamlcategory where amlcategory_gid = a.amlcategory_gid) as 'Category-AML(AntiMoneyLaundering)'," +
                     " (select group_concat(businesscategory_name SEPARATOR ' || ')  from ocs_mst_tbusinesscategory where businesscategory_gid = a.businesscategory_gid) as 'Category-Business'," +
                     " (select group_concat(creditrating_agencyname SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2ratingdetail where institution_gid = a.institution_gid) as 'Credit Rating Agency'," +
                    " (select group_concat(creditrating_name SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2ratingdetail where institution_gid = a.institution_gid) as 'Credit Rating'," +
                    " (select group_concat(date_format(assessed_on, '%d-%m-%Y') SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2ratingdetail where institution_gid = a.institution_gid) as 'Assessed On'," +
                    " (select group_concat(creditrating_link SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2ratingdetail where institution_gid = a.institution_gid) as 'Credit Rating Link'," +
                    " contactperson_firstname as 'First Name*', contactperson_middlename as 'Middle Name'," +
                    " contactperson_lastname as 'Last Name',designation as 'Designation*',  " +
                    " (select group_concat(mobile_no SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2mobileno where institution_gid = a.institution_gid) as 'Mobile Number'," +
                    " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2mobileno where institution_gid = a.institution_gid) as 'Mobile Primary Status(Yes/No)'," +
                    " (select group_concat(whatsapp_no SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2mobileno where institution_gid = a.institution_gid) as 'Whatsapp Number(Yes/No)'," +
                    " (select group_concat(email_address SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2email where institution_gid = a.institution_gid) as 'Email Address'," +
                    " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2email where institution_gid = a.institution_gid) as 'Email Primary Status(Yes/No)'," +
                    " (select group_concat(addresstype_name SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'Address Type'," +
                    " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'Address Primary Status(Yes/No)'," +
                    " (select group_concat(addressline1 SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'Address Line1'," +
                    " (select group_concat(addressline2 SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'Address Line2'," +
                    " (select group_concat(landmark SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'Land Mark'," +
                    " (select group_concat(postal_code SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'Postal Code'," +
                    " (select group_concat(city SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'City'," +
                    " (select group_concat(taluka SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'Taluka'," +
                    " (select group_concat(district SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'District'," +
                    " (select group_concat(state SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'State'," +
                    " (select group_concat(country SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'Country'," +
                    " (select group_concat(latitude SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'Latitude'," +
                    " (select group_concat(longitude SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2address where institution_gid = a.institution_gid) as 'Longitude'," +
                    " (select group_concat(ifsc_code SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'IFSC Code'," +
                    " (select group_concat(bank_name SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Bank Name'," +
                   " (select group_concat(branch_name SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Branch Name'," +
                    " (select group_concat(bank_address SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Branch Address'," +
                    " (select group_concat(micr_code SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'MICR Code'," +
                    " (select group_concat(bankaccount_number SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'BankAccount Number'," +
                    " (select group_concat(confirmbankaccountnumber SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'ConfirmAccount Number'," +
                    " (select group_concat(bankaccount_name SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'AccountHolder Name'," +
                    " (select group_concat(bankaccounttype_name SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Account Type'," +
                    " (select group_concat(joinaccount_status SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Joint Account(Yes/No)'," +
                    " (select group_concat(joinaccount_name SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Joint AccountHolder Name'," +
                    " (select group_concat(chequebook_status SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'IsChequeBook Facility Available'," +
                    " (select group_concat(accountopen_date SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Account Open Date'," +
                    " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Account Primary Status(Yes/No)'," +
                    " date_format(start_date, '%d-%m-%Y') as 'Start Date*', date_format(end_date, '%d-%m-%Y') as 'End Date*'," +
                    " escrow as 'Escrow*', lastyear_turnover as 'LastYear TurnOver*', a.incometax_returnsstatus as 'IncomeTax Returns status*'," +
                    " a.revenue as 'Revenue(in₹)', a.profit as 'Profit(in₹)', a.fixed_assets as 'Fixed Assets(in₹)', a.sundrydebt_adv as 'Sundry Debtors & Advances(in₹)'," +
                    " (select group_concat(document_title SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2documentupload where institution_gid = a.institution_gid) as 'Document Title'," +
                    " (select group_concat(document_id SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2documentupload where institution_gid = a.institution_gid) as 'Document ID'," +
                    " (select group_concat(document_name SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2documentupload where institution_gid = a.institution_gid) as 'Upload Documents'," +
                    " (select group_concat(licensetype_name SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2licensedtl where institution_gid = a.institution_gid) as 'License Type'," +
                    " (select group_concat(license_no SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2licensedtl where institution_gid = a.institution_gid) as 'License Number'," +
                    " (select group_concat(date_format(issue_date, '%d-%m-%Y') SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2licensedtl where institution_gid = a.institution_gid) as 'Issue Date'," +
                    " (select group_concat(date_format(expiry_date, '%d-%m-%Y') SEPARATOR ' || ')  from agr_mst_tbyronboardinstitution2licensedtl where institution_gid = a.institution_gid) as 'Expiry Date'" +
                     " from agr_mst_tbyronboard2institution a " +
                    " left join agr_mst_tbyronboardinstitution2branch b on a.institution_gid = b.institution_gid " +
                    " left join ocs_mst_tcompanytype i on a.companytype_gid = i.companytype_gid " +
                    " left join ocs_mst_tamlcategory j on a.amlcategory_gid = j.amlcategory_gid " +
                    " left join agr_mst_tbyronboard l on a.application_gid = l.application_gid " +
                    " where onboard_approvalstatus = 'Y' " +
                    " group by a.institution_gid ";

            dt_datatable2 = objdbconn.GetDataTable(msSQL);
                        
            msSQL = " select b.application_no as 'Buyer ID', a.urn as 'ERP Id', a.  institution_name as 'Company Name', a. stakeholder_type as 'Stakeholder Type',  a.  aadhar_no as  'Aadhar Number', " +
                   " a.pan_status as 'PAN Status(Yes/No)', a.pan_no as 'PAN Value(IfPANStatusisYes,PANValueismandatory)', " +
                   " a.first_name as 'First Name' , a.middle_name as 'Middle Name', a.last_name as 'Last Name', " +
                   " a.individual_dob as 'Date of Birth', a.age as 'Age' , a.gender_name as 'Gender', a.designation_name as 'Designation', " +
                   " a.pep_status as 'Politically Exposedperson(PEP)', a.pepverified_date as 'PEP VerifiedOn(DD-MM-YYYY)'," +
                   " a.maritalstatus_name as 'Marital Status',  a.father_firstname as 'Fathers FirstName', a.father_middlename as 'Fathers MiddleName', a.father_lastname as 'Fathers LastName', a.fathernominee_status as 'Father Nominee Status(Yes/No)', "+
                   " a.father_dob as 'Fathers DateofBirth(DD-MM-YYYY)', a.father_age as 'Father Age',a.mother_firstname as 'Mothers FirstName', a.mother_middlename as 'Mothers MiddleName', a.mother_lastname as 'Mothers LastName', a.mothernominee_status as 'Mother Nominee Status(Yes/No)', " +
                   " a.mother_dob as 'Mothers DateofBirth(DD-MM-YYYY)', a.mother_age as 'Mother Age', a.spouse_firstname as 'Spouse FirstName', a.spouse_middlename as 'Spouse MiddleName', a.spousenominee_status as 'Spouse Nominee Status(Yes/No)'," +
                   " a.spouse_lastname as 'Spouse LastName', a.spouse_dob as 'Spouses DateofBirth(DD-MM-YYYY)', a.spouse_age,"+
                   " a.othernominee_status as 'Other Nominee(Yes/No)', a.relationshiptype as 'Relationship Type', a.nominee_dob as 'Nominee DOB', a.nominee_age as 'Nominee Age', " +
                   " a.nomineefirst_name as 'Nominee FirstName', a.nominee_middlename as 'Nominee MiddleName', a.nominee_lastname as 'Nominee LastName'," +
                   " a.educationalqualification_name as 'Educational Qualification',  a.main_occupation as 'Main Occupation', a.annual_income as 'Annual Income', " +
                   " a.monthly_income as 'Monthly Income', a.incometype_name as 'Income Type', " +
                   " (select group_concat(mobile_no SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2mobileno where contact_gid = a.contact_gid) as 'Mobile Number', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2mobileno where contact_gid = a.contact_gid) as 'Mobile Primary Status(Yes/No)', " +
                   " (select group_concat(whatsapp_no SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2mobileno where contact_gid = a.contact_gid) as 'WhatsApp Number(Yes/No)', " +
                   " (select group_concat(email_address SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2email where contact_gid = a.contact_gid) as 'Email Address', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2email where contact_gid = a.contact_gid) as 'Email Primary Status (Yes/No)', " +
                   " (select group_concat(ifsc_code SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'IFSC Code', " +
                   " (select group_concat(bank_name SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Bank Name', " +
                   " (select group_concat(branch_name SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Branch Name', " +
                   " (select group_concat(bank_address SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Branch Address', " +
                   " (select group_concat(micr_code SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'MICR Code', " +
                   " (select group_concat(bankaccount_number SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'BankAccount Number', " +
                   " (select group_concat(confirmbankaccountnumber SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Confirm AccountNumber', " +
                   " (select group_concat(bankaccount_name SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'AccountHolder Name', " +
                   " (select group_concat(bankaccounttype_name SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Account Type', " +
                   " (select group_concat(joinaccount_status SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'JointAccount(Yes/No)', " +
                   " (select group_concat(joinaccount_name SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'JointAccount HolderName', " +
                   " (select group_concat(chequebook_status SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'IsChequeBookFacilityAvailable(Yes/No)', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Primary Status(Yes/No)', " +
                   " (select group_concat(accountopen_date SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Account OpenDate', " +
                   " (select group_concat(idproof_name SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2idproof where contact_gid = a.contact_gid) as 'Identification Type', " +
                   " (select group_concat(idproof_no SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2idproof where contact_gid = a.contact_gid) as 'Identification value'," +
                   " (select group_concat(idproof_dob SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2idproof where contact_gid = a.contact_gid) as 'IDProof DOB', " +
                   " (select group_concat(file_no SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2idproof where contact_gid = a.contact_gid) as 'FILE NUMBER', " +
                   " (select group_concat(document_name SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2idproof where contact_gid = a.contact_gid) as 'IDProof Name', " +
                   " (select group_concat(addresstype_name SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'Address Type', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'Address Primary Status', " +
                   " (select group_concat(addressline1 SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'Address Line1', " +
                   " (select group_concat(addressline2 SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'Address Line2', " +
                   " (select group_concat(landmark SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'LandMark', " +
                   " (select group_concat(postal_code SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'Postal Code', " +
                   " (select group_concat(city SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'City', " +
                   " (select group_concat(taluka SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'Taluka', " +
                   " (select group_concat(district SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'District', " +
                   " (select group_concat(state SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'State', " +
                   " (select group_concat(country SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'Country', " +
                   " (select group_concat(latitude SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'Latitude', " +
                   " (select group_concat(longitude SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2address where contact_gid = a.contact_gid) as 'Longitude', " +
                   " a.totallandinacres as 'TotalLand inAcres', a.cultivatedland as 'Cultivated Land', a.previouscrop as 'Previous Crop', a.prposedcrop as 'Proposed Crop', " +
                  " a.ownershiptype_name as 'Ownership Type', a.propertyholder_name as 'Property inthe Nameof', a.residencetype_name as 'Residence Type',  a.currentresidence_years as 'Years in CurrentResidence', " +
                   " a.branch_distance as 'DistancefromBranch/RegionalOffice(inKms)', " +
                   " (select group_concat(document_title SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2document where contact_gid = a.contact_gid) as 'Individual DocTitle', " +
                   " (select group_concat(document_name SEPARATOR ' || ')  from agr_mst_tbyronboardcontact2document where contact_gid = a.contact_gid) as 'Individual DocName' " +
                   " from agr_mst_tbyronboardcontact a " +
                   " left join agr_mst_tbyronboard b on a.application_gid = b.application_gid" +
                   " where onboard_approvalstatus= 'Y' " +
                   " group by a.contact_gid ";

            dt_datatable3 = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet1 = excel.Workbook.Worksheets.Add("General");
            var workSheet2 = excel.Workbook.Worksheets.Add("Company");
            var workSheet3 = excel.Workbook.Worksheets.Add("Individual");

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objAgrMstOnboardApprovalReport.lsname = "BuyerOnboard Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/BuyerOnboardReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objAgrMstOnboardApprovalReport.lscloudpath = lscompany_code + "/" + "Master/BuyerOnboardReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objAgrMstOnboardApprovalReport.lsname;
                objAgrMstOnboardApprovalReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/BuyerOnboardReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objAgrMstOnboardApprovalReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet1.Cells[1, 1].LoadFromDataTable(dt_datatable1, true);
                workSheet2.Cells[1, 1].LoadFromDataTable(dt_datatable2, true);
                workSheet3.Cells[1, 1].LoadFromDataTable(dt_datatable3, true);
                FileInfo file = new FileInfo(objAgrMstOnboardApprovalReport.lspath);
                using (var range1 = workSheet1.Cells[1, 1, 1, 33])  //Address "A1:BD1"
                using (var range2 = workSheet2.Cells[1, 1, 1, 75])  //Address "A2:BD1"
                using (var range3 = workSheet3.Cells[1, 1, 1, 95])  //Address "A1:BD1"

                {
                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);

                    range2.Style.Font.Bold = true;
                    range2.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range2.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range2.Style.Font.Color.SetColor(Color.White);

                    range3.Style.Font.Bold = true;
                    range3.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range3.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range3.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objAgrMstOnboardApprovalReport.lscloudpath, ms);
                ms.Close();
                objAgrMstOnboardApprovalReport.lscloudpath = objcmnstorage.EncryptData(objAgrMstOnboardApprovalReport.lscloudpath);
                objAgrMstOnboardApprovalReport.lspath = objcmnstorage.EncryptData(objAgrMstOnboardApprovalReport.lspath);
                objAgrMstOnboardApprovalReport.status = true;
                objAgrMstOnboardApprovalReport.message = "Success";
            }
            
            catch (Exception ex)
            {
                objAgrMstOnboardApprovalReport.status = false;
                objAgrMstOnboardApprovalReport.message = "Failure";
            }
        }
        //Buyer Onboard Report Export Excel --- ENDING


        //Supplier Onboard Report Export Excel --- STARTING
        public void DaGetExportSupplierOnboardApproved(MdlAgrMstOnboardApprovalReport objAgrMstOnboardApprovalReport)
        {
            msSQL = " select  application_no as 'Supplier Id', " +
                    " customerref_name as 'Customer/SupplierName',constitution_name as 'Constitution', " +
                    " sa_status as 'Application From SA',sa_name as 'SAMAssociate ID/Name', " +
                    " vertical_name as 'Vertical',vernacular_language as 'Vernacular Language', buyersuppliertype_name as 'Supplier Type', " +
                    " (select group_concat(product_name SEPARATOR ' || ')  from agr_mst_tsupronboard2product where application_gid = a.application_gid) as 'Product', " +
                    " (select group_concat(sector_name SEPARATOR ' || ')  from agr_mst_tsupronboard2product where application_gid = a.application_gid) as 'Sector/Strategic BusinessUnit', " +
                    " (select group_concat(category_name SEPARATOR ' || ')  from agr_mst_tsupronboard2product where application_gid = a.application_gid) as 'Category', " +
                    " (select group_concat(variety_name SEPARATOR ' || ')  from agr_mst_tsupronboard2product where application_gid = a.application_gid) as 'Commodity', " +
                    " (select group_concat(botanical_name SEPARATOR ' || ')  from agr_mst_tsupronboard2product where application_gid = a.application_gid) as 'Botanical Name', " +
                    " (select group_concat(alternative_name SEPARATOR ' || ')  from agr_mst_tsupronboard2product where application_gid = a.application_gid) as 'Alternative Names', " +
                    " (select group_concat(hsn_code SEPARATOR ' || ')  from agr_mst_tsupronboard2product where application_gid = a.application_gid) as 'HSN Code', " +
                    " contactpersonfirst_name as 'First Name',contactpersonmiddle_name as 'Middle Name', contactpersonlast_name as 'Last Name', " +
                    " designation_type as 'Designation',landline_no as 'Landline Number', " +
                    " (select group_concat(mobile_no SEPARATOR ' || ')  from agr_mst_tsupronboard2contactno where application_gid = a.application_gid) as 'Mobile Number', " +
                    " (select group_concat(primary_mobileno SEPARATOR ' || ')  from agr_mst_tsupronboard2contactno where application_gid = a.application_gid) as 'Mobile Primary Status(Yes/No)', " +
                    " (select group_concat(whatsapp_mobileno SEPARATOR ' || ')  from agr_mst_tsupronboard2contactno where application_gid = a.application_gid) as 'Whatsapp Number(Yes/No)', " +
                    " (select group_concat(email_address SEPARATOR ' || ')  from agr_mst_tsupronboard2email where application_gid = a.application_gid) as 'Email Address', " +
                    " (select group_concat(primary_emailaddress SEPARATOR ' || ')  from agr_mst_tsupronboard2email where application_gid = a.application_gid) as 'Email Primary Status(Yes/No)', " +
                    " (select group_concat(geneticcode_name SEPARATOR ' || ')  from agr_mst_tsupronboard2geneticcode where application_gid = a.application_gid) as 'Genetic Code', " +
                    " (select group_concat(genetic_status SEPARATOR ' || ')  from agr_mst_tsupronboard2geneticcode where application_gid = a.application_gid) as 'Genetic Status(Yes/No)', " +
                    " (select group_concat(genetic_remarks SEPARATOR ' || ')  from agr_mst_tsupronboard2geneticcode where application_gid = a.application_gid) as 'Observation(s)' , lgltag_status as 'Tag Status', " +
                    " a.onboard_approvedbyname as 'Approved By', date_format(a.onboard_approveddate,'%d-%m-%Y %h:%i %p') as 'Approved Date', concat(i.user_firstname, ' ', i.user_lastname, ' / ', i.user_code) as RM_name " +
                    " from agr_mst_tsupronboard a " +
                    " left join hrm_mst_temployee h on a.created_by = h.employee_gid " +
                    " left join adm_mst_tuser i on i.user_gid = h.user_gid " +
                    " where onboard_approvalstatus= 'Y'" +
                    " group by a.application_gid ";

            dt_datatable1 = objdbconn.GetDataTable(msSQL);

            msSQL = " select l.application_no as 'Supplier Id', urn as 'ERP Id', company_name as 'Legal/TradeName'," +
                    " companypan_no as 'PAN Value*', " +
                    " (select group_concat(gst_registered SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2branch where institution_gid = a.institution_gid) as 'GST Registered(Yes/No)'," +
                    " (select group_concat(gst_no SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2branch where institution_gid = a.institution_gid) as 'GST Number'," +
                    " (select group_concat(gst_state SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2branch where institution_gid = a.institution_gid) as 'GST State'," +
                    " date_format(date_incorporation, '%d-%m-%Y') as 'Certificate of Incorporation*'," +
                    " date_format(businessstart_date, '%d-%m-%Y') as 'Business StartDate*' ," +
                    " cin_no as 'Corporate Identification Number(CIN)', tan_number as 'TAN', i.companytype_name as 'Company Type*'," +
                     " official_telephoneno as 'Official ContactNumber*', officialemail_address as 'Official MailID*', stakeholder_type as 'Stakeholder Type*'," +
                    " year_business as 'Years in Business', month_business as 'Months in Business'," +
                    " (select group_concat(amlcategory_name SEPARATOR ' || ')  from ocs_mst_tamlcategory where amlcategory_gid = a.amlcategory_gid) as 'Category-AML(AntiMoneyLaundering)'," +
                     " (select group_concat(businesscategory_name SEPARATOR ' || ')  from ocs_mst_tbusinesscategory where businesscategory_gid = a.businesscategory_gid) as 'Category-Business'," +
                     " (select group_concat(creditrating_agencyname SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2ratingdetail where institution_gid = a.institution_gid) as 'Credit Rating Agency'," +
                    " (select group_concat(creditrating_name SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2ratingdetail where institution_gid = a.institution_gid) as 'Credit Rating'," +
                    " (select group_concat(date_format(assessed_on, '%d-%m-%Y') SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2ratingdetail where institution_gid = a.institution_gid) as 'Assessed On'," +
                    " (select group_concat(creditrating_link SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2ratingdetail where institution_gid = a.institution_gid) as 'Credit Rating Link'," +
                    " contactperson_firstname as 'First Name*', contactperson_middlename as 'Middle Name'," +
                    " contactperson_lastname as 'Last Name',designation as 'Designation*',  " +
                    " (select group_concat(mobile_no SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2mobileno where institution_gid = a.institution_gid) as 'Mobile Number'," +
                    " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2mobileno where institution_gid = a.institution_gid) as 'Mobile Primary Status(Yes/No)'," +
                    " (select group_concat(whatsapp_no SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2mobileno where institution_gid = a.institution_gid) as 'Whatsapp Number(Yes/No)'," +
                    " (select group_concat(email_address SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2email where institution_gid = a.institution_gid) as 'Email Address'," +
                    " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2email where institution_gid = a.institution_gid) as 'Email Primary Status(Yes/No)'," +
                    " (select group_concat(addresstype_name SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'Address Type'," +
                    " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'Address Primary Status(Yes/No)'," +
                    " (select group_concat(addressline1 SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'Address Line1'," +
                    " (select group_concat(addressline2 SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'Address Line2'," +
                    " (select group_concat(landmark SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'Land Mark'," +
                    " (select group_concat(postal_code SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'Postal Code'," +
                    " (select group_concat(city SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'City'," +
                    " (select group_concat(taluka SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'Taluka'," +
                    " (select group_concat(district SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'District'," +
                    " (select group_concat(state SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'State'," +
                    " (select group_concat(country SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'Country'," +
                    " (select group_concat(latitude SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'Latitude'," +
                    " (select group_concat(longitude SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid) as 'Longitude'," +
                    " (select group_concat(ifsc_code SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'IFSC Code'," +
                    " (select group_concat(bank_name SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Bank Name'," +
                   " (select group_concat(branch_name SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Branch Name'," +
                    " (select group_concat(bank_address SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Branch Address'," +
                    " (select group_concat(micr_code SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'MICR Code'," +
                    " (select group_concat(bankaccount_number SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'BankAccount Number'," +
                    " (select group_concat(confirmbankaccountnumber SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'ConfirmAccount Number'," +
                    " (select group_concat(bankaccount_name SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'AccountHolder Name'," +
                    " (select group_concat(bankaccounttype_name SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Account Type'," +
                    " (select group_concat(joinaccount_status SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Joint Account(Yes/No)'," +
                    " (select group_concat(joinaccount_name SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Joint AccountHolder Name'," +
                    " (select group_concat(chequebook_status SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'IsChequeBook Facility Available'," +
                    " (select group_concat(accountopen_date SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Account Open Date'," +
                    " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2bankdtl where institution_gid = a.institution_gid) as 'Account Primary Status(Yes/No)'," +
                    " date_format(start_date, '%d-%m-%Y') as 'Start Date*', date_format(end_date, '%d-%m-%Y') as 'End Date'," +
                    " escrow as 'Escrow*', lastyear_turnover as 'LastYear TurnOver*', a.incometax_returnsstatus as 'IncomeTax Returns status'," +
                    " a.revenue as 'Revenue(in₹)', a.profit as 'Profit(in₹)', a.fixed_assets as 'Fixed Assets(in₹)', a.sundrydebt_adv as 'Sundry Debtors & Advances(in₹)'," +
                    " (select group_concat(document_title SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2documentupload where institution_gid = a.institution_gid) as 'Document Title'," +
                    " (select group_concat(document_id SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2documentupload where institution_gid = a.institution_gid) as 'Document ID'," +
                    " (select group_concat(document_name SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2documentupload where institution_gid = a.institution_gid) as 'Upload Documents'," +
                    " (select group_concat(licensetype_name SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2licensedtl where institution_gid = a.institution_gid) as 'License Type'," +
                    " (select group_concat(license_no SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2licensedtl where institution_gid = a.institution_gid) as 'License Number'," +
                    " (select group_concat(date_format(issue_date, '%d-%m-%Y') SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2licensedtl where institution_gid = a.institution_gid) as 'Issue Date'," +
                    " (select group_concat(date_format(expiry_date, '%d-%m-%Y') SEPARATOR ' || ')  from agr_mst_tsupronboardinstitution2licensedtl where institution_gid = a.institution_gid) as 'Expiry Date'" +
                     " from agr_mst_tsupronboard2institution a " +
                    " left join agr_mst_tsupronboardinstitution2branch b on a.institution_gid = b.institution_gid " +
                    " left join ocs_mst_tcompanytype i on a.companytype_gid = i.companytype_gid " +
                    " left join ocs_mst_tamlcategory j on a.amlcategory_gid = j.amlcategory_gid " +
                    " left join agr_mst_tsupronboard l on a.application_gid = l.application_gid " +
                    " where onboard_approvalstatus = 'Y' " +
                    " group by a.institution_gid ";

            dt_datatable2 = objdbconn.GetDataTable(msSQL);
                        
            msSQL = " select b.application_no as 'Buyer ID', a.urn as 'ERP Id', a.  institution_name as 'Company Name', a. stakeholder_type as 'Stakeholder Type',  a.  aadhar_no as  'Aadhar Number', " +
                   " a.pan_status as 'PAN Status(Yes/No)', a.pan_no as 'PAN Value(IfPANStatusisYes,PANValueismandatory)', " +
                   " a.first_name as 'First Name' , a.middle_name as 'Middle Name', a.last_name as 'Last Name', " +
                   " a.individual_dob as 'Date of Birth', a.age as 'Age' , a.gender_name as 'Gender', a.designation_name as 'Designation', " +
                   " a.pep_status as 'Politically Exposedperson(PEP)', a.pepverified_date as 'PEP VerifiedOn(DD-MM-YYYY)'," +
                   " a.maritalstatus_name as 'Marital Status',  a.father_firstname as 'Fathers FirstName', a.father_middlename as 'Fathers MiddleName', a.father_lastname as 'Fathers LastName', a.fathernominee_status as 'Father Nominee Status(Yes/No)', " +
                   " a.father_dob as 'Fathers DateofBirth(DD-MM-YYYY)', a.father_age as 'Father Age',a.mother_firstname as 'Mothers FirstName', a.mother_middlename as 'Mothers MiddleName', a.mother_lastname as 'Mothers LastName', a.mothernominee_status as 'Mother Nominee Status(Yes/No)', " +
                   " a.mother_dob as 'Mothers DateofBirth(DD-MM-YYYY)', a.mother_age as 'Mother Age', a.spouse_firstname as 'Spouse FirstName', a.spouse_middlename as 'Spouse MiddleName', a.spousenominee_status as 'Spouse Nominee Status(Yes/No)'," +
                   " a.spouse_lastname as 'Spouse LastName', a.spouse_dob as 'Spouses DateofBirth(DD-MM-YYYY)', a.spouse_age," +
                   " a.othernominee_status as 'Other Nominee(Yes/No)', a.relationshiptype as 'Relationship Type', a.nominee_dob as 'Nominee DOB', a.nominee_age as 'Nominee Age', " +
                   " a.nomineefirst_name as 'Nominee FirstName', a.nominee_middlename as 'Nominee MiddleName', a.nominee_lastname as 'Nominee LastName'," +
                   " a.educationalqualification_name as 'Educational Qualification',  a.main_occupation as 'Main Occupation', a.annual_income as 'Annual Income', " +
                   " a.monthly_income as 'Monthly Income', a.incometype_name as 'Income Type', " +
                   " (select group_concat(mobile_no SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2mobileno where contact_gid = a.contact_gid) as 'Mobile Number', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2mobileno where contact_gid = a.contact_gid) as 'Mobile Primary Status(Yes/No)', " +
                   " (select group_concat(whatsapp_no SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2mobileno where contact_gid = a.contact_gid) as 'WhatsApp Number(Yes/No)', " +
                   " (select group_concat(email_address SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2email where contact_gid = a.contact_gid) as 'Email Address', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2email where contact_gid = a.contact_gid) as 'Email Primary Status (Yes/No)', " +
                   " (select group_concat(ifsc_code SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'IFSC Code', " +
                   " (select group_concat(bank_name SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Bank Name', " +
                   " (select group_concat(branch_name SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Branch Name', " +
                   " (select group_concat(bank_address SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Branch Address', " +
                   " (select group_concat(micr_code SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'MICR Code', " +
                   " (select group_concat(bankaccount_number SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'BankAccount Number', " +
                   " (select group_concat(confirmbankaccountnumber SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Confirm AccountNumber', " +
                   " (select group_concat(bankaccount_name SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'AccountHolder Name', " +
                   " (select group_concat(bankaccounttype_name SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Account Type', " +
                   " (select group_concat(joinaccount_status SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'JointAccount(Yes/No)', " +
                   " (select group_concat(joinaccount_name SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'JointAccount HolderName', " +
                   " (select group_concat(chequebook_status SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'IsChequeBookFacilityAvailable(Yes/No)', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Account Primary Status(Yes/No)', " +
                   " (select group_concat(accountopen_date SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2bankdtl where contact_gid = a.contact_gid) as 'Account OpenDate', " +
                   " (select group_concat(idproof_name SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2idproof where contact_gid = a.contact_gid) as 'Identification Type', " +
                   " (select group_concat(idproof_no SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2idproof where contact_gid = a.contact_gid) as 'Identification value'," +
                   " (select group_concat(idproof_dob SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2idproof where contact_gid = a.contact_gid) as 'IDProof DOB', " +
                   " (select group_concat(file_no SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2idproof where contact_gid = a.contact_gid) as 'FILE NUMBER', " +
                   " (select group_concat(document_name SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2idproof where contact_gid = a.contact_gid) as 'IDProof Name', " +
                   " (select group_concat(addresstype_name SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'Address Type', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'Address Primary Status', " +
                   " (select group_concat(addressline1 SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'Address Line1', " +
                   " (select group_concat(addressline2 SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'Address Line2', " +
                   " (select group_concat(landmark SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'LandMark', " +
                   " (select group_concat(postal_code SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'Postal Code', " +
                   " (select group_concat(city SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'City', " +
                   " (select group_concat(taluka SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'Taluka', " +
                   " (select group_concat(district SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'District', " +
                   " (select group_concat(state SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'State', " +
                   " (select group_concat(country SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'Country', " +
                   " (select group_concat(latitude SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'Latitude', " +
                   " (select group_concat(longitude SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid) as 'Longitude', " +
                   " a.totallandinacres as 'TotalLand inAcres', a.cultivatedland as 'Cultivated Land', a.previouscrop as 'Previous Crop', a.prposedcrop as 'Proposed Crop', " +
                  " a.ownershiptype_name as 'Ownership Type', a.propertyholder_name as 'Property inthe Nameof', a.residencetype_name as 'Residence Type',  a.currentresidence_years as 'Years in CurrentResidence', " +
                   " a.branch_distance as 'DistancefromBranch/RegionalOffice(inKms)', " +
                   " (select group_concat(document_title SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2document where contact_gid = a.contact_gid) as 'Individual DocTitle', " +
                   " (select group_concat(document_name SEPARATOR ' || ')  from agr_mst_tsupronboardcontact2document where contact_gid = a.contact_gid) as 'Individual DocName' " +
                   " from agr_mst_tsupronboardcontact a " +
                   " left join agr_mst_tsupronboard b on a.application_gid = b.application_gid" +
                   " where onboard_approvalstatus= 'Y' " +
                   " group by a.contact_gid ";

            dt_datatable3 = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet1 = excel.Workbook.Worksheets.Add("General");
            var workSheet2 = excel.Workbook.Worksheets.Add("Company");
            var workSheet3 = excel.Workbook.Worksheets.Add("Individual");

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objAgrMstOnboardApprovalReport.lsname = "SupplierOnboard Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SupplierOnboardReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objAgrMstOnboardApprovalReport.lscloudpath = lscompany_code + "/" + "Master/SupplierOnboardReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objAgrMstOnboardApprovalReport.lsname;
                objAgrMstOnboardApprovalReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SupplierOnboardReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objAgrMstOnboardApprovalReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet1.Cells[1, 1].LoadFromDataTable(dt_datatable1, true);
                workSheet2.Cells[1, 1].LoadFromDataTable(dt_datatable2, true);
                workSheet3.Cells[1, 1].LoadFromDataTable(dt_datatable3, true);
                FileInfo file = new FileInfo(objAgrMstOnboardApprovalReport.lspath);
                using (var range1 = workSheet1.Cells[1, 1, 1, 32])  //Address "A1:BD1"
                using (var range2 = workSheet2.Cells[1, 1, 1, 75])  //Address "A2:BD1"
                using (var range3 = workSheet3.Cells[1, 1, 1, 95])  //Address "A1:BD1"

                {
                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);

                    range2.Style.Font.Bold = true;
                    range2.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range2.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range2.Style.Font.Color.SetColor(Color.White);

                    range3.Style.Font.Bold = true;
                    range3.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range3.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range3.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objAgrMstOnboardApprovalReport.lscloudpath, ms);
                ms.Close();
                objAgrMstOnboardApprovalReport.lscloudpath = objcmnstorage.EncryptData(objAgrMstOnboardApprovalReport.lscloudpath);
                objAgrMstOnboardApprovalReport.lspath = objcmnstorage.EncryptData(objAgrMstOnboardApprovalReport.lspath);
                objAgrMstOnboardApprovalReport.status = true;
                objAgrMstOnboardApprovalReport.message = "Success";
            }

            catch (Exception ex)
            {
                objAgrMstOnboardApprovalReport.status = false;
                objAgrMstOnboardApprovalReport.message = "Failure";
            }
        }
        // Supplier Onboard Report Export Excel --- ENDING


    }
}