using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.osd.Models;
using System.Configuration;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using ems.storage.Functions;

namespace ems.osd.DataAccess
{

    public class DaOsdMstActivity
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetDocumentGid;
        string msGetActivityCode;
        int mnResult;
        string activityGID = string.Empty;

        public void DaPostActivityAdd(activityadd values, string user_gid)
        {

            if (values.activity_tat != "0" && values.activity_tat != "00" && values.activity_tat != "000")
            {

                msSQL = " select activity_name from osd_mst_tactivitymaster " +
                        " where activity_name='" + values.activity_name.Replace("'", "\\'") + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Activity Name Already Exist";
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ACMA");

                    msGetActivityCode = objcmnfunctions.GetMasterGID("ATC");

                    msSQL = " insert into osd_mst_tactivitymaster(" +
                     " activitymaster_gid," +
                     " department_gid," +
                     " department_name," +
                     " activity_code, " +
                     " activity_name," +
                     " supportteam_gid," +
                     " supportteam_name," +
                     " activity_tat," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.department_gid + "'," +
                      "'" + values.department_name + "'," +
                     "'" + msGetActivityCode + "', " +
                     "'" + values.activity_name.Replace("'", "\\'") + "'," +
                     "'" + values.supportteam_gid + "'," +
                     "'" + values.supportteam_name + "'," +
                     "'" + values.activity_tat + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Activity Details are Added Successfully..!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured..!";
                    }

                    objODBCDatareader.Close();
                }
                
            }
            else
            {
                values.status = false;
                values.message = "TAT Value Should not allow Zero..!";
            }
        }

        public bool DaGetActivityUpdate(activityadd values, string user_gid)
        {
            bool status = false;
            msSQL = " select request_status,activitymaster_gid,request_refno from osd_trn_tservicerequest where activitymaster_gid = '" + values.activitymaster_gid + "' and " +
                   "(request_status = 'Allotted' or request_status = 'Work-In-Progress')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.message = "This Activity has Assigned Service Request, You Cannot Update";
                values.status = false;
                return status;
            }
            else
            {

               
            if (values.activity_tat != "0" && values.activity_tat != "00" && values.activity_tat != "000")
            {

                msSQL = " select activitymaster_gid from osd_mst_tactivitymaster " +
                        " where activity_name='" + values.activity_name + "'";
                activityGID = objdbconn.GetExecuteScalar(msSQL);
                if (activityGID != "")
                {
                    if (activityGID != values.activitymaster_gid)
                    {
                        values.message = "Activity Name Already Exist";
                        values.status = false;
                        return status;
                    }
                }
                msSQL = " update osd_mst_tactivitymaster set " +
                     " department_gid='" + values.department_gid + "'," +
                      " department_name='" + values.department_name + "'," +
                    " activity_name='" + values.activity_name.Replace("'", "\\'") + "'," +
                    " supportteam_gid='" + values.supportteam_gid + "'," +
                    " supportteam_name='" + values.supportteam_name + "'," +
                    " activity_tat='" + values.activity_tat + "'," +
                    " updated_by='" + user_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where activitymaster_gid='" + values.activitymaster_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {                 
                    values.status = true;
                    values.message = "Activity Details are Updated Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";

                }

            }
            else
            {
                values.status = false;
                values.message = "TAT Value Should not allow Zero..!";
            }

                return status;
            }
           
        }

        public void DaGetActivityDelete(string activitymaster_gid, result values)
        {
            msSQL = " select request_status,activitymaster_gid,request_refno from osd_trn_tservicerequest where activitymaster_gid = '" + activitymaster_gid + "' and " +
                    "(request_status = 'Allotted' or request_status = 'Work-In-Progress')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.message = "This Activity has Assigned Service Request, You Cannot Delete";
                values.status = false;
            }
            else
            {
                msSQL = " delete from osd_mst_tactivitymaster where activitymaster_gid='" + activitymaster_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Activity Details are Deleted Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

                objODBCDatareader.Close();
            }
        }

        public void DaGetActivityView(string activitymaster_gid, activityadd values)
        {
            msSQL = " select activity_code,activity_name,supportteam_name,supportteam_gid, activity_tat,department_name,department_gid from osd_mst_tactivitymaster " +
                    " where activitymaster_gid='" + activitymaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.activity_code = objODBCDatareader["activity_code"].ToString();
                values.activity_name = objODBCDatareader["activity_name"].ToString();
                values.supportteam_name = objODBCDatareader["supportteam_name"].ToString();
                values.supportteam_gid = objODBCDatareader["supportteam_gid"].ToString();
                values.activity_tat = objODBCDatareader["activity_tat"].ToString();
                values.department_gid = objODBCDatareader["department_gid"].ToString();
                values.department_name = objODBCDatareader["department_name"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetActivitySummary(actvitydtllist values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " select activitymaster_gid,activity_code,activity_name,supportteam_name,activity_tat,a.department_name," +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from osd_mst_tactivitymaster a " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                    " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid order by activitymaster_gid desc";
            }
            else
            {
                msSQL = " select activitymaster_gid,activity_code,activity_name,supportteam_name,activity_tat,a.department_name," +
                   " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                   " from osd_mst_tactivitymaster a " +
                   " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                   " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                   " where (a.department_gid in (select department_gid from osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))and e.department_status='Y' " +
                   " order by activitymaster_gid desc";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getActivityList = new List<activitydtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getActivityList.Add(new activitydtl
                    {
                        activity_name = dt["activity_name"].ToString(),
                        activitymaster_gid = dt["activitymaster_gid"].ToString(),
                        activity_code = dt["activity_code"].ToString(),
                        supportteam_name = dt["supportteam_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        activity_tat = dt["activity_tat"].ToString(),
                        department_name = dt["department_name"].ToString(),
                    });
                    values.activitydtl = getActivityList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetTeamSummary(supportdtllist values, string department_gid)
        {
            msSQL = " select supportteam_gid,team_code,team_name,team_description from osd_mst_tsupportteam where department_gid ='" + department_gid + "'" +
                    " order by supportteam_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsupportdtlList = new List<supportdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getsupportdtlList.Add(new supportdtl
                    {
                        team_code = dt["team_code"].ToString(),
                        team_name = dt["team_name"].ToString(),
                        supportteam_gid = dt["supportteam_gid"].ToString(),
                    });
                    values.supportdtl = getsupportdtlList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetActivity(activitylist values, string department_gid)
        {
            msSQL = " select activitymaster_gid,activity_code,activity_name from osd_mst_tactivitymaster where department_gid ='" + department_gid + "'" +
                    " order by activitymaster_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsupportdtlList = new List<activitydtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getsupportdtlList.Add(new activitydtl
                    {
                        activity_code = dt["activity_code"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        activitymaster_gid = dt["activitymaster_gid"].ToString(),
                    });
                    values.activitydtl = getsupportdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetDeptTeam(supportdtllist values, string department_gid)
        {
            msSQL = " select supportteam_gid,team_code,team_name from osd_mst_tsupportteam  where department_gid ='" + department_gid + "'" +
                    " order by supportteam_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsupportdtlList = new List<supportdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getsupportdtlList.Add(new supportdtl
                    {
                        team_code = dt["team_code"].ToString(),
                        team_name = dt["team_name"].ToString(),
                        supportteam_gid = dt["supportteam_gid"].ToString(),
                    });
                    values.supportdtl = getsupportdtlList;
                }
            }
            dt_datatable.Dispose();
        }
        public void  DaGetExportActivityReport(result values,string employee_gid)
        {
            msSQL = " select a.department_name as `Business Unit Name`,a.activity_name as `Activity Name`,a.supportteam_name as `Assigned Team`, " +
                   " group_concat(DISTINCT d.member_name) as Members from osd_mst_tactivitymaster a  " +
                   " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid  " +
                   //" left join osd_mst_tactivedepartment2member m on m.department_gid = a.department_gid " +
                   " left join osd_mst_tsupportteam n on n.supportteam_gid = a.supportteam_gid " +
                   " left join osd_mst_tsupportteam2member d on d.supportteam_gid = n.supportteam_gid " +
                    " where (a.department_gid in (select department_gid from osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                    " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))and e.department_status='Y' " +
                    " group by a.activitymaster_gid order by activitymaster_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet = excel.Workbook.Worksheets.Add("Activity_Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "ActivityMaster_Report.xlsx";
                //var path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //objExportBankReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objExportBankReport.lsname;
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ActivityMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ActivityMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "OSD/ActivityMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                //    var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ActivityMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ActivityMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                //values.lscloudpath = lscompany_code + "/" + "OSD/ActivityMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                //bool exists = System.IO.Directory.Exists(path);
                //if (!exists)
                //{
                //    System.IO.Directory.CreateDirectory(path);
                //}
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 4])  //Address "A1:A19"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/ActivityMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                values.lspath = objcmnstorage.EncryptData(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ActivityMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);
                values.lscloudpath = objcmnstorage.EncryptData(lscompany_code + "/" + "OSD/ActivityMasterReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);

                ms.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.status = true;
            values.message = "Success";
        }

    }
}