using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using ems.utilities.Functions;
using ems.storage.Functions;
using ems.businessteam.Models;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;

namespace ems.businessteam.DataAccess
{
    public class DaMarketing
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetGid1, msGetGid2;
        int mnResult;
        string lsmarketingcall2mobileno_gid, ls_inboundgid, ls_taggedmember, ls_taguser, lsmobile_no, lsprimary_status, lswhatsapp_status, lssms_to, lsmarketingcall2email_gid, lsemail_address,
        lsmarketingcall2followup_gid,lscheck_name, lsfollowup_date, lsfollowup_time, lspath;
        string lsaddress_typegid, lsaddress_type, lsaddressline1, lsaddressline2,lsbaselocation_name, lslandmark, lstaluka, lspostal_code, lscity, lsdistrict,
              lsstate, lscountry, lslatitude, lslongitude,lsfollowup_status,lsfollowup_remarks, lsmarketingcall_gid, lsmarketingcall2address_gid;
        string lsticket_refid, lsentity_gid, lsentity_name, lssourceofcontact_gid, lssourceofcontact_name, lscallreceivednumber_gid, lscallreceivednumber_name,
            lscustomer_type, lscallreceived_date, lscaller_name, lsinternalreference_gid, lsinternalreference_name, lscallerassociate_company,
            lsoffice_landlineno, lscalltype_gid, lscalltype_name, lsfunction_gid,lsleadrequesttype_gid,lsleadrequesttype_name, lsfunction_name, lsrequirement, lsenquiry_description,
            lscallclosure_status, lsassignemployee_gid, lsassignemployee_name, lsassignclosure_remarks,lsclosed_remarks, lsmarketingcall_status, lsfunction_remarks,lstat_hours,
            lsassign_by, lsassign_date, lstransfer_by, lstransfer_date, lscompleted_by, lscompleted_date;
        public string ls_server, ls_assignemployee_name,lsleadrequire_gid,lsleadrequire_name,lscompany_name,lsyour_name,lsmessage_name,lsindustry_name, ls_username, lsto2members, assign_date,ls_password, tomail_id, sub, body, employeename, cc_mailid, employee_reporting_to, lsconvertedmail_id,cc_leadmailid;
        int ls_port;
        public string[] lsCCReceipients;
        public string lsBccmail_id;
        public string[] lsBCCReceipients;
        public string[] lstoReceipients;




        //Mobile No
        public bool DaPostMarketingCallMobileNo(string employee_gid, MdlMarketingCallMobileNo values)
        {
            msSQL = " select primary_status from mar_trn_tmarketingcall2mobileno where primary_status='Yes' and " +
                    " (marketingcall_gid='" + employee_gid + "' or marketingcall_gid='" + values.marketingcall_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }
            msSQL = "select marketingcall2mobileno_gid from mar_trn_tmarketingcall2mobileno where mobile_no='" + values.mobile_no + "' " +
                " and marketingcall_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("BD2M");
            msSQL = " insert into mar_trn_tmarketingcall2mobileno(" +
                    " marketingcall2mobileno_gid," +
                    " marketingcall_gid," +
                    " mobile_no," +
                    " primary_status," +
                    " whatsapp_status," +
                    " sms_to," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.whatsapp_status + "'," +
                    "'" + values.sms_to + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetMarketingCallMobileNoList(string employee_gid, MdlMarketingCallMobileNo values)
        {
            msSQL = "select mobile_no,marketingcall2mobileno_gid,primary_status,whatsapp_status,sms_to from mar_trn_tmarketingcall2mobileno where " +
              " marketingcall_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallmobileno_list = new List<MarketingCallmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallmobileno_list.Add(new MarketingCallmobileno_list
                    {
                        marketingcall2mobileno_gid = (dr_datarow["marketingcall2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_status = (dr_datarow["whatsapp_status"].ToString()),
                        sms_to = (dr_datarow["sms_to"].ToString()),
                    });
                }
            }
            values.MarketingCallmobileno_list = getMarketingCallmobileno_list;
            dt_datatable.Dispose();
        }

        public void DaMarketingCallMobileNoTempList(string marketingcall_gid, string employee_gid, MdlMarketingCallMobileNo values)
        {
            msSQL = "select mobile_no,marketingcall2mobileno_gid,primary_status,whatsapp_status,sms_to from mar_trn_tmarketingcall2mobileno where " +
              " marketingcall_gid = '" + employee_gid + "' or marketingcall_gid = '" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallmobileno_list = new List<MarketingCallmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallmobileno_list.Add(new MarketingCallmobileno_list
                    {
                        marketingcall2mobileno_gid = (dr_datarow["marketingcall2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_status = (dr_datarow["whatsapp_status"].ToString()),
                        sms_to = (dr_datarow["sms_to"].ToString()),
                    });
                }
            }
            values.MarketingCallmobileno_list = getMarketingCallmobileno_list;
            dt_datatable.Dispose();
        }

        public void DaMarketingCallMobileNoList(string marketingcall_gid, string employee_gid, MdlMarketingCallMobileNo values)
        {
            msSQL = "select mobile_no,marketingcall2mobileno_gid,primary_status,whatsapp_status,sms_to from mar_trn_tmarketingcall2mobileno where " +
              " marketingcall_gid = '" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallmobileno_list = new List<MarketingCallmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallmobileno_list.Add(new MarketingCallmobileno_list
                    {
                        marketingcall2mobileno_gid = (dr_datarow["marketingcall2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_status = (dr_datarow["whatsapp_status"].ToString()),
                        sms_to = (dr_datarow["sms_to"].ToString()),
                    });
                }
            }
            values.MarketingCallmobileno_list = getMarketingCallmobileno_list;
            dt_datatable.Dispose();
        }

        public void DaEditMarketingCallMobileNo(string marketingcall2mobileno_gid, MdlMarketingCallMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,marketingcall2mobileno_gid,primary_status,whatsapp_status,sms_to from mar_trn_tmarketingcall2mobileno where " +
                        " marketingcall2mobileno_gid='" + marketingcall2mobileno_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.whatsapp_status = objODBCDatareader["whatsapp_status"].ToString();
                    values.sms_to = objODBCDatareader["sms_to"].ToString();
                    values.marketingcall2mobileno_gid = objODBCDatareader["marketingcall2mobileno_gid"].ToString();
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

        public void DaUpdateMarketingCallMobileNo(string employee_gid, MdlMarketingCallMobileNo values)
        {
            msSQL = " select mobile_no,marketingcall2mobileno_gid,primary_status,whatsapp_status,sms_to from mar_trn_tmarketingcall2mobileno where " +
                    " marketingcall2mobileno_gid='" + values.marketingcall2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lswhatsapp_status = objODBCDatareader["whatsapp_status"].ToString();
                lssms_to = objODBCDatareader["sms_to"].ToString();
                lsmarketingcall2mobileno_gid = objODBCDatareader["marketingcall2mobileno_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update mar_trn_tmarketingcall2mobileno set " +
                         " mobile_no='" + values.mobile_no + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " whatsapp_status='" + values.whatsapp_status + "'," +
                         " sms_to='" + values.sms_to + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where marketingcall2mobileno_gid='" + values.marketingcall2mobileno_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("M2ML");

                    msSQL = "Insert into mar_trn_tmarketingcall2mobilenoupdatelog(" +
                   " marketingcall2mobilenoupdatelog_gid, " +
                   " marketingcall2mobileno_gid, " +
                   " marketingcall_gid, " +
                   " mobile_no," +
                   " primary_status," +
                   " whatsapp_status," +
                   " sms_to," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + lsmarketingcall2mobileno_gid + "'," +
                   "'" + values.marketingcall_gid + "'," +
                   "'" + lsmobile_no + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + lswhatsapp_status + "'," +
                   "'" + lssms_to + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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

        public void DaMarketingCallMobileNoDelete(string marketingcall2mobileno_gid, MdlMarketingCallMobileNo values)
        {
            msSQL = "delete from mar_trn_tmarketingcall2mobileno where marketingcall2mobileno_gid='" + marketingcall2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        //lead status
        public bool DaPostMarketingCallLeadstatus(string employee_gid, MdlMarketingCallLeadstatus values)
        {
            
           
            msGetGid = objcmnfunctions.GetMasterGID("BD2L");
            msSQL = " insert into mar_trn_tmarketingcall2leadstatus(" +
                    " marketingcall2leadstatus_gid," +
                    " marketingcall_gid," +                   
                    " lead_type," +
                     "ticket_refid," +
                    " closure_status," +
                    " loanproduct_name," +
                    " loanproduct_gid," +
                    " loansubproduct_name," +
                    " loansubproduct_gid," +
                    " loan_amount," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.marketingcall_gid + "'," +
                    "'" + values.lead_type + "'," +
                     "'" + values.ticket_refid + "'," +
                    "'" + values.closure_status + "'," +
                     "'" + values.loanproduct_name + "'," +
                     "'" + values.loanproduct_gid + "'," +
                     "'" + values.loansubproduct_name + "'," +
                    "'" + values.loansubproduct_gid + "'," +
                     "'" + values.loan_amount + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
              
      
       
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Lead Status Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }


        //Email
        public bool DaPostMarketingCallEmail(string employee_gid, MdlMarketingCallEmail values)
        {
            msSQL = " select primary_status from mar_trn_tmarketingcall2email where primary_status='Yes' and " +
                    " (marketingcall_gid='" + employee_gid + "' or marketingcall_gid='" + values.marketingcall_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select marketingcall2email_gid from mar_trn_tmarketingcall2email where email_address='" + values.email_address + "' " +
                " and marketingcall_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("BD2E");
            msSQL = " insert into mar_trn_tmarketingcall2email(" +
                    " marketingcall2email_gid," +
                    " marketingcall_gid," +
                    " email_address," +
                    " primary_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_status + "'," +
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
                values.message = "Error Occured";
                return false;
            }
        }
      
        public void DaGetMarketingCallLeadstatusList(string marketingcall_gid, MdlMarketingCallLeadstatus values)
        {
            msSQL = "select lead_type,marketingcall2leadstatus_gid,closure_status, loanproduct_name, " +
                "loanproduct_gid, loansubproduct_name, loansubproduct_gid, loan_amount from mar_trn_tmarketingcall2leadstatus where " +
              " marketingcall_gid='" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallleadstatus_list = new List<MarketingCallLeadstatus_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallleadstatus_list.Add(new MarketingCallLeadstatus_list
                    {
                        marketingcall2leadstatus_gid = (dr_datarow["marketingcall2leadstatus_gid"].ToString()),
                        lead_type = (dr_datarow["lead_type"].ToString()),
                        closure_status = (dr_datarow["closure_status"].ToString()),
                        loanproduct_name = (dr_datarow["loanproduct_name"].ToString()),
                        loanproduct_gid = (dr_datarow["loanproduct_gid"].ToString()),
                        loansubproduct_name = (dr_datarow["loansubproduct_name"].ToString()),
                        loansubproduct_gid = (dr_datarow["loansubproduct_gid"].ToString()),
                        loan_amount = (dr_datarow["loan_amount"].ToString()),
                    });
                }
                
            }
            values.MarketingCallLeadstatus_list = getMarketingCallleadstatus_list;
            dt_datatable.Dispose();
        }

//listemail

        public void DaGetMarketingCallEmailList(string employee_gid, MdlMarketingCallEmail values)
        {
            msSQL = "select email_address,marketingcall2email_gid,primary_status from mar_trn_tmarketingcall2email where " +
              " marketingcall_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallemail_list = new List<MarketingCallemail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallemail_list.Add(new MarketingCallemail_list
                    {
                        marketingcall2email_gid = (dr_datarow["marketingcall2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                    });
                }
            }
            values.MarketingCallemail_list = getMarketingCallemail_list;
            dt_datatable.Dispose();
        }

        public void DaMarketingCallEmailTempList(string marketingcall_gid, string employee_gid, MdlMarketingCallEmail values)
        {
            msSQL = "select email_address,marketingcall2email_gid,primary_status from mar_trn_tmarketingcall2email where " +
              " marketingcall_gid = '" + employee_gid + "' or marketingcall_gid = '" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallemail_list = new List<MarketingCallemail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallemail_list.Add(new MarketingCallemail_list
                    {
                        marketingcall2email_gid = (dr_datarow["marketingcall2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                    });
                }
            }
            values.MarketingCallemail_list = getMarketingCallemail_list;
            dt_datatable.Dispose();
        }

        public void DaMarketingCallEmailList(string marketingcall_gid, string employee_gid, MdlMarketingCallEmail values)
        {
            msSQL = "select email_address,marketingcall2email_gid,primary_status from mar_trn_tmarketingcall2email where " +
              " marketingcall_gid = '" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallemail_list = new List<MarketingCallemail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallemail_list.Add(new MarketingCallemail_list
                    {
                        marketingcall2email_gid = (dr_datarow["marketingcall2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                    });
                }
            }
            values.MarketingCallemail_list = getMarketingCallemail_list;
            dt_datatable.Dispose();
        }

        public void DaEditMarketingCallEmail(string marketingcall2email_gid, MdlMarketingCallEmail values)
        {
            try
            {
                msSQL = " select email_address,marketingcall2email_gid,primary_status from mar_trn_tmarketingcall2email where " +
                        " marketingcall2email_gid='" + marketingcall2email_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.marketingcall2email_gid = objODBCDatareader["marketingcall2email_gid"].ToString();
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

        public void DaUpdateMarketingCallEmail(string employee_gid, MdlMarketingCallEmail values)
        {
            msSQL = " select email_address,marketingcall2email_gid,primary_status from mar_trn_tmarketingcall2email where " +
                    " marketingcall2email_gid='" + values.marketingcall2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lsmarketingcall2email_gid = objODBCDatareader["marketingcall2email_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update mar_trn_tmarketingcall2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where marketingcall2email_gid='" + values.marketingcall2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("B2EL");

                    msSQL = "Insert into mar_trn_tmarketingcall2emailupdatelog(" +
                   " marketingcall2emailupdatelog_gid, " +
                   " marketingcall2email_gid, " +
                   " marketingcall_gid, " +
                   " email_address," +
                   " primary_status," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + lsmarketingcall2email_gid + "'," +
                   "'" + values.marketingcall_gid + "'," +
                   "'" + lsemail_address + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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

        public void DaMarketingCallLeadstatusDelete(string marketingcall2leadstatus_gid, MdlMarketingCallLeadstatus values)
        {
            msSQL = "delete from mar_trn_tmarketingcall2leadstatus where marketingcall2leadstatus_gid='" + marketingcall2leadstatus_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Lead Status Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaMarketingCallEmailDelete(string marketingcall2email_gid, MdlMarketingCallEmail values)
        {
            msSQL = "delete from mar_trn_tmarketingcall2email where marketingcall2email_gid='" + marketingcall2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }



        //follow up by marketing Call Gid
        //

        public bool DaPostMarketingCallFollowUpMg(string employee_gid, MdlMarketingCallFollowUp values)
        {            
            msGetGid = objcmnfunctions.GetMasterGID("BCFU");
            msSQL = " insert into mar_trn_tmarketingcall2followup(" +
                    " marketingcall2followup_gid," +
                    " marketingcall_gid," +
                    " followup_date," +
                    " followup_time," +
                    " followup_status," +
                    " followup_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.marketingcall_gid + "',";

            if (values.followup_date == null || values.followup_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.followup_date).ToString("yyyy-MM-dd") + "',";
            }
            if (values.followup_time == null || values.followup_time == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.followup_time).ToString("HH:mm:ss") + "',";
            }
            msSQL += "'" + values.followup_status + "'," +
                    "'" + values.followup_remarks + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Follow Up Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }


        //Follow Up

        public bool DaPostMarketingCallFollowUp(string employee_gid, MdlMarketingCallFollowUp values)
        {

            /*  msSQL = "select marketingcall2followup_gid from mar_mst_tmarketingcall2followup where email_address='" + values.email_address + "' " +
                  " and marketingcall_gid='" + employee_gid + "'";
              objODBCDatareader = objdbconn.GetDataReader(msSQL);
              if (objODBCDatareader.HasRows)
              {
                  objODBCDatareader.Close();
                  values.status = false;
                  values.message = "Already This Email Address Added";
                  return false;
              } */
            msGetGid = objcmnfunctions.GetMasterGID("BCFU");
            msSQL = " insert into mar_trn_tmarketingcall2followup(" +
                    " marketingcall2followup_gid," +
                    " marketingcall_gid," +
                    " followup_date," +
                    " followup_time," +
                    " followup_status," +
                    " followup_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "',";

            if (values.followup_date == null || values.followup_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.followup_date).ToString("yyyy-MM-dd") + "',";
            }
            if (values.followup_time == null || values.followup_time == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.followup_time).ToString("HH:mm:ss") + "',";
            }
            msSQL += "'" + values.followup_status + "'," +
                    "'" + values.followup_remarks + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Follow Up Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
        public void DaGetMarketingCallMyFollowUpList(string marketingcall_gid, MdlMarketingCallFollowUp values)
        {
            msSQL = "select date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%H:%i') as followup_time,followup_status,followup_remarks," +
                " marketingcall2followup_gid from mar_trn_tmarketingcall2followup where " +
                " marketingcall_gid='" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallfollowup_list = new List<MarketingCallfollowup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallfollowup_list.Add(new MarketingCallfollowup_list
                    {
                        marketingcall2followup_gid = (dr_datarow["marketingcall2followup_gid"].ToString()),
                        followup_date = (dr_datarow["followup_date"].ToString()),
                        followup_time = (dr_datarow["followup_time"].ToString()),
                        followup_status = (dr_datarow["followup_status"].ToString()),
                        followup_remarks = (dr_datarow["followup_remarks"].ToString()),
                    });
                }
            }
            values.MarketingCallfollowup_list = getMarketingCallfollowup_list;
            dt_datatable.Dispose();
        }

        public void DaGetMarketingCallFollowUpList(string employee_gid, MdlMarketingCallFollowUp values)
        {
            msSQL = "select date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%H:%i') as followup_time,followup_status,followup_remarks," +
                " marketingcall2followup_gid from mar_trn_tmarketingcall2followup where " +
                " marketingcall_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallfollowup_list = new List<MarketingCallfollowup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallfollowup_list.Add(new MarketingCallfollowup_list
                    {
                        marketingcall2followup_gid = (dr_datarow["marketingcall2followup_gid"].ToString()),
                        followup_date = (dr_datarow["followup_date"].ToString()),
                        followup_time = (dr_datarow["followup_time"].ToString()),
                        followup_status = (dr_datarow["followup_status"].ToString()),
                        followup_remarks = (dr_datarow["followup_remarks"].ToString()),
                    });
                }
            }
            values.MarketingCallfollowup_list = getMarketingCallfollowup_list;
            dt_datatable.Dispose();
        }

        public void DaMarketingCallFollowUpTempList(string marketingcall_gid, string employee_gid, MdlMarketingCallFollowUp values)
        {
            msSQL = "select date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%H:%i') as followup_time,followup_status,followup_remarks," +
                " marketingcall2followup_gid from mar_trn_tmarketingcall2followup where " +
                " marketingcall_gid = '" + employee_gid + "' or marketingcall_gid = '" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallfollowup_list = new List<MarketingCallfollowup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallfollowup_list.Add(new MarketingCallfollowup_list
                    {
                        marketingcall2followup_gid = (dr_datarow["marketingcall2followup_gid"].ToString()),
                        followup_date = (dr_datarow["followup_date"].ToString()),
                        followup_time = (dr_datarow["followup_time"].ToString()),
                        followup_status = (dr_datarow["followup_status"].ToString()),
                        followup_remarks = (dr_datarow["followup_remarks"].ToString()),
                    });
                }
            }
            values.MarketingCallfollowup_list = getMarketingCallfollowup_list;
            dt_datatable.Dispose();
        }

        public void DaMarketingCallFollowUpList(string marketingcall_gid, string employee_gid, MdlMarketingCallFollowUp values)
        {
            msSQL = "select date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%H:%i') as followup_time,followup_status,followup_remarks," +
                " marketingcall2followup_gid from mar_trn_tmarketingcall2followup where " +
              " marketingcall_gid = '" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCallfollowup_list = new List<MarketingCallfollowup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCallfollowup_list.Add(new MarketingCallfollowup_list
                    {
                        marketingcall2followup_gid = (dr_datarow["marketingcall2followup_gid"].ToString()),
                        followup_date = (dr_datarow["followup_date"].ToString()),
                        followup_time = (dr_datarow["followup_time"].ToString()),
                        followup_status = (dr_datarow["followup_status"].ToString()),
                        followup_remarks = (dr_datarow["followup_remarks"].ToString()),
                    });
                }
            }
            values.MarketingCallfollowup_list = getMarketingCallfollowup_list;
            dt_datatable.Dispose();
        }

        public void DaEditMarketingCallFollowUp(string marketingcall2followup_gid, MdlMarketingCallFollowUp values)
        {
            try
            {
                msSQL = " select date_format(followup_date,'%Y-%m-%d') as followup_date,followup_time,followup_status,followup_remarks," +
                        " marketingcall2followup_gid from mar_trn_tmarketingcall2followup where " +
                        " marketingcall2followup_gid='" + marketingcall2followup_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.followup_date = objODBCDatareader["followup_date"].ToString();
                    values.followup_time = objODBCDatareader["followup_time"].ToString();
                    values.followup_status = objODBCDatareader["followup_status"].ToString();
                    values.followup_remarks = objODBCDatareader["followup_remarks"].ToString();

                    values.marketingcall2followup_gid = objODBCDatareader["marketingcall2followup_gid"].ToString();
                    if (objODBCDatareader["followup_time"].ToString() == "" || objODBCDatareader["followup_time"].ToString() == null)
                    {
                    }
                    else
                    {
                        values.Tfollowup_time = Convert.ToDateTime(objODBCDatareader["followup_time"].ToString());
                    }
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

        public void DaUpdateMarketingCallFollowUp(string employee_gid, MdlMarketingCallFollowUp values)
        {
            msSQL = " select followup_date,followup_time,followup_status,followup_remarks,marketingcall2followup_gid from mar_trn_tmarketingcall2followup where " +
                    " marketingcall2followup_gid='" + values.marketingcall2followup_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsfollowup_date = objODBCDatareader["followup_date"].ToString();
                lsfollowup_time = objODBCDatareader["followup_time"].ToString();
                lsfollowup_status = objODBCDatareader["followup_status"].ToString();
                lsfollowup_remarks = objODBCDatareader["followup_remarks"].ToString();
                lsmarketingcall2followup_gid = objODBCDatareader["marketingcall2followup_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update mar_trn_tmarketingcall2followup set ";
                if (Convert.ToDateTime(values.followup_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " followup_date='" + Convert.ToDateTime(values.followup_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if ((values.followup_time == null) || (values.followup_time == ""))
                {
                    msSQL += "followup_time=null,";
                }
                else
                {
                    msSQL += "followup_time='" + Convert.ToDateTime(values.followup_time).ToString("HH:mm:ss") + "',";
                }

                msSQL += " followup_status='" + values.followup_status + "'," +
                             " followup_remarks='" + values.followup_remarks + "'," +
                               " updated_by='" + employee_gid + "'," +
                              " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                              " where marketingcall2followup_gid='" + values.marketingcall2followup_gid + "' "; 
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("B2FL");

                    msSQL = "Insert into mar_trn_tmarketingcall2followupupdatelog(" +
                   " mar_trn_tmarketingcall2followupupdatelog_gid, " +
                   " marketingcall2followup_gid, " +
                   " marketingcall_gid, " +
                   " followup_date," +
                   " followup_time," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + lsmarketingcall2followup_gid + "'," +
                   "'" + values.marketingcall_gid + "'," +
                   "'" + lsfollowup_date + "'," +
                   "'" + lsfollowup_time + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Follow Up Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaMarketingCallFollowUpDelete(string marketingcall2followup_gid, MdlMarketingCallFollowUp values)
        {
            msSQL = "delete from mar_trn_tmarketingcall2followup where marketingcall2followup_gid='" + marketingcall2followup_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Follow Up Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        //Address

        public bool DaPostMarketingCallAddress(string employee_gid, MdlMarketingCallAddress values)
        {
            msSQL = "select primary_status from mar_trn_tmarketingcall2address where primary_status='Yes' and (marketingcall_gid='" + employee_gid + "' or marketingcall_gid='" + values.marketingcall_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }

            msSQL = "select marketingcall2address_gid from mar_trn_tmarketingcall2address where addresstype_name='" + values.addresstype_name + "' and " +
               " (marketingcall_gid='" + employee_gid + "' or marketingcall_gid='" + values.marketingcall_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("BD2A");
            msSQL = " insert into mar_trn_tmarketingcall2address(" +
                    " marketingcall2address_gid," +
                    " marketingcall_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " primary_status," +
                    " addressline1," +
                    " addressline2," +
                    " landmark," +
                    " postal_code," +
                    " city," +
                    " taluka," +
                    " district," +
                    " state," +
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
                    "'" + values.primary_status + "'," +
                    "'" + values.addressline1.Replace("'", "") + "'," +
                    "'" + values.addressline2 + "'," +
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

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetMarketingCallAddressList(string employee_gid, MdlMarketingCallAddress values)
        {
            msSQL = " select marketingcall2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from mar_trn_tmarketingcall2address where marketingcall_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCalladdress_list = new List<MarketingCalladdress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCalladdress_list.Add(new MarketingCalladdress_list
                    {
                        marketingcall2address_gid = (dr_datarow["marketingcall2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())
                    });
                }
                values.MarketingCalladdress_list = getMarketingCalladdress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaMarketingCallAddressTempList(string marketingcall_gid, string employee_gid, MdlMarketingCallAddress values)
        {
            msSQL = " select marketingcall2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from mar_trn_tmarketingcall2address where marketingcall_gid='" + employee_gid + "' or marketingcall_gid = '" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCalladdress_list = new List<MarketingCalladdress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCalladdress_list.Add(new MarketingCalladdress_list
                    {
                        marketingcall2address_gid = (dr_datarow["marketingcall2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())
                    });
                }
                values.MarketingCalladdress_list = getMarketingCalladdress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaMarketingCallAddressList(string marketingcall_gid, string employee_gid, MdlMarketingCallAddress values)
        {
            msSQL = " select marketingcall2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from mar_trn_tmarketingcall2address where marketingcall_gid = '" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMarketingCalladdress_list = new List<MarketingCalladdress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMarketingCalladdress_list.Add(new MarketingCalladdress_list
                    {
                        marketingcall2address_gid = (dr_datarow["marketingcall2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())
                    });
                }
                values.MarketingCalladdress_list = getMarketingCalladdress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditMarketingCallAddress(string marketingcall2address_gid, MdlMarketingCallAddress values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, marketingcall_gid, marketingcall2address_gid " +
                    " from mar_trn_tmarketingcall2address where marketingcall2address_gid='" + marketingcall2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.addresstype_gid = objODBCDatareader["addresstype_gid"].ToString();
                    values.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.marketingcall_gid = objODBCDatareader["marketingcall_gid"].ToString();
                    values.marketingcall2address_gid = objODBCDatareader["marketingcall2address_gid"].ToString();
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

        public void DaUpdateMarketingCallAddress(string employee_gid, MdlMarketingCallAddress values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, marketingcall_gid, marketingcall2address_gid " +
                    " from mar_trn_tmarketingcall2address where marketingcall2address_gid='" + values.marketingcall2address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaddress_typegid = objODBCDatareader["addresstype_gid"].ToString();
                lsaddress_type = objODBCDatareader["addresstype_name"].ToString();
                lsaddressline1 = objODBCDatareader["addressline1"].ToString();
                lsaddressline2 = objODBCDatareader["addressline2"].ToString();
                lslandmark = objODBCDatareader["landmark"].ToString();
                lstaluka = objODBCDatareader["taluka"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lspostal_code = objODBCDatareader["postal_code"].ToString();
                lscity = objODBCDatareader["city"].ToString();
                lsdistrict = objODBCDatareader["district"].ToString();
                lsstate = objODBCDatareader["state"].ToString();
                lscountry = objODBCDatareader["country"].ToString();
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
                lsmarketingcall_gid = objODBCDatareader["marketingcall_gid"].ToString();
                lsmarketingcall2address_gid = objODBCDatareader["marketingcall2address_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update mar_trn_tmarketingcall2address set " +
                         " addresstype_gid='" + values.addresstype_gid + "'," +
                         " addresstype_name='" + values.addresstype_name + "'," +
                         " addressline1='" + values.addressline1.Replace("'", "") + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " postal_code='" + values.postal_code + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " latitude='" + values.latitude + "'," +
                         " longitude='" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where marketingcall2address_gid='" + values.marketingcall2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("B2AL");

                    msSQL = " insert into mar_trn_tmarketingcall2addressupdatelog(" +
                  " marketingcall2addressupdatelog_gid," +
                  " marketingcall2address_gid," +
                  " marketingcall_gid," +
                  " addresstype_gid," +
                  " addresstype_name," +
                  " addressline1," +
                  " addressline2," +
                  " primary_status," +
                  " landmark," +
                  " postal_code," +
                  " city," +
                  " taluka," +
                  " district," +
                  " state," +
                  " country," +
                  " latitude," +
                  " longitude," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + lsmarketingcall2address_gid + "'," +
                  "'" + lsmarketingcall_gid + "'," +
                  "'" + lsaddress_typegid + "'," +
                  "'" + lsaddress_type + "'," +
                  "'" + lsaddressline1 + "'," +
                  "'" + lsaddressline2 + "'," +
                  "'" + lsprimary_status + "'," +
                  "'" + lslandmark + "'," +
                  "'" + lspostal_code + "'," +
                  "'" + lscity + "'," +
                  "'" + lstaluka + "'," +
                  "'" + lsdistrict + "'," +
                  "'" + lsstate + "'," +
                  "'" + lscountry + "'," +
                  "'" + lslatitude + "'," +
                  "'" + lslongitude + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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

        public void DaMarketingCallAddressDelete(string marketingcall2address_gid, MdlMarketingCallAddress values)
        {
            msSQL = "delete from mar_trn_tmarketingcall2address where marketingcall2address_gid='" + marketingcall2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        //IB Call

        public void DaMarketingCallSave(string employee_gid, MdlMarketingCall values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("BDCA");

            msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + values.entity_gid + "'";
            string lsentity_code = objdbconn.GetExecuteScalar(msSQL);

            string msGETRef = objcmnfunctions.GetMasterGID("BDC");
            msGETRef = msGETRef.Replace("BDC", "");

            string lsticket_refid = "BDC" + DateTime.Now.ToString("ddMMyyyy") + lsentity_code + msGETRef;

            msSQL = " insert into mar_trn_tmarketingcall(" +
                   " marketingcall_gid," +
                   " ticket_refid," +
                   " entity_gid," +
                   " entity_name," +
                   " marketingsourceofcontact_gid," +
                   " marketingsourceofcontact_name," +
                    " leadrequesttype_gid," +
                   " leadrequesttype_name," +
                   " marketingcallreceivednumber_gid," +
                   " marketingcallreceivednumber_name," +
                   " callreceived_date," +
                   " caller_name," +
                   " internalreference_gid," +
                   " internalreference_name," +
                   " callerassociate_company," +
                   " office_landlineno," +
                   " marketingcalltype_gid," +
                   " marketingcalltype_name," +
                   " marketingfunction_gid," +
                   " marketingfunction_name," +
                   " requirement," +
                   " enquiry_description," +
                   " callclosure_status," +
                   " assignemployee_gid," +
                   " assignemployee_name," +
                   " assign_by," +
                   " assign_date," +
                   " assignclosure_remarks," +
                   " marketingcall_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + lsticket_refid + "'," +
                   "'" + values.entity_gid + "'," +
                   "'" + values.entity_name + "'," +
                   "'" + values.marketingsourceofcontact_gid + "'," +
                   "'" + values.marketingsourceofcontact_name + "'," +
                     "'" + values.leadrequesttype_gid + "'," +
                   "'" + values.leadrequesttype_name + "'," +
                   "'" + values.marketingcallreceivednumber_gid + "'," +
                   "'" + values.marketingcallreceivednumber_name + "'," +
                   "'" + values.callreceived_date + "',";
            if (values.caller_name == "" || values.caller_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.caller_name.Replace("'", "") + "',";
            }
            msSQL += "'" + values.internalreference_gid + "'," +
                    "'" + values.internalreference_name + "',";
            if (values.callerassociate_company == "" || values.callerassociate_company == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.callerassociate_company.Replace("'", "") + "',";
            }
            msSQL += "'" + values.office_landlineno + "'," +
                     "'" + values.marketingcalltype_gid + "'," +
                     "'" + values.marketingcalltype_name + "'," +
                     "'" + values.marketingfunction_gid + "'," +
                     "'" + values.marketingfunction_name + "'," +
                     "'" + values.requirement + "'," +
                     "'" + values.enquiry_description + "'," +
                     "'" + values.callclosure_status + "'," +
                     "'" + values.assignemployee_gid + "'," +
                     "'" + values.assignemployee_name + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     "'" + values.assignclosure_remarks + "'," +
                     "'Incomplete'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                if (values.tagemployee_list == null)
                {
                }
                else
                {
                    for (var i = 0; i < values.tagemployee_list.Count; i++)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                        msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                " marketingcall2taggedmember_gid," +
                                " marketingcall_gid," +
                                " taggedmember_gid," +
                                " taggedmember_name," +
                                " tagged_by," +
                                " tagged_date," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + msGetGid + "'," +
                                "'" + values.tagemployee_list[i].employee_gid + "'," +
                                "'" + values.tagemployee_list[i].employee_name + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                //Updates

                msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Lead Details Saved Sucessfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaMarketingUnassignedLeadSubmit(string employee_gid, MdlMarketingCall values)
        {

            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            DateTime lsmailalert = datevalue.AddHours(Convert.ToDouble(values.tat_hours));
                     
            msGetGid = objcmnfunctions.GetMasterGID("BDCA");

            msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + values.entity_gid + "'";
            string lsentity_code = objdbconn.GetExecuteScalar(msSQL);

            string msGETRef = objcmnfunctions.GetMasterGID("BDC");
            msGETRef = msGETRef.Replace("BDC", "");

            string lsticket_refid = "BDC" + DateTime.Now.ToString("ddMMyyyy") + lsentity_code + msGETRef;
            
            msSQL = " insert into mar_trn_tmarketingcall(" +
                 " marketingcall_gid," +
                 " ticket_refid," +
                 " entity_gid," +
                 " entity_name," +
                 " marketingsourceofcontact_gid," +
                 " marketingsourceofcontact_name," +
                 " leadrequesttype_gid," +
                 " leadrequesttype_name," +
                 " caller_name," +
                 " internalreference_gid," +
                  " callreceived_date," +
                 " internalreference_name," +
                 " callerassociate_company," +
                   " assignclosure_remarks," +
                 " closed_remarks," +
                  " mail_alert," +
                 " office_landlineno," +
                 " marketingcalltype_gid," +
                 " marketingcalltype_name," +
                 " enquiry_description," +
                 " callclosure_status," +
                 "callassign_status," +
                 " assignemployee_gid," +
                 " assignemployee_name," +
                 " tat_hours," +
                 " assign_by," +
                 " assign_date," +               
                 " baselocation_name," +
                 " marketingcall_status," +
                 " created_by," +
                 " created_date)" +
                 " values(" +
                 "'" + values.marketingcall_gid + "'," +
                 "'" +values.ticket_refid + "'," +
                 "'" + values.entity_gid + "'," +
                 "'" + values.entity_name + "'," +
                 "'" + values.marketingsourceofcontact_gid + "'," +
                 "'" + values.marketingsourceofcontact_name + "'," +
                 "'" + values.leadrequesttype_gid + "'," +
                 "'" + values.leadrequesttype_name + "'," +
            "'" + values.caller_name.Replace("'", "") + "'," +
                 "'" + values.internalreference_gid + "'," +
              "'" + values.callreceived_date + "'," +
                 "'" + values.internalreference_name + "',";

            if (values.callerassociate_company == "" || values.callerassociate_company == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.callerassociate_company.Replace("'", "") + "',";
            }
            if (values.assignclosure_remarks == "" || values.assignclosure_remarks == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.assignclosure_remarks.Replace("'", "") + "',";
            }
            if (values.closed_remarks == "" || values.closed_remarks == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.closed_remarks.Replace("'", "") + "',";
            }
            if (lsmailalert == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(lsmailalert).ToString("yyyy-MM-dd HH:mm") + "',";
            }
            msSQL += "'" + values.office_landlineno + "'," +
                     "'" + values.marketingcalltype_gid + "'," +
                     "'" + values.marketingcalltype_name + "'," +
                     "'" + values.enquiry_description.Replace("'", "") + "'," +
                     "'" + values.callclosure_status + "'," +
                     "'" + values.callclosure_status + "'," +
                     "'" + values.assignemployee_gid + "'," +
                     "'" + values.assignemployee_name + "'," +
                     "'" + values.tat_hours + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     //"'" + values.assignclosure_remarks + "'," +
                     //"'" + values.closed_remarks + "'," +
                     "'" + values.baselocation_name + "'," +
                     "'Converted'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Lead Details Submitted Successfully";

                if (values.callclosure_status == "Follow Up")
                {
                    msSQL = " update mar_trn_tmarketingcall set followup_remarks = '" + values.assignclosure_remarks.Replace("'", "") + "'," +
                            " followup_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " followup_by = '" + employee_gid + "'" +
                            " where marketingcall_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                    msSQL = "Insert into mar_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " marketingcall_gid," +
                               " status," +
                               " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + msGetGid + "'," +
                               "' Follow Up'," +
                               "'" + employee_gid + "'," +
                               "'" + values.assignclosure_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.tagemployee_list != null)
                    {

                        for (var i = 0; i < values.tagemployee_list.Count; i++)
                        {
                            msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                            msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                    " marketingcall2taggedmember_gid," +
                                    " marketingcall_gid," +
                                    " taggedmember_gid," +
                                    " taggedmember_name," +
                                    " tagged_by," +
                                    " tagged_date," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + values.tagemployee_list[i].employee_gid + "'," +
                                    "'" + values.tagemployee_list[i].employee_name + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }



                        msSQL = " update mar_trn_tmarketingcall2taggedmember set taggeduser_flag='Y'" +
                                   " where marketingcall_gid = '" + msGetGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                    " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select group_concat(taggedmember_gid) as taguser  from mar_trn_tmarketingcall2taggedmember where marketingcall_gid = '" + msGetGid + "'";
                        ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taguser.Replace(",", "', '") + "')";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                        "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket FollowUp  ";
                        body = "Hi " + HttpUtility.HtmlEncode(values.assignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been FollowUp.<br><br>";
                        body = body + "Lead Name:" + HttpUtility.HtmlEncode(values.caller_name.Replace("'", "")) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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

                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;

                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Follow Up'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }


                if (values.callclosure_status == "Closed")

                {
                    msSQL = " update mar_trn_tmarketingcall set closed_remarks='" + values.closed_remarks.Replace("'", "") + "'," +
                            " closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " closed_flag = 'Y'," +
                            " closed_by = '" + employee_gid + "'" +
                            " where marketingcall_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                    msSQL = "Insert into mar_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " marketingcall_gid," +
                               " status," +
                               " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + msGetGid + "'," +
                               "' Closed'," +
                               "'" + employee_gid + "'," +
                               "'" + values.closed_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.tagemployee_list != null)
                    {

                        for (var i = 0; i < values.tagemployee_list.Count; i++)
                        {
                            msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                            msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                    " marketingcall2taggedmember_gid," +
                                    " marketingcall_gid," +
                                    " taggedmember_gid," +
                                    " taggedmember_name," +
                                    " tagged_by," +
                                    " tagged_date," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + values.tagemployee_list[i].employee_gid + "'," +
                                    "'" + values.tagemployee_list[i].employee_name + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                        msSQL = " update mar_trn_tmarketingcall2taggedmember set taggeduser_flag='Y'" +
                                   " where marketingcall_gid = '" + msGetGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                    " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select group_concat(taggedmember_gid) as taguser  from mar_trn_tmarketingcall2taggedmember where marketingcall_gid = '" + msGetGid + "'";
                        ls_taguser = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taguser.Replace(",", "', '") + "')";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                        "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket Closed ";
                        body = "Hi " + HttpUtility.HtmlEncode(values.assignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Closed.<br><br>";
                        body = body + "Lead Name:" + HttpUtility.HtmlEncode(values.caller_name.Replace("'", "")) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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

                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;
                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Close'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                }
                if (values.callclosure_status == "Assign")
                {
                    msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                    msSQL = "Insert into mar_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " marketingcall_gid," +
                               " status," +
                               " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + msGetGid + "'," +
                               "' Assign'," +
                                "'" + values.assignemployee_gid + "'," +
                               "'" + values.assignclosure_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.tagemployee_list == null)
                    {
                    }
                    else
                    {
                        for (var i = 0; i < values.tagemployee_list.Count; i++)
                        {
                            msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                            msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                    " marketingcall2taggedmember_gid," +
                                    " marketingcall_gid," +
                                    " taggedmember_gid," +
                                    " taggedmember_name," +
                                    " tagged_by," +
                                    " tagged_date," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + values.tagemployee_list[i].employee_gid + "'," +
                                    "'" + values.tagemployee_list[i].employee_name + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        msSQL = " update mar_trn_tmarketingcall2taggedmember set taggeduser_flag='Y'" +
                                       " where marketingcall_gid = '" + msGetGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                             " where marketingcall_gid ='" + msGetGid + "'";
                        ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select  group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taguser.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);
                    }

                    //Mail Start
                    int count = 0;
                    if (values.tagemployee_list != null)
                    {
                        count = values.tagemployee_list.Count;
                    }

                    //try
                    //{
                    //msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + employee_gid + "'";
                    //cc_mailid = objdbconn.GetExecuteScalar(msSQL);
                    //cc_mailid += ",";
                    //msSQL = "select employeereporting_to from adm_mst_tmodule2employee where employee_gid = '" + values.assignemployee_gid + "' and module_gid = 'ITS'";
                    //employee_reporting_to = objdbconn.GetExecuteScalar(msSQL);
                    //msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + employee_reporting_to + "'";
                    //cc_mailid += objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.assignemployee_gid + "'";
                    //tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    //for (var i = 0; i < count; i++)
                    //{
                    //    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.tagemployee_list[i].employee_gid + "'";
                    //    cc_mailid += ",";
                    //    cc_mailid += objdbconn.GetExecuteScalar(msSQL);
                    //}

                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.assignemployee_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                         " FROM adm_mst_tcompany";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                    "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                    "where b.employee_gid ='" + employee_gid + "'";
                    employeename = objdbconn.GetExecuteScalar(msSQL);

                    sub = " A Lead Ticket Assigned ";
                    body = "Hi " + HttpUtility.HtmlEncode(values.assignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been assigned to you.<br><br>";
                    body = body + "Lead Name:" + HttpUtility.HtmlEncode(values.caller_name.Replace("'", "")) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Assigned by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                    body = body + "Assigned time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Business development - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                    if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                    {
                        lstoReceipients = tomail_id.Split(',');
                        if (tomail_id.Length == 0)
                        {
                            message.To.Add(new MailAddress(tomail_id));
                        }
                        else
                        {
                            foreach (string ToEmail in lstoReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                           " marketingcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + tomail_id + "'," +
                           "'" + cc_mailid + "'," +
                           "'Assign'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                //    }

                //catch (Exception  ex)
                //{
                //    values.message = ex.ToString();
                //    values.status = false;
                //}
                //Mail Ends

                //Updates

                //msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //values.status = true;
                //values.message = "Marketing Call Details Submitted Sucessfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }


        public void DaMarketingCallSubmit(string employee_gid, MdlMarketingCall values)
        {

            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            DateTime lsmailalert = datevalue;
            //DateTime lsmailalert = datevalue.AddHours(Convert.ToDouble(values.tat_hours));

            msSQL = "select marketingcall_gid  from mar_trn_tmarketingcall2mobileno where marketingcall_gid ='" + employee_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number/Add Atleast One Primary Status";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select marketingcall_gid  from mar_trn_tmarketingcall2email where marketingcall_gid ='" + employee_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email ID /Add Atleast One Primary Status";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select marketingcall_gid from mar_trn_tmarketingcall2address where marketingcall_gid ='" + employee_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Address Details";
                return;
            }
            objODBCDatareader.Close();
            if (values.callclosure_status == "Follow Up")
                {
                    msSQL = "select marketingcall_gid from mar_trn_tmarketingcall2followup where marketingcall_gid ='" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        values.status = false;
                        values.message = "Add Atleast Follow Up Date/Follow Up Time";
                        return;
                    }
                objODBCDatareader.Close();

                    msGetGid = objcmnfunctions.GetMasterGID("BDCA");

                    msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + values.entity_gid + "'";
                    string lsentity_code = objdbconn.GetExecuteScalar(msSQL);

                    string msGETRef = objcmnfunctions.GetMasterGID("BDC");
                    msGETRef = msGETRef.Replace("BDC", "");
                    string off_code = "OF";

                    string lsticket_refid = "BDC" + DateTime.Now.ToString("ddMMyyyy") + off_code + msGETRef;

                    msSQL = " insert into mar_trn_tmarketingcall(" +
                         " marketingcall_gid," +
                         " ticket_refid," +
                         " entity_gid," +
                         " entity_name," +
                         " marketingsourceofcontact_gid," +
                         " marketingsourceofcontact_name," +
                         " leadrequesttype_gid," +
                         " leadrequesttype_name," +
                         " caller_name," +
                         " internalreference_gid," +
                          " callreceived_date," +
                         " internalreference_name," +
                         " callerassociate_company," +
                           " assignclosure_remarks," +
                         " closed_remarks," +
                          " mail_alert," +
                         " office_landlineno," +
                         " marketingcalltype_gid," +
                         " marketingcalltype_name," +
                         " enquiry_description," +
                         " callclosure_status," +
                         "callassign_status," +
                         " assignemployee_gid," +
                         " assignemployee_name," +
                         " tat_hours," +
                         " assign_by," +
                         " assign_date," +
                         " baselocation_name," +
                         " marketingcall_status," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + lsticket_refid + "'," +
                         "'" + values.entity_gid + "'," +
                         "'" + values.entity_name + "'," +
                         "'" + values.marketingsourceofcontact_gid + "'," +
                         "'" + values.marketingsourceofcontact_name + "'," +
                         "'" + values.leadrequesttype_gid + "'," +
                         "'" + values.leadrequesttype_name + "'," +
                    "'" + values.caller_name.Replace("'", "") + "'," +
                         "'" + values.internalreference_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "'" + values.internalreference_name + "',";

                    if (values.callerassociate_company == "" || values.callerassociate_company == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.callerassociate_company.Replace("'", "") + "',";
                    }
                    if (values.assignclosure_remarks == "" || values.assignclosure_remarks == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.assignclosure_remarks.Replace("'", "") + "',";
                    }
                    if (values.closed_remarks == "" || values.closed_remarks == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.closed_remarks.Replace("'", "") + "',";
                    }
                    if (lsmailalert == null)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsmailalert).ToString("yyyy-MM-dd HH:mm") + "',";
                    }
                    msSQL += "'" + values.office_landlineno + "'," +
                             "'" + values.marketingcalltype_gid + "'," +
                             "'" + values.marketingcalltype_name + "'," +
                             "'" + values.enquiry_description.Replace("'", "") + "'," +
                             "'" + values.callclosure_status + "'," +
                             "'" + values.callclosure_status + "'," +
                             "'" + values.assignemployee_gid + "'," +
                             "'" + values.assignemployee_name + "'," +
                             "'" + values.tat_hours + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "'" + values.baselocation_name + "'," +
                             "'Converted'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update mar_trn_tmarketingcall set followup_remarks = '" + values.assignclosure_remarks.Replace("'", "") + "'," +
                            " followup_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " followup_by = '" + employee_gid + "'" +
                            " where marketingcall_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Lead Details Submitted Successfully";

                    msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                    msSQL = "Insert into mar_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " marketingcall_gid," +
                               " status," +
                               " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + msGetGid + "'," +
                               "' Follow Up'," +
                               "'" + employee_gid + "'," +
                               "'" + values.assignclosure_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.tagemployee_list != null)
                    {

                        for (var i = 0; i < values.tagemployee_list.Count; i++)
                        {
                            msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                            msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                    " marketingcall2taggedmember_gid," +
                                    " marketingcall_gid," +
                                    " taggedmember_gid," +
                                    " taggedmember_name," +
                                    " tagged_by," +
                                    " tagged_date," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + values.tagemployee_list[i].employee_gid + "'," +
                                    "'" + values.tagemployee_list[i].employee_name + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }



                        msSQL = " update mar_trn_tmarketingcall2taggedmember set taggeduser_flag='Y'" +
                                   " where marketingcall_gid = '" + msGetGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                    " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select group_concat(taggedmember_gid) as taguser  from mar_trn_tmarketingcall2taggedmember where marketingcall_gid = '" + msGetGid + "'";
                        ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taguser.Replace(",", "', '") + "')";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                        "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket FollowUp  ";
                        body = "Hi " + HttpUtility.HtmlEncode(values.assignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been FollowUp.<br><br>";
                        body = body + "Lead Name:" + HttpUtility.HtmlEncode(values.caller_name.Replace("'", "")) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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

                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;

                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Follow Up'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }


                if (values.callclosure_status == "Rejected")

                {
                    msGetGid = objcmnfunctions.GetMasterGID("BDCA");

                    msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + values.entity_gid + "'";
                    string lsentity_code = objdbconn.GetExecuteScalar(msSQL);

                    string msGETRef = objcmnfunctions.GetMasterGID("BDC");
                    msGETRef = msGETRef.Replace("BDC", "");
                    string off_code = "OF";

                    string lsticket_refid = "BDC" + DateTime.Now.ToString("ddMMyyyy") + off_code + msGETRef;

                    msSQL = " insert into mar_trn_tmarketingcall(" +
                         " marketingcall_gid," +
                         " ticket_refid," +
                         " entity_gid," +
                         " entity_name," +
                         " marketingsourceofcontact_gid," +
                         " marketingsourceofcontact_name," +
                         " leadrequesttype_gid," +
                         " leadrequesttype_name," +
                         " caller_name," +
                         " internalreference_gid," +
                          " callreceived_date," +
                         " internalreference_name," +
                         " callerassociate_company," +
                           " assignclosure_remarks," +
                         " closed_remarks," +
                          " mail_alert," +
                         " office_landlineno," +
                         " marketingcalltype_gid," +
                         " marketingcalltype_name," +
                         " enquiry_description," +
                         " callclosure_status," +
                         "callassign_status," +
                         " assignemployee_gid," +
                         " assignemployee_name," +
                         " tat_hours," +
                         " assign_by," +
                         " assign_date," +
                         " baselocation_name," +
                         " marketingcall_status," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + lsticket_refid + "'," +
                         "'" + values.entity_gid + "'," +
                         "'" + values.entity_name + "'," +
                         "'" + values.marketingsourceofcontact_gid + "'," +
                         "'" + values.marketingsourceofcontact_name + "'," +
                         "'" + values.leadrequesttype_gid + "'," +
                         "'" + values.leadrequesttype_name + "'," +
                    "'" + values.caller_name.Replace("'", "") + "'," +
                         "'" + values.internalreference_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "'" + values.internalreference_name + "',";

                    if (values.callerassociate_company == "" || values.callerassociate_company == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.callerassociate_company.Replace("'", "") + "',";
                    }
                    if (values.assignclosure_remarks == "" || values.assignclosure_remarks == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.assignclosure_remarks.Replace("'", "") + "',";
                    }
                    if (values.closed_remarks == "" || values.closed_remarks == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.closed_remarks.Replace("'", "") + "',";
                    }
                    if (lsmailalert == null)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsmailalert).ToString("yyyy-MM-dd HH:mm") + "',";
                    }
                    msSQL += "'" + values.office_landlineno + "'," +
                             "'" + values.marketingcalltype_gid + "'," +
                             "'" + values.marketingcalltype_name + "'," +
                             "'" + values.enquiry_description.Replace("'", "") + "'," +
                             "'" + values.callclosure_status + "'," +
                             "'" + values.callclosure_status + "'," +
                             "'" + values.assignemployee_gid + "'," +
                             "'" + values.assignemployee_name + "'," +
                             "'" + values.tat_hours + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "'" + values.baselocation_name + "'," +
                             "'Converted'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " update mar_trn_tmarketingcall set rejected_remarks='" + values.closed_remarks.Replace("'", "") + "'," +
                            " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " rejected_flag = 'Y'," +
                            " rejected_by = '" + employee_gid + "'" +
                            " where marketingcall_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Lead Details Submitted Successfully";


                    msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                    msSQL = "Insert into mar_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " marketingcall_gid," +
                               " status," +
                               " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + msGetGid + "'," +
                               "' Closed'," +
                               "'" + employee_gid + "'," +
                               "'" + values.closed_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.tagemployee_list != null)
                    {

                        for (var i = 0; i < values.tagemployee_list.Count; i++)
                        {
                            msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                            msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                    " marketingcall2taggedmember_gid," +
                                    " marketingcall_gid," +
                                    " taggedmember_gid," +
                                    " taggedmember_name," +
                                    " tagged_by," +
                                    " tagged_date," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + values.tagemployee_list[i].employee_gid + "'," +
                                    "'" + values.tagemployee_list[i].employee_name + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                        msSQL = " update mar_trn_tmarketingcall2taggedmember set taggeduser_flag='Y'" +
                                   " where marketingcall_gid = '" + msGetGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                    " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select group_concat(taggedmember_gid) as taguser  from mar_trn_tmarketingcall2taggedmember where marketingcall_gid = '" + msGetGid + "'";
                        ls_taguser = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taguser.Replace(",", "', '") + "')";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                        "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket Rejected ";
                        body = "Hi " + HttpUtility.HtmlEncode(values.assignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Rejected.<br><br>";
                        body = body + "Lead Name:" + HttpUtility.HtmlEncode(values.caller_name.Replace("'", "")) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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

                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;
                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Close'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                }
                if (values.callclosure_status == "Assign")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BDCA");

                    msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + values.entity_gid + "'";
                    string lsentity_code = objdbconn.GetExecuteScalar(msSQL);

                    string msGETRef = objcmnfunctions.GetMasterGID("BDC");
                    msGETRef = msGETRef.Replace("BDC", "");
                    string off_code = "OF";

                    string lsticket_refid = "BDC" + DateTime.Now.ToString("ddMMyyyy") + off_code + msGETRef;

                    msSQL = " insert into mar_trn_tmarketingcall(" +
                         " marketingcall_gid," +
                         " ticket_refid," +
                         " entity_gid," +
                         " entity_name," +
                         " marketingsourceofcontact_gid," +
                         " marketingsourceofcontact_name," +
                         " leadrequesttype_gid," +
                         " leadrequesttype_name," +
                         " caller_name," +
                         " internalreference_gid," +
                          " callreceived_date," +
                         " internalreference_name," +
                         " callerassociate_company," +
                           " assignclosure_remarks," +
                         " closed_remarks," +
                          " mail_alert," +
                         " office_landlineno," +
                         " marketingcalltype_gid," +
                         " marketingcalltype_name," +
                         " enquiry_description," +
                         " callclosure_status," +
                         "callassign_status," +
                         " assignemployee_gid," +
                         " assignemployee_name," +
                         " tat_hours," +
                         " assign_by," +
                         " assign_date," +
                         " baselocation_name," +
                         " marketingcall_status," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + lsticket_refid + "'," +
                         "'" + values.entity_gid + "'," +
                         "'" + values.entity_name + "'," +
                         "'" + values.marketingsourceofcontact_gid + "'," +
                         "'" + values.marketingsourceofcontact_name + "'," +
                         "'" + values.leadrequesttype_gid + "'," +
                         "'" + values.leadrequesttype_name + "'," +
                    "'" + values.caller_name.Replace("'", "") + "'," +
                         "'" + values.internalreference_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "'" + values.internalreference_name + "',";

                    if (values.callerassociate_company == "" || values.callerassociate_company == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.callerassociate_company.Replace("'", "") + "',";
                    }
                    if (values.assignclosure_remarks == "" || values.assignclosure_remarks == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.assignclosure_remarks.Replace("'", "") + "',";
                    }
                    if (values.closed_remarks == "" || values.closed_remarks == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.closed_remarks.Replace("'", "") + "',";
                    }
                    if (lsmailalert == null)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsmailalert).ToString("yyyy-MM-dd HH:mm") + "',";
                    }
                    msSQL += "'" + values.office_landlineno + "'," +
                             "'" + values.marketingcalltype_gid + "'," +
                             "'" + values.marketingcalltype_name + "'," +
                             "'" + values.enquiry_description.Replace("'", "") + "'," +
                             "'" + values.callclosure_status + "'," +
                             "'" + values.callclosure_status + "'," +
                             "'" + values.assignemployee_gid + "'," +
                             "'" + values.assignemployee_name + "'," +
                             "'" + values.tat_hours + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "'" + values.baselocation_name + "'," +
                             "'Converted'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Lead Details Submitted Successfully";

                    msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                    msSQL = "Insert into mar_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " marketingcall_gid," +
                               " status," +
                               " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + msGetGid + "'," +
                               "' Assign'," +
                                "'" + values.assignemployee_gid + "'," +
                               "'" + values.assignclosure_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.tagemployee_list == null)
                    {
                    }
                    else
                    {
                        for (var i = 0; i < values.tagemployee_list.Count; i++)
                        {
                            msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                            msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                    " marketingcall2taggedmember_gid," +
                                    " marketingcall_gid," +
                                    " taggedmember_gid," +
                                    " taggedmember_name," +
                                    " tagged_by," +
                                    " tagged_date," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + values.tagemployee_list[i].employee_gid + "'," +
                                    "'" + values.tagemployee_list[i].employee_name + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        msSQL = " update mar_trn_tmarketingcall2taggedmember set taggeduser_flag='Y'" +
                                       " where marketingcall_gid = '" + msGetGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                             " where marketingcall_gid ='" + msGetGid + "'";
                        ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select  group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taguser.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);
                    }

                    //Mail Start
                    int count = 0;
                    if (values.tagemployee_list != null)
                    {
                        count = values.tagemployee_list.Count;
                    }

                    //try
                    //{
                    //msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + employee_gid + "'";
                    //cc_mailid = objdbconn.GetExecuteScalar(msSQL);
                    //cc_mailid += ",";
                    //msSQL = "select employeereporting_to from adm_mst_tmodule2employee where employee_gid = '" + values.assignemployee_gid + "' and module_gid = 'ITS'";
                    //employee_reporting_to = objdbconn.GetExecuteScalar(msSQL);
                    //msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + employee_reporting_to + "'";
                    //cc_mailid += objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.assignemployee_gid + "'";
                    //tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    //for (var i = 0; i < count; i++)
                    //{
                    //    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.tagemployee_list[i].employee_gid + "'";
                    //    cc_mailid += ",";
                    //    cc_mailid += objdbconn.GetExecuteScalar(msSQL);
                    //}

                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.assignemployee_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                         " FROM adm_mst_tcompany";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                    "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                    "where b.employee_gid ='" + employee_gid + "'";
                    employeename = objdbconn.GetExecuteScalar(msSQL);

                    sub = " A Lead Ticket Assigned ";
                    body = "Hi " + HttpUtility.HtmlEncode(values.assignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been assigned to you.<br><br>";
                    body = body + "Lead Name:" + HttpUtility.HtmlEncode(values.caller_name.Replace("'", "")) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Assigned by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                    body = body + "Assigned time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Business development - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                    if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                    {
                        lstoReceipients = tomail_id.Split(',');
                        if (tomail_id.Length == 0)
                        {
                            message.To.Add(new MailAddress(tomail_id));
                        }
                        else
                        {
                            foreach (string ToEmail in lstoReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                           " marketingcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + tomail_id + "'," +
                           "'" + cc_mailid + "'," +
                           "'Assign'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            //    }

            //catch (Exception  ex)
            //{
            //    values.message = ex.ToString();
            //    values.status = false;
            //}
            //Mail Ends

            //Updates

            //msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + msGetGid + "' where marketingcall_gid='" + employee_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //values.status = true;
            //values.message = "Marketing Call Details Submitted Sucessfully";
            if (mnResult != 0)
            {
                //msSQL = " update atm_trn_tmultipleauditee set auditcreation_gid='" + values.auditcreation_gid + "' where auditcreation_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);             
                values.status = true;
                values.message = " Lead Details Submitted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetMarketingLeadSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name, leadrequesttype_name, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,leadrequire_name," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.callclosure_status, " +                       
                         "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
      "   WHEN 'Enquiry' THEN 'Enquiry'  " +
      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Unassigned'" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                            origination = (dr_datarow["origination"].ToString())

                            
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaGetMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name, leadrequesttype_name, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " date_format(a.callreceived_date, '%d-%m-%Y %h:%i %p') as callreceived_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                        " if(callclosure_status = 'Work-In-Progress', callclosure_status , 'Yet-to-Acknowlege') as callclosure_status , " +
                         "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
     "   WHEN 'Enquiry' THEN 'Enquiry'  " +
      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where (a.callclosure_status = 'Assign' or a.callclosure_status = '' or a.callclosure_status = 'Work-In-Progress') and a.created_by = '" + employee_gid + "'" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaEditMarketingCall(string marketingcall_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " select ticket_refid,entity_gid,entity_name,marketingsourceofcontact_gid,leadrequesttype_gid,leadrequesttype_name,marketingsourceofcontact_name,marketingcallreceivednumber_gid,marketingcallreceivednumber_name," +
                        " caller_name,internalreference_gid,internalreference_name,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " callerassociate_company,office_landlineno,marketingcalltype_gid,baselocation_name,marketingcalltype_name,marketingfunction_gid,marketingfunction_name,function_remarks,tat_hours," +
                        " requirement,enquiry_description,callclosure_status,callassign_status,assignemployee_gid,assignemployee_name," +
                        " date_format(assign_date,'%d-%m-%Y %h:%i %p') as assign_date,date_format(callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date,assignclosure_remarks,your_name,industry,company_name,leadrequire_name,leadrequire_gid,milletrequire_name,milletrequire_gid,enquiryrequire_gid,enquiryrequire_name,startuprequire_gid,startuprequire_name,business_name,your_message,marketingcall_status, " +
                       "CASE origination " +
                            "   WHEN 'Online' THEN 'Online'  " +
                            "   WHEN 'Millet' THEN 'Millet'  " +
                                  "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " from mar_trn_tmarketingcall where marketingcall_gid='" + marketingcall_gid + "'";


                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                    values.entity_name = objODBCDatareader["entity_name"].ToString();
                    values.marketingsourceofcontact_gid = objODBCDatareader["marketingsourceofcontact_gid"].ToString();
                    values.marketingsourceofcontact_name = objODBCDatareader["marketingsourceofcontact_name"].ToString();
                    values.leadrequesttype_gid = objODBCDatareader["leadrequesttype_gid"].ToString();
                    values.leadrequesttype_name = objODBCDatareader["leadrequesttype_name"].ToString();
                    values.marketingcallreceivednumber_gid = objODBCDatareader["marketingcallreceivednumber_gid"].ToString();
                    values.marketingcallreceivednumber_name = objODBCDatareader["marketingcallreceivednumber_name"].ToString();
                    values.callreceived_date = objODBCDatareader["callreceived_date"].ToString();
                    values.caller_name = objODBCDatareader["caller_name"].ToString();
                    values.internalreference_gid = objODBCDatareader["internalreference_gid"].ToString();
                    values.internalreference_name = objODBCDatareader["internalreference_name"].ToString();
                    values.callerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                    values.office_landlineno = objODBCDatareader["office_landlineno"].ToString();
                    values.marketingcalltype_gid = objODBCDatareader["marketingcalltype_gid"].ToString();
                    values.marketingcalltype_name = objODBCDatareader["marketingcalltype_name"].ToString();
                    values.marketingfunction_gid = objODBCDatareader["marketingfunction_gid"].ToString();
                    values.marketingfunction_name = objODBCDatareader["marketingfunction_name"].ToString();
                    values.function_remarks = objODBCDatareader["function_remarks"].ToString();
                    values.requirement = objODBCDatareader["requirement"].ToString();
                    values.enquiry_description = objODBCDatareader["enquiry_description"].ToString();
                    values.callclosure_status = objODBCDatareader["callassign_status"].ToString();
                    values.assignemployee_gid = objODBCDatareader["assignemployee_gid"].ToString();
                    values.assignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                    values.tat_hours = objODBCDatareader["tat_hours"].ToString();
                    values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                    values.leadrequire_gid = objODBCDatareader["leadrequire_gid"].ToString();
                    values.leadrequire_name = objODBCDatareader["leadrequire_name"].ToString();
                    values.milletrequire_gid = objODBCDatareader["milletrequire_gid"].ToString();
                    values.milletrequire_name = objODBCDatareader["milletrequire_name"].ToString();
                    values.your_name = objODBCDatareader["your_name"].ToString();
                    values.industry_name = objODBCDatareader["industry"].ToString(); 
                    values.company_name = objODBCDatareader["company_name"].ToString();
                    values.message_name = objODBCDatareader["your_message"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.origination = objODBCDatareader["origination"].ToString();

                    values.enquiryrequire_gid = objODBCDatareader["enquiryrequire_gid"].ToString();
                    values.enquiryrequire_name = objODBCDatareader["enquiryrequire_name"].ToString();
                    values.startuprequire_gid = objODBCDatareader["startuprequire_gid"].ToString();
                    values.startuprequire_name = objODBCDatareader["startuprequire_name"].ToString();
                    values.business_name = objODBCDatareader["business_name"].ToString();


                    msSQL = "select taggedmember_gid,taggedmember_name from mar_trn_tmarketingcall2taggedmember where marketingcall_gid='" + marketingcall_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    values.tagemployee_list = dt_datatable.AsEnumerable().Select(row =>
               new tagemployee_list
               {
                   employee_gid = row["taggedmember_gid"].ToString(),
                   employee_name = row["taggedmember_name"].ToString()
               }).ToList();
                    dt_datatable.Dispose();

                    values.assignclosure_remarks = objODBCDatareader["assignclosure_remarks"].ToString();
                    values.marketingcall_status = objODBCDatareader["marketingcall_status"].ToString();
                }

                //Employee
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getemp_list = new List<emp_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getemp_list.Add(new emp_list
                        {
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.emp_list = getemp_list;
                }
                dt_datatable.Dispose();

                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaMarketingCallEditSave(MdlMarketingCall values, string employee_gid)
        {
            msSQL = " select ticket_refid,entity_gid,entity_name,marketingsourceofcontact_gid,leadrequesttype_gid,leadrequesttype_name,marketingsourceofcontact_name,marketingcallreceivednumber_gid,marketingcallreceivednumber_name," +
                        " callreceived_date,caller_name,internalreference_gid,internalreference_name," +
                        " callerassociate_company,office_landlineno,marketingcalltype_gid,marketingcalltype_name,marketingfunction_gid,marketingfunction_name,function_remarks,tat_hours," +
                        " requirement,enquiry_description,callclosure_status,assignemployee_gid,assignemployee_name," +
                        " assign_by,assign_date,transfer_by,baselocation_name,transfer_date,completed_by,completed_date," +
                        " assignclosure_remarks,marketingcall_status" +
                        " from mar_trn_tmarketingcall where marketingcall_gid='" + values.marketingcall_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                lsentity_gid = objODBCDatareader["entity_gid"].ToString();
                lsentity_name = objODBCDatareader["entity_name"].ToString();
                lssourceofcontact_gid = objODBCDatareader["marketingsourceofcontact_gid"].ToString();
                lssourceofcontact_name = objODBCDatareader["marketingsourceofcontact_name"].ToString();
                lsleadrequesttype_gid = objODBCDatareader["leadrequesttype_gid"].ToString();
                lsleadrequesttype_name = objODBCDatareader["leadrequesttype_name"].ToString();
                lscallreceivednumber_gid = objODBCDatareader["marketingcallreceivednumber_gid"].ToString();
                lscallreceivednumber_name = objODBCDatareader["marketingcallreceivednumber_name"].ToString();
                lscallreceived_date = objODBCDatareader["callreceived_date"].ToString();
                lscaller_name = objODBCDatareader["caller_name"].ToString();
                lsinternalreference_gid = objODBCDatareader["internalreference_gid"].ToString();
                lsinternalreference_name = objODBCDatareader["internalreference_name"].ToString();
                lscallerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                lsoffice_landlineno = objODBCDatareader["office_landlineno"].ToString();
                lscalltype_gid = objODBCDatareader["marketingcalltype_gid"].ToString();
                lscalltype_name = objODBCDatareader["marketingcalltype_name"].ToString();
                lsfunction_gid = objODBCDatareader["marketingfunction_gid"].ToString();
                lsfunction_name = objODBCDatareader["marketingfunction_name"].ToString();
                lsfunction_remarks = objODBCDatareader["function_remarks"].ToString();
                lsrequirement = objODBCDatareader["requirement"].ToString();
                lsenquiry_description = objODBCDatareader["enquiry_description"].ToString();
                lscallclosure_status = objODBCDatareader["callclosure_status"].ToString();
                lsassignemployee_gid = objODBCDatareader["assignemployee_gid"].ToString();
                lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                lstat_hours = objODBCDatareader["tat_hours"].ToString();
                lsbaselocation_name = objODBCDatareader["baselocation_name"].ToString();
                lsassign_by = objODBCDatareader["assign_by"].ToString();
                if (objODBCDatareader["assign_date"].ToString() == "")
                {
                }
                else
                {
                    lsassign_date = Convert.ToDateTime(objODBCDatareader["assign_date"]).ToString("dd-MM-yyyy");
                }
                lstransfer_by = objODBCDatareader["transfer_by"].ToString();
                if (objODBCDatareader["transfer_date"].ToString() == "")
                {
                }
                else
                {
                    lstransfer_date = Convert.ToDateTime(objODBCDatareader["transfer_date"]).ToString("dd-MM-yyyy");
                }
                lscompleted_by = objODBCDatareader["completed_by"].ToString();
                if (objODBCDatareader["completed_date"].ToString() == "")
                {
                }
                else
                {
                    lscompleted_date = Convert.ToDateTime(objODBCDatareader["completed_date"]).ToString("dd-MM-yyyy");
                }
                lsassignclosure_remarks = objODBCDatareader["assignclosure_remarks"].ToString();
                lsmarketingcall_status = objODBCDatareader["marketingcall_status"].ToString();

            }
            objODBCDatareader.Close();


            msSQL = " update mar_trn_tmarketingcall set " +
                      " entity_gid='" + values.entity_gid + "'," +
                      " entity_name='" + values.entity_name + "'," +
                      " marketingsourceofcontact_gid='" + values.marketingsourceofcontact_gid + "'," +
                      " marketingsourceofcontact_name='" + values.marketingsourceofcontact_name + "'," +
                       " leadrequesttype_gid='" + values.leadrequesttype_gid + "'," +
                      " leadrequesttype_name='" + values.leadrequesttype_name + "'," +
                      " marketingcallreceivednumber_gid='" + values.marketingcallreceivednumber_gid + "'," +
                      " marketingcallreceivednumber_name='" + values.marketingcallreceivednumber_name + "'," +
                      " callreceived_date='" + values.callreceived_date + "'," +
                      " caller_name='" + values.caller_name.Replace("'", "") + "'," +
                      " internalreference_gid='" + values.internalreference_gid + "'," +
                      " internalreference_name='" + values.internalreference_name + "',";

            if (values.callerassociate_company == "" || values.callerassociate_company == null)
            {

            }
            else
            {
                msSQL += " callerassociate_company='" + values.callerassociate_company.Replace("'", "") + "',";
            }


            msSQL += " office_landlineno='" + values.office_landlineno + "'," +
                      " marketingcalltype_gid='" + values.marketingcalltype_gid + "'," +
                      " marketingcalltype_name='" + values.marketingcalltype_name + "'," +
                      " marketingfunction_gid='" + values.marketingfunction_gid + "'," +
                      " marketingfunction_name='" + values.marketingfunction_name + "'," +                 
                      " function_remarks='" + values.function_remarks + "'," +
                      " requirement='" + values.requirement + "'," +
                      " enquiry_description='" + values.enquiry_description + "'," +
                      " callclosure_status='" + values.callclosure_status + "'," +
                      " assignemployee_gid='" + values.assignemployee_gid + "'," +
                      " assignemployee_name='" + values.assignemployee_name + "'," +
                      " tat_hours='" + values.tat_hours + "'," +
                      " baselocation_name='" + values.baselocation_name + "'," +
                      " assign_by='" + employee_gid + "'," +
                      " assign_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " assignclosure_remarks='" + values.assignclosure_remarks + "'," +
                      " marketingcall_status='" + "Incomplete" + "'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where marketingcall_gid='" + values.marketingcall_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "select taggedmember_gid,taggedmember_name from mar_trn_tmarketingcall2taggedmember where marketingcall_gid='" + values.marketingcall_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                List<tagemployee_list> existingtagemployee_list = new List<tagemployee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        existingtagemployee_list.Add(new tagemployee_list
                        {
                            employee_gid = dr_datarow["taggedmember_gid"].ToString(),
                            employee_name = dr_datarow["taggedmember_name"].ToString(),
                        });
                    }
                }

                for (var i = 0; i < values.tagemployee_list.Count; i++)
                {

                    if (existingtagemployee_list.Contains(values.tagemployee_list[i]) == false)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                        msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                " marketingcall2taggedmember_gid," +
                                " marketingcall_gid," +
                                " taggedmember_gid," +
                                " taggedmember_name," +
                                " tagged_by," +
                                " tagged_date," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.marketingcall_gid + "'," +
                                "'" + values.tagemployee_list[i].employee_gid + "'," +
                                "'" + values.tagemployee_list[i].employee_name + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                for (var i = 0; i < existingtagemployee_list.Count; i++)
                {
                    if (values.tagemployee_list.Contains(existingtagemployee_list[i]) == false)
                    {
                        msSQL = "select marketingcall2taggedmember_gid from mar_trn_tmarketingcall2taggedmember where taggedmember_gid='" + existingtagemployee_list[i].employee_gid + "' and marketingcall_gid = '" + values.marketingcall_gid + "'";
                        string lsmarketingcall2taggedmember_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "delete from mar_trn_tmarketingcall2taggedmember where marketingcall2taggedmember_gid='" + lsmarketingcall2taggedmember_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                //Update Log
                msGetGid = objcmnfunctions.GetMasterGID("BDUL");
                msSQL = " insert into mar_trn_tmarketingcallupdatelog(" +
                   " marketingcallupdatelog_gid," +
                   " marketingcall_gid," +
                   " ticket_refid," +
                   " entity_gid," +
                   " entity_name," +
                   " marketingsourceofcontact_gid," +
                   " marketingsourceofcontact_name," +
                   " marketingcallreceivednumber_gid," +
                   " marketingcallreceivednumber_name," +
                   " callreceived_date," +
                   " caller_name," +
                   " internalreference_gid," +
                   " internalreference_name," +
                   " callerassociate_company," +
                   " office_landlineno," +
                   " marketingcalltype_gid," +
                   " marketingcalltype_name," +
                   " marketingfunction_gid," +
                   " marketingfunction_name," +
                  " function_remarks," +
                   " requirement," +
                   " enquiry_description," +
                   " callclosure_status," +
                   " assignemployee_gid," +
                   " assignemployee_name," +
                   " tat_hours," +
                   " assign_by," +
                   " assign_date," +
                   " transfer_by," +
                   " transfer_date," +
                   " completed_by," +
                   " completed_date," +
                   " assignclosure_remarks," +
                   " marketingcall_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.marketingcall_gid + "'," +
                   "'" + lsticket_refid + "'," +
                   "'" + lsentity_gid + "'," +
                   "'" + lsentity_name + "'," +
                   "'" + lssourceofcontact_gid + "'," +
                   "'" + lssourceofcontact_name + "'," +
                   "'" + lscallreceivednumber_gid + "'," +
                   "'" + lscallreceivednumber_name + "'," +
                   "'" + lscallreceived_date + "'," +
                   "'" + lscaller_name + "'," +
                   "'" + lsinternalreference_gid + "'," +
                   "'" + lsinternalreference_name + "'," +
                   "'" + lscallerassociate_company + "'," +
                   "'" + lsoffice_landlineno + "'," +
                   "'" + lscalltype_gid + "'," +
                   "'" + lscalltype_name + "'," +
                   "'" + lsfunction_gid + "'," +
                   "'" + lsfunction_name + "'," +
                   "'" + lsfunction_remarks + "'," +
                   "'" + lsrequirement + "'," +
                   "'" + lsenquiry_description + "'," +
                   "'" + lscallclosure_status + "'," +
                   "'" + lsassignemployee_gid + "'," +
                   "'" + lsassignemployee_name + "'," +
                    "'" + lstat_hours + "'," +
                   "'" + lsassign_by + "'," +
                   "'" + lsassign_date + "'," +
                   "'" + lstransfer_by + "'," +
                   "'" + lstransfer_date + "'," +
                   "'" + lscompleted_by + "'," +
                   "'" + lscompleted_date + "'," +
                   "'" + lsassignclosure_remarks + "'," +
                   "'" + lsmarketingcall_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates

                msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Lead Details Saved Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaMarketingCallEditSubmit(MdlMarketingCall values, string employee_gid)
        {
            
            msSQL = " select ticket_refid,entity_name,marketingsourceofcontact_gid,marketingsourceofcontact_name,leadrequesttype_gid,leadrequesttype_name," +
                        " callreceived_date,caller_name,internalreference_gid,internalreference_name," +
                        " callerassociate_company,office_landlineno,marketingcalltype_gid,marketingcalltype_name,tat_hours," +
                        " enquiry_description,callclosure_status,assignemployee_gid,assignemployee_name," +
                        " assign_by,assign_date,transfer_by,transfer_date,baselocation_name,completed_by,completed_date," +
                        " assignclosure_remarks,marketingcall_status" +
                        " from mar_trn_tmarketingcall where marketingcall_gid='" + values.marketingcall_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                lsentity_name = objODBCDatareader["entity_name"].ToString();
                lssourceofcontact_gid = objODBCDatareader["marketingsourceofcontact_gid"].ToString();
                lssourceofcontact_name = objODBCDatareader["marketingsourceofcontact_name"].ToString();
                lsleadrequesttype_gid = objODBCDatareader["leadrequesttype_gid"].ToString();
                lsleadrequesttype_name = objODBCDatareader["leadrequesttype_name"].ToString();
                lscallreceived_date = objODBCDatareader["callreceived_date"].ToString();
                lscaller_name = objODBCDatareader["caller_name"].ToString();
                lsinternalreference_gid = objODBCDatareader["internalreference_gid"].ToString();
                lsinternalreference_name = objODBCDatareader["internalreference_name"].ToString();
                lscallerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                lsoffice_landlineno = objODBCDatareader["office_landlineno"].ToString();
                lscalltype_gid = objODBCDatareader["marketingcalltype_gid"].ToString();
                lscalltype_name = objODBCDatareader["marketingcalltype_name"].ToString();
                lsenquiry_description = objODBCDatareader["enquiry_description"].ToString();
                lscallclosure_status = objODBCDatareader["callclosure_status"].ToString();
                lsassignemployee_gid = objODBCDatareader["assignemployee_gid"].ToString();
                lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                lstat_hours = objODBCDatareader["tat_hours"].ToString();
                lsbaselocation_name = objODBCDatareader["baselocation_name"].ToString();
                lsassign_by = objODBCDatareader["assign_by"].ToString();
                if (objODBCDatareader["assign_date"].ToString() == "")
                {
                }
                else
                {
                    lsassign_date = Convert.ToDateTime(objODBCDatareader["assign_date"]).ToString("dd-MM-yyyy");
                }
                lstransfer_by = objODBCDatareader["transfer_by"].ToString();
                if (objODBCDatareader["transfer_date"].ToString() == "")
                {
                }
                else
                {
                    lstransfer_date = Convert.ToDateTime(objODBCDatareader["transfer_date"]).ToString("dd-MM-yyyy");
                }
                lscompleted_by = objODBCDatareader["completed_by"].ToString();
                if (objODBCDatareader["completed_date"].ToString() == "")
                {
                }
                else
                {
                    lscompleted_date = Convert.ToDateTime(objODBCDatareader["completed_date"]).ToString("dd-MM-yyyy");
                }
                lsassignclosure_remarks = objODBCDatareader["assignclosure_remarks"].ToString();
                lsmarketingcall_status = objODBCDatareader["marketingcall_status"].ToString();

            }
            objODBCDatareader.Close();


            msSQL = " update mar_trn_tmarketingcall set " +
                      " entity_name='" + values.entity_name + "'," +
                      " marketingsourceofcontact_gid='" + values.marketingsourceofcontact_gid + "'," +
                      " marketingsourceofcontact_name='" + values.marketingsourceofcontact_name + "'," +
                       " leadrequesttype_gid='" + values.leadrequesttype_gid + "'," +
                      " leadrequesttype_name='" + values.leadrequesttype_name + "'," +
                      //" callreceived_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " caller_name='" + values.caller_name.Replace("'", "") + "'," +
                      " internalreference_gid='" + values.internalreference_gid + "'," +
                      " internalreference_name='" + values.internalreference_name + "',";

            if (values.callerassociate_company == "" || values.callerassociate_company == null)
            {

            }
            else
            {
                msSQL += " callerassociate_company='" + values.callerassociate_company.Replace("'", "") + "',";
            }
            if (values.assignclosure_remarks == "" || values.assignclosure_remarks == null)
            {

            }
            else
            {
                msSQL += " assignclosure_remarks='" + values.assignclosure_remarks.Replace("'", "") + "',";
            }

            msSQL += " office_landlineno='" + values.office_landlineno + "'," +
                      " marketingcalltype_gid='" + values.marketingcalltype_gid + "'," +
                      " marketingcalltype_name='" + values.marketingcalltype_name + "'," +                    
                      " enquiry_description='" + values.enquiry_description + "'," +
                      " callclosure_status='" + values.callclosure_status + "'," +
                      " assignemployee_gid='" + values.assignemployee_gid + "'," +
                      " assignemployee_name='" + values.assignemployee_name + "'," +
                      " tat_hours='" + values.tat_hours + "'," +
                      " baselocation_name='" + values.baselocation_name + "'," +
                      " assign_by='" + employee_gid + "'," +
                      " assign_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      //" assignclosure_remarks='" + values.assignclosure_remarks + "'," +
                      " marketingcall_status='" + "Converted" + "'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where marketingcall_gid='" + values.marketingcall_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "select taggedmember_gid,taggedmember_name from mar_trn_tmarketingcall2taggedmember where marketingcall_gid='" + values.marketingcall_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                List<tagemployee_list> existingtagemployee_list = new List<tagemployee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        existingtagemployee_list.Add(new tagemployee_list
                        {
                            employee_gid = dr_datarow["taggedmember_gid"].ToString(),
                            employee_name = dr_datarow["taggedmember_name"].ToString(),
                        });
                    }
                }

                for (var i = 0; i < values.tagemployee_list.Count; i++)
                {

                    if (existingtagemployee_list.Contains(values.tagemployee_list[i]) == false)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                        msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                " marketingcall2taggedmember_gid," +
                                " marketingcall_gid," +
                                " taggedmember_gid," +
                                " taggedmember_name," +
                                " tagged_by," +
                                " tagged_date," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.marketingcall_gid + "'," +
                                "'" + values.tagemployee_list[i].employee_gid + "'," +
                                "'" + values.tagemployee_list[i].employee_name + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                for (var i = 0; i < existingtagemployee_list.Count; i++)
                {
                    if (values.tagemployee_list.Contains(existingtagemployee_list[i]) == false)
                    {
                        msSQL = "select marketingcall2taggedmember_gid from mar_trn_tmarketingcall2taggedmember where taggedmember_gid='" + existingtagemployee_list[i].employee_gid + "' and marketingcall_gid = '" + values.marketingcall_gid + "'";
                        string lsmarketingcall2taggedmember_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "delete from mar_trn_tmarketingcall2taggedmember where marketingcall2taggedmember_gid='" + lsmarketingcall2taggedmember_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }


                //UpdateLog
                msGetGid = objcmnfunctions.GetMasterGID("BDUL");
                msSQL = " insert into mar_trn_tmarketingcallupdatelog(" +
                   " marketingcallupdatelog_gid," +
                   " marketingcall_gid," +
                   " ticket_refid," +
                   " entity_gid," +
                   " entity_name," +
                   " marketingsourceofcontact_gid," +
                   " marketingsourceofcontact_name," +
                   " marketingcallreceivednumber_gid," +
                   " marketingcallreceivednumber_name," +
                   " callreceived_date," +
                   " caller_name," +
                   " internalreference_gid," +
                   " internalreference_name," +
                   " callerassociate_company," +
                   " office_landlineno," +
                   " marketingcalltype_gid," +
                   " marketingcalltype_name," +
                   " marketingfunction_gid," +
                   " marketingfunction_name," +
                   " function_remarks," +
                   " requirement," +
                   " enquiry_description," +
                   " callclosure_status," +
                   " assignemployee_gid," +
                   " assignemployee_name," +
                   " tat_hours," +
                   " assign_by," +
                   " assign_date," +
                   " transfer_by," +
                   " transfer_date," +
                   " completed_by," +
                   " completed_date," +
                   " assignclosure_remarks," +
                   " marketingcall_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.marketingcall_gid + "'," +
                   "'" + lsticket_refid + "'," +
                   "'" + lsentity_gid + "'," +
                   "'" + lsentity_name + "'," +
                   "'" + lssourceofcontact_gid + "'," +
                   "'" + lssourceofcontact_name + "'," +
                   "'" + lscallreceivednumber_gid + "'," +
                   "'" + lscallreceivednumber_name + "'," +
                   "'" + lscallreceived_date + "'," +
                   "'" + lscaller_name + "'," +
                   "'" + lsinternalreference_gid + "'," +
                   "'" + lsinternalreference_name + "'," +
                   "'" + lscallerassociate_company + "'," +
                   "'" + lsoffice_landlineno + "'," +
                   "'" + lscalltype_gid + "'," +
                   "'" + lscalltype_name + "'," +
                   "'" + lsfunction_gid + "'," +
                   "'" + lsfunction_name + "'," +
                    "'" + lsfunction_remarks + "'," +
                   "'" + lsrequirement + "'," +
                   "'" + lsenquiry_description + "'," +
                   "'" + lscallclosure_status + "'," +
                   "'" + lsassignemployee_gid + "'," +
                   "'" + lsassignemployee_name + "'," +
                    "'" + lstat_hours + "'," +
                   "'" + lsassign_by + "'," +
                   "'" + lsassign_date + "'," +
                   "'" + lstransfer_by + "'," +
                   "'" + lstransfer_date + "'," +
                   "'" + lscompleted_by + "'," +
                   "'" + lscompleted_date + "'," +
                   "'" + lsassignclosure_remarks + "'," +
                   "'" + lsmarketingcall_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates
                msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Lead Details Submitted Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaMarketingCallFormUpdate(MdlMarketingCall values, string employee_gid)
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            //DateTime lsmailalert = datevalue.AddHours(Convert.ToDouble(values.tat_hours));
            DateTime lsmailalert = datevalue;

            msSQL = " update mar_trn_tmarketingcall set " +
                      " entity_gid='" + values.entity_gid + "'," +
                      " entity_name='" + values.entity_name + "'," +
                      " marketingsourceofcontact_gid='" + values.marketingsourceofcontact_gid + "'," +
                      " marketingsourceofcontact_name='" + values.marketingsourceofcontact_name + "'," +
                         " leadrequesttype_gid='" + values.leadrequesttype_gid + "'," +
                      " leadrequesttype_name='" + values.leadrequesttype_name + "'," +
                    
                      " callreceived_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " caller_name='" + values.caller_name.Replace("'", "") + "'," +
                      " internalreference_gid='" + values.internalreference_gid + "'," +
                      " internalreference_name='" + values.internalreference_name + "',";

            if (values.callerassociate_company == "" || values.callerassociate_company == null)
            {

            }
            else
            {
                msSQL += " callerassociate_company='" + values.callerassociate_company.Replace("'", "") + "',";
            }
            if (values.assignclosure_remarks == "" || values.assignclosure_remarks == null)
            {

            }
            else
            {
                msSQL += " assignclosure_remarks='" + values.assignclosure_remarks.Replace("'", "") + "',";
            }
            if (values.closed_remarks == "" || values.closed_remarks == null)
            {

            }
            else
            {
                msSQL += " closed_remarks='" + values.closed_remarks.Replace("'", "") + "',";
            }
            if (values.leadrequire_name == "" || values.leadrequire_name == null)
            {

            }
            else
            {
                msSQL += " leadrequire_name='" + values.leadrequire_name.Replace("'", "") + "',";
            }
            if (values.milletrequire_name == "" || values.milletrequire_name == null)
            {

            }
            else
            {
                msSQL += " milletrequire_name='" + values.milletrequire_name.Replace("'", "") + "',";
            }
            if (values.enquiryrequire_name == "" || values.enquiryrequire_name == null)
            {

            }
            else
            {
                msSQL += " enquiryrequire_name='" + values.enquiryrequire_name.Replace("'", "") + "',";
            }
            if (values.startuprequire_name == "" || values.startuprequire_name == null)
            {

            }
            else
            {
                msSQL += " startuprequire_name='" + values.startuprequire_name.Replace("'", "") + "',";
            }
            if (lsmailalert == null)
            {
                msSQL += " mail_alert=null,";
            }
            else
            {
                msSQL += " mail_alert='" + Convert.ToDateTime(lsmailalert).ToString("yyyy-MM-dd HH:mm") + "',";
            }
            //if (lsmailalert == null)
            //{
            //    msSQL += "null,";
            //}
            //else
            //{
            //    msSQL += "'" + Convert.ToDateTime(lsmailalert).ToString("yyyy-MM-dd HH:mm") + "',";
            //}
            msSQL += " office_landlineno='" + values.office_landlineno + "'," +
                      " marketingcalltype_gid='" + values.marketingcalltype_gid + "'," +
                      " marketingcalltype_name='" + values.marketingcalltype_name + "'," +                                                         
                      " callclosure_status='" + values.callclosure_status + "'," +
                      " assignemployee_gid='" + values.assignemployee_gid + "'," +
                      " assignemployee_name='" + values.assignemployee_name + "'," +
                       " tat_hours='" + values.tat_hours + "'," +
                     " baselocation_name='" + values.baselocation_name + "'," +
                      " assign_by='" + employee_gid + "'," +
                      " assign_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      //" assignclosure_remarks='" + values.assignclosure_remarks + "'," +
                      //" closed_remarks='" + values.closed_remarks + "'," +
                         " leadrequire_gid='" + values.leadrequire_gid + "'," +
                       " milletrequire_gid='" + values.milletrequire_gid + "'," +
                        " enquiryrequire_gid='" + values.enquiryrequire_gid + "'," +
                      " startuprequire_gid='" + values.startuprequire_gid + "'," +
                      " your_name='" + values.your_name + "'," +
                       " industry='" + values.industry_name + "'," +
                        " enquiry_description='" + values.enquiry_description.Replace("'", "") + "'," + 
                     " company_name='" + values.company_name + "'," +
                      " business_name='" + values.business_name + "'," +
                      " your_message='" + values.message_name + "'," +
                      " marketingcall_status='" + "Converted" + "'," +
                      " created_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where marketingcall_gid='" + values.marketingcall_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                if (values.callclosure_status == "Follow Up")
                {

                    msSQL = "select marketingcall_gid from mar_trn_tmarketingcall2followup where marketingcall_gid ='" + employee_gid + "' or marketingcall_gid='" + values.marketingcall_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        values.status = false;
                        values.message = "Add atleast one follow up details";
                        return;
                    }
                    objODBCDatareader.Close();
                    msSQL = " update mar_trn_tmarketingcall set followup_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " followup_by = '" + employee_gid + "'" +
                            " where marketingcall_gid='" + values.marketingcall_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.callclosure_status == "Rejected")
                {
                    msSQL = " update mar_trn_tmarketingcall set rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " rejected_flag = 'Y'," +
                            " rejected_by = '" + employee_gid + "'" +
                            " where marketingcall_gid='" + values.marketingcall_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "select taggedmember_gid,taggedmember_name from mar_trn_tmarketingcall2taggedmember where marketingcall_gid='" + values.marketingcall_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                List<tagemployee_list> existingtagemployee_list = new List<tagemployee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        existingtagemployee_list.Add(new tagemployee_list
                        {
                            employee_gid = dr_datarow["taggedmember_gid"].ToString(),
                            employee_name = dr_datarow["taggedmember_name"].ToString(),
                        });
                    }
                }

                for (var i = 0; i < values.tagemployee_list.Count; i++)
                {

                    if (existingtagemployee_list.Contains(values.tagemployee_list[i]) == false)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                        msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                " marketingcall2taggedmember_gid," +
                                " marketingcall_gid," +
                                " taggedmember_gid," +
                                " taggedmember_name," +
                                " tagged_by," +
                                " tagged_date," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.marketingcall_gid + "'," +
                                "'" + values.tagemployee_list[i].employee_gid + "'," +
                                "'" + values.tagemployee_list[i].employee_name + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                for (var i = 0; i < existingtagemployee_list.Count; i++)
                {
                    if (values.tagemployee_list.Contains(existingtagemployee_list[i]) == false)
                    {
                        msSQL = "select marketingcall2taggedmember_gid from mar_trn_tmarketingcall2taggedmember where taggedmember_gid='" + existingtagemployee_list[i].employee_gid + "' and marketingcall_gid = '" + values.marketingcall_gid + "'";
                        string lsmarketingcall2taggedmember_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "delete from mar_trn_tmarketingcall2taggedmember where marketingcall2taggedmember_gid='" + lsmarketingcall2taggedmember_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                //UpdateLog
                msGetGid = objcmnfunctions.GetMasterGID("BDUL");
                msSQL = " insert into mar_trn_tmarketingcallupdatelog(" +
                   " marketingcallupdatelog_gid," +
                   " marketingcall_gid," +
                   " ticket_refid," +
                   " entity_gid," +
                   " entity_name," +
                   " marketingsourceofcontact_gid," +
                   " marketingsourceofcontact_name," +
                   " marketingcallreceivednumber_gid," +
                   " marketingcallreceivednumber_name," +
                   " callreceived_date," +
                   " caller_name," +
                   " internalreference_gid," +
                   " internalreference_name," +
                   " callerassociate_company," +
                   " office_landlineno," +
                   " marketingcalltype_gid," +
                   " marketingcalltype_name," +
                   " marketingfunction_gid," +
                   " marketingfunction_name," +
                   " function_remarks," +
                   " requirement," +
                   " enquiry_description," +
                   " callclosure_status," +
                   " assignemployee_gid," +
                   " assignemployee_name," +
                    " tat_hours," +
                   " assign_by," +
                   " assign_date," +
                   " transfer_by," +
                   " transfer_date," +
                   " completed_by," +
                   " completed_date," +
                   " assignclosure_remarks," +
                    " closed_remarks," +
                   " marketingcall_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.marketingcall_gid + "'," +
                   "'" + lsticket_refid + "'," +
                   "'" + lsentity_gid + "'," +
                   "'" + lsentity_name + "'," +
                   "'" + lssourceofcontact_gid + "'," +
                   "'" + lssourceofcontact_name + "'," +
                   "'" + lscallreceivednumber_gid + "'," +
                   "'" + lscallreceivednumber_name + "'," +
                   "'" + lscallreceived_date + "'," +
                   "'" + lscaller_name + "'," +
                   "'" + lsinternalreference_gid + "'," +
                   "'" + lsinternalreference_name + "'," +
                   "'" + lscallerassociate_company + "'," +
                   "'" + lsoffice_landlineno + "'," +
                   "'" + lscalltype_gid + "'," +
                   "'" + lscalltype_name + "'," +
                   "'" + lsfunction_gid + "'," +
                   "'" + lsfunction_name + "'," +
                    "'" + lsfunction_remarks + "'," +
                   "'" + lsrequirement + "'," +
                   "'" + lsenquiry_description + "'," +
                   "'" + lscallclosure_status + "'," +
                   "'" + lsassignemployee_gid + "'," +
                   "'" + lsassignemployee_name + "'," +
                   "'" + lstat_hours + "'," +
                   "'" + lsassign_by + "'," +
                   "'" + lsassign_date + "'," +
                   "'" + lstransfer_by + "'," +
                   "'" + lstransfer_date + "'," +
                   "'" + lscompleted_by + "'," +
                   "'" + lscompleted_date + "'," +
                   "'" + lsassignclosure_remarks + "'," +
                    "'" + lsclosed_remarks + "'," +
                   "'" + lsmarketingcall_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates
                msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Lead Details Updated Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaMarketingCallEditUpdate(MdlMarketingCall values, string employee_gid)
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            DateTime lsmailalert = datevalue;
            //DateTime lsmailalert = datevalue.AddHours(Convert.ToDouble(values.tat_hours));

            msSQL = "select marketingcall_gid  from mar_trn_tmarketingcall2mobileno where marketingcall_gid ='" + employee_gid + "' or marketingcall_gid='" + values.marketingcall_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number/Add Atleast One Primary Status";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select marketingcall_gid  from mar_trn_tmarketingcall2email where marketingcall_gid ='" + employee_gid + "' or marketingcall_gid='" + values.marketingcall_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email ID / Add Atleast One Primary Status";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select marketingcall_gid from mar_trn_tmarketingcall2address where marketingcall_gid ='" + employee_gid + "' or marketingcall_gid='" + values.marketingcall_gid + "' and primary_status = 'Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Address Details/Add Atleast One Primary Status";
                return;
            }
            objODBCDatareader.Close();
            msSQL = " select ticket_refid,entity_gid,entity_name,marketingsourceofcontact_gid,marketingsourceofcontact_name,leadrequesttype_gid,leadrequesttype_name,marketingcallreceivednumber_gid,marketingcallreceivednumber_name," +
                  " callreceived_date,caller_name,internalreference_gid,internalreference_name," +
                  " callerassociate_company,office_landlineno,marketingcalltype_gid,marketingcalltype_name,marketingfunction_gid,marketingfunction_name,function_remarks,tat_hours," +
                  " requirement,enquiry_description,callclosure_status,assignemployee_gid,assignemployee_name," +
                  " assign_by,assign_date,transfer_by,transfer_date,baselocation_name,completed_by,completed_date," +
                  " assignclosure_remarks,closed_remarks,your_name,your_message,company_name,industry,leadrequire_gid,leadrequire_name,marketingcall_status" +
                  " from mar_trn_tmarketingcall where marketingcall_gid='" + values.marketingcall_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                lsentity_gid = objODBCDatareader["entity_gid"].ToString();
                lsentity_name = objODBCDatareader["entity_name"].ToString();
                lssourceofcontact_gid = objODBCDatareader["marketingsourceofcontact_gid"].ToString();
                lssourceofcontact_name = objODBCDatareader["marketingsourceofcontact_name"].ToString();
                lsleadrequesttype_gid = objODBCDatareader["leadrequesttype_gid"].ToString();
                lsleadrequesttype_name = objODBCDatareader["leadrequesttype_name"].ToString();
                lscallreceivednumber_gid = objODBCDatareader["marketingcallreceivednumber_gid"].ToString();
                lscallreceivednumber_name = objODBCDatareader["marketingcallreceivednumber_name"].ToString();
                lscallreceived_date = objODBCDatareader["callreceived_date"].ToString();
                lscaller_name = objODBCDatareader["caller_name"].ToString();
                lsinternalreference_gid = objODBCDatareader["internalreference_gid"].ToString();
                lsinternalreference_name = objODBCDatareader["internalreference_name"].ToString();
                lscallerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                lsoffice_landlineno = objODBCDatareader["office_landlineno"].ToString();
                lscalltype_gid = objODBCDatareader["marketingcalltype_gid"].ToString();
                lscalltype_name = objODBCDatareader["marketingcalltype_name"].ToString();
                lsfunction_gid = objODBCDatareader["marketingfunction_gid"].ToString();
                lsfunction_name = objODBCDatareader["marketingfunction_name"].ToString();
                lsfunction_remarks = objODBCDatareader["function_remarks"].ToString();
                lsrequirement = objODBCDatareader["requirement"].ToString();
                lsenquiry_description = objODBCDatareader["enquiry_description"].ToString();
                lscallclosure_status = objODBCDatareader["callclosure_status"].ToString();
                lsassignemployee_gid = objODBCDatareader["assignemployee_gid"].ToString();
                lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                lstat_hours = objODBCDatareader["tat_hours"].ToString();
                lsbaselocation_name = objODBCDatareader["baselocation_name"].ToString();
                lsassign_by = objODBCDatareader["assign_by"].ToString();
             
                if (objODBCDatareader["assign_date"].ToString() == "")
                {
                }
                else
                {
                    lsassign_date = Convert.ToDateTime(objODBCDatareader["assign_date"]).ToString("dd-MM-yyyy");
                }
                lstransfer_by = objODBCDatareader["transfer_by"].ToString();
                if (objODBCDatareader["transfer_date"].ToString() == "")
                {
                }
                else
                {
                    lstransfer_date = Convert.ToDateTime(objODBCDatareader["transfer_date"]).ToString("dd-MM-yyyy");
                }
                lscompleted_by = objODBCDatareader["completed_by"].ToString();
                if (objODBCDatareader["completed_date"].ToString() == "")
                {
                }
                else
                {
                    lscompleted_date = Convert.ToDateTime(objODBCDatareader["completed_date"]).ToString("dd-MM-yyyy");
                }
                lsassignclosure_remarks = objODBCDatareader["assignclosure_remarks"].ToString();
                lsclosed_remarks = objODBCDatareader["closed_remarks"].ToString();
                lsmarketingcall_status = objODBCDatareader["marketingcall_status"].ToString();

            }
            objODBCDatareader.Close();


            msSQL = " update mar_trn_tmarketingcall set " +
                      " entity_gid='" + values.entity_gid + "'," +
                      " entity_name='" + values.entity_name + "'," +
                      " marketingsourceofcontact_gid='" + values.marketingsourceofcontact_gid + "'," +
                      " marketingsourceofcontact_name='" + values.marketingsourceofcontact_name + "'," +
                         " leadrequesttype_gid='" + values.leadrequesttype_gid + "'," +
                      " leadrequesttype_name='" + values.leadrequesttype_name + "'," +
                      " marketingcallreceivednumber_gid='" + values.marketingcallreceivednumber_gid + "'," +
                      " marketingcallreceivednumber_name='" + values.marketingcallreceivednumber_name + "'," +
                      //" callreceived_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " caller_name='" + values.caller_name.Replace("'", "") + "'," +
                      " internalreference_gid='" + values.internalreference_gid + "'," +
                      " internalreference_name='" + values.internalreference_name + "',";

            if (values.callerassociate_company == "" || values.callerassociate_company == null)
            {

            }
            else
            {
                msSQL += " callerassociate_company='" + values.callerassociate_company.Replace("'", "") + "',";
            }
            if (values.assignclosure_remarks == "" || values.assignclosure_remarks == null)
            {

            }
            else
            {
                msSQL += " assignclosure_remarks='" + values.assignclosure_remarks.Replace("'", "") + "',";
            }
            if (values.closed_remarks == "" || values.closed_remarks == null)
            {

            }
            else
            {
                msSQL += " closed_remarks='" + values.closed_remarks.Replace("'", "") + "',";
            }
            if (values.leadrequire_name == "" || values.leadrequire_name == null)
            {

            }
            else
            {
                msSQL += " leadrequire_name='" + values.leadrequire_name.Replace("'", "") + "',";
            }
            if (values.milletrequire_name == "" || values.milletrequire_name == null)
            {

            }
            else
            {
                msSQL += " milletrequire_name='" + values.milletrequire_name.Replace("'", "") + "',";
            }
            if (values.enquiryrequire_name == "" || values.enquiryrequire_name == null)
            {

            }
            else
            {
                msSQL += " enquiryrequire_name='" + values.enquiryrequire_name.Replace("'", "") + "',";
            }
            if (values.startuprequire_name == "" || values.startuprequire_name == null)
            {

            }
            else
            {
                msSQL += " startuprequire_name='" + values.startuprequire_name.Replace("'", "") + "',";
            }
            if (lsmailalert == null)
            {
                msSQL += " mail_alert=null,";
            }
            else
            {
                msSQL += " mail_alert='" + Convert.ToDateTime(lsmailalert).ToString("yyyy-MM-dd HH:mm") + "',";
            }
            //if (lsmailalert == null)
            //{
            //    msSQL += "null,";
            //}
            //else
            //{
            //    msSQL += "'" + Convert.ToDateTime(lsmailalert).ToString("yyyy-MM-dd HH:mm") + "',";
            //}
            msSQL += " office_landlineno='" + values.office_landlineno + "'," +
                      " marketingcalltype_gid='" + values.marketingcalltype_gid + "'," +
                      " marketingcalltype_name='" + values.marketingcalltype_name + "'," +
                      " marketingfunction_gid='" + values.marketingfunction_gid + "'," +
                      " marketingfunction_name='" + values.marketingfunction_name + "'," +
                       " leadrequire_gid='" + values.leadrequire_gid + "'," +
                      " enquiryrequire_gid='" + values.enquiryrequire_gid + "'," +
                       " milletrequire_gid='" + values.milletrequire_gid + "'," +
                      " startuprequire_gid='" + values.startuprequire_gid + "'," +
                      " function_remarks='" + values.function_remarks + "'," +
                      " requirement='" + values.requirement.Replace("'", "") + "'," +
                      " enquiry_description='" + values.enquiry_description.Replace("'", "") + "'," +
                      " callclosure_status='" + values.callclosure_status + "'," +
                      " assignemployee_gid='" + values.assignemployee_gid + "'," +
                      " assignemployee_name='" + values.assignemployee_name + "'," +
                       " tat_hours='" + values.tat_hours + "'," +
                     " baselocation_name='" + values.baselocation_name + "'," +
                      " business_name='" + values.business_name + "'," +
                       " industry='" + values.industry_name + "'," +
                      " assign_by='" + employee_gid + "'," +
                      " assign_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      //" assignclosure_remarks='" + values.assignclosure_remarks.Replace("'", "") + "'," +
                      //" closed_remarks='" + values.closed_remarks.Replace("'", "") + "'," +                      
                      " marketingcall_status='" + "Converted" + "'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where marketingcall_gid='" + values.marketingcall_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                if (values.callclosure_status == "Follow Up")
                {
                    msSQL = "select marketingcall_gid from mar_trn_tmarketingcall2followup where marketingcall_gid ='" + employee_gid + "' or marketingcall_gid='" + values.marketingcall_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        values.status = false;
                        values.message = "Add atleast one follow up details";
                        return;
                    }
                    objODBCDatareader.Close();
                    msSQL = " update mar_trn_tmarketingcall set followup_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " followup_by = '" + employee_gid + "'" +
                            " where marketingcall_gid='" + values.marketingcall_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.callclosure_status == "Rejected")
                {
                    msSQL = " update mar_trn_tmarketingcall set rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " rejected_flag = 'Y'," +
                            " rejected_by = '" + employee_gid + "'" +
                            " where marketingcall_gid='" + values.marketingcall_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "select taggedmember_gid,taggedmember_name from mar_trn_tmarketingcall2taggedmember where marketingcall_gid='" + values.marketingcall_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                List<tagemployee_list> existingtagemployee_list = new List<tagemployee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        existingtagemployee_list.Add(new tagemployee_list
                        {
                            employee_gid = dr_datarow["taggedmember_gid"].ToString(),
                            employee_name = dr_datarow["taggedmember_name"].ToString(),
                        });
                    }
                }

                for (var i = 0; i < values.tagemployee_list.Count; i++)
                {

                    if (existingtagemployee_list.Contains(values.tagemployee_list[i]) == false)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("BDTM");
                        msSQL = " insert into mar_trn_tmarketingcall2taggedmember(" +
                                " marketingcall2taggedmember_gid," +
                                " marketingcall_gid," +
                                " taggedmember_gid," +
                                " taggedmember_name," +
                                " tagged_by," +
                                " tagged_date," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.marketingcall_gid + "'," +
                                "'" + values.tagemployee_list[i].employee_gid + "'," +
                                "'" + values.tagemployee_list[i].employee_name + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                for (var i = 0; i < existingtagemployee_list.Count; i++)
                {
                    if (values.tagemployee_list.Contains(existingtagemployee_list[i]) == false)
                    {
                        msSQL = "select marketingcall2taggedmember_gid from mar_trn_tmarketingcall2taggedmember where taggedmember_gid='" + existingtagemployee_list[i].employee_gid + "' and marketingcall_gid = '" + values.marketingcall_gid + "'";
                        string lsmarketingcall2taggedmember_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "delete from mar_trn_tmarketingcall2taggedmember where marketingcall2taggedmember_gid='" + lsmarketingcall2taggedmember_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                //UpdateLog
                msGetGid = objcmnfunctions.GetMasterGID("BDUL");
                msSQL = " insert into mar_trn_tmarketingcallupdatelog(" +
                   " marketingcallupdatelog_gid," +
                   " marketingcall_gid," +
                   " ticket_refid," +
                   " entity_gid," +
                   " entity_name," +
                   " marketingsourceofcontact_gid," +
                   " marketingsourceofcontact_name," +
                   " marketingcallreceivednumber_gid," +
                   " marketingcallreceivednumber_name," +
                   " callreceived_date," +
                   " caller_name," +
                   " internalreference_gid," +
                   " internalreference_name," +
                   " callerassociate_company," +
                   " office_landlineno," +
                   " marketingcalltype_gid," +
                   " marketingcalltype_name," +
                   " marketingfunction_gid," +
                   " marketingfunction_name," +
                   " function_remarks," +
                   " requirement," +
                   " enquiry_description," +
                   " callclosure_status," +
                   " assignemployee_gid," +
                   " assignemployee_name," +
                    " tat_hours," +
                   " assign_by," +
                   " assign_date," +
                   " transfer_by," +
                   " transfer_date," +
                   " completed_by," +
                   " completed_date," +
                   " assignclosure_remarks," +
                    " closed_remarks," +
                   " marketingcall_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.marketingcall_gid + "'," +
                   "'" + lsticket_refid + "'," +
                   "'" + lsentity_gid + "'," +
                   "'" + lsentity_name + "'," +
                   "'" + lssourceofcontact_gid + "'," +
                   "'" + lssourceofcontact_name + "'," +
                   "'" + lscallreceivednumber_gid + "'," +
                   "'" + lscallreceivednumber_name + "'," +
                   "'" + lscallreceived_date + "'," +
                   "'" + lscaller_name + "'," +
                   "'" + lsinternalreference_gid + "'," +
                   "'" + lsinternalreference_name + "'," +
                   "'" + lscallerassociate_company + "'," +
                   "'" + lsoffice_landlineno + "'," +
                   "'" + lscalltype_gid + "'," +
                   "'" + lscalltype_name + "'," +
                   "'" + lsfunction_gid + "'," +
                   "'" + lsfunction_name + "'," +
                    "'" + lsfunction_remarks + "'," +
                   "'" + lsrequirement + "'," +
                   "'" + lsenquiry_description + "'," +
                   "'" + lscallclosure_status + "'," +
                   "'" + lsassignemployee_gid + "'," +
                   "'" + lsassignemployee_name + "'," +
                   "'" + lstat_hours + "'," +
                   "'" + lsassign_by + "'," +
                   "'" + lsassign_date + "'," +
                   "'" + lstransfer_by + "'," +
                   "'" + lstransfer_date + "'," +
                   "'" + lscompleted_by + "'," +
                   "'" + lscompleted_date + "'," +
                   "'" + lsassignclosure_remarks + "'," +
                    "'" + lsclosed_remarks + "'," +
                   "'" + lsmarketingcall_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates
                msSQL = "update mar_trn_tmarketingcall2mobileno set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2email set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update mar_trn_tmarketingcall2address set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Lead Details Updated Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaMarketingCallTempClear(string employee_gid, result values)
        {
            msSQL = "delete from mar_trn_tmarketingcall2mobileno where marketingcall_gid='" + employee_gid + "' or length(marketingcall_gid) < 4 ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from mar_trn_tmarketingcall2email where marketingcall_gid='" + employee_gid + "'  or length(marketingcall_gid) < 4 ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from mar_trn_tmarketingcall2followup where marketingcall_gid='" + employee_gid + "' or length(marketingcall_gid) < 4 ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from mar_trn_tmarketingcall2address where marketingcall_gid='" + employee_gid + "' or length(marketingcall_gid) < 4 ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetCompletedMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, a.callclosure_status , " +
                        " CASE a.origination " +
                        "   WHEN 'Online' THEN 'Online'  " +
                        "   WHEN 'Millet' THEN 'Millet'  " +
                              "   WHEN 'Enquiry' THEN 'Enquiry'  " +

                        "   ELSE 'Offline' " +
                        " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Converted' and a.created_by = '" + employee_gid + "'" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            //created_by = (dr_datarow["created_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                            completed_by = (dr_datarow["completed_by"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetRejectedMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name, leadrequesttype_name,date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.callclosure_status, " +
                         "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status='Rejected' and a.created_by = '" + employee_gid + "'" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetFollowUpMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.followup_date,'%d-%m-%Y %h:%i %p') as followup_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                        " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as followup_by,  " +
                        "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.followup_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " where (a.callclosure_status = 'Follow Up' or a.callclosure_status = 'Extend Follow Up') and (a.created_by = '" + employee_gid + "')" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            followup_date = (dr_datarow["followup_date"].ToString()),
                            followup_by = (dr_datarow["followup_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetClosedMyleadsMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                        " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as rejected_by ," +
                        "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +

                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.rejected_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +                      
                       " where (a.callclosure_status = 'Rejected' and " +
                       "  a.rejected_by = '"+  employee_gid +"')" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            closed_date = (dr_datarow["rejected_date"].ToString()),
                            closed_by = (dr_datarow["rejected_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        //
        //
        public void DaGetClosedMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                        " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as rejected_by ,  " +
                         "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.rejected_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " where (a.callclosure_status = 'Rejected' and a.created_by = '" + employee_gid + "') " +                       
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            rejected_date = (dr_datarow["rejected_date"].ToString()),
                            rejected_by = (dr_datarow["rejected_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetMarketingCallAssignedView(string marketingcall_gid, MdlMarketingCallView values)
        {
            try
            {
                msSQL = " select ticket_refid,a.entity_name,marketingsourceofcontact_name,baselocation_name,leadrequire_name,milletrequire_name,enquiryrequire_name,startuprequire_name,business_name,loanproduct_name,loansubproduct_name,caller_name,leadrequesttype_name, internalreference_name, " +
                        " callerassociate_company,office_landlineno,marketingcalltype_name,industry,enquiry_description,loan_amount,callclosure_status,assignemployee_name," +
                        " tagemployee_name,assignclosure_remarks,closed_remarks,marketingcall_status, date_format(assign_date,'%d-%m-%Y %h:%i %p') as assign_date,completed_remarks,closed_remarks," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as closed_by," +
                        " date_format(completed_date,'%d-%m-%Y %h:%i %p') as completed_date,date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, date_format(closed_date,'%d-%m-%Y %h:%i %p') as closed_date, followup_remarks,tat_hours," +
                        " date_format(acknowledge_date,'%d-%m-%Y %h:%i %p') as acknowledge_date, date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%h:%i %p') as followup_time," +
                        " concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as followup_by,assigningclosure_remarks, rejected_remarks, " +
                        " concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as rejected_by, date_format(rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date, " +
                        "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
       "   WHEN 'Enquiry' THEN 'Enquiry'  " +
      "   ELSE 'Offline' " +
     " END as origination  " +
                        " from mar_trn_tmarketingcall a " +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.closed_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " left join hrm_mst_temployee f on a.followup_by = f.employee_gid" +
                        " left join adm_mst_tuser g on g.user_gid = f.user_gid" +
                        " left join hrm_mst_temployee h on a.rejected_by = h.employee_gid" +
                        " left join adm_mst_tuser i on i.user_gid = h.user_gid" +
                        " where marketingcall_gid='" + marketingcall_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    values.entity_name = objODBCDatareader["entity_name"].ToString();
                    values.sourceofcontact_name = objODBCDatareader["marketingsourceofcontact_name"].ToString();
                    values.callreceived_date = objODBCDatareader["callreceived_date"].ToString();
                    values.caller_name = objODBCDatareader["caller_name"].ToString();
                    values.leadrequesttype_name = objODBCDatareader["leadrequesttype_name"].ToString();
                    values.internalreference_name = objODBCDatareader["internalreference_name"].ToString();
                    values.callerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                    values.office_landlineno = objODBCDatareader["office_landlineno"].ToString();
                    values.calltype_name = objODBCDatareader["marketingcalltype_name"].ToString();
                    values.enquiry_description = objODBCDatareader["enquiry_description"].ToString();
                    values.callclosure_status = objODBCDatareader["callclosure_status"].ToString();
                    values.assignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                    values.tagemployee_name = objODBCDatareader["tagemployee_name"].ToString();
                    values.assignclosure_remarks = objODBCDatareader["assignclosure_remarks"].ToString();
                    values.assign_date = objODBCDatareader["assign_date"].ToString();
                    values.completed_by = objODBCDatareader["completed_by"].ToString();
                    values.closed_by = objODBCDatareader["closed_by"].ToString();
                    values.assigningclosure_remarks = objODBCDatareader["assigningclosure_remarks"].ToString();
                    values.closed = objODBCDatareader["completed_remarks"].ToString();
                    values.closed_remarks = objODBCDatareader["closed_remarks"].ToString();
                    values.completed_date = objODBCDatareader["completed_date"].ToString();
                    values.closed_date = objODBCDatareader["closed_date"].ToString();
                    values.followup_remarks = objODBCDatareader["followup_remarks"].ToString();
                    values.acknowledge_date = objODBCDatareader["acknowledge_date"].ToString();
                    values.followup_date = objODBCDatareader["followup_date"].ToString();
                    values.followup_time = objODBCDatareader["followup_time"].ToString();
                    values.followup_by = objODBCDatareader["followup_by"].ToString();
                    values.rejected_date = objODBCDatareader["rejected_date"].ToString();
                    values.rejected_by = objODBCDatareader["rejected_by"].ToString();
                    values.rejected_remarks = objODBCDatareader["rejected_remarks"].ToString();
                    values.tat_hours = objODBCDatareader["tat_hours"].ToString();
                    values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                    values.loanproduct_name = objODBCDatareader["loanproduct_name"].ToString();
                    values.loansubproduct_name = objODBCDatareader["loansubproduct_name"].ToString();                   
                    values.loan_amount = objODBCDatareader["loan_amount"].ToString();
                    values.origination = objODBCDatareader["origination"].ToString();
                    values.leadrequire_name = objODBCDatareader["leadrequire_name"].ToString();
                    values.milletrequire_name = objODBCDatareader["milletrequire_name"].ToString();
                    values.enquiryrequire_name = objODBCDatareader["enquiryrequire_name"].ToString();
                    values.startuprequire_name = objODBCDatareader["startuprequire_name"].ToString();
                    values.business_name = objODBCDatareader["business_name"].ToString();
                    values.industry_name = objODBCDatareader["industry"].ToString();

                }

                objODBCDatareader.Close();

                msSQL = "select mobile_no from mar_trn_tmarketingcall2mobileno where primary_status='Yes' and marketingcall_gid='" + marketingcall_gid + "'";
                values.primary_mobileno = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select mobile_no,whatsapp_status,sms_to from mar_trn_tmarketingcall2mobileno where marketingcall_gid='" + marketingcall_gid + "' and primary_status='Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCallmobileno_list = new List<MarketingCallmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCallmobileno_list.Add(new MarketingCallmobileno_list
                        {
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_status = (dr_datarow["whatsapp_status"].ToString()),
                            sms_to = (dr_datarow["sms_to"].ToString()),
                        });
                    }
                    values.MarketingCallmobileno_list = getMarketingCallmobileno_list;
                }
                dt_datatable.Dispose();

                msSQL = "select email_address from mar_trn_tmarketingcall2email where primary_status='Yes' and marketingcall_gid='" + marketingcall_gid + "'";
                values.primary_email = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select email_address from mar_trn_tmarketingcall2email where marketingcall_gid='" + marketingcall_gid + "' and primary_status='Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCallemail_list = new List<MarketingCallemail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCallemail_list.Add(new MarketingCallemail_list
                        {
                            email_address = (dr_datarow["email_address"].ToString()),
                        });
                    }
                    values.MarketingCallemail_list = getMarketingCallemail_list;
                }
                dt_datatable.Dispose();

                msSQL = "  select addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code, city,latitude,longitude from mar_trn_tmarketingcall2address where marketingcall_gid='" + marketingcall_gid + "' and primary_status = 'Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCalladdress_list = new List<MarketingCalladdress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCalladdress_list.Add(new MarketingCalladdress_list
                        {
                            addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            addressline1 = (dr_datarow["addressline1"].ToString()),
                            addressline2 = (dr_datarow["addressline2"].ToString()),
                            landmark = (dr_datarow["landmark"].ToString()),
                            taluka = (dr_datarow["taluka"].ToString()),
                            district = (dr_datarow["district"].ToString()),
                            state = (dr_datarow["state"].ToString()),
                            country = (dr_datarow["country"].ToString()),
                            latitude = (dr_datarow["latitude"].ToString()),
                            longitude = (dr_datarow["longitude"].ToString()),
                            postal_code = (dr_datarow["postal_code"].ToString()),
                            city = (dr_datarow["city"].ToString())
                        });
                    }
                    values.MarketingCalladdress_list = getMarketingCalladdress_list;
                }
                dt_datatable.Dispose();

                msSQL = "select date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%H:%i') as followup_time,followup_status,followup_remarks" +
               " from mar_trn_tmarketingcall2followup where " +
               " marketingcall_gid = '" + marketingcall_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCallfollowup_list = new List<MarketingCallfollowup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCallfollowup_list.Add(new MarketingCallfollowup_list
                        {
                            followup_date = (dr_datarow["followup_date"].ToString()),
                            followup_time = (dr_datarow["followup_time"].ToString()),
                            followup_status = (dr_datarow["followup_status"].ToString()),
                            followup_remarks = (dr_datarow["followup_remarks"].ToString()),
                        });
                    }
                }
                values.MarketingCallfollowup_list = getMarketingCallfollowup_list;
                dt_datatable.Dispose();

                msSQL = "select date_format(a.extendfollowup_date, '%d-%m-%Y') as followup_date,time_format(a.extendfollowup_time, '%H:%i') as followup_time ,a.extendfollowup_remarks," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as extendfollowup_by" +
                    " from mar_trn_tmarketingcall a " +
                " left join hrm_mst_temployee b on a.extendfollowup_by = b.employee_gid" +
                " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
               " where marketingcall_gid = '" + marketingcall_gid + "' and followup_date is not null";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmarketingcallextendfollowup_list = new List<marketingcallextendfollowup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmarketingcallextendfollowup_list.Add(new marketingcallextendfollowup_list
                        {
                            extendfollowup_date = (dr_datarow["followup_date"].ToString()),
                            extendfollowup_time = (dr_datarow["followup_time"].ToString()),
                            extendfollowup_remarks = (dr_datarow["extendfollowup_remarks"].ToString()),
                            extendfollowup_by = (dr_datarow["extendfollowup_by"].ToString()),
                        });
                    }
                }
                values.marketingcallextendfollowup_list = getmarketingcallextendfollowup_list;
                dt_datatable.Dispose();
                msSQL = " select taggedmember_name," +
                 " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as tagged_by,date_format(a.tagged_date,'%d-%m-%Y %h:%i %p') as tagged_date" +
                 " from mar_trn_tmarketingcall2taggedmember a" +
                 " left join hrm_mst_temployee b on b.employee_gid=a.tagged_by " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                 " where marketingcall_gid = '" + marketingcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCalltaggedmember_list = new List<MarketingCalltaggedmember_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCalltaggedmember_list.Add(new MarketingCalltaggedmember_list
                        {
                            taggedmember_name = (dr_datarow["taggedmember_name"].ToString()),
                            tagged_by = (dr_datarow["tagged_by"].ToString()),
                            tagged_date = (dr_datarow["tagged_date"].ToString()),
                        });
                    }
                    values.MarketingCalltaggedmember_list = getMarketingCalltaggedmember_list;
                }
                dt_datatable.Dispose();

                msSQL = " select transferfrom_name,transferto_name,a.transfer_remarks," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as transfer_by,date_format(a.transfer_date,'%d-%m-%Y %h:%i %p') as transfer_date" +
                " from mar_trn_tmarketingcalltransferlog a" +
                " left join hrm_mst_temployee b on b.employee_gid=a.transfer_by " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                " where marketingcall_gid = '" + marketingcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCalltransfer_list = new List<MarketingCalltransfer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCalltransfer_list.Add(new MarketingCalltransfer_list
                        {
                            transferfrom_name = (dr_datarow["transferfrom_name"].ToString()),
                            transferto_name = (dr_datarow["transferto_name"].ToString()),
                            transfer_by = (dr_datarow["transfer_by"].ToString()),
                            transfer_date = (dr_datarow["transfer_date"].ToString()),
                            transfer_remarks = (dr_datarow["transfer_remarks"].ToString())
                        });
                    }
                    values.MarketingCalltransfer_list = getMarketingCalltransfer_list;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        //Transfer

        public void DaMarketingCallDetailsForTransfer(string marketingcall_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " select marketingcall_gid,ticket_refid,assignemployee_gid,assignemployee_name from mar_trn_tmarketingcall where " +
                        " marketingcall_gid='" + marketingcall_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.marketingcall_gid = objODBCDatareader["marketingcall_gid"].ToString();
                    values.ticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    values.assignemployee_gid = objODBCDatareader["assignemployee_gid"].ToString();
                    values.assignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                }

                msSQL = " select transferfrom_name,transferto_name," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as transfer_by,date_format(a.transfer_date,'%d-%m-%Y %h:%i %p') as transfer_date" +
                " from mar_trn_tmarketingcalltransferlog a" +
                " left join hrm_mst_temployee b on b.employee_gid=a.transfer_by " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                " where marketingcall_gid = '" + marketingcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCalltransfer_list = new List<MarketingCalltransfer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCalltransfer_list.Add(new MarketingCalltransfer_list
                        {
                            transferfrom_name = (dr_datarow["transferfrom_name"].ToString()),
                            transferto_name = (dr_datarow["transferto_name"].ToString()),
                            transfer_by = (dr_datarow["transfer_by"].ToString()),
                            transfer_date = (dr_datarow["transfer_date"].ToString())
                        });
                    }
                    values.MarketingCalltransfer_list = getMarketingCalltransfer_list;
                }
                dt_datatable.Dispose();

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

        public void DaMarketingCallTransferEmployee(string employee_gid, MdlMarketingCallTransfer values)
        {
            try
            {
                msSQL = " update mar_trn_tmarketingcall set " +
                         " assignemployee_gid='" + values.transferto_gid + "'," +
                         " assignemployee_name='" + values.transferto_name + "'," +
                         " transfer_by='" + employee_gid + "'," +
                         " transfer_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where marketingcall_gid='" + values.marketingcall_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                employeename = objdbconn.GetExecuteScalar(msSQL);
                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BDTR");

                    msSQL = " insert into mar_trn_tmarketingcalltransferlog(" +
                  " marketingcalltransferlog_gid," +
                  " marketingcall_gid," +
                  " ticket_refid," +
                  " transferfrom_gid," +
                  " transferfrom_name," +
                  " transferto_gid," +
                  " transferto_name," +
                  " transfer_remarks," +
                  " transfer_by," +
                  " transfer_date," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.marketingcall_gid + "'," +
                  "'" + values.ticket_refid + "'," +
                  "'" + employee_gid + "'," +
                  "'" + employeename + "'," +
                  "'" + values.transferto_gid + "'," +
                  "'" + values.transferto_name + "'," +
                  "'" + values.transfer_remarks + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                    msSQL = "Insert into mar_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " marketingcall_gid," +
                               " status," +
                                " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + values.marketingcall_gid + "'," +
                               "'Transferred'," +
                               "'" + values.transferto_gid + "'," +
                               "'" + values.transfer_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    values.status = true;
                    values.message = "Lead Transferred Successfully";

                    msSQL = " SELECT taggeduser_flag from mar_trn_tmarketingcall2taggedmember" +
                                        " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                    ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                    if (ls_taguser == "Y")
                    {

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                            " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select a.ticket_refid, a.caller_name,a.created_by as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from mar_trn_tmarketingcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();


                        msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                                 " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                        ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket Transferred";
                        body = "Hi " + HttpUtility.HtmlEncode(values.transferto_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Transferred to you.<br><br>";
                        body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid)+ "<br><br>";
                        body = body + "Transfer By:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                        body = body + "Transfer time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;
                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.marketingcall_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Employee Transferred'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {

                        if (mnResult == 1)
                        {


                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                           " FROM adm_mst_tcompany";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = objODBCDatareader["pop_username"].ToString();
                                ls_password = objODBCDatareader["pop_password"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = " select a.ticket_refid, a.caller_name,a.assignemployee_gid as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                        " from mar_trn_tmarketingcall a " +
                                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                       " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                                lscaller_name = objODBCDatareader["caller_name"].ToString();
                                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                                lsto2members = objODBCDatareader["to2members"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.transferto_gid + "'";
                            tomail_id = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                           "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                           "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);

                            sub = " A Lead Ticket Transferred";
                            body = "Hi " + HttpUtility.HtmlEncode(values.transferto_name) + ",<br><br>";
                            body = body + "Greetings! <br><br>";
                            body = body + "A ticket has been Transferred to you.<br><br>";
                            body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                            body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                            body = body + "Transfer By:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                            body = body + "Transfer time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                            body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                            body = body + "Regards<br><br>";
                            body = body + "Business Development - Customer Service Helpline<br><br>";
                            body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                            if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                            {
                                lstoReceipients = tomail_id.Split(',');
                                if (tomail_id.Length == 0)
                                {
                                    message.To.Add(new MailAddress(tomail_id));
                                }
                                else
                                {
                                    foreach (string ToEmail in lstoReceipients)
                                    {
                                        message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);
                            values.status = true;
                            if (values.status == true)
                            {
                                msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                                   " marketingcall_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.marketingcall_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + tomail_id + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Employee Transferred'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }


        //Employee Side

        public void DaGetEmpAssignedMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.assign_date,'%d-%m-%Y %h:%i %p') as assign_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as assign_by ," +
                         "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +

                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Assign' and a.assignemployee_gid = '" + employee_gid + "'" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            assign_date = (dr_datarow["assign_date"].ToString()),
                            assign_by = (dr_datarow["assign_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEmpInProgressMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.assign_date,'%d-%m-%Y %h:%i %p') as assign_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as assign_by ," +
                       "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Work-In-Progress' and a.assignemployee_gid = '" + employee_gid + "'" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            assign_date = (dr_datarow["assign_date"].ToString()),
                            assign_by = (dr_datarow["assign_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEmpTaggedSummary(MdlMarketingCall values, string employee_gid)
        {
            try
            {
                msSQL = " SELECT a.marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as assign_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as assign_by ," +
                      "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join mar_trn_tmarketingcall2taggedmember e on e.marketingcall_gid = a.marketingcall_gid " +
                        " where e.taggedmember_gid = '" + employee_gid + "' order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            assign_date = (dr_datarow["assign_date"].ToString()),
                            assign_by = (dr_datarow["assign_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEmpTransferredMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.transfer_date,'%d-%m-%Y %h:%i %p') as transfer_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as transfer_by ," +
                      "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where (a.callclosure_status = 'Assign' or a.callclosure_status = 'Work-In-Progress') and a.transfer_by = '" + employee_gid + "'" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            transfer_date = (dr_datarow["transfer_date"].ToString()),
                            transfer_by = (dr_datarow["transfer_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEmpFollowUpMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.followup_date,'%d-%m-%Y') as followup_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as followup_by ," +
                       "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.followup_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where (a.callclosure_status = 'Follow Up' or a.callclosure_status = 'Extend Follow Up') and (a.assignemployee_gid = '" + employee_gid + "')" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            followup_date = (dr_datarow["followup_date"].ToString()),
                            followup_by = (dr_datarow["followup_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEmpCompletedMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date, date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, a.callclosure_status," +
                        " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as rejected_by ," +
                       "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.rejected_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " where (a.callclosure_status = 'Converted') and " +
                        " (a.completed_by = '" + employee_gid + "')" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                            completed_by = (dr_datarow["completed_by"].ToString()),
                            rejected_date = (dr_datarow["rejected_date"].ToString()),
                            rejected_by = (dr_datarow["rejected_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetEmpRejectedMarketingCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date, date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, a.callclosure_status," +
                        " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as rejected_by ," +
                       "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +
      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.rejected_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " where (a.callclosure_status = 'Rejected') and " +
                        " (a.rejected_by = '" + employee_gid + "')" +
                        " order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                            completed_by = (dr_datarow["completed_by"].ToString()),
                            rejected_date = (dr_datarow["rejected_date"].ToString()),
                            rejected_by = (dr_datarow["rejected_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaMarketingCallCount(string employee_gid, MarketingCallCount values)
        {
            msSQL = "select count(marketingcall_gid) as assignedcall_count from mar_trn_tmarketingcall a where a.created_by='" + employee_gid + "' and (a.callclosure_status = 'Assign' or a.callclosure_status = '' or a.callclosure_status = 'Work-In-Progress')";
            values.assignedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(marketingcall_gid) as completedcall_count from mar_trn_tmarketingcall a where a.created_by='" + employee_gid + "' and a.callclosure_status = 'Converted'";
            values.completedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(marketingcall_gid) as unassignedcall_count from mar_trn_tmarketingcall a where  a.callclosure_status = 'Unassigned'";
            values.unassignedcall_count = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " select count(marketingcall_gid) as rejectedcall_count from mar_trn_tmarketingcall a where a.created_by='" + employee_gid + "' and a.callclosure_status='Rejected'";
            values.rejectedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(marketingcall_gid) as closedcall_count from mar_trn_tmarketingcall a where (a.created_by='" + employee_gid + "' and a.callclosure_status='Rejected')";
            values.closedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(marketingcall_gid) as followupcall_count from mar_trn_tmarketingcall a where a.created_by='" + employee_gid + "' and (a.callclosure_status = 'Follow Up' or a.callclosure_status = 'Extend Follow Up')";
            values.followupcall_count = objdbconn.GetExecuteScalar(msSQL);

        }

        public void DaEmployeeMarketingCallCount(string employee_gid, MarketingCallCount values)
        {
            msSQL = "select count(marketingcall_gid) as assignedcall_count from mar_trn_tmarketingcall a where a.assignemployee_gid='" + employee_gid + "' and a.callclosure_status = 'Assign'";
            values.assignedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(marketingcall_gid) as assignedcall_count from mar_trn_tmarketingcall a where a.assignemployee_gid='" + employee_gid + "' and a.callclosure_status = 'Work-In-Progress'";
            values.inprogresscall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(a.marketingcall_gid) as tagged_count from mar_trn_tmarketingcall2taggedmember a " +
                    " left join mar_trn_tmarketingcall b on b.marketingcall_gid = a.marketingcall_gid where a.taggedmember_gid = '" + employee_gid + "'";
            values.taggedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(marketingcall_gid) as transfercall_count from mar_trn_tmarketingcall a where a.transfer_by='" + employee_gid + "' and" +
                    " (a.callclosure_status = 'Assign' or a.callclosure_status = 'Work-In-Progress')";
            values.transfercall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(marketingcall_gid) as completedcall_count from mar_trn_tmarketingcall a where (a.completed_by='" + employee_gid + "')" +
                    " and (a.callclosure_status = 'Converted')";
            values.completedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(marketingcall_gid) as rejectedcall_count from mar_trn_tmarketingcall a where (a.rejected_by='" + employee_gid + "')" +
                    " and (a.callclosure_status = 'Rejected')";
            values.rejectedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(marketingcall_gid) as followupcall_count from mar_trn_tmarketingcall a where a.assignemployee_gid='" + employee_gid + "' and (a.callclosure_status = 'Follow Up' or a.callclosure_status = 'Extend Follow Up')";
            values.followupcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(marketingcall_gid) as closedcall_count from mar_trn_tmarketingcall a where (a.callclosure_status = 'Rejected' and a.rejected_by = '" + employee_gid + "') ";
            values.closedcall_count = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaGetMarketingCallCompletedView(string marketingcall_gid, MdlMarketingCallcompleteView values)
        {
            try
            {
                msSQL = " select date_format(completed_date, '%d-%m-%Y') as completed_date, " +
                       " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as completed_by,completed_remarks  from mar_trn_tmarketingcall a" +
                       " left join hrm_mst_temployee b on a.completed_by = b.employee_gid " +
                       " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                       " where marketingcall_gid='" + marketingcall_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.completed_date = objODBCDatareader["completed_date"].ToString();
                    values.completed_by = objODBCDatareader["completed_by"].ToString();
                    values.completed_remarks = objODBCDatareader["completed_remarks"].ToString();
                }

                objODBCDatareader.Close();


            }
            catch
            {

            }
        }

        public void DaPostUpdateAck(string employee_gid, MdlMarketingCall values)
        {
            msSQL = " update mar_trn_tmarketingcall set callclosure_status='Work-In-Progress'," +
                    " acknowledge_by = '" + employee_gid + "'," +
                    " acknowledge_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where marketingcall_gid='" + values.marketingcall_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
            msSQL = "Insert into mar_trn_tstatuslog( " +
                       " statuslog_gid," +
                       " marketingcall_gid," +
                       " status," +
                        "overall_detail," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid2 + "'," +
                       "'" + values.marketingcall_gid + "'," +
                       "'Work-In-Progress'," +
                       "'" + employee_gid + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " SELECT taggeduser_flag from mar_trn_tmarketingcall2taggedmember" +
                                       " where marketingcall_gid ='" + values.marketingcall_gid + "'";
            ls_taguser = objdbconn.GetExecuteScalar(msSQL);

            if (ls_taguser == "Y")
            {

                values.status = true;
                values.message = "Acknowledgement Updated Successfully";

                msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                    " FROM adm_mst_tcompany";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select a.ticket_refid, a.caller_name,a.created_by as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                            " from mar_trn_tmarketingcall a " +
                             " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                           " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                    lscaller_name = objODBCDatareader["caller_name"].ToString();
                    lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    lsto2members = objODBCDatareader["to2members"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                         " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);

                sub = " A Lead Ticket Acknowledged";
                body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                body = body + "Greetings! <br><br>";
                body = body + "A ticket has been Acknowledged.<br><br>";
                body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                body = body + "Regards<br><br>";
                body = body + "Business Development - Customer Service Helpline<br><br>";
                body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(tomail_id));
                lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                {
                    lstoReceipients = tomail_id.Split(',');
                    if (tomail_id.Length == 0)
                    {
                        message.To.Add(new MailAddress(tomail_id));
                    }
                    else
                    {
                        foreach (string ToEmail in lstoReceipients)
                        {
                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                values.status = true;
                if (values.status == true)
                {
                    msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                       " marketingcall_gid," +
                       " from_mail," +
                       " to_mail," +
                       " cc_mail," +
                       " mail_status," +
                       " mail_senddate, " +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + values.marketingcall_gid + "'," +
                       "'" + employee_gid + "'," +
                       "'" + tomail_id + "'," +
                       "'" + cc_mailid + "'," +
                       "'Acknowledged'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            else
            {

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Acknowledgement Updated Successfully";

                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                        " FROM adm_mst_tcompany";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.ticket_refid, a.caller_name,(a.created_by) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " from mar_trn_tmarketingcall a " +
                                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                        lscaller_name = objODBCDatareader["caller_name"].ToString();
                        lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                        lsto2members = objODBCDatareader["to2members"].ToString();
                    }
                    objODBCDatareader.Close();



                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    sub = " A Lead Ticket Acknowledged";
                    body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been Acknowledged.<br><br>";
                    body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Business Development - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                    if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                    {
                        lstoReceipients = tomail_id.Split(',');
                        if (tomail_id.Length == 0)
                        {
                            message.To.Add(new MailAddress(tomail_id));
                        }
                        else
                        {
                            foreach (string ToEmail in lstoReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    values.status = true;
                    if (values.status == true)
                    {
                        msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                           " marketingcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.marketingcall_gid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + tomail_id + "'," +
                           "'" + cc_mailid + "'," +
                           "'Acknowledged'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                else
                {
                    values.status = false;
                    values.message = "Error Occured";
                }
            }
        }
        public void DaRejectMarketingCall(string employee_gid, MdlMarketingCall values)
        {
            msSQL = " update mar_trn_tmarketingcall set callclosure_status='Rejected', " +
                   " rejected_remarks='" + values.reject_remarks.Replace("'", "") + "'," +
                   " rejected_flag='Y'," +
                   " rejected_by='" + employee_gid + "'," +
                   " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where marketingcall_gid='" + values.marketingcall_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
            msSQL = "Insert into mar_trn_tstatuslog( " +
                       " statuslog_gid," +
                       " marketingcall_gid," +
                       " status," +
                        " overall_detail," +
                       " remarks," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid2 + "'," +
                       "'" + values.marketingcall_gid + "'," +
                       "'Rejected'," +
                        "'" + employee_gid + "'," +
                       "'" + values.reject_remarks.Replace("'", "") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " SELECT taggeduser_flag from mar_trn_tmarketingcall2taggedmember" +
                                       " where marketingcall_gid ='" + values.marketingcall_gid + "'";
            ls_taguser = objdbconn.GetExecuteScalar(msSQL);

            if (ls_taguser == "Y")
            {

                values.status = true;
                values.message = "Rejected Updated Successfully";

                msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                    " FROM adm_mst_tcompany";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select a.ticket_refid, a.caller_name,(a.created_by) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                            " from mar_trn_tmarketingcall a " +
                             " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                           " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                    lscaller_name = objODBCDatareader["caller_name"].ToString();
                    lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    lsto2members = objODBCDatareader["to2members"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                         " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);

                sub = " A Lead Ticket Rejected";
                body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                body = body + "Greetings! <br><br>";
                body = body + "A ticket has been Rejected.<br><br>";
                body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                body = body + "Regards<br><br>";
                body = body + "Business Development - Customer Service Helpline<br><br>";
                body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(tomail_id));
                lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                {
                    lstoReceipients = tomail_id.Split(',');
                    if (tomail_id.Length == 0)
                    {
                        message.To.Add(new MailAddress(tomail_id));
                    }
                    else
                    {
                        foreach (string ToEmail in lstoReceipients)
                        {
                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                values.status = true;
                if (values.status == true)
                {
                    msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                       " marketingcall_gid," +
                       " from_mail," +
                       " to_mail," +
                       " cc_mail," +
                       " mail_status," +
                       " mail_senddate, " +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + values.marketingcall_gid + "'," +
                       "'" + employee_gid + "'," +
                       "'" + tomail_id + "'," +
                       "'" + cc_mailid + "'," +
                       "'Rejected'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Lead Rejected Successfully..!";

                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                       " FROM adm_mst_tcompany";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.ticket_refid, a.caller_name,(a.created_by) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " from mar_trn_tmarketingcall a " +
                                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                        lscaller_name = objODBCDatareader["caller_name"].ToString();
                        lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                        lsto2members = objODBCDatareader["to2members"].ToString();
                    }
                    objODBCDatareader.Close();



                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    sub = " A Lead Ticket Rejected";
                    body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been Rejected.<br><br>";
                    body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Business Development - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                    if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                    {
                        lstoReceipients = tomail_id.Split(',');
                        if (tomail_id.Length == 0)
                        {
                            message.To.Add(new MailAddress(tomail_id));
                        }
                        else
                        {
                            foreach (string ToEmail in lstoReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    values.status = true;
                    if (values.status == true)
                    {
                        msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                           " marketingcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.marketingcall_gid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + tomail_id + "'," +
                           "'" + cc_mailid + "'," +
                           "'Rejected'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                else
                {
                    values.status = false;
                    values.message = "Error Occured While Reject..!";
                }
            }
        }
        public void DaPostCompletedCall(string employee_gid, MdlMarketingCall values)
        {

            if (values.closure_status == "Converted")
            {

                msSQL = "select marketingcall_gid from mar_trn_tmarketingcall2leadstatus where marketingcall_gid ='" + values.marketingcall_gid + "' and (lead_type='SAMAGRO' or lead_type='SAMFIN')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Add Atleast One Samfin/Samagro Value";
                    return;
                }
                
                msSQL = " update mar_trn_tmarketingcall set callclosure_status='Converted', " +
                    " completed_flag='Y'," +
                    //" completed_remarks='" + values.completed_remarks.Replace("'", "") + "'," +
                    //  " closed_remarks='" + values.closure_status + "'," +
                    " assignclosure_status='" + values.closure_status + "'," +
                    //  " loanproduct_gid='" + values.loanproduct_gid + "'," +closed_remarks
                    //   " loanproduct_name='" + values.loanproduct_name + "'," +                 
                    " completed_by='" + employee_gid + "'," +
                    " completed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where marketingcall_gid='" + values.marketingcall_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " update mar_mst_tMarketingCallproofdocupload set marketingcall_gid='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //  msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                    //  mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    values.status = true;
                    values.message = "Lead Converted Successfully..!";

                    msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                    msSQL = "Insert into mar_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " marketingcall_gid," +
                               " status," +
                               " overall_detail," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + values.marketingcall_gid + "'," +
                               "' Completed'," +
                                "'" + employee_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " SELECT taggeduser_flag from mar_trn_tmarketingcall2taggedmember" +
                                      " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                    ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                    if (ls_taguser == "Y")
                    {

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                            " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select a.ticket_refid, a.caller_name,a.created_by as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from mar_trn_tmarketingcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                                 " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                        ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                        cc_leadmailid = objdbconn.GetExecuteScalar(msSQL);

                        lsconvertedmail_id = ConfigurationManager.AppSettings["convertedmailcc"].ToString();

                        cc_mailid = cc_leadmailid + "," + lsconvertedmail_id;
                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                                "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                                  "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket Converted";
                        body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Converted.<br><br>";
                        body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Lead Converted by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                        body = body + "Lead Converted time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business Development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;
                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.marketingcall_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Converted'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    else
                    {

                        if (mnResult == 1)
                        {
                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                   " FROM adm_mst_tcompany";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = objODBCDatareader["pop_username"].ToString();
                                ls_password = objODBCDatareader["pop_password"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = " select a.ticket_refid, a.caller_name,(a.created_by) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                        " from mar_trn_tmarketingcall a " +
                                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                       " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                                lscaller_name = objODBCDatareader["caller_name"].ToString();
                                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                                lsto2members = objODBCDatareader["to2members"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                            tomail_id = objdbconn.GetExecuteScalar(msSQL);

                            cc_mailid = ConfigurationManager.AppSettings["convertedmailcc"].ToString();
                          
                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                                    "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                                    "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);


                            sub = " A Lead Ticket Converted";
                            body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                            body = body + "Greetings! <br><br>";
                            body = body + "A ticket has been Converted.<br><br>";
                            body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                            body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                            body = body + "Lead Converted by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                            body = body + "Lead Converted time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                            body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                            body = body + "Regards<br><br>";
                            body = body + "Business Development - Customer Service Helpline<br><br>";
                            body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                            if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                            {
                                lstoReceipients = tomail_id.Split(',');
                                if (tomail_id.Length == 0)
                                {
                                    message.To.Add(new MailAddress(tomail_id));
                                }
                                else
                                {
                                    foreach (string ToEmail in lstoReceipients)
                                    {
                                        message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);
                            values.status = true;
                            if (values.status == true)
                            {
                                msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                                   " marketingcall_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.marketingcall_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + tomail_id + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Converted'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }

                        else
                        {
                            values.status = false;
                            values.message = "Error Occured..!";
                        }
                    }
                }
            }/////  follow up lead


            else if (values.closure_status == "Follow Up")
            {

                msSQL = "select marketingcall_gid from mar_trn_tmarketingcall2followup where marketingcall_gid ='" + values.marketingcall_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Add Atleast One Follow_Up Time";
                    return;
                }

                msSQL = " update mar_trn_tmarketingcall set callclosure_status='Follow UP', " +
                    " completed_flag='Y'," +
                    //" completed_remarks='" + values.completed_remarks.Replace("'", "") + "'," +
                    //  " closed_remarks='" + values.closure_status + "'," +
                    " assignclosure_status='" + values.closure_status + "'," +
                    //  " loanproduct_gid='" + values.loanproduct_gid + "'," +closed_remarks
                    //   " loanproduct_name='" + values.loanproduct_name + "'," +                 
                    " followup_by='" + employee_gid + "'," +
                    " followup_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where marketingcall_gid='" + values.marketingcall_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " update mar_mst_tMarketingCallproofdocupload set marketingcall_gid='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update mar_trn_tmarketingcall2followup set marketingcall_gid ='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    values.status = true;
                    values.message = "Lead Follow_Up Successfully..!";

                    msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                    msSQL = "Insert into mar_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " marketingcall_gid," +
                               " status," +
                               " overall_detail," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + values.marketingcall_gid + "'," +
                               "' Follow Up'," +
                                "'" + employee_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " SELECT taggeduser_flag from mar_trn_tmarketingcall2taggedmember" +
                                      " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                    ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
                /*
                if (ls_taguser == "Y")
                    {

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                            " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select a.ticket_refid, a.caller_name,a.created_by as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from mar_trn_tmarketingcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                                 " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                        ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                                "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                                  "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket Converted";
                        body = "Hi " + lsassignemployee_name + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Converted.<br><br>";
                        body = body + "Lead Name:" + lscaller_name + "<br><br>";
                        body = body + "Ticket request Ref ID:" + lsticket_refid + "<br><br>";
                        body = body + "Lead Converted by:" + employeename + "<br><br>";
                        body = body + "Lead Converted time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business Development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;
                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.marketingcall_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Converted'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    else
                    {

                        if (mnResult == 1)
                        {
                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                   " FROM adm_mst_tcompany";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = objODBCDatareader["pop_username"].ToString();
                                ls_password = objODBCDatareader["pop_password"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = " select a.ticket_refid, a.caller_name,(a.created_by) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                        " from mar_trn_tmarketingcall a " +
                                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                       " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                                lscaller_name = objODBCDatareader["caller_name"].ToString();
                                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                                lsto2members = objODBCDatareader["to2members"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                            tomail_id = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                                    "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                                    "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);


                            sub = " A Lead Ticket Converted";
                            body = "Hi " + lsassignemployee_name + ",<br><br>";
                            body = body + "Greetings! <br><br>";
                            body = body + "A ticket has been Converted.<br><br>";
                            body = body + "Lead Name:" + lscaller_name + "<br><br>";
                            body = body + "Ticket request Ref ID:" + lsticket_refid + "<br><br>";
                            body = body + "Lead Converted by:" + employeename + "<br><br>";
                            body = body + "Lead Converted time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                            body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                            body = body + "Regards<br><br>";
                            body = body + "Business Development - Customer Service Helpline<br><br>";
                            body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                            if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                            {
                                lstoReceipients = tomail_id.Split(',');
                                if (tomail_id.Length == 0)
                                {
                                    message.To.Add(new MailAddress(tomail_id));
                                }
                                else
                                {
                                    foreach (string ToEmail in lstoReceipients)
                                    {
                                        message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);
                            values.status = true;
                            if (values.status == true)
                            {
                                msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                                   " marketingcall_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.marketingcall_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + tomail_id + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Converted'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }

                        else
                        {
                            values.status = false;
                            values.message = "Error Occured..!";
                        }
                    }
                }*/
            }

            ////follow up call end
            else if (values.closure_status == "Rejected")
            {
                msSQL = " update mar_trn_tmarketingcall set callclosure_status='Rejected', " +
                          " rejected_flag = 'Y'," +
                          " rejected_remarks='" + values.closed + "'," +
                          " rejected_by = '" + employee_gid + "'," +
                          " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where marketingcall_gid='" + values.marketingcall_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " update mar_mst_tMarketingCallproofdocupload set marketingcall_gid='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update mar_mst_tMarketingCallrecordingdocupload set marketingcall_gid='" + values.marketingcall_gid + "' where marketingcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Lead Rejected Successfully..!";

                    msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                    msSQL = "Insert into mar_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " marketingcall_gid," +
                               " status," +
                               " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + values.marketingcall_gid + "'," +
                               "' Rejected'," +
                               "'" + employee_gid + "'," +
                               "'" + values.closed + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " SELECT taggeduser_flag from mar_trn_tmarketingcall2taggedmember" +
                                     " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                    ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                    if (ls_taguser == "Y")
                    {

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                            " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select a.ticket_refid, a.caller_name,(a.created_by) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from mar_trn_tmarketingcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                                 " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                        ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket Rejected ";
                        body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Rejected.<br><br>";
                        body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid)+ "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business Development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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

                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;
                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.marketingcall_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Follow Up'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        if (mnResult == 1)
                        {
                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                   " FROM adm_mst_tcompany";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = objODBCDatareader["pop_username"].ToString();
                                ls_password = objODBCDatareader["pop_password"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = " select a.ticket_refid, a.caller_name,a.created_by as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                        " from mar_trn_tmarketingcall a " +
                                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                       " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                                lscaller_name = objODBCDatareader["caller_name"].ToString();
                                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                                lsto2members = objODBCDatareader["to2members"].ToString();
                            }
                            objODBCDatareader.Close();



                            msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                            tomail_id = objdbconn.GetExecuteScalar(msSQL);

                            sub = " A Lead Ticket Rejected";
                            body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                            body = body + "Greetings! <br><br>";
                            body = body + "A ticket has been Rejected.<br><br>";
                            body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                            body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                            body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                            body = body + "Regards<br><br>";
                            body = body + "Business Development - Customer Service Helpline<br><br>";
                            body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                            if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                            {
                                lstoReceipients = tomail_id.Split(',');
                                if (tomail_id.Length == 0)
                                {
                                    message.To.Add(new MailAddress(tomail_id));
                                }
                                else
                                {
                                    foreach (string ToEmail in lstoReceipients)
                                    {
                                        message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);
                            values.status = true;
                            if (values.status == true)
                            {
                                msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                                   " marketingcall_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.marketingcall_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + tomail_id + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Extend Follow Up'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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

            else
            {
                values.status = false;
                values.message = "Please Select Lead Status";
                return;

            }
        }
        public void DaPostCloseCall(string employee_gid, MdlMarketingCall values)
        {
            if (values.followup_date == "" || values.followup_date == null || values.followup_date == "undefined")
            {
                msSQL = " update mar_trn_tmarketingcall set callclosure_status='Rejected'," +
                    " rejected_flag = 'Y'," +
                    " rejected_remarks='" + values.closed_remarks.Replace("'", "") + "'," +
                    " finalcallclosure_status='" + values.closure_status + "'," +
                    " rejected_by = '" + employee_gid + "'," +
                    " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where marketingcall_gid='" + values.marketingcall_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                msSQL = "Insert into mar_trn_tstatuslog( " +
                           " statuslog_gid," +
                           " marketingcall_gid," +
                           " status," +
                           " overall_detail," +
                           " remarks," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid2 + "'," +
                           "'" + values.marketingcall_gid + "'," +
                           "' Rejected'," +
                           "'" + employee_gid + "'," +
                           "'" + values.closed_remarks.Replace("'", "") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " SELECT taggeduser_flag from mar_trn_tmarketingcall2taggedmember" +
                                     " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                if (ls_taguser == "Y")
                {
                    values.status = true;
                    values.message = "Lead Rejected Successfully";
                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                        " FROM adm_mst_tcompany";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.ticket_refid, a.caller_name,(a.created_by) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " from mar_trn_tmarketingcall a " +
                                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                        lscaller_name = objODBCDatareader["caller_name"].ToString();
                        lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                        lsto2members = objODBCDatareader["to2members"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                             " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                    ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                   "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                   "where b.employee_gid ='" + employee_gid + "'";
                    employeename = objdbconn.GetExecuteScalar(msSQL);


                    sub = " A Lead Ticket Rejected";
                    body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been Rejected.<br><br>";
                    body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Lead Rejected by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                    body = body + "Lead Rejected time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Business Development - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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

                    if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                    {
                        lstoReceipients = tomail_id.Split(',');
                        if (tomail_id.Length == 0)
                        {
                            message.To.Add(new MailAddress(tomail_id));
                        }
                        else
                        {
                            foreach (string ToEmail in lstoReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    values.status = true;
                    if (values.status == true)
                    {
                        msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                           " marketingcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.marketingcall_gid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + tomail_id + "'," +
                           "'" + cc_mailid + "'," +
                           "'Call Closed'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {

                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Lead Rejected Successfully";

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select a.ticket_refid, a.caller_name,a.assignemployee_name,(a.assignemployee_gid) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from mar_trn_tmarketingcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket Rejected";
                        body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Rejected.<br><br>";
                        body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid)+ "<br><br>";
                        body = body + "Lead Rejected by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                        body = body + "lead Rejected time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business Development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;
                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.marketingcall_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Call Closed'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                }
            }
            else
            {
                msSQL = " update mar_trn_tmarketingcall set callclosure_status='Extend Follow Up', ";
                if (values.followup_date == null || values.followup_date == "")
                {
                    msSQL += " followup_date=null,";
                }
                else
                {
                    msSQL += " followup_date='" + Convert.ToDateTime(values.followup_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.followup_time == null || values.followup_time == "")
                {
                    msSQL += " followup_time=null,";
                }
                else
                {
                    msSQL += " followup_time='" + Convert.ToDateTime(values.followup_time).ToString("HH:mm:ss") + "',";
                }
                if (values.followup_date == null || values.followup_date == "")
                {
                    msSQL += " extendfollowup_date=null,";
                }
                else
                {
                    msSQL += " extendfollowup_date='" + Convert.ToDateTime(values.followup_date).ToString("yyyy-MM-dd") + "',";
                }
                if (values.followup_time == null || values.followup_time == "")
                {
                    msSQL += " extendfollowup_time=null,";
                }
                else
                {
                    msSQL += " extendfollowup_time='" + Convert.ToDateTime(values.followup_time).ToString("HH:mm:ss") + "',";
                }
                msSQL += " followup_remarks='" + values.followup_remarks.Replace("'", "") + "'," +
                        " extendfollowup_remarks='" + values.followup_remarks.Replace("'", "") + "'," +
                        " followup_by = '" + employee_gid + "'," +
                        " extendfollowup_by = '" + employee_gid + "'" +
                        " where marketingcall_gid='" + values.marketingcall_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                msSQL = "Insert into mar_trn_tstatuslog( " +
                           " statuslog_gid," +
                           " marketingcall_gid," +
                           " status," +
                            " overall_detail," +
                           " remarks," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid2 + "'," +
                           "'" + values.marketingcall_gid + "'," +
                           "' Extend Follow Up'," +
                           "'" + employee_gid + "'," +
                           "'" + values.followup_remarks.Replace("'", "") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " SELECT taggeduser_flag from mar_trn_tmarketingcall2taggedmember" +
                                     " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                if (ls_taguser == "Y")
                {
                    values.status = true;
                    values.message = "Lead Extend Follow Up Status Updated Successfully";

                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                        " FROM adm_mst_tcompany";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.ticket_refid, a.caller_name,(a.created_by) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " from mar_trn_tmarketingcall a " +
                                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                        lscaller_name = objODBCDatareader["caller_name"].ToString();
                        lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                        lsto2members = objODBCDatareader["to2members"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                             " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                    ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                   "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                   "where b.employee_gid ='" + employee_gid + "'";
                    employeename = objdbconn.GetExecuteScalar(msSQL);


                    sub = " A Lead Ticket Extend-FollowUP";
                    body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been Extend-FollowUp.<br><br>";
                    body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Lead Extend-FollowUP by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                    body = body + "Lead Extend-FollowUP time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Business Development - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                    if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                    {
                        lstoReceipients = tomail_id.Split(',');
                        if (tomail_id.Length == 0)
                        {
                            message.To.Add(new MailAddress(tomail_id));
                        }
                        else
                        {
                            foreach (string ToEmail in lstoReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    values.status = true;
                    if (values.status == true)
                    {
                        msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                           " marketingcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.marketingcall_gid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + tomail_id + "'," +
                           "'" + cc_mailid + "'," +
                           "'Call Extend Follow Up'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Lead Extend Follow Up Status Updated Successfully";

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                     " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select a.ticket_refid, a.caller_name,a.assignemployee_name,(a.assignemployee_gid) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from mar_trn_tmarketingcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket Extend-Follow UP ";
                        body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Extend-Follow Up.<br><br>";
                        body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Lead Extend-FollowUP by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                        body = body + "Lead Extend-FollowUP time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business Development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;
                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.marketingcall_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Call Extend Follow UP'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                }
            }
        }
        // Common Page

        public void DaGetAssignedCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date,date_format(a.assign_date,'%d-%m-%Y %h:%i %p') as assign_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                        "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +
      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where (a.callclosure_status = 'Assign' or a.callclosure_status = '') order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            assign_date = (dr_datarow["assign_date"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetCompletedCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, a.callclosure_status, " +
                        "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Converted' order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                            completed_by = (dr_datarow["completed_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetRejectedCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.callclosure_status," +
                        "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +
      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status='Rejected' order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetFollowUpCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.followup_date,'%d-%m-%Y %h:%i %p') as followup_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as followup_by , " +
                        "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +
      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.followup_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where (a.callclosure_status = 'Follow Up' or  a.callclosure_status = 'Extend Follow Up') order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            followup_date = (dr_datarow["followup_date"].ToString()),
                            followup_by = (dr_datarow["followup_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetClosedCallSummary(string employee_gid, MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT marketingcall_gid, ticket_refid,caller_name,leadrequesttype_name,customer_type, date_format(a.callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, assignemployee_name," +
                        " date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as rejected_by , " +
                        "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                        " FROM mar_trn_tmarketingcall a" +
                        " left join hrm_mst_temployee b on a.rejected_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Rejected' order by a.marketingcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCall_list = new List<MarketingCall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCall_list.Add(new MarketingCall_list
                        {
                            marketingcall_gid = (dr_datarow["marketingcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            closed_date = (dr_datarow["rejected_date"].ToString()),
                            closed_by = (dr_datarow["rejected_by"].ToString()),
                            origination = (dr_datarow["origination"].ToString())
                        });
                    }
                    values.MarketingCall_list = getMarketingCall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaMarketingAssignedCallCount(string employee_gid, MarketingCallCount values)
        {
            msSQL = "select count(marketingcall_gid) as assignedcall_count from mar_trn_tmarketingcall a where a.callclosure_status = 'Assign'";
            values.assignedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(marketingcall_gid) as completedcall_count from mar_trn_tmarketingcall a where a.callclosure_status = 'Converted'";
            values.completedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(marketingcall_gid) as completedcall_count from mar_trn_tmarketingcall a where a.callclosure_status='Rejected'";
            values.rejectedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(marketingcall_gid) as closedcall_count from mar_trn_tmarketingcall a where a.callclosure_status = 'Rejected' ";
            values.closedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(marketingcall_gid) as followupcall_count from mar_trn_tmarketingcall a where a.callclosure_status in ('Follow Up','Extend Follow Up')";
            values.followupcall_count = objdbconn.GetExecuteScalar(msSQL);

        }

        //Upload Call Proof Document

        public bool DaCallProofDocumentUpload(HttpRequest httpRequest, callproofuploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string marketingcall_gid = httpRequest.Form["marketingcall_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BusinessDevelopment/CallProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
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

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "BusinessDevelopment/CallProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "BusinessDevelopment/CallProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CallProofDocument" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                        msGetGid = objcmnfunctions.GetMasterGID("BCPU");
                        msSQL = " insert into mar_mst_tMarketingCallproofdocupload( " +
                                    " MarketingCallproofdocupload_gid," +
                                    " marketingcall_gid," +
                                    " document_title," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + marketingcall_gid + "'," +
                                    "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
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

        public void DaMarketingCallProofDocumentTmpList(string marketingcall_gid, string employee_gid, callproofuploaddocument values)
        {
            msSQL = " select MarketingCallproofdocupload_gid,marketingcall_gid,document_name,document_path,document_title from mar_mst_tMarketingCallproofdocupload " +
                             " where marketingcall_gid='" + marketingcall_gid + "' or marketingcall_gid='" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcallproofupload_list = new List<callproofupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcallproofupload_list.Add(new callproofupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        marketingcall_gid = dt["marketingcall_gid"].ToString(),
                        MarketingCallproofdocupload_gid = dt["MarketingCallproofdocupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.callproofupload_list = getcallproofupload_list;
                }
            }
            dt_datatable.Dispose();
        }
    /*    public void DaMarketingLeadFormDocumentList(string marketingcall_gid, string employee_gid, callproofuploaddocument values)
        {
            msSQL = " select a.document_gid,a.marketingcall_gid,a.document_name,a.file_path,a.document_title,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date  " +
     "            from mar_mst_tdocumentupload a  " +
            "      left join hrm_mst_temployee b on b.employee_gid=a.created_by  " +
        "          left join adm_mst_tuser c on c.user_gid=b.user_gid ";
         //   "        where marketingcall_gid='" +  marketingcall_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcallproofupload_list = new List<callproofupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {

                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = dt["file_path"].ToString();
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();



                foreach (DataRow dt in dt_datatable.Rows)
                {


                    getcallproofupload_list.Add(new callproofupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        document_path = dt["file_path"].ToString(),
                        marketingcall_gid = dt["marketingcall_gid"].ToString(),
                        MarketingCallproofdocupload_gid = dt["document_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.callproofupload_list = getcallproofupload_list;
                }
            }
            dt_datatable.Dispose();
        }
*/
        public void DaMarketingCallProofDocumentList(string marketingcall_gid, string employee_gid, callproofuploaddocument values)
        {
            msSQL = " select a.MarketingCallproofdocupload_gid,a.marketingcall_gid,a.document_name,a.document_path,a.document_title,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                " from mar_mst_tMarketingCallproofdocupload a " +
                 " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                   " where marketingcall_gid='" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcallproofupload_list = new List<callproofupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {

                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();



                foreach (DataRow dt in dt_datatable.Rows)
                {


                    getcallproofupload_list.Add(new callproofupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        marketingcall_gid = dt["marketingcall_gid"].ToString(),
                        MarketingCallproofdocupload_gid = dt["MarketingCallproofdocupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.callproofupload_list = getcallproofupload_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaMarketingCallProofDocumentDelete(string MarketingCallproofdocupload_gid, callproofuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from mar_mst_tMarketingCallproofdocupload where MarketingCallproofdocupload_gid='" + MarketingCallproofdocupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                objfilename.message = "Document Deleted Successfully";
                objfilename.status = true;
            }
            else
            {
                objfilename.message = "Error Occured";
                objfilename.status = false;

            }
        }

        //Upload Call Recording Document

        public bool DaCallRecordingDocumentUpload(HttpRequest httpRequest, callproofuploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BusinessDevelopment/CallRecordingDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
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

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "BusinessDevelopment/CallRecordingDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "BusinessDevelopment/CallRecordingDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        // ---repeated code-----

                        msGetGid = objcmnfunctions.GetMasterGID("BCRU");
                        msSQL = " insert into mar_mst_tMarketingCallrecordingdocupload( " +
                                    " MarketingCallrecordingocupload_gid," +
                                    " marketingcall_gid," +
                                    " document_title," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
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

        public void DaMarketingCallRecordingDocumentTmpList(string marketingcall_gid, string employee_gid, callproofuploaddocument values)
        {
            msSQL = " select MarketingCallrecordingocupload_gid,marketingcall_gid,document_name,document_path,document_title from mar_mst_tMarketingCallrecordingdocupload " +
                             " where marketingcall_gid='" + employee_gid + "' or marketingcall_gid='" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcallproofupload_list = new List<callproofupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcallproofupload_list.Add(new callproofupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        marketingcall_gid = dt["marketingcall_gid"].ToString(),
                        MarketingCallrecordingocupload_gid = dt["MarketingCallrecordingocupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.callproofupload_list = getcallproofupload_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaMarketingCallRecordingDocumentList(string marketingcall_gid, string employee_gid, callproofuploaddocument values)
        {
            msSQL = " select a.MarketingCallrecordingocupload_gid,a.marketingcall_gid,a.document_name,a.document_path,a.document_title , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                 " from mar_mst_tMarketingCallrecordingdocupload a " +
                 " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                " where marketingcall_gid='" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcallproofupload_list = new List<callproofupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcallproofupload_list.Add(new callproofupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        //document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        marketingcall_gid = dt["marketingcall_gid"].ToString(),
                        MarketingCallrecordingocupload_gid = dt["MarketingCallrecordingocupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.callproofupload_list = getcallproofupload_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaMarketingCallRecordingDocumentDelete(string MarketingCallrecordingocupload_gid, callproofuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from mar_mst_tMarketingCallrecordingdocupload where MarketingCallrecordingocupload_gid='" + MarketingCallrecordingocupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                objfilename.message = "Document Deleted Successfully";
                objfilename.status = true;
            }
            else
            {
                objfilename.message = "Error Occured";
                objfilename.status = false;

            }
        }

        public void DaMarketingCallDocTempClear(string employee_gid, result values)
        {
            msSQL = "delete from mar_mst_tMarketingCallrecordingdocupload where marketingcall_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from mar_mst_tMarketingCallproofdocupload where marketingcall_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from mar_trn_tmarketingcall2leadstatus where marketingcall_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetMarketingReportSummary(MarketingReport objTeleCallingReport)
        {
            msSQL = " select a.marketingcall_gid,a.entity_name, ticket_refid, caller_name,leadrequesttype_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,  concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by , " +
                    "CASE callclosure_status " +
  "   WHEN 'Assign' THEN 'Yet To Acknowledge'  " +
  "   WHEN 'Unassigned' THEN 'Unassigned'  " +
   "  WHEN 'Closed' THEN 'Closed' " +
   "  WHEN 'Converted' THEN 'Converted' " +
    " WHEN 'Follow Up' THEN 'Follow Up' " +
    "  WHEN 'Extend Follow Up' THEN 'Follow Up' " +
     "  WHEN 'Work-In-Progress' THEN ' Work In Progress' " +
     "  WHEN 'Rejected' THEN 'Rejected' " +
  "   ELSE '' " +
 " END as Leadclosure_status , " +
 "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
            "   WHEN 'Enquiry' THEN 'Enquiry'  " +

      "   ELSE 'Offline' " +
     " END as origination  " +
                    " from mar_trn_tmarketingcall a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.marketingcall_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objTeleCallingReportSummary = new List<MarketingReportList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objTeleCallingReportSummary.Add(new MarketingReportList
                    {
                        marketingcall_gid = dr_datarow["marketingcall_gid"].ToString(),
                        entity_name = dr_datarow["entity_name"].ToString(),
                        ticket_refid = dr_datarow["ticket_refid"].ToString(),
                        caller_name = dr_datarow["caller_name"].ToString(),
                        leadrequesttype_name = dr_datarow["leadrequesttype_name"].ToString(),
                        marketingcall_status = dr_datarow["Leadclosure_status"].ToString(),
                        created_date = dr_datarow["created_date"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                        origination = (dr_datarow["origination"].ToString())
                    });
                }
                objTeleCallingReport.MarketingReportList = objTeleCallingReportSummary;
            }
            dt_datatable.Dispose();
            objTeleCallingReport.status = true;
            objTeleCallingReport.message = "Success";
        }

        // export excel Marketing Report

        public void DaExportMarketingReport(MarketingReport objTeleCallingReport)
        {

            /*" select a.entity_name as `Entity`, ticket_refid as `Lead Ref Number`, caller_name as `Lead Name`, leadrequesttype_name as `Lead_Request_Type`, callreceived_date as `Lead Received Date`," +
" marketingsourceofcontact_name as `Source of Contact`, marketingcallreceivednumber_name as `Lead Received Number`, internalreference_name as `Internal Reference`, office_landlineno as `Office Landline Number`," +
" callerassociate_company as `Lead Associate Company`,baselocation_name as Baselocation, marketingfunction_name as `Function`, callassign_status as `Lead Assigning Status`," +
"CASE callclosure_status "+
  "   WHEN 'Assign' THEN 'Yet To Acknowledge'  "+
   "  WHEN 'Closed' THEN 'Closed' "+
   "  WHEN 'Converted' THEN 'Converted' "+
    " WHEN 'Follow Up' THEN 'Follow Up' "+
    "  WHEN 'Extend Follow Up' THEN 'Follow Up' "+
     "  WHEN 'Work-In-Progress' THEN ' Work In Progress' "+
     "  WHEN 'Rejected' THEN 'Rejected' "+
  "   ELSE '' " +
 " END as Leadclosure_status , "+
" assignclosure_remarks as `Lead Assigned Remarks`," +
" (select GROUP_CONCAT(taggedmember_name) from mar_trn_tmarketingcall2taggedmember p where p.marketingcall_gid = a.marketingcall_gid) as `Tag Details`," +
" (select CONCAT(addressline1,', ',addressline2,', ',city,', ',taluka,', ',district,', ',state,' ',postal_code) from mar_trn_tmarketingcall2address q where primary_status = 'Yes' and q.marketingcall_gid = a.marketingcall_gid) as `Address Details`," +
" (select (max(mobile_no)) from mar_trn_tmarketingcall2mobileno r where primary_status = 'Yes' and r.marketingcall_gid = a.marketingcall_gid) as `Mobile Number`," +
" (select (max(email_address)) from mar_trn_tmarketingcall2email s where primary_status = 'Yes' and s.marketingcall_gid = a.marketingcall_gid) as `Email Address`," +
" requirement as `Requirement/Enquiry Title`, enquiry_description as `Lead Description`," +
" (select date_format(max(t.followup_date),'%d-%m-%Y') from mar_trn_tmarketingcall2followup t where t.marketingcall_gid = a.marketingcall_gid) as `Follow Up Date`," +
" (select time_format(max(u.followup_time), '%H:%i') from mar_trn_tmarketingcall2followup u where u.marketingcall_gid = a.marketingcall_gid) as `Follow Up Time`, followup_remarks as `followup_remarks`," +
" assignemployee_name as `Assigned To`, date_format(assign_date,'%d-%m-%Y') as `Assigned Date`,assignclosure_status as `Assigned Status`, completed_remarks as `Assigned Remarks`,finalcallclosure_status as `Final Lead Closure Status`, closed_remarks as `Final Lead Closure Remarks`,date_format(a.updated_date,'%d-%m-%Y') as `Final Lead Closure Updated Date`," +
" (select (max(transferto_name)) from mar_trn_tmarketingcalltransferlog v where v.transferfrom_gid = a.assignemployee_gid and v.marketingcall_gid = a.marketingcall_gid) as `Transfer To`," +
" (select (max(transfer_remarks)) from mar_trn_tmarketingcalltransferlog v where v.transferfrom_gid = a.assignemployee_gid and v.marketingcall_gid = a.marketingcall_gid) as `Transfer Remarks`," +
" date_format(a.created_date,'%d-%m-%Y') as `Created Date`, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as `Created By` ," +
" date_format(a.closed_date,'%d-%m-%Y') as `Closed Date`, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as `Closed By`" +
" from mar_trn_tmarketingcall a" +
" left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
" left join adm_mst_tuser c on c.user_gid = b.user_gid" +
" left join hrm_mst_temployee d on a.closed_by = d.employee_gid" +
" left join adm_mst_tuser e on e.user_gid = d.user_gid";
        */
            msSQL = "call sp_rpt_leadmanagementreport";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            //ExcelPackage excel = new ExcelPackage();
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("LeadTrackerReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objTeleCallingReport.lsname = "LeadTrackerReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BusinessDevelopment/BusinessDevelopmentReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objTeleCallingReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BusinessDevelopment/BusinessDevelopmentReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objTeleCallingReport.lsname;
                objTeleCallingReport.lscloudpath = lscompany_code + "/" + "BusinessDevelopment/BusinessDevelopmentReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objTeleCallingReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
               /* if (!exists)
                {
                  //  System.IO.Directory.CreateDirectory(path);
                }*/
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objTeleCallingReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 52])  //Address "A1:A29"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "BusinessDevelopment/BusinessDevelopmentReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objTeleCallingReport.lsname, ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                objTeleCallingReport.status = false;
                objTeleCallingReport.message = "Failure";
            }
            objTeleCallingReport.lscloudpath = objcmnstorage.EncryptData(objTeleCallingReport.lscloudpath);
            objTeleCallingReport.lspath = objcmnstorage.EncryptData(objTeleCallingReport.lspath);
            objTeleCallingReport.status = true;
            objTeleCallingReport.message = "Success";
        }
        public void DaGetEntity(MdlMarketingCall values)
        {
            try
            {
                msSQL = "select entity_name,entity_gid from adm_mst_tentity where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinboundentity_list = new List<inboundentity_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinboundentity_list.Add(new inboundentity_list
                        {
                            entity_gid = (dr_datarow["entity_gid"].ToString()),
                            entity_name = (dr_datarow["entity_name"].ToString()),
                        });
                    }
                    values.inboundentity_list = getinboundentity_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetMarketingSourceofContact(MdlMarketingCall values)
        {
            try
            {
                msSQL = "select marketingsourceofcontact_name,marketingsourceofcontact_gid from mar_mst_tmarketingsourceofcontact where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingSourceofContact_list_list = new List<MarketingSourceofContact_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingSourceofContact_list_list.Add(new MarketingSourceofContact_list
                        {
                            marketingsourceofcontact_gid = (dr_datarow["marketingsourceofcontact_gid"].ToString()),
                            marketingsourceofcontact_name = (dr_datarow["marketingsourceofcontact_name"].ToString()),
                        });
                    }
                    values.MarketingSourceofContact_list = getMarketingSourceofContact_list_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetMarketingCallType(MdlMarketingCall values)
        {
            try
            {
                msSQL = "select marketingcalltype_name,marketingcalltype_gid from mar_mst_tmarketingcalltype where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCallType_list = new List<MarketingCallType_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCallType_list.Add(new MarketingCallType_list
                        {
                            marketingcalltype_gid = (dr_datarow["marketingcalltype_gid"].ToString()),
                            marketingcalltype_name = (dr_datarow["marketingcalltype_name"].ToString()),
                        });
                    }
                    values.MarketingCallType_list = getMarketingCallType_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetLeadRequestType(MdlMarketingCall values)
        {
            try
            {
                msSQL = "select leadrequesttype_name,leadrequesttype_gid from mar_mst_tleadrequesttype where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleadrequest_list = new List<leadrequest_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleadrequest_list.Add(new leadrequest_list
                        {
                            leadrequesttype_gid = (dr_datarow["leadrequesttype_gid"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                        });
                    }
                    values.leadrequest_list = getleadrequest_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetMarketingTelecallingFunction(MdlMarketingCall values)
        {
            try
            {
                msSQL = "select marketingtelecallingfunction_name,marketingtelecallingfunction_gid from mar_mst_tmarketingtelecallingfunction where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingTelecallingFunction_list = new List<MarketingTelecallingFunction_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingTelecallingFunction_list.Add(new MarketingTelecallingFunction_list
                        {
                            marketingtelecallingfunction_gid = (dr_datarow["marketingtelecallingfunction_gid"].ToString()),
                            marketingtelecallingfunction_name = (dr_datarow["marketingtelecallingfunction_name"].ToString()),
                        });
                    }
                    values.MarketingTelecallingFunction_list = getMarketingTelecallingFunction_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetMarketingCallReceivedNumber(MdlMarketingCall values)
        {
            try
            {
                msSQL = "select marketingcallreceivednumber_name,marketingcallreceivednumber_gid from mar_mst_tmarketingcallreceivednumber where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCallReceivedNumber_list = new List<MarketingCallReceivedNumber_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCallReceivedNumber_list.Add(new MarketingCallReceivedNumber_list
                        {
                            marketingcallreceivednumber_gid = (dr_datarow["marketingcallreceivednumber_gid"].ToString()),
                            marketingcallreceivednumber_name = (dr_datarow["marketingcallreceivednumber_name"].ToString()),
                        });
                    }
                    values.MarketingCallReceivedNumber_list = getMarketingCallReceivedNumber_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetMarketingCallReportView(string marketingcall_gid, MdlMarketingCallView values)
        {
            try
            {
                msSQL = " select ticket_refid,a.entity_name,marketingsourceofcontact_name,caller_name,industry,leadrequesttype_name, internalreference_name,leadrequire_name,milletrequire_name,enquiryrequire_name,startuprequire_name,business_name, " +
                        " callerassociate_company,office_landlineno,marketingcalltype_name,enquiry_description,baselocation_name, callclosure_status,assignemployee_name," +
                        " tagemployee_name,assignclosure_remarks,closed_remarks,marketingcall_status, date_format(assign_date,'%d-%m-%Y %h:%i %p') as assign_date,completed_remarks,closed_remarks," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as closed_by," +
                        " date_format(completed_date,'%d-%m-%Y %h:%i %p') as completed_date,date_format(callreceived_date,'%d-%m-%Y %h:%i %p') as callreceived_date, date_format(closed_date,'%d-%m-%Y %h:%i %p') as closed_date, followup_remarks,tat_hours," +
                        " date_format(acknowledge_date,'%d-%m-%Y %h:%i %p') as acknowledge_date, date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%h:%i %p') as followup_time," +
                        " concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as followup_by, rejected_remarks," +
                        " concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as rejected_by, date_format(rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date, " +
                       " concat(k.user_firstname,' ',k.user_lastname,' / ',k.user_code) as acknowledge_by, " +
                       "CASE a.origination " +
      "   WHEN 'Online' THEN 'Online'  " +
      "   WHEN 'Millet' THEN 'Millet'  " +
      "   WHEN 'Enquiry' THEN 'Enquiry'  " +
      "   ELSE 'Offline' " +
     " END as origination  " +
                       " from mar_trn_tmarketingcall a " +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.closed_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " left join hrm_mst_temployee f on a.followup_by = f.employee_gid" +
                        " left join adm_mst_tuser g on g.user_gid = f.user_gid" +
                        " left join hrm_mst_temployee h on a.rejected_by = h.employee_gid" +
                        " left join adm_mst_tuser i on i.user_gid = h.user_gid" +
                          " left join hrm_mst_temployee j on a.acknowledge_by = j.employee_gid" +
                        " left join adm_mst_tuser k on k.user_gid = j.user_gid" +
                        " where marketingcall_gid='" + marketingcall_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    values.entity_name = objODBCDatareader["entity_name"].ToString();
                    values.sourceofcontact_name = objODBCDatareader["marketingsourceofcontact_name"].ToString();
                    values.callreceived_date = objODBCDatareader["callreceived_date"].ToString();
                    values.caller_name = objODBCDatareader["caller_name"].ToString();
                    values.leadrequesttype_name = objODBCDatareader["leadrequesttype_name"].ToString();
                    values.internalreference_name = objODBCDatareader["internalreference_name"].ToString();
                    values.callerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                    values.office_landlineno = objODBCDatareader["office_landlineno"].ToString();
                    values.calltype_name = objODBCDatareader["marketingcalltype_name"].ToString();
                    values.enquiry_description = objODBCDatareader["enquiry_description"].ToString();
                    values.callclosure_status = objODBCDatareader["callclosure_status"].ToString();
                    values.assignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                    values.tagemployee_name = objODBCDatareader["tagemployee_name"].ToString();
                    values.assignclosure_remarks = objODBCDatareader["assignclosure_remarks"].ToString();
                    values.assign_date = objODBCDatareader["assign_date"].ToString();
                    values.completed_by = objODBCDatareader["completed_by"].ToString();
                    values.closed_by = objODBCDatareader["closed_by"].ToString();
                    values.completed_remarks = objODBCDatareader["completed_remarks"].ToString();
                    values.closed_remarks = objODBCDatareader["closed_remarks"].ToString();
                    values.completed_date = objODBCDatareader["completed_date"].ToString();
                    values.closed_date = objODBCDatareader["closed_date"].ToString();
                    values.followup_remarks = objODBCDatareader["followup_remarks"].ToString();
                    values.acknowledge_date = objODBCDatareader["acknowledge_date"].ToString();
                    values.followup_date = objODBCDatareader["followup_date"].ToString();
                    values.followup_time = objODBCDatareader["followup_time"].ToString();
                    values.followup_by = objODBCDatareader["followup_by"].ToString();
                    values.rejected_date = objODBCDatareader["rejected_date"].ToString();
                    values.rejected_by = objODBCDatareader["rejected_by"].ToString();
                    values.rejected_remarks = objODBCDatareader["rejected_remarks"].ToString();
                    values.acknowledge_date = objODBCDatareader["acknowledge_date"].ToString();
                    values.acknowledge_by = objODBCDatareader["acknowledge_by"].ToString();
                    values.tat_hours = objODBCDatareader["tat_hours"].ToString();
                    values.closed_remarks = objODBCDatareader["closed_remarks"].ToString();
                    values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                    values.origination = objODBCDatareader["origination"].ToString();
                    values.leadrequire_name = objODBCDatareader["leadrequire_name"].ToString();
                    values.milletrequire_name = objODBCDatareader["milletrequire_name"].ToString();
                    values.enquiryrequire_name = objODBCDatareader["enquiryrequire_name"].ToString();
                    values.startuprequire_name = objODBCDatareader["startuprequire_name"].ToString();
                    values.business_name = objODBCDatareader["business_name"].ToString();
                    values.industry_name = objODBCDatareader["industry"].ToString();

                }

                objODBCDatareader.Close();

                msSQL = "select mobile_no from mar_trn_tmarketingcall2mobileno where primary_status='Yes' and marketingcall_gid='" + marketingcall_gid + "'";
                values.primary_mobileno = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select mobile_no,whatsapp_status,sms_to from mar_trn_tmarketingcall2mobileno where marketingcall_gid='" + marketingcall_gid + "' and primary_status='Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCallmobileno_list = new List<MarketingCallmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCallmobileno_list.Add(new MarketingCallmobileno_list
                        {
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_status = (dr_datarow["whatsapp_status"].ToString()),
                            sms_to = (dr_datarow["sms_to"].ToString()),
                        });
                    }
                    values.MarketingCallmobileno_list = getMarketingCallmobileno_list;
                }
                dt_datatable.Dispose();

                msSQL = "select email_address from mar_trn_tmarketingcall2email where primary_status='Yes' and marketingcall_gid='" + marketingcall_gid + "'";
                values.primary_email = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select email_address from mar_trn_tmarketingcall2email where marketingcall_gid='" + marketingcall_gid + "' and primary_status='Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCallemail_list = new List<MarketingCallemail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCallemail_list.Add(new MarketingCallemail_list
                        {
                            email_address = (dr_datarow["email_address"].ToString()),
                        });
                    }
                    values.MarketingCallemail_list = getMarketingCallemail_list;
                }
                dt_datatable.Dispose();

                msSQL = "  select addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code, city,latitude,longitude from mar_trn_tmarketingcall2address where marketingcall_gid='" + marketingcall_gid + "' and primary_status = 'Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCalladdress_list = new List<MarketingCalladdress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCalladdress_list.Add(new MarketingCalladdress_list
                        {
                            addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            addressline1 = (dr_datarow["addressline1"].ToString()),
                            addressline2 = (dr_datarow["addressline2"].ToString()),
                            landmark = (dr_datarow["landmark"].ToString()),
                            taluka = (dr_datarow["taluka"].ToString()),
                            district = (dr_datarow["district"].ToString()),
                            state = (dr_datarow["state"].ToString()),
                            country = (dr_datarow["country"].ToString()),
                            latitude = (dr_datarow["latitude"].ToString()),
                            longitude = (dr_datarow["longitude"].ToString()),
                            postal_code = (dr_datarow["postal_code"].ToString()),
                            city = (dr_datarow["city"].ToString())
                        });
                    }
                    values.MarketingCalladdress_list = getMarketingCalladdress_list;
                }
                dt_datatable.Dispose();

                msSQL = "select date_format(a.followup_date, '%d-%m-%Y') as followup_date,date_format(a.created_date, '%d-%m-%Y') as created_date,time_format(a.followup_time, '%H:%i') as followup_time,followup_status,followup_remarks," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " from mar_trn_tmarketingcall2followup a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where marketingcall_gid = '" + marketingcall_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCallfollowup_list = new List<MarketingCallfollowup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCallfollowup_list.Add(new MarketingCallfollowup_list
                        {
                            followup_date = (dr_datarow["followup_date"].ToString()),
                            followup_time = (dr_datarow["followup_time"].ToString()),
                            followup_remarks = (dr_datarow["followup_remarks"].ToString()),
                            followup_status = (dr_datarow["followup_status"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                }
                values.MarketingCallfollowup_list = getMarketingCallfollowup_list;
                dt_datatable.Dispose();

                msSQL = "select date_format(a.extendfollowup_date, '%d-%m-%Y') as followup_date,time_format(a.extendfollowup_time, '%H:%i') as followup_time ,a.extendfollowup_remarks," +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as extendfollowup_by" +
              " from mar_trn_tmarketingcall a " +
               " left join hrm_mst_temployee b on a.extendfollowup_by = b.employee_gid" +
               " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
              " where marketingcall_gid = '" + marketingcall_gid + "' and followup_date is not null";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmarketingcallextendfollowup_list = new List<marketingcallextendfollowup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmarketingcallextendfollowup_list.Add(new marketingcallextendfollowup_list
                        {
                            extendfollowup_date = (dr_datarow["followup_date"].ToString()),
                            extendfollowup_time = (dr_datarow["followup_time"].ToString()),
                            extendfollowup_remarks = (dr_datarow["extendfollowup_remarks"].ToString()),
                            extendfollowup_by = (dr_datarow["extendfollowup_by"].ToString()),
                        });
                    }
                }
                values.marketingcallextendfollowup_list = getmarketingcallextendfollowup_list;
                dt_datatable.Dispose();

                msSQL = " select taggedmember_name," +
                 " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as tagged_by,date_format(a.tagged_date,'%d-%m-%Y %h:%i %p') as tagged_date" +
                 " from mar_trn_tmarketingcall2taggedmember a" +
                 " left join hrm_mst_temployee b on b.employee_gid=a.tagged_by " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                 " where marketingcall_gid = '" + marketingcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCalltaggedmember_list = new List<MarketingCalltaggedmember_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCalltaggedmember_list.Add(new MarketingCalltaggedmember_list
                        {
                            taggedmember_name = (dr_datarow["taggedmember_name"].ToString()),
                            tagged_by = (dr_datarow["tagged_by"].ToString()),
                            tagged_date = (dr_datarow["tagged_date"].ToString()),
                        });
                    }
                    values.MarketingCalltaggedmember_list = getMarketingCalltaggedmember_list;
                }
                dt_datatable.Dispose();
                msSQL = " select a.status,a.remarks," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
               " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as overall_detail" +
                " from mar_trn_tstatuslog a" +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                 " left join hrm_mst_temployee d on d.employee_gid=a.overall_detail " +
                " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                " where marketingcall_gid = '" + marketingcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCallstatus_list = new List<MarketingCallstatus_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCallstatus_list.Add(new MarketingCallstatus_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            overall_detail = (dr_datarow["overall_detail"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.MarketingCallstatus_list = getMarketingCallstatus_list;
                }
                dt_datatable.Dispose();

                msSQL = " select transferfrom_name,transferto_name,a.transfer_remarks," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as transfer_by,date_format(a.transfer_date,'%d-%m-%Y %h:%i %p') as transfer_date" +
                " from mar_trn_tmarketingcalltransferlog a" +
                " left join hrm_mst_temployee b on b.employee_gid=a.transfer_by " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                " where marketingcall_gid = '" + marketingcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMarketingCalltransfer_list = new List<MarketingCalltransfer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMarketingCalltransfer_list.Add(new MarketingCalltransfer_list
                        {
                            transferfrom_name = (dr_datarow["transferfrom_name"].ToString()),
                            transferto_name = (dr_datarow["transferto_name"].ToString()),
                            transfer_by = (dr_datarow["transfer_by"].ToString()),
                            transfer_date = (dr_datarow["transfer_date"].ToString()),
                            transfer_remarks = (dr_datarow["transfer_remarks"].ToString())
                        });
                    }
                    values.MarketingCalltransfer_list = getMarketingCalltransfer_list;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch(Exception ex )
            {
                values.status = false;
                values.message = "failure";
            }
        }
        public void DaMarketingCallAssignedClosed(string employee_gid, MdlMarketingCall values)
        {

            msSQL = " update mar_trn_tmarketingcall set callclosure_status='Rejected'," +
                     " rejected_flag = 'Y'," +
                     " rejected_remarks='" + values.closed_remarks.Replace("'", "") + "'," +
                     " rejected_by = '" + employee_gid + "'," +
                    " created_by = '" + employee_gid + "'," +
                     " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where marketingcall_gid='" + values.marketingcall_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Marketing Lead Rejected Successfully";


                msGetGid2 = objcmnfunctions.GetMasterGID("BSTA");
                msSQL = "Insert into mar_trn_tstatuslog( " +
                           " statuslog_gid," +
                           " marketingcall_gid," +
                           " status," +
                           " overall_detail," +
                           " remarks," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid2 + "'," +
                           "'" + values.marketingcall_gid + "'," +
                           "' Closed'," +
                           "'" + employee_gid + "'," +
                           "'" + values.closed_remarks.Replace("'", "") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " SELECT taggeduser_flag from mar_trn_tmarketingcall2taggedmember" +
                                     " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                if (ls_taguser == "Y")
                {
                    values.status = true;
                    values.message = "Lead Rejected Successfully";
                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                        " FROM adm_mst_tcompany";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.ticket_refid, a.caller_name,(a.created_by) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " from mar_trn_tmarketingcall a " +
                                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                        lscaller_name = objODBCDatareader["caller_name"].ToString();
                        lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                        lsto2members = objODBCDatareader["to2members"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from mar_trn_tmarketingcall2taggedmember" +
                             " where marketingcall_gid ='" + values.marketingcall_gid + "'";
                    ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                   "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                   "where b.employee_gid ='" + employee_gid + "'";
                    employeename = objdbconn.GetExecuteScalar(msSQL);


                    sub = " A Lead Ticket Rejected";
                    body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been Rejected.<br><br>";
                    body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Lead Rejected by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                    body = body + "Lead Rejected time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Business Development - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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

                    if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                    {
                        lstoReceipients = tomail_id.Split(',');
                        if (tomail_id.Length == 0)
                        {
                            message.To.Add(new MailAddress(tomail_id));
                        }
                        else
                        {
                            foreach (string ToEmail in lstoReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    values.status = true;
                    if (values.status == true)
                    {
                        msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                           " marketingcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.marketingcall_gid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + tomail_id + "'," +
                           "'" + cc_mailid + "'," +
                           "'Call Closed'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {

                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Lead Rejected Successfully";

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                " FROM adm_mst_tcompany";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " select a.ticket_refid, a.caller_name,a.assignemployee_name,(a.assignemployee_gid) as to2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from mar_trn_tmarketingcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where marketingcall_gid ='" + values.marketingcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Lead Ticket Rejected";
                        body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Rejected.<br><br>";
                        body = body + "Lead Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Lead Rejected by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                        body = body + "Lead Rejected time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Business Development - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["businessdevelopmentbcc"].ToString();
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
                        if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                        {
                            lstoReceipients = tomail_id.Split(',');
                            if (tomail_id.Length == 0)
                            {
                                message.To.Add(new MailAddress(tomail_id));
                            }
                            else
                            {
                                foreach (string ToEmail in lstoReceipients)
                                {
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        values.status = true;
                        if (values.status == true)
                        {
                            msSQL = "Insert into mar_trn_tmarketingmailcount( " +
                               " marketingcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.marketingcall_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Call Closed'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                }
            }
            else
            {
                values.message = "Error Occured While Closing";
                values.status = false;
            }
        }
        public void DaGetBaselocation(string employee_gid, MdlMarketingCall values)
        {
            msSQL = " SELECT a.employee_gid, a.baselocation_gid,b.baselocation_name  FROM hrm_mst_temployee a " +
                    " left join sys_mst_tbaselocation b on b.baselocation_gid = a.baselocation_gid " +
                    " where employee_gid = '" + employee_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.employee_gid = objODBCDatareader["employee_gid"].ToString();
                values.baselocation_gid = objODBCDatareader["baselocation_gid"].ToString();
                values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
            }
            objODBCDatareader.Close();

        }
        public void DaGetLoanSubProduct(MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT loansubproduct_gid,loansubproduct_name from ocs_mst_tloansubproduct where status='Y' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsamapplication_list = new List<samapplication_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsamapplication_list.Add(new samapplication_list
                        {
                            loansubproduct_gid = (dr_datarow["loansubproduct_gid"].ToString()),
                            loansubproduct_name = (dr_datarow["loansubproduct_name"].ToString()),
                           
                        });
                    }
                    values.samapplication_list = getsamapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetLoanProduct(MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT loanproduct_gid,loanproduct_name from ocs_mst_tloanproduct where status='Y' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsamapplication_list = new List<samapplication_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsamapplication_list.Add(new samapplication_list
                        {
                            loanproduct_gid = (dr_datarow["loanproduct_gid"].ToString()),
                            loanproduct_name = (dr_datarow["loanproduct_name"].ToString()),
                           
                        });
                    }
                    values.samapplication_list = getsamapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetAgrLoanProduct(MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT loanproduct_gid,loanproduct_name from agr_mst_tloanproduct where status='Y' "; 
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsamapplication_list = new List<samapplication_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsamapplication_list.Add(new samapplication_list
                        {
                            loanproduct_gid = (dr_datarow["loanproduct_gid"].ToString()),
                            loanproduct_name = (dr_datarow["loanproduct_name"].ToString()),
                           
                        });
                    }
                    values.samapplication_list = getsamapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetAgrLoanSubProduct(MdlMarketingCall values)
        {
            try
            {
                msSQL = " SELECT loansubproduct_gid,loansubproduct_name from agr_mst_tloansubproduct where status='Y' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsamapplication_list = new List<samapplication_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsamapplication_list.Add(new samapplication_list
                        {
                            loansubproduct_gid = (dr_datarow["loansubproduct_gid"].ToString()),
                            loansubproduct_name = (dr_datarow["loansubproduct_name"].ToString()),
                          
                        });
                    }
                    values.samapplication_list = getsamapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaMarketingRejected(string employee_gid, MdlMarketingCall values)
        {

            msSQL = " update mar_trn_tmarketingcall set " +
                 " updated_by='" + employee_gid + "'," +
                  " created_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  " rejected_remarks='" + values.rejected_remarks.Replace("'", "") + "'," +
                 " callclosure_status='Rejected'" +
                 " where marketingcall_gid='" + values.marketingcall_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Rejected successfully";
                return;
            }

            else
            {
                values.status = false;
                values.message = "Error Occurred While Rejected";
                return;
            }

        }
        public void DaGetMilletDocumentList(string marketingcall_gid, MdlMarketingCall values)
        {
            msSQL = " Select milletdocument_name,milletdocument_gid,file_path from mar_mst_tmilletdocumentupload where marketingcall_gid ='" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocument_list = new List<document_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["milletdocument_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["file_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdocument_list.Add(new document_list
                    {
                        document_name = (dr_datarow["milletdocument_name"].ToString()),
                        document_gid = (dr_datarow["milletdocument_gid"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["file_path"].ToString())),

                    });
                }
                values.milletdocument_list = getdocument_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetEnquiryDocumentList(string marketingcall_gid, MdlMarketingCall values)
        {
            msSQL = " Select enquirydocument_name,enquirydocument_gid,file_path from ldm_mst_tenquirydocumentupload where marketingcall_gid ='" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getenquirydocument_list = new List<enquirydocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["enquirydocument_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["file_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getenquirydocument_list.Add(new enquirydocument_list
                    {
                        document_name = (dr_datarow["enquirydocument_name"].ToString()),
                        document_gid = (dr_datarow["enquirydocument_gid"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["file_path"].ToString()),

                    });
                }
                values.enquirydocument_list = getenquirydocument_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetDocumentList(string marketingcall_gid, MdlMarketingCall values)
        {
            msSQL = " Select document_name,document_gid,file_path from mar_mst_tdocumentupload where marketingcall_gid ='" + marketingcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocument_list = new List<document_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["file_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdocument_list.Add(new document_list
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["document_gid"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["file_path"].ToString())),

                    });
                }
                values.document_list = getdocument_list;
            }
            dt_datatable.Dispose();
        }
    }
}