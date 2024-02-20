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
using OfficeOpenXml.Style;
using System.Drawing;
using ems.storage.Functions;


namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access to various excel reports in Samagro
    /// </summary>
    /// <remarks>Written by Abilash.A, Kalaiarasan Premchandar.K </remarks>
    /// 
    public class DaAgrMstApplicationReport
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_child, dt_datatable1, dt_datatable2, dt_datatable3, dt_datatable4, dt_datatable5;
        OdbcDataReader objODBCDatareader;
        OdbcDataReader objODBCDataReader, objODBCDataReader1;
        string msSQL, msGetGid;
        int mnResult;
        string lsmaster_value;


        public void DaGetApplicationCounts(string employee_gid, string user_gid, ApplicationListCount values)
        {

            msSQL = "SELECT COUNT(approval_status) as ApplicationCount FROM agr_mst_tapplication WHERE approval_status='Submitted to Underwriting' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_submit = objODBCDataReader["ApplicationCount"].ToString();
            }
            objODBCDataReader.Close();



            msSQL = "SELECT COUNT(approval_status) as ApplicationCount FROM agr_mst_tapplication WHERE approval_status='Incomplete' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_incomplete = objODBCDataReader["ApplicationCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(approval_status) as ApplicationCount FROM agr_mst_tapplication  ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_Report = objODBCDataReader["ApplicationCount"].ToString();
            }
            objODBCDataReader.Close();


        }

        public void DaGetMstAppSummary(MdlAgrMstApplicationReport objMstAppSummary)
        {
            msSQL = " select a.application_gid, a.application_no, a.customer_name,a.customerref_name,a.vertical_name,a.applicant_type,a.approval_status,a.overalllimit_amount," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p')as 'created_date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p')as 'submitted_date'," +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')as 'updated_date'," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) " +
                    " as 'updated_by' from agr_mst_tapplication a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid order by a.application_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetMstAppSummary = new List<MstAppSummaryList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetMstAppSummary.Add(new MstAppSummaryList
                    {

                        application_no = dr_datarow["application_no"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        customerref_name = dr_datarow["customerref_name"].ToString(),
                        vertical_name = dr_datarow["vertical_name"].ToString(),
                        applicant_type = dr_datarow["applicant_type"].ToString(),
                        approval_status = dr_datarow["approval_status"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                        submitted_date = dr_datarow["submitted_date"].ToString(),
                        updated_date = dr_datarow["updated_date"].ToString(),
                        updated_by = dr_datarow["updated_by"].ToString(),
                        overalllimit_amount = dr_datarow["overalllimit_amount"].ToString(),
                        created_date = (dr_datarow["created_date"].ToString())
                    });
                }
                objMstAppSummary.MstAppSummaryList = objGetMstAppSummary;
            }
            dt_datatable.Dispose();
            objMstAppSummary.status = true;
            objMstAppSummary.message = "Success";
        }
        // export excel

        public void DaGetMstApplicationReport(MdlAgrMstApplicationReport objMstApplicationReport)
        {
            //msSQL = " select  a.application_no as 'Application Number',a.customerref_name as 'Customer/Supplier Name',a.vertical_name as 'Customer/Supplier Type',a.applicant_type as 'Applicant Type',a.approval_status as 'Approval Status',a.overalllimit_amount as 'Overall Limit'," +
            //        " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'RM', concat(i.user_firstname, ' ', i.user_lastname, ' / ', i.user_code) as 'DRM', a.program_name as 'Business Classification', a.baselocation_name as 'RM Location', f.product_type as 'Product', f.productsub_type as 'Program', " +
            //        " a.ccgroup_name as 'CC Group', date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p')as 'Created Date'," +
            //        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By', " +
            //        " date_format(a.submitted_date, '%d-%m-%Y %h:%i:%s %p')as 'Submitted Date'," +
            //        " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')as 'Updated Date'," +
            //        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) " +
            //        " as 'Updated By' from agr_mst_tapplication a" +
            //        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
            //        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
            //        " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
            //        " left join adm_mst_tuser e on e.user_gid = d.user_gid "+
            //        " left join hrm_mst_temployee g on a.relationshipmanager_gid = g.employee_gid " +
            //        " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
            //         " left join hrm_mst_temployee j on a.drm_gid = j.employee_gid " +
            //        " left join adm_mst_tuser i on i.user_gid = j.user_gid " +
            //        " left join agr_mst_tapplication2loan f on f.application_gid = a.application_gid order by a.application_gid desc ";
            msSQL = " select  a.application_no as 'Application Number',a.customerref_name as 'Customer Name', " +
                    " a.vertical_name as 'Vertical',a.applicant_type as 'Applicant Type', " +
                    " a.approval_status as 'Approval Status',a.overalllimit_amount as 'Overall Limit',  " +                    
                    " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'RM',  " +
                    " concat(i.user_firstname, ' ', i.user_lastname, ' / ', i.user_code) as 'DRM',  " +
                    " concat(k.user_firstname, ' ', k.user_lastname, ' / ', k.user_code) as 'CH',   " +
                    " concat(m.user_firstname, ' ', m.user_lastname, ' / ', m.user_code) as 'RH',   " +
                    " concat(o.user_firstname, ' ', o.user_lastname, ' / ', o.user_code) as 'ZH',   " +
                    " concat(q.user_firstname, ' ', q.user_lastname, ' / ', q.user_code) as 'BVH',   " +
                    " a.program_name as 'Business Classification', a.baselocation_name as 'RM Location', " +
                    " f.product_type as 'Product', f.productsub_type as 'Program',   " +
                    " a.ccgroup_name as 'CC Group', date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p')as 'Created Date',  " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By',   " +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i:%s %p')as 'Submitted Date',  " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')as 'Updated Date',  " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code)    as 'Updated By', " +                   
                    " case " +
                    " when(select group_concat(application_gid) from agr_trn_tapplicationcomment where application_gid = a.application_gid) is not null then 'Business' " +
                    " when(select group_concat(application_gid) from agr_trn_tapplicationcreditquery where queryraised_to = 'RM' and application_gid = a.application_gid) is not null then 'Credit' " +
                    " else '' " +
                    " end " +
                    " as 'Stage of Query', " +
                    " case  " +
                    " when(select group_concat(application_gid) from agr_trn_tapplicationcomment where application_gid = a.application_gid) is not null then " +
                    " (select date_format(max(created_date), '%d-%m-%Y') from agr_trn_tapplicationcomment where application_gid = a.application_gid " +
                    " ) " +
                    " when(select group_concat(application_gid) from agr_trn_tapplicationcreditquery where application_gid = a.application_gid) is not null then " +
                    " (select date_format(max(created_date), '%d-%m-%Y') from agr_trn_tapplicationcreditquery where queryraised_to = 'RM' and application_gid = a.application_gid) " +
                    " else '' " +
                    " end " +
                    " as 'Query Raised Date'," +
                    " case when s.companypan_no is null then u.pan_no else s.companypan_no end as 'Pan Number', " +
                    " GROUP_CONCAT(distinct(w.gst_no)SEPARATOR ' || ') as 'Gst Number', " +
                    " count(x.application_gid) as  'Credit approval Query To RM Raised Count', " +
                    " case " +
                    " when y.query_status = 'Open' then 'Pending with RM' when y.query_status = 'Closed' then 'Closed By RM' " +
                    " when z.comment_status = 'Open' then 'Pending with RM' when z.comment_status = 'Closed' then 'Closed By RM' " +
                    " else '' end as 'Query Status', " +
                    " date_format(ls.approved_date, '%d-%m-%Y %h:%i %p') as 'Business Approval Date', ls.approval_name as 'Business Approval By', " +
                    " concat(qs.user_firstname, ' ', qs.user_lastname, ' / ', qs.user_code) as 'Submitted By', " +
                    " case when s.contactperson_firstname is null then concat(u.first_name, u.middle_name, u.last_name) " +
                    " else concat(s.contactperson_firstname, s.contactperson_middlename, s.contactperson_lastname) end as 'Contact Person Name'," +
                    " case when ss.mobile_no is null then GROUP_CONCAT(distinct Concat((concat(st.primary_status)), ' - ', (st.mobile_no))SEPARATOR ' || ') " +
                    " else GROUP_CONCAT(distinct Concat((concat(ss.primary_status)), ' - ', (ss.mobile_no))SEPARATOR ' || ') end as 'Contact Number', " +
                    " case when su.email_address is null then GROUP_CONCAT(distinct Concat((concat(sv.primary_status)), ' - ', (sv.email_address))SEPARATOR ' || ') " +
                    " else GROUP_CONCAT(distinct Concat((concat(su.primary_status)), ' - ', (su.email_address))SEPARATOR ' || ') end as 'Contact Email Address', " +
                    " case when uu.addressline1 is null then GROUP_CONCAT(distinct Concat((Concat(vv.primary_status)), ' - ', (vv.addressline1),'  ',(vv.addressline2),'  '," +
                    " (vv.landmark),'  ',(vv.postal_code),'  ',(vv.city),'  ',(vv.taluka),'  ',(vv.district),'  ',(vv.state) " +
                    " ,'  ',(vv.country),' ',(vv.latitude),'  ',(vv.longitude))SEPARATOR ' || ') " +
                    " else GROUP_CONCAT(distinct Concat((concat(uu.primary_status)), ' - ', (uu.addressline1), '  ', (uu.addressline2), '  ', " +
                    " (uu.landmark), '  ', (uu.postal_code), '  ', (uu.city), '  ', (uu.taluka), '  ', (uu.district), '  ', (uu.state) " +
                    " , '  ', (uu.country), '  ', (uu.latitude), '  ', (uu.longitude))SEPARATOR ' || ') end as 'Contact Address' " +
                    " from agr_mst_tapplication a  " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid   " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid   " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid   " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid  " +
                    " left join hrm_mst_temployee g on a.relationshipmanager_gid = g.employee_gid   " +
                    " left join adm_mst_tuser h on h.user_gid = g.user_gid   " +
                    " left join hrm_mst_temployee j on a.drm_gid = j.employee_gid   " +
                    " left join adm_mst_tuser i on i.user_gid = j.user_gid   " +
                    " left join hrm_mst_temployee r on a.clustermanager_gid = r.employee_gid   " +
                    " left join adm_mst_tuser k on k.user_gid = r.user_gid   " +
                    " left join hrm_mst_temployee l on a.regionalhead_gid = l.employee_gid   " +
                    " left join adm_mst_tuser m on m.user_gid = l.user_gid   " +
                    " left join hrm_mst_temployee n on a.zonalhead_gid = n.employee_gid   " +
                    " left join adm_mst_tuser o on o.user_gid = n.user_gid   " +
                    " left join hrm_mst_temployee p on a.businesshead_gid = p.employee_gid   " +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid   " +
                    " left join hrm_mst_temployee sl on a.submitted_by = sl.employee_gid " +
                    " left join adm_mst_tuser qs on qs.user_gid = sl.user_gid " +
                    " left join agr_mst_tapplication2loan f on f.application_gid = a.application_gid " +
                    " left join agr_mst_tinstitution s on a.application_gid = s.application_gid and s.stakeholder_type = 'Applicant' " +
                    " left join agr_mst_tcontact u on a.application_gid = u.application_gid and u.stakeholder_type = 'Applicant' " +
                    " left join agr_mst_tinstitution2branch w on s.institution_gid = w.institution_gid " +
                    " left join agr_trn_tapplicationcreditquery x on a.application_gid = x.application_gid and x.queryraised_to = 'RM' " +
                    " left join agr_trn_tapplicationcreditquery y on a.application_gid = y.application_gid and y.queryraised_to = 'RM' and " +
                    " y.created_date = (select max(created_date)from agr_trn_tapplicationcreditquery where queryraised_to = 'RM' and application_gid = a.application_gid) " +
                    " left join agr_trn_tapplicationcomment z on a.application_gid = z.application_gid  and " +
                    " z.created_date = (select max(created_date)from agr_trn_tapplicationcomment where application_gid = a.application_gid) " +
                    " left join agr_trn_tapplicationapproval ls on a.application_gid = ls.application_gid and ls.hierary_level = '5' " +
                    " left join agr_mst_tinstitution2mobileno ss on ss.institution_gid = s.institution_gid " +
                    " left join agr_mst_tcontact2mobileno st on st.contact_gid = u.contact_gid " +
                    " left join agr_mst_tinstitution2email su on su.institution_gid = s.institution_gid " +
                    " left join agr_mst_tcontact2email sv on sv.contact_gid = u.contact_gid " +
                    " left join agr_mst_tinstitution2address vv on vv.institution_gid = s.institution_gid " +
                    " left join agr_mst_tcontact2address uu on uu.contact_gid = u.contact_gid " +
                    " group by a.application_gid desc ";              
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Application Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Application Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Application Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Application Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 35])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/Application Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }

        //CC Summary
        public void DaGetMstCCSummary(MdlAgrMstApplicationReport objMstCCSummary)
        {
            msSQL = " select a.application_gid,date_format(a.submitted_date,'%d-%m-%Y %H:%i %p') as submitted_date, a.application_no, a.customer_name,a.region,a.vertical_name,a.overalllimit_amount," +
                    " date_format(b.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date, " +
                    " b.ccgroup_name from agr_mst_tapplication a" +
                    " left join agr_mst_tccschedulemeeting b on a.application_gid = b.application_gid where ccsubmit_flag ='Y' group by b.application_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetMstCCSummary = new List<MstCCSummaryList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetMstCCSummary.Add(new MstCCSummaryList
                    {
                        submitted_date = dr_datarow["submitted_date"].ToString(),
                        application_no = dr_datarow["application_no"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        region = dr_datarow["region"].ToString(),
                        vertical_name = dr_datarow["vertical_name"].ToString(),
                        overalllimit_amount = dr_datarow["overalllimit_amount"].ToString(),
                        ccmeeting_date = dr_datarow["ccmeeting_date"].ToString(),
                        ccgroup_name = dr_datarow["ccgroup_name"].ToString(),

                    });
                }
                objMstCCSummary.MstCCSummaryList = objGetMstCCSummary;
            }
            dt_datatable.Dispose();
            objMstCCSummary.status = true;
            objMstCCSummary.message = "Success";
        }
        // export excel CC

        public void DaGetMstCCReport(MdlAgrMstApplicationReport objMstApplicationReport)
        {
            msSQL = " ( select " +
                    " a.application_no as 'Application Number',a.customer_name as 'Customer Name'," +
                    " a.region as 'Region',a.vertical_name as 'Customer/Supplier Type',a.overalllimit_amount as 'Overall Limit'," +
                    //" b.ccmeeting_no as 'CC Meeting Number'," +
                    " date_format(b.ccmeeting_date, '%d-%m-%Y')as 'CC Meeting Date'," +
                    " time_format(b.start_time, '%h:%i %p') as 'Start Time',time_format(b.end_time, '%h:%i %p') as 'End Time'," +
                    " b.ccmeeting_mode as 'CC Meeting Mode',b.ccgroup_name as 'CC Group Name'," +
                    " b.otheruser_name as 'Other Samunnati Users',b.ccadmin_name as 'CC Admin'," +
                    " GROUP_CONCAT((c.ccmember_name) SEPARATOR ', ') as 'CC Members' , " +
                    " GROUP_CONCAT(Concat((c.ccmember_name), ' - ', case when c.attendance_status = 'P' then 'Present' when c.attendance_status = 'A' then 'Absent' end) SEPARATOR ', ') as 'Attendance Status', " +
                    " GROUP_CONCAT(Concat(Concat((c.ccmember_name), ' - ', case when c.attendance_status = 'P' then 'Present' when c.attendance_status = 'A' then 'Absent' end), ' - ', c.approval_status) SEPARATOR ', ') as 'Approval Status', " +
                    " GROUP_CONCAT(distinct(d.ccmember_name) SEPARATOR ', ') as 'Approval Initiated To'," +
                    " r.approval_remarks as 'Approval Remarks'," +
                    " concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as 'Approval Initiated By'," +
                    " GROUP_CONCAT(distinct (Concat((c.ccmember_name),' - ',date_format(r.created_date, '%d-%m-%Y'))) SEPARATOR ', ') as 'Approval Initiated Date'," +
                    " Concat(r.approval_name,' - ',date_format(r.approved_date, '%d-%m-%Y'))as 'Approved Date'," +
                    " date_format(cccompleted_date, '%d-%m-%Y') as 'CC Completed Date'" +
                    " from agr_mst_tApplication a " +
                    " left join agr_mst_tccschedulemeeting b on a.application_gid = b.application_gid " +
                    " left join agr_mst_tccmeeting2members c on b.application_gid = c.application_gid " +
                    " left join agr_mst_tccmeeting2members d on (b.application_gid = d.application_gid and d.ccapproval_flag = 'Y')" +
                    " left join hrm_mst_temployee p on d.approvalinitiate_by = p.employee_gid" +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid" +
                    " left join agr_trn_tccapproval r on (c.ccmeeting2members_gid = r.ccmeeting2members_gid and c.application_gid = r.application_gid)" +
                    " where ccsubmit_flag = 'Y' " +
                    " group by b.application_gid )" +
                    " Union " +
                    " (select " +
                    " a.application_no as 'Application Number',a.customer_name as 'Customer Name'," +
                    " a.region as 'Region',a.vertical_name as 'Customer/Supplier Type',a.overalllimit_amount as 'Overall Limit'," +
                    //" b.ccmeeting_no as 'CC Meeting Number'," +
                    " date_format(b.ccmeeting_date, '%d-%m-%Y')as 'CC Meeting Date'," +
                    " time_format(b.start_time, '%h:%i %p') as 'Start Time',time_format(b.end_time, '%h:%i %p') as 'End Time'," +
                    " b.ccmeeting_mode as 'CC Meeting Mode',b.ccgroup_name as 'CC Group Name'," +
                    " b.otheruser_name as 'Other Samunnati Users',b.ccadmin_name as 'CC Admin'," +
                    " GROUP_CONCAT((c.ccmember_name) SEPARATOR ', ') as 'CC Members' , " +
                    " GROUP_CONCAT(Concat((c.ccmember_name), ' - ', case when c.attendance_status = 'P' then 'Present' when c.attendance_status = 'A' then 'Absent' end) SEPARATOR ', ') as 'Attendance Status', " +
                    " GROUP_CONCAT(Concat(Concat((c.ccmember_name), ' - ', case when c.attendance_status = 'P' then 'Present' when c.attendance_status = 'A' then 'Absent' end), ' - ', c.approval_status) SEPARATOR ', ') as 'Approval Status', " +
                    " GROUP_CONCAT(distinct(d.ccmember_name) SEPARATOR ', ') as 'Approval Initiated To'," +
                    " r.approval_remarks as 'Approval Remarks'," +
                    " concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as 'Approval Initiated By'," +
                    " GROUP_CONCAT(distinct (Concat((c.ccmember_name),' - ',date_format(r.created_date, '%d-%m-%Y'))) SEPARATOR ', ') as 'Approval Initiated Date'," +
                    " Concat(r.approval_name,' - ',date_format(r.approved_date, '%d-%m-%Y'))as 'Approved Date'," +
                    " date_format(cccompleted_date, '%d-%m-%Y') as 'CC Completed Date'" +
                    " from agr_mst_tApplication a " +
                    " left join agr_mst_tccschedulemeetinglog b on a.application_gid = b.application_gid " +
                    " left join agr_mst_tccmeeting2memberslog c on b.application_gid = c.application_gid " +
                    " left join agr_mst_tccmeeting2memberslog d on (b.application_gid = d.application_gid and (d.approval_status = 'Approved' or d.approval_status = 'Rejected'))" +
                    " left join hrm_mst_temployee p on d.approvalinitiate_by = p.employee_gid" +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid" +
                    " left join agr_trn_tccapproval r on (c.ccmeeting2members_gid = r.ccmeeting2members_gid and c.application_gid = r.application_gid)" +
                    " where ccsubmit_flag = 'Y' " +
                    " group by b.application_gid) ";

          dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("CC Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "CC Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 22])  //Address "A1:X1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/CC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();


            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }

        //supplier export excel
        public void DaGetExportMstSupplierAppReport(MdlAgrMstApplicationReport objMstApplicationReport)
        {
            msSQL = " select  a.application_no as 'Application Number',a.customerref_name as 'Customer/Supplier Name', " +
                    " a.vertical_name as 'Customer/Supplier Type',a.applicant_type as 'Applicant Type', " +
                    " a.approval_status as 'Approval Status',a.overalllimit_amount as 'Overall Limit',  " +
                    " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'RM',  " +
                    " concat(i.user_firstname, ' ', i.user_lastname, ' / ', i.user_code) as 'DRM',  " +
                    " concat(k.user_firstname, ' ', k.user_lastname, ' / ', k.user_code) as 'CH',   " +
                    " concat(m.user_firstname, ' ', m.user_lastname, ' / ', m.user_code) as 'RH',   " +
                    " concat(o.user_firstname, ' ', o.user_lastname, ' / ', o.user_code) as 'ZH',   " +
                    " concat(q.user_firstname, ' ', q.user_lastname, ' / ', q.user_code) as 'BVH',   " +
                    " a.program_name as 'Business Classification', a.baselocation_name as 'RM Location', " +
                    " f.product_type as 'Product', f.productsub_type as 'Program',   " +
                    " a.ccgroup_name as 'CC Group', date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p')as 'Created Date',  " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By',   " +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i:%s %p')as 'Submitted Date',  " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')as 'Updated Date',  " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code)   " +
                    " as 'Updated By' from agr_mst_tsuprapplication a  " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid   " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid   " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid   " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid  " +
                    " left join hrm_mst_temployee g on a.relationshipmanager_gid = g.employee_gid   " +
                    " left join adm_mst_tuser h on h.user_gid = g.user_gid   " +
                    " left join hrm_mst_temployee j on a.drm_gid = j.employee_gid   " +
                    " left join adm_mst_tuser i on i.user_gid = j.user_gid   " +
                    " left join hrm_mst_temployee r on a.clustermanager_gid = r.employee_gid   " +
                    " left join adm_mst_tuser k on k.user_gid = r.user_gid   " +
                    " left join hrm_mst_temployee l on a.regionalhead_gid = l.employee_gid   " +
                    " left join adm_mst_tuser m on m.user_gid = l.user_gid   " +
                    " left join hrm_mst_temployee n on a.zonalhead_gid = n.employee_gid   " +
                    " left join adm_mst_tuser o on o.user_gid = n.user_gid   " +
                    " left join hrm_mst_temployee p on a.businesshead_gid = p.employee_gid   " +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid   " +
                    " left join agr_mst_tsuprapplication2loan f on f.application_gid = a.application_gid order by a.application_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Application Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Application Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Supplier Application Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Supplier Application Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 22])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/Supplier Application Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }

        //Supplier Count
        public void DaGetSupplierApplicationCounts(string employee_gid, string user_gid, ApplicationListCount values)
        {

            msSQL = "SELECT COUNT(approval_status) as ApplicationCount FROM agr_mst_tsuprapplication WHERE approval_status='Submitted to Underwriting' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_submit = objODBCDataReader["ApplicationCount"].ToString();
            }
            objODBCDataReader.Close();



            msSQL = "SELECT COUNT(approval_status) as ApplicationCount FROM agr_mst_tsuprapplication WHERE approval_status='Incomplete' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_incomplete = objODBCDataReader["ApplicationCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(approval_status) as ApplicationCount FROM agr_mst_tsuprapplication  ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_Report = objODBCDataReader["ApplicationCount"].ToString();
            }
            objODBCDataReader.Close();


        }

        //Supplier Application Summary
        public void DaGetMstSupplierAppSummary(MdlAgrMstApplicationReport objMstAppSummary)
        {
            msSQL = " select a.application_gid, a.application_no, a.customer_name,a.customerref_name,a.vertical_name,a.applicant_type,a.approval_status,a.overalllimit_amount," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p')as 'created_date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p')as 'submitted_date'," +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')as 'updated_date'," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) " +
                    " as 'updated_by' from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid order by a.application_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetMstAppSummary = new List<MstAppSummaryList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetMstAppSummary.Add(new MstAppSummaryList
                    {

                        application_no = dr_datarow["application_no"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        customerref_name = dr_datarow["customerref_name"].ToString(),
                        vertical_name = dr_datarow["vertical_name"].ToString(),
                        applicant_type = dr_datarow["applicant_type"].ToString(),
                        approval_status = dr_datarow["approval_status"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                        submitted_date = dr_datarow["submitted_date"].ToString(),
                        updated_date = dr_datarow["updated_date"].ToString(),
                        updated_by = dr_datarow["updated_by"].ToString(),
                        overalllimit_amount = dr_datarow["overalllimit_amount"].ToString(),
                        created_date = (dr_datarow["created_date"].ToString())
                    });
                }
                objMstAppSummary.MstAppSummaryList = objGetMstAppSummary;
            }
            dt_datatable.Dispose();
            objMstAppSummary.status = true;
            objMstAppSummary.message = "Success";
        }


        //Supplier CC Summary
        public void DaGetMstSupplierCCSummary(MdlAgrMstApplicationReport objMstCCSummary)
        {
            msSQL = " select a.application_gid,date_format(a.submitted_date,'%d-%m-%Y %H:%i %p') as submitted_date, a.application_no, a.customer_name,a.region,a.vertical_name,a.overalllimit_amount," +
                    " date_format(b.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date, " +
                    " b.ccgroup_name from agr_mst_tsuprapplication a" +
                    " left join agr_mst_tsuprccschedulemeeting b on a.application_gid = b.application_gid where ccsubmit_flag ='Y' group by b.application_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetMstCCSummary = new List<MstCCSummaryList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetMstCCSummary.Add(new MstCCSummaryList
                    {
                        submitted_date = dr_datarow["submitted_date"].ToString(),
                        application_no = dr_datarow["application_no"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        region = dr_datarow["region"].ToString(),
                        vertical_name = dr_datarow["vertical_name"].ToString(),
                        overalllimit_amount = dr_datarow["overalllimit_amount"].ToString(),
                        ccmeeting_date = dr_datarow["ccmeeting_date"].ToString(),
                        ccgroup_name = dr_datarow["ccgroup_name"].ToString(),

                    });
                }
                objMstCCSummary.MstCCSummaryList = objGetMstCCSummary;
            }
            dt_datatable.Dispose();
            objMstCCSummary.status = true;
            objMstCCSummary.message = "Success";
        }
        // export excel Supplier CC

        public void DaGetMstSupplierCCReport(MdlAgrMstApplicationReport objMstApplicationReport)
        {
            msSQL = " select date_format(a.submitted_date, '%d-%m-%Y') as 'Submitted Date'," +
                    " a.application_no as 'Application Number',a.customer_name as 'Customer Name'," +
                    " a.region as 'Region',a.vertical_name as 'Customer/Supplier Type',a.overalllimit_amount as 'Overall Limit'," +
                    " b.ccmeeting_title as 'CC Meeting Title',b.ccmeeting_no as 'CC Meeting Number'," +
                    " date_format(b.ccmeeting_date, '%d-%m-%Y')as 'CC Meeting Date'," +
                    " time_format(b.start_time, '%h:%i %p') as 'Start Time',time_format(b.end_time, '%h:%i %p') as 'End Time'," +
                    " b.ccmeeting_mode as 'CC Meeting Mode',b.ccgroup_name as 'CC Group Name'," +
                    " b.otheruser_name as 'Other Samunnati Users',b.ccadmin_name as 'CC Admin'," +
                    " GROUP_CONCAT((c.ccmember_name) SEPARATOR ', ') as 'CC Members' , " +
                    " GROUP_CONCAT(Concat((c.ccmember_name), ' - ', case when c.attendance_status = 'P' then 'Present' when c.attendance_status = 'A' then 'Absent' end) SEPARATOR ', ') as 'Attendance Status', " +
                    " GROUP_CONCAT(Concat(Concat((c.ccmember_name), ' - ', case when c.attendance_status = 'P' then 'Present' when c.attendance_status = 'A' then 'Absent' end), ' - ', c.approval_status) SEPARATOR ', ') as 'Approval Status', " +
                    " GROUP_CONCAT(distinct(d.ccmember_name) SEPARATOR ', ') as 'Approval Initiated To'," +
                    " r.approval_remarks as 'Approval Remarks'," +
                    " concat(q.user_firstname,' ',q.user_lastname,' / ',q.user_code) as 'Approval Initiated By'," +
                    " GROUP_CONCAT(distinct (Concat((c.ccmember_name),' - ',date_format(r.created_date, '%d-%m-%Y'))) SEPARATOR ', ') as 'Approval Initiated Date'," +
                    " Concat(r.approval_name,' - ',date_format(r.approved_date, '%d-%m-%Y'))as 'Approved Date'," +
                    " date_format(cccompleted_date, '%d-%m-%Y') as 'CC Completed Date'" +
                    " from agr_mst_tsuprapplication a " +
                    " left join agr_mst_tsuprccschedulemeeting b on a.application_gid = b.application_gid " +
                    " left join agr_mst_tsuprccmeeting2members c on b.application_gid = c.application_gid " +
                    " left join agr_mst_tsuprccmeeting2members d on (b.application_gid = d.application_gid and d.ccapproval_flag = 'Y')" +
                    " left join hrm_mst_temployee p on d.approvalinitiate_by = p.employee_gid" +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid" +
                    " left join agr_trn_tsuprccapproval r on (c.ccmeeting2members_gid = r.ccmeeting2members_gid and c.application_gid = r.application_gid)" +
                    " where ccsubmit_flag = 'Y' " +
                    " group by b.application_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("CC Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Supplier CC Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Supplier CC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Supplier CC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 24])  //Address "A1:X1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/Supplier CC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }


        //CAD Accepted export excel
        //public void DaGetExportCADAcceptedAppReport(ExportExcelReturnData objMstApplicationReport)
        //{
        //    msSQL = "select a.contract_id as 'Contract ID', b.application_no as 'Customer ID', a.application_no as 'ARN Number', " +
        //           " CASE WHEN (a.renewal_flag = 'N' AND a.amendment_flag = 'N' AND a.shortclosing_flag = 'N') THEN 'New' ELSE (CASE WHEN (a.renewal_flag = 'Y') THEN 'Renewal' ELSE (CASE WHEN (a.amendment_flag = 'Y') THEN 'Amendment' ELSE (CASE WHEN (a.shortclosing_flag = 'Y') THEN 'Short Closing' ELSE '' END) END) END) END AS 'Type'," +
        //            "b.virtualaccount_number as 'VAN', a.customerref_name as 'Customer Name',  a.overalllimit_amount as 'Overalllimitamount'," +
        //            "date_format(a.validityfrom_date, '%d-%m-%Y') as 'Validity From',  date_format(a.validityto_date, '%d-%m-%Y') as 'Validity to'," +
        //            "tenureoverall_limit as 'Calculation of overall limit validity'," +
        //            "date_format(facilityrequested_date, '%d-%m-%Y') as 'Facility Requested on', a.buyeragreement_id as 'Buyer Agreement ID', Group_concat( distinct f.supplieragreement_id  SEPARATOR ' || ') as 'Supplier Agreement ID', " +
        //            "group_concat(c.product_type SEPARATOR ' || ') as 'Product Name', " +
        //            "group_concat(c.productsub_type SEPARATOR ' || ') as 'Program Name', " +
        //            "c.loan_type as 'Trade Type', loanfacility_amount as 'Proposed Program Limit',(a.overalllimit_amount - c.loanfacility_amount) as 'Remaining'," +
        //            "c.rate_interest as 'Margin(%)', penal_interest as 'Penal Interest'," +
        //            "c.trade_orginatedby  as 'Trade Originated by','Financial statement method' as 'Scheme Type', " +
        //            "c.programlimit_validdfrom as 'Program Limit Validity from', " +
        //            "c.programlimit_validdto as  'Program Limit Validity to', " +
        //            "c.programoverall_limit as 'Calculation of Program limit validity', " +
        //            "c.tenureproduct_days as 'Credit Period Limit (days)', " +
        //            "c.tenureoverall_limit as 'Calculation of Credit Period Limit validity', " +
        //            "c.SA_Brokerage as 'SA/Brokerage %','Financial statement method' as 'Scheme Type', c.facility_type as 'Product Type', " +
        //            "c.Facility_mode as 'Program Mode', c.enduse_purpose as 'Purpose of Trade', " +
        //           "Group_concat(d.customerpaymenttype_name SEPARATOR ' || ') as  'Customer Payment Type', " +
        //           "Group_concat(d.maximumpercent_paymentterm  SEPARATOR ' || ') as 'Max Percentage',  " +
        //           "Group_concat(e.insurance_cost  SEPARATOR ' || ') as 'Insurance Limit',  " +
        //           "Group_concat(e.product_name  SEPARATOR ' || ') as 'Product',  " +
        //           "Group_concat(e.variety_name  SEPARATOR ' || ') as 'Commodity',  " +
        //           "Group_concat(k.IGST_percent  SEPARATOR ' || ') as 'IGST %', " +
        //           "Group_concat(k.SGST_percent  SEPARATOR ' || ') as 'SGST %', " +
        //           "Group_concat(k.CGST_percent  SEPARATOR ' || ') as 'CGST %', " +
        //           "Group_concat(k.CESS_percent  SEPARATOR ' || ') as 'Cess %', " +
        //           "Group_concat(k.wef_date  SEPARATOR ' || ') as 'w.e.f date', " +
        //           "Group_concat(e.unitpricevalue_commodity  SEPARATOR ' || ') as 'Unit price value of the commodity',  " +
        //           "Group_concat(e.natureformstate_commodity  SEPARATOR ' || ') as ' Nature form state of commodity',  " +
        //           "Group_concat(e.qualityof_commodity  SEPARATOR ' || ') as 'Quality of the Commodity',  " +
        //           "Group_concat(e.quantity  SEPARATOR ' || ') as 'Quantity of the Commodity',  " +
        //           "Group_concat(e.uom_name  SEPARATOR ' || ') as 'UOM of the Commodity',  " +
        //           "Group_concat(e.milestone_applicability  SEPARATOR ' || ') as 'Milestone Applicability',  " +
        //           "Group_concat(e.insurance_applicability  SEPARATOR ' || ') as 'Insurance Applicability',  " +
        //           "Group_concat(e.milestonepayment_name  SEPARATOR ' || ') as 'Milestone Payment Type', " +
        //           "Group_concat(e.sa_payout  SEPARATOR ' || ') as 'SApayout %', " +
        //           "Group_concat(e.insurance_percent  SEPARATOR ' || ') as 'Insurance %',  " +
        //           "Group_concat(e.insurance_cost SEPARATOR ' || ') as 'Insurance limit', " +
        //           "Group_concat(e.markto_marketvalue SEPARATOR ' || ') as 'Mark to Market Value (%)', " +
        //           "Group_concat(e.pricereference_source SEPARATOR ' || ') as 'Price Reference/Source', " +
        //           "Group_concat(e.overallcreditperiod_limit  SEPARATOR ' || ') as 'Calculation of Credit Period limit Validity',  " +
        //           "Group_concat(e.commodity_margin  SEPARATOR ' || ') as 'Margin %', " +
        //           "Group_concat(g.application_no  SEPARATOR ' || ') as 'Supplier ID',  " +
        //           "Group_concat(f.supplier_name  SEPARATOR ' || ') as 'Supplier Name',  " +
        //           "Group_concat(f.milestonepaymenttype_name  SEPARATOR ' || ') as 'Supplier Milestone Payment Type',  " +
        //           "Group_concat(f.supplier_vintage  SEPARATOR ' || ') as 'Vintage of the Supplier with Buyer',  " +
        //           "Group_concat(l.commodity_name  SEPARATOR ' || ') as 'Commodity Name', " +
        //           "Group_concat(l.supplierpayment_type SEPARATOR ' || ') as 'Supplier Payment Type', " +
        //           "Group_concat(l.maxpercent_paymentterm SEPARATOR ' || ') as 'Maximum percentage of the payment term', " +
        //           "Group_concat(c.holdingmonthly_procurement  SEPARATOR ' || ') as 'Holding Monthly procurement service charges(%)', " +
        //           "Group_concat(c.holdingmonthly_procurement  SEPARATOR ' || ') as 'Holding Monthly procurement service charges(%)', " +
        //           "Group_concat(c.extendedholding_periods  SEPARATOR ' || ') as 'Extended holding period (months)', " +
        //           "Group_concat(c.extendedmonthly_procurement  SEPARATOR ' || ') as 'Extended Monthly procurement service charges(%)', " +
        //           "Group_concat(c.charges_extendedperiod  SEPARATOR ' || ') as 'Charges for an extended period (Amount) ', " +
        //           "Group_concat(c.customer_advance  SEPARATOR ' || ') as 'Customer advance (Amount) ', " +
        //           "Group_concat(c.reimburesementof_expenses  SEPARATOR ' || ') as 'Reimburesement of expenses', " +
        //           "Group_concat(c.reimburesementof_expensespenalty  SEPARATOR ' || ') as 'Reimburesement of expenses - Penalty', " +
        //           "Group_concat(c.needfor_stocking  SEPARATOR ' || ') as 'Need for Stocking', " +
        //           "Group_concat(c.product_portfolio  SEPARATOR ' || ') as 'Product portfolio', " +
        //           "Group_concat(c.production_capacity  SEPARATOR ' || ') as 'Production capacity', " +
        //           "Group_concat(c.natureof_operations  SEPARATOR ' || ') as 'Nature of operations', " +
        //           "Group_concat(c.averagemonthly_inventoryholding  SEPARATOR ' || ') as 'Average monthly inventory holding', " +
        //           "Group_concat(c.financialinstitutions_relationship  SEPARATOR ' || ') as 'Financial Institutions in Relationship with', " +
        //           "Group_concat(h.doc_charges  SEPARATOR ' || ') as 'Documentation Charges ', " +
        //           "Group_concat(h.doccharge_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)', " +
        //           "Group_concat(h.processing_fee  SEPARATOR ' || ') as 'Processing Fee / Initiation Fee ', " +
        //           "Group_concat(h.processing_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)', " +
        //           "Group_concat(h.fieldvisit_charges  SEPARATOR ' || ') as 'Field Visit Charges ', " +
        //           "Group_concat(h.fieldvisit_charges_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)', " +
        //           "Group_concat(h.adhoc_fee  SEPARATOR ' || ') as 'Adhoc Fee ', " +
        //           "Group_concat(h.adhoc_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)', " +
        //           "Group_concat(h.life_insurance  SEPARATOR ' || ') as 'Term Life Insurance ', " +
        //           "Group_concat(h.lifeinsurance_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)', " +
        //           "Group_concat(h.acct_insurance  SEPARATOR ' || ') as 'Personal Accident Insurance ', " +
        //           "Group_concat(h.acctinsurance_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)', " +
        //           "Group_concat(h.product_type SEPARATOR ' || ') as Product, " +
        //           "Group_concat(i.salescontract_availability  SEPARATOR ' || ') as 'Sales contract Applicability', " +
        //           "Group_concat(i.scopeof_transport  SEPARATOR ' || ') as 'Scope of transport', " +
        //           "Group_concat(i.scopeof_loading  SEPARATOR ' || ') as'Scope of loading', " +
        //           "Group_concat(i.scopeof_unloading  SEPARATOR ' || ') as'Scope of unloading',  " +
        //           "Group_concat(i.scopeof_qualityandquantity  SEPARATOR ' || ') as 'Scope of quality and quantity', " +
        //           "Group_concat(i.scopeof_moisturegainloss  SEPARATOR ' || ') as 'Scope of moisture gain/loss', " +
        //           "Group_concat(i.scopeof_insurance  SEPARATOR ' || ') as 'Scope of Insurance', " +
        //           "Group_concat(j.warehouse_agency  SEPARATOR ' || ') as 'Warehouse agency (WSP)',  " +
        //           "Group_concat(j.warehouse_name  SEPARATOR ' || ') as 'Warehouse name', " +
        //           "Group_concat(j.typeofwarehouse_name  SEPARATOR ' || ') as 'Warehouse type', " +
        //           "Group_concat(j.totalcapacity_area  SEPARATOR ' || ') as 'Total Capacity (In Area)',  " +
        //           "Group_concat(j.area_uom  SEPARATOR ' || ') as 'Capacity Area UOM', " +
        //           "Group_concat(j.totalcapacity_volume  SEPARATOR ' || ') as 'Total Capacity (Volume)', " +
        //           "Group_concat(j.volume_uom  SEPARATOR ' || ') as 'Volume UOM', " +
        //           "Group_concat(j.warehouse_address  SEPARATOR ' || ') as 'Warehouse Address' " +
        //           " from agr_mst_tapplication a  " +
        //           "left join agr_mst_tbyronboard b on a.buyeronboard_gid = b.application_gid  " +
        //           "left join agr_mst_tapplication2loan c on c.application_gid = a.application_gid  " +
        //           "left join agr_mst_tapploan2paymenttypecustomer d on d.application_gid = a.application_gid  " +
        //           "left join agr_mst_tapplication2product e on e.application2loan_gid = c.application2loan_gid  " +
        //           "left join agr_mst_tapploan2supplierdtl f on f.application_gid = a.application_gid  " +
        //           "left join agr_mst_tsupronboard g on g.application_gid = f.application_gid  " +
        //           "left join agr_mst_tapplicationservicecharge h on a.application_gid = h.application_gid  " +
        //           "left join agr_mst_tapplication2trade i on i.application_gid = a.application_gid  " +
        //           "left join agr_mst_tapplicationtrade2warehouse j on j.application_gid = a.application_gid  " +
        //           "left join agr_mst_tcommoditygststatus k on k.variety_gid = e.variety_gid " +
        //           "left join agr_mst_tapploan2supplierpayment l on l.application_gid = a.application_gid " +
        //            "where a.process_type = 'Accept' group by a.application_gid";

        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    string lscompany_code = string.Empty;
        //    MemoryStream ms = new MemoryStream();
        //    ExcelPackage excel = new ExcelPackage(ms);
        //    var workSheet = excel.Workbook.Worksheets.Add("PMG Accepted Application Report");
        //    try
        //    {
        //        msSQL = " select company_code from adm_mst_tcompany";

        //        lscompany_code = objdbconn.GetExecuteScalar(msSQL);
        //        objMstApplicationReport.lsname = "PMG Accepted Application Report.xlsx";
        //        var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGAcceptedApplicationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
        //        objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGAcceptedApplicationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
        //        bool exists = System.IO.Directory.Exists(path);
        //        if (!exists)
        //        {
        //            System.IO.Directory.CreateDirectory(path);
        //        }
        //        workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
        //        FileInfo file = new FileInfo(objMstApplicationReport.lspath);
        //        using (var range = workSheet.Cells[1, 1, 1, 106])  //Address "A1:A9"
        //        {
        //            range.Style.Font.Bold = true;
        //            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
        //            range.Style.Font.Color.SetColor(Color.White);
        //        }
        //        excel.SaveAs(ms);
        //        objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/PMGAcceptedApplicationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
        //        bool status;
        //        status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
        //        ms.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        objMstApplicationReport.status = false;
        //        objMstApplicationReport.message = "Failure";
        //    }
        //    objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
        //    objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
        //    objMstApplicationReport.status = true;
        //    objMstApplicationReport.message = "Success";
        //}



        public void DaExportDocCheckMakerPendingReport(MdlAgrMstApplicationReport objMstApplicationReport)
        {
            msSQL = " (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    "  date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as AC_Approved_Date, " +
                    " d.maker_name as Maker_Name,d.checker_name as Checker_Name," +
                    "  d.approver_name as Approver_Name,f.company_name as Institution_Name," +
                    "  '' as Individual_Name," +
                    "  f.stakeholder_type as Stakeholder_Type," +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from agr_mst_tinstitution  f  " +
                    "  left join  agr_mst_tapplication a on f.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    "  left join agr_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'AGDMGTDCL'  " +
                    "  left join agr_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join agr_trn_tdocumentchecktls i on f.institution_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join agr_trn_tcovanantdocumentcheckdtls j on f.institution_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    "  where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and  " +
                    "  a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL'  " +
                    "  and maker_approvalflag = 'N') " +
                    "  group by f.institution_gid )  UNION " +
                    "  (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as AC_Approved_Date, " +
                    "  d.maker_name as Maker_Name, " +
                    " d.checker_name as Checker_Name, " +
                    "  d.approver_name as Approver_Name, " +
                    "  '' as Institution_Name, " +
                    "  concat(g.first_name, ' ', g.last_name) as  Individual_Name, " +
                    "  g.stakeholder_type as Stakeholder_Type, " +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from agr_mst_tcontact g  " +
                    "  left join agr_mst_tapplication a on g.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    "  left join agr_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'AGDMGTDCL'  " +
                    "  left join agr_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join agr_trn_tdocumentchecktls i on g.contact_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join agr_trn_tcovanantdocumentcheckdtls j on g.contact_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    "  where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and  " +
                    "  a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL'   " +
                    "  and maker_approvalflag = 'N') " +
                    "  group by g.contact_gid ) ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet = excel.Workbook.Worksheets.Add("Document Checklist Maker Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Document Checklist Maker Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Document Checklist Maker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/Document Checklist Maker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Document Checklist Maker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 12])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }

        public void DaExportDocCheckCheckerPendingReport(MdlAgrMstApplicationReport objMstApplicationReport)
        {
            msSQL = " (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    "  date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as AC_Approved_Date, " +
                    "  d.maker_name as Maker_Name,d.checker_name as Checker_Name," +
                    "  d.approver_name as Approver_Name,f.company_name as Institution_Name," +
                    "  '' as Individual_Name," +
                    "  f.stakeholder_type as Stakeholder_Type," +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from agr_mst_tinstitution f  " +
                     "  left join agr_mst_tapplication  a on f.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                     "  left join agr_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'AGDMGTDCL'  " +
                    "  left join agr_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join agr_trn_tdocumentchecktls i on f.institution_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join agr_trn_tcovanantdocumentcheckdtls j on f.institution_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    " left join agr_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                     " where a.process_type = 'Accept' and k.maker_flag='Y' and k.checker_flag='N' and " +
                    " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' " +
                    " and checker_approvalflag = 'N')" +
                    "  group by f.institution_gid)   UNION " +
                    "  (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as AC_Approved_Date, " +
                    "  d.maker_name as Maker_Name, " +
                    " d.checker_name as Checker_Name, " +
                    "  d.approver_name as Approver_Name, " +
                    "  '' as Institution_Name, " +
                    "  concat(g.first_name, ' ', g.last_name) as  Individual_Name, " +
                    "  g.stakeholder_type as Stakeholder_Type, " +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from agr_mst_tcontact g  " +
                     "  left join agr_mst_tapplication  a on g.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    "  left join agr_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'AGDMGTDCL'  " +
                    "  left join agr_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join agr_trn_tdocumentchecktls i on g.contact_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join agr_trn_tcovanantdocumentcheckdtls j on g.contact_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    " left join agr_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                    " where a.process_type = 'Accept' and k.maker_flag='Y' and k.checker_flag='N' and " +
                    " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' " +
                    " and checker_approvalflag = 'N')" +
                    "  group by g.contact_gid  )  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet = excel.Workbook.Worksheets.Add("Document Checklist Checker Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Document Checklist Checker Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Document Checklist Checker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/Document Checklist Checker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Document Checklist Checker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 12])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }

        public void DaExportDocCheckApprovalPendingReport(MdlAgrMstApplicationReport objMstApplicationReport)
        {
            msSQL = " (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    "  date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as AC_Approved_Date, " +
                    "  d.maker_name as Maker_Name,d.checker_name as Checker_Name," +
                    "  d.approver_name as Approver_Name,f.company_name as Institution_Name," +
                    "  '' as Individual_Name," +                  
                    "  f.stakeholder_type as Stakeholder_Type," +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from agr_mst_tinstitution f  " +
                     "  left join agr_mst_tapplication  a on f.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    "  left join agr_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'AGDMGTDCL'  " +
                    "  left join agr_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join agr_trn_tdocumentchecktls i on f.institution_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join agr_trn_tcovanantdocumentcheckdtls j on f.institution_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N') " +
                      " left join agr_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                    " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' and checker_flag='Y' " +
                     " and approver_approvalflag = 'N')" +
                    "  group by f.institution_gid )  UNION " +
                    "  (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as AC_Approved_Date, " +
                    "  d.maker_name as Maker_Name, " +
                    " d.checker_name as Checker_Name, " +
                    "  d.approver_name as Approver_Name, " +
                    "  '' as Institution_Name, " +
                    "  concat(g.first_name, ' ', g.last_name) as  Individual_Name, " +                  
                    "  g.stakeholder_type as Stakeholder_Type, " +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from agr_mst_tcontact g  " +
                    "  left join agr_mst_tapplication  a on g.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    "  left join agr_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'AGDMGTDCL'  " +
                    "  left join agr_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join agr_trn_tdocumentchecktls i on g.contact_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join agr_trn_tcovanantdocumentcheckdtls j on g.contact_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    " left join agr_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                    " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
                    " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' and checker_flag='Y' " +
                    " and approver_approvalflag = 'N')" +
                    "  group by g.contact_gid )  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet = excel.Workbook.Worksheets.Add("Document Checklist Approval Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Document Checklist Approval Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Document Checklist Approval Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/Document Checklist Approval Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Document Checklist Approval Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 12])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }

        public void DaGetCADDocChecklistReportCheckerSummary(string employee_gid, MdlAgrMstApplicationReport values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,f.sanction_refno, " +
                    " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                    " a.creditgroup_gid, e.cadgroup_name,a.customer_urn from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join agr_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                    " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                    " left join agr_trn_tapplication2sanction f on f.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.maker_flag='Y' and d.checker_flag='N' and " +
                    " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' " +
                    " and checker_approvalflag = 'N')" +
                    " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentpendinglist = new List<documentpendinglist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    }
                    else
                    {
                        lsccgroup_name = "";
                    }
                    objODBCDataReader.Close();
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    }
                    else
                    {
                        lsccadmin_name = "";
                    }
                    objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getdocumentpendinglist.Add(new documentpendinglist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = lsccgroup_name,
                        ccadmin_name = lsccadmin_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString()
                    });

                }
            }
            values.documentpendinglist = getdocumentpendinglist;
            dt_datatable.Dispose();
        }

        public void DaGetCADDocChecklistReportApprovalSummary(string employee_gid, MdlAgrMstApplicationReport values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,h.sanction_refno, " +
                     " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as checkerapproved_by, " +
                     " date_format(d.checkerapproved_on, '%d-%m-%Y %h:%i %p') as checkerapproved_on,a.customer_urn," +
                     " a.creditgroup_gid, docchecklist_approvalflag, d.approval_status as approval, g.cadgroup_name from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join agr_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " left join agr_trn_tprocesstype_assign g on g.application_gid = a.application_gid " +
                     " left join agr_trn_tapplication2sanction h on h.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' and checker_flag='Y' " +
                     " and approver_approvalflag = 'N')" +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentpendinglist = new List<documentpendinglist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    }
                    else
                    {
                        lsccgroup_name = "";
                    }
                    objODBCDataReader.Close();
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    }
                    else
                    {
                        lsccadmin_name = "";
                    }
                    objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getdocumentpendinglist.Add(new documentpendinglist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = lsccgroup_name,
                        ccadmin_name = lsccadmin_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        docchecklist_approvalflag = dt["docchecklist_approvalflag"].ToString(),
                        approval = dt["approval"].ToString(),
                        checkerapproved_by = dt["checkerapproved_by"].ToString(),
                        checkerapproved_on = dt["checkerapproved_on"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString()
                    });

                }
            }
            values.documentpendinglist = getdocumentpendinglist;
            dt_datatable.Dispose();
        }

        public void DaGetCADDocChecklistReportSummary(string employee_gid, MdlAgrMstApplicationReport values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,e.sanction_refno, " +
                     " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " a.creditgroup_gid, d.cadgroup_name,a.customer_urn from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join agr_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' " +
                     "  and maker_approvalflag = 'N')" +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentpendinglist = new List<documentpendinglist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccgroup_name = objODBCDataReader["ccgroup_name"].ToString();
                    }
                    else
                    {
                        lsccgroup_name = "";
                    }
                    objODBCDataReader.Close();
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        lsccadmin_name = objODBCDataReader["ccadmin_name"].ToString();
                    }
                    else
                    {
                        lsccadmin_name = "";
                    }
                    objODBCDataReader.Close();
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                    //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDataReader.HasRows == true)
                    //{
                    //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                    //}
                    //else
                    //{
                    //    lscadgroup_name = "";
                    //}
                    //objODBCDataReader.Close();
                    getdocumentpendinglist.Add(new documentpendinglist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        ccgroup_name = lsccgroup_name,
                        ccadmin_name = lsccadmin_name,
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString()
                    });

                }
            }
            values.documentpendinglist = getdocumentpendinglist;
            dt_datatable.Dispose();
        }

        public void DaGetDocChecklistPendingCount(string user_gid, string employee_gid, DocumentCount values)
        {
            msSQL = " select count(distinct (a.application_gid))  as cadsanction_count from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' " +
                     "  and maker_approvalflag = 'N')" +
                     "  order by a.updated_date desc ";
            values.cadmaker_count = objdbconn.GetExecuteScalar(msSQL);



            msSQL = " select count(distinct (a.application_gid))  as cadchecker_count from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join agr_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                    " left join agr_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.maker_flag='Y' and d.checker_flag='N' and " +
                    " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' " +
                    " and checker_approvalflag = 'N')" +
                    "  order by a.updated_date desc ";
            values.cadchecker_count = objdbconn.GetExecuteScalar(msSQL);



            msSQL = " select count(distinct (a.application_gid))  as cadcheckerapproval_count from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join agr_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " left join agr_trn_tprocesstype_assign g on g.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' and checker_flag='Y' " +
                     " and approver_approvalflag = 'N')" +
                     "  order by a.updated_date desc ";
            values.cadcheckerapproval_count = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " select count(distinct (a.application_gid))  as cadcheckerapproval_count from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join agr_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " left join agr_trn_tprocesstype_assign g on g.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='Y' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL'  " +
                     " and approver_approvalflag = 'Y')" +
                     "  order by a.updated_date desc ";
            values.cadapproved_count = objdbconn.GetExecuteScalar(msSQL);


        }
        // Warehouse report Summary
        public void DaGetMstWarehouseSummary(MdlAgrMstApplicationReport objMstWarehouseSummary)
        {
            msSQL = " select a.warehouse_gid, a.warehouse_ref_no, a.warehouse_name, d.product_gid, d.product_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                     " CASE WHEN(productapproval_flag = 'N' and pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                    " WHEN(productapproval_flag = 'Y' and pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                    " WHEN(productapproval_flag = 'R' and pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                    " WHEN(productapproval_flag = 'Y' and pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                    " ELSE 'Approved' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    "  from agr_mst_twarehouse a" +
                    " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " group by warehouse_gid order by warehouse_gid desc ";
           
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetMstWarehouseSummary = new List<MstWarehouseSummaryList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetMstWarehouseSummary.Add(new MstWarehouseSummaryList
                    {
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                        warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                        warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),

                    });
                }
                objMstWarehouseSummary.MstWarehouseSummaryList = objGetMstWarehouseSummary;
            }
            dt_datatable.Dispose();
            objMstWarehouseSummary.status = true;
            objMstWarehouseSummary.message = "Success";
        }
        // export excel Warehouse

        public void DaGetMstWarehouseReport(MdlAgrMstApplicationReport objMstApplicationReport)
        {
            msSQL = " select a.warehouse_ref_no as 'Warehouse Reference Number', a.warehouse_name as 'Warehouse Name', a.warehouse_pan as 'Warehouse PAN'," +
                    " a.first_name as 'First Name' , a.middle_name as 'Middle Name', a.last_name as 'Last Name', " +                  
                   " a.subsidiarywarshouse_name as 'Subsidiary Warehouse Name',a.warehouse_area as 'Warehouse Area', a.warehousearea_uom as 'Area UOM' , " +
                   " a.totalcapacity_area as 'Total Capacity Area',  a.area_uom as 'Ares UOM', a.totalcapacity_volume as 'Total Capacity Volume'," +
                   " a.volume_uom as 'Volume UOM', a.typeofwarehouse_name as 'Type of Warehouse Name',a.Applicant_name as 'Applicant Name'," +
                   " a.charges as 'Charges', a.capacity as 'Capacity', " +
                   " (select group_concat(addresstype_name SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'Address Type', " +
                   " (select group_concat(addressline1 SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'Address Line1', " +
                   " (select group_concat(addressline2 SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'Address Line2', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'Mobile Primary Status(Yes/No)', " +
                   " (select group_concat(landmark SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'LandMark', " +
                   " (select group_concat(postal_code SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'Postal Code', " +
                   " (select group_concat(city SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'City', " +
                   " (select group_concat(taluka SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'Taluka', " +
                   " (select group_concat(district SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'District', " +
                   " (select group_concat(state SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'State', " +
                   " (select group_concat(country SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'Country', " +
                   " (select group_concat(latitude SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'Latitude', " +
                   " (select group_concat(longitude SEPARATOR ' || ')  from agr_mst_twarehouse2address where warehouse_gid = a.warehouse_gid) as 'Longitude', " +
                   " (select group_concat(email_address SEPARATOR ' || ')  from agr_mst_twarehouse2email where warehouse_gid = a.warehouse_gid) as 'Email Address', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_twarehouse2email where warehouse_gid = a.warehouse_gid) as 'Email Primary Status (Yes/No)', " +
                   " (select group_concat(mobile_no SEPARATOR ' || ')  from agr_mst_twarehouse2mobileno where warehouse_gid = a.warehouse_gid) as 'Mobile Number', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_twarehouse2mobileno where warehouse_gid = a.warehouse_gid) as 'Mobile Primary Status(Yes/No)', " +
                   " (select group_concat(whatsapp_no SEPARATOR ' || ')  from agr_mst_twarehouse2mobileno where warehouse_gid = a.warehouse_gid) as 'WhatsApp Number(Yes/No)', " +
                   " (select group_concat(gst_state SEPARATOR ' || ')  from agr_mst_twarehouse2branch where warehouse_gid = a.warehouse_gid) as 'GST State', " +
                   " (select group_concat(gst_no SEPARATOR ' || ')  from agr_mst_twarehouse2branch where warehouse_gid = a.warehouse_gid) as 'GST Number', " +
                   " (select group_concat(gst_registered SEPARATOR ' || ')  from agr_mst_twarehouse2branch where warehouse_gid = a.warehouse_gid) as 'GST Registered', " +
                   " (select group_concat(product_name SEPARATOR ' || ') from agr_mst_twarehouse2commodity where warehouse_gid = a.warehouse_gid) as 'Commodity Name', " +                  
                   " (select group_concat(sector_name SEPARATOR ' || ') from agr_mst_twarehouse2commodity where warehouse_gid = a.warehouse_gid) as 'Sector Name', " +
                   " (select group_concat(category_name SEPARATOR ' || ') from agr_mst_twarehouse2commodity where warehouse_gid = a.warehouse_gid) as 'Category Name', " +
                   " (select group_concat(variety_name SEPARATOR ' || ') from agr_mst_twarehouse2commodity where warehouse_gid = a.warehouse_gid) as 'Variety Name', " +
                   " (select group_concat(botanical_name SEPARATOR ' || ') from agr_mst_twarehouse2commodity where warehouse_gid = a.warehouse_gid) as 'Botanical Name', " +
                   " (select group_concat(alternative_name SEPARATOR ' || ') from agr_mst_twarehouse2commodity where warehouse_gid = a.warehouse_gid) as 'Alternative Name', " +
                   " (select group_concat(hsn_code SEPARATOR ' || ') from agr_mst_twarehouse2commodity where warehouse_gid = a.warehouse_gid) as 'HSN Code', " +
                   " (select group_concat(spoc_name SEPARATOR ' || ')  from agr_mst_twarehouse2spoc where warehouse_gid = a.warehouse_gid) as 'Spoc Name', " +
                   " (select group_concat(spocmobile_no SEPARATOR ' || ')  from agr_mst_twarehouse2spoc where warehouse_gid = a.warehouse_gid) as 'Spoc Mobile Name', " +
                   " (select group_concat(warehousefacility_name SEPARATOR ' || ')  from agr_mst_twarehouse2facility where warehouse_gid = a.warehouse_gid) as 'Warehouse Facility Name', " +                  
                   " (select group_concat(warehouseagreement_address SEPARATOR ' || ')  from agr_mst_twarehouse2agreement where warehouse_gid = a.warehouse_gid) as 'Warehouse Agreement Address', " +
                   " (select group_concat(execution_date SEPARATOR ' || ')  from agr_mst_twarehouse2agreement where warehouse_gid = a.warehouse_gid) as 'Execution Date', " +
                   " (select group_concat(expiry_date SEPARATOR ' || ')  from agr_mst_twarehouse2agreement where warehouse_gid = a.warehouse_gid) as 'Expiry Date' " +   
                   " from agr_mst_twarehouse a " +
                   " left join agr_mst_twarehouse2address b on a.warehouse_gid = b.warehouse_gid" +
                   " left join agr_mst_twarehouse2email c on a.warehouse_gid = c.warehouse_gid" +
                   " left join agr_mst_twarehouse2mobileno d on a.warehouse_gid = d.warehouse_gid" +
                   " left join agr_mst_twarehouse2branch e on a.warehouse_gid = e.warehouse_gid" +
                   " left join agr_mst_twarehouse2commodity f on a.warehouse_gid = f.warehouse_gid" +
                   " left join agr_mst_twarehouse2spoc g on a.warehouse_gid = g.warehouse_gid" +
                   " left join agr_mst_twarehouse2facility h on a.warehouse_gid = h.warehouse_gid" +
                   " left join agr_mst_twarehouse2agreement i on a.warehouse_gid = i.warehouse_gid" +
                   " group by a.warehouse_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Warehouse_Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Warehouse_Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Warehouse_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Warehouse_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 51])  //Address "A1:X1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/Warehouse_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();


            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }
        
        // Other Creditor report Summary
        public void DaGetMstOtherCreditorSummary(MdlAgrMstApplicationReport objMstOtherCreditorSummary)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name,  a.approval_status as app_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " CASE WHEN( approval_status = 'N' and approval_submittedflag = 'N') THEN 'Pending' " +
                    " ELSE '' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    "  from agr_mst_tcreditor a" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.approved_by " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by creditor_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetMstOtherCreditorSummary = new List<MstOtherCreditorSummaryList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetMstOtherCreditorSummary.Add(new MstOtherCreditorSummaryList
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["app_status"].ToString()),

                    });
                }
                objMstOtherCreditorSummary.MstOtherCreditorSummaryList = objGetMstOtherCreditorSummary;
            }
            dt_datatable.Dispose();
            objMstOtherCreditorSummary.status = true;
            objMstOtherCreditorSummary.message = "Success";
        }
        // Export excel other creditor

        public void DaGetMstOtherCreditorReport(MdlAgrMstApplicationReport objMstApplicationReport)
        {
            msSQL = " select a.creditorref_no as 'Creditor Reference Number', a.Applicant_name as 'Applicant Name', a.Applicant_category as 'Applicant Category'," +
                    " a.Applicant_type as 'Applicant Type' , a.loanproduct_name as 'Loan Product Name', a.loansubproduct_name as 'Loan Subproduct Name', " +
                   " a.designation_type as 'Designation Type',a.contactperson_name as 'Contact Person Name', a.contact_no as 'Contact Number' , " +
                   " a.email_id as 'Email ID',  a.pan_no as 'PAN Number', a.aadhar_no as 'Aadhar Number'," +                                    
                   " (select group_concat(addresstype_name SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'Address Type', " +
                   " (select group_concat(addressline1 SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'Address Line1', " +
                   " (select group_concat(addressline2 SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'Address Line2', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'Mobile Primary Status(Yes/No)', " +
                   " (select group_concat(landmark SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'LandMark', " +
                   " (select group_concat(postal_code SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'Postal Code', " +
                   " (select group_concat(city SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'City', " +
                   " (select group_concat(taluka SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'Taluka', " +
                   " (select group_concat(district SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'District', " +
                   " (select group_concat(state SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'State', " +
                   " (select group_concat(country SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'Country', " +
                   " (select group_concat(latitude SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'Latitude', " +
                   " (select group_concat(longitude SEPARATOR ' || ')  from agr_mst_tcreditor2address where creditor_gid = a.creditor_gid) as 'Longitude', " +                  
                   " (select group_concat(accountholder_name SEPARATOR ' || ') from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Account Holder Name', " +
                   " (select group_concat(account_number SEPARATOR ' || ') from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Account Number', " +
                   " (select group_concat(bank_name SEPARATOR ' || ') from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Bank Name', " +
                   " (select group_concat(cheque_no SEPARATOR ' || ') from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Cheque Number', " +
                   " (select group_concat(ifsc_code SEPARATOR ' || ') from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'IFSC Code', " +
                   " (select group_concat(micr SEPARATOR ' || ') from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'MICR', " +
                   " (select group_concat(branch_address SEPARATOR ' || ') from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Branch Address', " +
                   " (select group_concat(branch_name SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Branch Name', " +
                   " (select group_concat(city SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'City', " +
                   " (select group_concat(district SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'District', " +
                   " (select group_concat(state SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'State', " +
                   " (select group_concat(mergedbankingentity_name SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Merged Banking Entity Name', " +
                   " (select group_concat(special_condition SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Special Condition', " +
                   " (select group_concat(general_remarks SEPARATOR ' || ') from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'General Remarks', " +
                   " (select group_concat(cts_enabled SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'CTS Enabled (Yes/No)', " +
                   " (select group_concat(primary_status SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Primary Status (Yes/No)', " +
                   " (select group_concat(cheque_type SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Cheque Type', " +
                   " (select group_concat(date_chequetype SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Date Cheque Type', " +
                   " (select group_concat(date_chequepresentation SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Date Cheque Presentation', " +
                   " (select group_concat(status_chequepresentation SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Status Cheque Presentation', " +
                   " (select group_concat(date_chequeclearance SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Date Cheque Clearance', " +
                   " (select group_concat(status_chequeclearance SEPARATOR ' || ')  from agr_mst_tcreditor2cheque where creditor_gid = a.creditor_gid) as 'Status Cheque Clearance', " +
                   " (select group_concat(gst_state SEPARATOR ' || ')  from agr_mst_tcreditor2branch where creditor_gid = a.creditor_gid) as 'GST State', " +
                   " (select group_concat(gst_no SEPARATOR ' || ')  from agr_mst_tcreditor2branch where creditor_gid = a.creditor_gid) as 'GST Number', " +
                   " (select group_concat(gst_registered SEPARATOR ' || ')  from agr_mst_tcreditor2branch where creditor_gid = a.creditor_gid) as 'GST Registered', " +
                   " (select group_concat(loanproduct_name SEPARATOR ' || ') from agr_mst_tcreditor2product where creditor_gid = a.creditor_gid) as 'Loan product Name', " +
                   " (select group_concat(loanproduct_name SEPARATOR ' || ') from agr_mst_tcreditorproduct2program where creditor_gid = a.creditor_gid) as 'Loan product Name', " +
                   " (select group_concat(loansubproduct_name SEPARATOR ' || ') from agr_mst_tcreditorproduct2program where creditor_gid = a.creditor_gid) as 'Loan Subproduct Name', " +
                   " (select group_concat(samcontactperson_name SEPARATOR ' || ')  from agr_mst_tcreditor2agreement where creditor_gid = a.creditor_gid) as 'Samunnati Contact Person Name', " +
                   " (select group_concat(agreementinvolvement_type SEPARATOR ' || ')  from agr_mst_tcreditor2agreement where creditor_gid = a.creditor_gid) as 'Agreement Involvement Type (Yes/No)', " +
                   " (select group_concat(creditor2agreement_no SEPARATOR ' || ')  from agr_mst_tcreditor2agreement where creditor_gid = a.creditor_gid) as 'Creditor Agreement Number', " +
                   " (select group_concat(execution_date SEPARATOR ' || ')  from agr_mst_tcreditor2agreement where creditor_gid = a.creditor_gid) as 'Execution Date', " +
                   " (select group_concat(expiry_date SEPARATOR ' || ')  from agr_mst_tcreditor2agreement where creditor_gid = a.creditor_gid) as 'Expiry Date' " +
                   " from agr_mst_tcreditor a " +
                   " left join agr_mst_tcreditor2branch b on a.creditor_gid = b.creditor_gid" +
                   " left join agr_mst_tcreditor2address c on a.creditor_gid = c.creditor_gid" +
                   " left join agr_mst_tcreditor2cheque d on a.creditor_gid = d.creditor_gid" +
                   " left join agr_mst_tcreditor2agreement e on a.creditor_gid = e.creditor_gid" +
                   " left join agr_mst_tcreditor2product f on a.creditor_gid = f.creditor_gid" +
                   " left join agr_mst_tcreditorproduct2program g on a.creditor_gid = g.creditor_gid" +                  
                   " group by a.creditor_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("OtherCreditor_Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "OtherCreditor_Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/OtherCreditor_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/OtherCreditor_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 58])  //Address "A1:X1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/OtherCreditor_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();

               
            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }

        public void DaGetPMGDocChecklistApprovalCompletedSummary(string employee_gid, MdlAgrMstApplicationReport values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, a.ccgroup_name," +
                     " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as checkerapproved_by, " +
                     " date_format(d.approved_on, '%d-%m-%Y %h:%i %p') as approved_on, " +
                     " date_format(d.checkerapproved_on, '%d-%m-%Y %h:%i %p') as checkerapproved_on,a.customer_urn, group_concat(h.ccadmin_name) as ccadmin_name , " +
                     " a.creditgroup_gid, docchecklist_approvalflag, d.approval_status as approval, g.cadgroup_name from agr_mst_tapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join agr_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " left join agr_trn_tprocesstype_assign g on g.application_gid = a.application_gid " +
                     "  left join agr_mst_tccschedulemeeting h on h.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and docchecklist_approvalflag='Y' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' " +
                     " and approver_approvalflag = 'Y')" +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentpendinglist = new List<documentpendinglist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                 
                    
                    getdocumentpendinglist.Add(new documentpendinglist
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
                        docchecklist_approvalflag = dt["docchecklist_approvalflag"].ToString(),
                        approval = dt["approval"].ToString(),
                        checkerapproved_by = dt["checkerapproved_by"].ToString(),
                        checkerapproved_on = dt["checkerapproved_on"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        approved_on = dt["approved_on"].ToString()
                    });

                }
            }
            values.documentpendinglist = getdocumentpendinglist;
            dt_datatable.Dispose();
        }


        public void DaExportDocCheckApprovalCompletedReport(MdlAgrMstApplicationReport objMstApplicationReport)
        {
            msSQL = " (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    "  date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as AC_Approved_Date, " +
                    "  d.maker_name as Maker_Name,d.checker_name as Checker_Name," +
                    "  d.approver_name as Approver_Name,f.company_name as Institution_Name," +
                    "  '' as Individual_Name," +
                    "  f.stakeholder_type as Stakeholder_Type," +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from agr_mst_tinstitution f  " +
                     "  left join agr_mst_tapplication  a on f.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    "  left join agr_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'AGDMGTDCL'  " +
                    "  left join agr_trn_tdocumentchecktls i on f.institution_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join agr_trn_tcovanantdocumentcheckdtls j on f.institution_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N') " +
                      " left join agr_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                    " where a.process_type = 'Accept' and  docchecklist_approvalflag='Y' and " +
                     " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' " +
                     " and approver_approvalflag = 'Y')" +
                    "  group by f.institution_gid )  UNION " +
                    "  (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as AC_Approved_Date, " +
                    "  d.maker_name as Maker_Name, " +
                    " d.checker_name as Checker_Name, " +
                    "  d.approver_name as Approver_Name, " +
                    "  '' as Institution_Name, " +
                    "  concat(g.first_name, ' ', g.last_name) as  Individual_Name, " +
                    "  g.stakeholder_type as Stakeholder_Type, " +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from agr_mst_tcontact g  " +
                    "  left join agr_mst_tapplication  a on g.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    "  left join agr_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'AGDMGTDCL'  " +
                    "  left join agr_trn_tdocumentchecktls i on g.contact_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join agr_trn_tcovanantdocumentcheckdtls j on g.contact_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    " left join agr_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                    " where a.process_type = 'Accept' and  docchecklist_approvalflag='Y' and " +
                    " a.application_gid in (Select application_gid from agr_trn_tprocesstype_assign where menu_gid = 'AGDMGTDCL' " +
                    " and approver_approvalflag = 'Y')" +
                    "  group by g.contact_gid )  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet = excel.Workbook.Worksheets.Add("Document Checklist Approval Completed Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Document Checklist Approval Completed Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Document Checklist Approval Completed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/Document Checklist Approval Completed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Document Checklist Approval Completed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 12])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }
            objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
            objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
            objMstApplicationReport.status = true;
            objMstApplicationReport.message = "Success";
        }


        public void DaGetExportCADAcceptedAppReport(ExportExcelReturnData objMstApplicationReport)
        {

            msSQL = " select a.contract_id as 'Contract ID', b.application_no as 'Customer ID', a.application_no as 'ARN Number',  " +
                    " CASE WHEN(a.renewal_flag = 'N' AND a.amendment_flag = 'N' AND a.shortclosing_flag = 'N') THEN 'New' ELSE(CASE WHEN(a.renewal_flag = 'Y') " + 
                    " THEN 'Renewal' ELSE(CASE WHEN(a.amendment_flag = 'Y') THEN 'Amendment' ELSE(CASE WHEN(a.shortclosing_flag = 'Y') THEN 'Short Closing' ELSE '' END) END) END) END AS 'Type', " +
                    " b.virtualaccount_number as 'VAN', a.customerref_name as 'Customer Name',  a.overalllimit_amount as 'Overalllimitamount', " +
                    " date_format(a.validityfrom_date, '%d-%m-%Y') as 'Validity From',  date_format(a.validityto_date, '%d-%m-%Y') as 'Validity to', " +
                    " a.calculationoveralllimit_validity as 'Calculation of overall limit validity', " +
                    " a.buyeragreement_id as 'Buyer Agreement ID', " +
                    " Group_concat(distinct f.supplieragreement_id  SEPARATOR ' || ') as 'Supplier Agreement ID' " +
                    " from agr_mst_tapplication a left join agr_mst_tbyronboard b on a.buyeronboard_gid = b.application_gid " +
                    " left join agr_mst_tapploan2supplierdtl f on f.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' group by a.application_gid ";
            dt_datatable1 = objdbconn.GetDataTable(msSQL);

            msSQL = " select a.application_no as 'ARN Number', date_format(c.facilityrequested_date, '%d-%m-%Y') as 'Facility Requested on', " +
                    " group_concat(distinct c.product_type SEPARATOR ' || ') as 'Product Name',   " +
                    "  group_concat(distinct c.productsub_type SEPARATOR ' || ') as 'Program Name',   " +
                    "  c.loan_type as 'Trade Type', c.loanfacility_amount as 'Proposed Program Limit',(a.overalllimit_amount - c.loanfacility_amount) as 'Remaining',  " +
                    "  c.rate_interest as 'Margin(%)', penal_interest as 'Penal Interest', " +
                    "  c.trade_orginatedby  as 'Trade Originated by','Financial statement method' as 'Scheme Type',  " +
                    " date_format( c.programlimit_validdfrom ,'%d-%m-%Y') as 'Program Limit Validity from', date_format(  c.programlimit_validdto ,'%d-%m-%Y') as  'Program Limit Validity to',   " +
                    "   c.programoverall_limit as 'Calculation of Program limit validity',   " +
                    "   c.tenureproduct_days as 'Credit Period Limit (days)',   c.tenureoverall_limit as 'Calculation of Credit Period Limit validity',   " +
                    "  c.SA_Brokerage as 'SA/Brokerage %','Financial statement method' as 'Scheme Type', c.facility_type as 'Product Type',    " +
                    "   c.Facility_mode as 'Program Mode', c.enduse_purpose as 'Purpose of Trade' from agr_mst_tapplication a   " +
                    " left join agr_mst_tapplication2loan c on c.application_gid = a.application_gid " +
                    "  where a.process_type = 'Accept' group by a.application_gid ";
            dt_datatable2 = objdbconn.GetDataTable(msSQL);

            msSQL = " select a.application_no as 'ARN Number', Group_concat(distinct d.customerpaymenttype_name SEPARATOR ' || ') as  'Customer Payment Type',   " +
                    " Group_concat(distinct d.maximumpercent_paymentterm  SEPARATOR ' || ') as 'Max Percentage',      " +
                    //" Group_concat(distinct e.insurance_cost  SEPARATOR ' || ') as 'Insurance Limit',     " +
                    "  Group_concat(distinct e.product_name  SEPARATOR ' || ') as 'Product',     " +
                    "  Group_concat(distinct k.IGST_percent  SEPARATOR ' || ') as 'IGST %',   " +
                    "   Group_concat(distinct k.SGST_percent  SEPARATOR ' || ') as 'SGST %',   " +
                    "  Group_concat(distinct k.CGST_percent  SEPARATOR ' || ') as 'CGST %',     " +
                    "  Group_concat(distinct k.CESS_percent  SEPARATOR ' || ') as 'Cess %',    " +
                    "  Group_concat(distinct date_format( k.wef_date ,'%d-%m-%Y') SEPARATOR ' || ') as 'w.e.f date',   " +
                    "   Group_concat(distinct e.unitpricevalue_commodity  SEPARATOR ' || ') as 'Unit price value of the commodity',     " +
                    "   Group_concat(distinct e.natureformstate_commodity  SEPARATOR ' || ') as ' Nature form state of commodity',    " +
                    "  Group_concat(distinct e.qualityof_commodity  SEPARATOR ' || ') as 'Quality of the Commodity',   " +
                    "  Group_concat(distinct e.quantity  SEPARATOR ' || ') as 'Quantity of the Commodity',   " +
                    " Group_concat(distinct e.uom_name  SEPARATOR ' || ') as 'UOM of the Commodity',  " +
                    "  Group_concat(distinct e.milestone_applicability  SEPARATOR ' || ') as 'Milestone Applicability',    " +
                    "   Group_concat(distinct e.insurance_applicability  SEPARATOR ' || ') as 'Insurance Applicability',    " +
                    "   Group_concat(distinct e.milestonepayment_name  SEPARATOR ' || ') as 'Milestone Payment Type',   " +
                    "   Group_concat(distinct e.sa_payout  SEPARATOR ' || ') as 'SApayout %',   " +
                    "   Group_concat(distinct e.insurance_percent  SEPARATOR ' || ') as 'Insurance %',  " +
                    "   Group_concat(distinct e.insurance_cost SEPARATOR ' || ') as 'Insurance limit',   " +
                    "   Group_concat(distinct e.markto_marketvalue SEPARATOR ' || ') as 'Mark to Market Value (%)',   " +
                    "  Group_concat(distinct e.pricereference_source SEPARATOR ' || ') as 'Price Reference/Source',   " +
                    "   Group_concat(distinct e.overallcreditperiod_limit  SEPARATOR ' || ') as 'Calculation of Credit Period limit Validity',   " +
                    "   Group_concat(distinct e.commodity_margin  SEPARATOR ' || ') as 'Margin %',    " +
                    "   Group_concat(distinct g.application_no  SEPARATOR ' || ') as 'Supplier ID',  " +
                    " Group_concat(distinct f.supplier_name  SEPARATOR ' || ') as 'Supplier Name',  " +
                   "  Group_concat(distinct f.milestonepaymenttype_name  SEPARATOR ' || ') as 'Supplier Milestone Payment Type',  " +
                  " Group_concat(distinct f.supplier_vintage  SEPARATOR ' || ') as 'Vintage of the Supplier with Buyer', " +
                 "  Group_concat(distinct l.commodity_name  SEPARATOR ' || ') as 'Commodity Name',  " +
                 "  Group_concat(distinct l.supplierpayment_type SEPARATOR ' || ') as 'Supplier Payment Type',  " +
                 "  Group_concat(distinct l.maxpercent_paymentterm SEPARATOR ' || ') as 'Maximum percentage of the payment term', " +
                 "  Group_concat(distinct c.holdingmonthly_procurement  SEPARATOR ' || ') as 'Holding Monthly procurement service charges(%)',  " +
                "   Group_concat(distinct c.holdingmonthly_procurement  SEPARATOR ' || ') as 'Holding Monthly procurement service charges(%)',  " +
                 "  Group_concat(distinct c.extendedholding_periods  SEPARATOR ' || ') as 'Extended holding period (months)', " +
                "   Group_concat(distinct c.extendedmonthly_procurement  SEPARATOR ' || ') as 'Extended Monthly procurement service charges(%)', " +
                "   Group_concat(distinct c.charges_extendedperiod  SEPARATOR ' || ') as 'Charges for an extended period (Amount) ',  " +
                "   Group_concat(distinct c.customer_advance  SEPARATOR ' || ') as 'Customer advance (Amount) ', " +
                "   Group_concat(distinct c.reimburesementof_expenses  SEPARATOR ' || ') as 'Reimburesement of expenses',  " +
                "   Group_concat(distinct c.reimburesementof_expensespenalty  SEPARATOR ' || ') as 'Reimburesement of expenses - Penalty', " +
                "   Group_concat(distinct c.needfor_stocking  SEPARATOR ' || ') as 'Need for Stocking',  " +
                "   Group_concat(distinct c.product_portfolio  SEPARATOR ' || ') as 'Product portfolio',  " +
                "   Group_concat(distinct c.production_capacity  SEPARATOR ' || ') as 'Production capacity', " +
               "    Group_concat(distinct c.natureof_operations  SEPARATOR ' || ') as 'Nature of operations', " +
               "    Group_concat(distinct c.averagemonthly_inventoryholding  SEPARATOR ' || ') as 'Average monthly inventory holding', " +
                "   Group_concat(distinct c.financialinstitutions_relationship  SEPARATOR ' || ') as 'Financial Institutions in Relationship with' " +
                 "   from agr_mst_tapplication a left join agr_mst_tapplication2loan c on c.application_gid = a.application_gid " +
                  "  left join agr_mst_tapploan2paymenttypecustomer d on d.application_gid = a.application_gid " +
                  "  left join agr_mst_tapplication2product e on e.application2loan_gid = c.application2loan_gid " +
                  "  left join agr_mst_tapploan2supplierdtl f on f.application_gid = a.application_gid " +
                  " left join agr_mst_tsupronboard g on g.application_gid = f.application_gid " +
                  "  left join agr_mst_tcommoditygststatus k on k.variety_gid = e.variety_gid " +
                  "  left join agr_mst_tapploan2supplierpayment l on l.application_gid = a.application_gid " +
                  " where a.process_type = 'Accept' group by a.application_gid ";

            dt_datatable3 = objdbconn.GetDataTable(msSQL);

            msSQL = " select a.application_no as 'ARN Number', Group_concat(distinct h.doc_charges  SEPARATOR ' || ') as 'Documentation Charges ',  " +
                    " Group_concat(distinct h.doccharge_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)',  " +
                    " Group_concat(distinct h.processing_fee  SEPARATOR ' || ') as 'Processing Fee / Initiation Fee ',  " +
                    " Group_concat(distinct h.processing_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)',  " +
                    " Group_concat(distinct h.fieldvisit_charges  SEPARATOR ' || ') as 'Field Visit Charges ',  " +
                    " Group_concat(distinct h.fieldvisit_charges_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)', " +
                    " Group_concat(distinct h.adhoc_fee  SEPARATOR ' || ') as 'Adhoc Fee ',  " +
                    " Group_concat(distinct h.adhoc_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)', " +
                    " Group_concat(distinct h.life_insurance  SEPARATOR ' || ') as 'Term Life Insurance ',  " +
                    " Group_concat(distinct h.lifeinsurance_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)',  " +
                    " Group_concat(distinct h.acct_insurance  SEPARATOR ' || ') as 'Personal Accident Insurance ',  " +
                    " Group_concat(distinct h.acctinsurance_collectiontype  SEPARATOR ' || ') as 'Collection Type (Collect / Deduct)',  " +
                    " Group_concat(distinct h.product_type SEPARATOR ' || ') as Product " +
                    " from agr_mst_tapplication a left join agr_mst_tapplicationservicecharge h on a.application_gid = h.application_gid " +
                    " where a.process_type = 'Accept' group by a.application_gid " ;

            dt_datatable4 = objdbconn.GetDataTable(msSQL);

            msSQL = "  select a.application_no as 'ARN Number', Group_concat(distinct i.salescontract_availability  SEPARATOR ' || ') as 'Sales contract Applicability',  " +
                   " Group_concat(distinct i.scopeof_transport  SEPARATOR ' || ') as 'Scope of transport',  " +
                   " Group_concat(distinct i.scopeof_loading  SEPARATOR ' || ') as'Scope of loading',  " +
                  " Group_concat(distinct i.scopeof_unloading  SEPARATOR ' || ') as'Scope of unloading', " +
                   " Group_concat(distinct i.scopeof_qualityandquantity  SEPARATOR ' || ') as 'Scope of quality and quantity',  " +
                   " Group_concat(distinct i.scopeof_moisturegainloss  SEPARATOR ' || ') as 'Scope of moisture gain/loss',  " +
                  " Group_concat(distinct i.scopeof_insurance  SEPARATOR ' || ') as 'Scope of Insurance',  " +
                   " Group_concat(distinct j.warehouse_agency  SEPARATOR ' || ') as 'Warehouse agency (WSP)',   " +
                   " Group_concat(distinct j.warehouse_name  SEPARATOR ' || ') as 'Warehouse name',  " +
                   " Group_concat(distinct j.typeofwarehouse_name  SEPARATOR ' || ') as 'Warehouse type',  " +
                  "  Group_concat(distinct j.totalcapacity_area  SEPARATOR ' || ') as 'Total Capacity (In Area)', " +
                  " Group_concat(distinct j.area_uom  SEPARATOR ' || ') as 'Capacity Area UOM', " +
                  " Group_concat(distinct j.totalcapacity_volume  SEPARATOR ' || ') as 'Total Capacity (Volume)',  " +
                  " Group_concat(distinct j.volume_uom  SEPARATOR ' || ') as 'Volume UOM',  " +
                   " Group_concat(distinct j.warehouse_address  SEPARATOR ' || ') as 'Warehouse Address' , " +
                   " Group_concat(distinct m.Applicant_name  SEPARATOR ' || ') as 'Applicant Name'  ," +
                   " Group_concat(distinct m.Applicant_category  SEPARATOR ' || ') as 'Applicant Category' ," +
                   " Group_concat(distinct m.designation_type  SEPARATOR ' || ') as 'Designation' ," +
                   " Group_concat(distinct m.contactperson_name  SEPARATOR ' || ') as 'Contact Person Name' ," +
                   " Group_concat(distinct m.contact_no  SEPARATOR ' || ') as 'Contact Person Number' ," +
                   " Group_concat(distinct m.email_id  SEPARATOR ' || ') as 'E-mail ID' ," +
                   " Group_concat(distinct m.pan_no  SEPARATOR ' || ') as 'Pan No'" +
                  " from agr_mst_tapplication a " +
                  " left join agr_mst_tapplication2trade i on i.application_gid = a.application_gid " +
                 "  left join agr_mst_tapplicationtrade2warehouse j on j.application_gid = a.application_gid " +
                 " left join agr_mst_tapplicationtrade2creditor m on m.application_gid = a.application_gid  " +
                  " where a.process_type = 'Accept' group by a.application_gid;  ";

            dt_datatable5 = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet1 = excel.Workbook.Worksheets.Add("Application Details");
            var workSheet2 = excel.Workbook.Worksheets.Add("Product Details");
            var workSheet3 = excel.Workbook.Worksheets.Add("Commodity & Supplier Details");
            var workSheet4 = excel.Workbook.Worksheets.Add("Service charges");
            var workSheet5 = excel.Workbook.Worksheets.Add("Trade Details");

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "PMG Accepted Application Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGAcceptedApplicationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "SamAgro/PMGAcceptedApplicationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PMGAcceptedApplicationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet1.Cells[1, 1].LoadFromDataTable(dt_datatable1, true);
                workSheet2.Cells[1, 1].LoadFromDataTable(dt_datatable2, true);
                workSheet3.Cells[1, 1].LoadFromDataTable(dt_datatable3, true);
                workSheet4.Cells[1, 1].LoadFromDataTable(dt_datatable4, true);
                workSheet5.Cells[1, 1].LoadFromDataTable(dt_datatable5, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range1 = workSheet1.Cells[1, 1, 1, 12])  //Address "A1:BD1"
                using (var range2 = workSheet2.Cells[1, 1, 1, 21])  //Address "A2:BD1"
                using (var range3 = workSheet3.Cells[1, 1, 1, 46])  //Address "A1:BD1"
                using (var range4 = workSheet4.Cells[1, 1, 1, 14])
                using (var range5 = workSheet5.Cells[1, 1, 1, 23])
                {
                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);

                    range2.Style.Font.Bold = true;
                    range2.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range2.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range2.Style.Font.Color.SetColor(Color.White);

                    range3.Style.Font.Bold = true;
                    range3.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range3.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range3.Style.Font.Color.SetColor(Color.White);

                    range4.Style.Font.Bold = true;
                    range4.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range4.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range4.Style.Font.Color.SetColor(Color.White);

                    range5.Style.Font.Bold = true;
                    range5.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range5.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range5.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstApplicationReport.lscloudpath, ms);
                ms.Close();
                objMstApplicationReport.lscloudpath = objcmnstorage.EncryptData(objMstApplicationReport.lscloudpath);
                objMstApplicationReport.lspath = objcmnstorage.EncryptData(objMstApplicationReport.lspath);
                objMstApplicationReport.status = true;
                objMstApplicationReport.message = "Success";
            }

            catch (Exception ex)
            {
                objMstApplicationReport.status = false;
                objMstApplicationReport.message = "Failure";
            }


        }

        public void DaGetConsolidatedSanctionReport(MdlMstAssignmentview values)
        {

            msSQL = "call agr_trn_tspconsolidatedsanctionreport";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassignment_list = new List<assignment_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getassignment_list.Add(new assignment_list
                    {
                        processtypeassign_gid = (dr_datarow["processtypeassign_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        processtype_name = (dr_datarow["processtype_name"].ToString()),
                        cadgroup_name = (dr_datarow["cadgroup_name"].ToString()),
                        menu_name = (dr_datarow["menu_name"].ToString()),
                        maker_name = (dr_datarow["maker_name"].ToString()),
                        checker_name = (dr_datarow["checker_name"].ToString()),
                        approver_name = (dr_datarow["approver_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        cadgroup_gid = (dr_datarow["cadgroup_gid"].ToString()),
                        menu_gid = (dr_datarow["menu_gid"].ToString()),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        overall_approvalstatus = (dr_datarow["overall_approvalstatus"].ToString()),
                        application_no = (dr_datarow["application_no"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                        contract_id = (dr_datarow["contract_id"].ToString()),
                        sanction_amount = (dr_datarow["sanction_amount"].ToString()),
                        maker_approvalflag = (dr_datarow["maker_approvalflag"].ToString()),
                        checker_approvalflag = (dr_datarow["checker_approvalflag"].ToString()),
                        approver_approvalflag = (dr_datarow["approver_approvalflag"].ToString()),
                        sanction_status = (dr_datarow["sanction_status"].ToString()),

                    });
                }
                values.assignment_list = getassignment_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetConsolidatedLSAReport(MdlMstAssignmentview values)
        {

            msSQL = "call agr_trn_tspconsolidatedlsareport";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassignment_list = new List<assignment_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getassignment_list.Add(new assignment_list
                    {
                        processtypeassign_gid = (dr_datarow["processtypeassign_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        processtype_name = (dr_datarow["processtype_name"].ToString()),
                        cadgroup_name = (dr_datarow["cadgroup_name"].ToString()),
                        menu_name = (dr_datarow["menu_name"].ToString()),
                        maker_name = (dr_datarow["maker_name"].ToString()),
                        checker_name = (dr_datarow["checker_name"].ToString()),
                        approver_name = (dr_datarow["approver_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        cadgroup_gid = (dr_datarow["cadgroup_gid"].ToString()),
                        menu_gid = (dr_datarow["menu_gid"].ToString()),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        overall_approvalstatus = (dr_datarow["overall_approvalstatus"].ToString()),
                        application_no = (dr_datarow["application_no"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                        lsa_amount = (dr_datarow["lsa_amount"].ToString()),
                        maker_approvalflag = (dr_datarow["maker_approvalflag"].ToString()),
                        checker_approvalflag = (dr_datarow["checker_approvalflag"].ToString()),
                        approver_approvalflag = (dr_datarow["approver_approvalflag"].ToString()),
                    });
                }
                values.assignment_list = getassignment_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetConsolidatedDocumentChecklistReport(MdlMstAssignmentview values)
        {

            msSQL = "call agr_trn_tspconsolidateddocumentchecklistreport";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassignment_list = new List<assignment_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getassignment_list.Add(new assignment_list
                    {
                        processtypeassign_gid = (dr_datarow["processtypeassign_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        processtype_name = (dr_datarow["processtype_name"].ToString()),
                        cadgroup_name = (dr_datarow["cadgroup_name"].ToString()),
                        menu_name = (dr_datarow["menu_name"].ToString()),
                        maker_name = (dr_datarow["maker_name"].ToString()),
                        checker_name = (dr_datarow["checker_name"].ToString()),
                        approver_name = (dr_datarow["approver_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        cadgroup_gid = (dr_datarow["cadgroup_gid"].ToString()),
                        menu_gid = (dr_datarow["menu_gid"].ToString()),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        overall_approvalstatus = (dr_datarow["overall_approvalstatus"].ToString()),
                        application_no = (dr_datarow["application_no"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                        maker_approvalflag = (dr_datarow["maker_approvalflag"].ToString()),
                        checker_approvalflag = (dr_datarow["checker_approvalflag"].ToString()),
                        approver_approvalflag = (dr_datarow["approver_approvalflag"].ToString()),
                    });
                }
                values.assignment_list = getassignment_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetConsolidatedSoftcopyVettingReport(MdlMstAssignmentview values)
        {

            msSQL = "call agr_trn_tspconsolidatedsoftcopyvettingreport";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassignment_list = new List<assignment_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getassignment_list.Add(new assignment_list
                    {
                        processtypeassign_gid = (dr_datarow["processtypeassign_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        processtype_name = (dr_datarow["processtype_name"].ToString()),
                        cadgroup_name = (dr_datarow["cadgroup_name"].ToString()),
                        menu_name = (dr_datarow["menu_name"].ToString()),
                        maker_name = (dr_datarow["maker_name"].ToString()),
                        checker_name = (dr_datarow["checker_name"].ToString()),
                        approver_name = (dr_datarow["approver_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        cadgroup_gid = (dr_datarow["cadgroup_gid"].ToString()),
                        menu_gid = (dr_datarow["menu_gid"].ToString()),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        overall_approvalstatus = (dr_datarow["overall_approvalstatus"].ToString()),
                        application_no = (dr_datarow["application_no"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                        queryraised_on = (dr_datarow["queryraised_on"].ToString()),
                        queryclosed_on = (dr_datarow["queryclosed_on"].ToString()),
                        rmupload_date = (dr_datarow["rmupload_date"].ToString()),
                        maker_approvalflag = (dr_datarow["maker_approvalflag"].ToString()),
                        checker_approvalflag = (dr_datarow["checker_approvalflag"].ToString()),
                        approver_approvalflag = (dr_datarow["approver_approvalflag"].ToString()),
                    });
                }
                values.assignment_list = getassignment_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetConsolidatedOriginalCopyVettingReport(MdlMstAssignmentview values)
        {

            msSQL = "call agr_trn_tspconsolidatedoriginalcopyvettingreport";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassignment_list = new List<assignment_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getassignment_list.Add(new assignment_list
                    {
                        processtypeassign_gid = (dr_datarow["processtypeassign_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        processtype_name = (dr_datarow["processtype_name"].ToString()),
                        cadgroup_name = (dr_datarow["cadgroup_name"].ToString()),
                        menu_name = (dr_datarow["menu_name"].ToString()),
                        maker_name = (dr_datarow["maker_name"].ToString()),
                        checker_name = (dr_datarow["checker_name"].ToString()),
                        approver_name = (dr_datarow["approver_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        cadgroup_gid = (dr_datarow["cadgroup_gid"].ToString()),
                        menu_gid = (dr_datarow["menu_gid"].ToString()),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        overall_approvalstatus = (dr_datarow["overall_approvalstatus"].ToString()),
                        application_no = (dr_datarow["application_no"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                        queryraised_on = (dr_datarow["queryraised_on"].ToString()),
                        queryclosed_on = (dr_datarow["queryclosed_on"].ToString()),
                        rmupload_date = (dr_datarow["rmupload_date"].ToString()),
                        maker_approvalflag = (dr_datarow["maker_approvalflag"].ToString()),
                        checker_approvalflag = (dr_datarow["checker_approvalflag"].ToString()),
                        approver_approvalflag = (dr_datarow["approver_approvalflag"].ToString()),
                    });
                }
                values.assignment_list = getassignment_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaCADConsolidatedReportCount(string employee_gid, CadConsolidatedRportCount values)
        {

            msSQL = "call agr_trn_tspcadconsolidatedcount";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.SanctionCount = objODBCDataReader["SanctionCount"].ToString();
                //values.LSACount = objODBCDataReader["LSACount"].ToString();
                values.DocumentCheckListCount = objODBCDataReader["DocumentCheckListCount"].ToString();
                values.SoftcopyVettingCount = objODBCDataReader["SoftcopyVettingCount"].ToString();
                values.OriginalCopyVettingCount = objODBCDataReader["OriginalCopyVettingCount"].ToString();


            }
            objODBCDataReader.Close();

        }

    }
}