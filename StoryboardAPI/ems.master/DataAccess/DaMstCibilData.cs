using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Net;
using System.IO;
using ems.utilities.Functions;
using ems.master.Models;
using System.Configuration;
using System.Drawing;

/// <summary>
/// (It's used for pages in CibilData )CibilData DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaMstCibilData
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL;
        int mnResult;
        HttpPostedFile httpPostedFile;
        string lspath;
        string msGetGID, msGETGIDDoc;
        string lsdocument_name = string.Empty;
        string lsdocument_code = string.Empty;
        string lsuser_name = string.Empty;
        string lssubmission_type, lssubmitted_on, lsindicator, lsname, lsaccount_no, lscurrent_balance, lsoverdue_amount, lsod_bucket1, lsod_bucket2, lsod_bucket3, lsod_bucket4, lsod_bucket5, lsod_days, lsaccount_status, lsclosed_on, lscibil, lshighmark, lsexperian, lsequifax;
        string lscompany_code = string.Empty;

        public void DaUploadCibil(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath, path, lsfile_name;
              
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/Master/CIBILDATA/"  + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);           
                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

               string  File_extension = Path.GetExtension(FileExtension).ToLower();

                if (File_extension == ".xls"|| File_extension == ".xlsx")
                { 
                string file_name = httpPostedFile.FileName;

                path = lsfilePath + "/" + file_name;
                //path creation        
                lspath = lsfilePath + "/";
                objcmnfunctions.uploadFile(lspath, file_name);
                //Excel To DataTable
                lsfile_name = file_name;
                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_name + "";
                lsfilePath = lsfilePath.Replace("\\", "\\\\");
                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, "A1:S");

                    msGETGIDDoc = objcmnfunctions.GetMasterGID("CBIL");
                    msSQL = "insert into ocs_trn_tcibildata (" +
                        " cibildata_gid ," +
                        " document_name, " +
                        " document_path," +
                        " doucmentuploaded_by, " +
                        " documentuploaded_date)" +
                        " values (" +
                        "'" + msGETGIDDoc + "'," +
                        "'" + lsfile_name + "'," +
                        "'" + lsfilePath + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    foreach (DataRow dr_datarow in dt.Rows)
                    {
                    try { 
                            lssubmission_type = dr_datarow["Submission Type"].ToString();                          
                            lsindicator = dr_datarow["Indicator"].ToString();
                            lsname = dr_datarow["Name"].ToString();
                            lsaccount_no = dr_datarow["Account Number"].ToString();
                            lscurrent_balance = dr_datarow["Current Balance"].ToString();
                            lsoverdue_amount = dr_datarow["Amount Overdue"].ToString();
                            lsod_bucket1 = dr_datarow["OD Bucket 01_1-30"].ToString();
                            lsod_bucket2 = dr_datarow["OD Bucket 02_31-60"].ToString();
                            lsod_bucket3 = dr_datarow["OD Bucket 03_61-90"].ToString();
                            lsod_bucket4 = dr_datarow["OD Bucket 04_91-180"].ToString();
                            lsod_bucket5 = dr_datarow["Overdue Bucket 05_Above 180"].ToString();
                            lsod_days = dr_datarow["OD Days"].ToString();
                            lsaccount_status = dr_datarow["Account Status"].ToString();
                            lsclosed_on = dr_datarow["Closed on"].ToString();
                            lscibil = dr_datarow["CIBIL"].ToString();
                            lshighmark = dr_datarow["Highmark"].ToString();
                            lsexperian = dr_datarow["Experian"].ToString();
                            lsequifax = dr_datarow["Equifax"].ToString();
                            lssubmitted_on = dr_datarow["Submitted on"].ToString();
                               
                            if ((lsaccount_no =="") || (lsindicator=="")|| (lssubmitted_on==""))
                            {                        
                                msSQL = "update ocs_trn_tcibildata set cibilhistory_flag='Y' where cibildata_gid='" + msGETGIDDoc + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msGetGID = objcmnfunctions.GetMasterGID("CBLD");
                                msSQL = " insert into ocs_trn_tcibildatalog (" +
                                  " cibildatalog_gid," +
                                  " cibildata_gid," +
                                  " customer_name," +
                                  " account_no," +
                                  " indicator," +
                                  " reason," +
                                  " submission_type, " +
                                 " submitted_on," +
                                 " current_balance, " +
                                 " overdue_amount," +
                                 " odbucket_01, " +
                                 " odbucket_02, " +
                                 " odbucket_03, " +
                                 " odbucket_04, " +
                                 " odbucket_05, " +
                                 " od_days, " +
                                 " account_status," +
                                 " cibil, " +
                                 " highmark, " +
                                 " experian, " +
                                 " euifax," +
                                 " uploded_by," +
                                 " uploaded_date) values(" +
                                 "'" + msGetGID + "'," +
                                  "'" + msGETGIDDoc + "'," +
                                  "'" + lsname + "'," +
                                  "'" + lsaccount_no + "'," +
                                  "'" + lsindicator + "',";
                                if (lsindicator == "")
                                {
                                    msSQL += "'Indicator is mandatory',";
                                }
                                else if (lsaccount_no == "")
                                {
                                    msSQL += "'Account No is mandatory',";
                                }
                                else if (lssubmitted_on == "")
                                {
                                    msSQL += "'Submitted On Date is mandatory',";
                                }
                                msSQL += "'" + lssubmission_type + "'," +
                                  "'" + lssubmitted_on + "'," +
                                  "'" + lscurrent_balance + "'," +
                                  "'" + lsoverdue_amount + "'," +
                                  "'" + lsod_bucket1 + "'," +
                                  "'" + lsod_bucket2 + "'," +
                                  "'" + lsod_bucket3 + "'," +
                                  "'" + lsod_bucket4 + "'," +
                                  "'" + lsod_bucket5 + "'," +
                                  "'" + lsod_days + "'," +
                                  "'" + lsaccount_status + "'," +
                                  "'" + lscibil + "'," +
                                  "'" + lshighmark + "'," +
                                  "'" + lsexperian + "'," +
                                  "'" + lsequifax + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                               
                       }
                       else
                       {
                                string inputString = lssubmitted_on;
                                DateTime dDate;
                                try {
                                    if (DateTime.TryParse(inputString, out dDate))
                                    {
                                        String.Format("{0:d/MM/yyyy}", dDate);

                                        msSQL = "select cibildatadtl_gid  from ocs_trn_tcibildatadtl where account_no='" + lsaccount_no + "' and indicator='" + lsindicator + "' and submitted_on='" + lssubmitted_on + "'";
                                        string lscibildatadtl_gid = objdbconn.GetExecuteScalar(msSQL);
                                        if (lscibildatadtl_gid == "" || lscibildatadtl_gid == null)
                                        {
                                            msGetGID = objcmnfunctions.GetMasterGID("CBLD");
                                            msSQL = " insert into ocs_trn_tcibildatadtl (" +
                                                    " cibildatadtl_gid ," +
                                                    " cibildata_gid," +
                                                    " submission_type, " +
                                                    " submitted_on," +
                                                    " indicator, " +
                                                    " name," +
                                                    " account_no ," +
                                                    " current_balance, " +
                                                    " overdue_amount," +
                                                    " odbucket_01, " +
                                                    " odbucket_02, " +
                                                    " odbucket_03, " +
                                                    " odbucket_04, " +
                                                    " odbucket_05, " +
                                                    " od_days, " +
                                                    " account_status," +
                                                    " cibil, " +
                                                    " highmark, " +
                                                    " experian, " +
                                                    " euifax," +
                                                    " created_by," +
                                                    " created_date )" +
                                                    " values (" +
                                                    "'" + msGetGID + "'," +
                                                    "'" + msGETGIDDoc + "'," +
                                                    "'" + lssubmission_type + "'," +
                                                    "'" + lssubmitted_on + "'," +
                                                    "'" + lsindicator + "'," +
                                                    "'" + lsname + "'," +
                                                    "'" + lsaccount_no + "'," +
                                                    "'" + lscurrent_balance + "'," +
                                                    "'" + lsoverdue_amount + "'," +
                                                    "'" + lsod_bucket1 + "'," +
                                                    "'" + lsod_bucket2 + "'," +
                                                    "'" + lsod_bucket3 + "'," +
                                                    "'" + lsod_bucket4 + "'," +
                                                    "'" + lsod_bucket5 + "'," +
                                                    "'" + lsod_days + "'," +
                                                    "'" + lsaccount_status + "'," +
                                                    "'" + lscibil + "'," +
                                                    "'" + lshighmark + "'," +
                                                    "'" + lsexperian + "'," +
                                                    "'" + lsequifax + "'," +
                                                    "'" + employee_gid + "'," +
                                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                        else
                                        {
                                            msSQL = "update ocs_trn_tcibildata set cibilhistory_flag='Y' where cibildata_gid='" + msGETGIDDoc + "'";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                            msGetGID = objcmnfunctions.GetMasterGID("CBLD");
                                            msSQL = " insert into ocs_trn_tcibildatalog (" +
                                                " cibildatalog_gid," +
                                                " cibildata_gid," +
                                                " customer_name," +
                                                " account_no," +
                                                " indicator," +
                                                " reason," +
                                                " submission_type, " +
                                               " submitted_on," +
                                               " current_balance, " +
                                               " overdue_amount," +
                                               " odbucket_01, " +
                                               " odbucket_02, " +
                                               " odbucket_03, " +
                                               " odbucket_04, " +
                                               " odbucket_05, " +
                                               " od_days, " +
                                               " account_status," +
                                               " cibil, " +
                                               " highmark, " +
                                               " experian, " +
                                               " euifax," +
                                               " uploded_by," +
                                               " uploaded_date) values(" +
                                               "'" + msGetGID + "'," +
                                                "'" + msGETGIDDoc + "'," +
                                                "'" + lsname + "'," +
                                                "'" + lsaccount_no + "'," +
                                                "'" + lsindicator + "'," +
                                                "'Already Added'," +
                                               "'" + lssubmission_type + "'," +
                                               "'" + lssubmitted_on + "'," +
                                               "'" + lscurrent_balance + "'," +
                                               "'" + lsoverdue_amount + "'," +
                                               "'" + lsod_bucket1 + "'," +
                                               "'" + lsod_bucket2 + "'," +
                                               "'" + lsod_bucket3 + "'," +
                                               "'" + lsod_bucket4 + "'," +
                                               "'" + lsod_bucket5 + "'," +
                                               "'" + lsod_days + "'," +
                                               "'" + lsaccount_status + "'," +
                                               "'" + lscibil + "'," +
                                               "'" + lshighmark + "'," +
                                               "'" + lsexperian + "'," +
                                               "'" + lsequifax + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }
                              
                                else
                                {
                                    msSQL = "update ocs_trn_tcibildata set cibilhistory_flag='Y' where cibildata_gid='" + msGETGIDDoc + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msGetGID = objcmnfunctions.GetMasterGID("CBLD");
                                    msSQL = " insert into ocs_trn_tcibildatalog (" +
                                        " cibildatalog_gid," +
                                        " cibildata_gid," +
                                        " customer_name," +
                                        " account_no," +
                                        " indicator," +
                                        " reason," +
                                        " submission_type, " +
                                       " submitted_on," +
                                       " current_balance, " +
                                       " overdue_amount," +
                                       " odbucket_01, " +
                                       " odbucket_02, " +
                                       " odbucket_03, " +
                                       " odbucket_04, " +
                                       " odbucket_05, " +
                                       " od_days, " +
                                       " account_status," +
                                       " cibil, " +
                                       " highmark, " +
                                       " experian, " +
                                       " euifax," +
                                       " uploded_by," +
                                       " uploaded_date) values(" +
                                       "'" + msGetGID + "'," +
                                        "'" + msGETGIDDoc + "'," +
                                        "'" + lsname + "'," +
                                        "'" + lsaccount_no + "'," +
                                        "'" + lsindicator + "'," +
                                        "'Date Format is mismatch'," +
                                       "'" + lssubmission_type + "'," +
                                       "'" + lssubmitted_on + "'," +
                                       "'" + lscurrent_balance + "'," +
                                       "'" + lsoverdue_amount + "'," +
                                       "'" + lsod_bucket1 + "'," +
                                       "'" + lsod_bucket2 + "'," +
                                       "'" + lsod_bucket3 + "'," +
                                       "'" + lsod_bucket4 + "'," +
                                       "'" + lsod_bucket5 + "'," +
                                       "'" + lsod_days + "'," +
                                       "'" + lsaccount_status + "'," +
                                       "'" + lscibil + "'," +
                                       "'" + lshighmark + "'," +
                                       "'" + lsexperian + "'," +
                                       "'" + lsequifax + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                }
                                catch (Exception e)
                                {
                                    msSQL = "update ocs_trn_tcibildata set cibilhistory_flag='Y' where cibildata_gid='" + msGETGIDDoc + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msGetGID = objcmnfunctions.GetMasterGID("CBLD");
                                    msSQL = " insert into ocs_trn_tcibildatalog (" +
                                        " cibildatalog_gid," +
                                        " cibildata_gid," +
                                        " customer_name," +
                                        " account_no," +
                                        " indicator," +
                                        " reason," +
                                        " submission_type, " +
                                       " submitted_on," +
                                       " current_balance, " +
                                       " overdue_amount," +
                                       " odbucket_01, " +
                                       " odbucket_02, " +
                                       " odbucket_03, " +
                                       " odbucket_04, " +
                                       " odbucket_05, " +
                                       " od_days, " +
                                       " account_status," +
                                       " cibil, " +
                                       " highmark, " +
                                       " experian, " +
                                       " euifax," +
                                       " uploded_by," +
                                       " uploaded_date) values(" +
                                       "'" + msGetGID + "'," +
                                        "'" + msGETGIDDoc + "'," +
                                        "'" + lsname + "'," +
                                        "'" + lsaccount_no + "'," +
                                        "'" + lsindicator + "'," +
                                        "'Date Format is mismatch'," +
                                       "'" + lssubmission_type + "'," +
                                       "'" + lssubmitted_on + "'," +
                                       "'" + lscurrent_balance + "'," +
                                       "'" + lsoverdue_amount + "'," +
                                       "'" + lsod_bucket1 + "'," +
                                       "'" + lsod_bucket2 + "'," +
                                       "'" + lsod_bucket3 + "'," +
                                       "'" + lsod_bucket4 + "'," +
                                       "'" + lsod_bucket5 + "'," +
                                       "'" + lsod_days + "'," +
                                       "'" + lsaccount_status + "'," +
                                       "'" + lscibil + "'," +
                                       "'" + lshighmark + "'," +
                                       "'" + lsexperian + "'," +
                                       "'" + lsequifax + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                        }
                    catch(Exception ex)
                    {
                        msSQL = "update ocs_trn_tcibildata set cibilhistory_flag='Y' where cibildata_gid='" + msGETGIDDoc + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            msSQL = " insert into ocs_trn_tcibildatalog (" +
                       " cibildatalog_gid," +
                       " cibildata_gid," +
                       " customer_name," +
                       " account_no," +
                       " indicator," +
                       " reason," +
                       " submission_type, " +
                      " submitted_on," +
                      " current_balance, " +
                      " overdue_amount," +
                      " odbucket_01, " +
                      " odbucket_02, " +
                      " odbucket_03, " +
                      " odbucket_04, " +
                      " odbucket_05, " +
                      " od_days, " +
                      " account_status," +
                      " cibil, " +
                      " highmark, " +
                      " experian, " +
                      " euifax," +
                      " uploded_by," +
                      " uploaded_date) values(" +
                      "'" + msGetGID + "'," +
                       "'" + msGETGIDDoc + "'," +
                       "'" + lsname + "'," +
                       "'" + lsaccount_no + "'," +
                       "'" + lsindicator + "'," +
                       "'Some data is missing / Format is mismatch'," +
                      "'" + lssubmission_type + "'," +
                      "'" + lssubmitted_on + "'," +
                      "'" + lscurrent_balance + "'," +
                      "'" + lsoverdue_amount + "'," +
                      "'" + lsod_bucket1 + "'," +
                      "'" + lsod_bucket2 + "'," +
                      "'" + lsod_bucket3 + "'," +
                      "'" + lsod_bucket4 + "'," +
                      "'" + lsod_bucket5 + "'," +
                      "'" + lsod_days + "'," +
                      "'" + lsaccount_status + "'," +
                      "'" + lscibil + "'," +
                      "'" + lshighmark + "'," +
                      "'" + lsexperian + "'," +
                      "'" + lsequifax + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                         
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        objResult.message = "Kindly check the Excel sheet";
                    }
                }
                            
                dt.Dispose();
             
                objResult.status = true;
                objResult.message = "Uploaded Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "File Format is not Supported";
                }
            }
           
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = "Kindly check the excel data";
            }
        }
        public void DaGetCibilUploadSummary(MdlMstCibilData values)
        {
            msSQL = " select a.cibildata_gid,a.document_name,date_format(a.documentuploaded_date ,'%d-%m-%Y %h:%i %p') as uploaded_date,"+
                    " concat(c.user_firstname,' ' ,c.user_lastname,' / ',c.user_code) as uploaded_by,cibilhistory_flag " +
                    " from ocs_trn_tcibildata a" +
                    " left join hrm_mst_temployee b on a.doucmentuploaded_by =b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid order by a.cibildata_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcibiluploaddtl= new List<uploadedcibil_data>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcibiluploaddtl.Add(new uploadedcibil_data
                    {
                        uploaded_date = dt["uploaded_date"].ToString(),
                        uploaded_by = dt["uploaded_by"].ToString(),
                        file_name = dt["document_name"].ToString(),
                        cibildata_gid= dt["cibildata_gid"].ToString(),
                        cibilhistory_flag = dt["cibilhistory_flag"].ToString(),
                    });
                }
                values.uploadedcibil_data = getcibiluploaddtl;               
            }
           
            dt_datatable.Dispose();
        }
        public void DaGetCibilSummary(MdlCibilSummary values, string cibildata_gid)
        {
            msSQL = " select cibildatadtl_gid,account_no,name,indicator,submission_type,account_status,format(overdue_amount,2,'en_IN') as overdue_amount,"+
                " submitted_on from ocs_trn_tcibildatadtl  where cibildata_gid ='" + cibildata_gid +"'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcibilsummary = new List<cibilsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getcibilsummary.Add(new cibilsummary_list
                    {
                        account_no = dt["account_no"].ToString(),
                        name = dt["name"].ToString(),
                        indicator = dt["indicator"].ToString(),
                        submission_type = dt["submission_type"].ToString(),
                        account_status = dt["account_status"].ToString(),
                        overdue_amount = dt["overdue_amount"].ToString(),
                        cibildatadtl_gid= dt["cibildatadtl_gid"].ToString(),
                        submitted_on = dt["submitted_on"].ToString(),
                    });
                }
                values.cibilsummary_list = getcibilsummary;
            }
            dt_datatable.Dispose();
        }
        public bool DaGetEditCibildata(MdlCibilEdit values, string cibildatadtl_gid)
        {
            msSQL = " select cibildatadtl_gid,submission_type,submitted_on as txtsubmitted_on, submitted_on,case when indicator='01' then 'Borrower' else 'Guarantor' end as indicator,name,account_no," +
                " format(current_balance,2,'en_IN') as current_balance,format(overdue_amount,2,'en_IN') as overdue_amount,odbucket_01,odbucket_02,odbucket_03,odbucket_04,odbucket_05,od_days," +
                " account_status,closed_on,cibil,highmark,experian,euifax from ocs_trn_tcibildatadtl  where cibildatadtl_gid ='" + cibildatadtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader .HasRows ==true)
            {
                objODBCDatareader.Read();
                values.submission_type = objODBCDatareader["submission_type"].ToString();
                if (objODBCDatareader["submitted_on"].ToString() == "")
                {
                }
                else
                {
                    values.submitted_on = Convert.ToDateTime(objODBCDatareader["submitted_on"]).ToString("MM-dd-yyyy");
                }
                values.txtsubmitted_on = objODBCDatareader["txtsubmitted_on"].ToString();
                values.indicator = objODBCDatareader["indicator"].ToString();
                values.name = objODBCDatareader["name"].ToString();
                values.account_no = objODBCDatareader["account_no"].ToString();
                values.current_balance = objODBCDatareader["current_balance"].ToString();
                values.overdue_amount = objODBCDatareader["overdue_amount"].ToString();
                values.odbucket_01 = objODBCDatareader["odbucket_01"].ToString();
                values.odbucket_02 = objODBCDatareader["odbucket_02"].ToString();
                values.odbucket_03 = objODBCDatareader["odbucket_03"].ToString();
                values.odbucket_04 = objODBCDatareader["odbucket_04"].ToString();
                values.odbucket_05 = objODBCDatareader["odbucket_05"].ToString();
                values.od_days = objODBCDatareader["od_days"].ToString();
                values.account_status = objODBCDatareader["account_status"].ToString();
                if (objODBCDatareader["closed_on"].ToString() == "")
                {
                }
                else
                {
                    values.closed_on = Convert.ToDateTime(objODBCDatareader["closed_on"]).ToString("MM-dd-yyyy");
                }
                //values.closed_on = objODBCDatareader["closed_on"].ToString();
                values.cibil = objODBCDatareader["cibil"].ToString();
                values.highmark = objODBCDatareader["highmark"].ToString();
                values.experian = objODBCDatareader["experian"].ToString();
                values.euifax = objODBCDatareader["euifax"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }
        public void DaPostCibilData(MdlCibilEdit values,string employee_gid)
        {
            msSQL = "update ocs_trn_tcibildatadtl set submission_type='" + values.submission_type + "'," +
                " current_balance='" + values.current_balance.Replace(",", "").Trim()+ "'," +
                " overdue_amount='" + values.overdue_amount.Replace(",", "").Trim() + "'," +
                " odbucket_01='" + values.odbucket_01 + "'," +
                " odbucket_02='" + values.odbucket_02 + "'," +
                " odbucket_03='" + values.odbucket_03 + "'," +
                " odbucket_04='" + values.odbucket_04 + "'," +
                " odbucket_05='" + values.odbucket_05 + "'," +
                " od_days='" + values.od_days + "'," +
                " account_status='" + values.account_status + "'," +
                 " cibil='" + values.cibil + "'," +
                " highmark='" + values.highmark + "'," +
                " experian='" + values.experian + "'," +
                " euifax='" + values.euifax + "' where cibildatadtl_gid='" + values.cibildatadtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

           
            if (Convert.ToDateTime(values.closedon).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL = "update ocs_trn_tcibildatadtl set closed_on='" + Convert.ToDateTime(values.closedon).ToString("yyyy-MM-dd 00:00:00") + "'" +
                    " where cibildatadtl_gid = '" + values.cibildatadtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult!=0)
            {
                values.message = "Cibil Data updated succesfully";
                values.status = true;
            }
            else
            {
                values.message = "Eror Occured while updating Cibil Data";
                values.status = false;
            }
            
        }
        public void DaGetCibilLog(MdlCibilSummary values, string cibildata_gid)
        {
            msSQL = " select cibildata_gid,account_no,customer_name,indicator,reason,date_format(uploaded_date,'%d-%m-%Y') as uploaded_date,submitted_on " +
                " from ocs_trn_tcibildatalog  where cibildata_gid ='" + cibildata_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcibilsummary = new List<cibilsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getcibilsummary.Add(new cibilsummary_list
                    {
                        account_no = dt["account_no"].ToString(),
                        name = dt["customer_name"].ToString(),
                        indicator = dt["indicator"].ToString(),
                        reason = dt["reason"].ToString(),
                        cibildata_gid = dt["cibildata_gid"].ToString(),
                        uploaded_date = dt["uploaded_date"].ToString(),
                        submitted_on = dt["submitted_on"].ToString(),
                    });
                }
                values.cibilsummary_list = getcibilsummary;               
            }            
            dt_datatable.Dispose();
        }
    }
}