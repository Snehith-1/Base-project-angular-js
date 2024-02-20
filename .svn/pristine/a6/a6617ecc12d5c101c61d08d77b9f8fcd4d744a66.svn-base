using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.rsk.Models;
using System.Configuration;


namespace ems.rsk.DataAccess
{
    public class DaRmMapping
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGet_documentGid;
        string lsassigned_RM, lsdistrict_gid;
        string lszonalmapping_gid;
        int mnResult;

        public bool DaGetStateDtls(statedtlList values)
        {

            msSQL = "select state_gid,district_gid,state_name,district_name,postal_code from rsk_mst_tstatedetails group by state_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_statedtl = new List<statedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_statedtl.Add(new statedtl
                    {
                        state_name = dt["state_name"].ToString(),
                        state_gid = dt["state_gid"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        district_gid = dt["district_gid"].ToString(),
                    });
                }
                values.statedtl = get_statedtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetDistrictDtls(statedtlList values, string state_gid)
        {

            msSQL = " select district_name,district_gid from rsk_mst_tstatedetails where state_gid='" + state_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_statedtl = new List<statedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_statedtl.Add(new statedtl
                    {
                        district_name = dt["district_name"].ToString(),
                        district_gid = dt["district_gid"].ToString(),
                    });
                }
                values.statedtl = get_statedtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool GetZonalStateDtls(statedtlList values, string zonalmapping_gid)
        {
            msSQL = " select state_gid, state_name from rsk_mst_trmmapping where zonalmapping_gid = '" + zonalmapping_gid + "' " +
                  " group by state_gid order by state_name desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_statedtl = new List<statedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_statedtl.Add(new statedtl
                    {
                        state_name = dt["state_name"].ToString(),
                        state_gid = dt["state_gid"].ToString(),
                    });
                }
                values.statedtl = get_statedtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetDistrictList(statedtlList values)
        {

            msSQL = " select district_name,district_gid from rsk_mst_tstatedetails order by district_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_statedtl = new List<statedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_statedtl.Add(new statedtl
                    {
                        district_name = dt["district_name"].ToString(),
                        district_gid = dt["district_gid"].ToString(),
                    });
                }
                values.statedtl = get_statedtl;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaPostMappingDetails(string employee_gid, mappingdtl values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("RKPM");

            msSQL = " Insert into rsk_mst_tRMmapping( " +
                           " RMmapping_gid," +
                           " state_gid," +
                           " state_name," +
                           " district_gid," +
                           " district_name," +
                           " assigned_RM," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "', " +
                           "'" + values.state_gid + "'," +
                           "'" + values.state_name + "'," +
                           "'" + values.district_gid + "'," +
                           "'" + values.district_name + "'," +
                           "'" + values.assigned_RM + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Mapping Details are Added Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetMappingSummay(mappingdtlList values)
        {

            msSQL = " select f.zonal_name,a.RMmapping_gid,a.state_name,a.district_name,concat(c.user_firstname,' ',c.user_lastname, ' / ',c.user_code) as RMname,  " +
                    " concat(e.user_firstname,' ',e.user_lastname, ' / ',e.user_code) as ZonalRMname from rsk_mst_tRMmapping a " +
                    " left join rsk_mst_tzonalmapping f on f.zonalmapping_gid=a.zonalmapping_gid " +
                    " left join hrm_mst_temployee b on a.assigned_RM=b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.zonal_RM" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " order by a.RMmapping_gid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<mappingdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new mappingdtl
                    {
                        RMmapping_gid = dt["RMmapping_gid"].ToString(),
                        state_name = dt["state_name"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        assigned_RM = dt["RMname"].ToString(),
                        ZonalRMname = dt["ZonalRMname"].ToString(),
                        zonal_name = dt["zonal_name"].ToString(),
                    });
                }
                values.mappingdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetDeleteMappingDtl(string RMmapping_gid)
        {

            msSQL = "delete from rsk_mst_tRMmapping where RMmapping_gid='" + RMmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DaPostUpdMappingDetails(string employee_gid, mappingdtl values)
        {
            msSQL = " select concat(b.user_firstname,' ',b.user_lastname, ' / ',b.user_code) as assignedRM_name from hrm_mst_temployee a " +
                    " left join adm_mst_tuser b on a.user_gid=b.user_gid " +
                    " where a.employee_gid='" + values.assigned_RM + "'";
            string lsassignedRM_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select zonalmapping_gid,state_gid,district_gid,assigned_RM from rsk_mst_tRMmapping  " +
                    " where RMmapping_gid='" + values.RMmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                 lsassigned_RM = objODBCDatareader["assigned_RM"].ToString();
                 lsdistrict_gid = objODBCDatareader["district_gid"].ToString();
                 lszonalmapping_gid = objODBCDatareader["zonalmapping_gid"].ToString();
                objODBCDatareader.Close();
                msSQL = " select allocationdtl_gid from rsk_trn_tallocationdtl where district_gid='" + lsdistrict_gid + "' " +
                        " and allocation_assignedRM='" + lsassigned_RM + "' and allocation_flag='Y'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows==true)
                {
                    objODBCDatareader.Close();
                    msSQL =   " update rsk_trn_tallocationdtl set" +
                              " allocation_assignedRM='" + values.assigned_RM + "'," +
                              " assignedRM_name='" + lsassignedRM_name + "'" +
                              " where district_gid='" + lsdistrict_gid + "' and allocation_assignedRM='" + lsassigned_RM + "' and allocation_flag='Y'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update rsk_trn_tallocation set assigned_RM='" + values.assigned_RM + "' " +
                            " where district_gid='" + lsdistrict_gid + "' and allocation_assignedRM='" + lsassigned_RM + "' and status='Allocated'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            else
            {
                objODBCDatareader.Close();
            }
 
            msSQL = " update ocs_mst_tcustomer set assigned_RM='" + values.assigned_RM + "'" +
                    " where district_gid='" + lsdistrict_gid + "' and zonal_gid='" + lszonalmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_mst_tRMmapping set assigned_RM='" + values.assigned_RM + "'," +
               //" zonal_RM='" + values.ZonalRM_gid + "'," +
               " updated_by='" + employee_gid + "'," +
               " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               " where RMmapping_gid='" + values.RMmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "RM Mapping Details are Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaPostCustomerMappingDetails(string employee_gid, mappingdtl values)
        {

            msSQL = " update ocs_mst_tcustomer set district_gid='" + values.district_gid + "'," +
                    " zonal_gid='" + values.zonal_gid + "'," +
                    " district_name='" + values.district_name + "'," +
                    " assigned_RM='" + values.assignedRM_gid + "'," +
                    " zonal_riskmanager='" + values.ZonalRM_gid + "'," +
                    " ppa_gid='" + values.PPA_gid + "'," +
                    " PPA_name='" + values.PPA_name + "'," +
                    " RM_assignedby='" + employee_gid + "'," +
                    " RM_assigneddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where customer_gid='" + values.customer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "RM Mapping Details are Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetMappingDtl(string RMmapping_gid, mappingdtl values)
        {

            msSQL = " select state_name,district_name,assigned_RM,concat(c.user_firstname,' ',c.user_lastname, ' / ',c.user_code) as ZonalRMname  " +
                    " from rsk_mst_tRMmapping a" +
                    " left join hrm_mst_temployee b on a.zonal_RM=b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " where RMmapping_gid='" + RMmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.state_name = objODBCDatareader["state_name"].ToString();
                values.district_name = objODBCDatareader["district_name"].ToString();
                values.assigned_RM = objODBCDatareader["assigned_RM"].ToString();
                values.ZonalRMname = objODBCDatareader["ZonalRMname"].ToString();
            }
            objODBCDatareader.Close();

            return true;

        }
    }
}