using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.storage.Functions;
using ems.master.Models;
using System.IO;
using OfficeOpenXml;
using System.Configuration;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.NetworkInformation;

/// <summary>
/// (It's used for Softcopy and Original copy report page in Samfin)CadCovenentReport DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaMstCadCovenentReport
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDataReader;
        string msSQL;
        int mnResult;

        public void DaGetScannedDocMakerPendingSummary(reportscannedmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, e.sanction_refno," +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as maker_name, " +
                    " a.creditgroup_gid, d.cadgroup_name from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                    " left join hrm_mst_temployee g on g.employee_gid = d.maker_gid " +
                    " left join adm_mst_tuser f on f.user_gid = g.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.ScannedDocument + "' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' order by d.created_date desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<reportscannedmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new reportscannedmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroup_name = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        maker_name = dt["maker_name"].ToString()
                    });
                }
            }
            values.reportscannedmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }


        public void DaGetScannedDocCheckerPendingSummary(reportscannedmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by,a.customer_urn, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as checker_name, " +
                    " a.creditgroup_gid, d.cadgroup_name,e.sanction_refno from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                      " left join hrm_mst_temployee g on g.employee_gid = d.checker_gid " +
                    " left join adm_mst_tuser f on f.user_gid = g.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.ScannedDocument + "' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'Y' and d.checker_approvalflag = 'N' and d.completed_flag='N' order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<reportscannedmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new reportscannedmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroup_name = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        checker_name = dt["checker_name"].ToString()
                    });
                }
            }
            values.reportscannedmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetScannedDocApproverPendingSummary(reportscannedmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                       " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as approver_name, " +
                    " a.creditgroup_gid, d.cadgroup_name,e.sanction_refno,a.customer_urn from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                      " left join hrm_mst_temployee g on g.employee_gid = d.approver_gid " +
                    " left join adm_mst_tuser f on f.user_gid = g.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.ScannedDocument + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag='Y' and d.checker_approvalflag = 'Y' and d.completed_flag='N' and d.approver_approvalflag='N'" +
                    " order by d.created_date desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<reportscannedmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new reportscannedmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroup_name = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        approver_name = dt["approver_name"].ToString()
                    });
                }
            }
            values.reportscannedmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }


        public void DaGetPhysicalDocMakerPendingSummary(reportphyiscalmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, e.sanction_refno, d.overall_approvalstatus, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as maker_name, " +
                    " a.creditgroup_gid, d.cadgroup_name from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee g on g.employee_gid = d.maker_gid " +
                    " left join adm_mst_tuser f on f.user_gid = g.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<reportphyiscalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new reportphyiscalmakerapplication
                    {

                        application_no = dt["application_no"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroup_name = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        maker_name = dt["maker_name"].ToString()
                    });
                }
            }
            values.reportphyiscalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }


        public void DaGetPhysicalDocCheckerPendingSummary(reportphyiscalmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                         " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as checker_name, " +
                    " a.creditgroup_gid, d.cadgroup_name from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee g on g.employee_gid = d.checker_gid " +
                    " left join adm_mst_tuser f on f.user_gid = g.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'Y' and d.checker_approvalflag = 'N' order by a.updated_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<reportphyiscalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new reportphyiscalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroup_name = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        checker_name = dt["checker_name"].ToString()
                    });
                }
            }
            values.reportphyiscalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetPhysicalDocApproverPendingSummary(reportphyiscalmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as approver_name, " +
                    " a.creditgroup_gid, d.cadgroup_name from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join hrm_mst_temployee e on e.employee_gid = d.approver_gid " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag='Y' and d.checker_approvalflag = 'Y' and d.completed_flag='N' and d.approver_approvalflag='N'" +
                    " order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<reportphyiscalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new reportphyiscalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroup_name = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        approver_name = dt["approver_name"].ToString()
                    });
                }
            }
            values.reportphyiscalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetAppPhysicalDocCount(CadScannedCount values)
        {
            msSQL = " select(select count(*) from ocs_trn_tprocesstype_assign a  " +
                    " left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                    " where maker_approvalflag = 'N' and b.docchecklist_approvalflag = 'Y'  and completed_flag = 'N' " +
                    " and menu_gid = '" + getMenuClass.PhysicalDocument + "') as MakerPendingCount , " +
                    " (select count(*) from ocs_trn_tprocesstype_assign " +
                    " where checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                    " and menu_gid = '" + getMenuClass.PhysicalDocument + "') as CheckerApprovalPendingCount, " +
                    " (select count(*) from ocs_trn_tprocesstype_assign " +
                    " where approver_approvalflag = 'N' and checker_approvalflag = 'Y'  and completed_flag = 'N' " +
                    " and menu_gid = '" + getMenuClass.PhysicalDocument + "')  as ApproverPendingCount ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.MakerPendingCount = objODBCDataReader["MakerPendingCount"].ToString();
                values.CheckerApprovalPendingCount = objODBCDataReader["CheckerApprovalPendingCount"].ToString();
                values.ApproverPendingCount = objODBCDataReader["ApproverPendingCount"].ToString();
            }
            objODBCDataReader.Close();
        }

        public void DaGetAppScannedDocCount(CadScannedCount values)
        {
            msSQL = " select(select count(*) from ocs_trn_tprocesstype_assign a  " +
                    " left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                    " where maker_approvalflag = 'N' and b.docchecklist_approvalflag = 'Y'  and completed_flag = 'N' " +
                    " and menu_gid = '" + getMenuClass.ScannedDocument + "') as MakerPendingCount , " +
                    " (select count(*) from ocs_trn_tprocesstype_assign " +
                    " where checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                    " and menu_gid = '" + getMenuClass.ScannedDocument + "') as CheckerApprovalPendingCount, " +
                    " (select count(*) from ocs_trn_tprocesstype_assign " +
                    " where approver_approvalflag = 'N' and checker_approvalflag = 'Y'  and completed_flag = 'N' " +
                    " and menu_gid = '" + getMenuClass.ScannedDocument + "')  as ApproverPendingCount ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.MakerPendingCount = objODBCDataReader["MakerPendingCount"].ToString();
                values.CheckerApprovalPendingCount = objODBCDataReader["CheckerApprovalPendingCount"].ToString();
                values.ApproverPendingCount = objODBCDataReader["ApproverPendingCount"].ToString();
            }
            objODBCDataReader.Close();
        }


        public void DaGetScannedDefPendingReport(string lsstatus, Mdlscannedapplexport objvalues)
        {
            if (lsstatus == "Maker")
            {
                msSQL = "select a.application_no as 'ARN NO',  " +
                    "CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                    "d.cadgroup_name as 'CAD Group Name'," +
                    "CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                    "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                    "group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                    "CASE WHEN(f.deferraltag_status   = '1') then 'Tagged' " +
                    "WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken' " +
                    "ELSE '-' END as 'Deferral Status' ,  " +
                    " (select GROUP_CONCAT( (CONCAT ( (@s:=@s+1), '.', ( query_description),' - ',IFNULL(query_responseremarks,' Pending'))) SEPARATOR '\r\n')  " +
                    " from  ocs_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                    " and fromphysical_document='N') as 'Query - Response'," +
                    "group_concat(date_format(c.extendeddue_date,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date'" +
                    "from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tcadgroupdocumentchecklist c on c.application_gid = a.application_gid" +
                    " left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                    " left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" +
                    " left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                    " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                    ", (SELECT @s:= 0) AS s" +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTDTS' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N'" +
                    " group by c.groupdocumentchecklist_gid order by a.application_gid ";
            }
            else if (lsstatus == "Checker")
            {
                msSQL = "select a.application_no as 'ARN NO',  " +
                    "CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                    "d.cadgroup_name as 'CAD Group Name'," +
                    "CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                     "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                    "group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                    "CASE WHEN(f.deferraltag_status   = '1') then 'Tagged' " +
                    "WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken' " +
                    "ELSE '-' END as 'Deferral Status' ,  " +
                     " (select GROUP_CONCAT( (CONCAT ( (@s:=@s+1), '.', ( query_description),' - ',IFNULL(query_responseremarks,' Pending'))) SEPARATOR '\r\n')  " +
                    " from  ocs_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                    " and fromphysical_document='N') as 'Query - Response'," +
                    "group_concat(date_format(c.extendeddue_date,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date'" +
                    "from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tcadgroupdocumentchecklist c on c.application_gid = a.application_gid" +
                    " left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                    " left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" +
                    " left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                    " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                    ", (SELECT @s:= 0) AS s" +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTDTS' and d.completed_flag='N'" +
                    " and a.docchecklist_approvalflag = 'Y' and checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                    " group by c.groupdocumentchecklist_gid order by a.application_gid ";
            }
            else
            {
                msSQL = "select a.application_no as 'ARN NO',  " +
                   "CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                   "d.cadgroup_name as 'CAD Group Name'," +
                   "CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                    "CASE WHEN b.stakeholder_type is null  then j.stakeholder_type ELSE b.stakeholder_type END as 'Stakeholder Type'," +
                    "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                   "group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                   "CASE WHEN(f.deferraltag_status   = '1') then 'Tagged' " +
                   "WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken' " +
                   "ELSE '-' END as 'Deferral Status' ,  " +
                   " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n')  " +
                    " from  ocs_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                    " and fromphysical_document='N') as 'Query - Response'," +
                    " group_concat(date_format(c.extendeddue_date,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date'" +
                   "from ocs_trn_tcadapplication a " +
                   " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                   " left join ocs_trn_tcadgroupdocumentchecklist c on c.application_gid = a.application_gid" +
                   " left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                   " left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" +
                   " left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                   " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                   ", (SELECT @s:= 0) AS s" +
                   " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTDTS' and d.completed_flag='N' " +
                   " and a.docchecklist_approvalflag = 'Y' and approver_approvalflag = 'N' and checker_approvalflag = 'Y'  and completed_flag = 'N'  " +
                   " group by c.groupdocumentchecklist_gid order by a.application_gid ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("CADScannedDeferral-" + lsstatus + "PendingReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objvalues.lsname = "CADScannedDeferral-" + lsstatus + "PendingReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADScannedDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objvalues.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADScannedDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                objvalues.lscloudpath = lscompany_code + "/" + "Master/CADScannedDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objvalues.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 72])  //Address "A1:BD1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objvalues.lscloudpath, ms);
                ms.Close();
                objvalues.lscloudpath = objcmnstorage.EncryptData(objvalues.lscloudpath);
                objvalues.lspath = objcmnstorage.EncryptData(objvalues.lspath);
                objvalues.status = true;
                objvalues.message = "Success";
            }
            catch (Exception ex)
            {
                objvalues.status = false;
                objvalues.message = "Failure";
            }

        }

        public void DaGetPhysicalDefPendingReport(string lsstatus, Mdlscannedapplexport objvalues)
        {
            if (lsstatus == "Maker")
            {
                msSQL = "select a.application_no as 'ARN NO',    " +
                    " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                    " d.cadgroup_name as 'CAD Group Name'," +
                    " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                    "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                    " group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged'  " +
                    " WHEN(f.deferraltag_status   = '1') THEN 'Deferral Taken'   " +
                    " ELSE '-' END as 'Deferral Status',  " +
                    " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n') " +
                    " from ocs_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                    " and fromphysical_document = 'N') as 'Query - Response', " +
                    " group_concat(date_format(c.physical_extendedduedate,'%d-%m-%Y')  SEPARATOR ' || ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" + 
                    " from ocs_trn_tcadapplication a     " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid  " +
                    " left join ocs_trn_tcadgroupdocumentchecklist c on c.application_gid = a.application_gid " +
                    "  left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                    "   left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" +
                    "  left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                    " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                    " , (SELECT @s:= 0) AS s" +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTPYD' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' " +
                    " group by c.groupdocumentchecklist_gid order by a.application_gid ";
            }
            else if (lsstatus == "Checker")
            {
                msSQL = "select a.application_no as 'ARN NO',    " +
                    " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                    " d.cadgroup_name as 'CAD Group Name'," +
                   " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                     "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                    " group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged'  " +
                    " WHEN(f.deferraltag_status   = '1') THEN 'Deferral Taken'   " +
                    " ELSE '-' END as 'Deferral Status' ,  " +
                 " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n') " +
                 " from ocs_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                  " and fromphysical_document = 'N') as 'Query - Response', " +
                  "  group_concat(date_format(c.physical_extendedduedate,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date'" +
                  " from ocs_trn_tcadapplication a     " +
                 " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid  " +
                 " left join ocs_trn_tcadgroupdocumentchecklist c on c.application_gid = a.application_gid " +
                 "  left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                 "   left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" +
                 "  left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                  " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                  " , (SELECT @s:= 0) AS s" +
                  " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTPYD' and d.completed_flag='N' " +
                  " and d.checker_approvalflag = 'N' and d.maker_approvalflag = 'Y' and a.docchecklist_approvalflag = 'Y'  " +
                 " group by c.groupdocumentchecklist_gid order by a.application_gid ";
            }
            else
            {
                msSQL = "select a.application_no as 'ARN NO',    " +
                    " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                    " d.cadgroup_name as 'CAD Group Name'," +
                    " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                       "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                    " group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged'  " +
                    " WHEN(f.deferraltag_status   = '1') THEN 'Deferral Taken'   " +
                    " ELSE '-' END as 'Deferral Status' ,  " +
                  " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n') " +
                    " from ocs_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                    " and fromphysical_document = 'N') as 'Query - Response', " +
                    "  group_concat(date_format(c.physical_extendedduedate,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date'" +
                    " from ocs_trn_tcadapplication a     " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid  " +
                    " left join ocs_trn_tcadgroupdocumentchecklist c on c.application_gid = a.application_gid " +
                    "  left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                    "   left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" +
                    "  left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                     " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                    " , (SELECT @s:= 0) AS s" +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTPYD' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.approver_approvalflag = 'N' and d.checker_approvalflag = 'Y'  " +
                    " group by c.groupdocumentchecklist_gid order by a.application_gid ";

            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("CADPhysicalDeferral-" + objvalues.lsstatus + "PendingReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objvalues.lsname = "CADPhysicalDeferral-" + lsstatus + "taskPendingReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADPhysicalDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objvalues.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADPhysicalDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                objvalues.lscloudpath = lscompany_code + "/" + "Master/CADPhysicalDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objvalues.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 72])  //Address "A1:BD1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objvalues.lscloudpath, ms);
                ms.Close();
                objvalues.lscloudpath = objcmnstorage.EncryptData(objvalues.lscloudpath);
                objvalues.lspath = objcmnstorage.EncryptData(objvalues.lspath);
                objvalues.status = true;
                objvalues.message = "Success";
            }
            catch (Exception ex)
            {
                objvalues.status = false;
                objvalues.message = "Failure";
            }
        }

        public void DaGetScannedCovPendingReport(string lsstatus, Mdlscannedapplexport objvalues)
        {
            if (lsstatus == "Maker")
            {
                msSQL = "select a.application_no as 'ARN NO',  " +
                    "CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                    "d.cadgroup_name as 'CAD Group Name'," +
                    "CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                    "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                    " group_concat( distinct c.mstdocumenttype_name SEPARATOR ' || ' ) as 'Document Type', group_concat( distinct c.mstdocument_name SEPARATOR ' || ') as 'Document Name' ," +
                    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged' " +
                    " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken' " +
                    " WHEN(f.deferraltag_status   = '3') THEN 'Approval Pending' " +
                    " ELSE '-' END as 'Deferral Status' ,  " +
                    " (select GROUP_CONCAT( (CONCAT ( (@s:=@s+1), '.', ( query_description),' - ',IFNULL(query_responseremarks,' Pending'))) SEPARATOR '\r\n')  " +
                    " from  ocs_trn_ttagquery where application_gid = a.application_gid and groupcovdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                    " and fromphysical_document='N') as 'Query - Response'," +
                    " group_concat(date_format(c.extendeddue_date,'%d-%m-%Y') SEPARATOR ' || ') as 'Extended Due Date' , d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                    " from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid" +
                    " left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                    " left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" + 
                    " left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                    " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                    ", (SELECT @s:= 0) AS s" +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTDTS' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N'" +
                    " group by c.groupcovdocumentchecklist_gid order by a.application_gid ";
            }
            else if (lsstatus == "Checker")
            {
                msSQL = "select a.application_no as 'ARN NO',  " +
                    "CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                    "d.cadgroup_name as 'CAD Group Name'," +
                    "CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                     "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                    " group_concat( distinct c.mstdocumenttype_name SEPARATOR ' || ' ) as 'Document Type', group_concat( distinct c.mstdocument_name SEPARATOR ' || ') as 'Document Name' ," +
                    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged' " +
                    " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken' " +
                    " WHEN(f.deferraltag_status   = '3') THEN 'Approval Pending' " +
                    " ELSE '-' END as 'Deferral Status' ,  " +
                     " (select GROUP_CONCAT( (CONCAT ( (@s:=@s+1), '.', ( query_description),' - ',IFNULL(query_responseremarks,' Pending'))) SEPARATOR '\r\n')  " +
                    " from  ocs_trn_ttagquery where application_gid = a.application_gid and groupcovdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                    " and fromphysical_document='N') as 'Query - Response'," +
                    " group_concat(date_format(c.extendeddue_date,'%d-%m-%Y') SEPARATOR ' || ') as 'Extended Due Date' , d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                    " from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid" +
                    " left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                    " left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" + 
                    " left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                    " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                    ", (SELECT @s:= 0) AS s" +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTDTS' and d.completed_flag='N'" +
                    " and a.docchecklist_approvalflag = 'Y' and checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                    " group by c.groupcovdocumentchecklist_gid order by a.application_gid ";
            }
            else
            {
                msSQL = "select a.application_no as 'ARN NO',  " +
                   "CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                   "d.cadgroup_name as 'CAD Group Name'," +
                   "CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                    "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                   " group_concat( distinct c.mstdocumenttype_name SEPARATOR ' || ' ) as 'Document Type', group_concat( distinct c.mstdocument_name SEPARATOR ' || ') as 'Document Name' ," +
                   " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged' " +
                   " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken' " +
                   " WHEN(f.deferraltag_status   = '3') THEN 'Approval Pending' " +
                   " ELSE '-' END as 'Deferral Status' ,  " +
                   " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n')  " +
                    " from  ocs_trn_ttagquery where application_gid = a.application_gid and groupcovdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                    " and fromphysical_document='N') as 'Query - Response'," +
                    " group_concat(date_format(c.extendeddue_date,'%d-%m-%Y') SEPARATOR ' || ') as 'Extended Due Date',  d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                   " from ocs_trn_tcadapplication a " +
                   " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                   " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid" +
                   " left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                   " left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" +
                   " left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                   " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                   ", (SELECT @s:= 0) AS s" +
                   " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTDTS' and d.completed_flag='N' " +
                   " and a.docchecklist_approvalflag = 'Y' and approver_approvalflag = 'N' and checker_approvalflag = 'Y'  and completed_flag = 'N'  " +
                   " group by c.groupcovdocumentchecklist_gid order by a.application_gid ";
            }
         
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("CADScannedCovenant-" + lsstatus + "PendingReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objvalues.lsname = "CADSoftcopyCovenant-" + lsstatus + "PendingReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADScannedCovenant-" + objvalues.lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objvalues.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADScannedCovenant-" + objvalues.lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                objvalues.lscloudpath = lscompany_code + "/" + "Master/CADScannedCovenant-" + objvalues.lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objvalues.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 72])  //Address "A1:BD1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objvalues.lscloudpath, ms);
                ms.Close();
                objvalues.lscloudpath = objcmnstorage.EncryptData(objvalues.lscloudpath);
                objvalues.lspath = objcmnstorage.EncryptData(objvalues.lspath);
                objvalues.status = true;
                objvalues.message = "Success";
            }
            catch (Exception ex)
            {
                objvalues.status = false;
                objvalues.message = "Failure";
            }

        }

        public void DaGetPhysicalCovPendingReport(string lsstatus, Mdlscannedapplexport objvalues)
        {
            if (lsstatus == "Maker")
            {
                msSQL = "select a.application_no as 'ARN NO',    " +
                    " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                    " d.cadgroup_name as 'CAD Group Name'," +
                    " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                    "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                    " group_concat( distinct c.mstdocumenttype_name SEPARATOR ' || ' ) as 'Document Type', group_concat(distinct c.mstdocument_name SEPARATOR ' || ') as 'Document Name' ," +
                    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged'  " +
                    " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken'   " +
                    " WHEN(f.deferraltag_status   = '3') THEN 'Approval Pending' " +
                    " ELSE '-' END as 'Deferral Status' ,  " +
                    " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n') " +
                    " from ocs_trn_ttagquery where application_gid = a.application_gid and groupcovdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                    " and fromphysical_document = 'N') as 'Query - Response', " +
                    "  group_concat(date_format(c.physical_extendedduedate,'%d-%m-%Y') SEPARATOR ' || ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                    " from ocs_trn_tcadapplication a     " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid  " +
                    " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid " +
                    "  left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                    "   left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" +
                    "  left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                    " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                    " , (SELECT @s:= 0) AS s" +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTPYD' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' " +
                    " group by c.groupcovdocumentchecklist_gid order by a.application_gid ";
            }
            else if (lsstatus == "Checker")
            {
                msSQL = "select a.application_no as 'ARN NO',    " +
                    " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                    " d.cadgroup_name as 'CAD Group Name'," +
                   " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                     "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                    " group_concat( distinct c.mstdocumenttype_name SEPARATOR ' || ' ) as 'Document Type', group_concat(distinct c.mstdocument_name SEPARATOR ' || ') as 'Document Name' ," +
                    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged'  " +
                    " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken'   " +
                    " WHEN(f.deferraltag_status   = '3') THEN 'Approval Pending' " +
                    " ELSE '-' END as 'Deferral Status' ,  " +
                 " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n') " +
                 " from ocs_trn_ttagquery where application_gid = a.application_gid and groupcovdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                  " and fromphysical_document = 'N') as 'Query - Response', " +
                  "  group_concat(date_format(c.physical_extendedduedate,'%d-%m-%Y') SEPARATOR ' || ') as 'Extended Due Date',  d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                  " from ocs_trn_tcadapplication a     " +
                 " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid  " +
                 " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid " +
                 "  left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                 "   left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" + 
                 "  left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                  " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                  " , (SELECT @s:= 0) AS s" +
                  " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTPYD' and d.completed_flag='N' " +
                  " and d.checker_approvalflag = 'N' and d.maker_approvalflag = 'Y' and a.docchecklist_approvalflag = 'Y'  " +
                 " group by c.groupcovdocumentchecklist_gid order by a.application_gid ";
            }
            else
            {
                msSQL = "select a.application_no as 'ARN NO',    " +
                    " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                    " d.cadgroup_name as 'CAD Group Name'," +
                    " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                       "CASE WHEN  b.institution_gid is not null and m.group_gid is null  then  'Institution'" +
                    " WHEN b.institution_gid is null and m.group_gid is not null  then  'Group'" +
                    " ELSE 'Individual' END as 'Customer Type'," +
                    " group_concat( distinct c.mstdocumenttype_name SEPARATOR ' || ' ) as 'Document Type', group_concat(distinct c.mstdocument_name SEPARATOR ' || ') as 'Document Name' ," +
                    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged'  " +
                    " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken'   " +
                    " WHEN(f.deferraltag_status   = '3') THEN 'Approval Pending' " +
                    " ELSE '-' END as 'Deferral Status' ,  " +
                  " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n') " +
                    " from ocs_trn_ttagquery where application_gid = a.application_gid and groupcovdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                    " and fromphysical_document = 'N') as 'Query - Response', " +
                    "  group_concat(date_format(c.physical_extendedduedate,'%d-%m-%Y') SEPARATOR ' || ') as 'Extended Due Date' , d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                    " from ocs_trn_tcadapplication a     " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid  " +
                    " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid " +
                    "  left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                    "   left join ocs_trn_tdeferraltagdoc f on f.application_gid = a.application_gid" +
                    "  left join ocs_trn_tcadcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                     " left join ocs_trn_tcadgroup m on m.application_gid = a.application_gid and c.credit_gid =m.group_gid" +
                    " , (SELECT @s:= 0) AS s" +
                    " where a.process_type = 'Accept' and d.menu_gid = 'CADMGTPYD' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.approver_approvalflag = 'N' and d.checker_approvalflag = 'Y'  " +
                    " group by c.groupcovdocumentchecklist_gid order by a.application_gid ";

            }
          
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("CADPhysicalCovenant-" + lsstatus + "PendingReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objvalues.lsname = "CADPhysicalCovenant-" + lsstatus + "PendingReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADPhysicalCovenant-" + objvalues.lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objvalues.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADPhysicalCovenant-" + objvalues.lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                objvalues.lscloudpath = lscompany_code + "/" + "Master/CADPhysicalCovenant-" + objvalues.lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objvalues.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 72])  //Address "A1:BD1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objvalues.lscloudpath, ms);
                ms.Close();
                objvalues.lscloudpath = objcmnstorage.EncryptData(objvalues.lscloudpath);
                objvalues.lspath = objcmnstorage.EncryptData(objvalues.lspath);
                objvalues.status = true;
                objvalues.message = "Success";
            }
            catch (Exception ex)
            {
                objvalues.status = false;
                objvalues.message = "Failure";
            }
        }
    }
}