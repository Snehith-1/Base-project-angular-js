using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.IO;
using System.Linq;
using System.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using ems.storage.Functions;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;



namespace ems.audit.DataAccess
{
    public class DaAtmMstCheckpointGroup
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        string msSQL, msGetGid, msGetGid1, msGetGidlog, lscheckpointgroup_value, lslms_code, lsbureau_code, lscheckpointgroup_code, lsauditdepartment_value, lsyes_score, lsno_score, lsnoteto_auditor, lspartial_score, lsna_score, lsauditname_value, lscompany_code,
            lscheckpoint_intent, lscheckpoint_description, lsriskcategory_name, lspositiveconfirmity_name, lsriskcategory_gid, lspositiveconfirmity_gid, lstotal_score,
            lsyes_disposition, lsno_disposition, lspartial_disposition, lsna_disposition, lschecklist_name, lsentity_name, lsauditdepartment_name, lscheckpointgroup_name, lsaudittype_name;
        string excelRange, endRange, lscheckpointadd_gid;
        int rowCount, columnCount;
        int insertCount = 0, logCount = 0;
        string checklistexcelimportlog_message = "";
        string lsflag = "No";
        int mnResult;

        public void DaGetCheckpointGroup(MdlAtmMstCheckpointGroup values)
        {
            try
            {
                msSQL = " SELECT distinct a.checkpointgroup_gid,a.checkpointgroup_name,a.checkpointgroup_code, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, f.auditdelete_flag," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM atm_mst_tcheckpointgroup a" +
                        " left join atm_mst_tchecklistmaster e on a.checkpointgroup_gid = e.checkpointgroup_gid" +
                        " left join atm_trn_tauditcreation f on f.checklistmaster_gid = e.checklistmaster_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid group by a.checkpointgroup_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcheckpointgroup_list = new List<checkpointgroup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcheckpointgroup_list.Add(new checkpointgroup_list
                        {
                            checkpointgroup_gid = (dr_datarow["checkpointgroup_gid"].ToString()),
                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),

                            checkpointgroup_code = (dr_datarow["checkpointgroup_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),

                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            auditdelete_flag = (dr_datarow["auditdelete_flag"].ToString()),

                        });
                    }
                    values.checkpointgroup_list = getcheckpointgroup_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaGetCheckpointGroupName(string checkpointgroup_gid, MdlAtmMstCheckpointGroup values)
        {
            msSQL = " select  checkpointgroup_name, checkpointgroup_gid  from atm_mst_tcheckpointgroup " +
                  " where checkpointgroup_gid='" + checkpointgroup_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checkpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                values.checkpointgroup_gid = objODBCDatareader["checkpointgroup_gid"].ToString();
            }
            objODBCDatareader.Close();


        }


        public void DaGetCheckpointAdd(string checkpointgroup_gid, MdlAtmMstCheckpointGroup values)
        {
           
            try
            {
                msSQL = " SELECT distinct a.checkpointgroup_gid,d.checkpointgroup_name,a.checkpointgroupadd_gid,a.riskcategory_name,a.positiveconfirmity_name,a.checkpoint_intent,a.checkpoint_description,a.noteto_auditor,a.yes_score,a.no_score,a.partial_score,a.na_score,a.total_score,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_mst_tcheckpointadd a" +
                        " left join atm_mst_tcheckpointgroup d on d.checkpointgroup_gid = a.checkpointgroup_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         "  where d.checkpointgroup_gid='" + checkpointgroup_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcheckpointgroupadd_list = new List<checkpointgroupadd_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcheckpointgroupadd_list.Add(new checkpointgroupadd_list
                        {
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                            checkpointgroup_gid = (dr_datarow["checkpointgroup_gid"].ToString()),
                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                            riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
                            positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
                            checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
                            checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
                            noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
                            yes_score = (dr_datarow["yes_score"].ToString()),
                            no_score = (dr_datarow["no_score"].ToString()),
                            partial_score = (dr_datarow["partial_score"].ToString()),
                            na_score = (dr_datarow["na_score"].ToString()),
                            total_score = (dr_datarow["total_score"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            //checkpointgroup_flag = (dr_datarow["checkpointgroup_flag"].ToString()),

                        });
                    }
                    values.checkpointgroupadd_list = getcheckpointgroupadd_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)

            {
                values.status = false;
            }
        }

        public void DaPostCheckpointAdd(MdlAtmMstCheckpointGroup values, string employee_gid)
        {
            msSQL = "select checkpointgroupadd_gid from atm_trn_tmultipleauditee where checkpointgroupadd_gid ='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add atleast one auditee details";
                return;
            }

            if (values.yes_score == null || values.yes_score == "")
            {
                lsyes_score = "0";
            }
            else
            {
                lsyes_score = values.yes_score.Replace("'", "");
            }
            if (values.no_score == null || values.no_score == "")
            {
                lsno_score = "0";
            }
            else
            {
                lsno_score = values.no_score.Replace("'", "");
            }
            if (values.partial_score == null || values.partial_score == "")
            {
                lspartial_score = "0";
            }
            else
            {
                lspartial_score = values.partial_score.Replace("'", "");
            }
            if (values.na_score == null || values.na_score == "")
            {
                lsna_score = "0";
            }
            else
            {
                lsna_score = values.na_score.Replace("'", "");
            }
            if (values.noteto_auditor == null || values.noteto_auditor == "")
            {
                lsnoteto_auditor = "";
            }
            else
            {
                lsnoteto_auditor = values.noteto_auditor.Replace("'", "");
            }

            msGetGid = objcmnfunctions.GetMasterGID("CHGA");
            msSQL = " insert into atm_mst_tcheckpointadd(" +
                    " checkpointgroupadd_gid," +
                    " checkpointgroup_gid ," +
                    " riskcategory_gid," +
                    " riskcategory_name ," +
                     " positiveconfirmity_gid," +
                    " positiveconfirmity_name ," +
                    " checkpoint_intent," +
                    " checkpoint_description ," +
                    " noteto_auditor," +
                    " yes_score ," +
                    " no_score," +
                    " partial_score," +
                    " na_score," +
                    " total_score," +
                    " yes_disposition," +
                    " no_disposition," +
                    " partial_disposition," +
                    " na_disposition," +                
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.checkpointgroup_gid + "', " +
                    "'" + values.riskcategory_gid + "'," +
                       "'" + values.riskcategory_name.Replace("'", "") + "'," +
                        "'" + values.positiveconfirmity_gid + "'," +
                       "'" + values.positiveconfirmity_name.Replace("'", "") + "'," +
                    "'" + values.checkpoint_intent.Replace("'", "") + "'," +
                      "'" + values.checkpoint_description.Replace("'", "") + "'," +
                    "'" + lsnoteto_auditor.Replace("'", "") + "'," +
                      "'" + lsyes_score + "'," +
                     "'" + lsno_score + "'," +
                          "'" + lspartial_score + "'," +
                       "'" + lsna_score + "'," +
                    "'" + values.total_score.Replace("'", "") + "'," +
                      "'" + values.yes_disposition.Replace("'", "") + "'," +
                        "'" + values.no_disposition.Replace("'", "") + "'," +
                          "'" + values.partial_disposition.Replace("'", "") + "'," +
                            "'" + values.na_disposition.Replace("'", "") + "'," +                  
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " update atm_trn_tmultipleauditee set checkpointgroupadd_gid='" + msGetGid + "',checkpointgroup_gid = '" + values.checkpointgroup_gid + "' where checkpointgroupadd_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tchecklist2checkpoint set checkpointgroupadd_gid = '" + msGetGid + "' " +
                 " where checkpointgroupadd_gid = '" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tchecklist2checkpoint set checkpointgroup_gid = '" + values.checkpointgroup_gid + "' " +
                " where checkpointgroupadd_gid = '" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tsample2checkpoint set checkpointgroupadd_gid = '" + msGetGid + "' " +
                " where checkpointgroupadd_gid = '" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tsample2checkpoint set checkpointgroup_gid = '" + values.checkpointgroup_gid + "' " +
                " where checkpointgroupadd_gid = '" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Checkpoint Added Successfully";
            }
            else
            {
                values.message = "Error Occured While Adding";
                values.status = false;
            }
        }


        public void DaCreateCheckpointGroup(MdlAtmMstCheckpointGroup values, string employee_gid)
        {
            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "");
            }

            if (values.checkpointgroup_code == null || values.checkpointgroup_code == "")
            {
                lscheckpointgroup_code = "";
            }
            else
            {
                lscheckpointgroup_code = values.checkpointgroup_code.Replace("'", "");
            }

            msSQL = "select checkpointgroup_name from atm_mst_tcheckpointgroup where checkpointgroup_name = '" + values.checkpointgroup_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Checklist Creation Name Already Exist";
            }
            else
            {


                msGetGid = objcmnfunctions.GetMasterGID("CHEC");
                lscheckpointgroup_code = objcmnfunctions.GetMasterGID("IADCG");

                msSQL = " insert into atm_mst_tcheckpointgroup(" +
                        " checkpointgroup_gid," +
                        " checkpointgroup_name," +

                        " checkpointgroup_code," +
                        " lms_code," +
                        " bureau_code," +

                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.checkpointgroup_name.Replace("'", "") + "'," +

                        "'" + lscheckpointgroup_code + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +

                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Checklist Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditCheckpointGroup(string checkpointgroup_gid, MdlAtmMstCheckpointGroup values)
        {
            try
            {
                msSQL = " SELECT checkpointgroup_gid,checkpointgroup_name,checkpointgroup_code,lms_code, bureau_code,status as Status FROM atm_mst_tcheckpointgroup where checkpointgroup_gid='" + checkpointgroup_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.checkpointgroup_gid = objODBCDatareader["checkpointgroup_gid"].ToString();
                    values.checkpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                    values.checkpointgroup_code = objODBCDatareader["checkpointgroup_code"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();

                    values.Status = objODBCDatareader["Status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateCheckpointGroup(string employee_gid, MdlAtmMstCheckpointGroup values)
        {


            msSQL = " update atm_mst_tcheckpointgroup set " +
                 " checkpointgroup_name='" + values.checkpointgroup_name.Replace("'", "") + "'," +

                 " checkpointgroup_code='" + values.checkpointgroup_code + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +

                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where checkpointgroup_gid='" + values.checkpointgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("CHEL");

                msSQL = " insert into atm_mst_tcheckpointgrouplog (" +
                       " checkpointgrouplog_gid, " +
                       " checkpointgroup_gid, " +
                       " checkpointgroup_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.checkpointgroup_gid + "'," +
                       " '" + values.checkpointgroup_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Checklist Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveCheckpointGroup(MdlAtmMstCheckpointGroup values, string employee_gid)
        {
            msSQL = " update atm_mst_tcheckpointgroup set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where checkpointgroup_gid='" + values.checkpointgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("CHIL");

                msSQL = " insert into atm_mst_tcheckpointgroupinactivelog (" +
                      " checkpointgroupinactivelog_gid, " +
                      " checkpointgroup_gid," +
                      " checkpointgroup_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.checkpointgroup_gid + "'," +
                      " '" + values.checkpointgroup_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Checklist Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Checklist  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteCheckpointGroup(string checkpointgroup_gid, string employee_gid, MdlAtmMstCheckpointGroup values)
        {

            msSQL = " select checkpointgroup_name from atm_mst_tcheckpointgroup where checkpointgroup_gid='" + checkpointgroup_gid + "'";
            lscheckpointgroup_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from atm_mst_tcheckpointgroup where checkpointgroup_gid='" + checkpointgroup_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Checklist Creation Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("CHDL");
                msSQL = " insert into atm_mst_tcheckpointgroupdeletelog(" +
                         "checkpointgroupdeletelog_gid, " +
                         "checkpointgroup_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + checkpointgroup_gid + "', " +
                         "'Check Point Group'," +
                         "'" + lscheckpointgroup_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }


        public void DaCheckpointGroupInactiveLogview(string checkpointgroup_gid, MdlAtmMstCheckpointGroup values)
        {
            try
            {
                msSQL = " SELECT a.checkpointgroup_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM atm_mst_tcheckpointgroupinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.checkpointgroup_gid ='" + checkpointgroup_gid + "' order by a.checkpointgroupinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<checkpointgroup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new checkpointgroup_list
                        {
                            checkpointgroup_gid = (dr_datarow["checkpointgroup_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.checkpointgroup_list = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaEditCheckpoint(string checkpointgroupadd_gid, MdlAtmMstCheckpointGroup values)
        {
            msSQL = " select    a.checkpointgroupadd_gid,b.checkpointgroup_gid,b.checkpointgroup_name,a.riskcategory_name,a.riskcategory_gid,a.positiveconfirmity_name,a.positiveconfirmity_gid,a.checkpoint_intent, a.checkpoint_description,a.noteto_auditor,a.yes_score,a.no_score,a.partial_score,a.na_score,a.total_score,a.yes_disposition,a.no_disposition,a.partial_disposition,a.na_disposition from atm_mst_tcheckpointadd a " +
                    " left join atm_mst_tcheckpointgroup b on a.checkpointgroup_gid = b.checkpointgroup_gid" +
                    " where   a.checkpointgroupadd_gid='" + checkpointgroupadd_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checkpointgroup_gid = objODBCDatareader["checkpointgroup_gid"].ToString();
                values.checkpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                values.checkpointgroupadd_gid = objODBCDatareader["checkpointgroupadd_gid"].ToString();
                values.riskcategory_gid = objODBCDatareader["riskcategory_gid"].ToString();
                values.riskcategory_name = objODBCDatareader["riskcategory_name"].ToString();
                values.positiveconfirmity_gid = objODBCDatareader["positiveconfirmity_gid"].ToString();
                values.positiveconfirmity_name = objODBCDatareader["positiveconfirmity_name"].ToString();            
                values.checkpoint_intent = objODBCDatareader["checkpoint_intent"].ToString();
                values.checkpoint_description = objODBCDatareader["checkpoint_description"].ToString();
                values.noteto_auditor = objODBCDatareader["noteto_auditor"].ToString();
                values.yes_score = objODBCDatareader["yes_score"].ToString();
                values.no_score = objODBCDatareader["no_score"].ToString();
                values.partial_score = objODBCDatareader["partial_score"].ToString();
                values.na_score = objODBCDatareader["na_score"].ToString();
                values.total_score = objODBCDatareader["total_score"].ToString();
                values.yes_disposition = objODBCDatareader["yes_disposition"].ToString();
                values.no_disposition = objODBCDatareader["no_disposition"].ToString();
                values.partial_disposition = objODBCDatareader["partial_disposition"].ToString();
                values.na_disposition = objODBCDatareader["na_disposition"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;


        }
        public void DaUpdateCheckpoint(string employee_gid, MdlAtmMstCheckpointGroup values)
        {
            msSQL = "select checkpointgroupadd_gid from atm_trn_tmultipleauditee where checkpointgroupadd_gid ='" + employee_gid + "' or checkpointgroupadd_gid='" + values.checkpointgroupadd_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add atleast one auditee details";
                return;
            }
            if (values.noteto_auditor == null || values.noteto_auditor == "")
            {
                lsnoteto_auditor = "";
            }
            else
            {
                lsnoteto_auditor = values.noteto_auditor.Replace("'", "");
            }
            msSQL = " update atm_mst_tcheckpointadd set " +
                 " riskcategory_gid='" + values.riskcategory_gid + "'," +
                  " riskcategory_name='" + values.riskcategory_name.Replace("'", "") + "'," +
                   " positiveconfirmity_gid='" + values.positiveconfirmity_gid + "'," +
                  " positiveconfirmity_name='" + values.positiveconfirmity_name.Replace("'", "") + "'," +
                   " checkpoint_intent='" + values.checkpoint_intent.Replace("'", "") + "'," +
                    " checkpoint_description='" + values.checkpoint_description.Replace("'", "") + "'," +
                     " noteto_auditor='" + lsnoteto_auditor.Replace("'", "") + "'," +
                      " yes_score='" + values.yes_score.Replace("'", "") + "'," +
                    " no_score='" + values.no_score.Replace("'", "") + "'," +
                    " partial_score='" + values.partial_score.Replace("'", "") + "'," +
                    " na_score='" + values.na_score.Replace("'", "") + "'," +
                    " total_score='" + values.total_score.Replace("'", "") + "'," +
                     " yes_disposition='" + values.yes_disposition.Replace("'", "") + "'," +
                      " no_disposition='" + values.no_disposition.Replace("'", "") + "'," +
                       " partial_disposition='" + values.partial_disposition.Replace("'", "") + "'," +
                        " na_disposition='" + values.na_disposition.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where    checkpointgroupadd_gid='" + values.checkpointgroupadd_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update atm_trn_tmultipleauditee set checkpointgroupadd_gid='" + values.checkpointgroupadd_gid + "',checkpointgroup_gid = '" + values.checkpointgroup_gid + "' where checkpointgroupadd_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tchecklist2checkpoint set checkpointgroupadd_gid = '" + values.checkpointgroupadd_gid + "' " +
                " where checkpointgroupadd_gid = '" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tchecklist2checkpoint set checkpointgroup_gid = '" + values.checkpointgroup_gid + "' " +
                " where checkpointgroupadd_gid = '" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tsample2checkpoint set checkpointgroupadd_gid = '" + values.checkpointgroupadd_gid + "' " +
                " where checkpointgroupadd_gid = '" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tsample2checkpoint set checkpointgroup_gid = '" + values.checkpointgroup_gid + "' " +
                " where checkpointgroupadd_gid = '" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Checkpoint Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }



        public void DaDeleteCheckpointAdd(string checkpointgroupadd_gid, string employee_gid, MdlAtmMstCheckpointGroup values)
        {
            msSQL = " delete from atm_mst_tcheckpointadd where checkpointgroupadd_gid='" + checkpointgroupadd_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Checkpoint Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("CPDL");
                msSQL = " insert into atm_mst_tcheckpointadddeletelog(" +
                         "checkpointgroupadddeletelog_gid, " +
                         "checkpointgroupadd_gid, " +
                          "checkpointgroup_gid, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + checkpointgroupadd_gid + "', " +
                          "'" + values.checkpointgroupadd_gid + "', " +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }


        public void DaGetCheckpointIntent(string checkpointgroupadd_gid, MdlAtmMstCheckpointGroup values)
        {
            msSQL = " select checkpoint_intent, checkpoint_description  from atm_mst_tcheckpointadd " +
                  " where    checkpointgroupadd_gid='" + checkpointgroupadd_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checkpoint_intent = objODBCDatareader["checkpoint_intent"].ToString();
                values.checkpoint_description = objODBCDatareader["checkpoint_description"].ToString();

            }
            objODBCDatareader.Close();

        }
        public void DaGetCheckpointDescription(string checkpointgroupadd_gid, MdlAtmMstCheckpointGroup values)
        {
            msSQL = " select checkpoint_description  from atm_mst_tcheckpointadd " +
                  " where    checkpointgroupadd_gid='" + checkpointgroupadd_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checkpoint_description = objODBCDatareader["checkpoint_description"].ToString();
            }
            objODBCDatareader.Close();

        }
        public void DaGetCheckpointNotestoAuditor(string checkpointgroupadd_gid, MdlAtmMstCheckpointGroup values)
        {
            msSQL = " select noteto_auditor  from atm_mst_tcheckpointadd " +
                  " where    checkpointgroupadd_gid='" + checkpointgroupadd_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.noteto_auditor = objODBCDatareader["noteto_auditor"].ToString();
            }
            objODBCDatareader.Close();

        }

        public void DaImportExcelCheckpoint(HttpRequest httpRequest, string employee_gid, result objResult, MdlAtmMstCheckpointGroup values)
        {
            try
            {

                msSQL = "select  riskcategory_name from atm_mst_triskcategory ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getriskcategory_info = new List<riskcategoryinfo>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getriskcategory_info.Add(new riskcategoryinfo
                        {
                            lsriskcategory_name = dr_datarow["riskcategory_name"].ToString(),

                        });
                    }
                }
                dt_datatable.Dispose();

                msSQL = "select  positiveconfirmity_name from atm_mst_tpositiveconfirmity";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpositiveconfirmity_info = new List<positiveconfirmityinfo>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpositiveconfirmity_info.Add(new positiveconfirmityinfo
                        {
                            lspositiveconfirmity_name = dr_datarow["positiveconfirmity_name"].ToString(),

                        });
                    }
                }
                dt_datatable.Dispose();

                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string checkpointgroup_gid = httpRequest.Form["checkpointgroup_gid"];
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/Audit/ImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                }

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/ImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid, ms);
                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                excelRange = "A1:" + endRange + rowCount.ToString();

                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);


                /*
                                foreach (DataRow row in dt.Rows)
                                {
                                    var getriskcategoryinfo = getriskcategory_info.Where(x => x.lsriskcategory_name == row["* Risk Category"].ToString()).FirstOrDefault();
                                    var getpositiveconfirmityinfo = getpositiveconfirmity_info.Where(y => y.lspositiveconfirmity_name == row["* Positive Conformity"].ToString()).FirstOrDefault();
                                    //
                                    lscheckpoint_intent = row["* Checkpoint Intent"].ToString();
                                    lscheckpoint_description = row["* Checkpoint Description"].ToString();
                                    lsnoteto_auditor = row["* Note to Auditor"].ToString();

                                    lsyes_disposition = row["* Yes Disposition"].ToString();
                                    lsno_disposition = row["* No Disposition"].ToString();
                                    lspartial_disposition = row["* Partial Disposition"].ToString();
                                    lsna_disposition = row["* NA Disposition"].ToString();

                                    lsyes_score = row["* Yes Score"].ToString();
                                    lsno_score = row["* No Score"].ToString();
                                    lspartial_score = row["* Partial Score"].ToString();
                                    lsna_score = row["* NA Score"].ToString();

                                    //

                                    if (getriskcategoryinfo == null || getpositiveconfirmityinfo == null)
                                    {
                                        objResult.status = false;
                                        objResult.message = "Error occured in uploading Excel Sheet Details ( Master Data may have mismatched....)";

                                    }
                                    else if
                                          ((lscheckpoint_intent == null || lscheckpoint_intent == "") ||
                                            (lscheckpoint_description == null || lscheckpoint_description == "") ||
                                            (lsnoteto_auditor == null || lsnoteto_auditor == "") ||
                                            (lsyes_disposition == null || lsyes_disposition == "") ||
                                            (lsno_disposition == null || lsno_disposition == "") ||
                                            (lspartial_disposition == null || lspartial_disposition == "") ||
                                            (lsna_disposition == null || lsna_disposition == "") ||
                                            (lsyes_score == null || lsyes_score == "") ||
                                            (lsno_score == null || lsno_score == "") ||
                                            (lspartial_score == null || lspartial_score == "") ||
                                             (lsna_score == null || lsna_score == "")
                                           )
                                    {
                                        //body else if

                                        objResult.status = false;
                                        objResult.message = "Error occured in uploading Excel Sheet Details ( Enter All Manditory Fields )";

                                    }
                                }
                                */
                foreach (DataRow row in dt.Rows)
                {
                    var getriskcategoryinfo = getriskcategory_info.Where(x => x.lsriskcategory_name == row["* Risk Category"].ToString()).FirstOrDefault();
                    var getpositiveconfirmityinfo = getpositiveconfirmity_info.Where(y => y.lspositiveconfirmity_name == row["* Positive Conformity"].ToString()).FirstOrDefault();

                    lscheckpoint_intent = row["* Checkpoint Intent"].ToString();
                    lscheckpoint_description = row["* Checkpoint Description"].ToString();
                    lsnoteto_auditor = row["* Note to Auditor"].ToString();

                    lsyes_disposition = row["* Yes Disposition"].ToString();
                    lsno_disposition = row["* No Disposition"].ToString();
                    lspartial_disposition = row["* Partial Disposition"].ToString();
                    lsna_disposition = row["* NA Disposition"].ToString();

                    lsyes_score = row["* Yes Score"].ToString();
                    lsno_score = row["* No Score"].ToString();
                    lspartial_score = row["* Partial Score"].ToString();
                    lsna_score = row["* NA Score"].ToString();

                    lsriskcategory_name = row["* Risk Category"].ToString();
                    msSQL = "select riskcategory_gid from atm_mst_triskcategory where riskcategory_name='" + lsriskcategory_name + "'";
                    lsriskcategory_gid = objdbconn.GetExecuteScalar(msSQL);

                    lspositiveconfirmity_name = row["* Positive Conformity"].ToString();
                    msSQL = "select positiveconfirmity_gid from atm_mst_tpositiveconfirmity where positiveconfirmity_name='" + lspositiveconfirmity_name + "'";
                    lspositiveconfirmity_gid = objdbconn.GetExecuteScalar(msSQL);


                    //if (getriskcategoryinfo != null && getpositiveconfirmityinfo != null)

                    if ((lscheckpoint_intent != null && lscheckpoint_intent != "") &&
                            (getriskcategoryinfo != null || getpositiveconfirmityinfo != null) &&
                               (lscheckpoint_description != null && lscheckpoint_description != "") &&
                               (lsnoteto_auditor != null && lsnoteto_auditor != "") &&
                               (lsyes_disposition != null && lsyes_disposition != "") &&
                               (lsno_disposition != null && lsno_disposition != "") &&
                               (lspartial_disposition != null && lspartial_disposition != "") &&
                               (lsna_disposition != null && lsna_disposition != "") &&
                               (lsyes_score != null && lsyes_score != "") &&
                               (lsno_score != null && lsno_score != "") &&
                               (lspartial_score != null && lspartial_score != "") &&
                               (lsna_score != null && lsna_score != "") &&
                               (lspositiveconfirmity_gid != null && lsriskcategory_gid != "")

                              )
                    {



                        lsriskcategory_name = row["* Risk Category"].ToString();
                        msSQL = "select riskcategory_gid from atm_mst_triskcategory where riskcategory_name='" + lsriskcategory_name + "'";
                        lsriskcategory_gid = objdbconn.GetExecuteScalar(msSQL);

                        lspositiveconfirmity_name = row["* Positive Conformity"].ToString();
                        msSQL = "select positiveconfirmity_gid from atm_mst_tpositiveconfirmity where positiveconfirmity_name='" + lspositiveconfirmity_name + "'";
                        lspositiveconfirmity_gid = objdbconn.GetExecuteScalar(msSQL);

                        //      if (lspositiveconfirmity_gid != null && lsriskcategory_gid != null)
                        //    {

                        lscheckpoint_intent = row["* Checkpoint Intent"].ToString();
                        lscheckpoint_description = row["* Checkpoint Description"].ToString();
                        lsnoteto_auditor = row["* Note to Auditor"].ToString();

                        lsyes_disposition = row["* Yes Disposition"].ToString();
                        lsno_disposition = row["* No Disposition"].ToString();
                        lspartial_disposition = row["* Partial Disposition"].ToString();
                        lsna_disposition = row["* NA Disposition"].ToString();

                        lsyes_score = row["* Yes Score"].ToString();
                        lsno_score = row["* No Score"].ToString();
                        lspartial_score = row["* Partial Score"].ToString();
                        lsna_score = row["* NA Score"].ToString();


                        //lstotal_score = row["* lstotal_score"].ToString();

                        //int a = int.Parse(lsyes_score);
                        double a = double.Parse(lsyes_score, System.Globalization.CultureInfo.InvariantCulture);
                        //int b = int.Parse(lsno_score);
                        double b = double.Parse(lsno_score, System.Globalization.CultureInfo.InvariantCulture);

                        //int c = int.Parse(lspartial_score);

                        double c = double.Parse(lspartial_score, System.Globalization.CultureInfo.InvariantCulture);

                        //int d = int.Parse(lsna_score);

                        double d = double.Parse(lsna_score, System.Globalization.CultureInfo.InvariantCulture);


                        values.total_mark = a.ToString();

                        // var convertDecimal = Convert.ToDecimal(values.total_mark);

                        // Decimal val = Decimal.Truncate(convertDecimal);

                        //double lstotal_score = Math.Round((double) (val), 2);





                        //
                        //
                        msGetGid = objcmnfunctions.GetMasterGID("CHGA");

                        msSQL = " insert into atm_mst_tcheckpointadd(" +
                        "    checkpointgroupadd_gid," +
                        " checkpointgroup_gid," +
                        " riskcategory_gid," +
                        " riskcategory_name," +
                        " positiveconfirmity_gid," +
                        " positiveconfirmity_name," +
                        " checkpoint_intent," +
                        " checkpoint_description," +
                        " noteto_auditor," +
                        " yes_disposition," +
                        " no_disposition," +
                        " partial_disposition," +
                        " na_disposition," +
                        " yes_score," +
                        " no_score," +
                        " partial_score," +
                        " na_score," +
                        " total_score," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + checkpointgroup_gid + "'," +
                        "'" + lsriskcategory_gid + "'," +
                        "'" + lsriskcategory_name + "'," +
                        "'" + lspositiveconfirmity_gid + "'," +
                        "'" + lspositiveconfirmity_name + "'," +
                        "'" + lscheckpoint_intent.Replace("'", "") + "'," +
                        "'" + lscheckpoint_description.Replace("'", "") + "'," +
                        "'" + lsnoteto_auditor.Replace("'", "") + "'," +
                        "'" + lsyes_disposition.Replace("'", "") + "'," +
                        "'" + lsno_disposition.Replace("'", "") + "'," +
                        "'" + lspartial_disposition.Replace("'", "") + "'," +
                        "'" + lsna_disposition.Replace("'", "") + "'," +
                        "'" + lsyes_score + "'," +
                        "'" + lsno_score + "'," +
                        "'" + lspartial_score + "'," +
                        "'" + lsna_score + "'," +
                        //"'" + lstotal_score + "'," +                 
                        "'" + values.total_mark + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGidlog = objcmnfunctions.GetMasterGID("CGUL");

                        msSQL = " insert into atm_mst_tcheckpointaddlog(" +
                        " checkpointaddlog_gid," +
                        " checkpointgroupadd_gid," +
                        " checkpointgroup_gid," +
                        " riskcategory_gid," +
                        " riskcategory_name," +
                        " positiveconfirmity_gid," +
                        " positiveconfirmity_name," +
                        " checkpoint_intent," +
                        " checkpoint_description," +
                        " noteto_auditor," +
                        " yes_disposition," +
                        " no_disposition," +
                        " partial_disposition," +
                        " na_disposition," +
                        " yes_score," +
                        " no_score," +
                        " partial_score," +
                        " na_score," +
                        " total_score," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGidlog + "'," +
                        "'" + msGetGid + "', " +
                        "'" + checkpointgroup_gid + "'," +
                        "'" + lsriskcategory_gid + "'," +
                        "'" + lsriskcategory_name + "'," +
                        "'" + lspositiveconfirmity_gid + "'," +
                        "'" + lspositiveconfirmity_name + "'," +
                        "'" + lscheckpoint_intent.Replace("'", "") + "'," +
                        "'" + lscheckpoint_description.Replace("'", "") + "'," +
                        "'" + lsnoteto_auditor.Replace("'", "") + "'," +
                        "'" + lsyes_disposition.Replace("'", "") + "'," +
                        "'" + lsno_disposition.Replace("'", "") + "'," +
                        "'" + lspartial_disposition.Replace("'", "") + "'," +
                        "'" + lsna_disposition.Replace("'", "") + "'," +
                        "'" + lsyes_score + "'," +
                        "'" + lsno_score + "'," +
                        "'" + lspartial_score + "'," +
                        "'" + lsna_score + "'," +
                        //"'" + lstotal_score + "'," +
                        "'" + values.total_mark + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        insertCount++;


                        // }
                    }
                }

                if (mnResult != 0)
                {

                    objResult.status = true;
                    objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + "Records Uploaded Successfully";

                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Check Master Data And Manditory Fields";

                }

                dt.Dispose();

            }

            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();

            }

        }
        //public void DaImportExcelCheckpoint(HttpRequest httpRequest, string employee_gid, result objResult, MdlAtmMstCheckpointGroup values)
        //{
        //    try
        //    {

        //        msSQL = "select  riskcategory_name from atm_mst_triskcategory ";
        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        var getriskcategory_info = new List<riskcategoryinfo>();
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
        //            {
        //                getriskcategory_info.Add(new riskcategoryinfo
        //                {
        //                    lsriskcategory_name = dr_datarow["riskcategory_name"].ToString(),

        //                });
        //            }
        //        }
        //        dt_datatable.Dispose();

        //        msSQL = "select  positiveconfirmity_name from atm_mst_tpositiveconfirmity";
        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        var getpositiveconfirmity_info = new List<positiveconfirmityinfo>();
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
        //            {
        //                getpositiveconfirmity_info.Add(new positiveconfirmityinfo
        //                {
        //                    lspositiveconfirmity_name = dr_datarow["positiveconfirmity_name"].ToString(),

        //                });
        //            }
        //        }
        //        dt_datatable.Dispose();

        //        HttpFileCollection httpFileCollection;
        //        DataTable dt = null;
        //        string lspath, lsfilePath;
        //        string checkpointgroup_gid = httpRequest.Form["checkpointgroup_gid"];

        //        msSQL = " select company_code from adm_mst_tcompany";
        //        lscompany_code = objdbconn.GetExecuteScalar(msSQL);

        //        // Create Directory
        //        lsfilePath = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/Audit/ImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

        //        if ((!System.IO.Directory.Exists(lsfilePath)))
        //            System.IO.Directory.CreateDirectory(lsfilePath);


        //        httpFileCollection = httpRequest.Files;
        //        for (int i = 0; i < httpFileCollection.Count; i++)
        //        {
        //            httpPostedFile = httpFileCollection[i];
        //        }
        //        string FileExtension = httpPostedFile.FileName;

        //        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
        //        string lsfile_gid = msdocument_gid;
        //        FileExtension = Path.GetExtension(FileExtension).ToLower();
        //        lsfile_gid = lsfile_gid + FileExtension;

        //        Stream ls_readStream;
        //        ls_readStream = httpPostedFile.InputStream;
        //        MemoryStream ms = new MemoryStream();
        //        ls_readStream.CopyTo(ms);



        //        //path creation        
        //        lspath = lsfilePath + "/";
        //        FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
        //        ms.WriteTo(file);

        //        using (ExcelPackage xlPackage = new ExcelPackage(ms))
        //        {
        //            ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
        //            rowCount = worksheet.Dimension.End.Row;
        //            columnCount = worksheet.Dimension.End.Column;
        //            endRange = worksheet.Dimension.End.Address;
        //        }

        //        bool status;
        //        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/ImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid, ms);
        //        file.Close();
        //        ms.Close();

        //        objcmnfunctions.uploadFile(lspath, lsfile_gid);

        //        //Excel To DataTable

        //        lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

        //        excelRange = "A1:" + endRange + rowCount.ToString();

        //        dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);



        //        foreach (DataRow row in dt.Rows)
        //        {
        //            var getriskcategoryinfo = getriskcategory_info.Where(x => x.lsriskcategory_name == row["* Risk Category"].ToString()).FirstOrDefault();
        //            var getpositiveconfirmityinfo = getpositiveconfirmity_info.Where(y => y.lspositiveconfirmity_name == row["* Positive Conformity"].ToString()).FirstOrDefault();

        //            if (getriskcategoryinfo == null || getpositiveconfirmityinfo == null)
        //            {
        //                objResult.status = false;
        //                objResult.message = "Error occured in uploading Excel Sheet Details ( Master Data may have mismatched....)";

        //            }
        //        }

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            var getriskcategoryinfo = getriskcategory_info.Where(x => x.lsriskcategory_name == row["* Risk Category"].ToString()).FirstOrDefault();
        //            var getpositiveconfirmityinfo = getpositiveconfirmity_info.Where(y => y.lspositiveconfirmity_name == row["* Positive Conformity"].ToString()).FirstOrDefault();

        //            if (getriskcategoryinfo != null || getpositiveconfirmityinfo != null)
        //            {



        //                lsriskcategory_name = row["* Risk Category"].ToString();
        //                msSQL = "select riskcategory_gid from atm_mst_triskcategory where riskcategory_name='" + lsriskcategory_name + "'";
        //                lsriskcategory_gid = objdbconn.GetExecuteScalar(msSQL);

        //                lspositiveconfirmity_name = row["* Positive Conformity"].ToString();
        //                msSQL = "select positiveconfirmity_gid from atm_mst_tpositiveconfirmity where positiveconfirmity_name='" + lspositiveconfirmity_name + "'";
        //                lspositiveconfirmity_gid = objdbconn.GetExecuteScalar(msSQL);

        //                lscheckpoint_intent = row["* Checkpoint Intent"].ToString();
        //                lscheckpoint_description = row["* Checkpoint Description"].ToString();
        //                lsnoteto_auditor = row["* Note to Auditor"].ToString();

        //                lsyes_disposition = row["* Yes Disposition"].ToString();
        //                lsno_disposition = row["* No Disposition"].ToString();
        //                lspartial_disposition = row["* Partial Disposition"].ToString();
        //                lsna_disposition = row["* NA Disposition"].ToString();

        //                lsyes_score = row["* Yes Score"].ToString();
        //                lsno_score = row["* No Score"].ToString();
        //                lspartial_score = row["* Partial Score"].ToString();
        //                lsna_score = row["* NA Score"].ToString();
        //                //lstotal_score = row["* lstotal_score"].ToString();

        //                //int a = int.Parse(lsyes_score);
        //                double a = double.Parse(lsyes_score, System.Globalization.CultureInfo.InvariantCulture);
        //                //int b = int.Parse(lsno_score);
        //                double b = double.Parse(lsno_score, System.Globalization.CultureInfo.InvariantCulture);

        //                //int c = int.Parse(lspartial_score);

        //                double c = double.Parse(lspartial_score, System.Globalization.CultureInfo.InvariantCulture);

        //                //int d = int.Parse(lsna_score);

        //                double d = double.Parse(lsna_score, System.Globalization.CultureInfo.InvariantCulture);


        //                values.total_mark = (a + b + c + d).ToString();

        //                // var convertDecimal = Convert.ToDecimal(values.total_mark);

        //                // Decimal val = Decimal.Truncate(convertDecimal);

        //                //double lstotal_score = Math.Round((double) (val), 2);


        //                msGetGid = objcmnfunctions.GetMasterGID("CHGA");

        //                msSQL = " insert into atm_mst_tcheckpointadd(" +
        //                "    checkpointgroupadd_gid," +
        //                " checkpointgroup_gid," +
        //                " riskcategory_gid," +
        //                " riskcategory_name," +
        //                " positiveconfirmity_gid," +
        //                " positiveconfirmity_name," +
        //                " checkpoint_intent," +
        //                " checkpoint_description," +
        //                " noteto_auditor," +
        //                " yes_disposition," +
        //                " no_disposition," +
        //                " partial_disposition," +
        //                " na_disposition," +
        //                " yes_score," +
        //                " no_score," +
        //                " partial_score," +
        //                " na_score," +
        //                " total_score," +
        //                " created_by," +
        //                " created_date)" +
        //                " values(" +
        //                "'" + msGetGid + "'," +
        //                "'" + checkpointgroup_gid + "'," +
        //                "'" + lsriskcategory_gid + "'," +
        //                "'" + lsriskcategory_name + "'," +
        //                "'" + lspositiveconfirmity_gid + "'," +
        //                "'" + lspositiveconfirmity_name + "'," +
        //                "'" + lscheckpoint_intent.Replace("'", "") + "'," +
        //                "'" + lscheckpoint_description.Replace("'", "") + "'," +
        //                "'" + lsnoteto_auditor.Replace("'", "") + "'," +
        //                "'" + lsyes_disposition.Replace("'", "") + "'," +
        //                "'" + lsno_disposition.Replace("'", "") + "'," +
        //                "'" + lspartial_disposition.Replace("'", "") + "'," +
        //                "'" + lsna_disposition.Replace("'", "") + "'," +
        //                "'" + lsyes_score + "'," +
        //                "'" + lsno_score + "'," +
        //                "'" + lspartial_score + "'," +
        //                "'" + lsna_score + "'," +
        //                //"'" + lstotal_score + "'," +                 
        //                "'" + values.total_mark + "'," +
        //                "'" + employee_gid + "'," +
        //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //                msGetGidlog = objcmnfunctions.GetMasterGID("CGUL");

        //                msSQL = " insert into atm_mst_tcheckpointaddlog(" +
        //                " checkpointaddlog_gid," +
        //                " checkpointgroupadd_gid," +
        //                " checkpointgroup_gid," +
        //                " riskcategory_gid," +
        //                " riskcategory_name," +
        //                " positiveconfirmity_gid," +
        //                " positiveconfirmity_name," +
        //                " checkpoint_intent," +
        //                " checkpoint_description," +
        //                " noteto_auditor," +
        //                " yes_disposition," +
        //                " no_disposition," +
        //                " partial_disposition," +
        //                " na_disposition," +
        //                " yes_score," +
        //                " no_score," +
        //                " partial_score," +
        //                " na_score," +
        //                " total_score," +
        //                " created_by," +
        //                " created_date)" +
        //                " values(" +
        //                "'" + msGetGidlog + "'," +
        //                "'" + msGetGid + "', " +
        //                "'" + checkpointgroup_gid + "'," +
        //                "'" + lsriskcategory_gid + "'," +
        //                "'" + lsriskcategory_name + "'," +
        //                "'" + lspositiveconfirmity_gid + "'," +
        //                "'" + lspositiveconfirmity_name + "'," +
        //                "'" + lscheckpoint_intent.Replace("'", "") + "'," +
        //                "'" + lscheckpoint_description.Replace("'", "") + "'," +
        //                "'" + lsnoteto_auditor.Replace("'", "") + "'," +
        //                "'" + lsyes_disposition.Replace("'", "") + "'," +
        //                "'" + lsno_disposition.Replace("'", "") + "'," +
        //                "'" + lspartial_disposition.Replace("'", "") + "'," +
        //                "'" + lsna_disposition.Replace("'", "") + "'," +
        //                "'" + lsyes_score + "'," +
        //                "'" + lsno_score + "'," +
        //                "'" + lspartial_score + "'," +
        //                "'" + lsna_score + "'," +
        //                //"'" + lstotal_score + "'," +
        //                "'" + values.total_mark + "'," +
        //                "'" + employee_gid + "'," +
        //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //                insertCount++;
        //            }


        //        }


        //        if (mnResult != 0)
        //        {

        //            objResult.status = true;
        //            objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";

        //        }
        //        else
        //        {
        //            objResult.status = false;
        //            objResult.message = "Error occured in uploading Excel Sheet Details";

        //        }

        //        dt.Dispose();

        //    }

        //    catch (Exception ex)
        //    {
        //        objResult.status = false;
        //        objResult.message = ex.ToString();

        //    }

        //}
        public void DaCreateCheckListToCheckpoint(checklistcheckpoint values, string employee_gid)
        {
            if (values.checklist_name == null || values.checklist_name == "")
            {
                lschecklist_name = "";
            }
            else
            {
                lschecklist_name = values.checklist_name.Replace("'", "");
            }

            msGetGid = objcmnfunctions.GetMasterGID("C2CP");
            msSQL = " insert into atm_trn_tchecklist2checkpoint(" +
                    " checklist2checkpoint," +
                    " checkpointgroupadd_gid," +
                    " checklist_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + lschecklist_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msGetGid1 = objcmnfunctions.GetMasterGID("S2CP");
            msSQL = " insert into atm_trn_tsample2checkpoint(" +
                    " sample2checkpoint," +
                    " checklist2checkpoint," +
                    " checkpointgroupadd_gid," +
                    " checklist_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid1 + "'," +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + lschecklist_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Check List Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }
        public void DaUpdateCheckListToCheckpoint(checklistsamplecheckpoint values, string employee_gid)
        {
            if (values.checklist_name == null || values.checklist_name == "")
            {
                lschecklist_name = "";
            }
            else
            {
                lschecklist_name = values.checklist_name.Replace("'", "");
            }

            msGetGid = objcmnfunctions.GetMasterGID("C2CP");
            msSQL = " insert into atm_trn_tchecklist2checkpoint(" +
                    " checklist2checkpoint," +
                    " checkpointgroupadd_gid," +
                    " checklist_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.checkpointgroupadd_gid + "'," +
                    "'" + lschecklist_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msGetGid1 = objcmnfunctions.GetMasterGID("S2CP");
            msSQL = " insert into atm_trn_tsample2checkpoint(" +
                    " sample2checkpoint," +
                    " checklist2checkpoint," +
                    " checkpointgroupadd_gid," +
                    " checklist_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid1 + "'," +
                    "'" + msGetGid + "'," +
                    "'" + values.checkpointgroupadd_gid + "'," +
                    "'" + lschecklist_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Check List Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }

        public void DaGetCheckListToCheckpoint(string employee_gid, checklistcheckpoint values)
        {
            try
            {
                msSQL = "select checklist2checkpoint,checklist_name,checkpointgroupadd_gid from atm_trn_tchecklist2checkpoint where " +
                   " checkpointgroupadd_gid ='" + employee_gid + "' and auditcreation_gid  is null";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                        {
                            checklist2checkpoint = (dr_datarow["checklist2checkpoint"].ToString()),
                            checklist_name = (dr_datarow["checklist_name"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),

                        });
                    }
                    values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetCheckListToCheckpointView(string checkpointgroupadd_gid, checklistcheckpoint values)
        {
            try
            {
                msSQL = "select checklist2checkpoint,checklist_name,checkpointgroupadd_gid from atm_trn_tchecklist2checkpoint where " +
                   " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "' and auditcreation_gid  is null";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                        {
                            checklist2checkpoint = (dr_datarow["checklist2checkpoint"].ToString()),
                            checklist_name = (dr_datarow["checklist_name"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),

                        });
                    }
                    values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetSampleToCheckpointCreate(string checkpointgroupadd_gid, checklistcheckpoint values)
        {
            try
            {
                msSQL = "select sample2checkpoint,checklist_name,checklist_verified,checkpointgroupadd_gid from atm_trn_tsample2checkpoint where " +
                   " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                        {
                            sample2checkpoint = (dr_datarow["sample2checkpoint"].ToString()),
                            checklist_name = (dr_datarow["checklist_name"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                            checklist_verified = (dr_datarow["checklist_verified"].ToString()),

                        });
                    }
                    values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetChecklistToCheckpointCreate(string checkpointgroupadd_gid,string auditcreation_gid, checklistcheckpoint values)
        {
            try
            {
                msSQL = " select auditcreation_gid from atm_trn_tchecklist2checkpoint where " +
                 " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "' and auditcreation_gid ='" + auditcreation_gid + "'";
                values.auditcreation_gid = objdbconn.GetExecuteScalar(msSQL);
                if (values.auditcreation_gid == "")
                {
                    msSQL = "select checklist2checkpoint,auditcreation_gid,checklist_name,overall_detail,checklistverified_flag,checkpointgroupadd_gid from atm_trn_tchecklist2checkpoint where " +
                   " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "'and auditcreation_gid is null";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                            {
                                checklist2checkpoint = (dr_datarow["checklist2checkpoint"].ToString()),
                                checklist_name = (dr_datarow["checklist_name"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                                overall_detail = (dr_datarow["overall_detail"].ToString()),
                                checklistverified_flag = (dr_datarow["checklistverified_flag"].ToString()),

                            });
                        }
                        values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
                else
                {
                    msSQL = "select checklist2checkpoint,checklist_name,overall_detail,checklistverified_flag,checkpointgroupadd_gid from atm_trn_tchecklist2checkpoint where " +
                   " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "' and auditcreation_gid = '" + auditcreation_gid + "'";
                    
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                            {
                                checklist2checkpoint = (dr_datarow["checklist2checkpoint"].ToString()),
                                checklist_name = (dr_datarow["checklist_name"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                                overall_detail = (dr_datarow["overall_detail"].ToString()),
                                checklistverified_flag = (dr_datarow["checklistverified_flag"].ToString()),
                            });
                        }
                        values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaPostChecklistAssign(checklistcheckpoint values, string employee_gid)
        {

           

            foreach (string i in values.checkpointgroupadd_gid)
            {
               
                msSQL = "select checklist2checkpoint,checklist_name,checkpointgroupadd_gid from atm_trn_tchecklist2checkpoint where " +
                  " checkpointgroupadd_gid = '" + i + "'  order by checklist2checkpoint desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("AC2C");

                        msSQL = " insert into atm_trn_tauditcreation2checklist(" +
                                " auditcreation2checklist_gid," +
                                " checklistmasteradd_gid," +
                                " checklistmaster_gid," +
                                " checkpointgroupadd_gid," +
                                " auditcreation_gid," +
                                " auditdepartment_name ," +
                                " audittype_name ," +
                                " checkpointgroup_name," +
                                " audit_name ," +
                                " checkpoint_intent," +
                                " checkpoint_description ," +
                                " riskcategory_name," +
                                " positiveconfirmity_name ," +
                                " noteto_auditor ," +
                                " yes_score ," +
                                " no_score ," +
                                " partial_score ," +
                                " na_score," +
                                " total_score ," +
                                " yes_disposition, " +
                                " no_disposition, " +
                                " partial_disposition, " +
                                " na_disposition, " +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + i + "', " +
                                "'" + dt["checklistmaster_gid"].ToString() + "'," +
                                 "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
                                "'" + dt["auditcreation_gid"].ToString() + "'," +
                                "'" + dt["auditdepartment_name"].ToString() + "'," +
                                "'" + dt["audittype_name"].ToString() + "'," +
                                "'" + dt["checkpointgroup_name"].ToString() + "'," +
                                "'" + dt["audit_name"].ToString() + "'," +
                                "'" + dt["checkpoint_intent"].ToString() + "'," +
                                "'" + dt["checkpoint_description"].ToString() + "'," +
                                "'" + dt["riskcategory_name"].ToString() + "'," +
                                "'" + dt["positiveconfirmity_name"].ToString() + "'," +
                                "'" + dt["noteto_auditor"].ToString() + "'," +
                                "'" + dt["yes_score"].ToString() + "'," +
                                "'" + dt["no_score"].ToString() + "'," +
                                "'" + dt["partial_score"].ToString() + "'," +
                                "'" + dt["na_score"].ToString() + "'," +
                                "'" + dt["total_score"].ToString() + "'," +
                                "'" + dt["yes_disposition"].ToString() + "'," +
                                "'" + dt["no_disposition"].ToString() + "'," +
                                "'" + dt["partial_disposition"].ToString() + "'," +
                                "'" + dt["na_disposition"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Check List Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }
        public void DaDeleteChecklist2Checkpoint(string checklist2checkpoint, string employee_gid, MdlAtmMstCheckpointGroup values)
        {
            msSQL = " delete from atm_trn_tchecklist2checkpoint where checklist2checkpoint='" + checklist2checkpoint + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from atm_trn_tsample2checkpoint where checklist2checkpoint='" + checklist2checkpoint + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Checkpoint Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("CPDL");
                msSQL = " insert into atm_mst_tcheckpointadddeletelog(" +
                         "checkpointgroupadddeletelog_gid, " +
                         "checkpointgroupadd_gid, " +
                          "checkpointgroup_gid, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + checklist2checkpoint + "', " +
                          "'" + values.checkpointgroupadd_gid + "', " +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetCheckpointList(string employee_gid, checklistcheckpoint values)
        {
            msSQL = "select checklist2checkpoint,checkpointgroupadd_gid,checklist_name " +
                    " from atm_trn_tchecklist2checkpoint where " +
                    " checkpointgroupadd_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                    {
                        checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                        checklist2checkpoint = (dr_datarow["checklist2checkpoint"].ToString()),
                        checklist_name = (dr_datarow["checklist_name"].ToString()),
                    });
                }
            }
            values.checklistcheckpoint_list = getchecklistcheckpoint_list;
            dt_datatable.Dispose();
        }

        public void DaGetTempCheckpointCheckList(string checkpointgroupadd_gid, string employee_gid, checklistcheckpoint values)
        {
            msSQL = "select checklist2checkpoint,checkpointgroupadd_gid,checklist_name from atm_trn_tchecklist2checkpoint" +
                    " where checkpointgroupadd_gid='" + employee_gid + "' or checkpointgroupadd_gid='" + checkpointgroupadd_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                    {
                        checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                        checklist2checkpoint = (dr_datarow["checklist2checkpoint"].ToString()),
                        checklist_name = (dr_datarow["checklist_name"].ToString()),
                    });
                }
                values.checklistcheckpoint_list = getchecklistcheckpoint_list;

            }
            dt_datatable.Dispose();
        }


        public void DaDeleteCheckpointList(string checklist2checkpoint, checklistcheckpoint values)
        {
            msSQL = "delete from atm_trn_tchecklist2checkpoint where checklist2checkpoint='" + checklist2checkpoint + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from atm_trn_tsample2checkpoint where checklist2checkpoint='" + checklist2checkpoint + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Checkpoint deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while deleting the Checkpoint";
                values.status = false;

            }
        }
        public void DaTempDeleteCheckpointList(string employee_gid, checklistcheckpoint values)
        {
            msSQL = "delete from atm_trn_tchecklist2checkpoint where checkpointgroupadd_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from atm_trn_tsample2checkpoint where checkpointgroupadd_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Checkpoint deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while deleting the Checkpoint";
                values.status = false;

            }
        }
        public void DaGetCheckpointgroupActive(MdlAtmMstCheckpointGroup values)
        {
            try
            {
                msSQL = "select checkpointgroup_gid,checkpointgroup_name from atm_mst_checkpointgroup where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcheckpointgroupadd_list = new List<checkpointgroupadd_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcheckpointgroupadd_list.Add(new checkpointgroupadd_list
                        {
                            checkpointgroup_gid = (dr_datarow["checkpointgroup_gid"].ToString()),
                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                        });
                    }
                    values.checkpointgroupadd_list = getcheckpointgroupadd_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetCheckpointCheckList(string checkpointgroupadd_gid,checklistcheckpoint values)
        {
            msSQL = "select checklist2checkpoint,checkpointgroupadd_gid,checklist_name from atm_trn_tchecklist2checkpoint" +
                    " where checkpointgroupadd_gid='" + checkpointgroupadd_gid + "' and auditcreation_gid is null ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                    {
                        checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                        checklist2checkpoint = (dr_datarow["checklist2checkpoint"].ToString()),
                        checklist_name = (dr_datarow["checklist_name"].ToString()),
                    });
                }
                values.checklistcheckpoint_list = getchecklistcheckpoint_list;

            }
            dt_datatable.Dispose();
        }
        public bool DaPostMultipleAuditee(multipleauditee values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("MUAU");
            msSQL = " insert into atm_trn_tmultipleauditee(" +
                    " multipleauditee_gid," +
                    " checkpointgroupadd_gid," +
                    " auditeemaker_gid," +
                     "auditeemaker_name," +
                    " auditeechecker_gid," +
                    " auditeechecker_name," +
                   " auditeeapproval_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.auditeemaker_gid + "'," +
                     "'" + values.auditeemaker_name + "'," +
                    "'" + values.auditeechecker_gid + "'," +
                     "'" + values.auditeechecker_name + "'," +
                      "' --'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Auditee Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetAuditeeSummaryList(string checkpointgroupadd_gid, multipleauditee values)
        {
            try
            {
                msSQL = "select multipleauditee_gid,checkpointgroupadd_gid,auditeechecker_name,auditeeapproval_status,auditeechecker_gid,auditeemaker_gid,auditeemaker_name from atm_trn_tmultipleauditee where " +
                   " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmultipleauditee_list = new List<multipleauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmultipleauditee_list.Add(new multipleauditee_list
                        {
                            multipleauditee_gid = (dr_datarow["multipleauditee_gid"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            auditeechecker_approvalstatus = (dr_datarow["auditeeapproval_status"].ToString()),

                        });
                    }
                    values.multipleauditee_list = getmultipleauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetAuditeeCheckpointSummaryList(string checkpointgroupadd_gid, multipleauditee values)
        {
            try
            {
                msSQL = "select multipleauditee_gid,checkpointgroupadd_gid,auditeeapproval_status,auditeechecker_name,auditeechecker_gid,auditeemaker_gid,auditeemaker_name from atm_trn_tmultipleauditee where " +
                   " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmultipleauditee_list = new List<multipleauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmultipleauditee_list.Add(new multipleauditee_list
                        {
                            multipleauditee_gid = (dr_datarow["multipleauditee_gid"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            auditeechecker_approvalstatus = (dr_datarow["auditeeapproval_status"].ToString()),

                        });
                    }
                    values.multipleauditee_list = getmultipleauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public bool DaMultipleAuditeeEdit(multipleauditee values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("MUAU");
            msSQL = " insert into atm_trn_tmultipleauditee(" +
                    " multipleauditee_gid," +
                    " checkpointgroupadd_gid," +
                    " auditeemaker_gid," +
                     "auditeemaker_name," +
                    " auditeechecker_gid," +
                    " auditeechecker_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.auditeemaker_gid + "'," +
                     "'" + values.auditeemaker_name + "'," +
                    "'" + values.auditeechecker_gid + "'," +
                     "'" + values.auditeechecker_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            objdbconn.CloseConn();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Auditee added successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error occured";
                return false;
            }
        }
        public void DaTempDeleteAuditee(string employee_gid, multipleauditee values)
        {
            msSQL = "delete from atm_trn_tmultipleauditee where checkpointgroupadd_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Auditee deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while deleting the Auditee";
                values.status = false;

            }
        }
        public void DaDeleteAuditee(string multipleauditee_gid, multipleauditee values)
        {
            msSQL = "delete from atm_trn_tmultipleauditee where multipleauditee_gid='" + multipleauditee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Auditee deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while deleting the Auditee";
                values.status = false;

            }
        }
        public void DaGetTempAssignedAuditeeList(string checkpointgroupadd_gid, string employee_gid, multipleauditee values)
        {
            values.employee_gid = employee_gid;

            {
                msSQL = "select multipleauditee_gid,auditeemaker_gid,auditeemaker_name,auditeechecker_name,auditeechecker_gid,auditeechecker_approvalflag,checkpointgroupadd_gid from atm_trn_tmultipleauditee where " +
                   " checkpointgroupadd_gid ='" + employee_gid + "' or checkpointgroupadd_gid='" + checkpointgroupadd_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmultipleauditee_list = new List<multipleauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmultipleauditee_list.Add(new multipleauditee_list
                        {
                            multipleauditee_gid = (dr_datarow["multipleauditee_gid"].ToString()),
                            auditeechecker_approvalflag = (dr_datarow["auditeechecker_approvalflag"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                        });
                    }
                    values.multipleauditee_list = getmultipleauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
        }
        public void DaDeleteAuditeeList(string multipleauditee_gid, multipleauditee values)
        {
            msSQL = "delete from atm_trn_tmultipleauditee where multipleauditee_gid='" + multipleauditee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Auditee deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while deleting the auditee";
                values.status = false;

            }
        }
        public void DaGetAuditeeList(string employee_gid, multipleauditee values)
        {
            try
            {
                msSQL = "select multipleauditee_gid,checkpointgroupadd_gid,auditeechecker_name,auditeechecker_gid,auditeemaker_gid,auditeemaker_name from atm_trn_tmultipleauditee where " +
                   " checkpointgroupadd_gid ='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmultipleauditee_list = new List<multipleauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmultipleauditee_list.Add(new multipleauditee_list
                        {
                            multipleauditee_gid = (dr_datarow["multipleauditee_gid"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                        });
                    }
                    values.multipleauditee_list = getmultipleauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetAuditeecheckpointList(string checkpointgroup_gid, multipleauditee values)
        {
            try
            {
                msSQL = "select multipleauditee_gid,checkpointgroupadd_gid,auditeechecker_name,auditeechecker_gid,auditeemaker_gid,auditeemaker_name from atm_trn_tmultipleauditee where " +
                   " checkpointgroup_gid ='" + checkpointgroup_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmultipleauditee_list = new List<multipleauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmultipleauditee_list.Add(new multipleauditee_list
                        {
                            multipleauditee_gid = (dr_datarow["multipleauditee_gid"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),

                        });
                    }
                    values.multipleauditee_list = getmultipleauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
    }
    }