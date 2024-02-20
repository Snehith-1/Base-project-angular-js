using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;
using ems.master.Models;


/// <summary>
/// (It's used for BRE) BRE DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sherin</remarks>

namespace ems.master.DataAccess
{
    public class DaMstBRE
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetGid1, msGetAPICode;
        int mnResult;

        // Group Title - Start

        public void DaGetGroupTitle(MdlMstBRE objMdlgrouptitle)
        {
            try
            {
                msSQL = " SELECT grouptitle_gid,grouptitle_name,lms_code,bureau_code,status,api_code, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,' || ',c.user_code) as created_by " +
                    " from ocs_mst_tgrouptitle a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by grouptitle_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgrouptitle_list = new List<grouptitle_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgrouptitle_list.Add(new grouptitle_list
                        {
                            grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objMdlgrouptitle.grouptitle_list = getgrouptitle_list;
                }
                dt_datatable.Dispose();
                objMdlgrouptitle.status = true;
            }
            catch
            {
                objMdlgrouptitle.status = false;
            }
        }

        public void DaCreateGroupTitle(GroupTitle values, string employee_gid)
        {
            msSQL = "select grouptitle_gid from ocs_mst_tgrouptitle where grouptitle_name = '" + values.grouptitle_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                //if (lsdocumentgid != values.grouptitle_gid)
                //{
                    values.message = " This Group Title Already Exists";
                    values.status = false;
                    return;
                //}
            }

            msGetAPICode = objcmnfunctions.GetApiMasterGID("GTAC");
            msGetGid = objcmnfunctions.GetMasterGID("GTCL");
            msSQL = " insert into ocs_mst_tgrouptitle(" +
                    " grouptitle_gid," +
                    " api_code," +
                    " lms_code," +
                    " bureau_code," +
                    " grouptitle_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.grouptitle_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Group Title Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
            }
        }

        public void DaEditGroupTitle(string grouptitle_gid, GroupTitle values)
        {
            try
            {
                msSQL = " select grouptitle_gid,lms_code,bureau_code,status ,grouptitle_name from ocs_mst_tgrouptitle where grouptitle_gid='" + grouptitle_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["status"].ToString();
                    values.grouptitle_name = objODBCDatareader["grouptitle_name"].ToString();
                    values.grouptitle_gid = objODBCDatareader["grouptitle_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateGroupTitle(string employee_gid, GroupTitle values)
        {
            msSQL = "select grouptitle_gid from ocs_mst_tgrouptitle where grouptitle_name = '" + values.grouptitle_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.grouptitle_gid)
                {
                    values.message = " This Group Title Already Exists";
                    values.status = false;
                    return;
                }
            }

            msSQL = "select updated_by, updated_date,grouptitle_name from ocs_mst_tgrouptitle where grouptitle_gid = '" + values.grouptitle_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("GTUL");
                    msSQL = " insert into ocs_trn_tgrouptitlelog(" +
                              " grouptitlelog_gid," +
                              " grouptitle_gid," +
                              " grouptitle_name, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.grouptitle_gid + "'," +
                              "'" + objODBCDatareader["grouptitle_name"].ToString() + "'," +
                               "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tgrouptitle set ";
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

            msSQL += " grouptitle_name='" + values.grouptitle_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where grouptitle_gid='" + values.grouptitle_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group Title Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        /* public void DaGroupTitleDelete(string answertype_gid, string employee_gid, answertype values)
        {
            msSQL = " select application2loan_gid from ocs_mst_tapplication2loan where answertype_gid='" + answertype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.message = "Can't able to Delete Group Title, Because it is tagged to Application Creation";
                values.status = false;
                return;
            }
            else
            {
                objODBCDatareader.Close();
                msSQL = " select grouptitle_name from ocs_mst_tanswertype where answertype_gid='" + answertype_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from ocs_mst_tanswertype where answertype_gid='" + answertype_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Group Title Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                    msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                             "master_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'Group Title'," +
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
        }*/

        public void DaGroupTitleStatusUpdate(string employee_gid, GroupTitle values)
        {

            msSQL = " update ocs_mst_tgrouptitle set status='" + values.Status + "'," +
                " remarks='" + values.remarks.Replace("'", "\\'") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where grouptitle_gid='" + values.grouptitle_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("GTSU");
                msSQL = " insert into ocs_mst_tgrouptitleinactivelog(" +
                          " grouptitleinactivelog_gid," +
                          " grouptitle_gid," +
                          " status, " +
                          " remarks, " +
                          " updated_by, " +
                          " updated_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.grouptitle_gid + "'," +
                          "'" + values.Status + "'," +
                          "'" + values.remarks.Replace("'", "\\'") + "'," +
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
        public void DaGetGroupTitleInactiveLog(string grouptitle_gid, MdlMstBRE objMdlgrouptitle)
        {
            try
            {
                msSQL = " SELECT d.grouptitle_name,a.status,a.remarks, " +
                    " date_format(a.updated_date,'%d-%m-%Y || %h:%i %p') as updated_date,concat(c.user_firstname,' ' ,c.user_lastname,' || ',c.user_code) as updated_by" +
                    " FROM ocs_mst_tgrouptitleinactivelog a" +
                    " left join hrm_mst_temployee b on a.updated_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join ocs_mst_tgrouptitle d on a.grouptitle_gid=d.grouptitle_gid where a.grouptitle_gid='" + grouptitle_gid + "'" +
                    " order by a.grouptitleinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<grouptitle_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new grouptitle_list
                        {
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                        });
                    }
                    objMdlgrouptitle.grouptitle_list = getSegment;
                }
                dt_datatable.Dispose();
                objMdlgrouptitle.status = true;

            }
            catch
            {
                objMdlgrouptitle.status = false;
            }
        }


        // Answer Type - Start
        public void DaGetAnswerType(MdlMstBRE objMdlAnswertype)
        {
            try
            {
                msSQL = " SELECT answertype_gid,answertype_name,lms_code,bureau_code,status,api_code, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,' || ',c.user_code) as created_by " +
                    " from ocs_mst_tanswertype a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by answertype_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getanswertype_list = new List<answertype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getanswertype_list.Add(new answertype_list
                        {
                            answertype_gid = (dr_datarow["answertype_gid"].ToString()),
                            answertype_name = (dr_datarow["answertype_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objMdlAnswertype.answertype_list = getanswertype_list;
                }
                dt_datatable.Dispose();
                objMdlAnswertype.status = true;
            }
            catch
            {
                objMdlAnswertype.status = false;
            }
        }

        public void DaCreateAnswerType(string employee_gid, AnswerType values)
        {
            msSQL = "select answertype_gid from ocs_mst_tanswertype where answertype_name = '" + values.answertype_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                //if (lsdocumentgid != values.answertype_gid)
                //{
                    values.message = " This Answer Type Already Exists";
                    values.status = false;
                    return;
                //}
            }
            msGetAPICode = objcmnfunctions.GetApiMasterGID("ATAC");
            msGetGid = objcmnfunctions.GetMasterGID("ATCL");
            msSQL = " insert into ocs_mst_tanswertype(" +
                    " answertype_gid," +
                    " api_code," +
                    " lms_code," +
                    " bureau_code," +
                    " answertype_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.answertype_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Answer Type Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
            }
        }

        public void DaEditAnswerType(string answertype_gid, AnswerType values)
        {
            try
            {
                msSQL = " select answertype_gid,lms_code,bureau_code,status ,answertype_name from ocs_mst_tanswertype where answertype_gid='" + answertype_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["status"].ToString();
                    values.answertype_name = objODBCDatareader["answertype_name"].ToString();
                    values.answertype_gid = objODBCDatareader["answertype_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateAnswerType(string employee_gid, AnswerType values)
        {
            msSQL = "select answertype_gid from ocs_mst_tanswertype where answertype_name = '" + values.answertype_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.answertype_gid)
                {
                    values.message = " This Answer Type Already Exists";
                    values.status = false;
                    return;
                }
            }
            msSQL = "select updated_by, updated_date,answertype_name from ocs_mst_tanswertype where answertype_gid = '" + values.answertype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ATUL");
                    msSQL = " insert into ocs_trn_tanswertypelog(" +
                              " answertypelog_gid," +
                              " answertype_gid," +
                              " answertype_name, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.answertype_gid + "'," +
                              "'" + objODBCDatareader["answertype_name"].ToString() + "'," +
                               "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tanswertype set ";
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

            msSQL += " answertype_name='" + values.answertype_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where answertype_gid='" + values.answertype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Answer Type Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        /* public void DaAnswerTypeDelete(string answertype_gid, string employee_gid, AnswerType values)
         {
             msSQL = " select application2loan_gid from ocs_mst_tapplication2loan where answertype_gid='" + answertype_gid + "'";
             objODBCDatareader = objdbconn.GetDataReader(msSQL);
             if (objODBCDatareader.HasRows == true)
             {
                 objODBCDatareader.Close();
                 values.message = "Can't able to Delete Answer Type, Because it is tagged to Application Creation";
                 values.status = false;
                 return;
             }
             else
             {
                 objODBCDatareader.Close();
                 msSQL = " select answertype_name from ocs_mst_tanswertype where answertype_gid='" + answertype_gid + "'";
                 lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                 msSQL = " delete from ocs_mst_tanswertype where answertype_gid='" + answertype_gid + "'";
                 mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                 if (mnResult != 0)
                 {
                     values.status = true;
                     values.message = "Answer Type Deleted Successfully..!";
                     msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                     msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                              "master_gid, " +
                              "master_name, " +
                              "master_value, " +
                              "deleted_by, " +
                              "deleted_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'Answer Type'," +
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
         } */

        public void DaAnswerTypeStatusUpdate(string employee_gid, AnswerType values)
        {

            msSQL = " update ocs_mst_tanswertype set status='" + values.Status + "'," +
                " remarks='" + values.remarks.Replace("'", "\\'") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where answertype_gid='" + values.answertype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ATSU");
                msSQL = " insert into ocs_mst_tanswertypeinactivelog(" +
                          " answertypeinactivelog_gid," +
                          " answertype_gid," +
                          " status, " +
                          " remarks, " +
                          " updated_by, " +
                          " updated_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.answertype_gid + "'," +
                          "'" + values.Status + "'," +
                          "'" + values.remarks.Replace("'", "\\'") + "'," +
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
        public void DaGetAnswerTypeInactiveLog(string answertype_gid, MdlMstBRE values)
        {
            try
            {
                msSQL = " SELECT d.answertype_name,a.status,a.remarks, " +
                    " date_format(a.updated_date,'%d-%m-%Y || %h:%i %p') as updated_date,concat(c.user_firstname,' ' ,c.user_lastname,' || ',c.user_code) as updated_by" +
                    " FROM ocs_mst_tanswertypeinactivelog a" +
                    " left join hrm_mst_temployee b on a.updated_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join ocs_mst_tanswertype d on a.answertype_gid=d.answertype_gid where a.answertype_gid='" + answertype_gid + "'" +
                    " order by a.answertypeinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<answertype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new answertype_list
                        {
                            answertype_name = (dr_datarow["answertype_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                        });
                    }
                    values.answertype_list = getSegment;
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