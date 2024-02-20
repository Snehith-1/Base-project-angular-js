using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.master.Models;
using System.Net.Mail;
using System.Net;

/// <summary>
/// (It's used for Kyc View) Kyc View DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Praveen Raj</remarks>

namespace ems.master.DataAccess
{
    public class DaKycView
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;

        public void DaGetPANAuthenticationDtl(string employee_gid, string application_gid, MdlPANAuthentication values)
        {
            msSQL = " select a.pan_no, a.remarks, a.validation_status," +
                    " case when a.pan_name = '' then '-' else a.pan_name end as pan_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_tkycpanauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.updated_by" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.created_by" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " where function_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpanauthentication_list = new List<panauthentication_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getpanauthentication_list.Add(new panauthentication_list
                    {
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        pan_name = (dr_datarow["pan_name"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        validation_status = (dr_datarow["validation_status"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.panauthentication_list = getpanauthentication_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetPANAadhaarLinkDtl(string employee_gid, string application_gid, MdlPANAadhaarLink values)
        {
            msSQL = " select a.pan_no, a.remarks, a.validation_status," +
                    " case when a.panaadhaarlink_status = '' then '-' else a.panaadhaarlink_status end as panaadhaarlink_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_tkycpanaadhaarlink a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.updated_by" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.created_by" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " where function_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpanaadhaarlink_list = new List<panaadhaarlink_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getpanaadhaarlink_list.Add(new panaadhaarlink_list
                    {
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        panaadhaarlink_status = (dr_datarow["panaadhaarlink_status"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        validation_status = (dr_datarow["validation_status"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.panaadhaarlink_list = getpanaadhaarlink_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetDLAuthenticationDtl(string employee_gid, string application_gid, MdlDLAuthentication values)
        {
            msSQL = " select a.kycdlauthentication_gid,a.dlno, a.remarks, a.validation_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_tkycdlauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.updated_by" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.created_by" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " where function_gid='" + application_gid + "'";
            //" where function_gid='" + application_gid + "' or function_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdlauthentication_list = new List<dlauthentication_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdlauthentication_list.Add(new dlauthentication_list
                    {
                        kycdlauthentication_gid = (dr_datarow["kycdlauthentication_gid"].ToString()),
                        dlno = (dr_datarow["dlno"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        validation_status = (dr_datarow["validation_status"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.dlauthentication_list = getdlauthentication_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetEPICAuthenticationDtl(string employee_gid, string application_gid, MdlEPICAuthentication values)
        {
            msSQL = " select a.kycepicauthentication_gid,a.epic_no, a.remarks, a.validation_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_tkycepicauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.updated_by" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.created_by" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " where function_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getepicauthentication_list = new List<epicauthentication_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getepicauthentication_list.Add(new epicauthentication_list
                    {
                        kycepicauthentication_gid = (dr_datarow["kycepicauthentication_gid"].ToString()),
                        epic_no = (dr_datarow["epic_no"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        validation_status = (dr_datarow["validation_status"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.epicauthentication_list = getepicauthentication_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetIFSCAuthenticationDtl(string employee_gid, string application_gid, MdlIFSCAuthentication values)
        {
            msSQL = " select a.kycifscauthentication_gid,a.ifsc, a.remarks, a.validation_status," +
                    " case when a.bank = '' then '-' else a.bank end as bank," +
                    " case when a.branch = '' then '-' else a.branch end as branch," +
                    " case when a.address = '' then '-' else a.address end as address," +
                    " case when a.micr = '' then '-' else a.micr end as micr," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_tkycifscauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.updated_by" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.created_by" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " where function_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getifscauthentication_list = new List<ifscauthentication_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getifscauthentication_list.Add(new ifscauthentication_list
                    {
                        kycifscauthentication_gid = (dr_datarow["kycifscauthentication_gid"].ToString()),
                        ifsc = (dr_datarow["ifsc"].ToString()),
                        bank = (dr_datarow["bank"].ToString()),
                        branch = (dr_datarow["branch"].ToString()),
                        address = (dr_datarow["address"].ToString()),
                        micr = (dr_datarow["micr"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        validation_status = (dr_datarow["validation_status"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.ifscauthentication_list = getifscauthentication_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetBankAccVerificationDtl(string employee_gid, string application_gid, MdlBankAccVerification values)
        {
            msSQL = " select a.kycbankaccverification_gid,a.ifsc,a.accountNumber, a.remarks, a.validation_status," +
                    " case when a.accountName = '' then '-' else a.accountName end as accountName," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_tkycbankaccverification a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.updated_by" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.created_by" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " where function_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbankaccverification_list = new List<bankaccverification_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbankaccverification_list.Add(new bankaccverification_list
                    {
                        kycbankaccverification_gid = (dr_datarow["kycbankaccverification_gid"].ToString()),
                        ifsc = (dr_datarow["ifsc"].ToString()),
                        accountNumber = (dr_datarow["accountNumber"].ToString()),
                        accountName = (dr_datarow["accountName"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        validation_status = (dr_datarow["validation_status"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.bankaccverification_list = getbankaccverification_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetGSTSBPANDtl(string employee_gid, string application_gid, MdlGSTSBPAN values)
        {
            msSQL = " select a.kycgstsbpan_gid,a.pan, a.remarks, a.validation_status," +
                    " case when a.gstValues = '' then '-' else a.gstValues end as gstValues," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_tkycgstsbpan a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.updated_by" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.created_by" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " where function_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgstsbpan_list = new List<gstsbpan_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgstsbpan_list.Add(new gstsbpan_list
                    {
                        kycgstsbpan_gid = (dr_datarow["kycgstsbpan_gid"].ToString()),
                        pan = (dr_datarow["pan"].ToString()),
                        gstValues = (dr_datarow["gstValues"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        validation_status = (dr_datarow["validation_status"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.gstsbpan_list = getgstsbpan_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetPassportAuthenticationDtl(string employee_gid, string application_gid, MdlPassportAuthentication values)
        {
            msSQL = " select a.kycpassportauthentication_gid,a.fileNo, a.remarks, a.validation_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_tkycpassportauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.updated_by" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.created_by" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " where function_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpassportauthentication_list = new List<passportauthentication_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getpassportauthentication_list.Add(new passportauthentication_list
                    {
                        kycpassportauthentication_gid = (dr_datarow["kycpassportauthentication_gid"].ToString()),
                        fileNo = (dr_datarow["fileNo"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        validation_status = (dr_datarow["validation_status"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.passportauthentication_list = getpassportauthentication_list;
            }
            dt_datatable.Dispose();
        }

        public void GetUDYAMAuthenticationDtl(string employee_gid, string application_gid, MdlUDYAMAuthentication values)
        {
            msSQL = " select a.udyamreg_no, a.remarks, a.validation_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_tkycudyamauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.updated_by" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.created_by" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " where function_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getudyamauthentication_list = new List<udyamauthentication_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getudyamauthentication_list.Add(new udyamauthentication_list
                    {
                        udyamreg_no = (dr_datarow["udyamreg_no"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        validation_status = (dr_datarow["validation_status"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.udyamauthentication_list = getudyamauthentication_list;
            }
            dt_datatable.Dispose();
        }


    }
}