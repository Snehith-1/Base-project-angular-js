using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using ems.storage.Functions;
using ems.mastersamagro.Models;
using System.IO;
using OfficeOpenXml;
using System.Configuration;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.NetworkInformation;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access for various Defferal & Convenat Document Reports
    /// </summary>
    /// <remarks>Written by Venkatesh, Premchander.K </remarks>
    public class DaAgrPmgReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDataReader; 
        string msSQL;
        int mnResult;

        public void DaGetScannedDocMakerPendingSummary(rptscannedmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as maker_name, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                    " a.creditgroup_gid, d.cadgroup_name from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join hrm_mst_temployee e on e.employee_gid = d.maker_gid " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid " + 
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.ScannedDocument + "' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<rptscannedmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new rptscannedmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        maker_name = dt["maker_name"].ToString(),
                    });
                }
            }
            values.rptscannedmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }


        public void DaGetScannedDocCheckerPendingSummary(rptscannedmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as checker_name, " +
                    " a.creditgroup_gid, d.cadgroup_name from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join hrm_mst_temployee e on e.employee_gid = d.checker_gid " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.ScannedDocument + "' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'Y' and d.checker_approvalflag = 'N' " +
                    " and d.completed_flag='N' order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<rptscannedmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new rptscannedmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        checker_name = dt["checker_name"].ToString(),
                    });
                }
            }
            values.rptscannedmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetScannedDocApproverPendingSummary(rptscannedmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as approver_name, " +
                    " a.creditgroup_gid, d.cadgroup_name from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join hrm_mst_temployee e on e.employee_gid = d.approver_gid " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.ScannedDocument + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag='Y' and d.checker_approvalflag = 'Y' " +
                    " and d.completed_flag='N' and d.approver_approvalflag='N'" +
                    " order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<rptscannedmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new rptscannedmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        approver_name = dt["approver_name"].ToString(),
                    });
                }
            }
            values.rptscannedmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }


        public void DaGetPhysicalDocMakerPendingSummary(rptphyiscalmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as maker_name, " +
                    " a.creditgroup_gid, d.cadgroup_name from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join hrm_mst_temployee e on e.employee_gid = d.maker_gid " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<rptphyiscalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new rptphyiscalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        maker_name = dt["maker_name"].ToString(),
                    });
                }
            }
            values.rptphyiscalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }


        public void DaGetPhysicalDocCheckerPendingSummary(rptphyiscalmakerapplicationlist values)
        {
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as checker_name, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,d.overall_approvalstatus, " +
                    " a.creditgroup_gid, d.cadgroup_name from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join hrm_mst_temployee e on e.employee_gid = d.checker_gid " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "' and d.completed_flag='N' " +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'Y' and d.checker_approvalflag = 'N' order by a.updated_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<rptphyiscalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new rptphyiscalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        overall_approvalstatus = dt["overall_approvalstatus"].ToString(),
                        checker_name = dt["checker_name"].ToString(),
                    });
                }
            }
            values.rptphyiscalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetPhysicalDocApproverPendingSummary(rptphyiscalmakerapplicationlist values)
        { 
            msSQL = " select a.application_gid,d.processtypeassign_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, " +
                    " a.customer_name as customer_name,a.ccgroup_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as approver_name, " +
                    " a.creditgroup_gid, d.cadgroup_name from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " left join hrm_mst_temployee e on e.employee_gid = d.approver_gid " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                    " where a.process_type = 'Accept' and d.menu_gid = '" + getMenuClass.PhysicalDocument + "'" +
                    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag='Y' and d.checker_approvalflag = 'Y' and d.completed_flag='N' and d.approver_approvalflag='N'" +
                    " order by d.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<rptphyiscalmakerapplication>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new rptphyiscalmakerapplication
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        processtypeassign_gid = dt["processtypeassign_gid"].ToString(),
                        approver_name = dt["approver_name"].ToString(),
                    });
                }
            }
            values.rptphyiscalmakerapplication = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetAppPhysicalDocCount(CadScannedCount values)
        {
            msSQL = " select(select count(*) from agr_trn_tprocesstype_assign a  " +
                    " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                    " where maker_approvalflag = 'N' and b.docchecklist_approvalflag = 'Y'  and completed_flag = 'N' " +
                    " and menu_gid = '" + getMenuClass.PhysicalDocument + "') as MakerPendingCount , " +
                    " (select count(*) from agr_trn_tprocesstype_assign " +
                    " where checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                    " and menu_gid = '" + getMenuClass.PhysicalDocument + "') as CheckerApprovalPendingCount, " +
                    " (select count(*) from agr_trn_tprocesstype_assign " +
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
            msSQL = " select(select count(*) from agr_trn_tprocesstype_assign a  " +
                    " left join agr_mst_tapplication b on a.application_gid = b.application_gid " +
                    " where maker_approvalflag = 'N' and b.docchecklist_approvalflag = 'Y'  and completed_flag = 'N' " +
                    " and menu_gid = '" + getMenuClass.ScannedDocument + "') as MakerPendingCount , " +
                    " (select count(*) from agr_trn_tprocesstype_assign " +
                    " where checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                    " and menu_gid = '" + getMenuClass.ScannedDocument + "') as CheckerApprovalPendingCount, " +
                    " (select count(*) from agr_trn_tprocesstype_assign " +
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


        public void DaGetScannedDefPendingReport(string lsstatus, Mdlscannedapplicationexport objvalues)
        {
            if (lsstatus == "Maker")
            {

                msSQL = "call agr_trn_GetScannedDefMakerPendingReport";

                //msSQL = "select a.application_no as 'ARN NUMBER',  " +
                //    " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                //    " d.cadgroup_name as 'PMG Group Name'," +
                //    " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                //    " CASE WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //    " group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                //    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged' " +
                //    " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken' " +
                //    " ELSE '-' END as 'Deferral Status' ,  " +
                //    " (select GROUP_CONCAT( (CONCAT ( (@s:=@s+1), '.', ( query_description),' - ',IFNULL(query_responseremarks,' Pending'))) SEPARATOR '\r\n')  " +
                //    " from  agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                //    " and fromphysical_document='N') as 'Query - Response'," +
                //    " group_concat(date_format(c.extendeddue_date,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date',  d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name " +
                //    " from agr_mst_tapplication a " +
                //    " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                //    " left join agr_trn_tgroupdocumentchecklist c on c.application_gid = a.application_gid" +
                //    " left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                //    " left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupdocumentchecklist_gid" +
                //    " left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                //    ", (SELECT @s:= 0) AS s" +
                //    " where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTDTS' and d.completed_flag='N' " +
                //    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N'" +
                //    " group by c.groupdocumentchecklist_gid order by a.application_gid ";
            }
            else if (lsstatus == "Checker")
            {

                msSQL = "call agr_trn_GetScannedDefCheckerPendingReport";
                //msSQL = "select a.application_no as 'ARN NUMBER',  " +
                //    " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                //    " d.cadgroup_name as 'PMG Group Name'," +
                //    " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                //    " CASE WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //    " group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                //    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged' " +
                //    " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken' " +
                //    " ELSE '-' END as 'Deferral Status' ,  " +
                //     " (select GROUP_CONCAT( (CONCAT ( (@s:=@s+1), '.', ( query_description),' - ',IFNULL(query_responseremarks,' Pending'))) SEPARATOR '\r\n')  " +
                //    " from  agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                //    " and fromphysical_document='N') as 'Query - Response'," +
                //    " group_concat(date_format(c.extendeddue_date,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                //    " from agr_mst_tapplication a " +
                //    " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                //    " left join agr_trn_tgroupdocumentchecklist c on c.application_gid = a.application_gid" +
                //    " left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                //    " left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupdocumentchecklist_gid" +
                //    " left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                //    ", (SELECT @s:= 0) AS s" +
                //    " where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTDTS' and d.completed_flag='N'" +
                //    " and a.docchecklist_approvalflag = 'Y' and checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                //    " group by c.groupdocumentchecklist_gid order by a.application_gid ";
            }
            else
            {
                msSQL = "call agr_trn_GetScannedDefApproverPendingReport";
                //msSQL = "select a.application_no as 'ARN NUMBER',  " +
                //   " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                //   " d.cadgroup_name as 'PMG Group Name'," +
                //   " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                //   " CASE WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //   " group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                //   " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged' " +
                //   " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken' " +
                //   " ELSE '-' END as 'Deferral Status' ,  " +
                //   " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n')  " +
                //    " from  agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                //    " and fromphysical_document='N') as 'Query - Response',"+
                //    " group_concat(date_format(c.extendeddue_date,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                //   " from agr_mst_tapplication a " +
                //   " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                //   " left join agr_trn_tgroupdocumentchecklist c on c.application_gid = a.application_gid" +
                //   " left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                //   " left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupdocumentchecklist_gid" +
                //   " left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                //   ", (SELECT @s:= 0) AS s" +
                //   " where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTDTS' and d.completed_flag='N' " +
                //   " and a.docchecklist_approvalflag = 'Y' and approver_approvalflag = 'N' and checker_approvalflag = 'Y'  and completed_flag = 'N'  " +
                //   " group by c.groupdocumentchecklist_gid order by a.application_gid ";
            } 
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("PMGScannedDeferral-" + lsstatus + "PendingReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objvalues.lsname = "PMGScannedDeferral-" + lsstatus + "PendingReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGScannedDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objvalues.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGScannedDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                objvalues.lscloudpath = lscompany_code + "/" + "SamAgro/PMGScannedDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objvalues.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 19])  //Address "A1:BD1"

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

        public void DaGetPhysicalDefPendingReport(string lsstatus, Mdlscannedapplicationexport objvalues)
        {
            if (lsstatus == "Maker")
            {

                msSQL = "call agr_trn_GetPhysicalDefMakerPendingReport";
                //msSQL = "select a.application_no as 'ARN NUMBER',    " +
                //    " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                //    " d.cadgroup_name as 'PMG Group Name'," +
                //    " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                //    "  CASE WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //    " group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                //    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged'  " +
                //    " WHEN(f.deferraltag_status   = '1') THEN 'Deferral Taken'   " +
                //    " ELSE '-' END as 'Deferral Status' ,  " +
                //    " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n') " +
                //    " from agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                //    " and fromphysical_document = 'N') as 'Query - Response', " +
                //    "  group_concat(date_format(c.physical_extendedduedate,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                //    " from agr_mst_tapplication a     " +
                //    " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid  " +
                //    " left join agr_trn_tgroupdocumentchecklist c on c.application_gid = a.application_gid " +
                //    "  left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                //    "   left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupdocumentchecklist_gid" +
                //    "  left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                //    " , (SELECT @s:= 0) AS s" +
                //    " where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTPYD' and d.completed_flag='N' " +
                //    " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' " +
                //    " group by c.groupdocumentchecklist_gid order by a.application_gid ";
            }
            else if (lsstatus == "Checker")
            {

                msSQL = "call agr_trn_GetPhysicalDefCheckerPendingReport";
                //                    msSQL = "select a.application_no as 'ARN NUMBER',    " +
                //        " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                //        " d.cadgroup_name as 'PMG Group Name'," +
                //        " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                //        "  CASE WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //        " group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                //        " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged'  " +
                //        " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken'   " +
                //        " ELSE '-' END as 'Deferral Status' ,  " +
                //         " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n') " +
                //        " from agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                //        " and fromphysical_document = 'N') as 'Query - Response', " +
                //        "  group_concat(date_format(c.physical_extendedduedate,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                //        " from agr_mst_tapplication a     " +
                //        " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid  " +
                //        " left join agr_trn_tgroupdocumentchecklist c on c.application_gid = a.application_gid " +
                //        "  left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                //        "   left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupdocumentchecklist_gid" +
                //        "  left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                //        " , (SELECT @s:= 0) AS s" +
                //        " where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTPYD' and d.completed_flag='N' " +
                //        " and d.checker_approvalflag = 'N' and d.maker_approvalflag = 'Y' and a.docchecklist_approvalflag = 'Y'  " +
                //        " group by c.groupdocumentchecklist_gid order by a.application_gid ";
            }
            else
            {
                msSQL = "call agr_trn_GetPhysicalDefApproverPendingReport";
                //msSQL = "select a.application_no as 'ARN NUMBER',    " +
                //    " CASE WHEN  b.company_name is null  then  concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name  END as 'Customer Name'," +
                //    " d.cadgroup_name as 'PMG Group Name'," +
                //    " CASE WHEN  b.stakeholder_type is null  then  j.stakeholder_type   ELSE b.stakeholder_type  END as 'Stakeholder Type'," +
                //    "  CASE WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //    " group_concat( c.mstdocumenttype_name SEPARATOR ' , ' ) as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name' ," +
                //    " CASE WHEN(f.deferraltag_status   = '1') then 'Tagged'  " +
                //    " WHEN(f.deferraltag_status   = '2') THEN 'Deferral Taken'   " +
                //    " ELSE '-' END as 'Deferral Status' ,  " +
                //  " (select GROUP_CONCAT((CONCAT((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n') " +
                //    " from agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupdocumentchecklist_gid " +
                //    " and fromphysical_document = 'N') as 'Query - Response', " +
                //    "  group_concat(date_format(c.physical_extendedduedate,'%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                //    " from agr_mst_tapplication a     " +
                //    " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid  " +
                //    " left join agr_trn_tgroupdocumentchecklist c on c.application_gid = a.application_gid " +
                //    "  left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid =b.institution_gid" +
                //    "   left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupdocumentchecklist_gid" +
                //    "  left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid =j.contact_gid" +
                //    " , (SELECT @s:= 0) AS s" +
                //    " where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTPYD' and d.completed_flag='N' " +
                //    " and a.docchecklist_approvalflag = 'Y' and d.approver_approvalflag = 'N' and d.checker_approvalflag = 'Y'  " +
                //    " group by c.groupdocumentchecklist_gid order by a.application_gid ";

            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("PMGPhysicalDeferral-" + lsstatus + "PendingReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objvalues.lsname = "PMGPhysicalDeferral-" + lsstatus + "PendingReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGPhysicalDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objvalues.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGPhysicalDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                objvalues.lscloudpath = lscompany_code + "/" + "SamAgro/PMGPhysicalDeferral-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objvalues.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 19])  //Address "A1:BD1"

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

        public void DaGetScannedCovPendingReport(string lsstatus, Mdlscannedapplicationexport objvalues)
        {
            if (lsstatus == "Maker")
            {


                msSQL = "call agr_trn_GetScannedCovMakerPendingReport";
                //msSQL = " select a.application_no as 'ARN NUMBER'," +
                //        " case" +
                //        " WHEN  b.company_name is null  then concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name END as 'Customer Name', d.cadgroup_name as 'PMG Group Name'," +
                //        " case" +
                //        " WHEN  b.stakeholder_type is null  then j.stakeholder_type ELSE b.stakeholder_type END as 'Stakeholder Type'," +
                //        " case" +
                //        " WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //        " group_concat(c.mstdocumenttype_name SEPARATOR ' , ') as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name'," +
                //        " case" +
                //        " WHEN(f.deferraltag_status   = '1') then 'Tagged'" +
                //        " WHEN(f.deferraltag_status = '2') THEN 'Deferral Taken'" +
                //        " else '-' ''" +
                //        " end" +
                //        " as 'Deferral Status'," +
                //        " (select GROUP_CONCAT((Concat((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n')" +
                //        " from agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid and fromphysical_document='N') as 'Query - Response'," +
                //        " group_concat(date_format(c.extendeddue_date, '%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date' , d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name " +
                //        " from agr_mst_tapplication a" +
                //        " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                //        " left join agr_trn_tgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid " +
                //        " left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid = b.institution_gid " +
                //        " left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid" +
                //        " left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid = j.contact_gid " +
                //        " left join agr_trn_ttagquery g on g.application_gid = a.application_gid ," +
                //        " (SELECT @s:= 0) AS s " +
                //        " where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTDTS' and d.completed_flag = 'N' " +
                //        //" where checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                //        " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' " +

                //        " group by c.groupcovdocumentchecklist_gid order by a.application_gid";


            }
            else if (lsstatus == "Checker")
            {
                msSQL = "call agr_trn_GetScannedCovCheckerPendingReport";

                //msSQL = " select a.application_no as 'ARN NUMBER'," +
                //        " case" +
                //        " WHEN  b.company_name is null  then concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name END as 'Customer Name', d.cadgroup_name as 'PMG Group Name'," +
                //        " case" +
                //        " WHEN  b.stakeholder_type is null  then j.stakeholder_type ELSE b.stakeholder_type END as 'Stakeholder Type'," +
                //        " case" +
                //        " WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //        " group_concat(c.mstdocumenttype_name SEPARATOR ' || ') as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' || ') as 'Document Name'," +
                //        " case" +
                //        " WHEN(f.deferraltag_status   = '1') then 'Tagged'" +
                //        " WHEN(f.deferraltag_status = '2') THEN 'Deferral Taken'" +
                //        " else '-' ''" +
                //        " end" +
                //        " as 'Deferral Status'," +
                //        " (select GROUP_CONCAT((Concat((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n')" +
                //        " from agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid and fromphysical_document='N') as 'Query - Response'," +
                //        " group_concat(date_format(c.extendeddue_date, '%d-%m-%Y') SEPARATOR ' || ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name" +
                //        " from agr_mst_tapplication a" +
                //        " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                //        " left join agr_trn_tgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid " +
                //        " left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid = b.institution_gid " +
                //        " left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                //        " left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid = j.contact_gid " +
                //        " left join agr_trn_ttagquery g on g.application_gid = a.application_gid ," +
                //        " (SELECT @s:= 0) AS s " +
                //" where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTDTS' and d.completed_flag='N'" +
                //   " and a.docchecklist_approvalflag = 'Y' and checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                //   " group by c.groupcovdocumentchecklist_gid order by a.application_gid";
            }
            else
            {

                msSQL = "call agr_trn_GetScannedCovApproverPendingReport";
                //msSQL = " select a.application_no as 'ARN NUMBER'," +
                //        " case" +
                //        " WHEN  b.company_name is null  then concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name END as 'Customer Name', d.cadgroup_name as 'PMG Group Name'," +
                //        " case" +
                //        " WHEN  b.stakeholder_type is null  then j.stakeholder_type ELSE b.stakeholder_type END as 'Stakeholder Type'," +
                //        " case" +
                //        " WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //        " group_concat(c.mstdocumenttype_name SEPARATOR ' || ') as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' || ') as 'Document Name'," +
                //        " case" +
                //        " WHEN(f.deferraltag_status   = '1') then 'Tagged'" +
                //        " WHEN(f.deferraltag_status = '2') THEN 'Deferral Taken'" +
                //        " else '-' ''" +
                //        " end" +
                //        " as 'Deferral Status'," +

                //        " (select GROUP_CONCAT((Concat((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks, ' Pending'))) SEPARATOR '\r\n')" +
                //        " from agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid and fromphysical_document='N') as 'Query - Response'," +
                //        " group_concat(date_format(c.extendeddue_date, '%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date',  d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name " +
                //        " from agr_mst_tapplication a" +
                //        " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                //        " left join agr_trn_tgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid " +
                //        " left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid = b.institution_gid " +
                //        " left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                //        " left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid = j.contact_gid " +
                //        " left join agr_trn_ttagquery g on g.application_gid = a.application_gid ," +
                //        " (SELECT @s:= 0) AS s " +
                //" where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTDTS' and d.completed_flag='N' " +
                //  " and a.docchecklist_approvalflag = 'Y' and approver_approvalflag = 'N' and checker_approvalflag = 'Y'  and completed_flag = 'N'  " +
                //  " group by c.groupcovdocumentchecklist_gid order by a.application_gid ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("PMGScannedCovenant-" + lsstatus + "PendingReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objvalues.lsname = "PMGScannedCovenant-" + lsstatus + "PendingReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGScannedCovenant-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objvalues.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGScannedCovenant-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                objvalues.lscloudpath = lscompany_code + "/" + "SamAgro/PMGScannedCovenant-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objvalues.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 19])  //Address "A1:BD1"

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

        public void DaGetPhysicalCovPendingReport(string lsstatus, Mdlscannedapplicationexport objvalues)
        {
            if (lsstatus == "Maker")
            {

                msSQL = "call agr_trn_GetPhysicalCovMakerPendingReport";
                //msSQL = " select a.application_no as 'ARN NO'," +
                //        " case" +
                //        " WHEN  b.company_name is null  then concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name END as 'Customer Name', d.cadgroup_name as 'PMG Group Name'," +
                //        " case" +
                //        " WHEN  b.stakeholder_type is null  then j.stakeholder_type ELSE b.stakeholder_type END as 'Stakeholder Type'," +
                //        " case" +
                //        " WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //        " group_concat(c.mstdocumenttype_name SEPARATOR ' || ') as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' || ') as 'Document Name'," +
                //        " case" +
                //        " WHEN(f.deferraltag_status   = '1') then 'Tagged'" +
                //        " WHEN(f.deferraltag_status = '2') THEN 'Deferral Taken'" +
                //        " else '-' ''" +
                //        " end" +
                //        " as 'Deferral Status'," +
                //        " (select GROUP_CONCAT((Concat((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks,' Pending'))) SEPARATOR '\n')" +
                //        " from agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid  and fromphysical_document='Y') as 'Query - Response'," +
                //        " group_concat(date_format(c.physical_extendedduedate, '%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name " +
                //        " from agr_mst_tapplication a" +
                //        " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                //        " left join agr_trn_tgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid " +
                //        " left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid = b.institution_gid " +
                //        " left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                //        " left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid = j.contact_gid " +
                //        " left join agr_trn_ttagquery g on g.application_gid = a.application_gid ," +
                //        " (SELECT @s:= 0) AS s " +
                //        " where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTPYD' and d.completed_flag = 'N' " +
                //        " and a.docchecklist_approvalflag = 'Y' and d.maker_approvalflag = 'N' " +

                //        " group by c.groupcovdocumentchecklist_gid order by a.application_gid";
            }
            else if (lsstatus == "Checker")
            {

                msSQL = "call agr_trn_GetPhysicalCovCheckerPendingReport";
                //msSQL = " select a.application_no as 'ARN NO'," +
                //        " case" +
                //        " WHEN  b.company_name is null  then concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name END as 'Customer Name', d.cadgroup_name as 'PMG Group Name'," +
                //        " case" +
                //        " WHEN  b.stakeholder_type is null  then j.stakeholder_type ELSE b.stakeholder_type END as 'Stakeholder Type'," +
                //        " case" +
                //        " WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //        " group_concat(c.mstdocumenttype_name SEPARATOR ' , ') as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name'," +
                //        " case" +
                //        " WHEN(f.deferraltag_status   = '1') then 'Tagged'" +
                //        " WHEN(f.deferraltag_status = '2') THEN 'Deferral Taken'" +
                //        " else '-' ''" +
                //        " end" +
                //        " as 'Deferral Status'," +
                //        " (select GROUP_CONCAT((Concat((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks,' Pending'))) SEPARATOR '\n')" +
                //        " from agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid and fromphysical_document='Y') as 'Query - Response'," +
                //        " group_concat(date_format(c.physical_extendedduedate, '%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name " +
                //        " from agr_mst_tapplication a" +
                //        " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                //        " left join agr_trn_tgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid " +
                //        " left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid = b.institution_gid " +
                //        " left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                //        " left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid = j.contact_gid " +
                //        " left join agr_trn_ttagquery g on g.application_gid = a.application_gid ," +
                //        " (SELECT @s:= 0) AS s " +

                //" where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTPYD' and d.completed_flag='N'" +
                //  " and a.docchecklist_approvalflag = 'Y' and checker_approvalflag = 'N' and maker_approvalflag = 'Y' and completed_flag = 'N' " +
                //  " group by c.groupcovdocumentchecklist_gid order by a.application_gid ";
            }
            else
            {
                msSQL = "call agr_trn_GetPhysicalCovApproverPendingReport";

                //msSQL = " select a.application_no as 'ARN NO'," +
                //        " case" +
                //        " WHEN  b.company_name is null  then concat_ws(' ', j.first_name, j.last_name, j.middle_name)   ELSE b.company_name END as 'Customer Name', d.cadgroup_name as 'PMG Group Name'," +
                //        " case" +
                //        " WHEN  b.stakeholder_type is null  then j.stakeholder_type ELSE b.stakeholder_type END as 'Stakeholder Type'," +
                //        " case" +
                //        " WHEN  b.institution_gid is not null then  'Institution' ELSE 'Individual' END as 'Customer Type'," +
                //        " group_concat(c.mstdocumenttype_name SEPARATOR ' , ') as 'Document Code', group_concat(c.mstdocument_name SEPARATOR ' , ') as 'Document Name'," +
                //        " case" +
                //        " WHEN(f.deferraltag_status   = '1') then 'Tagged'" +
                //        " WHEN(f.deferraltag_status = '2') THEN 'Deferral Taken'" +
                //        " else '-' ''" +
                //        " end" +
                //        " as 'Deferral Status'," +
                //        " (select GROUP_CONCAT((Concat((@s:= @s + 1), '.', (query_description), ' - ', IFNULL(query_responseremarks,' Pending'))) SEPARATOR '\n')" +
                //        " from agr_trn_ttagquery where application_gid = a.application_gid and groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid and fromphysical_document='Y') as 'Query - Response'," +
                //        " group_concat(date_format(c.physical_extendedduedate, '%d-%m-%Y') SEPARATOR ' , ') as 'Extended Due Date', d.maker_name as Maker_Name,  d.checker_name as Checker_Name, d.approver_name as Approver_Name " +
                //        " from agr_mst_tapplication a" +
                //        " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                //        " left join agr_trn_tgroupcovenantdocumentchecklist c on c.application_gid = a.application_gid " +
                //        " left join agr_mst_tinstitution b on b.application_gid = a.application_gid and c.credit_gid = b.institution_gid " +
                //        " left join agr_trn_tdeferraltagdoc f on f.application_gid = a.application_gid and f.groupdocumentchecklist_gid = c.groupcovdocumentchecklist_gid " +
                //        " left join agr_mst_tcontact j on j.application_gid = a.application_gid and c.credit_gid = j.contact_gid " +
                //        " left join agr_trn_ttagquery g on g.application_gid = a.application_gid ," +
                //        " (SELECT @s:= 0) AS s " +

                //" where a.process_type = 'Accept' and d.menu_gid = 'AGDMGTPYD' and d.completed_flag='N' " +
                //  " and a.docchecklist_approvalflag = 'Y' and approver_approvalflag = 'N' and checker_approvalflag = 'Y'  and completed_flag = 'N'  " +
                //  " group by c.groupcovdocumentchecklist_gid order by a.application_gid ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("PMGPhysicalCovenant-" + lsstatus + "PendingReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objvalues.lsname = "PMGPhysicalCovenant-" + lsstatus + "PendingReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGPhysicalCovenant-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objvalues.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGPhysicalCovenant-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                objvalues.lscloudpath = lscompany_code + "/" + "SamAgro/PMGPhysicalCovenant-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objvalues.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 19])  //Address "A1:BD1"

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


        public void DaGetSuprDocChecklistReport(string lsstatus, MdlAgrSuprDocChecklistReport objvalues)
        {
            if (lsstatus == "Maker")
            {
                msSQL = "call Agr_Supr_DocChecklistMakerPendingReport";
            }
            else if (lsstatus == "Checker")
            {
                msSQL = "call Agr_Supr_DocChecklistCheckerPendingReport";
            }
            else
            {
                msSQL = "call Agr_Supr_DocChecklistApprovalPendingReport";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("SupplierPMGDocumentChecklist-" + lsstatus + "PendingReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objvalues.lsname = "SupplierPMGDocumentChecklist-" + lsstatus + "PendingReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/SupplierPMGDocumentChecklist-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objvalues.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/SupplierPMGDocumentChecklist-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                objvalues.lscloudpath = lscompany_code + "/" + "SamAgro/SupplierPMGDocumentChecklist-" + lsstatus + "PendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objvalues.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objvalues.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 12])  //Address "A1:BD1"

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


        public void DaGetSuprDocChecklistReportSummary(string employee_gid, MdlAgrSuprDocChecklistReport values)
        {
            msSQL = "call Agr_Supr_DocChecklistMakerSummary";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentpendinglist = new List<suprdocumentpendinglist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getdocumentpendinglist.Add(new suprdocumentpendinglist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccadmin_name = dt["ccadmin_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        customer_urn = dt["customer_urn"].ToString()
                    });

                }
            }
            values.suprdocumentpendinglist = getdocumentpendinglist;
            dt_datatable.Dispose();
        }


        public void DaGetSuprDocChecklistReportCheckerSummary(string employee_gid, MdlAgrSuprDocChecklistReport values)
        {
            msSQL = "call Agr_Supr_DocChecklistCheckerSummary";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentpendinglist = new List<suprdocumentpendinglist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    
                    getdocumentpendinglist.Add(new suprdocumentpendinglist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccadmin_name = dt["ccadmin_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        customer_urn = dt["customer_urn"].ToString()
                    });

                }
            }
            values.suprdocumentpendinglist = getdocumentpendinglist;
            dt_datatable.Dispose();
        }

        public void DaGetSuprDocChecklistReportApprovalSummary(string employee_gid, MdlAgrSuprDocChecklistReport values)
        {
            msSQL = "call Agr_Supr_DocChecklistApprovalSummary";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentpendinglist = new List<suprdocumentpendinglist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentpendinglist.Add(new suprdocumentpendinglist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = dt["ccgroup_name"].ToString(),
                        ccadmin_name = dt["ccadmin_name"].ToString(),
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        customer_urn = dt["customer_urn"].ToString()
                    });

                }
            }
            values.suprdocumentpendinglist = getdocumentpendinglist;
            dt_datatable.Dispose();
        }


        public void DaGetSuprDocChecklistPendingCount(string user_gid, string employee_gid, SuprDocumentCount values)
        {
            msSQL = " select count(distinct (a.application_gid))  as cadsanction_count from agr_mst_tsuprapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join agr_trn_tsuprprocesstype_assign d on d.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tsuprprocesstype_assign where menu_gid = 'AGDSMPSDC' " +
                     "  and maker_approvalflag = 'N')" +
                     "  order by a.updated_date desc ";
            values.cadmaker_count = objdbconn.GetExecuteScalar(msSQL);



            msSQL = " select count(distinct (a.application_gid))  as cadchecker_count from agr_mst_tsuprapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join agr_trn_tsuprapplication2docchecklist d on d.application_gid = a.application_gid " +
                    " left join agr_trn_tsuprprocesstype_assign e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.maker_flag='Y' and d.checker_flag='N' and " +
                    " a.application_gid in (Select application_gid from agr_trn_tsuprprocesstype_assign where menu_gid = 'AGDSMPSDC' " +
                    " and checker_approvalflag = 'N')" +
                    "  order by a.updated_date desc ";
            values.cadchecker_count = objdbconn.GetExecuteScalar(msSQL);



            msSQL = " select count(distinct (a.application_gid))  as cadcheckerapproval_count from agr_mst_tsuprapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join agr_trn_tsuprapplication2docchecklist d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " left join agr_trn_tsuprprocesstype_assign g on g.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tsuprprocesstype_assign where menu_gid = 'AGDSMPSDC' and checker_flag='Y' " +
                     " and approver_approvalflag = 'N')" +
                     "  order by a.updated_date desc ";
            values.cadcheckerapproval_count = objdbconn.GetExecuteScalar(msSQL);


        }

    }
}