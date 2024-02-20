using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using ems.storage.Functions;
using System.Net;
using System.Net.Mail;
using static OfficeOpenXml.ExcelErrorValue;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ems.master.DataAccess
{
    public class DaMstPhysicalDocument
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_datatable1;
        string msSQL, msGetGid;
        int mnResult;
        OdbcDataReader objODBCDataReader, objODBCDatareader, objODBCDatareader1;
        string lspath;
        HttpPostedFile httpPostedFile;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, cc_mailid1, cc_mailid2, employee_reporting_to;
        public string[] lsCCReceipients;
        public string lsBccmail_id;
        public string[] lsBCCReceipients;
        string lsrequested_by, message;
        Random rand = new Random();
        private string body1;
        private string sub1, ls_raised_by, ls_raised_date, lscompany_name;
        public int ls_port;
        public string cc1_mailid, cc2_mailid, to_employee_emailid, cc3_mailid, documenttype_name, ls_query_title, ls_query_description, ls_application_gid;
        string ls_relationshipmanager_gid, ls_relationshipmanager_name, ls_customerref_name, to1_mailid, to_mailid, ls_application_no, ls_query_responseddate;


        public void DaGetCADPhysicalDocMakerSummary(physicalmakerapplicationlist values, string employee_gid)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, e.sanction_refno, d.overall_approvalstatus, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                    " a.creditgroup_gid, d.cadgroup_name,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.completed_flag='N' and d.maker_gid = '" + employee_gid + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<physicalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new physicalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        //creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = dt["ccgroup_name"].ToString(),
                        //cadgroupname = dt["cadgroup_name"].ToString(),
                        //cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        //cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.physicalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetCADPhysicalDocFollowupMakerSummary(physicalmakerapplicationlist values, string employee_gid)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by,e.sanction_refno, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, d.overall_approvalstatus, " +
                    " a.creditgroup_gid, d.cadgroup_name,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.completed_flag='N' and d.maker_gid = '" + employee_gid + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'Y'  order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<physicalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new physicalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        //creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = dt["ccgroup_name"].ToString(),
                        //cadgroupname = dt["cadgroup_name"].ToString(),
                        //cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        //cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.physicalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetCADPhysicalDocCheckerSummary(physicalmakerapplicationlist values, string employee_gid)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, e.sanction_refno, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, d.overall_approvalstatus, " +
                    " a.creditgroup_gid, d.cadgroup_name,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.checker_gid = '" + employee_gid + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' and d.checker_approvalflag = 'N' and d.completed_flag='N' order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<physicalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new physicalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        //approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        //creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = dt["ccgroup_name"].ToString(),
                        //cadgroupname = dt["cadgroup_name"].ToString(),
                        //cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        //cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.physicalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetCADPhysicalDocApprovalCheckerSummary(physicalmakerapplicationlist values, string employee_gid)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, e.sanction_refno, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,d.overall_approvalstatus, " +
                    " a.creditgroup_gid, d.cadgroup_name,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.completed_flag='N' and d.checker_gid = '" + employee_gid + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'Y' and d.checker_approvalflag = 'N' order by a.updated_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<physicalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new physicalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        //creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = dt["ccgroup_name"].ToString(),
                        //cadgroupname = dt["cadgroup_name"].ToString(),
                        //cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        //cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.physicalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetCADPhysicalDocFollowupCheckerSummary(physicalmakerapplicationlist values, string employee_gid)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by,e.sanction_refno, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,d.overall_approvalstatus, " +
                    " a.creditgroup_gid, d.cadgroup_name,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.completed_flag='N' and d.checker_gid = '" + employee_gid + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.checker_approvalflag = 'Y' order by a.updated_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<physicalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new physicalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        //creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = dt["ccgroup_name"].ToString(),
                        //cadgroupname = dt["cadgroup_name"].ToString(),
                        //cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        //cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.physicalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetCADPhysicalDocApproverSummary(physicalmakerapplicationlist values, string employee_gid)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by,e.sanction_refno, d.overall_approvalstatus, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                    " a.creditgroup_gid, d.cadgroup_name,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.approver_gid = '" + employee_gid + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag='Y' and d.checker_approvalflag = 'Y' and d.completed_flag='N' and d.approver_approvalflag='N'" +
                    " order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<physicalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new physicalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        //creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = dt["ccgroup_name"].ToString(),
                        //cadgroupname = dt["cadgroup_name"].ToString(),
                        //cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        //cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.physicalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetCADPhysicalDocFollowupApproverSummary(physicalmakerapplicationlist values, string employee_gid)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by,e.sanction_refno, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,d.overall_approvalstatus, " +
                    " a.creditgroup_gid, d.cadgroup_name,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.approver_gid = '" + employee_gid + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag='Y' and d.checker_approvalflag = 'Y' and d.completed_flag='N'  and d.approver_approvalflag='Y'" +
                    " order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<physicalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new physicalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        //creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = dt["ccgroup_name"].ToString(),
                        //cadgroupname = dt["cadgroup_name"].ToString(),
                        //cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        //cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.physicalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetCADPhysicalDocCompletedSummary(physicalmakerapplicationlist values, string employee_gid)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by,d.overall_approvalstatus, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, e.sanction_refno, " +
                    " date_format(d.completed_on, '%d-%m-%Y %h:%i %p') as completed_on, " +
                    " a.creditgroup_gid, d.cadgroup_name,a.renewal_flag,a.enhancement_flag from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' " +
                    " and d.maker_approvalflag='Y' and d.checker_approvalflag = 'Y' and d.approver_approvalflag='Y' " +
                    " and d.overall_approvalstatus='Approved' and d.completed_flag='Y' " +
                    " order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<physicalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new physicalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        //creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = dt["ccgroup_name"].ToString(),
                        //cadgroupname = dt["cadgroup_name"].ToString(),
                        //cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        //cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        completed_on = dt["completed_on"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        renewal_flag = dt["renewal_flag"].ToString(),
                        enhancement_flag = dt["enhancement_flag"].ToString()
                    });
                }
            }
            values.physicalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaUpdatePhysicalApproval(result values, string lstype, string processtypeassign_gid, string user_gid)
        {
            msSQL = " select application_gid from ocs_trn_tprocesstype_assign where processtypeassign_gid='" + processtypeassign_gid + "'";
            string lsapplication_gid1 = objdbconn.GetExecuteScalar(msSQL);
            if (lstype == "Maker")
            {
                msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', " +
                        " maker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " overall_approvalstatus='Proceed to Checker'  where processtypeassign_gid='" + processtypeassign_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Scanned document submitted to Checker Successfully..!";
            }
            else if (lstype == "Checker")
            {
                msSQL = " select tagquery_gid from ocs_trn_ttagquery " +
                                           " where  query_status in ('Pending') AND  fromphysical_document = 'Y'" +
                                           " and application_gid = '" + lsapplication_gid1 + "'";
                string tagquery_status = objdbconn.GetExecuteScalar(msSQL);

                if (!string.IsNullOrEmpty(tagquery_status))
                {
                    values.status = false;
                    values.message = "Kindly Confirm the Query Status";

                    return;
                }
                msSQL = " update ocs_trn_tprocesstype_assign set checker_approvalflag='Y', " +
                       " checker_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                       " overall_approvalstatus='Proceed To Approval'  where processtypeassign_gid='" + processtypeassign_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Scanned document Submitted to Approval Successfully..!";
            }
            else
            {
              
                msSQL = " select tagquery_gid from ocs_trn_ttagquery " +
                                           " where  query_status in ('Pending','Query Raised') AND  fromphysical_document = 'Y'" +
                                           " and application_gid = '" + lsapplication_gid1 + "'";
                string tagquery_status = objdbconn.GetExecuteScalar(msSQL);

                if (!string.IsNullOrEmpty(tagquery_status))
                {
                    values.status = false;
                    values.message = "Kindly Close the Open Query";

                    return;
                }
                msSQL = " update ocs_trn_tprocesstype_assign set approver_approvalflag='Y', " +
                       " approver_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                       " overall_approvalstatus='Approved'  where processtypeassign_gid='" + processtypeassign_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Approved Successfully..!";

                msSQL = " select application_gid from ocs_trn_tprocesstype_assign where processtypeassign_gid='" + processtypeassign_gid + "'";
                string lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lsapplication_gid != "")
                {
                    result objvalues = new result();
                    DaupdatePhysicalAutomaticCompletion(lsapplication_gid, user_gid, objvalues);
                }
            }
            if (mnResult == 1)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaCADAppPhysicalDocCount(string employee_gid, CadScannedCount values)
        {
            msSQL = " select(select count(*) from ocs_trn_tprocesstype_assign a  " +
                    " left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                    " where maker_approvalflag = 'N' and b.docchecklist_approvalflag = 'Y'  and completed_flag = 'N' " +
                    " and maker_gid = '" + employee_gid + "' and menu_gid = '" + getMenuClass.PhysicalDocument + "') as MakerPendingCount , " +
                    " (select count(*) from ocs_trn_tprocesstype_assign a " +
                    " left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                    " where maker_approvalflag = 'Y' and b.docchecklist_approvalflag = 'Y'  and completed_flag = 'N' " +
                    " and maker_gid = '" + employee_gid + "' and menu_gid = '" + getMenuClass.PhysicalDocument + "') as MakerFollowUpCount, " +
                    " (select count(*) from ocs_trn_tprocesstype_assign a " +
                    " left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                    " where checker_approvalflag = 'N' and b.docchecklist_approvalflag = 'Y' and maker_approvalflag = 'N' and completed_flag = 'N' " +
                    " and checker_gid = '" + employee_gid + "' and menu_gid = '" + getMenuClass.PhysicalDocument + "') as CheckerPendingCount, " +
                    " (select count(*) from ocs_trn_tprocesstype_assign " +
                    " where checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                    " and checker_gid = '" + employee_gid + "' and menu_gid = '" + getMenuClass.PhysicalDocument + "') as CheckerApprovalPendingCount, " +
                    " (select count(*) from ocs_trn_tprocesstype_assign " +
                    " where checker_approvalflag = 'Y' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                    " and checker_gid = '" + employee_gid + "' and menu_gid = '" + getMenuClass.PhysicalDocument + "') as CheckerFollowUpCount, " +
                    " (select count(*) from ocs_trn_tprocesstype_assign " +
                    " where approver_approvalflag = 'N' and checker_approvalflag = 'Y'  and completed_flag = 'N' " +
                    " and approver_gid = '" + employee_gid + "' and menu_gid = '" + getMenuClass.PhysicalDocument + "')  as ApproverPendingCount, " +
                    " (select count(*) from ocs_trn_tprocesstype_assign " +
                    " where approver_approvalflag = 'Y' and checker_approvalflag = 'Y' and completed_flag = 'N' " +
                    " and approver_gid = '" + employee_gid + "' and menu_gid = '" + getMenuClass.PhysicalDocument + "')  as ApproverFollowUpCount, " +
                    " (select count(*) from ocs_trn_tprocesstype_assign " +
                    " where approver_approvalflag = 'Y' and checker_approvalflag = 'Y' and completed_flag = 'Y' and menu_gid = '" + getMenuClass.PhysicalDocument + "' " +
                    " and overall_approvalstatus='Approved') as CompletedCount ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.MakerPendingCount = objODBCDataReader["MakerPendingCount"].ToString();
                values.MakerFollowUpCount = objODBCDataReader["MakerFollowUpCount"].ToString();
                values.CheckerPendingCount = objODBCDataReader["CheckerPendingCount"].ToString();
                values.CheckerApprovalPendingCount = objODBCDataReader["CheckerApprovalPendingCount"].ToString();
                values.CheckerFollowUpCount = objODBCDataReader["CheckerFollowUpCount"].ToString();
                values.ApproverPendingCount = objODBCDataReader["ApproverPendingCount"].ToString();
                values.ApproverFollowUpCount = objODBCDataReader["ApproverFollowUpCount"].ToString();
                values.CompletedCount = objODBCDataReader["CompletedCount"].ToString();

            }
            objODBCDataReader.Close();
        }
        //public bool DaGetCADTrnPhysicalDocList(PhysicalDocTaggedDocumentList values, string credit_gid, string application_gid)
        //{
        //    msSQL = "select application_no, customer_name from ocs_mst_tapplication where application_gid='" + application_gid + "'";
        //    objODBCDataReader = objdbconn.GetDataReader(msSQL);
        //    if (objODBCDataReader.HasRows == true)
        //    {
        //        values.application_no = objODBCDataReader["application_no"].ToString();
        //        values.customer_name = objODBCDataReader["customer_name"].ToString();
        //    }
        //    objODBCDataReader.Close();
        //    List<PhysicalDocTaggedDocument> deferraltagged = new List<PhysicalDocTaggedDocument>();
        //    msSQL = " select  groupdocumentchecklist_gid, date_format(due_date,'%d-%m-%Y') as due_date, deferraltagdoc_gid, deferraltag_status, " +
        //            " date_format(created_date, '%d-%m-%Y %h:%i %p') as taggeddate   from ocs_trn_tdeferraltagdoc " +
        //            " where application_gid = '" + application_gid + "' and deferraltag_status in ('1','2') and fromphysical_document = 'N'";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        deferraltagged = dt_datatable.AsEnumerable().Select(row => new PhysicalDocTaggedDocument
        //        {
        //            groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
        //            deferraltag_status = row["deferraltag_status"].ToString(),
        //            taggeddate = row["taggeddate"].ToString(),
        //            due_date = row["due_date"].ToString(),
        //            deferraltagdoc_gid = row["deferraltagdoc_gid"].ToString(),
        //        }).ToList();
        //    }
        //    dt_datatable.Dispose();

        //    msSQL = " SELECT a.groupdocumentchecklist_gid,b.fromphysical_document, a.overall_docstatus,a.physicaloverall_docstatus, mstdocumenttype_gid as documenttype_gid,mstdocumenttype_name as documenttype_code, " +
        //               " a.mstdocument_name as documenttype_name, a.tagged_by,date_format(a.physical_extendedduedate,'%d-%m-%Y') as extendeddue_date, " +
        //               " a.mstcovenant_type,a.application_gid, date_format(b.due_date,'%d-%m-%Y') as due_date, b.deferraltagdoc_gid, b.deferraltag_status, " +
        //               " date_format(b.created_date,'%d-%m-%Y %h:%i %p') as taggeddate, " +
        //               " (select count(*) from ocs_trn_tdeferralchecklist x where x.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and x.fromphysical_document='Y' LIMIT 1) as checklistcount, " +
        //               " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y " +
        //               " WHERE y.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid) as physical_documentcount, " +
        //               " (SELECT COUNT(*) from ocs_trn_tinitiateextendorwaiver z " +
        //               " where z.groupdocumentchecklist_gid =  a.groupdocumentchecklist_gid and z.fromphysical_document='Y' and approval_status in ('Pending','Approved') and activity_type = 'Waiver') as waiverpendingcount " +
        //               " FROM ocs_trn_tgroupdocumentchecklist a " +
        //               " LEFT JOIN ocs_trn_tdeferraltagdoc b on a.groupdocumentchecklist_gid = b.groupdocumentchecklist_gid " +
        //               " WHERE a.credit_gid = '" + credit_gid + "' and a.untagged_type is null " +
        //               " and  (b.deferraltag_status in ('" + deferralTagstatus.Active + "','" + deferralTagstatus.DeferralTaken + "') or b.deferraltag_status is null) " +
        //               " group by a.groupdocumentchecklist_gid order by documenttype_code asc,documenttype_name asc";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);

        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        var getPhysicalDocTaggedDocument = new List<PhysicalDocTaggedDocument>();
        //        var i = 0;
        //        foreach (DataRow row in dt_datatable.Rows)
        //        {
        //            var groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString();
        //            getPhysicalDocTaggedDocument.Add(new PhysicalDocTaggedDocument
        //            {
        //                groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
        //                documenttype_gid = row["documenttype_gid"].ToString(),
        //                documenttype_code = row["documenttype_code"].ToString(),
        //                documenttype_name = row["documenttype_name"].ToString(),
        //                covenant_type = row["mstcovenant_type"].ToString(),
        //                overall_docstatus = row["overall_docstatus"].ToString(),
        //                physical_documentcount = row["physical_documentcount"].ToString(),
        //                waiverpendingcount = row["waiverpendingcount"].ToString(),
        //                checklistcount = row["checklistcount"].ToString(),
        //                physicaloverall_docstatus = row["physicaloverall_docstatus"].ToString(),
        //                extendeddue_date = row["extendeddue_date"].ToString(),
        //                taggedby = row["tagged_by"].ToString(),
        //            });
        //            if (deferraltagged != null)
        //            {
        //                var getdeferraldata = deferraltagged.Where(a => a.groupdocumentchecklist_gid == groupdocumentchecklist_gid).FirstOrDefault();
        //                if (getdeferraldata != null)
        //                {
        //                    getPhysicalDocTaggedDocument[i].deferraltag_status = getdeferraldata.deferraltag_status;
        //                    getPhysicalDocTaggedDocument[i].taggeddate = getdeferraldata.taggeddate;
        //                    getPhysicalDocTaggedDocument[i].due_date = getdeferraldata.due_date;
        //                    getPhysicalDocTaggedDocument[i].deferraltagdoc_gid = getdeferraldata.deferraltagdoc_gid;
        //                }
        //            }
        //            i++;
        //        }

        //        values.PhysicalDocTaggedDocument = getPhysicalDocTaggedDocument;
        //    }
        //    dt_datatable.Dispose();

        //    msSQL = " SELECT a.groupcovdocumentchecklist_gid,b.fromphysical_document,a.overall_docstatus ,a.physicaloverall_docstatus, mstdocumenttype_gid as documenttype_gid,mstdocumenttype_name as documenttype_code, " +
        //               " a.mstdocument_name as documenttype_name, a.tagged_by,date_format(a.physical_extendedduedate,'%d-%m-%Y') as extendeddue_date, " +
        //               " a.mstcovenant_type,a.application_gid, date_format(b.due_date,'%d-%m-%Y') as due_date, b.deferraltagdoc_gid, b.deferraltag_status, " +
        //               " date_format(b.created_date,'%d-%m-%Y %h:%i %p') as taggeddate, " +
        //               " (select count(*) from ocs_trn_tdeferralchecklist x where x.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and x.fromphysical_document='Y' ) as checklistcount, " +
        //               " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y " +
        //               " WHERE y.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid) as physical_documentcount, " +
        //               " (select physical_covenant_periods from ocs_trn_tcovanantdocumentcheckdtls e " +
        //               " where e.groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid  group by a.groupcovdocumentchecklist_gid ) as covenant_periods, " +
        //               " (SELECT COUNT(*) from ocs_trn_tinitiateextendorwaiver z " +
        //               " where z.groupdocumentchecklist_gid =  a.groupcovdocumentchecklist_gid and z.fromphysical_document='Y' and approval_status = 'Pending' and activity_type = 'Waiver') as waiverpendingcount " +
        //               " FROM ocs_trn_tgroupcovenantdocumentchecklist a " +
        //               " LEFT JOIN ocs_trn_tdeferraltagdoc b on a.groupcovdocumentchecklist_gid = b.groupdocumentchecklist_gid " +
        //               " WHERE a.credit_gid = '" + credit_gid + "' and a.untagged_type is null " +
        //               " and (b.deferraltag_status in ('" + deferralTagstatus.Active + "','" + deferralTagstatus.DeferralTaken + "') or b.deferraltag_status is null) " +
        //               " group by a.groupcovdocumentchecklist_gid order by documenttype_code asc,documenttype_name asc";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);

        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        var getPhysicalCovenantDocTaggedDocument = new List<PhysicalCovenantDocTaggedDocument>();
        //        var i = 0;
        //        foreach (DataRow row in dt_datatable.Rows)
        //        {
        //            var groupdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString();
        //            getPhysicalCovenantDocTaggedDocument.Add(new PhysicalCovenantDocTaggedDocument
        //            {
        //                groupdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString(),
        //                documenttype_gid = row["documenttype_gid"].ToString(),
        //                documenttype_code = row["documenttype_code"].ToString(),
        //                documenttype_name = row["documenttype_name"].ToString(),
        //                covenant_type = row["mstcovenant_type"].ToString(),
        //                overall_docstatus = row["overall_docstatus"].ToString(),
        //                extendeddue_date = row["extendeddue_date"].ToString(),
        //                physical_documentcount = row["physical_documentcount"].ToString(),
        //                covenant_periods = row["covenant_periods"].ToString(),
        //                waiverpendingcount = row["waiverpendingcount"].ToString(),
        //                checklistcount = row["checklistcount"].ToString(),
        //                physicaloverall_docstatus = row["physicaloverall_docstatus"].ToString(),
        //                taggedby = row["tagged_by"].ToString(),
        //            });
        //            if (deferraltagged != null)
        //            {
        //                var getdeferraldata = deferraltagged.Where(a => a.groupdocumentchecklist_gid == groupdocumentchecklist_gid).FirstOrDefault();
        //                if (getdeferraldata != null)
        //                {
        //                    getPhysicalCovenantDocTaggedDocument[i].deferraltag_status = getdeferraldata.deferraltag_status;
        //                    getPhysicalCovenantDocTaggedDocument[i].taggeddate = getdeferraldata.taggeddate;
        //                    getPhysicalCovenantDocTaggedDocument[i].due_date = getdeferraldata.due_date;
        //                    getPhysicalCovenantDocTaggedDocument[i].deferraltagdoc_gid = getdeferraldata.deferraltagdoc_gid;
        //                }
        //            }
        //            i++;
        //        }
        //        values.PhysicalCovenantDocTaggedDocument = getPhysicalCovenantDocTaggedDocument;
        //    }
        //    dt_datatable.Dispose();
        //    return true;
        //}


        public bool DaGetCADTrnPhysicalDocList(PhysicalDocTaggedDocumentList values, string credit_gid, string application_gid)
        {
            msSQL = "select application_no, customer_name from ocs_trn_tcadapplication where application_gid='" + application_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.application_no = objODBCDataReader["application_no"].ToString();
                values.customer_name = objODBCDataReader["customer_name"].ToString();
            }
            objODBCDataReader.Close();
            List<PhysicalDocTaggedDocument> deferraltagged = new List<PhysicalDocTaggedDocument>();
            msSQL = " select  groupdocumentchecklist_gid, date_format(due_date,'%d-%m-%Y') as due_date, deferraltagdoc_gid, deferraltag_status, " +
                    " date_format(created_date, '%d-%m-%Y %h:%i %p') as taggeddate   from ocs_trn_tdeferraltagdoc " +
                    " where application_gid = '" + application_gid + "' and deferraltag_status in ('1','2') and fromphysical_document = 'Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                deferraltagged = dt_datatable.AsEnumerable().Select(row => new PhysicalDocTaggedDocument
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    deferraltag_status = row["deferraltag_status"].ToString(),
                    taggeddate = row["taggeddate"].ToString(),
                    due_date = row["due_date"].ToString(),
                    deferraltagdoc_gid = row["deferraltagdoc_gid"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.groupdocumentchecklist_gid,a.document_code,a.overall_docstatus,a.physicaloverall_docstatus, mstdocumenttype_gid as documenttype_gid,mstdocumenttype_name as documenttype_code, " +
                       " a.mstdocument_name as documenttype_name, a.tagged_by,date_format(a.physical_extendedduedate,'%d-%m-%Y') as extendeddue_date, " +
                       " a.mstcovenant_type,a.application_gid,a.physicalcopyquerystatus, " +
                       " (select count(*) from ocs_trn_tdeferralchecklist x where x.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and x.fromphysical_document='Y' LIMIT 1) as checklistcount, " +
                       " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y " +
                       " WHERE y.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid) as physical_documentcount, " +
                       " (SELECT COUNT(*) from ocs_trn_tinitiateextendorwaiver z " +
                       " where z.groupdocumentchecklist_gid =  a.groupdocumentchecklist_gid and z.fromphysical_document='Y' and approval_status in ('Pending','Approved') and activity_type = 'Waiver') as waiverpendingcount " +
                       " FROM ocs_trn_tcadgroupdocumentchecklist a " +
                       " WHERE a.credit_gid = '" + credit_gid + "' and a.untagged_type is null " +
                       " group by a.groupdocumentchecklist_gid order by documenttype_code asc,documenttype_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                var getPhysicalDocTaggedDocument = new List<PhysicalDocTaggedDocument>();
                var i = 0;
                foreach (DataRow row in dt_datatable.Rows)
                {
                    var groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString();
                    getPhysicalDocTaggedDocument.Add(new PhysicalDocTaggedDocument
                    {
                        groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                        documenttype_gid = row["documenttype_gid"].ToString(),
                        documenttype_code = row["documenttype_code"].ToString(),
                        document_code = row["document_code"].ToString(),
                        documenttype_name = row["documenttype_name"].ToString(),
                        covenant_type = row["mstcovenant_type"].ToString(),
                        overall_docstatus = row["overall_docstatus"].ToString(),
                        physical_documentcount = row["physical_documentcount"].ToString(),
                        waiverpendingcount = row["waiverpendingcount"].ToString(),
                        checklistcount = row["checklistcount"].ToString(),
                        physicaloverall_docstatus = row["physicaloverall_docstatus"].ToString(),
                        extendeddue_date = row["extendeddue_date"].ToString(),
                        taggedby = row["tagged_by"].ToString(),
                        physicalcopyquerystatus = row["physicalcopyquerystatus"].ToString(),
                    });
                    if (deferraltagged != null)
                    {
                        var getdeferraldata = deferraltagged.Where(a => a.groupdocumentchecklist_gid == groupdocumentchecklist_gid).FirstOrDefault();
                        if (getdeferraldata != null)
                        {
                            getPhysicalDocTaggedDocument[i].deferraltag_status = getdeferraldata.deferraltag_status;
                            getPhysicalDocTaggedDocument[i].taggeddate = getdeferraldata.taggeddate;
                            getPhysicalDocTaggedDocument[i].due_date = getdeferraldata.due_date;
                            getPhysicalDocTaggedDocument[i].deferraltagdoc_gid = getdeferraldata.deferraltagdoc_gid;
                        }
                    }
                    i++;
                }

                values.PhysicalDocTaggedDocument = getPhysicalDocTaggedDocument;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.groupcovdocumentchecklist_gid,a.document_code, a.overall_docstatus ,a.physicaloverall_docstatus, mstdocumenttype_gid as documenttype_gid,mstdocumenttype_name as documenttype_code, " +
                       " a.mstdocument_name as documenttype_name, a.tagged_by,date_format(a.physical_extendedduedate,'%d-%m-%Y') as extendeddue_date, " +
                       " a.mstcovenant_type,a.application_gid,a.physicalcopyquerystatus, " +
                       " (select count(*) from ocs_trn_tdeferralchecklist x where x.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and x.fromphysical_document='Y' ) as checklistcount, " +
                       " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y " +
                       " WHERE y.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid) as physical_documentcount, " +
                       " CASE  WHEN physical_covenant_periods is null THEN  (select covenant_periods from ocs_trn_tcadcovanantdocumentcheckdtls e " +
                       " where e.groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid  group by a.groupcovdocumentchecklist_gid ) " +
                       " ELSE physical_covenant_periods END as 'covenant_periods', " +
                        " CASE  WHEN physical_buffer_days is null THEN  (select buffer_days from ocs_trn_tcadcovanantdocumentcheckdtls e " +
                       " where e.groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid  group by a.groupcovdocumentchecklist_gid ) " +
                       " ELSE physical_buffer_days END as 'buffer_days', " +
                       " (SELECT COUNT(*) from ocs_trn_tinitiateextendorwaiver z " +
                       " where z.groupdocumentchecklist_gid =  a.groupcovdocumentchecklist_gid and z.fromphysical_document='Y' and approval_status = 'Pending' and activity_type = 'Waiver') as waiverpendingcount " +
                       " FROM ocs_trn_tcadgroupcovenantdocumentchecklist a " +
                       " WHERE a.credit_gid = '" + credit_gid + "' and a.untagged_type is null " +
                       " group by a.groupcovdocumentchecklist_gid order by documenttype_code asc,documenttype_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                var getPhysicalCovenantDocTaggedDocument = new List<PhysicalCovenantDocTaggedDocument>();
                var i = 0;
                foreach (DataRow row in dt_datatable.Rows)
                {
                    var groupdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString();
                    getPhysicalCovenantDocTaggedDocument.Add(new PhysicalCovenantDocTaggedDocument
                    {
                        groupdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString(),
                        documenttype_gid = row["documenttype_gid"].ToString(),
                        documenttype_code = row["documenttype_code"].ToString(),
                        document_code = row["document_code"].ToString(),
                        documenttype_name = row["documenttype_name"].ToString(),
                        covenant_type = row["mstcovenant_type"].ToString(),
                        overall_docstatus = row["overall_docstatus"].ToString(),
                        extendeddue_date = row["extendeddue_date"].ToString(),
                        physical_documentcount = row["physical_documentcount"].ToString(),
                        covenant_periods = row["covenant_periods"].ToString(),
                        waiverpendingcount = row["waiverpendingcount"].ToString(),
                        checklistcount = row["checklistcount"].ToString(),
                        physicaloverall_docstatus = row["physicaloverall_docstatus"].ToString(),
                        taggedby = row["tagged_by"].ToString(),
                        buffer_days = row["buffer_days"].ToString(),
                        physicalcopyquerystatus = row["physicalcopyquerystatus"].ToString(),
                    });
                    if (deferraltagged != null)
                    {
                        var getdeferraldata = deferraltagged.Where(a => a.groupdocumentchecklist_gid == groupdocumentchecklist_gid).FirstOrDefault();
                        if (getdeferraldata != null)
                        {
                            getPhysicalCovenantDocTaggedDocument[i].deferraltag_status = getdeferraldata.deferraltag_status;
                            getPhysicalCovenantDocTaggedDocument[i].taggeddate = getdeferraldata.taggeddate;
                            getPhysicalCovenantDocTaggedDocument[i].due_date = getdeferraldata.due_date;
                            getPhysicalCovenantDocTaggedDocument[i].deferraltagdoc_gid = getdeferraldata.deferraltagdoc_gid;
                        }
                    }
                    i++;
                }
                values.PhysicalCovenantDocTaggedDocument = getPhysicalCovenantDocTaggedDocument;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaPhysicalDocumentUpload(HttpRequest httpRequest, scanneduploaddocumentlist values, string employee_gid, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lscredit_gid = httpRequest.Form["credit_gid"].ToString();
            string lsapplication_gid = httpRequest.Form["application_gid"].ToString();
            string lscovenant_type = httpRequest.Form["covenant_type"].ToString();
            string lsdocumentcheckdtl_gid = httpRequest.Form["documentcheckdtl_gid"].ToString();
            string lsRMupload = httpRequest.Form["RMupload"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/PhysicalDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                            values.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/PhysicalDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/PhysicalDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "Master/PhysicalDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        string lsdocumenttype_gid = "", lsdocumenttype_code = "";
                        msSQL = "select mstdocumenttype_gid,mstdocumenttype_name from ocs_trn_tcadgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsdocumentcheckdtl_gid + "'";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows == true)
                        {
                            lsdocumenttype_gid = objODBCDataReader["mstdocumenttype_gid"].ToString();
                            lsdocumenttype_code = objODBCDataReader["mstdocumenttype_name"].ToString();
                        }
                        objODBCDataReader.Close();
                        msGetGid = objcmnfunctions.GetMasterGID("PYDO");
                        string msGetCode = objcmnfunctions.GetMasterGID("OCDG");
                        msSQL = " insert into ocs_trn_tphysicaldocument( " +
                                    " physicaldocument_gid," +
                                    " physicaldocument_code, " +
                                    " groupdocumentchecklist_gid," +
                                    " application_gid, " +
                                    " credit_gid," +
                                    " rm_upload, " +
                                    " documenttype_gid, " +
                                    " documenttype_code, " +
                                    " documenttype_name ," +
                                    " file_name ," +
                                       " file_path," +
                                        " covenant_type," +
                                        " created_by," +
                                        " created_date" +
                                        " )values(" +
                                        "'" + msGetGid + "'," +
                                        "'" + msGetCode + "'," +
                                        "'" + lsdocumentcheckdtl_gid + "'," +
                                        "'" + lsapplication_gid + "'," +
                                        "'" + lscredit_gid + "'," +
                                        "'" + lsRMupload + "'," +
                                        "'" + lsdocumenttype_gid + "'," +
                                        "'" + lsdocumenttype_code + "'," +
                                        "'" + lsdocument_title.Replace("'", "") + "'," +
                                        "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                        "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                        "'" + lscovenant_type + "'," +
                                        "'" + employee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            if (lscovenant_type == "Y")
                            {
                                msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physicaloverall_docstatus ='Pending Vetting'" +
                                      " where groupcovdocumentchecklist_gid='" + lsdocumentcheckdtl_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus ='Pending Vetting'" +
                                        " where groupcovdocumentchecklist_gid='" + lsdocumentcheckdtl_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else
                            {
                                msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='Pending Vetting'" +
                                  " where groupdocumentchecklist_gid='" + lsdocumentcheckdtl_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='Pending Vetting'" +
                                          " where groupdocumentchecklist_gid='" + lsdocumentcheckdtl_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            values.status = true;
                            values.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error Occured..!";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
            return true;
        }


        public void DaGetPhysicalDocument(string documentcheckdtl_gid, scanneduploaddocumentlist values)
        {
            msSQL = " select physicaldocument_gid,groupdocumentchecklist_gid,file_name,file_path,documenttype_name,documenttype_code, " +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date,physicaldocument_code, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                    " from ocs_trn_tphysicaldocument a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<scanneduploaddocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new scanneduploaddocument
                    {
                        documenttype_code = dt["documenttype_code"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        //file_path = HttpContext.Current.Server.MapPath(dt["file_path"].ToString()),
                        file_path = objcmnstorage.EncryptData((dt["file_path"].ToString())),
                        file_name = dt["file_name"].ToString(),
                        physicaldocument_gid = dt["physicaldocument_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        scanneddocument_code = dt["physicaldocument_code"].ToString(),
                    });
                    values.scanneduploaddocument = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetRMScannedDocument(string documentcheckdtl_gid, string signeddocument_flag, scanneduploaddocumentlist values, string user_gid)
        {
            msSQL = " select scanneddocument_gid,groupdocumentchecklist_gid,file_name,file_path,documenttype_name,documenttype_code, " +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                    " from ocs_trn_tscanneddocument a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "' and rm_upload='Y' and a.signeddocument_flag='" + signeddocument_flag + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<scanneduploaddocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new scanneduploaddocument
                    {
                        documenttype_code = dt["documenttype_code"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        //file_path = HttpContext.Current.Server.MapPath(dt["file_path"].ToString()),
                        file_path = objcmnstorage.EncryptData((dt["file_path"].ToString())),
                        file_name = dt["file_name"].ToString(),
                        scanneddocument_gid = dt["scanneddocument_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });
                    values.scanneduploaddocument = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }


        public void Dacancelphysicaluploaddocument(string physicaldocument_gid, result values)
        {
            msSQL = " delete from ocs_trn_tphysicaldocument where physicaldocument_gid='" + physicaldocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostMovedtoSignedDoc(movedtosigneddoc values, string user_gid)
        {
            var getdocumentid = DaGetvalueswithComma(values.scanneddocument_gid);
            msSQL = " update ocs_trn_tscanneddocument set signeddocument_flag='Y',signeddocument_updatedOn=Now(), " +
                    " signeddocument_updatedby='" + user_gid + "' " +
                    " where scanneddocument_gid in (" + getdocumentid + ")";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Selected Documents are Moved to Signed Documents Successfully..!";

                msSQL = " select application_gid from ocs_trn_tscanneddocument where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                string lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lsapplication_gid != "")
                {
                    result objvalues = new result();
                    DaupdatePhysicalAutomaticCompletion(lsapplication_gid, user_gid, objvalues);
                }
            }
            else
            {
                values.status = true;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostPhysicalDeferralTaggedDoc(deferraltagged values, string user_gid)
        {
            string lsstatus_update = "";
            int checklistcount = values.deferraltaggedchecklist.Count;
            List<deferraltaggedchecklist> DocumentVerifyChecklist = values.deferraltaggedchecklist.Where(a => a.documentverified == true && a.deferraltagged == false).ToList();
            List<deferraltaggedchecklist> DeferralTaggedChecklist = values.deferraltaggedchecklist.Where(a => a.deferraltagged == true && a.documentverified == false).ToList();
            int DocumentVerifycount = DocumentVerifyChecklist.Count; 

            foreach (var i in values.deferraltaggedchecklist)
            {
                string msGetchecklistGid = objcmnfunctions.GetMasterGID("DFCG");

                msSQL = " insert into ocs_trn_tdeferralchecklist( " +
                  " deferralchecklist_gid," +
                  " groupdocumentchecklist_gid," +
                  " mstchecklist_gid, " +
                  " checklist_name," +
                  " document_verified, " +
                  " deferral_tagged, " +
                  " fromphysical_document, " +
                  " created_by," +
                  " created_date" +
                  " )values(" +
                  "'" + msGetchecklistGid + "'," +
                  "'" + values.documentcheckdtl_gid + "'," +
                  "'" + i.mstchecklist_gid + "'," +
                  "'" + i.checklist_name + "'," +
                  "" + i.documentverified + "," +
                  "" + i.deferraltagged + "," +
                  "'Y', " +
                  "'" + user_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                 
            }

            if (checklistcount == DocumentVerifycount) // All Document Satisfied
            {
                lsstatus_update = "Satisfied";
                if (mnResult != 0)
                {
                    mdldocumentconfirmation objvalues = new mdldocumentconfirmation();
                    objvalues.overall_docstatus = "Satisfied";
                    objvalues.documentcheckdtl_gid = values.documentcheckdtl_gid;
                    objvalues.documentconfirmation_remarks = "";
                    DaPostPhysicalDocumentConfirmationDoc(objvalues, user_gid);
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document Satisfied Successfully..!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured..!";
                    }
                }
            }
            else
            {
                lsstatus_update = "Not Satisfied";
                if (mnResult != 0)
                {
                    msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='Not Satisfied' " +
                            " where documentcheckdtl_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='Not Satisfied' " +
                            " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='Not Satisfied' " +
                            " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult == 1)
                {
                    foreach (var i in DeferralTaggedChecklist)
                    {
                        msSQL = " insert into ocs_trn_tnotcleardocchecklist( " +
                          " documentcheckdtl_gid," +
                          " mstchecklist_gid, " +
                          " checklist_name," +
                          " fromphysical_document," +
                          " created_by," +
                          " created_date" +
                          " )values(" +
                          "'" + values.documentcheckdtl_gid + "'," +
                          "'" + i.mstchecklist_gid + "'," +
                          "'" + i.checklist_name + "'," +
                          "'Y'," +
                          "'" + user_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Document Status updated to not satisfied..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }

            msSQL = " insert into ocs_trn_tcadscannedphysicalstatusupdatelog( " +
               " application_gid," +
               " groupdocumentchecklist_gid," +
               " covenant_type, " +
               " scanneddoc_flag," +
               " fromphysical_document, " +
               " status_update, " +
               " created_by," +
               " created_date" +
               " )values(" +
               "'" + values.application_gid + "'," +
               "'" + values.documentcheckdtl_gid + "'," +
               "'" + values.covenant_type + "'," +
               "'" + values.scanneddoc_flag + "'," +
               "'Y'," +
               "'" + lsstatus_update + "'," +
               "'" + user_gid + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);  
        }

        public void DaUpdatePhysicalDeferralTaggedDoc(deferraltagged values, string user_gid)
        {
            string lsstatus_update = "";
            List<deferraltaggedchecklist> documentverifiedCount = values.deferraltaggedchecklist.Where(a => a.documentverified == true && a.deferraltagged == false).ToList();
            List<deferraltaggedchecklist> TagtodeferralChecklist = values.deferraltaggedchecklist.Where(a => a.deferraltagged == true && a.documentverified == false).ToList();
            int docverifiedCount = documentverifiedCount.Count;
            int overallchecklistcount = values.deferraltaggedchecklist.Count;
            string lsdeferraltagdoc_gid = "";
            msSQL = " select deferraltagdoc_gid from ocs_trn_tdeferraltagdoc " +
                    " where groupdocumentchecklist_gid= '" + values.documentcheckdtl_gid + "' and fromphysical_document='Y' order by deferraltagdoc_gid desc limit 1";
            //msSQL = " select deferraltagdoc_gid from ocs_trn_tdeferraltagdoc " +
            //        " where groupdocumentchecklist_gid = '" + values.documentcheckdtl_gid + "' and deferraltag_status='" + deferralTagstatus.Active + "'";
            lsdeferraltagdoc_gid = objdbconn.GetExecuteScalar(msSQL);
            foreach (var i in values.deferraltaggedchecklist)
            {
                msSQL = " update ocs_trn_tdeferralchecklist set document_verified=" + i.documentverified + ", " +
                        " deferral_tagged=" + i.deferraltagged + "," +
                        " updated_by ='" + user_gid + "', " +
                        " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where deferralchecklist_gid ='" + i.deferralchecklist_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (overallchecklistcount == docverifiedCount)
            {
                if (lsdeferraltagdoc_gid != "")
                {
                    msSQL = "update ocs_trn_tdeferraltagdoc set deferraltag_status='" + deferralTagstatus.DeferralTaken + "' where deferraltagdoc_gid='" + lsdeferraltagdoc_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                lsstatus_update = "Satisfied";
                if (mnResult != 0)
                {
                    mdldocumentconfirmation objvalues = new mdldocumentconfirmation();
                    objvalues.overall_docstatus = "Satisfied";
                    objvalues.documentcheckdtl_gid = values.documentcheckdtl_gid;
                    objvalues.documentconfirmation_remarks = "";
                    DaPostPhysicalDocumentConfirmationDoc(objvalues, user_gid);
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document Satisfied Successfully..!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured..!";
                    }
                }
            }
            if (TagtodeferralChecklist.Count > 0)
            {

                if (lsdeferraltagdoc_gid != "")
                {
                    msSQL = "update ocs_trn_tdeferraltagdoc set deferraltag_status='" + deferralTagstatus.Inactive + "' where deferraltagdoc_gid='" + lsdeferraltagdoc_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                lsstatus_update = "Not Satisfied";
                if (mnResult != 0)
                {
                    msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='Not Satisfied' " +
                            " where documentcheckdtl_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='Not Satisfied' " +
                            " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='Not Satisfied' " +
                            " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult == 1)
                {
                    msSQL = " delete from ocs_trn_tnotcleardocchecklist where documentcheckdtl_gid='" + values.documentcheckdtl_gid + "'" +
                            " and fromphysical_document='Y'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    foreach (var i in TagtodeferralChecklist)
                    {
                        msSQL = " insert into ocs_trn_tnotcleardocchecklist( " +
                          " documentcheckdtl_gid," +
                          " mstchecklist_gid, " +
                          " fromphysical_document, " +
                          " checklist_name," +
                          " created_by," +
                          " created_date" +
                          " )values(" +
                          "'" + values.documentcheckdtl_gid + "'," +
                          "'" + i.mstchecklist_gid + "'," +
                          "'Y'," +
                          "'" + i.checklist_name + "'," +
                          "'" + user_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
 
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Document Status updated to not satisfied..!";
                }
                else
                {
                    values.status = true;
                    values.message = "Error Occured..!";
                }
            }
            msSQL = " insert into ocs_trn_tcadscannedphysicalstatusupdatelog( " +
          " application_gid," +
          " groupdocumentchecklist_gid," +
          " covenant_type, " +
          " scanneddoc_flag," +
          " status_update, " +
          " fromphysical_document, " +
          " created_by," +
          " created_date" +
          " )values(" +
          "'" + values.application_gid + "'," +
          "'" + values.documentcheckdtl_gid + "'," +
          "'" + values.covenant_type + "'," +
          "'" + values.scanneddoc_flag + "'," +
          "'" + lsstatus_update + "'," +
          "'Y'," +
          "'" + user_gid + "'," +
          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }


        public void DaGettaggedDeferralinfo(string documentcheckdtl_gid, deferraltagged values)
        {
            msSQL = " select deferraltagdoc_gid,documentseverity_gid,documentseverity_name,tracking_id,tagged_to, " +
                    " date_format(due_date,'%d-%m-%Y') as due_date, due_date as Duedate, deferraltag_reason  from ocs_trn_tdeferraltagdoc " +
                    " where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "' and fromphysical_document='Y' and deferraltag_status='" + deferralTagstatus.Active + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.deferraltagdoc_gid = objODBCDataReader["deferraltagdoc_gid"].ToString();
                values.documentseverity_gid = objODBCDataReader["documentseverity_gid"].ToString();
                values.documentseverity_name = objODBCDataReader["documentseverity_name"].ToString();
                values.tracking_id = objODBCDataReader["tracking_id"].ToString();
                values.tagged_to = objODBCDataReader["tagged_to"].ToString();
                values.due_date = objODBCDataReader["due_date"].ToString();
                if (values.due_date != "")
                    values.Duedate = Convert.ToDateTime(objODBCDataReader["Duedate"].ToString());
                values.cad_remarks = objODBCDataReader["deferraltag_reason"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " select deferralchecklist_gid from ocs_trn_ttagquery where query_status='Query Raised' " +
                   " and groupdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getRaisedQuerygid = cmnfunctions.ConvertDataTable<MdlTagQueryCheckpoint>(dt_datatable);

            msSQL = " select deferralchecklist_gid,mstchecklist_gid,checklist_name,document_verified,deferral_tagged from ocs_trn_tdeferralchecklist " +
                    " where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "' and fromphysical_document = 'Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<deferraltaggedchecklist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsquery_flag = "N";
                    var lsraiseedquery = getRaisedQuerygid.Where(a => a.deferralchecklist_gid == dt["deferralchecklist_gid"].ToString()).FirstOrDefault();
                    if (lsraiseedquery != null)
                        lsquery_flag = "Y";
                    getdocumentdtlList.Add(new deferraltaggedchecklist
                    {
                        mstchecklist_gid = dt["mstchecklist_gid"].ToString(),
                        checklist_name = dt["checklist_name"].ToString(),
                        deferralchecklist_gid = dt["deferralchecklist_gid"].ToString(),
                        documentverified = Convert.ToBoolean(dt["document_verified"]),
                        deferraltagged = Convert.ToBoolean(dt["deferral_tagged"]),
                        query_flag = lsquery_flag,
                    });
                    values.deferraltaggedchecklist = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGettaggedHistoryDeferralinfo(string deferraltagdoc_gid, deferraltagged values)
        {
            msSQL = " select deferraltag_reason  from ocs_trn_tdeferraltagdoc " +
                    " where deferraltagdoc_gid='" + deferraltagdoc_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.cad_remarks = objODBCDataReader["deferraltag_reason"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " select checklist_name from ocs_trn_tdeferraltagdocchecklist " +
                    " where deferraltagdoc_gid='" + deferraltagdoc_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<deferraltaggedchecklist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new deferraltaggedchecklist
                    {
                        checklist_name = dt["checklist_name"].ToString(),
                    });
                    values.deferraltaggedchecklist = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetPhysicalMStDeferraltag(string documentcheckdtl_gid, string lstype, Mstdeferraltag values)
        {
            msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                    " where groupcovdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
            string lscovenant = objdbconn.GetExecuteScalar(msSQL);

            if (lscovenant == "")
            {
                if (lstype == "Institution")
                {
                    msSQL = " select documentseverity_gid,documentseverity_name,a.mstdocument_gid from  ocs_trn_tcadgroupdocumentchecklist a " +
                      " left join ocs_mst_tcompanydocument b on a.mstdocument_gid = b.companydocument_gid " +
                      " where a.groupdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        values.documentseverity_gid = objODBCDataReader["documentseverity_gid"].ToString();
                        values.documentseverity_name = objODBCDataReader["documentseverity_name"].ToString();
                        values.companydocument_gid = objODBCDataReader["mstdocument_gid"].ToString();
                    }
                    objODBCDataReader.Close();

                    msSQL = " select companychecklist_gid, checklist_name from  ocs_mst_tcompanychecklist " +
                            " where companydocument_gid ='" + values.companydocument_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklist_list = new List<MstChecklist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getchecklist_list.Add(new MstChecklist
                            {
                                mstchecklist_gid = dt["companychecklist_gid"].ToString(),
                                checklist_name = dt["checklist_name"].ToString(),
                            });
                            values.MstChecklist = getchecklist_list;
                        }
                    }
                    dt_datatable.Dispose();
                }
                else if (lstype == "Individual")
                {
                    msSQL = " select documentseverity_gid,documentseverity_name,a.mstdocument_gid from  ocs_trn_tcadgroupdocumentchecklist a " +
                     " left join ocs_mst_tindividualdocument b on a.mstdocument_gid = b.individualdocument_gid " +
                     " where a.groupdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        values.documentseverity_gid = objODBCDataReader["documentseverity_gid"].ToString();
                        values.documentseverity_name = objODBCDataReader["documentseverity_name"].ToString();
                        values.companydocument_gid = objODBCDataReader["mstdocument_gid"].ToString();
                    }
                    objODBCDataReader.Close();

                    msSQL = " select individualchecklist_gid,checklist_name from  ocs_mst_tindividualchecklist " +
                            " where individualdocument_gid ='" + values.companydocument_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklist_list = new List<MstChecklist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getchecklist_list.Add(new MstChecklist
                            {
                                mstchecklist_gid = dt["individualchecklist_gid"].ToString(),
                                checklist_name = dt["checklist_name"].ToString(),
                            });
                            values.MstChecklist = getchecklist_list;
                        }
                    }
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " select documentseverity_gid,documentseverity_name,a.mstdocument_gid from  ocs_trn_tcadgroupdocumentchecklist a " +
                     " left join ocs_mst_tgroupdocument b on a.mstdocument_gid = b.groupdocument_gid " +
                     " where a.groupdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        values.documentseverity_gid = objODBCDataReader["documentseverity_gid"].ToString();
                        values.documentseverity_name = objODBCDataReader["documentseverity_name"].ToString();
                        values.companydocument_gid = objODBCDataReader["mstdocument_gid"].ToString();
                    }
                    objODBCDataReader.Close();

                    msSQL = " select  groupchecklist_gid,checklist_name from ocs_mst_tgroupchecklist " +
                            " where groupdocument_gid ='" + values.companydocument_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklist_list = new List<MstChecklist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getchecklist_list.Add(new MstChecklist
                            {
                                mstchecklist_gid = dt["groupchecklist_gid"].ToString(),
                                checklist_name = dt["checklist_name"].ToString(),
                            });
                            values.MstChecklist = getchecklist_list;
                        }
                    }
                    dt_datatable.Dispose();
                }
            }
            else
            {
                if (lstype == "Institution")
                {
                    msSQL = " select documentseverity_gid,documentseverity_name,a.mstdocument_gid from  ocs_trn_tcadgroupcovenantdocumentchecklist a " +
                      " left join ocs_mst_tcompanydocument b on a.mstdocument_gid = b.companydocument_gid " +
                      " where a.groupcovdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        values.documentseverity_gid = objODBCDataReader["documentseverity_gid"].ToString();
                        values.documentseverity_name = objODBCDataReader["documentseverity_name"].ToString();
                        values.companydocument_gid = objODBCDataReader["mstdocument_gid"].ToString();
                    }
                    objODBCDataReader.Close();

                    msSQL = " select companychecklist_gid, checklist_name from  ocs_mst_tcompanychecklist " +
                            " where companydocument_gid ='" + values.companydocument_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklist_list = new List<MstChecklist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getchecklist_list.Add(new MstChecklist
                            {
                                mstchecklist_gid = dt["companychecklist_gid"].ToString(),
                                checklist_name = dt["checklist_name"].ToString(),
                            });
                            values.MstChecklist = getchecklist_list;
                        }
                    }
                    dt_datatable.Dispose();
                }
                else if (lstype == "Individual")
                {
                    msSQL = " select documentseverity_gid,documentseverity_name,a.mstdocument_gid from  ocs_trn_tcadgroupcovenantdocumentchecklist a " +
                     " left join ocs_mst_tindividualdocument b on a.mstdocument_gid = b.individualdocument_gid " +
                     " where a.groupcovdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        values.documentseverity_gid = objODBCDataReader["documentseverity_gid"].ToString();
                        values.documentseverity_name = objODBCDataReader["documentseverity_name"].ToString();
                        values.companydocument_gid = objODBCDataReader["mstdocument_gid"].ToString();
                    }
                    objODBCDataReader.Close();

                    msSQL = " select individualchecklist_gid,checklist_name from  ocs_mst_tindividualchecklist " +
                            " where individualdocument_gid ='" + values.companydocument_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklist_list = new List<MstChecklist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getchecklist_list.Add(new MstChecklist
                            {
                                mstchecklist_gid = dt["individualchecklist_gid"].ToString(),
                                checklist_name = dt["checklist_name"].ToString(),
                            });
                            values.MstChecklist = getchecklist_list;
                        }
                    }
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " select documentseverity_gid,documentseverity_name,a.mstdocument_gid from  ocs_trn_tcadgroupcovenantdocumentchecklist a " +
                     " left join ocs_mst_tgroupdocument b on a.mstdocument_gid = b.groupdocument_gid " +
                     " where a.groupcovdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        values.documentseverity_gid = objODBCDataReader["documentseverity_gid"].ToString();
                        values.documentseverity_name = objODBCDataReader["documentseverity_name"].ToString();
                        values.companydocument_gid = objODBCDataReader["mstdocument_gid"].ToString();
                    }
                    objODBCDataReader.Close();

                    msSQL = " select  groupchecklist_gid,checklist_name from ocs_mst_tgroupchecklist " +
                            " where groupdocument_gid ='" + values.companydocument_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklist_list = new List<MstChecklist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getchecklist_list.Add(new MstChecklist
                            {
                                mstchecklist_gid = dt["groupchecklist_gid"].ToString(),
                                checklist_name = dt["checklist_name"].ToString(),
                            });
                            values.MstChecklist = getchecklist_list;
                        }
                    }
                    dt_datatable.Dispose();
                }
            }


        }

        //---- Query --------//
        public void DaPostAppcadPhysicalqueryadd(mdlcadquery values, string user_gid)
        {
            msSQL = " select c.user_gid, concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as query_toname from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where application_gid = '" + values.application_gid + "' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.query_to = objODBCDataReader["user_gid"].ToString();
                values.query_toname = objODBCDataReader["query_toname"].ToString();
            }
            objODBCDataReader.Close();

            int query_no = 0;
            var lsquery_no = objdbconn.GetExecuteScalar("SELECT COUNT(*) FROM ocs_trn_ttagquery WHERE groupdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "' AND application_gid ='" + values.application_gid + "' AND  fromphysical_document = 'Y'  and query_status not in ('Pending','Cancelled')");
            if (lsquery_no == "")
            {
                query_no = 1;
            }
            else
            {
                query_no = Convert.ToInt16(lsquery_no) + 1;
            }
            values.query_code = objcmnfunctions.GetMasterGID("CQCG");
            msGetGid = objcmnfunctions.GetMasterGID("TGQG");
            string lsraisequerystatus = "";
            if (values.maker_flag == "Y")
                lsraisequerystatus = Raisequerystatus.Pending;
            else
                lsraisequerystatus = Raisequerystatus.QueryRaised;  
            msSQL = " insert into ocs_trn_ttagquery(" +
                     " tagquery_gid," +
                     " groupdocumentchecklist_gid," +
                     " query_code, " +
                     " document_gid, " +
                     " document_name, " +
                     " application_gid, " +
                     " query_title," +
                     " query_description," +
                     " query_status," +
                     " query_to," +
                     " query_toname," +
                     " fromphysical_document," +
                     " deferralchecklist_gid, " +
                     " deferralchecklist_name, ";
                    if (values.maker_flag == "Y")
                    {
                    }
                    else
                    {
                        msSQL += " query_no,";
                    }
            msSQL += " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.documentcheckdtl_gid + "', " +
                     "'" + values.query_code + "'," +
                     "'" + values.document_gid + "'," +
                     "'" + values.document_name + "'," +
                     "'" + values.application_gid + "'," +
                     "'" + values.query_title + "'," +
                     "'" + values.query_description.Replace("'", "") + "'," +
                     "'" + lsraisequerystatus + "'," +
                     "'" + values.query_to + "'," +
                     "'" + values.query_toname + "'," +
                     "'Y'," +
                     "'" + values.deferralchecklist_gid + "'," +
                     "'" + values.deferralchecklist_name + "',";
                    if (values.maker_flag == "Y")
                    {
                    }
                    else
                    {
                        msSQL += "'" + query_no + "',";
                    }
            msSQL += "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_trn_ttagquerydocument set tagquery_gid='" + msGetGid + "' where tagquery_gid='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1 && values.maker_flag != "Y")
            {
                msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                  " where groupcovdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "'";
                string lscovenant = objdbconn.GetExecuteScalar(msSQL);
                if (lscovenant != "")
                {
                    msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physicaloverall_docstatus='Query - Raised' " +
                          " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='Query - Raised', physicalcopyquerystatus='Query - Raised' " +
                            " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='Query - Raised' " +
                            " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='Query - Raised', physicalcopyquerystatus='Query - Raised' " +
                            " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult == 1 && values.maker_flag != "Y")
            {
                fnSendRMraisequeryMail(values.documentcheckdtl_gid, msGetGid);

                values.status = true;
                values.message = "Query Raised Successfully..!";
            }
            else if (mnResult == 1 && values.maker_flag == "Y")
            {
                values.status = true;
                values.message = "Query Raised Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            } 
        }



        public void DaPostAppcadresponsequery(mdlcadquery values, string user_gid)
        {

            msSQL = " update ocs_trn_ttagquery set query_responseremarks ='" + values.query_responseremarks.Replace("'", "") + "'," +
                    " queryclosed_status='" + values.queryclosed_status + "'," +
                   " query_responsedby='" + user_gid + "'," +
                   " query_responseddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " query_status='Closed' " +
                   " where tagquery_gid='" + values.tagquery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (values.queryclosed_status == Closequerystatus.ReDocumentSubmission)
            {
                msSQL = " select physicaldocument_code from ocs_trn_tphysicaldocument " +
                        " where physicaldocument_gid='" + values.document_gid + "'";
                string lsscanneddocument_code = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tphysicaldocument set " +
                        " groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "', " +
                        " replacementdocument_code = '" + lsscanneddocument_code + "' " +
                        " where groupdocumentchecklist_gid='" + values.tagquery_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " select  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as raised_by,query_title,query_description,application_gid,date_format(a.query_responseddate,'%d-%m-%Y %h:%i %p') as query_responseddate from ocs_trn_ttagquery a " +
                    "  left join adm_mst_tuser g on g.user_gid = a.created_by  " +
                    " where tagquery_gid='" + values.tagquery_gid + "'";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                ls_raised_by = objODBCDatareader1["raised_by"].ToString();
                ls_query_responseddate = objODBCDatareader1["query_responseddate"].ToString();
                ls_query_title = objODBCDatareader1["query_title"].ToString();
                ls_query_description = objODBCDatareader1["query_description"].ToString();
                ls_application_gid = objODBCDatareader1["application_gid"].ToString();
            }
            objODBCDatareader1.Close();

            if (mnResult == 1)
            {
                msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                 " where groupcovdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "'";
                string lscovenant = objdbconn.GetExecuteScalar(msSQL);
                if (lscovenant != "")
                {
                    msSQL = " select tagquery_gid from ocs_trn_ttagquery " +
                               " where groupdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "' and query_status in ('Pending','Query Raised') AND  fromphysical_document = 'Y'" +
                               " and application_gid = '" + ls_application_gid + "'";
                    string tagquery_status = objdbconn.GetExecuteScalar(msSQL);
                  
                    if (string.IsNullOrEmpty(tagquery_status))
                    {
                        msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physicaloverall_docstatus='Query - Closed' " +
                   " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='Query - Closed', physicalcopyquerystatus='Query - Closed' " +
                                " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
             
                }
                else
                {
                    msSQL = " select tagquery_gid from ocs_trn_ttagquery " +
                               " where groupdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "' and query_status in ('Pending','Query Raised') AND  fromphysical_document = 'Y'" +
                               " and application_gid = '" + ls_application_gid + "'";
                    string tagquery_status = objdbconn.GetExecuteScalar(msSQL);

                    if (string.IsNullOrEmpty(tagquery_status))
                    {
                        msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='Query - Closed' " +
                          " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='Query - Closed', physicalcopyquerystatus='Query - Closed' " +
                                " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                 
                }
            }
            if (mnResult == 1)
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
                msSQL = " select relationshipmanager_gid,relationshipmanager_name,customerref_name,application_no from ocs_trn_tcadapplication where application_gid = '" + ls_application_gid + "'";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == true)
                { 
                    ls_relationshipmanager_gid = objODBCDatareader1["relationshipmanager_gid"].ToString();
                    ls_relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                    ls_customerref_name = objODBCDatareader1["customerref_name"].ToString();
                    ls_application_no = objODBCDatareader1["application_no"].ToString(); 
                }
                objODBCDatareader1.Close();

                msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                  " where groupcovdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "'";
                string lscovenant = objdbconn.GetExecuteScalar(msSQL);
                if (lscovenant != "")
                {
                    msSQL = " select mstdocument_name as documenttype_name  from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                          " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    documenttype_name = objdbconn.GetExecuteScalar(msSQL);


                }
                else
                {
                    msSQL = " select mstdocument_name as documenttype_name  from ocs_trn_tcadgroupdocumentchecklist " +
                           " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    documenttype_name = objdbconn.GetExecuteScalar(msSQL);
                }


                msSQL = " select institution_gid from  ocs_trn_tcadinstitution where stakeholder_type = 'Applicant' and application_gid = '" + ls_application_gid + "'";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == true)
                {
                    msSQL = " select company_name from  ocs_trn_tcadinstitution where stakeholder_type = 'Applicant' and application_gid = '" + ls_application_gid + "'";
                    lscompany_name = objdbconn.GetExecuteScalar(msSQL);
                }
                else
                {
                    msSQL = " select concat(first_name,middle_name,last_name) from  ocs_trn_tcadcontact where stakeholder_type = 'Applicant' and application_gid = '" + ls_application_gid + "'";
                    lscompany_name = objdbconn.GetExecuteScalar(msSQL);
                } 

                msSQL = " select b.employee_emailid from ocs_trn_tprocesstype_assign a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.maker_gid " +
                        " where application_gid = '" + ls_application_gid + "' ";
                to_mailid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select c.employee_emailid from ocs_trn_tprocesstype_assign a " +
                        " left join hrm_mst_temployee c on c.employee_gid = a.checker_gid " +
                        " where application_gid = '" + ls_application_gid + "' ";
                to1_mailid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + ls_relationshipmanager_gid + "'";
                cc1_mailid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select c.employee_emailid from ocs_trn_tcadapplication a " +
                        " left join hrm_mst_temployee c on c.employee_gid = a.clustermanager_gid " +
                        " where application_gid = '" + ls_application_gid + "' ";
                cc2_mailid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select c.employee_emailid from ocs_trn_tcadapplication a " +
                       " left join hrm_mst_temployee c on c.employee_gid = a.drm_gid " +
                       " where application_gid = '" + ls_application_gid + "' ";
                cc3_mailid = objdbconn.GetExecuteScalar(msSQL);

                sub1 = "\"" + lscompany_name + "\"   \"" + ls_application_no + "\" – A query has been raised by CAD ";
                body1 = body1 + "<br />";
                body1 = body1 + " &nbsp&nbsp Dear \" CAD team\",";
                body1 = body1 + "<br />";
                body1 = body1 + "<br />&nbsp&nbsp The RM has closed the query raised by you. Please login CAD module and do the needful ";
                body1 = body1 + "<br />";
                body1 = body1 + "<br />&nbsp&nbsp <b>Query Title:</b> " + ls_query_title + "<br /><br />";
                body1 = body1 + "&nbsp&nbsp <b>Query Description:</b> " + ls_query_description.Replace("'", "") + "<br /><br />";
                body1 = body1 + "&nbsp&nbsp <b>Document name:  </b>" + documenttype_name + "<br /><br />";
                body1 = body1 + "&nbsp&nbsp <b>RM name:</b> " + ls_relationshipmanager_name + "<br /><br />";
                body1 = body1 + "&nbsp&nbsp <b>Query raised by:</b> " + ls_raised_by + "<br /><br />";
                body1 = body1 + "&nbsp&nbsp <b>Query Closure time:  </b>" + ls_query_responseddate + "<br /><br />";
                body1 = body1 + "<br />";
                body1 = body1 + "&nbsp&nbsp This is a system generated mail, do not reply <br /> ";
                body1 = body1 + "<br />";

                //body = body + "</td><td>&nbsp&nbsp</td></tr></table>";


                string[] tomail_array = to_mailid.Split(',');
                string[] tomail1_array = to1_mailid.Split(',');
                tomail_array = tomail1_array.Union(tomail_array).ToArray();

                string[] ccmail1_array = cc1_mailid.Split(',');
                string[] ccmail2_array = cc2_mailid.Split(',');
                string[] ccmail_array = cc3_mailid.Split(',');
                ccmail2_array = ccmail1_array.Union(ccmail2_array).ToArray();
                ccmail_array = ccmail2_array.Union(ccmail_array).ToArray();

                cc_mailid = string.Join(",", ccmail_array);
                tomail_id = string.Join(",", tomail_array);

                MailMessage message1 = new MailMessage();
                SmtpClient smtp1 = new SmtpClient();
                message1.From = new MailAddress(ls_username);
                //message1.To.Add(new MailAddress(tomail_id1));
                string[] lstoReceipients;
                if (tomail_id != null & tomail_id != string.Empty & tomail_id != "")
                {
                    lstoReceipients = tomail_array;
                    if (tomail_id.Length == 0)
                    {
                        message1.To.Add(new MailAddress(tomail_id));
                    }
                    else
                    {
                        foreach (string tomail_id in tomail_array)
                        {
                            message1.To.Add(new MailAddress(tomail_id));
                        }
                    }
                }

                if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                {
                    lsCCReceipients = ccmail_array;
                    if (cc_mailid.Length == 0)
                    {
                        message1.CC.Add(new MailAddress(cc_mailid));
                    }
                    else
                    {
                        foreach (string CCEmail in ccmail_array)
                        {
                            message1.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        }
                    }
                }
                lsBccmail_id = ConfigurationManager.AppSettings["QuerylBccMail"].ToString();
                if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                {
                    lsBCCReceipients = lsBccmail_id.Split(',');
                    if (lsBccmail_id.Length == 0)
                    {
                        message1.Bcc.Add(new MailAddress(lsBccmail_id));
                    }
                    else
                    {
                        foreach (string BCCEmail in lsBCCReceipients)
                        {
                            message1.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                        }
                    }
                }


                message1.Subject = sub1;
                message1.IsBodyHtml = true; //to make message body as html  
                message1.Body = body1;
                smtp1.Port = ls_port;
                smtp1.Host = ls_server; //for gmail host  
                smtp1.EnableSsl = true;
                smtp1.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp1.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp1.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp1.Send(message1);


                values.status = true;
                values.message = "Query Closed Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetPhysicalAppcadQuerySummary(mslcadquerylist values, string documentcheckdtl_gid)
        {

            msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                 " where groupcovdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
            string lscovenant = objdbconn.GetExecuteScalar(msSQL);
            if (lscovenant != "")
            {
                msSQL = " select mstdocumenttype_name as documenttype_code,mstdocument_name as documenttype_name, " +
                        " date_format(physicaldocumentsubmission_date, '%d-%m-%Y') as documentsubmission_date from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                        " where groupcovdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
            }
            else
            {
                msSQL = " select mstdocumenttype_name as documenttype_code,mstdocument_name as documenttype_name, " +
                        " date_format(physicaldocumentsubmission_date, '%d-%m-%Y') as documentsubmission_date from ocs_trn_tcadgroupdocumentchecklist " +
                        " where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
            }
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.documenttype_code = objODBCDataReader["documenttype_code"].ToString();
                values.documenttype_name = objODBCDataReader["documenttype_name"].ToString();
                values.documentsubmission_date = objODBCDataReader["documentsubmission_date"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " select (select count(*) from ocs_trn_tdeferralchecklist x where x.groupdocumentchecklist_gid =  '" + documentcheckdtl_gid + "' " +
                " and x.fromphysical_document = 'Y' LIMIT 1) as checklistcount, " +
                " (SELECT COUNT(*) from ocs_trn_tinitiateextendorwaiver z " +
                " where z.groupdocumentchecklist_gid ='" + documentcheckdtl_gid + "' and z.fromphysical_document = 'Y' " +
                " and approval_status in ('Pending', 'Approved') and activity_type = 'Waiver') as waiverpendingcount, " +
                " (select deferraltagdoc_gid  from ocs_trn_tdeferraltagdoc where groupdocumentchecklist_gid ='" + documentcheckdtl_gid + "' " +
                " and deferraltag_status in ('1','2','3') and fromphysical_document = 'Y' order by deferraltagdoc_gid desc limit 1) as deferraltagdoc_gid ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checklistcount = objODBCDatareader["checklistcount"].ToString();
                values.waiverpendingcount = objODBCDatareader["waiverpendingcount"].ToString();
                values.deferraltagdoc_gid = objODBCDatareader["deferraltagdoc_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select tagquery_gid,document_gid,groupdocumentchecklist_gid, query_title,query_description,query_status,query_to,query_toname, " +
                    " query_responseremarks, date_format(a.query_responseddate, '%d-%m-%Y %h:%i %p') as query_responseddate, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as query_responsedby, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as query_raisedby, " +
                    " query_code, document_name, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as query_raiseddate  " +
                    " from ocs_trn_ttagquery a" +
                    " left join adm_mst_tuser b on a.query_responsedby = b.user_gid " +
                    " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                    " where groupdocumentchecklist_gid = '" + documentcheckdtl_gid + "' and a.fromphysical_document='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.mdlcadquery = dt_datatable.AsEnumerable().Select(row => new mdlcadquery
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    tagquery_gid = row["tagquery_gid"].ToString(),
                    query_title = row["query_title"].ToString(),
                    query_description = row["query_description"].ToString(),
                    query_status = row["query_status"].ToString(),
                    query_toname = row["query_toname"].ToString(),
                    query_responseremarks = row["query_responseremarks"].ToString(),
                    query_responseddate = row["query_responseddate"].ToString(),
                    query_responsedby = row["query_responsedby"].ToString(),
                    created_by = row["query_raisedby"].ToString(),
                    created_date = row["query_raiseddate"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_gid = row["document_gid"].ToString(),
                    query_code = row["query_code"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
        }

        public bool DaPhysicalQueryDocumentUpload(HttpRequest httpRequest, result values, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lstagquery_gid = httpRequest.Form["tagquery_gid"].ToString();
            string lsdocumentcheckdtl_gid = httpRequest.Form["documentcheckdtl_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADPhysicalqueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                            values.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CADPhysicalqueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CADPhysicalqueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "Master/CADPhysicalqueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("TGQD");
                        msSQL = " insert into ocs_trn_ttagquerydocument( " +
                                    " tagquerydocument_gid," +
                                    " groupdocumentchecklist_gid," +
                                    " tagquery_gid, " +
                                    " document_name ," +
                                    " document_path," +
                                    " fromphysical_document, " +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lsdocumentcheckdtl_gid + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", " ") + "'," +
                                    "'Y'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            values.status = true;
                            values.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error Occured..!";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
            return true;
        }

        public void DaGetQueryDocument(string tagquery_gid, querydocumentlist values)
        {
            msSQL = " select tagquerydocument_gid,document_name,document_path, " +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date,concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by " +
                    " from ocs_trn_ttagquerydocument a" +
                    " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                    " where tagquery_gid='" + tagquery_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<queryuploaddocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new queryuploaddocument
                    {
                        //file_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        file_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        file_name = dt["document_name"].ToString(),
                        tagquerydocument_gid = dt["tagquerydocument_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });
                    values.queryuploaddocument = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetTmpPhysicalQueryDocument(string tagquery_gid, string user_gid, querydocumentlist values)
        {
            msSQL = " select tagquerydocument_gid,document_name,document_path, " +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date,concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by " +
                    " from ocs_trn_ttagquerydocument a" +
                    " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                    " where (tagquery_gid='" + tagquery_gid + "' or tagquery_gid='" + user_gid + "') and a.fromphysical_document='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<queryuploaddocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new queryuploaddocument
                    {
                        //file_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        file_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        file_name = dt["document_name"].ToString(),
                        tagquerydocument_gid = dt["tagquerydocument_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });
                    values.queryuploaddocument = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DatmpphysicalclearQueryuploaded(string user_gid, result values)
        {
            msSQL = " delete from ocs_trn_ttagquerydocument where tagquery_gid='" + user_gid + "' and fromphysical_document='Y'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
                values.status = true;
            else
                values.status = false;
        }

        public void DatmpclearRMuploaded(string user_gid, result values)
        {
            msSQL = " delete from ocs_trn_tscanneddocument where groupdocumentchecklist_gid='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
                values.status = true;
            else
                values.status = false;
        }


        public void DaGetRMSummary(string employee_gid, customerRMsummarylist values)
        {
            msSQL = " select a.application_gid, a.application_no, case when g.urn = '' then f.urn else g.urn end as customer_urn, vertical_name, " +
                   " a.customer_name as customer_name,a.approval_status,applicant_type, " +
                   " region,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as ccsubmitted_by, " +
                   " ccgroup_name,date_format(a.created_date, '%d-%m-%Y') as created_date,overalllimit_amount " +
                   " from ocs_trn_tcadapplication a " +
                   " left join hrm_mst_temployee d on a.ccsubmitted_by = d.employee_gid " +
                   " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                   " left join ocs_trn_tcadinstitution f on f.application_gid = a.application_gid " +
                   " left join ocs_trn_tcadcontact g on g.application_gid = a.application_gid " +
                   " where a.created_by = '" + employee_gid + "' and process_type = 'Accept' " +
                   " and (f.stakeholder_type in ('Applicant', 'Borrower') or g.stakeholder_type in ('Applicant','Borrower')) order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<customerRMsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new customerRMsummary
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.customerRMsummary = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaPostPhysicalInitiateExtensionorwaiver(mdlinitiateextendwaiver values, string user_gid)
        {

            if (values.activity_type == "Tag to Deferral")
            {
                msSQL = " select initiateextendorwaiver_gid  from ocs_trn_tinitiateextendorwaiver a " +
                        " where groupdocumentchecklist_gid = '" + values.documentcheckdtl_gid + "' and application_gid  = '" + values.application_gid + "' and approval_status not in ('Approved','Rejected') " +
                        " and activity_type = 'Tag to Deferral' and fromphysical_document='Y' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.status = false;
                    values.message = "Kindly Approve the Pending Tag to Deferral Record ";
                    objODBCDatareader.Close();
                    return;
                }
            }


            if (values.activity_type == "Extension")
            {
                msSQL = " select initiateextendorwaiver_gid from ocs_trn_tinitiateextendorwaiver " +
                        " where groupdocumentchecklist_gid = '" + values.documentcheckdtl_gid + "' and application_gid  = '" + values.application_gid + "' and approval_status not in ('Approved','Rejected') " +
                        " and activity_type = 'Extension' and fromphysical_document='Y'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.status = false;
                    values.message = "Kindly Approve the Pending Extension Record";
                    objODBCDatareader.Close();
                    return;
                }
            }


            if (values.activity_type == "Waiver")
            {
                msSQL = " select initiateextendorwaiver_gid from ocs_trn_tinitiateextendorwaiver a " +
                        " where groupdocumentchecklist_gid = '" + values.documentcheckdtl_gid + "' and application_gid  = '" + values.application_gid + "' and approval_status not in ('Approved','Rejected') " +
                        " and activity_type = 'Waiver' and fromphysical_document='Y'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.status = false;
                    values.message = "Kindly Approve the Pending Waiver Record";
                    objODBCDatareader.Close();
                    return;
                }
            }


            if (values.activity_type == "Extension")
            {
                msSQL = " select initiateextendorwaiver_gid from ocs_trn_tinitiateextendorwaiver a " +
                        " where groupdocumentchecklist_gid = '" + values.documentcheckdtl_gid + "' and application_gid  = '" + values.application_gid + "' and approval_status in ('Pending','Initiation Pending') " +
                        " and activity_type = 'Tag to Deferral' and fromphysical_document='Y'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.status = false;
                    values.message = "Kindly Approve the Pending Tag to Deferral Record";
                    objODBCDatareader.Close();
                    return;
                }
            }

            msGetGid = objcmnfunctions.GetMasterGID("IEWG");
            msSQL = " insert into ocs_trn_tinitiateextendorwaiver(" +
                     " initiateextendorwaiver_gid," +
                     " groupdocumentchecklist_gid," +
                     " application_gid, " +
                     " activity_type," +
                     " activity_title,";
            if (values.extendeddue_date == "" || values.extendeddue_date == null || Convert.ToDateTime(values.extendeddue_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
            }
            else
            {
                msSQL += " extendeddue_date,";
            }
            if (values.due_date == "" || values.due_date == null || Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00") { }
            else
            {
                msSQL += " due_date,";
            }
            msSQL += " reason," +
                     " approval_status," +
                     " fromphysical_document, " +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.documentcheckdtl_gid + "', " +
                     "'" + values.application_gid + "'," +
                     "'" + values.activity_type + "'," +
                     "'" + values.activity_title.Replace("'", "") + "',";
            if (values.extendeddue_date == "" || values.extendeddue_date == null || Convert.ToDateTime(values.extendeddue_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.extendeddue_date).ToString("yyyy-MM-dd") + "',";
            }
            if (values.due_date == "" || values.due_date == null || Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += "'" + values.reason.Replace("'", "") + "'," +
                     "'" + values.approval_status + "'," +
                     "'Y', " +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (values.tagquery_gid != "" && values.tagquery_gid != null)
            {
                msSQL = " update ocs_trn_ttagquery set query_responseremarks ='" + values.reason.Replace("'", "") + "'," +
                        " queryclosed_status='" + Closequerystatus.DeferralRequest + "'," +
                        " query_responsedby='" + user_gid + "'," +
                        " query_responseddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " query_status='Closed' " +
                        " where tagquery_gid='" + values.tagquery_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                " where groupcovdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "'";
                string lscovenant = objdbconn.GetExecuteScalar(msSQL);
                if (lscovenant != "")
                {
                    msSQL = " select tagquery_gid from ocs_trn_ttagquery " +
                               " where groupdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "' and query_status in ('Pending','Query Raised') AND  fromphysical_document = 'Y'" +
                               " and application_gid = '" + ls_application_gid + "'";
                    string tagquery_status = objdbconn.GetExecuteScalar(msSQL);

                    if (string.IsNullOrEmpty(tagquery_status))
                    {
                      

                        msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set  physicalcopyquerystatus='Query - Closed' " +
                                " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                else
                {
                    msSQL = " select tagquery_gid from ocs_trn_ttagquery " +
                               " where groupdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "' and query_status in ('Pending','Query Raised') AND  fromphysical_document = 'Y'" +
                               " and application_gid = '" + ls_application_gid + "'";
                    string tagquery_status = objdbconn.GetExecuteScalar(msSQL);

                    if (string.IsNullOrEmpty(tagquery_status))
                    {
                       

                        msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicalcopyquerystatus='Query - Closed' " +
                                " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
            }

            if (values.extendeddue_date != "" && Convert.ToDateTime(values.extendeddue_date).ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
            {


                msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                        " where groupcovdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "'";
                string lscovenant = objdbconn.GetExecuteScalar(msSQL);
                if (lscovenant != "")
                {
                    msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physical_extendedduedate ='" + Convert.ToDateTime(values.extendeddue_date).ToString("yyyy-MM-dd") + "' " +
                            " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physical_extendedduedate ='" + Convert.ToDateTime(values.extendeddue_date).ToString("yyyy-MM-dd") + "' " +
                            " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " update ocs_trn_tcaddocumentchecktls set physical_extendedduedate ='" + Convert.ToDateTime(values.extendeddue_date).ToString("yyyy-MM-dd") + "' " +
                            " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physical_extendedduedate ='" + Convert.ToDateTime(values.extendeddue_date).ToString("yyyy-MM-dd") + "' " +
                            " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            }
            if (values.activity_type == "Tag to Deferral")
            {
                mdlGroupDocStatus objvalues = new mdlGroupDocStatus();
                objvalues.application_gid = values.application_gid;
                objvalues.documentcheckdtl_gid = values.documentcheckdtl_gid;
                objvalues.covenant_type = values.covenant_type;
                objvalues.scanneddoc_flag = values.scanneddoc_flag;
                objvalues.credit_gid = values.credit_gid;
                //objvalues.documentseverity_gid = values.documentseverity_gid;
                //objvalues.documentseverity_name = values.documentseverity_name;
                //objvalues.tagged_to = values.tagged_to;
                objvalues.due_date = values.due_date;
                objvalues.cad_remarks = values.reason;
                objvalues.initiateextendorwaiver_gid = msGetGid;
                DaPostUpdateTagToDefStatus(objvalues, user_gid);
            }
            if (mnResult == 1 && values.mdlapproval != null)
            {
                foreach (var i in values.mdlapproval)
                {
                    string msGetApprovalGid = objcmnfunctions.GetMasterGID("EWAG");
                    msSQL = " insert into ocs_trn_textendorwaiverapproval(" +
                      " extendorwaiverapproval_gid," +
                      " initiateextendorwaiver_gid," +
                      " groupdocumentchecklist_gid, " +
                      " approval_gid," +
                      " approval_name," +
                      " approval_status," +
                      " fromphysical_document, " +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGetApprovalGid + "'," +
                      "'" + msGetGid + "', " +
                      "'" + values.documentcheckdtl_gid + "'," +
                      "'" + i.employee_gid + "'," +
                      "'" + i.employee_name + "'," +
                      "'Pending'," +
                      "'Y'," +
                      "'" + user_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            string lsstatus = "";
            if (values.activity_type == "Tag to Deferral")
                lsstatus = "Deferral Initiation Pending";
            else
                lsstatus = values.activity_type + " Initiation Pending";

            msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                       " where groupcovdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "'";
            string lscovenant1 = objdbconn.GetExecuteScalar(msSQL);
            if (lscovenant1 != "")
            {
                msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physicaloverall_docstatus='" + lsstatus + "' " +
                        " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='" + lsstatus + "' " +
                        " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='" + lsstatus + "' " +
                        " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='" + lsstatus + "' " +
                        " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Initiated Extension / Waiver Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostUpdateTagToDefStatus(mdlGroupDocStatus values, string user_gid)
        {
            msSQL = " insert into ocs_trn_tcadscannedphysicalstatusupdatelog( " +
                  " application_gid," +
                  " groupdocumentchecklist_gid," +
                  " covenant_type, " +
                  " scanneddoc_flag," +
                  " status_update, " +
                  " fromphysical_document, " +
                  " created_by," +
                  " created_date" +
                  " )values(" +
                  "'" + values.application_gid + "'," +
                  "'" + values.documentcheckdtl_gid + "'," +
                  "'" + values.covenant_type + "'," +
                  "'" + values.scanneddoc_flag + "'," +
                  "'Deferral Approval Pending'," +
                  "'Y'," +
                  "'" + user_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {

                string lsmsGetGid = objcmnfunctions.GetMasterGID("DTDG");
                string msGetTrackingId = objcmnfunctions.GetMasterGID("DT");

                msSQL = " insert into ocs_trn_tdeferraltagdoc( " +
                            " deferraltagdoc_gid," +
                            " initiateextendorwaiver_gid," +
                            " groupdocumentchecklist_gid," +
                            " application_gid, " +
                            " credit_gid," +
                            " documentseverity_gid, " +
                            " documentseverity_name, " +
                            " tracking_id ," +
                            " tagged_to ," +
                            " due_date," +
                            " deferraltag_reason," +
                            " deferraltag_status, " + 
                            " fromphysical_document, " +
                            " created_by," +
                            " created_date" +
                            " )values(" +
                            "'" + lsmsGetGid + "'," +
                            "'" + values.initiateextendorwaiver_gid + "'," +
                            "'" + values.documentcheckdtl_gid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.credit_gid + "'," +
                            "'" + values.documentseverity_gid + "'," +
                            "'" + values.documentseverity_name + "'," +
                            "'" + msGetTrackingId + "'," +
                            "'" + values.tagged_to + "'," +
                            "'" + Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd") + "'," +
                            "'" + values.cad_remarks.Replace("'", "") + "'," +
                            "'" + deferralTagstatus.ApprovalPending + "'," + 
                            "'Y', " +
                            "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tcaddocumentchecktls set overall_docstatus='Deferral Approval Initiated' " +
                        " where documentcheckdtl_gid='" + values.documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult == 1)
                {
                    msSQL = " select mstchecklist_gid,checklist_name,notcleardocchecklist_gid from ocs_trn_tnotcleardocchecklist where documentcheckdtl_gid='" + values.documentcheckdtl_gid + "' and " +
                            " fromphysical_document='Y'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            string msGetchecklistGid = objcmnfunctions.GetMasterGID("DTCL");
                            msSQL = " insert into ocs_trn_tdeferraltagdocchecklist( " +
                              " deferraltagdocchecklist_gid," +
                              " deferraltagdoc_gid," +
                              " mstchecklist_gid, " +
                              " checklist_name," +
                              " created_by," +
                              " created_date" +
                              " )values(" +
                              "'" + msGetchecklistGid + "'," +
                              "'" + lsmsGetGid + "'," +
                              "'" + dt["mstchecklist_gid"].ToString() + "'," +
                              "'" + dt["checklist_name"].ToString() + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                msSQL = "delete from ocs_trn_tnotcleardocchecklist where notcleardocchecklist_gid = '" + dt["notcleardocchecklist_gid"].ToString() + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                    dt_datatable.Dispose();
                }
            }
        }

        public void DaGetInitiatedExtensionorwaiver(mdlinitiateextendwaiverlist values, string documentcheckdtl_gid)
        {
            msSQL = " select initiateextendorwaiver_gid,approval_initiation,groupdocumentchecklist_gid,activity_type,activity_title, " +
                    " date_format(a.due_date, '%d-%m-%Y') as due_date, " +
                    " date_format(a.extendeddue_date, '%d-%m-%Y') as extendeddue_date,reason,approval_status, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from ocs_trn_tinitiateextendorwaiver a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where groupdocumentchecklist_gid = '" + documentcheckdtl_gid + "' and fromphysical_document='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.mdlinitiateextendwaiver = dt_datatable.AsEnumerable().Select(row => new mdlinitiateextendwaiver
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    initiateextendorwaiver_gid = row["initiateextendorwaiver_gid"].ToString(),
                    activity_type = row["activity_type"].ToString(),
                    activity_title = row["activity_title"].ToString(),
                    extendeddue_date = row["extendeddue_date"].ToString(),
                    reason = row["reason"].ToString(),
                    approval_status = row["approval_status"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),
                    due_date = row["due_date"].ToString(),
                    approval_initiation = row["approval_initiation"].ToString(),

                }).ToList();

                msSQL = " select extendorwaiverapproval_gid,initiateextendorwaiver_gid,approval_name,approval_status,approval_remarks, " +
                        " case when approval_status = 'Approved' then date_format(approved_date, '%d-%m-%Y %h:%i %p') " +
                        " else date_format(rejected_date, '%d-%m-%Y %h:%i %p') end as approvedrejected_date " +
                        " from ocs_trn_textendorwaiverapproval where groupdocumentchecklist_gid = '" + documentcheckdtl_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.mdlapprovaldtl = dt_datatable.AsEnumerable().Select(row => new mdlapprovaldtl
                    {
                        extendorwaiverapproval_gid = row["extendorwaiverapproval_gid"].ToString(),
                        initiateextendorwaiver_gid = row["initiateextendorwaiver_gid"].ToString(),
                        approval_name = row["approval_name"].ToString(),
                        approval_status = row["approval_status"].ToString(),
                        approval_remarks = row["approval_remarks"].ToString(),
                        approvedrejected_date = row["approvedrejected_date"].ToString(),

                    }).ToList(); 
                }

                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
        }

        public void DaGetApprovalExtensionwaiver(mdlapprovaldtllist values, string initiateextendorwaiver_gid)
        {

            msSQL = " select initiateextendorwaiver_gid,activity_type,activity_title, " +
                   " date_format(a.extendeddue_date, '%d-%m-%Y') as extendeddue_date,reason,approval_status, " +
                   " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                   " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from ocs_trn_tinitiateextendorwaiver a " +
                   " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                   " where initiateextendorwaiver_gid = '" + initiateextendorwaiver_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.activity_type = objODBCDataReader["activity_type"].ToString();
                values.activity_title = objODBCDataReader["activity_title"].ToString();
                values.extendeddue_date = objODBCDataReader["extendeddue_date"].ToString();
                values.reason = objODBCDataReader["reason"].ToString();
                values.approval_status = objODBCDataReader["approval_status"].ToString();
                values.created_by = objODBCDataReader["created_by"].ToString();
                values.created_date = objODBCDataReader["created_date"].ToString();
            }

            msSQL = " select extendorwaiverapproval_gid,initiateextendorwaiver_gid,approval_name,approval_status,approval_remarks, " +
                    " case when approval_status = 'Approved' then date_format(approved_date, '%d-%m-%Y %h:%i %p') " +
                    " else date_format(rejected_date, '%d-%m-%Y %h:%i %p') end as approvedrejected_date " +
                    " from ocs_trn_textendorwaiverapproval where initiateextendorwaiver_gid = '" + initiateextendorwaiver_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.mdlapprovaldtl = dt_datatable.AsEnumerable().Select(row => new mdlapprovaldtl
                {
                    extendorwaiverapproval_gid = row["extendorwaiverapproval_gid"].ToString(),
                    initiateextendorwaiver_gid = row["initiateextendorwaiver_gid"].ToString(),
                    approval_name = row["approval_name"].ToString(),
                    approval_status = row["approval_status"].ToString(),
                    approval_remarks = row["approval_remarks"].ToString(),
                    approvedrejected_date = row["approvedrejected_date"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
        }

        public void DaGetDeferralApprovalSummary(mdldeferralapprovallist values, string employee_gid)
        {
            msSQL = " select a.extendorwaiverapproval_gid,a.initiateextendorwaiver_gid, b.groupdocumentchecklist_gid, d.application_no,d.customer_name,d.customer_urn, " +
                    " b.activity_type,b.activity_title, date_format(b.created_date, '%d-%m-%Y %h:%i %p') as created_date,b.application_gid, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,a.approval_status " +
                    " from ocs_trn_textendorwaiverapproval a " +
                    " left join ocs_trn_tinitiateextendorwaiver b on a.initiateextendorwaiver_gid = b.initiateextendorwaiver_gid " +
                    " left join adm_mst_tuser c on b.created_by = c.user_gid " +
                    " left join ocs_trn_tcadapplication d on b.application_gid = d.application_gid " +
                    " where a.approval_gid = '" + employee_gid + "' and a.approval_status = 'Pending'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.mdldeferralapproval = dt_datatable.AsEnumerable().Select(row => new mdldeferralapproval
                {

                    extendorwaiverapproval_gid = row["extendorwaiverapproval_gid"].ToString(),
                    initiateextendorwaiver_gid = row["initiateextendorwaiver_gid"].ToString(),
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    application_gid = row["application_gid"].ToString(),
                    approval_status = row["approval_status"].ToString(),
                    application_no = row["application_no"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    activity_type = row["activity_type"].ToString(),
                    activity_title = row["activity_title"].ToString(),
                    created_date = row["created_date"].ToString(),
                    created_by = row["created_by"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
        }

        public void DaGetDeferralApprovalHistorySummary(mdldeferralapprovallist values, string employee_gid)
        {
            msSQL = " select a.extendorwaiverapproval_gid,a.initiateextendorwaiver_gid, b.groupdocumentchecklist_gid, d.application_no,d.customer_name,d.customer_urn, " +
                    " b.activity_type,b.activity_title, date_format(b.created_date, '%d-%m-%Y %h:%i %p') as created_date,b.application_gid, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,a.approval_status " +
                    " from ocs_trn_textendorwaiverapproval a " +
                    " left join ocs_trn_tinitiateextendorwaiver b on a.initiateextendorwaiver_gid = b.initiateextendorwaiver_gid " +
                    " left join adm_mst_tuser c on b.created_by = c.user_gid " +
                    " left join ocs_trn_tcadapplication d on b.application_gid = d.application_gid " +
                    " where a.approval_gid = '" + employee_gid + "' and a.approval_status in ('Approved','Rejected')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.mdldeferralapproval = dt_datatable.AsEnumerable().Select(row => new mdldeferralapproval
                {
                    extendorwaiverapproval_gid = row["extendorwaiverapproval_gid"].ToString(),
                    initiateextendorwaiver_gid = row["initiateextendorwaiver_gid"].ToString(),
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    application_gid = row["application_gid"].ToString(),
                    approval_status = row["approval_status"].ToString(),
                    application_no = row["application_no"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    activity_type = row["activity_type"].ToString(),
                    activity_title = row["activity_title"].ToString(),
                    created_date = row["created_date"].ToString(),
                    created_by = row["created_by"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
        }

        public void DaPostPhysicalextenstionwaiverApproval(mdldocumentapprovaldtllist values, string user_gid, string employee_gid)
        {
            foreach (var i in values.mdlapprovaldtl)
            {
                string lstotalapproval = "", lsapproved_count = "", lsactivity_type = "";
                msSQL = " select a.activity_type,a.groupdocumentchecklist_gid,b.extendorwaiverapproval_gid from ocs_trn_tinitiateextendorwaiver a " +
                        " left join ocs_trn_textendorwaiverapproval b on a.initiateextendorwaiver_gid =b.initiateextendorwaiver_gid " +
                        " where a.initiateextendorwaiver_gid='" + i.initiateextendorwaiver_gid + "' and b.approval_gid='" + employee_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    lsactivity_type = objODBCDataReader["activity_type"].ToString();
                    i.documentcheckdtl_gid = objODBCDataReader["groupdocumentchecklist_gid"].ToString();
                    i.extendorwaiverapproval_gid = objODBCDataReader["extendorwaiverapproval_gid"].ToString();
                }
                objODBCDataReader.Close();
                if (i.approval_remarks == null)
                    i.approval_remarks = "";
                if (i.approval_status == "Approved")
                {
                    msSQL = " update ocs_trn_textendorwaiverapproval set approval_remarks ='" + i.approval_remarks.Replace("'", "") + "'," +
                            " approval_status='" + i.approval_status + "'," +
                            " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where extendorwaiverapproval_gid='" + i.extendorwaiverapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_textendorwaiverapproval set approval_remarks ='Auto Approval'," +
                            " approval_status='" + i.approval_status + "'," +
                            " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where extendorwaiverapproval_gid !='" + i.extendorwaiverapproval_gid + "'" +
                            " and initiateextendorwaiver_gid='" + i.initiateextendorwaiver_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                      
                    msSQL = " update ocs_trn_tinitiateextendorwaiver set approval_status='Approved' " +
                              " where initiateextendorwaiver_gid='" + i.initiateextendorwaiver_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                            " where groupcovdocumentchecklist_gid ='" + i.documentcheckdtl_gid + "'";
                    string lscovenant = objdbconn.GetExecuteScalar(msSQL);
                    if (lscovenant != "")
                    {
                        msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physicaloverall_docstatus='Approved - " + lsactivity_type + "' " +
                              " where groupcovdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='Approved - " + lsactivity_type + "' " +
                                " where groupcovdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='Approved - " + lsactivity_type + "' " +
                          " where groupdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='Approved - " + lsactivity_type + "' " +
                               " where groupdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    if (lsactivity_type == "Waiver")
                    {
                        mdldocumentconfirmation objvalues = new mdldocumentconfirmation();
                        objvalues.overall_docstatus = "Waived";
                        objvalues.documentcheckdtl_gid = i.documentcheckdtl_gid;
                        objvalues.documentconfirmation_remarks = "";
                        DaPostPhysicalDocumentConfirmationDoc(objvalues, user_gid);
                    }
                    else if (lsactivity_type == "Tag to Deferral")
                    {

                        msSQL = " update ocs_trn_tdeferraltagdoc  set deferraltag_status='" + deferralTagstatus.Active + "' " +
                               " where initiateextendorwaiver_gid='" + i.initiateextendorwaiver_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='Deferral Accepted' " +
                                " where groupdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='Deferral Accepted' " +
                               " where groupcovdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    //}
                }
                else
                {
                    msSQL = " update ocs_trn_textendorwaiverapproval set approval_remarks ='" + i.approval_remarks.Replace("'", "") + "'," +
                            " approval_status='" + i.approval_status + "'," +
                            " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where extendorwaiverapproval_gid='" + i.extendorwaiverapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_textendorwaiverapproval set approval_remarks ='Auto Rejected'," +
                           " approval_status='" + i.approval_status + "'," +
                           " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where extendorwaiverapproval_gid !='" + i.extendorwaiverapproval_gid + "'" +
                           " and initiateextendorwaiver_gid='" + i.initiateextendorwaiver_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                               " where groupcovdocumentchecklist_gid ='" + i.documentcheckdtl_gid + "'";
                    string lscovenant = objdbconn.GetExecuteScalar(msSQL);
                    if (lscovenant != "")
                    {
                        msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physicaloverall_docstatus='" + i.approval_status + " - " + lsactivity_type + "'" +
                              " where groupcovdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='" + i.approval_status + " - " + lsactivity_type + "'" +
                                " where groupcovdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='" + i.approval_status + " - " + lsactivity_type + "'" +
                          " where groupdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='" + i.approval_status + " - " + lsactivity_type + "'" +
                                  " where groupdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    msSQL = " update ocs_trn_tinitiateextendorwaiver set approval_status='" + i.approval_status + "' " +
                            " where initiateextendorwaiver_gid='" + i.initiateextendorwaiver_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lsactivity_type == "Tag to Deferral")
                    {

                        msSQL = " update ocs_trn_tdeferraltagdoc  set deferraltag_status='" + deferralTagstatus.ApprovalRejected + "' " +
                               " where initiateextendorwaiver_gid='" + i.initiateextendorwaiver_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='Deferral Rejected' " +
                                " where groupdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='Deferral Rejected' " +
                               " where groupcovdocumentchecklist_gid='" + i.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }


            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "" + values.mdlapprovaldtl[0].approval_status + " Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostPhysicalDocumentConfirmationDoc(mdldocumentconfirmation values, string user_gid)
        {
            string lscovenant = "", lsapplication_gid = "";
            msSQL = " select groupcovdocumentchecklist_gid,application_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                           " where groupcovdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                lscovenant = objODBCDataReader["groupcovdocumentchecklist_gid"].ToString();
                lsapplication_gid = objODBCDataReader["application_gid"].ToString();
            }
            objODBCDataReader.Close();

            if (lscovenant != "")
            {
                msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='" + values.overall_docstatus + "'," +
                        " physical_confirmation_remarks ='" + values.documentconfirmation_remarks.Replace("'", "") + "'," +
                        " physical_confirmation_updatedby='" + user_gid + "'," +
                        " physical_confirmation_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.overall_docstatus == "Waived")
                {
                    msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set overall_docstatus='" + values.overall_docstatus + "'," +
                     " confirmation_updatedby='" + user_gid + "'," +
                     " confirmation_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                msSQL = " select application_gid from ocs_trn_tcadgroupdocumentchecklist " +
                          " where groupdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    lsapplication_gid = objODBCDataReader["application_gid"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='" + values.overall_docstatus + "'," +
                        " physical_confirmation_remarks ='" + values.documentconfirmation_remarks.Replace("'", "") + "'," +
                        " physical_confirmation_updatedby='" + user_gid + "'," +
                        " physical_confirmation_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.overall_docstatus == "Waived")
                {
                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set overall_docstatus='" + values.overall_docstatus + "'," +
                  " confirmation_updatedby='" + user_gid + "'," +
                  " confirmation_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                  " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Confirmation details are updated Successfully..!";

                if (lsapplication_gid != "")
                {
                    result objvalues = new result();
                    DaupdatePhysicalAutomaticCompletion(lsapplication_gid, user_gid, objvalues);
                }
            }
            else
            {
                values.status = true;
                values.message = "Error Occured..!";
            }
        }



        public void DaPostPhysicalCovenantPeriods(mdlscannedcovenantperiod values, string user_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CPDG");
            msSQL = " insert into ocs_trn_tcovenantperioddtl( " +
                        " covenantperioddtl_gid," +
                        " groupdocumentdtl_gid," +
                        " credit_gid," +
                        " physical_covenant_periods, " +
                        " physical_buffer_days, " +
                        " covenant_startdate, " +
                        " covenant_enddate ," +
                        " covenant_submissiondate ," +
                        " fromphysical_document, " +
                        " created_by," +
                        " created_date" +
                        " )values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.groupdocumentdtl_gid + "'," +
                        "'" + values.credit_gid + "'," +
                        "'" + values.covenant_periods + "'," +
                        "'" + values.buffer_days + "'," +
                        "'" + Convert.ToDateTime(values.covenant_startdate).ToString("yyyy-MM-dd") + "'," +
                        "'" + Convert.ToDateTime(values.covenant_enddate).ToString("yyyy-MM-dd") + "'," +
                        "'" + Convert.ToDateTime(values.covenant_submissiondate).ToString("yyyy-MM-dd") + "'," +
                        "'Y', " +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physical_covenant_periods='" + values.covenant_periods + "'," +
                        " physical_buffer_days = '" + values.buffer_days + "', " +
                        " physical_bufferday_updatedby='" + user_gid + "', " +
                        " physical_covenantperiod_updatedby='" + user_gid + "'," +
                        " physical_covenantperiod_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " physical_bufferday_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where groupcovdocumentchecklist_gid='" + values.groupdocumentdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physical_covenant_periods='" + values.covenant_periods + "'," +
                     " physical_buffer_days = '" + values.buffer_days + "', " +
                     " physical_bufferday_updatedby='" + user_gid + "', " +
               " physical_covenantperiod_updatedby='" + user_gid + "'," +
               " physical_covenantperiod_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " physical_bufferday_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               " where groupcovdocumentchecklist_gid='" + values.groupdocumentdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Covenant Periods added Successfully..!";
            }
            else
            {
                values.status = true;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetPhysicalCovenantPeriodsSummary(mdlscannedcovenantperiodlist values, string groupdocumentdtl_gid)
        {
            msSQL = " select a.covenantperioddtl_gid,a.groupdocumentdtl_gid, a.credit_gid, " +
                    " CASE WHEN a.physical_covenant_periods is null THEN (select covenant_periods from ocs_trn_tcadcovanantdocumentcheckdtls e " +
                    " where e.groupcovdocumentchecklist_gid = '" + groupdocumentdtl_gid + "'  group by e.groupcovdocumentchecklist_gid ) " +
                    "  ELSE a.physical_covenant_periods END as 'physical_covenant_periods', " +
                     " CASE WHEN a.physical_buffer_days is null THEN (select buffer_days from ocs_trn_tcadcovanantdocumentcheckdtls e " +
                    " where e.groupcovdocumentchecklist_gid = '" + groupdocumentdtl_gid + "'  group by e.groupcovdocumentchecklist_gid ) " +
                    "  ELSE a.physical_buffer_days END as 'physical_buffer_days', " +
                    " date_format(a.covenant_startdate, '%d-%m-%Y') as covenant_startdate,date_format(a.covenant_enddate, '%d-%m-%Y') as covenant_enddate, " +
                    " date_format(a.covenant_submissiondate, '%d-%m-%Y') as covenant_submissiondate,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by " +
                    " from ocs_trn_tcovenantperioddtl a " +
                    " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                    " where a.groupdocumentdtl_gid = '" + groupdocumentdtl_gid + "' and a.fromphysical_document='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.mdlscannedcovenantperiod = dt_datatable.AsEnumerable().Select(row => new mdlscannedcovenantperiod
                {
                    covenantperioddtl_gid = row["covenantperioddtl_gid"].ToString(),
                    groupdocumentdtl_gid = row["groupdocumentdtl_gid"].ToString(),
                    credit_gid = row["credit_gid"].ToString(),
                    covenant_periods = row["physical_covenant_periods"].ToString(),
                    buffer_days = row["physical_buffer_days"].ToString(),
                    covenant_startdate = row["covenant_startdate"].ToString(),
                    covenant_enddate = row["covenant_enddate"].ToString(),
                    covenant_submissiondate = row["covenant_submissiondate"].ToString(),
                    created_date = row["created_date"].ToString(),
                    created_by = row["created_by"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
        }

        public void DaGetcancelPhysicalCovenantPeriod(mdlscannedcovenantperiod values, result objresult, string user_gid)
        {
            msSQL = " delete from ocs_trn_tcovenantperioddtl where covenantperioddtl_gid='" + values.covenantperioddtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = "select physical_covenant_periods from ocs_trn_tcovenantperioddtl where groupdocumentdtl_gid='" + values.groupdocumentdtl_gid + "'";
                string lscovenant_periods = objdbconn.GetExecuteScalar(msSQL);
                if (lscovenant_periods != "")
                    values.previous_covenantperiods = lscovenant_periods;
                msSQL = "select physical_buffer_days from ocs_trn_tcovenantperioddtl where groupdocumentdtl_gid='" + values.groupdocumentdtl_gid + "'";
                string lsbuffer_days = objdbconn.GetExecuteScalar(msSQL);
                if (lsbuffer_days != "")
                    values.previous_bufferdays = lsbuffer_days;
                msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physical_covenant_periods='" + values.previous_covenantperiods + "'," +
                " physical_buffer_days = '" + values.previous_bufferdays + "', " +
                    " physical_bufferday_updatedby='" + user_gid + "', " +
                    " physical_covenantperiod_updatedby='" + user_gid + "'," +
                " physical_covenantperiod_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                 " physical_bufferday_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where groupcovdocumentchecklist_gid='" + values.groupdocumentdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physical_covenant_periods='" + values.previous_covenantperiods + "'," +
              " physical_buffer_days = '" + values.previous_bufferdays + "', " +
                    " physical_bufferday_updatedby='" + user_gid + "', " +
                    " physical_covenantperiod_updatedby='" + user_gid + "'," +
              " physical_covenantperiod_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
               " physical_bufferday_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
              " where groupcovdocumentchecklist_gid='" + values.groupdocumentdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult == 1)
            {
                objresult.status = true;
                objresult.message = "Covenant period details are Deleted Successfully..!";
            }
            else
            {
                objresult.status = false;
                objresult.message = "Error Occured..!";
            }
        }

        public void DaGetDeferralHistorySummary(deferraltaggedlist values, string groupdocumentchecklist_gid)
        {
            msSQL = " select mstdocument_name,mstdocumenttype_name from ocs_trn_tcadgroupdocumentchecklist " +
                    " where groupdocumentchecklist_gid = '" + groupdocumentchecklist_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.document_name = objODBCDataReader["mstdocument_name"].ToString();
                values.document_type = objODBCDataReader["mstdocumenttype_name"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " select mstdocument_name,mstdocumenttype_name from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                   " where groupcovdocumentchecklist_gid = '" + groupdocumentchecklist_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.document_name = objODBCDataReader["mstdocument_name"].ToString();
                values.document_type = objODBCDataReader["mstdocumenttype_name"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " select deferraltagdoc_gid,documentseverity_gid,documentseverity_name,tracking_id,tagged_to,deferraltag_status, " +
                  " date_format(due_date,'%d-%m-%Y') as due_date, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                  " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                  " deferraltag_reason  from ocs_trn_tdeferraltagdoc a" +
                  " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                  " where groupdocumentchecklist_gid='" + groupdocumentchecklist_gid + "' and fromphysical_document='Y' order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.deferraltagged = dt_datatable.AsEnumerable().Select(row => new deferraltagged
                {
                    deferraltagdoc_gid = row["deferraltagdoc_gid"].ToString(),
                    documentseverity_gid = row["documentseverity_gid"].ToString(),
                    documentseverity_name = row["documentseverity_name"].ToString(),
                    tracking_id = row["tracking_id"].ToString(),
                    tagged_to = row["tagged_to"].ToString(),
                    due_date = row["due_date"].ToString(),
                    cad_remarks = row["deferraltag_reason"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),
                    deferraltag_status = row["deferraltag_status"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();
        }

        public void DaGetPhysicalGeneralInfo(mdlscannedgeneral values, string application_gid)
        {
            msSQL = " select maker_name,checker_name,approver_name,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as RM_name " +
                    " from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " where d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and a.application_gid='" + application_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.maker_name = objODBCDataReader["maker_name"].ToString();
                values.checker_name = objODBCDataReader["checker_name"].ToString();
                values.approver_name = objODBCDataReader["approver_name"].ToString();
                values.RM_name = objODBCDataReader["RM_name"].ToString();
            }
            objODBCDataReader.Close();

        }

        public void DaGetPhysicalConfirmationValidation(result values, string groupdocumentcheckdtl_gid, string lstype)
        {
            if (lstype == "Waived")
            {
                msSQL = " select initiateextendorwaiver_gid from ocs_trn_tinitiateextendorwaiver " +
                       " where groupdocumentchecklist_gid = '" + groupdocumentcheckdtl_gid + "' and activity_type = 'Waiver' " +
                       " and fromphysical_document='Y' and approval_status = 'Pending'";
            }
            else
            {
                msSQL = " select tagquery_gid from ocs_trn_ttagquery where groupdocumentchecklist_gid='" + groupdocumentcheckdtl_gid + "'" +
                        "  and fromphysical_document='Y' and query_status in ('Query Raised')";
            }
            string lspending = objdbconn.GetExecuteScalar(msSQL);
            if (lspending != "")
            {
                values.status = true;
                values.message = "Pending";
            }
            else
            {
                values.status = false;
                values.message = "No Record";
            }

        }

        public void DaGetDuplicateDeferralTagged(result values, string application_gid)
        {
            msSQL = " SELECT a.mstdocument_gid,a.mstdocument_name FROM ocs_trn_tcadgroupdocumentchecklist A " +
                    " inner JOIN ocs_trn_tcadgroupcovenantdocumentchecklist B ON A.application_gid = B.application_gid " +
                    " and A.credit_gid = B.credit_gid  and A.mstdocument_gid = B.mstdocument_gid " +
                    " where A.untagged_type is null and A.application_gid = '" + application_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.status = true;
                values.message = "Duplicate Tagged Document";
            }
            else
            {
                values.status = false;
                values.message = "No Record";
            }
            objODBCDataReader.Close();

        }

        public void DapostPhysicalMakerCheckerConversation(string user_gid, mdlmakercheckerconversation values)
        {
            msSQL = " select maker_gid,checker_gid from ocs_trn_tprocesstype_assign " +
                       " where application_gid='" + values.application_gid + "' and menu_gid = '" + getMenuClass.PhysicalDocument + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                if (values.maker_flag == "Y")
                    values.send_to = objODBCDataReader["checker_gid"].ToString();
                else
                    values.send_to = objODBCDataReader["maker_gid"].ToString();
            }
            objODBCDataReader.Close();


            msGetGid = objcmnfunctions.GetMasterGID("MCCG");
            msSQL = " insert into ocs_trn_tmakercheckerconversation( " +
                        " makercheckerconversation_gid," +
                        " groupdocumentcheckdtl_gid," +
                        " application_gid, " +
                        " credit_gid," +
                        " message, " +
                        " maker_flag, " +
                        " send_to ," +
                        " fromphysical_document, " +
                        " created_by," +
                        " created_date" +
                        " )values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.groupdocumentdtl_gid + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.credit_gid + "'," +
                        "'" + values.send_message.Replace("'", "") + "'," +
                        "'" + values.maker_flag + "'," +
                        "'" + values.send_to + "'," +
                        "'Y'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Message Sent Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetMakerCheckerConversation(mdlmakercheckerconversationlist values, string groupdocumentcheckdtl_gid, string user_gid)
        {
            string sessionuser_flag = "";
            string lsmaker_flag = "";
            msSQL = " select makercheckerconversation_gid,application_gid,message,send_to,maker_flag, " +
                  " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.created_by as createduser_gid, " +
                  " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by " +
                  " from ocs_trn_tmakercheckerconversation a" +
                  " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                  " where groupdocumentcheckdtl_gid='" + groupdocumentcheckdtl_gid + "' and a.fromphysical_document='Y' order by a.created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmakercheckerconversation = new List<mdlmakercheckerconversation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (user_gid == dt["createduser_gid"].ToString())
                        sessionuser_flag = "Y";
                    else
                        sessionuser_flag = "N";
                    if (dt["maker_flag"].ToString() == "Y")
                        lsmaker_flag = "Maker";
                    else
                        lsmaker_flag = "Checker";
                    getmakercheckerconversation.Add(new mdlmakercheckerconversation
                    {
                        application_gid = dt["application_gid"].ToString(),
                        message = dt["message"].ToString(),
                        makercheckerconversation_gid = dt["makercheckerconversation_gid"].ToString(),
                        maker_flag = lsmaker_flag,
                        session_user = sessionuser_flag,
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });
                    values.mdlmakercheckerconversation = getmakercheckerconversation;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaupdatePhysicalAutomaticCompletion(string application_gid, string user_gid, result values)
        {
            string overall_approvalstatus = "", processtypeassign_gid = "";
            int OverallDeferralCount = 0, DeferralCompletedCount = 0, OverallCovenantCount = 0;
            int CovenantCompletedCount = 0, SignedDeferralCount = 0, signedCovenantCount = 0;
            msSQL = " select processtypeassign_gid,overall_approvalstatus from ocs_trn_tprocesstype_assign where application_gid='" + application_gid + "'" +
                    " and menu_gid='" + getMenuClass.PhysicalDocument + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                overall_approvalstatus = objODBCDataReader["overall_approvalstatus"].ToString();
                processtypeassign_gid = objODBCDataReader["processtypeassign_gid"].ToString();
            }
            objODBCDataReader.Close();

            if (overall_approvalstatus == "Approved")
            {
                msSQL = " select (select count(*) from ocs_trn_tcadgroupdocumentchecklist where application_gid='" + application_gid + "' " +
                       " and(untagged_type is null or untagged_type = 'N')) As OverallDeferralCount, " +
                       " (select count(*)  from ocs_trn_tcadgroupdocumentchecklist where application_gid = '" + application_gid + "' " +
                       " and physicaloverall_docstatus in ('Satisfied','Waived') " +
                       " and(untagged_type is null or untagged_type = 'N')) As DeferralCompletedCount, " +
                       " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where application_gid = '" + application_gid + "' " +
                       " and(untagged_type is null or untagged_type = 'N')) As OverallCovenantCount, " +
                       " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where application_gid = '" + application_gid + "' " +
                       " and physicaloverall_docstatus in ('Satisfied','Waived') " +
                       " and (untagged_type is null or untagged_type = 'N')) As CovenantCompletedCount, " +
                       " (select COUNT(DISTINCT groupdocumentchecklist_gid) from ocs_trn_tphysicaldocument A " +
                       " where application_gid = '" + application_gid + "' and groupdocumentchecklist_gid in " +
                       " (select groupdocumentchecklist_gid from ocs_trn_tcadgroupdocumentchecklist " +
                       " where application_gid = '" + application_gid + "' and(untagged_type is null or untagged_type = 'N'))) As SignedDeferralCount, " +
                       " (select COUNT(DISTINCT groupdocumentchecklist_gid) from ocs_trn_tphysicaldocument A " +
                       " where application_gid = '" + application_gid + "'" +
                       " and groupdocumentchecklist_gid  in (select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                       " where application_gid = '" + application_gid + "' and(untagged_type is null or untagged_type = 'N')))  As signedCovenantCount";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    OverallDeferralCount = Convert.ToInt16(objODBCDataReader["OverallDeferralCount"]);
                    DeferralCompletedCount = Convert.ToInt16(objODBCDataReader["DeferralCompletedCount"]);
                    OverallCovenantCount = Convert.ToInt16(objODBCDataReader["OverallCovenantCount"]);
                    CovenantCompletedCount = Convert.ToInt16(objODBCDataReader["CovenantCompletedCount"]);
                    SignedDeferralCount = Convert.ToInt16(objODBCDataReader["SignedDeferralCount"]);
                    signedCovenantCount = Convert.ToInt16(objODBCDataReader["signedCovenantCount"]);
                }
                objODBCDataReader.Close();
                int QueryclearedCount = DeferralCompletedCount + CovenantCompletedCount;
                int SignedCompletedCount = SignedDeferralCount + signedCovenantCount;
                int TaggedCount = OverallDeferralCount + OverallCovenantCount;
                if (TaggedCount == QueryclearedCount && TaggedCount == SignedCompletedCount)
                {
                    msSQL = " update ocs_trn_tprocesstype_assign set completed_flag='Y', completed_on=NOW(), " +
                            " completed_by='" + user_gid + "'" +
                            " where processtypeassign_gid='" + processtypeassign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult == 1)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }

        public string DaGetvalueswithComma(string[] array)
        {
            var value = "";
            foreach (var h in array)
            {
                value += "'" + h + "',";
            }
            value = value.TrimEnd(',');
            return value;
        }

        public void DaGetPhysicalInstitutionList(string application_gid, MdlCreditView values)
        {
            msSQL = " select a.institution_gid, a.application_gid, cin_no, companytype_name, " +
                     " company_name, companypan_no, date_format(date_incorporation, '%d-%m-%Y') as date_incorporation," +
                     " date_format(a.created_date, '%d-%m-%Y') as created_date," +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,stakeholder_type,credit_status, " +
                     " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid =a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                     " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                     " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid = a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N') and physicaloverall_docstatus in ('Waived','Satisfied')) as verifieddeferraldoc, " +
                     " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N') and physicaloverall_docstatus in ('Waived','Satisfied'))  as verifiedcovenantdoc, " +
                      " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.institution_gid or c.credit_gid = a.institution_gid) and fromphysical_document='Y' and (query_status!='Closed' or query_status!='Cancelled')) as QueryPendingCount, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.institution_gid or c.credit_gid = a.institution_gid) and fromphysical_document='Y' and ( query_status='Closed' or query_status='Cancelled')) as QueryClosedCount " +
                     " from ocs_trn_tcadinstitution a" +
                     " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                     " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                     " where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionList = new List<institution_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitutionList.Add(new institution_List
                    {
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        company_name = (dr_datarow["company_name"].ToString()),
                        companypan_no = (dr_datarow["companypan_no"].ToString()),
                        cin_no = (dr_datarow["cin_no"].ToString()),
                        companytype_name = (dr_datarow["companytype_name"].ToString()),
                        date_incorporation = (dr_datarow["date_incorporation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                        QueryPendingCount = (dr_datarow["QueryPendingCount"].ToString()),
                        QueryClosedCount = (dr_datarow["QueryClosedCount"].ToString()),
                    });
                }
                values.institution_List = getinstitutionList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetPhysicalIndividualList(string application_gid, MdlCreditView values)
        {
            msSQL = " select a.contact_gid, a.application_gid, concat_ws(' ', first_name, last_name, middle_name) as individual_name, " +
                      " a.pan_no, aadhar_no, date_format(individual_dob, '%d-%m-%Y') as individual_dob,a.age,designation_name," +
                      " main_occupation, date_format(a.created_date, '%d-%m-%Y') as created_date," +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,stakeholder_type,institution_name,credit_status, " +
                      " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid =a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                      " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                      " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid = a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N') and physicaloverall_docstatus in ('Waived','Satisfied')) as verifieddeferraldoc, " +
                      " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N') and physicaloverall_docstatus in ('Waived','Satisfied'))  as verifiedcovenantdoc , " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.contact_gid or c.credit_gid = a.contact_gid)  and fromphysical_document='Y'  and (query_status!='Closed' or query_status!='Cancelled')) as QueryPendingCount, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.contact_gid or c.credit_gid = a.contact_gid)  and fromphysical_document='Y' and ( query_status='Closed' or query_status='Cancelled')) as QueryClosedCount " +
                     " from ocs_trn_tcadcontact a" +
                     " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                     " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                     " where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getindividualList = new List<individual_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getindividualList.Add(new individual_List
                    {
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        individual_dob = (dr_datarow["individual_dob"].ToString()),
                        main_occupation = (dr_datarow["main_occupation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        company_name = (dr_datarow["institution_name"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        age = (dr_datarow["age"].ToString()),
                        designation_name = (dr_datarow["designation_name"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                        QueryPendingCount = (dr_datarow["QueryPendingCount"].ToString()),
                        QueryClosedCount = (dr_datarow["QueryClosedCount"].ToString()),
                    });
                }
                values.individual_List = getindividualList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetPhysicalGrouptoMemberList(string group_gid, MdlMstGroupMember values)
        {
            msSQL = "select a.contact_gid,a.pan_no,a.aadhar_no,concat(first_name, ' ',middle_name,' ',last_name) as individual_name,stakeholder_type,credit_status," +
                    " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid =a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                    " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                    " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid = a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and physicaloverall_docstatus in ('Waived','Satisfied')) as verifieddeferraldoc, " +
                    " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and physicaloverall_docstatus in ('Waived','Satisfied'))  as verifiedcovenantdoc, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.institution_gid or c.credit_gid = a.institution_gid) and fromphysical_document='Y' and query_status!='Closed') as QueryPendingCount, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.institution_gid or c.credit_gid = a.institution_gid) and fromphysical_document='Y' and query_status='Closed') as QueryClosedCount " +
                    " from ocs_trn_tcadcontact a " +
                    " where group_gid='" + group_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgroupmember_list = new List<groupmember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgroupmember_list.Add(new groupmember_list
                    {
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                        QueryPendingCount = (dr_datarow["QueryPendingCount"].ToString()),
                        QueryClosedCount = (dr_datarow["QueryClosedCount"].ToString()),
                    });
                }
            }
            values.groupmember_list = getgroupmember_list;
            dt_datatable.Dispose();
        }

        public void DaGetPhysicalGroupSummary(string application_gid, MdlMstGroup values)
        {
            msSQL = " select a.group_gid,a.group_name,date_format(a.date_of_formation,'%d-%m-%Y') as date_of_formation,a.group_status, a.group_type," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,credit_status," +
                    " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid =a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                    " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                    " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and physicaloverall_docstatus in ('Waived','Satisfied')) as verifieddeferraldoc, " +
                    " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and physicaloverall_docstatus in ('Waived','Satisfied'))  as verifiedcovenantdoc , " +
                    " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                    " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                    " and a.application_gid=c.application_gid  " +
                    " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                    " and a.application_gid=b.application_gid   " +
                    " where  (b.credit_gid = a.group_gid or c.credit_gid = a.group_gid)  and fromphysical_document='Y' and query_status!='Closed') as QueryPendingCount, " +
                    " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                    " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                    " and a.application_gid=c.application_gid  " +
                    " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                    " and a.application_gid=b.application_gid   " +
                    " where  (b.credit_gid = a.group_gid or c.credit_gid = a.group_gid) and fromphysical_document='Y' and query_status='Closed') as QueryClosedCount " +
                    " from ocs_trn_tcadgroup a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where application_gid='" + application_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgroup_list = new List<group_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgroup_list.Add(new group_list
                    {
                        group_gid = (dr_datarow["group_gid"].ToString()),
                        group_name = (dr_datarow["group_name"].ToString()),
                        date_of_formation = (dr_datarow["date_of_formation"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        group_status = (dr_datarow["group_status"].ToString()),
                        group_type = (dr_datarow["group_type"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                        QueryPendingCount = (dr_datarow["QueryPendingCount"].ToString()),
                        QueryClosedCount = (dr_datarow["QueryClosedCount"].ToString()),
                    });
                }
            }
            values.group_list = getgroup_list;
            dt_datatable.Dispose();
        }

        public void DaGetPhysicalDocApprovalDtls(string application_gid, MdlScannedDocApprovalDetails values)
        {
            try
            {
                msSQL = " select application_gid,maker_name,checker_name,approver_name, " +
                        " date_format(a.maker_approveddate,'%d-%m-%Y %h:%i %p') as maker_approveddate, " +
                        " date_format(a.checker_approveddate,'%d-%m-%Y %h:%i %p') as checker_approveddate, " +
                        " date_format(a.approver_approveddate,'%d-%m-%Y %h:%i %p') as approver_approveddate " +
                        " from ocs_trn_tprocesstype_assign a where processtype_name = 'Accept' and menu_gid = '" + getMenuClass.PhysicalDocument + "' and application_gid='" + application_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.application_gid = objODBCDataReader["application_gid"].ToString();
                    values.maker_name = objODBCDataReader["maker_name"].ToString();
                    values.checker_name = objODBCDataReader["checker_name"].ToString();
                    values.approver_name = objODBCDataReader["approver_name"].ToString();
                    values.maker_approveddate = objODBCDataReader["maker_approveddate"].ToString();
                    values.checker_approveddate = objODBCDataReader["checker_approveddate"].ToString();
                    values.approver_approveddate = objODBCDataReader["approver_approveddate"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDataReader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaPostPhysicalDocSent(string user_gid, Mdldocumentsend values)
        {
            try
            {
                msSQL = " insert into ocs_trn_tcadscannedphysicalstatusupdatelog( " +
                 " application_gid," +
                 " groupdocumentchecklist_gid," +
                 " covenant_type, " +
                 " scanneddoc_flag," +
                 " status_update, " +
                 " created_by," +
                 " created_date" +
                 " )values(" +
                 "'" + values.application_gid + "'," +
                 "'" + values.documentcheckdtl_gid + "'," +
                 "'" + values.covenant_type + "'," +
                 "'" + values.scanneddoc_flag + "'," +
                 "'" + values.document_status + "'," +
                 "'" + user_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    if (values.covenant_type == "Y")
                    {
                        msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='"+ values.document_status + "'" +
                                " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='" + values.document_status + "'" +
                                " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    } 
                }

                values.status = true;
                values.message = "Physical Document Sent Status updated Successfully..!";
            }
            catch
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public bool DaPhysicalDocumentMultiUpload(HttpRequest httpRequest, physicaluploaddocumentlist values, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsapplication_gid = httpRequest.Form["application_gid"].ToString();
            string lscovenant_type = httpRequest.Form["covenant_type"].ToString();
            string lsRMupload = httpRequest.Form["RMupload"].ToString();
            string lsgroupdocumentchecklist_gid = httpRequest.Form["groupdocumentchecklist_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/PhysicalDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    if (lscovenant_type == "Y")
                    {
                        msSQL = " select groupcovdocumentchecklist_gid as groupdocumentchecklist_gid,credit_gid,mstdocumenttype_gid,mstdocumenttype_name, " +
                               " mstdocument_name from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                               " where application_gid ='" + lsapplication_gid + "' and groupcovdocumentchecklist_gid in (" + lsgroupdocumentchecklist_gid + ")";
                    }
                    else
                    {
                        msSQL = " select groupdocumentchecklist_gid,credit_gid,mstdocumenttype_gid,mstdocumenttype_name,mstdocument_name " +
                               " from ocs_trn_tcadgroupdocumentchecklist " +
                               " where application_gid ='" + lsapplication_gid + "' and groupdocumentchecklist_gid in (" + lsgroupdocumentchecklist_gid + ")";
                    }

                    dt_datatable = objdbconn.GetDataTable(msSQL);

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
                            values.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/PhysicalDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/PhysicalDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        string lsdocumenttype_gid = "", lsdocumenttype_code = ""; 

                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                lsdocumenttype_gid = dt["mstdocumenttype_gid"].ToString();
                                lsdocumenttype_code = dt["mstdocumenttype_name"].ToString();
                                lsdocument_title = dt["mstdocument_name"].ToString();
                                string lsdocumentcheckdtl_gid = dt["groupdocumentchecklist_gid"].ToString();
                                string lscredit_gid = dt["credit_gid"].ToString();
                                string msGetCode = objcmnfunctions.GetMasterGID("OCDG");
                                msGetGid = objcmnfunctions.GetMasterGID("PYDO");
                                msSQL = " insert into ocs_trn_tphysicaldocument( " +
                                            " physicaldocument_gid," +
                                            " physicaldocument_code," +
                                            " groupdocumentchecklist_gid," +
                                            " application_gid, " +
                                            " credit_gid," +
                                            " rm_upload, " +
                                            " documenttype_gid, " +
                                            " documenttype_code, " +
                                            " documenttype_name ," +
                                            " file_name ," +
                                               " file_path," +
                                                " covenant_type," +
                                                " created_by," +
                                                " created_date" +
                                                " )values(" +
                                                "'" + msGetGid + "'," +
                                                 "'" + msGetCode + "'," +
                                                "'" + lsdocumentcheckdtl_gid + "'," +
                                                "'" + lsapplication_gid + "'," +
                                                "'" + lscredit_gid + "'," +
                                                "'" + lsRMupload + "'," +
                                                "'" + lsdocumenttype_gid + "'," +
                                                "'" + lsdocumenttype_code + "'," +
                                                "'" + lsdocument_title + "'," +
                                                "'" + httpPostedFile.FileName + "'," +
                                                "'" + lspath + msdocument_gid + FileExtension + "'," +
                                                "'" + lscovenant_type + "'," +
                                                "'" + employee_gid + "'," +
                                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                if (lscovenant_type == "Y")
                                {
                                    msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physicaloverall_docstatus ='Pending Vetting'" +
                                          " where groupcovdocumentchecklist_gid='" + lsdocumentcheckdtl_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus ='Pending Vetting'" +
                                            " where groupcovdocumentchecklist_gid='" + lsdocumentcheckdtl_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                else
                                {
                                    msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='Pending Vetting'" +
                                      " where groupdocumentchecklist_gid='" + lsdocumentcheckdtl_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='Pending Vetting'" +
                                              " where groupdocumentchecklist_gid='" + lsdocumentcheckdtl_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                        }
                        dt_datatable.Dispose();

                        if (mnResult == 1)
                        {
                            values.status = true;
                            values.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error Occured..!";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
            return true;
        }

        public void DaGetCADPhysicalDocFollowupSummary(physicalmakerapplicationlist values, string employee_gid)
        {
            msSQL = " (select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, e.sanction_refno, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,d.overall_approvalstatus, " +
                    " a.creditgroup_gid, d.cadgroup_name,'Checker Approval Pending' as Status from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTPYD' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'Y' and d.checker_approvalflag = 'N' order by a.updated_date desc) union" +
                    " (select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by,e.sanction_refno, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,d.overall_approvalstatus, " +
                    " a.creditgroup_gid, d.cadgroup_name,'Approval Pending' as Status from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTPYD' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.checker_approvalflag = 'Y' order by a.updated_date desc) union" +
                    " (select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by,e.sanction_refno, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,d.overall_approvalstatus, " +
                    " a.creditgroup_gid, d.cadgroup_name,'Approved' as Status from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTPYD'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag='Y' and d.checker_approvalflag = 'Y' and d.completed_flag='N'  and d.approver_approvalflag='Y'" +
                    " order by d.created_date desc )";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<physicalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new physicalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        //creditgroup_name = dt["creditgroup_name"].ToString(),
                        //ccgroup_name = dt["ccgroup_name"].ToString(),
                        //cadgroupname = dt["cadgroup_name"].ToString(),
                        //cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        //cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        followupstatus = dt["Status"].ToString(),
                    });
                }
            }
            values.physicalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }


        public void DaGetUpdateDocumentSubmission(mdlsubmissiondateupdate values, string user_gid)
        {
            if (values.covenant_type == "N")
            {
                msSQL = " update ocs_trn_tcadgroupdocumentchecklist set " +
                    " physicaldocumentsubmission_date='" + Convert.ToDateTime(values.documentsubmission_date).ToString("yyyy-MM-dd") + "', " +
                    " physicaldocumentsubmission_updatedby='" + user_gid + "'," +
                    " physicaldocumentsubmission_updateddate= current_timestamp " +
                    " where groupdocumentchecklist_gid='" + values.groupdocumentdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set " +
                    " physicaldocumentsubmission_date='" + Convert.ToDateTime(values.documentsubmission_date).ToString("yyyy-MM-dd") + "', " +
                  " physicaldocumentsubmission_updatedby='" + user_gid + "'," +
                  " physicaldocumentsubmission_updateddate= current_timestamp " +
                  " where groupcovdocumentchecklist_gid='" + values.groupdocumentdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document submission date updated successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetQueryRaisedinfo(string documentcheckdtl_gid, MdlTagQueryCheckpointList values)
        {

            msSQL = " select deferralchecklist_gid from ocs_trn_ttagquery where query_status='Query Raised' " +
                    " and groupdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            values.MdlTagQueryCheckpoint = cmnfunctions.ConvertDataTable<MdlTagQueryCheckpoint>(dt_datatable);
        }

        public void DatmpclearQueryuploaded(string user_gid, result values)
        {
            msSQL = " delete from ocs_trn_ttagquerydocument where tagquery_gid='" + user_gid + "' and fromphysical_document='Y'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
                values.status = true;
            else
                values.status = false;
        }

        public void fnSendRMraisequeryMail(string documentcheckdtl_gid, string tagquery_gid)
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

            string lsapplication_gid = "", lsquery_title = "", lsquery_description = "";
            msSQL = " select  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as raised_by, " +
                  " application_gid,query_title,query_description, " +
                  " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raised_date from ocs_trn_ttagquery a " +
                  "  left join adm_mst_tuser g on g.user_gid = a.created_by  " +
                  " where tagquery_gid='" + tagquery_gid + "'";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                ls_raised_by = objODBCDatareader1["raised_by"].ToString();
                ls_raised_date = objODBCDatareader1["raised_date"].ToString();
                lsapplication_gid = objODBCDatareader1["application_gid"].ToString();
                lsquery_title = objODBCDatareader1["query_title"].ToString();
                lsquery_description = objODBCDatareader1["query_description"].ToString();
            }
            objODBCDatareader1.Close();

            msSQL = " select relationshipmanager_gid,relationshipmanager_name,customerref_name,application_no " +
                    " from ocs_trn_tcadapplication where application_gid = '" + lsapplication_gid + "'";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            { 
                ls_relationshipmanager_gid = objODBCDatareader1["relationshipmanager_gid"].ToString();
                ls_relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                ls_customerref_name = objODBCDatareader1["customerref_name"].ToString();
                ls_application_no = objODBCDatareader1["application_no"].ToString(); 
            }
            objODBCDatareader1.Close();

            msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
              " where groupcovdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
            string lscovenant = objdbconn.GetExecuteScalar(msSQL);
            if (lscovenant != "")
            {
                msSQL = " select mstdocument_name as documenttype_name  from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                      " where groupcovdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
                documenttype_name = objdbconn.GetExecuteScalar(msSQL); 
            }
            else
            {
                msSQL = " select mstdocument_name as documenttype_name  from ocs_trn_tcadgroupdocumentchecklist " +
                       " where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
                documenttype_name = objdbconn.GetExecuteScalar(msSQL);
            }


            msSQL = " select  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as raised_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raised_date from ocs_trn_ttagquery a " +
                    "  left join adm_mst_tuser g on g.user_gid = a.created_by  " +
                    " where tagquery_gid='" + tagquery_gid + "'";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                ls_raised_by = objODBCDatareader1["raised_by"].ToString();
                ls_raised_date = objODBCDatareader1["raised_date"].ToString();
            }


            msSQL = " select institution_gid from  ocs_trn_tcadinstitution where stakeholder_type = 'Applicant' and application_gid = '" + lsapplication_gid + "'";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                msSQL = " select company_name from  ocs_trn_tcadinstitution where stakeholder_type = 'Applicant' and application_gid = '" + lsapplication_gid + "'";
                lscompany_name = objdbconn.GetExecuteScalar(msSQL);
            }
            else
            {
                msSQL = " select concat(first_name,middle_name,last_name) from  ocs_trn_tcadcontact where stakeholder_type = 'Applicant' and application_gid = '" + lsapplication_gid + "'";
                lscompany_name = objdbconn.GetExecuteScalar(msSQL);
            }


            msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + ls_relationshipmanager_gid + "'";
            to_employee_emailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select b.employee_emailid from ocs_trn_tprocesstype_assign a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.maker_gid " +
                    " where application_gid = '" + lsapplication_gid + "' ";
            cc_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select c.employee_emailid from ocs_trn_tprocesstype_assign a " +
                    " left join hrm_mst_temployee c on c.employee_gid = a.checker_gid " +
                    " where application_gid = '" + lsapplication_gid + "' ";
            cc1_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select c.employee_emailid from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee c on c.employee_gid = a.clustermanager_gid " +
                    " where application_gid = '" + lsapplication_gid + "' ";
            cc2_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select c.employee_emailid from ocs_trn_tcadapplication a " +
                   " left join hrm_mst_temployee c on c.employee_gid = a.drm_gid " +
                   " where application_gid = '" + lsapplication_gid + "' ";
            cc3_mailid = objdbconn.GetExecuteScalar(msSQL);


            tomail_id1 = to_employee_emailid;


            sub1 = "\"" + lscompany_name + "\"  \"" + ls_application_no + "\" – A query has been raised by CAD ";
            //body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
            //body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
            body1 = body1 + "<br />";
            //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
            //body = body + "<br />";
            body1 = body1 + " &nbsp&nbsp Dear \"" + ls_relationshipmanager_name + "\",<br />";
            body1 = body1 + "<br />";

            body1 = body1 + "&nbsp&nbsp CAD has raised a query on document for customer  \"" + lscompany_name + "\"  ";
            body1 = body1 + "<br />";
            body1 = body1 + "<br />&nbsp&nbsp <b>Query Title:</b> " + lsquery_title + "<br /><br />";
            body1 = body1 + "&nbsp&nbsp <b>Query Description:</b> " + lsquery_description.Replace("'", "") + "<br /><br />";
            body1 = body1 + "&nbsp&nbsp <b>Document name:  </b>" + documenttype_name + "<br /><br />";
            body1 = body1 + "&nbsp&nbsp <b>RM name:</b> " + ls_relationshipmanager_name + "<br /><br />";
            body1 = body1 + "&nbsp&nbsp <b>Query raised by:</b> " + ls_raised_by + "<br /><br />";
            body1 = body1 + "&nbsp&nbsp <b>Query raised time:  </b>" + ls_raised_date + "<br /><br />";
            body1 = body1 + "<br />";
            body1 = body1 + "&nbsp&nbsp Note: Kindly review and reupload the documents.<br /> ";
            body1 = body1 + "<br />";
            body1 = body1 + "&nbsp&nbsp <b>Pathway: </b>";
            body1 = body1 + "<br />";
            body1 = body1 + "&nbsp&nbsp Login " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " > Sam-Custopedia > Customer info > Customer 360 > Process <br /> ";
            body1 = body1 + "&nbsp&nbsp button in action (Associated to the customer name) > Deferral/Covenant > <br /> ";
            body1 = body1 + "<br />";
            body1 = body1 + "&nbsp&nbsp This is a system generated mail, do not reply. Reach out to CAD for queries.<br /> ";
            body1 = body1 + "<br />";
            //body = body + "</td><td>&nbsp&nbsp</td></tr></table>";



            string[] ccmail_array = cc_mailid.Split(',');
            string[] ccmail1_array = cc1_mailid.Split(',');
            string[] ccmail2_array = cc2_mailid.Split(',');
            string[] ccmail3_array = cc3_mailid.Split(',');
            ccmail1_array = ccmail_array.Union(ccmail1_array).ToArray();
            ccmail2_array = ccmail1_array.Union(ccmail2_array).ToArray();
            ccmail_array = ccmail2_array.Union(ccmail3_array).ToArray();
            cc_mailid = string.Join(",", ccmail_array);

            MailMessage message1 = new MailMessage();
            SmtpClient smtp1 = new SmtpClient();
            message1.From = new MailAddress(ls_username);
            message1.To.Add(new MailAddress(tomail_id1));


            if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
            {
                lsCCReceipients = ccmail_array;
                if (cc_mailid.Length == 0)
                {
                    message1.CC.Add(new MailAddress(cc_mailid));
                }
                else
                {
                    foreach (string CCEmail in ccmail_array)
                    {
                        message1.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                    }
                }
            }
            lsBccmail_id = ConfigurationManager.AppSettings["QuerylBccMail"].ToString();
            if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
            {
                lsBCCReceipients = lsBccmail_id.Split(',');
                if (lsBccmail_id.Length == 0)
                {
                    message1.Bcc.Add(new MailAddress(lsBccmail_id));
                }
                else
                {
                    foreach (string BCCEmail in lsBCCReceipients)
                    {
                        message1.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                    }
                }
            }


            message1.Subject = sub1;
            message1.IsBodyHtml = true; //to make message body as html  
            message1.Body = body1;
            smtp1.Port = ls_port;
            smtp1.Host = ls_server; //for gmail host  
            smtp1.EnableSsl = true;
            smtp1.UseDefaultCredentials = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            smtp1.Credentials = new NetworkCredential(ls_username, ls_password);
            smtp1.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp1.Send(message1);

        }

        public void DaGetPendingQueryDelete(result values, string tagquery_gid)
        {
            msSQL = " delete from ocs_trn_ttagquery where tagquery_gid='" + tagquery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Query Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetUpdateCADRaiseQueryStatus(mdlcadquerystatusupdate values, string user_gid)
        {
            string lsstatus = "";
            if (values.query_status == "Y")
                lsstatus = Raisequerystatus.QueryRaised;
            else
                lsstatus = Raisequerystatus.Cancel;

            msSQL = " select application_gid from ocs_trn_ttagquery " +
                 " where groupdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "'";
            string lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            int query_no = 0;
            var lsquery_no = objdbconn.GetExecuteScalar("SELECT COUNT(*) FROM ocs_trn_ttagquery WHERE groupdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "' AND application_gid ='" + lsapplication_gid + "' AND  fromphysical_document = 'N' and query_status not in ('Pending','Cancelled')");
            if (lsquery_no == "")
            {
                query_no = 1;
            }
            else
            {
                query_no = Convert.ToInt16(lsquery_no) + 1;
            }
            msSQL = " update ocs_trn_ttagquery set query_status='" + lsstatus + "', " +
                    " querystatus_updatedby='" + user_gid + "'," +
                    " querystatus_updateddate= current_timestamp " +
                    " where tagquery_gid='" + values.tagquery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                  " where groupcovdocumentchecklist_gid ='" + values.documentcheckdtl_gid + "'";
                string lscovenant = objdbconn.GetExecuteScalar(msSQL);
                if (lscovenant != "")
                {
                    if (values.query_status == "Y")
                    {
                        msSQL = " update ocs_trn_ttagquery set query_no='" + query_no + "'" +
                                " where tagquery_gid='" + values.tagquery_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physicaloverall_docstatus='Query - Raised' " +
                          " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='Query - Raised', physicalcopyquerystatus='Query - Raised' " +
                                " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {

                    }
                        
                }
                else
                {
                    if (values.query_status == "Y")
                    {
                        msSQL = " update ocs_trn_ttagquery set query_no='" + query_no + "'" +
                                " where tagquery_gid='" + values.tagquery_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='Query - Raised' " +
                       " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='Query - Raised', physicalcopyquerystatus='Query - Raised' " +
                                " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {

                    }
               
                }

                if (values.query_status == "Y")
                {
                    fnSendRMraisequeryMail(values.documentcheckdtl_gid, values.tagquery_gid);

                }
                else
                {

                }
              
                values.status = true;
                values.message = "Query status updated successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostDeferralTaggedDoc(deferraltagged values, string user_gid)
        {
            string lsstatus_update = "";
            int checklistcount = values.deferraltaggedchecklist.Count;
            List<deferraltaggedchecklist> DocumentVerifyChecklist = values.deferraltaggedchecklist.Where(a => a.documentverified == true && a.deferraltagged == false).ToList();
            List<deferraltaggedchecklist> DeferralTaggedChecklist = values.deferraltaggedchecklist.Where(a => a.deferraltagged == true && a.documentverified == false).ToList();
            int DocumentVerifycount = DocumentVerifyChecklist.Count;

            foreach (var i in values.deferraltaggedchecklist)
            {
                string msGetchecklistGid = objcmnfunctions.GetMasterGID("DFCG");

                msSQL = " insert into ocs_trn_tdeferralchecklist( " +
                  " deferralchecklist_gid," +
                  " groupdocumentchecklist_gid," +
                  " mstchecklist_gid, " +
                  " checklist_name," +
                  " document_verified, " +
                  " deferral_tagged, " +
                  " created_by," +
                  " created_date" +
                  " )values(" +
                  "'" + msGetchecklistGid + "'," +
                  "'" + values.documentcheckdtl_gid + "'," +
                  "'" + i.mstchecklist_gid + "'," +
                  "'" + i.checklist_name + "'," +
                  "" + i.documentverified + "," +
                  "" + i.deferraltagged + "," +
                  "'" + user_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (checklistcount == DocumentVerifycount) // All Document Satisfied
            {
                lsstatus_update = "Satisfied";
                if (mnResult != 0)
                {
                    mdldocumentconfirmation objvalues = new mdldocumentconfirmation();
                    objvalues.overall_docstatus = "Satisfied";
                    objvalues.documentcheckdtl_gid = values.documentcheckdtl_gid;
                    objvalues.documentconfirmation_remarks = "";
                    DaPostPhysicalDocumentConfirmationDoc(objvalues, user_gid);
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document Satisfied Successfully..!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured..!";
                    }
                }
            }
            else
            {
                lsstatus_update = "Not Satisfied";
                if (mnResult != 0)
                {
                    msSQL = " update ocs_trn_tcaddocumentchecktls set overall_docstatus='Not Satisfied' " +
                            " where documentcheckdtl_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set overall_docstatus='Not Satisfied' " +
                            " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set overall_docstatus='Not Satisfied' " +
                            " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult == 1)
                {
                    foreach (var i in DeferralTaggedChecklist)
                    {
                        msSQL = " insert into ocs_trn_tnotcleardocchecklist( " +
                          " documentcheckdtl_gid," +
                          " mstchecklist_gid, " +
                          " checklist_name," +
                          " fromphysical_document," +
                          " created_by," +
                          " created_date" +
                          " )values(" +
                          "'" + values.documentcheckdtl_gid + "'," +
                          "'" + i.mstchecklist_gid + "'," +
                          "'" + i.checklist_name + "'," +
                          "'Y'," +
                          "'" + user_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Document Status updated to not satisfied..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }

            msSQL = " insert into ocs_trn_tcadscannedphysicalstatusupdatelog( " +
               " application_gid," +
               " groupdocumentchecklist_gid," +
               " covenant_type, " +
               " scanneddoc_flag," +
               " status_update, " +
               " created_by," +
               " created_date" +
               " )values(" +
               "'" + values.application_gid + "'," +
               "'" + values.documentcheckdtl_gid + "'," +
               "'" + values.covenant_type + "'," +
               "'" + values.scanneddoc_flag + "'," +
               "'" + lsstatus_update + "'," +
               "'" + user_gid + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        public void Datmpcleartagquerydocument(string tagquery_gid, result values)
        {
            msSQL = " delete from ocs_trn_tphysicaldocument where groupdocumentchecklist_gid='" + tagquery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
                values.status = true;
            else
                values.status = false;
        }


        public void DaPostMultipleInitiateExtensionorwaiver(mdlmultipleinitiateextendwaiver values, string user_gid)
        {
            string lsactivity_type = "";
            if (values.lsinitiate == "Extension")
                lsactivity_type = "Extension";
            else if (values.lsinitiate == "Waiver")
                lsactivity_type = "Waiver";
            else
                lsactivity_type = "Tag to Deferral";

            string lsdocumentcheckdtl_gid = "";
            string lsstatus = "";

            if (values.covenant_type == "Y")
            {
                msSQL = " select GROUP_CONCAT('\\\'',groupdocumentchecklist_gid,'\\\'')  from ocs_trn_tinitiateextendorwaiver " +
                     " where groupdocumentchecklist_gid in (select groupdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                     " where application_gid='" + values.application_gid + "' and credit_gid='" + values.credit_gid + "') " +
                     " and activity_type='" + lsactivity_type + "' and approval_initiation='N' and fromphysical_document='Y'";
                lsdocumentcheckdtl_gid = objdbconn.GetExecuteScalar(msSQL);
            }
            else
            {
                msSQL = " select GROUP_CONCAT('\\\'',groupdocumentchecklist_gid,'\\\'')  from ocs_trn_tinitiateextendorwaiver " +
                     " where groupdocumentchecklist_gid in (select groupdocumentchecklist_gid from ocs_trn_tcadgroupdocumentchecklist " +
                     " where application_gid='" + values.application_gid + "' and credit_gid='" + values.credit_gid + "') " +
                     " and activity_type='" + lsactivity_type + "' and approval_initiation='N' and fromphysical_document='Y'";
                lsdocumentcheckdtl_gid = objdbconn.GetExecuteScalar(msSQL);
            }
            if (lsdocumentcheckdtl_gid != "")
            {
                msSQL = " select GROUP_CONCAT('\\\'',initiateextendorwaiver_gid,'\\\'')  from ocs_trn_tinitiateextendorwaiver " +
                        " where groupdocumentchecklist_gid in (" + lsdocumentcheckdtl_gid + ") and approval_initiation='N' "+
                        " and fromphysical_document='Y'";
                string lsinitiateextendorwaiver_gid = objdbconn.GetExecuteScalar(msSQL);

                string msGetinitGid = objcmnfunctions.GetMasterGID("SPIG");
                msSQL = " update ocs_trn_tinitiateextendorwaiver set approval_initiation ='Y'," +
                      " approval_initiationgid='" + msGetinitGid + "'," +
                      " approval_initiatedby='" + user_gid + "'," +
                      " approval_status='Pending'," +
                      " approval_initiateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                      " where groupdocumentchecklist_gid in (" + lsdocumentcheckdtl_gid + ") " +
                      " and activity_type='" + lsactivity_type + "' and approval_initiation='N' and fromphysical_document='Y'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_textendorwaiverapproval set approval_status ='Pending' " +
                        " where initiateextendorwaiver_gid in (" + lsinitiateextendorwaiver_gid + ")";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (lsactivity_type == "Tag to Deferral")
                    lsstatus = "Deferral Approval Initiated";
                else
                    lsstatus = values.lsinitiate + " Approval Initiated";

                if (values.covenant_type == "Y")
                {
                    msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set physicaloverall_docstatus='" + lsstatus + "' " +
                            " where groupcovdocumentchecklist_gid in (" + lsdocumentcheckdtl_gid + ")";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set physicaloverall_docstatus='" + lsstatus + "' " +
                            " where groupcovdocumentchecklist_gid in (" + lsdocumentcheckdtl_gid + ")";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " update ocs_trn_tcaddocumentchecktls set physicaloverall_docstatus='" + lsstatus + "' " +
                            " where groupdocumentchecklist_gid in (" + lsdocumentcheckdtl_gid + ")";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set physicaloverall_docstatus='" + lsstatus + "' " +
                            " where groupdocumentchecklist_gid in (" + lsdocumentcheckdtl_gid + ")";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Initiated " + values.lsinitiate + " Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
            else
            {
                if (lsactivity_type == "Tag to Deferral")
                    lsstatus = "No records to initiate - Deferral";
                else
                    lsstatus = "No records to initiate - " + values.lsinitiate;

                values.status = false;
                values.message = lsstatus;
            }

        }

        public void DaGetConfirmationValidation(result values, string groupdocumentcheckdtl_gid, string lstype)
        {
            if (lstype == "Waived")
            {
                msSQL = " select initiateextendorwaiver_gid from ocs_trn_tinitiateextendorwaiver " +
                       " where groupdocumentchecklist_gid = '" + groupdocumentcheckdtl_gid + "' and activity_type = 'Waiver' " +
                       " and fromphysical_document='Y' and approval_status = 'Pending'";
            }
            else
            {
                msSQL = " select tagquery_gid from ocs_trn_ttagquery where groupdocumentchecklist_gid='" + groupdocumentcheckdtl_gid + "'" +
                        " and fromphysical_document='Y' and query_status in ('" + Raisequerystatus.QueryRaised + "','" + Raisequerystatus.Pending + "')";
            }
            string lspending = objdbconn.GetExecuteScalar(msSQL);
            if (lspending != "")
            {
                values.status = true;
                values.message = "Pending";
            }
            else
            {
                values.status = false;
                values.message = "No Record";
            }

        }

        public void DaUpdateDeferralTaggedDoc(deferraltagged values, string user_gid)
        {
            string lsstatus_update = "";
            List<deferraltaggedchecklist> documentverifiedCount = values.deferraltaggedchecklist.Where(a => a.documentverified == true && a.deferraltagged == false).ToList();
            List<deferraltaggedchecklist> TagtodeferralChecklist = values.deferraltaggedchecklist.Where(a => a.deferraltagged == true && a.documentverified == false).ToList();
            int docverifiedCount = documentverifiedCount.Count;
            int overallchecklistcount = values.deferraltaggedchecklist.Count;
            string lsdeferraltagdoc_gid = "";
            msSQL = " select deferraltagdoc_gid from ocs_trn_tdeferraltagdoc " +
                    " where groupdocumentchecklist_gid= '" + values.documentcheckdtl_gid + "' and fromphysical_document='N' order by deferraltagdoc_gid desc limit 1";
            //msSQL = " select deferraltagdoc_gid from ocs_trn_tdeferraltagdoc " +
            //        " where groupdocumentchecklist_gid = '" + values.documentcheckdtl_gid + "' and deferraltag_status='" + deferralTagstatus.Active + "'";
            lsdeferraltagdoc_gid = objdbconn.GetExecuteScalar(msSQL);
            foreach (var i in values.deferraltaggedchecklist)
            {
                msSQL = " update ocs_trn_tdeferralchecklist set document_verified=" + i.documentverified + ", " +
                        " deferral_tagged=" + i.deferraltagged + "," +
                        " updated_by ='" + user_gid + "', " +
                        " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where deferralchecklist_gid ='" + i.deferralchecklist_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (overallchecklistcount == docverifiedCount)
            {
                if (lsdeferraltagdoc_gid != "")
                {
                    msSQL = "update ocs_trn_tdeferraltagdoc set deferraltag_status='" + deferralTagstatus.DeferralTaken + "' where deferraltagdoc_gid='" + lsdeferraltagdoc_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                lsstatus_update = "Satisfied";
                if (mnResult != 0)
                {
                    mdldocumentconfirmation objvalues = new mdldocumentconfirmation();
                    objvalues.overall_docstatus = "Satisfied";
                    objvalues.documentcheckdtl_gid = values.documentcheckdtl_gid;
                    objvalues.documentconfirmation_remarks = "";
                    DaPostPhysicalDocumentConfirmationDoc(objvalues, user_gid);
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document Satisfied Successfully..!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured..!";
                    }
                }
            }
            if (TagtodeferralChecklist.Count > 0)
            {

                if (lsdeferraltagdoc_gid != "")
                {
                    msSQL = "update ocs_trn_tdeferraltagdoc set deferraltag_status='" + deferralTagstatus.Inactive + "' where deferraltagdoc_gid='" + lsdeferraltagdoc_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                lsstatus_update = "Not Satisfied";
                if (mnResult != 0)
                {
                    msSQL = " update ocs_trn_tcaddocumentchecktls set overall_docstatus='Not Satisfied' " +
                            " where documentcheckdtl_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set overall_docstatus='Not Satisfied' " +
                            " where groupcovdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set overall_docstatus='Not Satisfied' " +
                            " where groupdocumentchecklist_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult == 1)
                {
                    msSQL = "delete from ocs_trn_tnotcleardocchecklist where documentcheckdtl_gid='" + values.documentcheckdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    foreach (var i in TagtodeferralChecklist)
                    {
                        msSQL = " insert into ocs_trn_tnotcleardocchecklist( " +
                          " documentcheckdtl_gid," +
                          " mstchecklist_gid, " +
                          " checklist_name," +
                          " created_by," +
                          " created_date" +
                          " )values(" +
                          "'" + values.documentcheckdtl_gid + "'," +
                          "'" + i.mstchecklist_gid + "'," +
                          "'" + i.checklist_name + "'," +
                          "'" + user_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                } 

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Document Status updated to not satisfied..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }

            msSQL = " insert into ocs_trn_tcadscannedphysicalstatusupdatelog( " +
            " application_gid," +
            " groupdocumentchecklist_gid," +
            " covenant_type, " +
            " scanneddoc_flag," +
            " status_update, " +
            " created_by," +
            " created_date" +
            " )values(" +
            "'" + values.application_gid + "'," +
            "'" + values.documentcheckdtl_gid + "'," +
            "'" + values.covenant_type + "'," +
            "'" + values.scanneddoc_flag + "'," +
            "'" + lsstatus_update + "'," +
            "'" + user_gid + "'," +
            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }


        public void DaGetInitiatedApprovalExtensionorwaiver(mdlinitiateextendwaiverlist values, string approval_initiationgid)
        {
            msSQL = " select initiateextendorwaiver_gid,approval_initiation,groupdocumentchecklist_gid,activity_type,activity_title, " +
                    " date_format(a.due_date, '%d-%m-%Y') as due_date, " +
                    " date_format(a.extendeddue_date, '%d-%m-%Y') as extendeddue_date,reason,approval_status, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                    " case when (select concat(mstdocumenttype_name,' / ',mstdocument_name ) from ocs_trn_tcadgroupdocumentchecklist " +
                    " where groupdocumentchecklist_gid = a.groupdocumentchecklist_gid) = ''  " +
                    " then (select concat(mstdocumenttype_name,' / ',mstdocument_name ) from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                    " where groupcovdocumentchecklist_gid = a.groupdocumentchecklist_gid) " +
                    " else (select concat(mstdocumenttype_name,' / ',mstdocument_name ) from ocs_trn_tcadgroupdocumentchecklist " +
                    " where groupdocumentchecklist_gid = a.groupdocumentchecklist_gid) end as 'DocumentType', " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from ocs_trn_tinitiateextendorwaiver a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where approval_initiationgid = '" + approval_initiationgid + "' and fromphysical_document='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.mdlinitiateextendwaiver = dt_datatable.AsEnumerable().Select(row => new mdlinitiateextendwaiver
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    initiateextendorwaiver_gid = row["initiateextendorwaiver_gid"].ToString(),
                    activity_type = row["activity_type"].ToString(),
                    activity_title = row["activity_title"].ToString(),
                    extendeddue_date = row["extendeddue_date"].ToString(),
                    reason = row["reason"].ToString(),
                    approval_status = row["approval_status"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),
                    due_date = row["due_date"].ToString(),
                    approval_initiation = row["approval_initiation"].ToString(),
                    document_type = row["DocumentType"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
        }

      

        public void DaOriginalcopyVettingDocExport(MdlCADExportConversation values)
        {
            msSQL = "delete from ocs_tmp_tdownloadcount where created_date <>'" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select customer_name from ocs_trn_tcadapplication where application_gid ='" + values.application_gid + "'";
            string lscustomer_name = objdbconn.GetExecuteScalar(msSQL);


            msSQL = "select customer_name from ocs_tmp_tdownloadcount where customer_name='" + lscustomer_name.Replace("'", "") + "' and application_gid ='" + values.application_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                objODBCDataReader.Close();
                string lscount = "0";

                int days_count = Convert.ToInt32(lscount) + 1;

                msSQL = "insert into ocs_tmp_tdownloadcount (" +
                " customer_name," +
                " application_gid," +
                " day_count," +
                " created_date ) values(" +
                "'" + lscustomer_name + "'," +
                "'" + values.application_gid + "'," +
                "'" + days_count + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                objODBCDataReader.Close();
                msSQL = "select day_count from ocs_tmp_tdownloadcount where customer_name='" + lscustomer_name.Replace("'", "") + "'";
                string lscount = objdbconn.GetExecuteScalar(msSQL);

                int days_count = Convert.ToInt32(lscount) + 1;

                msSQL = "update ocs_tmp_tdownloadcount set day_count= '" + days_count + "' where  customer_name='" + lscustomer_name.Replace("'", "") + "' and application_gid ='" + values.application_gid + "'" +
                        " and created_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = " select day_count from ocs_tmp_tdownloadcount where customer_name='" + lscustomer_name.Replace("'", "") + "' and application_gid ='" + values.application_gid + "'";
            string customerwise_count = objdbconn.GetExecuteScalar(msSQL);


            string lsfilename = lscustomer_name.Replace(".", " ").Replace("'", " ").Replace("/", " ") + " " + DateTime.Now.ToString("yyyyMMdd") + customerwise_count;

            var query_no = objdbconn.GetExecuteScalar("SELECT MAX(query_no) FROM ocs_trn_ttagquery WHERE  application_gid ='" + values.application_gid + "' AND  fromphysical_document = 'Y' and query_status not in ('Pending','Cancelled')");
            //msSQL = "(SELECT groupdocumentchecklist_gid from ocs_trn_ttagquery where application_gid ='" + values.application_gid + "' AND fromphysical_document = 'N')";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var gettagquery_list = new List<tagquery_list>();
            //if (dt_datatable.Rows.Count != 0)
            //{


            msSQL = " (select document_code as 'Document Code',mstdocument_name as 'Document Name', " +
                " date_format(e.processupdated_date, '%d-%m-%Y %h:%i %p') as 'CAD Accepted Date',";

            if (!string.IsNullOrEmpty(query_no))
            {

                for (int i = 1; i <= Convert.ToInt16(query_no); i++)
                {



                    //int i = 0;
                    //foreach (DataRow dr_datarow in dt_datatable.Rows)
                    //{
                    //    i++;



                    msSQL += " (" +
                     " SELECT  date_format(b.created_date, '%d-%m-%Y') FROM ocs_trn_ttagquery b" +
                     " WHERE a.groupdocumentchecklist_gid = b.groupdocumentchecklist_gid and" +
                     "  query_no = '" + i + "'  AND application_gid ='" + values.application_gid + "' and b.fromphysical_document = 'Y' and query_status not in ('Pending','Cancelled') " +
                     " ) AS 'CAD Query Raised on " + i + "'," +
                     " (" +
                     " SELECT b.query_description FROM ocs_trn_ttagquery b" +
                     " WHERE a.groupdocumentchecklist_gid = b.groupdocumentchecklist_gid and" +
                     " query_no = '" + i + "'  AND application_gid ='" + values.application_gid + "' AND b.fromphysical_document = 'Y' and query_status not in ('Pending','Cancelled') " +
                     " ) AS 'Query Description " + i + "'," +
                     " (" +
                     " SELECT query_responseremarks FROM ocs_trn_ttagquery b" +
                     " WHERE a.groupdocumentchecklist_gid = b.groupdocumentchecklist_gid and" +
                     " query_no = '" + i + "'  AND application_gid ='" + values.application_gid + "' AND b.fromphysical_document = 'Y' and query_status not in ('Pending','Cancelled') " +
                     " ) AS 'RM Response" + i + "'," +
                     " (" +
                     " SELECT date_format(b.query_responseddate, '%d-%m-%Y') FROM ocs_trn_ttagquery b" +
                     " WHERE a.groupdocumentchecklist_gid = b.groupdocumentchecklist_gid and" +
                     " query_no = '" + i + "'  AND application_gid ='" + values.application_gid + "' AND b.fromphysical_document = 'Y' and query_status not in ('Pending','Cancelled') " +
                     " ) as 'Query Closed on " + i + "',";

                    //    }
                    //}
                }
            }

            msSQL += " a.physicalcopyquerystatus as 'Query Status' ,a.physicaloverall_docstatus as 'Final Status'" +
                     " from ocs_trn_tcadgroupdocumentchecklist a" +
                     " left join ocs_trn_tcadapplication e on e.application_gid = a.application_gid " +
                     " WHERE a.application_gid='" + values.application_gid + "' and a.untagged_type is null" +
                     " ORDER BY a.created_date ASC) " +
                     " Union " +
                     " (select document_code as 'Document Code',mstdocument_name as 'Document Name', " +
                    " date_format(e.processupdated_date, '%d-%m-%Y %h:%i %p') as 'CAD Accepted Date',";
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    int i = 0;
            //    foreach (DataRow dr_datarow in dt_datatable.Rows)
            //    {
            //        i++;


            if (!string.IsNullOrEmpty(query_no))
            {

                for (int i = 1; i <= Convert.ToInt16(query_no); i++)
                {

                    msSQL += " (" +
                     " SELECT  date_format(b.created_date, '%d-%m-%Y') FROM ocs_trn_ttagquery b" +
                     " WHERE a.groupcovdocumentchecklist_gid = b.groupdocumentchecklist_gid and" +
                     " query_no = '" + i + "' AND  application_gid ='" + values.application_gid + "' AND b.fromphysical_document = 'Y' and query_status not in ('Pending','Cancelled') " +
                     " ) AS 'CAD Query Raised on " + i + "'," +
                     " (" +
                     " SELECT b.query_description FROM ocs_trn_ttagquery b" +
                     " WHERE a.groupcovdocumentchecklist_gid = b.groupdocumentchecklist_gid and" +
                     " query_no = '" + i + "' AND  application_gid ='" + values.application_gid + "' AND b.fromphysical_document = 'Y' and query_status not in ('Pending','Cancelled') " +
                     " ) AS 'Query Description " + i + "'," +
                     " (" +
                     " SELECT query_responseremarks FROM ocs_trn_ttagquery b" +
                     " WHERE a.groupcovdocumentchecklist_gid = b.groupdocumentchecklist_gid and" +
                     " query_no = '" + i + "' AND  application_gid ='" + values.application_gid + "' AND b.fromphysical_document = 'Y' and query_status not in ('Pending','Cancelled') " +
                     " ) AS 'RM Response" + i + "'," +
                     " (" +
                     " SELECT date_format(b.query_responseddate, '%d-%m-%Y') FROM ocs_trn_ttagquery b" +
                     " WHERE a.groupcovdocumentchecklist_gid = b.groupdocumentchecklist_gid and" +
                     " query_no = '" + i + "'  AND application_gid ='" + values.application_gid + "' AND b.fromphysical_document = 'Y' and query_status not in ('Pending','Cancelled') " +
                     " ) as 'Query Closed on " + i + "',";

                }
            }

            msSQL += " a.physicalcopyquerystatus as 'Query Status' ,a.physicaloverall_docstatus as 'Final Status'" +
                     " from ocs_trn_tcadgroupcovenantdocumentchecklist a" +
                     " left join ocs_trn_tcadapplication e on e.application_gid = a.application_gid " +
                     " WHERE a.application_gid='" + values.application_gid + "' and a.untagged_type is null" +
                     " ORDER BY a.created_date ASC) ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                string lscompany_code = string.Empty;
                MemoryStream ms = new MemoryStream();
                ExcelPackage excel = new ExcelPackage(ms);
                var workSheet = excel.Workbook.Worksheets.Add("" + lsfilename + "");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";
                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.attachment_name = "" + lsfilename + "" + ".xlsx";
                    var path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "Master/CAD Original Copy vetting/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                    values.attachment_path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "Master/CAD Original Copy vetting/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.attachment_name);
                    values.attachment_cloudpath = lscompany_code + "/" + "Master/CAD Original Copy vetting/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.attachment_name;
                    bool exists = System.IO.Directory.Exists(path);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    workSheet.Cells["A14"].LoadFromDataTable(dt_datatable, true);
                    dt_datatable.Dispose();

                    workSheet.Cells["B1"].Value = "CAD Original Copy vetting -";
                    workSheet.View.FreezePanes(14, 4);
                    using (var range = workSheet.Cells[1, 2, 1, 5])
                    {
                        range.Merge = true;
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        range.Style.Font.Color.SetColor(Color.Black);
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    //workSheet.Cells.Style.Locked = false;
                    //workSheet.Column(1).Style.Locked = true;
                    //workSheet.Column(2).Style.Locked = true;

                    //workSheet.Column(3).Style.Locked = true;
                    //workSheet.Column(4).Style.Locked = true;
                    //workSheet.Column(5).Style.Locked = true;
                    //workSheet.Column(6).Style.Locked = true;

                    workSheet.Column(1).AutoFit();
                    workSheet.Column(2).AutoFit();
                    workSheet.Column(3).AutoFit();
                    workSheet.Column(4).AutoFit();
                    workSheet.Column(5).AutoFit();
                    workSheet.Column(6).AutoFit();

                    //workSheet.Protection.AllowEditObject = true;
                    //workSheet.Protection.AllowEditScenarios = true;
                    //workSheet.Protection.AllowFormatCells = true;
                    //workSheet.Protection.AllowFormatColumns = true;
                    //workSheet.Protection.AllowFormatRows = true;
                    //workSheet.Protection.IsProtected = true;
                    //workSheet.Protection.SetPassword("Welcome@123");

                    workSheet.Cells["B2"].Value = "ARN Number";
                    using (var range = workSheet.Cells["B2"])
                    {

                        range.Style.Font.Bold = true;

                    }
                    workSheet.Cells["B3"].Value = "Borrower Name";
                    using (var range = workSheet.Cells["B3"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B4"].Value = "Vertical";
                    using (var range = workSheet.Cells["B4"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    //workSheet.Cells["B8"].Value = "Security Details";
                    //using (var range = workSheet.Cells["B8"])
                    //{
                    //    range.Style.Font.Bold = true;
                    //}
                    workSheet.Cells["B5"].Value = "Program";
                    using (var range = workSheet.Cells["B5"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    //workSheet.Cells["B7"].Value = "Facility";
                    //using (var range = workSheet.Cells["B7"])
                    //{
                    //    range.Style.Font.Bold = true;
                    //}
                    workSheet.Cells["B6"].Value = "Facility Name";
                    using (var range = workSheet.Cells["B6"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B7"].Value = "Facility sub type";
                    using (var range = workSheet.Cells["B7"])
                    {
                        range.Style.Font.Bold = true;
                        // range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    }
                    workSheet.Cells["B8"].Value = "CC Approved date";
                    using (var range = workSheet.Cells["B8"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B9"].Value = "Sanctioned Ref.No";
                    using (var range = workSheet.Cells["B9"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B10"].Value = "Sanctioned On";
                    using (var range = workSheet.Cells["B10"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B11"].Value = "Sanctioned Amount";

                    using (var range = workSheet.Cells["B11"])
                    {
                        range.Style.Font.Bold = true;


                    }
                    workSheet.Cells["B12"].Value = "RM Name ";

                    using (var range = workSheet.Cells["B12"])
                    {
                        range.Style.Font.Bold = true;


                    }


                    msSQL = " select a.application_no, a.customer_name,a.vertical_name,a.program_name, " +
                             " group_concat(distinct(concat(e.product_type))) as `product`, " +
                             " group_concat(distinct(concat( e.productsub_type))) as `subproduct`, " +
                             " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as 'ccapproveddate', " +
                             " i.sanction_refno,  date_format(i.sanction_date, '%d-%m-%Y') as sanction_date,sanction_amount," +
                             " a.relationshipmanager_name " +
                             " from ocs_trn_tcadapplication a " +
                             " left join ocs_trn_tcadapplication2loan e on e.application_gid = a.application_gid " +
                             " left join ocs_trn_tapplication2sanction i on i.application_gid = a.application_gid " +
                             " where a.application_gid ='" + values.application_gid + "' group by a.application_gid order by a.application_gid";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        workSheet.Cells["B1"].Value = "CAD Softcopy vetting -" + objODBCDataReader["customer_name"].ToString();
                        workSheet.Cells["C2"].Value = objODBCDataReader["application_no"].ToString();
                        workSheet.Cells["C3"].Value = objODBCDataReader["customer_name"].ToString();
                        workSheet.Cells["C4"].Value = objODBCDataReader["vertical_name"].ToString();
                        workSheet.Cells["C5"].Value = objODBCDataReader["program_name"].ToString();
                        workSheet.Cells["C6"].Value = objODBCDataReader["product"].ToString();

                        workSheet.Cells["C7"].Value = objODBCDataReader["subproduct"].ToString();
                        workSheet.Cells["C8"].Value = objODBCDataReader["ccapproveddate"].ToString();
                        workSheet.Cells["C9"].Value = objODBCDataReader["sanction_refno"].ToString(); ;
                        workSheet.Cells["C10"].Value = objODBCDataReader["sanction_date"].ToString();
                        workSheet.Cells["C11"].Value = objODBCDataReader["sanction_amount"].ToString();
                        workSheet.Cells["C12"].Value = objODBCDataReader["relationshipmanager_name"].ToString();

                    }
                    objODBCDataReader.Close();
                    FileInfo file = new FileInfo(values.attachment_path);
                    using (var range = workSheet.Cells[14, 1, 14, 40])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        range.Style.Font.Color.SetColor(Color.Black);

                    }



                    excel.SaveAs(ms);
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CAD Original Copy vetting/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.attachment_name, ms);
                    ms.Close();

                }

                catch (Exception ex)
                {
                    values.status = false;
                    values.message = "Failure";
                }
                values.status = true;
                values.message = "Success";
                values.attachment_cloudpath = objcmnstorage.EncryptData(values.attachment_cloudpath);
                values.attachment_path = objcmnstorage.EncryptData(values.attachment_path);

            }
            else
            {
                values.status = false;
                values.message = "No records to export!";
                return;
            }
            dt_datatable.Dispose();


        }

        public void DaGetPhysicalAppcadQuerySummaryRm(mslcadquerylist values, string documentcheckdtl_gid)
        {

            msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                 " where groupcovdocumentchecklist_gid ='" + documentcheckdtl_gid + "'";
            string lscovenant = objdbconn.GetExecuteScalar(msSQL);
            if (lscovenant != "")
            {
                msSQL = " select mstdocumenttype_name as documenttype_code,mstdocument_name as documenttype_name, " +
                        " date_format(physicaldocumentsubmission_date, '%d-%m-%Y') as documentsubmission_date from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                        " where groupcovdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
            }
            else
            {
                msSQL = " select mstdocumenttype_name as documenttype_code,mstdocument_name as documenttype_name, " +
                        " date_format(physicaldocumentsubmission_date, '%d-%m-%Y') as documentsubmission_date from ocs_trn_tcadgroupdocumentchecklist " +
                        " where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
            }
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.documenttype_code = objODBCDataReader["documenttype_code"].ToString();
                values.documenttype_name = objODBCDataReader["documenttype_name"].ToString();
                values.documentsubmission_date = objODBCDataReader["documentsubmission_date"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " select (select count(*) from ocs_trn_tdeferralchecklist x where x.groupdocumentchecklist_gid =  '" + documentcheckdtl_gid + "' " +
                " and x.fromphysical_document = 'Y' LIMIT 1) as checklistcount, " +
                " (SELECT COUNT(*) from ocs_trn_tinitiateextendorwaiver z " +
                " where z.groupdocumentchecklist_gid ='" + documentcheckdtl_gid + "' and z.fromphysical_document = 'Y' " +
                " and approval_status in ('Pending', 'Approved') and activity_type = 'Waiver') as waiverpendingcount, " +
                " (select deferraltagdoc_gid  from ocs_trn_tdeferraltagdoc where groupdocumentchecklist_gid ='" + documentcheckdtl_gid + "' " +
                " and deferraltag_status in ('1','2','3') and fromphysical_document = 'Y' order by deferraltagdoc_gid desc limit 1) as deferraltagdoc_gid ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checklistcount = objODBCDatareader["checklistcount"].ToString();
                values.waiverpendingcount = objODBCDatareader["waiverpendingcount"].ToString();
                values.deferraltagdoc_gid = objODBCDatareader["deferraltagdoc_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select tagquery_gid,document_gid,groupdocumentchecklist_gid, query_title,query_description,query_status,query_to,query_toname, " +
                    " query_responseremarks, date_format(a.query_responseddate, '%d-%m-%Y %h:%i %p') as query_responseddate, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as query_responsedby, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as query_raisedby, " +
                    " query_code, document_name, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as query_raiseddate  " +
                    " from ocs_trn_ttagquery a" +
                    " left join adm_mst_tuser b on a.query_responsedby = b.user_gid " +
                    " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                    " where groupdocumentchecklist_gid = '" + documentcheckdtl_gid + "' and a.fromphysical_document='Y' and  query_status !='Cancelled'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.mdlcadquery = dt_datatable.AsEnumerable().Select(row => new mdlcadquery
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    tagquery_gid = row["tagquery_gid"].ToString(),
                    query_title = row["query_title"].ToString(),
                    query_description = row["query_description"].ToString(),
                    query_status = row["query_status"].ToString(),
                    query_toname = row["query_toname"].ToString(),
                    query_responseremarks = row["query_responseremarks"].ToString(),
                    query_responseddate = row["query_responseddate"].ToString(),
                    query_responsedby = row["query_responsedby"].ToString(),
                    created_by = row["query_raisedby"].ToString(),
                    created_date = row["query_raiseddate"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_gid = row["document_gid"].ToString(),
                    query_code = row["query_code"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
        }


    }
}