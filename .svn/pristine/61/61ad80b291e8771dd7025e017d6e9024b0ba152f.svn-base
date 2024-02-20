using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;


namespace ems.audit.DataAccess
{
    public class DaAtmMstRiskCategory
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lsriskcategory_value, lslms_code, lsbureau_code, lsriskcategory_code;
        int mnResult;
        public void DaGetRiskCategory(MdlAtmMstRiskCategory values)
        {
            try
            {
                msSQL = " SELECT a.riskcategory_gid,a.riskcategory_name,a.riskcategory_code, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM atm_mst_triskcategory a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.riskcategory_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getriskcategory_list = new List<riskcategory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getriskcategory_list.Add(new riskcategory_list
                        {
                            riskcategory_gid = (dr_datarow["riskcategory_gid"].ToString()),
                            riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),

                            riskcategory_code = (dr_datarow["riskcategory_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),

                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.riskcategory_list = getriskcategory_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateRiskCategory(MdlAtmMstRiskCategory values, string employee_gid)
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

            if (values.riskcategory_code == null || values.riskcategory_code == "")
            {
                lsriskcategory_code = "";
            }
            else
            {
                lsriskcategory_code = values.riskcategory_code.Replace("'", "");
            }

            msSQL = "select riskcategory_name from atm_mst_triskcategory where riskcategory_name = '" + values.riskcategory_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Risk Category Already Exist";
            }
            else
            {

                msGetGid = objcmnfunctions.GetMasterGID("RISK");
                lsriskcategory_code = objcmnfunctions.GetMasterGID("IADRC");

                msSQL = " insert into atm_mst_triskcategory(" +
                        " riskcategory_gid," +
                        " riskcategory_name," +

                        " riskcategory_code," +
                        " lms_code," +
                        " bureau_code," +

                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.riskcategory_name.Replace("'", "") + "'," +

                        "'" + lsriskcategory_code + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +

                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Risk Category Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditRiskCategory(string riskcategory_gid, MdlAtmMstRiskCategory values)
        {
            try
            {
                msSQL = " SELECT riskcategory_gid,riskcategory_name,riskcategory_code,lms_code, bureau_code,status as Status FROM atm_mst_triskcategory where riskcategory_gid='" + riskcategory_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.riskcategory_gid = objODBCDatareader["riskcategory_gid"].ToString();
                    values.riskcategory_name = objODBCDatareader["riskcategory_name"].ToString();

                    values.riskcategory_code = objODBCDatareader["riskcategory_code"].ToString();
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

        public void DaUpdateRiskCategory(string employee_gid, MdlAtmMstRiskCategory values)
        {


            msSQL = " update atm_mst_triskcategory set " +
                 " riskcategory_name='" + values.riskcategory_name.Replace("'", "") + "'," +

                 " riskcategory_code='" + values.riskcategory_code + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +

                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where riskcategory_gid='" + values.riskcategory_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("RISL");

                msSQL = " insert into atm_mst_triskcategorylog (" +
                       " riskcategorylog_gid, " +
                       " riskcategory_gid, " +
                       " riskcategory_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.riskcategory_gid + "'," +
                       " '" + values.riskcategory_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Risk Category Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveRiskCategory(MdlAtmMstRiskCategory values, string employee_gid)
        {
            msSQL = " update atm_mst_triskcategory set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where riskcategory_gid='" + values.riskcategory_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("RIIL");

                msSQL = " insert into atm_mst_triskcategoryinactivelog (" +
                      " riskcategoryinactivelog_gid, " +
                      " riskcategory_gid," +
                      " riskcategory_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.riskcategory_gid + "'," +
                      " '" + values.riskcategory_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Risk Category Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Risk Category Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteRiskCategory(string riskcategory_gid, string employee_gid, MdlAtmMstRiskCategory values)
        {
            msSQL = " select riskcategory_gid from atm_mst_tcheckpointadd where riskcategory_gid='" + riskcategory_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Risk Category because it is mapped to Checkpointgroup";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " select riskcategory_name from atm_mst_triskcategory where riskcategory_gid='" + riskcategory_gid + "'";
                lsriskcategory_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from atm_mst_triskcategory where riskcategory_gid='" + riskcategory_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Entity Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("RIDL");
                    msSQL = " insert into atm_mst_triskcategorydeletelog(" +
                             "riskcategorydeletelog_gid, " +
                             "riskcategory_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'" + riskcategory_gid + "', " +
                             "'Risk Category'," +
                             "'" + lsriskcategory_value + "'," +
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
            }
        

        public void DaRiskCategoryInactiveLogview(string riskcategory_gid, MdlAtmMstRiskCategory values)
        {
            try
            {
                msSQL = " SELECT a.riskcategory_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM atm_mst_triskcategoryinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.riskcategory_gid ='" + riskcategory_gid + "' order by a.riskcategoryinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getriskcategory_list = new List<riskcategory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getriskcategory_list.Add(new riskcategory_list
                        {
                            riskcategory_gid = (dr_datarow["riskcategory_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.riskcategory_list = getriskcategory_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetRiskCategoryActive(MdlAtmMstRiskCategory values)
        {
            try
            {
                msSQL = "select riskcategory_gid,riskcategory_name from atm_mst_triskcategory where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getriskcategory_list = new List<riskcategory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getriskcategory_list.Add(new riskcategory_list
                        {
                            riskcategory_gid = (dr_datarow["riskcategory_gid"].ToString()),
                            riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
                        });
                    }
                    values.riskcategory_list = getriskcategory_list;
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