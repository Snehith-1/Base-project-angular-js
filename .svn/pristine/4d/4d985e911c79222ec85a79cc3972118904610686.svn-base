using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.foundation.Models;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Web;
using System.IO;
using ems.storage.Functions;



namespace ems.foundation.DataAccess
{
    public class DaFndMstCustomerMasterAdd
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objreader;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable, dt_table;
        string msSQL, msGetGid, msGetGid1, msGetGidREF, lspath, lsemployeeGID;
        string msGetDocumentGid, lsapp_refno;
        int mnResult;
        string frommail_id, sub, tomail_id, lsstate_name, lsdesignation_gid, lsgststate_name, lsgst_no, lscustomer2gst_gid, lscustomer_gid, lsgstregister_status,  lsdesignation_type, lsindividualproof_gid, lsindividualproof_name, contactperson, customer_name, body, message, ls_server, ls_username, ls_password, lscontent = string.Empty;
        string lscustomer_code, lscustomer_name, lscoi_date, lsbusinessstart_date, lsyear_business, lsaddress_typegid, lsaddress_type, lsaddressline1, lsaddressline2, lslandmark, lstaluka, lsprimary_address, lspostal_code, lscity, lsdistrict, lsstate, lscountry, lslatitude, lslongitude, lscustomer2address_gid, lsmonth_business, lsconstitution_gid, lsconstitution_name;
        string lscin_no, lspan_no, lscontactperson_firstname, lsmobile_no, lsprimary_mobileno, lswhatsapp_mobileno, lscustomer2mobileno_gid, lscontactperson_middlename, lscontactperson_lastname, lsassessmentagency_gid, lsassessmentagency_name;
        string lscreatedby, lscreateddate, lsassessmentagencyrating_gid, lsemail_address, lsprimary_emailaddress, lscustomer2emailaddress_gid, lsassessmentagencyrating_name, lsrating_date, lsamlcategory_gid, lsamlcategory_name, lsbusinesscategory_gid, lsbusinesscategory_name, lsmsme_registration, lsremarks;
        int k;
        public string     tomail_id1, tomail_id2, employeename, cc_mailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lsemployee_name, lsemployee_gid, lcampaign_status, lsstatus_remarks, lsTo2members, lscampaign_type, lsname, lscampaign_name, lscampaign_apr, lscampaign_remarks, lsBccmail_id, lscreated_by, lstomembers, lsdescription, lscc2members, lstonames, lsauditcreation_gid, lscreated_date,  lscc_mail, strBCC, lsbcc_mail;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;



        public bool DacustomerSave(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("BB");
            msGetGidREF = objcmnfunctions.GetMasterGID("BBCH_");


            msSQL = " insert into fnd_mst_tcustomer(" +
                    " customer_gid," +
                    " customer_code," +
                    " customer_name," +
                    " pan_no," +
                    " businessstart_date," +
                    " year_business," +
                    " month_business," +
                    " cin_no," +
                    " constitution_gid," +
                    " constitution_name," +
                    " assessmentagency_gid," +
                    " assessmentagency_name," +
                    " assessmentagencyrating_gid," +
                    " assessmentagencyrating_name," +
                    " rating_date," +
                    " amlcategory_gid," +
                    " amlcategory_name," +
                    " businesscategory_gid," +
                    " businesscategory_name," +
                    " contactperson_fn," +
                    " contactperson_mn," +
                    " contactperson_ln," +
                    " msme_registration," +
                    " designation_gid," +
                    " designation_type," +
                    " individualproof_gid," +
                    " individualproof_name," +
                    " remarks," +
                    " status_remarks," +
                    " msme_radio," +
                    " status," +
                    " created_by," +
                    " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                    "'" + msGetGidREF + "'," +
                    "'" + values.customer_name + "'," +
                     "'" + values.pan_no + "',";
            if ((values.businessstart_date == null) || (values.businessstart_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.businessstart_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.year_business + "'," +
                    "'" + values.month_business + "'," +
                    "'" + values.cin_no + "'," +
                    "'" + values.constitution_gid + "'," +
                    "'" + values.constitution_name + "'," +
                    "'" + values.assessmentagency_gid + "'," +
                    "'" + values.assessmentagency_name + "'," +
                    "'" + values.assessmentagencyrating_gid + "'," +
                    "'" + values.assessmentagencyrating_name + "',";
            if ((values.rating_date == null) || (values.rating_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.rating_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.amlcategory_gid + "'," +
                    "'" + values.amlcategory_name + "'," +
                    "'" + values.businesscategory_gid + "'," +
                    "'" + values.businesscategory_name + "'," +
                    "'" + values.contactperson_fn + "'," +
                    "'" + values.contactperson_mn + "'," +
                    "'" + values.contactperson_ln + "'," +
                    "'" + values.msme_registration + "'," +
                     "'" + values.designation_gid + "'," +
                    "'" + values.designation_type + "'," +
                     "'" + values.individualproof_gid + "'," +
                    "'" + values.individualproof_name + "'," +
                    "'" + values.remarks + "'," +
                    "'N'," +
                    "'" + values.msme_radio + "'," +
                    "'Y'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " update fnd_mst_tcustomer set status_remarks ='Save'  where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates

                msSQL = "update fnd_mst_tcustomer2mobileno set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2emailaddress set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2address set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tfndmanagement2cheque set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //msSQL = "update fnd_mst_tcustomer2bank set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2gst set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Customer Details Saved Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Saving Customer";
                return false;
            }

        }

        public bool DacustomerEditSave(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {

            msSQL = "customer_gid,customer_code,customer_name,pan_no,businessstart_date,year_business,month_business,cin_no,constitution_gid," +
                   "constitution_name,assessmentagency_gid,assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,rating_date,amlcategory_gid," +
                   "amlcategory_name,businesscategory_gid,businesscategory_name,contactperson_fn,contactperson_mn, contactperson_ln,msme_registration,remarks" +
                   " from fnd_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscustomer_code = objODBCDatareader["customer_code"].ToString();
                lscustomer_name = objODBCDatareader["customer_name"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                lsassessmentagency_gid = objODBCDatareader["assessmentagency_gid"].ToString();
                lsassessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                lsassessmentagencyrating_gid = objODBCDatareader["assessmentagencyrating_gid"].ToString();
                lsassessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                if (objODBCDatareader["rating_date"].ToString() == "")
                {
                }
                else
                {
                    lsrating_date = Convert.ToDateTime(objODBCDatareader["rating_date"]).ToString("dd-MM-yyyy");
                }
                lsamlcategory_gid = objODBCDatareader["amlcategory_gid"].ToString();
                lsamlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                lsbusinesscategory_gid = objODBCDatareader["businesscategory_gid"].ToString();
                lsbusinesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
                lsmsme_registration = objODBCDatareader["msme_registration"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();

            }
            objODBCDatareader.Close();
            msSQL = " update fnd_mst_tcustomer set " +
                    " customer_code='" + values.customer_code + "'," +
                    " customer_name='" + values.customer_name + "'," +
                     " pan_no='" + values.pan_no + "',";
            if (Convert.ToDateTime(values.editbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " businessstart_date='" + Convert.ToDateTime(values.editbusinessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " year_business='" + values.year_business + "'," +
                     " month_business='" + values.month_business + "'," +
                     "'" + values.cin_no + "'," +
                    "'" + values.constitution_gid + "'," +
                    "'" + values.constitution_name + "'," +
                    "'" + values.assessmentagency_gid + "'," +
                    "'" + values.assessmentagency_name + "'," +
                    "'" + values.assessmentagencyrating_gid + "'," +
                    "'" + values.assessmentagencyrating_name + "',";
            if ((values.rating_date == null) || (values.rating_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.rating_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.amlcategory_gid + "'," +
                    "'" + values.amlcategory_name + "'," +
                    "'" + values.businesscategory_gid + "'," +
                    "'" + values.businesscategory_name + "'," +
                    "'" + values.contactperson_fn + "'," +
                    "'" + values.contactperson_mn + "'," +
                    "'" + values.contactperson_ln + "'," +
                    "'" + values.msme_registration + "'," +
                    "'" + values.remarks + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("BULG");

                msSQL = "Insert into fnd_mst_tcustomerupdatelog(" +
                    " customerupdatelog_gid, " +
                    " customer_gid," +
                    " customer_code," +
                    " customer_name," +
                    " pan_no," +
                    " businessstart_date," +
                    " year_business," +
                    " month_business," +
                    " cin_no," +
                    " constitution_gid," +
                    " constitution_name," +
                    " assessmentagency_gid," +
                    " assessmentagency_name," +
                    " assessmentagencyrating_gid," +
                    " assessmentagencyrating_name," +
                    " rating_date," +
                    " amlcategory_gid," +
                    " amlcategory_name," +
                    " businesscategory_gid," +
                    " businesscategory_name," +
                    " contactperson_fn," +
                    " contactperson_mn," +
                    " contactperson_ln," +
                    " msme_registration," +
                    " remarks," +
                    " updated_by," +
                    " updated_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.customer_gid + "'," +
               "'" + lscustomer_code + "'," +
               "'" + lscustomer_name + "'," +
                 "'" + values.pan_no + "',";
                if ((values.businessstart_date == null) || (values.businessstart_date == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.businessstart_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }

                msSQL += "'" + values.year_business + "'," +
                        "'" + values.month_business + "'," +
                        "'" + values.cin_no + "'," +
                        "'" + values.constitution_gid + "'," +
                        "'" + values.constitution_name + "'," +
                        "'" + values.assessmentagency_gid + "'," +
                        "'" + values.assessmentagency_name + "'," +
                        "'" + values.assessmentagencyrating_gid + "'," +
                        "'" + values.assessmentagencyrating_name + "',";
                if ((values.rating_date == null) || (values.rating_date == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.rating_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += "'" + values.amlcategory_gid + "'," +
                        "'" + values.amlcategory_name + "'," +
                        "'" + values.businesscategory_gid + "'," +
                        "'" + values.businesscategory_name + "'," +
                        "'" + values.contactperson_fn + "'," +
                        "'" + values.contactperson_mn + "'," +
                        "'" + values.contactperson_ln + "'," +
                        "'" + values.msme_registration + "'," +
                        "'" + values.remarks + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates

                msSQL = "update fnd_mst_tcustomer2mobileno set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2emailaddress set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2address set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update fnd_mst_tcustomer2bank set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2gst set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Customer Details Saved Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Saving Customer";
                return false;
            }

        }


        public bool DacustomerSubmit(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {
            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2address where customer_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("BB");
            msGetGidREF = objcmnfunctions.GetMasterGID("BBCH_");

            msSQL = " insert into fnd_mst_tcustomer(" +
                    " customer_gid," +
                    " customer_code," +
                    " customer_name," +
                    " pan_no," +
                    " businessstart_date," +
                    " year_business," +
                    " month_business," +
                    " cin_no," +
                    " constitution_gid," +
                    " constitution_name," +
                    " assessmentagency_gid," +
                    " assessmentagency_name," +
                    " assessmentagencyrating_gid," +
                    " assessmentagencyrating_name," +
                    " rating_date," +
                    " amlcategory_gid," +
                    " amlcategory_name," +
                    " businesscategory_gid," +
                    " businesscategory_name," +
                    " contactperson_fn," +
                    " contactperson_mn," +
                    " contactperson_ln," +
                    " msme_registration," +
                    " designation_gid," +
                    " designation_type," +
                    " individualproof_gid," +
                    " individualproof_name," +
                    " remarks," +
                    " msme_radio," +
                    " status," +
                    " created_by," +
                    " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                    "'" + msGetGidREF + "'," +
                    "'" + values.customer_name + "'," +
                     "'" + values.pan_no + "',";
            if ((values.businessstart_date == null) || (values.businessstart_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.businessstart_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.year_business + "'," +
                    "'" + values.month_business + "'," +
                    "'" + values.cin_no + "'," +
                    "'" + values.constitution_gid + "'," +
                    "'" + values.constitution_name + "'," +
                    "'" + values.assessmentagency_gid + "'," +
                    "'" + values.assessmentagency_name + "'," +
                    "'" + values.assessmentagencyrating_gid + "'," +
                    "'" + values.assessmentagencyrating_name + "',";
            if ((values.rating_date == null) || (values.rating_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.rating_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.amlcategory_gid + "'," +
                    "'" + values.amlcategory_name + "'," +
                    "'" + values.businesscategory_gid + "'," +
                    "'" + values.businesscategory_name + "'," +
                    "'" + values.contactperson_fn + "'," +
                    "'" + values.contactperson_mn + "'," +
                    "'" + values.contactperson_ln + "'," +
                    "'" + values.msme_registration + "'," +
                     "'" + values.designation_gid + "'," +
                    "'" + values.designation_type + "'," +
                     "'" + values.individualproof_gid + "'," +
                    "'" + values.individualproof_name + "'," +
                    "'" + values.remarks + "'," +
                    "'" + values.msme_radio + "'," +
                    "'Y'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update fnd_mst_tcustomer set status_remarks ='Pending'  where customer_gid='" + msGetGid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates

                msSQL = "update fnd_mst_tcustomer2mobileno set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2emailaddress set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2address set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update fnd_mst_tcustomer2bank set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update fnd_mst_tfndmanagement2cheque set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2gst set customer_gid ='" + msGetGid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Customer Details Submitted Successfully";
                
            }

            else
            {
                values.status = false;
                values.message = "Error Occured While Submitting Customer";
                return false;
            }
            return true;
        }


        public bool DacustomerEditSubmit(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {

            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2address where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }

            msSQL = "select customer_gid,customer_code,customer_name,pan_no,businessstart_date,year_business,month_business,cin_no,constitution_gid," +
                   "constitution_name,assessmentagency_gid,assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,rating_date,amlcategory_gid," +
                   "amlcategory_name,businesscategory_gid,businesscategory_name,contactperson_fn,contactperson_mn, contactperson_ln,msme_registration,remarks" +
                   " from fnd_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscustomer_code = objODBCDatareader["customer_code"].ToString();
                lscustomer_name = objODBCDatareader["customer_name"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                lsassessmentagency_gid = objODBCDatareader["assessmentagency_gid"].ToString();
                lsassessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                lsassessmentagencyrating_gid = objODBCDatareader["assessmentagencyrating_gid"].ToString();
                lsassessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                if (objODBCDatareader["rating_date"].ToString() == "")
                {
                }
                else
                {
                    lsrating_date = Convert.ToDateTime(objODBCDatareader["rating_date"]).ToString("dd-MM-yyyy");
                }
                lsamlcategory_gid = objODBCDatareader["amlcategory_gid"].ToString();
                lsamlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                lsbusinesscategory_gid = objODBCDatareader["businesscategory_gid"].ToString();
                lsbusinesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
                lsmsme_registration = objODBCDatareader["msme_registration"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();

            }
            objODBCDatareader.Close();
            msSQL = " update fnd_mst_tcustomer set " +
                    " customer_code='" + values.customer_code + "'," +
                    " customer_name='" + values.customer_name + "'," +
                     " pan_no='" + values.pan_no + "',";
            //if (Convert.ToDateTime(values.editbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            if (lsbusinessstart_date == values.businessstart_date)
            {

            }
            else
            {
                msSQL += " businessstart_date='" + Convert.ToDateTime(values.businessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }

            msSQL += " year_business='" + values.year_business + "'," +
                     " month_business='" + values.month_business + "'," +
                     " cin_no='" + values.cin_no + "'," +
                    "constitution_gid='" + values.constitution_gid + "'," +
                    "constitution_name='" + values.constitution_name + "'," +
                    "assessmentagency_gid='" + values.assessmentagency_gid + "'," +
                    "assessmentagency_name='" + values.assessmentagency_name + "'," +
                    "assessmentagencyrating_gid='" + values.assessmentagencyrating_gid + "'," +
                    "assessmentagencyrating_name='" + values.assessmentagencyrating_name + "',";
            //if ((values.rating_date == null) || (values.rating_date == ""))
            //{
            //    msSQL += "null,";
            //}
            //else
            //{
            //    msSQL += "rating_date='" + Convert.ToDateTime(values.rating_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            //}
            if (lsrating_date == values.rating_date)
            {

            }
            else
            {
                msSQL += " rating_date='" + Convert.ToDateTime(values.rating_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += "amlcategory_gid ='" + values.amlcategory_gid + "'," +
                    "amlcategory_name ='" + values.amlcategory_name + "'," +
                    "businesscategory_gid ='" + values.businesscategory_gid + "'," +
                    "businesscategory_name ='" + values.businesscategory_name + "'," +
                    "contactperson_fn ='" + values.contactperson_fn + "'," +
                    "contactperson_mn ='" + values.contactperson_mn + "'," +
                    "contactperson_ln ='" + values.contactperson_ln + "'," +
                    "msme_registration ='" + values.msme_registration + "'," +
                     " designation_gid = '" + values.designation_gid + "'," +
                    " designation_type ='" + values.designation_type + "'," +
                    " individualproof_gid ='" + values.individualproof_gid + "'," +
                    " individualproof_name ='" + values.individualproof_name + "'," +
                    "remarks='" + values.remarks + "'," +
                    "msme_radio='" + values.msme_radio + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("BULG");

                msSQL = "Insert into fnd_mst_tcustomerupdatelog(" +
               " customerupdatelog_gid, " +
               " customer_gid, " +
               " customer_code," +
               " customer_name," +
               " coi_date," +
               " businessstart_date," +
               " year_business," +
               " month_business," +
               " constitution_gid," +
               " constitution_name," +
               " cin_no," +
               " pan_no," +
               " contactperson_fn," +
               " contactperson_mn," +
               " contactperson_ln," +
               " updated_by," +
               " updated_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.customer_gid + "'," +
               "'" + lscustomer_code + "'," +
               "'" + lscustomer_name + "'," +
               "'" + lscoi_date + "'," +
               "'" + lsbusinessstart_date + "'," +
               "'" + lsyear_business + "'," +
                         "'" + lsmonth_business + "'," +
                         "'" + lsconstitution_gid + "'," +
                         "'" + lsconstitution_name + "'," +
                         "'" + lscin_no + "'," +
                         "'" + lspan_no + "'," +
                         "'" + lscontactperson_firstname + "'," +
                         "'" + lscontactperson_middlename + "'," +
                         "'" + lscontactperson_lastname + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " update fnd_mst_tcustomer set status_remarks ='Pending'  where customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //Updates

                msSQL = "update fnd_mst_tcustomer2mobileno set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2emailaddress set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2address set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update fnd_mst_tcustomer2bank set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2gst set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Customer Details Submitted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }
        public bool DacustomerEditupdated(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {

            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2address where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }

            msSQL = "select customer_gid,customer_code,customer_name,pan_no,businessstart_date,year_business,month_business,cin_no,constitution_gid," +
                   "constitution_name,assessmentagency_gid,assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,rating_date,amlcategory_gid," +
                   "amlcategory_name,businesscategory_gid,businesscategory_name,contactperson_fn,contactperson_mn, contactperson_ln,msme_registration,remarks" +
                   " from fnd_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscustomer_code = objODBCDatareader["customer_code"].ToString();
                lscustomer_name = objODBCDatareader["customer_name"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                lsassessmentagency_gid = objODBCDatareader["assessmentagency_gid"].ToString();
                lsassessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                lsassessmentagencyrating_gid = objODBCDatareader["assessmentagencyrating_gid"].ToString();
                lsassessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                if (objODBCDatareader["rating_date"].ToString() == "")
                {
                }
                else
                {
                    lsrating_date = Convert.ToDateTime(objODBCDatareader["rating_date"]).ToString("dd-MM-yyyy");
                }
                lsamlcategory_gid = objODBCDatareader["amlcategory_gid"].ToString();
                lsamlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                lsbusinesscategory_gid = objODBCDatareader["businesscategory_gid"].ToString();
                lsbusinesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
                lsmsme_registration = objODBCDatareader["msme_registration"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();

            }
            objODBCDatareader.Close();
            msSQL = " update fnd_mst_tcustomer set " +
                    " customer_code='" + values.customer_code + "'," +
                    " customer_name='" + values.customer_name + "'," +
                     " pan_no='" + values.pan_no + "',";
            //if (Convert.ToDateTime(values.editbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            if (lsbusinessstart_date == values.businessstart_date)
            {

            }
            else
            {
                msSQL += " businessstart_date='" + Convert.ToDateTime(values.businessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }

            msSQL += " year_business='" + values.year_business + "'," +
                     " month_business='" + values.month_business + "'," +
                     " cin_no='" + values.cin_no + "'," +
                    "constitution_gid='" + values.constitution_gid + "'," +
                    "constitution_name='" + values.constitution_name + "'," +
                    "assessmentagency_gid='" + values.assessmentagency_gid + "'," +
                    "assessmentagency_name='" + values.assessmentagency_name + "'," +
                    "assessmentagencyrating_gid='" + values.assessmentagencyrating_gid + "'," +
                    "assessmentagencyrating_name='" + values.assessmentagencyrating_name + "',";
            //if ((values.rating_date == null) || (values.rating_date == ""))
            //{
            //    msSQL += "null,";
            //}
            //else
            //{
            //    msSQL += "rating_date='" + Convert.ToDateTime(values.rating_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            //}
            if (lsrating_date == values.rating_date)
            {

            }
            else
            {
                msSQL += " rating_date='" + Convert.ToDateTime(values.rating_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += "amlcategory_gid ='" + values.amlcategory_gid + "'," +
                    "amlcategory_name ='" + values.amlcategory_name + "'," +
                    "businesscategory_gid ='" + values.businesscategory_gid + "'," +
                    "businesscategory_name ='" + values.businesscategory_name + "'," +
                    "contactperson_fn ='" + values.contactperson_fn + "'," +
                    "contactperson_mn ='" + values.contactperson_mn + "'," +
                    "contactperson_ln ='" + values.contactperson_ln + "'," +
                    "msme_registration ='" + values.msme_registration + "'," +
                     " designation_gid = '" + values.designation_gid + "'," +
                    " designation_type ='" + values.designation_type + "'," +
                    " individualproof_gid ='" + values.individualproof_gid + "'," +
                    " individualproof_name ='" + values.individualproof_name + "'," +
                    "remarks='" + values.remarks + "'," +
                    "msme_radio='" + values.msme_radio + "'," +
                    //"status = 'Y'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("BULG");

                msSQL = "Insert into fnd_mst_tcustomerupdatelog(" +
               " customerupdatelog_gid, " +
               " customer_gid, " +
               " customer_code," +
               " customer_name," +
               " coi_date," +
               " businessstart_date," +
               " year_business," +
               " month_business," +
               " constitution_gid," +
               " constitution_name," +
               " cin_no," +
               " pan_no," +
               " contactperson_fn," +
               " contactperson_mn," +
               " contactperson_ln," +
               " updated_by," +
               " updated_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.customer_gid + "'," +
               "'" + lscustomer_code + "'," +
               "'" + lscustomer_name + "'," +
               "'" + lscoi_date + "'," +
               "'" + lsbusinessstart_date + "'," +
               "'" + lsyear_business + "'," +
                         "'" + lsmonth_business + "'," +
                         "'" + lsconstitution_gid + "'," +
                         "'" + lsconstitution_name + "'," +
                         "'" + lscin_no + "'," +
                         "'" + lspan_no + "'," +
                         "'" + lscontactperson_firstname + "'," +
                         "'" + lscontactperson_middlename + "'," +
                         "'" + lscontactperson_lastname + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //msSQL = " update fnd_mst_tcustomer set status_remarks ='N'  where customer_gid='" + values.customer_gid + "' ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //Updates

                msSQL = "update fnd_mst_tcustomer2mobileno set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2emailaddress set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2address set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update fnd_mst_tcustomer2bank set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2gst set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Customer Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }
        public void DaGetconstitution(MdlFndMstCustomerMasterAdd objconstitution)
        {
            try
            {
                msSQL = " SELECT constitution_gid,constitution_name FROM ocs_mst_tconstitution ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getconstitution_list = new List<constitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getconstitution_list.Add(new constitution_list
                        {
                            constitution_gid = (dr_datarow["constitution_gid"].ToString()),
                            constitution_name = (dr_datarow["constitution_name"].ToString()),
                        });
                    }
                    objconstitution.constitution_list = getconstitution_list;
                }
                dt_datatable.Dispose();

                objconstitution.status = true;
            }
            catch
            {
                objconstitution.status = false;
            }

        }

        public void DaGetassessmentagency(MdlFndMstCustomerMasterAdd objassessmentagency)
        {
            try
            {
                msSQL = " SELECT assessmentagency_gid,assessmentagency_name FROM ocs_mst_tassessmentagency ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getassessmentagency_list = new List<assessmentagency_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getassessmentagency_list.Add(new assessmentagency_list
                        {
                            assessmentagency_gid = (dr_datarow["assessmentagency_gid"].ToString()),
                            assessmentagency_name = (dr_datarow["assessmentagency_name"].ToString()),
                        });
                    }
                    objassessmentagency.assessmentagency_list = getassessmentagency_list;
                }
                dt_datatable.Dispose();

                objassessmentagency.status = true;
            }
            catch
            {
                objassessmentagency.status = false;
            }

        }

        public void DaGetassessmentagencyrating(MdlFndMstCustomerMasterAdd objassessmentagencyrating)
        {
            try
            {
                msSQL = " SELECT assessmentagencyrating_gid,assessmentagencyrating_name FROM ocs_mst_tassessmentagencyrating ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getassessmentagencyrating_list = new List<assessmentagencyrating_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getassessmentagencyrating_list.Add(new assessmentagencyrating_list
                        {
                            assessmentagencyrating_gid = (dr_datarow["assessmentagencyrating_gid"].ToString()),
                            assessmentagencyrating_name = (dr_datarow["assessmentagencyrating_name"].ToString()),
                        });
                    }
                    objassessmentagencyrating.assessmentagencyrating_list = getassessmentagencyrating_list;
                }
                dt_datatable.Dispose();

                objassessmentagencyrating.status = true;
            }
            catch
            {
                objassessmentagencyrating.status = false;
            }

        }

        public void DaGetamlcategory(MdlFndMstCustomerMasterAdd objamlcategory)
        {
            try
            {
                msSQL = " SELECT amlcategory_gid,amlcategory_name FROM ocs_mst_tamlcategory ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getamlcategory_list = new List<amlcategory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getamlcategory_list.Add(new amlcategory_list
                        {
                            amlcategory_gid = (dr_datarow["amlcategory_gid"].ToString()),
                            amlcategory_name = (dr_datarow["amlcategory_name"].ToString()),
                        });
                    }
                    objamlcategory.amlcategory_list = getamlcategory_list;
                }
                dt_datatable.Dispose();

                objamlcategory.status = true;
            }
            catch
            {
                objamlcategory.status = false;
            }

        }

        public void DaGetbusinesscategory(MdlFndMstCustomerMasterAdd objbusinesscategory)
        {
            try
            {
                msSQL = " SELECT businesscategory_gid,businesscategory_name FROM ocs_mst_tbusinesscategory ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbusinesscategory_list = new List<businesscategory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbusinesscategory_list.Add(new businesscategory_list
                        {
                            businesscategory_gid = (dr_datarow["businesscategory_gid"].ToString()),
                            businesscategory_name = (dr_datarow["businesscategory_name"].ToString()),
                        });
                    }
                    objbusinesscategory.businesscategory_list = getbusinesscategory_list;
                }
                dt_datatable.Dispose();

                objbusinesscategory.status = true;
            }
            catch
            {
                objbusinesscategory.status = false;
            }

        }



        public void DaGetdesignation(MdlFndMstCustomerMasterAdd objdesignation)
        {
            try
            {
                msSQL = " SELECT designation_gid,designation_type FROM ocs_mst_tdesignation ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdesignation_list = new List<designation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdesignation_list.Add(new designation_list
                        {
                            designation_gid = (dr_datarow["designation_gid"].ToString()),
                            designation_type = (dr_datarow["designation_type"].ToString()),
                        });
                    }
                    objdesignation.designation_list = getdesignation_list;
                }
                dt_datatable.Dispose();

                objdesignation.status = true;
            }
            catch
            {
                objdesignation.status = false;
            }

        }

        public void DaGetindividualproof(MdlFndMstCustomerMasterAdd objindividualproof)
        {
            try
            {
                msSQL = " SELECT individualproof_gid,individualproof_name FROM ocs_mst_tindividualproof ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getindividualproof_list = new List<individualproof_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getindividualproof_list.Add(new individualproof_list
                        {
                            individualproof_gid = (dr_datarow["individualproof_gid"].ToString()),
                            individualproof_name = (dr_datarow["individualproof_name"].ToString()),
                        });
                    }
                    objindividualproof.individualproof_list = getindividualproof_list;
                }
                dt_datatable.Dispose();

                objindividualproof.status = true;
            }
            catch
            {
                objindividualproof.status = false;
            }

        }

        public void DaGetState(MdlFndMstCustomerMasterAdd objState)
        {
            try
            {
                msSQL = " SELECT state_gid,state_name FROM ocs_mst_tstate ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getState = new List<state_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getState.Add(new state_list
                        {
                            state_gid = (dr_datarow["state_gid"].ToString()),
                            state_name = (dr_datarow["state_name"].ToString()),
                        });
                    }
                    objState.state_list = getState;
                }
                dt_datatable.Dispose();

                objState.status = true;
            }
            catch
            {
                objState.status = false;
            }

        }

        public bool DacustomerEditUpdate(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {

            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2address where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }

            msSQL = "select customer_gid,customer_code,customer_name,pan_no,businessstart_date,year_business,month_business,cin_no,constitution_gid," +
                  "constitution_name,assessmentagency_gid,assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,rating_date,amlcategory_gid," +
                  "amlcategory_name,businesscategory_gid,businesscategory_name,contactperson_fn,contactperson_mn, contactperson_ln,msme_registration,remarks" +
                  " from fnd_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //lscustomer_code = objODBCDatareader["customer_code"].ToString();
                lscustomer_name = objODBCDatareader["customer_name"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                lsassessmentagency_gid = objODBCDatareader["assessmentagency_gid"].ToString();
                lsassessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                lsassessmentagencyrating_gid = objODBCDatareader["assessmentagencyrating_gid"].ToString();
                lsassessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                if (objODBCDatareader["rating_date"].ToString() == "")
                {
                }
                else
                {
                    lsrating_date = Convert.ToDateTime(objODBCDatareader["rating_date"]).ToString("dd-MM-yyyy");
                }
                lsamlcategory_gid = objODBCDatareader["amlcategory_gid"].ToString();
                lsamlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                lsbusinesscategory_gid = objODBCDatareader["businesscategory_gid"].ToString();
                lsbusinesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
                lsmsme_registration = objODBCDatareader["msme_registration"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();

            }
            objODBCDatareader.Close();
            msSQL = " update fnd_mst_tcustomer set " +
                    " customer_code='" + values.customer_code + "'," +
                    " customer_name='" + values.customer_name + "'," +
                     " pan_no='" + values.pan_no + "',";
            if (Convert.ToDateTime(values.editbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " businessstart_date='" + Convert.ToDateTime(values.editbusinessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " year_business='" + values.year_business + "'," +
                     " month_business='" + values.month_business + "'," +
                     "cin_no='" + values.cin_no + "'," +
                    "constitution_gid='" + values.constitution_gid + "'," +
                    "constitution_name='" + values.constitution_name + "'," +
                    "assessmentagency_gid='" + values.assessmentagency_gid + "'," +
                    "assessmentagency_name='" + values.assessmentagency_name + "'," +
                    "assessmentagencyrating_gid='" + values.assessmentagencyrating_gid + "'," +
                    "assessmentagencyrating_name='" + values.assessmentagencyrating_name + "',";
            //if ((values.rating_date == null) || (values.rating_date == ""))
            //{
            //    msSQL += "null,";
            //}
            //else
            //{
            //    msSQL += "rating_date='" + Convert.ToDateTime(values.rating_date).ToString("dd-MM-yyyy") + "',";
            //}
            if (Convert.ToDateTime(values.editrating_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " rating_date='" + Convert.ToDateTime(values.editrating_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += "amlcategory_gid='" + values.amlcategory_gid + "'," +
                    "amlcategory_name='" + values.amlcategory_name + "'," +
                    "businesscategory_gid='" + values.businesscategory_gid + "'," +
                    "businesscategory_name='" + values.businesscategory_name + "'," +
                    "contactperson_fn='" + values.contactperson_fn + "'," +
                    "contactperson_mn='" + values.contactperson_mn + "'," +
                    "contactperson_ln='" + values.contactperson_ln + "'," +
                    "msme_registration='" + values.msme_registration + "'," +
                    "remarks='" + values.remarks + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " update fnd_mst_tcustomer set status_remarks ='Save'  where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("BULG");

                msSQL = "Insert into fnd_mst_tcustomerupdatelog(" +
               " customerupdatelog_gid, " +
               " customer_gid, " +
               " customer_code," +
               " customer_name," +
               " coi_date," +
               " businessstart_date," +
               " year_business," +
               " month_business," +
               " constitution_gid," +
               " constitution_name," +
               " cin_no," +
               " pan_no," +
               " contactperson_fn," +
               " contactperson_mn," +
               " contactperson_ln," +
               " updated_by," +
               " updated_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.customer_gid + "'," +
               "'" + lscustomer_code + "'," +
               "'" + lscustomer_name + "'," +
               "'" + lscoi_date + "'," +
               "'" + lsbusinessstart_date + "'," +
               "'" + lsyear_business + "'," +
                         "'" + lsmonth_business + "'," +
                         "'" + lsconstitution_gid + "'," +
                         "'" + lsconstitution_name + "'," +
                         "'" + lscin_no + "'," +
                         "'" + lspan_no + "'," +
                         "'" + lscontactperson_firstname + "'," +
                         "'" + lscontactperson_middlename + "'," +
                         "'" + lscontactperson_lastname + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates

                msSQL = "update fnd_mst_tcustomer2mobileno set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2emailaddress set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2address set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update fnd_mst_tfndmanagement2cheque set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update fnd_mst_tcustomer2bank set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2gst set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "customer Details Updated Successfully";
                try
                {
                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = "select email_id from hrm_mst_tdepartment where department_name='Credit Administration'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select  date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'created_date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by" +
                    " from fnd_mst_tcustomer a  left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid  " +
                    " where customer_gid='" + values.customer_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscreatedby = objODBCDatareader["created_by"].ToString();
                        lscreateddate = objODBCDatareader["created_date"].ToString();
                    }
                    //objODBCDatareader.Close();
                    //sub = "Customer ";
                    //body = "Dear Sir/Madam,  <br />";
                    //body = body + "<br />";
                    //body = body + "Greetings,  <br />";
                    //body = body + "<br />";
                    //body = body + "A New Customer is Created By " + lscreatedby + "  On:" + lscreateddate + "<br />";
                    //body = body + "<br />";
                    //body = body + "<b>Thanks & Regards, </b> ";
                    //body = body + "<br />";
                    //body = body + "<b> Team customer ";
                    //body = body + "<br />";
                    //MailMessage message = new MailMessage();
                    //SmtpClient smtp = new SmtpClient();
                    //message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    //message.Subject = sub;
                    //message.IsBodyHtml = true; //to make message body as html  
                    //message.Body = body;
                    //smtp.Port = ls_port;
                    //smtp.Host = ls_server; //for gmail host  
                    //smtp.EnableSsl = true;
                    //smtp.UseDefaultCredentials = false;
                    //smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    ////smtp.Send(message);
                    //values.status = true;
                    //return true;
                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                    return false;
                }
            }

            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Customer";
                return false;
            }
            return true;
        }



        public bool Dacustomersubmitapproval(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {

            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2address where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }

            msSQL = "select customer_gid,customer_code,customer_name,pan_no,businessstart_date,year_business,month_business,cin_no,constitution_gid," +
                  "constitution_name,assessmentagency_gid,assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,rating_date,amlcategory_gid," +
                  "amlcategory_name,businesscategory_gid,businesscategory_name,contactperson_fn,contactperson_mn, contactperson_ln,msme_registration,remarks" +
                  " from fnd_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //lscustomer_code = objODBCDatareader["customer_code"].ToString();
                lscustomer_name = objODBCDatareader["customer_name"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                lsassessmentagency_gid = objODBCDatareader["assessmentagency_gid"].ToString();
                lsassessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                lsassessmentagencyrating_gid = objODBCDatareader["assessmentagencyrating_gid"].ToString();
                lsassessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                if (objODBCDatareader["rating_date"].ToString() == "")
                {
                }
                else
                {
                    lsrating_date = Convert.ToDateTime(objODBCDatareader["rating_date"]).ToString("dd-MM-yyyy");
                }
                lsamlcategory_gid = objODBCDatareader["amlcategory_gid"].ToString();
                lsamlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                lsbusinesscategory_gid = objODBCDatareader["businesscategory_gid"].ToString();
                lsbusinesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
                lsmsme_registration = objODBCDatareader["msme_registration"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();

            }
            objODBCDatareader.Close();
            msSQL = " update fnd_mst_tcustomer set " +
                    " customer_code='" + values.customer_code + "'," +
                    " customer_name='" + values.customer_name + "'," +
                     " pan_no='" + values.pan_no + "',";
            if (Convert.ToDateTime(values.editbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " businessstart_date='" + Convert.ToDateTime(values.editbusinessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " year_business='" + values.year_business + "'," +
                     " month_business='" + values.month_business + "'," +
                     "cin_no='" + values.cin_no + "'," +
                    "constitution_gid='" + values.constitution_gid + "'," +
                    "constitution_name='" + values.constitution_name + "'," +
                    "assessmentagency_gid='" + values.assessmentagency_gid + "'," +
                    "assessmentagency_name='" + values.assessmentagency_name + "'," +
                    "assessmentagencyrating_gid='" + values.assessmentagencyrating_gid + "'," +
                    "assessmentagencyrating_name='" + values.assessmentagencyrating_name + "',";
            //if ((values.rating_date == null) || (values.rating_date == ""))
            //{
            //    msSQL += "null,";
            //}
            //else
            //{
            //    msSQL += "rating_date='" + Convert.ToDateTime(values.rating_date).ToString("dd-MM-yyyy") + "',";
            //}
            if (Convert.ToDateTime(values.editrating_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " rating_date='" + Convert.ToDateTime(values.editrating_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += "amlcategory_gid='" + values.amlcategory_gid + "'," +
                    "amlcategory_name='" + values.amlcategory_name + "'," +
                    "businesscategory_gid='" + values.businesscategory_gid + "'," +
                    "businesscategory_name='" + values.businesscategory_name + "'," +
                    "contactperson_fn='" + values.contactperson_fn + "'," +
                    "contactperson_mn='" + values.contactperson_mn + "'," +
                    "contactperson_ln='" + values.contactperson_ln + "'," +
                    "msme_registration='" + values.msme_registration + "'," +
                    "remarks='" + values.remarks + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update fnd_mst_tcustomer set status ='Pending'  where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("BULG");

                msSQL = "Insert into fnd_mst_tcustomerupdatelog(" +
               " customerupdatelog_gid, " +
               " customer_gid, " +
               " customer_code," +
               " customer_name," +
               " coi_date," +
               " businessstart_date," +
               " year_business," +
               " month_business," +
               " constitution_gid," +
               " constitution_name," +
               " cin_no," +
               " pan_no," +
               " contactperson_fn," +
               " contactperson_mn," +
               " contactperson_ln," +
               " updated_by," +
               " updated_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.customer_gid + "'," +
               "'" + lscustomer_code + "'," +
               "'" + lscustomer_name + "'," +
               "'" + lscoi_date + "'," +
               "'" + lsbusinessstart_date + "'," +
               "'" + lsyear_business + "'," +
                         "'" + lsmonth_business + "'," +
                         "'" + lsconstitution_gid + "'," +
                         "'" + lsconstitution_name + "'," +
                         "'" + lscin_no + "'," +
                         "'" + lspan_no + "'," +
                         "'" + lscontactperson_firstname + "'," +
                         "'" + lscontactperson_middlename + "'," +
                         "'" + lscontactperson_lastname + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates

                msSQL = "update fnd_mst_tcustomer2mobileno set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2emailaddress set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2address set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update fnd_mst_tfndmanagement2cheque set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update fnd_mst_tcustomer2bank set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2gst set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Customer Details Submitted Successfully";
                try
                {
                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = "select email_id from hrm_mst_tdepartment where department_name='Credit Administration'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select  date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'created_date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by" +
                    " from fnd_mst_tcustomer a  left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid  " +
                    " where customer_gid='" + values.customer_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscreatedby = objODBCDatareader["created_by"].ToString();
                        lscreateddate = objODBCDatareader["created_date"].ToString();
                    }
                    //objODBCDatareader.Close();
                    //sub = "Customer ";
                    //body = "Dear Sir/Madam,  <br />";
                    //body = body + "<br />";
                    //body = body + "Greetings,  <br />";
                    //body = body + "<br />";
                    //body = body + "A New Customer is Created By " + lscreatedby + "  On:" + lscreateddate + "<br />";
                    //body = body + "<br />";
                    //body = body + "<b>Thanks & Regards, </b> ";
                    //body = body + "<br />";
                    //body = body + "<b> Team customer ";
                    //body = body + "<br />";
                    //MailMessage message = new MailMessage();
                    //SmtpClient smtp = new SmtpClient();
                    //message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    //message.Subject = sub;
                    //message.IsBodyHtml = true; //to make message body as html  
                    //message.Body = body;
                    //smtp.Port = ls_port;
                    //smtp.Host = ls_server; //for gmail host  
                    //smtp.EnableSsl = true;
                    //smtp.UseDefaultCredentials = false;
                    //smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    ////smtp.Send(message);
                    //values.status = true;
                    //return true;
                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                    return false;
                }
            }

            else
            {
                values.status = false;
                values.message = "Error Occured While Submitted Customer";
                return false;
            }
            return true;
        }
        public bool DaapproverEditUpdate(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {
            msSQL = " select count(customer_gid) as openquery from fnd_trn_tcustomerraisequery where customer_gid = '" + values.customer_gid + "'" +
                                " and customerraisequery_status = 'Query Raised'";
            values.openquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.openquerycount == "0")
            {
                msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "' and primary_mobileno='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Add Primary Mobile Number";
                    return false;
                }

                msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Add Atleast One Mobile Number";
                    return false;
                }

                msSQL = "select customer_gid from fnd_mst_tcustomer2address where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Add Atleast One Address";
                    return false;
                }

                msSQL = "select customer_gid,customer_code,customer_name,pan_no,businessstart_date,year_business,month_business,cin_no,constitution_gid," +
                      "constitution_name,assessmentagency_gid,assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,rating_date,amlcategory_gid," +
                      "amlcategory_name,businesscategory_gid,businesscategory_name,contactperson_fn,contactperson_mn, contactperson_ln,msme_registration,remarks" +
                      " from fnd_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    //lscustomer_code = objODBCDatareader["customer_code"].ToString();
                    lscustomer_name = objODBCDatareader["customer_name"].ToString();
                    lspan_no = objODBCDatareader["pan_no"].ToString();
                    if (objODBCDatareader["businessstart_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                    }
                    lsyear_business = objODBCDatareader["year_business"].ToString();
                    lsmonth_business = objODBCDatareader["month_business"].ToString();
                    lscin_no = objODBCDatareader["cin_no"].ToString();
                    lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                    lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                    lsassessmentagency_gid = objODBCDatareader["assessmentagency_gid"].ToString();
                    lsassessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                    lsassessmentagencyrating_gid = objODBCDatareader["assessmentagencyrating_gid"].ToString();
                    lsassessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                    if (objODBCDatareader["rating_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        lsrating_date = Convert.ToDateTime(objODBCDatareader["rating_date"]).ToString("dd-MM-yyyy");
                    }
                    lsamlcategory_gid = objODBCDatareader["amlcategory_gid"].ToString();
                    lsamlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                    lsbusinesscategory_gid = objODBCDatareader["businesscategory_gid"].ToString();
                    lsbusinesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                    lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                    lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                    lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
                    lsmsme_registration = objODBCDatareader["msme_registration"].ToString();
                    lsremarks = objODBCDatareader["remarks"].ToString();

                }
                objODBCDatareader.Close();
                msSQL = " update fnd_mst_tcustomer set " +
                        " customer_code='" + values.customer_code + "'," +
                        " customer_name='" + values.customer_name + "'," +
                         " pan_no='" + values.pan_no + "',";
                if (Convert.ToDateTime(values.editbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " businessstart_date='" + Convert.ToDateTime(values.editbusinessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " year_business='" + values.year_business + "'," +
                         " month_business='" + values.month_business + "'," +
                         "cin_no='" + values.cin_no + "'," +
                        "constitution_gid='" + values.constitution_gid + "'," +
                        "constitution_name='" + values.constitution_name + "'," +
                        "assessmentagency_gid='" + values.assessmentagency_gid + "'," +
                        "assessmentagency_name='" + values.assessmentagency_name + "'," +
                        "assessmentagencyrating_gid='" + values.assessmentagencyrating_gid + "'," +
                        "assessmentagencyrating_name='" + values.assessmentagencyrating_name + "',";
                //if ((values.rating_date == null) || (values.rating_date == ""))
                //{
                //    msSQL += "null,";
                //}
                //else
                //{
                //    msSQL += "rating_date='" + Convert.ToDateTime(values.rating_date).ToString("dd-MM-yyyy") + "',";
                //}
                if (Convert.ToDateTime(values.editrating_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " rating_date='" + Convert.ToDateTime(values.editrating_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += "amlcategory_gid='" + values.amlcategory_gid + "'," +
                        "amlcategory_name='" + values.amlcategory_name + "'," +
                        "businesscategory_gid='" + values.businesscategory_gid + "'," +
                        "businesscategory_name='" + values.businesscategory_name + "'," +
                        "contactperson_fn='" + values.contactperson_fn + "'," +
                        "contactperson_mn='" + values.contactperson_mn + "'," +
                        "contactperson_ln='" + values.contactperson_ln + "'," +
                        "msme_registration='" + values.msme_registration + "'," +
                        "remarks='" + values.remarks + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update fnd_mst_tcustomer set status_remarks ='Approved' where customer_gid='" + values.customer_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {

                    msGetGid = objcmnfunctions.GetMasterGID("BULG");

                    msSQL = "Insert into fnd_mst_tcustomerupdatelog(" +
                   " customerupdatelog_gid, " +
                   " customer_gid, " +
                   " customer_code," +
                   " customer_name," +
                   " coi_date," +
                   " businessstart_date," +
                   " year_business," +
                   " month_business," +
                   " constitution_gid," +
                   " constitution_name," +
                   " cin_no," +
                   " pan_no," +
                   " contactperson_fn," +
                   " contactperson_mn," +
                   " contactperson_ln," +
                   " updated_by," +
                   " updated_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.customer_gid + "'," +
                   "'" + lscustomer_code + "'," +
                   "'" + lscustomer_name + "'," +
                   "'" + lscoi_date + "'," +
                   "'" + lsbusinessstart_date + "'," +
                   "'" + lsyear_business + "'," +
                             "'" + lsmonth_business + "'," +
                             "'" + lsconstitution_gid + "'," +
                             "'" + lsconstitution_name + "'," +
                             "'" + lscin_no + "'," +
                             "'" + lspan_no + "'," +
                             "'" + lscontactperson_firstname + "'," +
                             "'" + lscontactperson_middlename + "'," +
                             "'" + lscontactperson_lastname + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //Updates

                    msSQL = "update fnd_mst_tcustomer2mobileno set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update fnd_mst_tcustomer2emailaddress set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update fnd_mst_tcustomer2address set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "update fnd_mst_tfndmanagement2cheque set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //msSQL = "update fnd_mst_tcustomer2bank set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update fnd_mst_tcustomer2gst set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "customer Details Updated Successfully";
                    try
                    {
                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select  group_concat(distinct a.created_by)  as To2members, status_remarks  from fnd_mst_tcustomer a" +
                            " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                            " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                        " where a.customer_gid ='" + values.customer_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsTo2members = objODBCDatareader["To2members"].ToString();
                            lsstatus_remarks = objODBCDatareader["status_remarks"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                                " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                " where employee_gid = '" + employee_gid + "'";
                        string employee_name = objdbconn.GetExecuteScalar(msSQL);

                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                        sToken = "";
                        int Length = 100;
                        for (int j = 0; j < Length; j++)
                        {
                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                            sToken += sTempChars;
                        }

                        k = k + 1;


                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                        sub = "RE: Customer Approval ";
                        body = "Dear Team,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "This customer creation has been approved.  <br />";
                        body = body + "<br />";
                        body = body + "<b> Customer :</b> " + HttpUtility.HtmlEncode(lscustomer_name) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Status :</b> " + HttpUtility.HtmlEncode(lsstatus_remarks) + "<br />";
                        //body = body + "<br />";
                        //body = body + "<b>Checkpoint Group :</b> " + lscheckpointgroup_name + "<br />";
                        body = body + "<br />";
                        body = body + "Kindly log into systems to view more details.";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "Thanks & Regards, ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(employee_name);
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(lsto_mail));


                        lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

                        if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                        {
                            lsBCCReceipients = lsBccmail_id.Split(',');
                            if (lsBccmail_id.Length == 0)
                            {
                                message.Bcc.Add(new MailAddress(lsBccmail_id));
                            }
                            else
                            {
                                foreach (string BCCEmail in lsBCCReceipients)
                                {
                                    message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                }
                            }
                        }
                        if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                        {
                            lsToReceipients = lsto_mail.Split(',');
                            if (lsto_mail.Length == 0)
                            {
                                message.To.Add(new MailAddress(lsto_mail));
                            }
                            else
                            {
                                foreach (string ToEmail in lsToReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                                }
                            }
                        }

                        //if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                        //{
                        //    lsCCReceipients = cc_mailid.Split(',');
                        //    if (cc_mailid.Length == 0)
                        //    {
                        //        message.CC.Add(new MailAddress(cc_mailid));
                        //    }
                        //    else
                        //    {
                        //        foreach (string CCEmail in lsCCReceipients)
                        //        {
                        //            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        //        }
                        //    }
                        //}

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
                        smtp.Send(message);

                        values.status = true;


                        if (values.status == true)
                        {
                            msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                            " customer_gid," +
                            " from_mail," +
                            " to_mail," +
                            " cc_mail," +
                            " mail_status," +
                            " mail_senddate, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + values.customer_gid + "'," +
                            "'" + employee_name + "'," +
                            "'" + lsto_mail + "'," +
                            "'" + cc_mailid + "'," +
                            "'Customer Approval'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }


                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;
                    }
                }

                else
                {
                    values.status = false;
                    values.message = "Error Occured While Updating Customer";
                    return false;
                }
            }
            else
            {
                values.status = false;
                values.message = "Approval Can't be done, the query is still open";

            }
            
            return true;
        }
        public bool DarejectedEditUpdate(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {
            msSQL = " select count(customer_gid) as openquery from fnd_trn_tcustomerraisequery where customer_gid = '" + values.customer_gid + "'" +
                               " and customerraisequery_status = 'Query Raised'";
            values.openquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.openquerycount == "0")
            {
                msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select customer_gid from fnd_mst_tcustomer2address where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }

            msSQL = "select customer_gid,customer_code,customer_name,pan_no,businessstart_date,year_business,month_business,cin_no,constitution_gid," +
                  "constitution_name,assessmentagency_gid,assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,rating_date,amlcategory_gid," +
                  "amlcategory_name,businesscategory_gid,businesscategory_name,contactperson_fn,contactperson_mn, contactperson_ln,msme_registration,remarks" +
                  " from fnd_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //lscustomer_code = objODBCDatareader["customer_code"].ToString();
                lscustomer_name = objODBCDatareader["customer_name"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                lsassessmentagency_gid = objODBCDatareader["assessmentagency_gid"].ToString();
                lsassessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                lsassessmentagencyrating_gid = objODBCDatareader["assessmentagencyrating_gid"].ToString();
                lsassessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                if (objODBCDatareader["rating_date"].ToString() == "")
                {
                }
                else
                {
                    lsrating_date = Convert.ToDateTime(objODBCDatareader["rating_date"]).ToString("dd-MM-yyyy");
                }
                lsamlcategory_gid = objODBCDatareader["amlcategory_gid"].ToString();
                lsamlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                lsbusinesscategory_gid = objODBCDatareader["businesscategory_gid"].ToString();
                lsbusinesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
                lsmsme_registration = objODBCDatareader["msme_registration"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();

            }
            objODBCDatareader.Close();
            msSQL = " update fnd_mst_tcustomer set " +
                    " customer_code='" + values.customer_code + "'," +
                    " customer_name='" + values.customer_name + "'," +
                     " pan_no='" + values.pan_no + "',";
            if (Convert.ToDateTime(values.editbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " businessstart_date='" + Convert.ToDateTime(values.editbusinessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " year_business='" + values.year_business + "'," +
                     " month_business='" + values.month_business + "'," +
                     "cin_no='" + values.cin_no + "'," +
                    "constitution_gid='" + values.constitution_gid + "'," +
                    "constitution_name='" + values.constitution_name + "'," +
                    "assessmentagency_gid='" + values.assessmentagency_gid + "'," +
                    "assessmentagency_name='" + values.assessmentagency_name + "'," +
                    "assessmentagencyrating_gid='" + values.assessmentagencyrating_gid + "'," +
                    "assessmentagencyrating_name='" + values.assessmentagencyrating_name + "',";
            //if ((values.rating_date == null) || (values.rating_date == ""))
            //{
            //    msSQL += "null,";
            //}
            //else
            //{
            //    msSQL += "rating_date='" + Convert.ToDateTime(values.rating_date).ToString("dd-MM-yyyy") + "',";
            //}
            if (Convert.ToDateTime(values.editrating_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " rating_date='" + Convert.ToDateTime(values.editrating_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += "amlcategory_gid='" + values.amlcategory_gid + "'," +
                    "amlcategory_name='" + values.amlcategory_name + "'," +
                    "businesscategory_gid='" + values.businesscategory_gid + "'," +
                    "businesscategory_name='" + values.businesscategory_name + "'," +
                    "contactperson_fn='" + values.contactperson_fn + "'," +
                    "contactperson_mn='" + values.contactperson_mn + "'," +
                    "contactperson_ln='" + values.contactperson_ln + "'," +
                    "msme_registration='" + values.msme_registration + "'," +
                    "remarks='" + values.remarks + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "update fnd_mst_tcustomer set status_remarks ='Rejected' where customer_gid='" + values.customer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("BULG");

                msSQL = "Insert into fnd_mst_tcustomerupdatelog(" +
               " customerupdatelog_gid, " +
               " customer_gid, " +
               " customer_code," +
               " customer_name," +
               " coi_date," +
               " businessstart_date," +
               " year_business," +
               " month_business," +
               " constitution_gid," +
               " constitution_name," +
               " cin_no," +
               " pan_no," +
               " contactperson_fn," +
               " contactperson_mn," +
               " contactperson_ln," +
               " updated_by," +
               " updated_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.customer_gid + "'," +
               "'" + lscustomer_code + "'," +
               "'" + lscustomer_name + "'," +
               "'" + lscoi_date + "'," +
               "'" + lsbusinessstart_date + "'," +
               "'" + lsyear_business + "'," +
                         "'" + lsmonth_business + "'," +
                         "'" + lsconstitution_gid + "'," +
                         "'" + lsconstitution_name + "'," +
                         "'" + lscin_no + "'," +
                         "'" + lspan_no + "'," +
                         "'" + lscontactperson_firstname + "'," +
                         "'" + lscontactperson_middlename + "'," +
                         "'" + lscontactperson_lastname + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates

                msSQL = "update fnd_mst_tcustomer2mobileno set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2emailaddress set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2address set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update fnd_mst_tfndmanagement2cheque set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update fnd_mst_tcustomer2bank set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update fnd_mst_tcustomer2gst set customer_gid ='" + values.customer_gid + "' where customer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "customer Details Updated Successfully";
                try
                {
                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select  group_concat(distinct a.created_by)  as To2members, status_remarks  from fnd_mst_tcustomer a" +
                        " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                        " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                    " where a.customer_gid ='" + values.customer_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsTo2members = objODBCDatareader["To2members"].ToString();
                        lsstatus_remarks = objODBCDatareader["status_remarks"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                            " where employee_gid = '" + employee_gid + "'";
                    string employee_name = objdbconn.GetExecuteScalar(msSQL);

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                    sub = "RE: Customer Approval ";
                    body = "Dear Team,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "This customer creation is rejected.  <br />";
                    body = body + "<br />";
                    body = body + "<b> Customer :</b> " + HttpUtility.HtmlEncode(lscustomer_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Status :</b> " + HttpUtility.HtmlEncode(lsstatus_remarks) + "<br />";
                    //body = body + "<br />";
                    //body = body + "<b>Checkpoint Group :</b> " + lscheckpointgroup_name + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to view more details.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(employee_name);
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    {
                        lsBCCReceipients = lsBccmail_id.Split(',');
                        if (lsBccmail_id.Length == 0)
                        {
                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                        }
                        else
                        {
                            foreach (string BCCEmail in lsBCCReceipients)
                            {
                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                            }
                        }
                    }
                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                    {
                        lsToReceipients = lsto_mail.Split(',');
                        if (lsto_mail.Length == 0)
                        {
                            message.To.Add(new MailAddress(lsto_mail));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                            }
                        }
                    }

                    //if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    //{
                    //    lsCCReceipients = cc_mailid.Split(',');
                    //    if (cc_mailid.Length == 0)
                    //    {
                    //        message.CC.Add(new MailAddress(cc_mailid));
                    //    }
                    //    else
                    //    {
                    //        foreach (string CCEmail in lsCCReceipients)
                    //        {
                    //            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                    //        }
                    //    }
                    //}

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
                    smtp.Send(message);

                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                        " customer_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.customer_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Customer Approved'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }


                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }
            }

            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Customer";
                return false;
            }
        }
             else
            {
                values.status = false;
                values.message = "Reject Can't be done, the query is still open";

            }
            return true;
        }
        public void DacustomerDetailsEdit(string customer_gid, MdlFndMstCustomerMasterAdd values)
        {
            try
            {

                msSQL = "select customer_gid,customer_code,customer_name,pan_no,businessstart_date,year_business,month_business,cin_no,status_remarks,msme_radio,constitution_gid,constitution_name,assessmentagency_gid,assessmentagency_name,assessmentagencyrating_gid,assessmentagencyrating_name,rating_date,amlcategory_gid,amlcategory_name,businesscategory_gid,businesscategory_name,contactperson_fn,designation_gid,designation_type,individualproof_gid,individualproof_name,contactperson_mn, contactperson_ln,msme_registration,remarks from fnd_mst_tcustomer where customer_gid='" + customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.customer_code = objODBCDatareader["customer_code"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    if (objODBCDatareader["businessstart_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.businessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                    }
                    values.year_business = objODBCDatareader["year_business"].ToString();
                    values.month_business = objODBCDatareader["month_business"].ToString();
                    values.cin_no = objODBCDatareader["cin_no"].ToString();
                    values.constitution_gid = objODBCDatareader["constitution_gid"].ToString();
                    values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    values.assessmentagency_gid = objODBCDatareader["assessmentagency_gid"].ToString();
                    values.assessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                    values.assessmentagencyrating_gid = objODBCDatareader["assessmentagencyrating_gid"].ToString();
                    values.assessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                    if (objODBCDatareader["rating_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.rating_date = Convert.ToDateTime(objODBCDatareader["rating_date"]).ToString("dd-MM-yyyy");
                    }
                    values.amlcategory_gid = objODBCDatareader["amlcategory_gid"].ToString();
                    values.amlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                    values.businesscategory_gid = objODBCDatareader["businesscategory_gid"].ToString();
                    values.businesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                    values.designation_gid = objODBCDatareader["designation_gid"].ToString();
                    values.designation_type = objODBCDatareader["designation_type"].ToString();
                    values.individualproof_gid = objODBCDatareader["individualproof_gid"].ToString();
                    values.individualproof_name = objODBCDatareader["individualproof_name"].ToString();
                    values.contactperson_fn = objODBCDatareader["contactperson_fn"].ToString();
                    values.contactperson_mn = objODBCDatareader["contactperson_mn"].ToString();
                    values.contactperson_ln = objODBCDatareader["contactperson_ln"].ToString();
                    values.msme_registration = objODBCDatareader["msme_registration"].ToString();
                    values.status_remarks = objODBCDatareader["status_remarks"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.msme_radio = objODBCDatareader["msme_radio"].ToString();

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

        public void DaChequeDetailsEdit(string customer_gid, MdlFndMstCustomerMasterAdd values)
        {
            try
            {
                msSQL = " select accountholder_name,account_number,bank_name,cheque_no," +
                    " ifsc_code, micr, branch_address,branch_name,city,district,state,mergedbankingentity_gid,mergedbankingentity_name,special_condition,general_remarks, cts_enabled, cheque_type,date_chequetype,date_chequepresentation,status_chequepresentation,date_chequeclearance,status_chequeclearance" +
                    " from fnd_mst_tfndmanagement2cheque where customer_gid='" + customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    values.account_number = objODBCDatareader["account_number"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.cheque_no = objODBCDatareader["cheque_no"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.micr = objODBCDatareader["micr"].ToString();
                    values.branch_address = objODBCDatareader["branch_address"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.mergedbankingentity_gid = objODBCDatareader["mergedbankingentity_gid"].ToString();
                    values.mergedbankingentity_name = objODBCDatareader["mergedbankingentity_name"].ToString();

                    values.special_condition = objODBCDatareader["special_condition"].ToString();
                    values.general_remarks = objODBCDatareader["general_remarks"].ToString();
                    values.cts_enabled = objODBCDatareader["cts_enabled"].ToString();
                    values.cheque_type = objODBCDatareader["cheque_type"].ToString();
                    values.date_chequetype = Convert.ToDateTime(objODBCDatareader["date_chequetype"]).ToString("MM-dd-yyyy");
                    values.date_chequepresentation = Convert.ToDateTime(objODBCDatareader["date_chequepresentation"]).ToString("MM-dd-yyyy");
                    values.status_chequepresentation = objODBCDatareader["status_chequepresentation"].ToString();
                    values.date_chequeclearance = Convert.ToDateTime(objODBCDatareader["date_chequeclearance"]).ToString("MM-dd-yyyy");
                    values.status_chequeclearance = objODBCDatareader["status_chequeclearance"].ToString();


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







            //msSQL = " select accountholder_name,account_number,bank_name,cheque_no," +
            //        " ifsc_code, micr, branch_address,branch_name,city,district,state" +
            //        " from fnd_mst_tfndmanagement2cheque where customer_gid='" + customer_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getcheque_list = new List<cheque_list>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dr_datarow in dt_datatable.Rows)
            //    {
            //        getcheque_list.Add(new cheque_list
            //        {
            //            accountholder_name = (dr_datarow["accountholder_name"].ToString()),
            //            account_number = (dr_datarow["account_number"].ToString()),
            //            bank_name = (dr_datarow["bank_name"].ToString()),
            //            cheque_no = (dr_datarow["cheque_no"].ToString()),
            //            ifsc_code = (dr_datarow["ifsc_code"].ToString()),
            //            micr = (dr_datarow["micr"].ToString()),
            //            branch_address = (dr_datarow["branch_address"].ToString()),
            //            city = (dr_datarow["city"].ToString()),
            //            district = (dr_datarow["district"].ToString()),
            //            state = (dr_datarow["state"].ToString()),
            //            customer_gid = (dr_datarow["customer_gid"].ToString()),
            //        });
            //    }
            //    values.cheque_list = getcheque_list;
            //}
            //dt_datatable.Dispose();
        }


        public void DaGSTUpdate(string employee_gid, MdlbuyerGST values)
        {
            msSQL = "select gststate_name, gst_no, gstregister_status, customer_gid, customer2gst_gid from fnd_mst_tcustomer2gst where customer2gst_gid='" + values.customer2gst_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsgststate_name = objODBCDatareader["gststate_name"].ToString();
                lsgst_no = objODBCDatareader["gst_no"].ToString();
                lscustomer2gst_gid = objODBCDatareader["customer2gst_gid"].ToString();
                lscustomer_gid = objODBCDatareader["customer_gid"].ToString();
                lsgstregister_status = objODBCDatareader["gstregister_status"].ToString();
                
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update fnd_mst_tcustomer2gst set " +
                         " gststate_name='" + values.gststate_name + "'," +
                         " gst_no='" + values.gst_no + "'," +
                         " gstregister_status='" + values.gstregister_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where customer2gst_gid='" + values.customer2gst_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    if (lscustomer_gid == values.customer_gid)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("BGST");

                        msSQL = "Insert into fnd_mst_tcustomer2gstupdatelog(" +
                       " customer2gstupdatelog_gid, " +
                       " customer2gst_gid, " +
                       " customer_gid, " +
                       " gststate_name," +
                       " gst_no," +
                       " gstregister_status," +
                       " created_by," +
                       " created_date)" +
                       " values (" +
                       "'" + msGetGid + "'," +
                       "'" + values.customer2gst_gid + "'," +
                       "'" + values.customer_gid + "'," +
                       "'" + lsgststate_name + "'," +
                       "'" + lsgst_no + "'," +
                       "'" + lsgstregister_status + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = true;
                    values.message = "Customer GST Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaGSTEdit(string customer2gst_gid, MdlcustomerGST values)
        {
            try
            {
                msSQL = "select gststate_name, gst_no, customer_gid, customer2gst_gid, gstregister_status from fnd_mst_tcustomer2gst where customer2gst_gid='" + customer2gst_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.gststate_name = objODBCDatareader["gststate_name"].ToString();
                    values.gst_no = objODBCDatareader["gst_no"].ToString();
                    values.customer2gst_gid = objODBCDatareader["customer2gst_gid"].ToString();
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    values.gstregister_status = objODBCDatareader["gstregister_status"].ToString();
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


        public void DaMobileNoEdit(string customer2mobileno_gid, Mdlmobile_no values)
        {
            try
            {
                msSQL = "select mobile_no,primary_mobileno,whatsapp_mobileno, customer2mobileno_gid from fnd_mst_tcustomer2mobileno " +
                " where customer2mobileno_gid='" + customer2mobileno_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.primary_mobileno = objODBCDatareader["primary_mobileno"].ToString();
                    values.whatsapp_mobileno = objODBCDatareader["whatsapp_mobileno"].ToString();
                    values.customer2mobileno_gid = objODBCDatareader["customer2mobileno_gid"].ToString();
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

        public void DaAddressDetailEdit(string customer2address_gid, MdlMstaddresstype values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_address, postal_code, city," +
                    " district, state_name, country, latitude, longitude, customer_gid, customer2address_gid " +
                    " from fnd_mst_tcustomer2address where customer2address_gid='" + customer2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.address_typegid = objODBCDatareader["addresstype_gid"].ToString();
                    values.address_type = objODBCDatareader["addresstype_name"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.primary_address = objODBCDatareader["primary_address"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state_name"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    values.customer2address_gid = objODBCDatareader["customer2address_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaAddressDetailUpdate(string employee_gid, MdlMstaddresstype values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_address, postal_code, city," +
                    " district, state_name, country, latitude, longitude, customer_gid, customer2address_gid " +
                    " from fnd_mst_tcustomer2address where customer2address_gid='" + values.customer2address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
              
                   lsaddress_typegid = objODBCDatareader["addresstype_gid"].ToString();
                lsaddress_type = objODBCDatareader["addresstype_name"].ToString();
                lsaddressline1 = objODBCDatareader["addressline1"].ToString();
                lsaddressline2 = objODBCDatareader["addressline2"].ToString();
                lslandmark = objODBCDatareader["landmark"].ToString();
                lstaluka = objODBCDatareader["taluka"].ToString();
                lsprimary_address = objODBCDatareader["primary_address"].ToString();
                lspostal_code = objODBCDatareader["postal_code"].ToString();
                lscity = objODBCDatareader["city"].ToString();
                lsdistrict = objODBCDatareader["district"].ToString();
                lsstate = objODBCDatareader["state_name"].ToString();
                lscountry = objODBCDatareader["country"].ToString();
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
                lscustomer_gid = objODBCDatareader["customer_gid"].ToString();
                lscustomer2address_gid = objODBCDatareader["customer2address_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update fnd_mst_tcustomer2address set " +
                         " addresstype_gid='" + values.address_typegid + "'," +
                         " addresstype_name='" + values.address_type + "'," +
                         " addressline1='" + values.addressline1 + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_address='" + values.primary_address + "'," +
                         " postal_code='" + values.postal_code + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state_name='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " latitude='" + values.latitude + "'," +
                         " longitude='" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where customer2address_gid='" + values.customer2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    if (lscustomer_gid == values.customer_gid)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("ADUL");

                        msSQL = " insert into fnd_mst_tcustomer2addressupdatelog(" +
                      " customer2addressupdatelog_gid," +
                      " customer2address_gid," +
                      " customer_gid," +
                      " addresstype_gid," +
                      " addresstype_name," +
                      " addressline1," +
                      " addressline2," +
                      " primary_address," +
                      " landmark," +
                      " postal_code," +
                      " city," +
                      " taluka," +
                      " district," +
                      " state_name," +
                      " country," +
                      " latitude," +
                      " longitude," +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGetGid + "'," +
                      "'" + values.customer2address_gid + "'," +
                      "'" + values.customer_gid + "'," +
                      "'" + values.address_typegid + "'," +
                      "'" + values.address_type + "'," +
                      "'" + values.addressline1 + "'," +
                      "'" + values.addressline2 + "'," +
                      "'" + values.primary_address + "'," +
                      "'" + values.landmark + "'," +
                      "'" + values.postal_code + "'," +
                      "'" + values.city + "'," +
                      "'" + values.taluka + "'," +
                      "'" + values.district + "'," +
                      "'" + values.state + "'," +
                      "'" + values.country + "'," +
                      "'" + values.latitude + "'," +
                      "'" + values.longitude + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = true;
                    values.message = "Address Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaMobileNoUpdate(string employee_gid, Mdlmobile_no values)
        {
            msSQL = "select mobile_no,primary_mobileno,whatsapp_mobileno, customer2mobileno_gid, customer_gid from fnd_mst_tcustomer2mobileno " +
                 " where customer2mobileno_gid='" + values.customer2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_mobileno = objODBCDatareader["primary_mobileno"].ToString();
                lswhatsapp_mobileno = objODBCDatareader["whatsapp_mobileno"].ToString();
                lscustomer2mobileno_gid = objODBCDatareader["customer2mobileno_gid"].ToString();
                lscustomer_gid = objODBCDatareader["customer_gid"].ToString();
            }
            
            objODBCDatareader.Close();
            try
            {
                msSQL = " update fnd_mst_tcustomer2mobileno set " +
                         " mobile_no='" + values.mobile_no + "'," +
                         " primary_mobileno='" + values.primary_mobileno + "'," +
                         " whatsapp_mobileno='" + values.whatsapp_mobileno + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where customer2mobileno_gid='" + values.customer2mobileno_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    if (lscustomer_gid == values.customer_gid)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("BMNU");

                        msSQL = "Insert into fnd_mst_tcustomer2mobilenoupdatelog(" +
                       " customer2mobilenoupdatelog_gid, " +
                       " customer2mobileno_gid, " +
                       " customer_gid, " +
                       " mobile_no," +
                       " primary_mobileno," +
                       " whatsapp_mobileno," +
                       " created_by," +
                       " created_date)" +
                       " values (" +
                       "'" + msGetGid + "'," +
                       "'" + values.customer2mobileno_gid + "'," +
                       "'" + values.customer_gid + "'," +
                       "'" + lsmobile_no + "'," +
                       "'" + lsprimary_mobileno + "'," +
                       "'" + lswhatsapp_mobileno + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = true;
                    values.message = "Mobile Number Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaEmailAddressEdit(string customer2emailaddress_gid, MdlEmail_address values)
        {
            try
            {
                msSQL = "select email_address, primary_emailaddress, customer2emailaddress_gid, customer_gid from fnd_mst_tcustomer2emailaddress " +
                " where customer2emailaddress_gid='" + customer2emailaddress_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.primary_emailaddress = objODBCDatareader["primary_emailaddress"].ToString();
                    values.customer2emailaddress_gid = objODBCDatareader["customer2emailaddress_gid"].ToString();
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
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

        public void DaEmailAddressUpdate(string employee_gid, MdlEmail_address values)
        {
            msSQL = "select email_address, primary_emailaddress, customer2emailaddress_gid, customer_gid from fnd_mst_tcustomer2emailaddress" +
                 " where customer2emailaddress_gid='" + values.customer2emailaddress_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_emailaddress = objODBCDatareader["primary_emailaddress"].ToString();
                lscustomer2emailaddress_gid = objODBCDatareader["customer2emailaddress_gid"].ToString();
                lscustomer_gid = objODBCDatareader["customer_gid"].ToString();
                
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update fnd_mst_tcustomer2emailaddress set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_emailaddress='" + values.primary_emailaddress + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where customer2emailaddress_gid='" + values.customer2emailaddress_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    if (lscustomer_gid == values.customer_gid)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("BEAU");

                        msSQL = "Insert into fnd_mst_tcustomer2emailaddressupdatelog(" +
                       " customer2emailaddressupdatelog_gid, " +
                       " customer2emailaddress_gid, " +
                       " customer_gid, " +
                       " email_address," +
                       " primary_emailaddress," +
                       " created_by," +
                       " created_date)" +
                       " values (" +
                       "'" + msGetGid + "'," +
                       "'" + values.customer2emailaddress_gid + "'," +
                       "'" + values.customer_gid + "'," +
                       "'" + lsemail_address + "'," +
                       "'" + lsprimary_emailaddress + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = true;
                    values.message = "Email Address Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaGetChequeSummaryView(string customer_gid, MdlFndMstCustomerMasterAdd values)
        {
            msSQL = " select customer_gid,fndmanagement2cheque_gid,accountholder_name,account_number,bank_name,cheque_no," +
                    " ifsc_code, micr, branch_address,branch_name,city,district,state,mergedbankingentity_gid,mergedbankingentity_name,special_condition,general_remarks, cts_enabled, cheque_type,date_chequetype,date_chequepresentation,status_chequepresentation,date_chequeclearance,status_chequeclearance" +
                    " from fnd_mst_tfndmanagement2cheque where customer_gid='" + customer_gid + "'";
            //msSQL = " select fndmanagement2cheque_gid,mergedbankingentity_name,cheque_type,cts_enabled,account_number, cheque_no" +
            //        " from fnd_mst_tfndmanagement2cheque" +
            //        " where customer_gid='" + employee_gid + "' group by created_date";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getchequeList = new List<cheque_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getchequeList.Add(new cheque_list
                    {
                        fndmanagement2cheque_gid = dt["fndmanagement2cheque_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        cheque_no = dt["cheque_no"].ToString(),
                        mergedbankingentity_name = dt["mergedbankingentity_name"].ToString(),
                        cheque_type = dt["cheque_type"].ToString(),
                        cts_enabled = dt["cts_enabled"].ToString(),
                        account_number = dt["account_number"].ToString(),
                        accountholder_name = dt["accountholder_name"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        micr = dt["micr"].ToString(),

                        branch_address = dt["branch_address"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        city = dt["city"].ToString(),
                        district = dt["district"].ToString(),
                        state = dt["state"].ToString(),
                        special_condition = dt["special_condition"].ToString(),
                        general_remarks = dt["general_remarks"].ToString(),
                        date_chequetype = dt["date_chequetype"].ToString(),
                        date_chequepresentation = dt["date_chequepresentation"].ToString(),
                        status_chequepresentation = dt["status_chequepresentation"].ToString(),
                        date_chequeclearance = dt["date_chequeclearance"].ToString(),
                        status_chequeclearance = dt["status_chequeclearance"].ToString(),
         
                    });

                }
            }
            values.cheque_list = getchequeList;
            dt_datatable.Dispose();
        }
        public void DaGetChequeView(string fndmanagement2cheque_gid, MdlFndMstCustomerMasterAdd values)
        {
            try
            {
                msSQL = " select accountholder_name,account_number,bank_name,cheque_no," +
                    " ifsc_code, micr, branch_address,branch_name,city,district,state,mergedbankingentity_gid,mergedbankingentity_name,special_condition,general_remarks, cts_enabled, cheque_type,date_chequetype,date_chequepresentation,status_chequepresentation,date_chequeclearance,status_chequeclearance" +
                    " from fnd_mst_tfndmanagement2cheque where fndmanagement2cheque_gid='" + fndmanagement2cheque_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    values.account_number = objODBCDatareader["account_number"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.cheque_no = objODBCDatareader["cheque_no"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.micr = objODBCDatareader["micr"].ToString();
                    values.branch_address = objODBCDatareader["branch_address"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.mergedbankingentity_gid = objODBCDatareader["mergedbankingentity_gid"].ToString();
                    values.mergedbankingentity_name = objODBCDatareader["mergedbankingentity_name"].ToString();

                    values.special_condition = objODBCDatareader["special_condition"].ToString();
                    values.general_remarks = objODBCDatareader["general_remarks"].ToString();
                    values.cts_enabled = objODBCDatareader["cts_enabled"].ToString();
                    values.cheque_type = objODBCDatareader["cheque_type"].ToString();
                    values.date_chequetype = Convert.ToDateTime(objODBCDatareader["date_chequetype"]).ToString("MM-dd-yyyy");
                    values.date_chequepresentation = Convert.ToDateTime(objODBCDatareader["date_chequepresentation"]).ToString("MM-dd-yyyy");
                    values.status_chequepresentation = objODBCDatareader["status_chequepresentation"].ToString();
                    values.date_chequeclearance = Convert.ToDateTime(objODBCDatareader["date_chequeclearance"]).ToString("MM-dd-yyyy");
                    values.status_chequeclearance = objODBCDatareader["status_chequeclearance"].ToString();


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


        public void DaGetChequeDetails(string employee_gid, MdlCheque values)
        {

            msSQL = " select accountholder_name,account_number,bank_name,cheque_no," +
                   " ifsc_code, micr, branch_address,branch_name,city,district,state,mergedbankingentity_gid,mergedbankingentity_name,special_condition,general_remarks, cts_enabled, cheque_type,date_chequetype,date_chequepresentation,status_chequepresentation,date_chequeclearance,status_chequeclearance" +
                   " from fnd_mst_tfndmanagement2cheque where customer_gid='" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getchequeList = new List<cheque_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getchequeList.Add(new cheque_list
                    {
                        fndmanagement2cheque_gid = dt["fndmanagement2cheque_gid"].ToString(),
                        cheque_no = dt["cheque_no"].ToString(),
                        mergedbankingentity_name = dt["mergedbankingentity_name"].ToString(),
                        cheque_type = dt["cheque_type"].ToString(),
                        cts_enabled = dt["cts_enabled"].ToString(),
                        account_number = dt["account_number"].ToString(),

                    });

                }
            }
            values.cheque_list = getchequeList;
            dt_datatable.Dispose();
        }
        public void DacustomerGSTList(string customer_gid, string employee_gid, MdlFndMstCustomerMasterAdd values)
        {
            msSQL = "select customer2gst_gid,gststate_name,gst_no, gstregister_status from fnd_mst_tcustomer2gst where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomergst_list = new List<customergst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomergst_list.Add(new customergst_list
                    {
                        customer2gst_gid = (dr_datarow["customer2gst_gid"].ToString()),
                        gststate_name = (dr_datarow["gststate_name"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gstregister_status = (dr_datarow["gstregister_status"].ToString())
                    });
                }
                values.customergst_list = getcustomergst_list;
            }
            dt_datatable.Dispose();
        }




        public void DaGetCustomerDetailscount(MdlFndMstCustomerMasterAdd values, string Employee_gid)
        {
            msSQL = " select count(customer_gid) as customerpending_count from fnd_mst_tcustomer where  status_remarks='Pending' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customerpending_count = objODBCDatareader["customerpending_count"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = " select count(customer_gid) as customerapprover_count from fnd_mst_tcustomer where  status_remarks='Approved' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.customerapprover_count = objODBCDatareader["customerapprover_count"].ToString();

            }

            objODBCDatareader.Close();
            msSQL = " select count(customer_gid) as customerreject_count from fnd_mst_tcustomer where  status_remarks='Rejected' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.customerreject_count = objODBCDatareader["customerreject_count"].ToString();

            }

            objODBCDatareader.Close();
          
        }

        public void DacustomerMobileNoList(string customer_gid, string employee_gid, Mdlmobile_no values)
        {
            msSQL = "select mobile_no,primary_mobileno,whatsapp_mobileno, customer2mobileno_gid from fnd_mst_tcustomer2mobileno " +
                " where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmobileno_list = new List<mobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmobileno_list.Add(new mobileno_list
                    {
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                        customer2mobileno_gid = (dr_datarow["customer2mobileno_gid"].ToString()),
                    });
                }
                values.mobileno_list = getmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DacustomerEmailAddressList(string customer_gid, string employee_gid, MdlEmail_address values)
        {
            msSQL = "select email_address, primary_emailaddress, customer2emailaddress_gid, customer_gid from fnd_mst_tcustomer2emailaddress" +
                  " where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemail_list = new List<email_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getemail_list.Add(new email_list
                    {
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString()),
                        customer2emailaddress_gid = (dr_datarow["customer2emailaddress_gid"].ToString()),
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                    });
                }
                values.email_list = getemail_list;
            }
            dt_datatable.Dispose();
        }


        public void DacustomerAddressList(string customer_gid, string employee_gid, MdlcustomerAddress values)
        {
            msSQL = "  select customer2address_gid,addresstype_name,primary_address, addressline1, addressline2, taluka, district, state_name, country, latitude, longitude, city,landmark," +
                    " postal_code from fnd_mst_tcustomer2address where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomeraddress_list = new List<customeraddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomeraddress_list.Add(new customeraddress_list
                    {
                        customer2address_gid = (dr_datarow["customer2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_address = (dr_datarow["primary_address"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.customeraddress_list = getcustomeraddress_list;
            }
            dt_datatable.Dispose();
        }


        public void DaUpdatecustomer(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {


            msSQL = " update fnd_mst_tcustomer set " +
                 " customer_name='" + values.customer_name.Replace("'", "") + "'," +

                 " customer_code='" + values.customer_code + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +
                 " remarks='" + values.remarks + "'," +

                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("FDLO");

                msSQL = " insert into fnd_mst_tcustomerlog (" +
                       " customerlog_gid, " +
                       " customer_gid, " +
                       " customer_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.customer_gid + "'," +
                       " '" + values.customer_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Customer Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaGetcustomerSummary(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {
            msSQL = " select a.customer_gid,a.customer_code,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.customer_name,a.status_remarks,a.pan_no,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.remarks,case when a.status='N' then 'Inactive' else 'Active' end as status " +
                        " FROM fnd_mst_tcustomer a" +                     
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where (a.status_remarks = 'Pending' or a.status_remarks = 'N ') and a.created_by='" + employee_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer_list = new List<customer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer_list.Add(new customer_list
                    {
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                        customer_code = (dr_datarow["customer_code"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),                   
                        remarks = (dr_datarow["remarks"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        status_remarks = (dr_datarow["status_remarks"].ToString()),                        
                        status = (dr_datarow["Status"].ToString()),

                    });
                }
                values.customer_list = getcustomer_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetPendingCustomer(MdlFndMstCustomerMasterAdd values, string employee_gid)
        {
            try
            {


                msSQL = " SELECT customer_gid,customer_name FROM fnd_mst_tcustomer where status_remarks = 'Pending' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomerpending_list = new List<customerpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomerpending_list.Add(new customerpending_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customer_name = (dr_datarow["customer_name"].ToString()),
                        });
                    }
                    values.customerpending_list = getcustomerpending_list;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetcustomerApprovalSummary(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {
            msSQL = " select a.customer_gid,a.customer_code,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.customer_name,d.approver_gid,a.pan_no,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.remarks,case when a.status='N' then 'Inactive' else 'Active' end as status " +
                        " FROM fnd_mst_tcustomer a" +
                        " left join fnd_mst_tcustomerapproving d on a.customer_gid = d.customer_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         "  where a.status_remarks = 'Pending' and d.approver_gid='" + employee_gid + "' order by a.created_by desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer_list = new List<customer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer_list.Add(new customer_list
                    {
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                        customer_code = (dr_datarow["customer_code"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        status = (dr_datarow["status"].ToString()),

                    });
                }
                values.customer_list = getcustomer_list;
            }
            dt_datatable.Dispose();
        }


        public void DaEditcustomer(string customer_gid, MdlFndMstCustomerMasterAdd values)
        {
            try
            {
                msSQL = " SELECT customer_gid,customer_name,customer_code,lms_code, bureau_code,remarks, status as Status FROM fnd_mst_tcustomer where customer_gid='" + customer_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();

                    values.customer_code = objODBCDatareader["customer_code"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.Status = objODBCDatareader["Status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }
        public void DaInactivecustomer(MdlFndMstCustomerMasterAdd values, string employee_gid)
        {
            msSQL = " update fnd_mst_tcustomer set status='" + values.rbo_status + "'" +
                    " where customer_gid='" + values.customer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("FDIL");

                msSQL = " insert into fnd_mst_tcustomerinactivelog (" +
                      " customerinactivelog_gid, " +
                      " customer_gid," +
                      " customer_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.customer_gid + "'," +
                      " '" + values.customer_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Customer Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Customer Type Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }
        public void DacustomerInactiveLogview(string customer_gid, MdlFndMstCustomerMasterAdd values)
        {
            try
            {
                msSQL = " SELECT a.customer_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM fnd_mst_tcustomerinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.customer_gid ='" + customer_gid + "' order by a.customerinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomerstatus_list = new List<customerstatus_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomerstatus_list.Add(new customerstatus_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.customerstatus_list = getcustomerstatus_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }



    


    public void DaGetcustomerapprover(string employee_gid, MdlFndMstCustomerMasterAdd values)
    {
        msSQL = " select a.customer_gid,a.customer_code,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,d.approver_gid,a.customer_name,a.pan_no,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.remarks,case when a.status='N' then 'Inactive' else 'Active' end as status " +
                    " FROM fnd_mst_tcustomer a" +
                    " left join fnd_mst_tcustomerapproving d on a.customer_gid = d.customer_gid" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where a.status_remarks = 'Approved' and d.approver_gid='" + employee_gid + "' order by a.customer_gid desc ";
        //" select customer_gid,customer_code,customer_name,pan_no,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date,remarks from fnd_mst_tcustomer where status = 'Approved' order by customer_gid desc";

        dt_datatable = objdbconn.GetDataTable(msSQL);
        var getcustomerapprover_list = new List<customerapprover_list>();
        if (dt_datatable.Rows.Count != 0)
        {
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                getcustomerapprover_list.Add(new customerapprover_list
                {
                    customer_gid = (dr_datarow["customer_gid"].ToString()),
                    customer_code = (dr_datarow["customer_code"].ToString()),
                    customer_name = (dr_datarow["customer_name"].ToString()),
                    created_date = (dr_datarow["created_date"].ToString()),
                    created_by = (dr_datarow["created_by"].ToString()),
                    remarks = (dr_datarow["remarks"].ToString()),
                    pan_no = (dr_datarow["pan_no"].ToString()),
                    status = (dr_datarow["status"].ToString()),

                });
            }
            values.customerapprover_list = getcustomerapprover_list;
        }
        dt_datatable.Dispose();
    }

        public void DaGetcustomerapproverview(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {
            msSQL = " select a.customer_gid,a.customer_code,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.customer_name,a.pan_no,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.remarks,case when a.status='N' then 'Inactive' else 'Active' end as status " +
                        " FROM fnd_mst_tcustomer a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.status_remarks = 'Approved' and a.created_by='" + employee_gid + "' order by a.customer_gid desc ";
            //" select customer_gid,customer_code,customer_name,pan_no,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date,remarks from fnd_mst_tcustomer where status = 'Approved' order by customer_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomerapprover_list = new List<customerapprover_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomerapprover_list.Add(new customerapprover_list
                    {
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                        customer_code = (dr_datarow["customer_code"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        status = (dr_datarow["status"].ToString()),

                    });
                }
                values.customerapprover_list = getcustomerapprover_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetcustomerreject(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {
           
            msSQL = " select a.customer_gid,a.customer_code,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.customer_name,a.pan_no,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.remarks,case when a.status='N' then 'Inactive' else 'Active' end as status " +
                        " FROM fnd_mst_tcustomer a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " where a.status_remarks = 'Rejected' and a.created_by='" + employee_gid + "' order by a.customer_gid desc ";

           // " select customer_gid,customer_code,customer_name,pan_no,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date,remarks from fnd_mst_tcustomer where status = 'Rejected' order by customer_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomerrejected_list = new List<customerrejected_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomerrejected_list.Add(new customerrejected_list
                    {
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                        customer_code = (dr_datarow["customer_code"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        status = (dr_datarow["status"].ToString()),

                    });
                }
                values.customerrejected_list = getcustomerrejected_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetcustomerapprovalreject(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {

            msSQL = " select a.customer_gid,a.customer_code,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.customer_name,a.pan_no,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.remarks,case when a.status='N' then 'Inactive' else 'Active' end as status " +
                        " FROM fnd_mst_tcustomer a" +
                        " left join fnd_mst_tcustomerapproving d on a.customer_gid = d.customer_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " where a.status_remarks = 'Rejected' and d.approver_gid='" + employee_gid + "' order by a.customer_gid desc ";

            // " select customer_gid,customer_code,customer_name,pan_no,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date,remarks from fnd_mst_tcustomer where status = 'Rejected' order by customer_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomerrejected_list = new List<customerrejected_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomerrejected_list.Add(new customerrejected_list
                    {
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                        customer_code = (dr_datarow["customer_code"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        status = (dr_datarow["status"].ToString()),

                    });
                }
                values.customerrejected_list = getcustomerrejected_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetCustomerCounts(MdlTrnCampaign values, string Employee_gid)
        {
            msSQL = " select count(a.customer_gid) as customerpending_count from fnd_mst_tcustomer a" +
                " left join fnd_mst_tcustomerapproving b on b.customer_gid = a.customer_gid " +
             " where a.status_remarks = 'Pending' and b.approver_gid='" + Employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customerpending_count = objODBCDatareader["customerpending_count"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = "select count(a.customer_gid) as customerrejected_count from fnd_mst_tcustomer a " +
                 " left join fnd_mst_tcustomerapproving b on b.customer_gid = a.customer_gid " +
                     " where a.status_remarks = 'Rejected' and b.approver_gid='" + Employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.customerrejected_count = objODBCDatareader["customerrejected_count"].ToString();

            }

            objODBCDatareader.Close();

            msSQL = "select count(a.customer_gid) as customerapproved_count from fnd_mst_tcustomer a " +
                    " left join fnd_mst_tcustomerapproving b on b.customer_gid = a.customer_gid " +
                    " where a.status_remarks = 'Approved' and b.approver_gid='" + Employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.customerapproved_count = objODBCDatareader["customerapproved_count"].ToString();

            }

            objODBCDatareader.Close();
           
        }

        public void DaGetCustomerViewCounts(MdlTrnCampaign values, string Employee_gid)
        {
            msSQL = " select count(a.customer_gid) as customerpendingview_count from fnd_mst_tcustomer a" +
             " where (a.status_remarks = 'Pending' or a.status_remarks = 'N ') and a.created_by='" + Employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customerpendingview_count = objODBCDatareader["customerpendingview_count"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = "select count(a.customer_gid) as customerrejectedview_count from fnd_mst_tcustomer a " +
                     " where a.status_remarks = 'Rejected' and a.created_by='" + Employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.customerrejectedview_count = objODBCDatareader["customerrejectedview_count"].ToString();

            }

            objODBCDatareader.Close();

            msSQL = "select count(a.customer_gid) as customerapprovedview_count from fnd_mst_tcustomer a " +
                    " where a.status_remarks = 'Approved' and a.created_by='" + Employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.customerapprovedview_count = objODBCDatareader["customerapprovedview_count"].ToString();

            }

            objODBCDatareader.Close();

        }
        public void DaDeletecustomer(string customer_gid, MdlFndMstCustomerMasterAdd values)
        {
            msSQL = "delete from fnd_mst_tcustomer where customer_gid='" + customer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.message = "customer Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting customer Details";
                values.status = false;

            }
        }

        public bool DaPostMobileNo(string employee_gid, MdlcustomerMobileNo values)
        {

            msSQL = "select primary_mobileno " + " from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' and primary_mobileno='Yes'";
            string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msSQL = "select mobile_no from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "' and mobile_no ='" + values.mobile_no + "'";
            string mobile_no = objdbconn.GetExecuteScalar(msSQL);
            if (mobile_no == (values.mobile_no))
            {
                values.status = false;
                values.message = "Already This Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("B2MN");
            msSQL = " insert into fnd_mst_tcustomer2mobileno(" +
                    " customer2mobileno_gid," +
                    " customer_gid," +
                    " mobile_no," +
                    " primary_mobileno," +
                    " whatsapp_mobileno," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_mobileno + "'," +
                    "'" + values.whatsapp_mobileno + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Mobile Number Added Successfully";
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Mobile Number";
                objdbconn.CloseConn();
                return false;
            }
        }

        public void DaGetMobileNoList(string employee_gid, MdlcustomerMobileNo values)
        {
            msSQL = "select mobile_no,customer2mobileno_gid,primary_mobileno,whatsapp_mobileno from fnd_mst_tcustomer2mobileno where " +
              " customer_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomermobileno_list = new List<customermobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomermobileno_list.Add(new customermobileno_list
                    {
                        customer2mobileno_gid = (dr_datarow["customer2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                    });
                }
                values.customermobileno_list = getcustomermobileno_list;
            }
            dt_datatable.Dispose();
        }
        public void DaMobileNoTempList(string customer_gid, string employee_gid, MdlcustomerMobileNo values)
        {
            msSQL = "select mobile_no,customer2mobileno_gid,primary_mobileno,whatsapp_mobileno from fnd_mst_tcustomer2mobileno where " +
              " customer_gid = '" + employee_gid + "' or customer_gid = '" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomermobileno_list = new List<customermobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomermobileno_list.Add(new customermobileno_list
                    {
                        customer2mobileno_gid = (dr_datarow["customer2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                    });
                }
                values.customermobileno_list = getcustomermobileno_list;
                dt_datatable.Dispose();
            }
        }
        public void DaDeleteMobileNo(string customer2mobileno_gid, MdlcustomerMobileNo values)
        {
            msSQL = "delete from fnd_mst_tcustomer2mobileno where customer2mobileno_gid='" + customer2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Mobile Number";
                values.status = false;

            }
        }

        public bool DaPostEmailAddress(string employee_gid, MdlcustomerEmailAddress values)
        {
            msSQL = "select primary_emailaddress " + " from fnd_mst_tcustomer2emailaddress where customer_gid='" + employee_gid + "' and primary_emailaddress='Yes'";
            string lsprimary_emailaddress = objdbconn.GetExecuteScalar(msSQL);

            if (lsprimary_emailaddress == (values.primary_emailaddress))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                objdbconn.CloseConn();
                return false;
            }
            msSQL = "select email_address from fnd_mst_tcustomer2emailaddress where customer_gid='" + employee_gid + "' or customer_gid='" + values.customer_gid + "' and email_address='" + values.email_address + "'";
            string lsemail_address = objdbconn.GetExecuteScalar(msSQL);
            if (lsemail_address == (values.email_address))
            {
                values.status = false;
                values.message = "Already This Email Address Added";
                objdbconn.CloseConn();
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("B2EA");
            msSQL = " insert into fnd_mst_tcustomer2emailaddress(" +
                    " customer2emailaddress_gid," +
                    " customer_gid," +
                    " email_address," +
                    " primary_emailaddress," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_emailaddress + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Email Address";
                return false;
            }
        }

        public void DaGetEmailAddressList(string employee_gid, MdlcustomerEmailAddress values)
        {
            msSQL = "select email_address,customer2emailaddress_gid,primary_emailaddress from fnd_mst_tcustomer2emailaddress where " +
              " customer_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomeremailaddress_list = new List<customeremailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomeremailaddress_list.Add(new customeremailaddress_list
                    {
                        customer2emailaddress_gid = (dr_datarow["customer2emailaddress_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString())
                    });
                }
                values.customeremailaddress_list = getcustomeremailaddress_list;
            }
            dt_datatable.Dispose();
        }


        public void DaEmailTempList(string customer_gid, string employee_gid, MdlcustomerEmailAddress values)
        {
            msSQL = "select email_address,customer2emailaddress_gid,primary_emailaddress from fnd_mst_tcustomer2emailaddress where " +
              " customer_gid = '" + employee_gid + "' or customer_gid = '" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomeremailaddress_list = new List<customeremailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomeremailaddress_list.Add(new customeremailaddress_list
                    {
                        customer2emailaddress_gid = (dr_datarow["customer2emailaddress_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString())
                    });
                }
                values.customeremailaddress_list = getcustomeremailaddress_list;
                dt_datatable.Dispose();
            }
        }



        public void DaDeleteEmailAddress(string customer2emailaddress_gid, MdlcustomerEmailAddress values)
        {
            msSQL = "delete from fnd_mst_tcustomer2emailaddress where customer2emailaddress_gid='" + customer2emailaddress_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Email Address deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While deleting the email address";
                values.status = false;

            }
        }

        public bool DaPostAddress(string employee_gid, MdlcustomerAddress values)
        {
            msSQL = "select primary_address from fnd_mst_tcustomer2address where primary_address='Yes' and customer_gid='" + employee_gid + "'";
            string lsprimary_address = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_address == (values.primary_address))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("B2AD");
            msSQL = " insert into fnd_mst_tcustomer2address(" +
                    " customer2address_gid," +
                    " customer_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " addressline1," +
                    " addressline2," +
                    " primary_address," +
                    " landmark," +
                    " postal_code," +
                    " city," +
                    " taluka," +
                    " district," +
                    " state_name," +
                    " country," +
                    " latitude," +
                    " longitude," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.addresstype_gid + "'," +
                    "'" + values.addresstype_name + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.primary_address + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state_name + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Address Details Added Sucessfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Address";
                return false;
            }

        }

        public void DaGetAddressList(string employee_gid, MdlcustomerAddress values)
        {
            msSQL = " select customer2address_gid,addresstype_name,primary_address, addressline1, addressline2, taluka, district, state_name, country, latitude, longitude," +
                    " postal_code from fnd_mst_tcustomer2address where customer_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomeraddress_list = new List<customeraddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomeraddress_list.Add(new customeraddress_list
                    {
                        customer2address_gid = (dr_datarow["customer2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_address = (dr_datarow["primary_address"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())

                    });
                }
                values.customeraddress_list = getcustomeraddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteAddress(string customer2address_gid, MdlcustomerAddress values)
        {
            msSQL = "delete from fnd_mst_tcustomer2address where customer2address_gid='" + customer2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Address";
                values.status = false;

            }
        }
        public void DaGetDropDownUdc(string employee_gid, MdlDropDownUdc values)
        {
            //Bank Merging Entity
            msSQL = " SELECT a.bankname_gid,a.bankname_name" +
                    " FROM ocs_mst_tbankname a  where status='Y' order by a.bankname_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSegment = new List<bankname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getSegment.Add(new bankname_list
                    {
                        bankname_gid = (dr_datarow["bankname_gid"].ToString()),
                        bankname_name = (dr_datarow["bankname_name"].ToString()),
                    });
                }
                values.bankname_list = getSegment;
            }
            dt_datatable.Dispose();

            values.status = true;
        }


        public void DaPostChequeDetail(string employee_gid, MdlFndMstCustomerMasterAdd values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("UM2C");

            msSQL = "select cheque2document_gid,fndmanagement2cheque_gid,document_gid,document_name,document_path,created_by,created_date,updated_by,updated_date " + 
                    " from fnd_mst_tcheque2document where fndmanagement2cheque_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //msSQL = "select * " + " from fnd_mst_tcheque2document where fndmanagement2cheque_gid='" + customer_gid + "'";

            //if (objODBCDatareader.HasRows == false)
            //{
            //    values.status = false;
            //    values.message = "Kindly Add Cheque Document ";
            //    return;
            //}
            //msSQL = "select * " + " from fnd_mst_tcheque2document where fndmanagement2cheque_gid='" + employee_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            msSQL = " insert into fnd_mst_tfndmanagement2cheque(" +
                   " fndmanagement2cheque_gid ," +
                   " customer_gid ," +
                   " accountholder_name," +
                   " account_number," +
                   " bank_name," +
                   " cheque_no," +
                   " ifsc_code," +
                   " micr," +
                   " branch_address," +
                   " branch_name," +
                   " city," +
                   " district," +
                   " state," +
                    " mergedbankingentity_gid," +
                   " mergedbankingentity_name," +
                   " special_condition," +
                   " general_remarks," +
                   " cts_enabled," +
                   " cheque_type," +
                   " date_chequetype," +
                   " date_chequepresentation," +
                   " status_chequepresentation," +
                   " date_chequeclearance," +
                   " status_chequeclearance," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + values.accountholder_name + "'," +
                   "'" + values.account_number + "'," +
                   "'" + values.bank_name + "'," +
                   "'" + values.cheque_no + "'," +
                   "'" + values.ifsc_code + "'," +
                   "'" + values.micr + "'," +
                   "'" + values.branch_address + "'," +
                   "'" + values.branch_name + "'," +
                   "'" + values.city + "'," +
                   "'" + values.district + "'," +
                   "'" + values.state + "'," +
                  "'" + values.mergedbankingentity_gid + "'," +
                   "'" + values.mergedbankingentity_name + "'," +
                   "'" + values.special_condition + "'," +
                   "'" + values.general_remarks + "'," +
                   "'" + values.cts_enabled + "'," +
                   "'" + values.cheque_type + "',";
            if ((values.date_chequetype == null) || (values.date_chequetype == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_chequetype).ToString("yyyy-MM-dd") + "',";
            }
            if ((values.date_chequepresentation == null) || (values.date_chequepresentation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_chequepresentation).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += "'" + values.status_chequepresentation + "',";
            if ((values.date_chequeclearance == null) || (values.date_chequeclearance == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_chequeclearance).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += "'" + values.status_chequeclearance + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates

                msSQL = "update fnd_mst_tcheque2document set fndmanagement2cheque_gid ='" + msGetGid + "' where fndmanagement2cheque_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //msSQL = "update fnd_mst_tfndmanagement2cheque set fndmanagement2cheque_gid ='" + msGetGid + "' where fndmanagement2cheque_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Cheque details Added successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding Cheque";
            }
        }

        public void DaDeleteChequeDetail(string fndmanagement2cheque_gid, MdlCheque values)
        {
            msSQL = "delete from fnd_mst_tfndmanagement2cheque where fndmanagement2cheque_gid='" + fndmanagement2cheque_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Cheque Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaGetChequeDocumentList(string employee_gid, MdlChequeDocument values)
        {
            msSQL = " select cheque2document_gid ,document_name,document_path from fnd_mst_tcheque2document " +
                                 " where fndmanagement2cheque_gid ='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<chequedocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new chequedocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path =dt["document_path"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        cheque2document_gid = dt["cheque2document_gid"].ToString(),
                    });
                    values.chequedocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaChequeDocumentList(string fndmanagement2cheque_gid, MdlChequeDocument values)
        {
            msSQL = " select cheque2document_gid,document_name,document_path from fnd_mst_tcheque2document " +
                                 " where fndmanagement2cheque_gid='" + fndmanagement2cheque_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<chequedocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new chequedocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        cheque2document_gid = dt["cheque2document_gid"].ToString(),
                    });
                    values.chequedocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public bool DaChequeDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            //string lsdocument_title = httpRequest.Form["document_title"].ToString();
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Foundation/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Foundation/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(file);
                        file.Close();
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Foundation/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Foundation/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CQ2D");
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("CQDA");

                        msSQL = " delete from fnd_mst_tcheque2document where fndmanagement2cheque_gid = '" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " insert into fnd_mst_tcheque2document( " +
                                    " cheque2document_gid," +
                                    " fndmanagement2cheque_gid," +
                                    " document_gid ," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Cheque Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured in uploading cheque document..!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaChequeDocumentDelete(string cheque2document_gid, MdlChequeDocument values)
        {
            msSQL = "delete from fnd_mst_tcheque2document where cheque2document_gid='" + cheque2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaGetChequeSummary(string employee_gid, MdlCheque values)
        {

            msSQL = " select fndmanagement2cheque_gid,mergedbankingentity_name,cheque_type,cts_enabled,account_number, cheque_no" +
                    " from fnd_mst_tfndmanagement2cheque" +
                    " where customer_gid='" + employee_gid + "' group by created_date";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getchequeList = new List<cheque_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getchequeList.Add(new cheque_list
                    {
                        fndmanagement2cheque_gid = dt["fndmanagement2cheque_gid"].ToString(),
                        cheque_no = dt["cheque_no"].ToString(),
                        mergedbankingentity_name = dt["mergedbankingentity_name"].ToString(),
                        cheque_type = dt["cheque_type"].ToString(),
                        cts_enabled = dt["cts_enabled"].ToString(),
                        account_number = dt["account_number"].ToString(),

                    });

                }
            }
            values.cheque_list = getchequeList;
            dt_datatable.Dispose();
        }
        //public bool DaPostBank(string employee_gid, MdlcustomerBank values)
        //{

        //    msGetGid = objcmnfunctions.GetMasterGID("B2BK");
        //    msSQL = " insert into fnd_mst_tcustomer2bank(" +
        //            " customer2bank_gid," +
        //            " customer_gid," +
        //            " bank_name," +
        //            " branch_name," +
        //            " bank_address," +
        //            " micr_code," +
        //            " ifsc_code," +
        //            " bankaccount_name," +
        //            " bankaccountlevel_gid," +
        //            " bankaccountlevel_name," +
        //            " bankaccounttype_gid," +
        //            " bankaccounttype_name," +
        //            " bankaccount_number," +
        //            " confirmbankaccountnumber," +
        //            " created_by," +
        //            " created_date)" +
        //            " values(" +
        //            "'" + msGetGid + "'," +
        //            "'" + employee_gid + "'," +
        //             "'" + values.bank_name + "'," +
        //            "'" + values.branch_name + "'," +
        //            "'" + values.bank_address + "'," +
        //            "'" + values.micr_code + "'," +
        //            "'" + values.ifsc_code + "'," +
        //            "'" + values.bankaccount_name + "'," +
        //            "'" + values.bankaccountlevel_gid + "'," +
        //            "'" + values.bankaccountlevel_name + "'," +
        //            "'" + values.bankaccounttype_gid + "'," +
        //            "'" + values.bankaccounttype_name + "'," +
        //            "'" + values.bankaccount_number + "'," +
        //            "'" + values.confirmbankaccountnumber + "'," +
        //            "'" + employee_gid + "'," +
        //            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //    if (mnResult != 0)
        //    {

        //        values.status = true;
        //        values.message = "Bank Details Added Successfully";
        //        return true;
        //    }
        //    else
        //    {
        //        values.status = true;
        //        values.message = "Error Occured While Adding Bank Details";
        //        return false;
        //    }

        //}

        //public void DaGetBankList(string employee_gid, MdlcustomerBank values)
        //{
        //    msSQL = "select customer2bank_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name, bankaccount_number, bankaccountlevel_name,bankaccounttype_name from fnd_mst_tcustomer2bank where " +
        //      " customer_gid='" + employee_gid + "'";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    var getcustomerbank_list = new List<customerbank_list>();
        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        foreach (DataRow dr_datarow in dt_datatable.Rows)
        //        {
        //            getcustomerbank_list.Add(new customerbank_list
        //            {
        //                customer2bank_gid = (dr_datarow["customer2bank_gid"].ToString()),
        //                bank_name = (dr_datarow["bank_name"].ToString()),
        //                branch_name = (dr_datarow["branch_name"].ToString()),
        //                bank_address = (dr_datarow["bank_address"].ToString()),
        //                micr_code = (dr_datarow["micr_code"].ToString()),
        //                ifsc_code = (dr_datarow["ifsc_code"].ToString()),
        //                bankaccount_name = (dr_datarow["bankaccount_name"].ToString()),
        //                bankaccount_number = (dr_datarow["bankaccount_number"].ToString()),
        //                bankaccountlevel_name = (dr_datarow["bankaccountlevel_name"].ToString()),
        //                bankaccounttype_name = (dr_datarow["bankaccounttype_name"].ToString()),
        //            });
        //        }
        //        values.customerbank_list = getcustomerbank_list;
        //    }
        //    dt_datatable.Dispose();
        //}

        //public void DaDeleteBank(string customer2bank_gid, MdlcustomerBank values)
        //{
        //    msSQL = "delete from fnd_mst_tcustomer2bank where customer2bank_gid='" + customer2bank_gid + "'";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //    if (mnResult != 0)
        //    {

        //        values.message = "Bank Details Deleted Successfully";
        //        values.status = true;
        //    }
        //    else
        //    {
        //        values.message = "Error Occured While Deleting The Bank Details";
        //        values.status = false;

        //    }
        //}

        public bool DaPostGST(string employee_gid, MdlcustomerGST values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("B2GS");
            msSQL = " insert into fnd_mst_tcustomer2gst(" +
                    " customer2gst_gid," +
                    " customer_gid," +
                    " gststate_name," +
                    " gst_no," +
                    " gstregister_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.gststate_name + "'," +
                    "'" + values.gst_no + "'," +
                    "'" + values.gstregister_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding GST Details";
                return false;
            }

        }
        public void DaGetGSTState(string gst_code, MdlMstGST objMdlMstGST)
        {
            try
            {
                msSQL = "select gst_state from ocs_mst_tgstcode2state where " +
                        " gst_code='" + gst_code + "'";
                objMdlMstGST.gst_state = objdbconn.GetExecuteScalar(msSQL);

                objMdlMstGST.status = true;
            }
            catch
            {
                objMdlMstGST.status = false;
            }
        }


        public bool DaPostGSTList(string employee_gid, MdlcustomerGST values)
        {
            customerGSTDetails[] GstArray = values.GSTArray;
            string GSTValue, GSTStateCode, GSTState;

            for (int i = 0; i < GstArray.Length; i++)
            {
                GSTValue = GstArray[i].gstinId;
                GSTStateCode = GSTValue.Substring(0, 2);

                msSQL = "select gst_state from fnd_mst_tgstcode2state where " +
                       " gst_code='" + GSTStateCode + "'";
                GSTState = objdbconn.GetExecuteScalar(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("B2GS");
                msSQL = " insert into fnd_mst_tcustomer2gst(" +
                        " customer2gst_gid," +
                        " customer_gid," +
                        " gststate_name," +
                        " gst_no," +
                        " gstregister_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + GSTState + "'," +
                        "'" + GSTValue + "'," +
                        "'" + "Yes" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding GST Details";
                return false;
            }

        }

        public void DaGetGSTList(string employee_gid, MdlcustomerGST values)
        {
            msSQL = "select customer2gst_gid,gststate_name,gst_no,gstregister_status from fnd_mst_tcustomer2gst where " +
              " customer_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomergst_list = new List<customergst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomergst_list.Add(new customergst_list
                    {
                        customer2gst_gid = (dr_datarow["customer2gst_gid"].ToString()),
                        gststate_name = (dr_datarow["gststate_name"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gstregister_status = (dr_datarow["gstregister_status"].ToString())
                    });
                }
                values.customergst_list = getcustomergst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteGST(string customer2gst_gid, MdlcustomerGST values)
        {
            msSQL = "delete from fnd_mst_tcustomer2gst where customer2gst_gid='" + customer2gst_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Gst Details";
                values.status = false;

            }
        }

        public void DaDeleteGSTcustomer(string employee_gid, string customer_gid, MdlcustomerGST values)
        {
            msSQL = "select customer2gst_gid from fnd_mst_tcustomer2gst where customer_gid='" + employee_gid + "' or customer_gid='" + customer_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string customer2gst_gid;
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                customer2gst_gid = (dr_datarow["customer2gst_gid"].ToString());
                msSQL = "delete from fnd_mst_tcustomer2gst where customer2gst_gid='" + customer2gst_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Gst Details";
                values.status = false;

            }
        }



        public void DaGetYearsAndMonthsInBusiness(string businessstart_date, MdlFndMstCustomerMasterAdd values)
        {
            if (businessstart_date == "" || businessstart_date == null)
            {

            }
            else
            {
                var date = DateTime.Parse(new string(businessstart_date.Take(24).ToArray()));
                var businessstartdate = date.ToString("yyyy/MM/dd");

                msSQL = "select TIMESTAMPDIFF( YEAR, ('" + businessstartdate + "'), now() ) as year";
                values.year_business = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select TIMESTAMPDIFF( MONTH, ('" + businessstartdate + "'), now() ) % 12 as month";
                values.month_business = objdbconn.GetExecuteScalar(msSQL);
            }

            values.status = true;
        }

        public void DaGetPostalCodeDetails(string postal_code, MdlcustomerAddress objMdlcustomerAddress)
        {
            try
            {
                msSQL = "select city,taluka,district, state from fnd_mst_tpostalcode where " +
                        " postalcode_value='" + postal_code + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlcustomerAddress.city = (dr_datarow["city"].ToString());
                        objMdlcustomerAddress.taluka = (dr_datarow["taluka"].ToString());
                        objMdlcustomerAddress.district = (dr_datarow["district"].ToString());
                        objMdlcustomerAddress.state_name = (dr_datarow["state"].ToString());
                    }

                }
                dt_datatable.Dispose();

                objMdlcustomerAddress.status = true;
            }
            catch
            {
                objMdlcustomerAddress.status = false;
            }

        }

        public void DaGetcustomerTempClear(string employee_gid, result values)
        {
            msSQL = "delete from fnd_mst_tcustomer2mobileno where customer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from fnd_mst_tcustomer2emailaddress where customer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from fnd_mst_tcustomer2address where customer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from fnd_mst_tfndmanagement2cheque where customer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from fnd_mst_tcustomer2gst where customer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }


        public void DaPostCustomerRaiseQuery(MdlFndMstCustomerMasterAdd values, string employee_gid)
        {


            msGetGid = objcmnfunctions.GetMasterGID("FCRG");
            msSQL = "Insert into fnd_trn_tcustomerraisequery( " +
                   " customerraisequery_gid, " +
                   " customer_gid," +
                   " query_title, " +
                   " query_description," +
                   " customerraisequery_status, " +                 
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.customer_gid + "', " +
                   "'" + values.query_title.Replace("'", "") + "'," +
                    "'" + values.query_description.Replace("'", "") + "'," +
                   "'Query Raised'," +                  
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                try
                {              
                values.status = true;
                values.message = "customer Query Raised  Successfully";
                k = 1;

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();

                msSQL =
                       " select a.customer_gid, a.customer_name, a.created_by,d.approver_gid," +
                       " a.created_date from fnd_mst_tcustomer a" +
                        " left join fnd_mst_tcustomerapproving d on a.customer_gid = d.customer_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +                        
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +                          
                        " where a.customer_gid ='" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    //lsemployee_name = objODBCDatareader["employee_name"].ToString();
                    //lsname = objODBCDatareader["campaign_apr"].ToString();    
                    lscustomer_name = objODBCDatareader["customer_name"].ToString();
                    //lscc2members = objODBCDatareader["CC2members"].ToString();                 
                    lsTo2members = objODBCDatareader["created_by"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                        " where employee_gid = '" + employee_gid + "'";
                string employee_name = objdbconn.GetExecuteScalar(msSQL);

                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                k = k + 1;


                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                //msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                //cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                sub = "RE: Query Raised ";
                body = "Dear Sir,<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "Query has been Raised. The details are as follows, <br />";
                body = body + "<br />";
                body = body + "<b> Customer Name :</b> " + HttpUtility.HtmlEncode(lscustomer_name) + "<br />";
                body = body + "<br />";            
                body = body + "Kindly log into systems to view more details.";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(employee_name);
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));


                lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

                if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                {
                    lsBCCReceipients = lsBccmail_id.Split(',');
                    if (lsBccmail_id.Length == 0)
                    {
                        message.Bcc.Add(new MailAddress(lsBccmail_id));
                    }
                    else
                    {
                        foreach (string BCCEmail in lsBCCReceipients)
                        {
                            message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                        }
                    }
                }
                if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                {
                    lsToReceipients = lsto_mail.Split(',');
                    if (lsto_mail.Length == 0)
                    {
                        message.To.Add(new MailAddress(lsto_mail));
                    }
                    else
                    {
                        foreach (string ToEmail in lsToReceipients)
                        {
                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                        }
                    }
                }

                if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                {
                    lsCCReceipients = cc_mailid.Split(',');
                    if (cc_mailid.Length == 0)
                    {
                        message.CC.Add(new MailAddress(cc_mailid));
                    }
                    else
                    {
                        foreach (string CCEmail in lsCCReceipients)
                        {
                            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        }
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
                smtp.Send(message);

                values.status = true;
                    if (values.status == true)
                    {
                        msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                        " customer_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                          " raisequery_gid, " +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.customer_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Raised Successfully'," +
                           "'" + msGetGid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }


                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }

            }     
            else
            {
                values.message = "Error Occured While Adding";
                values.status = false;
            }
        }


        public void DaGetCustomerRaiseQuery(string customer_gid, MdlFndMstCustomerMasterAdd values, string employee_gid)
        {


            msSQL = " select distinct a.customer_gid,a.customerraisequery_gid,a.query_title,a.query_description,a.customerraisequery_status,a.queryresponse_remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as query_responseby " +
                    " from fnd_trn_tcustomerraisequery a " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                     " left join hrm_mst_temployee e on a.query_responseby = e.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
                    " where a.customer_gid = '" + customer_gid + "'  group by a.customerraisequery_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomerraisequery_list = new List<customerraisequery_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomerraisequery_list.Add(new customerraisequery_list
                    {
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                        customerraisequery_gid = (dr_datarow["customerraisequery_gid"].ToString()),
                        query_title = (dr_datarow["query_title"].ToString()),
                        query_description = (dr_datarow["query_description"].ToString()),
                        queryresponse_remarks = (dr_datarow["queryresponse_remarks"].ToString()),
                        query_responseby = (dr_datarow["query_responseby"].ToString()),
                        customerraisequery_status = (dr_datarow["customerraisequery_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.customerraisequery_list = getcustomerraisequery_list;
            }

            dt_datatable.Dispose();

        }

        public void DaPostCustomerresponsequery(MdlFndMstCustomerMasterAdd values, string employee_gid)
        {

            msSQL = " update fnd_trn_tcustomerraisequery set queryresponse_remarks ='" + values.queryresponse_remarks.Replace("'", "") + "'," +
                   " query_responseby='" + employee_gid + "'," +
                   " query_responsedate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " customerraisequery_status='Closed' " +
                   " where customerraisequery_gid='" + values.customerraisequery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Customer Query Closed Successfully..!";

                try { 


                k = 1;

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();

                msSQL =
                        " select a.customer_gid, a.customer_name, a.created_by,d.approver_gid," +
                        " a.created_date from fnd_mst_tcustomer a" +
                         " left join fnd_mst_tcustomerapproving d on a.customer_gid = d.customer_gid" +
                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                         " where a.customer_gid ='" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    //lsemployee_name = objODBCDatareader["employee_name"].ToString();
                    //lsname = objODBCDatareader["campaign_apr"].ToString();    
                    lscustomer_name = objODBCDatareader["customer_name"].ToString();
                    //lscc2members = objODBCDatareader["CC2members"].ToString();                 
                    lsTo2members = objODBCDatareader["approver_gid"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select query_title from fnd_trn_tcustomerraisequery  " +
                      " where customer_gid = '" + values.customer_gid + "'";
                string lsquery_title = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                        " where employee_gid = '" + employee_gid + "'";
                string employee_name = objdbconn.GetExecuteScalar(msSQL);

                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                k = k + 1;


                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                //msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                //cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                sub = "RE: Query Response ";
                body = "Dear Sir,<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "Query has been Response. The details are as follows, <br />";
                body = body + "<br />";
                body = body + "<b> Customer Name :</b> " + HttpUtility.HtmlEncode(lscustomer_name) + "<br />";
                body = body + "<br />";              
                body = body + "<b> Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title) + "<br />";
                body = body + "<br />";
                body = body + "Kindly log into systems to view more details.";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(employee_name);
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));


                lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

                if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                {
                    lsBCCReceipients = lsBccmail_id.Split(',');
                    if (lsBccmail_id.Length == 0)
                    {
                        message.Bcc.Add(new MailAddress(lsBccmail_id));
                    }
                    else
                    {
                        foreach (string BCCEmail in lsBCCReceipients)
                        {
                            message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                        }
                    }
                }
                if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                {
                    lsToReceipients = lsto_mail.Split(',');
                    if (lsto_mail.Length == 0)
                    {
                        message.To.Add(new MailAddress(lsto_mail));
                    }
                    else
                    {
                        foreach (string ToEmail in lsToReceipients)
                        {
                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                        }
                    }
                }

                if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                {
                    lsCCReceipients = cc_mailid.Split(',');
                    if (cc_mailid.Length == 0)
                    {
                        message.CC.Add(new MailAddress(cc_mailid));
                    }
                    else
                    {
                        foreach (string CCEmail in lsCCReceipients)
                        {
                            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        }
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
                smtp.Send(message);

                values.status = true;


                    if (values.status == true)
                    {
                        msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                        " customer_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                          " raisequery_gid, " +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.customer_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Close Successfully'," +
                        "'" + values.customerraisequery_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }


                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }
            }         
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

    }
}