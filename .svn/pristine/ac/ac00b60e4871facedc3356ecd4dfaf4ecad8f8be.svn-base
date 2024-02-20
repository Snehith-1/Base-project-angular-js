using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
using System.Web;
using OfficeOpenXml;
using RestSharp;
using System.Configuration;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using ems.storage.Functions;
using ems.hbapiconn.Functions;
using ems.hbapiconn.Models;
using Newtonsoft.Json;
using System.Web.Http;
using System.Threading;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace ems.system.DataAccess
{
    public class DaManageEmployee
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        FnSamAgroHBAPIConn objFnSamAgroHBAPIConn = new FnSamAgroHBAPIConn();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msEmployeeGID, msGetGid, msGetPrivilege_gid, msGetModule2employee_gid, msGetAPICode;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        int mnResult6;
        string lsentity_flag = string.Empty;
        int  k, ls_port, hierarchy_level;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, employee_reporting_to, erp_id;      
        string lsemployee_name, lsemployee_gid, lscreated_by, lsemployee_reportingto, lsmodule2employee_gid;
        string sToken = string.Empty;
        Random rand = new Random();
        string lsto_mail, ls_baselocation,lstask_gid, lshierarchy_level, lscc_mail, strBCC, lsbcc_mail;
        string lstask_name, lstask_remarks, lscreated_date, lsbase_location, lsrptngto_erpid, lstaskassignedby, lsnewemployee_name, lsBccmail_id, lsreporting_to, lsreporting_toname;
        public string[] lsBCCReceipients;
        public string[] lsCCReceipients;
        string lsentity_name, lsemployee_emailid, lsemployee_mobileno, lsuser_firstname, lsuser_lastname, lsuser_code, lsdepartment_gid;
        string loglspath = "", logFileName = "";
        string lspath,lsentity_gid,lsemployee_externalid;
        string lsmodulereportingto_gid = "";
        public bool DaEmployeeSummary(employee_list objemployee)
        {
            try
            {
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , a.api_codes, " +
                    " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                    " c.employee_gender,  " +
                    " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                    " d.designation_name,c.designation_gid,c.employee_gid,c.employee_emailid,e.branch_name,s.baselocation_name,c.baselocation_gid,concat(v.user_firstname,' ',v.user_lastname) as employeereporting_to, " +
                    " CASE " +
                    " WHEN a.user_status = 'Y' THEN 'Active'  " +
                    " WHEN a.user_status = 'N' THEN 'Inactive' " +
                    " END as user_status,c.department_gid,c.branch_gid, e.branch_name, g.department_name " +
                    " FROM adm_mst_tuser a " +
                    " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee p on c.employeereporting_to = p.employee_gid " +
                    " left join adm_mst_tuser v on p.user_gid = v.user_gid " +
                    " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " + 
                    " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                    " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                    " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                    " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                    " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                    " left join sys_mst_tbaselocation s on s.baselocation_gid=c.baselocation_gid" +
                    " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid " +
                    " group by c.employee_gid " +
                    " order by c.employee_gid desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employeelist =new List<employee>();
                if(dt_datatable.Rows.Count !=0)
                {
                  foreach(DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employeelist.Add(new employee
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            user_gid = dr_datarow["user_gid"].ToString(),
                            branch_name = dr_datarow["branch_name"].ToString(),
                            designation_name = dr_datarow["designation_name"].ToString(),
                            department_name = dr_datarow["department_name"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            employee_name = dr_datarow["user_name"].ToString(),
                            user_status = dr_datarow["user_status"].ToString(),
                            company_name = dr_datarow["entity_name"].ToString(),
                            baselocation_gid = dr_datarow["baselocation_gid"].ToString(),
                            baselocation_name = dr_datarow["baselocation_name"].ToString(),
                             employeereporting_to = dr_datarow["employeereporting_to"].ToString(),
                            employee_emailid = dr_datarow["employee_emailid"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            api_code = dr_datarow["api_codes"].ToString()
                            
                        });
                    }
                    objemployee.employee = get_employeelist;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        
        public bool DaEmployeeActiveSummary(employee_list objemployee)
        {
            try
            {
                msSQL = " Select distinct a.user_gid,c.erp_id,c.useraccess,a.api_codes,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , " +
                    " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                    " c.employee_gender,  " +
                    " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                    " d.designation_name,c.designation_gid,c.employee_gid,c.employee_emailid,e.branch_name,s.baselocation_name,c.baselocation_gid,concat(v.user_firstname,' ',v.user_lastname) as employeereporting_to, " +
                    " concat(r.user_firstname,' ',r.user_lastname) as created_by, date_format(c.created_date, '%d-%m-%Y') as created_date, c.employee_externalid, c.employee_entitychange_flag," +
                    " CASE " +
                    " WHEN a.user_status = 'Y' THEN 'Active'  " +
                    " WHEN a.user_status = 'N' THEN 'Inactive' " +
                    " END as user_status,c.department_gid,c.branch_gid, e.branch_name, g.department_name" +
                    " FROM adm_mst_tuser a " +
                    " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee p on c.employeereporting_to = p.employee_gid " +
                    " left join hrm_mst_temployee q on c.created_by = q.employee_gid " +
                    " left join adm_mst_tuser r on q.user_gid = r.user_gid " +
                    " left join adm_mst_tuser v on p.user_gid = v.user_gid " +
                    " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                    " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                    " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                    " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                    " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                    " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                    " left join sys_mst_tbaselocation s on s.baselocation_gid=c.baselocation_gid" +
                    " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid " +
                    " left join sys_mst_temployeelog n on n.employee_gid = c.employee_gid " +
                    " where a.user_status = 'Y' and (c.employee_status = 'A' || c.employee_status = null)" +
                    " group by c.employee_gid " +
                    " order by c.created_date desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employeelist = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employeelist.Add(new employee
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            user_gid = dr_datarow["user_gid"].ToString(),
                            branch_name = dr_datarow["branch_name"].ToString(),
                            designation_name = dr_datarow["designation_name"].ToString(),
                            department_name = dr_datarow["department_name"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            employee_name = dr_datarow["user_name"].ToString(),
                            user_status = dr_datarow["user_status"].ToString(),
                            company_name = dr_datarow["entity_name"].ToString(),
                            baselocation_gid = dr_datarow["baselocation_gid"].ToString(),
                            baselocation_name = dr_datarow["baselocation_name"].ToString(),
                            employeereporting_to = dr_datarow["employeereporting_to"].ToString(),
                            employee_emailid = dr_datarow["employee_emailid"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date= dr_datarow["created_date"].ToString(),
                            erp_id = dr_datarow["erp_id"].ToString(),
                            employee_externalid = dr_datarow["employee_externalid"].ToString(),
                            employee_entitychange_flag = dr_datarow["employee_entitychange_flag"].ToString(),
                            api_code = dr_datarow["api_codes"].ToString()
                        });
                    }
                    objemployee.employee = get_employeelist;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        public bool DaEmployeeRelievedSummary(employee_list objemployee)
        {
            try
            {
                msSQL = "select b.employee_gid,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name, " +
                    " a.user_code ,concat(a.user_firstname,' ',a.user_lastname) as user_name , b.remarks, date_format(b.created_date, '%d-%m-%Y') as created_date, " +
                    " concat(e.user_firstname,' ',e.user_lastname) as created_by,date_format( b.exit_date, '%d-%m-%Y') as exit_date,concat(g.user_firstname, ' ', g.user_lastname) as employeereporting_to," +
                    " s.baselocation_name,c.baselocation_gid,c.department_gid,h.department_name, " +
                    " CASE " +
                   " WHEN a.user_status = 'Y' THEN 'Active'  " +
                   " WHEN a.user_status = 'N' THEN 'Inactive' " +
                   " END as user_access " +
                    " from hrm_trn_texitemployee b" +
                    " left join hrm_mst_temployee c on c.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser a on c.user_gid = a.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = b.created_by " +
                    " left join adm_mst_tuser e on d.user_gid = e.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = c.employeereporting_to "+
                    " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                    " left join sys_mst_tbaselocation s on s.baselocation_gid = c.baselocation_gid " + 
                    " left join hrm_mst_tdepartment h on h.department_gid = c.department_gid" +
                    " left join adm_mst_tentity z on z.entity_gid = c.entity_gid " +
                    " left join sys_mst_temployeelog i on c.employee_gid = i.employee_gid " +
                   " where a.user_status = 'Y' and  c.employee_status = 'R'" +
                    " group by c.employee_gid " +
                    " order by b.exit_date desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employeelist = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employeelist.Add(new employee
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            department_name = dr_datarow["department_name"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            employee_name = dr_datarow["user_name"].ToString(),
                            entity_name = dr_datarow["entity_name"].ToString(),
                            baselocation_gid = dr_datarow["baselocation_gid"].ToString(),
                            baselocation_name = dr_datarow["baselocation_name"].ToString(),
                            employeereporting_to = dr_datarow["employeereporting_to"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            remarks = dr_datarow["remarks"].ToString(),
                            relive_date = dr_datarow["exit_date"].ToString(),
                            user_access = dr_datarow["user_access"].ToString()
                    });
                    }
                    objemployee.employee = get_employeelist;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        public bool DaEmployeePendingSummary(employee_list objemployee)
        {
            try
            {
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , " +
                    " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,a.api_codes," +
                    " c.employee_gender,  " +
                    " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                    " d.designation_name,c.designation_gid,c.employee_gid,c.employee_emailid,e.branch_name,s.baselocation_name,"+
                    " c.baselocation_gid,concat(v.user_firstname,' ',v.user_lastname) as employeereporting_to, " +
                    " CASE " +
                    " WHEN a.user_status = 'Y' THEN 'Active'  " +
                    " WHEN a.user_status = 'N' THEN 'Inactive' " +
                    " END as user_status,c.department_gid,c.branch_gid, date_format(c.employee_joiningdate,'%d-%m-%Y') as joining_date,"+
                    " e.branch_name, g.department_name, date_format(c.created_date,'%d-%m-%Y') as created_date,concat(r.user_firstname,' ',r.user_lastname) as created_by, b.employee_status" +
                    " FROM adm_mst_tuser a " +
                    " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee p on c.employeereporting_to = p.employee_gid " +
                    " left join hrm_mst_temployee q on q.employee_gid = c.created_by " +
                    " left join adm_mst_tuser r on q.user_gid = r.user_gid " +
                    " left join adm_mst_tuser v on p.user_gid = v.user_gid " +
                    " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                    " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                    " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                    " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                    " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                    " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                    " left join sys_mst_tbaselocation s on s.baselocation_gid=c.baselocation_gid" +
                    " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid " +
                    " left join sys_mst_temployeelog b on b.employee_gid = c.employee_gid " +
                    " where a.user_status = 'N' and c.employee_status = 'P'" +
                    " group by c.employee_gid " +
                    " order by c.employee_gid desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employeelist = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employeelist.Add(new employee
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            user_gid = dr_datarow["user_gid"].ToString(),
                            branch_name = dr_datarow["branch_name"].ToString(),
                            designation_name = dr_datarow["designation_name"].ToString(),
                            department_name = dr_datarow["department_name"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            employee_name = dr_datarow["user_name"].ToString(),
                            user_status = dr_datarow["user_status"].ToString(),
                            entity_name = dr_datarow["entity_name"].ToString(),
                            baselocation_gid = dr_datarow["baselocation_gid"].ToString(),
                            baselocation_name = dr_datarow["baselocation_name"].ToString(),
                            employeereporting_to = dr_datarow["employeereporting_to"].ToString(),
                            employee_emailid = dr_datarow["employee_emailid"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            joining_date = dr_datarow["joining_date"].ToString(),
                            employee_status = dr_datarow["employee_status"].ToString(),
                            api_code = dr_datarow["api_codes"].ToString()
                        });
                    }
                    objemployee.employee = get_employeelist;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        public bool DaEmployeeInactiveSummary(employee_list objemployee)
        {
            try
            {
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name ,"+
                        " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,concat(l.user_firstname,' ',l.user_lastname) as employeereporting_to, " +
                        " date_format(c.employee_joiningdate, '%d-%m-%Y') as joining_date,c.employee_gid, date_format(c.updated_date, '%d-%m-%Y') as updated_date, " +
                        " concat(e.user_firstname,' ',e.user_lastname) as updated_by,c.remarks  " +
                        " FROM adm_mst_tuser a left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                        " left join hrm_mst_temployee d on d.employee_gid = c.updated_by " +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                        " left join adm_mst_tentity z on z.entity_gid=c.entity_gid " +
                        " left join sys_mst_temployeelog n on n.employee_gid=c.employee_gid " +
                        " left join hrm_mst_temployee m on m.employee_gid=c.employeereporting_to" +
                        " left join adm_mst_tuser l on m.user_gid=l.user_gid " +
                        " where a.user_status = 'N' and c.employee_status='I'" +
                        " group by employee_gid " +
                        " order by updated_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employeelist = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employeelist.Add(new employee
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            user_gid = dr_datarow["user_gid"].ToString(),
                            user_code = dr_datarow["user_code"].ToString(),
                            employee_name = dr_datarow["user_name"].ToString(),
                            employeereporting_to = dr_datarow["employeereporting_to"].ToString(),
                            remarks = dr_datarow["remarks"].ToString(),
                            entity_name = dr_datarow["entity_name"].ToString(),
                            updated_date = dr_datarow["updated_date"].ToString(),
                            updated_by = dr_datarow["updated_by"].ToString(),
                            joining_date = dr_datarow["joining_date"].ToString(),
                        });
                    }
                    objemployee.employee = get_employeelist;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        //UPDATE CHANGE
        public bool DaEmployeeAdd(employee objemployee, string employee_gid, string user_gid)
        {
            msSQL = "select user_gid from adm_mst_tuser where user_code = '" + objemployee.user_code + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objemployee.message = "User Code Already in Use";
                return false;
            }
            else
            {

                string lsgender = string.Empty;
            string msBiometricGID = string.Empty;
            string msUserGid = string.Empty;
            string msPermanentAddressGetGID = string.Empty;
            string msTemporaryAddressGetGID = string.Empty;
            string str1 = string.Empty;
            string msEmployeedtlGid = string.Empty;
            string msGetleaveGID = string.Empty;
            string msGetemployeetype = string.Empty;
            string msgetgidof = string.Empty;
            string msGetemployeelog = string.Empty;

                try
                {
                    msUserGid = objcmnfunctions.GetMasterGID("SUSM");
                    if (msUserGid == "E")
                    {
                        objemployee.message = "Create Sequence Code SUSM for User Table";
                        objemployee.status = false;
                    }
                    msEmployeeGID = objcmnfunctions.GetMasterGID("SERM");
                    if (msEmployeeGID == "E")
                    {
                        objemployee.message = "Create Sequence Code SERM for Employee Table";
                        objemployee.status = false;
                    }
                    msPermanentAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                    if (msPermanentAddressGetGID == "E")
                    {
                        objemployee.message = "Create Sequence Code SADM for Address Table";
                        objemployee.status = false;
                    }
                    msTemporaryAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
                    if (msTemporaryAddressGetGID == "E")
                    {
                        objemployee.message = "Create Sequence Code SADM for Address Table";
                        objemployee.status = false;
                    }
                    msEmployeedtlGid = objcmnfunctions.GetMasterGID("HEMC");
                    if (msEmployeedtlGid == "E")
                    {
                        objemployee.message = "Create Sequence Code HEMC for Employee Detail Table";
                        objemployee.status = false;
                    }
                    str1 = objcmnfunctions.PopTransactionUpload(objemployee.employee_photo, objemployee.employee_gid, "System", "Photos");
                    msGetAPICode = objcmnfunctions.GetApiMasterGID("MEAC");
                    if (objemployee.user_password != null & objemployee.user_password != string.Empty & objemployee.user_password != "")
                    {
                        var encripedpassword = objcmnfunctions.ConvertToAscii(objemployee.user_password);
                        msSQL = " Insert into adm_mst_tuser ( " +
                         " user_gid , " +
                         " api_codes , " +
                         " user_code , " +
                         " user_firstname , " +
                         " user_lastname , " +
                         " user_password , " +
                         " user_status  ," +
                         " created_by, " +
                         " created_date " +
                         " )values ( " +
                         "'" + msUserGid + "'," +
                         "'" + msGetAPICode + "'," +
                         " '" + objemployee.user_code + "'," +
                         " '" + objemployee.user_firstname + "'," +
                         " '" + objemployee.user_lastname + "'," +
                         " '" + encripedpassword + "'," +
                         "'N'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " Insert into adm_mst_tuser ( " +
                         " user_gid , " +
                         " api_codes , " +
                         " user_code , " +
                         " user_firstname , " +
                         " user_lastname , " +
                         " user_status , " +
                         " created_by, " +
                         " created_date " +
                         " )values ( " +
                         "'" + msUserGid + "'," +
                         "'" + msGetAPICode + "'," +
                         " '" + objemployee.user_code + "'," +
                         " '" + objemployee.user_firstname + "'," +
                         " '" + objemployee.user_lastname + "'," +
                         "'N'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }


                    if (mnResult == 1)
                    {
                        msSQL = " Insert into hrm_mst_temployee " +
                               " (employee_gid , " +
                               " user_gid," +
                               " employee_externalid," +
                               " designation_gid," +
                               " employee_mobileno , " +
                               " employee_emailid , " +
                               " employee_gender , " +
                               " department_gid," +
                               " employee_photo," +
                               " useraccess," +
                               " engagement_type," +
                               " created_by, " +
                               " created_date, " +
                               " employeereporting_to, " +
                               " role_gid," +
                               " attendance_flag, " +
                               " branch_gid, " +
                               " baselocation_gid, " +
                               " marital_status, " +
                               " marital_status_gid, " +
                               " bloodgroup," +
                               " bloodgroup_gid," +
                               " employee_joiningdate," +
                               " employee_personalno," +
                               " personal_emailid, " +
                               " employee_status," +
                               " subfunction_gid " +
                               " )values( " +
                               "'" + msEmployeeGID + "', " +
                               "'" + msUserGid + "', " +
                               "'" + msEmployeeGID + "', " + 
                               "'" + objemployee.designation_gid + "'," +
                               "'" + objemployee.employee_mobileno + "'," +
                               "'" + objemployee.employee_emailid + "'," +
                               "'" + objemployee.gender + "'," +
                               "'" + objemployee.department_gid + "'," +
                               "'" + str1 + "'," +
                               "'" + objemployee.useraccess + "'," +
                               "'Direct'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + objemployee.employee_reportingto + "'," +
                               "'" + objemployee.role_gid + "'," +
                               " 'Y', " +
                               "'" + objemployee.branch_gid + "'," +
                               "'" + objemployee.baselocation_gid + "'," +
                               "'" + objemployee.marital_status + "'," +
                               "'" + objemployee.marital_status_gid + "'," +
                               "'" + objemployee.bloodgroup_name + "'," +
                               "'" + objemployee.bloodgroup_gid + "'," +
                               "'" + Convert.ToDateTime(objemployee.joining_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + objemployee.personal_phone_no + "'," +
                               "'" + objemployee.personal_emailid + "'," +
                               "'P'," +
                               "'" + objemployee.subfunction_gid + "')";
                        mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        string lsentity_flag;
                        msSQL = "SELECT entity_flag from adm_mst_tcompany where 1=1";
                        lsentity_flag = objdbconn.GetExecuteScalar(msSQL);
                        if (lsentity_flag == "Y")
                        {
                            msSQL = " update hrm_mst_temployee set entity_gid='" + objemployee.entity_gid + "' where" +
                                   " employee_gid='" + msEmployeeGID + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msSQL = " update hrm_mst_temployee set entity_name='" + objemployee.company_name + "'" +
                                    " where employee_gid='" + msEmployeeGID + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        msGetleaveGID = objcmnfunctions.GetMasterGID("HLCP");
                        msSQL = "Insert Into hrm_trn_tleavecredits " +
                            "(leavecredits_gid," +
                            "employee_gid)" +
                            " VALUES " +
                            "('" + msGetleaveGID + "'," +
                            " '" + msEmployeeGID + "')";
                        mnResult6 = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    if (mnResult == 1)
                    {
                        msGetemployeetype = objcmnfunctions.GetMasterGID("SETD");
                        if (msGetemployeetype == "E")
                        {
                            objemployee.message = "Create Sequence Code SETD for Employee Table";
                            objemployee.status = false;
                            return false;
                        }

                        msSQL = " insert into hrm_trn_temployeetypedtl(" +
                            " employeetypedtl_gid," +
                            " employee_gid, " +
                            " workertype_gid," +
                            " systemtype_gid, " +
                            " branch_gid," +
                            " wagestype_gid," +
                            " department_gid," +
                            " designation_gid," +
                            " employeetype_name," +
                            " created_date," +
                            "created_by)" +
                            " values " +
                            " ('" + msGetemployeetype + "', " +
                            " '" + msEmployeeGID + "'," +
                            " null, " +
                            " 'Audit', " +
                            " '" + objemployee.branch_gid + "'," +
                            " 'wg001', " +
                            " '" + objemployee.department_gid + "'," +
                            " '" + objemployee.designation_gid + "'," +
                            " 'Roll', " +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            " '" + employee_gid + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 0)
                        {
                            objemployee.message = "Error while inserting employeetype";
                            objemployee.status = false;
                        }
                        msSQL = " insert into adm_mst_taddress " +
                                " (address_gid ," +
                                " parent_gid," +
                                " country_gid, " +
                                " address1, " +
                                " address2, " +
                                " city, " +
                                " postal_code," +
                                " state) " +
                                " values( " +
                                " '" + msPermanentAddressGetGID + "', " +
                                " '" + msEmployeeGID + "', " +
                                " '" + objemployee.per_country_gid + "', " +
                                " '" + objemployee.per_address1 + "', " +
                                " '" + objemployee.per_address2 + "', " +
                                " '" + objemployee.per_city + "', " +
                                " '" + objemployee.per_postal_code + "'," +
                                " '" + objemployee.per_state + "')";
                        mnResult2 = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " insert into adm_mst_taddress " +
                               " (address_gid ," +
                               " parent_gid," +
                               " country_gid, " +
                               " address1, " +
                               " address2, " +
                               " city, " +
                               " postal_code," +
                               " state) " +
                               " values( " +
                               " '" + msTemporaryAddressGetGID + "', " +
                               " '" + msEmployeeGID + "', " +
                               " '" + objemployee.temp_country_gid + "', " +
                               " '" + objemployee.temp_address1 + "', " +
                               " '" + objemployee.temp_address2 + "', " +
                               " '" + objemployee.temp_city + "', " +
                               " '" + objemployee.temp_postal_code + "'," +
                               " '" + objemployee.temp_state + "')";
                        mnResult3 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult3 == 1)
                        {
                            msSQL = " Insert into hrm_trn_temployeedtl " +
                                " (employeedtl_gid," +
                                " permanentaddress_gid ," +
                                " temporaryaddress_gid," +
                                " employee_gid)" +
                                " VALUE ( " +
                                " '" + msEmployeedtlGid + "'," +
                                " '" + msPermanentAddressGetGID + "'," +
                                " '" + msTemporaryAddressGetGID + "' ," +
                                " '" + msEmployeeGID + "')";
                            mnResult4 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    //Employee Status - Pending 
                    msGetemployeelog = objcmnfunctions.GetMasterGID("EMLG");
                    msSQL = " Insert into sys_mst_temployeelog " +
                                " (employeelog_gid," +
                                " employee_gid ," +
                                " user_gid," +
                                " user_name," +
                                " created_by," +
                                " created_date)" +
                                " VALUE ( " +
                                " '" + msGetemployeelog + "'," +
                                " '" + msEmployeeGID + "'," +
                                "'" + msUserGid + "', " +
                                " '" + objemployee.user_firstname + " " + objemployee.user_lastname + "/" + objemployee.user_code + "'," +
                                " '" + employee_gid + "'," +
                                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                    mnResult4 = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //Manage Privileges

                        msSQL = " select module_gid from sys_mst_tmenumapping where module_gid_parent='$'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {


                            msSQL = "select hierarchy_level, employee_gid  as  reportingto  from adm_mst_tmodule2employee " +
                                    " where default_manager_flag ='Y' and module_gid ='" + dt["module_gid"].ToString() + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                objemployee.hierarchy_level = objODBCDatareader["hierarchy_level"].ToString();
                                objemployee.reportingto = objODBCDatareader["reportingto"].ToString();
                            }

                            hierarchy_level = Convert.ToInt16(objemployee.hierarchy_level);
                            hierarchy_level += 1;
                            lshierarchy_level = Convert.ToString(hierarchy_level);
                            msGetModule2employee_gid = objcmnfunctions.GetMasterGID("SMEM");



                            msSQL = " insert into adm_mst_tmodule2employee( " +
                                 " module2employee_gid, " +
                                 " module_gid, " +
                                 " employee_gid, " +
                                 " hierarchy_level," +
                                 " employeereporting_to, " +
                                 " created_by, " +
                                 " created_date) " +
                              " values(" +
                              "'" + msGetModule2employee_gid + "'," +
                              "'" + dt["module_gid"].ToString() + "'," +
                              "'" + msEmployeeGID + "'," +
                              "'" + lshierarchy_level + "'," +
                              "'" + objemployee.reportingto + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        objODBCDatareader.Close();
                    }

                    dt_datatable.Dispose();

                    msSQL = " select module_gid, module_gid_parent from sys_mst_tmenumapping where status='Y'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msGetPrivilege_gid = objcmnfunctions.GetMasterGID("SPGM");

                            msSQL = " insert into adm_mst_tprivilege( " +
                                " privilege_gid, " +
                                " module_gid, " +
                                " user_gid, " +
                                " module_parent_gid) " +
                             " values(" +
                             "'" + msGetPrivilege_gid + "'," +
                             "'" + dt["module_gid"].ToString() + "'," +
                             "'" + msUserGid + "'," +
                             "'" + dt["module_gid_parent"].ToString() + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    dt_datatable.Dispose();
                    msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany )";
                    lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select module2employee_gid from adm_mst_tmodule2employee a  where" +
                        " a.module_gid='" + lsmodulereportingto_gid + "' and a.employee_gid='" + msEmployeeGID + "'";
                    lsmodule2employee_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (lsmodule2employee_gid == "" || lsmodule2employee_gid == null)
                    {
                        msSQL = "select hierarchy_level from adm_mst_tmodule2employee a where " +
                        " a.module_gid='" + lsmodulereportingto_gid + "' and a.employee_gid='" + objemployee.employee_reportingto + "'";
                        lshierarchy_level = objdbconn.GetExecuteScalar(msSQL);
                        //if (lshierarchy_level=="" || lshierarchy_level== null)
                        //    {
                        //    lshierarchy_level = "0";

                        //}
                        hierarchy_level = Convert.ToInt16(lshierarchy_level);
                        hierarchy_level += 1;
                        lshierarchy_level = Convert.ToString(hierarchy_level);
                        msGetModule2employee_gid = objcmnfunctions.GetMasterGID("SMEM");


                        msSQL = " insert into adm_mst_tmodule2employee( " +
                             " module2employee_gid, " +
                             " module_gid, " +
                             " employee_gid, " +
                             " hierarchy_level," +
                             " employeereporting_to, " +
                             " created_by, " +
                             " created_date) " +
                          " values(" +
                          "'" + msGetModule2employee_gid + "'," +
                          "'" + lsmodulereportingto_gid + "'," +
                          "'" + msEmployeeGID + "'," +
                          "'" + lshierarchy_level + "'," +
                          "'" + objemployee.employee_reportingto + "'," +
                          "'" + user_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";


                    }

                    else
                    {
                        msSQL = "select hierarchy_level from adm_mst_tmodule2employee a where " +
                       " a.module_gid='" + lsmodulereportingto_gid + "' and a.employee_gid='" + objemployee.employee_reportingto + "'";
                        lshierarchy_level = objdbconn.GetExecuteScalar(msSQL);

                        hierarchy_level = Convert.ToInt16(lshierarchy_level);
                        hierarchy_level += 1;
                        lshierarchy_level = Convert.ToString(hierarchy_level);
                        msSQL = "Update adm_mst_tmodule2employee set employeereporting_to='" + objemployee.employee_reportingto + "' , " +
                                " hierarchy_level='" + lshierarchy_level + "' ," +
                                " updated_by ='" + user_gid + "'," +
                                " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " where module_gid='" + lsmodulereportingto_gid + "' and employee_gid='" + msEmployeeGID + "'";

                    }
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objemployee.message = "Employee Added Successfully";
                objemployee.status = true;
                return true;

            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objemployee.message = "Error While Adding Employee";
                return false;
            }

        }
    }



        //public bool DaEmployeeAdd(employee objemployee, string employee_gid, string user_gid)
        //{


        //    string lsgender = string.Empty;
        //    string msBiometricGID = string.Empty;
        //    string msUserGid = string.Empty;
        //    string msPermanentAddressGetGID = string.Empty;
        //    string msTemporaryAddressGetGID = string.Empty;
        //    string str1 = string.Empty;
        //    string msEmployeedtlGid = string.Empty;
        //    string msGetleaveGID = string.Empty;
        //    string msGetemployeetype = string.Empty;
        //    string msgetgidof = string.Empty;
        //    string msGetemployeelog = string.Empty;

        //    try
        //    {
        //        msUserGid = objcmnfunctions.GetMasterGID("SUSM");
        //        if (msUserGid == "E")
        //        {
        //            objemployee.message = "Create Sequence Code SUSM for User Table";
        //            objemployee.status = false;
        //        }
        //        msEmployeeGID = objcmnfunctions.GetMasterGID("SERM");
        //        if (msEmployeeGID == "E")
        //        {
        //            objemployee.message = "Create Sequence Code SERM for Employee Table";
        //            objemployee.status = false;
        //        }
        //        msPermanentAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
        //        if (msPermanentAddressGetGID == "E")
        //        {
        //            objemployee.message = "Create Sequence Code SADM for Address Table";
        //            objemployee.status = false;
        //        }
        //        msTemporaryAddressGetGID = objcmnfunctions.GetMasterGID("SADM");
        //        if (msTemporaryAddressGetGID == "E")
        //        {
        //            objemployee.message = "Create Sequence Code SADM for Address Table";
        //            objemployee.status = false;
        //        }
        //        msEmployeedtlGid = objcmnfunctions.GetMasterGID("HEMC");
        //        if (msEmployeedtlGid == "E")
        //        {
        //            objemployee.message = "Create Sequence Code HEMC for Employee Detail Table";
        //            objemployee.status = false;
        //        }
        //        str1 = objcmnfunctions.PopTransactionUpload(objemployee.employee_photo, objemployee.employee_gid, "System", "Photos");
        //        if (objemployee.user_password != null & objemployee.user_password != string.Empty & objemployee.user_password != "")
        //        {
        //            var encripedpassword = objcmnfunctions.ConvertToAscii(objemployee.user_password);
        //            msSQL = " Insert into adm_mst_tuser ( " +
        //             " user_gid , " +
        //             " user_code , " +
        //             " user_firstname , " +
        //             " user_lastname , " +
        //             " user_password , " +
        //             " user_status  ," +
        //             " created_by, " +
        //             " created_date " +
        //             " )values ( " +
        //             "'" + msUserGid + "'," +
        //             " '" + objemployee.user_code + "'," +
        //             " '" + objemployee.user_firstname + "'," +
        //             " '" + objemployee.user_lastname + "'," +
        //             " '" + encripedpassword + "'," +
        //             "'Y'," +
        //             "'" + employee_gid + "'," +
        //             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //        }
        //        else
        //        {
        //            msSQL = " Insert into adm_mst_tuser ( " +
        //             " user_gid , " +
        //             " user_code , " +
        //             " user_firstname , " +
        //             " user_lastname , " +
        //             " user_status , " +
        //             " created_by, " +
        //             " created_date " +
        //             " )values ( " +
        //             "'" + msUserGid + "'," +
        //             " '" + objemployee.user_code + "'," +
        //             " '" + objemployee.user_firstname + "'," +
        //             " '" + objemployee.user_lastname + "'," +
        //             "'N'," +
        //             "'" + employee_gid + "'," +
        //             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //        }


        //        if (mnResult == 1)
        //        {
        //            msSQL = " Insert into hrm_mst_temployee " +
        //                   " (employee_gid , " +
        //                   " user_gid," +
        //                   " designation_gid," +
        //                   " employee_mobileno , " +
        //                   " employee_emailid , " +
        //                   " employee_gender , " +
        //                   " department_gid," +
        //                   " employee_photo," +
        //                   " useraccess," +
        //                   " engagement_type," +
        //                   " created_by, " +
        //                   " created_date, " +
        //                   " employeereporting_to, " +
        //                   " role_gid," +
        //                   " attendance_flag, " +
        //                   " branch_gid, " +
        //                   " baselocation_gid, " +
        //                   " marital_status, " +
        //                   " marital_status_gid, " +
        //                   " bloodgroup," +
        //                   " bloodgroup_gid," +
        //                   " employee_joiningdate," +
        //                   " employee_personalno," +
        //                   " personal_emailid, " +
        //                   " employee_status" +
        //                   " )values( " +
        //                   "'" + msEmployeeGID + "', " +
        //                   "'" + msUserGid + "', " +
        //                   "'" + objemployee.designation_gid + "'," +
        //                   "'" + objemployee.employee_mobileno + "'," +
        //                   "'" + objemployee.employee_emailid + "'," +
        //                   "'" + objemployee.gender + "'," +
        //                   "'" + objemployee.department_gid + "'," +
        //                   "'" + str1 + "'," +
        //                   "'" + objemployee.useraccess + "'," +
        //                   "'Direct'," +
        //                   "'" + employee_gid + "'," +
        //                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
        //                   "'" + objemployee.employee_reportingto + "'," +
        //                   "'" + objemployee.role_gid + "'," +
        //                   " 'Y', " +
        //                   "'" + objemployee.branch_gid + "'," +
        //                   "'" + objemployee.baselocation_gid + "'," +
        //                   "'" + objemployee.marital_status + "'," +
        //                   "'" + objemployee.marital_status_gid + "'," +
        //                   "'" + objemployee.bloodgroup_name + "'," +
        //                   "'" + objemployee.bloodgroup_gid + "'," +
        //                   "'" + Convert.ToDateTime(objemployee.joining_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
        //                   "'" + objemployee.personal_phone_no + "'," +
        //                   "'" + objemployee.personal_emailid + "'," +
        //                   "'P')";
        //            mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            string lsentity_flag;
        //            msSQL = "SELECT entity_flag from adm_mst_tcompany where 1=1";
        //            lsentity_flag = objdbconn.GetExecuteScalar(msSQL);
        //            if (lsentity_flag == "Y")
        //            {
        //                msSQL = " update hrm_mst_temployee set entity_gid='" + objemployee.entity_gid + "' where" +
        //                       " employee_gid='" + msEmployeeGID + "'";
        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            }
        //            else
        //            {
        //                msSQL = " update hrm_mst_temployee set entity_name='" + objemployee.company_name + "'" +
        //                        " where employee_gid='" + msEmployeeGID + "'";
        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            }

        //            msGetleaveGID = objcmnfunctions.GetMasterGID("HLCP");
        //            msSQL = "Insert Into hrm_trn_tleavecredits " +
        //                "(leavecredits_gid," +
        //                "employee_gid)" +
        //                " VALUES " +
        //                "('" + msGetleaveGID + "'," +
        //                " '" + msEmployeeGID + "')";
        //            mnResult6 = objdbconn.ExecuteNonQuerySQL(msSQL);
        //        }
        //        if (mnResult == 1)
        //        {
        //            msGetemployeetype = objcmnfunctions.GetMasterGID("SETD");
        //            if (msGetemployeetype == "E")
        //            {
        //                objemployee.message = "Create Sequence Code SETD for Employee Table";
        //                objemployee.status = false;
        //                return false;
        //            }

        //            msSQL = " insert into hrm_trn_temployeetypedtl(" +
        //                " employeetypedtl_gid," +
        //                " employee_gid, " +
        //                " workertype_gid," +
        //                " systemtype_gid, " +
        //                " branch_gid," +
        //                " wagestype_gid," +
        //                " department_gid," +
        //                " designation_gid," +
        //                " employeetype_name," +
        //                " created_date," +
        //                "created_by)" +
        //                " values " +
        //                " ('" + msGetemployeetype + "', " +
        //                " '" + msEmployeeGID + "'," +
        //                " null, " +
        //                " 'Audit', " +
        //                " '" + objemployee.branch_gid + "'," +
        //                " 'wg001', " +
        //                " '" + objemployee.department_gid + "'," +
        //                " '" + objemployee.designation_gid + "'," +
        //                " 'Roll', " +
        //                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
        //                " '" + employee_gid + "')";

        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            if (mnResult == 0)
        //            {
        //                objemployee.message = "Error while inserting employeetype";
        //                objemployee.status = false;
        //            }
        //            msSQL = " insert into adm_mst_taddress " +
        //                    " (address_gid ," +
        //                    " parent_gid," +
        //                    " country_gid, " +
        //                    " address1, " +
        //                    " address2, " +
        //                    " city, " +
        //                    " postal_code," +
        //                    " state) " +
        //                    " values( " +
        //                    " '" + msPermanentAddressGetGID + "', " +
        //                    " '" + msEmployeeGID + "', " +
        //                    " '" + objemployee.per_country_gid + "', " +
        //                    " '" + objemployee.per_address1 + "', " +
        //                    " '" + objemployee.per_address2 + "', " +
        //                    " '" + objemployee.per_city + "', " +
        //                    " '" + objemployee.per_postal_code + "'," +
        //                    " '" + objemployee.per_state + "')";
        //            mnResult2 = objdbconn.ExecuteNonQuerySQL(msSQL);

        //            msSQL = " insert into adm_mst_taddress " +
        //                   " (address_gid ," +
        //                   " parent_gid," +
        //                   " country_gid, " +
        //                   " address1, " +
        //                   " address2, " +
        //                   " city, " +
        //                   " postal_code," +
        //                   " state) " +
        //                   " values( " +
        //                   " '" + msTemporaryAddressGetGID + "', " +
        //                   " '" + msEmployeeGID + "', " +
        //                   " '" + objemployee.temp_country_gid + "', " +
        //                   " '" + objemployee.temp_address1 + "', " +
        //                   " '" + objemployee.temp_address2 + "', " +
        //                   " '" + objemployee.temp_city + "', " +
        //                   " '" + objemployee.temp_postal_code + "'," +
        //                   " '" + objemployee.temp_state + "')";
        //            mnResult3 = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            if (mnResult3 == 1)
        //            {
        //                msSQL = " Insert into hrm_trn_temployeedtl " +
        //                    " (employeedtl_gid," +
        //                    " permanentaddress_gid ," +
        //                    " temporaryaddress_gid," +
        //                    " employee_gid)" +
        //                    " VALUE ( " +
        //                    " '" + msEmployeedtlGid + "'," +
        //                    " '" + msPermanentAddressGetGID + "'," +
        //                    " '" + msTemporaryAddressGetGID + "' ," +
        //                    " '" + msEmployeeGID + "')";
        //                mnResult4 = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            }
        //        }
        //        //Employee Status - Pending 
        //        msGetemployeelog = objcmnfunctions.GetMasterGID("EMLG");
        //        msSQL = " Insert into sys_mst_temployeelog " +
        //                    " (employeelog_gid," +
        //                    " employee_gid ," +
        //                    " user_gid," +
        //                    " user_name," +
        //                    " created_by," +
        //                    " created_date)" +
        //                    " VALUE ( " +
        //                    " '" + msGetemployeelog + "'," +
        //                    " '" + msEmployeeGID + "'," +
        //                    "'" + msUserGid + "', " +
        //                    " '" + objemployee.user_firstname + " " + objemployee.user_lastname + "/" + objemployee.user_code + "'," +
        //                    " '" + employee_gid + "'," +
        //                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

        //        mnResult4 = objdbconn.ExecuteNonQuerySQL(msSQL);

        //        // Manage Privileges

        //        msSQL = " select module_gid from sys_mst_tmenumapping where module_gid_parent='$' and status='Y'";
        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            foreach (DataRow dt in dt_datatable.Rows)
        //            {
        //                msSQL = "select hierarchy_level from adm_mst_tmodule2employee where employee_gid='" + objemployee.employee_reportingto + "' and module_gid='" + dt["module_gid"].ToString() + "'";
        //                lshierarchy_level = objdbconn.GetExecuteScalar(msSQL);
        //                hierarchy_level = Convert.ToInt16(lshierarchy_level);
        //                hierarchy_level += 1;
        //                lshierarchy_level = Convert.ToString(hierarchy_level);
        //                msGetModule2employee_gid = objcmnfunctions.GetMasterGID("SMEM");

        //                msSQL = " insert into adm_mst_tmodule2employee( " +
        //                    " module2employee_gid, " +
        //                    " module_gid, " +
        //                    " employee_gid, " +
        //                    " hierarchy_level," +
        //                    " employeereporting_to, " +
        //                    " created_by, " +
        //                    " created_date) " +
        //                 " values(" +
        //                 "'" + msGetModule2employee_gid + "'," +
        //                 "'" + dt["module_gid"].ToString() + "'," +
        //                 "'" + msEmployeeGID + "'," +
        //                 "'" + lshierarchy_level + "'," +
        //                 "'" + objemployee.employee_reportingto + "'," +
        //                 "'" + user_gid + "'," +
        //                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            }
        //        }
        //        dt_datatable.Dispose();

        //        msSQL = " select module_gid, module_gid_parent from sys_mst_tmenumapping where status='Y'";
        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            foreach (DataRow dt in dt_datatable.Rows)
        //            {
        //                msGetPrivilege_gid = objcmnfunctions.GetMasterGID("SPGM");

        //                msSQL = " insert into adm_mst_tprivilege( " +
        //                    " privilege_gid, " +
        //                    " module_gid, " +
        //                    " user_gid, " +
        //                    " module_parent_gid) " +
        //                 " values(" +
        //                 "'" + msGetPrivilege_gid + "'," +
        //                 "'" + dt["module_gid"].ToString() + "'," +
        //                 "'" + msUserGid + "'," +
        //                 "'" + dt["module_gid_parent"].ToString() + "')";
        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            }
        //        }
        //        dt_datatable.Dispose();


        //        objemployee.message = "Employee Added Successfully";
        //        objemployee.status = true;
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        objemployee.status = false;
        //        objemployee.message = "Error While Adding Employee";
        //        return false;
        //    }
        //}
        public bool DaEmployeeEditView(employee objemployee,string employee_gid)
        {
            
            try
            {
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , user_firstname,user_lastname," +
                   " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                   " c.employee_gender,c.role_gid,employeereporting_to, x.subfunction_name,c.subfunction_gid, " +
                   " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                   " d.designation_name,c.designation_gid,c.employee_gid,e.branch_name, employee_emailid,employee_mobileno,baselocation_name,s.baselocation_gid,c.entity_gid," +
                   " CASE " +
                   " WHEN a.user_status = 'Y' THEN 'Active'  " +
                   " WHEN a.user_status = 'N' THEN 'Inactive' " +
                   " END as user_access,a.user_status,c.department_gid,c.branch_gid,n.role_name, e.branch_name, g.department_name,c.marital_status,c.marital_status_gid,date_format(c.employee_joiningdate, '%d-%m-%Y') as joiningdate, " +
                   " c.employee_personalno as personal_phone_no,c.personal_emailid,c.bloodgroup as bloodgroup_name,c.bloodgroup_gid " +
                   " FROM hrm_mst_temployee c " +   
                   " left join adm_mst_tuser a on a.user_gid = c.user_gid " +
                   " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                   " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                   " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                   " left join sys_mst_tsubfunction x on x.subfunction_gid=c.subfunction_gid " +
                   " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                   " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                   " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                   " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid" +
                   " left join hrm_mst_trole n on n.role_gid=c.role_gid "+
                   " left join sys_mst_tbaselocation s on s.baselocation_gid = c.baselocation_gid " +
                   " where c.employee_gid='" + employee_gid +"'" +
                   " group by c.employee_gid " +
                   " order by c.employee_gid desc  ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows ==true)
                {
                    objemployee.company_name = objODBCDatareader["entity_name"].ToString();
                    objemployee.entity_gid = objODBCDatareader["entity_gid"].ToString();
                    objemployee.branch_gid = objODBCDatareader["branch_gid"].ToString();
                    objemployee.branch_name = objODBCDatareader["branch_name"].ToString();
                    objemployee.department_gid = objODBCDatareader["department_gid"].ToString();
                    objemployee.department_name = objODBCDatareader["department_name"].ToString();
                    objemployee.subfunction_gid = objODBCDatareader["subfunction_gid"].ToString();
                    objemployee.subfunction_name = objODBCDatareader["subfunction_name"].ToString();
                    objemployee.designation_gid = objODBCDatareader["designation_gid"].ToString();
                    objemployee.designation_name = objODBCDatareader["designation_name"].ToString();
                    objemployee.useraccess = objODBCDatareader["useraccess"].ToString();
                    objemployee.user_access = objODBCDatareader["user_access"].ToString();
                    objemployee.user_status = objODBCDatareader["user_status"].ToString();
                    objemployee.user_firstname = objODBCDatareader["user_firstname"].ToString();
                    objemployee.user_lastname = objODBCDatareader["user_lastname"].ToString();
                    objemployee.gender = objODBCDatareader["employee_gender"].ToString();
                    objemployee.employee_emailid = objODBCDatareader["employee_emailid"].ToString();
                    objemployee.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                    objemployee.user_code = objODBCDatareader["user_code"].ToString();
                    objemployee.role_gid = objODBCDatareader["role_gid"].ToString();
                    objemployee.role_name = objODBCDatareader["role_name"].ToString();
                    objemployee.employee_reportingto = objODBCDatareader["employeereporting_to"].ToString();
                    objemployee.baselocation_gid = objODBCDatareader["baselocation_gid"].ToString();
                    objemployee.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                    objemployee.marital_status = objODBCDatareader["marital_status"].ToString();
                    objemployee.marital_status_gid = objODBCDatareader["marital_status_gid"].ToString();
                    objemployee.joining_date = objODBCDatareader["joiningdate"].ToString();
                    objemployee.personal_phone_no = objODBCDatareader["personal_phone_no"].ToString();
                    objemployee.personal_emailid = objODBCDatareader["personal_emailid"].ToString();
                    objemployee.bloodgroup_name = objODBCDatareader["bloodgroup_name"].ToString();
                    objemployee.bloodgroup_gid = objODBCDatareader["bloodgroup_gid"].ToString();
                    if (objODBCDatareader["employee_joiningdate"].ToString() != "")
                    {
                        objemployee.joiningdate = Convert.ToDateTime(objODBCDatareader["employee_joiningdate"].ToString());
                    }
                }
                msSQL = "select concat(user_code, ' || ', user_firstname,' ',user_lastname) as employee_reportingto_name" +
                        " from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                        " where employee_gid = '"+ objemployee.employee_reportingto +"'";
                objemployee.employee_reportingto_name = objdbconn.GetExecuteScalar(msSQL);

                string lspermanentaddressGID = string.Empty;
                string lstemporaryaddressGID = string.Empty;

                msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                " hrm_trn_temployeedtl where " +
                " employee_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows==true)
                {
                    lspermanentaddressGID = objODBCDatareader["permanentaddress_gid"].ToString();
                    lstemporaryaddressGID = objODBCDatareader["temporaryaddress_gid"].ToString();
                }
                msSQL = " Select a.address1,a.address2,a.city, " +
                " a.postal_code,a.state,b.country_name,b.country_gid" +
                " from adm_mst_taddress a,adm_mst_tcountry b " +
                " where  address_gid = '" + lspermanentaddressGID + "' and " +
                " a.country_gid = b.country_gid and " +
                " a.parent_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if(objODBCDatareader.HasRows==true)
                {
                    objemployee.per_address1 = objODBCDatareader["address1"].ToString();
                    objemployee.per_address2 = objODBCDatareader["address2"].ToString();
                    objemployee.per_country_gid = objODBCDatareader["country_gid"].ToString();
                    objemployee.per_country_name = objODBCDatareader["country_name"].ToString();
                    objemployee.per_state = objODBCDatareader["state"].ToString();
                    objemployee.per_city = objODBCDatareader["city"].ToString();
                    objemployee.per_postal_code = objODBCDatareader["postal_code"].ToString();
                }

                msSQL = " Select a.address1,a.address2,a.city, " +
                " a.postal_code,a.state,b.country_name,b.country_gid" +
                " from adm_mst_taddress a,adm_mst_tcountry b " +
                " where  address_gid = '" + lstemporaryaddressGID + "' and " +
                " a.country_gid = b.country_gid and " +
                " a.parent_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows==true)
                {
                    objemployee.temp_address1 = objODBCDatareader["address1"].ToString();
                    objemployee.temp_address2 = objODBCDatareader["address2"].ToString();
                    objemployee.temp_country_gid = objODBCDatareader["country_gid"].ToString();
                    objemployee.temp_country_name = objODBCDatareader["country_name"].ToString();
                    objemployee.temp_state = objODBCDatareader["state"].ToString();
                    objemployee.temp_city = objODBCDatareader["city"].ToString();
                    objemployee.temp_postal_code = objODBCDatareader["postal_code"].ToString();
                }
                objemployee.status = true;
                return true;
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
                objemployee.status = false;
                return false;
            }
            objODBCDatareader.Close();

        }

        //Pending View
        public bool DaEmployeePendingEditView(employee objemployee, string employee_gid)
        {

            try
            {
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , user_firstname,user_lastname," +
                   " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ," +
                   " c.employee_gender,c.role_gid,employeereporting_to,  " +
                   " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                   " d.designation_name,c.designation_gid,c.employee_gid,e.branch_name, employee_emailid,employee_mobileno,baselocation_name,s.baselocation_gid,c.entity_gid," +
                   "c.subfunction_gid,x.subfunction_name," +
                   " CASE " +
                   " WHEN a.user_status = 'Y' THEN 'Active'  " +
                   " WHEN a.user_status = 'N' THEN 'Inactive' " +
                   " END as user_access,a.user_status,c.department_gid,c.branch_gid,n.role_name, e.branch_name, g.department_name,c.marital_status, " +
                  " c.marital_status_gid, date_format(c.employee_joiningdate,'%d-%m-%Y') as joiningdate,c.employee_joiningdate as joining_date,c.employee_personalno as personal_phone_no,c.personal_emailid,c.bloodgroup as bloodgroup_name,c.bloodgroup_gid " +
                   " FROM hrm_mst_temployee c " +
                   " left join adm_mst_tuser a on a.user_gid = c.user_gid " + 
                   " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                   " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                   " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                   "left join sys_mst_tsubfunction x on x.subfunction_gid=c.subfunction_gid " +
                   " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                   " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                   " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                   " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid" +
                   " left join hrm_mst_trole n on n.role_gid=c.role_gid " +
                   " left join sys_mst_tbaselocation s on s.baselocation_gid = c.baselocation_gid " +
                   " where c.employee_gid='" + employee_gid + "'" +
                   " group by c.employee_gid " +
                   " order by c.employee_gid desc  ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objemployee.company_name = objODBCDatareader["entity_name"].ToString();
                    objemployee.entity_gid = objODBCDatareader["entity_gid"].ToString();
                    objemployee.branch_gid = objODBCDatareader["branch_gid"].ToString();
                    objemployee.branch_name = objODBCDatareader["branch_name"].ToString();
                    objemployee.department_gid = objODBCDatareader["department_gid"].ToString();
                    objemployee.department_name = objODBCDatareader["department_name"].ToString();
                    objemployee.subfunction_gid = objODBCDatareader["subfunction_gid"].ToString();
                    objemployee.subfunction_name = objODBCDatareader["subfunction_name"].ToString();
                    objemployee.designation_gid = objODBCDatareader["designation_gid"].ToString();
                    objemployee.designation_name = objODBCDatareader["designation_name"].ToString();
                    objemployee.useraccess = objODBCDatareader["useraccess"].ToString();
                    objemployee.user_access = objODBCDatareader["user_access"].ToString();
                    objemployee.user_status = objODBCDatareader["user_status"].ToString();
                    objemployee.user_firstname = objODBCDatareader["user_firstname"].ToString();
                    objemployee.user_lastname = objODBCDatareader["user_lastname"].ToString();
                    objemployee.gender = objODBCDatareader["employee_gender"].ToString();
                    objemployee.employee_emailid = objODBCDatareader["employee_emailid"].ToString();
                    objemployee.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                    objemployee.user_code = objODBCDatareader["user_code"].ToString();
                    objemployee.role_gid = objODBCDatareader["role_gid"].ToString();
                    objemployee.role_name = objODBCDatareader["role_name"].ToString();
                    objemployee.employee_reportingto = objODBCDatareader["employeereporting_to"].ToString();
                    objemployee.baselocation_gid = objODBCDatareader["baselocation_gid"].ToString();
                    objemployee.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                    objemployee.marital_status = objODBCDatareader["marital_status"].ToString();
                    objemployee.marital_status_gid = objODBCDatareader["marital_status_gid"].ToString();
                    objemployee.joining_date = objODBCDatareader["joiningdate"].ToString();
                    if (objODBCDatareader["joining_date"].ToString() != "")
                    {
                        objemployee.joiningdate = Convert.ToDateTime(objODBCDatareader["joining_date"].ToString());
                    }
                    objemployee.personal_phone_no = objODBCDatareader["personal_phone_no"].ToString();
                    objemployee.personal_emailid = objODBCDatareader["personal_emailid"].ToString();
                    objemployee.bloodgroup_name = objODBCDatareader["bloodgroup_name"].ToString();
                    objemployee.bloodgroup_gid = objODBCDatareader["bloodgroup_gid"].ToString();
                }
                msSQL = "select concat(user_code, ' || ', user_firstname,' ',user_lastname) as employee_reportingto_name" +
                        " from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                        " where employee_gid = '" + objemployee.employee_reportingto + "'";
                objemployee.employee_reportingto_name = objdbconn.GetExecuteScalar(msSQL);

                string lspermanentaddressGID = string.Empty;
                string lstemporaryaddressGID = string.Empty;

                msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                " hrm_trn_temployeedtl where " +
                " employee_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lspermanentaddressGID = objODBCDatareader["permanentaddress_gid"].ToString();
                    lstemporaryaddressGID = objODBCDatareader["temporaryaddress_gid"].ToString();
                }
                msSQL = " Select a.address1,a.address2,a.city, " +
                " a.postal_code,a.state,b.country_name,b.country_gid" +
                " from adm_mst_taddress a,adm_mst_tcountry b " +
                " where  address_gid = '" + lspermanentaddressGID + "' and " +
                " a.country_gid = b.country_gid and " +
                " a.parent_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objemployee.per_address1 = objODBCDatareader["address1"].ToString();
                    objemployee.per_address2 = objODBCDatareader["address2"].ToString();
                    objemployee.per_country_gid = objODBCDatareader["country_gid"].ToString();
                    objemployee.per_country_name = objODBCDatareader["country_name"].ToString();
                    objemployee.per_state = objODBCDatareader["state"].ToString();
                    objemployee.per_city = objODBCDatareader["city"].ToString();
                    objemployee.per_postal_code = objODBCDatareader["postal_code"].ToString();
                }

                msSQL = " Select a.address1,a.address2,a.city, " +
                " a.postal_code,a.state,b.country_name,b.country_gid" +
                " from adm_mst_taddress a,adm_mst_tcountry b " +
                " where  address_gid = '" + lstemporaryaddressGID + "' and " +
                " a.country_gid = b.country_gid and " +
                " a.parent_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objemployee.temp_address1 = objODBCDatareader["address1"].ToString();
                    objemployee.temp_address2 = objODBCDatareader["address2"].ToString();
                    objemployee.temp_country_gid = objODBCDatareader["country_gid"].ToString();
                    objemployee.temp_country_name = objODBCDatareader["country_name"].ToString();
                    objemployee.temp_state = objODBCDatareader["state"].ToString();
                    objemployee.temp_city = objODBCDatareader["city"].ToString();
                    objemployee.temp_postal_code = objODBCDatareader["postal_code"].ToString();

                }
                objemployee.status = true;
                return true;
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                objemployee.status = false;
                return false;
            }
            objODBCDatareader.Close();

        }

        // Pending Approval
        public bool DaEmployeePendingApproval(employee objemployee, string employee_gid)
        {
            string lsuser_gid = string.Empty;
            try
            {
                
                    msSQL = "update sys_mst_temployeelog set status_flag ='Y' where employee_gid = '" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update hrm_mst_temployee set employee_status ='A' where employee_gid = '" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select user_gid from hrm_mst_temployee where employee_gid='" + employee_gid + "'";
                lsuser_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "update adm_mst_tuser set" +
                        " user_status='Y'" +
                        " where user_gid='" + lsuser_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

              /*  if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;
                        objFnSamAgroHBAPIConn.PostEmployeeHBAPI(employee_gid);

                    }));

                    t.Start();
                } */
                    objemployee.status = true;
                objemployee.message = "User Approval Initiated Sucessfully";
                return true;
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                objemployee.status = false;
                objemployee.message = "Error while User Approval Initiation";
                return false;
            }

        }
       

        // Reliving Details Added
        public bool DaEmployeeRelievingAdd(employee objemployee, string employee_gid)
        {
            string msGETGID = string.Empty;
            msGETGID = objcmnfunctions.GetMasterGID("EXTE");
            try
            {
                msSQL = "insert into hrm_trn_texitemployee " +
                " (exitemployee_gid, " +
                " employee_gid, " +
                " remarks," +
                " exit_date," +
                " created_date," +
                 " created_by)" +
                " values( " +
                "'" + msGETGID  + "'," +
                "'" + objemployee.employee_gid + "'," +
                "'" + objemployee.remarks.Replace("'", "") + "'," +
                 "'" + Convert.ToDateTime(objemployee.relive_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," + 
                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                " '" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    //string msGETLOGGID = string.Empty;
                    //msGETLOGGID = objcmnfunctions.GetMasterGID("EMLG");
                    //string user_gid = string.Empty;
                    //msSQL = "select user_gid from hrm_mst_temployee where employee_gid='" + objemployee.employee_gid + "'";
                    //user_gid = objdbconn.GetExecuteScalar(msSQL);
                    //string user_name = string.Empty;
                    //msSQL = "select concat(user_firstname,' ',user_lastname,'/',user_code) from adm_mst_tuser where user_gid='" + user_gid + "'";
                    //user_name = objdbconn.GetExecuteScalar(msSQL);
                    //msSQL = "update sys_mst_temployeelog set status_flag = 'N' where employee_gid = '" + objemployee.employee_gid  + "'";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //msSQL = "insert into sys_mst_temployeelog " +
                    //        " (exitemployee_gid, " +
                    //        " employee_gid, " +
                    //        " user_gid, " +
                    //        " user_name, " +
                    //        " employee_status,"  +
                    //        " created_date," +
                    //        " created_by)" +
                    //        " values (" +
                    //        "'" + msGETLOGGID + "'," +
                    //        "'" + objemployee.employee_gid + "'," +
                    //        "'" + user_gid + "'," +
                    //        "'" + user_name + "'," +
                    //        "'Relieving'" +
                    //        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                    //        " '" + employee_gid + "')";

                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update sys_mst_temployeelog set relieved_date ='" + Convert.ToDateTime(objemployee.relive_date).ToString("yyyy-MM-dd HH:mm:ss") + "', relieved_by =  '" + employee_gid + "' where employee_gid = '" + objemployee.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "update hrm_mst_temployee set employee_status = 'R' where employee_gid = '"+ objemployee.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objemployee.status = true;
                    objemployee.message = "Employee Reliving Details Added Successfully";
                    return true;
                }
                else
                {
                    objemployee.status = false;
                    objemployee.message = "Error While Adding Reliving Details";
                    return false;
                }
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objemployee.message = "Error While Adding Reliving Details";
                return false;
            }
        }


        //Reliving View
        public bool DaEmployeeRelievingView(employee values,string employee_gid)
        {

            try
            {
                msSQL = " select a.remarks,date_format(a.exit_date, '%d-%m-%Y') as exit_date, date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                        " a.created_by  from hrm_trn_texitemployee a " +
                        " left join hrm_mst_temployee b on a.employee_gid=b. employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid=b. user_gid" +
                        " where b.employee_gid = '" + employee_gid + "' and b.employee_status = 'R'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.relive_date = objODBCDatareader["exit_date"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                values.status = false;
                return false;
            }

        }

        //User Code Verification
        public bool DaUserCodeCheck(result values, string user_gid)
        {

            try
            {
                msSQL = "select user_gid from adm_mst_tuser where user_code = '" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.message = "User Code Already in Use";
                }
                else
                {
                    values.message = "";
                }

                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                values.status = false;
                return false;
            }

        }
        // Reliving Details Update
        public bool DaEmployeeRelievingEdit(employee objemployee, string employee_gid)
        {
            try
            {
               msSQL = "update hrm_trn_texitemployee  set" +
                           " remarks='" + objemployee.remarks.Replace("'", "") + "'," +
                           " exit_date='" + Convert.ToDateTime(objemployee.relive_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           " updated_by='" + employee_gid + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                           " where employee_gid='" + objemployee.employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {

                    objemployee.status = true;
                    objemployee.message = "Employee Reliving Details Updated Successfully";
                    return true;
                }
                else
                {
                    objemployee.status = false;
                    objemployee.message = "Error While Updating Reliving Details";
                    return false;
                }
            }
            catch (Exception ex)
            {
                objemployee.status = false;
                objemployee.message = "Error While Updating Reliving Details";
                return false;
            }
        }
       
        public bool DaPopRole(role_list objrolemaster)
        {
            try
            {
                msSQL = "select role_gid,role_name from hrm_mst_trole order by role_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_role = new List<rolemaster>();
               
                 if(dt_datatable.Rows.Count !=0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_role.Add(new rolemaster
                        {
                            role_gid = dr_datarow["role_gid"].ToString(),
                            role_name = dr_datarow["role_name"].ToString()

                        });
                    }
                    objrolemaster.rolemaster = get_role;
                    objrolemaster.status = true;
                    return true;
                }
                else
                {
                    objrolemaster.status = false;
                    return false;
                }
                
            }
            catch
            {
                objrolemaster.status = false;
                return false;
            }
        }

        //Updated change 
        public bool DaPopReportingTo(reportingto_list objreportingto)
        {
          
            string lsemployee_gid = "";
            string lsmodulereportingto_gid = "";
            try
            {
                msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany)";
                lsmodulereportingto_gid=objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select employee_gid from adm_mst_tmodule2employee where module_gid ='" + lsmodulereportingto_gid +"' group by employee_gid"; 
               objODBCDatareader=objdbconn.GetDataReader(msSQL);
                while (objODBCDatareader.Read())
                {
                    lsemployee_gid = lsemployee_gid   + "'" + objODBCDatareader["employee_gid"].ToString() + "',";
                }
                lsemployee_gid = lsemployee_gid.TrimEnd(',');

                msSQL = " select a.employee_gid,concat(user_code, ' || ', user_firstname, ' ', user_lastname) as employee_name " +
                   " from hrm_mst_temployee a " +
                   " left join adm_mst_tuser b on a.user_gid = b.user_gid  " +
                   " where a.employee_gid in(" + lsemployee_gid + ") and b.user_status = 'Y' and a.employee_status = 'A'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var get_reportingto = new List<reportingto>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_reportingto.Add(new reportingto
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            employee_name = dr_datarow["employee_name"].ToString()
                        });
                    }
                    objreportingto.reportingto = get_reportingto;
                    objreportingto.status = true;
                    return true;
                }
                else
                {
                    objreportingto.status = false;
                    return false;
                }
            }
            catch
            {
                objreportingto.status = false;
                return false;
            }
        }

        //public bool DaPopReportingTo(reportingto_list objreportingto)
        //{
        //    try
        //    {
        //        msSQL = " select employee_gid,concat(user_code, ' || ', user_firstname, ' ', user_lastname) as employee_name " +
        //           " from hrm_mst_temployee a " +
        //           " left join adm_mst_tuser b on a.user_gid = b.user_gid";
        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        var get_reportingto = new List<reportingto>();
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
        //            {
        //                get_reportingto.Add(new reportingto
        //                {
        //                    employee_gid = dr_datarow["employee_gid"].ToString(),
        //                    employee_name = dr_datarow["employee_name"].ToString()
        //                });
        //            }
        //            objreportingto.reportingto = get_reportingto;
        //            objreportingto.status = true;
        //            return true;
        //        }
        //        else
        //        {
        //            objreportingto.status = false;
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        objreportingto.status = false;
        //        return false;
        //    }
        //}


        public bool DaEmployeeDeactivate(string employee_gid,string employee_gid1,string exit_date,employee objemployee)
        {
        
            string lsupdated_by = string.Empty;
            string lsuser_gid = string.Empty;
            
            try
            {
                
                msSQL = "select user_gid from hrm_mst_temployee where employee_gid='" + employee_gid +"'";
                lsuser_gid = objdbconn.GetExecuteScalar(msSQL);
                var lsstr = exit_date.Split();
                var lsexit_date = Convert.ToDateTime(lsstr[3] + "-" + lsstr[1] + "-" + lsstr[2]).ToString("yyyy-MM-dd");
                //lsexit_date = Convert.ToDateTime(exit_date);
                lsupdated_by = employee_gid1;
                msSQL = "update adm_mst_tuser set" +
                        " user_status='N'," +
                        " updated_by='" + lsupdated_by +"',"+
                        " updated_date='"+ DateTime.Now.ToString("yyyy-MM-dd") +"'" + 
                        " where user_gid='" + lsuser_gid +"'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult==1)
                {
                    //msSQL = "update hrm_mst_temployee set" +
                    //       " exit_date='" + lsexit_date + "'," +
                    //       " updated_by='" + lsupdated_by + "'," +
                    //       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                    //       " where user_gid='" + employee_gid  + "'";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update hrm_mst_temployee set employee_status ='I' ,"+
                            " exit_date='" + lsexit_date + "'," +
                            " updated_by='" + lsupdated_by + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                            " where employee_gid = '" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objemployee.status = true;
                    objemployee.message = "Employee has been Deactivated Successfully";
                    return true;
               }
                else
                {
                    objemployee.status = false;
                    objemployee.message = "Error while Deactivating Employee";
                    return false;
                }
            }
            catch
            {
                objemployee.status = false;
                objemployee.message = "Error while Deactivating Employee";
                return false;

            }
        }
        public bool DaEmployeeActivate(string employee_gid,string employee_gid1,employee objemployee)
        {
            string lsuser_gid = string.Empty;
            try
            {
                msSQL = "select user_gid from hrm_mst_temployee where employee_gid='" + employee_gid + "'";
                lsuser_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "update adm_mst_tuser set" +
                        " user_status='Y'," +
                        " updated_by ='" + employee_gid1 + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") +"'"+
                        " where user_gid='" + lsuser_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " update hrm_mst_temployee set exit_date=null where user_gid='" + lsuser_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                  

                    objemployee.status = true;
                    objemployee.message = "Employee Has been Activated Successfully";
                    return true;
                }
                else
                {
                    objemployee.status = false; ;
                    objemployee.message = "Error while Activating Employee";
                    return false; 
                }
                
            }
            catch
            {
                objemployee.status = false; ;
                objemployee.message = "Error while Activating Employee";
                return false;
            }
        }
        public bool DaEmployeeUpdate(employee objemployee,string employee_gid)
        {
            string type = "Employee";
            string user_gid = string.Empty;
            try
            {
                msSQL = "select user_gid  from hrm_mst_temployee where employee_gid='" + objemployee.employee_gid +"'";
                user_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " update adm_mst_tuser set " +
                " user_code = '" + objemployee.user_code + "', " +
                " user_firstname = '" + objemployee.user_firstname + "', " +
                " user_lastname = '" + objemployee.user_lastname + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                " where user_gid = '" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 0)
                {
                    objemployee.message = "Error Occured While Updating Records";
                    objemployee.status = false;
                    return false;
                }
                else
                {
                    msSQL = " select a.employee_emailid,d.baselocation_name,a.employee_mobileno,b.user_firstname,b.user_lastname,b.user_code, employeereporting_to, case when a.entity_gid is null then c.entity_name else c.entity_name end as entity_name, a.department_gid " +
                            " from hrm_mst_temployee a" +
                            " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                            " left join adm_mst_tentity c on c.entity_gid=a.entity_gid" +
                            " left join sys_mst_tbaselocation d on d.baselocation_gid = a.baselocation_gid " +
                            " where a.employee_gid='" + objemployee.employee_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {

                        lsentity_name = objODBCDatareader["entity_name"].ToString();
                        lsemployee_emailid = objODBCDatareader["employee_emailid"].ToString();
                        lsemployee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                        lsuser_firstname = objODBCDatareader["user_firstname"].ToString();
                        lsuser_lastname = objODBCDatareader["user_lastname"].ToString();
                        lsuser_code = objODBCDatareader["user_code"].ToString();
                        lsreporting_to = objODBCDatareader["employeereporting_to"].ToString();
                        lsbase_location = objODBCDatareader["baselocation_name"].ToString();
                        lsdepartment_gid = objODBCDatareader["department_gid"].ToString();

                    }
                    objODBCDatareader.Close();
                    msSQL = "select concat(user_firstname,' ', user_lastname) as employeereporting_to" +
                            " from hrm_mst_temployee a" +
                            " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                            " where a.employee_gid='" + lsreporting_to + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsreporting_toname = objODBCDatareader["employeereporting_to"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " Update hrm_mst_temployee set  " +
                     " branch_gid = '" + objemployee.branch_gid + "'," +
                     " baselocation_gid = '" + objemployee.baselocation_gid + "'," +
                     " designation_gid = '" + objemployee.designation_gid + "'," +
                     " subfunction_gid ='"+ objemployee.subfunction_gid +"'," +
                     " employee_mobileno ='" + objemployee.employee_mobileno + "'," +
                     " employeereporting_to ='" + objemployee.employee_reportingto +"'," + 
                     " role_gid ='" + objemployee.role_gid + "'," +
                     " employee_emailid = '" + objemployee.employee_emailid + "'," +
                     " employee_gender = '" + objemployee.gender + "'," +
                     " department_gid = '" + objemployee.department_gid + "' ," +
                     " marital_status = '" + objemployee.marital_status + "'," +
                     " bloodgroup = '" + objemployee.bloodgroup_name + "'," +
                     " employee_joiningdate = '" + Convert.ToDateTime(objemployee.joining_date).ToString("yyyy-MM-dd") + "'," +
                     " employee_personalno = '" + objemployee.personal_phone_no.Replace("'", "") + "'," +
                     " personal_emailid = '" + objemployee.personal_emailid.Replace("'", "") + "'," +
                     " marital_status_gid = '" + objemployee.marital_status_gid + "'," +
                     " bloodgroup_gid = '" + objemployee.bloodgroup_gid + "'," +
                     " updated_by = '" + employee_gid + "'," +
                     " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                     " update_flag='N' " +
                     " where employee_gid = '" + objemployee.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult ==1)
                    {
                        msSQL = " update hrm_trn_temployeetypedtl set " +
                           " wagestype_gid='wg001', " +
                           " systemtype_gid='Actual', " +
                           " branch_gid = '" + objemployee.branch_gid + "'," +
                           " department_gid='" + objemployee.department_gid + "', " +
                           " designation_gid='" + objemployee.designation_gid + "', " +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                           " updated_by='" + employee_gid + "'" +
                           " where employee_gid = '" + objemployee.employee_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult==1)
                        {
                            string lsPerAddress_gid = string.Empty;
                            string lsTempAddress_gid = string.Empty;
                            msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                            " hrm_trn_temployeedtl where " +
                            " employee_gid = '" + objemployee.employee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsPerAddress_gid = objODBCDatareader["permanentaddress_gid"].ToString();
                                lsTempAddress_gid = objODBCDatareader["temporaryaddress_gid"].ToString();
                            }
                            msSQL = " update adm_mst_taddress SET " +
                            " country_gid = '" + objemployee.per_country_gid + "', " +
                            " address1 =  '" + objemployee.per_address1 + "', " +
                            " address2 = '" + objemployee.per_address2 + "'," +
                            " city = '" + objemployee.per_city + "', " +
                            " postal_code = '" + objemployee.per_postal_code + "'," +
                            " state = '" + objemployee.per_state + "' " +
                            " where address_gid = '" + lsPerAddress_gid + "' and " +
                            " parent_gid = '" + objemployee.employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update adm_mst_taddress SET " +
                           " country_gid = '" + objemployee.temp_country_gid + "', " +
                           " address1 =  '" + objemployee.temp_address1 + "', " +
                           " address2 = '" + objemployee.temp_address2 + "'," +
                           " city = '" + objemployee.temp_city + "', " +
                           " postal_code = '" + objemployee.temp_postal_code + "'," +
                           " state = '" + objemployee.temp_state + "' " +
                           " where address_gid = '" + lsTempAddress_gid + "' and " +
                           " parent_gid = '" + objemployee.employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany)";
                            lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                             msSQL = "select hierarchy_level from adm_mst_tmodule2employee a where" +
                            " a.module_gid='" + lsmodulereportingto_gid + "' and a.employee_gid='" + objemployee.employee_reportingto + "'";
                            lshierarchy_level = objdbconn.GetExecuteScalar(msSQL);
                            hierarchy_level = Convert.ToInt16(lshierarchy_level);
                            hierarchy_level += 1;
                            lshierarchy_level = Convert.ToString(hierarchy_level);
                            msSQL = "update adm_mst_tmodule2employee set " +
                                    " employeereporting_to='"+ objemployee.employee_reportingto + "',"+
                                    " hierarchy_level = '"+ lshierarchy_level +"' " +
                                    " where employee_gid='"+ objemployee.employee_gid +"' and module_gid='"+ lsmodulereportingto_gid +"'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                        else
                        {
                            objemployee.message = "Error while updating Employee Details - 1";
                            objemployee.status = false;
                            return false;
                        }
                    }
                    else
                    {
                        objemployee.message = "Error while updating Employee Details - 2";
                        objemployee.status = false;
                        return false;
                    }

                    msSQL = "select baselocation_name from sys_mst_tbaselocation where baselocation_gid = '" + objemployee.baselocation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_baselocation = objODBCDatareader["baselocation_name"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = "select erp_id from hrm_mst_temployee where employee_gid = '" + objemployee.employee_reportingto + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsrptngto_erpid = objODBCDatareader["erp_id"].ToString();
                    }
                    objODBCDatareader.Close();


                   /*if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                    {
                        HttpContext ctx = HttpContext.Current;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            HttpContext.Current = ctx;

                            UpdateHBAPIEmployeeRequest updateHBAPIEmployeeRequest = new UpdateHBAPIEmployeeRequest();

                            List<string> existingStringList = new List<string> { lsentity_name, lsemployee_emailid, lsemployee_mobileno, lsuser_firstname, lsuser_lastname, lsuser_code, lsreporting_to, lsbase_location, lsdepartment_gid };
                            List<string> updatedStringList = new List<string> { objemployee.company_name, objemployee.employee_emailid, objemployee.employee_mobileno, objemployee.user_firstname, objemployee.user_lastname, objemployee.user_code, objemployee.employee_reportingto, objemployee.baselocation_name, objemployee.department_gid };

                            if (!(existingStringList.SequenceEqual(updatedStringList)))
                            {
                                updateHBAPIEmployeeRequest.entity_name = objemployee.company_name;
                                updateHBAPIEmployeeRequest.employee_emailid = objemployee.employee_emailid;
                                updateHBAPIEmployeeRequest.employee_mobileno = objemployee.employee_mobileno;
                                updateHBAPIEmployeeRequest.user_firstname = objemployee.user_firstname;
                                updateHBAPIEmployeeRequest.user_lastname = objemployee.user_lastname;
                                updateHBAPIEmployeeRequest.user_code = objemployee.user_code;
                                updateHBAPIEmployeeRequest.employeereporting_to = lsrptngto_erpid;
                                updateHBAPIEmployeeRequest.employee_gid = objemployee.employee_gid;
                                updateHBAPIEmployeeRequest.baselocation = ls_baselocation;
                                updateHBAPIEmployeeRequest.department_gid = objemployee.department_gid;
                                objFnSamAgroHBAPIConn.UpdateEmployeeHBAPI(updateHBAPIEmployeeRequest);
                            }

                        }));

                        t.Start();
                    }*/

                        
                    objemployee.message = "Employee Details has been Updated Successfullly";
                    objemployee.status = true;
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                objemployee.message = "Exception Occured while updating Employee Details";
                objemployee.status = false;
                //LogForAuditNetsuiteERPDT("Exception" + ex.ToString(), "System");
                return false;
                
            }
            objODBCDatareader.Close();
        }

        public bool DaEmployeePendingUpdate(employee objemployee, string employee_gid)
        {
            string user_gid = string.Empty;
            try
            {
                msSQL = "select user_gid  from hrm_mst_temployee where employee_gid='" + objemployee.employee_gid + "'";
                user_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " update adm_mst_tuser set " +
                " user_code = '" + objemployee.user_code + "', " +
                " user_firstname = '" + objemployee.user_firstname + "', " +
                " user_lastname = '" + objemployee.user_lastname + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                " where user_gid = '" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 0)
                {
                    objemployee.message = "Error Occured While Updating Records";
                    objemployee.status = false;
                    return false;
                }
                else
                {
                    msSQL = " Update hrm_mst_temployee set  " +
                     " branch_gid = '" + objemployee.branch_gid + "'," +
                     " baselocation_gid = '" + objemployee.baselocation_gid + "'," +
                     " designation_gid = '" + objemployee.designation_gid + "'," +
                     " subfunction_gid ='" + objemployee.subfunction_gid +"'," +
                     " employee_mobileno ='" + objemployee.employee_mobileno + "'," +
                     " employeereporting_to ='" + objemployee.employee_reportingto + "'," +
                     " role_gid ='" + objemployee.role_gid + "'," +
                     " employee_emailid = '" + objemployee.employee_emailid + "'," +
                     " employee_gender = '" + objemployee.gender + "'," +
                     " department_gid = '" + objemployee.department_gid + "' ," +
                     " marital_status = '" + objemployee.marital_status + "'," +
                     " bloodgroup = '" + objemployee.bloodgroup_name + "'," +
                     " employee_joiningdate = '" + Convert.ToDateTime(objemployee.joining_date).ToString("yyyy-MM-dd") + "'," +
                     " employee_personalno = '" + objemployee.personal_phone_no.Replace("'", "") + "'," +
                     " personal_emailid = '" + objemployee.personal_emailid.Replace("'", "") + "'," +
                     " marital_status_gid = '" + objemployee.marital_status_gid + "'," +
                     " bloodgroup_gid = '" + objemployee.bloodgroup_gid + "'," +
                     " updated_by = '" + employee_gid + "'," +
                     " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                     " update_flag='N' " +
                     " where employee_gid = '" + objemployee.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        msSQL = " update hrm_trn_temployeetypedtl set " +
                           " wagestype_gid='wg001', " +
                           " systemtype_gid='Actual', " +
                           " branch_gid = '" + objemployee.branch_gid + "'," +
                           " department_gid='" + objemployee.department_gid + "', " +
                           " designation_gid='" + objemployee.designation_gid + "', " +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                           " updated_by='" + employee_gid + "'" +
                           " where employee_gid = '" + objemployee.employee_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            string lsPerAddress_gid = string.Empty;
                            string lsTempAddress_gid = string.Empty;
                            msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                            " hrm_trn_temployeedtl where " +
                            " employee_gid = '" + objemployee.employee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsPerAddress_gid = objODBCDatareader["permanentaddress_gid"].ToString();
                                lsTempAddress_gid = objODBCDatareader["temporaryaddress_gid"].ToString();
                            }
                            msSQL = " update adm_mst_taddress SET " +
                            " country_gid = '" + objemployee.per_country_gid + "', " +
                            " address1 =  '" + objemployee.per_address1 + "', " +
                            " address2 = '" + objemployee.per_address2 + "'," +
                            " city = '" + objemployee.per_city + "', " +
                            " postal_code = '" + objemployee.per_postal_code + "'," +
                            " state = '" + objemployee.per_state + "' " +
                            " where address_gid = '" + lsPerAddress_gid + "' and " +
                            " parent_gid = '" + objemployee.employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update adm_mst_taddress SET " +
                           " country_gid = '" + objemployee.temp_country_gid + "', " +
                           " address1 =  '" + objemployee.temp_address1 + "', " +
                           " address2 = '" + objemployee.temp_address2 + "'," +
                           " city = '" + objemployee.temp_city + "', " +
                           " postal_code = '" + objemployee.temp_postal_code + "'," +
                           " state = '" + objemployee.temp_state + "' " +
                           " where address_gid = '" + lsTempAddress_gid + "' and " +
                           " parent_gid = '" + objemployee.employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany)";
                            lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                             msSQL = "select hierarchy_level from adm_mst_tmodule2employee a where " +
                            " a.module_gid='" + lsmodulereportingto_gid + "' and a.employee_gid='" + objemployee.employee_reportingto + "'";
                            lshierarchy_level = objdbconn.GetExecuteScalar(msSQL);
                            hierarchy_level = Convert.ToInt16(lshierarchy_level);
                            hierarchy_level += 1;
                            lshierarchy_level = Convert.ToString(hierarchy_level);
                            msSQL = "update adm_mst_tmodule2employee set " +
                                    " employeereporting_to='" + objemployee.employee_reportingto + "'," +
                                    " hierarchy_level = '" + lshierarchy_level + "' " +
                                    " where employee_gid='" + objemployee.employee_gid + "' and module_gid='" + lsmodulereportingto_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                        else
                        {
                            objemployee.message = "Error while updating Employee Details";
                            objemployee.status = false;
                            return false;
                        }
                    }
                    else
                    {
                        objemployee.message = "Error while updating Employee Details";
                        objemployee.status = false;
                        return false;
                    }



                    objemployee.message = "Employee Details has been Updated Successfullly";
                    objemployee.status = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                objemployee.message = "Error while updating Employee Details";
                objemployee.status = false;
                return false;

            }
        }
        public bool DaPopCountry(country_list objcountry_list)
        {
            try
            {
                msSQL = " Select country_gid,country_code,country_name From adm_mst_tcountry ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_country = new List<country>();
                if(dt_datatable.Rows.Count!=0)
                {
                    foreach(DataRow dr_row in dt_datatable.Rows)
                    {
                        get_country.Add(new country
                        {
                            country_gid = dr_row["country_gid"].ToString(),
                            country_name = dr_row["country_name"].ToString()
                        });
                    }
                    objcountry_list.country = get_country;
                    objcountry_list.status = true;
                    return true;
                }
                else
                {
                    objcountry_list.status = false;
                    return false;
                }
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
                return false;
            }
        }
        public bool DaPopBranch(employee_list objemployee_list)
        {
            try
            {
                msSQL = "select branch_gid,branch_name from hrm_mst_tbranch order by branch_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_branch_list = new List<employee>();
                if(dt_datatable.Rows.Count!=0)
                {
                    foreach(DataRow dr_row in dt_datatable.Rows)
                    {
                        get_branch_list.Add(new employee
                        {
                            branch_gid = dr_row["branch_gid"].ToString(),
                            branch_name = dr_row["branch_name"].ToString()
                        });
                    }
                    objemployee_list.employee = get_branch_list;
                    objemployee_list.status = true;
                    return true;
                }
                else
                {
                    objemployee_list.status = false;
                    return false;
                }
            }
            catch
            {
                objemployee_list.status = false;
                return false;
            }
        }
        public bool DaPopDepartment(employee_list objemployee_list)
        {
            try
            {
                msSQL = "select department_gid,department_name from hrm_mst_tdepartment order by department_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_department_list = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_department_list.Add(new employee
                        {
                            department_gid = dr_row["department_gid"].ToString(),
                            department_name = dr_row["department_name"].ToString()
                        });
                    }
                    objemployee_list.employee = get_department_list;
                    objemployee_list.status = true;
                    return true;
                }
                else
                {
                    objemployee_list.status = false;
                    return false;
                }
            }
            catch
            {
                objemployee_list.status = false;
                return false;
            }
        }
        public bool DaPopSubfunction(employee_list objemployee_list)
        {
            try
            {
                msSQL = "select subfunction_gid,subfunction_name from sys_mst_tsubfunction where status='Y' and delete_flag='N'; ";
                dt_datatable=objdbconn.GetDataTable(msSQL);
                var get_subfunction_list = new List<employee>();
                if(dt_datatable.Rows.Count !=0)
                {
                    foreach(DataRow dr_row in dt_datatable.Rows)
                    {
                        get_subfunction_list.Add(new employee
                        {
                            subfunction_gid = dr_row["subfunction_gid"].ToString(),
                            subfunction_name = dr_row["subfunction_name"].ToString()
                        });
                    }
                    objemployee_list.employee = get_subfunction_list;
                    objemployee_list.status = true;
                    return true;
                }
                
                else
                {
                    objemployee_list.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objemployee_list.status = false;
                return false;
            }
            
        }
        public bool DaPopDesignation(employee_list objemployee_list)
        {
            try
            {
                msSQL = "select designation_gid,designation_name from adm_mst_tdesignation order by designation_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_designation_list = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_designation_list.Add(new employee
                        {
                            designation_gid = dr_row["designation_gid"].ToString(),
                            designation_name = dr_row["designation_name"].ToString()
                        });
                    }
                    objemployee_list.employee = get_designation_list;
                    objemployee_list.status = true;
                    return true;
                }
                else
                {
                    objemployee_list.status = false;
                    return false;
                }
            }
            catch
            {
                objemployee_list.status = false;
                return false;
            }
        }
        public bool DaPopRoleEdit(role_list objrolemaster,string employee_gid)
        {
            try
            {
                string lsrole_gid = string.Empty;
                msSQL="select role_gid from hrm_mst_temployee where employee_gid ='" + employee_gid +"'";
                lsrole_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select role_gid,role_name from hrm_mst_trole where role_gid<>'" + lsrole_gid  +"' order by role_name ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_role = new List<rolemaster>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_role.Add(new rolemaster
                        {
                            role_gid = dr_datarow["role_gid"].ToString(),
                            role_name = dr_datarow["role_name"].ToString()

                        });
                    }
                    objrolemaster.rolemaster = get_role;
                    objrolemaster.status = true;
                    return true;
                }
                else
                {
                    objrolemaster.status = false;
                    return false;
                }

            }
            catch
            {
                objrolemaster.status = false;
                return false;
            }
        }
        public bool DaPopReportingToEdit(reportingto_list objreportingto,string employee_gid)
        {
            try
            {
                msSQL = " select employee_gid,concat(user_code, ' || ', user_firstname, ' ', user_lastname) as employee_name " +
                   " from hrm_mst_temployee a " +
                   " left join adm_mst_tuser b on a.user_gid = b.user_gid where a.employee_gid='"+ employee_gid +"'" ;
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_reportingto = new List<reportingto>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_reportingto.Add(new reportingto
                        {
                            employee_gid = dr_datarow["employee_gid"].ToString(),
                            employee_name = dr_datarow["employee_name"].ToString()
                        });
                    }
                    objreportingto.reportingto = get_reportingto;
                    objreportingto.status = true;
                    return true;
                }
                else
                {
                    objreportingto.status = false;
                    return false;
                }
            }
            catch
            {
                objreportingto.status = false;
                return false;
            }
        }
        public bool DaEntity(employee objemployee)
        {
            
            try
            {
                msSQL = "select entity_flag from adm_mst_tcompany where 1=1";
                lsentity_flag = objdbconn.GetExecuteScalar(msSQL);
                objemployee.entity_flag = lsentity_flag;
                if (lsentity_flag =="N")
                {
                    msSQL = "SELECT company_name from adm_mst_tcompany where 1=1";
                    objemployee.entity_name = objdbconn.GetExecuteScalar(msSQL);
                    return true;
                }
                else
                {
                    return false;
                }
                    
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
                return false;
            }
        }
        public bool DaPopEntity(entity_list objentity_list)
        {
            try
            {
                msSQL = "select entity_gid,entity_name from adm_mst_tentity where 1=1 ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_entity = new List<entity>();
                if (dt_datatable.Rows.Count!=0)
                {
                    foreach(DataRow dr_row in dt_datatable.Rows)
                    {
                        get_entity.Add(new entity
                        {
                            entity_gid = dr_row["entity_gid"].ToString(),
                            entity_name = dr_row["entity_name"].ToString()
                        });
                    }
                    objentity_list.entity = get_entity;
                    objentity_list.status = true;
                    return true;
                }
                else
                { 
                    objentity_list.status = false;
                    return false;
                }
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
                return false;
            }
        }

        public bool DaPopEntityActive(entity_list objentity_list)
        {
            try
            {
                msSQL = "select entity_gid,entity_name from adm_mst_tentity where status='Y' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_entity = new List<entity>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_entity.Add(new entity
                        {
                            entity_gid = dr_row["entity_gid"].ToString(),
                            entity_name = dr_row["entity_name"].ToString()
                        });
                    }
                    objentity_list.entity = get_entity;
                    objentity_list.status = true;
                    return true;
                }
                else
                {
                    objentity_list.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                return false;
            }
        }


        public bool DaGetResetPswdEdit(employee values, string employee_gid)
        {

            msSQL = "select a.user_code,concat(a.user_firstname, ' ', a.user_lastname) as user_name, b.user_gid,b.entity_gid from hrm_mst_temployee b" +
                   " left join adm_mst_tuser a on a.user_gid = b.user_gid " +
                    " WHERE employee_gid ='" + employee_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                values.user_code = objODBCDatareader["user_code"].ToString();
                values.employee_name = objODBCDatareader["user_name"].ToString();
                values.user_gid = objODBCDatareader["user_gid"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }

        public bool DaPostPasswordUpdate(employee values, string user_gid)
        {
            msSQL = "UPDATE adm_mst_tuser set user_password = '" + objcmnfunctions.ConvertToAscii(values.user_password) + "'," +
                     "updated_by = '" + user_gid + "',"+
                     "updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     "where user_gid = '" + values.user_gid + "'";                
              
            mnResult5 = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult5 == 1)
            {
                values.message = "Password Updated Successfully..!!";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occurred While Updating Password..!!";
                values.status = false;
                return false;
            }
        }

        public void DaPostUserCodeUpdate(employee values, string user_gid)
        {
            msSQL = "select entity_gid, user_code from hrm_mst_temployee where user_gid ='" + values.user_gid + "' ";
            lsentity_gid = objdbconn.GetExecuteScalar(msSQL);
            lsuser_code = objdbconn.GetExecuteScalar(msSQL);
            if (lsentity_gid != values.entity_gid)
            {
                msSQL = " update hrm_mst_temployee set entity_gid = '" + values.entity_gid + "'" +
                        " where user_gid = '" + values.user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    //HttpContext ctx = HttpContext.Current;

                    //Thread t = new Thread(new ThreadStart(() =>
                    //{
                    //    HttpContext.Current = ctx;
                    //    objFnSamAgroHBAPIConn.nsEmpEntityUpdate(values.user_gid, user_gid, lsentity_gid, lsuser_code);
                    //}));

                    //t.Start();

                    values.message = "Entity Update successful.";
                    values.status = true;
                }
                else
                {
                    values.message = "Entity Update Failed.";
                    values.status = false;
                }
            }
            if (values.user_code == null || values.user_code == "")
            {
                return;
            }
            else
            {
                msSQL = "select user_gid from adm_mst_tuser where user_code ='" + values.user_code + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    msSQL = " update adm_mst_tuser a left join hrm_mst_temployee b on a.user_gid = b.user_gid set a.user_code='" + values.user_code + "', a.updated_by= '" + user_gid + "', a.updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where a.user_gid = '" + values.user_gid + "'";
                    mnResult5 = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult5 == 1)
                    {
                        values.message = values.message + "Employee Code Updated Successfully..!!";
                        values.status = true;
                    }
                    else
                    {
                        values.message = values.message + "Error Occurred While Updating the Employee Code";
                        values.status = false;
                    }
                }
                else
                {
                    values.message = values.message + "Employee Code Already Exist..!!";
                }
            }
        }


        public void DaEmployeeExport(export values)
        {
            msSQL = " Select distinct case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , " +
                   " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,date_format(c.employee_joiningdate,'%d-%m-%Y %h:%i %p') as employee_joiningdate," +
                   " c.employee_gender,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                   " concat(w.user_firstname,' ',w.user_lastname,' / ',w.user_code) as created_by, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, concat(y.user_firstname,' ',y.user_lastname,' / ',y.user_code) as updated_by, " +
                   " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                   " d.designation_name,c.employee_emailid,e.branch_name,s.baselocation_name,concat(v.user_firstname,' ',v.user_lastname) as employeereporting_to, " +
                   " CASE " +
                   " WHEN a.user_status = 'Y' THEN 'Active'  " +
                   " WHEN a.user_status = 'N' THEN 'Inactive' " +
                   " END as user_status, e.branch_name, g.department_name " +
                   " FROM adm_mst_tuser a " +
                   " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                   " left join hrm_mst_temployee p on c.employeereporting_to = p.employee_gid " +
                   " left join adm_mst_tuser v on p.user_gid = v.user_gid " +
                   " left join hrm_mst_temployee u on u.employee_gid = a.created_by " +
                   " left join adm_mst_tuser w on w.user_gid = u.user_gid " +
                   " left join hrm_mst_temployee x on x.employee_gid = a.updated_by " +
                   " left join adm_mst_tuser y on y.user_gid = x.user_gid " +
                   " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                   " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                   " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                   " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                   " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                   " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                   " left join sys_mst_tbaselocation s on s.baselocation_gid=c.baselocation_gid" +
                   " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid " +
                   " group by c.employee_gid " +
                   " order by c.employee_gid desc  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Employee List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Employee Report.xlsx";
                //var path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "System/Employee Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "System/Employee Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/Employee Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/Employee Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 18])  //Address "A1:A14"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                values.lspath =lscompany_code + "/" + "System/Employee Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                status = objcmnstorage.UploadStream("erpdocument", values.lspath, ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }

        // Initiate Task

        public void DaPostTask(string employee_gid, string user_gid, tasklist values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("TKIN");
            msSQL = " insert into sys_mst_ttaskinitiate(" +
                    " taskinitiate_gid," +
                    " employee_gid," +
                    //" user_gid," +
                    " task_gid," +
                    " task_name," +
                    " task_remarks," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    "'" + user_gid + "'," +
                    //"'" + user_gid + "'," +
                    "'" + values.task_gid + "'," +
                    "'" + values.task_name.Replace("'", "") + "',";
            if (values.task_remarks == null || values.task_remarks == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.task_remarks.Replace("'", "") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Task Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Task";
            }
        }

        // Task dropdown

        public void DaGetTaskList(string employee_gid, string user_gid, MdlTaskList values)
        {
            msSQL = " SELECT task_gid,task_name FROM sys_mst_ttask a" +
                    " where status='Y' and delete_flag='N' and task_gid not in (select task_gid from sys_mst_ttaskinitiate where employee_gid ='" + user_gid + "' or employee_gid ='" + employee_gid + "') order by a.task_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettask_list = new List<tasklists>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettask_list.Add(new tasklists
                    {
                        task_gid = (dr_datarow["task_gid"].ToString()),
                        task_name = (dr_datarow["task_name"].ToString()),
                    });
                }
                values.tasklists = gettask_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        // Task list

        public void DaGetTaskSummary(string employee_gid, string user_gid, MdlTaskList values)
        {
            msSQL = " SELECT a.employee_gid,a.complete_flag,a.completed_date,a.task_completeremarks, a.task_gid,a.task_name,a.task_remarks , a.taskinitiate_gid,a.taskinitiate_flag," +
            " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
            " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as completed_by, " +
            " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date FROM sys_mst_ttaskinitiate a " +
            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
            " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            " left join hrm_mst_temployee h on a.completed_by = h.employee_gid " +
            " left join adm_mst_tuser j on h.user_gid = j.user_gid " +
            " where ( a.employee_gid ='" + employee_gid + "' ) order by a.taskinitiate_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettask_list = new List<tasksummarylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettask_list.Add(new tasksummarylist
                    {
                        task_gid = (dr_datarow["task_gid"].ToString()),
                        task_name = (dr_datarow["task_name"].ToString()),
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        task_remarks = (dr_datarow["task_remarks"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        taskinitiate_gid = (dr_datarow["taskinitiate_gid"].ToString()),
                        taskinitiate_flag = (dr_datarow["taskinitiate_flag"].ToString()),
                        complete_flag = (dr_datarow["complete_flag"].ToString()),
                        completed_date = (dr_datarow["completed_date"].ToString()),
                        completed_by = (dr_datarow["completed_by"].ToString()),
                        task_completeremarks = (dr_datarow["task_completeremarks"].ToString()),
                    });
                }
                values.tasksummarylist = gettask_list;
            }
        }


        public void DaGetTempTaskList(string employee_gid, string user_gid, MdlTaskList values)
        {
            msSQL = " SELECT a.employee_gid,a.complete_flag,a.completed_date,a.task_completeremarks, a.task_gid,a.task_name,a.task_remarks , a.taskinitiate_gid,a.taskinitiate_flag," +
            " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
            " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as completed_by, " +
            " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date FROM sys_mst_ttaskinitiate a " +
            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
            " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            " left join hrm_mst_temployee h on a.completed_by = h.employee_gid " +
            " left join adm_mst_tuser j on h.user_gid = j.user_gid " +
            " where a.employee_gid ='" + employee_gid + "'or a.employee_gid ='" + user_gid + "' order by a.taskinitiate_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettask_list = new List<tasksummarylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettask_list.Add(new tasksummarylist
                    {
                        task_gid = (dr_datarow["task_gid"].ToString()),
                        task_name = (dr_datarow["task_name"].ToString()),
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        task_remarks = (dr_datarow["task_remarks"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        taskinitiate_gid = (dr_datarow["taskinitiate_gid"].ToString()),
                        taskinitiate_flag = (dr_datarow["taskinitiate_flag"].ToString()),
                        complete_flag = (dr_datarow["complete_flag"].ToString()),
                        completed_date = (dr_datarow["completed_date"].ToString()),
                        completed_by = (dr_datarow["completed_by"].ToString()),
                        task_completeremarks = (dr_datarow["task_completeremarks"].ToString()),
                    });
                }
                values.tasksummarylist = gettask_list;
            }
        }

        //Overall submit

        public void DaPostOverallTask(string employee_gid, string user_gid, tasklist values)
        {
            msSQL = " update sys_mst_ttaskinitiate set employee_gid ='" + values.employee_gid + "'," +
                    " overallinitiate_flag = 'Y'  " +
                    " where employee_gid='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = " Task Initiated Sucessfully";

                msSQL = "update hrm_mst_temployee set employee_status ='Initiated' where employee_gid = '" + values.employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                try
                {
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

                    msSQL = " SELECT a.employee_gid,a.complete_flag,a.completed_date,a.created_by,a.task_gid,a.task_name,a.task_remarks , g.assignedto_gid, a.taskinitiate_gid,a.taskinitiate_flag," +
                     //" concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                     " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as assignedto_name," +
                     " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as completed_by, " +
                     " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date FROM sys_mst_ttaskinitiate a " +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      " left join hrm_mst_temployee h on a.completed_by = h.employee_gid " +
                     " left join adm_mst_tuser j on h.user_gid = j.user_gid  " +
                     " left join sys_mst_ttask2assignedto g on a.task_gid = g.task_gid   " +
                     " left join hrm_mst_temployee i on g.assignedto_gid = i.employee_gid " +
                     " left join adm_mst_tuser f on i.user_gid = f.user_gid  " +
                     " where ( a.employee_gid ='" + values.employee_gid + "' ) and taskinitiate_flag = 'Y' and ccmail_flag = 'N' order by a.taskinitiate_gid desc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var gettask_list = new List<tasksummarylist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            lsemployee_name = dr_datarow["assignedto_name"].ToString();
                            lsemployee_gid = dr_datarow["assignedto_gid"].ToString();
                            lscreated_by = dr_datarow["created_by"].ToString();
                            lstask_gid = dr_datarow["task_gid"].ToString();

                            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                            sToken = "";
                            int Length = 100;
                            for (int j = 0; j < Length; j++)
                            {
                                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                sToken += sTempChars;
                            }

                            msGetGid = objcmnfunctions.GetMasterGID("TSMI");

                            msSQL = "Insert into sys_trn_ttaskmailinitiate( " +
                                   " taskmailinitiate_gid, " +
                                   " taskinitiate_gid, " +
                                   " employee_gid, " +
                                   " taskassignedmember_gid," +
                                   " taskassignedmember_name," +
                                   " approval_token," +
                                   //" requestapproval_remarks," +
                                   " taskmailinitiate_flag," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + dr_datarow["taskinitiate_gid"].ToString() + "'," +
                                   "'" + dr_datarow["employee_gid"].ToString() + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsemployee_name + "'," +
                                   "'" + sToken + "'," +
                                   //"'" + values.approval_remarks.Replace("'", "\\'") + "'," +
                                   "'Y'," +
                                   "'" + user_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            k = k + 1;


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsemployee_gid.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscreated_by.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                            msSQL = " select distinct a.task_name,a.taskinitiate_gid,date_format(a.completed_date, '%d-%m-%Y %h:%i %p') as completed_date,a.complete_flag,a.task_remarks,concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as created_by, " +
                      " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as completed_by, " +
                      " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as employee_name, p.employee_gid" +
                      " from sys_mst_ttaskinitiate a " +
                      " left join hrm_mst_temployee e on a.created_by = e.employee_gid " +
                      " left join adm_mst_tuser f on e.user_gid = f.user_gid  " +
                      " left join hrm_mst_temployee h on a.completed_by = h.employee_gid " +
                      " left join adm_mst_tuser j on h.user_gid = j.user_gid  " +
                      " left join hrm_mst_temployee p on a.employee_gid = p.employee_gid " +
                      " left join adm_mst_tuser q on p.user_gid = q.user_gid " +
                      " left join sys_mst_ttask2assignedto g on a.task_gid = g.task_gid " +
                      " where  a.taskinitiate_gid = '" + dr_datarow["taskinitiate_gid"].ToString() + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lstask_name = objODBCDatareader["task_name"].ToString();
                                lstask_remarks = objODBCDatareader["task_remarks"].ToString();
                                lstaskassignedby = objODBCDatareader["created_by"].ToString();
                                lscreated_date = objODBCDatareader["created_date"].ToString();
                                lsnewemployee_name = objODBCDatareader["employee_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            sub = " Task Information ";
                            body = "Dear Sir/Madam,  <br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(lstaskassignedby) + " has been Initiated the Employee Onboarding Task and the details are as follows,<br />";
                            body = body + "<br />";
                            body = body + "<b> Employee Name :</b> " + HttpUtility.HtmlEncode(lsnewemployee_name) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Task Name :</b> " + HttpUtility.HtmlEncode(lstask_name) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Task Initiated On :</b> " + lscreated_date + "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems and complete the necessary actions.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + " Hr ";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            message.To.Add(new MailAddress(lsto_mail));

                            lsBccmail_id = ConfigurationManager.AppSettings["CCMeetingbcc"].ToString();

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
                                msSQL = "Insert into sys_mst_ttaskmailcount( " +
                                " employee_gid," +
                                " from_mail," +
                                " to_mail," +
                                //" cc_mail," +
                                " mail_status," +
                                " mail_senddate, " +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + values.employee_gid + "'," +
                                "'" + ls_username + "'," +
                                "'" + lsto_mail + "'," +
                                //"'" + lscc_mail + "'," +
                                "'Task Initiated'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update sys_mst_ttaskinitiate set ccmail_flag = 'Y' " +
                                        " where taskinitiate_gid = '" + dr_datarow["taskinitiate_gid"].ToString() + "' and taskinitiate_flag = 'Y' and ccmail_flag = 'N' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }

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
                values.message = "Error While Adding Task";
            }

        }

        //Task Initiate

        public void DaInitiateTask(string employee_gid, string user_gid, tasklist values)
        {
            msSQL = "update sys_mst_ttaskinitiate set taskinitiate_flag ='Y'," +
                    " task_status = 'Initiated', " +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " updated_by='" + employee_gid + "'" +
                    " where ( employee_gid='" + values.employee_gid + "' or employee_gid='" + user_gid + "') and taskinitiate_gid = '" + values.taskinitiate_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = " Task Initiated Sucessfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Task";
            }
        }

        //Delete Task Initate

        public void DaDeleteTaskInitiated(string taskinitiate_gid, MdlTaskList values, string employee_gid)
        {
            msSQL = "delete from sys_mst_ttaskinitiate where taskinitiate_gid='" + taskinitiate_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Task details Deleted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Task";
            }
        }

        // Temp clear

        public void DaDeleteTaskInitiatedUnsaved(MdlTaskList values, string user_gid)
        {
            msSQL = " delete from sys_mst_ttaskinitiate where employee_gid='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Task details Deleted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Task";
            }
        }

        public void DaGetTaskInitiate(string taskinitiate_gid, string employee_gid, string user_gid, tasksummarylist values)
        {
            msSQL = " select task_name, task_gid from sys_mst_ttaskinitiate a" +
                    "  where (employee_gid='" + employee_gid + "' or employee_gid='" + user_gid + "') and  taskinitiate_gid = '" + taskinitiate_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.task_name = objODBCDatareader["task_name"].ToString();
                values.task_gid = objODBCDatareader["task_gid"].ToString();

            }
            objODBCDatareader.Close();

        }

        // Cancel Task Initiate

        public void DaCancelTaskInitiate(string taskinitiate_gid, tasklist values, string employee_gid, string user_gid)
        {
            msSQL = " update sys_mst_ttaskinitiate set taskinitiate_flag='N' ," +
                    " task_status = null , ccmail_flag = 'N' " +
                    " where (employee_gid='" + employee_gid + "' or employee_gid='" + user_gid + "') and  taskinitiate_gid = '" + taskinitiate_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Task Initiate Cancelled Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!, While Cancel";
            }
        }

        // Overall submit icon disable flag

        public void DaGetEmployeeSubmit(string employee_gid, string user_gid, MdlTaskList values)
        {
            msSQL = " SELECT task_status,taskinitiate_flag,overallinitiate_flag from sys_mst_ttaskinitiate a " +
                    " where (a.employee_gid = '" + employee_gid + "'or employee_gid='" + user_gid + "') and taskinitiate_flag = 'Y' and ccmail_flag = 'N' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.initiate_flag = "Y";
            }
            else
            {
                objODBCDatareader.Close();
                values.initiate_flag = "N";
            }
        }
        // My Onboarding Pending Summary
        public void DaGetMyTaskPendingSummary(string employee_gid, string user_gid, MdlTaskList values)
        {
            msSQL = " select  a.task_name,a.taskinitiate_gid,a.task_remarks,concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as created_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as employee_name, p.employee_gid" +
                    " from sys_mst_ttaskinitiate a " +
                    " left join hrm_mst_temployee e on a.created_by = e.employee_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid  " +
                    " left join hrm_mst_temployee p on a.employee_gid = p.employee_gid " +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid " +
                    " left join sys_mst_ttask2assignedto g on a.task_gid = g.task_gid " +
                    " where g.assignedto_gid = '" + employee_gid + "' and a.taskinitiate_flag = 'Y' and complete_flag = 'N' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettask_list = new List<tasksummarylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettask_list.Add(new tasksummarylist
                    {

                        task_name = (dr_datarow["task_name"].ToString()),
                        employee_name = (dr_datarow["employee_name"].ToString()),
                        task_remarks = (dr_datarow["task_remarks"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        taskinitiate_gid = (dr_datarow["taskinitiate_gid"].ToString()),
                        //api_code = (dr_datarow["api_code"].ToString()),

                    });
                }
                values.tasksummarylist = gettask_list;
            }
        }
        // My Onboarding Complete Summary
        public void DaGetMyTaskCompleteSummary(string employee_gid, string user_gid, MdlTaskList values)
        {
            msSQL = " select a.task_name,a.taskinitiate_gid,a.complete_flag,a.task_completeremarks,a.task_remarks,concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as created_by, " +
            " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as completed_by, " +
            " date_format(a.completed_date, '%d-%m-%Y %h:%i %p') as completed_date ," +
            " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as employee_name, p.employee_gid" +
            " from sys_mst_ttaskinitiate a " +
            " left join hrm_mst_temployee e on a.created_by = e.employee_gid " +
            " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
            " left join hrm_mst_temployee h on a.completed_by = h.employee_gid " +
            " left join adm_mst_tuser j on h.user_gid = j.user_gid " +
            " left join hrm_mst_temployee p on a.employee_gid = p.employee_gid " +
            " left join adm_mst_tuser q on p.user_gid = q.user_gid " +
            " left join sys_mst_ttask2assignedto g on a.task_gid = g.task_gid " +
            " where g.assignedto_gid = '" + employee_gid + "' and a.taskinitiate_flag = 'Y' and a.complete_flag = 'Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettask_list = new List<tasksummarylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettask_list.Add(new tasksummarylist
                    {



                        task_name = (dr_datarow["task_name"].ToString()),
                        employee_name = (dr_datarow["employee_name"].ToString()),
                        task_remarks = (dr_datarow["task_remarks"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        taskinitiate_gid = (dr_datarow["taskinitiate_gid"].ToString()),
                        completed_date = (dr_datarow["completed_date"].ToString()),
                        complete_flag = (dr_datarow["complete_flag"].ToString()),
                        completed_by = (dr_datarow["completed_by"].ToString()),
                        task_completeremarks = (dr_datarow["task_completeremarks"].ToString()),
                    });
                }
                values.tasksummarylist = gettask_list;
            }
        }

        // Complete task

        public void DaCompleteTask(string employee_gid, tasklist values)
        {
            msSQL = " update sys_mst_ttaskinitiate set complete_flag='Y',task_status = 'Completed', task_completeremarks ='" + values.task_completeremarks.Replace("'", "") + "', " +
            " completed_by='" + employee_gid + "'," +
            " completed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where taskinitiate_gid = '" + values.taskinitiate_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Task Completed Successfully..!";

                msSQL = " select taskinitiate_gid from sys_mst_ttaskinitiate where employee_gid='" + values.employee_gid + "' " +
                        " and taskinitiate_flag='Y' and complete_flag='N'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    string lsuser_gid = string.Empty;
                    msSQL = "update sys_mst_temployeelog set status_flag ='Y' where employee_gid = '" + values.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update hrm_mst_temployee set employee_status ='A' where employee_gid = '" + values.employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                   /* if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                    {
                        HttpContext ctx = HttpContext.Current;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            HttpContext.Current = ctx;
                            objFnSamAgroHBAPIConn.PostEmployeeHBAPI(values.employee_gid);
                        }));

                        t.Start();
                    } */

                    msSQL = "select user_gid from hrm_mst_temployee where employee_gid='" + values.employee_gid + "'";
                    lsuser_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "update adm_mst_tuser set user_status='Y'" +
                            " where user_gid='" + lsuser_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                objODBCDatareader.Close();
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        // Flag for Complete button

        public void DaGetCompleteflag(string taskinitiate_gid, string employee_gid, string user_gid, MdlTaskList values)
        {
            msSQL = " select a.employee_gid, a.complete_flag" +
            " from sys_mst_ttaskinitiate a " +
            " where a.employee_gid = '" + employee_gid + "' and a.taskinitiate_flag = 'Y' and a.taskinitiate_gid = '" + taskinitiate_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.complete_flag = objODBCDatareader["complete_flag"].ToString();
            }
            objODBCDatareader.Close();
        }

        // My Onboarding count

        public void DaGetMyTashCount(string employee_gid, countlist values)
        {
            msSQL = " select count(*) as pending_count from sys_mst_ttaskinitiate a " +
            " left join sys_mst_ttask2assignedto g on a.task_gid = g.task_gid " +
            " where a.task_status='Initiated' and a.overallinitiate_flag = 'Y' and g.assignedto_gid = '" + employee_gid + "'";
            values.pending_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as pending_count from sys_mst_ttaskinitiate a " +
            " left join sys_mst_ttask2assignedto g on a.task_gid = g.task_gid " +
            " where a.task_status='Completed' and g.assignedto_gid = '" + employee_gid + "'";
            values.completed_count = objdbconn.GetExecuteScalar(msSQL);
        }

        //Get task details

        public void DaGetTaskDetails(string taskinitiate_gid, string employee_gid, string user_gid, tasksummarylist values)
        {
            msSQL = " select a.task_name,a.taskinitiate_gid,a.task_completeremarks,date_format(a.completed_date, '%d-%m-%Y %h:%i %p') as completed_date,a.complete_flag,a.task_remarks,concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as created_by, " +
            " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as completed_by, " +
            " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as employee_name, p.employee_gid" +
            " from sys_mst_ttaskinitiate a " +
            " left join hrm_mst_temployee e on a.created_by = e.employee_gid " +
            " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
            " left join hrm_mst_temployee h on a.completed_by = h.employee_gid " +
            " left join adm_mst_tuser j on h.user_gid = j.user_gid " +
            " left join hrm_mst_temployee p on a.employee_gid = p.employee_gid " +
            " left join adm_mst_tuser q on p.user_gid = q.user_gid " +
            " left join sys_mst_ttask2assignedto g on a.task_gid = g.task_gid " +
            " where g.assignedto_gid = '" + employee_gid + "' and a.taskinitiate_flag = 'Y' and a.taskinitiate_gid = '" + taskinitiate_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.task_name = objODBCDatareader["task_name"].ToString();
                values.task_remarks = objODBCDatareader["task_remarks"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.completed_by = objODBCDatareader["completed_by"].ToString();
                values.completed_date = objODBCDatareader["completed_date"].ToString();
                values.complete_flag = objODBCDatareader["complete_flag"].ToString();
                values.task_completeremarks = objODBCDatareader["task_completeremarks"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetDeactivationCondition(string deactivateemployee_gid, string employee_gid, MdlDeactivation values)
        {
            msSQL = " select status from ams_trn_tasset2custodian  " + 
                    " where employee_gid= '" + deactivateemployee_gid + "' and" + 
                    " status in ('Acknowledged','Surrender Pending','Temporary Handover Pending','Temporary Handover')";
            values.asset_status = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select status from ams_trn_tasset2custodian  " +
                    " where temporaryhandover_gid='" + deactivateemployee_gid + "' and" +
                    " status in ('Temporary Handover Surrender Pending','Temporary Handover-holding','Temporary Handover')";
            values.tempasset_status = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select b.employeereporting_to from hrm_mst_temployee a" +
                         " inner join adm_mst_tmodule2employee b on a.employee_gid=b.employee_gid" +
                         " inner join adm_mst_tmodule c on b.module_gid=c.module_gid " +
                         " where b.employeereporting_to='" + deactivateemployee_gid + "'";
            values.employeereporting_to = objdbconn.GetExecuteScalar(msSQL);
            if (values.employeereporting_to != null || values.employeereporting_to != "")
            {
                msSQL = " Select GROUP_CONCAT(distinct c.module_name SEPARATOR ', ') from hrm_mst_temployee a " +
                        " inner join adm_mst_tmodule2employee b on a.employee_gid=b.employeereporting_to" +
                        " inner join adm_mst_tmodule c on b.module_gid=c.module_gid " +
                        " where b.hierarchy_level <> '-1' and a.employee_gid='" + deactivateemployee_gid + "' and b.visible='T' " +
                        " order by c.menu_level desc ";
                values.module_name = objdbconn.GetExecuteScalar(msSQL);
            }

            //Samfin
            msSQL = " select application_gid from ocs_mst_tapplication  " +
                    " where relationshipmanager_gid = '" + deactivateemployee_gid + "' and  approval_status not  in ('Incomplete', 'CC Approved' ,'CC Rejected','CC Meeting Skip'," +
                    "'Rejected By Business','Rejected By Credit','Rejected by Credit Manager')";
            values.application_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select applicationapproval_gid from ocs_trn_tapplicationapproval " +                    
                       " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
                values.applicationapproval_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " Select appcreditapproval_gid from ocs_trn_tappcreditapproval " +
                     " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
                values.appcreditapproval_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " Select ccmeeting2members_gid from ocs_mst_tccmeeting2members " +
                     " where ccmember_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
                values.ccmeeting2members_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " Select cadgroupmanager_gid from ocs_mst_tcadgroupmanager " +
                     " where employee_gid='" + deactivateemployee_gid + "'";
                values.cadgroupmanager_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " Select cadgroupmembers_gid from ocs_mst_tcadgroupmembers " +
                  " where employee_gid='" + deactivateemployee_gid + "'";
                values.cadgroupmembers_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " Select creditops2maker_gid from ocs_mst_tcreditops2maker " +
                " where employee_gid='" + deactivateemployee_gid + "'";
                values.creditops2maker_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " Select creditops2checker_gid from ocs_mst_tcreditops2checker " +
                " where employee_gid='" + deactivateemployee_gid + "'";
                values.creditops2checker_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select credit2credithead_gid from ocs_mst_tcredit2credithead a " +
                        " LEFT JOIN ocs_mst_tcredit2nationalmanager b ON a.creditmapping_gid=b.creditmapping_gid " +
                        " LEFT JOIN ocs_mst_tcreditr2regionalmanager c ON a.creditmapping_gid=c.creditmapping_gid " +
                        " LEFT JOIN ocs_mst_tcredit2creditmanager d ON a.creditmapping_gid=d.creditmapping_gid " +
                        " where (a.employee_gid = '" + deactivateemployee_gid + "' or b.employee_gid = '" + deactivateemployee_gid + "' " +
                        " or c.employee_gid = '" + deactivateemployee_gid + "' or d.employee_gid = '" + deactivateemployee_gid + "') group by a.creditmapping_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {

                    values.creditmapping_gid = "True";
                }
                else
                {
                    values.creditmapping_gid = "False";
                }


                msSQL = "  SELECT processtypeassign_gid FROM ocs_trn_tprocesstype_assign " +
                        "  WHERE (maker_gid ='" + deactivateemployee_gid + "'  AND maker_approvalflag = 'N') " +
                        "  || (checker_gid = '" + deactivateemployee_gid + "'  AND checker_approvalflag = 'N') " +
                        "  || (approver_gid ='" + deactivateemployee_gid + "'  AND approver_approvalflag = 'N') ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {

                    values.processtype_assign = "True";
                }
                else
                {
                    values.processtype_assign = "False";
                }
            //if (values.processtype_assign == "True" || values.creditmapping_gid == "True" || values.creditops2checker_gid != "" ||
            //    values.creditops2maker_gid != "" || values.cadgroupmembers_gid != "" || values.cadgroupmanager_gid != "" ||
            //    values.ccmeeting2members_gid != "" || values.appcreditapproval_gid != "" || values.applicationapproval_gid != "" || 
            //    values.module_name != "" || values.tempasset_status != "" || values.asset_status != "") {
            //    values.submitbutton = "True";
            //}
            //else {
            //    values.submitbutton = "False";
            //}

            msSQL = " Select ccmembermaster_gid from ocs_mst_tccmember " +
                     " where ccmember_gid='" + deactivateemployee_gid + "'";
            values.ccmembermaster_gid = objdbconn.GetExecuteScalar(msSQL);

            //Samagro
            msSQL = " select application_gid from agr_mst_tapplication " +
" where relationshipmanager_gid = '" + deactivateemployee_gid + "' and  approval_status not  in ('Incomplete', 'CC Approved' ,'CC Rejected','CC Meeting Skip','Rejected By Business','Rejected By Credit')";
            values.agrapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select applicationapproval_gid from agr_trn_tapplicationapproval " +
                       " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
            values.agrapplicationapproval_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select appcreditapproval_gid from agr_trn_tappcreditapproval " +
                 " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
            values.agrappcreditapproval_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select ccmeeting2members_gid from agr_mst_tccmeeting2members " +
                 " where ccmember_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
            values.agrccmeeting2members_gid = objdbconn.GetExecuteScalar(msSQL);
          
            msSQL = "  SELECT processtypeassign_gid FROM agr_trn_tprocesstype_assign " +
                      "  WHERE (maker_gid ='" + deactivateemployee_gid + "'  AND maker_approvalflag = 'N') " +
                      "  || (checker_gid = '" + deactivateemployee_gid + "'  AND checker_approvalflag = 'N') " +
                      "  || (approver_gid ='" + deactivateemployee_gid + "'  AND approver_approvalflag = 'N') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                values.agrprocesstype_assign = "True";
            }
            else
            {
                values.agrprocesstype_assign = "False";
            }

            msSQL = " select credit2credithead_gid from  agr_mst_tcredit2credithead a " +
                      " LEFT JOIN  agr_mst_tcredit2nationalmanager b ON a.creditmapping_gid=b.creditmapping_gid " +
                      " LEFT JOIN  agr_mst_tcreditr2regionalmanager c ON a.creditmapping_gid=c.creditmapping_gid " +
                      " LEFT JOIN  agr_mst_tcredit2creditmanager d ON a.creditmapping_gid=d.creditmapping_gid " +
                      " where (a.employee_gid = '" + deactivateemployee_gid + "' or b.employee_gid = '" + deactivateemployee_gid + "' " +
                      " or c.employee_gid = '" + deactivateemployee_gid + "' or d.employee_gid = '" + deactivateemployee_gid + "') group by a.creditmapping_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                values.agrcreditmapping_gid = "True";
            }
            else
            {
                values.agrcreditmapping_gid = "False";
            }

            msSQL = " Select productdeskmanager_gid from agr_mst_tproductdeskmapping_manager " +
                     " where employee_gid='" + deactivateemployee_gid + "'";
            values.productdeskmanager_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select productdeskmember_gid from agr_mst_tproductdeskmapping_member " +
                " where employee_gid='" + deactivateemployee_gid + "'";
            values.productdeskmember_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select mstpmgapproval_gid from agr_mst_pmgapproval " +
                " where pmgapproval_gid='" + deactivateemployee_gid + "'";
            values.mstpmgapproval_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select mstproductapproval_gid from agr_mst_productapproval " +
                " where productapproval_gid='" + deactivateemployee_gid + "'";
            values.mstproductapproval_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select appproductapproval_gid from agr_trn_tappproductapproval " +
                  " where (product_managergid='" + deactivateemployee_gid + "' and productmanager_approvalflag = 'N' ) or " +
                   " (product_membergid='" + deactivateemployee_gid + "'and productmember_approvalflag = 'N' )";
            values.appproductapproval_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select warehouse2approval_gid from agr_trn_twarehouse2approval " +
                  " where (approval_gid='" + deactivateemployee_gid + "' and productapproval_flag = 'N'  and mstproductapproval_gid is not null ) or " +
                   " (approval_gid='" + deactivateemployee_gid + "'and pmgapproval_flag = 'N' and pmgapproval_flag is not null)";
            values.warehouse2approval_gid = objdbconn.GetExecuteScalar(msSQL);

            //Audit

            msSQL = " Select auditmapping2employee_gid from atm_mst_tauditmapping2employee  " +
                     " where employee_gid='" + deactivateemployee_gid + "'";
            values.auditmapping2employee_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select a.multipleauditee_gid from atm_trn_tmultipleauditee a" +
                    "  left  join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid " +
                      " where a.auditeechecker_gid='" + deactivateemployee_gid + "' and  b.approval_status ='Checker - Auditee Pending'";
            values.multipleauditee_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select auditcreation_gid from atm_trn_tauditcreation " +
                       " where (auditmapping_gid = '" + deactivateemployee_gid + "' and (approval_status='Audit Approval pending' or approval_status='Checker Approval pending')) or " +
                       " (employee_gid = '" + deactivateemployee_gid + "'  and approval_status='Approved') " +
                       " or (auditmapping2employee_gid = '" + deactivateemployee_gid + "' and approval_status='Final Approval Pending') group by auditcreation_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                values.auditcreation_gid = "True";
            }
            else
            {
                values.auditcreation_gid = "False";
            }


            msSQL = " Select applicationapproval_gid from agr_trn_tapplicationapproval " +
                    //" LEFT JOIN ocs_mst_tcredit2nationalmanager b ON a.creditmapping_gid=b.creditmapping_gid " +
                    " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
            values.agrapplicationapproval_gid = objdbconn.GetExecuteScalar(msSQL);


            // Foundation
            msSQL = " Select customerapproving_gid from fnd_mst_tcustomerapproving " +
                     " where approver_gid='" + deactivateemployee_gid + "'";
            values.customerapproving_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select campaign_gid from fnd_trn_tcampaign " +
                      " where campaign_approver='" + deactivateemployee_gid + "' and  (campaign_status ='Pending')";
            values.campaign_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select campaign_gid from fnd_trn_tcampaign " +
                       " where campaign_approver='" + deactivateemployee_gid + "' and  (campaign_status ='Pending for Final Approval' )";
            values.finalcampaign_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select campaignapproving2employee_gid from  fnd_mst_tcampaignapproving2employee a " +
                    " left join fnd_trn_tcampaign b on a.campaign_gid=b.campaign_gid " +
                    " where a.employee_gid = '" + deactivateemployee_gid + "' and b.campaign_status not in ('Approved','Rejected')";
            values.campaignapproving2employee_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select customerapproving_gid from  fnd_mst_tcustomerapproving a " +
                    " left join fnd_mst_tcustomer b on a.customer_gid=b.customer_gid " +
                    " where a.approver_gid = '" + deactivateemployee_gid + "'  and b.status_remarks not in ('Approved','Rejected')";
            values.appcustomerapproving_gid = objdbconn.GetExecuteScalar(msSQL);

            // Business Developement
            msSQL = " select marketingcall_gid from mar_trn_tmarketingcall " +
                         " where (assignemployee_gid = '" + deactivateemployee_gid + "' and (callclosure_status='Pending-Acknowledge' or callclosure_status='Work-In-Progress')) or " +
                         " (assign_by = '" + deactivateemployee_gid + "'  and callclosure_status='Converted') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                values.marketingcall_gid = "True";
            }
            else
            {
                values.marketingcall_gid = "False";
            }

            //Inbound

            msSQL = " select inboundcall_gid from ocs_trn_tinboundcall " +
                  " where (assignemployee_gid = '" + deactivateemployee_gid + "' and (callclosure_status= 'Assign' or callclosure_status= 'Work-In-Progress' or callclosure_status='Completed')) or " +
                  " (assign_by = '" + deactivateemployee_gid + "'  and callclosure_status='Closed') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                values.inboundcall_gid = "True";
            }
            else
            {
                values.inboundcall_gid = "False";
            }

            //Sa_onboarding

            //Institution
            msSQL = " Select sacontactinstitution_gid from ocs_mst_tsainstitution a " +
                    " where institutionmaker_gid='" + deactivateemployee_gid + "' and  approvalstatus ='BD Verification Completed'";
            values.makersacontactinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select sacontactinstitution_gid from ocs_mst_tsainstitution a " +
                    " where institutionchecker_gid='" + deactivateemployee_gid + "' and  approvalstatus ='Maker Verified'";
            values.checkersacontactinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select sacontactinstitution_gid from ocs_mst_tsainstitution a " +
                    " where rm_tagging='" + deactivateemployee_gid + "' and  approvalstatus in ('Checker Verified' ,'Pending BD Verification','Maker Verified','BD Verification Completed') ";
            values.finalsacontactinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

            //Individual
          
            msSQL = " Select sacontact_gid from ocs_mst_tsacontact a " +
                    " where individualmaker_gid='" + deactivateemployee_gid + "' and  approvalstatus ='BD Verification Completed'";
            values.makersacontact_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select sacontact_gid from ocs_mst_tsacontact a " +
                    " where individualchecker_gid='" + deactivateemployee_gid + "' and  approvalstatus ='Maker Verified'";
            values.checkersacontact_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select sacontact_gid from ocs_mst_tsacontact a " +
                    " where rm_tagging='" + deactivateemployee_gid + "' and  approvalstatus in ('Checker Verified' ,'Pending BD Verification','Maker Verified','BD Verification Completed')";
            values.finalsacontact_gid = objdbconn.GetExecuteScalar(msSQL);



            //msSQL = " select sacontactinstitution_gid from ocs_mst_tsainstitution " +
            //       " where (rm_tagging = '" + deactivateemployee_gid + "' and approvalstatus = 'Pending BD Verification') or " +
            //       " (institutionmaker_gid = '" + deactivateemployee_gid + "' and approvalstatus = 'BD Verification Completed') or " +
            //       " (institutionmaker_gid = '" + deactivateemployee_gid + "' and approvalstatus='Maker Verified') or " +
            //       " (rm_tagging = '" + deactivateemployee_gid + "' and approvalstatus='Checker Verified')";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //if (dt_datatable.Rows.Count != 0)
            //{

            //    values.sacontactinstitution_gid = "True";
            //}
            //else
            //{
            //    values.sacontactinstitution_gid = "False";
            //}

            //msSQL = " select sacontact_gid from ocs_mst_tsacontact " +
            //       " where (rm_tagging = '" + deactivateemployee_gid + "' and approvalstatus = 'Pending BD Verification') or " +
            //       " (individualmaker_gid = '" + deactivateemployee_gid + "' and approvalstatus = 'BD Verification Completed') or " +
            //       " (individualmaker_gid = '" + deactivateemployee_gid + "' and approvalstatus='Maker Verified') or " +
            //       " (rm_tagging = '" + deactivateemployee_gid + "' and approvalstatus='Checker Verified')";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //if (dt_datatable.Rows.Count != 0)
            //{

            //    values.sacontact_gid = "True";
            //}
            //else
            //{
            //    values.sacontact_gid = "False";
            //}

            // Service Request

            msSQL = " Select activedepartment2manager_gid from osd_mst_tactivedepartment2manager " +
                     " where (manager_gid='" + deactivateemployee_gid + "')";
            values.activedepartment2manager_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select activedepartment2member_gid from osd_mst_tactivedepartment2member " +
                    " where (member_gid='" + deactivateemployee_gid + "')";
            values.activedepartment2member_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select supportteam2member_gid from osd_mst_tsupportteam2member " +
              " where member_gid='" + deactivateemployee_gid + "'";
            values.supportteam2member_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select requestapproval_gid from osd_trn_trequestapproval " +
                      " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
            values.requestapproval_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select servicerequest_gid from osd_trn_tservicerequest where assigned_membergid = '" + deactivateemployee_gid + "' and " +
                    " request_status in('Allotted' , 'Work-In-Progress')";
            values.servicerequest_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select a.email_gid from osd_trn_tmaildetails a" +
                    "  left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " where b.employee_gid='" + deactivateemployee_gid + "' and  b.status = 'Assigned' and a.status in ('Assigned', 'Transfer')";
            values.email_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " Select maildetails_gid from osd_trn_tmaildetails " +
            //     " where updated_by='" + deactivateemployee_gid + "' and  status='Pending'";
            //values.maildetails_gid = objdbconn.GetExecuteScalar(msSQL);


            //Submit

            if (values.processtype_assign == "True" || values.creditmapping_gid == "True" || values.creditops2checker_gid != "" ||
                values.creditops2maker_gid != "" || values.cadgroupmembers_gid != "" || values.cadgroupmanager_gid != "" ||
                values.ccmeeting2members_gid != "" || values.appcreditapproval_gid != "" || values.applicationapproval_gid != "" ||
                values.module_name != "" || values.tempasset_status != "" || values.asset_status != "" || values.application_gid != "" ||
                values.ccmembermaster_gid != "" || values.agrapplication_gid != "" || values.agrapplicationapproval_gid != "" || 
                values.agrappcreditapproval_gid != "" || values.agrccmeeting2members_gid != "" || values.agrprocesstype_assign == "True" || 
                values.agrcreditmapping_gid == "True" || values.productdeskmanager_gid != "" || values.productdeskmember_gid != "" || 
                values.mstpmgapproval_gid != "" || values.mstproductapproval_gid != "" || values.appproductapproval_gid != "" || 
                values.warehouse2approval_gid != "" || values.auditmapping2employee_gid != "" || values.multipleauditee_gid != "" || 
                values.auditcreation_gid == "True" || values.agrapplicationapproval_gid != "" || values.customerapproving_gid != "" || 
                values.campaign_gid != "" || values.finalcampaign_gid != "" || values.campaignapproving2employee_gid != "" || 
                values.appcustomerapproving_gid != "" || values.marketingcall_gid == "True" || values.inboundcall_gid == "True" || 
                values.sacontactinstitution_gid == "True" || values.sacontact_gid == "True" || values.activedepartment2manager_gid != "" || 
                values.supportteam2member_gid != "" || values.requestapproval_gid != "" || values.servicerequest_gid != "" || values.email_gid != "" || 
                 values.makersacontactinstitution_gid != "" ||
                values.checkersacontactinstitution_gid != "" || values.finalsacontactinstitution_gid != "" || values.makersacontact_gid != "" || values.checkersacontact_gid != "" ||
                values.finalsacontact_gid != "" || values.activedepartment2member_gid != "" )
            {
                values.submitbutton = "True";
            }
            else
            {
                values.submitbutton = "False";
            }

        }

        public bool DaGetHRDoclist(string employee_gid, hrdoc_list objemployeedoc_list)
        {
            try
            {
                msSQL = " select hrdoc_id,hrdocument_gid,hrdocument_name,hrdoc_name, " +
                        " hrdoc_path,documentsentforsign_flag,esignexpiry_flag,documentsigned_flag, " +
                        " concat(c.user_firstname,' ', c.user_lastname,'/' ,c.user_code) as created_by," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, migration_flag " +
                        " from sys_mst_temployeehrdocument a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.employee_gid = '" + employee_gid+"'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_hrdoc_list = new List<hrdoc>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_hrdoc_list.Add(new hrdoc_list
                        {
                            hrdoc_id = dr_datarow["hrdoc_id"].ToString(),
                            hrdocument_gid = dr_datarow["hrdocument_gid"].ToString(),
                            hrdocument_name = dr_datarow["hrdocument_name"].ToString(),
                            hrdoc_name = dr_datarow["hrdoc_name"].ToString(),
                            hrdoc_path = objcmnstorage.EncryptData((dr_datarow["hrdoc_path"].ToString())),                          
                            documentsentforsign_flag = dr_datarow["documentsentforsign_flag"].ToString(),
                            esignexpiry_flag = dr_datarow["esignexpiry_flag"].ToString(),
                            documentsigned_flag = dr_datarow["documentsigned_flag"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            migration_flag = dr_datarow["migration_flag"].ToString(),
                        });
                    }
                    objemployeedoc_list.hrdoc = get_hrdoc_list;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public void DaPostHRDocumentUpload(HttpRequest httpRequest, HRuploaddocument objfilename, string employee_gid)
        {
            // upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;

            string lsdocumenttype_gid = string.Empty;
            string hrdocument_name = httpRequest.Form["document_name"];
            string hrdocument_gid = httpRequest.Form["document_gid"];
            string employee = httpRequest.Form["employee_gid"];
            String path = lspath;
            HttpPostedFile httpPostedFile;



            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/HRDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        string project_flag = httpRequest.Form["project_flag"].ToString();
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
                            return;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "System/HRDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "System/HRDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("HRDU");
                        msSQL = " insert into sys_mst_temployeehrdocument(" +
                                    " hrdoc_id," +
                                    " employee_gid ," +
                                    " hrdocument_gid," +
                                    " hrdocument_name," +
                                    " hrdoc_name ," +
                                    " hrdoc_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee + "'," +
                                    "'" + hrdocument_gid + "'," +
                                    "'" + hrdocument_name.Replace("'", @"\'") + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully";

                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured While Uploading the document";

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }

        }

        public void DaHRDocDelete(string hrdoc_id, result values)
        {
            msSQL = " DELETE FROM sys_mst_temployeehrdocument WHERE hrdoc_id='" + hrdoc_id + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.message = "Document List Deleted Successfully....!";
                values.status = true;
                return;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
                return;
            }

        }

        //public void DaGetDeactivationConditionPopup(string user_gid, MdlDeactivation values)
        //{
        //    msSQL = " select employee_gid from hrm_mst_temployee  " +
        //            " where user_gid= '" + user_gid + "'";

        //    string deactivateemployee_gid = objdbconn.GetExecuteScalar(msSQL);


        //    msSQL = " select status from ams_trn_tasset2custodian  " +
        //            " where employee_gid= '" + deactivateemployee_gid + "' and" +
        //            " status in ('Acknowledged','Surrender Pending','Temporary Handover Pending','Temporary Handover')";
        //    values.asset_status = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " select status from ams_trn_tasset2custodian  " +
        //            " where temporaryhandover_gid='" + deactivateemployee_gid + "' and" +
        //            " status in ('Temporary Handover Surrender Pending','Temporary Handover-holding','Temporary Handover')";
        //    values.tempasset_status = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select b.employeereporting_to from hrm_mst_temployee a" +
        //                 " inner join adm_mst_tmodule2employee b on a.employee_gid=b.employee_gid" +
        //                 " inner join adm_mst_tmodule c on b.module_gid=c.module_gid " +
        //                 " where b.employeereporting_to='" + deactivateemployee_gid + "'";
        //    values.employeereporting_to = objdbconn.GetExecuteScalar(msSQL);
        //    if (values.employeereporting_to != null || values.employeereporting_to != "")
        //    {
        //        msSQL = " Select GROUP_CONCAT(distinct c.module_name SEPARATOR ', ') from hrm_mst_temployee a " +
        //                " inner join adm_mst_tmodule2employee b on a.employee_gid=b.employeereporting_to" +
        //                " inner join adm_mst_tmodule c on b.module_gid=c.module_gid " +
        //                " where b.hierarchy_level <> '-1' and a.employee_gid='" + deactivateemployee_gid + "' and b.visible='T' " +
        //                " order by c.menu_level desc ";
        //        values.module_name = objdbconn.GetExecuteScalar(msSQL);
        //    }

        //    //Samfin
        //    msSQL = " select application_gid from ocs_mst_tapplication  " +
        //            " where relationshipmanager_gid = '" + deactivateemployee_gid + "' and  approval_status not  in ('Incomplete', 'CC Approved' ,'CC Rejected','CC Meeting Skip','Rejected By Business','Rejected By Credit')";
        //    values.application_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select applicationapproval_gid from ocs_trn_tapplicationapproval " +
        //               " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
        //    values.applicationapproval_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select appcreditapproval_gid from ocs_trn_tappcreditapproval " +
        //         " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
        //    values.appcreditapproval_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select ccmeeting2members_gid from ocs_mst_tccmeeting2members " +
        //         " where ccmember_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
        //    values.ccmeeting2members_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select cadgroupmanager_gid from ocs_mst_tcadgroupmanager " +
        //         " where employee_gid='" + deactivateemployee_gid + "'";
        //    values.cadgroupmanager_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select cadgroupmembers_gid from ocs_mst_tcadgroupmembers " +
        //      " where employee_gid='" + deactivateemployee_gid + "'";
        //    values.cadgroupmembers_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select creditops2maker_gid from ocs_mst_tcreditops2maker " +
        //    " where employee_gid='" + deactivateemployee_gid + "'";
        //    values.creditops2maker_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select creditops2checker_gid from ocs_mst_tcreditops2checker " +
        //    " where employee_gid='" + deactivateemployee_gid + "'";
        //    values.creditops2checker_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " select credit2credithead_gid from ocs_mst_tcredit2credithead a " +
        //            " LEFT JOIN ocs_mst_tcredit2nationalmanager b ON a.creditmapping_gid=b.creditmapping_gid " +
        //            " LEFT JOIN ocs_mst_tcreditr2regionalmanager c ON a.creditmapping_gid=c.creditmapping_gid " +
        //            " LEFT JOIN ocs_mst_tcredit2creditmanager d ON a.creditmapping_gid=d.creditmapping_gid " +
        //            " where (a.employee_gid = '" + deactivateemployee_gid + "' or b.employee_gid = '" + deactivateemployee_gid + "' " +
        //            " or c.employee_gid = '" + deactivateemployee_gid + "' or d.employee_gid = '" + deactivateemployee_gid + "') group by a.creditmapping_gid ";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    if (dt_datatable.Rows.Count != 0)
        //    {

        //        values.creditmapping_gid = "True";
        //    }
        //    else
        //    {
        //        values.creditmapping_gid = "False";
        //    }


        //    msSQL = "  SELECT processtypeassign_gid FROM ocs_trn_tprocesstype_assign " +
        //            "  WHERE (maker_gid ='" + deactivateemployee_gid + "'  AND maker_approvalflag = 'N') " +
        //            "  || (checker_gid = '" + deactivateemployee_gid + "'  AND checker_approvalflag = 'N') " +
        //            "  || (approver_gid ='" + deactivateemployee_gid + "'  AND approver_approvalflag = 'N') ";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    if (dt_datatable.Rows.Count != 0)
        //    {

        //        values.processtype_assign = "True";
        //    }
        //    else
        //    {
        //        values.processtype_assign = "False";
        //    }
        //    //if (values.processtype_assign == "True" || values.creditmapping_gid == "True" || values.creditops2checker_gid != "" ||
        //    //    values.creditops2maker_gid != "" || values.cadgroupmembers_gid != "" || values.cadgroupmanager_gid != "" ||
        //    //    values.ccmeeting2members_gid != "" || values.appcreditapproval_gid != "" || values.applicationapproval_gid != "" || 
        //    //    values.module_name != "" || values.tempasset_status != "" || values.asset_status != "") {
        //    //    values.submitbutton = "True";
        //    //}
        //    //else {
        //    //    values.submitbutton = "False";
        //    //}

        //    msSQL = " Select ccmembermaster_gid from ocs_mst_tccmember " +
        //             " where ccmember_gid='" + deactivateemployee_gid + "'";
        //    values.ccmembermaster_gid = objdbconn.GetExecuteScalar(msSQL);

        //    //Samagro
        //    msSQL = " select application_gid from agr_mst_tapplication " +
        //            " where relationshipmanager_gid = '" + deactivateemployee_gid + "' and  approval_status not  in ('Incomplete', 'CC Approved' ,'CC Rejected','CC Meeting Skip','Rejected By Business','Rejected By Credit')";
        //    values.agrapplication_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select applicationapproval_gid from agr_trn_tapplicationapproval " +
        //               " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
        //    values.agrapplicationapproval_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select appcreditapproval_gid from agr_trn_tappcreditapproval " +
        //         " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
        //    values.agrappcreditapproval_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select ccmeeting2members_gid from agr_mst_tccmeeting2members " +
        //         " where ccmember_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
        //    values.agrccmeeting2members_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = "  SELECT processtypeassign_gid FROM agr_trn_tprocesstype_assign " +
        //              "  WHERE (maker_gid ='" + deactivateemployee_gid + "'  AND maker_approvalflag = 'N') " +
        //              "  || (checker_gid = '" + deactivateemployee_gid + "'  AND checker_approvalflag = 'N') " +
        //              "  || (approver_gid ='" + deactivateemployee_gid + "'  AND approver_approvalflag = 'N') ";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    if (dt_datatable.Rows.Count != 0)
        //    {

        //        values.agrprocesstype_assign = "True";
        //    }
        //    else
        //    {
        //        values.agrprocesstype_assign = "False";
        //    }

        //    msSQL = " select credit2credithead_gid from  agr_mst_tcredit2credithead a " +
        //              " LEFT JOIN  agr_mst_tcredit2nationalmanager b ON a.creditmapping_gid=b.creditmapping_gid " +
        //              " LEFT JOIN  agr_mst_tcreditr2regionalmanager c ON a.creditmapping_gid=c.creditmapping_gid " +
        //              " LEFT JOIN  agr_mst_tcredit2creditmanager d ON a.creditmapping_gid=d.creditmapping_gid " +
        //              " where (a.employee_gid = '" + deactivateemployee_gid + "' or b.employee_gid = '" + deactivateemployee_gid + "' " +
        //              " or c.employee_gid = '" + deactivateemployee_gid + "' or d.employee_gid = '" + deactivateemployee_gid + "') group by a.creditmapping_gid ";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    if (dt_datatable.Rows.Count != 0)
        //    {

        //        values.agrcreditmapping_gid = "True";
        //    }
        //    else
        //    {
        //        values.agrcreditmapping_gid = "False";
        //    }

        //    msSQL = " Select productdeskmanager_gid from agr_mst_tproductdeskmapping_manager " +
        //             " where employee_gid='" + deactivateemployee_gid + "'";
        //    values.productdeskmanager_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select productdeskmember_gid from agr_mst_tproductdeskmapping_member " +
        //        " where employee_gid='" + deactivateemployee_gid + "'";
        //    values.productdeskmember_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select mstpmgapproval_gid from agr_mst_pmgapproval " +
        //        " where pmgapproval_gid='" + deactivateemployee_gid + "'";
        //    values.mstpmgapproval_gid = objdbconn.GetExecuteScalar(msSQL);
        //    msSQL = " Select mstproductapproval_gid from agr_mst_productapproval " +
        //        " where productapproval_gid='" + deactivateemployee_gid + "'";
        //    values.mstproductapproval_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select appproductapproval_gid from agr_trn_tappproductapproval " +
        //          " where (product_managergid='" + deactivateemployee_gid + "' and productmanager_approvalflag = 'N' ) or " +
        //           " (product_membergid='" + deactivateemployee_gid + "'and productmember_approvalflag = 'N' ))";
        //    values.appproductapproval_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select warehouse2approval_gid from agr_trn_twarehouse2approval " +
        //          " where (approval_gid='" + deactivateemployee_gid + "' and productapproval_flag = 'N'  and mstproductapproval_gid is not null ) or " +
        //           " (approval_gid='" + deactivateemployee_gid + "'and pmgapproval_flag = 'N' and pmgapproval_flag is not null))";
        //    values.warehouse2approval_gid = objdbconn.GetExecuteScalar(msSQL);

        //    //Audit

        //    msSQL = " Select auditmapping2employee_gid from atm_mst_tauditmapping2employee  " +
        //             " where employee_gid='" + deactivateemployee_gid + "'";
        //    values.auditmapping2employee_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select a.multipleauditee_gid from atm_trn_tmultipleauditee a" +
        //            "  left  join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid " +
        //              " where a.auditeechecker_gid='" + deactivateemployee_gid + "' and  b.approval_status ='Checker - Auditee Pending'";
        //    values.multipleauditee_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " select auditcreation_gid from atm_trn_tauditcreation " +
        //               " where (auditmapping_gid = '" + deactivateemployee_gid + "' and (approval_status='Audit Approval pending' or approval_status='Checker Approval pending')) or " +
        //               " (employee_gid = '" + deactivateemployee_gid + "'  and approval_status='Approved') " +
        //               " or (auditmapping2employee_gid = '" + deactivateemployee_gid + "' and approval_status='Final Approval Pending') group by auditcreation_gid ";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    if (dt_datatable.Rows.Count != 0)
        //    {

        //        values.auditcreation_gid = "True";
        //    }
        //    else
        //    {
        //        values.auditcreation_gid = "False";
        //    }


        //    msSQL = " Select applicationapproval_gid from agr_trn_tapplicationapproval " +
        //            " LEFT JOIN ocs_mst_tcredit2nationalmanager b ON a.creditmapping_gid=b.creditmapping_gid " +
        //            " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
        //    values.agrapplicationapproval_gid = objdbconn.GetExecuteScalar(msSQL);


        //    // Foundation
        //    msSQL = " Select customerapproving_gid from fnd_mst_tcustomerapproving " +
        //             " where approver_gid='" + deactivateemployee_gid + "'";
        //    values.customerapproving_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select campaign_gid from fnd_trn_tcampaign " +
        //              " where campaign_approver='" + deactivateemployee_gid + "' and  (campaign_status ='Pending')";
        //    values.campaign_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select campaign_gid from fnd_trn_tcampaign " +
        //               " where campaign_approver='" + deactivateemployee_gid + "' and  (campaign_status ='Pending for Final Approval' )";
        //    values.finalcampaign_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " select campaignapproving2employee_gid from  fnd_mst_tcampaignapproving2employee a " +
        //            " left join fnd_trn_tcampaign b on a.campaign_gid=b.campaign_gid " +
        //            " where a.employee_gid = '" + deactivateemployee_gid + "'  and and b.campaign_status not in ('Approved','Rejected')";
        //    values.campaignapproving2employee_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " select customerapproving_gid from  fnd_mst_tcustomerapproving a " +
        //            " left join fnd_mst_tcustomer b on a.customer_gid=b.customer_gid " +
        //            " where a.approver_gid = '" + deactivateemployee_gid + "'  and b.status_remarks not in ('Approved','Rejected')";
        //    values.appcustomerapproving_gid = objdbconn.GetExecuteScalar(msSQL);

        //    // Business Developement
        //    msSQL = " select marketingcall_gid from mar_trn_tmarketingcall " +
        //                 " where (assignemployee_gid = '" + deactivateemployee_gid + "' and (callclosure_status='Pending-Acknowledge' or callclosure_status='Work-In-Progress')) or " +
        //                 " (assign_by = '" + deactivateemployee_gid + "'  and callclosure_status='Converted') ";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    if (dt_datatable.Rows.Count != 0)
        //    {

        //        values.marketingcall_gid = "True";
        //    }
        //    else
        //    {
        //        values.marketingcall_gid = "False";
        //    }

        //    //Inbound

        //    msSQL = " select inboundcall_gid from ocs_trn_tinboundcall " +
        //          " where (assignemployee_gid = '" + deactivateemployee_gid + "' and (callclosure_status= 'Assign' or callclosure_status= 'Work-In-Progress' or callclosure_status='Completed')) or " +
        //          " (assign_by = '" + deactivateemployee_gid + "'  and callclosure_status='Closed') ";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    if (dt_datatable.Rows.Count != 0)
        //    {

        //        values.inboundcall_gid = "True";
        //    }
        //    else
        //    {
        //        values.inboundcall_gid = "False";
        //    }

        //    //Sa_onboarding

        //    //Institution
        //    msSQL = " Select sacontactinstitution_gid from ocs_mst_tsainstitution a " +
        //            " where institutionmaker_gid='" + deactivateemployee_gid + "' and  approvalstatus ='BD Verification Completed'";
        //    values.makersacontactinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select sacontactinstitution_gid from ocs_mst_tsainstitution a " +
        //            " where institutionchecker_gid='" + deactivateemployee_gid + "' and  approvalstatus ='Maker Verified'";
        //    values.checkersacontactinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select sacontactinstitution_gid from ocs_mst_tsainstitution a " +
        //            " where rm_tagging='" + deactivateemployee_gid + "' and  approvalstatus in ('Checker Verified' ,'Pending BD Verification','Maker Verified','BD Verification Completed') ";
        //    values.finalsacontactinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

        //    //Individual

        //    msSQL = " Select sacontact_gid from ocs_mst_tsacontact a " +
        //            " where individualmaker_gid='" + deactivateemployee_gid + "' and  approvalstatus ='BD Verification Completed'";
        //    values.makersacontact_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select sacontact_gid from ocs_mst_tsacontact a " +
        //            " where individualchecker_gid='" + deactivateemployee_gid + "' and  approvalstatus ='Maker Verified'";
        //    values.checkersacontact_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select sacontact_gid from ocs_mst_tsacontact a " +
        //            " where rm_tagging='" + deactivateemployee_gid + "' and  approvalstatus in ('Checker Verified' ,'Pending BD Verification','Maker Verified','BD Verification Completed')";
        //    values.finalsacontact_gid = objdbconn.GetExecuteScalar(msSQL);



        //    //msSQL = " select sacontactinstitution_gid from ocs_mst_tsainstitution " +
        //    //       " where (rm_tagging = '" + deactivateemployee_gid + "' and approvalstatus = 'Pending BD Verification') or " +
        //    //       " (institutionmaker_gid = '" + deactivateemployee_gid + "' and approvalstatus = 'BD Verification Completed') or " +
        //    //       " (institutionmaker_gid = '" + deactivateemployee_gid + "' and approvalstatus='Maker Verified') or " +
        //    //       " (rm_tagging = '" + deactivateemployee_gid + "' and approvalstatus='Checker Verified')";
        //    //dt_datatable = objdbconn.GetDataTable(msSQL);
        //    //if (dt_datatable.Rows.Count != 0)
        //    //{

        //    //    values.sacontactinstitution_gid = "True";
        //    //}
        //    //else
        //    //{
        //    //    values.sacontactinstitution_gid = "False";
        //    //}

        //    //msSQL = " select sacontact_gid from ocs_mst_tsacontact " +
        //    //       " where (rm_tagging = '" + deactivateemployee_gid + "' and approvalstatus = 'Pending BD Verification') or " +
        //    //       " (individualmaker_gid = '" + deactivateemployee_gid + "' and approvalstatus = 'BD Verification Completed') or " +
        //    //       " (individualmaker_gid = '" + deactivateemployee_gid + "' and approvalstatus='Maker Verified') or " +
        //    //       " (rm_tagging = '" + deactivateemployee_gid + "' and approvalstatus='Checker Verified')";
        //    //dt_datatable = objdbconn.GetDataTable(msSQL);
        //    //if (dt_datatable.Rows.Count != 0)
        //    //{

        //    //    values.sacontact_gid = "True";
        //    //}
        //    //else
        //    //{
        //    //    values.sacontact_gid = "False";
        //    //}

        //    // Service Request

        //    msSQL = " Select activedepartment2manager_gid from osd_mst_tactivedepartment2manager " +
        //             " where (manager_gid='" + deactivateemployee_gid + "')";
        //    values.activedepartment2manager_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select activedepartment2member_gid from osd_mst_tactivedepartment2member " +
        //            " where (member_gid='" + deactivateemployee_gid + "')";
        //    values.activedepartment2member_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select supportteam2member_gid from osd_mst_tsupportteam2member " +
        //      " where member_gid='" + deactivateemployee_gid + "'";
        //    values.supportteam2member_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select requestapproval_gid from osd_trn_trequestapproval " +
        //              " where approval_gid='" + deactivateemployee_gid + "' and  approval_status ='Pending'";
        //    values.requestapproval_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " select servicerequest_gid from osd_trn_tservicerequest where assigned_membergid = '" + deactivateemployee_gid + "' and " +
        //            " request_status in('Allotted' , 'Work-In-Progress')";
        //    values.servicerequest_gid = objdbconn.GetExecuteScalar(msSQL);

        //    msSQL = " Select a.email_gid from osd_trn_tmaildetails a" +
        //            "  left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
        //            " where b.employee_gid='" + deactivateemployee_gid + "' and  b.status = 'Assigned' and a.status in ('Assigned', 'Transfer')";
        //    values.email_gid = objdbconn.GetExecuteScalar(msSQL);

        //    //msSQL = " Select maildetails_gid from osd_trn_tmaildetails " +
        //    //     " where updated_by='" + deactivateemployee_gid + "' and  status='Pending'";
        //    //values.maildetails_gid = objdbconn.GetExecuteScalar(msSQL);


        //    //Submit

        //    if (values.processtype_assign == "True" || values.creditmapping_gid == "True" || values.creditops2checker_gid != "" ||
        //        values.creditops2maker_gid != "" || values.cadgroupmembers_gid != "" || values.cadgroupmanager_gid != "" ||
        //        values.ccmeeting2members_gid != "" || values.appcreditapproval_gid != "" || values.applicationapproval_gid != "" ||
        //        values.module_name != "" || values.tempasset_status != "" || values.asset_status != "" || values.application_gid != "" ||
        //        values.ccmembermaster_gid != "" || values.agrapplication_gid != "" || values.agrapplicationapproval_gid != "" ||
        //        values.agrappcreditapproval_gid != "" || values.agrccmeeting2members_gid != "" || values.agrprocesstype_assign == "True" ||
        //        values.agrcreditmapping_gid == "True" || values.productdeskmanager_gid != "" || values.productdeskmember_gid != "" ||
        //        values.mstpmgapproval_gid != "" || values.mstproductapproval_gid != "" || values.appproductapproval_gid != "" ||
        //        values.warehouse2approval_gid != "" || values.auditmapping2employee_gid != "" || values.multipleauditee_gid != "" ||
        //        values.auditcreation_gid == "True" || values.agrapplicationapproval_gid != "" || values.customerapproving_gid != "" ||
        //        values.campaign_gid != "" || values.finalcampaign_gid != "" || values.campaignapproving2employee_gid != "" ||
        //        values.appcustomerapproving_gid != "" || values.marketingcall_gid == "True" || values.inboundcall_gid == "True" ||
        //        values.sacontactinstitution_gid == "True" || values.sacontact_gid == "True" || values.activedepartment2manager_gid != "" ||
        //        values.supportteam2member_gid != "" || values.requestapproval_gid != "" || values.servicerequest_gid != "" || values.email_gid != "" ||
        //         values.makersacontactinstitution_gid != "" ||
        //        values.checkersacontactinstitution_gid != "" || values.finalsacontactinstitution_gid != "" || values.makersacontact_gid != "" || values.checkersacontact_gid != "" ||
        //        values.finalsacontact_gid != "" || values.activedepartment2member_gid != "")
        //    {
        //        values.submitbutton = "True";
        //    }
        //    else
        //    {
        //        values.submitbutton = "False";
        //    }


        //    // service request

        //    if (values.activedepartment2member_gid != "" || values.activedepartment2manager_gid != "" ||
        //        values.supportteam2member_gid != "" || values.requestapproval_gid != "" || values.servicerequest_gid != "" || values.email_gid != "")
        //    {
        //        values.servicerequest = "True";
        //    }
        //    else
        //    {
        //        values.servicerequest = "False";
        //    }


        //    //Sa onboarding

        //    if (values.makersacontactinstitution_gid != "" ||
        //       values.checkersacontactinstitution_gid != "" || values.finalsacontactinstitution_gid != "" || values.makersacontact_gid != "" || values.checkersacontact_gid != "" ||
        //       values.finalsacontact_gid != "")
        //    {
        //        values.saonboarding = "True";
        //    }
        //    else
        //    {
        //        values.saonboarding = "False";
        //    }

        //    //Foundation

        //    if (values.customerapproving_gid != "" || values.campaign_gid != "" ||
        //        values.finalcampaign_gid != "" || values.campaignapproving2employee_gid != "" ||
        //        values.appcustomerapproving_gid != "")
        //    {
        //        values.foundation = "True";
        //    }
        //    else
        //    {
        //        values.foundation = "False";
        //    }

        //    //Audit
        //    if (values.auditmapping2employee_gid != "" || values.multipleauditee_gid != "" ||
        //        values.auditcreation_gid == "True")
        //    {
        //        values.audit = "True";
        //    }
        //    else
        //    {
        //        values.audit = "False";
        //    }

        //    //Samagro

        //    if (
        //       values.productdeskmanager_gid != "" || values.productdeskmember_gid != "" ||
        //       values.mstpmgapproval_gid != "" || values.agrcreditmapping_gid == "True" ||
        //       values.agrapplicationapproval_gid != "" || values.mstproductapproval_gid != "" ||
        //       values.agrappcreditapproval_gid != "" || values.agrapplication_gid != "" ||
        //       values.agrprocesstype_assign == "True" || values.agrccmeeting2members_gid != "" ||
        //       values.appproductapproval_gid != "" || values.warehouse2approval_gid != "")
        //    {
        //        values.samagro = "True";
        //    }
        //    else
        //    {
        //        values.samagro = "False";
        //    }

        //    //Samfin


        //    if (values.processtype_assign == "True" || values.ccmeeting2members_gid != "" ||
        //        values.appcreditapproval_gid != "" || values.applicationapproval_gid != "" ||
        //        values.application_gid != "" || values.ccmembermaster_gid != "" ||
        //        values.creditmapping_gid == "True" || values.creditops2checker_gid != "" ||
        //       values.creditops2maker_gid != "" ||
        //       values.cadgroupmembers_gid != "" || values.cadgroupmanager_gid != "" ||

        //        values.tempasset_status != "" || values.asset_status != ""
        //        )
        //    {
        //        values.samfin = "True";
        //    }
        //    else
        //    {
        //        values.samfin = "False";
        //    }

        //    //Asset

        //    if (values.tempasset_status != "" || values.asset_status != "")
        //    {
        //        values.asset = "True";
        //    }
        //    else
        //    {
        //        values.asset = "False";
        //    }

        //}

        public bool DaEmployeeProfileView(employee objemployee, string employee_gid)
        {
            try
            {
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , user_firstname,user_lastname," +
                       " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                       " c.employee_gender,c.role_gid,employeereporting_to,  " +
                       " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                       " d.designation_name,c.designation_gid,c.employee_gid,e.branch_name, employee_emailid,employee_mobileno,baselocation_name,s.baselocation_gid,c.entity_gid," +
                       " CASE " +
                       " WHEN a.user_status = 'Y' THEN 'Active'  " +
                       " WHEN a.user_status = 'N' THEN 'Inactive' " +
                       " END as user_access,a.user_status,c.department_gid,c.branch_gid,n.role_name, e.branch_name, g.department_name,c.marital_status,c.marital_status_gid,date_format(c.employee_joiningdate, '%d-%m-%Y') as joiningdate, " +
                       " c.employee_personalno as personal_phone_no,c.personal_emailid,c.bloodgroup as bloodgroup_name,c.bloodgroup_gid " +
                       " FROM hrm_mst_temployee c " +
                       " left join adm_mst_tuser a on a.user_gid = c.user_gid " +
                       " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                       " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                       " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                       " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                       " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                       " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                       " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid" +
                       " left join hrm_mst_trole n on n.role_gid=c.role_gid " +
                       " left join sys_mst_tbaselocation s on s.baselocation_gid = c.baselocation_gid " +
                       " where c.employee_gid='" + employee_gid + "'" +
                       " group by c.employee_gid " +
                       " order by c.employee_gid desc ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objemployee.company_name = objODBCDatareader["entity_name"].ToString();
                    objemployee.entity_gid = objODBCDatareader["entity_gid"].ToString();
                    objemployee.branch_gid = objODBCDatareader["branch_gid"].ToString();
                    objemployee.branch_name = objODBCDatareader["branch_name"].ToString();
                    objemployee.department_gid = objODBCDatareader["department_gid"].ToString();
                    objemployee.department_name = objODBCDatareader["department_name"].ToString();
                    objemployee.designation_gid = objODBCDatareader["designation_gid"].ToString();
                    objemployee.designation_name = objODBCDatareader["designation_name"].ToString();
                    objemployee.useraccess = objODBCDatareader["useraccess"].ToString();
                    objemployee.user_access = objODBCDatareader["user_access"].ToString();
                    objemployee.user_status = objODBCDatareader["user_status"].ToString();
                    objemployee.user_firstname = objODBCDatareader["user_firstname"].ToString();
                    objemployee.user_lastname = objODBCDatareader["user_lastname"].ToString();
                    objemployee.gender = objODBCDatareader["employee_gender"].ToString();
                    objemployee.employee_emailid = objODBCDatareader["employee_emailid"].ToString();
                    objemployee.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                    objemployee.user_code = objODBCDatareader["user_code"].ToString();
                    objemployee.role_gid = objODBCDatareader["role_gid"].ToString();
                    objemployee.role_name = objODBCDatareader["role_name"].ToString();
                    objemployee.employee_reportingto = objODBCDatareader["employeereporting_to"].ToString();
                    objemployee.baselocation_gid = objODBCDatareader["baselocation_gid"].ToString();
                    objemployee.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                    objemployee.marital_status = objODBCDatareader["marital_status"].ToString();
                    objemployee.marital_status_gid = objODBCDatareader["marital_status_gid"].ToString();
                    objemployee.joining_date = objODBCDatareader["joiningdate"].ToString();
                    objemployee.personal_phone_no = objODBCDatareader["personal_phone_no"].ToString();
                    objemployee.personal_emailid = objODBCDatareader["personal_emailid"].ToString();
                    objemployee.bloodgroup_name = objODBCDatareader["bloodgroup_name"].ToString();
                    objemployee.bloodgroup_gid = objODBCDatareader["bloodgroup_gid"].ToString();
                    if (objODBCDatareader["employee_joiningdate"].ToString() != "")
                    {
                        objemployee.joiningdate = Convert.ToDateTime(objODBCDatareader["employee_joiningdate"].ToString());
                    }
                }
                msSQL = "select concat(user_code, ' || ', user_firstname,' ',user_lastname) as employee_reportingto_name" +
                        " from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                        " where employee_gid = '" + objemployee.employee_reportingto + "'";
                objemployee.employee_reportingto_name = objdbconn.GetExecuteScalar(msSQL);

                string lspermanentaddressGID = string.Empty;
                string lstemporaryaddressGID = string.Empty;

                msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                        " hrm_trn_temployeedtl where " +
                        " employee_gid = '" + employee_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lspermanentaddressGID = objODBCDatareader["permanentaddress_gid"].ToString();
                    lstemporaryaddressGID = objODBCDatareader["temporaryaddress_gid"].ToString();
                }
                msSQL = " Select a.address1,a.address2,a.city, " +
                        " a.postal_code,a.state,b.country_name,b.country_gid" +
                        " from adm_mst_taddress a,adm_mst_tcountry b " +
                        " where  address_gid = '" + lspermanentaddressGID + "' and " +
                        " a.country_gid = b.country_gid and " +
                        " a.parent_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objemployee.per_address1 = objODBCDatareader["address1"].ToString();
                    objemployee.per_address2 = objODBCDatareader["address2"].ToString();
                    objemployee.per_country_gid = objODBCDatareader["country_gid"].ToString();
                    objemployee.per_country_name = objODBCDatareader["country_name"].ToString();
                    objemployee.per_state = objODBCDatareader["state"].ToString();
                    objemployee.per_city = objODBCDatareader["city"].ToString();
                    objemployee.per_postal_code = objODBCDatareader["postal_code"].ToString();
                }

                msSQL = " Select a.address1,a.address2,a.city, " +
                        " a.postal_code,a.state,b.country_name,b.country_gid" +
                        " from adm_mst_taddress a,adm_mst_tcountry b " +
                        " where  address_gid = '" + lstemporaryaddressGID + "' and " +
                        " a.country_gid = b.country_gid and " +
                        " a.parent_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objemployee.temp_address1 = objODBCDatareader["address1"].ToString();
                    objemployee.temp_address2 = objODBCDatareader["address2"].ToString();
                    objemployee.temp_country_gid = objODBCDatareader["country_gid"].ToString();
                    objemployee.temp_country_name = objODBCDatareader["country_name"].ToString();
                    objemployee.temp_state = objODBCDatareader["state"].ToString();
                    objemployee.temp_city = objODBCDatareader["city"].ToString();
                    objemployee.temp_postal_code = objODBCDatareader["postal_code"].ToString();
                }
                objODBCDatareader.Close();
                objemployee.status = true;
                return true;               
            }            
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                objemployee.status = false;
                return false;
            }          

        }

        public bool DaGetHRDocProfilelist(string employee_gid, hrdoc_list objemployeedoc_list)
        {
            try
            {
                msSQL = " select hrdoc_id,hrdocument_gid,hrdocument_name,hrdoc_name, hrdoc_path, " +
                        " documentsentforsign_flag,esignexpiry_flag,documentsigned_flag, " +
                        " concat(c.user_firstname,' ', c.user_lastname,'/' ,c.user_code) as created_by, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,migration_flag " +
                        " from sys_mst_temployeehrdocument a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.employee_gid = '" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_hrdoc_list = new List<hrdoc>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_hrdoc_list.Add(new hrdoc_list
                        {
                            hrdoc_id = dr_datarow["hrdoc_id"].ToString(),
                            hrdocument_gid = dr_datarow["hrdocument_gid"].ToString(),
                            hrdocument_name = dr_datarow["hrdocument_name"].ToString(),
                            hrdoc_name = dr_datarow["hrdoc_name"].ToString(),
                            hrdoc_path = objcmnstorage.EncryptData((dr_datarow["hrdoc_path"].ToString())),
                            documentsentforsign_flag = dr_datarow["documentsentforsign_flag"].ToString(),
                            esignexpiry_flag = dr_datarow["esignexpiry_flag"].ToString(),
                            documentsigned_flag = dr_datarow["documentsigned_flag"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            migration_flag = dr_datarow["migration_flag"].ToString(),
                        });
                    }
                    objemployeedoc_list.hrdoc = get_hrdoc_list;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

    }

}
