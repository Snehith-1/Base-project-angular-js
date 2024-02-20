using ems.hrloan.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using ems.storage.Functions;

namespace ems.hrloan.DataAccess 
{
    public class DaMstHRLoanApprovalsApproved
{
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL;

        public void DaGetHRloanApprovalSummary(MdlMstHRLoanApprovalsApproved values, string user_gid, string employee_gid, string request_gid)
        {
            msSQL = " select request_gid,'Direct Reporting Manager' as approval_Level,drm_status as approval_status,drm_approvedbyname as approved_byname,date_format(drm_updateddate,'%d-%m-%Y %h:%i %p') as updated_date,drm_remarks as approval_remarks from hrl_trn_trequest " +
                    " where request_gid = '" + request_gid + "' " +
                    " union all " +
                    " select request_gid,'Functional Head' as approval_Level ,fh_status as approval_status,fh_approvedbyname as approved_byname,date_format(fh_updateddate,'%d-%m-%Y %h:%i %p') as updated_date,fh_remarks as approval_remarks  from  " +
                    " hrl_trn_trequest where request_gid = '" + request_gid + "' " +
                    " union all " +
                    " select request_gid,'HR Head' as approval_Level,hrhead_status as approval_status,hrhead_approvedbyname as approved_byname,date_format(hrhead_updateddate,'%d-%m-%Y %h:%i %p') as updated_date,hrhead_remarks as approval_remarks " +
                    " from hrl_trn_trequest where request_gid = '" + request_gid + "' " +
                    " union all " +
                    " select request_gid,'HR Manager' as approval_Level,hrverify_status as approval_status,hrverify_approvedbyname as approved_byname,date_format(hrverify_updateddate,'%d-%m-%Y %h:%i %p') as updated_date,hrverify_remarks as approval_remarks " +
                    " from hrl_trn_trequest where request_gid = '" + request_gid + "' " +
                    " union all " +
                    " select request_gid,'Payment Approval' as approval_Level,hrpayment_status as approval_status,hrpayment_approvedbyname as approved_byname,date_format(hrpayment_updateddate,'%d-%m-%Y %h:%i %p') as updated_date,hrpayment_remarks as approval_remarks " +
                    " from hrl_trn_trequest where request_gid = '" + request_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getApprovalsummary = new List<ApprovedApprovalsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getApprovalsummary.Add(new ApprovedApprovalsummary
                    {
                        request_gid = dt["request_gid"].ToString(),
                        approval_Level = dt["approval_Level"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        approved_by = dt["approved_byname"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),

                    });
                    values.ApprovedApprovalsummary = getApprovalsummary;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetHRloanRequestDetails(MdlMstHRLoanApprovalsApproved values, string user_gid, string employee_gid)
        { 
            msSQL =  "  select  a.request_gid,  a.request_refno,  a.request_status,   a.fintype_name, " +
                         "  a.employee_gid,  a.employee_name,  a.employee_role,  a.department_name, " +
                         "  a.user_gid,  a.reporting_mgr,   a.functional_head, a.hr_head, a.created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                         "  a.raised_department,   a.amount,   a.purpose_name,   a.severity_name,  a.tenure ," +
                         "  a.drm_status from hrl_trn_trequest a " +
                         "  where ((a.reportingmgr_gid='" + employee_gid + "') and  (a.request_status = 'FH Pending' or  a.request_status = 'HR Pending'" +
                         "  or  a.request_status = 'Reupload Pending' or a.request_status = 'Reupload Completed' or a.request_status = 'HRVerify Pending' "+
                         " or a.request_status = 'HRVerify Approved' or  a.request_status = 'Payment Completed'))" +
                         "  or ((a.functionalhead_gid='" + employee_gid + "') and  (a.request_status = 'HR Pending'" +
                         "  or  a.request_status = 'Reupload Pending' or a.request_status = 'Reupload Completed' or "+
                         " a.request_status = 'HRVerify Pending' or a.request_status = 'HRVerify Approved' or  a.request_status = 'Payment Completed')) " +
                         "  order by a.request_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var gethrequestList = new List<Approvedrequestsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    gethrequestList.Add(new Approvedrequestsummary
                    {
                        request_gid = dt["request_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        drm_status = dt["drm_status"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        employee_role = dt["employee_role"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        user_gid = dt["user_gid"].ToString(),
                        reporting_mgr = dt["reporting_mgr"].ToString(),
                        functional_head = dt["functional_head"].ToString(),
                        hr_head = dt["hr_head"].ToString(),
                        amount = dt["amount"].ToString(),
                        purpose_name = dt["purpose_name"].ToString(),
                        severity_name = dt["severity_name"].ToString(),
                        tenure = dt["tenure"].ToString(),
                        fintype_name = dt["fintype_name"].ToString(),
                        created_date = dt["created_date"].ToString(),

                    });
                    values.Approvedrequestsummary = gethrequestList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetHRloanHRheadRequestDetails(MdlMstHRLoanApprovalsApproved values, string user_gid, string employee_gid)
        {
            msSQL = " select b.employee_name from hrl_mst_thrmapping a " +
                    " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                    " where hrmapping_name = 'Approver' and b.employee_gid ='" + employee_gid + "' " +
                    " order by b.created_date desc limit 1";

            string hr_name = objdbconn.GetExecuteScalar(msSQL);
            if (hr_name != "")
            {
                msSQL = "  select  a.request_gid,  a.request_refno,  a.request_status,   a.fintype_name, " +
                         "  a.employee_gid,  a.employee_name,  a.employee_role,  a.department_name, " +
                         "  a.user_gid,  a.reporting_mgr,   a.functional_head, a.hr_head, a.created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                         "  a.raised_department,   a.amount,   a.purpose_name,   a.severity_name,  a.tenure ," +
                         "  a.drm_status from hrl_trn_trequest a " +
                         "  where (a.request_status = 'HR Approved'" +
                         "  or  a.request_status = 'Reupload Pending' or  a.request_status = 'HRVerify Pending' or a.request_status = 'Reupload Completed' " +
                         "  or a.request_status = 'HRVerify Approved' or  a.request_status = 'Payment Completed') " +
                         "  order by a.request_gid desc";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var gethrequestList = new List<Approvedrequestsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    gethrequestList.Add(new Approvedrequestsummary
                    {
                        request_gid = dt["request_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        drm_status = dt["drm_status"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        employee_role = dt["employee_role"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        user_gid = dt["user_gid"].ToString(),
                        reporting_mgr = dt["reporting_mgr"].ToString(),
                        functional_head = dt["functional_head"].ToString(),
                        hr_head = dt["hr_head"].ToString(),
                        amount = dt["amount"].ToString(),
                        purpose_name = dt["purpose_name"].ToString(),
                        severity_name = dt["severity_name"].ToString(),
                        tenure = dt["tenure"].ToString(),
                        fintype_name = dt["fintype_name"].ToString(),
                        created_date = dt["created_date"].ToString(),

                    });
                    values.Approvedrequestsummary = gethrequestList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetHRloanRequestviewDetails(Approvedrequestsummary values, string user_gid, string employee_gid,string request_gid)
        {
            msSQL = " select request_gid, request_refno, request_status,  fintype_name, " +
                     " employee_gid, employee_name, employee_role, department_name, " +
                     " user_gid, reporting_mgr,  functional_head,hr_head, created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                     " raised_department,  amount,  purpose_name,  severity_name, tenure ,drm_status,entity_name from hrl_trn_trequest a " +
                     " where a.request_gid='" + request_gid + "' ";


            dt_datatable = objdbconn.GetDataTable(msSQL);

            var gethrequestList = new List<Approvedrequestsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)

                {

                 
                        values.request_gid = dt["request_gid"].ToString();
                        values.request_refno = dt["request_refno"].ToString();
                        values.request_status = dt["request_status"].ToString();
                        values.drm_status = dt["drm_status"].ToString();
                        values.employee_gid = dt["employee_gid"].ToString();
                        values.employee_name = dt["employee_name"].ToString();
                        values.employee_role = dt["employee_role"].ToString();
                        values.department_name = dt["department_name"].ToString();
                        values.user_gid = dt["user_gid"].ToString();
                        values.reporting_mgr = dt["reporting_mgr"].ToString();
                        values.functional_head = dt["functional_head"].ToString();
                        values.amount = dt["amount"].ToString();
                        values.purpose_name = dt["purpose_name"].ToString();
                        values.severity_name = dt["severity_name"].ToString();
                        values.tenure = dt["tenure"].ToString();
                        values.fintype_name = dt["fintype_name"].ToString();
                        values.created_date = dt["created_date"].ToString();
                        values.entity_name = dt["entity_name"].ToString();

                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetEmployeeDetails(MdlMstHRLoanApprovalsApproved objHRLoanRequest, string employee_gid)
        {
            try
            {

                msSQL = " select r.employee_gid,c.department_manager,a.user_code,a.user_firstname,a.user_gid , " +
                        " c.department_gid,c.department_name ,n.role_name, " +
                        " concat(rt.user_code, ' || ', rt.user_firstname, ' ', rt.user_lastname) as reporting_mgr," +
                        " concat(fh.user_code, ' || ', fh.user_firstname, ' ', fh.user_lastname) as functional_head, " +
                        " b.employee_personalno as personal_phone_no,b.personal_emailid, " +
                        " f.employee_gid as functionalhead_gid,r.employee_gid as reportingmgr_gid ," +
                        " b.employee_emailid,b.employee_mobileno " +
                        " FROM adm_mst_tuser a " +
                        " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                        " left join hrm_mst_trole n on n.role_gid = b.role_gid " +
                        " left join hrm_mst_temployee r on b.employeereporting_to = r.employee_gid " +
                        " left join adm_mst_tuser rt on rt.user_gid = r.user_gid " +
                        " left join hrm_mst_temployee f on r.employeereporting_to = f.employee_gid  " +
                        " left join adm_mst_tuser fh on fh.user_gid = f.user_gid " +
                        " where b.employee_gid = '" + employee_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objHRLoanRequest.employee_name = objODBCDatareader["user_firstname"].ToString();
                    objHRLoanRequest.role = objODBCDatareader["role_name"].ToString();
                    objHRLoanRequest.department = objODBCDatareader["department_name"].ToString();
                    objHRLoanRequest.reporting_manager = objODBCDatareader["reporting_mgr"].ToString();
                    objHRLoanRequest.functional_head = objODBCDatareader["functional_head"].ToString();
                    objHRLoanRequest.functionalhead_gid = objODBCDatareader["functionalhead_gid"].ToString();
                    objHRLoanRequest.reportingmgr_gid = objODBCDatareader["reportingmgr_gid"].ToString(); 
                    objHRLoanRequest.department_gid = objODBCDatareader["department_gid"].ToString();
                    objHRLoanRequest.employee_gid = employee_gid;
                    objHRLoanRequest.official_mailid = objODBCDatareader["employee_emailid"].ToString();
                    objHRLoanRequest.official_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                    objHRLoanRequest.pers_mailid = objODBCDatareader["personal_emailid"].ToString();
                    objHRLoanRequest.pers_mobileno = objODBCDatareader["personal_phone_no"].ToString();
                }
                objODBCDatareader.Close();
                
                objHRLoanRequest.status = true;
            }
            catch
            {
                objHRLoanRequest.status = false;
            }

        }
    }
}