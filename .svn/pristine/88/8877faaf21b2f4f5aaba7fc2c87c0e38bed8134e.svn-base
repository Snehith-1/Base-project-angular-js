using ems.foundation.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.Odbc;
using System.Linq;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Web.Script.Serialization;
using System.Net;
using System.Web;
using ems.storage.Functions;
using System.Web.UI;
using System.Web.Http;
using System.IO;
using OfficeOpenXml;
using System.Drawing;


namespace ems.foundation.DataAccess
{
    public class DaFndTrnMyCampaignSummary
    {
       
        //public HttpResponse Response { get; set; }
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        DataTable dt_tab;
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, Query, msGettaguser2audit_gid, msGetemployee_gid, lsquery_status, lssession_user, count, lsauditdepartment_value, lscapture_yesscore, lscapture_noscore, lscapture_partialscore, lscapture_nascore, lscapture_totalscore, msGetaudituniqueno, lsdue_date, lsreport_date, lsperiodfrom_date, lsauditperiod_to, lsauditname_value;
        int mnResult;
        string lsSingleformflag;
        int k;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lsemployee_name, lsemployee_gid, lcampaign_status, lsTo2members, lsmycampaign_remarks, lscampaign_type, lsname, lscampaign_name, lscampaign_apr, lscampaign_remarks, lsBccmail_id, lscreated_by, lstomembers, lsdescription, lscc2members, lstonames, lsauditcreation_gid, lscreated_date, frommail_id, lscc_mail, strBCC, lsbcc_mail;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;


        public void DaGetCampaignSummary(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {
            
        
                msSQL = "select a.campaign_gid,a.campaign_code,a.campaign_name," +
               " CASE a.campaign_status WHEN 'Approved' then 'WIP' " +
                " WHEN 'Pending for Final Approval' then 'Pending for Final Approval' " +
                " WHEN 'WIP' THEN(CASE WHEN(e.mycampaign_status = 'Pending for Final Approval') THEN e.mycampaign_status ELSE 'WIP' END) " +
                " END as campaign_status,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
                 "  date_format(a.assesment_date,'%d-%m-%Y %h:%i %p') as assesment_date , " +
                 " case when a.status='N' then 'Inactive' else 'Active' end as status, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                 " a.status_flag,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_manager," +
                " d.campaigntype_name,concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as campaign_approver  " +
                 " from fnd_trn_tcampaign  a" +
                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                   " left join hrm_mst_temployee f on a.campaign_approver = f.employee_gid" +
                " left join fnd_mst_tcampaigntype d on a.campaigntype_gid = d.campaigntype_gid" +
                " left join adm_mst_tuser c on c.user_gid = b.user_gid   " +
                 " left join adm_mst_tuser g on g.user_gid = f.user_gid   " +
                "  left join fnd_mst_tcampaignapproving2employee e on e.campaign_gid = a.campaign_gid " +
                " and e.employee_gid = '"+ employee_gid +"' " +
                "  where (a.campaign_status = 'Approved' or a.campaign_status = 'Pending for Final Approval' or "+
                " a.campaign_status = 'WIP')" +
                " and e.employee_gid = '" + employee_gid + "'" +
                " order by campaign_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmycampaign_list = new List<mycampaign_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmycampaign_list.Add(new mycampaign_list
                        {
                            campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                            campaign_code = (dr_datarow["campaign_code"].ToString()),
                            campaign_name = (dr_datarow["campaign_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            assesment_date = (dr_datarow["assesment_date"].ToString()),
                            campaign_approver = (dr_datarow["campaign_approver"].ToString()),
                            campaign_manager = (dr_datarow["campaign_manager"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            campaign_status = (dr_datarow["campaign_status"].ToString()),
                            campaigntype_name = (dr_datarow["campaigntype_name"].ToString()),               
                            status_flag = (dr_datarow["status_flag"].ToString()),

                        });
                    }
                    values.mycampaign_list = getmycampaign_list;
                }
                dt_datatable.Dispose();                       
          
        }



        public void DaGetCampaignSummaryApproved(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {


            msSQL = "select a.campaign_gid,a.campaign_code,a.campaign_name,a.campaign_status,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
             "  date_format(a.assesment_date,'%d-%m-%Y %h:%i %p') as assesment_date , case when a.status='N' then 'Inactive' else 'Active' end as status, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
             " a.status_flag,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_manager,d.campaigntype_name,concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as campaign_approver  " +
             " from fnd_trn_tcampaign  a" +
             " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
              " left join hrm_mst_temployee f on a.campaign_approver = f.employee_gid" +
            " left join fnd_mst_tcampaigntype d on a.campaigntype_gid = d.campaigntype_gid" +
            "  left join fnd_mst_tcampaignapproving2employee e on e.campaign_gid = a.campaign_gid " +
            " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
             " left join adm_mst_tuser g on g.user_gid = f.user_gid" +
            " where e.employee_gid ='" + employee_gid + "' and a.campaign_status = 'Mycampaign closed'  order by campaign_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmycampaign_list = new List<mycampaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmycampaign_list.Add(new mycampaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        assesment_date = (dr_datarow["assesment_date"].ToString()),
                        campaign_approver = (dr_datarow["campaign_approver"].ToString()),
                        campaign_manager = (dr_datarow["campaign_manager"].ToString()),
                        status = (dr_datarow["status"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        campaigntype_name = (dr_datarow["campaigntype_name"].ToString()),
                        status_flag = (dr_datarow["status_flag"].ToString()),

                    });
                }
                values.mycampaign_list = getmycampaign_list;
            }
            dt_datatable.Dispose();

        }

        public void DaGetCampaignSummaryRejected(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {


            msSQL = "select a.campaign_gid,a.campaign_code,a.campaign_name,a.campaign_status,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
             "  date_format(a.assesment_date,'%d-%m-%Y %h:%i %p') as assesment_date , case when a.status='N' then 'Inactive' else 'Active' end as status, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
             " a.status_flag,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_manager,d.campaigntype_name,concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as campaign_approver  " +
             " from fnd_trn_tcampaign  a" +
             " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
              " left join hrm_mst_temployee f on a.campaign_approver = f.employee_gid" +
            " left join fnd_mst_tcampaigntype d on a.campaigntype_gid = d.campaigntype_gid" +
              "  left join fnd_mst_tcampaignapproving2employee e on e.campaign_gid = a.campaign_gid " +
            " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
              " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
            " where e.employee_gid ='" + employee_gid + "' and a.campaign_status = 'Mycampaign rejected'  order by campaign_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmycampaign_list = new List<mycampaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmycampaign_list.Add(new mycampaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        assesment_date = (dr_datarow["assesment_date"].ToString()),
                        campaign_approver = (dr_datarow["campaign_approver"].ToString()),
                        campaign_manager = (dr_datarow["campaign_manager"].ToString()),
                        status = (dr_datarow["status"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        campaigntype_name = (dr_datarow["campaigntype_name"].ToString()),
                        status_flag = (dr_datarow["status_flag"].ToString()),

                    });
                }
                values.mycampaign_list = getmycampaign_list;
            }
            dt_datatable.Dispose();

        }
        public void DaGetMyCampaignCounts(MdlFndTrnMyCampaignSummary values, string Employee_gid)
        {
            msSQL = " select count(a.campaign_gid) as mycampaignpending_count from fnd_trn_tcampaign a  " +
                     "  left join fnd_mst_tcampaignapproving2employee b on b.campaign_gid = a.campaign_gid " +
            " where (a.campaign_status = 'Approved' or a.campaign_status = 'Pending for Final Approval' or " +
                " a.campaign_status = 'WIP')  and b.employee_gid = '" + Employee_gid + "' ";
           
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.mycampaignpending_count = objODBCDatareader["mycampaignpending_count"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = "select count(a.campaign_gid) as campaignapproved_count from fnd_trn_tcampaign a  " +
                 "  left join fnd_mst_tcampaignapproving2employee b on b.campaign_gid = a.campaign_gid " +
                  " where  b.employee_gid = '" + Employee_gid + "' and a.campaign_status = 'Mycampaign closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.campaignapproved_count = objODBCDatareader["campaignapproved_count"].ToString();

            }

            objODBCDatareader.Close();

            msSQL = "select count(a.campaign_gid) as campaignrejected_count from fnd_trn_tcampaign a  " +
                 "  left join fnd_mst_tcampaignapproving2employee b on b.campaign_gid = a.campaign_gid " +
                   "where  b.employee_gid = '" + Employee_gid + "' and a.campaign_status = 'Mycampaign rejected'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.campaignrejected_count = objODBCDatareader["campaignrejected_count"].ToString();

            }

            objODBCDatareader.Close();           
        }

        public void DaDynamicExcelTemplate(sampledynamicdatadtl values, string campaign_gid,string employee_gid,SingleMultiFormReport objSingleMultiFormReport)
        {
            string lscompany_code;
            var memoryStream = new MemoryStream();
            ExcelPackage xlPackage = new ExcelPackage();
            msSQL = " select company_code from adm_mst_tcompany";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);

            // Create Directory

            objSingleMultiFormReport.lsname = "Questionnaire.xlsx";
            var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Foundation/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            objSingleMultiFormReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Foundation/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objSingleMultiFormReport.lsname;
            bool exists = System.IO.Directory.Exists(path);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            //lsfilePath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/Foundation/SampleImportExcelDocument/" ;
            // lsfilePath = HttpContext.Current.Server.MapPath("../erp_documents/../templates/");
            //  lsfilePath = "E:\\Web\\EMS\\templates";
           // var path = lsfilePath + "\\Questionnaire.xlsx";
            //path = "E:\\Web\\EMS\\templates\\CampaignQuestions.xlsx";

                       FileInfo file ;
            file = new FileInfo(objSingleMultiFormReport.lspath);

            try
            {
                msSQL = " select c.campaign_gid,a.campaigndtl_gid,a.questionnarie_gid, " +
                       " campaign_code,d.campaigntype_name,c.start_date,c.end_date," +
                       " c.assesment_date,e.customer_name,c.contact_name, c.contact_mobile," +
                       " c.contact_email, c.campaign_name,a.questionnarie_type," +
                       " a.questionnarie_answer,  a.questionnarie_name ,a.importance" +
                       " from fnd_trn_tcampaigndtl a" +
                       " left join fnd_mst_tquestionnarie b on a.questionnarie_gid = b.questionnarie_gid" +
                       " left join fnd_trn_tcampaign c on c.campaign_gid = a.campaign_gid" +
                       " left join fnd_mst_tcampaigntype d on d.campaigntype_gid = c.campaigntype_gid" +
                       " left join fnd_mst_tcustomer e on e.customer_gid = c.campaigntype_gid" +
                       " where a.campaign_gid = '" + campaign_gid + "' and a.form_type = 'M' and b.questionnarie_type not in ('List','Radio_Button','Radio Button') order by questionnarie_gid asc";

              
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    int j = 1;
                    memoryStream = new MemoryStream();
                    using ( xlPackage = new ExcelPackage(memoryStream))
                    {
                       
                        ExcelWorkbook workbook = xlPackage.Workbook;
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("QuestionaryList");
                        for (int i = 0; i <= dt_datatable.Rows.Count - 1; i++)
                        {
                            string Questionnairename;
                            worksheet.Cells[1, j].Value = dt_datatable.Rows[i].ItemArray[2].ToString();
                            //worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                            worksheet.Cells[2, j].Value = dt_datatable.Rows[i].ItemArray[15].ToString();
                            Questionnairename = dt_datatable.Rows[i].ItemArray[15].ToString();
                            // worksheet.Cells[2, j].Value = Questionnairename.Replace('"', '_').Replace(')', '_').Replace('(', '_').Replace('?', '_').Replace('-', '_').Replace('/', '_').Replace(' ', '_');
                            worksheet.Cells[2, j].Value = Questionnairename;
                            j = j + 1;
                        }


                        worksheet.Row(1).Hidden = true;

                    
                        byte[] bin = xlPackage.GetAsByteArray();                      

                        //write the file to the disk
                        File.WriteAllBytes(objSingleMultiFormReport.lspath, bin);

                    }

                }


            }
            catch (Exception ex)
            {
                objSingleMultiFormReport.status = false;
                objSingleMultiFormReport.message = "Failure";
            }
            objSingleMultiFormReport.status = true;
            objSingleMultiFormReport.message = "Success";
        }
        public void logforAudit(string strVal)
        {

            string loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + "ErrorLog/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            if ((!System.IO.Directory.Exists(loglspath)))
                System.IO.Directory.CreateDirectory(loglspath);

            loglspath = loglspath + "log.txt";
            System.IO.StreamWriter sw = new System.IO.StreamWriter(loglspath, true);
            sw.WriteLine(strVal);
            sw.Close();

        }
        public void DaCampaignimportexcel(System.Web.HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                sampledynamicdatadtl values = new sampledynamicdatadtl();
                string lscompany_code;               
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/Foundation/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                // lsfilePath= "E:\\Web\\EMS\\templates\\"; 

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);


                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
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
                    objResult.message = "File format is not supported";
                    return;
                }

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                int rowCount;
                int columnCount;
                string excelRange, endRange;
                try
                {
                    using (ExcelPackage xlPackage = new ExcelPackage(ms))
                    {
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                        rowCount = worksheet.Dimension.End.Row;
                        columnCount = worksheet.Dimension.End.Column;
                        endRange = worksheet.Dimension.End.Address;

                    }
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Foundation/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                    file.Close();
                    ms.Close();

                    objcmnfunctions.uploadFile(lspath, lsfile_gid);
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.ToString();
                    return;
                }
                string campaign_gid = httpRequest.Form.Get(1);
              //  string form_type = httpRequest.Form.Get(2);
              
                //Excel To DataTable


                try
                {
                    lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                    excelRange = "A1:" + endRange + rowCount.ToString();
                    dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.ToString();
                    return;
                }
              //  Nullable<DateTime> ldcodecreation_date;

                string[] columnNames = dt.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
                string Header_name = "", Header_value = "";
                int k = 0;
                foreach (DataRow row in dt.Rows)
                {
                    
                    
                   
                    Random rnd = new Random();
                    var rand = rnd.NextDouble();
                    double form_type = Math.Floor(Math.Pow(10, 6 - 1) + rand * (Math.Pow(10, 6) - Math.Pow(10, 6 - 1) - 1));
                    foreach (var i in columnNames)
                    {
                        Header_name = i.Trim();
                        Header_name = Header_name.Replace("*", "");
                        Header_name = Header_name.Replace(" ", "_");

                        Header_value = row[i].ToString();
                        if (k != 0)
                        {

                            string questionnarie_name =string.Empty;
                        string sql = "SELECT  questionnarie_name FROM fnd_mst_tquestionnarie  where questionnarie_gid ='" + Header_name + "'";
                            questionnarie_name = objdbconn.GetExecuteScalar(sql);

                        msSQL = " insert into fnd_trn_tmycampaignmultiple ( " +
                   " campaign_gid,reference_gid," +
                   " questionnarie_gid, header_names," +
                   " header_values," +
                   " created_by,created_date" +
                  "  )" +
                  " values(" +
                  "'" + campaign_gid + "'," +
                  "'" + form_type + "'," +
                  "'" + Header_name + "'," +
                  "'" + questionnarie_name + "'," +
                  "'" + Header_value + "'," +
                  "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = " update fnd_mst_tcampaignapproving2employee set mycampaign_status = 'Pending for Final Approval' " +
                                    " where campaign_gid='" + campaign_gid + "' and employee_gid = '" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            values.status = true;
                            values.message = "Multiple Form Added Successfully";


                           // return true;
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Error Occured While Saving Multiple Form";
                         //   return false;
                        }
                       }
                       
                    }
                    k = k + 1;
                }
            }


            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }

        }
        public void DaGetCampaignApprovalpending(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {

            msSQL = "select a.campaign_gid,a.campaign_code,a.campaign_name,a.campaign_status,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
             "  date_format(a.assesment_date,'%d-%m-%Y %h:%i %p') as assesment_date , case when a.status='N' then 'Inactive' else 'Active' end as status, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
             " a.status_flag,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_manager,d.campaigntype_name,concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as campaign_approver  " +
             " from fnd_trn_tcampaign  a" +
             " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
              " left join hrm_mst_temployee f on a.campaign_approver = f.employee_gid" +
             " left join fnd_mst_tcampaigntype d on a.campaigntype_gid = d.campaigntype_gid" +
             " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
              " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
             " where a.campaign_approver='" + employee_gid + "' and  " +
             " a.campaign_status = 'Pending for Final Approval'" +
             " order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmycampaign_list = new List<mycampaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmycampaign_list.Add(new mycampaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        assesment_date = (dr_datarow["assesment_date"].ToString()),
                        campaign_approver = (dr_datarow["campaign_approver"].ToString()),
                        campaign_manager = (dr_datarow["campaign_manager"].ToString()),
                        status = (dr_datarow["status"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        campaigntype_name = (dr_datarow["campaigntype_name"].ToString()),
                        status_flag = (dr_datarow["status_flag"].ToString()),

                    });
                }
                values.mycampaign_list = getmycampaign_list;
            }
            dt_datatable.Dispose();

        }

        public void DaGetMyCampaignSummaryCounts(MdlFndTrnMyCampaignSummary values, string Employee_gid)
        {
            msSQL = " select count(campaign_gid) as campaignspending_count from fnd_trn_tcampaign  " +
            " where campaign_approver='" + Employee_gid + "' and campaign_status= 'Pending for Final Approval'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.campaignspending_count = objODBCDatareader["campaignspending_count"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = "select count(campaign_gid) as campaignsapproved_count from fnd_trn_tcampaign  " +
                     " where campaign_approver='" + Employee_gid + "' and campaign_status = 'Mycampaign closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.campaignsapproved_count = objODBCDatareader["campaignsapproved_count"].ToString();

            }

            objODBCDatareader.Close();
        }

        public bool MyCampaignRadioSubmit(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {
            string lsSingleCampaignGId;
            msSQL = " select mycampaignsingle_gid from fnd_trn_tmycampaignsingle a where " +
                " a.campaign_gid='" + values.campaign_gid + "' and " +
                " questionnarie_gid = '" + values.questionnarie_gid + "' and a.form_type = 'S'";
            lsSingleCampaignGId = objdbconn.GetExecuteScalar(msSQL);

            if (lsSingleCampaignGId != "")
            {
                msSQL = "update fnd_trn_tmycampaignsingle set " +
                  "questionnarie_answer =  '" + values.questionnarie_answer + "'" +
                  "where mycampaignsingle_gid = '" + lsSingleCampaignGId + "' " +
                  "and campaign_gid= '" + values.campaign_gid + "'";


                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Single Form Added Successfully";

                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured While saving MyCampaign";
                    return false;
                }
            }
            else
            {
                msSQL = " insert into fnd_trn_tmycampaignsingle ( " +
                                  " campaign_gid," +
                                  " questionnarie_gid, questionnarie_name," +
                                  " questionnarie_type, " +
                                  " form_type, status,mycampaign_status, created_by,created_date" +
                                 "  )" +
                                 " values(" +
                                 "'" + values.campaign_gid + "'," +
                                 "'" + values.questionnarie_gid + "'," +
                                 "'" + values.questionnarie_name + "'," +
                                 "'" + values.questionnarie_type + "'," +
                                 "'" + values.form_type + "'," +
                                 "'Pending' ," +
                                 "'Pending for Approval' ," +
                                 "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msSQL = " select mycampaignsingle_gid from fnd_trn_tmycampaignsingle a where " +
                        " a.campaign_gid='" + values.campaign_gid + "' and " +
                        " questionnarie_gid = '" + values.questionnarie_gid + "' and a.form_type = 'S'";
                    lsSingleCampaignGId = objdbconn.GetExecuteScalar(msSQL);

                    if (lsSingleCampaignGId != "")
                    {
                        msSQL = "update fnd_trn_tmycampaignsingle set " +
                                "questionnarie_answer =  '" + values.questionnarie_answer + "'" +
                                "where mycampaignsingle_gid = '" + lsSingleCampaignGId + "' " +
                                "and campaign_gid= '" + values.campaign_gid + "'";


                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }


                    values.status = true;
                    values.message = "Single Form Added Successfully";


                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured While saving MyCampaign";
                    return false;
                }
            }
          

        }
        public bool DaMyCampaignSubmit(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {
            string lsSingleCampaignGId;
            msSQL = " select mycampaignsingle_gid from fnd_trn_tmycampaignsingle a where " +
                " a.campaign_gid='" + values.campaign_gid + "' and " +
                " questionnarie_gid = '" + values.questionnarie_gid + "' and a.form_type = 'S'";
            lsSingleCampaignGId = objdbconn.GetExecuteScalar(msSQL);

            if (lsSingleCampaignGId != "")
            {
                msSQL = "update fnd_trn_tmycampaignsingle set " +
                  "questionnarie_answer =  '" + values.questionnarie_answer + "'" +
                  "where mycampaignsingle_gid = '" + lsSingleCampaignGId + "' " +
                  "and campaign_gid= '" + values.campaign_gid + "'";


                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Single Form Added Successfully";

                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured While saving Single Form";
                    return false;
                }
            }
            else
            {
                msSQL = " select mycampaignsingle_gid from fnd_trn_tmycampaignsingle a where " +
               " a.campaign_gid='" + values.campaign_gid + "' and " +
               " questionnarie_gid = '" + values.questionnarie_gid + "' and a.form_type = 'S'";
                lsSingleCampaignGId = objdbconn.GetExecuteScalar(msSQL);
                if (lsSingleCampaignGId == "")
                {
                    msSQL = " insert into fnd_trn_tmycampaignsingle ( " +
                       " campaign_gid," +
                       " questionnarie_gid, questionnarie_name," +
                       " questionnarie_type, questionnarie_answer," +
                       " form_type, status,mycampaign_status, created_by,created_date" +
                      "  )" +
                      " values(" +
                      "'" + values.campaign_gid + "'," +
                      "'" + values.questionnarie_gid + "'," +
                      "'" + values.questionnarie_name + "'," +
                      "'" + values.questionnarie_type + "'," +
                      "'" + values.questionnarie_answer + "'," +
                      "'" + values.form_type + "'," +
                      "'Pending' ," +
                      "'Pending for Approval' ," +
                      "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {


                        values.status = true;
                        values.message = "Single Form Added Successfully";


                        return true;
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured While saving MyCampaign";
                        return false;
                    }
                }
                else
                {
                    values.status = false;
                    return false;
                }
                
            }

            

        }
        public bool DaMyCampaignUpdate(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {

            msSQL = "update fnd_trn_tmycampaignsingle set " +
                    "questionnarie_answer =  '" + values.questionnarie_answer + "'," +
                    "updated_by = '" + employee_gid + "'," +
                    "updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    "where mycampaignsingle_gid = '" + values.mycampaignsingle_gid + "' " +
                    "and campaign_gid= '" + values.campaign_gid + "'" ;
            

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {


                values.status = true;
                values.message = "Single Form updated Successfully";


                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While updating Single form";
                return false;
            }

        }
        public bool DaMyCampaignMultipleUpdate(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {

            msSQL = "update fnd_trn_tmycampaignmultiple set " +
                    "header_values =  '" + values.questionnarie_answer + "'," +
                    "updated_by = '" + employee_gid + "'," +
                    "updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    "where mycampaignmultiple_gid = '" + values.mycampaignmultiple_gid + "' " +
                    "and campaign_gid= '" + values.campaign_gid + "'";


            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {


                values.status = true;
                values.message = "Single Form updated Successfully";


                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While updating Single form";
                return false;
            }

        }
        public void DaGetCampaignDetails(MdlTrnCampaign values, string campaign_gid)
        {



            try
            {

                msSQL = " select a.campaign_gid from fnd_trn_tcampaigndtl a where a.campaign_gid='" + campaign_gid + "' and a.form_type = 'S'";
                lsSingleformflag = objdbconn.GetExecuteScalar(msSQL);

                if (lsSingleformflag != "")
                {
                    msSQL = " select c.campaign_gid,a.campaigndtl_gid,a.questionnarie_gid, " +
                      "campaign_code,d.campaigntype_name,date_format(c.start_date,'%d-%m-%Y') as start_date ,date_format(c.end_date,'%d-%m-%Y') as end_date,date_format(c.assesment_date,'%d-%m-%Y') as assesment_date,e.customer_name," +
                      "c.contact_name, c.contact_mobile, c.contact_email," +
                      " c.campaign_name,a.questionnarie_type,a.questionnarie_answer, " +
                      " a.questionnarie_name ,a.importance,f.questionnarie_answer as singleform_answer," +
                      " c.campaign_status from fnd_trn_tcampaigndtl a " +
                      " left join fnd_mst_tquestionnarie b on a.questionnarie_gid = b.questionnarie_gid " +
                      " left join fnd_trn_tcampaign c on c.campaign_gid = a.campaign_gid " +
                      " left join fnd_mst_tcampaigntype d on d.campaigntype_gid = c.campaigntype_gid " +
                       " left join fnd_mst_tcustomer e on e.customer_gid = c.customer_gid " +
                       " left join fnd_trn_tmycampaignsingle f on f.questionnarie_gid = a.questionnarie_gid and f.campaign_gid =a.campaign_gid " +
                      " where a.campaign_gid='" + campaign_gid + "' and a.form_type = 'S'";
                }
                else
                {
                    msSQL = " select c.campaign_gid,a.campaigndtl_gid,a.questionnarie_gid, " +
                    " campaign_code,d.campaigntype_name,date_format(c.start_date,'%d-%m-%Y') as start_date ," +
                    " date_format(c.end_date,'%d-%m-%Y') as end_date,date_format(c.assesment_date,'%d-%m-%Y') as assesment_date,e.customer_name," +
                    " c.contact_name, c.contact_mobile, c.contact_email," +
                    " c.campaign_name, " +
                    " c.campaign_status from fnd_trn_tcampaigndtl a " +
                    " left join fnd_mst_tquestionnarie b on a.questionnarie_gid = b.questionnarie_gid " +
                    " left join fnd_trn_tcampaign c on c.campaign_gid = a.campaign_gid " +
                    " left join fnd_mst_tcampaigntype d on d.campaigntype_gid = c.campaigntype_gid " +
                     " left join fnd_mst_tcustomer e on e.customer_gid = c.customer_gid " +
                     " left join fnd_trn_tmycampaignsingle f on f.questionnarie_gid = a.questionnarie_gid and f.campaign_gid =a.campaign_gid " +
                    " where a.campaign_gid='" + campaign_gid + "'";
                }

            
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcampaign_details = new List<campaign_details>();
                if (lsSingleformflag != "")
                {
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {


                            var getanswerdesc_details = new List<answerdesc_list>();
                            if ((dr_datarow["questionnarie_type"].ToString() == "List"))
                            {
                                string[] ansdesc_sList ;
                                string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
                                 ansdesc_sList = answerdesc_list.Split(',');

                                foreach (string author in ansdesc_sList)
                                {
                                    getanswerdesc_details.Add(new answerdesc_list
                                    {
                                        name = author,

                                    });
                                }

                            }
                            if ((dr_datarow["questionnarie_type"].ToString() == "Radio Button") || (dr_datarow["questionnarie_type"].ToString() == "Radio_Button"))
                            {
                                string lsRadioAnswer = dr_datarow["singleform_answer"].ToString();

                                string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
                                string[] ansdesc_sList = answerdesc_list.Split(',');
                                bool radioAnswer;
                                foreach (string author in ansdesc_sList)
                                {
                                    if (lsRadioAnswer == author)
                                    { radioAnswer = true; }
                                    else { radioAnswer = false; }
                                    getanswerdesc_details.Add(new answerdesc_list
                                    {
                                        id = (dr_datarow["questionnarie_gid"].ToString()),
                                        name = author,
                                        check = radioAnswer,
                                    });
                                }

                            }
                            getcampaign_details.Add(new campaign_details
                            {
                                campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                                campaigndtl_gid = (dr_datarow["campaigndtl_gid"].ToString()),
                                questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
                                answer_type = (dr_datarow["questionnarie_type"].ToString()),
                                answer_desc = (dr_datarow["questionnarie_answer"].ToString()),
                                campaign_name = (dr_datarow["campaign_name"].ToString()),
                                question = (dr_datarow["questionnarie_name"].ToString()),
                                campaignrefno = dr_datarow["campaign_code"].ToString(),
                                campaign_type = dr_datarow["campaigntype_name"].ToString(),
                                customer_name = dr_datarow["customer_name"].ToString(),
                                contactperson_fn = dr_datarow["contact_name"].ToString(),
                                contactperson_mobile = dr_datarow["contact_mobile"].ToString(),
                                contactperson_email = dr_datarow["contact_email"].ToString(),
                                start_date = dr_datarow["start_date"].ToString(),
                                end_date = dr_datarow["end_date"].ToString(),
                                assesment_date = dr_datarow["assesment_date"].ToString(),
                                importance = (dr_datarow["importance"].ToString()),
                                singleform_answer = (dr_datarow["singleform_answer"].ToString()),
                                campaign_status = (dr_datarow["campaign_status"].ToString()),

                                answerdesc_list = getanswerdesc_details,
                            });

                        }
                        values.campaign_details = getcampaign_details;




                    }
                   
                }
                else
                {
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {


                            getcampaign_details.Add(new campaign_details
                            {
                                campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                                campaigndtl_gid = (dr_datarow["campaigndtl_gid"].ToString()),

                                campaign_name = (dr_datarow["campaign_name"].ToString()),

                                campaignrefno = dr_datarow["campaign_code"].ToString(),
                                campaign_type = dr_datarow["campaigntype_name"].ToString(),
                                customer_name = dr_datarow["customer_name"].ToString(),
                                contactperson_fn = dr_datarow["contact_name"].ToString(),
                                contactperson_mobile = dr_datarow["contact_mobile"].ToString(),
                                contactperson_email = dr_datarow["contact_email"].ToString(),
                                start_date = dr_datarow["start_date"].ToString(),
                                end_date = dr_datarow["end_date"].ToString(),
                                assesment_date = dr_datarow["assesment_date"].ToString(),

                                campaign_status = (dr_datarow["campaign_status"].ToString()),


                            });

                        }
                        values.campaign_details = getcampaign_details;




                    }
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void MyCampaignApprovedSubmit(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {
            try
            {
                msSQL = " select count(campaign_gid) as openquery from fnd_trn_tmycampaignraisequery where campaign_gid = '" + values.campaign_gid + "'" +
                                " and raisequery_status = 'Query Raised'";
                values.openquerycount = objdbconn.GetExecuteScalar(msSQL);
                if (values.openquerycount == "0")
                {
                    msSQL = " update fnd_trn_tcampaign set mycampaignapproval_remarks = '" + values.mycampaignapproval_remarks + "'" +
                        " ,campaign_status = 'Mycampaign closed' " +
                        " where campaign_gid='" + values.campaign_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    lsmycampaign_remarks = values.mycampaignapproval_remarks;

                    values.message = "My Campaign Form Approved";
                    values.status = true;

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

                    msSQL =
                           " select  group_concat(distinct a.employee_gid)  as CC2members, d.campaign_name, d.campaign_status, d.created_by, h.campaigntype_name, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_apr, d.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as employee_name  from fnd_mst_tcampaignapproving2employee a" +
                                " left join fnd_trn_tcampaign d on a.campaign_gid = d.campaign_gid" +
                                " left join fnd_mst_tcampaigntype h on h.campaigntype_gid = d.campaigntype_gid" +
                                " left join hrm_mst_temployee b on d.campaign_approver = b.employee_gid" +
                                " left join hrm_mst_temployee f on d.created_by = f.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                            " where a.campaign_gid ='" + values.campaign_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {

                        lsemployee_name = objODBCDatareader["employee_name"].ToString();
                        lsname = objODBCDatareader["campaign_apr"].ToString();
                        lcampaign_status = objODBCDatareader["campaign_status"].ToString();
                        lscampaign_type = objODBCDatareader["campaigntype_name"].ToString();
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lscampaign_name = objODBCDatareader["campaign_name"].ToString();
                        lsTo2members = objODBCDatareader["created_by"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                    lscc2members.Replace(employee_gid, "");
                    lscc2members.Replace(",,", ",");

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                    sub = "RE: Campaign Final Approval ";
                    body = "Dear All,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "A Campaign has been Approved. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "<b> Campaign Name :</b> " + HttpUtility.HtmlEncode(lscampaign_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Campaign Type :</b> " + HttpUtility.HtmlEncode(lscampaign_type) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Campaign Remarks :</b> " + HttpUtility.HtmlEncode(lsmycampaign_remarks) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Campaign Status :</b> " + HttpUtility.HtmlEncode(lcampaign_status) + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to view more details.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsname);
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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
                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                    {
                        lsToReceipients = lsto_mail.Split(',');
                        if (lsto_mail.Length == 0)
                        {
                            message.To.Add(new MailAddress(lsto_mail));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
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
                            msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                            " campaign_gid," +
                            " from_mail," +
                            " to_mail," +
                            " cc_mail," +
                            " mail_status," +                         
                            " mail_senddate, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + values.campaign_gid + "'," +
                            "'" + lsname + "'," +
                            "'" + lsto_mail + "'," +
                            "'" + cc_mailid + "'," +
                            "'Campaign Final Approval'," +                       
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                    values.message = "Approval Can't be done,the query is still open";
                   
                }
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void MyCampaignRejectSubmit(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {

            try
            {
                msSQL = " select count(campaign_gid) as openquery from fnd_trn_tmycampaignraisequery where campaign_gid = '" + values.campaign_gid + "'" +
                              " and raisequery_status = 'Query Raised'";
                values.openquerycount = objdbconn.GetExecuteScalar(msSQL);
                if (values.openquerycount == "0")
                {
                    msSQL = " update fnd_trn_tcampaign set mycampaignapproval_remarks = '" + values.mycampaignapproval_remarks + "'" +
                        " ,campaign_status = 'Mycampaign rejected' " +
                        " where campaign_gid='" + values.campaign_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    lsmycampaign_remarks = values.mycampaignapproval_remarks;

                    values.message = "My Campaign Form Rejected";
                    values.status = true;
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

                    msSQL =
                           " select  group_concat(distinct a.employee_gid)  as CC2members, d.campaign_name, d.campaign_status, d.created_by, h.campaigntype_name, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_apr, d.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as employee_name  from fnd_mst_tcampaignapproving2employee a" +
                                " left join fnd_trn_tcampaign d on a.campaign_gid = d.campaign_gid" +
                                " left join fnd_mst_tcampaigntype h on h.campaigntype_gid = d.campaigntype_gid" +
                                " left join hrm_mst_temployee b on d.campaign_approver = b.employee_gid" +
                                " left join hrm_mst_temployee f on d.created_by = f.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                            " where a.campaign_gid ='" + values.campaign_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {

                        lsemployee_name = objODBCDatareader["employee_name"].ToString();
                        lsname = objODBCDatareader["campaign_apr"].ToString();
                        lcampaign_status = objODBCDatareader["campaign_status"].ToString();
                        lscampaign_type = objODBCDatareader["campaigntype_name"].ToString();
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lscampaign_name = objODBCDatareader["campaign_name"].ToString();
                        lsTo2members = objODBCDatareader["created_by"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                    lscc2members.Replace(employee_gid, "");
                    lscc2members.Replace(",,", ",");

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                    sub = "RE: Campaign Final Rejected ";
                    body = "Dear All,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "A Campaign has been Rejected. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "<b> Campaign Name :</b> " + HttpUtility.HtmlEncode(lscampaign_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Campaign Type :</b> " + HttpUtility.HtmlEncode(lscampaign_type) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Campaign Remarks :</b> " + HttpUtility.HtmlEncode(lsmycampaign_remarks) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Campaign Status :</b> " + HttpUtility.HtmlEncode(lcampaign_status) + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to view more details.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsname);
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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
                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                    {
                        lsToReceipients = lsto_mail.Split(',');
                        if (lsto_mail.Length == 0)
                        {
                            message.To.Add(new MailAddress(lsto_mail));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
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
                            msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                            " campaign_gid," +
                            " from_mail," +
                            " to_mail," +
                            " cc_mail," +
                            " mail_status," +                       
                            " mail_senddate, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + values.campaign_gid + "'," +
                            "'" + lsname + "'," +
                            "'" + lsto_mail + "'," +
                            "'" + cc_mailid + "'," +
                            "'Campaign Final Rejected'," +                            
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                    values.message = "Reject Can't be done,the query is still open";

                }
            }
            catch (Exception ex)
            {
                values.status = false;
            }
            
            
        }
        public void CampaignFinalSubmit(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {
            try
            {


                msSQL = " update fnd_mst_tcampaignapproving2employee set mycampaign_status = 'Pending for Final Approval'" +
                        " where campaign_gid='" + values.campaign_gid + "' and employee_gid = '" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update fnd_trn_tcampaign set campaign_status = 'WIP'" +
                       " where campaign_gid='" + values.campaign_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                int lsCompleted_count = 0;
                int lsActual_count = 0;


                msSQL = " select count(campaign_gid) as completed_count from fnd_mst_tcampaignapproving2employee where campaign_gid = '" + values.campaign_gid + "' " +
                          "  and mycampaign_status = 'Pending for Final Approval' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsCompleted_count = Convert.ToInt16(objODBCDatareader["completed_count"].ToString());
                }
                objODBCDatareader.Close();
                msSQL = "  select count(campaign_gid) as actual_count from fnd_mst_tcampaignapproving2employee where campaign_gid = '" + values.campaign_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsActual_count = Convert.ToInt16(objODBCDatareader["actual_count"].ToString());
                }
                objODBCDatareader.Close();
                if (lsCompleted_count == lsActual_count)
                {
                    msSQL = "update fnd_trn_tcampaign set campaign_status = 'Pending for Final Approval' where campaign_gid='" + values.campaign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                values.message = "My Campaign Form Submitted Successfully";
                values.status = true;


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

                msSQL =
                       " select  group_concat(distinct a.employee_gid)  as CC2members, d.campaign_name, d.campaign_status, d.created_by, h.campaigntype_name, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_apr, d.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as employee_name  from fnd_mst_tcampaignapproving2employee a" +
                            " left join fnd_trn_tcampaign d on a.campaign_gid = d.campaign_gid" +
                            " left join fnd_mst_tcampaigntype h on h.campaigntype_gid = d.campaigntype_gid" +
                            " left join hrm_mst_temployee b on d.campaign_approver = b.employee_gid" +
                            " left join hrm_mst_temployee f on d.created_by = f.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                        " where a.campaign_gid ='" + values.campaign_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    lsemployee_name = objODBCDatareader["employee_name"].ToString();
                    lsname = objODBCDatareader["campaign_apr"].ToString();
                    lcampaign_status = objODBCDatareader["campaign_status"].ToString();
                    lscampaign_type = objODBCDatareader["campaigntype_name"].ToString();
                    lscc2members = objODBCDatareader["CC2members"].ToString();
                    lscampaign_name = objODBCDatareader["campaign_name"].ToString();
                    lsTo2members = objODBCDatareader["created_by"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                k = k + 1;


                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                lscc2members.Replace(employee_gid, "");
                lscc2members.Replace(",,", ",");

                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                sub = "RE: Campaign Approval ";
                body = "Dear All,<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "A Campaign has been submitted for Approval. The details are as follows, <br />";
                body = body + "<br />";
                body = body + "<b> Campaign Name :</b> " + HttpUtility.HtmlEncode(lscampaign_name) + "<br />";
                body = body + "<br />";
                body = body + "<b> Campaign Type :</b> " + HttpUtility.HtmlEncode(lscampaign_type) + "<br />";
                body = body + "<br />";
                //body = body + "<b>Campaign Approval Remarks :</b> " + lscampaign_remarks + "<br />";
                //body = body + "<br />";
                //body = body + "<b>Campaign Status :</b> " + lcampaign_status + "<br />";
                //body = body + "<br />";
                body = body + "Kindly log into systems to view more details.";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(lsname);
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));


                lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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
                if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                {
                    lsToReceipients = lsto_mail.Split(',');
                    if (lsto_mail.Length == 0)
                    {
                        message.To.Add(new MailAddress(lsto_mail));
                    }
                    else
                    {
                        foreach (string ToEmail in lsToReceipients)
                        {
                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
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
                    msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                    " campaign_gid," +
                    " from_mail," +
                    " to_mail," +
                    " cc_mail," +
                    " mail_status," +
                    " mail_senddate, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.campaign_gid + "'," +
                    "'" + lsname + "'," +
                    "'" + lsto_mail + "'," +
                    "'" + cc_mailid + "'," +
                    "'Campaign Approval'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }


            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }
                       
        
        public void GetCampaignMultipleDetails(MdlTrnCampaign values, string campaign_gid)
        {



            try
            {
                
                msSQL = " select c.campaign_gid,a.campaigndtl_gid,a.questionnarie_gid, " +
                         " campaign_code,d.campaigntype_name,c.start_date,c.end_date," +
                         " c.assesment_date,e.customer_name,c.contact_name, c.contact_mobile," +
                         " c.contact_email, c.campaign_name,a.questionnarie_type," +
                         " a.questionnarie_answer,  a.questionnarie_name ,a.importance" +
                         " from fnd_trn_tcampaigndtl a" +
                         " left join fnd_mst_tquestionnarie b on a.questionnarie_gid = b.questionnarie_gid" +
                         " left join fnd_trn_tcampaign c on c.campaign_gid = a.campaign_gid" +
                         " left join fnd_mst_tcampaigntype d on d.campaigntype_gid = c.campaigntype_gid" +
                         " left join fnd_mst_tcustomer e on e.customer_gid = c.campaigntype_gid" +
                         " where a.campaign_gid = '" + campaign_gid + "' and a.form_type = 'M'  order by questionnarie_gid asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmulticampaign_details = new List<multi_campaign_details>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {


                        var getmultianswerdesc_details = new List<multi_answerdesc_list>();
                        if ((dr_datarow["questionnarie_type"].ToString() == "List"))
                        {
                            string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
                            string[] ansdesc_sList = answerdesc_list.Split(',');

                            foreach (string author in ansdesc_sList)
                            {
                                getmultianswerdesc_details.Add(new multi_answerdesc_list
                                {
                                    name = author,

                                });
                            }

                        }
                        if ((dr_datarow["questionnarie_type"].ToString() == "Radio_Button") || (dr_datarow["questionnarie_type"].ToString() == "Radio Button"))
                        {
                            string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
                            string[] ansdesc_sList = answerdesc_list.Split(',');

                            foreach (string author in ansdesc_sList)
                            {
                                getmultianswerdesc_details.Add(new multi_answerdesc_list
                                {
                                    id = (dr_datarow["questionnarie_gid"].ToString()),
                                    name = author,

                                });
                            }

                        }
                        getmulticampaign_details.Add(new multi_campaign_details
                        {
                            campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                            campaigndtl_gid = (dr_datarow["campaigndtl_gid"].ToString()),
                            questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
                            answer_type = (dr_datarow["questionnarie_type"].ToString()),
                            answer_desc = (dr_datarow["questionnarie_answer"].ToString()),
                            campaign_name = (dr_datarow["campaign_name"].ToString()),
                            question = (dr_datarow["questionnarie_name"].ToString()),
                            campaignrefno = dr_datarow["campaign_code"].ToString(),
                            campaign_type = dr_datarow["campaigntype_name"].ToString(),
                            customer_name = dr_datarow["customer_name"].ToString(),
                            contactperson_fn = dr_datarow["contact_name"].ToString(),
                            contactperson_mobile = dr_datarow["contact_mobile"].ToString(),
                            contactperson_email = dr_datarow["contact_email"].ToString(),
                            start_date = dr_datarow["start_date"].ToString(),
                            end_date = dr_datarow["end_date"].ToString(),
                            assesment_date = dr_datarow["assesment_date"].ToString(),
                            importance = (dr_datarow["importance"].ToString()),
                            multi_answerdesc_list = getmultianswerdesc_details,
                        });

                    }
                    values.multi_campaign_details = getmulticampaign_details;




                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetCampaignApprovalclose(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {


            msSQL = "select a.campaign_gid,a.campaign_code,a.campaign_name,a.campaign_status,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
             "  date_format(a.assesment_date,'%d-%m-%Y %h:%i %p') as assesment_date , case when a.status='N' then 'Inactive' else 'Active' end as status, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
             " a.status_flag,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_manager,d.campaigntype_name,concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as campaign_approver  " +
             " from fnd_trn_tcampaign  a" +
             " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
               " left join hrm_mst_temployee f on a.campaign_approver = f.employee_gid" +
            " left join fnd_mst_tcampaigntype d on a.campaigntype_gid = d.campaigntype_gid" +       
            " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
           " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
            " where a.campaign_approver='" + employee_gid + "' and " +
            " ( a.campaign_status in ('Mycampaign closed') ) order by campaign_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmycampaign_list = new List<mycampaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmycampaign_list.Add(new mycampaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        assesment_date = (dr_datarow["assesment_date"].ToString()),
                        campaign_approver = (dr_datarow["campaign_approver"].ToString()),
                        campaign_manager = (dr_datarow["campaign_manager"].ToString()),
                        status = (dr_datarow["status"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        campaigntype_name = (dr_datarow["campaigntype_name"].ToString()),
                        status_flag = (dr_datarow["status_flag"].ToString()),

                    });
                }
                values.mycampaign_list = getmycampaign_list;
            }
            dt_datatable.Dispose();

        }
        public void DaGetSingleCampaignSummary(string employee_gid, MdlTrnCampaign values, string campaign_gid)
        {
            msSQL = " select a.campaign_gid,a.campaign_code,a.campaign_name," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " case when a.status='N' then 'Inactive' else 'Active' end as status," +
                    " case when a.campaign_status='NA' then 'Not yet Completed' when a.campaign_status='Pending' " +
                    " then 'Approval Pending' else 'Completed' end as campaign_status," +
                    " a.status_flag,b.customer_name ," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by, " +
                    " e.campaigntype_name" +
                    " from fnd_trn_tcampaign a left join fnd_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " left join fnd_mst_tcampaigntype e on a.campaigntype_gid = e.campaigntype_gid" +
                    " where campaign_gid = '" + campaign_gid +"'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampaign_list = new List<campaign_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcampaign_list.Add(new campaign_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaign_code = (dr_datarow["campaign_code"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        Status = (dr_datarow["status"].ToString()),
                        campaign_status = (dr_datarow["campaign_status"].ToString()),
                        status_flag = (dr_datarow["status_flag"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                         campaigntype_name = (dr_datarow["campaigntype_name"].ToString()),
                    });
                }
                values.campaign_list = getcampaign_list;

                msSQL = " select campaign_gid from fnd_trn_tcampaigndtl a where a.campaign_gid='" + campaign_gid + "' and a.form_type = 'S'";
                lsSingleformflag = objdbconn.GetExecuteScalar(msSQL);

                if (lsSingleformflag != "")
                {
                    msSQL = " select campaign_gid from fnd_trn_tmycampaignsingle where campaign_gid = '" + campaign_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    { values.lsFlag = "Y"; }
                    else { values.lsFlag = "N"; }
                    objODBCDatareader.Close();
                }
                else
                {
                    values.lsFlag = "Nothing";
                }

            }
            dt_datatable.Dispose();
        }
        public void DaGetMycampaignSingle(MdlTrnCampaign values, string campaign_gid, string employee_gid)
        {
            values.lsFlag = "N";
            msSQL = " select c.campaign_gid,a.campaigndtl_gid,a.questionnarie_gid, " +
                     "campaign_code,d.campaigntype_name,c.start_date,c.end_date,c.assesment_date,e.customer_name," +
                     "c.contact_name, c.contact_mobile, c.contact_email," +
                     " c.campaign_name,a.questionnarie_type,a.questionnarie_answer, " +
                     " a.questionnarie_name,f.questionnarie_answer as singleform_answer ,f.mycampaignsingle_gid,a.importance from fnd_trn_tcampaigndtl a " +
                     " left join fnd_mst_tquestionnarie b on a.questionnarie_gid = b.questionnarie_gid " +
                     " left join fnd_trn_tcampaign c on c.campaign_gid = a.campaign_gid " +
                     " left join fnd_mst_tcampaigntype d on d.campaigntype_gid = c.campaigntype_gid " +
                     " left join fnd_mst_tcustomer e on e.customer_gid = c.customer_gid " +
                     " left join fnd_trn_tmycampaignsingle f on f.questionnarie_gid = a.questionnarie_gid and f.campaign_gid =a.campaign_gid " +
                     " where a.campaign_gid='" + campaign_gid + "' and a.form_type = 'S'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcampaign_details = new List<campaign_details>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {


                    var getanswerdesc_details = new List<answerdesc_list>();
                    if ((dr_datarow["questionnarie_type"].ToString() == "List"))
                    {
                        string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
                        string[] ansdesc_sList = answerdesc_list.Split(',');

                        foreach (string author in ansdesc_sList)
                        {
                            getanswerdesc_details.Add(new answerdesc_list
                            {
                                name = author,

                            });
                        }

                    }
                    if ((dr_datarow["questionnarie_type"].ToString() == "Radio_Button") || (dr_datarow["questionnarie_type"].ToString() == "Radio Button"))
                    {
                        string lsRadioAnswer = dr_datarow["singleform_answer"].ToString();

                        string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
                        string[] ansdesc_sList = answerdesc_list.Split(',');
                        bool radioAnswer;
                        foreach (string author in ansdesc_sList)
                        {
                            if (lsRadioAnswer == author)
                            { radioAnswer = true; }
                            else { radioAnswer = false; }
                            getanswerdesc_details.Add(new answerdesc_list
                            {
                                id = (dr_datarow["questionnarie_gid"].ToString()),
                                name = author,
                                check = radioAnswer,
                            });
                        }

                    }
                    getcampaign_details.Add(new campaign_details
                    {

                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        campaigndtl_gid = (dr_datarow["campaigndtl_gid"].ToString()),
                        questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
                        answer_type = (dr_datarow["questionnarie_type"].ToString()),
                        answer_desc = (dr_datarow["questionnarie_answer"].ToString()),
                        campaign_name = (dr_datarow["campaign_name"].ToString()),
                        question = (dr_datarow["questionnarie_name"].ToString()),
                        campaignrefno = dr_datarow["campaign_code"].ToString(),
                        campaign_type = dr_datarow["campaigntype_name"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        contactperson_fn = dr_datarow["contact_name"].ToString(),
                        contactperson_mobile = dr_datarow["contact_mobile"].ToString(),
                        contactperson_email = dr_datarow["contact_email"].ToString(),
                        start_date = dr_datarow["start_date"].ToString(),
                        end_date = dr_datarow["end_date"].ToString(),
                        assesment_date = dr_datarow["assesment_date"].ToString(),
                        singleform_answer = (dr_datarow["singleform_answer"].ToString()),
                        importance = (dr_datarow["importance"].ToString()),
                        mycampaignsingle_gid = (dr_datarow["mycampaignsingle_gid"].ToString()),
                        answerdesc_list = getanswerdesc_details,
                    });

                }
                values.campaign_details = getcampaign_details;




            }
            dt_datatable.Dispose();
            values.status = true;

            msSQL = " select campaign_gid from fnd_trn_tcampaigndtl a where a.campaign_gid='" + campaign_gid + "' and a.form_type = 'S'";
            lsSingleformflag = objdbconn.GetExecuteScalar(msSQL);

            if (lsSingleformflag != "")
            {
                msSQL = " select campaign_gid from fnd_trn_tmycampaignsingle where campaign_gid = '" + campaign_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                { values.lsFlag = "Y"; }
                else { values.lsFlag = "N"; }
                objODBCDatareader.Close();
            }
            else
            {
                values.lsFlag = "Nothing";
            }

               


        }
        public void DaGetSingleformView(MdlTrnCampaign values, string campaign_gid, string employee_gid)
        {
            //msSQL = " select distinct c.campaigndtl_gid,  c.questionnarie_gid," +
            //         " c.questionnarie_type,  f.questionnarie_answer, " +
            //         " f.questionnarie_name," +
            //         " f.questionnarie_answer as singleform_answer, " +
            //         " d.categorytype_name from fnd_trn_tmycampaignsingle f " +
            //         " left join fnd_trn_tcampaign b on b.campaign_gid = f.campaign_gid " +
            //         " left join fnd_trn_tcampaigndtl c on c.campaign_gid = b.campaign_gid " +
            //         " left join fnd_mst_tcategorytype d on c.categorytype_gid = d.categorytype_gid " +
            //         " where f.campaign_gid= '" + campaign_gid + "'";

            msSQL = " select  f.questionnarie_answer," +
                    " f.questionnarie_name from fnd_trn_tmycampaignsingle f " +
                    " where f.campaign_gid= '" + campaign_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var singleform_list = new List<singleform_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    singleform_list.Add(new singleform_list
                    {
                        //questionnarie_gid = (dr_datarow["campaigndtl_gid"].ToString()),
                        //questionnarie_category = (dr_datarow["categorytype_name"].ToString()),
                        //questionnarie_type = (dr_datarow["questionnarie_type"].ToString()),
                        questionnarie_name = (dr_datarow["questionnarie_name"].ToString()),
                        questionnarie_answer = (dr_datarow["questionnarie_answer"].ToString()),

                    });
                }
                values.singleform_list = singleform_list;
            }
            dt_datatable.Dispose();

        }
        public void DaGeteditMycampaignMultiple(MdlTrnCampaign values, string campaign_gid,string reference_gid, string employee_gid)
        {
            values.lsFlag = "N";
         
            try
            {
                msSQL = " select c.campaign_gid,a.campaigndtl_gid,a.questionnarie_gid, " +
                    "campaign_code,d.campaigntype_name,c.start_date,c.end_date,c.assesment_date,e.customer_name," +
                    "c.contact_name, c.contact_mobile, c.contact_email," +
                        " c.campaign_name,a.questionnarie_type,a.questionnarie_answer, " +
                        " a.questionnarie_name,f.header_values as multipleform_answer ,f.mycampaignmultiple_gid ,a.importance from fnd_trn_tcampaigndtl a " +
                        " left join fnd_mst_tquestionnarie b on a.questionnarie_gid = b.questionnarie_gid " +
                        " left join fnd_trn_tcampaign c on c.campaign_gid = a.campaign_gid " +
                        " left join fnd_mst_tcampaigntype d on d.campaigntype_gid = c.campaigntype_gid " +
                         " left join fnd_mst_tcustomer e on e.customer_gid = c.campaigntype_gid " +
                          " left join fnd_trn_tmycampaignmultiple f on f.questionnarie_gid = a.questionnarie_gid and f.campaign_gid =a.campaign_gid " +
                     " and f.reference_gid = '" + reference_gid + "'" +
                        " where a.campaign_gid='" + campaign_gid + "' and a.form_type = 'M' order by f.reference_gid asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmulticampaign_details = new List<multi_campaign_details>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {


                        var getmultianswerdesc_details = new List<multi_answerdesc_list>();
                        if ((dr_datarow["questionnarie_type"].ToString() == "List"))
                        {
                            string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
                            string[] ansdesc_sList = answerdesc_list.Split(',');

                            foreach (string author in ansdesc_sList)
                            {
                                getmultianswerdesc_details.Add(new multi_answerdesc_list
                                {
                                    name = author,

                                });
                            }

                        }
                        if ((dr_datarow["questionnarie_type"].ToString() == "Radio_Button") || (dr_datarow["questionnarie_type"].ToString() == "Radio Button"))
                        {
                            string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
                            string[] ansdesc_sList = answerdesc_list.Split(',');

                            foreach (string author in ansdesc_sList)
                            {
                                getmultianswerdesc_details.Add(new multi_answerdesc_list
                                {
                                    id = (dr_datarow["questionnarie_gid"].ToString()),
                                    name = author,

                                });
                            }

                        }
                        getmulticampaign_details.Add(new multi_campaign_details
                        {
                            campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                            campaigndtl_gid = (dr_datarow["campaigndtl_gid"].ToString()),
                            questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
                            answer_type = (dr_datarow["questionnarie_type"].ToString()),
                            answer_desc = (dr_datarow["questionnarie_answer"].ToString()),
                            campaign_name = (dr_datarow["campaign_name"].ToString()),
                            question = (dr_datarow["questionnarie_name"].ToString()),
                            campaignrefno = dr_datarow["campaign_code"].ToString(),
                            campaign_type = dr_datarow["campaigntype_name"].ToString(),
                            customer_name = dr_datarow["customer_name"].ToString(),
                            contactperson_fn = dr_datarow["contact_name"].ToString(),
                            contactperson_mobile = dr_datarow["contact_mobile"].ToString(),
                            contactperson_email = dr_datarow["contact_email"].ToString(),
                            start_date = dr_datarow["start_date"].ToString(),
                            end_date = dr_datarow["end_date"].ToString(),
                            assesment_date = dr_datarow["assesment_date"].ToString(),
                            multi_answerdesc_list = getmultianswerdesc_details,
                            multipleform_answer = dr_datarow["multipleform_answer"].ToString(),
                            mycampaignmultiple_gid = dr_datarow["mycampaignmultiple_gid"].ToString(),
                            importance = (dr_datarow["importance"].ToString()),


                        });

                    }
                    values.multi_campaign_details = getmulticampaign_details;




                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }
        public void DaGetCampaignEditDetails(MdlFndTrnMyCampaignSummary values, string campaign_gid)
        {



            //try
            //{
            //    msSQL = " select c.campaign_gid,a.campaigndtl_gid,a.questionnarie_gid, " +
            //        "campaign_code,d.campaigntype_name,c.start_date,c.end_date,c.assesment_date,e.customer_name," +
            //        "c.contact_name, c.contact_mobile, c.contact_email," +
            //            " c.campaign_name,a.questionnarie_type,a.questionnarie_answer, " +
            //            " a.questionnarie_name from fnd_trn_tmycampaignsingle c " +
            //            " where a.campaign_gid='" + campaign_gid + "' and a.form_type = 'S'";
            //    dt_datatable = objdbconn.GetDataTable(msSQL);
            //    var getcampaign_details = new List<campaign_details>();

            //    if (dt_datatable.Rows.Count != 0)
            //    {
            //        foreach (DataRow dr_datarow in dt_datatable.Rows)
            //        {


            //            var getanswerdesc_details = new List<answerdesc_list>();
            //            if ((dr_datarow["questionnarie_type"].ToString() == "List"))
            //            {
            //                string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
            //                string[] ansdesc_sList = answerdesc_list.Split(',');

            //                foreach (string author in ansdesc_sList)
            //                {
            //                    getanswerdesc_details.Add(new answerdesc_list
            //                    {
            //                        name = author,

            //                    });
            //                }

            //            }
            //            if ((dr_datarow["questionnarie_type"].ToString() == "Radio_Button"))
            //            {
            //                string answerdesc_list = dr_datarow["questionnarie_answer"].ToString();
            //                string[] ansdesc_sList = answerdesc_list.Split(',');

            //                foreach (string author in ansdesc_sList)
            //                {
            //                    getanswerdesc_details.Add(new answerdesc_list
            //                    {
            //                        id = (dr_datarow["questionnarie_gid"].ToString()),
            //                        name = author,

            //                    });
            //                }

            //            }
            //            getcampaign_details.Add(new campaign_details
            //            {
            //                campaign_gid = (dr_datarow["campaign_gid"].ToString()),
            //                campaigndtl_gid = (dr_datarow["campaigndtl_gid"].ToString()),
            //                questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
            //                answer_type = (dr_datarow["questionnarie_type"].ToString()),
            //                answer_desc = (dr_datarow["questionnarie_answer"].ToString()),
            //                campaign_name = (dr_datarow["campaign_name"].ToString()),
            //                question = (dr_datarow["questionnarie_name"].ToString()),
            //                campaignrefno = dr_datarow["campaign_code"].ToString(),
            //                campaign_type = dr_datarow["campaigntype_name"].ToString(),
            //                customer_name = dr_datarow["customer_name"].ToString(),
            //                contactperson_fn = dr_datarow["contact_name"].ToString(),
            //                contactperson_mobile = dr_datarow["contact_mobile"].ToString(),
            //                contactperson_email = dr_datarow["contact_email"].ToString(),
            //                start_date = dr_datarow["start_date"].ToString(),
            //                end_date = dr_datarow["end_date"].ToString(),
            //                assesment_date = dr_datarow["assesment_date"].ToString(),
            //                answerdesc_list = getanswerdesc_details,
            //            });

            //        }
            //        values.campaign_details = getcampaign_details;




            //    }
            //    dt_datatable.Dispose();
            //    values.status = true;
            //}
            //catch (Exception ex)
            //{
            //    values.status = false;
            //}
        }
        public void DaFutureDateCheck(string date, result values)
        {
            DateTime documentdate = DateTime.Parse(Convert.ToDateTime(date).ToShortDateString());
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            if (documentdate > nowdate)
            {
                values.status = false;
                values.message = "Future Date is Not Allowed...";
            }
            else
            {
                values.status = true;
            }
        }

        public void DaPastDateCheck(string date, result values)
        {
            DateTime documentdate = DateTime.Parse(Convert.ToDateTime(date).ToShortDateString());
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            if (documentdate < nowdate)
            {
                values.status = false;
                values.message = "Past Date is Not Allowed...";
            }
            else
            {
                values.status = true;
            }
        }
        public void DaGetMycampaignMultiple(sampledynamicdatadtl values, string campaign_gid, string employee_gid)
        {
            //         msSQL = " select distinct * from " +
            //         " (select  a.mycampaignmultiple_gid, a.questionnarie_gid, a.reference_gid," +
            //         " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by ," +
            //         " a.header_names as questionnarie_name, a.header_values as questionnarie_answer " +
            //         " from fnd_trn_tmycampaignmultiple a " +
            //         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
            //         " left join fnd_trn_tcampaigndtl d on d.campaign_gid = a.campaign_gid " +
            //         " where a.campaign_gid = '" + campaign_gid + "' " +
            //         " and a.created_by  in ('" + employee_gid + "')  order by  a.questionnarie_gid asc) as a group by a.reference_gid  ";

            //dt_datatable = objdbconn.GetDataTable(msSQL);
            // var getmultipleformanswer_list = new List<multipleformanswer_list>();
            // if (dt_datatable.Rows.Count != 0)
            // {
            //     foreach (DataRow dr_datarow in dt_datatable.Rows)
            //     {
            //         getmultipleformanswer_list.Add(new multipleformanswer_list
            //         {

            //             quest_Id = (dr_datarow["questionnarie_gid"].ToString()),
            //             quest_name = (dr_datarow["questionnarie_name"].ToString()),
            //             quest_answer = (dr_datarow["questionnarie_answer"].ToString()),
            //             mycampaignmultiple_gid = (dr_datarow["mycampaignmultiple_gid"].ToString()),
            //             reference_Id = (dr_datarow["reference_gid"].ToString()),
            //             created_by = (dr_datarow["created_by"].ToString()),

            //         });
            //     }
            //     values.multipleformanswer_list = getmultipleformanswer_list;
            // }
            // dt_datatable.Dispose();

            msSQL = "CALL GetMultipleformdata ('" + campaign_gid + "')";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                Query = objODBCDatareader["sqlquery"].ToString();
            }
            objODBCDatareader.Close();
            if(Query != "")
            {
                msSQL = Query + ", concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Created_by, " +
                            " a.reference_gid as Reference_No" +
                           " from fnd_trn_tmycampaignmultiple a " +
                           " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                           " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                           " where a.campaign_gid = '" + campaign_gid + "' and " +
                           " a.created_by  in ('" + employee_gid + "')   group by Reference_No order by a.mycampaignmultiple_gid asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                    Dictionary<string, object> childRow;
                    foreach (DataRow row in dt_datatable.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in dt_datatable.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                    string json = jsSerializer.Serialize(parentRow);
                    values.JSONdata = json;
                    values.status = true;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            else
            {
                values.status = false;
            }



        }
        public void DaGetMycampaignTeamActivity(sampledynamicdatadtl values, string campaign_gid, string employee_gid)
        {

            //msSQL = " select distinct * from " +
            //        " (select a.mycampaignmultiple_gid,a.questionnarie_gid," +
            //        " a.reference_gid,a.header_names as questionnarie_name," +
            //        " a.header_values as questionnarie_answer , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
            //        " from  fnd_trn_tmycampaignmultiple a " +
            //        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
            //        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
            //        " where a.campaign_gid = '" + campaign_gid + "' and " +
            //        " a.created_by not in ('" + employee_gid + "')   order by questionnarie_gid asc) as a group by a.reference_gid ";


            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getmultipleformTeamanswer_list = new List<multipleformTeamanswer_list>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dr_datarow in dt_datatable.Rows)
            //    {
            //        getmultipleformTeamanswer_list.Add(new multipleformTeamanswer_list
            //        {

            //            quest_Id = (dr_datarow["questionnarie_gid"].ToString()),
            //            quest_name = (dr_datarow["questionnarie_name"].ToString()),
            //            quest_answer = (dr_datarow["questionnarie_answer"].ToString()),
            //            mycampaignmultiple_gid = (dr_datarow["mycampaignmultiple_gid"].ToString()),
            //            reference_Id = (dr_datarow["reference_gid"].ToString()),
            //            created_by = (dr_datarow["created_by"].ToString()),

            //        });
            //    }
            //    values.multipleformTeamanswer_list = getmultipleformTeamanswer_list;
            //}
            //dt_datatable.Dispose();
            msSQL = "CALL GetMultipleformdata ('" + campaign_gid + "')";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                Query = objODBCDatareader["sqlquery"].ToString();
            }
            objODBCDatareader.Close();
            if (Query !="")
            {
                msSQL = Query + ", concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Created_by,a.reference_gid as Reference_No" +
                           " from fnd_trn_tmycampaignmultiple a " +
                           " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                           " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                           " where a.campaign_gid = '" + campaign_gid + "' and " +
                           " a.created_by not in ('" + employee_gid + "')   group by Reference_No  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                    Dictionary<string, object> childRow;
                    foreach (DataRow row in dt_datatable.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in dt_datatable.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                    string json = jsSerializer.Serialize(parentRow);
                    values.JSONdata = json;
                    values.status = true;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            else
            {
                values.status = false;
            }


            

        
            
}
        public void DaGetMycampaignApprovalTeamActivity(MdlFndTrnMyCampaignSummary values, string campaign_gid, string employee_gid)
        {

            msSQL = " select distinct a.mycampaignmultiple_gid,a.questionnarie_gid,a.reference_gid,a.questionnarie_name,a.questionnarie_answer,a.created_by from " +
                    " (select a.mycampaignmultiple_gid,a.questionnarie_gid," +
                    " a.reference_gid,a.header_names as questionnarie_name," +
                    " a.header_values as questionnarie_answer , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                    " from  fnd_trn_tmycampaignmultiple a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                    " where a.campaign_gid = '" + campaign_gid + "' order by questionnarie_gid asc) as a group by a.reference_gid ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmultipleformTeamanswer_list = new List<multipleformTeamanswer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmultipleformTeamanswer_list.Add(new multipleformTeamanswer_list
                    {

                        quest_Id = (dr_datarow["questionnarie_gid"].ToString()),
                        quest_name = (dr_datarow["questionnarie_name"].ToString()),
                        quest_answer = (dr_datarow["questionnarie_answer"].ToString()),
                        mycampaignmultiple_gid = (dr_datarow["mycampaignmultiple_gid"].ToString()),
                        reference_Id = (dr_datarow["reference_gid"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),

                    });
                }
                values.multipleformTeamanswer_list = getmultipleformTeamanswer_list;
            }
            dt_datatable.Dispose();

        }
        public bool DaMyCampaignExcelSubmit(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {
            MemoryStream memoryStream = new MemoryStream();
            string path = values.file_path;
            FileInfo newFile = new FileInfo(path);
            ExcelPackage pck = new ExcelPackage(newFile);
          

            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                var ws = xlPackage.Workbook.Worksheets.Add("Content");
                string celval = ws.Cells[1, 1].ToString();
                ExcelWorkbook workbook = xlPackage.Workbook;
              //  ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("QuestionaryList");
                //for (int i = 0; i <= dt_datatable.Rows.Count - 1; i++)
                //{
                //    worksheet.Cells[1, j].Value = dt_datatable.Rows[i].ItemArray[15];
                //    j = j + 1;
                //}


            }
          

            msSQL = " insert into fnd_trn_tmycampaignmultiple ( " +
                    " campaign_gid,reference_gid," +
                    " questionnarie_gid, header_names," +
                    " header_values," +
                    " created_by,created_date" +
                   "  )" +
                   " values(" +
                   "'" + values.campaign_gid + "'," +
                   "'" + values.form_type + "'," +
                   "'" + values.questionnarie_gid + "'," +
                   "'" + values.questionnarie_name.Replace("_", " ") + "'," +
                   "'" + values.questionnarie_answer + "'," +
                   "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " update fnd_mst_tcampaignapproving2employee set mycampaign_status = 'Pending for Final Approval' " +
                        " where campaign_gid='" + values.campaign_gid + "' and employee_gid = '" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Multiple Form Added Successfully";


                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Saving Multiple Form";
                return false;
            }
        }
        public bool DaMyCampaignMultipleSubmit(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {


            msSQL = " insert into fnd_trn_tmycampaignmultiple ( " +
                    " campaign_gid,reference_gid," +
                    " questionnarie_gid, header_names," +
                    " header_values," +
                    " created_by,created_date" +
                   "  )" +
                   " values(" +
                   "'" + values.campaign_gid + "'," +
                   "'" + values.form_type + "'," +
                   "'" + values.questionnarie_gid + "'," +
                   "'" + values.questionnarie_name.Replace("_", " ") + "'," +
                   "'" + values.questionnarie_answer + "'," +
                   "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " update fnd_mst_tcampaignapproving2employee set mycampaign_status = 'Pending for Final Approval' " +
                        " where campaign_gid='" + values.campaign_gid + "' and employee_gid = '" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Multiple Form Added Successfully";


                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Saving Multiple Form";
                return false;
            }
        }
        public void DaGetSampleDynamicdata(string campaign_gid, sampledynamicdatadtl values, string employee_gid)
        {
            try
            {               
                msSQL = "CALL GetMultipleformdata ('" + campaign_gid + "')";
              
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    Query = objODBCDatareader["sqlquery"].ToString();
                }
                objODBCDatareader.Close();
                if (Query !="")
                {
                    msSQL = Query + ", concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Created_by,a.reference_gid " +
                               " from fnd_trn_tmycampaignmultiple a " +
                               " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                               " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                               " where campaign_gid = '" + campaign_gid + "' GROUP BY a.reference_gid ";


                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                        Dictionary<string, object> childRow;
                        foreach (DataRow row in dt_datatable.Rows)
                        {
                            childRow = new Dictionary<string, object>();
                            foreach (DataColumn col in dt_datatable.Columns)
                            {
                                childRow.Add(col.ColumnName, row[col]);
                            }
                            parentRow.Add(childRow);
                        }
                        string json = jsSerializer.Serialize(parentRow);
                        values.JSONdata = json;
                        values.status = true;
                    }
                    dt_datatable.Dispose();

                    values.status = true;
                }
                else
                {
                    values.status = false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetEmployeeName(string campaign_gid, MdlFndTrnMyCampaignSummary values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from fnd_mst_tcampaignapproving2employee " +
                  " where campaign_gid='" + campaign_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.employee_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
           

        }

        public void DaPostMyCampaignRaiseQuery(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {


            msGetGid = objcmnfunctions.GetMasterGID("MCRQ");
            msSQL = "Insert into fnd_trn_tmycampaignraisequery( " +
                   " mycampaignraisequery_gid, " +
                   " campaign_gid," +
                   " query_title, " +
                   " query_description," +
                   " raisequery_status, " +
                   " manager_gid, " +
                   " manager_name, " +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.campaign_gid + "', " +
                   "'" + values.query_title.Replace("'", "") + "'," +
                    "'" + values.query_description.Replace("'", "") + "'," +
                   "'Query Raised'," +
                   "'" + values.manager_gid + "', " +
                   "'" + values.manager_name.Replace("'", "") + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
               
                values.status = true;
                values.message = "Query Raised  Successfully";

                lsTo2members = values.manager_gid;

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

                msSQL =
                       " select  distinct (a.created_by)  as CC2members ,a.campaign_name, a.created_by, h.campaigntype_name, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_apr, " +
                       " a.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as employee_name  from fnd_trn_tcampaign a" +                        
                            " left join fnd_mst_tcampaigntype h on h.campaigntype_gid = a.campaigntype_gid" +
                            " left join hrm_mst_temployee b on a.campaign_approver = b.employee_gid" +
                            " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                        " where a.campaign_gid ='" + values.campaign_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    lsemployee_name = objODBCDatareader["employee_name"].ToString();
                    //lsname = objODBCDatareader["campaign_apr"].ToString();    
                    lscampaign_name = objODBCDatareader["campaign_name"].ToString();
                    lscc2members = objODBCDatareader["CC2members"].ToString();
                    lscampaign_type = objODBCDatareader["campaigntype_name"].ToString();
                    //lsTo2members = objODBCDatareader["created_by"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                        " where employee_gid = '" + employee_gid + "'";
                string employee_name = objdbconn.GetExecuteScalar(msSQL);

                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                k = k + 1;


                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                lscc2members.Replace(lsTo2members, "");
                lscc2members.Replace(",,", ",");

                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                sub = "RE: Query Raised ";
                body = "Dear Sir,<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "Query has been Raised. The details are as follows, <br />";
                body = body + "<br />";
                body = body + "<b> Campaign Name :</b> " + HttpUtility.HtmlEncode(lscampaign_name) + "<br />";
                body = body + "<br />";
                body = body + "<b> Campaign Type :</b> " + HttpUtility.HtmlEncode(lscampaign_type) + "<br />";
                body = body + "<br />";
                body = body + "Kindly log into systems to view more details.";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(employee_name);
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));


                lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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
                if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                {
                    lsToReceipients = lsto_mail.Split(',');
                    if (lsto_mail.Length == 0)
                    {
                        message.To.Add(new MailAddress(lsto_mail));
                    }
                    else
                    {
                        foreach (string ToEmail in lsToReceipients)
                        {
                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
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
                        msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                        " campaign_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                          " raisequery_gid, " +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.campaign_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Raised Successfully'," +
                           "'" + msGetGid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                values.message = "Error Occured While Adding";
                values.status = false;
            }
        }
        public void DaGetMyCampaignApprovalRaiseQuery(string campaign_gid, MdlFndTrnMyCampaignSummary values, string employee_gid)
        {


            msSQL = " select distinct a.campaign_gid,a.mycampaignraisequery_gid,a.manager_gid,a.manager_name,b.campaign_approver,a.query_title,a.query_description,a.raisequery_status,a.queryresponse_remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as query_responseby " +
                    " from fnd_trn_tmycampaignraisequery a " +
                     " left join fnd_trn_tcampaign b on a.campaign_gid = b.campaign_gid" +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                     " left join hrm_mst_temployee e on a.query_responseby = e.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
                    " where a.campaign_gid = '" + campaign_gid + "' and (a.manager_gid = '" + employee_gid + "' or b.campaign_approver = '" + employee_gid + "')   group by mycampaignraisequery_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmycampiagnraisequery_list = new List<mycampiagnraisequery_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmycampiagnraisequery_list.Add(new mycampiagnraisequery_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        mycampaignraisequery_gid = (dr_datarow["mycampaignraisequery_gid"].ToString()),
                        manager_gid = (dr_datarow["manager_gid"].ToString()),
                        manager_name = (dr_datarow["manager_name"].ToString()),
                        query_title = (dr_datarow["query_title"].ToString()),
                        query_description = (dr_datarow["query_description"].ToString()),
                        queryresponse_remarks = (dr_datarow["queryresponse_remarks"].ToString()),
                        query_responseby = (dr_datarow["query_responseby"].ToString()),
                        raisequery_status = (dr_datarow["raisequery_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.mycampiagnraisequery_list = getmycampiagnraisequery_list;
            }

            dt_datatable.Dispose();

        }
        public void DaGetMyCampaignRaiseQuery(string campaign_gid, MdlFndTrnMyCampaignSummary values, string employee_gid)
        {


            msSQL = " select distinct a.campaign_gid,a.mycampaignraisequery_gid,a.manager_gid,a.manager_name,b.campaign_approver,a.query_title,a.query_description,a.raisequery_status,a.queryresponse_remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by, " +
                     " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as query_responseby " +
                    " from fnd_trn_tmycampaignraisequery a " +
                     " left join fnd_trn_tcampaign b on a.campaign_gid = b.campaign_gid" +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                     " left join hrm_mst_temployee e on a.query_responseby = e.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
                    " where a.campaign_gid = '" + campaign_gid + "' and (a.manager_gid = '" + employee_gid + "' )   group by mycampaignraisequery_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmycampiagnraisequery_list = new List<mycampiagnraisequery_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmycampiagnraisequery_list.Add(new mycampiagnraisequery_list
                    {
                        campaign_gid = (dr_datarow["campaign_gid"].ToString()),
                        mycampaignraisequery_gid = (dr_datarow["mycampaignraisequery_gid"].ToString()),
                        manager_gid = (dr_datarow["manager_gid"].ToString()),
                        manager_name = (dr_datarow["manager_name"].ToString()),
                        query_title = (dr_datarow["query_title"].ToString()),
                        query_description = (dr_datarow["query_description"].ToString()),
                        queryresponse_remarks = (dr_datarow["queryresponse_remarks"].ToString()),
                        query_responseby = (dr_datarow["query_responseby"].ToString()),
                        raisequery_status = (dr_datarow["raisequery_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.mycampiagnraisequery_list = getmycampiagnraisequery_list;
            }

            dt_datatable.Dispose();

        }

        public void DaPostMyCampaignresponsequery(MdlFndTrnMyCampaignSummary values, string employee_gid)
        {

            msSQL = " update fnd_trn_tmycampaignraisequery set queryresponse_remarks ='" + values.queryresponse_remarks.Replace("'", "") + "'," +
                   " query_responseby='" + employee_gid + "'," +
                   " query_responsedate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " raisequery_status='Closed' " +
                   " where mycampaignraisequery_gid='" + values.mycampaignraisequery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Query Closed Successfully..!";

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

                msSQL =
                       " select  distinct (a.created_by)  as CC2members ,a.campaign_name,a.campaign_approver, a.created_by, h.campaigntype_name, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as campaign_apr, " +
                       " a.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as employee_name  from fnd_trn_tcampaign a" +
                            " left join fnd_mst_tcampaignapproving2employee d on a.campaign_gid = d.campaign_gid" +
                            " left join fnd_mst_tcampaigntype h on h.campaigntype_gid = a.campaigntype_gid" +
                            " left join hrm_mst_temployee b on a.campaign_approver = b.employee_gid" +
                            " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                        " where a.campaign_gid ='" + values.campaign_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    lsemployee_name = objODBCDatareader["employee_name"].ToString();
                    //lsname = objODBCDatareader["campaign_apr"].ToString();    
                    lscampaign_name = objODBCDatareader["campaign_name"].ToString();
                    lscc2members = objODBCDatareader["CC2members"].ToString();
                    lscampaign_type = objODBCDatareader["campaigntype_name"].ToString();
                    lsTo2members = objODBCDatareader["campaign_approver"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                        " where employee_gid = '" + employee_gid + "'";
                string employee_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select query_title from fnd_trn_tmycampaignraisequery  " +
                    " where campaign_gid = '" + values.campaign_gid + "'";
                string lsquery_title = objdbconn.GetExecuteScalar(msSQL);


                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                k = k + 1;


                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                lscc2members.Replace(employee_gid, "");
                lscc2members.Replace(",,", ",");

                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                sub = "RE: Query Response ";
                body = "Dear Sir,<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "Query has been Response. The details are as follows, <br />";
                body = body + "<br />";
                body = body + "<b> Campaign Name :</b> " + HttpUtility.HtmlEncode(lscampaign_name) + "<br />";
                body = body + "<br />";
                body = body + "<b> Campaign Type :</b> " + HttpUtility.HtmlEncode(lscampaign_type) + "<br />";
                body = body + "<br />";
                body = body + "<b> Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title) + "<br />";
                body = body + "<br />";
                body = body + "Kindly log into systems to view more details.";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(employee_name);
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));


                lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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
                if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                {
                    lsToReceipients = lsto_mail.Split(',');
                    if (lsto_mail.Length == 0)
                    {
                        message.To.Add(new MailAddress(lsto_mail));
                    }
                    else
                    {
                        foreach (string ToEmail in lsToReceipients)
                        {
                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
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
                        msSQL = "Insert into fnd_trn_tfoundationmailcount( " +
                        " campaign_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                          " raisequery_gid, " +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.campaign_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Close Successfully'," +
                        "'" + values.mycampaignraisequery_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                values.message = "Error Occured..!";
            }
        }
        public void DaGetCampaignManager(string campaign_gid, MdlFndTrnMyCampaignSummary values)
        {
            try
            {
                msSQL = "select a.employee_name,a.employee_gid,campaign_gid from fnd_mst_tcampaignapproving2employee a " +
                                 "where a.campaign_gid = '" + campaign_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcampaignmanager_list = new List<campaignmanager_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcampaignmanager_list.Add(new campaignmanager_list
                        {
                            manager_gid = (dr_datarow["employee_gid"].ToString()),
                            manager_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.campaignmanager_list = getcampaignmanager_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetManagerEmployeeName(string campaignmanager2employee_gid, MdlFndTrnMyCampaignSummary values)
        {
            msSQL = " select group_concat(manager_name) as manager_name  from fnd_trn_tcampaignmanager2employee " +
                  " where campaignmanager2employee_gid='" + campaignmanager2employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.manager_name = objODBCDatareader["manager_name"].ToString();
            }
            objODBCDatareader.Close();


        }

        public void DaExportSingleMultipleFormDetails(SingleMultiFormReport objSingleMultiFormReport, string campaign_gid, string employee_gid)
        {
            string msSQL, lscompany_code, lsfilePath;
            MemoryStream ms = new MemoryStream();
            ExcelPackage xlPackage = new ExcelPackage(ms);         
            DataTable dtExcel = new DataTable();
            DataRow dr = dtExcel.NewRow();
            DataColumn dc = new DataColumn();
            FileInfo file;           
            string Filepath = string.Empty;

            msSQL = " select company_code from adm_mst_tcompany";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);

            objSingleMultiFormReport.lsname = "SingleMultiFormReport.xlsx";
            var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Foundation/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            objSingleMultiFormReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Foundation/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objSingleMultiFormReport.lsname;
            objSingleMultiFormReport.lscloudpath = lscompany_code + "/" + "Foundation/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objSingleMultiFormReport.lsname;
            bool exists = System.IO.Directory.Exists(path);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(path);
            }

            // Create Directory


            //Filepath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/Foundation/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            //path = Filepath + "\\CampaignApproval.xlsx";

            //if ((!System.IO.Directory.Exists(Filepath)))
            //    System.IO.Directory.CreateDirectory(Filepath);
            // file = new FileInfo(path);

            ////Filepath = HttpContext.Current.Server.MapPath("../../../templates");

            //if ((!System.IO.Directory.Exists(Filepath)))
            //    System.IO.Directory.CreateDirectory(Filepath);

          
            //file = new FileInfo(path);
            file = new FileInfo(objSingleMultiFormReport.lspath);
            try
            {
                msSQL = " select  f.questionnarie_answer," +
                    " f.questionnarie_name from fnd_trn_tmycampaignsingle f " +
                    " where f.campaign_gid= '" + campaign_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    int j = 1;

                   
                        ExcelWorkbook workbook = xlPackage.Workbook;
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("SingleFormList");

                    //worksheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);

                    for (int i = 0; i <= dt_datatable.Rows.Count - 1; i++)
                    {
                        //  worksheet.Cells[1, 1].Value = dt_datatable.Rows[i].ItemArray[15].ToString();
                        worksheet.Cells[1, 1].Value = "S.No";
                        worksheet.Cells[1, 2].Value = "Questionnarie Title";
                        worksheet.Cells[1, 3].Value = "Questionnarie Answer";
                        worksheet.Cells[i + 2, 1].Value = j;
                        worksheet.Cells[i + 2, 2].Value = dt_datatable.Rows[i].ItemArray[1].ToString();
                        worksheet.Cells[i + 2, 3].Value = dt_datatable.Rows[i].ItemArray[0].ToString();
                        j = j + 1;

                    }

                    
                    //using (var range = worksheet.Cells[1, 1, 1, 3])  //Address "A1:A3"

                    //{
                    //    range.Style.Font.Bold = true;
                    //    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    //    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    //    range.Style.Font.Color.SetColor(Color.White);
                    //}
                  //  xlPackage.SaveAs(file);

                    //}
                }

                    //MultiFormExcelWrite(campaign_gid);
                    ms = new MemoryStream();
                    //  xlPackage = new ExcelPackage(ms);

                    try
                    {
                        msSQL = "CALL GetMultipleformdata ('" + campaign_gid + "')";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            Query = objODBCDatareader["sqlquery"].ToString();
                        }
                        objODBCDatareader.Close();
                        if (Query != "")
                        {
                            msSQL = Query + ", concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Created_by,a.reference_gid " +
                                       " from fnd_trn_tmycampaignmultiple a " +
                                       " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                                       " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                                       " where campaign_gid = '" + campaign_gid + "' GROUP BY a.reference_gid ";


                            dt_tab = objdbconn.GetDataTable(msSQL);
                            if (dt_tab.Rows.Count != 0)
                            {
                                int j = 1;
                                
                                ExcelWorkbook workbook = xlPackage.Workbook;
                                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("MultiFormList");

                            //for (int i = 0; i <= dt_tab.Rows.Count - 1; i++)
                            //{                                    
                            //    worksheet.Cells[1, 1].Value = "S.No";
                            //    worksheet.Cells[1, 2].Value = "Name of the Person";
                            //    worksheet.Cells[1, 3].Value = "Age of the Person";
                            //    worksheet.Cells[1, 4].Value = "DOB";
                            //    worksheet.Cells[i + 2, 1].Value = j;
                            //    worksheet.Cells[i + 2, 2].Value = dt_tab.Rows[i].ItemArray[2].ToString();
                            //    worksheet.Cells[i + 2, 3].Value = dt_tab.Rows[i].ItemArray[0].ToString();
                            //    worksheet.Cells[i + 2, 4].Value = dt_tab.Rows[i].ItemArray[1].ToString();

                            //    j = j + 1;

                            //}
                            //worksheet.Cells[1, 1].LoadFromDataTable(dt_tab, true);
                            //using (var range = worksheet.Cells[1, 1, 1, 10])  //Address "A1:A10"

                            //{
                            //    range.Style.Font.Bold = true;
                            //    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            //    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                            //    range.Style.Font.Color.SetColor(Color.White);
                            //}


                            
                        }

                    }
                    }
                    catch (Exception ex)
                    {

                    }
                if (dt_datatable.Rows.Count == 0 && dt_tab.Rows.Count == 0)
                {
                   // values.status = false;
                }
                else
                {                   
                    xlPackage.SaveAs(ms);
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Foundation/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objSingleMultiFormReport.lsname, ms);
                    ms.Close();

                    //convert the excel package to a byte array
                    //byte[] bin = xlPackage.GetAsByteArray();

                    ////write the file to the disk
                    //File.WriteAllBytes(path, bin);
                    //if (file.Exists)
                    //{
                    //    mnResult = 1;
                    //   // values.status = true;
                    //    string message= "Exported Succesfully @" + path;
                    ////    values.message = message;

                    //}
                    //else
                    //{
                    //   // values.status = false;
                    //    mnResult = 0;
                    //    //values.message = "Export Failed";

                    //}
                    if (mnResult==1)
                    {
                      
                    }
                    else
                    {
                        
                    }
                    
                }

            }
            catch (Exception ex)
            {
                objSingleMultiFormReport.status = false;
                objSingleMultiFormReport.message = "Failure";
            }
            objSingleMultiFormReport.status = true;
            objSingleMultiFormReport.message = "Success";
        }
        public ExcelPackage MultiFormExcelWrite(string campaign_gid)
        {
            MemoryStream ms = new MemoryStream();
            ExcelPackage xlPackage = new ExcelPackage(ms);
            string msSQL;
            try
            {
                msSQL = "CALL GetMultipleformdata ('" + campaign_gid + "')";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    Query = objODBCDatareader["sqlquery"].ToString();
                }
                objODBCDatareader.Close();
                if (Query != "")
                {
                    msSQL = Query + ", concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Created_by,a.reference_gid " +
                               " from fnd_trn_tmycampaignmultiple a " +
                               " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                               " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                               " where campaign_gid = '" + campaign_gid + "' GROUP BY a.reference_gid ";


                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int j = 1;
                        //string path = @"E:\\Web\\EMS\\templates\\CampaignApproval.xlsx";
                        string path = ConfigurationManager.AppSettings["file_path"] + "/templates/CampaignApproval.xlsx";
                        FileInfo file = new FileInfo(path);
                       // file.Open(FileMode.OpenOrCreate,FileAccess.Write);
                        using (xlPackage = new ExcelPackage(ms))
                        {
                          
                            ExcelWorkbook workbook = xlPackage.Workbook;
                            ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("MultiFormList");

                            for (int i = 0; i <= dt_datatable.Rows.Count - 1; i++)
                            {
                                //  worksheet.Cells[1, 1].Value = dt_datatable.Rows[i].ItemArray[15].ToString();
                                worksheet.Cells[1, 1].Value = "S.No";
                                worksheet.Cells[1, 2].Value = "Name of the Person";
                                worksheet.Cells[1, 3].Value = "Age of the Person";
                                worksheet.Cells[1, 4].Value = "DOB";
                                worksheet.Cells[i + 2, 1].Value = j;
                                worksheet.Cells[i + 2, 2].Value = dt_datatable.Rows[i].ItemArray[2].ToString();
                                worksheet.Cells[i + 2, 3].Value = dt_datatable.Rows[i].ItemArray[0].ToString();
                                worksheet.Cells[i + 2, 4].Value = dt_datatable.Rows[i].ItemArray[1].ToString();
                                
                                j = j + 1;

                            }
                            //string path1 = @"E:\\CampaignApproval.xlsx";
                            //FileInfo file1 = new FileInfo(path1);
                           
                            xlPackage.SaveAs(file);
                        }
                    }

                }
            }
            catch(Exception ex)
            {

            }
            return xlPackage;
        }

    }
    }

