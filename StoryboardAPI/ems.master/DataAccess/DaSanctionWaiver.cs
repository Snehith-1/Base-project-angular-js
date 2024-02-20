using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace ems.master.DataAccess
{
    public class DaSanctionWaiver
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, msGetGidREF, msGetAPICode;
        int mnResult;
        string lslms_code, lsbureau_code, lsentity_code, lsvertical_code;
        string lsvariety_name, lsbotanical_name, lsalternative_name;
        string lsmaster_value, lssanctionwaiver_gid;

        //Add

        public void DaPostSanctionWaiver(MdlMstSanctionWaiver values, string employee_gid)
        {
            msSQL = "select sanctionwaiver_name from ocs_mst_tsanctionwaiver where sanctionwaiver_name = '" + values.sanctionwaiver_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Sanction Waiver Name Already Exist";

            }
            else
            {
                msGetAPICode = objcmnfunctions.GetApiMasterGID("SWSC");
                msGetGid = objcmnfunctions.GetMasterGID("SANW");
                msGetGidREF = objcmnfunctions.GetMasterGID("SWC");
                msSQL = " insert into ocs_mst_tsanctionwaiver(" +
                       " sanctionwaiver_gid ," +
                       " sanctionwaiver_code ," +
                       " api_code ," +
                       " sanctionwaiver_name," +
                       " description," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + msGetGidREF + "'," +
                       "'" + msGetAPICode + "'," +
                       "'" + values.sanctionwaiver_name.Replace("'", "") + "',";
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
                    values.message = "Sanction Waiver Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetSanctionWaiver(MdlMstSanctionWaiver objmaster)
        {
            try
            {
                msSQL = "select a.sanctionwaiver_gid ,a.sanctionwaiver_code,a.sanctionwaiver_name,a.description, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, api_code," +
                    "  case when a.status='N' then 'Inactive' else 'Active' end as Status " +
                    " from ocs_mst_tsanctionwaiver a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsanctionwaiver_list = new List<sanctionwaiver>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsanctionwaiver_list.Add(new sanctionwaiver
                        {
                            sanctionwaiver_gid = (dr_datarow["sanctionwaiver_gid"].ToString()),
                            sanctionwaiver_code = (dr_datarow["sanctionwaiver_code"].ToString()),
                            sanctionwaiver_name = (dr_datarow["sanctionwaiver_name"].ToString()),
                            description = (dr_datarow["description"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            Status = (dr_datarow["Status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.sanctionwaiver = getsanctionwaiver_list;
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

        public void DaGetSanctionEdit(string sanctionwaiver_gid, MdlMstSanctionWaiver objmaster)
        {
            msSQL = " select sanctionwaiver_gid,sanctionwaiver_name,sanctionwaiver_code,description,status as Status  from ocs_mst_tsanctionwaiver " +
                    " where sanctionwaiver_gid='" + sanctionwaiver_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.sanctionwaiver_gid = objODBCDatareader["sanctionwaiver_gid"].ToString();
                objmaster.sanctionwaiver_code = objODBCDatareader["sanctionwaiver_code"].ToString();
                objmaster.sanctionwaiver_name = objODBCDatareader["sanctionwaiver_name"].ToString();
                objmaster.description = objODBCDatareader["description"].ToString();
                objmaster.Status = objODBCDatareader["Status"].ToString();
            }
            objODBCDatareader.Close();

        }

        public bool DaUpdateSanctionWaiver(string employee_gid, MdlMstSanctionWaiver values)
        {
            msSQL = "select sanctionwaiver_gid from ocs_mst_tsanctionwaiver where sanctionwaiver_name = '" + values.sanctionwaiver_name.Replace("'", "\\'") + "'";
            lssanctionwaiver_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lssanctionwaiver_gid != "")
            {
                if (lssanctionwaiver_gid != values.sanctionwaiver_gid)
                {
                    values.message = "Sanction Waiver Name Already Exist";
                    values.status = false;
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,sanctionwaiver_name,sanctionwaiver_code from ocs_mst_tsanctionwaiver where sanctionwaiver_gid ='" + values.sanctionwaiver_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SAWL");
                    msSQL = " insert into ocs_mst_tsanctionwaiverlog(" +
                            " sanctionwaiverlog_gid," +
                            " sanctionwaiver_gid," +
                            " sanctionwaiver_code , " +
                            " sanctionwaiver_name," +
                            " updated_by, " +
                            " updated_date) " +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.sanctionwaiver_gid + "'," +
                            "'" + objODBCDatareader["sanctionwaiver_code"].ToString().Replace("'", "") + "'," +
                            "'" + objODBCDatareader["sanctionwaiver_name"].ToString().Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update ocs_mst_tsanctionwaiver set sanctionwaiver_name='" + values.sanctionwaiver_name.Replace("'", "") + "'," +
                    " sanctionwaiver_code='" + values.sanctionwaiver_code.Replace("'", "") + "',";
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
                     " where sanctionwaiver_gid='" + values.sanctionwaiver_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Sanction Waiver Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Sanction Waiver";
                return false;
            }

        }

        // Status

        public void DaInactiveSanctionWaiver(MdlMstSanctionWaiver values, string employee_gid)
        {
            msSQL = " update ocs_mst_tsanctionwaiver set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where sanctionwaiver_gid='" + values.sanctionwaiver_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SWIL");

                msSQL = " insert into ocs_mst_tsanctionwaiverinactivelog (" +
                      " sanctionwaiverinactivelog_gid, " +
                      " sanctionwaiver_gid," +
                      " sanctionwaiver_code," +
                      " sanctionwaiver_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.sanctionwaiver_gid + "'," +
                      " '" + values.sanctionwaiver_code + "'," +
                       " '" + values.sanctionwaiver_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Sanction Waiver Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Sanction Waiver Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaInactiveSanctionWaiverHistory(MdlMstSanctionWaiver objmaster, string sanctionwaiver_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from ocs_mst_tsanctionwaiverinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.sanctionwaiver_gid='" + sanctionwaiver_gid + "' order by a.sanctionwaiverinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsanctioninactivehistory_list = new List<sanctioninactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsanctioninactivehistory_list.Add(new sanctioninactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objmaster.sanctioninactivehistory_list = getsanctioninactivehistory_list;
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

        public void DaDeleteSanctionWaiver(string sanctionwaiver_gid, string employee_gid, result values)
        {
            
                msSQL = " select sanctionwaiver_name from ocs_mst_tsanctionwaiver where sanctionwaiver_gid='" + sanctionwaiver_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from ocs_mst_tsanctionwaiver where sanctionwaiver_gid='" + sanctionwaiver_gid + "'";
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
                             "'Sanction Waive'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Sanction Waiver Deleted Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

        }

    }
}