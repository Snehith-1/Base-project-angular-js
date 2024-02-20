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
using ems.master.Models;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ems.master.DataAccess
{
    public class DaTeleCalling
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetGid1, msGetGid2;
        int mnResult;
        string lsinboundcall2mobileno_gid, ls_inboundgid, ls_taggedmember, ls_taguser, lsmobile_no, lsprimary_status, lswhatsapp_status, lssms_to, lsinboundcall2email_gid, lsemail_address,
        lsinboundcall2followup_gid, lsfollowup_date, lsfollowup_time, lspath;
        string lsaddress_typegid, lsaddress_type, lsaddressline1, lsaddressline2, lslandmark, lstaluka, lspostal_code, lscity, lsdistrict,
              lsstate, lscountry, lslatitude, lslongitude, lsinboundcall_gid, lsinboundcall2address_gid;
        string lsticket_refid, lsentity_gid, lsentity_name, lssourceofcontact_gid, lssourceofcontact_name, lscallreceivednumber_gid, lscallreceivednumber_name,
            lscustomer_type, lscallreceived_date, lscaller_name, lsinternalreference_gid, lsinternalreference_name, lscallerassociate_company,
            lsoffice_landlineno, lscalltype_gid, lscalltype_name, lsfunction_gid, lsfunction_name, lsrequirement, lsenquiry_description,
            lscallclosure_status, lsassignemployee_gid, lsassignemployee_name, lsassignclosure_remarks, lsinboundcall_status, lsfunction_remarks,lstat_hours, lstat_date, lstat_days,
            lsassign_by, lsassign_date, lstransfer_by, lstransfer_date, lscompleted_by, lscompleted_date;
        public string ls_server, ls_assignemployee_name, ls_username, lsto2members, ls_password, tomail_id, sub, body, employeename, cc_mailid, employee_reporting_to;
        int ls_port;
        public string[] lsCCReceipients;
        public string lsBccmail_id;
        public string[] lsBCCReceipients;
        public string[] lstoReceipients;




        //Mobile No
        public bool DaPostIBCallMobileNo(string employee_gid, MdlIBCallMobileNo values)
        {
            msSQL = " select primary_status from ocs_trn_tinboundcall2mobileno where primary_status='Yes' and " +
                    " (inboundcall_gid='" + employee_gid + "' or inboundcall_gid='" + values.inboundcall_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }
            msSQL = "select inboundcall2mobileno_gid from ocs_trn_tinboundcall2mobileno where mobile_no='" + values.mobile_no + "' " +
                " and inboundcall_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("IC2M");
            msSQL = " insert into ocs_trn_tinboundcall2mobileno(" +
                    " inboundcall2mobileno_gid," +
                    " inboundcall_gid," +
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
        //

        //
        public void DaGetIBCallMobileNoList(string employee_gid, MdlIBCallMobileNo values)
        {
            msSQL = "select mobile_no,inboundcall2mobileno_gid,primary_status,whatsapp_status,sms_to from ocs_trn_tinboundcall2mobileno where " +
              " inboundcall_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcallmobileno_list = new List<ibcallmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcallmobileno_list.Add(new ibcallmobileno_list
                    {
                        inboundcall2mobileno_gid = (dr_datarow["inboundcall2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_status = (dr_datarow["whatsapp_status"].ToString()),
                        sms_to = (dr_datarow["sms_to"].ToString()),
                    });
                }
            }
            values.ibcallmobileno_list = getibcallmobileno_list;
            dt_datatable.Dispose();
        }

        public void DaIBCallMobileNoTempList(string inboundcall_gid, string employee_gid, MdlIBCallMobileNo values)
        {
            msSQL = "select mobile_no,inboundcall2mobileno_gid,primary_status,whatsapp_status,sms_to from ocs_trn_tinboundcall2mobileno where " +
              " inboundcall_gid = '" + employee_gid + "' or inboundcall_gid = '" + inboundcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcallmobileno_list = new List<ibcallmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcallmobileno_list.Add(new ibcallmobileno_list
                    {
                        inboundcall2mobileno_gid = (dr_datarow["inboundcall2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_status = (dr_datarow["whatsapp_status"].ToString()),
                        sms_to = (dr_datarow["sms_to"].ToString()),
                    });
                }
            }
            values.ibcallmobileno_list = getibcallmobileno_list;
            dt_datatable.Dispose();
        }

        public void DaIBCallMobileNoList(string inboundcall_gid, string employee_gid, MdlIBCallMobileNo values)
        {
            msSQL = "select mobile_no,inboundcall2mobileno_gid,primary_status,whatsapp_status,sms_to from ocs_trn_tinboundcall2mobileno where " +
              " inboundcall_gid = '" + inboundcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcallmobileno_list = new List<ibcallmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcallmobileno_list.Add(new ibcallmobileno_list
                    {
                        inboundcall2mobileno_gid = (dr_datarow["inboundcall2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_status = (dr_datarow["whatsapp_status"].ToString()),
                        sms_to = (dr_datarow["sms_to"].ToString()),
                    });
                }
            }
            values.ibcallmobileno_list = getibcallmobileno_list;
            dt_datatable.Dispose();
        }

        public void DaEditIBCallMobileNo(string inboundcall2mobileno_gid, MdlIBCallMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,inboundcall2mobileno_gid,primary_status,whatsapp_status,sms_to from ocs_trn_tinboundcall2mobileno where " +
                        " inboundcall2mobileno_gid='" + inboundcall2mobileno_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.whatsapp_status = objODBCDatareader["whatsapp_status"].ToString();
                    values.sms_to = objODBCDatareader["sms_to"].ToString();
                    values.inboundcall2mobileno_gid = objODBCDatareader["inboundcall2mobileno_gid"].ToString();
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

        public void DaUpdateIBCallMobileNo(string employee_gid, MdlIBCallMobileNo values)
        {
            msSQL = " select mobile_no,inboundcall2mobileno_gid,primary_status,whatsapp_status,sms_to from ocs_trn_tinboundcall2mobileno where " +
                    " inboundcall2mobileno_gid='" + values.inboundcall2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lswhatsapp_status = objODBCDatareader["whatsapp_status"].ToString();
                lssms_to = objODBCDatareader["sms_to"].ToString();
                lsinboundcall2mobileno_gid = objODBCDatareader["inboundcall2mobileno_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_trn_tinboundcall2mobileno set " +
                         " mobile_no='" + values.mobile_no + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " whatsapp_status='" + values.whatsapp_status + "'," +
                         " sms_to='" + values.sms_to + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where inboundcall2mobileno_gid='" + values.inboundcall2mobileno_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IBML");

                    msSQL = "Insert into ocs_trn_tinboundcall2mobilenoupdatelog(" +
                   " inboundcall2mobilenoupdatelog_gid, " +
                   " inboundcall2mobileno_gid, " +
                   " inboundcall_gid, " +
                   " mobile_no," +
                   " primary_status," +
                   " whatsapp_status," +
                   " sms_to," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + lsinboundcall2mobileno_gid + "'," +
                   "'" + values.inboundcall_gid + "'," +
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

        public void DaIBCallMobileNoDelete(string inboundcall2mobileno_gid, MdlIBCallMobileNo values)
        {
            msSQL = "delete from ocs_trn_tinboundcall2mobileno where inboundcall2mobileno_gid='" + inboundcall2mobileno_gid + "'";
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

        //Email
        public bool DaPostIBCallEmail(string employee_gid, MdlIBCallEmail values)
        {
            msSQL = " select primary_status from ocs_trn_tinboundcall2email where primary_status='Yes' and " +
                    " (inboundcall_gid='" + employee_gid + "' or inboundcall_gid='" + values.inboundcall_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select inboundcall2email_gid from ocs_trn_tinboundcall2email where email_address='" + values.email_address + "' " +
                " and inboundcall_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("IC2E");
            msSQL = " insert into ocs_trn_tinboundcall2email(" +
                    " inboundcall2email_gid," +
                    " inboundcall_gid," +
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

        public void DaGetIBCallEmailList(string employee_gid, MdlIBCallEmail values)
        {
            msSQL = "select email_address,inboundcall2email_gid,primary_status from ocs_trn_tinboundcall2email where " +
              " inboundcall_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcallemail_list = new List<ibcallemail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcallemail_list.Add(new ibcallemail_list
                    {
                        inboundcall2email_gid = (dr_datarow["inboundcall2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                    });
                }
            }
            values.ibcallemail_list = getibcallemail_list;
            dt_datatable.Dispose();
        }

        public void DaIBCallEmailTempList(string inboundcall_gid, string employee_gid, MdlIBCallEmail values)
        {
            msSQL = "select email_address,inboundcall2email_gid,primary_status from ocs_trn_tinboundcall2email where " +
              " inboundcall_gid = '" + employee_gid + "' or inboundcall_gid = '" + inboundcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcallemail_list = new List<ibcallemail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcallemail_list.Add(new ibcallemail_list
                    {
                        inboundcall2email_gid = (dr_datarow["inboundcall2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                    });
                }
            }
            values.ibcallemail_list = getibcallemail_list;
            dt_datatable.Dispose();
        }

        public void DaIBCallEmailList(string inboundcall_gid, string employee_gid, MdlIBCallEmail values)
        {
            msSQL = "select email_address,inboundcall2email_gid,primary_status from ocs_trn_tinboundcall2email where " +
              " inboundcall_gid = '" + inboundcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcallemail_list = new List<ibcallemail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcallemail_list.Add(new ibcallemail_list
                    {
                        inboundcall2email_gid = (dr_datarow["inboundcall2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                    });
                }
            }
            values.ibcallemail_list = getibcallemail_list;
            dt_datatable.Dispose();
        }

        public void DaEditIBCallEmail(string inboundcall2email_gid, MdlIBCallEmail values)
        {
            try
            {
                msSQL = " select email_address,inboundcall2email_gid,primary_status from ocs_trn_tinboundcall2email where " +
                        " inboundcall2email_gid='" + inboundcall2email_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.inboundcall2email_gid = objODBCDatareader["inboundcall2email_gid"].ToString();
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

        public void DaUpdateIBCallEmail(string employee_gid, MdlIBCallEmail values)
        {
            msSQL = " select email_address,inboundcall2email_gid,primary_status from ocs_trn_tinboundcall2email where " +
                    " inboundcall2email_gid='" + values.inboundcall2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lsinboundcall2email_gid = objODBCDatareader["inboundcall2email_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_trn_tinboundcall2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where inboundcall2email_gid='" + values.inboundcall2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IBEL");

                    msSQL = "Insert into ocs_trn_tinboundcall2emailupdatelog(" +
                   " inboundcall2emailupdatelog_gid, " +
                   " inboundcall2email_gid, " +
                   " inboundcall_gid, " +
                   " email_address," +
                   " primary_status," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + lsinboundcall2email_gid + "'," +
                   "'" + values.inboundcall_gid + "'," +
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

        public void DaIBCallEmailDelete(string inboundcall2email_gid, MdlIBCallEmail values)
        {
            msSQL = "delete from ocs_trn_tinboundcall2email where inboundcall2email_gid='" + inboundcall2email_gid + "'";
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

        //Follow Up

        public bool DaPostIBCallFollowUp(string employee_gid, MdlIBCallFollowUp values)
        {

            /*  msSQL = "select inboundcall2followup_gid from ocs_mst_tinboundcall2followup where email_address='" + values.email_address + "' " +
                  " and inboundcall_gid='" + employee_gid + "'";
              objODBCDatareader = objdbconn.GetDataReader(msSQL);
              if (objODBCDatareader.HasRows)
              {
                  objODBCDatareader.Close();
                  values.status = false;
                  values.message = "Already This Email Address Added";
                  return false;
              } */
            msGetGid = objcmnfunctions.GetMasterGID("IB2F");
            msSQL = " insert into ocs_trn_tinboundcall2followup(" +
                    " inboundcall2followup_gid," +
                    " inboundcall_gid," +
                    " followup_date," +
                    " followup_time," +
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

            msSQL += "'" + employee_gid + "'," +
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

        public void DaGetIBCallFollowUpList(string employee_gid, MdlIBCallFollowUp values)
        {
            msSQL = "select date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%H:%i') as followup_time," +
                " inboundcall2followup_gid from ocs_trn_tinboundcall2followup where " +
                " inboundcall_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcallfollowup_list = new List<ibcallfollowup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcallfollowup_list.Add(new ibcallfollowup_list
                    {
                        inboundcall2followup_gid = (dr_datarow["inboundcall2followup_gid"].ToString()),
                        followup_date = (dr_datarow["followup_date"].ToString()),
                        followup_time = (dr_datarow["followup_time"].ToString()),
                    });
                }
            }
            values.ibcallfollowup_list = getibcallfollowup_list;
            dt_datatable.Dispose();
        }

        public void DaIBCallFollowUpTempList(string inboundcall_gid, string employee_gid, MdlIBCallFollowUp values)
        {
            msSQL = "select date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%H:%i') as followup_time," +
                " inboundcall2followup_gid from ocs_trn_tinboundcall2followup where " +
                " inboundcall_gid = '" + employee_gid + "' or inboundcall_gid = '" + inboundcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcallfollowup_list = new List<ibcallfollowup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcallfollowup_list.Add(new ibcallfollowup_list
                    {
                        inboundcall2followup_gid = (dr_datarow["inboundcall2followup_gid"].ToString()),
                        followup_date = (dr_datarow["followup_date"].ToString()),
                        followup_time = (dr_datarow["followup_time"].ToString()),
                    });
                }
            }
            values.ibcallfollowup_list = getibcallfollowup_list;
            dt_datatable.Dispose();
        }

        public void DaIBCallFollowUpList(string inboundcall_gid, string employee_gid, MdlIBCallFollowUp values)
        {
            msSQL = "select date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%H:%i') as followup_time," +
                " inboundcall2followup_gid from ocs_trn_tinboundcall2followup where " +
              " inboundcall_gid = '" + inboundcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcallfollowup_list = new List<ibcallfollowup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcallfollowup_list.Add(new ibcallfollowup_list
                    {
                        inboundcall2followup_gid = (dr_datarow["inboundcall2followup_gid"].ToString()),
                        followup_date = (dr_datarow["followup_date"].ToString()),
                        followup_time = (dr_datarow["followup_time"].ToString()),
                    });
                }
            }
            values.ibcallfollowup_list = getibcallfollowup_list;
            dt_datatable.Dispose();
        }

        public void DaEditIBCallFollowUp(string inboundcall2followup_gid, MdlIBCallFollowUp values)
        {
            try
            {
                msSQL = " select date_format(followup_date,'%Y-%m-%d') as followup_date,followup_time," +
                        " inboundcall2followup_gid from ocs_trn_tinboundcall2followup where " +
                        " inboundcall2followup_gid='" + inboundcall2followup_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.followup_date = objODBCDatareader["followup_date"].ToString();
                    values.followup_time = objODBCDatareader["followup_time"].ToString();
                    values.inboundcall2followup_gid = objODBCDatareader["inboundcall2followup_gid"].ToString();
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

        public void DaUpdateIBCallFollowUp(string employee_gid, MdlIBCallFollowUp values)
        {
            msSQL = " select followup_date,followup_time,inboundcall2followup_gid from ocs_trn_tinboundcall2followup where " +
                    " inboundcall2followup_gid='" + values.inboundcall2followup_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsfollowup_date = objODBCDatareader["followup_date"].ToString();
                lsfollowup_time = objODBCDatareader["followup_time"].ToString();
                lsinboundcall2followup_gid = objODBCDatareader["inboundcall2followup_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_trn_tinboundcall2followup set ";
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

                msSQL += " updated_by='" + employee_gid + "'," +
                              " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                              " where inboundcall2followup_gid='" + values.inboundcall2followup_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IBFL");

                    msSQL = "Insert into ocs_trn_tinboundcall2followupupdatelog(" +
                   " ocs_trn_tinboundcall2followupupdatelog_gid, " +
                   " inboundcall2followup_gid, " +
                   " inboundcall_gid, " +
                   " followup_date," +
                   " followup_time," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + lsinboundcall2followup_gid + "'," +
                   "'" + values.inboundcall_gid + "'," +
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

        public void DaIBCallFollowUpDelete(string inboundcall2followup_gid, MdlIBCallFollowUp values)
        {
            msSQL = "delete from ocs_trn_tinboundcall2followup where inboundcall2followup_gid='" + inboundcall2followup_gid + "'";
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

        public bool DaPostIBCallAddress(string employee_gid, MdlIBCallAddress values)
        {
            msSQL = "select primary_status from ocs_trn_tinboundcall2address where primary_status='Yes' and (inboundcall_gid='" + employee_gid + "' or inboundcall_gid='" + values.inboundcall_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }

            msSQL = "select inboundcall2address_gid from ocs_trn_tinboundcall2address where addresstype_name='" + values.addresstype_name + "' and " +
               " (inboundcall_gid='" + employee_gid + "' or inboundcall_gid='" + values.inboundcall_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("IC2A");
            msSQL = " insert into ocs_trn_tinboundcall2address(" +
                    " inboundcall2address_gid," +
                    " inboundcall_gid," +
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
                    "'" + values.addressline1 + "'," +
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

        public void DaGetIBCallAddressList(string employee_gid, MdlIBCallAddress values)
        {
            msSQL = " select inboundcall2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from ocs_trn_tinboundcall2address where inboundcall_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcalladdress_list = new List<ibcalladdress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcalladdress_list.Add(new ibcalladdress_list
                    {
                        inboundcall2address_gid = (dr_datarow["inboundcall2address_gid"].ToString()),
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
                values.ibcalladdress_list = getibcalladdress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaIBCallAddressTempList(string inboundcall_gid, string employee_gid, MdlIBCallAddress values)
        {
            msSQL = " select inboundcall2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from ocs_trn_tinboundcall2address where inboundcall_gid='" + employee_gid + "' or inboundcall_gid = '" + inboundcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcalladdress_list = new List<ibcalladdress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcalladdress_list.Add(new ibcalladdress_list
                    {
                        inboundcall2address_gid = (dr_datarow["inboundcall2address_gid"].ToString()),
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
                values.ibcalladdress_list = getibcalladdress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaIBCallAddressList(string inboundcall_gid, string employee_gid, MdlIBCallAddress values)
        {
            msSQL = " select inboundcall2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from ocs_trn_tinboundcall2address where inboundcall_gid = '" + inboundcall_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getibcalladdress_list = new List<ibcalladdress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getibcalladdress_list.Add(new ibcalladdress_list
                    {
                        inboundcall2address_gid = (dr_datarow["inboundcall2address_gid"].ToString()),
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
                values.ibcalladdress_list = getibcalladdress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditIBCallAddress(string inboundcall2address_gid, MdlIBCallAddress values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, inboundcall_gid, inboundcall2address_gid " +
                    " from ocs_trn_tinboundcall2address where inboundcall2address_gid='" + inboundcall2address_gid + "'";
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
                    values.inboundcall_gid = objODBCDatareader["inboundcall_gid"].ToString();
                    values.inboundcall2address_gid = objODBCDatareader["inboundcall2address_gid"].ToString();
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

        public void DaUpdateIBCallAddress(string employee_gid, MdlIBCallAddress values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, inboundcall_gid, inboundcall2address_gid " +
                    " from ocs_trn_tinboundcall2address where inboundcall2address_gid='" + values.inboundcall2address_gid + "'";
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
                lsinboundcall_gid = objODBCDatareader["inboundcall_gid"].ToString();
                lsinboundcall2address_gid = objODBCDatareader["inboundcall2address_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_trn_tinboundcall2address set " +
                         " addresstype_gid='" + values.addresstype_gid + "'," +
                         " addresstype_name='" + values.addresstype_name + "'," +
                         " addressline1='" + values.addressline1 + "'," +
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
                         " where inboundcall2address_gid='" + values.inboundcall2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IBAL");

                    msSQL = " insert into ocs_trn_tinboundcall2addressupdatelog(" +
                  " inboundcall2addressupdatelog_gid," +
                  " inboundcall2address_gid," +
                  " inboundcall_gid," +
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
                  "'" + lsinboundcall2address_gid + "'," +
                  "'" + lsinboundcall_gid + "'," +
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

        public void DaIBCallAddressDelete(string inboundcall2address_gid, MdlIBCallAddress values)
        {
            msSQL = "delete from ocs_trn_tinboundcall2address where inboundcall2address_gid='" + inboundcall2address_gid + "'";
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

        public void DaIBCallSave(string employee_gid, MdlIBCall values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("ICAL");

            msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + values.entity_gid + "'";
            string lsentity_code = objdbconn.GetExecuteScalar(msSQL);

            string msGETRef = objcmnfunctions.GetMasterGID("ICA");
            msGETRef = msGETRef.Replace("ICA", "");

            string lsticket_refid = "ICA" + DateTime.Now.ToString("ddMMyyyy") + lsentity_code + msGETRef;

            msSQL = " insert into ocs_trn_tinboundcall(" +
                   " inboundcall_gid," +
                   " ticket_refid," +
                   " entity_gid," +
                   " entity_name," +
                   " sourceofcontact_gid," +
                   " sourceofcontact_name," +
                   " callreceivednumber_gid," +
                   " callreceivednumber_name," +
                   " customer_type," +
                   " callreceived_date," +
                   " caller_name," +
                   " internalreference_gid," +
                   " internalreference_name," +
                   " callerassociate_company," +
                   " office_landlineno," +
                   " calltype_gid," +
                   " calltype_name," +
                   " function_gid," +
                   " function_name," +
                   " requirement," +
                   " enquiry_description," +
                   " callclosure_status," +
                   " assignemployee_gid," +
                   " assignemployee_name," +
                   " assign_by," +
                   " assign_date," +
                   " assignclosure_remarks," +
                   " inboundcall_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + lsticket_refid + "'," +
                   "'" + values.entity_gid + "'," +
                   "'" + values.entity_name + "'," +
                   "'" + values.sourceofcontact_gid + "'," +
                   "'" + values.sourceofcontact_name + "'," +
                   "'" + values.callreceivednumber_gid + "'," +
                   "'" + values.callreceivednumber_name + "'," +
                   "'" + values.customer_type + "'," +
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
                     "'" + values.calltype_gid + "'," +
                     "'" + values.calltype_name + "'," +
                     "'" + values.function_gid + "'," +
                     "'" + values.function_name + "'," +
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
                        msGetGid1 = objcmnfunctions.GetMasterGID("ICTM");
                        msSQL = " insert into ocs_trn_tinboundcall2taggedmember(" +
                                " inboundcall2taggedmember_gid," +
                                " inboundcall_gid," +
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

                msSQL = "update ocs_trn_tinboundcall2mobileno set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2email set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2followup set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2address set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Inbound Call Details Saved Sucessfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaIBCallSubmit(string employee_gid, MdlIBCall values)
        {

            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            DateTime lsmailalert = datevalue.AddHours(Convert.ToDouble(values.tat_hours));

            //   DateTime lsmailalert = datevalue.AddHours(Convert.ToDouble(values.tat_hours));
           if (values.callclosure_status == "Follow Up")
            {
                msSQL = "select inboundcall2followup_gid from ocs_trn_tinboundcall2followup where inboundcall_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Enter Follow Up Details";
                    return;
                }
            }

            msSQL = "select inboundcall_gid " + " from ocs_trn_tinboundcall2mobileno where inboundcall_gid ='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return;
            }
            msSQL = "select inboundcall_gid " + " from ocs_trn_tinboundcall2email where inboundcall_gid ='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email ID";
                return;
            }
            msSQL = "select inboundcall_gid " + " from ocs_trn_tinboundcall2address where inboundcall_gid ='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Address Details";
                return;
            }
           // msGetGid = objcmnfunctions.GetMasterGID("IBCL");
            msGetGid = objcmnfunctions.GetMasterGID("ICAL");

            msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + values.entity_gid + "'";
            string lsentity_code = objdbconn.GetExecuteScalar(msSQL);

            string msGETRef = objcmnfunctions.GetMasterGID("ICA");
            msGETRef = msGETRef.Replace("ICA", "");

            string lsticket_refid = "ICA" + DateTime.Now.ToString("ddMMyyyy") + lsentity_code + msGETRef;

            msSQL = " insert into ocs_trn_tinboundcall(" +
                 " inboundcall_gid," +
                 " ticket_refid," +
                 " entity_gid," +
                 " entity_name," +
                 " sourceofcontact_gid," +
                 " sourceofcontact_name," +
                 " callreceivednumber_gid," +
                 " callreceivednumber_name," +
                 " customer_type," +
                 " callreceived_date," +
                 " caller_name," +
                 " internalreference_gid," +
                 " internalreference_name," +
                 " callerassociate_company," +
                  " mail_alert," +
                 " office_landlineno," +
                 " calltype_gid," +
                 " calltype_name," +
                 " function_gid," +
                 " function_name," +
                 " function_remarks," +
                 " requirement," +
                 " enquiry_description," +
                 " callclosure_status," +
                 "callassign_status," +
                 " assignemployee_gid," +
                 " assignemployee_name," +
                 " tat_hours," +
                 " tat_date," +
                 " tat_days," +
                 " assign_by," +
                 " assign_date," +
                 " assignclosure_remarks," +
                 " inboundcall_status," +
                 " created_by," +
                 " created_date)" +
                 " values(" +
                 "'" + msGetGid + "'," +
                 "'" + lsticket_refid + "'," +
                 "'" + values.entity_gid + "'," +
                 "'" + values.entity_name + "'," +
                 "'" + values.sourceofcontact_gid + "'," +
                 "'" + values.sourceofcontact_name + "'," +
                 "'" + values.callreceivednumber_gid + "'," +
                 "'" + values.callreceivednumber_name + "'," +
                 "'" + values.customer_type + "'," +
                 "'" + values.callreceived_date + "'," +
                 "'" + values.caller_name.Replace("'", "") + "'," +
                 "'" + values.internalreference_gid + "'," +
                 "'" + values.internalreference_name + "',";
            if (values.callerassociate_company == "" || values.callerassociate_company == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.callerassociate_company.Replace("'", "") + "',";
            }
            /*if (values.tat_hours == null || values.tat_hours == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.tat_hours).ToString("yyyy-MM-dd") + "',";
            }*/
            if (lsmailalert == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(lsmailalert).ToString("yyyy-MM-dd HH:mm") + "',";
            }
            msSQL += "'" + values.office_landlineno + "'," +
                     "'" + values.calltype_gid + "'," +
                     "'" + values.calltype_name + "'," +
                     "'" + values.function_gid + "'," +
                     "'" + values.function_name + "'," +
                     "'" + values.function_remarks + "'," +
                     "'" + values.requirement.Replace("'", "") + "'," +
                     "'" + values.enquiry_description.Replace("'", "") + "'," +
                     "'" + values.callclosure_status + "'," +
                     "'" + values.callclosure_status + "'," +
                     "'" + values.assignemployee_gid + "'," +
                     "'" + values.assignemployee_name + "'," +
                     "'" + values.tat_hours + "'," +
                       "'" + values.tat_date + "'," +
                       "'" + values.tat_days + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     "'" + values.assignclosure_remarks.Replace("'", "") + "'," +
                     "'Completed'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_tinboundcall2mobileno set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2email set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2followup set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2address set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Inbound Call Details Submitted Successfully";

                if (values.callclosure_status == "Follow Up")
                {
                    msSQL = " update ocs_trn_tinboundcall set followup_remarks = '" + values.assignclosure_remarks.Replace("'", "") + "'," +
                            " followup_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " followup_by = '" + employee_gid + "'" +
                            " where inboundcall_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
                    msSQL = "Insert into ocs_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " inboundcall_gid," +
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
                            msGetGid1 = objcmnfunctions.GetMasterGID("ICTM");
                            msSQL = " insert into ocs_trn_tinboundcall2taggedmember(" +
                                    " inboundcall2taggedmember_gid," +
                                    " inboundcall_gid," +
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



                        msSQL = " update ocs_trn_tinboundcall2taggedmember set taggeduser_flag='Y'" +
                                   " where inboundcall_gid = '" + msGetGid + "'";
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

                        msSQL = " select group_concat(taggedmember_gid) as taguser  from ocs_trn_tinboundcall2taggedmember where inboundcall_gid = '" + msGetGid + "'";
                        ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taguser.Replace(",", "', '") + "')";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                        "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Inbound Call Ticket FollowUp - " + HttpUtility.HtmlEncode(values.requirement.Replace("'", "")) + " ";
                        body = "Hi " + HttpUtility.HtmlEncode(values.assignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been FollowUp.<br><br>";
                        body = body + "Caller Name:" + HttpUtility.HtmlEncode(values.caller_name.Replace("'", "")) + "<br><br>";
                        body = body + "Requirement Title:" + HttpUtility.HtmlEncode(values.requirement.Replace("'", "")) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Inbound - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                            msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                               " inboundcall_gid," +
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
                    msSQL = " update ocs_trn_tinboundcall set assigningclosure_remarks='" + values.assignclosure_remarks.Replace("'", "") + "'," +
                            " closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " closed_flag = 'Y'," +
                            " closed_by = '" + employee_gid + "'" +
                            " where inboundcall_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
                    msSQL = "Insert into ocs_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " inboundcall_gid," +
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
                               "'" + values.assignclosure_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.tagemployee_list != null)
                    {

                        for (var i = 0; i < values.tagemployee_list.Count; i++)
                        {
                            msGetGid1 = objcmnfunctions.GetMasterGID("ICTM");
                            msSQL = " insert into ocs_trn_tinboundcall2taggedmember(" +
                                    " inboundcall2taggedmember_gid," +
                                    " inboundcall_gid," +
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

                        msSQL = " update ocs_trn_tinboundcall2taggedmember set taggeduser_flag='Y'" +
                                   " where inboundcall_gid = '" + msGetGid + "'";
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

                        msSQL = " select group_concat(taggedmember_gid) as taguser  from ocs_trn_tinboundcall2taggedmember where inboundcall_gid = '" + msGetGid + "'";
                        ls_taguser = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taguser.Replace(",", "', '") + "')";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                        "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Inbound Call Ticket Closed - " + HttpUtility.HtmlEncode(values.requirement.Replace("'", "")) + " ";
                        body = "Hi " + HttpUtility.HtmlEncode(values.assignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Closed.<br><br>";
                        body = body + "Caller Name:" + HttpUtility.HtmlEncode(values.caller_name.Replace("'", "")) + "<br><br>";
                        body = body + "Requirement Title:" + HttpUtility.HtmlEncode(values.requirement.Replace("'", "")) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Inbound - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                            msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                               " inboundcall_gid," +
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
                    msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
                    msSQL = "Insert into ocs_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " inboundcall_gid," +
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
                            msGetGid1 = objcmnfunctions.GetMasterGID("ICTM");
                            msSQL = " insert into ocs_trn_tinboundcall2taggedmember(" +
                                    " inboundcall2taggedmember_gid," +
                                    " inboundcall_gid," +
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

                        msSQL = " update ocs_trn_tinboundcall2taggedmember set taggeduser_flag='Y'" +
                                       " where inboundcall_gid = '" + msGetGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from ocs_trn_tinboundcall2taggedmember" +
                             " where inboundcall_gid ='" + msGetGid + "'";
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

                    sub = " A Inbound Call Ticket Assigned - " + HttpUtility.HtmlEncode(values.requirement.Replace("'", "")) + " ";
                    body = "Hi " + HttpUtility.HtmlEncode(values.assignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been assigned to you.<br><br>";
                    body = body + "Caller Name:" + HttpUtility.HtmlEncode(values.caller_name.Replace("'", "")) + "<br><br>";
                    body = body + "Requirement Title:" + HttpUtility.HtmlEncode(values.requirement.Replace("'", "")) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Assigned by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                    body = body + "Assigned time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Inbound - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                        msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                           " inboundcall_gid," +
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

                //msSQL = "update ocs_trn_tinboundcall2mobileno set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update ocs_trn_tinboundcall2email set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update ocs_trn_tinboundcall2followup set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update ocs_trn_tinboundcall2address set inboundcall_gid ='" + msGetGid + "' where inboundcall_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //values.status = true;
                //values.message = "Inbound Call Details Submitted Sucessfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                        " if(callclosure_status = 'Work-In-Progress', callclosure_status ,'Acknowledge-Pending') as callclosure_status " +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where (a.callclosure_status = 'Assign' or a.callclosure_status = '' or a.callclosure_status = 'Work-In-Progress') and a.created_by = '" + employee_gid + "'" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaEditIBCall(string inboundcall_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " select ticket_refid,entity_gid,entity_name,sourceofcontact_gid,sourceofcontact_name,callreceivednumber_gid,callreceivednumber_name," +
                        " customer_type,callreceived_date,caller_name,internalreference_gid,internalreference_name," +
                        " callerassociate_company,office_landlineno,calltype_gid,calltype_name,function_gid,function_name,function_remarks,tat_hours,tat_date,tat_days," +
                        " requirement,enquiry_description,callclosure_status,assignemployee_gid,assignemployee_name," +
                        " date_format(assign_date,'%d-%m-%Y %h:%i %p') as assign_date,assignclosure_remarks,inboundcall_status" +
                        " from ocs_trn_tinboundcall where inboundcall_gid='" + inboundcall_gid + "'";


                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                    values.entity_name = objODBCDatareader["entity_name"].ToString();
                    values.sourceofcontact_gid = objODBCDatareader["sourceofcontact_gid"].ToString();
                    values.sourceofcontact_name = objODBCDatareader["sourceofcontact_name"].ToString();
                    values.callreceivednumber_gid = objODBCDatareader["callreceivednumber_gid"].ToString();
                    values.callreceivednumber_name = objODBCDatareader["callreceivednumber_name"].ToString();
                    values.customer_type = objODBCDatareader["customer_type"].ToString();
                    values.callreceived_date = objODBCDatareader["callreceived_date"].ToString();
                    values.caller_name = objODBCDatareader["caller_name"].ToString();
                    values.internalreference_gid = objODBCDatareader["internalreference_gid"].ToString();
                    values.internalreference_name = objODBCDatareader["internalreference_name"].ToString();
                    values.callerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                    values.office_landlineno = objODBCDatareader["office_landlineno"].ToString();
                    values.calltype_gid = objODBCDatareader["calltype_gid"].ToString();
                    values.calltype_name = objODBCDatareader["calltype_name"].ToString();
                    values.function_gid = objODBCDatareader["function_gid"].ToString();
                    values.function_name = objODBCDatareader["function_name"].ToString();
                    values.function_remarks = objODBCDatareader["function_remarks"].ToString();
                    values.requirement = objODBCDatareader["requirement"].ToString();
                    values.enquiry_description = objODBCDatareader["enquiry_description"].ToString();
                    values.callclosure_status = objODBCDatareader["callclosure_status"].ToString();
                    values.assignemployee_gid = objODBCDatareader["assignemployee_gid"].ToString();
                    values.assignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                    values.tat_hours = objODBCDatareader["tat_hours"].ToString();
                    values.tat_date = objODBCDatareader["tat_date"].ToString();
                    values.tat_days = objODBCDatareader["tat_days"].ToString();
                    values.assign_date = objODBCDatareader["assign_date"].ToString();

                    msSQL = "select taggedmember_gid,taggedmember_name from ocs_trn_tinboundcall2taggedmember where inboundcall_gid='" + inboundcall_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    values.tagemployee_list = dt_datatable.AsEnumerable().Select(row =>
               new tagemployee_list
               {
                   employee_gid = row["taggedmember_gid"].ToString(),
                   employee_name = row["taggedmember_name"].ToString()
               }).ToList();
                    dt_datatable.Dispose();

                    values.assignclosure_remarks = objODBCDatareader["assignclosure_remarks"].ToString();
                    values.inboundcall_status = objODBCDatareader["inboundcall_status"].ToString();
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

        public void DaIBCallEditSave(MdlIBCall values, string employee_gid)
        {
            msSQL = " select ticket_refid,entity_gid,entity_name,sourceofcontact_gid,sourceofcontact_name,callreceivednumber_gid,callreceivednumber_name," +
                        " customer_type,callreceived_date,caller_name,internalreference_gid,internalreference_name," +
                        " callerassociate_company,office_landlineno,calltype_gid,calltype_name,function_gid,function_name,function_remarks,tat_hours," +
                        " requirement,enquiry_description,callclosure_status,assignemployee_gid,assignemployee_name," +
                        " assign_by,assign_date,transfer_by,transfer_date,completed_by,completed_date," +
                        " assignclosure_remarks,inboundcall_status" +
                        " from ocs_trn_tinboundcall where inboundcall_gid='" + values.inboundcall_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                lsentity_gid = objODBCDatareader["entity_gid"].ToString();
                lsentity_name = objODBCDatareader["entity_name"].ToString();
                lssourceofcontact_gid = objODBCDatareader["sourceofcontact_gid"].ToString();
                lssourceofcontact_name = objODBCDatareader["sourceofcontact_name"].ToString();
                lscallreceivednumber_gid = objODBCDatareader["callreceivednumber_gid"].ToString();
                lscallreceivednumber_name = objODBCDatareader["callreceivednumber_name"].ToString();
                lscustomer_type = objODBCDatareader["customer_type"].ToString();
                lscallreceived_date = objODBCDatareader["callreceived_date"].ToString();
                lscaller_name = objODBCDatareader["caller_name"].ToString();
                lsinternalreference_gid = objODBCDatareader["internalreference_gid"].ToString();
                lsinternalreference_name = objODBCDatareader["internalreference_name"].ToString();
                lscallerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                lsoffice_landlineno = objODBCDatareader["office_landlineno"].ToString();
                lscalltype_gid = objODBCDatareader["calltype_gid"].ToString();
                lscalltype_name = objODBCDatareader["calltype_name"].ToString();
                lsfunction_gid = objODBCDatareader["function_gid"].ToString();
                lsfunction_name = objODBCDatareader["function_name"].ToString();
                lsfunction_remarks = objODBCDatareader["function_remarks"].ToString();
                lsrequirement = objODBCDatareader["requirement"].ToString();
                lsenquiry_description = objODBCDatareader["enquiry_description"].ToString();
                lscallclosure_status = objODBCDatareader["callclosure_status"].ToString();
                lsassignemployee_gid = objODBCDatareader["assignemployee_gid"].ToString();
                lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                lstat_hours = objODBCDatareader["tat_hours"].ToString();
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
                lsinboundcall_status = objODBCDatareader["inboundcall_status"].ToString();

            }
            objODBCDatareader.Close();


            msSQL = " update ocs_trn_tinboundcall set " +
                      " entity_gid='" + values.entity_gid + "'," +
                      " entity_name='" + values.entity_name + "'," +
                      " sourceofcontact_gid='" + values.sourceofcontact_gid + "'," +
                      " sourceofcontact_name='" + values.sourceofcontact_name + "'," +
                      " callreceivednumber_gid='" + values.callreceivednumber_gid + "'," +
                      " callreceivednumber_name='" + values.callreceivednumber_name + "'," +
                      " customer_type='" + values.customer_type + "'," +
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
                      " calltype_gid='" + values.calltype_gid + "'," +
                      " calltype_name='" + values.calltype_name + "'," +
                      " function_gid='" + values.function_gid + "'," +
                      " function_name='" + values.function_name + "'," +
                      " function_remarks='" + values.function_remarks + "'," +
                      " requirement='" + values.requirement + "'," +
                      " enquiry_description='" + values.enquiry_description + "'," +
                      " callclosure_status='" + values.callclosure_status + "'," +
                      " assignemployee_gid='" + values.assignemployee_gid + "'," +
                      " assignemployee_name='" + values.assignemployee_name + "'," +
                      " tat_hours='" + values.tat_hours + "'," +
                      " assign_by='" + employee_gid + "'," +
                      " assign_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " assignclosure_remarks='" + values.assignclosure_remarks + "'," +
                      " inboundcall_status='" + "Incomplete" + "'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where inboundcall_gid='" + values.inboundcall_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "select taggedmember_gid,taggedmember_name from ocs_trn_tinboundcall2taggedmember where inboundcall_gid='" + values.inboundcall_gid + "'";
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
                        msGetGid1 = objcmnfunctions.GetMasterGID("ICTM");
                        msSQL = " insert into ocs_trn_tinboundcall2taggedmember(" +
                                " inboundcall2taggedmember_gid," +
                                " inboundcall_gid," +
                                " taggedmember_gid," +
                                " taggedmember_name," +
                                " tagged_by," +
                                " tagged_date," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.inboundcall_gid + "'," +
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
                        msSQL = "select inboundcall2taggedmember_gid from ocs_trn_tinboundcall2taggedmember where taggedmember_gid='" + existingtagemployee_list[i].employee_gid + "' and inboundcall_gid = '" + values.inboundcall_gid + "'";
                        string lsinboundcall2taggedmember_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "delete from ocs_trn_tinboundcall2taggedmember where inboundcall2taggedmember_gid='" + lsinboundcall2taggedmember_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                //Update Log
                msGetGid = objcmnfunctions.GetMasterGID("ICUL");
                msSQL = " insert into ocs_trn_tinboundcallupdatelog(" +
                   " inboundcallupdatelog_gid," +
                   " inboundcall_gid," +
                   " ticket_refid," +
                   " entity_gid," +
                   " entity_name," +
                   " sourceofcontact_gid," +
                   " sourceofcontact_name," +
                   " callreceivednumber_gid," +
                   " callreceivednumber_name," +
                   " customer_type," +
                   " callreceived_date," +
                   " caller_name," +
                   " internalreference_gid," +
                   " internalreference_name," +
                   " callerassociate_company," +
                   " office_landlineno," +
                   " calltype_gid," +
                   " calltype_name," +
                   " function_gid," +
                   " function_name," +
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
                   " inboundcall_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.inboundcall_gid + "'," +
                   "'" + lsticket_refid + "'," +
                   "'" + lsentity_gid + "'," +
                   "'" + lsentity_name + "'," +
                   "'" + lssourceofcontact_gid + "'," +
                   "'" + lssourceofcontact_name + "'," +
                   "'" + lscallreceivednumber_gid + "'," +
                   "'" + lscallreceivednumber_name + "'," +
                   "'" + lscustomer_type + "'," +
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
                   "'" + lsinboundcall_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates

                msSQL = "update ocs_trn_tinboundcall2mobileno set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2email set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2followup set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2address set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Inbound Call Details Saved Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaIBCallEditSubmit(MdlIBCall values, string employee_gid)
        {
            if (values.callclosure_status == "Follow Up")
            {
                msSQL = "select inboundcall2followup_gid from ocs_trn_tinboundcall2followup where inboundcall_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Enter Follow Up Details";
                    return;
                }
            }

            msSQL = " select ticket_refid,entity_gid,entity_name,sourceofcontact_gid,sourceofcontact_name,callreceivednumber_gid,callreceivednumber_name," +
                        " customer_type,callreceived_date,caller_name,internalreference_gid,internalreference_name," +
                        " callerassociate_company,office_landlineno,calltype_gid,calltype_name,function_gid,function_name,function_remarks,tat_hours," +
                        " requirement,enquiry_description,callclosure_status,assignemployee_gid,assignemployee_name," +
                        " assign_by,assign_date,transfer_by,transfer_date,completed_by,completed_date," +
                        " assignclosure_remarks,inboundcall_status" +
                        " from ocs_trn_tinboundcall where inboundcall_gid='" + values.inboundcall_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                lsentity_gid = objODBCDatareader["entity_gid"].ToString();
                lsentity_name = objODBCDatareader["entity_name"].ToString();
                lssourceofcontact_gid = objODBCDatareader["sourceofcontact_gid"].ToString();
                lssourceofcontact_name = objODBCDatareader["sourceofcontact_name"].ToString();
                lscallreceivednumber_gid = objODBCDatareader["callreceivednumber_gid"].ToString();
                lscallreceivednumber_name = objODBCDatareader["callreceivednumber_name"].ToString();
                lscustomer_type = objODBCDatareader["customer_type"].ToString();
                lscallreceived_date = objODBCDatareader["callreceived_date"].ToString();
                lscaller_name = objODBCDatareader["caller_name"].ToString();
                lsinternalreference_gid = objODBCDatareader["internalreference_gid"].ToString();
                lsinternalreference_name = objODBCDatareader["internalreference_name"].ToString();
                lscallerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                lsoffice_landlineno = objODBCDatareader["office_landlineno"].ToString();
                lscalltype_gid = objODBCDatareader["calltype_gid"].ToString();
                lscalltype_name = objODBCDatareader["calltype_name"].ToString();
                lsfunction_gid = objODBCDatareader["function_gid"].ToString();
                lsfunction_name = objODBCDatareader["function_name"].ToString();
                lsfunction_remarks = objODBCDatareader["function_remarks"].ToString();
                lsrequirement = objODBCDatareader["requirement"].ToString();
                lsenquiry_description = objODBCDatareader["enquiry_description"].ToString();
                lscallclosure_status = objODBCDatareader["callclosure_status"].ToString();
                lsassignemployee_gid = objODBCDatareader["assignemployee_gid"].ToString();
                lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                lstat_hours = objODBCDatareader["tat_hours"].ToString();
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
                lsinboundcall_status = objODBCDatareader["inboundcall_status"].ToString();

            }
            objODBCDatareader.Close();


            msSQL = " update ocs_trn_tinboundcall set " +
                      " entity_gid='" + values.entity_gid + "'," +
                      " entity_name='" + values.entity_name + "'," +
                      " sourceofcontact_gid='" + values.sourceofcontact_gid + "'," +
                      " sourceofcontact_name='" + values.sourceofcontact_name + "'," +
                      " callreceivednumber_gid='" + values.callreceivednumber_gid + "'," +
                      " callreceivednumber_name='" + values.callreceivednumber_name + "'," +
                      " customer_type='" + values.customer_type + "'," +
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
                      " calltype_gid='" + values.calltype_gid + "'," +
                      " calltype_name='" + values.calltype_name + "'," +
                      " function_gid='" + values.function_gid + "'," +
                      " function_name='" + values.function_name + "'," +
                      " function_remarks='" + values.function_remarks + "'," +
                      " requirement='" + values.requirement + "'," +
                      " enquiry_description='" + values.enquiry_description + "'," +
                      " callclosure_status='" + values.callclosure_status + "'," +
                      " assignemployee_gid='" + values.assignemployee_gid + "'," +
                      " assignemployee_name='" + values.assignemployee_name + "'," +
                      " tat_hours='" + values.tat_hours + "'," +
                      " assign_by='" + employee_gid + "'," +
                      " assign_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " assignclosure_remarks='" + values.assignclosure_remarks + "'," +
                      " inboundcall_status='" + "Completed" + "'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where inboundcall_gid='" + values.inboundcall_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "select taggedmember_gid,taggedmember_name from ocs_trn_tinboundcall2taggedmember where inboundcall_gid='" + values.inboundcall_gid + "'";
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
                        msGetGid1 = objcmnfunctions.GetMasterGID("ICTM");
                        msSQL = " insert into ocs_trn_tinboundcall2taggedmember(" +
                                " inboundcall2taggedmember_gid," +
                                " inboundcall_gid," +
                                " taggedmember_gid," +
                                " taggedmember_name," +
                                " tagged_by," +
                                " tagged_date," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.inboundcall_gid + "'," +
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
                        msSQL = "select inboundcall2taggedmember_gid from ocs_trn_tinboundcall2taggedmember where taggedmember_gid='" + existingtagemployee_list[i].employee_gid + "' and inboundcall_gid = '" + values.inboundcall_gid + "'";
                        string lsinboundcall2taggedmember_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "delete from ocs_trn_tinboundcall2taggedmember where inboundcall2taggedmember_gid='" + lsinboundcall2taggedmember_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }


                //UpdateLog
                msGetGid = objcmnfunctions.GetMasterGID("ICUL");
                msSQL = " insert into ocs_trn_tinboundcallupdatelog(" +
                   " inboundcallupdatelog_gid," +
                   " inboundcall_gid," +
                   " ticket_refid," +
                   " entity_gid," +
                   " entity_name," +
                   " sourceofcontact_gid," +
                   " sourceofcontact_name," +
                   " callreceivednumber_gid," +
                   " callreceivednumber_name," +
                   " customer_type," +
                   " callreceived_date," +
                   " caller_name," +
                   " internalreference_gid," +
                   " internalreference_name," +
                   " callerassociate_company," +
                   " office_landlineno," +
                   " calltype_gid," +
                   " calltype_name," +
                   " function_gid," +
                   " function_name," +
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
                   " inboundcall_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.inboundcall_gid + "'," +
                   "'" + lsticket_refid + "'," +
                   "'" + lsentity_gid + "'," +
                   "'" + lsentity_name + "'," +
                   "'" + lssourceofcontact_gid + "'," +
                   "'" + lssourceofcontact_name + "'," +
                   "'" + lscallreceivednumber_gid + "'," +
                   "'" + lscallreceivednumber_name + "'," +
                   "'" + lscustomer_type + "'," +
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
                   "'" + lsinboundcall_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates
                msSQL = "update ocs_trn_tinboundcall2mobileno set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2email set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2followup set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2address set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Inbound Call Details Submitted Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaIBCallEditUpdate(MdlIBCall values, string employee_gid)
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            DateTime lsmailalert = datevalue.AddHours(Convert.ToDouble(values.tat_hours)); 

            msSQL = "select inboundcall_gid " + " from ocs_trn_tinboundcall2mobileno where inboundcall_gid ='" + employee_gid + "' or inboundcall_gid='" + values.inboundcall_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return;
            }
            msSQL = "select inboundcall_gid " + " from ocs_trn_tinboundcall2email where inboundcall_gid ='" + employee_gid + "' or inboundcall_gid='" + values.inboundcall_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email ID";
                return;
            }
            msSQL = "select inboundcall_gid " + " from ocs_trn_tinboundcall2address where inboundcall_gid ='" + employee_gid + "' or inboundcall_gid='" + values.inboundcall_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Address Details";
                return;
            }


            if (values.callclosure_status == "Follow Up")
            {
                msSQL = "select inboundcall2followup_gid from ocs_trn_tinboundcall2followup where inboundcall_gid='" + employee_gid + "' or inboundcall_gid='" + values.inboundcall_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Enter Follow Up Details";
                    return;
                }
            }

            msSQL = " select ticket_refid,entity_gid,entity_name,sourceofcontact_gid,sourceofcontact_name,callreceivednumber_gid,callreceivednumber_name," +
                  " customer_type,callreceived_date,caller_name,internalreference_gid,internalreference_name," +
                  " callerassociate_company,office_landlineno,calltype_gid,calltype_name,function_gid,function_name,function_remarks,tat_hours,tat_date,tat_days" +
                  " requirement,enquiry_description,callclosure_status,assignemployee_gid,assignemployee_name," +
                  " assign_by,assign_date,transfer_by,transfer_date,completed_by,completed_date," +
                  " assignclosure_remarks,inboundcall_status" +
                  " from ocs_trn_tinboundcall where inboundcall_gid='" + values.inboundcall_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                lsentity_gid = objODBCDatareader["entity_gid"].ToString();
                lsentity_name = objODBCDatareader["entity_name"].ToString();
                lssourceofcontact_gid = objODBCDatareader["sourceofcontact_gid"].ToString();
                lssourceofcontact_name = objODBCDatareader["sourceofcontact_name"].ToString();
                lscallreceivednumber_gid = objODBCDatareader["callreceivednumber_gid"].ToString();
                lscallreceivednumber_name = objODBCDatareader["callreceivednumber_name"].ToString();
                lscustomer_type = objODBCDatareader["customer_type"].ToString();
                lscallreceived_date = objODBCDatareader["callreceived_date"].ToString();
                lscaller_name = objODBCDatareader["caller_name"].ToString();
                lsinternalreference_gid = objODBCDatareader["internalreference_gid"].ToString();
                lsinternalreference_name = objODBCDatareader["internalreference_name"].ToString();
                lscallerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                lsoffice_landlineno = objODBCDatareader["office_landlineno"].ToString();
                lscalltype_gid = objODBCDatareader["calltype_gid"].ToString();
                lscalltype_name = objODBCDatareader["calltype_name"].ToString();
                lsfunction_gid = objODBCDatareader["function_gid"].ToString();
                lsfunction_name = objODBCDatareader["function_name"].ToString();
                lsfunction_remarks = objODBCDatareader["function_remarks"].ToString();
                lsrequirement = objODBCDatareader["requirement"].ToString();
                lsenquiry_description = objODBCDatareader["enquiry_description"].ToString();
                lscallclosure_status = objODBCDatareader["callclosure_status"].ToString();
                lsassignemployee_gid = objODBCDatareader["assignemployee_gid"].ToString();
                lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                lstat_hours = objODBCDatareader["tat_hours"].ToString();               
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
                lsinboundcall_status = objODBCDatareader["inboundcall_status"].ToString();

            }
            objODBCDatareader.Close();


            msSQL = " update ocs_trn_tinboundcall set " +
                      " entity_gid='" + values.entity_gid + "'," +
                      " entity_name='" + values.entity_name + "'," +
                      " sourceofcontact_gid='" + values.sourceofcontact_gid + "'," +
                      " sourceofcontact_name='" + values.sourceofcontact_name + "'," +
                      " callreceivednumber_gid='" + values.callreceivednumber_gid + "'," +
                      " callreceivednumber_name='" + values.callreceivednumber_name + "'," +
                      " customer_type='" + values.customer_type + "'," +
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
          /*  if (values.tat_hours == null || values.tat_hours == "")
            {
                msSQL += " mail_alert=null,";
            }
            else
            {
                msSQL += " mail_alert='" + Convert.ToDateTime(values.tat_hours).ToString("yyyy-MM-dd HH:mm") + "',";
            }*/
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
                      " calltype_gid='" + values.calltype_gid + "'," +
                      " calltype_name='" + values.calltype_name + "'," +
                      " function_gid='" + values.function_gid + "'," +
                      " function_name='" + values.function_name + "'," +
                       " function_remarks='" + values.function_remarks + "'," +
                      " requirement='" + values.requirement.Replace("'", "") + "'," +
                      " enquiry_description='" + values.enquiry_description.Replace("'", "") + "'," +
                      " callclosure_status='" + values.callclosure_status + "'," +
                      " assignemployee_gid='" + values.assignemployee_gid + "'," +
                      " assignemployee_name='" + values.assignemployee_name + "'," +
                       " tat_hours='" + values.tat_hours + "'," +
                       " tat_date='" + values.tat_date + "'," +
                        " tat_days='" + values.tat_days + "'," +
                      " assign_by='" + employee_gid + "'," +
                      " assign_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " assignclosure_remarks='" + values.assignclosure_remarks.Replace("'", "") + "'," +
                      " inboundcall_status='" + "Completed" + "'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where inboundcall_gid='" + values.inboundcall_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                if (values.callclosure_status == "Follow Up")
                {
                    msSQL = " update ocs_trn_tinboundcall set followup_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " followup_by = '" + employee_gid + "'" +
                            " where inboundcall_gid='" + values.inboundcall_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.callclosure_status == "Closed")
                {
                    msSQL = " update ocs_trn_tinboundcall set closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " closed_flag = 'Y'," +
                            " closed_by = '" + employee_gid + "'" +
                            " where inboundcall_gid='" + values.inboundcall_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "select taggedmember_gid,taggedmember_name from ocs_trn_tinboundcall2taggedmember where inboundcall_gid='" + values.inboundcall_gid + "'";
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
                        msGetGid1 = objcmnfunctions.GetMasterGID("ICTM");
                        msSQL = " insert into ocs_trn_tinboundcall2taggedmember(" +
                                " inboundcall2taggedmember_gid," +
                                " inboundcall_gid," +
                                " taggedmember_gid," +
                                " taggedmember_name," +
                                " tagged_by," +
                                " tagged_date," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + values.inboundcall_gid + "'," +
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
                        msSQL = "select inboundcall2taggedmember_gid from ocs_trn_tinboundcall2taggedmember where taggedmember_gid='" + existingtagemployee_list[i].employee_gid + "' and inboundcall_gid = '" + values.inboundcall_gid + "'";
                        string lsinboundcall2taggedmember_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "delete from ocs_trn_tinboundcall2taggedmember where inboundcall2taggedmember_gid='" + lsinboundcall2taggedmember_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                //UpdateLog
                msGetGid = objcmnfunctions.GetMasterGID("ICUL");
                msSQL = " insert into ocs_trn_tinboundcallupdatelog(" +
                   " inboundcallupdatelog_gid," +
                   " inboundcall_gid," +
                   " ticket_refid," +
                   " entity_gid," +
                   " entity_name," +
                   " sourceofcontact_gid," +
                   " sourceofcontact_name," +
                   " callreceivednumber_gid," +
                   " callreceivednumber_name," +
                   " customer_type," +
                   " callreceived_date," +
                   " caller_name," +
                   " internalreference_gid," +
                   " internalreference_name," +
                   " callerassociate_company," +
                   " office_landlineno," +
                   " calltype_gid," +
                   " calltype_name," +
                   " function_gid," +
                   " function_name," +
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
                   " inboundcall_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.inboundcall_gid + "'," +
                   "'" + lsticket_refid + "'," +
                   "'" + lsentity_gid + "'," +
                   "'" + lsentity_name + "'," +
                   "'" + lssourceofcontact_gid + "'," +
                   "'" + lssourceofcontact_name + "'," +
                   "'" + lscallreceivednumber_gid + "'," +
                   "'" + lscallreceivednumber_name + "'," +
                   "'" + lscustomer_type + "'," +
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
                   "'" + lsinboundcall_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates
                msSQL = "update ocs_trn_tinboundcall2mobileno set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2email set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2followup set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tinboundcall2address set inboundcall_gid ='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Inbound Call Details Updated Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaIBCallTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_trn_tinboundcall2mobileno where inboundcall_gid='" + employee_gid + "' or inboundcall_gid=''";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_trn_tinboundcall2email where inboundcall_gid='" + employee_gid + "' or inboundcall_gid=''";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_trn_tinboundcall2followup where inboundcall_gid='" + employee_gid + "' or inboundcall_gid=''";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_trn_tinboundcall2address where inboundcall_gid='" + employee_gid + "' or inboundcall_gid=''";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetCompletedIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, a.callclosure_status" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Completed' and a.created_by = '" + employee_gid + "'" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            //created_by = (dr_datarow["created_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                            completed_by = (dr_datarow["completed_by"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetRejectedIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.callclosure_status" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status='Rejected' and a.created_by = '" + employee_gid + "'" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetFollowUpIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.followup_date,'%d-%m-%Y %h:%i %p') as followup_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                        " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as followup_by " +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.followup_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " where (a.callclosure_status = 'Follow Up' or a.callclosure_status = 'Extend Follow Up') and (a.created_by = '" + employee_gid + "')" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            followup_date = (dr_datarow["followup_date"].ToString()),
                            followup_by = (dr_datarow["followup_by"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetClosedIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " date_format(a.closed_date,'%d-%m-%Y %h:%i %p') as closed_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                        " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as closed_by " +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.closed_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " where a.callclosure_status = 'Closed' and a.created_by = '" + employee_gid + "'" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            closed_date = (dr_datarow["closed_date"].ToString()),
                            closed_by = (dr_datarow["closed_by"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetIBCallAssignedView(string inboundcall_gid, MdlIBCallView values)
        {
            try
            {
                msSQL = " select ticket_refid,a.entity_name,sourceofcontact_name,callreceivednumber_name, customer_type,callreceived_date,caller_name,tat_days,tat_hours, internalreference_name, " +
                        " callerassociate_company,office_landlineno,calltype_name,function_name, requirement,enquiry_description, callclosure_status,assignemployee_name," +
                        " tagemployee_name,assignclosure_remarks,inboundcall_status, date_format(assign_date,'%d-%m-%Y %h:%i %p') as assign_date,completed_remarks,closed_remarks," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as closed_by," +
                        " date_format(completed_date,'%d-%m-%Y %h:%i %p') as completed_date, date_format(closed_date,'%d-%m-%Y %h:%i %p') as closed_date, followup_remarks,function_remarks,date_format(tat_date,'%d-%m-%Y') as tat_date," +
                        " date_format(acknowledge_date,'%d-%m-%Y %h:%i %p') as acknowledge_date, date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%h:%i %p') as followup_time," +
                        " concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as followup_by,assigningclosure_remarks, rejected_remarks, " +
                        " concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as rejected_by, date_format(rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date " +
                        " from ocs_trn_tinboundcall a " +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.closed_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " left join hrm_mst_temployee f on a.followup_by = f.employee_gid" +
                        " left join adm_mst_tuser g on g.user_gid = f.user_gid" +
                        " left join hrm_mst_temployee h on a.rejected_by = h.employee_gid" +
                        " left join adm_mst_tuser i on i.user_gid = h.user_gid" +
                        " where inboundcall_gid='" + inboundcall_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    values.entity_name = objODBCDatareader["entity_name"].ToString();
                    values.sourceofcontact_name = objODBCDatareader["sourceofcontact_name"].ToString();
                    values.callreceivednumber_name = objODBCDatareader["callreceivednumber_name"].ToString();
                    values.customer_type = objODBCDatareader["customer_type"].ToString();
                    values.callreceived_date = objODBCDatareader["callreceived_date"].ToString();
                    values.caller_name = objODBCDatareader["caller_name"].ToString();
                    values.internalreference_name = objODBCDatareader["internalreference_name"].ToString();
                    values.callerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                    values.office_landlineno = objODBCDatareader["office_landlineno"].ToString();
                    values.calltype_name = objODBCDatareader["calltype_name"].ToString();
                    values.function_name = objODBCDatareader["function_name"].ToString();
                    values.requirement = objODBCDatareader["requirement"].ToString();
                    values.enquiry_description = objODBCDatareader["enquiry_description"].ToString();
                    values.callclosure_status = objODBCDatareader["callclosure_status"].ToString();
                    values.assignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                    values.tagemployee_name = objODBCDatareader["tagemployee_name"].ToString();
                    values.assignclosure_remarks = objODBCDatareader["assignclosure_remarks"].ToString();
                    values.assign_date = objODBCDatareader["assign_date"].ToString();
                    values.completed_by = objODBCDatareader["completed_by"].ToString();
                    values.closed_by = objODBCDatareader["closed_by"].ToString();
                    values.assigningclosure_remarks = objODBCDatareader["assigningclosure_remarks"].ToString();
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
                    values.tat_hours = objODBCDatareader["tat_hours"].ToString();
                    values.tat_date = objODBCDatareader["tat_date"].ToString();
                    values.tat_days = objODBCDatareader["tat_days"].ToString();
                    values.function_remarks = objODBCDatareader["function_remarks"].ToString();

                }

                objODBCDatareader.Close();

                msSQL = "select mobile_no from ocs_trn_tinboundcall2mobileno where primary_status='Yes' and inboundcall_gid='" + inboundcall_gid + "'";
                values.primary_mobileno = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select mobile_no,whatsapp_status,sms_to from ocs_trn_tinboundcall2mobileno where inboundcall_gid='" + inboundcall_gid + "' and primary_status='No'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcallmobileno_list = new List<ibcallmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcallmobileno_list.Add(new ibcallmobileno_list
                        {
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_status = (dr_datarow["whatsapp_status"].ToString()),
                            sms_to = (dr_datarow["sms_to"].ToString()),
                        });
                    }
                    values.ibcallmobileno_list = getibcallmobileno_list;
                }
                dt_datatable.Dispose();

                msSQL = "select email_address from ocs_trn_tinboundcall2email where primary_status='Yes' and inboundcall_gid='" + inboundcall_gid + "'";
                values.primary_email = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select email_address from ocs_trn_tinboundcall2email where inboundcall_gid='" + inboundcall_gid + "' and primary_status='No'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcallemail_list = new List<ibcallemail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcallemail_list.Add(new ibcallemail_list
                        {
                            email_address = (dr_datarow["email_address"].ToString()),
                        });
                    }
                    values.ibcallemail_list = getibcallemail_list;
                }
                dt_datatable.Dispose();

                msSQL = "  select addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code, city,latitude,longitude from ocs_trn_tinboundcall2address where inboundcall_gid='" + inboundcall_gid + "' and primary_status = 'Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcalladdress_list = new List<ibcalladdress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcalladdress_list.Add(new ibcalladdress_list
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
                    values.ibcalladdress_list = getibcalladdress_list;
                }
                dt_datatable.Dispose();

                msSQL = "select date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%H:%i') as followup_time" +
               " from ocs_trn_tinboundcall2followup where " +
               " inboundcall_gid = '" + inboundcall_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcallfollowup_list = new List<ibcallfollowup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcallfollowup_list.Add(new ibcallfollowup_list
                        {
                            followup_date = (dr_datarow["followup_date"].ToString()),
                            followup_time = (dr_datarow["followup_time"].ToString()),
                        });
                    }
                }
                values.ibcallfollowup_list = getibcallfollowup_list;
                dt_datatable.Dispose();

                msSQL = "select date_format(a.extendfollowup_date, '%d-%m-%Y') as followup_date,time_format(a.extendfollowup_time, '%H:%i') as followup_time ,a.extendfollowup_remarks," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as extendfollowup_by" +
                    " from ocs_trn_tinboundcall a " +
                " left join hrm_mst_temployee b on a.extendfollowup_by = b.employee_gid" +
                " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
               " where inboundcall_gid = '" + inboundcall_gid + "' and followup_date is not null";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinboundcallfollowup_list = new List<inboundcallfollowup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinboundcallfollowup_list.Add(new inboundcallfollowup_list
                        {
                            extendfollowup_date = (dr_datarow["followup_date"].ToString()),
                            extendfollowup_time = (dr_datarow["followup_time"].ToString()),
                            extendfollowup_remarks = (dr_datarow["extendfollowup_remarks"].ToString()),
                            extendfollowup_by = (dr_datarow["extendfollowup_by"].ToString()),
                        });
                    }
                }
                values.inboundcallfollowup_list = getinboundcallfollowup_list;
                dt_datatable.Dispose();
                msSQL = " select taggedmember_name," +
                 " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as tagged_by,date_format(a.tagged_date,'%d-%m-%Y %h:%i %p') as tagged_date" +
                 " from ocs_trn_tinboundcall2taggedmember a" +
                 " left join hrm_mst_temployee b on b.employee_gid=a.tagged_by " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                 " where inboundcall_gid = '" + inboundcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcalltaggedmember_list = new List<ibcalltaggedmember_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcalltaggedmember_list.Add(new ibcalltaggedmember_list
                        {
                            taggedmember_name = (dr_datarow["taggedmember_name"].ToString()),
                            tagged_by = (dr_datarow["tagged_by"].ToString()),
                            tagged_date = (dr_datarow["tagged_date"].ToString()),
                        });
                    }
                    values.ibcalltaggedmember_list = getibcalltaggedmember_list;
                }
                dt_datatable.Dispose();

                msSQL = " select transferfrom_name,transferto_name,a.transfer_remarks," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as transfer_by,date_format(a.transfer_date,'%d-%m-%Y %h:%i %p') as transfer_date" +
                " from ocs_trn_tinboundcalltransferlog a" +
                " left join hrm_mst_temployee b on b.employee_gid=a.transfer_by " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                " where inboundcall_gid = '" + inboundcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcalltransfer_list = new List<ibcalltransfer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcalltransfer_list.Add(new ibcalltransfer_list
                        {
                            transferfrom_name = (dr_datarow["transferfrom_name"].ToString()),
                            transferto_name = (dr_datarow["transferto_name"].ToString()),
                            transfer_by = (dr_datarow["transfer_by"].ToString()),
                            transfer_date = (dr_datarow["transfer_date"].ToString()),
                            transfer_remarks = (dr_datarow["transfer_remarks"].ToString())
                        });
                    }
                    values.ibcalltransfer_list = getibcalltransfer_list;
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

        public void DaIBCallDetailsForTransfer(string inboundcall_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " select inboundcall_gid,ticket_refid,assignemployee_gid,assignemployee_name from ocs_trn_tinboundcall where " +
                        " inboundcall_gid='" + inboundcall_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.inboundcall_gid = objODBCDatareader["inboundcall_gid"].ToString();
                    values.ticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    values.assignemployee_gid = objODBCDatareader["assignemployee_gid"].ToString();
                    values.assignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                }

                msSQL = " select transferfrom_name,transferto_name," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as transfer_by,date_format(a.transfer_date,'%d-%m-%Y %h:%i %p') as transfer_date" +
                " from ocs_trn_tinboundcalltransferlog a" +
                " left join hrm_mst_temployee b on b.employee_gid=a.transfer_by " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                " where inboundcall_gid = '" + inboundcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcalltransfer_list = new List<ibcalltransfer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcalltransfer_list.Add(new ibcalltransfer_list
                        {
                            transferfrom_name = (dr_datarow["transferfrom_name"].ToString()),
                            transferto_name = (dr_datarow["transferto_name"].ToString()),
                            transfer_by = (dr_datarow["transfer_by"].ToString()),
                            transfer_date = (dr_datarow["transfer_date"].ToString())
                        });
                    }
                    values.ibcalltransfer_list = getibcalltransfer_list;
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

        public void DaIBCallTransferEmployee(string employee_gid, MdlIBCallTransfer values)
        {
            try
            {
                msSQL = " update ocs_trn_tinboundcall set " +
                         " assignemployee_gid='" + values.transferto_gid + "'," +
                         " assignemployee_name='" + values.transferto_name + "'," +
                         " transfer_by='" + employee_gid + "'," +
                         " transfer_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where inboundcall_gid='" + values.inboundcall_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                employeename = objdbconn.GetExecuteScalar(msSQL);
                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ICTL");

                    msSQL = " insert into ocs_trn_tinboundcalltransferlog(" +
                  " inboundcalltransferlog_gid," +
                  " inboundcall_gid," +
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
                  "'" + values.inboundcall_gid + "'," +
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
                    msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
                    msSQL = "Insert into ocs_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " inboundcall_gid," +
                               " status," +
                                " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + values.inboundcall_gid + "'," +
                               "'Transferred'," +
                               "'" + values.transferto_gid + "'," +
                               "'" + values.transfer_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    values.status = true;
                    values.message = "Employee Transferred Successfully";

                    msSQL = " SELECT taggeduser_flag from ocs_trn_tinboundcall2taggedmember" +
                                        " where inboundcall_gid ='" + values.inboundcall_gid + "'";
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

                        msSQL = " select a.ticket_refid, a.caller_name, a.requirement,a.created_by as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from ocs_trn_tinboundcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequirement = objODBCDatareader["requirement"].ToString();
                            lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsfunction_name = objODBCDatareader["function_name"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();


                        msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from ocs_trn_tinboundcall2taggedmember" +
                                 " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                        ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Inbound Call Ticket Transferred - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                        body = "Hi " + HttpUtility.HtmlEncode(values.transferto_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Transferred to you.<br><br>";
                        body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Transfer By:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                        body = body + "Transfer time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Inbound - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                            msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                               " inboundcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.inboundcall_gid + "'," +
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

                            msSQL = " select a.ticket_refid, a.caller_name, a.requirement,a.assignemployee_gid as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                        " from ocs_trn_tinboundcall a " +
                                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                       " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsrequirement = objODBCDatareader["requirement"].ToString();
                                lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                                lscaller_name = objODBCDatareader["caller_name"].ToString();
                                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                                lsfunction_name = objODBCDatareader["function_name"].ToString();
                                lsto2members = objODBCDatareader["to2members"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.transferto_gid + "'";
                            tomail_id = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                           "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                           "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);

                            sub = " A Inbound Call Ticket Transferred - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                            body = "Hi " + HttpUtility.HtmlEncode(values.transferto_name) + ",<br><br>";
                            body = body + "Greetings! <br><br>";
                            body = body + "A ticket has been Transferred to you.<br><br>";
                            body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                            body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                            body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                            body = body + "Transfer By:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                            body = body + "Transfer time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                            body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                            body = body + "Regards<br><br>";
                            body = body + "Inbound - Customer Service Helpline<br><br>";
                            body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                                msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                                   " inboundcall_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.inboundcall_gid + "'," +
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

        public void DaGetEmpAssignedIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.assign_date,'%d-%m-%Y %h:%i %p') as assign_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as assign_by" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Assign' and a.assignemployee_gid = '" + employee_gid + "'" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            assign_date = (dr_datarow["assign_date"].ToString()),
                            assign_by = (dr_datarow["assign_by"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEmpInProgressIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.assign_date,'%d-%m-%Y %h:%i %p') as assign_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as assign_by" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Work-In-Progress' and a.assignemployee_gid = '" + employee_gid + "'" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            assign_date = (dr_datarow["assign_date"].ToString()),
                            assign_by = (dr_datarow["assign_by"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEmpTaggedSummary(MdlIBCall values, string employee_gid)
        {
            try
            {
                msSQL = " SELECT a.inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as assign_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as assign_by" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join ocs_trn_tinboundcall2taggedmember e on e.inboundcall_gid = a.inboundcall_gid " +
                        " where e.taggedmember_gid = '" + employee_gid + "' order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            assign_date = (dr_datarow["assign_date"].ToString()),
                            assign_by = (dr_datarow["assign_by"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEmpTransferredIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.transfer_date,'%d-%m-%Y %h:%i %p') as transfer_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as transfer_by" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where (a.callclosure_status = 'Assign' or a.callclosure_status = 'Work-In-Progress') and a.transfer_by = '" + employee_gid + "'" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            transfer_date = (dr_datarow["transfer_date"].ToString()),
                            transfer_by = (dr_datarow["transfer_by"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DGetEmpFollowUpIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.followup_date,'%d-%m-%Y') as followup_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as followup_by" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.followup_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where (a.callclosure_status = 'Follow Up' or a.callclosure_status = 'Extend Follow Up') and (a.assignemployee_gid = '" + employee_gid + "')" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            followup_date = (dr_datarow["followup_date"].ToString()),
                            followup_by = (dr_datarow["followup_by"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEmpCompletedIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date, date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, a.callclosure_status," +
                        " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as rejected_by" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.rejected_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " where (a.callclosure_status = 'Completed') and " +
                        " (a.completed_by = '" + employee_gid + "')" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                            completed_by = (dr_datarow["completed_by"].ToString()),
                            rejected_date = (dr_datarow["rejected_date"].ToString()),
                            rejected_by = (dr_datarow["rejected_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaGetEmpRejectedIBCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date, date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, a.callclosure_status," +
                        " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as rejected_by" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join hrm_mst_temployee d on a.rejected_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                        " where (a.callclosure_status = 'Rejected') and " +
                        " ( a.rejected_by='" + employee_gid + "')" +
                        " order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                            completed_by = (dr_datarow["completed_by"].ToString()),
                            rejected_date = (dr_datarow["rejected_date"].ToString()),
                            rejected_by = (dr_datarow["rejected_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaIBCallCount(string employee_gid, IBCallCount values)
        {
            msSQL = "select count(inboundcall_gid) as assignedcall_count from ocs_trn_tinboundcall a where a.created_by='" + employee_gid + "' and (a.callclosure_status = 'Assign' or a.callclosure_status = '' or a.callclosure_status = 'Work-In-Progress')";
            values.assignedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(inboundcall_gid) as completedcall_count from ocs_trn_tinboundcall a where a.created_by='" + employee_gid + "' and a.callclosure_status = 'Completed'";
            values.completedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(inboundcall_gid) as completedcall_count from ocs_trn_tinboundcall a where a.created_by='" + employee_gid + "' and a.callclosure_status='Rejected'";
            values.rejectedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(inboundcall_gid) as closedcall_count from ocs_trn_tinboundcall a where a.created_by='" + employee_gid + "' and a.callclosure_status = 'Closed'";
            values.closedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(inboundcall_gid) as followupcall_count from ocs_trn_tinboundcall a where a.created_by='" + employee_gid + "' and (a.callclosure_status = 'Follow Up' or a.callclosure_status = 'Extend Follow Up')";
            values.followupcall_count = objdbconn.GetExecuteScalar(msSQL);

        }

        public void DaEmployeeIBCallCount(string employee_gid, IBCallCount values)
        {
            msSQL = "select count(inboundcall_gid) as assignedcall_count from ocs_trn_tinboundcall a where a.assignemployee_gid='" + employee_gid + "' and a.callclosure_status = 'Assign'";
            values.assignedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(inboundcall_gid) as assignedcall_count from ocs_trn_tinboundcall a where a.assignemployee_gid='" + employee_gid + "' and a.callclosure_status = 'Work-In-Progress'";
            values.inprogresscall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(a.inboundcall_gid) as tagged_count from ocs_trn_tinboundcall2taggedmember a " +
                    " left join ocs_trn_tinboundcall b on b.inboundcall_gid = a.inboundcall_gid where a.taggedmember_gid = '" + employee_gid + "'";
            values.taggedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(inboundcall_gid) as transfercall_count from ocs_trn_tinboundcall a where a.transfer_by='" + employee_gid + "' and " +
                    " (a.callclosure_status = 'Assign' or a.callclosure_status = 'Work-In-Progress')";
            values.transfercall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(inboundcall_gid) as completedcall_count from ocs_trn_tinboundcall a where a.completed_by='" + employee_gid + "'   and " + 
                "(a.callclosure_status = 'Completed'  )";
            values.completedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(inboundcall_gid) as rejectedcall_count from ocs_trn_tinboundcall a where a.rejected_by='" + employee_gid + "' and "+
                "(a.callclosure_status = 'Rejected' )";
            values.rejectedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(inboundcall_gid) as followupcall_count from ocs_trn_tinboundcall a where a.assignemployee_gid='" + employee_gid + "' and (a.callclosure_status = 'Follow Up' or a.callclosure_status = 'Extend Follow Up')";
            values.followupcall_count = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaGetIBCallCompletedView(string inboundcall_gid, MdlIBCallcompleteView values)
        {
            try
            {
                msSQL = " select date_format(completed_date, '%d-%m-%Y') as completed_date, " +
                       " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as completed_by,completed_remarks  from ocs_trn_tinboundcall a" +
                       " left join hrm_mst_temployee b on a.completed_by = b.employee_gid " +
                       " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                       " where inboundcall_gid='" + inboundcall_gid + "'";
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

        public void DaPostUpdateAck(string employee_gid, MdlIBCall values)
        {
            msSQL = " update ocs_trn_tinboundcall set callclosure_status='Work-In-Progress'," +
                    " acknowledge_by = '" + employee_gid + "'," +
                    " acknowledge_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where inboundcall_gid='" + values.inboundcall_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
            msSQL = "Insert into ocs_trn_tstatuslog( " +
                       " statuslog_gid," +
                       " inboundcall_gid," +
                       " status," +
                        "overall_detail," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid2 + "'," +
                       "'" + values.inboundcall_gid + "'," +
                       "'Work-In-Progress'," +
                       "'" + employee_gid + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " SELECT taggeduser_flag from ocs_trn_tinboundcall2taggedmember" +
                                       " where inboundcall_gid ='" + values.inboundcall_gid + "'";
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

                msSQL = " select a.ticket_refid, a.caller_name, a.requirement,a.created_by as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                            " from ocs_trn_tinboundcall a " +
                             " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                           " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrequirement = objODBCDatareader["requirement"].ToString();
                    lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                    lscaller_name = objODBCDatareader["caller_name"].ToString();
                    lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    lsfunction_name = objODBCDatareader["function_name"].ToString();
                    lsto2members = objODBCDatareader["to2members"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from ocs_trn_tinboundcall2taggedmember" +
                         " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);

                sub = " A Inbound Call Ticket Acknowledged - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                body = body + "Greetings! <br><br>";
                body = body + "A ticket has been Acknowledged.<br><br>";
                body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                body = body + "Regards<br><br>";
                body = body + "Inbound - Customer Service Helpline<br><br>";
                body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(tomail_id));
                lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                    msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                       " inboundcall_gid," +
                       " from_mail," +
                       " to_mail," +
                       " cc_mail," +
                       " mail_status," +
                       " mail_senddate, " +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + values.inboundcall_gid + "'," +
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

                    msSQL = " select a.ticket_refid, a.caller_name, a.requirement,(a.created_by) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " from ocs_trn_tinboundcall a " +
                                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsrequirement = objODBCDatareader["requirement"].ToString();
                        lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                        lscaller_name = objODBCDatareader["caller_name"].ToString();
                        lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                        lsfunction_name = objODBCDatareader["function_name"].ToString();
                        lsto2members = objODBCDatareader["to2members"].ToString();
                    }
                    objODBCDatareader.Close();



                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    sub = " A Inbound Call Ticket Acknowledged - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                    body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been Acknowledged.<br><br>";
                    body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                    body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Inbound - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                        msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                           " inboundcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.inboundcall_gid + "'," +
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
        public void DaRejectIBCall(string employee_gid, MdlIBCall values)
        {
            msSQL = " update ocs_trn_tinboundcall set callclosure_status='Rejected', " +
                   " rejected_remarks='" + values.reject_remarks.Replace("'", "") + "'," +
                   " rejected_flag='Y'," +
                   " rejected_by='" + employee_gid + "'," +
                   " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where inboundcall_gid='" + values.inboundcall_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
            msSQL = "Insert into ocs_trn_tstatuslog( " +
                       " statuslog_gid," +
                       " inboundcall_gid," +
                       " status," +
                        " overall_detail," +
                       " remarks," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid2 + "'," +
                       "'" + values.inboundcall_gid + "'," +
                       "'Rejected'," +
                        "'" + employee_gid + "'," +
                       "'" + values.reject_remarks.Replace("'", "") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " SELECT taggeduser_flag from ocs_trn_tinboundcall2taggedmember" +
                                       " where inboundcall_gid ='" + values.inboundcall_gid + "'";
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

                msSQL = " select a.ticket_refid, a.caller_name, a.requirement,(a.created_by) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                            " from ocs_trn_tinboundcall a " +
                             " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                           " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrequirement = objODBCDatareader["requirement"].ToString();
                    lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                    lscaller_name = objODBCDatareader["caller_name"].ToString();
                    lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    lsfunction_name = objODBCDatareader["function_name"].ToString();
                    lsto2members = objODBCDatareader["to2members"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from ocs_trn_tinboundcall2taggedmember" +
                         " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);

                sub = " A Inbound Call Ticket Rejected - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                body = body + "Greetings! <br><br>";
                body = body + "A ticket has been Rejected.<br><br>";
                body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                body = body + "Regards<br><br>";
                body = body + "Inbound - Customer Service Helpline<br><br>";
                body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(tomail_id));
                lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                    msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                       " inboundcall_gid," +
                       " from_mail," +
                       " to_mail," +
                       " cc_mail," +
                       " mail_status," +
                       " mail_senddate, " +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + values.inboundcall_gid + "'," +
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
                    values.message = "Call Rejected Successfully..!";

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

                    msSQL = " select a.ticket_refid, a.caller_name, a.requirement,(a.created_by) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " from ocs_trn_tinboundcall a " +
                                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsrequirement = objODBCDatareader["requirement"].ToString();
                        lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                        lscaller_name = objODBCDatareader["caller_name"].ToString();
                        lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                        lsfunction_name = objODBCDatareader["function_name"].ToString();
                        lsto2members = objODBCDatareader["to2members"].ToString();
                    }
                    objODBCDatareader.Close();



                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    sub = " A Inbound Call Ticket Rejected - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                    body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been Rejected.<br><br>";
                    body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                    body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Inbound - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                        msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                           " inboundcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.inboundcall_gid + "'," +
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
        public void DaPostCompletedCall(string employee_gid, MdlIBCall values)
        {
            if (values.closure_status == "Complete")
            {
                msSQL = " update ocs_trn_tinboundcall set callclosure_status='Completed', " +
                    " completed_flag='Y'," +
                    " completed_remarks='" + values.completed_remarks.Replace("'", "") + "'," +
                    " assignclosure_status='" + values.closure_status + "'," +
                    " completed_by='" + employee_gid + "'," +
                    " completed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where inboundcall_gid='" + values.inboundcall_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " update ocs_mst_tibcallproofdocupload set inboundcall_gid='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_mst_tibcallrecordingdocupload set inboundcall_gid='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Call Completed Successfully..!";

                    msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
                    msSQL = "Insert into ocs_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " inboundcall_gid," +
                               " status," +
                               " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + values.inboundcall_gid + "'," +
                               "' Completed'," +
                                "'" + employee_gid + "'," +
                               "'" + values.completed_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " SELECT taggeduser_flag from ocs_trn_tinboundcall2taggedmember" +
                                      " where inboundcall_gid ='" + values.inboundcall_gid + "'";
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

                        msSQL = " select a.ticket_refid, a.caller_name, a.requirement,a.created_by as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from ocs_trn_tinboundcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequirement = objODBCDatareader["requirement"].ToString();
                            lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsfunction_name = objODBCDatareader["function_name"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from ocs_trn_tinboundcall2taggedmember" +
                                 " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                        ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                                "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                                  "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Inbound Call Ticket Completed - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                        body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Completed.<br><br>";
                        body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Call Completed by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                        body = body + "Call Completed time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Inbound - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                            msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                               " inboundcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.inboundcall_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + tomail_id + "'," +
                               "'" + cc_mailid + "'," +
                               "'Completed'," +
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

                            msSQL = " select a.ticket_refid, a.caller_name, a.requirement,(a.created_by) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                        " from ocs_trn_tinboundcall a " +
                                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                       " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsrequirement = objODBCDatareader["requirement"].ToString();
                                lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                                lscaller_name = objODBCDatareader["caller_name"].ToString();
                                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                                lsfunction_name = objODBCDatareader["function_name"].ToString();
                                lsto2members = objODBCDatareader["to2members"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                            tomail_id = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                                    "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                                    "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);


                            sub = " A Inbound Call Ticket Completed - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                            body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                            body = body + "Greetings! <br><br>";
                            body = body + "A ticket has been Completed.<br><br>";
                            body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                            body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                            body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                            body = body + "Call Completed by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                            body = body + "Call Completed time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                            body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                            body = body + "Regards<br><br>";
                            body = body + "Inbound - Customer Service Helpline<br><br>";
                            body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                                msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                                   " inboundcall_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.inboundcall_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + tomail_id + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Completed'," +
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
                msSQL = " update ocs_trn_tinboundcall set callclosure_status='Extend Follow Up', ";
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
                msSQL += " followup_by='" + employee_gid + "'," +
                         " extendfollowup_by='" + employee_gid + "'," +
                         " followup_remarks='" + values.followup_remarks.Replace("'", "") + "'," +
                         " extendfollowup_remarks='" + values.followup_remarks.Replace("'", "") + "'" +
                         " where inboundcall_gid='" + values.inboundcall_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " update ocs_mst_tibcallproofdocupload set inboundcall_gid='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_mst_tibcallrecordingdocupload set inboundcall_gid='" + values.inboundcall_gid + "' where inboundcall_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Call Follow Up Status Successfully..!";

                    msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
                    msSQL = "Insert into ocs_trn_tstatuslog( " +
                               " statuslog_gid," +
                               " inboundcall_gid," +
                               " status," +
                               " overall_detail," +
                               " remarks," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid2 + "'," +
                               "'" + values.inboundcall_gid + "'," +
                               "' Extend Follow-Up'," +
                               "'" + employee_gid + "'," +
                               "'" + values.followup_remarks.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " SELECT taggeduser_flag from ocs_trn_tinboundcall2taggedmember" +
                                     " where inboundcall_gid ='" + values.inboundcall_gid + "'";
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

                        msSQL = " select a.ticket_refid, a.caller_name, a.requirement,(a.created_by) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from ocs_trn_tinboundcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequirement = objODBCDatareader["requirement"].ToString();
                            lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsfunction_name = objODBCDatareader["function_name"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from ocs_trn_tinboundcall2taggedmember" +
                                 " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                        ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Inbound Call Ticket FollowUp - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                        body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been FollowUp.<br><br>";
                        body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Inbound - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                            msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                               " inboundcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.inboundcall_gid + "'," +
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

                            msSQL = " select a.ticket_refid, a.caller_name, a.requirement,a.created_by as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                        " from ocs_trn_tinboundcall a " +
                                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                       " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsrequirement = objODBCDatareader["requirement"].ToString();
                                lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                                lscaller_name = objODBCDatareader["caller_name"].ToString();
                                lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                                lsfunction_name = objODBCDatareader["function_name"].ToString();
                                lsto2members = objODBCDatareader["to2members"].ToString();
                            }
                            objODBCDatareader.Close();



                            msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + HttpUtility.HtmlEncode(lsto2members) + "'";
                            tomail_id = objdbconn.GetExecuteScalar(msSQL);

                            sub = " A Inbound Call Ticket FollowUp - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                            body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                            body = body + "Greetings! <br><br>";
                            body = body + "A ticket has been FollowUP.<br><br>";
                            body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                            body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                            body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                            body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                            body = body + "Regards<br><br>";
                            body = body + "Inbound - Customer Service Helpline<br><br>";
                            body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                                msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                                   " inboundcall_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.inboundcall_gid + "'," +
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
        }
        public void DaPostCloseCall(string employee_gid, MdlIBCall values)
        {
            if (values.followup_date == "" || values.followup_date == null || values.followup_date == "undefined")
            {
                msSQL = " update ocs_trn_tinboundcall set callclosure_status='Closed'," +
                    " closed_flag = 'Y'," +
                    " closed_remarks='" + values.closed_remarks.Replace("'", "") + "'," +
                    " finalcallclosure_status='" + values.closure_status + "'," +
                    " closed_by = '" + employee_gid + "'," +
                    " closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where inboundcall_gid='" + values.inboundcall_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
                msSQL = "Insert into ocs_trn_tstatuslog( " +
                           " statuslog_gid," +
                           " inboundcall_gid," +
                           " status," +
                           " overall_detail," +
                           " remarks," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid2 + "'," +
                           "'" + values.inboundcall_gid + "'," +
                           "' Closed'," +
                           "'" + employee_gid + "'," +
                           "'" + values.closed_remarks.Replace("'", "") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " SELECT taggeduser_flag from ocs_trn_tinboundcall2taggedmember" +
                                     " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                if (ls_taguser == "Y")
                {
                    values.status = true;
                    values.message = "Call Closed Successfully";
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

                    msSQL = " select a.ticket_refid, a.caller_name, a.requirement,(a.created_by) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " from ocs_trn_tinboundcall a " +
                                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsrequirement = objODBCDatareader["requirement"].ToString();
                        lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                        lscaller_name = objODBCDatareader["caller_name"].ToString();
                        lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                        lsfunction_name = objODBCDatareader["function_name"].ToString();
                        lsto2members = objODBCDatareader["to2members"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from ocs_trn_tinboundcall2taggedmember" +
                             " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                    ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                   "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                   "where b.employee_gid ='" + employee_gid + "'";
                    employeename = objdbconn.GetExecuteScalar(msSQL);


                    sub = " A Inbound Call Ticket Closed - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                    body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been Closed.<br><br>";
                    body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                    body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Call Close by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                    body = body + "Call Close time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Inbound - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                        msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                           " inboundcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.inboundcall_gid + "'," +
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
                        values.message = "Call Closed Successfully";

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

                        msSQL = " select a.ticket_refid, a.caller_name,a.assignemployee_name, a.requirement,(a.assignemployee_gid) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from ocs_trn_tinboundcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequirement = objODBCDatareader["requirement"].ToString();
                            lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsfunction_name = objODBCDatareader["function_name"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Inbound Call Ticket Closed - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                        body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Closed.<br><br>";
                        body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Call Closed by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                        body = body + "Call Closed time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Inbound - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                            msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                               " inboundcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.inboundcall_gid + "'," +
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
                msSQL = " update ocs_trn_tinboundcall set callclosure_status='Extend Follow Up', ";
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
                        " where inboundcall_gid='" + values.inboundcall_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
                msSQL = "Insert into ocs_trn_tstatuslog( " +
                           " statuslog_gid," +
                           " inboundcall_gid," +
                           " status," +
                            " overall_detail," +
                           " remarks," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid2 + "'," +
                           "'" + values.inboundcall_gid + "'," +
                           "' Extend Follow Up'," +
                           "'" + employee_gid + "'," +
                           "'" + values.followup_remarks.Replace("'", "") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " SELECT taggeduser_flag from ocs_trn_tinboundcall2taggedmember" +
                                     " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                if (ls_taguser == "Y")
                {
                    values.status = true;
                    values.message = "Call Extend Follow Up Status Updated Successfully";

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

                    msSQL = " select a.ticket_refid, a.caller_name, a.requirement,(a.created_by) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " from ocs_trn_tinboundcall a " +
                                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsrequirement = objODBCDatareader["requirement"].ToString();
                        lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                        lscaller_name = objODBCDatareader["caller_name"].ToString();
                        lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                        lsfunction_name = objODBCDatareader["function_name"].ToString();
                        lsto2members = objODBCDatareader["to2members"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from ocs_trn_tinboundcall2taggedmember" +
                             " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                    ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                   "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                   "where b.employee_gid ='" + employee_gid + "'";
                    employeename = objdbconn.GetExecuteScalar(msSQL);


                    sub = " A Inbound Call Ticket Extend-FollowUP - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                    body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been Extend-FollowUp.<br><br>";
                    body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                    body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Call Extend-FollowUP by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                    body = body + "Call Extend-FollowUP time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Inbound - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                        msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                           " inboundcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.inboundcall_gid + "'," +
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
                        values.message = "Call Extend Follow Up Status Updated Successfully";

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

                        msSQL = " select a.ticket_refid, a.caller_name, a.requirement,a.assignemployee_name,(a.assignemployee_gid) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from ocs_trn_tinboundcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequirement = objODBCDatareader["requirement"].ToString();
                            lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsfunction_name = objODBCDatareader["function_name"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Inbound Call Ticket Extend-Follow UP - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                        body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Extend-Follow Up.<br><br>";
                        body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Call Extend-FollowUP by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                        body = body + "Call Extend-FollowUP time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Inbound - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                            msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                               " inboundcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.inboundcall_gid + "'," +
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

        public void DaGetAssignedCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where (a.callclosure_status = 'Assign' or a.callclosure_status = '') order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetCompletedCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, a.callclosure_status" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Completed' order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            completed_date = (dr_datarow["completed_date"].ToString()),
                            completed_by = (dr_datarow["completed_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetRejectedCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.callclosure_status" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.completed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status='Rejected' order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            callclosure_status = (dr_datarow["callclosure_status"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetFollowUpCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.followup_date,'%d-%m-%Y %h:%i %p') as followup_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as followup_by" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.followup_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where (a.callclosure_status = 'Follow Up' or  a.callclosure_status = 'Extend Follow Up') order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            followup_date = (dr_datarow["followup_date"].ToString()),
                            followup_by = (dr_datarow["followup_by"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetClosedCallSummary(string employee_gid, MdlIBCall values)
        {
            try
            {
                msSQL = " SELECT inboundcall_gid, ticket_refid,caller_name,customer_type, callreceived_date, assignemployee_name," +
                        " date_format(a.closed_date,'%d-%m-%Y %h:%i %p') as closed_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as closed_by" +
                        " FROM ocs_trn_tinboundcall a" +
                        " left join hrm_mst_temployee b on a.closed_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.callclosure_status = 'Closed' order by a.inboundcall_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcall_list = new List<ibcall_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcall_list.Add(new ibcall_list
                        {
                            inboundcall_gid = (dr_datarow["inboundcall_gid"].ToString()),
                            ticket_refid = (dr_datarow["ticket_refid"].ToString()),
                            caller_name = (dr_datarow["caller_name"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            callreceived_date = (dr_datarow["callreceived_date"].ToString()),
                            assignemployee_name = (dr_datarow["assignemployee_name"].ToString()),
                            closed_date = (dr_datarow["closed_date"].ToString()),
                            closed_by = (dr_datarow["closed_by"].ToString()),
                        });
                    }
                    values.ibcall_list = getibcall_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaIBAssignedCallCount(string employee_gid, IBCallCount values)
        {
            msSQL = "select count(inboundcall_gid) as assignedcall_count from ocs_trn_tinboundcall a where a.callclosure_status = 'Assign'";
            values.assignedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(inboundcall_gid) as completedcall_count from ocs_trn_tinboundcall a where a.callclosure_status = 'Completed'";
            values.completedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(inboundcall_gid) as completedcall_count from ocs_trn_tinboundcall a where a.callclosure_status='Rejected'";
            values.rejectedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(inboundcall_gid) as closedcall_count from ocs_trn_tinboundcall a where a.callclosure_status = 'Closed'";
            values.closedcall_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(inboundcall_gid) as followupcall_count from ocs_trn_tinboundcall a where a.callclosure_status in ('Follow Up','Extend Follow Up')";
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
            string inboundcall_gid = httpRequest.Form["inboundcall_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);

           
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
                        //bool validdocument = objcmnstorage.CheckIsValidfilename(FileExtension, project_flag);

                        //if (validdocument == false)
                        //{
                        //    objfilename.message = "file format not supported";
                        //    return false;
                        //}

                        //// Check Document content validation;

                        //bool validdoccontent = objcmnstorage.CheckIsExecutable(bytes);

                        //if (validdoccontent == true)
                        //{
                        //    objfilename.message = "file format not supported";
                        //    return false;
                        //}


                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CallProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                        {
                            if ((!System.IO.Directory.Exists(lspath)))
                                System.IO.Directory.CreateDirectory(lspath);
                        }


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CallProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CallProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        msGetGid = objcmnfunctions.GetMasterGID("IBCP");
                        msSQL = " insert into ocs_mst_tibcallproofdocupload( " +
                                    " ibcallproofdocupload_gid," +
                                    " inboundcall_gid," +
                                    " document_title," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + inboundcall_gid + "'," +
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

        public void DaIBCallProofDocumentTmpList(string inboundcall_gid, string employee_gid, callproofuploaddocument values)
        {
            msSQL = " select ibcallproofdocupload_gid,inboundcall_gid,document_name,document_path,document_title from ocs_mst_tibcallproofdocupload " +
                             " where inboundcall_gid='" + employee_gid + "' or inboundcall_gid='" + inboundcall_gid + "'";
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
                    //file_path = dt["document_path"].ToString();
                    file_path = objcmnstorage.EncryptData((dt["document_path"].ToString()));
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                //
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcallproofupload_list.Add(new callproofupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        //document_path = dt["document_path"].ToString(),
                       document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                    inboundcall_gid = dt["inboundcall_gid"].ToString(),
                        ibcallproofdocupload_gid = dt["ibcallproofdocupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.callproofupload_list = getcallproofupload_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaIBCallProofDocumentList(string inboundcall_gid, string employee_gid, callproofuploaddocument values)
        {
            msSQL = " select a.ibcallproofdocupload_gid,a.inboundcall_gid,a.document_name,a.document_path,a.document_title,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                " from ocs_mst_tibcallproofdocupload a " +
                 " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                   " where inboundcall_gid='" + inboundcall_gid + "'";
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
                    //file_path = dt["document_path"].ToString();
                    file_path = objcmnstorage.EncryptData((dt["document_path"].ToString()));
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcallproofupload_list.Add(new callproofupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        //document_path = dt["document_path"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        inboundcall_gid = dt["inboundcall_gid"].ToString(),
                        ibcallproofdocupload_gid = dt["ibcallproofdocupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.callproofupload_list = getcallproofupload_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaIBCallProofDocumentDelete(string ibcallproofdocupload_gid, callproofuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from ocs_mst_tibcallproofdocupload where ibcallproofdocupload_gid='" + ibcallproofdocupload_gid + "'";
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
            string inboundcall_gid = httpRequest.Form["inboundcall_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CallRecordingDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CallRecordingDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CallRecordingDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        // ---repeated code-----

                        msGetGid = objcmnfunctions.GetMasterGID("IBCR");
                        msSQL = " insert into ocs_mst_tibcallrecordingdocupload( " +
                                    " ibcallrecordingocupload_gid," +
                                    " inboundcall_gid," +
                                    " document_title," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + inboundcall_gid + "'," +
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

        public void DaIBCallRecordingDocumentTmpList(string inboundcall_gid, string employee_gid, callproofuploaddocument values)
        {
            msSQL = " select ibcallrecordingocupload_gid,inboundcall_gid,document_name,document_path,document_title from ocs_mst_tibcallrecordingdocupload " +
                             " where inboundcall_gid='" + employee_gid + "' or inboundcall_gid='" + inboundcall_gid + "'";
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
                    file_path = objcmnstorage.EncryptData((dt["document_path"].ToString()));
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcallproofupload_list.Add(new callproofupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        inboundcall_gid = dt["inboundcall_gid"].ToString(),
                        ibcallrecordingocupload_gid = dt["ibcallrecordingocupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.callproofupload_list = getcallproofupload_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaIBCallRecordingDocumentList(string inboundcall_gid, string employee_gid, callproofuploaddocument values)
        {
            msSQL = " select a.ibcallrecordingocupload_gid,a.inboundcall_gid,a.document_name,a.document_path,a.document_title , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                 " from ocs_mst_tibcallrecordingdocupload a " +
                 " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                " where inboundcall_gid='" + inboundcall_gid + "'";
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
                    file_path = objcmnstorage.EncryptData((dt["document_path"].ToString()));
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcallproofupload_list.Add(new callproofupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        //document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        inboundcall_gid = dt["inboundcall_gid"].ToString(),
                        ibcallrecordingocupload_gid = dt["ibcallrecordingocupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.callproofupload_list = getcallproofupload_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaIBCallRecordingDocumentDelete(string ibcallrecordingocupload_gid, callproofuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from ocs_mst_tibcallrecordingdocupload where ibcallrecordingocupload_gid='" + ibcallrecordingocupload_gid + "'";
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

        public void DaIBCallDocTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_mst_tibcallrecordingdocupload where inboundcall_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tibcallproofdocupload where inboundcall_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetTeleCallingReportSummary(TeleCallingReport objTeleCallingReport)
        {
            msSQL = " select a.inboundcall_gid,a.entity_name, ticket_refid, caller_name, customer_type, callclosure_status," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,  concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                    " from ocs_trn_tinboundcall a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid order by inboundcall_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objTeleCallingReportSummary = new List<TeleCallingReportList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objTeleCallingReportSummary.Add(new TeleCallingReportList
                    {
                        inboundcall_gid = dr_datarow["inboundcall_gid"].ToString(),
                        entity_name = dr_datarow["entity_name"].ToString(),
                        ticket_refid = dr_datarow["ticket_refid"].ToString(),
                        caller_name = dr_datarow["caller_name"].ToString(),
                        customer_type = dr_datarow["customer_type"].ToString(),
                        inboundcall_status = dr_datarow["callclosure_status"].ToString(),
                        created_date = dr_datarow["created_date"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                    });
                }
                objTeleCallingReport.TeleCallingReportList = objTeleCallingReportSummary;
            }
            dt_datatable.Dispose();
            objTeleCallingReport.status = true;
            objTeleCallingReport.message = "Success";
        }

        // export excel TeleCalling Report

        public void DaExportTelecallingReport(TeleCallingReport objTeleCallingReport)
        {

           

            msSQL = " select a.entity_name as `Entity`, ticket_refid as `Ticket Ref Number`, caller_name as `Enquiry Name`, customer_type as `Customer Type`, callreceived_date as `Call Received Date`,  " +
 " sourceofcontact_name as `Source of Contact`, callreceivednumber_name as `Call Received Number`, internalreference_name as `Internal Reference`, office_landlineno as `Office Landline Number`, " +
 " callerassociate_company as `Company`, function_name as `Function`, callassign_status as `Enquiry Assigning Status`, a.tat_hours as `TAT_Hrs`, date_format(a.tat_date,'%d-%m-%Y') as `Allocated TAT Date`,a.tat_days as `Allocated TAT days`  , assignclosure_remarks as `Remarks by Inbound Team`,  " +
 " (select GROUP_CONCAT(taggedmember_name) from ocs_trn_tinboundcall2taggedmember p where p.inboundcall_gid = a.inboundcall_gid) as `Tag Details`,  " +
" (select CONCAT(addressline1,', ',addressline2,', ',city,', ',taluka,', ',district,', ',state,' ',postal_code) from ocs_trn_tinboundcall2address q where primary_status = 'Yes' and q.inboundcall_gid = a.inboundcall_gid) as `Address Details`,  " +
 " (select (max(mobile_no)) from ocs_trn_tinboundcall2mobileno r where primary_status = 'Yes' and r.inboundcall_gid = a.inboundcall_gid) as `Mobile Number`,  " +
 " (select (max(email_address)) from ocs_trn_tinboundcall2email s where primary_status = 'Yes' and s.inboundcall_gid = a.inboundcall_gid) as `Email Address`,  " +
 " requirement as `Requirement/Enquiry Title`, enquiry_description as `Enquiry Detailed Remarks`,  " +
 " (select date_format(max(t.followup_date),'%d-%m-%Y') from ocs_trn_tinboundcall2followup t where t.inboundcall_gid = a.inboundcall_gid) as `Follow Up Date`, " +
 " (select time_format(max(u.followup_time), '%H:%i') from ocs_trn_tinboundcall2followup u where u.inboundcall_gid = a.inboundcall_gid) as `Follow Up Time`, followup_remarks as `followup_remarks`, " +
" assignemployee_name as `Assigned To`, date_format(assign_date,'%d-%m-%Y') as `Assigned Date`,assignclosure_status as `Assignee Status`, completed_remarks as `Assignee Remarks`,finalcallclosure_status as `Call Closure Status`, closed_remarks as `Call Closure Remarks`,date_format(a.updated_date,'%d-%m-%Y') as `Call Closure Updated Date`,  " +
 " (select (max(transferto_name)) from ocs_trn_tinboundcalltransferlog v where v.transferfrom_gid = a.assignemployee_gid and v.inboundcall_gid = a.inboundcall_gid) as `Transfer To`,  " +
" (select (max(transfer_remarks)) from ocs_trn_tinboundcalltransferlog v where v.transferfrom_gid = a.assignemployee_gid and v.inboundcall_gid = a.inboundcall_gid) as `Transfer Remarks`,  " +
 " date_format(a.created_date,'%d-%m-%Y') as `Created Date`, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as `Created By`  " +
" from ocs_trn_tinboundcall a  " +
" left join hrm_mst_temployee b on a.created_by = b.employee_gid  " +
" left join adm_mst_tuser c on c.user_gid = b.user_gid ";



            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            //ExcelPackage excel = new ExcelPackage();
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet = excel.Workbook.Worksheets.Add("Inbound report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objTeleCallingReport.lsname = "InboundReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/TeleCallingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objTeleCallingReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/TeleCallingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objTeleCallingReport.lsname;
                objTeleCallingReport.lscloudpath = lscompany_code + "/" + "Master/TeleCallingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objTeleCallingReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objTeleCallingReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 35])  //Address "A1:A29"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/TeleCallingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objTeleCallingReport.lsname, ms);
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
        public void DaGetEntity(MdlIBCall values)
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

        public void DaGetIBCallReportView(string inboundcall_gid, MdlIBCallView values)
        {
            try
            {
                msSQL = " select ticket_refid,a.entity_name,sourceofcontact_name,callreceivednumber_name, customer_type,callreceived_date,caller_name,tat_days,tat_hours,internalreference_name, " +
                        " callerassociate_company,office_landlineno,calltype_name,function_name, requirement,enquiry_description, callclosure_status,assignemployee_name," +
                        " tagemployee_name,assignclosure_remarks,inboundcall_status, date_format(assign_date,'%d-%m-%Y %h:%i %p') as assign_date,completed_remarks,closed_remarks," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as completed_by, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as closed_by," +
                        " date_format(completed_date,'%d-%m-%Y %h:%i %p') as completed_date, date_format(closed_date,'%d-%m-%Y %h:%i %p') as closed_date, followup_remarks,function_remarks,date_format(a.tat_date,'%d-%m-%Y') as tat_date," +
                        " date_format(acknowledge_date,'%d-%m-%Y %h:%i %p') as acknowledge_date, date_format(followup_date, '%d-%m-%Y') as followup_date,time_format(followup_time, '%h:%i %p') as followup_time," +
                        " concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as followup_by, rejected_remarks," +
                        " concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as rejected_by, date_format(rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date, " +
                       " concat(k.user_firstname,' ',k.user_lastname,' / ',k.user_code) as acknowledge_by" +
                        " from ocs_trn_tinboundcall a " +
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
                        " where inboundcall_gid='" + inboundcall_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ticket_refid = objODBCDatareader["ticket_refid"].ToString();
                    values.entity_name = objODBCDatareader["entity_name"].ToString();
                    values.sourceofcontact_name = objODBCDatareader["sourceofcontact_name"].ToString();
                    values.callreceivednumber_name = objODBCDatareader["callreceivednumber_name"].ToString();
                    values.customer_type = objODBCDatareader["customer_type"].ToString();
                    values.callreceived_date = objODBCDatareader["callreceived_date"].ToString();
                    values.caller_name = objODBCDatareader["caller_name"].ToString();
                    values.internalreference_name = objODBCDatareader["internalreference_name"].ToString();
                    values.callerassociate_company = objODBCDatareader["callerassociate_company"].ToString();
                    values.office_landlineno = objODBCDatareader["office_landlineno"].ToString();
                    values.calltype_name = objODBCDatareader["calltype_name"].ToString();
                    values.function_name = objODBCDatareader["function_name"].ToString();
                    values.requirement = objODBCDatareader["requirement"].ToString();
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
                    values.tat_days = objODBCDatareader["tat_days"].ToString();
                    values.tat_date = objODBCDatareader["tat_date"].ToString();
                    values.function_remarks = objODBCDatareader["function_remarks"].ToString();
                }

                objODBCDatareader.Close();

                msSQL = "select mobile_no from ocs_trn_tinboundcall2mobileno where primary_status='Yes' and inboundcall_gid='" + inboundcall_gid + "'";
                values.primary_mobileno = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select mobile_no,whatsapp_status,sms_to from ocs_trn_tinboundcall2mobileno where inboundcall_gid='" + inboundcall_gid + "' and primary_status='No'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcallmobileno_list = new List<ibcallmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcallmobileno_list.Add(new ibcallmobileno_list
                        {
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_status = (dr_datarow["whatsapp_status"].ToString()),
                            sms_to = (dr_datarow["sms_to"].ToString()),
                        });
                    }
                    values.ibcallmobileno_list = getibcallmobileno_list;
                }
                dt_datatable.Dispose();

                msSQL = "select email_address from ocs_trn_tinboundcall2email where primary_status='Yes' and inboundcall_gid='" + inboundcall_gid + "'";
                values.primary_email = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select email_address from ocs_trn_tinboundcall2email where inboundcall_gid='" + inboundcall_gid + "' and primary_status='No'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcallemail_list = new List<ibcallemail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcallemail_list.Add(new ibcallemail_list
                        {
                            email_address = (dr_datarow["email_address"].ToString()),
                        });
                    }
                    values.ibcallemail_list = getibcallemail_list;
                }
                dt_datatable.Dispose();

                msSQL = "  select addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code, city,latitude,longitude from ocs_trn_tinboundcall2address where inboundcall_gid='" + inboundcall_gid + "' and primary_status = 'Yes'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcalladdress_list = new List<ibcalladdress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcalladdress_list.Add(new ibcalladdress_list
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
                    values.ibcalladdress_list = getibcalladdress_list;
                }
                dt_datatable.Dispose();

                msSQL = "select date_format(a.followup_date, '%d-%m-%Y') as followup_date,date_format(a.created_date, '%d-%m-%Y') as created_date,time_format(a.followup_time, '%H:%i') as followup_time," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " from ocs_trn_tinboundcall2followup a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where inboundcall_gid = '" + inboundcall_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcallfollowup_list = new List<ibcallfollowup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcallfollowup_list.Add(new ibcallfollowup_list
                        {
                            followup_date = (dr_datarow["followup_date"].ToString()),
                            followup_time = (dr_datarow["followup_time"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                }
                values.ibcallfollowup_list = getibcallfollowup_list;
                dt_datatable.Dispose();

                msSQL = "select date_format(a.extendfollowup_date, '%d-%m-%Y') as followup_date,time_format(a.extendfollowup_time, '%H:%i') as followup_time ,a.extendfollowup_remarks," +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as extendfollowup_by" +
              " from ocs_trn_tinboundcall a " +
               " left join hrm_mst_temployee b on a.extendfollowup_by = b.employee_gid" +
               " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
              " where inboundcall_gid = '" + inboundcall_gid + "' and followup_date is not null";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinboundcallfollowup_list = new List<inboundcallfollowup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinboundcallfollowup_list.Add(new inboundcallfollowup_list
                        {
                            extendfollowup_date = (dr_datarow["followup_date"].ToString()),
                            extendfollowup_time = (dr_datarow["followup_time"].ToString()),
                            extendfollowup_remarks = (dr_datarow["extendfollowup_remarks"].ToString()),
                            extendfollowup_by = (dr_datarow["extendfollowup_by"].ToString()),
                        });
                    }
                }
                values.inboundcallfollowup_list = getinboundcallfollowup_list;
                dt_datatable.Dispose();

                msSQL = " select taggedmember_name," +
                 " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as tagged_by,date_format(a.tagged_date,'%d-%m-%Y %h:%i %p') as tagged_date" +
                 " from ocs_trn_tinboundcall2taggedmember a" +
                 " left join hrm_mst_temployee b on b.employee_gid=a.tagged_by " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                 " where inboundcall_gid = '" + inboundcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcalltaggedmember_list = new List<ibcalltaggedmember_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcalltaggedmember_list.Add(new ibcalltaggedmember_list
                        {
                            taggedmember_name = (dr_datarow["taggedmember_name"].ToString()),
                            tagged_by = (dr_datarow["tagged_by"].ToString()),
                            tagged_date = (dr_datarow["tagged_date"].ToString()),
                        });
                    }
                    values.ibcalltaggedmember_list = getibcalltaggedmember_list;
                }
                dt_datatable.Dispose();
                msSQL = " select a.status,a.remarks," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
               " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as overall_detail" +
                " from ocs_trn_tstatuslog a" +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                 " left join hrm_mst_temployee d on d.employee_gid=a.overall_detail " +
                " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                " where inboundcall_gid = '" + inboundcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcallstatus_list = new List<ibcallstatus_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcallstatus_list.Add(new ibcallstatus_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            overall_detail = (dr_datarow["overall_detail"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.ibcallstatus_list = getibcallstatus_list;
                }
                dt_datatable.Dispose();

                msSQL = " select transferfrom_name,transferto_name,a.transfer_remarks," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as transfer_by,date_format(a.transfer_date,'%d-%m-%Y %h:%i %p') as transfer_date" +
                " from ocs_trn_tinboundcalltransferlog a" +
                " left join hrm_mst_temployee b on b.employee_gid=a.transfer_by " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                " where inboundcall_gid = '" + inboundcall_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getibcalltransfer_list = new List<ibcalltransfer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getibcalltransfer_list.Add(new ibcalltransfer_list
                        {
                            transferfrom_name = (dr_datarow["transferfrom_name"].ToString()),
                            transferto_name = (dr_datarow["transferto_name"].ToString()),
                            transfer_by = (dr_datarow["transfer_by"].ToString()),
                            transfer_date = (dr_datarow["transfer_date"].ToString()),
                            transfer_remarks = (dr_datarow["transfer_remarks"].ToString())
                        });
                    }
                    values.ibcalltransfer_list = getibcalltransfer_list;
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
        public void DaIBCallAssignedClosed(string employee_gid, MdlIBCall values)
        {

            msSQL = " update ocs_trn_tinboundcall set callclosure_status='Closed'," +
                     " closed_flag = 'Y'," +
                     " closed_remarks='" + values.closed_remarks.Replace("'", "") + "'," +
                     " closed_by = '" + employee_gid + "'," +
                     " closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where inboundcall_gid='" + values.inboundcall_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Inbound Call Closed Successfully";


                msGetGid2 = objcmnfunctions.GetMasterGID("STAT");
                msSQL = "Insert into ocs_trn_tstatuslog( " +
                           " statuslog_gid," +
                           " inboundcall_gid," +
                           " status," +
                           " overall_detail," +
                           " remarks," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid2 + "'," +
                           "'" + values.inboundcall_gid + "'," +
                           "' Closed'," +
                           "'" + employee_gid + "'," +
                           "'" + values.closed_remarks.Replace("'", "") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " SELECT taggeduser_flag from ocs_trn_tinboundcall2taggedmember" +
                                     " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                ls_taguser = objdbconn.GetExecuteScalar(msSQL);

                if (ls_taguser == "Y")
                {
                    values.status = true;
                    values.message = "Call Closed Successfully";
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

                    msSQL = " select a.ticket_refid, a.caller_name, a.requirement,(a.created_by) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " from ocs_trn_tinboundcall a " +
                                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsrequirement = objODBCDatareader["requirement"].ToString();
                        lsassignemployee_name = objODBCDatareader["created_by"].ToString();
                        lscaller_name = objODBCDatareader["caller_name"].ToString();
                        lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                        lsfunction_name = objODBCDatareader["function_name"].ToString();
                        lsto2members = objODBCDatareader["to2members"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " SELECT group_concat(distinct taggedmember_gid) as taggedmember from ocs_trn_tinboundcall2taggedmember" +
                             " where inboundcall_gid ='" + values.inboundcall_gid + "'";
                    ls_taggedmember = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + ls_taggedmember.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                   "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                   "where b.employee_gid ='" + employee_gid + "'";
                    employeename = objdbconn.GetExecuteScalar(msSQL);


                    sub = " A Inbound Call Ticket Closed - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                    body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                    body = body + "Greetings! <br><br>";
                    body = body + "A ticket has been Closed.<br><br>";
                    body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                    body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                    body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                    body = body + "Call Close by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                    body = body + "Call Close time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                    body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                    body = body + "Regards<br><br>";
                    body = body + "Inbound - Customer Service Helpline<br><br>";
                    body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(tomail_id));
                    lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                        msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                           " inboundcall_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.inboundcall_gid + "'," +
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
                        values.message = "Call Closed Successfully";

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

                        msSQL = " select a.ticket_refid, a.caller_name,a.assignemployee_name, a.requirement,(a.assignemployee_gid) as to2members, a.function_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " from ocs_trn_tinboundcall a " +
                                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                   " where inboundcall_gid ='" + values.inboundcall_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequirement = objODBCDatareader["requirement"].ToString();
                            lsassignemployee_name = objODBCDatareader["assignemployee_name"].ToString();
                            lscaller_name = objODBCDatareader["caller_name"].ToString();
                            lsticket_refid = objODBCDatareader["ticket_refid"].ToString();
                            lsfunction_name = objODBCDatareader["function_name"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsto2members + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " A Inbound Call Ticket Closed - " + HttpUtility.HtmlEncode(lsrequirement) + " ";
                        body = "Hi " + HttpUtility.HtmlEncode(lsassignemployee_name) + ",<br><br>";
                        body = body + "Greetings! <br><br>";
                        body = body + "A ticket has been Closed.<br><br>";
                        body = body + "Caller Name:" + HttpUtility.HtmlEncode(lscaller_name) + "<br><br>";
                        body = body + "Requirement Title:" + HttpUtility.HtmlEncode(lsrequirement) + "<br><br>";
                        body = body + "Ticket request Ref ID:" + HttpUtility.HtmlEncode(lsticket_refid) + "<br><br>";
                        body = body + "Call Closed by:" + HttpUtility.HtmlEncode(employeename) + "<br><br>";
                        body = body + "Call Closed time:" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br><br>";
                        body = body + "Click the link to enter the web portal and respond <a href='https://" + ConfigurationManager.AppSettings["livedomain_url"].ToString() + "/v1'>Click Here</a><br><br>";
                        body = body + "Regards<br><br>";
                        body = body + "Inbound - Customer Service Helpline<br><br>";
                        body = body + "Disclaimer: This is a system generated mail do not reply<br>";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(tomail_id));
                        lsBccmail_id = ConfigurationManager.AppSettings["telecallingbcc"].ToString();
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
                            msSQL = "Insert into ocs_trn_ttelecallingmailcount( " +
                               " inboundcall_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.inboundcall_gid + "'," +
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
    }
}