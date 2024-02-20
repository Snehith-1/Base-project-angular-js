using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace ems.audit.DataAccess
{
    public class DaAtmMstPositiveConfirmity 
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lspositiveconfirmity_value, lslms_code, lsbureau_code, lspositiveconfirmity_code, lspositiveconfirmity_name;
        int mnResult;
        public void DaGetPositiveConfirmity(MdlAtmMstPositiveConfirmity values)
        {
            try
            {
                msSQL = " SELECT a.positiveconfirmity_gid,a.positiveconfirmity_name, a.positiveconfirmity_code, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM atm_mst_tpositiveconfirmity a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.positiveconfirmity_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpositiveconfirmity_list = new List<positiveconfirmity_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpositiveconfirmity_list.Add(new positiveconfirmity_list
                        {
                            positiveconfirmity_gid = (dr_datarow["positiveconfirmity_gid"].ToString()),
                            positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),

                            positiveconfirmity_code = (dr_datarow["positiveconfirmity_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),

                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.positiveconfirmity_list = getpositiveconfirmity_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaCreatePositiveConfirmity(MdlAtmMstPositiveConfirmity values, string employee_gid)
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

           
            msSQL = "select positiveconfirmity_name from atm_mst_tpositiveconfirmity where positiveconfirmity_name = '" + values.positiveconfirmity_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Positive Conformity Already Exist";
            }
            else
            {

                msGetGid = objcmnfunctions.GetMasterGID("AUPC");
                lspositiveconfirmity_code = objcmnfunctions.GetMasterGID("IADPC");

                msSQL = " insert into atm_mst_tpositiveconfirmity(" +
                        " positiveconfirmity_gid," +
                        " positiveconfirmity_name," +
                        " positiveconfirmity_code," +
                        " lms_code," +
                        " bureau_code," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.positiveconfirmity_name.Replace("'", "") + "'," +
                        "'" + lspositiveconfirmity_code + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Positive Conformity Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }

        }

        public void DaEditPositiveConfirmity(string positiveconfirmity_gid, MdlAtmMstPositiveConfirmity values)
        {
            try
            {
                msSQL = " SELECT positiveconfirmity_gid,positiveconfirmity_name,positiveconfirmity_code,lms_code, bureau_code, status as Status FROM atm_mst_tpositiveconfirmity where positiveconfirmity_gid='" + positiveconfirmity_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.positiveconfirmity_gid = objODBCDatareader["positiveconfirmity_gid"].ToString();
                    values.positiveconfirmity_name = objODBCDatareader["positiveconfirmity_name"].ToString();

                    values.positiveconfirmity_code = objODBCDatareader["positiveconfirmity_code"].ToString();
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



        public void DaUpdatePositiveConfirmity(string employee_gid, MdlAtmMstPositiveConfirmity values)
        {


            msSQL = " update atm_mst_tpositiveconfirmity set " +
                 " positiveconfirmity_name='" + values.positiveconfirmity_name.Replace("'", "") + "'," +

                 " positiveconfirmity_code='" + values.positiveconfirmity_code + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +

                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where positiveconfirmity_gid='" + values.positiveconfirmity_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("APCL");

                msSQL = " insert into atm_mst_tpositiveconfirmitylog (" +
                       " positiveconfirmitylog_gid, " +
                       " positiveconfirmity_gid, " +
                       " positiveconfirmity_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.positiveconfirmity_gid + "'," +
                       " '" + values.positiveconfirmity_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Positive Conformity Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactivePositiveConfirmity(MdlAtmMstPositiveConfirmity values, string employee_gid)
        {
            msSQL = " update atm_mst_tpositiveconfirmity set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where positiveconfirmity_gid='" + values.positiveconfirmity_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("PCIL");

                msSQL = " insert into atm_mst_tpositiveconfirmityinactivelog (" +
                      " positiveconfirmityinactivelog_gid, " +
                      " positiveconfirmity_gid," +
                      " positiveconfirmity_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.positiveconfirmity_gid + "'," +
                      " '" + values.positiveconfirmity_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Positive Conformity Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Positive Conformity Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }


        public void DaDeletePositiveConfirmity(string positiveconfirmity_gid, string employee_gid, MdlAtmMstPositiveConfirmity values)
        {
            msSQL = " select positiveconfirmity_gid from atm_mst_tcheckpointadd where positiveconfirmity_gid='" + positiveconfirmity_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Positive Confirmity because it is mapped to Checkpointgroup";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select positiveconfirmity_name from atm_mst_tpositiveconfirmity where positiveconfirmity_gid='" + positiveconfirmity_gid + "'";
                lspositiveconfirmity_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from atm_mst_tpositiveconfirmity where positiveconfirmity_gid='" + positiveconfirmity_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Positive Conformity Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("PCDL");
                    msSQL = " insert into atm_mst_tpositiveconfirmitydeletelog(" +
                             "positiveconfirmitydeletelog_gid, " +
                             "positiveconfirmity_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'" + positiveconfirmity_gid + "', " +
                             "'Positive Confirmity'," +
                             "'" + lspositiveconfirmity_value + "'," +
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


        public void DaPositiveConfirmityInactiveLogview(string positiveconfirmity_gid, MdlAtmMstPositiveConfirmity values)
        {
            try
            {
                msSQL = " SELECT a.positiveconfirmity_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM atm_mst_tpositiveconfirmityinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.positiveconfirmity_gid ='" + positiveconfirmity_gid + "' order by a.positiveconfirmityinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpositiveconfirmity_list = new List<positiveconfirmity_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpositiveconfirmity_list.Add(new positiveconfirmity_list
                        {
                            positiveconfirmity_gid = (dr_datarow["positiveconfirmity_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.positiveconfirmity_list = getpositiveconfirmity_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetPositiveConfirmityActive(MdlAtmMstPositiveConfirmity values)
        {
            try
            {
                msSQL = "select positiveconfirmity_gid,positiveconfirmity_name from atm_mst_tpositiveconfirmity where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpositiveconfirmity_list = new List<positiveconfirmity_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpositiveconfirmity_list.Add(new positiveconfirmity_list
                        {
                            positiveconfirmity_gid = (dr_datarow["positiveconfirmity_gid"].ToString()),
                            positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
                        });
                    }
                    values.positiveconfirmity_list = getpositiveconfirmity_list;
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
