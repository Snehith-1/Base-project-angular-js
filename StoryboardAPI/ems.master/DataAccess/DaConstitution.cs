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
using ems.storage.Functions;

/// <summary>
/// (It's used for Constitution Master) Constitution DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala and Logapriya</remarks>


namespace ems.master.DataAccess
{
    public class DaConstitution
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetAPICode;
        int mnResult, GetApiMasterGID;
        string lsmaster_value;

        public void DaGetconstitution(MdlConstitution objMdlConstitution)
        {
            try
            {
                msSQL = " SELECT constitution_gid,api_code,constitution_name,lms_code,bureau_code,status_log, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by " +
                    " FROM ocs_mst_tconstitution a"+
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by constitution_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getconstitution = new List<constitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getconstitution.Add(new constitution_list
                        {
                            constitution_gid = (dr_datarow["constitution_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            constitution_name = (dr_datarow["constitution_name"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objMdlConstitution.constitution_list = getconstitution;
                }
                dt_datatable.Dispose();
                objMdlConstitution.status = true;
            }
            catch
            {
                objMdlConstitution.status = false;
            }
        }

        public void DaCreateConstitution(constitution values, string employee_gid)
        {
            msSQL = "select constitution_gid from ocs_mst_tconstitution where constitution_name = '" + values.constitution_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                //if (lsdocumentgid != values.constitution_gid)
                //{
                    values.message = "This Constitution Already Exists";
                    values.status = false;
                    return;
                //}
            }
            string lslms_code, lsbureau_code, bureau_code, constitution_name, lms_code;
            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }

            
            msGetGid = objcmnfunctions.GetMasterGID("CONS");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("CNST");
            msSQL = " insert into ocs_mst_tconstitution(" +
                    " constitution_gid," +
                    " api_code," +
                    " constitution_name," +
                    " lms_code," +
                    " bureau_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + values.constitution_name.Replace("'", "\\'") + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Constitution Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding";
            }
        }

        public void DaEditConstitution(string constitution_gid, constitution values)
        {
            try
            {
                msSQL = " select constitution_gid,lms_code,bureau_code,constitution_name,status_log"+
                    " from ocs_mst_tconstitution where constitution_gid='" + constitution_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.constitution_gid = objODBCDatareader["constitution_gid"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.status_log = objODBCDatareader["status_log"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateConstitution(string employee_gid, constitution values)
        {

            msSQL = "select constitution_gid from ocs_mst_tconstitution where constitution_name = '" + values.constitution_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.constitution_gid)
                {
                    values.message = "This Constitution Already Exists";
                    values.status = false;
                    return ;
                }
            }
                        
            msSQL = "select updated_by, updated_date,constitution_name from ocs_mst_tconstitution where constitution_gid = '" + values.constitution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CLOG");
                    msSQL = " insert into ocs_trn_tauditconstitutionlog(" +
                              " auditconstitutionlog_gid," +
                              " constitution_gid," +
                              " constitution_name, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.constitution_gid + "'," +
                              "'" + objODBCDatareader["constitution_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tconstitution set " +
                 " constitution_name='" + values.constitution_name + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where constitution_gid='" + values.constitution_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Constitution Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public void DaDeleteConstitution(string constitution_gid,string employee_gid, constitution values)
        {
            msSQL = " select constitution_name from ocs_mst_tconstitution where constitution_gid='" + constitution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmaster_value = objODBCDatareader["constitution_name"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select application_gid from ocs_mst_tapplication where constitution_gid='" + constitution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.message = "Can't able to delete Constitution, Because it is tagged to Application Creation";
                values.status = false;
                return;
            }
            else
            {
                msSQL = " delete from ocs_mst_tconstitution where constitution_gid='" + constitution_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                    msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                             "master_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'Constitution'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    values.status = false;
                }
            }
        }
        //-----------Get Constitution - order by ASC-----//
        public void DaGetconstitutionASC(MdlConstitution objMdlConstitution)
        {
            try
            {
                msSQL = " SELECT constitution_gid,constitution_name,constitution_code FROM ocs_mst_tconstitution order by constitution_gid asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getconstitution = new List<constitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getconstitution.Add(new constitution_list
                        {
                            constitution_gid = (dr_datarow["constitution_gid"].ToString()),
                            constitution_code = (dr_datarow["constitution_code"].ToString()),
                            constitution_name = (dr_datarow["constitution_name"].ToString()),
                        });
                    }
                    objMdlConstitution.constitution_list = getconstitution;
                }
                dt_datatable.Dispose();
                objMdlConstitution.status = true;
            }
            catch
            {
                objMdlConstitution.status = false;
            }
        }

        public void DaconstitutionStatusUpdate(string employee_gid, constitution values)
        {

            msSQL = " update ocs_mst_tconstitution set status_log='" + values.status_log + "'," +
                " remarks='" + values.remarks.Replace("'"," ") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where constitution_gid='" + values.constitution_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("CLOG");
                msSQL = " insert into ocs_trn_tconstitutionstatuslog(" +
                          " constitutionstatuslog_gid," +
                          " constitution_gid," +
                          " status_log, " +
                          " remarks, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.constitution_gid + "'," +
                          "'" + values.status_log + "'," +
                          "'" + values.remarks.Replace("'", " ") + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating Status";
                values.status = false;
            }
        }
        //Get Active Status Log
        public void DaGetActiveLog(string constitution_gid, MdlConstitution objgetsegment)
        {
            try
            {
                msSQL = " SELECT d.constitution_name,a.status_log,a.remarks, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by" +
                    " FROM ocs_trn_tconstitutionstatuslog a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join ocs_mst_tconstitution d on a.constitution_gid=d.constitution_gid where a.constitution_gid='" + constitution_gid + "' order by a.constitutionstatuslog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<constitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new constitution_list
                        {
                            constitution_name = (dr_datarow["constitution_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objgetsegment.constitution_list = getSegment;
                }
                dt_datatable.Dispose();
                objgetsegment.status = true;

            }
            catch
            {
                objgetsegment.status = false;
            }
        }
        //export excel
        public void DaGetConstitutionData(constitutionSummary objconstitutionSummary)
        {
            msSQL = " select a.api_code,a.constitution_name as 'Constitution Name',a.lms_code as 'Lms Code',a.bureau_Code as 'Bureau Code', case when a.status_log= 'N' then 'Inactive' else 'Active' end as status," +
                   " date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'Created Date'," +
                  " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as CreatedBy, " +
                  " date_format(a.updated_date, '%d-%m-%Y %h:%i:%s %p') as 'Updated Date', " +
                  " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) " +
                  " as 'Updated By' FROM ocs_mst_tconstitution a " +
                  " left join hrm_mst_temployee b on a.Created_by = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                  " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                   " left join adm_mst_tuser e on e.user_gid = d.user_gid group by constitution_gid ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;

            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Constitution List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objconstitutionSummary.lsname = "Constitution List Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/Constitution/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objconstitutionSummary.lscloudpath =  lscompany_code + "/" + "Master/Constitution/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objconstitutionSummary.lsname;
                objconstitutionSummary.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/Constitution/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objconstitutionSummary.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objconstitutionSummary.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 10])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objconstitutionSummary.lscloudpath, ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                objconstitutionSummary.status = false;
                objconstitutionSummary.message = "Failure";
            }
            objconstitutionSummary.lscloudpath = objcmnstorage.EncryptData(objconstitutionSummary.lscloudpath);
            objconstitutionSummary.lspath = objcmnstorage.EncryptData(objconstitutionSummary.lspath);
            objconstitutionSummary.status = true;
            objconstitutionSummary.message = "Success";
        }
    }
}