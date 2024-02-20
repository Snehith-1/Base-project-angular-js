using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.foundation.Models;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Web;
using System.IO;
using OfficeOpenXml;
using ems.storage.Functions;



namespace ems.foundation.DataAccess
{    
    public class DaFndMstQuestionnarie
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, msGetGid1, lscampaigntype_value, lslms_code, lsbureau_code, lsremarks, lscampaigntype_code, msGetGidQA;
        string lscompany_code, excelRange, endRange, lssample_name, questionnarie_gid;
        int rowCount, columnCount;
        int mnResult;
        HttpPostedFile httpPostedFile;
        public void DaGetCampaignType(Mdlcampaigntype objcampaigntype ,string employee_gid)
        {
            try
            {
                
                msSQL = "delete from fnd_mst_tquestionnarieanswer where status = 'Y' and created_by = '" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                

                msSQL = " SELECT campaigntype_gid,campaigntype_name FROM fnd_mst_tcampaigntype ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcampaigntype_list = new List<campaigntype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcampaigntype_list.Add(new campaigntype_list
                        {
                            campaigntype_gid = (dr_datarow["campaigntype_gid"].ToString()),
                            campaigntype_name = (dr_datarow["campaigntype_name"].ToString()),
                        });
                    }
                    objcampaigntype.campaigntype_list = getcampaigntype_list;
                }
                dt_datatable.Dispose();

                objcampaigntype.status = true;
            }
            catch
            {
                objcampaigntype.status = false;
            }

        }
        public void DaQuestionnarieEdit(string Questionnarie_gid, Questionnarie_list values)
        {
            try
            {
                msSQL = " select a.Questionnarie_gid, a.questionnarieanswer_gid,a.status as Status, a.questionnarie_name,a.campaigntype_gid,e.categorytype_name, " +
                    " a.questionnarie_type, a.questionnarie_answer, a.importance,a.status,a.remarks,d.campaigntype_name, a.categorytype_gid " +
                    "  from fnd_mst_tquestionnarie a" +
                     " left join fnd_mst_tcampaigntype d on d.campaigntype_gid = a.campaigntype_gid "  +
                     " left join fnd_mst_tcategorytype e on e.categorytype_gid = a.categorytype_gid " +
                     " where a.Questionnarie_gid='" + Questionnarie_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.Questionnarie_gid = objODBCDatareader["Questionnarie_gid"].ToString();
                    values.categorytype_gid = objODBCDatareader["categorytype_gid"].ToString();
                    values.categorytype_name = objODBCDatareader["categorytype_name"].ToString();
                    values.Questionnarie_name = objODBCDatareader["questionnarie_name"].ToString();
                    values.Questionnarie_type = objODBCDatareader["questionnarie_type"].ToString();
                    values.campaigntype_gid = objODBCDatareader["campaigntype_gid"].ToString();                    
                    values.Campaigntype_name = objODBCDatareader["campaigntype_name"].ToString();
                    values.mandatory = objODBCDatareader["importance"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.Questionnarie_answer = objODBCDatareader["questionnarie_answer"].ToString();
                    values.Status = objODBCDatareader["Status"].ToString();


                }

                values.status = "true";
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = "false";
                values.message = "failure";
            }
        }
        
        public void DaGetQuestionnarie(MdlMstQuestionnarie values)
        {
            try
            {
                msSQL = " SELECT a.questionnarie_gid, a.campaigntype_gid, " +
                    "a.questionnarie_code, a.questionnarie_name, a.questionnarie_type, a.questionnarie_answer, a.questionnarieanswer_gid, a.importance," +
                    " a.lms_code, a.bureau_code,a.remarks, d.campaigntype_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM fnd_mst_tquestionnarie a" +
                        " left join fnd_mst_tcampaigntype d on d.campaigntype_gid = a.campaigntype_gid " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getQuestionnarie_list = new List<Questionnarie_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getQuestionnarie_list.Add(new Questionnarie_list
                        {
                            Questionnarie_gid  = (dr_datarow["questionnarie_gid"].ToString()),
                            Campaigntype_name = (dr_datarow["campaigntype_name"].ToString()),
                            Questionnarie_type = (dr_datarow["questionnarie_type"].ToString()),
                            Questionnarie_name  = (dr_datarow["questionnarie_name"].ToString()),
                            mandatory = (dr_datarow["importance"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.Questionnarie_list  = getQuestionnarie_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public bool DaEditPostAnswerDesc(MdlMstQuestionnarie values, string employee_gid)
        {
          
            msGetGid = objcmnfunctions.GetMasterGID("FCQA");
            msSQL = " insert into fnd_mst_tquestionnarieanswer(" +

                    " questionnarieanswer_gid," +
                    " campaigntype_gid," +
                    " questionnarie_gid," +
                    " questionnarie_type," +
                    " questionnarie_answer," +
                    " status, " +
                    " created_by " +
                    " )" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.campaigntype_gid + "'," +
                    "'" + values.Questionnarie_gid + "'," +
                    "'" + values.Questionnarie_type + "'," +
                    "'" + values.answer_desc + "'," +
                    "'Y', " +
                    "'" + employee_gid + "'" +
                    " )";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Answer description Added Successfully";
                values.Questionnarie_gid = values.Questionnarie_gid;
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Answer description";
                objdbconn.CloseConn();
                return false;
            }
        }
        public bool DaPostAnswerDesc(MdlMstQuestionnarie values, string employee_gid)
        {
           
                msSQL = "select questionnarie_gid from fnd_mst_tquestionnarieanswer where status = 'Y' and created_by = '" + employee_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.Questionnarie_gid = objODBCDatareader["questionnarie_gid"].ToString();
               
            }
            else
            {
                values.Questionnarie_gid = objcmnfunctions.GetMasterGID("FCQM");
            }
           
            msGetGid = objcmnfunctions.GetMasterGID("FCQA");
            msSQL = " insert into fnd_mst_tquestionnarieanswer(" +

                    " questionnarieanswer_gid," +
                    " campaigntype_gid," +
                    " questionnarie_gid," +
                    " questionnarie_type," +
                    " questionnarie_answer," +
                    " status, " +
                    " created_by " +
                    " )" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.campaigntype_gid + "'," +
                    "'" + values.Questionnarie_gid + "'," +
                    "'" + values.Questionnarie_type + "'," +
                    "'" + values.answer_desc + "'," +
                    "'Y', " +
                    "'" + employee_gid + "'" +
                    " )";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Answer description Added Successfully";
                values.Questionnarie_gid = values.Questionnarie_gid;
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Answer description";
                objdbconn.CloseConn();
                return false;
            }
        }

        public void DaGetAnswerDesc(questionnarieanswer_list values,string employee_gid,string Questionnarie_gid)
        {
            msSQL = "select questionnarieanswer_gid, campaigntype_gid, questionnarie_gid, questionnarie_type, questionnarie_answer " +
                    "from fnd_mst_tquestionnarieanswer where created_by = '" + employee_gid + "' and status = 'Y' " +
                    " and questionnarie_gid = '"+ Questionnarie_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var questionnarieanswer_list = new List<questionnarieanswer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    questionnarieanswer_list.Add(new questionnarieanswer_list
                    {
                        questionnarieanswer_gid = (dr_datarow["questionnarieanswer_gid"].ToString()),
                        questionnarie_type = (dr_datarow["questionnarie_type"].ToString()),
                        questionnarie_answer = (dr_datarow["questionnarie_answer"].ToString()),
                        
                    });
                }
                values.questionnarieanswerlist = questionnarieanswer_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGeteditAnswerDesc(questionnarieanswer_list values, string employee_gid, string Questionnarie_gid)
        {
            msSQL = "select questionnarieanswer_gid, campaigntype_gid, questionnarie_gid, questionnarie_type, questionnarie_answer " +
                    "from fnd_mst_tquestionnarieanswer where created_by = '" + employee_gid + "'  " +
                    " and questionnarie_gid = '" + Questionnarie_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var questionnarieanswer_list = new List<questionnarieanswer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    questionnarieanswer_list.Add(new questionnarieanswer_list
                    {
                        questionnarieanswer_gid = (dr_datarow["questionnarieanswer_gid"].ToString()),
                        questionnarie_type = (dr_datarow["questionnarie_type"].ToString()),
                        questionnarie_answer = (dr_datarow["questionnarie_answer"].ToString()),

                    });
                }
                values.questionnarieanswerlist = questionnarieanswer_list;
            }
            dt_datatable.Dispose();
        }
        public void DaDeleteAnswerDesc(string questionnarieanswer_gid, MdlMstQuestionnarie values)
        {
            
                msSQL = "delete from fnd_mst_tquestionnarieanswer where questionnarieanswer_gid='" + questionnarieanswer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Answer description deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting the Answer description";
                values.status = false;

            }
        }
        public void DaCreateCampaignType(MdlMstQuestionnarie  values, string employee_gid)
        {

            msSQL = "select campaigntype_gid,questionnarie_name,categorytype_gid from fnd_mst_tquestionnarie where campaigntype_gid = '" + values.campaigntype_gid + "' and questionnarie_name= '" + values.Questionnarie_name.Replace("'", "\\'") + "' and categorytype_gid= '" + values.category_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = " Questionnarie group  Already Exist";
               
            }

            else
            {
                string lsQuestionnarie_answer = string.Empty;

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
                if (values.remarks == null || values.remarks == "")
                {
                    lsremarks = "";
                }
                else
                {
                    lsremarks = values.remarks.Replace("'", "");
                }

                if (values.Questionnarie_gid != null)
                {
                    msGetGid = values.Questionnarie_gid;

                    msSQL = " SELECT GROUP_CONCAT(questionnarie_answer SEPARATOR ', ') questionnarie_answer " +
                             " FROM fnd_mst_tquestionnarieanswer where questionnarie_gid =  '" + values.Questionnarie_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsQuestionnarie_answer = objODBCDatareader["questionnarie_answer"].ToString();

                    }


                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FCQM");
                }


                msSQL = " insert into fnd_mst_tquestionnarie(" +
                            " questionnarie_gid," +
                            " campaigntype_gid," +
                            " questionnarie_name," +
                            " questionnarie_type," +
                            " questionnarie_answer," +
                            " importance," +
                            " lms_code," +
                            " bureau_code," +
                            " remarks," +
                            " created_by," +
                            " created_date,categorytype_gid)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.campaigntype_gid + "'," +
                            "'" + values.Questionnarie_name.Replace("'", "\\'") + "'," +
                           // "'" + values.Questionnarie_name + "'," +
                            "'" + values.Questionnarie_type + "'," +
                            "'" + lsQuestionnarie_answer + "'," +
                            "'" + values.rbo_mandatory + "'," +
                            "'" + lslms_code + "'," +
                            "'" + lsbureau_code + "'," +
                            "'" + lsremarks + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                             "'" + values.category_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Questionnarie Added Successfully";

                    msSQL = "update fnd_mst_tquestionnarieanswer set status = 'N' where created_by='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                   
                }
            }
            objODBCDatareader.Close();
        }
        public void DaEditQuestionnarieSubmit(MdlMstQuestionnarie values, string employee_gid)
        {

            msSQL = "select campaigntype_gid,questionnarie_name,categorytype_gid from fnd_mst_tquestionnarie where campaigntype_gid = '" + values.campaigntype_gid + "' and questionnarie_name= '" + values.Questionnarie_name.Replace("'", "\\'") + "' and categorytype_gid= '" + values.category_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = " Questionnarie group  Already Exist";

            }

            else
            {
                string lsQuestionnarie_answer = string.Empty;

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
                if (values.remarks == null || values.remarks == "")
                {
                    lsremarks = "";
                }
                else
                {
                    lsremarks = values.remarks.Replace("'", "");
                }

                if (values.Questionnarie_gid != null)
                {
                    msGetGid = values.Questionnarie_gid;

                    msSQL = " SELECT GROUP_CONCAT(questionnarie_answer SEPARATOR ', ') questionnarie_answer " +
                             " FROM fnd_mst_tquestionnarieanswer where questionnarie_gid =  '" + values.Questionnarie_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsQuestionnarie_answer = objODBCDatareader["questionnarie_answer"].ToString();

                    }


                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FCQM");
                }

                msSQL = " update fnd_mst_tquestionnarie set " +
                     " campaigntype_gid=  '" + values.campaigntype_gid + "'," +
                      " categorytype_gid=  '" + values.categorytype_gid + "'," +
                            " questionnarie_name = '" + values.Questionnarie_name + "'," +
                            " questionnarie_type='" + values.Questionnarie_type + "'," +
                            " questionnarie_answer='" + lsQuestionnarie_answer + "'," +
                            " importance='" + values.rbo_mandatory + "'," +
                            " remarks='" + lsremarks + "' " +
                            " where questionnarie_gid = '" + values.Questionnarie_gid + "'";


                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Questionnarie Updated Successfully";

                    msSQL = "update fnd_mst_tquestionnarieanswer set status = 'N' where questionnarie_gid = '" + values.Questionnarie_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
            objODBCDatareader.Close();
        }
        public void DaDeleteQuestionnarie(MdlMstQuestionnarie values, string Questionnarie_gid, string employee_gid)
        {

            msSQL = "select Questionnarie_gid from fnd_trn_tcampaigndtl where Questionnarie_gid='" + Questionnarie_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = " Can't be deleted, this Questionnarie is already assigned";
            }
            else
            {

                msSQL = " select Questionnarie_name from fnd_mst_tquestionnarie where Questionnarie_gid='" + Questionnarie_gid + "'";
                lscampaigntype_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from fnd_mst_tquestionnarie where Questionnarie_gid='" + Questionnarie_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Questionnarie Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("FCQD");
                    msSQL = " insert into fnd_mst_tquestionnariedeletelog(" +
                             "questionnariedeletelog_gid, " +
                             "questionnarie_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'" + Questionnarie_gid + "', " +
                             "'Questionnarie'," +
                             "'" + lscampaigntype_value + "'," +
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
            objODBCDatareader.Close();
        }
   

        public void DaInactiveQuestionnarie(MdlMstQuestionnarie values, string employee_gid)
        {
            msSQL = " update fnd_mst_tquestionnarie set status='" + values.rbo_status + "'" +
                    " where Questionnarie_gid='" + values.Questionnarie_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("FDIL");

                msSQL = " insert into fnd_mst_tquestionnarieinactivelog (" +
                      " questionnarieinactivelog_gid, " +
                      " Questionnarie_gid," +
                      " Questionnarie_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.Questionnarie_gid + "'," +
                      " '" + values.Questionnarie_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Questionnarie Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Questionnarie Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }
        
        public void DaQuestionnarieInactiveLogview(string Questionnarie_gid, MdlMstQuestionnarie values)
        {
            try
            {
                msSQL = " SELECT a.questionnarie_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM fnd_mst_tquestionnarieinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.questionnarie_gid ='" + Questionnarie_gid + "' order by a.questionnarieinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getQuestionnarie_list = new List<Questionnarie_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getQuestionnarie_list.Add(new Questionnarie_list
                        {
                            Questionnarie_gid = (dr_datarow["questionnarie_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.Questionnarie_list = getQuestionnarie_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaQuestionnarieimportexcel(HttpRequest httpRequest, string employee_gid, result objResult, MdlMstQuestionnarie values)
        {
            string campaigntype_gid, questionnarie_name,categorytype_gid, questionnarie_type,importance, lsQuestionnarie_answer;          
            string categoryName = string.Empty;
            string campaignName = string.Empty;
            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                //  string lsaudit_gid = httpRequest.Form["auditcreation_gid"];
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/Foundation/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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
                if (dt.Rows.Count == 0)
                {
                    objResult.message = "No excel data to import ";
                    objResult.status = true;
                }
                foreach (DataRow row in dt.Rows)
                {
                    

                    foreach (var i in columnNames)
                    {
                        Header_name = i.Trim();
                        Header_name = Header_name.Replace("*", "");
                        Header_name = Header_name.Replace(" ", "_");

                        Header_value = row[i].ToString();                        


                    }
                   
                }
              
                for (int r = 0; r <= dt.Rows.Count - 1; r++)
                {

                    //categoryName = String.Concat(Convert.ToString(dt.Rows[r][2]).Where(c => !Char.IsWhiteSpace(c)));
                    //campaignName = String.Concat(Convert.ToString(dt.Rows[r][1]).Where(c => !Char.IsWhiteSpace(c)));
                    categoryName = Convert.ToString(dt.Rows[r][2]);
                    campaignName = Convert.ToString(dt.Rows[r][1]);

                    if (campaignName == "")
                    {
                        campaignName = "null";
                        objResult.message = "Campaign Type is Mandatory ";
                        objResult.status = true;
                        break;
                    }
                    if (categoryName == "")
                    {
                        categoryName = "null";
                        objResult.message = "Questionnaire Category type is Mandatory ";
                        objResult.status = true;
                        break;
                    }

                    msSQL = " SELECT categorytype_gid,categorytype_name  FROM fnd_mst_tcategorytype where categorytype_name like  '%" + categoryName + "%'";
                    DataTable dt_tab = objdbconn.GetDataTable(msSQL);
                    if (dt_tab.Rows.Count == 0)
                    {
                        objResult.status = true;
                        objResult.message= "Category Name" + " " + categoryName + " " + "Does Not Exist";
                        break;
                    }
                    else
                    {

                        objResult.status = false;
                        //values.message = "Category Name" + " " + categoryName + " " + "Does Not Exist";
                    }
                 

                    msSQL = " SELECT campaigntype_gid,campaigntype_name FROM fnd_mst_tcampaigntype where campaigntype_name like '%" + campaignName +"%' ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        campaigntype_gid = dt_datatable.Rows[0][0].ToString();
                        questionnarie_name = dt.Rows[r][3].ToString().Replace("'", "\\'");
                        if(dt_tab.Rows[0][0].ToString()!=null)
                        {
                            categorytype_gid = dt_tab.Rows[0][0].ToString();
                        }
                        else
                        {
                            categorytype_gid = "";
                        }
                       
                        questionnarie_type = dt.Rows[r][5].ToString();
                        importance = dt.Rows[r][4].ToString();
                        lsQuestionnarie_answer= dt.Rows[r][6].ToString();
                        lsremarks = dt.Rows[r][7].ToString();
                        
                         if (questionnarie_name == "" || questionnarie_name == "null")
                        {
                            objResult.message = "Questions is Mandatory";
                            objResult.status = true;
                            break;
                        }
                        if (importance == "" || importance == "null")
                        {
                            objResult.message = "Give Question Mandatory ";
                            objResult.status = true;
                            break;
                        }
                        if (questionnarie_type == "" || questionnarie_type == "null")
                        {
                            objResult.message = "Answer type is Mandatory";
                            objResult.status = true;
                            break;
                        }
                        if ((questionnarie_type != "" || questionnarie_type != "null") && (questionnarie_type == "List") || (questionnarie_type == "Radio Button") || (questionnarie_type == "Radio_Button"))
                        {
                            if ((lsQuestionnarie_answer == "" || lsQuestionnarie_answer == "null") )
                            {
                                objResult.status = true;
                                objResult.message = "Answer description is Mandatory";
                                break;
                            }
                        }
                        if (lsremarks == null || lsremarks == "")
                        {
                            objResult.status = true;
                            objResult.message = "Remarks is Mandatory";
                            break;
                        }

                        msSQL = "select campaigntype_gid,questionnarie_name,categorytype_gid from fnd_mst_tquestionnarie where " +
                                " campaigntype_gid = '" + campaigntype_gid + "' and " +
                                " questionnarie_name= '" + questionnarie_name + "' and " +
                                " categorytype_gid= '" + categorytype_gid + "' ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == false)
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
                            //if (values.remarks == null || values.remarks == "")
                            //{
                            //    lsremarks = "";
                            //}
                            //else
                            //{
                            //    lsremarks = values.remarks.Replace("'", "");
                            //}

                           
                           msGetGid = objcmnfunctions.GetMasterGID("FCQM");                           


                            msSQL = " insert into fnd_mst_tquestionnarie(" +
                                        " questionnarie_gid," +
                                        " campaigntype_gid," +
                                        " questionnarie_name," +
                                        " questionnarie_type," +
                                        " questionnarie_answer," +
                                        " importance," +
                                        " lms_code," +
                                        " bureau_code," +
                                        " remarks," +
                                        " created_by," +
                                        " created_date,categorytype_gid)" +
                                        " values(" +
                                        "'" + msGetGid + "'," +
                                        "'" + campaigntype_gid + "'," +
                                        "'" + questionnarie_name + "'," +
                                        "'" + questionnarie_type + "'," +
                                        "'" + lsQuestionnarie_answer + "'," +
                                        "'" + importance + "'," +
                                        "'" + lslms_code + "'," +
                                        "'" + lsbureau_code + "'," +
                                        "'" + lsremarks + "'," +
                                        "'" + employee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                         "'" + categorytype_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                values.status = true;
                                // values.message = "Questionnarie Added Successfully";
                                string AnswerDescription = Convert.ToString(dt.Rows[r][6]);
                                string[] AnsDesc = AnswerDescription.Split(',');
                                if (AnsDesc.Length > 1)
                                {
                                    foreach (string AnswerDesc in AnsDesc)
                                    {
                                        msGetGidQA = objcmnfunctions.GetMasterGID("FCQA");
                                        msSQL = " insert into fnd_mst_tquestionnarieanswer(" +

                                                " questionnarieanswer_gid," +
                                                " campaigntype_gid," +
                                                " questionnarie_gid," +
                                                " questionnarie_type," +
                                                " questionnarie_answer," +
                                                " status, " +
                                                " created_by " +
                                                " )" +
                                                " values(" +
                                                "'" + msGetGidQA + "'," +
                                                "'" + campaigntype_gid + "'," +
                                                "'" + msGetGid + "'," +
                                                "'" + questionnarie_type + "'," +
                                                "'" + AnswerDesc + "'," +
                                                "'N', " +
                                                "'" + employee_gid + "'" +
                                                " )";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                                //msSQL = "update fnd_mst_tquestionnarieanswer set status = 'N' where created_by='" + employee_gid + "'";
                                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                               
                            }
                            else
                            {
                                values.status = false;
                               // values.message = "Error Occurred While Adding";

                            }
                            insertCount++;
                            objODBCDatareader.Close();
                        }
                     
                        else
                        {
                            objResult.message = "Records Existing";
                            mnResult = 1;
                        }
                    }
                    else
                    {
                        objResult.status = true;
                        objResult.message = "Campaign Name" + " " + campaignName + " " + "Does Not Exist";
                        objResult.message = "Campaign Name Does Not Exist";
                    }           
                              
                                                   
                  
                }

                if (mnResult != 0)
                {
                  
                    objResult.status = true;
                    objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                   // values.message= insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                    dt.Dispose();
                }


                else
                {
                    objResult.status = false;
                    if (objResult.message == "")
                    {
                        objResult.message = "Error occured in uploading Excel Sheet Details";
                    }
                  
                   // values.message= "Error occured in uploading Excel Sheet Details";
                }


            }


            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }

        }

    }

}
