using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Configuration;
using ems.storage.Functions;
using System.Web;
using System.IO;


namespace ems.audit.DataAccess
{
    public class DaAtmRptAuditReports
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_datatable1, dt_datatable2;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetGid1, lspath, lsauditvisitref_no, lsemployeereporting_to, lsreporting_name;
        string excelRange, endRange;
        int rowCount, columnCount;
        int mnResult, mnresult;
        HttpPostedFile httpPostedFile;
        public void DaGetAuditReportSummary(MdlAtmRptAuditReports values)
        {
            try
            {
                msSQL = " SELECT distinct a.auditcreation_gid,a.approval_status,a.audittype_name,d.auditcreation2checklist_gid,a.checklistmaster_gid,a.entity_name,a.checkpointgroup_name,a.auditdepartment_name, " +
                        "  a.audit_name,a.auditpriority_name,  " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                         " left join atm_trn_tauditcreation2checklist d  on a.auditcreation_gid = d.auditcreation_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid group by a.auditcreation_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditreport_list = new List<auditreport_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditreport_list.Add(new auditreport_list
                        {
                            auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            entity_name = (dr_datarow["entity_name"].ToString()),
                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),

                        });
                    }
                    values.auditreport_list = getauditreport_list;

                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }

        public void DaGetAuditReportSummaryExcelExport(MdlAtmRptAuditReports values)
        {

            msSQL = " SELECT  /*+ MAX_EXECUTION_TIME(900000) */ distinct  (a.entity_name) as 'Entity', (a.auditdepartment_name) as 'Audit Department', (a.audit_name) as 'Audit Name', " +
                    " (a.auditpriority_name) as 'Priority', (a.audittype_name) as 'Audit Type', (a.checkpointgroup_name) as 'Checkpoint Group', (a.audit_description)  as 'Audit Description', " +
                    " date_format(a.auditperiod_fromdate,'%d-%m-%Y') as 'Audit Period From', date_format(a.auditperiod_todate,'%d-%m-%Y') as 'Audit Period To', date_format(a.due_date,'%d-%m-%Y') as 'Audit End Date', " +
                    " (a.auditmaker_name) as 'Auditor Maker' , (a.auditchecker_name) as 'Auditor Checker' , (a.auditapprover_name) as 'Auditor Approver', (a.auditmaker_name) as 'Auditee Maker', (a.auditchecker_name) as 'Auditee Checker', (f.employee_name) as 'Tagged Employee'," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as 'Audit Initated by', date_format(a.created_date,'%d-%m-%Y %h:%i %p') as 'Audit Initated date', " +
                    "   concat(l.user_firstname,' ',l.user_lastname,' / ',l.user_code) as 'Audit Approved by', (date_format(a.auditapproved_date, '%d-%m-%Y %h:%i %p')) as 'Audit Approved Date', " +
                    "     date_format(j.created_date ,'%d-%m-%Y %h:%i %p') as 'Auditor Maker Initiated Date', " +
                    " date_format(a.auditorcheckerinitiated_date ,'%d-%m-%Y %h:%i %p') as 'Auditor checker initiated Date' , " +
                    "   date_format(g.created_date, '%d-%m-%Y %h:%i %p') as 'Auditee Checker Initiated Date', " +
                    "   date_format(h.created_date,'%d-%m-%Y %h:%i %p') as 'Auditor Final Approved Date' " +
                    " FROM atm_trn_tauditcreation a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join hrm_mst_temployee k on a.auditapproved_by = k.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join adm_mst_tuser l on l.user_gid = k.user_gid " +
                    " left join atm_mst_ttaguser2employee f on f.auditcreation_gid = a.auditcreation_gid " +
                    " left join atm_trn_tauditorapprovalget g on g.auditcreation_gid = a.auditcreation_gid " +
                    "left join atm_trn_tauditapproval h on h.auditcreation_gid = a.auditcreation_gid  " +
                    "left join atm_trn_tmakerinitiateapproval j on j.auditcreation_gid = a.auditcreation_gid " +
                    "group by a.auditcreation_gid order by a.auditcreation_gid  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();         
            var workSheet = excel.Workbook.Worksheets.Add("Audit Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Audit Report.xlsx";               
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                values.lscloudpath = lscompany_code + "/" + "Audit/Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);              
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 24])  //Address "A1:A24"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                //excel.SaveAs(file);
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }


        public void DaEditAuditReport(string auditcreation_gid, MdlAtmRptAuditReports values)
        {
            msSQL = " SELECT distinct a.auditapprover_name,a.auditdepartment_name, a.audit_description,  a.entity_name, concat(l.user_firstname,' ',l.user_lastname,' / ',l.user_code) as auditapproved_by, a.auditapproved_date, " +
                        "  a.audit_name,a.auditpriority_name, i.audittype_name, a.checkpointgroup_name, date_format(j.created_date ,'%d-%m-%Y %h:%i %p') as AuditorMakerInitiatedDate, " +
                        " a.auditmaker_name,a.auditchecker_name, f.employee_name as TaggedEmployee, date_format(h.created_date,'%d-%m-%Y %h:%i %p') as AuditorApprovedDate, " +
                        " date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as AuditRequest_created_date, " +
                        " date_format(a.auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate,  date_format(g.created_date, '%d-%m-%Y %h:%i %p') as AuditeeCheckerInitiatedDate, " +
                         " date_format(a.auditperiod_todate,'%d-%m-%Y') as auditperiod_todate, a.auditeemaker_name, a.auditeechecker_name," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as AuditRequest_created_by," +
                        " date_format(a.auditorcheckerinitiated_date ,'%d-%m-%Y %h:%i %p') as Auditorcheckerinitiated_date" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join hrm_mst_temployee k on a.auditapproved_by = k.employee_gid" +
                        " left join atm_trn_tsampleexcelimport d on d.auditcreation_gid = a.auditcreation_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join adm_mst_tuser l on l.user_gid = k.user_gid " +
                        " left join atm_trn_tauditcreation2checklist e on e.checklistmaster_gid = a.checklistmaster_gid " +
                        " left join atm_mst_ttaguser2employee f on f.auditcreation_gid = a.auditcreation_gid " +
                        " left join atm_trn_tauditorapprovalget g on g.auditcreation_gid = a.auditcreation_gid " +
                         "left join atm_trn_tauditapproval h on h.auditcreation_gid = a.auditcreation_gid  " +
                         "left join atm_trn_tmakerinitiateapproval j on j.auditcreation_gid = a.auditcreation_gid " +
                         "left join atm_mst_tchecklistmaster i on i.checklistmaster_gid=a.checklistmaster_gid "+
                    " where   a.auditcreation_gid='" + auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditapprover_name = objODBCDatareader["auditapprover_name"].ToString();
                values.entity_name = objODBCDatareader["entity_name"].ToString();
                values.audittype_name = objODBCDatareader["audittype_name"].ToString();
                values.checkpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                values.auditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                values.audit_description = objODBCDatareader["audit_description"].ToString();
                //values.approval_name = objODBCDatareader["FinalApprover"].ToString();
                values.audit_name = objODBCDatareader["audit_name"].ToString();
                values.auditpriority_name = objODBCDatareader["auditpriority_name"].ToString();
                values.auditmaker_name = objODBCDatareader["auditmaker_name"].ToString();
                values.auditchecker_name = objODBCDatareader["auditchecker_name"].ToString();
                values.tagged_user = objODBCDatareader["TaggedEmployee"].ToString();
                values.due_date = objODBCDatareader["due_date"].ToString();
                values.created_date = objODBCDatareader["AuditRequest_created_date"].ToString();
                values.auditperiod_fromdate = objODBCDatareader["auditperiod_fromdate"].ToString();
                values.auditperiod_todate = objODBCDatareader["auditperiod_todate"].ToString();
                values.auditeemaker_name = objODBCDatareader["auditeemaker_name"].ToString();
                values.auditeechecker_name = objODBCDatareader["auditeechecker_name"].ToString();
                values.created_by = objODBCDatareader["AuditRequest_created_by"].ToString();
                values.AuditorMakerInitiatedDate = objODBCDatareader["AuditorMakerInitiatedDate"].ToString();
                values.AuditeeCheckerInitiatedDate = objODBCDatareader["AuditeeCheckerInitiatedDate"].ToString();
                values.AuditorApprovedDate = objODBCDatareader["AuditorApprovedDate"].ToString();
                values.auditapproved_by = objODBCDatareader["auditapproved_by"].ToString();
                values.auditapproved_date = objODBCDatareader["auditapproved_date"].ToString();
                values.Auditorcheckerinitiated_date = objODBCDatareader["Auditorcheckerinitiated_date"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;


        }

        public void DaGetIndividualAuditReportSummaryExcelExport(MdlAtmRptAuditReports values, string auditcreation_gid)
        {

            msSQL = "SELECT distinct d.audit_name, a.query_title, (a.description) as raisedquery,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as queryraisedon, date_format(a.closed_date,'%d-%m-%Y %h:%i %p') as querycloseddate, " +
                        " m.header_names, m.header_values, h.audit_name, h.checkpoint_intent, h.checkpoint_description, h.noteto_auditor, h.yes_disposition, h.no_disposition, h.partial_disposition, h.na_disposition, h.yes_score, h.no_score, h.partial_score, h.na_score, h.total_score, h.capture_score, d.observation_percentage,  " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,(i.remarks) as Response,a.close_remarks, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as closed_by " +
                        " FROM atm_trn_tsampleraisequery a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join atm_trn_tauditcreation d on d.auditcreation_gid = a.auditcreation_gid " +
                        " left join atm_trn_tauditcreation2checklist h on d.auditcreation_gid = h.auditcreation_gid " +
                        " left join atm_trn_tsampleexcelimport m on m.auditcreation_gid = h.auditcreation_gid" +
                        " left join atm_trn_tsamplequeries2response i on i.auditcreation_gid = h.auditcreation_gid " +
                        " left join hrm_mst_temployee f on a.closed_by = f.employee_gid" +
                        " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                        " where a.auditcreation_gid = '" + auditcreation_gid + "'  ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Audit Query Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Audit Query Report.xlsx";             
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Individual Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Individual Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                values.lscloudpath = lscompany_code + "/" + "Audit/Individual Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/Individual Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }


        public void DaGetAuditObservationReportExcelExport(MdlAtmRptAuditReports values, string auditcreation_gid)
        {

            msSQL = " select   a.audittype_name, a.auditdepartment_name, b.riskcategory_name, b.positiveconfirmity_name," +
                    " b.checkpointgroup_name, a.audit_name, b.checkpoint_intent, b.checkpoint_description, b.noteto_auditor," +
                    " b.yes_disposition, b.no_disposition, b.partial_disposition, b.na_disposition," +
                    " b.yes_score, b.no_score, b.partial_score, b.na_score, b.capture_score, b.total_score, a.overall_score, a.observation_score,  a.observation_percentage" +
                    " from atm_trn_tauditcreation a " +
                    " left join atm_trn_tauditcreation2checklist b on a.auditcreation_gid=b.auditcreation_gid " +
                    " left join atm_trn_tobservationtotalamount c on b.auditcreation2checklist_gid=c.auditcreation2checklist_gid " +
                    " where a.auditcreation_gid='" + auditcreation_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Audit Observation Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Audit Observation Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Observation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Observation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Audit/Audit Observation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/Audit Observation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }


        public void DaGetAuditSampleQueryReportExcelExport(MdlAtmRptAuditReports values, string auditcreation_gid)
        {

            msSQL = " select c.sample_name, a.query_title, a.description, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as queryrasiedby," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as queryraisedon, (b.remarks) as Response, concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as repliedby," +
                    " date_format(b.created_date, '%d-%m-%Y %h:%i %p') as replieddate, a.close_remarks, " +
                    " concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as closedby, date_format(a.closed_date, '%d-%m-%Y %h:%i %p') as closed_date " +
                    " from atm_trn_tsampleraisequery a " +
                    " left join atm_trn_tsamplequeries2response b on a.sampleraisequery_gid=b.sampleraisequery_gid " +
                    " left join atm_trn_tsampleimport c on a.sampleimport_gid=c.sampleimport_gid " +
                     " left join hrm_mst_temployee d on a.created_by = d.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid "+
                        " left join hrm_mst_temployee f on b.created_by = f.employee_gid" +
                        " left join adm_mst_tuser g on f.user_gid = g.user_gid " +
                        " left join hrm_mst_temployee h on a.closed_by = h.employee_gid" +
                        " left join adm_mst_tuser i on i.user_gid = h.user_gid " +
            " where a.auditcreation_gid='" + auditcreation_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            //ExcelPackage excel = new ExcelPackage();
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet = excel.Workbook.Worksheets.Add("Audit Sample Query Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Audit Sample Query Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Sample Query Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Sample Query Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Audit/Audit Sample Query Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/Audit Sample Query Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }

        public void DaGetAuditSampleReportExcelExport(MdlAtmRptAuditReports values, string auditcreation_gid)
        {

            msSQL = "select auditcreation_gid from atm_trn_tsampleexcelimport where auditcreation_gid = '" + auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                
                msSQL = "CALL Getsampleexportdata ('"+auditcreation_gid+"')";
            dt_datatable = objdbconn.GetDataTable(msSQL);

                string lscompany_code = string.Empty;
                //ExcelPackage excel = new ExcelPackage();
                MemoryStream ms = new MemoryStream();
                ExcelPackage excel = new ExcelPackage(ms);

                var workSheet = excel.Workbook.Worksheets.Add("Audit Sample Report");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";
                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.lsname = "Audit Sample Report.xlsx";
                    var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Sample Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Sample Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                    values.lscloudpath = lscompany_code + "/" + "Audit/Audit Sample Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                    bool exists = System.IO.Directory.Exists(path);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }

                    workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                    FileInfo file = new FileInfo(values.lspath);
                    using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                        range.Style.Font.Color.SetColor(Color.White);
                    }

                    excel.SaveAs(ms);
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/Audit Sample Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                    ms.Close();

                }


                catch (Exception ex)
                {
                    values.status = false;
                    values.message = "Failure";
                }
                values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
                values.lspath = objcmnstorage.EncryptData(values.lspath);
                values.status = true;
                values.message = "Success";
            }

            else
            {
                values.message = "No records found";
                values.status = false;
            }
        }
        public void DaGetAuditObservationSampleReportExcelExport(MdlAtmRptAuditReports values, string auditcreation_gid)
        {
            
            msSQL = " select distinct (a.auditcreation_gid) as 'AuditRef No',(a.audit_name) as 'Audit Name',(a.audittype_name) as 'Audit Type', (a.auditdepartment_name) as 'Audit Department', (a.auditmaker_name) as 'Auditor maker',(a.auditchecker_name) as 'Auditor Checker',(a.auditapprover_name) as 'Auditor Approver',group_concat(distinct o.auditeemaker_name SEPARATOR ' | ') as 'Auditee Maker',group_concat(distinct o.auditeechecker_name SEPARATOR ' | ') as 'Auditee Checker',group_concat(f.employee_name SEPARATOR ' | ') as 'Tagged Employee', group_concat(distinct p.checklist_name SEPARATOR ' | ') as 'checklist name',group_concat(distinct b.riskcategory_name SEPARATOR ' | ') as 'risk catagory'," +
                " group_concat(distinct b.positiveconfirmity_name SEPARATOR ' | ') as 'positive confirmity',group_concat(distinct b.checkpointgroup_name SEPARATOR ' | ') as 'checkpointgroup',group_concat(distinct b.checkpoint_intent SEPARATOR ' | ') as 'checkpoint intent',group_concat(distinct b.checkpoint_description SEPARATOR ' | ') as 'checkpoint description'," +
          "group_concat(distinct b.noteto_auditor SEPARATOR ' | ') as 'noteto auditor',group_concat(distinct b.yes_disposition SEPARATOR ' | ') as 'yes disposition',group_concat(distinct b.no_disposition SEPARATOR ' | ') as 'no disposition',group_concat(distinct b.partial_disposition SEPARATOR ' | ') as 'partial disposition',group_concat(distinct b.na_disposition SEPARATOR ' | ') as 'na disposition'," +
         " group_concat(distinct b.yes_score SEPARATOR ' | ') as 'Yes Score',group_concat(distinct b.no_score SEPARATOR ' | ') as 'No Score',group_concat(distinct b.partial_score SEPARATOR ' | ') as 'partial score',group_concat(distinct b.na_score SEPARATOR ' | ') as 'na score', group_concat(distinct b.capture_score SEPARATOR ' | ') as 'capture score'," +
            " group_concat(distinct b.total_score SEPARATOR ' | ') as 'total score',group_concat(distinct a.overall_score SEPARATOR ' | ') as 'overall score',group_concat(distinct a.observation_score SEPARATOR ' | ') as 'observation score',group_concat(distinct a.observation_percentage SEPARATOR ' | ') as 'observation_percentage'," +
           " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as 'Created_by',group_concat( distinct d.query_title SEPARATOR ' | ') as 'Query Title',group_concat(distinct d.description SEPARATOR ' | ') as 'Description',group_concat(distinct d.raisequery_raisedby SEPARATOR ' | ') as 'Queryrasiedby',group_concat(distinct e.remarks SEPARATOR ' | ') as 'Response'," +
        " group_concat(distinct e.raisequery_replyby SEPARATOR ' | ') as 'Repliedby',group_concat(distinct d.close_remarks SEPARATOR ' | ') as 'Close remarks',group_concat(distinct d.raisequery_closedby SEPARATOR ' | ') as 'Closedby' " +
                    " from atm_trn_tauditcreation a " +
                     " left join atm_trn_tauditcreation2checklist b on a.auditcreation_gid = b.auditcreation_gid " +                   
                   " left join atm_trn_tauditraisequery d on a.auditcreation_gid = d.auditcreation_gid " +
                   " left join atm_trn_tchecklist2checkpoint p on a.auditcreation_gid = p.auditcreation_gid " +                 
                    " left join atm_trn_tauditqueries2response e on d.auditcreation_gid = e.auditcreation_gid " +
                    " left join atm_mst_ttaguser2employee f on f.auditcreation_gid = a.auditcreation_gid " +             
                  " left join atm_trn_tauditagainstmultipleauditeechecker o on o.auditcreation_gid = a.auditcreation_gid " +
                    " left join hrm_mst_temployee g on a.created_by = g.employee_gid" +
                        " left join adm_mst_tuser h on h.user_gid = g.user_gid "+
                    " where a.auditcreation_gid='" + auditcreation_gid + "' group by a.auditcreation_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            msSQL = " select distinct (a.auditcreation_gid) as 'AuditRef No',(a.audit_name) as 'Audit Name',(a.audittype_name) as 'Audit Type',(a.auditobservation_name) as 'Auditobservation Name', (a.auditdepartment_name) as 'Auditdepartment Name', group_concat(distinct b.riskcategory_name SEPARATOR ' | ') as 'risk catagory', group_concat(distinct b.positiveconfirmity_name SEPARATOR ' | ') as 'positive confirmity',group_concat(distinct b.checkpointgroup_name SEPARATOR ' | ') as 'checkpoint group',group_concat(distinct b.checkpoint_intent SEPARATOR ' | ') as 'checkpoint intent'," +
                     " group_concat(distinct b.checkpoint_description SEPARATOR ' | ') as 'checkpoint description',group_concat(distinct b.noteto_auditor SEPARATOR ' | ') as 'Note To Auditor',group_concat(distinct b.yes_disposition SEPARATOR ' | ') as 'yes disposition',group_concat(distinct b.no_disposition SEPARATOR ' | ') as 'no disposition',group_concat(distinct b.partial_disposition SEPARATOR ' | ') as 'partial disposition',group_concat(distinct b.na_disposition SEPARATOR ' | ') as 'na disposition', " +
                 " group_concat(distinct b.yes_score SEPARATOR ' | ') as 'sample yes score',group_concat(distinct b.no_score SEPARATOR ' | ') as 'sample no score',group_concat(distinct b.partial_score SEPARATOR ' | ') as 'sample partial score', group_concat(distinct b.na_score SEPARATOR ' | ') as 'sample na score',group_concat(distinct b.total_score SEPARATOR ' | ') as 'sample total score',group_concat(distinct b.samplecapture_field SEPARATOR ' | ') as 'samplecapture field'," +
                   " group_concat(distinct b.samplecapture_score SEPARATOR ' | ') as 'samplecapture score',group_concat(distinct b.sampleobservation_score SEPARATOR ' | ') as 'sampleobservation score',group_concat(distinct b.sampleobservation_percentage SEPARATOR ' | ') as 'sampleobservation percentage',group_concat(distinct d.sample_name SEPARATOR ' | ') as 'sample name',group_concat(distinct d.query_title SEPARATOR ' | ') as 'Query Title'," +
                 " group_concat(distinct d.description SEPARATOR ' | ') as 'Description',group_concat(distinct d.raisequery_raisedby SEPARATOR ' | ') as 'Queryrasiedby',group_concat(distinct e.remarks SEPARATOR ' | ') as 'Response',group_concat(distinct e.raisequery_replyby SEPARATOR ' | ') as 'Repliedby', " +
                 " group_concat(distinct d.close_remarks SEPARATOR ' | ') as 'Close remarks',group_concat(distinct d.raisequery_closedby SEPARATOR ' | ') as 'Closedby' " +
                    " from atm_trn_tauditcreation a " +                    
                    " left join atm_trn_tobservationscoresample b  on a.auditcreation_gid = b.auditcreation_gid " +                
                   " left join atm_trn_tsample2checkpoint c on c.auditcreation_gid = b.auditcreation_gid " +               
                   " left join atm_trn_tsampleraisequery d on a.auditcreation_gid = d.auditcreation_gid " +
                    " left join atm_trn_tsamplequeries2response e on d.auditcreation_gid = e.auditcreation_gid " +                                      
                     " where a.auditcreation_gid='" + auditcreation_gid + "' group by a.auditcreation_gid ";

            dt_datatable1 = objdbconn.GetDataTable(msSQL);

            //msSQL = "select auditcreation_gid from atm_trn_tsampleexcelimport where auditcreation_gid = '" + auditcreation_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == true)
            //{

            //    msSQL = "CALL Getsampleexportdata ('" + auditcreation_gid + "')";
            //    dt_datatable2 = objdbconn.GetDataTable(msSQL);
            //}

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Audit Observation Report");
            var workSheet1 = excel.Workbook.Worksheets.Add("Audit Sample Observation Report");
            //var workSheet2 = excel.Workbook.Worksheets.Add("Audit Sample Report");

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Audit Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Audit/Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                workSheet1.Cells[1, 1].LoadFromDataTable(dt_datatable1, true);
                //workSheet2.Cells[1, 1].LoadFromDataTable(dt_datatable2, true);

                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])
                using (var range1 = workSheet1.Cells[1, 1, 1, 60])
                //using (var range2 = workSheet2.Cells[1, 1, 1, 60])

                //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);

                    //range1.Style.Font.Bold = true;
                    //range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    //range1.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/Audit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }

        public void DaGetAuditReportExcelExport(MdlAtmRptAuditReports values)
        {

            msSQL = " select distinct (a.auditcreation_gid) as 'AuditRef No', (a.audit_name) as 'Audit Name',(a.audittype_name) as 'Audit Type',(a.auditobservation_name) as 'Auditobservation Name', (a.auditdepartment_name) as 'Auditdepartment Name',group_concat(distinct n.checklist_name SEPARATOR ' | ') as 'checklist name',group_concat(distinct b.riskcategory_name SEPARATOR ' | ') as 'risk catagory'," +
                " group_concat(distinct b.positiveconfirmity_name SEPARATOR ' | ') as 'positive confirmity',group_concat(distinct b.checkpointgroup_name SEPARATOR ' | ') as 'checkpointgroup',group_concat(distinct b.checkpoint_intent SEPARATOR ' | ') as 'checkpoint intent',group_concat(distinct b.checkpoint_description SEPARATOR ' | ') as 'checkpoint description'," +
          " group_concat(distinct b.noteto_auditor SEPARATOR ' | ') as 'noteto auditor',group_concat(distinct b.yes_disposition SEPARATOR ' | ') as 'yes disposition',group_concat(distinct b.no_disposition SEPARATOR ' | ') as 'no disposition',group_concat(distinct b.partial_disposition SEPARATOR ' | ') as 'partial disposition',group_concat(distinct b.na_disposition SEPARATOR ' | ') as 'na disposition'," +
         " group_concat(distinct b.yes_score SEPARATOR ' | ') as 'Yes Score',group_concat(distinct b.no_score SEPARATOR ' | ') as 'No Score',group_concat(distinct b.partial_score SEPARATOR ' | ') as 'partial score',group_concat(distinct b.na_score SEPARATOR ' | ') as 'na score', group_concat(distinct b.capture_score SEPARATOR ' | ') as 'capture score'," +
            " group_concat(distinct b.total_score SEPARATOR ' | ') as 'total score',group_concat(distinct a.overall_score SEPARATOR ' | ') as 'overall score',group_concat(distinct a.observation_score SEPARATOR ' | ') as 'observation score',group_concat(distinct a.observation_percentage SEPARATOR ' | ') as 'observation_percentage'," +
           " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as 'Created_by',group_concat(distinct d.query_title SEPARATOR ' | ') as 'Query Title',group_concat(distinct d.description SEPARATOR ' | ') as 'Description',group_concat(distinct d.raisequery_raisedby SEPARATOR ' | ') as 'Queryrasiedby',group_concat(distinct e.remarks SEPARATOR ' | ') as 'Response'," +
        " group_concat(distinct e.raisequery_replyby SEPARATOR ' | ') as 'Repliedby',group_concat(distinct d.close_remarks SEPARATOR ' | ') as 'Close remarks',group_concat(distinct d.raisequery_closedby SEPARATOR ' | ') as 'Closedby' " +
                    " from atm_trn_tauditcreation a " +
                     " left join atm_trn_tauditcreation2checklist b on a.auditcreation_gid = b.auditcreation_gid " +
                   " left join atm_trn_tauditraisequery d on a.auditcreation_gid = d.auditcreation_gid " +
                   " left join atm_trn_tchecklist2checkpoint n on a.auditcreation_gid = n.auditcreation_gid " +
                    " left join atm_trn_tauditqueries2response e on d.auditcreation_gid = e.auditcreation_gid " +
                     " left join hrm_mst_temployee g on a.created_by = g.employee_gid" +
                        " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
                    " group by a.auditcreation_gid  ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            msSQL = " select distinct (a.auditcreation_gid) as 'AuditRef No',(a.audit_name) as 'Audit Name',(a.audittype_name) as 'Audit Type',(a.auditobservation_name) as 'Auditobservation Name', (a.auditdepartment_name) as 'Auditdepartment Name', group_concat(distinct b.riskcategory_name SEPARATOR ' | ') as 'risk catagory', group_concat(distinct b.positiveconfirmity_name SEPARATOR ' | ') as 'positive confirmity',group_concat(distinct b.checkpointgroup_name SEPARATOR ' | ') as 'checkpoint group',group_concat(distinct b.checkpoint_intent SEPARATOR ' | ') as 'checkpoint intent'," +
                     " group_concat(distinct b.checkpoint_description SEPARATOR ' | ') as 'checkpoint description',group_concat(distinct b.noteto_auditor SEPARATOR ' | ') as 'Note To Auditor',group_concat(distinct b.yes_disposition SEPARATOR ' | ') as 'yes disposition',group_concat(distinct b.no_disposition SEPARATOR ' | ') as 'no disposition',group_concat(distinct b.partial_disposition SEPARATOR ' | ') as 'partial disposition',group_concat(distinct b.na_disposition SEPARATOR ' | ') as 'na disposition', " +
                 " group_concat(distinct b.yes_score SEPARATOR ' | ') as 'sample yes score',group_concat(distinct b.no_score SEPARATOR ' | ') as 'sample no score',group_concat(distinct b.partial_score SEPARATOR ' | ') as 'sample partial score', group_concat(distinct b.na_score SEPARATOR ' | ') as 'sample na score',group_concat(distinct b.total_score SEPARATOR ' | ') as 'sample total score',group_concat(distinct b.samplecapture_field SEPARATOR ' | ') as 'samplecapture field'," +
                   " group_concat(distinct b.samplecapture_score SEPARATOR ' | ') as 'samplecapture score',group_concat(distinct b.sampleobservation_score SEPARATOR ' | ') as 'sampleobservation score',group_concat(distinct b.sampleobservation_percentage SEPARATOR ' | ') as 'sampleobservation percentage',group_concat(distinct d.sample_name SEPARATOR ' | ') as 'sample name',group_concat(distinct d.query_title SEPARATOR ' | ') as 'Query Title'," +
                 " group_concat(distinct d.description SEPARATOR ' | ') as 'Description',group_concat(distinct d.raisequery_raisedby SEPARATOR ' | ') as 'Queryrasiedby',group_concat(distinct e.remarks SEPARATOR ' | ') as 'Response',group_concat(distinct e.raisequery_replyby SEPARATOR ' | ') as 'Repliedby'," +
                 " group_concat(distinct d.close_remarks SEPARATOR ' | ') as 'Close remarks',group_concat(distinct d.raisequery_closedby SEPARATOR ' | ') as 'Closedby' " +
                    " from atm_trn_tauditcreation a " +
                    " left join atm_trn_tobservationscoresample b  on a.auditcreation_gid = b.auditcreation_gid " +
                   " left join atm_trn_tsample2checkpoint c on c.auditcreation_gid = b.auditcreation_gid " +
                   " left join atm_trn_tsampleraisequery d on a.auditcreation_gid = d.auditcreation_gid " +
                    " left join atm_trn_tsamplequeries2response e on d.auditcreation_gid = e.auditcreation_gid " +              
                     " group by a.auditcreation_gid  ";

            dt_datatable1 = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Audit Observation Report");
            var workSheet1 = excel.Workbook.Worksheets.Add("Audit Sample Observation Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Audit Observation Sample Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Sample Observation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Audit/Audit Sample Observation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Audit/Audit Sample Observation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                workSheet1.Cells[1, 1].LoadFromDataTable(dt_datatable1, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])
                using (var range1 = workSheet1.Cells[1, 1, 1, 60])
                //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);


                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/Audit Sample Observation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaGetAuditVisitSamfinCustomerSummary(MdlAtmRptAuditReports values)
        {
            try
            {
                msSQL = " SELECT application_gid,concat(customer_name,' / ',customer_urn) as customer_name from ocs_trn_tcadapplication";
                                              
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditvisitreport_list = new List<auditvisitreport_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditvisitreport_list.Add(new auditvisitreport_list
                        {
                            samfincustomer_name = (dr_datarow["customer_name"].ToString()),
                            samfincustomer_gid = (dr_datarow["application_gid"].ToString()),
                           

                        });
                    }
                    values.auditvisitreport_list = getauditvisitreport_list;

                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }
        public void DaGetAuditVisitSamagroCustomerSummary(MdlAtmRptAuditReports values)
        {
            try
            {
                msSQL = " SELECT application_gid,concat(customer_name,' / ',customer_urn) as customer_name from agr_mst_tapplication";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditvisitreport_list = new List<auditvisitreport_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditvisitreport_list.Add(new auditvisitreport_list
                        {
                            samagrocustomer_name = (dr_datarow["customer_name"].ToString()),
                            samagrocustomer_gid = (dr_datarow["application_gid"].ToString()),


                        });
                    }
                    values.auditvisitreport_list = getauditvisitreport_list;

                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }
        public bool DaPostSubmitAuditVisitReport(string employee_gid, string user_gid, MdlAtmRptAuditVisitReport values)
        {

            msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
            string lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
"  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as reporting_name  " +
"  from adm_mst_tmodule2employee a " +
"  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
"  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
"  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
"  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
"  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
"  where a.module_gid ='" + lsmodulereportingto_gid + "' and b.employee_gid ='" + employee_gid + "'" +
"  group by a.employeereporting_to ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemployeereporting_to = objODBCDatareader["employeereporting_to"].ToString();
                lsreporting_name = objODBCDatareader["reporting_name"].ToString();

            }
            objODBCDatareader.Close();


            msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2person where auditvisit_gid ='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Person Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2address where auditvisit_gid ='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Address Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            //msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2document where auditvisit_gid ='" + employee_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Documents are Not Uploaded";
            //    return false;

            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //}
            //msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2photo where auditvisit_gid ='" + employee_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Photos are Not Uploaded";
            //    return false;

            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //}

            string lsaudit_refno = "VISITAUD" + DateTime.Now.ToString("yyyyMMdd");

            string msGETRef = objcmnfunctions.GetMasterGID("VISI");
            msGETRef = msGETRef.Replace("VISI", "");

            lsauditvisitref_no = lsaudit_refno + msGETRef;


            msGetGid = objcmnfunctions.GetMasterGID("AVRE");
            msSQL = " insert into atm_rpt_tauditvisitreport(" +
                    " auditvisit_gid," +
                     " auditvisitreportref_no," +
                    " auditvisit_date," +
                    " clientkmp_activities," +
                    " promoter_background," +
                    " overall_observations," +
                    " inspectingofficial_recommenation," +
                    " trading_relationship," +
                    " summary," +
                    " entity_name," +
                    " entity_gid," +
                     " samfincustomer_gid," +
                    " samfincustomer_name," +
                     " samagrocustomer_gid," +
                    " samagrocustomer_name," +
                    " draft_flag," +
                     " approval_status," +
                   " reportingmanager_gid," +
                  " reportingmanager_name," +
                    " statusupdated_by," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                   "'" + lsauditvisitref_no + "',";
            if (values.auditvisit_date == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.auditvisit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.clientkmp_activities.Replace("'", "") + "'," +
                    "'" + values.promoter_background.Replace("'", "") + "'," +
                    "'" + values.overall_observations.Replace("'", "") + "'," +
                    "'" + values.inspectingofficial_recommenation.Replace("'", "") + "'," +
                    "'" + values.trading_relationship.Replace("'", "") + "'," +
                    "'" + values.summary.Replace("'", "") + "'," +
                    "'" + values.entity_name.Replace("'", "") + "'," +
                    "'" + values.entity_gid + "',";
                 if (values.samfincustomer_gid == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.samfincustomer_gid + "',";
            }
            if (values.samfincustomer_name == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.samfincustomer_name.Replace("'", "") + "',";
            }
            if (values.samagrocustomer_gid == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.samagrocustomer_gid + "',";
            }
            if (values.samagrocustomer_name == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.samagrocustomer_name.Replace("'", "") + "',";
            }
            msSQL += "'N'," +
                  "'" + "Pending" + "'," +
                   "'" + lsemployeereporting_to + "'," +
                    "'" + lsreporting_name + "'," +
                    "'" + values.statusupdated_by + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            for (var i = 0; i < values.mstinspectingofficials.Count; i++)
            {
                msGetGid1 = objcmnfunctions.GetMasterGID("AV2O");

                msSQL = "Insert into atm_rpt_tauditvisit2inspectingofficial( " +
                       " auditvisit2inspectingofficial_gid, " +
                       " auditvisit_gid," +
                       " inspectingofficials_gid," +
                       " inspectingofficials_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid1 + "'," +
                       "'" + msGetGid + "'," +
                       "'" + values.mstinspectingofficials[i].employee_gid + "'," +
                       "'" + values.mstinspectingofficials[i].employee_name + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            for (var i = 0; i < values.mdlvisitdone.Count; i++)
            {
                msGetGid1 = objcmnfunctions.GetMasterGID("AV2D");

                msSQL = "Insert into atm_rpt_tauditvisit2visitdone( " +
                       " auditvisit2visitdone_gid, " +
                       " auditvisit_gid," +
                       " visitdone_gid," +
                       " visitdone_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid1 + "'," +
                       "'" + msGetGid + "'," +
                       "'" + values.mdlvisitdone[i].visitdone_gid + "'," +
                       "'" + values.mdlvisitdone[i].visitdone_name + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = "update atm_rpt_tauditvisit2person set auditvisit_gid ='" + msGetGid + "' where auditvisit_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2address set auditvisit_gid ='" + msGetGid + "' where auditvisit_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2document set auditvisit_gid ='" + msGetGid + "' where auditvisit_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2photo set auditvisit_gid ='" + msGetGid + "' where auditvisit_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Audit Visit Report Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured ";
                return false;
            }
        }
        public bool DaPostAuditVisitContactNo(string employee_gid, mstVisitpersoncontact_list values)
        {
           

            msGetGid = objcmnfunctions.GetMasterGID("AV2C");
            msSQL = " insert into atm_rpt_tauditvisitperson2contactno(" +
                    " auditvisitperson2contact_gid," +
                    " auditvisit2person_gid," +
                    " mobile_no," +
                    " primary_status," +
                    " whatsapp_mobileno," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.whatsapp_mobileno + "'," +
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
        public bool DaPostAuditPersonDetails(string employee_gid, mstVisitpersondtl_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AV2P");
            msSQL = " insert into atm_rpt_tauditvisit2person(" +
                    " auditvisit2person_gid," +
                    " auditvisit_gid," +
                    " clientrepresentative_name," +
                    " clientrepresentative_designationgid," +
                    " clientrepresentative_designationname," +
                    " personal_mail," +
                    " office_mail, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.clientrepresentative_name + "'," +
                    "'" + values.clientrepresentative_designationgid + "'," +
                     "'" + values.clientrepresentative_designationname + "'," +
                    "'" + values.clientrepresentative_personalmail + "'," +
                    "'" + values.clientrepresentative_officemail + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "update atm_rpt_tauditvisitperson2contactno set auditvisit2person_gid= '" + msGetGid + "' where auditvisit2person_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Visit Person Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
        public void DaGetAuditVisittmpContactList(string employee_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select auditvisitperson2contact_gid,mobile_no,primary_status,whatsapp_mobileno from atm_rpt_tauditvisitperson2contactno where " +
              " auditvisit2person_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersoncontact_list = new List<mstVisitpersoncontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersoncontact_list.Add(new mstVisitpersoncontact_list
                    {
                        auditvisitperson2contact_gid = (dr_datarow["auditvisitperson2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString())
                    });
                }
                values.mstVisitpersoncontact_list = getmstVisitpersoncontact_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public bool DaPostAuditVisitAddress(string employee_gid, mstVisitpersonaddress_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AV2A");
            msSQL = " insert into atm_rpt_tauditvisit2address(" +
                    " auditvisit2address_gid," +
                    " auditvisit_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " primary_status," +
                    " address_line1, " +
                    " address_line2, " +
                    " landmark, " +
                    " postal_code, " +
                    " city, " +
                    " taluk, " +
                    " district, " +
                    " state_name, " +
                    " country, " +
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
                    "'" + values.address_line1 + "'," +
                    "'" + values.address_line2 + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluk + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state_name + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = " Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured ";
                return false;
            }
        }

        public void DaPostAuditVisitDocumentUpload(HttpRequest httpRequest, UploadDocumentList objfilename, string employee_gid)
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
            string document_name = httpRequest.Form["document_name"];
            string project_flag = httpRequest.Form["project_flag"].ToString();

            String path = lspath;



            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            // path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "CST/VisitDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "AuditVisitReport/AuditVisitDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "AuditVisitReport/AuditVisitDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "AuditVisitReport/AuditVisitDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("AVDU");
                        msSQL = " insert into atm_rpt_tauditvisit2document( " +
                                    " auditvisit2document_gid ," +
                                    " auditvisit_gid," +
                                    " file_name ," +
                                    " document_path," +
                                    " document_name," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                    "'" + document_name.Replace("'", "") + "'," +
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
        public void DaGetAuditVisittmpDocumentList(string employee_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2document_gid,document_name,document_path,file_name  from atm_rpt_tauditvisit2document where " +
              " auditvisit_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadDocumentList = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filesname = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadDocumentList.Add(new UploadDocumentList
                    {
                        auditvisit2document_gid = (dr_datarow["auditvisit2document_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadDocumentList = getUploadDocumentList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetAuditVisittmpPersondtlList(string employee_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2person_gid,clientrepresentative_name,clientrepresentative_designationgid,clientrepresentative_designationname,personal_mail,office_mail from atm_rpt_tauditvisit2person where " +
              " auditvisit_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersondtl_list = new List<mstVisitpersondtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersondtl_list.Add(new mstVisitpersondtl_list
                    {
                        auditvisit2person_gid = (dr_datarow["auditvisit2person_gid"].ToString()),
                        clientrepresentative_name = (dr_datarow["clientrepresentative_name"].ToString()),
                        clientrepresentative_designationgid = (dr_datarow["clientrepresentative_designationgid"].ToString()),
                        clientrepresentative_designationname = (dr_datarow["clientrepresentative_designationname"].ToString()),
                        clientrepresentative_personalmail = (dr_datarow["personal_mail"].ToString()),
                        clientrepresentative_officemail = (dr_datarow["office_mail"].ToString())
                    });
                }
                values.mstVisitpersondtl_list = getmstVisitpersondtl_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetAuditVisittmpAddressList(string employee_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,landmark,postal_code,city,taluk,district,state_name,country,latitude,longitude from atm_rpt_tauditvisit2address where " +
              " auditvisit_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersonaddress_list = new List<mstVisitpersonaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersonaddress_list.Add(new mstVisitpersonaddress_list
                    {
                        auditvisit2address_gid = (dr_datarow["auditvisit2address_gid"].ToString()),
                        addresstype_gid = (dr_datarow["addresstype_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        address_line1 = (dr_datarow["address_line1"].ToString()),
                        address_line2 = (dr_datarow["address_line2"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        taluk = (dr_datarow["taluk"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstVisitpersonaddress_list = getmstVisitpersonaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetVisitReportList(MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,auditvisitreportref_no,concat(samfincustomer_name,samagrocustomer_name) as customer_name,draft_flag,approval_status,a.entity_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.auditvisit_date,'%d-%m-%Y') as auditvisit_date,a.auditvisit_gid  from atm_rpt_tauditvisitreport a " +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join  adm_mst_tuser c on c.user_gid=b.user_gid " +
                 " where approval_status = 'Pending' or draft_flag='Y' order by auditvisit_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getVisitReportList = new List<VisitReportList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getVisitReportList.Add(new VisitReportList
                    {
                        draft_flag = (dr_datarow["draft_flag"]).ToString(),
                        visitreport_gid = (dr_datarow["auditvisit_gid"].ToString()),
                        visitreport_date = (dr_datarow["auditvisit_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        entity_name = (dr_datarow["entity_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        auditvisitreportref_no = (dr_datarow["auditvisitreportref_no"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString())
                    });
                }
                values.VisitReportList = getVisitReportList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaDeleteVisittmpDocument(string employee_gid, UploadDocumentList values)
        {
            msSQL = "delete from atm_rpt_tauditvisit2document where auditvisit_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaDeleteVisittmpPhoto(string employee_gid, UploadphotoList values)
        {
            msSQL = "delete from atm_rpt_tauditvisit2photo where auditvisit_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Photo Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured ";
                values.status = false;

            }
        }
        public void DaDeleteVisittmpContact(string employee_gid, mstVisitpersoncontact_list values)
        {
            msSQL = "delete from atm_rpt_tauditvisitperson2contactno where auditvisit2person_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Visit Contact Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaDeleteVisittmppersondtl(string employee_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select auditvisit2person_gid from atm_rpt_tauditvisit2person where " +
                    " auditvisit_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = "delete from atm_rpt_tauditvisitperson2contactno where auditvisit2person_gid='" + dr_datarow["auditvisit2person_gid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

            }
            dt_datatable.Dispose();
            msSQL = "delete from atm_rpt_tauditvisit2person where auditvisit_gid ='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Visit Person Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaDeleteVisittmpAddress(string employee_gid, mstVisitpersonaddress_list values)
        {
            msSQL = "delete from atm_rpt_tauditvisit2address where auditvisit_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public bool DaAuditVisitPhotoUpload(HttpRequest httpRequest, UploadDocumentList objfilename, string employee_gid)
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
            string photo_name = httpRequest.Form["photo_name"];
            string project_flag = httpRequest.Form["project_flag"].ToString();


            String path = lspath;



            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "CST/Visitphotos/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "AuditVisitReport/AuditVisitphotos/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPPD");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();

                        if ((FileExtension == ".jpg") || (FileExtension == ".jpeg") || (FileExtension == ".png") || (FileExtension == ".tif") || (FileExtension == ".tiff") || (FileExtension == ".jfif") || (FileExtension == ".gif"))
                        {

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
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "AuditVisitReport/AuditVisitphotos/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "erpdocument" + "/" + lscompany_code + "/" + "AuditVisitReport/AuditVisitphotos/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            msGetGid = objcmnfunctions.GetMasterGID("AVPD");

                            msSQL = " insert into atm_rpt_tauditvisit2photo( " +
                                  " auditvisit2photo_gid ," +
                                  " auditvisit_gid," +
                                  " file_name," +
                                  " visitphoto_name," +
                                  " visitphoto_path, " +
                                  " created_by ," +
                                  " created_date " +
                                  " )values(" +
                                  "'" + msGetGid + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                                  "'" + photo_name.Replace("'", " ") + "'," +
                                  "'" + lspath + msdocument_gid + FileExtension.Replace("'", " ") + "'," +
                                  "'" + employee_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                    }

                    if (mnResult != 0)
                    {
                        objfilename.status = true;
                        objfilename.message = "Photo Uploaded Successfully";
                        return true;
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "Error Occured while uploading photo";
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
                return false;
            }

        }
        public bool DaPostAuditPersonDetailsUpdate(string employee_gid, mstVisitpersondtl_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("A2CN");

            msSQL = " update atm_rpt_tauditvisit2person set clientrepresentative_name='" + values.clientrepresentative_name + "'," +
                   "  clientrepresentative_designationgid='" + values.clientrepresentative_designationgid + "',clientrepresentative_designationname='" + values.clientrepresentative_designationname + "',personal_mail='" + values.clientrepresentative_personalmail + "'," +
                   " office_mail='" + values.clientrepresentative_officemail + "', updated_by='" + employee_gid + "',updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where auditvisit2person_gid='" + values.auditvisit2person_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "update atm_rpt_tauditvisitperson2contactno set auditvisit2person_gid= '" + values.auditvisit2person_gid + "' where auditvisit2person_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Visit Person Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
        public void DaGetAuditVisitContactList(string employee_gid, string auditvisit2person_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select auditvisitperson2contact_gid,mobile_no,primary_status,whatsapp_mobileno from atm_rpt_tauditvisitperson2contactno where " +
              " auditvisit2person_gid='" + auditvisit2person_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersoncontact_list = new List<mstVisitpersoncontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersoncontact_list.Add(new mstVisitpersoncontact_list
                    {
                        auditvisitperson2contact_gid = (dr_datarow["auditvisitperson2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString())
                    });
                }
                values.mstVisitpersoncontact_list = getmstVisitpersoncontact_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaEditAuditVisitpersondtl(string auditvisit2person_gid, mstVisitpersondtl_list values)
        {
            try
            {
                msSQL = "select auditvisit2person_gid,clientrepresentative_name,clientrepresentative_designationgid,clientrepresentative_designationname," +
                       " personal_mail,office_mail from atm_rpt_tauditvisit2person where " +
                       " auditvisit2person_gid='" + auditvisit2person_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.auditvisit2person_gid = objODBCDatareader["auditvisit2person_gid"].ToString();
                    values.clientrepresentative_name = objODBCDatareader["clientrepresentative_name"].ToString();
                    values.clientrepresentative_designationgid = objODBCDatareader["clientrepresentative_designationgid"].ToString();
                    values.clientrepresentative_designationname = objODBCDatareader["clientrepresentative_designationname"].ToString();
                    values.clientrepresentative_personalmail = objODBCDatareader["personal_mail"].ToString();
                    values.clientrepresentative_officemail = objODBCDatareader["office_mail"].ToString();

                    msSQL = "select auditvisitperson2contact_gid,mobile_no,primary_status,whatsapp_mobileno from atm_rpt_tauditvisitperson2contactno where " +
                           " auditvisit2person_gid='" + values.auditvisit2person_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmstVisitpersoncontact_list = new List<mstVisitpersoncontact_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmstVisitpersoncontact_list.Add(new mstVisitpersoncontact_list
                            {
                                auditvisitperson2contact_gid = (dr_datarow["auditvisitperson2contact_gid"].ToString()),
                                mobile_no = (dr_datarow["mobile_no"].ToString()),
                                primary_status = (dr_datarow["primary_status"].ToString()),
                                whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString())
                            });
                        }
                        values.mstVisitpersoncontact_list = getmstVisitpersoncontact_list;
                    }
                    dt_datatable.Dispose();
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
        public void DaDeleteAuditVisittmpAddressList(string auditvisit2address_gid, mstVisitpersonaddress_list values)
        {
            msSQL = "delete from atm_rpt_tauditvisit2address where auditvisit2address_gid='" + auditvisit2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaDeleteAuditVisittmpContactList(string auditvisitperson2contact_gid, mstVisitpersoncontact_list values)
        {
            msSQL = "delete from atm_rpt_tauditvisitperson2contactno where auditvisitperson2contact_gid='" + auditvisitperson2contact_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Visit Contact Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaDeleteAuditVisittmppersondtlList(string auditvisit2person_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select auditvisitperson2contact_gid from atm_rpt_tauditvisitperson2contactno where " +
                    " auditvisit2person_gid='" + auditvisit2person_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = "delete from atm_rpt_tauditvisitperson2contactno where auditvisitperson2contact_gid='" + dr_datarow["auditvisitperson2contact_gid"].ToString() + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

            }
            dt_datatable.Dispose();
            msSQL = "delete from atm_rpt_tauditvisit2person where auditvisit2person_gid='" + auditvisit2person_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = " Visit Person Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured ";
                values.status = false;

            }
        }
        public void DaGetAuditVisitReportDtls(string auditvisit_gid, MdlAtmRptAuditVisitReport values)
        {
            try
            {
                msSQL = " select date_format(a.auditvisit_date,'%d-%m-%Y') as auditvisit_date, a.clientkmp_activities, a.promoter_background,entity_name,samfincustomer_name,samagrocustomer_name, " +
               " a.overall_observations, a.inspectingofficial_recommenation, a.trading_relationship,reportingmanager_name, summary, GROUP_CONCAT(distinct(b.inspectingofficials_name) SEPARATOR ', ') as inspectingofficials_name, " +
               " GROUP_CONCAT(distinct(c.visitdone_name) SEPARATOR ', ') as visitdone_name " +
               " from atm_rpt_tauditvisitreport a " +
               " left join atm_rpt_tauditvisit2inspectingofficial b on a.auditvisit_gid = b.auditvisit_gid" +
               " left join atm_rpt_tauditvisit2visitdone c on c.auditvisit_gid = a.auditvisit_gid " +
               " where a.auditvisit_gid='" + auditvisit_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.auditvisit_date = objODBCDatareader["auditvisit_date"].ToString();
                    values.inspectingofficials_name = objODBCDatareader["inspectingofficials_name"].ToString();
                    values.visitdone_name = objODBCDatareader["visitdone_name"].ToString();
                    values.clientkmp_activities = objODBCDatareader["clientkmp_activities"].ToString();
                    values.promoter_background = objODBCDatareader["promoter_background"].ToString();
                    values.overall_observations = objODBCDatareader["overall_observations"].ToString();
                    values.inspectingofficial_recommenation = objODBCDatareader["inspectingofficial_recommenation"].ToString();
                    values.trading_relationship = objODBCDatareader["trading_relationship"].ToString();
                    values.summary = objODBCDatareader["summary"].ToString();
                    values.entity_name = objODBCDatareader["entity_name"].ToString();
                    values.samfincustomer_name = objODBCDatareader["samfincustomer_name"].ToString();
                    values.samagrocustomer_name = objODBCDatareader["samagrocustomer_name"].ToString();
                    values.reportingmanager_name = objODBCDatareader["reportingmanager_name"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = "select auditvisit2person_gid,clientrepresentative_name,clientrepresentative_designationname,personal_mail,office_mail from atm_rpt_tauditvisit2person where " +
                        " auditvisit_gid='" + auditvisit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstVisitpersondtl_list = new List<mstVisitpersondtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstVisitpersondtl_list.Add(new mstVisitpersondtl_list
                        {
                            auditvisit2person_gid = (dr_datarow["auditvisit2person_gid"].ToString()),
                            clientrepresentative_name = (dr_datarow["clientrepresentative_name"].ToString()),
                            clientrepresentative_designationname = (dr_datarow["clientrepresentative_designationname"].ToString()),
                            clientrepresentative_personalmail = (dr_datarow["personal_mail"].ToString()),
                            clientrepresentative_officemail = (dr_datarow["office_mail"].ToString())
                        });
                    }
                    values.mstVisitpersondtl_list = getmstVisitpersondtl_list;
                }
                dt_datatable.Dispose();


                msSQL = "select auditvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,landmark,postal_code,city,taluk,district,state_gid,state_name,latitude,longitude,country from atm_rpt_tauditvisit2address where " +
                        " auditvisit_gid='" + auditvisit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstVisitpersonaddress_list = new List<mstVisitpersonaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstVisitpersonaddress_list.Add(new mstVisitpersonaddress_list
                        {
                           auditvisit2address_gid = (dr_datarow["auditvisit2address_gid"].ToString()),
                            addresstype_gid = (dr_datarow["addresstype_gid"].ToString()),
                            addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            address_line1 = (dr_datarow["address_line1"].ToString()),
                            address_line2 = (dr_datarow["address_line2"].ToString()),
                            landmark = (dr_datarow["landmark"].ToString()),
                            postal_code = (dr_datarow["postal_code"].ToString()),
                            city = (dr_datarow["city"].ToString()),
                            taluk = (dr_datarow["taluk"].ToString()),
                            district = (dr_datarow["district"].ToString()),
                            state_name = (dr_datarow["state_name"].ToString()),
                            country = (dr_datarow["country"].ToString()),
                            latitude = (dr_datarow["latitude"].ToString()),
                            longitude = (dr_datarow["longitude"].ToString()),
                        });
                    }
                    values.mstVisitpersonaddress_list = getmstVisitpersonaddress_list;
                }
                dt_datatable.Dispose();


                msSQL = "select auditvisit2document_gid,document_name,document_path,file_name,date_format(created_date,'%d-%m-%Y') as created_date  from atm_rpt_tauditvisit2document where " +
                        " auditvisit_gid='" + auditvisit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getUploadDocumentList = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    var file_name = new List<string>();
                    var file_path = string.Empty;

                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        file_name.Add(dt["document_name"].ToString());
                        file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                    }
                    values.filesname = file_name.ToArray();
                    values.filepath = file_path.ToString();
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getUploadDocumentList.Add(new UploadDocumentList
                        {
                            auditvisit2document_gid = (dr_datarow["auditvisit2document_gid"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            filename = (dr_datarow["file_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString())
                        });
                    }
                    values.UploadDocumentList = getUploadDocumentList;
                }
                dt_datatable.Dispose();


                msSQL = "select auditvisit2photo_gid,visitphoto_name,visitphoto_path,file_name,date_format(created_date,'%d-%m-%Y') as created_date  from atm_rpt_tauditvisit2photo where " +
                        " auditvisit_gid='" + auditvisit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getUploadphotoList = new List<UploadphotoList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getUploadphotoList.Add(new UploadphotoList
                        {
                            auditvisit2photo_gid = (dr_datarow["auditvisit2photo_gid"].ToString()),
                            photo_name = (dr_datarow["visitphoto_name"].ToString()),
                            document_path = objcmnstorage.EncryptData(dr_datarow["visitphoto_path"].ToString()),
                            filename = (dr_datarow["file_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString())
                        });
                    }
                    values.UploadphotoList = getUploadphotoList;
                }
                dt_datatable.Dispose();

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }
        public void DaGetAuditVisitDocumentList(string employee_gid, string auditvisit_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2document_gid,document_name,document_path,file_name  from atm_rpt_tauditvisit2document where " +
              " auditvisit_gid='" + auditvisit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadDocumentList = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filesname = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadDocumentList.Add(new UploadDocumentList
                    {
                        auditvisit2document_gid = (dr_datarow["auditvisit2document_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadDocumentList = getUploadDocumentList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaEditAuditVisitReport(string auditvisit_gid, MdlAtmRptAuditVisitReport values)
        {
            try
            {
                msSQL = "select auditvisit_gid,entity_gid,date_format(auditvisit_date,'%d-%m-%Y') as auditvisit_date,clientkmp_activities," +
                    " promoter_background,approval_status,overall_observations,inspectingofficial_recommenation,trading_relationship,entity_name,samfincustomer_name,samagrocustomer_name,samagrocustomer_gid,samfincustomer_gid," +
                    " summary,draft_flag from atm_rpt_tauditvisitreport where " +
                       " auditvisit_gid='" + auditvisit_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.auditvisit_gid = objODBCDatareader["auditvisit_gid"].ToString();
                    values.auditvisit_date = objODBCDatareader["auditvisit_date"].ToString();
                    values.clientkmp_activities = objODBCDatareader["clientkmp_activities"].ToString();
                    values.promoter_background = objODBCDatareader["promoter_background"].ToString();
                    values.overall_observations = objODBCDatareader["overall_observations"].ToString();
                    values.inspectingofficial_recommenation = objODBCDatareader["inspectingofficial_recommenation"].ToString();
                    values.trading_relationship = objODBCDatareader["trading_relationship"].ToString();
                    values.summary = objODBCDatareader["summary"].ToString();
                    values.draft_flag = objODBCDatareader["draft_flag"].ToString();
                    values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                    values.entity_name = objODBCDatareader["entity_name"].ToString();
                    values.samagrocustomer_gid = objODBCDatareader["samagrocustomer_gid"].ToString();
                    values.samfincustomer_gid = objODBCDatareader["samfincustomer_gid"].ToString();
                    values.samfincustomer_name = objODBCDatareader["samfincustomer_name"].ToString();
                    values.samagrocustomer_name = objODBCDatareader["samagrocustomer_name"].ToString();
                    values.approval_status = objODBCDatareader["approval_status"].ToString();
                    msSQL = " select inspectingofficials_gid,inspectingofficials_name,auditvisit2inspectingofficial_gid from atm_rpt_tauditvisit2inspectingofficial " +
                    " where auditvisit_gid='" + auditvisit_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmstinspectingofficials = new List<mstinspectingofficials>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getmstinspectingofficials.Add(new mstinspectingofficials
                            {
                                employee_gid = dt["inspectingofficials_gid"].ToString(),
                                employee_name = dt["inspectingofficials_name"].ToString(),
                                auditvisit2inspectingofficial_gid = dt["auditvisit2inspectingofficial_gid"].ToString(),
                            });
                            values.mstinspectingofficials = getmstinspectingofficials;
                        }
                    }
                    dt_datatable.Dispose();


                    msSQL = " select visitdone_name,visitdone_gid,auditvisit2visitdone_gid from atm_rpt_tauditvisit2visitdone " +
                 " where auditvisit_gid='" + auditvisit_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getvisitdone_list = new List<visitdone_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getvisitdone_list.Add(new visitdone_list
                            {
                                visitdone_gid = dt["visitdone_gid"].ToString(),
                                visitdone_name = dt["visitdone_name"].ToString(),
                                auditvisit2visitdone_gid = dt["auditvisit2visitdone_gid"].ToString(),
                            });
                            values.visitdone_list = getvisitdone_list;
                        }
                    }
                    dt_datatable.Dispose();

                    msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name, " +
                               " b.employee_gid from adm_mst_tuser a " +
                            " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                            " where user_status<>'N' order by a.user_firstname asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getemployeeList = new List<inspectemployee_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getemployeeList.Add(new inspectemployee_list
                            {
                                employee_gid = dt["employee_gid"].ToString(),
                                employee_name = dt["employee_name"].ToString(),
                            });
                            values.employeelist = getemployeeList;
                        }
                    }
                    dt_datatable.Dispose();

                    msSQL = " SELECT visitedplace_gid,visitedplace_name FROM ocs_mst_tvisitedplace order by visitedplace_gid asc ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmdlvisitdone = new List<mdlvisitdone>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmdlvisitdone.Add(new mdlvisitdone
                            {
                                visitdone_gid = (dr_datarow["visitedplace_gid"].ToString()),
                                visitdone_name = (dr_datarow["visitedplace_name"].ToString())
                            });
                        }
                        values.mdlvisitdone = getmdlvisitdone;
                    }
                    dt_datatable.Dispose();
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
        public void DaGetAuditVisitPersondtlList(string employee_gid, string auditvisit_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2person_gid,clientrepresentative_name,clientrepresentative_designationgid,clientrepresentative_designationname,personal_mail,office_mail from atm_rpt_tauditvisit2person where " +
              " auditvisit_gid='" + auditvisit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersondtl_list = new List<mstVisitpersondtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersondtl_list.Add(new mstVisitpersondtl_list
                    {
                        auditvisit2person_gid = (dr_datarow["auditvisit2person_gid"].ToString()),
                        clientrepresentative_name = (dr_datarow["clientrepresentative_name"].ToString()),
                        clientrepresentative_designationgid = (dr_datarow["clientrepresentative_designationgid"].ToString()),
                        clientrepresentative_designationname = (dr_datarow["clientrepresentative_designationname"].ToString()),
                        clientrepresentative_personalmail = (dr_datarow["personal_mail"].ToString()),
                        clientrepresentative_officemail = (dr_datarow["office_mail"].ToString())
                    });
                }
                values.mstVisitpersondtl_list = getmstVisitpersondtl_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetAuditVisitAddressList(string employee_gid, string auditvisit_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,landmark,postal_code,city,taluk,district,state_name,latitude,longitude,country from atm_rpt_tauditvisit2address where " +
              " auditvisit_gid='" + auditvisit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersonaddress_list = new List<mstVisitpersonaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersonaddress_list.Add(new mstVisitpersonaddress_list
                    {
                        auditvisit2address_gid = (dr_datarow["auditvisit2address_gid"].ToString()),
                        addresstype_gid = (dr_datarow["addresstype_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        address_line1 = (dr_datarow["address_line1"].ToString()),
                        address_line2 = (dr_datarow["address_line2"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        taluk = (dr_datarow["taluk"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstVisitpersonaddress_list = getmstVisitpersonaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetAuditVisitPhotosList(string employee_gid, string auditvisit_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2photo_gid,visitphoto_name,visitphoto_path,file_name  from atm_rpt_tauditvisit2photo where " +
             " auditvisit_gid='" + auditvisit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadphotoList = new List<UploadphotoList>();
            if (dt_datatable.Rows.Count != 0)
            {
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["visitphoto_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["visitphoto_path"].ToString());
                }
                values.filesname = file_name.ToArray();
                values.filepath = file_path.ToString();
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadphotoList.Add(new UploadphotoList
                    {
                        auditvisit2photo_gid = (dr_datarow["auditvisit2photo_gid"].ToString()),
                        photo_name = (dr_datarow["visitphoto_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["visitphoto_path"].ToString()),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadphotoList = getUploadphotoList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaEditAuditVisitaddress(string auditvisit2address_gid, mstVisitpersonaddress_list values)
        {
            try
            {
                msSQL = "select auditvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,latitude, longitude,landmark,postal_code,city,taluk,district,state_name,country from atm_rpt_tauditvisit2address where " +
                       " auditvisit2address_gid='" + auditvisit2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.auditvisit2address_gid = objODBCDatareader["auditvisit2address_gid"].ToString();
                    values.addresstype_gid = objODBCDatareader["addresstype_gid"].ToString();
                    values.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.address_line1 = objODBCDatareader["address_line1"].ToString();
                    values.address_line2 = objODBCDatareader["address_line2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.taluk = objODBCDatareader["taluk"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state_name = objODBCDatareader["state_name"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
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
        public void DaGetEditAuditVisitPersondtlList(string employee_gid, string auditvisit_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2person_gid,clientrepresentative_name,clientrepresentative_designationgid,clientrepresentative_designationname,personal_mail,office_mail from atm_rpt_tauditvisit2person where " +
              " auditvisit_gid='" + auditvisit_gid + "' or auditvisit_gid='" + employee_gid + "' order by auditvisit2person_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersondtl_list = new List<mstVisitpersondtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersondtl_list.Add(new mstVisitpersondtl_list
                    {
                        auditvisit2person_gid = (dr_datarow["auditvisit2person_gid"].ToString()),
                        clientrepresentative_name = (dr_datarow["clientrepresentative_name"].ToString()),
                        clientrepresentative_designationgid = (dr_datarow["clientrepresentative_designationgid"].ToString()),
                        clientrepresentative_designationname = (dr_datarow["clientrepresentative_designationname"].ToString()),
                        clientrepresentative_personalmail = (dr_datarow["personal_mail"].ToString()),
                        clientrepresentative_officemail = (dr_datarow["office_mail"].ToString())
                    });
                }
                values.mstVisitpersondtl_list = getmstVisitpersondtl_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetVisitedplace(MdlAtmRptAuditVisitReport values)
        {
            try
            {
                msSQL = " SELECT visitedplace_gid,visitedplace_name FROM ocs_mst_tvisitedplace order by visitedplace_gid asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmdlvisitdone = new List<mdlvisitdone>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmdlvisitdone.Add(new mdlvisitdone
                        {
                            visitdone_gid = (dr_datarow["visitedplace_gid"].ToString()),
                            visitdone_name = (dr_datarow["visitedplace_name"].ToString())
                        });
                    }
                    values.mdlvisitdone = getmdlvisitdone;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetEditAuditVisitContactList(string employee_gid, string auditvisit2person_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select auditvisitperson2contact_gid,mobile_no,primary_status,whatsapp_mobileno from atm_rpt_tauditvisitperson2contactno where " +
              " auditvisit2person_gid='" + auditvisit2person_gid + "'  or auditvisit2person_gid='" + employee_gid + "' order by auditvisitperson2contact_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersoncontact_list = new List<mstVisitpersoncontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersoncontact_list.Add(new mstVisitpersoncontact_list
                    {
                        auditvisitperson2contact_gid = (dr_datarow["auditvisitperson2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString())
                    });
                }
                values.mstVisitpersoncontact_list = getmstVisitpersoncontact_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetAuditVisittmpPhotosList(string employee_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2photo_gid,visitphoto_name,visitphoto_path,file_name  from atm_rpt_tauditvisit2photo where " +
              " auditvisit_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadphotoList = new List<UploadphotoList>();
            if (dt_datatable.Rows.Count != 0)
            {
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["visitphoto_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["visitphoto_path"].ToString());
                }
                values.filesname = file_name.ToArray();
                values.filepath = file_path.ToString();
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadphotoList.Add(new UploadphotoList
                    {
                        auditvisit2photo_gid = (dr_datarow["auditvisit2photo_gid"].ToString()),
                        photo_name = (dr_datarow["visitphoto_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["visitphoto_path"].ToString()),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadphotoList = getUploadphotoList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaDeleteAuditVisittmpPhotoList(string auditvisit2photo_gid, UploadphotoList values)
        { 
            msSQL = "delete from atm_rpt_tauditvisit2photo where auditvisit2photo_gid='" + auditvisit2photo_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Photo Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public bool DaPostAuditVisitReport(string employee_gid, string user_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
            string lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
"  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as reporting_name  " +
"  from adm_mst_tmodule2employee a " +
"  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
"  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
"  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
"  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
"  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
"  where a.module_gid ='" + lsmodulereportingto_gid + "' and b.employee_gid ='" + employee_gid + "'" +
"  group by a.employeereporting_to ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemployeereporting_to = objODBCDatareader["employeereporting_to"].ToString();
                lsreporting_name = objODBCDatareader["reporting_name"].ToString();

            }
            objODBCDatareader.Close();

            string lsaudit_refno = "VISITAUD" + DateTime.Now.ToString("yyyyMMdd");

            string msGETRef = objcmnfunctions.GetMasterGID("VISI");
            msGETRef = msGETRef.Replace("VISI", "");

            lsauditvisitref_no = lsaudit_refno + msGETRef;

            msGetGid = objcmnfunctions.GetMasterGID("AVRE");
            msSQL = " insert into atm_rpt_tauditvisitreport(" +
                    " auditvisit_gid," +
                     " auditvisitreportref_no," +
                    " auditvisit_date," +
                    " clientkmp_activities," +
                    " promoter_background," +
                    " overall_observations," +
                    " inspectingofficial_recommenation," +
                    " trading_relationship," +
                    " summary," +
                     " entity_name," +
                      " entity_gid," +
                       " samfincustomer_gid," +
                        " samfincustomer_name," +
                         " samagrocustomer_gid," +
                          " samagrocustomer_name," +
                    " draft_flag," +
                     " approval_status," +
                      " reportingmanager_gid," +
                        " reportingmanager_name," +
                    " statusupdated_by," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
            "'" + lsauditvisitref_no + "',";
            if (values.auditvisit_date == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.auditvisit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.clientkmp_activities == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.clientkmp_activities.Replace("'", "") + "',";
            }
            if (values.promoter_background == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.promoter_background.Replace("'", "") + "',";
            }
            if (values.overall_observations == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.overall_observations.Replace("'", "") + "',";
            }

            if (values.inspectingofficial_recommenation == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.inspectingofficial_recommenation.Replace("'", "") + "',";
            }
            if (values.trading_relationship == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.trading_relationship.Replace("'", "") + "',";
            }
            if (values.summary == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.summary.Replace("'", "") + "',";
            }
            if (values.entity_name == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.entity_name.Replace("'", "") + "',";
            }
            if (values.entity_gid == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.entity_gid + "',";
            }
            if (values.samfincustomer_gid == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.samfincustomer_gid + "',";
            }
            if (values.samfincustomer_name == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.samfincustomer_name.Replace("'", "") + "',";
            }
            if (values.samagrocustomer_gid == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.samagrocustomer_gid + "',";
            }
            if (values.samagrocustomer_name == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.samagrocustomer_name.Replace("'", "") + "',";
            }

            msSQL += "'Y'," +
                 "'" + "Draft" + "'," +
                 "'" + lsemployeereporting_to + "'," +
                    "'" + lsreporting_name + "'," +
                    "'" + values.statusupdated_by + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (values.mstinspectingofficials == null)
            {

            }
            else
            {
                for (var i = 0; i < values.mstinspectingofficials.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("AV2O");

                    msSQL = "Insert into atm_rpt_tauditvisit2inspectingofficial( " +
                           " auditvisit2inspectingofficial_gid, " +
                           " auditvisit_gid," +
                           " inspectingofficials_gid," +
                           " inspectingofficials_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (values.mdlvisitdone == null)
            {

            }
            else
            {
                for (var i = 0; i < values.mdlvisitdone.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("AV2D");

                    msSQL = "Insert into atm_rpt_tauditvisit2visitdone( " +
                           " auditvisit2visitdone_gid, " +
                           " auditvisit_gid," +
                           " visitdone_gid," +
                           " visitdone_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = "update atm_rpt_tauditvisit2person set auditvisit_gid ='" + msGetGid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2address set auditvisit_gid ='" + msGetGid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2document set auditvisit_gid ='" + msGetGid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2photo set auditvisit_gid ='" + msGetGid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Audit Visit Report Saved Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured ";
                return false;
            }
        }
        public bool DaPostUpdateAuditVisitReportUpdate(string employee_gid, string user_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
            string lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
"  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as reporting_name  " +
"  from adm_mst_tmodule2employee a " +
"  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
"  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
"  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
"  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
"  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
"  where a.module_gid ='" + lsmodulereportingto_gid + "' and b.employee_gid ='" + employee_gid + "'" +
"  group by a.employeereporting_to ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemployeereporting_to = objODBCDatareader["employeereporting_to"].ToString();
                lsreporting_name = objODBCDatareader["reporting_name"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2person where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Person Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2address where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Address Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            //msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2document where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Documents are Not Uploaded";
            //    return false;

            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //}
            //msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2photo where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Photos are Not Uploaded";
            //    return false;

            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //}
            msSQL = "select date_format(auditvisit_date,'%d-%m-%Y') as auditvisit_date from atm_rpt_tauditvisitreport where auditvisit_gid='" + values.auditvisit_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (values.auditvisit_date == (objODBCDatareader["auditvisit_date"].ToString()))
                {
                    values.auditvisit_date = "exist";
                }
                objODBCDatareader.Close();

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "update atm_rpt_tauditvisitreport set " +
                     " clientkmp_activities='" + values.clientkmp_activities.Replace("'", "") + "'," +
                     " promoter_background='" + values.promoter_background.Replace("'", "") + "'," +
                     " overall_observations='" + values.overall_observations.Replace("'", "") + "'," +
                     " inspectingofficial_recommenation='" + values.inspectingofficial_recommenation.Replace("'", "") + "'," +
                     " trading_relationship='" + values.trading_relationship.Replace("'", "") + "'," +
                     " summary='" + values.summary.Replace("'", "") + "'," +
                      " entity_name='" + values.entity_name.Replace("'", "") + "'," +
                     " entity_gid='" + values.entity_gid + "'," +
                     " samfincustomer_name='" + values.samfincustomer_name.Replace("'", "") + "'," +
                     " samfincustomer_gid='" + values.samfincustomer_gid + "'," +
                     " samagrocustomer_name='" + values.samagrocustomer_name.Replace("'", "") + "'," +
                     " samagrocustomer_gid='" + values.samagrocustomer_gid + "'," +
                      " reportingmanager_name='" + lsreporting_name + "'," +
                     " reportingmanager_gid='" + lsemployeereporting_to + "'," +
                     " draft_flag='N'," +
                      " approval_status='Pending'," +
                     " statusupdated_by = '" + values.statusupdated_by + "', ";
           

            if (values.auditvisit_date == null)
            {
                msSQL += "auditvisit_date=null,";
            }
            else if (values.auditvisit_date == "exist")
            {

            }
            else if (Convert.ToDateTime(values.auditvisit_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += "auditvisit_date='" + Convert.ToDateTime(values.auditvisit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "updated_by='" + employee_gid + "'," +
                     "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where auditvisit_gid='" + values.auditvisit_gid + "' ";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from atm_rpt_tauditvisit2inspectingofficial where auditvisit_gid ='" + values.auditvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                for (var i = 0; i < values.mstinspectingofficials.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("AV2O");

                    msSQL = "Insert into atm_rpt_tauditvisit2inspectingofficial( " +
                           " auditvisit2inspectingofficial_gid, " +
                           " auditvisit_gid," +
                           " inspectingofficials_gid," +
                           " inspectingofficials_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.auditvisit_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " delete from atm_rpt_tauditvisit2visitdone where auditvisit_gid ='" + values.auditvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                for (var i = 0; i < values.mdlvisitdone.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("AV2D");

                    msSQL = "Insert into atm_rpt_tauditvisit2visitdone( " +
                           " auditvisit2visitdone_gid, " +
                           " auditvisit_gid," +
                           " visitdone_gid," +
                           " visitdone_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.auditvisit_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = "update atm_rpt_tauditvisit2person set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2address set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2document set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2photo set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Audit Visit Report Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
        public void DaGetEditAuditVisitAddressList(string employee_gid, string auditvisit_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,landmark,postal_code,city,taluk,district,state_name,latitude,longitude,country from atm_rpt_tauditvisit2address where " +
              " auditvisit_gid='" + auditvisit_gid + "' or auditvisit_gid='" + employee_gid + "' order by auditvisit2address_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersonaddress_list = new List<mstVisitpersonaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersonaddress_list.Add(new mstVisitpersonaddress_list
                    {
                        auditvisit2address_gid = (dr_datarow["auditvisit2address_gid"].ToString()),
                        addresstype_gid = (dr_datarow["addresstype_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        address_line1 = (dr_datarow["address_line1"].ToString()),
                        address_line2 = (dr_datarow["address_line2"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        taluk = (dr_datarow["taluk"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstVisitpersonaddress_list = getmstVisitpersonaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetEditAuditVisitDocumentList(string employee_gid, string auditvisit_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2document_gid,document_name,document_path,file_name  from atm_rpt_tauditvisit2document where " +
              " auditvisit_gid='" + auditvisit_gid + "'  or auditvisit_gid='" + employee_gid + "' order by auditvisit2document_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadDocumentList = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadDocumentList.Add(new UploadDocumentList
                    {
                        auditvisit2document_gid = (dr_datarow["auditvisit2document_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadDocumentList = getUploadDocumentList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetEditAuditVisitPhotoList(string employee_gid, string auditvisit_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit2photo_gid,visitphoto_name,visitphoto_path,file_name  from atm_rpt_tauditvisit2photo where " +
             " auditvisit_gid='" + auditvisit_gid + "'  or auditvisit_gid='" + employee_gid + "' order by auditvisit2photo_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadphotoList = new List<UploadphotoList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadphotoList.Add(new UploadphotoList
                    {
                        auditvisit2photo_gid = (dr_datarow["auditvisit2photo_gid"].ToString()),
                        photo_name = (dr_datarow["visitphoto_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["visitphoto_path"].ToString()),
                        filename = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadphotoList = getUploadphotoList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public bool DaPostAuditVisitReportApproval(string employee_gid, string user_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2person where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Person Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2address where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Address Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            //msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2document where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Documents are Not Uploaded";
            //    return false;

            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //}
            //msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2photo where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Photos are Not Uploaded";
            //    return false;

            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //}
            msSQL = "select date_format(auditvisit_date,'%d-%m-%Y') as auditvisit_date from atm_rpt_tauditvisitreport where auditvisit_gid='" + values.auditvisit_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (values.auditvisit_date == (objODBCDatareader["auditvisit_date"].ToString()))
                {
                    values.auditvisit_date = "exist";
                }
                objODBCDatareader.Close();

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "update atm_rpt_tauditvisitreport set " +
                     " clientkmp_activities='" + values.clientkmp_activities.Replace("'", "") + "'," +
                     " promoter_background='" + values.promoter_background.Replace("'", "") + "'," +
                     " overall_observations='" + values.overall_observations.Replace("'", "") + "'," +
                     " inspectingofficial_recommenation='" + values.inspectingofficial_recommenation.Replace("'", "") + "'," +
                     " trading_relationship='" + values.trading_relationship.Replace("'", "") + "'," +
                     " summary='" + values.summary.Replace("'", "") + "'," +
                      " entity_name='" + values.entity_name.Replace("'", "") + "'," +
                     " entity_gid='" + values.entity_gid + "'," +
                     " samfincustomer_name='" + values.samfincustomer_name.Replace("'", "") + "'," +
                     " samfincustomer_gid='" + values.samfincustomer_gid + "'," +
                     " samagrocustomer_name='" + values.samagrocustomer_name.Replace("'", "") + "'," +
                     " samagrocustomer_gid='" + values.samagrocustomer_gid + "'," +
                     " draft_flag='N'," +
                     " approval_flag='Y'," +
                     " approval_status='Approved'," +
                     " statusupdated_by = '" + values.statusupdated_by + "', ";


            if (values.auditvisit_date == null)
            {
                msSQL += "auditvisit_date=null,";
            }
            else if (values.auditvisit_date == "exist")
            {

            }
            else if (Convert.ToDateTime(values.auditvisit_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += "auditvisit_date='" + Convert.ToDateTime(values.auditvisit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "approved_by='" + employee_gid + "'," +
                     "approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where auditvisit_gid='" + values.auditvisit_gid + "' ";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from atm_rpt_tauditvisit2inspectingofficial where auditvisit_gid ='" + values.auditvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                for (var i = 0; i < values.mstinspectingofficials.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("AV2O");

                    msSQL = "Insert into atm_rpt_tauditvisit2inspectingofficial( " +
                           " auditvisit2inspectingofficial_gid, " +
                           " auditvisit_gid," +
                           " inspectingofficials_gid," +
                           " inspectingofficials_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.auditvisit_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " delete from atm_rpt_tauditvisit2visitdone where auditvisit_gid ='" + values.auditvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                for (var i = 0; i < values.mdlvisitdone.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("AV2D");

                    msSQL = "Insert into atm_rpt_tauditvisit2visitdone( " +
                           " auditvisit2visitdone_gid, " +
                           " auditvisit_gid," +
                           " visitdone_gid," +
                           " visitdone_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.auditvisit_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = "update atm_rpt_tauditvisit2person set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2address set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2document set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2photo set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Audit Visit Report Approved Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostAuditVisitReportApprovalUpdate(string employee_gid, string user_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2person where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Person Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2address where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Address Details are Not Added";
                return false;

            }
            else
            {
                objODBCDatareader.Close();
            }
            //msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2document where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Documents are Not Uploaded";
            //    return false;

            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //}
            //msSQL = "select auditvisit_gid from atm_rpt_tauditvisit2photo where auditvisit_gid ='" + employee_gid + "' or auditvisit_gid='" + values.auditvisit_gid + "' ";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Photos are Not Uploaded";
            //    return false;

            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //}
            msSQL = "select date_format(auditvisit_date,'%d-%m-%Y') as auditvisit_date from atm_rpt_tauditvisitreport where auditvisit_gid='" + values.auditvisit_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (values.auditvisit_date == (objODBCDatareader["auditvisit_date"].ToString()))
                {
                    values.auditvisit_date = "exist";
                }
                objODBCDatareader.Close();

            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "update atm_rpt_tauditvisitreport set " +
                     " clientkmp_activities='" + values.clientkmp_activities.Replace("'", "") + "'," +
                     " promoter_background='" + values.promoter_background.Replace("'", "") + "'," +
                     " overall_observations='" + values.overall_observations.Replace("'", "") + "'," +
                     " inspectingofficial_recommenation='" + values.inspectingofficial_recommenation.Replace("'", "") + "'," +
                     " trading_relationship='" + values.trading_relationship.Replace("'", "") + "'," +
                     " summary='" + values.summary.Replace("'", "") + "'," +
                      " entity_name='" + values.entity_name.Replace("'", "") + "'," +
                     " entity_gid='" + values.entity_gid + "'," +
                     " samfincustomer_name='" + values.samfincustomer_name.Replace("'", "") + "'," +
                     " samfincustomer_gid='" + values.samfincustomer_gid + "'," +
                     " samagrocustomer_name='" + values.samagrocustomer_name.Replace("'", "") + "'," +
                     " samagrocustomer_gid='" + values.samagrocustomer_gid + "'," +
                     " draft_flag='N'," +
                     " approval_flag='N'," +
                     " approval_status='Pending'," +
                     " statusupdated_by = '" + values.statusupdated_by + "', ";


            if (values.auditvisit_date == null)
            {
                msSQL += "auditvisit_date=null,";
            }
            else if (values.auditvisit_date == "exist")
            {

            }
            else if (Convert.ToDateTime(values.auditvisit_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += "auditvisit_date='" + Convert.ToDateTime(values.auditvisit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "approved_by='" + employee_gid + "'," +
                     "approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where auditvisit_gid='" + values.auditvisit_gid + "' ";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from atm_rpt_tauditvisit2inspectingofficial where auditvisit_gid ='" + values.auditvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                for (var i = 0; i < values.mstinspectingofficials.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("AV2O");

                    msSQL = "Insert into atm_rpt_tauditvisit2inspectingofficial( " +
                           " auditvisit2inspectingofficial_gid, " +
                           " auditvisit_gid," +
                           " inspectingofficials_gid," +
                           " inspectingofficials_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.auditvisit_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_gid + "'," +
                           "'" + values.mstinspectingofficials[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " delete from atm_rpt_tauditvisit2visitdone where auditvisit_gid ='" + values.auditvisit_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                for (var i = 0; i < values.mdlvisitdone.Count; i++)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("AV2D");

                    msSQL = "Insert into atm_rpt_tauditvisit2visitdone( " +
                           " auditvisit2visitdone_gid, " +
                           " auditvisit_gid," +
                           " visitdone_gid," +
                           " visitdone_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid1 + "'," +
                           "'" + values.auditvisit_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_gid + "'," +
                           "'" + values.mdlvisitdone[i].visitdone_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = "update atm_rpt_tauditvisit2person set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2address set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2document set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update atm_rpt_tauditvisit2photo set auditvisit_gid ='" + values.auditvisit_gid + "' where auditvisit_gid='" + employee_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.status = true;
                values.message = "Audit Visit Report Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
        public void DaGetAuditVisitReportApprovalPendingSummary(string employee_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,auditvisitreportref_no,concat(samfincustomer_name,samagrocustomer_name) as customer_name,draft_flag,approval_status,a.entity_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.auditvisit_date,'%d-%m-%Y') as auditvisit_date,a.auditvisit_gid  from atm_rpt_tauditvisitreport a " +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join  adm_mst_tuser c on c.user_gid=b.user_gid " +
                 " where reportingmanager_gid='" + employee_gid + "' and approval_status='Pending' order by auditvisit_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getVisitReportList = new List<VisitReportList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getVisitReportList.Add(new VisitReportList
                    {
                        draft_flag = (dr_datarow["draft_flag"]).ToString(),
                        visitreport_gid = (dr_datarow["auditvisit_gid"].ToString()),
                        visitreport_date = (dr_datarow["auditvisit_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["approval_status"]).ToString(),
                        entity_name = (dr_datarow["entity_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        auditvisitreportref_no = (dr_datarow["auditvisitreportref_no"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString())
                    });
                }
                values.VisitReportList = getVisitReportList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetAuditVisitReportApprovalApprovedSummary(string employee_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,auditvisitreportref_no,concat(samfincustomer_name,samagrocustomer_name) as customer_name,draft_flag,approval_status,a.entity_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.auditvisit_date,'%d-%m-%Y') as auditvisit_date,a.auditvisit_gid  from atm_rpt_tauditvisitreport a " +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join  adm_mst_tuser c on c.user_gid=b.user_gid " +
                 " where reportingmanager_gid='" + employee_gid + "' and approval_status='Approved' order by auditvisit_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getVisitReportList = new List<VisitReportList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getVisitReportList.Add(new VisitReportList
                    {
                        draft_flag = (dr_datarow["draft_flag"]).ToString(),
                        visitreport_gid = (dr_datarow["auditvisit_gid"].ToString()),
                        visitreport_date = (dr_datarow["auditvisit_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["approval_status"]).ToString(),
                        entity_name = (dr_datarow["entity_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        auditvisitreportref_no = (dr_datarow["auditvisitreportref_no"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString())
                    });
                }
                values.VisitReportList = getVisitReportList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetAuditVisitReportApprovedSummary(string employee_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,auditvisitreportref_no,concat(samfincustomer_name,samagrocustomer_name) as customer_name,draft_flag,approval_status,a.entity_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.auditvisit_date,'%d-%m-%Y') as auditvisit_date,a.auditvisit_gid  from atm_rpt_tauditvisitreport a " +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join  adm_mst_tuser c on c.user_gid=b.user_gid " +
                 " where approval_status='Approved' order by auditvisit_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getVisitReportList = new List<VisitReportList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getVisitReportList.Add(new VisitReportList
                    {
                        draft_flag = (dr_datarow["draft_flag"]).ToString(),
                        visitreport_gid = (dr_datarow["auditvisit_gid"].ToString()),
                        visitreport_date = (dr_datarow["auditvisit_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["approval_status"]).ToString(),
                        entity_name = (dr_datarow["entity_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        auditvisitreportref_no = (dr_datarow["auditvisitreportref_no"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString())
                    });
                }
                values.VisitReportList = getVisitReportList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetAuditVisitReportManagementPendingSummary(MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,concat(samfincustomer_name,samagrocustomer_name) as customer_name,auditvisitreportref_no,draft_flag,approval_status,a.entity_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.auditvisit_date,'%d-%m-%Y') as auditvisit_date,a.auditvisit_gid  from atm_rpt_tauditvisitreport a " +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join  adm_mst_tuser c on c.user_gid=b.user_gid " +
                 " where approval_status = 'Pending' or draft_flag='Y' order by auditvisit_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getVisitReportList = new List<VisitReportList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getVisitReportList.Add(new VisitReportList
                    {
                        draft_flag = (dr_datarow["draft_flag"]).ToString(),
                        visitreport_gid = (dr_datarow["auditvisit_gid"].ToString()),
                        visitreport_date = (dr_datarow["auditvisit_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        entity_name = (dr_datarow["entity_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        auditvisitreportref_no = (dr_datarow["auditvisitreportref_no"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString())
                    });
                }
                values.VisitReportList = getVisitReportList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetAuditVisitReportManagementApprovedSummary(string employee_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,auditvisitreportref_no,concat(samfincustomer_name,samagrocustomer_name) as customer_name,draft_flag,approval_status,a.entity_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.auditvisit_date,'%d-%m-%Y') as auditvisit_date,a.auditvisit_gid  from atm_rpt_tauditvisitreport a " +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                 " left join  adm_mst_tuser c on c.user_gid=b.user_gid " +
                 " where approval_status='Approved' order by auditvisit_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getVisitReportList = new List<VisitReportList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getVisitReportList.Add(new VisitReportList
                    {
                        draft_flag = (dr_datarow["draft_flag"]).ToString(),
                        visitreport_gid = (dr_datarow["auditvisit_gid"].ToString()),
                        visitreport_date = (dr_datarow["auditvisit_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["approval_status"]).ToString(),
                        entity_name = (dr_datarow["entity_name"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        auditvisitreportref_no = (dr_datarow["auditvisitreportref_no"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString())
                    });
                }
                values.VisitReportList = getVisitReportList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetAuditVisitReportCounts(string employee_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "select (select count(auditvisit_gid) from atm_rpt_tauditvisitreport  where approval_status = 'Pending' or draft_flag='Y' ) as pending, " +
            " (select count(auditvisit_gid) from atm_rpt_tauditvisitreport  where approval_status = 'Approved' ) as approved, " +
            " (select count(auditvisit_gid) from atm_rpt_tauditvisitreport  where approval_status = 'Pending' or draft_flag='Y' ) as management_pending, " +
            " (select count(auditvisit_gid) from atm_rpt_tauditvisitreport  where approval_status = 'Approved' ) as management_approved, " +
            " (select count(auditvisit_gid) from atm_rpt_tauditvisitreport  where approval_status = 'Approved' and reportingmanager_gid='" + employee_gid + "') as approval_approved, " +
            " (select count(auditvisit_gid) from atm_rpt_tauditvisitreport  where approval_status = 'Pending' and reportingmanager_gid='" + employee_gid + "') as approval_pending ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.pending = objODBCDatareader["pending"].ToString();
                values.approved = objODBCDatareader["approved"].ToString();
                values.approval_approved = objODBCDatareader["approval_approved"].ToString();
                values.approval_pending = objODBCDatareader["approval_pending"].ToString();
                values.management_approved = objODBCDatareader["management_approved"].ToString();
                values.management_pending = objODBCDatareader["management_pending"].ToString();
            }
            objODBCDatareader.Close();
        }
        public void DaDeleteAuditVisittmpDocumentList(string auditvisit2document_gid, MdlAtmRptAuditVisitReport values)
        {
            msSQL = "delete from atm_rpt_tauditvisit2document where auditvisit2document_gid='" + auditvisit2document_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured ";
                values.status = false;

            }
        }
        public bool DaPostAuditPersonaddressUpdate(string employee_gid, mstVisitpersonaddress_list values)
        {
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " update atm_rpt_tauditvisit2address set addresstype_gid='" + values.addresstype_gid + "'," +
                   "  addresstype_name='" + values.addresstype_name + "'," +
                   "  primary_status='" + values.primary_status + "'," +
                   "  address_line1='" + values.address_line1 + "'," +
                   "  address_line2='" + values.address_line2 + "'," +
                   "  landmark='" + values.landmark + "'," +
                   "  postal_code='" + values.postal_code + "'," +
                   "  city='" + values.city + "'," +
                   " district='" + values.district + "'," +
                   " taluk='" + values.taluk + "'," +
                   " state_name='" + values.state_name + "'," +
                   " country='" + values.country + "'," +
                   " latitude='" + values.latitude + "'," +
                   " longitude='" + values.longitude + "'," +
                   " updated_by='" + employee_gid + "',updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where auditvisit2address_gid='" + values.auditvisit2address_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Address Details Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
        public void DaExportAuditVisitPendingReport(MdlAtmRptAuditVisitReport values)
        {
            msSQL = " select (a.auditvisitreportref_no) as 'Audit Visit Report Ref No',(a.entity_name) as 'Entity Name',concat(a.samfincustomer_name,a.samagrocustomer_name) as 'Customer Name',date_format(a.auditvisit_date, '%d-%m-%Y') as 'AuditVisit Date',(a.clientkmp_activities) as 'Clientkmp Activities',(a.promoter_background) as 'Promoter Background',(a.overall_observations) as 'Overall Observations',(a.inspectingofficial_recommenation) as 'Inspectingofficial Recommenation',(a.trading_relationship) as 'Trading Relationship',(a.summary) as 'Summary',group_concat(distinct b.inspectingofficials_name SEPARATOR ' | ') as 'Inspectingofficials Name', group_concat(distinct c.visitdone_name SEPARATOR ' | ') as 'Visitdone Name',group_concat(distinct d.clientrepresentative_name SEPARATOR ' | ') as 'Clientrepresentative Name',group_concat(distinct d.clientrepresentative_designationname SEPARATOR ' | ') as 'Clientrepresentative Designationname'," +
                     " group_concat(distinct d.office_mail SEPARATOR ' | ') as 'Office Mail',group_concat(distinct d.personal_mail SEPARATOR ' | ') as 'Personal Mail',group_concat(distinct f.mobile_no SEPARATOR ' | ') as 'Mobile No',group_concat(distinct f.primary_status SEPARATOR ' | ') as 'Primary Status',group_concat(distinct f.whatsapp_mobileno SEPARATOR ' | ') as 'WhatsApp No',group_concat(distinct e.addresstype_name SEPARATOR ' | ') as 'Address Name', " +
                 " group_concat(distinct e.address_line1 SEPARATOR ' | ') as 'Address Line',group_concat(distinct e.primary_status SEPARATOR ' | ') as 'Primary Status',group_concat(distinct e.postal_code SEPARATOR ' | ') as 'Postal Code', group_concat(distinct e.city SEPARATOR ' | ') as 'City',group_concat(distinct e.taluk SEPARATOR ' | ') as 'Taluk',group_concat(distinct e.district SEPARATOR ' | ') as 'district'," +
                   " group_concat(distinct e.state_name SEPARATOR ' | ') as 'State Name',group_concat(distinct e.country SEPARATOR ' | ') as 'Country',(a.reportingmanager_name) as 'ReportingManager Name'," +
                 " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'Created By',date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created date'" +
                    " from atm_rpt_tauditvisitreport a " +
                    " left join atm_rpt_tauditvisit2inspectingofficial b  on b.auditvisit_gid = a.auditvisit_gid " +
                   " left join atm_rpt_tauditvisit2visitdone c on c.auditvisit_gid = a.auditvisit_gid " +
                   " left join atm_rpt_tauditvisit2person d on d.auditvisit_gid = a.auditvisit_gid " +
                     " left join atm_rpt_tauditvisit2address e on e.auditvisit_gid = a.auditvisit_gid " +
                    " left join atm_rpt_tauditvisitperson2contactno f on f.auditvisit2person_gid = d.auditvisit2person_gid " +
                    " left join hrm_mst_temployee g on a.created_by = g.employee_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.user_gid " +               
                     " where a.approval_status = 'Pending' group by a.auditvisit_gid  ";
        
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            //ExcelPackage excel = new ExcelPackage();
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("AuditVisitPendingReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "AuditVisitPendingReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "AuditVisitReport/AuditVisitReportPending/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "AuditVisitReport/AuditVisitReportPending/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "AuditVisitReport/AuditVisitReportPending/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                /* if (!exists)
                 {
                   //  System.IO.Directory.CreateDirectory(path);
                 }*/
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 52])  //Address "A1:A29"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "AuditVisitReport/AuditVisitReportPending/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaExportAuditVisitApprovedReport(MdlAtmRptAuditVisitReport values)
        {
            msSQL = " select (a.auditvisitreportref_no) as 'Audit Visit Report Ref No',(a.entity_name) as 'Entity Name',concat(a.samfincustomer_name,a.samagrocustomer_name) as 'Customer Name',date_format(a.auditvisit_date, '%d-%m-%Y') as 'AuditVisit Date',(a.clientkmp_activities) as 'Clientkmp Activities',(a.promoter_background) as 'Promoter Background',(a.overall_observations) as 'Overall Observations',(a.inspectingofficial_recommenation) as 'Inspectingofficial Recommenation',(a.trading_relationship) as 'Trading Relationship',(a.summary) as 'Summary',group_concat(distinct b.inspectingofficials_name SEPARATOR ' | ') as 'Inspectingofficials Name', group_concat(distinct c.visitdone_name SEPARATOR ' | ') as 'Visitdone Name',group_concat(distinct d.clientrepresentative_name SEPARATOR ' | ') as 'Clientrepresentative Name',group_concat(distinct d.clientrepresentative_designationname SEPARATOR ' | ') as 'Clientrepresentative Designationname'," +
                     " group_concat(distinct d.office_mail SEPARATOR ' | ') as 'Office Mail',group_concat(distinct d.personal_mail SEPARATOR ' | ') as 'Personal Mail',group_concat(distinct f.mobile_no SEPARATOR ' | ') as 'Mobile No',group_concat(distinct f.primary_status SEPARATOR ' | ') as 'Primary Status',group_concat(distinct f.whatsapp_mobileno SEPARATOR ' | ') as 'WhatsApp No',group_concat(distinct e.addresstype_name SEPARATOR ' | ') as 'Address Name', " +
                 " group_concat(distinct e.address_line1 SEPARATOR ' | ') as 'Address Line',group_concat(distinct e.primary_status SEPARATOR ' | ') as 'Primary Status',group_concat(distinct e.postal_code SEPARATOR ' | ') as 'Postal Code', group_concat(distinct e.city SEPARATOR ' | ') as 'City',group_concat(distinct e.taluk SEPARATOR ' | ') as 'Taluk',group_concat(distinct e.district SEPARATOR ' | ') as 'district'," +
                   " group_concat(distinct e.state_name SEPARATOR ' | ') as 'State Name',group_concat(distinct e.country SEPARATOR ' | ') as 'Country',(a.reportingmanager_name) as 'ReportingManager Name'," +
                 " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'Created By',date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created date'" +
                    " from atm_rpt_tauditvisitreport a " +
                    " left join atm_rpt_tauditvisit2inspectingofficial b  on b.auditvisit_gid = a.auditvisit_gid " +
                   " left join atm_rpt_tauditvisit2visitdone c on c.auditvisit_gid = a.auditvisit_gid " +
                   " left join atm_rpt_tauditvisit2person d on d.auditvisit_gid = a.auditvisit_gid " +
                     " left join atm_rpt_tauditvisit2address e on e.auditvisit_gid = a.auditvisit_gid " +
                    " left join atm_rpt_tauditvisitperson2contactno f on f.auditvisit2person_gid = d.auditvisit2person_gid " +
                    " left join hrm_mst_temployee g on a.created_by = g.employee_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
                     " where a.approval_status = 'Approved' group by a.auditvisit_gid  ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            //ExcelPackage excel = new ExcelPackage();
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("AuditVisitApprovedReport");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "AuditVisitApprovedReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "AuditVisitReport/AuditVisitReportApproved/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "AuditVisitReport/AuditVisitReportApproved/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "AuditVisitReport/AuditVisitReportApproved/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                /* if (!exists)
                 {
                   //  System.IO.Directory.CreateDirectory(path);
                 }*/
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 52])  //Address "A1:A29"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "AuditVisitReport/AuditVisitReportApproved/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
    }
}
