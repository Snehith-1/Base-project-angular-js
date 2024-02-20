using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.ecms.Models;
using ems.utilities.Functions;
using System.IO;
using System.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using ems.storage.Functions;


namespace ems.ecms.DataAccess
{
    /// <summary>
    /// Deferral Controller Class containing API methods for accessing the  DataAccess class DaDeferral 
    /// Create defferal, show deferral records, set deferral to loan, show rm details in a table, usercode of employee, export excel of npa, export excel for defferal,
    /// 
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaDeferral
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        string body;
        int def_count = 0;
        int check_count = 0;
        DataTable dt_datatable;
        DaPenalityAlert objdaPenalityAlert = new DaPenalityAlert();
        DataSet ds;
        string msSQL, mspsSQL,mspSQL, msGetGid, msGetGidchecker, msGetGidREF, gid, gidapp, gidhis, lsvertical_gid, lssanction, lssanctionref, lssanctiondate;
        int mnResult, mailflag, mnResult1;
        HttpPostedFile httpPostedFile;
        string lspath, loan_gid, loanref_no, sanction_date, sanction_refno, lssanctionreference;
        string lscustomeralert_gid, lsalert_totalcount, lspenality_deferralcount;
        string lspop_mail, lspop_password;
        string lsrecord_id, lstomail_id, lschecked_by, lscustomer_name;
        string strRes = string.Empty;
        string created_by = string.Empty;
        string lsregional_head, lsregionalhead_name;

        public void DaGetExport(deferralSummary objrmDeferral, string employee_gid)
        {
            msSQL = " SELECT a.entity_name as 'Entity',n.state as 'State',SUBSTRING_INDEX(a.branch_name, '/', -1) as 'Branch',n.customer_urn as 'URN',a.customer_name as 'Customer', " +
                    " a.vertical_code as 'Vertical',a.loanref_no as 'Loan Ref No',a.sanction_refno as 'Sanction Refno',a.sanction_date as 'Sanction Date', " +
                    " a.record_id as 'Record ID', a.tracking_type as 'Tracking Type'," +
                    " a.deferral_catagory as 'Deferral Catagory'," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as 'Deferral Type',a.criticallity as 'Criticality',  " +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as 'Due Date'," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as 'Extended Date', a.extend_type as Extend_Type," +
                    " case when m.approval_status='ReOpen' then 'Pending' when m.approval_status='Extend' then 'Pending' when m.approval_status='Re-track' then 'Pending' else  m.approval_status end as 'Deferral Stage', " +
                    " case when m.approved_by is null then 'N/A' else concat(z.user_code,'/',z.user_firstname,z.user_lastname) end as 'Stages Updated By',case when m.approval_date is null then 'N/A' else date_format(m.approval_date, '%d-%m-%Y %h:%i:%s %p') end as 'Stages Updated Date'," +
                    " a.customer_remarks as 'Customer Remarks'," +
                    " case when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as 'Internal Remarks'," +
                    " case when m.approval_status='Closed' then '-' else a.aging end as 'Aging'," +
                    " case when m.approval_status='Closed' then 'Closed' when m.approval_status='Waived' then 'Waived' else a.deferral_status end as 'Deferral Status'," +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as  'Extension Count', " +
                    " concat(o.user_code,'/',o.user_firstname,o.user_lastname) as 'Created By' ," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'Created Date', " +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as 'Relationship Manager', " +
                    " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as 'Cluster Manager', " +
                    " case when a.regionalhead_gid = '' then 'NA' else a.regionalhead_name end as 'Regional Head', " +
                    " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as 'Zonal Head'," +
                    " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as 'Business Head', " +
                    " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as 'Credit Manager', " +
                    " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as 'Risk Manager', " +
                    " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as 'Zonal Risk Manager', " +
                    " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as 'Head Risk Monitoring', " +
                    " case when n.legaltag_flag='Y' then concat(mn.customer_status,' ','by',' ',concat(cv.user_code,'/',cv.user_firstname,' ',cv.user_lastname),' ','on',' ',date_format(mn.created_date,'%d-%m-%Y %h:%i:%s %p')) " +
                    " when n.legaltag_flag='N' then concat(mn.customer_status,' ','by',' ',concat(cv.user_code,' / ',cv.user_firstname,' ',cv.user_lastname),' ','on',' ',date_format(mn.created_date,'%d-%m-%Y %h:%i:%s %p')) " +
                    " else 'NA' end as  'Legal Status'," +
                    " case when n.legaltag_flag='Y' then n.tag_remarks when n.legaltag_flag='N' then n.untag_remarks else '-' end as 'Legal Remarks'," +
                    " concat(l.user_code,'/',l.user_firstname,l.user_lastname) as 'Deferral Updated By' ," +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i:%s %p') as 'Deferral Updated Date,', " +
                    " case when m.checker_status='Approved' then concat(zmw.user_code,'/',zmw.user_firstname,zmw.user_lastname) else '-' end as 'Checker Approved By'," +
                    " case when m.checker_status='Approved' then date_format(m.checked_date, '%d-%m-%Y %h:%i:%s %p') else '-' end as 'Checker Approved Date'," +
                    " concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as 'NPA Tagged By',date_format(n.npatagged_date,'%d-%m-%Y %h:%i:%s %p')  as 'NPA Tagged Date', n.npatag_remarks  as 'NPA Tagged Remarks'," +
                    " case when n.npatag_flag = 'Y' then 'Tagged' when n.npatag_flag = 'N' then 'UnTagged' else '' end as 'NPA Tag Status'," +
                    " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as 'NPA UnTagged By',date_format(n.npauntagged_date,'%d-%m-%Y %h:%i:%s %p')  as 'NPA UnTagged Date', n.npauntag_remarks  as 'NPA UnTagged Remarks'" +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left join ocs_trn_tcustomer2legalhistory mn on n.customer2legalhistory_gid=mn.customer2legalhistory_gid " +
                      " left join hrm_mst_temployee nb on mn.created_by = nb.employee_gid" +
                        " left join adm_mst_tuser cv on cv.user_gid = nb.user_gid " +
                    " left join hrm_mst_temployee k on a.updated_by = k.employee_gid" +
                    " left join adm_mst_tuser l on l.user_gid = k.user_gid" +
                     " left join hrm_mst_temployee w on w.employee_gid = m.approved_by " +
                      " left join hrm_mst_temployee wmz on wmz.employee_gid = m.checked_by " +
                      " left join adm_mst_tuser zmw on zmw.user_gid = wmz.user_gid" +
                    " left join adm_mst_tuser z on z.user_gid = w.user_gid" +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser o on o.user_gid = p.user_gid " +
                     " left join hrm_mst_temployee r on n.npatagged_by = r.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = r.user_gid " +
                    " left join hrm_mst_temployee e on n.npauntagged_by = e.employee_gid" +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid where";
            if (objrmDeferral.vertical_gid == null || objrmDeferral.vertical_gid == "")
            {
                msSQL += "  1=1 ";
            }
            else
            {

                msSQL += "   a.vertical_gid = '" + objrmDeferral.vertical_gid + "'";
            }
            if (objrmDeferral.branch_gid == null || objrmDeferral.branch_gid == "")
            {
                msSQL += " and  1=1 ";
            }
            else
            {

                msSQL += " and  a.branch_gid = '" + objrmDeferral.branch_gid + "'";
            }
            if (objrmDeferral.relationshipMgmt == null || objrmDeferral.relationshipMgmt == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.relationshipmgmt_gid = '" + objrmDeferral.relationshipMgmt + "'";
            }
            if (objrmDeferral.zonalHead == null || objrmDeferral.zonalHead == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.zonal_gid = '" + objrmDeferral.zonalHead + "'";
            }
            if (objrmDeferral.businessHead == null || objrmDeferral.businessHead == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.businesshead_gid = '" + objrmDeferral.businessHead + "'";
            }
            if (objrmDeferral.clustermanager == null || objrmDeferral.clustermanager == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.cluster_manager_gid = '" + objrmDeferral.clustermanager + "'";
            }
            if (objrmDeferral.creditmanager == null || objrmDeferral.creditmanager == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.creditmanager_gid = '" + objrmDeferral.creditmanager + "'";
            }


            if (objrmDeferral.entity_gid == null || objrmDeferral.entity_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.entity_gid = '" + objrmDeferral.entity_gid + "'";
            }

            if (objrmDeferral.customer_gid == null || objrmDeferral.customer_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.customer_gid = '" + objrmDeferral.customer_gid + "'";
            }
            if (objrmDeferral.state_gid == null || objrmDeferral.state_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and n.state_gid = '" + objrmDeferral.state_gid + "'";
            }


            msSQL += " and (a.checker_status='Approved' or a.checker_status='N/A' or  a.checker_status='Closed') order by a.deferral_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Deferral List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objrmDeferral.lsname = "Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/DeferralReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objrmDeferral.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/DeferralReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objrmDeferral.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 49])  //Address "A1:AW1"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                objrmDeferral.lspath = lscompany_code + "/" + "ECMS/DeferralDoc/DeferralReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                status = objcmnstorage.UploadStream("erpdocument", objrmDeferral.lspath, ms);
                ms.Close();

                dt_datatable.Dispose();
            }
           
            catch (Exception ex)
            {
                objrmDeferral.status = false;
                objrmDeferral.message = "Failure";
            }
            objrmDeferral.lspath = objcmnstorage.EncryptData(objrmDeferral.lspath);
            objrmDeferral.status = true;
            objrmDeferral.message = "Success";
        }
        // NPA Tagged Export
        public void DaGetNPAExport(deferralSummary objrmDeferral, string employee_gid)
        {
            msSQL = " SELECT a.entity_name as 'Entity',n.state as 'State',SUBSTRING_INDEX(a.branch_name, '/', -1) as 'Branch',n.customer_urn as 'URN',a.customer_name as 'Customer', " +
                    " a.vertical_code as 'Vertical',a.loanref_no as 'Loan Ref No',a.sanction_refno as 'Sanction Refno',a.sanction_date as 'Sanction Date', " +
                    " a.record_id as 'Record ID', a.tracking_type as 'Tracking Type'," +
                    " a.deferral_catagory as 'Deferral Catagory'," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as 'Deferral Type',a.criticallity as 'Criticality',  " +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as 'Due Date'," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as 'Extended Date', a.extend_type as Extend_Type," +
                    " case when m.approval_status='ReOpen' then 'Pending' when m.approval_status='Extend' then 'Pending' when m.approval_status='Re-track' then 'Pending' else  m.approval_status end as 'Deferral Stage', " +
                    " case when m.approved_by is null then 'N/A' else concat(z.user_code,'/',z.user_firstname,z.user_lastname) end as 'Stages Updated By',case when m.approval_date is null then 'N/A' else date_format(m.approval_date, '%d-%m-%Y %h:%i:%s %p') end as 'Stages Updated Date'," +
                    " a.customer_remarks as 'Customer Remarks'," +
                    " case when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as 'Internal Remarks'," +
                    " case when m.approval_status='Closed' then '-' else a.aging end as 'Aging'," +
                    " case when m.approval_status='Closed' then 'Closed' when m.approval_status='Waived' then 'Waived' else a.deferral_status end as 'Deferral Status'," +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as  'Extension Count', " +
                    " concat(o.user_code,'/',o.user_firstname,o.user_lastname) as 'Created By' ," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'Created Date', " +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as 'Relationship Manager', " +
                    " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as 'Cluster Manager', " +
                    " case when a.regionalhead_gid = '' then 'NA' else a.regionalhead_name end as 'Regional Head', " +
                    " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as 'Zonal Head'," +
                    " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as 'Business Head', " +
                    " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as 'Credit Manager', " +
                    " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as 'Risk Manager', " +
                    " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as 'Zonal Risk Manager', " +
                    " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as 'Head Risk Monitoring', " +
                    " case when n.legaltag_flag='Y' then concat(mn.customer_status,' ','by',' ',concat(cv.user_code,'/',cv.user_firstname,' ',cv.user_lastname),' ','on',' ',date_format(mn.created_date,'%d-%m-%Y %h:%i:%s %p')) " +
                    " when n.legaltag_flag='N' then concat(mn.customer_status,' ','by',' ',concat(cv.user_code,' / ',cv.user_firstname,' ',cv.user_lastname),' ','on',' ',date_format(mn.created_date,'%d-%m-%Y %h:%i:%s %p')) " +
                    " else 'NA' end as  'Legal Status'," +
                    " case when n.legaltag_flag='Y' then n.tag_remarks when n.legaltag_flag='N' then n.untag_remarks else '-' end as 'Legal Remarks'," +
                    " concat(l.user_code,'/',l.user_firstname,l.user_lastname) as 'Deferral Updated By' ," +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i:%s %p') as 'Deferral Updated Date,', " +
                    " case when m.checker_status='Approved' then concat(zmw.user_code,'/',zmw.user_firstname,zmw.user_lastname) else '-' end as 'Checker Approved By'," +
                    " case when m.checker_status='Approved' then date_format(m.checked_date, '%d-%m-%Y %h:%i:%s %p') else '-' end as 'Checker Approved Date'," +
                    " concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as 'NPA Tagged By',date_format(n.npatagged_date,'%d-%m-%Y %h:%i:%s %p')  as 'NPA Tagged Date', n.npatag_remarks  as 'NPA Tagged Remarks' from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left join ocs_trn_tcustomer2legalhistory mn on n.customer2legalhistory_gid=mn.customer2legalhistory_gid " +
                      " left join hrm_mst_temployee nb on mn.created_by = nb.employee_gid" +
                        " left join adm_mst_tuser cv on cv.user_gid = nb.user_gid " +
                    " left join hrm_mst_temployee k on a.updated_by = k.employee_gid" +
                    " left join adm_mst_tuser l on l.user_gid = k.user_gid" +
                     " left join hrm_mst_temployee w on w.employee_gid = m.approved_by " +
                      " left join hrm_mst_temployee wmz on wmz.employee_gid = m.checked_by " +
                      " left join adm_mst_tuser zmw on zmw.user_gid = wmz.user_gid" +
                    " left join adm_mst_tuser z on z.user_gid = w.user_gid" +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser o on o.user_gid = p.user_gid " +
                     " left join hrm_mst_temployee r on n.npatagged_by = r.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = r.user_gid where";
            if (objrmDeferral.vertical_gid == null || objrmDeferral.vertical_gid == "")
            {
                msSQL += "  1=1 ";
            }
            else
            {

                msSQL += "   a.vertical_gid = '" + objrmDeferral.vertical_gid + "'";
            }
            if (objrmDeferral.branch_gid == null || objrmDeferral.branch_gid == "")
            {
                msSQL += " and  1=1 ";
            }
            else
            {

                msSQL += " and  a.branch_gid = '" + objrmDeferral.branch_gid + "'";
            }
            if (objrmDeferral.relationshipMgmt == null || objrmDeferral.relationshipMgmt == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.relationshipmgmt_gid = '" + objrmDeferral.relationshipMgmt + "'";
            }
            if (objrmDeferral.zonalHead == null || objrmDeferral.zonalHead == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.zonal_gid = '" + objrmDeferral.zonalHead + "'";
            }
            if (objrmDeferral.businessHead == null || objrmDeferral.businessHead == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.businesshead_gid = '" + objrmDeferral.businessHead + "'";
            }
            if (objrmDeferral.clustermanager == null || objrmDeferral.clustermanager == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.cluster_manager_gid = '" + objrmDeferral.clustermanager + "'";
            }
            if (objrmDeferral.creditmanager == null || objrmDeferral.creditmanager == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.creditmanager_gid = '" + objrmDeferral.creditmanager + "'";
            }


            if (objrmDeferral.entity_gid == null || objrmDeferral.entity_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.entity_gid = '" + objrmDeferral.entity_gid + "'";
            }

            if (objrmDeferral.customer_gid == null || objrmDeferral.customer_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {

                msSQL += " and a.customer_gid = '" + objrmDeferral.customer_gid + "'";
            }
            if (objrmDeferral.state_gid == null || objrmDeferral.state_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and n.state_gid = '" + objrmDeferral.state_gid + "'";
            }


            msSQL += " and (a.checker_status='Approved' or a.checker_status='N/A' or  a.checker_status='Closed') and n.npatag_flag= 'Y' order by a.deferral_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Deferral List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objrmDeferral.lsname = "Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/DeferralReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objrmDeferral.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/DeferralReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objrmDeferral.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 45])  //Address "A1:A18"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);

                dt_datatable.Dispose();
                bool status;
                objrmDeferral.lspath =lscompany_code + "/" + "ECMS/DeferralDoc/DeferralReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                status = objcmnstorage.UploadStream("erpdocument", objrmDeferral.lspath, ms);
                ms.Close();

            }

            catch (Exception ex)
            {
                objrmDeferral.status = false;
                objrmDeferral.message = "Failure";
            }
            objrmDeferral.lspath = objcmnstorage.EncryptData(objrmDeferral.lspath);
            objrmDeferral.status = true;
            objrmDeferral.message = "Success";
        }
        // cad report excel
        public void DaGetExcel(deferralSummary objrmDeferral, string employee_gid)
        {
            msSQL = " SELECT a.entity_name as 'Entity',n.state as 'State',SUBSTRING_INDEX(a.branch_name, '/', -1) as 'Branch',a.customer_name as 'Customer', " +
                    " a.vertical_code as 'Vertical' ,a.loanref_no as 'Loan Ref No', a.sanction_refno as 'Sanction Refno',a.sanction_date as 'Sanction Date', " +
                    " a.record_id as 'Record ID', a.tracking_type as 'Tracking Type'," +
                    " a.deferral_catagory as 'Deferral Catagory'," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as 'Deferral Type',a.criticallity as 'Criticality',  " +
                 " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as 'Due Date'," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as 'Extended Date'," +
                      " case when m.approval_status='ReOpen' then 'Pending' when m.approval_status='Extend' then 'Pending' when m.approval_status='Re-track' then 'Pending' else  m.approval_status end as 'Deferral Stage', " +
                    " case when m.approval_status='Closed' then 'Closed' when m.approval_status='Waived' then 'Waived' else a.deferral_status end as 'Deferral Status'," +
                     " case when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as 'Internal Remarks'," +
                      " case when m.approval_status='Closed' then '-' else a.aging end as 'Aging'," +
                    " z.employee_emailid as 'Relationship Manager'," +
                      " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as 'Risk Manager', " +
                    " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as 'Zonal Risk Manager', " +
                    " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as 'Head Risk Monitoring', " +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as  'Extension Count' ," +
                    " concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as 'NPA Tagged By',date_format(n.npatagged_date,'%d-%m-%Y %h:%i:%s %p')  as 'NPA Tagged Date', n.npatag_remarks  as 'NPA Tagged Remarks' " +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left" +
                    " join hrm_mst_temployee z on a.relationshipmgmt_gid = z.employee_gid " +
                    " left join hrm_mst_temployee r on n.npatagged_by = r.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = r.user_gid " +
                    " where (m.approval_status='Extend' or m.approval_status='Pending' or  m.approval_status='ReOpen' or m.approval_status = 'Re-track') " +
                    " and a.deferral_catagory not in('CAD Internal','CMD Internal')  and";

            if (objrmDeferral.vertical_gid == null || objrmDeferral.vertical_gid == "")
            {
                msSQL += "  1=1 ";
            }
            else
            {
                msSQL += "   a.vertical_gid = '" + objrmDeferral.vertical_gid + "'";
            }
            if (objrmDeferral.branch_gid == null || objrmDeferral.branch_gid == "")
            {
                msSQL += " and  1=1 ";
            }
            else
            {
                msSQL += " and  a.branch_gid = '" + objrmDeferral.branch_gid + "'";
            }
            if (objrmDeferral.relationshipMgmt == null || objrmDeferral.relationshipMgmt == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and a.relationshipmgmt_gid = '" + objrmDeferral.relationshipMgmt + "'";
            }

            if (objrmDeferral.entity_gid == null || objrmDeferral.entity_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and a.entity_gid = '" + objrmDeferral.entity_gid + "'";
            }

            if (objrmDeferral.customer_gid == null || objrmDeferral.customer_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and a.customer_gid = '" + objrmDeferral.customer_gid + "'";
            }
            if (objrmDeferral.state_gid == null || objrmDeferral.state_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and n.state_gid = '" + objrmDeferral.state_gid + "'";
            }
            msSQL += " and (a.checker_status='Approved' or a.checker_status='N/A') order by a.deferral_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Deferral List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                
                objrmDeferral.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(objrmDeferral.lspath)))
                        System.IO.Directory.CreateDirectory(objrmDeferral.lspath);
                }
                
                objrmDeferral.lsname = "Report.xlsx";
                objrmDeferral.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objrmDeferral.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 27])  //Address "A1:A27"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                dt_datatable.Dispose();
                bool status;
                objrmDeferral.lspath = lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                status = objcmnstorage.UploadStream("erpdocument", objrmDeferral.lspath, ms);
                ms.Close();

                objrmDeferral.lspath = objcmnstorage.EncryptData(objrmDeferral.lspath);
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
            catch (Exception ex)
            {
               
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
        }
        public void DaGetExcelExport(deferralSummary objrmDeferral, string employee_gid)
        {            
            msSQL = " SELECT a.entity_name as 'Entity',n.state as 'State',SUBSTRING_INDEX(a.branch_name, '/', -1) as 'Branch',a.customer_name as 'Customer', " +
                    " a.vertical_code as 'Vertical' ," +
                    " a.record_id as 'Record ID', a.tracking_type as 'Tracking Type'," +
                    " a.deferral_catagory as 'Deferral Catagory'," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as 'Deferral Type',a.criticallity as 'Criticality',  " +
                     " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as 'Due Date'," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as 'Extended Date'," +
                    " case when m.approval_status='ReOpen' then 'Pending' when m.approval_status='Extend' then 'Pending' when m.approval_status='Re-track' then 'Pending' else  m.approval_status end as 'Deferral Stage', " +
                    " case when m.approval_status='Closed' then 'Closed' when m.approval_status='Waived' then 'Waived' else a.deferral_status end as 'Deferral Status'," +
                    " case when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as 'Remarks'," +
                    " case when m.approval_status='Closed' then '-' else a.aging end as 'Aging'," +
                    " z.employee_emailid as 'Relationship Manager'," +
                     " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as 'Risk Manager', " +
                    " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as 'Zonal Risk Manager', " +
                    " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as 'Head Risk Monitoring', " +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as  'Extension Count' , " +
                    " case when n.npatag_flag = 'Y' then n.npatag_remarks else '' end as 'NPA Tagged Remarks' " +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left" +
                    " join hrm_mst_temployee z on a.relationshipmgmt_gid = z.employee_gid " +
                    " left join hrm_mst_temployee r on n.npatagged_by = r.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = r.user_gid " +
                    " where (m.approval_status='Extend' or m.approval_status='Pending' or m.approval_status='ReOpen' or m.approval_status='Re-track') " +
                    " and a.deferral_catagory not in('CAD Internal','CMD Internal') and";

            if (objrmDeferral.vertical_gid == null || objrmDeferral.vertical_gid == "")
            {
                msSQL += "  1=1 ";
            }
            else
            {
                msSQL += "   a.vertical_gid = '" + objrmDeferral.vertical_gid + "'";
            }
            if (objrmDeferral.branch_gid == null || objrmDeferral.branch_gid == "")
            {
                msSQL += " and  1=1 ";
            }
            else
            {
                msSQL += " and  a.branch_gid = '" + objrmDeferral.branch_gid + "'";
            }
            if (objrmDeferral.relationshipMgmt == null || objrmDeferral.relationshipMgmt == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and a.relationshipmgmt_gid = '" + objrmDeferral.relationshipMgmt + "'";
            }

            if (objrmDeferral.entity_gid == null || objrmDeferral.entity_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and a.entity_gid = '" + objrmDeferral.entity_gid + "'";
            }

            if (objrmDeferral.customer_gid == null || objrmDeferral.customer_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and a.customer_gid = '" + objrmDeferral.customer_gid + "'";
            }
            if (objrmDeferral.state_gid == null || objrmDeferral.state_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and n.state_gid = '" + objrmDeferral.state_gid + "'";
            }
            msSQL += " and (a.checker_status='Approved' or a.checker_status='N/A') order by a.deferral_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);          
            
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Deferral List");
            try
            {
               
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objrmDeferral.lsname = "Report.xlsx";            
                lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/UserReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                if (!System.IO.Directory.Exists(lspath))
                    System.IO.Directory.CreateDirectory(lspath);

                objrmDeferral.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/UserReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objrmDeferral.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 22])  //Address "A1:A18"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                dt_datatable.Dispose();
                bool status;
                objrmDeferral.lspath = lscompany_code + "/" + "ECMS/DeferralDoc/UserReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                status = objcmnstorage.UploadStream("erpdocument", objrmDeferral.lspath, ms);
                ms.Close();

                objrmDeferral.lspath = objcmnstorage.EncryptData(objrmDeferral.lspath);
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
            catch (Exception ex)
            {
             
                objrmDeferral.status = false;
                objrmDeferral.message = ex.ToString ();
            }
        }
        public void DaGetCheckerExcelExport(deferralSummary objrmDeferral, string employee_gid)
        {
            msSQL = " SELECT a.entity_name as 'Entity',n.state as 'State',SUBSTRING_INDEX(a.branch_name, '/', -1) as 'Branch',a.customer_name as 'Customer', " +
                    " a.vertical_code as 'Vertical' ,a.loanref_no as 'Loan Ref No',a.sanction_refno as 'Sanction Refno',a.sanction_date as 'Sanction Date'," +
                    " a.record_id as 'Record ID'," +
                    " a.deferral_catagory as 'Deferral Catagory', a.tracking_type as 'Tracking Type'," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as 'Deferral Type',a.criticallity as 'Criticality',  " +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as 'Due Date'," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as 'Extended Date'," +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as 'Deferral Stage', " +
                    " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as 'Deferral Status'," +
                    " case when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as 'Internal Remarks'," +
                    " case when m.customer_remarks <> '' then m.customer_remarks else a.customer_remarks end as 'Customer Remarks'," +
                    " case when m.approval_status='Closed' then '-' else a.aging end as 'Aging'," +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as 'Relationship Manager', " +
                    " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as 'Cluster Manager', " +
                    " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as 'Zonal Head'," +
                    " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as 'Business Head', " +
                    " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as 'Credit Manager', " +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as  'Extension Count'," +
                    " concat(o.user_code,'/',o.user_firstname,o.user_lastname) as 'Created By' ," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'Created Date'" +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser o on o.user_gid = p.user_gid " +
                    " left join hrm_mst_temployee z on a.relationshipmgmt_gid = z.employee_gid where " +
                    " a.checker_status='Pending' ";
            if (objrmDeferral.deferral_gid.Length<1)
            {
                msSQL += " and 1=1";
            }
            else
            {
                mspSQL += "(";
                foreach (string i in objrmDeferral.deferral_gid)
                {
                    mspSQL += "'" + i + "',";
                }              
                msSQL += " and a.deferral_gid in " + mspSQL.TrimEnd(',') + "";
                msSQL += ")";
            }

            msSQL += " order by a.deferral_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Deferral List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objrmDeferral.lsname = "Report.xlsx";
                lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/UserReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                if (!System.IO.Directory.Exists(lspath))
                    System.IO.Directory.CreateDirectory(lspath);

                objrmDeferral.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/UserReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objrmDeferral.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 28])  //Address "A1:A18"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                objrmDeferral.lspath = lscompany_code + "/" + "ECMS/DeferralDoc/UserReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objrmDeferral.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objrmDeferral.lspath, ms);
                ms.Close();

                dt_datatable.Dispose();
                objrmDeferral.lspath = objcmnstorage.EncryptData(objrmDeferral.lspath);
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
            catch (Exception ex)
            {
               
            }
        }

        public void DaPostCreateDeferral(createDeferral values, string employee_gid, string user_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("LNDR");
            msGetGidREF = objcmnfunctions.GetMasterGID("RDID");
            msSQL = " select vertical_gid from ocs_mst_tvertical where vertical_code='" + values.vertical_code + "'";
            lsvertical_gid = objdbconn.GetExecuteScalar(msSQL);
            for (var i = 0; i < values.loans.Count; i++)
            {
                msSQL = " select sanction_refno,sanction_date from ocs_trn_tloan where loan_gid='" + values.loans[i].value + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    lssanctionref = objODBCDatareader["sanction_refno"].ToString();
                    lssanction = objODBCDatareader["sanction_date"].ToString();
                }
                objODBCDatareader.Close();
                gid = objcmnfunctions.GetMasterGID("DR2L");
                msSQL = " insert into ocs_trn_tdeferral2loan(" +
                        " deferral2loan_gid," +
                        " deferral_gid," +
                        " entity_gid," +
                        " entity_name," +
                        " branch_gid," +
                        " branch_name," +
                        " loan_gid," +
                        " covenanttype_gid," +
                        " covenanttype_name," +
                        " customer_gid," +
                        " record_id," +
                        " tracking_type," +
                        " deferraltype_gid," +
                        " deferral_name," +
                        " criticallity," +
                        " vertical_gid," +
                        " vertical_code," +
                        " deferral_catagory," +
                        " due_date," +
                        " sanction_refno," +
                        " sanction_date," +
                        " remarks," +
                        " customer_remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + gid + "'," +
                        "'" + msGetGid + "'," +
                        "'" + values.entity_gid + "'," +
                        "'" + values.entity_name.Replace("    ", "").Replace("\n", "") + "'," +
                        "'" + values.branch_gid + "'," +
                        "'" + values.branch_name.Replace("    ", "").Replace("\n", "") + "'," +
                        "'" + values.loans[i].value + "'," +
                        "'" + values.covenanttype_gid + "'," +
                        "'" + values.covenanttype_name + "'," +
                        "'" + values.customer_gid + "'," +
                        "'" + msGetGidREF + "'," +
                        "'" + values.tracking_type.Replace("'", "") + "',";
                if (values.deferraltype_gid == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.deferraltype_gid + "',";
                }

                msSQL += "'" + values.deferral_name.Replace("'", "") + "'," +
                         "'" + values.criticallity + "'," +
                         "'" + lsvertical_gid + "'," +
                         "'" + values.vertical_code + "'," +
                         "'" + values.deferral_category + "'," +
                         "'" + Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + lssanctionref + "',";
                if (lssanction == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(lssanction).ToString("yyyy-MM-dd HH:mm:ss") + "',";

                }
                if (values.remarks == null)
                {
                    msSQL += "'null',";
                }
                else
                {
                    msSQL += "'" + values.remarks.Replace("'", "") + "',";
                }
                if (values.customerremarks == null)
                {
                    msSQL += "'null',";
                }
                else
                {
                    msSQL += "'" + values.customerremarks.Replace("'", "") + "',";
                }
                msSQL += "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " update ocs_trn_tcaddocument set deferral_gid='" + msGetGid + "' where deferral_gid='" + user_gid + "' and " +
                    " created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            for (var i = 0; i < values.loans.Count; i++)
            {
                loan_gid += "'" + values.loans[i].value + "',";
            }
            msSQL = " select loanref_no,date_format(sanction_date, '%d-%m-%Y') as sanction_date,sanction_refno from ocs_trn_tloan where loan_gid in(" + loan_gid.TrimEnd(',') + ")";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                loanref_no += "'" + dr_datarow["loanref_no"].ToString() + "',";
                sanction_date += "'" + dr_datarow["sanction_date"].ToString() + "',";
                sanction_refno += "'" + dr_datarow["sanction_refno"].ToString() + "',";
            }
            dt_datatable.Dispose();

            msSQL = " select regional_head,regionalhead_name from ocs_mst_tcustomer where customer_gid='"+ values.customer_gid +"'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lsregional_head = objODBCDatareader["regional_head"].ToString();
                lsregionalhead_name = objODBCDatareader["regionalhead_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " insert into ocs_trn_tdeferral(" +
                     " deferral_gid," +
                     " loan_gid," +
                     " loanref_no," +
                     " entity_gid," +
                     " entity_name," +
                     " branch_gid," +
                     " branch_name," +
                     " covenanttype_gid," +
                     " covenanttype_name," +
                     " customer_gid," +
                     " customer_name," +
                     " zonal_gid," +
                     " businesshead_gid," +
                     " relationshipmgmt_gid," +
                     " cluster_manager_gid," +
                     " creditmanager_gid," +
                     " regionalhead_gid," +
                     " zonal_name," +
                     " businesshead_name," +
                     " relationshipmgmt_name," +
                     " cluster_manager_name," +
                     " creditmgmt_name," +
                     " regionalhead_name," +
                     " record_id," +
                     " tracking_type," +
                     " criticallity," +
                     " vertical_gid, " +
                     " vertical_code, " +
                     " deferraltype_gid," +
                     " deferral_name," +
                     " deferral_catagory," +
                     " due_date," +
                     " sanction_refno," +
                     " sanction_date," +
                     " remarks," +
                     " customer_remarks," +
                     " checker_status," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + loan_gid.TrimEnd(',').Replace("'", "") + "'," +
                     "'" + loanref_no.TrimEnd(',').Replace("'", "") + "'," +
                     "'" + values.entity_gid + "'," +
                     "'" + values.entity_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.branch_gid + "'," +
                     "'" + values.branch_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.covenanttype_gid + "'," +
                     "'" + values.covenanttype_name + "'," +
                     "'" + values.customer_gid + "'," +
                     "'" + values.customer_name + "'," +
                     "'" + values.zonalGid + "'," +
                     "'" + values.businessHeadGid + "'," +
                     "'" + values.relationshipMgmtGid + "'," +
                     "'" + values.clustermanagerGid + "'," +
                     "'" + values.creditmanager_gid + "'," +
                     "'" + lsregional_head + "'," +
                     "'" + values.zonal_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.businesshead_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.relationshipmgmt_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.cluster_manager_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.creditmgmt_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + lsregionalhead_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + msGetGidREF + "'," +
                     "'" + values.tracking_type.Replace("'", "") + "'," +
                     "'" + values.criticallity + "'," +
                     "'" + lsvertical_gid + "'," +
                     "'" + values.vertical_code + "',";

            if (values.deferraltype_gid == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.deferraltype_gid + "',";
            }
            msSQL += "'" + values.deferral_name.Replace("'", "") + "'," +
                     "'" + values.deferral_category + "'," +
                     "'" + Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     "'" + sanction_refno.Replace("'", "").TrimEnd(',').TrimStart(',') + "',";
            if (sanction_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + sanction_date.Replace("'", "").TrimEnd(',').TrimStart(',') + "',";

            }
            if (values.remarks == null)
            {
                msSQL += "'null',";
            }
            else
            {
                msSQL += "'" + values.remarks.Replace("'", "") + "',";
            }
            if (values.customerremarks == null)
            {
                msSQL += "'null',";
            }
            else
            {
                msSQL += "'" + values.customerremarks.Replace("'", "") + "',";
            }
            msSQL += "'Pending'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE ocs_trn_tdeferral " +
                 " set deferral_status = 'Expired' where deferral_gid='" + msGetGid + "' and  DATEDIFF(due_date, now())< 0 ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE ocs_trn_tdeferral " +
                   " set deferral_status = 'Live' where deferral_gid='" + msGetGid + "' and DATEDIFF(due_date, now())> 0 ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE ocs_trn_tdeferral" +
                   " set aging = DATEDIFF(now(), due_date)" +
                   " where deferral_gid='" + msGetGid + "' and old_duedate is null and DATEDIFF(due_date, now())< 0";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE ocs_trn_tdeferral" +
                   " set aging='0' where deferral_gid='" + msGetGid + "' and old_duedate is null and DATEDIFF(due_date, now())> 0";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "  UPDATE ocs_trn_tdeferral" +
                    " set aging='0' where deferral_gid='" + msGetGid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())> 0";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            gidapp = objcmnfunctions.GetMasterGID("DFAR");
            msSQL = " insert into ocs_trn_tdeferralapproval(" +
                    " deferralapproval_gid," +
                    " deferral_gid," +
                    " checker_status," +
                    " checkrequest_by," +
                    " checkrequest_date," +
                    " approval_status," +
                    " approvalrequest_by," +
                    " approvalrequest_date)" +
                    " values(" +
                    "'" + gidapp + "'," +
                    "'" + msGetGid + "'," +
                     "'Pending'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'Pending'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            gidhis = objcmnfunctions.GetMasterGID("DFAH");
            msSQL = " insert into ocs_trn_tdeferralapprovalhistory(" +
                    " deferralapprovalhistory_gid," +
                    " deferral_gid," +
                    " approval_status," +
                    " approvalrequest_by," +
                    " approvalrequest_date)" +
                    " values(" +
                    "'" + gidhis + "'," +
                    "'" + msGetGid + "'," +
                    "'Pending'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGidchecker = objcmnfunctions.GetMasterGID("CKAH");
            msSQL = " insert into ocs_trn_tcheckerapprovalhistory(" +
                   " checkerapprovalhistory_gid," +
                   " deferral_gid," +
                   " checker_status," +
                   " checkrequest_by," +
                   " checkrequest_date)" +
                   " values(" +
                   "'" + msGetGidchecker + "'," +
                   "'" + msGetGid + "'," +
                   "'Pending',";
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "success";
            }
            else
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaPostLoan2Deferral(loan2Deferral values, string employee_gid, string user_gid)
        {
            msSQL = " select loanref_no from ocs_trn_tloan where loan_gid='" + values.loan_gid + "'";
            loanref_no = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select vertical_gid from ocs_mst_tvertical where vertical_code='" + values.vertical_code + "'";
            lsvertical_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select sanction_refno,sanction_date from ocs_trn_tloan where loan_gid='" + values.loan_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lssanctionreference = objODBCDatareader["sanction_refno"].ToString();
                lssanctiondate = objODBCDatareader["sanction_date"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select regional_head,regionalhead_name from ocs_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lsregional_head = objODBCDatareader["regional_head"].ToString();
                lsregionalhead_name = objODBCDatareader["regionalhead_name"].ToString();
            }
            objODBCDatareader.Close();
            
            msGetGid = objcmnfunctions.GetMasterGID("LNDR");
            msGetGidREF = objcmnfunctions.GetMasterGID("RDID");
            msSQL = " insert into ocs_trn_tdeferral(" +
                    " deferral_gid," +
                    " loan_gid," +
                    " entity_gid," +
                    " entity_name," +
                    " branch_gid," +
                    " branch_name," +
                    " loanref_no," +
                    " covenanttype_gid," +
                    " covenanttype_name," +
                    " customer_gid," +
                    " customer_name," +
                    " zonal_gid," +
                    " businesshead_gid," +
                    " relationshipmgmt_gid," +
                    " cluster_manager_gid," +
                    " creditmanager_gid," +
                    " regionalhead_gid," +
                    " zonal_name," +
                    " businesshead_name," +
                    " relationshipmgmt_name," +
                    " cluster_manager_name," +
                    " creditmgmt_name," +
                    " regionalhead_name," +
                    " record_id," +
                    " criticallity," +
                    " vertical_gid," +
                    " vertical_code," +
                    " sanction_refno," +
                    " sanction_date," +
                    " tracking_type," +
                    " deferraltype_gid," +
                    " deferral_name," +
                    " deferral_catagory," +
                    " due_date," +
                    " remarks," +
                    " customer_remarks," +
                    " checker_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.loan_gid + "'," +
                    "'" + values.entity_gid + "'," +
                    "'" + values.entity_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + values.branch_gid + "'," +
                    "'" + values.branch_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + loanref_no + "',";

            if (values.covenanttype_gid == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.covenanttype_gid + "',";
            }
            msSQL += "'" + values.covenanttype_name + "'," +
                     "'" + values.customer_gid + "'," +
                     "'" + values.customer_name + "'," +
                     "'" + values.zonalGid + "'," +
                     "'" + values.businessHeadGid + "'," +
                     "'" + values.relationshipMgmtGid + "'," +
                     "'" + values.clustermanagerGid + "'," +
                     "'" + values.creditmanager_gid + "'," +
                     "'" + lsregional_head + "'," +
                     "'" + values.zonal_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.businesshead_name.Replace("    ", "").Replace("\n", "") + "',";
            if (values.relationshipmgmt_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.relationshipmgmt_name.Replace("    ", "").Replace("\n", "") + "',";
            }
            msSQL += "'" + values.cluster_manager_name.Replace("    ", "").Replace("\n", "") + "'," +
                   "'" + values.creditmgmt_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + lsregionalhead_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + msGetGidREF + "'," +
                     "'" + values.criticallity + "'," +
                     "'" + lsvertical_gid + "'," +
                     "'" + values.vertical_code + "'," +
                     "'" + lssanctionreference + "',";
            if (lssanctiondate == "")
            {
                msSQL += "null,";
            }
            else if (lssanctiondate == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(lssanctiondate).ToString("dd-MM-yyyy") + "',";

            }
            msSQL += "'" + values.tracking_type.Replace("'", "") + "',";
            if (values.deferraltype_gid == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.deferraltype_gid + "',";
            }
            msSQL += "'" + values.deferral_name.Replace("'", "") + "'," +
                     "'" + values.deferral_category + "'," +
                     "'" + Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            if (values.remarks == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.remarks.Replace("'", "") + "',";
            }
            if (values.customerremarks == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.customerremarks.Replace("'", "") + "',";
            }
            msSQL += "'Pending'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tcaddocument set deferral_gid='" + msGetGid + "' where deferral_gid='" + user_gid + "' and " +
                    " created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            gid = objcmnfunctions.GetMasterGID("DR2L");
            msSQL = " insert into ocs_trn_tdeferral2loan(" +
                    " deferral2loan_gid," +
                    " deferral_gid," +
                    " entity_gid," +
                    " entity_name," +
                    " branch_gid," +
                    " branch_name," +
                    " sanction_refno," +
                    " sanction_date," +
                    " loan_gid," +
                    " covenanttype_gid," +
                    " covenanttype_name," +
                    " customer_gid," +
                    " record_id," +
                    " criticallity," +
                    " vertical_gid," +
                    " vertical_code," +
                    " tracking_type," +
                    " deferraltype_gid," +
                    " deferral_name," +
                    " deferral_catagory," +
                    " due_date," +
                    " remarks," +
                    " customer_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + gid + "'," +
                    "'" + msGetGid + "'," +
                    "'" + values.entity_gid + "'," +
                    "'" + values.entity_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + values.branch_gid + "'," +
                    "'" + values.branch_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + lssanctionreference + "',";
            if (lssanctiondate == null)
            {
                msSQL += "null,";
            }
            else if (lssanctiondate == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(lssanctiondate).ToString("yyyy-MM-dd HH:mm:ss") + "',";

            }
            msSQL += "'" + values.loan_gid + "'," +
                     "'" + values.covenanttype_gid + "'," +
                     "'" + values.covenanttype_name + "'," +
                     "'" + values.customer_gid + "'," +
                     "'" + msGetGidREF + "'," +
                     "'" + values.criticallity + "'," +
                     "'" + lsvertical_gid + "'," +
                     "'" + values.vertical_code + "'," +
                     "'" + values.tracking_type.Replace("'", "") + "',";
            if (values.deferraltype_gid == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.deferraltype_gid + "',";
            }
            msSQL += "'" + values.deferral_name.Replace("'", "") + "'," +
                     "'" + values.deferral_category + "'," +
                     "'" + Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            if (values.remarks == null)
            {
                msSQL += "'null',";
            }
            else
            {
                msSQL += "'" + values.remarks.Replace("'", "") + "',";
            }
            if (values.customerremarks == null)
            {
                msSQL += "'null',";
            }
            else
            {
                msSQL += "'" + values.customerremarks.Replace("'", "") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE ocs_trn_tdeferral " +
                 " set deferral_status = 'Expired' where deferral_gid='" + msGetGid + "' and  DATEDIFF(due_date, now())< 0 ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE ocs_trn_tdeferral " +
                   " set deferral_status = 'Live' where deferral_gid='" + msGetGid + "' and DATEDIFF(due_date, now())> 0 ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE ocs_trn_tdeferral" +
                   " set aging = DATEDIFF(now(), due_date)" +
                   " where deferral_gid='" + msGetGid + "' and old_duedate is null and DATEDIFF(due_date, now())< 0";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE ocs_trn_tdeferral" +
                   " set aging='0' where deferral_gid='" + msGetGid + "' and old_duedate is null and DATEDIFF(due_date, now())> 0";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "  UPDATE ocs_trn_tdeferral" +
                    " set aging='0' where deferral_gid='" + msGetGid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())> 0";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            gidapp = objcmnfunctions.GetMasterGID("DFAR");
            msSQL = " insert into ocs_trn_tdeferralapproval(" +
                    " deferralapproval_gid," +
                    " deferral_gid," +
                    " checker_status," +
                    " checkrequest_by," +
                    " checkrequest_date," +
                    " approval_status," +
                    " approvalrequest_by," +
                    " approvalrequest_date)" +
                    " values(" +
                    "'" + gidapp + "'," +
                    "'" + msGetGid + "'," +
                    "'Pending'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'Pending'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            gidhis = objcmnfunctions.GetMasterGID("DFAH");
            msSQL = " insert into ocs_trn_tdeferralapprovalhistory(" +
                   " deferralapprovalhistory_gid," +
                   " deferral_gid," +
                   " approval_status," +
                   " approvalrequest_by," +
                   " approvalrequest_date)" +
                   " values(" +
                   "'" + gidhis + "'," +
                   "'" + msGetGid + "'," +
                   "'Pending'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGidchecker = objcmnfunctions.GetMasterGID("CKAH");
            msSQL = " insert into ocs_trn_tcheckerapprovalhistory(" +
                   " checkerapprovalhistory_gid," +
                   " deferral_gid," +
                   " checker_status," +
                   " checkrequest_by," +
                   " checkrequest_date)" +
                   " values(" +
                   "'" + msGetGidchecker + "'," +
                   "'" + msGetGid + "'," +
                   "'Pending',";
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "success";
            }
            else
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetDeferralMaster(deferral objgetDeferral)
        {
            msSQL = " SELECT *  FROM ocs_mst_tdeferral order by deferral_name ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDeferral = new List<deferral_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getDeferral.Add(new deferral_list
                    {
                        deferraltype_gid = (dr_datarow["deferraltype_gid"].ToString()),
                        deferral_code = (dr_datarow["deferral_code"].ToString()),
                        deferral_name = (dr_datarow["deferral_name"].ToString()),
                    });
                }
                objgetDeferral.deferral_list = getDeferral;
            }
            dt_datatable.Dispose();
            objgetDeferral.status = true;
        }

        public void DaGetDeferralSummary(deferralSummary objDeferral, string loan_gid)
        {
            msSQL = " select a.deferral_gid, a.loan_gid, a.record_id, a.tracking_type, case when a.tracking_type = 'Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, a.vertical_gid, a.vertical_code," +
                    " a.deferral_catagory,a.remarks,a.checker_status, " +
                      " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " date_format(a.created_date, '%d-%m-%Y') as created_date " +
                    " from ocs_trn_tdeferral a " +
                    " left join ocs_trn_tloan b on a.loan_gid=b.loan_gid " +
                    " where a.loan_gid='" + loan_gid + "' order by a.deferral_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetDeferral = new List<deferralSummaryDtls>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetDeferral.Add(new deferralSummaryDtls
                    {
                        deferral_gid = (dr_datarow["deferral_gid"].ToString()),
                        loan_gid = (dr_datarow["loan_gid"].ToString()),
                        record_id = (dr_datarow["record_id"].ToString()),
                        tracking_type = (dr_datarow["tracking_type"].ToString()),
                        deferral_name = (dr_datarow["deferral_name"].ToString()),
                        checker_status = (dr_datarow["checker_status"].ToString()),
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_code = (dr_datarow["vertical_code"].ToString()),
                        deferral_catagory = (dr_datarow["deferral_catagory"].ToString()),
                        duedate = (dr_datarow["due_date"].ToString()),
                        extended_date = (dr_datarow["extended_date"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())
                    });
                }
                objDeferral.deferralSummaryDtls = objGetDeferral;
            }
            dt_datatable.Dispose();
            objDeferral.status = true;
            objDeferral.message = "Success";
        }

        public void DaGetRm(deferralSummary objrmDeferral, string employee_gid)
        {
            msSQL = " select a.relationshipmgmt_gid,a.relationshipmgmt_name,a.branch_name,a.entity_name,count(*) as count " +
                    " from ocs_trn_tdeferral a " +
                    " left join ocs_trn_tdeferral2loan b on a.deferral_gid=b.deferral_gid " +
                    " left join ocs_trn_tloan c on c.loan_gid=b.loan_gid " +
                    " left join ocs_trn_tdeferralapproval f on f.deferral_gid=a.deferral_gid " +
                    " where f.approval_status<>'Closed' group by a.relationshipmgmt_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetDeferral = new List<deferralSummaryDtls>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetDeferral.Add(new deferralSummaryDtls
                    {
                        relationshipmgmt_gid = (dr_datarow["relationshipmgmt_gid"].ToString()),
                        relationshipmgmt_name = (dr_datarow["relationshipmgmt_name"].ToString()),
                        branch_name = (dr_datarow["branch_name"].ToString()),
                        entity_name = (dr_datarow["entity_name"].ToString()),
                        count = (dr_datarow["count"].ToString()),
                    });
                }
                objrmDeferral.deferralSummaryDtls = objGetDeferral;
            }
            dt_datatable.Dispose();
            objrmDeferral.status = true;
            objrmDeferral.message = "Success";
        }
        //getusercode
        public void DaGetUserCode(usercode objusercode, string user_gid)
        {
            msSQL = "select user_code from adm_mst_tuser where user_gid='" + user_gid + "'";
            objusercode.user_code = objdbconn.GetExecuteScalar(msSQL);
        }
        // getrmSummary
        public void DaGetRMSummary(rmdeferralSummary objrmDeferral, string employee_gid)
        {
            msSQL = " SELECT a.deferral_gid,a.customer_name as customer_name,a.tracking_type,a.record_id," +
                    "  a.deferral_catagory,a.loanref_no as loan_title,a.vertical_code," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                     " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status " +
                    " from ocs_trn_tdeferral a" +
                    " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid where ";
            if (employee_gid == "")
            {
                msSQL += "  1=1 ";
            }
            else
            {
                msSQL += "  a.relationshipmgmt_gid = '" + employee_gid + "'";
            }
            msSQL += " group by a.deferral_gid order by a.deferral_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetDeferral = new List<rmdeferralSummaryDtls>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetDeferral.Add(new rmdeferralSummaryDtls
                    {
                        customername = (dr_datarow["customer_name"].ToString()),
                        deferral_gid = (dr_datarow["deferral_gid"].ToString()),
                        record_id = (dr_datarow["record_id"].ToString()),
                        tracking_type = (dr_datarow["tracking_type"].ToString()),
                        deferral_name = (dr_datarow["deferral_name"].ToString()),
                        vertical_code = (dr_datarow["vertical_code"].ToString()),
                        deferral_catagory = (dr_datarow["deferral_catagory"].ToString()),
                        loanTitle = (dr_datarow["loan_title"].ToString()),
                        deferral_status = (dr_datarow["deferral_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                    });
                }
                objrmDeferral.rmdeferralSummaryDtls = objGetDeferral;
            }
            dt_datatable.Dispose();
            objrmDeferral.status = true;
        }
        public void DaGetRmDeferralDetails(deferralSummary objrmDeferral, string relationshipmgmt_gid)
        {
            msSQL = " SELECT a.deferral_gid,a.branch_name,a.customer_name, a.tracking_type,a.record_id," +
                    " a.deferral_catagory," +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " a.remarks,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                    " a.loanref_no as loan_title,a.vertical_code," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status, " +
                    " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
                    " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                    "  case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name " +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid where";
            msSQL += " a.relationshipmgmt_gid = '" + relationshipmgmt_gid + "' and m.approval_status<>'Closed'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetDeferral = new List<deferralSummaryDtls>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetDeferral.Add(new deferralSummaryDtls
                    {
                        customername = (dr_datarow["customer_name"].ToString()),
                        deferral_gid = (dr_datarow["deferral_gid"].ToString()),
                        record_id = (dr_datarow["record_id"].ToString()),
                        tracking_type = (dr_datarow["tracking_type"].ToString()),
                        deferral_name = (dr_datarow["deferral_name"].ToString()),
                        branch_name = (dr_datarow["branch_name"].ToString()),
                        relationshipmgmt_name = (dr_datarow["relationshipmgmt_name"].ToString()),
                        zonal_name = (dr_datarow["zonal_name"].ToString()),
                        businesshead_name = (dr_datarow["businesshead_name"].ToString()),
                        cluster_manager_name = (dr_datarow["cluster_manager_name"].ToString()),
                        vertical_code = (dr_datarow["vertical_code"].ToString()),
                        deferral_catagory = (dr_datarow["deferral_catagory"].ToString()),
                        duedate = (dr_datarow["due_date"].ToString()),
                        extended_date = (dr_datarow["extended_date"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        loanTitle = (dr_datarow["loan_title"].ToString()),
                        clustermanager = (dr_datarow["cluster_manager_name"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                    });
                }
                objrmDeferral.deferralSummaryDtls = objGetDeferral;
            }
            dt_datatable.Dispose();
            objrmDeferral.status = true;
            objrmDeferral.message = "Success";

        }
        // DaGetrmDeferralSummary
        public void DaGetrmDeferralSummary(deferralSummary objrmDeferral)
        {
            msSQL = " SELECT a.deferral_gid,a.checker_status,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory, " +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " a.criticallity," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date," +
                    " a.loanref_no as loan_title,a.vertical_code," +
                    " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                    " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " concat(q.user_code,'/',q.user_firstname,q.user_lastname) as Created_by ," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status " +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid" +
                    " group by a.deferral_gid order by a.deferral_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objrmDeferral.deferralSummaryDtls = dt_datatable.AsEnumerable().Select(row =>
                new deferralSummaryDtls
                {
                    customername = (row["customer_name"].ToString()),
                    deferral_gid = (row["deferral_gid"].ToString()),
                    checker_status = (row["checker_status"].ToString()),
                    record_id = (row["record_id"].ToString()),
                    tracking_type = (row["tracking_type"].ToString()),
                    deferral_name = (row["deferral_name"].ToString()),
                    branch_name = (row["branch_name"].ToString()),
                    entity_name = (row["entity_name"].ToString()),
                    aging = (row["aging"].ToString()),
                    Created_by = (row["Created_by"].ToString()),
                    vertical_code = (row["vertical_code"].ToString()),
                    deferral_catagory = (row["deferral_catagory"].ToString()),
                    duedate = (row["due_date"].ToString()),
                    extended_date = (row["extended_date"].ToString()),
                    criticallity = (row["criticallity"].ToString()),
                    created_date = (row["created_date"].ToString()),
                    loanTitle = (row["loan_title"].ToString()),
                    deferral_status = (row["deferral_status"].ToString()),
                    approval_status = (row["approval_status"].ToString())
                }).ToList();
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
            dt_datatable.Dispose();
        }
        //checker approval summary
        public void DaGetCheckerApprovalSummary(deferralSummary objrmDeferral)
        {
            msSQL = " SELECT a.deferral_gid,a.checker_status,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory, " +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " a.criticallity," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date," +
                    " a.loanref_no as loan_title,a.vertical_code," +
                    " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                     " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " concat(q.user_code,'/',q.user_firstname,q.user_lastname) as Created_by ," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status " +
                    " from  ocs_trn_tdeferral a" +
                    " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid" +
                    " where a.checker_status='Pending'" +
                    " group by a.deferral_gid order by a.deferral_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objrmDeferral.deferralSummaryDtls = dt_datatable.AsEnumerable().Select(row =>
                new deferralSummaryDtls
                {
                    customername = (row["customer_name"].ToString()),
                    deferral_gid = (row["deferral_gid"].ToString()),
                    checker_status = (row["checker_status"].ToString()),
                    record_id = (row["record_id"].ToString()),
                    tracking_type = (row["tracking_type"].ToString()),
                    deferral_name = (row["deferral_name"].ToString()),
                    branch_name = (row["branch_name"].ToString()),
                    entity_name = (row["entity_name"].ToString()),
                    aging = (row["aging"].ToString()),
                    Created_by = (row["Created_by"].ToString()),
                    vertical_code = (row["vertical_code"].ToString()),
                    deferral_catagory = (row["deferral_catagory"].ToString()),
                    duedate = (row["due_date"].ToString()),
                    extended_date = (row["extended_date"].ToString()),
                    criticallity = (row["criticallity"].ToString()),
                    created_date = (row["created_date"].ToString()),
                    loanTitle = (row["loan_title"].ToString()),
                    deferral_status = (row["deferral_status"].ToString()),
                    approval_status = (row["approval_status"].ToString())
                }).ToList();
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
            dt_datatable.Dispose();
        }
        //  getrmDeferralSummaryview
        public void DaGetRmDeferral(deferralSummary objrmDeferral)
        {
            try
            {
                msSQL = " SELECT a.deferral_gid,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name," +
                  " a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                  " a.deferral_catagory,a.criticallity,n.legaltag_flag," +
                  " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                  " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                  " case  " +
                  " when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as remarks," +
                  " a.loanref_no as loan_title,a.vertical_code,n.state," +
                  " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                  " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                  " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                  " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status, " +
                  " z.employee_emailid, " +
                  " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
                  " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as zonal_riskmanagerName, " +
                  " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as assigned_RMName, " +
                  " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as riskMonitoring_Name, " +
                  " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                  " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as count " +
                  " from ocs_trn_tdeferral a" +
                  " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                  " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                  " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                  " left join adm_mst_tuser q on q.user_gid = p.user_gid" +
                  " left join hrm_mst_temployee s on a.updated_by = s.employee_gid" +
                  " left join adm_mst_tuser r on s.user_gid = r.user_gid" +
                  " left join hrm_mst_temployee z on a.relationshipmgmt_gid = z.employee_gid " +
                  " where (m.approval_status='Extend' or m.approval_status='Pending'or m.approval_status='ReOpen' or m.approval_status = 'Re-track')" +
                  " and a.deferral_catagory not in ('CAD Internal','CMD Internal')  ";
                msSQL += " and (a.checker_status='Approved' or a.checker_status='N/A') group by a.deferral_gid order by a.deferral_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objrmDeferral.deferralSummaryDtls = dt_datatable.AsEnumerable().Select(row =>
                    new deferralSummaryDtls
                    {
                        customername = (row["customer_name"].ToString()),
                        deferral_gid = (row["deferral_gid"].ToString()),
                        record_id = (row["record_id"].ToString()),
                        tracking_type = (row["tracking_type"].ToString()),
                        deferral_name = (row["deferral_name"].ToString()),
                        branch_name = (row["branch_name"].ToString()),
                        entity_name = (row["entity_name"].ToString()),
                        rm_mail = (row["employee_emailid"].ToString()),
                        legaltag_flag = (row["legaltag_flag"].ToString()),
                        relationshipmgmt_name = (row["relationshipmgmt_name"].ToString()),
                        zonal_rm = (row["zonal_riskmanagerName"].ToString()),
                        risk_manager = (row["assigned_RMName"].ToString()),
                        riskmonitoring_head = (row["riskMonitoring_Name"].ToString()),
                        aging = (row["aging"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        deferral_catagory = (row["deferral_catagory"].ToString()),
                        duedate = (row["due_date"].ToString()),
                        extended_date = (row["extended_date"].ToString()),
                        remarks = (row["remarks"].ToString()),
                        state = (row["state"].ToString()),
                        criticallity = (row["criticallity"].ToString()),
                        count_extension = (row["count"].ToString()),
                        loanTitle = (row["loan_title"].ToString()),
                        deferral_status = (row["deferral_status"].ToString()),
                        approval_status = (row["approval_status"].ToString())
                    }).ToList();
                }
                dt_datatable.Dispose();
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
            catch
            {
                objrmDeferral.status = false;
                objrmDeferral.message = "failure";
            }
        }
        // reportView
        public void DaGetReportView(deferralSummary objrmDeferral)
        {
            try
            {
                msSQL = " SELECT a.deferral_gid,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory,a.criticallity," +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                      " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " case  " +
                    " when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as remarks," +
                    " a.loanref_no as loan_title,a.vertical_code,n.state," +
                      " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                       " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status, " +
                    " z.employee_emailid,n.legaltag_flag, " +
                    " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as zonal_riskmanagerName, " +
                    " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as assigned_RMName, " +
                    " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as riskMonitoring_Name, " +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as count " +
                    " from  ocs_trn_tdeferral a" +
                    " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid" +
                    " left join hrm_mst_temployee s on a.updated_by = s.employee_gid" +
                    " left join adm_mst_tuser r on s.user_gid = r.user_gid" +
                    " left join hrm_mst_temployee z on a.relationshipmgmt_gid = z.employee_gid " +
                    " where (m.approval_status='Extend' or m.approval_status='Pending' or m.approval_status='ReOpen' or m.approval_status = 'Re-track') " +
                    " and a.deferral_catagory not in('CAD Internal','CMD Internal') ";

                msSQL += " and (a.checker_status='Approved' or a.checker_status='N/A')  group by a.deferral_gid order by a.deferral_gid desc";
                
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objrmDeferral.deferralSummaryDtls = dt_datatable.AsEnumerable().Select(row =>
                    new deferralSummaryDtls
                    {
                        customername = (row["customer_name"].ToString()),
                        deferral_gid = (row["deferral_gid"].ToString()),
                        legaltag_flag = (row["legaltag_flag"].ToString()),
                        record_id = (row["record_id"].ToString()),
                        tracking_type = (row["tracking_type"].ToString()),
                        deferral_name = (row["deferral_name"].ToString()),
                        branch_name = (row["branch_name"].ToString()),
                        entity_name = (row["entity_name"].ToString()),
                        rm_mail = (row["employee_emailid"].ToString()),
                        relationshipmgmt_name = (row["relationshipmgmt_name"].ToString()),
                        zonal_rm = (row["zonal_riskmanagerName"].ToString()),
                        risk_manager = (row["assigned_RMName"].ToString()),
                        riskmonitoring_head = (row["riskMonitoring_Name"].ToString()),
                        aging = (row["aging"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        deferral_catagory = (row["deferral_catagory"].ToString()),
                        duedate = (row["due_date"].ToString()),
                        extended_date = (row["extended_date"].ToString()),
                        remarks = (row["remarks"].ToString()),
                        state = (row["state"].ToString()),
                        criticallity = (row["criticallity"].ToString()),
                        count_extension = (row["count"].ToString()),
                        loanTitle = (row["loan_title"].ToString()),
                        deferral_status = (row["deferral_status"].ToString()),
                        approval_status = (row["approval_status"].ToString())
                    }).ToList();
                }
                dt_datatable.Dispose();
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
            catch
            {
                objrmDeferral.status = false;
                objrmDeferral.message = "Failure";
            }
        }

        // directDeferralSummaryreport
        public void DaGetDirectDeferral(deferralSummary objrmDeferral, string employee_gid)
        {
            try
            {
                msSQL = " SELECT a.deferral_gid,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory,a.sanction_refno,a.sanction_date," +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " case when a.customer_remarks <> '' then a.customer_remarks " +
                    " when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date," +
                    " a.loanref_no as loan_title,a.vertical_code,a.criticallity," +
                    " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                    " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                   " concat(q.user_code,'/',q.user_firstname,q.user_lastname) as Created_by ," +
                   " concat(r.user_code,'/',r.user_firstname,r.user_lastname) as updated_by ," +
                   " n.state,date_format(a.updated_date,'%d-%m-%Y %h:%i:%s %p') as updated_date, " +
                   " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                   " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status, " +
                   " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
                   " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                   " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name,z.employee_emailid, " +
                   " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name ," +
                   " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name ," +
                   " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                   " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as count " +
                   " from ocs_trn_tdeferral a" +
                   " left join hrm_mst_temployee s on a.updated_by = s.employee_gid" +
                   " left join adm_mst_tuser r on s.user_gid = r.user_gid" +
                   " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                   " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                   " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                   " left join hrm_mst_temployee z on a.relationshipmgmt_gid = z.employee_gid" +
                   " left join adm_mst_tuser q on q.user_gid = p.user_gid where";
                if (objrmDeferral.vertical_gid == null || objrmDeferral.vertical_gid == "")
                {
                    msSQL += "  1=1 ";
                }
                else
                {
                    msSQL += "   a.vertical_gid = '" + objrmDeferral.vertical_gid + "'";
                }
                if (objrmDeferral.branch_gid == null || objrmDeferral.branch_gid == "")
                {
                    msSQL += " and  1=1 ";
                }
                else
                {
                    msSQL += " and  a.branch_gid = '" + objrmDeferral.branch_gid + "'";
                }
                if (objrmDeferral.relationshipMgmt == null || objrmDeferral.relationshipMgmt == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.relationshipmgmt_gid = '" + objrmDeferral.relationshipMgmt + "'";
                }
                if (objrmDeferral.zonalHead == null || objrmDeferral.zonalHead == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.zonal_gid = '" + objrmDeferral.zonalHead + "'";
                }
                if (objrmDeferral.businessHead == null || objrmDeferral.businessHead == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.businesshead_gid = '" + objrmDeferral.businessHead + "'";
                }
                if (objrmDeferral.clustermanager == null || objrmDeferral.clustermanager == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.cluster_manager_gid = '" + objrmDeferral.clustermanager + "'";
                }

                if (objrmDeferral.creditmanager == null || objrmDeferral.creditmanager == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.creditmanager_gid = '" + objrmDeferral.creditmanager + "'";
                }
                if (objrmDeferral.customer_gid == null || objrmDeferral.customer_gid == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.customer_gid = '" + objrmDeferral.customer_gid + "'";
                }

                if (objrmDeferral.entity_gid == null || objrmDeferral.entity_gid == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.entity_gid = '" + objrmDeferral.entity_gid + "'";
                }
                if (objrmDeferral.state_gid == null || objrmDeferral.state_gid == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and n.state_gid = '" + objrmDeferral.state_gid + "'";
                }
                msSQL += " order by a.deferral_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var objGetDeferral = new List<deferralSummaryDtls>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objGetDeferral.Add(new deferralSummaryDtls
                        {
                            customername = (dr_datarow["customer_name"].ToString()),
                            deferral_gid = (dr_datarow["deferral_gid"].ToString()),
                            rm_mail = (dr_datarow["employee_emailid"].ToString()),
                            record_id = (dr_datarow["record_id"].ToString()),
                            tracking_type = (dr_datarow["tracking_type"].ToString()),
                            deferral_name = (dr_datarow["deferral_name"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            relationshipmgmt_name = (dr_datarow["relationshipmgmt_name"].ToString()),
                            zonal_name = (dr_datarow["zonal_name"].ToString()),
                            businesshead_name = (dr_datarow["businesshead_name"].ToString()),
                            cluster_manager_name = (dr_datarow["cluster_manager_name"].ToString()),
                            creditmanager = (dr_datarow["creditmgmt_name"].ToString()),
                            vertical_code = (dr_datarow["vertical_code"].ToString()),
                            aging = (dr_datarow["aging"].ToString()),
                            Created_by = (dr_datarow["Created_by"].ToString()),
                            entity_name = (dr_datarow["entity_name"].ToString()),
                            deferral_catagory = (dr_datarow["deferral_catagory"].ToString()),
                            duedate = (dr_datarow["due_date"].ToString()),
                            extended_date = (dr_datarow["extended_date"].ToString()),
                            sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                            sanction_date = (dr_datarow["sanction_date"].ToString()),
                            criticallity = (dr_datarow["criticallity"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            state = (dr_datarow["state"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            loanTitle = (dr_datarow["loan_title"].ToString()),
                            clustermanager = (dr_datarow["cluster_manager_name"].ToString()),
                            deferral_status = (dr_datarow["deferral_status"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            count_extension = (dr_datarow["count"].ToString()),
                        });
                    }
                    objrmDeferral.deferralSummaryDtls = objGetDeferral;
                }
                dt_datatable.Dispose();
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
            catch
            {
                objrmDeferral.status = false;
                objrmDeferral.message = "Failure";
            }
        }
        // directDeferralSummaryreportview
        public void DaGetDirectDeferralReport(deferralSummary objrmDeferral, string employee_gid)
        {
            try
            {
                msSQL = " SELECT a.deferral_gid,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,n.customer_urn," +
                    " a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory,n.legaltag_flag," +
                    " n.state," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date," +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " case when a.customer_remarks <> '' then a.customer_remarks " +
                    " when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as remarks," +
                    " a.loanref_no as loan_title,a.vertical_code,a.criticallity," +
                    " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
                    " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                    " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
                    " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                    " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as zonal_riskmanagerName, " +
                    " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as assigned_RMName, " +
                    " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as riskMonitoring_Name, " +
                    " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                    " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status, " +
                    " z.employee_emailid, " +
                    " concat(sq.user_code,'/',sq.user_firstname,sq.user_lastname) as Created_by ," +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name ," +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as count " +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left join hrm_mst_temployee ps on a.created_by = ps.employee_gid" +
                    " left join adm_mst_tuser sq on sq.user_gid = ps.user_gid" +
                   " left join hrm_mst_temployee s on a.updated_by = s.employee_gid" +
                   " left join adm_mst_tuser r on s.user_gid = r.user_gid" +
                   " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                   " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                   " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                   " left join hrm_mst_temployee z on a.relationshipmgmt_gid = z.employee_gid" +
                   " left join adm_mst_tuser q on q.user_gid = p.user_gid " +
                   " where (m.approval_status='Extend' or m.approval_status='pending') and a.deferral_catagory not in('CAD Internal','CMD Internal')  and ";

                if (objrmDeferral.vertical_gid == null || objrmDeferral.vertical_gid == "")
                {
                    msSQL += "  1=1 ";
                }
                else
                {
                    msSQL += "   a.vertical_gid = '" + objrmDeferral.vertical_gid + "'";
                }
                if (objrmDeferral.branch_gid == null || objrmDeferral.branch_gid == "")
                {
                    msSQL += " and  1=1 ";
                }
                else
                {
                    msSQL += " and  a.branch_gid = '" + objrmDeferral.branch_gid + "'";
                }
                if (objrmDeferral.relationshipMgmt == null || objrmDeferral.relationshipMgmt == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.relationshipmgmt_gid = '" + objrmDeferral.relationshipMgmt + "'";
                }
                if (objrmDeferral.zonalHead == null || objrmDeferral.zonalHead == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.zonal_gid = '" + objrmDeferral.zonalHead + "'";
                }
                if (objrmDeferral.businessHead == null || objrmDeferral.businessHead == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.businesshead_gid = '" + objrmDeferral.businessHead + "'";
                }
                if (objrmDeferral.clustermanager == null || objrmDeferral.clustermanager == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.cluster_manager_gid = '" + objrmDeferral.clustermanager + "'";
                }

                if (objrmDeferral.customer_gid == null || objrmDeferral.customer_gid == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.customer_gid = '" + objrmDeferral.customer_gid + "'";
                }

                if (objrmDeferral.entity_gid == null || objrmDeferral.entity_gid == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.entity_gid = '" + objrmDeferral.entity_gid + "'";
                }

                if (objrmDeferral.state_gid == null || objrmDeferral.state_gid == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and n.state_gid = '" + objrmDeferral.state_gid + "'";
                }

                msSQL += " and (a.checker_status='Approved' or a.checker_status='N/A') order by a.deferral_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var objGetDeferral = new List<deferralSummaryDtls>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objGetDeferral.Add(new deferralSummaryDtls
                        {
                            customername = (dr_datarow["customer_name"].ToString()),
                            customer_urn = (dr_datarow["customer_urn"].ToString()),
                            legaltag_flag = (dr_datarow["legaltag_flag"].ToString()),
                            deferral_gid = (dr_datarow["deferral_gid"].ToString()),
                            rm_mail = (dr_datarow["employee_emailid"].ToString()),
                            record_id = (dr_datarow["record_id"].ToString()),
                            tracking_type = (dr_datarow["tracking_type"].ToString()),
                            deferral_name = (dr_datarow["deferral_name"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            Created_by = (dr_datarow["Created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            zonal_name = (dr_datarow["zonal_name"].ToString()),
                            businesshead_name = (dr_datarow["businesshead_name"].ToString()),
                            cluster_manager_name = (dr_datarow["cluster_manager_name"].ToString()),
                            relationshipmgmt_name = (dr_datarow["relationshipmgmt_name"].ToString()),
                            creditmanager = (dr_datarow["creditmgmt_name"].ToString()),
                            zonal_rm = (dr_datarow["zonal_riskmanagerName"].ToString()),
                            risk_manager = (dr_datarow["assigned_RMName"].ToString()),
                            riskmonitoring_head = (dr_datarow["riskMonitoring_Name"].ToString()),
                            vertical_code = (dr_datarow["vertical_code"].ToString()),
                            aging = (dr_datarow["aging"].ToString()),
                            entity_name = (dr_datarow["entity_name"].ToString()),
                            deferral_catagory = (dr_datarow["deferral_catagory"].ToString()),
                            duedate = (dr_datarow["due_date"].ToString()),
                            extended_date = (dr_datarow["extended_date"].ToString()),
                            criticallity = (dr_datarow["criticallity"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            state = (dr_datarow["state"].ToString()),
                            loanTitle = (dr_datarow["loan_title"].ToString()),
                            deferral_status = (dr_datarow["deferral_status"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            count_extension = (dr_datarow["count"].ToString()),
                        });
                    }
                    objrmDeferral.deferralSummaryDtls = objGetDeferral;
                }
                dt_datatable.Dispose();
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
            catch(Exception ex)
            {
                objrmDeferral.status = false;
                objrmDeferral.message = "Failure";
            }
        }
        // Reportsummaryview
        public void DaGetReports(deferralSummary objrmDeferral, string employee_gid)
        {
            try
            {
                msSQL = " SELECT a.deferral_gid,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                 " a.deferral_catagory,n.legaltag_flag," +
                 " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                 " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                 " case when a.customer_remarks <> '' then a.customer_remarks " +
                 " when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as remarks," +
                 " n.state," +
                 " a.loanref_no as loan_title,a.vertical_code,a.criticallity," +
                 " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                 " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                 " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                 " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status, " +
                 " z.employee_emailid, " +
                 " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as zonal_riskmanagerName, " +
                 " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as assigned_RMName, " +
                 " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as riskMonitoring_Name, " +
                 " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name ," +
                 " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                 " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as count " +
                 " from" +
                 " ocs_trn_tdeferral a" +
                 " left join hrm_mst_temployee s on a.updated_by = s.employee_gid" +
                 " left join adm_mst_tuser r on s.user_gid = r.user_gid" +
                 " left" +
                 " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                 " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                 " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                 " left" +
                 " join hrm_mst_temployee z on a.relationshipmgmt_gid = z.employee_gid" +
                 " left join adm_mst_tuser q on q.user_gid = p.user_gid where (m.approval_status='Extend' or m.approval_status='pending') and a.deferral_catagory not in('CAD Internal','CMD Internal') and";

                if (objrmDeferral.vertical_gid == null || objrmDeferral.vertical_gid == "")
                {
                    msSQL += "  1=1 ";
                }
                else
                {
                    msSQL += "   a.vertical_gid = '" + objrmDeferral.vertical_gid + "'";
                }
                if (objrmDeferral.branch_gid == null || objrmDeferral.branch_gid == "")
                {
                    msSQL += " and  1=1 ";
                }
                else
                {
                    msSQL += " and  a.branch_gid = '" + objrmDeferral.branch_gid + "'";
                }
                if (objrmDeferral.relationshipMgmt == null || objrmDeferral.relationshipMgmt == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.relationshipmgmt_gid = '" + objrmDeferral.relationshipMgmt + "'";
                }
                if (objrmDeferral.zonalHead == null || objrmDeferral.zonalHead == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.zonal_gid = '" + objrmDeferral.zonalHead + "'";
                }
                if (objrmDeferral.businessHead == null || objrmDeferral.businessHead == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.businesshead_gid = '" + objrmDeferral.businessHead + "'";
                }
                if (objrmDeferral.clustermanager == null || objrmDeferral.clustermanager == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.cluster_manager_gid = '" + objrmDeferral.clustermanager + "'";
                }

                if (objrmDeferral.customer_gid == null || objrmDeferral.customer_gid == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.customer_gid = '" + objrmDeferral.customer_gid + "'";
                }

                if (objrmDeferral.entity_gid == null || objrmDeferral.entity_gid == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.entity_gid = '" + objrmDeferral.entity_gid + "'";
                }
                if (objrmDeferral.state_gid == null || objrmDeferral.state_gid == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and n.state_gid = '" + objrmDeferral.state_gid + "'";
                }
                msSQL += " and (a.checker_status='Approved' or a.checker_status='N/A') order by a.deferral_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var objGetDeferral = new List<deferralSummaryDtls>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objGetDeferral.Add(new deferralSummaryDtls
                        {
                            customername = (dr_datarow["customer_name"].ToString()),
                            deferral_gid = (dr_datarow["deferral_gid"].ToString()),
                            legaltag_flag = (dr_datarow["legaltag_flag"].ToString()),
                            rm_mail = (dr_datarow["employee_emailid"].ToString()),
                            record_id = (dr_datarow["record_id"].ToString()),
                            tracking_type = (dr_datarow["tracking_type"].ToString()),
                            deferral_name = (dr_datarow["deferral_name"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            vertical_code = (dr_datarow["vertical_code"].ToString()),
                            zonal_rm = (dr_datarow["zonal_riskmanagerName"].ToString()),
                            risk_manager = (dr_datarow["assigned_RMName"].ToString()),
                            riskmonitoring_head = (dr_datarow["riskMonitoring_Name"].ToString()),
                            aging = (dr_datarow["aging"].ToString()),
                            entity_name = (dr_datarow["entity_name"].ToString()),
                            deferral_catagory = (dr_datarow["deferral_catagory"].ToString()),
                            duedate = (dr_datarow["due_date"].ToString()),
                            extended_date = (dr_datarow["extended_date"].ToString()),
                            criticallity = (dr_datarow["criticallity"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            state = (dr_datarow["state"].ToString()),
                            loanTitle = (dr_datarow["loan_title"].ToString()),
                            deferral_status = (dr_datarow["deferral_status"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            count_extension = (dr_datarow["count"].ToString()),
                        });
                    }
                    objrmDeferral.deferralSummaryDtls = objGetDeferral;
                }
                dt_datatable.Dispose();
                objrmDeferral.status = true;
                objrmDeferral.message = "Success";
            }
            catch
            {
                objrmDeferral.status = false;
                objrmDeferral.message = "Failure";
            }
        }    
        public bool DaPostUploaddocuments(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string deferral_gid = HttpContext.Current.Request.Params["deferral_gid"];
            string loan_gid = HttpContext.Current.Request.Params["loan_gid"];
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " select company_code from adm_mst_tcompany";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();  
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        msGetGid = objcmnfunctions.GetMasterGID("DEDC");
                        msSQL = " insert into ocs_trn_tdeferraldocument( " +
                        " deferraldocument_gid," +
                        " deferral_gid," +
                        " path," +
                        " file_name," +
                        " created_by," +
                        " uploaded_by," +
                        " created_date" +
                        " )values(" +
                        "'" + msGetGid + "'," +
                        "'" + deferral_gid + "'," +
                        "'" + lspath + "'," +
                        "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                        "'" + user_gid + "'," +
                        "'rm'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                        }
                        else
                        {
                            objfilename.status = false;
                        }
                    }
                    DaGetDeferralDocuments(objfilename, deferral_gid);
                }
            }
            catch
            {

            }
            return true;
        }
        // uploaddeferraldocumentbycad

        public void DaPostDocumentbyCad(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string deferral_gid = HttpContext.Current.Request.Params["deferral_gid"];
            string loan_gid = HttpContext.Current.Request.Params["loan_gid"];
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            //msSQL = " SELECT a.company_code FROM adm_mst_ttoken a " +
            //        " LEFT JOIN hrm_mst_temployee b ON a.employee_gid = b.employee_gid " +
            //        " LEFT JOIN  adm_mst_tuser c ON b.user_gid = c.user_gid " +
            //        " WHERE a.employee_gid = '" + employee_gid + "'";

            msSQL = " select company_code from adm_mst_tcompany";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try

            {

                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {

                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("DEDC");
                        msSQL = " insert into ocs_trn_tdeferraldocument( " +
                                " deferraldocument_gid," +
                                " deferral_gid," +
                                " path," +
                                " file_name," +
                                " created_by," +
                                " uploaded_by," +
                                " created_date" +
                        " )values(" +
                        "'" + msGetGid + "'," +
                        "'" + deferral_gid + "'," +
                         "'" + lspath + msdocument_gid + FileExtension + "'," +
                        "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                        "'" + user_gid + "'," +
                        "'cad'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                        }
                        else
                        {
                            objfilename.status = false;
                        }


                    }
                    DaGetDeferralDocuments(objfilename, deferral_gid);


                }
                objfilename.status = true;
                objfilename.message = "Success";
            }
            catch
            {
                objfilename.status = false;
                objfilename.message = "Success";
            }

        }

        // UploadcadDocument
        public void DaPostUploadcadDocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string deferral_gid = string.Empty;
            deferral_gid = HttpContext.Current.Request.Params["deferral_gid"];
            string loan_gid = HttpContext.Current.Request.Params["loan_gid"];
            string lsdocumenttype_gid = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;



            //msSQL = " SELECT a.company_code FROM adm_mst_ttoken a " +
            //      " LEFT JOIN hrm_mst_temployee b ON a.employee_gid = b.employee_gid " +
            //      " LEFT JOIN  adm_mst_tuser c ON b.user_gid = c.user_gid " +
            //      " WHERE a.employee_gid = '" + employee_gid + "'";

            msSQL = " select company_code from adm_mst_tcompany";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try

            {

                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {

                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "ECMS/DeferralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("DEDC");
                        msSQL = " insert into ocs_trn_tcaddocument( " +
                        " caddocument_gid," +
                        " deferral_gid," +
                        " path," +
                        " file_name," +
                        " created_by," +
                        " created_date" +
                        " )values(" +
                        "'" + msGetGid + "',";
                        if (deferral_gid == "undefined")
                        {
                            msSQL += "'" + user_gid + "',";
                        }
                        else if (deferral_gid == "")
                        {
                            msSQL += "'" + user_gid + "',";
                        }
                        else
                        {
                            msSQL += "'" + deferral_gid + "',";
                        }
                        msSQL += "'" + lspath + msdocument_gid + FileExtension + "'," +
                      "'" + httpPostedFile.FileName + "'," +
                      "'" + user_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                        }
                        else
                        {
                            objfilename.status = false;
                        }


                    }
                    //getDeferralDocuments(objfilename, deferral_gid);

                }
                objfilename.status = true;
                objfilename.message = "Success";
            }
            catch
            {
                objfilename.status = false;
                objfilename.message = "Failure";

            }

        }

        // getDeferralDocuments
        public void DaGetDeferralDocuments(UploadDocumentname objfilename, string deferral_gid)
        {


            msSQL = " select a.remarks,a.criticallity,a.sanction_refno, a.sanction_date, " +
                " a.deferral_gid,a.record_id,case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name,a.loanref_no,n.customer_code,a.deferral_catagory," +
                " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date, " +
                " case when m.approval_status='ReOpen' then 'Pending'" +
                " else  m.approval_status end as approval_status," +
                " a.customer_name,a.entity_name,a.branch_name,a.vertical_code, " +
                 " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
                 " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                 " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                 " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name ," +
                 " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name " +
                " from ocs_trn_tdeferral a" +
                " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid " +
                " left join ocs_mst_tcustomer n on a.customer_gid=n.customer_gid" +
                " where a.deferral_gid = '" + deferral_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objfilename.deferral_gid = objODBCDatareader["deferral_gid"].ToString();
                objfilename.record_id = objODBCDatareader["record_id"].ToString();
                objfilename.deferral_catagory = objODBCDatareader["deferral_catagory"].ToString();
                objfilename.deferral_name = objODBCDatareader["deferral_name"].ToString();
                objfilename.remarks = objODBCDatareader["remarks"].ToString();
                objfilename.loanref_no = objODBCDatareader["loanref_no"].ToString();
                objfilename.loan_title = objODBCDatareader["loanref_no"].ToString();
                objfilename.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                objfilename.sanctiondate = objODBCDatareader["sanction_date"].ToString();
                objfilename.approval_status = objODBCDatareader["approval_status"].ToString();
                objfilename.customer_name = objODBCDatareader["customer_name"].ToString();
                objfilename.zonal_name = objODBCDatareader["zonal_name"].ToString();
                objfilename.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                objfilename.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                objfilename.credit_manager = objODBCDatareader["creditmgmt_name"].ToString();
                objfilename.duedate = objODBCDatareader["due_date"].ToString();
                objfilename.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                objfilename.customer_code = objODBCDatareader["customer_code"].ToString();
                objfilename.vertical_code = objODBCDatareader["vertical_code"].ToString();
                objfilename.entity_name = objODBCDatareader["entity_name"].ToString();
                objfilename.branch_name = objODBCDatareader["branch_name"].ToString();
                objfilename.criticallity = objODBCDatareader["criticallity"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select a.file_name,a.deferraldocument_gid,a.path,date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date, " +
                    " concat(c.user_code,'/',c.user_firstname,' ',c.user_lastname) as user_name " +
                    " from ocs_trn_tdeferraldocument a" +
                    "  left join adm_mst_tuser c on c.user_gid=a.created_by " +
                    " where a.deferral_gid='" + deferral_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadDocumentList
                    {
                        filename = (dr_datarow["file_name"].ToString()),
                        id = (dr_datarow["deferraldocument_gid"].ToString()),
                        path = objcmnstorage.EncryptData(dr_datarow["path"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        uploaded_by = (dr_datarow["user_name"].ToString())
                    });
                }
                objfilename.filename_list = get_filename;
            }
            dt_datatable.Dispose();

            objfilename.status = true;
            objfilename.message = "Success";

        }
        // getDeferraldetail

        public void DaGetDeferralDetail(UploadDocumentname objfilename, string deferral_gid, string employee_gid)
        {

            try
            {
                msSQL = " select a.remarks,a.customer_remarks,a.criticallity,a.sanction_refno, a.sanction_date, " +
              " a.deferral_gid,a.record_id,case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name,a.loanref_no,n.customer_code,a.deferral_catagory," +
              " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date, " +
              " case when m.approval_status='ReOpen' then 'Pending'" +
              " else  m.approval_status end as approval_status," +
              " case when m.approval_status='ReOpen' then 'Pending'" +
              " else  m.approval_status end as def_status," +
              " a.customer_name,a.entity_name,a.branch_name,a.vertical_code, " +
              " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
              " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
              " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
              " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
                " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, a.tracking_type " +
              " from ocs_trn_tdeferral a" +
              " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid " +
              " left join ocs_mst_tcustomer n on a.customer_gid=n.customer_gid" +
              " where a.deferral_gid = '" + deferral_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    objfilename.deferral_gid = objODBCDatareader["deferral_gid"].ToString();
                    objfilename.record_id = objODBCDatareader["record_id"].ToString();
                    objfilename.deferral_catagory = objODBCDatareader["deferral_catagory"].ToString();
                    objfilename.deferral_name = objODBCDatareader["deferral_name"].ToString();
                    objfilename.remarks = objODBCDatareader["remarks"].ToString();
                    objfilename.customer_remarks = objODBCDatareader["customer_remarks"].ToString();
                    objfilename.loanref_no = objODBCDatareader["loanref_no"].ToString();
                    objfilename.loan_title = objODBCDatareader["loanref_no"].ToString();
                    objfilename.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                    objfilename.sanctiondate = objODBCDatareader["sanction_date"].ToString();
                    objfilename.approval_status = objODBCDatareader["approval_status"].ToString();
                    objfilename.def_status = objODBCDatareader["def_status"].ToString();
                    objfilename.customer_name = objODBCDatareader["customer_name"].ToString();
                    objfilename.zonal_name = objODBCDatareader["zonal_name"].ToString();
                    objfilename.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                    objfilename.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                    objfilename.credit_manager = objODBCDatareader["creditmgmt_name"].ToString();
                    objfilename.duedate = objODBCDatareader["due_date"].ToString();
                    objfilename.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                    objfilename.customer_code = objODBCDatareader["customer_code"].ToString();
                    objfilename.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    objfilename.entity_name = objODBCDatareader["entity_name"].ToString();
                    objfilename.branch_name = objODBCDatareader["branch_name"].ToString();
                    objfilename.criticallity = objODBCDatareader["criticallity"].ToString();
                    objfilename.tracking_type = objODBCDatareader["tracking_type"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select a.file_name,a.deferraldocument_gid,a.path,date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date, " +
                        " concat(c.user_code,'/',c.user_firstname,' ',c.user_lastname) as user_name " +
                        " from ocs_trn_tdeferraldocument a" +
                        "  left join adm_mst_tuser c on c.user_gid=a.created_by " +
                        " where a.deferral_gid='" + deferral_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadDocumentList
                        {
                            filename = (dr_datarow["file_name"].ToString()),
                            id = (dr_datarow["deferraldocument_gid"].ToString()),
                            path = objcmnstorage.EncryptData(dr_datarow["path"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            uploaded_by = (dr_datarow["user_name"].ToString())
                        });
                    }
                    objfilename.filename_list = get_filename;
                }
                dt_datatable.Dispose();

                objfilename.status = true;
                objfilename.message = "Success";
            }
            catch
            {
                objfilename.status = false;
                objfilename.message = "Failure";
            }

        }
        // getReopen
        public void DaGetReOpen(UploadDocumentname objfilename, string deferral_gid, string employee_gid)
        {
            try
            {
                msSQL = " select a.remarks,a.criticallity,a.sanction_refno, a.sanction_date,a.customer_remarks, " +
              " a.deferral_gid,a.record_id,case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name,a.loanref_no,n.customer_code,a.deferral_catagory," +
              " m.approval_status, " +
              " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date, " +
              " a.customer_name,a.entity_name,a.branch_name,a.vertical_code, " +
               " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
               " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
               " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
               " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
               " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name " +
              " from ocs_trn_tdeferral a" +
              " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid " +
              " left join ocs_mst_tcustomer n on a.customer_gid=n.customer_gid" +
              " where a.deferral_gid = '" + deferral_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    objfilename.deferral_gid = objODBCDatareader["deferral_gid"].ToString();
                    objfilename.record_id = objODBCDatareader["record_id"].ToString();
                    objfilename.deferral_catagory = objODBCDatareader["deferral_catagory"].ToString();
                    objfilename.deferral_name = objODBCDatareader["deferral_name"].ToString();
                    objfilename.remarks = objODBCDatareader["remarks"].ToString();
                    objfilename.loanref_no = objODBCDatareader["loanref_no"].ToString();
                    objfilename.loan_title = objODBCDatareader["loanref_no"].ToString();
                    objfilename.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                    objfilename.sanctiondate = objODBCDatareader["sanction_date"].ToString();
                    objfilename.approval_status = objODBCDatareader["approval_status"].ToString();
                    objfilename.def_status = objODBCDatareader["approval_status"].ToString();
                    objfilename.customer_name = objODBCDatareader["customer_name"].ToString();
                    objfilename.zonal_name = objODBCDatareader["zonal_name"].ToString();
                    objfilename.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                    objfilename.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                    objfilename.credit_manager = objODBCDatareader["creditmgmt_name"].ToString();
                    objfilename.duedate = objODBCDatareader["due_date"].ToString();
                    objfilename.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                    objfilename.customer_code = objODBCDatareader["customer_code"].ToString();
                    objfilename.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    objfilename.entity_name = objODBCDatareader["entity_name"].ToString();
                    objfilename.branch_name = objODBCDatareader["branch_name"].ToString();
                    objfilename.criticallity = objODBCDatareader["criticallity"].ToString();
                    objfilename.customer_remarks = objODBCDatareader["customer_remarks"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select a.file_name,a.deferraldocument_gid,a.path,date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date, " +
                        " concat(c.user_code,'/',c.user_firstname,' ',c.user_lastname) as user_name " +
                        " from ocs_trn_tdeferraldocument a" +
                        "  left join adm_mst_tuser c on c.user_gid=a.created_by " +
                        " where a.deferral_gid='" + deferral_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadDocumentList
                        {
                            filename = (dr_datarow["file_name"].ToString()),
                            id = (dr_datarow["deferraldocument_gid"].ToString()),
                            path = objcmnstorage.EncryptData(dr_datarow["path"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            uploaded_by = (dr_datarow["user_name"].ToString())
                        });
                    }
                    objfilename.filename_list = get_filename;
                }
                dt_datatable.Dispose();
                objfilename.status = true;
                objfilename.message = "Success";
            }
            catch
            {
                objfilename.status = false;
                objfilename.message = "failure";
            }
        }
        // insertdeferral
        public void DaPostInsertDeferral(deferrelmaster values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("DEFR");
            msGetGidREF = objcmnfunctions.GetMasterGID("DEF");
            msSQL = " insert into ocs_mst_tdeferral(" +
                    " deferraltype_gid," +
                    " deferral_code," +
                    " deferral_name," +
                    " criticallity," +
                    " comments," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.deferral_code + "'," +
                    "'" + values.deferral_name + "'," +
                    "'" + values.criticallity + "',";
            if(values.comments==null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.comments.Replace("'", "") + "',";
            }
                  
                 msSQL+=   "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "failure";
            }
        }
        //getdeferral

        public void DaGetDeferral(deferral objdeferral)
        {
            try
            {
                msSQL = " select * from ocs_mst_tdeferral order by deferraltype_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<deferral_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employee.Add(new deferral_list
                        {
                            deferral_gid = (dr_datarow["deferraltype_gid"].ToString()),
                            deferral_code = (dr_datarow["deferral_code"].ToString()),
                            deferral_name = (dr_datarow["deferral_name"].ToString()),
                            criticallity = (dr_datarow["criticallity"].ToString()),
                            comments = (dr_datarow["comments"].ToString()),
                            deferraltype_gid = (dr_datarow["deferraltype_gid"].ToString()),
                        });
                    }
                    objdeferral.deferral_list = get_employee;
                }
                dt_datatable.Dispose();
                objdeferral.status = true;

            }
            catch (Exception ex)
            {
                objdeferral.status = false;
                objdeferral.message = ex.ToString();
            }
        }
        // GetDeferralStages
        public void DaGetDeferralStages(string deferral_gid, UploadDocumentname objfilename)
        {
            try
            {
                msSQL = " select a.approval_remarks,concat(c.user_code,'/',c.user_firstname,c.user_lastname) as approved_by, " +
                 " concat(e.user_code,'/',e.user_firstname,e.user_lastname) as approvalrequest_by, " +
                 " case when a.approval_status='ReOpen' then 'Pending' else a.approval_status end as approval_status, " +
                 " case when a.approval_status='Extend' then date_format(a.extended_date,'%d-%m-%Y')" +
                 " else 'N/A' end as extended_date" +
                 " from ocs_trn_tdeferralapprovalhistory a " +
                 " left join hrm_mst_temployee b on a.approved_by = b.employee_gid" +
                 " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                 " left join hrm_mst_temployee d on a.approvalrequest_by = d.employee_gid" +
                 " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                 " where a.deferral_gid = '" + deferral_gid + "' and a.approval_status<>'Pending' order by a.approval_date asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<DeferralstageList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (dr_datarow["approved_by"].ToString() == "")
                        {
                            get_filename.Add(new DeferralstageList
                            {
                                approval_remarks = (dr_datarow["approval_remarks"].ToString()),
                                approval_status = (dr_datarow["approval_status"].ToString()),
                                approved_by = (dr_datarow["approvalrequest_by"].ToString()),
                                extended_date = (dr_datarow["extended_date"].ToString()),
                            });
                        }
                        else if (dr_datarow["approvalrequest_by"].ToString() == "")
                        {
                            get_filename.Add(new DeferralstageList
                            {
                                approval_remarks = (dr_datarow["approval_remarks"].ToString()),
                                approval_status = (dr_datarow["approval_status"].ToString()),
                                approved_by = (dr_datarow["approved_by"].ToString()),
                                extended_date = (dr_datarow["extended_date"].ToString())
                            });
                        }
                    }
                    objfilename.stage_list = get_filename;
                    objfilename.status = true;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                objfilename.status = false;
                objfilename.message = ex.ToString();
            }

        }
        public void DaGetcaddoc(string deferral_gid, UploadDocumentname objfilename, UploadDocumentList lstvalues, string user_gid)
        {
            try
            {
                if (deferral_gid != null)
                {
                    msSQL = " select a.remarks,a.customer_remarks,a.criticallity,a.sanction_refno, a.sanction_date," +
                    " a.deferral_gid,a.record_id,case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name," +
                    " a.loanref_no,n.customer_urn,n.customer_code,a.deferral_catagory," +
                   " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date, " +
                    " date_format(a.due_date, '%d-%m-%Y') as extended_date, " +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status, " +
                    " a.customer_name,a.entity_name,a.branch_name,a.vertical_code, " +
                    " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
                    " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                    " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
                     " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name " +
                    " from ocs_trn_tdeferral a" +
                    " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid " +
                    " left join ocs_mst_tcustomer n on a.customer_gid=n.customer_gid" +
                    " where a.deferral_gid = '" + deferral_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {
                        objfilename.deferral_gid = objODBCDatareader["deferral_gid"].ToString();
                        objfilename.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                        objfilename.record_id = objODBCDatareader["record_id"].ToString();
                        objfilename.deferral_catagory = objODBCDatareader["deferral_catagory"].ToString();
                        objfilename.deferral_name = objODBCDatareader["deferral_name"].ToString();
                        objfilename.remarks = objODBCDatareader["remarks"].ToString();
                        objfilename.customer_remarks = objODBCDatareader["customer_remarks"].ToString();
                        objfilename.loanref_no = objODBCDatareader["loanref_no"].ToString();
                        objfilename.loan_title = objODBCDatareader["loanref_no"].ToString();
                        objfilename.approval_status = objODBCDatareader["approval_status"].ToString();
                        objfilename.customer_name = objODBCDatareader["customer_name"].ToString();
                        objfilename.zonal_name = objODBCDatareader["zonal_name"].ToString();
                        objfilename.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                        objfilename.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                        objfilename.credit_manager = objODBCDatareader["creditmgmt_name"].ToString();
                        objfilename.duedate = objODBCDatareader["due_date"].ToString();
                        objfilename.extended_date = objODBCDatareader["extended_date"].ToString();
                        objfilename.sanctiondate = objODBCDatareader["sanction_date"].ToString();
                        objfilename.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                        objfilename.customer_code = objODBCDatareader["customer_code"].ToString();
                        objfilename.customer_urn = objODBCDatareader["customer_urn"].ToString();
                        objfilename.vertical_code = objODBCDatareader["vertical_code"].ToString();
                        objfilename.entity_name = objODBCDatareader["entity_name"].ToString();
                        objfilename.branch_name = objODBCDatareader["branch_name"].ToString();
                        objfilename.criticallity = objODBCDatareader["criticallity"].ToString();
                    }
                    objODBCDatareader.Close();
                }               

                msSQL = " select a.file_name,a.caddocument_gid,a.path,date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date, " +
                     " date_format(a.created_date,'%h:%i:%s %p') as time,concat(c.user_code,'/',c.user_firstname,' ',c.user_lastname) as user_name, " +
                     " concat(a.file_name,' ','by',' ',concat(c.user_code,'/',c.user_firstname,' ',c.user_lastname),' ','on',' ',date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p')) as upload_by " +
                     " from ocs_trn_tcaddocument a" +
                     " left join adm_mst_tuser c on c.user_gid=a.created_by ";
                if (deferral_gid == null)
                {
                    msSQL += " where a.deferral_gid='" + user_gid + "'";
                }
                else
                {
                    msSQL += " where a.deferral_gid='" + deferral_gid + "'";
                }
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadDocumentList
                        {
                            filename = (dr_datarow["file_name"].ToString()),
                            id = (dr_datarow["caddocument_gid"].ToString()),
                            path = objcmnstorage.EncryptData(dr_datarow["path"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            time = (dr_datarow["time"].ToString()),
                            uploaded_by = (dr_datarow["user_name"].ToString()),
                            upload_by = (dr_datarow["upload_by"].ToString())
                        });
                    }
                    objfilename.filename_list = get_filename;
                }
                dt_datatable.Dispose();
                objfilename.status = true;
            }
            catch (Exception ex)
            {
                objfilename.status = false;
                objfilename.message = ex.ToString();
            }
        }
        // checker_list
        public void DaGetcheckerlist(string deferral_gid, UploadDocumentname lstvalues)
        {
            msSQL = " SELECT a.checker_status, " +
                    " case when a.checker_remarks is null then 'N/A' " +
                    " when a.checker_remarks = 'null' then 'N/A' else a.checker_remarks " +
                    " end as checker_remarks, " +
                    " case when a.checked_by is null then 'N/A' else " +
                    " concat('Checked By',' ',concat(c.user_code,'/',c.user_firstname,' ',c.user_lastname),' ','On',' ', " +
                    " date_format(a.checked_date,'%d-%m-%Y %h:%i:%s %p')) end as checked_by " +
                    " FROM ocs_trn_tcheckerapprovalhistory a" +
                    " left join hrm_mst_temployee b on a.checked_by=b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.deferral_gid='" + deferral_gid + "'" +
                    " order by a.checkerapprovalhistory_gid asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDeferral = new List<checker_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getDeferral.Add(new checker_list
                    {
                        checker_status = (dr_datarow["checker_status"].ToString()),
                        checker_remarks = (dr_datarow["checker_remarks"].ToString()),
                        checked_by = (dr_datarow["checked_by"].ToString()),
                    });
                }
                lstvalues.checker_list = getDeferral;
            }
            dt_datatable.Dispose();
            lstvalues.status = true;
        }
        // getApprove
        public void DaGetApprove(mdldeferralgetapproval values, string employee_gid)
        {
            try
            {
                msSQL = " update ocs_trn_tdeferralapproval set ";
                if (values.deferral_status == "Approval")
                {
                    msSQL += " approval_status='Approval Pending',";
                }
                else if (values.deferral_status == "Extension")
                {
                    msSQL += " approval_status='Extension Approval Pending',";
                }
                msSQL += " approvalrequest_by='" + employee_gid + "'," +
                  " approvalrequest_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                if (values.applied_remarks == null)
                {
                    msSQL += "approval_remarks='null'";
                }
                else
                {
                    msSQL += "approval_remarks='" + values.applied_remarks.Replace("'", "") + "'";
                }
                msSQL += " where deferral_gid='" + values.def_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGidREF = objcmnfunctions.GetMasterGID("DFAH");
                msSQL = " insert into ocs_trn_tdeferralapprovalhistory(" +
                       " deferralapprovalhistory_gid," +
                       " deferral_gid," +
                       " approval_status," +
                       " approval_remarks," +
                       " approvalrequest_by," +
                       " approvalrequest_date)" +
                       " values(" +
                       "'" + msGetGidREF + "'," +
                       "'" + values.def_gid + "',";
                if (values.deferral_status == "Approval")
                {
                    msSQL += "'Approval Pending',";
                }
                else if (values.deferral_status == "Extension")
                {
                    msSQL += "'Extension Approval Pending',";
                }
                if(values.applied_remarks==null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.applied_remarks.Replace("'", "") + "',";
                }
                msSQL += "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select * from ocs_trn_tdeferraldocument where deferral_gid='" + values.def_gid + "' and uploaded_by='rm'" +
                  " and deferralapprovalhistory_gid is null ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = " update ocs_trn_tdeferraldocument set deferralapprovalhistory_gid='" + msGetGidREF + "'" +
                                " where deferraldocument_gid='" + dr_datarow["deferraldocument_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = true;
                values.message = ex.Message.ToString();
            }

        }

        // deferralApprove
        public void DaPostDeferralApprove(mdldeferralgetapproval values, string employee_gid)
        {
            try
            {
                DateTime lsdate;
                if (values.deferral_status == "Extend")
                {
                    msSQL = " select date_format(due_date, '%m-%d-%Y') as due_date from ocs_trn_tdeferral where deferral_gid='" + values.def_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {
                        lsdate = Convert.ToDateTime(objODBCDatareader["due_date"]);

                        if (Convert.ToDateTime(objODBCDatareader["due_date"]) > Convert.ToDateTime(values.due_date))
                        {
                            values.status = false;
                            values.message = "failure";
                            return;
                        }
                    }
                    objODBCDatareader.Close();
                }

                msSQL = " update ocs_trn_tdeferralapproval " +
                    " set approval_status='" + values.deferral_status + "'," +
                    " extend_type='" + values.extend_type + "'," +
                    " approved_by='" + employee_gid + "'," +
                    " approval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                if(values.customer_remarks==null)
                {
                    msSQL += "customer_remarks='',";
                }
                else
                {
                    msSQL += " customer_remarks='" + values.customer_remarks.Replace("'","") + "',";
                }
                if (values.approval_remarks == null)
                {
                    msSQL += "approval_remarks='',";
                }
                else
                {
                    msSQL += " approval_remarks='" + values.approval_remarks.Replace("'", "") + "',";
                }
                if (Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    msSQL += "extended_date =null";
                }
                else
                {
                    msSQL += "extended_date ='" + Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd") + "'";
                }
                msSQL += " where deferral_gid='" + values.def_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                {
                    msSQL = " select * from ocs_trn_tdeferralapprovalhistory " +
                            " where deferral_gid='" + values.def_gid + "' and approval_status='Extend'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count == 0)
                    {
                        msSQL = " UPDATE ocs_trn_tdeferral " +
                               " set old_duedate = due_date, new_duedate = due_date where deferral_gid='" + values.def_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    };
                    dt_datatable.Dispose();
                    msSQL = " UPDATE ocs_trn_tdeferral " +
                             " set due_date = '" + Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd 00:00:00") + "'  where deferral_gid='" + values.def_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " UPDATE ocs_trn_tdeferral2loan " +
                             " set due_date = '" + Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd 00:00:00") + "'  where deferral_gid='" + values.def_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }


                msSQL = " UPDATE ocs_trn_tdeferral " +
                       " set deferral_status = 'Expired' where deferral_gid='" + values.def_gid + "' and  DATEDIFF(due_date, now())< 0 ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral " +
                       " set deferral_status = 'Live' where deferral_gid='" + values.def_gid + "' and DATEDIFF(due_date, now())> 0 ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral" +
                       " set aging = DATEDIFF(now(), due_date)" +
                       " where deferral_gid='" + values.def_gid + "' and old_duedate is null and DATEDIFF(due_date, now())< 0";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral" +
                      " set aging = DATEDIFF(now(), new_duedate)" +
                      " where deferral_gid='" + values.def_gid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())< 0 ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral" +
                       " set aging='0' where deferral_gid='" + values.def_gid + "' and old_duedate is null and DATEDIFF(due_date, now())> 0";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "  UPDATE ocs_trn_tdeferral" +
                        " set aging='0' where deferral_gid='" + values.def_gid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())> 0";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if(values.extend_type == "On Submission")
                {
                    msSQL = " UPDATE ocs_trn_tdeferral set aging='0', new_duedate='"+ Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd 00:00:00") + "' where deferral_gid='" + values.def_gid + "' and old_duedate is not null ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " UPDATE ocs_trn_tdeferralapproval set approval_status='Re-track' where deferral_gid='" + values.def_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msGetGid = objcmnfunctions.GetMasterGID("DFAH");
                msSQL = " insert into ocs_trn_tdeferralapprovalhistory(" +
                       " deferralapprovalhistory_gid," +
                       " deferral_gid," +
                       " approval_status," +
                       " extended_date , " +
                       " approved_by," +
                       " approval_remarks," +
                       " customer_remarks, " +
                       " extend_type," +
                       " approval_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + values.def_gid + "'," +
                       "'" + values.deferral_status + "',";
                if (Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.due_date).ToString("yyyy-MM-dd") + "',";
                }
                msSQL += "'" + employee_gid + "',";
                if(values.approval_remarks==null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.approval_remarks.Replace("'", "") + "',";
                }
                if (values.customer_remarks == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.customer_remarks.Replace("'", "") + "',";
                }
                msSQL+= "'" + values.extend_type + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "update ocs_trn_tdeferral set extend_type = '" + values.extend_type + "',";
                if (values.approval_remarks == null)
                {
                    msSQL += "remarks='',";
                }
                else
                {
                    msSQL += "remarks ='" + values.approval_remarks.Replace("'", "") + "',";
                }
                if (values.customer_remarks == null)
                {
                    msSQL += "customer_remarks=''";
                }
                else
                {
                    msSQL += "customer_remarks ='" + values.customer_remarks.Replace("'", "") + "'";
                }
                msSQL+=" where deferral_gid='" + values.def_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tdeferral2loan set extend_type = '" + values.extend_type + "',";
                if (values.customer_remarks == null)
                {
                    msSQL += "customer_remarks=''";
                }
                else
                {
                    msSQL += "customer_remarks ='" + values.customer_remarks.Replace("'", "") + "'";
                }
                msSQL+= " where deferral_gid='" + values.def_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select * from ocs_trn_tdeferraldocument where deferral_gid='" + values.def_gid + "' and uploaded_by='cad'" +
                          " and deferralapprovalhistory_gid is null ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = " update ocs_trn_tdeferraldocument set deferralapprovalhistory_gid='" + msGetGid + "'" +
                                " where deferraldocument_gid='" + dr_datarow["deferraldocument_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                dt_datatable.Dispose();
                if (values.deferral_status == "Closed")
                {
                    msSQL = " select a.customeralert_gid from ocs_trn_thistorycustomeralert a " +
                            " where a.deferral_gid = '" + values.def_gid + "'";
                    lscustomeralert_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (lscustomeralert_gid != "")
                    {
                        msSQL = " update ocs_trn_thistorycustomeralert set penalityalert_status='N' " +
                                " where deferral_gid = '" + values.def_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " select count(customeralert_gid) from ocs_trn_thistorycustomeralert " +
                                " where customeralert_gid='" + lscustomeralert_gid + "'";
                        lsalert_totalcount = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select count(customeralert_gid) from ocs_trn_thistorycustomeralert " +
                             " where customeralert_gid='" + lscustomeralert_gid + "' and penalityalert_status='N'";
                        lspenality_deferralcount = objdbconn.GetExecuteScalar(msSQL);

                        if (lsalert_totalcount == lspenality_deferralcount)
                        {
                            mailalert objvalues = new mailalert();
                            objdaPenalityAlert.DaPostendpPenalityAlert(employee_gid, lscustomeralert_gid, objvalues);
                            if (values.status == true)
                            {

                                msSQL = " update ocs_trn_tpenalityalerthistory set autostop_penal='Y' " +
                                        " where customeralert_gid='" + lscustomeralert_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                }
                if (values.deferral_status == "Closed")
                {
                    msSQL = " select sanction_refno from ocs_trn_tdeferral"+
                            " where deferral_gid = '" + values.def_gid + "' and tracking_type = 'Deferral' and deferral_name = 'Original Documents'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL );
                    if (objODBCDatareader .HasRows ==true)
                    {
                        var lssanction_refno = objODBCDatareader["sanction_refno"].ToString();
                        objODBCDatareader.Close();

                        msSQL = " select distinct a.sanction_gid from ids_trn_tsanctiondocumentdtls a" +
                                " left join ocs_mst_tcustomer2sanction b on a.sanction_gid=b.customer2sanction_gid" +
                                " where sanction_refno='" + lssanction_refno + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            var lssanction_gid = objODBCDatareader["sanction_gid"].ToString();
                            objODBCDatareader.Close();
                            DaPostMailSenttoMaker(lssanction_gid, values.def_gid);
                        }
                        else
                        {
                            objODBCDatareader.Close();
                        }
                    }
                    else
                    {
                        objODBCDatareader.Close();
                    }
                  
                }
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }            
        }
        //checker verification
        public void DaPostCheckerVerify(deferralsupdate values, string employee_gid)
        {
            msSQL = " update ocs_trn_tdeferralapproval set ";
            if (values.deferral_status == "Approve")
            {
                msSQL += " checker_status='Approved',";
            }
            else if (values.deferral_status == "PushBack")
            {
                msSQL += " checker_status='PushBack'";
            }
            else if (values.deferral_status == "Close")
            {
                msSQL += " checker_status='Closed',";
            }
            if (values.checker_remarks == null)
            {
                msSQL += "checker_remarks='null',";
            }
            else
            {
                msSQL += "checker_remarks='" + values.checker_remarks.Replace("'", "") + "',";
            }
            msSQL += " checked_by='" + employee_gid + "'," +
                     " checked_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            msSQL += " where deferral_gid='" + values.def_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tdeferral set ";
            if (values.deferral_status == "Approve")
            {
                msSQL += " checker_status='Approved'";
            }
            else if (values.deferral_status == "PushBack")
            {
                msSQL += " checker_status='PushBack'";
            }
            else if (values.deferral_status == "Close")
            {
                msSQL += " checker_status='Closed'";
            }
            msSQL += " where deferral_gid='" + values.def_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("CKAH");
            msSQL = " insert into ocs_trn_tcheckerapprovalhistory(" +
                   " checkerapprovalhistory_gid," +
                   " deferral_gid," +
                   " checker_status," +
                   " checker_remarks," +
                   " checked_by," +
                   " checked_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.def_gid + "',";
            if (values.deferral_status == "Approve")
            {
                msSQL += "'Approved',";
            }
            else if (values.deferral_status == "PushBack")
            {
                msSQL += "'PushBack',";
            }
            else if (values.deferral_status == "Close")
            {
                msSQL += "'Closed',";
            }
            if (values.checker_remarks == null)
            {
                msSQL += "'null',";
            }
            else
            {
                msSQL += "'" + values.checker_remarks.Replace("'", "") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                if (values.deferral_status == "PushBack")
                {
                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {
                        lspop_mail = objODBCDatareader["pop_username"].ToString();
                        lspop_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select b.employee_emailid,a.record_id,a.customer_name, " +
                            " concat('Checked By',' ',concat(e.user_code,'/',e.user_firstname,' ',e.user_lastname),' ','On',' ', " +
                            " date_format(c.checked_date, '%d-%m-%Y %h:%i:%s %p')) as checked_by " +
                            " from ocs_trn_tdeferral a " +
                            " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                            " left join ocs_trn_tdeferralapproval c on c.deferral_gid=a.deferral_gid " +
                            " left join hrm_mst_temployee d on d.employee_gid=c.checked_by " +
                            " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                            " where a.deferral_gid='" + values.def_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {
                        lstomail_id = objODBCDatareader["employee_emailid"].ToString();
                        lsrecord_id = objODBCDatareader["record_id"].ToString();
                        lscustomer_name = objODBCDatareader["customer_name"].ToString();
                        lschecked_by = objODBCDatareader["checked_by"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select group_concat(employee_mailid) as cc_mail from ocs_trn_tmailcclist where mailtrigger_function='LSA-Alert'";
                    strRes = objdbconn.GetExecuteScalar(msSQL);

                    body = "<span style=\"font-family: Times New Roman;font-size:12pt;\" >";
                    body = body + "Dear Sir/Madam,  <br />";
                    body = body + "<br />";

                    body = body + "The below Deferral has been pushback to you. Kindly do the Needful and resubmit for Approval.</span>";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<table style='border:1px solid #060606;' style=\"font-family: Times New Roman;font-size:12pt;\" border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + ">";
                    body = body + "<tr style='color: #fff;background: #00008B;text-align:center;'> ";
                    body = body + "<td style=\"width:10%;\"><b> Record ID </b></td> ";
                    body = body + "<td style=\"width:10%;\"><b> Customer </b> </td>";
                    body = body + "<td style=\"width:10%;\"><b> Checker Status </b> </td>";
                    body = body + "<td style=\"width:30%;\"> <b> Checker Remarks </b> </td>";
                    body = body + "<td style=\"width:40%;\"> <b> Checked By </b> </td>";
                    body = body + "</tr>";
                    
                    body = body + "<tr style='text-align:center;'>";
                    body = body + "<td> " + lsrecord_id + "</td>";
                    body = body + "<td> " + lscustomer_name + "</td>";
                    body = body + "<td> " + "PushBack" + "</td>";
                    body = body + "<td> " + values.checker_remarks + "</td>";
                    body = body + "<td> " + lschecked_by + "</td>";
                    body = body + " </tr>";

                    body = body + "</table>";
                    body = body + "<br /><span style=\"font-family: Times New Roman;font-size:12pt;\" >";
                    //body = body + "The below Deferral has been pushback to you. Kindly do the Needful and resubmit for Approval.";
                    body = body + "<br/>";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanking You,";
                    body = body + "<br/>";
                    body = body + "<br/>";
                    body = body + "Yours Sincerely," + "<br/>";
                    body = body + " Samunnati Financial Intermediation & Services Pvt Ltd </span>";

                    mailflag = objcmnfunctions.SendSMTP2(lspop_mail, lspop_password, lstomail_id, " Checker Verify Status ", body, strRes, "", "");
                    values.status = true;
                    values.message = "success";
                }
                else if (values.deferral_status == "Close")
                {

                    msSQL = " update ocs_trn_tdeferralapproval " +
                   " set approval_status='Closed'," +
                   " approved_by='" + employee_gid + "'," +
                   " approval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    if (values.customer_remarks == null)
                    {
                        msSQL += "customer_remarks='',";
                    }
                    else
                    {
                        msSQL += "customer_remarks='" + values.customer_remarks.Replace("'", "") + "',";
                    }
                    if (values.approval_remarks == null)
                    {
                        msSQL += "approval_remarks=''";
                    }
                    else
                    {
                        msSQL += "approval_remarks='" + values.approval_remarks.Replace("'", "") + "'";
                    }
                   msSQL+=" where deferral_gid='" + values.def_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGidREF = objcmnfunctions.GetMasterGID("DFAH");
                    msSQL = " insert into ocs_trn_tdeferralapprovalhistory(" +
                           " deferralapprovalhistory_gid," +
                           " deferral_gid," +
                           " approval_status," +
                           " approved_by," +
                           " approval_remarks," +
                           " customer_remarks, " +
                           " approval_date)" +
                           " values(" +
                           "'" + msGetGidREF + "'," +
                           "'" + values.def_gid + "'," +
                           "'Closed'," +
                           "'" + employee_gid + "',";
                    if (values.approval_remarks == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.approval_remarks.Replace("'", "") + "',";
                    }
                    if (values.customer_remarks == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.customer_remarks.Replace("'", "") + "',";
                    }
                  msSQL+="'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_trn_tcheckerapprovalhistory set deferralapprovalhistory_gid= '" + msGetGidREF + "' where checkerapprovalhistory_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_trn_tdeferral set";
                    if (values.customer_remarks == null)
                    {
                        msSQL += " customer_remarks=''";
                    }
                    else
                    {
                        msSQL += " customer_remarks='" + values.customer_remarks.Replace("'", "") + "'";
                    }
                    msSQL+=" where deferral_gid='" + values.def_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_trn_tdeferral2loan set ";
                    if (values.customer_remarks == null)
                    {
                        msSQL += " customer_remarks=''";
                    }
                    else
                    {
                        msSQL += " customer_remarks='" + values.customer_remarks.Replace("'", "") + "'";
                    }
                   msSQL+=" where deferral_gid='" + values.def_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select a.customeralert_gid from ocs_trn_thistorycustomeralert a " +
                            " where a.deferral_gid = '" + values.def_gid + "'";
                    lscustomeralert_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (lscustomeralert_gid != "")
                    {
                        msSQL = " update ocs_trn_thistorycustomeralert set penalityalert_status='N' " +
                                " where deferral_gid = '" + values.def_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " select count(customeralert_gid) from ocs_trn_thistorycustomeralert " +
                                " where customeralert_gid='" + lscustomeralert_gid + "'";
                        lsalert_totalcount = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select count(customeralert_gid) from ocs_trn_thistorycustomeralert " +
                             " where customeralert_gid='" + lscustomeralert_gid + "' and penalityalert_status='N'";
                        lspenality_deferralcount = objdbconn.GetExecuteScalar(msSQL);

                        if (lsalert_totalcount == lspenality_deferralcount)
                        {
                            mailalert objvalues = new mailalert();
                            objdaPenalityAlert.DaPostendpPenalityAlert(employee_gid, lscustomeralert_gid, objvalues);
                            if (values.status == true)
                            {

                                msSQL = " update ocs_trn_tpenalityalerthistory set autostop_penal='Y' " +
                                        " where customeralert_gid='" + lscustomeralert_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                   
                        msSQL = " select sanction_refno from ocs_trn_tdeferral" +
                                " where deferral_gid = '" + values.def_gid + "' and tracking_type = 'Deferral' and deferral_name = 'Original Documents'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            var lssanction_refno = objODBCDatareader["sanction_refno"].ToString();
                            objODBCDatareader.Close();

                            msSQL = " select distinct a.sanction_gid from ids_trn_tsanctiondocumentdtls a" +
                                    " left join ocs_mst_tcustomer2sanction b on a.sanction_gid=b.customer2sanction_gid" +
                                    " where sanction_refno='" + lssanction_refno + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                var lssanction_gid = objODBCDatareader["sanction_gid"].ToString();
                                objODBCDatareader.Close();
                                DaPostMailSenttoMaker(lssanction_gid, values.def_gid);
                            }
                            else
                            {
                                objODBCDatareader.Close();
                            }
                        }
                        else
                        {
                            objODBCDatareader.Close();
                        }
                        
                    values.status = true;
                    values.message = "success";
                }
                else if (values.deferral_status == "Approve")
                {
                    try
                    {
                        msSQL = " select old_duedate from ocs_trn_tdeferral" +
                            " where deferral_gid='" + values.def_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows)
                        {
                            objODBCDatareader.Read();
                            if (objODBCDatareader["old_duedate"].ToString() == "")
                            {
                                objODBCDatareader.Close();
                                msSQL = " update ocs_trn_tdeferral set" +
                                       " due_date='" + Convert.ToDateTime(values.duedate).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                       " where deferral_gid='" + values.def_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                            else
                            {
                                objODBCDatareader.Close();
                                msSQL = " update ocs_trn_tdeferral set" +
                                       " old_duedate='" + Convert.ToDateTime(values.duedate).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                       " where deferral_gid='" + values.def_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                            objODBCDatareader.Close();
                        }
                        else
                        {
                            objODBCDatareader.Close();
                        }

                        msSQL = " update ocs_trn_tdeferral2loan set " +
                                " deferral_catagory='" + values.category_gid + "',";
                        if (values.remarks == null)
                        {
                            msSQL += " remarks='',";
                        }
                        else
                        {
                            msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
                        }
                        if (values.customerremarks == null)
                        {
                            msSQL += " customer_remarks='',";
                        }
                        else
                        {
                            msSQL += " customer_remarks='" + values.customerremarks.Replace("'", "") + "',";
                        }
                       msSQL+= " tracking_type='" + values.tracking_type + "'," +
                                " deferraltype_gid='" + values.deferraltype_gid + "'," +
                                " deferral_name='" + values.deferraltype_name + "'," +
                                " covenanttype_name='" + values.covenanttype_name + "'," +
                                " customer_gid='" + values.customer_gid + "'," +
                                " due_date='" + Convert.ToDateTime(values.duedate).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " criticallity='" + values.criticallity + "'," +
                                " vertical_gid='" + values.vertical_gid + "'," +
                                " vertical_code='" + values.vertical_code + "'," +
                                " entity_gid='" + values.entity_gid + "'," +
                                " entity_name='" + values.entity_name + "'," +
                                " branch_gid='" + values.branch_gid + "'," +
                                " branch_name='" + values.branch_name + "'," +
                                " updated_by='" + employee_gid + "'," +
                                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " covenanttype_gid='" + values.covenanttype_gid + "'" +
                                " where deferral_gid='" + values.def_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tdeferral set " +
                            " tracking_type='" + values.tracking_type + "'," +
                            " deferraltype_gid='" + values.deferraltype_gid + "'," +
                            " deferral_name='" + values.deferraltype_name + "'," +
                            " deferral_catagory='" + values.category_gid + "',";
                        if (values.remarks == null)
                        {
                            msSQL += " remarks='',";
                        }
                        else
                        {
                            msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
                        }
                        if (values.customerremarks == null)
                        {
                            msSQL += " customer_remarks='',";
                        }
                        else
                        {
                            msSQL += " customer_remarks='" + values.customerremarks.Replace("'", "") + "',";
                        }
                        msSQL+= " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " covenanttype_gid='" + values.covenanttype_gid + "'," +
                            " covenanttype_name='" + values.covenanttype_name + "'," +
                            " customer_gid='" + values.customer_gid + "'," +
                            " customer_name='" + values.customer_name + "'," +
                            " zonal_gid='" + values.zonalhead_gid + "'," +
                            " zonal_name='" + values.zonalhead_name + "'," +
                            " businesshead_gid='" + values.businesshead_gid + "'," +
                            " businesshead_name='" + values.businesshead_name + "'," +
                            " relationshipmgmt_gid='" + values.relationmgr_gid + "'," +
                            " relationshipmgmt_name='" + values.relationmgr_name + "'," +
                            " cluster_manager_gid='" + values.clustermgr_gid + "'," +
                            " cluster_manager_name='" + values.clusterhead_name + "'," +
                            " creditmanager_gid='" + values.creditmgr_gid + "'," +
                            " creditmgmt_name='" + values.creditmgr_name + "'," +
                            " criticallity='" + values.criticallity + "'," +
                            " vertical_gid='" + values.vertical_gid + "'," +
                            " vertical_code='" + values.vertical_code + "'," +
                            " entity_gid='" + values.entity_gid + "'," +
                            " entity_name='" + values.entity_name + "'," +
                            " branch_gid='" + values.branch_gid + "'," +
                            " branch_name='" + values.branch_name + "'" +
                            " where deferral_gid='" + values.def_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " UPDATE ocs_trn_tdeferral " +
                        " set deferral_status = 'Expired' where deferral_gid='" + values.def_gid + "' and  DATEDIFF(due_date, now())< 0 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " UPDATE ocs_trn_tdeferral " +
                               " set deferral_status = 'Live' where deferral_gid='" + values.def_gid + "' and DATEDIFF(due_date, now())> 0 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " UPDATE ocs_trn_tdeferral" +
                               " set aging = DATEDIFF(now(), due_date)" +
                               " where deferral_gid='" + values.def_gid + "' and old_duedate is null and DATEDIFF(due_date, now())< 0";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " UPDATE ocs_trn_tdeferral" +
                              " set aging = DATEDIFF(now(), new_duedate)" +
                              " where deferral_gid='" + values.def_gid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())< 0 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " UPDATE ocs_trn_tdeferral" +
                               " set aging='0' where deferral_gid='" + values.def_gid + "' and old_duedate is null and DATEDIFF(due_date, now())> 0";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "  UPDATE ocs_trn_tdeferral" +
                                " set aging='0' where deferral_gid='" + values.def_gid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())> 0";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                      

                    }
                    catch
                    {
                        msSQL = " update ocs_trn_tdeferral2loan set " +
                           " deferral_catagory='" + values.category_gid + "',";
                        if (values.remarks == null)
                        {
                            msSQL += " remarks='',";
                        }
                        else
                        {
                            msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
                        }
                        if (values.customerremarks == null)
                        {
                            msSQL += " customer_remarks='',";
                        }
                        else
                        {
                            msSQL += " customer_remarks='" + values.customerremarks.Replace("'", "") + "',";
                        }
                       msSQL+= " tracking_type='" + values.tracking_type + "'," +
                           " deferraltype_gid='" + values.deferraltype_gid + "'," +
                           " deferral_name='" + values.deferraltype_name + "'," +
                           " covenanttype_name='" + values.covenanttype_name + "'," +
                           " customer_gid='" + values.customer_gid + "'," +
                           " criticallity='" + values.criticallity + "'," +
                           " vertical_gid='" + values.vertical_gid + "'," +
                           " vertical_code='" + values.vertical_code + "'," +
                           " entity_gid='" + values.entity_gid + "'," +
                           " entity_name='" + values.entity_name + "'," +
                           " branch_gid='" + values.branch_gid + "'," +
                           " branch_name='" + values.branch_name + "'," +
                           " updated_by='" + employee_gid + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           " covenanttype_gid='" + values.covenanttype_gid + "'" +
                           " where deferral_gid='" + values.def_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tdeferral set " +
                            " tracking_type='" + values.tracking_type + "'," +
                            " deferraltype_gid='" + values.deferraltype_gid + "'," +
                            " deferral_name='" + values.deferraltype_name + "'," +
                            " deferral_catagory='" + values.category_gid + "',";
                        if (values.remarks == null)
                        {
                            msSQL += " remarks='',";
                        }
                        else
                        {
                            msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
                        }
                        if (values.customerremarks == null)
                        {
                            msSQL += " customer_remarks='',";
                        }
                        else
                        {
                            msSQL += " customer_remarks='" + values.customerremarks.Replace("'", "") + "',";
                        }
                       msSQL+= " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " covenanttype_gid='" + values.covenanttype_gid + "'," +
                            " covenanttype_name='" + values.covenanttype_name + "'," +
                            " customer_gid='" + values.customer_gid + "'," +
                            " customer_name='" + values.customer_name + "'," +
                            " zonal_gid='" + values.zonalhead_gid + "'," +
                            " businesshead_gid='" + values.businesshead_gid + "'," +
                            " businesshead_name='" + values.businesshead_name + "'," +
                            " relationshipmgmt_gid='" + values.relationmgr_gid + "'," +
                            " relationshipmgmt_name='" + values.relationmgr_name + "'," +
                            " cluster_manager_gid='" + values.clustermgr_gid + "'," +
                            " cluster_manager_name='" + values.clusterhead_name + "'," +
                             " creditmanager_gid='" + values.creditmgr_gid + "'," +
                            " creditmgmt_name='" + values.creditmgr_name + "'," +
                            " zonal_name='" + values.zonalhead_name + "'," +
                            " criticallity='" + values.criticallity + "'," +
                            " vertical_gid='" + values.vertical_gid + "'," +
                            " vertical_code='" + values.vertical_code + "'," +
                            " entity_gid='" + values.entity_gid + "'," +
                            " entity_name='" + values.entity_name + "'," +
                            " branch_gid='" + values.branch_gid + "'," +
                            " branch_name='" + values.branch_name + "'" +
                            " where deferral_gid='" + values.def_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " UPDATE ocs_trn_tdeferral " +
                      " set deferral_status = 'Expired' where deferral_gid='" + values.def_gid + "' and  DATEDIFF(due_date, now())< 0 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " UPDATE ocs_trn_tdeferral " +
                               " set deferral_status = 'Live' where deferral_gid='" + values.def_gid + "' and DATEDIFF(due_date, now())> 0 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " UPDATE ocs_trn_tdeferral" +
                               " set aging = DATEDIFF(now(), due_date)" +
                               " where deferral_gid='" + values.def_gid + "' and old_duedate is null and DATEDIFF(due_date, now())< 0";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " UPDATE ocs_trn_tdeferral" +
                              " set aging = DATEDIFF(now(), new_duedate)" +
                              " where deferral_gid='" + values.def_gid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())< 0 ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " UPDATE ocs_trn_tdeferral" +
                               " set aging='0' where deferral_gid='" + values.def_gid + "' and old_duedate is null and DATEDIFF(due_date, now())> 0";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "  UPDATE ocs_trn_tdeferral" +
                                " set aging='0' where deferral_gid='" + values.def_gid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())> 0";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        

                    }
                    values.status = true;
                    values.message = "success";
                }
            }
            else
            {
                values.status = false;
                values.message = "failure";
            }
        }
        // deferraltransfer

        public result DaPostMailSenttoMaker(string lssanction_gid, string lsdeferral_gid)
        {
            result objResult = new result();
            try
            {
                string frommail_id, ls_server, ls_username, ls_password, tomail_id, ccmail_id;
                int ls_port;
                string[] to;
                string[] cc;
                string lscustomer_name = string.Empty;
                string lssanction_refno = string.Empty;
                string lsrecord_id = string.Empty;
                string lsremarks = string.Empty;
                string lscustomer_remarks = string.Empty;

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    frommail_id = objODBCDatareader["company_mail"].ToString();
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();
                    objODBCDatareader.Close();
                }
                else
                {
                    ls_username = "";
                    ls_port = 0;
                    ls_server = "";
                    ls_password = "";
                    objODBCDatareader.Close();
                }

                msSQL = " SELECT customer_name,record_id,sanction_refno,remarks,customer_remarks" +
                        " FROM ocs_trn_tdeferral WHERE deferral_gid = '" + lsdeferral_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    lscustomer_name = objODBCDatareader["customer_name"].ToString();
                    lsrecord_id = objODBCDatareader["record_id"].ToString();
                    lssanction_refno = objODBCDatareader["sanction_refno"].ToString();
                    lsremarks = objODBCDatareader["remarks"].ToString();
                    lscustomer_remarks = objODBCDatareader["customer_remarks"].ToString();
                }
                objODBCDatareader.Close();

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);

                msSQL = " select group_concat( distinct c.employee_emailid)" +
                        " from ids_trn_tsanctiondocumentdtls a" +
                        " left join adm_mst_tuser b on a.maker_gid = b.user_gid" +
                        " left join hrm_mst_temployee c on b.user_gid = c.user_gid" +
                        " where a.sanction_gid='" + lssanction_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);
                if (tomail_id != "" && tomail_id != null)
                {
                    to = (tomail_id).Split(',');
                    if (to.Length > 0)
                    {
                        foreach (string toEmail in to)
                        {
                            message.To.Add(new MailAddress(toEmail));
                        }
                    }
                }

                msSQL = "select group_concat(employee_mailid) as cc_mail from ocs_trn_tmailcclist where mailtrigger_function='ECMS-Original Doc'";
                ccmail_id = objdbconn.GetExecuteScalar(msSQL);
                if (ccmail_id != "" && ccmail_id != null)
                {
                    cc = (ccmail_id).Split(',');
                    if (cc.Length > 0)
                    {
                        foreach (string ccEmail in cc)
                        {
                            message.CC.Add(new MailAddress(ccEmail));
                        }
                    }
                }

                message.Subject = "REG : DTS Deferral Original Document - Closed";
                message.IsBodyHtml = true;
                body = body + "Dear Sir/Madam, <br><br>";
                body = body + "CAD Physical Document Verification is Pending for the Customer <b>" + lscustomer_name + "</b> with Sanction Ref.No. of <b>" + lssanction_refno + "</b><br><br>";
                body = body + "Original Documents has been received & the status updated as closed on Dt.<b>" + DateTime.Now.ToString("dd-MM-yyyy") + "</b> in DTS Deferral Tracking for the <b>Record ID: " + lsrecord_id + "</b> with the below Remarks,<br><br>";
                body = body + "Internal Remarks : " + lsremarks + "<br><br>";
                body = body + "Customer Remarks : " + lscustomer_remarks + "<br><br>";
                body = body + "Please do the necessary verification immediately and confirm.<br><br>";
                body = body + "<br /><br/>";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox " + "<br/>";
                body = body + "<br/>";
                message.Body = body;
                smtp.Port = ls_port;
                smtp.Host = ls_server;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                objResult.status = true;
                objResult.message = "Mail Sent";
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.Message;
                return objResult;
            }

        }
        public void DaGetDeferralTransfer(mdldeferralgetapproval values, string employee_gid)
        {

            var count = 0;
            foreach (string i in values.deferral_gid)
            {

                msSQL = " SELECT relationshipmgmt_gid from ocs_trn_tdeferral " +
                        " where deferral_gid='" + i + "'";
                var relationshipmgmt_gid = objdbconn.GetExecuteScalar(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("DFTR");
                msSQL = " insert into ocs_trn_tdeferraltransfer(" +
                       " deferraltransfer_gid," +
                       " deferral_gid," +
                       " transferred_from," +
                       " transferred_to," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + i + "'," +
                       "'" + relationshipmgmt_gid + "'," +
                       "'" + values.employee_gid + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " SELECT concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) as employee_name from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where b.employee_gid='" + values.employee_gid + "'";
                var relationshipmgmt_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " update ocs_trn_tdeferral a " +
                        " set a.relationshipmgmt_gid='" + values.employee_gid + "'," +
                        " a.relationshipmgmt_name='" + relationshipmgmt_name + "'" +
                        " where a.deferral_gid='" + i + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                count = count + 1;
            }

            if (count == 0)
            {
                values.status = false;
                values.message = "failure";
            }
            else
            {
                values.status = true;
                values.message = "success";
            }
        }
        //BulkVerifyByChecker
        public void DaGetCheckerBulkVerify(mdldeferralgetapproval values, string employee_gid)
        {
            foreach (string i in values.deferral_gid)
            {
                msSQL = " update ocs_trn_tdeferralapproval set ";
                if (values.deferral_status == "Approve")
                {
                    msSQL += " checker_status='Approved',";
                }
                else if (values.deferral_status == "PushBack")
                {
                    msSQL += " checker_status='PushBack'";
                }
                else if (values.deferral_status == "Close")
                {
                    msSQL += " checker_status='Closed',";
                }
                if (values.checker_remarks == null)
                {
                    msSQL += "checker_remarks='null',";
                }
                else
                {
                    msSQL += "checker_remarks='" + values.checker_remarks.Replace("'", "") + "',";
                }
                msSQL += " checked_by='" + employee_gid + "'," +
                  " checked_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                msSQL += " where deferral_gid='" + i + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdeferral set ";
                if (values.deferral_status == "Approve")
                {
                    msSQL += " checker_status='Approved'";
                }
                else if (values.deferral_status == "PushBack")
                {
                    msSQL += " checker_status='PushBack'";
                }
                else if (values.deferral_status == "Close")
                {
                    msSQL += " checker_status='Closed'";
                }
                msSQL += " where deferral_gid='" + i + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("CKAH");
                msSQL = " insert into ocs_trn_tcheckerapprovalhistory(" +
                       " checkerapprovalhistory_gid," +
                       " deferral_gid," +
                       " checker_status," +
                       " checker_remarks," +
                       " checked_by," +
                       " checked_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + i + "',";
                if (values.deferral_status == "Approve")
                {
                    msSQL += "'Approved',";
                }
                else if (values.deferral_status == "PushBack")
                {
                    msSQL += "'PushBack',";
                }
                else if (values.deferral_status == "Close")
                {
                    msSQL += "'Closed',";
                }
                if (values.checker_remarks == null)
                {
                    msSQL += "'null',";
                }
                else
                {
                    msSQL += "'" + values.checker_remarks.Replace("'", "") + "',";
                }
                msSQL += "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.deferral_status == "Close")
                {
                    msSQL = " update ocs_trn_tdeferralapproval " +
                     " set approval_status='Closed'," +
                     " approved_by='" + employee_gid + "'," +
                     " approval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    if (values.customer_remarks == null)
                    {
                        msSQL += "customer_remarks='',";
                    }
                    else
                    {
                        msSQL += "customer_remarks='" + values.customer_remarks.Replace("'", "") + "',";
                    }
                    if (values.approval_remarks == null)
                    {
                        msSQL += "approval_remarks=''";
                    }
                    else
                    {
                        msSQL += "approval_remarks='" + values.approval_remarks.Replace("'", "") + "'";
                    }
                   msSQL+=  " where deferral_gid='" + i + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGidREF = objcmnfunctions.GetMasterGID("DFAH");
                    msSQL = " insert into ocs_trn_tdeferralapprovalhistory(" +
                           " deferralapprovalhistory_gid," +
                           " deferral_gid," +
                           " approval_status," +
                           " approved_by," +
                           " approval_remarks," +
                           " customer_remarks, " +
                           " approval_date)" +
                           " values(" +
                           "'" + msGetGidREF + "'," +
                           "'" + i + "'," +
                           "'Closed'," +
                           "'" + employee_gid + "'," ;
                     if (values.approval_remarks == null)
                    {
                        msSQL += "''";
                    }
                    else
                    {
                        msSQL += "'" + values.approval_remarks.Replace("'", "") + "'";
                    }
                    if (values.customer_remarks == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.customer_remarks.Replace("'", "") + "',";
                    }
                          msSQL += "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_trn_tcheckerapprovalhistory set deferralapprovalhistory_gid= '" + msGetGidREF + "' where checkerapprovalhistory_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_trn_tdeferral set ";
                    if (values.customer_remarks == null)
                    {
                        msSQL += "customer_remarks=''";
                    }
                    else
                    {
                        msSQL += "customer_remarks='" + values.customer_remarks.Replace("'", "") + "'";
                    }

                   msSQL+=" where deferral_gid='" + i + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_trn_tdeferral2loan set";
                    if (values.customer_remarks == null)
                    {
                        msSQL += " customer_remarks=''";
                    }
                    else
                    {
                        msSQL += " customer_remarks='" + values.customer_remarks.Replace("'", "") + "'";
                    }
                  msSQL+=" where deferral_gid='" + i + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select a.customeralert_gid from ocs_trn_thistorycustomeralert a " +
                                              " where a.deferral_gid = '" + i + "'";
                    lscustomeralert_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (lscustomeralert_gid != "")
                    {
                        msSQL = " update ocs_trn_thistorycustomeralert set penalityalert_status='N' " +
                                " where deferral_gid = '" + i + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " select count(customeralert_gid) from ocs_trn_thistorycustomeralert " +
                                " where customeralert_gid='" + lscustomeralert_gid + "'";
                        lsalert_totalcount = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select count(customeralert_gid) from ocs_trn_thistorycustomeralert " +
                             " where customeralert_gid='" + lscustomeralert_gid + "' and penalityalert_status='N'";
                        lspenality_deferralcount = objdbconn.GetExecuteScalar(msSQL);

                        if (lsalert_totalcount == lspenality_deferralcount)
                        {
                            mailalert objvalues = new mailalert();
                            objdaPenalityAlert.DaPostendpPenalityAlert(employee_gid, lscustomeralert_gid, objvalues);
                            if (values.status == true)
                            {

                                msSQL = " update ocs_trn_tpenalityalerthistory set autostop_penal='Y' " +
                                        " where customeralert_gid='" + lscustomeralert_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }


                    msSQL = " select sanction_refno from ocs_trn_tdeferral  where deferral_gid = '" + i + "' and tracking_type = 'Deferral' and deferral_name = 'Original Documents'";
                    var lssanction_refno = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select distinct a.sanction_gid from ids_trn_tsanctiondocumentdtls a" +
                            " left join ocs_mst_tcustomer2sanction b on a.sanction_gid=b.customer2sanction_gid" +
                            " where sanction_refno='" + lssanction_refno + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        var lssanction_gid = objODBCDatareader["sanction_gid"].ToString();
                        objODBCDatareader.Close();
                        DaPostMailSenttoMaker(lssanction_gid, i);
                    }
                    else
                    {
                        objODBCDatareader.Close();
                    }
                }

                msSQL = " select created_by from ocs_trn_tdeferral where deferral_gid='" + i + "'";
                created_by = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " insert into ocs_trn_tdeferralcreationtracking(created_by,deferral_gid,employee_gid)values('" + created_by + "','" + i + "','" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            try
            {
                if (values.deferral_status == "PushBack")
                {
                    string lsdef_gid;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {
                        lspop_mail = objODBCDatareader["pop_username"].ToString();
                        lspop_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select group_concat(employee_mailid) as cc_mail from ocs_trn_tmailcclist where mailtrigger_function='LSA-Alert'";
                    strRes = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " select created_by from ocs_trn_tdeferralcreationtracking where employee_gid='" + employee_gid + "' group by created_by ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = " select group_concat(deferral_gid) as deferral_gid from ocs_trn_tdeferralcreationtracking where created_by='" + dr_datarow["created_by"].ToString() + "'";
                        lsdef_gid = objdbconn.GetExecuteScalar(msSQL);
                        string[] lsarray_defgid = lsdef_gid.Split(',');
                        if (lsarray_defgid.Length == 1)
                        {


                            msSQL = " select b.employee_emailid,a.record_id,a.customer_name, " +
                                    " concat('Checked By',' ',concat(e.user_code,'/',e.user_firstname,' ',e.user_lastname),' ','On',' ', " +
                                    " date_format(c.checked_date, '%d-%m-%Y %h:%i:%s %p')) as checked_by " +
                                    " from ocs_trn_tdeferral a " +
                                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                                    " left join ocs_trn_tdeferralapproval c on c.deferral_gid=a.deferral_gid " +
                                    " left join hrm_mst_temployee d on d.employee_gid=c.checked_by " +
                                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                                    " where a.deferral_gid='" + lsarray_defgid[0] + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows)
                            {
                                lstomail_id = objODBCDatareader["employee_emailid"].ToString();
                                lsrecord_id = objODBCDatareader["record_id"].ToString();
                                lscustomer_name = objODBCDatareader["customer_name"].ToString();
                                lschecked_by = objODBCDatareader["checked_by"].ToString();
                            }
                            objODBCDatareader.Close();


                            body = "<span style=\"font-family: Times New Roman;font-size:12pt;\" >";
                            body = body + "Dear Sir/Madam,  <br />";
                            body = body + "<br />";
                            body = body + "The below Deferral has been pushback to you. Kindly do the Needful and resubmit for Approval.</span>";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "<table style='border:1px solid #060606;' style=\"font-family: Times New Roman;font-size:12pt;\" border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + ">";
                            body = body + "<tr style='color: #fff;background: #00008B;text-align:center;'> ";
                            body = body + "<td style=\"width:10%;\"><b> Record ID </b></td> ";
                            body = body + "<td style=\"width:10%;\"><b> Customer </b> </td>";
                            body = body + "<td style=\"width:10%;\"><b> Checker Status </b> </td>";
                            body = body + "<td style=\"width:30%;\"> <b> Checker Remarks </b> </td>";
                            body = body + "<td style=\"width:40%;\"> <b> Checked By </b> </td>";
                            body = body + "</tr>";
                            body = body + "<tr style='text-align:center;'>";
                            body = body + "<td> " + lsrecord_id + "</td>";
                            body = body + "<td> " + lscustomer_name + "</td>";
                            body = body + "<td> " + "PushBack" + "</td>";
                            body = body + "<td> " + values.checker_remarks + "</td>";
                            body = body + "<td> " + lschecked_by + "</td>";
                            body = body + " </tr>";
                            body = body + "</table>";
                            body = body + "<br /><span style=\"font-family: Times New Roman;font-size:12pt;\" >";
                            body = body + "<br/>";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanking You,";
                            body = body + "<br/>";
                            body = body + "<br/>";
                            body = body + "Yours Sincerely," + "<br/>";
                            body = body + " Samunnati Financial Intermediation & Services Pvt Ltd </span>";

                            mailflag = objcmnfunctions.SendSMTP2(lspop_mail, lspop_password, lstomail_id, " Checker Verify Status ", body, strRes, "", "");
                            values.status = true;
                            values.message = "success";
                        }
                        else
                        {
                            msSQL = " select a.employee_emailid " +
                                    " from hrm_mst_temployee a " +
                                    " where a.employee_gid='" + dr_datarow["created_by"].ToString() + "'";
                            lstomail_id = objdbconn.GetExecuteScalar(msSQL);

                            body = "<span style=\"font-family: Times New Roman;font-size:12pt;\" >";
                            body = body + "Dear Sir/Madam,  <br />";
                            body = body + "<br />";
                            body = body + "The below Deferral has been pushback to you. Kindly do the Needful and resubmit for Approval.</span>";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "<table style='border:1px solid #060606;' style=\"font-family: Times New Roman;font-size:12pt;\" border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + ">";
                            body = body + "<tr style='color: #fff;background: #00008B;text-align:center;'> ";
                            body = body + "<td style=\"width:10%;\"><b> Record ID </b></td> ";
                            body = body + "<td style=\"width:10%;\"><b> Customer </b></td> ";
                            body = body + "<td style=\"width:10%;\"><b> Checker Status </b> </td>";
                            body = body + "<td style=\"width:30%;\"> <b> Checker Remarks </b> </td>";
                            body = body + "<td style=\"width:40%;\"> <b> Checked By </b> </td>";
                            body = body + "</tr>";

                            for (int loopCount = 0; loopCount < lsarray_defgid.Length; loopCount++)
                            {
                                msSQL = " select b.employee_emailid,a.record_id,a.customer_name, " +
                                    " concat('Checked By',' ',concat(e.user_code,'/',e.user_firstname,' ',e.user_lastname),' ','On',' ', " +
                                    " date_format(c.checked_date, '%d-%m-%Y %h:%i:%s %p')) as checked_by " +
                                    " from ocs_trn_tdeferral a " +
                                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                                    " left join ocs_trn_tdeferralapproval c on c.deferral_gid=a.deferral_gid " +
                                    " left join hrm_mst_temployee d on d.employee_gid=c.checked_by " +
                                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                                    " where a.deferral_gid='" + lsarray_defgid[loopCount] + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows)
                                {
                                    lsrecord_id = objODBCDatareader["record_id"].ToString();
                                    lscustomer_name = objODBCDatareader["customer_name"].ToString();
                                    lschecked_by = objODBCDatareader["checked_by"].ToString();
                                }
                                objODBCDatareader.Close();
                                body = body + "<tr style='text-align:center;'><td> " + lsrecord_id + "</td>";
                                body = body + "<td> " + lscustomer_name + "</td>";
                                body = body + "<td> " + "PushBack" + "</td>";
                                body = body + "<td> " + values.checker_remarks + "</td>";
                                body = body + "<td> " + lschecked_by + "</td>";
                                body = body + " </tr>";
                            }

                            body = body + "</table>";
                            body = body + "<br /><span style=\"font-family: Times New Roman;font-size:12pt;\" >";
                            body = body + "<br/>";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanking You,";
                            body = body + "<br/>";
                            body = body + "<br/>";
                            body = body + "Yours Sincerely," + "<br/>";
                            body = body + " Samunnati Financial Intermediation & Services Pvt Ltd </span>";

                            mailflag = objcmnfunctions.SendSMTP2(lspop_mail, lspop_password, lstomail_id, " Checker Verify Status ", body, strRes, "", "");
                            values.status = true;
                            values.message = "success";
                        }
                    }
                    dt_datatable.Dispose();
                    msSQL = " delete from ocs_trn_tdeferralcreationtracking where employee_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " delete from ocs_trn_tdeferralcreationtracking where employee_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            catch (Exception ex)
            {
                msSQL = " delete from ocs_trn_tdeferralcreationtracking where employee_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult1 == 0)
            {
                values.status = false;
                values.message = "failure";
            }
            else
            {
                values.status = true;
                values.message = "success";
            }
        }
        // customer2loan
        public void DaGetCustomer2Loan(mdlcustomer2loan values, string customer_gid)
        {

            msSQL = " select loan_gid,concat(loanref_no,'/',loan_title) as loan_title,loanref_no,sanction_refno,sanction_date" +
                    " from ocs_trn_tloan where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<loan_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new loan_list
                    {
                        value = dr_datarow["loan_gid"].ToString(),
                        title = dr_datarow["loan_title"].ToString(),
                        label = dr_datarow["loanref_no"].ToString(),
                        sanction_date = dr_datarow["sanction_date"].ToString(),
                        sanction_refno = dr_datarow["sanction_refno"].ToString()

                    });
                }
                values.loan = get_filename;
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";

            }

            else
            {
                values.status = false;
                values.message = "failure";
                dt_datatable.Dispose();

            }
        }
        // editdeferral
        public void DaGetEditDeferral(string deferral_gid, deferraledit values)
        {
            msSQL = " select deferral_code,deferral_name,criticallity,comments from ocs_mst_tdeferral where deferraltype_gid='" + deferral_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.deferralCodeedit = objODBCDatareader["deferral_code"].ToString();
                values.deferralNameedit = objODBCDatareader["deferral_name"].ToString();
                values.criticallity = objODBCDatareader["criticallity"].ToString();
                values.comments = objODBCDatareader["comments"].ToString();
                values.deferral_gid = deferral_gid;
            }
            objODBCDatareader.Close();

        }
        // updatedeferral
        public void DaPostUpdateDeferral(string employee_gid, deferraledit values)
        {
            try
            {
                msSQL = " update ocs_mst_tdeferral set " +
                " deferral_code='" + values.deferralCodeedit + "'," +
                " deferral_name='" + values.deferralNameedit + "'," +
                " criticallity='" + values.criticallity + "',";
                if(values.comments==null)
                {
                    msSQL += "comments='',";
                }
                else
                {
                    msSQL += " comments='" + values.comments.Replace("'", "") + "',";
                }
                
               msSQL+= " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where deferraltype_gid='" + values.deferral_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdeferral set " +
                        " deferral_name='" + values.deferralNameedit + "'," +
                        " criticallity='" + values.criticallity + "'";
                if (values.comments == null)
                {
                    msSQL += "comments=''";
                }
                else
                {
                    msSQL += " comments='" + values.comments.Replace("'", "") + "'";
                }

                msSQL +=" where deferraltype_gid='" + values.deferral_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdeferral2loan set " +
                        " deferral_name='" + values.deferralNameedit + "'," +
                        " criticallity='" + values.criticallity + "'" +
                        " where deferraltype_gid='" + values.deferral_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaPostDeleteDeferral(string deferral_gid, deferraledit values)
        {
            try
            {
                msSQL = " delete from ocs_mst_tdeferral where deferraltype_gid='" + deferral_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }

        }
        // deferraldetails
        public void DaGetDeferralDetails(string deferral_gid, createDeferral values)
        {
            try
            {
                msSQL = " select a.branch_gid,a.entity_gid,a.customer_gid,a.customer_name,a.loanref_no,b.checker_status,b.checker_remarks, " +
                 " a.zonal_gid,a.businesshead_gid,a.relationshipmgmt_gid,a.cluster_manager_gid,a.creditmanager_gid,a.deferral_catagory,a.tracking_type," +
                 " deferraltype_gid,covenanttype_gid,criticallity,vertical_gid,vertical_code," +
                 " cast( case when a.old_duedate is null then a.due_date else a.old_duedate end  as char)as due_date, " +
                 " a.remarks,a.customer_remarks" +
                 " from ocs_trn_tdeferral a " +
                 " left join ocs_trn_tdeferralapproval b on b.deferral_gid=a.deferral_gid " +
                 " where a.deferral_gid='" + deferral_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    objODBCDatareader.Read();
                    values.branch_gid = objODBCDatareader["branch_gid"].ToString();
                    values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.loanGID = objODBCDatareader["loanref_no"].ToString();
                    values.zonalGid = objODBCDatareader["zonal_gid"].ToString();
                    values.businessHeadGid = objODBCDatareader["businesshead_gid"].ToString();
                    values.relationshipMgmtGid = objODBCDatareader["relationshipmgmt_gid"].ToString();
                    values.clustermanagerGid = objODBCDatareader["cluster_manager_gid"].ToString();
                    values.creditmanager_gid = objODBCDatareader["creditmanager_gid"].ToString();
                    values.deferral_category = objODBCDatareader["deferral_catagory"].ToString();
                    values.tracking_type = objODBCDatareader["tracking_type"].ToString();
                    values.deferraltype_gid = objODBCDatareader["deferraltype_gid"].ToString();
                    values.covenanttype_gid = objODBCDatareader["covenanttype_gid"].ToString();
                    values.criticallity = objODBCDatareader["criticallity"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.due_date = Convert.ToDateTime(objODBCDatareader["due_date"]).ToString("MM-dd-yyyy");
                    //  values.due_date = objODBCDatareader["due_date"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.customerremarks = objODBCDatareader["customer_remarks"].ToString();
                    values.checker_remarks = objODBCDatareader["checker_remarks"].ToString();
                    values.checker_status = objODBCDatareader["checker_status"].ToString();
                }
                objODBCDatareader.Close();

                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }
        }

        // deferralsupdate
        public void DaPostDeferralsUpdate(string employee_gid, deferralsupdate values)
        {


            try
            {
                msSQL = " select old_duedate from ocs_trn_tdeferral" +
                    " where deferral_gid='" + values.deferral_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    objODBCDatareader.Read();
                    if (objODBCDatareader["old_duedate"].ToString() == "")
                    {
                        objODBCDatareader.Close();
                        msSQL = " update ocs_trn_tdeferral set" +
                               " due_date='" + Convert.ToDateTime(values.duedate).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                               " where deferral_gid='" + values.deferral_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        msSQL = " update ocs_trn_tdeferral set" +
                               " old_duedate='" + Convert.ToDateTime(values.duedate).ToString("yyyy-MM-dd HH:mm:ss") + "',"+
                               " new_duedate='" + Convert.ToDateTime(values.duedate).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                               " where deferral_gid='" + values.deferral_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    objODBCDatareader.Close();
                }
                msSQL = " update ocs_trn_tdeferral2loan set " +
                        " deferral_catagory='" + values.category_gid + "',";
                if (values.remarks == null)
                {
                    msSQL += "remarks='null',";
                }
                else
                {
                    msSQL += "remarks='" + values.remarks.Replace("'", "") + "',";
                }
                if (values.customerremarks == null)
                {
                    msSQL += "customer_remarks='null',";
                }
                else
                {
                    msSQL += "customer_remarks='" + values.customerremarks.Replace("'", "") + "',";
                }
               msSQL+= " tracking_type='" + values.tracking_type + "'," +
                        " deferraltype_gid='" + values.deferraltype_gid + "'," +
                        " deferral_name='" + values.deferraltype_name + "'," +
                        " covenanttype_name='" + values.covenanttype_name + "'," +
                        " customer_gid='" + values.customer_gid + "'," +
                        " due_date='" + Convert.ToDateTime(values.duedate).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " criticallity='" + values.criticallity + "'," +
                        " vertical_gid='" + values.vertical_gid + "'," +
                        " vertical_code='" + values.vertical_code + "'," +
                        " entity_gid='" + values.entity_gid + "'," +
                        " entity_name='" + values.entity_name + "'," +
                        " branch_gid='" + values.branch_gid + "'," +
                        " branch_name='" + values.branch_name + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " covenanttype_gid='" + values.covenanttype_gid + "'" +
                        " where deferral_gid='" + values.deferral_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdeferral set " +
                    " tracking_type='" + values.tracking_type + "'," +
                    " deferraltype_gid='" + values.deferraltype_gid + "'," +
                    " deferral_name='" + values.deferraltype_name + "'," +
                    " deferral_catagory='" + values.category_gid + "',";
                    if (values.remarks == null)
                {
                    msSQL += "remarks='null',";
                }
                else
                {
                    msSQL += "remarks='" + values.remarks.Replace("'", "") + "',";
                }
                if (values.customerremarks == null)
                {
                    msSQL += "customer_remarks='null',";
                }
                else
                {
                    msSQL += "customer_remarks='" + values.customerremarks.Replace("'", "") + "',";
                }
               msSQL+= " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " covenanttype_gid='" + values.covenanttype_gid + "'," +
                    " covenanttype_name='" + values.covenanttype_name + "'," +
                    " customer_gid='" + values.customer_gid + "'," +
                    " customer_name='" + values.customer_name + "'," +
                    " zonal_gid='" + values.zonalhead_gid + "'," +
                    " zonal_name='" + values.zonalhead_name + "'," +
                    " businesshead_gid='" + values.businesshead_gid + "'," +
                    " businesshead_name='" + values.businesshead_name + "'," +
                    " relationshipmgmt_gid='" + values.relationmgr_gid + "'," +
                    " relationshipmgmt_name='" + values.relationmgr_name + "'," +
                    " cluster_manager_gid='" + values.clustermgr_gid + "'," +
                    " cluster_manager_name='" + values.clusterhead_name + "'," +
                    " creditmanager_gid='" + values.creditmgr_gid + "'," +
                    " creditmgmt_name='" + values.creditmgr_name + "'," +
                    " criticallity='" + values.criticallity + "'," +
                    " vertical_gid='" + values.vertical_gid + "'," +
                    " vertical_code='" + values.vertical_code + "'," +
                    " entity_gid='" + values.entity_gid + "'," +
                    " entity_name='" + values.entity_name + "'," +
                    " branch_gid='" + values.branch_gid + "'," +
                    " branch_name='" + values.branch_name + "'" +
                    " where deferral_gid='" + values.deferral_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral " +
                " set deferral_status = 'Expired' where deferral_gid='" + values.deferral_gid + "' and  DATEDIFF(due_date, now())< 0 ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral " +
                       " set deferral_status = 'Live' where deferral_gid='" + values.deferral_gid + "' and DATEDIFF(due_date, now())> 0 ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral" +
                       " set aging = DATEDIFF(now(), due_date)" +
                       " where deferral_gid='" + values.deferral_gid + "' and old_duedate is null and DATEDIFF(due_date, now())< 0";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral" +
                      " set aging = DATEDIFF(now(), new_duedate)" +
                      " where deferral_gid='" + values.deferral_gid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())< 0 ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral" +
                       " set aging='0' where deferral_gid='" + values.deferral_gid + "' and old_duedate is null and DATEDIFF(due_date, now())> 0";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "  UPDATE ocs_trn_tdeferral" +
                        " set aging='0' where deferral_gid='" + values.deferral_gid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())> 0";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.checker_status == "PushBack")
                {
                    msSQL = "  UPDATE ocs_trn_tdeferral" +
                            " set checker_status='Pending' where deferral_gid='" + values.deferral_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "  UPDATE ocs_trn_tdeferralapproval" +
                           " set checker_status='Pending' where deferral_gid='" + values.deferral_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("CKAH");
                    msSQL = " insert into ocs_trn_tcheckerapprovalhistory(" +
                           " checkerapprovalhistory_gid," +
                           " deferral_gid," +
                           " checker_status," +
                           " checkrequest_by," +
                           " checkrequest_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + values.deferral_gid + "'," +
                           "'Pending',";
                    msSQL += "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                 }
            }
            catch
            {
                msSQL = " update ocs_trn_tdeferral2loan set " +
                   " deferral_catagory='" + values.category_gid + "',";
                if (values.remarks == null)
                {
                    msSQL += "remarks='null',";
                }
                else
                {
                    msSQL += "remarks='" + values.remarks.Replace("'", "") + "',";
                }
                if (values.customerremarks == null)
                {
                    msSQL += "customer_remarks='null',";
                }
                else
                {
                    msSQL += "customer_remarks='" + values.customerremarks.Replace("'", "") + "',";
                }
               msSQL+= " tracking_type='" + values.tracking_type + "'," +
                   " deferraltype_gid='" + values.deferraltype_gid + "'," +
                   " deferral_name='" + values.deferraltype_name + "'," +
                   " covenanttype_name='" + values.covenanttype_name + "'," +
                   " customer_gid='" + values.customer_gid + "'," +
                   " criticallity='" + values.criticallity + "'," +
                   " vertical_gid='" + values.vertical_gid + "'," +
                   " vertical_code='" + values.vertical_code + "'," +
                   " entity_gid='" + values.entity_gid + "'," +
                   " entity_name='" + values.entity_name + "'," +
                   " branch_gid='" + values.branch_gid + "'," +
                   " branch_name='" + values.branch_name + "'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " covenanttype_gid='" + values.covenanttype_gid + "'" +
                   " where deferral_gid='" + values.deferral_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdeferral set " +
                    " tracking_type='" + values.tracking_type + "'," +
                    " deferraltype_gid='" + values.deferraltype_gid + "'," +
                    " deferral_name='" + values.deferraltype_name + "'," +
                    " deferral_catagory='" + values.category_gid + "',";
                   if (values.remarks == null)
                {
                    msSQL += "remarks='null',";
                }
                else
                {
                    msSQL += "remarks='" + values.remarks.Replace("'", "") + "',";
                }
                if (values.customerremarks == null)
                {
                    msSQL += "customer_remarks='null',";
                }
                else
                {
                    msSQL += "customer_remarks='" + values.customerremarks.Replace("'", "") + "',";
                }
              msSQL+=  " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " covenanttype_gid='" + values.covenanttype_gid + "'," +
                    " covenanttype_name='" + values.covenanttype_name + "'," +
                    " customer_gid='" + values.customer_gid + "'," +
                    " customer_name='" + values.customer_name + "'," +
                    " zonal_gid='" + values.zonalhead_gid + "'," +
                    " businesshead_gid='" + values.businesshead_gid + "'," +
                    " businesshead_name='" + values.businesshead_name + "'," +
                    " relationshipmgmt_gid='" + values.relationmgr_gid + "'," +
                    " relationshipmgmt_name='" + values.relationmgr_name + "'," +
                    " cluster_manager_gid='" + values.clustermgr_gid + "'," +
                    " cluster_manager_name='" + values.clusterhead_name + "'," +
                     " creditmanager_gid='" + values.creditmgr_gid + "'," +
                    " creditmgmt_name='" + values.creditmgr_name + "'," +
                    " zonal_name='" + values.zonalhead_name + "'," +
                    " criticallity='" + values.criticallity + "'," +
                    " vertical_gid='" + values.vertical_gid + "'," +
                    " vertical_code='" + values.vertical_code + "'," +
                    " entity_gid='" + values.entity_gid + "'," +
                    " entity_name='" + values.entity_name + "'," +
                    " branch_gid='" + values.branch_gid + "'," +
                    " branch_name='" + values.branch_name + "'" +
                    " where deferral_gid='" + values.deferral_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral " +
              " set deferral_status = 'Expired' where deferral_gid='" + values.deferral_gid + "' and  DATEDIFF(due_date, now())< 0 ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral " +
                       " set deferral_status = 'Live' where deferral_gid='" + values.deferral_gid + "' and DATEDIFF(due_date, now())> 0 ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral" +
                       " set aging = DATEDIFF(now(), due_date)" +
                       " where deferral_gid='" + values.deferral_gid + "' and old_duedate is null and DATEDIFF(due_date, now())< 0";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral" +
                      " set aging = DATEDIFF(now(), new_duedate)" +
                      " where deferral_gid='" + values.deferral_gid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())< 0 ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral" +
                       " set aging='0' where deferral_gid='" + values.deferral_gid + "' and old_duedate is null and DATEDIFF(due_date, now())> 0";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "  UPDATE ocs_trn_tdeferral" +
                        " set aging='0' where deferral_gid='" + values.deferral_gid + "' and old_duedate is not null and DATEDIFF(new_duedate, now())> 0";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.checker_status == "PushBack")
                {
                    msSQL = "  UPDATE ocs_trn_tdeferral" +
                            " set checker_status='Pending' where deferral_gid='" + values.deferral_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "  UPDATE ocs_trn_tdeferralapproval" +
                           " set checker_status='Pending' where deferral_gid='" + values.deferral_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("CKAH");
                    msSQL = " insert into ocs_trn_tcheckerapprovalhistory(" +
                           " checkerapprovalhistory_gid," +
                           " deferral_gid," +
                           " checker_status," +
                           " checkrequest_by," +
                           " checkrequest_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + values.deferral_gid + "'," +
                           "'Pending',";
                    msSQL += "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            values.status = true;
            values.message = "success";

        }
        // deferraldeleterecords
        public void DaPostDeferralDelete(string deferral_gid, deferralsupdate values)
        {
            //msSQL = " select deferral_gid from ocs_trn_tdeferralapproval where deferral_gid='" + deferral_gid + "' and (approval_status<>'Pending' or checker_status<>'Pending')";
            msSQL = " select count(deferralapprovalhistory_gid) from ocs_trn_tdeferralapprovalhistory " +
                    " where deferral_gid='" + deferral_gid + "' group by deferral_gid ";
            def_count = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));
            msSQL = " select count(checkerapprovalhistory_gid) from ocs_trn_tcheckerapprovalhistory " +
                   " where deferral_gid='" + deferral_gid + "' group by deferral_gid ";
            check_count = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));

            if ((def_count >= 2) || (check_count >= 2))
            {
                values.status = false;
                values.message = "failure";
            }
            else
            {
                msSQL = "delete from ocs_trn_tdeferral where deferral_gid='" + deferral_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from ocs_trn_tdeferral2loan where deferral_gid='" + deferral_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from ocs_trn_tdeferralapproval where deferral_gid='" + deferral_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from ocs_trn_tdeferralapprovalhistory where deferral_gid='" + deferral_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "delete from ocs_trn_tcheckerapprovalhistory where deferral_gid='" + deferral_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "success";
                }
                else
                {
                    values.status = false;
                    values.message = "failure";
                }
            }
        }

        public void DaGetReOpenSummary(rmdeferralSummary objrmDeferral)
        {
            msSQL = " SELECT a.deferral_gid,a.customer_name as customer_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory, " +
                      " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                      " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " a.loanref_no as loan_title,a.vertical_code," +
                     " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " m.approval_status," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name " +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " where m.approval_status='Closed'  ";
            msSQL += " group by a.deferral_gid order by a.deferral_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetDeferral = new List<rmdeferralSummaryDtls>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetDeferral.Add(new rmdeferralSummaryDtls
                    {
                        customername = (dr_datarow["customer_name"].ToString()),
                        deferral_gid = (dr_datarow["deferral_gid"].ToString()),
                        record_id = (dr_datarow["record_id"].ToString()),
                        tracking_type = (dr_datarow["tracking_type"].ToString()),
                        deferral_name = (dr_datarow["deferral_name"].ToString()),
                        vertical_code = (dr_datarow["vertical_code"].ToString()),
                        deferral_catagory = (dr_datarow["deferral_catagory"].ToString()),
                        duedate = (dr_datarow["due_date"].ToString()),
                        extended_date = (dr_datarow["extended_date"].ToString()),
                        loanTitle = (dr_datarow["loan_title"].ToString()),
                        deferral_status = (dr_datarow["deferral_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                    });
                }
                objrmDeferral.rmdeferralSummaryDtls = objGetDeferral;
            }
            dt_datatable.Dispose();
            objrmDeferral.status = true;
        }

        public void DaGetManageDeferralSummary(managedeferralSummary objrmDeferral)
        {

            msSQL = " SELECT a.deferral_gid,a.checker_status,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory," +
                     " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                      " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date," +
                    " a.loanref_no as loan_title,a.vertical_code," +
                      " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                       " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " concat(q.user_code,'/',q.user_firstname,q.user_lastname) as Created_by ," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status " +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid";
            msSQL += " group by a.deferral_gid order by a.deferral_gid desc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objrmDeferral.managedeferralSummaryDtls = dt_datatable.AsEnumerable().Select(row =>
                new managedeferralSummaryDtls
                {
                    customername = (row["customer_name"].ToString()),
                    deferral_gid = (row["deferral_gid"].ToString()),
                    record_id = (row["record_id"].ToString()),
                    tracking_type = (row["tracking_type"].ToString()),
                    deferral_name = (row["deferral_name"].ToString()),
                    branch_name = (row["branch_name"].ToString()),
                    entity_name = (row["entity_name"].ToString()),
                    aging = (row["aging"].ToString()),
                    Created_by = (row["Created_by"].ToString()),
                    vertical_code = (row["vertical_code"].ToString()),
                    deferral_catagory = (row["deferral_catagory"].ToString()),
                    duedate = (row["due_date"].ToString()),
                    extended_date = (row["extended_date"].ToString()),
                    created_date = (row["created_date"].ToString()),
                    loanTitle = (row["loan_title"].ToString()),
                    deferral_status = (row["deferral_status"].ToString()),
                    checker_status = (row["checker_status"].ToString()),
                    approval_status = (row["approval_status"].ToString())
                }).ToList();
                objrmDeferral.status = true;
            }
            dt_datatable.Dispose();
            objrmDeferral.status = true;
        }

        public void DaGetcadApprovalSummary(managedeferralSummary objrmDeferral)
        {

            msSQL = " SELECT a.deferral_gid,a.checker_status,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory," +
                     " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                      " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date," +
                    " a.loanref_no as loan_title,a.vertical_code," +
                      " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                       " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " concat(q.user_code,'/',q.user_firstname,q.user_lastname) as Created_by ," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status " +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid" +
                    " where m.checker_status='Approved' or m.checker_status='N/A' or m.checker_status='Closed'";
            msSQL += " group by a.deferral_gid order by a.deferral_gid desc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objrmDeferral.managedeferralSummaryDtls = dt_datatable.AsEnumerable().Select(row =>
                new managedeferralSummaryDtls
                {
                    customername = (row["customer_name"].ToString()),
                    deferral_gid = (row["deferral_gid"].ToString()),
                    record_id = (row["record_id"].ToString()),
                    tracking_type = (row["tracking_type"].ToString()),
                    deferral_name = (row["deferral_name"].ToString()),
                    branch_name = (row["branch_name"].ToString()),
                    entity_name = (row["entity_name"].ToString()),
                    aging = (row["aging"].ToString()),
                    Created_by = (row["Created_by"].ToString()),
                    vertical_code = (row["vertical_code"].ToString()),
                    deferral_catagory = (row["deferral_catagory"].ToString()),
                    duedate = (row["due_date"].ToString()),
                    extended_date = (row["extended_date"].ToString()),
                    created_date = (row["created_date"].ToString()),
                    loanTitle = (row["loan_title"].ToString()),
                    deferral_status = (row["deferral_status"].ToString()),
                    checker_status = (row["checker_status"].ToString()),
                    approval_status = (row["approval_status"].ToString())
                }).ToList();
                objrmDeferral.status = true;
            }
            dt_datatable.Dispose();
            objrmDeferral.status = true;
        }

        public void DaGetDeferralReportSummary(deferralSummary objrmDeferral)
        {

            msSQL = " SELECT a.deferral_gid,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,n.customer_urn,a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory,a.criticallity,a.sanction_refno,a.sanction_date ," +
                      " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                      " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date," +
                    " a.loanref_no as loan_title,a.vertical_code,n.state," +
                      " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                       " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " concat(q.user_code,'/',q.user_firstname,q.user_lastname) as Created_by ,n.legaltag_flag," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                    " case when m.approval_status='ReOpen' then 'Pending' when m.approval_status='Extend' then 'Pending' else  m.approval_status end as approval_status, " +
                    " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
                    " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                    " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
                    " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                      " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as zonal_riskmanagerName, " +
                       " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as assigned_RMName, " +
                        " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as riskMonitoring_Name, " +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as count " +
                    " from" +
                    " ocs_trn_tdeferral a" +
                    " left" +
                    " join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid" +
                    " where (a.checker_status='Approved' or a.checker_status='N/A' or  a.checker_status='Closed' ) group by a.deferral_gid order by a.deferral_gid desc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objrmDeferral.deferralSummaryDtls = dt_datatable.AsEnumerable().Select(row =>
                new deferralSummaryDtls
                {
                    customername = (row["customer_name"].ToString()),
                    customer_urn = (row["customer_urn"].ToString()),
                    deferral_gid = (row["deferral_gid"].ToString()),
                    record_id = (row["record_id"].ToString()),
                    tracking_type = (row["tracking_type"].ToString()),
                    deferral_name = (row["deferral_name"].ToString()),
                    branch_name = (row["branch_name"].ToString()),
                    entity_name = (row["entity_name"].ToString()),
                    relationshipmgmt_name = (row["relationshipmgmt_name"].ToString()),
                    aging = (row["aging"].ToString()),
                    Created_by = (row["Created_by"].ToString()),
                    zonal_name = (row["zonal_name"].ToString()),
                    businesshead_name = (row["businesshead_name"].ToString()),
                    cluster_manager_name = (row["cluster_manager_name"].ToString()),
                    zonal_rm = (row["zonal_riskmanagerName"].ToString()),
                    risk_manager = (row["assigned_RMName"].ToString()),
                    riskmonitoring_head = (row["riskMonitoring_Name"].ToString()),
                    creditmanager = (row["creditmgmt_name"].ToString()),
                    vertical_code = (row["vertical_code"].ToString()),
                    legaltag_flag = (row["legaltag_flag"].ToString()),
                    deferral_catagory = (row["deferral_catagory"].ToString()),
                    duedate = (row["due_date"].ToString()),
                    extended_date = (row["extended_date"].ToString()),
                    state = (row["state"].ToString()),
                    criticallity = (row["criticallity"].ToString()),
                    sanction_refno = (row["sanction_refno"].ToString()),
                    sanction_date = (row["sanction_date"].ToString()),
                    created_date = (row["created_date"].ToString()),
                    loanTitle = (row["loan_title"].ToString()),
                    clustermanager = (row["cluster_manager_name"].ToString()),
                    deferral_status = (row["deferral_status"].ToString()),
                    approval_status = (row["approval_status"].ToString()),
                    count_extension = (row["count"].ToString())
                }).ToList();
                objrmDeferral.status = true;
            }
            dt_datatable.Dispose();
            objrmDeferral.status = true;
        }

        public void DaUserReport2export(deferralSummary objuser2report)
        {
            msSQL = " SELECT a.entity_name as 'Entity',n.state as 'State',n.customer_urn as 'URN',a.customer_name as 'Customer', " +
                    " a.vertical_code as 'Vertical', a.sanction_refno as 'Sanction Refno',a.sanction_date as 'Sanction Date', " + 
                    " a.record_id as 'Record ID', a.tracking_type as 'Tracking Type', " + 
                    " case when a.tracking_type = 'Deferral' then a.deferral_name else a.covenanttype_name end as 'Deferral Type',a.criticallity as 'Criticality', " +  
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as 'Due Date'," + 
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as 'Extended Date', " +
                    " case when m.approval_status = 'ReOpen' then 'Pending' when m.approval_status='Extend' then 'Pending' when m.approval_status='Re-track' then 'Pending' else  m.approval_status end as 'Deferral Stage', " + 
                    " a.customer_remarks as 'Customer Remarks', " +
                    " case when m.approval_status = 'Closed' then '-' else a.aging end as 'Aging', " +
                    " case when m.approval_status = 'Closed' then 'Closed' when m.approval_status='Waived' then 'Waived' else a.deferral_status end as 'Deferral Status', " +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid), '0') as char) as  'Extension Count', " +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as 'Relationship Manager', " +
                    " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as 'Cluster Manager', " +
                    " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as 'Zonal Head', " +
                    " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as 'Business Head', " +
                    " case when n.npatag_flag = 'Y' then n.npatag_remarks else '' end as 'NPA Tagged Remarks' " +
                    " from ocs_trn_tdeferral a " +
                    " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid " +
                    " left join ocs_mst_tcustomer n on n.customer_gid = a.customer_gid " +
                    " left join ocs_trn_tcustomer2legalhistory mn on n.customer2legalhistory_gid = mn.customer2legalhistory_gid " +
                    " left join hrm_mst_temployee nb on mn.created_by = nb.employee_gid " +
                    " left join adm_mst_tuser cv on cv.user_gid = nb.user_gid " +
                    " left join hrm_mst_temployee k on a.updated_by = k.employee_gid " +
                    " left join adm_mst_tuser l on l.user_gid = k.user_gid " +
                    " left join hrm_mst_temployee w on w.employee_gid = m.approved_by " +
                    " left join adm_mst_tuser z on z.user_gid = w.user_gid " +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid " +
                    " left join adm_mst_tuser o on o.user_gid = p.user_gid " +
                    " left join hrm_mst_temployee r on n.npatagged_by = r.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = r.user_gid " +
                    " where(m.approval_status = 'Extend' or m.approval_status = 'Pending' or m.approval_status = 'ReOpen' or m.approval_status = 'Re-track') " +
                    " and a.deferral_catagory not in('CAD Internal','CMD Internal') " +
                    " and (a.deferral_status='Expired' or a.deferral_status='Live') " +
                    " and (a.checker_status='Approved' or a.checker_status='N/A') and " ;
                    if (objuser2report.vertical_gid == null || objuser2report.vertical_gid == "")
                    {
                        msSQL += "  1=1 ";
                    }
                    else
                    {
                        msSQL += "   a.vertical_gid = '" + objuser2report.vertical_gid + "'";
                    }
                    if (objuser2report.entity_gid == null || objuser2report.entity_gid == "")
                    {
                        msSQL += " and 1=1 ";
                    }
                    else
                    {
                        msSQL += " and a.entity_gid = '" + objuser2report.entity_gid + "'";
                    }
                    if (objuser2report.state_gid == null || objuser2report.state_gid == "")
                    {
                        msSQL += " and 1=1 ";
                    }
                    else
                    {
                        msSQL += " and n.state_gid = '" + objuser2report.state_gid + "'";
                    }
            msSQL += " group by a.deferral_gid order by a.deferral_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("User Report2");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objuser2report.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/UserReport2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(objuser2report.lspath)))
                        System.IO.Directory.CreateDirectory(objuser2report.lspath);
                }

                objuser2report.lsname = "User_Report2" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                objuser2report.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/UserReport2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objuser2report.lsname;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objuser2report.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 23])  
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                dt_datatable.Dispose();
                bool status;
                objuser2report.lspath = lscompany_code + "/" + "ECMS/UserReport2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objuser2report.lsname;
                status = objcmnstorage.UploadStream("erpdocument", objuser2report.lspath, ms);
                ms.Close();

                objuser2report.lspath = objcmnstorage.EncryptData(objuser2report.lspath);
                objuser2report.status = true;
                objuser2report.message = "Success";
            }
            catch (Exception ex)
            {
                objuser2report.status = false;
                objuser2report.message = "Failure";
            }
        }

        public void DaUser2reportsummary(deferralSummary objrmDeferral)
        {

            msSQL = " SELECT a.deferral_gid,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,n.customer_urn,a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory,a.criticallity,a.sanction_refno,a.sanction_date ," +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date," +
                    " a.loanref_no as loan_title,a.vertical_code,n.state," +
                    " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                    " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " concat(q.user_code,'/',q.user_firstname,q.user_lastname) as Created_by ,n.legaltag_flag," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status, " +
                    " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
                    " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                    " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
                    " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                    " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as zonal_riskmanagerName, " +
                    " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as assigned_RMName, " +
                    " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as riskMonitoring_Name, " +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as count " +
                    " from ocs_trn_tdeferral a" +
                    " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid" +
                    " where(m.approval_status = 'Extend' or m.approval_status = 'Pending' or m.approval_status = 'ReOpen' or m.approval_status = 'Re-track') " +
                    " and a.deferral_catagory not in('CAD Internal','CMD Internal') " +
                    " and (a.deferral_status='Expired' or a.deferral_status='Live') " +
                    " and (a.checker_status='Approved' or a.checker_status='N/A') group by a.deferral_gid order by a.deferral_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objrmDeferral.deferralSummaryDtls = dt_datatable.AsEnumerable().Select(row =>
                new deferralSummaryDtls
                {
                    customername = (row["customer_name"].ToString()),
                    customer_urn = (row["customer_urn"].ToString()),
                    deferral_gid = (row["deferral_gid"].ToString()),
                    record_id = (row["record_id"].ToString()),
                    tracking_type = (row["tracking_type"].ToString()),
                    deferral_name = (row["deferral_name"].ToString()),
                    branch_name = (row["branch_name"].ToString()),
                    entity_name = (row["entity_name"].ToString()),
                    relationshipmgmt_name = (row["relationshipmgmt_name"].ToString()),
                    aging = (row["aging"].ToString()),
                    Created_by = (row["Created_by"].ToString()),
                    zonal_name = (row["zonal_name"].ToString()),
                    businesshead_name = (row["businesshead_name"].ToString()),
                    cluster_manager_name = (row["cluster_manager_name"].ToString()),
                    zonal_rm = (row["zonal_riskmanagerName"].ToString()),
                    risk_manager = (row["assigned_RMName"].ToString()),
                    riskmonitoring_head = (row["riskMonitoring_Name"].ToString()),
                    creditmanager = (row["creditmgmt_name"].ToString()),
                    vertical_code = (row["vertical_code"].ToString()),
                    legaltag_flag = (row["legaltag_flag"].ToString()),
                    deferral_catagory = (row["deferral_catagory"].ToString()),
                    duedate = (row["due_date"].ToString()),
                    extended_date = (row["extended_date"].ToString()),
                    state = (row["state"].ToString()),
                    criticallity = (row["criticallity"].ToString()),
                    sanction_refno = (row["sanction_refno"].ToString()),
                    sanction_date = (row["sanction_date"].ToString()),
                    created_date = (row["created_date"].ToString()),
                    loanTitle = (row["loan_title"].ToString()),
                    clustermanager = (row["cluster_manager_name"].ToString()),
                    deferral_status = (row["deferral_status"].ToString()),
                    approval_status = (row["approval_status"].ToString()),
                    count_extension = (row["count"].ToString())
                }).ToList();
                objrmDeferral.status = true;
            }
            dt_datatable.Dispose();
            objrmDeferral.status = true;
        }

        public void DaUser2reportsummarysearch(deferralSummary objrmDeferral)
        {

            msSQL = " SELECT a.deferral_gid,SUBSTRING_INDEX(a.branch_name, '/', -1) as branch_name,n.customer_urn,a.customer_name as customer_name,a.entity_name,a.tracking_type,a.record_id," +
                    " a.deferral_catagory,a.criticallity,a.sanction_refno,a.sanction_date ," +
                    " case when a.old_duedate is null then date_format(a.due_date, '%d-%m-%Y') else date_format(a.old_duedate, '%d-%m-%Y') end as due_date," +
                    " case when a.old_duedate is null then 'N/A' else date_format(a.due_date, '%d-%m-%Y') end as extended_date," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date," +
                    " a.loanref_no as loan_title,a.vertical_code,n.state," +
                    " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                    " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                    " concat(q.user_code,'/',q.user_firstname,q.user_lastname) as Created_by ,n.legaltag_flag," +
                    " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name, " +
                    " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status, " +
                    " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
                    " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                    " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                    " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
                    " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                    " case when n.zonal_riskmanagerName is null then 'NA' else n.zonal_riskmanagerName end as zonal_riskmanagerName, " +
                    " case when n.assigned_RMName is null then 'NA' else n.assigned_RMName end as assigned_RMName, " +
                    " case when n.riskMonitoring_Name is null then 'NA' else n.riskMonitoring_Name end as riskMonitoring_Name, " +
                    " cast(ifnull((select count(*) from ocs_trn_tdeferralapprovalhistory sd " +
                    " where sd.approval_status = 'Extend' and sd.deferral_gid = a.deferral_gid group by sd.deferral_gid) ,'0') as char) as count " +
                    " from ocs_trn_tdeferral a" +
                    " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                    " left join ocs_mst_tcustomer n on n.customer_gid=a.customer_gid " +
                    " left join hrm_mst_temployee p on a.created_by = p.employee_gid" +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid" +
                    " where(m.approval_status = 'Extend' or m.approval_status = 'Pending' or m.approval_status = 'ReOpen' or m.approval_status = 'Re-track') " +
                    " and a.deferral_catagory not in('CAD Internal','CMD Internal') " +
                    " and (a.deferral_status='Expired' or a.deferral_status='Live') " +
                    " and (a.checker_status='Approved' or a.checker_status='N/A')  and ";
            if (objrmDeferral.vertical_gid == null || objrmDeferral.vertical_gid == "")
            {
                msSQL += "  1=1 ";
            }
            else
            {
                msSQL += "   a.vertical_gid = '" + objrmDeferral.vertical_gid + "'";
            }
            if (objrmDeferral.entity_gid == null || objrmDeferral.entity_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and a.entity_gid = '" + objrmDeferral.entity_gid + "'";
            }
            if (objrmDeferral.state_gid == null || objrmDeferral.state_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and n.state_gid = '" + objrmDeferral.state_gid + "'";
            }
            msSQL += " group by a.deferral_gid order by a.deferral_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objrmDeferral.deferralSummaryDtls = dt_datatable.AsEnumerable().Select(row =>
                new deferralSummaryDtls
                {
                    customername = (row["customer_name"].ToString()),
                    customer_urn = (row["customer_urn"].ToString()),
                    deferral_gid = (row["deferral_gid"].ToString()),
                    record_id = (row["record_id"].ToString()),
                    tracking_type = (row["tracking_type"].ToString()),
                    deferral_name = (row["deferral_name"].ToString()),
                    branch_name = (row["branch_name"].ToString()),
                    entity_name = (row["entity_name"].ToString()),
                    relationshipmgmt_name = (row["relationshipmgmt_name"].ToString()),
                    aging = (row["aging"].ToString()),
                    Created_by = (row["Created_by"].ToString()),
                    zonal_name = (row["zonal_name"].ToString()),
                    businesshead_name = (row["businesshead_name"].ToString()),
                    cluster_manager_name = (row["cluster_manager_name"].ToString()),
                    zonal_rm = (row["zonal_riskmanagerName"].ToString()),
                    risk_manager = (row["assigned_RMName"].ToString()),
                    riskmonitoring_head = (row["riskMonitoring_Name"].ToString()),
                    creditmanager = (row["creditmgmt_name"].ToString()),
                    vertical_code = (row["vertical_code"].ToString()),
                    legaltag_flag = (row["legaltag_flag"].ToString()),
                    deferral_catagory = (row["deferral_catagory"].ToString()),
                    duedate = (row["due_date"].ToString()),
                    extended_date = (row["extended_date"].ToString()),
                    state = (row["state"].ToString()),
                    criticallity = (row["criticallity"].ToString()),
                    sanction_refno = (row["sanction_refno"].ToString()),
                    sanction_date = (row["sanction_date"].ToString()),
                    created_date = (row["created_date"].ToString()),
                    loanTitle = (row["loan_title"].ToString()),
                    clustermanager = (row["cluster_manager_name"].ToString()),
                    deferral_status = (row["deferral_status"].ToString()),
                    approval_status = (row["approval_status"].ToString()),
                    count_extension = (row["count"].ToString())
                }).ToList();
                objrmDeferral.status = true;
            }
            dt_datatable.Dispose();
            objrmDeferral.status = true;
        }
    }

}




