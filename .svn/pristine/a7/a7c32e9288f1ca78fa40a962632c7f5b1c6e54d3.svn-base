using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
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
    public class DaAtmMstChecklistMaster
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetGid1, lsauditdepartment_value, lscheckpointgroup_gid, lsyes_score, lsno_score, lsnoteto_auditor, lspartial_score, lsna_score, lsauditname_value, lscompany_code,
            lscheckpoint_intent, lscheckpoint_description, lsriskcategory_name, lspositiveconfirmity_name, lsriskcategory_gid, lspositiveconfirmity_gid, lstotal_score,
            lsyes_disposition, lsno_disposition, msGetcheckpoint2groupname_gid, lscheckpoint_group, lspartial_disposition, lsna_disposition, lsentity_name, lsauditdepartment_name, lscheckpointgroup_name, lsaudittype_name;
        string excelRange, endRange;
        int rowCount, columnCount;
        int insertCount = 0, logCount = 0;
        string checklistexcelimportlog_message = "";
        string lserrflag = "No";
        int mnResult;

        public void DaGetChecklistMaster(MdlAtmMstChecklistMaster values)
        {
            try
            {

                msSQL = " SELECT distinct a.checklistmaster_gid,a.entity_name,a.auditdepartment_name,a.checkpointgroup_gid,a.audit_description,e.auditdelete_flag,a.audittype_name,a.checkpointgroup_name,a.audit_name,a.checklist_uniqueno,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,concat(d.user_firstname,' ',d.user_lastname,' / ',d.user_code) as employee_name" +
                        " FROM atm_mst_tchecklistmaster a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser d on d.user_gid=b.user_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join atm_trn_tauditcreation e on e.checklistmaster_gid = a.checklistmaster_gid order by a.checklistmaster_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getchecklistmaster_list = new List<checklistmaster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getchecklistmaster_list.Add(new checklistmaster_list
                        {
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            checklist_uniqueno = (dr_datarow["checklist_uniqueno"].ToString()),
                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                            entity_name = (dr_datarow["entity_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_description = (dr_datarow["audit_description"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            auditdelete_flag = (dr_datarow["auditdelete_flag"].ToString()),
                            checkpointgroup_gid = (dr_datarow["checkpointgroup_gid"].ToString()),

                        });
                    }
                    values.checklistmaster_list = getchecklistmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaPostChecklistMaster(MdlAtmMstChecklistMaster values, string employee_gid)
        {

            if (values.checkpointgroupadd_list != null)
            {
                for (var i = 0; i < values.checkpointgroupadd_list.Count; i++)
                {
                    lscheckpointgroup_gid += values.checkpointgroupadd_list[i].checkpointgroup_gid + ",";
                    lscheckpointgroup_name += values.checkpointgroupadd_list[i].checkpointgroup_name + ",";
                }
                lscheckpointgroup_gid = lscheckpointgroup_gid.TrimEnd(',');
                lscheckpointgroup_name = lscheckpointgroup_name.TrimEnd(',');
            }
            string[] sampleimportgid_array = lscheckpointgroup_gid.Split(',');

            msGetGid1 = objcmnfunctions.GetMasterGID("CLIS");

            msSQL = " insert into atm_mst_tchecklistmaster(" +
                    " checklistmaster_gid," +
                    " checklist_uniqueno ," +
                    " entity_gid ," +
                    " entity_name," +
                    " auditdepartment_gid ," +
                    " auditdepartment_name," +
                    //" audittype_gid ," +
                    //" audittype_name," +
                    " checkpointgroup_gid ," +
                    " checkpointgroup_name," +
                    " audit_name," +
                     " audit_description," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid1 + "'," +
                     "'" + msGetGid1 + "'," +
                    "'" + values.entity_gid + "', " +
                    "'" + values.entity_name.Replace("'", "") + "'," +
                      "'" + values.auditdepartment_gid + "', " +
                    "'" + values.auditdepartment_name.Replace("'", "") + "'," +
                    //  "'" + values.audittype_gid + "', " +
                    //"'" + values.audittype_name.Replace("'", "") + "'," +
                      "'" + lscheckpointgroup_gid + "', " +
                    "'" + lscheckpointgroup_name.Replace("'", "") + "'," +
                         "'" + values.audit_name.Replace("'", "") + "'," +
                         "'" + values.audit_description.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

                int array_length = sampleimportgid_array.Length;
                string sample_querystring = "";
                for (int i = 0; i < sampleimportgid_array.Length; i++)
                {
                    if (i == sampleimportgid_array.Length - 1)
                    {
                        sample_querystring = sample_querystring + "'" + sampleimportgid_array[i] + "'";
                    }
                    else
                    {
                        sample_querystring = sample_querystring + "'" + sampleimportgid_array[i] + "',";
                    }
                }

                msSQL = " SELECT distinct a.checkpointgroup_gid,a.riskcategory_gid,a.checkpointgroupadd_gid,d.checkpointgroup_name,a.riskcategory_name,a.positiveconfirmity_gid,a.positiveconfirmity_name,a.checkpoint_intent,a.checkpoint_description,a.noteto_auditor,a.yes_score,a.no_score,a.partial_score,a.na_score,a.total_score,a.yes_disposition,a.no_disposition,a.partial_disposition,a.na_disposition,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                       " FROM atm_mst_tcheckpointadd a" +
                       " left join atm_mst_tcheckpointgroup d on d.checkpointgroup_gid = a.checkpointgroup_gid" +
                       " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        "  where a.checkpointgroup_gid in (" + sample_querystring + ") ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("CLAD");
                        msSQL = " insert into atm_mst_tchecklistmasteradd(" +
                                " checklistmasteradd_gid," +
                                " checklistmaster_gid ," +
                               " checkpointgroup_gid," +
                                  " checkpointgroupadd_gid," +
                                  " checkpointgroup_name," +
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
                           "'" + msGetGid1 + "', " +
                            "'" + dt["checkpointgroup_gid"].ToString() + "'," +
                              "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
                              "'" + dt["checkpointgroup_name"].ToString() + "'," +
                            "'" + dt["riskcategory_gid"].ToString() + "'," +
                             "'" + dt["riskcategory_name"].ToString() + "'," +
                              "'" + dt["positiveconfirmity_gid"].ToString() + "'," +
                              "'" + dt["positiveconfirmity_name"].ToString() + "'," +
                               "'" + dt["checkpoint_intent"].ToString() + "'," +
                                "'" + dt["checkpoint_description"].ToString() + "'," +
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

                    msSQL = " update atm_mst_tcheckpointadd set checkpointgroup_flag='Y'" +
                     " where checkpointgroup_gid = '" + lscheckpointgroup_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Audit Created Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }
        public void DaGetChecklistMasterAdd(string checklistmaster_gid, MdlAtmMstChecklistMaster values)
        {
            try
            {
                msSQL = " SELECT distinct a.checklistmaster_gid,a.checkpointgroup_gid,a.checkpointgroup_name, a.checklistmasteradd_gid, e.auditdelete_flag, " +
                        " a.riskcategory_name,a.positiveconfirmity_name,a.checkpoint_intent,a.checkpoint_description, " +
                        " a.noteto_auditor,a.yes_score,a.no_score,a.partial_score,a.na_score,a.total_score, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_mst_tchecklistmasteradd a" +
                        " left join atm_mst_tchecklistmaster d on d.checklistmaster_gid = a.checklistmaster_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join atm_trn_tauditcreation e on e.checklistmaster_gid = a.checklistmaster_gid" +
                        " where  d.checklistmaster_gid='" + checklistmaster_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getchecklistmasteradd_list = new List<checklistmasteradd_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getchecklistmasteradd_list.Add(new checklistmasteradd_list
                        {
                            checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
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
                            auditdelete_flag = (dr_datarow["auditdelete_flag"].ToString()),


                        });
                    }
                    values.checklistmasteradd_list = getchecklistmasteradd_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostChecklistMasterAdd(MdlAtmMstChecklistMaster values, string employee_gid)
        {
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

            msGetGid = objcmnfunctions.GetMasterGID("CLAD");
            msSQL = " insert into atm_mst_tchecklistmasteradd(" +
                    " checklistmasteradd_gid," +
                    " checklistmaster_gid ," +
                    " riskcategory_gid," +
                    " riskcategory_name ," +
                     " positiveconfirmity_gid," +
                    " positiveconfirmity_name ," +
                      " checkpointgroup_gid," +
                    " checkpointgroup_name ," +
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
                    "'" + values.checklistmaster_gid + "', " +
                    "'" + values.riskcategory_gid + "'," +
                       "'" + values.riskcategory_name.Replace("'", "") + "'," +
                        "'" + values.positiveconfirmity_gid + "'," +
                       "'" + values.positiveconfirmity_name.Replace("'", "") + "'," +
                        "'" + values.checkpointgroup_gid + "'," +
                       "'" + values.checkpointgroup_name.Replace("'", "") + "'," +
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
        public void DaEditChecklistMaster(string checklistmasteradd_gid, MdlAtmMstChecklistMaster values)
        {
            msSQL = " select checklistmasteradd_gid,riskcategory_name,riskcategory_gid,positiveconfirmity_name,positiveconfirmity_gid,checkpointgroup_gid,checkpointgroup_name,checkpoint_intent, checkpoint_description,noteto_auditor,yes_score,no_score,partial_score,na_score,total_score,yes_disposition,no_disposition,partial_disposition,na_disposition from atm_mst_tchecklistmasteradd " +
                    " where checklistmasteradd_gid='" + checklistmasteradd_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checklistmasteradd_gid = objODBCDatareader["checklistmasteradd_gid"].ToString();
                values.riskcategory_gid = objODBCDatareader["riskcategory_gid"].ToString();
                values.riskcategory_name = objODBCDatareader["riskcategory_name"].ToString();
                values.checkpointgroup_gid = objODBCDatareader["checkpointgroup_gid"].ToString();
                values.checkpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
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
        public void DaUpdateChecklistMaster(string employee_gid, MdlAtmMstChecklistMaster values)
        {
            if (values.noteto_auditor == null || values.noteto_auditor == "")
            {
                lsnoteto_auditor = "";
            }
            else
            {
                lsnoteto_auditor = values.noteto_auditor.Replace("'", "");
            }
            msSQL = " update atm_mst_tchecklistmasteradd set " +
                 " riskcategory_gid='" + values.riskcategory_gid + "'," +
                  " riskcategory_name='" + values.riskcategory_name.Replace("'", "") + "'," +
                   " positiveconfirmity_gid='" + values.positiveconfirmity_gid + "'," +
                  " positiveconfirmity_name='" + values.positiveconfirmity_name.Replace("'", "") + "'," +
                     " checkpointgroup_gid='" + values.checkpointgroup_gid + "'," +
                  " checkpointgroup_name='" + values.checkpointgroup_name.Replace("'", "") + "'," +
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
                 " where checklistmasteradd_gid='" + values.checklistmasteradd_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ACLL");

                msSQL = " insert into atm_mst_tchecklistmasteraddlog(" +
                     " checklistmasteraddlog_gid," +
                     " checklistmasteradd_gid ," +
                     " riskcategory_gid," +
                     " riskcategory_name ," +
                      " positiveconfirmity_gid," +
                     " positiveconfirmity_name ," +
                       " checkpointgroup_gid," +
                     " checkpointgroup_name ," +
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
                     " updated_by," +
                     " updated_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.checklistmasteradd_gid + "', " +
                     "'" + values.riskcategory_name.Replace("'", "") + "'," +
                       "'" + values.riskcategory_gid + "'," +
                         "'" + values.positiveconfirmity_name.Replace("'", "") + "'," +
                       "'" + values.positiveconfirmity_gid + "'," +
                        "'" + values.checkpointgroup_name.Replace("'", "") + "'," +
                       "'" + values.checkpointgroup_gid + "'," +
                     "'" + values.checkpoint_intent.Replace("'", "") + "'," +
                       "'" + values.checkpoint_description + "', " +
                     " '" + lsnoteto_auditor.Replace("'", "") + "'," +
                       "'" + values.yes_score.Replace("'", "") + "'," +
                     "'" + values.no_score.Replace("'", "") + "'," +
                          "'" + values.partial_score.Replace("'", "") + "'," +
                       "'" + values.na_score.Replace("'", "") + "'," +
                     "'" + values.total_score.Replace("'", "") + "'," +
                       "'" + values.yes_disposition.Replace("'", "") + "'," +
                         "'" + values.no_disposition.Replace("'", "") + "'," +
                           "'" + values.partial_disposition.Replace("'", "") + "'," +
                             "'" + values.na_disposition.Replace("'", "") + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
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
        public void DaDeleteChecklistMaster(string checklistmaster_gid, string employee_gid, MdlAtmMstChecklistMaster values)
        {


            msSQL = " select audit_name from atm_mst_tchecklistmaster where checklistmaster_gid='" + checklistmaster_gid + "'";
            lsauditname_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from atm_mst_tchecklistmaster where checklistmaster_gid='" + checklistmaster_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from atm_mst_tchecklistmasteradd where checklistmaster_gid='" + checklistmaster_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Audit Creation Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("CMDL");
                msSQL = " insert into atm_mst_tchecklistmasterdeletelog(" +
                         "checklistmasterdeletelog_gid, " +
                         "checklistmaster_gid, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + checklistmaster_gid + "', " +
                         "'" + lsauditname_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                {
                    msGetGid = objcmnfunctions.GetMasterGID("CADL");
                    msSQL = " insert into atm_mst_tchecklistmasteradddeletelog(" +
                             "checklistmasteradddeletelog_gid, " +
                             "checklistmaster_gid, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'" + checklistmaster_gid + "', " +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }
        public void DaDeleteChecklistMasterAdd(string checklistmasteradd_gid, string employee_gid, MdlAtmMstChecklistMaster values)
        {
            msSQL = " delete from atm_mst_tchecklistmasteradd where checklistmasteradd_gid='" + checklistmasteradd_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Checkpoint Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("CADL");
                msSQL = " insert into atm_mst_tchecklistmasteradddeletelog(" +
                         "checklistmasteradddeletelog_gid, " +
                         "checklistmasteradd_gid, " +
                          "checklistmaster_gid, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + checklistmasteradd_gid + "', " +
                          "'" + values.checklistmasteradd_gid + "', " +
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

        public void DaGetChecklistMasterIntent(string checklistmasteradd_gid, MdlAtmMstChecklistMaster values)
        {
            msSQL = " select checkpoint_intent , checkpoint_description  from atm_mst_tchecklistmasteradd " +
                  " where checklistmasteradd_gid='" + checklistmasteradd_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checkpoint_intent = objODBCDatareader["checkpoint_intent"].ToString();
                values.checkpoint_description = objODBCDatareader["checkpoint_description"].ToString();

            }
            objODBCDatareader.Close();

        }
        public void DaGetChecklistMasterDescription(string checklistmasteradd_gid, MdlAtmMstChecklistMaster values)
        {
            msSQL = " select checkpoint_description  from atm_mst_tchecklistmasteradd " +
                  " where checklistmasteradd_gid='" + checklistmasteradd_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checkpoint_description = objODBCDatareader["checkpoint_description"].ToString();
            }
            objODBCDatareader.Close();

        }
        public void DaGetChecklistMasterAuditor(string checklistmasteradd_gid, MdlAtmMstChecklistMaster values)
        {
            msSQL = " select noteto_auditor  from atm_mst_tchecklistmasteradd " +
                  " where checklistmasteradd_gid='" + checklistmasteradd_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.noteto_auditor = objODBCDatareader["noteto_auditor"].ToString();
            }
            objODBCDatareader.Close();

        }
        public void DaGetChecklistMasterAuditorName(string checklistmaster_gid, MdlAtmMstChecklistMaster values)
        {
            msSQL = " select audit_description  from atm_mst_tchecklistmaster " +
                  " where checklistmaster_gid='" + checklistmaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.audit_description = objdbconn.GetExecuteScalar(msSQL);
            }
            objODBCDatareader.Close();

        }


        public void DaImportExcelChecklist(HttpRequest httpRequest, string employee_gid, result objResult, MdlAtmMstChecklistMaster values)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath, lschecklistaudit_gid;
                string lsaudit_gid = httpRequest.Form["checklistmaster_gid"];

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



                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                try
                {
                    using (ExcelPackage xlPackage = new ExcelPackage(ms))
                    {
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                        rowCount = worksheet.Dimension.End.Row;
                        columnCount = worksheet.Dimension.End.Column;
                        endRange = worksheet.Dimension.End.Address;
                    }
                    file.Close();
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/ImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid, ms);

                    ms.Close();

                    objcmnfunctions.uploadFile(lspath, lsfile_gid);

                    //Excel To DataTable

                    lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                    excelRange = "A1:" + endRange + rowCount.ToString();

                    dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                }
                catch(Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.ToString();
                    return;
                }
                foreach (DataRow row in dt.Rows)
                {
                    lschecklistaudit_gid = row["* Audit Unique Id"].ToString();

                    if (lschecklistaudit_gid != lsaudit_gid)
                    {
                        if (lschecklistaudit_gid != "")
                        {
                            lserrflag = "Yes";
                        }
                    }
                }

                if (lserrflag == "No")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        checklistexcelimportlog_message = "";

                        //lsentity_name = row["* Entity Name"].ToString();
                        //lsauditdepartment_name = row["* Audit Department"].ToString();
                        //lscheckpointgroup_name = row["* Checkpointgroup Name"].ToString();
                        //lsaudittype_name = row["* Audittype Name"].ToString();

                        lschecklistaudit_gid = row["* Audit Unique Id"].ToString();


                        //msSQL = "select checklistmaster_gid from atm_mst_tchecklistmaster where checklistmaster_gid='" + lschecklistaudit_gid + "'";

                        //riskcategory_name, positiveconfirmity_name, checkpoint_intent, checkpoint_description, noteto_auditor, yes_disposition, no_disposition, partial_disposition, na_disposition, yes_score, no_score, partial_score, na_score, total_score

                        //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDatareader.HasRows == true) 
                        if (lschecklistaudit_gid == lsaudit_gid)
                        {
                            //foreach (DataRow dr_datarow in dt_datatable.Rows)
                            //{



                            lsriskcategory_name = row["* Risk Category"].ToString();
                            msSQL = "select riskcategory_gid from atm_mst_triskcategory where riskcategory_name='" + lsriskcategory_name + "'";
                            lsriskcategory_gid = objdbconn.GetExecuteScalar(msSQL);

                            lspositiveconfirmity_name = row["* Positive Conformity"].ToString();
                            msSQL = "select positiveconfirmity_gid from atm_mst_tpositiveconfirmity where positiveconfirmity_name='" + lspositiveconfirmity_name + "'";
                            lspositiveconfirmity_gid = objdbconn.GetExecuteScalar(msSQL);
                            lscheckpoint_group = row["* Checkpoint Group"].ToString();
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
                            lstotal_score = row["* lstotal_score"].ToString();


                        }


                        else
                        {
                            checklistexcelimportlog_message = "Audit ID is Mismatched.....";
                        }

                        if (checklistexcelimportlog_message == "")
                        {
                            if ((lsriskcategory_name == "") || (lspositiveconfirmity_name == "") || (lscheckpoint_intent == "") || (lscheckpoint_description == "") || (lsnoteto_auditor == "")
                            || (lsyes_disposition == "") || (lsno_disposition == "") || (lspartial_disposition == "") || (lsna_disposition == "")
                            || (lsyes_score == "") || (lsno_score == "") || (lspartial_score == "") || (lsna_score == "") || (lstotal_score == ""))
                            {
                                checklistexcelimportlog_message = "Mandatory fields are empty";
                            }
                            else
                            {

                                msGetGid = objcmnfunctions.GetMasterGID("CLAD");

                                msSQL = " insert into atm_mst_tchecklistmasteradd(" +
                                " checklistmasteradd_gid," +
                                " checklistmaster_gid," +
                                " riskcategory_gid," +
                                " riskcategory_name," +
                                " positiveconfirmity_gid," +
                                " positiveconfirmity_name," +
                                " checkpointgroup_name," +
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
                                "'" + lschecklistaudit_gid + "'," +
                                "'" + lsriskcategory_gid + "'," +
                                "'" + lsriskcategory_name + "'," +
                                "'" + lspositiveconfirmity_gid + "'," +
                                "'" + lspositiveconfirmity_name + "'," +
                                "'" + lscheckpoint_group.Replace("'", "") + "'," +
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
                                "'" + lstotal_score + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msGetGid = objcmnfunctions.GetMasterGID("IECL");

                                msSQL = " insert into atm_mst_timportexcelchecklistlog(" +
                                " importexcelchecklistlog_gid," +
                                " checklistmasteradd_gid," +
                                " checklistmaster_gid," +
                                " riskcategory_name," +
                                " positiveconfirmity_name," +
                                " checkpointgroup_name," +
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
                                "'" + values.checklistmasteradd_gid + "', " +
                                "'" + lschecklistaudit_gid + "'," +
                                "'" + lsriskcategory_name + "'," +
                                "'" + lspositiveconfirmity_name + "'," +
                                "'" + lscheckpoint_group.Replace("'", "") + "'," +
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
                                "'" + lstotal_score + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                insertCount++;

                            }
                        }
                    }
                }
                if (mnResult != 0)
                {
                    objResult.status = true;
                    objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details (Audit ID may mismatched)....";
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }

        }
        public void DaEditChecklistMasterAudit(string checklistmaster_gid, MdlAtmMstChecklistMaster values)
        {
            msSQL = " select checklistmaster_gid,audit_name,audit_description,entity_gid,entity_name,audittype_gid,audittype_name, " +
                    " checkpointgroup_gid,checkpointgroup_name,auditdepartment_gid,auditdepartment_name from atm_mst_tchecklistmaster " +
                    " where checklistmaster_gid='" + checklistmaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checklistmaster_gid = objODBCDatareader["checklistmaster_gid"].ToString();
                values.audit_name = objODBCDatareader["audit_name"].ToString();
                values.audit_description = objODBCDatareader["audit_description"].ToString();
                values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                values.entity_name = objODBCDatareader["entity_name"].ToString();
                values.audittype_gid = objODBCDatareader["audittype_gid"].ToString();
                values.audittype_name = objODBCDatareader["audittype_name"].ToString();
                values.auditdepartment_gid = objODBCDatareader["auditdepartment_gid"].ToString();
                values.auditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                //values.checkpointgroup_gid = objODBCDatareader["checkpointgroup_gid"].ToString();

                String[] checkpointgid_list = objODBCDatareader["checkpointgroup_gid"].ToString().Split(',');
                String[] checkpointname_list = objODBCDatareader["checkpointgroup_name"].ToString().Split(',');
                objODBCDatareader.Close();
                var getcheckpoint_list = new List<checkpoint_list>();
                for (var i = 0; i < checkpointgid_list.Length; i++)
                {
                    getcheckpoint_list.Add(new checkpoint_list
                    {
                        checkpointgroup_gid = checkpointgid_list[i],
                        checkpointgroup_name = checkpointname_list[i]
                    });

                }
                values.checkpoint_list = getcheckpoint_list;

                //if (values.checkpointgroupadd_list != null)
                //{
                //    for (var i = 0; i < values.checkpointgroupadd_list.Count; i++)
                //    {
                //        lscheckpointgroup_gid += values.checkpointgroupadd_list[i].checkpointgroup_gid + ",";
                //        lscheckpointgroup_name += values.checkpointgroupadd_list[i].checkpointgroup_name + ",";
                //    }
                //    lscheckpointgroup_gid = lscheckpointgroup_gid.TrimEnd(',');
                //    lscheckpointgroup_name = lscheckpointgroup_name.TrimEnd(',');
                //}
                msSQL = " select checkpointgroup_gid,checkpointgroup_name from atm_mst_tcheckpointgroup " +
                " where checkpointgroup_status ='UnAssign'";
				dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcheckpointaddgroup_list = new List<checkpointaddgroup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcheckpointaddgroup_list.Add(new checkpointaddgroup_list
                        {
                            checkpointgroup_gid = dt["checkpointgroup_gid"].ToString(),
                            checkpointgroup_name = dt["checkpointgroup_name"].ToString(),
                        });
                        values.checkpointaddgroup_list = getcheckpointaddgroup_list;
                    }
                }
                dt_datatable.Dispose();
            }

            values.status = true;
        }
        public void DaUpdateChecklistMasterAudit(string employee_gid, MdlAtmMstChecklistMaster values)
        {
            if (values.checkpoint_list != null)
            {
                for (var i = 0; i < values.checkpoint_list.Count; i++)
                {
                    lscheckpointgroup_gid += values.checkpoint_list[i].checkpointgroup_gid + ",";
                    lscheckpointgroup_name += values.checkpoint_list[i].checkpointgroup_name + ",";
                }
                lscheckpointgroup_gid = lscheckpointgroup_gid.TrimEnd(',');
                lscheckpointgroup_name = lscheckpointgroup_name.TrimEnd(',');
            }
            string[] checkpointgroupgid_array = lscheckpointgroup_gid.Split(',');

            msSQL = " update atm_mst_tchecklistmaster set " +
                    " checklistmaster_gid='" + values.checklistmaster_gid + "'," +
                    " audit_name='" + values.audit_name.Replace("'", "") + "'," +
                    " audit_description='" + values.audit_description.Replace("'", "") + "'," +
                    " audittype_gid='" + values.audittype_gid + "'," +
                    " audittype_name='" + values.audittype_name.Replace("'", "") + "'," +
                    " entity_gid='" + values.entity_gid + "'," +
                    " entity_name='" + values.entity_name.Replace("'", "") + "'," +
                    " auditdepartment_gid='" + values.auditdepartment_gid + "'," +
                    " auditdepartment_name='" + values.auditdepartment_name + "'," +
                    " checkpointgroup_gid='" + lscheckpointgroup_gid + "'," +
                    " checkpointgroup_name='" + lscheckpointgroup_name.Replace("'", "") + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where checklistmaster_gid='" + values.checklistmaster_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                int array_length = checkpointgroupgid_array.Length;
                string sample_querystring = "";
                for (int i = 0; i < checkpointgroupgid_array.Length; i++)
                {
                    if (i == checkpointgroupgid_array.Length - 1)
                    {
                        sample_querystring = sample_querystring + "'" + checkpointgroupgid_array[i] + "'";
                    }
                    else
                    {
                        sample_querystring = sample_querystring + "'" + checkpointgroupgid_array[i] + "',";
                    }
                }
                
                msSQL = " SELECT distinct a.checkpointgroup_gid,a.riskcategory_gid,a.checkpointgroupadd_gid,a.riskcategory_name,d.checkpointgroup_name, " +
                        " a.positiveconfirmity_gid,a.positiveconfirmity_name,a.checkpoint_intent,a.checkpoint_description,a.noteto_auditor, " +
                        " a.yes_score,a.no_score,a.partial_score,a.na_score,a.total_score,a.yes_disposition,a.no_disposition,a.partial_disposition, " +
                        " a.na_disposition,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                        " FROM atm_mst_tcheckpointadd a" +
                        " left join atm_mst_tcheckpointgroup d on d.checkpointgroup_gid = a.checkpointgroup_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.checkpointgroup_gid in (" + sample_querystring + ") ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                List<checkpointgroupadd_list> getcheckpointaddgroup_list = new List<checkpointgroupadd_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcheckpointaddgroup_list.Add(new checkpointgroupadd_list
                        {
                            checkpointgroup_gid = dt["checkpointgroup_gid"].ToString(),
                            checkpointgroup_name = dt["checkpointgroup_name"].ToString(),
                            checkpointgroupadd_gid = dt["checkpointgroupadd_gid"].ToString(),
                            riskcategory_gid = dt["riskcategory_gid"].ToString(),
                            riskcategory_name = dt["riskcategory_name"].ToString(),
                            positiveconfirmity_gid = dt["positiveconfirmity_gid"].ToString(),
                            positiveconfirmity_name = dt["positiveconfirmity_name"].ToString(),
                            checkpoint_intent = dt["checkpoint_intent"].ToString(),
                            checkpoint_description = dt["checkpoint_description"].ToString(),
                            noteto_auditor = dt["noteto_auditor"].ToString(),
                            yes_score = dt["yes_score"].ToString(),
                            no_score = dt["no_score"].ToString(),
                            partial_score = dt["partial_score"].ToString(),
                            na_score = dt["na_score"].ToString(),
                            total_score = dt["total_score"].ToString(),
                            yes_disposition = dt["yes_disposition"].ToString(),
                            no_disposition = dt["no_disposition"].ToString(),
                            partial_disposition = dt["partial_disposition"].ToString(),
                            na_disposition = dt["na_disposition"].ToString()
                        });
                    }
                    dt_datatable.Dispose();

                    foreach (var i in checkpointgroupgid_array)
                    {
                        msSQL = " select checkpointgroup_gid from atm_mst_tchecklistmasteradd  " +
                                " where checklistmaster_gid = '" + values.checklistmaster_gid + "' and checkpointgroup_gid='" + i + "'";
                        string lsexistingcheckpointgroup = objdbconn.GetExecuteScalar(msSQL); 
                        if (lsexistingcheckpointgroup == "")
                        {
                            var lscheckpoint = getcheckpointaddgroup_list.Where(a => a.checkpointgroup_gid == i).ToList();
                            foreach(var j in lscheckpoint)
                            {
                                msGetGid = objcmnfunctions.GetMasterGID("CLAD");
                                msSQL = " insert into atm_mst_tchecklistmasteradd(" +
                                        " checklistmasteradd_gid," +
                                        " checklistmaster_gid ," +
                                        " checkpointgroup_gid," +
                                        " checkpointgroupadd_gid," +
                                        " riskcategory_gid," +
                                        " riskcategory_name ," +
                                        " positiveconfirmity_gid," +
                                        " positiveconfirmity_name ," +
                                        " checkpoint_intent," +
                                        " checkpoint_description ," +
                                        " checkpointgroup_name ," +
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
                                        "'" + values.checklistmaster_gid + "', " +
                                        "'" + j.checkpointgroup_gid + "'," +
                                        "'" + j.checkpointgroupadd_gid + "'," +
                                        "'" + j.riskcategory_gid + "'," +
                                        "'" + j.riskcategory_name + "'," +
                                        "'" + j.positiveconfirmity_gid + "'," +
                                        "'" + j.positiveconfirmity_name + "'," +
                                        "'" + j.checkpoint_intent + "'," +
                                        "'" + j.checkpoint_description + "'," +
                                        "'" + j.checkpointgroup_name + "', " +
                                        "'" + j.noteto_auditor + "'," +
                                        "'" + j.yes_score + "'," +
                                        "'" + j.no_score + "'," +
                                        "'" + j.partial_score + "'," +
                                        "'" + j.na_score + "'," +
                                        "'" + j.total_score + "'," +
                                        "'" + j.yes_disposition + "'," +
                                        "'" + j.no_disposition + "'," +
                                        "'" + j.partial_disposition + "'," +
                                        "'" + j.na_disposition+ "'," +
                                        "'" + employee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
                            }
                           
                            values.status = true;
                            values.message = "Audit Updated Successfully";
                        }
                    }
                     
                    msSQL = " select checkpointgroup_gid from atm_mst_tchecklistmasteradd  where checklistmaster_gid = '" + values.checklistmaster_gid + "' " +
                            " and checkpointgroup_gid not in (" + sample_querystring + ") group by checkpointgroup_gid ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = "delete from atm_mst_tchecklistmasteradd where checklistmaster_gid = '" + values.checklistmaster_gid + "' and checkpointgroup_gid='" + dt["checkpointgroup_gid"].ToString() + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    dt_datatable.Dispose();

                    if (mnResult != 0)
                    { 
                        values.status = true;
                        values.message = "Audit Updated Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occurred While Updating";
                    }
                }
            }
        }
        public void DaGetCheckpointStatus(MdlAtmMstChecklistMaster values)
        {
            try
            {
                msSQL = "select checkpointgroup_name,status,checkpointgroup_gid from atm_mst_tcheckpointgroup" +
                                 " where checkpointgroup_status ='UnAssign' and status='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getchecklistmaster_list = new List<checklistmaster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getchecklistmaster_list.Add(new checklistmaster_list
                        {
                            checkpointgroup_gid = (dr_datarow["checkpointgroup_gid"].ToString()),
                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                        });
                    }
                    values.checklistmaster_list = getchecklistmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetMultipleCheckpointgroup(MdlAtmMstChecklistMaster values,string employee_gid)
        {
            try
            {
                string checkpointgroup_gid = string.Empty;
                string checkpointgroup_name = string.Empty;

               
                if (values.checkpointgroup_list != null)
                {
                    for (var i = 0; i < values.checkpointgroup_list.Count; i++)
                    {
                        checkpointgroup_gid += "'" + values.checkpointgroup_list[i].checkpointgroup_gid + "'" + ",";

                    }

                    checkpointgroup_gid = checkpointgroup_gid.TrimEnd(',');
                } 
             
                msSQL = " SELECT distinct a.checkpointgroup_gid,a.checkpointgroupadd_gid,d.checkpointgroup_name,a.riskcategory_name, " +
                        " a.positiveconfirmity_name,a.checkpoint_intent,a.checkpoint_description,a.noteto_auditor,a.yes_score,a.no_score, " +
                        " a.partial_score,a.na_score,a.total_score,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_mst_tcheckpointadd a" +
                        " left join atm_mst_tcheckpointgroup d on d.checkpointgroup_gid = a.checkpointgroup_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         "  where d.checkpointgroup_gid in (" + checkpointgroup_gid + ")";

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

        public void DaGetCheckpointgroupMultiple(MdlAtmMstChecklistMaster values, string checkpointgroup_gid)
        {
            try
            {
                string checkpointgroup_name = string.Empty;

                if (values.checkpointgroup_list != null)
                {
                    for (var i = 0; i < values.checkpointgroup_list.Count; i++)
                    {
                        checkpointgroup_gid += "'" + values.checkpointgroup_list[i].checkpointgroup_gid + "'" + ",";

                    }

                    checkpointgroup_gid = checkpointgroup_gid.TrimEnd(',');
                }
                msSQL = " SELECT distinct a.checkpointgroup_gid,a.checkpointgroupadd_gid,d.checkpointgroup_name,a.riskcategory_name,a.positiveconfirmity_name,a.checkpoint_intent,a.checkpoint_description,a.noteto_auditor,a.yes_score,a.no_score,a.partial_score,a.na_score,a.total_score,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_mst_tcheckpointadd a" +
                        " left join atm_mst_tcheckpointgroup d on d.checkpointgroup_gid = a.checkpointgroup_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         "  where d.checkpointgroup_gid in (" + checkpointgroup_gid + ")";

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

        public void DaGetEditMultipleCheckpointgroup(MdlAtmMstChecklistMaster values, string employee_gid)
        {
            try
            {
                string checkpointgroup_gid = string.Empty;
                string checkpointgroup_name = string.Empty;


                if (values.checkpointgroup_list != null)
                {
                    for (var i = 0; i < values.checkpointgroup_list.Count; i++)
                    {
                        checkpointgroup_gid += "'" + values.checkpointgroup_list[i].checkpointgroup_gid + "'" + ",";

                    }
                    checkpointgroup_gid = checkpointgroup_gid.TrimEnd(',');
                }




                msSQL = " SELECT distinct a.checkpointgroup_gid,a.checkpointgroupadd_gid,d.checkpointgroup_name,a.riskcategory_name, " +
                        " a.positiveconfirmity_name,a.checkpoint_intent,a.checkpoint_description,a.noteto_auditor,a.yes_score,a.no_score, " +
                        " a.partial_score,a.na_score,a.total_score,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_mst_tcheckpointadd a" +
                        " left join atm_mst_tcheckpointgroup d on d.checkpointgroup_gid = a.checkpointgroup_gid" + 
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where d.checkpointgroup_gid in (" + checkpointgroup_gid + ") and d.checkpointgroup_gid not in " +
                        " (select checkpointgroup_gid from atm_mst_tchecklistmasteradd where " +
                        " checklistmaster_gid = '" + values.checklistmaster_gid + "' group by checkpointgroup_gid)";

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

                        });
                    }
                    values.checkpointgroupadd_list = getcheckpointgroupadd_list;
                }
                dt_datatable.Dispose();

                msSQL = " SELECT distinct a.checkpointgroup_gid,a.checkpointgroupadd_gid,a.checkpointgroup_name,a.riskcategory_name, " +
                        " a.positiveconfirmity_name,a.checkpoint_intent,a.checkpoint_description,a.noteto_auditor,a.yes_score,a.no_score, " +
                        " a.partial_score,a.na_score,a.total_score,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_mst_tchecklistmasteradd a" +
                        " left join atm_mst_tchecklistmaster d on d.checklistmaster_gid = a.checklistmaster_gid" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.checkpointgroup_gid in (" + checkpointgroup_gid + ") and a.checklistmaster_gid ='" + values.checklistmaster_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
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


    }
}