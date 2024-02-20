using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Linq;
using ems.storage.Functions;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access for various Application events (Credit, CAD, Business Stage - Approved, rejected, Revoked, Pending,
    /// History, Get Hierarchy) in Admin Application.
    /// </summary>
    /// <remarks>Written by Premchander.K, Sherin </remarks>

    public class DaAgrAdminApplication
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable;
        int mnResult, mnResultapprovallog, mnResulthierarchyupdateapprovallog,  mnResultapprovalreasonlog;
        string msSQL, msGetGid;
        string sToken = string.Empty;
        Random rand = new Random();
        string msGetGidapprovallog, msGetGidcreditapprovallog, lsapproved_date, msGetGidproductapprovallog, msGetGidcreditmanagerapprovalreasonlog, lsproductmember_approvaldate, lsproductmanager_approvaldate, msGetGidcreditmanagerapprovallog;

        public void DaGetBusinessRejectedApplSummary(string employee_gid, MdlRejectedAppl values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.creditgroup_gid,a.creditgroup_name, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date, " +
                    " case when ((d.application_gid is not null) or (e.application_gid is not null)) then 'Y' else 'N' end as history_flag " +
                    " from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tapplicationapprovallog d on d.application_gid = a.application_gid " +
                    " left join agr_trn_tbusinessrejectrevokelog e on e.application_gid = a.application_gid " +
                    " where a.approval_status='Rejected By Business' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrejectedappl_list = new List<rejectedappl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getrejectedappl_list.Add(new rejectedappl_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        history_flag = dt["history_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString(),
                    });

                }
            }
            values.rejectedappl_list = getrejectedappl_list;
            dt_datatable.Dispose();
        }

        public void DaGetBusinessHoldApplSummary(string employee_gid, MdlHoldAppl values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.creditgroup_gid,a.creditgroup_name, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date, " +
                    " case when ((d.application_gid is not null) or (e.application_gid is not null)) then 'Y' else 'N' end as history_flag " +
                    " from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tapplicationapprovallog d on d.application_gid = a.application_gid " +
                    " left join agr_trn_tbusinessrejectrevokelog e on e.application_gid = a.application_gid " +
                    " where a.approval_status='Hold By Business' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getholdappl_list = new List<holdappl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getholdappl_list.Add(new holdappl_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        history_flag = dt["history_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString(),
                    });

                }
            }
            values.holdappl_list = getholdappl_list;
            dt_datatable.Dispose();
        }

        public bool DaPostRejectRevokeApplication(mdlrejectrevoke values, string user_gid, string employee_gid)
        {
            msSQL = "select approval_status from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.approval_status = objODBCDatareader["approval_status"].ToString();
            }
            objODBCDatareader.Close();

            if (values.approval_status == "Rejected By Business")
            {
                msSQL = " select applicationapproval_gid,hierary_level,(hierary_level-1) as hierarylevel_count, " +
                        " approval_gid as rejected_by,rejected_date,approval_remarks as business_remarks" +
                        " from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "'" +
                        " and approval_status='Rejected'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.applicationapproval_gid = objODBCDatareader["applicationapproval_gid"].ToString();
                    values.hierary_level = objODBCDatareader["hierary_level"].ToString();
                    values.hierarylevel_count = objODBCDatareader["hierarylevel_count"].ToString();
                    values.rejected_by = objODBCDatareader["rejected_by"].ToString();
                    values.rejected_date = objODBCDatareader["rejected_date"].ToString();
                    values.business_remarks = objODBCDatareader["business_remarks"].ToString();
                }
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " select applicationapproval_gid,hierary_level,(hierary_level-1) as hierarylevel_count," +
                        " approval_gid as hold_by,hold_date,approval_remarks as business_remarks" +
                        " from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "' " +
                        " and approval_status='Hold'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.applicationapproval_gid = objODBCDatareader["applicationapproval_gid"].ToString();
                    values.hierary_level = objODBCDatareader["hierary_level"].ToString();
                    values.hierarylevel_count = objODBCDatareader["hierarylevel_count"].ToString();
                    values.hold_by = objODBCDatareader["hold_by"].ToString();
                    values.hold_date = objODBCDatareader["hold_date"].ToString();
                    values.business_remarks = objODBCDatareader["business_remarks"].ToString();
                }
                objODBCDatareader.Close();
            }

            msSQL = "select approved_date from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "' and hierary_level='" + values.hierarylevel_count + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapproved_date = objODBCDatareader["approved_date"].ToString();
            }
            else
            {
                lsapproved_date = null;
            }
            objODBCDatareader.Close();

            msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidapprovallog = objcmnfunctions.GetMasterGID("A2CN");
                    msSQL = " insert into agr_trn_tapplicationapprovallog " +
                            " (applicationapprovallog_gid,applicationapproval_gid, application_gid, approval_gid, approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag) " +
                            " select @applicationapprovallog_gid := '" + msGetGidapprovallog + "', applicationapproval_gid,application_gid, approval_gid,approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag " +
                            " from agr_trn_tapplicationapproval " +
                            " where application_gid='" + values.application_gid + "'" +
                            " and applicationapproval_gid='" + dt["applicationapproval_gid"].ToString() + "'";
                    mnResultapprovallog = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {

            }

            if (mnResultapprovallog != 0)
            {
                if (values.approval_status == "Rejected By Business")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BRRL");
                    msSQL = " insert into agr_trn_tbusinessrejectrevokelog ( " +
                            " businessrejectrevokelog_gid, " +
                            " application_gid, " +
                            " reason, " +
                            " created_by, " +
                            " created_date, " +
                            " approval_status," +
                            " headapproval_status, " +
                            " headapproval_date, " +
                            " rejected_by," +
                            " rejected_date," +
                            " hold_by," +
                            " hold_date," +
                            " business_remarks," +
                            " businessapproval_status) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.reason + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Rejected By Business'," +
                            "'" + "L" + values.hierary_level + " - Rejected" + "',";
                    if (lsapproved_date == null || lsapproved_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsapproved_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    msSQL += "'" + values.rejected_by + "'," +
                            "'" + Convert.ToDateTime(values.rejected_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "null," +
                             "null," +
                             "'" + values.business_remarks + "'," +
                             "'" + "L" + values.hierary_level + " - Pending" + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BRRL");
                    msSQL = " insert into agr_trn_tbusinessrejectrevokelog ( " +
                            " businessrejectrevokelog_gid, " +
                            " application_gid, " +
                            " reason, " +
                            " created_by, " +
                            " created_date, " +
                            " approval_status," +
                            " headapproval_status, " +
                            " headapproval_date," +
                            " rejected_by," +
                            " rejected_date," +
                            " hold_by," +
                            " hold_date," +
                            " business_remarks, " +
                            " businessapproval_status) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.reason + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Hold By Business'," +
                            "'" + "L" + values.hierary_level + " - Hold" + "',";
                    if (lsapproved_date == null || lsapproved_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsapproved_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    msSQL += "null," +
                             "null," +
                             "'" + values.hold_by + "'," +
                             "'" + Convert.ToDateTime(values.hold_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "'" + values.business_remarks + "'," +
                             "'" + "L" + values.hierary_level + " - Pending" + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    if (values.hierarylevel_count == "0")
                    {
                        msSQL = " update agr_mst_tapplication set approval_status='Submitted to Approval', headapproval_status='Pending',headapproval_date=null" +
                                " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " update agr_mst_tapplication set approval_status='Submitted to Approval', headapproval_status='L" + values.hierarylevel_count + " - Approved',headapproval_date='" + Convert.ToDateTime(lsapproved_date).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    msSQL = " update agr_trn_tapplicationapproval set approval_status='Pending',approval_remarks=null," +
                            " rejected_date=null,hold_date=null where applicationapproval_gid='" + values.applicationapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_status='Pending' " +
                            " where application_gid='" + values.application_gid + "' and applicationapproval_gid > '" + values.applicationapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                values.status = true;
                values.message = "Application Revoked Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred..!";
                return false;
            }
        }

        public void DaGetBusinessRevokedApplSummary(string employee_gid, MdlRevokedAppl values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customer_name,a.customer_urn,a.vertical_name,a.applicant_type, a.region, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                     " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.creditgroup_gid,a.creditgroup_name,d.created_date, " +
                     " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date, " +
                     " (select f.approval_status from agr_trn_tbusinessrejectrevokelog f where " +
                     " f.created_date = (select max(k.created_date) from agr_trn_tbusinessrejectrevokelog k where k.application_gid = a.application_gid)) " +
                     " as approval_status" +
                     " from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid=a.created_by  " +
                     " left join agr_trn_tapplicationapprovallog d on d.application_gid=a.application_gid " +
                     " left join agr_trn_tbusinessrejectrevokelog e on e.application_gid = a.application_gid " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  " +
                     " where d.application_gid=a.application_gid " +
                     " group by(d.application_gid) order by d.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrevokedappl_list = new List<revokedappl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getrevokedappl_list.Add(new revokedappl_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });

                }
            }
            values.revokedappl_list = getrevokedappl_list;
            dt_datatable.Dispose();
        }

        public void DaGetBusinessPendingHistoryLog(string application_gid, string employee_gid, MdlRevokedAppl values)
        {
            msSQL = " select a.application_gid, a.approval_remarks,approval_status,applicationapproval_gid, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as rejecttedhold_by,hierary_level, " +
                     " case when a.rejected_date is not null then date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') when date_format(a.hold_date,'%d-%m-%Y %h:%i %p') is not null then a.hold_date else null end as rejecttedhold_date " +
                     " from agr_trn_tapplicationapproval a " +
                     " left join hrm_mst_temployee b on b.employee_gid=a.approval_gid  " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  " +
                     " where a.application_gid='" + application_gid + "' and (a.approval_status = 'Rejected' or a.approval_status = 'Hold') group by a.application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpendingapplhis_list = new List<pendingapplhis_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getpendingapplhis_list.Add(new pendingapplhis_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        rejecttedhold_by = dt["rejecttedhold_by"].ToString(),
                        rejecttedhold_date = dt["rejecttedhold_date"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        hierary_level = dt["hierary_level"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicationapproval_gid = dt["applicationapproval_gid"].ToString()
                    });

                }
            }
            values.pendingapplhis_list = getpendingapplhis_list;
            dt_datatable.Dispose();
        }

        public void DaGetBusinessHistoryLog(string application_gid, string employee_gid, MdlRevokedAppl values)
        {
            msSQL = " select a.application_gid,a.reason as revoked_remarks,businessrejectrevokelog_gid,  " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as rejected_by, " +
                    " date_format(a.rejected_date, '%d-%m-%Y %h:%i %p') as rejected_date," +
                    " date_format(a.hold_date, '%d-%m-%Y %h:%i %p') as hold_date,business_remarks,businessapproval_status, " +
                    " concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as hold_by," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as revoked_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as revoked_date  " +
                    " from agr_trn_tbusinessrejectrevokelog a  " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by   " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid=a.rejected_by  " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid   " +
                    " left join hrm_mst_temployee f on f.employee_gid=a.hold_by  " +
                    " left join adm_mst_tuser g on g.user_gid=f.user_gid   " +
                    " where a.application_gid = '" + application_gid + "' order by a.businessrejectrevokelog_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinesshistory_list = new List<businesshistory_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getbusinesshistory_list.Add(new businesshistory_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        rejected_by = dt["rejected_by"].ToString(),
                        rejected_date = dt["rejected_date"].ToString(),
                        hold_by = dt["hold_by"].ToString(),
                        hold_date = dt["hold_date"].ToString(),
                        business_remarks = dt["business_remarks"].ToString(),
                        revoked_by = dt["revoked_by"].ToString(),
                        revoked_date = dt["revoked_date"].ToString(),
                        revoked_remarks = dt["revoked_remarks"].ToString(),
                        businessapproval_status = dt["businessapproval_status"].ToString(),
                        businessrejectrevokelog_gid = dt["businessrejectrevokelog_gid"].ToString()
                    });

                }
            }
            values.businesshistory_list = getbusinesshistory_list;
            dt_datatable.Dispose();
        }

        public void DaApplicationRevokeCount(string user_gid, string employee_gid, RevokeApplicationCount values)
        {
            msSQL = " select count(application_gid) as businessreject_count from agr_mst_tapplication a " +
                    " where a.approval_status='Rejected By Business'";
            values.businessreject_count = objdbconn.GetExecuteScalar(msSQL);
            int businessreject_count = Convert.ToInt16(values.businessreject_count);

            msSQL = " select count(application_gid) as businesshold_count from agr_mst_tapplication a " +
                    " where a.approval_status = 'Hold By Business'";
            values.businesshold_count = objdbconn.GetExecuteScalar(msSQL);
            int businesshold_count = Convert.ToInt16(values.businesshold_count);

            msSQL = " select count(distinct a.application_gid) as businessrevoked_count from agr_mst_tapplication a " +
                    " left join agr_trn_tapplicationapprovallog b on b.application_gid=a.application_gid  " +
                    " where b.application_gid=a.application_gid ";
            values.businessrevoked_count = objdbconn.GetExecuteScalar(msSQL);
            int businessrevoked_count = Convert.ToInt16(values.businessrevoked_count);

            int totalcount = businessreject_count + businesshold_count + businessrevoked_count;
            values.lstotalcount = Convert.ToInt16(totalcount);

        }

        public void DaGetHistoryRemarksView(string businessrejectrevokelog_gid, MdlRevokedAppl values)
        {
            try
            {
                msSQL = " select  businessrejectrevokelog_gid,business_remarks,reason,application_gid," +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                        " from agr_trn_tbusinessrejectrevokelog a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where businessrejectrevokelog_gid = '" + businessrejectrevokelog_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.businessrejectrevokelog_gid = objODBCDatareader["businessrejectrevokelog_gid"].ToString();
                    values.business_remarks = objODBCDatareader["business_remarks"].ToString();
                    values.reason = objODBCDatareader["reason"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetPendingHistoryRemarksView(string applicationapproval_gid, MdlRevokedAppl values)
        {
            try
            {
                msSQL = " select applicationapproval_gid,approval_remarks as rejecthold_remarks,application_gid," +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                        " from agr_trn_tapplicationapproval a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where applicationapproval_gid = '" + applicationapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.applicationapproval_gid = objODBCDatareader["applicationapproval_gid"].ToString();
                    values.rejecthold_remarks = objODBCDatareader["rejecthold_remarks"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetCreditRejectedApplSummary(string employee_gid, MdlCreditRejectedAppl values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.creditgroup_gid,a.creditgroup_name, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date, " +
                    " case when ((d.application_gid is not null) or (e.application_gid is not null)) then 'Y' else 'N' end as history_flag " +
                    " from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappcreditrevokeapprovallog d on d.application_gid = a.application_gid " +
                    " left join agr_trn_tcreditrevokelog e on e.application_gid = a.application_gid " +
                    " where a.approval_status='Rejected By Credit' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditrejectedappl_list = new List<creditrejectedappl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditrejectedappl_list.Add(new creditrejectedappl_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        history_flag = dt["history_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString()
                    });

                }
            }
            values.creditrejectedappl_list = getcreditrejectedappl_list;
            dt_datatable.Dispose();
        }

        public void DaGetCreditHoldApplSummary(string employee_gid, MdlCreditHoldAppl values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.creditgroup_gid,a.creditgroup_name, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date, " +
                    " case when ((d.application_gid is not null) or (e.application_gid is not null)) then 'Y' else 'N' end as history_flag " +
                    " from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappcreditrevokeapprovallog d on d.application_gid = a.application_gid " +
                    " left join agr_trn_tcreditrevokelog e on e.application_gid = a.application_gid " +
                    " where a.approval_status='Hold By Credit' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditholdappl_list = new List<creditholdappl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditholdappl_list.Add(new creditholdappl_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        history_flag = dt["history_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString()
                    });

                }
            }
            values.creditholdappl_list = getcreditholdappl_list;
            dt_datatable.Dispose();
        }

        public void DaCreditApplicationRevokeCount(string user_gid, string employee_gid, CreditRevokeApplicationCount values)
        {
            msSQL = " select count(application_gid) creditreject_count from agr_mst_tapplication a " +
                    " where a.approval_status='Rejected By Credit'";
            values.creditreject_count = objdbconn.GetExecuteScalar(msSQL);
            int creditreject_count = Convert.ToInt16(values.creditreject_count);

            msSQL = " select count(application_gid) as credithold_count from agr_mst_tapplication a " +
                    " where a.approval_status = 'Hold By Credit'";
            values.credithold_count = objdbconn.GetExecuteScalar(msSQL);
            int credithold_count = Convert.ToInt16(values.credithold_count);

            msSQL = " select count(distinct a.application_gid) as creditrevoked_count from agr_mst_tapplication a " +
                    " left join agr_trn_tappcreditrevokeapprovallog b on b.application_gid=a.application_gid  " +
                    " where b.application_gid=a.application_gid ";
            values.creditrevoked_count = objdbconn.GetExecuteScalar(msSQL);
            int creditrevoked_count = Convert.ToInt16(values.creditrevoked_count);

            msSQL = " select count(application_gid) creditreject_count from agr_mst_tapplication a " +
                  " where a.approval_status='Rejected by Credit Manager'";
            values.creditmanagerreject_count = objdbconn.GetExecuteScalar(msSQL);
            int creditmanagerreject_count = Convert.ToInt16(values.creditmanagerreject_count);

            int totalcount = creditreject_count + credithold_count + creditrevoked_count + creditmanagerreject_count;
            values.lstotalcount = Convert.ToInt16(totalcount);

        }

        public bool DaPostCreditRevokeApplication(Mdlcreditrevoke values, string user_gid, string employee_gid)
        {
            msSQL = "select approval_status from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.approval_status = objODBCDatareader["approval_status"].ToString();
            }
            objODBCDatareader.Close();

            if (values.approval_status == "Rejected By Credit")
            {
                msSQL = " select appcreditapproval_gid,hierary_level,(hierary_level-1) as hierarylevel_count, " +
                        " approval_gid as rejected_by,rejected_date,approval_remarks as credit_remarks" +
                        " from agr_trn_tappcreditapproval where application_gid='" + values.application_gid + "'" +
                        " and approval_status='Rejected'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.appcreditapproval_gid = objODBCDatareader["appcreditapproval_gid"].ToString();
                    values.hierary_level = objODBCDatareader["hierary_level"].ToString();
                    values.hierarylevel_count = objODBCDatareader["hierarylevel_count"].ToString();
                    values.rejected_by = objODBCDatareader["rejected_by"].ToString();
                    values.rejected_date = objODBCDatareader["rejected_date"].ToString();
                    values.credit_remarks = objODBCDatareader["credit_remarks"].ToString();
                }
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " select appcreditapproval_gid,hierary_level,(hierary_level-1) as hierarylevel_count," +
                        " approval_gid as hold_by,hold_date,approval_remarks as credit_remarks" +
                        " from agr_trn_tappcreditapproval where application_gid='" + values.application_gid + "' " +
                        " and approval_status='Hold'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.appcreditapproval_gid = objODBCDatareader["appcreditapproval_gid"].ToString();
                    values.hierary_level = objODBCDatareader["hierary_level"].ToString();
                    values.hierarylevel_count = objODBCDatareader["hierarylevel_count"].ToString();
                    values.hold_by = objODBCDatareader["hold_by"].ToString();
                    values.hold_date = objODBCDatareader["hold_date"].ToString();
                    values.credit_remarks = objODBCDatareader["credit_remarks"].ToString();
                }
                objODBCDatareader.Close();
            }

            msSQL = "select approved_date from agr_trn_tappcreditapproval where application_gid='" + values.application_gid + "' and hierary_level='" + values.hierarylevel_count + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapproved_date = objODBCDatareader["approved_date"].ToString();
            }
            else
            {
                lsapproved_date = null;
            }
            objODBCDatareader.Close();

            msSQL = "select appcreditapproval_gid from agr_trn_tappcreditapproval where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidcreditapprovallog = objcmnfunctions.GetMasterGID("ACAL");
                    msSQL = " insert into agr_trn_tappcreditrevokeapprovallog " +
                            " (appcreditrevokeapprovallog_gid,appcreditapproval_gid, application_gid, approval_gid, approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag) " +
                            " select @appcreditrevokeapprovallog_gid := '" + msGetGidcreditapprovallog + "', appcreditapproval_gid,application_gid, approval_gid,approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag " +
                            " from agr_trn_tappcreditapproval " +
                            " where application_gid='" + values.application_gid + "'" +
                            " and appcreditapproval_gid='" + dt["appcreditapproval_gid"].ToString() + "'";
                    mnResultapprovallog = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {

            }

            if (mnResultapprovallog != 0)
            {
                if (values.approval_status == "Rejected By Credit")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CRRL");
                    msSQL = " insert into agr_trn_tcreditrevokelog ( " +
                            " creditrevokelog_gid, " +
                            " application_gid, " +
                            " reason, " +
                            " created_by, " +
                            " created_date, " +
                            " approval_status," +
                            " creditheadapproval_status, " +
                            " creditheadapproval_date, " +
                            " rejected_by," +
                            " rejected_date," +
                            " hold_by," +
                            " hold_date," +
                            " credit_remarks," +
                            " creditapproval_status) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.reason + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Rejected By Credit'," +
                            "'" + "L" + values.hierary_level + " - Rejected" + "',";
                    if (lsapproved_date == null || lsapproved_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsapproved_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    msSQL += "'" + values.rejected_by + "'," +
                            "'" + Convert.ToDateTime(values.rejected_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "null," +
                             "null," +
                             "'" + values.credit_remarks + "'," +
                             "'" + "L" + values.hierary_level + " - Pending" + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CRRL");
                    msSQL = " insert into agr_trn_tcreditrevokelog ( " +
                            " creditrevokelog_gid, " +
                            " application_gid, " +
                            " reason, " +
                            " created_by, " +
                            " created_date, " +
                            " approval_status," +
                            " creditheadapproval_status, " +
                            " creditheadapproval_date," +
                            " rejected_by," +
                            " rejected_date," +
                            " hold_by," +
                            " hold_date," +
                            " credit_remarks, " +
                            " creditapproval_status) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.reason + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Hold By Credit'," +
                            "'" + "L" + values.hierary_level + " - Hold" + "',";
                    if (lsapproved_date == null || lsapproved_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsapproved_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    msSQL += "null," +
                             "null," +
                             "'" + values.hold_by + "'," +
                             "'" + Convert.ToDateTime(values.hold_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "'" + values.credit_remarks + "'," +
                             "'" + "L" + values.hierary_level + " - Pending" + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    if (values.hierarylevel_count == "0")
                    {
                        msSQL = " update agr_mst_tapplication set approval_status='Submitted to Credit Approval', creditheadapproval_status='Pending',creditheadapproval_date=null" +
                                " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " update agr_mst_tapplication set approval_status='Submitted to Credit Approval', creditheadapproval_status='L" + values.hierarylevel_count + " - Approved', " +
                                " creditheadapproval_date='" + Convert.ToDateTime(lsapproved_date).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    msSQL = " update agr_trn_tappcreditapproval set approval_status='Pending',approval_remarks=null," +
                            " rejected_date=null,hold_date=null where appcreditapproval_gid='" + values.appcreditapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tappcreditapproval set approval_status='Pending' " +
                            " where application_gid='" + values.application_gid + "' and appcreditapproval_gid > '" + values.appcreditapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                values.status = true;
                values.message = "Application Revoked Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred..!";
                return false;
            }
        }

        public void DaGetCreditRevokedApplSummary(string employee_gid, MdlCreditRevokedAppl values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customer_name,a.customer_urn,a.vertical_name,a.applicant_type, a.region, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                     " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.creditgroup_gid,a.creditgroup_name,d.created_date, " +
                     " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date, " +
                     " (select f.approval_status from agr_trn_tcreditrevokelog f where " +
                     " f.created_date = (select max(k.created_date) from agr_trn_tcreditrevokelog k where k.application_gid = a.application_gid)) " +
                     " as approval_status" +
                     " from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid=a.created_by  " +
                     " left join agr_trn_tappcreditrevokeapprovallog d on d.application_gid=a.application_gid " +
                     " left join agr_trn_tcreditrevokelog e on e.application_gid = a.application_gid " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  " +
                     " where d.application_gid=a.application_gid " +
                     " group by(d.application_gid) order by d.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditrevokedappl_list = new List<creditrevokedappl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditrevokedappl_list.Add(new creditrevokedappl_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });

                }
            }
            values.creditrevokedappl_list = getcreditrevokedappl_list;
            dt_datatable.Dispose();
        }

        public void DaGetCreditPendingHistoryLog(string application_gid, string employee_gid, MdlCreditPendingHistoryAppl values)
        {
            msSQL = " select a.application_gid, a.approval_remarks,approval_status,appcreditapproval_gid, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as rejecttedhold_by,hierary_level, " +
                     " case when a.rejected_date is not null then date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') when date_format(a.hold_date,'%d-%m-%Y %h:%i %p') is not null then a.hold_date else null end as rejecttedhold_date " +
                     " from agr_trn_tAppcreditapproval a " +
                     " left join hrm_mst_temployee b on b.employee_gid=a.approval_gid  " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  " +
                     " where a.application_gid='" + application_gid + "' and (a.approval_status = 'Rejected' or a.approval_status = 'Hold') group by a.application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditpendingapplhis_list = new List<creditpendingapplhis_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditpendingapplhis_list.Add(new creditpendingapplhis_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        rejecttedhold_by = dt["rejecttedhold_by"].ToString(),
                        rejecttedhold_date = dt["rejecttedhold_date"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        hierary_level = dt["hierary_level"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString()
                    });

                }
            }
            values.creditpendingapplhis_list = getcreditpendingapplhis_list;
            dt_datatable.Dispose();
        }

        public void DaGetCreditHistoryLog(string application_gid, string employee_gid, MdlCreditHistoryLog values)
        {
            msSQL = " select a.application_gid,a.reason as revoked_remarks,creditrevokelog_gid,  " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as rejected_by, " +
                    " date_format(a.rejected_date, '%d-%m-%Y %h:%i %p') as rejected_date," +
                    " date_format(a.hold_date, '%d-%m-%Y %h:%i %p') as hold_date,credit_remarks,creditapproval_status, " +
                    " concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as hold_by," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as revoked_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as revoked_date  " +
                    " from agr_trn_tcreditrevokelog a  " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by   " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid=a.rejected_by  " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid   " +
                    " left join hrm_mst_temployee f on f.employee_gid=a.hold_by  " +
                    " left join adm_mst_tuser g on g.user_gid=f.user_gid   " +
                    " where a.application_gid = '" + application_gid + "' order by a.creditrevokelog_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcredithistorylog_list = new List<credithistorylog_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcredithistorylog_list.Add(new credithistorylog_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        rejected_by = dt["rejected_by"].ToString(),
                        rejected_date = dt["rejected_date"].ToString(),
                        hold_by = dt["hold_by"].ToString(),
                        hold_date = dt["hold_date"].ToString(),
                        credit_remarks = dt["credit_remarks"].ToString(),
                        revoked_by = dt["revoked_by"].ToString(),
                        revoked_date = dt["revoked_date"].ToString(),
                        revoked_remarks = dt["revoked_remarks"].ToString(),
                        creditapproval_status = dt["creditapproval_status"].ToString(),
                        creditrevokelog_gid = dt["creditrevokelog_gid"].ToString()
                    });

                }
            }
            values.credithistorylog_list = getcredithistorylog_list;
            dt_datatable.Dispose();
        }

        public void DaGetCreditPendingHistoryRemarksView(string appcreditapproval_gid, MdlCreditPendingHistoryAppl values)
        {
            try
            {
                msSQL = " select appcreditapproval_gid,approval_remarks as creditrejecthold_remarks,application_gid," +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                        " from agr_trn_tAppcreditapproval a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where appcreditapproval_gid = '" + appcreditapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.appcreditapproval_gid = objODBCDatareader["appcreditapproval_gid"].ToString();
                    values.creditrejecthold_remarks = objODBCDatareader["creditrejecthold_remarks"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetCreditHistoryRemarksView(string creditrevokelog_gid, MdlCreditHistoryApplLog values)
        {
            try
            {
                msSQL = " select creditrevokelog_gid,credit_remarks,reason,application_gid," +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                        " from agr_trn_tcreditrevokelog a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where creditrevokelog_gid = '" + creditrevokelog_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditrevokelog_gid = objODBCDatareader["creditrevokelog_gid"].ToString();
                    values.credit_remarks = objODBCDatareader["credit_remarks"].ToString();
                    values.reason = objODBCDatareader["reason"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetBusinessStageSummary(string employee_gid, MdlBusinessHierarchyUpdate values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,a.vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,a.program_gid,a.program_name, " +
                    " case when (d.application_gid is not null) then 'Y' else 'N' end as hierarchyupdated_flag " +
                    " from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tbusinesshierarchyupdatelog d on d.application_gid=a.application_gid " +
                    " where a.approval_status='Submitted to Approval' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessstage_list = new List<businessstage_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getbusinessstage_list.Add(new businessstage_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        program_gid = dt["program_gid"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        hierarchyupdated_flag = dt["hierarchyupdated_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString(),
                    });

                }
            }
            values.businessstage_list = getbusinessstage_list;
            dt_datatable.Dispose();
        }

        public void DaGetCreditStageSummary(string employee_gid, MdlBusinessHierarchyUpdate values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,a.vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,a.program_gid,a.program_name," +
                    " case when (d.application_gid is not null) then 'Y' else 'N' end as hierarchyupdated_flag " +
                    " from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tbusinesshierarchyupdatelog d on d.application_gid=a.application_gid " +
                    " where a.approval_status='Submitted to Underwriting' or a.approval_status='Submitted to Credit Approval' " +
                    " or a.approval_status='Sent Back to Credit' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditstage_list = new List<creditstage_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditstage_list.Add(new creditstage_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        program_gid = dt["program_gid"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        hierarchyupdated_flag = dt["hierarchyupdated_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString(),
                    });

                }
            }
            values.creditstage_list = getcreditstage_list;
            dt_datatable.Dispose();
        }

        public void DaGetCcStageSummary(string employee_gid, MdlBusinessHierarchyUpdate values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,a.vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,a.program_gid,a.program_name, " +
                    " case when (d.application_gid is not null) then 'Y' else 'N' end as hierarchyupdated_flag " +
                    " from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tbusinesshierarchyupdatelog d on d.application_gid=a.application_gid " +
                    " where a.approval_status='Submitted to CC' or a.approval_status='Sent Back to CC' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getccstage_list = new List<ccstage_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getccstage_list.Add(new ccstage_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        program_gid = dt["program_gid"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        hierarchyupdated_flag = dt["hierarchyupdated_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString(),

                    });

                }
            }
            values.ccstage_list = getccstage_list;
            dt_datatable.Dispose();
        }

        public void DaGetCadPendingStageSummary(string employee_gid, MdlBusinessHierarchyUpdate values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,a.vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,a.program_gid,a.program_name, " +
                    " case when (d.application_gid is not null) then 'Y' else 'N' end as hierarchyupdated_flag " +
                    " from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tbusinesshierarchyupdatelog d on d.application_gid=a.application_gid " +
                    " where a.approval_status='CC Approved' and a.process_type is null group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcadpendingstage_list = new List<cadpendingstage_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcadpendingstage_list.Add(new cadpendingstage_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        program_gid = dt["program_gid"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        hierarchyupdated_flag = dt["hierarchyupdated_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString(),
                    });

                }
            }
            values.cadpendingstage_list = getcadpendingstage_list;
            dt_datatable.Dispose();
        }

        public void DaGetCadAcceptedStageSummary(string employee_gid, MdlBusinessHierarchyUpdate values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,a.vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,a.program_gid,a.program_name, " +
                    " case when (d.application_gid is not null) then 'Y' else 'N' end as hierarchyupdated_flag " +
                    " from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tbusinesshierarchyupdatelog d on d.application_gid=a.application_gid " +
                    " where a.process_type = 'Accept' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcadacceptedstage_list = new List<cadacceptedstage_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcadacceptedstage_list.Add(new cadacceptedstage_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        program_gid = dt["program_gid"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        hierarchyupdated_flag = dt["hierarchyupdated_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString(),
                    });

                }
            }
            values.cadacceptedstage_list = getcadacceptedstage_list;
            dt_datatable.Dispose();
        }

        public void DaHierarchyUpdateApplCount(string user_gid, string employee_gid, MdlHierarchyUpdateApplCount values)
        {
            msSQL = " select count(application_gid) incompletestage_count from agr_mst_tapplication a " +
                    " where a.approval_status='Incomplete'";
            values.incompletestage_count = objdbconn.GetExecuteScalar(msSQL);
            int incompletestage_count = Convert.ToInt16(values.incompletestage_count);

            msSQL = " select count(application_gid) businessstage_count from agr_mst_tapplication a " +
                    " where a.approval_status='Submitted to Approval'";
            values.businessstage_count = objdbconn.GetExecuteScalar(msSQL);
            int businessstage_count = Convert.ToInt16(values.businessstage_count);

            msSQL = " select count(application_gid) as creditstage_count from agr_mst_tapplication a " +
                    " where a.approval_status='Submitted to Underwriting' or a.approval_status='Submitted to Credit Approval'" +
                    " or a.approval_status='Sent Back to Credit'";
            values.creditstage_count = objdbconn.GetExecuteScalar(msSQL);
            int creditstage_count = Convert.ToInt16(values.creditstage_count);

            msSQL = " select count(distinct a.application_gid) as ccstage_count from agr_mst_tapplication a " +
                    " where a.approval_status='Submitted to CC' or a.approval_status='Sent Back to CC'";
            values.ccstage_count = objdbconn.GetExecuteScalar(msSQL);
            int ccstage_count = Convert.ToInt16(values.ccstage_count);

            msSQL = " select count(distinct a.application_gid) as cadpendingstage_count from agr_mst_tapplication a " +
                    " where a.approval_status='CC Approved' and a.process_type is null";
            values.cadpendingstage_count = objdbconn.GetExecuteScalar(msSQL);
            int cadpendingstage_count = Convert.ToInt16(values.cadpendingstage_count);

            msSQL = " select count(distinct a.application_gid) as cadacceptedstage_count from agr_mst_tapplication a " +
                    " where a.process_type = 'Accept'";
            values.cadacceptedstage_count = objdbconn.GetExecuteScalar(msSQL);
            int cadacceptedstage_count = Convert.ToInt16(values.cadacceptedstage_count);

            msSQL = " select count(distinct a.application_gid) as productstage_count from agr_mst_tapplication a " +
                    " where a.approval_status in ('Submitted to Product Desk','Submitted to Product Approval','Product Approval - Pending')";
            values.productstage_count = objdbconn.GetExecuteScalar(msSQL);
            int productstage_count = Convert.ToInt16(values.productstage_count);

            int totalcount = incompletestage_count + businessstage_count + creditstage_count + ccstage_count + cadpendingstage_count + cadacceptedstage_count + productstage_count;
            values.lstotalcount = Convert.ToInt16(totalcount);

        }

        public void DaGetapplicationdetails(Mdlapplicationdetials values, string application_gid)
        {
            try
            {
                msSQL = " select application_no,customer_name,vertical_gid,vertical_name,program_gid,program_name, " +
                        " a.created_by, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as rm_name " +
                        " from agr_mst_tapplication a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                        " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    values.program_gid = objODBCDatareader["program_gid"].ToString();
                    values.program_name = objODBCDatareader["program_name"].ToString();
                    values.rm_name = objODBCDatareader["rm_name"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaPostAllHierarchyverticalListSearch(MdlAdminRMMappingview values)
        {
            try
            {
                msSQL = " select distinct c.cluster_name, c.program_name, c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                        " e.region_name,e.employee_name as regionhead,g.zonal_name,g.employee_name as zonalhead ," +
                        " h.employee_name as businesshead from hrm_mst_temployee a" +
                        " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                        " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid " +
                        " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid " +
                        " left join sys_mst_tregionhead e on d.region_gid = e.region_gid  and c.program_gid = e.program_gid " +
                        " left join sys_mst_tzone2region f on f.region_gid = d.region_gid " +
                        " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid  and c.program_gid = g.program_gid " +
                        " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid and c.program_gid = h.program_gid " +
                        " where a.employee_gid = '" + values.employee_gid + "' and " +
                        " c.vertical_gid = '" + values.vertical_gid + "' and" +
                        " e.vertical_gid = '" + values.vertical_gid + "' and " +
                        " g.vertical_gid = '" + values.vertical_gid + "' and " +
                        " h.vertical_gid = '" + values.vertical_gid + "' and " +
                        " c.program_gid = '" + values.program_gid + "' and " +
                        " e.program_gid = '" + values.program_gid + "' and " +
                        " g.program_gid = '" + values.program_gid + "' and " +
                        " h.program_gid = '" + values.program_gid + "' and " +
                        " e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.hierarchyavailable_status = true;
                    values.clusterhead = objODBCDatareader["clusterhead"].ToString();
                    values.regionhead = objODBCDatareader["regionhead"].ToString();
                    values.zonalhead = objODBCDatareader["zonalhead"].ToString();
                    values.businesshead = objODBCDatareader["businesshead"].ToString();
                    values.program_name = objODBCDatareader["program_name"].ToString();
                }
                else
                {
                    values.hierarchyavailable_status = false;

                }
                objODBCDatareader.Close();


                msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as level_zero,b.employee_gid, " +
                        "  concat( g.user_firstname, ' ', g.user_lastname, ' || ', g.user_code) as level_one  " +
                        "  from adm_mst_tmodule2employee a " +
                        "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                        "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                        "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                        "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                     "  (select modulereportingto_gid from adm_mst_tcompany)) " +
                    " and c.user_status = 'Y' and b.employee_gid ='" + values.employee_gid + "' group by a.employee_gid ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.level_zero = objODBCDatareader["level_zero"].ToString();
                    values.level_one = objODBCDatareader["level_one"].ToString();

                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public bool DaPostBusinessHierarchyUpdate(MdlBusinessHierarchyUpdateDtl values, string employee_gid)
        {
            msSQL = " select application_gid,approval_status,vertical_gid,vertical_name,program_gid,program_name,created_by as relationshipmanager_gid, " +
                    " relationshipmanager_name,drm_gid,drm_name,clustermanager_gid,clustermanager_name, " +
                    " zonalhead_gid,zonalhead_name,regionalhead_gid,regionalhead_name,businesshead_gid,businesshead_name " +
                    " from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.approval_status = objODBCDatareader["approval_status"].ToString();
                values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                values.program_gid = objODBCDatareader["program_gid"].ToString();
                values.program_name = objODBCDatareader["program_name"].ToString();
                values.relationshipmanager_gid = objODBCDatareader["relationshipmanager_gid"].ToString();
                values.relationshipmanager_name = objODBCDatareader["relationshipmanager_name"].ToString();
                values.drm_gid = objODBCDatareader["drm_gid"].ToString();
                values.drm_name = objODBCDatareader["drm_name"].ToString();
                values.clustermanager_gid = objODBCDatareader["clustermanager_gid"].ToString();
                values.clustermanager_name = objODBCDatareader["clustermanager_name"].ToString();
                values.zonalhead_gid = objODBCDatareader["zonalhead_gid"].ToString();
                values.zonalhead_name = objODBCDatareader["zonalhead_name"].ToString();
                values.regionalhead_gid = objODBCDatareader["regionalhead_gid"].ToString();
                values.regionalhead_name = objODBCDatareader["regionalhead_name"].ToString();
                values.businesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
                values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
            }

            msSQL = " select distinct c.cluster_name, c.program_name, c.employee_gid as updatedclusterhead_gid, " +
                    " c.employee_name as updatedclusterhead_name, " +
                    " c.vertical_name as clustervertical,e.employee_gid as updatedregionhead_gid," +
                    " e.region_name,e.employee_name as updatedregionhead_name,g.employee_gid as updatedzohalhead_gid,g.zonal_name, " +
                    " g.employee_name as updatedzonalhead_name, h.employee_gid as updatedbusinesshead_gid," +
                    " h.employee_name as updatedbusinesshead_name from hrm_mst_temployee a" +
                    " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                    " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid " +
                    " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid " +
                    " left join sys_mst_tregionhead e on d.region_gid = e.region_gid  and c.program_gid = e.program_gid " +
                    " left join sys_mst_tzone2region f on f.region_gid = d.region_gid " +
                    " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid  and c.program_gid = g.program_gid " +
                    " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid and c.program_gid = h.program_gid " +
                    " where a.employee_gid = '" + values.employee_gid + "' and " +
                    " c.vertical_gid = '" + values.submitvertical_gid + "' and " +
                    " e.vertical_gid = '" + values.submitvertical_gid + "' and " +
                    " g.vertical_gid = '" + values.submitvertical_gid + "' and " +
                    " h.vertical_gid = '" + values.submitvertical_gid + "' and " +
                    " c.program_gid = '" + values.submitprogram_gid + "' and " +
                    " e.program_gid = '" + values.submitprogram_gid + "' and " +
                    " g.program_gid = '" + values.submitprogram_gid + "' and " +
                    " h.program_gid = '" + values.submitprogram_gid + "' and " +
                    " e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.updatedclusterhead_gid = objODBCDatareader["updatedclusterhead_gid"].ToString();
                values.updatedclusterhead_name = objODBCDatareader["updatedclusterhead_name"].ToString();
                values.updatedregionhead_gid = objODBCDatareader["updatedregionhead_gid"].ToString();
                values.updatedregionhead_name = objODBCDatareader["updatedregionhead_name"].ToString();
                values.updatedzohalhead_gid = objODBCDatareader["updatedzohalhead_gid"].ToString();
                values.updatedzonalhead_name = objODBCDatareader["updatedzonalhead_name"].ToString();
                values.updatedbusinesshead_gid = objODBCDatareader["updatedbusinesshead_gid"].ToString();
                values.updatedbusinesshead_name = objODBCDatareader["updatedbusinesshead_name"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as updatedrelationshipmanager_name, " +
                    " b.employee_gid as updatedrelationshipmanager_gid, f.employee_gid as updateddrm_gid, " +
                    "  concat( g.user_firstname, ' ', g.user_lastname, ' || ', g.user_code) as updateddrm_name  " +
                    "  from adm_mst_tmodule2employee a " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                    "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                    "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                    "  (select modulereportingto_gid from adm_mst_tcompany)) " +
                   " and c.user_status = 'Y' and b.employee_gid ='" + values.employee_gid + "' group by a.employee_gid ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.updatedrelationshipmanager_gid = objODBCDatareader["updatedrelationshipmanager_gid"].ToString();
                values.updatedrelationshipmanager_name = objODBCDatareader["updatedrelationshipmanager_name"].ToString();
                values.updateddrm_gid = objODBCDatareader["updateddrm_gid"].ToString();
                values.updateddrm_name = objODBCDatareader["updateddrm_name"].ToString();

            }
            objODBCDatareader.Close();
            if (values.application_stage == "IncompleteStage")
            {
                msGetGid = objcmnfunctions.GetMasterGID("BHUL");
                msSQL = " insert into agr_trn_tbusinesshierarchyupdatelog ( " +
                        " businesshierarchyupdatelog_gid, " +
                        " application_gid, " +
                        " hierarchyupdate_remarks, " +
                        " approval_status, " +
                        " vertical_gid, " +
                        " vertical_name," +
                        " program_gid, " +
                        " program_name, " +
                        " relationshipmanager_gid," +
                        " relationshipmanager_name," +
                        " drm_gid," +
                        " drm_name," +
                        " clustermanager_gid," +
                        " clustermanager_name," +
                        " zonalhead_gid, " +
                        " zonalhead_name, " +
                        " regionalhead_gid, " +
                        " regionalhead_name, " +
                        " businesshead_gid, " +
                        " businesshead_name," +
                        " updatedvertical_gid, " +
                        " updatedvertical_name, " +
                        " updatedprogram_gid," +
                        " updatedprogram_name," +
                        " updatedrelationshipmanager_gid," +
                        " updatedrelationshipmanager_name," +
                        " updateddrm_gid," +
                        " updateddrm_name," +
                        " updatedclustermanager_gid, " +
                        " updatedclustermanager_name, " +
                        " updatedzonalhead_gid, " +
                        " updatedzonalhead_name," +
                        " updatedregionalhead_gid, " +
                        " updatedregionalhead_name, " +
                        " updatedbusinesshead_gid," +
                        " updatedbusinesshead_name," +
                        " application_stage," +
                        " created_by," +
                        " created_date) " +
                        " values (" +
                        "'" + msGetGid + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.hierarchyupdate_remarks + "'," +
                        "'" + values.approval_status + "'," +
                        "'" + values.vertical_gid + "'," +
                        "'" + values.vertical_name + "'," +
                        "'" + values.program_gid + "'," +
                        "'" + values.program_name + "'," +
                        "'" + values.relationshipmanager_gid + "'," +
                        "'" + values.relationshipmanager_name + "'," +
                        "'" + values.drm_gid + "'," +
                        "'" + values.drm_name + "'," +
                        "'" + values.clustermanager_gid + "'," +
                        "'" + values.clustermanager_name + "'," +
                        "'" + values.zonalhead_gid + "'," +
                        "'" + values.zonalhead_name + "'," +
                        "'" + values.regionalhead_gid + "'," +
                        "'" + values.regionalhead_name + "'," +
                        "'" + values.businesshead_gid + "'," +
                        "'" + values.businesshead_name + "'," +
                        "'" + values.submitvertical_gid + "'," +
                        "'" + values.submitvertical_name + "'," +
                        "'" + values.submitprogram_gid + "'," +
                        "'" + values.submitprogram_name + "'," +
                        "'" + values.updatedrelationshipmanager_gid + "'," +
                        "'" + values.updatedrelationshipmanager_name + "'," +
                        "'" + values.updateddrm_gid + "'," +
                        "'" + values.updateddrm_name + "'," +
                        "'" + values.updatedclusterhead_gid + "'," +
                        "'" + values.updatedclusterhead_name + "'," +
                        "'" + values.updatedzohalhead_gid + "'," +
                        "'" + values.updatedzonalhead_name + "'," +
                        "'" + values.updatedregionhead_gid + "'," +
                        "'" + values.updatedregionhead_name + "'," +
                        "'" + values.updatedbusinesshead_gid + "'," +
                        "'" + values.updatedbusinesshead_name + "'," +
                        "'" + values.application_stage + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_tapplication set vertical_gid='" + values.submitvertical_gid + "', vertical_name='" + values.submitvertical_name + "'," +
                           " program_gid='" + values.submitprogram_gid + "', program_name='" + values.submitprogram_name + "'," +
                           " relationshipmanager_gid='" + values.updatedrelationshipmanager_gid + "', relationshipmanager_name='" + values.updatedrelationshipmanager_name + "'," +
                           " created_by= '" + values.updatedrelationshipmanager_gid + "', drm_gid ='" + values.updateddrm_gid + "'," +
                           " drm_name= '" + values.updateddrm_name + "', clustermanager_gid ='" + values.updatedclusterhead_gid + "'," +
                           " clustermanager_name= '" + values.updatedclusterhead_name + "', zonalhead_gid ='" + values.updatedzohalhead_gid + "'," +
                           " zonalhead_name= '" + values.updatedzonalhead_name + "', regionalhead_gid ='" + values.updatedregionhead_gid + "'," +
                           " regionalhead_name= '" + values.updatedregionhead_name + "', businesshead_gid ='" + values.updatedbusinesshead_gid + "'," +
                           " businesshead_name= '" + values.updatedbusinesshead_name + "'" +
                           " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Business Hierarchy Updated Successfully..!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred..!";
                    return false;
                }
            }
            else if (values.application_stage == "BusinessStage")
            {
                msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGidapprovallog = objcmnfunctions.GetMasterGID("BAHL");
                        msSQL = " insert into agr_trn_tbusinessapprovalhierarchylog " +
                                " (businessapprovalhierarchylog_gid,applicationapproval_gid, application_gid, approval_gid, approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag) " +
                                " select @businessapprovalhierarchylog_gid := '" + msGetGidapprovallog + "', applicationapproval_gid,application_gid, approval_gid,approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag " +
                                " from agr_trn_tapplicationapproval " +
                                " where application_gid='" + values.application_gid + "'" +
                                " and applicationapproval_gid='" + dt["applicationapproval_gid"].ToString() + "'";
                        mnResulthierarchyupdateapprovallog = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {

                }

                if (mnResulthierarchyupdateapprovallog != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BHUL");
                    msSQL = " insert into agr_trn_tbusinesshierarchyupdatelog ( " +
                            " businesshierarchyupdatelog_gid, " +
                            " application_gid, " +
                            " hierarchyupdate_remarks, " +
                            " approval_status, " +
                            " vertical_gid, " +
                            " vertical_name," +
                            " program_gid, " +
                            " program_name, " +
                            " relationshipmanager_gid," +
                            " relationshipmanager_name," +
                            " drm_gid," +
                            " drm_name," +
                            " clustermanager_gid," +
                            " clustermanager_name," +
                            " zonalhead_gid, " +
                            " zonalhead_name, " +
                            " regionalhead_gid, " +
                            " regionalhead_name, " +
                            " businesshead_gid, " +
                            " businesshead_name," +
                            " updatedvertical_gid, " +
                            " updatedvertical_name, " +
                            " updatedprogram_gid," +
                            " updatedprogram_name," +
                            " updatedrelationshipmanager_gid," +
                            " updatedrelationshipmanager_name," +
                            " updateddrm_gid," +
                            " updateddrm_name," +
                            " updatedclustermanager_gid, " +
                            " updatedclustermanager_name, " +
                            " updatedzonalhead_gid, " +
                            " updatedzonalhead_name," +
                            " updatedregionalhead_gid, " +
                            " updatedregionalhead_name, " +
                            " updatedbusinesshead_gid," +
                            " updatedbusinesshead_name," +
                            " application_stage," +
                            " created_by," +
                            " created_date) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.hierarchyupdate_remarks + "'," +
                            "'" + values.approval_status + "'," +
                            "'" + values.vertical_gid + "'," +
                            "'" + values.vertical_name + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program_name + "'," +
                            "'" + values.relationshipmanager_gid + "'," +
                            "'" + values.relationshipmanager_name + "'," +
                            "'" + values.drm_gid + "'," +
                            "'" + values.drm_name + "'," +
                            "'" + values.clustermanager_gid + "'," +
                            "'" + values.clustermanager_name + "'," +
                            "'" + values.zonalhead_gid + "'," +
                            "'" + values.zonalhead_name + "'," +
                            "'" + values.regionalhead_gid + "'," +
                            "'" + values.regionalhead_name + "'," +
                            "'" + values.businesshead_gid + "'," +
                            "'" + values.businesshead_name + "'," +
                            "'" + values.submitvertical_gid + "'," +
                            "'" + values.submitvertical_name + "'," +
                            "'" + values.submitprogram_gid + "'," +
                            "'" + values.submitprogram_name + "'," +
                            "'" + values.updatedrelationshipmanager_gid + "'," +
                            "'" + values.updatedrelationshipmanager_name + "'," +
                            "'" + values.updateddrm_gid + "'," +
                            "'" + values.updateddrm_name + "'," +
                            "'" + values.updatedclusterhead_gid + "'," +
                            "'" + values.updatedclusterhead_name + "'," +
                            "'" + values.updatedzohalhead_gid + "'," +
                            "'" + values.updatedzonalhead_name + "'," +
                            "'" + values.updatedregionhead_gid + "'," +
                            "'" + values.updatedregionhead_name + "'," +
                            "'" + values.updatedbusinesshead_gid + "'," +
                            "'" + values.updatedbusinesshead_name + "'," +
                            "'" + values.application_stage + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select applicationapproval_gid,hierary_level,(hierary_level-1) as hierarylevel_count " +
                        " from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.applicationapproval_gid = objODBCDatareader["applicationapproval_gid"].ToString();
                    values.hierary_level = objODBCDatareader["hierary_level"].ToString();
                    values.hierarylevel_count = objODBCDatareader["hierarylevel_count"].ToString();
                }
                objODBCDatareader.Close();

                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_tapplication set approval_status='Submitted to Approval', headapproval_status='Pending',headapproval_date=null" +
                            " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_status='Pending',approval_remarks=null,approved_date=null " +
                            " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updateddrm_gid + "', approval_name='" + values.updateddrm_name + "'," +
                            " initiate_flag='Y'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='1'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedclusterhead_gid + "', approval_name='" + values.updatedclusterhead_name + "'," +
                            " initiate_flag='N'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='2'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedregionhead_gid + "', approval_name='" + values.updatedregionhead_name + "'," +
                            " initiate_flag='N'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='3'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedzohalhead_gid + "', approval_name='" + values.updatedzonalhead_name + "'," +
                            " initiate_flag='N'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='4'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedbusinesshead_gid + "', approval_name='" + values.updatedbusinesshead_name + "'," +
                            " initiate_flag='N'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='5'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tapplication set vertical_gid='" + values.submitvertical_gid + "', vertical_name='" + values.submitvertical_name + "'," +
                           " program_gid='" + values.submitprogram_gid + "', program_name='" + values.submitprogram_name + "'," +
                           " relationshipmanager_gid='" + values.updatedrelationshipmanager_gid + "', relationshipmanager_name='" + values.updatedrelationshipmanager_name + "'," +
                           " created_by= '" + values.updatedrelationshipmanager_gid + "', drm_gid ='" + values.updateddrm_gid + "'," +
                           " drm_name= '" + values.updateddrm_name + "', clustermanager_gid ='" + values.updatedclusterhead_gid + "'," +
                           " clustermanager_name= '" + values.updatedclusterhead_name + "', zonalhead_gid ='" + values.updatedzohalhead_gid + "'," +
                           " zonalhead_name= '" + values.updatedzonalhead_name + "', regionalhead_gid ='" + values.updatedregionhead_gid + "'," +
                           " regionalhead_name= '" + values.updatedregionhead_name + "', businesshead_gid ='" + values.updatedbusinesshead_gid + "'," +
                           " businesshead_name= '" + values.updatedbusinesshead_name + "'" +
                           " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Business Hierarchy Updated Successfully..!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred..!";
                    return false;
                }
            }
            else if (values.application_stage == "CadAcceptedStage")
            {
                msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGidapprovallog = objcmnfunctions.GetMasterGID("BAHL");
                        msSQL = " insert into agr_trn_tbusinessapprovalhierarchylog " +
                                " (businessapprovalhierarchylog_gid,applicationapproval_gid, application_gid, approval_gid, approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag) " +
                                " select @businessapprovalhierarchylog_gid := '" + msGetGidapprovallog + "', applicationapproval_gid,application_gid, approval_gid,approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag " +
                                " from agr_trn_tapplicationapproval " +
                                " where application_gid='" + values.application_gid + "'" +
                                " and applicationapproval_gid='" + dt["applicationapproval_gid"].ToString() + "'";
                        mnResulthierarchyupdateapprovallog = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {

                }

                if (mnResulthierarchyupdateapprovallog != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BHUL");
                    msSQL = " insert into agr_trn_tbusinesshierarchyupdatelog ( " +
                            " businesshierarchyupdatelog_gid, " +
                            " application_gid, " +
                            " hierarchyupdate_remarks, " +
                            " approval_status, " +
                            " vertical_gid, " +
                            " vertical_name," +
                            " program_gid, " +
                            " program_name, " +
                            " relationshipmanager_gid," +
                            " relationshipmanager_name," +
                            " drm_gid," +
                            " drm_name," +
                            " clustermanager_gid," +
                            " clustermanager_name," +
                            " zonalhead_gid, " +
                            " zonalhead_name, " +
                            " regionalhead_gid, " +
                            " regionalhead_name, " +
                            " businesshead_gid, " +
                            " businesshead_name," +
                            " updatedvertical_gid, " +
                            " updatedvertical_name, " +
                            " updatedprogram_gid," +
                            " updatedprogram_name," +
                            " updatedrelationshipmanager_gid," +
                            " updatedrelationshipmanager_name," +
                            " updateddrm_gid," +
                            " updateddrm_name," +
                            " updatedclustermanager_gid, " +
                            " updatedclustermanager_name, " +
                            " updatedzonalhead_gid, " +
                            " updatedzonalhead_name," +
                            " updatedregionalhead_gid, " +
                            " updatedregionalhead_name, " +
                            " updatedbusinesshead_gid," +
                            " updatedbusinesshead_name," +
                            " application_stage," +
                            " created_by," +
                            " created_date) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.hierarchyupdate_remarks + "'," +
                            "'" + values.approval_status + "'," +
                            "'" + values.vertical_gid + "'," +
                            "'" + values.vertical_name + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program_name + "'," +
                            "'" + values.relationshipmanager_gid + "'," +
                            "'" + values.relationshipmanager_name + "'," +
                            "'" + values.drm_gid + "'," +
                            "'" + values.drm_name + "'," +
                            "'" + values.clustermanager_gid + "'," +
                            "'" + values.clustermanager_name + "'," +
                            "'" + values.zonalhead_gid + "'," +
                            "'" + values.zonalhead_name + "'," +
                            "'" + values.regionalhead_gid + "'," +
                            "'" + values.regionalhead_name + "'," +
                            "'" + values.businesshead_gid + "'," +
                            "'" + values.businesshead_name + "'," +
                             "'" + values.submitvertical_gid + "'," +
                            "'" + values.submitvertical_name + "'," +
                            "'" + values.submitprogram_gid + "'," +
                            "'" + values.submitprogram_name + "'," +
                            "'" + values.updatedrelationshipmanager_gid + "'," +
                            "'" + values.updatedrelationshipmanager_name + "'," +
                            "'" + values.updateddrm_gid + "'," +
                            "'" + values.updateddrm_name + "'," +
                            "'" + values.updatedclusterhead_gid + "'," +
                            "'" + values.updatedclusterhead_name + "'," +
                            "'" + values.updatedzohalhead_gid + "'," +
                            "'" + values.updatedzonalhead_name + "'," +
                            "'" + values.updatedregionhead_gid + "'," +
                            "'" + values.updatedregionhead_name + "'," +
                            "'" + values.updatedbusinesshead_gid + "'," +
                            "'" + values.updatedbusinesshead_name + "'," +
                            "'" + values.application_stage + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updateddrm_gid + "', approval_name='" + values.updateddrm_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='1'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedclusterhead_gid + "', approval_name='" + values.updatedclusterhead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='2'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedregionhead_gid + "', approval_name='" + values.updatedregionhead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='3'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedzohalhead_gid + "', approval_name='" + values.updatedzonalhead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='4'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedbusinesshead_gid + "', approval_name='" + values.updatedbusinesshead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='5'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tapplication set vertical_gid='" + values.submitvertical_gid + "', vertical_name='" + values.submitvertical_name + "'," +
                            " program_gid='" + values.submitprogram_gid + "', program_name='" + values.submitprogram_name + "'," +
                            " relationshipmanager_gid='" + values.updatedrelationshipmanager_gid + "', relationshipmanager_name='" + values.updatedrelationshipmanager_name + "'," +
                            " created_by= '" + values.updatedrelationshipmanager_gid + "', drm_gid ='" + values.updateddrm_gid + "'," +
                            " drm_name= '" + values.updateddrm_name + "', clustermanager_gid ='" + values.updatedclusterhead_gid + "'," +
                            " clustermanager_name= '" + values.updatedclusterhead_name + "', zonalhead_gid ='" + values.updatedzohalhead_gid + "'," +
                            " zonalhead_name= '" + values.updatedzonalhead_name + "', regionalhead_gid ='" + values.updatedregionhead_gid + "'," +
                            " regionalhead_name= '" + values.updatedregionhead_name + "', businesshead_gid ='" + values.updatedbusinesshead_gid + "'," +
                            " businesshead_name= '" + values.updatedbusinesshead_name + "'" +
                            " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tapplication set vertical_gid='" + values.submitvertical_gid + "', vertical_name='" + values.submitvertical_name + "'," +
                            " program_gid='" + values.submitprogram_gid + "', program_name='" + values.submitprogram_name + "'," +
                            " relationshipmanager_gid='" + values.updatedrelationshipmanager_gid + "', relationshipmanager_name='" + values.updatedrelationshipmanager_name + "'," +
                            " created_by= '" + values.updatedrelationshipmanager_gid + "', drm_gid ='" + values.updateddrm_gid + "'," +
                            " drm_name= '" + values.updateddrm_name + "', clustermanager_gid ='" + values.updatedclusterhead_gid + "'," +
                            " clustermanager_name= '" + values.updatedclusterhead_name + "', zonalhead_gid ='" + values.updatedzohalhead_gid + "'," +
                            " zonalhead_name= '" + values.updatedzonalhead_name + "', regionalhead_gid ='" + values.updatedregionhead_gid + "'," +
                            " regionalhead_name= '" + values.updatedregionhead_name + "', businesshead_gid ='" + values.updatedbusinesshead_gid + "'," +
                            " businesshead_name= '" + values.updatedbusinesshead_name + "'" +
                            " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Business Hierarchy Updated Successfully..!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred..!";
                    return false;
                }
            }
            else if (values.application_stage == "ProductStage")
            {
                msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGidapprovallog = objcmnfunctions.GetMasterGID("BAHL");
                        msSQL = " insert into agr_trn_tbusinessapprovalhierarchylog " +
                                " (businessapprovalhierarchylog_gid,applicationapproval_gid, application_gid, approval_gid, approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag) " +
                                " select @businessapprovalhierarchylog_gid := '" + msGetGidapprovallog + "', applicationapproval_gid,application_gid, approval_gid,approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag " +
                                " from agr_trn_tapplicationapproval " +
                                " where application_gid='" + values.application_gid + "'" +
                                " and applicationapproval_gid='" + dt["applicationapproval_gid"].ToString() + "'";
                        mnResulthierarchyupdateapprovallog = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {

                }

                if (mnResulthierarchyupdateapprovallog != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BHUL");
                    msSQL = " insert into agr_trn_tbusinesshierarchyupdatelog ( " +
                            " businesshierarchyupdatelog_gid, " +
                            " application_gid, " +
                            " hierarchyupdate_remarks, " +
                            " approval_status, " +
                            " vertical_gid, " +
                            " vertical_name," +
                            " program_gid, " +
                            " program_name, " +
                            " relationshipmanager_gid," +
                            " relationshipmanager_name," +
                            " drm_gid," +
                            " drm_name," +
                            " clustermanager_gid," +
                            " clustermanager_name," +
                            " zonalhead_gid, " +
                            " zonalhead_name, " +
                            " regionalhead_gid, " +
                            " regionalhead_name, " +
                            " businesshead_gid, " +
                            " businesshead_name," +
                            " updatedvertical_gid, " +
                            " updatedvertical_name, " +
                            " updatedprogram_gid," +
                            " updatedprogram_name," +
                            " updatedrelationshipmanager_gid," +
                            " updatedrelationshipmanager_name," +
                            " updateddrm_gid," +
                            " updateddrm_name," +
                            " updatedclustermanager_gid, " +
                            " updatedclustermanager_name, " +
                            " updatedzonalhead_gid, " +
                            " updatedzonalhead_name," +
                            " updatedregionalhead_gid, " +
                            " updatedregionalhead_name, " +
                            " updatedbusinesshead_gid," +
                            " updatedbusinesshead_name," +
                            " application_stage," +
                            " created_by," +
                            " created_date) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.hierarchyupdate_remarks.Replace("'", "\\'") + "'," +
                            "'" + values.approval_status + "'," +
                            "'" + values.vertical_gid + "'," +
                            "'" + values.vertical_name + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program_name + "'," +
                            "'" + values.relationshipmanager_gid + "'," +
                            "'" + values.relationshipmanager_name + "'," +
                            "'" + values.drm_gid + "'," +
                            "'" + values.drm_name + "'," +
                            "'" + values.clustermanager_gid + "'," +
                            "'" + values.clustermanager_name + "'," +
                            "'" + values.zonalhead_gid + "'," +
                            "'" + values.zonalhead_name + "'," +
                            "'" + values.regionalhead_gid + "'," +
                            "'" + values.regionalhead_name + "'," +
                            "'" + values.businesshead_gid + "'," +
                            "'" + values.businesshead_name + "'," +
                            "'" + values.submitvertical_gid + "'," +
                            "'" + values.submitvertical_name + "'," +
                            "'" + values.submitprogram_gid + "'," +
                            "'" + values.submitprogram_name + "'," +
                            "'" + values.updatedrelationshipmanager_gid + "'," +
                            "'" + values.updatedrelationshipmanager_name + "'," +
                            "'" + values.updateddrm_gid + "'," +
                            "'" + values.updateddrm_name + "'," +
                            "'" + values.updatedclusterhead_gid + "'," +
                            "'" + values.updatedclusterhead_name + "'," +
                            "'" + values.updatedzohalhead_gid + "'," +
                            "'" + values.updatedzonalhead_name + "'," +
                            "'" + values.updatedregionhead_gid + "'," +
                            "'" + values.updatedregionhead_name + "'," +
                            "'" + values.updatedbusinesshead_gid + "'," +
                            "'" + values.updatedbusinesshead_name + "'," +
                            "'" + values.application_stage + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select applicationapproval_gid,hierary_level,(hierary_level-1) as hierarylevel_count " +
                        " from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.applicationapproval_gid = objODBCDatareader["applicationapproval_gid"].ToString();
                    values.hierary_level = objODBCDatareader["hierary_level"].ToString();
                    values.hierarylevel_count = objODBCDatareader["hierarylevel_count"].ToString();
                }
                objODBCDatareader.Close();

                if (mnResult != 0)
                {
                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updateddrm_gid + "', approval_name='" + values.updateddrm_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='1'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedclusterhead_gid + "', approval_name='" + values.updatedclusterhead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='2'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedregionhead_gid + "', approval_name='" + values.updatedregionhead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='3'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedzohalhead_gid + "', approval_name='" + values.updatedzonalhead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='4'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedbusinesshead_gid + "', approval_name='" + values.updatedbusinesshead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='5'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    

                    msSQL = " update agr_mst_tapplication set vertical_gid='" + values.submitvertical_gid + "', vertical_name='" + values.submitvertical_name + "'," +
                            " program_gid='" + values.submitprogram_gid + "', program_name='" + values.submitprogram_name + "'," +
                            " relationshipmanager_gid='" + values.updatedrelationshipmanager_gid + "', relationshipmanager_name='" + values.updatedrelationshipmanager_name + "'," +
                            " created_by= '" + values.updatedrelationshipmanager_gid + "',submitted_by = '" + values.updatedrelationshipmanager_gid + "', drm_gid ='" + values.updateddrm_gid + "'," +
                            " drm_name= '" + values.updateddrm_name + "', clustermanager_gid ='" + values.updatedclusterhead_gid + "'," +
                            " clustermanager_name= '" + values.updatedclusterhead_name + "', zonalhead_gid ='" + values.updatedzohalhead_gid + "'," +
                            " zonalhead_name= '" + values.updatedzonalhead_name + "', regionalhead_gid ='" + values.updatedregionhead_gid + "'," +
                            " regionalhead_name= '" + values.updatedregionhead_name + "', businesshead_gid ='" + values.updatedbusinesshead_gid + "'," +
                            " businesshead_name= '" + values.updatedbusinesshead_name + "'" +
                            " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Business Hierarchy Updated Successfully..!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred..!";
                    return false;
                }
            }
            else
            {
                msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval where application_gid='" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGidapprovallog = objcmnfunctions.GetMasterGID("BAHL");
                        msSQL = " insert into agr_trn_tbusinessapprovalhierarchylog " +
                                " (businessapprovalhierarchylog_gid,applicationapproval_gid, application_gid, approval_gid, approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag) " +
                                " select @businessapprovalhierarchylog_gid := '" + msGetGidapprovallog + "', applicationapproval_gid,application_gid, approval_gid,approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag " +
                                " from agr_trn_tapplicationapproval " +
                                " where application_gid='" + values.application_gid + "'" +
                                " and applicationapproval_gid='" + dt["applicationapproval_gid"].ToString() + "'";
                        mnResulthierarchyupdateapprovallog = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {

                }

                if (mnResulthierarchyupdateapprovallog != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BHUL");
                    msSQL = " insert into agr_trn_tbusinesshierarchyupdatelog ( " +
                            " businesshierarchyupdatelog_gid, " +
                            " application_gid, " +
                            " hierarchyupdate_remarks, " +
                            " approval_status, " +
                            " vertical_gid, " +
                            " vertical_name," +
                            " program_gid, " +
                            " program_name, " +
                            " relationshipmanager_gid," +
                            " relationshipmanager_name," +
                            " drm_gid," +
                            " drm_name," +
                            " clustermanager_gid," +
                            " clustermanager_name," +
                            " zonalhead_gid, " +
                            " zonalhead_name, " +
                            " regionalhead_gid, " +
                            " regionalhead_name, " +
                            " businesshead_gid, " +
                            " businesshead_name," +
                            " updatedvertical_gid, " +
                            " updatedvertical_name, " +
                            " updatedprogram_gid," +
                            " updatedprogram_name," +
                            " updatedrelationshipmanager_gid," +
                            " updatedrelationshipmanager_name," +
                            " updateddrm_gid," +
                            " updateddrm_name," +
                            " updatedclustermanager_gid, " +
                            " updatedclustermanager_name, " +
                            " updatedzonalhead_gid, " +
                            " updatedzonalhead_name," +
                            " updatedregionalhead_gid, " +
                            " updatedregionalhead_name, " +
                            " updatedbusinesshead_gid," +
                            " updatedbusinesshead_name," +
                            " application_stage," +
                            " created_by," +
                            " created_date) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.hierarchyupdate_remarks + "'," +
                            "'" + values.approval_status + "'," +
                            "'" + values.vertical_gid + "'," +
                            "'" + values.vertical_name + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program_name + "'," +
                            "'" + values.relationshipmanager_gid + "'," +
                            "'" + values.relationshipmanager_name + "'," +
                            "'" + values.drm_gid + "'," +
                            "'" + values.drm_name + "'," +
                            "'" + values.clustermanager_gid + "'," +
                            "'" + values.clustermanager_name + "'," +
                            "'" + values.zonalhead_gid + "'," +
                            "'" + values.zonalhead_name + "'," +
                            "'" + values.regionalhead_gid + "'," +
                            "'" + values.regionalhead_name + "'," +
                            "'" + values.businesshead_gid + "'," +
                            "'" + values.businesshead_name + "'," +
                            "'" + values.submitvertical_gid + "'," +
                            "'" + values.submitvertical_name + "'," +
                            "'" + values.submitprogram_gid + "'," +
                            "'" + values.submitprogram_name + "'," +
                            "'" + values.updatedrelationshipmanager_gid + "'," +
                            "'" + values.updatedrelationshipmanager_name + "'," +
                            "'" + values.updateddrm_gid + "'," +
                            "'" + values.updateddrm_name + "'," +
                            "'" + values.updatedclusterhead_gid + "'," +
                            "'" + values.updatedclusterhead_name + "'," +
                            "'" + values.updatedzohalhead_gid + "'," +
                            "'" + values.updatedzonalhead_name + "'," +
                            "'" + values.updatedregionhead_gid + "'," +
                            "'" + values.updatedregionhead_name + "'," +
                            "'" + values.updatedbusinesshead_gid + "'," +
                            "'" + values.updatedbusinesshead_name + "'," +
                            "'" + values.application_stage + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updateddrm_gid + "', approval_name='" + values.updateddrm_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='1'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedclusterhead_gid + "', approval_name='" + values.updatedclusterhead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='2'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedregionhead_gid + "', approval_name='" + values.updatedregionhead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='3'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedzohalhead_gid + "', approval_name='" + values.updatedzonalhead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='4'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tapplicationapproval set approval_gid='" + values.updatedbusinesshead_gid + "', approval_name='" + values.updatedbusinesshead_name + "'" +
                            " where application_gid='" + values.application_gid + "' and hierary_level='5'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tapplication set vertical_gid='" + values.submitvertical_gid + "', vertical_name='" + values.submitvertical_name + "'," +
                            " program_gid='" + values.submitprogram_gid + "', program_name='" + values.submitprogram_name + "'," +
                            " relationshipmanager_gid='" + values.updatedrelationshipmanager_gid + "', relationshipmanager_name='" + values.updatedrelationshipmanager_name + "'," +
                            " created_by= '" + values.updatedrelationshipmanager_gid + "', drm_gid ='" + values.updateddrm_gid + "'," +
                            " drm_name= '" + values.updateddrm_name + "', clustermanager_gid ='" + values.updatedclusterhead_gid + "'," +
                            " clustermanager_name= '" + values.updatedclusterhead_name + "', zonalhead_gid ='" + values.updatedzohalhead_gid + "'," +
                            " zonalhead_name= '" + values.updatedzonalhead_name + "', regionalhead_gid ='" + values.updatedregionhead_gid + "'," +
                            " regionalhead_name= '" + values.updatedregionhead_name + "', businesshead_gid ='" + values.updatedbusinesshead_gid + "'," +
                            " businesshead_name= '" + values.updatedbusinesshead_name + "'" +
                            " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Business Hierarchy Updated Successfully..!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred..!";
                    return false;
                }

            }
        }

        public void DaGetHierarchyUpdateHistoryLog(string application_gid, MdlHierarchyUpdateHistoryLog values)
        {
            msSQL = " select a.businesshierarchyupdatelog_gid,a.vertical_gid ,a.vertical_name,a.program_gid,a.program_name, " +
                    " a.relationshipmanager_gid,a.relationshipmanager_name,a.drm_gid,a.drm_name, " +
                    " a.clustermanager_gid ,a.clustermanager_name,a.zonalhead_gid,a.zonalhead_name, " +
                    " a.regionalhead_gid,a.regionalhead_name,a.businesshead_gid,a.businesshead_name, " +
                    " a.updatedvertical_gid ,a.updatedvertical_name,a.updatedprogram_gid,a.updatedprogram_name, " +
                    " a.updatedrelationshipmanager_gid,a.updatedrelationshipmanager_name,a.updateddrm_gid,a.updateddrm_name, " +
                    " a.updatedclustermanager_gid ,a.updatedclustermanager_name,a.updatedzonalhead_gid,a.updatedzonalhead_name, " +
                    " a.updatedregionalhead_gid,a.updatedregionalhead_name,a.updatedbusinesshead_gid,a.updatedbusinesshead_name, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                    " from agr_trn_tbusinesshierarchyupdatelog a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where a.application_gid = '" + application_gid + "' order by a.created_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gethierarchyupdatedhistory_list = new List<hierarchyupdatedhistory_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gethierarchyupdatedhistory_list.Add(new hierarchyupdatedhistory_list
                    {
                        businesshierarchyupdatelog_gid = (dr_datarow["businesshierarchyupdatelog_gid"].ToString()),
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                        program_gid = (dr_datarow["program_gid"].ToString()),
                        program_name = (dr_datarow["program_name"].ToString()),
                        relationshipmanager_gid = (dr_datarow["relationshipmanager_gid"].ToString()),
                        relationshipmanager_name = (dr_datarow["relationshipmanager_name"].ToString()),
                        drm_gid = (dr_datarow["drm_gid"].ToString()),
                        drm_name = (dr_datarow["drm_name"].ToString()),
                        clustermanager_gid = (dr_datarow["clustermanager_gid"].ToString()),
                        clustermanager_name = (dr_datarow["clustermanager_name"].ToString()),
                        zonalhead_gid = (dr_datarow["zonalhead_gid"].ToString()),
                        zonalhead_name = (dr_datarow["zonalhead_name"].ToString()),
                        regionalhead_gid = (dr_datarow["regionalhead_gid"].ToString()),
                        regionalhead_name = (dr_datarow["regionalhead_name"].ToString()),
                        businesshead_gid = (dr_datarow["businesshead_gid"].ToString()),
                        businesshead_name = (dr_datarow["businesshead_name"].ToString()),
                        updatedvertical_gid = (dr_datarow["updatedvertical_gid"].ToString()),
                        updatedvertical_name = (dr_datarow["updatedvertical_name"].ToString()),
                        updatedprogram_gid = (dr_datarow["updatedprogram_gid"].ToString()),
                        updatedprogram_name = (dr_datarow["updatedprogram_name"].ToString()),
                        updatedrelationshipmanager_gid = (dr_datarow["updatedrelationshipmanager_gid"].ToString()),
                        updatedrelationshipmanager_name = (dr_datarow["updatedrelationshipmanager_name"].ToString()),
                        updateddrm_gid = (dr_datarow["updateddrm_gid"].ToString()),
                        updateddrm_name = (dr_datarow["updateddrm_name"].ToString()),
                        updatedclustermanager_gid = (dr_datarow["updatedclustermanager_gid"].ToString()),
                        updatedclustermanager_name = (dr_datarow["updatedclustermanager_name"].ToString()),
                        updatedzonalhead_gid = (dr_datarow["updatedzonalhead_gid"].ToString()),
                        updatedzonalhead_name = (dr_datarow["updatedzonalhead_name"].ToString()),
                        updatedregionalhead_gid = (dr_datarow["updatedregionalhead_gid"].ToString()),
                        updatedregionalhead_name = (dr_datarow["updatedregionalhead_name"].ToString()),
                        updatedbusinesshead_gid = (dr_datarow["updatedbusinesshead_gid"].ToString()),
                        updatedbusinesshead_name = (dr_datarow["updatedbusinesshead_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())
                    });
                }
                values.hierarchyupdatedhistory_list = gethierarchyupdatedhistory_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetIncompleteStageSummary(string employee_gid, MdlBusinessHierarchyUpdate values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,a.vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,a.program_gid,a.program_name, " +
                    " case when (d.application_gid is not null) then 'Y' else 'N' end as hierarchyupdated_flag " +
                    " from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tbusinesshierarchyupdatelog d on d.application_gid=a.application_gid " +
                    " where a.approval_status='Incomplete' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getincompletestage_list = new List<incompletestage_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getincompletestage_list.Add(new incompletestage_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        program_gid = dt["program_gid"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        hierarchyupdated_flag = dt["hierarchyupdated_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString(),
                    });

                }
            }
            values.incompletestage_list = getincompletestage_list;
            dt_datatable.Dispose();
        }

        public void DaGetHierarchyUpdateRemarks(MdlHierarchyUpdateRemarks values, string businesshierarchyupdatelog_gid)
        {
            try
            {
                msSQL = " select businesshierarchyupdatelog_gid,hierarchyupdate_remarks " +
                        " from agr_trn_tbusinesshierarchyupdatelog a " +
                        " where businesshierarchyupdatelog_gid = '" + businesshierarchyupdatelog_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.businesshierarchyupdatelog_gid = objODBCDatareader["businesshierarchyupdatelog_gid"].ToString();
                    values.hierarchyupdate_remarks = objODBCDatareader["hierarchyupdate_remarks"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetProductRejectedApplSummary(string employee_gid, MdlCreditRejectedAppl values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.creditgroup_gid,a.creditgroup_name, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date, " +
                    " case when ((d.application_gid is not null) or (e.application_gid is not null)) then 'Y' else 'N' end as history_flag " +
                    " from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappproductrevokeapprovallog d on d.application_gid = a.application_gid " +
                    " left join agr_trn_tproductrevokelog e on e.application_gid = a.application_gid " +
                    " where a.approval_status='Product Approval - Rejected' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditrejectedappl_list = new List<creditrejectedappl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditrejectedappl_list.Add(new creditrejectedappl_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        history_flag = dt["history_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString()
                    });

                }
            }
            values.creditrejectedappl_list = getcreditrejectedappl_list;
            dt_datatable.Dispose();
        }

        public void DaGetProductHoldApplSummary(string employee_gid, MdlCreditRejectedAppl values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.creditgroup_gid,a.creditgroup_name, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date, " +
                    " case when ((d.application_gid is not null) or (e.application_gid is not null)) then 'Y' else 'N' end as history_flag " +
                    " from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappproductrevokeapprovallog d on d.application_gid = a.application_gid " +
                    " left join agr_trn_tproductrevokelog e on e.application_gid = a.application_gid " +
                    " where a.approval_status='Product Approval - Hold' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditrejectedappl_list = new List<creditrejectedappl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditrejectedappl_list.Add(new creditrejectedappl_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        history_flag = dt["history_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString()
                    });

                }
            }
            values.creditrejectedappl_list = getcreditrejectedappl_list;
            dt_datatable.Dispose();
        }

        public void DaGetProductStageSummary(string employee_gid, MdlBusinessHierarchyUpdate values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,a.vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.created_by as created_gid, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date,a.program_gid,a.program_name, " +
                    " case when (d.application_gid is not null) then 'Y' else 'N' end as hierarchyupdated_flag " +
                    " from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tbusinesshierarchyupdatelog d on d.application_gid=a.application_gid " +
                    " where a.approval_status in ('Submitted to Product Desk','Submitted to Product Approval','Product Approval - Pending') group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getincompletestage_list = new List<incompletestage_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getincompletestage_list.Add(new incompletestage_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        program_gid = dt["program_gid"].ToString(),
                        program_name = dt["program_name"].ToString(),
                        hierarchyupdated_flag = dt["hierarchyupdated_flag"].ToString(),
                        created_gid = dt["created_gid"].ToString(),
                    });

                }
            }
            values.incompletestage_list = getincompletestage_list;
            dt_datatable.Dispose();
        }

        public bool DaPostProjectRevokeApplication(Mdlcreditrevoke values, string user_gid, string employee_gid)
        {
            msSQL = "select approval_status from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.approval_status = objODBCDatareader["approval_status"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = "select productmember_approvalflag,productmanager_approvalflag from agr_trn_tappproductapproval where application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.productmember_approvalflag = objODBCDatareader["productmember_approvalflag"].ToString();
                values.productmanager_approvalflag = objODBCDatareader["productmanager_approvalflag"].ToString();
            }
            objODBCDatareader.Close();

            if (values.approval_status == "Product Approval - Rejected")
            {
                if (values.productmember_approvalflag == "R") {
                    msSQL = " select appproductapproval_gid," +
                            " product_membergid as rejected_by,productmember_approvaldate as rejected_date,assign_remarks as credit_remarks" +
                            " from agr_trn_tappproductapproval where application_gid='" + values.application_gid + "'" +
                            " and productmember_approvalflag = 'R'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.appproductapproval_gid = objODBCDatareader["appproductapproval_gid"].ToString();
                        
                        values.rejected_by = objODBCDatareader["rejected_by"].ToString();
                        values.rejected_date = objODBCDatareader["rejected_date"].ToString();
                        values.credit_remarks = objODBCDatareader["credit_remarks"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                if (values.productmanager_approvalflag == "R") {
                    msSQL = " select appproductapproval_gid, " +
                            " product_managergid as rejected_by,productmanager_approvaldate as rejected_date,productmanager_approvalremarks as credit_remarks" +
                            " from agr_trn_tappproductapproval where application_gid='" + values.application_gid + "'" +
                            " and productmanager_approvalflag ='R'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.appproductapproval_gid = objODBCDatareader["appproductapproval_gid"].ToString();
                        
                        values.rejected_by = objODBCDatareader["rejected_by"].ToString();
                        values.rejected_date = objODBCDatareader["rejected_date"].ToString();
                        values.credit_remarks = objODBCDatareader["credit_remarks"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                
            }
            else
            {
                if (values.productmember_approvalflag == "H") {
                    msSQL = " select appproductapproval_gid," +
                            " product_membergid as hold_by,productmember_approvaldate as hold_date,assign_remarks as credit_remarks" +
                            " from agr_trn_tappproductapproval where application_gid='" + values.application_gid + "' " +
                            " and productmember_approvalflag = 'H'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.appproductapproval_gid = objODBCDatareader["appproductapproval_gid"].ToString();
                        
                        values.hold_by = objODBCDatareader["hold_by"].ToString();
                        values.hold_date = objODBCDatareader["hold_date"].ToString();
                        values.credit_remarks = objODBCDatareader["credit_remarks"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                if (values.productmanager_approvalflag == "H") {
                    msSQL = " select appproductapproval_gid," +
                            " product_managergid as hold_by,productmanager_approvaldate as hold_date,productmanager_approvalremarks as credit_remarks" +
                            " from agr_trn_tappproductapproval where application_gid='" + values.application_gid + "' " +
                            " and productmanager_approvalflag ='H'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.appproductapproval_gid = objODBCDatareader["appproductapproval_gid"].ToString();
                     
                        values.hold_by = objODBCDatareader["hold_by"].ToString();
                        values.hold_date = objODBCDatareader["hold_date"].ToString();
                        values.credit_remarks = objODBCDatareader["credit_remarks"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                
            }

            msSQL = "select productmanager_approvaldate,productmember_approvaldate from agr_trn_tappproductapproval where application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsproductmanager_approvaldate = objODBCDatareader["productmanager_approvaldate"].ToString();
                lsproductmember_approvaldate = objODBCDatareader["productmember_approvaldate"].ToString();
            }
            else
            {
                lsapproved_date = null;
            }
            objODBCDatareader.Close();

            msSQL = "select appproductapproval_gid from agr_trn_tappproductapproval where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidproductapprovallog = objcmnfunctions.GetMasterGID("ACAL");
                    msSQL = " insert into agr_trn_tappproductrevokeapprovallog " +
                            " (appproductrevokeapprovallog_gid,appproductapproval_gid, application_gid, productdesk_gid, productdesk_name, product_gid, product_name, product_managergid, product_managername, productmanager_approvalflag, productmanager_approvaldate, productmanager_approvalremarks, product_membergid, product_membername,productmember_approvalflag,productmember_approvaldate,created_by, created_date, assign_remarks) " +
                            " select @appproductrevokeapprovallog_gid := '" + msGetGidproductapprovallog + "', appproductapproval_gid,application_gid, productdesk_gid,productdesk_name, product_gid, product_name, product_managergid, product_managername, productmanager_approvalflag, productmanager_approvaldate, productmanager_approvalremarks, product_membergid, product_membername, productmember_approvalflag,productmember_approvaldate,created_by, created_date, assign_remarks " +
                            " from agr_trn_tappproductapproval " +
                            " where application_gid='" + values.application_gid + "'" +
                            " and appproductapproval_gid ='" + dt["appproductapproval_gid"].ToString() + "'";
                    mnResultapprovallog = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {

            }

            if (mnResultapprovallog != 0)
            {
                if (values.approval_status == "Product Approval - Rejected")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CRRL");
                    msSQL = " insert into agr_trn_tproductrevokelog ( " +
                            " productrevokelog_gid, " +
                            " application_gid, " +
                            " reason, " +
                            " created_by, " +
                            " created_date, " +
                            " approval_status," +
                            " rejected_by," +
                            " rejected_date," +
                            " hold_by," +
                            " hold_date," +
                            " product_remarks," +
                            " productapproval_status) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.reason.Replace("'", "\\'") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Product Approval - Rejected',";
                    
                    msSQL += "'" + values.rejected_by + "'," +
                            "'" + Convert.ToDateTime(values.rejected_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "null," +
                             "null," +
                             "'" + values.credit_remarks + "',";
                    if (values.productmanager_approvalflag == "R")
                    {
                        msSQL += "'productmanager - Pending')";
                    }
                    else
                    {
                        msSQL += "'productmember - Pending')";
                    }
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CRRL");
                    msSQL = " insert into agr_trn_tproductrevokelog ( " +
                            " productrevokelog_gid, " +
                            " application_gid, " +
                            " reason, " +
                            " created_by, " +
                            " created_date, " +
                            " approval_status," +
                            " rejected_by," +
                            " rejected_date," +
                            " hold_by," +
                            " hold_date," +
                            " product_remarks, " +
                            " productapproval_status) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.reason.Replace("'", "\\'") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            
                            "'Product Approval - Hold" + "',";
                    msSQL += "null," +
                             "null," +
                             "'" + values.hold_by + "'," +
                             "'" + Convert.ToDateTime(values.hold_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "'" + values.credit_remarks + "',";
                    if (values.productmanager_approvalflag == "H")
                    {
                        msSQL += "'Productmanager - Pending')";
                    }
                    else
                    {
                        msSQL += "'Productmember - Pending')";
                    }
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    if (values.productmanager_approvalflag == "H")
                    {
                        msSQL = " update agr_trn_tappproductapproval set productmanager_approvalflag='N'," +
                                       " productmanager_approvalremarks = null," +
                                       " productmanager_approvaldate= null " +
                                       " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    else if (values.productmember_approvalflag == "R")
                    {
                        msSQL = " update agr_trn_tappproductapproval set productmember_approvalflag='N'," +
                                       " productmember_approvalremarks = null," +
                                       " productmember_approvaldate= null " +
                                       " where application_gid='" + values.application_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else if (values.productmanager_approvalflag == "R")
                    {
                        msSQL = " update agr_trn_tappproductapproval set productmanager_approvalflag='N'," +
                                       " productmanager_approvalremarks = null," +
                                       " productmanager_approvaldate= null " +
                                       " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    else if (values.productmember_approvalflag == "H")
                    {
                        msSQL = " update agr_trn_tappproductapproval set productmember_approvalflag='N'," +
                                       " productmember_approvalremarks = null," +
                                       " productmember_approvaldate= null " +
                                       " where application_gid='" + values.application_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }

                if ((mnResult != 0))
                {
                    msSQL = " update agr_mst_tapplication set approval_status='Product Approval - Pending', productdesk_flag='P'" +
                            " where application_gid='" + values.application_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                values.status = true;
                values.message = "Product Revoked Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred..!";
                return false;
            }
        }

        public void DaGetProductRevokedApplSummary(string employee_gid, MdlCreditRevokedAppl values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customer_name,a.customer_urn,a.vertical_name,a.applicant_type, a.region, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                     " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.creditgroup_gid,a.creditgroup_name,d.created_date, " +
                     " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date, " +
                     " (select f.approval_status from agr_trn_tproductrevokelog f where " +
                     " f.created_date = (select max(k.created_date) from agr_trn_tproductrevokelog k where k.application_gid = a.application_gid)) " +
                     " as approval_status" +
                     " from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid=a.created_by  " +
                     " left join agr_trn_tappproductrevokeapprovallog d on d.application_gid=a.application_gid " +
                     " left join agr_trn_tproductrevokelog e on e.application_gid = a.application_gid " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  " +
                     " where d.application_gid=a.application_gid " +
                     " group by(d.application_gid) order by d.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditrevokedappl_list = new List<creditrevokedappl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditrevokedappl_list.Add(new creditrevokedappl_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });

                }
            }
            values.creditrevokedappl_list = getcreditrevokedappl_list;
            dt_datatable.Dispose();
        }

        public void DaGetProductPendingHistoryLog(string application_gid, string employee_gid, MdlCreditPendingHistoryAppl values)
        {
            msSQL = " select a.application_gid, a.approval_remarks,approval_status,appproductapproval_gid, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as rejecttedhold_by,hierary_level, " +
                     " case when a.rejected_date is not null then date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') when date_format(a.hold_date,'%d-%m-%Y %h:%i %p') is not null then a.hold_date else null end as rejecttedhold_date " +
                     " from agr_trn_tAppcreditapproval a " +
                     " left join hrm_mst_temployee b on b.employee_gid=a.approval_gid  " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  " +
                     " where a.application_gid='" + application_gid + "' and (a.approval_status = 'Rejected' or a.approval_status = 'Hold') group by a.application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditpendingapplhis_list = new List<creditpendingapplhis_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditpendingapplhis_list.Add(new creditpendingapplhis_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        rejecttedhold_by = dt["rejecttedhold_by"].ToString(),
                        rejecttedhold_date = dt["rejecttedhold_date"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        hierary_level = dt["hierary_level"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        appcreditapproval_gid = dt["appcreditapproval_gid"].ToString()
                    });

                }
            }
            values.creditpendingapplhis_list = getcreditpendingapplhis_list;
            dt_datatable.Dispose();
        }

        public void DaGetProductHistoryLog(string application_gid, string employee_gid, MdlCreditHistoryLog values)
        {
            msSQL = " select a.application_gid,a.reason as revoked_remarks, productrevokelog_gid,  " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as rejected_by, " +
                    " date_format(a.rejected_date, '%d-%m-%Y %h:%i %p') as rejected_date," +
                    " date_format(a.hold_date, '%d-%m-%Y %h:%i %p') as hold_date,product_remarks,productapproval_status, " +
                    " concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as hold_by," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as revoked_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as revoked_date  " +
                    " from agr_trn_tproductrevokelog a  " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by   " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid=a.rejected_by  " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid   " +
                    " left join hrm_mst_temployee f on f.employee_gid=a.hold_by  " +
                    " left join adm_mst_tuser g on g.user_gid=f.user_gid   " +
                    " where a.application_gid = '" + application_gid + "' order by a.productrevokelog_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproducthistorylog_list = new List<credithistorylog_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getproducthistorylog_list.Add(new credithistorylog_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        rejected_by = dt["rejected_by"].ToString(),
                        rejected_date = dt["rejected_date"].ToString(),
                        hold_by = dt["hold_by"].ToString(),
                        hold_date = dt["hold_date"].ToString(),
                        credit_remarks = dt["product_remarks"].ToString(),
                        revoked_by = dt["revoked_by"].ToString(),
                        revoked_date = dt["revoked_date"].ToString(),
                        revoked_remarks = dt["revoked_remarks"].ToString(),
                        creditapproval_status = dt["productapproval_status"].ToString(),
                        creditrevokelog_gid = dt["productrevokelog_gid"].ToString()
                    });

                }
            }
            values.credithistorylog_list = getproducthistorylog_list;
            dt_datatable.Dispose();
        }
        public void DaGetProductPendingHistory(string application_gid, string employee_gid, MdlProductPendingHistoryAppl values)
        {
            msSQL = " select application_gid,appproductapproval_gid,product_managername,date_format(productmanager_approvaldate, '%d-%m-%Y %h:%i %p') as productmanager_approvaldate, productmanager_approvalflag, " +
                " productmanager_approvalremarks,product_membername,date_format(productmember_approvaldate, '%d-%m-%Y %h:%i %p') as productmember_approvaldate,productmember_approvalflag,assign_remarks " +
                " from agr_trn_tappproductapproval  where application_gid = '"+application_gid+"' and(productmanager_approvalflag = 'R' or "+
                " productmanager_approvalflag = 'H'  or productmember_approvalflag = 'R' or productmember_approvalflag = 'H') group by application_gid";           
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductpendingapplhis_list = new List<productpendingapplhis_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getproductpendingapplhis_list.Add(new productpendingapplhis_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        product_managername = dt["product_managername"].ToString(),
                        productmanager_approvaldate = dt["productmanager_approvaldate"].ToString(),
                        productmanager_approvalflag = dt["productmanager_approvalflag"].ToString(),
                        product_membername = dt["product_membername"].ToString(),
                        productmember_approvaldate = dt["productmember_approvaldate"].ToString(),
                        productmember_approvalflag = dt["productmember_approvalflag"].ToString(),
                        assign_remarks = dt["assign_remarks"].ToString(),
                        productmanager_approvalremarks = dt["productmanager_approvalremarks"].ToString(),
                        appproductapproval_gid = dt["appproductapproval_gid"].ToString()
                    });

                }
            }
            values.productpendingapplhis_list = getproductpendingapplhis_list;
            dt_datatable.Dispose();
        }
        public void DaProductApplicationRevokeCount(string user_gid, string employee_gid, CreditRevokeApplicationCount values)
        {
            msSQL = " select count(application_gid) creditreject_count from agr_mst_tapplication a " +
                    " where a.approval_status='Product Approval - Rejected'";
            values.creditreject_count = objdbconn.GetExecuteScalar(msSQL);
            int creditreject_count = Convert.ToInt16(values.creditreject_count);

            msSQL = " select count(application_gid) as credithold_count from agr_mst_tapplication a " +
                    " where a.approval_status = 'Product Approval - Hold'";
            values.credithold_count = objdbconn.GetExecuteScalar(msSQL);
            int credithold_count = Convert.ToInt16(values.credithold_count);

            msSQL = " select count(distinct a.application_gid) as creditrevoked_count from agr_mst_tapplication a " +
                    " left join agr_trn_tappproductrevokeapprovallog b on b.application_gid=a.application_gid  " +
                    " where b.application_gid=a.application_gid ";
            values.creditrevoked_count = objdbconn.GetExecuteScalar(msSQL);
            int creditrevoked_count = Convert.ToInt16(values.creditrevoked_count);

            int totalcount = creditreject_count + credithold_count + creditrevoked_count;
            values.lstotalcount = Convert.ToInt16(totalcount);

        }

        public void DaGetProductHistoryRemarksView(string productrevokelog_gid, MdlCreditHistoryApplLog values)
        {
            try
            {
                msSQL = " select productrevokelog_gid,product_remarks,reason,application_gid," +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date" +
                        " from agr_trn_tproductrevokelog a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where productrevokelog_gid = '" + productrevokelog_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.productrevokelog_gid = objODBCDatareader["productrevokelog_gid"].ToString();
                    values.product_remarks = objODBCDatareader["product_remarks"].ToString();
                    values.reason = objODBCDatareader["reason"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetProductPendingHistoryRemarksView(string appproductapproval_gid, MdlCreditPendingHistoryAppl values)
        {
            try
            {
                msSQL = " select appproductapproval_gid,productmanager_approvalflag,productmanager_approvalremarks, " + 
                        " productmember_approvalflag,assign_remarks from agr_trn_tappproductapproval" + 
                        " where appproductapproval_gid = '" + appproductapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.appproductapproval_gid = objODBCDatareader["appproductapproval_gid"].ToString();
                    values.productmanager_approvalflag = objODBCDatareader["productmanager_approvalflag"].ToString();
                    values.productmanager_approvalremarks = objODBCDatareader["productmanager_approvalremarks"].ToString();
                    values.productmember_approvalflag = objODBCDatareader["productmember_approvalflag"].ToString();
                    values.assign_remarks = objODBCDatareader["assign_remarks"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
            }
        }


        public bool DaPostCreditManagerRevokeApplication(Mdlcreditrevoke values, string user_gid, string employee_gid)
        {
            msSQL = "select approval_status from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.approval_status = objODBCDatareader["approval_status"].ToString();
            }
            objODBCDatareader.Close();

            if (values.approval_status == "Rejected by Credit Manager")
            {
                msSQL = " select appcreditapproval_gid,hierary_level, " +
                        " approval_gid as rejected_by,rejected_date,approval_remarks as credit_remarks" +
                        " from agr_trn_tappcreditapproval where application_gid='" + values.application_gid + "'" +
                        " and approval_status='Rejected'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.appcreditapproval_gid = objODBCDatareader["appcreditapproval_gid"].ToString();
                    values.hierary_level = objODBCDatareader["hierary_level"].ToString();
                    values.rejected_by = objODBCDatareader["rejected_by"].ToString();
                    values.rejected_date = objODBCDatareader["rejected_date"].ToString();
                    values.credit_remarks = objODBCDatareader["credit_remarks"].ToString();
                }
                objODBCDatareader.Close();
            }
            else
            {

            }

            msSQL = "select creditmanagerrejectlog_gid from agr_trn_tcreditmanagerrejectlog where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt2 in dt_datatable.Rows)
                {
                    msGetGidcreditmanagerapprovalreasonlog = objcmnfunctions.GetMasterGID("CMRE");
                    msSQL = " insert into agr_trn_tcreditmanagerrejectrevokereasonlog " +
                            " (creditmanagerrejectrevokereasonlog_gid,application_gid, application_no, approval_status, approval_remarks, " +
                            " rejected_by, rejected_date, created_by, created_date) " +
                            " select @creditmanagerrejectrevokereasonlog_gid := '" + msGetGidcreditmanagerapprovalreasonlog + "', " +
                            " application_gid,application_no, approval_status,approval_remarks, rejected_by, rejected_date, " +
                            " created_by, created_date " +
                            " from agr_trn_tcreditmanagerrejectlog " +
                            " where application_gid='" + values.application_gid + "'";
                    mnResultapprovalreasonlog = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            }
            else
            {

            }

            msSQL = "select appcreditapproval_gid from agr_trn_tappcreditapproval where application_gid='" + values.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGidcreditmanagerapprovallog = objcmnfunctions.GetMasterGID("CMRL");
                    msSQL = " insert into agr_trn_tappcreditmanagerrevokeapprovallog " +
                            " (appcreditmanagerrevokeapprovallog_gid,appcreditapproval_gid, application_gid, approval_gid, approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag) " +
                            " select @appcreditmanagerrevokeapprovallog_gid := '" + msGetGidcreditmanagerapprovallog + "', appcreditapproval_gid,application_gid, approval_gid,approval_name, approval_type, approval_status, approval_remarks, approved_date, rejected_date, hold_date, approval_token, hierary_level, created_by, created_date, initiate_flag " +
                            " from agr_trn_tappcreditapproval " +
                            " where application_gid='" + values.application_gid + "'" +
                            " and appcreditapproval_gid='" + dt["appcreditapproval_gid"].ToString() + "'";
                    mnResultapprovallog = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {

            }

            if (mnResultapprovalreasonlog != 0 && mnResultapprovallog != 0)
            {
                msSQL = " delete from agr_trn_tcreditmanagerrejectlog " +
                        " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.approval_status == "Rejected by Credit Manager")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CMRR");
                    msSQL = " insert into agr_trn_tcreditmanagerrevokelog ( " +
                            " creditmanagerrevokelog_gid, " +
                            " application_gid, " +
                            " reason, " +
                            " created_by, " +
                            " created_date, " +
                            " approval_status," +
                            " creditheadapproval_status, " +
                            " creditheadapproval_date, " +
                            " rejected_by," +
                            " rejected_date," +
                            " hold_by," +
                            " hold_date," +
                            " credit_remarks," +
                            " creditapproval_status) " +
                            " values (" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.reason.Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Rejected by Credit Manager'," +
                            "'" + "L" + values.hierary_level + " - Rejected" + "',";
                    if (lsapproved_date == null || lsapproved_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsapproved_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    msSQL += "'" + values.rejected_by + "'," +
                             "'" + Convert.ToDateTime(values.rejected_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "null," +
                             "null," +
                             "'" + values.credit_remarks.Replace("'", "") + "'," +
                             "'" + "L" + values.hierary_level + " - Pending" + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                }

                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_tapplication set approval_status='Submitted to Underwriting'" +
                           " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_trn_tappcreditapproval set approval_status='Pending',approval_remarks=null," +
                            " rejected_date=null where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                values.status = true;
                values.message = "Application Revoked Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred..!";
                return false;
            }
        }

        public void DaGetCreditManagerRejectedApplSummary(string employee_gid, MdlCreditRejectedAppl values)
        {
            msSQL = " select a.application_gid,application_no,customer_name,customer_urn,vertical_name,applicant_type, region, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.approval_status, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.creditgroup_gid,a.creditgroup_name, " +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as submitted_date, " +
                    " case when ((d.application_gid is not null) or (e.application_gid is not null)) then 'Y' else 'N' end as history_flag " +
                    " from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join agr_trn_tappcreditmanagerrevokeapprovallog d on d.application_gid = a.application_gid " +
                    " left join agr_trn_tcreditmanagerrevokelog e on e.application_gid = a.application_gid " +
                    " where a.approval_status='Rejected by Credit Manager' group by(a.application_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditmanagerrejectedappl_list = new List<creditmanagerrejectedappl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditmanagerrejectedappl_list.Add(new creditmanagerrejectedappl_list
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        region = dt["region"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        submitted_date = dt["submitted_date"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        history_flag = dt["history_flag"].ToString()
                    });

                }
            }
            values.creditmanagerrejectedappl_list = getcreditmanagerrejectedappl_list;
            dt_datatable.Dispose();
        }


    }
}