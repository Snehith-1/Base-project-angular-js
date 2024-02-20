using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace ems.master.DataAccess
{
    public class DaLANWaiver
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, msGetGidREF, msGetAPICode;
        int mnResult;
        string lslms_code, lsbureau_code, lsentity_code, lsvertical_code;
        string lsvariety_name, lsbotanical_name, lsalternative_name;
        string lsmaster_value, lslanwaiver_gid;

        //Add

        public void DaPostLANWaiver(MdlMstLANWaiver values, string employee_gid)
        {
            msSQL = "select lanwaiver_name from ocs_mst_tlanwaiver where lanwaiver_name = '" + values.lanwaiver_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "LAN Waiver Name Already Exist";

            }
            else
            {
                msGetAPICode = objcmnfunctions.GetApiMasterGID("LWSC");
                msGetGid = objcmnfunctions.GetMasterGID("LANW");
                msGetGidREF = objcmnfunctions.GetMasterGID("LAN");
                msSQL = " insert into ocs_mst_tlanwaiver(" +
                       " lanwaiver_gid," +
                       " lanwaiver_code," +
                       " api_code," +
                       " lanwaiver_name," +
                       " description," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + msGetGidREF + "'," +
                       "'" + msGetAPICode + "'," +
                       "'" + values.lanwaiver_name.Replace("'", "") + "',";
                if (values.description == null || values.description == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.description.Replace("'", "") + "',";
                }
                msSQL += "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "LAN Waiver Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetLANWaiver(MdlMstLANWaiver objmaster)
        {
            try
            {
                msSQL = "select a.lanwaiver_gid,a.lanwaiver_code,a.lanwaiver_name,a.description, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                    "  case when a.status='N' then 'Inactive' else 'Active' end as Status " +
                    " from ocs_mst_tlanwaiver a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlanwaiver_list = new List<lanwaiver>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlanwaiver_list.Add(new lanwaiver
                        {
                            lanwaiver_gid = (dr_datarow["lanwaiver_gid"].ToString()),
                            lanwaiver_code = (dr_datarow["lanwaiver_code"].ToString()),
                            lanwaiver_name = (dr_datarow["lanwaiver_name"].ToString()),
                            description = (dr_datarow["description"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            Status = (dr_datarow["Status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.lanwaiver = getlanwaiver_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        //Edit

        public void DaGetLANEdit(string lanwaiver_gid, MdlMstLANWaiver objmaster)
        {
            msSQL = " select lanwaiver_gid,lanwaiver_name,lanwaiver_code,description,status as Status  from ocs_mst_tlanwaiver " +
                    " where lanwaiver_gid='" + lanwaiver_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.lanwaiver_gid = objODBCDatareader["lanwaiver_gid"].ToString();
                objmaster.lanwaiver_code = objODBCDatareader["lanwaiver_code"].ToString();
                objmaster.lanwaiver_name = objODBCDatareader["lanwaiver_name"].ToString();
                objmaster.description = objODBCDatareader["description"].ToString();
                objmaster.Status = objODBCDatareader["Status"].ToString();
            }
            objODBCDatareader.Close();

        }

        public bool DaUpdateLANWaiver(string employee_gid, MdlMstLANWaiver values)
        {
            msSQL = "select lanwaiver_gid from ocs_mst_tlanwaiver where lanwaiver_name = '" + values.lanwaiver_name.Replace("'", "\\'") + "'";
            lslanwaiver_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lslanwaiver_gid != "")
            {
                if (lslanwaiver_gid != values.lanwaiver_gid)
                {
                    values.message = "LAN Waiver Name Already Exist";
                    values.status = false;
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,lanwaiver_name,lanwaiver_code from ocs_mst_tlanwaiver where lanwaiver_gid ='" + values.lanwaiver_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("LAWL");
                    msSQL = " insert into ocs_mst_tlanwaiverlog(" +
                            " lanwaiverlog_gid," +
                            " lanwaiver_gid," +
                            " lanwaiver_code, " +
                            " lanwaiver_name," +
                            " updated_by, " +
                            " updated_date) " +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.lanwaiver_gid + "'," +
                            "'" + objODBCDatareader["lanwaiver_code"].ToString().Replace("'", "") + "'," +
                            "'" + objODBCDatareader["lanwaiver_name"].ToString().Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update ocs_mst_tlanwaiver set lanwaiver_name='" + values.lanwaiver_name.Replace("'", "") + "'," +
                    " lanwaiver_code='" + values.lanwaiver_code.Replace("'", "") + "',";
            if (values.description == null || values.description == "")
            {
                msSQL += "description='',";
            }
            else
            {
                msSQL += "description='" + values.description.Replace("'", "") + "',";

            }
            msSQL += " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where lanwaiver_gid='" + values.lanwaiver_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "LAN Waiver Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating LAN Waiver";
                return false;
            }

        }

        // Status

        public void DaInactiveLANWaiver(MdlMstLANWaiver values, string employee_gid)
        {
            msSQL = " update ocs_mst_tlanwaiver set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where lanwaiver_gid='" + values.lanwaiver_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("LAIL");

                msSQL = " insert into ocs_mst_tlanwaiverinactivelog (" +
                      " lanwaiverinactivelog_gid," +
                      " lanwaiver_gid," +
                      " lanwaiver_code," +
                      " lanwaiver_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.lanwaiver_gid + "'," +
                      " '" + values.lanwaiver_code + "'," +
                       " '" + values.lanwaiver_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "LAN Waiver Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "LAN Waiver Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaInactiveLANWaiverHistory(MdlMstLANWaiver objmaster, string lanwaiver_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from ocs_mst_tlanwaiverinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.lanwaiver_gid='" + lanwaiver_gid + "' order by a.lanwaiverinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlaninactivehistory_list = new List<laninactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlaninactivehistory_list.Add(new laninactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objmaster.laninactivehistory_list = getlaninactivehistory_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        //Delete

        public void DaDeleteLANWaiver(string lanwaiver_gid, string employee_gid, result values)
        {
            
            msSQL = " select lanwaiver_name from ocs_mst_tlanwaiver where lanwaiver_gid='" + lanwaiver_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from ocs_mst_tlanwaiver where lanwaiver_gid='" + lanwaiver_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                         "master_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'LAN Waiver'," +
                         "'" + lsmaster_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "LAN Waiver Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

            //}
        }

    }
}