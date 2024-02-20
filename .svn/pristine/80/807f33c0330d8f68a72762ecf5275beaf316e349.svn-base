using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Configuration;
using System.IO;
using System.Linq;
using ems.storage.Functions;
using System.Reflection;


/// <summary>
/// (It's used for ApplicationReport) ApplicationReport DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaMstApplicationReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        //DaCustomerMailTrigger objcmt = new DaCustomerMailTrigger();
        DataTable dt_datatable, dt_child, dt_datatable1, dt_datatable2;
        OdbcDataReader objODBCDatareader;
        OdbcDataReader objODBCDataReader, objODBCDataReader1;
        string msSQL, msGetGid;
        int mnResult;
        string lsmaster_value;
        //count
        public void DaGetApplicationCounts(string employee_gid, string user_gid, ApplicationListCount values)
        {

            msSQL = "SELECT COUNT(approval_status) as ApplicationCount FROM ocs_mst_tApplication WHERE approval_status='Submitted to Underwriting' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_submit = objODBCDataReader["ApplicationCount"].ToString();
            }
            objODBCDataReader.Close();



            msSQL = "SELECT COUNT(approval_status) as ApplicationCount FROM ocs_mst_tApplication WHERE approval_status='Incomplete' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_incomplete = objODBCDataReader["ApplicationCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(approval_status) as ApplicationCount FROM ocs_mst_tApplication  ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_Report = objODBCDataReader["ApplicationCount"].ToString();
            }
            objODBCDataReader.Close();


        }

        public void DaGetMstAppSummary(MstApplicationReport objMstAppSummary)
        {
            msSQL = " select a.application_gid, a.application_no, a.customer_name,a.customerref_name,a.vertical_name,a.applicant_type,a.approval_status,a.overalllimit_amount," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p')as 'created_date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p')as 'submitted_date'," +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')as 'updated_date'," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) " +
                    " as 'updated_by' from ocs_mst_tapplication a" +
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
                        created_date = (dr_datarow["created_date"].ToString()),                       

                    });
                }
                objMstAppSummary.MstAppSummaryList = objGetMstAppSummary;
            }
            dt_datatable.Dispose();
            objMstAppSummary.status = true;
            objMstAppSummary.message = "Success";
        }

        // Application Visit Report export excel

        public void DaGetMstApplicationVisitReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " select statusupdated_by as Process_Stage, " +
        " case when statusupdated_by ='RM' then concat(i.user_firstname, ' ', i.user_lastname, ' / ', i.user_code) else concat(k.user_firstname, ' ', k.user_lastname, ' / ', k.user_code) end as 'RM / CM', " +
        " customer_name as Customer_Name,customer_urn as URN_No,approval_status as 'Application Status', " +
        " application_no as Application_Number,applicationvisit_date as Date_of_Visit, " +
        " GROUP_CONCAT(inspectingofficials_name) as Names_of_the_Inspecting_officials,   " +
        " visitdone_name as Where_the_visit_was_done, a.applicationvisit_gid, " +
        " GROUP_CONCAT(distinct Concat(ifnull(g.primary_status,''), ' - ', ifnull(g.address_line1,''),'',ifnull(g.address_line2,''),'',  " +
        " ifnull(g.landmark,''),'',ifnull(g.postal_code,''),'',ifnull(g.city,''),'',ifnull(g.taluk,''),'',ifnull(g.district,''),'',ifnull(g.state_name,'') " +
        " ,'',ifnull(g.country,''),' ',ifnull(g.latitude,''),'',ifnull(g.longitude,''))SEPARATOR ' || ') as 'Address Details', " +
        " GROUP_CONCAT(distinct Concat((Concat (clientrepresentative_name)), ' - ', ifnull(clientrepresentative_designationname,''),'',ifnull(personal_mail,''),'',  " +
        " ifnull(office_mail,''),'',ifnull(f.mobile_no,''),'',ifnull(f.primary_status,''),'',ifnull(f.whatsapp_mobileno,''))SEPARATOR ' || ') as 'Visited Person Details', " +
        " REPLACE(REPLACE(clientkmp_activities, '\r', ' '), '\n', ' ')as Client_and_KMP_Their_activities, " +
        " REPLACE(REPLACE(promoter_background, '\r', ' '), '\n', ' ')as Promoter_Background, " +
        " REPLACE(REPLACE(overall_observations, '\r', ' '), '\n', ' ')as Overall_Observations, " +
        " REPLACE(REPLACE(inspectingofficial_recommenation, '\r', ' '), '\n', ' ')as Recommendation_of_the_inspecting_officials, " +
        " REPLACE(REPLACE(trading_relationship, '\r', ' '), '\n', ' ')as Trading_Releationship, " +
        " REPLACE(REPLACE(summary, '\r', ' '), '\n', ' ') as Summary " +
        " , date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'Created Date', " +
        " concat(m.user_firstname, ' ', m.user_lastname, ' / ', m.user_code) as 'Created By', " +
        " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as 'Updated Date',  " +
        " concat(o.user_firstname, ' ', o.user_lastname, ' / ', o.user_code)as 'Updated By' " +
        " from ocs_mst_tapplicationvisitreport a " +
        " left join ocs_mst_tapplication b on a.application_gid=b.application_gid " +
        " left join ocs_mst_tapplicationvisit2inspectingofficial c on a.applicationvisit_gid=c.applicationvisit_gid " +
        " left join ocs_mst_tapplicationvisit2visitdone d on a.applicationvisit_gid=d.applicationvisit_gid " +
        " left join ocs_mst_tapplicationvisit2person e on a.applicationvisit_gid=e.applicationvisit_gid " +
        " left join ocs_mst_tapplicationvisitperson2contactno f on e.applicationvisit2person_gid=f.applicationvisit2person_gid " +
        " left join ocs_mst_tapplicationvisit2address g on a.applicationvisit_gid=g.applicationvisit_gid " +
        " left join hrm_mst_temployee h on b.relationshipmanager_gid = h.employee_gid " +
        " left join adm_mst_tuser i on i.user_gid = h.user_gid  " +
        " left join hrm_mst_temployee j on b.creditmanager_gid=j.employee_gid " +
        " left join adm_mst_tuser k on k.user_gid = j.user_gid  " +
        " left join hrm_mst_temployee l on a.created_by = l.employee_gid " +
        " left join adm_mst_tuser m on m.user_gid = l.user_gid  " +
        " left join hrm_mst_temployee n on a.updated_by =n.employee_gid " +
        " left join adm_mst_tuser o on o.user_gid = n.user_gid  " +
        " group by a.applicationvisit_gid; ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Visit Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Visit Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Visit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Visit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Visit Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
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

        // Application Report export excel

        public void DaGetMstApplicationReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " select  a.application_no as 'Application Number',a.customerref_name as 'Customer Name', " +
                    " a.vertical_name as 'Vertical Name',a.applicant_type as 'Applicant Type',a.approval_status as 'Approval Status', " +
                    " a.overalllimit_amount as 'Overall Limit', " +
                    " a.baselocation_name as 'RM Location', concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as 'RM', " +
                    " concat(i.user_firstname, ' ', i.user_lastname, ' / ', i.user_code) as 'DRM', " +
                    " concat(k.user_firstname, ' ', k.user_lastname, ' / ', k.user_code) as 'CH', " +
                    " concat(m.user_firstname, ' ', m.user_lastname, ' / ', m.user_code) as 'RH', " +
                    " concat(o.user_firstname, ' ', o.user_lastname, ' / ', o.user_code) as 'ZH', " +
                    " concat(q.user_firstname, ' ', q.user_lastname, ' / ', q.user_code) as 'BVH'," +
                    " a.program_name as 'Program', " +
                    " j.product_type as 'Product', j.productsub_type as 'Sub Product', a.ccgroup_name as 'CC Group', " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'Created Date', " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By'," +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i:%s %p') as 'Submitted Date', " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as 'Updated Date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  as 'Updated By', " +
                    " case " +
                    " when(select group_concat(application_gid) from ocs_trn_tapplicationcomment where application_gid = a.application_gid) is not null then 'Business' " +
                    " when(select group_concat(application_gid) from ocs_trn_tapplicationcreditquery where queryraised_to = 'RM' and application_gid = a.application_gid) is not null then 'Credit' " +
                    " else '' " +
                    " end " +
                    " as 'Stage of Query', " +
                    " case  " +
                    " when(select group_concat(application_gid) from ocs_trn_tapplicationcomment where application_gid = a.application_gid) is not null then " +
                    " (select date_format(max(created_date), '%d-%m-%Y') from ocs_trn_tapplicationcomment where application_gid = a.application_gid " +
                    " ) " +
                    " when(select group_concat(application_gid) from ocs_trn_tapplicationcreditquery where application_gid = a.application_gid) is not null then " +
                    " (select date_format(max(created_date), '%d-%m-%Y') from ocs_trn_tapplicationcreditquery where queryraised_to = 'RM' and application_gid = a.application_gid) " +
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
                    " , '  ', (uu.country), '  ', (uu.latitude), '  ', (uu.longitude))SEPARATOR ' || ') end as 'Contact Address', " +
                    " case when a.renewal_flag = 'N' and a.enhancement_flag = 'N' then 'New' when a.renewal_flag = 'Y' then 'Renewal' " +
                    " when a.enhancement_flag = 'Y' then 'Enhancement' end as Type " +
                    " from ocs_mst_tapplication a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on a.relationshipmanager_gid = f.employee_gid " +
                    " left join adm_mst_tuser g on f.user_gid = g.user_gid " +
                    " left join hrm_mst_temployee h on a.drm_gid = h.employee_gid " +
                    " left join adm_mst_tuser i on i.user_gid = h.user_gid " +
                    " left join hrm_mst_temployee r on a.clustermanager_gid = r.employee_gid " +
                    " left join adm_mst_tuser k on k.user_gid = r.user_gid " +
                    " left join hrm_mst_temployee l on a.regionalhead_gid = l.employee_gid " +
                    " left join adm_mst_tuser m on m.user_gid = l.user_gid " +
                    " left join hrm_mst_temployee n on a.zonalhead_gid = n.employee_gid " +
                    " left join adm_mst_tuser o on o.user_gid = n.user_gid " +
                    " left join hrm_mst_temployee p on a.businesshead_gid = p.employee_gid " +
                    " left join adm_mst_tuser q on q.user_gid = p.user_gid " +
                    " left join hrm_mst_temployee sl on a.submitted_by = sl.employee_gid " +
                    " left join adm_mst_tuser qs on qs.user_gid = sl.user_gid " +
                    " left join ocs_mst_tapplication2loan j on j.application_gid = a.application_gid " +
                    " left join ocs_mst_tinstitution s on a.application_gid = s.application_gid and s.stakeholder_type = 'Applicant' " +
                    " left join ocs_mst_tcontact u on a.application_gid = u.application_gid and u.stakeholder_type = 'Applicant' " +
                    " left join ocs_mst_tinstitution2branch w on s.institution_gid = w.institution_gid " +
                    " left join ocs_trn_tapplicationcreditquery x on a.application_gid = x.application_gid and x.queryraised_to = 'RM' " +
                    " left join ocs_trn_tapplicationcreditquery y on a.application_gid = y.application_gid and y.queryraised_to = 'RM' and " +
                    " y.created_date = (select max(created_date)from ocs_trn_tapplicationcreditquery where queryraised_to = 'RM' and application_gid = a.application_gid) " +
                    " left join ocs_trn_tapplicationcomment z on a.application_gid = z.application_gid  and " +
                    " z.created_date = (select max(created_date)from ocs_trn_tapplicationcomment where application_gid = a.application_gid) " +
                    " left join ocs_trn_tapplicationapproval ls on a.application_gid = ls.application_gid and ls.hierary_level = '5' " +
                    " left join ocs_mst_tinstitution2mobileno ss on ss.institution_gid = s.institution_gid " +
                    " left join ocs_mst_tcontact2mobileno st on st.contact_gid = u.contact_gid " +
                    " left join ocs_mst_tinstitution2email su on su.institution_gid = s.institution_gid " +
                    " left join ocs_mst_tcontact2email sv on sv.contact_gid = u.contact_gid " +
                    " left join ocs_mst_tinstitution2address vv on vv.institution_gid = s.institution_gid " +
                    " left join ocs_mst_tcontact2address uu on uu.contact_gid = u.contact_gid " +
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
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Application Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Application Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Application Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 36])  //Address "A1:A9"
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

        //CC Summary
        public void DaGetMstCCSummary(MstApplicationReport objMstCCSummary)
        {
            msSQL = " select a.application_gid,date_format(a.submitted_date,'%d-%m-%Y %H:%i %p') as submitted_date, a.application_no, a.customer_name,a.region,a.vertical_name,a.overalllimit_amount," +
                    " date_format(b.ccmeeting_date, '%d-%m-%Y') as ccmeeting_date, " +
                    " b.ccgroup_name from ocs_mst_tapplication a" +
                    " left join ocs_mst_tccschedulemeeting b on a.application_gid = b.application_gid where ccsubmit_flag ='Y' group by b.application_gid ";

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

        public void DaGetMstCCReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " select " +
                    " a.application_no as 'Application Number',a.customer_name as 'Customer Name'," +
                    " a.region as 'Region', a.vertical_name as 'Vertical Name',a.overalllimit_amount as 'Overall Limit'," +
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
                    " from ocs_mst_tapplication a " +
                    " left join ocs_mst_tccschedulemeeting b on a.application_gid = b.application_gid " +
                    " left join ocs_mst_tccmeeting2members c on b.application_gid = c.application_gid " +
                    " left join ocs_mst_tccmeeting2members d on (b.application_gid = d.application_gid and d.ccapproval_flag = 'Y')" +
                    " left join hrm_mst_temployee p on d.approvalinitiate_by = p.employee_gid" +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid" +
                    " left join ocs_trn_tccapproval r on (c.ccmeeting2members_gid = r.ccmeeting2members_gid and c.application_gid = r.application_gid)" +
                    " where ccsubmit_flag = 'Y' " +
                    " group by b.application_gid " +
                    " Union " +
                    " select " +
                    " a.application_no as 'Application Number',a.customer_name as 'Customer Name'," +
                    " a.region as 'Region', a.vertical_name as 'Vertical Name',a.overalllimit_amount as 'Overall Limit'," +
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
                    " from ocs_mst_tapplication a " +
                    " left join ocs_mst_tccschedulemeetinglog b on a.application_gid = b.application_gid " +
                    " left join ocs_mst_tccmeeting2memberslog c on b.application_gid = c.application_gid " +
                    " left join ocs_mst_tccmeeting2memberslog d on (b.application_gid = d.application_gid and (d.approval_status = 'Approved' or d.approval_status = 'Rejected'))" +
                    " left join hrm_mst_temployee p on d.approvalinitiate_by = p.employee_gid" +
                    " left join adm_mst_tuser q on p.user_gid = q.user_gid" +
                    " left join ocs_trn_tccapproval r on (c.ccmeeting2members_gid = r.ccmeeting2members_gid and c.application_gid = r.application_gid)" +
                    " where ccsubmit_flag = 'Y' and a.application_gid in (select application_gid from ocs_mst_tccmeeting2memberslog ) " +
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
                objMstApplicationReport.lsname = "CC Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/CC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
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

        // PSLCSA Management Completed export excel

        public void DaGetMstPSLCSAManagement(MstApplicationReport objMstPSLCSAManagement)
        {
            msSQL = " (select  a.application_no as 'Application Number',a.customer_name as 'Customer Name', concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'RM', concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as 'Credit underwritten by',date_format(a.cccompleted_date, '%d-%m-%Y %h:%i') as 'CC Approved date'," +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i') as 'CAD Accepted date'," +
                    " i.stakeholder_type as 'Stakeholder Type',i.startupasofloansanction_date as 'Start up as of loan sanction date',i.occupation as Occupation,i.lineofactivity as 'Line of Activity'," +
                    " i.bsrcode as 'BSR code',i.pslcategory as 'PSL Category',i.weakersection as 'Weaker section',i.pslpurpose as 'PSL purpose'," +
                    " i.totalsanction_financialinstitution as 'Total sanction from financial institutions',i.natureofentity as 'Nature of Entity',i.indulgeinmarketing_activity as 'Indulge in Marketing Activity (for FPOs only)',i.plantandmachineryinvestment as 'Investment in Plan and Machinery'," +
                     " i.turnover as Turnover,i.msmeclassification as 'MSME Classification'," +
                     " date_format(i.loansanction_date, '%d-%m-%Y %h:%i') as 'Date of Loan Sanction'," +
                     " date_format(i.entityincorporation_date, '%d-%m-%Y %h:%i') as 'Date of Entity Incorporation'," +
                     " i.hq_metropolitancity as 'Whether entity HQ is in a metropolitan City',i.client_dtl as 'Client Details'," +
                    " REPLACE(REPLACE(a.pslcompleteremarks, '\r', ' '), '\n', ' ') as 'Complete Remarks'," +
                     " concat(k.user_firstname, ' ', k.user_lastname, ' / ', k.user_code) as 'Completed by'," +
                     " date_format(a.pslupdated_date, '%d-%m-%Y %h:%i') as 'Completed date' from ocs_trn_tcadapplication a " +
                     " left join ocs_trn_tcadinstitution i on i.application_gid = a.application_gid" +
                     " left join hrm_mst_temployee j on j.employee_gid = a.pslupdated_by" +
                     " left join adm_mst_tuser k on k.user_gid = j.user_gid" +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by" +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid" +
                     " left join hrm_mst_temployee g on g.employee_gid = a.created_by" +
                     " left join adm_mst_tuser h on h.user_gid = g.user_gid" +
                     " left join hrm_mst_temployee e on e.employee_gid = a.ccsubmitted_by" +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid" +
                     " where a.process_type = 'Accept' and a.pslcompleted_flag = 'Y' and i.pslcategory is not null" +
                     " group by a.application_gid order by a.processupdated_date desc)" +
                     " UNION " +
                     " (select  a.application_no as 'Application Number',a.customer_name as 'Customer Name', concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'RM', concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as 'Credit underwritten by',date_format(a.cccompleted_date, '%d-%m-%Y %h:%i') as 'CC Approved date'," +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i') as 'CAD Accepted date'," +
                    " i.stakeholder_type as 'Stakeholder Type',i.startupasofloansanction_date as 'Start up as of loan sanction date',i.occupation as Occupation,i.lineofactivity as 'Line of Activity'," +
                    " i.bsrcode as 'BSR code',i.pslcategory as 'PSL Category',i.weakersection as 'Weaker section',i.pslpurpose as 'PSL purpose'," +
                    " i.totalsanction_financialinstitution as 'Total sanction from financial institutions',i.natureofentity as 'Nature of Entity',i.indulgeinmarketing_activity as 'Indulge in Marketing Activity (for FPOs only)',i.plantandmachineryinvestment as 'Investment in Plan and Machinery'," +
                     " i.turnover as Turnover,i.msmeclassification as 'MSME Classification'," +
                     " date_format(i.loansanction_date, '%d-%m-%Y %h:%i') as 'Date of Loan Sanction'," +
                     " date_format(i.entityincorporation_date, '%d-%m-%Y %h:%i') as 'Date of Entity Incorporation'," +
                     " i.hq_metropolitancity as 'Whether entity HQ is in a metropolitan City',i.client_dtl as 'Client Details'," +
                    " REPLACE(REPLACE(a.pslcompleteremarks, '\r', ' '), '\n', ' ') as 'Complete Remarks'," +
                     " concat(k.user_firstname, ' ', k.user_lastname, ' / ', k.user_code) as 'Completed by'," +
                     " date_format(a.pslupdated_date, '%d-%m-%Y %h:%i') as 'Completed date'" +
                    " from ocs_trn_tcadapplication a" +
                    " left join ocs_trn_tcadcontact i on i.application_gid = a.application_gid" +
                    " left join hrm_mst_temployee j on j.employee_gid = a.pslupdated_by" +
                    " left join adm_mst_tuser k on k.user_gid = j.user_gid" +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid" +
                    " left join hrm_mst_temployee g on g.employee_gid = a.created_by" +
                    " left join adm_mst_tuser h on h.user_gid = g.user_gid" +
                    " left join hrm_mst_temployee e on e.employee_gid = a.ccsubmitted_by" +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid" +
                    " where a.process_type = 'Accept' and a.pslcompleted_flag = 'Y'  and i.pslcategory  is not null" +
                    " group by a.application_gid order by a.processupdated_date desc) " +
                    " UNION " +
                     " (select  a.application_no as 'Application Number',a.customer_name as 'Customer Name', concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'RM', concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as 'Credit underwritten by',date_format(a.cccompleted_date, '%d-%m-%Y %h:%i') as 'CC Approved date'," +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i') as 'CAD Accepted date'," +
                    " '',i.startupasofloansanction_date as 'Start up as of loan sanction date',i.occupation as Occupation,i.lineofactivity as 'Line of Activity'," +
                    " i.bsrcode as 'BSR code',i.pslcategory as 'PSL Category',i.weakersection as 'Weaker section',i.pslpurpose as 'PSL purpose'," +
                    " i.totalsanction_financialinstitution as 'Total sanction from financial institutions',i.natureofentity as 'Nature of Entity',i.indulgeinmarketing_activity as 'Indulge in Marketing Activity (for FPOs only)',i.plantandmachineryinvestment as 'Investment in Plan and Machinery'," +
                     " i.turnover as Turnover,i.msmeclassification as 'MSME Classification'," +
                     " date_format(i.loansanction_date, '%d-%m-%Y %h:%i') as 'Date of Loan Sanction'," +
                     " date_format(i.entityincorporation_date, '%d-%m-%Y %h:%i') as 'Date of Entity Incorporation'," +
                     " i.hq_metropolitancity as 'Whether entity HQ is in a metropolitan City',i.client_dtl as 'Client Details'," +
                    " REPLACE(REPLACE(a.pslcompleteremarks, '\r', ' '), '\n', ' ') as 'Complete Remarks'," +
                     " concat(k.user_firstname, ' ', k.user_lastname, ' / ', k.user_code) as 'Completed by'," +
                     " date_format(a.pslupdated_date, '%d-%m-%Y %h:%i') as 'Completed date'" +
                    " from ocs_trn_tcadapplication a" +
                    " left join ocs_trn_tcadgroup i on i.application_gid = a.application_gid" +
                    " left join hrm_mst_temployee j on j.employee_gid = a.pslupdated_by" +
                    " left join adm_mst_tuser k on k.user_gid = j.user_gid" +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid" +
                    " left join hrm_mst_temployee g on g.employee_gid = a.created_by" +
                    " left join adm_mst_tuser h on h.user_gid = g.user_gid" +
                    " left join hrm_mst_temployee e on e.employee_gid = a.ccsubmitted_by" +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid" +
                    " where a.process_type = 'Accept' and a.pslcompleted_flag = 'Y'  and i.pslcategory  is not null" +
                    " group by a.application_gid order by a.processupdated_date desc) ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("PSLCSA Management");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstPSLCSAManagement.lsname = "PSLCSA Management.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/PSLCSA Management/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstPSLCSAManagement.lscloudpath = lscompany_code + "/" + "Master/PSLCSA Management/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstPSLCSAManagement.lsname;
                objMstPSLCSAManagement.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/PSLCSA Management/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstPSLCSAManagement.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstPSLCSAManagement.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 27])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstPSLCSAManagement.lscloudpath, ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                objMstPSLCSAManagement.status = false;
                objMstPSLCSAManagement.message = "Failure";
            }
            objMstPSLCSAManagement.lscloudpath = objcmnstorage.EncryptData(objMstPSLCSAManagement.lscloudpath);
            objMstPSLCSAManagement.lspath = objcmnstorage.EncryptData(objMstPSLCSAManagement.lspath);
            objMstPSLCSAManagement.status = true;
            objMstPSLCSAManagement.message = "Success";
        }

        // PSLCSA Management Pending export excel 

        public void DaGetMstPSLCSAManagementPending(MstApplicationReport objMstPSLCSAManagementPending)
        {
            msSQL = " select a.application_no as 'Application Number',a.customer_name as 'Customer Name'," +
                    " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'RM Name'," +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as 'ccapproved Date'," +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as 'Credit underwritten by'," +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as 'cadaccepted Date', " +
                     " a.approval_status as 'Action Status' " +
                     " from ocs_trn_tcadapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee g on g.employee_gid = a.created_by " +
                     " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
                     " left join hrm_mst_temployee e on e.employee_gid = a.ccsubmitted_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " where a.process_type = 'Accept' and a.pslcompleted_flag = 'N' " +
                     " group by a.application_gid order by a.processupdated_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("PSLCSA Management Pending");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstPSLCSAManagementPending.lsname = "PSLCSA Management Pending.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/PSLCSA Management Pending/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstPSLCSAManagementPending.lscloudpath = lscompany_code + "/" + "Master/PSLCSA Management Pending/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstPSLCSAManagementPending.lsname;
                objMstPSLCSAManagementPending.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/PSLCSA Management Pending/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstPSLCSAManagementPending.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstPSLCSAManagementPending.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 7])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstPSLCSAManagementPending.lscloudpath, ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                objMstPSLCSAManagementPending.status = false;
                objMstPSLCSAManagementPending.message = "Failure";
            }
            objMstPSLCSAManagementPending.lscloudpath = objcmnstorage.EncryptData(objMstPSLCSAManagementPending.lscloudpath);
            objMstPSLCSAManagementPending.lspath = objcmnstorage.EncryptData(objMstPSLCSAManagementPending.lspath);
            objMstPSLCSAManagementPending.status = true;
            objMstPSLCSAManagementPending.message = "Success";
        }

        //Sanction MIS Report

        public void DaGetSanctionMISSummary(SanctionMISSummary values)
        {
            try
            {
                msSQL = " SELECT b.application_gid,b.application_no,b.customer_urn,b.customer_name,a.application2sanction_gid,a.sanction_refno," +
                       " date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, format((a.sanction_amount), 2, 'en_IN') as sanction_amount" +
                       " FROM ocs_trn_tapplication2sanction a" +
                       " LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid" +
                       " ORDER BY a.application2sanction_gid DESC";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSanctionMISdtl_list = new List<SanctionMISdtl>();
                if (dt_datatable.Rows.Count != 0)

                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getSanctionMISdtl_list.Add(new SanctionMISdtl
                        {
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application_no = dt["application_no"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            sanction_date = dt["sanction_date"].ToString(),
                            sanction_amount = dt["sanction_amount"].ToString(),
                        });
                    }
                    values.SanctionMISdtl_list = getSanctionMISdtl_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public void DaCADSanctionDtls(string application2sanction_gid, reportcadsanctiondetails values)
        {
            try
            {
                msSQL = " SELECT a.entity,a.application2sanction_gid,b.application_gid,b.application_no,a.sanction_refno,sanction_date," +
                "  format((a.sanction_amount), 2, 'en_IN') as sanction_amount,a.sanctionto_name, date_format(a.sanction_date,'%d-%m-%Y') as sanctionDate," +
                "  a.application_type,a.contactperson_address,a.contactperson_name,a.contactperson_number,a.contactpersonemail_address,date_format(a.sanctionfrom_date, '%d-%m-%Y') as sanctionfrom_date," +
                "  date_format(a.sanctiontill_date, '%d-%m-%Y') as sanctiontill_date,a.paycard,a.sanction_type,a.natureof_proposal,a.branch_name,a.esdeclaration_status," +
                 " a.makerfile_path,a.makerfile_name, checkerletter_flag, checkerapproval_flag FROM ocs_trn_tapplication2sanction a " +
                 " LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid " +
                 " WHERE application2sanction_gid ='" + application2sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                    values.application2sanction_gid = objODBCDataReader["application2sanction_gid"].ToString();
                    values.sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                    values.sanction_date = objODBCDataReader["sanctionDate"].ToString();
                    values.sanctionto_name = objODBCDataReader["sanctionto_name"].ToString();

                    if (objODBCDataReader["sanction_date"].ToString() != "")
                    {
                        values.sanctionDate = Convert.ToDateTime(objODBCDataReader["sanction_date"].ToString());
                    }
                    values.application_no = objODBCDataReader["application_no"].ToString();

                    values.application_type = objODBCDataReader["application_type"].ToString();
                    values.contactperson_address = objODBCDataReader["contactperson_address"].ToString();
                    values.contactperson_name = objODBCDataReader["contactperson_name"].ToString();
                    values.contactperson_number = objODBCDataReader["contactperson_number"].ToString();
                    values.contactpersonemail_address = objODBCDataReader["contactpersonemail_address"].ToString();
                    values.sanctionfrom_date = objODBCDataReader["sanctionfrom_date"].ToString();
                    values.sanctiontill_date = objODBCDataReader["sanctiontill_date"].ToString();
                    values.paycard = objODBCDataReader["paycard"].ToString();
                    values.sanction_type = objODBCDataReader["sanction_type"].ToString();
                    values.natureof_proposal = objODBCDataReader["natureof_proposal"].ToString();
                    values.branch_name = objODBCDataReader["branch_name"].ToString();
                    values.esdeclaration_status = objODBCDataReader["esdeclaration_status"].ToString();


                }
                objODBCDataReader.Close();

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public bool DaGetTemplateDetails(reportmdltemplate values, string application2sanction_gid)
        {
            msSQL = " select sanctionletter_status, template_name, template_content, makerfile_name, makerfile_path, sanctionletter_flag, checkerapproval_flag," +
                    " checkerletter_flag, checkerpushback_remarks, digitalsignature_flag, date_format(checkerupdated_on, '%d-%m-%Y') as checkerupdated_on," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as checkerupdated_by, date_format(makersubmitted_on, '%d-%m-%Y') as makersubmitted_on," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as makersubmitted_by, " +
                    " f.approver_name as approved_by,date_format(f.approver_approveddate, '%d-%m-%Y') as approved_date " +
                    " from ocs_trn_tapplication2sanction a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.checkerupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = a.makersubmitted_by " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join ocs_trn_tprocesstype_assign f on f.application_gid = a.application_gid " +
                    " where application2sanction_gid='" + application2sanction_gid + "' and f.menu_gid ='CADMGTSAN'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.sanctionletter_status = objODBCDataReader["sanctionletter_status"].ToString();
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_content = objODBCDataReader["template_content"].ToString();
                values.makerfile_name = objODBCDataReader["makerfile_name"].ToString();
                values.makerfile_path = objcmnstorage.EncryptData(objODBCDataReader["makerfile_path"].ToString());
                values.sanctionletter_flag = objODBCDataReader["sanctionletter_flag"].ToString();
                values.checkerapproval_flag = objODBCDataReader["checkerapproval_flag"].ToString();
                values.checkerletter_flag = objODBCDataReader["checkerletter_flag"].ToString();
                values.checkerpushback_remarks = objODBCDataReader["checkerpushback_remarks"].ToString();
                values.digitalsignature_flag = objODBCDataReader["digitalsignature_flag"].ToString();
                values.checkerupdated_by = objODBCDataReader["checkerupdated_by"].ToString();
                values.checkerupdated_on = objODBCDataReader["checkerupdated_on"].ToString();
                values.makersubmitted_by = objODBCDataReader["makersubmitted_by"].ToString();
                values.makersubmitted_on = objODBCDataReader["makersubmitted_on"].ToString();
                values.approved_by = objODBCDataReader["approved_by"].ToString();
                values.approved_date = objODBCDataReader["approved_date"].ToString();
            }
            objODBCDataReader.Close();

            values.status = true;
            return true;
        }

        public bool DaCADSanctionLetterSummary(string sanction_gid, mdlreportsanction values)
        {
            msSQL = " SELECT a.sanctionapprovallog_gid, a.sanction_gid, a.sanction_status, concat(c.user_firstname, c.user_lastname, ' / ', c.user_code) as created_by, " +
                   " date_format(a.created_date, '%d-%m-%Y %H:%i %p') as created_date, checkerpushback_remarks" +
                   " FROM ocs_trn_tsanctionapprovallog a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.created_by=b.employee_gid" +
                   " LEFT JOIN adm_mst_tuser c ON c.user_gid=b.user_gid where sanction_gid= '" + sanction_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getreportsanctiondetails_list = new List<reportsanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getreportsanctiondetails_list.Add(new reportsanctiondetails
                    {
                        sanctionapprovallog_gid = dt["sanctionapprovallog_gid"].ToString(),
                        application2sanction_gid = dt["sanction_gid"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        remarks = dt["checkerpushback_remarks"].ToString(),
                    });
                }
                values.reportsanctiondetails_list = getreportsanctiondetails_list;
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaGetesdocument(ReportUploadCADDocumentname values, string application2sanction_gid)
        {
            msSQL = " select esdeclaration_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ocs_trn_tuploadesdeclarationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( application2sanction_gid='" + application2sanction_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_esdeclarationdocumentlist = new List<ReportUploadCADES_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_esdeclarationdocumentlist.Add(new ReportUploadCADES_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["esdeclaration_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadCADES_DocumentList = get_esdeclarationdocumentlist;
            }
        }

        public void DaGetMaildocument(ReportUploadCADDocumentname values, string application2sanction_gid)
        {
            msSQL = " select maildocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ocs_trn_tdeviationmaildocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( application2sanction_gid='" + application2sanction_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_maildocumentlist = new List<ReportDeviationCADMail_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_maildocumentlist.Add(new ReportDeviationCADMail_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["maildocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.DeviationCADMail_DocumentList = get_maildocumentlist;
            }
        }

        public void DaExportSanctionMISReport(MstApplicationReport objMstApplicationReport)
        {
             msSQL = " SELECT b.application_no as `Application Number`, b.customer_urn as `CustomerURN`,b.customer_name as `CustomerName`, " +
" case when p.application_gid is not null then q.state when r.application_gid is not null then s.state end as 'Region',b.vertical_name as `Vertical`,  " +
" (select group_concat(distinct(aa.category_name) SEPARATOR ' | ')  from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Classification of MSME', " +
" (select group_concat(distinct(aa.ccmember_name) SEPARATOR ' | ') from ocs_mst_tccmeeting2members aa where aa.application_gid = a.application_gid and aa.ccmember_gid = 'SERM1811210510' and approval_status = 'Approved') as 'Executive Approval', " +
" date_format(DATE_ADD(`sanctiontill_date`, INTERVAL - 1 month), '%d-%m-%Y') AS 'Review Date (Expiry Date - 1 Month)', " +
" (select group_concat(distinct(aa.collateralSSV_value)SEPARATOR ' | ') from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Collateral FSV', " +
" (select group_concat(distinct(aa.forcedvalueassessed_on)SEPARATOR ' | ') from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Forced Value Assessed On', " +
" (select group_concat(distinct(aa.primary_security)SEPARATOR ' | ') from ocs_trn_tcadapplication2hypothecation aa where aa.application_gid = a.application_gid) as 'Primary Security', " +
" (select group_concat(distinct(aa.doc_charges)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Documentation Charge', " +
" (select group_concat(distinct(aa.fieldvisit_charges)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Client Visit Charges', " +
" (select group_concat(distinct(aa.processing_fee)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Processing  fees', " +
" b.constitution_name as `Constitution`," +
"a.sanction_refno as `SanctionRefNo`, date_format(a.sanction_date, '%d-%m-%Y') as `SanctionDate`,a.application_type as `Applicant Type`,  " +
" a.sanctionto_name as `Sanction To`, " +
"a.contactperson_name as `Contact Person Name`,a.contactperson_number as `Contact Person Number`,  " +
" a.contactpersonemail_address as `Email ID`, date_format(a.sanctionfrom_date, '%d-%m-%Y') as `Sanction From (Validity)`, date_format(a.sanctiontill_date, '%d-%m-%Y') as `Sanction Till(Validity)`," +
" a.paycard as `Paycard Solutions`, a.sanction_type as `Sanction Type`, a.branch_name as `Branch Name`, a.esdeclaration_status as `E & S Declaration Available`, format((a.sanction_amount), 2, 'en_IN') as `SanctionAmount`, " +
" concat(b.validityoveralllimit_year, ' - Year,', b.validityoveralllimit_month, ' - Month,', b.validityoveralllimit_days, ' - Day') as `Sanction Amount validity`, " +
" group_concat(distinct(concat(c.product_type, '/', c.productsub_type))SEPARATOR ' | ') as `Product / Subproduct`, group_concat(distinct(concat(c.product_type, '-', c.loanfacility_amount))SEPARATOR ' | ') as `Facility Loan Amount`, " +
" group_concat(distinct(concat(c.product_type, '-', c.loan_type))SEPARATOR ' | ') as `Loan Type`, group_concat(distinct(concat(c.product_type, '-', c.rate_interest))SEPARATOR ' | ') as `Rate of Interest`, group_concat(distinct(concat(c.product_type, '-', c.margin))SEPARATOR ' | ') as `Margin`," +
" group_concat(distinct(concat(c.product_type, '-', c.penal_interest))SEPARATOR ' | ') as `Penal Interest`, group_concat(distinct(c.scheme_type)SEPARATOR ' | ') as `Scheme Type`, " +
" group_concat(distinct(concat(c.product_type, ': ', concat(c.facilityvalidity_year, ' - Year,', facilityvalidity_month, ' - Month,', facilityvalidity_days, ' - Day')))SEPARATOR ' | ') as `Validity of the Facility`, " +
" group_concat(distinct(concat(c.product_type, ': ', concat(c.tenureproduct_year, ' - Year,', tenureproduct_month, ' - Month,', tenureproduct_days, ' - Day')))SEPARATOR ' | ') as `Tenure of the product`, " +
" group_concat(distinct(concat(c.product_type, '-', c.facility_type))SEPARATOR ' | ') as `Facility Type`, group_concat(distinct(concat(c.product_type, '-', c.facility_mode))SEPARATOR ' | ') as `Facility Mode`, " +
" group_concat(distinct(concat(c.product_type, '-', c.principalfrequency_name))SEPARATOR ' | ') as `Principal Frequency`, group_concat(distinct(concat(c.product_type, '-', c.interestfrequency_name))SEPARATOR ' | ') as `Interest Frequency`, " +
" group_concat(distinct(concat(c.product_type, '-', c.interest_status))SEPARATOR ' | ') as `Interest to be deducted upfront`, IFNULL(group_concat(distinct(concat(c.product_type, '-', c.moratorium_status))SEPARATOR ' | '), '') as `Moratorium Applicable`, " +
" IFNULL(group_concat(distinct(concat(c.product_type, '-', c.moratorium_type))SEPARATOR ' | '), '') as `Moratorium Type`, " +
" IFNULL(group_concat(distinct(concat(c.product_type, '-', date_format(c.moratorium_startdate, '%d-%m-%Y')))SEPARATOR ' | '), '') as `Moratorium Start Date`, IFNULL(group_concat(distinct(concat(c.product_type, '-', date_format(c.moratorium_enddate, '%d-%m-%Y')))SEPARATOR ' | '), '') as `Moratorium End Date`, " +
" group_concat(distinct(concat(c.product_type, '-', c.enduse_purpose))SEPARATOR ' | ') as `Purpose of Loan`, group_concat(distinct(concat(c.product_type, '-', c.program))SEPARATOR ' | ') as `Program`, " +
" IFNULL(group_concat(distinct (k.buyer_name)SEPARATOR ' | '), '') as `RM Buyer Details`, IFNULL(group_concat(distinct (l.buyer_name)SEPARATOR ' | '), '') as `Credit Buyer Details`, " +
" IFNULL(a.personal_guarantee, '') as `Personal Guarantee`, a.sanction_status as `Sanction Status`, case when b.cccompleted_date != '' then DATE_FORMAT(STR_TO_DATE(b.cccompleted_date, '%Y-%m-%d'), '%d-%m-%Y') else b.cccompleted_date end as `CC Approved Date`, " +
" j.ccgroup_name as `CC Committee Name`, IFNULL(i.maker_name, '') as `Maker Name`, IFNULL(date_format(i.maker_approveddate, '%d-%m-%Y'), '') as `Maker Submitted Date`, " +
" IFNULL(i.checker_name, '') as `Checker Name`, IFNULL(date_format(i.checker_approveddate, '%d-%m-%Y'), '') as `Checker Submitted Date`, " +
" IFNULL(i.approver_name, '') as `Approval Name`, IFNULL(date_format(i.approver_approveddate, '%d-%m-%Y'), '') as `Approved Date`, IFNULL(a.relationshipmgr_name, '') as `RelationshipManager`, " +
" IFNULL(x.employee_emailid, '') as `RM Email`, IFNULL(x.employee_mobileno, '') as `RM_PhoneNo`," +
" b.drm_name as 'DRM',   " +
" b.clustermanager_name as 'Cluster Head',  " +
" b.zonalhead_name as 'ZonalHead',  " +
" b.regionalhead_name as 'Region Head', " +
" b.businesshead_name as 'BusinessHead',  " +
" b.credithead_name as 'CH',  " +
" b.creditnationalmanager_name as 'NCM',  " +
" b.creditregionalmanager_name as 'RCM',  " +
" b.creditmanager_name as 'CreditManager',  " +
" p.businesscategory_name as 'Category - Business',  " +
" case when p.application_gid is not null then p.companypan_no when r.application_gid is not null then r.pan_no end as 'Applicant PAN Number',  " +
" k.buyer_name as 'Buyer Name/ID',  " +
" (select  group_concat(concat(aa.gst_no, ' / ', aa.gst_state)) from ocs_trn_tcadinstitution2branch aa " +
" LEFT JOIN ocs_trn_tcadinstitution p on(aa.institution_gid = p.institution_gid and p.stakeholder_type = 'Applicant') " +
" where p.application_gid = a.application_gid) as 'GSTNumber/State', " +
" c.product_type as 'Commodity Name',  " +
" b.overalllimit_amount as 'Overall Limit'," +
" date_format(z.approver_approveddate, '%d-%m-%Y %h:%i %p') as 'LSA Approved date',  " +
" (select group_concat(distinct(aa.limit_released) SEPARATOR ' | ') from ocs_trn_tlimitproductinfo aa where aa.application_gid = a.application_gid) as 'LSA Amount', " +
" (select group_concat(distinct(aa.roc_fillingid) SEPARATOR ' | ') from ocs_trn_tcadapplication2hypothecation aa where aa.application_gid = a.application_gid) as 'ROC Charging ID', " +
" v.ccmember_name as 'CC Approved member Name',  " +
" date_format(v.approved_date, '%d-%m-%Y %h:%i %p') as 'CC Approved member date',  " +
" date_format(b.processupdated_date, '%d-%m-%Y %h:%i %p') as 'CAD Allocated date',  " +
" REPLACE(REPLACE(a.contactperson_address, '\r', ' '), '\n', ' ') as `Address`" +
" FROM ocs_trn_tapplication2sanction a  " +
" LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid  " +
" LEFT JOIN  ocs_trn_tcadapplication2loan c on b.application_gid = c.application_gid  " +
" LEFT JOIN ocs_trn_tprocesstype_assign i on a.application_gid = i.application_gid  " +
" LEFT JOIN ocs_mst_tccschedulemeeting j on a.application_gid = j.application_gid  " +
" LEFT JOIN ocs_trn_tcadapplication2buyer k on c.application2loan_gid = k.application2loan_gid  " +
" LEFT JOIN ocs_trn_tcadcreditbuyer l on b.application_gid = l.application_gid  " +
" LEFT JOIN ocs_trn_tcadinstitution p on(b.application_gid = p.application_gid and p.stakeholder_type = 'Applicant')  " +
" LEFT JOIN ocs_trn_tcadinstitution2address q on(p.institution_gid = q.institution_gid and q.primary_status = 'Yes')  " +
" LEFT JOIN ocs_trn_tcadcontact r on(b.application_gid = r.application_gid and r.stakeholder_type = 'Applicant')  " +
" LEFT JOIN ocs_trn_tcadcontact2address s on(r.contact_gid = s.contact_gid and s.primary_status = 'Yes')  " +
" LEFT JOIN hrm_mst_temployee x on a.relationshipmgr_gid = x.employee_gid  " +
" left join ocs_trn_tcadinstitution2branch y on y.institution_gid = p.institution_gid    " +
" LEFT JOIN ocs_trn_tprocesstype_assign z on(a.application_gid = z.application_gid and z.menu_name = 'LSA')   " +
" LEFT JOIN ocs_mst_tccmeeting2members v on(a.application_gid = v.application_gid and v.approval_status = 'Approved')   " +
" left join hrm_mst_temployee w on w.employee_gid = b.processupdated_by    " +
" left join adm_mst_tuser t on t.user_gid = w.user_gid    " +
" group by c.application_gid, k.application2loan_gid, l.application_gid, y.institution_gid  " +
" ORDER BY a.application2sanction_gid DESC ";




            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Sanction MIS Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "SanctionMISReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Sanction MIS Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Sanction MIS Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Sanction MIS Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 87])  //Address "A1:BD1"

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

        //Buyer Report Export Excel --- STARTING
        public void DaExportBuyerReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " select a.buyer_code as 'Buyer Code',a.buyer_name as 'Buyer Name', date_format(a.coi_date, '%d-%m-%Y') as 'COI Date', date_format(a.businessstart_date, '%d-%m-%Y') as 'Business Start Date', " +
                    " a.year_business as 'Year in Business', a.month_business as 'Months in Business', a.constitution_name as 'Constitution Name', a.cin_no as 'CIN / Reg No', a.pan_no as 'PAN No',  " +
                    " concat(a.contactperson_fn, ' ', a.contactperson_ln) as 'Contact Person Name', group_concat(distinct(concat(k.mobile_no)) SEPARATOR ' || ') as `Contact Person Mobile Number`, a.credit_status as 'Credit Status', " +
                    " case when a.creditActive_status = 'NA' then 'Not Applicable' when a.creditActive_status = 'Y' then 'Active' else 'Inactive' end as 'Buyer Status', " +
                    " case when a.creditstatus_Approval = 'NA' then 'NA' when a.creditstatus_Approval = 'Y' then 'Approved' else 'Non Approved' end as 'Buyer Approval Status', concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as 'Created By', " +
                    " date_format(a.created_date, '%d-%m-%Y') as 'Created Date',a.cap_limit as 'Cap Limit', a.overall_limit as 'Overall Limit', a.buyer_limit as 'Buyer Limit', a.guarantor_limit as 'Guarantor Limit', " +
                    " a.borrower_limit as 'Borrower Limit',  " +
                    " group_concat(distinct(concat(b.gst_no)) SEPARATOR ' || ') as `GST Number`, group_concat(distinct(concat(c.email_address)) SEPARATOR ' || ') as `Contact Person Email Address`, " +
                    " group_concat(distinct(concat(d.addresstype_name, ': ', concat(d.addressline1, ', ', d.addressline2, ', ', d.landmark, ', ', d.postal_code, ', ', d.city, ', ', d.taluka, ', ', d.district, ', ', d.state_name, ', ', d.country)))SEPARATOR ' || ') as `Address`, " +
                    " (select group_concat(ifsc_code SEPARATOR ' || ')  from ocs_mst_tbuyer2bank where buyer_gid = a.buyer_gid) as   'IFSC Code', " +
                    " (select group_concat(bank_name SEPARATOR ' || ')  from ocs_mst_tbuyer2bank where buyer_gid = a.buyer_gid) as   'Bank Name', " +
                    " (select group_concat(bankaccount_number SEPARATOR ' || ')  from ocs_mst_tbuyer2bank where buyer_gid = a.buyer_gid) as   'Bank Account Number', " +
                    " (select group_concat(branch_name SEPARATOR ' || ')  from ocs_mst_tbuyer2bank where buyer_gid = a.buyer_gid) as   'Branch Name', " +
                    " (select group_concat(bankaccountlevel_name SEPARATOR ' || ')  from ocs_mst_tbuyer2bank where buyer_gid = a.buyer_gid) as   'Bank Account Level', " +
                    " (select group_concat(bankaccounttype_name SEPARATOR ' || ')  from ocs_mst_tbuyer2bank where buyer_gid = a.buyer_gid) as   'Bank Account Type', " +
                    " (select group_concat(micr_code SEPARATOR ' || ')  from ocs_mst_tbuyer2bank where buyer_gid = a.buyer_gid) as   'MICR', " +
                    " (select group_concat(bureauname_name SEPARATOR ' || ')  from ocs_mst_tbureauscoreadd where buyer_gid = a.buyer_gid) as   'Bureau Name', " +
                    " (select group_concat(bureau_score SEPARATOR ' || ')  from ocs_mst_tbureauscoreadd where buyer_gid = a.buyer_gid) as   'Bureau Score', " +
                    " (select group_concat((date_format(bureaugenerated_date, '%d-%m-%Y')) SEPARATOR ' || ')  from ocs_mst_tbureauscoreadd where buyer_gid = a.buyer_gid) as   'Bureau Generated On', " +
                    " (select group_concat(lastyear_turnover SEPARATOR ' || ')  from ocs_mst_tbureauscoreadd where buyer_gid = a.buyer_gid) as   'Last Year Turnover', " +
                    " (select group_concat(creditrating_name SEPARATOR ' || ')  from ocs_mst_tbureauscoreadd where buyer_gid = a.buyer_gid) as   'Credit Rating', " +
                    " (select group_concat((date_format(creditrating_date, '%d-%m-%Y')) SEPARATOR ' || ')  from ocs_mst_tbureauscoreadd where buyer_gid = a.buyer_gid) as   'Credit Rating as On', " +
                    " (select group_concat((date_format(creditratingexpiry_date, '%d-%m-%Y')) SEPARATOR ' || ')  from ocs_mst_tbureauscoreadd where buyer_gid = a.buyer_gid) as   'Credit Rating Expiry Date', " +
                    " concat(j.user_firstname, ' ', j.user_lastname, ' / ', j.user_code) as 'Credit Approved By', date_format(f.created_date, '%d-%m-%Y') as 'Credit Approved date' " +
                    " from ocs_mst_tbuyer a " +
                    " left join ocs_mst_tbuyer2gst b on a.buyer_gid = b.buyer_gid " +
                    " left join ocs_mst_tbuyer2emailaddress c on a.buyer_gid = c.buyer_gid " +
                    " left join ocs_mst_tbuyer2address d on a.buyer_gid = d.buyer_gid " +
                    " left join ocs_mst_tbureauscoreadd f on a.buyer_gid = f.buyer_gid " +
                    " LEFT JOIN hrm_mst_temployee g on a.created_by = g.employee_gid " +
                    " LEFT JOIN adm_mst_tuser h on g.user_gid = h.user_gid " +
                    " LEFT JOIN hrm_mst_temployee i on f.created_by = i.employee_gid " +
                    " LEFT JOIN adm_mst_tuser j on j.user_gid = i.user_gid " +
                    " left join ocs_mst_tbuyer2mobileno k on a.buyer_gid = k.buyer_gid " +
                    " group by a.buyer_gid, b.buyer_gid, c.buyer_gid, d.buyer_gid, f.buyer_gid, k.buyer_gid " +
                    " order by a.buyer_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Buyer Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "BuyerReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Buyer Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Buyer Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Buyer Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 40])  //Address "A1:BD1"

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
        //Buyer Report Export Excel --- ENDING

        //Buyer Report Summary Export Excel --- STARTING
        public void DaGetBuyerReportSummary(MstApplicationReport objMstAppSummary)
        {
            msSQL = " select a.buyer_gid , a.buyer_code, a.buyer_name, a.credit_status, concat(a.contactperson_fn, ' ', a.contactperson_ln) as contactperson_name, " +
                    " case when a.creditActive_status='NA' then 'NA' when a.creditActive_status='Y' then 'Active' else 'Inactive' end as creditActive_status," +
                    " case when a.creditstatus_Approval='NA' then 'NA' when a.creditstatus_Approval='Y' then 'Approved' else 'Non Approved' end as creditstatus_Approval, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by " +
                    " from ocs_mst_tbuyer a " +
                    " LEFT JOIN hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " LEFT JOIN adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " order by buyer_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyer_list = new List<BuyerReport_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyer_list.Add(new BuyerReport_list
                    {
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        buyer_code = (dr_datarow["buyer_code"].ToString()),
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        creditActive_status = (dr_datarow["creditActive_status"].ToString()),
                        contactperson_name = (dr_datarow["contactperson_name"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        creditstatus_Approval = (dr_datarow["creditstatus_Approval"].ToString())
                    });
                }
                objMstAppSummary.BuyerReport_list = getbuyer_list;
            }
            dt_datatable.Dispose();
        }
        //Buyer Report Summary Export Excel --- ENDING
        //Sanction MIS Report - Maker

        public void DaGetSanctionMISMakerSummary(SanctionMISSummary values)
        {
            try
            {
                msSQL =
" SELECT b.application_gid,b.application_no,b.customer_urn,b.customer_name,a.application2sanction_gid,a.sanction_refno, " +
" date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, format((a.sanction_amount), 2, 'en_IN') as sanction_amount " +
" FROM ocs_trn_tapplication2sanction a " +
" LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid " +
" LEFT JOIN ocs_trn_tprocesstype_assign i on a.application_gid = i.application_gid " +
" where processtype_name = 'Accept' and menu_name = 'Sanction' and (i.maker_approvalflag = 'Y' or i.maker_approvalflag = 'N') " +
" ORDER BY a.application2sanction_gid DESC ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSanctionMISdtl_list = new List<SanctionMISdtl>();
                if (dt_datatable.Rows.Count != 0)

                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getSanctionMISdtl_list.Add(new SanctionMISdtl
                        {
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application_no = dt["application_no"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            sanction_date = dt["sanction_date"].ToString(),
                            sanction_amount = dt["sanction_amount"].ToString(),
                        });
                    }
                    values.SanctionMISdtl_list = getSanctionMISdtl_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public void DaExportSanctionMISMakerReport(MstApplicationReport objMstApplicationReport)
        {
            //            msSQL = " SELECT b.application_no as `Application Number`, b.customer_urn as `CustomerURN`,b.customer_name as `CustomerName`, " +
            //" case when p.application_gid is not null then q.state when r.application_gid is not null then s.state end as 'Region',b.vertical_name as `Vertical`,  " +
            //" (select group_concat(distinct(aa.category_name) SEPARATOR ' | ') from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Classification of MSME', " +
            //" (select group_concat(distinct(aa.ccmember_name) SEPARATOR ' | ') from ocs_mst_tccmeeting2members aa where aa.application_gid = a.application_gid and aa.ccmember_gid = 'SERM1811210510' and approval_status = 'Approved') as 'Executive Approval', " +
            //" date_format(DATE_ADD(`sanctiontill_date`, INTERVAL - 1 month), '%d-%m-%Y ') AS 'Review Date (Expiry Date - 1 Month)', " +
            //" (select group_concat(distinct(aa.collateralSSV_value)SEPARATOR ' | ') from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Collateral FSV', " +
            //" (select group_concat(distinct(aa.forcedvalueassessed_on)SEPARATOR ' | ') from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Forced Value Assessed On', " +
            //" (select group_concat(distinct(aa.primary_security)SEPARATOR ' | ') from ocs_trn_tcadapplication2hypothecation aa where aa.application_gid = a.application_gid) as 'Primary Security', " +
            //" (select group_concat(distinct(aa.doc_charges)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Documentation Charge', " +
            //" (select group_concat(distinct(aa.fieldvisit_charges)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Client Visit Charges', " +
            //" (select group_concat(distinct(aa.processing_fee)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Processing  fees', " +
            //" b.constitution_name as `Constitution`," +
            //"a.sanction_refno as `SanctionRefNo`, date_format(a.sanction_date, '%d-%m-%Y') as `SanctionDate`,a.application_type as `Applicant Type`,  " +
            //" a.sanctionto_name as `Sanction To`, " +
            //"a.contactperson_name as `Contact Person Name`,a.contactperson_number as `Contact Person Number`,  " +
            //" b.constitution_name as `Constitution`,a.sanction_refno as `SanctionRefNo`, date_format(a.sanction_date, '%d-%m-%Y') as `SanctionDate`,a.application_type as `Applicant Type`,  " +
            //" a.sanctionto_name as `Sanction To`,a.contactperson_name as `Contact Person Name`,a.contactperson_number as `Contact Person Number`,  " +
            //" a.contactpersonemail_address as `Email ID`, date_format(a.sanctionfrom_date, '%d-%m-%Y') as `Sanction From (Validity)`, date_format(a.sanctiontill_date, '%d-%m-%Y') as `Sanction Till(Validity)`," +
            //" a.paycard as `Paycard Solutions`, a.sanction_type as `Sanction Type`, a.branch_name as `Branch Name`, a.esdeclaration_status as `E & S Declaration Available`, format((a.sanction_amount), 2, 'en_IN') as `SanctionAmount`, " +
            //" concat(b.validityoveralllimit_year, ' - Year,', b.validityoveralllimit_month, ' - Month,', b.validityoveralllimit_days, ' - Day') as `Sanction Amount validity`, " +
            //" group_concat(distinct(concat(c.product_type, '/', c.productsub_type))SEPARATOR ' | ') as `Product / Subproduct`, group_concat(distinct(concat(c.product_type, '-', c.loanfacility_amount))SEPARATOR ' | ') as `Facility Loan Amount`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.loan_type))SEPARATOR ' | ') as `Loan Type`, group_concat(distinct(concat(c.product_type, '-', c.rate_interest))SEPARATOR ' | ') as `Rate of Interest`,group_concat(distinct(concat(c.product_type, '-', c.margin))SEPARATOR ' | ') as `Margin`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.penal_interest))SEPARATOR ' | ') as `Penal Interest`, group_concat(distinct(c.scheme_type)SEPARATOR ' | ') as `Scheme Type`, " +
            //" group_concat(distinct(concat(c.product_type, ': ', concat(c.facilityvalidity_year, ' - Year,', facilityvalidity_month, ' - Month,', facilityvalidity_days, ' - Day')))SEPARATOR ' | ') as `Validity of the Facility`, " +
            //" group_concat(distinct(concat(c.product_type, ': ', concat(c.tenureproduct_year, ' - Year,', tenureproduct_month, ' - Month,', tenureproduct_days, ' - Day')))SEPARATOR ' | ') as `Tenure of the product`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.facility_type))SEPARATOR ' | ') as `Facility Type`, group_concat(distinct(concat(c.product_type, '-', c.facility_mode))SEPARATOR ' | ') as `Facility Mode`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.principalfrequency_name))SEPARATOR ' | ') as `Principal Frequency`, group_concat(distinct(concat(c.product_type, '-', c.interestfrequency_name))SEPARATOR ' | ') as `Interest Frequency`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.interest_status))) as `Interest to be deducted upfront`, IFNULL(group_concat(distinct(concat(c.product_type, '-', c.moratorium_status))SEPARATOR ' | '), '') as `Moratorium Applicable`, " +
            //" IFNULL(group_concat(distinct(concat(c.product_type, '-', c.moratorium_type))SEPARATOR ' | '), '') as `Moratorium Type`, " +
            //" IFNULL(group_concat(distinct(concat(c.product_type, '-', date_format(c.moratorium_startdate, '%d-%m-%Y')))SEPARATOR ' | '), '') as `Moratorium Start Date`, IFNULL(group_concat(distinct(concat(c.product_type, '-', date_format(c.moratorium_enddate, '%d-%m-%Y')))SEPARATOR ' | '), '') as `Moratorium End Date`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.enduse_purpose))SEPARATOR ' | ') as `Purpose of Loan`, group_concat(distinct(concat(c.product_type, '-', c.program))SEPARATOR ' | ') as `Program`, " +
            //" IFNULL(group_concat(distinct (k.buyer_name)SEPARATOR ' | '), '') as `RM Buyer Details`, IFNULL(group_concat(distinct (l.buyer_name)SEPARATOR ' | '), '') as `Credit Buyer Details`, " +
            //" IFNULL(a.personal_guarantee, '') as `Personal Guarantee`, a.sanction_status as `Sanction Status`, case when b.cccompleted_date != '' then DATE_FORMAT(STR_TO_DATE(b.cccompleted_date, '%Y-%m-%d'), '%d-%m-%Y') else b.cccompleted_date end as `CC Approved Date`, " +
            //" j.ccgroup_name as `CC Committee Name`, IFNULL(i.maker_name, '') as `Maker Name`, IFNULL(date_format(i.maker_approveddate, '%d-%m-%Y'), '') as `Maker Submitted Date`, " +
            //" IFNULL(a.relationshipmgr_name, '') as `RelationshipManager`, " +
            //" IFNULL(x.employee_emailid, '') as `RM Email`, IFNULL(x.employee_mobileno, '') as `RM_PhoneNo`," +
            //" b.drm_name as 'DRM',   " +
            //" b.clustermanager_name as 'Cluster Head',  " +
            //" b.zonalhead_name as 'ZonalHead',  " +
            //" b.regionalhead_name as 'Region Head', " +
            //" b.businesshead_name as 'BusinessHead',  " +
            //" b.credithead_name as 'CH',  " +
            //" b.creditnationalmanager_name as 'NCM',  " +
            //" b.creditregionalmanager_name as 'RCM',  " +
            //" b.creditmanager_name as 'CreditManager',  " +
            //" p.businesscategory_name as 'Category - Business',  " +
            //" case when p.application_gid is not null then p.companypan_no when r.application_gid is not null then r.pan_no end as 'Applicant PAN Number',  " +
            //" k.buyer_name as 'Buyer Name/ID',  " +
            //" (select  group_concat(concat(aa.gst_no, ' / ', aa.gst_state)) from ocs_trn_tcadinstitution2branch aa " +
            //" LEFT JOIN ocs_trn_tcadinstitution p on(aa.institution_gid = p.institution_gid and p.stakeholder_type = 'Applicant') " +
            //" where p.application_gid = a.application_gid) as 'GSTNumber/State', " +
            //" c.product_type as 'Commodity Name',  " +
            //" b.overalllimit_amount as 'Overall Limit'," +
            //" date_format(z.approver_approveddate, '%d-%m-%Y %h:%i %p') as 'LSA Approved date',  " +
            //" (select group_concat(distinct(aa.limit_released) SEPARATOR ' | ') from ocs_trn_tlimitproductinfo aa where aa.application_gid = a.application_gid) as 'LSA Amount', " +
            //" (select group_concat(distinct(aa.roc_fillingid) SEPARATOR ' | ') from ocs_trn_tcadapplication2hypothecation aa where aa.application_gid = a.application_gid) as 'ROC Charging ID', " +
            //" v.ccmember_name as 'CC Approved member Name',  " +
            //" date_format(v.approved_date, '%d-%m-%Y %h:%i %p') as 'CC Approved member date',  " +
            //" date_format(b.processupdated_date, '%d-%m-%Y %h:%i %p') as 'CAD Allocated date' , " +
            //" REPLACE(REPLACE(a.contactperson_address, '\r', ' '), '\n', ' ') as `Address`" +
            //" FROM ocs_trn_tapplication2sanction a  " +
            //" LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid  " +
            //" LEFT JOIN  ocs_trn_tcadapplication2loan c on b.application_gid = c.application_gid  " +
            //" LEFT JOIN ocs_trn_tprocesstype_assign i on a.application_gid = i.application_gid  " +
            //" LEFT JOIN ocs_mst_tccschedulemeeting j on a.application_gid = j.application_gid  " +
            //" LEFT JOIN ocs_trn_tcadapplication2buyer k on c.application2loan_gid = k.application2loan_gid  " +
            //" LEFT JOIN ocs_trn_tcadcreditbuyer l on b.application_gid = l.application_gid  " +
            //" LEFT JOIN ocs_trn_tcadinstitution p on(b.application_gid = p.application_gid and p.stakeholder_type = 'Applicant')  " +
            //" LEFT JOIN ocs_trn_tcadinstitution2address q on(p.institution_gid = q.institution_gid and q.primary_status = 'Yes')  " +
            //" LEFT JOIN ocs_trn_tcadcontact r on(b.application_gid = r.application_gid and r.stakeholder_type = 'Applicant')  " +
            //" LEFT JOIN ocs_trn_tcadcontact2address s on(r.contact_gid = s.contact_gid and s.primary_status = 'Yes')  " +
            //" LEFT JOIN hrm_mst_temployee x on a.relationshipmgr_gid = x.employee_gid  " +
            //" left join ocs_trn_tcadinstitution2branch y on y.institution_gid = p.institution_gid    " +
            //" LEFT JOIN ocs_trn_tprocesstype_assign z on(a.application_gid = z.application_gid and z.menu_name = 'LSA')   " +
            //" LEFT JOIN ocs_mst_tccmeeting2members v on(a.application_gid = v.application_gid and v.approval_status = 'Approved')   " +
            //" left join hrm_mst_temployee w on w.employee_gid = b.processupdated_by    " +
            //" left join adm_mst_tuser t on t.user_gid = w.user_gid    " +
            //" where i.processtype_name = 'Accept' and i.menu_name = 'Sanction' and (i.maker_approvalflag = 'Y' or i.maker_approvalflag = 'N') " +
            //" group by c.application_gid, k.application2loan_gid, l.application_gid, y.institution_gid  " +
            //" ORDER BY a.application2sanction_gid DESC ";
            msSQL = "call ocs_trn_tspsanctionmakerreportfirstquery ()";
            dt_datatable1 = objdbconn.GetDataTable(msSQL);
            msSQL = "call ocs_trn_tspsanctionmakerreportsecondquery ()";
            dt_datatable2 = objdbconn.GetDataTable(msSQL);
            
            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet1 = excel.Workbook.Worksheets.Add("Application Details");
            var workSheet2 = excel.Workbook.Worksheets.Add("Other Details");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "SanctionMISMakerReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Sanction MIS Maker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Sanction MIS Maker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Sanction MIS Master Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet1.Cells[1, 1].LoadFromDataTable(dt_datatable1, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet1.Cells[1, 1, 1, 61])  //Address "A1:BD1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                workSheet2.Cells[1, 1].LoadFromDataTable(dt_datatable2, true);
                using (var range = workSheet2.Cells[1, 1, 1, 35])  //Address "A1:BD1"

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


        //Sanction MIS Report - Checker

        public void DaGetSanctionMISCheckerSummary(SanctionMISSummary values)
        {
            try
            {
                msSQL = " SELECT b.application_gid,b.application_no,b.customer_urn,b.customer_name,a.application2sanction_gid,a.sanction_refno, " +
" date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, format((a.sanction_amount), 2, 'en_IN') as sanction_amount " +
" FROM ocs_trn_tapplication2sanction a " +
" LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid " +
" LEFT JOIN ocs_trn_tprocesstype_assign i on a.application_gid = i.application_gid " +
" where processtype_name = 'Accept' and menu_name = 'Sanction' and i.maker_approvalflag = 'Y' " +
" and (i.checker_approvalflag = 'Y' or i.checker_approvalflag = 'N') " +
" ORDER BY a.application2sanction_gid DESC ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSanctionMISdtl_list = new List<SanctionMISdtl>();
                if (dt_datatable.Rows.Count != 0)

                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getSanctionMISdtl_list.Add(new SanctionMISdtl
                        {
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application_no = dt["application_no"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            sanction_date = dt["sanction_date"].ToString(),
                            sanction_amount = dt["sanction_amount"].ToString(),
                        });
                    }
                    values.SanctionMISdtl_list = getSanctionMISdtl_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public void DaExportSanctionMISCheckerReport(MstApplicationReport objMstApplicationReport)
        {
            //            msSQL = " SELECT b.application_no as `Application Number`, b.customer_urn as `CustomerURN`,b.customer_name as `CustomerName`, " +
            //" case when p.application_gid is not null then q.state when r.application_gid is not null then s.state end as 'Region',b.vertical_name as `Vertical`,  " +
            //" (select group_concat(distinct(aa.category_name) SEPARATOR ' | ')  from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Classification of MSME', " +
            //" (select group_concat(distinct(aa.ccmember_name) SEPARATOR ' | ') from ocs_mst_tccmeeting2members aa where aa.application_gid = a.application_gid and aa.ccmember_gid = 'SERM1811210510' and approval_status = 'Approved') as 'Executive Approval', " +
            //" date_format(DATE_ADD(`sanctiontill_date`, INTERVAL - 1 month), '%d-%m-%Y ') AS 'Review Date (Expiry Date - 1 Month)', " +
            //" (select group_concat(distinct(aa.collateralSSV_value)SEPARATOR ' | ') from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Collateral FSV', " +
            //" (select group_concat(distinct(aa.forcedvalueassessed_on)SEPARATOR ' | ') from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Forced Value Assessed On', " +
            //" (select group_concat(distinct(aa.primary_security)SEPARATOR ' | ') from ocs_trn_tcadapplication2hypothecation aa where aa.application_gid = a.application_gid) as 'Primary Security', " +
            //" (select group_concat(distinct(aa.doc_charges)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Documentation Charge', " +
            //" (select group_concat(distinct(aa.fieldvisit_charges)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Client Visit Charges', " +
            //" (select group_concat(distinct(aa.processing_fee)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Processing  fees', " +
            //" b.constitution_name as `Constitution`," +
            //"a.sanction_refno as `SanctionRefNo`, date_format(a.sanction_date, '%d-%m-%Y') as `SanctionDate`,a.application_type as `Applicant Type`,  " +
            //" a.sanctionto_name as `Sanction To`, " +
            //"a.contactperson_name as `Contact Person Name`,a.contactperson_number as `Contact Person Number`,  " +
            //" b.constitution_name as `Constitution`,a.sanction_refno as `SanctionRefNo`, date_format(a.sanction_date, '%d-%m-%Y') as `SanctionDate`,a.application_type as `Applicant Type`,  " +
            //" a.sanctionto_name as `Sanction To`, a.contactperson_name as `Contact Person Name`,a.contactperson_number as `Contact Person Number`,  " +
            //" a.contactpersonemail_address as `Email ID`, date_format(a.sanctionfrom_date, '%d-%m-%Y') as `Sanction From (Validity)`, date_format(a.sanctiontill_date, '%d-%m-%Y') as `Sanction Till(Validity)`," +
            //" a.paycard as `Paycard Solutions`, a.sanction_type as `Sanction Type`, a.branch_name as `Branch Name`, a.esdeclaration_status as `E & S Declaration Available`, format((a.sanction_amount), 2, 'en_IN') as `SanctionAmount`, " +
            //" concat(b.validityoveralllimit_year, ' - Year,', b.validityoveralllimit_month, ' - Month,', b.validityoveralllimit_days, ' - Day') as `Sanction Amount validity`, " +
            //" group_concat(distinct(concat(c.product_type, '/', c.productsub_type))SEPARATOR ' | ') as `Product / Subproduct`, group_concat(distinct(concat(c.product_type, '-', c.loanfacility_amount))SEPARATOR ' | ') as `Facility Loan Amount`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.loan_type))SEPARATOR ' | ') as `Loan Type`, group_concat(distinct(concat(c.product_type, '-', c.rate_interest))SEPARATOR ' | ') as `Rate of Interest`,group_concat(distinct(concat(c.product_type, '-', c.margin))SEPARATOR ' | ') as `Margin`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.penal_interest))SEPARATOR ' | ') as `Penal Interest`, group_concat(distinct(c.scheme_type)SEPARATOR ' | ') as `Scheme Type`, " +
            //" group_concat(distinct(concat(c.product_type, ': ', concat(c.facilityvalidity_year, ' - Year,', facilityvalidity_month, ' - Month,', facilityvalidity_days, ' - Day')))SEPARATOR ' | ') as `Validity of the Facility`, " +
            //" group_concat(distinct(concat(c.product_type, ': ', concat(c.tenureproduct_year, ' - Year,', tenureproduct_month, ' - Month,', tenureproduct_days, ' - Day')))SEPARATOR ' | ') as `Tenure of the product`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.facility_type))SEPARATOR ' | ') as `Facility Type`, group_concat(distinct(concat(c.product_type, '-', c.facility_mode))SEPARATOR ' | ') as `Facility Mode`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.principalfrequency_name))SEPARATOR ' | ') as `Principal Frequency`, group_concat(distinct(concat(c.product_type, '-', c.interestfrequency_name))SEPARATOR ' | ') as `Interest Frequency`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.interest_status))SEPARATOR ' | ') as `Interest to be deducted upfront`, IFNULL(group_concat(distinct(concat(c.product_type, '-', c.moratorium_status))SEPARATOR ' | '), '') as `Moratorium Applicable`, " +
            //" IFNULL(group_concat(distinct(concat(c.product_type, '-', c.moratorium_type))SEPARATOR ' | '), '') as `Moratorium Type`, " +
            //" IFNULL(group_concat(distinct(concat(c.product_type, '-', date_format(c.moratorium_startdate, '%d-%m-%Y')))SEPARATOR ' | '), '') as `Moratorium Start Date`, IFNULL(group_concat(distinct(concat(c.product_type, '-', date_format(c.moratorium_enddate, '%d-%m-%Y')))SEPARATOR ' | '), '') as `Moratorium End Date`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.enduse_purpose))SEPARATOR ' | ') as `Purpose of Loan`, group_concat(distinct(concat(c.product_type, '-', c.program))SEPARATOR ' | ') as `Program`, " +
            //" IFNULL(group_concat(distinct (k.buyer_name)SEPARATOR ' | '), '') as `RM Buyer Details`, IFNULL(group_concat(distinct (l.buyer_name)SEPARATOR ' | '), '') as `Credit Buyer Details`, " +
            //" IFNULL(a.personal_guarantee, '') as `Personal Guarantee`, a.sanction_status as `Sanction Status`, case when b.cccompleted_date != '' then DATE_FORMAT(STR_TO_DATE(b.cccompleted_date, '%Y-%m-%d'), '%d-%m-%Y') else b.cccompleted_date end as `CC Approved Date`, " +
            //" j.ccgroup_name as `CC Committee Name`, IFNULL(i.maker_name, '') as `Maker Name`, IFNULL(date_format(i.maker_approveddate, '%d-%m-%Y'), '') as `Maker Submitted Date`, " +
            //" IFNULL(i.checker_name, '') as `Checker Name`, IFNULL(date_format(i.checker_approveddate, '%d-%m-%Y'), '') as `Checker Submitted Date`, " +
            //" IFNULL(a.relationshipmgr_name, '') as `RelationshipManager`, " +
            //" IFNULL(x.employee_emailid, '') as `RM Email`, IFNULL(x.employee_mobileno, '') as `RM_PhoneNo`," +
            //" b.drm_name as 'DRM',   " +
            //" b.clustermanager_name as 'Cluster Head',  " +
            //" b.zonalhead_name as 'ZonalHead',  " +
            //" b.regionalhead_name as 'Region Head', " +
            //" b.businesshead_name as 'BusinessHead',  " +
            //" b.credithead_name as 'CH',  " +
            //" b.creditnationalmanager_name as 'NCM',  " +
            //" b.creditregionalmanager_name as 'RCM',  " +
            //" b.creditmanager_name as 'CreditManager',  " +
            //" p.businesscategory_name as 'Category - Business',  " +
            //" case when p.application_gid is not null then p.companypan_no when r.application_gid is not null then r.pan_no end as 'Applicant PAN Number',  " +
            //" k.buyer_name as 'Buyer Name/ID',  " +
            //" (select  group_concat(concat(aa.gst_no, ' / ', aa.gst_state)) from ocs_trn_tcadinstitution2branch aa " +
            //" LEFT JOIN ocs_trn_tcadinstitution p on(aa.institution_gid = p.institution_gid and p.stakeholder_type = 'Applicant') " +
            //" where p.application_gid = a.application_gid) as 'GSTNumber/State', " +
            //" c.product_type as 'Commodity Name',  " +
            //" b.overalllimit_amount as 'Overall Limit'," +
            //" date_format(z.approver_approveddate, '%d-%m-%Y %h:%i %p') as 'LSA Approved date',  " +
            //" (select group_concat(distinct(aa.limit_released)SEPARATOR ' | ') from ocs_trn_tlimitproductinfo aa where aa.application_gid = a.application_gid) as 'LSA Amount', " +
            //" (select group_concat(distinct(aa.roc_fillingid)SEPARATOR ' | ') from ocs_trn_tcadapplication2hypothecation aa where aa.application_gid = a.application_gid) as 'ROC Charging ID', " +
            //" v.ccmember_name as 'CC Approved member Name',  " +
            //" date_format(v.approved_date, '%d-%m-%Y %h:%i %p') as 'CC Approved member date',  " +
            //" date_format(b.processupdated_date, '%d-%m-%Y %h:%i %p') as 'CAD Allocated date',  " +
            //" REPLACE(REPLACE(a.contactperson_address, '\r', ' '), '\n', ' ') as `Address`" +
            //" FROM ocs_trn_tapplication2sanction a  " +
            //" LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid  " +
            //" LEFT JOIN  ocs_trn_tcadapplication2loan c on b.application_gid = c.application_gid  " +
            //" LEFT JOIN ocs_trn_tprocesstype_assign i on a.application_gid = i.application_gid  " +
            //" LEFT JOIN ocs_mst_tccschedulemeeting j on a.application_gid = j.application_gid  " +
            //" LEFT JOIN ocs_trn_tcadapplication2buyer k on c.application2loan_gid = k.application2loan_gid  " +
            //" LEFT JOIN ocs_trn_tcadcreditbuyer l on b.application_gid = l.application_gid  " +
            //" LEFT JOIN ocs_trn_tcadinstitution p on(b.application_gid = p.application_gid and p.stakeholder_type = 'Applicant')  " +
            //" LEFT JOIN ocs_trn_tcadinstitution2address q on(p.institution_gid = q.institution_gid and q.primary_status = 'Yes')  " +
            //" LEFT JOIN ocs_trn_tcadcontact r on(b.application_gid = r.application_gid and r.stakeholder_type = 'Applicant')  " +
            //" LEFT JOIN ocs_trn_tcadcontact2address s on(r.contact_gid = s.contact_gid and s.primary_status = 'Yes')  " +
            //" LEFT JOIN hrm_mst_temployee x on a.relationshipmgr_gid = x.employee_gid  " +
            //" left join ocs_trn_tcadinstitution2branch y on y.institution_gid = p.institution_gid    " +
            //" LEFT JOIN ocs_trn_tprocesstype_assign z on(a.application_gid = z.application_gid and z.menu_name = 'LSA')   " +
            //" LEFT JOIN ocs_mst_tccmeeting2members v on(a.application_gid = v.application_gid and v.approval_status = 'Approved')   " +
            //" left join hrm_mst_temployee w on w.employee_gid = b.processupdated_by    " +
            //" left join adm_mst_tuser t on t.user_gid = w.user_gid    " +
            //" where i.processtype_name = 'Accept' and i.menu_name = 'Sanction' and i.maker_approvalflag = 'Y' " +
            //" and (i.checker_approvalflag = 'Y' or i.checker_approvalflag = 'N') " +
            //" group by c.application_gid, k.application2loan_gid, l.application_gid, y.institution_gid  " +
            //" ORDER BY a.application2sanction_gid DESC ";
            msSQL = "call ocs_trn_tspsanctioncheckerreportfirstquery ()";
            dt_datatable1 = objdbconn.GetDataTable(msSQL);
            msSQL = "call ocs_trn_tspsanctioncheckerreportsecondquery ()";
            dt_datatable2 = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet1 = excel.Workbook.Worksheets.Add("Application Details");
            var workSheet2 = excel.Workbook.Worksheets.Add("Other Details");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "SanctionMISCheckerReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Sanction MIS Checker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Sanction MIS Checker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Sanction MIS Checker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet1.Cells[1, 1].LoadFromDataTable(dt_datatable1, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet1.Cells[1, 1, 1, 61])  //Address "A1:BD1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                workSheet2.Cells[1, 1].LoadFromDataTable(dt_datatable2, true);
                using (var range = workSheet2.Cells[1, 1, 1, 35])  //Address "A1:BD1"

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

        //Sanction MIS Report - Approver

        public void DaGetSanctionMISApprovedSummary(SanctionMISSummary values)
        {
            try
            {
                msSQL = " SELECT b.application_gid,b.application_no,b.customer_urn,b.customer_name,a.application2sanction_gid,a.sanction_refno, " +
" date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, format((a.sanction_amount), 2, 'en_IN') as sanction_amount " +
" FROM ocs_trn_tapplication2sanction a " +
" LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid " +
" LEFT JOIN ocs_trn_tprocesstype_assign i on a.application_gid = i.application_gid" +
" where processtype_name = 'Accept' and menu_name = 'Sanction' and i.maker_approvalflag = 'Y'" +
" and i.checker_approvalflag = 'Y' and (i.approver_approvalflag = 'Y' or i.approver_approvalflag = 'N') " +
" ORDER BY a.application2sanction_gid DESC ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSanctionMISdtl_list = new List<SanctionMISdtl>();
                if (dt_datatable.Rows.Count != 0)

                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getSanctionMISdtl_list.Add(new SanctionMISdtl
                        {
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application_no = dt["application_no"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            sanction_date = dt["sanction_date"].ToString(),
                            sanction_amount = dt["sanction_amount"].ToString(),
                        });
                    }
                    values.SanctionMISdtl_list = getSanctionMISdtl_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public void DaExportSanctionMISApprovedReport(MstApplicationReport objMstApplicationReport)
        {
            //            msSQL = " SELECT b.application_no as `Application Number`, b.customer_urn as `CustomerURN`,b.customer_name as `CustomerName`, " +
            //" case when p.application_gid is not null then q.state when r.application_gid is not null then s.state end as 'Region',b.vertical_name as `Vertical`,  " +
            //" b.constitution_name as `Constitution`,a.sanction_refno as `SanctionRefNo`, date_format(a.sanction_date, '%d-%m-%Y') as `SanctionDate`,a.application_type as `Applicant Type`,  " +
            //" a.sanctionto_name as `Sanction To`, a.contactperson_address as `Address`,a.contactperson_name as `Contact Person Name`,a.contactperson_number as `Contact Person Number`,  " +
            //" a.contactpersonemail_address as `Email ID`, date_format(a.sanctionfrom_date, '%d-%m-%Y') as `Sanction From (Validity)`, date_format(a.sanctiontill_date, '%d-%m-%Y') as `Sanction Till(Validity)`," +
            //" a.paycard as `Paycard Solutions`, a.sanction_type as `Sanction Type`, a.branch_name as `Branch Name`, a.esdeclaration_status as `E & S Declaration Available`, format((a.sanction_amount), 2, 'en_IN') as `SanctionAmount`, " +
            //" concat(b.validityoveralllimit_year, ' - Year,', b.validityoveralllimit_month, ' - Month,', b.validityoveralllimit_days, ' - Day') as `Sanction Amount validity`, " +
            //" group_concat(distinct(concat(c.product_type, '/', c.productsub_type))) as `Product / Subproduct`, group_concat(distinct(concat(c.product_type, '-', c.loanfacility_amount))) as `Facility Loan Amount`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.loan_type))) as `Loan Type`, group_concat(distinct(concat(c.product_type, '-', c.rate_interest))) as `Rate of Interest`,group_concat(distinct(concat(c.product_type, '-', c.margin))) as `Margin`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.penal_interest))) as `Penal Interest`, group_concat(distinct(c.scheme_type)) as `Scheme Type`, " +
            //" group_concat(distinct(concat(c.product_type, ': ', concat(c.facilityvalidity_year, ' - Year,', facilityvalidity_month, ' - Month,', facilityvalidity_days, ' - Day')))) as `Validity of the Facility`, " +
            //" group_concat(distinct(concat(c.product_type, ': ', concat(c.tenureproduct_year, ' - Year,', tenureproduct_month, ' - Month,', tenureproduct_days, ' - Day')))) as `Tenure of the product`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.facility_type))) as `Facility Type`, group_concat(distinct(concat(c.product_type, '-', c.facility_mode))) as `Facility Mode`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.principalfrequency_name))) as `Principal Frequency`, group_concat(distinct(concat(c.product_type, '-', c.interestfrequency_name))) as `Interest Frequency`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.interest_status))) as `Interest to be deducted upfront`, IFNULL(group_concat(distinct(concat(c.product_type, '-', c.moratorium_status))), '') as `Moratorium Applicable`, " +
            //" IFNULL(group_concat(distinct(concat(c.product_type, '-', c.moratorium_type))), '') as `Moratorium Type`, " +
            //" IFNULL(group_concat(distinct(concat(c.product_type, '-', date_format(c.moratorium_startdate, '%d-%m-%Y')))), '') as `Moratorium Start Date`, IFNULL(group_concat(distinct(concat(c.product_type, '-', date_format(c.moratorium_enddate, '%d-%m-%Y')))), '') as `Moratorium End Date`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.enduse_purpose))) as `Purpose of Loan`, group_concat(distinct(concat(c.product_type, '-', c.program))) as `Program`, " +
            //" IFNULL(group_concat(distinct k.buyer_name), '') as `RM Buyer Details`, IFNULL(group_concat(distinct l.buyer_name), '') as `Credit Buyer Details`, " +
            //" IFNULL(a.personal_guarantee, '') as `Personal Guarantee`, a.sanction_status as `Sanction Status`, case when ccapproved_date != '' then DATE_FORMAT(STR_TO_DATE(ccapproved_date, '%Y-%m-%d'), '%d-%m-%Y') else ccapproved_date end as `CC Approved Date`, " +
            //" j.ccgroup_name as `CC Committee Name`, IFNULL(i.maker_name, '') as `Maker Name`, IFNULL(date_format(i.maker_approveddate, '%d-%m-%Y'), '') as `Maker Submitted Date`, " +
            //" IFNULL(i.checker_name, '') as `Checker Name`, IFNULL(date_format(i.checker_approveddate, '%d-%m-%Y'), '') as `Checker Submitted Date`, " +
            //" IFNULL(i.approver_name, '') as `Approval Name`, IFNULL(date_format(i.approver_approveddate, '%d-%m-%Y'), '') as `Approved Date`, IFNULL(a.relationshipmgr_name, '') as `RelationshipManager`, " +
            //" IFNULL(x.employee_emailid, '') as `RM Email`, IFNULL(x.employee_mobileno, '') as `RM_PhoneNo`," +
            //" b.drm_name as 'DRM',   " +
            //" b.clustermanager_name as 'Cluster Head',  " +
            //" b.zonalhead_name as 'ZonalHead',  " +
            //" b.regionalhead_name as 'Region Head', " +
            //" b.businesshead_name as 'BusinessHead',  " +
            //" b.credithead_name as 'CH',  " +
            //" b.creditnationalmanager_name as 'NCM',  " +
            //" b.creditregionalmanager_name as 'RCM',  " +
            //" b.creditmanager_name as 'CreditManager',  " +
            //" p.businesscategory_name as 'Category - Business',  " +
            //" case when p.application_gid is not null then p.companypan_no when r.application_gid is not null then r.pan_no end as 'Applicant PAN Number',  " +
            //" k.buyer_name as 'Buyer Name/ID',  " +
            //" group_concat(concat(y.gst_no,' / ',y.gst_state)) as 'GSTNumber/State',  " +
            //" c.product_type as 'Commodity Name',  " +
            //" date_format(z.approver_approveddate, '%d-%m-%Y %h:%i %p') as 'LSA Approved date',  " +
            //" v.ccmember_name as 'CC Approved member Name',  " +
            //" date_format(v.approved_date, '%d-%m-%Y %h:%i %p') as 'CC Approved member date',  " +
            //" date_format(b.processupdated_date, '%d-%m-%Y %h:%i %p') as 'CAD Allocated date'  " +
            //" FROM ocs_trn_tapplication2sanction a  " +
            //" LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid  " +
            //" LEFT JOIN  ocs_trn_tcadapplication2loan c on b.application_gid = c.application_gid  " +
            //" LEFT JOIN ocs_trn_tprocesstype_assign i on a.application_gid = i.application_gid  " +
            //" LEFT JOIN ocs_mst_tccschedulemeeting j on a.application_gid = j.application_gid  " +
            //" LEFT JOIN ocs_trn_tcadapplication2buyer k on c.application2loan_gid = k.application2loan_gid  " +
            //" LEFT JOIN ocs_trn_tcadcreditbuyer l on b.application_gid = l.application_gid  " +
            //" LEFT JOIN ocs_trn_tcadinstitution p on(b.application_gid = p.application_gid and p.stakeholder_type = 'Applicant')  " +
            //" LEFT JOIN ocs_trn_tcadinstitution2address q on(p.institution_gid = q.institution_gid and q.primary_status = 'Yes')  " +
            //" LEFT JOIN ocs_trn_tcadcontact r on(b.application_gid = r.application_gid and r.stakeholder_type = 'Applicant')  " +
            //" LEFT JOIN ocs_trn_tcadcontact2address s on(r.contact_gid = s.contact_gid and s.primary_status = 'Yes')  " +
            //" LEFT JOIN hrm_mst_temployee x on a.relationshipmgr_gid = x.employee_gid  " +
            //" left join ocs_trn_tcadinstitution2branch y on y.institution_gid = p.institution_gid    " +
            //" LEFT JOIN ocs_trn_tprocesstype_assign z on(a.application_gid = z.application_gid and z.menu_name = 'LSA')   " +
            //" LEFT JOIN ocs_mst_tccmeeting2members v on(a.application_gid = v.application_gid and v.approval_status = 'Approved')   " +
            //" left join hrm_mst_temployee w on w.employee_gid = b.processupdated_by    " +
            //" left join adm_mst_tuser t on t.user_gid = w.user_gid    " +
            //" where i.processtype_name = 'Accept' and i.menu_name = 'Sanction' and i.maker_approvalflag = 'Y' " +
            //" and i.checker_approvalflag = 'Y' and (i.approver_approvalflag = 'Y' or i.approver_approvalflag = 'N') " +
            //" group by c.application_gid, k.application2loan_gid, l.application_gid, y.institution_gid  " +
            //" ORDER BY a.application2sanction_gid DESC ";
            msSQL = "call ocs_trn_tspsanctionapprovalreportfirstquery ()";
            dt_datatable1 = objdbconn.GetDataTable(msSQL);
            msSQL = "call ocs_trn_tspsanctionapprovalreportsecondquery ()";
            dt_datatable2 = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet1 = excel.Workbook.Worksheets.Add("Application Details");
            var workSheet2 = excel.Workbook.Worksheets.Add("Other Details");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "SanctionMISApprovedReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Sanction MIS Approved Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Sanction MIS Approved Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Sanction MIS Approved Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet1.Cells[1, 1].LoadFromDataTable(dt_datatable1, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet1.Cells[1, 1, 1, 61])  //Address "A1:BD1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                workSheet2.Cells[1, 1].LoadFromDataTable(dt_datatable2, true);
                using (var range = workSheet2.Cells[1, 1, 1, 35])  //Address "A1:BD1"

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
        //Sanction MIS Report - Approved

        public void DaGetSanctionMISApproverSummary(SanctionMISSummary values)
        {
            try
            {
                msSQL = " SELECT b.application_gid,b.application_no,b.customer_urn,b.customer_name,a.application2sanction_gid,a.sanction_refno, " +
" date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, format((a.sanction_amount), 2, 'en_IN') as sanction_amount " +
" FROM ocs_trn_tapplication2sanction a " +
" LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid " +
" LEFT JOIN ocs_trn_tprocesstype_assign i on a.application_gid = i.application_gid" +
" where processtype_name = 'Accept' and menu_name = 'Sanction' and i.maker_approvalflag = 'Y'" +
" and i.checker_approvalflag = 'Y' and (i.approver_approvalflag = 'Y' or i.approver_approvalflag = 'N') " +
" ORDER BY a.application2sanction_gid DESC ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSanctionMISdtl_list = new List<SanctionMISdtl>();
                if (dt_datatable.Rows.Count != 0)

                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getSanctionMISdtl_list.Add(new SanctionMISdtl
                        {
                            application_gid = dt["application_gid"].ToString(),
                            application2sanction_gid = dt["application2sanction_gid"].ToString(),
                            application_no = dt["application_no"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            sanction_date = dt["sanction_date"].ToString(),
                            sanction_amount = dt["sanction_amount"].ToString(),
                        });
                    }
                    values.SanctionMISdtl_list = getSanctionMISdtl_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public void DaExportSanctionMISApproverReport(MstApplicationReport objMstApplicationReport)
        {
            //            msSQL = " SELECT b.application_no as `Application Number`, b.customer_urn as `CustomerURN`,b.customer_name as `CustomerName`, " +
            //" case when p.application_gid is not null then q.state when r.application_gid is not null then s.state end as 'Region',b.vertical_name as `Vertical`,  " +
            //" (select group_concat(distinct(aa.category_name) SEPARATOR ' | ') from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Classification of MSME', " +
            //" (select group_concat(distinct(aa.ccmember_name) SEPARATOR ' | ') from ocs_mst_tccmeeting2members aa where aa.application_gid = a.application_gid and aa.ccmember_gid = 'SERM1811210510' and approval_status = 'Approved') as 'Executive Approval', " +
            //" date_format(DATE_ADD(`sanctiontill_date`, INTERVAL - 1 month), '%d-%m-%Y') AS 'Review Date (Expiry Date - 1 Month)', " +
            //" (select group_concat(distinct(aa.collateralSSV_value)SEPARATOR ' | ') from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Collateral FSV', " +
            //" (select group_concat(distinct(aa.forcedvalueassessed_on)SEPARATOR ' | ') from ocs_trn_tcadapplication2loan aa where aa.application_gid = a.application_gid) as 'Forced Value Assessed On', " +
            //" (select group_concat(distinct(aa.primary_security)SEPARATOR ' | ') from ocs_trn_tcadapplication2hypothecation aa where aa.application_gid = a.application_gid) as 'Primary Security', " +
            //" (select group_concat(distinct(aa.doc_charges)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Documentation Charge', " +
            //" (select group_concat(distinct(aa.fieldvisit_charges)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Client Visit Charges', " +
            //" (select group_concat(distinct(aa.processing_fee)SEPARATOR ' | ') from ocs_trn_tlsafeescharge aa where aa.application_gid = a.application_gid) as 'Processing  fees', " +
            //" b.constitution_name as `Constitution`," +
            //"a.sanction_refno as `SanctionRefNo`, date_format(a.sanction_date, '%d-%m-%Y') as `SanctionDate`,a.application_type as `Applicant Type`,  " +
            //" a.sanctionto_name as `Sanction To`, " +
            //"a.contactperson_name as `Contact Person Name`,a.contactperson_number as `Contact Person Number`,  " +
            //" b.constitution_name as `Constitution`,a.sanction_refno as `SanctionRefNo`, date_format(a.sanction_date, '%d-%m-%Y') as `SanctionDate`,a.application_type as `Applicant Type`,  " +
            //" a.sanctionto_name as `Sanction To`,a.contactperson_name as `Contact Person Name`,a.contactperson_number as `Contact Person Number`,  " +
            //" a.contactpersonemail_address as `Email ID`, date_format(a.sanctionfrom_date, '%d-%m-%Y') as `Sanction From (Validity)`, date_format(a.sanctiontill_date, '%d-%m-%Y') as `Sanction Till(Validity)`," +
            //" a.paycard as `Paycard Solutions`, a.sanction_type as `Sanction Type`, a.branch_name as `Branch Name`, a.esdeclaration_status as `E & S Declaration Available`, format((a.sanction_amount), 2, 'en_IN') as `SanctionAmount`, " +
            //" concat(b.validityoveralllimit_year, ' - Year,', b.validityoveralllimit_month, ' - Month,', b.validityoveralllimit_days, ' - Day') as `Sanction Amount validity`, " +
            //" group_concat(distinct(concat(c.product_type, '/', c.productsub_type))SEPARATOR ' | ') as `Product / Subproduct`, group_concat(distinct(concat(c.product_type, '-', c.loanfacility_amount))SEPARATOR ' | ') as `Facility Loan Amount`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.loan_type))SEPARATOR ' | ') as `Loan Type`, group_concat(distinct(concat(c.product_type, '-', c.rate_interest))SEPARATOR ' | ') as `Rate of Interest`,group_concat(distinct(concat(c.product_type, '-', c.margin))SEPARATOR ' | ') as `Margin`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.penal_interest))SEPARATOR ' | ') as `Penal Interest`, group_concat(distinct(c.scheme_type)SEPARATOR ' | ') as `Scheme Type`, " +
            //" group_concat(distinct(concat(c.product_type, ': ', concat(c.facilityvalidity_year, ' - Year,', facilityvalidity_month, ' - Month,', facilityvalidity_days, ' - Day')))SEPARATOR ' | ') as `Validity of the Facility`, " +
            //" group_concat(distinct(concat(c.product_type, ': ', concat(c.tenureproduct_year, ' - Year,', tenureproduct_month, ' - Month,', tenureproduct_days, ' - Day')))SEPARATOR ' | ') as `Tenure of the product`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.facility_type))SEPARATOR ' | ') as `Facility Type`, group_concat(distinct(concat(c.product_type, '-', c.facility_mode))SEPARATOR ' | ') as `Facility Mode`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.principalfrequency_name))SEPARATOR ' | ') as `Principal Frequency`, group_concat(distinct(concat(c.product_type, '-', c.interestfrequency_name))SEPARATOR ' | ') as `Interest Frequency`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.interest_status))SEPARATOR ' | ') as `Interest to be deducted upfront`, IFNULL(group_concat(distinct(concat(c.product_type, '-', c.moratorium_status))SEPARATOR ' | '), '') as `Moratorium Applicable`, " +
            //" IFNULL(group_concat(distinct(concat(c.product_type, '-', c.moratorium_type))SEPARATOR ' | '), '') as `Moratorium Type`, " +
            //" IFNULL(group_concat(distinct(concat(c.product_type, '-', date_format(c.moratorium_startdate, '%d-%m-%Y')))SEPARATOR ' | '), '') as `Moratorium Start Date`, IFNULL(group_concat(distinct(concat(c.product_type, '-', date_format(c.moratorium_enddate, '%d-%m-%Y')))SEPARATOR ' | '), '') as `Moratorium End Date`, " +
            //" group_concat(distinct(concat(c.product_type, '-', c.enduse_purpose))SEPARATOR ' | ') as `Purpose of Loan`, group_concat(distinct(concat(c.product_type, '-', c.program))SEPARATOR ' | ') as `Program`, " +
            //" IFNULL(group_concat(distinct (k.buyer_name)SEPARATOR ' | '), '') as `RM Buyer Details`, IFNULL(group_concat(distinct (l.buyer_name)SEPARATOR ' | '), '') as `Credit Buyer Details`, " +
            //" IFNULL(a.personal_guarantee, '') as `Personal Guarantee`, a.sanction_status as `Sanction Status`, case when b.cccompleted_date != '' then DATE_FORMAT(STR_TO_DATE(b.cccompleted_date, '%Y-%m-%d'), '%d-%m-%Y') else b.cccompleted_date end as `CC Approved Date`, " +
            //" j.ccgroup_name as `CC Committee Name`, IFNULL(i.maker_name, '') as `Maker Name`, IFNULL(date_format(i.maker_approveddate, '%d-%m-%Y'), '') as `Maker Submitted Date`, " +
            //" IFNULL(i.checker_name, '') as `Checker Name`, IFNULL(date_format(i.checker_approveddate, '%d-%m-%Y'), '') as `Checker Submitted Date`, " +
            //" IFNULL(i.approver_name, '') as `Approval Name`, IFNULL(date_format(i.approver_approveddate, '%d-%m-%Y'), '') as `Approved Date`, IFNULL(a.relationshipmgr_name, '') as `RelationshipManager`, " +
            //" IFNULL(x.employee_emailid, '') as `RM Email`, IFNULL(x.employee_mobileno, '') as `RM_PhoneNo`," +
            //" b.drm_name as 'DRM',   " +
            //" b.clustermanager_name as 'Cluster Head',  " +
            //" b.zonalhead_name as 'ZonalHead',  " +
            //" b.regionalhead_name as 'Region Head', " +
            //" b.businesshead_name as 'BusinessHead',  " +
            //" b.credithead_name as 'CH',  " +
            //" b.creditnationalmanager_name as 'NCM',  " +
            //" b.creditregionalmanager_name as 'RCM',  " +
            //" b.creditmanager_name as 'CreditManager',  " +
            //" p.businesscategory_name as 'Category - Business',  " +
            //" case when p.application_gid is not null then p.companypan_no when r.application_gid is not null then r.pan_no end as 'Applicant PAN Number',  " +
            //" k.buyer_name as 'Buyer Name/ID',  " +
            //" (select  group_concat(concat(aa.gst_no, ' / ', aa.gst_state)) from ocs_trn_tcadinstitution2branch aa " +
            //" LEFT JOIN ocs_trn_tcadinstitution p on(aa.institution_gid = p.institution_gid and p.stakeholder_type = 'Applicant') " +
            //" where p.application_gid = a.application_gid) as 'GSTNumber/State', " +
            //" c.product_type as 'Commodity Name',  " +
            //" b.overalllimit_amount as 'Overall Limit'," +
            //" date_format(z.approver_approveddate, '%d-%m-%Y %h:%i %p') as 'LSA Approved date',  " +
            //" (select group_concat(distinct(aa.limit_released) SEPARATOR ' | ') from ocs_trn_tlimitproductinfo aa where aa.application_gid = a.application_gid) as 'LSA Amount', " +
            //" (select group_concat(distinct(aa.roc_fillingid) SEPARATOR ' | ') from ocs_trn_tcadapplication2hypothecation aa where aa.application_gid = a.application_gid) as 'ROC Charging ID', " +
            //" v.ccmember_name as 'CC Approved member Name',  " +
            //" date_format(v.approved_date, '%d-%m-%Y %h:%i %p') as 'CC Approved member date',  " +
            //" date_format(b.processupdated_date, '%d-%m-%Y %h:%i %p') as 'CAD Allocated date',  " +
            //" REPLACE(REPLACE(a.contactperson_address, '\r', ' '), '\n', ' ') as `Address`" +
            //" FROM ocs_trn_tapplication2sanction a  " +
            //" LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid  " +
            //" LEFT JOIN  ocs_trn_tcadapplication2loan c on b.application_gid = c.application_gid  " +
            //" LEFT JOIN ocs_trn_tprocesstype_assign i on a.application_gid = i.application_gid  " +
            //" LEFT JOIN ocs_mst_tccschedulemeeting j on a.application_gid = j.application_gid  " +
            //" LEFT JOIN ocs_trn_tcadapplication2buyer k on c.application2loan_gid = k.application2loan_gid  " +
            //" LEFT JOIN ocs_trn_tcadcreditbuyer l on b.application_gid = l.application_gid  " +
            //" LEFT JOIN ocs_trn_tcadinstitution p on(b.application_gid = p.application_gid and p.stakeholder_type = 'Applicant')  " +
            //" LEFT JOIN ocs_trn_tcadinstitution2address q on(p.institution_gid = q.institution_gid and q.primary_status = 'Yes')  " +
            //" LEFT JOIN ocs_trn_tcadcontact r on(b.application_gid = r.application_gid and r.stakeholder_type = 'Applicant')  " +
            //" LEFT JOIN ocs_trn_tcadcontact2address s on(r.contact_gid = s.contact_gid and s.primary_status = 'Yes')  " +
            //" LEFT JOIN hrm_mst_temployee x on a.relationshipmgr_gid = x.employee_gid  " +
            //" left join ocs_trn_tcadinstitution2branch y on y.institution_gid = p.institution_gid    " +
            //" LEFT JOIN ocs_trn_tprocesstype_assign z on(a.application_gid = z.application_gid and z.menu_name = 'LSA')   " +
            //" LEFT JOIN ocs_mst_tccmeeting2members v on(a.application_gid = v.application_gid and v.approval_status = 'Approved')   " +
            //" left join hrm_mst_temployee w on w.employee_gid = b.processupdated_by    " +
            //" left join adm_mst_tuser t on t.user_gid = w.user_gid    " +
            //" where i.processtype_name = 'Accept' and i.menu_name = 'Sanction' and i.maker_approvalflag = 'Y' " +
            //" and i.checker_approvalflag = 'Y' and (i.approver_approvalflag = 'Y' or i.approver_approvalflag = 'N') " +
            //" group by c.application_gid, k.application2loan_gid, l.application_gid, y.institution_gid  " +
            //" ORDER BY a.application2sanction_gid DESC ";
            msSQL = "call ocs_trn_tspsanctionapprovalreportfirstquery ()";
            dt_datatable1 = objdbconn.GetDataTable(msSQL);
            msSQL = "call ocs_trn_tspsanctionapprovalreportsecondquery ()";
            dt_datatable2 = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet1 = excel.Workbook.Worksheets.Add("Application Details");
            var workSheet2 = excel.Workbook.Worksheets.Add("Other Details");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "SanctionMISApproverReport.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Sanction MIS Approver Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Sanction MIS Approver Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Sanction MIS Approver Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet1.Cells[1, 1].LoadFromDataTable(dt_datatable1, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet1.Cells[1, 1, 1, 61])  //Address "A1:BD1"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                workSheet2.Cells[1, 1].LoadFromDataTable(dt_datatable2, true);
                using (var range = workSheet2.Cells[1, 1, 1, 35])  //Address "A1:BD1"

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
        public void DaExporthypothecationSummaryReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " select a.application2hypothecation_gid, a.security_type, a.security_description, a.security_value, " +
                        " date_format(a.securityassessed_date, '%d-%m-%Y') as securityassessed_date, a.asset_id, a.roc_fillingid, " +
                        " a.CERSAI_fillingid, a.hypoobservation_summary, a.primary_security, " +
                        " b.region, b.vertical_name, b.constitution_name, b.customer_urn, b.application_no, b.customerref_name, " +
                        " c.sanction_refno,date_format(c.sanction_date, '%d-%m-%Y') as sanction_date, c.application_type" +
                        " from ocs_trn_tcadapplication2hypothecation a " +
                        " left join ocs_mst_tapplication b on a.application_gid = b.application_gid" +
                        " left join ocs_trn_tapplication2sanction c on a.application_gid = c.application_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Hypothecation Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Hypothecation.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Hypothecation/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Hypothecation/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Hypothecation/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 18])  //Address "A1:BD1"

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
        public void DaGethypothecationSummary(SanctionMISSummary values)
        {
            try
            {
                msSQL = " select b.application_no, a.security_type, a.security_description, a.security_value, " +
                        " date_format(a.securityassessed_date, '%d-%m-%Y') as securityassessed_date, a.asset_id, a.roc_fillingid, " +
                        " a.CERSAI_fillingid, a.hypoobservation_summary, a.primary_security, " +
                        " b.region, b.vertical_name, b.constitution_name, b.customer_urn, b.application_no, b.customerref_name, " +
                        " c.sanction_refno,date_format(c.sanction_date, '%d-%m-%Y') as sanction_date, c.application_type" +
                        " from ocs_trn_tcadapplication2hypothecation a " +
                        " left join ocs_mst_tapplication b on a.application_gid = b.application_gid" +
                        " left join ocs_trn_tapplication2sanction c on a.application_gid = c.application_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethypothecation_list = new List<hypothecation>();
                if (dt_datatable.Rows.Count != 0)

                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        gethypothecation_list.Add(new hypothecation
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            customer_name = dt["customerref_name"].ToString(),
                            region = dt["region"].ToString(),
                            vertical = dt["vertical_name"].ToString(),
                            constitution = dt["constitution_name"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            sanction_date = dt["sanction_date"].ToString(),
                            //sanction_amount = dt["sanction_amount"].ToString(),
                            applicant_type = dt["application_type"].ToString(),
                            security_type = dt["security_type"].ToString(),
                            security_value = dt["security_value"].ToString(),
                            security_assessed_on = dt["securityassessed_date"].ToString(),
                            asset_id = dt["asset_id"].ToString(),
                            roc_filling_id = dt["roc_fillingid"].ToString(),
                            cersai_Filling_id = dt["CERSAI_fillingid"].ToString(),
                            security_description = dt["security_description"].ToString(),
                            observation_summary = dt["hypoobservation_summary"].ToString(),
                            primary_security = dt["primary_security"].ToString(),
                        });
                    }
                    values.hypothecation_list = gethypothecation_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public void DaExportDocCheckMakerPendingReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    "  date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date, " +
                    "  e.sanction_refno as Sanction_Reference_No ,d.maker_name as Maker_Name,d.checker_name as Checker_Name," +
                    "  d.approver_name as Approver_Name,f.company_name as Institution_Name," +
                    "  '' as Individual_Name," +
                    "  '' as Group_Name, " +
                    "  f.stakeholder_type as Stakeholder_Type," +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from ocs_trn_tcadinstitution f  " +
                    "  left join ocs_trn_tcadapplication  a on f.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    "  left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL' " +
                    "  left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join ocs_trn_tcaddocumentchecktls i on f.institution_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join ocs_trn_tcadcovanantdocumentcheckdtls j on f.institution_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    "  where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and  " +
                    "  a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL'  " +
                    "  and maker_approvalflag = 'N') " +
                    "  group by f.institution_gid )  union " +
                    "  (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date, " +
                    "  e.sanction_refno as Sanction_Reference_No , " +
                    "  d.maker_name as Maker_Name, " +
                    " d.checker_name as Checker_Name, " +
                    "  d.approver_name as Approver_Name, " +
                    "  '' as Institution_Name, " +
                    "  concat(g.first_name, ' ', g.last_name) as  Individual_Name, " +
                    "  '' as Group_Name, " +
                    "  g.stakeholder_type as Stakeholder_Type, " +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from ocs_trn_tcadcontact g  " +
                    "  left join ocs_trn_tcadapplication a on g.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    "  left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL' " +
                    "  left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join ocs_trn_tcaddocumentchecktls i on g.contact_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join ocs_trn_tcadcovanantdocumentcheckdtls j on g.contact_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    "  where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and  " +
                    "  a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL'   " +
                    "  and maker_approvalflag = 'N') " +
                    "  group by g.contact_gid )  union " +
                    "  (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date, " +
                    " e.sanction_refno as Sanction_Reference_No , " +
                    " d.maker_name as Maker_Name, " +
                    " d.checker_name as Checker_Name, " +
                    " d.approver_name as Approver_Name, " +
                    " '' as Institution_Name, " +
                    " '' as Individual_Name, " +
                    " h.group_name as Group_Name, " +
                    "  '' as Stakeholder_Type, " +
                    " GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist, " +
                    " GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    " from ocs_trn_tcadgroup h  " +
                    "  left join ocs_trn_tcadapplication  a on h.application_gid = a.application_gid  " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    " left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL' " +
                    " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid  " +
                    "  left join ocs_trn_tcaddocumentchecktls i on h.group_gid = i.credit_gid  and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    " left join ocs_trn_tcadcovanantdocumentcheckdtls j on h.group_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    " where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and  " +
                    " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL'  " +
                    " and maker_approvalflag = 'N')" +
                    " group by h.group_gid )  ";
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
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Document Checklist Maker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Document Checklist Maker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Document Checklist Maker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 14])  //Address "A1:A9"
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

        public void DaExportDocCheckCheckerPendingReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    "  date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date, " +
                    "  e.sanction_refno as Sanction_Reference_No ,d.maker_name as Maker_Name,d.checker_name as Checker_Name," +
                    "  d.approver_name as Approver_Name,f.company_name as Institution_Name," +
                    "  '' as Individual_Name," +
                    "  '' as Group_Name, " +
                    "  f.stakeholder_type as Stakeholder_Type," +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from ocs_trn_tcadinstitution  f  " +
                    "  left join ocs_trn_tcadapplication a on f.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                     " left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL' " +
                    "  left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join ocs_trn_tcaddocumentchecktls i on f.institution_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join ocs_trn_tcadcovanantdocumentcheckdtls j on f.institution_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    " left join ocs_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                     " where a.process_type = 'Accept' and k.maker_flag='Y' and k.checker_flag='N' and " +
                    " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                    " and checker_approvalflag = 'N')" +
                    "  group by f.institution_gid)   union " +
                    "  (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date, " +
                    "  e.sanction_refno as Sanction_Reference_No , " +
                    "  d.maker_name as Maker_Name, " +
                    " d.checker_name as Checker_Name, " +
                    "  d.approver_name as Approver_Name, " +
                    "  '' as Institution_Name, " +
                    "  concat(g.first_name, ' ', g.last_name) as  Individual_Name, " +
                    "  '' as Group_Name, " +
                    "  g.stakeholder_type as Stakeholder_Type, " +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from ocs_trn_tcadcontact g  " +
                     "  left join ocs_trn_tcadapplication  a on g.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                      " left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL' " +
                    "  left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join ocs_trn_tcaddocumentchecktls i on g.contact_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join ocs_trn_tcadcovanantdocumentcheckdtls j on g.contact_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    " left join ocs_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                    " where a.process_type = 'Accept' and k.maker_flag='Y' and k.checker_flag='N' and " +
                    " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                    " and checker_approvalflag = 'N')" +
                    "  group by g.contact_gid  ) union " +
                    "  (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date, " +
                    " e.sanction_refno as Sanction_Reference_No , " +
                    " d.maker_name as Maker_Name, " +
                    " d.checker_name as Checker_Name, " +
                    " d.approver_name as Approver_Name, " +
                    " '' as Institution_Name, " +
                    " '' as Individual_Name, " +
                    " h.group_name as Group_Name, " +
                    "  '' as Stakeholder_Type, " +
                    " GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist, " +
                    " GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    " from ocs_trn_tcadgroup  h  " +
                    "  left join ocs_trn_tcadapplication  a on h.application_gid = a.application_gid  " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                     " left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL' " +
                    " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid  " +
                    "  left join ocs_trn_tcaddocumentchecktls i on h.group_gid = i.credit_gid  and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    " left join ocs_trn_tcadcovanantdocumentcheckdtls j on h.group_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                    " left join ocs_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                     " where a.process_type = 'Accept' and k.maker_flag='Y' and k.checker_flag='N' and " +
                    " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                    " and checker_approvalflag = 'N')" +
                    " group by h.group_gid  ) ";
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
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Document Checklist Checker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Document Checklist Checker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Document Checklist Checker Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 14])  //Address "A1:A9"
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

        public void DaExportDocCheckApprovalPendingReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    "  date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date, " +
                    "  e.sanction_refno as Sanction_Reference_No ,d.maker_name as Maker_Name,d.checker_name as Checker_Name," +
                    "  d.approver_name as Approver_Name,f.company_name as Institution_Name," +
                    "  '' as Individual_Name," +
                    "  '' as Group_Name, " +
                    "  f.stakeholder_type as Stakeholder_Type," +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from ocs_trn_tcadinstitution f  " +
                     "  left join ocs_trn_tcadapplication  a on f.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                     " left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL' " +
                    "  left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join ocs_trn_tcaddocumentchecktls i on f.institution_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join ocs_trn_tcadcovanantdocumentcheckdtls j on f.institution_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                      " left join ocs_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                    " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' and checker_flag='Y' " +
                     " and approver_approvalflag = 'N')" +
                    "  group by f.institution_gid )  union " +
                    "  (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date, " +
                    "  e.sanction_refno as Sanction_Reference_No , " +
                    "  d.maker_name as Maker_Name, " +
                    " d.checker_name as Checker_Name, " +
                    "  d.approver_name as Approver_Name, " +
                    "  '' as Institution_Name, " +
                    "  concat(g.first_name, ' ', g.last_name) as  Individual_Name, " +
                    "  '' as Group_Name, " +
                    "  g.stakeholder_type as Stakeholder_Type, " +
                    "  GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist, " +
                    "  GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    "  from ocs_trn_tcadcontact g  " +
                    "  left join ocs_trn_tcadapplication  a on g.application_gid = a.application_gid  " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                      " left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL' " +
                    "  left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid   " +
                    "  left join ocs_trn_tcaddocumentchecktls i on g.contact_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N')" +
                    "  left join ocs_trn_tcadcovanantdocumentcheckdtls j on g.contact_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                      " left join ocs_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                     " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' and checker_flag='Y' " +
                     " and approver_approvalflag = 'N')" +
                    "  group by g.contact_gid )  union " +
                    "  (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN, " +
                    " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date, " +
                    " e.sanction_refno as Sanction_Reference_No , " +
                    " d.maker_name as Maker_Name, " +
                    " d.checker_name as Checker_Name, " +
                    " d.approver_name as Approver_Name, " +
                    " '' as Institution_Name, " +
                    " '' as Individual_Name, " +
                    " h.group_name as Group_Name, " +
                    "  '' as Stakeholder_Type, " +
                    " GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist, " +
                    " GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist " +
                    " from ocs_trn_tcadgroup  h  " +
                     "  left join ocs_trn_tcadapplication a on h.application_gid = a.application_gid  " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                     " left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL' " +
                    " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid  " +
                    "  left join ocs_trn_tcaddocumentchecktls i on h.group_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N') " +
                    " left join ocs_trn_tcadcovanantdocumentcheckdtls j on h.group_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N')" +
                      " left join ocs_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                     " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' and checker_flag='Y' " +
                     " and approver_approvalflag = 'N')" +
                    " group by h.group_gid )  ";
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
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Document Checklist Approval Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Document Checklist Approval Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Document Checklist Approval Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 14])  //Address "A1:A9"
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

        public void DaGetCADDocChecklistReportCheckerSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,f.sanction_refno, " +
                    " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                    " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                    " a.creditgroup_gid, e.cadgroup_name,a.customer_urn from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                    " left join ocs_trn_tapplication2sanction f on f.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.maker_flag='Y' and d.checker_flag='N' and " +
                    " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                    " and checker_approvalflag = 'N')" +
                    " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_trn_tcadapplication where application_gid='" + dt["application_gid"] + "'";
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
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
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
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
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
                    getapplicationadd_list.Add(new cadapplicationlist
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
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            
            dt_datatable.Dispose();          
        }

        public void DaGetCADDocChecklistReportApprovalSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL =  " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,h.sanction_refno, " +
                     " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as checkerapproved_by, " +
                     " date_format(d.checkerapproved_on, '%d-%m-%Y %h:%i %p') as checkerapproved_on,a.customer_urn," +
                     " a.creditgroup_gid, docchecklist_approvalflag, d.approval_status as approval, g.cadgroup_name from ocs_trn_tcadapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " left join ocs_trn_tprocesstype_assign g on g.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction h on h.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' and checker_flag='Y' " +
                     " and approver_approvalflag = 'N')" +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
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
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
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
                    getapplicationadd_list.Add(new cadapplicationlist
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
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            
            dt_datatable.Dispose();
        }

        public void DaGetCADDocChecklistReportSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,e.sanction_refno, " +
                     " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " a.creditgroup_gid, d.cadgroup_name,a.customer_urn from ocs_trn_tcadapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                     "  and maker_approvalflag = 'N')" +
                     " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsccgroup_name;
                    string lsccadmin_name;
                    string lscadgroup_name;

                    msSQL = "select group_concat(ccgroup_name) as ccgroup_name from ocs_mst_tapplication where application_gid='" + dt["application_gid"] + "'";
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
                    msSQL = "select group_concat(ccadmin_name) as ccadmin_name from ocs_mst_tccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
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
                    //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from ocs_trn_tprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
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
                    getapplicationadd_list.Add(new cadapplicationlist
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
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            
            dt_datatable.Dispose();
        }
        public class ListtoDataTable
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        }

        public void DaGetDocChecklistPendingCount(string user_gid, string employee_gid, DocumentCount values)
        {
            msSQL = " select count(distinct (a.application_gid))  as cadsanction_count from ocs_trn_tcadapplication a " +
                   " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and a.docchecklist_makerflag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                     "  and maker_approvalflag = 'N')" +
                     "  order by a.updated_date desc ";
            values.cadmaker_count = objdbconn.GetExecuteScalar(msSQL);

           

            msSQL = " select count(distinct (a.application_gid))  as cadchecker_count from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                    " left join ocs_trn_tprocesstype_assign e on e.application_gid = a.application_gid " +
                    " left join ocs_trn_tapplication2sanction f on f.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and d.maker_flag='Y' and d.checker_flag='N' and " +
                    " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                    " and checker_approvalflag = 'N')" +
                    "  order by a.updated_date desc ";
            values.cadchecker_count = objdbconn.GetExecuteScalar(msSQL);

           

            msSQL = " select count(distinct (a.application_gid))  as cadcheckerapproval_count from ocs_trn_tcadapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " left join ocs_trn_tprocesstype_assign g on g.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction h on h.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and docchecklist_checkerflag='Y'and docchecklist_approvalflag='N' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' and checker_flag='Y' " +
                     " and approver_approvalflag = 'N')" +
                     "  order by a.updated_date desc ";
            values.cadcheckerapproval_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(distinct (a.application_gid))  as cadcheckerapproval_count from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                    " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                    " left join ocs_trn_tprocesstype_assign g on g.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' and docchecklist_approvalflag='Y' and " +
                    " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL'  " +
                    " and approver_approvalflag = 'Y')" +
                    "  order by a.updated_date desc ";
            values.cadapproved_count = objdbconn.GetExecuteScalar(msSQL);

        }

        // Application TAT Report export excel

        public void DaExportMstTatAppReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = "call ocs_rpt_sptatreport";
            //msSQL = " select a.application_no as 'Application_No',a.vertical_name as 'Vertical Name',a.program_name as 'Program', " +
            //        " a.relationshipmanager_name as 'RM Name',date_format(a.submitted_date, '%d-%m-%Y %h:%i:%s %p') as 'Application Submitted Date', " +
            //        " date_format(r.approved_date, '%d-%m-%Y %h:%i:%s %p') as 'Business Approved Date', " +
            //        " date_format(a.creditassigned_date, '%d-%m-%Y %h:%i:%s %p') as 'Credit Allocation Date', " +
            //        " date_format(t.rejected_date, '%d-%m-%Y %h:%i:%s %p') as 'Credit Manager Rejected Date', " +
            //        " date_format(t.approved_date, '%d-%m-%Y %h:%i:%s %p') as 'Credit Manager submission Date',   " +
            //        " date_format(s.approved_date, '%d-%m-%Y %h:%i:%s %p') as 'Credit Approved Date',   " +
            //        " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i:%s %p') as 'CC Approved Date',   " +
            //        " date_format(a.scheduled_date, '%d-%m-%Y %h:%i:%s %p') as ' CC Meeting Scheduled Date',  " +
            //        " GROUP_CONCAT(distinct(date_format(z.processupdated_date, '%d-%m-%Y %h:%i:%s %p'))SEPARATOR ' || ') as 'CAD Accepted Date', " +
            //        " GROUP_CONCAT(distinct(date_format(g.created_date, '%d-%m-%Y %h:%i:%s %p'))SEPARATOR ' || ') as 'CC Sentback To Credit Date', " +
            //        " GROUP_CONCAT(distinct(date_format(e.created_date, '%d-%m-%Y %h:%i:%s %p'))SEPARATOR ' || ') as 'CAD Sentback To CC Date', " +
            //        " GROUP_CONCAT(distinct(date_format(f.created_date, '%d-%m-%Y %h:%i:%s %p'))SEPARATOR ' || ') as 'CAD Sentback To Credit Date', " +
            //        " GROUP_CONCAT(distinct(date_format(k.created_date, '%d-%m-%Y %h:%i:%s %p'))SEPARATOR ' || ') as 'CAD Send back To Credit Without CC', " +
            //        " GROUP_CONCAT(distinct Concat((concat(p.user_firstname, ' ', p.user_lastname, ' / ', p.user_code)), ' - ', date_format(h.created_date, '%d-%m-%Y %h:%i:%s %p')) SEPARATOR ' || ') as 'Business approval Query Raised Date', " +
            //        " GROUP_CONCAT(distinct Concat((concat(q.user_firstname, ' ', q.user_lastname, ' / ', q.user_code)), ' - ', date_format(h.closed_date, '%d-%m-%Y %h:%i:%s %p')) SEPARATOR ' || ') as 'Business approval Query Close Date', " +
            //        " GROUP_CONCAT(distinct Concat((concat(l.user_firstname, ' ', l.user_lastname, ' / ', l.user_code)), ' - ', date_format(i.created_date, '%d-%m-%Y %h:%i:%s %p')) SEPARATOR ' || ') as 'Credit approval Query To RM Raised Date', " +
            //        " GROUP_CONCAT(distinct Concat((concat(m.user_firstname, ' ', m.user_lastname, ' / ', m.user_code)), ' - ', date_format(i.closed_date, '%d-%m-%Y %h:%i:%s %p')) SEPARATOR ' || ') as 'Credit approval Query To RM Close Date', " +
            //        " GROUP_CONCAT(distinct Concat((concat(n.user_firstname, ' ', n.user_lastname, ' / ', n.user_code)), ' - ', date_format(j.created_date, '%d-%m-%Y %h:%i:%s %p')) SEPARATOR ' || ') as 'Credit approval Query To Manager Raised Date', " +
            //        " GROUP_CONCAT(distinct Concat((concat(o.user_firstname, ' ', o.user_lastname, ' / ', o.user_code)), ' - ', date_format(j.closed_date, '%d-%m-%Y %h:%i:%s %p')) SEPARATOR ' || ') as 'Credit approval Query To Manager Close Date' " +
            //        " from ocs_mst_tapplication a " +
            //        " left join ocs_trn_tapplicationapproval b on b.application_gid = a.application_gid " +
            //        " left join ocs_trn_tappcreditapproval c on c.application_gid = a.application_gid " +
            //        " left join ocs_mst_tccmeeting2members d on d.application_gid = a.application_gid " +
            //        " left join ocs_trn_tcadtoccmeetinglog e on e.application_gid = a.application_gid " +
            //        " left join ocs_trn_tcadtocreditlog f on f.application_gid = a.application_gid " +
            //        " left join ocs_trn_tccmeetingtocreditlog g on g.application_gid = a.application_gid " +
            //        " left join ocs_trn_tapplicationcomment h on h.application_gid = a.application_gid " +
            //        " left join adm_mst_tuser p on p.user_gid = h.created_by " +
            //        " left join adm_mst_tuser q on q.user_gid = h.closed_by " +
            //        " left join ocs_trn_tapplicationcreditquery i on a.application_gid = i.application_gid and i.queryraised_to = 'RM' " +
            //        " left join adm_mst_tuser l on l.user_gid = i.created_by " +
            //        " left join adm_mst_tuser m on m.user_gid = i.closed_by " +
            //        " left join ocs_trn_tapplicationcreditquery j on a.application_gid = j.application_gid and j.queryraised_to = 'Credit' " +
            //        " left join adm_mst_tuser n on n.user_gid = j.created_by " +
            //        " left join adm_mst_tuser o on o.user_gid = j.closed_by " +
            //        " left join ocs_trn_tccmeetingskip k on k.application_gid = a.application_gid " +
            //        " left join ocs_trn_tapplicationapproval r on a.application_gid = r.application_gid and r.hierary_level = '5' " +
            //        " left join ocs_trn_tappcreditapproval s on a.application_gid = s.application_gid and s.hierary_level = '3' " +
            //        " left join ocs_trn_tappcreditapproval t on a.application_gid = t.application_gid and t.hierary_level = '0' " +
            //        " left join ocs_mst_tapplication z on a.application_gid = z.application_gid and z.process_type = 'Accept' " +
            //        " group by a.application_gid "; 

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Application Tat Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Application Tat Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Application Tat Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Application Tat Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Application Tat Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 23])  //Address "A1:A9"
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

        //Program Master Export Excel --- STARTING
        public void DaExportExcelAddprogram(ExportExcelAddprogram objMstApplicationReport)
        {
            msSQL = " select a.api_code,a.program_refno as 'Program Ref No', a.entity_name as 'Entity Name', group_concat(distinct b.vertical_name SEPARATOR ' || ') as Vertical, a.program as Program, " +
                " group_concat(distinct c.employee_name SEPARATOR ' || ') as 'Approved by', date_format(a.approved_date, '%d-%m-%Y') as 'Approved Date', a.program_limit as 'Overall Program Limit (in Rs)', a.maximum_limit as 'Maximum Limit per Transaction (in Rs)'," +
                " a.lms_code as 'LMS Code', a.bureau_code as 'Bureau Code', a.program_description as Description, " +
                " (select group_concat(loanproduct_name SEPARATOR ' || ') from ocs_mst_tprogram2loanproduct where program_gid = a.program_gid) as 'Product'," +
                " (select group_concat(loansubproduct_name SEPARATOR ' || ') from ocs_mst_tprogram2loanproduct where program_gid = a.program_gid) as 'Sub Product / Loan Program', " +
                " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as 'Created by', date_format(a.created_date, '%d-%m-%Y') as 'Created Date'," +
                 " concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as 'Updated by', date_format(a.updated_date, '%d-%m-%Y') as 'Updated Date'," +
                " case when a.status = 'Y' then 'Active' else 'Inactive' End as Status " +
                " from ocs_mst_tprogram a " +
                " left join ocs_mst_tprogram2vertical b on a.program_gid = b.program_gid " +
                " left join ocs_mst_tprogram2approval c on a.program_gid = c.program_gid " +
                " left join hrm_mst_temployee f on f.employee_gid = a.created_by " +
                " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                " left join hrm_mst_temployee h on h.employee_gid = a.updated_by" +
                " left join adm_mst_tuser g on g.user_gid = h.user_gid" +
                " group by a.program_gid order by a.program_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Program Master");

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Program Master Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ProgramMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/ProgramMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ProgramMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range1 = workSheet.Cells[1, 1, 1, 19])  //Address "A1:BD1"

                {
                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);

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
        //Program Master Export Excel --- ENDING

        //CAD Accepted Customers Export Excel --- STARTING
        public void DaExportexcelCADAcceptedCus(ExportExcelAddprogram objMstApplicationReport)
        {
            msSQL = " select a.application_no as 'Application No', a.customer_urn as 'URN Number', a.customer_name as 'Customer Name', a.relationshipmanager_name as 'RM Name', format(a.overalllimit_amount, 2) as 'Overall Limit Amount', a.vertical_name as Vertical, a.program_name as Program," + // concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as 'RM Name',
                " case when a.renewal_flag='Y' then 'Renewal' when a.enhancement_flag='Y' then 'Enhancement' when (a.enhancement_flag='N' and a.renewal_flag='N') then 'New' else '-' end as 'Application Type', " +
                " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as 'CC Approved Date', " +
                " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'CAD Accepted by', " +
                " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as 'CAD Accepted Date'," +
                " a.validityoveralllimit_year as 'Validity of overall limit (years)', a.validityoveralllimit_month as 'Validity of overall limit (month)', a.validityoveralllimit_days as 'Validity of overall limit (days)', a.calculationoveralllimit_validity as 'Calculation of overall limit validity', " +
                " a.csa_applicability as 'CSA Applicability', a.csaactivity_name as 'CSA Activity', a.percentageoftotal_limit as 'Percentage of the total limit', date_format(e.facilityrequested_date, '%d-%m-%Y %h:%i %p') as 'Facility Requested On', e.product_type as 'Product Type', " +
                " e.productsub_type as 'Product Sub Type', format(e.loanfacility_amount, 2) as 'Facility Loan Amount', e.loan_type as 'Loan Type' " +
                " from ocs_trn_tcadapplication a " +
                " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by  " +
                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                //" left join ocs_mst_tapplication h on h.application_gid = a.application_gid " +
                //" left join hrm_mst_temployee f on f.employee_gid = a.created_by " +
                //" left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                " left join ocs_trn_tcadapplication2loan e on e.application_gid = a.application_gid " +
                " where a.process_type = 'Accept' group by a.application_gid order by a.application_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("CAD Accepted Customers");

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "CAD Accepted Customers.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADAcceptedCustomers/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/CADAcceptedCustomers/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CADAcceptedCustomers/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range1 = workSheet.Cells[1, 1, 1, 23])  //Address "A1:BD1"

                {
                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);

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
        //CAD Accepted Customers Export Excel --- ENDING

        //Product master Export Excel

        public void DaGetMstProductReport(MstApplicationReport objMstApplicationReport)
        {

            msSQL = " select a.product_code as 'Product Code',a.product_name as 'Product Name',a.businessunit_name as 'sector',  " +
                        " a.valuechain_name as 'category',a.category_name as 'CSA Category',   " +
                        " (select group_concat(variety_name SEPARATOR ' | ')  from ocs_mst_tvariety where product_gid = a.product_gid) as 'Variety / Commodity',    " +
                        " (select if(group_concat(botanical_name) is null,null,group_concat(botanical_name SEPARATOR ' | '))  from ocs_mst_tvariety where product_gid = a.product_gid) as 'Botanical Name',   " +
                        " (select if(group_concat(alternative_name) is null,null,group_concat(alternative_name SEPARATOR ' | '))  from ocs_mst_tvariety where product_gid = a.product_gid) as 'Alternate Name',    " +
                        " (select if(group_concat(hsn_code) is null,null,group_concat(hsn_code SEPARATOR ' | '))  from ocs_mst_tvariety where product_gid = a.product_gid) as 'HSN Code',    " +
                        " (select if(group_concat(typeofsupplynature_name) is null,null,group_concat(typeofsupplynature_name SEPARATOR ' | '))  from ocs_mst_tvariety where product_gid = a.product_gid) as 'Type of Supply/Nature', " +
                        " (select if(group_concat(sectorclassification_name) is null,null,group_concat(sectorclassification_name SEPARATOR ' | '))  from ocs_mst_tvariety where product_gid = a.product_gid) as 'Sector Classification',   " +
                        " (select if(group_concat(varietysector_name) is null,null,group_concat(varietysector_name SEPARATOR ' | '))  from ocs_mst_tvariety where product_gid = a.product_gid) as 'Sector(Variety / Commodity)',   " +
                        " (select if(group_concat(varietycategory_name) is null,null,group_concat(varietycategory_name SEPARATOR ' | '))  from ocs_mst_tvariety where product_gid = a.product_gid) as  'Industry / Category',   " +
                        " (select if(group_concat(headingdesc_product) is null,null,group_concat(headingdesc_product SEPARATOR ' | '))  from ocs_mst_tvariety where product_gid = a.product_gid) as 'Heading Description/Product',  " +
                        " (select group_concat(distinct (select group_concat(IGST_percent separator ' , ') from agr_mst_tcommoditygststatus r where r.variety_gid = q.variety_gid) SEPARATOR ' | ')  from agr_mst_tcommoditygststatus q where q.product_gid = a.product_gid) as 'IGST %',   " +
                        " (select group_concat(distinct (select group_concat(SGST_percent SEPARATOR ' , ')  from agr_mst_tcommoditygststatus r where r.variety_gid = q.variety_gid) SEPARATOR ' | ')  from agr_mst_tcommoditygststatus q where q.product_gid = a.product_gid) as 'SGST %',    " +
                        " (select group_concat(distinct (select group_concat(CGST_percent SEPARATOR ' , ')  from agr_mst_tcommoditygststatus r where r.variety_gid = q.variety_gid) SEPARATOR ' | ')  from agr_mst_tcommoditygststatus q where q.product_gid = a.product_gid) as 'CGST %',    " +
                        " (select group_concat(distinct (select group_concat(CESS_percent SEPARATOR ' , ')  from agr_mst_tcommoditygststatus r where r.variety_gid = q.variety_gid) SEPARATOR ' | ')  from agr_mst_tcommoditygststatus q where q.product_gid = a.product_gid) as 'CESS %',    " +
                        " (select group_concat(distinct (select group_concat((date_format(wef_date, '%d-%m-%Y')) SEPARATOR ' , ')  from agr_mst_tcommoditygststatus r where r.variety_gid = q.variety_gid) SEPARATOR ' | ')  from agr_mst_tcommoditygststatus q where q.product_gid = a.product_gid)as 'w.e.f Date',   " +
                        " (select group_concat(distinct (select group_concat(product_name SEPARATOR ' , ')  from agr_mst_tcommoditytradeproductdtl t where t.variety_gid = s.variety_gid) SEPARATOR ' | ')  from agr_mst_tcommoditytradeproductdtl s where s.mstproduct_gid = a.product_gid) as 'Trade Product',   " +
                        " (select group_concat(distinct (select group_concat(subproduct_name SEPARATOR ' , ')  from agr_mst_tcommoditytradeproductdtl t where t.variety_gid = s.variety_gid) SEPARATOR ' | ')  from agr_mst_tcommoditytradeproductdtl s where s.mstproduct_gid = a.product_gid) as 'Trade Sub Product',   " +
                        " (select group_concat(distinct (select group_concat(insurancecompany_name SEPARATOR ' , ')  from agr_mst_tcommoditytradeproductdtl t where t.variety_gid = s.variety_gid) SEPARATOR ' | ')  from agr_mst_tcommoditytradeproductdtl s where  s.mstproduct_gid = a.product_gid) as 'Trade Insurance Company', " +
                        " (select group_concat(distinct (select group_concat(insurancepolicy_name SEPARATOR ' , ')  from agr_mst_tcommoditytradeproductdtl t where t.variety_gid = s.variety_gid) SEPARATOR ' | ')  from agr_mst_tcommoditytradeproductdtl s where s.mstproduct_gid = a.product_gid) as 'Trade Insurance Policy'    " +
                        " from ocs_mst_tproducts a    " +
                        " left join ocs_mst_tvariety b on a.product_gid = b.product_gid    " +
                        " left join agr_mst_tcommoditygststatus c on c.variety_gid = b.variety_gid     " +
                        " left join agr_mst_tcommoditytradeproductdtl d on d.variety_gid = b.variety_gid   " +
                        " group by a.product_gid  order by a.product_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Product Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Product Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Product Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Product Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Product Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 23])  //Address "A1:A9"
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

        //TAT CAD Accepted Customers Export Excel --- STARTING
        public void DaTATExportexcelCADAcceptedCus(ExportExcelAddprogram objMstApplicationReport)
        {
            msSQL = " select a.application_no as 'Application Number', a.customer_name as 'Application Name', a.vertical_name as 'Vertical', date_format(a.created_date,'%d-%m-%Y %h:%i %p') as 'Created On'," +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p') as 'Submitted On' , a.relationshipmanager_name as 'Created By'," +
                    " a.businesshead_name as 'Business Head Approval',credithead_name as 'Credit Head Approval',"+
                    " date_format(a.scheduled_date, '%d-%m-%Y %h:%i %p') as 'CC Meeting Scheduled On',date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as 'CC Approved On', date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as 'CAD Accepted On'," +
                    " c.sanction_refno as 'Sanction Ref.No',a.overalllimit_amount as 'Sanction Limit'," +
                    " (select group_concat(date_format(c.approver_approveddate, '%d-%m-%Y %h:%i %p')) from ocs_trn_tprocesstype_assign c where c.menu_name = 'Sanction' and c.application_gid = a.application_gid) as 'Sanction Approved on'," +
                    " (select group_concat(date_format(c.maker_approveddate, '%d-%m-%Y %h:%i %p')) from ocs_trn_tprocesstype_assign c where c.menu_name = 'Document Checklist' and c.application_gid = a.application_gid) as 'Document Checklist created on'," +
                    " (select group_concat(date_format(c.approver_approveddate, '%d-%m-%Y %h:%i %p')) from ocs_trn_tprocesstype_assign c where c.menu_name = 'Document Checklist' and c.application_gid = a.application_gid) as 'Document Checklist Approved  on', " +
                    " (select group_concat(date_format(c.maker_approveddate, '%d-%m-%Y %h:%i %p')) from ocs_trn_tprocesstype_assign c where c.menu_name = 'LSA' and c.application_gid = a.application_gid) as 'LSA Making date'," +
                    " (select group_concat(date_format(c.approver_approveddate, '%d-%m-%Y %h:%i %p')) from ocs_trn_tprocesstype_assign c where c.menu_name = 'LSA' and c.application_gid = a.application_gid) as 'LSA Approving date'," +
                    " (select group_concat(date_format(d.created_date, '%d-%m-%Y %h:%i %p')) from ocs_trn_ttagquery d where d.fromphysical_document = 'N' and d.application_gid = a.application_gid) as 'Soft Copy Vetting Query raised on'," +
                    " (select group_concat(date_format(d.querystatus_updateddate, '%d-%m-%Y %h:%i %p')) from ocs_trn_ttagquery d where d.fromphysical_document = 'N' and d.application_gid = a.application_gid) as 'Soft Copy Vetting Query Approved on' ," +
                    " (select group_concat(date_format(d.query_responseddate, '%d-%m-%Y %h:%i %p')) from ocs_trn_ttagquery d where d.fromphysical_document = 'N' and d.application_gid = a.application_gid) as 'Soft Copy Vetting Query Resolved on'," +
                    " (select group_concat(date_format(d.created_date, '%d-%m-%Y %h:%i %p')) from ocs_trn_ttagquery d where d.fromphysical_document = 'Y' and d.application_gid = a.application_gid) as 'Original Copy Vetting Query raised on'," +
                    " (select group_concat(date_format(d.querystatus_updateddate, '%d-%m-%Y %h:%i %p')) from ocs_trn_ttagquery d where d.fromphysical_document = 'Y' and d.application_gid = a.application_gid) as 'Original Copy Vetting Query Approved on' ," +
                    " (select group_concat(date_format(d.query_responseddate, '%d-%m-%Y %h:%i %p')) from ocs_trn_ttagquery d where d.fromphysical_document = 'Y' and d.application_gid = a.application_gid) as 'Original Copy Vetting Query Resolved on' " +
                    " from ocs_trn_tcadapplication a " +
                    " left join ocs_trn_tprocesstype_assign b on a.application_gid = b.application_gid " +
                    " left join ocs_trn_tapplication2sanction c on c.application_gid = a.application_gid " +
                    " where a.process_type = 'Accept' group by a.application_gid order by a.application_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("TAT CAD Accepted Customers");

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "TAT CAD Accepted Customers.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/TATCADAcceptedCustomers/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/TATCADAcceptedCustomers/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/TAT CADAcceptedCustomers/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range1 = workSheet.Cells[1, 1, 1, 24])  //Address "A1:BD1"

                {
                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);

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
        //TAT CAD Accepted Customers Export Excel --- ENDING

        //Document approval summary
       

        public void DaGetCADDocChecklistReportApprovalCompletedSummary(string employee_gid, MdlMstCAD values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,h.sanction_refno, " +
                     " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                     " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as checkerapproved_by, " +
                     " date_format(d.checkerapproved_on, '%d-%m-%Y %h:%i %p') as checkerapproved_on,a.customer_urn," +
                     " a.creditgroup_gid, docchecklist_approvalflag, d.approval_status as approval, g.cadgroup_name from ocs_trn_tcadapplication a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join ocs_trn_tapplication2docchecklist d on d.application_gid = a.application_gid " +
                     " left join hrm_mst_temployee e on e.employee_gid = d.checkerapproved_by " +
                     " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                     " left join ocs_trn_tprocesstype_assign g on g.application_gid = a.application_gid " +
                     " left join ocs_trn_tapplication2sanction h on h.application_gid = a.application_gid " +
                     " where a.process_type = 'Accept' and docchecklist_approvalflag='Y' and " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                     " and approver_approvalflag = 'Y')" +
                     " group by a.application_gid order by a.updated_date desc ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
       
           var getapplicationadd_list = new List<cadapplicationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                   
                    getapplicationadd_list.Add(new cadapplicationlist
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        ccapproved_date = dt["ccapproved_date"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        
                        cadgroupname = dt["cadgroup_name"].ToString(),
                        cadaccepted_by = dt["cadaccepted_by"].ToString(),
                        cadaccepted_date = dt["cadaccepted_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_urn = dt["customer_urn"].ToString()
                    });

                }
                values.cadapplicationlist = getapplicationadd_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            
            dt_datatable.Dispose();
        }


        //approval completed report 
        public void DaExportDocCheckApprovalCompletedReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN,  " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date,  " +
                     " e.sanction_refno as Sanction_Reference_No ,d.maker_name as Maker_Name,d.checker_name as Checker_Name, " +
                     " d.approver_name as Approver_Name,f.company_name as Institution_Name, " +
                     " '' as Individual_Name, " +
                     " f.stakeholder_type as Stakeholder_Type, " +
                     " GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_checklist,  " +
                     " GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist  " +
                     " from ocs_trn_tcadinstitution f   " +
                     "  left join ocs_trn_tcadapplication  a on f.application_gid = a.application_gid   " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by   " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid   " +
                     " left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL'  " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid    " +
                     " left join ocs_trn_tcaddocumentchecktls i on f.institution_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N') " +
                     " left join ocs_trn_tcadcovanantdocumentcheckdtls j on f.institution_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N') " +
                     "  left join ocs_trn_tapplication2docchecklist k on k.application_gid = a.application_gid    " +
                     " where a.process_type = 'Accept' and docchecklist_approvalflag='Y' and  " +
                     "  a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                     " and approver_approvalflag = 'Y') " +
                     " group by f.institution_gid )  union  " +
                     " (select a.application_no as Application_No ,a.customerref_name as Customer_Name,a.customer_urn as Customer_URN,  " +
                     " date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as CC_Approved_Date,  " +
                     " e.sanction_refno as Sanction_Reference_No ,  " +
                     " d.maker_name as Maker_Name,  " +
                     " d.checker_name as Checker_Name,  " +
                     " d.approver_name as Approver_Name,  " +
                     " '' as Institution_Name,  " +
                     " concat(g.first_name, ' ', g.last_name) as  Individual_Name,  " +
                     " g.stakeholder_type as Stakeholder_Type,  " +
                     " GROUP_CONCAT( distinct( (i.documenttype_name)) SEPARATOR '|| ') as Deferral_Document_Checklist,  " +
                     " GROUP_CONCAT(distinct Concat((concat(j.documenttype_name)), ' - ', ' - ', (j.covenant_periods) ) SEPARATOR '|| ') as Covenant_Document_Checklist  " +
                     " from ocs_trn_tcadcontact g   " +
                     " left join ocs_trn_tcadapplication  a on g.application_gid = a.application_gid   " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by   " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid   " +
                     "  left join ocs_trn_tprocesstype_assign d on a.application_gid = d.application_gid and d.menu_gid = 'CADMGTDCL'  " +
                     " left join ocs_trn_tapplication2sanction e on e.application_gid = a.application_gid    " +
                     " left join ocs_trn_tcaddocumentchecktls i on g.contact_gid = i.credit_gid and (i.untagged_type is  NULL or i.untagged_type = 'N') " +
                     " left join ocs_trn_tcadcovanantdocumentcheckdtls j on g.contact_gid = j.credit_gid and (j.untagged_type is  NULL or j.untagged_type = 'N') " +
                     "  left join ocs_trn_tapplication2docchecklist k on k.application_gid = a.application_gid   " +
                     " where a.process_type = 'Accept' and docchecklist_approvalflag='Y' and  " +
                     " a.application_gid in (Select application_gid from ocs_trn_tprocesstype_assign where menu_gid = 'CADMGTDCL' " +
                     " and approver_approvalflag = 'Y') " +
                     " group by g.contact_gid )    ";

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
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Document Checklist Approval Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Document Checklist Approval Completed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Document Checklist Approval Completed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 14])  //Address "A1:A9"
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


        //Report in Disbursement 

        public void DaLoanODDispersement(ExportExcelAddprogram objMstApplicationReport, string rmdisbursementrequest_gid)
        {
            msSQL = " select '' as `Account Id`,'' as `Unused`,'Disbursement' as `Transaction name`,curdate() as `Value Date`,curdate() as `Unused`,a.disbursement_to, " +
                    " case when a.disbursement_to = 'Applicant(B2B)' then d.checkerdisbursement_amount when a.disbursement_to = 'Farmer(B2B2C)' then e.creditopscheckerdisbursement_amount when a.disbursement_to = 'Supplier' then f.creditopscheckerdisbursement_amount end as `Amount`, " +
                    " '0' as `Unused`,'0' as `Unused`,'0' as `Fee1`,'0' as `Fee2`,'0' as `Fee3`,'' as `Fee4`,'' as `Fee5`, " +
                    " 'MANUAL FORM' as `Description`,'0' as `Unused`,'0' as `Unused`,c.user_code as `UserId`,'' as `First Repayment Date`, " +
                    " 'CHEQUE_OR_DD' as `Instrument`,'accountCode:AxisBank' as `Reference` " +
                    " from ocs_trn_trmdisbursementrequest a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tdisbursementamount  d on a.rmdisbursementrequest_gid = d.rmdisbursementrequest_gid " +
                    " left join  ocs_trn_tfarmercontact e on a.rmdisbursementrequest_gid = e.rmdisbursementrequest_gid " +
                    " left join ocs_trn_tdisbursementsupplier f on a.rmdisbursementrequest_gid = f.rmdisbursementrequest_gid " +
                    " where a.rmdisbursementrequest_gid = '" + rmdisbursementrequest_gid + "' group by a.rmdisbursementrequest_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("LoanODDisp");

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "LoanODDisp.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/LoanODDisp/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/LoanODDisp/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/LoanODDisp/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range1 = workSheet.Cells[1, 1, 1, 21])  //Address "A1:BD1"

                {
                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);

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

        public void DaDispersementNEFT(ExportExcelAddprogram objMstApplicationReport, string rmdisbursementrequest_gid)
        {
            msSQL = "set @a:=0";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select (@a:=@a+1) as `S.No`,curdate() as `Date`,'NBFC' as `Books`,'MANUAL FORM' as `Application Id`,'HO' as `Customer Branch Name`,b.vertical_name as `Vertical`,'TERM' as `Type`,'' as `Loan Acc No`, " +
                    " case when a.disbursement_to = 'Applicant' then (case when b.applicant_type = 'Institution' then c.company_name else d.first_name end) when a.disbursement_to = 'Supplier' then i.supplier_name when a.disbursement_to = 'Farmer' then concat(h.first_name,' ',h.middle_name,' ',h.last_name) end as `Customer Name`, " +
                    " case when a.disbursement_to = 'Farmer(B2B2C)' then e.bank_name when a.disbursement_to = 'Supplier' then f.bank_name when a.disbursement_to = 'ApplicantB2B' then j.bank_name end as `Bank Name`, " +
                    " case when a.disbursement_to = 'Farmer(B2B2C)' then e.branch_name when a.disbursement_to = 'Supplier' then f.branch_name when a.disbursement_to = 'ApplicantB2B' then j.branch_name end as `Branch Name`, " +
                    " case when a.disbursement_to = 'Farmer(B2B2C)' then e.bankaccount_number when a.disbursement_to = 'Supplier' then f.bankaccount_number when a.disbursement_to = 'ApplicantB2B' then j.bankaccount_number end as `Account No`, " +
                    " case when a.disbursement_to = 'Farmer(B2B2C)' then e.ifsc_code when a.disbursement_to = 'Supplier' then f.ifsc_code when a.disbursement_to = 'ApplicantB2B' then j.ifsc_code end as `IFSC Code`, " +
                    " case when a.disbursement_to = 'Farmer(B2B2C)' then e.micr_code when a.disbursement_to = 'Supplier' then f.micr_code when a.disbursement_to = 'ApplicantB2B' then j.micr_code end as `MICR Code`, " +
                    " case when a.disbursement_to = 'Applicant(B2B)' then g.checkerdisbursement_amount when a.disbursement_to = 'Farmer(B2B2C)' then h.creditopscheckerdisbursement_amount when a.disbursement_to = 'Supplier' then i.creditopscheckerdisbursement_amount end as `NEFT Disbursement`,'0' as `InsuranceProcess fee`, " +
                    " case when a.disbursement_to = 'Applicant(B2B)' then g.checkerdisbursement_amount when a.disbursement_to = 'Farmer(B2B2C)' then h.creditopscheckerdisbursement_amount when a.disbursement_to = 'Supplier' then i.creditopscheckerdisbursement_amount end as `Disbursement`, " +
                    " a.processing_fees as `Pro Fee`,'0' as `PAI`,'0' as `GTL`,a.processing_gst as `GST`,'' as `Existing o/s`,'0' as `Projected Interest`,'0' as `Existing LAN`, " +
                    " 'PF COLLECTED IN DISB ON OVERALL LIMIT' as `Processing fee Charged Type`,'' as `Bank Transaction`, '' as `Bank Name` " +
                    " from ocs_trn_trmdisbursementrequest a " +
                    " left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                    " left join ocs_trn_tcadinstitution c on c.application_gid = b.application_gid and c.stakeholder_type = 'Applicant' " +
                    " left join ocs_trn_tcadcontact d on d.application_gid = b.application_gid and d.stakeholder_type = 'Applicant' " +
                    " left join ocs_trn_tfarmercontact e on e.application_gid = a.application_gid  " +
                    " left join ocs_trn_tdisbursementsupplier f on f.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid " +
                    " left join ocs_trn_tdisbursementamount  g on a.rmdisbursementrequest_gid = g.rmdisbursementrequest_gid " +
                    " left join  ocs_trn_tfarmercontact h on a.rmdisbursementrequest_gid = h.rmdisbursementrequest_gid " +
                    " left join ocs_trn_tdisbursementsupplier i on a.rmdisbursementrequest_gid = i.rmdisbursementrequest_gid " +
                    " left join ocs_trn_tdisbapplicantbankdtl j on j.rmdisbursementrequest_gid = a.rmdisbursementrequest_gid and j.disbursementaccount_status = 'Yes' " +
                    " where a.rmdisbursementrequest_gid = '" + rmdisbursementrequest_gid + "' group by a.rmdisbursementrequest_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("NEFT");

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "NEFT.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/NEFT/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/TATCADAcceptedCustomers/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/NEFT/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range1 = workSheet.Cells[1, 1, 1, 27])  //Address "A1:BD1"

                {
                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);

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

        public void DaODAccountopen(ExportExcelAddprogram objMstApplicationReport, string application_gid)
        {
            msSQL = " select '' as `Account Id`,'' as `Product Code`,'3800' as `Branch Code`,x.facilityoverall_limit as `Tenure Magnitude`,'' as `Tenure Unit`,z.amounttobe_disbursed as `Loan Amount(Rs)`,curdate() as `Opened on Date(yyyy-mm-dd)`, " +
             " 'INR' as `Currency Code`,a.customer_urn as `Customer Id 1`, " +
             " case when a.applicant_type = 'Institution' then b.company_name when applicant_type = 'Individual' then c.first_name end as `Customer 1 First Name`,c.middle_name as `Customer 1 Middle Name`,c.last_name as `Customer 1 Last Name`," +
             " case when a.applicant_type = 'Institution' then d.addressline1 when applicant_type = 'Individual' then e.addressline1 end as `Customer 1 Address1`," +
             " case when a.applicant_type = 'Institution' then d.addressline2 when applicant_type = 'Individual' then e.addressline2 end as `Customer 1 Address2`," +
             " case when a.applicant_type = 'Institution' then d.landmark when applicant_type = 'Individual' then e.landmark end as `Customer 1 Address3`," +
             " case when a.applicant_type = 'Institution' then d.country when applicant_type = 'Individual' then e.country end as `Customer 1 Country Code`," +
             " case when a.applicant_type = 'Institution' then d.state when applicant_type = 'Individual' then e.state end as `Customer 1 State Code`," +
             " case when a.applicant_type = 'Institution' then d.city when applicant_type = 'Individual' then e.city end as `Customer 1 City Code`," +
             " case when a.applicant_type = 'Institution' then f.mobile_no  when applicant_type='Individual' then g.mobile_no end as `Customer 1 Phone1`,'' as `Customer 1 Phone2`," +
             " case when a.applicant_type = 'Institution' then h.email_address when applicant_type='Individual' then i.email_address end as `Customer 1 Email`," +
             " case when a.applicant_type = 'Institution' then d.postal_code when applicant_type = 'Individual' then e.postal_code end as `Customer 1 Pin Code`," +
             " '' as `Customer Id 2`,'' as `Customer 2 First Name`,'' as `Customer 2 Middle Name`,'' as `Customer 2 Last Name`,'' as `Customer 2 Address1`,'' as `Customer 2 Address2`," +
             " '' as `Customer 2 Address3`,'' as `Customer 2 Country Code`, '' as `Customer 2 State Code`, '' as `Customer 1 City Code`,'' as `Customer 1 Phone1`,'' as `Customer 1 Phone2`,'' as `Customer 2 Email`,'' as `Customer 2 Pin Code`," +
             " '' as `Guarantor CustomerId`,l.user_code as `Open UserId`,x.rate_interest as `Normal Interest`," +
             " j.bankaccount_name as `Account Holder Name`,j.bankaccount_number as `Bank Account Number`,j.bank_name as `Bank Name`," +
             " j.branch_name as `Branch Name`,j.ifsc_code as `IFSC Code`,l.user_code as `RO Name`," +
             " '' as `Customer DOB`,'' as `Nominee CustomerId`,'' as `Nominee Name`,'' as `Nominee DOB`,'' as `Occupation`,'' as `Father/Spouse Name`," +
             " '' as `Gender`,'' as `Relationship with Insurer`,'' as `Annual Income`,n.idproof_name as `KYC Document Type`,n.idproof_no as `KYC Document Number`,'' as `Company Type`,a.vertical_name as `Sector`," +
             " '' as `Segment`,'' as `Value Chain Primary`,'' as `Value Chain Secondary`, '' as `Intervention`," +
             " '' as `LoanSourcing Details`, '' as `K2B Name/Referral Name`,'' as `K2B Code/Referral Code`,'' as `Samunnati Pay Card`,q.loan_type as `Collateral`, '' as `Hypothecated To`,'' as `Proposal Type`," +
             " '' as `Udf Date1`,'' as `Udf Date2`,m.gst_no as `GSTIN`,'' as `Location Code`,k.employee_emailid as `RO EmailId`,'' as `Moratorium Period`," +
             " '' as `Udf Text11`,'' as `Udf Text12`,concat(l.user_firstname,l.user_lastname) as `Udf Text13`,'' as `Product Solution`,'' as `Sub Product`,'' as `Business/Industry Type`," +
             " '' as `Udf Text17`,'' as `Udf Text18`,'' as `Udf Text19`,'' as `Udf Text20`,'' as `Guarantor CustomerId 2`," +
             " '' as `Udf Text21`,'' as `Udf Text22`,'' as `Udf Text23`,'' as `Udf Text24`,'' as `Udf Text25`,'' as `Udf Text26`,'' as `Udf Text27`,'' as `Udf Text28`,'' as `Udf Text29`,'' as `Udf Text30`," +
             " x.penal_interest as `Penal Interest`," +
             " '' as `Udf Text31`,'' as `Udf Text32`,'' as `Udf Text33`,'' as `Udf Text34`,'' as `Udf Text35`,'' as `Udf Text36`,'' as `Udf Text37`,'' as `Udf Text38`,'' as `Udf Text39`,'' as `Udf Text40`," +
             " '' as `Udf Text41`,'' as `Udf Text42`,'' as `Udf Text43`,'' as `Udf Text44`,'' as `Udf Text45`,'' as `Udf Text46`,'' as `Udf Text47`,'' as `Udf Text48`," +
             " '' as `Customer Id 3`,'' as `Customer 3 First Name`,'' as `Customer 3 Middle Name`,'' as `Customer 3 Last Name`," +
             " '' as `Customer 3 Address1`,'' as `Customer 3 Address2`, '' as `Customer 3 Address3`,'' as `Customer 3 Country Code`," +
             " '' as `Customer 3 State Code`, '' as `Customer 3 City Code`,'' as `Customer 3 Phone1`,'' as `Customer 3 Phone2`," +
             " '' as `Customer 3 Email`,'' as `Customer 3 Pin Code`," +
             " '' as `Demand Interval`,'' as `BPI Demand`,'' as `Compunding Day`,'' as `Compunding Interval`,'' as `Post Maturity Demand Interval`" +
             " from ocs_trn_trmdisbursementrequest z" +
             " left join ocs_trn_tcadapplication a on z.application_gid = a.application_gid" +
             " left join ocs_trn_tcadapplication2loan x on z.application2loan_gid = x.application2loan_gid" +
             " left join ocs_trn_tcadinstitution b on b.application_gid = a.application_gid and b.stakeholder_type = 'Applicant'" +
             " left join ocs_trn_tcadcontact c on c.application_gid = a.application_gid and c.stakeholder_type = 'Applicant'" +
             " left join ocs_trn_tcadinstitution2address d on b.institution_gid = d.institution_gid and d.primary_status = 'Yes'" +
             " left join ocs_trn_tcadcontact2address e on c.contact_gid = e.contact_gid and e.primary_status = 'Yes'" +
             " left join ocs_trn_tcadinstitution2mobileno f on f.institution_gid = b.institution_gid and f.primary_status = 'Yes'" +
             " left join ocs_trn_tcadcontact2mobileno g on g.contact_gid = c.contact_gid and g.primary_status = 'Yes'" +
             " left join ocs_trn_tcadinstitution2email h on h.institution_gid = b.institution_gid and h.primary_status = 'Yes'" +
             " left join ocs_trn_tcadcontact2email i on i.contact_gid = c.contact_gid and i.primary_status = 'Yes'" +
             " left join ocs_trn_tcadcreditbankdtl j on j.application_gid = a.application_gid" +
             " left join hrm_mst_temployee k on k.employee_gid = a.created_by" +
             " left join adm_mst_tuser l on l.user_gid = k.user_gid" +
             " left join ocs_trn_tcustomercreationlms m on j.application_gid = m.application_gid" +
             " left join ocs_trn_tcadapplication2loan q on q.application_gid =a.application_gid" +
             " left join ocs_mst_tcontact2idproof n on n.contact_gid = c.contact_gid  where a.application_gid='" + application_gid + "'group by a.application_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("OD Acc Open");

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "OD Acc Open.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/OD Acc Open/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/OD Acc Open/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/OD Acc Open/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range1 = workSheet.Cells[1, 1, 1, 134])  //Address "A1:BD1"

                {
                    range1.Style.Font.Bold = true;
                    range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range1.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range1.Style.Font.Color.SetColor(Color.White);

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

        //Report End

        //Company Document

        public void DaGetExportMstCompanyReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " select a.api_code,a.companydocument_name as 'Company Document Name',a.documenttype_name as 'Document Type Name',a.documentseverity_name as 'Document Severity Name',case when a.covenant_type = 'Y' then 'Yes' else 'No' end as 'Covenant Type'," +
                    " a.lms_code as 'LMS Code',a.bureau_code as 'Bureau Code',a.document_code as 'Document Code',a.display_order as 'Display Order', " +
                    " replace(a.program_name, ',', ' | ') as 'Program Name',a.document_remarks as 'Remarks', " +
                    " (select group_concat(distinct(q.checklist_name) separator ' |') from ocs_mst_tcompanychecklist q where q.companydocument_gid = a.companydocument_gid GROUP BY a.companydocument_gid) as 'Checklist Name', " +
                    " case when a.status='N' then 'Inactive' else 'Active' end as 'Status'," +
                    "  concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By', " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created Date', " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as 'Updated By'," +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as 'Updated Date' FROM ocs_mst_tcompanydocument a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee e on a.updated_by = e.employee_gid " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Company Document Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Company Document Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Company Document Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Company Document Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Company Document Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 17])  //Address "A1:A9"
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


        //Group Document

        public void DaExportMstGroupReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " SELECT a.api_code,a.groupdocument_name as 'Group Document Name',a.documenttype_name as 'Document Type Name',a.documentseverity_name as 'Document Severity Name',case when a.covenant_type = 'Y' then 'Yes' else 'No' end as 'Covenant Type', " +
                    " a.lms_code as 'LMS Code',a.bureau_code as 'Bureau Code',a.document_code as 'Document Code',a.display_order as 'Display Order', " +
                    " replace(a.program_name, ',', ' | ') as 'Program Name',a.document_remarks as 'Remarks'," +
                    " (select group_concat(distinct(q.checklist_name) separator ' |') from ocs_mst_tgroupchecklist q where q.groupdocument_gid = a.groupdocument_gid GROUP BY a.groupdocument_gid) as 'Checklist Name', " +
                    " case when a.status='N' then 'Inactive' else 'Active' end as 'Status'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By', " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created Date', " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as 'Updated By', " +             
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as 'Updated Date' FROM ocs_mst_tgroupdocument a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee e on a.updated_by = e.employee_gid " +
                    " left join adm_mst_tuser f on f.user_gid = e.user_gid where delete_flag='N'";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Group Document Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Group Document Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Group Document Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Group Document Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Group Document Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 17])  //Address "A1:A9"
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

        //Individual Document

        public void DaExportMstIndividualReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " SELECT a.api_code,a.individualdocument_name as 'Individual Document Name',a.documenttype_name as 'Document Type Name',a.documentseverity_name as 'Document Severity Name',case when a.covenant_type = 'Y' then 'Yes' else 'No' end as 'Covenant Type',  " +
                   " a.lms_code as 'LMS Code',a.bureau_code as 'Bureau Code',a.document_code as 'Document Code',a.display_order as 'Display Order',   " +
                   " replace(a.program_name, ',', ' | ') as 'Program Name',a.document_remarks as 'Remarks',   " +
                   " (select group_concat(distinct(q.checklist_name) separator ' |') from ocs_mst_tindividualchecklist q where q.individualdocument_gid = a.individualdocument_gid GROUP BY a.individualdocument_gid) as 'Checklist Name',  " +
                   " case when a.status='N' then 'Inactive' else 'Active' end as 'Status'," +
                   " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By',  " +
                   " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created Date',  " +
                   " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as 'Updated By',   " +
                   " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as 'Updated Date' FROM ocs_mst_tindividualdocument a  " +
                   " left join hrm_mst_temployee b on a.created_by = b.employee_gid  " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                   " left join hrm_mst_temployee e on a.updated_by = e.employee_gid  " +
                   " left join adm_mst_tuser f on f.user_gid = e.user_gid ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Individual Document Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Individual Document Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Individual Document Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Individual Document Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Individual Document Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 17])  //Address "A1:A9"
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

        //Encore Document

        public void DaExportMstEncoreReport(MstApplicationReport objMstApplicationReport)
        {
            msSQL = " select a.api_code,a.Products as 'Products',a.SubProduct as 'Sub Product',a.PrincipalTennure as 'Principal Tennure',a.InterestTennure as 'interest Tennure',a.LoanTennure as 'Loan Tennure'," +
                   " case when a.Intdeductstatus = 'Y' then 'Yes' else 'No' end as 'Interest Deduction Upfront',  " +
                   " case when a.Moratoriamstatus = 'Y' then 'Yes' else 'No' end as 'Moratorium Status',  " +
                   " a.MoratoriumType as 'Moratorium Type',a.lms_code as 'LMS Code',case when a.status_log = 'Y' then 'Active' else 'Inactive' end as 'Status'," +
                   " a.remarks as 'Remarks',concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By',  " +
                   " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created Date', " +
                   " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as 'Updated By', " +
                   " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as 'Updated Date' FROM ocs_mst_tencoreproduct a " +
                   " left join adm_mst_tuser c on c.user_gid = a.created_by " +
                   " left join adm_mst_tuser f on f.user_gid = a.updated_by "; 


            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Encore Product Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstApplicationReport.lsname = "Encore Product Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Encore Product Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstApplicationReport.lscloudpath = lscompany_code + "/" + "Master/Encore Product Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                objMstApplicationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Encore Product Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstApplicationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objMstApplicationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 17])  //Address "A1:A9"
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

        public void DaGetConsolidatedSanctionReport( MdlMstAssignmentview values)
        {
           
            msSQL = "call ocs_trn_tspconsolidatedsanctionreport";
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
                        sanction_refno = (dr_datarow["sanction_refno"].ToString()),
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

            msSQL = "call ocs_trn_tspconsolidatedlsareport";
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

            msSQL = "call ocs_trn_tspconsolidateddocumentchecklistreport";
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

            msSQL = "call ocs_trn_tspconsolidatedsoftcopyvettingreport";
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

            msSQL = "call ocs_trn_tspconsolidatedoriginalcopyvettingreport";
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

            msSQL = "call ocs_trn_tspcadconsolidatedcount";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.SanctionCount = objODBCDataReader["SanctionCount"].ToString();
                values.LSACount = objODBCDataReader["LSACount"].ToString();
                values.DocumentCheckListCount = objODBCDataReader["DocumentCheckListCount"].ToString();
                values.SoftcopyVettingCount = objODBCDataReader["SoftcopyVettingCount"].ToString();
                values.OriginalCopyVettingCount = objODBCDataReader["OriginalCopyVettingCount"].ToString();
               

            }
            objODBCDataReader.Close();

        }


    }
}
