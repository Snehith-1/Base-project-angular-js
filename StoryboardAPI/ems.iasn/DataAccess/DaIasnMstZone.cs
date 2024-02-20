using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.iasn.Models;
using System.Configuration;
using System.IO;
using System.Text;

namespace ems.iasn.DataAccess
{
    public class DaIasnMstZone
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        OdbcDataReader objODBCDataReader;
        string msGetGid, msGetGidCode, msGetChildGid;
        int mnResult;
        result objResult = new result();

        public result DaPostZone(MdlCreateZone values, string user_gid)
        {
            msSQL = "select zonal_name from isn_mst_tzonal2rmmapping where zonal_name = '" + values.zone_name.Replace("'", "\\'") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Close();
                msSQL = " DELETE FROM isn_mst_temployeelist WHERE zone_name='" + values.zone_name.Replace("'", "\\'") + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                objResult.status = false;
                objResult.message = "Zone Name Already Exist";
            }
            else
            {
                objODBCDataReader.Close();
                msGetGid = objcmnfunctions.GetMasterGID("RMZO");
                msGetGidCode = objcmnfunctions.GetMasterGID("Zone");

                msSQL = " INSERT INTO isn_mst_tzonal2rmmapping(" +
                    " zone_gid," +
                    " zonalref_no," +
                    " zonal_name," +
                    " created_by)" +
                    " VALUES(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetGidCode + "'," +
                    "'" + values.zone_name.Replace("'", "\\'") + "'," +
                    "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = " UPDATE isn_mst_temployeelist set zone_gid='"+ msGetGid + "' where zone_name = '" + values.zone_name.Replace("'", "\\'") + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objResult.status = true;
                    objResult.message = "Zone to RM Mapping Created Successfully";

                }
                else
                {
                    msSQL = " DELETE FROM isn_mst_tzonal2rmmapping WHERE zone_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " DELETE FROM isn_mst_temployeelist WHERE zone_name='" + values.zone_name.Replace("'", "\\'") + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    objResult.status = false;
                    objResult.message = "Error Occured";
                }
            }
            return objResult;

        }

        public result DaUpdateZone(MdlUpdateZone values, string user_gid)
        {
            msSQL = " UPDATE isn_mst_tzonal2rmmapping SET" +
                " updated_by='" + user_gid + "'," +
                " updated_date=current_timestamp" +
                " WHERE zone_gid='" + values.zone_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " SELECT employeelist_gid" +
                    " FROM isn_mst_temployeelist " +
                    " WHERE employee_gid='" + values.employee_gid + "' AND employee_type='RM'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == false)
                {
                    objODBCDataReader.Close();
                    msGetChildGid = objcmnfunctions.GetMasterGID("EMPL");
                    msSQL = " INSERT INTO isn_mst_temployeelist(" +
                            " employeelist_gid," +
                            " zone_gid ," +
                            " employee_gid ," +
                            " employee_name ," +
                            " employee_emailid ," +
                            " employee_type ," +
                            " acknowledgement_flag ," +
                            " zone_name," +
                            " created_by ," +
                            " created_date )" +
                            " VALUES(" +
                            "'" + msGetChildGid + "'," +
                            "'" + values.zone_gid + "'," +
                            "'" + values.employee_gid + "'," +
                            "'" + values.employee_name + "'," +
                            "(select employee_emailid from hrm_mst_temployee where employee_gid='" + values.employee_gid + "')," +
                            "'RM'," +
                            "'" + values.acknowledgement_flag + "'," +
                            "'" + values.zone_name.Replace("'", "\\'") + "'," +
                            "'" + user_gid + "'," +
                            "current_timestamp)";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    objODBCDataReader.Close();
                }

              
                objResult.status = true;
                objResult.message = "Updated Successfully";
                return objResult;
            }

            else
            {
                objResult.status = true;
                objResult.message = "Error Occured";
                return objResult;
            }

        }

        public void DaGetZoneEdit(string zone_gid, MdlZoneEdit values)
        {
            msSQL = " SELECT a.zonalref_no,a.zone_gid,a.zonal_name " +
                  " FROM isn_mst_tzonal2rmmapping a" +
                  " WHERE a.zone_gid='" + zone_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.zoneref_no = objODBCDataReader["zonalref_no"].ToString();
                values.zone_gid = objODBCDataReader["zone_gid"].ToString();
                values.zone_name = objODBCDataReader["zonal_name"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) as employee_name," +
                    " b.employee_gid" +
                    " FROM adm_mst_tuser a " +
                    " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                    " WHERE user_status<>'N' ORDER BY a.user_firstname ASC";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.employee_list = dt_datatable.AsEnumerable().Select(row =>
                  new employee_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();


            msSQL = " select employee_gid ,employee_name, case when acknowledgement_flag = 'Y' then 'Yes' else 'No' end as acknowledgement_status " +
                    " from isn_mst_temployeelist" +
                    " where zone_gid='" + zone_gid + "' and employee_type ='RM'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlRmList = dt_datatable.AsEnumerable().Select(row => new MdlRm
                {
                    employee_gid = row["employee_gid"].ToString(),
                    employee_name = row["employee_name"].ToString(),
                    acknowledgement_status = row["acknowledgement_status"].ToString(),
                }).ToList();


            }
            dt_datatable.Dispose();


        }

        public void DaGetZoneSummary(MdlZoneSummaryList values)
        {
            msSQL = " SELECT zone_gid,zonalref_no,zonal_name" +
                " FROM isn_mst_tzonal2rmmapping" +
                " WHERE 1=1 order by zone_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlZoneSummary = dt_datatable.AsEnumerable().Select(row => new MdlZoneSummary
                {
                    zoneref_no = row["zonalref_no"].ToString(),
                    zone_gid = row["zone_gid"].ToString(),
                    zone_name = row["zonal_name"].ToString()

                }).ToList();
            }
            dt_datatable.Dispose();
        }

        public void DaGetEmployeeList(MdlEmployeeList values)
        {
            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name," +
                       " b.employee_gid" +
                       " FROM adm_mst_tuser a " +
                       " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                       " WHERE user_status<>'N' AND b.employee_gid NOT IN (SELECT employee_gid FROM isn_mst_temployeelist WHERE employee_type ='RM')" +
                       " ORDER BY a.user_firstname ASC";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.employee_list = dt_datatable.AsEnumerable().Select(row =>
                  new employee_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        public void DaGetRMStatusSummary(string zone_name, MdlZoneSummaryList values)
        {
            msSQL = " SELECT zone_gid FROM isn_mst_tzonal2rmmapping WHERE zonal_name='" + zone_name.Replace("'", "\\'") + "'";
            var zone_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_name, employee_gid, case when acknowledgement_flag = 'Y' then 'Yes' else 'No' end as acknowledgement_status FROM " +
                    " isn_mst_temployeelist where zone_name = '" + zone_name.Replace("'", "\\'") + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlRMStatusSummary = dt_datatable.AsEnumerable().Select(row => new MdlRMStatusSummary
                {
                    acknowledgement_status = row["acknowledgement_status"].ToString(),
                    employee_name = row["employee_name"].ToString(),
                    employee_gid = row["employee_gid"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();
        }

        public void DaRM_Delete(string employee_gid, MdlRMStatusSummary values)
        {
            msSQL = "delete from isn_mst_temployeelist where employee_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "RM deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting RM";
                values.status = false;
            }
        }

        public void DaZone_Delete(string zone_gid, MdlRMStatusSummary values)
        {
            msSQL = " select * from isn_mst_temployeelist where zone_gid = '" + zone_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Close();
                values.message = "Zone has Assigned, You Cannot Delete";
                values.status = false;
            }
            else
            {
                msSQL = "delete from isn_mst_tzonal2rmmapping where zone_gid='" + zone_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.message = "Zone deleted successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occrued while deleting Zone";
                    values.status = false;
                }
                objODBCDataReader.Close();
            }
        }

        public result DaPostRMName(MdlCreateZone values, string user_gid)
        {
            msSQL = " select employeelist_gid from isn_mst_temployeelist where employee_gid='" + values.employee_gid + "' and employee_type='RM'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                objODBCDataReader.Close();
                msGetChildGid = objcmnfunctions.GetMasterGID("EMPL");
                msSQL = " INSERT INTO isn_mst_temployeelist(" +
                        " employeelist_gid," +
                        " employee_gid ," +
                        " employee_name ," +
                        " employee_emailid ," +
                        " employee_type ," +
                        " acknowledgement_flag," +
                        " zone_name," +
                        " created_by ," +
                        " created_date )" +
                        " VALUES(" +
                        "'" + msGetChildGid + "'," +
                        "'" + values.employee_gid + "'," +
                        "'" + values.employee_name + "'," +
                         "(select employee_emailid from hrm_mst_temployee where employee_gid='" + values.employee_gid + "')," +
                        "'RM'," +
                         "'" + values.acknowledgement_flag + "'," +
                         "'" + values.zone_name.Replace("'", "\\'") + "'," +
                        "'" + user_gid + "'," +
                        "current_timestamp)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if(mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "RM Added Successfully";
                }
            }
            else
            {
                objResult.status = false;

                objODBCDataReader.Close();
            }
            return objResult;
        }
    }
}