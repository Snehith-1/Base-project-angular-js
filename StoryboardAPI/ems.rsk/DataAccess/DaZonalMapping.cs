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
    public class DaZonalMapping
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGet_documentGid;
        string lstransferred_from, lstransfrom_stategid, lstransfrom_districtgid;
        string lsstate_name, lsdistrict_name;
        int mnResult;
        string lspath;

        public bool DaPostzonalMapping(string employee_gid, zonalMapping values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("ZMAP");

            msSQL = " insert into rsk_mst_tzonalmapping (" +
                    " zonalmapping_gid," +
                    " zonal_name," +
                    " zonalrisk_managerGid," +
                    " zonalrisk_managername," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.zonal_name + "'," +
                    "'" + values.zonalrisk_managerGid + "'," +
                    "'" + values.zonalrisk_managername + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Zonal Mapping details are added Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetEmployee(employee objemployee)
        {
            msSQL = " SELECT a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                    " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                    " where user_status<>'N' order by employee_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_employee.Add(new employee_list
                    {
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        employee_name = (dr_datarow["employee_name"].ToString()),
                        employee_id = (dr_datarow["employee_gid"].ToString())
                    });
                }
                objemployee.employee_list = get_employee;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaPostUpdatezonalMapping(string employee_gid, zonalMapping values)
        {
            msSQL = "select zonalrisk_managerGid,zonalrisk_managername,zonal_name from rsk_mst_tzonalmapping where zonalmapping_gid='" + values.zonalmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader.HasRows == true)
            {
                values.lszonalrisk_managerGid = objODBCDatareader["zonalrisk_managerGid"].ToString();
                values.lszonalrisk_managerName = objODBCDatareader["zonalrisk_managername"].ToString();
                values.lszonal_Name = objODBCDatareader["zonal_name"].ToString();
            }
            objODBCDatareader.Close();
            
            msSQL = " update rsk_mst_tzonalmapping set zonalrisk_managerGid='" + values.zonalrisk_managerGid + "', " +
                    " zonalrisk_managername='" + values.zonalrisk_managername + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    " where zonalmapping_gid='" + values.zonalmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_trn_ttier1format set zonal_riskmanagergid='" + values.zonalrisk_managerGid + "'," +
                    " zonal_riskmanagername='" + values.zonalrisk_managername + "'" +
                    " where zonal_gid='" + values.zonalmapping_gid + "' and tier1_approvalstatus!='Approved'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_trn_ttier2preparation set zonal_riskmanagergid='" + values.zonalrisk_managerGid + "'," +
                    " zonal_riskmanagername='" + values.zonalrisk_managername + "'" +
                    " where zonalmapping_gid='" + values.zonalmapping_gid + "' and tier2_approval_status!='Approved'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "update rsk_mst_tRMmapping set zonal_RM='" + values.zonalrisk_managerGid + "' where zonalmapping_gid='" + values.zonalmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_mst_tcustomer set zonal_riskmanager='" + values.zonalrisk_managerGid + "'," +
                    " zonal_riskmanagerName='" + values.zonalrisk_managername + "'" +
                    " where zonal_gid='" + values.zonalmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_mst_tstatetag2zonal set zonalrisk_managergid='" + values.zonalrisk_managerGid + "' " +
                    " where zonalmapping_gid='" + values.zonalmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select zonal_RM from rsk_trn_tallocation where zonal_gid='" + values.zonalmapping_gid + "' and status='Allocated'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader.HasRows==true)
            {
                objODBCDatareader.Close();
                msSQL = " update rsk_trn_tallocation set zonal_RM='" + values.zonalrisk_managerGid + "'" +
                        " where zonal_gid='" + values.zonalmapping_gid + "' and status='Allocated'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();

            msSQL = "select allocation_zonalRM from rsk_trn_tallocationdtl where zonal_gid='" + values.zonalmapping_gid + "' and allocation_status='Allocated'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                msSQL = " update rsk_trn_tallocationdtl set allocation_zonalRM='" + values.zonalrisk_managerGid + "'," +
                        " zonalRM_name='" + values.zonalrisk_managername + "'" +
                        " where zonal_gid='" + values.zonalmapping_gid + "' and allocation_status='Allocated'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();

            msSQL = " select allocation_transfergid,transferfrom_zonalRM,transferfrom_zonalRMname from rsk_trn_tallocationtransfer " +
                    " where transferFrom_zonalgid='" + values.zonalmapping_gid + "' and transferapproval_status = 'Pending'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if(dt_datatable.Rows.Count!=0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " update rsk_trn_ttransferapproval set zonal_approvalfrom='" + values.zonalrisk_managerGid + "'," +
                            " zonalapprovalfrom_name='" + values.zonalrisk_managername + "'" +
                            " where zonalapprovalfrom_status='Pending' and allocation_transfergid='" + dt["allocation_transfergid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                msSQL = " update rsk_trn_tallocationtransfer set transferfrom_zonalRM='" + values.zonalrisk_managerGid + "'," +
                  " transferfrom_zonalRMname='" + values.zonalrisk_managername + "'" +
                  " where transferFrom_zonalgid='" + values.zonalmapping_gid + "' and transferapproval_status='Pending'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            dt_datatable.Dispose();

            msSQL = " select allocation_transfergid,transferto_zonalRM,transferto_zonalRMname from rsk_trn_tallocationtransfer " +
                    " where transferTo_zonalgid='" + values.zonalmapping_gid + "' and transferapproval_status='Pending'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " update rsk_trn_ttransferapproval set zonal_approvalto='" + values.zonalrisk_managerGid + "'," +
                           " zonalapprovalto_name='" + values.zonalrisk_managername + "'" +
                           " where zonalapprovalto_status='Pending' and allocation_transfergid='" + dt["allocation_transfergid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " update rsk_trn_tallocationtransfer set transferto_zonalRM='" + values.zonalrisk_managerGid + "'," +
                        " transferto_zonalRMname='" + values.zonalrisk_managername + "'" +
                        " where transferTo_zonalgid='" + values.zonalmapping_gid + "' and transferapproval_status='Pending'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            dt_datatable.Dispose();

           
            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ZMPL");

                msSQL = " insert into rsk_mst_tzonalmappinglog (" +
                        " zonalmappinglog_gid, "+
                        " zonal_name," +
                        " zonalrisk_managerGidFrom, "+
                        " zonalrisk_managernameFrom," +
                        " zonalrisk_managerGidTo," +
                        " zonalrisk_managernameTo," +
                        " updated_by," +
                        " updated_date) " +
                        " values (" +
                        " '"+ msGetGid +"'," +
                        " '"+ values.lszonal_Name +"'," +
                        " '"+ values.lszonalrisk_managerGid +"'," +
                        " '"+ values.lszonalrisk_managerName +"'," +
                        " '"+ values.zonalrisk_managerGid +"'," +
                        " '"+ values.zonalrisk_managername +"'," +
                        " '"+ employee_gid +"'," +
                        " '"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Zonal Mapping details are Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetZonalMappingDtl(zonalMappinglist values)
        {

            msSQL = " select zonal_name,zonalrisk_managername,zonalrisk_managerGid,zonalmapping_gid, " +
                    " concat(c.user_firstname, c.user_lastname, ' / ', c.user_code) as created_by, date_format(a.created_date, '%d-%m-%Y') as created_date" +
                    " from rsk_mst_tzonalmapping a" +
                    " inner join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " inner join adm_mst_tuser c on c.user_gid=b.user_gid order by zonalmapping_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<zonalMapping>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new zonalMapping
                    {
                        zonalmapping_gid = dt["zonalmapping_gid"].ToString(),
                        zonal_name = dt["zonal_name"].ToString(),
                        zonalrisk_managername = dt["zonalrisk_managername"].ToString(),
                        zonalrisk_managerGid = dt["zonalrisk_managerGid"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString()
                    });
                }
                values.zonalMapping = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetViewZonalMappingDtl(string zonalmapping_gid, zonalMapping values)
        {

            msSQL = " select zonal_name,zonalrisk_managername,zonalrisk_managerGid " +
                    " from rsk_mst_tzonalmapping where zonalmapping_gid='" + zonalmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                values.zonalrisk_managername = objODBCDatareader["zonalrisk_managername"].ToString();
                values.zonalrisk_managerGid = objODBCDatareader["zonalrisk_managerGid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select assigned_RM,concat(c.user_firstname,' ',c.user_lastname,' / ' ,c.user_code) as assignedRmname from rsk_mst_trmmapping a " +
                    " left join hrm_mst_temployee b on a.assigned_RM = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where a.zonalmapping_gid = '" + zonalmapping_gid + "' and assigned_RM<> ''";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_assignedRMdtl = new List<assignedRMlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_assignedRMdtl.Add(new assignedRMlist
                    {
                        assigned_RMname = dt["assignedRmname"].ToString(),
                        assignedRM_gid = dt["assigned_RM"].ToString(),
                    });
                }
                values.assignedRMlist = get_assignedRMdtl;
            }
            dt_datatable.Dispose();
            msSQL = "select state_name,state_gid from rsk_mst_tstatetag2zonal where zonalmapping_gid='" + zonalmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_statedtl = new List<tagzonalmapping>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_statedtl.Add(new tagzonalmapping
                    {
                        state_name = dt["state_name"].ToString(),
                        state_gid = dt["state_gid"].ToString(),
                    });
                }
                values.tagzonalmapping = get_statedtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaPostStateTag2Zonal(string employee_gid, tagzonalmappinglist values)
        {

            msSQL = "delete from rsk_mst_tstatetag2zonal where zonalmapping_gid='" + values.zonalmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            foreach (string i in values.state_gid)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ZMST");

                msSQL = "select state_name from rsk_mst_trmMapping where state_gid='" + i + "'";
                string lsstate_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "Insert into rsk_mst_tstatetag2zonal( " +
                       " statetag2_zonalgid, " +
                       " zonalmapping_gid," +
                       " state_gid," +
                       " state_name," +
                       " zonalrisk_managergid," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + values.zonalmapping_gid + "'," +
                       "'" + i + "'," +
                       "'" + lsstate_name + "'," +
                       "'" + values.zonalrisk_managerGid + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_mst_tRMmapping set zonal_RM='" + values.zonalrisk_managerGid + "', " +
                        " zonalmapping_gid='" + values.zonalmapping_gid + "'" +
                        " where state_gid='" + i + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_mst_tcustomer set zonal_riskmanager='" + values.zonalrisk_managerGid + "', " +
                        " zonal_gid='" + values.zonalmapping_gid + "'" +
                        " where state_gid='" + i + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "State Tagged to Zonal Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaPostUpdateZonalMapping(tagzonalmappinglist values, string employee_gid)
        {

            msSQL = "delete from rsk_mst_tstatetag2zonal where zonalmapping_gid='" + values.zonalmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            foreach (string i in values.state_gid)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ZMST");

                msSQL = "select state_name from rsk_mst_trmMapping where state_gid='" + i + "'";
                string lsstate_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "Insert into rsk_mst_tstatetag2zonal( " +
                       " statetag2_zonalgid, " +
                       " zonalmapping_gid," +
                       " state_gid," +
                       " state_name," +
                       " zonalrisk_managergid," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + values.zonalmapping_gid + "'," +
                       "'" + i + "'," +
                       "'" + lsstate_name + "'," +
                       "'" + values.zonalrisk_managerGid + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_mst_tRMmapping set zonal_RM='" + values.zonalrisk_managerGid + "', " +
                        " zonalmapping_gid='" + values.zonalmapping_gid + "'" +
                        " where state_gid='" + i + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "State Tagged to Zonal Risk Manager Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetZonalList(zonalMappinglist values, string employee_gid)
        {

            msSQL = "select zonalmapping_gid,zonal_name,zonalrisk_managerGid,zonalrisk_managername " +
                    "from rsk_mst_tzonalmapping where zonalrisk_managerGid<>'' and zonalrisk_managerGid<>'" + employee_gid + "' order by zonalmapping_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_statedtl = new List<zonalMapping>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_statedtl.Add(new zonalMapping
                    {
                        zonalmapping_gid = dt["zonalmapping_gid"].ToString(),
                        zonal_name = dt["zonal_name"].ToString(),
                        zonalrisk_managerGid = dt["zonalrisk_managerGid"].ToString(),
                        zonalrisk_managername = dt["zonalrisk_managername"].ToString(),
                    });
                }
                values.zonalMapping = get_statedtl;

            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetRMStateDistrict(string assigned_RMGid, assignedRMdtl values)
        {

            msSQL = "select state_name,state_gid,district_name,district_gid from rsk_mst_trmmapping where assigned_RM='" + assigned_RMGid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.state_gid = objODBCDatareader["state_gid"].ToString();
                values.state_name = objODBCDatareader["state_name"].ToString();
                values.district_name = objODBCDatareader["district_name"].ToString();
                values.district_gid = objODBCDatareader["district_gid"].ToString();
            }
            objODBCDatareader.Close();

            return true;
        }
    }
}
